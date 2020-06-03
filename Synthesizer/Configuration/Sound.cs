namespace Synthesizer.Configuration
{
    class Sound
    {
        /// <summary>
        /// Częstotliwość próbkowania dźwięków
        /// </summary>
        public static readonly int SampleRateHz = 44100;
        /// <summary>
        /// Czas względem końca próbki dźwiękowej, po którym następuje stopniowe wyciszanie.
        /// </summary>
        public static readonly int WaveFadeoutMs = 50;
    }
}
