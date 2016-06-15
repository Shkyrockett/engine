using Engine.Geometry;
using Engine.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MethodSpeedTester
{
    public partial class RectanglePointTester
        : Form
    {
        private List<Point2D> points;
        private Rectangle2D rectangle;
        private List<Tuple<string, Rectangle2D>> rectangles = new List<Tuple<string, Rectangle2D>>();

        public RectanglePointTester()
        {
            InitializeComponent();
        }

        private void RectanglePointTester_Load(object sender, EventArgs e)
        {
            points = GenerateGrid(25, 25, 130, 130, 5, 5);

            rectangles.Add(new Tuple<string, Rectangle2D>("Rectangle 1", new Rectangle2D(25, 25, 50, 50)));
            rectangles.Add(new Tuple<string, Rectangle2D>("Rectangle 2", new Rectangle2D(75, 75, 50, 50)));
            rectangles.Add(new Tuple<string, Rectangle2D>("Rectangle 3", new Rectangle2D(125, 125, 100, 100)));

            rectangle = rectangles[0].Item2;

            comboBox1.DataSource = rectangles;
            comboBox1.ValueMember = "Item1";
            comboBox1.SelectedIndex = 0;
        }

        public static void DrawRectangles(Graphics g, Font font,
            Rectangle2D rectangle,
            List<Point2D> points,
            Func<Rectangle2D, Point2D, InsideOutside> method,
            string text)
        {
            g.DrawString(text, font, Brushes.Black, new Point());
            g.FillRectangle(Brushes.White, rectangle.Bounds.ToRectangleF());
            g.DrawRectangle(Pens.Black, rectangle.Bounds.ToRectangle());
            foreach (Point2D point in points)
                DrawHitPoint(g, point, method.Invoke(rectangle, point));
        }

        public static void DrawHitPoint(Graphics g, Point2D point, InsideOutside hit)
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
            }

            g.DrawLine(pointpen, new PointF((float)point.X, (float)point.Y - pointRadius), new PointF((float)point.X, (float)point.Y + pointRadius));
            g.DrawLine(pointpen, new PointF((float)point.X - pointRadius, (float)point.Y), new PointF((float)point.X + pointRadius, (float)point.Y));
        }

        public static List<Point2D> GenerateGrid(int minX, int minY, int maxX, int maxY, int stepX, int stepY)
        {
            int width = stepX == 0 ? (maxX - minX) : (maxX - minX) / stepX;
            int height = stepY == 0 ? (maxY - minY) : (maxY - minY) / stepY;
            return new List<Point2D>(
                from x in Enumerable.Range(0, width)
                from y in Enumerable.Range(0, height)
                select new Point2D(minX + (x * stepX), minY + (y * stepY)));
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            rectangle = (comboBox1.SelectedItem as Tuple<string, Rectangle2D>).Item2;
            Invalidate(true);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            DrawRectangles(e.Graphics, Font, rectangle, points,
                Experiments.Contains,
                nameof(Experiments.Contains));
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            DrawRectangles(e.Graphics, Font, rectangle, points,
                Experiments.Contains2,
                nameof(Experiments.Contains2));
        }

        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            DrawRectangles(e.Graphics, Font, rectangle, points,
                Experiments.PointOnRectangleX,
                nameof(Experiments.PointOnRectangleX));
        }

        private void pictureBox4_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox5_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox6_Paint(object sender, PaintEventArgs e)
        {
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
