using Engine.Geometry;
using Engine.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MethodSpeedTester
{
    public partial class CirclePointTester
        : Form
    {
        List<PointF> points;
        Circle circle;
        List<Tuple<string, Circle>> circles = new List<Tuple<string, Circle>>();

        public CirclePointTester()
        {
            InitializeComponent();
        }

        private void PolygonPointTester_Load(object sender, EventArgs e)
        {
            points = GenerateGrid(25, 25, 130, 130, 5, 5);

            circles.Add(new Tuple<string, Circle>("Circle 1", new Circle(25, 25, 10)));
            circles.Add(new Tuple<string, Circle>("Circle 2", new Circle(75, 75, 50)));
            circles.Add(new Tuple<string, Circle>("Circle 3", new Circle(125, 125, 100)));

            circle = circles[0].Item2;

            comboBox1.DataSource = circles;
            comboBox1.ValueMember = "Item1";
            comboBox1.SelectedIndex = 0;
        }

        public static void DrawCircles(Graphics g, Font font,
            Circle circle,
            List<PointF> points,
            Func<Circle, PointF, InsideOutside> method,
            string text)
        {
            g.DrawString(text, font, Brushes.Black, new Point());
            g.FillEllipse(Brushes.White, circle.Bounds.ToRectangleF());
            g.DrawEllipse(Pens.Black, circle.Bounds.ToRectangleF());
            foreach (var point in points)
            {
                DrawHitPoint(g, point, method.Invoke(circle, point));
            }
        }

        public static void DrawHitPoint(Graphics g, PointF point, InsideOutside hit)
        {
            float pointRadius = 1;
            Pen pointpen = Pens.Red;
            switch (hit)
            {
                case InsideOutside.Inside:
                    pointpen = Pens.Lime;
                    break;
                case InsideOutside.Boundary:
                    pointpen = Pens.Magenta;
                    break;
                case InsideOutside.Outside:
                default:
                    break;
            }

            g.DrawLine(pointpen, new PointF(point.X, point.Y - pointRadius), new PointF(point.X, point.Y + pointRadius));
            g.DrawLine(pointpen, new PointF(point.X - pointRadius, point.Y), new PointF(point.X + pointRadius, point.Y));
        }

        public static List<PointF> GenerateGrid(int minX, int minY, int maxX, int maxY, int stepX, int stepY)
        {
            int width = stepX == 0 ? (maxX - minX) : (maxX - minX) / stepX;
            int height = stepY == 0 ? (maxY - minY) : (maxY - minY) / stepY;
            return new List<PointF>(
                from x in Enumerable.Range(0, width)
                from y in Enumerable.Range(0, height)
                select new PointF(minX + (x * stepX), minY + (y * stepY)));
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            circle = (comboBox1.SelectedItem as Tuple<string, Circle>).Item2;
            Invalidate(true);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            DrawCircles(e.Graphics, Font, circle, points,
                (s, p) => Experiments.PointInCircle(s.Center.X, s.Center.Y, s.Radius, p.X, p.Y),
                nameof(Experiments.PointInCircle));
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            DrawCircles(e.Graphics, Font, circle, points,
                (s, p) => Experiments.PointInCircleInline(s.Center.X, s.Center.Y, s.Radius, p.X, p.Y),
                nameof(Experiments.PointInCircleInline));
        }

        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            DrawCircles(e.Graphics, Font, circle, points,
                (s, p) => Experiments.PointInCirclePhilcolbourn(s.Center.X, s.Center.Y, s.Radius, p.X, p.Y),
                nameof(Experiments.PointInCirclePhilcolbourn));
        }

        private void pictureBox4_Paint(object sender, PaintEventArgs e)
        {
            DrawCircles(e.Graphics, Font, circle, points,
                (s, p) => Experiments.PointInCircleNPhilcolbourn(s.Center.X, s.Center.Y, s.Radius, p.X, p.Y),
                nameof(Experiments.PointInCircleNPhilcolbourn));
        }

        private void pictureBox5_Paint(object sender, PaintEventArgs e)
        {
            DrawCircles(e.Graphics, Font, circle, points,
                (s, p) => Experiments.PointInCircleWilliamMorrison(s.Center.X, s.Center.Y, s.Radius, p.X, p.Y),
                nameof(Experiments.PointInCircleWilliamMorrison));
        }

        private void pictureBox6_Paint(object sender, PaintEventArgs e)
        {
            DrawCircles(e.Graphics, Font, circle, points,
                (s, p) => Experiments.PointInCircleX(s.Center.X, s.Center.Y, s.Radius, p.X, p.Y),
                nameof(Experiments.PointInCircleX));
        }

        private void pictureBox7_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox8_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox9_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox10_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox12_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox11_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox13_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox14_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox15_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox19_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox20_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox18_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox16_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox17_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
