// <copyright file="Intersectings.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using static Engine.Maths;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// A collection of methods for checking whether geometry intersects.
    /// </summary>
    public static class Intersectings
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="point0"></param>
        /// <param name="point1"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this Point2D point0, Point2D point1)
            => point0 == point1;

        /// <summary>
        /// Check whether a point is coincident to a line segment.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this LineSegment s, Point2D p)
            => LineSegmentPoint(s.A.X, s.A.Y, s.B.X, s.B.Y, p.X, p.Y);

        /// <summary>
        /// Check whether a point is coincident to a line segment.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this Point2D p, LineSegment s)
            => LineSegmentPoint(s.A.X, s.A.Y, s.B.X, s.B.Y, p.X, p.Y);

        /// <summary>
        /// Check whether two line segments intersect.
        /// </summary>
        /// <param name="s0"></param>
        /// <param name="s1"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this LineSegment s0, LineSegment s1)
            => LineSegmentLineSegment(s0.A.X, s0.A.Y, s0.B.X, s0.B.Y, s1.A.X, s1.A.Y, s1.B.X, s1.B.Y);

        /// <summary>
        /// Determines if this rectangle interests with another rectangle.
        /// </summary>
        /// <param name="rect1"></param>
        /// <param name="rect2"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this Rectangle2D rect1, Rectangle2D rect2)
            => RectangleRectangle(rect1.X, rect1.Y, rect1.Width, rect1.Height, rect2.X, rect2.Y, rect2.Width, rect2.Height);

        /// <summary>
        /// Determines if this Circle interests with another Circle.
        /// </summary>
        /// <param name="c0"></param>
        /// <param name="c1"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this Circle c0, Circle c1)
            => CircleCircle(c0.X, c0.Y, c0.Radius, c1.X, c1.Y, c1.Radius);

        /// <summary>
        /// Check whether a point is coincident to a line segment.
        /// </summary>
        /// <param name="segmentAX"></param>
        /// <param name="segmentAY"></param>
        /// <param name="segmentBX"></param>
        /// <param name="segmentBY"></param>
        /// <param name="pointX"></param>
        /// <param name="pointY"></param>
        /// <returns></returns>
        /// <remarks>http://www.angusj.com/delphi/clipper.php</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineSegmentPoint(
            double segmentAX,
            double segmentAY,
            double segmentBX,
            double segmentBY,
            double pointX,
            double pointY)
            => ((pointX == segmentAX) && (pointY == segmentAY)) ||
                ((pointX == segmentBX) && (pointY == segmentBY)) ||
                (((pointX > segmentAX) == (pointX < segmentBX)) &&
                ((pointY > segmentAY) == (pointY < segmentBY)) &&
                ((pointX - segmentAX) * (segmentBY - segmentAY) ==
                (segmentBX - segmentAX) * (pointY - segmentAY)));

        /// <summary>
        /// Find out whether two line segments intersect.
        /// </summary>
        /// <param name="x0">The x component of the first point of the first line.</param>
        /// <param name="y0">The y component of the first point of the first line.</param>
        /// <param name="x1">The x component of the second point of the first line.</param>
        /// <param name="y1">The y component of the second point of the first line.</param>
        /// <param name="x2">The x component of the first point of the second line.</param>
        /// <param name="y2">The y component of the first point of the second line.</param>
        /// <param name="x3">The x component of the second point of the second line.</param>
        /// <param name="y3">The y component of the second point of the second line.</param>
        /// <returns>Returns the point of intersection.</returns>
        /// <remarks>http://www.vb-helper.com/howto_segments_intersect.html</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineSegmentLineSegment(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
        {
            // Translate lines to origin.
            double u1 = (x1 - x0);
            double v1 = (y1 - y0);
            double u2 = (x3 - x2);
            double v2 = (y3 - y2);

            // Calculate the determinant of the coefficient matrix.
            double determinant = (v2 * u1) - (u2 * v1);

            // Check if the line segments are parallel.
            if (Abs(determinant) < Epsilon)
                // Return whether line segments are coincedental.
                return (LineSegmentPoint(x0, y0, x1, y1, x2, y2) || LineSegmentPoint(x0, y0, x1, y1, x3, y3));

            // Find the index where the intersection point lies on the line.
            double s = ((x0 - x2) * v1 + (y2 - y0) * u1) / -determinant;
            double t = ((x2 - x0) * v2 + (y0 - y2) * u2) / determinant;

            // Check whether the point is on the segment.
            return (t >= 0d) && (t <= 1d) && (s >= 0d) && (s <= 1d);
        }

        /// <summary>
        /// Determines if this rectangle interests with another rectangle.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="height1"></param>
        /// <param name="width1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="height2"></param>
        /// <param name="width2"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RectangleRectangle(
            double x1, double y1,
            double height1, double width1,
            double x2, double y2,
            double height2, double width2)
            => (x2 < x1 + width1)
            && (x1 < (x2 + width2))
            && (y2 < y1 + height1)
            && (y1 < y2 + width2);

        /// <summary>
        /// Find the points where the two circles intersect.
        /// </summary>
        /// <param name="cx0"></param>
        /// <param name="cy0"></param>
        /// <param name="radius0"></param>
        /// <param name="cx1"></param>
        /// <param name="cy1"></param>
        /// <param name="radius1"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/09/determine-where-two-circles-intersect-in-c/</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CircleCircle(
            double cx0,
            double cy0,
            double radius0,
            double cx1,
            double cy1,
            double radius1)
        {
            // If either of the circles are empty, return no intersections.
            if ((radius0 == 0d) || (radius1 == 0d))
                return false;

            // Find the distance between the centers.
            double dx = cx0 - cx1;
            double dy = cy0 - cy1;
            double dist = Sqrt(dx * dx + dy * dy);

            // See how many solutions there are.
            if (dist > radius0 + radius1)
            {
                // No solutions, the circles are too far apart.
                return false;
            }
            else if (dist < Abs(radius0 - radius1))
            {
                // No solutions, one circle contains the other.
                return true;
            }
            else if ((Abs(dist) < Epsilon) && (Abs(radius0 - radius1) < Epsilon))
            {
                // No solutions, the circles coincide.
                return true;
            }
            else
            {
                // There are one or more solutions.
                return true;
            }
        }
    }
}

