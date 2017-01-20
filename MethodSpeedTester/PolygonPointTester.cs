// <copyright file="PolygonPointTester.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using Engine;
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
    public partial class PolygonPointTester
        : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private List<PointF> points;

        /// <summary>
        /// 
        /// </summary>
        private List<PointF> polygon;

        /// <summary>
        /// 
        /// </summary>
        private List<(string description, List<PointF> points)> polygons = new List<(string, List<PointF>)>();

        /// <summary>
        /// 
        /// </summary>
        private (List<double>, List<double>) ? PatrickMullenValues;

        /// <summary>
        /// 
        /// </summary>
        public PolygonPointTester()
            => InitializeComponent();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PolygonPointTester_Load(object sender, EventArgs e)
        {
            points = GenerateGrid(25, 25, 125, 125, 5, 5);

            polygons.Add(("Square", new List<PointF> {
                new PointF(25, 25),
                new PointF(100, 25),
                new PointF(100, 100),
                new PointF(25, 100) }));
            polygons.Add(("Top Left Triangle", new List<PointF> {
                new PointF(25, 25),
                new PointF(100, 25),
                new PointF(25, 100) }));
            polygons.Add(("Bottom Right Triangle", new List<PointF> {
                new PointF(100, 100),
                new PointF(100, 25),
                new PointF(25, 100) }));
            polygons.Add(("Right Reversed Bow-tie", new List<PointF> {
                new PointF(25, 25),
                new PointF(100, 100),
                new PointF(100, 25),
                new PointF(25, 100) }));
            polygons.Add(("Left Reversed Bow-tie", new List<PointF> {
                new PointF(100, 25),
                new PointF(100, 100),
                new PointF(25, 25),
                new PointF(25, 100) }));
            polygons.Add(("C Shape", new List<PointF> {
                new PointF(25, 25),
                new PointF(100, 25),
                new PointF(100, 50),
                new PointF(50, 50),
                new PointF(50, 75),
                new PointF(100, 75),
                new PointF(100, 100),
                new PointF(25, 100) }));
            polygons.Add(("n Shape", new List<PointF> {
                new PointF(25, 25),
                new PointF(100, 25),
                new PointF(100, 100),
                new PointF(75, 100),
                new PointF(75, 50),
                new PointF(50, 50),
                new PointF(50, 100),
                new PointF(25, 100) }));
            polygons.Add(("C Bow-tie hole Shape", new List<PointF> {
                new PointF(25, 25),
                new PointF(100, 25),
                new PointF(100, 50),
                new PointF(50, 75),
                new PointF(50, 50),
                new PointF(100, 75),
                new PointF(100, 100),
                new PointF(25, 100) }));
            polygons.Add(("n Bow-tie hole Shape", new List<PointF> {
                new PointF(25, 25),
                new PointF(100, 25),
                new PointF(100, 100),
                new PointF(75, 100),
                new PointF(50, 50),
                new PointF(75, 50),
                new PointF(50, 100),
                new PointF(25, 100) }));

            polygon = polygons[0].points;
            PatrickMullenValues = Experiments.PrecalcPointInPolygonPatrickMullenValues(polygon);

            comboBox1.DataSource = polygons;
            //comboBox1.ValueMember = "Item1";
            comboBox1.SelectedIndex = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="font"></param>
        /// <param name="polygon"></param>
        /// <param name="points"></param>
        /// <param name="method"></param>
        /// <param name="text"></param>
        public static void DrawPolys(Graphics g, Font font,
            List<PointF> polygon,
            List<PointF> points,
            Func<List<PointF>, PointF, Inclusion> method,
            string text)
        {
            PointF[] poly = polygon.ToArray();
            g.DrawString(text, font, Brushes.Black, new Point());
            g.FillPolygon(Brushes.White, poly);
            g.DrawPolygon(Pens.Black, poly);
            foreach (PointF point in points)
                DrawHitPoint(g, point, method.Invoke(polygon, point));
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
            polygon = (((string, List<PointF>))comboBox1.SelectedItem).Item2;
            PatrickMullenValues = Experiments.PrecalcPointInPolygonPatrickMullenValues(polygon);
            Invalidate(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox1_Paint(object sender, PaintEventArgs e) => DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonAlienRyderFlex(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonAlienRyderFlex));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox2_Paint(object sender, PaintEventArgs e) => DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonDarelRexFinley(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonDarelRexFinley));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox3_Paint(object sender, PaintEventArgs e) => DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonGilKr(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonGilKr));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox4_Paint(object sender, PaintEventArgs e) => DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonKeith(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonKeith));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox5_Paint(object sender, PaintEventArgs e) => DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonLaschaLagidse(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonLaschaLagidse));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox6_Paint(object sender, PaintEventArgs e) => DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonLaschaLagidse2(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonLaschaLagidse2));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox7_Paint(object sender, PaintEventArgs e) => DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonMeowNET(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonMeowNET));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox8_Paint(object sender, PaintEventArgs e) => DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonMKatzWRandolphFranklin(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonMKatzWRandolphFranklin));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox9_Paint(object sender, PaintEventArgs e) => DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonNathanMercer(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonNathanMercer));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox10_Paint(object sender, PaintEventArgs e) => DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonPatrickMullen(s, p, PatrickMullenValues?.Item1, PatrickMullenValues?.Item2) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonPatrickMullen));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox12_Paint(object sender, PaintEventArgs e) => DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonRodStephens(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonRodStephens));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox11_Paint(object sender, PaintEventArgs e) => DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonSaeedAmiri(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonSaeedAmiri));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox13_Paint(object sender, PaintEventArgs e) => DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonJerryKnauss(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonJerryKnauss));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox14_Paint(object sender, PaintEventArgs e) => DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonJerryKnauss2(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonJerryKnauss2));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox15_Paint(object sender, PaintEventArgs e) => DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonPaulBourke(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonPaulBourke));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox19_Paint(object sender, PaintEventArgs e) => DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonWRandolphFranklin(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonWRandolphFranklin));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox20_Paint(object sender, PaintEventArgs e) => DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonPhilippeReverdy(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonPhilippeReverdy));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox18_Paint(object sender, PaintEventArgs e) => DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonBobStein(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonBobStein));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox16_Paint(object sender, PaintEventArgs e) => DrawPolys(e.Graphics, Font, polygon, points,
                Experiments.PointInPolygonHormannAgathos,
                nameof(Experiments.PointInPolygonHormannAgathos));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox17_Paint(object sender, PaintEventArgs e) => DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonHormannAgathosX(s, p),
                nameof(Experiments.PointInPolygonHormannAgathosX));
    }
}
