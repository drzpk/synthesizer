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
        /// <summary>
        /// Ilość punktów do uwzględnienia podczas rysowania wizualizacji fal dźwiękowych
        /// </summary>
        public static readonly int VisualizationPointCount = 200;
    }
}
