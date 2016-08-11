using Engine.Geometry;
using Engine.Imaging;
using Engine.Objects;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using static System.Math;

namespace Engine.Winforms
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
        public static void Render(this Polyline shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            ShapeStyle itemStyle = style ?? (ShapeStyle)item.Style;
            g.FillPolygon((itemStyle).BackBrush, shape.Points.ToPointFArray());
            g.DrawLines((itemStyle).ForePen, shape.Points.ToPointFArray());
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
        public static void Render(this CircularArc shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            ShapeStyle itemStyle = style ?? (ShapeStyle)item.Style;
            List<Point2D> points = item?.InterpolatePoints();
            g.FillPolygon((itemStyle).BackBrush, points?.ToPointFArray());
            //g.DrawPolygon((itemStyle).ForePen, points?.ToPointFArray());

            g.DrawArc((itemStyle).ForePen, shape.DrawingBounds.ToRectangleF(), (float)shape.StartAngle.ToDegrees(), (float)(shape.SweepAngle.ToDegrees()));
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
            List<Point2D> points = item?.InterpolatePoints();
            g.FillPolygon((itemStyle).BackBrush, points?.ToPointFArray());
            //g.DrawPolygon((itemStyle).ForePen, points?.ToPointFArray());

            var mat = new Matrix();
            mat.RotateAt((float)shape.Angle.ToDegrees(), shape.Center.ToPointF());
            g.Transform = mat;
            g.DrawArc((itemStyle).ForePen, shape.DrawingBounds.ToRectangleF(), (float)(shape.StartAngle.ToDegrees()), (float)shape.SweepAngle.ToDegrees());
            g.ResetTransform();
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
        public static void Render(this Ellipse shape, Graphics g, GraphicItem item, ShapeStyle style = null)
        {
            ShapeStyle itemStyle = style ?? (ShapeStyle)item.Style;
            g.FillPolygon((itemStyle).BackBrush, item.LengthInterpolatedPoints.ToPointFArray());
            g.DrawPolygon((itemStyle).ForePen, item.LengthInterpolatedPoints.ToPointFArray());
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
            // g.FillPolygon(itemStyle.BackBrush, item.LengthInterpolatedPoints.ToPointFArray());
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
            //g.FillPolygon(((ShapeStyle)item.Style).BackBrush, item.LengthInterpolatedPoints.ToPointFArray());
            g.DrawCurve((itemStyle).ForePen, item.LengthInterpolatedPoints.ToPointFArray());
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
            var xaxis = new Point2D(Cos(theta), Sin(theta));
            var yaxis = new Point2D(-Sin(theta), Cos(theta));
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
            var NewPoint = new Point2D(
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

            var NewPoint = new Point2D(
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
