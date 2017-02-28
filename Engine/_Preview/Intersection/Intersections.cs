// <copyright file="Intersections.cs" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>

// <copyright company="kevlindev" >
//     Many of the Intersections methods were adapted from Kevin Lindsey's site http://www.kevlindev.com/gui/math/intersection/. 
//     Copyright (c) 2000 - 2003 Kevin Lindsey. All rights reserved.
// </copyright>
// <author id="thelonious">Kevin Lindsey</author>
// <license>
//     Licensed under the BSD-3-Clause https://github.com/thelonious/kld-intersections/blob/development/LICENSE
// </license>

// <copyright company="angusj" >
//     The Point in Polygon method is from the Clipper Library.
//     Copyright (c) 2010-2014 Angus Johnson. All rights reserved.
// </copyright>
// <author id="angusj">Angus Johnson</author>
// <license id="Boost">
//     Licensed under the Boost Software License (http://www.boost.org/LICENSE_1_0.txt).
// </license>

// <copyright company="vb-helper" >
//     Some of the methods came from Rod Stephens excellent blogs vb-helper(http://vb-helper.com), and csharphelper (http://csharphelper.com), as well as from his books.
//     Copyright (c) Rod Stephens.
// </copyright>
// <author id="RodStephens">Rod Stephens</author>
// <license id="No Restrictions">
//     You can use the code you find on this site or in my books. I request but don’t require an acknowledgment.
//     I also recommend (but again don’t require) that you put the URL where you found the code in a comment inside your code in case you need to look it up later.
//     So really no restrictions. (http://csharphelper.com/blog/rod/)
// </license>

// <summary></summary>
// <remarks></remarks>

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static Engine.Maths;
using static System.Math;
using static Engine.Measurements;

namespace Engine
{
    /// <summary>
    /// A collection of methods for collecting the interactions of geometry.
    /// </summary>
    public static class Intersections
    {
        #region Between Extension Method Overloads

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <param name="m"></param>
        /// <param name="M"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://pomax.github.io/bezierinfo
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Between(double v, double m, double M)
            => (m <= v && v <= M) || Approximately(v, m) || Approximately(v, M);

        /// <summary>
        /// Check whether an angle lies within the sweep angle.
        /// </summary>
        /// <param name="angle">Angle of rotation to check.</param>
        /// <param name="startAngle">The starting angle.</param>
        /// <param name="sweepAngle">The amount of angle to offset from the start angle.</param>
        /// <returns>A Boolean value indicating whether an angle is between two others.</returns>
        /// <remarks>
        /// http://www.xarg.org/2010/06/is-an-angle-between-two-other-angles/
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Within(double angle, double startAngle, double sweepAngle)
        {
            // If the sweep angle is greater than 360 degrees it is overlapping, so any angle would intersect the sweep angle.
            if (sweepAngle > Tau)
                return true;

            // Wrap the angles to values between 2PI and -2PI.
            double s = Maths.WrapAngle(startAngle);
            double e = Maths.WrapAngle(s + sweepAngle);
            double a = Maths.WrapAngle(angle);

            // return whether the angle is contained within the sweep angle.
            // The calculations are opposite when the sweep angle is negative.
            return (sweepAngle >= 0) ?
                (s < e) ? a >= s && a <= e : a >= s || a <= e :
                (s > e) ? a <= s && a >= e : a <= s || a >= e;
        }

        /// <summary>
        /// Check whether a vector lies between two other vectors.
        /// </summary>
        /// <param name="a">The vector to compare.</param>
        /// <param name="b">The start vector.</param>
        /// <param name="c">The end vector.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Between(Vector2D a, Vector2D b, Vector2D c)
            => VectorBetweenVectorVector(a.I, a.J, b.I, b.J, c.I, c.J);

        #endregion

        #region Contains Extension Method Overloads

        /// <summary>
        /// Determines whether the specified point is contained within the region defined by this <see cref="Point2D"/>.
        /// </summary>
        /// <param name="point0">First Point to test.</param>
        /// <param name="point1">Second Point to test.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this Point2D point0, Point2D point1)
            => point0 == point1 ? Inclusion.Boundary : Inclusion.Outside;

        /// <summary>
        /// Determines whether the specified point is contained within the region defined by this <see cref="LineSegment"/>.
        /// </summary>
        /// <param name="seg">Line segment to test.</param>
        /// <param name="point">Point to test.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this LineSegment seg, Point2D point)
            => PointLineSegmentIntersects(point.X, point.Y, seg.AX, seg.AY, seg.BX, seg.BY) ? Inclusion.Boundary : Inclusion.Outside;

        /// <summary>
        /// Determines whether the specified point is contained within the rectangular region defined by this <see cref="Rectangle2D"/>.
        /// </summary>
        /// <param name="rectangle"><see cref="Rectangle2D"/> class.</param>
        /// <param name="point">Point to test.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this Rectangle2D rectangle, Point2D point)
            => RectangleContainsPoint(rectangle.X, rectangle.Y, rectangle.Right, rectangle.Bottom, point.X, point.Y);

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="Contour"/>.
        /// </summary>
        /// <param name="polygon"><see cref="Contour"/> class.</param>
        /// <param name="point">Point to test.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this Contour polygon, Point2D point)
            => PolygonContainsPoint(polygon.Points, point.X, point.Y);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="figure"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this PathContour figure, Point2D point)
            => GeometryPathContainsPoint(figure, point);

        /// <summary>
        /// Determines whether the specified point is contained withing the set of regions defined by this <see cref="Polygon"/>.
        /// </summary>
        /// <param name="polygons">List of <see cref="Contour"/> classes.</param>
        /// <param name="point">Point to test.</param>
        /// <returns></returns>
        /// <remarks>This function automatically knows that enclosed polygons are "no-go" areas.</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this Polygon polygons, Point2D point)
            => PolygonSetContainsPoint(polygons.Contours, point.X, point.Y);

        /// <summary>
        /// Determines whether the specified point is contained within the region defined by this <see cref="Circle"/>.
        /// </summary>
        /// <param name="circle"><see cref="Circle"/> class.</param>
        /// <param name="point">Point to test.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this Circle circle, Point2D point)
            => CircleContainsPoint(circle.X, circle.Y, circle.Radius, point.X, point.Y);

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="Ellipse"/>.
        /// </summary>
        /// <param name="ellipse"><see cref="Ellipse"/> class.</param>
        /// <param name="point">Point to test.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this Ellipse ellipse, Point2D point)
            => EllipseContainsPoint(ellipse.Center.X, ellipse.Center.Y, ellipse.RX, ellipse.RY, ellipse.Angle, point.X, point.Y);

        /// <summary>
        /// Determines whether the specified point is contained within the region defined by this <see cref="Circle"/>.
        /// </summary>
        /// <param name="arc"><see cref="Circle"/> class.</param>
        /// <param name="point">Point to test.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this CircularArc arc, Point2D point)
            => CircularArcSectorContainsPoint(arc.X, arc.Y, arc.Radius, arc.StartAngle, arc.SweepAngle, point.X, point.Y);

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="EllipticalArc"/>.
        /// </summary>
        /// <param name="ellipseArc"><see cref="Ellipse"/> class.</param>
        /// <param name="point">Point to test.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this EllipticalArc ellipseArc, Point2D point)
            => EllipticalArcContainsPoint(ellipseArc.Center.X, ellipseArc.Center.Y, ellipseArc.RX, ellipseArc.RY, ellipseArc.Angle, ellipseArc.StartAngle, ellipseArc.SweepAngle, point.X, point.Y);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(this Rectangle2D a, Rectangle2D b)
            => RectangleContainsRectangle(a.X, a.Y, a.Width, a.Height, b.X, b.Y, b.Width, b.Height);

