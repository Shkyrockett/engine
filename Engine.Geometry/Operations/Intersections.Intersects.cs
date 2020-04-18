// <copyright file="Intersections.Intersects.cs" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>

// <copyright company="kevlindev" >
//     Many of the Intersections methods were adapted from Kevin Lindsey's site http://www.kevlindev.com/gui/math/intersection/.
//     Copyright © 2000 - 2003 Kevin Lindsey. All rights reserved.
// </copyright>
// <author id="thelonious">Kevin Lindsey</author>
// <license>
//     Licensed under the BSD-3-Clause https://github.com/thelonious/kld-intersections/blob/development/LICENSE
// </license>

// <copyright company="angusj" >
//     The Point in Polygon method is from the Clipper Library.
//     Copyright © 2010 - 2014 Angus Johnson. All rights reserved.
// </copyright>
// <author id="angusj">Angus Johnson</author>
// <license id="Boost">
//     Licensed under the Boost Software License (http://www.boost.org/LICENSE_1_0.txt).
// </license>

// <copyright company="vb-helper" >
//     Some of the methods came from Rod Stephens excellent blogs vb-helper(http://vb-helper.com), and csharphelper (http://csharphelper.com), as well as from his books.
//     Copyright © Rod Stephens.
// </copyright>
// <author id="RodStephens">Rod Stephens</author>
// <license id="No Restrictions">
//     You can use the code you find on this site or in my books. I request but don’t require an acknowledgment.
//     I also recommend (but again don’t require) that you put the URL where you found the code in a comment inside your code in case you need to look it up later.
//     So really no restrictions. (http://csharphelper.com/blog/rod/)
// </license>

// <summary></summary>
// <remarks></remarks>

