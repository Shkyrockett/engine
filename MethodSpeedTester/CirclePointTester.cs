// <copyright file="CirclePointTester.cs" company="Shkyrockett" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
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
    /// 
    /// </summary>
    public partial class CirclePointTester
        : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private List<PointF> points;

        /// <summary>
        /// 
        /// </summary>
        private Circle circle;

        /// <summary>
        /// 
        /// </summary>
        private List<(string, Circle)> circles = new List<(string, Circle)>();

        /// <summary>
        /// 
        /// </summary>
        public CirclePointTester()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="font"></param>
        /// <param name="circle"></param>
        /// <param name="points"></param>
        /// <param name="method"></param>
        /// <param name="text"></param>
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
                DrawHitPoint(g, point, method.Invoke(circle, point));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="point"></param>
        /// <param name="hit"></param>
        public static void DrawHitPoint(Graphics g, PointF point, Inclusion hit)
        {
            float pointRadius = 1;
            Pen pointpen = Pens.Red;
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
        /// 
        /// </summary>
        /// <param name="minX"></param>
        /// <param name="minY"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        /// <param name="stepX"></param>
        /// <param name="stepY"></param>
        /// <returns></returns>
        public static List<PointF> GenerateGrid(int minX, int minY, int maxX, int maxY, int stepX, int stepY)
        {
            int width = stepX == 0 ? (maxX - minX) : (maxX - minX) / stepX;
            int height = stepY == 0 ? (maxY - minY) : (maxY - minY) / stepY;
            return new List<PointF>(
                from x in Enumerable.Range(0, width)
                from y in Enumerable.Range(0, height)
                select new PointF(minX + (x * stepX), minY + (y * stepY)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            circle = (((string, Circle))comboBox1.SelectedItem).Item2;
            Invalidate(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            DrawCircles(e.Graphics, Font, circle, points,
                (s, p) => Experiments.PointInCircle(s.Center.X, s.Center.Y, s.Radius, p.X, p.Y),
                nameof(Experiments.PointInCircle));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox2_Paint(object sender, PaintEventArgs e)
        {
            DrawCircles(e.Graphics, Font, circle, points,
                (s, p) => Experiments.PointInCircleInline(s.Center.X, s.Center.Y, s.Radius, p.X, p.Y),
                nameof(Experiments.PointInCircleInline));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox3_Paint(object sender, PaintEventArgs e)
        {
            DrawCircles(e.Graphics, Font, circle, points,
                (s, p) => Experiments.PointInCirclePhilcolbourn(s.Center.X, s.Center.Y, s.Radius, p.X, p.Y),
                nameof(Experiments.PointInCirclePhilcolbourn));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox4_Paint(object sender, PaintEventArgs e)
        {
            DrawCircles(e.Graphics, Font, circle, points,
                (s, p) => Experiments.PointInCircleNPhilcolbourn(s.Center.X, s.Center.Y, s.Radius, p.X, p.Y),
                nameof(Experiments.PointInCircleNPhilcolbourn));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox5_Paint(object sender, PaintEventArgs e)
        {
            DrawCircles(e.Graphics, Font, circle, points,
                (s, p) => Experiments.PointInCircleWilliamMorrison(s.Center.X, s.Center.Y, s.Radius, p.X, p.Y),
                nameof(Experiments.PointInCircleWilliamMorrison));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox6_Paint(object sender, PaintEventArgs e)
        {
            DrawCircles(e.Graphics, Font, circle, points,
                (s, p) => Experiments.PointInCircleX(s.Center.X, s.Center.Y, s.Radius, p.X, p.Y),
                nameof(Experiments.PointInCircleX));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox7_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox8_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox9_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox10_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox12_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox11_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox13_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox14_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox15_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox19_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox20_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox18_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox16_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox17_Paint(object sender, PaintEventArgs e)
        { }
    }
}
