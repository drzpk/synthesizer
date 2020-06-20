using NAudio.Wave;
using Synthesizer.Configuration;
using System;
using System.IO;
using System.Threading;


/// <summary>
/// Klasy zawierające logikę biznesową aplikacji
/// </summary>
namespace Synthesizer.Models
{
    /// <summary>
    /// Reprezentuje ścieżkę dziwiękową zawierającą kompletną melodię.
    /// Ścieżka składa się z linii.
    /// </summary>
    public class Track
    {
        /// <summary>
        /// Bieżący stan.
        /// </summary>
        public States State { get; private set; }
        /// <summary>
        /// Listener wywoływany, gdy stan zostanie zmieniony.
        /// </summary>
        public StateChangeCallback OnStateChange { get; set; }
        /// <summary>
        /// Listener wywoływany w momencie zmiany indeksu odtwarzanego dźwięku.
        /// </summary>
        public PositionChangeCallback OnPositionChange { get; set; }

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
        
        private const int VOLUME = 15000;
        /// <summary>
        /// Magiczna liczba dodawana na początku tablicy bajtów podczas zapisywania danych,
        /// pozwala na wykrycie formatu pliku.
        /// </summary>
        private const int MAGIC_NUMBER = 0x38dead11;

        private int lineSize;
        private Line[] lines;
        private int tempo;

        private volatile bool playerRunning;
        private volatile bool playerPlaying;

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
                    new Thread(StartPlayer).Start();
                    UpdateState(States.PLAYING);
                    break;
                case States.PAUSED:
                    playerPlaying = true;
                    UpdateState(States.PLAYING);
                    break;
            }
        }

        public void Pause()
        {
            if (State == States.PAUSED)
                return;

            playerPlaying = false;
            UpdateState(States.PAUSED);
        }

        public void Stop()
        {
            if (State == States.STOPPED)
                return;

            playerRunning = false;
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

        public WaveType GetType(int lineIndex, int noteIndex)
        {
            var line = lines[lineIndex];
            return line.GetWaveType(noteIndex);
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

        /// <summary>
        /// Zapisuje stan tej ścieżki (serializacja)
        /// </summary>
        /// <returns></returns>
        public byte[] Save()
        {
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);
                writer.Write(MAGIC_NUMBER);
                writer.Write(tempo);
                writer.Write(lineSize);
                writer.Write(lines.Length);
                foreach (var line in lines)
                {
                    line.Save(writer);
                }
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Wczytuje stan i ustawia go w tej ścieżce (deserializacja)
        /// </summary>
        /// <param name="data"></param>
        public void Load(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                var reader = new BinaryReader(stream);
                var number = reader.ReadInt32();
                if (number != MAGIC_NUMBER)
                    throw new FileFormatException("Niepoprawny format pliku");

                SetTempo(reader.ReadInt32());
                var lineSize = reader.ReadInt32();

                var length = reader.ReadInt32();

                for (var i = 0; i < length && i < lines.Length; i++)
                {
                    lines[i].Load(reader);
                }

                Resize(lineSize);
            }
        }

        /// <summary>
        /// Eksportuje ścieżkę do pliku WAV.
        /// </summary>
        /// <returns></returns>
        public byte[] Export()
        {
            using (var stream = new MemoryStream())
            {
                float[] combined = null;
                foreach (var line in lines)
                {
                    var data = line.GenerateSoundData();
                    if (combined != null)
                    {
                        float[] smaller = data.Length > combined.Length ? combined : data;
                        float[] bigger = data.Length > combined.Length ? data : combined;
                        for (var i = 0; i < smaller.Length; i++)
                            bigger[i] = smaller[i];

                        combined = bigger;
                    }
                    else
                    {
                        combined = data;
                    }

                }

                return ConvertToWavFormat(combined);
            }
        }

        private void StartPlayer()
        {
            const int SLEEP_TIME = 100;
            int currentNoteIndex = 0;
            OnPositionChange(currentNoteIndex);

            playerRunning = true;
            playerPlaying = true;

            while (playerRunning)
            {
                Thread.Sleep(SLEEP_TIME);
                if (playerPlaying)
                {
                    if (currentNoteIndex >= lines.Length)
                    {
                        playerPlaying = false;
                        playerRunning = false;
                        continue;
                    }

                    bool hasMoreNotes = false;
                    foreach (var line in lines)
                        hasMoreNotes |= line.Play(currentNoteIndex);

                    // Nie czekaj (wywołaj onInterrupt natychmiast), jeśli nie był odtworzony żaden dźwięk.
                    if (hasMoreNotes || currentNoteIndex > 0)
                        Thread.Sleep(NoteDurationMs);

                    if (!hasMoreNotes)
                    {
                        Stop();
                        break;
                    }

                    currentNoteIndex++;
                    OnPositionChange(currentNoteIndex);
                }

            }
        }

        private byte[] ConvertToWavFormat(float[] input)
        {
            // Konwersja danych dźwiękowych do formatu wymaganego przez plik WAV
            int requiredSamples = input.Length * 4;
            byte[] soundData = new byte[requiredSamples];
            var waveBuffer = new WaveBuffer(soundData);
            for (int i = 0; i < input.Length; i++)
                waveBuffer.FloatBuffer[i] = input[i];


            using (var stream = new MemoryStream())
            {
                var format = WaveFormat.CreateIeeeFloatWaveFormat(Sound.SampleRateHz, 1);
                var writer = new WaveFileWriter(stream, format);
                writer.WriteSamples(input, 0, input.Length);
                writer.Close();
                return stream.ToArray();
            }
        }

        private void UpdateState(States newState)
        {
            var oldState = State;
            State = newState;
            OnStateChange?.Invoke(newState, oldState);
        }

        public enum States
        {
            PLAYING,
            PAUSED,
            STOPPED
        }

        public delegate void StateChangeCallback(States newState, States oldState);

        /// <summary>
        /// Informuje o pozycji, która jest teraz odtwarzana.
        /// </summary>
        /// <param name="noteIndex">indeks dźwięku</param>
        public delegate void PositionChangeCallback(int noteIndex);
    }
}