using System;
using System.Runtime.CompilerServices;
using static Engine.Operations;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The intersections class.
    /// </summary>
    public static partial class Intersections
    {
        #region Intersects Extension Method Overloads
        /// <summary>
        /// Check whether a Rectangle and a shape intersects.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="shape">The shape.</param>
        /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        public static bool Intersects(Rectangle2D rect, Shape2D shape, double epsilon = double.Epsilon) =>
            // Shapes arranged by degree and complexity.
            shape switch
            {
                ScreenPoint2D p => PointRectangleIntersects(p.X, p.Y, rect.X, rect.Y, rect.Right, rect.Bottom, epsilon),
                BezierSegmentX2D _ => throw new NotImplementedException(),
                LineSegment2D l => LineSegmentRectangleIntersects(l.AX, l.AY, l.BX, l.BY, rect.X, rect.Y, rect.Right, rect.Bottom, epsilon),
                Ray2D r => RayRectangleIntersects(r.Location.X, r.Location.Y, r.Location.X + r.Direction.I, r.Location.Y + r.Direction.J, rect.X, rect.Y, rect.Right, rect.Bottom, epsilon),
                Line2D l => LineRectangleIntersects(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, rect.X, rect.Y, rect.Right, rect.Bottom, epsilon),
                QuadraticBezier2D b => QuadraticBezierSegmentRectangleIntersects(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, rect.X, rect.Y, rect.Right, rect.Bottom, epsilon),
                CubicBezier2D b => CubicBezierSegmentRectangleIntersects(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, rect.X, rect.Y, rect.Right, rect.Bottom, epsilon),
                Rectangle2D r => RectangleRectangleIntersects(r.X, r.Y, r.Right, r.Bottom, rect.X, rect.Y, rect.Right, rect.Bottom, epsilon),
                Polyline2D _ => throw new NotImplementedException(),
                PolygonContour2D _ => throw new NotImplementedException(),
                Polygon2D _ => throw new NotImplementedException(),
                PolyBezierContour2D _ => throw new NotImplementedException(),
                PolyBezier2D _ => throw new NotImplementedException(),
                PolycurveContour2D _ => throw new NotImplementedException(),
                Polycurve2D _ => throw new NotImplementedException(),
                _ => false,
            };

        /// <summary>
        /// Check whether two points intersect.
        /// </summary>
        /// <param name="point0">The point0.</param>
        /// <param name="point1">The point1.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this Point2D point0, Point2D point1, double epsilon = double.Epsilon)
        {
            _ = epsilon;
            return point0 == point1;
        }

        /// <summary>
        /// Check whether a point is coincident to a line segment.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this Point2D p, Point2D a, Point2D b, double epsilon = double.Epsilon) => PointLineSegmentIntersects(p.X, p.Y, a.X, a.Y, b.X, b.Y, epsilon);

        /// <summary>
        /// Check whether a point is coincident to a line segment.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="s">The s.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this Point2D p, LineSegment2D s, double epsilon = double.Epsilon) => PointLineSegmentIntersects(p.X, p.Y, s.A.X, s.A.Y, s.B.X, s.B.Y, epsilon);

        /// <summary>
        /// Check whether a point is coincident to a line segment.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="p">The p.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this LineSegment2D s, Point2D p, double epsilon = double.Epsilon) => PointLineSegmentIntersects(p.X, p.Y, s.A.X, s.A.Y, s.B.X, s.B.Y, epsilon);

        /// <summary>
        /// Check whether two line segments intersect.
        /// </summary>
        /// <param name="s0">The s0.</param>
        /// <param name="s1">The s1.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this LineSegment2D s0, LineSegment2D s1, double epsilon = double.Epsilon) => LineSegmentLineSegmentIntersects(s0.A.X, s0.A.Y, s0.B.X, s0.B.Y, s1.A.X, s1.A.Y, s1.B.X, s1.B.Y, epsilon);

        /// <summary>
        /// Determines if this rectangle interests with another rectangle.
        /// </summary>
        /// <param name="rect1">The rect1.</param>
        /// <param name="rect2">The rect2.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this Rectangle2D rect1, Rectangle2D rect2, double epsilon = double.Epsilon) => RectangleRectangleIntersects(rect1.X, rect1.Y, rect1.Width, rect1.Height, rect2.X, rect2.Y, rect2.Width, rect2.Height, epsilon);

        /// <summary>
        /// Determines if this Circle interests with another Circle.
        /// </summary>
        /// <param name="c0">The c0.</param>
        /// <param name="c1">The c1.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this Circle2D c0, Circle2D c1, double epsilon = double.Epsilon) => CircleCircleIntersects(c0.X, c0.Y, c0.Radius, c1.X, c1.Y, c1.Radius, epsilon);
        #endregion Intersects Extension Method Overloads

        #region Intersects Methods
        /// <summary>
        /// Checks whether two points are at the same location.
        /// </summary>
        /// <param name="point0X">The point0 x.</param>
        /// <param name="point0Y">The point0 y.</param>
        /// <param name="point1X">The point1 x.</param>
        /// <param name="point1Y">The point1 y.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointPointIntersects(double point0X, double point0Y, double point1X, double point1Y) => point0X == point1X && point0Y == point1Y;

        /// <summary>
        /// Check whether a point is coincident to a line segment.
        /// </summary>
        /// <param name="pointX">The point x.</param>
        /// <param name="pointY">The point y.</param>
        /// <param name="segmentAX">The segment ax.</param>
        /// <param name="segmentAY">The segment ay.</param>
        /// <param name="segmentBX">The segment bx.</param>
        /// <param name="segmentBY">The segment by.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        /// <acknowledgment>
        /// http://www.angusj.com/delphi/clipper.php
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointLineSegmentIntersects(double pointX, double pointY, double segmentAX, double segmentAY, double segmentBX, double segmentBY, double epsilon = double.Epsilon)
        {
            _ = epsilon;
            return ((pointX == segmentAX) && (pointY == segmentAY))
                    || ((pointX == segmentBX) && (pointY == segmentBY))
                    || (((pointX > segmentAX) == (pointX < segmentBX))
                    && ((pointY > segmentAY) == (pointY < segmentBY))
                    && ((pointX - segmentAX) * (segmentBY - segmentAY) == (pointY - segmentAY) * (segmentBX - segmentAX)));
        }

        /// <summary>
        /// Check whether a point is coincident to a line segment.
        /// </summary>
        /// <param name="pX">The p x.</param>
        /// <param name="pY">The p y.</param>
        /// <param name="lX">The l x.</param>
        /// <param name="lY">The l y.</param>
        /// <param name="lI">The l i.</param>
        /// <param name="lJ">The l j.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        /// <acknowledgment>
        /// http://www.angusj.com/delphi/clipper.php
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointRayIntersects(
            double pX, double pY,
            double lX, double lY,
            double lI, double lJ,
            double epsilon = double.Epsilon)
        {
            _ = epsilon;
            return ((pX == lX) && (pY == lY)) || ((pX == lI) && (pY == lJ))
            || (
                ((pX - lX > 0) == (pX - lI < 0)) && ((pY - lY > 0) == (pY - lJ < 0))
                && ((pX - lX) * (lJ - lY) == (pY - lY) * (lI - lX))
            );
        }

        /// <summary>
        /// Check whether a point is coincident to a line segment.
        /// </summary>
        /// <param name="pX">The p x.</param>
        /// <param name="pY">The p y.</param>
        /// <param name="lX">The l x.</param>
        /// <param name="lY">The l y.</param>
        /// <param name="lI">The l i.</param>
        /// <param name="lJ">The l j.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        /// <acknowledgment>
        /// http://www.angusj.com/delphi/clipper.php
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointLineIntersects(
            double pX, double pY,
            double lX, double lY,
            double lI, double lJ,
            double epsilon = double.Epsilon)
        {
            _ = epsilon;
            return ((pX == lX) && (pY == lY))
                   || (pX == lI)
                   && (pY == lJ)
                   && ((pX - lX) * (lJ - lY) == (pY - lY) * (lI - lX));
        }

        /// <summary>
        /// Check whether a point is within a rectangle.
        /// </summary>
        /// <param name="pX">The p x.</param>
        /// <param name="pY">The p y.</param>
        /// <param name="r1X">The r1 x.</param>
        /// <param name="r1Y">The r1 y.</param>
        /// <param name="r2X">The r2 x.</param>
        /// <param name="r2Y">The r2 y.</param>
        /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointRectangleIntersects(
            double pX, double pY,
            double r1X, double r1Y,
            double r2X, double r2Y,
            double epsilon = double.Epsilon)
        {
            var (minX, minY) = MinPoint(r1X, r1Y, r2X, r2Y);
            var (maxX, maxY) = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(maxX, minY);
            var bottomLeft = new Point2D(minX, maxY);
            return PointLineSegmentIntersects(pX, pY, minX, minY, topRight.X, topRight.Y, epsilon)
                || PointLineSegmentIntersects(pX, pY, topRight.X, topRight.Y, maxX, maxY, epsilon)
                || PointLineSegmentIntersects(pX, pY, maxX, maxY, bottomLeft.X, bottomLeft.Y, epsilon)
                || PointLineSegmentIntersects(pX, pY, bottomLeft.X, bottomLeft.Y, minX, minY, epsilon);
        }

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
        /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        /// <acknowledgment>
        /// http://www.vb-helper.com/howto_segments_intersect.html
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineSegmentLineSegmentIntersects(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double epsilon = double.Epsilon)
        {
            // Translate lines to origin.
            var u1 = x1 - x0;
            var v1 = y1 - y0;
            var u2 = x3 - x2;
            var v2 = y3 - y2;

            // Calculate the determinant of the coefficient matrix.
            var determinant = (v2 * u1) - (u2 * v1);

            // Check if the line segments are parallel.
            if (Abs(determinant) < epsilon)
            {
                // Return whether line segments are coincidental.
                return PointLineSegmentIntersects(x2, y2, x0, y0, x1, y1) || PointLineSegmentIntersects(x3, y3, x0, y0, x1, y1);
            }

            // Find the index where the intersection point lies on the line.
            var s = (((x0 - x2) * v1) + ((y2 - y0) * u1)) / -determinant;
            var t = (((x2 - x0) * v2) + ((y0 - y2) * u2)) / determinant;

            // Check whether the point is on the segment.
            return (t >= 0d) && (t <= 1d) && (s >= 0d) && (s <= 1d);
        }

        /// <summary>
        /// Find out whether a line segment and a rectangle intersects.
        /// </summary>
        /// <param name="a1X">The a1 x.</param>
        /// <param name="a1Y">The a1 y.</param>
        /// <param name="a2X">The a2 x.</param>
        /// <param name="a2Y">The a2 y.</param>
        /// <param name="r1X">The r1 x.</param>
        /// <param name="r1Y">The r1 y.</param>
        /// <param name="r2X">The r2 x.</param>
        /// <param name="r2Y">The r2 y.</param>
        /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineSegmentRectangleIntersects(
            double a1X, double a1Y,
            double a2X, double a2Y,
            double r1X, double r1Y,
            double r2X, double r2Y,
            double epsilon = double.Epsilon)
        {
            var (minX, minY) = MinPoint(r1X, r1Y, r2X, r2Y);
            var (maxX, maxY) = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(maxX, minY);
            var bottomLeft = new Point2D(minX, maxY);
            return LineSegmentLineSegmentIntersects(a1X, a1Y, a2X, a2Y, minX, minY, topRight.X, topRight.Y, epsilon)
                || LineSegmentLineSegmentIntersects(a1X, a1Y, a2X, a2Y, topRight.X, topRight.Y, maxX, maxY, epsilon)
                || LineSegmentLineSegmentIntersects(a1X, a1Y, a2X, a2Y, maxX, maxY, bottomLeft.X, bottomLeft.Y, epsilon)
                || LineSegmentLineSegmentIntersects(a1X, a1Y, a2X, a2Y, bottomLeft.X, bottomLeft.Y, minX, minY, epsilon);
        }

        /// <summary>
        /// Find out whether a ray and a line segment intersect.
        /// </summary>
        /// <param name="x0">The x component of the first point of the first line.</param>
        /// <param name="y0">The y component of the first point of the first line.</param>
        /// <param name="x1">The x component of the second point of the first line.</param>
        /// <param name="y1">The y component of the second point of the first line.</param>
        /// <param name="x2">The x component of the first point of the second line.</param>
        /// <param name="y2">The y component of the first point of the second line.</param>
        /// <param name="x3">The x component of the second point of the second line.</param>
        /// <param name="y3">The y component of the second point of the second line.</param>
        /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        /// <acknowledgment>
        /// http://www.vb-helper.com/howto_segments_intersect.html
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RayLineSegmentIntersects(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double epsilon = double.Epsilon)
        {
            // ToDo: Figure out ray intersection.

            // Translate lines to origin.
            var u1 = x1 - x0;
            var v1 = y1 - y0;
            var u2 = x3 - x2;
            var v2 = y3 - y2;

            // Calculate the determinant of the coefficient matrix.
            var determinant = (v2 * u1) - (u2 * v1);

            // Check if the line segments are parallel.
            if (Abs(determinant) < epsilon)
            {
                // Return whether line segments are coincidental.
                return PointLineSegmentIntersects(x2, y2, x0, y0, x1, y1) || PointLineSegmentIntersects(x3, y3, x0, y0, x1, y1);
            }

            // Find the index where the intersection point lies on the line.
            var t = (((x0 - x2) * v1) + ((y2 - y0) * u1)) / -determinant;

            // Check whether the point is on the segment.
            return (t >= 0d) && (t <= 1d);
        }

        /// <summary>
        /// Find out whether a ray and a line segment intersect.
        /// </summary>
        /// <param name="x0">The x component of the first point of the first line.</param>
        /// <param name="y0">The y component of the first point of the first line.</param>
        /// <param name="x1">The x component of the second point of the first line.</param>
        /// <param name="y1">The y component of the second point of the first line.</param>
        /// <param name="x2">The x component of the first point of the second line.</param>
        /// <param name="y2">The y component of the first point of the second line.</param>
        /// <param name="x3">The x component of the second point of the second line.</param>
        /// <param name="y3">The y component of the second point of the second line.</param>
        /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        /// <acknowledgment>
        /// http://www.vb-helper.com/howto_segments_intersect.html
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RayRayIntersects(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double epsilon = double.Epsilon)
        {
            // ToDo: Figure out ray intersection.

            // Translate lines to origin.
            var u1 = x1 - x0;
            var v1 = y1 - y0;
            var u2 = x3 - x2;
            var v2 = y3 - y2;

            // Calculate the determinant of the coefficient matrix.
            var determinant = (v2 * u1) - (u2 * v1);

            // Check if the line segments are parallel.
            if (Abs(determinant) < epsilon)
            {
                // Return whether line segments are coincidental.
                return PointLineSegmentIntersects(x2, y2, x0, y0, x1, y1) || PointLineSegmentIntersects(x3, y3, x0, y0, x1, y1);
            }

            // Find the index where the intersection point lies on the line.
            var t = (((x0 - x2) * v1) + ((y2 - y0) * u1)) / -determinant;

            // Check whether the point is on the other ray.
            return t >= 0d;// && (t <= 1d);
        }

        /// <summary>
        /// Find out whether a ray and a rectangle intersects.
        /// </summary>
        /// <param name="a1X">The a1 x.</param>
        /// <param name="a1Y">The a1 y.</param>
        /// <param name="a2X">The a2 x.</param>
        /// <param name="a2Y">The a2 y.</param>
        /// <param name="r1X">The r1 x.</param>
        /// <param name="r1Y">The r1 y.</param>
        /// <param name="r2X">The r2 x.</param>
        /// <param name="r2Y">The r2 y.</param>
        /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RayRectangleIntersects(
            double a1X, double a1Y,
            double a2X, double a2Y,
            double r1X, double r1Y,
            double r2X, double r2Y,
            double epsilon = double.Epsilon)
        {
            var (minX, minY) = MinPoint(r1X, r1Y, r2X, r2Y);
            var (maxX, maxY) = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(maxX, minY);
            var bottomLeft = new Point2D(minX, maxY);
            return RayLineSegmentIntersects(a1X, a1Y, a2X, a2Y, minX, minY, topRight.X, topRight.Y, epsilon)
                || RayLineSegmentIntersects(a1X, a1Y, a2X, a2Y, topRight.X, topRight.Y, maxX, maxY, epsilon)
                || RayLineSegmentIntersects(a1X, a1Y, a2X, a2Y, maxX, maxY, bottomLeft.X, bottomLeft.Y, epsilon)
                || RayLineSegmentIntersects(a1X, a1Y, a2X, a2Y, bottomLeft.X, bottomLeft.Y, minX, minY, epsilon);
        }

        /// <summary>
        /// Find out whether a line and a line segment intersect.
        /// </summary>
        /// <param name="x0">The x component of the first point of the first line.</param>
        /// <param name="y0">The y component of the first point of the first line.</param>
        /// <param name="x1">The x component of the second point of the first line.</param>
        /// <param name="y1">The y component of the second point of the first line.</param>
        /// <param name="x2">The x component of the first point of the second line.</param>
        /// <param name="y2">The y component of the first point of the second line.</param>
        /// <param name="x3">The x component of the second point of the second line.</param>
        /// <param name="y3">The y component of the second point of the second line.</param>
        /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        /// <acknowledgment>
        /// http://www.vb-helper.com/howto_segments_intersect.html
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineLineSegmentIntersects(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double epsilon = double.Epsilon)
        {
            // Translate lines to origin.
            var u1 = x1 - x0;
            var v1 = y1 - y0;
            var u2 = x3 - x2;
            var v2 = y3 - y2;

            // Calculate the determinant of the coefficient matrix.
            var determinant = (v2 * u1) - (u2 * v1);

            // Check if the line segments are parallel.
            if (Abs(determinant) < epsilon)
            {
                // Return whether line segments are coincidental.
                return PointLineSegmentIntersects(x2, y2, x0, y0, x1, y1) || PointLineSegmentIntersects(x3, y3, x0, y0, x1, y1);
            }

            // Find the index where the intersection point lies on the line.
            var t = (((x0 - x2) * v1) + ((y2 - y0) * u1)) / -determinant;

            // Check whether the point is on the segment.
            return (t >= 0d) && (t <= 1d);
        }

        /// <summary>
        /// Find out whether a line and a line segment intersect.
        /// </summary>
        /// <param name="x0">The x component of the first point of the first line.</param>
        /// <param name="y0">The y component of the first point of the first line.</param>
        /// <param name="x1">The x component of the second point of the first line.</param>
        /// <param name="y1">The y component of the second point of the first line.</param>
        /// <param name="x2">The x component of the first point of the second line.</param>
        /// <param name="y2">The y component of the first point of the second line.</param>
        /// <param name="x3">The x component of the second point of the second line.</param>
        /// <param name="y3">The y component of the second point of the second line.</param>
        /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        /// <acknowledgment>
        /// http://www.vb-helper.com/howto_segments_intersect.html
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineRayIntersects(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double epsilon = double.Epsilon)
        {
            // ToDo: Figure out Line Ray intersection.

            // Translate lines to origin.
            var u1 = x1 - x0;
            var v1 = y1 - y0;
            var u2 = x3 - x2;
            var v2 = y3 - y2;

            // Calculate the determinant of the coefficient matrix.
            var determinant = (v2 * u1) - (u2 * v1);

            // Check if the line segments are parallel.
            if (Abs(determinant) < epsilon)
            {
                // Return whether line segments are coincidental.
                return PointLineSegmentIntersects(x2, y2, x0, y0, x1, y1) || PointLineSegmentIntersects(x3, y3, x0, y0, x1, y1);
            }

            // Find the index where the intersection point lies on the line.
            var t = (((x0 - x2) * v1) + ((y2 - y0) * u1)) / -determinant;

            // Check whether the point is on the segment.
            return (t >= 0d) && (t <= 1d);
        }

        /// <summary>
        /// Find out whether two lines intersect.
        /// </summary>
        /// <param name="x0">The x component of the first point of the first line.</param>
        /// <param name="y0">The y component of the first point of the first line.</param>
        /// <param name="x1">The x component of the second point of the first line.</param>
        /// <param name="y1">The y component of the second point of the first line.</param>
        /// <param name="x2">The x component of the first point of the second line.</param>
        /// <param name="y2">The y component of the first point of the second line.</param>
        /// <param name="x3">The x component of the second point of the second line.</param>
        /// <param name="y3">The y component of the second point of the second line.</param>
        /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        /// <acknowledgment>
        /// http://www.vb-helper.com/howto_segments_intersect.html
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineLineIntersects(double x0, double y0, double x1, double y1, double x2, double y2, double x3, double y3, double epsilon = double.Epsilon) => Abs(((y3 - y2) * (x1 - x0)) - ((x3 - x2) * (y1 - y0))) < epsilon;

        /// <summary>
        /// Find out whether a line and a rectangle intersects.
        /// </summary>
        /// <param name="a1X">The a1 x.</param>
        /// <param name="a1Y">The a1 y.</param>
        /// <param name="a2X">The a2 x.</param>
        /// <param name="a2Y">The a2 y.</param>
        /// <param name="r1X">The r1 x.</param>
        /// <param name="r1Y">The r1 y.</param>
        /// <param name="r2X">The r2 x.</param>
        /// <param name="r2Y">The r2 y.</param>
        /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineRectangleIntersects(
            double a1X, double a1Y,
            double a2X, double a2Y,
            double r1X, double r1Y,
            double r2X, double r2Y,
            double epsilon = double.Epsilon)
        {
            var (minX, minY) = MinPoint(r1X, r1Y, r2X, r2Y);
            var (maxX, maxY) = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(maxX, minY);
            var bottomLeft = new Point2D(minX, maxY);
            return LineLineSegmentIntersects(a1X, a1Y, a2X, a2Y, minX, minY, topRight.X, topRight.Y, epsilon)
                || LineLineSegmentIntersects(a1X, a1Y, a2X, a2Y, topRight.X, topRight.Y, maxX, maxY, epsilon)
                || LineLineSegmentIntersects(a1X, a1Y, a2X, a2Y, maxX, maxY, bottomLeft.X, bottomLeft.Y, epsilon)
                || LineLineSegmentIntersects(a1X, a1Y, a2X, a2Y, bottomLeft.X, bottomLeft.Y, minX, minY, epsilon);
        }

        /// <summary>
        /// Find out whether a quadratic Bézier segment and a line segment intersects.
        /// </summary>
        /// <param name="aX">a x.</param>
        /// <param name="aY">a y.</param>
        /// <param name="bX">The b x.</param>
        /// <param name="bY">The b y.</param>
        /// <param name="cX">The c x.</param>
        /// <param name="cY">The c y.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="right">The right.</param>
        /// <param name="bottom">The bottom.</param>
        /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool QuadraticBezierSegmentLineSegmentIntersects(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double x, double y,
            double right, double bottom,
            double epsilon)
        {
            // ToDo: Figure out code to check whether a quadratic Bézier curve and a line segment intersect.
            _ = aX;
            _ = aY;
            _ = bX;
            _ = bY;
            _ = cX;
            _ = cY;
            _ = x;
            _ = y;
            _ = right;
            _ = bottom;
            _ = epsilon;
            throw new NotImplementedException();
        }

        /// <summary>
        /// Find out whether a quadratic Bézier segment and a rectangle intersects.
        /// </summary>
        /// <param name="aX">a x.</param>
        /// <param name="aY">a y.</param>
        /// <param name="bX">The b x.</param>
        /// <param name="bY">The b y.</param>
        /// <param name="cX">The c x.</param>
        /// <param name="cY">The c y.</param>
        /// <param name="rLeft">The r left.</param>
        /// <param name="rTop">The r top.</param>
        /// <param name="rRight">The r right.</param>
        /// <param name="rBottom">The r bottom.</param>
        /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool QuadraticBezierSegmentRectangleIntersects(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double rLeft, double rTop,
            double rRight, double rBottom,
            double epsilon)
        {
            var (minX, minY) = MinPoint(rLeft, rTop, rRight, rBottom);
            var (maxX, maxY) = MaxPoint(rLeft, rTop, rRight, rBottom);
            var topRight = new Point2D(maxX, minY);
            var bottomLeft = new Point2D(minX, maxY);
            return QuadraticBezierSegmentLineSegmentIntersects(aX, aY, bX, bY, cX, cY, minX, minY, topRight.X, topRight.Y, epsilon)
                || QuadraticBezierSegmentLineSegmentIntersects(aX, aY, bX, bY, cX, cY, topRight.X, topRight.Y, maxX, maxY, epsilon)
                || QuadraticBezierSegmentLineSegmentIntersects(aX, aY, bX, bY, cX, cY, maxX, maxY, bottomLeft.X, bottomLeft.Y, epsilon)
                || QuadraticBezierSegmentLineSegmentIntersects(aX, aY, bX, bY, cX, cY, bottomLeft.X, bottomLeft.Y, minX, minY, epsilon);
        }

        /// <summary>
        /// Find out whether a cubic Bézier segment and a line segment intersects.
        /// </summary>
        /// <param name="aX">a x.</param>
        /// <param name="aY">a y.</param>
        /// <param name="bX">The b x.</param>
        /// <param name="bY">The b y.</param>
        /// <param name="cX">The c x.</param>
        /// <param name="cY">The c y.</param>
        /// <param name="dX">The d x.</param>
        /// <param name="dY">The d y.</param>
        /// <param name="a1X">The a1 x.</param>
        /// <param name="a1Y">The a1 y.</param>
        /// <param name="a2X">The a2 x.</param>
        /// <param name="a2Y">The a2 y.</param>
        /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CubicBezierSegmentLineSegmentIntersects(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double dX, double dY,
            double a1X, double a1Y,
            double a2X, double a2Y,
            double epsilon)
        {
            _ = aX;
            _ = aY;
            _ = bX;
            _ = bY;
            _ = cX;
            _ = cY;
            _ = dX;
            _ = dY;
            _ = a1X;
            _ = a1Y;
            _ = a2X;
            _ = a2Y;
            _ = epsilon;
            throw new NotImplementedException();
        }

        // ToDo: Figure out code to check whether a cubic Bézier curve and a line segment intersect.

        /// <summary>
        /// Find out whether a cubic Bézier segment and a rectangle intersects.
        /// </summary>
        /// <param name="aX">a x.</param>
        /// <param name="aY">a y.</param>
        /// <param name="bX">The b x.</param>
        /// <param name="bY">The b y.</param>
        /// <param name="cX">The c x.</param>
        /// <param name="cY">The c y.</param>
        /// <param name="dX">The d x.</param>
        /// <param name="dY">The d y.</param>
        /// <param name="r1X">The r1 x.</param>
        /// <param name="r1Y">The r1 y.</param>
        /// <param name="r2X">The r2 x.</param>
        /// <param name="r2Y">The r2 y.</param>
        /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CubicBezierSegmentRectangleIntersects(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double dX, double dY,
            double r1X, double r1Y,
            double r2X, double r2Y,
            double epsilon)
        {
            var (minX, minY) = MinPoint(r1X, r1Y, r2X, r2Y);
            var (maxX, maxY) = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(maxX, minY);
            var bottomLeft = new Point2D(minX, maxY);
            return CubicBezierSegmentLineSegmentIntersects(aX, aY, bX, bY, cX, cY, dX, dY, minX, minY, topRight.X, topRight.Y, epsilon)
                || CubicBezierSegmentLineSegmentIntersects(aX, aY, bX, bY, cX, cY, dX, dY, topRight.X, topRight.Y, maxX, maxY, epsilon)
                || CubicBezierSegmentLineSegmentIntersects(aX, aY, bX, bY, cX, cY, dX, dY, maxX, maxY, bottomLeft.X, bottomLeft.Y, epsilon)
                || CubicBezierSegmentLineSegmentIntersects(aX, aY, bX, bY, cX, cY, dX, dY, bottomLeft.X, bottomLeft.Y, minX, minY, epsilon);
        }

        /// <summary>
        /// Determines if this rectangle interests with another rectangle.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="width0">The width0.</param>
        /// <param name="height0">The height0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="width1">The width1.</param>
        /// <param name="height1">The height1.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RectangleRectangleIntersects(
            double x0, double y0,
            double width0, double height0,
            double x1, double y1,
            double width1, double height1,
            double epsilon = double.Epsilon)
        {
            _ = epsilon;
            return (x1 < x0 + width0)
                   && (x0 < (x1 + width1))
                   && (y1 < y0 + height0)
                   && (y0 < y1 + height1);
        }

        /// <summary>
        /// Find out whether two circles intersects.
        /// </summary>
        /// <param name="cx0">The CX0.</param>
        /// <param name="cy0">The cy0.</param>
        /// <param name="radius0">The radius0.</param>
        /// <param name="cx1">The CX1.</param>
        /// <param name="cy1">The cy1.</param>
        /// <param name="radius1">The radius1.</param>
        /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
        /// <returns>
        /// Returns a Boolean value indicating whether the two shapes intersect.
        /// </returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/09/determine-where-two-circles-intersect-in-c/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CircleCircleIntersects(
            double cx0,
            double cy0,
            double radius0,
            double cx1,
            double cy1,
            double radius1,
            double epsilon = double.Epsilon)
        {
            // If either of the circles are empty, return no intersections.
            if ((radius0 == 0d) || (radius1 == 0d))
            {
                return false;
            }

            // Find the distance between the centers.
            var dx = cx0 - cx1;
            var dy = cy0 - cy1;
            var dist = Sqrt((dx * dx) + (dy * dy));

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
            else if ((Abs(dist) < epsilon) && (Abs(radius0 - radius1) < epsilon))
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
        #endregion Intersects Methods
    }
}
