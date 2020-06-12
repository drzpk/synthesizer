using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using Synthesizer.Configuration;
using System;

/// <summary>
/// Wrapper na generator sygnału NAudio pozwalający na stopniowe wyciszanie końca próbki.
/// Pozwala na wyeliminowanie charakterystycznego kliku po zakończeniu odtwarzania danej próbki.
/// </summary>
namespace Synthesizer.Models
{
    class Wave : ISampleProvider
    {
        public WaveFormat WaveFormat { get { return signalGenerator.WaveFormat; } }

        private SignalGenerator signalGenerator;
        private int fadeStartIndex;
        private int fadeRange;
        private int readSamples;

        public Wave(SignalGeneratorType type, double frequency, int durationMs)
        {
            var totalSamples = (int)Math.Ceiling(Sound.SampleRateHz * durationMs / 1000f);

            // Określenie indeksu próbki od której powinno zacząć się wyciszanie
            fadeStartIndex = (int)Math.Ceiling((durationMs - Sound.WaveFadeoutMs) / (float)durationMs * totalSamples);
            fadeRange = totalSamples - fadeStartIndex;

            signalGenerator = new SignalGenerator(Sound.SampleRateHz, 1)
            {
                Gain = 0.2,
                Frequency = frequency,
                Type = type
            };
        }

        /// <summary>
        /// Resetuje wewnętrzny stan dźwięku umożliwiając jego ponowne odtworzenie
        /// </summary>
        public void Reset()
        {
            readSamples = 0;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            var read = signalGenerator.Read(buffer, offset, count);

            readSamples += read;
            if (readSamples >= fadeStartIndex)
            {
                // Rozpoczęcie wyciszania

                // Ilość próbek od przekroczenia cutoff (względem końca bufora)
                var diff = readSamples - fadeStartIndex;

                // Indeks rozpoczęcia wyciszania względem bufora
                var relativeStartAt = Math.Max(count - diff, 0);

                // Indeks rozpoczęcia wyciszania względem całego dźwięku
                var absoluteStartAt = readSamples - read - fadeStartIndex;

                for (var i = relativeStartAt; i < count; i++)
                {
                    var factor = 1f - (absoluteStartAt + i) / (float) fadeRange;
                    buffer[offset + i] *= factor;
                }
            }

            return read;
        }

        // todo: metoda zwracająca kompletny bufor danych podczas eksportowania do formatu audio
    }
}