        #endregion

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
        /// 
        /// </summary>
        /// <param name="r0"></param>
        /// <param name="r1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray r0, Ray r1)
            => RayRayIntersection(r0.Location.X, r0.Location.Y, r0.Direction.I, r0.Direction.J, r1.Location.X, r1.Location.Y, r1.Direction.I, r1.Direction.J);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r0"></param>
        /// <param name="r1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line r0, Line r1)
            => LineLineIntersection(r0.Location.X, r0.Location.Y, r0.Direction.I, r0.Direction.J, r1.Location.X, r1.Location.Y, r1.Direction.I, r1.Direction.J);

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
            => EllipseLineSegmentIntersection(e.X, e.Y, e.RX, e.RY, e.Angle, s.A.X, s.A.Y, s.B.X, s.B.Y);

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a rectangle.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, Rectangle2D r)
            => UnrotatedEllipseRectangleIntersection(e.X, e.Y, e.RX, e.RY, r.X, r.Y, r.Right, r.Bottom);

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a rectangle.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r, Ellipse e)
            => UnrotatedEllipseRectangleIntersection(e.X, e.Y, e.RX, e.RY, r.X, r.Y, r.Right, r.Bottom);

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a polygon.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, Contour p)
            => UnrotatedEllipsePolygonIntersection(e.X, e.Y, e.RX, e.RY, p.Points);

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a polygon.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Contour p, Ellipse e)
            => UnrotatedEllipsePolygonIntersection(e.X, e.Y, e.RX, e.RY, p.Points);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, Ellipse e)
            => CircleUnrotatedEllipseIntersection(c.Center.X, c.Center.Y, c.Radius, e.Center.X, e.Center.Y, e.RX, e.RY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, Circle c)
            => CircleUnrotatedEllipseIntersection(c.Center.X, c.Center.Y, c.Radius, e.Center.X, e.Center.Y, e.RX, e.RY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e0"></param>
        /// <param name="e1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e0, Ellipse e1)
            => UnrotatedEllipseUnrotatedEllipseIntersection(e0.Center.X, e0.Center.Y, e0.RX, e0.RY, e1.Center.X, e1.Center.Y, e1.RX, e1.RY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r, LineSegment l)
            => LineSegmentRectangleIntersection(l.AX, l.AY, l.BX, l.BY, r.X, r.Y, r.Right, r.Bottom);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment l, Rectangle2D r)
            => LineSegmentRectangleIntersection(l.AX, l.AY, l.BX, l.BY, r.X, r.Y, r.Right, r.Bottom);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r0"></param>
        /// <param name="r1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r0, Rectangle2D r1)
            => RectangleRectangleIntersection(r0.X, r0.Y, r0.Right, r0.Bottom, r1.X, r1.Y, r1.Right, r1.Bottom);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="l"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment l, Contour p)
            => LineSegmentPolygonIntersection(l.AX, l.AY, l.BX, l.BY, p.Points);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Contour p, LineSegment l)
            => LineSegmentPolygonIntersection(l.AX, l.AY, l.BX, l.BY, p.Points);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Contour p, Rectangle2D l)
            => PolygonRectangleIntersection(p.Points, l.X, l.Y, l.Right, l.Bottom);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="l"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D l, Contour p)
            => PolygonRectangleIntersection(p.Points, l.X, l.Y, l.Right, l.Bottom);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Contour p0, Contour p1)
            => PolygonPolygonIntersection(p0.Points, p1.Points);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, Rectangle2D r)
            => CircleRectangleIntersection(c.X, c.Y, c.Radius, r.X, r.Y, r.Right, r.Bottom);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r, Circle c)
            => CircleRectangleIntersection(c.X, c.Y, c.Radius, r.X, r.Y, r.Right, r.Bottom);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b, LineSegment l)
            => CubicBezierLineSegmentIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, l.AX, l.AY, l.BX, l.BY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="l"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment l, CubicBezier b)
            => CubicBezierLineSegmentIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, l.AX, l.AY, l.BX, l.BY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b, Rectangle2D r)
            => CubicBezierLineSegmentIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, r.X, r.Y, r.Right, r.Bottom);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r, CubicBezier b)
            => CubicBezierLineSegmentIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, r.X, r.Y, r.Right, r.Bottom);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b, Contour p)
            => CubicBezierPolygonIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, p.Points);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Contour p, CubicBezier b)
            => CubicBezierPolygonIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, p.Points);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b, Circle c)
            => CubicBezierCircleIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, c.X, c.Y, c.Radius);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, CubicBezier b)
            => CubicBezierCircleIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, c.X, c.Y, c.Radius);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b, Ellipse e)
            => CubicBezierUnrotatedEllipseIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, e.X, e.Y, e.RX, e.RY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, CubicBezier b)
            => CubicBezierUnrotatedEllipseIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, e.X, e.Y, e.RX, e.RY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="b1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b0, CubicBezier b1)
            => CubicBezierCubicBezierIntersection(b0.AX, b0.AY, b0.BX, b0.BY, b0.CX, b0.CY, b0.DX, b0.DY, b1.AX, b1.AY, b1.BX, b1.BY, b1.CX, b1.CY, b1.DX, b1.DY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b, LineSegment l)
            => QuadraticBezierLineSegmentIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, l.AX, l.AY, l.BX, l.BY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="l"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment l, QuadraticBezier b)
            => QuadraticBezierLineSegmentIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, l.AX, l.AY, l.BX, l.BY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b, Rectangle2D r)
            => QuadraticBezierRectangleIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, r.X, r.Y, r.Right, r.Bottom);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r, QuadraticBezier b)
            => QuadraticBezierRectangleIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, r.X, r.Y, r.Right, r.Bottom);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b, Contour p)
            => QuadraticBezierPolygonIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, p.Points);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Contour p, QuadraticBezier b)
            => QuadraticBezierPolygonIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, p.Points);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b, Circle c)
            => QuadraticBezierCircleIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, c.X, c.Y, c.Radius);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, QuadraticBezier b)
            => QuadraticBezierCircleIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, c.X, c.Y, c.Radius);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b, Ellipse e)
            => QuadraticBezierUnrotatedEllipseIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, e.X, e.Y, e.RX, e.RY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, QuadraticBezier b)
            => QuadraticBezierUnrotatedEllipseIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, e.X, e.Y, e.RX, e.RY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="b1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b0, CubicBezier b1)
            => QuadraticBezierCubicBezierIntersection(b0.AX, b0.AY, b0.BX, b0.BY, b0.CX, b0.CY, b1.AX, b1.AY, b1.BX, b1.BY, b1.CX, b1.CY, b1.DX, b1.DY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b0"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b1, QuadraticBezier b0)
            => QuadraticBezierCubicBezierIntersection(b0.AX, b0.AY, b0.BX, b0.BY, b0.CX, b0.CY, b1.AX, b1.AY, b1.BX, b1.BY, b1.CX, b1.CY, b1.DX, b1.DY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="b1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b0, QuadraticBezier b1)
            => QuadraticBezierQuadraticBezierIntersection(b0.AX, b0.AY, b0.BX, b0.BY, b0.CX, b0.CY, b1.AX, b1.AY, b1.BX, b1.BY, b1.CX, b1.CY);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="b1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this BezierSegment b0, BezierSegment b1)
        {
            switch (b0.Degree)
            {
                case PolynomialDegree.Linear:
                    switch (b1.Degree)
                    {
                        case PolynomialDegree.Linear:
                            return LineSegmentLineSegmentIntersection(b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b1[0].X, b1[0].Y, b1[1].X, b1[1].Y);
                        case PolynomialDegree.Quadratic:
                            return QuadraticBezierLineSegmentIntersection(b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b0[2].X, b0[2].Y, b1[0].X, b1[0].Y, b1[1].X, b1[1].Y);
                        case PolynomialDegree.Cubic:
                            return CubicBezierLineSegmentIntersection(b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b0[2].X, b0[2].Y, b0[3].X, b0[3].Y, b1[0].X, b1[0].Y, b1[1].X, b1[1].Y);
                        default:
                            return new Intersection(IntersectionState.NoIntersection);
                    }
                case PolynomialDegree.Quadratic:
                    switch (b1.Degree)
                    {
                        case PolynomialDegree.Linear:
                            return QuadraticBezierLineSegmentIntersection(b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b0[2].X, b0[2].Y, b1[0].X, b1[0].Y, b1[1].X, b1[1].Y);
                        case PolynomialDegree.Quadratic:
                            return QuadraticBezierQuadraticBezierIntersection(b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b0[2].X, b0[2].Y, b1[0].X, b1[0].Y, b1[1].X, b1[1].Y, b1[2].X, b1[2].Y);
                        case PolynomialDegree.Cubic:
                            return QuadraticBezierCubicBezierIntersection(b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b0[2].X, b0[2].Y, b1[0].X, b1[0].Y, b1[1].X, b1[1].Y, b1[2].X, b1[2].Y, b1[3].X, b1[3].Y);
                        default:
                            return new Intersection(IntersectionState.NoIntersection);
                    }
                case PolynomialDegree.Cubic:
                    switch (b1.Degree)
                    {
                        case PolynomialDegree.Linear:
                            return CubicBezierLineSegmentIntersection(b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b0[2].X, b0[2].Y, b0[3].X, b0[3].Y, b1[0].X, b1[0].Y, b1[1].X, b1[1].Y);
                        case PolynomialDegree.Quadratic:
                            return QuadraticBezierCubicBezierIntersection(b1[0].X, b1[0].Y, b1[1].X, b1[1].Y, b1[2].X, b1[2].Y, b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b0[2].X, b0[2].Y, b0[3].X, b0[3].Y);
                        case PolynomialDegree.Cubic:
                            return CubicBezierCubicBezierIntersection(b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b0[2].X, b0[2].Y, b0[3].X, b0[3].Y, b1[0].X, b1[0].Y, b1[1].X, b1[1].Y, b1[2].X, b1[2].Y, b1[3].X, b1[3].Y);
                        default:
                            return new Intersection(IntersectionState.NoIntersection);
                    }
                default:
                    return new Intersection(IntersectionState.NoIntersection);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment l, BezierSegment b0)
            => Intersection(b0, l);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this BezierSegment b, LineSegment l)
        {
            switch (b.Degree)
            {
                case PolynomialDegree.Linear:
                    return LineSegmentLineSegmentIntersection(b[0].X, b[0].Y, b[1].X, b[1].Y, l.AX, l.AY, l.BX, l.BY);
                case PolynomialDegree.Quadratic:
                    return QuadraticBezierLineSegmentIntersection(b[0].X, b[0].Y, b[1].X, b[1].Y, b[2].X, b[2].Y, l.AX, l.AY, l.BX, l.BY);
                case PolynomialDegree.Cubic:
                    return CubicBezierLineSegmentIntersection(b[0].X, b[0].Y, b[1].X, b[1].Y, b[2].X, b[2].Y, b[3].X, b[3].Y, l.AX, l.AY, l.BX, l.BY);
                default:
                    return new Intersection(IntersectionState.NoIntersection);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="b1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b0, BezierSegment b1)
            => Intersection(b1, b0);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="b1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this BezierSegment b0, QuadraticBezier b1)
        {
            switch (b0.Degree)
            {
                case PolynomialDegree.Linear:
                    return QuadraticBezierLineSegmentIntersection(b1.AX, b1.AY, b1.BX, b1.BY, b1.CX, b1.CY, b0[0].X, b0[0].Y, b0[1].X, b0[1].Y);
                case PolynomialDegree.Quadratic:
                    return QuadraticBezierQuadraticBezierIntersection(b1.AX, b1.AY, b1.BX, b1.BY, b1.CX, b1.CY, b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b0[2].X, b0[2].Y);
                case PolynomialDegree.Cubic:
                    return QuadraticBezierCubicBezierIntersection(b1.AX, b1.AY, b1.BX, b1.BY, b1.CX, b1.CY, b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b0[2].X, b0[2].Y, b0[3].X, b0[3].Y);
                default:
                    return new Intersection(IntersectionState.NoIntersection);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="b1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b0, BezierSegment b1)
            => Intersection(b1, b0);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this BezierSegment b, CubicBezier c)
        {
            switch (b.Degree)
            {
                case PolynomialDegree.Linear:
                    return CubicBezierLineSegmentIntersection(c.AX, c.AY, c.BX, c.BY, c.CX, c.CY, c.DX, c.DY, b[0].X, b[0].Y, b[1].X, b[1].Y);
                case PolynomialDegree.Quadratic:
                    return QuadraticBezierCubicBezierIntersection(b[0].X, b[0].Y, b[1].X, b[1].Y, b[2].X, b[2].Y, c.AX, c.AY, c.BX, c.BY, c.CX, c.CY, c.DX, c.DY);
                case PolynomialDegree.Cubic:
                    return CubicBezierCubicBezierIntersection(c.AX, c.AY, c.BX, c.BY, c.CX, c.CY, c.DX, c.DY, b[0].X, b[0].Y, b[1].X, b[1].Y, b[2].X, b[2].Y, b[3].X, b[3].Y);
                default:
                    return new Intersection(IntersectionState.NoIntersection);
            }
        }

        #endregion

        #region Between Methods

        /// <summary>
        /// Check whether a vector lies between two other vectors.
        /// </summary>
        /// <param name="i0">The horizontal component of the vector to compare.</param>
        /// <param name="j0">The vertical component of the vector to compare.</param>
        /// <param name="i1">The start vector horizontal component.</param>
        /// <param name="j1">The start vector vertical component.</param>
        /// <param name="i2">The end vector horizontal component.</param>
        /// <param name="j2">The end vector vertical component.</param>
        /// <returns>A boolean value representing whether the reference vector is contained within the start and end vectors.</returns>
        /// <remarks>
        /// http://math.stackexchange.com/questions/1698835/find-if-a-vector-is-between-2-vectors
        /// http://stackoverflow.com/questions/13640931/how-to-determine-if-a-vector-is-between-two-other-vectors
        /// http://gamedev.stackexchange.com/questions/22392/what-is-a-good-way-to-determine-if-a-vector-is-between-two-other-vectors-in-2d
        /// http://math.stackexchange.com/questions/169998/figure-out-if-a-fourth-point-resides-within-an-angle-created-by-three-other-poin
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool VectorBetweenVectorVector(double i0, double j0, double i1, double j1, double i2, double j2)
            => ((i1 * j0) - (j1 * i0)) * ((i1 * j2) - (j1 * i2)) >= 0
            && ((i2 * j0) - (j2 * i0)) * ((i2 * j1) - (j2 * i1)) >= 0;

        #endregion

        #region Contains Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="o"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool PointInTriangle(LineSegment s, Point2D o, Point2D p)
        {
            int x = Sign(s.A, s.B, p);
            return ((x == Sign(s.B, o, p)) && (x == Sign(o, s.A, p)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion TriangleContainsPoint(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double pX, double pY)
        {
            var a = new Point2D(aX, aY);
            var b = new Point2D(bX, bY);
            var c = new Point2D(cX, cY);
            var p = new Point2D(pX, pY);
            if (Intersects(a, b) || Intersects(b, c) || Intersects(c, a)) return Inclusion.Boundary;
            bool clockwise = ((((b - a).CrossProduct(p - b))) >= 0);
            return !(((((c - b).CrossProduct(p - c)) >= 0) ^ clockwise) && ((((a - c).CrossProduct(p - a)) >= 0) ^ clockwise)) ? Inclusion.Inside : Inclusion.Outside;
        }

        /// <summary>
        /// Determines whether the specified point is contained within the rectangular region defined by this <see cref="Rectangle2D"/>.
        /// </summary>
        /// <param name="left">The left location of the <see cref="Rectangle2D"/>.</param>
        /// <param name="top">The top location of the <see cref="Rectangle2D"/>.</param>
        /// <param name="right">The right location of the <see cref="Rectangle2D"/>.</param>
        /// <param name="bottom">The bottom location of the <see cref="Rectangle2D"/>.</param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion RectangleContainsPoint(
            double left, double top,
            double right, double bottom,
            double pX, double pY)
            => (((left == pX || right == pX) && ((top <= pY) == (bottom >= pY)))
                || ((top == pY || bottom == pY) && ((left <= pX) == (right >= pX)))) ? Inclusion.Boundary
                : (left <= pX && pX < right && top <= pY && pY < bottom) ? Inclusion.Inside : Inclusion.Outside;

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="Contour"/>.
        /// </summary>
        /// <param name="points">The points that form the corners of the polygon.</param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion PolygonContainsPoint(
            List<Point2D> points,
            double pX, double pY)
        {
            // From Clipper library: http://www.angusj.com/delphi/clipper.php

            // returns 0 if false, +1 if true, -1 if pt on polygon boundary
            // See "The Point in Polygon Problem for Arbitrary Polygons" by Hormann & Agathos
            // http://www.inf.usi.ch/hormann/papers/Hormann.2001.TPI.pdf
            Inclusion result = Inclusion.Outside;

            // If the polygon has 2 or fewer points, it is a line or point and has no interior.
            if (points.Count < 3)
                return Inclusion.Outside;
            Point2D curPoint = points[0];
            for (int i = 1; i <= points.Count; ++i)
            {
                Point2D nextPoint = (i == points.Count ? points[0] : points[i]);
                if (Abs(nextPoint.Y - pY) < Epsilon)
                {
                    if ((Abs(nextPoint.X - pX) < Epsilon)
                        || (Abs(curPoint.Y - pY) < Epsilon
                        && ((nextPoint.X > pX) == (curPoint.X < pX))))
                    {
                        return Inclusion.Boundary;
                    }
                }

                if ((curPoint.Y < pY) != (nextPoint.Y < pY))
                {
                    if (curPoint.X >= pX)
                    {
                        if (nextPoint.X > pX)
                        {
                            result = 1 - result;
                        }
                        else
                        {
                            double determinant = (curPoint.X - pX) * (nextPoint.Y - pY) - (nextPoint.X - pX) * (curPoint.Y - pY);
                            if (Abs(determinant) < Epsilon)
                                return Inclusion.Boundary;
                            else if ((determinant > 0) == (nextPoint.Y > curPoint.Y))
                                result = 1 - result;
                        }
                    }
                    else if (nextPoint.X > pX)
                    {
                        double determinant = (curPoint.X - pX) * (nextPoint.Y - pY) - (nextPoint.X - pX) * (curPoint.Y - pY);
                        if (Abs(determinant) < Epsilon)
                            return Inclusion.Boundary;
                        if ((determinant > 0) == (nextPoint.Y > curPoint.Y))
                            result = 1 - result;
                    }
                }

                curPoint = nextPoint;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="figure"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion GeometryPathContainsPoint(PathContour figure, Point2D point)
        {
            Inclusion included = PolygonContainsPoint(figure.Nodes, point.X, point.Y);
            foreach (var item in figure?.Items)
            {
                switch (item)
                {
                    case PathArc t:
                        // This produces false negatives at the Polygon boundaries. But that is better than false positives.
                        var arc = t.Contains(point);
                        if (included == Inclusion.Boundary & arc == Inclusion.Inside) included = Inclusion.Inside;
                        //var line = Intersectings.LineSegmentPoint(t.Start.X, t.Start.Y, t.End.X, t.End.Y, point.X, point.Y);
                        included = included ^ arc;
                        if (arc == Inclusion.Boundary) included = Inclusion.Boundary;
                        break;
                    default:
                        break;
                }
            }

            return included;
        }

        /// <summary>
        /// Determines whether the specified point is contained withing the set of regions defined by this <see cref="Polygon"/>.
        /// </summary>
        /// <param name="polygons">List of polygons.</param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion PolygonSetContainsPoint(List<Contour> polygons, double pX, double pY)
        {
            Inclusion returnValue = Inclusion.Outside;

            foreach (Contour poly in polygons)
            {
                // Use alternating rule with XOR to determine if the point is in a polygon or a hole.
                // If the point is in an odd number of polygons, it is inside. If even, it is a hole.
                returnValue ^= PolygonContainsPoint(poly.Points, pX, pY);

                // Any point on any boundary is on a boundary.
                if (returnValue == Inclusion.Boundary)
                    return Inclusion.Boundary;
            }

            return returnValue;
        }

        /// <summary>
        /// Determines whether the specified point is contained within the region defined by this <see cref="Circle"/>.
        /// </summary>
        /// <param name="x">Center x-coordinate.</param>
        /// <param name="y">Center y-coordinate.</param>
        /// <param name="r">Radius of circle.</param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/481144/equation-for-testing-if-a-point-is-inside-a-circle
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion CircleContainsPoint(double x, double y, double r, double pX, double pY)
        {
            // Check if it is within the bounding rectangle.
            if (pX >= x - r && pX <= x + r
                && pY >= y - r && pY <= y + r)
            {
                double dx = x - pX;
                double dy = y - pY;
                dx *= dx;
                dy *= dy;
                double distanceSquared = dx + dy;
                double radiusSquared = r * r;
                return (radiusSquared >= distanceSquared) ? ((Abs(radiusSquared - distanceSquared) < Epsilon) ? Inclusion.Boundary : Inclusion.Inside) : Inclusion.Outside;
            }

            return Inclusion.Outside;
        }

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="Ellipse"/>.
        /// </summary>
        /// <param name="x">Center x-coordinate.</param>
        /// <param name="y">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <returns></returns>
        /// <remarks>
        /// Based off of: http://stackoverflow.com/questions/7946187/point-and-ellipse-rotated-position-test-algorithm
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion EllipseContainsPoint(double x, double y, double r1, double r2, double angle, double pX, double pY)
        {
            if (r1 <= 0d || r2 <= 0d)
                return Inclusion.Outside;

            // Get the ellipse rotation transform.
            double cosT = Cos(angle);
            double sinT = Sin(angle);

            // Translate points to origin.
            double u = pX - x;
            double v = (pY - y);

            // Apply the rotation transformation.
            double a = (u * cosT + v * sinT);
            double b = (u * sinT - v * cosT);

            double normalizedRadius = ((a * a) / (r1 * r1))
                                    + ((b * b) / (r2 * r2));

            return (normalizedRadius <= 1d)
                ? ((Abs(normalizedRadius - 1d) < Epsilon)
                ? Inclusion.Boundary : Inclusion.Inside) : Inclusion.Outside;
        }

        /// <summary>
        /// Determines whether the specified point is contained within the region defined by this <see cref="Circle"/>.
        /// </summary>
        /// <param name="x">Center x-coordinate.</param>
        /// <param name="y">Center y-coordinate.</param>
        /// <param name="r">Radius of circle.</param>
        /// <param name="startAngle">The angle to start the arc.</param>
        /// <param name="sweepAngle">The difference of the angle to where the arc should end.</param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/481144/equation-for-testing-if-a-point-is-inside-a-circle
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion CircularArcSectorContainsPoint(double x, double y, double r, double startAngle, double sweepAngle, double pX, double pY)
        {
            if (r <= 0d)
                return Inclusion.Outside;

            // Check if it is within the bounding rectangle.
            if (pX >= x - r && pX <= x + r
                && pY >= y - r && pY <= y + r)
            {
                // Find the points of the chord.
                Point2D startPoint = Interpolaters.CircularArc(x, y, r, startAngle, sweepAngle, 0);
                Point2D endPoint = Interpolaters.CircularArc(x, y, r, startAngle, sweepAngle, 1);

                // Find the determinant of the chord and point.
                double determinant = (startPoint.X - pX) * (endPoint.Y - pY) - (endPoint.X - pX) * (startPoint.Y - pY);

                // Check if the point is on the chord.
                if (Abs(determinant) < Epsilon)
                    return Inclusion.Boundary;
                // Check whether the point is on the same side of the chord as the center.
                else if (Sign(determinant) == Sign(sweepAngle))
                    return Inclusion.Outside;

                double dx = x - pX;
                double dy = y - pY;
                dx *= dx;
                dy *= dy;
                double distanceSquared = dx + dy;
                double radiusSquared = r * r;
                return (radiusSquared >= distanceSquared) ? ((Abs(radiusSquared - distanceSquared) < Epsilon) ? Inclusion.Boundary : Inclusion.Inside) : Inclusion.Outside;
            }

            return Inclusion.Outside;
        }

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="EllipticalArc"/>.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <returns></returns>
        /// <remarks>
        /// Based off of: http://stackoverflow.com/questions/7946187/point-and-ellipse-rotated-position-test-algorithm
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion EllipticalArcContainsPoint(double cX, double cY, double r1, double r2, double angle, double startAngle, double sweepAngle, double pX, double pY)
        {
            if (r1 <= 0d || r2 <= 0d)
                return Inclusion.Outside;

            // Find the start and end angles.
            double sa = EllipsePolarAngle(startAngle, r1, r2);
            double ea = EllipsePolarAngle(startAngle + sweepAngle, r1, r2);

            // Get the ellipse rotation transform.
            double cosT = Cos(angle);
            double sinT = Sin(angle);

            // Ellipse equation for an ellipse at origin for the chord end points.
            double u1 = r1 * Cos(sa);
            double v1 = -(r2 * Sin(sa));
            double u2 = r1 * Cos(ea);
            double v2 = -(r2 * Sin(ea));

            // Find the points of the chord.
            double sX = cX + (u1 * cosT + v1 * sinT);
            double sY = cY + (u1 * sinT - v1 * cosT);
            double eX = cX + (u2 * cosT + v2 * sinT);
            double eY = cY + (u2 * sinT - v2 * cosT);

            // Find the determinant of the chord.
            double determinant = (sX - pX) * (eY - pY) - (eX - pX) * (sY - pY);

            //// Check if the point is on the chord.
            //if (Abs(determinant) <= Epsilon)
            //{
            //    return (sX < eX) ?
            //    (sX <= pX && pX <= eX) ? Inclusion.Boundary : Inclusion.Outside :
            //    (eX <= pX && pX <= sX) ? Inclusion.Boundary : Inclusion.Outside;
            //}

            // Check whether the point is on the side of the chord as the center.
            if (Sign(determinant) == Sign(sweepAngle))
                return Inclusion.Outside;

            // Translate points to origin.
            double u0 = pX - cX;
            double v0 = pY - cY;

            // Apply the rotation transformation.
            double a = u0 * cosT + v0 * sinT;
            double b = u0 * sinT - v0 * cosT;

            double normalizedRadius
                = ((a * a) / (r1 * r1))
                + ((b * b) / (r2 * r2));

            return (normalizedRadius <= 1d)
                ? ((Abs(normalizedRadius - 1d) < Epsilon)
                ? Inclusion.Boundary : Inclusion.Inside) : Inclusion.Outside;
        }

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="EllipticalArc"/>.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <returns></returns>
        /// <remarks>
        /// Based off of: http://stackoverflow.com/questions/7946187/point-and-ellipse-rotated-position-test-algorithm
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion EllipticalArcSectorContainsPoint(double cX, double cY, double r1, double r2, double angle, double startAngle, double sweepAngle, double pX, double pY)
        {
            if (r1 <= 0d || r2 <= 0d)
                return Inclusion.Outside;

            // Find the start and end angles.
            double sa = EllipsePolarAngle(startAngle, r1, r2);
            double ea = EllipsePolarAngle(startAngle + sweepAngle, r1, r2);

            // Get the ellipse rotation transform.
            double cosT = Cos(angle);
            double sinT = Sin(angle);

            // Ellipse equation for an ellipse at origin for the chord end points.
            double u1 = r1 * Cos(sa);
            double v1 = -(r2 * Sin(sa));
            double u2 = r1 * Cos(ea);
            double v2 = -(r2 * Sin(ea));

            // Find the points of the chord.
            double sX = cX + (u1 * cosT + v1 * sinT);
            double sY = cY + (u1 * sinT - v1 * cosT);
            double eX = cX + (u2 * cosT + v2 * sinT);
            double eY = cY + (u2 * sinT - v2 * cosT);

            // Find the determinant of the chord.
            double determinant = (sX - pX) * (eY - pY) - (eX - pX) * (sY - pY);

            // Check if the point is on the chord.
            if (Abs(determinant) <= Epsilon)
            {
                return (sX < eX) ?
                (sX <= pX && pX <= eX) ? Inclusion.Boundary : Inclusion.Outside :
                (eX <= pX && pX <= sX) ? Inclusion.Boundary : Inclusion.Outside;
            }

            // Check whether the point is on the side of the chord as the center.
            if (Sign(determinant) == Sign(sweepAngle))
                return Inclusion.Outside;

            // Translate points to origin.
            double u0 = pX - cX;
            double v0 = pY - cY;

            // Apply the rotation transformation.
            double a = u0 * cosT + v0 * sinT;
            double b = u0 * sinT - v0 * cosT;

            double normalizedRadius
                = ((a * a) / (r1 * r1))
                + ((b * b) / (r2 * r2));

            return (normalizedRadius <= 1d)
                ? ((Abs(normalizedRadius - 1d) < Epsilon)
                ? Inclusion.Boundary : Inclusion.Inside) : Inclusion.Outside;
        }

        /// <summary>
        /// This function should be called with the full set of *all* relevant polygons.
        /// (The algorithm automatically knows that enclosed polygons are “no-go” areas.)
        /// Note:  As much as possible, this algorithm tries to return YES when the
        /// test line-segment is exactly on the border of the polygon, particularly
        /// if the test line-segment *is* a side of a polygon.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="polygons"></param>
        /// <returns></returns>
        /// <remarks>
        /// Public-domain code by Darel Rex Finley, 2006.
        /// http://alienryderflex.com/shortest_path/
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion PolygonSetContainsPoints(this Polygon polygons, Point2D start, Point2D end)
        {
            int j;
            double sX;
            double sY;
            double eX;
            double eY;
            double rotSX;
            double rotSY;
            double rotEX;
            double rotEY;
            double crossX;

            end.X -= start.X;
            end.Y -= start.Y;
            double dist = Sqrt(end.X * end.X + end.Y * end.Y);
            double theCos = end.X / dist;
            double theSin = end.Y / dist;

            foreach (Contour poly in polygons.Contours)
            {
                for (int i = 0; i < poly.Points.Count; i++)
                {
                    j = i + 1;
                    if (j == poly.Points.Count)
                        j = 0;

                    sX = poly.Points[i].X - start.X;
                    sY = poly.Points[i].Y - start.Y;
                    eX = poly.Points[j].X - start.X;
                    eY = poly.Points[j].Y - start.Y;

                    if (Abs(sX) < Epsilon && Abs(sY) < Epsilon
                        && Abs(eX - end.X) < Epsilon && Abs(eY - end.Y) < Epsilon
                        || Abs(eX) < Epsilon
                        && Abs(eY) < Epsilon && Abs(sX - end.X) < Epsilon
                        && Abs(sY - end.Y) < Epsilon)
                    {
                        return Inclusion.Inside;
                    }

                    rotSX = sX * theCos + sY * theSin;
                    rotSY = sY * theCos - sX * theSin;
                    rotEX = eX * theCos + eY * theSin;
                    rotEY = eY * theCos - eX * theSin;

                    if (rotSY < 0.0 && rotEY > 0.0
                    || rotEY < 0.0 && rotSY > 0.0)
                    {
                        crossX = rotSX + (rotEX - rotSX) * (0.0 - rotSY) / (rotEY - rotSY);
                        if (crossX >= 0.0 && crossX <= dist)
                            return Inclusion.Outside;
                    }

                    if (Abs(rotSY) < Epsilon
                        && Abs(rotEY) < Epsilon
                        && (rotSX >= 0.0 || rotEX >= 0.0)
                        && (rotSX <= dist || rotEX <= dist)
                        && (rotSX < 0.0 || rotEX < 0.0
                        || rotSX > dist || rotEX > dist))
                    {
                        return Inclusion.Outside;
                    }
                }
            }

            return PolygonSetContainsPoint(polygons.Contours, start.X + end.X / 2.0, start.Y + end.Y / 2.0);
        }

        /// <summary>
        /// Determines if the rectangular region is entirely contained within the <see cref="Rectangle2D"/> region represented by another <see cref="Rectangle2D"/>.
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="aWidth"></param>
        /// <param name="aHeight"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="bWidth"></param>
        /// <param name="bHeight"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RectangleContainsRectangle(
            double aX, double aY,
            double aWidth, double aHeight,
            double bX, double bY,
            double bWidth, double bHeight)
            => (aX <= bX)
            && ((bX + bWidth) <= (aX + aWidth))
            && (aY <= bY)
            && ((bY + bHeight) <= (aY + aHeight));

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
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="height0"></param>
        /// <param name="width0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="height1"></param>
        /// <param name="width1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RectangleRectangleIntersects(
            double x0, double y0,
            double width0, double height0,
            double x1, double y1,
            double width1, double height1)
            => (x1 < x0 + width0)
            && (x0 < (x1 + width1))
            && (y1 < y0 + height0)
            && (y0 < y1 + height1);

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
            => PointPointIntersects(point0X, point0Y, point1X, point1Y)
            ? new Intersection(IntersectionState.Intersection, new Point2D(point0X, point0Y))
            : new Intersection(IntersectionState.NoIntersection);

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
            => (PointLineSegmentIntersects(pointX, pointY, segmentAX, segmentAY, segmentBX, segmentBY))
            ? new Intersection(IntersectionState.Intersection, new Point2D(pointX, pointY))
            : new Intersection(IntersectionState.NoIntersection);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <param name="b1X"></param>
        /// <param name="b1Y"></param>
        /// <param name="b2X"></param>
        /// <param name="b2Y"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection RayRayIntersection(
            double a1X, double a1Y,
            double a2X, double a2Y,
            double b1X, double b1Y,
            double b2X, double b2Y)
        {
            Intersection result;
            var ua_t = (b2X - b1X) * (a1Y - b1Y) - (b2Y - b1Y) * (a1X - b1X);
            var ub_t = (a2X - a1X) * (a1Y - b1Y) - (a2Y - a1Y) * (a1X - b1X);
            var u_b = (b2Y - b1Y) * (a2X - a1X) - (b2X - b1X) * (a2Y - a1Y);
            if (u_b != 0)
            {
                var ua = ua_t / u_b;
                result = new Intersection(IntersectionState.Intersection);
                result.AppendPoint(new Point2D(a1X + ua * (a2X - a1X), a1Y + ua * (a2Y - a1Y)));
            }
            else
            {
                if (ua_t == 0 || ub_t == 0)
                {
                    result = new Intersection(IntersectionState.Coincident);
                }
                else
                {
                    result = new Intersection(IntersectionState.Parallel);
                }
            }

            return result;
        }

        /// <summary>
        /// Find the intersection point between two line segments.
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
        public static Intersection LineSegmentLineSegmentIntersection2(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
        {
            Intersection result = new Intersection(IntersectionState.NoIntersection);

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
                result = new Intersection(IntersectionState.Intersection);
                result.AppendPoint(new Point2D(x0 + t * u1, y0 + t * v1));
            }

            return result;
        }

        /// <summary>
        /// Find the intersection point between two line segments.
        /// </summary>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <param name="b1X"></param>
        /// <param name="b1Y"></param>
        /// <param name="b2X"></param>
        /// <param name="b2Y"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentLineSegmentIntersection(
            double a1X, double a1Y,
            double a2X, double a2Y,
            double b1X, double b1Y,
            double b2X, double b2Y)
        {
            Intersection result;
            var ua_t = (b2X - b1X) * (a1Y - b1Y) - (b2Y - b1Y) * (a1X - b1X);
            var ub_t = (a2X - a1X) * (a1Y - b1Y) - (a2Y - a1Y) * (a1X - b1X);
            var u_b = (b2Y - b1Y) * (a2X - a1X) - (b2X - b1X) * (a2Y - a1Y);
            if (u_b != 0)
            {
                var ua = ua_t / u_b;
                var ub = ub_t / u_b;
                if (0 <= ua && ua <= 1 && 0 <= ub && ub <= 1)
                {
                    result = new Intersection(IntersectionState.Intersection);
                    result.AppendPoint(new Point2D(a1X + ua * (a2X - a1X), a1Y + ua * (a2Y - a1Y)));
                }
                else
                {
                    result = new Intersection(IntersectionState.NoIntersection);
                }
            }
            else
            {
                if (ua_t == 0 || ub_t == 0)
                {
                    result = new Intersection(IntersectionState.Coincident);
                }
                else
                {
                    result = new Intersection(IntersectionState.Parallel);
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
            var result = new Intersection(IntersectionState.NoIntersection);

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
            result = new Intersection(IntersectionState.Intersection);
            result.AppendPoint(new Point2D(x0 + t * u1, y0 + t * v1));

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentRectangleIntersection(
            double a1X, double a1Y,
            double a2X, double a2Y,
            double r1X, double r1Y,
            double r2X, double r2Y)
        {
            var min = MinPoint(r1X, r1Y, r2X, r2Y);
            var max = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);
            var inter1 = LineSegmentLineSegmentIntersection(min.X, min.Y, topRight.X, topRight.Y, a1X, a1Y, a2X, a2Y);
            var inter2 = LineSegmentLineSegmentIntersection(topRight.X, topRight.Y, max.X, max.Y, a1X, a1Y, a2X, a2Y);
            var inter3 = LineSegmentLineSegmentIntersection(max.X, max.Y, bottomLeft.X, bottomLeft.Y, a1X, a1Y, a2X, a2Y);
            var inter4 = LineSegmentLineSegmentIntersection(bottomLeft.X, bottomLeft.Y, min.X, min.Y, a1X, a1Y, a2X, a2Y);
            var result = new Intersection(IntersectionState.NoIntersection);
            result.AppendPoints(inter1.Points);
            result.AppendPoints(inter2.Points);
            result.AppendPoints(inter3.Points);
            result.AppendPoints(inter4.Points);
            if (result.Points.Count > 0)
                result.State = IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <param name="b1X"></param>
        /// <param name="b1Y"></param>
        /// <param name="b2X"></param>
        /// <param name="b2Y"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection RectangleRectangleIntersection(
            double a1X, double a1Y,
            double a2X, double a2Y,
            double b1X, double b1Y,
            double b2X, double b2Y)
        {
            var min = MinPoint(a1X, a1Y, a2X, a2Y);
            var max = MaxPoint(a1X, a1Y, a2X, a2Y);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);
            var inter1 = LineSegmentRectangleIntersection(min.X, min.Y, topRight.X, topRight.Y, b1X, b1Y, b2X, b2Y);
            var inter2 = LineSegmentRectangleIntersection(topRight.X, topRight.Y, max.X, max.Y, b1X, b1Y, b2X, b2Y);
            var inter3 = LineSegmentRectangleIntersection(max.X, max.Y, bottomLeft.X, bottomLeft.Y, b1X, b1Y, b2X, b2Y);
            var inter4 = LineSegmentRectangleIntersection(bottomLeft.X, bottomLeft.Y, min.X, min.Y, b1X, b1Y, b2X, b2Y);
            var result = new Intersection(IntersectionState.NoIntersection);
            result.AppendPoints(inter1.Points);
            result.AppendPoints(inter2.Points);
            result.AppendPoints(inter3.Points);
            result.AppendPoints(inter4.Points);
            if (result.Points.Count > 0)
                result.State = IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentPolygonIntersection(
            double a1X, double a1Y,
            double a2X, double a2Y,
            List<Point2D> points)
        {
            var result = new Intersection(IntersectionState.NoIntersection);
            var length = points.Count;
            for (var i = 0; i < length; i++)
            {
                var b1 = points[i];
                var b2 = points[(i + 1) % length];
                var inter = LineSegmentLineSegmentIntersection(a1X, a1Y, a2X, a2Y, b1.X, b1.Y, b2.X, b2.Y);
                result.AppendPoints(inter.Points);
            }
            if (result.Points.Count > 0)
                result.State = IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection PolygonRectangleIntersection(
            List<Point2D> points,
            double r1X, double r1Y,
            double r2X, double r2Y)
        {
            var min = MinPoint(r1X, r1Y, r2X, r2Y);
            var max = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);
            var inter1 = LineSegmentPolygonIntersection(min.X, min.Y, topRight.X, topRight.Y, points);
            var inter2 = LineSegmentPolygonIntersection(topRight.X, topRight.Y, max.X, max.Y, points);
            var inter3 = LineSegmentPolygonIntersection(max.X, max.Y, bottomLeft.X, bottomLeft.Y, points);
            var inter4 = LineSegmentPolygonIntersection(bottomLeft.X, bottomLeft.Y, min.X, min.Y, points);
            var result = new Intersection(IntersectionState.NoIntersection);
            result.AppendPoints(inter1.Points);
            result.AppendPoints(inter2.Points);
            result.AppendPoints(inter3.Points);
            result.AppendPoints(inter4.Points);
            if (result.Points.Count > 0)
                result.State = IntersectionState.Intersection;
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
            var result = new Intersection(IntersectionState.NoIntersection);
            var length = points1.Count;
            for (var i = 0; i < length; i++)
            {
                var a1 = points1[i];
                var a2 = points1[(i + 1) % length];
                var inter = LineSegmentPolygonIntersection(a1.X, a1.Y, a2.X, a2.Y, points2);
                result.AppendPoints(inter.Points);
            }
            if (result.Points.Count > 0)
                result.State = IntersectionState.Intersection;
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
        public static Intersection CircleLineSegmentIntersection1(
            double cX, double cY,
            double radius,
            double x1, double y1,
            double x2, double y2)
        {
            var result = new Intersection(IntersectionState.NoIntersection);

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
                    result = new Intersection(IntersectionState.Intersection);
                    result.AppendPoint(new Point2D(x1 + t * dx, y1 + t * dy));
                }
            }
            else if (discriminant > 0)
            {
                // Two possible solutions.
                double t1 = ((-b + Sqrt(discriminant)) / (2 * a));
                double t2 = ((-b - Sqrt(discriminant)) / (2 * a));

                // Add the points if they are between the end points of the line segment.
                result = new Intersection(IntersectionState.Intersection);
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
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CircleLineSegmentIntersection(
            double cX, double cY,
            double r,
            double a1X, double a1Y,
            double a2X, double a2Y)
        {
            Intersection result;
            var a = (a2X - a1X) * (a2X - a1X) + (a2Y - a1Y) * (a2Y - a1Y);
            var b = 2 * ((a2X - a1X) * (a1X - cX) + (a2Y - a1Y) * (a1Y - cY));
            var cc = cX * cX + cY * cY + a1X * a1X + a1Y * a1Y - 2 * (cX * a1X + cY * a1Y) - r * r;
            var deter = b * b - 4 * a * cc;
            if (deter < 0)
            {
                result = new Intersection(IntersectionState.Outside);
            }
            else if (deter == 0)
            {
                result = new Intersection(IntersectionState.Tangent);
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
                        result = new Intersection(IntersectionState.Outside);
                    }
                    else
                    {
                        result = new Intersection(IntersectionState.Inside);
                    }
                }
                else
                {
                    result = new Intersection(IntersectionState.Intersection);
                    if (0 <= u1 && u1 <= 1)
                        result.Points.Add(Lerp(a1X, a1Y, a2X, a2Y, u1));
                    if (0 <= u2 && u2 <= 1)
                        result.Points.Add(Lerp(a1X, a1Y, a2X, a2Y, u2));
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection CircleRectangleIntersection(
            double cX, double cY,
            double r,
            double r1X, double r1Y,
            double r2X, double r2Y)
        {
            var min = MinPoint(r1X, r1Y, r2X, r2Y);
            var max = MinPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);
            var inter1 = CircleLineSegmentIntersection(cX, cY, r, min.X, min.Y, topRight.X, topRight.Y);
            var inter2 = CircleLineSegmentIntersection(cX, cY, r, topRight.X, topRight.Y, max.X, max.Y);
            var inter3 = CircleLineSegmentIntersection(cX, cY, r, max.X, max.Y, bottomLeft.X, bottomLeft.Y);
            var inter4 = CircleLineSegmentIntersection(cX, cY, r, bottomLeft.X, bottomLeft.Y, min.X, min.Y);
            var result = new Intersection(IntersectionState.NoIntersection);
            result.AppendPoints(inter1.Points);
            result.AppendPoints(inter2.Points);
            result.AppendPoints(inter3.Points);
            result.AppendPoints(inter4.Points);
            if (result.Points.Count > 0)
                result.State = IntersectionState.Intersection;
            else result.State = inter1.State;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection CirclePolygonIntersection(
            double cX, double cY,
            double r,
            List<Point2D> points)
        {
            var result = new Intersection(IntersectionState.NoIntersection);
            var length = points.Count;
            Intersection inter = new Intersection(IntersectionState.NoIntersection);
            for (var i = 0; i < length; i++)
            {
                var a1 = points[i];
                var a2 = points[(i + 1) % length];
                inter = CircleLineSegmentIntersection(cX, cY, r, a1.X, a1.Y, a2.X, a2.Y);
                result.AppendPoints(inter.Points);
            }
            if (result.Points.Count > 0)
                result.State = IntersectionState.Intersection;
            else result.State = inter.State;
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
        public static Intersection CircleCircleIntersection1(
            double cx0,
            double cy0,
            double radius0,
            double cx1,
            double cy1,
            double radius1)
        {
            var result = new Intersection(IntersectionState.NoIntersection);

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
                    result = new Intersection(IntersectionState.Intersection);
                    result.AppendPoint(new Point2D(
                        (cx2 + h * (cy1 - cy0) / dist),
                        (cy2 - h * (cx1 - cx0) / dist)));
                }
                else
                {
                    // Get the points P3.
                    result = new Intersection(IntersectionState.Intersection);
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
        /// <param name="c1X"></param>
        /// <param name="c1Y"></param>
        /// <param name="r1"></param>
        /// <param name="c2X"></param>
        /// <param name="c2Y"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CircleCircleIntersection(
            double c1X, double c1Y,
            double r1,
            double c2X, double c2Y,
            double r2)
        {
            Intersection result;
            var r_max = r1 + r2;
            var r_min = Abs(r1 - r2);
            var c_dist = Measurements.Distance(c1X, c1Y, c2X, c2Y);
            if (c_dist > r_max)
            {
                result = new Intersection(IntersectionState.Outside);
            }
            else if (c_dist < r_min)
            {
                result = new Intersection(IntersectionState.Inside);
            }
            else
            {
                result = new Intersection(IntersectionState.Intersection);
                var a = (r1 * r1 - r2 * r2 + c_dist * c_dist) / (2 * c_dist);
                var h = Sqrt(r1 * r1 - a * a);
                var p = Lerp(c1X, c1Y, c2X, c2Y, a / c_dist);
                var b = h / c_dist;
                result.AppendPoint(new Point2D(p.X - b * (c2Y - c1Y), p.Y + b * (c2X - c1X)));
                result.AppendPoint(new Point2D(p.X + b * (c2Y - c1Y), p.Y - b * (c2X - c1X)));
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ccX"></param>
        /// <param name="ccY"></param>
        /// <param name="r"></param>
        /// <param name="ecX"></param>
        /// <param name="ecY"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection CircleUnrotatedEllipseIntersection(
            double ccX, double ccY,
            double r,
            double ecX, double ecY,
            double rx,
            double ry)
            => UnrotatedEllipseUnrotatedEllipseIntersection(ccX, ccY, r, r, ecX, ecY, rx, ry);

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
        public static Intersection UnrotatedEllipseLineSegmentIntersection2(
            double cx, double cy,
            double rx, double ry,
            double x0, double y0,
            double x1, double y1)
        {
            var result = new Intersection(IntersectionState.NoIntersection);

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
                    result.State = IntersectionState.Intersection;
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
                    result.State = IntersectionState.Intersection;
                }

                if ((t2 >= 0d) && (t2 <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + (u2 - u1) * t2 + cx, v1 + (v2 - v1) * t2 + cy));
                    result.State = IntersectionState.Intersection;
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection UnrotatedEllipseLineSegmentIntersection(
            double centerX, double centerY,
            double rx,
            double ry,
            double a1X, double a1Y,
            double a2X, double a2Y
            )
        {
            Intersection result;
            var origin = new Vector2D(a1X, a1Y);
            var dir = new Vector2D(a1X, a1Y, a2X, a2Y);
            var diff = origin.Subtract(centerX, centerY);
            var mDir = new Vector2D(dir.I / (rx * rx), dir.J / (ry * ry));
            var mDiff = new Vector2D(diff.I / (rx * rx), diff.J / (ry * ry));
            var a = dir.DotProduct(mDir);
            var b = dir.DotProduct(mDiff);
            var c = diff.DotProduct(mDiff) - 1.0;
            var d = b * b - a * c;
            if (d < 0)
            {
                result = new Intersection(IntersectionState.Outside);
            }
            else if (d > 0)
            {
                var root = Sqrt(d);
                var t_a = (-b - root) / a;
                var t_b = (-b + root) / a;
                if ((t_a < 0 || 1 < t_a) && (t_b < 0 || 1 < t_b))
                {
                    if ((t_a < 0 && t_b < 0) || (t_a > 1 && t_b > 1))
                        result = new Intersection(IntersectionState.Outside);
                    else result = new Intersection(IntersectionState.Inside);
                }
                else
                {
                    result = new Intersection(IntersectionState.Intersection);
                    if (0 <= t_a && t_a <= 1) result.AppendPoint(Lerp(a1X, a1Y, a2X, a2Y, t_a));
                    if (0 <= t_b && t_b <= 1) result.AppendPoint(Lerp(a1X, a1Y, a2X, a2Y, t_b));
                }
            }
            else
            {
                var t = -b / a; if (0 <= t && t <= 1)
                {
                    result = new Intersection(IntersectionState.Intersection);
                    result.AppendPoint(Lerp(a1X, a1Y, a2X, a2Y, t));
                }
                else
                {
                    result = new Intersection(IntersectionState.Outside);
                }
            }

            return result;
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
            var result = new Intersection(IntersectionState.NoIntersection);

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
                    result.State = IntersectionState.Intersection;
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
                    result.State = IntersectionState.Intersection;
                }

                if ((t2 >= 0d) && (t2 <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + (u2 - u1) * t2 + cx, v1 + (v2 - v1) * t2 + cy));
                    result.State = IntersectionState.Intersection;
                }

                // ToDo: Figure out why the results are weird between 30 degrees and 5 degrees.
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection UnrotatedEllipseRectangleIntersection(
            double cX, double cY,
            double rx,
            double ry,
            double r1X, double r1Y,
            double r2X, double r2Y)
        {
            var min = MinPoint(r1X, r1Y, r2X, r2Y);
            var max = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);
            var inter1 = UnrotatedEllipseLineSegmentIntersection(cX, cY, rx, ry, min.X, min.Y, topRight.X, topRight.Y);
            var inter2 = UnrotatedEllipseLineSegmentIntersection(cX, cY, rx, ry, topRight.X, topRight.Y, max.X, max.Y);
            var inter3 = UnrotatedEllipseLineSegmentIntersection(cX, cY, rx, ry, max.X, max.Y, bottomLeft.X, bottomLeft.Y);
            var inter4 = UnrotatedEllipseLineSegmentIntersection(cX, cY, rx, ry, bottomLeft.X, bottomLeft.Y, min.X, min.Y);
            var result = new Intersection(IntersectionState.NoIntersection);
            result.AppendPoints(inter1.Points);
            result.AppendPoints(inter2.Points);
            result.AppendPoints(inter3.Points);
            result.AppendPoints(inter4.Points);
            if (result.Points.Count > 0)
                result.State = IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection UnrotatedEllipsePolygonIntersection(
            double cX, double cY,
            double rx,
            double ry,
            List<Point2D> points)
        {
            var result = new Intersection(IntersectionState.NoIntersection);
            var length = points.Count;
            for (var i = 0; i < length; i++)
            {
                var b1 = points[i];
                var b2 = points[(i + 1) % length];
                var inter = UnrotatedEllipseLineSegmentIntersection(cX, cY, rx, ry, b1.X, b1.Y, b2.X, b2.Y);
                result.AppendPoints(inter.Points);
            }
            if (result.Points.Count > 0)
                result.State = IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c1X"></param>
        /// <param name="c1Y"></param>
        /// <param name="rx1"></param>
        /// <param name="ry1"></param>
        /// <param name="c2X"></param>
        /// <param name="c2Y"></param>
        /// <param name="rx2"></param>
        /// <param name="ry2"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection UnrotatedEllipseUnrotatedEllipseIntersection(
            double c1X, double c1Y,
            double rx1,
            double ry1,
            double c2X, double c2Y,
            double rx2,
            double ry2)
        {
            double[] a = new double[] { ry1 * ry1, 0, rx1 * rx1, -2 * ry1 * ry1 * c1X, -2 * rx1 * rx1 * c1Y, ry1 * ry1 * c1X * c1X + rx1 * rx1 * c1Y * c1Y - rx1 * rx1 * ry1 * ry1 };
            double[] b = new double[] { ry2 * ry2, 0, rx2 * rx2, -2 * ry2 * ry2 * c2X, -2 * rx2 * rx2 * c2Y, ry2 * ry2 * c2X * c2X + rx2 * rx2 * c2Y * c2Y - rx2 * rx2 * ry2 * ry2 };
            var yPoly = Bezout(a, b);
            var yRoots = yPoly.Roots();
            var epsilon = 1e-3;
            var norm0 = (a[0] * a[0] + 2 * a[1] * a[1] + a[2] * a[2]) * epsilon;
            var norm1 = (b[0] * b[0] + 2 * b[1] * b[1] + b[2] * b[2]) * epsilon;
            var result = new Intersection(IntersectionState.NoIntersection);
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
                result.State = IntersectionState.Intersection; return result;
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
        /// Found at: https://www.particleincell.com/2013/cubic-line-intersection/
        /// Based on code now found at: http://www.abecedarical.com/javascript/script_cubic.html
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierLineSegmentIntersection1(
            double p0x, double p0y,
            double p1x, double p1y,
            double p2x, double p2y,
            double p3x, double p3y,
            double l0x, double l0y,
            double l1x, double l1y)
        {
            // ToDo: Figure out why this can't handle intersection with horizontal lines.
            var I = new Intersection(IntersectionState.NoIntersection);

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
                    I.State = IntersectionState.Intersection;
                }
            }
            return I;
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
        /// <param name="p4X"></param>
        /// <param name="p4Y"></param>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection CubicBezierLineSegmentIntersection(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            double p4X, double p4Y,
            double a1X, double a1Y,
            double a2X, double a2Y)
        {
            Vector2D a, b, c, d;
            Vector2D c3, c2, c1, c0;
            double cl;
            Vector2D n;
            var min = MinPoint(a1X, a1Y, a2X, a2Y);
            var max = MaxPoint(a1X, a1Y, a2X, a2Y);
            var result = new Intersection(IntersectionState.NoIntersection);
            a = new Vector2D(p1X, p1Y).Scale(-1);
            b = new Vector2D(p2X, p2Y).Scale(3);
            c = new Vector2D(p3X, p3Y).Scale(-3);
            d = a.Add(b.Add(c.Add((p4X, p4Y))));
            c3 = new Vector2D(d.I, d.J);
            a = new Vector2D(p1X, p1Y).Scale(3);
            b = new Vector2D(p2X, p2Y).Scale(-6);
            c = new Vector2D(p3X, p3Y).Scale(3);
            d = a.Add(b.Add(c));
            c2 = new Vector2D(d.I, d.J);
            a = new Vector2D(p1X, p1Y).Scale(-3);
            b = new Vector2D(p2X, p2Y).Scale(3);
            c = a.Add(b);
            c1 = new Vector2D(c.I, c.J);
            c0 = new Vector2D(p1X, p1Y);
            n = new Vector2D(a1Y - a2Y, a2X - a1X);
            cl = a1X * a2Y - a2X * a1Y;
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
                    var p5 = Lerp(p1X, p1Y, p2X, p2Y, t);
                    var p6 = Lerp(p2X, p2Y, p3X, p3Y, t);
                    var p7 = Lerp(p3X, p3Y, p4X, p4Y, t);
                    var p8 = Lerp(p5.X, p5.Y, p6.X, p6.Y, t);
                    var p9 = Lerp(p6.X, p6.Y, p7.X, p7.Y, t);
                    var p10 = Lerp(p8.X, p8.Y, p9.X, p9.Y, t);
                    if (a1X == a2X)
                    {
                        if (min.Y <= p10.Y && p10.Y <= max.Y)
                        {
                            result.State = IntersectionState.Intersection;
                            result.AppendPoint(p10);
                        }
                    }
                    else if (a1Y == a2Y)
                    {
                        if (min.X <= p10.X && p10.X <= max.X)
                        {
                            result.State = IntersectionState.Intersection;
                            result.AppendPoint(p10);
                        }
                    }
                    else if (p10.GreaterThanOrEqual(min) && p10.LessThanOrEqual(max))
                    {
                        result.State = IntersectionState.Intersection;
                        result.AppendPoint(p10);
                    }
                }
            }
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
        /// <param name="p4X"></param>
        /// <param name="p4Y"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection CubicBezierRectangleIntersection(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            double p4X, double p4Y,
            double r1X, double r1Y,
            double r2X, double r2Y)
        {
            var min = MinPoint(r1X, r1Y, r2X, r2Y);
            var max = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);
            var inter1 = CubicBezierLineSegmentIntersection(p1X, p1Y, p2X, p2Y, p3X, p3Y, p4X, p4Y, min.X, min.Y, topRight.X, topRight.Y);
            var inter2 = CubicBezierLineSegmentIntersection(p1X, p1Y, p2X, p2Y, p3X, p3Y, p4X, p4Y, topRight.X, topRight.Y, max.X, max.Y);
            var inter3 = CubicBezierLineSegmentIntersection(p1X, p1Y, p2X, p2Y, p3X, p3Y, p4X, p4Y, max.X, max.Y, bottomLeft.X, bottomLeft.Y);
            var inter4 = CubicBezierLineSegmentIntersection(p1X, p1Y, p2X, p2Y, p3X, p3Y, p4X, p4Y, bottomLeft.X, bottomLeft.Y, min.X, min.Y);
            var result = new Intersection(IntersectionState.NoIntersection);
            result.AppendPoints(inter1.Points);
            result.AppendPoints(inter2.Points);
            result.AppendPoints(inter3.Points);
            result.AppendPoints(inter4.Points);
            if (result.Points.Count > 0)
                result.State = IntersectionState.Intersection;
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
        /// <param name="p4X"></param>
        /// <param name="p4Y"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection CubicBezierPolygonIntersection(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            double p4X, double p4Y,
            List<Point2D> points)
        {
            var result = new Intersection(IntersectionState.NoIntersection);
            var length = points.Count;
            for (var i = 0; i < length; i++)
            {
                var a1 = points[i];
                var a2 = points[(i + 1) % length];
                var inter = CubicBezierLineSegmentIntersection(p1X, p1Y, p2X, p2Y, p3X, p3Y, p4X, p4Y, a1.X, a1.Y, a2.X, a2.Y);
                result.AppendPoints(inter.Points);
            }
            if (result.Points.Count > 0)
                result.State = IntersectionState.Intersection;
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
        /// <param name="p4X"></param>
        /// <param name="p4Y"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection CubicBezierCircleIntersection(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            double p4X, double p4Y,
            double cX, double cY,
            double r)
            => CubicBezierUnrotatedEllipseIntersection(p1X, p1Y, p2X, p2Y, p3X, p3Y, p4X, p4Y, cX, cY, r, r);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1X"></param>
        /// <param name="p1Y"></param>
        /// <param name="p2X"></param>
        /// <param name="p2Y"></param>
        /// <param name="p3X"></param>
        /// <param name="p3Y"></param>
        /// <param name="p4X"></param>
        /// <param name="p4Y"></param>
        /// <param name="ecX"></param>
        /// <param name="ecY"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection CubicBezierUnrotatedEllipseIntersection(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            double p4X, double p4Y,
            double ecX, double ecY,
            double rx, double ry)
        {
            Vector2D a, b, c, d;
            Vector2D c3, c2, c1, c0;
            var result = new Intersection(IntersectionState.NoIntersection);
            a = new Vector2D(p1X, p1Y).Scale(-1);
            b = new Vector2D(p2X, p2Y).Scale(3);
            c = new Vector2D(p3X, p3Y).Scale(-3);
            d = a.Add(b.Add(c.Add((p4X, p4Y))));
            c3 = new Vector2D(d.I, d.J);
            a = new Vector2D(p1X, p1Y).Scale(3);
            b = new Vector2D(p2X, p2Y).Scale(-6);
            c = new Vector2D(p3X, p3Y).Scale(3);
            d = a.Add(b.Add(c));
            c2 = new Vector2D(d.I, d.J);
            a = new Vector2D(p1X, p1Y).Scale(-3);
            b = new Vector2D(p2X, p2Y).Scale(3);
            c = a.Add(b);
            c1 = new Vector2D(c.I, c.J);
            c0 = new Vector2D(p1X, p1Y);
            var rxrx = rx * rx;
            var ryry = ry * ry;
            var poly = new Polynomial(
                c0.I * c0.I * ryry - 2 * c0.J * ecY * rxrx - 2 * c0.I * ecX * ryry + c0.J * c0.J * rxrx + ecX * ecX * ryry + ecY * ecY * rxrx - rxrx * ryry,
                2 * c1.I * ryry * (c0.I - ecX) + 2 * c1.J * rxrx * (c0.J - ecY),
                2 * c2.I * ryry * (c0.I - ecX) + 2 * c2.J * rxrx * (c0.J - ecY) + c1.I * c1.I * ryry + c1.J * c1.J * rxrx,
                2 * c3.I * ryry * (c0.I - ecX) + 2 * c3.J * rxrx * (c0.J - ecY) + 2 * (c2.I * c1.I * ryry + c2.J * c1.J * rxrx),
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
                result.State = IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <param name="a3X"></param>
        /// <param name="a3Y"></param>
        /// <param name="a4X"></param>
        /// <param name="a4Y"></param>
        /// <param name="b1X"></param>
        /// <param name="b1Y"></param>
        /// <param name="b2X"></param>
        /// <param name="b2Y"></param>
        /// <param name="b3X"></param>
        /// <param name="b3Y"></param>
        /// <param name="b4X"></param>
        /// <param name="b4Y"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection CubicBezierCubicBezierIntersection(
            double a1X, double a1Y,
            double a2X, double a2Y,
            double a3X, double a3Y,
            double a4X, double a4Y,
            double b1X, double b1Y,
            double b2X, double b2Y,
            double b3X, double b3Y,
            double b4X, double b4Y,
            double epsilon = Epsilon)
        {
            Vector2D a, b, c, d;
            Vector2D c13, c12, c11, c10;
            Vector2D c23, c22, c21, c20;
            var result = new Intersection(IntersectionState.NoIntersection);
            a = new Vector2D(a1X, a1Y).Scale(-1);
            b = new Vector2D(a2X, a2Y).Scale(3);
            c = new Vector2D(a3X, a3Y).Scale(-3);
            d = a.Add(b.Add(c.Add((a4X, a4Y))));
            c13 = new Vector2D(d.I, d.J);
            a = new Vector2D(a1X, a1Y).Scale(3);
            b = new Vector2D(a2X, a2Y).Scale(-6);
            c = new Vector2D(a3X, a3Y).Scale(3);
            d = a.Add(b.Add(c));
            c12 = new Vector2D(d.I, d.J);
            a = new Vector2D(a1X, a1Y).Scale(-3);
            b = new Vector2D(a2X, a2Y).Scale(3);
            c = a.Add(b);
            c11 = new Vector2D(c.I, c.J);
            c10 = new Vector2D(a1X, a1Y);
            a = new Vector2D(b1X, b1Y).Scale(-1);
            b = new Vector2D(b2X, b2Y).Scale(3);
            c = new Vector2D(b3X, b3Y).Scale(-3);
            d = a.Add(b.Add(c.Add((b4X, b4Y))));
            c23 = new Vector2D(d.I, d.J);
            a = new Vector2D(b1X, b1Y).Scale(3);
            b = new Vector2D(b2X, b2Y).Scale(-6);
            c = new Vector2D(b3X, b3Y).Scale(3);
            d = a.Add(b.Add(c));
            c22 = new Vector2D(d.I, d.J);
            a = new Vector2D(b1X, b1Y).Scale(-3);
            b = new Vector2D(b2X, b2Y).Scale(3);
            c = a.Add(b);
            c21 = new Vector2D(c.I, c.J);
            c20 = new Vector2D(b1X, b1Y);
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
            if (result.Points.Count > 0) result.State = IntersectionState.Intersection;
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
        public static Intersection QuadraticBezierLineSegmentIntersection1(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            double a1X, double a1Y,
            double a2X, double a2Y)
        {
            var intersections = new Intersection(IntersectionState.NoIntersection);

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
                    var result = new Intersection(IntersectionState.Intersection);
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
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection QuadraticBezierLineSegmentIntersection(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            double a1X, double a1Y,
            double a2X, double a2Y)
        {
            var min = MinPoint(a1X, a1Y, a2X, a2Y);
            var max = MaxPoint(a1X, a1Y, a2X, a2Y);
            var result = new Intersection(IntersectionState.NoIntersection);
            var a = new Vector2D(p2X, p2Y).Scale(-2);
            var c2 = new Vector2D(p1X, p1Y).Add(a.Add((p3X, p3Y)));
            a = new Vector2D(p1X, p1Y).Scale(-2);
            var b = new Vector2D(p2X, p2Y).Scale(2);
            var c1 = a.Add(b);
            var c0 = new Point2D(p1X, p1Y);
            var n = new Vector2D(a1Y - a2Y, a2X - a1X);
            var cl = a1X * a2Y - a2X * a1Y;
            var roots = new Polynomial(
                n.DotProduct(c0) + cl,
                n.DotProduct(c1),
                n.DotProduct(c2)).Roots();
            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];
                if (0 <= t && t <= 1)
                {
                    Point2D p4 = Lerp(p1X, p1Y, p2X, p2Y, t);
                    Point2D p5 = Lerp(p2X, p2Y, p3X, p3Y, t);
                    Point2D p6 = Lerp(p4.X, p4.Y, p5.X, p5.Y, t);
                    if (a1X == a2X)
                    {
                        if (min.Y <= p6.Y && p6.Y <= max.Y)
                        {
                            result.State = IntersectionState.Intersection;
                            result.AppendPoint(p6);
                        }
                    }
                    else if (a1Y == a2Y)
                    {
                        if (min.X <= p6.X && p6.X <= max.X)
                        {
                            result.State = IntersectionState.Intersection;
                            result.AppendPoint(p6);
                        }
                    }
                    else if (p6.GreaterThanOrEqual(min) && p6.LessThanOrEqual(max))
                    {
                        result.State = IntersectionState.Intersection;
                        result.AppendPoint(p6);
                    }
                }
            }
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
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection QuadraticBezierRectangleIntersection(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            double r1X, double r1Y,
            double r2X, double r2Y)
        {
            var min = MinPoint(r1X, r1Y, r2X, r2Y);
            var max = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);
            var inter1 = QuadraticBezierLineSegmentIntersection(p1X, p1Y, p2X, p2Y, p3X, p3Y, min.X, min.Y, topRight.X, topRight.Y);
            var inter2 = QuadraticBezierLineSegmentIntersection(p1X, p1Y, p2X, p2Y, p3X, p3Y, topRight.X, topRight.Y, max.X, max.Y);
            var inter3 = QuadraticBezierLineSegmentIntersection(p1X, p1Y, p2X, p2Y, p3X, p3Y, max.X, max.Y, bottomLeft.X, bottomLeft.Y);
            var inter4 = QuadraticBezierLineSegmentIntersection(p1X, p1Y, p2X, p2Y, p3X, p3Y, bottomLeft.X, bottomLeft.Y, min.X, min.Y);
            var result = new Intersection(IntersectionState.NoIntersection);
            result.AppendPoints(inter1.Points);
            result.AppendPoints(inter2.Points);
            result.AppendPoints(inter3.Points);
            result.AppendPoints(inter4.Points);
            if (result.Points.Count > 0)
                result.State = IntersectionState.Intersection;
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
        /// <param name="points"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection QuadraticBezierPolygonIntersection(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            List<Point2D> points)
        {
            var result = new Intersection(IntersectionState.NoIntersection);
            var length = points.Count;
            for (var i = 0; i < length; i++)
            {
                var a1 = points[i];
                var a2 = points[(i + 1) % length];
                var inter = QuadraticBezierLineSegmentIntersection(p1X, p1Y, p2X, p2Y, p3X, p3Y, a1.X, a1.Y, a2.X, a2.Y);
                result.AppendPoints(inter.Points);
            }
            if (result.Points.Count > 0)
                result.State = IntersectionState.Intersection;
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
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection QuadraticBezierCircleIntersection(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            double cX, double cY,
            double r)
            => QuadraticBezierUnrotatedEllipseIntersection(p1X, p1Y, p2X, p2Y, p3X, p3Y, cX, cY, r, r);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1X"></param>
        /// <param name="p1Y"></param>
        /// <param name="p2X"></param>
        /// <param name="p2Y"></param>
        /// <param name="p3X"></param>
        /// <param name="p3Y"></param>
        /// <param name="ecX"></param>
        /// <param name="ecY"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection QuadraticBezierUnrotatedEllipseIntersection(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            double ecX, double ecY,
            double rx, double ry)
        {
            Vector2D a, b;
            Vector2D c2, c1, c0;
            var result = new Intersection(IntersectionState.NoIntersection);
            a = new Vector2D(p2X, p2Y).Scale(-2);
            c2 = new Vector2D(p1X, p1Y).Add(a.Add((p3X, p3Y)));
            a = new Vector2D(p1X, p1Y).Scale(-2);
            b = new Vector2D(p2X, p2Y).Scale(2);
            c1 = a.Add(b);
            c0 = new Point2D(p1X, p1Y);
            var rxrx = rx * rx;
            var ryry = ry * ry;
            var roots = new Polynomial(
                ryry * (c0.I * c0.I + ecY * ecY) + rxrx * (c0.J * c0.J + ecY * ecY) - 2 * (ryry * ecX * c0.I + rxrx * ecY * c0.J) - rxrx * ryry,
                2 * (ryry * c1.I * (c0.I - ecX) + rxrx * c1.J * (c0.J - ecY)),
                ryry * (2 * c2.I * c0.I + c1.I * c1.I) + rxrx * (2 * c2.J * c0.J + c1.J * c1.J) - 2 * (ryry * ecX * c2.I + rxrx * ecY * c2.J),
                2 * (ryry * c2.I * c1.I + rxrx * c2.J * c1.J),
                ryry * c2.I * c2.I + rxrx * c2.J * c2.J).Roots();
            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];
                if (0 <= t && t <= 1)
                    result.Points.Add((Point2D)c2.Scale(t * t).Add(c1.Scale(t).Add(c0)));
            }
            if (result.Points.Count > 0)
                result.State = IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <param name="a3X"></param>
        /// <param name="a3Y"></param>
        /// <param name="b1X"></param>
        /// <param name="b1Y"></param>
        /// <param name="b2X"></param>
        /// <param name="b2Y"></param>
        /// <param name="b3X"></param>
        /// <param name="b3Y"></param>
        /// <param name="b4X"></param>
        /// <param name="b4Y"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection QuadraticBezierCubicBezierIntersection(
            double a1X, double a1Y,
            double a2X, double a2Y,
            double a3X, double a3Y,
            double b1X, double b1Y,
            double b2X, double b2Y,
            double b3X, double b3Y,
            double b4X, double b4Y,
            double epsilon = Epsilon)
        {
            Vector2D a, b, c, d;
            Vector2D c12, c11, c10;
            Vector2D c23, c22, c21, c20;
            var result = new Intersection(IntersectionState.NoIntersection);
            a = new Vector2D(a2X, a2Y).Scale(-2);
            c12 = new Vector2D(a1X, a1Y).Add(a.Add((a3X, a3Y)));
            a = new Vector2D(a1X, a1Y).Scale(-2);
            b = new Vector2D(a2X, a2Y).Scale(2);
            c11 = a.Add(b);
            c10 = new Point2D(a1X, a1Y);
            a = new Vector2D(b1X, b1Y).Scale(-1);
            b = new Vector2D(b2X, b2Y).Scale(3);
            c = new Vector2D(b3X, b3Y).Scale(-3);
            d = a.Add(b.Add(c.Add((b4X, b4Y))));
            c23 = new Vector2D(d.I, d.J);
            a = new Vector2D(b1X, b1Y).Scale(3);
            b = new Vector2D(b2X, b2Y).Scale(-6);
            c = new Vector2D(b3X, b3Y).Scale(3);
            d = a.Add(b.Add(c));
            c22 = new Vector2D(d.I, d.J);
            a = new Vector2D(b1X, b1Y).Scale(-3);
            b = new Vector2D(b2X, b2Y).Scale(3);
            c = a.Add(b);
            c21 = new Vector2D(c.I, c.J);
            c20 = new Vector2D(b1X, b1Y);
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
                result.State = IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <param name="a3X"></param>
        /// <param name="a3Y"></param>
        /// <param name="b1X"></param>
        /// <param name="b1Y"></param>
        /// <param name="b2X"></param>
        /// <param name="b2Y"></param>
        /// <param name="b3X"></param>
        /// <param name="b3Y"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        public static Intersection QuadraticBezierQuadraticBezierIntersection(
            double a1X, double a1Y,
            double a2X, double a2Y,
            double a3X, double a3Y,
            double b1X, double b1Y,
            double b2X, double b2Y,
            double b3X, double b3Y,
            double epsilon = Epsilon)
        {
            Vector2D va, vb;
            Vector2D c12, c11, c10;
            Vector2D c22, c21, c20;
            var result = new Intersection(IntersectionState.NoIntersection);
            va = new Vector2D(a2X, a2Y).Scale(-2);
            c12 = new Vector2D(a1X, a1Y).Add(va.Add((a3X, a3Y)));
            va = new Vector2D(a1X, a1Y).Scale(-2);
            vb = new Vector2D(a2X, a2Y).Scale(2);
            c11 = va.Add(vb);
            c10 = new Point2D(a1X, a1Y);
            va = new Vector2D(b2X, b2Y).Scale(-2);
            c22 = new Vector2D(b1X, b1Y).Add(va.Add((b3X, b3Y)));
            va = new Vector2D(b1X, b1Y).Scale(-2);
            vb = new Vector2D(b2X, b2Y).Scale(2);
            c21 = va.Add(vb);
            c20 = new Point2D(b1X, b1Y);
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
                result.State = IntersectionState.Intersection;
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

