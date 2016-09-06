﻿using Engine.Geometry;
using Engine.Objects;
using System.Collections.Generic;
using System.Drawing;

namespace Engine.Imaging
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class Renderer
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="style"></param>
        public static void Render(this ParametricPointTester shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            float pointRadius = 1;

            (List<Point2D>, List<Point2D>, List<Point2D>) results = shape.Interactions();

            Pen pointpen = Pens.Magenta;
            foreach (var point in results.Item1)
            {
                g.DrawLine(pointpen, new PointF((float)point.X, (float)point.Y - pointRadius), new PointF((float)point.X, (float)point.Y + pointRadius));
                g.DrawLine(pointpen, new PointF((float)point.X - pointRadius, (float)point.Y), new PointF((float)point.X + pointRadius, (float)point.Y));
            }

            pointpen = Pens.Lime;
            foreach (var point in results.Item2)
            {
                g.DrawLine(pointpen, new PointF((float)point.X, (float)point.Y - pointRadius), new PointF((float)point.X, (float)point.Y + pointRadius));
                g.DrawLine(pointpen, new PointF((float)point.X - pointRadius, (float)point.Y), new PointF((float)point.X + pointRadius, (float)point.Y));
            }

            pointpen = Pens.Red;
            foreach (var point in results.Item3)
            {
                g.DrawLine(pointpen, new PointF((float)point.X, (float)point.Y - pointRadius), new PointF((float)point.X, (float)point.Y + pointRadius));
                g.DrawLine(pointpen, new PointF((float)point.X - pointRadius, (float)point.Y), new PointF((float)point.X + pointRadius, (float)point.Y));
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="style"></param>
        public static void Render(this AngleVisualizerTester shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            ShapeStyle itemStyle = style ?? (ShapeStyle)item.Style;

            Brush backBrush = new SolidBrush(Color.FromArgb(128, Color.MediumPurple));
            Pen forePen = new Pen(Color.FromArgb(128, Color.Purple));

            g.FillPie(backBrush, shape.Bounds.ToRectangle(), (float)shape.StartAngle.ToDegrees(), (float)shape.SweepAngle.ToDegrees());
            g.DrawPie(forePen, shape.Bounds.ToRectangleF(), (float)shape.StartAngle.ToDegrees(), (float)(shape.SweepAngle.ToDegrees()));

            int num = 1;

            Pen tickBrush = Pens.Red;
            foreach (var angle in shape.TestAngles)
            {
                if (shape.InSweep(angle))
                    tickBrush = Pens.Lime;
                else
                    tickBrush = Pens.Red;
                g.DrawLine(tickBrush, shape.Location.ToPointF(), shape.TestPoint(angle).ToPointF());
                g.DrawString("a" + num, new Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular), Brushes.Black, shape.TestPoint(angle).ToPointF());
                num++;
            }
        }
    }
}