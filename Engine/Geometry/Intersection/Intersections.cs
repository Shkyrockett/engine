// <copyright file="Intersections.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Engine.Maths;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// A collection of methods for collecting the interactions of geometry.
    /// </summary>
    public static class Intersections
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="point0"></param>
        /// <param name="point1"></param>
        /// <returns></returns>
        public static List<Point2D> Intersection(this Point2D point0, Point2D point1)
            => (point0 == point1) ? new List<Point2D> { point0 } : new List<Point2D>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static List<Point2D> Intersection(this LineSegment segment, Point2D point)
            => (Intersectings.Intersects(segment, point)) ? new List<Point2D> { point } : new List<Point2D>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static List<Point2D> Intersection(this Point2D point, LineSegment segment)
            => (Intersectings.Intersects(segment, point)) ? new List<Point2D> { point } : new List<Point2D>();

        /// <summary>
        /// Find the intersection point between two lines.
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static List<Point2D> Intersection(this LineSegment s1, LineSegment s2)
            => LineSegmentLineSegment(s1.A.X, s1.A.Y, s1.B.X, s1.B.Y, s2.A.X, s2.A.Y, s2.B.X, s2.B.Y);

        /// <summary>
        /// Find the points of the intersection of a circle and a line segment.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> Intersection(this LineSegment s, Circle c)
            => CircleLineSegment(c.X, c.Y, c.Radius, s.A.X, s.A.Y, s.B.X, s.B.Y);

        /// <summary>
        /// Find the points of the intersection of a circle and a line segment.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> Intersection(this Circle c, LineSegment s)
            => CircleLineSegment(c.X, c.Y, c.Radius, s.A.X, s.A.Y, s.B.X, s.B.Y);

        /// <summary>
        /// Find the points of the intersection between two circles.
        /// </summary>
        /// <param name="c0"></param>
        /// <param name="c1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> Intersection(this Circle c0, Circle c1)
            => CircleCircle(c0.X, c0.Y, c0.Radius, c1.X, c1.Y, c1.Radius);

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> Intersection(this Ellipse e, LineSegment s)
            => EllipseLineSegment(e.X, e.Y, e.RX, e.RY, s.A.X, s.A.Y, e.Angle, s.B.X, s.B.Y);

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> Intersection(this LineSegment s, Ellipse e)
            => EllipseLineSegment(e.X, e.Y, e.RX, e.RY, s.A.X, s.A.Y, e.Angle, s.B.X, s.B.Y);

        /// <summary>
        /// Find the intersection point between two lines.
        /// </summary>
        /// <param name="x0">The x component of the first point of the first line.</param>
        /// <param name="y0">The y component of the first point of the first line.</param>
        /// <param name="x1">The x component of the second point of the first line.</param>
        /// <param name="y1">The y component of the second point of the first line.</param>
        /// <param name="x2">The x component of the first point of the second line.</param>
        /// <param name="y2">The y component of the first point of the second line.</param>
        /// <param name="x3">The x component of the second point of the second line.</param>
        /// <param name="y3">The y component of the second point of the second line.</param>
        /// <returns>Returns the point of intersection.</returns>
        /// <remarks>http://www.vb-helper.com/howto_segments_intersect.html</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> LineSegmentLineSegment(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
        {
            List<Point2D> result = new List<Point2D>();

            // Translate lines to origin.
            double u1 = (x1 - x0);
            double v1 = (y1 - y0);
            double u2 = (x3 - x2);
            double v2 = (y3 - y2);

            // Calculate the determinant of the coefficient matrix.
            double determinant = (v2 * u1) - (u2 * v1);

            // Check if the lines are parallel.
            if (Abs(determinant) < Epsilon)
                return result;

            // Find the index where the intersection point lies on the line.
            double s = ((x0 - x2) * v1 + (y2 - y0) * u1) / -determinant;
            double t = ((x2 - x0) * v2 + (y0 - y2) * u2) / determinant;

            // Check whether the point is on the segment.
            if ((t >= 0d) && (t <= 1d) && (s >= 0d) && (s <= 1d)) result.Add(new Point2D(x0 + t * u1, y0 + t * v1));

            return result;
        }

        /// <summary>
        /// Find the intersection point between two lines.
        /// </summary>
        /// <param name="x0">The x component of the first point of the first line.</param>
        /// <param name="y0">The y component of the first point of the first line.</param>
        /// <param name="x1">The x component of the second point of the first line.</param>
        /// <param name="y1">The y component of the second point of the first line.</param>
        /// <param name="x2">The x component of the first point of the second line.</param>
        /// <param name="y2">The y component of the first point of the second line.</param>
        /// <param name="x3">The x component of the second point of the second line.</param>
        /// <param name="y3">The y component of the second point of the second line.</param>
        /// <returns>Returns the point of intersection.</returns>
        /// <remarks>http://www.vb-helper.com/howto_segments_intersect.html</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> LineLine(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
        {
            List<Point2D> result = new List<Point2D>();

            // Translate lines to origin.
            double u1 = (x1 - x0);
            double v1 = (y1 - y0);
            double u2 = (x3 - x2);
            double v2 = (y3 - y2);

            // Calculate the determinant of the coefficient matrix.
            double determinant = (v2 * u1) - (u2 * v1);

            // Check if the lines are parallel.
            if (Abs(determinant) < Epsilon)
                return result;

            // Find the index where the intersection point lies on the line.
            double s = ((x0 - x2) * v1 + (y2 - y0) * u1) / -determinant;
            double t = ((x2 - x0) * v2 + (y0 - y2) * u2) / determinant;

            // Check whether the point is on the segment.
            result.Add(new Point2D(x0 + t * u1, y0 + t * v1));

            return result;
        }

        /// <summary>
        /// Find the points where the two circles intersect.
        /// </summary>
        /// <param name="cx0"></param>
        /// <param name="cy0"></param>
        /// <param name="radius0"></param>
        /// <param name="cx1"></param>
        /// <param name="cy1"></param>
        /// <param name="radius1"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/09/determine-where-two-circles-intersect-in-c/</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> CircleCircle(
            double cx0,
            double cy0,
            double radius0,
            double cx1,
            double cy1,
            double radius1)
        {
            List<Point2D> result = new List<Point2D>();

            // If either of the circles are empty, return no intersections.
            if ((radius0 == 0d) || (radius1 == 0d))
                return result;

            // Find the distance between the centers.
            double dx = cx0 - cx1;
            double dy = cy0 - cy1;
            double dist = Sqrt(dx * dx + dy * dy);

            // See how many solutions there are.
            if (dist > radius0 + radius1)
            {
                // No solutions, the circles are too far apart.
                // This would be a good point to return a null Lotus.
            }
            else if (dist < Abs(radius0 - radius1))
            {
                // No solutions, one circle contains the other.
                // This would be a good point to return a Lotus struct of th smaller of the circles.
            }
            else if ((Abs(dist) < Epsilon) && (Abs(radius0 - radius1) < Epsilon))
            {
                // No solutions, the circles coincide.
                // This would be a good point to return a Lotus struct of one of the circles.
            }
            else
            {
                // Find a and h.
                double a = (radius0 * radius0 - radius1 * radius1 + dist * dist) / (2 * dist);
                double h = Sqrt(radius0 * radius0 - a * a);

                // Find P2.
                double cx2 = cx0 + a * (cx1 - cx0) / dist;
                double cy2 = cy0 + a * (cy1 - cy0) / dist;

                // See if we have 1 or 2 solutions.
                if (Abs(dist - radius0 + radius1) < Epsilon)
                    // Get the points P3.
                    result.Add(new Point2D(
                    (cx2 + h * (cy1 - cy0) / dist),
                    (cy2 - h * (cx1 - cx0) / dist)));
                else
                {
                    // Get the points P3.
                    result.Add(new Point2D(
                    (cx2 + h * (cy1 - cy0) / dist),
                    (cy2 - h * (cx1 - cx0) / dist)));
                    result.Add(new Point2D(
                    (cx2 - h * (cy1 - cy0) / dist),
                    (cy2 + h * (cx1 - cx0) / dist)));
                }
            }

            return result;
        }

        /// <summary>
        /// Find the points of the intersection of a circle and a line segment.
        /// </summary>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="radius"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> CircleLineSegment(
            double cX, double cY,
            double radius,
            double x1, double y1,
            double x2, double y2)
        {
            List<Point2D> result = new List<Point2D>();

            // If the circle or line segment are empty, return no intersections.
            if ((radius == 0d) || ((x1 == x2) && (y1 == y2)))
                return result;

            double dx = x2 - x1;
            double dy = y2 - y1;

            // Calculate the quadratic parameters.
            double a = dx * dx + dy * dy;
            double b = 2 * (dx * (x1 - cX) + dy * (y1 - cY));
            double c = (x1 - cX) * (x1 - cX) + (y1 - cY) * (y1 - cY) - radius * radius;

            // Calculate the discriminant.
            double discriminant = b * b - 4 * a * c;

            if ((a <= Epsilon) || (discriminant < 0))
            {
                // No real solutions.
            }
            else if (discriminant == 0)
            {
                // One possible solution.
                double t = -b / (2 * a);

                // Add the points if they are between the end points of the line segment.
                if ((t >= 0d) && (t <= 1d)) result.Add(new Point2D(x1 + t * dx, y1 + t * dy));
            }
            else if (discriminant > 0)
            {
                // Two possible solutions.
                double t1 = ((-b + Sqrt(discriminant)) / (2 * a));
                double t2 = ((-b - Sqrt(discriminant)) / (2 * a));

                // Add the points if they are between the end points of the line segment.
                if ((t1 >= 0d) && (t1 <= 1d)) result.Add(new Point2D(x1 + t1 * dx, y1 + t1 * dy));
                if ((t2 >= 0d) && (t2 <= 1d)) result.Add(new Point2D(x1 + t2 * dx, y1 + t2 * dy));
            }

            return result;
        }

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://csharphelper.com/blog/2012/09/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> UnrotatedEllipseLineSegment(
            double cx, double cy,
            double rx, double ry,
            double x0, double y0,
            double x1, double y1)
        {
            List<Point2D> result = new List<Point2D>();

            // If the ellipse or line segment are empty, return no intersections.
            if ((rx == 0d) || (ry == 0d) ||
                ((x0 == x1) && (y0 == y1)))
                return result;

            // Translate the line to put the ellipse centered at the origin.
            double u1 = x0 - cx;
            double v1 = y0 - cy;
            double u2 = x1 - cx;
            double v2 = y1 - cy;

            // Calculate the quadratic parameters.
            double a = (u2 - u1) * (u2 - u1) / (rx * rx) + (v2 - v1) * (v2 - v1) / (ry * ry);
            double b = 2d * u1 * (u2 - u1) / (rx * rx) + 2d * v1 * (v2 - v1) / (ry * ry);
            double c = (u1 * u1) / (rx * rx) + (v1 * v1) / (ry * ry) - 1d;

            // Calculate the discriminant.
            double discriminant = b * b - 4d * a * c;

            if ((a <= Epsilon) || (discriminant < 0))
            {
                // No real solutions.
            }
            else if (discriminant == 0)
            {
                // One real possible solution.
                double t = 0.5d * -b / a;

                // Add the points if it is between the end points of the line segment.
                if ((t >= 0d) && (t <= 1d)) result.Add(new Point2D(u1 + (u2 - u1) * t + cx, v1 + (v2 - v1) * t + cy));
            }
            else if (discriminant > 0)
            {
                // Two real possible solutions.
                double t1 = (0.5d * (-b + Sqrt(discriminant)) / a);
                double t2 = (0.5d * (-b - Sqrt(discriminant)) / a);

                // Add the points if they are between the end points of the line segment.
                if ((t1 >= 0d) && (t1 <= 1d)) result.Add(new Point2D(u1 + (u2 - u1) * t1 + cx, v1 + (v2 - v1) * t1 + cy));
                if ((t2 >= 0d) && (t2 <= 1d)) result.Add(new Point2D(u1 + (u2 - u1) * t2 + cx, v1 + (v2 - v1) * t2 + cy));
            }

            return result;
        }

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="angle"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://csharphelper.com/blog/2012/09/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> EllipseLineSegment(
            double cx, double cy,
            double rx, double ry,
            double angle,
            double x0, double y0,
            double x1, double y1)
        {
            List<Point2D> result = new List<Point2D>();

            // If the ellipse or line segment are empty, return no intersections.
            if ((rx == 0d) || (ry == 0d) ||
                ((x0 == x1) && (y0 == y1)))
                return result;

            // Get the Sine and Cosine of the angle.
            double sinA = Sin(angle);
            double cosA = Cos(angle);

            // Translate the line to put the ellipse centered at the origin.
            double u1 = x0 - cx;
            double v1 = y0 - cy;
            double u2 = x1 - cx;
            double v2 = y1 - cy;

            // Apply Rotation Transform to line at the origin.
            double u1A = (0 + (u1 * cosA - v1 * sinA));
            double v1A = (0 + (u1 * sinA + v1 * cosA));
            double u2A = (0 + (u2 * cosA - v2 * sinA));
            double v2A = (0 + (u2 * sinA + v2 * cosA));

            // Calculate the quadratic parameters.
            double a = (u2A - u1A) * (u2A - u1A) / (rx * rx) + (v2A - v1A) * (v2A - v1A) / (ry * ry);
            double b = 2d * u1A * (u2A - u1A) / (rx * rx) + 2d * v1A * (v2A - v1A) / (ry * ry);
            double c = (u1A * u1A) / (rx * rx) + (v1A * v1A) / (ry * ry) - 1d;

            // Calculate the discriminant.
            double discriminant = b * b - 4d * a * c;

            if ((a <= Epsilon) || (discriminant < 0))
            {
                // No real solutions.
            }
            else if (discriminant == 0)
            {
                // One real possible solution.
                double t = 0.5d * -b / a;

                // Add the point if it is between the end points of the line segment.
                if ((t >= 0d) && (t <= 1d)) result.Add(new Point2D(u1 + (u2 - u1) * t + cx, v1 + (v2 - v1) * t + cy));
            }
            else if (discriminant > 0)
            {
                // Two real possible solutions.
                double t1 = (0.5d * (-b + Sqrt(discriminant)) / a);
                double t2 = (0.5d * (-b - Sqrt(discriminant)) / a);

                // Add the points if they are between the end points of the line segment.
                if ((t1 >= 0d) && (t1 <= 1d)) result.Add(new Point2D(u1 + (u2 - u1) * t1 + cx, v1 + (v2 - v1) * t1 + cy));
                if ((t2 >= 0d) && (t2 <= 1d)) result.Add(new Point2D(u1 + (u2 - u1) * t2 + cx, v1 + (v2 - v1) * t2 + cy));

                // ToDo: Figure out why the results are weird between 30 degrees and 5 degrees.
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p0x"></param>
        /// <param name="p0y"></param>
        /// <param name="p1x"></param>
        /// <param name="p1y"></param>
        /// <param name="p2x"></param>
        /// <param name="p2y"></param>
        /// <param name="p3x"></param>
        /// <param name="p3y"></param>
        /// <param name="l0x"></param>
        /// <param name="l0y"></param>
        /// <param name="l1x"></param>
        /// <param name="l1y"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://www.particleincell.com/2013/cubic-line-intersection/
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> CubicBezierLineSegment(
            double p0x, double p0y,
            double p1x, double p1y,
            double p2x, double p2y,
            double p3x, double p3y,
            double l0x, double l0y,
            double l1x, double l1y)
        {
            // ToDo: Figure out why this can't handle intersection with horizontal lines.
            var I = new List<Point2D>();

            var A = l1y - l0y;      //A=y2-y1
            var B = l0x - l1x;      //B=x1-x2
            var C = l0x * (l0y - l1y) + l0y * (l1x - l0x);  //C=x1*(y1-y2)+y1*(x2-x1)

            var bx = BezierCoefficients(p0x, p1x, p2x, p3x);
            var by = BezierCoefficients(p0y, p1y, p2y, p3y);

            var r = CubicRoots(
                A * bx.A + B * by.A,    /*t^3*/
                A * bx.B + B * by.B,    /*t^2*/
                A * bx.C + B * by.C,    /*t*/
                A * bx.D + B * by.D + C /*1*/
                );

            /*verify the roots are in bounds of the linear segment*/
            for (var i = 0; i < 3; i++)
            {
                double t = r[i];

                double x = bx.A * t * t * t + bx.B * t * t + bx.C * t + bx.D;
                double y = by.A * t * t * t + by.B * t * t + by.C * t + by.D;

                /*above is intersection point assuming infinitely long line segment,
                  make sure we are also in bounds of the line*/
                double m;
                if ((l1x - l0x) != 0)           /*if not vertical line*/
                    m = (x - l0x) / (l1x - l0x);
                else
                    m = (y - l0y) / (l1y - l0y);

                /*in bounds?*/
                if (t < 0 || t > 1d || m < 0 || m > 1d)
                {
                    x = 0;// -100;  /*move off screen*/
                    y = 0;// -100;
                }
                else
                    /*intersection point*/
                    I.Add(new Point2D(x, y));
            }
            return I;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        /// <remarks>
        /// based on http://abecedarical.com/javascript/script_exact_cubic.html
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double[] CubicRoots(double a, double b, double c, double d)
        {
            // The horizontal line issue seems to be somewhere in here.
            var A = b / a;
            var B = c / a;
            var C = d / a;

            double S, T, Im;

            double Q = (3 * B - Pow(A, 2)) / 9;
            double R = (9 * A * B - 27 * C - 2 * Pow(A, 3)) / 54;
            double D = Pow(Q, 3) + Pow(R, 2);    // polynomial discriminant

            var t = new double[3];

            if (D >= 0)                                 // complex or duplicate roots
            {
                S = Sign(R + Sqrt(D)) * Pow(Abs(R + Sqrt(D)), (1 / 3));
                T = Sign(R - Sqrt(D)) * Pow(Abs(R - Sqrt(D)), (1 / 3));

                t[0] = -A / 3 + (S + T);                    // real root
                t[1] = -A / 3 - (S + T) / 2;                  // real part of complex root
                t[2] = -A / 3 - (S + T) / 2;                  // real part of complex root
                Im = Abs(Sqrt(3) * (S - T) / 2);    // complex part of root pair   

                /*discard complex roots*/
                if (Im != 0)
                {
                    t[1] = -1;
                    t[2] = -1;
                }

            }
            else                                          // distinct real roots
            {
                var th = Acos(R / Sqrt(-Pow(Q, 3)));

                t[0] = 2 * Sqrt(-Q) * Cos(th / 3) - A / 3;
                t[1] = 2 * Sqrt(-Q) * Cos((th + Tau) / 3) - A / 3;
                t[2] = 2 * Sqrt(-Q) * Cos((th + 4 * PI) / 3) - A / 3;
                Im = 0.0;
            }

            /*discard out of spec roots*/
            for (var i = 0; i < 3; i++)
                if (t[i] < 0 || t[i] > 1.0) t[i] = -1;

            /*sort but place -1 at the end*/
            t = SortSpecial(t);

            //Console.log(t[0] + " " + t[1] + " " + t[2]);
            return t;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double[] SortSpecial(double[] a)
        {
            bool flip;
            double temp;

            do
            {
                flip = false;
                for (var i = 0; i < a.Length - 1; i++)
                {
                    if ((a[i + 1] >= 0 && a[i] > a[i + 1]) ||
                        (a[i] < 0 && a[i + 1] >= 0))
                    {
                        flip = true;
                        temp = a[i];
                        a[i] = a[i + 1];
                        a[i + 1] = temp;

                    }
                }
            } while (flip);
            return a;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1X"></param>
        /// <param name="p1Y"></param>
        /// <param name="p2X"></param>
        /// <param name="p2Y"></param>
        /// <param name="p3X"></param>
        /// <param name="p3Y"></param>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/27664298/calculating-intersection-point-of-quadratic-bezier-curve
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> QuadraticBezierLineSegment(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            double a1X, double a1Y,
            double a2X, double a2Y)
        {
            List<Point2D> intersections = new List<Point2D>();

            // inverse line normal
            var normal = new Point2D(a1Y - a2Y, a2X - a1X);

            // Q-coefficients
            var c2 = new Point2D(p1X + p2X * -2 + p3X, p1Y + p2Y * -2 + p3Y);
            var c1 = new Point2D(p1X * -2 + p2X * 2, p1Y * -2 + p2Y * 2);
            var c0 = new Point2D(p1X, p1Y);

            // Transform to line 
            var coefficient = a1X * a2Y - a2X * a1Y;
            var a = normal.X * c2.X + normal.Y * c2.Y;
            var b = (normal.X * c1.X + normal.Y * c1.Y) / a;
            var c = (normal.X * c0.X + normal.Y * c0.Y + coefficient) / a;

            // solve the roots
            List<double> roots = new List<double>();
            var d = b * b - 4 * c;
            if (d > 0)
            {
                var e = Sqrt(d);
                roots.Add((-b + Sqrt(d)) / 2);
                roots.Add((-b - Sqrt(d)) / 2);
            }
            else if (d == 0)
            {
                roots.Add(-b / 2);
            }

            // calc the solution points
            for (var i = 0; i < roots.Count; i++)
            {
                var minX = Min(a1X, a2X);
                var minY = Min(a1Y, a2Y);
                var maxX = Max(a1X, a2X);
                var maxY = Max(a1Y, a2Y);
                var t = roots[i];
                if (t >= 0 && t <= 1)
                {
                    // possible point -- pending bounds check
                    var point = new Point2D(
                        Interpolaters.Linear(Interpolaters.Linear(p1X, p2X, t), Interpolaters.Linear(p2X, p3X, t), t),
                        Interpolaters.Linear(Interpolaters.Linear(p1Y, p2Y, t), Interpolaters.Linear(p2Y, p3Y, t), t));
                    var x = point.X;
                    var y = point.Y;
                    // bounds checks
                    if (a1X == a2X && y >= minY && y <= maxY)
                    {
                        // vertical line
                        intersections.Add(point);
                    }
                    else if (a1Y == a2Y && x >= minX && x <= maxX)
                    {
                        // horizontal line
                        intersections.Add(point);
                    }
                    else if (x >= minX && y >= minY && x <= maxX && y <= maxY)
                    {
                        // line passed bounds check
                        intersections.Add(point);
                    }
                }
            }
            return intersections;
        }
    }
}

