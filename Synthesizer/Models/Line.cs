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
        public int NoteDurationMs
        {
            get { return noteDurationMs; }
            set
            {
                noteDurationMs = value;
                note.DurationMs = value;
            }
        }

        private Note note;
        private bool[] states;
        private int noteDurationMs;

        /// <summary>
        /// Indeks ostatniego aktywnego dźwięku, używany do określenia,
        /// czy wszystkie dźwięki na linii zostały już odtworzone.
        /// </summary>
        private int lastNoteIndex = -1;

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
        /// <returns>Czy linia nie została wyczerpana (czy są jeszcze dalsze dźwięki do odtworzenia)</returns>
        public bool Play(int noteIndex)
        {
            if (states[noteIndex])
                note.Play();

            return noteIndex < lastNoteIndex;
        }

        /// <summary>
        /// Ustawia czy dźwięk na danej pozycji jest aktywny
        /// </summary>
        /// <param name="noteIndex">pozycja</param>
        /// <param name="state">stan</param>
        public void SetState(int noteIndex, bool state)
        {
            states[noteIndex] = state;

            if (state && noteIndex > lastNoteIndex || !state)
                UpdateLastNoteIndex();
        }

        public void Resize(int newSize)
        {
            Array.Resize(ref states, newSize);
            UpdateLastNoteIndex();
        }

        private void UpdateLastNoteIndex()
        {
            lastNoteIndex = -1;
            for (int i = 0; i < states.Length; i++)
            {
                if (states[i])
                    lastNoteIndex = i;
            }
        }
    }
}
