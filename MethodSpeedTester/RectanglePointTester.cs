// <copyright file="RectanglePointTester.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
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
    /// 
    /// </summary>
    public partial class RectanglePointTester
        : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private List<Point2D> points;

        /// <summary>
        /// 
        /// </summary>
        private Rectangle2D rectangle;

        /// <summary>
        /// 
        /// </summary>
        private List<(string, Rectangle2D)> rectangles = new List<(string, Rectangle2D)>();

        /// <summary>
        /// 
        /// </summary>
        public RectanglePointTester()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="font"></param>
        /// <param name="rectangle"></param>
        /// <param name="points"></param>
        /// <param name="method"></param>
        /// <param name="text"></param>
        public static void DrawRectangles(Graphics g, Font font,
            Rectangle2D rectangle,
            List<Point2D> points,
            Func<Rectangle2D, Point2D, Inclusion> method,
            string text)
        {
            g.DrawString(text, font, Brushes.Black, new Point());
            g.FillRectangle(Brushes.White, rectangle.Bounds.ToRectangleF());
            g.DrawRectangle(Pens.Black, rectangle.Bounds.ToRectangle());
            foreach (Point2D point in points)
                DrawHitPoint(g, point, method.Invoke(rectangle, point));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="point"></param>
        /// <param name="hit"></param>
        public static void DrawHitPoint(Graphics g, Point2D point, Inclusion hit)
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

            g.DrawLine(pointpen, new PointF((float)point.X, (float)point.Y - pointRadius), new PointF((float)point.X, (float)point.Y + pointRadius));
            g.DrawLine(pointpen, new PointF((float)point.X - pointRadius, (float)point.Y), new PointF((float)point.X + pointRadius, (float)point.Y));
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
        public static List<Point2D> GenerateGrid(int minX, int minY, int maxX, int maxY, int stepX, int stepY)
        {
            int width = stepX == 0 ? (maxX - minX) : (maxX - minX) / stepX;
            int height = stepY == 0 ? (maxY - minY) : (maxY - minY) / stepY;
            return new List<Point2D>(
                from x in Enumerable.Range(0, width)
                from y in Enumerable.Range(0, height)
                select new Point2D(minX + (x * stepX), minY + (y * stepY)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            rectangle = (((string, Rectangle2D))comboBox1.SelectedItem).Item2;
            Invalidate(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
            => DrawRectangles(e.Graphics, Font, rectangle, points,
                Experiments.Contains,
                nameof(Experiments.Contains));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox2_Paint(object sender, PaintEventArgs e)
            => DrawRectangles(e.Graphics, Font, rectangle, points,
                Experiments.Contains2,
                nameof(Experiments.Contains2));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox3_Paint(object sender, PaintEventArgs e)
            => DrawRectangles(e.Graphics, Font, rectangle, points,
                Experiments.PointOnRectangleX,
                nameof(Experiments.PointOnRectangleX));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox4_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox5_Paint(object sender, PaintEventArgs e)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox6_Paint(object sender, PaintEventArgs e)
        { }

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
