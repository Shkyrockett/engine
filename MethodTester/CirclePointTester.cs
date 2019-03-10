// <copyright file="CirclePointTester.cs" company="Shkyrockett" >
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
    /// The circle point tester class.
    /// </summary>
    public partial class CirclePointTester
        : Form
    {
        /// <summary>
        /// The points.
        /// </summary>
        private List<PointF> points;

        /// <summary>
        /// The circle.
        /// </summary>
        private Circle circle;

        /// <summary>
        /// The circles.
        /// </summary>
        private readonly List<(string, Circle)> circles = new List<(string, Circle)>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CirclePointTester"/> class.
        /// </summary>
        public CirclePointTester()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The polygon point tester load.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void PolygonPointTester_Load(object sender, EventArgs e)
        {
            points = GenerateGrid(25, 25, 130, 130, 5, 5);

            circles.Add(("Circle 1", new Circle(25, 25, 10)));
            circles.Add(("Circle 2", new Circle(75, 75, 50)));
            circles.Add(("Circle 3", new Circle(125, 125, 100)));

            circle = circles[0].Item2;

            comboBox1.DataSource = circles;
            comboBox1.ValueMember = "Item1";
            comboBox1.SelectedIndex = 0;
        }

        /// <summary>
        /// The draw circles.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="font">The font.</param>
        /// <param name="circle">The circle.</param>
        /// <param name="points">The points.</param>
        /// <param name="method">The method.</param>
        /// <param name="text">The text.</param>
        public static void DrawCircles(Graphics g, Font font,
            Circle circle,
            List<PointF> points,
            Func<Circle, PointF, Inclusion> method,
            string text)
        {
            g.DrawString(text, font, Brushes.Black, new Point());
            g.FillEllipse(Brushes.White, circle.Bounds.ToRectangleF());
            g.DrawEllipse(Pens.Black, circle.Bounds.ToRectangleF());
            foreach (PointF point in points)
            {
                DrawHitPoint(g, point, method.Invoke(circle, point));
            }
        }

        /// <summary>
        /// The draw hit point.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="point">The point.</param>
        /// <param name="hit">The hit.</param>
        public static void DrawHitPoint(Graphics g, PointF point, Inclusion hit)
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

            g.DrawLine(pointpen, new PointF(point.X, point.Y - pointRadius), new PointF(point.X, point.Y + pointRadius));
            g.DrawLine(pointpen, new PointF(point.X - pointRadius, point.Y), new PointF(point.X + pointRadius, point.Y));
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
        /// <returns>The <see cref="T:List{PointF}"/>.</returns>
        public static List<PointF> GenerateGrid(int minX, int minY, int maxX, int maxY, int stepX, int stepY)
        {
            var width = stepX == 0 ? (maxX - minX) : (maxX - minX) / stepX;
            var height = stepY == 0 ? (maxY - minY) : (maxY - minY) / stepY;
            return new List<PointF>(
                from x in Enumerable.Range(0, width)
                from y in Enumerable.Range(0, height)
                select new PointF(minX + (x * stepX), minY + (y * stepY)));
        }

        /// <summary>
        /// The combo box1 selection change committed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void ComboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            circle = (((string, Circle))comboBox1.SelectedItem).Item2;
            Invalidate(true);
        }

        /// <summary>
        /// The picture box1 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //DrawCircles(e.Graphics, Font, circle, points,
            //               (s, p) => Experiments.PointInCircle(s.Center.X, s.Center.Y, s.Radius, p.X, p.Y),
            //               nameof(Experiments.PointInCircle));
        }

        /// <summary>
        /// The picture box2 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox2_Paint(object sender, PaintEventArgs e)
        {
            //DrawCircles(e.Graphics, Font, circle, points,
            //               (s, p) => Experiments.PointInCircleInline(s.Center.X, s.Center.Y, s.Radius, p.X, p.Y),
            //               nameof(Experiments.PointInCircleInline));
        }

        /// <summary>
        /// The picture box3 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox3_Paint(object sender, PaintEventArgs e)
        {
            //DrawCircles(e.Graphics, Font, circle, points,
            //               (s, p) => Experiments.PointInCirclePhilcolbourn(s.Center.X, s.Center.Y, s.Radius, p.X, p.Y),
            //               nameof(Experiments.PointInCirclePhilcolbourn));
        }

        /// <summary>
        /// The picture box4 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox4_Paint(object sender, PaintEventArgs e)
        {
            //DrawCircles(e.Graphics, Font, circle, points,
            //               (s, p) => Experiments.PointInCircleNPhilcolbourn(s.Center.X, s.Center.Y, s.Radius, p.X, p.Y),
            //               nameof(Experiments.PointInCircleNPhilcolbourn));
        }

        /// <summary>
        /// The picture box5 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox5_Paint(object sender, PaintEventArgs e)
        {
            //DrawCircles(e.Graphics, Font, circle, points,
            //               (s, p) => Experiments.PointInCircleWilliamMorrison(s.Center.X, s.Center.Y, s.Radius, p.X, p.Y),
            //               nameof(Experiments.PointInCircleWilliamMorrison));
        }

        /// <summary>
        /// The picture box6 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox6_Paint(object sender, PaintEventArgs e)
        {
            //DrawCircles(e.Graphics, Font, circle, points,
            //               (s, p) => Experiments.PointInCircleX(s.Center.X, s.Center.Y, s.Radius, p.X, p.Y),
            //               nameof(Experiments.PointInCircleX));
        }

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
