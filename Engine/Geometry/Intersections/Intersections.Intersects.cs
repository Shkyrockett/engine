// <copyright file="Intersections.Intersects.cs" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
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

using System.Runtime.CompilerServices;
using static Engine.Maths;
using static System.Math;
using System;

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
        /// <param name="rect"></param>
        /// <param name="shape"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
        public static bool Intersects(Rectangle2D rect, Shape shape, double epsilon = Epsilon)
        {
            // Shapes arranged by degree and complexity.
            switch (shape)
            {
                case ScreenPoint p:
                    //return RectangleContainsPoint(rect.X, rect.Y, rect.Right, rect.Bottom, p.X, p.Y, epsilon)!= Inclusion.Outside ;
                    return PointRectangleIntersects(p.X, p.Y, rect.X, rect.Y, rect.Right, rect.Bottom, epsilon);
                case BezierSegmentX b:
                    throw new NotImplementedException();
                case LineSegment l:
                    return LineSegmentRectangleIntersects(l.AX, l.AY, l.BX, l.BY, rect.X, rect.Y, rect.Right, rect.Bottom, epsilon);
                case Ray r:
                    return RayRectangleIntersects(r.Location.X, r.Location.Y, r.Location.X + r.Direction.I, r.Location.Y + r.Direction.J, rect.X, rect.Y, rect.Right, rect.Bottom, epsilon);
                case Line l:
                    return LineRectangleIntersects(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, rect.X, rect.Y, rect.Right, rect.Bottom, epsilon);
                case QuadraticBezier b:
                    return QuadraticBezierSegmentRectangleIntersects(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, rect.X, rect.Y, rect.Right, rect.Bottom, epsilon);
                case CubicBezier b:
                    return CubicBezierSegmentRectangleIntersects(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, rect.X, rect.Y, rect.Right, rect.Bottom, epsilon);
                case Rectangle2D r:
                    return RectangleRectangleIntersects(r.X, r.Y, r.Right, r.Bottom, rect.X, rect.Y, rect.Right, rect.Bottom, epsilon);
                case Polyline p:
                    //return RectanglePolylineIntersects();
                    throw new NotImplementedException();
                case PolygonContour p:
                    throw new NotImplementedException();
                case Polygon p:
                    throw new NotImplementedException();
                case PolyBezierContour p:
                    throw new NotImplementedException();
                case PolyBezier p:
                    throw new NotImplementedException();
                case PolycurveContour p:
                    throw new NotImplementedException();
                case Polycurve p:
                    throw new NotImplementedException();
                default:
                    return false;
            }
        }

        /// <summary>
        /// Check whether two points intersect.
        /// </summary>
        /// <param name="point0"></param>
        /// <param name="point1"></param>
        /// <param name="epsilon"></param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this Point2D point0, Point2D point1, double epsilon = Epsilon)
            => point0 == point1;

        /// <summary>
        /// Check whether a point is coincident to a line segment.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="epsilon"></param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this Point2D p, Point2D a, Point2D b, double epsilon = Epsilon)
            => PointLineSegmentIntersects(p.X, p.Y, a.X, a.Y, b.X, b.Y, epsilon);

        /// <summary>
        /// Check whether a point is coincident to a line segment.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="epsilon"></param>
        /// <param name="p"></param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this Point2D p, LineSegment s, double epsilon = Epsilon)
            => PointLineSegmentIntersects(p.X, p.Y, s.A.X, s.A.Y, s.B.X, s.B.Y, epsilon);

        /// <summary>
        /// Check whether a point is coincident to a line segment.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="p"></param>
        /// <param name="epsilon"></param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this LineSegment s, Point2D p, double epsilon = Epsilon)
            => PointLineSegmentIntersects(p.X, p.Y, s.A.X, s.A.Y, s.B.X, s.B.Y, epsilon);

        /// <summary>
        /// Check whether two line segments intersect.
        /// </summary>
        /// <param name="s0"></param>
        /// <param name="s1"></param>
        /// <param name="epsilon"></param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this LineSegment s0, LineSegment s1, double epsilon = Epsilon)
            => LineSegmentLineSegmentIntersects(s0.A.X, s0.A.Y, s0.B.X, s0.B.Y, s1.A.X, s1.A.Y, s1.B.X, s1.B.Y, epsilon);

        /// <summary>
        /// Determines if this rectangle interests with another rectangle.
        /// </summary>
        /// <param name="rect1"></param>
        /// <param name="rect2"></param>
        /// <param name="epsilon"></param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this Rectangle2D rect1, Rectangle2D rect2, double epsilon = Epsilon)
            => RectangleRectangleIntersects(rect1.X, rect1.Y, rect1.Width, rect1.Height, rect2.X, rect2.Y, rect2.Width, rect2.Height, epsilon);

        /// <summary>
        /// Determines if this Circle interests with another Circle.
        /// </summary>
        /// <param name="c0"></param>
        /// <param name="c1"></param>
        /// <param name="epsilon"></param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this Circle c0, Circle c1, double epsilon = Epsilon)
            => CircleCircleIntersects(c0.X, c0.Y, c0.Radius, c1.X, c1.Y, c1.Radius, epsilon);
        #endregion Intersects Extension Method Overloads

        #region Intersects Methods
        /// <summary>
        /// Checks whether two points are at the same location.
        /// </summary>
        /// <param name="point0X"></param>
        /// <param name="point0Y"></param>
        /// <param name="point1X"></param>
        /// <param name="point1Y"></param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointPointIntersects(
            double point0X, double point0Y,
            double point1X, double point1Y)
            => point0X == point1X && point0Y == point1Y;

        /// <summary>
        /// Check whether a point is coincident to a line segment.
        /// </summary>
        /// <param name="pointX"></param>
        /// <param name="pointY"></param>
        /// <param name="segmentAX"></param>
        /// <param name="segmentAY"></param>
        /// <param name="segmentBX"></param>
        /// <param name="segmentBY"></param>
        /// <param name="epsilon"></param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
        /// <acknowledgment>
        /// http://www.angusj.com/delphi/clipper.php
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointLineSegmentIntersects(
            double pointX, double pointY,
            double segmentAX, double segmentAY,
            double segmentBX, double segmentBY,
            double epsilon = Epsilon)
            => ((pointX == segmentAX) && (pointY == segmentAY))
                || ((pointX == segmentBX) && (pointY == segmentBY))
                || (((pointX > segmentAX) == (pointX < segmentBX))
                && ((pointY > segmentAY) == (pointY < segmentBY))
                && ((pointX - segmentAX) * (segmentBY - segmentAY) == (pointY - segmentAY) * (segmentBX - segmentAX)));

        /// <summary>
        /// Check whether a point is coincident to a line segment.
        /// </summary>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <param name="lX"></param>
        /// <param name="lY"></param>
        /// <param name="lI"></param>
        /// <param name="lJ"></param>
        /// <param name="epsilon"></param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
        /// <acknowledgment>
        /// http://www.angusj.com/delphi/clipper.php
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointRayIntersects(
            double pX, double pY,
            double lX, double lY,
            double lI, double lJ,
            double epsilon = Epsilon)
            => ((pX == lX) && (pY == lY)) || ((pX == lI) && (pY == lJ))
                || (
                    ((pX - lX > 0) == (pX - lI < 0)) && ((pY - lY > 0) == (pY - lJ < 0))
                    && ((pX - lX) * (lJ - lY) == (pY - lY) * (lI - lX))
                );

        /// <summary>
        /// Check whether a point is coincident to a line segment.
        /// </summary>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <param name="lX"></param>
        /// <param name="lY"></param>
        /// <param name="lI"></param>
        /// <param name="lJ"></param>
        /// <param name="epsilon"></param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
        /// <acknowledgment>
        /// http://www.angusj.com/delphi/clipper.php
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointLineIntersects(
            double pX, double pY,
            double lX, double lY,
            double lI, double lJ,
            double epsilon = Epsilon)
            => ((pX == lX) && (pY == lY))
                || (pX == lI) && (pY == lJ)
                && ((pX - lX) * (lJ - lY) == (pY - lY) * (lI - lX));

        /// <summary>
        /// Check whether a point is within a rectangle.
        /// </summary>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointRectangleIntersects(
            double pX, double pY,
            double r1X, double r1Y,
            double r2X, double r2Y,
            double epsilon = Epsilon)
        {
            var (minX, minY) = MinPoint(r1X, r1Y, r2X, r2Y);
            var (maxX, maxY) = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(maxX, minY);
            var bottomLeft = new Point2D(minX, maxY);
            if (PointLineSegmentIntersects(pX, pY, minX, minY, topRight.X, topRight.Y, epsilon))
            {
                return true;
            }

            if (PointLineSegmentIntersects(pX, pY, topRight.X, topRight.Y, maxX, maxY, epsilon))
            {
                return true;
            }

            if (PointLineSegmentIntersects(pX, pY, maxX, maxY, bottomLeft.X, bottomLeft.Y, epsilon))
            {
                return true;
            }

            if (PointLineSegmentIntersects(pX, pY, bottomLeft.X, bottomLeft.Y, minX, minY, epsilon))
            {
                return true;
            }

            return false;
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
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>        /// <acknowledgment>
        /// http://www.vb-helper.com/howto_segments_intersect.html
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineSegmentLineSegmentIntersects(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double epsilon = Epsilon)
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
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineSegmentRectangleIntersects(
            double a1X, double a1Y,
            double a2X, double a2Y,
            double r1X, double r1Y,
            double r2X, double r2Y,
            double epsilon = Epsilon)
        {
            var (minX, minY) = MinPoint(r1X, r1Y, r2X, r2Y);
            var (maxX, maxY) = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(maxX, minY);
            var bottomLeft = new Point2D(minX, maxY);
            if (LineSegmentLineSegmentIntersects(a1X, a1Y, a2X, a2Y, minX, minY, topRight.X, topRight.Y, epsilon))
            {
                return true;
            }

            if (LineSegmentLineSegmentIntersects(a1X, a1Y, a2X, a2Y, topRight.X, topRight.Y, maxX, maxY, epsilon))
            {
                return true;
            }

            if (LineSegmentLineSegmentIntersects(a1X, a1Y, a2X, a2Y, maxX, maxY, bottomLeft.X, bottomLeft.Y, epsilon))
            {
                return true;
            }

            if (LineSegmentLineSegmentIntersects(a1X, a1Y, a2X, a2Y, bottomLeft.X, bottomLeft.Y, minX, minY, epsilon))
            {
                return true;
            }

            return false;
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
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>        /// <acknowledgment>
        /// http://www.vb-helper.com/howto_segments_intersect.html
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RayLineSegmentIntersects(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double epsilon = Epsilon)
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
        /// Find out whether a ray and a rectangle intersects.
        /// </summary>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RayRectangleIntersects(
            double a1X, double a1Y,
            double a2X, double a2Y,
            double r1X, double r1Y,
            double r2X, double r2Y,
            double epsilon = Epsilon)
        {
            var (minX, minY) = MinPoint(r1X, r1Y, r2X, r2Y);
            var (maxX, maxY) = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(maxX, minY);
            var bottomLeft = new Point2D(minX, maxY);
            if (RayLineSegmentIntersects(a1X, a1Y, a2X, a2Y, minX, minY, topRight.X, topRight.Y, epsilon))
            {
                return true;
            }

            if (RayLineSegmentIntersects(a1X, a1Y, a2X, a2Y, topRight.X, topRight.Y, maxX, maxY, epsilon))
            {
                return true;
            }

            if (RayLineSegmentIntersects(a1X, a1Y, a2X, a2Y, maxX, maxY, bottomLeft.X, bottomLeft.Y, epsilon))
            {
                return true;
            }

            if (RayLineSegmentIntersects(a1X, a1Y, a2X, a2Y, bottomLeft.X, bottomLeft.Y, minX, minY, epsilon))
            {
                return true;
            }

            return false;
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
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
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
            double epsilon = Epsilon)
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
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
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
            double epsilon = Epsilon)
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
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
        /// <acknowledgment>
        /// http://www.vb-helper.com/howto_segments_intersect.html
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineLineIntersects(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double epsilon = Epsilon)
                // Check if the lines are parallel. Non-parallel lines always intersect.
                => Abs(((y3 - y2) * (x1 - x0)) - ((x3 - x2) * (y1 - y0))) < epsilon;

        /// <summary>
        /// Find out whether a line and a rectangle intersects.
        /// </summary>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineRectangleIntersects(
            double a1X, double a1Y,
            double a2X, double a2Y,
            double r1X, double r1Y,
            double r2X, double r2Y,
            double epsilon = Epsilon)
        {
            var (minX, minY) = MinPoint(r1X, r1Y, r2X, r2Y);
            var (maxX, maxY) = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(maxX, minY);
            var bottomLeft = new Point2D(minX, maxY);
            if (LineLineSegmentIntersects(a1X, a1Y, a2X, a2Y, minX, minY, topRight.X, topRight.Y, epsilon))
            {
                return true;
            }

            if (LineLineSegmentIntersects(a1X, a1Y, a2X, a2Y, topRight.X, topRight.Y, maxX, maxY, epsilon))
            {
                return true;
            }

            if (LineLineSegmentIntersects(a1X, a1Y, a2X, a2Y, maxX, maxY, bottomLeft.X, bottomLeft.Y, epsilon))
            {
                return true;
            }

            if (LineLineSegmentIntersects(a1X, a1Y, a2X, a2Y, bottomLeft.X, bottomLeft.Y, minX, minY, epsilon))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Find out whether a quadratic Bézier segment and a line segment intersects.
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool QuadraticBezierSegmentLineSegmentIntersects(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double x, double y,
            double right, double bottom,
            double epsilon)
            // ToDo: Figure out code to check whether a quadratic Bézier curve and a line segment intersect.
            => throw new NotImplementedException();

        /// <summary>
        /// Find out whether a quadratic Bézier segment and a rectangle intersects.
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="rLeft"></param>
        /// <param name="rTop"></param>
        /// <param name="rRight"></param>
        /// <param name="rBottom"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool QuadraticBezierSegmentRectangleIntersects(
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
            if (QuadraticBezierSegmentLineSegmentIntersects(aX, aY, bX, bY, cX, cY, minX, minY, topRight.X, topRight.Y, epsilon))
            {
                return true;
            }

            if (QuadraticBezierSegmentLineSegmentIntersects(aX, aY, bX, bY, cX, cY, topRight.X, topRight.Y, maxX, maxY, epsilon))
            {
                return true;
            }

            if (QuadraticBezierSegmentLineSegmentIntersects(aX, aY, bX, bY, cX, cY, maxX, maxY, bottomLeft.X, bottomLeft.Y, epsilon))
            {
                return true;
            }

            if (QuadraticBezierSegmentLineSegmentIntersects(aX, aY, bX, bY, cX, cY, bottomLeft.X, bottomLeft.Y, minX, minY, epsilon))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Find out whether a cubic Bézier segment and a line segment intersects.
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="dX"></param>
        /// <param name="dY"></param>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
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
            // ToDo: Figure out code to check whether a cubic Bézier curve and a line segment intersect.
            => throw new NotImplementedException();

        /// <summary>
        /// Find out whether a cubic Bézier segment and a rectangle intersects.
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="dX"></param>
        /// <param name="dY"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CubicBezierSegmentRectangleIntersects(
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
            if (CubicBezierSegmentLineSegmentIntersects(aX, aY, bX, bY, cX, cY, dX, dY, minX, minY, topRight.X, topRight.Y, epsilon))
            {
                return true;
            }

            if (CubicBezierSegmentLineSegmentIntersects(aX, aY, bX, bY, cX, cY, dX, dY, topRight.X, topRight.Y, maxX, maxY, epsilon))
            {
                return true;
            }

            if (CubicBezierSegmentLineSegmentIntersects(aX, aY, bX, bY, cX, cY, dX, dY, maxX, maxY, bottomLeft.X, bottomLeft.Y, epsilon))
            {
                return true;
            }

            if (CubicBezierSegmentLineSegmentIntersects(aX, aY, bX, bY, cX, cY, dX, dY, bottomLeft.X, bottomLeft.Y, minX, minY, epsilon))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines if this rectangle interests with another rectangle.
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="height0"></param>
        /// <param name="width0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="height1"></param>
        /// <param name="epsilon"></param>
        /// <param name="width1"></param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RectangleRectangleIntersects(
            double x0, double y0,
            double width0, double height0,
            double x1, double y1,
            double width1, double height1,
            double epsilon = Epsilon)
            => (x1 < x0 + width0)
            && (x0 < (x1 + width1))
            && (y1 < y0 + height0)
            && (y0 < y1 + height1);

        /// <summary>
        /// Find out whether two circles intersects.
        /// </summary>
        /// <param name="cx0"></param>
        /// <param name="cy0"></param>
        /// <param name="radius0"></param>
        /// <param name="cx1"></param>
        /// <param name="cy1"></param>
        /// <param name="radius1"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
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
            double epsilon = Epsilon)
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
