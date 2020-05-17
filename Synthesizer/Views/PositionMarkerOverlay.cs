using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace Synthesizer.Views
{
    /// <summary>
    /// Panel odpowiadający za rysowanie pozycji obecnie odtwarzanej ścieżki.
    /// Prezroczystość kontrolek nie jest obsługiwana (https://stackoverflow.com/q/14424399),
    /// więc nie można przy obecnym modelu "kafelków" w projecie
    /// narysować jednej długiej linii biegnącej od góry do dołu i wyznaczającej
    /// odtwarzaną pozycję. Zamiast tego jest rysowana strzałka.
    /// </summary>
    class PositionMarkerOverlay : Panel
    {
        /// <summary>
        /// Interwał odświeżania pozycji markera
        /// </summary>
        private const int MarkerRefreshInterval = 100;

        private readonly Brush brush = new SolidBrush(Color.Red);
        private readonly PointF[] points = new PointF[3];

        private bool isRunning = false;
        private float speed = 0f;
        private float position = 0f;
        private float offset;

        public PositionMarkerOverlay()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            DoubleBuffered = true;
        }

        /// <summary>
        /// Rozpoczyna animację pozycji
        /// </summary>
        /// <param name="speed">szybkość przesuwania, w pikselach na sekundę</param>
        public void Start(float speed)
        {
            if (isRunning)
                return;
            isRunning = true;

            this.speed = speed / 1000f * MarkerRefreshInterval;

            new Thread(() =>
            {
                while (isRunning)
                {
                    Thread.Sleep(MarkerRefreshInterval);
                    if (!isRunning)
                        continue;
                    Invoke((MethodInvoker)delegate
                    {
                        UpdateMarkerPosition();
                    });
                }


            }).Start();
        }

        /// <summary>
        /// Zatrzymuje animację. Nie zmienia obecnej pozycji.
        /// </summary>
        public void Stop()
        {
            isRunning = false;
        }

        /// <summary>
        /// Zatrzymuje animację i resetuje pozycję wskaźnika.
        /// </summary>
        public void Reset()
        {
            Stop();
            position = 0;
            TriggerRedraw();
        }

        /// <summary>
        /// Zmienia bieżącą pozycję wskaźnika.
        /// </summary>
        public void SetPosition(float position)
        {
            this.position = position;
        }

        /// <summary>
        /// Ustawia pozycję miejsca będącego początkiem ścieżki.
        /// </summary>
        public void SetOffset(float offset)
        {
            this.offset = offset;
            TriggerRedraw();
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            UpdateMarkerLayout();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var matrix = new Matrix();
            matrix.Translate(position + offset, 0);
            e.Graphics.Transform = matrix;
            e.Graphics.FillPolygon(brush, points);
        }

        private void UpdateMarkerPosition()
        {
            position += speed;
            if (position > Width)
                isRunning = false;

            TriggerRedraw();
        }

        private void UpdateMarkerLayout()
        {
            var edgeLength = (float)(2 * Math.Sqrt(3) * Height / 3);
            points[0] = new PointF(edgeLength / -2, 0);
            points[1] = new PointF(edgeLength / 2, 0);
            points[2] = new PointF(0, Height);
        }

        private void TriggerRedraw()
        {
            Invalidate();
            Update();
        }
    }
}
