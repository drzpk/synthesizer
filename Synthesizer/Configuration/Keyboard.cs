using System;
using System.Collections.Generic;
using System.Drawing;

namespace Synthesizer.Configuration
{
    /// <summary>
    /// Konfiguracja klawiatury
    /// </summary>
    class Keyboard
    {
        /// <summary>
        /// Szerokość pojedynczego panelu dźwięku
        /// </summary>
        public static readonly int keyPanelWidth = 35;

        /// <summary>
        /// Wysokość pojedynczego panelu dźwięku
        /// </summary>
        public static readonly int keyPanelHeight = 16;

        /// <summary>
        /// Kolor panelu reprezentującego wartość rytmiczną nieaktywnej (niegranej) nuty
        /// </summary>
        public static readonly Color inactiveNoteColor = Color.FromArgb(255, 251, 242); 

        // Kolory dla poszczególnych typów fal dźwiękowych
        public static readonly Color SineColor = Color.FromArgb(240, 88, 58);
        public static readonly Color SquareColor = Color.FromArgb(55, 219, 104);
        public static readonly Color TriangleColor = Color.FromArgb(55, 126, 219);
        public static readonly Color SawToothColor = Color.FromArgb(232, 172, 7);


        /// <summary>
        /// Lista dostępnych dźwięków
        /// </summary>
        public static readonly List<Key> keys = new List<Key>
        {
            new Key("C4", 261.63),
            new Key("C4#", 277.18),
            new Key("D4", 293.66),
            new Key("D4#", 311.13),
            new Key("E4", 329.63),
            new Key("F4", 349.23),
            new Key("F4#", 369.99),
            new Key("G4", 392.00),
            new Key("G4#", 415.30),
            new Key("A4", 440.00), // 440
            new Key("A4#", 466.16),
            new Key("H4", 493.88),
            new Key("C5", 523.25),
            new Key("C5#", 554.37),
            new Key("D5", 587.33),
            new Key("D5#", 622.25),
            new Key("E5", 659.25),
            new Key("F5", 698.46),
            new Key("F5#", 739.99),
            new Key("G5", 783.99),
            new Key("G5#", 830.61),
            new Key("A5", 880.00),
            new Key("A5#", 932.33),
            new Key("H5", 987.77),
            new Key("C6", 1046.50)
        };
    }

    public class Key
    {
        public readonly String name;
        public readonly double frequency;

        internal Key(String name, double frequency)
        {
            this.name = name;
            this.frequency = frequency;
        }
    }
}
