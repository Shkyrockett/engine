using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Math;
using static Engine.Mathematics;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class Centers
    {
        /// <summary>
        /// Returns the Cartesian coordinate for one axis of a point that is defined by a given triangle and two normalized barycentric (areal) coordinates.
        /// </summary>
        /// <param name="value1">The coordinate on one axis of vertex 1 of the defining triangle.</param>
        /// <param name="value2">The coordinate on the same axis of vertex 2 of the defining triangle.</param>
        /// <param name="value3">The coordinate on the same axis of vertex 3 of the defining triangle.</param>
        /// <param name="amount1">The normalized barycentric (areal) coordinate b2, equal to the weighting factor for vertex 2, the coordinate of which is specified in value2.</param>
        /// <param name="amount2">The normalized barycentric (areal) coordinate b3, equal to the weighting factor for vertex 3, the coordinate of which is specified in value3.</param>
        /// <returns>Cartesian coordinate of the specified point with respect to the axis being used.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Barycentric(double value1, double value2, double value3, double amount1, double amount2)
            => value1 + ((value2 - value1) * amount1) + ((value3 - value1) * amount2);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) LineSegmentCenter(double x1, double y1, double x2, double y2)
            => (x1 + ((x2 - x1) * 0.5d), y1 + ((y2 - y1) * 0.5d));

        /// <summary>
        /// Compute and return the centroid of the polygon.  See
        /// http://wikipedia.org/wiki/Centroid
        /// </summary>
        /// <param name="poly">The poly.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/c/5567955982876672
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double x, double y) PolygonCentroid((double x, double y)[] poly)
        {
            var area = 0d;
            var cx = 0d;
            var cy = 0d;
            for (int i = poly.Length - 1, j = 0; j < poly.Length; i = j, j++)
            {
                var a = (poly[i].x * poly[j].y) - (poly[j].x * poly[i].y);
                cx += (poly[i].x + poly[j].x) * a;
                cy += (poly[i].y + poly[j].y) * a;
                area += a;
            }
            area *= 3;
            return (x: cx / area, y: cy / area);
        }

        /// <summary>
        /// Compute the center of triangle a-b-c.
        /// </summary>
        /// <param name="ax">The ax.</param>
        /// <param name="ay">The ay.</param>
        /// <param name="bx">The bx.</param>
        /// <param name="by">The by.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/c/5567955982876672
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double x, double y) TriangleCenter(
            double ax, double ay,
            double bx, double by,
            double cx, double cy)
        {
            var a = Measurements.Distance(bx, by, cx, cy);
            var b = Measurements.Distance(ax, ay, cx, cy);
            var c = Measurements.Distance(bx, by, ax, ay);
            var p = a + b + c;
            return (
                x: ((a * ax) + (b * bx) + (c * cx)) / p,
                y: ((a * ay) + (b * by) + (c * cy)) / p
            );
        }

        /// <summary>
        /// The circle center from points.
        /// </summary>
        /// <param name="p1X">The p1X.</param>
        /// <param name="p1Y">The p1Y.</param>
        /// <param name="p2X">The p2X.</param>
        /// <param name="p2Y">The p2Y.</param>
        /// <param name="p3X">The p3X.</param>
        /// <param name="p3Y">The p3Y.</param>
        /// <returns>The <see cref="T:ValueTuple{T1, T2}"/>.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/4103405/what-is-the-algorithm-for-finding-the-center-of-a-circle-from-three-points
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) CircleCenterFromThreePoints(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y)
        {
            var offset = (p2X * p2X) + (p2Y * p2Y);
            var bc = ((p1X * p1X) + (p1Y * p1Y) - offset) * 0.5d;
            var cd = (offset - (p3X * p3X) - (p3Y * p3Y)) * 0.5d;
            var determinant = ((p1X - p2X) * (p2Y - p3Y)) - ((p2X - p3X) * (p1Y - p2Y));

            if (Abs(determinant) < DoubleEpsilon)
            {
                return (0d, 0d);
            }

            return (
                ((bc * (p2Y - p3Y)) - (cd * (p1Y - p2Y))) / determinant,
                ((cd * (p1X - p2X)) - (bc * (p2X - p3X))) / determinant);
        }

        /// <summary>
        /// Extension method to find the center point of a rectangle.
        /// </summary>
        /// <param name="rectangle">The <see cref="Rectangle2D"/> of which you want the center.</param>
        /// <returns>A <see cref="Point2D"/> representing the center point of the <see cref="Rectangle2D"/>.</returns>
        /// <acknowledgment>Be sure to cache the results of this method if used repeatedly, as it is recalculated each time.
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) RectangleCenter(Rectangle2D rectangle)
            => (
                rectangle.Left + ((rectangle.Right - rectangle.Left) * 0.5d),
                rectangle.Top + ((rectangle.Bottom - rectangle.Top) * 0.5d)
                );
    }
}
