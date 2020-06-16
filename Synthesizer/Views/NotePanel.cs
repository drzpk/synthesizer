using Synthesizer.Configuration;
using Synthesizer.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Synthesizer.Views
{
    /// <summary>
    /// Panel z zdefiniowanym stanem (aktywny lub nie)
    /// </summary>
    class NotePanel : Panel
    {
        public bool State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                BackColor = state ? WaveType.Color : Keyboard.inactiveNoteColor;
            }
        }
        public WaveType WaveType
        {
            get
            {
                return waveType;
            }
            set
            {
                waveType = value;
                if (state)
                    BackColor = waveType.Color;
            }
        }
        public StateChange OnStateChange { get; set; }

        private WaveType waveType;
        private bool state;

        public NotePanel(WaveType waveType)
        {
            WaveType = waveType;
            BackColor = Keyboard.inactiveNoteColor;
            SetupContextMenu();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (((MouseEventArgs)e).Button != MouseButtons.Left)
            {
                // Rekcja tylko na lewy przycisk myszy. Pod prawym jest menu kontekstowe.
                return;
            }

            state = !state;
            var newType = OnStateChange?.Invoke(state, null);

            if (state)
            {
                if (newType != null)
                    BackColor = newType.Color;
            }
            else
            {
                BackColor = Keyboard.inactiveNoteColor;
            }
        }

        private void SetupContextMenu()
        {
            var menu = new ContextMenu();
            foreach (var type in WaveType.AllTypes)
            {
                var item = new MenuItem(type.Name)
                {
                    Checked = type == WaveType
                };
                item.Click += (object sender, EventArgs e) =>
                {
                    ClickContextMenuItem(type);
                };
                menu.MenuItems.Add(item);
            }
            ContextMenu = menu;
        }

        private void ClickContextMenuItem(WaveType selectedType)
        {
            if (!state || selectedType == WaveType)
                return;

            var newSelectedIndex = WaveType.AllTypes.IndexOf(selectedType);
            for (var i = 0; i < ContextMenu.MenuItems.Count; i++)
                ContextMenu.MenuItems[i].Checked = i == newSelectedIndex;

            BackColor = selectedType.Color;
            OnStateChange?.Invoke(state, selectedType);
            waveType = selectedType;
        }

        /// <summary>
        /// Wywoływana w momencie zmiany stanu.
        /// </summary>
        /// <param name="newState">czy panel jest zaznaczony, zwracany zawsze</param>
        /// <param name="type">wybrany typ fali, zwracany tylko wtedy, gdy zostanie zmieniony</param>
        /// <returns>Nowy typ fail, który powinien zostać ustawiony, jeśli panel został zaznaczony</returns>
        public delegate WaveType StateChange(bool newState, WaveType type);
    }
}
