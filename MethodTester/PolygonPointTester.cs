// <copyright file="PolygonPointTester.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MethodSpeedTester
{
    /// <summary>
    /// The polygon point tester class.
    /// </summary>
    public partial class PolygonPointTester
        : Form
    {
        ///// <summary>
        ///// The points.
        ///// </summary>
        //private List<PointF> points;

        ///// <summary>
        ///// The polygon.
        ///// </summary>
        //private List<PointF> polygon;

        ///// <summary>
        ///// The polygons.
        ///// </summary>
        //private List<(string description, List<PointF> points)> polygons = new List<(string, List<PointF>)>();

        ///// <summary>
        ///// The patrick mullen values.
        ///// </summary>
        //private (List<double>, List<double>) ? PatrickMullenValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonPointTester"/> class.
        /// </summary>
        public PolygonPointTester()
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
            //points = GenerateGrid(25, 25, 125, 125, 5, 5);

            //polygons.Add(("Square", new List<PointF> {
            //    new PointF(25, 25),
            //    new PointF(100, 25),
            //    new PointF(100, 100),
            //    new PointF(25, 100) }));
            //polygons.Add(("Top Left Triangle2D", new List<PointF> {
            //    new PointF(25, 25),
            //    new PointF(100, 25),
            //    new PointF(25, 100) }));
            //polygons.Add(("Bottom Right Triangle2D", new List<PointF> {
            //    new PointF(100, 100),
            //    new PointF(100, 25),
            //    new PointF(25, 100) }));
            //polygons.Add(("Right Reversed Bow-tie", new List<PointF> {
            //    new PointF(25, 25),
            //    new PointF(100, 100),
            //    new PointF(100, 25),
            //    new PointF(25, 100) }));
            //polygons.Add(("Left Reversed Bow-tie", new List<PointF> {
            //    new PointF(100, 25),
            //    new PointF(100, 100),
            //    new PointF(25, 25),
            //    new PointF(25, 100) }));
            //polygons.Add(("C Shape2D", new List<PointF> {
            //    new PointF(25, 25),
            //    new PointF(100, 25),
            //    new PointF(100, 50),
            //    new PointF(50, 50),
            //    new PointF(50, 75),
            //    new PointF(100, 75),
            //    new PointF(100, 100),
            //    new PointF(25, 100) }));
            //polygons.Add(("n Shape2D", new List<PointF> {
            //    new PointF(25, 25),
            //    new PointF(100, 25),
            //    new PointF(100, 100),
            //    new PointF(75, 100),
            //    new PointF(75, 50),
            //    new PointF(50, 50),
            //    new PointF(50, 100),
            //    new PointF(25, 100) }));
            //polygons.Add(("C Bow-tie hole Shape2D", new List<PointF> {
            //    new PointF(25, 25),
            //    new PointF(100, 25),
            //    new PointF(100, 50),
            //    new PointF(50, 75),
            //    new PointF(50, 50),
            //    new PointF(100, 75),
            //    new PointF(100, 100),
            //    new PointF(25, 100) }));
            //polygons.Add(("n Bow-tie hole Shape2D", new List<PointF> {
            //    new PointF(25, 25),
            //    new PointF(100, 25),
            //    new PointF(100, 100),
            //    new PointF(75, 100),
            //    new PointF(50, 50),
            //    new PointF(75, 50),
            //    new PointF(50, 100),
            //    new PointF(25, 100) }));

            //polygon = polygons[0].points;
            ////PatrickMullenValues = Experiments.PrecalcPointInPolygonContourPatrickMullenValues(polygon);

            //comboBox1.DataSource = polygons;
            //comboBox1.ValueMember = "Item1";
            //comboBox1.SelectedIndex = 0;
        }

        /// <summary>
        /// The draw polys.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="font">The font.</param>
        /// <param name="polygon">The polygon.</param>
        /// <param name="points">The points.</param>
        /// <param name="method">The method.</param>
        /// <param name="text">The text.</param>
        public static void DrawPolys(Graphics g, Font font,
            List<PointF> polygon,
            List<PointF> points,
            Func<List<PointF>, PointF, Inclusions> method,
            string text)
        {
            if ((polygon is not null) && (points is not null) && (method is not null))
            {
                var poly = polygon.ToArray();
                g?.DrawString(text, font, Brushes.Black, new Point());
                g.FillPolygon(Brushes.White, poly);
                g.DrawPolygon(Pens.Black, poly);
                foreach (var point in points)
                {
                    DrawHitPoint(g, point, method.Invoke(polygon, point));
                }
            }
        }

        /// <summary>
        /// The draw hit point.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="point">The point.</param>
        /// <param name="hit">The hit.</param>
        public static void DrawHitPoint(Graphics g, PointF point, Inclusions hit)
        {
            const float pointRadius = 1;
            var pointpen = Pens.Red;
            switch (hit)
            {
                case Inclusions.Inside:
                    pointpen = Pens.Lime;
                    break;
                case Inclusions.Boundary:
                    pointpen = Pens.Magenta;
                    break;
            }

            g?.DrawLine(pointpen, new PointF(point.X, point.Y - pointRadius), new PointF(point.X, point.Y + pointRadius));
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
        /// <returns>The <see cref="List{T}"/>.</returns>
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
            //polygon = (((string, List<PointF>))comboBox1.SelectedItem).Item2;
            //PatrickMullenValues = Experiments.PrecalcPointInPolygonContourPatrickMullenValues(polygon);
            //Invalidate(true);
        }

        /// <summary>
        /// The picture box1 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //DrawPolys(e.Graphics, Font, polygon, points,
            //(s, p) => Experiments.PointInPolygonContourAlienRyderFlex(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
            //nameof(Experiments.PointInPolygonContourAlienRyderFlex));
        }

        /// <summary>
        /// The picture box2 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox2_Paint(object sender, PaintEventArgs e)
        {
            //DrawPolys(e.Graphics, Font, polygon, points,
            //(s, p) => Experiments.PointInPolygonContourDarelRexFinley(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
            //nameof(Experiments.PointInPolygonContourDarelRexFinley));
        }

        /// <summary>
        /// The picture box3 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox3_Paint(object sender, PaintEventArgs e)
        {
            //DrawPolys(e.Graphics, Font, polygon, points,
            //(s, p) => Experiments.PointInPolygonContourGilKr(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
            //nameof(Experiments.PointInPolygonContourGilKr));
        }

        /// <summary>
        /// The picture box4 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox4_Paint(object sender, PaintEventArgs e)
        {
            //DrawPolys(e.Graphics, Font, polygon, points,
            //(s, p) => Experiments.PointInPolygonContourKeith(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
            //nameof(Experiments.PointInPolygonContourKeith));
        }

        /// <summary>
        /// The picture box5 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox5_Paint(object sender, PaintEventArgs e)
        {
            //DrawPolys(e.Graphics, Font, polygon, points,
            //(s, p) => Experiments.PointInPolygonContourLaschaLagidse(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
            //nameof(Experiments.PointInPolygonContourLaschaLagidse));
        }

        /// <summary>
        /// The picture box6 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox6_Paint(object sender, PaintEventArgs e)
        {
            //DrawPolys(e.Graphics, Font, polygon, points,
            //(s, p) => Experiments.PointInPolygonContourLaschaLagidse2(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
            //nameof(Experiments.PointInPolygonContourLaschaLagidse2));
        }

        /// <summary>
        /// The picture box7 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox7_Paint(object sender, PaintEventArgs e)
        {
            //DrawPolys(e.Graphics, Font, polygon, points,
            //(s, p) => Experiments.PointInPolygonContourMeowNET(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
            //nameof(Experiments.PointInPolygonContourMeowNET));
        }

        /// <summary>
        /// The picture box8 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox8_Paint(object sender, PaintEventArgs e)
        {
            //DrawPolys(e.Graphics, Font, polygon, points,
            //(s, p) => Experiments.PointInPolygonContourMKatzWRandolphFranklin(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
            //nameof(Experiments.PointInPolygonContourMKatzWRandolphFranklin));
        }

        /// <summary>
        /// The picture box9 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox9_Paint(object sender, PaintEventArgs e)
        {
            //DrawPolys(e.Graphics, Font, polygon, points,
            //(s, p) => Experiments.PointInPolygonContourNathanMercer(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
            //nameof(Experiments.PointInPolygonContourNathanMercer));
        }

        /// <summary>
        /// The picture box10 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox10_Paint(object sender, PaintEventArgs e)
        {
            //DrawPolys(e.Graphics, Font, polygon, points,
            //(s, p) => Experiments.PointInPolygonContourPatrickMullen(s, p, PatrickMullenValues?.Item1, PatrickMullenValues?.Item2) == true ? Inclusion.Inside : Inclusion.Outside,
            //nameof(Experiments.PointInPolygonContourPatrickMullen));
        }

        /// <summary>
        /// The picture box12 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox12_Paint(object sender, PaintEventArgs e)
        {
            //DrawPolys(e.Graphics, Font, polygon, points,
            //(s, p) => Experiments.PointInPolygonContourRodStephens(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
            //nameof(Experiments.PointInPolygonContourRodStephens));
        }

        /// <summary>
        /// The picture box11 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox11_Paint(object sender, PaintEventArgs e)
        {
            //DrawPolys(e.Graphics, Font, polygon, points,
            //(s, p) => Experiments.PointInPolygonContourSaeedAmiri(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
            //nameof(Experiments.PointInPolygonContourSaeedAmiri));
        }

        /// <summary>
        /// The picture box13 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox13_Paint(object sender, PaintEventArgs e)
        {
            //DrawPolys(e.Graphics, Font, polygon, points,
            //(s, p) => Experiments.PointInPolygonContourJerryKnauss(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
            //nameof(Experiments.PointInPolygonContourJerryKnauss));
        }

        /// <summary>
        /// The picture box14 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox14_Paint(object sender, PaintEventArgs e)
        {
            //DrawPolys(e.Graphics, Font, polygon, points,
            //(s, p) => Experiments.PointInPolygonContourJerryKnauss2(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
            //nameof(Experiments.PointInPolygonContourJerryKnauss2));
        }

        /// <summary>
        /// The picture box15 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox15_Paint(object sender, PaintEventArgs e)
        {
            //DrawPolys(e.Graphics, Font, polygon, points,
            //(s, p) => Experiments.PointInPolygonContourPaulBourke(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
            //nameof(Experiments.PointInPolygonContourPaulBourke));
        }

        /// <summary>
        /// The picture box19 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox19_Paint(object sender, PaintEventArgs e)
        {
            //DrawPolys(e.Graphics, Font, polygon, points,
            //(s, p) => Experiments.PointInPolygonContourWRandolphFranklin(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
            //nameof(Experiments.PointInPolygonContourWRandolphFranklin));
        }

        /// <summary>
        /// The picture box20 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox20_Paint(object sender, PaintEventArgs e)
        {
            //DrawPolys(e.Graphics, Font, polygon, points,
            //(s, p) => Experiments.PointInPolygonContourPhilippeReverdy(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
            //nameof(Experiments.PointInPolygonContourPhilippeReverdy));
        }

        /// <summary>
        /// The picture box18 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox18_Paint(object sender, PaintEventArgs e)
        {
            //DrawPolys(e.Graphics, Font, polygon, points,
            //(s, p) => Experiments.PointInPolygonContourBobStein(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
            //nameof(Experiments.PointInPolygonContourBobStein));
        }

        /// <summary>
        /// The picture box16 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox16_Paint(object sender, PaintEventArgs e)
        {
            //DrawPolys(e.Graphics, Font, polygon, points,
            //Experiments.PointInPolygonContourHormannAgathosExpanded,
            //nameof(Experiments.PointInPolygonContourHormannAgathosExpanded));
        }

        /// <summary>
        /// The picture box17 paint.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The paint event arguments.</param>
        private void PictureBox17_Paint(object sender, PaintEventArgs e)
        {
            //DrawPolys(e.Graphics, Font, polygon, points,
            //(s, p) => Experiments.PointInPolygonContourHormannAgathosSimplified(s, p),
            //nameof(Experiments.PointInPolygonContourHormannAgathosSimplified));
        }
    }
}
