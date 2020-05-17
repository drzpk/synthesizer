using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Synthesizer.Views
{
    class PositionMarker : Control
    {
        private int elementSize = 0;
        private readonly Brush brush = new SolidBrush(Color.Red);
        private readonly PointF[] points = new PointF[3];


        public void SetSize(int elementSize)
        {
            this.elementSize = elementSize;
            Width = elementSize;
            Height = elementSize;
            InitializeShape();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Transparent);
            e.Graphics.FillPolygon(brush, points);
        }

        private void InitializeShape()
        {
            var baseLength = (float) (2 * Math.Sqrt(3) * elementSize / 3);
            points[0] = new PointF((elementSize - baseLength) / 2, 0f);
            points[1] = new PointF(elementSize - points[0].X, 0f);
            points[2] = new PointF(elementSize / 2f, elementSize);
        }
    }
}
