using System.Threading;

namespace Synthesizer.Models
{
    /// <summary>
    /// Reprezentuje ścieżkę dziwiękową. W skład ścieżki wchodzą linie.
    /// </summary>
    class Track
    {
        public States State { get; private set; }

        private const int NOTE_DURATION_MS = 1000; // todo
        private const int VOLUME = 15000; // todo

        private int lineLength;
        private Line[] lines;
        private TrackPlayer player;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="length">Długość ścieżki (ilość dźwięków)</param>
        public Track(int length)
        {
            this.lineLength = length;
            State = States.STOPPED;

            var keys = Configuration.Keyboard.keys;
            lines = new Line[keys.Count];
            for (int i = 0; i < keys.Count; i++)
                lines[i] = new Line(length, keys[i].frequency, NOTE_DURATION_MS, VOLUME);
        }

        /// <summary>
        /// Odtwarza ścieżkę (w osobnym wątku)
        /// </summary>
        public void Play()
        {
            if (State == States.PLAYING)
                return;
            State = States.PLAYING;

            player = new TrackPlayer(lineLength, lines, NOTE_DURATION_MS);
            new Thread(player.Worker).Start();
        }

        public void Pause()
        {
            if (State == States.PAUSED)
                return;
            State = States.PAUSED;

            player.Pause();
        }

        public void Stop()
        {
            if (State == States.STOPPED)
                return;
            State = States.STOPPED;

            player.Stop();
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

        public enum States
        {
            PLAYING,
            PAUSED,
            STOPPED
        }

        private class TrackPlayer
        {
            private const int SLEEP_TIME = 100;

            private volatile bool playing;
            private volatile bool running;

            private int lineLength;
            private Line[] lines;
            private int noteDurationMs;
            private int currentNoteIndex;

            public TrackPlayer(int lineLength, Line[] lines, int noteDurationMs)
            {
                playing = true;
                running = true;

                this.lineLength = lineLength;
                this.lines = lines;
                this.noteDurationMs = noteDurationMs;
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

                        foreach (var line in lines)
                            line.Play(currentNoteIndex);

                        Thread.Sleep(noteDurationMs);
                        currentNoteIndex++;
                    }
                }
            }
        }
    }
}
