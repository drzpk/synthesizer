using System;
using System.IO;
using System.Threading;

namespace Synthesizer.Models
{
    /// <summary>
    /// Reprezentuje ścieżkę dziwiękową. W skład ścieżki wchodzą linie.
    /// </summary>
    class Track
    {
        public States State { get; private set; }
        public StateChangeCallback onStateChange;
        public int NoteDurationMs
        {
            get
            {
                return lines[0].NoteDurationMs;
            }
            set
            {
                foreach (var line in lines)
                    line.NoteDurationMs = value;
            }
        }
        public int CurrentNoteIndex
        {
            get
            {
                return player.currentNoteIndex;
            }
        }

        private const int VOLUME = 15000; // todo
        /// <summary>
        /// Magiczna liczba dodawana na początku tablicy bajtów podczas zapisywania danych,
        /// pozwala na wykrycie formatu pliku.
        /// </summary>
        private const int MAGIC_NUMBER = 0x38dead11;

        private int lineSize;
        private Line[] lines;
        private TrackPlayer player;
        private int tempo;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="size">Początkowa długość ścieżki (ilość dźwięków)</param>
        public Track(int size)
        {
            lineSize = size;
            State = States.STOPPED;

            var keys = Configuration.Keyboard.keys;
            lines = new Line[keys.Count];
            for (int i = 0; i < keys.Count; i++)
                lines[i] = new Line(size, keys[i].frequency, 1000, VOLUME);
        }

        /// <summary>
        /// Odtwarza ścieżkę (w osobnym wątku)
        /// </summary>
        public void Play()
        {
            switch (State)
            {
                case States.STOPPED:
                    player = new TrackPlayer(lineSize, lines, NoteDurationMs, Stop);
                    new Thread(player.Worker).Start();
                    UpdateState(States.PLAYING);
                    break;
                case States.PAUSED:
                    player.Start();
                    UpdateState(States.PLAYING);
                    break;
            }
        }

        public void Pause()
        {
            if (State == States.PAUSED)
                return;

            player.Pause();
            UpdateState(States.PAUSED);
        }

        public void Stop()
        {
            if (State == States.STOPPED)
                return;

            player.Stop();
            UpdateState(States.STOPPED);
        }

        /// <summary>
        /// Ustawia czy dźwięk na danej pozycji jest aktywny
        /// </summary>
        /// <param name="lineIndex">Indeks linii. Numerowanie według zmiennej Keyboard.keys</param>
        /// <param name="noteIndex">Indeks dźwięku</param>
        /// <param name="state">Nowy stan</param>
        /// <param name="type">Nowy typ</param>
        /// <see cref="Synthesizer.Configuration.Keyboard.keys"/>
        public void SetStateAndType(int lineIndex, int noteIndex, bool state, WaveType type)
        {
            var line = lines[lineIndex];
            line.SetStateAndType(noteIndex, state, type);
        }

        public bool GetState(int lineIndex, int noteIndex)
        {
            var line = lines[lineIndex];
            return line.GetState(noteIndex);
        }

        public WaveType GetWaveType(int lineIndex, int noteIndex)
        {
            var line = lines[lineIndex];
            return line.GetWaveType(noteIndex);
        }

        /// <summary>
        /// Zmienia długość ścieżki
        /// </summary>
        public void Resize(int newSize)
        {
            if (newSize < 1)
                throw new ArgumentOutOfRangeException("Rozmiar ścieżki musi być > 0");

            foreach (var line in lines)
                line.Resize(newSize);
            lineSize = newSize;
        }

        public int GetSize()
        {
            return lineSize;
        }

        /// <summary>
        /// Ustawia tempo
        /// </summary>
        /// <param name="bpm">Wartość tempa (uderzenia na sekundę)</param>
        public void SetTempo(int bpm)
        {
            tempo = bpm;
            NoteDurationMs = (int)Math.Floor(60.0 / bpm * 1000);
            foreach (var line in lines)
                line.NoteDurationMs = NoteDurationMs;
        }

        public int GetTempo()
        {
            return tempo;
        }

        public byte[] Save()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);
                writer.Write(MAGIC_NUMBER);
                writer.Write(NoteDurationMs);
                writer.Write(lines.Length);
                foreach (var line in lines)
                {
                    line.Save(writer);
                }
                return stream.ToArray();
            }
        }

        public void Load(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                var reader = new BinaryReader(stream);
                var number = reader.ReadInt32();
                if (number != MAGIC_NUMBER)
                    throw new FileFormatException("Niepoprawny format pliku");

                NoteDurationMs = reader.ReadInt32();
                var length = reader.ReadInt32();
                Resize(length);

                for (var i = 0; i < length && i < lines.Length; i++)
                {
                    lines[i].Load(reader);
                }
            }
        }

        private void UpdateState(States newState)
        {
            var oldState = State;
            State = newState;
            onStateChange?.Invoke(newState, oldState);
        }

        public enum States
        {
            PLAYING,
            PAUSED,
            STOPPED
        }

        public delegate void StateChangeCallback(States newState, States oldState);

        private class TrackPlayer
        {
            private const int SLEEP_TIME = 100;

            private volatile bool playing;
            private volatile bool running;

            private int lineLength;
            private Line[] lines;
            private int noteDurationMs;
            InterruptCallcack onInterrupt;
            internal int currentNoteIndex;

            public TrackPlayer(int lineLength, Line[] lines, int noteDurationMs, InterruptCallcack onInterrupt)
            {
                playing = true;
                running = true;

                this.lineLength = lineLength;
                this.lines = lines;
                this.noteDurationMs = noteDurationMs;
                this.onInterrupt = onInterrupt;
            }

            public void Start()
            {
                playing = true;
            }

            public void Pause()
            {
                playing = false;
            }

            public void Stop()
            {
                running = false;
            }

            public void Worker()
            {
                while (running)
                {
                    Thread.Sleep(SLEEP_TIME);
                    if (playing)
                    {
                        if (currentNoteIndex >= lineLength)
                        {
                            playing = false;
                            running = false;
                            continue;
                        }

                        bool hasMoreNotes = false;
                        foreach (var line in lines)
                            hasMoreNotes |= line.Play(currentNoteIndex);

                        // Nie czekaj (wywołaj onInterrupt natychmiast), jeśli nie był odtworzony
                        // żaden dźwięk.
                        if (hasMoreNotes || currentNoteIndex > 0)
                            Thread.Sleep(noteDurationMs);

                        if (!hasMoreNotes)
                            onInterrupt.Invoke();

                        currentNoteIndex++;
                    }

                }
            }

            public delegate void InterruptCallcack();
        }
    }
}
