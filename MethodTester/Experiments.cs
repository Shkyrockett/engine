// <copyright file="Experiments.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <acknowledgment></acknowledgment>

using Engine;
using Engine.Imaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using static Engine.Maths;
using static System.Math;

// 22 classes left to migrate.

namespace MethodSpeedTester
{
    /// <summary>
    /// Class to contain experimental methods to test.
    /// </summary>
    public static partial class Experiments
    {
        #region Cubic Bézier Get T
        /// <summary>
        /// Find the t for coordinate.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="Lut">The Lut.</param>
        /// <returns>The <see cref="T:List{double}"/>.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/27053888/how-to-get-time-value-from-bezier-curve-given-length/27071218#27071218
        /// </acknowledgment>
        public static List<double> FindTForCoordinate(Point2D value, List<Point2D> Lut)
        {
            var point = new Point2D();
            var found = new List<double>();
            var len = Lut.Count;
            for (var i = 0; i < len; i++)
            {
                point.X = Lut[i].X;
                point.Y = Lut[i].Y;
                if (Abs(value.X - point.X) < DoubleEpsilon && Abs(value.Y - point.Y) < DoubleEpsilon)
                {
                    found.Add(i / len);
                }
            }
            return found;
        }

        /// <summary>
        /// Build the LUT.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/27053888/how-to-get-time-value-from-bezier-curve-given-length/27071218#27071218
        /// </acknowledgment>
        public static List<Point2D> BuildLUT(Point2D a, Point2D b, Point2D c, Point2D d)
        {
            var Lut = new List<Point2D>(100);
            for (double t = 0; t <= 1; t += 0.01)
            {
                Lut[(int)(t * 100)] = new Point2D(Interpolators.CubicBezier(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y, t));
            }

            return Lut;
        }
        #endregion Cubic Bézier Get T

        #region Find Polygon Ear
        /// <summary>
        /// Find the indexes of three points that form an "ear."
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/
        /// </acknowledgment>
        public static Triangle FindEar(PolygonContour polygon, ref int a, ref int b, ref int c)
        {
            var num_points = polygon.Points.Count;

            for (a = 0; a < num_points; a++)
            {
                b = (a + 1) % num_points;
                c = (b + 1) % num_points;

                if (FormsEar(polygon, a, b, c))
                {
                    return new Triangle(polygon.Points[a], polygon.Points[b], polygon.Points[c]);
                }
            }

            // We should never get here because there should
            // always be at least two ears.
            Debug.Assert(false);

            return null;
        }

        /// <summary>
        /// The forms ear.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <returns>The <see cref="bool"/>. Return true if the three points form an ear.</returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/
        /// </acknowledgment>
        public static bool FormsEar(PolygonContour polygon, int a, int b, int c)
        {
            // See if the angle ABC is concave.
            if (AngleVector(
                polygon.Points[a].X, polygon.Points[a].Y,
                polygon.Points[b].X, polygon.Points[b].Y,
                polygon.Points[c].X, polygon.Points[c].Y) > 0)
            {
                // This is a concave corner so the triangle
                // cannot be an ear.
                return false;
            }

            // Make the triangle A, B, C.
            var triangle = new Triangle(polygon.Points[a], polygon.Points[b], polygon.Points[c]);

            // Check the other points to see
            // if they lie in triangle A, B, C.
            for (var i = 0; i < polygon.Points.Count; i++)
            {
                if ((i != a) && (i != b) && (i != c) && triangle.Contains(polygon.Points[i]))
                {
                    // This point is in the triangle
                    // do this is not an ear.
                    return false;
                }
            }

            // This is an ear.
            return true;
        }
        #endregion Find Polygon Ear

        #region Fit in Rectangle
        /// <summary>
        /// The fit rect.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="radians">The radians.</param>
        /// <returns>The <see cref="Size2D"/>.</returns>
        public static Size2D FitRect(Size2D size, double radians)
        {
            var angleCos = Cos(radians);
            var angleSin = Sin(radians);

            var x1 = -size.Width * 0.5d;
            var x2 = size.Width * 0.5d;
            var x3 = size.Width * 0.5d;
            var x4 = -size.Width * 0.5d;

            var y1 = size.Height * 0.5d;
            var y2 = size.Height * 0.5d;
            var y3 = -size.Height * 0.5d;
            var y4 = -size.Height * 0.5d;

            var x11 = (x1 * angleCos) + (y1 * angleSin);
            var y11 = (-x1 * angleSin) + (y1 * angleCos);

            var x21 = (x2 * angleCos) + (y2 * angleSin);
            var y21 = (-x2 * angleSin) + (y2 * angleCos);

            var x31 = (x3 * angleCos) + (y3 * angleSin);
            var y31 = (-x3 * angleSin) + (y3 * angleCos);

            var x41 = (x4 * angleCos) + (y4 * angleSin);
            var y41 = (-x4 * angleSin) + (y4 * angleCos);

            var x_min = Min(Min(x11, x21), Min(x31, x41));
            var x_max = Max(Max(x11, x21), Max(x31, x41));

            var y_min = Min(Min(y11, y21), Min(y31, y41));
            var y_max = Max(Max(y11, y21), Max(y31, y41));

            return new Size2D(x_max - x_min, y_max - y_min);
        }
        #endregion Fit in Rectangle

