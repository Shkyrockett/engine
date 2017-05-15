// <copyright file="Conversions.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
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

namespace Engine._Preview
{
    /// <summary>
    /// 
    /// </summary>
    public static class Conversions
    {
        #region Conversion Extension methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ellipse ToEllipse(this Circle c)
            => CircleToEllipse(c.X, c.Y, c.Radius);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CircularArc ToCircularArc(this Circle c)
            => CircleToCircularArc(c.X, c.Y, c.Radius);

        /// <summary>
        /// Generate an array of CubicBeziers, representing an elliptical arc centered at (x, y)
        /// with width w and height h. The arc starts at subtended angle start and stops at subtended angle startAngle + sweepAngle.
        /// Arcs greater than 90° are split into multiple arcs.
        /// </summary>
        /// <param name="ellipse"></param>
        /// <returns></returns>
        /// <remarks>
        /// Code ported from: https://www.khanacademy.org/computer-programming/e/6221186997551104
        /// Math from: http://www.spaceroots.org/documents/ellipse/node22.html
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<CubicBezier> ToCubicBeziers(this EllipticalArc ellipse)
            => EllipticalArcToCubicBeziers(ellipse.X, ellipse.Y, ellipse.RX, ellipse.RY, ellipse.StartAngle, ellipse.SweepAngle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuadraticBezier ToQuadraticBezier(this LineSegment line)
            => LineSegmentToQuadraticBezier(line.AX, line.AY, line.BX, line.BY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CubicBezier ToCubicBezier(this LineSegment line)
            => LineSegmentToCubicBezier(line.AX, line.AY, line.BX, line.BY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CubicBezier ToCubicBezier(this QuadraticBezier b)
            => QuadraticBezierToCubicBezier(b.A, b.B, b.C);

        #endregion

        #region Conversion Implementations

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CircularArc CircleToCircularArc(
            double x, double y,
            double r)
            => new CircularArc(x, y, r, 0, Tau);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r"></param>
        /// <returns></returns>
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
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <returns></returns>
        /// <remarks>
        /// Code ported from: https://www.khanacademy.org/computer-programming/e/6221186997551104
        /// Math from: http://www.spaceroots.org/documents/ellipse/node22.html
        /// </remarks>
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
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuadraticBezier LineSegmentToQuadraticBezier(
            Point2D a,
            Point2D b)
            => LineSegmentToQuadraticBezier(a.X, a.Y, b.X, b.Y);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static QuadraticBezier LineSegmentToQuadraticBezier(
            double x0, double y0,
            double x1, double y1)
            => new QuadraticBezier(new Point2D(x0, y0), Lerp(x0, y0, x1, y1, OneHalf), new Point2D(x1, y1));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CubicBezier LineSegmentToCubicBezier(
            Point2D a,
            Point2D b)
            => LineSegmentToCubicBezier(a.X, a.Y, b.X, b.Y);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CubicBezier LineSegmentToCubicBezier(
            double x0, double y0,
            double x1, double y1)
            => new CubicBezier(new Point2D(x0, y0), Lerp(x0, y0, x1, y1, OneThird), Lerp(x0, y0, x1, y1, TwoThirds), new Point2D(x1, y1));

        /// <summary>
        /// Raises a <see cref="QuadraticBezier"/> to a <see cref="CubicBezier"/>.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CubicBezier QuadraticBezierToCubicBezier(
            Point2D a,
            Point2D b,
            Point2D c)
        {
            var nodes = Interpolators.QuadraticBezierToCubicBezier(a, b, c);
            return new CubicBezier(nodes[0].X, nodes[0].Y, nodes[1].X, nodes[1].Y, nodes[2].X, nodes[2].Y, nodes[3].X, nodes[3].Y);
        }

        /// <summary>
        /// Raise a Quadratic Bezier to a Cubic Bezier.
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<(double X, double Y)> QuadraticBezierToCubicBezier(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2)
            => new List<(double X, double Y)>()
            {
                (x0, y0),
                (x0 + TwoThirds * (x1 - x0), y0 + TwoThirds * (y1 - y0)),
                (x2 + TwoThirds * (x1 - x2), y2 + TwoThirds * (y1 - y2)),
                (x2, y2)
            };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/ariutta/catmullrom2bezier/blob/master/catmullrom2bezier.js
        /// </remarks>
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
                    p.Add((x: points[i].X, y: points[i].Y));
                    p.Add((x: points[i].X, y: points[i].Y));
                    p.Add((x: points[i + 1].X, y: points[i + 1].Y));
                    p.Add((x: points[i + 2].X, y: points[i + 2].Y));
                }
                else if (iLen - 4 == i)
                {
                    p.Add((x: points[i - 1].X, y: points[i - 1].Y));
                    p.Add((x: points[i].X, y: points[i].Y));
                    p.Add((x: points[i + 1].X, y: points[i + 1].Y));
                    p.Add((x: points[i + 2].X, y: points[i + 2].Y));
                }
                else
                {
                    p.Add((x: points[i - 1].X, y: points[i - 1].Y));
                    p.Add((x: points[i].X, y: points[i].Y));
                    p.Add((x: points[i + 1].X, y: points[i + 1].Y));
                    p.Add((x: points[i + 2].X, y: points[i + 2].Y));
                }

                // Catmull-Rom to Cubic Bezier conversion matrix 
                //    0       1       0       0
                //  -1/6      1      1/6      0
                //    0      1/6      1     -1/6
                //    0       0       1       0
                var bp = new List<(double x, double y)>
                {
                    (x: p[1].x, y: p[1].y),
                    (x: ((-p[0].x + 6 * p[1].x + p[2].x) / 6), y: ((-p[0].y + 6 * p[1].y + p[2].y) / 6)),
                    (x: ((p[1].x + 6 * p[2].x - p[3].x) / 6), y: ((p[1].y + 6 * p[2].y - p[3].y) / 6)),
                    (x: p[2].x, y: p[2].y)
                };
                d.Add(new CubicBezier(bp[1].x, bp[1].y, bp[2].x, bp[2].y, bp[3].x, bp[3].y));
            }

            return d;
        }

        #endregion
    }
}
