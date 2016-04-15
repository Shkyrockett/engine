using Engine.Geometry;
using System;
using System.Drawing;

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
        public static void Render(Shape shape, Graphics g, ShapeStyle style)
        {
            g.DrawRectangles(Pens.Lime, new RectangleF[] { shape.Bounds.ToRectangleF() });

            if (shape == null)
            {
                throw new NullReferenceException("shape is null.");
            }
            else if (shape is LineSegment) // Line segment needs to be in front of Polyline.
            {
                ((LineSegment)shape).Render(g, style);
            }
            else if (shape is Polyline)
            {
                ((Polyline)shape).Render(g, style);
            }
            else if (shape is Polygon)
            {
                ((Polygon)shape).Render(g, style);
            }
            else if (shape is Rectangle2D)
            {
                ((Rectangle2D)shape).Render(g, style);
            }
            else if (shape is Circle)
            {
                ((Circle)shape).Render(g, style);
            }
            else if (shape is Ellipse)
            {
                ((Ellipse)shape).Render(g, style);
            }
            else if (shape is CubicBezier)
            {
                ((CubicBezier)shape).Render(g, style);
            }
            else if (shape is QuadraticBezier)
            {
                ((QuadraticBezier)shape).Render(g, style);
            }
            else
            {
                shape.Render(g);
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
        /// <param name="g"></param>
        /// <param name="style"></param>
        /// <param name="shape"></param>
        public static void Render(this Polygon shape, Graphics g, ShapeStyle style)
        {
            g.FillPolygon(style.BackBrush, shape.Points.ToPointFArray());
            g.DrawPolygon(style.ForePen, shape.Points.ToPointFArray());
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
            if (shape.Points == null || shape.Points.Count <= 0) shape.Points = shape.InterpolatePoints((int)shape.Length());
            if (shape.Points != null && shape.Points.Count > 1)
            {
                g.FillPolygon(style.BackBrush, shape.Points.ToPointFArray());
                g.DrawBezier(style.ForePen, shape.A.ToPointF(), shape.B.ToPointF(), shape.C.ToPointF(), shape.D.ToPointF());
            }
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
            if (shape.Points == null || shape.Points.Count <= 0) shape.Points = shape.InterpolatePoints((int)shape.Length());
            if (shape.Points != null && shape.Points.Count > 1)
            {
                g.FillPolygon(style.BackBrush, shape.Points.ToPointFArray());
                g.DrawCurve(style.ForePen, shape.Points.ToPointFArray());
            }
        }
    }
}
