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
using System.Windows.Forms;
using static System.Math;

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
        /// <param name="item"></param>
        /// <param name="shape"></param>
        public static void Render(GraphicsObject shape, Graphics g, GraphicItem item)
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
                (shape as LineSegment).Render(g, item);
            }
            else if (shape is Polyline)
            {
                (shape as Polyline).Render(g, item);
            }
            else if (shape is PolylineSet)
            {
                (shape as PolylineSet).Render(g, item);
            }
            else if (shape is Polygon)
            {
                (shape as Polygon).Render(g, item);
            }
            else if (shape is PolygonSet)
            {
                (shape as PolygonSet).Render(g, item);
            }
            else if (shape is Oval)
            {
                (shape as Oval).Render(g, item);
            }
            else if (shape is Rectangle2D)
            {
                (shape as Rectangle2D).Render(g, item);
            }
            else if (shape is Arc)
            {
                (shape as Arc).Render(g, item);
            }
            else if (shape is Circle)
            {
                (shape as Circle).Render(g, item);
            }
            else if (shape is Ellipse)
            {
                (shape as Ellipse).Render(g, item);
            }
            else if (shape is CubicBezier)
            {
                (shape as CubicBezier).Render(g, item);
            }
            else if (shape is QuadraticBezier)
            {
                (shape as QuadraticBezier).Render(g, item);
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
        /// <param name="item"></param>
        /// <param name="shape"></param>
        public static void Render(this LineSegment shape, Graphics g, GraphicItem item)
        {
            g.DrawLine(((ShapeStyle)item.Style).ForePen, shape.A.ToPointF(), shape.B.ToPointF());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="g"></param>
        /// <param name="item"></param>
        public static void Render(this Polygon shape, Graphics g, GraphicItem item)
        {
            g.FillPolygon(((ShapeStyle)item.Style).BackBrush, shape.Points.ToPointFArray());
            g.DrawPolygon(((ShapeStyle)item.Style).ForePen, shape.Points.ToPointFArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="set"></param>
        /// <param name="g"></param>
        /// <param name="item"></param>
        public static void Render(this PolylineSet set, Graphics g, GraphicItem item)
        {
            foreach (Polyline shape in set.Polylines)
            {
                g.FillPolygon(((ShapeStyle)item.Style).BackBrush, shape.Points.ToPointFArray());
                g.DrawLines(((ShapeStyle)item.Style).ForePen, shape.Points.ToPointFArray());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="set"></param>
        /// <param name="g"></param>
        /// <param name="item"></param>
        public static void Render(this PolygonSet set, Graphics g, GraphicItem item)
        {
            // Start the Path object.
            GraphicsPath path = new GraphicsPath();
            foreach (Polygon shape in set.Polygons)
            {
                path.AddPolygon(shape.Points.ToPointFArray());
            }

            g.FillPath(((ShapeStyle)item.Style).BackBrush, path);
            g.DrawPath(((ShapeStyle)item.Style).ForePen, path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="g"></param>
        /// <param name="item"></param>
        public static void Render(this Oval shape, Graphics g, GraphicItem item)
        {
            // Determine the orientation.
            double radius = (shape.Size.Height > shape.Size.Width) ? shape.Size.Width / 2 : shape.Size.Height / 2;

            // Start the Path object.
            GraphicsPath path = new GraphicsPath();

            //  prepare the curves.
            path.AddArc((float)(shape.Location.X + (shape.Size.Width - (radius * 2))), (float)shape.Location.Y, (float)(radius * 2), (float)(radius * 2), 270, 90);
            path.AddArc((float)(shape.Location.X + (shape.Size.Width - (radius * 2))), (float)(shape.Location.Y + (shape.Size.Height - (radius * 2))), (float)(radius * 2), (float)(radius * 2), 0, 90);
            path.AddArc((float)shape.Location.X, (float)(shape.Location.Y + (shape.Size.Height - (radius * 2))), (float)(radius * 2), (float)(radius * 2), 90, 90);
            path.AddArc((float)shape.Location.X, (float)shape.Location.Y, (float)(radius * 2), (float)(radius * 2), 180, 90);

            // Close the path.
            path.CloseFigure();

            //  Draw the path.
            g.FillPath(((ShapeStyle)item.Style).BackBrush, path);
            g.DrawPath(((ShapeStyle)item.Style).ForePen, path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="shape"></param>
        public static void Render(this Polyline shape, Graphics g, GraphicItem item)
        {
            g.FillPolygon(((ShapeStyle)item.Style).BackBrush, shape.Points.ToPointFArray());
            g.DrawLines(((ShapeStyle)item.Style).ForePen, shape.Points.ToPointFArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="shape"></param>
        public static void Render(this Rectangle2D shape, Graphics g, GraphicItem item)
        {
            g.FillRectangles(((ShapeStyle)item.Style).BackBrush, new RectangleF[] { shape.Bounds.ToRectangleF() });
            g.DrawRectangles(((ShapeStyle)item.Style).ForePen, new RectangleF[] { shape.Bounds.ToRectangleF() });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="shape"></param>
        public static void Render(this Arc shape, Graphics g, GraphicItem item)
        {
            g.DrawArc(((ShapeStyle)item.Style).ForePen, shape.DrawingBounds.ToRectangleF(), -(float)shape.StartAngle.ToDegrees(), (float)(shape.SweepAngle.ToDegrees()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="shape"></param>
        public static void Render(this Circle shape, Graphics g, GraphicItem item)
        {
            g.FillEllipse(((ShapeStyle)item.Style).BackBrush, shape.Bounds.ToRectangleF());
            g.DrawEllipse(((ShapeStyle)item.Style).ForePen, shape.Bounds.ToRectangleF());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="shape"></param>
        public static void Render(this Ellipse shape, Graphics g, GraphicItem item)
        {
            g.FillPolygon(((ShapeStyle)item.Style).BackBrush, item.LengthInterpolatedPoints.ToPointFArray());
            g.DrawPolygon(((ShapeStyle)item.Style).ForePen, item.LengthInterpolatedPoints.ToPointFArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="shape"></param>
        public static void Render(this CubicBezier shape, Graphics g, GraphicItem item)
        {
            // g.FillPolygon(style.BackBrush, item.LengthInterpolatedPoints.ToPointFArray());
            g.DrawBezier(((ShapeStyle)item.Style).ForePen, shape.A.ToPointF(), shape.B.ToPointF(), shape.C.ToPointF(), shape.D.ToPointF());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="shape"></param>
        public static void Render(this QuadraticBezier shape, Graphics g, GraphicItem item)
        {
            //g.FillPolygon(((ShapeStyle)item.Style).BackBrush, item.LengthInterpolatedPoints.ToPointFArray());
            g.DrawCurve(((ShapeStyle)item.Style).ForePen, item.LengthInterpolatedPoints.ToPointFArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="theta"></param>
        /// <param name="ellipse"></param>
        /// <param name="phi"></param>
        /// <param name="rect"></param>
        private static void Draw_rect_at_ellipse(Graphics g, double theta, Rectangle2D ellipse, double phi, Rectangle2D rect)
        {
            Point2D xaxis = new Point2D(Cos(theta), Sin(theta));
            Point2D yaxis = new Point2D(-Sin(theta), Cos(theta));
            Point2D ellipse_point;

            // Ellipse equation for an ellipse at origin.
            ellipse_point = new Point2D(ellipse.Width * Cos(phi), ellipse.Height * Sin(phi));

            // Apply the rotation transformation and translate to new center.
            rect.Location = new Point2D(ellipse.Left + (ellipse_point.X * xaxis.X + ellipse_point.Y * xaxis.Y),
                                       ellipse.Top + (ellipse_point.X * yaxis.X + ellipse_point.Y * yaxis.Y));

            g.DrawRectangle(Pens.AntiqueWhite, (float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
        }

        /// <summary>
        /// Bow Curve (2D)
        /// </summary>
        /// <param name="g"></param>
        /// <param name="DPen"></param>
        /// <param name="Precision"></param>
        /// <param name="Offset"></param>
        /// <param name="Multiplyer"></param>
        /// <remarks>
        ///  Also known as the "cocked hat", it was first documented by Sylvester around 
        ///  1864 and Cayley in 1867. 
        /// </remarks>
        private static void DrawBowCurve2D(Graphics g, Pen DPen, double Precision, Size2D Offset, Size2D Multiplyer)
        {
            Point2D NewPoint = new Point2D(
                ((1 - (Tan((PI * -1)) * 2)) * Cos((PI * -1))) * Multiplyer.Width,
                ((1 - (Tan((PI * -1)) * 2)) * (2 * Sin((PI * -1)))) * Multiplyer.Height
                );

            Point2D LastPoint = NewPoint;

            for (double Index = (PI * -1); (Index <= PI); Index += Precision)
            {
                LastPoint = NewPoint;
                NewPoint = new Point2D(
                    ((1 - (Tan(Index) * 2)) * Cos(Index)) * Multiplyer.Width,
                    ((1 - (Tan(Index) * 2)) * (2 * Sin(Index))) * Multiplyer.Height
                    );

                g.DrawLine(DPen, NewPoint.ToPointF(), LastPoint.ToPointF());
            }
        }

        /// <summary>
        /// Butterfly Curve
        /// </summary>
        /// <param name="g"></param>
        /// <param name="DPen"></param>
        /// <param name="Precision"></param>
        /// <param name="Offset"></param>
        /// <param name="Multiplyer"></param>
        private static void DrawButterflyCurve2D(Graphics g, Pen DPen, double Precision, SizeF Offset, SizeF Multiplyer)
        {
            const double N = 10000;
            double U = (0 * (24 * (PI / N)));

            Point2D NewPoint = new Point2D(
                Cos(U) * ((Exp(Cos(U)) - ((2 * Cos((4 * U))) - Pow(Sin((U / 12)), 5))) * Multiplyer.Width),
                (Sin(U) * (Exp(Cos(U)) - ((2 * Cos((4 * U))) - Pow(Sin((U / 12)), 5)))) * Multiplyer.Height
                );

            Point2D LastPoint = NewPoint;

            for (double Index = 1; (Index <= N); Index = (Index + Precision))
            {
                LastPoint = NewPoint;
                U = (Index * (24 * (PI / N)));

                NewPoint = new Point2D(
                    Cos(U) * ((Exp(Cos(U)) - ((2 * Cos((4 * U))) - Pow(Sin((U / 12)), 5))) * Multiplyer.Width),
                    (Sin(U) * (Exp(Cos(U)) - ((2 * Cos((4 * U))) - Pow(Sin((U / 12)), 5)))) * Multiplyer.Height
                    );

                g.DrawLine(DPen, NewPoint.ToPointF(), LastPoint.ToPointF());
            }
        }
    }
}
