using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using Synthesizer.Configuration;
using System;
using System.Threading;

namespace Synthesizer.Models
{
    /// <summary>
    /// Reprezentuje pojedynczą nutę o określonej wysokości, barwie i czasie trwania
    /// </summary>
    class Note
    {
        public WaveType WaveType
        {
            get { return waveType; }
            set
            {
                changed = waveType != value;
                waveType = value;
            }
        }
        public double Frequency
        {
            get { return frequency; }
            set
            {
                changed = frequency != value;
                frequency = value;
            }
        }

        public int DurationMs
        {
            get { return durationMs; }
            set
            {
                changed = durationMs != value;
                durationMs = value;
            }
        }

        private WaveType waveType;
        private double frequency;
        private int durationMs;

        private Wave wave;
        private bool changed = false;

        public Note(WaveType waveType, double frequency, int durationMs, UInt16 volume)
        {
            this.waveType = waveType;
            this.frequency = frequency;
            this.DurationMs = durationMs;

            CreateWave();
        }

        /// <summary>
        /// Odtwarza dźwięk w tle
        /// </summary>
        public void Play()
        {
            if (changed)
            {
                CreateWave();
                changed = false;
            }

            wave.Reset();
            new Thread(Player).Start();
        }

        public float[] GenerateSoundData()
        {
            var size = GetBufferSize();
            var buffer = new float[size];
            wave.Read(buffer, 0, size);
            return buffer;
        }

        public float[] GenerateSilence()
        {
            var size = GetBufferSize();
            return new float[size];
        }

        private int GetBufferSize()
        {
            return (int)Math.Floor(durationMs / 1000f * Sound.SampleRateHz);
        }

        private void CreateWave()
        {
            wave = new Wave(waveType.Type, frequency, durationMs);
        }

        private void Player()
        {
            using (var waveOutEvent = new WaveOutEvent())
            {
                waveOutEvent.Init(wave);
                waveOutEvent.Play();
                Thread.Sleep(durationMs);
            }
        }
    }
}
