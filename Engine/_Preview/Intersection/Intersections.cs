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
    /// A collection of methods for collecting the interactions of shapes.
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
            var s = Maths.WrapAngle(startAngle);
            var e = Maths.WrapAngle(s + sweepAngle);
            var a = Maths.WrapAngle(angle);

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
            => PolygonContourContainsPoint(polygon.Points, point.X, point.Y);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="figure"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this PolycurveContour figure, Point2D point)
            => PolycurveContourContainsPoint(figure, point);

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
            => PolygonContainsPoint(polygons.Contours, point.X, point.Y);

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
        /// Find the intersection of two Points. 
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Point2D p0, Point2D p1)
            => PointPointIntersection(p0.X, p0.Y, p1.X, p1.Y);

        /// <summary>
        /// Find the intersection of a Line segment and a Point.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, Point2D p)
            => PointLineSegmentIntersection(p.X, p.Y, s.AX, s.AY, s.BX, s.BY);

        /// <summary>
        /// Find the intersection of a Point and a Line segment.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Point2D p, LineSegment s)
            => PointLineSegmentIntersection(p.X, p.Y, s.AX, s.AY, s.BX, s.BY);

        /// <summary>
        /// Find the intersection of two Rays.
        /// </summary>
        /// <param name="r0"></param>
        /// <param name="r1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray r0, Ray r1)
            => RayRayIntersection(r0.Location.X, r0.Location.Y, r0.Direction.I, r0.Direction.J, r1.Location.X, r1.Location.Y, r1.Direction.I, r1.Direction.J);

        /// <summary>
        /// Find the intersection of two lines segments.
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s1, LineSegment s2)
            => LineSegmentLineSegmentIntersection(s1.A.X, s1.A.Y, s1.B.X, s1.B.Y, s2.A.X, s2.A.Y, s2.B.X, s2.B.Y);

        /// <summary>
        /// Find the intersection of to Lines.
        /// </summary>
        /// <param name="l0"></param>
        /// <param name="l1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l0, Line l1)
            => LineLineIntersection(
                l0.Location.X, l0.Location.Y,
                l0.Location.X + l0.Direction.I, l0.Location.Y + l0.Direction.J,
                l1.Location.X, l1.Location.Y,
                l1.Location.X + l1.Direction.I, l1.Location.Y + l1.Direction.J);

        /// <summary>
        /// Find the intersection of a Line and a Line segment.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, LineSegment s)
            => LineLineSegmentIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, s.AX, s.AY, s.BX, s.BY);

        /// <summary>
        /// Find the intersection of a Line segment and a Line.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, Line l)
            => LineLineSegmentIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, s.AX, s.AY, s.BX, s.BY);

        /// <summary>
        /// Find the intersection of a circle and a line segment.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, Circle c)
            => CircleLineSegmentIntersection(c.X, c.Y, c.Radius, s.A.X, s.A.Y, s.B.X, s.B.Y);

        /// <summary>
        /// Find the intersection of a circle and a line segment.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, LineSegment s)
            => CircleLineSegmentIntersection(c.X, c.Y, c.Radius, s.A.X, s.A.Y, s.B.X, s.B.Y);

        /// <summary>
        /// Find the intersection of a circle and a line.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, CircularArc c)
            => LineCircularArcIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, c.X, c.Y, c.Radius, 0, c.StartAngle, c.SweepAngle);

        /// <summary>
        /// Find the intersection of a circle and a line.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CircularArc c, Line l)
            => LineCircularArcIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, c.X, c.Y, c.Radius, 0, c.StartAngle, c.SweepAngle);

        /// <summary>
        /// Find the intersection of a circle and a line.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, Circle c)
            => LineCircleIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, c.X, c.Y, c.Radius);

        /// <summary>
        /// Find the intersection of a circle and a line.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, Line l)
            => LineCircleIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, c.X, c.Y, c.Radius);

        /// <summary>
        /// Find the intersection between two circles.
        /// </summary>
        /// <param name="c0"></param>
        /// <param name="c1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c0, Circle c1)
            => CircleCircleIntersection(c0.X, c0.Y, c0.Radius, c1.X, c1.Y, c1.Radius);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="s"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, Line s, double epsilon = Epsilon)
            => LineEllipseIntersection(s.Location.X, s.Location.Y, s.Location.X + s.Direction.I, s.Location.Y + s.Direction.J, e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="epsilon"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line s, Ellipse e, double epsilon = Epsilon)
            => LineEllipseIntersection(s.Location.X, s.Location.Y, s.Location.X + s.Direction.I, s.Location.Y + s.Direction.J, e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="s"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this EllipticalArc e, Line s, double epsilon = Epsilon)
            => LineEllipticalArcIntersection(s.Location.X, s.Location.Y, s.Location.X + s.Direction.I, s.Location.Y + s.Direction.J, e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, e.StartAngle, e.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="epsilon"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line s, EllipticalArc e, double epsilon = Epsilon)
            => LineEllipticalArcIntersection(s.Location.X, s.Location.Y, s.Location.X + s.Direction.I, s.Location.Y + s.Direction.J, e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, e.StartAngle, e.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="s"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this EllipticalArc e, LineSegment s, double epsilon = Epsilon)
            => EllipticalArcLineSegmentIntersection(e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, e.StartAngle, e.SweepAngle, s.A.X, s.A.Y, s.B.X, s.B.Y, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="epsilon"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, EllipticalArc e, double epsilon = Epsilon)
            => EllipticalArcLineSegmentIntersection(e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, e.StartAngle, e.SweepAngle, s.A.X, s.A.Y, s.B.X, s.B.Y, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="s"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, LineSegment s, double epsilon = Epsilon)
            => EllipseLineSegmentIntersection(e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, s.A.X, s.A.Y, s.B.X, s.B.Y, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="epsilon"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, Ellipse e, double epsilon = Epsilon)
            => EllipseLineSegmentIntersection(e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, s.A.X, s.A.Y, s.B.X, s.B.Y, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a rectangle.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="r"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, Rectangle2D r, double epsilon = Epsilon)
            => EllipseRectangleIntersection(e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a rectangle.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="e"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r, Ellipse e, double epsilon = Epsilon)
            => EllipseRectangleIntersection(e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a polygon.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="p"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, Contour p, double epsilon = Epsilon)
            => EllipsePolygonIntersection(e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a polygon.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="e"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Contour p, Ellipse e, double epsilon = Epsilon)
            => EllipsePolygonIntersection(e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of a Circle and an unrotated Ellipse.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, Ellipse e)
            => UnrotatedEllipseUnrotatedEllipseIntersection(c.Center.X, c.Center.Y, c.Radius, c.Radius, e.Center.X, e.Center.Y, e.RX, e.RY);

        /// <summary>
        /// Find the intersection of an unrotated Ellipse and a Circle.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, Circle c)
            => UnrotatedEllipseUnrotatedEllipseIntersection(c.Center.X, c.Center.Y, c.Radius, c.Radius, e.Center.X, e.Center.Y, e.RX, e.RY);

        /// <summary>
        /// Find the intersection of two unrotated Ellipses.
        /// </summary>
        /// <param name="e0"></param>
        /// <param name="e1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e0, Ellipse e1)
            => UnrotatedEllipseUnrotatedEllipseIntersection(e0.Center.X, e0.Center.Y, e0.RX, e0.RY, e1.Center.X, e1.Center.Y, e1.RX, e1.RY);

        /// <summary>
        /// Find the intersection of a Line and a Rectangle.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, Rectangle2D r)
            => LineRectangleIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, r.X, r.Y, r.Right, r.Bottom);

        /// <summary>
        /// Find the intersection of a Rectangle and a line.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r, Line l)
            => LineRectangleIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, r.X, r.Y, r.Right, r.Bottom);

        /// <summary>
        /// Find the intersection of a Rectangle and a Line segment.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r, LineSegment l)
            => LineSegmentRectangleIntersection(l.AX, l.AY, l.BX, l.BY, r.X, r.Y, r.Right, r.Bottom);

        /// <summary>
        /// Find the intersection of a Line segment and a Rectangle.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment l, Rectangle2D r)
            => LineSegmentRectangleIntersection(l.AX, l.AY, l.BX, l.BY, r.X, r.Y, r.Right, r.Bottom);

        /// <summary>
        /// Find the intersection of two Rectangles.
        /// </summary>
        /// <param name="r0"></param>
        /// <param name="r1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r0, Rectangle2D r1)
            => RectangleRectangleIntersection(r0.X, r0.Y, r0.Right, r0.Bottom, r1.X, r1.Y, r1.Right, r1.Bottom);

        /// <summary>
        /// Find the intersection of a Line and a Polygon contour.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, Contour p)
            => LinePolygonContourIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, p.Points);

        /// <summary>
        /// Find the intersection of a Polygon contour and a Line.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Contour p, Line l)
            => LinePolygonContourIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, p.Points);

        /// <summary>
        /// Find the intersection of a Line segment and a Polygon contour.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment l, Contour p)
            => LineSegmentPolygonContourIntersection(l.AX, l.AY, l.BX, l.BY, p.Points);

        /// <summary>
        /// Find the intersection of a Polygon contour and a Line segment.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Contour p, LineSegment l)
            => LineSegmentPolygonContourIntersection(l.AX, l.AY, l.BX, l.BY, p.Points);

        /// <summary>
        /// Find the intersection of a Polygon contour and a Rectangle.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Contour p, Rectangle2D l)
            => PolygonRectangleIntersection(p.Points, l.X, l.Y, l.Right, l.Bottom);

        /// <summary>
        /// Find the intersection of a Rectangle and a Polygon contour.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D l, Contour p)
            => PolygonRectangleIntersection(p.Points, l.X, l.Y, l.Right, l.Bottom);

        /// <summary>
        /// Find the intersection of two Polygon contours.
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Contour p0, Contour p1)
            => PolygonPolygonIntersection(p0.Points, p1.Points);

        /// <summary>
        /// Find the intersection of a Circle and a Rectangle.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, Rectangle2D r)
            => CircleRectangleIntersection(c.X, c.Y, c.Radius, r.X, r.Y, r.Right, r.Bottom);

        /// <summary>
        /// Find the intersection of a Rectangle and a Circle.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r, Circle c)
            => CircleRectangleIntersection(c.X, c.Y, c.Radius, r.X, r.Y, r.Right, r.Bottom);

        /// <summary>
        /// Find the intersection of a Cubic Bezier and a Line segment.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b, Line l)
            => LineCubicBezierIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY);

        /// <summary>
        /// Find the intersection of a Line segment and a Cubic Bezier.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, CubicBezier b)
            => LineCubicBezierIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY);

        /// <summary>
        /// Find the intersection of a Cubic Bezier and a Line segment.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b, LineSegment l)
            => LineSegmentCubicBezierIntersection(l.AX, l.AY, l.BX, l.BY, b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY);

        /// <summary>
        /// Find the intersection of a Line segment and a Cubic Bezier.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment l, CubicBezier b)
            => LineSegmentCubicBezierIntersection(l.AX, l.AY, l.BX, l.BY, b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY);

        /// <summary>
        /// Find the intersection of a Cubic Bezier and a Rectangle.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b, Rectangle2D r)
            => CubicBezierRectangleIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, r.X, r.Y, r.Right, r.Bottom);

        /// <summary>
        /// Find the intersection of a Rectangle and a Cubic Bezier.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r, CubicBezier b)
            => CubicBezierRectangleIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, r.X, r.Y, r.Right, r.Bottom);

        /// <summary>
        /// Find the intersection of a Cubic Bezier and a Polygon Contour.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b, Contour p)
            => CubicBezierPolygonIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, p.Points);

        /// <summary>
        /// Find the intersection of a Polygon Contour and a Cubic Bezier.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Contour p, CubicBezier b)
            => CubicBezierPolygonIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, p.Points);

        /// <summary>
        /// Find the intersection of a Cubic Bezier and a Circle.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b, Circle c)
            => CubicBezierUnrotatedEllipseIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, c.X, c.Y, c.Radius, c.Radius);

        /// <summary>
        /// Find the intersection of a Circle and a Cubic Bezier.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, CubicBezier b)
            => CubicBezierUnrotatedEllipseIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, c.X, c.Y, c.Radius, c.Radius);

        /// <summary>
        /// Find the intersection of a Cubic Bezier and an unrotated Ellipse.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b, Ellipse e)
            => CubicBezierUnrotatedEllipseIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, e.X, e.Y, e.RX, e.RY);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a Cubic Bezier.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, CubicBezier b)
            => CubicBezierUnrotatedEllipseIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, e.X, e.Y, e.RX, e.RY);

        /// <summary>
        /// Find the intersection of two Cubic Beziers.
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="b1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b0, CubicBezier b1)
            => CubicBezierCubicBezierIntersection(b0.AX, b0.AY, b0.BX, b0.BY, b0.CX, b0.CY, b0.DX, b0.DY, b1.AX, b1.AY, b1.BX, b1.BY, b1.CX, b1.CY, b1.DX, b1.DY);

        /// <summary>
        /// Find the intersection of a Quadratic Bezier and a Line segment.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b, Line l)
            => LineQuadraticBezierIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, b.AX, b.AY, b.BX, b.BY, b.CX, b.CY);

        /// <summary>
        /// Find the intersection of a Line segment and a Quadratic Bezier.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, QuadraticBezier b)
            => LineQuadraticBezierIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, b.AX, b.AY, b.BX, b.BY, b.CX, b.CY);

        /// <summary>
        /// Find the intersection of a Quadratic Bezier and a Line segment.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b, LineSegment l)
            => LineSegmentQuadraticBezierIntersection(l.AX, l.AY, l.BX, l.BY, b.AX, b.AY, b.BX, b.BY, b.CX, b.CY);

        /// <summary>
        /// Find the intersection of a Line segment and a Quadratic Bezier.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment l, QuadraticBezier b)
            => LineSegmentQuadraticBezierIntersection(l.AX, l.AY, l.BX, l.BY, b.AX, b.AY, b.BX, b.BY, b.CX, b.CY);

        /// <summary>
        /// Find the intersection of a Quadratic Bezier and a Rectangle.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b, Rectangle2D r)
            => QuadraticBezierRectangleIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, r.X, r.Y, r.Right, r.Bottom);

        /// <summary>
        /// Find the intersection of a Rectangle and a Quadratic Bezier.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r, QuadraticBezier b)
            => QuadraticBezierRectangleIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, r.X, r.Y, r.Right, r.Bottom);

        /// <summary>
        /// Find the intersection of a Quadratic Bezier and a Polygon Contour.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b, Contour p)
            => QuadraticBezierPolygonIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, p.Points);

        /// <summary>
        /// Find the intersection of a Polygon contour and a Quadratic Bezier.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Contour p, QuadraticBezier b)
            => QuadraticBezierPolygonIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, p.Points);

        /// <summary>
        /// Find the intersection of a Quadratic Bezier and a Circle.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b, Circle c)
            => QuadraticBezierUnrotatedEllipseIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, c.X, c.Y, c.Radius, c.Radius);

        /// <summary>
        /// Find the intersection of a Circle and a Quadratic Bezier.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, QuadraticBezier b)
            => QuadraticBezierUnrotatedEllipseIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, c.X, c.Y, c.Radius, c.Radius);

        /// <summary>
        /// Find the intersection of a Quadratic Bezier and an unrotated Ellipse.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b, Ellipse e)
            => QuadraticBezierUnrotatedEllipseIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, e.X, e.Y, e.RX, e.RY);

        /// <summary>
        /// Find the intersection of an unrotated Ellipse and a Quadratic Bezier.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, QuadraticBezier b)
            => QuadraticBezierUnrotatedEllipseIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, e.X, e.Y, e.RX, e.RY);

        /// <summary>
        /// Find the intersection of a Quadratic Bezier and a Cubic Bezier.
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="b1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b0, CubicBezier b1)
            => QuadraticBezierCubicBezierIntersection(b0.AX, b0.AY, b0.BX, b0.BY, b0.CX, b0.CY, b1.AX, b1.AY, b1.BX, b1.BY, b1.CX, b1.CY, b1.DX, b1.DY);

        /// <summary>
        /// Find the intersection of a Cubic Bezier and a Quadratic Bezier.
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b0"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b1, QuadraticBezier b0)
            => QuadraticBezierCubicBezierIntersection(b0.AX, b0.AY, b0.BX, b0.BY, b0.CX, b0.CY, b1.AX, b1.AY, b1.BX, b1.BY, b1.CX, b1.CY, b1.DX, b1.DY);

        /// <summary>
        /// Find the intersection of two Quadratic Beziers.
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="b1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b0, QuadraticBezier b1)
            => QuadraticBezierQuadraticBezierIntersection(b0.AX, b0.AY, b0.BX, b0.BY, b0.CX, b0.CY, b1.AX, b1.AY, b1.BX, b1.BY, b1.CX, b1.CY);

        /// <summary>
        /// Find the intersection of two Bezier segments.
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="b1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this BezierSegmentX b0, BezierSegmentX b1)
        {
            switch (b0.Degree)
            {
                case PolynomialDegree.Linear:
                    switch (b1.Degree)
                    {
                        case PolynomialDegree.Linear:
                            return LineSegmentLineSegmentIntersection(b1[0].X, b1[0].Y, b1[1].X, b1[1].Y, b0[0].X, b0[0].Y, b0[1].X, b0[1].Y);
                        case PolynomialDegree.Quadratic:
                            return LineSegmentQuadraticBezierIntersection(b1[0].X, b1[0].Y, b1[1].X, b1[1].Y, b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b0[2].X, b0[2].Y);
                        case PolynomialDegree.Cubic:
                            return LineSegmentCubicBezierIntersection(b1[0].X, b1[0].Y, b1[1].X, b1[1].Y, b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b0[2].X, b0[2].Y, b0[3].X, b0[3].Y);
                        default:
                            return new Intersection(IntersectionState.NoIntersection);
                    }
                case PolynomialDegree.Quadratic:
                    switch (b1.Degree)
                    {
                        case PolynomialDegree.Linear:
                            return LineSegmentQuadraticBezierIntersection(b1[0].X, b1[0].Y, b1[1].X, b1[1].Y, b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b0[2].X, b0[2].Y);
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
                            return LineSegmentCubicBezierIntersection(b1[0].X, b1[0].Y, b1[1].X, b1[1].Y, b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b0[2].X, b0[2].Y, b0[3].X, b0[3].Y);
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
        /// Find the intersection of a line segment and a Bezier segment.
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment l, BezierSegmentX b0)
            => Intersection(b0, l);

        /// <summary>
        /// Find the intersection of a Bezier segment and a line segment.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this BezierSegmentX b, LineSegment l)
        {
            switch (b.Degree)
            {
                case PolynomialDegree.Linear:
                    return LineSegmentLineSegmentIntersection(l.AX, l.AY, l.BX, l.BY, b[0].X, b[0].Y, b[1].X, b[1].Y);
                case PolynomialDegree.Quadratic:
                    return LineSegmentQuadraticBezierIntersection(l.AX, l.AY, l.BX, l.BY, b[0].X, b[0].Y, b[1].X, b[1].Y, b[2].X, b[2].Y);
                case PolynomialDegree.Cubic:
                    return LineSegmentCubicBezierIntersection(l.AX, l.AY, l.BX, l.BY, b[0].X, b[0].Y, b[1].X, b[1].Y, b[2].X, b[2].Y, b[3].X, b[3].Y);
                default:
                    return new Intersection(IntersectionState.NoIntersection);
            }
        }

        /// <summary>
        /// Find the intersection of a Quadratic Bezier and a Bezier segment.
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="b1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b0, BezierSegmentX b1)
            => Intersection(b1, b0);

        /// <summary>
        /// Find the intersection of a Bezier segment and a Quadratic Bezier.
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="b1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this BezierSegmentX b0, QuadraticBezier b1)
        {
            switch (b0.Degree)
            {
                case PolynomialDegree.Linear:
                    return LineSegmentQuadraticBezierIntersection(b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b1.AX, b1.AY, b1.BX, b1.BY, b1.CX, b1.CY);
                case PolynomialDegree.Quadratic:
                    return QuadraticBezierQuadraticBezierIntersection(b1.AX, b1.AY, b1.BX, b1.BY, b1.CX, b1.CY, b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b0[2].X, b0[2].Y);
                case PolynomialDegree.Cubic:
                    return QuadraticBezierCubicBezierIntersection(b1.AX, b1.AY, b1.BX, b1.BY, b1.CX, b1.CY, b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b0[2].X, b0[2].Y, b0[3].X, b0[3].Y);
                default:
                    return new Intersection(IntersectionState.NoIntersection);
            }
        }

        /// <summary>
        /// Find the intersection of a Cubic Bezier and a Bezier segment.
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="b1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b0, BezierSegmentX b1)
            => Intersection(b1, b0);

        /// <summary>
        /// Find the intersection of a Bezier segment and a Cubic Bezier.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this BezierSegmentX b, CubicBezier c)
        {
            switch (b.Degree)
            {
                case PolynomialDegree.Linear:
                    return LineSegmentCubicBezierIntersection(c.AX, c.AY, c.BX, c.BY, c.CX, c.CY, c.DX, c.DY, b[0].X, b[0].Y, b[1].X, b[1].Y);
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointInTriangle(LineSegment s, Point2D o, Point2D p)
        {
            var x = Sign(s.A, s.B, p);
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
            var clockwise = ((((b - a).CrossProduct(p - b))) >= 0);
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
        /// <param name="epsilon"></param>
        /// <returns>
        /// Returns Outside (0) if false, Inside (+1) if true, Boundary (-1) if the point is on a polygon boundary.
        /// </returns>
        /// <remarks>
        /// Adapted from Clipper library: http://www.angusj.com/delphi/clipper.php
        /// See "The Point in Polygon Problem for Arbitrary Polygons" by Hormann and Agathos
        /// http://www.inf.usi.ch/hormann/papers/Hormann.2001.TPI.pdf
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion PolygonContourContainsPoint(
            List<Point2D> points,
            double pX, double pY,
            double epsilon = Epsilon)
        {
            // Default value is no inclusion.
            Inclusion result = Inclusion.Outside;

            // Special cases for points and line segments.
            if (points.Count < 3)
                if (points.Count == 1)
                    // If the polygon has 1 point, it is a point and has no interior, but a point can intersect a point.
                    return (pX == points[0].X && pY == points[0].Y) ? Inclusion.Boundary : Inclusion.Outside;
                else if (points.Count == 2)
                    // If the polygon has 2 points, it is a line and has no interior, but a point can intersect a line.
                    return ((pX == points[0].X) && (pY == points[0].Y))
                        || ((pX == points[1].X) && (pY == points[1].Y))
                        || (((pX > points[0].X) == (pX < points[1].X))
                        && ((pY > points[0].Y) == (pY < points[1].Y))
                        && ((pX - points[0].X) * (points[1].Y - points[0].Y) == (pY - points[0].Y) * (points[1].X - points[0].X))) ? Inclusion.Boundary : Inclusion.Outside;
                else
                    // Empty geometry.
                    return Inclusion.Outside;

            // Loop through each line segment.
            Point2D curPoint = points[0];
            for (var i = 1; i <= points.Count; ++i)
            {
                Point2D nextPoint = (i == points.Count ? points[0] : points[i]);

                // Special case for horizontal lines. Check whether the point is on one of the ends, or whether the point is on the segment, if the line is horizontal.
                if (((curPoint.Y == pY)) && (((curPoint.X == pX)) || ((nextPoint.Y == pY) && ((curPoint.X > pX) == (nextPoint.X < pX)))))
                //if ((Abs(nextPoint.Y - pY) < epsilon) && ((Abs(nextPoint.X - pX) < epsilon) || (Abs(curPoint.Y - pY) < epsilon && ((nextPoint.X > pX) == (curPoint.X < pX)))))
                {
                    return Inclusion.Boundary;
                }

                // If Point between start and end points horizontally.
                //if ((curPoint.Y < pY) == (nextPoint.Y >= pY))
                if ((nextPoint.Y < pY) != (curPoint.Y < pY))
                {
                    // If point between start and end points vertically.
                    if (nextPoint.X >= pX)
                    {
                        if (curPoint.X > pX)
                        {
                            result = 1 - result;
                        }
                        else
                        {
                            var determinant = (nextPoint.X - pX) * (curPoint.Y - pY) - (curPoint.X - pX) * (nextPoint.Y - pY);
                            if (Abs(determinant) < epsilon)
                                return Inclusion.Boundary;
                            else if ((determinant > 0) == (curPoint.Y > nextPoint.Y))
                                result = 1 - result;
                        }
                    }
                    else if (curPoint.X > pX)
                    {
                        var determinant = (nextPoint.X - pX) * (curPoint.Y - pY) - (curPoint.X - pX) * (nextPoint.Y - pY);
                        if (Abs(determinant) < epsilon)
                            return Inclusion.Boundary;
                        if ((determinant > 0) == (curPoint.Y > nextPoint.Y))
                            result = 1 - result;
                    }
                }

                curPoint = nextPoint;
            }

            return result;
        }

#if PolycurveTest

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="point"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public static Inclusion PolycurveContourContainsPoint(
            PolycurveContour path,
            Point2D point,
            double epsilon = Epsilon)
        {
            var inside = 0;

            foreach (var item in path)
            {
                switch (item)
                {
                    case PathLineSegment l:
                        {
                            var p1 = l.Start.Value;
                            var p2 = l.End.Value;

                            if ((p1.Y < point.Y != p2.Y < point.Y) && //at least one point is below the Y threshold and the other is above or equal
                                (p1.X >= point.X || p2.X >= point.X)) //optimization: at least one point must be to the right of the test point
                            {
                                if (p1.X + (point.Y - p1.Y) / (p2.Y - p1.Y) * (p2.X - p1.X) > point.X)
                                    inside += 1;
                            }
                        }
                        break;
                    case PathArc a:
                        {
                            var p1 = a.Start.Value;
                            var p2 = a.End.Value;

                        }
                        break;
                    default:
                        break;
                }
            }

            return inside % 2 == 1 ? Inclusion.Inside : Inclusion.Outside;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="point"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public static Inclusion PolycurveContourContainsPoint2(
            PolycurveContour path,
            Point2D point,
            double epsilon = Epsilon)
        {
            Inclusion result = Inclusion.Outside;
            Inclusion boundary = Inclusion.Outside;

            if (path.Count < 2)
                return Contains(path[0].Start.Value, point);
            foreach (var item in path)
            {
                switch (item)
                {
                    case PathPoint p:
                        {
                            if (path[0].Start.Value == point)
                                return Inclusion.Boundary;
                            break;
                        }
                    case PathLineSegment l:
                        {
                            // Special case for horizontal lines. Check whether the point is on one of the ends, or whether the point is on the segment, if the line is horizontal.
                            if (((l.End.Value.Y == point.Y)) && (((l.End.Value.X == point.X)) || ((l.Start.Value.Y == point.Y) && ((l.End.Value.X > point.X) == (l.Start.Value.X < point.X)))))
                            //if ((Abs(nextPoint.Y - pY) < epsilon) && ((Abs(nextPoint.X - pX) < epsilon) || (Abs(curPoint.Y - pY) < epsilon && ((nextPoint.X > pX) == (curPoint.X < pX)))))
                            {
                                return Inclusion.Boundary;
                            }

                            // If Point between start and end points horizontally.
                            //if ((curPoint.Y < pY) == (nextPoint.Y >= pY))
                            if ((l.Start.Value.Y < point.Y) != (l.End.Value.Y < point.Y))
                            {
                                // If point between start and end points vertically.
                                if (l.Start.Value.X >= point.X)
                                {
                                    if (l.End.Value.X > point.X)
                                    {
                                        result = 1 - result;
                                    }
                                    else
                                    {
                                        var determinant = (l.Start.Value.X - point.X) * (l.End.Value.Y - point.Y) - (l.End.Value.X - point.X) * (l.Start.Value.Y - point.Y);
                                        if (Abs(determinant) < epsilon)
                                            return Inclusion.Boundary;
                                        else if ((determinant > 0) == (l.End.Value.Y > l.Start.Value.Y))
                                            result = 1 - result;
                                    }
                                }
                                else if (l.End.Value.X > point.X)
                                {
                                    var determinant = (l.Start.Value.X - point.X) * (l.End.Value.Y - point.Y) - (l.End.Value.X - point.X) * (l.Start.Value.Y - point.Y);
                                    if (Abs(determinant) < epsilon)
                                        return Inclusion.Boundary;
                                    if ((determinant > 0) == (l.End.Value.Y > l.Start.Value.Y))
                                        result = 1 - result;
                                }
                            }
                            break;
                        }
                    case PathArc t:
                        {
                            // Find the start and end angles.
                            var sa = EllipsePolarAngle(t.StartAngle, t.RX, t.RY);
                            var ea = EllipsePolarAngle(t.StartAngle + t.SweepAngle, t.RX, t.RY);

                            // Get the ellipse rotation transform.
                            var cosT = Cos(t.Angle);
                            var sinT = Sin(t.Angle);

                            // Ellipse equation for an ellipse at origin for the chord end points.
                            var u1 = t.RX * Cos(sa);
                            var v1 = -(t.RY * Sin(sa));
                            var u2 = t.RX * Cos(ea);
                            var v2 = -(t.RY * Sin(ea));

                            // Find the points of the chord.
                            var sX = t.Center.X + (u1 * cosT + v1 * sinT);
                            var sY = t.Center.Y + (u1 * sinT - v1 * cosT);
                            var eX = t.Center.X + (u2 * cosT + v2 * sinT);
                            var eY = t.Center.Y + (u2 * sinT - v2 * cosT);

                            // Find the determinant of the chord.
                            var determinant = (sX - point.X) * (eY - point.Y) - (eX - point.X) * (sY - point.Y);

                            // Check whether the point is on the side of the chord as the center.
                            if (Sign(-determinant) == Sign(t.SweepAngle))
                            {
                                // Translate points to origin.
                                var u0 = point.X - t.Center.X;
                                var v0 = point.Y - t.Center.Y;

                                // Apply the rotation transformation.
                                var a = u0 * cosT + v0 * sinT;
                                var b = u0 * sinT - v0 * cosT;

                                // Normalize the radius.
                                var normalizedRadius
                                    = ((a * a) / (t.RX * t.RX))
                                    + ((b * b) / (t.RY * t.RY));

                                if (Abs(normalizedRadius - 1d) < Epsilon)
                                    return Inclusion.Boundary;

                                if (normalizedRadius < 1d)
                                    result = 1 - result;
                            }


                            // If Point between start and end points horizontally.
                            //if ((curPoint.Y < pY) == (nextPoint.Y >= pY))
                            if ((t.Start.Value.Y < point.Y) != (t.End.Value.Y < point.Y))
                            {
                                // If point between start and end points vertically.
                                if (t.Start.Value.X >= point.X)
                                {
                                    if (t.End.Value.X > point.X)
                                    {
                                        result = 1 - result;
                                    }
                                    else
                                    {
                                        var determinant2 = (t.Start.Value.X - point.X) * (t.End.Value.Y - point.Y) - (t.End.Value.X - point.X) * (t.Start.Value.Y - point.Y);
                                        if ((determinant2 > 0) == (t.End.Value.Y > t.Start.Value.Y))
                                            result = 1 - result;
                                    }
                                }
                                else if (t.End.Value.X > point.X)
                                {
                                    var determinant2 = (t.Start.Value.X - point.X) * (t.End.Value.Y - point.Y) - (t.End.Value.X - point.X) * (t.Start.Value.Y - point.Y);
                                    if ((determinant2 > 0) == (t.End.Value.Y > t.Start.Value.Y))
                                        result = 1 - result;
                                }
                            }

                            break;
                        }
                    case PathQuadraticBezier b:
                        break;
                    case PathCubicBezier b:
                        break;
                    case PathCardinal c:
                        break;
                    default:
                        break;
                }

                if (boundary == Inclusion.Boundary)
                {
                    result = boundary;
                    return result;
                }
            }
            return result;
        }

#else 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="figure"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion PolycurveContourContainsPoint(PolycurveContour figure, Point2D point)
        {
            Inclusion included = PolygonContourContainsPoint(figure.Nodes, point.X, point.Y);
            foreach (var item in figure?.Items)
            {
                switch (item)
                {
                    case ArcSegment t:
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

#endif

        /// <summary>
        /// Determines whether the specified point is contained withing the set of regions defined by this <see cref="Polygon"/>.
        /// </summary>
        /// <param name="polygons">List of polygons.</param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion PolygonContainsPoint(List<Contour> polygons, double pX, double pY)
        {
            Inclusion returnValue = Inclusion.Outside;

            foreach (Contour poly in polygons)
            {
                // Use alternating rule with XOR to determine if the point is in a polygon or a hole.
                // If the point is in an odd number of polygons, it is inside. If even, it is a hole.
                returnValue ^= PolygonContourContainsPoint(poly.Points, pX, pY);

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
                var dx = x - pX;
                var dy = y - pY;
                dx *= dx;
                dy *= dy;
                var distanceSquared = dx + dy;
                var radiusSquared = r * r;
                return (radiusSquared >= distanceSquared) ? ((Abs(radiusSquared - distanceSquared) < Epsilon) ? Inclusion.Boundary : Inclusion.Inside) : Inclusion.Outside;
            }

            return Inclusion.Outside;
        }

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="Ellipse"/>.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
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
        public static Inclusion EllipseContainsPoint(
            double cX, double cY,
            double r1, double r2,
            double angle,
            double pX, double pY)
        {
            if (r1 <= 0d || r2 <= 0d)
                return Inclusion.Outside;

            // Get the ellipse rotation transform.
            var cosT = Cos(angle);
            var sinT = Sin(angle);

            // Translate point to origin.
            var u = pX - cX;
            var v = pY - cY;

            // Apply the rotation transformation.
            var a = (u * cosT + v * sinT);
            var b = (u * sinT - v * cosT);

            var normalizedRadius = ((a * a) / (r1 * r1))
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
                Point2D startPoint = Interpolators.CircularArc(x, y, r, startAngle, sweepAngle, 0);
                Point2D endPoint = Interpolators.CircularArc(x, y, r, startAngle, sweepAngle, 1);

                // Find the determinant of the chord and point.
                var determinant = (startPoint.X - pX) * (endPoint.Y - pY) - (endPoint.X - pX) * (startPoint.Y - pY);

                // Check if the point is on the chord.
                if (Abs(determinant) < Epsilon)
                    return Inclusion.Boundary;
                // Check whether the point is on the same side of the chord as the center.
                else if (Sign(determinant) == Sign(sweepAngle))
                    return Inclusion.Outside;

                var dx = x - pX;
                var dy = y - pY;
                dx *= dx;
                dy *= dy;
                var distanceSquared = dx + dy;
                var radiusSquared = r * r;
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
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks>
        /// Based off of: http://stackoverflow.com/questions/7946187/point-and-ellipse-rotated-position-test-algorithm
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion EllipticalArcContainsPoint(double cX, double cY, double r1, double r2, double angle, double startAngle, double sweepAngle, double pX, double pY, double epsilon = Epsilon)
        {
            // If the ellipse is empty it can't contain anything.
            if (r1 <= 0d || r2 <= 0d)
                return Inclusion.Outside;

            // Find the start and end angles.
            var sa = EllipticalPolarAngle(startAngle, r1, r2);
            var ea = EllipticalPolarAngle(startAngle + sweepAngle, r1, r2);

            // Get the ellipse rotation transform.
            var cosT = Cos(angle);
            var sinT = Sin(angle);

            // Ellipse equation for an ellipse at origin for the chord end points.
            var u1 = r1 * Cos(sa);
            var v1 = -(r2 * Sin(sa));
            var u2 = r1 * Cos(ea);
            var v2 = -(r2 * Sin(ea));

            // Find the points of the chord.
            var sX = cX + (u1 * cosT + v1 * sinT);
            var sY = cY + (u1 * sinT - v1 * cosT);
            var eX = cX + (u2 * cosT + v2 * sinT);
            var eY = cY + (u2 * sinT - v2 * cosT);

            // Find the determinant of the chord.
            var determinant = (sX - pX) * (eY - pY) - (eX - pX) * (sY - pY);

            // Check whether the point is on the side of the chord as the center.
            if (Sign(determinant) == Sign(sweepAngle))
                return Inclusion.Outside;

            // Translate point to origin.
            var u0 = pX - cX;
            var v0 = pY - cY;

            // Apply the rotation transformation to the point at the origin.
            var a = u0 * cosT + v0 * sinT;
            var b = u0 * sinT - v0 * cosT;

            // Normalize the radius.
            var normalizedRadius
                = ((a * a) / (r1 * r1))
                + ((b * b) / (r2 * r2));

            return (normalizedRadius <= 1d)
                ? ((Abs(normalizedRadius - 1d) < epsilon)
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
            // If the ellipse is empty it can't contain anything.
            if (r1 <= 0d || r2 <= 0d)
                return Inclusion.Outside;

            // Find the start and end angles.
            var sa = EllipticalPolarAngle(startAngle, r1, r2);
            var ea = EllipticalPolarAngle(startAngle + sweepAngle, r1, r2);

            // Get the ellipse rotation transform.
            var cosT = Cos(angle);
            var sinT = Sin(angle);

            // Ellipse equation for an ellipse at origin for the chord end points.
            var u1 = r1 * Cos(sa);
            var v1 = -(r2 * Sin(sa));
            var u2 = r1 * Cos(ea);
            var v2 = -(r2 * Sin(ea));

            // Find the points of the chord.
            var sX = cX + (u1 * cosT + v1 * sinT);
            var sY = cY + (u1 * sinT - v1 * cosT);
            var eX = cX + (u2 * cosT + v2 * sinT);
            var eY = cY + (u2 * sinT - v2 * cosT);

            // Find the determinant of the chord.
            var determinant = (sX - pX) * (eY - pY) - (eX - pX) * (sY - pY);

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
            var u0 = pX - cX;
            var v0 = pY - cY;

            // Apply the rotation transformation.
            var a = u0 * cosT + v0 * sinT;
            var b = u0 * sinT - v0 * cosT;

            // Normalize the radius.
            var normalizedRadius
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
            var dist = Sqrt(end.X * end.X + end.Y * end.Y);
            var theCos = end.X / dist;
            var theSin = end.Y / dist;

            foreach (Contour poly in polygons.Contours)
            {
                for (var i = 0; i < poly.Points.Count; i++)
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

            return PolygonContainsPoint(polygons.Contours, start.X + end.X / 2.0, start.Y + end.Y / 2.0);
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
            double pointX, double pointY,
            double segmentAX, double segmentAY,
            double segmentBX, double segmentBY)
            => ((pointX == segmentAX) && (pointY == segmentAY))
            || ((pointX == segmentBX) && (pointY == segmentBY))
            || (((pointX > segmentAX) == (pointX < segmentBX))
            && ((pointY > segmentAY) == (pointY < segmentBY))
            && ((pointX - segmentAX) * (segmentBY - segmentAY) == (pointY - segmentAY) * (segmentBX - segmentAX)));

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
        /// <param name="epsilon"></param>
        /// <returns>Returns the point of intersection.</returns>
        /// <remarks>http://www.vb-helper.com/howto_segments_intersect.html</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineSegmentLineSegmentIntersects(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double epsilon = Epsilon)
        {
            // Translate lines to origin.
            var u1 = (x1 - x0);
            var v1 = (y1 - y0);
            var u2 = (x3 - x2);
            var v2 = (y3 - y2);

            // Calculate the determinant of the coefficient matrix.
            var determinant = (v2 * u1) - (u2 * v1);

            // Check if the line segments are parallel.
            if (Abs(determinant) < epsilon)
                // Return whether line segments are coincidental.
                return (PointLineSegmentIntersects(x2, y2, x0, y0, x1, y1) || PointLineSegmentIntersects(x3, y3, x0, y0, x1, y1));

            // Find the index where the intersection point lies on the line.
            var s = ((x0 - x2) * v1 + (y2 - y0) * u1) / -determinant;
            var t = ((x2 - x0) * v2 + (y0 - y2) * u2) / determinant;

            // Check whether the point is on the segment.
            return (t >= 0d) && (t <= 1d) && (s >= 0d) && (s <= 1d);
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
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineSegmentRectangleIntersects(
            double a1X, double a1Y,
            double a2X, double a2Y,
            double r1X, double r1Y,
            double r2X, double r2Y,
            double epsilon = Epsilon)
        {
            var min = MinPoint(r1X, r1Y, r2X, r2Y);
            var max = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);
            if (LineSegmentLineSegmentIntersects(a1X, a1Y, a2X, a2Y, min.X, min.Y, topRight.X, topRight.Y, epsilon)) return true;
            if (LineSegmentLineSegmentIntersects(a1X, a1Y, a2X, a2Y, topRight.X, topRight.Y, max.X, max.Y, epsilon)) return true;
            if (LineSegmentLineSegmentIntersects(a1X, a1Y, a2X, a2Y, max.X, max.Y, bottomLeft.X, bottomLeft.Y, epsilon)) return true;
            if (LineSegmentLineSegmentIntersects(a1X, a1Y, a2X, a2Y, bottomLeft.X, bottomLeft.Y, min.X, min.Y, epsilon)) return true;
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
        /// <param name="epsilon"></param>
        /// <returns>Returns the point of intersection.</returns>
        /// <remarks>http://www.vb-helper.com/howto_segments_intersect.html</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineLineSegmentIntersects(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double epsilon = Epsilon)
        {
            // Translate lines to origin.
            var u1 = (x1 - x0);
            var v1 = (y1 - y0);
            var u2 = (x3 - x2);
            var v2 = (y3 - y2);

            // Calculate the determinant of the coefficient matrix.
            var determinant = (v2 * u1) - (u2 * v1);

            // Check if the line segments are parallel.
            if (Abs(determinant) < epsilon)
                // Return whether line segments are coincidental.
                return (PointLineSegmentIntersects(x2, y2, x0, y0, x1, y1) || PointLineSegmentIntersects(x3, y3, x0, y0, x1, y1));

            // Find the index where the intersection point lies on the line.
            var t = ((x0 - x2) * v1 + (y2 - y0) * u1) / -determinant;

            // Check whether the point is on the segment.
            return (t >= 0d) && (t <= 1d);
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
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LineRectangleIntersects(
            double a1X, double a1Y,
            double a2X, double a2Y,
            double r1X, double r1Y,
            double r2X, double r2Y,
            double epsilon = Epsilon)
        {
            var min = MinPoint(r1X, r1Y, r2X, r2Y);
            var max = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);
            if (LineLineSegmentIntersects(a1X, a1Y, a2X, a2Y, min.X, min.Y, topRight.X, topRight.Y, epsilon)) return true;
            if (LineLineSegmentIntersects(a1X, a1Y, a2X, a2Y, topRight.X, topRight.Y, max.X, max.Y, epsilon)) return true;
            if (LineLineSegmentIntersects(a1X, a1Y, a2X, a2Y, max.X, max.Y, bottomLeft.X, bottomLeft.Y, epsilon)) return true;
            if (LineLineSegmentIntersects(a1X, a1Y, a2X, a2Y, bottomLeft.X, bottomLeft.Y, min.X, min.Y, epsilon)) return true;
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
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/09/determine-where-two-circles-intersect-in-c/</remarks>
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
                return false;

            // Find the distance between the centers.
            var dx = cx0 - cx1;
            var dy = cy0 - cy1;
            var dist = Sqrt(dx * dx + dy * dy);

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

        #endregion

        #region Intersection Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p0X"></param>
        /// <param name="p0Y"></param>
        /// <param name="p1X"></param>
        /// <param name="p1Y"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection PointPointIntersection(double p0X, double p0Y, double p1X, double p1Y)
            => PointPointIntersects(p0X, p0Y, p1X, p1Y)
            ? new Intersection(IntersectionState.Intersection, new Point2D(p0X, p0Y))
            : new Intersection(IntersectionState.NoIntersection);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <param name="lAX"></param>
        /// <param name="lAY"></param>
        /// <param name="lBX"></param>
        /// <param name="lBY"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection PointLineSegmentIntersection(double pX, double pY, double lAX, double lAY, double lBX, double lBY)
            => (PointLineSegmentIntersects(pX, pY, lAX, lAY, lBX, lBY))
            ? new Intersection(IntersectionState.Intersection, new Point2D(pX, pY))
            : new Intersection(IntersectionState.NoIntersection);

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
        /// <param name="epsilon"></param>
        /// <returns>Returns the point of intersection.</returns>
        /// <remarks>http://www.vb-helper.com/howto_segments_intersect.html</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection LineLineIntersection(double x0, double y0, double x1, double y1, double x2, double y2, double x3, double y3, double epsilon = Epsilon)
        {
            // Initialize the intersection results.
            var result = new Intersection(IntersectionState.NoIntersection);

            // Translate lines to origin.
            var u1 = (x1 - x0);
            var v1 = (y1 - y0);
            var u2 = (x3 - x2);
            var v2 = (y3 - y2);

            // Calculate the determinant of the coefficient matrix.
            var determinant = (v2 * u1) - (u2 * v1);

            // Check if the lines are parallel.
            if (Abs(determinant) < epsilon)
                // Lines are parallel. There are an infinite number of intersections.
                // ToDo: Figure out how to return a line as an intersection.
                return result;

            // Find the index where the intersection point lies on the line.
            var t = ((x2 - x0) * v2 + (y0 - y2) * u2) / determinant;

            // Return the intersection point.
            result.AppendPoint(new Point2D(x0 + t * u1, y0 + t * v1));
            result.State |= IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// Find the intersection point between two line segments.
        /// </summary>
        /// <param name="l0X"></param>
        /// <param name="l0Y"></param>
        /// <param name="l1X"></param>
        /// <param name="l1Y"></param>
        /// <param name="s0X"></param>
        /// <param name="s0Y"></param>
        /// <param name="s1X"></param>
        /// <param name="s1Y"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection LineLineSegmentIntersection(double l0X, double l0Y, double l1X, double l1Y, double s0X, double s0Y, double s1X, double s1Y, double epsilon = Epsilon)
        {
            // Initialize the intersection result.
            var result = new Intersection(IntersectionState.Intersection);

            // Translate lines to origin.
            var u1 = l1X - l0X;
            var v1 = l1Y - l0Y;
            var u2 = s1X - s0X;
            var v2 = s1Y - s0Y;

            var ua = u2 * (l0Y - s0Y) - v2 * (l0X - s0X);
            var ub = u1 * (l0Y - s0Y) - v1 * (l0X - s0X);

            // Calculate the determinant of the coefficient matrix.
            var determinant = (v2 * u1) - (u2 * v1);

            // Check if the lines are parallel.
            if (Abs(determinant) < epsilon)
            {
                if (ua == 0 || ub == 0)
                {
                    // Line segment is coincident to the Line. There are an infinite number of intersections, but we only care about the start and end points of the line segment.
                    result.AppendPoints(new List<Point2D> { new Point2D(s0X, s0Y), new Point2D(s1X, s1Y) });
                    result.State |= IntersectionState.Coincident | IntersectionState.Parallel | IntersectionState.Intersection;
                }
                else
                {
                    // The Line and line segment are parallel. There are no intersections.
                    result.State |= IntersectionState.Parallel | IntersectionState.NoIntersection;
                }
            }
            else
            {
                // Find the index where the intersection point lies on the line.
                var ta = ua / determinant;
                var tb = ub / determinant;

                if (tb >= 0 && tb <= 1)
                {
                    // One intersection.
                    result.AppendPoint(new Point2D(l0X + ta * u1, l0Y + ta * v1));
                    result.State |= IntersectionState.Intersection;
                }
                else
                {
                    // The intersection is outside of the bounds of the Line segment. We can break early.
                    return result;
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="p0x"></param>
        /// <param name="p0y"></param>
        /// <param name="p1x"></param>
        /// <param name="p1y"></param>
        /// <param name="p2x"></param>
        /// <param name="p2y"></param>
        /// <returns></returns>
        /// <remarks>
        /// Adapted from code found at: https://www.particleincell.com/2013/cubic-line-intersection/
        /// Which was based off of code found at: http://stackoverflow.com/questions/14005096/mathematical-solution-for-bezier-curve-and-line-intersection-in-coffeescript-or
        /// Which was based off of code found at: http://www.blitzbasic.com/Community/posts.php?topic=64459
        /// </remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection LineQuadraticBezierIntersection(double x1, double y1, double x2, double y2, double p0x, double p0y, double p1x, double p1y, double p2x, double p2y)
        {
            // Initialize intersection.
            var result = new Intersection(IntersectionState.NoIntersection);

            // Translate the line to the origin.
            var A = y2 - y1;
            var B = x1 - x2;

            var C = x1 * (y1 - y2) + y1 * (x2 - x1);

            var bx = BezierCoefficients(p0x, p1x, p2x);
            var by = BezierCoefficients(p0y, p1y, p2y);

            List<double> roots;
            roots = QuadraticRoots(
                A * bx.A + B * by.A,    // t^2
                A * bx.B + B * by.B,    // t^1
                A * bx.C + B * by.C + C // 1
                );

            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];
                // Add intersection point.
                result.AppendPoint(new Point2D(
                    bx.A * t * t + bx.B * t + bx.C,
                    by.A * t * t + by.B * t + by.C));
            }

            // Return result.
            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="p0x"></param>
        /// <param name="p0y"></param>
        /// <param name="p1x"></param>
        /// <param name="p1y"></param>
        /// <param name="p2x"></param>
        /// <param name="p2y"></param>
        /// <param name="p3x"></param>
        /// <param name="p3y"></param>
        /// <returns></returns>
        /// <remarks>
        /// Adapted from code found at: https://www.particleincell.com/2013/cubic-line-intersection/
        /// Which was based off of code found at: http://stackoverflow.com/questions/14005096/mathematical-solution-for-bezier-curve-and-line-intersection-in-coffeescript-or
        /// Which was based off of code found at: http://www.blitzbasic.com/Community/posts.php?topic=64459
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection LineCubicBezierIntersection(double x1, double y1, double x2, double y2, double p0x, double p0y, double p1x, double p1y, double p2x, double p2y, double p3x, double p3y)
        {
            // Initialize the intersection.
            var result = new Intersection(IntersectionState.NoIntersection);

            // Translate the line to the origin.
            var A = y2 - y1;
            var B = x1 - x2;
            var C = x1 * (y1 - y2) + y1 * (x2 - x1);

            var bx = BezierCoefficients(p0x, p1x, p2x, p3x);
            var by = BezierCoefficients(p0y, p1y, p2y, p3y);

            List<double> roots;
            // Fix for missing intersections for curves that can be reduced to lower degrees.
            var determinant0 = (p3y - p0y) * (x2 - x1) - (p3x - p0x) * (y2 - y1);
            var determinant1 = (p2y - p1y) * (x2 - x1) - (p2x - p1x) * (y2 - y1);
            if (Abs(determinant0) < Epsilon && Abs(determinant1) < Epsilon)
                roots = QuadraticRoots(
                    A * bx.B + B * by.B,    // t^2
                    A * bx.C + B * by.C,    // t^1
                    A * bx.D + B * by.D + C // 1
                    );
            else
                roots = CubicRoots(
                    A * bx.A + B * by.A,    // t^3
                    A * bx.B + B * by.B,    // t^2
                    A * bx.C + B * by.C,    // t^1
                    A * bx.D + B * by.D + C // 1
                    );

            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];

                var x = bx.A * t * t * t + bx.B * t * t + bx.C * t + bx.D;
                var y = by.A * t * t * t + by.B * t * t + by.C * t + by.D;

                // Add intersection point.
                result.AppendPoint(new Point2D(x, y));
            }

            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection LinePolygonContourIntersection(double a1X, double a1Y, double a2X, double a2Y, List<Point2D> points)
        {
            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            var length = points.Count;

            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            Point2D b1 = points[length - 1];
            for (var i = 0; i < length; ++i)
            {
                Point2D b2 = points[i];

                intersections.UnionWith(LineLineSegmentIntersection(a1X, a1Y, a2X, a2Y, b1.X, b1.Y, b2.X, b2.Y).Points);

                b1 = b2;
            }

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection LineRectangleIntersection(double a1X, double a1Y, double a2X, double a2Y, double r1X, double r1Y, double r2X, double r2Y)
        {
            var min = MinPoint(r1X, r1Y, r2X, r2Y);
            var max = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);

            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            intersections.UnionWith(LineLineSegmentIntersection(a1X, a1Y, a2X, a2Y, min.X, min.Y, topRight.X, topRight.Y).Points);
            intersections.UnionWith(LineLineSegmentIntersection(a1X, a1Y, a2X, a2Y, topRight.X, topRight.Y, max.X, max.Y).Points);
            intersections.UnionWith(LineLineSegmentIntersection(a1X, a1Y, a2X, a2Y, max.X, max.Y, bottomLeft.X, bottomLeft.Y).Points);
            intersections.UnionWith(LineLineSegmentIntersection(a1X, a1Y, a2X, a2Y, bottomLeft.X, bottomLeft.Y, min.X, min.Y).Points);

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Count > 0)
                result.State = IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lAX"></param>
        /// <param name="lAY"></param>
        /// <param name="lBX"></param>
        /// <param name="lBY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection LineCircleIntersection(double lAX, double lAY, double lBX, double lBY, double cX, double cY, double r, double epsilon = Epsilon)
        {
            // Initialize the intersection result.
            var result = new Intersection(IntersectionState.NoIntersection);

            // If the circle or line segment are empty, return no intersections.
            if ((r == 0d) || ((lAX == lBX) && (lAY == lBY)))
                return result;

            // Translate the line to the origin. 
            var dx = lBX - lAX;
            var dy = lBY - lAY;

            // Calculate the quadratic parameters.
            var a = dx * dx + dy * dy;
            var b = 2 * (dx * (lAX - cX) + dy * (lAY - cY));
            var c = (lAX - cX) * (lAX - cX) + (lAY - cY) * (lAY - cY) - r * r;

            // Calculate the discriminant.
            var discriminant = b * b - 4 * a * c;

            if ((a <= epsilon) || (discriminant < 0))
            {
                // No real solutions.
                result.State |= IntersectionState.Outside;
                return result;
            }
            else if (discriminant == 0)
            {
                // One possible solution.
                var t = -b / (2 * a);

                // Add the points.
                result = new Intersection(IntersectionState.Intersection);
                result.AppendPoint(new Point2D(lAX + t * dx, lAY + t * dy));
            }
            else if (discriminant > 0)
            {
                // Two possible solutions.
                var t1 = ((-b + Sqrt(discriminant)) / (2 * a));
                var t2 = ((-b - Sqrt(discriminant)) / (2 * a));

                // Add the points.
                result = new Intersection(IntersectionState.Intersection);
                result.AppendPoint(new Point2D(lAX + t1 * dx, lAY + t1 * dy));
                result.AppendPoint(new Point2D(lAX + t2 * dx, lAY + t2 * dy));
            }

            // Return result.
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lAX"></param>
        /// <param name="lAY"></param>
        /// <param name="lBX"></param>
        /// <param name="lBY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="angle"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection LineCircularArcIntersection(double lAX, double lAY, double lBX, double lBY, double cX, double cY, double r, double angle, double startAngle, double sweepAngle, double epsilon = Epsilon)
        {
            // Initialize the intersection.
            var result = new Intersection(IntersectionState.NoIntersection);

            // If the circle or line segment are empty, return no intersections.
            if ((r == 0d) || ((lAX == lBX) && (lAY == lBY)))
                return result;

            // Translate the line to the origin. 
            var dx = lBX - lAX;
            var dy = lBY - lAY;

            // Calculate the quadratic parameters.
            var a = dx * dx + dy * dy;
            var b = 2 * (dx * (lAX - cX) + dy * (lAY - cY));
            var c = (lAX - cX) * (lAX - cX) + (lAY - cY) * (lAY - cY) - r * r;

            // Find the points of the chord.
            var startPoint = new Point2D(cX + (Cos(angle + startAngle) * r), cY + (Sin(angle + startAngle) * r));
            var endPoint = new Point2D(cX + (Cos(angle + startAngle + sweepAngle) * r), cY + (Sin(angle + startAngle + sweepAngle) * r));

            // Calculate the discriminant.
            var discriminant = b * b - 4 * a * c;

            // Check for intersections.
            if ((a <= epsilon) || (discriminant < 0))
            {
                // No real solutions.
                result.State |= IntersectionState.Outside;
                return result;
            }
            else if (discriminant == 0)
            {
                // One possible solution.
                var t = -b / (2 * a);

                // Find the point.
                var pX = lAX + t * dx;
                var pY = lAY + t * dy;

                // Find the determinant of the chord and point.
                var determinant = (startPoint.X - pX) * (endPoint.Y - pY) - (endPoint.X - pX) * (startPoint.Y - pY);

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle))
                {
                    // Add the point.
                    result.AppendPoint(new Point2D(pX, pY));
                }
            }
            else if (discriminant > 0)
            {
                // Two possible solutions.
                var t1 = ((-b + Sqrt(discriminant)) / (2 * a));
                var t2 = ((-b - Sqrt(discriminant)) / (2 * a));

                // Find the point.
                var pX = lAX + t1 * dx;
                var pY = lAY + t1 * dy;

                // Find the determinant of the chord and point.
                var determinant = (startPoint.X - pX) * (endPoint.Y - pY) - (endPoint.X - pX) * (startPoint.Y - pY);

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle))
                {
                    // Add the point.
                    result.AppendPoint(new Point2D(pX, pY));
                }

                // Find the point.
                pX = lAX + t2 * dx;
                pY = lAY + t2 * dy;

                // Find the determinant of the chord and point.
                determinant = (startPoint.X - pX) * (endPoint.Y - pY) - (endPoint.X - pX) * (startPoint.Y - pY);

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle))
                {
                    // Add the point.
                    result.AppendPoint(new Point2D(pX, pY));
                }
            }

            // Return the result.
            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="angle"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection LineEllipseIntersection(double x0, double y0, double x1, double y1, double cx, double cy, double rx, double ry, double angle, double epsilon = Epsilon)
            => LineEllipseIntersection(x0, y0, x1, y1, cx, cy, rx, ry, Cos(angle), Sin(angle), epsilon);

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="cosA"></param>
        /// <param name="sinA"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://csharphelper.com/blog/2012/09/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection LineEllipseIntersection(double x0, double y0, double x1, double y1, double cx, double cy, double rx, double ry, double cosA, double sinA, double epsilon = Epsilon)
        {
            // Initialize the resulting intersection structure.
            var result = new Intersection(IntersectionState.NoIntersection);

            // If the ellipse or line segment are empty, return no intersections.
            if ((rx == 0d) || (ry == 0d) ||
                ((x0 == x1) && (y0 == y1)))
                return result;

            // Translate the line to put the ellipse centered at the origin.
            var u1 = x0 - cx;
            var v1 = y0 - cy;
            var u2 = x1 - cx;
            var v2 = y1 - cy;

            // Apply Rotation Transform to line at the origin.
            var u1A = u1 * cosA - v1 * -sinA;
            var v1A = u1 * -sinA + v1 * cosA;
            var u2A = u2 * cosA - v2 * -sinA;
            var v2A = u2 * -sinA + v2 * cosA;

            // Calculate the quadratic parameters.
            var a = (u2A - u1A) * (u2A - u1A) / (rx * rx) + (v2A - v1A) * (v2A - v1A) / (ry * ry);
            var b = 2d * u1A * (u2A - u1A) / (rx * rx) + 2d * v1A * (v2A - v1A) / (ry * ry);
            var c = (u1A * u1A) / (rx * rx) + (v1A * v1A) / (ry * ry) - 1d;

            // Calculate the discriminant.
            var discriminant = b * b - 4d * a * c;

            // Find solutions.
            if ((a <= epsilon) || (discriminant < 0))
            {
                // No real solutions.
                result.State |= IntersectionState.Outside;
                return result;
            }
            else if (discriminant == 0)
            {
                // One real possible solution.
                var t = OneHalf * -b / a;

                // Add the point.
                result.AppendPoint(new Point2D(u1 + (u2 - u1) * t + cx, v1 + (v2 - v1) * t + cy));
            }
            else if (discriminant > 0)
            {
                // Two real possible solutions.
                var t1 = (OneHalf * (-b + Sqrt(discriminant)) / a);
                var t2 = (OneHalf * (-b - Sqrt(discriminant)) / a);

                // Add the points.
                result.AppendPoint(new Point2D(u1 + (u2 - u1) * t1 + cx, v1 + (v2 - v1) * t1 + cy));
                result.AppendPoint(new Point2D(u1 + (u2 - u1) * t2 + cx, v1 + (v2 - v1) * t2 + cy));
            }

            // Return the intersections.
            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="angle"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection LineEllipticalArcIntersection(double x0, double y0, double x1, double y1, double cx, double cy, double rx, double ry, double angle, double startAngle, double sweepAngle, double epsilon = Epsilon)
            => EllipseLineSegmentIntersection(cx, cy, rx, ry, Cos(angle), Sin(angle), x0, y0, x1, y1, epsilon);

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="sinA"></param>
        /// <param name="cosA"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://csharphelper.com/blog/2012/09/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection LineEllipticalArcIntersection(double x0, double y0, double x1, double y1, double cx, double cy, double rx, double ry, double cosA, double sinA, double startAngle, double sweepAngle, double epsilon = Epsilon)
        {
            // Initialize the resulting intersection structure.
            var result = new Intersection(IntersectionState.NoIntersection);

            // If the ellipse or line segment are empty, return no intersections.
            if ((sweepAngle == 0d) || (rx == 0d) || (ry == 0d) ||
                ((x0 == x1) && (y0 == y1)))
                return result;

            // Translate the line to put it at the ellipse centered at the origin.
            var u0 = x0 - cx;
            var v0 = y0 - cy;
            var u1 = x1 - cx;
            var v1 = y1 - cy;

            // Apply the rotation transformation to line at the origin.
            var u0A = u0 * cosA - v0 * -sinA;
            var v0A = u0 * -sinA + v0 * cosA;
            var u1A = u1 * cosA - v1 * -sinA;
            var v1A = u1 * -sinA + v1 * cosA;

            // Calculate the quadratic parameters.
            var a = (u1A - u0A) * (u1A - u0A) / (rx * rx) + (v1A - v0A) * (v1A - v0A) / (ry * ry);
            var b = 2d * u0A * (u1A - u0A) / (rx * rx) + 2d * v0A * (v1A - v0A) / (ry * ry);
            var c = (u0A * u0A) / (rx * rx) + (v0A * v0A) / (ry * ry) - 1d;

            // Calculate the discriminant of the quadratic.
            var discriminant = b * b - 4d * a * c;

            // Check whether line segment is outside of the ellipse.
            if ((a <= epsilon) || (discriminant < 0))
            {
                // No real solutions.
                result.State |= IntersectionState.Outside;
                return result;
            }

            // Find the corrected start and end angles.
            var sa = EllipticalPolarAngle(startAngle, rx, ry);
            var ea = EllipticalPolarAngle(startAngle + sweepAngle, rx, ry);

            // Ellipse equation for an ellipse at origin; for the chord end points.
            var usa = rx * Cos(sa);
            var vsa = -(ry * Sin(sa));
            var uea = rx * Cos(ea);
            var vea = -(ry * Sin(ea));

            // Apply the rotation and translation transformations to find the chord points.
            var sx = cx + (usa * cosA + vsa * sinA);
            var sy = cy + (usa * sinA - vsa * cosA);
            var ex = cx + (uea * cosA + vea * sinA);
            var ey = cy + (uea * sinA - vea * cosA);

            if (discriminant == 0)
            {
                // One real possible solution.
                var t = OneHalf * -b / a;

                // Find the point.
                var p = new Point2D(u0 + (u1 - u0) * t + cx, v0 + (v1 - v0) * t + cy);

                // Find the determinant of the matrix representing the chord.
                var determinant = (sx - p.X) * (ey - p.Y) - (ex - p.X) * (sy - p.Y);

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle))
                    result.AppendPoint(p);
            }
            else if (discriminant > 0)
            {
                // Two real possible solutions.
                var root = Sqrt(discriminant);
                var t1 = (OneHalf * (-b + root) / a);
                var t2 = (OneHalf * (-b - root) / a);

                // Find the point.
                var p = new Point2D(u0 + (u1 - u0) * t1 + cx, v0 + (v1 - v0) * t1 + cy);

                // Find the determinant of the matrix representing the chord.
                var determinant = (sx - p.X) * (ey - p.Y) - (ex - p.X) * (sy - p.Y);

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle))
                    result.AppendPoint(p);
                // Find the point.
                p = new Point2D(u0 + (u1 - u0) * t2 + cx, v0 + (v1 - v0) * t2 + cy);

                // Find the determinant of the matrix representing the chord.
                determinant = (sx - p.X) * (ey - p.Y) - (ex - p.X) * (sy - p.Y);

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle))
                    result.AppendPoint(p);
            }

            // Return the intersections.
            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
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
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection RayRayIntersection(
            double a1X, double a1Y,
            double a2X, double a2Y,
            double b1X, double b1Y,
            double b2X, double b2Y,
            double epsilon = Epsilon)
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
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="b1X"></param>
        /// <param name="b1Y"></param>
        /// <param name="b2X"></param>
        /// <param name="b2Y"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection LineSegmentLineSegmentIntersection(double x1, double y1, double x2, double y2, double b1X, double b1Y, double b2X, double b2Y, double epsilon = Epsilon)
        {
            Intersection result;
            var ua_t = (b2X - b1X) * (y1 - b1Y) - (b2Y - b1Y) * (x1 - b1X);
            var ub_t = (x2 - x1) * (y1 - b1Y) - (y2 - y1) * (x1 - b1X);
            var u_b = (b2Y - b1Y) * (x2 - x1) - (b2X - b1X) * (y2 - y1);
            if (u_b != 0)
            {
                var ua = ua_t / u_b;
                var ub = ub_t / u_b;
                if (0 <= ua && ua <= 1 && 0 <= ub && ub <= 1)
                {
                    result = new Intersection(IntersectionState.Intersection);
                    result.AppendPoint(new Point2D(x1 + ua * (x2 - x1), y1 + ua * (y2 - y1)));
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
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="p0x"></param>
        /// <param name="p0y"></param>
        /// <param name="p1x"></param>
        /// <param name="p1y"></param>
        /// <param name="p2x"></param>
        /// <param name="p2y"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks>
        /// Adapted from code found at: https://www.particleincell.com/2013/cubic-line-intersection/
        /// Which was based off of code found at: http://stackoverflow.com/questions/14005096/mathematical-solution-for-bezier-curve-and-line-intersection-in-coffeescript-or
        /// Which was based off of code found at: http://www.blitzbasic.com/Community/posts.php?topic=64459
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection LineSegmentQuadraticBezierIntersection(double x1, double y1, double x2, double y2, double p0x, double p0y, double p1x, double p1y, double p2x, double p2y, double epsilon = Epsilon)
        {
            // Initialize the intersection.
            var result = new Intersection(IntersectionState.NoIntersection);

            // Translate the line to the origin.
            var A = y2 - y1;
            var B = x1 - x2;

            var C = x1 * (y1 - y2) + y1 * (x2 - x1);

            var bx = BezierCoefficients(p0x, p1x, p2x);
            var by = BezierCoefficients(p0y, p1y, p2y);

            List<double> roots;
            roots = QuadraticRoots(
                A * bx.A + B * by.A,    // t^2
                A * bx.B + B * by.B,    // t^1
                A * bx.C + B * by.C + C // 1
                );

            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];

                // Intersection point assuming it was an infinitely long line.
                var x = bx.A * t * t + bx.B * t + bx.C;
                var y = by.A * t * t + by.B * t + by.C;

                double m;
                // Special handling for vertical lines.
                if ((x2 - x1) != 0)
                    m = (x - x1) / (x2 - x1);
                else
                    m = (y - y1) / (y2 - y1);

                // Make sure we are in bounds of the line segment.
                if (!(t < 0 || t > 1d || m < 0 || m > 1d))
                {
                    // Add intersection point.
                    result.AppendPoint(new Point2D(x, y));
                }
            }

            // Return the result.
            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="p0x"></param>
        /// <param name="p0y"></param>
        /// <param name="p1x"></param>
        /// <param name="p1y"></param>
        /// <param name="p2x"></param>
        /// <param name="p2y"></param>
        /// <param name="p3x"></param>
        /// <param name="p3y"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks>
        /// Adapted from code found at: https://www.particleincell.com/2013/cubic-line-intersection/
        /// Which was based off of code found at: http://stackoverflow.com/questions/14005096/mathematical-solution-for-bezier-curve-and-line-intersection-in-coffeescript-or
        /// Which was based off of code found at: http://www.blitzbasic.com/Community/posts.php?topic=64459
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection LineSegmentCubicBezierIntersection(double x1, double y1, double x2, double y2, double p0x, double p0y, double p1x, double p1y, double p2x, double p2y, double p3x, double p3y, double epsilon = Epsilon)
        {
            // Initialize the intersection.
            var result = new Intersection(IntersectionState.NoIntersection);

            // Translate the line to the origin.
            var A = y2 - y1;
            var B = x1 - x2;

            var C = x1 * (y1 - y2) + y1 * (x2 - x1);

            var bx = BezierCoefficients(p0x, p1x, p2x, p3x);
            var by = BezierCoefficients(p0y, p1y, p2y, p3y);

            List<double> roots;
            // Fix for missing intersections for curves that can be reduced to lower degrees.
            var determinant0 = (p3y - p0y) * (x2 - x1) - (p3x - p0x) * (y2 - y1);
            var determinant1 = (p2y - p1y) * (x2 - x1) - (p2x - p1x) * (y2 - y1);
            if (Abs(determinant0) < epsilon && Abs(determinant1) < epsilon)
                roots = QuadraticRoots(
                    A * bx.B + B * by.B,    // t^2
                    A * bx.C + B * by.C,    // t^1
                    A * bx.D + B * by.D + C // 1
                    );
            else
                roots = CubicRoots(
                    A * bx.A + B * by.A,    // t^3
                    A * bx.B + B * by.B,    // t^2
                    A * bx.C + B * by.C,    // t^1
                    A * bx.D + B * by.D + C // 1
                    );

            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];

                // Intersection point assuming infinitely long line segment.
                var x = bx.A * t * t * t + bx.B * t * t + bx.C * t + bx.D;
                var y = by.A * t * t * t + by.B * t * t + by.C * t + by.D;

                double m;
                // Special handling for vertical lines.
                if ((x2 - x1) != 0)
                    m = (x - x1) / (x2 - x1);
                else
                    m = (y - y1) / (y2 - y1);

                // Make sure we are in bounds of the line segment.
                if (!(t < 0 || t > 1d || m < 0 || m > 1d))
                {
                    // Add intersection point.
                    result.AppendPoint(new Point2D(x, y));
                }
            }

            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lAX"></param>
        /// <param name="lAY"></param>
        /// <param name="lBX"></param>
        /// <param name="lBY"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection LineSegmentRectangleIntersection(double lAX, double lAY, double lBX, double lBY, double r1X, double r1Y, double r2X, double r2Y, double epsilon = Epsilon)
        {
            var topLeft = MinPoint(r1X, r1Y, r2X, r2Y);
            var bottomRight = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(bottomRight.X, topLeft.Y);
            var bottomLeft = new Point2D(topLeft.X, bottomRight.Y);

            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            intersections.UnionWith(LineSegmentLineSegmentIntersection(topLeft.X, topLeft.Y, topRight.X, topRight.Y, lAX, lAY, lBX, lBY, epsilon).Points);
            intersections.UnionWith(LineSegmentLineSegmentIntersection(topRight.X, topRight.Y, bottomRight.X, bottomRight.Y, lAX, lAY, lBX, lBY, epsilon).Points);
            intersections.UnionWith(LineSegmentLineSegmentIntersection(bottomRight.X, bottomRight.Y, bottomLeft.X, bottomLeft.Y, lAX, lAY, lBX, lBY, epsilon).Points);
            intersections.UnionWith(LineSegmentLineSegmentIntersection(bottomLeft.X, bottomLeft.Y, topLeft.X, topLeft.Y, lAX, lAY, lBX, lBY, epsilon).Points);

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Count > 0)
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
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection LineSegmentPolygonContourIntersection(double a1X, double a1Y, double a2X, double a2Y, List<Point2D> points, double epsilon = Epsilon)
        {
            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            var length = points.Count;

            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            Point2D b1 = points[length - 1];
            for (var i = 0; i < length; ++i)
            {
                Point2D b2 = points[i];

                intersections.UnionWith(LineSegmentLineSegmentIntersection(b1.X, b1.Y, b2.X, b2.Y, a1X, a1Y, a2X, a2Y, epsilon).Points);

                b1 = b2;
            }

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
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
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection PolygonRectangleIntersection(List<Point2D> points, double r1X, double r1Y, double r2X, double r2Y, double epsilon = Epsilon)
        {
            var min = MinPoint(r1X, r1Y, r2X, r2Y);
            var max = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);

            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            intersections.UnionWith(LineSegmentPolygonContourIntersection(min.X, min.Y, topRight.X, topRight.Y, points, epsilon).Points);
            intersections.UnionWith(LineSegmentPolygonContourIntersection(topRight.X, topRight.Y, max.X, max.Y, points, epsilon).Points);
            intersections.UnionWith(LineSegmentPolygonContourIntersection(max.X, max.Y, bottomLeft.X, bottomLeft.Y, points, epsilon).Points);
            intersections.UnionWith(LineSegmentPolygonContourIntersection(bottomLeft.X, bottomLeft.Y, min.X, min.Y, points, epsilon).Points);

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Points.Count > 0)
                result.State = IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points1"></param>
        /// <param name="points2"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection PolygonPolygonIntersection(List<Point2D> points1, List<Point2D> points2, double epsilon = Epsilon)
        {
            var intersections = new HashSet<Point2D>();
            var length = points1.Count;

            // We shouldn't care about the ordering, we can start with the last segment for a slight performance boost.
            Point2D a1 = points1[length - 1];
            for (var i = 0; i < length; ++i)
            {
                Point2D a2 = points1[i];

                intersections.UnionWith(LineSegmentPolygonContourIntersection(a1.X, a1.Y, a2.X, a2.Y, points2).Points);

                a1 = a2;
            }

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Points.Count > 0)
                result.State |= IntersectionState.Intersection;
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
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection RectangleRectangleIntersection(double a1X, double a1Y, double a2X, double a2Y, double b1X, double b1Y, double b2X, double b2Y, double epsilon = Epsilon)
        {
            var min = MinPoint(a1X, a1Y, a2X, a2Y);
            var max = MaxPoint(a1X, a1Y, a2X, a2Y);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);

            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            intersections.UnionWith(LineSegmentRectangleIntersection(min.X, min.Y, topRight.X, topRight.Y, b1X, b1Y, b2X, b2Y).Points);
            intersections.UnionWith(LineSegmentRectangleIntersection(topRight.X, topRight.Y, max.X, max.Y, b1X, b1Y, b2X, b2Y).Points);
            intersections.UnionWith(LineSegmentRectangleIntersection(max.X, max.Y, bottomLeft.X, bottomLeft.Y, b1X, b1Y, b2X, b2Y).Points);
            intersections.UnionWith(LineSegmentRectangleIntersection(bottomLeft.X, bottomLeft.Y, min.X, min.Y, b1X, b1Y, b2X, b2Y).Points);

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Points.Count > 0)
                result.State = IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="lAX"></param>
        /// <param name="lAY"></param>
        /// <param name="lBX"></param>
        /// <param name="lBY"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection CircleLineSegmentIntersection(
            double cX, double cY,
            double r,
            double lAX, double lAY,
            double lBX, double lBY,
            double epsilon = Epsilon)
        {
            Intersection result;

            var a = (lBX - lAX) * (lBX - lAX) + (lBY - lAY) * (lBY - lAY);
            var b = 2 * ((lBX - lAX) * (lAX - cX) + (lBY - lAY) * (lAY - cY));
            var c = cX * cX + cY * cY + lAX * lAX + lAY * lAY - 2 * (cX * lAX + cY * lAY) - r * r;

            var determinant = b * b - 4 * a * c;
            if (determinant < 0)
            {
                result = new Intersection(IntersectionState.Outside);
            }
            else if (determinant == 0)
            {
                result = new Intersection(IntersectionState.Tangent | IntersectionState.Intersection);
                var u1 = (-b) / (2 * a);
                if (u1 < 0 || u1 > 1)
                {
                    if ((u1 < 0) || (u1 > 1))
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
                        result.Points.Add(Lerp(lAX, lAY, lBX, lBY, u1));
                }
            }
            else
            {
                var e = Sqrt(determinant);
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
                        result.Points.Add(Lerp(lAX, lAY, lBX, lBY, u1));
                    if (0 <= u2 && u2 <= 1)
                        result.Points.Add(Lerp(lAX, lAY, lBX, lBY, u2));
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
        /// <param name="angle"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="lAX"></param>
        /// <param name="lAY"></param>
        /// <param name="lBX"></param>
        /// <param name="lBY"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection CircularArcLineSegmentIntersection(
            double cX, double cY,
            double r,
            double angle,
            double startAngle, double sweepAngle,
            double lAX, double lAY,
            double lBX, double lBY,
            double epsilon = Epsilon)
        {
            var result = new Intersection(IntersectionState.NoIntersection);

            // If the circle or line segment are empty, return no intersections.
            if ((r == 0d) || ((lAX == lBX) && (lAY == lBY)))
                return result;

            var dx = lBX - lAX;
            var dy = lBY - lAY;

            // Calculate the quadratic parameters.
            var a = dx * dx + dy * dy;
            var b = 2 * (dx * (lAX - cX) + dy * (lAY - cY));
            var c = (lAX - cX) * (lAX - cX) + (lAY - cY) * (lAY - cY) - r * r;

            // Find the points of the chord.
            Point2D startPoint = Interpolators.CircularArc(cX, cY, r, startAngle, sweepAngle, 0);
            Point2D endPoint = Interpolators.CircularArc(cX, cY, r, startAngle, sweepAngle, 1);

            // Calculate the discriminant.
            var discriminant = b * b - 4 * a * c;

            if ((a <= epsilon) || (discriminant < 0))
            {
                // No real solutions.
            }
            else if (discriminant == 0)
            {
                // One possible solution.
                var t = -b / (2 * a);

                // Find the point.
                var pX = lAX + t * dx;
                var pY = lAY + t * dy;

                // Find the determinant of the chord and point.
                var determinant = (startPoint.X - pX) * (endPoint.Y - pY) - (endPoint.X - pX) * (startPoint.Y - pY);

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle) && (0 <= t && t <= 1))
                {
                    // Add the point.
                    result.AppendPoint(new Point2D(pX, pY));
                }
            }
            else if (discriminant > 0)
            {
                // Two possible solutions.
                var t1 = ((-b + Sqrt(discriminant)) / (2 * a));
                var t2 = ((-b - Sqrt(discriminant)) / (2 * a));

                // Find the point.
                var pX = lAX + t1 * dx;
                var pY = lAY + t1 * dy;

                // Find the determinant of the chord and point.
                var determinant = (startPoint.X - pX) * (endPoint.Y - pY) - (endPoint.X - pX) * (startPoint.Y - pY);

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle) && (0 <= t1 && t1 <= 1))
                {
                    // Add the point.
                    result.AppendPoint(new Point2D(pX, pY));
                }

                // Find the point.
                pX = lAX + t2 * dx;
                pY = lAY + t2 * dy;

                // Find the determinant of the chord and point.
                determinant = (startPoint.X - pX) * (endPoint.Y - pY) - (endPoint.X - pX) * (startPoint.Y - pY);

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle) && (0 <= t2 && t2 <= 1))
                {
                    // Add the point.
                    result.AppendPoint(new Point2D(pX, pY));
                }
            }

            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection CircleRectangleIntersection(
            double cX, double cY,
            double r,
            double r1X, double r1Y,
            double r2X, double r2Y)
        {
            var min = MinPoint(r1X, r1Y, r2X, r2Y);
            var max = MinPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);

            var result = new Intersection(IntersectionState.NoIntersection);
            result.AppendPoints(CircleLineSegmentIntersection(cX, cY, r, min.X, min.Y, topRight.X, topRight.Y).Points);
            result.AppendPoints(CircleLineSegmentIntersection(cX, cY, r, topRight.X, topRight.Y, max.X, max.Y).Points);
            result.AppendPoints(CircleLineSegmentIntersection(cX, cY, r, max.X, max.Y, bottomLeft.X, bottomLeft.Y).Points);
            result.AppendPoints(CircleLineSegmentIntersection(cX, cY, r, bottomLeft.X, bottomLeft.Y, min.X, min.Y).Points);

            if (result.Points.Count > 0)
                result.State = IntersectionState.Intersection;
            else result.State = CircleLineSegmentIntersection(cX, cY, r, min.X, min.Y, topRight.X, topRight.Y).State;
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection CirclePolygonIntersection(
            double cX, double cY,
            double r,
            List<Point2D> points)
        {
            var result = new Intersection(IntersectionState.NoIntersection);
            var inter = new Intersection(IntersectionState.NoIntersection);
            var length = points.Count;

            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            Point2D a1 = points[length - 1];
            for (var i = 0; i < length; ++i)
            {
                Point2D a2 = points[i];

                inter = CircleLineSegmentIntersection(cX, cY, r, a1.X, a1.Y, a2.X, a2.Y);
                result.AppendPoints(inter.Points);

                a1 = a2;
            }

            if (result.Points.Count > 0)
                result.State |= IntersectionState.Intersection;
            else
                result.State = inter.State;
            return result;
        }

        /// <summary>
        /// Find intersection ff two circles.
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
        private static Intersection CircleCircleIntersection1(
            double cx0, double cy0,
            double radius0,
            double cx1, double cy1,
            double radius1)
        {
            var result = new Intersection(IntersectionState.NoIntersection);

            // If either of the circles are empty, return no intersections.
            if ((radius0 == 0d) || (radius1 == 0d))
                return result;

            // Find the distance between the centers.
            var dx = cx0 - cx1;
            var dy = cy0 - cy1;
            var dist = Sqrt(dx * dx + dy * dy);

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
                var a = (radius0 * radius0 - radius1 * radius1 + dist * dist) / (2 * dist);
                var h = Sqrt(radius0 * radius0 - a * a);

                // Find P2.
                var cx2 = cx0 + a * (cx1 - cx0) / dist;
                var cy2 = cy0 + a * (cy1 - cy0) / dist;

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
        /// Find intersection ff two circles.
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
        private static Intersection CircleCircleIntersection(
            double c1X, double c1Y,
            double r1,
            double c2X, double c2Y,
            double r2)
        {
            var result = new Intersection(IntersectionState.NoIntersection);
            var r_max = r1 + r2;
            var r_min = Abs(r1 - r2);
            var c_dist = Distance(c1X, c1Y, c2X, c2Y);
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
        /// Find the points of the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="angle"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection EllipticalArcLineSegmentIntersection(double cx, double cy, double rx, double ry, double angle, double startAngle, double sweepAngle, double x0, double y0, double x1, double y1, double epsilon = Epsilon)
            => EllipseLineSegmentIntersection(cx, cy, rx, ry, Cos(angle), Sin(angle), x0, y0, x1, y1, epsilon);

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="sinA"></param>
        /// <param name="cosA"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://csharphelper.com/blog/2012/09/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection EllipticalArcLineSegmentIntersection(double cx, double cy, double rx, double ry, double cosA, double sinA, double startAngle, double sweepAngle, double x0, double y0, double x1, double y1, double epsilon = Epsilon)
        {
            // Initialize the resulting intersection structure.
            var result = new Intersection(IntersectionState.NoIntersection);

            // If the ellipse or line segment are empty, return no intersections.
            if ((sweepAngle == 0d) || (rx == 0d) || (ry == 0d) ||
                ((x0 == x1) && (y0 == y1)))
                return result;

            // Translate the line to move it to the ellipse centered at the origin.
            var u0 = x0 - cx;
            var v0 = y0 - cy;
            var u1 = x1 - cx;
            var v1 = y1 - cy;

            // Apply Rotation Transform to line at the origin to align it with the unrotated ellipse.
            var u0A = u0 * cosA - v0 * -sinA;
            var v0A = u0 * -sinA + v0 * cosA;
            var u1A = u1 * cosA - v1 * -sinA;
            var v1A = u1 * -sinA + v1 * cosA;

            // Calculate the quadratic parameters.
            var a = (u1A - u0A) * (u1A - u0A) / (rx * rx) + (v1A - v0A) * (v1A - v0A) / (ry * ry);
            var b = 2d * u0A * (u1A - u0A) / (rx * rx) + 2d * v0A * (v1A - v0A) / (ry * ry);
            var c = (u0A * u0A) / (rx * rx) + (v0A * v0A) / (ry * ry) - 1d;

            // Calculate the discriminant of the quadratic.
            var discriminant = b * b - 4d * a * c;

            // Check whether line segment is outside of the ellipse.
            if ((a <= epsilon) || (discriminant < 0))
            {
                // No real solutions.
                result.State |= IntersectionState.Outside;
                return result;
            }

            // Find the corrected start and end angles.
            var sa = EllipticalPolarAngle(startAngle, rx, ry);
            var ea = EllipticalPolarAngle(startAngle + sweepAngle, rx, ry);

            // Ellipse equation for an ellipse at origin.
            var usa = rx * Cos(sa);
            var vsa = -(ry * Sin(sa));
            var uea = rx * Cos(ea);
            var vea = -(ry * Sin(ea));

            // Apply the rotation transformation to find the chord points.
            var sx = cx + (usa * cosA + vsa * sinA);
            var sy = cy + (usa * sinA - vsa * cosA);
            var ex = cx + (uea * cosA + vea * sinA);
            var ey = cy + (uea * sinA - vea * cosA);

            if (discriminant == 0)
            {
                // One real possible solution.
                var t = OneHalf * -b / a;

                // Add the point if it is between the end points of the line segment.
                if (t >= 0d && t <= 1d)
                {
                    // Find the point.
                    var p = new Point2D(u0 + (u1 - u0) * t + cx, v0 + (v1 - v0) * t + cy);

                    // Find the determinant of the matrix representing the chord.
                    var determinant = (sx - p.X) * (ey - p.Y) - (ex - p.X) * (sy - p.Y);

                    // Add the point if it is on the sweep side of the chord.
                    if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle))
                        result.AppendPoint(p);
                }
            }
            else if (discriminant > 0)
            {
                // Two real possible solutions.
                var root = Sqrt(discriminant);
                var t1 = (OneHalf * (-b + root) / a);
                var t2 = (OneHalf * (-b - root) / a);

                // Add the points if they are between the end points of the line segment.
                if ((t1 >= 0d) && (t1 == 1d))
                {
                    // Find the point.
                    var p = new Point2D(u0 + (u1 - u0) * t1 + cx, v0 + (v1 - v0) * t1 + cy);

                    // Find the determinant of the matrix representing the chord.
                    var determinant = (sx - p.X) * (ey - p.Y) - (ex - p.X) * (sy - p.Y);

                    // Add the point if it is on the sweep side of the chord.
                    if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle))
                        result.AppendPoint(p);
                }

                if ((t2 >= 0d) && (t2 <= 1d))
                {
                    // Find the point.
                    var p = new Point2D(u0 + (u1 - u0) * t2 + cx, v0 + (v1 - v0) * t2 + cy);

                    // Find the determinant of the matrix representing the chord.
                    var determinant = (sx - p.X) * (ey - p.Y) - (ex - p.X) * (sy - p.Y);

                    // Add the point if it is on the sweep side of the chord.
                    if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle))
                        result.AppendPoint(p);
                }
            }

            // Return the intersections.
            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
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
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection EllipseLineSegmentIntersection(double cx, double cy, double rx, double ry, double angle, double x0, double y0, double x1, double y1, double epsilon = Epsilon)
            => EllipseLineSegmentIntersection(cx, cy, rx, ry, Cos(angle), Sin(angle), x0, y0, x1, y1, epsilon);

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="cosA"></param>
        /// <param name="sinA"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://csharphelper.com/blog/2012/09/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection EllipseLineSegmentIntersection(double cx, double cy, double rx, double ry, double cosA, double sinA, double x0, double y0, double x1, double y1, double epsilon = Epsilon)
        {
            // Initialize the resulting intersection structure.
            var result = new Intersection(IntersectionState.NoIntersection);

            // If the ellipse or line segment are empty, return no intersections.
            if ((rx == 0d) || (ry == 0d) ||
                ((x0 == x1) && (y0 == y1)))
                return result;

            // Translate the line to put the ellipse centered at the origin.
            var u1 = x0 - cx;
            var v1 = y0 - cy;
            var u2 = x1 - cx;
            var v2 = y1 - cy;

            // Apply Rotation Transform to line at the origin.
            var u1A = (0 + (u1 * cosA - v1 * -sinA));
            var v1A = (0 + (u1 * -sinA + v1 * cosA));
            var u2A = (0 + (u2 * cosA - v2 * -sinA));
            var v2A = (0 + (u2 * -sinA + v2 * cosA));

            // Calculate the quadratic parameters.
            var a = (u2A - u1A) * (u2A - u1A) / (rx * rx) + (v2A - v1A) * (v2A - v1A) / (ry * ry);
            var b = 2d * u1A * (u2A - u1A) / (rx * rx) + 2d * v1A * (v2A - v1A) / (ry * ry);
            var c = (u1A * u1A) / (rx * rx) + (v1A * v1A) / (ry * ry) - 1d;

            // Calculate the discriminant.
            var discriminant = b * b - 4d * a * c;

            // Find solutions.
            if ((a <= epsilon) || (discriminant < 0))
            {
                // No real solutions.
                result.State |= IntersectionState.Outside;
                return result;
            }
            else if (discriminant == 0)
            {
                // One real possible solution.
                var t = 0.5d * -b / a;

                // Add the point if it is between the end points of the line segment.
                if ((t >= 0d) && (t <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + (u2 - u1) * t + cx, v1 + (v2 - v1) * t + cy));
                }

            }
            else if (discriminant > 0)
            {
                // Two real possible solutions.
                var t1 = (0.5d * (-b + Sqrt(discriminant)) / a);
                var t2 = (0.5d * (-b - Sqrt(discriminant)) / a);

                // Add the points if they are between the end points of the line segment.
                if ((t1 >= 0d) && (t1 <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + (u2 - u1) * t1 + cx, v1 + (v2 - v1) * t1 + cy));
                }

                if ((t2 >= 0d) && (t2 <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + (u2 - u1) * t2 + cx, v1 + (v2 - v1) * t2 + cy));
                }
            }

            // Return the intersections.
            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="angle"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection EllipseRectangleIntersection(double cx, double cy, double rx, double ry, double angle, double r1X, double r1Y, double r2X, double r2Y, double epsilon = Epsilon)
            => EllipseRectangleIntersection(cx, cy, rx, ry, Cos(angle), Sin(angle), r1X, r1Y, r2X, r2Y, epsilon);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="cosA"></param>
        /// <param name="sinA"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection EllipseRectangleIntersection(double cX, double cY, double rx, double ry, double cosA, double sinA, double r1X, double r1Y, double r2X, double r2Y, double epsilon = Epsilon)
        {
            var min = MinPoint(r1X, r1Y, r2X, r2Y);
            var max = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);

            var result = new Intersection(IntersectionState.NoIntersection);
            result.AppendPoints(EllipseRectangleIntersection(cX, cY, rx, ry, cosA, sinA, min.X, min.Y, topRight.X, topRight.Y, epsilon).Points);
            result.AppendPoints(EllipseRectangleIntersection(cX, cY, rx, ry, cosA, sinA, topRight.X, topRight.Y, max.X, max.Y, epsilon).Points);
            result.AppendPoints(EllipseRectangleIntersection(cX, cY, rx, ry, cosA, sinA, max.X, max.Y, bottomLeft.X, bottomLeft.Y, epsilon).Points);
            result.AppendPoints(EllipseRectangleIntersection(cX, cY, rx, ry, cosA, sinA, bottomLeft.X, bottomLeft.Y, min.X, min.Y, epsilon).Points);

            if (result.Points.Count > 0)
                result.State = IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="angle"></param>
        /// <param name="points"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection EllipsePolygonIntersection(double cx, double cy, double rx, double ry, double angle, List<Point2D> points, double epsilon = Epsilon)
            => EllipsePolygonIntersection(cx, cy, rx, ry, Cos(angle), Sin(angle), points, epsilon);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="cosA"></param>
        /// <param name="sinA"></param>
        /// <param name="points"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection EllipsePolygonIntersection(double cX, double cY, double rx, double ry, double cosA, double sinA, List<Point2D> points, double epsilon = Epsilon)
        {
            var result = new Intersection(IntersectionState.NoIntersection);
            var inter = new Intersection(IntersectionState.NoIntersection);
            var length = points.Count;

            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            Point2D b1 = points[length - 1];
            for (var i = 0; i < points.Count; ++i)
            {
                Point2D b2 = points[i];

                inter = EllipseLineSegmentIntersection(cX, cY, rx, ry, cosA, sinA, b1.X, b1.Y, b2.X, b2.Y, epsilon);
                result.AppendPoints(inter.Points);

                b1 = b2;
            }

            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            else
                result.State = inter.State;
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
        private static Intersection UnrotatedEllipseUnrotatedEllipseIntersection(
            double c1X, double c1Y,
            double rx1, double ry1,
            double c2X, double c2Y,
            double rx2, double ry2)
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection CubicBezierRectangleIntersection(
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

            var result = new Intersection(IntersectionState.NoIntersection);
            result.AppendPoints(LineSegmentCubicBezierIntersection(p1X, p1Y, p2X, p2Y, p3X, p3Y, p4X, p4Y, min.X, min.Y, topRight.X, topRight.Y).Points);
            result.AppendPoints(LineSegmentCubicBezierIntersection(p1X, p1Y, p2X, p2Y, p3X, p3Y, p4X, p4Y, topRight.X, topRight.Y, max.X, max.Y).Points);
            result.AppendPoints(LineSegmentCubicBezierIntersection(p1X, p1Y, p2X, p2Y, p3X, p3Y, p4X, p4Y, max.X, max.Y, bottomLeft.X, bottomLeft.Y).Points);
            result.AppendPoints(LineSegmentCubicBezierIntersection(p1X, p1Y, p2X, p2Y, p3X, p3Y, p4X, p4Y, bottomLeft.X, bottomLeft.Y, min.X, min.Y).Points);

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection CubicBezierPolygonIntersection(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            double p4X, double p4Y,
            List<Point2D> points)
        {
            var result = new Intersection(IntersectionState.NoIntersection);
            var length = points.Count;

            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            Point2D a1 = points[length - 1];
            for (var i = 0; i < length; ++i)
            {
                Point2D a2 = points[i];

                result.AppendPoints(LineSegmentCubicBezierIntersection(p1X, p1Y, p2X, p2Y, p3X, p3Y, p4X, p4Y, a1.X, a1.Y, a2.X, a2.Y).Points);

                a1 = a2;
            }

            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
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
        /// <param name="ecX"></param>
        /// <param name="ecY"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection CubicBezierUnrotatedEllipseIntersection(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            double p4X, double p4Y,
            double ecX, double ecY,
            double rx, double ry)
        {
            var result = new Intersection(IntersectionState.NoIntersection);
            var a = new Vector2D(p1X, p1Y).Scale(-1);
            var b = new Vector2D(p2X, p2Y).Scale(3);
            var c = new Vector2D(p3X, p3Y).Scale(-3);
            var d = a.Add(b.Add(c.Add(new Vector2D(p4X, p4Y))));
            var c3 = new Vector2D(d.I, d.J);
            a = new Vector2D(p1X, p1Y).Scale(3);
            b = new Vector2D(p2X, p2Y).Scale(-6);
            c = new Vector2D(p3X, p3Y).Scale(3);
            d = a.Add(b.Add(c));
            var c2 = new Vector2D(d.I, d.J);
            a = new Vector2D(p1X, p1Y).Scale(-3);
            b = new Vector2D(p2X, p2Y).Scale(3);
            c = a.Add(b);
            var c1 = new Vector2D(c.I, c.J);
            var c0 = new Vector2D(p1X, p1Y);
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection CubicBezierCubicBezierIntersection(
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
            var result = new Intersection(IntersectionState.NoIntersection);
            var a = new Vector2D(a1X, a1Y).Scale(-1);
            var b = new Vector2D(a2X, a2Y).Scale(3);
            var c = new Vector2D(a3X, a3Y).Scale(-3);
            var d = a.Add(b.Add(c.Add(new Vector2D(a4X, a4Y))));
            var c13 = new Vector2D(d.I, d.J);
            a = new Vector2D(a1X, a1Y).Scale(3);
            b = new Vector2D(a2X, a2Y).Scale(-6);
            c = new Vector2D(a3X, a3Y).Scale(3);
            d = a.Add(b.Add(c));
            var c12 = new Vector2D(d.I, d.J);
            a = new Vector2D(a1X, a1Y).Scale(-3);
            b = new Vector2D(a2X, a2Y).Scale(3);
            c = a.Add(b);
            var c11 = new Vector2D(c.I, c.J);
            var c10 = new Vector2D(a1X, a1Y);
            a = new Vector2D(b1X, b1Y).Scale(-1);
            b = new Vector2D(b2X, b2Y).Scale(3);
            c = new Vector2D(b3X, b3Y).Scale(-3);
            d = a.Add(b.Add(c.Add(new Vector2D(b4X, b4Y))));
            var c23 = new Vector2D(d.I, d.J);
            a = new Vector2D(b1X, b1Y).Scale(3);
            b = new Vector2D(b2X, b2Y).Scale(-6);
            c = new Vector2D(b3X, b3Y).Scale(3);
            d = a.Add(b.Add(c));
            var c22 = new Vector2D(d.I, d.J);
            a = new Vector2D(b1X, b1Y).Scale(-3);
            b = new Vector2D(b2X, b2Y).Scale(3);
            c = a.Add(b);
            var c21 = new Vector2D(c.I, c.J);
            var c20 = new Vector2D(b1X, b1Y);
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
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection QuadraticBezierRectangleIntersection(
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

            var result = new Intersection(IntersectionState.NoIntersection);
            result.AppendPoints(LineSegmentQuadraticBezierIntersection(min.X, min.Y, topRight.X, topRight.Y, p1X, p1Y, p2X, p2Y, p3X, p3Y).Points);
            result.AppendPoints(LineSegmentQuadraticBezierIntersection(topRight.X, topRight.Y, max.X, max.Y, p1X, p1Y, p2X, p2Y, p3X, p3Y).Points);
            result.AppendPoints(LineSegmentQuadraticBezierIntersection(max.X, max.Y, bottomLeft.X, bottomLeft.Y, p1X, p1Y, p2X, p2Y, p3X, p3Y).Points);
            result.AppendPoints(LineSegmentQuadraticBezierIntersection(bottomLeft.X, bottomLeft.Y, min.X, min.Y, p1X, p1Y, p2X, p2Y, p3X, p3Y).Points);

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection QuadraticBezierPolygonIntersection(
            double p1X, double p1Y,
            double p2X, double p2Y,
            double p3X, double p3Y,
            List<Point2D> points)
        {
            var result = new Intersection(IntersectionState.NoIntersection);
            var length = points.Count;

            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            Point2D a1 = points[length - 1];
            for (var i = 0; i < length; ++i)
            {
                Point2D a2 = points[i];

                result.AppendPoints(LineSegmentQuadraticBezierIntersection(a1.X, a1.Y, a2.X, a2.Y, p1X, p1Y, p2X, p2Y, p3X, p3Y).Points);

                a1 = a2;
            }

            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
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
        /// <param name="ecX"></param>
        /// <param name="ecY"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/ </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection QuadraticBezierUnrotatedEllipseIntersection(
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
            c2 = new Vector2D(p1X, p1Y).Add(a.Add(new Vector2D(p3X, p3Y)));
            a = new Vector2D(p1X, p1Y).Scale(-2);
            b = new Vector2D(p2X, p2Y).Scale(2);
            c1 = a.Add(b);
            c0 = new Vector2D(p1X, p1Y);
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection QuadraticBezierCubicBezierIntersection(
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
            c12 = new Vector2D(a1X, a1Y).Add(a.Add(new Vector2D(a3X, a3Y)));
            a = new Vector2D(a1X, a1Y).Scale(-2);
            b = new Vector2D(a2X, a2Y).Scale(2);
            c11 = a.Add(b);
            c10 = new Vector2D(a1X, a1Y);
            a = new Vector2D(b1X, b1Y).Scale(-1);
            b = new Vector2D(b2X, b2Y).Scale(3);
            c = new Vector2D(b3X, b3Y).Scale(-3);
            d = a.Add(b.Add(c.Add(new Vector2D(b4X, b4Y))));
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Intersection QuadraticBezierQuadraticBezierIntersection(
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
            c12 = new Vector2D(a1X, a1Y).Add(va.Add(new Vector2D(a3X, a3Y)));
            va = new Vector2D(a1X, a1Y).Scale(-2);
            vb = new Vector2D(a2X, a2Y).Scale(2);
            c11 = va.Add(vb);
            c10 = new Vector2D(a1X, a1Y);
            va = new Vector2D(b2X, b2Y).Scale(-2);
            c22 = new Vector2D(b1X, b1Y).Add(va.Add(new Vector2D(b3X, b3Y)));
            va = new Vector2D(b1X, b1Y).Scale(-2);
            vb = new Vector2D(b2X, b2Y).Scale(2);
            c21 = va.Add(vb);
            c20 = new Vector2D(b1X, b1Y);
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

        #endregion
    }
}

