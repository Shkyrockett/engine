// <copyright file="Intersector.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// From: http://stackoverflow.com/questions/2255842/detecting-coincident-subset-of-two-coincident-line-segments/2255848
    /// port of this JavaScript code with some changes:
    ///   http://www.kevlindev.com/gui/math/intersection/Intersection.js
    /// found here:
    ///   http://stackoverflow.com/questions/563198/how-do-you-detect-where-two-line-segments-intersect/563240#563240
    /// </remarks>
    public class Intersector
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ub1"></param>
        /// <param name="ub2"></param>
        /// <returns></returns>
        private static double[] OverlapIntervals(double ub1, double ub2)
        {
            var A = Math.Max(0, Math.Min(ub1, ub2));
            var B = Math.Min(1, Math.Max(ub1, ub2));
            if (A > B) // No intersection
                return new double[] { };
            else if (A == B)
                return new double[] { A };
            else // if (A < B)
                return new double[] { A, B };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        /// <remarks>
        /// IMPORTANT: a1 and a2 cannot be the same, e.g. a1--a2 is a true segment, not a point
        /// b1/b2 may be the same (b1--b2 is a point)
        /// </remarks>
        private static Point2D[] OneD_Intersection(Point2D a1, Point2D a2, Point2D b1, Point2D b2)
        {
            var denomX = a2.X - a1.X;
            var denomY = a2.Y - a1.Y;

            // double ua1 = 0d; // by definition
            // double ua2 = 1d; // by definition
            (double, double) ub = (Math.Abs(denomX) > Math.Abs(denomY))
                ? ((b1.X - a1.X) / denomX, (b2.X - a1.X) / denomX)
                : ((b1.Y - a1.Y) / denomY, (b2.Y - a1.Y) / denomY);

            var ret = new List<Point2D>();
            double[] interval = OverlapIntervals(ub.Item1, ub.Item2);
            foreach (var f in interval)
            {
                var x = a2.X * f + a1.X * (1d - f);
                var y = a2.Y * f + a1.Y * (1d - f);
                var p = new Point2D(x, y);
                ret.Add(p);
            }

            return ret.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        public static Point2D[] Intersection(Point2D a1, Point2D a2, Point2D b1, Point2D b2)
        {
            // This is the general case. Really general
            if (a1.Equals(a2) && b1.Equals(b2))
            {
                // Both "segments" are points, return either point
                if (a1.Equals(b1))
                    return new Point2D[] { a1 };
                else // Both "segments" are different points, return empty set
                    return new Point2D[] { };
            }
            else if (b1.Equals(b2)) // b is a point, a is a segment
            {
                if (Intersections.PointLineSegmentIntersects(b1.X, b1.Y, a1.X, a1.Y, a2.X, a2.Y))
                    return new Point2D[] { b1 };
                else
                    return new Point2D[] { };
            }
            else if (a1.Equals(a2)) // a is a point, b is a segment
            {
                if (Intersections.PointLineSegmentIntersects(a1.X, a1.Y, b1.X, b1.Y, b2.X, b2.Y))
                    return new Point2D[] { a1 };
                else
                    return new Point2D[] { };
            }

            // At this point we know both a and b are actual segments
            var ua_t = (b2.X - b1.X) * (a1.Y - b1.Y) - (b2.Y - b1.Y) * (a1.X - b1.X);
            var ub_t = (a2.X - a1.X) * (a1.Y - b1.Y) - (a2.Y - a1.Y) * (a1.X - b1.X);
            var u_b = (b2.Y - b1.Y) * (a2.X - a1.X) - (b2.X - b1.X) * (a2.Y - a1.Y);

            // Infinite lines intersect somewhere
            if (!(-Maths.Epsilon < u_b && u_b < Maths.Epsilon))   // e.g. u_b != 0.0
            {
                var ua = ua_t / u_b;
                var ub = ub_t / u_b;
                if (0.0f <= ua && ua <= 1.0f && 0.0f <= ub && ub <= 1.0f)
                {
                    // Intersection
                    return new Point2D[] {
                    new Point2D(a1.X + ua * (a2.X - a1.X),
                        a1.Y + ua * (a2.Y - a1.Y)) };
                }
                else
                {
                    // No Intersection
                    return new Point2D[] { };
                }
            }
            else // lines (not just segments) are parallel or the same line
            {
                // Coincident
                // find the common overlapping section of the lines
                // first find the distance (squared) from one point (a1) to each point
                if ((-Maths.Epsilon < ua_t && ua_t < Maths.Epsilon)
                   || (-Maths.Epsilon < ub_t && ub_t < Maths.Epsilon))
                {
                    if (a1.Equals(a2)) // danger!
                        return OneD_Intersection(b1, b2, a1, a2);
                    else // safe
                        return OneD_Intersection(a1, a2, b1, b2);
                }
                else
                {
                    // Parallel
                    return new Point2D[] { };
                }
            }
        }
    }
}
