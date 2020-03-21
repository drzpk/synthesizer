using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Synthesizer
{
    partial class Form1 // todo: refactoring
    {
        private readonly int SegmentWidth = 30;
        private readonly int SegmentHeight = 10;
        private readonly int SegmentVerticalCount = 20;
        private readonly int SegmentHorizontalCont = 10;

        private void BuildKeyboardGrid()
        {
            for (int w = 0; w < SegmentHorizontalCont; w++)
            {
                for (int h = 0; h < SegmentVerticalCount; h++)
                {
                    var component = new Panel();
                    component.Name = "Grid_" + w + "_" + h;
                    component.Location = new System.Drawing.Point(w * SegmentWidth, h * SegmentHeight);
                    component.Size = new System.Drawing.Size(SegmentWidth, SegmentHeight);
                    component.BackColor = System.Drawing.Color.FromArgb(255, 0, 0);
                    component.BorderStyle = BorderStyle.FixedSingle;
                    KeyboardContainer.Controls.Add(component);
                }
            }

            // Zatwierdzenie dodanych wyżej kontrolek
            KeyboardContainer.ResumeLayout();
            KeyboardContainer.PerformLayout();
        }

        private void VScroll_Scroll(object sender, ScrollEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
