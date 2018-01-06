// <copyright file="Conversions.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static System.Math;
using static Engine.Maths;

namespace Engine
{
    /// <summary>
    /// Conversion methods to convert from one shape to another.
    /// </summary>
    public static class Conversions
    {
        #region Conversion Extension Methods

        /// <summary>
        /// Converts a circle to an ellipse.
        /// </summary>
        /// <param name="circle">The circle.</param>
        /// <returns>Returns an ellipse with the values derived from the circle.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ellipse ToEllipse(this Circle circle)
            => CircleToEllipse(circle.X, circle.Y, circle.Radius);

        /// <summary>
        /// Convert a circle to a circular arc.
        /// </summary>
        /// <param name="circle">The circle.</param>
        /// <returns>Returns a circular arc from the parameters of a circle.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CircularArc ToCircularArc(this Circle circle)
            => CircleToCircularArc(circle.X, circle.Y, circle.Radius);

        /// <summary>
        /// Generate an array of CubicBeziers, representing an elliptical arc centered at (x, y)
        /// with width w and height h. The arc starts at subtended angle start and stops at subtended angle startAngle + sweepAngle.
        /// Arcs greater than 90° are split into multiple arcs.
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns>Returns a list of Cubic Bezier curves that approximate an elliptical arc.</returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// Code ported from: https://www.khanacademy.org/computer-programming/e/6221186997551104
        /// Math from: http://www.spaceroots.org/documents/ellipse/node22.html
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<CubicBezier> ToCubicBeziers(this EllipticalArc ellipse)
            => EllipticalArcToCubicBeziers(ellipse.X, ellipse.Y, ellipse.RX, ellipse.RY, ellipse.StartAngle, ellipse.SweepAngle);

        /// <summary>
        /// Converts a line segment to a quadratic Bezier curve.
        /// </summary>
        /// <param name="segment">The line segment</param>
        /// <returns>Returns a Quadratic Bezier from a line segment.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuadraticBezier ToQuadraticBezier(this LineSegment segment)
            => LineSegmentToQuadraticBezier(segment.AX, segment.AY, segment.BX, segment.BY);

        /// <summary>
        /// Converts a line segment to a Cubic Bezier.
        /// </summary>
        /// <param name="segment">The line segment.</param>
        /// <returns>Returns a Cubic Bezier from a line segment.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CubicBezier ToCubicBezier(this LineSegment segment)
            => LineSegmentToCubicBezier(segment.AX, segment.AY, segment.BX, segment.BY);

        /// <summary>
        /// Converts a Quadratic Bezier curve to a Cubic Bezier curve. 
        /// </summary>
        /// <param name="curve">The Quadratic Bezier curve.</param>
        /// <returns>Returns a Cubic Bezier curve from a Quadratic Bezier curve.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CubicBezier ToCubicBezier(this QuadraticBezier curve)
            => QuadraticBezierToCubicBezier(curve.A, curve.B, curve.C);

        #endregion

        #region Conversion Implementations

        /// <summary>
        /// Converts a Circle to a Circular arc.
        /// </summary>
        /// <param name="x">The x-component of the center point.</param>
        /// <param name="y">The y-component of the center point.</param>
        /// <param name="r">The radius of circle.</param>
        /// <returns>Returns a circular arc from a circle.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CircularArc CircleToCircularArc(
            double x, double y,
            double r)
            => new CircularArc(x, y, r, 0, Tau);

