using System;
using System.Collections.Generic;

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
            get { return availableNotes.GetEnumerator().Current.Value.DurationMs; }
            set
            {
                foreach (var entry in availableNotes)
                {
                    entry.Value.DurationMs = value;
                }
            }
        }

        private Dictionary<WaveType, Note> availableNotes;
        private bool[] states;
        private WaveType[] types;

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
            availableNotes = new Dictionary<WaveType, Note>();
            WaveType.AllTypes.ForEach(type =>
            {
                var note = new Note(type, frequency, noteDurationMs, volume);
                availableNotes.Add(type, note);
            });

            states = new bool[size];
            types = new WaveType[size];
        }

        /// <summary>
        /// Odtwarza dany dźwięk ze ścieżki w tle
        /// </summary>
        /// <param name="noteIndex"></param>
        /// <returns>Czy linia nie została wyczerpana (czy są jeszcze dalsze dźwięki do odtworzenia)</returns>
        public bool Play(int noteIndex)
        {
            if (states[noteIndex])
            {
                var note = availableNotes[types[noteIndex]];
                note.Play();
            }

            return noteIndex < lastNoteIndex;
        }

        /// <summary>
        /// Ustawia czy dźwięk na danej pozycji jest aktywny
        /// </summary>
        /// <param name="noteIndex">pozycja</param>
        /// <param name="state">stan</param>
        /// <param name="type">typ fali</param>
        public void SetStateAndType(int noteIndex, bool state, WaveType type)
        {
            states[noteIndex] = state;
            types[noteIndex] = type;

            if (state && noteIndex > lastNoteIndex || !state)
                UpdateLastNoteIndex();
        }

        public void Resize(int newSize)
        {
            Array.Resize(ref states, newSize);
            Array.Resize(ref types, newSize);
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