        #region Gear Points
        // Draw the gear.
        /// <summary>
        /// The pic gears paint.
        /// </summary>
        /// <param name="e">The paint event arguments.</param>
        /// <param name="bounds">The bounds.</param>
        public static void PicGears_Paint(PaintEventArgs e, Rectangle bounds)
        {
            // Draw smoothly.
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            const double radius = 50;
            const double tooth_length = 10;
            var x = bounds.Width / 2 - radius - tooth_length - 1;
            double y = bounds.Height / 3;
            DrawGear(e.Graphics, Brushes.Black, Brushes.LightBlue, Pens.Blue, new Point2D(x, y),
                radius, tooth_length, 10, 5, true);

            x += 2 * radius + tooth_length + 2;
            DrawGear(e.Graphics, Brushes.Black, Brushes.LightGreen, Pens.Green, new Point2D(x, y),
                radius, tooth_length, 10, 5, false);

            y += 2 * radius + tooth_length + 2;
            DrawGear(e.Graphics, Brushes.Black, Brushes.Pink, Pens.Red, new Point2D(x, y),
                radius, tooth_length, 10, 5, true);
        }

        /// <summary>
        /// Draw a gear.
        /// </summary>
        /// <param name="gr">The gr.</param>
        /// <param name="axle_brush">The axle_brush.</param>
        /// <param name="gear_brush">The gear_brush.</param>
        /// <param name="gear_pen">The gear_pen.</param>
        /// <param name="center">The center.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="tooth_length">The tooth_length.</param>
        /// <param name="num_teeth">The num_teeth.</param>
        /// <param name="axle_radius">The axle_radius.</param>
        /// <param name="start_with_tooth">The start_with_tooth.</param>
        public static void DrawGear(Graphics gr, Brush axle_brush, Brush gear_brush, Pen gear_pen, Point2D center, double radius, double tooth_length, int num_teeth, double axle_radius, bool start_with_tooth)
        {
            var dtheta = PI / num_teeth;
            var dtheta_degrees = dtheta * 180 / PI; // dtheta in degrees.

            const double chamfer = 2;
            var tooth_width = radius * dtheta - chamfer;
            var alpha = tooth_width / (radius + tooth_length);
            var alpha_degrees = alpha * 180 / PI;
            var phi = (dtheta - alpha) / 2;

            // Set theta for the beginning of the first tooth.
            double theta;
            theta = start_with_tooth ? dtheta / 2 : -dtheta / 2;

            // Make rectangles to represent the gear's inner and outer arcs.
            var inner_rect = new Rectangle2D(
                center.X - radius, center.Y - radius,
                2 * radius, 2 * radius);
            var outer_rect = new Rectangle2D(
                center.X - radius - tooth_length, center.Y - radius - tooth_length,
                2 * (radius + tooth_length), 2 * (radius + tooth_length));

            // Make a path representing the gear.
            var path = new GraphicsPath();
            for (var i = 0; i < num_teeth; i++)
            {
                // Move across the gap between teeth.
                var degrees = theta * 180 / PI;
                path.AddArc(inner_rect.ToRectangleF(), (float)degrees, (float)dtheta_degrees);
                theta += dtheta;

                // Move across the tooth's outer edge.
                degrees = (theta + phi) * 180 / PI;
                path.AddArc(outer_rect.ToRectangleF(), (float)degrees, (float)alpha_degrees);
                theta += dtheta;
            }

            path.CloseFigure();

            // Draw the gear.
            gr.FillPath(gear_brush, path);
            gr.DrawPath(gear_pen, path);
            gr.FillEllipse(axle_brush,
                 (float)(center.X - axle_radius), (float)(center.Y - axle_radius),
                (float)(2 * axle_radius), (float)(2 * axle_radius));
        }
        #endregion Gear Points

