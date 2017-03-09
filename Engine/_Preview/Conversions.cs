using System.Collections.Generic;
using static System.Math;
using static Engine.Maths;

namespace Engine._Preview
{
    /// <summary>
    /// 
    /// </summary>
    public static class Conversions
    {
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
        public static List<CubicBezier> EllipticalArcToCubicBeziers(EllipticalArc ellipse)
            => EllipticalArcToCubicBeziers(ellipse.X, ellipse.Y, ellipse.RX, ellipse.RY, ellipse.StartAngle, ellipse.SweepAngle);

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
            for (int s = 0; s < segs; s++, sx = ex, sy = ey)
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
        /// Initializes a new instance of the <see cref="CubicBezier"/> class from a <see cref="QuadraticBezier"/>.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        public static CubicBezier QuadraticBezierToCubicBezier(
            Point2D a,
            Point2D b,
            Point2D c)
        {
            var nodes = Interpolaters.QuadraticBezierToCubicBezier(a, b, c);
            return new CubicBezier(nodes[0].X,nodes[0].Y,nodes[1].X,nodes[1].Y,nodes[2].X,nodes[2].Y,nodes[3].X,nodes[3].Y);
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
    }
}
