// <copyright file="Renderer.cs" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using Engine.Geometry;
using Engine.Objects;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Engine.Imaging
{
    /// <summary>
    /// 
    /// </summary>
    public static class Renderer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="style"></param>
        /// <param name="shape"></param>
        public static void Render(GraphicsObject shape, Graphics g, IStyle style)
        {
            //g.DrawRectangles(Pens.Lime, new RectangleF[] { shape.Bounds.ToRectangleF() });

            //// Waiting on c# 7... https://channel9.msdn.com/Events/Build/2016/B889
            //switch (shape)
            //{
            //    case LineSegment s:
            //        ((LineSegment)shape).Render(g, style);
            //        break;
            //    case null:
            //    default:
            //        break;
            //}

            if (shape == null)
            {
                throw new NullReferenceException("shape is null.");
            }
            else if (shape is LineSegment) // Line segment needs to be in front of Polyline because LineSegment is a subset of Polyline.
            {
                ((LineSegment)shape).Render(g, (ShapeStyle)style);
            }
            else if (shape is PolylineSet)
            {
                ((PolylineSet)shape).Render(g, (ShapeStyle)style);
            }
            else if (shape is Polyline)
            {
                ((Polyline)shape).Render(g, (ShapeStyle)style);
            }
            else if (shape is Polygon)
            {
                ((Polygon)shape).Render(g, (ShapeStyle)style);
            }
            else if (shape is PolygonSet)
            {
                ((PolygonSet)shape).Render(g, (ShapeStyle)style);
            }
            else if (shape is Rectangle2D)
            {
                ((Rectangle2D)shape).Render(g, (ShapeStyle)style);
            }
            else if (shape is Arc)
            {
                ((Arc)shape).Render(g, (ShapeStyle)style);
            }
            else if (shape is Circle)
            {
                ((Circle)shape).Render(g, (ShapeStyle)style);
            }
            else if (shape is Ellipse)
            {
                ((Ellipse)shape).Render(g, (ShapeStyle)style);
            }
            else if (shape is CubicBezier)
            {
                ((CubicBezier)shape).Render(g, (ShapeStyle)style);
            }
            else if (shape is QuadraticBezier)
            {
                ((QuadraticBezier)shape).Render(g, (ShapeStyle)style);
            }
            else
            {
                //shape.Render(g);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="style"></param>
        /// <param name="shape"></param>
        public static void Render(this LineSegment shape, Graphics g, ShapeStyle style)
        {
            g.DrawLine(style.ForePen, shape.A.ToPointF(), shape.B.ToPointF());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="g"></param>
        /// <param name="style"></param>
        public static void Render(this Polygon shape, Graphics g, ShapeStyle style)
        {
            g.FillPolygon(style.BackBrush, shape.Points.ToPointFArray());
            g.DrawPolygon(style.ForePen, shape.Points.ToPointFArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="set"></param>
        /// <param name="g"></param>
        /// <param name="style"></param>
        public static void Render(this PolylineSet set, Graphics g, ShapeStyle style)
        {
            //GraphicsPath path = new GraphicsPath();
            foreach (Polyline shape in set.Polylines)
            {
                g.FillPolygon(style.BackBrush, shape.Points.ToPointFArray());
                g.DrawLines(style.ForePen, shape.Points.ToPointFArray());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="set"></param>
        /// <param name="g"></param>
        /// <param name="style"></param>
        public static void Render(this PolygonSet set, Graphics g, ShapeStyle style)
        {
            GraphicsPath path = new GraphicsPath();
            foreach (Polygon shape in set.Polygons)
            {
                path.AddPolygon(shape.Points.ToPointFArray());
            }

            g.FillPath(style.BackBrush, path);
            g.DrawPath(style.ForePen, path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="style"></param>
        /// <param name="shape"></param>
        public static void Render(this Polyline shape, Graphics g, ShapeStyle style)
        {
            g.FillPolygon(style.BackBrush, shape.Points.ToPointFArray());
            g.DrawLines(style.ForePen, shape.Points.ToPointFArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="style"></param>
        /// <param name="shape"></param>
        public static void Render(this Rectangle2D shape, Graphics g, ShapeStyle style)
        {
            g.FillRectangles(style.BackBrush, new RectangleF[] { shape.Bounds.ToRectangleF() });
            g.DrawRectangles(style.ForePen, new RectangleF[] { shape.Bounds.ToRectangleF() });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="style"></param>
        /// <param name="shape"></param>
        public static void Render(this Arc shape, Graphics g, ShapeStyle style)
        {
            g.DrawArc(style.ForePen, shape.DrawingBounds.ToRectangleF(), -(float)shape.StartAngle.ToDegrees(), (float)(shape.SweepAngle.ToDegrees()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="style"></param>
        /// <param name="shape"></param>
        public static void Render(this Circle shape, Graphics g, ShapeStyle style)
        {
            g.FillEllipse(style.BackBrush, shape.Bounds.ToRectangleF());
            g.DrawEllipse(style.ForePen, shape.Bounds.ToRectangleF());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="style"></param>
        /// <param name="shape"></param>
        public static void Render(this Ellipse shape, Graphics g, ShapeStyle style)
        {
            if (shape.Points == null || shape.Points.Count <= 0) shape.Points = shape.InterpolatePoints();
            if (shape.Points != null && shape.Points.Count > 1)
            {
                g.FillPolygon(style.BackBrush, shape.Points.ToPointFArray());
                g.DrawPolygon(style.ForePen, shape.Points.ToPointFArray());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="style"></param>
        /// <param name="shape"></param>
        public static void Render(this CubicBezier shape, Graphics g, ShapeStyle style)
        {
            //if (shape.Points == null || shape.Points.Count <= 0) shape.Points = shape.InterpolatePoints((int)shape.Length);
            //if (shape.Points != null && shape.Points.Count > 1)
            //{
            //    g.FillPolygon(style.BackBrush, shape.Points.ToPointFArray());
            g.DrawBezier(style.ForePen, shape.A.ToPointF(), shape.B.ToPointF(), shape.C.ToPointF(), shape.D.ToPointF());
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="style"></param>
        /// <param name="shape"></param>
        public static void Render(this QuadraticBezier shape, Graphics g, ShapeStyle style)
        {
            // ToDo: Need to update point list when the nodes are moved.
            if (shape.Points == null || shape.Points.Count <= 0) shape.Points = shape.InterpolatePoints((int)shape.Length);
            if (shape.Points != null && shape.Points.Count > 1)
            {
                //g.FillPolygon(style.BackBrush, shape.Points.ToPointFArray());
                g.DrawCurve(style.ForePen, shape.Points.ToPointFArray());
            }
        }
    }
}
