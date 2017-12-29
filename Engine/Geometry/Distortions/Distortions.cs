// <copyright file="Filter.cs" company="Shkyrockett" >
//     Copyright © 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

// <copyright file="CurvePreprocess.cs" >
//     Copyright © 2015 burningmime. All rights reserved.
// </copyright>
// <author id="burningmime">burningmime</author>
// <license>
//     Licensed under the Zlib License. See https://opensource.org/licenses/Zlib for full license information.
// </license>
// <summary></summary>
// <remarks>
//     Linearize, RecursiveRamerDouglasPeukerReduce, and RemoveDuplicates are from: https://github.com/burningmime/curves
// </remarks>

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static System.Math;
using static Engine.Maths;
using Engine;

namespace Engine
{
    /// <summary>
    /// The distortions class.
    /// </summary>
    public static class Distortions
    {
        #region Point Warp Filters

        /// <summary>
        /// The scale.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="factors">The factors.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Scale(Point2D point, Size2D factors)
            => new Point2D(point.X * factors.Width, point.Y * factors.Height);

        /// <summary>
        /// The flip.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="fulcrum">The fulcrum.</param>
        /// <param name="bHorz">The bHorz.</param>
        /// <param name="bVert">The bVert.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Flip(Point2D point, Point2D fulcrum, bool bHorz, bool bVert)
        {
            var x = (bHorz) ? fulcrum.X - (point.X - fulcrum.X + 1) : point.X;
            var y = (bVert) ? fulcrum.Y - (point.Y - fulcrum.Y + 1) : point.Y;
            return new Point2D(x, y);
        }

        /// <summary>
        /// Translate.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Translate(Point2D point, Vector2D offset)
            => point + offset;

        /// <summary>
        /// The matrix.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="matrix">The matrix.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Matrix(Point2D point, Matrix3x2D matrix)
            => matrix.Transform(point);

        /// <summary>
        /// Rotate a point about a center point.
        /// </summary>
        /// <param name="point">The point to rotate.</param>
        /// <param name="fulcrum">The center axis point.</param>
        /// <param name="xAxis">The Sine and Cosine of the angle on the x-axis.</param>
        /// <param name="yAxis">The Sine and Cosine of the angle on the y-axis.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Rotate(Point2D point, Point2D fulcrum, Point2D xAxis, Point2D yAxis)
            => new Point2D(
                fulcrum.X + ((point.X - fulcrum.X) * xAxis.X + (point.Y - fulcrum.Y) * xAxis.Y),
                fulcrum.Y + ((point.X - fulcrum.X) * yAxis.X + (point.Y - fulcrum.Y) * yAxis.Y));

        /// <summary>
        /// Rotate all the coordinates in-place around the center point (cx, cy) by angle theta.
        /// </summary>
        /// <param name="a">an array of arrays of coordinates in 2D space.</param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="theta"></param>
        public static void RotateArrays(List<List<Point2D>> a, double cx, double cy, double theta)
        {
            var cosine = Cos(theta);
            var sine = Sin(theta);
            foreach (var p in a)
            {
                for (var j = 0; j < p.Count; j++)
                {
                    var x = p[j].X - cx;
                    var y = p[j].Y - cy;
                    p[j] = new Point2D(cosine * x - sine * y + cx, sine * x + cosine * y + cy);
                }
            }
        }

        /// <summary>
        /// The pinch.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="fulcrum">The fulcrum.</param>
        /// <param name="strength">The strength.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Pinch(Point2D point, Point2D fulcrum, double strength = OneHalf)
        {
            if (fulcrum == point)
                return point;
            var dx = point.X - fulcrum.X;
            var dy = point.Y - fulcrum.Y;
            var distanceSquared = dx * dx + dy * dy;
            var sx = point.X;
            var sy = point.Y;
            var distance = Sqrt(distanceSquared);
            if (strength < 0d)
            {
                var r = distance;
                var a = Atan2(dy, dx); // Might this be simplified by finding the unit of the vector?
                var rn = Pow(r, strength) * distance;
                var newX = rn * Cos(a) + fulcrum.X;
                var newY = rn * Sin(a) + fulcrum.Y;
                sx += (newX - point.X);
                sy += (newY - point.Y);
            }
            else
            {
                var dirX = dx / distance;
                var dirY = dy / distance;
                var alpha = distance;
                var distortionFactor = distance * Pow(1d - alpha, 1d / strength);
                sx -= distortionFactor * dirX;
                sy -= distortionFactor * dirY;
            }

            return new Point2D(sx, sy);
        }

