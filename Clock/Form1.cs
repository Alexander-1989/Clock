using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Graphics graphics = null;
        private readonly Pen circlePen = new Pen(Color.Orange, 4);
        private readonly Pen secondsPen = new Pen(Color.Red, 4);
        private readonly Pen minutesPen = new Pen(Color.Green, 4);
        private readonly Pen hoursPen = new Pen(Color.Blue, 4);
        private const int DegreesPerHour = 360 / 12;
        private const int DegreesPerMinute = 360 / 60;
        private const int DegreesPerSecond = 360 / 60;
        private const int radius = 120;
        private readonly Point center = new Point(20 + radius, 20 + radius);

        private double DegreesToRadian(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        private Point GetEndPoint(Point startPoint, int length, double degrees)
        {
            double angle = DegreesToRadian(degrees);
            int x = (int)(startPoint.X + (length * Math.Cos(angle)));
            int y = (int)(startPoint.Y + (length * Math.Sin(angle)));
            return new Point(x, y);
        }

        private void DrawCircle(Pen pen, Point center, int radius, Graphics graphics)
        {
            graphics.DrawArc(pen, center.X - radius, center.Y - radius, radius * 2, radius * 2, 0, 360);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (graphics != null)
            {
                DateTime now = DateTime.Now;
                int seconds = now.Second;
                int minutes = now.Minute;
                int hours = now.Hour % 12;

                graphics.Clear(DefaultBackColor);
                DrawCircle(circlePen, center, radius, graphics);

                int secondsAngle = (DegreesPerSecond * seconds) - 90;
                graphics.DrawLine(secondsPen, center, GetEndPoint(center, radius, secondsAngle));

                int minutesAngle = (DegreesPerMinute * minutes) + (DegreesPerMinute * seconds / 60) - 90;
                graphics.DrawLine(minutesPen, center, GetEndPoint(center, (int)(radius * 0.7), minutesAngle));

                int hoursAngle = (DegreesPerHour * hours) + (DegreesPerHour * minutes / 60) - 90;
                graphics.DrawLine(hoursPen, center, GetEndPoint(center, (int)(radius * 0.5), hoursAngle));

                pictureBox1.Invalidate();
            }
        }
    }
}