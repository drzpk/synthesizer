using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Threading;

namespace Synthesizer.Models
{
    /// <summary>
    /// Reprezentuje pojedynczą nutę o określonej wysokości, barwie i czasie trwania
    /// </summary>
    class Note
    {
        public double Frequency
        {
            get { return frequency; }
            set
            {
                frequency = value;
                changed = true;
            }
        }

        public int DurationMs
        {
            get { return durationMs; }
            set
            {
                durationMs = value;
                changed = true;
            }
        }

        private double frequency;
        private int durationMs;

        private Wave wave;
        private bool changed = false;

        public Note(double frequency, int durationMs, UInt16 volume)
        {
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

        private void CreateWave()
        {
            wave = new Wave(SignalGeneratorType.SawTooth, frequency, durationMs);
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