        /// <summary>
        /// The pinch.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="fulcrum">The fulcrum.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="strength">The strength.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Pinch(Point2D point, Point2D fulcrum, double radius, double strength = OneHalf)
        {
            if (fulcrum == point)
                return point;
            var dx = point.X - fulcrum.X;
            var dy = point.Y - fulcrum.Y;
            var distanceSquared = dx * dx + dy * dy;
            var sx = point.X;
            var sy = point.Y;
            if (distanceSquared < radius * radius)
            {
                var distance = Sqrt(distanceSquared);
                if (strength < 0d)
                {
                    var r = distance / radius;
                    var a = Atan2(dy, dx); // Might this be simplified by finding the unit of the vector?
                    var rn = Pow(r, strength) * distance;
                    var newX = rn * Cos(a) + fulcrum.X;
                    var newY = rn * Sin(a) + fulcrum.Y;
                    sx += (newX - point.X);
                    sy += (newY - point.Y);
                }
                else
                {
                    var dirX = dx / distance;
                    var dirY = dy / distance;
                    var alpha = distance / radius;
                    var distortionFactor = distance * Pow(1d - alpha, 1d / strength);
                    sx -= distortionFactor * dirX;
                    sy -= distortionFactor * dirY;
                }
            }

            return new Point2D(sx, sy);
        }

        /// <summary>
        /// The pinch1.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="fulcrum">The fulcrum.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="strength">The strength.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Pinch1(Point2D point, Point2D fulcrum, double radius, double strength = OneHalf)
        {
            if (fulcrum == point)
                return point;
            var dx = point.X - fulcrum.X;
            var dy = point.Y - fulcrum.Y;
            var distanceSquared = dx * dx + dy * dy;
            var sx = point.X;
            var sy = point.Y;
            if (distanceSquared < radius * radius)
            {
                var distance = Sqrt(distanceSquared);
                var r = distance / radius;
                var a = Atan2(dy, dx); // Might this be simplified by finding the unit of the vector?
                var rn = Pow(r, strength) * distance;
                var newX = rn * Cos(a) + fulcrum.X;
                var newY = rn * Sin(a) + fulcrum.Y;
                sx += (newX - point.X);
                sy += (newY - point.Y);
            }

            return new Point2D(sx, sy);
        }

        /// <summary>
        /// The pinch2.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="fulcrum">The fulcrum.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="strength">The strength.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Pinch2(Point2D point, Point2D fulcrum, double radius, double strength = OneHalf)
        {
            if (fulcrum == point)
                return point;
            var dx = point.X - fulcrum.X;
            var dy = point.Y - fulcrum.Y;
            var distanceSquared = dx * dx + dy * dy;
            var sx = point.X;
            var sy = point.Y;
            if (distanceSquared < radius * radius)
            {
                var distance = Sqrt(distanceSquared);
                var dirX = dx / distance;
                var dirY = dy / distance;
                var alpha = distance / radius;
                var distortionFactor = distance * Pow(1d - alpha, 1d / strength);
                sx -= distortionFactor * dirX;
                sy -= distortionFactor * dirY;
            }

            return new Point2D(sx, sy);
        }

        /// <summary>
        /// The swirl.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="fulcrum">The fulcrum.</param>
        /// <param name="degree">The degree.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Swirl(Point2D point, Point2D fulcrum, double degree = OneHalf)
        {
            if (fulcrum == point)
                return point;
            var dX = point.X - fulcrum.X;
            var dY = point.Y - fulcrum.Y;
            var theta = Atan2((dY), (dX));
            var radius = Sqrt(dX * dX + dY * dY);
            var newX = fulcrum.X + (radius * Cos(theta + degree * radius));
            var newY = fulcrum.Y + (radius * Sin(theta + degree * radius));
            return new Point2D(newX, newY);
        }

        /// <summary>
        /// The time warp.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="fulcrum">The fulcrum.</param>
        /// <param name="factor">The factor.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D TimeWarp(Point2D point, Point2D fulcrum, double factor = 10d)
        {
            var dX = point.X - fulcrum.X;
            var dY = point.Y - fulcrum.Y;
            var theta = Atan2((dY), (dX)); // Might this be simplified by finding the unit of the vector?
            var radius = Sqrt(dX * dX + dY * dY);
            var newRadius = Sqrt(radius) * factor;
            var newX = fulcrum.X + (newRadius * Cos(theta));
            var newY = fulcrum.Y + (newRadius * Sin(theta));
            return new Point2D(newX, newY);
        }

        /// <summary>
        /// The water.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="fulcrum">The fulcrum.</param>
        /// <param name="nWave">The nWave.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Water(Point2D point, Point2D fulcrum, double nWave = 1)
        {
            var xo = nWave * Sin(2d * PI * point.Y / 128d);
            var yo = nWave * Cos(2d * PI * point.X / 128d);
            var newX = (point.X + xo);
            var newY = (point.Y + yo);
            return new Point2D(newX, newY);
        }

