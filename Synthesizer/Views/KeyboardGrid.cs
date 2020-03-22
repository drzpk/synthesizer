using System.Windows.Forms;

namespace Synthesizer.Views
{
    class KeyboardGrid : Panel
    {
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
                    var component = new NotePanel();
                    component.Location = new System.Drawing.Point(
                        (w + 1) * Configuration.Keyboard.keyPanelWidth, h * Configuration.Keyboard.keyPanelHeight);
                    component.Size = GetSize();
                    component.BorderStyle = BorderStyle.FixedSingle;
                    Controls.Add(component);
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
    }
}
