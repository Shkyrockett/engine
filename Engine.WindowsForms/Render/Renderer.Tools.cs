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

using Engine.Colorspace;
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
        /// <param name="renderer">The renderer.</param>
        /// <param name="item">The item.</param>
        /// <param name="style">The style.</param>
        public static void Render(this ParametricPointTester shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle? style = null)
        {
            const float pointRadius = 1f;

            var results = shape.Interactions();

            var pointpen = new Stroke(new SolidFill(Colorspace.Colors.Magenta));
            foreach (var point in results.Item1)
            {
                renderer.DrawLine(pointpen, point.X, point.Y - pointRadius, point.X, point.Y + pointRadius);
                renderer.DrawLine(pointpen, point.X - pointRadius, point.Y, point.X + pointRadius, point.Y);
            }

            pointpen = new Stroke(new SolidFill(Colorspace.Colors.Lime));
            foreach (var point in results.Item2)
            {
                renderer.DrawLine(pointpen, point.X, point.Y - pointRadius, point.X, point.Y + pointRadius);
                renderer.DrawLine(pointpen, point.X - pointRadius, point.Y, point.X + pointRadius, point.Y);
            }

            pointpen = new Stroke(new SolidFill(Colorspace.Colors.Red));
            foreach (var point in results.Item3)
            {
                renderer.DrawLine(pointpen, point.X, point.Y - pointRadius, point.X, point.Y + pointRadius);
                renderer.DrawLine(pointpen, point.X - pointRadius, point.Y, point.X + pointRadius, point.Y);
            }
        }

        /// <summary>
        /// The render.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="g">The g.</param>
        /// <param name="renderer">The renderer.</param>
        /// <param name="item">The item.</param>
        /// <param name="style">The style.</param>
        public static void Render(this ParametricWarpGrid shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle? style = null)
        {
            const float pointRadius = 1f;

            var results = shape.Warp();

            var pointpen = new Stroke(new SolidFill(Colorspace.Colors.Gold));
            foreach (var point in results)
            {
                renderer.DrawLine(pointpen, point.X, point.Y - pointRadius, point.X, point.Y + pointRadius);
                renderer.DrawLine(pointpen, point.X - pointRadius, point.Y, point.X + pointRadius, point.Y);
            }
        }

        /// <summary>
        /// The render.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="g">The g.</param>
        /// <param name="renderer">The renderer.</param>
        /// <param name="item">The item.</param>
        /// <param name="style">The style.</param>
        public static void Render(this AngleVisualizerTester shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle? style = null)
        {
            var itemStyle = style ?? (ShapeStyle)item?.Style;

            var fill = new SolidFill(RGBA.FromRGBA(Colors.MediumPurple, 128));
            var stroke = new Stroke(new SolidFill(RGBA.FromRGBA(Colors.Purple, 128)));

            Rectangle2D bounds = shape.Bounds!!;
            renderer.FillPie(fill, bounds.X, bounds.Y, bounds.Width, bounds.Height, shape.StartAngle, shape.SweepAngle);
            renderer.DrawPie(stroke, bounds.X, bounds.Y, bounds.Width, bounds.Height, shape.StartAngle, shape.SweepAngle);

            var num = 1;

            var tickStroke = SolidStrokes.Red;
            foreach (var angle in shape.TestAngles)
            {
                tickStroke = shape.InSweep(angle) ? SolidStrokes.Lime : SolidStrokes.Red;
                Point2D anglePoint = shape.TestPoint(angle);
                renderer.DrawLine(tickStroke, shape.Location.X, shape.Location.Y, anglePoint.X, anglePoint.Y);
                renderer.DrawString($"a{num}", new RenderFont("GenericSansSerif", 12, Engine.TextStyle.Regular), SolidFills.Black, anglePoint.X, anglePoint.Y, new TextFormat(TextBoxFormatFlags.NoWrap, 0));
                num++;
            }
        }

        /// <summary>
        /// The render.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="g">The g.</param>
        /// <param name="renderer">The renderer.</param>
        /// <param name="item">The item.</param>
        /// <param name="style">The style.</param>
        public static void Render(this NodeRevealer shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle? style = null)
        {
            var itemStyle = style ?? (ShapeStyle)item?.Style;

            var dashPen = new Stroke(SolidFills.DarkGray) { DashStyle = LineDashStyle.Dash, DashPattern = new float[] { 3f, 3f } };

            if (shape?.Points.Count > 1 && shape.ConnectPoints)
            {
                renderer.DrawLines(dashPen, shape?.Points);
            }

            foreach (var point in shape?.Points)
            {
                var rect = new Rectangle2D(new Point2D(point.X - shape.Radius, point.Y - shape.Radius), new Size2D(2 * shape.Radius, 2 * shape.Radius));
                renderer.FillEllipse(itemStyle.Fill, rect.X, rect.Y, rect.Width, rect.Height);
                renderer.DrawEllipse(itemStyle.Stroke, rect.X, rect.Y, rect.Width, rect.Height);
            }
        }
    }
}
