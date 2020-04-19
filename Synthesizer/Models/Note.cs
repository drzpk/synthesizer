using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Synthesizer.Models
{
    /// <summary>
    /// Reprezentuje pojedynczą nutę o określonej wysokości, barwie i czasie trwania
    /// </summary>
    class Note
    {
        private const double TAU = 2 * Math.PI;
        private const int FORMAT_CHUNK_SIZE = 16;
        private const int HEADER_SIZE = 8;
        private const short FORMAT_TYPE = 1;
        private const short TRACKS = 1;
        private const int SAMPLES_PER_SECOND = 44100;
        private const short BITS_PER_SAMPLE = 16;
        private const int WAVE_SIZE = 4;

        public int DurationMs
        {
            get { return durationMs; }
            set
            {
                durationMs = value;
                GenerateSound();
            }
        }

        private double frequency;
        private int durationMs;
        private UInt16 volume;

        private Stream soundStream;
        private bool changed = false;

        public Note(double frequency, int durationMs, UInt16 volume)
        {
            this.frequency = frequency;
            this.DurationMs = durationMs;
            this.volume = volume;

            GenerateSound();
        }

        /// <summary>
        /// Odtwarza dźwięk w tle
        /// </summary>
        public void Play()
        {
            if (changed)
            {
                GenerateSound();
                changed = false;
            }

            soundStream.Seek(0, SeekOrigin.Begin);
            new SoundPlayer(soundStream).Play();
        }

        private void GenerateSound()
        {
            if (soundStream != null)
                soundStream.Dispose();
            soundStream = new MemoryStream();

            // Trzeci parametr oznacza, że strumień "soundStream" nie zostanie zamknięty
            BinaryWriter writer = new BinaryWriter(soundStream, Encoding.UTF8, true);

            short frameSize = (short)(TRACKS * ((BITS_PER_SAMPLE + 7) / 8));
            int bytesPerSecond = SAMPLES_PER_SECOND * frameSize;
            int samples = (int)((decimal)SAMPLES_PER_SECOND * durationMs / 1000);
            int dataChunkSize = samples * frameSize;
            int fileSize = WAVE_SIZE + HEADER_SIZE + FORMAT_CHUNK_SIZE + HEADER_SIZE + dataChunkSize;

            writer.Write(Encoding.ASCII.GetBytes("RIFF"));
            writer.Write(fileSize);
            writer.Write(Encoding.ASCII.GetBytes("WAVE"));
            writer.Write(Encoding.ASCII.GetBytes("fmt "));
            writer.Write(FORMAT_CHUNK_SIZE);
            writer.Write(FORMAT_TYPE);
            writer.Write(TRACKS);
            writer.Write(SAMPLES_PER_SECOND);
            writer.Write(bytesPerSecond);
            writer.Write(frameSize);
            writer.Write(BITS_PER_SAMPLE);
            writer.Write(Encoding.ASCII.GetBytes("data"));
            writer.Write(dataChunkSize);

            double theta = frequency * TAU / (double)SAMPLES_PER_SECOND;
            double amp = volume >> 2;
            for (int step = 0; step < samples; step++)
            {
                short s = (short)(amp * Math.Sin(theta * (double)step));
                writer.Write(s);
            }

            writer.Dispose();
        }
    }
}
