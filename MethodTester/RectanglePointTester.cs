// <copyright file="RectanglePointTester.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Engine;
using Engine.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MethodSpeedTester
{
    /// <summary>
    /// The rectangle point tester class.
    /// </summary>
    public partial class RectanglePointTester
        : Form
    {
        /// <summary>
        /// The points.
        /// </summary>
        private List<Point2D> points;

        /// <summary>
        /// The rectangle.
        /// </summary>
        private Rectangle2D rectangle;

        /// <summary>
        /// The rectangles.
        /// </summary>
        private List<(string, Rectangle2D)> rectangles = new List<(string, Rectangle2D)>();

        /// <summary>
        /// Initializes a new instance of the <see cref="RectanglePointTester"/> class.
        /// </summary>
        public RectanglePointTester()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The rectangle point tester load.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void RectanglePointTester_Load(object sender, EventArgs e)
        {
            points = GenerateGrid(25, 25, 130, 130, 5, 5);

            rectangles.Add(("Rectangle 1", new Rectangle2D(25, 25, 50, 50)));
            rectangles.Add(("Rectangle 2", new Rectangle2D(75, 75, 50, 50)));
            rectangles.Add(("Rectangle 3", new Rectangle2D(125, 125, 100, 100)));

            rectangle = rectangles[0].Item2;

            comboBox1.DataSource = rectangles;
            comboBox1.ValueMember = "Item1";
            comboBox1.SelectedIndex = 0;
        }

        /// <summary>
        /// The draw rectangles.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="font">The font.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="points">The points.</param>
        /// <param name="method">The method.</param>
        /// <param name="text">The text.</param>
        public static void DrawRectangles(Graphics g, Font font,
            Rectangle2D rectangle,
            List<Point2D> points,
            Func<Rectangle2D, Point2D, Inclusion> method,
            string text)
        {
            g.DrawString(text, font, Brushes.Black, new Point());
            g.FillRectangle(Brushes.White, rectangle.Bounds.ToRectangleF());
            g.DrawRectangle(Pens.Black, rectangle.Bounds.ToRectangle());
            foreach (var point in points)
            {
                DrawHitPoint(g, point, method.Invoke(rectangle, point));
            }
        }

        /// <summary>
        /// The draw hit point.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="point">The point.</param>
        /// <param name="hit">The hit.</param>
        public static void DrawHitPoint(Graphics g, Point2D point, Inclusion hit)
        {
            const float pointRadius = 1;
            var pointpen = Pens.Red;
            switch (hit)
            {
                case Inclusion.Inside:
                    pointpen = Pens.Lime;
                    break;
                case Inclusion.Boundary:
                    pointpen = Pens.Magenta;
                    break;
            }

            g.DrawLine(pointpen, new PointF((float)point.X, (float)point.Y - pointRadius), new PointF((float)point.X, (float)point.Y + pointRadius));
            g.DrawLine(pointpen, new PointF((float)point.X - pointRadius, (float)point.Y), new PointF((float)point.X + pointRadius, (float)point.Y));
        }

        /// <summary>
        /// The generate grid.
        /// </summary>
        /// <param name="minX">The minX.</param>
        /// <param name="minY">The minY.</param>
        /// <param name="maxX">The maxX.</param>
        /// <param name="maxY">The maxY.</param>
        /// <param name="stepX">The stepX.</param>
        /// <param name="stepY">The stepY.</param>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        public static List<Point2D> GenerateGrid(int minX, int minY, int maxX, int maxY, int stepX, int stepY)
        {
            var width = stepX == 0 ? (maxX - minX) : (maxX - minX) / stepX;
            var height = stepY == 0 ? (maxY - minY) : (maxY - minY) / stepY;
            return new List<Point2D>(
                from x in Enumerable.Range(0, width)
                from y in Enumerable.Range(0, height)
                select new Point2D(minX + (x * stepX), minY + (y * stepY)));
        }

        /// <summary>
        /// The combo box1 selection change committed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ComboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            rectangle = (((string, Rectangle2D))comboBox1.SelectedItem).Item2;
            Invalidate(true);
        }

        /// <summary>
        /// The picture box1 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //DrawRectangles(e.Graphics, Font, rectangle, points,
            //               Experiments.Contains,
            //               nameof(Experiments.Contains));
        }

        /// <summary>
        /// The picture box2 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox2_Paint(object sender, PaintEventArgs e)
        {
            //DrawRectangles(e.Graphics, Font, rectangle, points,
            //               Experiments.Contains2,
            //               nameof(Experiments.Contains2));
        }

        /// <summary>
        /// The picture box3 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox3_Paint(object sender, PaintEventArgs e)
        {
            //DrawRectangles(e.Graphics, Font, rectangle, points,
            //               Experiments.PointOnRectangleX,
            //               nameof(Experiments.PointOnRectangleX));
        }

        /// <summary>
        /// The picture box4 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private static void PictureBox4_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// The picture box5 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private static void PictureBox5_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// The picture box6 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private static void PictureBox6_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// The picture box7 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private static void PictureBox7_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// The picture box8 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private static void PictureBox8_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// The picture box9 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private static void PictureBox9_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// The picture box10 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private static void PictureBox10_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// The picture box12 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private static void PictureBox12_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// The picture box11 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private static void PictureBox11_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// The picture box13 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private static void PictureBox13_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// The picture box14 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private static void PictureBox14_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// The picture box15 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private static void PictureBox15_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// The picture box19 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private static void PictureBox19_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// The picture box20 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private static void PictureBox20_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// The picture box18 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private static void PictureBox18_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// The picture box16 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private static void PictureBox16_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// The picture box17 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private static void PictureBox17_Paint(object sender, PaintEventArgs e)
        { }
    }
}
