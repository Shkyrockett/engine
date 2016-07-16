// <copyright file="Intersections.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using static Engine.Maths;
using static System.Math;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    public static class Perimeters
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="sweepAngle"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ArcLength(double r, double sweepAngle)
            => 2d * PI * r * -sweepAngle;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CircleCircumference(double r)
            => 2d * r * PI;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <returns></returns>
        /// <remarks>http://steve.hollasch.net/cgindex/curves/cbezarclen.html</remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CubicBezierArcLength(Point2D p1, Point2D p2, Point2D p3, Point2D p4)
        {
            var k1 = (Point2D)(-p1 + 3d * (p2 - p3) + p4);
            Point2D k2 = 3d * (p1 + p3) - 6d * p2;
            var k3 = (Point2D)(3d * (p2 - p1));
            Point2D k4 = p1;

            double q1 = 9d * (Sqrt(Abs(k1.X)) + Sqrt((Abs(k1.Y))));
            double q2 = 12d * (k1.X * k2.X + k1.Y * k2.Y);
            double q3 = 3d * (k1.X * k3.X + k1.Y * k3.Y) + 4d * (Sqrt(Abs(k2.X)) + Sqrt(Abs(k2.Y)));
            double q4 = 4d * (k2.X * k3.X + k2.Y * k3.Y);
            double q5 = Sqrt(Abs(k3.X)) + Sqrt(Abs(k3.Y));

            // Approximation algorithm based on Simpson. 
            double a = 0d;
            double b = 1d;
            int n_limit = 1024;
            double TOLERANCE = 0.001d;

            int n = 1;

            double multiplier = (b - a) / 6d;
            double endsum = CubicBezierArcLengthHelper(ref q1, ref q2, ref q3, ref q4, ref q5, a) + CubicBezierArcLengthHelper(ref q1, ref q2, ref q3, ref q4, ref q5, b);
            double interval = (b - a) / 2d;
            double asum = 0d;
            double bsum = CubicBezierArcLengthHelper(ref q1, ref q2, ref q3, ref q4, ref q5, a + interval);
            double est1 = multiplier * (endsum + 2d * asum + 4d * bsum);
            double est0 = 2d * est1;

            while (n < n_limit && (Abs(est1) > 0 && Abs((est1 - est0) / est1) > TOLERANCE))
            {
                n *= 2;
                multiplier /= 2d;
                interval /= 2d;
                asum += bsum;
                bsum = 0;
                est0 = est1;
                double interval_div_2n = interval / (2d * n);

                for (int i = 1; i < 2 * n; i += 2)
                {
                    double t = a + i * interval_div_2n;
                    bsum += CubicBezierArcLengthHelper(ref q1, ref q2, ref q3, ref q4, ref q5, t);
                }

                est1 = multiplier * (endsum + 2d * asum + 4d * bsum);
            }

            return est1 * 10d;
        }

        /// <summary>
        /// Bezier Arc Length Function
        /// </summary>
        /// <param name="t"></param>
        /// <param name="q1"></param>
        /// <param name="q2"></param>
        /// <param name="q3"></param>
        /// <param name="q4"></param>
        /// <param name="q5"></param>
        /// <returns></returns>
        /// <remarks>http://steve.hollasch.net/cgindex/curves/cbezarclen.html</remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double CubicBezierArcLengthHelper(ref double q1, ref double q2, ref double q3, ref double q4, ref double q5, double t)
        {
            double result = q5 + t * (q4 + t * (q3 + t * (q2 + t * q1)));
            result = Sqrt(Abs(result));
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math05a/EllipseCircumference05.html
        /// </remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double EllipsePerimeter(double a, double b)
            => 4d * ((PI * a * b) + ((a - b) * (a - b))) / (a + b);

        /// <summary>
        /// Closed-form solution to elliptic integral for arc length.
        /// </summary>
        /// <param name="pointA">The starting node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="pointB">The middle tangent control node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="pointC">The closing node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <returns></returns>
        /// <remarks>
        /// https://algorithmist.wordpress.com/2009/01/05/quadratic-bezier-arc-length/
        /// </remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuadraticBezierArcLengthByIntegral(Point2D pointA, Point2D pointB, Point2D pointC)
        {
            double ax = pointA.X - 2d * pointB.X + pointC.X;
            double ay = pointA.Y - 2d * pointB.Y + pointC.Y;
            double bx = 2d * pointB.X - 2d * pointA.X;
            double by = 2d * pointB.Y - 2d * pointA.Y;

            double a = 4d * (ax * ax + ay * ay);
            double b = 4d * (ax * bx + ay * by);
            double c = bx * bx + by * by;

            double abc = 2d * Sqrt(a + b + c);
            double a2 = Sqrt(a);
            double a32 = 2d * a * a2;
            double c2 = 2d * Sqrt(c);
            double ba = b / a2;

            return (a32 * abc + a2 * b * (abc - c2) + (4d * c * a - b * b) * Log((2d * a2 + ba + abc) / (ba + c2))) / (4d * a32);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double PolygonPerimeter(IEnumerable polygon)
        {
            List<Point2D> points = (polygon as List<Point2D>);
            return points.Count > 0 ? points.Zip(points.Skip(1), Primitives.Distance).Sum() + points[0].Distance(points[points.Count - 1]) : 0d;
        }
    }
}
