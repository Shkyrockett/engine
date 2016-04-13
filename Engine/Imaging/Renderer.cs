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
            else if (shape is Rect)
            {
                ((Rect)shape).Render(g, style);
            }
            else if (shape is RectF)
            {
                ((RectF)shape).Render(g, style);
            }
            else if (shape is Circle)
            {
                ((Circle)shape).Render(g, style);
            }
            else if (shape is Ellipse)
            {
                ((Ellipse)shape).Render(g, style);
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
            g.DrawLine(style.ForePen, shape.A, shape.B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="style"></param>
        /// <param name="shape"></param>
        public static void Render(this Polygon shape, Graphics g, ShapeStyle style)
        {
            g.FillPolygon(style.BackBrush, shape.Points.ToArray());
            g.DrawPolygon(style.ForePen, shape.Points.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="style"></param>
        /// <param name="shape"></param>
        public static void Render(this Polyline shape, Graphics g, ShapeStyle style)
        {
            g.FillPolygon(style.BackBrush, shape.Points.ToArray());
            g.DrawLines(style.ForePen, shape.Points.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="style"></param>
        /// <param name="shape"></param>
        public static void Render(this Rect shape, Graphics g, ShapeStyle style)
        {
            g.FillRectangles(style.BackBrush, new RectangleF[] { shape.Bounds });
            g.DrawRectangles(style.ForePen, new RectangleF[] { shape.Bounds });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="style"></param>
        /// <param name="shape"></param>
        public static void Render(this RectF shape, Graphics g, ShapeStyle style)
        {
            g.FillRectangles(style.BackBrush, new RectangleF[] { shape.Bounds });
            g.DrawRectangles(style.ForePen, new RectangleF[] { shape.Bounds });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="style"></param>
        /// <param name="shape"></param>
        public static void Render(this Circle shape, Graphics g, ShapeStyle style)
        {
            g.FillEllipse(style.BackBrush, shape.Bounds);
            g.DrawEllipse(style.ForePen, shape.Bounds);
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
                g.FillPolygon(style.BackBrush, shape.Points.ToArray());
                g.DrawPolygon(style.ForePen, shape.Points.ToArray());
            }
        }
    }
}