        #endregion

        /// <summary>
        /// Creates a list of equally spaced points that lie on the path described by straight line segments between
        /// adjacent points in the source list.
        /// </summary>
        /// <param name="source">Source list of points.</param>
        /// <param name="distance">Distance between points on the new path.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>List of equally-spaced points on the path.</returns>
        /// <acknowledgment>
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> Linearize(List<Point2D> source, double distance, double epsilon = Epsilon)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "List must not be null");
            if (distance <= epsilon)
                throw new InvalidOperationException($"{nameof(distance)} {distance} must not be less than epsilon { epsilon }.");
            var dest = new List<Point2D>();
            if (source.Count < 1)
                return dest;

            var pp = source[0];
            dest.Add(pp);
            double cd = 0;
            for (var ip = 1; ip < source.Count; ip++)
            {
                var p0 = source[ip - 1];
                var p1 = source[ip];
                var td = p0.Distance(p1);
                if (cd + td > distance)
                {
                    var pd = distance - cd;
                    dest.Add(p0.Lerp(p1, pd / td));
                    var rd = td - pd;
                    while (rd > distance)
                    {
                        rd -= distance;
                        var np = p0.Lerp(p1, (td - rd) / td);
                        if (!Primitives.EqualsOrClose(np, pp))
                        {
                            dest.Add(np);
                            pp = np;
                        }
                    }
                    cd = rd;
                }
                else
                {
                    cd += td;
                }
            }

            // last point
            var lp = source[source.Count - 1];
            if (!Primitives.EqualsOrClose(pp, lp))
                dest.Add(lp);

