// <copyright file="CurvePreprocess.cs" >
//     Copyright (c) 2015 burningmime. All rights reserved.
// </copyright>
// <author id="burningmime">burningmime</author>
// <license>
//     Licensed under the Zlib License. See https://opensource.org/licenses/Zlib for full license information.
// </license>
// <summary></summary>
// <remarks> https://github.com/burningmime/curves </remarks>

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Engine._Preview
{
    /// <summary>
    /// 
    /// </summary>
    public static class CurvePreprocess
    {
        /// <summary>
        /// Creates a list of equally spaced points that lie on the path described by straight line segments between
        /// adjacent points in the source list.
        /// </summary>
        /// <param name="src">Source list of points.</param>
        /// <param name="md">Distance between points on the new path.</param>
        /// <returns>List of equally-spaced points on the path.</returns>
        public static List<Point2D> Linearize(List<Point2D> src, double md)
        {
            if (src == null) throw new ArgumentNullException("src");
            if (md <= Maths.Epsilon) throw new InvalidOperationException("md " + md + " is be less than epsilon " + Maths.Epsilon);
            List<Point2D> dst = new List<Point2D>();
            if (src.Count > 0)
            {
                Point2D pp = src[0];
                dst.Add(pp);
                double cd = 0;
                for (int ip = 1; ip < src.Count; ip++)
                {
                    Point2D p0 = src[ip - 1];
                    Point2D p1 = src[ip];
                    double td = Measurements.Distance(p0, p1);
                    if (cd + td > md)
                    {
                        double pd = md - cd;
                        dst.Add(Primitives.Lerp(p0, p1, pd / td));
                        double rd = td - pd;
                        while (rd > md)
                        {
                            rd -= md;
                            Point2D np = Primitives.Lerp(p0, p1, (td - rd) / td);
                            if (!Primitives.EqualsOrClose(np, pp))
                            {
                                dst.Add(np);
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
                Point2D lp = src[src.Count - 1];
                if (!Primitives.EqualsOrClose(pp, lp))
                    dst.Add(lp);
            }
            return dst;
        }

        /// <summary>
        /// Removes any repeated points (that is, one point extremely close to the previous one). The same point can
        /// appear multiple times just not right after one another. This does not modify the input list. If no repeats
        /// were found, it returns the input list; otherwise it creates a new list with the repeats removed.
        /// </summary>
        /// <param name="pts">Initial list of points.</param>
        /// <returns>Either pts (if no duplicates were found), or a new list containing pts with duplicates removed.</returns>
        public static List<Point2D> RemoveDuplicates(List<Point2D> pts)
        {
            if (pts.Count < 2)
                return pts;

            // Common case -- no duplicates, so just return the source list
            Point2D prev = pts[0];
            int len = pts.Count;
            int nDup = 0;
            for (int i = 1; i < len; i++)
            {
                Point2D cur = pts[i];
                if (Primitives.EqualsOrClose(prev, cur))
                    nDup++;
                else
                    prev = cur;
            }

            if (nDup == 0)
                return pts;
            else
            {
                // Create a copy without them
                List<Point2D> dst = new List<Point2D>(len - nDup);
                prev = pts[0];
                dst.Add(prev);
                for (int i = 1; i < len; i++)
                {
                    Point2D cur = pts[i];
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
        /// "Reduces" a set of line segments by removing points that are too far away. Does not modify the input list; returns
        /// a new list with the points removed.
        /// The image says it better than I could ever describe: http://upload.wikimedia.org/wikipedia/commons/3/30/Douglas-Peucker_animated.gif
        /// The wiki article: http://en.wikipedia.org/wiki/Ramer%E2%80%93Douglas%E2%80%93Peucker_algorithm
        /// Based on:  http://www.codeproject.com/Articles/18936/A-Csharp-Implementation-of-Douglas-Peucker-Line-Ap
        /// </summary>
        /// <param name="pts">Points to reduce</param>
        /// <param name="error">Maximum distance of a point to a line. Low values (~2-4) work well for mouse/touchscreen data.</param>
        /// <returns>A new list containing only the points needed to approximate the curve.</returns>
        public static List<Point2D> RamerDouglasPeukerReduce(List<Point2D> pts, double error)
        {
            if (pts == null) throw new ArgumentNullException("pts");
            pts = RemoveDuplicates(pts);
            if (pts.Count < 3)
                return new List<Point2D>(pts);
            List<int> keepIndex = new List<int>(Math.Max(pts.Count / 2, 16))
            {
                0,
                pts.Count - 1
            };
            RamerDouglasPeukerRecursive(pts, error, 0, pts.Count - 1, keepIndex);
            keepIndex.Sort();
            List<Point2D> res = new List<Point2D>(keepIndex.Count);
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (int idx in keepIndex)
                res.Add(pts[idx]);
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
        private static void RamerDouglasPeukerRecursive(List<Point2D> pts, double error, int first, int last, List<int> keepIndex)
        {
            int nPts = last - first + 1;
            if (nPts < 3)
                return;

            Point2D a = pts[first];
            Point2D b = pts[last];
            double abDist = Measurements.Distance(a, b);
            double aCrossB = a.X * b.Y - b.X * a.Y;
            double maxDist = error;
            int split = 0;
            for (int i = first + 1; i < last - 1; i++)
            {
                Point2D p = pts[i];
                double pDist = PerpendicularDistance(a, b, abDist, aCrossB, p);
                if (pDist > maxDist)
                {
                    maxDist = pDist;
                    split = i;
                }
            }

            if (split != 0)
            {
                keepIndex.Add(split);
                RamerDouglasPeukerRecursive(pts, error, first, split, keepIndex);
                RamerDouglasPeukerRecursive(pts, error, split, last, keepIndex);
            }
        }

        /// <summary>
        /// Finds the shortest distance between a point and a line. See: http://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line
        /// </summary>
        /// <param name="a">First point of the line.</param>
        /// <param name="b">Last point of the line.</param>
        /// <param name="abDist">Distance between a and b (length of the line).</param>
        /// <param name="aCrossB">"a.X * b.Y - b.X * a.Y" This would be the Z-component of (⟪a.X, a.Y, 0⟫ ⨯ ⟪b.X, b.Y, 0⟫) in 3-space.</param>
        /// <param name="p">The point to test.</param>
        /// <returns>The perpendicular distance to the line.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double PerpendicularDistance(Point2D a, Point2D b, double abDist, double aCrossB, Point2D p)
        {
            // a profile with the test data showed that originally this was eating up ~44% of the runtime. So, this went through
            // several iterations of optimization and staring at the disassembly. I tried different methods of using cross
            // products, doing the computation with larger vector types, etc... this is the best I could do in ~45 minutes
            // running on 3 hours of sleep, which is all scalar math, but RyuJIT puts it into XMM registers and does
            // ADDSS/SUBSS/MULSS/DIVSS because that's what it likes to do whenever it sees a vector in a function. -burningmime
            double area = Math.Abs(aCrossB + b.X * p.Y + p.X * a.Y - p.X * b.Y - a.X *p.Y);
            double height = area / abDist;
            return height;
        }
    }
}
