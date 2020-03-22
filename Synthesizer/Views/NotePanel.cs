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
        private bool state = false;

        public NotePanel()
        {
            UpdateColor();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            state = !state;
            UpdateColor();
        }

        private void UpdateColor()
        {
            BackColor = state ? Keyboard.activeNoteColor : Keyboard.inactiveNoteColor;
        }
    }
}
