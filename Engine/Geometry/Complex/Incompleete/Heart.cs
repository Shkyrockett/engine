using Engine.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Engine.Geometry
{
    /// <summary>
    /// http://csharphelper.com/blog/2016/02/draw-parametric-heart-shaped-curve-c/
    /// </summary>
    [Serializable]
    //[GraphicsObject]
    [DisplayName(nameof(Heart))]
    public class Heart
        :Shape
    {
        // Draw the curve on a bitmap.
        private Bitmap DrawHeart(int width, int height)
        {
            Bitmap bm = new Bitmap(width, height);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;

                // Generate the points.
                const int num_points = 100;
                List<Point2D> points = new List<Point2D>();
                double dt = 2 * Math.PI / num_points;
                for (double t = 0; t <= 2 * Math.PI; t += dt)
                    points.Add(new Point2D(X(t), Y(t)));

                // Get the coordinate bounds.
                double wxmin = points[0].X;
                double wxmax = wxmin;
                double wymin = points[0].Y;
                double wymax = wymin;
                foreach (Point2D point in points)
                {
                    if (wxmin > point.X) wxmin = point.X;
                    if (wxmax < point.X) wxmax = point.X;
                    if (wymin > point.Y) wymin = point.Y;
                    if (wymax < point.Y) wymax = point.Y;
                }

                // Make the world coordinate rectangle.
                Rectangle2D world_rect = new Rectangle2D(
                    wxmin, wymin, wxmax - wxmin, wymax - wymin);

                // Make the device coordinate rectangle with a margin.
                const int margin = 5;
                Rectangle2D device_rect = new Rectangle2D(
                    margin, margin,
                    width - 2 * margin,
                    height - 2 * margin);

                // Map world to device coordinates without distortion.
                // Flip vertically so Y increases downward.
                SetTransformationWithoutDisortion(gr, world_rect, device_rect, false, true);

                // Draw the curve.
                gr.FillPolygon(Brushes.Pink, points.ToPointFArray());
                using (Pen pen = new Pen(Color.Red, 0))
                {
                    gr.DrawPolygon(pen, points.ToPointFArray());

                    //// Draw a rectangle around the coordinate bounds.
                    //pen.Color = Color.Blue;
                    //gr.DrawRectangle(pen, Rectangle.Round( world_rect));

                    //// Draw the X and Y axes.
                    //pen.Color = Color.Green;
                    //gr.DrawLine(pen, -20, 0, 20, 0);
                    //gr.DrawLine(pen, 0, -20, 0, 20);
                    //for (int x = -20; x <= 20; x++)
                    //    gr.DrawLine(pen, x, -0.3f, x, 0.3f);
                    //for (int y = -20; y <= 20; y++)
                    //    gr.DrawLine(pen, -0.3f, y, 0.3f, y);
                }
            }
            return bm;
        }

        // The curve's parametric equations.
        private double X(double t)
        {
            double sin_t = Math.Sin(t);
            return 16 * sin_t * sin_t * sin_t;
        }

        private double Y(double t)
        {
            return
                13 * Math.Cos(t) -
                5 * Math.Cos(2 * t) -
                2 * Math.Cos(3 * t) -
                Math.Cos(4 * t);
        }

        // Map from world coordinates to device coordinates
        // without distortion.
        private void SetTransformationWithoutDisortion(Graphics gr,
            Rectangle2D world_rect, Rectangle2D device_rect,
            bool invert_x, bool invert_y)
        {
            // Get the aspect ratios.
            double world_aspect = world_rect.Width / world_rect.Height;
            double device_aspect = device_rect.Width / device_rect.Height;

            // Adjust the world rectangle to maintain the aspect ratio.
            double world_cx = world_rect.X + world_rect.Width / 2f;
            double world_cy = world_rect.Y + world_rect.Height / 2f;
            if (world_aspect > device_aspect)
            {
                // The world coordinates are too short and width.
                // Make them taller.
                double world_height = world_rect.Width / device_aspect;
                world_rect = new Rectangle2D(
                    world_rect.Left,
                    world_cy - world_height / 2f,
                    world_rect.Width,
                    world_height);
            }
            else
            {
                // The world coordinates are too tall and thin.
                // Make them wider.
                double world_width = device_aspect * world_rect.Height;
                world_rect = new Rectangle2D(
                    world_cx - world_width / 2f,
                    world_rect.Top,
                    world_width,
                    world_rect.Height);
            }

            // Map the new world coordinates into the device coordinates.
            SetTransformation(gr, world_rect, device_rect, invert_x, invert_y);
        }

        // Map from world coordinates to device coordinates.
        private void SetTransformation(Graphics gr,
            Rectangle2D world_rect, Rectangle2D device_rect,
            bool invert_x, bool invert_y)
        {
            List<Point2D> device_points = new List<Point2D>()
            {
                new Point2D(device_rect.Left, device_rect.Top),      // Upper left.
                new Point2D(device_rect.Right, device_rect.Top),     // Upper right.
                new Point2D(device_rect.Left, device_rect.Bottom),   // Lower left.
            };

            if (invert_x)
            {
                device_points[0].X = device_rect.Right;
                device_points[1].X = device_rect.Left;
                device_points[2].X = device_rect.Right;
            }
            if (invert_y)
            {
                device_points[0].Y = device_rect.Bottom;
                device_points[1].Y = device_rect.Bottom;
                device_points[2].Y = device_rect.Top;
            }

            gr.Transform = new Matrix(world_rect.ToRectangleF(), device_points.ToPointFArray());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Heart";
        }
    }
}
