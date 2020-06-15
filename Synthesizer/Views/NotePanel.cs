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
        public bool State { get; set; }
        public StateChange OnStateChange { get; set; }

        private WaveType waveType;

        public NotePanel(WaveType waveType)
        {
            this.waveType = waveType;
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

            State = !State;
            var newType = OnStateChange?.Invoke(State, null);

            if (State)
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
                    Checked = type == waveType
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
            if (!State || selectedType == waveType)
                return;

            var newSelectedIndex = WaveType.AllTypes.IndexOf(selectedType);
            for (var i = 0; i < ContextMenu.MenuItems.Count; i++)
                ContextMenu.MenuItems[i].Checked = i == newSelectedIndex;

            BackColor = selectedType.Color;
            OnStateChange?.Invoke(State, selectedType);
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
