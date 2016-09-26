﻿using Engine.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MethodSpeedTester
{
    public partial class PolygonPointTester
        : Form
    {
        private List<PointF> points;
        private List<PointF> polygon;
        private List<(string description, List<PointF> points)> polygons = new List<(string, List<PointF>)>();
        private (List<double>, List<double>) ? PatrickMullenValues;

        public PolygonPointTester()
        {
            InitializeComponent();
        }

        private void PolygonPointTester_Load(object sender, EventArgs e)
        {
            points = GenerateGrid(25, 25, 125, 125, 5, 5);

            polygons.Add(new(string, List<PointF>)("Square", new List<PointF> {
                new PointF(25, 25),
                new PointF(100, 25),
                new PointF(100, 100),
                new PointF(25, 100) }));
            polygons.Add(new(string, List<PointF>)("Top Left Triangle", new List<PointF> {
                new PointF(25, 25),
                new PointF(100, 25),
                new PointF(25, 100) }));
            polygons.Add(new(string, List<PointF>)("Bottom Right Triangle", new List<PointF> {
                new PointF(100, 100),
                new PointF(100, 25),
                new PointF(25, 100) }));
            polygons.Add(new(string, List<PointF>)("Right Reversed Bow-tie", new List<PointF> {
                new PointF(25, 25),
                new PointF(100, 100),
                new PointF(100, 25),
                new PointF(25, 100) }));
            polygons.Add(new(string, List<PointF>)("Left Reversed Bow-tie", new List<PointF> {
                new PointF(100, 25),
                new PointF(100, 100),
                new PointF(25, 25),
                new PointF(25, 100) }));
            polygons.Add(new(string, List<PointF>)("C Shape", new List<PointF> {
                new PointF(25, 25),
                new PointF(100, 25),
                new PointF(100, 50),
                new PointF(50, 50),
                new PointF(50, 75),
                new PointF(100, 75),
                new PointF(100, 100),
                new PointF(25, 100) }));
            polygons.Add(new(string, List<PointF>)("n Shape", new List<PointF> {
                new PointF(25, 25),
                new PointF(100, 25),
                new PointF(100, 100),
                new PointF(75, 100),
                new PointF(75, 50),
                new PointF(50, 50),
                new PointF(50, 100),
                new PointF(25, 100) }));
            polygons.Add(new(string, List<PointF>)("C Bow-tie hole Shape", new List<PointF> {
                new PointF(25, 25),
                new PointF(100, 25),
                new PointF(100, 50),
                new PointF(50, 75),
                new PointF(50, 50),
                new PointF(100, 75),
                new PointF(100, 100),
                new PointF(25, 100) }));
            polygons.Add(new(string, List<PointF>)("n Bow-tie hole Shape", new List<PointF> {
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

        public static void DrawHitPoint(Graphics g, PointF point, Inclusion hit)
        {
            float pointRadius = 1;
            Pen pointpen = hit ? Pens.Lime : Pens.Red;
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
            polygon = (((string, List<PointF>))comboBox1.SelectedItem).Item2;
            PatrickMullenValues = Experiments.PrecalcPointInPolygonPatrickMullenValues(polygon);
            Invalidate(true);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            DrawPolys(e.Graphics, Font, polygon, points,
                Experiments.PointInPolygonAlienRyderFlex,
                nameof(Experiments.PointInPolygonAlienRyderFlex));
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            DrawPolys(e.Graphics, Font, polygon, points,
                Experiments.PointInPolygonDarelRexFinley,
                nameof(Experiments.PointInPolygonDarelRexFinley));
        }

        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            DrawPolys(e.Graphics, Font, polygon, points,
                Experiments.PointInPolygonGilKr,
                nameof(Experiments.PointInPolygonGilKr));
        }

        private void pictureBox4_Paint(object sender, PaintEventArgs e)
        {
            DrawPolys(e.Graphics, Font, polygon, points,
                Experiments.PointInPolygonKeith,
                nameof(Experiments.PointInPolygonKeith));
        }

        private void pictureBox5_Paint(object sender, PaintEventArgs e)
        {
            DrawPolys(e.Graphics, Font, polygon, points,
                Experiments.PointInPolygonLaschaLagidse,
                nameof(Experiments.PointInPolygonLaschaLagidse));
        }

        private void pictureBox6_Paint(object sender, PaintEventArgs e)
        {
            DrawPolys(e.Graphics, Font, polygon, points,
                Experiments.PointInPolygonLaschaLagidse2,
                nameof(Experiments.PointInPolygonLaschaLagidse2));
        }

        private void pictureBox7_Paint(object sender, PaintEventArgs e)
        {
            DrawPolys(e.Graphics, Font, polygon, points,
                Experiments.PointInPolygonMeowNET,
                nameof(Experiments.PointInPolygonMeowNET));
        }

        private void pictureBox8_Paint(object sender, PaintEventArgs e)
        {
            DrawPolys(e.Graphics, Font, polygon, points,
                Experiments.PointInPolygonMKatzWRandolphFranklin,
                nameof(Experiments.PointInPolygonMKatzWRandolphFranklin));
        }

        private void pictureBox9_Paint(object sender, PaintEventArgs e)
        {
            DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonNathanMercer(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonNathanMercer));
        }

        private void pictureBox10_Paint(object sender, PaintEventArgs e)
        {
            DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonPatrickMullen(s, p, PatrickMullenValues?.Item1, PatrickMullenValues?.Item2) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonPatrickMullen));
        }

        private void pictureBox12_Paint(object sender, PaintEventArgs e)
        {
            DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonRodStephens(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonRodStephens));
        }

        private void pictureBox11_Paint(object sender, PaintEventArgs e)
        {
            DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonSaeedAmiri(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonSaeedAmiri));
        }

        private void pictureBox13_Paint(object sender, PaintEventArgs e)
        {
            DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonJerryKnauss(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonJerryKnauss));
        }

        private void pictureBox14_Paint(object sender, PaintEventArgs e)
        {
            DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonJerryKnauss2(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonJerryKnauss2));
        }

        private void pictureBox15_Paint(object sender, PaintEventArgs e)
        {
            DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonPaulBourke(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonPaulBourke));
        }

        private void pictureBox19_Paint(object sender, PaintEventArgs e)
        {
            DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonWRandolphFranklin(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonWRandolphFranklin));
        }

        private void pictureBox20_Paint(object sender, PaintEventArgs e)
        {
            DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonPhilippeReverdy(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonPhilippeReverdy));
        }

        private void pictureBox18_Paint(object sender, PaintEventArgs e)
        {
            DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonBobStein(s, p) == true ? Inclusion.Inside : Inclusion.Outside,
                nameof(Experiments.PointInPolygonBobStein));
        }

        private void pictureBox16_Paint(object sender, PaintEventArgs e)
        {
            DrawPolys(e.Graphics, Font, polygon, points,
                Experiments.PointInPolygonHormannAgathos,
                nameof(Experiments.PointInPolygonHormannAgathos));
        }

        private void pictureBox17_Paint(object sender, PaintEventArgs e)
        {
            DrawPolys(e.Graphics, Font, polygon, points,
                (s, p) => Experiments.PointInPolygonHormannAgathosX(s, p),
                nameof(Experiments.PointInPolygonHormannAgathosX));
        }
    }
}
