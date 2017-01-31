// <copyright file="Intersections.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <copyright file="Intersections.cs" company="kevlindev" >
//     Copyright (c) 2000 - 2003 Kevin Lindsey. All rights reserved.
// </copyright>
// <license>
//     Possibly BSD-3-Clause https://github.com/thelonious/kld-intersections/blob/development/LICENSE
// </license>
// <author id="thelonious">Kevin Lindsey</author>
// <summary></summary>

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Engine.Maths;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// A collection of methods for collecting the interactions of geometry.
    /// </summary>
    public static class Intersections
    {
        #region Intersects Extension Method Overloads

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point0"></param>
        /// <param name="point1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this Point2D point0, Point2D point1)
            => point0 == point1;

        /// <summary>
        /// Check whether a point is coincident to a line segment.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this LineSegment s, Point2D p)
            => PointLineSegmentIntersects(p.X, p.Y, s.A.X, s.A.Y, s.B.X, s.B.Y);

        /// <summary>
        /// Check whether a point is coincident to a line segment.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this Point2D p, LineSegment s)
            => PointLineSegmentIntersects(p.X, p.Y, s.A.X, s.A.Y, s.B.X, s.B.Y);

        /// <summary>
        /// Check whether two line segments intersect.
        /// </summary>
        /// <param name="s0"></param>
        /// <param name="s1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this LineSegment s0, LineSegment s1)
            => LineSegmentLineSegmentIntersects(s0.A.X, s0.A.Y, s0.B.X, s0.B.Y, s1.A.X, s1.A.Y, s1.B.X, s1.B.Y);

        /// <summary>
        /// Determines if this rectangle interests with another rectangle.
        /// </summary>
        /// <param name="rect1"></param>
        /// <param name="rect2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this Rectangle2D rect1, Rectangle2D rect2)
            => RectangleRectangleIntersects(rect1.X, rect1.Y, rect1.Width, rect1.Height, rect2.X, rect2.Y, rect2.Width, rect2.Height);

        /// <summary>
        /// Determines if this Circle interests with another Circle.
        /// </summary>
        /// <param name="c0"></param>
        /// <param name="c1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this Circle c0, Circle c1)
            => CircleCircleIntersects(c0.X, c0.Y, c0.Radius, c1.X, c1.Y, c1.Radius);

        #endregion

        #region Intersection Extension Method Overloads

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point0"></param>
        /// <param name="point1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Point2D point0, Point2D point1)
            => PointPointIntersection(point0.X, point0.Y, point1.X, point1.Y);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment segment, Point2D point)
            => PointLineSegmentIntersection(point.X, point.Y, segment.AX, segment.AY, segment.BX, segment.BY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Point2D point, LineSegment segment)
            => PointLineSegmentIntersection(point.X, point.Y, segment.AX, segment.AY, segment.BX, segment.BY);

        /// <summary>
        /// Find the intersection point between two lines segments.
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s1, LineSegment s2)
            => LineSegmentLineSegmentIntersection(s1.A.X, s1.A.Y, s1.B.X, s1.B.Y, s2.A.X, s2.A.Y, s2.B.X, s2.B.Y);

        /// <summary>
        /// Find the points of the intersection of a circle and a line segment.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, Circle c)
            => CircleLineSegmentIntersection(c.X, c.Y, c.Radius, s.A.X, s.A.Y, s.B.X, s.B.Y);

        /// <summary>
        /// Find the points of the intersection of a circle and a line segment.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, LineSegment s)
            => CircleLineSegmentIntersection(c.X, c.Y, c.Radius, s.A.X, s.A.Y, s.B.X, s.B.Y);

        /// <summary>
        /// Find the points of the intersection between two circles.
        /// </summary>
        /// <param name="c0"></param>
        /// <param name="c1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c0, Circle c1)
            => CircleCircleIntersection(c0.X, c0.Y, c0.Radius, c1.X, c1.Y, c1.Radius);

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, LineSegment s)
            => EllipseLineSegmentIntersection(e.X, e.Y, e.RX, e.RY, s.A.X, s.A.Y, e.Angle, s.B.X, s.B.Y);

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, Ellipse e)
            => EllipseLineSegmentIntersection(e.X, e.Y, e.RX, e.RY, s.A.X, s.A.Y, e.Angle, s.B.X, s.B.Y);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e0"></param>
        /// <param name="e1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e0, Ellipse e1)
            => UnrotatedEllipseUnrotatedEllipseIntersection(e0.Center, e0.RX, e0.RY, e1.Center, e1.RX, e1.RY);

        #endregion

        #region Intersects Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point0X"></param>
        /// <param name="point0Y"></param>
        /// <param name="point1X"></param>
        /// <param name="point1Y"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointPointIntersects(
            double point0X, double point0Y,
            double point1X, double point1Y)
            => (point0X == point1X && point0Y == point1Y);

        /// <summary>
        /// Check whether a point is coincident to a line segment.
        /// </summary>
        /// <param name="pointX"></param>
        /// <param name="pointY"></param>
        /// <param name="segmentAX"></param>
        /// <param name="segmentAY"></param>
        /// <param name="segmentBX"></param>
        /// <param name="segmentBY"></param>
        /// <returns></returns>
        /// <remarks>http://www.angusj.com/delphi/clipper.php</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointLineSegmentIntersects(
            double pointX,
            double pointY,
            double segmentAX,
            double segmentAY,
            double segmentBX,
            double segmentBY)
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineSegmentLineSegmentIntersects(
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
                // Return whether line segments are coincidental.
                return (PointLineSegmentIntersects(x2, y2, x0, y0, x1, y1) || PointLineSegmentIntersects(x3, y3, x0, y0, x1, y1));

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RectangleRectangleIntersects(
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CircleCircleIntersects(
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

        #endregion

        #region Intersection Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point0X"></param>
        /// <param name="point0Y"></param>
        /// <param name="point1X"></param>
        /// <param name="point1Y"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection PointPointIntersection(
            double point0X, double point0Y,
            double point1X, double point1Y)
            => PointPointIntersects(point0X, point0Y, point1X, point1Y) ? new Intersection(IntersectionStatus.Intersection, new Point2D(point0X, point0Y)) : new Intersection(IntersectionStatus.NoIntersection);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pointX"></param>
        /// <param name="pointY"></param>
        /// <param name="segmentAX"></param>
        /// <param name="segmentAY"></param>
        /// <param name="segmentBX"></param>
        /// <param name="segmentBY"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection PointLineSegmentIntersection(
            double pointX, double pointY,
            double segmentAX, double segmentAY,
            double segmentBX, double segmentBY)
            => (PointLineSegmentIntersects(pointX, pointY, segmentAX, segmentAY, segmentBX, segmentBY)) ? new Intersection(IntersectionStatus.Intersection, new Point2D(pointX, pointY)) : new Intersection(IntersectionStatus.NoIntersection);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection RayRayIntersection(
            Point2D a1,
            Point2D a2,
            Point2D b1,
            Point2D b2)
        {
            Intersection result;
            var ua_t = (b2.X - b1.X) * (a1.Y - b1.Y) - (b2.Y - b1.Y) * (a1.X - b1.X);
            var ub_t = (a2.X - a1.X) * (a1.Y - b1.Y) - (a2.Y - a1.Y) * (a1.X - b1.X);
            var u_b = (b2.Y - b1.Y) * (a2.X - a1.X) - (b2.X - b1.X) * (a2.Y - a1.Y);
            if (u_b != 0)
            {
                var ua = ua_t / u_b;
                result = new Intersection(IntersectionStatus.Intersection);
                result.AppendPoint(new Point2D(a1.X + ua * (a2.X - a1.X), a1.Y + ua * (a2.Y - a1.Y)));
            }
            else
            {
                if (ua_t == 0 || ub_t == 0)
                {
                    result = new Intersection(IntersectionStatus.Coincident);
                }
                else
                {
                    result = new Intersection(IntersectionStatus.Parallel);
                }
            }
            return result;
        }

        /// <summary>
        /// Find the intersection point between two lines.
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentLineSegmentIntersection(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
        {
            Intersection result = new Intersection(IntersectionStatus.NoIntersection);

            // Translate lines to origin.
            double u1 = (x1 - x0);
            double v1 = (y1 - y0);
            double u2 = (x3 - x2);
            double v2 = (y3 - y2);

            // Calculate the determinant of the coefficient matrix.
            double determinant = (v2 * u1) - (u2 * v1);

            // Check if the lines are parallel or coincident.
            if (Abs(determinant) < Epsilon)
                return result;

            // Find the index where the intersection point lies on the line.
            double s = ((x0 - x2) * v1 + (y2 - y0) * u1) / -determinant;
            double t = ((x2 - x0) * v2 + (y0 - y2) * u2) / determinant;

            // Check whether the point is on the segment.
            if ((t >= 0d) && (t <= 1d) && (s >= 0d) && (s <= 1d))
            {
                result = new Intersection(IntersectionStatus.Intersection);
                result.AppendPoint(new Point2D(x0 + t * u1, y0 + t * v1));
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineLineIntersection(
            Point2D a1,
            Point2D a2,
            Point2D b1,
            Point2D b2)
        {
            Intersection result;
            var ua_t = (b2.X - b1.X) * (a1.Y - b1.Y) - (b2.Y - b1.Y) * (a1.X - b1.X);
            var ub_t = (a2.X - a1.X) * (a1.Y - b1.Y) - (a2.Y - a1.Y) * (a1.X - b1.X);
            var u_b = (b2.Y - b1.Y) * (a2.X - a1.X) - (b2.X - b1.X) * (a2.Y - a1.Y);
            if (u_b != 0)
            {
                var ua = ua_t / u_b;
                var ub = ub_t / u_b;
                if (0 <= ua && ua <= 1 && 0 <= ub && ub <= 1)
                {
                    result = new Intersection(IntersectionStatus.Intersection);
                    result.AppendPoint(new Point2D(a1.X + ua * (a2.X - a1.X), a1.Y + ua * (a2.Y - a1.Y)));
                }
                else
                {
                    result = new Intersection(IntersectionStatus.NoIntersection);
                }
            }
            else
            {
                if (ua_t == 0 || ub_t == 0)
                {
                    result = new Intersection(IntersectionStatus.Coincident);
                }
                else
                {
                    result = new Intersection(IntersectionStatus.Parallel);
                }
            }
            return result;
        }

        /// <summary>
        /// Find the intersection point between two lines.
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineLineIntersection(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
        {
            var result = new Intersection(IntersectionStatus.NoIntersection);

            // Translate lines to origin.
            double u1 = (x1 - x0);
            double v1 = (y1 - y0);
            double u2 = (x3 - x2);
            double v2 = (y3 - y2);

            // Calculate the determinant of the coefficient matrix.
            double determinant = (v2 * u1) - (u2 * v1);

            // Check if the lines are parallel.
            if (Abs(determinant) < Epsilon)
                return result;

            // Find the index where the intersection point lies on the line.
            double s = ((x0 - x2) * v1 + (y2 - y0) * u1) / -determinant;
            double t = ((x2 - x0) * v2 + (y0 - y2) * u2) / determinant;

            // Check whether the point is on the segment.
            result = new Intersection(IntersectionStatus.Intersection);
            result.AppendPoint(new Point2D(x0 + t * u1, y0 + t * v1));

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineRectangleIntersection(
            Point2D a1,
            Point2D a2,
            Point2D r1,
            Point2D r2)
        {
            var min = r1.Min(r2);
            var max = r1.Max(r2);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);
            var inter1 = LineLineIntersection(min, topRight, a1, a2);
            var inter2 = LineLineIntersection(topRight, max, a1, a2);
            var inter3 = LineLineIntersection(max, bottomLeft, a1, a2);
            var inter4 = LineLineIntersection(bottomLeft, min, a1, a2);
            var result = new Intersection(IntersectionStatus.NoIntersection);
            result.AppendPoints(inter1.Points);
            result.AppendPoints(inter2.Points);
            result.AppendPoints(inter3.Points);
            result.AppendPoints(inter4.Points);
            if (result.Points.Count > 0)
                result.Status = IntersectionStatus.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection RectangleRectangleIntersection(
            Point2D a1,
            Point2D a2,
            Point2D b1,
            Point2D b2)
        {
            var min = a1.Min(a2);
            var max = a1.Max(a2);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);
            var inter1 = LineRectangleIntersection(min, topRight, b1, b2);
            var inter2 = LineRectangleIntersection(topRight, max, b1, b2);
            var inter3 = LineRectangleIntersection(max, bottomLeft, b1, b2);
            var inter4 = LineRectangleIntersection(bottomLeft, min, b1, b2);
            var result = new Intersection(IntersectionStatus.NoIntersection);
            result.AppendPoints(inter1.Points);
            result.AppendPoints(inter2.Points);
            result.AppendPoints(inter3.Points);
            result.AppendPoints(inter4.Points);
            if (result.Points.Count > 0)
                result.Status = IntersectionStatus.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LinePolygonIntersection(
            Point2D a1,
            Point2D a2,
            List<Point2D> points)
        {
            var result = new Intersection(IntersectionStatus.NoIntersection);
            var length = points.Count;
            for (var i = 0; i < length; i++)
            {
                var b1 = points[i];
                var b2 = points[(i + 1) % length];
                var inter = LineLineIntersection(a1, a2, b1, b2);
                result.AppendPoints(inter.Points);
            }
            if (result.Points.Count > 0)
                result.Status = IntersectionStatus.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection PolygonRectangleIntersection(
            List<Point2D> points,
            Point2D r1,
            Point2D r2)
        {
            var min = r1.Min(r2);
            var max = r1.Max(r2);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);
            var inter1 = LinePolygonIntersection(min, topRight, points);
            var inter2 = LinePolygonIntersection(topRight, max, points);
            var inter3 = LinePolygonIntersection(max, bottomLeft, points);
            var inter4 = LinePolygonIntersection(bottomLeft, min, points);
            var result = new Intersection(IntersectionStatus.NoIntersection);
            result.AppendPoints(inter1.Points);
            result.AppendPoints(inter2.Points);
            result.AppendPoints(inter3.Points);
            result.AppendPoints(inter4.Points);
            if (result.Points.Count > 0)
                result.Status = IntersectionStatus.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points1"></param>
        /// <param name="points2"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection PolygonPolygonIntersection(
            List<Point2D> points1,
            List<Point2D> points2)
        {
            var result = new Intersection(IntersectionStatus.NoIntersection);
            var length = points1.Count;
            for (var i = 0; i < length; i++)
            {
                var a1 = points1[i];
                var a2 = points1[(i + 1) % length];
                var inter = LinePolygonIntersection(a1, a2, points2);
                result.AppendPoints(inter.Points);
            }
            if (result.Points.Count > 0)
                result.Status = IntersectionStatus.Intersection;
            return result;
        }

        /// <summary>
        /// Find the points of the intersection of a circle and a line segment.
        /// </summary>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="radius"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CircleLineSegmentIntersection(
            double cX, double cY,
            double radius,
            double x1, double y1,
            double x2, double y2)
        {
            var result = new Intersection(IntersectionStatus.NoIntersection);

            // If the circle or line segment are empty, return no intersections.
            if ((radius == 0d) || ((x1 == x2) && (y1 == y2)))
                return result;

            double dx = x2 - x1;
            double dy = y2 - y1;

            // Calculate the quadratic parameters.
            double a = dx * dx + dy * dy;
            double b = 2 * (dx * (x1 - cX) + dy * (y1 - cY));
            double c = (x1 - cX) * (x1 - cX) + (y1 - cY) * (y1 - cY) - radius * radius;

            // Calculate the discriminant.
            double discriminant = b * b - 4 * a * c;

            if ((a <= Epsilon) || (discriminant < 0))
            {
                // No real solutions.
            }
            else if (discriminant == 0)
            {
                // One possible solution.
                double t = -b / (2 * a);

                // Add the points if they are between the end points of the line segment.
                if ((t >= 0d) && (t <= 1d))
                {
                    result = new Intersection(IntersectionStatus.Intersection);
                    result.AppendPoint(new Point2D(x1 + t * dx, y1 + t * dy));
                }
            }
            else if (discriminant > 0)
            {
                // Two possible solutions.
                double t1 = ((-b + Sqrt(discriminant)) / (2 * a));
                double t2 = ((-b - Sqrt(discriminant)) / (2 * a));

                // Add the points if they are between the end points of the line segment.
                result = new Intersection(IntersectionStatus.Intersection);
                if ((t1 >= 0d) && (t1 <= 1d))
                    result.AppendPoint(new Point2D(x1 + t1 * dx, y1 + t1 * dy));
                if ((t2 >= 0d) && (t2 <= 1d))
                    result.AppendPoint(new Point2D(x1 + t2 * dx, y1 + t2 * dy));
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <param name="r"></param>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CircleLineIntersection(
            Point2D c,
            double r,
            Point2D a1,
            Point2D a2)
        {
            Intersection result;
            var a = (a2.X - a1.X) * (a2.X - a1.X) + (a2.Y - a1.Y) * (a2.Y - a1.Y);
            var b = 2 * ((a2.X - a1.X) * (a1.X - c.X) + (a2.Y - a1.Y) * (a1.Y - c.Y));
            var cc = c.X * c.X + c.Y * c.Y + a1.X * a1.X + a1.Y * a1.Y - 2 * (c.X * a1.X + c.Y * a1.Y) - r * r;
            var deter = b * b - 4 * a * cc;
            if (deter < 0)
            {
                result = new Intersection(IntersectionStatus.Outside);
            }
            else if (deter == 0)
            {
                result = new Intersection(IntersectionStatus.Tangent);
            }
            else
            {
                var e = Sqrt(deter);
                var u1 = (-b + e) / (2 * a);
                var u2 = (-b - e) / (2 * a);
                if ((u1 < 0 || u1 > 1) && (u2 < 0 || u2 > 1))
                {
                    if ((u1 < 0 && u2 < 0) || (u1 > 1 && u2 > 1))
                    {
                        result = new Intersection(IntersectionStatus.Outside);
                    }
                    else
                    {
                        result = new Intersection(IntersectionStatus.Inside);
                    }
                }
                else
                {
                    result = new Intersection(IntersectionStatus.Intersection);
                    if (0 <= u1 && u1 <= 1)
                        result.Points.Add(a1.Lerp(a2, u1));
                    if (0 <= u2 && u2 <= 1)
                        result.Points.Add(a1.Lerp(a2, u2));
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <param name="r"></param>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection CircleRectangleIntersection(
            Point2D c,
            double r,
            Point2D r1,
            Point2D r2)
        {
            var min = r1.Min(r2);
            var max = r1.Max(r2);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);
            var inter1 = CircleLineIntersection(c, r, min, topRight);
            var inter2 = CircleLineIntersection(c, r, topRight, max);
            var inter3 = CircleLineIntersection(c, r, max, bottomLeft);
            var inter4 = CircleLineIntersection(c, r, bottomLeft, min);
            var result = new Intersection(IntersectionStatus.NoIntersection);
            result.AppendPoints(inter1.Points);
            result.AppendPoints(inter2.Points);
            result.AppendPoints(inter3.Points);
            result.AppendPoints(inter4.Points);
            if (result.Points.Count > 0)
                result.Status = IntersectionStatus.Intersection;
            else result.Status = inter1.Status;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <param name="r"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection CirclePolygonIntersection(
            Point2D c,
            double r,
            List<Point2D> points)
        {
            var result = new Intersection(IntersectionStatus.NoIntersection);
            var length = points.Count;
            Intersection inter = new Intersection(IntersectionStatus.NoIntersection);
            for (var i = 0; i < length; i++)
            {
                var a1 = points[i];
                var a2 = points[(i + 1) % length];
                inter = CircleLineIntersection(c, r, a1, a2);
                result.AppendPoints(inter.Points);
            }
            if (result.Points.Count > 0)
                result.Status = IntersectionStatus.Intersection;
            else result.Status = inter.Status;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="r1"></param>
        /// <param name="c2"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CircleCircleIntersection(
            Point2D c1,
            double r1,
            Point2D c2,
            double r2)
        {
            Intersection result;
            var r_max = r1 + r2;
            var r_min = Abs(r1 - r2);
            var c_dist = c1.Distance(c2);
            if (c_dist > r_max)
            {
                result = new Intersection(IntersectionStatus.Outside);
            }
            else if (c_dist < r_min)
            {
                result = new Intersection(IntersectionStatus.Inside);
            }
            else
            {
                result = new Intersection(IntersectionStatus.Intersection);
                var a = (r1 * r1 - r2 * r2 + c_dist * c_dist) / (2 * c_dist);
                var h = Sqrt(r1 * r1 - a * a);
                var p = c1.Lerp(c2, a / c_dist);
                var b = h / c_dist;
                result.AppendPoint(new Point2D(p.X - b * (c2.Y - c1.Y), p.Y + b * (c2.X - c1.X)));
                result.AppendPoint(new Point2D(p.X + b * (c2.Y - c1.Y), p.Y - b * (c2.X - c1.X)));
            }

            return result;
        }

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CircleCircleIntersection(
            double cx0,
            double cy0,
            double radius0,
            double cx1,
            double cy1,
            double radius1)
        {
            var result = new Intersection(IntersectionStatus.NoIntersection);

            // If either of the circles are empty, return no intersections.
            if ((radius0 == 0d) || (radius1 == 0d))
                return result;

            // Find the distance between the centers.
            double dx = cx0 - cx1;
            double dy = cy0 - cy1;
            double dist = Sqrt(dx * dx + dy * dy);

            // See how many solutions there are.
            if (dist > radius0 + radius1)
            {
                // No solutions, the circles are too far apart.
                // This would be a good point to return a null Lotus.
            }
            else if (dist < Abs(radius0 - radius1))
            {
                // No solutions, one circle contains the other.
                // This would be a good point to return a Lotus struct of th smaller of the circles.
            }
            else if ((Abs(dist) < Epsilon) && (Abs(radius0 - radius1) < Epsilon))
            {
                // No solutions, the circles coincide.
                // This would be a good point to return a Lotus struct of one of the circles.
            }
            else
            {
                // Find a and h.
                double a = (radius0 * radius0 - radius1 * radius1 + dist * dist) / (2 * dist);
                double h = Sqrt(radius0 * radius0 - a * a);

                // Find P2.
                double cx2 = cx0 + a * (cx1 - cx0) / dist;
                double cy2 = cy0 + a * (cy1 - cy0) / dist;

                // See if we have 1 or 2 solutions.
                if (Abs(dist - radius0 + radius1) < Epsilon)
                {
                    // Get the points P3.
                    result = new Intersection(IntersectionStatus.Intersection);
                    result.AppendPoint(new Point2D(
                        (cx2 + h * (cy1 - cy0) / dist),
                        (cy2 - h * (cx1 - cx0) / dist)));
                }
                else
                {
                    // Get the points P3.
                    result = new Intersection(IntersectionStatus.Intersection);
                    result.AppendPoint(new Point2D(
                    (cx2 + h * (cy1 - cy0) / dist),
                    (cy2 - h * (cx1 - cx0) / dist)));
                    result.AppendPoint(new Point2D(
                    (cx2 - h * (cy1 - cy0) / dist),
                    (cy2 + h * (cx1 - cx0) / dist)));
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cc"></param>
        /// <param name="r"></param>
        /// <param name="ec"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection CircleUnrotatedEllipseIntersection(
            Point2D cc,
            double r,
            Point2D ec,
            double rx,
            double ry)
            => UnrotatedEllipseUnrotatedEllipseIntersection(cc, r, r, ec, rx, ry);

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://csharphelper.com/blog/2012/09/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection UnrotatedEllipseLineSegmentIntersection(
            double cx, double cy,
            double rx, double ry,
            double x0, double y0,
            double x1, double y1)
        {
            var result = new Intersection(IntersectionStatus.NoIntersection);

            // If the ellipse or line segment are empty, return no intersections.
            if ((rx == 0d) || (ry == 0d) ||
                ((x0 == x1) && (y0 == y1)))
                return result;

            // Translate the line to put the ellipse centered at the origin.
            double u1 = x0 - cx;
            double v1 = y0 - cy;
            double u2 = x1 - cx;
            double v2 = y1 - cy;

            // Calculate the quadratic parameters.
            double a = (u2 - u1) * (u2 - u1) / (rx * rx) + (v2 - v1) * (v2 - v1) / (ry * ry);
            double b = 2d * u1 * (u2 - u1) / (rx * rx) + 2d * v1 * (v2 - v1) / (ry * ry);
            double c = (u1 * u1) / (rx * rx) + (v1 * v1) / (ry * ry) - 1d;

            // Calculate the discriminant.
            double discriminant = b * b - 4d * a * c;

            if ((a <= Epsilon) || (discriminant < 0))
            {
                // No real solutions.
            }
            else if (discriminant == 0)
            {
                // One real possible solution.
                double t = 0.5d * -b / a;

                // Add the points if it is between the end points of the line segment.
                if ((t >= 0d) && (t <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + (u2 - u1) * t + cx, v1 + (v2 - v1) * t + cy));
                    result.Status = IntersectionStatus.Intersection;
                }
            }
            else if (discriminant > 0)
            {
                // Two real possible solutions.
                double t1 = (0.5d * (-b + Sqrt(discriminant)) / a);
                double t2 = (0.5d * (-b - Sqrt(discriminant)) / a);

                // Add the points if they are between the end points of the line segment.
                if ((t1 >= 0d) && (t1 <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + (u2 - u1) * t1 + cx, v1 + (v2 - v1) * t1 + cy));
                    result.Status = IntersectionStatus.Intersection;
                }

                if ((t2 >= 0d) && (t2 <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + (u2 - u1) * t2 + cx, v1 + (v2 - v1) * t2 + cy));
                    result.Status = IntersectionStatus.Intersection;
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="center"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection UnrotatedEllipseLineIntersection(
            Point2D center,
            double rx,
            double ry,
            Point2D a1,
            Point2D a2)
        {
            Intersection result;
            var origin = new Vector2D(a1.X, a1.Y);
            var dir = new Vector2D(a1, a2);
            var diff = origin.Subtract(center);
            var mDir = new Vector2D(dir.I / (rx * rx), dir.J / (ry * ry));
            var mDiff = new Vector2D(diff.X / (rx * rx), diff.Y / (ry * ry));
            var a = dir.DotProduct(mDir);
            var b = dir.DotProduct(mDiff);
            var c = diff.DotProduct(mDiff) - 1.0;
            var d = b * b - a * c;
            if (d < 0)
            {
                result = new Intersection(IntersectionStatus.Outside);
            }
            else if (d > 0)
            {
                var root = Sqrt(d);
                var t_a = (-b - root) / a;
                var t_b = (-b + root) / a;
                if ((t_a < 0 || 1 < t_a) && (t_b < 0 || 1 < t_b))
                {
                    if ((t_a < 0 && t_b < 0) || (t_a > 1 && t_b > 1))
                        result = new Intersection(IntersectionStatus.Outside);
                    else result = new Intersection(IntersectionStatus.Inside);
                }
                else
                {
                    result = new Intersection(IntersectionStatus.Intersection);
                    if (0 <= t_a && t_a <= 1) result.AppendPoint(a1.Lerp(a2, t_a));
                    if (0 <= t_b && t_b <= 1) result.AppendPoint(a1.Lerp(a2, t_b));
                }
            }
            else
            {
                var t = -b / a; if (0 <= t && t <= 1)
                {
                    result = new Intersection(IntersectionStatus.Intersection);
                    result.AppendPoint(a1.Lerp(a2, t));
                }
                else
                {
                    result = new Intersection(IntersectionStatus.Outside);
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection UnrotatedEllipseRectangleIntersection(
            Point2D c,
            double rx,
            double ry,
            Point2D r1,
            Point2D r2)
        {
            var min = r1.Min(r2);
            var max = r1.Max(r2);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);
            var inter1 = UnrotatedEllipseLineIntersection(c, rx, ry, min, topRight);
            var inter2 = UnrotatedEllipseLineIntersection(c, rx, ry, topRight, max);
            var inter3 = UnrotatedEllipseLineIntersection(c, rx, ry, max, bottomLeft);
            var inter4 = UnrotatedEllipseLineIntersection(c, rx, ry, bottomLeft, min);
            var result = new Intersection(IntersectionStatus.NoIntersection);
            result.AppendPoints(inter1.Points);
            result.AppendPoints(inter2.Points);
            result.AppendPoints(inter3.Points);
            result.AppendPoints(inter4.Points);
            if (result.Points.Count > 0)
                result.Status = IntersectionStatus.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection UnrotatedEllipsePolygonIntersection(
            Point2D c,
            double rx,
            double ry,
            List<Point2D> points)
        {
            var result = new Intersection(IntersectionStatus.NoIntersection);
            var length = points.Count;
            for (var i = 0; i < length; i++)
            {
                var b1 = points[i];
                var b2 = points[(i + 1) % length];
                var inter = UnrotatedEllipseLineIntersection(c, rx, ry, b1, b2);
                result.AppendPoints(inter.Points);
            }
            if (result.Points.Count > 0)
                result.Status = IntersectionStatus.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="rx1"></param>
        /// <param name="ry1"></param>
        /// <param name="c2"></param>
        /// <param name="rx2"></param>
        /// <param name="ry2"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection UnrotatedEllipseUnrotatedEllipseIntersection(
            Point2D c1,
            double rx1,
            double ry1,
            Point2D c2,
            double rx2,
            double ry2)
        {
            double[] a = new double[] { ry1 * ry1, 0, rx1 * rx1, -2 * ry1 * ry1 * c1.X, -2 * rx1 * rx1 * c1.Y, ry1 * ry1 * c1.X * c1.X + rx1 * rx1 * c1.Y * c1.Y - rx1 * rx1 * ry1 * ry1 };
            double[] b = new double[] { ry2 * ry2, 0, rx2 * rx2, -2 * ry2 * ry2 * c2.X, -2 * rx2 * rx2 * c2.Y, ry2 * ry2 * c2.X * c2.X + rx2 * rx2 * c2.Y * c2.Y - rx2 * rx2 * ry2 * ry2 };
            var yPoly = Bezout(a, b);
            var yRoots = yPoly.Roots();
            var epsilon = 1e-3;
            var norm0 = (a[0] * a[0] + 2 * a[1] * a[1] + a[2] * a[2]) * epsilon;
            var norm1 = (b[0] * b[0] + 2 * b[1] * b[1] + b[2] * b[2]) * epsilon;
            var result = new Intersection(IntersectionStatus.NoIntersection);
            for (var y = 0; y < yRoots.Count; y++)
            {
                var xPoly = new Polynomial(
                    a[5] + yRoots[y] * (a[4] + yRoots[y] * a[2]),
                    a[3] + yRoots[y] * a[1],
                    a[0]
                    );
                var xRoots = xPoly.Roots();
                for (var x = 0; x < xRoots.Count; x++)
                {
                    var test = (a[0] * xRoots[x] + a[1] * yRoots[y] + a[3]) * xRoots[x] + (a[2] * yRoots[y] + a[4]) * yRoots[y] + a[5];
                    if (Abs(test) < norm0)
                    {
                        test = (b[0] * xRoots[x] + b[1] * yRoots[y] + b[3]) * xRoots[x] + (b[2] * yRoots[y] + b[4]) * yRoots[y] + b[5];
                        if (Abs(test) < norm1)
                        {
                            result.AppendPoint(new Point2D(xRoots[x], yRoots[y]));
                        }
                    }
                }
            }
            if (result.Points.Count > 0)
                result.Status = IntersectionStatus.Intersection; return result;
        }

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="angle"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://csharphelper.com/blog/2012/09/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection EllipseLineSegmentIntersection(
            double cx, double cy,
            double rx, double ry,
            double angle,
            double x0, double y0,
            double x1, double y1)
        {
            var result = new Intersection(IntersectionStatus.NoIntersection);

            // If the ellipse or line segment are empty, return no intersections.
            if ((rx == 0d) || (ry == 0d) ||
                ((x0 == x1) && (y0 == y1)))
                return result;

            // Get the Sine and Cosine of the angle.
            double sinA = Sin(angle);
            double cosA = Cos(angle);

            // Translate the line to put the ellipse centered at the origin.
            double u1 = x0 - cx;
            double v1 = y0 - cy;
            double u2 = x1 - cx;
            double v2 = y1 - cy;

            // Apply Rotation Transform to line at the origin.
            double u1A = (0 + (u1 * cosA - v1 * sinA));
            double v1A = (0 + (u1 * sinA + v1 * cosA));
            double u2A = (0 + (u2 * cosA - v2 * sinA));
            double v2A = (0 + (u2 * sinA + v2 * cosA));

            // Calculate the quadratic parameters.
            double a = (u2A - u1A) * (u2A - u1A) / (rx * rx) + (v2A - v1A) * (v2A - v1A) / (ry * ry);
            double b = 2d * u1A * (u2A - u1A) / (rx * rx) + 2d * v1A * (v2A - v1A) / (ry * ry);
            double c = (u1A * u1A) / (rx * rx) + (v1A * v1A) / (ry * ry) - 1d;

            // Calculate the discriminant.
            double discriminant = b * b - 4d * a * c;

            if ((a <= Epsilon) || (discriminant < 0))
            {
                // No real solutions.
            }
            else if (discriminant == 0)
            {
                // One real possible solution.
                double t = 0.5d * -b / a;

                // Add the point if it is between the end points of the line segment.
                if ((t >= 0d) && (t <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + (u2 - u1) * t + cx, v1 + (v2 - v1) * t + cy));
                    result.Status = IntersectionStatus.Intersection;
                }

            }
            else if (discriminant > 0)
            {
                // Two real possible solutions.
                double t1 = (0.5d * (-b + Sqrt(discriminant)) / a);
                double t2 = (0.5d * (-b - Sqrt(discriminant)) / a);

                // Add the points if they are between the end points of the line segment.
                if ((t1 >= 0d) && (t1 <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + (u2 - u1) * t1 + cx, v1 + (v2 - v1) * t1 + cy));
                    result.Status = IntersectionStatus.Intersection;
                }

                if ((t2 >= 0d) && (t2 <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + (u2 - u1) * t2 + cx, v1 + (v2 - v1) * t2 + cy));
                    result.Status = IntersectionStatus.Intersection;
                }

                // ToDo: Figure out why the results are weird between 30 degrees and 5 degrees.
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p0x"></param>
        /// <param name="p0y"></param>
        /// <param name="p1x"></param>
        /// <param name="p1y"></param>
        /// <param name="p2x"></param>
        /// <param name="p2y"></param>
        /// <param name="p3x"></param>
        /// <param name="p3y"></param>
        /// <param name="l0x"></param>
        /// <param name="l0y"></param>
        /// <param name="l1x"></param>
        /// <param name="l1y"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://www.particleincell.com/2013/cubic-line-intersection/
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierLineSegmentIntersection(
            double p0x, double p0y,
            double p1x, double p1y,
            double p2x, double p2y,
            double p3x, double p3y,
            double l0x, double l0y,
            double l1x, double l1y)
        {
            // ToDo: Figure out why this can't handle intersection with horizontal lines.
            var I = new Intersection(IntersectionStatus.NoIntersection);

            var A = l1y - l0y;      //A=y2-y1
            var B = l0x - l1x;      //B=x1-x2
            var C = l0x * (l0y - l1y) + l0y * (l1x - l0x);  //C=x1*(y1-y2)+y1*(x2-x1)

            var bx = BezierCoefficients(p0x, p1x, p2x, p3x);
            var by = BezierCoefficients(p0y, p1y, p2y, p3y);

            var r = CubicRoots(
                A * bx.A + B * by.A,    /*t^3*/
                A * bx.B + B * by.B,    /*t^2*/
                A * bx.C + B * by.C,    /*t*/
                A * bx.D + B * by.D + C /*1*/
                );

            /*verify the roots are in bounds of the linear segment*/
            for (var i = 0; i < 3; i++)
            {
                double t = r[i];

                double x = bx.A * t * t * t + bx.B * t * t + bx.C * t + bx.D;
                double y = by.A * t * t * t + by.B * t * t + by.C * t + by.D;

                /*above is intersection point assuming infinitely long line segment,
                  make sure we are also in bounds of the line*/
                double m;
                if ((l1x - l0x) != 0)           /*if not vertical line*/
                    m = (x - l0x) / (l1x - l0x);
                else
                    m = (y - l0y) / (l1y - l0y);

                /*in bounds?*/
                if (t < 0 || t > 1d || m < 0 || m > 1d)
                {
                    x = 0;// -100;  /*move off screen*/
                    y = 0;// -100;
                }
                else
                {
                    /*intersection point*/
                    I.AppendPoint(new Point2D(x, y));
                    I.Status = IntersectionStatus.Intersection;
                }
            }
            return I;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection CubicBezierLineIntersection(
            Point2D p1,
            Point2D p2,
            Point2D p3,
            Point2D p4,
            Point2D a1,
            Point2D a2)
        {
            Vector2D a, b, c, d;
            Vector2D c3, c2, c1, c0;
            double cl;
            Vector2D n;
            var min = a1.Min(a2);
            var max = a1.Max(a2);
            var result = new Intersection(IntersectionStatus.NoIntersection);
            a = p1.Scale(-1);
            b = p2.Scale(3);
            c = p3.Scale(-3);
            d = a.Add(b.Add(c.Add(p4)));
            c3 = new Vector2D(d.I, d.J);
            a = p1.Scale(3);
            b = p2.Scale(-6);
            c = p3.Scale(3);
            d = a.Add(b.Add(c));
            c2 = new Vector2D(d.I, d.J);
            a = p1.Scale(-3);
            b = p2.Scale(3);
            c = a.Add(b);
            c1 = new Vector2D(c.I, c.J);
            c0 = new Vector2D(p1.X, p1.Y);
            n = new Vector2D(a1.Y - a2.Y, a2.X - a1.X);
            cl = a1.X * a2.Y - a2.X * a1.Y;
            var roots = new Polynomial(
                n.DotProduct(c0) + cl,
                n.DotProduct(c1),
                n.DotProduct(c2),
                n.DotProduct(c3)
                ).Roots();
            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];
                if (0 <= t && t <= 1)
                {
                    var p5 = p1.Lerp(p2, t);
                    var p6 = p2.Lerp(p3, t);
                    var p7 = p3.Lerp(p4, t);
                    var p8 = p5.Lerp(p6, t);
                    var p9 = p6.Lerp(p7, t);
                    var p10 = p8.Lerp(p9, t);
                    if (a1.X == a2.X)
                    {
                        if (min.Y <= p10.Y && p10.Y <= max.Y)
                        {
                            result.Status = IntersectionStatus.Intersection;
                            result.AppendPoint(p10);
                        }
                    }
                    else if (a1.Y == a2.Y)
                    {
                        if (min.X <= p10.X && p10.X <= max.X)
                        {
                            result.Status = IntersectionStatus.Intersection;
                            result.AppendPoint(p10);
                        }
                    }
                    else if (p10.GreaterThanOrEqual(min) && p10.LessThanOrEqual(max))
                    {
                        result.Status = IntersectionStatus.Intersection;
                        result.AppendPoint(p10);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection CubicBezierRectangleIntersection(
            Point2D p1,
            Point2D p2,
            Point2D p3,
            Point2D p4,
            Point2D r1,
            Point2D r2)
        {
            var min = r1.Min(r2);
            var max = r1.Max(r2);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);
            var inter1 = CubicBezierLineIntersection(p1, p2, p3, p4, min, topRight);
            var inter2 = CubicBezierLineIntersection(p1, p2, p3, p4, topRight, max);
            var inter3 = CubicBezierLineIntersection(p1, p2, p3, p4, max, bottomLeft);
            var inter4 = CubicBezierLineIntersection(p1, p2, p3, p4, bottomLeft, min);
            var result = new Intersection(IntersectionStatus.NoIntersection);
            result.AppendPoints(inter1.Points);
            result.AppendPoints(inter2.Points);
            result.AppendPoints(inter3.Points);
            result.AppendPoints(inter4.Points);
            if (result.Points.Count > 0)
                result.Status = IntersectionStatus.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection CubicBezierPolygonIntersection(
            Point2D p1,
            Point2D p2,
            Point2D p3,
            Point2D p4,
            List<Point2D> points)
        {
            var result = new Intersection(IntersectionStatus.NoIntersection);
            var length = points.Count;
            for (var i = 0; i < length; i++)
            {
                var a1 = points[i];
                var a2 = points[(i + 1) % length];
                var inter = CubicBezierLineIntersection(p1, p2, p3, p4, a1, a2);
                result.AppendPoints(inter.Points);
            }
            if (result.Points.Count > 0)
                result.Status = IntersectionStatus.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <param name="c"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection CubicBezierCircleIntersection(
            Point2D p1,
            Point2D p2,
            Point2D p3,
            Point2D p4,
            Point2D c,
            double r)
            => CubicBezierUnrotatedEllipseIntersection(p1, p2, p3, p4, c, r, r);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <param name="ec"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection CubicBezierUnrotatedEllipseIntersection(
            Point2D p1,
            Point2D p2,
            Point2D p3,
            Point2D p4,
            Point2D ec,
            double rx,
            double ry)
        {
            Vector2D a, b, c, d;
            Vector2D c3, c2, c1, c0;
            var result = new Intersection(IntersectionStatus.NoIntersection);
            a = p1.Scale(-1);
            b = p2.Scale(3);
            c = p3.Scale(-3);
            d = a.Add(b.Add(c.Add(p4)));
            c3 = new Vector2D(d.I, d.J);
            a = p1.Scale(3);
            b = p2.Scale(-6);
            c = p3.Scale(3);
            d = a.Add(b.Add(c));
            c2 = new Vector2D(d.I, d.J);
            a = p1.Scale(-3);
            b = p2.Scale(3);
            c = a.Add(b);
            c1 = new Vector2D(c.I, c.J);
            c0 = new Vector2D(p1.X, p1.Y);
            var rxrx = rx * rx;
            var ryry = ry * ry;
            var poly = new Polynomial(
                c0.I * c0.I * ryry - 2 * c0.J * ec.Y * rxrx - 2 * c0.I * ec.X * ryry + c0.J * c0.J * rxrx + ec.X * ec.X * ryry + ec.Y * ec.Y * rxrx - rxrx * ryry,
                2 * c1.I * ryry * (c0.I - ec.X) + 2 * c1.J * rxrx * (c0.J - ec.Y),
                2 * c2.I * ryry * (c0.I - ec.X) + 2 * c2.J * rxrx * (c0.J - ec.Y) + c1.I * c1.I * ryry + c1.J * c1.J * rxrx,
                2 * c3.I * ryry * (c0.I - ec.X) + 2 * c3.J * rxrx * (c0.J - ec.Y) + 2 * (c2.I * c1.I * ryry + c2.J * c1.J * rxrx),
                2 * (c3.I * c1.I * ryry + c3.J * c1.J * rxrx) + c2.I * c2.I * ryry + c2.J * c2.J * rxrx,
                2 * (c3.I * c2.I * ryry + c3.J * c2.J * rxrx),
                c3.I * c3.I * ryry + c3.J * c3.J * rxrx);
            var roots = poly.RootsInInterval(0, 1);
            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];
                result.Points.Add((Point2D)c3.Scale(t * t * t).Add(c2.Scale(t * t).Add(c1.Scale(t).Add(c0))));
            }
            if (result.Points.Count > 0)
                result.Status = IntersectionStatus.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <param name="a3"></param>
        /// <param name="a4"></param>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <param name="b3"></param>
        /// <param name="b4"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection CubicBezierCubicBezierIntersection(
            Point2D a1,
            Point2D a2,
            Point2D a3,
            Point2D a4,
            Point2D b1,
            Point2D b2,
            Point2D b3,
            Point2D b4, double epsilon = Epsilon)
        {
            Vector2D a, b, c, d;
            Vector2D c13, c12, c11, c10;
            Vector2D c23, c22, c21, c20;
            var result = new Intersection(IntersectionStatus.NoIntersection);
            a = a1.Scale(-1);
            b = a2.Scale(3);
            c = a3.Scale(-3);
            d = a.Add(b.Add(c.Add(a4)));
            c13 = new Vector2D(d.I, d.J);
            a = a1.Scale(3);
            b = a2.Scale(-6);
            c = a3.Scale(3);
            d = a.Add(b.Add(c));
            c12 = new Vector2D(d.I, d.J);
            a = a1.Scale(-3);
            b = a2.Scale(3);
            c = a.Add(b);
            c11 = new Vector2D(c.I, c.J);
            c10 = new Vector2D(a1.X, a1.Y);
            a = b1.Scale(-1);
            b = b2.Scale(3);
            c = b3.Scale(-3);
            d = a.Add(b.Add(c.Add(b4)));
            c23 = new Vector2D(d.I, d.J);
            a = b1.Scale(3);
            b = b2.Scale(-6);
            c = b3.Scale(3);
            d = a.Add(b.Add(c));
            c22 = new Vector2D(d.I, d.J);
            a = b1.Scale(-3);
            b = b2.Scale(3);
            c = a.Add(b);
            c21 = new Vector2D(c.I, c.J);
            c20 = new Vector2D(b1.X, b1.Y);
            var c10x2 = c10.I * c10.I;
            var c10x3 = c10.I * c10.I * c10.I;
            var c10y2 = c10.J * c10.J;
            var c10y3 = c10.J * c10.J * c10.J;
            var c11x2 = c11.I * c11.I;
            var c11x3 = c11.I * c11.I * c11.I;
            var c11y2 = c11.J * c11.J;
            var c11y3 = c11.J * c11.J * c11.J;
            var c12x2 = c12.I * c12.I;
            var c12x3 = c12.I * c12.I * c12.I;
            var c12y2 = c12.J * c12.J;
            var c12y3 = c12.J * c12.J * c12.J;
            var c13x2 = c13.I * c13.I;
            var c13x3 = c13.I * c13.I * c13.I;
            var c13y2 = c13.J * c13.J;
            var c13y3 = c13.J * c13.J * c13.J;
            var c20x2 = c20.I * c20.I;
            var c20x3 = c20.I * c20.I * c20.I;
            var c20y2 = c20.J * c20.J;
            var c20y3 = c20.J * c20.J * c20.J;
            var c21x2 = c21.I * c21.I;
            var c21x3 = c21.I * c21.I * c21.I;
            var c21y2 = c21.J * c21.J;
            var c22x2 = c22.I * c22.I;
            var c22x3 = c22.I * c22.I * c22.I;
            var c22y2 = c22.J * c22.J;
            var c23x2 = c23.I * c23.I;
            var c23x3 = c23.I * c23.I * c23.I;
            var c23y2 = c23.J * c23.J;
            var c23y3 = c23.J * c23.J * c23.J;
            var poly = new Polynomial(
                c10.I * c10.J * c11.I * c12.J * c13.I * c13.J - c10.I * c10.J * c11.J * c12.I * c13.I * c13.J + c10.I * c11.I * c11.J * c12.I * c12.J * c13.J - c10.J * c11.I * c11.J * c12.I * c12.J * c13.I - c10.I * c11.I * c20.J * c12.J * c13.I * c13.J + 6 * c10.I * c20.I * c11.J * c12.J * c13.I * c13.J + c10.I * c11.J * c12.I * c20.J * c13.I * c13.J - c10.J * c11.I * c20.I * c12.J * c13.I * c13.J - 6 * c10.J * c11.I * c12.I * c20.J * c13.I * c13.J + c10.J * c20.I * c11.J * c12.I * c13.I * c13.J - c11.I * c20.I * c11.J * c12.I * c12.J * c13.J + c11.I * c11.J * c12.I * c20.J * c12.J * c13.I + c11.I * c20.I * c20.J * c12.J * c13.I * c13.J - c20.I * c11.J * c12.I * c20.J * c13.I * c13.J - 2 * c10.I * c20.I * c12y3 * c13.I + 2 * c10.J * c12x3 * c20.J * c13.J - 3 * c10.I * c10.J * c11.I * c12.I * c13y2 - 6 * c10.I * c10.J * c20.I * c13.I * c13y2 + 3 * c10.I * c10.J * c11.J * c12.J * c13x2 - 2 * c10.I * c10.J * c12.I * c12y2 * c13.I - 2 * c10.I * c11.I * c20.I * c12.J * c13y2 - c10.I * c11.I * c11.J * c12y2 * c13.I + 3 * c10.I * c11.I * c12.I * c20.J * c13y2 - 4 * c10.I * c20.I * c11.J * c12.I * c13y2 + 3 * c10.J * c11.I * c20.I * c12.I * c13y2 + 6 * c10.I * c10.J * c20.J * c13x2 * c13.J + 2 * c10.I * c10.J * c12x2 * c12.J * c13.J + 2 * c10.I * c11.I * c11y2 * c13.I * c13.J + 2 * c10.I * c20.I * c12.I * c12y2 * c13.J + 6 * c10.I * c20.I * c20.J * c13.I * c13y2 - 3 * c10.I * c11.J * c20.J * c12.J * c13x2 + 2 * c10.I * c12.I * c20.J * c12y2 * c13.I + c10.I * c11y2 * c12.I * c12.J * c13.I + c10.J * c11.I * c11.J * c12x2 * c13.J + 4 * c10.J * c11.I * c20.J * c12.J * c13x2 - 3 * c10.J * c20.I * c11.J * c12.J * c13x2 + 2 * c10.J * c20.I * c12.I * c12y2 * c13.I + 2 * c10.J * c11.J * c12.I * c20.J * c13x2 + c11.I * c20.I * c11.J * c12y2 * c13.I - 3 * c11.I * c20.I * c12.I * c20.J * c13y2 - 2 * c10.I * c12x2 * c20.J * c12.J * c13.J - 6 * c10.J * c20.I * c20.J * c13x2 * c13.J - 2 * c10.J * c20.I * c12x2 * c12.J * c13.J - 2 * c10.J * c11x2 * c11.J * c13.I * c13.J - c10.J * c11x2 * c12.I * c12.J * c13.J - 2 * c10.J * c12x2 * c20.J * c12.J * c13.I - 2 * c11.I * c20.I * c11y2 * c13.I * c13.J - c11.I * c11.J * c12x2 * c20.J * c13.J + 3 * c20.I * c11.J * c20.J * c12.J * c13x2 - 2 * c20.I * c12.I * c20.J * c12y2 * c13.I - c20.I * c11y2 * c12.I * c12.J * c13.I + 3 * c10y2 * c11.I * c12.I * c13.I * c13.J + 3 * c11.I * c12.I * c20y2 * c13.I * c13.J + 2 * c20.I * c12x2 * c20.J * c12.J * c13.J - 3 * c10x2 * c11.J * c12.J * c13.I * c13.J + 2 * c11x2 * c11.J * c20.J * c13.I * c13.J + c11x2 * c12.I * c20.J * c12.J * c13.J - 3 * c20x2 * c11.J * c12.J * c13.I * c13.J - c10x3 * c13y3 + c10y3 * c13x3 + c20x3 * c13y3 - c20y3 * c13x3 - 3 * c10.I * c20x2 * c13y3 - c10.I * c11y3 * c13x2 + 3 * c10x2 * c20.I * c13y3 + c10.J * c11x3 * c13y2 + 3 * c10.J * c20y2 * c13x3 + c20.I * c11y3 * c13x2 + c10x2 * c12y3 * c13.I - 3 * c10y2 * c20.J * c13x3 - c10y2 * c12x3 * c13.J + c20x2 * c12y3 * c13.I - c11x3 * c20.J * c13y2 - c12x3 * c20y2 * c13.J - c10.I * c11x2 * c11.J * c13y2 + c10.J * c11.I * c11y2 * c13x2 - 3 * c10.I * c10y2 * c13x2 * c13.J - c10.I * c11y2 * c12x2 * c13.J + c10.J * c11x2 * c12y2 * c13.I - c11.I * c11y2 * c20.J * c13x2 + 3 * c10x2 * c10.J * c13.I * c13y2 + c10x2 * c11.I * c12.J * c13y2 + 2 * c10x2 * c11.J * c12.I * c13y2 - 2 * c10y2 * c11.I * c12.J * c13x2 - c10y2 * c11.J * c12.I * c13x2 + c11x2 * c20.I * c11.J * c13y2 - 3 * c10.I * c20y2 * c13x2 * c13.J + 3 * c10.J * c20x2 * c13.I * c13y2 + c11.I * c20x2 * c12.J * c13y2 - 2 * c11.I * c20y2 * c12.J * c13x2 + c20.I * c11y2 * c12x2 * c13.J - c11.J * c12.I * c20y2 * c13x2 - c10x2 * c12.I * c12y2 * c13.J - 3 * c10x2 * c20.J * c13.I * c13y2 + 3 * c10y2 * c20.I * c13x2 * c13.J + c10y2 * c12x2 * c12.J * c13.I - c11x2 * c20.J * c12y2 * c13.I + 2 * c20x2 * c11.J * c12.I * c13y2 + 3 * c20.I * c20y2 * c13x2 * c13.J - c20x2 * c12.I * c12y2 * c13.J - 3 * c20x2 * c20.J * c13.I * c13y2 + c12x2 * c20y2 * c12.J * c13.I,
                -c10.I * c11.I * c12.J * c13.I * c21.J * c13.J + c10.I * c11.J * c12.I * c13.I * c21.J * c13.J + 6 * c10.I * c11.J * c21.I * c12.J * c13.I * c13.J - 6 * c10.J * c11.I * c12.I * c13.I * c21.J * c13.J - c10.J * c11.I * c21.I * c12.J * c13.I * c13.J + c10.J * c11.J * c12.I * c21.I * c13.I * c13.J - c11.I * c11.J * c12.I * c21.I * c12.J * c13.J + c11.I * c11.J * c12.I * c12.J * c13.I * c21.J + c11.I * c20.I * c12.J * c13.I * c21.J * c13.J + 6 * c11.I * c12.I * c20.J * c13.I * c21.J * c13.J + c11.I * c20.J * c21.I * c12.J * c13.I * c13.J - c20.I * c11.J * c12.I * c13.I * c21.J * c13.J - 6 * c20.I * c11.J * c21.I * c12.J * c13.I * c13.J - c11.J * c12.I * c20.J * c21.I * c13.I * c13.J - 6 * c10.I * c20.I * c21.I * c13y3 - 2 * c10.I * c21.I * c12y3 * c13.I + 6 * c10.J * c20.J * c13x3 * c21.J + 2 * c20.I * c21.I * c12y3 * c13.I + 2 * c10.J * c12x3 * c21.J * c13.J - 2 * c12x3 * c20.J * c21.J * c13.J - 6 * c10.I * c10.J * c21.I * c13.I * c13y2 + 3 * c10.I * c11.I * c12.I * c21.J * c13y2 - 2 * c10.I * c11.I * c21.I * c12.J * c13y2 - 4 * c10.I * c11.J * c12.I * c21.I * c13y2 + 3 * c10.J * c11.I * c12.I * c21.I * c13y2 + 6 * c10.I * c10.J * c13x2 * c21.J * c13.J + 6 * c10.I * c20.I * c13.I * c21.J * c13y2 - 3 * c10.I * c11.J * c12.J * c13x2 * c21.J + 2 * c10.I * c12.I * c21.I * c12y2 * c13.J + 2 * c10.I * c12.I * c12y2 * c13.I * c21.J + 6 * c10.I * c20.J * c21.I * c13.I * c13y2 + 4 * c10.J * c11.I * c12.J * c13x2 * c21.J + 6 * c10.J * c20.I * c21.I * c13.I * c13y2 + 2 * c10.J * c11.J * c12.I * c13x2 * c21.J - 3 * c10.J * c11.J * c21.I * c12.J * c13x2 + 2 * c10.J * c12.I * c21.I * c12y2 * c13.I - 3 * c11.I * c20.I * c12.I * c21.J * c13y2 + 2 * c11.I * c20.I * c21.I * c12.J * c13y2 + c11.I * c11.J * c21.I * c12y2 * c13.I - 3 * c11.I * c12.I * c20.J * c21.I * c13y2 + 4 * c20.I * c11.J * c12.I * c21.I * c13y2 - 6 * c10.I * c20.J * c13x2 * c21.J * c13.J - 2 * c10.I * c12x2 * c12.J * c21.J * c13.J - 6 * c10.J * c20.I * c13x2 * c21.J * c13.J - 6 * c10.J * c20.J * c21.I * c13x2 * c13.J - 2 * c10.J * c12x2 * c21.I * c12.J * c13.J - 2 * c10.J * c12x2 * c12.J * c13.I * c21.J - c11.I * c11.J * c12x2 * c21.J * c13.J - 4 * c11.I * c20.J * c12.J * c13x2 * c21.J - 2 * c11.I * c11y2 * c21.I * c13.I * c13.J + 3 * c20.I * c11.J * c12.J * c13x2 * c21.J - 2 * c20.I * c12.I * c21.I * c12y2 * c13.J - 2 * c20.I * c12.I * c12y2 * c13.I * c21.J - 6 * c20.I * c20.J * c21.I * c13.I * c13y2 - 2 * c11.J * c12.I * c20.J * c13x2 * c21.J + 3 * c11.J * c20.J * c21.I * c12.J * c13x2 - 2 * c12.I * c20.J * c21.I * c12y2 * c13.I - c11y2 * c12.I * c21.I * c12.J * c13.I + 6 * c20.I * c20.J * c13x2 * c21.J * c13.J + 2 * c20.I * c12x2 * c12.J * c21.J * c13.J + 2 * c11x2 * c11.J * c13.I * c21.J * c13.J + c11x2 * c12.I * c12.J * c21.J * c13.J + 2 * c12x2 * c20.J * c21.I * c12.J * c13.J + 2 * c12x2 * c20.J * c12.J * c13.I * c21.J + 3 * c10x2 * c21.I * c13y3 - 3 * c10y2 * c13x3 * c21.J + 3 * c20x2 * c21.I * c13y3 + c11y3 * c21.I * c13x2 - c11x3 * c21.J * c13y2 - 3 * c20y2 * c13x3 * c21.J - c11.I * c11y2 * c13x2 * c21.J + c11x2 * c11.J * c21.I * c13y2 - 3 * c10x2 * c13.I * c21.J * c13y2 + 3 * c10y2 * c21.I * c13x2 * c13.J - c11x2 * c12y2 * c13.I * c21.J + c11y2 * c12x2 * c21.I * c13.J - 3 * c20x2 * c13.I * c21.J * c13y2 + 3 * c20y2 * c21.I * c13x2 * c13.J,
                -c10.I * c11.I * c12.J * c13.I * c13.J * c22.J + c10.I * c11.J * c12.I * c13.I * c13.J * c22.J + 6 * c10.I * c11.J * c12.J * c13.I * c22.I * c13.J - 6 * c10.J * c11.I * c12.I * c13.I * c13.J * c22.J - c10.J * c11.I * c12.J * c13.I * c22.I * c13.J + c10.J * c11.J * c12.I * c13.I * c22.I * c13.J + c11.I * c11.J * c12.I * c12.J * c13.I * c22.J - c11.I * c11.J * c12.I * c12.J * c22.I * c13.J + c11.I * c20.I * c12.J * c13.I * c13.J * c22.J + c11.I * c20.J * c12.J * c13.I * c22.I * c13.J + c11.I * c21.I * c12.J * c13.I * c21.J * c13.J - c20.I * c11.J * c12.I * c13.I * c13.J * c22.J - 6 * c20.I * c11.J * c12.J * c13.I * c22.I * c13.J - c11.J * c12.I * c20.J * c13.I * c22.I * c13.J - c11.J * c12.I * c21.I * c13.I * c21.J * c13.J - 6 * c10.I * c20.I * c22.I * c13y3 - 2 * c10.I * c12y3 * c13.I * c22.I + 2 * c20.I * c12y3 * c13.I * c22.I + 2 * c10.J * c12x3 * c13.J * c22.J - 6 * c10.I * c10.J * c13.I * c22.I * c13y2 + 3 * c10.I * c11.I * c12.I * c13y2 * c22.J - 2 * c10.I * c11.I * c12.J * c22.I * c13y2 - 4 * c10.I * c11.J * c12.I * c22.I * c13y2 + 3 * c10.J * c11.I * c12.I * c22.I * c13y2 + 6 * c10.I * c10.J * c13x2 * c13.J * c22.J + 6 * c10.I * c20.I * c13.I * c13y2 * c22.J - 3 * c10.I * c11.J * c12.J * c13x2 * c22.J + 2 * c10.I * c12.I * c12y2 * c13.I * c22.J + 2 * c10.I * c12.I * c12y2 * c22.I * c13.J + 6 * c10.I * c20.J * c13.I * c22.I * c13y2 + 6 * c10.I * c21.I * c13.I * c21.J * c13y2 + 4 * c10.J * c11.I * c12.J * c13x2 * c22.J + 6 * c10.J * c20.I * c13.I * c22.I * c13y2 + 2 * c10.J * c11.J * c12.I * c13x2 * c22.J - 3 * c10.J * c11.J * c12.J * c13x2 * c22.I + 2 * c10.J * c12.I * c12y2 * c13.I * c22.I - 3 * c11.I * c20.I * c12.I * c13y2 * c22.J + 2 * c11.I * c20.I * c12.J * c22.I * c13y2 + c11.I * c11.J * c12y2 * c13.I * c22.I - 3 * c11.I * c12.I * c20.J * c22.I * c13y2 - 3 * c11.I * c12.I * c21.I * c21.J * c13y2 + 4 * c20.I * c11.J * c12.I * c22.I * c13y2 - 2 * c10.I * c12x2 * c12.J * c13.J * c22.J - 6 * c10.J * c20.I * c13x2 * c13.J * c22.J - 6 * c10.J * c20.J * c13x2 * c22.I * c13.J - 6 * c10.J * c21.I * c13x2 * c21.J * c13.J - 2 * c10.J * c12x2 * c12.J * c13.I * c22.J - 2 * c10.J * c12x2 * c12.J * c22.I * c13.J - c11.I * c11.J * c12x2 * c13.J * c22.J - 2 * c11.I * c11y2 * c13.I * c22.I * c13.J + 3 * c20.I * c11.J * c12.J * c13x2 * c22.J - 2 * c20.I * c12.I * c12y2 * c13.I * c22.J - 2 * c20.I * c12.I * c12y2 * c22.I * c13.J - 6 * c20.I * c20.J * c13.I * c22.I * c13y2 - 6 * c20.I * c21.I * c13.I * c21.J * c13y2 + 3 * c11.J * c20.J * c12.J * c13x2 * c22.I + 3 * c11.J * c21.I * c12.J * c13x2 * c21.J - 2 * c12.I * c20.J * c12y2 * c13.I * c22.I - 2 * c12.I * c21.I * c12y2 * c13.I * c21.J - c11y2 * c12.I * c12.J * c13.I * c22.I + 2 * c20.I * c12x2 * c12.J * c13.J * c22.J - 3 * c11.J * c21x2 * c12.J * c13.I * c13.J + 6 * c20.J * c21.I * c13x2 * c21.J * c13.J + 2 * c11x2 * c11.J * c13.I * c13.J * c22.J + c11x2 * c12.I * c12.J * c13.J * c22.J + 2 * c12x2 * c20.J * c12.J * c22.I * c13.J + 2 * c12x2 * c21.I * c12.J * c21.J * c13.J - 3 * c10.I * c21x2 * c13y3 + 3 * c20.I * c21x2 * c13y3 + 3 * c10x2 * c22.I * c13y3 - 3 * c10y2 * c13x3 * c22.J + 3 * c20x2 * c22.I * c13y3 + c21x2 * c12y3 * c13.I + c11y3 * c13x2 * c22.I - c11x3 * c13y2 * c22.J + 3 * c10.J * c21x2 * c13.I * c13y2 - c11.I * c11y2 * c13x2 * c22.J + c11.I * c21x2 * c12.J * c13y2 + 2 * c11.J * c12.I * c21x2 * c13y2 + c11x2 * c11.J * c22.I * c13y2 - c12.I * c21x2 * c12y2 * c13.J - 3 * c20.J * c21x2 * c13.I * c13y2 - 3 * c10x2 * c13.I * c13y2 * c22.J + 3 * c10y2 * c13x2 * c22.I * c13.J - c11x2 * c12y2 * c13.I * c22.J + c11y2 * c12x2 * c22.I * c13.J - 3 * c20x2 * c13.I * c13y2 * c22.J + 3 * c20y2 * c13x2 * c22.I * c13.J + c12x2 * c12.J * c13.I * (2 * c20.J * c22.J + c21y2) + c11.I * c12.I * c13.I * c13.J * (6 * c20.J * c22.J + 3 * c21y2) + c12x3 * c13.J * (-2 * c20.J * c22.J - c21y2) + c10.J * c13x3 * (6 * c20.J * c22.J + 3 * c21y2) + c11.J * c12.I * c13x2 * (-2 * c20.J * c22.J - c21y2) + c11.I * c12.J * c13x2 * (-4 * c20.J * c22.J - 2 * c21y2) + c10.I * c13x2 * c13.J * (-6 * c20.J * c22.J - 3 * c21y2) + c20.I * c13x2 * c13.J * (6 * c20.J * c22.J + 3 * c21y2) + c13x3 * (-2 * c20.J * c21y2 - c20y2 * c22.J - c20.J * (2 * c20.J * c22.J + c21y2)),
                -c10.I * c11.I * c12.J * c13.I * c13.J * c23.J + c10.I * c11.J * c12.I * c13.I * c13.J * c23.J + 6 * c10.I * c11.J * c12.J * c13.I * c13.J * c23.I - 6 * c10.J * c11.I * c12.I * c13.I * c13.J * c23.J - c10.J * c11.I * c12.J * c13.I * c13.J * c23.I + c10.J * c11.J * c12.I * c13.I * c13.J * c23.I + c11.I * c11.J * c12.I * c12.J * c13.I * c23.J - c11.I * c11.J * c12.I * c12.J * c13.J * c23.I + c11.I * c20.I * c12.J * c13.I * c13.J * c23.J + c11.I * c20.J * c12.J * c13.I * c13.J * c23.I + c11.I * c21.I * c12.J * c13.I * c13.J * c22.J + c11.I * c12.J * c13.I * c21.J * c22.I * c13.J - c20.I * c11.J * c12.I * c13.I * c13.J * c23.J - 6 * c20.I * c11.J * c12.J * c13.I * c13.J * c23.I - c11.J * c12.I * c20.J * c13.I * c13.J * c23.I - c11.J * c12.I * c21.I * c13.I * c13.J * c22.J - c11.J * c12.I * c13.I * c21.J * c22.I * c13.J - 6 * c11.J * c21.I * c12.J * c13.I * c22.I * c13.J - 6 * c10.I * c20.I * c13y3 * c23.I - 6 * c10.I * c21.I * c22.I * c13y3 - 2 * c10.I * c12y3 * c13.I * c23.I + 6 * c20.I * c21.I * c22.I * c13y3 + 2 * c20.I * c12y3 * c13.I * c23.I + 2 * c21.I * c12y3 * c13.I * c22.I + 2 * c10.J * c12x3 * c13.J * c23.J - 6 * c10.I * c10.J * c13.I * c13y2 * c23.I + 3 * c10.I * c11.I * c12.I * c13y2 * c23.J - 2 * c10.I * c11.I * c12.J * c13y2 * c23.I - 4 * c10.I * c11.J * c12.I * c13y2 * c23.I + 3 * c10.J * c11.I * c12.I * c13y2 * c23.I + 6 * c10.I * c10.J * c13x2 * c13.J * c23.J + 6 * c10.I * c20.I * c13.I * c13y2 * c23.J - 3 * c10.I * c11.J * c12.J * c13x2 * c23.J + 2 * c10.I * c12.I * c12y2 * c13.I * c23.J + 2 * c10.I * c12.I * c12y2 * c13.J * c23.I + 6 * c10.I * c20.J * c13.I * c13y2 * c23.I + 6 * c10.I * c21.I * c13.I * c13y2 * c22.J + 6 * c10.I * c13.I * c21.J * c22.I * c13y2 + 4 * c10.J * c11.I * c12.J * c13x2 * c23.J + 6 * c10.J * c20.I * c13.I * c13y2 * c23.I + 2 * c10.J * c11.J * c12.I * c13x2 * c23.J - 3 * c10.J * c11.J * c12.J * c13x2 * c23.I + 2 * c10.J * c12.I * c12y2 * c13.I * c23.I + 6 * c10.J * c21.I * c13.I * c22.I * c13y2 - 3 * c11.I * c20.I * c12.I * c13y2 * c23.J + 2 * c11.I * c20.I * c12.J * c13y2 * c23.I + c11.I * c11.J * c12y2 * c13.I * c23.I - 3 * c11.I * c12.I * c20.J * c13y2 * c23.I - 3 * c11.I * c12.I * c21.I * c13y2 * c22.J - 3 * c11.I * c12.I * c21.J * c22.I * c13y2 + 2 * c11.I * c21.I * c12.J * c22.I * c13y2 + 4 * c20.I * c11.J * c12.I * c13y2 * c23.I + 4 * c11.J * c12.I * c21.I * c22.I * c13y2 - 2 * c10.I * c12x2 * c12.J * c13.J * c23.J - 6 * c10.J * c20.I * c13x2 * c13.J * c23.J - 6 * c10.J * c20.J * c13x2 * c13.J * c23.I - 6 * c10.J * c21.I * c13x2 * c13.J * c22.J - 2 * c10.J * c12x2 * c12.J * c13.I * c23.J - 2 * c10.J * c12x2 * c12.J * c13.J * c23.I - 6 * c10.J * c13x2 * c21.J * c22.I * c13.J - c11.I * c11.J * c12x2 * c13.J * c23.J - 2 * c11.I * c11y2 * c13.I * c13.J * c23.I + 3 * c20.I * c11.J * c12.J * c13x2 * c23.J - 2 * c20.I * c12.I * c12y2 * c13.I * c23.J - 2 * c20.I * c12.I * c12y2 * c13.J * c23.I - 6 * c20.I * c20.J * c13.I * c13y2 * c23.I - 6 * c20.I * c21.I * c13.I * c13y2 * c22.J - 6 * c20.I * c13.I * c21.J * c22.I * c13y2 + 3 * c11.J * c20.J * c12.J * c13x2 * c23.I + 3 * c11.J * c21.I * c12.J * c13x2 * c22.J + 3 * c11.J * c12.J * c13x2 * c21.J * c22.I - 2 * c12.I * c20.J * c12y2 * c13.I * c23.I - 2 * c12.I * c21.I * c12y2 * c13.I * c22.J - 2 * c12.I * c21.I * c12y2 * c22.I * c13.J - 2 * c12.I * c12y2 * c13.I * c21.J * c22.I - 6 * c20.J * c21.I * c13.I * c22.I * c13y2 - c11y2 * c12.I * c12.J * c13.I * c23.I + 2 * c20.I * c12x2 * c12.J * c13.J * c23.J + 6 * c20.J * c13x2 * c21.J * c22.I * c13.J + 2 * c11x2 * c11.J * c13.I * c13.J * c23.J + c11x2 * c12.I * c12.J * c13.J * c23.J + 2 * c12x2 * c20.J * c12.J * c13.J * c23.I + 2 * c12x2 * c21.I * c12.J * c13.J * c22.J + 2 * c12x2 * c12.J * c21.J * c22.I * c13.J + c21x3 * c13y3 + 3 * c10x2 * c13y3 * c23.I - 3 * c10y2 * c13x3 * c23.J + 3 * c20x2 * c13y3 * c23.I + c11y3 * c13x2 * c23.I - c11x3 * c13y2 * c23.J - c11.I * c11y2 * c13x2 * c23.J + c11x2 * c11.J * c13y2 * c23.I - 3 * c10x2 * c13.I * c13y2 * c23.J + 3 * c10y2 * c13x2 * c13.J * c23.I - c11x2 * c12y2 * c13.I * c23.J + c11y2 * c12x2 * c13.J * c23.I - 3 * c21x2 * c13.I * c21.J * c13y2 - 3 * c20x2 * c13.I * c13y2 * c23.J + 3 * c20y2 * c13x2 * c13.J * c23.I + c11.I * c12.I * c13.I * c13.J * (6 * c20.J * c23.J + 6 * c21.J * c22.J) + c12x3 * c13.J * (-2 * c20.J * c23.J - 2 * c21.J * c22.J) + c10.J * c13x3 * (6 * c20.J * c23.J + 6 * c21.J * c22.J) + c11.J * c12.I * c13x2 * (-2 * c20.J * c23.J - 2 * c21.J * c22.J) + c12x2 * c12.J * c13.I * (2 * c20.J * c23.J + 2 * c21.J * c22.J) + c11.I * c12.J * c13x2 * (-4 * c20.J * c23.J - 4 * c21.J * c22.J) + c10.I * c13x2 * c13.J * (-6 * c20.J * c23.J - 6 * c21.J * c22.J) + c20.I * c13x2 * c13.J * (6 * c20.J * c23.J + 6 * c21.J * c22.J) + c21.I * c13x2 * c13.J * (6 * c20.J * c22.J + 3 * c21y2) + c13x3 * (-2 * c20.J * c21.J * c22.J - c20y2 * c23.J - c21.J * (2 * c20.J * c22.J + c21y2) - c20.J * (2 * c20.J * c23.J + 2 * c21.J * c22.J)),
                c11.I * c21.I * c12.J * c13.I * c13.J * c23.J + c11.I * c12.J * c13.I * c21.J * c13.J * c23.I + c11.I * c12.J * c13.I * c22.I * c13.J * c22.J - c11.J * c12.I * c21.I * c13.I * c13.J * c23.J - c11.J * c12.I * c13.I * c21.J * c13.J * c23.I - c11.J * c12.I * c13.I * c22.I * c13.J * c22.J - 6 * c11.J * c21.I * c12.J * c13.I * c13.J * c23.I - 6 * c10.I * c21.I * c13y3 * c23.I + 6 * c20.I * c21.I * c13y3 * c23.I + 2 * c21.I * c12y3 * c13.I * c23.I + 6 * c10.I * c21.I * c13.I * c13y2 * c23.J + 6 * c10.I * c13.I * c21.J * c13y2 * c23.I + 6 * c10.I * c13.I * c22.I * c13y2 * c22.J + 6 * c10.J * c21.I * c13.I * c13y2 * c23.I - 3 * c11.I * c12.I * c21.I * c13y2 * c23.J - 3 * c11.I * c12.I * c21.J * c13y2 * c23.I - 3 * c11.I * c12.I * c22.I * c13y2 * c22.J + 2 * c11.I * c21.I * c12.J * c13y2 * c23.I + 4 * c11.J * c12.I * c21.I * c13y2 * c23.I - 6 * c10.J * c21.I * c13x2 * c13.J * c23.J - 6 * c10.J * c13x2 * c21.J * c13.J * c23.I - 6 * c10.J * c13x2 * c22.I * c13.J * c22.J - 6 * c20.I * c21.I * c13.I * c13y2 * c23.J - 6 * c20.I * c13.I * c21.J * c13y2 * c23.I - 6 * c20.I * c13.I * c22.I * c13y2 * c22.J + 3 * c11.J * c21.I * c12.J * c13x2 * c23.J - 3 * c11.J * c12.J * c13.I * c22x2 * c13.J + 3 * c11.J * c12.J * c13x2 * c21.J * c23.I + 3 * c11.J * c12.J * c13x2 * c22.I * c22.J - 2 * c12.I * c21.I * c12y2 * c13.I * c23.J - 2 * c12.I * c21.I * c12y2 * c13.J * c23.I - 2 * c12.I * c12y2 * c13.I * c21.J * c23.I - 2 * c12.I * c12y2 * c13.I * c22.I * c22.J - 6 * c20.J * c21.I * c13.I * c13y2 * c23.I - 6 * c21.I * c13.I * c21.J * c22.I * c13y2 + 6 * c20.J * c13x2 * c21.J * c13.J * c23.I + 2 * c12x2 * c21.I * c12.J * c13.J * c23.J + 2 * c12x2 * c12.J * c21.J * c13.J * c23.I + 2 * c12x2 * c12.J * c22.I * c13.J * c22.J - 3 * c10.I * c22x2 * c13y3 + 3 * c20.I * c22x2 * c13y3 + 3 * c21x2 * c22.I * c13y3 + c12y3 * c13.I * c22x2 + 3 * c10.J * c13.I * c22x2 * c13y2 + c11.I * c12.J * c22x2 * c13y2 + 2 * c11.J * c12.I * c22x2 * c13y2 - c12.I * c12y2 * c22x2 * c13.J - 3 * c20.J * c13.I * c22x2 * c13y2 - 3 * c21x2 * c13.I * c13y2 * c22.J + c12x2 * c12.J * c13.I * (2 * c21.J * c23.J + c22y2) + c11.I * c12.I * c13.I * c13.J * (6 * c21.J * c23.J + 3 * c22y2) + c21.I * c13x2 * c13.J * (6 * c20.J * c23.J + 6 * c21.J * c22.J) + c12x3 * c13.J * (-2 * c21.J * c23.J - c22y2) + c10.J * c13x3 * (6 * c21.J * c23.J + 3 * c22y2) + c11.J * c12.I * c13x2 * (-2 * c21.J * c23.J - c22y2) + c11.I * c12.J * c13x2 * (-4 * c21.J * c23.J - 2 * c22y2) + c10.I * c13x2 * c13.J * (-6 * c21.J * c23.J - 3 * c22y2) + c13x2 * c22.I * c13.J * (6 * c20.J * c22.J + 3 * c21y2) + c20.I * c13x2 * c13.J * (6 * c21.J * c23.J + 3 * c22y2) + c13x3 * (-2 * c20.J * c21.J * c23.J - c22.J * (2 * c20.J * c22.J + c21y2) - c20.J * (2 * c21.J * c23.J + c22y2) - c21.J * (2 * c20.J * c23.J + 2 * c21.J * c22.J)),
                6 * c11.I * c12.I * c13.I * c13.J * c22.J * c23.J + c11.I * c12.J * c13.I * c22.I * c13.J * c23.J + c11.I * c12.J * c13.I * c13.J * c22.J * c23.I - c11.J * c12.I * c13.I * c22.I * c13.J * c23.J - c11.J * c12.I * c13.I * c13.J * c22.J * c23.I - 6 * c11.J * c12.J * c13.I * c22.I * c13.J * c23.I - 6 * c10.I * c22.I * c13y3 * c23.I + 6 * c20.I * c22.I * c13y3 * c23.I + 6 * c10.J * c13x3 * c22.J * c23.J + 2 * c12y3 * c13.I * c22.I * c23.I - 2 * c12x3 * c13.J * c22.J * c23.J + 6 * c10.I * c13.I * c22.I * c13y2 * c23.J + 6 * c10.I * c13.I * c13y2 * c22.J * c23.I + 6 * c10.J * c13.I * c22.I * c13y2 * c23.I - 3 * c11.I * c12.I * c22.I * c13y2 * c23.J - 3 * c11.I * c12.I * c13y2 * c22.J * c23.I + 2 * c11.I * c12.J * c22.I * c13y2 * c23.I + 4 * c11.J * c12.I * c22.I * c13y2 * c23.I - 6 * c10.I * c13x2 * c13.J * c22.J * c23.J - 6 * c10.J * c13x2 * c22.I * c13.J * c23.J - 6 * c10.J * c13x2 * c13.J * c22.J * c23.I - 4 * c11.I * c12.J * c13x2 * c22.J * c23.J - 6 * c20.I * c13.I * c22.I * c13y2 * c23.J - 6 * c20.I * c13.I * c13y2 * c22.J * c23.I - 2 * c11.J * c12.I * c13x2 * c22.J * c23.J + 3 * c11.J * c12.J * c13x2 * c22.I * c23.J + 3 * c11.J * c12.J * c13x2 * c22.J * c23.I - 2 * c12.I * c12y2 * c13.I * c22.I * c23.J - 2 * c12.I * c12y2 * c13.I * c22.J * c23.I - 2 * c12.I * c12y2 * c22.I * c13.J * c23.I - 6 * c20.J * c13.I * c22.I * c13y2 * c23.I - 6 * c21.I * c13.I * c21.J * c13y2 * c23.I - 6 * c21.I * c13.I * c22.I * c13y2 * c22.J + 6 * c20.I * c13x2 * c13.J * c22.J * c23.J + 2 * c12x2 * c12.J * c13.I * c22.J * c23.J + 2 * c12x2 * c12.J * c22.I * c13.J * c23.J + 2 * c12x2 * c12.J * c13.J * c22.J * c23.I + 3 * c21.I * c22x2 * c13y3 + 3 * c21x2 * c13y3 * c23.I - 3 * c13.I * c21.J * c22x2 * c13y2 - 3 * c21x2 * c13.I * c13y2 * c23.J + c13x2 * c22.I * c13.J * (6 * c20.J * c23.J + 6 * c21.J * c22.J) + c13x2 * c13.J * c23.I * (6 * c20.J * c22.J + 3 * c21y2) + c21.I * c13x2 * c13.J * (6 * c21.J * c23.J + 3 * c22y2) + c13x3 * (-2 * c20.J * c22.J * c23.J - c23.J * (2 * c20.J * c22.J + c21y2) - c21.J * (2 * c21.J * c23.J + c22y2) - c22.J * (2 * c20.J * c23.J + 2 * c21.J * c22.J)),
                c11.I * c12.J * c13.I * c13.J * c23.I * c23.J - c11.J * c12.I * c13.I * c13.J * c23.I * c23.J + 6 * c21.I * c22.I * c13y3 * c23.I + 3 * c11.I * c12.I * c13.I * c13.J * c23y2 + 6 * c10.I * c13.I * c13y2 * c23.I * c23.J - 3 * c11.I * c12.I * c13y2 * c23.I * c23.J - 3 * c11.J * c12.J * c13.I * c13.J * c23x2 - 6 * c10.J * c13x2 * c13.J * c23.I * c23.J - 6 * c20.I * c13.I * c13y2 * c23.I * c23.J + 3 * c11.J * c12.J * c13x2 * c23.I * c23.J - 2 * c12.I * c12y2 * c13.I * c23.I * c23.J - 6 * c21.I * c13.I * c22.I * c13y2 * c23.J - 6 * c21.I * c13.I * c13y2 * c22.J * c23.I - 6 * c13.I * c21.J * c22.I * c13y2 * c23.I + 6 * c21.I * c13x2 * c13.J * c22.J * c23.J + 2 * c12x2 * c12.J * c13.J * c23.I * c23.J + c22x3 * c13y3 - 3 * c10.I * c13y3 * c23x2 + 3 * c10.J * c13x3 * c23y2 + 3 * c20.I * c13y3 * c23x2 + c12y3 * c13.I * c23x2 - c12x3 * c13.J * c23y2 - 3 * c10.I * c13x2 * c13.J * c23y2 + 3 * c10.J * c13.I * c13y2 * c23x2 - 2 * c11.I * c12.J * c13x2 * c23y2 + c11.I * c12.J * c13y2 * c23x2 - c11.J * c12.I * c13x2 * c23y2 + 2 * c11.J * c12.I * c13y2 * c23x2 + 3 * c20.I * c13x2 * c13.J * c23y2 - c12.I * c12y2 * c13.J * c23x2 - 3 * c20.J * c13.I * c13y2 * c23x2 + c12x2 * c12.J * c13.I * c23y2 - 3 * c13.I * c22x2 * c13y2 * c22.J + c13x2 * c13.J * c23.I * (6 * c20.J * c23.J + 6 * c21.J * c22.J) + c13x2 * c22.I * c13.J * (6 * c21.J * c23.J + 3 * c22y2) + c13x3 * (-2 * c21.J * c22.J * c23.J - c20.J * c23y2 - c22.J * (2 * c21.J * c23.J + c22y2) - c23.J * (2 * c20.J * c23.J + 2 * c21.J * c22.J)),
                -6 * c21.I * c13.I * c13y2 * c23.I * c23.J - 6 * c13.I * c22.I * c13y2 * c22.J * c23.I + 6 * c13x2 * c22.I * c13.J * c22.J * c23.J + 3 * c21.I * c13y3 * c23x2 + 3 * c22x2 * c13y3 * c23.I + 3 * c21.I * c13x2 * c13.J * c23y2 - 3 * c13.I * c21.J * c13y2 * c23x2 - 3 * c13.I * c22x2 * c13y2 * c23.J + c13x2 * c13.J * c23.I * (6 * c21.J * c23.J + 3 * c22y2) + c13x3 * (-c21.J * c23y2 - 2 * c22y2 * c23.J - c23.J * (2 * c21.J * c23.J + c22y2)),
                -6 * c13.I * c22.I * c13y2 * c23.I * c23.J + 6 * c13x2 * c13.J * c22.J * c23.I * c23.J + 3 * c22.I * c13y3 * c23x2 - 3 * c13x3 * c22.J * c23y2 - 3 * c13.I * c13y2 * c22.J * c23x2 + 3 * c13x2 * c22.I * c13.J * c23y2,
                -c13x3 * c23y3 + c13y3 * c23x3 - 3 * c13.I * c13y2 * c23x2 * c23.J + 3 * c13x2 * c13.J * c23.I * c23y2);
            var roots = poly.RootsInInterval(0, 1);
            for (var i = 0; i < roots.Count; i++)
            {
                var s = roots[i];
                var xRoots = new Polynomial(
                    c10.I - c20.I - s * c21.I - s * s * c22.I - s * s * s * c23.I,
                    c11.I,
                    c12.I,
                    c13.I).Roots();
                var yRoots = new Polynomial(
                    c10.J - c20.J - s * c21.J - s * s * c22.J - s * s * s * c23.J,
                    c11.J,
                    c12.J,
                    c13.J).Roots();
                if (xRoots.Count > 0 && yRoots.Count > 0)
                {
                    for (var j = 0; j < xRoots.Count; j++)
                    {
                        var xRoot = xRoots[j];
                        if (0 <= xRoot && xRoot <= 1)
                        {
                            for (var k = 0; k < yRoots.Count; k++)
                            {
                                if (Abs(xRoot - yRoots[k]) < epsilon)
                                {
                                    result.Points.Add((Point2D)c23.Scale(s * s * s).Add(c22.Scale(s * s).Add(c21.Scale(s).Add(c20))));
                                    goto checkRoots;
                                }
                            }
                        }
                    }
                    checkRoots:;
                }
            }
            if (result.Points.Count > 0) result.Status = IntersectionStatus.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1X"></param>
        /// <param name="p1Y"></param>
        /// <param name="p2X"></param>
        /// <param name="p2Y"></param>
        /// <param name="p3X"></param>
        /// <param name="p3Y"></param>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/27664298/calculating-intersection-point-of-quadratic-bezier-curve
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierLineSegmentIntersection(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            double a1X, double a1Y,
            double a2X, double a2Y)
        {
            var intersections = new Intersection(IntersectionStatus.NoIntersection);

            // inverse line normal
            var normal = new Point2D(a1Y - a2Y, a2X - a1X);

            // Q-coefficients
            var c2 = new Point2D(p1X + p2X * -2 + p3X, p1Y + p2Y * -2 + p3Y);
            var c1 = new Point2D(p1X * -2 + p2X * 2, p1Y * -2 + p2Y * 2);
            var c0 = new Point2D(p1X, p1Y);

            // Transform to line 
            var coefficient = a1X * a2Y - a2X * a1Y;
            var a = normal.X * c2.X + normal.Y * c2.Y;
            var b = (normal.X * c1.X + normal.Y * c1.Y) / a;
            var c = (normal.X * c0.X + normal.Y * c0.Y + coefficient) / a;

            // solve the roots
            List<double> roots = new List<double>();
            var d = b * b - 4 * c;
            if (d > 0)
            {
                var e = Sqrt(d);
                roots.Add((-b + Sqrt(d)) / 2);
                roots.Add((-b - Sqrt(d)) / 2);
            }
            else if (d == 0)
            {
                roots.Add(-b / 2);
            }

            // calc the solution points
            for (var i = 0; i < roots.Count; i++)
            {
                var minX = Min(a1X, a2X);
                var minY = Min(a1Y, a2Y);
                var maxX = Max(a1X, a2X);
                var maxY = Max(a1Y, a2Y);
                var t = roots[i];
                if (t >= 0 && t <= 1)
                {
                    // possible point -- pending bounds check
                    var point = new Point2D(
                        Interpolaters.Linear(Interpolaters.Linear(p1X, p2X, t), Interpolaters.Linear(p2X, p3X, t), t),
                        Interpolaters.Linear(Interpolaters.Linear(p1Y, p2Y, t), Interpolaters.Linear(p2Y, p3Y, t), t));
                    var x = point.X;
                    var y = point.Y;
                    var result = new Intersection(IntersectionStatus.Intersection);
                    // bounds checks
                    if (a1X == a2X && y >= minY && y <= maxY)
                    {
                        // vertical line
                        intersections.AppendPoint(point);
                    }
                    else if (a1Y == a2Y && x >= minX && x <= maxX)
                    {
                        // horizontal line
                        intersections.AppendPoint(point);
                    }
                    else if (x >= minX && y >= minY && x <= maxX && y <= maxY)
                    {
                        // line passed bounds check
                        intersections.AppendPoint(point);
                    }
                }
            }
            return intersections;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection QuadraticBezierLineIntersection(
            Point2D p1,
            Point2D p2,
            Point2D p3,
            Point2D a1,
            Point2D a2)
        {
            Vector2D a, b;
            Vector2D c2, c1, c0;
            double cl;
            Vector2D n;
            var min = a1.Min(a2);
            var max = a1.Max(a2);
            var result = new Intersection(IntersectionStatus.NoIntersection);
            a = p2.Scale(-2);
            c2 = p1.Add(a.Add(p3));
            a = p1.Scale(-2);
            b = p2.Scale(2);
            c1 = a.Add(b);
            c0 = new Point2D(p1.X, p1.Y);
            n = new Vector2D(a1.Y - a2.Y, a2.X - a1.X);
            cl = a1.X * a2.Y - a2.X * a1.Y;
            var roots = new Polynomial(
                n.DotProduct(c0) + cl,
                n.DotProduct(c1),
                n.DotProduct(c2)).Roots();
            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];
                if (0 <= t && t <= 1)
                {
                    Point2D p4 = p1.Lerp(p2, t);
                    Point2D p5 = p2.Lerp(p3, t);
                    Point2D p6 = p4.Lerp(p5, t);
                    if (a1.X == a2.X)
                    {
                        if (min.Y <= p6.Y && p6.Y <= max.Y)
                        {
                            result.Status = IntersectionStatus.Intersection;
                            result.AppendPoint(p6);
                        }
                    }
                    else if (a1.Y == a2.Y)
                    {
                        if (min.X <= p6.X && p6.X <= max.X)
                        {
                            result.Status = IntersectionStatus.Intersection;
                            result.AppendPoint(p6);
                        }
                    }
                    else if (p6.GreaterThanOrEqual(min) && p6.LessThanOrEqual(max))
                    {
                        result.Status = IntersectionStatus.Intersection;
                        result.AppendPoint(p6);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection QuadraticBezierRectangleIntersection(
            Point2D p1,
            Point2D p2,
            Point2D p3,
            Point2D r1,
            Point2D r2)
        {
            var min = r1.Min(r2);
            var max = r1.Max(r2);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);
            var inter1 = QuadraticBezierLineIntersection(p1, p2, p3, min, topRight);
            var inter2 = QuadraticBezierLineIntersection(p1, p2, p3, topRight, max);
            var inter3 = QuadraticBezierLineIntersection(p1, p2, p3, max, bottomLeft);
            var inter4 = QuadraticBezierLineIntersection(p1, p2, p3, bottomLeft, min);
            var result = new Intersection(IntersectionStatus.NoIntersection);
            result.AppendPoints(inter1.Points);
            result.AppendPoints(inter2.Points);
            result.AppendPoints(inter3.Points);
            result.AppendPoints(inter4.Points);
            if (result.Points.Count > 0)
                result.Status = IntersectionStatus.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection QuadraticBezierPolygonIntersection(
            Point2D p1,
            Point2D p2,
            Point2D p3,
            List<Point2D> points)
        {
            var result = new Intersection(IntersectionStatus.NoIntersection);
            var length = points.Count;
            for (var i = 0; i < length; i++)
            {
                var a1 = points[i];
                var a2 = points[(i + 1) % length];
                var inter = QuadraticBezierLineIntersection(p1, p2, p3, a1, a2);
                result.AppendPoints(inter.Points);
            }
            if (result.Points.Count > 0)
                result.Status = IntersectionStatus.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="c"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection QuadraticBezierCircleIntersection(
            Point2D p1,
            Point2D p2,
            Point2D p3,
            Point2D c,
            double r)
            => QuadraticBezierUnrotatedEllipseIntersection(p1, p2, p3, c, r, r);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="ec"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection QuadraticBezierUnrotatedEllipseIntersection(
            Point2D p1,
            Point2D p2,
            Point2D p3,
            Point2D ec,
            double rx,
            double ry)
        {
            Vector2D a, b;
            Vector2D c2, c1, c0;
            var result = new Intersection(IntersectionStatus.NoIntersection);
            a = p2.Scale(-2);
            c2 = p1.Add(a.Add(p3));
            a = p1.Scale(-2);
            b = p2.Scale(2);
            c1 = a.Add(b);
            c0 = new Point2D(p1.X, p1.Y);
            var rxrx = rx * rx;
            var ryry = ry * ry;
            var roots = new Polynomial(
                ryry * (c0.I * c0.I + ec.Y * ec.Y) + rxrx * (c0.J * c0.J + ec.Y * ec.Y) - 2 * (ryry * ec.X * c0.I + rxrx * ec.Y * c0.J) - rxrx * ryry,
                2 * (ryry * c1.I * (c0.I - ec.X) + rxrx * c1.J * (c0.J - ec.Y)),
                ryry * (2 * c2.I * c0.I + c1.I * c1.I) + rxrx * (2 * c2.J * c0.J + c1.J * c1.J) - 2 * (ryry * ec.X * c2.I + rxrx * ec.Y * c2.J),
                2 * (ryry * c2.I * c1.I + rxrx * c2.J * c1.J),
                ryry * c2.I * c2.I + rxrx * c2.J * c2.J).Roots();
            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];
                if (0 <= t && t <= 1)
                    result.Points.Add((Point2D)c2.Scale(t * t).Add(c1.Scale(t).Add(c0)));
            }
            if (result.Points.Count > 0)
                result.Status = IntersectionStatus.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <param name="a3"></param>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <param name="b3"></param>
        /// <param name="b4"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection QuadraticBezierCubicBezierIntersection(
            Point2D a1,
            Point2D a2,
            Point2D a3,
            Point2D b1,
            Point2D b2,
            Point2D b3,
            Point2D b4, double epsilon = Epsilon)
        {
            Vector2D a, b, c, d;
            Vector2D c12, c11, c10;
            Vector2D c23, c22, c21, c20;
            var result = new Intersection(IntersectionStatus.NoIntersection);
            a = a2.Scale(-2);
            c12 = a1.Add(a.Add(a3));
            a = a1.Scale(-2);
            b = a2.Scale(2);
            c11 = a.Add(b);
            c10 = new Point2D(a1.X, a1.Y);
            a = b1.Scale(-1);
            b = b2.Scale(3);
            c = b3.Scale(-3);
            d = a.Add(b.Add(c.Add(b4)));
            c23 = new Vector2D(d.I, d.J);
            a = b1.Scale(3);
            b = b2.Scale(-6);
            c = b3.Scale(3);
            d = a.Add(b.Add(c));
            c22 = new Vector2D(d.I, d.J);
            a = b1.Scale(-3);
            b = b2.Scale(3);
            c = a.Add(b);
            c21 = new Vector2D(c.I, c.J);
            c20 = new Vector2D(b1.X, b1.Y);
            var c10x2 = c10.I * c10.I;
            var c10y2 = c10.J * c10.J;
            var c11x2 = c11.I * c11.I;
            var c11y2 = c11.J * c11.J;
            var c12x2 = c12.I * c12.I;
            var c12y2 = c12.J * c12.J;
            var c20x2 = c20.I * c20.I;
            var c20y2 = c20.J * c20.J;
            var c21x2 = c21.I * c21.I;
            var c21y2 = c21.J * c21.J;
            var c22x2 = c22.I * c22.I;
            var c22y2 = c22.J * c22.J;
            var c23x2 = c23.I * c23.I;
            var c23y2 = c23.J * c23.J;
            var poly = new Polynomial(
                -2 * c10.I * c10.J * c12.I * c12.J - c10.I * c11.I * c11.J * c12.J - c10.J * c11.I * c11.J * c12.I + 2 * c10.I * c12.I * c20.J * c12.J + 2 * c10.J * c20.I * c12.I * c12.J + c11.I * c20.I * c11.J * c12.J + c11.I * c11.J * c12.I * c20.J - 2 * c20.I * c12.I * c20.J * c12.J - 2 * c10.I * c20.I * c12y2 + c10.I * c11y2 * c12.I + c10.J * c11x2 * c12.J - 2 * c10.J * c12x2 * c20.J - c20.I * c11y2 * c12.I - c11x2 * c20.J * c12.J + c10x2 * c12y2 + c10y2 * c12x2 + c20x2 * c12y2 + c12x2 * c20y2,
                2 * c10.I * c12.I * c12.J * c21.J + 2 * c10.J * c12.I * c21.I * c12.J + c11.I * c11.J * c12.I * c21.J + c11.I * c11.J * c21.I * c12.J - 2 * c20.I * c12.I * c12.J * c21.J - 2 * c12.I * c20.J * c21.I * c12.J - 2 * c10.I * c21.I * c12y2 - 2 * c10.J * c12x2 * c21.J + 2 * c20.I * c21.I * c12y2 - c11y2 * c12.I * c21.I - c11x2 * c12.J * c21.J + 2 * c12x2 * c20.J * c21.J,
                2 * c10.I * c12.I * c12.J * c22.J + 2 * c10.J * c12.I * c12.J * c22.I + c11.I * c11.J * c12.I * c22.J + c11.I * c11.J * c12.J * c22.I - 2 * c20.I * c12.I * c12.J * c22.J - 2 * c12.I * c20.J * c12.J * c22.I - 2 * c12.I * c21.I * c12.J * c21.J - 2 * c10.I * c12y2 * c22.I - 2 * c10.J * c12x2 * c22.J + 2 * c20.I * c12y2 * c22.I - c11y2 * c12.I * c22.I - c11x2 * c12.J * c22.J + c21x2 * c12y2 + c12x2 * (2 * c20.J * c22.J + c21y2),
                2 * c10.I * c12.I * c12.J * c23.J + 2 * c10.J * c12.I * c12.J * c23.I + c11.I * c11.J * c12.I * c23.J + c11.I * c11.J * c12.J * c23.I - 2 * c20.I * c12.I * c12.J * c23.J - 2 * c12.I * c20.J * c12.J * c23.I - 2 * c12.I * c21.I * c12.J * c22.J - 2 * c12.I * c12.J * c21.J * c22.I - 2 * c10.I * c12y2 * c23.I - 2 * c10.J * c12x2 * c23.J + 2 * c20.I * c12y2 * c23.I + 2 * c21.I * c12y2 * c22.I - c11y2 * c12.I * c23.I - c11x2 * c12.J * c23.J + c12x2 * (2 * c20.J * c23.J + 2 * c21.J * c22.J),
                -2 * c12.I * c21.I * c12.J * c23.J - 2 * c12.I * c12.J * c21.J * c23.I - 2 * c12.I * c12.J * c22.I * c22.J + 2 * c21.I * c12y2 * c23.I + c12y2 * c22x2 + c12x2 * (2 * c21.J * c23.J + c22y2),
                -2 * c12.I * c12.J * c23.I * c23.J + c12x2 * c23y2 + c12y2 * c23x2,
                -2 * c12.I * c12.J * c22.I * c23.J - 2 * c12.I * c12.J * c22.J * c23.I + 2 * c12y2 * c22.I * c23.I + 2 * c12x2 * c22.J * c23.J);
            var roots = poly.RootsInInterval(0, 1);
            for (var i = 0; i < roots.Count; i++)
            {
                var s = roots[i];
                var xRoots = new Polynomial(
                    c10.I - c20.I - s * c21.I - s * s * c22.I - s * s * s * c23.I,
                    c11.I,
                    c12.I).Roots();
                var yRoots = new Polynomial(
                    c10.J - c20.J - s * c21.J - s * s * c22.J - s * s * s * c23.J,
                    c11.J,
                    c12.J).Roots();
                if (xRoots.Count > 0 && yRoots.Count > 0)
                {
                    for (var j = 0; j < xRoots.Count; j++)
                    {
                        var xRoot = xRoots[j];
                        if (0 <= xRoot && xRoot <= 1)
                        {
                            for (var k = 0; k < yRoots.Count; k++)
                            {
                                if (Abs(xRoot - yRoots[k]) < epsilon)
                                {
                                    result.Points.Add((Point2D)c23.Scale(s * s * s).Add(c22.Scale(s * s).Add(c21.Scale(s).Add(c20))));
                                    goto checkRoots;
                                }
                            }
                        }
                    }
                    checkRoots:;
                }
            }
            if (result.Points.Count > 0)
                result.Status = IntersectionStatus.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <param name="a3"></param>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <param name="b3"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection QuadraticBezierQuadraticBezierIntersection(
            Point2D a1,
            Point2D a2,
            Point2D a3,
            Point2D b1,
            Point2D b2,
            Point2D b3, double epsilon = Epsilon)
        {
            Vector2D va, vb;
            Vector2D c12, c11, c10;
            Vector2D c22, c21, c20;
            var result = new Intersection(IntersectionStatus.NoIntersection);
            va = a2.Scale(-2);
            c12 = a1.Add(va.Add(a3));
            va = a1.Scale(-2);
            vb = a2.Scale(2);
            c11 = va.Add(vb);
            c10 = new Point2D(a1.X, a1.Y);
            va = b2.Scale(-2);
            c22 = b1.Add(va.Add(b3));
            va = b1.Scale(-2);
            vb = b2.Scale(2);
            c21 = va.Add(vb);
            c20 = new Point2D(b1.X, b1.Y);
            var a = c12.I * c11.J - c11.I * c12.J;
            var b = c22.I * c11.J - c11.I * c22.J;
            var c = c21.I * c11.J - c11.I * c21.J;
            var d = c11.I * (c10.J - c20.J) + c11.J * (-c10.I + c20.I);
            var e = c22.I * c12.J - c12.I * c22.J;
            var f = c21.I * c12.J - c12.I * c21.J;
            var g = c12.I * (c10.J - c20.J) + c12.J * (-c10.I + c20.I);
            var poly = new Polynomial(
                a * d - g * g,
                a * c - 2 * f * g,
                a * b - f * f - 2 * e * g,
                -2 * e * f,
                -e * e);
            var roots = poly.Roots();
            for (var i = 0; i < roots.Count; i++)
            {
                var s = roots[i];
                if (0 <= s && s <= 1)
                {
                    var xRoots = new Polynomial(
                        -c10.I + c20.I + s * c21.I + s * s * c22.I,
                        -c11.I,
                        -c12.I).Roots();
                    var yRoots = new Polynomial(
                        -c10.J + c20.J + s * c21.J + s * s * c22.J,
                        -c11.J,
                        -c12.J).Roots();
                    if (xRoots.Count > 0 && yRoots.Count > 0)
                    {
                        for (var j = 0; j < xRoots.Count; j++)
                        {
                            var xRoot = xRoots[j];
                            if (0 <= xRoot && xRoot <= 1)
                            {
                                for (var k = 0; k < yRoots.Count; k++)
                                {
                                    if (Abs(xRoot - yRoots[k]) < epsilon)
                                    {
                                        result.Points.Add((Point2D)c22.Scale(s * s).Add(c21.Scale(s).Add(c20)));
                                        goto checkRoots;
                                    }
                                }
                            }
                        }
                        checkRoots:;
                    }
                }
            }

            if (result.Points.Count > 0)
                result.Status = IntersectionStatus.Intersection;
            return result;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Bezout(double[] e1, double[] e2)
        {
            var AB = e1[0] * e2[1] - e2[0] * e1[1];
            var AC = e1[0] * e2[2] - e2[0] * e1[2];
            var AD = e1[0] * e2[3] - e2[0] * e1[3];
            var AE = e1[0] * e2[4] - e2[0] * e1[4];
            var AF = e1[0] * e2[5] - e2[0] * e1[5];
            var BC = e1[1] * e2[2] - e2[1] * e1[2];
            var BE = e1[1] * e2[4] - e2[1] * e1[4];
            var BF = e1[1] * e2[5] - e2[1] * e1[5];
            var CD = e1[2] * e2[3] - e2[2] * e1[3];
            var DE = e1[3] * e2[4] - e2[3] * e1[4];
            var DF = e1[3] * e2[5] - e2[3] * e1[5];
            var BFpDE = BF + DE;
            var BEmCD = BE - CD;
            return new Polynomial(
                AD * DF - AF * AF,
                AB * DF + AD * BFpDE - 2 * AE * AF,
                AB * BFpDE + AD * BEmCD - AE * AE - 2 * AC * AF,
                AB * BEmCD + AD * BC - 2 * AC * AE,
                AB * BC - AC * AC);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        /// <remarks>
        /// based on http://abecedarical.com/javascript/script_exact_cubic.html
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double[] CubicRoots(double a, double b, double c, double d)
        {
            // The horizontal line issue seems to be somewhere in here.
            var A = b / a;
            var B = c / a;
            var C = d / a;

            double S, T, Im;

            double Q = (3 * B - Pow(A, 2)) / 9;
            double R = (9 * A * B - 27 * C - 2 * Pow(A, 3)) / 54;
            double D = Pow(Q, 3) + Pow(R, 2);    // polynomial discriminant

            var t = new double[3];

            if (D >= 0)                                 // complex or duplicate roots
            {
                S = Sign(R + Sqrt(D)) * Pow(Abs(R + Sqrt(D)), (1 / 3));
                T = Sign(R - Sqrt(D)) * Pow(Abs(R - Sqrt(D)), (1 / 3));

                t[0] = -A / 3 + (S + T);                    // real root
                t[1] = -A / 3 - (S + T) / 2;                  // real part of complex root
                t[2] = -A / 3 - (S + T) / 2;                  // real part of complex root
                Im = Abs(Sqrt(3) * (S - T) / 2);    // complex part of root pair   

                /*discard complex roots*/
                if (Im != 0)
                {
                    t[1] = -1;
                    t[2] = -1;
                }

            }
            else                                          // distinct real roots
            {
                var th = Acos(R / Sqrt(-Pow(Q, 3)));

                t[0] = 2 * Sqrt(-Q) * Cos(th / 3) - A / 3;
                t[1] = 2 * Sqrt(-Q) * Cos((th + Tau) / 3) - A / 3;
                t[2] = 2 * Sqrt(-Q) * Cos((th + 4 * PI) / 3) - A / 3;
                Im = 0.0;
            }

            /*discard out of spec roots*/
            for (var i = 0; i < 3; i++)
                if (t[i] < 0 || t[i] > 1.0) t[i] = -1;

            /*sort but place -1 at the end*/
            t = SortSpecial(t);

            //Console.log(t[0] + " " + t[1] + " " + t[2]);
            return t;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double[] SortSpecial(double[] a)
        {
            bool flip;
            double temp;

            do
            {
                flip = false;
                for (var i = 0; i < a.Length - 1; i++)
                {
                    if ((a[i + 1] >= 0 && a[i] > a[i + 1]) ||
                        (a[i] < 0 && a[i + 1] >= 0))
                    {
                        flip = true;
                        temp = a[i];
                        a[i] = a[i + 1];
                        a[i + 1] = temp;

                    }
                }
            } while (flip);
            return a;
        }

        #endregion
    }
}

