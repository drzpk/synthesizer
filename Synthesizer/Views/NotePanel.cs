using Synthesizer.Configuration;
using System;
using System.Windows.Forms;

namespace Synthesizer.Views
{
    /// <summary>
    /// Panel z zdefiniowanym stanem (aktywny lub nie)
    /// </summary>
    class NotePanel : Panel
    {
        public StateChange OnStateChange { get; set; }

        private bool state = false;

        public NotePanel()
        {
            UpdateColor();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            state = !state;
            OnStateChange?.Invoke(state);
            UpdateColor();
        }

        private void UpdateColor()
        {
            BackColor = state ? Keyboard.activeNoteColor : Keyboard.inactiveNoteColor;
        }

        public delegate void StateChange(bool newState);
    }
}
