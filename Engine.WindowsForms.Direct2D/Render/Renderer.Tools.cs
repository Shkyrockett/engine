// <copyright file="Renderer.Tools.cs" >
// Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine.Imaging;

/// <summary>
/// The renderer class.
/// </summary>
internal static partial class Renderer
{
    ///// <summary>
    /////
    ///// </summary>
    ///// <param name="shape"></param>
    ///// <param name="g"></param>
    ///// <param name="item"></param>
    ///// <param name="style"></param>
    //public static void Render(this ParametricPointTester shape, Graphics g, GraphicItem item, ShapeStyle style = null)
    //{
    //    float pointRadius = 1;

    //    (List<Point2D>, List<Point2D>, List<Point2D>) results = shape.Interactions();

    //    Pen pointpen = Pens.Magenta;
    //    foreach (var point in results.Item1)
    //    {
    //        g.DrawLine(pointpen, new PointF((float)point.X, (float)point.Y - pointRadius), new PointF((float)point.X, (float)point.Y + pointRadius));
    //        g.DrawLine(pointpen, new PointF((float)point.X - pointRadius, (float)point.Y), new PointF((float)point.X + pointRadius, (float)point.Y));
    //    }

    //    pointpen = Pens.Lime;
    //    foreach (var point in results.Item2)
    //    {
    //        g.DrawLine(pointpen, new PointF((float)point.X, (float)point.Y - pointRadius), new PointF((float)point.X, (float)point.Y + pointRadius));
    //        g.DrawLine(pointpen, new PointF((float)point.X - pointRadius, (float)point.Y), new PointF((float)point.X + pointRadius, (float)point.Y));
    //    }

    //    pointpen = Pens.Red;
    //    foreach (var point in results.Item3)
    //    {
    //        g.DrawLine(pointpen, new PointF((float)point.X, (float)point.Y - pointRadius), new PointF((float)point.X, (float)point.Y + pointRadius));
    //        g.DrawLine(pointpen, new PointF((float)point.X - pointRadius, (float)point.Y), new PointF((float)point.X + pointRadius, (float)point.Y));
    //    }
    //}

    ///// <summary>
    /////
    ///// </summary>
    ///// <param name="shape"></param>
    ///// <param name="g"></param>
    ///// <param name="item"></param>
    ///// <param name="style"></param>
    //public static void Render(this AngleVisualizerTester shape, Graphics g, GraphicItem item, ShapeStyle style = null)
    //{
    //    ShapeStyle itemStyle = style ?? (ShapeStyle)item?.Style;

    //    Brush backBrush = new SolidBrush(Color.FromArgb(128, Color.MediumPurple));
    //    var forePen = new Pen(Color.FromArgb(128, Color.Purple));

    //    g.FillPie(backBrush, shape.Bounds.ToRectangle(), (float)shape.StartAngle.ToDegrees(), (float)shape.SweepAngle.ToDegrees());
    //    g.DrawPie(forePen, shape.Bounds.ToRectangleF(), (float)shape.StartAngle.ToDegrees(), (float)(shape.SweepAngle.ToDegrees()));

    //    var num = 1;

    //    Pen tickBrush = Pens.Red;
    //    foreach (var angle in shape.TestAngles)
    //    {
    //        if (shape.InSweep(angle))
    //            tickBrush = Pens.Lime;
    //        else
    //            tickBrush = Pens.Red;
    //        g.DrawLine(tickBrush, shape.Location.ToPointF(), shape.TestPoint(angle).ToPointF());
    //        g.DrawString("a" + num, new Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular), Brushes.Black, shape.TestPoint(angle).ToPointF());
    //        num++;
    //    }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="shape"></param>
    ///// <param name="g"></param>
    ///// <param name="item"></param>
    ///// <param name="style"></param>
    //public static void Render(this NodeRevealer shape, Graphics g, GraphicItem item, ShapeStyle style = null)
    //{
    //    ShapeStyle itemStyle = style ?? (ShapeStyle)item?.Style;

    //    var dashPen = new Pen(Color.DarkGray, 1f) { DashPattern = new float[] { 3f, 3f } };

    //    if (shape?.Points.Count > 1 && shape.ConnectPoints)
    //        g.DrawLines(dashPen, shape?.Points.ToPointFArray());

    //    foreach (var point in shape?.Points)
    //    {
    //        var rect = new Rectangle2D(new Point2D(point.X - shape.Radius, point.Y - shape.Radius), new Size2D(2 * shape.Radius, 2 * shape.Radius));
    //        g.FillEllipse(itemStyle.BackBrush, rect.ToRectangleF());
    //        g.DrawEllipse(itemStyle.ForePen, rect.ToRectangleF());
    //    }
    //}
}
