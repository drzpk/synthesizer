using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Synthesizer.Models
{
    /// <summary>
    /// Reprezentuje linię, która jest składową ścieżki.
    /// Linia reprezentuje pojedynczą wysokość dźwięku
    /// i zawiera konkretne nuty na tej wysokości, które
    /// są odtwarzane w odpowiednim momencie.
    /// </summary>
    class Line
    {
        private Note note;
        private bool[] states;
        private int noteDurationMs;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="size">Ilość dźwięków</param>
        /// <param name="frequency">Częstotliwość dżwięków w tej ścieżce</param>
        /// <param name="noteDurationMs">Czas trwania dzwięków w milisekundach</param>
        /// <param name="volume">Głośność</param>
        public Line(int size, double frequency, int noteDurationMs, UInt16 volume)
        {
            note = new Note(frequency, noteDurationMs, volume);
            states = new bool[size];
            this.noteDurationMs = noteDurationMs;
        }

        /// <summary>
        /// Odtwarza dany dźwięk ze ścieżki w tle
        /// </summary>
        /// <param name="noteIndex"></param>
        public void Play(int noteIndex)
        {
            if (states[noteIndex])
                note.Play();
        }

        /// <summary>
        /// Ustawia czy dźwięk na danej pozycji jest aktywny
        /// </summary>
        /// <param name="noteIndex">pozycja</param>
        /// <param name="state">stan</param>
        public void SetState(int noteIndex, bool state)
        {
            states[noteIndex] = state;
        }
    }
}