            return dest;
        }

        /// <summary>
        /// "Reduces" a set of line segments by removing points that are too far away. Does not modify the input list; returns
        /// a new list with the points removed.
        /// </summary>
        /// <param name="points">Points to reduce</param>
        /// <param name="error">Maximum distance of a point to a line. Low values (~2-4) work well for mouse/touchscreen data.</param>
        /// <returns>A new list containing only the points needed to approximate the curve.</returns>
        /// <remarks>
        /// </remarks>
        /// <acknowledgment>
        /// The image says it better than I could ever describe: http://upload.wikimedia.org/wikipedia/commons/3/30/Douglas-Peucker_animated.gif
        /// The Wiki article: http://en.wikipedia.org/wiki/Ramer%E2%80%93Douglas%E2%80%93Peucker_algorithm
        /// Based on:  http://www.codeproject.com/Articles/18936/A-Csharp-Implementation-of-Douglas-Peucker-Line-Ap
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> RamerDouglasPeukerReduce(List<Point2D> points, double error = 4)
        {
            if (points == null)
                throw new ArgumentNullException(nameof(points), "Must not be null.");
            points = RemoveDuplicates(points);
            if (points.Count < 3)
                return new List<Point2D>(points);
            var keepIndex = new List<int>(Max(points.Count / 2, 16)) { 0, points.Count - 1 };
            RecursiveRamerDouglasPeukerReduce(points, error, 0, points.Count - 1, ref keepIndex);
            keepIndex.Sort();
            var res = new List<Point2D>(keepIndex.Count);
            foreach (var idx in keepIndex)
                res.Add(points[idx]);
            return res;
        }

        /// <summary>
        /// The recursive ramer douglas peuker reduce.
        /// </summary>
        /// <param name="pts">The pts.</param>
        /// <param name="error">The error.</param>
        /// <param name="first">The first.</param>
        /// <param name="last">The last.</param>
        /// <param name="keepIndex">The keepIndex.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void RecursiveRamerDouglasPeukerReduce(List<Point2D> pts, double error, int first, int last, ref List<int> keepIndex)
        {
            var nPts = last - first + 1;
            if (nPts < 3)
                return;

            var segment = new LineSegment(pts[first], pts[last]);

            var maxDist = error;
            var split = 0;
            for (var i = first + 1; i < last - 1; i++)
            {
                var p = pts[i];
                var pDist = Measurements.PerpendicularDistance(segment, p);
                if (!double.IsNaN(pDist) && pDist > maxDist)
                {
                    maxDist = pDist;
                    split = i;
                }
            }

            if (split != 0)
            {
                keepIndex.Add(split);
                RecursiveRamerDouglasPeukerReduce(pts, error, first, split, ref keepIndex);
                RecursiveRamerDouglasPeukerReduce(pts, error, split, last, ref keepIndex);
            }
        }

        /// <summary>
        /// Removes any repeated points (that is, one point extremely close to the previous one). The same point can
        /// appear multiple times just not right after one another. This does not modify the input list. If no repeats
        /// were found, it returns the input list; otherwise it creates a new list with the repeats removed.
        /// </summary>
        /// <param name="points">Initial list of points.</param>
        /// <returns>Either points (if no duplicates were found), or a new list containing points with duplicates removed.</returns>
        /// <acknowledgment>
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> RemoveDuplicates(List<Point2D> points)
        {
            if (points.Count < 2)
                return points;

            // Common case -- no duplicates, so just return the source list
            var prev = points[0];
            var len = points.Count;
            var nDup = 0;
            for (var i = 1; i < len; i++)
            {
                var cur = points[i];
                if (Primitives.EqualsOrClose(prev, cur))
                    nDup++;
                else
                    prev = cur;
            }

            if (nDup == 0)
                return points;
            else
            {
                // Create a copy without them
                var dst = new List<Point2D>(len - nDup);
                prev = points[0];
                dst.Add(prev);
                for (var i = 1; i < len; i++)
                {
                    var cur = points[i];
                    if (!Primitives.EqualsOrClose(prev, cur))
                    {
                        dst.Add(cur);
                        prev = cur;
                    }
                }
                return dst;
            }
        }

        /// <summary>
        /// Add the points to sides.
        /// </summary>
        /// <param name="contour">The contour.</param>
        /// <returns>The <see cref="PolygonContour"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PolygonContour AddPointsToSides(PolygonContour contour)
        {
            var result = new PolygonContour();
            for (var i = 1; i < contour.Count; i++)
            {
                for (double j = 0; j < 1; j = j + 1d / (contour[contour.Count - 1].Distance(contour[0]) * 8))
                {
                    result.Add(Interpolators.Linear(contour[i - 1], contour[i], j));
                }
            }
            for (double j = 0; j < 1; j = j + 1d / (contour[contour.Count - 1].Distance(contour[0]) * 8))
            {
                result.Add(Interpolators.Linear(contour[contour.Count - 1], contour[0], j));
            }

            return result;
        }

        /// <summary>
        /// The bilinear.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="u">The u.</param>
        /// <param name="v">The v.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        /// <acknowledgment>
        /// https://www.codeproject.com/articles/674433/perspective-projection-of-a-rectangle-homography
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Point2D Bilinear(Point2D[] point, double u, double v)
        {
            // Evaluate the bilinear transform
            var r = new Point2D(
                (1 - u) * point[0].X + u * point[1].X,
                (1 - u) * point[0].Y + u * point[1].Y);
            var s = new Point2D(
                (1 - u) * point[3].X + u * point[2].X,
                (1 - u) * point[3].Y + u * point[2].Y);
            return new Point2D((1 - v) * r.X + v * s.X, (1 - v) * r.Y + v * s.Y);
        }

        /// <summary>
        /// The perspective.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="c">The c.</param>
        /// <param name="u">The u.</param>
        /// <param name="v">The v.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        /// <acknowledgment>
        /// https://www.codeproject.com/articles/674433/perspective-projection-of-a-rectangle-homography
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Point2D Perspective(Point2D[] points, (double a, double b, double d, double e, double g, double h) c, double u, double v)
        {
            // Evaluate the homographic transform
            var T = c.g * u + c.h * v + 1;
            return new Point2D((c.a * u + c.b * v) / T + points[0].X, (c.d * u + c.e * v) / T + points[0].Y);
        }

        /// <summary>
        /// The solve perspective.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <returns>The <see cref="(double a, double b, double d, double e, double g, double h)"/>.</returns>
        /// <acknowledgment>
        /// https://www.codeproject.com/articles/674433/perspective-projection-of-a-rectangle-homography
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (double a, double b, double d, double e, double g, double h) SolvePerspective(Point2D[] points)
        {
            // Compute the transform coefficients
            var t = (points[2].X - points[1].X) * (points[2].Y - points[3].Y) - (points[2].X - points[3].X) * (points[2].Y - points[1].Y);

            var g = ((points[2].X - points[0].X) * (points[2].Y - points[3].Y) - (points[2].X - points[3].X) * (points[2].Y - points[0].Y)) / t;
            var h = ((points[2].X - points[1].X) * (points[2].Y - points[0].Y) - (points[2].X - points[0].X) * (points[2].Y - points[1].Y)) / t;

            var a = g * (points[1].X - points[0].X);
            var d = g * (points[1].Y - points[0].Y);
            var b = h * (points[3].X - points[0].X);
            var e = h * (points[3].Y - points[0].Y);

            g -= 1;
            h -= 1;
            return (a, b, d, e, g, h);
        }
    }
}
