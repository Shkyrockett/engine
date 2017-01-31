﻿// <copyright file="Distances.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using static Engine.Maths;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class Distances
    {
        /// <summary>
        /// Distance between two points.
        /// </summary>
        /// <param name="x1">First X component.</param>
        /// <param name="y1">First Y component.</param>
        /// <param name="x2">Second X component.</param>
        /// <param name="y2">Second Y component.</param>
        /// <returns>The distance between two points.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(
            double x1, double y1,
            double x2, double y2)
            => Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));

        /// <summary>
        /// Distance between two points.
        /// </summary>
        /// <param name="x1">First X component.</param>
        /// <param name="y1">First Y component.</param>
        /// <param name="z1">First Z component.</param>
        /// <param name="x2">Second X component.</param>
        /// <param name="y2">Second Y component.</param>
        /// <param name="z2">Second Z component.</param>
        /// <returns>The distance between two points.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1) + (z2 - z1) * (z2 - z1));

        /// <summary>
        /// The square of the distance between two points.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SquareDistance(
            double x1, double y1,
            double x2, double y2)
            => ((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));

        /// <summary>
        /// Returns the Length of a lineSeg.
        /// </summary>
        /// <param name="x1">Horizontal Component of Point Starting Point</param>
        /// <param name="y1">Vertical Component of Point Starting Point</param>
        /// <param name="x2">Horizontal Component of Ending Point</param>
        /// <param name="y2">Vertical Component of Ending Point</param>
        /// <returns>Returns the Length of a lineSeg.</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(int x1, int y1, int x2, int y2)
            => Distance(x1, y1, x2, y2);

        /// <summary>
        /// Returns the Length of a lineSeg.
        /// </summary>
        /// <param name="x1">Horizontal Component of Point Starting Point</param>
        /// <param name="y1">Vertical Component of Point Starting Point</param>
        /// <param name="x2">Horizontal Component of Ending Point</param>
        /// <param name="y2">Vertical Component of Ending Point</param>
        /// <returns>Returns the Length of a lineSeg.</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(float x1, float y1, float x2, float y2)
            => Distance(x1, y1, x2, y2);

        /// <summary>
        /// Returns the Length of a lineSeg.
        /// </summary>
        /// <param name="x1">Horizontal Component of Point Starting Point</param>
        /// <param name="y1">Vertical Component of Point Starting Point</param>
        /// <param name="x2">Horizontal Component of Ending Point</param>
        /// <param name="y2">Vertical Component of Ending Point</param>
        /// <returns>Returns the Length of a lineSeg.</returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Length(double x1, double y1, double x2, double y2)
            => Distance(x1, y1, x2, y2);

        /// <summary>
        /// Find the square of the distance of a point from a line.
        /// </summary>
        /// <param name="lx2">The x component of the first point on the line.</param>
        /// <param name="ly2">The y component of the first point on the line.</param>
        /// <param name="lx3">The x component of the second point on the line.</param>
        /// <param name="ly3">The y component of the second point on the line.</param>
        /// <param name="x1">The x component of the Point.</param>
        /// <param name="y1">The y component of the Point.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SquareDistanceToLine(
            double lx2, double ly2,
            double lx3, double ly3,
            double x1, double y1)
        {
            double A = ly2 - ly3;
            double B = lx3 - lx2;
            double C = (A * x1 + B * y1) - (A * lx2 + B * ly2);
            return (C * C) / (A * A + B * B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="sweepAngle"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ArcLength(double r, double sweepAngle)
            => 2d * PI * r * -sweepAngle;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CircleCircumference(double r)
            => 2d * r * PI;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.ebyte.it/library/docs/math05a/EllipseCircumference05.html
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double EllipsePerimeter(double a, double b)
            => 4d * ((PI * a * b) + ((a - b) * (a - b))) / (a + b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start">Start.</param>
        /// <param name="end">End.</param>
        /// <param name="startAngle"></param>
        /// <param name="endAngle">Sweep Angle.</param>
        /// <returns></returns>
        public static double EllipticalArcPerimeter(Point2D start, Point2D end, double startAngle, double endAngle)
            => EllipticalArcPerimeter(start.X, start.Y, end.X, end.Y, startAngle, endAngle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        /// <param name="startAngle"></param>
        /// <param name="endAngle">Sweep Angle.</param>
        /// <returns></returns>
        /// <remarks>
        /// http://www.iosrjournals.org/iosr-jm/papers/Vol3-issue2/B0320813.pdf
        /// http://mathforum.org/kb/servlet/JiveServlet/download/130-2391290-7852023-766514/PERIMETER%20OF%20THE%20ELLIPTICAL%20ARC%20A%20GEOMETRIC%20METHOD.pdf
        /// </remarks>
        public static double EllipticalArcPerimeter(double startX, double startY, double endX, double endY, double startAngle, double endAngle)
            => (/*ChordLength*/(Sqrt(Abs(endX - startX) * Abs(endX - startX) + Abs(endY - startY) * Abs(endY - startY)))
            / /*Middle Angle*/(2 * Sin(OneHalf * (startAngle - endAngle))))
            * (startAngle - endAngle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ax"></param>
        /// <param name="ay"></param>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <returns></returns>
        /// <remarks>http://steve.hollasch.net/cgindex/curves/cbezarclen.html</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CubicBezierArcLength(
            double ax, double ay,
            double bx, double by,
            double cx, double cy,
            double dx, double dy)
        {
            var k1 = new Point2D(-ax + 3d * (bx - cx) + dx, -ay + 3d * (by - cy) + dy);
            var k2 = new Point2D(3d * (ax + cx) - 6d * bx, 3d * (ay + cy) - 6d * by);
            var k3 = new Point2D(3d * (bx - ax), 3d * (by - ax));
            var k4 = new Point2D(ax, ay);

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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double CubicBezierArcLengthHelper(ref double q1, ref double q2, ref double q3, ref double q4, ref double q5, double t)
        {
            double result = q5 + t * (q4 + t * (q3 + t * (q2 + t * q1)));
            result = Sqrt(Abs(result));
            return result;
        }

        /// <summary>
        /// Closed-form solution to elliptic integral for arc length.
        /// </summary>
        /// <param name="ax">The starting x-coordinate for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="ay">The starting y-coordinate for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="bx">The middle x-coordinate for the tangent control node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="by">The middle y-coordinate for the tangent control node for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="cx">The closing x-coordinate for the <see cref="QuadraticBezier"/> curve.</param>
        /// <param name="cy">The closing y-coordinate for the <see cref="QuadraticBezier"/> curve.</param>
        /// <returns></returns>
        /// <remarks>
        /// https://algorithmist.wordpress.com/2009/01/05/quadratic-bezier-arc-length/
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuadraticBezierArcLengthByIntegral(
            double ax, double ay,
            double bx, double by,
            double cx, double cy)
        {
            double _ax = ax - 2d * bx + cx;
            double _ay = ay - 2d * by + cy;
            double _bx = 2d * bx - 2d * ax;
            double _by = 2d * by - 2d * ay;

            double a = 4d * (_ax * _ax + _ay * _ay);
            double b = 4d * (_ax * _bx + _ay * _by);
            double c = _bx * _bx + _by * _by;

            double abc = 2d * Sqrt(a + b + c);
            double a2 = Sqrt(a);
            double a32 = 2d * a * a2;
            double c2 = 2d * Sqrt(c);
            double ba = b / a2;

            return (a32 * abc + a2 * b * (abc - c2) + (4d * c * a - b * b) * Log((2d * a2 + ba + abc) / (ba + c2))) / (4d * a32);
        }
    }
}
