using Engine.Geometry;
using System;
using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    internal class Utilities
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="curve"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://pomax.github.io/bezierinfo/
        /// </remarks>
        public static List<Point2D> Align(List<Point2D> curve, LineSegment line)
        {
            double tx = line.A.X;
            double ty = line.A.Y;
            double a = -Math.Atan2(line.B.Y - ty, line.B.X - tx);
            List<Point2D> results = new List<Point2D>();
            foreach (Point2D v in curve)
            {
                results.Add(new Point2D((v.X - tx) * Math.Cos(a) - (v.Y - ty) * Math.Sin(a), (v.X - tx) * Math.Sin(a) + (v.Y - ty) * Math.Cos(a)));
            }

            return results;
        }
    }
}
