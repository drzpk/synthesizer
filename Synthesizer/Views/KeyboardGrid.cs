using System.Windows.Forms;

namespace Synthesizer.Views
{
    class KeyboardGrid : Panel
    {
        public GridUpdate OnGridUpdate { get; set; }

        public KeyboardGrid()
        {
            AutoScroll = true;
            BuildKeyboardGrid();
        }

        private void BuildKeyboardGrid()
        {
            SuspendLayout();
            BuildKeyboardHeader();

            const int totalHorizontalSegments = 30;
            for (int w = 0; w < totalHorizontalSegments; w++)
            {
                for (int h = 0; h < Configuration.Keyboard.keys.Count; h++)
                {
                    var panel = new NotePanel();
                    panel.Location = new System.Drawing.Point(
                        (w + 1) * Configuration.Keyboard.keyPanelWidth, h * Configuration.Keyboard.keyPanelHeight);
                    panel.Size = GetSize();
                    panel.BorderStyle = BorderStyle.FixedSingle;
                    Controls.Add(panel);

                    // W celu utworzenia domknięcia wymagane jest zadeklarowanie nowych zmiennych
                    int cw = w;
                    int ch = h;
                    panel.OnStateChange = (newState) => OnGridUpdate?.Invoke(ch, cw, newState);
                }
            }

            ResumeLayout();
        }

        /// <summary>
        /// Tworzy nagłówek klawiatury prezentujący nazwy klawiszy
        /// </summary>
        /// <returns>Maksymalna szerokość nagłówka</returns>
        private void BuildKeyboardHeader()
        {
            for (var i = 0; i < Configuration.Keyboard.keys.Count; i++)
            {
                var key = Configuration.Keyboard.keys[i];
                var label = new Label();
                label.Name = "Label_" + key.name;
                label.Location = new System.Drawing.Point(0, i * Configuration.Keyboard.keyPanelHeight);
                label.Size = GetSize();
                label.Text = key.name;
                Controls.Add(label);
            }
        }

        private System.Drawing.Size GetSize()
        {
            return new System.Drawing.Size(
                        Configuration.Keyboard.keyPanelWidth, Configuration.Keyboard.keyPanelHeight);
        }

        public delegate void GridUpdate(int row, int column, bool state);
    }
}
