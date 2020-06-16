// <copyright file="Renderer.Geometry.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
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
        //    if (g.VisibleClipBounds.ToRectangle2D().Contains(item.Shape2D.Bounds))
        //        switch (item?.Shape2D)
        //        {
        //            case ParametricDelegateCurve2D s:
        //                Render(item, g, (ParametricDelegateCurve2D)item.Shape2D);
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
        //    where T : ParametricDelegateCurve2D
        //{
        //    var itemStyle = item.Style as ShapeStyle ?? default;
        //    var points = item.Shape2D.InterpolatePoints(100);
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
        public static void Render(this ParametricDelegateCurve2D shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item?.Style;
            var points = shape?.InterpolatePoints(100);
            if ((itemStyle is not null) && (points is not null))
            {
                renderer?.FillPolygon(itemStyle.Fill, points);
                renderer?.DrawPolygon(itemStyle.Stroke, points);
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
        public static void Render(this ScreenPoint2D shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item?.Style;
            if ((itemStyle is not null) && (shape is not null))
            {
                renderer?.DrawRectangle(itemStyle.Stroke, shape.X, shape.Y, 1d, 1d);
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
        public static void Render(this Ray2D shape, Graphics g, IRenderer renderer, GraphicItem item, Rectangle2D bounds, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item?.Style;

            var p1 = shape?.Location;
            //var p2 = shape.Location + shape.Direction;
            var intersection = Intersections.Intersection(shape, bounds);

            if ((itemStyle is not null) && (shape is not null))
            {
                if (intersection.Count == 1)
                {
                    renderer?.DrawLine(itemStyle.Stroke, p1.X, p1.Y, intersection.Points[0].X, intersection.Points[0].Y);
                }
                if (intersection.Count == 2)
                {
                    renderer?.DrawLine(itemStyle.Stroke, intersection.Points[0].X, intersection.Points[0].Y, intersection.Points[1].X, intersection.Points[1].Y);
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
        public static void Render(this Line2D shape, Graphics g, IRenderer renderer, GraphicItem item, Rectangle2D bounds, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item?.Style;
            var intersection = Intersections.Intersection(shape, bounds);
            if ((itemStyle is not null) && (shape is not null))
            {
                if (intersection.Count == 2)
                {
                    renderer?.DrawLine(itemStyle.Stroke, intersection.Points[0].X, intersection.Points[0].Y, intersection.Points[1].X, intersection.Points[1].Y);
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
        public static void Render(this LineSegment2D shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item?.Style;
            if ((itemStyle is not null) && (shape is not null))
            {
                renderer?.DrawLine(itemStyle.Stroke, shape.AX, shape.AY, shape.BX, shape.BY);
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
        public static void Render(this PolygonContour2D shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item?.Style;
            if ((itemStyle is not null) && (shape is not null))
            {
                renderer?.FillPolygon(itemStyle.Fill, shape.Points);
                renderer?.DrawPolygon(itemStyle.Stroke, shape.Points);
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
        public static void Render(this Polyline2D shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item?.Style;
            if ((itemStyle is not null) && (shape is not null))
            {
                renderer?.FillPolygon(itemStyle.Fill, shape.Points);
                renderer?.DrawLines(itemStyle.Stroke, shape.Points);
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
        public static void Render(this PolylineSet2D set, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item?.Style;
            if ((itemStyle is not null) && (set?.Polylines is not null))
            {
                foreach (var shape in set.Polylines)
                {
                    renderer?.FillPolygon(itemStyle.Fill, shape.Points);
                    renderer?.DrawLines(itemStyle.Stroke, shape.Points);
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
        public static void Render(this Polygon2D set, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            var itemStyle = style ?? (ShapeStyle)item?.Style;
            // Start the Path object.
            using var path = new GraphicsPath();
            foreach (var shape in set?.Contours)
            {
                path.AddPolygon(shape.Points.ToPointFArray());
            }

            g?.FillPath(itemStyle.BackBrush, path);
            g?.DrawPath(itemStyle.ForePen, path);
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
        public static void Render(this PolycurveContour2D shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            var itemStyle = style ?? (ShapeStyle)item?.Style;
            // Start the Path object.
            using var path = new GraphicsPath();
            foreach (var figureItem in shape?.Items)
            {
                switch (figureItem)
                {
                    case PointSegment2D t:
                        path.StartFigure();
                        path.AddLine(t.Head.ToPointF(), t.Tail.ToPointF());
                        break;
                    case LineCurveSegment2D t:
                        path.AddLine(t.Head.ToPointF(), t.Tail.ToPointF());
                        break;
                    case ArcSegment2D t:
                        var arc = t.ToEllipticalArc();
                        using (var mat = new Matrix())
                        {
                            mat.RotateAt(-(float)arc.Angle.RadiansToDegrees(), arc.Center.ToPointF());
                            path.Transform(mat);
                            path.AddArc(arc.OrthogonalBounds.ToRectangleF(), (float)arc.StartAngle.RadiansToDegrees(), (float)arc.SweepAngle.RadiansToDegrees());
                            mat.RotateAt(2 * (float)arc.Angle.RadiansToDegrees(), arc.Center.ToPointF());
                            path.Transform(mat);
                        }
                        break;
                    case CubicBezierSegment2D t:
                        path.AddBezier(t.Head.ToPointF(), t.Handle1.ToPointF(), t.Handle2.ToPointF(), t.Tail.ToPointF());
                        break;
                    case QuadraticBezierSegment2D t:
                        path.AddBeziers(new PointF[] { t.Head.ToPointF(), t.Handle.ToPointF(), t.Tail.ToPointF() });
                        break;
                    case CardinalSegment2D t:
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
            g?.FillPath(itemStyle.BackBrush, path);
            g?.DrawPath(itemStyle.ForePen, path);
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
        public static void Render(this Polycurve2D set, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            var itemStyle = style ?? (ShapeStyle)item?.Style;
            // Start the Path object.
            using var path = new GraphicsPath();
            foreach (var shape in set?.Contours)
            {
                foreach (var figureItem in shape.Items)
                {
                    switch (figureItem)
                    {
                        case PointSegment2D t:
                            path.StartFigure();
                            path.AddLine(t.Head.ToPointF(), t.Tail.ToPointF());
                            break;
                        case LineCurveSegment2D t:
                            path.AddLine(t.Head.ToPointF(), t.Tail.ToPointF());
                            break;
                        case ArcSegment2D t:
                            var arc = t.ToEllipticalArc();
                            using (var mat = new Matrix())
                            {
                                mat.RotateAt(-(float)arc.Angle.RadiansToDegrees(), arc.Center.ToPointF());
                                path.Transform(mat);
                                path.AddArc(arc.OrthogonalBounds.ToRectangleF(), (float)arc.StartAngle.RadiansToDegrees(), (float)arc.SweepAngle.RadiansToDegrees());
                                mat.RotateAt(2 * (float)arc.Angle.RadiansToDegrees(), arc.Center.ToPointF());
                                path.Transform(mat);
                            }
                            break;
                        case CubicBezierSegment2D t:
                            path.AddBezier(t.Head.ToPointF(), t.Handle1.ToPointF(), t.Handle2.ToPointF(), t.Tail.ToPointF());
                            break;
                        case QuadraticBezierSegment2D t:
                            path.AddBeziers(new PointF[] { t.Head.ToPointF(), t.Handle.ToPointF(), t.Tail.ToPointF() });
                            break;
                        case CardinalSegment2D t:
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
            g?.FillPath(itemStyle.BackBrush, path);
            g?.DrawPath(itemStyle.ForePen, path);
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
            var itemStyle = style ?? (ShapeStyle)item?.Style;
            if ((itemStyle is not null) && (shape is not null))
            {
                renderer?.FillRectangle(itemStyle.Fill, shape.X, shape.Y, shape.Width, shape.Height);
                renderer?.DrawRectangle(itemStyle.Stroke, shape.X, shape.Y, shape.Width, shape.Height);
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
        public static void Render(this Circle2D shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item?.Style;
            var bounds = shape?.Bounds;
            if ((itemStyle is not null) && (bounds is not null))
            {
                renderer?.FillEllipse(itemStyle.Fill, bounds.X, bounds.Y, bounds.Width, bounds.Height);
                renderer?.DrawEllipse(itemStyle.Stroke, bounds.X, bounds.Y, bounds.Width, bounds.Height);
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
        public static void Render(this CircularArc2D shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item?.Style;
            var bounds = shape?.DrawingBounds;
            if ((itemStyle is not null) && (shape is not null) && (bounds is not null))
            {
                renderer?.FillArc(itemStyle.Fill, bounds.X, bounds.Y, bounds.Width, bounds.Height, shape.StartAngle, shape.SweepAngle);
                renderer?.DrawArc(itemStyle.Stroke, bounds.X, bounds.Y, bounds.Width, bounds.Height, shape.StartAngle, shape.SweepAngle);
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
        public static void Render(this Ellipse2D shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item?.Style;
            var bounds = shape?.OrthogonalBounds;
            if ((itemStyle is not null) && (shape is not null) && (bounds is not null))
            {
                renderer?.FillEllipse(itemStyle.Fill, bounds.X, bounds.Y, bounds.Width, bounds.Height, shape.Angle);
                renderer?.DrawEllipse(itemStyle.Stroke, bounds.X, bounds.Y, bounds.Width, bounds.Height, shape.Angle);
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
        public static void Render(this EllipticalArc2D shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item?.Style;
            var bounds = shape?.OrthogonalBounds;
            if ((itemStyle is not null) && (shape is not null) && (bounds is not null))
            {
                renderer?.FillArc(itemStyle.Fill, bounds.X, bounds.Y, bounds.Width, bounds.Height, shape.StartAngle, shape.SweepAngle, shape.Angle);
                renderer?.DrawArc(itemStyle.Stroke, bounds.X, bounds.Y, bounds.Width, bounds.Height, shape.StartAngle, shape.SweepAngle, shape.Angle);
            }
        }

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="shape"></param>
        ///// <param name="g"></param>
        ///// <param name="item"></param>
        ///// <param name="style"></param>
        //public static void Render(this Oval shape, Graphics g, GraphicItem item, ShapeStyle? style = null)
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
        public static void Render(this BezierSegment2D shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item?.Style;
            if ((itemStyle is not null) && (shape is not null))
            {
                switch (shape.Degree)
                {
                    case PolynomialDegree.Linear:
                        renderer?.DrawLine(itemStyle.Stroke, shape.Points[0].X, shape.Points[0].Y, shape.Points[1].X, shape.Points[1].Y);
                        break;
                    case PolynomialDegree.Quadratic:
                        renderer?.FillQuadraticBezier(itemStyle.Fill, shape.Points[0].X, shape.Points[0].Y, shape.Points[1].X, shape.Points[1].Y, shape.Points[2].X, shape.Points[2].Y);
                        renderer?.DrawQuadraticBezier(itemStyle.Stroke, shape.Points[0].X, shape.Points[0].Y, shape.Points[1].X, shape.Points[1].Y, shape.Points[2].X, shape.Points[2].Y);
                        break;
                    case PolynomialDegree.Cubic:
                        renderer?.FillCubicBezier(itemStyle.Fill, shape.Points[0].X, shape.Points[0].Y, shape.Points[1].X, shape.Points[1].Y, shape.Points[2].X, shape.Points[2].Y, shape.Points[3].X, shape.Points[3].Y);
                        renderer?.DrawCubicBezier(itemStyle.Stroke, shape.Points[0].X, shape.Points[0].Y, shape.Points[1].X, shape.Points[1].Y, shape.Points[2].X, shape.Points[2].Y, shape.Points[3].X, shape.Points[3].Y);
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
        public static void Render(this CubicBezier2D shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item?.Style;
            if ((itemStyle is not null) && (shape is not null))
            {
                renderer?.FillCubicBezier(itemStyle.Fill, shape.AX, shape.AY, shape.BX, shape.BY, shape.CX, shape.CY, shape.DX, shape.DY);
                renderer?.DrawCubicBezier(itemStyle.Stroke, shape.AX, shape.AY, shape.BX, shape.BY, shape.CX, shape.CY, shape.DX, shape.DY);
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
        public static void Render(this QuadraticBezier2D shape, Graphics g, IRenderer renderer, GraphicItem item, ShapeStyle style = null)
        {
            _ = g;
            var itemStyle = style ?? (ShapeStyle)item?.Style;
            if ((itemStyle is not null) && (shape is not null))
            {
                renderer?.FillQuadraticBezier(itemStyle.Fill, shape.AX, shape.AY, shape.BX, shape.BY, shape.CX, shape.CY);
                renderer?.DrawQuadraticBezier(itemStyle.Stroke, shape.AX, shape.AY, shape.BX, shape.BY, shape.CX, shape.CY);
            }
        }
    }
}