        #region Heart Interpolation
        /// <summary>
        /// The curve's parametric equations.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public static Point2D Heart(double t)
        {
            var sin_t = Sin(t);
            return new Point2D(16 * sin_t * sin_t * sin_t,
                 13 * Cos(t)
                - 5 * Cos(2 * t)
                - 2 * Cos(3 * t)
                - Cos(4 * t));
        }

        // Draw the curve on a bitmap.
        /// <summary>
        /// The draw heart.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>The <see cref="Bitmap"/>.</returns>
        public static Bitmap DrawHeart(int width, int height)
        {
            var bm = new Bitmap(width, height);
            using (var gr = Graphics.FromImage(bm))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;

                // Generate the points.
                const int num_points = 100;
                var points = new List<Point2D>();
                const double dt = 2 * PI / num_points;
                for (double t = 0; t <= 2 * PI; t += dt)
                {
                    points.Add(new Point2D(Heart(t).X, Heart(t).Y));
                }

                // Get the coordinate bounds.
                var wxmin = points[0].X;
                var wxmax = wxmin;
                var wymin = points[0].Y;
                var wymax = wymin;
                foreach (Point2D point in points)
                {
                    if (wxmin > point.X)
                    {
                        wxmin = point.X;
                    }

                    if (wxmax < point.X)
                    {
                        wxmax = point.X;
                    }

                    if (wymin > point.Y)
                    {
                        wymin = point.Y;
                    }

                    if (wymax < point.Y)
                    {
                        wymax = point.Y;
                    }
                }

                // Make the world coordinate rectangle.
                var world_rect = new Rectangle2D(
                    wxmin, wymin, wxmax - wxmin, wymax - wymin);

                // Make the device coordinate rectangle with a margin.
                const int margin = 5;
                var device_rect = new Rectangle2D(
                    margin, margin,
                    width - 2 * margin,
                    height - 2 * margin);

                // Map world to device coordinates without distortion.
                // Flip vertically so Y increases downward.
                SetTransformationWithoutDisortion(gr, world_rect, device_rect, false, true);

                // Draw the curve.
                gr.FillPolygon(Brushes.Pink, points.ToPointFArray());
                using (var pen = new Pen(Color.Red, 0))
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

        // Map from world coordinates to device coordinates
        // without distortion.
        /// <summary>
        /// Set the transformation without distortion.
        /// </summary>
        /// <param name="gr">The gr.</param>
        /// <param name="world_rect">The world_rect.</param>
        /// <param name="device_rect">The device_rect.</param>
        /// <param name="invert_x">The invert_x.</param>
        /// <param name="invert_y">The invert_y.</param>
        public static void SetTransformationWithoutDisortion(Graphics gr,
            Rectangle2D world_rect, Rectangle2D device_rect,
            bool invert_x, bool invert_y)
        {
            // Get the aspect ratios.
            var world_aspect = world_rect.Width / world_rect.Height;
            var device_aspect = device_rect.Width / device_rect.Height;

            // Adjust the world rectangle to maintain the aspect ratio.
            var world_cx = world_rect.X + world_rect.Width / 2f;
            var world_cy = world_rect.Y + world_rect.Height / 2f;
            if (world_aspect > device_aspect)
            {
                // The world coordinates are too short and width.
                // Make them taller.
                var world_height = world_rect.Width / device_aspect;
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
                var world_width = device_aspect * world_rect.Height;
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
        /// <summary>
        /// Set the transformation.
        /// </summary>
        /// <param name="gr">The gr.</param>
        /// <param name="world_rect">The world_rect.</param>
        /// <param name="device_rect">The device_rect.</param>
        /// <param name="invert_x">The invert_x.</param>
        /// <param name="invert_y">The invert_y.</param>
        public static void SetTransformation(Graphics gr,
            Rectangle2D world_rect, Rectangle2D device_rect,
            bool invert_x, bool invert_y)
        {
            var device_points = new List<Point2D>
            {
                new Point2D(device_rect.Left, device_rect.Top),      // Upper left.
                new Point2D(device_rect.Right, device_rect.Top),     // Upper right.
                new Point2D(device_rect.Left, device_rect.Bottom)   // Lower left.
            };

            if (invert_x)
            {
                device_points[0] = new Point2D(device_rect.Right, device_points[0].Y);
                device_points[1] = new Point2D(device_rect.Left, device_points[1].Y);
                device_points[2] = new Point2D(device_rect.Right, device_points[2].Y);
            }
            if (invert_y)
            {
                device_points[0] = new Point2D(device_points[0].X, device_rect.Bottom);
                device_points[1] = new Point2D(device_points[1].X, device_rect.Bottom);
                device_points[2] = new Point2D(device_points[2].X, device_rect.Top);
            }

            gr.Transform = new Matrix(world_rect.ToRectangleF(), device_points.ToPointFArray());
        }
        #endregion Heart Interpolation

        #region Is Convex
        /// <summary>
        /// For each set of three adjacent points A, B, C,
        /// find the dot product AB · BC. If the sign of
        /// all the dot products is the same, the angles
        /// are all positive or negative (depending on the
        /// order in which we visit them) so the polygon
        /// is convex.
        /// </summary>
        /// <returns>
        /// Return true if the polygon is convex.
        /// </returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/determine-whether-a-polygon-is-convex-in-c/
        /// </acknowledgment>
        public static bool IsConvex(PolygonContour polygon)
        {
            var got_negative = false;
            var got_positive = false;
            var num_points = polygon.Points.Count;
            int B, C;
            for (var A = 0; A < num_points; A++)
            {
                B = (A + 1) % num_points;
                C = (B + 1) % num_points;

                var cross_product = CrossProductVector(
                        polygon.Points[A].X, polygon.Points[A].Y,
                        polygon.Points[B].X, polygon.Points[B].Y,
                        polygon.Points[C].X, polygon.Points[C].Y);
                if (cross_product < 0)
                {
                    got_negative = true;
                }
                else
                {
                    got_positive |= cross_product > 0;
                }

                if (got_negative && got_positive)
                {
                    return false;
                }
            }

            // If we got this far, the polygon is convex.
            return true;
        }
        #endregion Is Convex

        #region Linear Offset Interpolation
        /// <summary>
        /// The offset interpolate.
        /// </summary>
        /// <param name="Value1">The Value1.</param>
        /// <param name="Value2">The Value2.</param>
        /// <param name="Offset">The Offset.</param>
        /// <param name="Weight">The Weight.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public static Point2D OffsetInterpolate(Point2D Value1, Point2D Value2, double Offset, double Weight)
        {
            var UnitVectorAB = new Vector2D(Value1, Value2);
            var PerpendicularAB = UnitVectorAB.Perpendicular().Scale(0.5d).Scale(Offset);
            return Interpolators.Linear(Value1, Value2, Weight).Inflate(PerpendicularAB);
        }
        #endregion Linear Offset Interpolation

        #region Offset Line
        /// <summary>
        /// Calculate the geometry of points offset at a specified distance. aka Double Line.
        /// </summary>
        /// <param name="pointA">First reference point.</param>
        /// <param name="pointB">First inclusive point.</param>
        /// <param name="pointC">Second inclusive point.</param>
        /// <param name="pointD">Second reference point.</param>
        /// <param name="offsetDistance">Offset Distance</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Suppose you have 4 points; A, B C, and D. With Lines AB, BC, and CD.<BR/>
        ///<BR/>
        ///                 B1         BC1       C1<BR/>
        ///                   |\¯B¯¯¯¯¯BC¯¯¯C¯¯¯/|<BR/>
        ///                   | \--------------/ |<BR/>
        ///                   | |\____________/| |<BR/>
        ///                   | | |B2  BC2 C2| | |<BR/>
        ///                 AB| | |          | | |CD<BR/>
        ///                AB1| | |AB2    CD2| | |CD1<BR/>
        ///                   | | |          | | |<BR/>
        ///                   | | |          | | |<BR/>
        ///               A1  A  A2      D2  D  D1<BR/>
        ///
        /// </acknowledgment>
        public static Point2D[] CenteredOffsetLinePoints(Point2D pointA, Point2D pointB, Point2D pointC, Point2D pointD, double offsetDistance)
        {
            // To get the vectors of the angles at each corner B and C, Normalize the Unit Delta Vectors along AB, BC, and CD.
            var UnitVectorAB = pointB.Subtract(pointA).Unit();
            var UnitVectorBC = pointC.Subtract(pointB).Unit();
            var UnitVectorCD = pointD.Subtract(pointC).Unit();

            //  Find the Perpendicular of the outside vectors
            var PerpendicularAB = UnitVectorAB.Perpendicular();
            var PerpendicularCD = UnitVectorCD.Perpendicular();

            //  Normalized Vectors pointing out from B and C.
            var OutUnitVectorB = (UnitVectorAB - UnitVectorBC).Unit();
            var OutUnitVectorC = (UnitVectorCD - UnitVectorBC).Unit();

            //  The distance out from B is the radius / Cos(theta) where theta is the angle
            //  from the perpendicular of BC of the UnitVector. The cosine can also be
            //  calculated by doing the dot product of  Unit(Perpendicular(AB)) and
            //  UnitVector.
            var BPointScale = PerpendicularAB.DotProduct(OutUnitVectorB) * offsetDistance;
            var CPointScale = PerpendicularCD.DotProduct(OutUnitVectorC) * offsetDistance;

            OutUnitVectorB = OutUnitVectorB.Scale(CPointScale);
            OutUnitVectorC = OutUnitVectorC.Scale(BPointScale);

            // Corners of the parallelogram to draw
            var Out = new Point2D[] {
                pointC + OutUnitVectorC,
                pointB + OutUnitVectorB,
                pointB - OutUnitVectorB,
                pointC - OutUnitVectorC,
                pointC + OutUnitVectorC
            };
            return Out;
        }
        #endregion Offset Line

        #region List Interpolation Points of Cubic Bézier
        /// <summary>
        /// The interpolate points.
        /// </summary>
        /// <param name="bezier">The bezier.</param>
        /// <param name="count">The count.</param>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        public static List<Point2D> InterpolatePoints(CubicBezier bezier, int count)
        {
            var ipoints = new Point2D[count + 1];
            for (var i = 0; i <= count; i += 1)
            {
                var v = 1d / count * i;
                ipoints[i] = bezier.Interpolate(v);
            }

            return new List<Point2D>(ipoints);
        }

        /// <summary>
        /// The interpolate cubic beizer points.
        /// </summary>
        /// <param name="bezier">The bezier.</param>
        /// <param name="count">The count.</param>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        public static List<Point2D> InterpolateCubicBeizerPoints(CubicBezier bezier, int count) => InterpolateCubicBeizerPoints(bezier.A, bezier.B, bezier.C, bezier.D, count);

        /// <summary>
        /// The interpolate cubic beizer points.
        /// </summary>
        /// <param name="a">the starting point, or A in the above diagram</param>
        /// <param name="b">the first control point, or B</param>
        /// <param name="c">the second control point, or C</param>
        /// <param name="d">the end point, or D</param>
        /// <param name="Precision">The Precision.</param>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        public static List<Point2D> InterpolateCubicBeizerPoints(Point2D a, Point2D b, Point2D c, Point2D d, double Precision)
        {
            var BPoints = new Point2D[(int)((1 / Precision) + 2)];
            BPoints[0] = a;
            BPoints[BPoints.Length - 1] = d;
            var Node = 0;
            for (double Index = 0; Index < 1; Index += Precision)
            {
                Node++;
                BPoints[Node] = new Point2D(Interpolators.CubicBezier(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y, Index));
            }

            return new List<Point2D>(BPoints);
        }

        /// <summary>
        /// The compute bezier interpolations.
        /// </summary>
        /// <param name="bezier">The bezier.</param>
        /// <param name="count">The count.</param>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        public static List<Point2D> ComputeBezierInterpolations(CubicBezier bezier, int count) => ComputeBezierInterpolations(bezier.A, bezier.B, bezier.C, bezier.D, count);

        /// <summary>
        ///  ComputeBezier fills an array of Point2D structs with the curve points
        ///  generated from the control points cp. Caller must allocate sufficient memory
        ///  for the result, which is [sizeof(Point2D) * numberOfPoints]
        /// </summary>
        /// <param name="a">the starting point, or A in the above diagram</param>
        /// <param name="b">the first control point, or B</param>
        /// <param name="c">the second control point, or C</param>
        /// <param name="d">the end point, or D</param>
        /// <param name="numberOfPoints"></param>
        public static List<Point2D> ComputeBezierInterpolations(Point2D a, Point2D b, Point2D c, Point2D d, int numberOfPoints)
        {
            var curve = new List<Point2D>();
            double t = 0;
            var dt = 1.0d / (numberOfPoints - 1);
            for (var i = 0; i <= numberOfPoints; i++)
            {
                t += dt;
                curve.Add(new Point2D(Interpolators.CubicBezier(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y, t)));
            }
            return curve;
        }

        /// <summary>
        /// The interpolate cubic beizer points0.
        /// </summary>
        /// <param name="bezier">The bezier.</param>
        /// <param name="count">The count.</param>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        public static List<Point2D> InterpolateCubicBeizerPoints0(CubicBezier bezier, int count) => InterpolateCubicBeizerPoints0(bezier.A, bezier.B, bezier.C, bezier.D, count);

        /// <summary>
        /// Function to Plot a Cubic Bezier
        /// </summary>
        /// <param name="a">the starting point, or A in the above diagram</param>
        /// <param name="b">the first control point, or B</param>
        /// <param name="c">the second control point, or C</param>
        /// <param name="d">the end point, or D</param>
        /// <param name="Precision"></param>
        /// <returns></returns>
        public static List<Point2D> InterpolateCubicBeizerPoints0(Point2D a, Point2D b, Point2D c, Point2D d, double Precision)
        {
            var BPoints = new Point2D[(int)((1 / Precision) + 2)];
            BPoints[0] = a;
            BPoints[BPoints.Length - 1] = d;
            var Node = 0;
            for (double Index = 0; Index <= 1; Index = Index + Precision)
            {
                Node++;
                BPoints[Node] = new Point2D(Interpolators.CubicBezier(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y, Index));
            }

            return new List<Point2D>(BPoints);
        }
        #endregion List Interpolation Points of Cubic Bézier

        #region List Interpolation Points of Quadratic Bézier
        /// <summary>
        /// The interpolate quadratic bezier points.
        /// </summary>
        /// <param name="bezier">The bezier.</param>
        /// <param name="count">The count.</param>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        public static List<Point2D> InterpolateQuadraticBezierPoints(QuadraticBezier bezier, int count)
        {
            var ipoints = new Point2D[count + 1];
            for (var i = 0; i <= count; i += 1)
            {
                double v = 1f / count * i;
                ipoints[i] = bezier.Interpolate(v);
            }

            return new List<Point2D>(ipoints);
        }
        #endregion List Interpolation Points of Quadratic Bézier

        #region N Polygon Intersecting Star
        // Return PointFs to define a non-intersecting star.
        /// <summary>
        /// The non intersecting star points.
        /// </summary>
        /// <param name="num_points">The num_points.</param>
        /// <param name="bounds">The bounds.</param>
        /// <returns>The <see cref="T:PointF[]"/>.</returns>
        public static PointF[] NonIntersectingStarPoints(int num_points, Rectangle bounds)
        {
            // Make room for the points.
            var pts = new PointF[2 * num_points];

            double rx1 = bounds.Width / 2;
            double ry1 = bounds.Height / 2;
            var rx2 = rx1 * 0.5;
            var ry2 = ry1 * 0.5;
            var cx = bounds.X + rx1;
            var cy = bounds.Y + ry1;

            // Start at the top.
            var theta = -PI / 2;
            var dtheta = PI / num_points;
            for (var i = 0; i < 2 * num_points; i += 2)
            {
                pts[i] = new PointF(
                    (float)(cx + rx1 * Cos(theta)),
                    (float)(cy + ry1 * Sin(theta)));
                theta += dtheta;

                pts[i + 1] = new PointF(
                    (float)(cx + rx2 * Cos(theta)),
                    (float)(cy + ry2 * Sin(theta)));
                theta += dtheta;
            }

            return pts;
        }
        #endregion N Polygon Intersecting Star

        #region Orient Polygon Clockwise
        /// <summary>
        /// If the polygon is oriented counterclockwise,
        /// reverse the order of its points.
        /// </summary>
        /// <param name="polygon"></param>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/
        /// </acknowledgment>
        public static void OrientPolygonClockwise(PolygonContour polygon)
        {
            if (polygon.Orientation == RotationDirections.CounterClockwise)
            {
                polygon.Points.Reverse();
            }
        }
        #endregion Orient Polygon Clockwise

        #region Rectangle To Square
        /// <summary>
        /// The to square.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <returns>The <see cref="Rectangle2D"/>.</returns>
        public static Rectangle2D ToSquare(Rectangle2D rect)
        {
            var smallest = rect.Height <= rect.Width ? rect.Height : rect.Width;
            return new Rectangle2D(
                rect.X + ((rect.Width - smallest) * 0.5d),
                rect.Y + ((rect.Height - smallest) * 0.5d),
                smallest,
                smallest);
        }
        #endregion Rectangle To Square

        #region Remove Point
        /// <summary>
        /// Remove point target from the array.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="target"></param>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/
        /// </acknowledgment>
        public static void RemovePoint(PolygonContour polygon, int target)
            => polygon.Points.RemoveAt(target);

        /// <summary>
        /// Remove point target from the array.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="target"></param>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/
        /// </acknowledgment>
        public static void RemovePoint1(PolygonContour polygon, int target)
        {
            var points = new Point2D[polygon.Points.Count - 1];
            //List.Copy(polygon.Points, 0, points, 0, target);
            Array.Copy(polygon.Points.ToArray(), target + 1, points, target, polygon.Points.Count - target - 1);
            polygon.Points = points.ToList();
        }
        #endregion Remove Point

        #region Remove Polygon Ear
        /// <summary>
        /// Remove an ear from the polygon and add it to the triangles array.
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="triangles"></param>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/
        /// </acknowledgment>
        public static void RemoveEar(PolygonContour polygon, List<Triangle> triangles)
        {
            // Find an ear.
            var A = 0;
            var B = 0;
            var C = 0;

            // Create a new triangle for the ear.
            triangles.Add(FindEar(polygon, ref A, ref B, ref C));

            // Remove the ear from the polygon.
            RemovePoint(polygon, B);
        }
        #endregion Remove Polygon Ear

        #region Retrieve Cursor Resource
        /// <summary>
        /// Retrieve Cursor Resource from Executable
        /// </summary>
        /// <param name="ResourceName"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// BE SURE (embedded).cur HAS BUILD ACTION IN PROPERTIES SET TO EMBEDDED RESOURCE!!
        /// </acknowledgment>
        public static Cursor RetriveCursorResource(string ResourceName)
        {
            //  Get the namespace
            var strNameSpace = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            //  Get the resource into a stream
            var ResourceStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(strNameSpace + "." + ResourceName);
            if (ResourceStream is null)
            {
                // ToDo: #If Then ... Warning!!! not translated
                MessageBox.Show("Unable to find: "
                                + ResourceName + "\r\n" + "Be Sure "
                                + ResourceName + " Property Build Action is set to Embedded Resource" + "\r\n" + "Another reason can be that the Project Root Namespace is not the same as the Assembly Name");
                // ToDo: # ... Warning!!! not translated
            }
            else
            {
                //  ToDo: Report the Error message in a nicer fashion since this in game.
                //  Perhaps on Exit provide a message errors were encountered and
                //  ignored would you like to send an error report?
                // ToDo: #End If ... Warning!!! not translated
                return Cursors.Default;
            }
            //  Return the Resource as a cursor
            if (ResourceStream.CanRead)
            {
                return new Cursor(ResourceStream);
            }
            else
            {
                return Cursors.Default;
            }
        }
        #endregion Retrieve Cursor Resource

        #region Rotation Matrix
        /// <summary>
        /// Creates a matrix to rotate an object around a particular point.
        /// </summary>
        /// <param name="center">The point around which to rotate.</param>
        /// <param name="angle">The angle to rotate in radians.</param>
        /// <returns>Return a rotation matrix to rotate around a point.</returns>
        public static Matrix3x2D RotateAroundPoint(Point2D center, double angle)
        {
            // Translate the point to the origin.
            var result = new Matrix3x2D();

            // We need to go counter-clockwise.
            result.RotateAt(-angle.ToDegrees(), center.X, center.Y);

            return result;
        }
        #endregion Rotation Matrix

        #region Round
        /// <summary>
        /// Set of tests to run testing methods that calculate the 1D cubic interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(RoundTests))]
        public static List<SpeedTester> RoundTests()
        {
            const double value = 0.5d;
            return new List<SpeedTester> {
                new SpeedTester(() => RoundAFZ(value),
                $"{nameof(Experiments.RoundAFZ)}({value})"),
                new SpeedTester(() => RoundToEven(value),
                $"{nameof(Experiments.RoundToEven)}({value})"),
                new SpeedTester(() => RoundToInt32(value),
                $"{nameof(Experiments.RoundToInt32)}({value})"),
                new SpeedTester(() => Round(value),
                $"{nameof(Experiments.Round)}({value})"),
                new SpeedTester(() => Truncate(value),
                $"{nameof(Experiments.Truncate)}({value})")
            };
        }

        /// <summary>
        /// Away from zero rounding.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static double RoundAFZ(double value, int decimals)
            => Math.Round(value, decimals, MidpointRounding.AwayFromZero);

        /// <summary>
        /// Away from zero rounding.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double RoundAFZ(double value)
            => Math.Round(value, 0, MidpointRounding.AwayFromZero);

        /// <summary>
        /// To Even, or Bankers rounding.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double RoundToEven(double value)
            => Math.Round(value, 0, MidpointRounding.ToEven);

        /// <summary>
        /// To Even, or Bankers rounding.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double RoundToInt32(double value)
            => Convert.ToInt32(value);

        /// <summary>
        /// Away from zero rounding.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double Round(double value)
            => value < 0 ? (int)(value - 0.5) : (int)(value + 0.5);

        /// <summary>
        /// Truncate rounding.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double Truncate(double value)
            => (int)value;
        #endregion Round

        #region Triangulate a Polygon
        /// <summary>
        /// Set of tests to run testing methods that get the triangles of a polygon.
        /// </summary>
        /// <returns></returns>
        [System.ComponentModel.DisplayName(nameof(TriangulateTests))]
        public static List<SpeedTester> TriangulateTests()
            => new List<SpeedTester> {
                new SpeedTester(() => Triangulate(new PolygonContour(new Point2D[] { new Point2D(0, 0), new Point2D(0, 1), new Point2D(1, 1), new Point2D(1, 0)})),
                $"{nameof(Experiments.Triangulate)}(new Polygon(new Point2D[] {{ new Point2D(0, 0), new Point2D(0, 1), new Point2D(1, 1), new Point2D(1, 0)}}))"),
           };

        /// <summary>
        /// Triangulate the polygon.
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/triangulate-a-polygon-in-c/
        /// For a nice, detailed explanation of this method,
        /// see Ian Garton's Web page:
        /// http://www-cgrl.cs.mcgill.ca/~godfried/teaching/cg-projects/97/Ian/cutting_ears.html
        /// </acknowledgment>
        public static List<Triangle> Triangulate(PolygonContour polygon)
        {
            // Copy the points into a scratch array.
            var pts = new List<Point2D>(polygon.Points);

            // Make a scratch polygon.
            var pgon = new PolygonContour(pts);

            // Orient the polygon clockwise.
            OrientPolygonClockwise(pgon);

            // Make room for the triangles.
            var triangles = new List<Triangle>();

            // While the copy of the polygon has more than
            // three points, remove an ear.
            while (pgon.Points.Count > 3)
            {
                // Remove an ear from the polygon.
                RemoveEar(pgon, triangles);
            }

            // Copy the last three points into their own triangle.
            triangles.Add(new Triangle(pgon.Points[0], pgon.Points[1], pgon.Points[2]));

            return triangles;
        }
        #endregion Triangulate a Polygon

        /// <summary>
        /// The draw rect at ellipse.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="theta">The theta.</param>
        /// <param name="ellipse">The ellipse.</param>
        /// <param name="phi">The phi.</param>
        /// <param name="rect">The rect.</param>
        public static void Draw_rect_at_ellipse(Graphics g, double theta, Rectangle2D ellipse, double phi, Rectangle2D rect)
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
        /// <acknowledgment>
        ///  Also known as the "cocked hat", it was first documented by Sylvester around
        ///  1864 and Cayley in 1867.
        /// </acknowledgment>
        public static void DrawBowCurve2D(Graphics g, Pen DPen, double Precision, Size2D Offset, Size2D Multiplyer)
        {
            var NewPoint = new Point2D(
                (1d - (Tan(PI * -1d) * 2d)) * Cos(PI * -1d) * Multiplyer.Width,
                (1d - (Tan(PI * -1d) * 2d)) * (2d * Sin(PI * -1d)) * Multiplyer.Height
                );

            var LastPoint = NewPoint;

            for (var Index = PI * -1d; Index <= PI; Index += Precision)
            {
                LastPoint = NewPoint;
                NewPoint = new Point2D(
                    (1d - (Tan(Index) * 2d)) * Cos(Index) * Multiplyer.Width,
                    (1d - (Tan(Index) * 2d)) * (2d * Sin(Index)) * Multiplyer.Height
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
        public static void DrawButterflyCurve2D(Graphics g, Pen DPen, double Precision, SizeF Offset, SizeF Multiplyer)
        {
            const double N = 10000d;
            var U = 0d;

            var NewPoint = new Point2D(
                Cos(U) * ((Exp(Cos(U)) - ((2d * Cos(4d * U)) - Pow(Sin(U / 12d), 5d))) * Multiplyer.Width),
                Sin(U) * (Exp(Cos(U)) - ((2d * Cos(4d * U)) - Pow(Sin(U / 12d), 5d))) * Multiplyer.Height
                );

            var LastPoint = NewPoint;

            for (double Index = 1; Index <= N; Index = Index + Precision)
            {
                LastPoint = NewPoint;
                U = Index * (24d * (PI / N));

                NewPoint = new Point2D(
                    Cos(U) * ((Exp(Cos(U)) - ((2d * Cos(4d * U)) - Pow(Sin(U / 12d), 5d))) * Multiplyer.Width),
                    Sin(U) * (Exp(Cos(U)) - ((2d * Cos(4d * U)) - Pow(Sin(U / 12d), 5d))) * Multiplyer.Height
                    );

                g.DrawLine(DPen, NewPoint.ToPointF(), LastPoint.ToPointF());
            }
        }
    }
}
