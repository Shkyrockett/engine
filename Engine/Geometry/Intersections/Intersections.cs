// <copyright file="Intersections.cs" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
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

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static Engine.Maths;
using static System.Math;
using static Engine.Measurements;
using System;

namespace Engine
{
    /// <summary>
    /// A collection of methods for collecting the interactions of shapes.
    /// </summary>
    public static class Intersections
    {
        #region Between Extension Method Overloads

        /// <summary>
        /// Check whether a value lies between two other values.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="m"></param>
        /// <param name="M"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo
        /// </acknowledgment>
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
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.xarg.org/2010/06/is-an-angle-between-two-other-angles/
        /// </acknowledgment>
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
        /// Determines whether the specified point is contained withing the region defined by this <see cref="PolygonContour"/>.
        /// </summary>
        /// <param name="polygon"><see cref="PolygonContour"/> class.</param>
        /// <param name="point">Point to test.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this PolygonContour polygon, Point2D point)
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
        /// <param name="polygons">List of <see cref="PolygonContour"/> classes.</param>
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
        /// <param name="rect"></param>
        /// <param name="shape"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="point0"></param>
        /// <param name="point1"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this Point2D point0, Point2D point1)
            => point0 == point1;

        /// <summary>
        /// Check whether a point is coincident to a line segment.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this Point2D p, Point2D a, Point2D b)
            => PointLineSegmentIntersects(p.X, p.Y, a.X, a.Y, b.X, b.Y);

        /// <summary>
        /// Check whether a point is coincident to a line segment.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this Point2D p, LineSegment s)
            => PointLineSegmentIntersects(p.X, p.Y, s.A.X, s.A.Y, s.B.X, s.B.Y);

        /// <summary>
        /// Check whether a point is coincident to a line segment.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this LineSegment s, Point2D p)
            => PointLineSegmentIntersects(p.X, p.Y, s.A.X, s.A.Y, s.B.X, s.B.Y);

        /// <summary>
        /// Check whether two line segments intersect.
        /// </summary>
        /// <param name="s0"></param>
        /// <param name="s1"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this LineSegment s0, LineSegment s1)
            => LineSegmentLineSegmentIntersects(s0.A.X, s0.A.Y, s0.B.X, s0.B.Y, s1.A.X, s1.A.Y, s1.B.X, s1.B.Y);

        /// <summary>
        /// Determines if this rectangle interests with another rectangle.
        /// </summary>
        /// <param name="rect1"></param>
        /// <param name="rect2"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this Rectangle2D rect1, Rectangle2D rect2)
            => RectangleRectangleIntersects(rect1.X, rect1.Y, rect1.Width, rect1.Height, rect2.X, rect2.Y, rect2.Width, rect2.Height);

        /// <summary>
        /// Determines if this Circle interests with another Circle.
        /// </summary>
        /// <param name="c0"></param>
        /// <param name="c1"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this Circle c0, Circle c1)
            => CircleCircleIntersects(c0.X, c0.Y, c0.Radius, c1.X, c1.Y, c1.Radius);

        #endregion

        #region Intersection Extension Method Overloads

        /// <summary>
        /// Find the intersection of two Points. 
        /// </summary>
        /// <param name="p0">The first point.</param>
        /// <param name="p1">The second point.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Point2D p0, Point2D p1, double epsilon = Epsilon)
            => PointPointIntersection(p0.X, p0.Y, p1.X, p1.Y, epsilon);

        /// <summary>
        /// Find the intersection of a point and a line.
        /// </summary>
        /// <param name="p">The point.</param>
        /// <param name="l">The line.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Point2D p, Line l, double epsilon = Epsilon)
            => PointLineIntersection(p.X, p.Y, l.Location.X, l.Location.Y, l.Direction.I, l.Direction.J, epsilon);

        /// <summary>
        /// Find the intersection of a Point and a Line segment.
        /// </summary>
        /// <param name="p">The point.</param>
        /// <param name="s">The line segment.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Point2D p, LineSegment s, double epsilon = Epsilon)
            => PointLineSegmentIntersection(p.X, p.Y, s.AX, s.AY, s.BX, s.BY, epsilon);

        /// <summary>
        /// Find the intersection of a line and a point.
        /// </summary>
        /// <param name="l">The Line.</param>
        /// <param name="p">The Point.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, Point2D p, double epsilon = Epsilon)
            => PointLineIntersection(p.X, p.Y, l.Location.X, l.Location.Y, l.Direction.I, l.Direction.J, epsilon);

        /// <summary>
        /// Find the intersection of to Lines.
        /// </summary>
        /// <param name="a">The first line.</param>
        /// <param name="b">The second line.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line a, Line b, double epsilon = Epsilon)
            => LineLineIntersection(
                a.Location.X, a.Location.Y,
                a.Location.X + a.Direction.I, a.Location.Y + a.Direction.J,
                b.Location.X, b.Location.Y,
                b.Location.X + b.Direction.I, b.Location.Y + b.Direction.J, epsilon
                );

        /// <summary>
        /// Find the intersection of a Line and a Line segment.
        /// </summary>
        /// <param name="l">The line.</param>
        /// <param name="s">The line segment.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, LineSegment s, double epsilon = Epsilon)
            => LineLineSegmentIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, s.AX, s.AY, s.BX, s.BY, epsilon);

