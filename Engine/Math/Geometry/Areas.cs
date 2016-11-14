// <copyright file="Areas.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class Areas
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="sweepAngle"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ArcSector(double r, double sweepAngle)
            => Abs((r * r * 0.5d) * (sweepAngle - Sin(sweepAngle)));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rX"></param>
        /// <param name="rY"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://math.stackexchange.com/questions/114371/deriving-the-area-of-a-sector-of-an-ellipse?rq=1
        /// </remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double EllipticalArcSector(double rX, double rY, double startAngle, double sweepAngle)
        {
            return 0.5d * rX * rY * (Atan(rX * Tan(startAngle) / rY) - Atan(rX * Tan(startAngle + sweepAngle) / rY));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Circle(double r)
            => PI * r * r;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Ellipse(double r1, double r2)
            => PI * r2 * r1;

        /// <summary>
        /// Returns a positive number if c is to the left of the line going from a to b.
        /// </summary>
        /// <returns>
        /// Positive number if point is left, negative if point is right, 
        /// and 0 if points are collinear.
        /// </returns>
        /// <remarks>From Farseer Physics Engine.</remarks>
        public static double SignedTriangle(double aX, double aY, double bX, double bY, double cX, double cY)
            => aX * (bY - cY) + bX * (cY - aY) + cX * (aY - bY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Rectangle(double width, double height)
            => width * height;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="depth"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Square(double depth)
            => depth * depth;

        /// <summary>
        /// Return the polygon's area in "square units."
        /// Add the areas of the trapezoids defined by the
        /// polygon's edges dropped to the X-axis. When the
        /// program considers a bottom edge of a polygon, the
        /// calculation gives a negative area so the space
        /// between the polygon and the axis is subtracted,
        /// leaving the polygon's area. This method gives odd
        /// results for non-simple polygons.
        /// </summary>
        /// <returns>
        /// Return the absolute value of the signed area.
        /// The signed area is negative if the polygon is
        /// oriented clockwise.
        /// </returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Polygon(IEnumerable<Point2D> polygon)
            => Abs(SignedPolygon(polygon as List<Point2D>));

        /// <summary>
        /// Return the polygon's area in "square units."
        /// Add the areas of the trapezoids defined by the
        /// polygon's edges dropped to the X-axis. When the
        /// program considers a bottom edge of a polygon, the
        /// calculation gives a negative area so the space
        /// between the polygon and the axis is subtracted,
        /// leaving the polygon's area. This method gives odd
        /// results for non-simple polygons.
        ///
        /// The value will be negative if the polygon is
        /// oriented clockwise.
        /// </summary>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SignedPolygon(List<Point2D> polygon)
        {
            // Add the first point to the end.
            int num_points = polygon.Count;
            var pts = new Point2D[num_points + 1];
            polygon.CopyTo(pts, 0);
            pts[num_points] = polygon[0];

            // Get the areas.
            double area = 0d;
            for (int i = 0; i < num_points; i++)
            {
                area +=
                    (pts[i + 1].X - pts[i].X)
                    * (pts[i + 1].Y + pts[i].Y) * 0.5d;
            }

            // Return the result.
            return area;
        }
    }
}
