// <copyright file="Renderer.Geometry.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

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
        public static void Render(this ParametricDelegateCurve shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            ShapeStyle itemStyle = style ?? (ShapeStyle)item.Style;
            List<Point2D> points = item?.InterpolatePoints();
            g.FillPolygon((itemStyle).BackBrush, points?.ToPointFArray());
            g.DrawPolygon((itemStyle).ForePen, points?.ToPointFArray());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="shape"></param>
        /// <param name="style"></param>
        public static void Render(this ScreenPoint shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            ShapeStyle itemStyle = style ?? (ShapeStyle)item.Style;
            g.FillRectangle(itemStyle.ForeBrush, (float)shape.X, (float)shape.Y, 1, 1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="shape"></param>
        /// <param name="style"></param>
        public static void Render(this LineSegment shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            ShapeStyle itemStyle = style ?? (ShapeStyle)item.Style;
            g.DrawLine(itemStyle.ForePen, shape.A.ToPointF(), shape.B.ToPointF());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="style"></param>
        public static void Render(this Polygon shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            ShapeStyle itemStyle = style ?? (ShapeStyle)item.Style;
            g.FillPolygon((itemStyle).BackBrush, shape.Points.ToPointFArray());
            g.DrawPolygon((itemStyle).ForePen, shape.Points.ToPointFArray());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="shape"></param>
        /// <param name="style"></param>
        public static void Render(this Polyline shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            ShapeStyle itemStyle = style ?? (ShapeStyle)item.Style;
            g.FillPolygon((itemStyle).BackBrush, shape.Points.ToPointFArray());
            g.DrawLines((itemStyle).ForePen, shape.Points.ToPointFArray());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="set"></param>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="style"></param>
        public static void Render(this PolylineSet set, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            ShapeStyle itemStyle = style ?? (ShapeStyle)item.Style;
            foreach (Polyline shape in set.Polylines)
            {
                g.FillPolygon((itemStyle).BackBrush, shape.Points.ToPointFArray());
                g.DrawLines((itemStyle).ForePen, shape.Points.ToPointFArray());
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="set"></param>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="style"></param>
        public static void Render(this PolygonSet set, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            ShapeStyle itemStyle = style ?? (ShapeStyle)item.Style;
            // Start the Path object.
            var path = new GraphicsPath();
            foreach (Polygon shape in set.Polygons)
                path.AddPolygon(shape.Points.ToPointFArray());

            g.FillPath((itemStyle).BackBrush, path);
            g.DrawPath((itemStyle).ForePen, path);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="shape"></param>
        /// <param name="style"></param>
        public static void Render(this Rectangle2D shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            ShapeStyle itemStyle = style ?? (ShapeStyle)item.Style;
            g.FillRectangles((itemStyle).BackBrush, new RectangleF[] { shape.Bounds.ToRectangleF() });
            g.DrawRectangles((itemStyle).ForePen, new RectangleF[] { shape.Bounds.ToRectangleF() });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="shape"></param>
        /// <param name="style"></param>
        public static void Render(this Circle shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            ShapeStyle itemStyle = style ?? (ShapeStyle)item.Style;
            g.FillEllipse((itemStyle).BackBrush, shape.Bounds.ToRectangleF());
            g.DrawEllipse((itemStyle).ForePen, shape.Bounds.ToRectangleF());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="shape"></param>
        /// <param name="style"></param>
        public static void Render(this CircularArc shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            ShapeStyle itemStyle = style ?? (ShapeStyle)item.Style;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(shape.DrawingBounds.ToRectangleF(), (float)(shape.StartAngle.ToDegrees()), (float)shape.SweepAngle.ToDegrees());
            g.FillPath((itemStyle).BackBrush, path);
            g.DrawArc((itemStyle).ForePen, shape.DrawingBounds.ToRectangleF(), (float)shape.StartAngle.ToDegrees(), (float)(shape.SweepAngle.ToDegrees()));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="shape"></param>
        /// <param name="style"></param>
        public static void Render(this Ellipse shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            ShapeStyle itemStyle = style ?? (ShapeStyle)item.Style;
            var mat = new Matrix();
            mat.RotateAt((float)shape.Angle.ToDegrees(), shape.Center.ToPointF());
            g.Transform = mat;
            g.FillEllipse((itemStyle).BackBrush, shape.UnrotatedBounds.ToRectangleF());
            g.DrawEllipse((itemStyle).ForePen, shape.UnrotatedBounds.ToRectangleF());
            g.ResetTransform();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="shape"></param>
        /// <param name="style"></param>
        public static void Render(this EllipticalArc shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            ShapeStyle itemStyle = style ?? (ShapeStyle)item.Style;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(shape.DrawingBounds.ToRectangleF(), (float)(shape.StartAngle.ToDegrees()), (float)shape.SweepAngle.ToDegrees());
            var mat = new Matrix();
            mat.RotateAt((float)shape.Angle.ToDegrees(), shape.Center.ToPointF());
            g.Transform = mat;
            g.FillPath((itemStyle).BackBrush, path);
            g.DrawArc((itemStyle).ForePen, shape.DrawingBounds.ToRectangleF(), (float)(shape.StartAngle.ToDegrees()), (float)shape.SweepAngle.ToDegrees());
            g.ResetTransform();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="style"></param>
        public static void Render(this Oval shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            ShapeStyle itemStyle = style ?? (ShapeStyle)item.Style;
            // Determine the orientation.
            double radius = (shape.Size.Height > shape.Size.Width) ? shape.Size.Width / 2 : shape.Size.Height / 2;

            // Start the Path object.
            var path = new GraphicsPath();

            //  prepare the curves.
            path.AddArc((float)(shape.Location.X + (shape.Size.Width - (radius * 2))), (float)shape.Location.Y, (float)(radius * 2), (float)(radius * 2), 270, 90);
            path.AddArc((float)(shape.Location.X + (shape.Size.Width - (radius * 2))), (float)(shape.Location.Y + (shape.Size.Height - (radius * 2))), (float)(radius * 2), (float)(radius * 2), 0, 90);
            path.AddArc((float)shape.Location.X, (float)(shape.Location.Y + (shape.Size.Height - (radius * 2))), (float)(radius * 2), (float)(radius * 2), 90, 90);
            path.AddArc((float)shape.Location.X, (float)shape.Location.Y, (float)(radius * 2), (float)(radius * 2), 180, 90);

            // Close the path.
            path.CloseFigure();

            //  Draw the path.
            g.FillPath(itemStyle.BackBrush, path);
            g.DrawPath(itemStyle.ForePen, path);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="shape"></param>
        /// <param name="style"></param>
        public static void Render(this BezierSegment shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            ShapeStyle itemStyle = style ?? (ShapeStyle)item.Style;
            GraphicsPath path = new GraphicsPath();

            switch (shape.Points.Length)
            {
                case 2:
                    path.AddLine(shape.Points[0].ToPointF(), shape.Points[1].ToPointF());
                    break;
                case 3:
                    Point2D[] cubic = Interpolaters.QuadraticBezierToCubicBezier(shape.Points[0], shape.Points[1], shape.Points[2]);
                    path.AddBezier(cubic[0].ToPointF(), cubic[1].ToPointF(), cubic[2].ToPointF(), cubic[3].ToPointF());
                    break;
                case 4:
                    path.AddBezier(shape.Points[0].ToPointF(), shape.Points[1].ToPointF(), shape.Points[2].ToPointF(), shape.Points[3].ToPointF());
                    break;
                default:
                    break;
            }

            g.FillPath((itemStyle).BackBrush, path);
            g.DrawPath((itemStyle).ForePen, path);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="shape"></param>
        /// <param name="style"></param>
        public static void Render(this CubicBezier shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            ShapeStyle itemStyle = style ?? (ShapeStyle)item.Style;
            GraphicsPath path = new GraphicsPath();
            path.AddBezier(shape.A.ToPointF(), shape.B.ToPointF(), shape.C.ToPointF(), shape.D.ToPointF());
            g.FillPath((itemStyle).BackBrush, path);
            g.DrawBezier((itemStyle).ForePen, shape.A.ToPointF(), shape.B.ToPointF(), shape.C.ToPointF(), shape.D.ToPointF());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="shape"></param>
        /// <param name="style"></param>
        public static void Render(this QuadraticBezier shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            ShapeStyle itemStyle = style ?? (ShapeStyle)item.Style;
            GraphicsPath path = new GraphicsPath();

            Point2D[] cubic = Interpolaters.QuadraticBezierToCubicBezier(shape.A, shape.B, shape.C);
            path.AddBezier(cubic[0].ToPointF(), cubic[1].ToPointF(), cubic[2].ToPointF(), cubic[3].ToPointF());

            g.FillPath((itemStyle).BackBrush, path);
            g.DrawBezier((itemStyle).ForePen, cubic[0].ToPointF(), cubic[1].ToPointF(), cubic[2].ToPointF(), cubic[3].ToPointF());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="shape"></param>
        /// <param name="style"></param>
        public static void Render(this GeometryPath shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            ShapeStyle itemStyle = style ?? (ShapeStyle)item.Style;
            // Start the Path object.
            var path = new GraphicsPath();
            foreach (var figureItem in shape.Items)
            {
                switch (figureItem)
                {
                    case PathPoint t:
                        path.StartFigure();
                        path.AddLine(t.Start.Value.ToPointF(), t.End.Value.ToPointF());
                        break;
                    case PathLineSegment t:
                        path.AddLine(t.Start.Value.ToPointF(), t.End.Value.ToPointF());
                        break;
                    case PathArc t:
                        var arc = t.ToEllipticalArc();
                        var mat = new Matrix();
                        mat.RotateAt(-(float)arc.Angle.ToDegrees(), arc.Center.ToPointF());
                        path.Transform(mat);
                        path.AddArc(arc.DrawingBounds.ToRectangleF(), (float)(arc.StartAngle.ToDegrees()), (float)arc.SweepAngle.ToDegrees());
                        mat.RotateAt(2 * (float)arc.Angle.ToDegrees(), arc.Center.ToPointF());
                        path.Transform(mat);
                        break;
                    case PathCubicBezier t:
                        path.AddBezier(t.Start.Value.ToPointF(), t.Handle1.ToPointF(), t.Handle2.Value.ToPointF(), t.End.Value.ToPointF());
                        break;
                    case PathQuadraticBezier t:
                        path.AddBeziers(new PointF[] { t.Start.Value.ToPointF(), t.Handle.Value.ToPointF(), t.End.Value.ToPointF() });
                        break;
                    case PathCardinal t:
                        path.AddCurve(t.Nodes.ToPointFArray());
                        break;
                    case null:
                        throw new NullReferenceException($"{nameof(figureItem)} is null. Geometry to render is missing.");
                    default:
                        throw new InvalidCastException($"Unknown {nameof(figureItem)}.");
                }
            }

            // Close the path.
            if (shape.Closed) path.CloseFigure();

            //  Draw the path.
            g.FillPath(itemStyle.BackBrush, path);
            g.DrawPath(itemStyle.ForePen, path);
        }
    }
}