        /// <summary>
        /// Converts a Circle to an ellipse.
        /// </summary>
        /// <param name="x">The x-component of the center point.</param>
        /// <param name="y">The y-component of the center point.</param>
        /// <param name="r">The radius of the circle.</param>
        /// <returns>Returns an ellipse from a circle.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ellipse CircleToEllipse(
            double x, double y,
            double r)
            => new Ellipse(x, y, r, r, 0);

        /// <summary>
        /// Generate an array of CubicBeziers, representing an elliptical arc centered at (x, y)
        /// with width w and height h. The arc starts at subtended angle start and stops at subtended angle startAngle + sweepAngle.
        /// Arcs greater than 90° are split into multiple arcs.
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx">X radius</param>
        /// <param name="ry">Y radius</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        /// <returns>Returns a list of Cubic Bezier curves that approximate a circular arc.</returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// Code ported from: https://www.khanacademy.org/computer-programming/e/6221186997551104
        /// Math from: http://www.spaceroots.org/documents/ellipse/node22.html
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<CubicBezier> EllipticalArcToCubicBeziers(
            double cx, double cy,
            double rx, double ry,
            double startAngle, double sweepAngle)
        {
            /* Definition of ellipse: x²/a² + y²/b² = 1 */
            var stop = startAngle + sweepAngle;
            startAngle += SubtendedToParametric(startAngle, rx, ry);
            sweepAngle = stop + SubtendedToParametric(stop, rx, ry) - startAngle;
            var segs = Ceiling(Abs(sweepAngle) / Right);
            var theta = sweepAngle / segs;  /* arc size of each segment */
            var tanT2 = Tan(theta * OneHalf);
            var alpha = Sin(theta) * (Sqrt(4 + 3 * tanT2 * tanT2) - 1) * OneThird;
            var sine = Sin(startAngle);
            var cosine = Cos(startAngle);
            var sx = cx + rx * cosine;  /* start pt X coordinate */
            var sy = cy + ry * sine;  /* start pt Y coordinate */
            var dx = -rx * sine;  /* dx/dθ at start pt */
            var dy = ry * cosine;  /* dy/dθ at start pt */

            var beziers = new List<CubicBezier>();  /* the results */

            double ex = 0;
            double ey = 0;  /* end pt coordinates */
            for (var s = 0; s < segs; s++, sx = ex, sy = ey)
            {
                var c1x = sx + alpha * dx;  /* 1st control pt X */
                var c1y = sy + alpha * dy; /* 1st control pt Y */

                cosine = Cos(startAngle += theta);
                sine = Sin(startAngle);
                ex = cx + rx * cosine;  /* end pt X coordinate */
                ey = cy + ry * sine;  /* end py Y coordinate */
                dx = -rx * sine;  /* dx/dθ at end pt */
                dy = ry * cosine;  /* dy/dθ at end pt */
                var c2x = ex - alpha * dx;  /* 2nd control pt X */
                var c2y = ey - alpha * dy;  /* 2nd control pt Y */

                beziers.Add(new CubicBezier(sx, sy, c1x, c1y, c2x, c2y, ex, ey));
            }
            return beziers;
        }

        /// <summary>
        /// Converts a line segment to a Quadratic Bezier curve.
        /// </summary>
        /// <param name="a">The starting point.</param>
        /// <param name="b">The end point.</param>
        /// <returns>Returns a Quadratic Bezier curve with the properties of the line segment.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuadraticBezier LineSegmentToQuadraticBezier(
            Point2D a,
            Point2D b)
            => LineSegmentToQuadraticBezier(a.X, a.Y, b.X, b.Y);

        /// <summary>
        /// Converts a line segment to a Quadratic Bezier curve.
        /// </summary>
        /// <param name="x0">The x-component of the first point of a line segment.</param>
        /// <param name="y0">The y-component of the first point of a line segment.</param>
        /// <param name="x1">The x-component of the second point of a line segment.</param>
        /// <param name="y1">The y-component of the second point of a line segment.</param>
        /// <returns>Returns a Quadratic Bezier with the properties of a line segment.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuadraticBezier LineSegmentToQuadraticBezier(
            double x0, double y0,
            double x1, double y1)
            => new QuadraticBezier(new Point2D(x0, y0), Lerp(x0, y0, x1, y1, OneHalf), new Point2D(x1, y1));

        /// <summary>
        /// Converts a Line segment to a Cubic Bezier curve.
        /// </summary>
        /// <param name="a">The starting point of the line segment.</param>
        /// <param name="b">The ending point of the line segment.</param>
        /// <returns>Returns a Cubic Bezier with the properties of a line segment.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CubicBezier LineSegmentToCubicBezier(
            Point2D a,
            Point2D b)
            => LineSegmentToCubicBezier(a.X, a.Y, b.X, b.Y);

        /// <summary>
        /// Converts a Line segment to a Cubic Bezier curve.
        /// </summary>
        /// <param name="x0">The x-component of the starting point.</param>
        /// <param name="y0">The y-component of the starting point.</param>
        /// <param name="x1">The x-component of the end point.</param>
        /// <param name="y1">The y-component of the end point.</param>
        /// <returns>Returns a Cubic Bezier curve from the properties of a line segment.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CubicBezier LineSegmentToCubicBezier(
            double x0, double y0,
            double x1, double y1)
            => new CubicBezier(new Point2D(x0, y0), Lerp(x0, y0, x1, y1, OneThird), Lerp(x0, y0, x1, y1, TwoThirds), new Point2D(x1, y1));

        /// <summary>
        /// Converts a Quadratic Bezier to a Cubic Bezier.
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <param name="c">The third point.</param>
        /// <returns>Returns a Cubic Bezier from a Quadratic Bezier.</returns>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D[] QuadraticBezierToCubicBezierList(
            Point2D a,
            Point2D b,
            Point2D c)
            => new Point2D[]
            {
                a,
                new Point2D(a.X + TwoThirds * (b.X - a.X), a.Y + TwoThirds * (b.Y - a.Y)),
                new Point2D(c.X + TwoThirds * (b.X - c.X), c.Y + TwoThirds * (b.Y - c.Y)),
                c
            };

        /// <summary>
        /// Raises a <see cref="QuadraticBezier"/> to a <see cref="CubicBezier"/>.
        /// </summary>
        /// <param name="a">The starting point of the Quadratic Bezier curve.</param>
        /// <param name="b">The handle of the Quadratic Bezier curve.</param>
        /// <param name="c">The end point of the Quadratic Bezier curve.</param>
        /// <returns>Returns a Cubic Bezier curve from the Quadratic Bezier curve.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CubicBezier QuadraticBezierToCubicBezier(
            Point2D a,
            Point2D b,
            Point2D c)
            => new CubicBezier(
                a.X, a.Y,
                a.X + TwoThirds * (b.X - a.X), a.Y + TwoThirds * (b.Y - a.Y),
                c.X + TwoThirds * (b.X - c.X), c.Y + TwoThirds * (b.Y - c.Y),
                c.X, c.Y
            );

        /// <summary>
        /// Raise a Quadratic Bezier to a Cubic Bezier.
        /// </summary>
        /// <param name="aX">The x-component of the starting point.</param>
        /// <param name="aY">The y-component of the starting point.</param>
        /// <param name="bX">The x-component of the handle.</param>
        /// <param name="bY">The y-component of the handle.</param>
        /// <param name="cX">The x-component of the end point.</param>
        /// <param name="cY">The y-component of the end point.</param>
        /// <returns>Returns Quadratic Bezier curve from a cubic curve.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<(double X, double Y)> QuadraticBezierToCubicBezier(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY)
            => new List<(double X, double Y)>
            {
                (aX, aY),
                (aX + TwoThirds * (bX - aX), aY + TwoThirds * (bY - aY)),
                (cX + TwoThirds * (bX - cX), cY + TwoThirds * (bY - cY)),
                (cX, cY)
            };

        /// <summary>
        /// Raise a Quadratic Bezier to a Cubic Bezier.
        /// </summary>
        /// <param name="aX">The x-component of the starting point.</param>
        /// <param name="aY">The y-component of the starting point.</param>
        /// <param name="bX">The x-component of the handle.</param>
        /// <param name="bY">The y-component of the handle.</param>
        /// <param name="cX">The x-component of the end point.</param>
        /// <param name="cY">The y-component of the end point.</param>
        /// <returns>Returns Quadratic Bezier curve from a cubic curve.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double aX, double aY, double bX, double bY, double cX, double cY, double dX, double dY) QuadraticBezierToCubicBezierTuple(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY)
            => (aX, aY,
                aX + TwoThirds * (bX - aX), aY + TwoThirds * (bY - aY),
                cX + TwoThirds * (bX - cX), cY + TwoThirds * (bY - cY),
                cX, cY);

        /// <summary>
        /// Converts a list of points on a Catmull Rom Curve to a list of Cubic Bezier curves.
        /// </summary>
        /// <param name="points">The list of points.</param>
        /// <returns>Returns a list of Cubic Bezier curves from a list of points on a Catmull Rom curve.</returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/ariutta/catmullrom2bezier/blob/master/catmullrom2bezier.js
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<CubicBezier> CatmullRomToBezier(
            Point2D[] points)
        {
            var d = new List<CubicBezier>();
            for (int i = 0, iLen = points.Length; iLen - 1 > i; i++)
            {
                var p = new List<(double x, double y)>();
                if (0 == i)
                {
                    p.Add((points[i].X, points[i].Y));
                    p.Add((points[i].X, points[i].Y));
                    p.Add((points[i + 1].X, points[i + 1].Y));
                    p.Add((points[i + 2].X, points[i + 2].Y));
                }
                else if (iLen - 4 == i)
                {
                    p.Add((points[i - 1].X, points[i - 1].Y));
                    p.Add((points[i].X, points[i].Y));
                    p.Add((points[i + 1].X, points[i + 1].Y));
                    p.Add((points[i + 2].X, points[i + 2].Y));
                }
                else
                {
                    p.Add((points[i - 1].X, points[i - 1].Y));
                    p.Add((points[i].X, points[i].Y));
                    p.Add((points[i + 1].X, points[i + 1].Y));
                    p.Add((points[i + 2].X, points[i + 2].Y));
                }

                // Catmull-Rom to Cubic Bezier conversion matrix 
                //    0       1       0       0
                //  -1/6      1      1/6      0
                //    0      1/6      1     -1/6
                //    0       0       1       0
                var bp = new List<(double x, double y)>
                {
                    (p[1].x,  p[1].y),
                    (((-p[0].x + 6 * p[1].x + p[2].x) / 6),  ((-p[0].y + 6 * p[1].y + p[2].y) / 6)),
                    (((p[1].x + 6 * p[2].x - p[3].x) / 6),  ((p[1].y + 6 * p[2].y - p[3].y) / 6)),
                    (p[2].x,  p[2].y)
                };
                d.Add(new CubicBezier(bp[1].x, bp[1].y, bp[2].x, bp[2].y, bp[3].x, bp[3].y));
            }

            return d;
        }

        #endregion
    }
}
