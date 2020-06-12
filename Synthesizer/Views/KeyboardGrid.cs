using Synthesizer.Models;
using System.Drawing;
using System.Windows.Forms;

namespace Synthesizer.Views
{
    class KeyboardGrid : Panel
    {
        private const string PanelNamePrefix = "GridPanel_";

        public GridUpdate OnGridUpdate { get; set; }
        /// <summary>
        /// Typ fali, który zostanie ustawiony w nowo włączanych panelach
        /// </summary>
        public WaveType WaveType { get; set; }

        private int gridWidth = 0;

        public KeyboardGrid()
        {
            WaveType = WaveType.Sin;
            AutoScroll = true;
        }

        public void ResizeGrid(int newSize)
        {
            if (gridWidth < newSize)
                ExtendGrid(newSize - gridWidth, newSize);
            else
                ShrinkGrid(newSize);

            gridWidth = newSize;
        }

        private void ExtendGrid(int startColumn, int endColumn)
        {
            SuspendLayout();
            BuildKeyboardHeader();

            for (int w = 0; w < endColumn; w++)
            {
                for (int h = 0; h < Configuration.Keyboard.keys.Count; h++)
                {
                    AddPanel(h, w);
                }
            }

            ResumeLayout();
        }

        private void ShrinkGrid(int firstRemovalIndex)
        {
            SuspendLayout();

            for (int w = gridWidth - 1; w >= firstRemovalIndex; w--)
            {
                for (int h = 0; h < Configuration.Keyboard.keys.Count; h++)
                {
                    RemovePanel(h, w);
                }
            }

            ResumeLayout();
        }

        private void AddPanel(int row, int column)
        {
            var panel = new NotePanel(WaveType)
            {
                Name = PanelNamePrefix + row.ToString() + "_" + column.ToString(),
                Location = new System.Drawing.Point(
                        (column + 1) * Configuration.Keyboard.keyPanelWidth, row * Configuration.Keyboard.keyPanelHeight),
                Size = GetSize(),
                BorderStyle = BorderStyle.FixedSingle
            };

            Controls.Add(panel);

            // W celu utworzenia domknięcia wymagane jest zadeklarowanie nowych zmiennych
            int ch = row;
            int cw = column;
            panel.OnStateChange = (newState, type) =>
            {
                OnGridUpdate?.Invoke(ch, cw, newState);
                return WaveType;
            };
        }

        private void RemovePanel(int row, int column)
        {
            Controls.RemoveByKey(PanelNamePrefix + row.ToString() + "_" + column.ToString());
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
                var label = new Label
                {
                    Name = "Label_" + key.name,
                    Location = new System.Drawing.Point(0, i * Configuration.Keyboard.keyPanelHeight),
                    Size = GetSize(),
                    Text = key.name
                };
                Controls.Add(label);
            }
        }

        private Size GetSize()
        {
            return new Size(Configuration.Keyboard.keyPanelWidth, Configuration.Keyboard.keyPanelHeight);
        }

        public delegate void GridUpdate(int row, int column, bool state);
    }
}
