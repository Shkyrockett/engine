// <copyright file="Filter.cs" company="Shkyrockett" >
//     Copyright (c) 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

// <copyright file="CurvePreprocess.cs" >
//     Copyright (c) 2015 burningmime. All rights reserved.
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

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class Distortions
    {
        #region Point Warp Filters

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="factors"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Scale(Point2D point, Size2D factors)
            => new Point2D(point.X * factors.Width, point.Y * factors.Height);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fulcrum"></param>
        /// <param name="point"></param>
        /// <param name="bHorz"></param>
        /// <param name="bVert"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Flip(Point2D fulcrum, Point2D point, bool bHorz, bool bVert)
        {
            var x = (bHorz) ? fulcrum.X - (point.X - fulcrum.X + 1) : point.X;
            var y = (bVert) ? fulcrum.Y - (point.Y - fulcrum.Y + 1) : point.Y;
            return new Point2D(x, y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Translate(Point2D point, Vector2D offset)
            => point + offset;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Matrix(Point2D point, Matrix3x2D matrix)
            => matrix.Transform(point);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="center"></param>
        /// <param name="xAxis"></param>
        /// <param name="yAxis"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Rotate(Point2D point, Point2D center, Point2D xAxis, Point2D yAxis)
            => new Point2D(center.X - (point.X * xAxis.X + point.Y * xAxis.Y), center.Y - (point.X * yAxis.X + point.Y * yAxis.Y));

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
            for (var i = 0; i < a.Count; i++)
            {
                var p = a[i];
                for (var j = 0; j < p.Count;)
                {
                    var x = p[j].X - cx;
                    var y = p[j].Y - cy;
                    p[j] = new Point2D(cosine * x - sine * y + cx, sine * x + cosine * y + cy);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fulcrum"></param>
        /// <param name="point"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Pinch(Point2D fulcrum, Point2D point, double strength = 0.5d)
        {
            if (fulcrum == point)
                return point;
            var dx = point.X - fulcrum.X;
            var dy = point.Y - fulcrum.Y;
            var distanceSquared = dx * dx + dy * dy;
            var sx = point.X;
            var sy = point.Y;
            double distance = Math.Sqrt(distanceSquared);
            if (strength < 0)
            {
                double r = distance;
                double a = Math.Atan2(dy, dx);
                double rn = Math.Pow(r, strength) * distance;
                double newX = rn * Math.Cos(a) + fulcrum.X;
                double newY = rn * Math.Sin(a) + fulcrum.Y;
                sx += (newX - point.X);
                sy += (newY - point.Y);
            }
            else
            {
                double dirX = dx / distance;
                double dirY = dy / distance;
                double alpha = distance;
                double distortionFactor = distance * Math.Pow(1 - alpha, 1d / strength);
                sx -= distortionFactor * dirX;
                sy -= distortionFactor * dirY;
            }

            return new Point2D(sx, sy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fulcrum"></param>
        /// <param name="point"></param>
        /// <param name="radius"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Pinch(Point2D fulcrum, Point2D point, double radius, double strength = 0.5d)
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
                double distance = Math.Sqrt(distanceSquared);
                if (strength < 0)
                {
                    double r = distance / radius;
                    double a = Math.Atan2(dy, dx);
                    double rn = Math.Pow(r, strength) * distance;
                    double newX = rn * Math.Cos(a) + fulcrum.X;
                    double newY = rn * Math.Sin(a) + fulcrum.Y;
                    sx += (newX - point.X);
                    sy += (newY - point.Y);
                }
                else
                {
                    double dirX = dx / distance;
                    double dirY = dy / distance;
                    double alpha = distance / radius;
                    double distortionFactor = distance * Math.Pow(1 - alpha, 1d / strength);
                    sx -= distortionFactor * dirX;
                    sy -= distortionFactor * dirY;
                }
            }

            return new Point2D(sx, sy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fulcrum"></param>
        /// <param name="point"></param>
        /// <param name="radius"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Pinch1(Point2D fulcrum, Point2D point, double radius, double strength = 0.5d)
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
                double distance = Math.Sqrt(distanceSquared);
                double r = distance / radius;
                double a = Math.Atan2(dy, dx);
                double rn = Math.Pow(r, strength) * distance;
                double newX = rn * Math.Cos(a) + fulcrum.X;
                double newY = rn * Math.Sin(a) + fulcrum.Y;
                sx += (newX - point.X);
                sy += (newY - point.Y);
            }

            return new Point2D(sx, sy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fulcrum"></param>
        /// <param name="point"></param>
        /// <param name="radius"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Pinch2(Point2D fulcrum, Point2D point, double radius, double strength = 0.5d)
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
                double distance = Math.Sqrt(distanceSquared);
                double dirX = dx / distance;
                double dirY = dy / distance;
                double alpha = distance / radius;
                double distortionFactor = distance * Math.Pow(1 - alpha, 1d / strength);
                sx -= distortionFactor * dirX;
                sy -= distortionFactor * dirY;
            }

            return new Point2D(sx, sy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fulcrum"></param>
        /// <param name="point"></param>
        /// <param name="degree"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Swirl(Point2D fulcrum, Point2D point, double degree = 0.05d)
        {
            if (fulcrum == point)
                return point;
            var dX = point.X - fulcrum.X;
            var dY = point.Y - fulcrum.Y;
            double theta = Math.Atan2((dY), (dX));
            double radius = Math.Sqrt(dX * dX + dY * dY);
            var newX = fulcrum.X + (radius * Math.Cos(theta + degree * radius));
            var newY = fulcrum.Y + (radius * Math.Sin(theta + degree * radius));
            return new Point2D(newX, newY);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fulcrum"></param>
        /// <param name="point"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D TimeWarp(Point2D fulcrum, Point2D point, double factor = 10d)
        {
            var dX = point.X - fulcrum.X;
            var dY = point.Y - fulcrum.Y;
            var theta = Math.Atan2((dY), (dX));
            var radius = Math.Sqrt(dX * dX + dY * dY);
            double newRadius = Math.Sqrt(radius) * factor;
            var newX = fulcrum.X + (newRadius * Math.Cos(theta));
            var newY = fulcrum.Y + (newRadius * Math.Sin(theta));
            return new Point2D(newX, newY);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fulcrum"></param>
        /// <param name="point"></param>
        /// <param name="nWave"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Water(Point2D fulcrum, Point2D point, double nWave = 1)
        {
            double xo = nWave * Math.Sin(2d * Math.PI * point.Y / 128d);
            double yo = nWave * Math.Cos(2d * Math.PI * point.X / 128d);
            double newX = (point.X + xo);
            double newY = (point.Y + yo);
            return new Point2D(newX, newY);
        }

        #endregion

        /// <summary>
        /// Creates a list of equally spaced points that lie on the path described by straight line segments between
        /// adjacent points in the source list.
        /// </summary>
        /// <param name="source">Source list of points.</param>
        /// <param name="distance">Distance between points on the new path.</param>
        /// <returns>List of equally-spaced points on the path.</returns>
        public static List<Point2D> Linearize(List<Point2D> source, double distance)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "List must not be null");
            if (distance <= Maths.Epsilon)
                throw new InvalidOperationException($"{nameof(distance)} {distance} must not be less than epsilon { Maths.Epsilon }.");
            List<Point2D> dest = new List<Point2D>();
            if (source.Count < 1)
                return dest;

            Point2D pp = source[0];
            dest.Add(pp);
            double cd = 0;
            for (int ip = 1; ip < source.Count; ip++)
            {
                Point2D p0 = source[ip - 1];
                Point2D p1 = source[ip];
                double td = Measurements.Distance(p0, p1);
                if (cd + td > distance)
                {
                    double pd = distance - cd;
                    dest.Add(Primitives.Lerp(p0, p1, pd / td));
                    double rd = td - pd;
                    while (rd > distance)
                    {
                        rd -= distance;
                        Point2D np = Primitives.Lerp(p0, p1, (td - rd) / td);
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
            Point2D lp = source[source.Count - 1];
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
        /// The image says it better than I could ever describe: http://upload.wikimedia.org/wikipedia/commons/3/30/Douglas-Peucker_animated.gif
        /// The Wiki article: http://en.wikipedia.org/wiki/Ramer%E2%80%93Douglas%E2%80%93Peucker_algorithm
        /// Based on:  http://www.codeproject.com/Articles/18936/A-Csharp-Implementation-of-Douglas-Peucker-Line-Ap
        /// </remarks>
        public static List<Point2D> RamerDouglasPeukerReduce(List<Point2D> points, double error)
        {
            if (points == null)
                throw new ArgumentNullException(nameof(points), "Must not be null.");
            points = RemoveDuplicates(points);
            if (points.Count < 3)
                return new List<Point2D>(points);
            var keepIndex = new List<int>(Math.Max(points.Count / 2, 16)) { 0, points.Count - 1 };
            RecursiveRamerDouglasPeukerReduce(points, error, 0, points.Count - 1, ref keepIndex);
            keepIndex.Sort();
            List<Point2D> res = new List<Point2D>(keepIndex.Count);
            foreach (int idx in keepIndex)
                res.Add(points[idx]);
            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pts"></param>
        /// <param name="error"></param>
        /// <param name="first"></param>
        /// <param name="last"></param>
        /// <param name="keepIndex"></param>
        private static void RecursiveRamerDouglasPeukerReduce(List<Point2D> pts, double error, int first, int last, ref List<int> keepIndex)
        {
            int nPts = last - first + 1;
            if (nPts < 3)
                return;

            var segment = new LineSegment(pts[first], pts[last]);

            double maxDist = error;
            int split = 0;
            for (int i = first + 1; i < last - 1; i++)
            {
                Point2D p = pts[i];
                double pDist = Measurements.PerpendicularDistance(segment, p);
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
        public static List<Point2D> RemoveDuplicates(List<Point2D> points)
        {
            if (points.Count < 2)
                return points;

            // Common case -- no duplicates, so just return the source list
            Point2D prev = points[0];
            int len = points.Count;
            int nDup = 0;
            for (int i = 1; i < len; i++)
            {
                Point2D cur = points[i];
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
                List<Point2D> dst = new List<Point2D>(len - nDup);
                prev = points[0];
                dst.Add(prev);
                for (int i = 1; i < len; i++)
                {
                    Point2D cur = points[i];
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
        /// 
        /// </summary>
        /// <param name="contour"></param>
        /// <returns></returns>
        public static Contour AddPointsToSides(Contour contour)
        {
            var result = new Contour();
            for (int i = 1; i < contour.Count; i++)
            {
                for (double j = 0; j < 1; j = j + 1d / (Measurements.Distance(contour[contour.Count - 1], contour[0]) * 8))
                {
                    result.Add(Interpolators.Linear(contour[i - 1], contour[i], j));
                }
            }
            for (double j = 0; j < 1; j = j + 1d / (Measurements.Distance(contour[contour.Count - 1], contour[0]) * 8))
            {
                result.Add(Interpolators.Linear(contour[contour.Count - 1], contour[0], j));
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="P"></param>
        /// <param name="U"></param>
        /// <param name="V"></param>
        /// <returns></returns>
        /// <remarks> https://www.codeproject.com/articles/674433/perspective-projection-of-a-rectangle-homography </remarks>
        private static Point2D Bilinear(Point2D[] P, double U, double V)
        {
            Point2D R = new Point2D();
            Point2D S = new Point2D();

            // Evaluate the bilinear transform
            R.X = (1 - U) * P[0].X + U * P[1].X;
            R.Y = (1 - U) * P[0].Y + U * P[1].Y;
            S.X = (1 - U) * P[3].X + U * P[2].X;
            S.Y = (1 - U) * P[3].Y + U * P[2].Y;
            return new Point2D((1 - V) * R.X + V * S.X, (1 - V) * R.Y + V * S.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="P"></param>
        /// <param name="C"></param>
        /// <param name="U"></param>
        /// <param name="V"></param>
        /// <returns></returns>
        /// <remarks> https://www.codeproject.com/articles/674433/perspective-projection-of-a-rectangle-homography </remarks>
        private static Point2D Perspective(Point2D[] P, (double A, double B, double D, double E, double G, double H) C, double U, double V)
        {
            // Evaluate the homographic transform
            double T = C.G * U + C.H * V + 1;
            return new Point2D((C.A * U + C.B * V) / T + P[0].X, (C.D * U + C.E * V) / T + P[0].Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="P"></param>
        /// <returns></returns>
        /// <remarks> https://www.codeproject.com/articles/674433/perspective-projection-of-a-rectangle-homography </remarks>
        private static (double A, double B, double D, double E, double G, double H) SolvePerspective(Point2D[] P)
        {
            // Compute the transform coefficients
            var t = (P[2].X - P[1].X) * (P[2].Y - P[3].Y) - (P[2].X - P[3].X) * (P[2].Y - P[1].Y);

            var g = ((P[2].X - P[0].X) * (P[2].Y - P[3].Y) - (P[2].X - P[3].X) * (P[2].Y - P[0].Y)) / t;
            var h = ((P[2].X - P[1].X) * (P[2].Y - P[0].Y) - (P[2].X - P[0].X) * (P[2].Y - P[1].Y)) / t;

            var a = g * (P[1].X - P[0].X);
            var d = g * (P[1].Y - P[0].Y);
            var b = h * (P[3].X - P[0].X);
            var e = h * (P[3].Y - P[0].Y);

            g -= 1;
            h -= 1;
            return (a, b, d, e, g, h);
        }
    }
}
