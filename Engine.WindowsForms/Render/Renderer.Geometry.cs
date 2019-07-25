// <copyright file="Renderer.Geometry.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Engine.Imaging
{
    /// <summary>
    /// The renderer class.
    /// </summary>
    public static partial class Renderer
    {
        //public static void Render(this GraphicItem item, Graphics g)
        //{
        //    if (g.VisibleClipBounds.ToRectangle2D().Contains(item.Shape.Bounds))
        //        switch (item?.Shape)
        //        {
        //            case ParametricDelegateCurve s:
        //                Render(item, g, (ParametricDelegateCurve)item.Shape);
        //                break;
        //            default:
        //                break;
        //        }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="item"></param>
        ///// <param name="g"></param>
        ///// <param name="shape"></param>
        //public static void Render<T>(this GraphicItem item, T shape, Graphics g)
        //    where T : ParametricDelegateCurve
        //{
        //    var itemStyle = item.Style as ShapeStyle ?? default;
        //    var points = item.Shape.InterpolatePoints(100);
        //    g.FillPolygon((itemStyle).BackBrush, points?.ToPointFArray());
        //    g.DrawPolygon((itemStyle).ForePen, points?.ToPointFArray());
        //}

        /// <summary>
        /// The render.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="g">The g.</param>
        /// <param name="renderer">The renderer.</param>
        /// <param name="item">The item.</param>
        /// <param name="style">The style.</param>
        public static void Render(this ParametricDelegateCurve shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item.Style!!;
            var points = shape.InterpolatePoints(100);
            if (!(itemStyle is null) && !(points is null))
            {
                renderer.FillPolygon(itemStyle.Fill, points);
                renderer.DrawPolygon(itemStyle.Stroke, points);
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
        public static void Render(this ScreenPoint shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item.Style!!;
            if (!(itemStyle is null) && !(shape is null))
            {
                renderer.DrawRectangle(itemStyle.Stroke, shape.X, shape.Y, 1d, 1d);
            }
        }

        /// <summary>
        /// The render.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="g">The g.</param>
        /// <param name="renderer">The renderer.</param>
        /// <param name="item">The item.</param>
        /// <param name="bounds">The bounds.</param>
        /// <param name="style">The style.</param>
        public static void Render(this Ray shape, Graphics g, IRenderer renderer, GraphicItem item, Rectangle2D bounds, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item.Style!!;

            var p1 = shape.Location;
            //var p2 = shape.Location + shape.Direction;
            var intersection = Intersections.Intersection(shape, bounds);

            if (!(itemStyle is null) && !(shape is null))
            {
                if (intersection.Count == 1)
                {
                    renderer.DrawLine(itemStyle.Stroke, p1.X, p1.Y, intersection.Points[0].X, intersection.Points[0].Y);
                }
                if (intersection.Count == 2)
                {
                    renderer.DrawLine(itemStyle.Stroke, intersection.Points[0].X, intersection.Points[0].Y, intersection.Points[1].X, intersection.Points[1].Y);
                }
            }
        }

        /// <summary>
        /// The render.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="g">The g.</param>
        /// <param name="renderer">The renderer.</param>
        /// <param name="item">The item.</param>
        /// <param name="bounds">The bounds.</param>
        /// <param name="style">The style.</param>
        public static void Render(this Line shape, Graphics g, IRenderer renderer, GraphicItem item, Rectangle2D bounds, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item.Style!!;
            var intersection = Intersections.Intersection(shape, bounds);
            if (!(itemStyle is null) && !(shape is null))
            {
                if (intersection.Count == 2)
                {
                    renderer.DrawLine(itemStyle.Stroke, intersection.Points[0].X, intersection.Points[0].Y, intersection.Points[1].X, intersection.Points[1].Y);
                }
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
        public static void Render(this LineSegment shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item.Style!!;
            if (!(itemStyle is null) && !(shape is null))
            {
                renderer.DrawLine(itemStyle.Stroke, shape.AX, shape.AY, shape.BX, shape.BY);
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
        public static void Render(this PolygonContour shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item.Style!!;
            if (!(itemStyle is null) && !(shape is null))
            {
                renderer.FillPolygon(itemStyle.Fill, shape.Points);
                renderer.DrawPolygon(itemStyle.Stroke, shape.Points);
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
        public static void Render(this Polyline shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item.Style!!;
            if (!(itemStyle is null) && !(shape is null))
            {
                renderer.FillPolygon(itemStyle.Fill, shape.Points);
                renderer.DrawLines(itemStyle.Stroke, shape.Points);
            }
        }

        /// <summary>
        /// The render.
        /// </summary>
        /// <param name="set">The set.</param>
        /// <param name="g">The g.</param>
        /// <param name="renderer">The renderer.</param>
        /// <param name="item">The item.</param>
        /// <param name="style">The style.</param>
        public static void Render(this PolylineSet set, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item.Style!!;
            if (!(itemStyle is null) && !(set.Polylines is null))
            {
                foreach (var shape in set.Polylines)
                {
                    renderer.FillPolygon(itemStyle.Fill, shape.Points);
                    renderer.DrawLines(itemStyle.Stroke, shape.Points);
                }
            }
        }

        /// <summary>
        /// The render.
        /// </summary>
        /// <param name="set">The set.</param>
        /// <param name="g">The g.</param>
        /// <param name="item">The item.</param>
        /// <param name="style">The style.</param>
        public static void Render(this Polygon set, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            var itemStyle = style ?? (ShapeStyle)item.Style;
            // Start the Path object.
            using var path = new GraphicsPath();
            foreach (var shape in set.Contours)
            {
                path.AddPolygon(shape.Points.ToPointFArray());
            }

            g.FillPath(itemStyle.BackBrush, path);
            g.DrawPath(itemStyle.ForePen, path);
        }

        /// <summary>
        /// The render.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="g">The g.</param>
        /// <param name="item">The item.</param>
        /// <param name="style">The style.</param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        public static void Render(this PolycurveContour shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            var itemStyle = style ?? (ShapeStyle)item.Style;
            // Start the Path object.
            using var path = new GraphicsPath();
            foreach (var figureItem in shape.Items)
            {
                switch (figureItem)
                {
                    case PointSegment t:
                        path.StartFigure();
                        path.AddLine(t.Start.Value.ToPointF(), t.End.Value.ToPointF());
                        break;
                    case LineCurveSegment t:
                        path.AddLine(t.Start.Value.ToPointF(), t.End.Value.ToPointF());
                        break;
                    case ArcSegment t:
                        var arc = t.ToEllipticalArc();
                        using (var mat = new Matrix())
                        {
                            mat.RotateAt(-(float)arc.Angle.ToDegrees(), arc.Center.ToPointF());
                            path.Transform(mat);
                            path.AddArc(arc.DrawingBounds.ToRectangleF(), (float)arc.StartAngle.ToDegrees(), (float)arc.SweepAngle.ToDegrees());
                            mat.RotateAt(2 * (float)arc.Angle.ToDegrees(), arc.Center.ToPointF());
                            path.Transform(mat);
                        }
                        break;
                    case CubicBezierSegment t:
                        path.AddBezier(t.Start.Value.ToPointF(), t.Handle1.ToPointF(), t.Handle2.Value.ToPointF(), t.End.Value.ToPointF());
                        break;
                    case QuadraticBezierSegment t:
                        path.AddBeziers(new PointF[] { t.Start.Value.ToPointF(), t.Handle.Value.ToPointF(), t.End.Value.ToPointF() });
                        break;
                    case CardinalSegment t:
                        path.AddCurve(t.Nodes.ToPointFArray());
                        break;
                    case null:
                        throw new NullReferenceException($"{nameof(figureItem)} is null. Geometry to render is missing.");
                    default:
                        throw new InvalidCastException($"Unknown {nameof(figureItem)}.");
                }
            }

            // Close the path.
            if (shape.Closed)
            {
                path.CloseFigure();
            }

            //  Draw the path.
            g.FillPath(itemStyle.BackBrush, path);
            g.DrawPath(itemStyle.ForePen, path);
        }

        /// <summary>
        /// The render.
        /// </summary>
        /// <param name="set">The set.</param>
        /// <param name="g">The g.</param>
        /// <param name="item">The item.</param>
        /// <param name="style">The style.</param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        public static void Render(this Polycurve set, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            var itemStyle = style ?? (ShapeStyle)item.Style;
            // Start the Path object.
            using var path = new GraphicsPath();
            foreach (var shape in set.Contours)
            {
                foreach (var figureItem in shape.Items)
                {
                    switch (figureItem)
                    {
                        case PointSegment t:
                            path.StartFigure();
                            path.AddLine(t.Start.Value.ToPointF(), t.End.Value.ToPointF());
                            break;
                        case LineCurveSegment t:
                            path.AddLine(t.Start.Value.ToPointF(), t.End.Value.ToPointF());
                            break;
                        case ArcSegment t:
                            var arc = t.ToEllipticalArc();
                            using (var mat = new Matrix())
                            {
                                mat.RotateAt(-(float)arc.Angle.ToDegrees(), arc.Center.ToPointF());
                                path.Transform(mat);
                                path.AddArc(arc.DrawingBounds.ToRectangleF(), (float)arc.StartAngle.ToDegrees(), (float)arc.SweepAngle.ToDegrees());
                                mat.RotateAt(2 * (float)arc.Angle.ToDegrees(), arc.Center.ToPointF());
                                path.Transform(mat);
                            }
                            break;
                        case CubicBezierSegment t:
                            path.AddBezier(t.Start.Value.ToPointF(), t.Handle1.ToPointF(), t.Handle2.Value.ToPointF(), t.End.Value.ToPointF());
                            break;
                        case QuadraticBezierSegment t:
                            path.AddBeziers(new PointF[] { t.Start.Value.ToPointF(), t.Handle.Value.ToPointF(), t.End.Value.ToPointF() });
                            break;
                        case CardinalSegment t:
                            path.AddCurve(t.Nodes.ToPointFArray());
                            break;
                        case null:
                            throw new NullReferenceException($"{nameof(figureItem)} is null. Geometry to render is missing.");
                        default:
                            throw new InvalidCastException($"Unknown {nameof(figureItem)}.");
                    }
                }

                // Close the path.
                if (shape.Closed)
                {
                    path.CloseFigure();
                }
            }

            //  Draw the path.
            g.FillPath(itemStyle.BackBrush, path);
            g.DrawPath(itemStyle.ForePen, path);
        }

        /// <summary>
        /// The render.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="g">The g.</param>
        /// <param name="renderer">The renderer.</param>
        /// <param name="item">The item.</param>
        /// <param name="style">The style.</param>
        public static void Render(this Rectangle2D shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item.Style!!;
            if (!(itemStyle is null) && !(shape is null))
            {
                renderer.FillRectangle(itemStyle.Fill, shape.X, shape.Y, shape.Width, shape.Height);
                renderer.DrawRectangle(itemStyle.Stroke, shape.X, shape.Y, shape.Width, shape.Height);
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
        public static void Render(this Circle shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item.Style!!;
            var bounds = shape.Bounds;
            if (!(itemStyle is null) && !(bounds is null))
            {
                renderer.FillEllipse(itemStyle.Fill, bounds.X, bounds.Y, bounds.Width, bounds.Height);
                renderer.DrawEllipse(itemStyle.Stroke, bounds.X, bounds.Y, bounds.Width, bounds.Height);
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
        public static void Render(this CircularArc shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item.Style!!;
            var bounds = shape.Bounds!!;
            if (!(itemStyle is null) && !(shape is null) && !(bounds is null))
            {
                renderer.FillArc(itemStyle.Fill, bounds.X, bounds.Y, bounds.Width, bounds.Height, shape.StartAngle, shape.SweepAngle);
                renderer.DrawArc(itemStyle.Stroke, bounds.X, bounds.Y, bounds.Width, bounds.Height, shape.StartAngle, shape.SweepAngle);
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
        public static void Render(this Ellipse shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item.Style!!;
            var bounds = shape.UnrotatedBounds!!;
            if (!(itemStyle is null) && !(shape is null) && !(bounds is null))
            {
                renderer.FillEllipse(itemStyle.Fill, bounds.X, bounds.Y, bounds.Width, bounds.Height, shape.Angle);
                renderer.DrawEllipse(itemStyle.Stroke, bounds.X, bounds.Y, bounds.Width, bounds.Height, shape.Angle);
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
        public static void Render(this EllipticalArc shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item.Style!!;
            var bounds = shape.DrawingBounds!!;
            if (!(itemStyle is null) && !(shape is null) && !(bounds is null))
            {
                renderer.FillArc(itemStyle.Fill, bounds.X, bounds.Y, bounds.Width, bounds.Height, (float)shape.StartAngle, (float)shape.SweepAngle, shape.Angle);
                renderer.DrawArc(itemStyle.Stroke, bounds.X, bounds.Y, bounds.Width, bounds.Height, (float)shape.StartAngle, (float)shape.SweepAngle, shape.Angle);
            }
        }

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="shape"></param>
        ///// <param name="g"></param>
        ///// <param name="item"></param>
        ///// <param name="style"></param>
        //public static void Render(this Oval shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        //{
        //    ShapeStyle itemStyle = style ?? (ShapeStyle)item.Style;
        //    // Determine the orientation.
        //    var radius = (shape.Size.Height > shape.Size.Width) ? shape.Size.Width / 2 : shape.Size.Height / 2;

        //    // Start the Path object.
        //    var path = new GraphicsPath();

        //    //  prepare the curves.
        //    path.AddArc((float)(shape.Location.X + (shape.Size.Width - (radius * 2))), (float)shape.Location.Y, (float)(radius * 2), (float)(radius * 2), 270, 90);
        //    path.AddArc((float)(shape.Location.X + (shape.Size.Width - (radius * 2))), (float)(shape.Location.Y + (shape.Size.Height - (radius * 2))), (float)(radius * 2), (float)(radius * 2), 0, 90);
        //    path.AddArc((float)shape.Location.X, (float)(shape.Location.Y + (shape.Size.Height - (radius * 2))), (float)(radius * 2), (float)(radius * 2), 90, 90);
        //    path.AddArc((float)shape.Location.X, (float)shape.Location.Y, (float)(radius * 2), (float)(radius * 2), 180, 90);

        //    // Close the path.
        //    path.CloseFigure();

        //    //  Draw the path.
        //    g.FillPath(itemStyle.BackBrush, path);
        //    g.DrawPath(itemStyle.ForePen, path);
        //}

        /// <summary>
        /// The render.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="g">The g.</param>
        /// <param name="renderer">The renderer.</param>
        /// <param name="item">The item.</param>
        /// <param name="style">The style.</param>
        public static void Render(this BezierSegment shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item.Style!!;
            if (!(itemStyle is null) && !(shape is null))
            {
                switch (shape.Degree)
                {
                    case PolynomialDegree.Linear:
                        renderer.DrawLine(itemStyle.Stroke, shape.Points[0].X, shape.Points[0].Y, shape.Points[1].X, shape.Points[1].Y);
                        break;
                    case PolynomialDegree.Quadratic:
                        renderer.FillQuadraticBezier(itemStyle.Fill, shape.Points[0].X, shape.Points[0].Y, shape.Points[1].X, shape.Points[1].Y, shape.Points[2].X, shape.Points[2].Y);
                        renderer.DrawQuadraticBezier(itemStyle.Stroke, shape.Points[0].X, shape.Points[0].Y, shape.Points[1].X, shape.Points[1].Y, shape.Points[2].X, shape.Points[2].Y);
                        break;
                    case PolynomialDegree.Cubic:
                        renderer.FillCubicBezier(itemStyle.Fill, shape.Points[0].X, shape.Points[0].Y, shape.Points[1].X, shape.Points[1].Y, shape.Points[2].X, shape.Points[2].Y, shape.Points[3].X, shape.Points[3].Y);
                        renderer.DrawCubicBezier(itemStyle.Stroke, shape.Points[0].X, shape.Points[0].Y, shape.Points[1].X, shape.Points[1].Y, shape.Points[2].X, shape.Points[2].Y, shape.Points[3].X, shape.Points[3].Y);
                        break;
                    default:
                        break;
                }
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
        public static void Render(this CubicBezier shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item.Style!!;
            if (!(itemStyle is null) && !(shape is null))
            {
                renderer.FillCubicBezier(itemStyle.Fill, shape.AX, shape.AY, shape.BX, shape.BY, shape.CX, shape.CY, shape.DX, shape.DY);
                renderer.DrawCubicBezier(itemStyle.Stroke, shape.AX, shape.AY, shape.BX, shape.BY, shape.CX, shape.CY, shape.DX, shape.DY);
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
        public static void Render(this QuadraticBezier shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item.Style!!;
            if (!(itemStyle is null) && !(shape is null))
            {
                renderer.FillQuadraticBezier(itemStyle.Fill, shape.AX, shape.AY, shape.BX, shape.BY, shape.CX, shape.CY);
                renderer.DrawQuadraticBezier(itemStyle.Stroke, shape.AX, shape.AY, shape.BX, shape.BY, shape.CX, shape.CY);
            }
        }
    }
}
