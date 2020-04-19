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
        public bool State { get; set; }
        public StateChange OnStateChange { get; set; }

        public NotePanel()
        {
            UpdateColor();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            State = !State;
            OnStateChange?.Invoke(State);
            UpdateColor();
        }

        private void UpdateColor()
        {
            BackColor = State ? Keyboard.activeNoteColor : Keyboard.inactiveNoteColor;
        }

        public delegate void StateChange(bool newState);
    }
}