        /// <summary>
        /// Find the intersection of a Line segment and a Quadratic Bezier.
        /// </summary>
        /// <param name="l">The line.</param>
        /// <param name="b">The quadratic bezier curve segmet.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, QuadraticBezier b, double epsilon = Epsilon)
            => LineQuadraticBezierIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, epsilon);

        /// <summary>
        /// Find the intersection of a Line segment and a Cubic Bezier.
        /// </summary>
        /// <param name="l">The line.</param>
        /// <param name="b">The cubic bezier curve segment.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, CubicBezier b, double epsilon = Epsilon)
            => LineCubicBezierIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a line.
        /// </summary>
        /// <param name="l">The line.</param>
        /// <param name="c">The circle.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, Circle c, double epsilon = Epsilon)
            => LineCircleIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, c.X, c.Y, c.Radius, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a line.
        /// </summary>
        /// <param name="l">The line.</param>
        /// <param name="c">The circular arc.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, CircularArc c, double epsilon = Epsilon)
            => LineCircularArcIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, c.X, c.Y, c.Radius, 0, c.StartAngle, c.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="e">The ellipse.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line s, Ellipse e, double epsilon = Epsilon)
            => LineEllipseIntersection(s.Location.X, s.Location.Y, s.Location.X + s.Direction.I, s.Location.Y + s.Direction.J, e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="e">The elliptical arc.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line s, EllipticalArc e, double epsilon = Epsilon)
            => LineEllipticalArcIntersection(s.Location.X, s.Location.Y, s.Location.X + s.Direction.I, s.Location.Y + s.Direction.J, e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, e.StartAngle, e.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of a Line and a Rectangle.
        /// </summary>
        /// <param name="l">The line.</param>
        /// <param name="r">The rectangle.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, Rectangle2D r, double epsilon = Epsilon)
            => LineRectangleIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Line and a Polygon contour.
        /// </summary>
        /// <param name="l">The line.</param>
        /// <param name="p">The polygon contour.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, PolygonContour p, double epsilon = Epsilon)
            => LinePolygonContourIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of two Rays.
        /// </summary>
        /// <param name="r0">The first ray.</param>
        /// <param name="r1">The second ray.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray r0, Ray r1, double epsilon = Epsilon)
            => RayRayIntersection(r0.Location.X, r0.Location.Y, r0.Direction.I, r0.Direction.J, r1.Location.X, r1.Location.Y, r1.Direction.I, r1.Direction.J, epsilon);

        /// <summary>
        /// Find the intersection of two Bezier segments.
        /// </summary>
        /// <param name="b0">The first Bezier segment.</param>
        /// <param name="b1">The second Bezier segment.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this BezierSegmentX b0, BezierSegmentX b1, double epsilon = Epsilon)
        {
            switch (b0.Degree)
            {
                case PolynomialDegree.Linear:
                    switch (b1.Degree)
                    {
                        case PolynomialDegree.Linear:
                            return LineSegmentLineSegmentIntersection(b1[0].X, b1[0].Y, b1[1].X, b1[1].Y, b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, epsilon);
                        case PolynomialDegree.Quadratic:
                            return LineSegmentQuadraticBezierSegmentIntersection(b1[0].X, b1[0].Y, b1[1].X, b1[1].Y, b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b0[2].X, b0[2].Y, epsilon);
                        case PolynomialDegree.Cubic:
                            return LineSegmentCubicBezierSegmentIntersection(b1[0].X, b1[0].Y, b1[1].X, b1[1].Y, b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b0[2].X, b0[2].Y, b0[3].X, b0[3].Y, epsilon);
                        default:
                            return new Intersection(IntersectionState.NoIntersection);
                    }
                case PolynomialDegree.Quadratic:
                    switch (b1.Degree)
                    {
                        case PolynomialDegree.Linear:
                            return LineSegmentQuadraticBezierSegmentIntersection(b1[0].X, b1[0].Y, b1[1].X, b1[1].Y, b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b0[2].X, b0[2].Y, epsilon);
                        case PolynomialDegree.Quadratic:
                            return QuadraticBezierSegmentQuadraticBezierSegmentIntersection(b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b0[2].X, b0[2].Y, b1[0].X, b1[0].Y, b1[1].X, b1[1].Y, b1[2].X, b1[2].Y, epsilon);
                        case PolynomialDegree.Cubic:
                            return QuadraticBezierSegmentCubicBezierSegmentIntersection(b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b0[2].X, b0[2].Y, b1[0].X, b1[0].Y, b1[1].X, b1[1].Y, b1[2].X, b1[2].Y, b1[3].X, b1[3].Y, epsilon);
                        default:
                            return new Intersection(IntersectionState.NoIntersection);
                    }
                case PolynomialDegree.Cubic:
                    switch (b1.Degree)
                    {
                        case PolynomialDegree.Linear:
                            return LineSegmentCubicBezierSegmentIntersection(b1[0].X, b1[0].Y, b1[1].X, b1[1].Y, b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b0[2].X, b0[2].Y, b0[3].X, b0[3].Y, epsilon);
                        case PolynomialDegree.Quadratic:
                            return QuadraticBezierSegmentCubicBezierSegmentIntersection(b1[0].X, b1[0].Y, b1[1].X, b1[1].Y, b1[2].X, b1[2].Y, b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b0[2].X, b0[2].Y, b0[3].X, b0[3].Y, epsilon);
                        case PolynomialDegree.Cubic:
                            return CubicBezierSegmentCubicBezierSegmentIntersection(b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b0[2].X, b0[2].Y, b0[3].X, b0[3].Y, b1[0].X, b1[0].Y, b1[1].X, b1[1].Y, b1[2].X, b1[2].Y, b1[3].X, b1[3].Y, epsilon);
                        default:
                            return new Intersection(IntersectionState.NoIntersection);
                    }
                default:
                    return new Intersection(IntersectionState.NoIntersection);
            }
        }

        /// <summary>
        /// Find the intersection of a Bezier segment and a line segment.
        /// </summary>
        /// <param name="b">The bezier segment.</param>
        /// <param name="l">The line segment.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this BezierSegmentX b, LineSegment l, double epsilon = Epsilon)
        {
            switch (b.Degree)
            {
                case PolynomialDegree.Linear:
                    return LineSegmentLineSegmentIntersection(l.AX, l.AY, l.BX, l.BY, b[0].X, b[0].Y, b[1].X, b[1].Y, epsilon);
                case PolynomialDegree.Quadratic:
                    return LineSegmentQuadraticBezierSegmentIntersection(l.AX, l.AY, l.BX, l.BY, b[0].X, b[0].Y, b[1].X, b[1].Y, b[2].X, b[2].Y, epsilon);
                case PolynomialDegree.Cubic:
                    return LineSegmentCubicBezierSegmentIntersection(l.AX, l.AY, l.BX, l.BY, b[0].X, b[0].Y, b[1].X, b[1].Y, b[2].X, b[2].Y, b[3].X, b[3].Y, epsilon);
                default:
                    return new Intersection(IntersectionState.NoIntersection);
            }
        }

        /// <summary>
        /// Find the intersection of a Bezier segment and a Quadratic Bezier.
        /// </summary>
        /// <param name="b0">The Bezier segment.</param>
        /// <param name="b1">The quadratic bezier curve segment.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this BezierSegmentX b0, QuadraticBezier b1, double epsilon = Epsilon)
        {
            switch (b0.Degree)
            {
                case PolynomialDegree.Linear:
                    return LineSegmentQuadraticBezierSegmentIntersection(b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b1.AX, b1.AY, b1.BX, b1.BY, b1.CX, b1.CY, epsilon);
                case PolynomialDegree.Quadratic:
                    return QuadraticBezierSegmentQuadraticBezierSegmentIntersection(b1.AX, b1.AY, b1.BX, b1.BY, b1.CX, b1.CY, b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b0[2].X, b0[2].Y, epsilon);
                case PolynomialDegree.Cubic:
                    return QuadraticBezierSegmentCubicBezierSegmentIntersection(b1.AX, b1.AY, b1.BX, b1.BY, b1.CX, b1.CY, b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b0[2].X, b0[2].Y, b0[3].X, b0[3].Y, epsilon);
                default:
                    return new Intersection(IntersectionState.NoIntersection);
            }
        }

        /// <summary>
        /// Find the intersection of a Bezier segment and a Cubic Bezier.
        /// </summary>
        /// <param name="b">The Bezier segment.</param>
        /// <param name="c">The cubic bezier curve segment.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this BezierSegmentX b, CubicBezier c, double epsilon = Epsilon)
        {
            switch (b.Degree)
            {
                case PolynomialDegree.Linear:
                    return LineSegmentCubicBezierSegmentIntersection(c.AX, c.AY, c.BX, c.BY, c.CX, c.CY, c.DX, c.DY, b[0].X, b[0].Y, b[1].X, b[1].Y, epsilon);
                case PolynomialDegree.Quadratic:
                    return QuadraticBezierSegmentCubicBezierSegmentIntersection(b[0].X, b[0].Y, b[1].X, b[1].Y, b[2].X, b[2].Y, c.AX, c.AY, c.BX, c.BY, c.CX, c.CY, c.DX, c.DY, epsilon);
                case PolynomialDegree.Cubic:
                    return CubicBezierSegmentCubicBezierSegmentIntersection(c.AX, c.AY, c.BX, c.BY, c.CX, c.CY, c.DX, c.DY, b[0].X, b[0].Y, b[1].X, b[1].Y, b[2].X, b[2].Y, b[3].X, b[3].Y, epsilon);
                default:
                    return new Intersection(IntersectionState.NoIntersection);
            }
        }

        /// <summary>
        /// Find the intersection of a Line segment and a Point.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="p">The point.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, Point2D p, double epsilon = Epsilon)
            => PointLineSegmentIntersection(p.X, p.Y, s.AX, s.AY, s.BX, s.BY, epsilon);

        /// <summary>
        /// Find the intersection of a Line segment and a Line.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="l">The line.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, Line l, double epsilon = Epsilon)
            => LineLineSegmentIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, s.AX, s.AY, s.BX, s.BY, epsilon);

        /// <summary>
        /// Find the intersection of a line segment and a Bezier segment.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="b">The bezier curve segment.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, BezierSegmentX b, double epsilon = Epsilon)
            => Intersection(b, s, epsilon);

        /// <summary>
        /// Find the intersection of two lines segments.
        /// </summary>
        /// <param name="s1">The line segment.</param>
        /// <param name="s2">The line segment.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s1, LineSegment s2, double epsilon = Epsilon)
            => LineSegmentLineSegmentIntersection(s1.A.X, s1.A.Y, s1.B.X, s1.B.Y, s2.A.X, s2.A.Y, s2.B.X, s2.B.Y, epsilon);

        /// <summary>
        /// Find the intersection of a Line segment and a Quadratic Bezier.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="b">The quadratic bezier curve segment.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, QuadraticBezier b, double epsilon = Epsilon)
            => LineSegmentQuadraticBezierSegmentIntersection(s.AX, s.AY, s.BX, s.BY, b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, epsilon);

        /// <summary>
        /// Find the intersection of a Line segment and a Cubic Bezier.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="b">The cubic bezier curve segment.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, CubicBezier b, double epsilon = Epsilon)
            => LineSegmentCubicBezierSegmentIntersection(s.AX, s.AY, s.BX, s.BY, b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, epsilon);

        /// <summary>
        /// Find the intersection of a Line segment and a Polygon contour.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="p">The polygon contour.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, PolygonContour p, double epsilon = Epsilon)
            => LineSegmentPolygonContourIntersection(s.AX, s.AY, s.BX, s.BY, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of a Line segment and a Rectangle.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="r">The rectangle.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, Rectangle2D r, double epsilon = Epsilon)
            => LineSegmentRectangleIntersection(s.AX, s.AY, s.BX, s.BY, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a line segment.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="c">The circle.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, Circle c, double epsilon = Epsilon)
            => LineSegmentCircleIntersection(s.A.X, s.A.Y, s.B.X, s.B.Y, c.X, c.Y, c.Radius, 0, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="c">The circular arc.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, CircularArc c, double epsilon = Epsilon)
            => LineSegmentCircularArcIntersection(s.A.X, s.A.Y, c.X, s.B.X, s.B.Y, c.Y, c.Radius, 0, c.StartAngle, c.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="e">The ellipse.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, Ellipse e, double epsilon = Epsilon)
            => LineSegmentEllipseIntersection(s.A.X, s.A.Y, s.B.X, s.B.Y, e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="e">The elliptical arc.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, EllipticalArc e, double epsilon = Epsilon)
            => LineSegmentEllipticalArcIntersection(s.A.X, s.A.Y, e.X, s.B.X, s.B.Y, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, e.StartAngle, e.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bezier and a Line segment.
        /// </summary>
        /// <param name="b">The quadratic bezier curve segment.</param>
        /// <param name="l">The line.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b, Line l, double epsilon = Epsilon)
            => LineQuadraticBezierIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bezier and a Bezier segment.
        /// </summary>
        /// <param name="q">The quadratic bezier curve segment.</param>
        /// <param name="b">The bezier segment.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier q, BezierSegmentX b, double epsilon = Epsilon)
            => Intersection(b, q, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bezier and a Line segment.
        /// </summary>
        /// <param name="b">The quadratic bezier curve segment.</param>
        /// <param name="l">The line segment.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b, LineSegment l, double epsilon = Epsilon)
            => LineSegmentQuadraticBezierSegmentIntersection(l.AX, l.AY, l.BX, l.BY, b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, epsilon);

        /// <summary>
        /// Find the intersection of two Quadratic Beziers.
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="b1"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b0, QuadraticBezier b1, double epsilon = Epsilon)
            => QuadraticBezierSegmentQuadraticBezierSegmentIntersection(b0.AX, b0.AY, b0.BX, b0.BY, b0.CX, b0.CY, b1.AX, b1.AY, b1.BX, b1.BY, b1.CX, b1.CY, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bezier and a Cubic Bezier.
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="b1"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b0, CubicBezier b1, double epsilon = Epsilon)
            => QuadraticBezierSegmentCubicBezierSegmentIntersection(b0.AX, b0.AY, b0.BX, b0.BY, b0.CX, b0.CY, b1.AX, b1.AY, b1.BX, b1.BY, b1.CX, b1.CY, b1.DX, b1.DY, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bezier and a Circle.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b, Circle c, double epsilon = Epsilon)
            => QuadraticBezierSegmentUnrotatedEllipseIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, c.X, c.Y, c.Radius, c.Radius, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bezier and an unrotated Ellipse.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="e"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        //[DebuggerStepThrough]
        public static Intersection Intersection(this QuadraticBezier b, Ellipse e, double epsilon = Epsilon)
            => QuadraticBezierSegmentUnrotatedEllipseIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, e.X, e.Y, e.RX, e.RY, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bezier and a Rectangle.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="r"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b, Rectangle2D r, double epsilon = Epsilon)
            => QuadraticBezierSegmentRectangleIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bezier and a Polygon Contour.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="p"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b, PolygonContour p, double epsilon = Epsilon)
            => QuadraticBezierSegmentPolygonContourIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bezier and a Line segment.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="l"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b, Line l, double epsilon = Epsilon)
            => LineCubicBezierIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bezier and a Bezier segment.
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="b1"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b0, BezierSegmentX b1, double epsilon = Epsilon)
            => Intersection(b1, b0, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bezier and a Line segment.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="l"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b, LineSegment l, double epsilon = Epsilon)
            => LineSegmentCubicBezierSegmentIntersection(l.AX, l.AY, l.BX, l.BY, b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bezier and a Quadratic Bezier.
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b0"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b1, QuadraticBezier b0, double epsilon = Epsilon)
            => QuadraticBezierSegmentCubicBezierSegmentIntersection(b0.AX, b0.AY, b0.BX, b0.BY, b0.CX, b0.CY, b1.AX, b1.AY, b1.BX, b1.BY, b1.CX, b1.CY, b1.DX, b1.DY, epsilon);

        /// <summary>
        /// Find the intersection of two Cubic Beziers.
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="b1"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b0, CubicBezier b1, double epsilon = Epsilon)
            => CubicBezierSegmentCubicBezierSegmentIntersection(b0.AX, b0.AY, b0.BX, b0.BY, b0.CX, b0.CY, b0.DX, b0.DY, b1.AX, b1.AY, b1.BX, b1.BY, b1.CX, b1.CY, b1.DX, b1.DY, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bezier and a Circle.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b, Circle c, double epsilon = Epsilon)
            => CubicBezierSegmentUnrotatedEllipseIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, c.X, c.Y, c.Radius, c.Radius, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bezier and an unrotated Ellipse.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="e"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b, Ellipse e, double epsilon = Epsilon)
            => CubicBezierSegmentUnrotatedEllipseIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, e.X, e.Y, e.RX, e.RY, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bezier and a Rectangle.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="r"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b, Rectangle2D r, double epsilon = Epsilon)
            => CubicBezierSegmentRectangleIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bezier and a Polygon Contour.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="p"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b, PolygonContour p, double epsilon = Epsilon)
            => CubicBezierSegmentPolygonIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a line.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="l"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, Line l, double epsilon = Epsilon)
            => LineCircleIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, c.X, c.Y, c.Radius, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a line segment.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="s"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, LineSegment s, double epsilon = Epsilon)
            => LineSegmentCircleIntersection(s.A.X, s.A.Y, s.B.X, s.B.Y, c.X, c.Y, c.Radius, 0, epsilon);

        /// <summary>
        /// Find the intersection of a Circle and a Quadratic Bezier.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="b"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, QuadraticBezier b, double epsilon = Epsilon)
            => QuadraticBezierSegmentUnrotatedEllipseIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, c.X, c.Y, c.Radius, c.Radius, epsilon);

        /// <summary>
        /// Find the intersection of a Circle and a Cubic Bezier.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="b"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, CubicBezier b, double epsilon = Epsilon)
            => CubicBezierSegmentUnrotatedEllipseIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, c.X, c.Y, c.Radius, c.Radius, epsilon);

        /// <summary>
        /// Find the intersection between two circles.
        /// </summary>
        /// <param name="c0"></param>
        /// <param name="c1"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c0, Circle c1, double epsilon = Epsilon)
            => CircleCircleIntersection(c0.X, c0.Y, c0.Radius, c1.X, c1.Y, c1.Radius, epsilon);

        /// <summary>
        /// Find the intersection of a Circle and an unrotated Ellipse.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="e"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, Ellipse e, double epsilon = Epsilon)
            => UnrotatedEllipseUnrotatedEllipseIntersection(c.Center.X, c.Center.Y, c.Radius, c.Radius, e.Center.X, e.Center.Y, e.RX, e.RY, epsilon);

        /// <summary>
        /// Find the intersection of a Circle and a Rectangle.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="r"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, Rectangle2D r, double epsilon = Epsilon)
            => CircleRectangleIntersection(c.X, c.Y, c.Radius, 0, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Circle and a polygon contour.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="r"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, PolygonContour r, double epsilon = Epsilon)
            => CirclePolygonContourIntersection(c.X, c.Y, c.Radius, 0, r.Points, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <param name="s"></param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CircularArc c, LineSegment s, double epsilon = Epsilon)
            => LineSegmentCircularArcIntersection(s.A.X, s.A.Y, c.X, s.B.X, s.B.Y, c.Y, c.Radius, 0, c.StartAngle, c.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a line.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="s"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CircularArc c, Line s, double epsilon = Epsilon)
            => LineCircularArcIntersection(s.Location.X, s.Location.Y, s.Location.X + s.Direction.I, s.Location.Y + s.Direction.J, c.X, c.Y, c.Radius, 0, c.StartAngle, c.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="s"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, Line s, double epsilon = Epsilon)
            => LineEllipseIntersection(s.Location.X, s.Location.Y, s.Location.X + s.Direction.I, s.Location.Y + s.Direction.J, e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="s"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, LineSegment s, double epsilon = Epsilon)
            => LineSegmentEllipseIntersection(s.A.X, s.A.Y, s.B.X, s.B.Y, e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated Ellipse and a Quadratic Bezier.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="b"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, QuadraticBezier b, double epsilon = Epsilon)
             => QuadraticBezierSegmentUnrotatedEllipseIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, e.X, e.Y, e.RX, e.RY, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a Cubic Bezier.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="b"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, CubicBezier b, double epsilon = Epsilon)
            => CubicBezierSegmentUnrotatedEllipseIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, e.X, e.Y, e.RX, e.RY, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated Ellipse and a Circle.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <param name="e"></param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, Circle c, double epsilon = Epsilon)
            => UnrotatedEllipseUnrotatedEllipseIntersection(c.Center.X, c.Center.Y, c.Radius, c.Radius, e.Center.X, e.Center.Y, e.RX, e.RY, epsilon);

        /// <summary>
        /// Find the intersection of two unrotated Ellipses.
        /// </summary>
        /// <param name="e0"></param>
        /// <param name="e1"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e0, Ellipse e1, double epsilon = Epsilon)
            => UnrotatedEllipseUnrotatedEllipseIntersection(e0.Center.X, e0.Center.Y, e0.RX, e0.RY, e1.Center.X, e1.Center.Y, e1.RX, e1.RY, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a rectangle.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="r"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, Rectangle2D r, double epsilon = Epsilon)
            => EllipseRectangleIntersection(e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a polygon.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="p"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, PolygonContour p, double epsilon = Epsilon)
            => EllipsePolygonContourIntersection(e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="s"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this EllipticalArc e, Line s, double epsilon = Epsilon)
            => LineEllipticalArcIntersection(s.Location.X, s.Location.Y, s.Location.X + s.Direction.I, s.Location.Y + s.Direction.J, e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, e.StartAngle, e.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="s"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this EllipticalArc e, LineSegment s, double epsilon = Epsilon)
            => LineSegmentEllipticalArcIntersection(s.A.X, s.A.Y, e.X, s.B.X, s.B.Y, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, e.StartAngle, e.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of a Rectangle and a line.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <param name="r"></param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r, Line l, double epsilon = Epsilon)
            => LineRectangleIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Rectangle and a Line segment.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="l"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r, LineSegment l, double epsilon = Epsilon)
            => LineSegmentRectangleIntersection(l.AX, l.AY, l.BX, l.BY, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Rectangle and a Quadratic Bezier.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="b"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r, QuadraticBezier b, double epsilon = Epsilon)
            => QuadraticBezierSegmentRectangleIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Rectangle and a Cubic Bezier.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="b"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r, CubicBezier b, double epsilon = Epsilon)
            => CubicBezierSegmentRectangleIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Rectangle and a Circle.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r, Circle c, double epsilon = Epsilon)
            => CircleRectangleIntersection(c.X, c.Y, c.Radius, 0, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a rectangle.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="e"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r, Ellipse e, double epsilon = Epsilon)
            => EllipseRectangleIntersection(e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of two Rectangles.
        /// </summary>
        /// <param name="r0"></param>
        /// <param name="r1"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r0, Rectangle2D r1, double epsilon = Epsilon)
            => RectangleRectangleIntersection(r0.X, r0.Y, r0.Right, r0.Bottom, r1.X, r1.Y, r1.Right, r1.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Rectangle and a Polygon contour.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="p"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D l, PolygonContour p, double epsilon = Epsilon)
            => PolygonContourRectangleIntersection(p.Points, l.X, l.Y, l.Right, l.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Polygon contour and a Line.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <param name="p"></param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p, Line l, double epsilon = Epsilon)
            => LinePolygonContourIntersection(l.Location.X, l.Location.Y, l.Location.X + l.Direction.I, l.Location.Y + l.Direction.J, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of a Polygon contour and a Line segment.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="l"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p, LineSegment l, double epsilon = Epsilon)
            => LineSegmentPolygonContourIntersection(l.AX, l.AY, l.BX, l.BY, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of a Polygon contour and a Quadratic Bezier.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="b"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p, QuadraticBezier b, double epsilon = Epsilon)
             => QuadraticBezierSegmentPolygonContourIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of a Polygon Contour and a Cubic Bezier.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="b"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p, CubicBezier b, double epsilon = Epsilon)
            => CubicBezierSegmentPolygonIntersection(b.AX, b.AY, b.BX, b.BY, b.CX, b.CY, b.DX, b.DY, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a polygon.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="e"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p, Circle e, double epsilon = Epsilon)
            => CirclePolygonContourIntersection(e.X, e.Y, e.Radius, 0, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a polygon.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="e"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p, Ellipse e, double epsilon = Epsilon)
            => EllipsePolygonContourIntersection(e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of a Polygon contour and a Rectangle.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="l"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p, Rectangle2D l, double epsilon = Epsilon)
            => PolygonContourRectangleIntersection(p.Points, l.X, l.Y, l.Right, l.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of two Polygon contours.
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p0, PolygonContour p1, double epsilon = Epsilon)
             => PolygonContourPolygonContourIntersection(p0.Points, p1.Points, epsilon);

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
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://math.stackexchange.com/questions/1698835/find-if-a-vector-is-between-2-vectors
        /// http://stackoverflow.com/questions/13640931/how-to-determine-if-a-vector-is-between-two-other-vectors
        /// http://gamedev.stackexchange.com/questions/22392/what-is-a-good-way-to-determine-if-a-vector-is-between-two-other-vectors-in-2d
        /// http://math.stackexchange.com/questions/169998/figure-out-if-a-fourth-point-resides-within-an-angle-created-by-three-other-poin
        /// </acknowledgment>
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
        public static bool PointInTriangle(LineSegment s, Point2D o, Point2D p, double epsilon = Epsilon)
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
        public static Inclusion TriangleContainsPoint(double aX, double aY, double bX, double bY, double cX, double cY, double pX, double pY, double epsilon = Epsilon)
        {
            var a = new Point2D(aX, aY);
            var b = new Point2D(bX, bY);
            var c = new Point2D(cX, cY);
            var p = new Point2D(pX, pY);
            if (Intersects(p, a, b) || Intersects(p, b, c) || Intersects(p, c, a)) return Inclusion.Boundary;
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
        public static Inclusion RectangleContainsPoint(double left, double top, double right, double bottom, double pX, double pY, double epsilon = Epsilon)
            => (((left == pX || right == pX) && ((top <= pY) == (bottom >= pY)))
                || ((top == pY || bottom == pY) && ((left <= pX) == (right >= pX)))) ? Inclusion.Boundary
                : (left <= pX && pX < right && top <= pY && pY < bottom) ? Inclusion.Inside : Inclusion.Outside;

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="PolygonContour"/>.
        /// </summary>
        /// <param name="points">The points that form the corners of the polygon.</param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>
        /// Returns Outside (0) if false, Inside (+1) if true, Boundary (-1) if the point is on a polygon boundary.
        /// </returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// Adapted from Clipper library: http://www.angusj.com/delphi/clipper.php
        /// See "The Point in Polygon Problem for Arbitrary Polygons" by Hormann and Agathos
        /// http://www.inf.usi.ch/hormann/papers/Hormann.2001.TPI.pdf
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion PolygonContourContainsPoint(List<Point2D> points, double pX, double pY, double epsilon = Epsilon)
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
                if ((nextPoint.Y < pY) != (curPoint.Y < pY)) // At least one point is below the Y threshold and the other is above or equal
                {
                    // Optimisation: at least one point must be to the right of the test point
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

#if !PolycurveTest

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="point"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
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
                    case LineCurveSegment l:
                        {
                            inside += ScanbeamPointsToRightLineSegment(point.X, point.Y, l.Start.Value.X, l.Start.Value.Y, l.End.Value.X, l.End.Value.Y);
                        }
                        break;
                    case ArcSegment a:
                        {
                            // ToDo: Figure out how to implement this: https://stackoverflow.com/a/34884949
                            inside += ScanbeamPointsToRightEllipticalArc(point.X, point.Y, a.Center.X, a.Center.Y, a.RX, a.RY, a.CosAngle, a.SinAngle, a.StartAngle, a.SweepAngle, epsilon);
                        }
                        break;
                    case QuadraticBezierSegment q:
                        {
                            inside += ScanbeamPointsToRightQuadraticBezierSegment(point.X, point.Y, q.Start.Value.X, q.Start.Value.Y, q.Handle.Value.X, q.Handle.Value.Y, q.End.Value.X, q.End.Value.Y);
                        }
                        break;
                    case CubicBezierSegment c:
                        {
                            inside += ScanbeamPointsToRightCubicBezierSegment(point.X, point.Y, c.Start.Value.X, c.Start.Value.Y, c.Handle1.X, c.Handle1.Y, c.Handle2.Value.X, c.Handle2.Value.Y, c.End.Value.X, c.End.Value.Y);
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
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
                    case PointSegment p:
                        {
                            if (path[0].Start.Value == point)
                                return Inclusion.Boundary;
                            break;
                        }
                    case LineCurveSegment l:
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
                    case ArcSegment t:
                        {
                            // Find the start and end angles.
                            var sa = EllipticalPolarAngle(t.StartAngle, t.RX, t.RY);
                            var ea = EllipticalPolarAngle(t.StartAngle + t.SweepAngle, t.RX, t.RY);

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
                    case QuadraticBezierSegment b:
                        break;
                    case CubicBezierSegment b:
                        break;
                    case CardinalSegment c:
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
        public static Inclusion PolygonContainsPoint(List<PolygonContour> polygons, double pX, double pY)
        {
            Inclusion returnValue = Inclusion.Outside;

            foreach (PolygonContour poly in polygons)
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
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/481144/equation-for-testing-if-a-point-is-inside-a-circle
        /// </acknowledgment>
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
        /// <remarks></remarks>
        /// <acknowledgment>
        /// Based off of: http://stackoverflow.com/questions/7946187/point-and-ellipse-rotated-position-test-algorithm
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion EllipseContainsPoint(double cX, double cY, double r1, double r2, double angle, double pX, double pY)
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
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/481144/equation-for-testing-if-a-point-is-inside-a-circle
        /// </acknowledgment>
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// Based off of: http://stackoverflow.com/questions/7946187/point-and-ellipse-rotated-position-test-algorithm
        /// </acknowledgment>
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
        /// <remarks></remarks>
        /// <acknowledgment>
        /// Based off of: http://stackoverflow.com/questions/7946187/point-and-ellipse-rotated-position-test-algorithm
        /// </acknowledgment>
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
        /// <remarks></remarks>
        /// <acknowledgment>
        /// Public-domain code by Darel Rex Finley, 2006.
        /// http://alienryderflex.com/shortest_path/
        /// </acknowledgment>
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

            foreach (PolygonContour poly in polygons.Contours)
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
        public static bool RectangleContainsRectangle(double aX, double aY, double aWidth, double aHeight, double bX, double bY, double bWidth, double bHeight)
            => (aX <= bX)
            && ((bX + bWidth) <= (aX + aWidth))
            && (aY <= bY)
            && ((bY + bHeight) <= (aY + aHeight));

        #endregion

        #region Intersects Methods

        /// <summary>
        /// Checks whether two points are at the same location.
        /// </summary>
        /// <param name="point0X"></param>
        /// <param name="point0Y"></param>
        /// <param name="point1X"></param>
        /// <param name="point1Y"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
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
        /// <remarks></remarks>
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
        /// Check whether a point is within a rectangle.
        /// </summary>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PointRectangleIntersects(
            double pX, double pY,
            double r1X, double r1Y,
            double r2X, double r2Y,
            double epsilon = Epsilon)
        {
            var min = MinPoint(r1X, r1Y, r2X, r2Y);
            var max = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);
            if (PointLineSegmentIntersects(pX, pY, min.X, min.Y, topRight.X, topRight.Y, epsilon)) return true;
            if (PointLineSegmentIntersects(pX, pY, topRight.X, topRight.Y, max.X, max.Y, epsilon)) return true;
            if (PointLineSegmentIntersects(pX, pY, max.X, max.Y, bottomLeft.X, bottomLeft.Y, epsilon)) return true;
            if (PointLineSegmentIntersects(pX, pY, bottomLeft.X, bottomLeft.Y, min.X, min.Y, epsilon)) return true;
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns the point of intersection.</returns>
        /// <remarks></remarks>
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns the point of intersection.</returns>
        /// <remarks></remarks>
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
            double epsilon = Epsilon)
        {
            // Todo: Figure out ray intersection.

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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RayRectangleIntersects(
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
            if (RayLineSegmentIntersects(a1X, a1Y, a2X, a2Y, min.X, min.Y, topRight.X, topRight.Y, epsilon)) return true;
            if (RayLineSegmentIntersects(a1X, a1Y, a2X, a2Y, topRight.X, topRight.Y, max.X, max.Y, epsilon)) return true;
            if (RayLineSegmentIntersects(a1X, a1Y, a2X, a2Y, max.X, max.Y, bottomLeft.X, bottomLeft.Y, epsilon)) return true;
            if (RayLineSegmentIntersects(a1X, a1Y, a2X, a2Y, bottomLeft.X, bottomLeft.Y, min.X, min.Y, epsilon)) return true;
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns the point of intersection.</returns>
        /// <remarks></remarks>
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns the point of intersection.</returns>
        /// <remarks></remarks>
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns the point of intersection.</returns>
        /// <remarks></remarks>
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
                => (Abs(((y3 - y2) * (x1 - x0)) - ((x3 - x2) * (y1 - y0))) < epsilon);

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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
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
        /// 
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
        /// <param name="epsilon"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool QuadraticBezierSegmentLineSegmentIntersects(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double x, double y,
            double right, double bottom,
            double epsilon)
            // ToDo: Figure out code to check whether a quadratic Bezier curve and a line segment intersect.
            => throw new NotImplementedException();

        /// <summary>
        /// 
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
        /// <param name="epsilon"></param>
        /// <returns></returns>
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
            var min = MinPoint(rLeft, rTop, rRight, rBottom);
            var max = MaxPoint(rLeft, rTop, rRight, rBottom);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);
            if (QuadraticBezierSegmentLineSegmentIntersects(aX, aY, bX, bY, cX, cY, min.X, min.Y, topRight.X, topRight.Y, epsilon)) return true;
            if (QuadraticBezierSegmentLineSegmentIntersects(aX, aY, bX, bY, cX, cY, topRight.X, topRight.Y, max.X, max.Y, epsilon)) return true;
            if (QuadraticBezierSegmentLineSegmentIntersects(aX, aY, bX, bY, cX, cY, max.X, max.Y, bottomLeft.X, bottomLeft.Y, epsilon)) return true;
            if (QuadraticBezierSegmentLineSegmentIntersects(aX, aY, bX, bY, cX, cY, bottomLeft.X, bottomLeft.Y, min.X, min.Y, epsilon)) return true;
            return false;
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
        /// <param name="dX"></param>
        /// <param name="dY"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
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
            // ToDo: Figure out code to check whether a cubic Bezier curve and a line segment intersect.
            => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="dX"></param>
        /// <param name="dY"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
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
            var min = MinPoint(r1X, r1Y, r2X, r2Y);
            var max = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);
            if (CubicBezierSegmentLineSegmentIntersects(aX, aY, bX, bY, cX, cY, dX, dY, min.X, min.Y, topRight.X, topRight.Y, epsilon)) return true;
            if (CubicBezierSegmentLineSegmentIntersects(aX, aY, bX, bY, cX, cY, dX, dY, topRight.X, topRight.Y, max.X, max.Y, epsilon)) return true;
            if (CubicBezierSegmentLineSegmentIntersects(aX, aY, bX, bY, cX, cY, dX, dY, max.X, max.Y, bottomLeft.X, bottomLeft.Y, epsilon)) return true;
            if (CubicBezierSegmentLineSegmentIntersects(aX, aY, bX, bY, cX, cY, dX, dY, bottomLeft.X, bottomLeft.Y, min.X, min.Y, epsilon)) return true;
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
        /// Find the points where the two circles intersect.
        /// </summary>
        /// <param name="cx0"></param>
        /// <param name="cy0"></param>
        /// <param name="radius0"></param>
        /// <param name="cx1"></param>
        /// <param name="cy1"></param>
        /// <param name="radius1"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
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
        /// Find the intersection point between two points.
        /// </summary>
        /// <param name="p0X"></param>
        /// <param name="p0Y"></param>
        /// <param name="p1X"></param>
        /// <param name="p1Y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection PointPointIntersection(
            double p0X, double p0Y,
            double p1X, double p1Y,
            double epsilon = Epsilon)
            => PointPointIntersects(p0X, p0Y, p1X, p1Y)
            ? new Intersection(IntersectionState.Intersection, new Point2D(p0X, p0Y))
            : new Intersection(IntersectionState.NoIntersection);

        /// <summary>
        /// Find the intersection of a point and a line.
        /// </summary>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <param name="lx"></param>
        /// <param name="ly"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection PointLineIntersection(
            double pX, double pY,
            double lx, double ly, double i, double j,
            double epsilon = Epsilon)
        {
            var result = new Intersection(IntersectionState.NoIntersection);
            if (i == 0 && pX == lx)
                result.AppendPoint(new Point2D(pX, pY));
            else if ((pY - ly) / (pX - lx) == (j) / (i))
                result.AppendPoint(new Point2D(pX, pY));
            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// Find the intersection between a point and a line segment.
        /// </summary>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <param name="lAX"></param>
        /// <param name="lAY"></param>
        /// <param name="lBX"></param>
        /// <param name="lBY"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection PointLineSegmentIntersection(
            double pX, double pY,
            double lAX, double lAY, double lBX, double lBY,
            double epsilon = Epsilon)
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns the point of intersection.</returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.vb-helper.com/howto_segments_intersect.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineLineIntersection(
            double x0, double y0, double x1, double y1,
            double x2, double y2, double x3, double y3,
            double epsilon = Epsilon)
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineLineSegmentIntersection(
            double l0X, double l0Y, double l1X, double l1Y,
            double s0X, double s0Y, double s1X, double s1Y,
            double epsilon = Epsilon)
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
        /// Find the intersection between a line and a quadratic bezier.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// Adapted from code found at: https://www.particleincell.com/2013/cubic-line-intersection/
        /// Which was based off of code found at: http://stackoverflow.com/questions/14005096/mathematical-solution-for-bezier-curve-and-line-intersection-in-coffeescript-or
        /// Which was based off of code found at: http://www.blitzbasic.com/Community/posts.php?topic=64459 (https://web.archive.org/web/20111206104736/http://www.blitzbasic.com/Community/posts.php?topic=64459)
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineQuadraticBezierIntersection(
            double x1, double y1, double x2, double y2,
            double p0x, double p0y, double p1x, double p1y, double p2x, double p2y,
            double epsilon = Epsilon)
        {
            // Initialize intersection.
            var result = new Intersection(IntersectionState.NoIntersection);

            // Translate the line to the origin.
            var a = y2 - y1;
            var b = x1 - x2;

            var c = x1 * (y1 - y2) + y1 * (x2 - x1);

            var xCoeff = QuadraticBezierCoefficients(p0x, p1x, p2x);
            var yCoeff = QuadraticBezierCoefficients(p0y, p1y, p2y);

            var roots = QuadraticRoots(
                /* t^2 */ a * xCoeff.C + b * yCoeff.C,
                /* t^1 */ a * xCoeff.B + b * yCoeff.B,
                /* 1 */ a * xCoeff.A + b * yCoeff.A + c,
                epsilon);

            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];

                // Add intersection point.
                if (!(t < 0 || t > 1d))
                    result.AppendPoint(new Point2D(
                        xCoeff.C * t * t + xCoeff.B * t + xCoeff.A,
                        yCoeff.C * t * t + yCoeff.B * t + yCoeff.A));
            }

            // Return result.
            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// Find the intersection between a line and a cubic bezier.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// Adapted from code found at: https://www.particleincell.com/2013/cubic-line-intersection/
        /// Which was based off of code found at: http://stackoverflow.com/questions/14005096/mathematical-solution-for-bezier-curve-and-line-intersection-in-coffeescript-or
        /// Which was based off of code found at: http://www.blitzbasic.com/Community/posts.php?topic=64459 (https://web.archive.org/web/20111206104736/http://www.blitzbasic.com/Community/posts.php?topic=64459)
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineCubicBezierIntersection(
            double x1, double y1, double x2, double y2,
            double p0x, double p0y, double p1x, double p1y, double p2x, double p2y, double p3x, double p3y,
            double epsilon = Epsilon)
        {
            // Initialize the intersection.
            var result = new Intersection(IntersectionState.NoIntersection);

            // Translate the line to the origin.
            var A = y2 - y1;
            var B = x1 - x2;

            var C = x1 * (y1 - y2) + y1 * (x2 - x1);

            var xCoeff = CubicBezierCoefficients(p0x, p1x, p2x, p3x);
            var yCoeff = CubicBezierCoefficients(p0y, p1y, p2y, p3y);

            List<double> roots;

            // Fix for missing intersections for curves that can be reduced to lower degrees.
            // Figure out whether the handles and ends are parallel.
            var determinant = (x2 - x1) * (p3y - p2y + p1y - p0y) - (y2 - y1) * (p3x - p2x + p1x - p0x);
            if (Abs(determinant) < epsilon)
                roots = QuadraticRoots(
                    /* t^2 */ A * xCoeff.C + B * yCoeff.C,
                    /* t^1 */ A * xCoeff.B + B * yCoeff.B,
                    /* C^0 */ A * xCoeff.A + B * yCoeff.A + C,
                    epsilon);
            else
                roots = CubicRoots(
                    /* t^3 */ A * xCoeff.D + B * yCoeff.D,
                    /* t^2 */ A * xCoeff.C + B * yCoeff.C,
                    /* t^1 */ A * xCoeff.B + B * yCoeff.B,
                    /* C^0 */ A * xCoeff.A + B * yCoeff.A + C,
                    epsilon);

            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];

                // Add intersection point.
                if (!(t < 0 || t > 1d))
                    result.AppendPoint(new Point2D(
                        xCoeff.D * t * t * t + xCoeff.C * t * t + xCoeff.B * t + xCoeff.A,
                        yCoeff.D * t * t * t + yCoeff.C * t * t + yCoeff.B * t + yCoeff.A));
            }

            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// Find the intersection between a line and a polygon contour.
        /// </summary>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <param name="points"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LinePolygonContourIntersection(
            double a1X, double a1Y, double a2X, double a2Y,
            List<Point2D> points,
            double epsilon = Epsilon)
        {
            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            var length = points.Count;

            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            Point2D b1 = points[length - 1];
            for (var i = 0; i < length; ++i)
            {
                Point2D b2 = points[i];

                intersections.UnionWith(LineLineSegmentIntersection(a1X, a1Y, a2X, a2Y, b1.X, b1.Y, b2.X, b2.Y, epsilon).Points);

                b1 = b2;
            }

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// Find the intersection between a line and a rectangle.
        /// </summary>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineRectangleIntersection(
            double a1X, double a1Y, double a2X, double a2Y,
            double r1X, double r1Y, double r2X, double r2Y,
            double epsilon = Epsilon)
        {
            var min = MinPoint(r1X, r1Y, r2X, r2Y);
            var max = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);

            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            intersections.UnionWith(LineLineSegmentIntersection(a1X, a1Y, a2X, a2Y, min.X, min.Y, topRight.X, topRight.Y, epsilon).Points);
            intersections.UnionWith(LineLineSegmentIntersection(a1X, a1Y, a2X, a2Y, topRight.X, topRight.Y, max.X, max.Y, epsilon).Points);
            intersections.UnionWith(LineLineSegmentIntersection(a1X, a1Y, a2X, a2Y, max.X, max.Y, bottomLeft.X, bottomLeft.Y, epsilon).Points);
            intersections.UnionWith(LineLineSegmentIntersection(a1X, a1Y, a2X, a2Y, bottomLeft.X, bottomLeft.Y, min.X, min.Y, epsilon).Points);

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Count > 0)
                result.State = IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// Find the intersection between a line and a circle.
        /// </summary>
        /// <param name="lAX"></param>
        /// <param name="lAY"></param>
        /// <param name="lBX"></param>
        /// <param name="lBY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineCircleIntersection(
            double lAX, double lAY, double lBX, double lBY,
            double cX, double cY, double r,
            double epsilon = Epsilon)
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
        /// Find the intersection between a line and a circular arc.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineCircularArcIntersection(
            double lAX, double lAY, double lBX, double lBY,
            double cX, double cY, double r, double angle, double startAngle, double sweepAngle,
            double epsilon = Epsilon)
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
        /// Find the intersection between a line and an ellipse.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineEllipseIntersection(
            double x0, double y0, double x1, double y1,
            double cx, double cy, double rx, double ry, double angle,
            double epsilon = Epsilon)
            => LineEllipseIntersection(x0, y0, x1, y1, cx, cy, rx, ry, Cos(angle), Sin(angle), epsilon);

        /// <summary>
        /// Find the intersection between a line and an ellipse.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2012/09/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineEllipseIntersection(
            double x0, double y0, double x1, double y1,
            double cx, double cy, double rx, double ry, double cosA, double sinA,
            double epsilon = Epsilon)
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

            // ToDo: Return IntersectionState.Inside if both points are inside the Ellipse.

            // Return the intersections.
            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// Find the intersection between a line and an elliptical arc.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineEllipticalArcIntersection(
            double x0, double y0, double x1, double y1,
            double cx, double cy, double rx, double ry, double angle, double startAngle, double sweepAngle,
            double epsilon = Epsilon)
            => LineSegmentEllipseIntersection(x0, y0, x1, y1, cx, cy, rx, ry, Cos(angle), Sin(angle), epsilon);

        /// <summary>
        /// Find the intersection between a line and an elliptical arc.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2012/09/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineEllipticalArcIntersection(
            double x0, double y0, double x1, double y1,
            double cx, double cy, double rx, double ry, double cosA, double sinA, double startAngle, double sweepAngle,
            double epsilon = Epsilon)
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
        /// Find the intersection between two rays.
        /// </summary>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <param name="b1X"></param>
        /// <param name="b1Y"></param>
        /// <param name="b2X"></param>
        /// <param name="b2Y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection RayRayIntersection(
            double a1X, double a1Y, double a2X, double a2Y,
            double b1X, double b1Y, double b2X, double b2Y,
            double epsilon = Epsilon)
        {
            var ua_t = (b2X - b1X) * (a1Y - b1Y) - (b2Y - b1Y) * (a1X - b1X);
            var ub_t = (a2X - a1X) * (a1Y - b1Y) - (a2Y - a1Y) * (a1X - b1X);
            var u_b = (b2Y - b1Y) * (a2X - a1X) - (b2X - b1X) * (a2Y - a1Y);

            var result = new Intersection(IntersectionState.NoIntersection);

            if (u_b != 0)
            {
                var ua = ua_t / u_b;
                result.State = IntersectionState.Intersection;
                result.AppendPoint(new Point2D(a1X + ua * (a2X - a1X), a1Y + ua * (a2Y - a1Y)));
            }
            else
            {
                if (ua_t == 0 || ub_t == 0)
                {
                    result.State |= IntersectionState.Coincident;
                }
                else
                {
                    result.State |= IntersectionState.Parallel;
                }
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between two line segments.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="b1X"></param>
        /// <param name="b1Y"></param>
        /// <param name="b2X"></param>
        /// <param name="b2Y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentLineSegmentIntersection(
            double x1, double y1, double x2, double y2,
            double b1X, double b1Y, double b2X, double b2Y,
            double epsilon = Epsilon)
        {
            var result = new Intersection(IntersectionState.NoIntersection);
            var ua_t = (b2X - b1X) * (y1 - b1Y) - (b2Y - b1Y) * (x1 - b1X);
            var ub_t = (x2 - x1) * (y1 - b1Y) - (y2 - y1) * (x1 - b1X);
            var u_b = (b2Y - b1Y) * (x2 - x1) - (b2X - b1X) * (y2 - y1);
            if (u_b != 0)
            {
                var ua = ua_t / u_b;
                var ub = ub_t / u_b;
                if (0 <= ua && ua <= 1 && 0 <= ub && ub <= 1)
                {
                    result.State = IntersectionState.Intersection;
                    result.AppendPoint(new Point2D(x1 + ua * (x2 - x1), y1 + ua * (y2 - y1)));
                }
                else
                {
                    result.State = IntersectionState.NoIntersection;
                }
            }
            else
            {
                if (ua_t == 0 || ub_t == 0)
                {
                    result.State = IntersectionState.Coincident;
                }
                else
                {
                    result.State = IntersectionState.Parallel;
                }
            }
            return result;
        }

        /// <summary>
        /// Find the intersection between a line segment and a quadratic bezier.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// Adapted from code found at: https://www.particleincell.com/2013/cubic-line-intersection/
        /// Which was based off of code found at: http://stackoverflow.com/questions/14005096/mathematical-solution-for-bezier-curve-and-line-intersection-in-coffeescript-or
        /// Which was based off of code found at: http://www.blitzbasic.com/Community/posts.php?topic=64459
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentQuadraticBezierSegmentIntersection(
            double x1, double y1, double x2, double y2,
            double p0x, double p0y, double p1x, double p1y, double p2x, double p2y,
            double epsilon = Epsilon)
        {
            // Initialize the intersection.
            var result = new Intersection(IntersectionState.NoIntersection);

            // Translate the line to the origin.
            var A = y2 - y1;
            var B = x1 - x2;

            var C = x1 * (y1 - y2) + y1 * (x2 - x1);

            var xCoeff = QuadraticBezierCoefficients(p0x, p1x, p2x);
            var yCoeff = QuadraticBezierCoefficients(p0y, p1y, p2y);

            List<double> roots;
            roots = QuadraticRoots(
                /* t^2 */ A * xCoeff.C + B * yCoeff.C,
                /* t^1 */ A * xCoeff.B + B * yCoeff.B,
                /* C^0 */ A * xCoeff.A + B * yCoeff.A + C
                );

            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];

                // Intersection point assuming it was an infinitely long line.
                var x = xCoeff.C * t * t + xCoeff.B * t + xCoeff.A;
                var y = yCoeff.C * t * t + yCoeff.B * t + yCoeff.A;

                double slope;
                // Special handling for vertical lines.
                if ((x2 - x1) != 0)
                    slope = (x - x1) / (x2 - x1);
                else
                    slope = (y - y1) / (y2 - y1);

                // Make sure we are in bounds of the line segment.
                if (!(t < 0 || t > 1d || slope < 0 || slope > 1d))
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
        /// Find the intersection between a line segment and a cubic bezier.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// Adapted from code found at: https://www.particleincell.com/2013/cubic-line-intersection/
        /// Which was based off of code found at: http://stackoverflow.com/questions/14005096/mathematical-solution-for-bezier-curve-and-line-intersection-in-coffeescript-or
        /// Which was based off of code found at: http://www.blitzbasic.com/Community/posts.php?topic=64459
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentCubicBezierSegmentIntersection(
            double x1, double y1, double x2, double y2,
            double p0x, double p0y, double p1x, double p1y, double p2x, double p2y, double p3x, double p3y,
            double epsilon = Epsilon)
        {
            // Initialize the intersection.
            var result = new Intersection(IntersectionState.NoIntersection);

            // Translate the line to the origin.
            var A = y2 - y1;
            var B = x1 - x2;

            var C = x1 * (y1 - y2) + y1 * (x2 - x1);

            var xCoeff = CubicBezierCoefficients(p0x, p1x, p2x, p3x);
            var yCoeff = CubicBezierCoefficients(p0y, p1y, p2y, p3y);

            List<double> roots;
            // Fix for missing intersections for curves that can be reduced to lower degrees due to parallel components.
            var determinant = (x2 - x1) * (p3y - p2y + p1y - p0y) - (y2 - y1) * (p3x - p2x + p1x - p0x);
            if (Abs(determinant) < epsilon)
                roots = QuadraticRoots(
                    /* t^2 */ A * xCoeff.C + B * yCoeff.C,
                    /* t^1 */ A * xCoeff.B + B * yCoeff.B,
                    /* C^0 */   A * xCoeff.A + B * yCoeff.A + C
                    );
            else
                roots = CubicRoots(
                    /* t^3 */ A * xCoeff.D + B * yCoeff.D,
                    /* t^2 */ A * xCoeff.C + B * yCoeff.C,
                    /* t^1 */ A * xCoeff.B + B * yCoeff.B,
                    /* C^0 */ A * xCoeff.A + B * yCoeff.A + C
                    );

            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];

                // Intersection point assuming infinitely long line segment.
                var point = new Point2D(
                    xCoeff.D * t * t * t + xCoeff.C * t * t + xCoeff.B * t + xCoeff.A,
                    yCoeff.D * t * t * t + yCoeff.C * t * t + yCoeff.B * t + yCoeff.A);

                double slope;

                // Special handling for vertical lines.
                if ((x2 - x1) != 0)
                    slope = (point.X - x1) / (x2 - x1);
                else
                    slope = (point.Y - y1) / (y2 - y1);

                // Make sure we are in bounds of the line segment.
                if (!(t < 0 || t > 1d || slope < 0 || slope > 1d))
                {
                    // Add intersection point.
                    result.AppendPoint(point);
                }
            }

            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// Find the intersection between a line segment and a rectangle.
        /// </summary>
        /// <param name="lAX"></param>
        /// <param name="lAY"></param>
        /// <param name="lBX"></param>
        /// <param name="lBY"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentRectangleIntersection(
            double lAX, double lAY, double lBX, double lBY,
            double r1X, double r1Y, double r2X, double r2Y,
            double epsilon = Epsilon)
        {
            var topLeft = MinPoint(r1X, r1Y, r2X, r2Y);
            var bottomRight = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(bottomRight.X, topLeft.Y);
            var bottomLeft = new Point2D(topLeft.X, bottomRight.Y);

            // ToDo: Return IntersectionState.Inside if both end points are inside the rectangle.

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
        /// Find the intersection between a line segment and a polygon contour.
        /// </summary>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <param name="points"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentPolygonContourIntersection(
            double a1X, double a1Y, double a2X, double a2Y,
            List<Point2D> points,
            double epsilon = Epsilon)
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

            // ToDo: Return IntersectionState.Inside if both end points are inside the polygon, and there are no intersections.

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// Find the intersection between a line segment and a circle.
        /// </summary>
        /// <param name="lAX"></param>
        /// <param name="lAY"></param>
        /// <param name="lBX"></param>
        /// <param name="lBY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="angle"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentCircleIntersection(
            double lAX, double lAY, double lBX, double lBY,
            double cX, double cY, double r, double angle,
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
        /// Find the intersection between a line segment and a circular arc.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentCircularArcIntersection(
            double lAX, double lAY, double lBX, double lBY,
            double cX, double cY, double r, double angle, double startAngle, double sweepAngle,
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
        /// Find the intersection between a line segment and an ellipse.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentEllipseIntersection(
            double x0, double y0, double x1, double y1,
            double cx, double cy, double rx, double ry, double angle,
            double epsilon = Epsilon)
            => LineSegmentEllipseIntersection(x0, y0, x1, y1, cx, cy, rx, ry, Cos(angle), Sin(angle), epsilon);

        /// <summary>
        /// Find the intersection between a line segment and an ellipse.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2012/09/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentEllipseIntersection(
            double x0, double y0, double x1, double y1,
            double cx, double cy, double rx, double ry, double cosA, double sinA,
            double epsilon = Epsilon)
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

            // ToDo: Return IntersectionState.Inside if both end points are inside the ellipse.

            // Return the intersections.
            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// Find the intersection between a line segment and an elliptical arc.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentEllipticalArcIntersection(
            double x0, double y0, double x1, double y1,
            double cx, double cy, double rx, double ry, double angle, double startAngle, double sweepAngle,
            double epsilon = Epsilon)
            => LineSegmentEllipseIntersection(x0, y0, x1, y1, cx, cy, rx, ry, Cos(angle), Sin(angle), epsilon);

        /// <summary>
        /// Find the intersection between a line segment and an elliptical arc.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2012/09/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentEllipticalArcIntersection(
            double x0, double y0,
            double cx, double x1, double y1, double cy, double rx, double ry, double cosA, double sinA, double startAngle, double sweepAngle,
            double epsilon = Epsilon)
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
        /// Find the intersection between two quadratic beziers.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// A combination of the method ideas found at: https://www.particleincell.com/2013/cubic-line-intersection/
        /// and the intersections methods at: http://www.kevlindev.com/ also found at: https://github.com/thelonious/kld-intersections/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentQuadraticBezierSegmentIntersection(
            double a1X, double a1Y, double a2X, double a2Y, double a3X, double a3Y,
            double b1X, double b1Y, double b2X, double b2Y, double b3X, double b3Y,
            double epsilon = Epsilon)
        {
            // Initialize the intersection.
            var result = new Intersection(IntersectionState.NoIntersection);

            // ToDo: Break early if the AABB of the ends and handles do not intersect.
            // Todo: Break early if the AABB of the curve does not intersect.

            // Parametric matrix form of the Bezier curves
            var xCoeffA = QuadraticBezierCoefficients(a1X, a2X, a3X);
            var yCoeffA = QuadraticBezierCoefficients(a1Y, a2Y, a3Y);
            var xCoeffB = QuadraticBezierCoefficients(b1X, b2X, b3X);
            var yCoeffB = QuadraticBezierCoefficients(b1Y, b2Y, b3Y);

            List<double> roots = null;

            // ToDo: Find the intersections of Bezier curves where all of the nodes are parallel.

            if (yCoeffA.C == 0)
            {
                var v0 = xCoeffA.C * (yCoeffA.A - yCoeffB.A);
                var v1 = v0 - xCoeffA.B * yCoeffA.B;
                var v2 = v0 + v1;
                var v3 = yCoeffA.B * yCoeffA.B;

                roots = QuarticRoots(
                    /* t^4 */ xCoeffA.C * yCoeffB.C * yCoeffB.C,
                    /* t^3 */ 2 * xCoeffA.C * yCoeffB.B * yCoeffB.C,
                    /* t^2 */ xCoeffA.C * yCoeffB.B * yCoeffB.B - xCoeffB.C * v3 - yCoeffB.C * v0 - yCoeffB.C * v1,
                    /* t^1 */ -xCoeffB.B * v3 - yCoeffB.B * v0 - yCoeffB.B * v1,
                    /* C^0 */ (xCoeffA.A - xCoeffB.A) * v3 + (yCoeffA.A - yCoeffB.A) * v1,
                    epsilon);
            }
            else
            {
                var v0 = xCoeffA.C * yCoeffB.C - yCoeffA.C * xCoeffB.C;
                var v1 = xCoeffA.C * yCoeffB.B - xCoeffB.B * yCoeffA.C;
                var v2 = xCoeffA.B * yCoeffA.C - yCoeffA.B * xCoeffA.C;
                var v3 = yCoeffA.A - yCoeffB.A;
                var v4 = yCoeffA.C * (xCoeffA.A - xCoeffB.A) - xCoeffA.C * v3;
                var v5 = -yCoeffA.B * v2 + yCoeffA.C * v4;
                var v6 = v2 * v2;

                roots = (v0 == 0)
                    ? QuadraticRoots(
                        /* t^2 */ (-yCoeffB.C * v6 + yCoeffA.C * v1 * v1 + yCoeffA.C * v0 * v4 + v0 * v5) / yCoeffA.C,
                        /* t^1 */ (-yCoeffB.B * v6 + yCoeffA.C * v1 * v4 + v1 * v5) / yCoeffA.C,
                        /* C^0 */ (v3 * v6 + v4 * v5) / yCoeffA.C,
                        epsilon)
                    : QuarticRoots(
                        /* t^4 */ v0 * v0,
                        /* t^3 */ 2 * v0 * v1,
                        /* t^2 */ (-yCoeffB.C * v6 + yCoeffA.C * v1 * v1 + yCoeffA.C * v0 * v4 + v0 * v5) / yCoeffA.C,
                        /* t^1 */ (-yCoeffB.B * v6 + yCoeffA.C * v1 * v4 + v1 * v5) / yCoeffA.C,
                        /* C^0 */ (v3 * v6 + v4 * v5) / yCoeffA.C,
                        epsilon);
            }

            foreach (var s in roots)
            {
                var point = new Point2D(
                    xCoeffB.C * s * s + xCoeffB.B * s + xCoeffB.A,
                    yCoeffB.C * s * s + yCoeffB.B * s + yCoeffB.A);
                if (s >= 0 && s <= 1)
                {
                    var xRoots = (xCoeffA.C == 0)
                        ? LinearRoots(
                            /* t^1 */ -xCoeffA.B,
                            /* C^0 */ -xCoeffA.A + point.X,
                            epsilon)
                        : QuadraticRoots(
                            /* t^2 */ -xCoeffA.C,
                            /* t^1 */ -xCoeffA.B,
                            /* C^0 */ -xCoeffA.A + point.X,
                            epsilon);
                    var yRoots = (yCoeffA.C == 0)
                        ? LinearRoots(
                            /* t^1 */ -yCoeffA.B,
                            /* C^0 */ -yCoeffA.A + point.Y,
                            epsilon)
                        : QuadraticRoots(
                            /* t^2 */ -yCoeffA.C,
                            /* t^1 */ -yCoeffA.B,
                            /* C^0 */ -yCoeffA.A + point.Y,
                            epsilon);

                    if (xRoots.Count > 0 && yRoots.Count > 0)
                    {
                        // Find the nearest matching x and y roots in the ranges 0 < x < 1; 0 < y < 1.
                        foreach (var xRoot in xRoots)
                        {
                            if (xRoot >= 0 && xRoot <= 1)
                            {
                                foreach (var yRoot in yRoots)
                                {
                                    var t = xRoot - yRoot;
                                    if ((t >= 0 ? t : -t) < epsilon)
                                    {
                                        result.AppendPoint(point);
                                        goto checkRoots; // Break through two levels of foreach loops to exit early. Using goto for performance.
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

        /// <summary>
        /// Find the intersection between a quadratic bezier and a cubic bezier.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// This is a performance improved rewrite of a method ported from: http://www.kevlindev.com/ also found at: https://github.com/thelonious/kld-intersections/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentCubicBezierSegmentIntersection(
            double a1X, double a1Y, double a2X, double a2Y, double a3X, double a3Y,
            double b1X, double b1Y, double b2X, double b2Y, double b3X, double b3Y, double b4X, double b4Y,
            double epsilon = Epsilon)
        {
            var result = new Intersection(IntersectionState.NoIntersection);
            // ToDo: Break early if the AABB bounding box of the curve does not intersect.

            // The tolerance is off by too much. Need to find the error.
            var tolerance = 4294967295 * epsilon; // 1e-4;

            var xCoeffA = QuadraticBezierCoefficients(a1X, a2X, a3X);
            var yCoeffA = QuadraticBezierCoefficients(a1Y, a2Y, a3Y);
            var xCoeffB = CubicBezierCoefficients(b1X, b2X, b3X, b4X);
            var yCoeffB = CubicBezierCoefficients(b1Y, b2Y, b3Y, b4Y);

            var cAAx2 = xCoeffA.C * xCoeffA.C;
            var cAAy2 = yCoeffA.C * yCoeffA.C;
            var cABx2 = xCoeffA.B * xCoeffA.B;
            var cABy2 = yCoeffA.B * yCoeffA.B;
            var cACx2 = xCoeffA.A * xCoeffA.A;
            var cACy2 = yCoeffA.A * yCoeffA.A;

            var cBAx2 = xCoeffB.D * xCoeffB.D;
            var cBAy2 = yCoeffB.D * yCoeffB.D;
            var cBBx2 = xCoeffB.C * xCoeffB.C;
            var cBBy2 = yCoeffB.C * yCoeffB.C;
            var cBCx2 = xCoeffB.B * xCoeffB.B;
            var cBCy2 = yCoeffB.B * yCoeffB.B;
            var cBDx2 = xCoeffB.A * xCoeffB.A;
            var cBDy2 = yCoeffB.A * yCoeffB.A;

            var roots = new Polynomial(
                /* t^6 */ -2 * xCoeffA.A * yCoeffA.A * xCoeffA.C * yCoeffA.C - xCoeffA.A * xCoeffA.B * yCoeffA.B * yCoeffA.C - yCoeffA.A * xCoeffA.B * yCoeffA.B * xCoeffA.C + 2 * xCoeffA.A * xCoeffA.C * yCoeffB.A * yCoeffA.C + 2 * yCoeffA.A * xCoeffB.A * xCoeffA.C * yCoeffA.C + xCoeffA.B * xCoeffB.A * yCoeffA.B * yCoeffA.C + xCoeffA.B * yCoeffA.B * xCoeffA.C * yCoeffB.A - 2 * xCoeffB.A * xCoeffA.C * yCoeffB.A * yCoeffA.C - 2 * xCoeffA.A * xCoeffB.A * cAAy2 + xCoeffA.A * cABy2 * xCoeffA.C + yCoeffA.A * cABx2 * yCoeffA.C - 2 * yCoeffA.A * cAAx2 * yCoeffB.A - xCoeffB.A * cABy2 * xCoeffA.C - cABx2 * yCoeffB.A * yCoeffA.C + cACx2 * cAAy2 + cACy2 * cAAx2 + cBDx2 * cAAy2 + cAAx2 * cBDy2,
                /* t^5 */ 2 * xCoeffA.A * xCoeffA.C * yCoeffA.C * yCoeffB.B + 2 * yCoeffA.A * xCoeffA.C * xCoeffB.B * yCoeffA.C + xCoeffA.B * yCoeffA.B * xCoeffA.C * yCoeffB.B + xCoeffA.B * yCoeffA.B * xCoeffB.B * yCoeffA.C - 2 * xCoeffB.A * xCoeffA.C * yCoeffA.C * yCoeffB.B - 2 * xCoeffA.C * yCoeffB.A * xCoeffB.B * yCoeffA.C - 2 * xCoeffA.A * xCoeffB.B * cAAy2 - 2 * yCoeffA.A * cAAx2 * yCoeffB.B + 2 * xCoeffB.A * xCoeffB.B * cAAy2 - cABy2 * xCoeffA.C * xCoeffB.B - cABx2 * yCoeffA.C * yCoeffB.B + 2 * cAAx2 * yCoeffB.A * yCoeffB.B,
                /* t^4 */ 2 * xCoeffA.A * xCoeffA.C * yCoeffA.C * yCoeffB.C + 2 * yCoeffA.A * xCoeffA.C * yCoeffA.C * xCoeffB.C + xCoeffA.B * yCoeffA.B * xCoeffA.C * yCoeffB.C + xCoeffA.B * yCoeffA.B * yCoeffA.C * xCoeffB.C - 2 * xCoeffB.A * xCoeffA.C * yCoeffA.C * yCoeffB.C - 2 * xCoeffA.C * yCoeffB.A * yCoeffA.C * xCoeffB.C - 2 * xCoeffA.C * xCoeffB.B * yCoeffA.C * yCoeffB.B - 2 * xCoeffA.A * cAAy2 * xCoeffB.C - 2 * yCoeffA.A * cAAx2 * yCoeffB.C + 2 * xCoeffB.A * cAAy2 * xCoeffB.C - cABy2 * xCoeffA.C * xCoeffB.C - cABx2 * yCoeffA.C * yCoeffB.C + cBCx2 * cAAy2 + cAAx2 * (2 * yCoeffB.A * yCoeffB.C + cBCy2),
                /* t^3 */ 2 * xCoeffA.A * xCoeffA.C * yCoeffA.C * yCoeffB.D + 2 * yCoeffA.A * xCoeffA.C * yCoeffA.C * xCoeffB.D + xCoeffA.B * yCoeffA.B * xCoeffA.C * yCoeffB.D + xCoeffA.B * yCoeffA.B * yCoeffA.C * xCoeffB.D - 2 * xCoeffB.A * xCoeffA.C * yCoeffA.C * yCoeffB.D - 2 * xCoeffA.C * yCoeffB.A * yCoeffA.C * xCoeffB.D - 2 * xCoeffA.C * xCoeffB.B * yCoeffA.C * yCoeffB.C - 2 * xCoeffA.C * yCoeffA.C * yCoeffB.B * xCoeffB.C - 2 * xCoeffA.A * cAAy2 * xCoeffB.D - 2 * yCoeffA.A * cAAx2 * yCoeffB.D + 2 * xCoeffB.A * cAAy2 * xCoeffB.D + 2 * xCoeffB.B * cAAy2 * xCoeffB.C - cABy2 * xCoeffA.C * xCoeffB.D - cABx2 * yCoeffA.C * yCoeffB.D + cAAx2 * (2 * yCoeffB.A * yCoeffB.D + 2 * yCoeffB.B * yCoeffB.C),
                /* t^2 */ -2 * xCoeffA.C * xCoeffB.B * yCoeffA.C * yCoeffB.D - 2 * xCoeffA.C * yCoeffA.C * yCoeffB.B * xCoeffB.D - 2 * xCoeffA.C * yCoeffA.C * xCoeffB.C * yCoeffB.C + 2 * xCoeffB.B * cAAy2 * xCoeffB.D + cAAy2 * cBBx2 + cAAx2 * (2 * yCoeffB.B * yCoeffB.D + cBBy2),
                /* t^1 */ -2 * xCoeffA.C * yCoeffA.C * xCoeffB.C * yCoeffB.D - 2 * xCoeffA.C * yCoeffA.C * yCoeffB.C * xCoeffB.D + 2 * cAAy2 * xCoeffB.C * xCoeffB.D + 2 * cAAx2 * yCoeffB.C * yCoeffB.D,
                /* t^0 */ -2 * xCoeffA.C * yCoeffA.C * xCoeffB.D * yCoeffB.D + cAAx2 * cBAy2 + cAAy2 * cBAx2
            ).RootsInInterval();

            foreach (var s in roots)
            {
                var point = new Point2D(
                   xCoeffB.D * s * s * s + xCoeffB.C * s * s + xCoeffB.B * s + xCoeffB.A,
                   yCoeffB.D * s * s * s + yCoeffB.C * s * s + yCoeffB.B * s + yCoeffB.A);

                var xRoots = (xCoeffA.C == 0)
                    ? LinearRoots(
                        /* t^1 */ xCoeffA.B,
                        /* t^0 */ xCoeffA.A - point.X,
                        epsilon)
                    : QuadraticRoots(
                        /* t^2 */ xCoeffA.C,
                        /* t^1 */ xCoeffA.B,
                        /* t^0 */ xCoeffA.A - point.X,
                        epsilon);

                var yRoots = (yCoeffA.C == 0)
                    ? LinearRoots(
                        /* t^1 */ yCoeffA.B,
                        /* t^0 */ yCoeffA.A - point.Y,
                        epsilon)
                    : QuadraticRoots(
                        /* t^2 */ yCoeffA.C,
                        /* t^1 */ yCoeffA.B,
                        /* t^0 */ yCoeffA.A - point.Y,
                        epsilon);

                if (xRoots.Count > 0 && yRoots.Count > 0)
                {
                    foreach (var xRoot in xRoots)
                    {
                        if (0 <= xRoot && xRoot <= 1)
                        {
                            foreach (var yRoot in yRoots)
                            {
                                if (Abs(xRoot - yRoot) < tolerance)
                                {
                                    result.AppendPoint(point);
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
        /// Find the intersection between a quadratic bezier and a polygon contour.
        /// </summary>
        /// <param name="p1X"></param>
        /// <param name="p1Y"></param>
        /// <param name="p2X"></param>
        /// <param name="p2Y"></param>
        /// <param name="p3X"></param>
        /// <param name="p3Y"></param>
        /// <param name="points"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentPolygonContourIntersection(
            double p1X, double p1Y, double p2X, double p2Y, double p3X, double p3Y,
            List<Point2D> points,
            double epsilon = Epsilon)
        {
            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            var length = points.Count;

            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            Point2D a1 = points[length - 1];
            for (var i = 0; i < length; ++i)
            {
                Point2D a2 = points[i];

                intersections.UnionWith(LineSegmentQuadraticBezierSegmentIntersection(a1.X, a1.Y, a2.X, a2.Y, p1X, p1Y, p2X, p2Y, p3X, p3Y, epsilon).Points);

                a1 = a2;
            }

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// Find the intersection between a quadratic bezier and a rectangle.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentRectangleIntersection(
            double p1X, double p1Y, double p2X, double p2Y, double p3X, double p3Y,
            double r1X, double r1Y, double r2X, double r2Y,
            double epsilon = Epsilon)
        {
            var min = MinPoint(r1X, r1Y, r2X, r2Y);
            var max = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);

            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            intersections.UnionWith(LineSegmentQuadraticBezierSegmentIntersection(min.X, min.Y, topRight.X, topRight.Y, p1X, p1Y, p2X, p2Y, p3X, p3Y, epsilon).Points);
            intersections.UnionWith(LineSegmentQuadraticBezierSegmentIntersection(topRight.X, topRight.Y, max.X, max.Y, p1X, p1Y, p2X, p2Y, p3X, p3Y, epsilon).Points);
            intersections.UnionWith(LineSegmentQuadraticBezierSegmentIntersection(max.X, max.Y, bottomLeft.X, bottomLeft.Y, p1X, p1Y, p2X, p2Y, p3X, p3Y, epsilon).Points);
            intersections.UnionWith(LineSegmentQuadraticBezierSegmentIntersection(bottomLeft.X, bottomLeft.Y, min.X, min.Y, p1X, p1Y, p2X, p2Y, p3X, p3Y, epsilon).Points);

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Points.Count > 0)
                result.State = IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// Find the intersection between two cubic beziers.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// This is a performance improved rewrite of a method ported from: http://www.kevlindev.com/ also found at: https://github.com/thelonious/kld-intersections/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierSegmentCubicBezierSegmentIntersection(
            double a1X, double a1Y, double a2X, double a2Y, double a3X, double a3Y, double a4X, double a4Y,
            double b1X, double b1Y, double b2X, double b2Y, double b3X, double b3Y, double b4X, double b4Y,
            double epsilon = Epsilon)
        {
            var result = new Intersection(IntersectionState.NoIntersection);

            // ToDo: Break early if the AABB bounding box of the curve does not intersect.

            // The tolerance is off by too much. Need to find the error.
            var tolerance = 4194303 * epsilon;

            // Parametric matrix form of the Bezier curves
            var xCoeffA = CubicBezierCoefficients(a1X, a2X, a3X, a4X);
            var yCoeffA = CubicBezierCoefficients(a1Y, a2Y, a3Y, a4Y);
            var xCoeffB = CubicBezierCoefficients(b1X, b2X, b3X, b4X);
            var yCoeffB = CubicBezierCoefficients(b1Y, b2Y, b3Y, b4Y);

            var c10x2 = xCoeffA.A * xCoeffA.A;
            var c10x3 = xCoeffA.A * xCoeffA.A * xCoeffA.A;
            var c10y2 = yCoeffA.A * yCoeffA.A;
            var c10y3 = yCoeffA.A * yCoeffA.A * yCoeffA.A;
            var c11x2 = xCoeffA.B * xCoeffA.B;
            var c11x3 = xCoeffA.B * xCoeffA.B * xCoeffA.B;
            var c11y2 = yCoeffA.B * yCoeffA.B;
            var c11y3 = yCoeffA.B * yCoeffA.B * yCoeffA.B;
            var c12x2 = xCoeffA.C * xCoeffA.C;
            var c12x3 = xCoeffA.C * xCoeffA.C * xCoeffA.C;
            var c12y2 = yCoeffA.C * yCoeffA.C;
            var c12y3 = yCoeffA.C * yCoeffA.C * yCoeffA.C;
            var c13x2 = xCoeffA.D * xCoeffA.D;
            var c13x3 = xCoeffA.D * xCoeffA.D * xCoeffA.D;
            var c13y2 = yCoeffA.D * yCoeffA.D;
            var c13y3 = yCoeffA.D * yCoeffA.D * yCoeffA.D;
            var c20x2 = xCoeffB.A * xCoeffB.A;
            var c20x3 = xCoeffB.A * xCoeffB.A * xCoeffB.A;
            var c20y2 = yCoeffB.A * yCoeffB.A;
            var c20y3 = yCoeffB.A * yCoeffB.A * yCoeffB.A;
            var c21x2 = xCoeffB.B * xCoeffB.B;
            var c21x3 = xCoeffB.B * xCoeffB.B * xCoeffB.B;
            var c21y2 = yCoeffB.B * yCoeffB.B;
            var c22x2 = xCoeffB.C * xCoeffB.C;
            var c22x3 = xCoeffB.C * xCoeffB.C * xCoeffB.C;
            var c22y2 = yCoeffB.C * yCoeffB.C;
            var c23x2 = xCoeffB.D * xCoeffB.D;
            var c23x3 = xCoeffB.D * xCoeffB.D * xCoeffB.D;
            var c23y2 = yCoeffB.D * yCoeffB.D;
            var c23y3 = yCoeffB.D * yCoeffB.D * yCoeffB.D;

            var poly = new Polynomial(
                /* t^9 */ xCoeffA.A * yCoeffA.A * xCoeffA.B * yCoeffA.C * xCoeffA.D * yCoeffA.D - xCoeffA.A * yCoeffA.A * yCoeffA.B * xCoeffA.C * xCoeffA.D * yCoeffA.D + xCoeffA.A * xCoeffA.B * yCoeffA.B * xCoeffA.C * yCoeffA.C * yCoeffA.D - yCoeffA.A * xCoeffA.B * yCoeffA.B * xCoeffA.C * yCoeffA.C * xCoeffA.D - xCoeffA.A * xCoeffA.B * yCoeffB.A * yCoeffA.C * xCoeffA.D * yCoeffA.D + 6 * xCoeffA.A * xCoeffB.A * yCoeffA.B * yCoeffA.C * xCoeffA.D * yCoeffA.D + xCoeffA.A * yCoeffA.B * xCoeffA.C * yCoeffB.A * xCoeffA.D * yCoeffA.D - yCoeffA.A * xCoeffA.B * xCoeffB.A * yCoeffA.C * xCoeffA.D * yCoeffA.D - 6 * yCoeffA.A * xCoeffA.B * xCoeffA.C * yCoeffB.A * xCoeffA.D * yCoeffA.D + yCoeffA.A * xCoeffB.A * yCoeffA.B * xCoeffA.C * xCoeffA.D * yCoeffA.D - xCoeffA.B * xCoeffB.A * yCoeffA.B * xCoeffA.C * yCoeffA.C * yCoeffA.D + xCoeffA.B * yCoeffA.B * xCoeffA.C * yCoeffB.A * yCoeffA.C * xCoeffA.D + xCoeffA.B * xCoeffB.A * yCoeffB.A * yCoeffA.C * xCoeffA.D * yCoeffA.D - xCoeffB.A * yCoeffA.B * xCoeffA.C * yCoeffB.A * xCoeffA.D * yCoeffA.D - 2 * xCoeffA.A * xCoeffB.A * c12y3 * xCoeffA.D + 2 * yCoeffA.A * c12x3 * yCoeffB.A * yCoeffA.D - 3 * xCoeffA.A * yCoeffA.A * xCoeffA.B * xCoeffA.C * c13y2 - 6 * xCoeffA.A * yCoeffA.A * xCoeffB.A * xCoeffA.D * c13y2 + 3 * xCoeffA.A * yCoeffA.A * yCoeffA.B * yCoeffA.C * c13x2 - 2 * xCoeffA.A * yCoeffA.A * xCoeffA.C * c12y2 * xCoeffA.D - 2 * xCoeffA.A * xCoeffA.B * xCoeffB.A * yCoeffA.C * c13y2 - xCoeffA.A * xCoeffA.B * yCoeffA.B * c12y2 * xCoeffA.D + 3 * xCoeffA.A * xCoeffA.B * xCoeffA.C * yCoeffB.A * c13y2 - 4 * xCoeffA.A * xCoeffB.A * yCoeffA.B * xCoeffA.C * c13y2 + 3 * yCoeffA.A * xCoeffA.B * xCoeffB.A * xCoeffA.C * c13y2 + 6 * xCoeffA.A * yCoeffA.A * yCoeffB.A * c13x2 * yCoeffA.D + 2 * xCoeffA.A * yCoeffA.A * c12x2 * yCoeffA.C * yCoeffA.D + 2 * xCoeffA.A * xCoeffA.B * c11y2 * xCoeffA.D * yCoeffA.D + 2 * xCoeffA.A * xCoeffB.A * xCoeffA.C * c12y2 * yCoeffA.D + 6 * xCoeffA.A * xCoeffB.A * yCoeffB.A * xCoeffA.D * c13y2 - 3 * xCoeffA.A * yCoeffA.B * yCoeffB.A * yCoeffA.C * c13x2 + 2 * xCoeffA.A * xCoeffA.C * yCoeffB.A * c12y2 * xCoeffA.D + xCoeffA.A * c11y2 * xCoeffA.C * yCoeffA.C * xCoeffA.D + yCoeffA.A * xCoeffA.B * yCoeffA.B * c12x2 * yCoeffA.D + 4 * yCoeffA.A * xCoeffA.B * yCoeffB.A * yCoeffA.C * c13x2 - 3 * yCoeffA.A * xCoeffB.A * yCoeffA.B * yCoeffA.C * c13x2 + 2 * yCoeffA.A * xCoeffB.A * xCoeffA.C * c12y2 * xCoeffA.D + 2 * yCoeffA.A * yCoeffA.B * xCoeffA.C * yCoeffB.A * c13x2 + xCoeffA.B * xCoeffB.A * yCoeffA.B * c12y2 * xCoeffA.D - 3 * xCoeffA.B * xCoeffB.A * xCoeffA.C * yCoeffB.A * c13y2 - 2 * xCoeffA.A * c12x2 * yCoeffB.A * yCoeffA.C * yCoeffA.D - 6 * yCoeffA.A * xCoeffB.A * yCoeffB.A * c13x2 * yCoeffA.D - 2 * yCoeffA.A * xCoeffB.A * c12x2 * yCoeffA.C * yCoeffA.D - 2 * yCoeffA.A * c11x2 * yCoeffA.B * xCoeffA.D * yCoeffA.D - yCoeffA.A * c11x2 * xCoeffA.C * yCoeffA.C * yCoeffA.D - 2 * yCoeffA.A * c12x2 * yCoeffB.A * yCoeffA.C * xCoeffA.D - 2 * xCoeffA.B * xCoeffB.A * c11y2 * xCoeffA.D * yCoeffA.D - xCoeffA.B * yCoeffA.B * c12x2 * yCoeffB.A * yCoeffA.D + 3 * xCoeffB.A * yCoeffA.B * yCoeffB.A * yCoeffA.C * c13x2 - 2 * xCoeffB.A * xCoeffA.C * yCoeffB.A * c12y2 * xCoeffA.D - xCoeffB.A * c11y2 * xCoeffA.C * yCoeffA.C * xCoeffA.D + 3 * c10y2 * xCoeffA.B * xCoeffA.C * xCoeffA.D * yCoeffA.D + 3 * xCoeffA.B * xCoeffA.C * c20y2 * xCoeffA.D * yCoeffA.D + 2 * xCoeffB.A * c12x2 * yCoeffB.A * yCoeffA.C * yCoeffA.D - 3 * c10x2 * yCoeffA.B * yCoeffA.C * xCoeffA.D * yCoeffA.D + 2 * c11x2 * yCoeffA.B * yCoeffB.A * xCoeffA.D * yCoeffA.D + c11x2 * xCoeffA.C * yCoeffB.A * yCoeffA.C * yCoeffA.D - 3 * c20x2 * yCoeffA.B * yCoeffA.C * xCoeffA.D * yCoeffA.D - c10x3 * c13y3 + c10y3 * c13x3 + c20x3 * c13y3 - c20y3 * c13x3 - 3 * xCoeffA.A * c20x2 * c13y3 - xCoeffA.A * c11y3 * c13x2 + 3 * c10x2 * xCoeffB.A * c13y3 + yCoeffA.A * c11x3 * c13y2 + 3 * yCoeffA.A * c20y2 * c13x3 + xCoeffB.A * c11y3 * c13x2 + c10x2 * c12y3 * xCoeffA.D - 3 * c10y2 * yCoeffB.A * c13x3 - c10y2 * c12x3 * yCoeffA.D + c20x2 * c12y3 * xCoeffA.D - c11x3 * yCoeffB.A * c13y2 - c12x3 * c20y2 * yCoeffA.D - xCoeffA.A * c11x2 * yCoeffA.B * c13y2 + yCoeffA.A * xCoeffA.B * c11y2 * c13x2 - 3 * xCoeffA.A * c10y2 * c13x2 * yCoeffA.D - xCoeffA.A * c11y2 * c12x2 * yCoeffA.D + yCoeffA.A * c11x2 * c12y2 * xCoeffA.D - xCoeffA.B * c11y2 * yCoeffB.A * c13x2 + 3 * c10x2 * yCoeffA.A * xCoeffA.D * c13y2 + c10x2 * xCoeffA.B * yCoeffA.C * c13y2 + 2 * c10x2 * yCoeffA.B * xCoeffA.C * c13y2 - 2 * c10y2 * xCoeffA.B * yCoeffA.C * c13x2 - c10y2 * yCoeffA.B * xCoeffA.C * c13x2 + c11x2 * xCoeffB.A * yCoeffA.B * c13y2 - 3 * xCoeffA.A * c20y2 * c13x2 * yCoeffA.D + 3 * yCoeffA.A * c20x2 * xCoeffA.D * c13y2 + xCoeffA.B * c20x2 * yCoeffA.C * c13y2 - 2 * xCoeffA.B * c20y2 * yCoeffA.C * c13x2 + xCoeffB.A * c11y2 * c12x2 * yCoeffA.D - yCoeffA.B * xCoeffA.C * c20y2 * c13x2 - c10x2 * xCoeffA.C * c12y2 * yCoeffA.D - 3 * c10x2 * yCoeffB.A * xCoeffA.D * c13y2 + 3 * c10y2 * xCoeffB.A * c13x2 * yCoeffA.D + c10y2 * c12x2 * yCoeffA.C * xCoeffA.D - c11x2 * yCoeffB.A * c12y2 * xCoeffA.D + 2 * c20x2 * yCoeffA.B * xCoeffA.C * c13y2 + 3 * xCoeffB.A * c20y2 * c13x2 * yCoeffA.D - c20x2 * xCoeffA.C * c12y2 * yCoeffA.D - 3 * c20x2 * yCoeffB.A * xCoeffA.D * c13y2 + c12x2 * c20y2 * yCoeffA.C * xCoeffA.D,
                /* t^8 */ -xCoeffA.A * xCoeffA.B * yCoeffA.C * xCoeffA.D * yCoeffB.B * yCoeffA.D + xCoeffA.A * yCoeffA.B * xCoeffA.C * xCoeffA.D * yCoeffB.B * yCoeffA.D + 6 * xCoeffA.A * yCoeffA.B * xCoeffB.B * yCoeffA.C * xCoeffA.D * yCoeffA.D - 6 * yCoeffA.A * xCoeffA.B * xCoeffA.C * xCoeffA.D * yCoeffB.B * yCoeffA.D - yCoeffA.A * xCoeffA.B * xCoeffB.B * yCoeffA.C * xCoeffA.D * yCoeffA.D + yCoeffA.A * yCoeffA.B * xCoeffA.C * xCoeffB.B * xCoeffA.D * yCoeffA.D - xCoeffA.B * yCoeffA.B * xCoeffA.C * xCoeffB.B * yCoeffA.C * yCoeffA.D + xCoeffA.B * yCoeffA.B * xCoeffA.C * yCoeffA.C * xCoeffA.D * yCoeffB.B + xCoeffA.B * xCoeffB.A * yCoeffA.C * xCoeffA.D * yCoeffB.B * yCoeffA.D + 6 * xCoeffA.B * xCoeffA.C * yCoeffB.A * xCoeffA.D * yCoeffB.B * yCoeffA.D + xCoeffA.B * yCoeffB.A * xCoeffB.B * yCoeffA.C * xCoeffA.D * yCoeffA.D - xCoeffB.A * yCoeffA.B * xCoeffA.C * xCoeffA.D * yCoeffB.B * yCoeffA.D - 6 * xCoeffB.A * yCoeffA.B * xCoeffB.B * yCoeffA.C * xCoeffA.D * yCoeffA.D - yCoeffA.B * xCoeffA.C * yCoeffB.A * xCoeffB.B * xCoeffA.D * yCoeffA.D - 6 * xCoeffA.A * xCoeffB.A * xCoeffB.B * c13y3 - 2 * xCoeffA.A * xCoeffB.B * c12y3 * xCoeffA.D + 6 * yCoeffA.A * yCoeffB.A * c13x3 * yCoeffB.B + 2 * xCoeffB.A * xCoeffB.B * c12y3 * xCoeffA.D + 2 * yCoeffA.A * c12x3 * yCoeffB.B * yCoeffA.D - 2 * c12x3 * yCoeffB.A * yCoeffB.B * yCoeffA.D - 6 * xCoeffA.A * yCoeffA.A * xCoeffB.B * xCoeffA.D * c13y2 + 3 * xCoeffA.A * xCoeffA.B * xCoeffA.C * yCoeffB.B * c13y2 - 2 * xCoeffA.A * xCoeffA.B * xCoeffB.B * yCoeffA.C * c13y2 - 4 * xCoeffA.A * yCoeffA.B * xCoeffA.C * xCoeffB.B * c13y2 + 3 * yCoeffA.A * xCoeffA.B * xCoeffA.C * xCoeffB.B * c13y2 + 6 * xCoeffA.A * yCoeffA.A * c13x2 * yCoeffB.B * yCoeffA.D + 6 * xCoeffA.A * xCoeffB.A * xCoeffA.D * yCoeffB.B * c13y2 - 3 * xCoeffA.A * yCoeffA.B * yCoeffA.C * c13x2 * yCoeffB.B + 2 * xCoeffA.A * xCoeffA.C * xCoeffB.B * c12y2 * yCoeffA.D + 2 * xCoeffA.A * xCoeffA.C * c12y2 * xCoeffA.D * yCoeffB.B + 6 * xCoeffA.A * yCoeffB.A * xCoeffB.B * xCoeffA.D * c13y2 + 4 * yCoeffA.A * xCoeffA.B * yCoeffA.C * c13x2 * yCoeffB.B + 6 * yCoeffA.A * xCoeffB.A * xCoeffB.B * xCoeffA.D * c13y2 + 2 * yCoeffA.A * yCoeffA.B * xCoeffA.C * c13x2 * yCoeffB.B - 3 * yCoeffA.A * yCoeffA.B * xCoeffB.B * yCoeffA.C * c13x2 + 2 * yCoeffA.A * xCoeffA.C * xCoeffB.B * c12y2 * xCoeffA.D - 3 * xCoeffA.B * xCoeffB.A * xCoeffA.C * yCoeffB.B * c13y2 + 2 * xCoeffA.B * xCoeffB.A * xCoeffB.B * yCoeffA.C * c13y2 + xCoeffA.B * yCoeffA.B * xCoeffB.B * c12y2 * xCoeffA.D - 3 * xCoeffA.B * xCoeffA.C * yCoeffB.A * xCoeffB.B * c13y2 + 4 * xCoeffB.A * yCoeffA.B * xCoeffA.C * xCoeffB.B * c13y2 - 6 * xCoeffA.A * yCoeffB.A * c13x2 * yCoeffB.B * yCoeffA.D - 2 * xCoeffA.A * c12x2 * yCoeffA.C * yCoeffB.B * yCoeffA.D - 6 * yCoeffA.A * xCoeffB.A * c13x2 * yCoeffB.B * yCoeffA.D - 6 * yCoeffA.A * yCoeffB.A * xCoeffB.B * c13x2 * yCoeffA.D - 2 * yCoeffA.A * c12x2 * xCoeffB.B * yCoeffA.C * yCoeffA.D - 2 * yCoeffA.A * c12x2 * yCoeffA.C * xCoeffA.D * yCoeffB.B - xCoeffA.B * yCoeffA.B * c12x2 * yCoeffB.B * yCoeffA.D - 4 * xCoeffA.B * yCoeffB.A * yCoeffA.C * c13x2 * yCoeffB.B - 2 * xCoeffA.B * c11y2 * xCoeffB.B * xCoeffA.D * yCoeffA.D + 3 * xCoeffB.A * yCoeffA.B * yCoeffA.C * c13x2 * yCoeffB.B - 2 * xCoeffB.A * xCoeffA.C * xCoeffB.B * c12y2 * yCoeffA.D - 2 * xCoeffB.A * xCoeffA.C * c12y2 * xCoeffA.D * yCoeffB.B - 6 * xCoeffB.A * yCoeffB.A * xCoeffB.B * xCoeffA.D * c13y2 - 2 * yCoeffA.B * xCoeffA.C * yCoeffB.A * c13x2 * yCoeffB.B + 3 * yCoeffA.B * yCoeffB.A * xCoeffB.B * yCoeffA.C * c13x2 - 2 * xCoeffA.C * yCoeffB.A * xCoeffB.B * c12y2 * xCoeffA.D - c11y2 * xCoeffA.C * xCoeffB.B * yCoeffA.C * xCoeffA.D + 6 * xCoeffB.A * yCoeffB.A * c13x2 * yCoeffB.B * yCoeffA.D + 2 * xCoeffB.A * c12x2 * yCoeffA.C * yCoeffB.B * yCoeffA.D + 2 * c11x2 * yCoeffA.B * xCoeffA.D * yCoeffB.B * yCoeffA.D + c11x2 * xCoeffA.C * yCoeffA.C * yCoeffB.B * yCoeffA.D + 2 * c12x2 * yCoeffB.A * xCoeffB.B * yCoeffA.C * yCoeffA.D + 2 * c12x2 * yCoeffB.A * yCoeffA.C * xCoeffA.D * yCoeffB.B + 3 * c10x2 * xCoeffB.B * c13y3 - 3 * c10y2 * c13x3 * yCoeffB.B + 3 * c20x2 * xCoeffB.B * c13y3 + c11y3 * xCoeffB.B * c13x2 - c11x3 * yCoeffB.B * c13y2 - 3 * c20y2 * c13x3 * yCoeffB.B - xCoeffA.B * c11y2 * c13x2 * yCoeffB.B + c11x2 * yCoeffA.B * xCoeffB.B * c13y2 - 3 * c10x2 * xCoeffA.D * yCoeffB.B * c13y2 + 3 * c10y2 * xCoeffB.B * c13x2 * yCoeffA.D - c11x2 * c12y2 * xCoeffA.D * yCoeffB.B + c11y2 * c12x2 * xCoeffB.B * yCoeffA.D - 3 * c20x2 * xCoeffA.D * yCoeffB.B * c13y2 + 3 * c20y2 * xCoeffB.B * c13x2 * yCoeffA.D,
                /* t^7 */ -xCoeffA.A * xCoeffA.B * yCoeffA.C * xCoeffA.D * yCoeffA.D * yCoeffB.C + xCoeffA.A * yCoeffA.B * xCoeffA.C * xCoeffA.D * yCoeffA.D * yCoeffB.C + 6 * xCoeffA.A * yCoeffA.B * yCoeffA.C * xCoeffA.D * xCoeffB.C * yCoeffA.D - 6 * yCoeffA.A * xCoeffA.B * xCoeffA.C * xCoeffA.D * yCoeffA.D * yCoeffB.C - yCoeffA.A * xCoeffA.B * yCoeffA.C * xCoeffA.D * xCoeffB.C * yCoeffA.D + yCoeffA.A * yCoeffA.B * xCoeffA.C * xCoeffA.D * xCoeffB.C * yCoeffA.D + xCoeffA.B * yCoeffA.B * xCoeffA.C * yCoeffA.C * xCoeffA.D * yCoeffB.C - xCoeffA.B * yCoeffA.B * xCoeffA.C * yCoeffA.C * xCoeffB.C * yCoeffA.D + xCoeffA.B * xCoeffB.A * yCoeffA.C * xCoeffA.D * yCoeffA.D * yCoeffB.C + xCoeffA.B * yCoeffB.A * yCoeffA.C * xCoeffA.D * xCoeffB.C * yCoeffA.D + xCoeffA.B * xCoeffB.B * yCoeffA.C * xCoeffA.D * yCoeffB.B * yCoeffA.D - xCoeffB.A * yCoeffA.B * xCoeffA.C * xCoeffA.D * yCoeffA.D * yCoeffB.C - 6 * xCoeffB.A * yCoeffA.B * yCoeffA.C * xCoeffA.D * xCoeffB.C * yCoeffA.D - yCoeffA.B * xCoeffA.C * yCoeffB.A * xCoeffA.D * xCoeffB.C * yCoeffA.D - yCoeffA.B * xCoeffA.C * xCoeffB.B * xCoeffA.D * yCoeffB.B * yCoeffA.D - 6 * xCoeffA.A * xCoeffB.A * xCoeffB.C * c13y3 - 2 * xCoeffA.A * c12y3 * xCoeffA.D * xCoeffB.C + 2 * xCoeffB.A * c12y3 * xCoeffA.D * xCoeffB.C + 2 * yCoeffA.A * c12x3 * yCoeffA.D * yCoeffB.C - 6 * xCoeffA.A * yCoeffA.A * xCoeffA.D * xCoeffB.C * c13y2 + 3 * xCoeffA.A * xCoeffA.B * xCoeffA.C * c13y2 * yCoeffB.C - 2 * xCoeffA.A * xCoeffA.B * yCoeffA.C * xCoeffB.C * c13y2 - 4 * xCoeffA.A * yCoeffA.B * xCoeffA.C * xCoeffB.C * c13y2 + 3 * yCoeffA.A * xCoeffA.B * xCoeffA.C * xCoeffB.C * c13y2 + 6 * xCoeffA.A * yCoeffA.A * c13x2 * yCoeffA.D * yCoeffB.C + 6 * xCoeffA.A * xCoeffB.A * xCoeffA.D * c13y2 * yCoeffB.C - 3 * xCoeffA.A * yCoeffA.B * yCoeffA.C * c13x2 * yCoeffB.C + 2 * xCoeffA.A * xCoeffA.C * c12y2 * xCoeffA.D * yCoeffB.C + 2 * xCoeffA.A * xCoeffA.C * c12y2 * xCoeffB.C * yCoeffA.D + 6 * xCoeffA.A * yCoeffB.A * xCoeffA.D * xCoeffB.C * c13y2 + 6 * xCoeffA.A * xCoeffB.B * xCoeffA.D * yCoeffB.B * c13y2 + 4 * yCoeffA.A * xCoeffA.B * yCoeffA.C * c13x2 * yCoeffB.C + 6 * yCoeffA.A * xCoeffB.A * xCoeffA.D * xCoeffB.C * c13y2 + 2 * yCoeffA.A * yCoeffA.B * xCoeffA.C * c13x2 * yCoeffB.C - 3 * yCoeffA.A * yCoeffA.B * yCoeffA.C * c13x2 * xCoeffB.C + 2 * yCoeffA.A * xCoeffA.C * c12y2 * xCoeffA.D * xCoeffB.C - 3 * xCoeffA.B * xCoeffB.A * xCoeffA.C * c13y2 * yCoeffB.C + 2 * xCoeffA.B * xCoeffB.A * yCoeffA.C * xCoeffB.C * c13y2 + xCoeffA.B * yCoeffA.B * c12y2 * xCoeffA.D * xCoeffB.C - 3 * xCoeffA.B * xCoeffA.C * yCoeffB.A * xCoeffB.C * c13y2 - 3 * xCoeffA.B * xCoeffA.C * xCoeffB.B * yCoeffB.B * c13y2 + 4 * xCoeffB.A * yCoeffA.B * xCoeffA.C * xCoeffB.C * c13y2 - 2 * xCoeffA.A * c12x2 * yCoeffA.C * yCoeffA.D * yCoeffB.C - 6 * yCoeffA.A * xCoeffB.A * c13x2 * yCoeffA.D * yCoeffB.C - 6 * yCoeffA.A * yCoeffB.A * c13x2 * xCoeffB.C * yCoeffA.D - 6 * yCoeffA.A * xCoeffB.B * c13x2 * yCoeffB.B * yCoeffA.D - 2 * yCoeffA.A * c12x2 * yCoeffA.C * xCoeffA.D * yCoeffB.C - 2 * yCoeffA.A * c12x2 * yCoeffA.C * xCoeffB.C * yCoeffA.D - xCoeffA.B * yCoeffA.B * c12x2 * yCoeffA.D * yCoeffB.C - 2 * xCoeffA.B * c11y2 * xCoeffA.D * xCoeffB.C * yCoeffA.D + 3 * xCoeffB.A * yCoeffA.B * yCoeffA.C * c13x2 * yCoeffB.C - 2 * xCoeffB.A * xCoeffA.C * c12y2 * xCoeffA.D * yCoeffB.C - 2 * xCoeffB.A * xCoeffA.C * c12y2 * xCoeffB.C * yCoeffA.D - 6 * xCoeffB.A * yCoeffB.A * xCoeffA.D * xCoeffB.C * c13y2 - 6 * xCoeffB.A * xCoeffB.B * xCoeffA.D * yCoeffB.B * c13y2 + 3 * yCoeffA.B * yCoeffB.A * yCoeffA.C * c13x2 * xCoeffB.C + 3 * yCoeffA.B * xCoeffB.B * yCoeffA.C * c13x2 * yCoeffB.B - 2 * xCoeffA.C * yCoeffB.A * c12y2 * xCoeffA.D * xCoeffB.C - 2 * xCoeffA.C * xCoeffB.B * c12y2 * xCoeffA.D * yCoeffB.B - c11y2 * xCoeffA.C * yCoeffA.C * xCoeffA.D * xCoeffB.C + 2 * xCoeffB.A * c12x2 * yCoeffA.C * yCoeffA.D * yCoeffB.C - 3 * yCoeffA.B * c21x2 * yCoeffA.C * xCoeffA.D * yCoeffA.D + 6 * yCoeffB.A * xCoeffB.B * c13x2 * yCoeffB.B * yCoeffA.D + 2 * c11x2 * yCoeffA.B * xCoeffA.D * yCoeffA.D * yCoeffB.C + c11x2 * xCoeffA.C * yCoeffA.C * yCoeffA.D * yCoeffB.C + 2 * c12x2 * yCoeffB.A * yCoeffA.C * xCoeffB.C * yCoeffA.D + 2 * c12x2 * xCoeffB.B * yCoeffA.C * yCoeffB.B * yCoeffA.D - 3 * xCoeffA.A * c21x2 * c13y3 + 3 * xCoeffB.A * c21x2 * c13y3 + 3 * c10x2 * xCoeffB.C * c13y3 - 3 * c10y2 * c13x3 * yCoeffB.C + 3 * c20x2 * xCoeffB.C * c13y3 + c21x2 * c12y3 * xCoeffA.D + c11y3 * c13x2 * xCoeffB.C - c11x3 * c13y2 * yCoeffB.C + 3 * yCoeffA.A * c21x2 * xCoeffA.D * c13y2 - xCoeffA.B * c11y2 * c13x2 * yCoeffB.C + xCoeffA.B * c21x2 * yCoeffA.C * c13y2 + 2 * yCoeffA.B * xCoeffA.C * c21x2 * c13y2 + c11x2 * yCoeffA.B * xCoeffB.C * c13y2 - xCoeffA.C * c21x2 * c12y2 * yCoeffA.D - 3 * yCoeffB.A * c21x2 * xCoeffA.D * c13y2 - 3 * c10x2 * xCoeffA.D * c13y2 * yCoeffB.C + 3 * c10y2 * c13x2 * xCoeffB.C * yCoeffA.D - c11x2 * c12y2 * xCoeffA.D * yCoeffB.C + c11y2 * c12x2 * xCoeffB.C * yCoeffA.D - 3 * c20x2 * xCoeffA.D * c13y2 * yCoeffB.C + 3 * c20y2 * c13x2 * xCoeffB.C * yCoeffA.D + c12x2 * yCoeffA.C * xCoeffA.D * (2 * yCoeffB.A * yCoeffB.C + c21y2) + xCoeffA.B * xCoeffA.C * xCoeffA.D * yCoeffA.D * (6 * yCoeffB.A * yCoeffB.C + 3 * c21y2) + c12x3 * yCoeffA.D * (-2 * yCoeffB.A * yCoeffB.C - c21y2) + yCoeffA.A * c13x3 * (6 * yCoeffB.A * yCoeffB.C + 3 * c21y2) + yCoeffA.B * xCoeffA.C * c13x2 * (-2 * yCoeffB.A * yCoeffB.C - c21y2) + xCoeffA.B * yCoeffA.C * c13x2 * (-4 * yCoeffB.A * yCoeffB.C - 2 * c21y2) + xCoeffA.A * c13x2 * yCoeffA.D * (-6 * yCoeffB.A * yCoeffB.C - 3 * c21y2) + xCoeffB.A * c13x2 * yCoeffA.D * (6 * yCoeffB.A * yCoeffB.C + 3 * c21y2) + c13x3 * (-2 * yCoeffB.A * c21y2 - c20y2 * yCoeffB.C - yCoeffB.A * (2 * yCoeffB.A * yCoeffB.C + c21y2)),
                /* t^6 */ -xCoeffA.A * xCoeffA.B * yCoeffA.C * xCoeffA.D * yCoeffA.D * yCoeffB.D + xCoeffA.A * yCoeffA.B * xCoeffA.C * xCoeffA.D * yCoeffA.D * yCoeffB.D + 6 * xCoeffA.A * yCoeffA.B * yCoeffA.C * xCoeffA.D * yCoeffA.D * xCoeffB.D - 6 * yCoeffA.A * xCoeffA.B * xCoeffA.C * xCoeffA.D * yCoeffA.D * yCoeffB.D - yCoeffA.A * xCoeffA.B * yCoeffA.C * xCoeffA.D * yCoeffA.D * xCoeffB.D + yCoeffA.A * yCoeffA.B * xCoeffA.C * xCoeffA.D * yCoeffA.D * xCoeffB.D + xCoeffA.B * yCoeffA.B * xCoeffA.C * yCoeffA.C * xCoeffA.D * yCoeffB.D - xCoeffA.B * yCoeffA.B * xCoeffA.C * yCoeffA.C * yCoeffA.D * xCoeffB.D + xCoeffA.B * xCoeffB.A * yCoeffA.C * xCoeffA.D * yCoeffA.D * yCoeffB.D + xCoeffA.B * yCoeffB.A * yCoeffA.C * xCoeffA.D * yCoeffA.D * xCoeffB.D + xCoeffA.B * xCoeffB.B * yCoeffA.C * xCoeffA.D * yCoeffA.D * yCoeffB.C + xCoeffA.B * yCoeffA.C * xCoeffA.D * yCoeffB.B * xCoeffB.C * yCoeffA.D - xCoeffB.A * yCoeffA.B * xCoeffA.C * xCoeffA.D * yCoeffA.D * yCoeffB.D - 6 * xCoeffB.A * yCoeffA.B * yCoeffA.C * xCoeffA.D * yCoeffA.D * xCoeffB.D - yCoeffA.B * xCoeffA.C * yCoeffB.A * xCoeffA.D * yCoeffA.D * xCoeffB.D - yCoeffA.B * xCoeffA.C * xCoeffB.B * xCoeffA.D * yCoeffA.D * yCoeffB.C - yCoeffA.B * xCoeffA.C * xCoeffA.D * yCoeffB.B * xCoeffB.C * yCoeffA.D - 6 * yCoeffA.B * xCoeffB.B * yCoeffA.C * xCoeffA.D * xCoeffB.C * yCoeffA.D - 6 * xCoeffA.A * xCoeffB.A * c13y3 * xCoeffB.D - 6 * xCoeffA.A * xCoeffB.B * xCoeffB.C * c13y3 - 2 * xCoeffA.A * c12y3 * xCoeffA.D * xCoeffB.D + 6 * xCoeffB.A * xCoeffB.B * xCoeffB.C * c13y3 + 2 * xCoeffB.A * c12y3 * xCoeffA.D * xCoeffB.D + 2 * xCoeffB.B * c12y3 * xCoeffA.D * xCoeffB.C + 2 * yCoeffA.A * c12x3 * yCoeffA.D * yCoeffB.D - 6 * xCoeffA.A * yCoeffA.A * xCoeffA.D * c13y2 * xCoeffB.D + 3 * xCoeffA.A * xCoeffA.B * xCoeffA.C * c13y2 * yCoeffB.D - 2 * xCoeffA.A * xCoeffA.B * yCoeffA.C * c13y2 * xCoeffB.D - 4 * xCoeffA.A * yCoeffA.B * xCoeffA.C * c13y2 * xCoeffB.D + 3 * yCoeffA.A * xCoeffA.B * xCoeffA.C * c13y2 * xCoeffB.D + 6 * xCoeffA.A * yCoeffA.A * c13x2 * yCoeffA.D * yCoeffB.D + 6 * xCoeffA.A * xCoeffB.A * xCoeffA.D * c13y2 * yCoeffB.D - 3 * xCoeffA.A * yCoeffA.B * yCoeffA.C * c13x2 * yCoeffB.D + 2 * xCoeffA.A * xCoeffA.C * c12y2 * xCoeffA.D * yCoeffB.D + 2 * xCoeffA.A * xCoeffA.C * c12y2 * yCoeffA.D * xCoeffB.D + 6 * xCoeffA.A * yCoeffB.A * xCoeffA.D * c13y2 * xCoeffB.D + 6 * xCoeffA.A * xCoeffB.B * xCoeffA.D * c13y2 * yCoeffB.C + 6 * xCoeffA.A * xCoeffA.D * yCoeffB.B * xCoeffB.C * c13y2 + 4 * yCoeffA.A * xCoeffA.B * yCoeffA.C * c13x2 * yCoeffB.D + 6 * yCoeffA.A * xCoeffB.A * xCoeffA.D * c13y2 * xCoeffB.D + 2 * yCoeffA.A * yCoeffA.B * xCoeffA.C * c13x2 * yCoeffB.D - 3 * yCoeffA.A * yCoeffA.B * yCoeffA.C * c13x2 * xCoeffB.D + 2 * yCoeffA.A * xCoeffA.C * c12y2 * xCoeffA.D * xCoeffB.D + 6 * yCoeffA.A * xCoeffB.B * xCoeffA.D * xCoeffB.C * c13y2 - 3 * xCoeffA.B * xCoeffB.A * xCoeffA.C * c13y2 * yCoeffB.D + 2 * xCoeffA.B * xCoeffB.A * yCoeffA.C * c13y2 * xCoeffB.D + xCoeffA.B * yCoeffA.B * c12y2 * xCoeffA.D * xCoeffB.D - 3 * xCoeffA.B * xCoeffA.C * yCoeffB.A * c13y2 * xCoeffB.D - 3 * xCoeffA.B * xCoeffA.C * xCoeffB.B * c13y2 * yCoeffB.C - 3 * xCoeffA.B * xCoeffA.C * yCoeffB.B * xCoeffB.C * c13y2 + 2 * xCoeffA.B * xCoeffB.B * yCoeffA.C * xCoeffB.C * c13y2 + 4 * xCoeffB.A * yCoeffA.B * xCoeffA.C * c13y2 * xCoeffB.D + 4 * yCoeffA.B * xCoeffA.C * xCoeffB.B * xCoeffB.C * c13y2 - 2 * xCoeffA.A * c12x2 * yCoeffA.C * yCoeffA.D * yCoeffB.D - 6 * yCoeffA.A * xCoeffB.A * c13x2 * yCoeffA.D * yCoeffB.D - 6 * yCoeffA.A * yCoeffB.A * c13x2 * yCoeffA.D * xCoeffB.D - 6 * yCoeffA.A * xCoeffB.B * c13x2 * yCoeffA.D * yCoeffB.C - 2 * yCoeffA.A * c12x2 * yCoeffA.C * xCoeffA.D * yCoeffB.D - 2 * yCoeffA.A * c12x2 * yCoeffA.C * yCoeffA.D * xCoeffB.D - 6 * yCoeffA.A * c13x2 * yCoeffB.B * xCoeffB.C * yCoeffA.D - xCoeffA.B * yCoeffA.B * c12x2 * yCoeffA.D * yCoeffB.D - 2 * xCoeffA.B * c11y2 * xCoeffA.D * yCoeffA.D * xCoeffB.D + 3 * xCoeffB.A * yCoeffA.B * yCoeffA.C * c13x2 * yCoeffB.D - 2 * xCoeffB.A * xCoeffA.C * c12y2 * xCoeffA.D * yCoeffB.D - 2 * xCoeffB.A * xCoeffA.C * c12y2 * yCoeffA.D * xCoeffB.D - 6 * xCoeffB.A * yCoeffB.A * xCoeffA.D * c13y2 * xCoeffB.D - 6 * xCoeffB.A * xCoeffB.B * xCoeffA.D * c13y2 * yCoeffB.C - 6 * xCoeffB.A * xCoeffA.D * yCoeffB.B * xCoeffB.C * c13y2 + 3 * yCoeffA.B * yCoeffB.A * yCoeffA.C * c13x2 * xCoeffB.D + 3 * yCoeffA.B * xCoeffB.B * yCoeffA.C * c13x2 * yCoeffB.C + 3 * yCoeffA.B * yCoeffA.C * c13x2 * yCoeffB.B * xCoeffB.C - 2 * xCoeffA.C * yCoeffB.A * c12y2 * xCoeffA.D * xCoeffB.D - 2 * xCoeffA.C * xCoeffB.B * c12y2 * xCoeffA.D * yCoeffB.C - 2 * xCoeffA.C * xCoeffB.B * c12y2 * xCoeffB.C * yCoeffA.D - 2 * xCoeffA.C * c12y2 * xCoeffA.D * yCoeffB.B * xCoeffB.C - 6 * yCoeffB.A * xCoeffB.B * xCoeffA.D * xCoeffB.C * c13y2 - c11y2 * xCoeffA.C * yCoeffA.C * xCoeffA.D * xCoeffB.D + 2 * xCoeffB.A * c12x2 * yCoeffA.C * yCoeffA.D * yCoeffB.D + 6 * yCoeffB.A * c13x2 * yCoeffB.B * xCoeffB.C * yCoeffA.D + 2 * c11x2 * yCoeffA.B * xCoeffA.D * yCoeffA.D * yCoeffB.D + c11x2 * xCoeffA.C * yCoeffA.C * yCoeffA.D * yCoeffB.D + 2 * c12x2 * yCoeffB.A * yCoeffA.C * yCoeffA.D * xCoeffB.D + 2 * c12x2 * xCoeffB.B * yCoeffA.C * yCoeffA.D * yCoeffB.C + 2 * c12x2 * yCoeffA.C * yCoeffB.B * xCoeffB.C * yCoeffA.D + c21x3 * c13y3 + 3 * c10x2 * c13y3 * xCoeffB.D - 3 * c10y2 * c13x3 * yCoeffB.D + 3 * c20x2 * c13y3 * xCoeffB.D + c11y3 * c13x2 * xCoeffB.D - c11x3 * c13y2 * yCoeffB.D - xCoeffA.B * c11y2 * c13x2 * yCoeffB.D + c11x2 * yCoeffA.B * c13y2 * xCoeffB.D - 3 * c10x2 * xCoeffA.D * c13y2 * yCoeffB.D + 3 * c10y2 * c13x2 * yCoeffA.D * xCoeffB.D - c11x2 * c12y2 * xCoeffA.D * yCoeffB.D + c11y2 * c12x2 * yCoeffA.D * xCoeffB.D - 3 * c21x2 * xCoeffA.D * yCoeffB.B * c13y2 - 3 * c20x2 * xCoeffA.D * c13y2 * yCoeffB.D + 3 * c20y2 * c13x2 * yCoeffA.D * xCoeffB.D + xCoeffA.B * xCoeffA.C * xCoeffA.D * yCoeffA.D * (6 * yCoeffB.A * yCoeffB.D + 6 * yCoeffB.B * yCoeffB.C) + c12x3 * yCoeffA.D * (-2 * yCoeffB.A * yCoeffB.D - 2 * yCoeffB.B * yCoeffB.C) + yCoeffA.A * c13x3 * (6 * yCoeffB.A * yCoeffB.D + 6 * yCoeffB.B * yCoeffB.C) + yCoeffA.B * xCoeffA.C * c13x2 * (-2 * yCoeffB.A * yCoeffB.D - 2 * yCoeffB.B * yCoeffB.C) + c12x2 * yCoeffA.C * xCoeffA.D * (2 * yCoeffB.A * yCoeffB.D + 2 * yCoeffB.B * yCoeffB.C) + xCoeffA.B * yCoeffA.C * c13x2 * (-4 * yCoeffB.A * yCoeffB.D - 4 * yCoeffB.B * yCoeffB.C) + xCoeffA.A * c13x2 * yCoeffA.D * (-6 * yCoeffB.A * yCoeffB.D - 6 * yCoeffB.B * yCoeffB.C) + xCoeffB.A * c13x2 * yCoeffA.D * (6 * yCoeffB.A * yCoeffB.D + 6 * yCoeffB.B * yCoeffB.C) + xCoeffB.B * c13x2 * yCoeffA.D * (6 * yCoeffB.A * yCoeffB.C + 3 * c21y2) + c13x3 * (-2 * yCoeffB.A * yCoeffB.B * yCoeffB.C - c20y2 * yCoeffB.D - yCoeffB.B * (2 * yCoeffB.A * yCoeffB.C + c21y2) - yCoeffB.A * (2 * yCoeffB.A * yCoeffB.D + 2 * yCoeffB.B * yCoeffB.C)),
                /* t^5 */ xCoeffA.B * xCoeffB.B * yCoeffA.C * xCoeffA.D * yCoeffA.D * yCoeffB.D + xCoeffA.B * yCoeffA.C * xCoeffA.D * yCoeffB.B * yCoeffA.D * xCoeffB.D + xCoeffA.B * yCoeffA.C * xCoeffA.D * xCoeffB.C * yCoeffA.D * yCoeffB.C - yCoeffA.B * xCoeffA.C * xCoeffB.B * xCoeffA.D * yCoeffA.D * yCoeffB.D - yCoeffA.B * xCoeffA.C * xCoeffA.D * yCoeffB.B * yCoeffA.D * xCoeffB.D - yCoeffA.B * xCoeffA.C * xCoeffA.D * xCoeffB.C * yCoeffA.D * yCoeffB.C - 6 * yCoeffA.B * xCoeffB.B * yCoeffA.C * xCoeffA.D * yCoeffA.D * xCoeffB.D - 6 * xCoeffA.A * xCoeffB.B * c13y3 * xCoeffB.D + 6 * xCoeffB.A * xCoeffB.B * c13y3 * xCoeffB.D + 2 * xCoeffB.B * c12y3 * xCoeffA.D * xCoeffB.D + 6 * xCoeffA.A * xCoeffB.B * xCoeffA.D * c13y2 * yCoeffB.D + 6 * xCoeffA.A * xCoeffA.D * yCoeffB.B * c13y2 * xCoeffB.D + 6 * xCoeffA.A * xCoeffA.D * xCoeffB.C * c13y2 * yCoeffB.C + 6 * yCoeffA.A * xCoeffB.B * xCoeffA.D * c13y2 * xCoeffB.D - 3 * xCoeffA.B * xCoeffA.C * xCoeffB.B * c13y2 * yCoeffB.D - 3 * xCoeffA.B * xCoeffA.C * yCoeffB.B * c13y2 * xCoeffB.D - 3 * xCoeffA.B * xCoeffA.C * xCoeffB.C * c13y2 * yCoeffB.C + 2 * xCoeffA.B * xCoeffB.B * yCoeffA.C * c13y2 * xCoeffB.D + 4 * yCoeffA.B * xCoeffA.C * xCoeffB.B * c13y2 * xCoeffB.D - 6 * yCoeffA.A * xCoeffB.B * c13x2 * yCoeffA.D * yCoeffB.D - 6 * yCoeffA.A * c13x2 * yCoeffB.B * yCoeffA.D * xCoeffB.D - 6 * yCoeffA.A * c13x2 * xCoeffB.C * yCoeffA.D * yCoeffB.C - 6 * xCoeffB.A * xCoeffB.B * xCoeffA.D * c13y2 * yCoeffB.D - 6 * xCoeffB.A * xCoeffA.D * yCoeffB.B * c13y2 * xCoeffB.D - 6 * xCoeffB.A * xCoeffA.D * xCoeffB.C * c13y2 * yCoeffB.C + 3 * yCoeffA.B * xCoeffB.B * yCoeffA.C * c13x2 * yCoeffB.D - 3 * yCoeffA.B * yCoeffA.C * xCoeffA.D * c22x2 * yCoeffA.D + 3 * yCoeffA.B * yCoeffA.C * c13x2 * yCoeffB.B * xCoeffB.D + 3 * yCoeffA.B * yCoeffA.C * c13x2 * xCoeffB.C * yCoeffB.C - 2 * xCoeffA.C * xCoeffB.B * c12y2 * xCoeffA.D * yCoeffB.D - 2 * xCoeffA.C * xCoeffB.B * c12y2 * yCoeffA.D * xCoeffB.D - 2 * xCoeffA.C * c12y2 * xCoeffA.D * yCoeffB.B * xCoeffB.D - 2 * xCoeffA.C * c12y2 * xCoeffA.D * xCoeffB.C * yCoeffB.C - 6 * yCoeffB.A * xCoeffB.B * xCoeffA.D * c13y2 * xCoeffB.D - 6 * xCoeffB.B * xCoeffA.D * yCoeffB.B * xCoeffB.C * c13y2 + 6 * yCoeffB.A * c13x2 * yCoeffB.B * yCoeffA.D * xCoeffB.D + 2 * c12x2 * xCoeffB.B * yCoeffA.C * yCoeffA.D * yCoeffB.D + 2 * c12x2 * yCoeffA.C * yCoeffB.B * yCoeffA.D * xCoeffB.D + 2 * c12x2 * yCoeffA.C * xCoeffB.C * yCoeffA.D * yCoeffB.C - 3 * xCoeffA.A * c22x2 * c13y3 + 3 * xCoeffB.A * c22x2 * c13y3 + 3 * c21x2 * xCoeffB.C * c13y3 + c12y3 * xCoeffA.D * c22x2 + 3 * yCoeffA.A * xCoeffA.D * c22x2 * c13y2 + xCoeffA.B * yCoeffA.C * c22x2 * c13y2 + 2 * yCoeffA.B * xCoeffA.C * c22x2 * c13y2 - xCoeffA.C * c12y2 * c22x2 * yCoeffA.D - 3 * yCoeffB.A * xCoeffA.D * c22x2 * c13y2 - 3 * c21x2 * xCoeffA.D * c13y2 * yCoeffB.C + c12x2 * yCoeffA.C * xCoeffA.D * (2 * yCoeffB.B * yCoeffB.D + c22y2) + xCoeffA.B * xCoeffA.C * xCoeffA.D * yCoeffA.D * (6 * yCoeffB.B * yCoeffB.D + 3 * c22y2) + xCoeffB.B * c13x2 * yCoeffA.D * (6 * yCoeffB.A * yCoeffB.D + 6 * yCoeffB.B * yCoeffB.C) + c12x3 * yCoeffA.D * (-2 * yCoeffB.B * yCoeffB.D - c22y2) + yCoeffA.A * c13x3 * (6 * yCoeffB.B * yCoeffB.D + 3 * c22y2) + yCoeffA.B * xCoeffA.C * c13x2 * (-2 * yCoeffB.B * yCoeffB.D - c22y2) + xCoeffA.B * yCoeffA.C * c13x2 * (-4 * yCoeffB.B * yCoeffB.D - 2 * c22y2) + xCoeffA.A * c13x2 * yCoeffA.D * (-6 * yCoeffB.B * yCoeffB.D - 3 * c22y2) + c13x2 * xCoeffB.C * yCoeffA.D * (6 * yCoeffB.A * yCoeffB.C + 3 * c21y2) + xCoeffB.A * c13x2 * yCoeffA.D * (6 * yCoeffB.B * yCoeffB.D + 3 * c22y2) + c13x3 * (-2 * yCoeffB.A * yCoeffB.B * yCoeffB.D - yCoeffB.C * (2 * yCoeffB.A * yCoeffB.C + c21y2) - yCoeffB.A * (2 * yCoeffB.B * yCoeffB.D + c22y2) - yCoeffB.B * (2 * yCoeffB.A * yCoeffB.D + 2 * yCoeffB.B * yCoeffB.C)),
                /* t^4 */ 6 * xCoeffA.B * xCoeffA.C * xCoeffA.D * yCoeffA.D * yCoeffB.C * yCoeffB.D + xCoeffA.B * yCoeffA.C * xCoeffA.D * xCoeffB.C * yCoeffA.D * yCoeffB.D + xCoeffA.B * yCoeffA.C * xCoeffA.D * yCoeffA.D * yCoeffB.C * xCoeffB.D - yCoeffA.B * xCoeffA.C * xCoeffA.D * xCoeffB.C * yCoeffA.D * yCoeffB.D - yCoeffA.B * xCoeffA.C * xCoeffA.D * yCoeffA.D * yCoeffB.C * xCoeffB.D - 6 * yCoeffA.B * yCoeffA.C * xCoeffA.D * xCoeffB.C * yCoeffA.D * xCoeffB.D - 6 * xCoeffA.A * xCoeffB.C * c13y3 * xCoeffB.D + 6 * xCoeffB.A * xCoeffB.C * c13y3 * xCoeffB.D + 6 * yCoeffA.A * c13x3 * yCoeffB.C * yCoeffB.D + 2 * c12y3 * xCoeffA.D * xCoeffB.C * xCoeffB.D - 2 * c12x3 * yCoeffA.D * yCoeffB.C * yCoeffB.D + 6 * xCoeffA.A * xCoeffA.D * xCoeffB.C * c13y2 * yCoeffB.D + 6 * xCoeffA.A * xCoeffA.D * c13y2 * yCoeffB.C * xCoeffB.D + 6 * yCoeffA.A * xCoeffA.D * xCoeffB.C * c13y2 * xCoeffB.D - 3 * xCoeffA.B * xCoeffA.C * xCoeffB.C * c13y2 * yCoeffB.D - 3 * xCoeffA.B * xCoeffA.C * c13y2 * yCoeffB.C * xCoeffB.D + 2 * xCoeffA.B * yCoeffA.C * xCoeffB.C * c13y2 * xCoeffB.D + 4 * yCoeffA.B * xCoeffA.C * xCoeffB.C * c13y2 * xCoeffB.D - 6 * xCoeffA.A * c13x2 * yCoeffA.D * yCoeffB.C * yCoeffB.D - 6 * yCoeffA.A * c13x2 * xCoeffB.C * yCoeffA.D * yCoeffB.D - 6 * yCoeffA.A * c13x2 * yCoeffA.D * yCoeffB.C * xCoeffB.D - 4 * xCoeffA.B * yCoeffA.C * c13x2 * yCoeffB.C * yCoeffB.D - 6 * xCoeffB.A * xCoeffA.D * xCoeffB.C * c13y2 * yCoeffB.D - 6 * xCoeffB.A * xCoeffA.D * c13y2 * yCoeffB.C * xCoeffB.D - 2 * yCoeffA.B * xCoeffA.C * c13x2 * yCoeffB.C * yCoeffB.D + 3 * yCoeffA.B * yCoeffA.C * c13x2 * xCoeffB.C * yCoeffB.D + 3 * yCoeffA.B * yCoeffA.C * c13x2 * yCoeffB.C * xCoeffB.D - 2 * xCoeffA.C * c12y2 * xCoeffA.D * xCoeffB.C * yCoeffB.D - 2 * xCoeffA.C * c12y2 * xCoeffA.D * yCoeffB.C * xCoeffB.D - 2 * xCoeffA.C * c12y2 * xCoeffB.C * yCoeffA.D * xCoeffB.D - 6 * yCoeffB.A * xCoeffA.D * xCoeffB.C * c13y2 * xCoeffB.D - 6 * xCoeffB.B * xCoeffA.D * yCoeffB.B * c13y2 * xCoeffB.D - 6 * xCoeffB.B * xCoeffA.D * xCoeffB.C * c13y2 * yCoeffB.C + 6 * xCoeffB.A * c13x2 * yCoeffA.D * yCoeffB.C * yCoeffB.D + 2 * c12x2 * yCoeffA.C * xCoeffA.D * yCoeffB.C * yCoeffB.D + 2 * c12x2 * yCoeffA.C * xCoeffB.C * yCoeffA.D * yCoeffB.D + 2 * c12x2 * yCoeffA.C * yCoeffA.D * yCoeffB.C * xCoeffB.D + 3 * xCoeffB.B * c22x2 * c13y3 + 3 * c21x2 * c13y3 * xCoeffB.D - 3 * xCoeffA.D * yCoeffB.B * c22x2 * c13y2 - 3 * c21x2 * xCoeffA.D * c13y2 * yCoeffB.D + c13x2 * xCoeffB.C * yCoeffA.D * (6 * yCoeffB.A * yCoeffB.D + 6 * yCoeffB.B * yCoeffB.C) + c13x2 * yCoeffA.D * xCoeffB.D * (6 * yCoeffB.A * yCoeffB.C + 3 * c21y2) + xCoeffB.B * c13x2 * yCoeffA.D * (6 * yCoeffB.B * yCoeffB.D + 3 * c22y2) + c13x3 * (-2 * yCoeffB.A * yCoeffB.C * yCoeffB.D - yCoeffB.D * (2 * yCoeffB.A * yCoeffB.C + c21y2) - yCoeffB.B * (2 * yCoeffB.B * yCoeffB.D + c22y2) - yCoeffB.C * (2 * yCoeffB.A * yCoeffB.D + 2 * yCoeffB.B * yCoeffB.C)),
                /* t^3 */ xCoeffA.B * yCoeffA.C * xCoeffA.D * yCoeffA.D * xCoeffB.D * yCoeffB.D - yCoeffA.B * xCoeffA.C * xCoeffA.D * yCoeffA.D * xCoeffB.D * yCoeffB.D + 6 * xCoeffB.B * xCoeffB.C * c13y3 * xCoeffB.D + 3 * xCoeffA.B * xCoeffA.C * xCoeffA.D * yCoeffA.D * c23y2 + 6 * xCoeffA.A * xCoeffA.D * c13y2 * xCoeffB.D * yCoeffB.D - 3 * xCoeffA.B * xCoeffA.C * c13y2 * xCoeffB.D * yCoeffB.D - 3 * yCoeffA.B * yCoeffA.C * xCoeffA.D * yCoeffA.D * c23x2 - 6 * yCoeffA.A * c13x2 * yCoeffA.D * xCoeffB.D * yCoeffB.D - 6 * xCoeffB.A * xCoeffA.D * c13y2 * xCoeffB.D * yCoeffB.D + 3 * yCoeffA.B * yCoeffA.C * c13x2 * xCoeffB.D * yCoeffB.D - 2 * xCoeffA.C * c12y2 * xCoeffA.D * xCoeffB.D * yCoeffB.D - 6 * xCoeffB.B * xCoeffA.D * xCoeffB.C * c13y2 * yCoeffB.D - 6 * xCoeffB.B * xCoeffA.D * c13y2 * yCoeffB.C * xCoeffB.D - 6 * xCoeffA.D * yCoeffB.B * xCoeffB.C * c13y2 * xCoeffB.D + 6 * xCoeffB.B * c13x2 * yCoeffA.D * yCoeffB.C * yCoeffB.D + 2 * c12x2 * yCoeffA.C * yCoeffA.D * xCoeffB.D * yCoeffB.D + c22x3 * c13y3 - 3 * xCoeffA.A * c13y3 * c23x2 + 3 * yCoeffA.A * c13x3 * c23y2 + 3 * xCoeffB.A * c13y3 * c23x2 + c12y3 * xCoeffA.D * c23x2 - c12x3 * yCoeffA.D * c23y2 - 3 * xCoeffA.A * c13x2 * yCoeffA.D * c23y2 + 3 * yCoeffA.A * xCoeffA.D * c13y2 * c23x2 - 2 * xCoeffA.B * yCoeffA.C * c13x2 * c23y2 + xCoeffA.B * yCoeffA.C * c13y2 * c23x2 - yCoeffA.B * xCoeffA.C * c13x2 * c23y2 + 2 * yCoeffA.B * xCoeffA.C * c13y2 * c23x2 + 3 * xCoeffB.A * c13x2 * yCoeffA.D * c23y2 - xCoeffA.C * c12y2 * yCoeffA.D * c23x2 - 3 * yCoeffB.A * xCoeffA.D * c13y2 * c23x2 + c12x2 * yCoeffA.C * xCoeffA.D * c23y2 - 3 * xCoeffA.D * c22x2 * c13y2 * yCoeffB.C + c13x2 * yCoeffA.D * xCoeffB.D * (6 * yCoeffB.A * yCoeffB.D + 6 * yCoeffB.B * yCoeffB.C) + c13x2 * xCoeffB.C * yCoeffA.D * (6 * yCoeffB.B * yCoeffB.D + 3 * c22y2) + c13x3 * (-2 * yCoeffB.B * yCoeffB.C * yCoeffB.D - yCoeffB.A * c23y2 - yCoeffB.C * (2 * yCoeffB.B * yCoeffB.D + c22y2) - yCoeffB.D * (2 * yCoeffB.A * yCoeffB.D + 2 * yCoeffB.B * yCoeffB.C)),
                /* t^2 */ -6 * xCoeffB.B * xCoeffA.D * c13y2 * xCoeffB.D * yCoeffB.D - 6 * xCoeffA.D * xCoeffB.C * c13y2 * yCoeffB.C * xCoeffB.D + 6 * c13x2 * xCoeffB.C * yCoeffA.D * yCoeffB.C * yCoeffB.D + 3 * xCoeffB.B * c13y3 * c23x2 + 3 * c22x2 * c13y3 * xCoeffB.D + 3 * xCoeffB.B * c13x2 * yCoeffA.D * c23y2 - 3 * xCoeffA.D * yCoeffB.B * c13y2 * c23x2 - 3 * xCoeffA.D * c22x2 * c13y2 * yCoeffB.D + c13x2 * yCoeffA.D * xCoeffB.D * (6 * yCoeffB.B * yCoeffB.D + 3 * c22y2) + c13x3 * (-yCoeffB.B * c23y2 - 2 * c22y2 * yCoeffB.D - yCoeffB.D * (2 * yCoeffB.B * yCoeffB.D + c22y2)),
                /* t^1 */ -6 * xCoeffA.D * xCoeffB.C * c13y2 * xCoeffB.D * yCoeffB.D + 6 * c13x2 * yCoeffA.D * yCoeffB.C * xCoeffB.D * yCoeffB.D + 3 * xCoeffB.C * c13y3 * c23x2 - 3 * c13x3 * yCoeffB.C * c23y2 - 3 * xCoeffA.D * c13y2 * yCoeffB.C * c23x2 + 3 * c13x2 * xCoeffB.C * yCoeffA.D * c23y2,
                /* t^0 */ -c13x3 * c23y3 + c13y3 * c23x3 - 3 * xCoeffA.D * c13y2 * c23x2 * yCoeffB.D + 3 * c13x2 * yCoeffA.D * xCoeffB.D * c23y2
            );
            var roots = poly.RootsInInterval();

            foreach (var s in roots)
            {
                var point = new Point2D(
                    xCoeffB.D * s * s * s + xCoeffB.C * s * s + xCoeffB.B * s + xCoeffB.A,
                    yCoeffB.D * s * s * s + yCoeffB.C * s * s + yCoeffB.B * s + yCoeffB.A);

                var xRoots = (xCoeffA.D == 0)
                    ? (xCoeffA.C == 0)
                    ? LinearRoots(
                        /* t^1 */ xCoeffA.B,
                        /* t^0 */ xCoeffA.A - point.X,
                        epsilon)
                    : QuadraticRoots(
                        /* t^2 */ xCoeffA.C,
                        /* t^1 */ xCoeffA.B,
                        /* t^0 */ xCoeffA.A - point.X,
                        epsilon)
                    : CubicRoots(
                        /* t^3 */ xCoeffA.D,
                        /* t^2 */ xCoeffA.C,
                        /* t^1 */ xCoeffA.B,
                        /* t^0 */ xCoeffA.A - point.X,
                        epsilon);

                var yRoots = (yCoeffA.D == 0)
                    ? (yCoeffA.C == 0)
                    ? LinearRoots(
                        /* t^1 */ yCoeffA.B,
                        /* t^0 */ yCoeffA.A - point.Y,
                        epsilon)
                    : QuadraticRoots(
                        /* t^2 */ yCoeffA.C,
                        /* t^1 */ yCoeffA.B,
                        /* t^0 */ yCoeffA.A - point.Y,
                        epsilon)
                    : CubicRoots(
                        /* t^3 */ yCoeffA.D,
                        /* t^2 */ yCoeffA.C,
                        /* t^1 */ yCoeffA.B,
                        /* t^0 */ yCoeffA.A - point.Y,
                        epsilon);

                // ToDo: Figure out why the xRoots can be larger than 1 or smaller than 0 and still work...
                if (xRoots.Count > 0 && yRoots.Count > 0)
                {
                    // Find the nearest matching x and y roots in the ranges 0 < x < 1; 0 < y < 1.
                    foreach (var xRoot in xRoots)
                    {
                        //if (0 <= xRoot && xRoot <= 1)
                        {
                            foreach (var yRoot in yRoots)
                            {
                                //var t = xRoot - yRoot;
                                //if ((t >= 0 ? t : -t) < tolerance)
                                {
                                    result.Points.Add(point);
                                    goto checkRoots; // Break through two levels of foreach loops. Using goto for performance.
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
        /// Finds the intersection between a cubic bezier and a polygon.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierSegmentPolygonIntersection(
            double p1X, double p1Y, double p2X, double p2Y, double p3X, double p3Y, double p4X, double p4Y,
            List<Point2D> points,
            double epsilon = Epsilon)
        {
            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            var length = points.Count;

            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            Point2D a1 = points[length - 1];
            for (var i = 0; i < length; ++i)
            {
                Point2D a2 = points[i];

                intersections.UnionWith(LineSegmentCubicBezierSegmentIntersection(p1X, p1Y, p2X, p2Y, p3X, p3Y, p4X, p4Y, a1.X, a1.Y, a2.X, a2.Y, epsilon).Points);

                a1 = a2;
            }

            // ToDo: Return IntersectionState.Inside if both end points are inside the Polygon and there are no intersections.

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// Find the intersection between a cubic bezier and a rectangle.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierSegmentRectangleIntersection(
            double p1X, double p1Y, double p2X, double p2Y, double p3X, double p3Y, double p4X, double p4Y,
            double r1X, double r1Y, double r2X, double r2Y,
            double epsilon = Epsilon)
        {
            var min = MinPoint(r1X, r1Y, r2X, r2Y);
            var max = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);

            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            intersections.UnionWith(LineSegmentCubicBezierSegmentIntersection(p1X, p1Y, p2X, p2Y, p3X, p3Y, p4X, p4Y, min.X, min.Y, topRight.X, topRight.Y, epsilon).Points);
            intersections.UnionWith(LineSegmentCubicBezierSegmentIntersection(p1X, p1Y, p2X, p2Y, p3X, p3Y, p4X, p4Y, topRight.X, topRight.Y, max.X, max.Y, epsilon).Points);
            intersections.UnionWith(LineSegmentCubicBezierSegmentIntersection(p1X, p1Y, p2X, p2Y, p3X, p3Y, p4X, p4Y, max.X, max.Y, bottomLeft.X, bottomLeft.Y, epsilon).Points);
            intersections.UnionWith(LineSegmentCubicBezierSegmentIntersection(p1X, p1Y, p2X, p2Y, p3X, p3Y, p4X, p4Y, bottomLeft.X, bottomLeft.Y, min.X, min.Y, epsilon).Points);

            // ToDo: Return IntersectionState.Inside if both end points are inside the rectangle and there are no intersections.

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Points.Count > 0)
                result.State = IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// Find the intersection between two polygon contours.
        /// </summary>
        /// <param name="points1"></param>
        /// <param name="points2"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection PolygonContourPolygonContourIntersection(
            List<Point2D> points1,
            List<Point2D> points2,
            double epsilon = Epsilon)
        {
            var intersections = new HashSet<Point2D>();
            var length = points1.Count;

            // We shouldn't care about the ordering, we can start with the last segment for a slight performance boost.
            Point2D a1 = points1[length - 1];
            for (var i = 0; i < length; ++i)
            {
                Point2D a2 = points1[i];

                intersections.UnionWith(LineSegmentPolygonContourIntersection(a1.X, a1.Y, a2.X, a2.Y, points2, epsilon).Points);

                a1 = a2;
            }

            // ToDo: Return IntersectionState.Inside if all end points of a polygon are inside the other polygon and there are no intersections.

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Points.Count > 0)
                result.State |= IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// Find the intersection between a polygon and a rectangle.
        /// </summary>
        /// <param name="points"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection PolygonContourRectangleIntersection(
            List<Point2D> points,
            double r1X, double r1Y, double r2X, double r2Y,
            double epsilon = Epsilon)
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

            // ToDo: Return IntersectionState.Inside if all of the end points are contained inside the rectangle, or the points of the rectangle are inside the polygon, and there are no intersections.

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Points.Count > 0)
                result.State = IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// Find the intersection between two rectangles.
        /// </summary>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <param name="b1X"></param>
        /// <param name="b1Y"></param>
        /// <param name="b2X"></param>
        /// <param name="b2Y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection RectangleRectangleIntersection(
            double a1X, double a1Y, double a2X, double a2Y,
            double b1X, double b1Y, double b2X, double b2Y,
            double epsilon = Epsilon)
        {
            var min = MinPoint(a1X, a1Y, a2X, a2Y);
            var max = MaxPoint(a1X, a1Y, a2X, a2Y);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);

            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            intersections.UnionWith(LineSegmentRectangleIntersection(min.X, min.Y, topRight.X, topRight.Y, b1X, b1Y, b2X, b2Y, epsilon).Points);
            intersections.UnionWith(LineSegmentRectangleIntersection(topRight.X, topRight.Y, max.X, max.Y, b1X, b1Y, b2X, b2Y, epsilon).Points);
            intersections.UnionWith(LineSegmentRectangleIntersection(max.X, max.Y, bottomLeft.X, bottomLeft.Y, b1X, b1Y, b2X, b2Y, epsilon).Points);
            intersections.UnionWith(LineSegmentRectangleIntersection(bottomLeft.X, bottomLeft.Y, min.X, min.Y, b1X, b1Y, b2X, b2Y, epsilon).Points);

            // ToDo: Return IntersectionState.Inside if all of the points of one rectangle are inside the other rectangle.

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Points.Count > 0)
                result.State = IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// Find the intersection between a circle and a polygon.
        /// </summary>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="points"></param>
        /// <param name="angle"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CirclePolygonIntersection(
            double cX, double cY, double r,
            List<Point2D> points, double angle,
            double epsilon = Epsilon)
        {
            var result = new Intersection(IntersectionState.NoIntersection);
            var inter = new Intersection(IntersectionState.NoIntersection);
            var length = points.Count;

            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            Point2D a1 = points[length - 1];
            for (var i = 0; i < length; ++i)
            {
                Point2D a2 = points[i];

                inter = LineSegmentCircleIntersection(a1.X, a1.Y, a2.X, a2.Y, cX, cY, r, angle, epsilon);
                result.AppendPoints(inter.Points);

                a1 = a2;
            }

            // ToDo: Return IntersectionState.Inside if all of the points of the polygon are inside the circle.

            if (result.Points.Count > 0)
                result.State |= IntersectionState.Intersection;
            else
                result.State = inter.State;
            return result;
        }

        /// <summary>
        /// Find the intersection between a circle and a rectangle.
        /// </summary>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="angle"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CircleRectangleIntersection(
            double cX, double cY, double r, double angle,
            double r1X, double r1Y, double r2X, double r2Y,
            double epsilon = Epsilon)
        {
            var min = MinPoint(r1X, r1Y, r2X, r2Y);
            var max = MinPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(max.X, min.Y);
            var bottomLeft = new Point2D(min.X, max.Y);

            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            intersections.UnionWith(LineSegmentCircleIntersection(min.X, min.Y, topRight.X, topRight.Y, cX, cY, r, angle, epsilon).Points);
            intersections.UnionWith(LineSegmentCircleIntersection(topRight.X, topRight.Y, max.X, max.Y, cX, cY, r, angle, epsilon).Points);
            intersections.UnionWith(LineSegmentCircleIntersection(max.X, max.Y, bottomLeft.X, bottomLeft.Y, cX, cY, r, angle, epsilon).Points);
            intersections.UnionWith(LineSegmentCircleIntersection(bottomLeft.X, bottomLeft.Y, min.X, min.Y, cX, cY, r, angle, epsilon).Points);

            // ToDo: Return IntersectionState.Inside if all of the points of the rectangle are contained within the circle.

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Points.Count > 0)
                result.State = IntersectionState.Intersection;
            else result.State = LineSegmentCircleIntersection(min.X, min.Y, topRight.X, topRight.Y, cX, cY, r, angle, epsilon).State;
            return result;
        }

        /// <summary>
        /// Find intersection between two circles.
        /// </summary>
        /// <param name="cx0"></param>
        /// <param name="cy0"></param>
        /// <param name="radius0"></param>
        /// <param name="cx1"></param>
        /// <param name="cy1"></param>
        /// <param name="radius1"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/09/determine-where-two-circles-intersect-in-c/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CircleCircleIntersection1(
            double cx0, double cy0, double radius0,
            double cx1, double cy1, double radius1,
            double epsilon = Epsilon)
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

                result.State = IntersectionState.Outside;
            }
            else if (dist < Abs(radius0 - radius1))
            {
                // No solutions, one circle contains the other.
                // This would be a good point to return a Lotus struct of the smaller of the circles.

                result.State = IntersectionState.Inside;
            }
            else if ((Abs(dist) < epsilon) && (Abs(radius0 - radius1) < epsilon))
            {
                // No solutions, the circles coincide.
                // This would be a good point to return a Lotus struct of one of the circles.

                result.State = IntersectionState.Outside;
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
                if (Abs(dist - radius0 + radius1) < epsilon)
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
        /// Find intersection between two circles.
        /// </summary>
        /// <param name="c1X"></param>
        /// <param name="c1Y"></param>
        /// <param name="r1"></param>
        /// <param name="c2X"></param>
        /// <param name="c2Y"></param>
        /// <param name="r2"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CircleCircleIntersection(
            double c1X, double c1Y, double r1,
            double c2X, double c2Y, double r2,
            double epsilon = Epsilon)
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
        /// Find the intersection between an ellipse and a polygon contour.
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="angle"></param>
        /// <param name="points"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection EllipsePolygonContourIntersection(
            double cx, double cy, double rx, double ry, double angle,
            List<Point2D> points,
            double epsilon = Epsilon)
            => EllipsePolygonContourIntersection(cx, cy, rx, ry, Cos(angle), Sin(angle), points, epsilon);

        /// <summary>
        /// Find the intersection between an ellipse and a polygon contour.
        /// </summary>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="radius"></param>
        /// <param name="angle"></param>
        /// <param name="points"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CirclePolygonContourIntersection(
            double cX, double cY, double radius, double angle,
            List<Point2D> points,
            double epsilon = Epsilon)
        {
            var result = new Intersection(IntersectionState.NoIntersection);
            var inter = new Intersection(IntersectionState.NoIntersection);
            var length = points.Count;

            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            Point2D b1 = points[length - 1];
            for (var i = 0; i < points.Count; ++i)
            {
                Point2D b2 = points[i];

                inter = LineSegmentCircleIntersection(b1.X, b1.Y, b2.X, b2.Y, cX, cY, radius, angle, epsilon);
                result.AppendPoints(inter.Points);

                b1 = b2;
            }

            // ToDo: Return IntersectionState.Inside if all of the points of th polygon are contained inside the circle, or the circle and there are no intersections.

            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            else
                result.State = inter.State;
            return result;
        }

        /// <summary>
        /// Find the intersection between an ellipse and a polygon contour.
        /// </summary>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="cosA"></param>
        /// <param name="sinA"></param>
        /// <param name="points"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection EllipsePolygonContourIntersection(
            double cX, double cY, double rx, double ry, double cosA, double sinA,
            List<Point2D> points,
            double epsilon = Epsilon)
        {
            var result = new Intersection(IntersectionState.NoIntersection);
            var inter = new Intersection(IntersectionState.NoIntersection);
            var length = points.Count;

            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            Point2D b1 = points[length - 1];
            for (var i = 0; i < points.Count; ++i)
            {
                Point2D b2 = points[i];

                inter = LineSegmentEllipseIntersection(b1.X, b1.Y, b2.X, b2.Y, cX, cY, rx, ry, cosA, sinA, epsilon);
                result.AppendPoints(inter.Points);

                b1 = b2;
            }

            // ToDo: Return IntersectionState.Inside if all of the points of the polygon are contained inside the ellipse, and there are no intersections.

            if (result.Count > 0)
                result.State |= IntersectionState.Intersection;
            else
                result.State = inter.State;
            return result;
        }

        /// <summary>
        /// Find the intersection between an ellipse and a rectangle.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection EllipseRectangleIntersection(
            double cx, double cy, double rx, double ry, double angle,
            double r1X, double r1Y, double r2X, double r2Y,
            double epsilon = Epsilon)
            => EllipseRectangleIntersection(cx, cy, rx, ry, Cos(angle), Sin(angle), r1X, r1Y, r2X, r2Y, epsilon);

        /// <summary>
        /// Find the intersection between an ellipse and a rectangle.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection EllipseRectangleIntersection(
            double cX, double cY, double rx, double ry, double cosA, double sinA,
            double r1X, double r1Y, double r2X, double r2Y,
            double epsilon = Epsilon)
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
        /// Find the intersection between a quadratic bezier and an unrotated ellipse.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentUnrotatedEllipseIntersection(
            double p1X, double p1Y, double p2X, double p2Y, double p3X, double p3Y,
            double ecX, double ecY, double rx, double ry,
            double epsilon = Epsilon)
        {
            var a = new Vector2D(p2X, p2Y) * -2;
            var c2 = new Vector2D(p1X, p1Y) + a + new Vector2D(p3X, p3Y);
            a = new Vector2D(p1X, p1Y) * -2;
            var b = new Vector2D(p2X, p2Y) * 2;
            var c1 = a + b;
            var c0 = new Vector2D(p1X, p1Y);

            var rxrx = rx * rx;
            var ryry = ry * ry;

            var roots = QuarticRoots(
                ryry * c2.I * c2.I + rxrx * c2.J * c2.J,
                2 * (ryry * c2.I * c1.I + rxrx * c2.J * c1.J),
                ryry * (2 * c2.I * c0.I + c1.I * c1.I) + rxrx * (2 * c2.J * c0.J + c1.J * c1.J) - 2 * (ryry * ecX * c2.I + rxrx * ecY * c2.J),
                2 * (ryry * c1.I * (c0.I - ecX) + rxrx * c1.J * (c0.J - ecY)),
                ryry * (c0.I * c0.I + ecY * ecY) + rxrx * (c0.J * c0.J + ecY * ecY) - 2 * (ryry * ecX * c0.I + rxrx * ecY * c0.J) - rxrx * ryry,
                epsilon);

            var result = new Intersection(IntersectionState.NoIntersection);

            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];
                if (0 <= t && t <= 1)
                    result.Points.Add((Point2D)c2 * t * t + (c1 * t + c0));
            }

            if (result.Points.Count > 0)
                result.State = IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// Find the intersection between a cubic bezier and an unrotated ellipse.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierSegmentUnrotatedEllipseIntersection(
            double p1X, double p1Y, double p2X, double p2Y, double p3X, double p3Y, double p4X, double p4Y,
            double ecX, double ecY, double rx, double ry,
            double epsilon = Epsilon)
        {
            var a = new Vector2D(p1X, p1Y) * -1;
            var b = new Vector2D(p2X, p2Y) * 3;
            var c = new Vector2D(p3X, p3Y) * -3;
            var d = a + b + c + new Vector2D(p4X, p4Y);
            var c3 = new Vector2D(d.I, d.J);
            a = new Vector2D(p1X, p1Y) * 3;
            b = new Vector2D(p2X, p2Y) * -6;
            c = new Vector2D(p3X, p3Y) * 3;
            d = a + b + c;
            var c2 = new Vector2D(d.I, d.J);
            a = new Vector2D(p1X, p1Y) * -3;
            b = new Vector2D(p2X, p2Y) * 3;
            c = a + b;
            var c1 = new Vector2D(c.I, c.J);
            var c0 = new Vector2D(p1X, p1Y);

            var rxrx = rx * rx;
            var ryry = ry * ry;

            var roots = new Polynomial(
                c0.I * c0.I * ryry - 2 * c0.J * ecY * rxrx - 2 * c0.I * ecX * ryry + c0.J * c0.J * rxrx + ecX * ecX * ryry + ecY * ecY * rxrx - rxrx * ryry,
                2 * c1.I * ryry * (c0.I - ecX) + 2 * c1.J * rxrx * (c0.J - ecY),
                2 * c2.I * ryry * (c0.I - ecX) + 2 * c2.J * rxrx * (c0.J - ecY) + c1.I * c1.I * ryry + c1.J * c1.J * rxrx,
                2 * c3.I * ryry * (c0.I - ecX) + 2 * c3.J * rxrx * (c0.J - ecY) + 2 * (c2.I * c1.I * ryry + c2.J * c1.J * rxrx),
                2 * (c3.I * c1.I * ryry + c3.J * c1.J * rxrx) + c2.I * c2.I * ryry + c2.J * c2.J * rxrx,
                2 * (c3.I * c2.I * ryry + c3.J * c2.J * rxrx),
                c3.I * c3.I * ryry + c3.J * c3.J * rxrx).Simplify().RootsInInterval();

            var result = new Intersection(IntersectionState.NoIntersection);

            for (var i = 0; i < roots.Count; i++)
            {
                var t = roots[i];
                result.Points.Add((Point2D)c3 * t * t * t + (c2 * t * t + (c1 * t + c0)));
            }

            if (result.Points.Count > 0)
                result.State = IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// Find the intersection between two unrotated ellipses.
        /// </summary>
        /// <param name="c1X"></param>
        /// <param name="c1Y"></param>
        /// <param name="rx1"></param>
        /// <param name="ry1"></param>
        /// <param name="c2X"></param>
        /// <param name="c2Y"></param>
        /// <param name="rx2"></param>
        /// <param name="ry2"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection UnrotatedEllipseUnrotatedEllipseIntersection(
            double c1X, double c1Y, double rx1, double ry1,
            double c2X, double c2Y, double rx2, double ry2,
            double epsilon = Epsilon)
        {
            double[] a = new double[] { ry1 * ry1, 0, rx1 * rx1, -2 * ry1 * ry1 * c1X, -2 * rx1 * rx1 * c1Y, ry1 * ry1 * c1X * c1X + rx1 * rx1 * c1Y * c1Y - rx1 * rx1 * ry1 * ry1 };
            double[] b = new double[] { ry2 * ry2, 0, rx2 * rx2, -2 * ry2 * ry2 * c2X, -2 * rx2 * rx2 * c2Y, ry2 * ry2 * c2X * c2X + rx2 * rx2 * c2Y * c2Y - rx2 * rx2 * ry2 * ry2 };

            var yPoly = Bezout(a, b);
            var yRoots = yPoly.Simplify().Roots();

            var norm0 = (a[0] * a[0] + 2 * a[1] * a[1] + a[2] * a[2]) * epsilon;
            var norm1 = (b[0] * b[0] + 2 * b[1] * b[1] + b[2] * b[2]) * epsilon;

            var result = new Intersection(IntersectionState.NoIntersection);

            for (var y = 0; y < yRoots.Count; y++)
            {
                var xRoots = QuadraticRoots(
                    a[0],
                    a[3] + yRoots[y] * a[1],
                    a[5] + yRoots[y] * (a[4] + yRoots[y] * a[2]),
                    epsilon);
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

        #endregion

        #region Scan-beam Intersection Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamPoint(ref List<double> scanlist, double x, double y, double px, double py, double epsilon = Epsilon)
        {
            if ((y - py) / (x - px) == 1)
                scanlist.Add(x);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamLine(ref List<double> scanlist, double x, double y, double x0, double y0, double i, double j, double epsilon = Epsilon)
        {
            // Translate lines to origin.
            var u1 = 1 - x;
            var v1 = 0 - y;

            var ua = i * (y - y0) - j * (x - x0);
            var ub = u1 * (y - y0) - v1 * (x - x0);

            // Calculate the determinant of the coefficient matrix.
            var determinant = (j * u1) - (i * v1);

            // Check if the lines are parallel.
            if (Abs(determinant) < epsilon)
            {
                if (ua == 0 || ub == 0)
                {
                    // Line segment is coincident to the Line. There are an infinite number of intersections.
                    scanlist.Add(double.NegativeInfinity);
                    scanlist.Add(double.PositiveInfinity);
                }
            }
            else
            {
                // Find the index where the intersection point lies on the line.
                var ta = ua / determinant;

                // One intersection.
                scanlist.Add(x0 + ta * u1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamLineSegment(ref List<double> scanlist, double x, double y, double x0, double y0, double x1, double y1, double epsilon = Epsilon)
        {
            // Translate lines to origin.
            var u2 = x1 - x0;
            var v2 = y1 - y0;

            var ua = u2 * (y - y0) - v2 * (x - x0);
            var ub = (y - y0);

            // Calculate the determinant of the coefficient matrix.
            var determinant = v2;

            // Check if the lines are parallel.
            if (Abs(determinant) < epsilon)
            {
                if (ua == 0 || ub == 0)
                {
                    // Line segment is coincident to the scan-beam. There are an infinite number of intersections, but we only care about the start and end points of the line segment.
                    scanlist.Add(x0);
                    scanlist.Add(x1);
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
                    scanlist.Add(x + ta * 1);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="p0x"></param>
        /// <param name="p0y"></param>
        /// <param name="p1x"></param>
        /// <param name="p1y"></param>
        /// <param name="p2x"></param>
        /// <param name="p2y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamQuadraticBezierSegment(ref List<double> scanlist, double x, double y, double p0x, double p0y, double p1x, double p1y, double p2x, double p2y, double epsilon = Epsilon)
        {
            var C = x * (y - (y + 0)) + y * (x + 1 - x);

            var xCoeff = QuadraticBezierCoefficients(p0x, p1x, p2x);
            var yCoeff = QuadraticBezierCoefficients(p0y, p1y, p2y);

            List<double> roots = QuadraticRoots(
                -yCoeff.C,    // t^2
                -yCoeff.B,    // t^1
                -yCoeff.A + C // 1
                );

            foreach (var t in roots)
            {
                // Add intersection point.
                if (!(t < 0 || t > 1d))
                    scanlist.Add(xCoeff.C * t * t + xCoeff.B * t + xCoeff.A);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="p0x"></param>
        /// <param name="p0y"></param>
        /// <param name="p1x"></param>
        /// <param name="p1y"></param>
        /// <param name="p2x"></param>
        /// <param name="p2y"></param>
        /// <param name="p3x"></param>
        /// <param name="p3y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamCubicBezierSegment(ref List<double> scanlist, double x, double y, double p0x, double p0y, double p1x, double p1y, double p2x, double p2y, double p3x, double p3y, double epsilon = Epsilon)
        {
            // Translate the line to the origin.
            var C = x * (y - (y + 0)) + y * (x + 1 - x);

            var xCoeff = CubicBezierCoefficients(p0x, p1x, p2x, p3x);
            var yCoeff = CubicBezierCoefficients(p0y, p1y, p2y, p3y);

            // Fix for missing intersections for curves that can be reduced to lower degrees.
            var determinant = (x + 1 - x) * (p3y - p2y + p1y - p0y) - (y + 0 - y) * (p3x - p2x + p1x - p0x);
            List<double> roots = (Abs(determinant) < epsilon) ?
                QuadraticRoots(
                    -yCoeff.C,    // t^2
                    -yCoeff.B,    // t^1
                    -yCoeff.A + C // 1
                    ) :
                CubicRoots(
                    -yCoeff.D,    // t^3
                    -yCoeff.C,    // t^2
                    -yCoeff.B,    // t^1
                    -yCoeff.A + C // 1
                    );

            foreach (var t in roots)
            {
                // Add intersection point.
                if (!(t < 0 || t > 1d))
                    scanlist.Add(xCoeff.D * t * t * t + xCoeff.C * t * t + xCoeff.B * t + xCoeff.A);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamCircle(ref List<double> scanlist, double x, double y, double cX, double cY, double r, double epsilon = Epsilon)
        {
            // If the circle or line segment are empty, return no intersections.
            if ((r == 0d) || ((x == x + 1) && (y == x + 0)))
                return;

            // Calculate the quadratic parameters.
            var b = 2 * (x - cX);
            var c = (x - cX) * (x - cX) + (y - cY) * (y - cY) - r * r;

            // Calculate the discriminant.
            var discriminant = b * b - 4 * c;

            if ((discriminant < 0))
            {
                // No real solutions.
                return;
            }
            else if (discriminant == 0)
            {
                // One possible solution.
                var t = -b / 2d;

                // Add two points for top or bottom of the circle.
                scanlist.Add(x + t * 1);
                scanlist.Add(x + t * 1);
            }
            else if (discriminant > 0)
            {
                // Two possible solutions.
                var t1 = ((-b + Sqrt(discriminant)) / (2 * 1));
                var t2 = ((-b - Sqrt(discriminant)) / (2 * 1));

                // Add the points.
                scanlist.Add(x + t1 * 1);
                scanlist.Add(x + t2 * 1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="angle"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamCircularArc(ref List<double> scanlist, double x, double y, double cX, double cY, double r, double angle, double startAngle, double sweepAngle, double epsilon = Epsilon)
        {
            // If the circle or line segment are empty, return no intersections.
            if (r == 0d)
                return;

            // Calculate the quadratic parameters.
            var b = 2 * (x - cX);
            var c = (x - cX) * (x - cX) + (y - cY) * (y - cY) - r * r;

            // Find the points of the chord.
            var startPoint = new Point2D(cX + (Cos(angle + startAngle) * r), cY + (Sin(angle + startAngle) * r));
            var endPoint = new Point2D(cX + (Cos(angle + startAngle + sweepAngle) * r), cY + (Sin(angle + startAngle + sweepAngle) * r));

            // Calculate the discriminant.
            var discriminant = b * b - 4 * c;

            // Check for intersections.
            if ((1 <= epsilon) || (discriminant < 0))
            {
                // No real solutions.
                return;
            }
            else if (discriminant == 0)
            {
                // One possible solution.
                var t = -b / 2d;

                // Find the point.
                var pX = x + t;
                var pY = y + t;

                // Find the determinant of the chord and point.
                var determinant = (startPoint.X - pX) * (endPoint.Y - pY) - (endPoint.X - pX) * (startPoint.Y - pY);

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle))
                {
                    // Add the point.
                    scanlist.Add(pX);
                    scanlist.Add(pX);
                }
            }
            else if (discriminant > 0)
            {
                // Two possible solutions.
                var t1 = ((-b + Sqrt(discriminant)) / 2d);
                var t2 = ((-b - Sqrt(discriminant)) / 2d);

                // Find the point.
                var pX = x + t1 * 1;
                var pY = y + t1;

                // Find the determinant of the chord and point.
                var determinant = (startPoint.X - pX) * (endPoint.Y - pY) - (endPoint.X - pX) * (startPoint.Y - pY);

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle))
                {
                    // Add the point.
                    scanlist.Add(pX);
                }

                // Find the point.
                pX = x + t2 * 1;
                pY = y + t2;

                // Find the determinant of the chord and point.
                determinant = (startPoint.X - pX) * (endPoint.Y - pY) - (endPoint.X - pX) * (startPoint.Y - pY);

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle))
                {
                    // Add the point.
                    scanlist.Add(pX);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="cosA"></param>
        /// <param name="sinA"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamEllipse(ref List<double> scanlist, double x, double y, double cx, double cy, double rx, double ry, double cosA, double sinA, double epsilon = Epsilon)
        {
            // If the ellipse is empty, return no intersections.
            if ((rx == 0d) || (ry == 0d))
                return;

            // Translate the line to put the ellipse centered at the origin.
            var u1 = x - cx;
            var v1 = y - cy;
            var u2 = x - cx + 1;
            var v2 = y - cy + 0;

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
                return;
            }
            else if (discriminant == 0)
            {
                // One real possible solution.
                var t = OneHalf * -b / a;

                // Add two points for either the top or bottom.
                scanlist.Add((u1 + (u2 - u1) * t + cx));
                scanlist.Add((u1 + (u2 - u1) * t + cx));
            }
            else if (discriminant > 0)
            {
                // Two real possible solutions.
                var t1 = (OneHalf * (-b + Sqrt(discriminant)) / a);
                var t2 = (OneHalf * (-b - Sqrt(discriminant)) / a);

                // Add the points.
                scanlist.Add(u1 + (u2 - u1) * t1 + cx);
                scanlist.Add(u1 + (u2 - u1) * t2 + cx);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="cosA"></param>
        /// <param name="sinA"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamEllipticalArc(ref List<double> scanlist, double x, double y, double cx, double cy, double rx, double ry, double cosA, double sinA, double startAngle, double sweepAngle, double epsilon = Epsilon)
        {
            // If the ellipse or line segment are empty, return no intersections.
            if ((sweepAngle == 0d) || (rx == 0d) || (ry == 0d))
                return;

            // Translate the line to put it at the ellipse centered at the origin.
            var u0 = x - cx;
            var v0 = y - cy;
            var u1 = x - cx + 1;
            var v1 = y - cy;

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
                return;
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
                var px = u0 + (u1 - u0) * t + cx;
                var py = v0 + (v1 - v0) * t + cy;

                // Find the determinant of the matrix representing the chord.
                var determinant = (sx - px) * (ey - py) - (ex - px) * (sy - py);

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle))
                {
                    scanlist.Add(px);
                    scanlist.Add(px);
                }
            }
            else if (discriminant > 0)
            {
                // Two real possible solutions.
                var root = Sqrt(discriminant);
                var t1 = (OneHalf * (-b + root) / a);
                var t2 = (OneHalf * (-b - root) / a);

                // Find the point.
                var px = u0 + (u1 - u0) * t1 + cx;
                var py = v0 + (v1 - v0) * t1 + cy;

                // Find the determinant of the matrix representing the chord.
                var determinant = (sx - px) * (ey - py) - (ex - px) * (sy - py);

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle))
                    scanlist.Add(px);

                // Find the point.
                px = u0 + (u1 - u0) * t2 + cx;
                py = v0 + (v1 - v0) * t2 + cy;

                // Find the determinant of the matrix representing the chord.
                determinant = (sx - px) * (ey - py) - (ex - px) * (sy - py);

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle))
                    scanlist.Add(px);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamRectangle(ref List<double> scanlist, double x, double y, double r1X, double r1Y, double r2X, double r2Y, double epsilon = Epsilon)
        {
            var min = MinPoint(r1X, r1Y, r2X, r2Y);
            var max = MaxPoint(r1X, r1Y, r2X, r2Y);
            ScanbeamLineSegment(ref scanlist, x, y, max.X, min.Y, max.X, max.Y, epsilon);
            ScanbeamLineSegment(ref scanlist, x, y, min.X, max.Y, min.X, min.Y, epsilon);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="points"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamPolygonContour(ref List<double> scanlist, double x, double y, List<Point2D> points, double epsilon = Epsilon)
        {
            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            Point2D b1 = points[points.Count - 1];
            for (var i = 0; i < points.Count; ++i)
            {
                Point2D b2 = points[i];

                ScanbeamLineSegment(ref scanlist, x, y, b1.X, b1.Y, b2.X, b2.Y, epsilon);

                b1 = b2;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="curve"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamPolycurveContour(ref List<double> scanlist, double x, double y, PolycurveContour curve, double epsilon = Epsilon)
        {
            foreach (var segment in curve.Items)
            {
                switch (segment)
                {
                    case PointSegment t:
                        ScanbeamPoint(ref scanlist, x, y, t.Start.Value.X, t.Start.Value.Y, epsilon);
                        break;
                    case LineCurveSegment t:
                        ScanbeamLineSegment(ref scanlist, x, y, t.Start.Value.X, t.Start.Value.Y, t.End.Value.X, t.End.Value.Y, epsilon);
                        break;
                    case QuadraticBezierSegment t:
                        ScanbeamQuadraticBezierSegment(ref scanlist, x, y, t.Start.Value.X, t.Start.Value.Y, t.Handle.Value.X, t.Handle.Value.Y, t.End.Value.X, t.End.Value.Y, epsilon);
                        break;
                    case CubicBezierSegment t:
                        ScanbeamCubicBezierSegment(ref scanlist, x, y, t.Start.Value.X, t.Start.Value.Y, t.Handle1.X, t.Handle1.Y, t.Handle2.Value.X, t.Handle2.Value.Y, t.End.Value.X, t.End.Value.Y, epsilon);
                        break;
                    case ArcSegment t:
                        ScanbeamEllipticalArc(ref scanlist, x, y, t.Center.X, t.Center.Y, t.RX, t.RY, t.CosAngle, t.SinAngle, t.StartAngle, t.SweepAngle, epsilon);
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region Scan-beam to left Increment Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftPoint(double x, double y, double px, double py, double epsilon = Epsilon)
            => (((y - py) / (x - px) == 1) && (px <= x)) ? 1 : 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftLine(double x, double y, double x0, double y0, double i, double j, double epsilon = Epsilon)
        {
            // Translate lines to origin.
            var u1 = 1 - x;
            var v1 = 0 - y;

            var ua = i * (y - y0) - j * (x - x0);
            var ub = u1 * (y - y0) - v1 * (x - x0);

            // Calculate the determinant of the coefficient matrix.
            var determinant = (j * u1) - (i * v1);

            // Check if the lines are parallel.
            if (Abs(determinant) < epsilon)
            {
                if (ua == 0 || ub == 0)
                {
                    // Line segment is coincident to the Line. There are an infinite number of intersections.
                    return 1;
                }
            }
            else
            {
                // Find the index where the intersection point lies on the line.
                var ta = ua / determinant;

                // One intersection.
                if (x0 + ta * u1 <= x)
                    return 1;
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftLineSegment(double x, double y, double x0, double y0, double x1, double y1, double epsilon = Epsilon)
        {
            // Translate lines to origin.
            var u2 = x1 - x0;
            var v2 = y1 - y0;

            var ua = u2 * (y - y0) - v2 * (x - x0);
            var ub = (y - y0);

            // Calculate the determinant of the coefficient matrix.
            var determinant = v2;

            var result = 0;

            // Check if the lines are parallel.
            if (Abs(determinant) < epsilon)
            {
                if (ua == 0 || ub == 0)
                {
                    // Line segment is coincident to the scan-beam. There are an infinite number of intersections, but we only care about the start and end points of the line segment.
                    if (x0 <= x)
                        result++;
                    if (x1 <= x)
                        result++;
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
                    if (x0 + ta <= x)
                        result++;
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="p0x"></param>
        /// <param name="p0y"></param>
        /// <param name="p1x"></param>
        /// <param name="p1y"></param>
        /// <param name="p2x"></param>
        /// <param name="p2y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftQuadraticBezierSegment(double x, double y, double p0x, double p0y, double p1x, double p1y, double p2x, double p2y, double epsilon = Epsilon)
        {
            var C = x * (y - (y + 0)) + y * (x + 1 - x);

            var xCoeff = QuadraticBezierCoefficients(p0x, p1x, p2x);
            var yCoeff = QuadraticBezierCoefficients(p0y, p1y, p2y);

            List<double> roots = QuadraticRoots(
                -yCoeff.C,    // t^2
                -yCoeff.B,    // t^1
                -yCoeff.A + C // 1
                );

            var result = 0;
            foreach (var t in roots)
            {
                // Add intersection point.
                if (!(t < 0 || t > 1d) && (xCoeff.C * t * t + xCoeff.B * t + xCoeff.A <= x))
                    result++;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="p0x"></param>
        /// <param name="p0y"></param>
        /// <param name="p1x"></param>
        /// <param name="p1y"></param>
        /// <param name="p2x"></param>
        /// <param name="p2y"></param>
        /// <param name="p3x"></param>
        /// <param name="p3y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftCubicBezierSegment(double x, double y, double p0x, double p0y, double p1x, double p1y, double p2x, double p2y, double p3x, double p3y, double epsilon = Epsilon)
        {
            // Translate the line to the origin.
            var C = x * (y - (y + 0)) + y * (x + 1 - x);

            var xCoeff = CubicBezierCoefficients(p0x, p1x, p2x, p3x);
            var yCoeff = CubicBezierCoefficients(p0y, p1y, p2y, p3y);

            // Fix for missing intersections for curves that can be reduced to lower degrees.
            var determinant = (x + 1 - x) * (p3y - p2y + p1y - p0y) - (y + 0 - y) * (p3x - p2x + p1x - p0x);
            List<double> roots = (Abs(determinant) < epsilon) ?
                QuadraticRoots(
                    -yCoeff.C,    // t^2
                    -yCoeff.B,    // t^1
                    -yCoeff.A + C // 1
                    ) :
                CubicRoots(
                    -yCoeff.D,    // t^3
                    -yCoeff.C,    // t^2
                    -yCoeff.B,    // t^1
                    -yCoeff.A + C // 1
                    );

            var results = 0;
            foreach (var t in roots)
            {
                // Add intersection point.
                if (!(t < 0 || t > 1d) && (xCoeff.D * t * t * t + xCoeff.C * t * t + xCoeff.B * t + xCoeff.A) <= x)
                    results++;
            }

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="angle"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftCircle(double x, double y, double cX, double cY, double r, double angle, double startAngle, double sweepAngle, double epsilon = Epsilon)
        {
            // If the circle or line segment are empty, return no intersections.
            if ((r == 0d) || ((x == x + 1) && (y == x + 0)))
                return 0;

            // Calculate the quadratic parameters.
            var b = 2 * (x - cX);
            var c = (x - cX) * (x - cX) + (y - cY) * (y - cY) - r * r;

            // Calculate the discriminant.
            var discriminant = b * b - 4 * c;

            var result = 0;
            if ((discriminant < 0))
            {
                // No real solutions.
                return 0;
            }
            else if (discriminant == 0)
            {
                // One possible solution.
                var t = -b / 2d;

                // Add two points for top or bottom of the circle.
                if (x + t * 1 <= x)
                {
                    result++;
                    result++;
                }
            }
            else if (discriminant > 0)
            {
                // Two possible solutions.
                var t1 = ((-b + Sqrt(discriminant)) / (2 * 1));
                var t2 = ((-b - Sqrt(discriminant)) / (2 * 1));

                // Add the points.
                if (x + t1 * 1 <= x)
                    result++;
                if (x + t2 * 1 <= x)
                    result++;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="angle"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftCircularArc(double x, double y, double cX, double cY, double r, double angle, double startAngle, double sweepAngle, double epsilon = Epsilon)
        {
            // If the circle or line segment are empty, return no intersections.
            if (r == 0d)
                return 0;

            // Calculate the quadratic parameters.
            var b = 2 * (x - cX);
            var c = (x - cX) * (x - cX) + (y - cY) * (y - cY) - r * r;

            // Find the points of the chord.
            var startPoint = new Point2D(cX + (Cos(angle + startAngle) * r), cY + (Sin(angle + startAngle) * r));
            var endPoint = new Point2D(cX + (Cos(angle + startAngle + sweepAngle) * r), cY + (Sin(angle + startAngle + sweepAngle) * r));

            // Calculate the discriminant.
            var discriminant = b * b - 4 * c;

            var results = 0;

            // Check for intersections.
            if ((1 <= epsilon) || (discriminant < 0))
            {
                // No real solutions.
                return 0;
            }
            else if (discriminant == 0)
            {
                // One possible solution.
                var t = -b / 2d;

                // Find the point.
                var pX = x + t;
                var pY = y + t;

                // Find the determinant of the chord and point.
                var determinant = (startPoint.X - pX) * (endPoint.Y - pY) - (endPoint.X - pX) * (startPoint.Y - pY);

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle) && pX <= x)
                {
                    // Add the point.
                    results++;
                    results++;
                }
            }
            else if (discriminant > 0)
            {
                // Two possible solutions.
                var t1 = ((-b + Sqrt(discriminant)) / 2d);
                var t2 = ((-b - Sqrt(discriminant)) / 2d);

                // Find the point.
                var pX = x + t1;
                var pY = y + t1;

                // Find the determinant of the chord and point.
                var determinant = (startPoint.X - pX) * (endPoint.Y - pY) - (endPoint.X - pX) * (startPoint.Y - pY);

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle) && pX <= x)
                {
                    // Add the point.
                    results++;
                }

                // Find the point.
                pX = x + t2;
                pY = y + t2;

                // Find the determinant of the chord and point.
                determinant = (startPoint.X - pX) * (endPoint.Y - pY) - (endPoint.X - pX) * (startPoint.Y - pY);

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle) && pX <= x)
                {
                    // Add the point.
                    results++;
                }
            }

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="cosA"></param>
        /// <param name="sinA"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftEllipse(double x, double y, double cx, double cy, double rx, double ry, double cosA, double sinA, double epsilon = Epsilon)
        {
            // If the ellipse is empty, return no intersections.
            if ((rx == 0d) || (ry == 0d))
                return 0;

            // Translate the line to put the ellipse centered at the origin.
            var u1 = x - cx;
            var v1 = y - cy;
            var u2 = x - cx + 1;
            var v2 = y - cy + 0;

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

            var results = 0;

            // Find solutions.
            if ((a <= epsilon) || (discriminant < 0))
            {
                // No real solutions.
                return 0;
            }
            else if (discriminant == 0)
            {
                // One real possible solution.
                var t = OneHalf * -b / a;

                // Add two points for either the top or bottom.
                if ((u1 + (u2 - u1) * t + cx) <= x)
                    results++;
                if ((u1 + (u2 - u1) * t + cx) <= x)
                    results++;
            }
            else if (discriminant > 0)
            {
                // Two real possible solutions.
                var t1 = (OneHalf * (-b + Sqrt(discriminant)) / a);
                var t2 = (OneHalf * (-b - Sqrt(discriminant)) / a);

                // Add the points.
                if (u1 + (u2 - u1) * t1 + cx <= x)
                    results++;
                if (u1 + (u2 - u1) * t2 + cx <= x)
                    results++;
            }

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="cosA"></param>
        /// <param name="sinA"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftEllipticalArc(double x, double y, double cx, double cy, double rx, double ry, double cosA, double sinA, double startAngle, double sweepAngle, double epsilon = Epsilon)
        {
            // If the ellipse or line segment are empty, return no intersections.
            if ((sweepAngle == 0d) || (rx == 0d) || (ry == 0d))
                return 0;

            // Translate the line to put it at the ellipse centered at the origin.
            var u0 = x - cx;
            var v0 = y - cy;
            var u1 = x - cx + 1;
            var v1 = y - cy;

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
                return 0;
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

            var results = 0;
            if (discriminant == 0)
            {
                // One real possible solution.
                var t = OneHalf * -b / a;

                // Find the point.
                var px = u0 + (u1 - u0) * t + cx;
                var py = v0 + (v1 - v0) * t + cy;

                // Find the determinant of the matrix representing the chord.
                var determinant = (sx - px) * (ey - py) - (ex - px) * (sy - py);

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle) && px <= x)
                {
                    results++;
                    results++;
                }
            }
            else if (discriminant > 0)
            {
                // Two real possible solutions.
                var root = Sqrt(discriminant);
                var t1 = (OneHalf * (-b + root) / a);
                var t2 = (OneHalf * (-b - root) / a);

                // Find the point.
                var px = u0 + (u1 - u0) * t1 + cx;
                var py = v0 + (v1 - v0) * t1 + cy;

                // Find the determinant of the matrix representing the chord.
                var determinant = (sx - px) * (ey - py) - (ex - px) * (sy - py);

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle) && px <= x)
                    results++;

                // Find the point.
                px = u0 + (u1 - u0) * t2 + cx;
                py = v0 + (v1 - v0) * t2 + cy;

                // Find the determinant of the matrix representing the chord.
                determinant = (sx - px) * (ey - py) - (ex - px) * (sy - py);

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle) && px <= x)
                    results++;
            }

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftRectangle(double x, double y, double r1X, double r1Y, double r2X, double r2Y, double epsilon = Epsilon)
        {
            var results = 0;
            var min = MinPoint(r1X, r1Y, r2X, r2Y);
            var max = MaxPoint(r1X, r1Y, r2X, r2Y);
            results += ScanbeamPointsToLeftLineSegment(x, y, max.X, min.Y, max.X, max.Y, epsilon);
            results += ScanbeamPointsToLeftLineSegment(x, y, min.X, max.Y, min.X, min.Y, epsilon);
            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="points"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftPolygonContour(double x, double y, List<Point2D> points, double epsilon = Epsilon)
        {
            var result = 0;

            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            Point2D b1 = points[points.Count - 1];
            for (var i = 0; i < points.Count; ++i)
            {
                Point2D b2 = points[i];

                result += ScanbeamPointsToLeftLineSegment(x, y, b1.X, b1.Y, b2.X, b2.Y, epsilon);

                b1 = b2;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="curve"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftPolycurveContour(double x, double y, PolycurveContour curve, double epsilon = Epsilon)
        {
            var results = 0;
            foreach (var segment in curve.Items)
            {
                switch (segment)
                {
                    case PointSegment t:
                        results += ScanbeamPointsToLeftPoint(x, y, t.Start.Value.X, t.Start.Value.Y, epsilon);
                        break;
                    case LineCurveSegment t:
                        results += ScanbeamPointsToLeftLineSegment(x, y, t.Start.Value.X, t.Start.Value.Y, t.End.Value.X, t.End.Value.Y, epsilon);
                        break;
                    case QuadraticBezierSegment t:
                        results += ScanbeamPointsToLeftQuadraticBezierSegment(x, y, t.Start.Value.X, t.Start.Value.Y, t.Handle.Value.X, t.Handle.Value.Y, t.End.Value.X, t.End.Value.Y, epsilon);
                        break;
                    case CubicBezierSegment t:
                        results += ScanbeamPointsToLeftCubicBezierSegment(x, y, t.Start.Value.X, t.Start.Value.Y, t.Handle1.X, t.Handle1.Y, t.Handle2.Value.X, t.Handle2.Value.Y, t.End.Value.X, t.End.Value.Y, epsilon);
                        break;
                    case ArcSegment t:
                        results += ScanbeamPointsToLeftEllipticalArc(x, y, t.Center.X, t.Center.Y, t.RX, t.RY, t.CosAngle, t.SinAngle, t.StartAngle, t.SweepAngle, epsilon);
                        break;
                    default:
                        break;
                }
            }

            return results;
        }

        #endregion

        #region Scan-beam to right Increment Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightPoint(double x, double y, double px, double py, double epsilon = Epsilon)
            => (((y - py) / (x - px) == 1) && (px >= x)) ? 1 : 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightLine(double x, double y, double x0, double y0, double i, double j, double epsilon = Epsilon)
        {
            // Translate lines to origin.
            var u1 = 1 - x;
            var v1 = 0 - y;

            var ua = i * (y - y0) - j * (x - x0);
            var ub = u1 * (y - y0) - v1 * (x - x0);

            // Calculate the determinant of the coefficient matrix.
            var determinant = (j * u1) - (i * v1);

            // Check if the lines are parallel.
            if (Abs(determinant) < epsilon)
            {
                if (ua == 0 || ub == 0)
                {
                    // Line segment is coincident to the Line. There are an infinite number of intersections.
                    return 1;
                }
            }
            else
            {
                // Find the index where the intersection point lies on the line.
                var ta = ua / determinant;

                // One intersection.
                if (x0 + ta * u1 >= x)
                    return 1;
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightLineSegment(double x, double y, double x0, double y0, double x1, double y1, double epsilon = Epsilon)
        {
            // Translate lines to origin.
            var u2 = x1 - x0;
            var v2 = y1 - y0;

            var ua = u2 * (y - y0) - v2 * (x - x0);
            var ub = (y - y0);

            // Calculate the determinant of the coefficient matrix.
            var determinant = v2;

            var result = 0;

            // Check if the lines are parallel.
            if (Abs(determinant) < epsilon)
            {
                //if (ua == 0 || ub == 0)
                //{
                //    // Line segment is coincident to the scan-beam. There are an infinite number of intersections, but we only care about the start and end points of the line segment.
                //    if (x0 >= x)
                //        result++;
                //    if (x1 >= x)
                //        result++;
                //}
            }
            else
            {
                // Find the index where the intersection point lies on the line.
                var ta = ua / determinant;
                var tb = ub / determinant;

                if (tb >= 0 && tb <= 1)
                {
                    // One intersection.
                    if (x0 + ta >= x)
                        result++;
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="p0x"></param>
        /// <param name="p0y"></param>
        /// <param name="p1x"></param>
        /// <param name="p1y"></param>
        /// <param name="p2x"></param>
        /// <param name="p2y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightQuadraticBezierSegment(double x, double y, double p0x, double p0y, double p1x, double p1y, double p2x, double p2y, double epsilon = Epsilon)
        {
            var C = x * (y - (y + 0)) + y * (x + 1 - x);

            var xCoeff = QuadraticBezierCoefficients(p0x, p1x, p2x);
            var yCoeff = QuadraticBezierCoefficients(p0y, p1y, p2y);

            List<double> roots = QuadraticRoots(
                -yCoeff.C,    // t^2
                -yCoeff.B,    // t^1
                -yCoeff.A + C // 1
                );

            var result = 0;
            foreach (var t in roots)
            {
                // Add intersection point.
                if (!(t < 0 || t > 1d) && ((xCoeff.C * t * t + xCoeff.B * t + xCoeff.A) >= x))
                    result++;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="p0x"></param>
        /// <param name="p0y"></param>
        /// <param name="p1x"></param>
        /// <param name="p1y"></param>
        /// <param name="p2x"></param>
        /// <param name="p2y"></param>
        /// <param name="p3x"></param>
        /// <param name="p3y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightCubicBezierSegment(double x, double y, double p0x, double p0y, double p1x, double p1y, double p2x, double p2y, double p3x, double p3y, double epsilon = Epsilon)
        {
            // Translate the line to the origin.
            var C = x * (y - (y + 0)) + y * (x + 1 - x);

            var xCoeff = CubicBezierCoefficients(p0x, p1x, p2x, p3x);
            var yCoeff = CubicBezierCoefficients(p0y, p1y, p2y, p3y);

            // Fix for missing intersections for curves that can be reduced to lower degrees.
            var determinant = (x + 1 - x) * (p3y - p2y + p1y - p0y) - (y + 0 - y) * (p3x - p2x + p1x - p0x);
            List<double> roots = (Abs(determinant) < epsilon) ?
                QuadraticRoots(
                    -yCoeff.C,    // t^2
                    -yCoeff.B,    // t^1
                    -yCoeff.A + C // 1
                    ) :
                CubicRoots(
                    -yCoeff.D,    // t^3
                    -yCoeff.C,    // t^2
                    -yCoeff.B,    // t^1
                    -yCoeff.A + C // 1
                    );

            var results = 0;
            foreach (var t in roots)
            {
                // Add intersection point.
                if (!(t < 0 || t > 1d) && (xCoeff.D * t * t * t + xCoeff.C * t * t + xCoeff.B * t + xCoeff.A) >= x)
                    results++;
            }

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="angle"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightCircle(double x, double y, double cX, double cY, double r, double angle, double startAngle, double sweepAngle, double epsilon = Epsilon)
        {
            // If the circle or line segment are empty, return no intersections.
            if ((r == 0d) || ((x == x + 1) && (y == x + 0)))
                return 0;

            // Calculate the quadratic parameters.
            var b = 2 * (x - cX);
            var c = (x - cX) * (x - cX) + (y - cY) * (y - cY) - r * r;

            // Calculate the discriminant.
            var discriminant = b * b - 4 * c;

            var result = 0;
            if ((discriminant < 0))
            {
                // No real solutions.
                return 0;
            }
            else if (discriminant == 0)
            {
                // One possible solution.
                var t = -b / 2d;

                // Add two points for top or bottom of the circle.
                if (x + t * 1 >= x)
                {
                    result++;
                    result++;
                }
            }
            else if (discriminant > 0)
            {
                // Two possible solutions.
                var t1 = ((-b + Sqrt(discriminant)) / (2 * 1));
                var t2 = ((-b - Sqrt(discriminant)) / (2 * 1));

                // Add the points.
                if (x + t1 * 1 >= x)
                    result++;
                if (x + t2 * 1 >= x)
                    result++;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="angle"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightCircularArc(double x, double y, double cX, double cY, double r, double angle, double startAngle, double sweepAngle, double epsilon = Epsilon)
        {
            // If the circle or line segment are empty, return no intersections.
            if (r == 0d)
                return 0;

            // Calculate the quadratic parameters.
            var b = 2 * (x - cX);
            var c = (x - cX) * (x - cX) + (y - cY) * (y - cY) - r * r;

            // Find the points of the chord.
            var startPoint = new Point2D(cX + (Cos(angle + startAngle) * r), cY + (Sin(angle + startAngle) * r));
            var endPoint = new Point2D(cX + (Cos(angle + startAngle + sweepAngle) * r), cY + (Sin(angle + startAngle + sweepAngle) * r));

            // Calculate the discriminant.
            var discriminant = b * b - 4 * c;

            var results = 0;

            // Check for intersections.
            if ((1 <= epsilon) || (discriminant < 0))
            {
                // No real solutions.
                return 0;
            }
            else if (discriminant == 0)
            {
                // One possible solution.
                var t = -b / 2d;

                // Find the point.
                var pX = x + t;
                var pY = y + t;

                // Find the determinant of the chord and point.
                var determinant = (startPoint.X - pX) * (endPoint.Y - pY) - (endPoint.X - pX) * (startPoint.Y - pY);

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle) && pX >= x)
                {
                    // Add the point.
                    results++;
                    results++;
                }
            }
            else if (discriminant > 0)
            {
                // Two possible solutions.
                var t1 = ((-b + Sqrt(discriminant)) / 2d);
                var t2 = ((-b - Sqrt(discriminant)) / 2d);

                // Find the point.
                var pX = x + t1;
                var pY = y + t1;

                // Find the determinant of the chord and point.
                var determinant = (startPoint.X - pX) * (endPoint.Y - pY) - (endPoint.X - pX) * (startPoint.Y - pY);

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle) && pX >= x)
                {
                    // Add the point.
                    results++;
                }

                // Find the point.
                pX = x + t2;
                pY = y + t2;

                // Find the determinant of the chord and point.
                determinant = (startPoint.X - pX) * (endPoint.Y - pY) - (endPoint.X - pX) * (startPoint.Y - pY);

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle) && pX >= x)
                {
                    // Add the point.
                    results++;
                }
            }

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="cosA"></param>
        /// <param name="sinA"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightEllipse(double x, double y, double cx, double cy, double rx, double ry, double cosA, double sinA, double epsilon = Epsilon)
        {
            // If the ellipse is empty, return no intersections.
            if ((rx == 0d) || (ry == 0d))
                return 0;

            // Translate the line to put the ellipse centered at the origin.
            var u1 = x - cx;
            var v1 = y - cy;
            var u2 = x - cx + 1;
            var v2 = y - cy + 0;

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

            var results = 0;

            // Find solutions.
            if ((a <= epsilon) || (discriminant < 0))
            {
                // No real solutions.
                return 0;
            }
            else if (discriminant == 0)
            {
                // One real possible solution.
                var t = OneHalf * -b / a;

                // Add two points for either the top or bottom.
                if ((u1 + (u2 - u1) * t + cx) >= x)
                    results++;
                if ((u1 + (u2 - u1) * t + cx) >= x)
                    results++;
            }
            else if (discriminant > 0)
            {
                // Two real possible solutions.
                var t1 = (OneHalf * (-b + Sqrt(discriminant)) / a);
                if (u1 + (u2 - u1) * t1 + cx >= x)
                    results++;

                var t2 = (OneHalf * (-b - Sqrt(discriminant)) / a);
                if (u1 + (u2 - u1) * t2 + cx >= x)
                    results++;
            }

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="cosA"></param>
        /// <param name="sinA"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightEllipticalArc(double x, double y, double cx, double cy, double rx, double ry, double cosA, double sinA, double startAngle, double sweepAngle, double epsilon = Epsilon)
        {
            // If the ellipse or line segment are empty, return no intersections.
            if ((sweepAngle == 0d) || (rx == 0d) || (ry == 0d))
                return 0;

            // Translate the line to put it at the ellipse centered at the origin.
            var u0 = x - cx;
            var v0 = y - cy;
            var u1 = x - cx + 1;
            var v1 = y - cy;

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
                return 0;
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

            var results = 0;
            if (discriminant == 0)
            {
                // One real possible solution.
                var t = OneHalf * -b / a;

                // Find the point.
                var px = u0 + (u1 - u0) * t + cx;
                var py = v0 + (v1 - v0) * t + cy;

                // Find the determinant of the matrix representing the chord.
                var determinant = (sx - px) * (ey - py) - (ex - px) * (sy - py);

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle) && px >= x)
                {
                    //if (sx != px && sy != py || ex != px && ey != py)
                    results++;
                    results++;
                }
            }
            else if (discriminant > 0)
            {
                // Two real possible solutions.
                var root = Sqrt(discriminant);
                var t1 = (OneHalf * (-b + root) / a);
                var t2 = (OneHalf * (-b - root) / a);

                // Find the point.
                var px = u0 + (u1 - u0) * t1 + cx;
                var py = v0 + (v1 - v0) * t1 + cy;

                // Find the determinant of the matrix representing the chord.
                var determinant = (sx - px) * (ey - py) - (ex - px) * (sy - py);

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle) && px >= x)
                    results++;

                // Find the point.
                px = u0 + (u1 - u0) * t2 + cx;
                py = v0 + (v1 - v0) * t2 + cy;

                // Find the determinant of the matrix representing the chord.
                determinant = (sx - px) * (ey - py) - (ex - px) * (sy - py);

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle) && px >= x)
                    results++;
            }

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightRectangle(double x, double y, double r1X, double r1Y, double r2X, double r2Y, double epsilon = Epsilon)
        {
            var results = 0;
            var min = MinPoint(r1X, r1Y, r2X, r2Y);
            var max = MaxPoint(r1X, r1Y, r2X, r2Y);
            results += ScanbeamPointsToRightLineSegment(x, y, max.X, min.Y, max.X, max.Y, epsilon);
            results += ScanbeamPointsToRightLineSegment(x, y, min.X, max.Y, min.X, min.Y, epsilon);
            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="points"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightPolygonContour(double x, double y, List<Point2D> points, double epsilon = Epsilon)
        {
            var result = 0;

            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            Point2D b1 = points[points.Count - 1];
            for (var i = 0; i < points.Count; ++i)
            {
                Point2D b2 = points[i];

                result += ScanbeamPointsToRightLineSegment(x, y, b1.X, b1.Y, b2.X, b2.Y, epsilon);

                b1 = b2;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="curve"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightPolycurveContour(double x, double y, PolycurveContour curve, double epsilon = Epsilon)
        {
            var results = 0;
            foreach (var segment in curve.Items)
            {
                switch (segment)
                {
                    case PointSegment t:
                        results += ScanbeamPointsToRightPoint(x, y, t.Start.Value.X, t.Start.Value.Y, epsilon);
                        break;
                    case LineCurveSegment t:
                        results += ScanbeamPointsToRightLineSegment(x, y, t.Start.Value.X, t.Start.Value.Y, t.End.Value.X, t.End.Value.Y, epsilon);
                        break;
                    case QuadraticBezierSegment t:
                        results += ScanbeamPointsToRightQuadraticBezierSegment(x, y, t.Start.Value.X, t.Start.Value.Y, t.Handle.Value.X, t.Handle.Value.Y, t.End.Value.X, t.End.Value.Y, epsilon);
                        break;
                    case CubicBezierSegment t:
                        results += ScanbeamPointsToRightCubicBezierSegment(x, y, t.Start.Value.X, t.Start.Value.Y, t.Handle1.X, t.Handle1.Y, t.Handle2.Value.X, t.Handle2.Value.Y, t.End.Value.X, t.End.Value.Y, epsilon);
                        break;
                    case ArcSegment t:
                        results += ScanbeamPointsToRightEllipticalArc(x, y, t.Center.X, t.Center.Y, t.RX, t.RY, t.CosAngle, t.SinAngle, t.StartAngle, t.SweepAngle, epsilon);
                        break;
                    default:
                        break;
                }
            }

            return results;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e1">First Ellipse parameters.</param>
        /// <param name="e2">Second Ellipse parameters.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// This code is based on MgcIntr2DElpElp.cpp written by David Eberly.
        /// His code along with many other excellent examples formerly available
        /// at his site but the latest version now at: https://www.geometrictools.com/
        /// </acknowledgment>
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
                AB * BC - AC * AC,
                AB * BEmCD + AD * BC - 2 * AC * AE,
                AB * BFpDE + AD * BEmCD - AE * AE - 2 * AC * AF,
                AB * DF + AD * BFpDE - 2 * AE * AF,
                AD * DF - AF * AF);
        }

        #endregion
    }
}

