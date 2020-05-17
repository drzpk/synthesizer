using System;
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
        public int CurrentNoteIndex
        {
            get
            {
                return player.currentNoteIndex;
            }
        }

        private const int VOLUME = 15000; // todo

        private int lineSize;
        private Line[] lines;
        private int noteDurationMs = 1000;
        private TrackPlayer player;

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
                lines[i] = new Line(size, keys[i].frequency, noteDurationMs, VOLUME);
        }

        /// <summary>
        /// Odtwarza ścieżkę (w osobnym wątku)
        /// </summary>
        public void Play()
        {
            switch (State)
            {
                case States.STOPPED:
                    player = new TrackPlayer(lineSize, lines, noteDurationMs, Stop);
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
        /// <see cref="Synthesizer.Configuration.Keyboard.keys"/>
        public void SetState(int lineIndex, int noteIndex, bool state)
        {
            var line = lines[lineIndex];
            line.SetState(noteIndex, state);
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

        /// <summary>
        /// Ustawia tempo
        /// </summary>
        /// <param name="bpm">Wartość tempa (uderzenia na sekundę)</param>
        public void SetTempo(int bpm)
        {
            noteDurationMs = (int)Math.Floor(60.0 / bpm * 1000);
            foreach (var line in lines)
                line.NoteDurationMs = noteDurationMs;
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
