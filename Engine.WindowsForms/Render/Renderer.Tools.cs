// <copyright file="Renderer.Tools.cs" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>
// <remarks></remarks>

using System.Drawing;

namespace Engine.Imaging
{
    /// <summary>
    /// The renderer class.
    /// </summary>
    public static partial class Renderer
    {
        /// <summary>
        /// The render.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="g">The g.</param>
        /// <param name="item">The item.</param>
        /// <param name="style">The style.</param>
        public static void Render(this ParametricPointTester shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            const float pointRadius = 1f;

            var results = shape.Interactions();

            var pointpen = Pens.Magenta;
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
        /// The render.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="g">The g.</param>
        /// <param name="item">The item.</param>
        /// <param name="style">The style.</param>
        public static void Render(this ParametricWarpGrid shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            const float pointRadius = 1f;

            var results = shape.Warp();

            var pointpen = Pens.Gold;
            foreach (var point in results)
            {
                g.DrawLine(pointpen, new PointF((float)point.X, (float)point.Y - pointRadius), new PointF((float)point.X, (float)point.Y + pointRadius));
                g.DrawLine(pointpen, new PointF((float)point.X - pointRadius, (float)point.Y), new PointF((float)point.X + pointRadius, (float)point.Y));
            }
        }

        /// <summary>
        /// The render.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="g">The g.</param>
        /// <param name="item">The item.</param>
        /// <param name="style">The style.</param>
        public static void Render(this AngleVisualizerTester shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            var itemStyle = style ?? (ShapeStyle)item?.Style;

            var backBrush = new SolidBrush(Color.FromArgb(128, Color.MediumPurple));
            var forePen = new Pen(Color.FromArgb(128, Color.Purple));

            g.FillPie(backBrush, shape.Bounds.ToRectangle(), (float)shape.StartAngle.ToDegrees(), (float)shape.SweepAngle.ToDegrees());
            g.DrawPie(forePen, shape.Bounds.ToRectangleF(), (float)shape.StartAngle.ToDegrees(), (float)shape.SweepAngle.ToDegrees());

            var num = 1;

            var tickBrush = Pens.Red;
            foreach (var angle in shape.TestAngles)
            {
                tickBrush = shape.InSweep(angle) ? Pens.Lime : Pens.Red;
                g.DrawLine(tickBrush, shape.Location.ToPointF(), shape.TestPoint(angle).ToPointF());
                g.DrawString("a" + num, new Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular), Brushes.Black, shape.TestPoint(angle).ToPointF());
                num++;
            }
        }

        /// <summary>
        /// The render.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="g">The g.</param>
        /// <param name="item">The item.</param>
        /// <param name="style">The style.</param>
        public static void Render(this NodeRevealer shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            var itemStyle = style ?? (ShapeStyle)item?.Style;

            var dashPen = new Pen(Color.DarkGray, 1f) { DashPattern = new float[] { 3f, 3f } };

            if (shape?.Points.Count > 1 && shape.ConnectPoints)
                g.DrawLines(dashPen, shape?.Points.ToPointFArray());

            foreach (var point in shape?.Points)
            {
                var rect = new Rectangle2D(new Point2D(point.X - shape.Radius, point.Y - shape.Radius), new Size2D(2 * shape.Radius, 2 * shape.Radius));
                g.FillEllipse(itemStyle.BackBrush, rect.ToRectangleF());
                g.DrawEllipse(itemStyle.ForePen, rect.ToRectangleF());
            }
        }
    }
}
