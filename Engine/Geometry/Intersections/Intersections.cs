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
using System.Linq;

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
        /// <param name="v">The value to check whether it is between the other two.</param>
        /// <param name="m">The first value to compare to.</param>
        /// <param name="M">The second value to compare to.</param>
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
            {
                return true;
            }

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
        /// <param name="a">First Point to test.</param>
        /// <param name="b">Second Point to test.</param>
        /// <returns>Returns an <see cref="Inclusion"/> object with the points of intersection, and relationship status.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this Point2D a, Point2D b)
            => a == b ? Inclusion.Boundary : Inclusion.Outside;

        /// <summary>
        /// Determines whether the specified point is contained within the region defined by this <see cref="LineSegment"/>.
        /// </summary>
        /// <param name="seg">Line segment to test.</param>
        /// <param name="point">Point to test.</param>
        /// <returns>Returns an <see cref="Inclusion"/> object with the points of intersection, and relationship status.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this LineSegment seg, Point2D point)
            => PointLineSegmentIntersects(point.X, point.Y, seg.AX, seg.AY, seg.BX, seg.BY) ? Inclusion.Boundary : Inclusion.Outside;

        /// <summary>
        /// Determines whether the specified point is contained within the rectangular region defined by this <see cref="Rectangle2D"/>.
        /// </summary>
        /// <param name="rectangle"><see cref="Rectangle2D"/> class.</param>
        /// <param name="point">Point to test.</param>
        /// <returns>Returns an <see cref="Inclusion"/> object with the points of intersection, and relationship status.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this Rectangle2D rectangle, Point2D point)
            => RectangleContainsPoint(rectangle.X, rectangle.Y, rectangle.Right, rectangle.Bottom, point.X, point.Y);

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="PolygonContour"/>.
        /// </summary>
        /// <param name="polygon"><see cref="PolygonContour"/> class.</param>
        /// <param name="point">Point to test.</param>
        /// <returns>Returns an <see cref="Inclusion"/> object with the points of intersection, and relationship status.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this PolygonContour polygon, Point2D point)
            => PolygonContourContainsPoint(polygon.Points, point.X, point.Y);

        /// <summary>
        /// Determines whether the specified point is contained within the region defined by this <see cref="PolyBezierContour"/>.
        /// </summary>
        /// <param name="figure"><see cref="PolyBezierContour"/> class.</param>
        /// <param name="point">Point to test.</param>
        /// <returns>Returns an <see cref="Inclusion"/> object with the points of intersection, and relationship status.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this PolycurveContour figure, Point2D point)
            => PolycurveContourContainsPoint(figure, point);

        /// <summary>
        /// Determines whether the specified point is contained withing the set of regions defined by this <see cref="Polygon"/>.
        /// </summary>
        /// <param name="polygons">List of <see cref="PolygonContour"/> classes.</param>
        /// <param name="point">Point to test.</param>
        /// <returns>Returns an <see cref="Inclusion"/> object with the points of intersection, and relationship status.</returns>
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
        /// <returns>Returns an <see cref="Inclusion"/> object with the points of intersection, and relationship status.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this Circle circle, Point2D point)
            => CircleContainsPoint(circle.X, circle.Y, circle.Radius, point.X, point.Y);

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="Ellipse"/>.
        /// </summary>
        /// <param name="ellipse"><see cref="Ellipse"/> class.</param>
        /// <param name="point">Point to test.</param>
        /// <returns>Returns an <see cref="Inclusion"/> object with the points of intersection, and relationship status.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this Ellipse ellipse, Point2D point)
            => EllipseContainsPoint(ellipse.Center.X, ellipse.Center.Y, ellipse.RX, ellipse.RY, ellipse.SinAngle, ellipse.CosAngle, point.X, point.Y);

        /// <summary>
        /// Determines whether the specified point is contained within the region defined by this <see cref="Circle"/>.
        /// </summary>
        /// <param name="arc"><see cref="Circle"/> class.</param>
        /// <param name="point">Point to test.</param>
        /// <returns>Returns an <see cref="Inclusion"/> object with the points of intersection, and relationship status.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this CircularArc arc, Point2D point)
            => CircularArcSectorContainsPoint(arc.X, arc.Y, arc.Radius, arc.StartAngle, arc.SweepAngle, point.X, point.Y);

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="EllipticalArc"/>.
        /// </summary>
        /// <param name="ellipseArc"><see cref="Ellipse"/> class.</param>
        /// <param name="point">Point to test.</param>
        /// <returns>Returns an <see cref="Inclusion"/> object with the points of intersection, and relationship status.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this EllipticalArc ellipseArc, Point2D point)
            => EllipticalArcContainsPoint(ellipseArc.Center.X, ellipseArc.Center.Y, ellipseArc.RX, ellipseArc.RY, ellipseArc.Angle, ellipseArc.StartAngle, ellipseArc.SweepAngle, point.X, point.Y);

        /// <summary>
        /// Determines whether the specified <see cref="Rectangle2D"/> is contained withing the region defined by this <see cref="Rectangle2D"/>.
        /// </summary>
        /// <param name="a"><see cref="Rectangle2D"/> class.</param>
        /// <param name="b"><see cref="Rectangle2D"/> to test.</param>
        /// <returns>Returns an <see cref="Inclusion"/> object with the points of intersection, and relationship status.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(this Rectangle2D a, Rectangle2D b)
            => RectangleContainsRectangle(a.X, a.Y, a.Width, a.Height, b.X, b.Y, b.Width, b.Height);

        #endregion

        #region Intersects Extension Method Overloads

        /// <summary>
        /// Check whether a Rectangle and a shape intersects.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="shape"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
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
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
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
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this Point2D p, Point2D a, Point2D b)
            => PointLineSegmentIntersects(p.X, p.Y, a.X, a.Y, b.X, b.Y);

        /// <summary>
        /// Check whether a point is coincident to a line segment.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="p"></param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this Point2D p, LineSegment s)
            => PointLineSegmentIntersects(p.X, p.Y, s.A.X, s.A.Y, s.B.X, s.B.Y);

        /// <summary>
        /// Check whether a point is coincident to a line segment.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="p"></param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this LineSegment s, Point2D p)
            => PointLineSegmentIntersects(p.X, p.Y, s.A.X, s.A.Y, s.B.X, s.B.Y);

        /// <summary>
        /// Check whether two line segments intersect.
        /// </summary>
        /// <param name="s0"></param>
        /// <param name="s1"></param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this LineSegment s0, LineSegment s1)
            => LineSegmentLineSegmentIntersects(s0.A.X, s0.A.Y, s0.B.X, s0.B.Y, s1.A.X, s1.A.Y, s1.B.X, s1.B.Y);

        /// <summary>
        /// Determines if this rectangle interests with another rectangle.
        /// </summary>
        /// <param name="rect1"></param>
        /// <param name="rect2"></param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Intersects(this Rectangle2D rect1, Rectangle2D rect2)
            => RectangleRectangleIntersects(rect1.X, rect1.Y, rect1.Width, rect1.Height, rect2.X, rect2.Y, rect2.Width, rect2.Height);

        /// <summary>
        /// Determines if this Circle interests with another Circle.
        /// </summary>
        /// <param name="c0"></param>
        /// <param name="c1"></param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Point2D p, LineSegment s, double epsilon = Epsilon)
            => PointLineSegmentIntersection(p.X, p.Y, s.AX, s.AY, s.BX, s.BY, epsilon);

        /// <summary>
        /// Find the intersection of a Point and a Line segment.
        /// </summary>
        /// <param name="p">The point.</param>
        /// <param name="r"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Point2D p, Ray r, double epsilon = Epsilon)
            => PointRayIntersection(p.X, p.Y, r.Location.X, r.Location.Y, r.Direction.I, r.Direction.J, epsilon);

        /// <summary>
        /// Find the intersection of two Bezier segments.
        /// </summary>
        /// <param name="b0">The first Bezier segment.</param>
        /// <param name="b1">The second Bezier segment.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
                            return LineSegmentQuadraticBezierSegmentIntersection(b1[0].X, b1[0].Y, b1[1].X, b1[1].Y, b0.CurveX, b0.CurveY, epsilon);
                        case PolynomialDegree.Cubic:
                            return LineSegmentCubicBezierSegmentIntersection(b1[0].X, b1[0].Y, b1[1].X, b1[1].Y, b0.CurveX, b0.CurveY, epsilon);
                        default:
                            return new Intersection(IntersectionState.NoIntersection);
                    }
                case PolynomialDegree.Quadratic:
                    switch (b1.Degree)
                    {
                        case PolynomialDegree.Linear:
                            return LineSegmentQuadraticBezierSegmentIntersection(b1[0].X, b1[0].Y, b1[1].X, b1[1].Y, b0.CurveX, b0.CurveY, epsilon);
                        case PolynomialDegree.Quadratic:
                            return QuadraticBezierSegmentQuadraticBezierSegmentIntersection(b0.CurveX, b0.CurveY, b1.CurveX, b1.CurveY, epsilon);
                        case PolynomialDegree.Cubic:
                            return QuadraticBezierSegmentCubicBezierSegmentIntersection(b0.CurveX, b0.CurveY, b1.CurveX, b1.CurveY, epsilon);
                        default:
                            return new Intersection(IntersectionState.NoIntersection);
                    }
                case PolynomialDegree.Cubic:
                    switch (b1.Degree)
                    {
                        case PolynomialDegree.Linear:
                            return LineSegmentCubicBezierSegmentIntersection(b1[0].X, b1[0].Y, b1[1].X, b1[1].Y, b0.CurveX, b0.CurveY, epsilon);
                        case PolynomialDegree.Quadratic:
                            return QuadraticBezierSegmentCubicBezierSegmentIntersection(b1.CurveX, b1.CurveY, b0.CurveX, b0.CurveY, epsilon);
                        case PolynomialDegree.Cubic:
                            return CubicBezierSegmentCubicBezierSegmentIntersection(b0.CurveX, b0.CurveY, b1.CurveX, b1.CurveY, epsilon);
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this BezierSegmentX b, LineSegment l, double epsilon = Epsilon)
        {
            switch (b.Degree)
            {
                case PolynomialDegree.Linear:
                    return LineSegmentLineSegmentIntersection(l.AX, l.AY, l.BX, l.BY, b[0].X, b[0].Y, b[1].X, b[1].Y, epsilon);
                case PolynomialDegree.Quadratic:
                    return LineSegmentQuadraticBezierSegmentIntersection(l.AX, l.AY, l.BX, l.BY, b.CurveX, b.CurveY, epsilon);
                case PolynomialDegree.Cubic:
                    return LineSegmentCubicBezierSegmentIntersection(l.AX, l.AY, l.BX, l.BY, b.CurveX, b.CurveY, epsilon);
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this BezierSegmentX b0, QuadraticBezier b1, double epsilon = Epsilon)
        {
            switch (b0.Degree)
            {
                case PolynomialDegree.Linear:
                    return LineSegmentQuadraticBezierSegmentIntersection(b0[0].X, b0[0].Y, b0[1].X, b0[1].Y, b1.CurveX, b1.CurveY, epsilon);
                case PolynomialDegree.Quadratic:
                    return QuadraticBezierSegmentQuadraticBezierSegmentIntersection(b1.CurveX, b1.CurveY, b0.CurveX, b0.CurveY, epsilon);
                case PolynomialDegree.Cubic:
                    return QuadraticBezierSegmentCubicBezierSegmentIntersection(b1.CurveX, b1.CurveY, b0.CurveX, b0.CurveY, epsilon);
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this BezierSegmentX b, CubicBezier c, double epsilon = Epsilon)
        {
            switch (b.Degree)
            {
                case PolynomialDegree.Linear:
                    return LineSegmentCubicBezierSegmentIntersection(b[0].X, b[0].Y, b[1].X, b[1].Y, c.CurveX, c.CurveY, epsilon);
                case PolynomialDegree.Quadratic:
                    return QuadraticBezierSegmentCubicBezierSegmentIntersection(b.CurveX, b.CurveY, c.CurveX, c.CurveY, epsilon);
                case PolynomialDegree.Cubic:
                    return CubicBezierSegmentCubicBezierSegmentIntersection(c.CurveX, c.CurveY, b.CurveX, b.CurveY, epsilon);
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, Line l, double epsilon = Epsilon)
            => LineLineSegmentIntersection(l.Location.X, l.Location.Y, l.Direction.I, l.Direction.J, s.AX, s.AY, s.BX, s.BY, epsilon);

        /// <summary>
        /// Find the intersection of a Line segment and a Ray.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="r">The ray.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, Ray r, double epsilon = Epsilon)
            => RayLineSegmentIntersection(r.Location.X, r.Location.Y, r.Direction.I, r.Direction.J, s.AX, s.AY, s.BX, s.BY, epsilon);

        /// <summary>
        /// Find the intersection of a line segment and a Bezier segment.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="b">The bezier curve segment.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, QuadraticBezier b, double epsilon = Epsilon)
            => LineSegmentQuadraticBezierSegmentIntersection(s.AX, s.AY, s.BX, s.BY, b.CurveX, b.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a Line segment and a Cubic Bezier.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="b">The cubic bezier curve segment.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, CubicBezier b, double epsilon = Epsilon)
            => LineSegmentCubicBezierSegmentIntersection(s.AX, s.AY, s.BX, s.BY, b.CurveX, b.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a Line segment and a triangle.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="r">The rectangle.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, Triangle r, double epsilon = Epsilon)
            => LineSegmentTriangleIntersection(s.AX, s.AY, s.BX, s.BY, r.A.X, r.A.Y, r.B.X, r.B.Y, r.C.X, r.C.Y, epsilon);

        /// <summary>
        /// Find the intersection of a Line segment and a Rectangle.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="r">The rectangle.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, Rectangle2D r, double epsilon = Epsilon)
            => LineSegmentRectangleIntersection(s.AX, s.AY, s.BX, s.BY, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Line segment and a Polygon contour.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="p">The polygon contour.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, PolygonContour p, double epsilon = Epsilon)
            => LineSegmentPolygonContourIntersection(s.AX, s.AY, s.BX, s.BY, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a line segment.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="c">The circle.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, EllipticalArc e, double epsilon = Epsilon)
            => LineSegmentEllipticalArcIntersection(s.A.X, s.A.Y, s.B.X, s.B.Y, e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, e.StartAngle, e.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of a Ray and a Point.
        /// </summary>
        /// <param name="r">The ray.</param>
        /// <param name="p">The point.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray r, Point2D p, double epsilon = Epsilon)
            => PointRayIntersection(p.X, p.Y, r.Location.X, r.Location.Y, r.Direction.I, r.Direction.J, epsilon);

        /// <summary>
        /// Find the intersection of a <see cref="Ray"/> and a <see cref="LineSegment"/>.
        /// </summary>
        /// <param name="r">The ray.</param>
        /// <param name="l">The line segment.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray r, LineSegment l, double epsilon = Epsilon)
            => LineRayIntersection(l.AX, l.AY, l.BX, l.BY, r.Location.X, r.Location.Y, r.Direction.I, r.Direction.J, epsilon);

        /// <summary>
        /// Find the intersection of two Rays.
        /// </summary>
        /// <param name="r0">The first ray.</param>
        /// <param name="r1">The second ray.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray r0, Ray r1, double epsilon = Epsilon)
            => RayRayIntersection(r0.Location.X, r0.Location.Y, r0.Direction.I, r0.Direction.J, r1.Location.X, r1.Location.Y, r1.Direction.I, r1.Direction.J, epsilon);

        /// <summary>
        /// Find the intersection of a <see cref="Ray"/> and a <see cref="Line"/>.
        /// </summary>
        /// <param name="r">The ray.</param>
        /// <param name="l">The line.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray r, Line l, double epsilon = Epsilon)
            => LineRayIntersection(l.Location.X, l.Location.Y, l.Direction.I, l.Direction.J, r.Location.X, r.Location.Y, r.Direction.I, r.Direction.J, epsilon);

        /// <summary>
        /// Find the intersection of a ray and a Quadratic Bezier.
        /// </summary>
        /// <param name="s">The ray.</param>
        /// <param name="b">The quadratic bezier curve segment.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray s, QuadraticBezier b, double epsilon = Epsilon)
            => RayQuadraticBezierSegmentIntersection(s.Location.X, s.Location.Y, s.Direction.I, s.Direction.J, b.CurveX, b.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a Ray and a Cubic Bezier.
        /// </summary>
        /// <param name="s">The ray.</param>
        /// <param name="b">The cubic bezier curve segment.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray s, CubicBezier b, double epsilon = Epsilon)
            => RayCubicBezierSegmentIntersection(s.Location.X, s.Location.Y, s.Direction.I, s.Direction.J, b.CurveX, b.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a Ray and a triangle.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="r">The rectangle.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray s, Triangle r, double epsilon = Epsilon)
            => LineSegmentTriangleIntersection(s.Location.X, s.Location.Y, s.Direction.I, s.Direction.J, r.A.X, r.A.Y, r.B.X, r.B.Y, r.C.X, r.C.Y, epsilon);

        /// <summary>
        /// Find the intersection of a <see cref="Ray"/> and a <see cref="Rectangle2D"/>.
        /// </summary>
        /// <param name="l">The line.</param>
        /// <param name="r">The rectangle.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray l, Rectangle2D r, double epsilon = Epsilon)
            => RayRectangleIntersection(l.Location.X, l.Location.Y, l.Direction.I, l.Direction.J, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Ray and a Polygon contour.
        /// </summary>
        /// <param name="l">The line.</param>
        /// <param name="p">The polygon contour.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray l, PolygonContour p, double epsilon = Epsilon)
            => RayPolygonContourIntersection(l.Location.X, l.Location.Y, l.Direction.I, l.Direction.J, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a ray.
        /// </summary>
        /// <param name="s">The ray.</param>
        /// <param name="c">The circle.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray s, Circle c, double epsilon = Epsilon)
            => RayCircleIntersection(s.Location.X, s.Location.Y, s.Direction.I, s.Direction.J, c.X, c.Y, c.Radius, 0, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a ray.
        /// </summary>
        /// <param name="l">The line.</param>
        /// <param name="c">The circular arc.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray l, CircularArc c, double epsilon = Epsilon)
            => RayCircularArcIntersection(l.Location.X, l.Location.Y, l.Direction.I, l.Direction.J, c.X, c.Y, c.Radius, 0, c.StartAngle, c.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a ray.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="e">The ellipse.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray s, Ellipse e, double epsilon = Epsilon)
            => RayEllipseIntersection(s.Location.X, s.Location.Y, s.Direction.I, s.Direction.J, e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a ray segment.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="e">The elliptical arc.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray s, EllipticalArc e, double epsilon = Epsilon)
            => LineEllipticalArcIntersection(s.Location.X, s.Location.Y, s.Direction.I, s.Direction.J, e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, e.StartAngle, e.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of a line and a point.
        /// </summary>
        /// <param name="l">The Line.</param>
        /// <param name="p">The Point.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, Point2D p, double epsilon = Epsilon)
            => PointLineIntersection(p.X, p.Y, l.Location.X, l.Location.Y, l.Direction.I, l.Direction.J, epsilon);

        /// <summary>
        /// Find the intersection of a Line and a Line segment.
        /// </summary>
        /// <param name="l">The line.</param>
        /// <param name="s">The line segment.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, LineSegment s, double epsilon = Epsilon)
            => LineLineSegmentIntersection(l.Location.X, l.Location.Y, l.Direction.I, l.Direction.J, s.AX, s.AY, s.BX, s.BY, epsilon);

        /// <summary>
        /// Find the intersection of a line and a ray.
        /// </summary>
        /// <param name="l">The line.</param>
        /// <param name="r">The ray.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, Ray r, double epsilon = Epsilon)
            => LineRayIntersection(l.Location.X, l.Location.Y, l.Direction.I, l.Direction.J, r.Location.X, r.Location.Y, r.Direction.I, r.Direction.J, epsilon);

        /// <summary>
        /// Find the intersection of to Lines.
        /// </summary>
        /// <param name="a">The first line.</param>
        /// <param name="b">The second line.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line a, Line b, double epsilon = Epsilon)
            => LineLineIntersection(
                a.Location.X, a.Location.Y,
                a.Direction.I, a.Direction.J,
                b.Location.X, b.Location.Y,
                b.Direction.I, b.Direction.J, epsilon
                );

        /// <summary>
        /// Find the intersection of a Line segment and a Quadratic Bezier.
        /// </summary>
        /// <param name="l">The line.</param>
        /// <param name="b">The quadratic bezier curve segment.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, QuadraticBezier b, double epsilon = Epsilon)
            => LineQuadraticBezierIntersection(l.Location.X, l.Location.Y, l.Direction.I, l.Direction.J, b.CurveX, b.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a Line segment and a Cubic Bezier.
        /// </summary>
        /// <param name="l">The line.</param>
        /// <param name="b">The cubic bezier curve segment.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, CubicBezier b, double epsilon = Epsilon)
            => LineCubicBezierIntersection(l.Location.X, l.Location.Y, l.Direction.I, l.Direction.J, b.CurveX, b.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a Line and a triangle.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="r">The rectangle.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line s, Triangle r, double epsilon = Epsilon)
            => LineSegmentTriangleIntersection(s.Location.X, s.Location.Y, s.Direction.I, s.Direction.J, r.A.X, r.A.Y, r.B.X, r.B.Y, r.C.X, r.C.Y, epsilon);

        /// <summary>
        /// Find the intersection of a Line and a Rectangle.
        /// </summary>
        /// <param name="l">The line.</param>
        /// <param name="r">The rectangle.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, Rectangle2D r, double epsilon = Epsilon)
            => LineRectangleIntersection(l.Location.X, l.Location.Y, l.Direction.I, l.Direction.J, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Line and a Polygon contour.
        /// </summary>
        /// <param name="l">The line.</param>
        /// <param name="p">The polygon contour.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, PolygonContour p, double epsilon = Epsilon)
            => LinePolygonContourIntersection(l.Location.X, l.Location.Y, l.Direction.I, l.Direction.J, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a line.
        /// </summary>
        /// <param name="l">The line.</param>
        /// <param name="c">The circle.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, Circle c, double epsilon = Epsilon)
            => LineCircleIntersection(l.Location.X, l.Location.Y, l.Direction.I, l.Direction.J, c.X, c.Y, c.Radius, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a line.
        /// </summary>
        /// <param name="l">The line.</param>
        /// <param name="c">The circular arc.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, CircularArc c, double epsilon = Epsilon)
            => LineCircularArcIntersection(l.Location.X, l.Location.Y, l.Direction.I, l.Direction.J, c.X, c.Y, c.Radius, 0, c.StartAngle, c.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="e">The ellipse.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line s, Ellipse e, double epsilon = Epsilon)
            => LineEllipseIntersection(s.Location.X, s.Location.Y, s.Direction.I, s.Direction.J, e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="e">The elliptical arc.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line s, EllipticalArc e, double epsilon = Epsilon)
            => LineEllipticalArcIntersection(s.Location.X, s.Location.Y, s.Direction.I, s.Direction.J, e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, e.StartAngle, e.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bezier and a Bezier segment.
        /// </summary>
        /// <param name="q">The quadratic bezier curve segment.</param>
        /// <param name="b">The bezier segment.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b, LineSegment l, double epsilon = Epsilon)
            => LineSegmentQuadraticBezierSegmentIntersection(l.AX, l.AY, l.BX, l.BY, b.CurveX, b.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a ray and a Quadratic Bezier.
        /// </summary>
        /// <param name="s">The ray.</param>
        /// <param name="b">The quadratic bezier curve segment.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b, Ray s, double epsilon = Epsilon)
            => RayQuadraticBezierSegmentIntersection(s.Location.X, s.Location.Y, s.Direction.I, s.Direction.J, b.CurveX, b.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bezier and a Line segment.
        /// </summary>
        /// <param name="b">The quadratic bezier curve segment.</param>
        /// <param name="l">The line.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b, Line l, double epsilon = Epsilon)
            => LineQuadraticBezierIntersection(l.Location.X, l.Location.Y, l.Direction.I, l.Direction.J, b.CurveX, b.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of two Quadratic Beziers.
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="b1"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b0, QuadraticBezier b1, double epsilon = Epsilon)
            => QuadraticBezierSegmentQuadraticBezierSegmentIntersection(b0.CurveX, b0.CurveY, b1.CurveX, b1.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bezier and a Cubic Bezier.
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="b1"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b0, CubicBezier b1, double epsilon = Epsilon)
            => QuadraticBezierSegmentCubicBezierSegmentIntersection(b0.CurveX, b0.CurveY, b1.CurveX, b1.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bezier and a triangle.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="t"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b, Triangle t, double epsilon = Epsilon)
            => QuadraticBezierSegmentTriangleIntersection(b.CurveX, b.CurveY, t.A.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bezier and a Rectangle.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="r"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b, Rectangle2D r, double epsilon = Epsilon)
            => QuadraticBezierSegmentRectangleIntersection(b.CurveX, b.CurveY, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bezier and a Polygon Contour.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="p"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b, PolygonContour p, double epsilon = Epsilon)
            => QuadraticBezierSegmentPolygonContourIntersection(b.CurveX, b.CurveY, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bezier and a Circle.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier b, Circle c, double epsilon = Epsilon)
            => QuadraticBezierSegmentUnrotatedEllipseIntersection(b.CurveX, b.CurveY, c.X, c.Y, c.Radius, c.Radius, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bezier and an unrotated Ellipse.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="e"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        //[DebuggerStepThrough]
        public static Intersection Intersection(this QuadraticBezier b, Ellipse e, double epsilon = Epsilon)
            => QuadraticBezierSegmentUnrotatedEllipseIntersection(b.CurveX, b.CurveY, e.X, e.Y, e.RX, e.RY, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bezier and a Bezier segment.
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="b1"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b, LineSegment l, double epsilon = Epsilon)
            => LineSegmentCubicBezierSegmentIntersection(l.AX, l.AY, l.BX, l.BY, b.CurveX, b.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a Ray and a Cubic Bezier.
        /// </summary>
        /// <param name="s">The ray.</param>
        /// <param name="b">The cubic bezier curve segment.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b, Ray s, double epsilon = Epsilon)
            => RayCubicBezierSegmentIntersection(s.Location.X, s.Location.Y, s.Direction.I, s.Direction.J, b.CurveX, b.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bezier and a Line segment.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="l"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b, Line l, double epsilon = Epsilon)
            => LineCubicBezierIntersection(l.Location.X, l.Location.Y, l.Direction.I, l.Direction.J, b.CurveX, b.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bezier and a Quadratic Bezier.
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b0"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b1, QuadraticBezier b0, double epsilon = Epsilon)
            => QuadraticBezierSegmentCubicBezierSegmentIntersection(b0.CurveX, b0.CurveY, b1.CurveX, b1.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of two Cubic Beziers.
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="b1"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b0, CubicBezier b1, double epsilon = Epsilon)
            => CubicBezierSegmentCubicBezierSegmentIntersection(b0.CurveX, b0.CurveY, b1.CurveX, b1.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bezier and a Triangle.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="t"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b, Triangle t, double epsilon = Epsilon)
            => CubicBezierSegmentTriangleIntersection(b.CurveX, b.CurveY, t.A.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bezier and a Rectangle.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="r"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b, Rectangle2D r, double epsilon = Epsilon)
            => CubicBezierSegmentRectangleIntersection(b.CurveX, b.CurveY, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bezier and a Polygon Contour.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="p"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b, PolygonContour p, double epsilon = Epsilon)
            => CubicBezierSegmentPolygonIntersection(b.CurveX, b.CurveY, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bezier and a Circle.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b, Circle c, double epsilon = Epsilon)
            => CubicBezierSegmentUnrotatedEllipseIntersection(b.CurveX, b.CurveY, c.X, c.Y, c.Radius, c.Radius, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bezier and an unrotated Ellipse.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="e"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b, Ellipse e, double epsilon = Epsilon)
            => CubicBezierSegmentUnrotatedEllipseIntersection(b.CurveX, b.CurveY, e.X, e.Y, e.RX, e.RY, epsilon);

        /// <summary>
        /// Find the intersection of a triangle and a Line segment.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="t">The rectangle.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Triangle t, LineSegment s, double epsilon = Epsilon)
            => LineSegmentTriangleIntersection(s.AX, s.AY, s.BX, s.BY, t.A.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, epsilon);

        /// <summary>
        /// Find the intersection of a triangle and a Ray.
        /// </summary>
        /// <param name="r">The line segment.</param>
        /// <param name="t">The rectangle.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Triangle t, Ray r, double epsilon = Epsilon)
            => RayTriangleIntersection(r.Location.X, r.Location.Y, r.Direction.I, r.Direction.J, t.A.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, epsilon);

        /// <summary>
        /// Find the intersection of a triangle and a Line.
        /// </summary>
        /// <param name="l">The line segment.</param>
        /// <param name="t">The rectangle.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Triangle t, Line l, double epsilon = Epsilon)
            => LineSegmentTriangleIntersection(l.Location.X, l.Location.Y, l.Direction.I, l.Direction.J, t.A.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bezier and a triangle.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="t"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Triangle t, QuadraticBezier b, double epsilon = Epsilon)
            => QuadraticBezierSegmentTriangleIntersection(b.CurveX, b.CurveY, t.A.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bezier and a Triangle.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="t"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Triangle t, CubicBezier b, double epsilon = Epsilon)
            => CubicBezierSegmentTriangleIntersection(b.CurveX, b.CurveY, t.A.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, epsilon);

        /// <summary>
        /// Find the intersection of two triangles.
        /// </summary>
        /// <param name="t0"></param>
        /// <param name="t1"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Triangle t0, Triangle t1, double epsilon = Epsilon)
            => TriangleTriangleIntersection(t0.A.X, t0.A.Y, t0.B.X, t0.B.Y, t0.C.X, t0.C.Y, t1.A.X, t1.A.Y, t1.B.X, t1.B.Y, t1.C.X, t1.C.Y);

        /// <summary>
        /// Find the intersection of a triangle and a rectangle.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="r"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Triangle t, Rectangle2D r, double epsilon = Epsilon)
            => TriangleRectangleIntersection(t.A.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Polygon contour and a Rectangle.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="t"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Triangle t, PolygonContour p, double epsilon = Epsilon)
            => TrianglePolygonContourIntersection(t.A.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of a triangle and a circle.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="c"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Triangle t, Circle c, double epsilon = Epsilon)
            => TriangleCircleIntersection(t.A.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, c.X, c.Y, c.Radius, 0d);

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a triangle.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="e"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Triangle r, Ellipse e, double epsilon = Epsilon)
            => TriangleEllipseIntersection(e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, r.A.X, r.A.Y, r.B.X, r.B.Y, r.C.X, r.C.Y, epsilon);

        /// <summary>
        /// Find the intersection of a Rectangle and a Line segment.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="l"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r, LineSegment l, double epsilon = Epsilon)
            => LineSegmentRectangleIntersection(l.AX, l.AY, l.BX, l.BY, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Rectangle and a line.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <param name="r"></param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r, Line l, double epsilon = Epsilon)
            => LineRectangleIntersection(l.Location.X, l.Location.Y, l.Direction.I, l.Direction.J, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a <see cref="Ray"/> and a <see cref="Rectangle2D"/>.
        /// </summary>
        /// <param name="l">The line.</param>
        /// <param name="r">The rectangle.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r, Ray l, double epsilon = Epsilon)
            => RayRectangleIntersection(l.Location.X, l.Location.Y, l.Direction.I, l.Direction.J, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Rectangle and a Quadratic Bezier.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="b"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r, QuadraticBezier b, double epsilon = Epsilon)
            => QuadraticBezierSegmentRectangleIntersection(b.CurveX, b.CurveY, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Rectangle and a Cubic Bezier.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="b"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r, CubicBezier b, double epsilon = Epsilon)
            => CubicBezierSegmentRectangleIntersection(b.CurveX, b.CurveY, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Rectangle and a triangle.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="r"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r, Triangle t, double epsilon = Epsilon)
            => TriangleRectangleIntersection(t.A.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of two Rectangles.
        /// </summary>
        /// <param name="r0"></param>
        /// <param name="r1"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D l, PolygonContour p, double epsilon = Epsilon)
            => PolygonContourRectangleIntersection(p.Points, l.X, l.Y, l.Right, l.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Rectangle and a Circle.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D r, Ellipse e, double epsilon = Epsilon)
            => EllipseRectangleIntersection(e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, r.X, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Polygon contour and a Line segment.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="l"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p, LineSegment l, double epsilon = Epsilon)
            => LineSegmentPolygonContourIntersection(l.AX, l.AY, l.BX, l.BY, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of a Polygon contour and a Ray.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <param name="p"></param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p, Ray l, double epsilon = Epsilon)
            => RayPolygonContourIntersection(l.Location.X, l.Location.Y, l.Direction.I, l.Direction.J, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of a Polygon contour and a Line.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <param name="p"></param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p, Line l, double epsilon = Epsilon)
            => LinePolygonContourIntersection(l.Location.X, l.Location.Y, l.Direction.I, l.Direction.J, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of a Polygon contour and a Quadratic Bezier.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="b"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p, QuadraticBezier b, double epsilon = Epsilon)
             => QuadraticBezierSegmentPolygonContourIntersection(b.CurveX, b.CurveY, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of a Polygon Contour and a Cubic Bezier.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="b"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p, CubicBezier b, double epsilon = Epsilon)
            => CubicBezierSegmentPolygonIntersection(b.CurveX, b.CurveY, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of a Polygon contour and a Rectangle.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="t"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p, Triangle t, double epsilon = Epsilon)
            => TrianglePolygonContourIntersection(t.A.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of a Polygon contour and a Rectangle.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="l"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p, Rectangle2D l, double epsilon = Epsilon)
            => PolygonContourRectangleIntersection(p.Points, l.X, l.Y, l.Right, l.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of two Polygon contours.
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p0, PolygonContour p1, double epsilon = Epsilon)
             => PolygonContourPolygonContourIntersection(p0.Points, p1.Points, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a polygon.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="e"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p, Circle e, double epsilon = Epsilon)
            => CirclePolygonContourIntersection(e.X, e.Y, e.Radius, 0, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a polygon.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="e"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p, Ellipse e, double epsilon = Epsilon)
            => EllipsePolygonContourIntersection(e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a line segment.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="s"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, LineSegment s, double epsilon = Epsilon)
            => LineSegmentCircleIntersection(s.A.X, s.A.Y, s.B.X, s.B.Y, c.X, c.Y, c.Radius, 0, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a ray.
        /// </summary>
        /// <param name="s">The ray.</param>
        /// <param name="c">The circle.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, Ray s, double epsilon = Epsilon)
            => RayCircleIntersection(s.Location.X, s.Location.Y, s.Direction.I, s.Direction.J, c.X, c.Y, c.Radius, 0, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a line.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="l"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, Line l, double epsilon = Epsilon)
            => LineCircleIntersection(l.Location.X, l.Location.Y, l.Direction.I, l.Direction.J, c.X, c.Y, c.Radius, epsilon);

        /// <summary>
        /// Find the intersection of a Circle and a Quadratic Bezier.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="b"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, QuadraticBezier b, double epsilon = Epsilon)
            => QuadraticBezierSegmentUnrotatedEllipseIntersection(b.CurveX, b.CurveY, c.X, c.Y, c.Radius, c.Radius, epsilon);

        /// <summary>
        /// Find the intersection of a Circle and a Cubic Bezier.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="b"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, CubicBezier b, double epsilon = Epsilon)
            => CubicBezierSegmentUnrotatedEllipseIntersection(b.CurveX, b.CurveY, c.X, c.Y, c.Radius, c.Radius, epsilon);

        /// <summary>
        /// Find the intersection of a Circle and a triangle.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="c"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, Triangle t, double epsilon = Epsilon)
            => TriangleCircleIntersection(t.A.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, c.X, c.Y, c.Radius, 0d);

        /// <summary>
        /// Find the intersection of a Circle and a Rectangle.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="r"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, PolygonContour r, double epsilon = Epsilon)
            => CirclePolygonContourIntersection(c.X, c.Y, c.Radius, 0, r.Points, epsilon);

        /// <summary>
        /// Find the intersection between two circles.
        /// </summary>
        /// <param name="c0"></param>
        /// <param name="c1"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, Ellipse e, double epsilon = Epsilon)
            => UnrotatedEllipseUnrotatedEllipseIntersection(c.Center.X, c.Center.Y, c.Radius, c.Radius, e.Center.X, e.Center.Y, e.RX, e.RY, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <param name="s"></param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CircularArc c, LineSegment s, double epsilon = Epsilon)
            => LineSegmentCircularArcIntersection(s.A.X, s.A.Y, c.X, s.B.X, s.B.Y, c.Y, c.Radius, 0, c.StartAngle, c.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a ray.
        /// </summary>
        /// <param name="l">The line.</param>
        /// <param name="c">The circular arc.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CircularArc c, Ray l, double epsilon = Epsilon)
            => RayCircularArcIntersection(l.Location.X, l.Location.Y, l.Direction.I, l.Direction.J, c.X, c.Y, c.Radius, 0, c.StartAngle, c.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a line.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="s"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CircularArc c, Line s, double epsilon = Epsilon)
            => LineCircularArcIntersection(s.Location.X, s.Location.Y, s.Direction.I, s.Direction.J, c.X, c.Y, c.Radius, 0, c.StartAngle, c.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="s"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, LineSegment s, double epsilon = Epsilon)
            => LineSegmentEllipseIntersection(s.A.X, s.A.Y, s.B.X, s.B.Y, e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a ray.
        /// </summary>
        /// <param name="s">The line segment.</param>
        /// <param name="e">The ellipse.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, Ray s, double epsilon = Epsilon)
            => RayEllipseIntersection(s.Location.X, s.Location.Y, s.Direction.I, s.Direction.J, e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="s"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, Line s, double epsilon = Epsilon)
            => LineEllipseIntersection(s.Location.X, s.Location.Y, s.Direction.I, s.Direction.J, e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated Ellipse and a Quadratic Bezier.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="b"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, QuadraticBezier b, double epsilon = Epsilon)
             => QuadraticBezierSegmentUnrotatedEllipseIntersection(b.CurveX, b.CurveY, e.X, e.Y, e.RX, e.RY, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a Cubic Bezier.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="b"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, CubicBezier b, double epsilon = Epsilon)
            => CubicBezierSegmentUnrotatedEllipseIntersection(b.CurveX, b.CurveY, e.X, e.Y, e.RX, e.RY, epsilon);

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a triangle.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="e"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, Triangle r, double epsilon = Epsilon)
            => TriangleEllipseIntersection(e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, r.A.X, r.A.Y, r.B.X, r.B.Y, r.C.X, r.C.Y, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a rectangle.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="r"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, PolygonContour p, double epsilon = Epsilon)
            => EllipsePolygonContourIntersection(e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, p.Points, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated Ellipse and a Circle.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <param name="e"></param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e0, Ellipse e1, double epsilon = Epsilon)
            => UnrotatedEllipseUnrotatedEllipseIntersection(e0.Center.X, e0.Center.Y, e0.RX, e0.RY, e1.Center.X, e1.Center.Y, e1.RX, e1.RY, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="s"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this EllipticalArc e, LineSegment s, double epsilon = Epsilon)
            => LineSegmentEllipticalArcIntersection(s.A.X, s.A.Y, s.B.X, s.B.Y, e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, e.StartAngle, e.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a ray.
        /// </summary>
        /// <param name="s">The ray.</param>
        /// <param name="e">The elliptical arc.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this EllipticalArc e, Ray s, double epsilon = Epsilon)
            => LineEllipticalArcIntersection(s.Location.X, s.Location.Y, s.Direction.I, s.Direction.J, e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, e.StartAngle, e.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="s"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this EllipticalArc e, Line s, double epsilon = Epsilon)
            => LineEllipticalArcIntersection(s.Location.X, s.Location.Y, s.Direction.I, s.Direction.J, e.X, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, e.StartAngle, e.SweepAngle, epsilon);

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
        /// Determines whether the specified point is contained within the region defined by a triangle.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="o"></param>
        /// <param name="p"></param>
        /// <param name="epsilon"></param>
        /// <returns>Returns a Boolean value indicating whether the shape contains the point.</returns>
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
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion TriangleContainsPoint(double aX, double aY, double bX, double bY, double cX, double cY, double pX, double pY, double epsilon = Epsilon)
        {
            var a = new Point2D(aX, aY);
            var b = new Point2D(bX, bY);
            var c = new Point2D(cX, cY);
            var p = new Point2D(pX, pY);
            if (Intersects(p, a, b) || Intersects(p, b, c) || Intersects(p, c, a))
            {
                return Inclusion.Boundary;
            }

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
        /// <param name="epsilon"></param>
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
            {
                if (points.Count == 1)
                {
                    // If the polygon has 1 point, it is a point and has no interior, but a point can intersect a point.
                    return (pX == points[0].X && pY == points[0].Y) ? Inclusion.Boundary : Inclusion.Outside;
                }
                else if (points.Count == 2)
                {
                    // If the polygon has 2 points, it is a line and has no interior, but a point can intersect a line.
                    return ((pX == points[0].X) && (pY == points[0].Y))
                        || ((pX == points[1].X) && (pY == points[1].Y))
                        || (((pX > points[0].X) == (pX < points[1].X))
                        && ((pY > points[0].Y) == (pY < points[1].Y))
                        && ((pX - points[0].X) * (points[1].Y - points[0].Y) == (pY - points[0].Y) * (points[1].X - points[0].X))) ? Inclusion.Boundary : Inclusion.Outside;
                }
                else
                {
                    // Empty geometry.
                    return Inclusion.Outside;
                }
            }

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
                            {
                                return Inclusion.Boundary;
                            }
                            else if ((determinant > 0) == (curPoint.Y > nextPoint.Y))
                            {
                                result = 1 - result;
                            }
                        }
                    }
                    else if (curPoint.X > pX)
                    {
                        var determinant = (nextPoint.X - pX) * (curPoint.Y - pY) - (curPoint.X - pX) * (nextPoint.Y - pY);
                        if (Abs(determinant) < epsilon)
                        {
                            return Inclusion.Boundary;
                        }

                        if ((determinant > 0) == (curPoint.Y > nextPoint.Y))
                        {
                            result = 1 - result;
                        }
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
            {
                return Contains(path[0].Start.Value, point);
            }

            foreach (var item in path)
            {
                switch (item)
                {
                    case PointSegment p:
                        {
                            if (path[0].Start.Value == point)
                            {
                                return Inclusion.Boundary;
                            }

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
                                        {
                                            return Inclusion.Boundary;
                                        }
                                        else if ((determinant > 0) == (l.End.Value.Y > l.Start.Value.Y))
                                        {
                                            result = 1 - result;
                                        }
                                    }
                                }
                                else if (l.End.Value.X > point.X)
                                {
                                    var determinant = (l.Start.Value.X - point.X) * (l.End.Value.Y - point.Y) - (l.End.Value.X - point.X) * (l.Start.Value.Y - point.Y);
                                    if (Abs(determinant) < epsilon)
                                    {
                                        return Inclusion.Boundary;
                                    }

                                    if ((determinant > 0) == (l.End.Value.Y > l.Start.Value.Y))
                                    {
                                        result = 1 - result;
                                    }
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
                                {
                                    return Inclusion.Boundary;
                                }

                                if (normalizedRadius < 1d)
                                {
                                    result = 1 - result;
                                }
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
                                        {
                                            result = 1 - result;
                                        }
                                    }
                                }
                                else if (t.End.Value.X > point.X)
                                {
                                    var determinant2 = (t.Start.Value.X - point.X) * (t.End.Value.Y - point.Y) - (t.End.Value.X - point.X) * (t.Start.Value.Y - point.Y);
                                    if ((determinant2 > 0) == (t.End.Value.Y > t.Start.Value.Y))
                                    {
                                        result = 1 - result;
                                    }
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
                {
                    return Inclusion.Boundary;
                }
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
            => EllipseContainsPoint(cX, cY, r1, r2, Sin(angle), Cos(angle), pX, pY);

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="Ellipse"/>.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="sinT">The sine of the angle of rotation of Ellipse about it's center.</param>
        /// <param name="cosT">The cosine of the angle of rotation of Ellipse about it's center.</param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// Based off of: http://stackoverflow.com/questions/7946187/point-and-ellipse-rotated-position-test-algorithm
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion EllipseContainsPoint(double cX, double cY, double r1, double r2, double sinT, double cosT, double pX, double pY)
        {
            if (r1 <= 0d || r2 <= 0d)
            {
                return Inclusion.Outside;
            }

            // Translate point to origin.
            var u = pX - cX;
            var v = pY - cY;

            // Apply the rotation transformation.
            var a = (u * cosT + v * sinT);
            var b = (u * sinT - v * cosT);

            var normalizedRadius = ((a * a) / (r1 * r1)) + ((b * b) / (r2 * r2));

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
            {
                return Inclusion.Outside;
            }

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
                {
                    return Inclusion.Boundary;
                }
                // Check whether the point is on the same side of the chord as the center.
                else if (Sign(determinant) == Sign(sweepAngle))
                {
                    return Inclusion.Outside;
                }

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
            => EllipticalArcContainsPoint(cX, cY, r1, r2, Sin(angle), Cos(angle), startAngle, sweepAngle, pX, pY, epsilon);

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="EllipticalArc"/>.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="sinT">The sine of the angle of rotation of Ellipse about it's center.</param>
        /// <param name="cosT">The cosine of the angle of rotation of Ellipse about it's center.</param>
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
        public static Inclusion EllipticalArcContainsPoint(double cX, double cY, double r1, double r2, double sinT, double cosT, double startAngle, double sweepAngle, double pX, double pY, double epsilon = Epsilon)
        {
            // If the ellipse is empty it can't contain anything.
            if (r1 <= 0d || r2 <= 0d)
            {
                return Inclusion.Outside;
            }

            // Find the start and end angles.
            var sa = EllipticalPolarAngle(startAngle, r1, r2);
            var ea = EllipticalPolarAngle(startAngle + sweepAngle, r1, r2);

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
            {
                return Inclusion.Outside;
            }

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
            => EllipticalArcSectorContainsPoint(cX, cY, r1, r2, Sin(angle), Cos(angle), startAngle, sweepAngle, pX, pY);

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="EllipticalArc"/>.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="sinT">The sine of the angle of rotation of Ellipse about it's center.</param>
        /// <param name="cosT">The cosine of the angle of rotation of Ellipse about it's center.</param>
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
        public static Inclusion EllipticalArcSectorContainsPoint(double cX, double cY, double r1, double r2, double sinT, double cosT, double startAngle, double sweepAngle, double pX, double pY)
        {
            // If the ellipse is empty it can't contain anything.
            if (r1 <= 0d || r2 <= 0d)
            {
                return Inclusion.Outside;
            }

            // Find the start and end angles.
            var sa = EllipticalPolarAngle(startAngle, r1, r2);
            var ea = EllipticalPolarAngle(startAngle + sweepAngle, r1, r2);

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
            {
                return Inclusion.Outside;
            }

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
                    {
                        j = 0;
                    }

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
                        {
                            return Inclusion.Outside;
                        }
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
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
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
                || ((pX == lI) && (pY == lJ))
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
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
            {
                // Return whether line segments are coincidental.
                return (PointLineSegmentIntersects(x2, y2, x0, y0, x1, y1) || PointLineSegmentIntersects(x3, y3, x0, y0, x1, y1));
            }

            // Find the index where the intersection point lies on the line.
            var s = ((x0 - x2) * v1 + (y2 - y0) * u1) / -determinant;
            var t = ((x2 - x0) * v2 + (y0 - y2) * u2) / determinant;

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
        /// <param name="epsilon">The minimal value to represent a change.</param>
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns a Boolean value indicating whether the two shapes intersect.</returns>
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
            // ToDo: Figure out ray intersection.

            // Translate lines to origin.
            var u1 = (x1 - x0);
            var v1 = (y1 - y0);
            var u2 = (x3 - x2);
            var v2 = (y3 - y2);

            // Calculate the determinant of the coefficient matrix.
            var determinant = (v2 * u1) - (u2 * v1);

            // Check if the line segments are parallel.
            if (Abs(determinant) < epsilon)
            {
                // Return whether line segments are coincidental.
                return (PointLineSegmentIntersects(x2, y2, x0, y0, x1, y1) || PointLineSegmentIntersects(x3, y3, x0, y0, x1, y1));
            }

            // Find the index where the intersection point lies on the line.
            var t = ((x0 - x2) * v1 + (y2 - y0) * u1) / -determinant;

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
        /// <param name="epsilon">The minimal value to represent a change.</param>
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
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
            var u1 = (x1 - x0);
            var v1 = (y1 - y0);
            var u2 = (x3 - x2);
            var v2 = (y3 - y2);

            // Calculate the determinant of the coefficient matrix.
            var determinant = (v2 * u1) - (u2 * v1);

            // Check if the line segments are parallel.
            if (Abs(determinant) < epsilon)
            {
                // Return whether line segments are coincidental.
                return (PointLineSegmentIntersects(x2, y2, x0, y0, x1, y1) || PointLineSegmentIntersects(x3, y3, x0, y0, x1, y1));
            }

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
            var u1 = (x1 - x0);
            var v1 = (y1 - y0);
            var u2 = (x3 - x2);
            var v2 = (y3 - y2);

            // Calculate the determinant of the coefficient matrix.
            var determinant = (v2 * u1) - (u2 * v1);

            // Check if the line segments are parallel.
            if (Abs(determinant) < epsilon)
            {
                // Return whether line segments are coincidental.
                return (PointLineSegmentIntersects(x2, y2, x0, y0, x1, y1) || PointLineSegmentIntersects(x3, y3, x0, y0, x1, y1));
            }

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
                => (Abs(((y3 - y2) * (x1 - x0)) - ((x3 - x2) * (y1 - y0))) < epsilon);

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
        /// <param name="epsilon">The minimal value to represent a change.</param>
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
        /// Find out whether a quadratic bezier segment and a line segment intersects.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
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
            // ToDo: Figure out code to check whether a quadratic Bezier curve and a line segment intersect.
            => throw new NotImplementedException();

        /// <summary>
        /// Find out whether a quadratic bezier segment and a rectangle intersects.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
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
        /// Find out whether a cubic bezier segment and a line segment intersects.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
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
            // ToDo: Figure out code to check whether a cubic Bezier curve and a line segment intersect.
            => throw new NotImplementedException();

        /// <summary>
        /// Find out whether a cubic bezier segment and a rectangle intersects.
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection PointLineIntersection(
            double pX, double pY,
            double lx, double ly, double i, double j,
            double epsilon = Epsilon)
        {
            var result = new Intersection(IntersectionState.NoIntersection);
            if (i == 0 && pX == lx)
            {
                result.AppendPoint(new Point2D(pX, pY));
            }
            else if ((pY - ly) / (pX - lx) == j / i)
            {
                result.AppendPoint(new Point2D(pX, pY));
            }

            if (result.Count > 0)
            {
                result.State |= IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection of a point and a ray.
        /// </summary>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <param name="lX"></param>
        /// <param name="lY"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection PointRayIntersection(
            double pX, double pY,
            double lX, double lY, double i, double j,
            double epsilon = Epsilon)
            => (PointRayIntersects(pX, pY, lX, lY, i, j, epsilon))
            ? new Intersection(IntersectionState.Intersection, new Point2D(pX, pY))
            : new Intersection(IntersectionState.NoIntersection);

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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection PointLineSegmentIntersection(
            double pX, double pY,
            double lAX, double lAY, double lBX, double lBY,
            double epsilon = Epsilon)
            => (PointLineSegmentIntersects(pX, pY, lAX, lAY, lBX, lBY, epsilon))
            ? new Intersection(IntersectionState.Intersection, new Point2D(pX, pY))
            : new Intersection(IntersectionState.NoIntersection);

        /// <summary>
        /// Find the intersection point between two lines.
        /// </summary>
        /// <param name="lx0">The x component of the first point of the first line.</param>
        /// <param name="ly0">The y component of the first point of the first line.</param>
        /// <param name="li0">The x component of the second point of the first line.</param>
        /// <param name="lj0">The y component of the second point of the first line.</param>
        /// <param name="lx1">The x component of the first point of the second line.</param>
        /// <param name="ly1">The y component of the first point of the second line.</param>
        /// <param name="li1">The x component of the second point of the second line.</param>
        /// <param name="lj1">The y component of the second point of the second line.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.vb-helper.com/howto_segments_intersect.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineLineIntersection(
            double lx0, double ly0, double li0, double lj0,
            double lx1, double ly1, double li1, double lj1,
            double epsilon = Epsilon)
        {
            // Initialize the intersection results.
            var result = new Intersection(IntersectionState.NoIntersection);

            // Calculate the determinant of the coefficient matrix.
            var determinant = (lj1 * li0) - (li1 * lj0);

            // Check if the lines are parallel.
            if (Abs(determinant) < epsilon)
            {
                // Lines are parallel. There are an infinite number of intersections.
                // ToDo: Figure out how to return a line as an intersection.
                return result;
            }

            // Find the index where the intersection point lies on the line.
            var t = ((lx1 - lx0) * lj1 + (ly0 - ly1) * li1) / determinant;

            // Return the intersection point.
            result.AppendPoint(new Point2D(lx0 + t * li0, ly0 + t * lj0));
            result.State |= IntersectionState.Intersection;
            return result;
        }

        /// <summary>
        /// Find the intersection point between a line and a ray.
        /// </summary>
        /// <param name="lx">The x component of the first point of the line.</param>
        /// <param name="ly">The y component of the first point of the line.</param>
        /// <param name="li">The x component of the second point of the line.</param>
        /// <param name="lj">The y component of the second point of the line.</param>
        /// <param name="rx">The x component of the first point of the ray.</param>
        /// <param name="ry">The y component of the first point of the ray.</param>
        /// <param name="ri">The x component of the second point of the ray.</param>
        /// <param name="rj">The y component of the second point of the ray.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.vb-helper.com/howto_segments_intersect.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineRayIntersection(
            double lx, double ly, double li, double lj,
            double rx, double ry, double ri, double rj,
            double epsilon = Epsilon)
        {
            // Initialize the intersection result.
            var result = new Intersection(IntersectionState.Intersection);

            // Intersection cross product.
            var ua = (li * (ry - ly)) - (lj * (rx - lx));
            var ub = (ri * (ry - ly)) - (rj * (rx - lx));

            // Calculate the determinant of the coefficient matrix.
            var determinant = (lj * ri) - (li * rj);

            // Check if the lines are parallel.
            if (Abs(determinant) < epsilon)
            {
                if (ua == 0 || ub == 0)
                {
                    //// Line segment is coincident to the Line. There are an infinite number of intersections, but we only care about the start and end points of the line segment.
                    //result.AppendPoints(new List<Point2D> { new Point2D(x2, y2), new Point2D(x3, y3) });
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
                //var tb = ub / determinant;

                if (ta >= 0 /*&& ta <= 1 && tb >= 0 && tb <= 1*/)
                {
                    // One intersection.
                    result.AppendPoint(new Point2D(rx + ta * ri, ry + ta * rj));
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
        /// Find the intersection point between two line segments.
        /// </summary>
        /// <param name="lx"></param>
        /// <param name="ly"></param>
        /// <param name="li"></param>
        /// <param name="lj"></param>
        /// <param name="s0X"></param>
        /// <param name="s0Y"></param>
        /// <param name="s1X"></param>
        /// <param name="s1Y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineLineSegmentIntersection(
            double lx, double ly, double li, double lj,
            double s0X, double s0Y, double s1X, double s1Y,
            double epsilon = Epsilon)
        {
            // Initialize the intersection result.
            var result = new Intersection(IntersectionState.Intersection);

            // Translate lines to origin.
            (var vi, var vj) = (s1X - s0X, s1Y - s0Y);

            var ua = vi * (ly - s0Y) - vj * (lx - s0X);
            var ub = li * (ly - s0Y) - lj * (lx - s0X);

            // Calculate the determinant of the coefficient matrix.
            var determinant = (vj * li) - (vi * lj);

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
                    result.AppendPoint(new Point2D(lx + ta * li, ly + ta * lj));
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
        /// <param name="b0x"></param>
        /// <param name="b0y"></param>
        /// <param name="b1x"></param>
        /// <param name="b1y"></param>
        /// <param name="b2x"></param>
        /// <param name="b2y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineQuadraticBezierIntersection(
            double x1, double y1, double x2, double y2,
            double b0x, double b0y, double b1x, double b1y, double b2x, double b2y,
            double epsilon = Epsilon)
            => LineQuadraticBezierIntersection(
                x1, y1, x2, y2,
                Polynomial.Quadratic(b0x, b1x, b2x),
                Polynomial.Quadratic(b0y, b1y, b2y),
                epsilon);

        /// <summary>
        /// Find the intersection between a line and a quadratic bezier.
        /// </summary>
        /// <param name="lx"></param>
        /// <param name="ly"></param>
        /// <param name="li"></param>
        /// <param name="lj"></param>
        /// <param name="xCurve">The set of Polynomial Bezier Coefficients of the x coordinates of the Bezier curve.</param>
        /// <param name="yCurve">The set of Polynomial Bezier Coefficients of the y coordinates of the Bezier curve.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// Adapted from code found at: https://www.particleincell.com/2013/cubic-line-intersection/
        /// Which was based off of code found at: http://stackoverflow.com/questions/14005096/mathematical-solution-for-bezier-curve-and-line-intersection-in-coffeescript-or
        /// Which was based off of code found at: http://www.blitzbasic.com/Community/posts.php?topic=64459 (https://web.archive.org/web/20111206104736/http://www.blitzbasic.com/Community/posts.php?topic=64459)
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineQuadraticBezierIntersection(
            double lx, double ly, double li, double lj,
            Polynomial xCurve, Polynomial yCurve,
            double epsilon = Epsilon)
        {
            // Initialize intersection.
            var result = new Intersection(IntersectionState.NoIntersection);

            var c = lx * (ly - lj) + ly * (li - lx);

            // Find the polynomial that represents the intersections.
            var roots = (lj * xCurve - li * yCurve + c).Trim().Roots();

            foreach (var s in roots)
            {
                // Add intersection point.
                if (!(s < 0 || s > 1d))
                {
                    result.AppendPoint(new Point2D(
                        xCurve[0] * s * s + xCurve[1] * s + xCurve[2],
                        yCurve[0] * s * s + yCurve[1] * s + yCurve[2]));
                }
            }

            // Return result.
            if (result.Count > 0)
            {
                result.State |= IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a line and a cubic bezier.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="b0x"></param>
        /// <param name="b0y"></param>
        /// <param name="b1x"></param>
        /// <param name="b1y"></param>
        /// <param name="b2x"></param>
        /// <param name="b2y"></param>
        /// <param name="b3x"></param>
        /// <param name="b3y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineCubicBezierIntersection(
            double x1, double y1, double x2, double y2,
            double b0x, double b0y, double b1x, double b1y, double b2x, double b2y, double b3x, double b3y,
            double epsilon = Epsilon)
            => LineCubicBezierIntersection(
                x1, y1, x2, y2,
                Polynomial.Cubic(b0x, b1x, b2x, b3x),
                Polynomial.Cubic(b0y, b1y, b2y, b3y),
                epsilon);

        /// <summary>
        /// Find the intersection between a line and a cubic bezier.
        /// </summary>
        /// <param name="lx"></param>
        /// <param name="ly"></param>
        /// <param name="li"></param>
        /// <param name="lj"></param>
        /// <param name="xCurve">The set of Polynomial Bezier Coefficients of the x coordinates of the Bezier curve.</param>
        /// <param name="yCurve">The set of Polynomial Bezier Coefficients of the y coordinates of the Bezier curve.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// Adapted from code found at: https://www.particleincell.com/2013/cubic-line-intersection/
        /// Which was based off of code found at: http://stackoverflow.com/questions/14005096/mathematical-solution-for-bezier-curve-and-line-intersection-in-coffeescript-or
        /// Which was based off of code found at: http://www.blitzbasic.com/Community/posts.php?topic=64459 (https://web.archive.org/web/20111206104736/http://www.blitzbasic.com/Community/posts.php?topic=64459)
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineCubicBezierIntersection(
            double lx, double ly, double li, double lj,
            Polynomial xCurve, Polynomial yCurve,
            double epsilon = Epsilon)
        {
            // Initialize the intersection.
            var result = new Intersection(IntersectionState.NoIntersection);

            var c = lx * (ly - lj) + ly * (li - lx);

            // Find the polynomial that represents the intersections.
            var roots = (lj * xCurve - li * yCurve + c).Trim().Roots();

            foreach (var s in roots)
            {
                // Add intersection point.
                if (!(s < 0 || s > 1d))
                {
                    result.AppendPoint(new Point2D(
                    xCurve[0] * s * s * s + xCurve[1] * s * s + xCurve[2] * s + xCurve[3],
                    yCurve[0] * s * s * s + yCurve[1] * s * s + yCurve[2] * s + yCurve[3]));
                }
            }

            if (result.Count > 0)
            {
                result.State |= IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a Line and a triangle.
        /// </summary>
        /// <param name="lx"></param>
        /// <param name="ly"></param>
        /// <param name="li"></param>
        /// <param name="lj"></param>
        /// <param name="tx0"></param>
        /// <param name="ty0"></param>
        /// <param name="tx1"></param>
        /// <param name="ty1"></param>
        /// <param name="tx2"></param>
        /// <param name="ty2"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineTriangleIntersection(
            double lx, double ly, double li, double lj,
            double tx0, double ty0, double tx1, double ty1, double tx2, double ty2,
            double epsilon = Epsilon)
        {
            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();

            intersections.UnionWith(LineLineSegmentIntersection(lx, ly, li, lj, tx0, ty0, tx1, ty1, epsilon).Points);
            intersections.UnionWith(LineLineSegmentIntersection(lx, ly, li, lj, tx1, ty1, tx2, ty2, epsilon).Points);
            intersections.UnionWith(LineLineSegmentIntersection(lx, ly, li, lj, tx0, ty0, tx2, ty2, epsilon).Points);

            // ToDo: Return IntersectionState.Inside if all of the points of one rectangle are inside the other rectangle.

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionState.Intersection;
            }

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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
            var (minX, minY) = MinPoint(r1X, r1Y, r2X, r2Y);
            var (maxX, maxY) = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(maxX, minY);
            var bottomLeft = new Point2D(minX, maxY);

            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            intersections.UnionWith(LineLineSegmentIntersection(a1X, a1Y, a2X, a2Y, minX, minY, topRight.X, topRight.Y, epsilon).Points);
            intersections.UnionWith(LineLineSegmentIntersection(a1X, a1Y, a2X, a2Y, topRight.X, topRight.Y, maxX, maxY, epsilon).Points);
            intersections.UnionWith(LineLineSegmentIntersection(a1X, a1Y, a2X, a2Y, maxX, maxY, bottomLeft.X, bottomLeft.Y, epsilon).Points);
            intersections.UnionWith(LineLineSegmentIntersection(a1X, a1Y, a2X, a2Y, bottomLeft.X, bottomLeft.Y, minX, minY, epsilon).Points);

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Count > 0)
            {
                result.State = IntersectionState.Intersection;
            }

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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
            {
                result.State |= IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a line and a circle.
        /// </summary>
        /// <param name="lx"></param>
        /// <param name="ly"></param>
        /// <param name="li"></param>
        /// <param name="lj"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineCircleIntersection(
            double lx, double ly, double li, double lj,
            double cX, double cY, double r,
            double epsilon = Epsilon)
        {
            // Initialize the intersection result.
            var result = new Intersection(IntersectionState.NoIntersection);

            // If the circle or line segment are empty, return no intersections.
            if ((r == 0d) || ((lx == li) && (ly == lj)))
            {
                return result;
            }

            // Calculate the quadratic parameters.
            var a = li * li + lj * lj;
            var b = 2 * (li * (lx - cX) + lj * (ly - cY));
            var c = (lx - cX) * (lx - cX) + (ly - cY) * (ly - cY) - r * r;

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
                result.AppendPoint(new Point2D(lx + t * li, ly + t * lj));
            }
            else if (discriminant > 0)
            {
                // Two possible solutions.
                var t1 = ((-b + Sqrt(discriminant)) / (2 * a));
                var t2 = ((-b - Sqrt(discriminant)) / (2 * a));

                // Add the points.
                result = new Intersection(IntersectionState.Intersection);
                result.AppendPoint(new Point2D(lx + t1 * li, ly + t1 * lj));
                result.AppendPoint(new Point2D(lx + t2 * li, ly + t2 * lj));
            }

            // Return result.
            return result;
        }

        /// <summary>
        /// Find the intersection between a line and a circular arc.
        /// </summary>
        /// <param name="lx"></param>
        /// <param name="ly"></param>
        /// <param name="li"></param>
        /// <param name="lj"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="angle"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineCircularArcIntersection(
            double lx, double ly, double li, double lj,
            double cX, double cY, double r, double angle, double startAngle, double sweepAngle,
            double epsilon = Epsilon)
        {
            // Initialize the intersection.
            var result = new Intersection(IntersectionState.NoIntersection);

            // If the circle or line segment are empty, return no intersections.
            if ((r == 0d) || ((lx == li) && (ly == lj)))
            {
                return result;
            }

            // Calculate the quadratic parameters.
            var a = li * li + lj * lj;
            var b = 2 * (li * (lx - cX) + lj * (ly - cY));
            var c = (lx - cX) * (lx - cX) + (ly - cY) * (ly - cY) - r * r;

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
                var pX = lx + t * li;
                var pY = ly + t * lj;

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
                var pX = lx + t1 * li;
                var pY = ly + t1 * lj;

                // Find the determinant of the chord and point.
                var determinant = (startPoint.X - pX) * (endPoint.Y - pY) - (endPoint.X - pX) * (startPoint.Y - pY);

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle))
                {
                    // Add the point.
                    result.AppendPoint(new Point2D(pX, pY));
                }

                // Find the point.
                pX = lx + t2 * li;
                pY = ly + t2 * lj;

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
            {
                result.State |= IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a line and an ellipse.
        /// </summary>
        /// <param name="lx"></param>
        /// <param name="ly"></param>
        /// <param name="li"></param>
        /// <param name="lj"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="angle"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineEllipseIntersection(
            double lx, double ly, double li, double lj,
            double cx, double cy, double rx, double ry, double angle,
            double epsilon = Epsilon)
            => LineEllipseIntersection(lx, ly, li, lj, cx, cy, rx, ry, Cos(angle), Sin(angle), epsilon);

        /// <summary>
        /// Find the intersection between a line and an ellipse.
        /// </summary>
        /// <param name="lx"></param>
        /// <param name="ly"></param>
        /// <param name="li"></param>
        /// <param name="lj"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="cosA"></param>
        /// <param name="sinA"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2012/09/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineEllipseIntersection(
            double lx, double ly, double li, double lj,
            double cx, double cy, double rx, double ry, double cosA, double sinA,
            double epsilon = Epsilon)
        {
            // Initialize the resulting intersection structure.
            var result = new Intersection(IntersectionState.NoIntersection);

            // If the ellipse or line segment are empty, return no intersections.
            if ((rx == 0d) || (ry == 0d) ||
                ((lx == li) && (ly == lj)))
            {
                return result;
            }

            // Translate the line to put the ellipse centered at the origin.
            var u1 = lx - cx;
            var v1 = ly - cy;
            var u2 = lx + li - cx;
            var v2 = ly + lj - cy;

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
            {
                result.State |= IntersectionState.Intersection;
            }

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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
        /// <param name="lx"></param>
        /// <param name="ly"></param>
        /// <param name="li"></param>
        /// <param name="lj"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2012/09/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineEllipticalArcIntersection(
            double lx, double ly, double li, double lj,
            double cx, double cy, double rx, double ry, double cosA, double sinA, double startAngle, double sweepAngle,
            double epsilon = Epsilon)
        {
            // Initialize the resulting intersection structure.
            var result = new Intersection(IntersectionState.NoIntersection);

            // If the ellipse or line segment are empty, return no intersections.
            if ((sweepAngle == 0d) || (rx == 0d) || (ry == 0d) ||
                ((li == 0) && (lj == 0)))
            {
                return result;
            }

            // Translate the line to put it at the ellipse centered at the origin.
            var u0 = lx - cx;
            var v0 = ly - cy;
            var u1 = lx + li - cx;
            var v1 = ly + lj - cy;

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
                {
                    result.AppendPoint(p);
                }
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
                {
                    result.AppendPoint(p);
                }

                // Find the point.
                p = new Point2D(u0 + (u1 - u0) * t2 + cx, v0 + (v1 - v0) * t2 + cy);

                // Find the determinant of the matrix representing the chord.
                determinant = (sx - p.X) * (ey - p.Y) - (ex - p.X) * (sy - p.Y);

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle))
                {
                    result.AppendPoint(p);
                }
            }

            // Return the intersections.
            if (result.Count > 0)
            {
                result.State |= IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a line segment and a ray.
        /// </summary>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="ri"></param>
        /// <param name="rj"></param>
        /// <param name="s1X"></param>
        /// <param name="s1Y"></param>
        /// <param name="s2X"></param>
        /// <param name="s2Y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection RayLineSegmentIntersection(
            double rx, double ry, double ri, double rj,
            double s1X, double s1Y, double s2X, double s2Y,
            double epsilon = Epsilon)
        {
            // Initialize the intersection result.
            var result = new Intersection(IntersectionState.Intersection);

            // Translate line segment to origin.
            var u = s2X - s1X;
            var v = s2Y - s1Y;

            // Intersection cross product.
            var ua = u * (ry - s1Y) - v * (rx - s1X);
            var ub = ri * (ry - s1Y) - rj * (rx - s1X);

            // Calculate the determinant of the coefficient matrix.
            var determinant = (v * ri) - (u * rj);

            // Check if the lines are parallel.
            if (Abs(determinant) < epsilon)
            {
                if (ua == 0 || ub == 0)
                {
                    //// Line segment is coincident to the Line. There are an infinite number of intersections, but we only care about the start and end points of the line segment.
                    //result.AppendPoints(new List<Point2D> { new Point2D(s1X, s1Y), new Point2D(s2X, s2Y) });
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

                if (ta >= 0 /*&& ta <= 1*/ && tb >= 0 && tb <= 1)
                {
                    // One intersection.
                    result.AppendPoint(new Point2D(rx + ta * ri, ry + ta * rj));
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
        /// Find the intersection between two rays.
        /// </summary>
        /// <param name="r0x"></param>
        /// <param name="r0y"></param>
        /// <param name="r0i"></param>
        /// <param name="r0j"></param>
        /// <param name="r1x"></param>
        /// <param name="r1y"></param>
        /// <param name="r1i"></param>
        /// <param name="r1j"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection RayRayIntersection(
            double r0x, double r0y, double r0i, double r0j,
            double r1x, double r1y, double r1i, double r1j,
            double epsilon = Epsilon)
        {
            // Initialize the intersection result.
            var result = new Intersection(IntersectionState.NoIntersection);

            // Intersection cross product.
            var ua = (r1i) * (r0y - r1y) - (r1j) * (r0x - r1x);
            var ub = (r0i) * (r0y - r1y) - (r0j) * (r0x - r1x);

            // Calculate the determinant of the coefficient matrix.
            var determinant = (r1j) * (r0i) - (r1i) * (r0j);

            if (Abs(determinant) < epsilon)
            {
                if (ua == 0 || ub == 0)
                {
                    result.State |= IntersectionState.Coincident;
                }
                else
                {
                    result.State |= IntersectionState.Parallel;
                }
            }
            else
            {
                var ta = ua / determinant;
                var tb = ub / determinant;

                if (ta >= 0 /*&& ta <= 1*/ && tb >= 0 /*&& tb <= 1*/)
                {
                    // One intersection.
                    result.State = IntersectionState.Intersection;
                    result.AppendPoint(new Point2D(r0x + ta * r0i, r0y + ta * r0j));
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
        /// Find the intersection between a Ray and a quadratic bezier.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="i1"></param>
        /// <param name="j1"></param>
        /// <param name="xCurve">The set of Polynomial Bezier Coefficients of the x coordinates of the Bezier curve.</param>
        /// <param name="yCurve">The set of Polynomial Bezier Coefficients of the y coordinates of the Bezier curve.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// Adapted from code found at: https://www.particleincell.com/2013/cubic-line-intersection/
        /// Which was based off of code found at: http://stackoverflow.com/questions/14005096/mathematical-solution-for-bezier-curve-and-line-intersection-in-coffeescript-or
        /// Which was based off of code found at: http://www.blitzbasic.com/Community/posts.php?topic=64459
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection RayQuadraticBezierSegmentIntersection(
            double x1, double y1, double i1, double j1,
            Polynomial xCurve, Polynomial yCurve,
            double epsilon = Epsilon)
        {
            // Initialize the intersection.
            var result = new Intersection(IntersectionState.NoIntersection);

            var c = x1 * j1 + y1 * i1;

            // Find the polynomial that represents the intersections.
            var roots = (j1 * xCurve - i1 * yCurve + c).Trim().Roots();

            foreach (var s in roots)
            {
                // Intersection point assuming it was an infinitely long line.
                var x = xCurve[0] * s * s + xCurve[1] * s + xCurve[2];
                var y = yCurve[0] * s * s + yCurve[1] * s + yCurve[2];

                double slope;
                // Special handling for vertical lines.
                if (i1 != 0)
                {
                    slope = (x - x1) / i1;
                }
                else
                {
                    slope = (y - y1) / j1;
                }

                // Make sure we are in bounds of the line segment.
                if (!(s < 0 /*|| s > 1d*/ || slope < 0 || slope > 1d))
                {
                    // Add intersection point.
                    result.AppendPoint(new Point2D(x, y));
                }
            }

            // Return the result.
            if (result.Count > 0)
            {
                result.State |= IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a ray and a cubic bezier.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="i1"></param>
        /// <param name="j1"></param>
        /// <param name="xCurve">The set of Polynomial Bezier Coefficients of the x coordinates of the Bezier curve.</param>
        /// <param name="yCurve">The set of Polynomial Bezier Coefficients of the y coordinates of the Bezier curve.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// Adapted from code found at: https://www.particleincell.com/2013/cubic-line-intersection/
        /// Which was based off of code found at: http://stackoverflow.com/questions/14005096/mathematical-solution-for-bezier-curve-and-line-intersection-in-coffeescript-or
        /// Which was based off of code found at: http://www.blitzbasic.com/Community/posts.php?topic=64459
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection RayCubicBezierSegmentIntersection(
            double x1, double y1, double i1, double j1,
            Polynomial xCurve, Polynomial yCurve,
            double epsilon = Epsilon)
        {
            // Initialize the intersection.
            var result = new Intersection(IntersectionState.NoIntersection);

            var c = x1 * -j1 + y1 * i1;

            // Find the polynomial that represents the intersections.
            var roots = (j1 * xCurve + -i1 * yCurve + c).Trim().Roots();

            foreach (var s in roots)
            {
                // Intersection point assuming infinitely long line segment.
                var point = new Point2D(
                    xCurve[0] * s * s * s + xCurve[1] * s * s + xCurve[2] * s + xCurve[3],
                    yCurve[0] * s * s * s + yCurve[1] * s * s + yCurve[2] * s + yCurve[3]);

                double slope;

                // Special handling for vertical lines.
                if (i1 != 0)
                {
                    slope = (point.X - x1) / i1;
                }
                else
                {
                    slope = (point.Y - y1) / j1;
                }

                // Make sure we are in bounds of the line segment.
                if (!(s < 0 /*|| s > 1d*/ || slope < 0 || slope > 1d))
                {
                    // Add intersection point.
                    result.AppendPoint(point);
                }
            }

            if (result.Count > 0)
            {
                result.State |= IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a ray and a triangle.
        /// </summary>
        /// <param name="lx0"></param>
        /// <param name="ly0"></param>
        /// <param name="lx1"></param>
        /// <param name="ly1"></param>
        /// <param name="tx0"></param>
        /// <param name="ty0"></param>
        /// <param name="tx1"></param>
        /// <param name="ty1"></param>
        /// <param name="tx2"></param>
        /// <param name="ty2"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection RayTriangleIntersection(
            double lx0, double ly0, double lx1, double ly1,
            double tx0, double ty0, double tx1, double ty1, double tx2, double ty2,
            double epsilon = Epsilon)
        {
            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();

            intersections.UnionWith(RayLineSegmentIntersection(lx0, ly0, lx1, ly1, tx0, ty0, tx1, ty1, epsilon).Points);
            intersections.UnionWith(RayLineSegmentIntersection(lx0, ly0, lx1, ly1, tx1, ty1, tx2, ty2, epsilon).Points);
            intersections.UnionWith(RayLineSegmentIntersection(lx0, ly0, lx1, ly1, tx0, ty0, tx2, ty2, epsilon).Points);

            // ToDo: Return IntersectionState.Inside if all of the points of one rectangle are inside the other rectangle.

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a ray and a rectangle.
        /// </summary>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="ri"></param>
        /// <param name="rj"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection RayRectangleIntersection(
            double rx, double ry, double ri, double rj,
            double r1X, double r1Y, double r2X, double r2Y,
            double epsilon = Epsilon)
        {
            var (minX, minY) = MinPoint(r1X, r1Y, r2X, r2Y);
            var (maxX, maxY) = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(maxX, minY);
            var bottomLeft = new Point2D(minX, maxY);

            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            intersections.UnionWith(RayLineSegmentIntersection(rx, ry, ri, rj, minX, minY, topRight.X, topRight.Y, epsilon).Points);
            intersections.UnionWith(RayLineSegmentIntersection(rx, ry, ri, rj, topRight.X, topRight.Y, maxX, maxY, epsilon).Points);
            intersections.UnionWith(RayLineSegmentIntersection(rx, ry, ri, rj, maxX, maxY, bottomLeft.X, bottomLeft.Y, epsilon).Points);
            intersections.UnionWith(RayLineSegmentIntersection(rx, ry, ri, rj, bottomLeft.X, bottomLeft.Y, minX, minY, epsilon).Points);

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Count > 0)
            {
                result.State = IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a ray and a polygon contour.
        /// </summary>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <param name="points"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection RayPolygonContourIntersection(
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

                intersections.UnionWith(RayLineSegmentIntersection(b1.X, b1.Y, b2.X, b2.Y, a1X, a1Y, a2X, a2Y, epsilon).Points);

                b1 = b2;
            }

            // ToDo: Return IntersectionState.Inside if both end points are inside the polygon, and there are no intersections.

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Count > 0)
            {
                result.State |= IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a ray and a circle.
        /// </summary>
        /// <param name="lAX"></param>
        /// <param name="lAY"></param>
        /// <param name="lBi"></param>
        /// <param name="lBj"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="angle"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection RayCircleIntersection(
            double lAX, double lAY, double lBi, double lBj,
            double cX, double cY, double r, double angle,
            double epsilon = Epsilon)
        {
            Intersection result;

            var a = (lBi) * (lBi) + (lBj) * (lBj);
            var b = 2 * ((lBi) * (lAX - cX) + (lBj) * (lAY - cY));
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
                    {
                        result.Points.Add(Lerp(lAX, lAY, lBi, lBj, u1));
                    }
                }
            }
            else
            {
                var e = Sqrt(determinant);
                var u1 = (-b + e) / (2 * a);
                var u2 = (-b - e) / (2 * a);
                if ((u1 < 0 /*|| u1 > 1*/) && (u2 < 0 || u2 > 1))
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
                    if (0 <= u1/* && u1 <= 1*/)
                    {
                        result.Points.Add(Lerp(lAX, lAY, lBi, lBj, u1));
                    }

                    if (0 <= u2/* && u2 <= 1*/)
                    {
                        result.Points.Add(Lerp(lAX, lAY, lBi, lBj, u2));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a ray and a circular arc.
        /// </summary>
        /// <param name="lAX"></param>
        /// <param name="lAY"></param>
        /// <param name="lBI"></param>
        /// <param name="lBJ"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="angle"></param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection RayCircularArcIntersection(
            double lAX, double lAY, double lBI, double lBJ,
            double cX, double cY, double r, double angle, double startAngle, double sweepAngle,
            double epsilon = Epsilon)
        {
            var result = new Intersection(IntersectionState.NoIntersection);

            // If the circle or line segment are empty, return no intersections.
            if ((r == 0d) || ((0d == lBI) && (0d == lBJ)))
            {
                return result;
            }

            // Calculate the quadratic parameters.
            var a = lBI * lBI + lBJ * lBJ;
            var b = 2 * (lBI * (lAX - cX) + lBJ * (lAY - cY));
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
                var pX = lAX + t * lBI;
                var pY = lAY + t * lBJ;

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
                var pX = lAX + t1 * lBI;
                var pY = lAY + t1 * lBJ;

                // Find the determinant of the chord and point.
                var determinant = (startPoint.X - pX) * (endPoint.Y - pY) - (endPoint.X - pX) * (startPoint.Y - pY);

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle) && (0 <= t1 && t1 <= 1))
                {
                    // Add the point.
                    result.AppendPoint(new Point2D(pX, pY));
                }

                // Find the point.
                pX = lAX + t2 * lBI;
                pY = lAY + t2 * lBJ;

                // Find the determinant of the chord and point.
                determinant = (startPoint.X - pX) * (endPoint.Y - pY) - (endPoint.X - pX) * (startPoint.Y - pY);

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle) && (0 <= t2/* && t2 <= 1*/))
                {
                    // Add the point.
                    result.AppendPoint(new Point2D(pX, pY));
                }
            }

            if (result.Count > 0)
            {
                result.State |= IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a ray and an ellipse.
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="i0"></param>
        /// <param name="j0"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="cosA">The cosine of the rotation angle.</param>
        /// <param name="sinA">The sine of the rotation angle.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2012/09/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection RayEllipseIntersection(
            double x0, double y0, double i0, double j0,
            double cx, double cy, double rx, double ry, double cosA, double sinA,
            double epsilon = Epsilon)
        {
            // Initialize the resulting intersection structure.
            var result = new Intersection(IntersectionState.NoIntersection);

            // If the ellipse or line segment are empty, return no intersections.
            if ((rx == 0d) || (ry == 0d) ||
                ((0 == i0) && (0 == j0)))
            {
                return result;
            }

            // Translate the line to put the ellipse centered at the origin.
            var u1 = x0 - cx;
            var v1 = y0 - cy;
            var u2 = x0 + i0 - cx;
            var v2 = y0 + j0 - cy;

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
                if ((t1 >= 0d)/* && (t1 <= 1d)*/)
                {
                    result.AppendPoint(new Point2D(u1 + (u2 - u1) * t1 + cx, v1 + (v2 - v1) * t1 + cy));
                }

                if ((t2 >= 0d)/* && (t2 <= 1d)*/)
                {
                    result.AppendPoint(new Point2D(u1 + (u2 - u1) * t2 + cx, v1 + (v2 - v1) * t2 + cy));
                }
            }

            // ToDo: Return IntersectionState.Inside if both end points are inside the ellipse.

            // Return the intersections.
            if (result.Count > 0)
            {
                result.State |= IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a ray and an elliptical arc.
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="cosA">The cosine of the rotation angle.</param>
        /// <param name="sinA">The sine of the rotation angle.</param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="i0"></param>
        /// <param name="j0"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2012/09/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection RayEllipticalArcIntersection(
            double x0, double y0, double i0, double j0,
            double cx, double cy, double rx, double ry, double cosA, double sinA, double startAngle, double sweepAngle,
            double epsilon = Epsilon)
        {
            // Initialize the resulting intersection structure.
            var result = new Intersection(IntersectionState.NoIntersection);

            // If the ellipse or line segment are empty, return no intersections.
            if ((sweepAngle == 0d) || (rx == 0d) || (ry == 0d) ||
                ((0 == i0) && (0 == j0)))
            {
                return result;
            }

            // Translate the line to move it to the ellipse centered at the origin.
            var u0 = x0 - cx;
            var v0 = y0 - cy;
            var u1 = x0 + i0 - cx;
            var v1 = y0 + j0 - cy;

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
                if (t >= 0d /*&& t <= 1d*/)
                {
                    // Find the point.
                    var p = new Point2D(u0 + (u1 - u0) * t + cx, v0 + (v1 - v0) * t + cy);

                    // Find the determinant of the matrix representing the chord.
                    var determinant = (sx - p.X) * (ey - p.Y) - (ex - p.X) * (sy - p.Y);

                    // Add the point if it is on the sweep side of the chord.
                    if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle))
                    {
                        result.AppendPoint(p);
                    }
                }
            }
            else if (discriminant > 0)
            {
                // Two real possible solutions.
                var root = Sqrt(discriminant);
                var t1 = (OneHalf * (-b + root) / a);
                var t2 = (OneHalf * (-b - root) / a);

                // Add the points if they are between the end points of the line segment.
                if ((t1 >= 0d) /*&& (t1 == 1d)*/)
                {
                    // Find the point.
                    var p = new Point2D(u0 + (u1 - u0) * t1 + cx, v0 + (v1 - v0) * t1 + cy);

                    // Find the determinant of the matrix representing the chord.
                    var determinant = (sx - p.X) * (ey - p.Y) - (ex - p.X) * (sy - p.Y);

                    // Add the point if it is on the sweep side of the chord.
                    if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle))
                    {
                        result.AppendPoint(p);
                    }
                }

                if ((t2 >= 0d) /*&& (t2 <= 1d)*/)
                {
                    // Find the point.
                    var p = new Point2D(u0 + (u1 - u0) * t2 + cx, v0 + (v1 - v0) * t2 + cy);

                    // Find the determinant of the matrix representing the chord.
                    var determinant = (sx - p.X) * (ey - p.Y) - (ex - p.X) * (sy - p.Y);

                    // Add the point if it is on the sweep side of the chord.
                    if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle))
                    {
                        result.AppendPoint(p);
                    }
                }
            }

            // Return the intersections.
            if (result.Count > 0)
            {
                result.State |= IntersectionState.Intersection;
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentLineSegmentIntersection(
            double x1, double y1, double x2, double y2,
            double b1X, double b1Y, double b2X, double b2Y,
            double epsilon = Epsilon)
        {
            var result = new Intersection(IntersectionState.NoIntersection);

            var ua = (b2X - b1X) * (y1 - b1Y) - (b2Y - b1Y) * (x1 - b1X);
            var ub = (x2 - x1) * (y1 - b1Y) - (y2 - y1) * (x1 - b1X);

            var determinant = (b2Y - b1Y) * (x2 - x1) - (b2X - b1X) * (y2 - y1);

            if (determinant == 0)
            {
                if (ua == 0 || ub == 0)
                {
                    result.State = IntersectionState.Coincident;
                }
                else
                {
                    result.State = IntersectionState.Parallel;
                }
            }
            else
            {
                var ta = ua / determinant;
                var tb = ub / determinant;

                if (0 <= ta && ta <= 1 && 0 <= tb && tb <= 1)
                {
                    result.State = IntersectionState.Intersection;
                    result.AppendPoint(new Point2D(x1 + ta * (x2 - x1), y1 + ta * (y2 - y1)));
                }
                else
                {
                    result.State = IntersectionState.NoIntersection;
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
        /// <param name="b0x"></param>
        /// <param name="b0y"></param>
        /// <param name="b1x"></param>
        /// <param name="b1y"></param>
        /// <param name="b2x"></param>
        /// <param name="b2y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentQuadraticBezierSegmentIntersection(
            double x1, double y1, double x2, double y2,
            double b0x, double b0y, double b1x, double b1y, double b2x, double b2y,
            double epsilon = Epsilon)
            => LineSegmentQuadraticBezierSegmentIntersection(
                x1, y1, x2, y2,
                Polynomial.Quadratic(b0x, b1x, b2x),
                Polynomial.Quadratic(b0y, b1y, b2y),
                epsilon);

        /// <summary>
        /// Find the intersection between a line segment and a quadratic bezier.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="xCurve">The set of Polynomial Bezier Coefficients of the x coordinates of the Bezier curve.</param>
        /// <param name="yCurve">The set of Polynomial Bezier Coefficients of the y coordinates of the Bezier curve.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// Adapted from code found at: https://www.particleincell.com/2013/cubic-line-intersection/
        /// Which was based off of code found at: http://stackoverflow.com/questions/14005096/mathematical-solution-for-bezier-curve-and-line-intersection-in-coffeescript-or
        /// Which was based off of code found at: http://www.blitzbasic.com/Community/posts.php?topic=64459
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentQuadraticBezierSegmentIntersection(
            double x1, double y1, double x2, double y2,
            Polynomial xCurve, Polynomial yCurve,
            double epsilon = Epsilon)
        {
            // Initialize the intersection.
            var result = new Intersection(IntersectionState.NoIntersection);

            // Translate the line to the origin.
            var a = y2 - y1;
            var b = x1 - x2;

            var c = x1 * (y1 - y2) + y1 * (x2 - x1);

            // Find the roots of the polynomial that represents the intersections.
            var roots = (a * xCurve + b * yCurve + c).Trim().Roots();

            foreach (var s in roots)
            {
                // Intersection point assuming it was an infinitely long line.
                var x = xCurve[0] * s * s + xCurve[1] * s + xCurve[2];
                var y = yCurve[0] * s * s + yCurve[1] * s + yCurve[2];

                double slope;

                // Special handling for vertical lines.
                if ((x2 - x1) != 0)
                {
                    slope = (x - x1) / (x2 - x1);
                }
                else
                {
                    slope = (y - y1) / (y2 - y1);
                }

                // Make sure we are in bounds of the line segment.
                if (!(s < 0 || s > 1d || slope < 0 || slope > 1d))
                {
                    // Add intersection point.
                    result.AppendPoint(new Point2D(x, y));
                }
            }

            // Return the result.
            if (result.Count > 0)
            {
                result.State |= IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a line segment and a cubic bezier.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="b0x"></param>
        /// <param name="b0y"></param>
        /// <param name="b1x"></param>
        /// <param name="b1y"></param>
        /// <param name="b2x"></param>
        /// <param name="b2y"></param>
        /// <param name="b3x"></param>
        /// <param name="b3y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentCubicBezierSegmentIntersection(
            double x1, double y1, double x2, double y2,
            double b0x, double b0y, double b1x, double b1y, double b2x, double b2y, double b3x, double b3y,
            double epsilon = Epsilon)
            => LineSegmentCubicBezierSegmentIntersection(
                x1, y1, x2, y2,
                Polynomial.Cubic(b0x, b1x, b2x, b3x),
                Polynomial.Cubic(b0y, b1y, b2y, b3y),
                epsilon);

        /// <summary>
        /// Find the intersection between a line segment and a cubic bezier.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="xCurve">The set of Polynomial Bezier Coefficients of the x coordinates of the Bezier curve.</param>
        /// <param name="yCurve">The set of Polynomial Bezier Coefficients of the y coordinates of the Bezier curve.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// Adapted from code found at: https://www.particleincell.com/2013/cubic-line-intersection/
        /// Which was based off of code found at: http://stackoverflow.com/questions/14005096/mathematical-solution-for-bezier-curve-and-line-intersection-in-coffeescript-or
        /// Which was based off of code found at: http://www.blitzbasic.com/Community/posts.php?topic=64459
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentCubicBezierSegmentIntersection(
            double x1, double y1, double x2, double y2,
            Polynomial xCurve, Polynomial yCurve,
            double epsilon = Epsilon)
        {
            // Initialize the intersection.
            var result = new Intersection(IntersectionState.NoIntersection);

            // Translate the line to the origin.
            var a = y2 - y1;
            var b = x1 - x2;

            var c = x1 * (y1 - y2) + y1 * (x2 - x1);

            // Find the roots of the polynomial that represents the intersections.
            var roots = (a * xCurve + b * yCurve + c).Trim().Roots();

            foreach (var s in roots)
            {
                // Intersection point assuming infinitely long line segment.
                var point = new Point2D(
                    xCurve[0] * s * s * s + xCurve[1] * s * s + xCurve[2] * s + xCurve[3],
                    yCurve[0] * s * s * s + yCurve[1] * s * s + yCurve[2] * s + yCurve[3]);

                double slope;

                // Special handling for vertical lines.
                if ((x2 - x1) != 0)
                {
                    slope = (point.X - x1) / (x2 - x1);
                }
                else
                {
                    slope = (point.Y - y1) / (y2 - y1);
                }

                // Make sure we are in bounds of the line segment.
                if (!(s < 0 || s > 1d || slope < 0 || slope > 1d))
                {
                    // Add intersection point.
                    result.AppendPoint(point);
                }
            }

            if (result.Count > 0)
            {
                result.State |= IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a line segment and a triangle.
        /// </summary>
        /// <param name="lx0"></param>
        /// <param name="ly0"></param>
        /// <param name="lx1"></param>
        /// <param name="ly1"></param>
        /// <param name="tx0"></param>
        /// <param name="ty0"></param>
        /// <param name="tx1"></param>
        /// <param name="ty1"></param>
        /// <param name="tx2"></param>
        /// <param name="ty2"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentTriangleIntersection(
            double lx0, double ly0, double lx1, double ly1,
            double tx0, double ty0, double tx1, double ty1, double tx2, double ty2,
            double epsilon = Epsilon)
        {
            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();

            intersections.UnionWith(LineSegmentLineSegmentIntersection(lx0, ly0, lx1, ly1, tx0, ty0, tx1, ty1, epsilon).Points);
            intersections.UnionWith(LineSegmentLineSegmentIntersection(lx0, ly0, lx1, ly1, tx1, ty1, tx2, ty2, epsilon).Points);
            intersections.UnionWith(LineSegmentLineSegmentIntersection(lx0, ly0, lx1, ly1, tx0, ty0, tx2, ty2, epsilon).Points);

            // ToDo: Return IntersectionState.Inside if all of the points of one rectangle are inside the other rectangle.

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionState.Intersection;
            }

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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentRectangleIntersection(
            double lAX, double lAY, double lBX, double lBY,
            double r1X, double r1Y, double r2X, double r2Y,
            double epsilon = Epsilon)
        {
            var (minX, minY) = MinPoint(r1X, r1Y, r2X, r2Y);
            var (maxX, maxY) = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(maxX, minY);
            var bottomLeft = new Point2D(minX, maxY);

            // ToDo: Return IntersectionState.Inside if both end points are inside the rectangle.

            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            intersections.UnionWith(LineSegmentLineSegmentIntersection(minX, minY, topRight.X, topRight.Y, lAX, lAY, lBX, lBY, epsilon).Points);
            intersections.UnionWith(LineSegmentLineSegmentIntersection(topRight.X, topRight.Y, maxX, maxY, lAX, lAY, lBX, lBY, epsilon).Points);
            intersections.UnionWith(LineSegmentLineSegmentIntersection(maxX, maxY, bottomLeft.X, bottomLeft.Y, lAX, lAY, lBX, lBY, epsilon).Points);
            intersections.UnionWith(LineSegmentLineSegmentIntersection(bottomLeft.X, bottomLeft.Y, minX, minY, lAX, lAY, lBX, lBY, epsilon).Points);

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Count > 0)
            {
                result.State = IntersectionState.Intersection;
            }

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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
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
            {
                result.State |= IntersectionState.Intersection;
            }

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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
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
                    {
                        result.Points.Add(Lerp(lAX, lAY, lBX, lBY, u1));
                    }
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
                    {
                        result.Points.Add(Lerp(lAX, lAY, lBX, lBY, u1));
                    }

                    if (0 <= u2 && u2 <= 1)
                    {
                        result.Points.Add(Lerp(lAX, lAY, lBX, lBY, u2));
                    }
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentCircularArcIntersection(
            double lAX, double lAY, double lBX, double lBY,
            double cX, double cY, double r, double angle, double startAngle, double sweepAngle,
            double epsilon = Epsilon)
        {
            var result = new Intersection(IntersectionState.NoIntersection);

            // If the circle or line segment are empty, return no intersections.
            if ((r == 0d) || ((lAX == lBX) && (lAY == lBY)))
            {
                return result;
            }

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
            {
                result.State |= IntersectionState.Intersection;
            }

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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
        /// <param name="cosA">The cosine of the rotation angle.</param>
        /// <param name="sinA">The sine of the rotation angle.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
            {
                return result;
            }

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
            {
                result.State |= IntersectionState.Intersection;
            }

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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
        /// <param name="cosA">The cosine of the rotation angle.</param>
        /// <param name="sinA">The sine of the rotation angle.</param>
        /// <param name="startAngle"></param>
        /// <param name="sweepAngle"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2012/09/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentEllipticalArcIntersection(
            double x0, double y0,
            double x1, double y1,
            double cx, double cy, double rx, double ry, double cosA, double sinA, double startAngle, double sweepAngle,
            double epsilon = Epsilon)
        {
            // Initialize the resulting intersection structure.
            var result = new Intersection(IntersectionState.NoIntersection);

            // If the ellipse or line segment are empty, return no intersections.
            if ((sweepAngle == 0d) || (rx == 0d) || (ry == 0d) ||
                ((x0 == x1) && (y0 == y1)))
            {
                return result;
            }

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
                    {
                        result.AppendPoint(p);
                    }
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
                    {
                        result.AppendPoint(p);
                    }
                }

                if ((t2 >= 0d) && (t2 <= 1d))
                {
                    // Find the point.
                    var p = new Point2D(u0 + (u1 - u0) * t2 + cx, v0 + (v1 - v0) * t2 + cy);

                    // Find the determinant of the matrix representing the chord.
                    var determinant = (sx - p.X) * (ey - p.Y) - (ex - p.X) * (sy - p.Y);

                    // Add the point if it is on the sweep side of the chord.
                    if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle))
                    {
                        result.AppendPoint(p);
                    }
                }
            }

            // Return the intersections.
            if (result.Count > 0)
            {
                result.State |= IntersectionState.Intersection;
            }

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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentQuadraticBezierSegmentIntersection(
            double a1X, double a1Y, double a2X, double a2Y, double a3X, double a3Y,
            double b1X, double b1Y, double b2X, double b2Y, double b3X, double b3Y,
            double epsilon = Epsilon)
            => QuadraticBezierSegmentQuadraticBezierSegmentIntersection(
                Polynomial.Quadratic(a1X, a2X, a3X),
                Polynomial.Quadratic(a1Y, a2Y, a3Y),
                Polynomial.Quadratic(b1X, b2X, b3X),
                Polynomial.Quadratic(b1Y, b2Y, b3Y),
                epsilon);

        /// <summary>
        /// Find the intersection between two quadratic beziers.
        /// </summary>
        /// <param name="xCurveA">The set of Polynomial Bezier Coefficients of the x coordinates of the first Bezier curve.</param>
        /// <param name="yCurveA">The set of Polynomial Bezier Coefficients of the y coordinates of the first Bezier curve.</param>
        /// <param name="xCurveB">The set of Polynomial Bezier Coefficients of the x coordinates of the second Bezier curve.</param>
        /// <param name="yCurveB">The set of Polynomial Bezier Coefficients of the y coordinates of the second Bezier curve.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// A combination of the method ideas found at: https://www.particleincell.com/2013/cubic-line-intersection/
        /// and the intersections methods at: http://www.kevlindev.com/ also found at: https://github.com/thelonious/kld-intersections/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentQuadraticBezierSegmentIntersection(
            Polynomial xCurveA, Polynomial yCurveA,
            Polynomial xCurveB, Polynomial yCurveB,
            double epsilon = Epsilon)
        {
            // Initialize the intersection.
            var result = new Intersection(IntersectionState.NoIntersection);

            // Cross product of first coefficient of a and b.
            var v0 = xCurveA[0] * yCurveB[0] - yCurveA[0] * xCurveB[0];

            // Cross product of first coefficient of a and second coefficient of b.
            var v1 = xCurveA[0] * yCurveB[1] - xCurveB[1] * yCurveA[0];

            // Cross product of second coefficient of a and first coefficient of b.
            var v2 = xCurveA[1] * yCurveA[0] - yCurveA[1] * xCurveA[0];

            // Delta of third coefficients of curves b and a.
            var v3 = yCurveA[2] - yCurveB[2];

            // The difference of the first coefficients x and y of a times the deltas of the third coefficients of the x and y of curves a and b.
            var v4 = yCurveA[0] * (xCurveA[2] - xCurveB[2]) - xCurveA[0] * v3;

            var v5 = -yCurveA[1] * v2 + yCurveA[0] * v4;

            // Square of the second cross product.
            var v6 = v2 * v2;

            // Find the roots of the polynomial that represents the intersections.
            var roots = new Polynomial(
                // Square of first cross product.
                /* x⁴ */ v0 * v0,
                // Two times the first cross product times the second cross product.
                /* x³ */ 2 * v0 * v1,
                /* x² */ (-yCurveB[0] * v6 + yCurveA[0] * v1 * v1 + yCurveA[0] * v0 * v4 + v0 * v5) / yCurveA[0],
                /* x¹ */ (-yCurveB[1] * v6 + yCurveA[0] * v1 * v4 + v1 * v5) / yCurveA[0],
                /* c  */ (v3 * v6 + v4 * v5) / yCurveA[0]
            ).Trim().Roots();

            foreach (var s in roots)
            {
                // Interpolate the point at t of the root s on curve b.
                var point = new Point2D(
                    xCurveB[0] * s * s + xCurveB[1] * s + xCurveB[2],
                    yCurveB[0] * s * s + yCurveB[1] * s + yCurveB[2]);

                if (s >= 0 && s <= 1)
                {
                    // Look for intersections on curve a at the same location.
                    var xRoots = (xCurveA - point.X).Trim().Roots();
                    var yRoots = (yCurveA - point.Y).Trim().Roots();

                    if (xRoots.Length > 0 && yRoots.Length > 0)
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
            {
                result.State = IntersectionState.Intersection;
            }

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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentCubicBezierSegmentIntersection(
            double a1X, double a1Y, double a2X, double a2Y, double a3X, double a3Y,
            double b1X, double b1Y, double b2X, double b2Y, double b3X, double b3Y, double b4X, double b4Y,
            double epsilon = Epsilon)
            => QuadraticBezierSegmentCubicBezierSegmentIntersection(
                Polynomial.Quadratic(a1X, a2X, a3X),
                Polynomial.Quadratic(a1Y, a2Y, a3Y),
                Polynomial.Cubic(b1X, b2X, b3X, b4X),
                Polynomial.Cubic(b1Y, b2Y, b3Y, b4Y),
                epsilon);

        /// <summary>
        /// Find the intersection between a quadratic bezier and a cubic bezier.
        /// </summary>
        /// <param name="xCurveA">The set of Polynomial Bezier Coefficients of the x coordinates of the first Bezier curve.</param>
        /// <param name="yCurveA">The set of Polynomial Bezier Coefficients of the y coordinates of the first Bezier curve.</param>
        /// <param name="xCurveB">The set of Polynomial Bezier Coefficients of the x coordinates of the second Bezier curve.</param>
        /// <param name="yCurveB">The set of Polynomial Bezier Coefficients of the y coordinates of the second Bezier curve.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// This is a rewrite of a method ported from: http://www.kevlindev.com/ also found at: https://github.com/thelonious/kld-intersections/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentCubicBezierSegmentIntersection(
            Polynomial xCurveA, Polynomial yCurveA,
            Polynomial xCurveB, Polynomial yCurveB,
            double epsilon = Epsilon)
        {
            // Initialize intersection.
            var result = new Intersection(IntersectionState.NoIntersection);

            // ToDo: The tolerance is off by too much. Need to find the error.
            var tolerance = 4294967295 * epsilon; // 1e-4;

            var cAAx2 = xCurveA[0] * xCurveA[0];
            var cAAy2 = yCurveA[0] * yCurveA[0];

            var cABx2 = xCurveA[1] * xCurveA[1];
            var cABy2 = yCurveA[1] * yCurveA[1];

            var cACx2 = xCurveA[2] * xCurveA[2];
            var cACy2 = yCurveA[2] * yCurveA[2];

            var cBAx2 = xCurveB[0] * xCurveB[0];
            var cBAy2 = yCurveB[0] * yCurveB[0];

            var cBBx2 = xCurveB[1] * xCurveB[1];
            var cBBy2 = yCurveB[1] * yCurveB[1];

            var cBCx2 = xCurveB[2] * xCurveB[2];
            var cBCy2 = yCurveB[2] * yCurveB[2];

            var cBDx2 = xCurveB[3] * xCurveB[3];
            var cBDy2 = yCurveB[3] * yCurveB[3];

            // Find the roots of the polynomial that represents the intersections.
            var roots = new Polynomial(
                /* x⁶ */ -2 * xCurveA[0] * yCurveA[0] * xCurveB[0] * yCurveB[0] + cAAx2 * cBAy2 + cAAy2 * cBAx2,
                /* x⁵ */ -2 * xCurveA[0] * yCurveA[0] * xCurveB[1] * yCurveB[0] - 2 * xCurveA[0] * yCurveA[0] * yCurveB[1] * xCurveB[0] + 2 * cAAy2 * xCurveB[1] * xCurveB[0] + 2 * cAAx2 * yCurveB[1] * yCurveB[0],
                /* x⁴ */ -2 * xCurveA[0] * xCurveB[2] * yCurveA[0] * yCurveB[0] - 2 * xCurveA[0] * yCurveA[0] * yCurveB[2] * xCurveB[0] - 2 * xCurveA[0] * yCurveA[0] * xCurveB[1] * yCurveB[1] + 2 * xCurveB[2] * cAAy2 * xCurveB[0] + cAAy2 * cBBx2 + cAAx2 * (2 * yCurveB[2] * yCurveB[0] + cBBy2),
                /* x³ */ 2 * xCurveA[2] * xCurveA[0] * yCurveA[0] * yCurveB[0] + 2 * yCurveA[2] * xCurveA[0] * yCurveA[0] * xCurveB[0] + xCurveA[1] * yCurveA[1] * xCurveA[0] * yCurveB[0] + xCurveA[1] * yCurveA[1] * yCurveA[0] * xCurveB[0] - 2 * xCurveB[3] * xCurveA[0] * yCurveA[0] * yCurveB[0] - 2 * xCurveA[0] * yCurveB[3] * yCurveA[0] * xCurveB[0] - 2 * xCurveA[0] * xCurveB[2] * yCurveA[0] * yCurveB[1] - 2 * xCurveA[0] * yCurveA[0] * yCurveB[2] * xCurveB[1] - 2 * xCurveA[2] * cAAy2 * xCurveB[0] - 2 * yCurveA[2] * cAAx2 * yCurveB[0] + 2 * xCurveB[3] * cAAy2 * xCurveB[0] + 2 * xCurveB[2] * cAAy2 * xCurveB[1] - cABy2 * xCurveA[0] * xCurveB[0] - cABx2 * yCurveA[0] * yCurveB[0] + cAAx2 * (2 * yCurveB[3] * yCurveB[0] + 2 * yCurveB[2] * yCurveB[1]),
                /* x² */ 2 * xCurveA[2] * xCurveA[0] * yCurveA[0] * yCurveB[1] + 2 * yCurveA[2] * xCurveA[0] * yCurveA[0] * xCurveB[1] + xCurveA[1] * yCurveA[1] * xCurveA[0] * yCurveB[1] + xCurveA[1] * yCurveA[1] * yCurveA[0] * xCurveB[1] - 2 * xCurveB[3] * xCurveA[0] * yCurveA[0] * yCurveB[1] - 2 * xCurveA[0] * yCurveB[3] * yCurveA[0] * xCurveB[1] - 2 * xCurveA[0] * xCurveB[2] * yCurveA[0] * yCurveB[2] - 2 * xCurveA[2] * cAAy2 * xCurveB[1] - 2 * yCurveA[2] * cAAx2 * yCurveB[1] + 2 * xCurveB[3] * cAAy2 * xCurveB[1] - cABy2 * xCurveA[0] * xCurveB[1] - cABx2 * yCurveA[0] * yCurveB[1] + cBCx2 * cAAy2 + cAAx2 * (2 * yCurveB[3] * yCurveB[1] + cBCy2),
                /* x¹ */ 2 * xCurveA[2] * xCurveA[0] * yCurveA[0] * yCurveB[2] + 2 * yCurveA[2] * xCurveA[0] * xCurveB[2] * yCurveA[0] + xCurveA[1] * yCurveA[1] * xCurveA[0] * yCurveB[2] + xCurveA[1] * yCurveA[1] * xCurveB[2] * yCurveA[0] - 2 * xCurveB[3] * xCurveA[0] * yCurveA[0] * yCurveB[2] - 2 * xCurveA[0] * yCurveB[3] * xCurveB[2] * yCurveA[0] - 2 * xCurveA[2] * xCurveB[2] * cAAy2 - 2 * yCurveA[2] * cAAx2 * yCurveB[2] + 2 * xCurveB[3] * xCurveB[2] * cAAy2 - cABy2 * xCurveA[0] * xCurveB[2] - cABx2 * yCurveA[0] * yCurveB[2] + 2 * cAAx2 * yCurveB[3] * yCurveB[2],
                /* c  */ -2 * xCurveA[2] * yCurveA[2] * xCurveA[0] * yCurveA[0] - xCurveA[2] * xCurveA[1] * yCurveA[1] * yCurveA[0] - yCurveA[2] * xCurveA[1] * yCurveA[1] * xCurveA[0] + 2 * xCurveA[2] * xCurveA[0] * yCurveB[3] * yCurveA[0] + 2 * yCurveA[2] * xCurveB[3] * xCurveA[0] * yCurveA[0] + xCurveA[1] * xCurveB[3] * yCurveA[1] * yCurveA[0] + xCurveA[1] * yCurveA[1] * xCurveA[0] * yCurveB[3] - 2 * xCurveB[3] * xCurveA[0] * yCurveB[3] * yCurveA[0] - 2 * xCurveA[2] * xCurveB[3] * cAAy2 + xCurveA[2] * cABy2 * xCurveA[0] + yCurveA[2] * cABx2 * yCurveA[0] - 2 * yCurveA[2] * cAAx2 * yCurveB[3] - xCurveB[3] * cABy2 * xCurveA[0] - cABx2 * yCurveB[3] * yCurveA[0] + cACx2 * cAAy2 + cACy2 * cAAx2 + cBDx2 * cAAy2 + cAAx2 * cBDy2
            ).Trim().RootsInInterval();

            foreach (var s in roots)
            {
                var point = new Point2D(
                   xCurveB[0] * s * s * s + xCurveB[1] * s * s + xCurveB[2] * s + xCurveB[3],
                   yCurveB[0] * s * s * s + yCurveB[1] * s * s + yCurveB[2] * s + yCurveB[3]);

                var xRoots = (xCurveA - point.X).Trim().Roots(epsilon);
                var yRoots = (yCurveA - point.Y).Trim().Roots();

                if (xRoots.Length > 0 && yRoots.Length > 0)
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
            {
                result.State = IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a quadratic bezier and a polygon contour.
        /// </summary>
        /// <param name="b1X"></param>
        /// <param name="b1Y"></param>
        /// <param name="b2X"></param>
        /// <param name="b2Y"></param>
        /// <param name="b3X"></param>
        /// <param name="b3Y"></param>
        /// <param name="points"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentPolygonContourIntersection(
            double b1X, double b1Y, double b2X, double b2Y, double b3X, double b3Y,
            List<Point2D> points,
            double epsilon = Epsilon)
            => QuadraticBezierSegmentPolygonContourIntersection(
                Polynomial.Quadratic(b1X, b2X, b3X),
                Polynomial.Quadratic(b1Y, b2Y, b3Y),
                points,
                epsilon);

        /// <summary>
        /// Find the intersection between a quadratic bezier and a polygon contour.
        /// </summary>
        /// <param name="xCurve"></param>
        /// <param name="yCurve"></param>
        /// <param name="points"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentPolygonContourIntersection(
            Polynomial xCurve, Polynomial yCurve,
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

                intersections.UnionWith(LineSegmentQuadraticBezierSegmentIntersection(a1.X, a1.Y, a2.X, a2.Y, xCurve, yCurve, epsilon).Points);

                a1 = a2;
            }

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Count > 0)
            {
                result.State |= IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a quadratic bezier and a triangle.
        /// </summary>
        /// <param name="xCurve"></param>
        /// <param name="yCurve"></param>
        /// <param name="t1X"></param>
        /// <param name="t1Y"></param>
        /// <param name="t2X"></param>
        /// <param name="t2Y"></param>
        /// <param name="t3X"></param>
        /// <param name="t3Y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentTriangleIntersection(
            Polynomial xCurve, Polynomial yCurve,
            double t1X, double t1Y, double t2X, double t2Y, double t3X, double t3Y,
            double epsilon = Epsilon)
        {
            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();

            intersections.UnionWith(LineSegmentQuadraticBezierSegmentIntersection(t1X, t1Y, t2X, t2Y, xCurve, yCurve, epsilon).Points);
            intersections.UnionWith(LineSegmentQuadraticBezierSegmentIntersection(t2X, t2Y, t3X, t3Y, xCurve, yCurve, epsilon).Points);
            intersections.UnionWith(LineSegmentQuadraticBezierSegmentIntersection(t3X, t3Y, t1X, t1Y, xCurve, yCurve, epsilon).Points);

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionState.Intersection;
            }

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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentRectangleIntersection(
            double p1X, double p1Y, double p2X, double p2Y, double p3X, double p3Y,
            double r1X, double r1Y, double r2X, double r2Y,
            double epsilon = Epsilon)
            => QuadraticBezierSegmentRectangleIntersection(
                Polynomial.Quadratic(p1X, p2X, p3X),
                Polynomial.Quadratic(p1Y, p2Y, p3Y),
                r1X, r1Y, r2X, r2Y,
                epsilon);

        /// <summary>
        /// Find the intersection between a quadratic bezier and a rectangle.
        /// </summary>
        /// <param name="xCurve"></param>
        /// <param name="yCurve"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentRectangleIntersection(
            Polynomial xCurve, Polynomial yCurve,
            double r1X, double r1Y, double r2X, double r2Y,
            double epsilon = Epsilon)
        {
            var (minX, minY) = MinPoint(r1X, r1Y, r2X, r2Y);
            var (maxX, maxY) = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(maxX, minY);
            var bottomLeft = new Point2D(minX, maxY);

            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            intersections.UnionWith(LineSegmentQuadraticBezierSegmentIntersection(minX, minY, topRight.X, topRight.Y, xCurve, yCurve, epsilon).Points);
            intersections.UnionWith(LineSegmentQuadraticBezierSegmentIntersection(topRight.X, topRight.Y, maxX, maxY, xCurve, yCurve, epsilon).Points);
            intersections.UnionWith(LineSegmentQuadraticBezierSegmentIntersection(maxX, maxY, bottomLeft.X, bottomLeft.Y, xCurve, yCurve, epsilon).Points);
            intersections.UnionWith(LineSegmentQuadraticBezierSegmentIntersection(bottomLeft.X, bottomLeft.Y, minX, minY, xCurve, yCurve, epsilon).Points);

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the point of self intersection of a cubic bezier curve, if the cubic bezier curve has self intersection.
        /// </summary>
        /// <param name="x0">The x-component of the starting point.</param>
        /// <param name="y0">The y-component of the starting point.</param>
        /// <param name="x1">The x-component of the first tangent handle.</param>
        /// <param name="y1">The y-component of the first tangent handle.</param>
        /// <param name="x2">The x-component of the second tangent handle.</param>
        /// <param name="y2">The y-component of the second tangent handle.</param>
        /// <param name="x3">The x-component of the ending point.</param>
        /// <param name="y3">The y-component of the ending point.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierSegmentSelfIntersection(
            double x0, double y0, double x1, double y1, double x2, double y2, double x3, double y3,
            double epsilon = Epsilon)
            => CubicBezierSegmentSelfIntersection(
                Polynomial.Cubic(x0, x1, x2, x3),
                Polynomial.Cubic(y0, y1, y2, y3));

        /// <summary>
        /// Find the point of self intersection of a cubic bezier curve, if the cubic bezier curve has self intersection.
        /// </summary>
        /// <param name="xCurve"></param>
        /// <param name="yCurve"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// https://groups.google.com/d/msg/comp.graphics.algorithms/SRm97nRWlw4/R1Rn38ep8n0J
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierSegmentSelfIntersection(
            Polynomial xCurve, Polynomial yCurve,
            double epsilon = Epsilon)
        {
            var result = new Intersection(IntersectionState.NoIntersection);

            // Not sure why the difference between the two supposedly same points at different values of t can be so high. It seems to be a lot for floating point rounding. So far it only seems to happen at orthogonal cases.
            var tolerence = 98838707421d * epsilon; // 0.56183300455876406

            (var a, var b) = (xCurve[0] == 0d) ? (xCurve[1], xCurve[2]) : (xCurve[1] / xCurve[0], xCurve[2] / xCurve[0]);
            (var p, var q) = (yCurve[0] == 0d) ? (yCurve[1], yCurve[2]) : (yCurve[1] / yCurve[0], yCurve[2] / yCurve[0]);

            if (a == p || q == b)
            {
                return result;
            }

            var k = (q - b) / (a - p);


            var roots = new Polynomial(
                2,
                -3 * k,
                3 * k * k + 2 * k * a + 2 * b,
                -k * k * k - a * k * k - b * k
                ).Trim().Roots();

            // ToDo: Figure out edge case. When all nodes are linear, even if there should be a flat loop, there is only one root. The locus of points overlap three times for a little ways, and possibly twice past an edge.
            if (roots.Length != 3)
            {
                return result;
            }

            if (roots[0] >= 0.0 && roots[0] <= 1.0 && roots[2] >= 0.0 && roots[2] <= 1.0)
            {
                // Locate the points that overlap.
                var points = new List<Point2D>();

                // This loop is for the general case. For cubic curves, one should just be able to grab the point at root[0] or root[2], or lerp halfway between them.
                foreach (var s in roots)
                {
                    var point = new Point2D(
                        xCurve[0] * s * s * s + xCurve[1] * s * s + xCurve[2] * s + xCurve[3],
                        yCurve[0] * s * s * s + yCurve[1] * s * s + yCurve[2] * s + yCurve[3]);

                    for (var i = 0; i < points.Count; i++)
                    {
                        var r = points[i];
                        if (Abs(point.X - r.X) < tolerence && Abs(point.Y - r.Y) < tolerence)
                        {
                            // ToDo: Should the real resulting point be a lerp halfway between the two points to counteract floating point rounding?
                            result.Points.Add(point);
                            break;
                        }
                    }

                    points.Add(point);
                }
            }

            if (result.Count > 0)
            {
                result.State = IntersectionState.Intersection;
            }

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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierSegmentCubicBezierSegmentIntersection(
            double a1X, double a1Y, double a2X, double a2Y, double a3X, double a3Y, double a4X, double a4Y,
            double b1X, double b1Y, double b2X, double b2Y, double b3X, double b3Y, double b4X, double b4Y,
            double epsilon = Epsilon)
            => CubicBezierSegmentCubicBezierSegmentIntersection(
                Polynomial.Cubic(a1X, a2X, a3X, a4X),
                Polynomial.Cubic(a1Y, a2Y, a3Y, a4Y),
                Polynomial.Cubic(b1X, b2X, b3X, b4X),
                Polynomial.Cubic(b1Y, b2Y, b3Y, b4Y),
                epsilon);

        /// <summary>
        /// Find the intersection between two cubic beziers.
        /// </summary>
        /// <param name="xCurveA">The set of Polynomial Bezier Coefficients of the x coordinates of the first Bezier curve.</param>
        /// <param name="yCurveA">The set of Polynomial Bezier Coefficients of the y coordinates of the first Bezier curve.</param>
        /// <param name="xCurveB">The set of Polynomial Bezier Coefficients of the x coordinates of the second Bezier curve.</param>
        /// <param name="yCurveB">The set of Polynomial Bezier Coefficients of the y coordinates of the second Bezier curve.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// This is a rewrite of a method ported from: http://www.kevlindev.com/ also found at: https://github.com/thelonious/kld-intersections/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierSegmentCubicBezierSegmentIntersection(
            Polynomial xCurveA, Polynomial yCurveA,
            Polynomial xCurveB, Polynomial yCurveB,
            double epsilon = Epsilon)
        {
            // Initialize the intersection.
            var result = new Intersection(IntersectionState.NoIntersection);

            // ToDo: The tolerance is off by too much. Need to find the error.
            var tolerance = 4194303 * epsilon;

            var c10x2 = xCurveA[3] * xCurveA[3];
            var c10x3 = xCurveA[3] * xCurveA[3] * xCurveA[3];
            var c10y2 = yCurveA[3] * yCurveA[3];
            var c10y3 = yCurveA[3] * yCurveA[3] * yCurveA[3];

            var c11x2 = xCurveA[2] * xCurveA[2];
            var c11x3 = xCurveA[2] * xCurveA[2] * xCurveA[2];
            var c11y2 = yCurveA[2] * yCurveA[2];
            var c11y3 = yCurveA[2] * yCurveA[2] * yCurveA[2];

            var c12x2 = xCurveA[1] * xCurveA[1];
            var c12x3 = xCurveA[1] * xCurveA[1] * xCurveA[1];
            var c12y2 = yCurveA[1] * yCurveA[1];
            var c12y3 = yCurveA[1] * yCurveA[1] * yCurveA[1];

            var c13x2 = xCurveA[0] * xCurveA[0];
            var c13x3 = xCurveA[0] * xCurveA[0] * xCurveA[0];
            var c13y2 = yCurveA[0] * yCurveA[0];
            var c13y3 = yCurveA[0] * yCurveA[0] * yCurveA[0];

            var c20x2 = xCurveB[3] * xCurveB[3];
            var c20x3 = xCurveB[3] * xCurveB[3] * xCurveB[3];
            var c20y2 = yCurveB[3] * yCurveB[3];
            var c20y3 = yCurveB[3] * yCurveB[3] * yCurveB[3];

            var c21x2 = xCurveB[2] * xCurveB[2];
            var c21x3 = xCurveB[2] * xCurveB[2] * xCurveB[2];
            var c21y2 = yCurveB[2] * yCurveB[2];

            var c22x2 = xCurveB[1] * xCurveB[1];
            var c22x3 = xCurveB[1] * xCurveB[1] * xCurveB[1];
            var c22y2 = yCurveB[1] * yCurveB[1];

            var c23x2 = xCurveB[0] * xCurveB[0];
            var c23x3 = xCurveB[0] * xCurveB[0] * xCurveB[0];
            var c23y2 = yCurveB[0] * yCurveB[0];
            var c23y3 = yCurveB[0] * yCurveB[0] * yCurveB[0];

            // Find the roots of the polynomial that represents the intersections.
            var roots = new Polynomial(
                /* x⁹ */ -c13x3 * c23y3 + c13y3 * c23x3 - 3 * xCurveA[0] * c13y2 * c23x2 * yCurveB[0] + 3 * c13x2 * yCurveA[0] * xCurveB[0] * c23y2,
                /* x⁸ */ -6 * xCurveA[0] * xCurveB[1] * c13y2 * xCurveB[0] * yCurveB[0] + 6 * c13x2 * yCurveA[0] * yCurveB[1] * xCurveB[0] * yCurveB[0] + 3 * xCurveB[1] * c13y3 * c23x2 - 3 * c13x3 * yCurveB[1] * c23y2 - 3 * xCurveA[0] * c13y2 * yCurveB[1] * c23x2 + 3 * c13x2 * xCurveB[1] * yCurveA[0] * c23y2,
                /* x⁷ */ -6 * xCurveB[2] * xCurveA[0] * c13y2 * xCurveB[0] * yCurveB[0] - 6 * xCurveA[0] * xCurveB[1] * c13y2 * yCurveB[1] * xCurveB[0] + 6 * c13x2 * xCurveB[1] * yCurveA[0] * yCurveB[1] * yCurveB[0] + 3 * xCurveB[2] * c13y3 * c23x2 + 3 * c22x2 * c13y3 * xCurveB[0] + 3 * xCurveB[2] * c13x2 * yCurveA[0] * c23y2 - 3 * xCurveA[0] * yCurveB[2] * c13y2 * c23x2 - 3 * xCurveA[0] * c22x2 * c13y2 * yCurveB[0] + c13x2 * yCurveA[0] * xCurveB[0] * (6 * yCurveB[2] * yCurveB[0] + 3 * c22y2) + c13x3 * (-yCurveB[2] * c23y2 - 2 * c22y2 * yCurveB[0] - yCurveB[0] * (2 * yCurveB[2] * yCurveB[0] + c22y2)),
                /* x⁶ */ xCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] * xCurveB[0] * yCurveB[0] - yCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * xCurveB[0] * yCurveB[0] + 6 * xCurveB[2] * xCurveB[1] * c13y3 * xCurveB[0] + 3 * xCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * c23y2 + 6 * xCurveA[3] * xCurveA[0] * c13y2 * xCurveB[0] * yCurveB[0] - 3 * xCurveA[2] * xCurveA[1] * c13y2 * xCurveB[0] * yCurveB[0] - 3 * yCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] * c23x2 - 6 * yCurveA[3] * c13x2 * yCurveA[0] * xCurveB[0] * yCurveB[0] - 6 * xCurveB[3] * xCurveA[0] * c13y2 * xCurveB[0] * yCurveB[0] + 3 * yCurveA[2] * yCurveA[1] * c13x2 * xCurveB[0] * yCurveB[0] - 2 * xCurveA[1] * c12y2 * xCurveA[0] * xCurveB[0] * yCurveB[0] - 6 * xCurveB[2] * xCurveA[0] * xCurveB[1] * c13y2 * yCurveB[0] - 6 * xCurveB[2] * xCurveA[0] * c13y2 * yCurveB[1] * xCurveB[0] - 6 * xCurveA[0] * yCurveB[2] * xCurveB[1] * c13y2 * xCurveB[0] + 6 * xCurveB[2] * c13x2 * yCurveA[0] * yCurveB[1] * yCurveB[0] + 2 * c12x2 * yCurveA[1] * yCurveA[0] * xCurveB[0] * yCurveB[0] + c22x3 * c13y3 - 3 * xCurveA[3] * c13y3 * c23x2 + 3 * yCurveA[3] * c13x3 * c23y2 + 3 * xCurveB[3] * c13y3 * c23x2 + c12y3 * xCurveA[0] * c23x2 - c12x3 * yCurveA[0] * c23y2 - 3 * xCurveA[3] * c13x2 * yCurveA[0] * c23y2 + 3 * yCurveA[3] * xCurveA[0] * c13y2 * c23x2 - 2 * xCurveA[2] * yCurveA[1] * c13x2 * c23y2 + xCurveA[2] * yCurveA[1] * c13y2 * c23x2 - yCurveA[2] * xCurveA[1] * c13x2 * c23y2 + 2 * yCurveA[2] * xCurveA[1] * c13y2 * c23x2 + 3 * xCurveB[3] * c13x2 * yCurveA[0] * c23y2 - xCurveA[1] * c12y2 * yCurveA[0] * c23x2 - 3 * yCurveB[3] * xCurveA[0] * c13y2 * c23x2 + c12x2 * yCurveA[1] * xCurveA[0] * c23y2 - 3 * xCurveA[0] * c22x2 * c13y2 * yCurveB[1] + c13x2 * yCurveA[0] * xCurveB[0] * (6 * yCurveB[3] * yCurveB[0] + 6 * yCurveB[2] * yCurveB[1]) + c13x2 * xCurveB[1] * yCurveA[0] * (6 * yCurveB[2] * yCurveB[0] + 3 * c22y2) + c13x3 * (-2 * yCurveB[2] * yCurveB[1] * yCurveB[0] - yCurveB[3] * c23y2 - yCurveB[1] * (2 * yCurveB[2] * yCurveB[0] + c22y2) - yCurveB[0] * (2 * yCurveB[3] * yCurveB[0] + 2 * yCurveB[2] * yCurveB[1])),
                /* x⁵ */ 6 * xCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[1] * yCurveB[0] + xCurveA[2] * yCurveA[1] * xCurveA[0] * xCurveB[1] * yCurveA[0] * yCurveB[0] + xCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[1] * xCurveB[0] - yCurveA[2] * xCurveA[1] * xCurveA[0] * xCurveB[1] * yCurveA[0] * yCurveB[0] - yCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[1] * xCurveB[0] - 6 * yCurveA[2] * yCurveA[1] * xCurveA[0] * xCurveB[1] * yCurveA[0] * xCurveB[0] - 6 * xCurveA[3] * xCurveB[1] * c13y3 * xCurveB[0] + 6 * xCurveB[3] * xCurveB[1] * c13y3 * xCurveB[0] + 6 * yCurveA[3] * c13x3 * yCurveB[1] * yCurveB[0] + 2 * c12y3 * xCurveA[0] * xCurveB[1] * xCurveB[0] - 2 * c12x3 * yCurveA[0] * yCurveB[1] * yCurveB[0] + 6 * xCurveA[3] * xCurveA[0] * xCurveB[1] * c13y2 * yCurveB[0] + 6 * xCurveA[3] * xCurveA[0] * c13y2 * yCurveB[1] * xCurveB[0] + 6 * yCurveA[3] * xCurveA[0] * xCurveB[1] * c13y2 * xCurveB[0] - 3 * xCurveA[2] * xCurveA[1] * xCurveB[1] * c13y2 * yCurveB[0] - 3 * xCurveA[2] * xCurveA[1] * c13y2 * yCurveB[1] * xCurveB[0] + 2 * xCurveA[2] * yCurveA[1] * xCurveB[1] * c13y2 * xCurveB[0] + 4 * yCurveA[2] * xCurveA[1] * xCurveB[1] * c13y2 * xCurveB[0] - 6 * xCurveA[3] * c13x2 * yCurveA[0] * yCurveB[1] * yCurveB[0] - 6 * yCurveA[3] * c13x2 * xCurveB[1] * yCurveA[0] * yCurveB[0] - 6 * yCurveA[3] * c13x2 * yCurveA[0] * yCurveB[1] * xCurveB[0] - 4 * xCurveA[2] * yCurveA[1] * c13x2 * yCurveB[1] * yCurveB[0] - 6 * xCurveB[3] * xCurveA[0] * xCurveB[1] * c13y2 * yCurveB[0] - 6 * xCurveB[3] * xCurveA[0] * c13y2 * yCurveB[1] * xCurveB[0] - 2 * yCurveA[2] * xCurveA[1] * c13x2 * yCurveB[1] * yCurveB[0] + 3 * yCurveA[2] * yCurveA[1] * c13x2 * xCurveB[1] * yCurveB[0] + 3 * yCurveA[2] * yCurveA[1] * c13x2 * yCurveB[1] * xCurveB[0] - 2 * xCurveA[1] * c12y2 * xCurveA[0] * xCurveB[1] * yCurveB[0] - 2 * xCurveA[1] * c12y2 * xCurveA[0] * yCurveB[1] * xCurveB[0] - 2 * xCurveA[1] * c12y2 * xCurveB[1] * yCurveA[0] * xCurveB[0] - 6 * yCurveB[3] * xCurveA[0] * xCurveB[1] * c13y2 * xCurveB[0] - 6 * xCurveB[2] * xCurveA[0] * yCurveB[2] * c13y2 * xCurveB[0] - 6 * xCurveB[2] * xCurveA[0] * xCurveB[1] * c13y2 * yCurveB[1] + 6 * xCurveB[3] * c13x2 * yCurveA[0] * yCurveB[1] * yCurveB[0] + 2 * c12x2 * yCurveA[1] * xCurveA[0] * yCurveB[1] * yCurveB[0] + 2 * c12x2 * yCurveA[1] * xCurveB[1] * yCurveA[0] * yCurveB[0] + 2 * c12x2 * yCurveA[1] * yCurveA[0] * yCurveB[1] * xCurveB[0] + 3 * xCurveB[2] * c22x2 * c13y3 + 3 * c21x2 * c13y3 * xCurveB[0] - 3 * xCurveA[0] * yCurveB[2] * c22x2 * c13y2 - 3 * c21x2 * xCurveA[0] * c13y2 * yCurveB[0] + c13x2 * xCurveB[1] * yCurveA[0] * (6 * yCurveB[3] * yCurveB[0] + 6 * yCurveB[2] * yCurveB[1]) + c13x2 * yCurveA[0] * xCurveB[0] * (6 * yCurveB[3] * yCurveB[1] + 3 * c21y2) + xCurveB[2] * c13x2 * yCurveA[0] * (6 * yCurveB[2] * yCurveB[0] + 3 * c22y2) + c13x3 * (-2 * yCurveB[3] * yCurveB[1] * yCurveB[0] - yCurveB[0] * (2 * yCurveB[3] * yCurveB[1] + c21y2) - yCurveB[2] * (2 * yCurveB[2] * yCurveB[0] + c22y2) - yCurveB[1] * (2 * yCurveB[3] * yCurveB[0] + 2 * yCurveB[2] * yCurveB[1])),
                /* x⁴ */ xCurveA[2] * xCurveB[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[0] + xCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveB[2] * yCurveA[0] * xCurveB[0] + xCurveA[2] * yCurveA[1] * xCurveA[0] * xCurveB[1] * yCurveA[0] * yCurveB[1] - yCurveA[2] * xCurveA[1] * xCurveB[2] * xCurveA[0] * yCurveA[0] * yCurveB[0] - yCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveB[2] * yCurveA[0] * xCurveB[0] - yCurveA[2] * xCurveA[1] * xCurveA[0] * xCurveB[1] * yCurveA[0] * yCurveB[1] - 6 * yCurveA[2] * xCurveB[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] * xCurveB[0] - 6 * xCurveA[3] * xCurveB[2] * c13y3 * xCurveB[0] + 6 * xCurveB[3] * xCurveB[2] * c13y3 * xCurveB[0] + 2 * xCurveB[2] * c12y3 * xCurveA[0] * xCurveB[0] + 6 * xCurveA[3] * xCurveB[2] * xCurveA[0] * c13y2 * yCurveB[0] + 6 * xCurveA[3] * xCurveA[0] * yCurveB[2] * c13y2 * xCurveB[0] + 6 * xCurveA[3] * xCurveA[0] * xCurveB[1] * c13y2 * yCurveB[1] + 6 * yCurveA[3] * xCurveB[2] * xCurveA[0] * c13y2 * xCurveB[0] - 3 * xCurveA[2] * xCurveA[1] * xCurveB[2] * c13y2 * yCurveB[0] - 3 * xCurveA[2] * xCurveA[1] * yCurveB[2] * c13y2 * xCurveB[0] - 3 * xCurveA[2] * xCurveA[1] * xCurveB[1] * c13y2 * yCurveB[1] + 2 * xCurveA[2] * xCurveB[2] * yCurveA[1] * c13y2 * xCurveB[0] + 4 * yCurveA[2] * xCurveA[1] * xCurveB[2] * c13y2 * xCurveB[0] - 6 * yCurveA[3] * xCurveB[2] * c13x2 * yCurveA[0] * yCurveB[0] - 6 * yCurveA[3] * c13x2 * yCurveB[2] * yCurveA[0] * xCurveB[0] - 6 * yCurveA[3] * c13x2 * xCurveB[1] * yCurveA[0] * yCurveB[1] - 6 * xCurveB[3] * xCurveB[2] * xCurveA[0] * c13y2 * yCurveB[0] - 6 * xCurveB[3] * xCurveA[0] * yCurveB[2] * c13y2 * xCurveB[0] - 6 * xCurveB[3] * xCurveA[0] * xCurveB[1] * c13y2 * yCurveB[1] + 3 * yCurveA[2] * xCurveB[2] * yCurveA[1] * c13x2 * yCurveB[0] - 3 * yCurveA[2] * yCurveA[1] * xCurveA[0] * c22x2 * yCurveA[0] + 3 * yCurveA[2] * yCurveA[1] * c13x2 * yCurveB[2] * xCurveB[0] + 3 * yCurveA[2] * yCurveA[1] * c13x2 * xCurveB[1] * yCurveB[1] - 2 * xCurveA[1] * xCurveB[2] * c12y2 * xCurveA[0] * yCurveB[0] - 2 * xCurveA[1] * xCurveB[2] * c12y2 * yCurveA[0] * xCurveB[0] - 2 * xCurveA[1] * c12y2 * xCurveA[0] * yCurveB[2] * xCurveB[0] - 2 * xCurveA[1] * c12y2 * xCurveA[0] * xCurveB[1] * yCurveB[1] - 6 * yCurveB[3] * xCurveB[2] * xCurveA[0] * c13y2 * xCurveB[0] - 6 * xCurveB[2] * xCurveA[0] * yCurveB[2] * xCurveB[1] * c13y2 + 6 * yCurveB[3] * c13x2 * yCurveB[2] * yCurveA[0] * xCurveB[0] + 2 * c12x2 * xCurveB[2] * yCurveA[1] * yCurveA[0] * yCurveB[0] + 2 * c12x2 * yCurveA[1] * yCurveB[2] * yCurveA[0] * xCurveB[0] + 2 * c12x2 * yCurveA[1] * xCurveB[1] * yCurveA[0] * yCurveB[1] - 3 * xCurveA[3] * c22x2 * c13y3 + 3 * xCurveB[3] * c22x2 * c13y3 + 3 * c21x2 * xCurveB[1] * c13y3 + c12y3 * xCurveA[0] * c22x2 + 3 * yCurveA[3] * xCurveA[0] * c22x2 * c13y2 + xCurveA[2] * yCurveA[1] * c22x2 * c13y2 + 2 * yCurveA[2] * xCurveA[1] * c22x2 * c13y2 - xCurveA[1] * c12y2 * c22x2 * yCurveA[0] - 3 * yCurveB[3] * xCurveA[0] * c22x2 * c13y2 - 3 * c21x2 * xCurveA[0] * c13y2 * yCurveB[1] + c12x2 * yCurveA[1] * xCurveA[0] * (2 * yCurveB[2] * yCurveB[0] + c22y2) + xCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * (6 * yCurveB[2] * yCurveB[0] + 3 * c22y2) + xCurveB[2] * c13x2 * yCurveA[0] * (6 * yCurveB[3] * yCurveB[0] + 6 * yCurveB[2] * yCurveB[1]) + c12x3 * yCurveA[0] * (-2 * yCurveB[2] * yCurveB[0] - c22y2) + yCurveA[3] * c13x3 * (6 * yCurveB[2] * yCurveB[0] + 3 * c22y2) + yCurveA[2] * xCurveA[1] * c13x2 * (-2 * yCurveB[2] * yCurveB[0] - c22y2) + xCurveA[2] * yCurveA[1] * c13x2 * (-4 * yCurveB[2] * yCurveB[0] - 2 * c22y2) + xCurveA[3] * c13x2 * yCurveA[0] * (-6 * yCurveB[2] * yCurveB[0] - 3 * c22y2) + c13x2 * xCurveB[1] * yCurveA[0] * (6 * yCurveB[3] * yCurveB[1] + 3 * c21y2) + xCurveB[3] * c13x2 * yCurveA[0] * (6 * yCurveB[2] * yCurveB[0] + 3 * c22y2) + c13x3 * (-2 * yCurveB[3] * yCurveB[2] * yCurveB[0] - yCurveB[1] * (2 * yCurveB[3] * yCurveB[1] + c21y2) - yCurveB[3] * (2 * yCurveB[2] * yCurveB[0] + c22y2) - yCurveB[2] * (2 * yCurveB[3] * yCurveB[0] + 2 * yCurveB[2] * yCurveB[1])),
                /* x³ */ -xCurveA[3] * xCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[0] + xCurveA[3] * yCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[0] + 6 * xCurveA[3] * yCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] * xCurveB[0] - 6 * yCurveA[3] * xCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[0] - yCurveA[3] * xCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] * xCurveB[0] + yCurveA[3] * yCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * xCurveB[0] + xCurveA[2] * yCurveA[2] * xCurveA[1] * yCurveA[1] * xCurveA[0] * yCurveB[0] - xCurveA[2] * yCurveA[2] * xCurveA[1] * yCurveA[1] * yCurveA[0] * xCurveB[0] + xCurveA[2] * xCurveB[3] * yCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[0] + xCurveA[2] * yCurveB[3] * yCurveA[1] * xCurveA[0] * yCurveA[0] * xCurveB[0] + xCurveA[2] * xCurveB[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[1] + xCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveB[2] * xCurveB[1] * yCurveA[0] - xCurveB[3] * yCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[0] - 6 * xCurveB[3] * yCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] * xCurveB[0] - yCurveA[2] * xCurveA[1] * yCurveB[3] * xCurveA[0] * yCurveA[0] * xCurveB[0] - yCurveA[2] * xCurveA[1] * xCurveB[2] * xCurveA[0] * yCurveA[0] * yCurveB[1] - yCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveB[2] * xCurveB[1] * yCurveA[0] - 6 * yCurveA[2] * xCurveB[2] * yCurveA[1] * xCurveA[0] * xCurveB[1] * yCurveA[0] - 6 * xCurveA[3] * xCurveB[3] * c13y3 * xCurveB[0] - 6 * xCurveA[3] * xCurveB[2] * xCurveB[1] * c13y3 - 2 * xCurveA[3] * c12y3 * xCurveA[0] * xCurveB[0] + 6 * xCurveB[3] * xCurveB[2] * xCurveB[1] * c13y3 + 2 * xCurveB[3] * c12y3 * xCurveA[0] * xCurveB[0] + 2 * xCurveB[2] * c12y3 * xCurveA[0] * xCurveB[1] + 2 * yCurveA[3] * c12x3 * yCurveA[0] * yCurveB[0] - 6 * xCurveA[3] * yCurveA[3] * xCurveA[0] * c13y2 * xCurveB[0] + 3 * xCurveA[3] * xCurveA[2] * xCurveA[1] * c13y2 * yCurveB[0] - 2 * xCurveA[3] * xCurveA[2] * yCurveA[1] * c13y2 * xCurveB[0] - 4 * xCurveA[3] * yCurveA[2] * xCurveA[1] * c13y2 * xCurveB[0] + 3 * yCurveA[3] * xCurveA[2] * xCurveA[1] * c13y2 * xCurveB[0] + 6 * xCurveA[3] * yCurveA[3] * c13x2 * yCurveA[0] * yCurveB[0] + 6 * xCurveA[3] * xCurveB[3] * xCurveA[0] * c13y2 * yCurveB[0] - 3 * xCurveA[3] * yCurveA[2] * yCurveA[1] * c13x2 * yCurveB[0] + 2 * xCurveA[3] * xCurveA[1] * c12y2 * xCurveA[0] * yCurveB[0] + 2 * xCurveA[3] * xCurveA[1] * c12y2 * yCurveA[0] * xCurveB[0] + 6 * xCurveA[3] * yCurveB[3] * xCurveA[0] * c13y2 * xCurveB[0] + 6 * xCurveA[3] * xCurveB[2] * xCurveA[0] * c13y2 * yCurveB[1] + 6 * xCurveA[3] * xCurveA[0] * yCurveB[2] * xCurveB[1] * c13y2 + 4 * yCurveA[3] * xCurveA[2] * yCurveA[1] * c13x2 * yCurveB[0] + 6 * yCurveA[3] * xCurveB[3] * xCurveA[0] * c13y2 * xCurveB[0] + 2 * yCurveA[3] * yCurveA[2] * xCurveA[1] * c13x2 * yCurveB[0] - 3 * yCurveA[3] * yCurveA[2] * yCurveA[1] * c13x2 * xCurveB[0] + 2 * yCurveA[3] * xCurveA[1] * c12y2 * xCurveA[0] * xCurveB[0] + 6 * yCurveA[3] * xCurveB[2] * xCurveA[0] * xCurveB[1] * c13y2 - 3 * xCurveA[2] * xCurveB[3] * xCurveA[1] * c13y2 * yCurveB[0] + 2 * xCurveA[2] * xCurveB[3] * yCurveA[1] * c13y2 * xCurveB[0] + xCurveA[2] * yCurveA[2] * c12y2 * xCurveA[0] * xCurveB[0] - 3 * xCurveA[2] * xCurveA[1] * yCurveB[3] * c13y2 * xCurveB[0] - 3 * xCurveA[2] * xCurveA[1] * xCurveB[2] * c13y2 * yCurveB[1] - 3 * xCurveA[2] * xCurveA[1] * yCurveB[2] * xCurveB[1] * c13y2 + 2 * xCurveA[2] * xCurveB[2] * yCurveA[1] * xCurveB[1] * c13y2 + 4 * xCurveB[3] * yCurveA[2] * xCurveA[1] * c13y2 * xCurveB[0] + 4 * yCurveA[2] * xCurveA[1] * xCurveB[2] * xCurveB[1] * c13y2 - 2 * xCurveA[3] * c12x2 * yCurveA[1] * yCurveA[0] * yCurveB[0] - 6 * yCurveA[3] * xCurveB[3] * c13x2 * yCurveA[0] * yCurveB[0] - 6 * yCurveA[3] * yCurveB[3] * c13x2 * yCurveA[0] * xCurveB[0] - 6 * yCurveA[3] * xCurveB[2] * c13x2 * yCurveA[0] * yCurveB[1] - 2 * yCurveA[3] * c12x2 * yCurveA[1] * xCurveA[0] * yCurveB[0] - 2 * yCurveA[3] * c12x2 * yCurveA[1] * yCurveA[0] * xCurveB[0] - 6 * yCurveA[3] * c13x2 * yCurveB[2] * xCurveB[1] * yCurveA[0] - xCurveA[2] * yCurveA[2] * c12x2 * yCurveA[0] * yCurveB[0] - 2 * xCurveA[2] * c11y2 * xCurveA[0] * yCurveA[0] * xCurveB[0] + 3 * xCurveB[3] * yCurveA[2] * yCurveA[1] * c13x2 * yCurveB[0] - 2 * xCurveB[3] * xCurveA[1] * c12y2 * xCurveA[0] * yCurveB[0] - 2 * xCurveB[3] * xCurveA[1] * c12y2 * yCurveA[0] * xCurveB[0] - 6 * xCurveB[3] * yCurveB[3] * xCurveA[0] * c13y2 * xCurveB[0] - 6 * xCurveB[3] * xCurveB[2] * xCurveA[0] * c13y2 * yCurveB[1] - 6 * xCurveB[3] * xCurveA[0] * yCurveB[2] * xCurveB[1] * c13y2 + 3 * yCurveA[2] * yCurveB[3] * yCurveA[1] * c13x2 * xCurveB[0] + 3 * yCurveA[2] * xCurveB[2] * yCurveA[1] * c13x2 * yCurveB[1] + 3 * yCurveA[2] * yCurveA[1] * c13x2 * yCurveB[2] * xCurveB[1] - 2 * xCurveA[1] * yCurveB[3] * c12y2 * xCurveA[0] * xCurveB[0] - 2 * xCurveA[1] * xCurveB[2] * c12y2 * xCurveA[0] * yCurveB[1] - 2 * xCurveA[1] * xCurveB[2] * c12y2 * xCurveB[1] * yCurveA[0] - 2 * xCurveA[1] * c12y2 * xCurveA[0] * yCurveB[2] * xCurveB[1] - 6 * yCurveB[3] * xCurveB[2] * xCurveA[0] * xCurveB[1] * c13y2 - c11y2 * xCurveA[1] * yCurveA[1] * xCurveA[0] * xCurveB[0] + 2 * xCurveB[3] * c12x2 * yCurveA[1] * yCurveA[0] * yCurveB[0] + 6 * yCurveB[3] * c13x2 * yCurveB[2] * xCurveB[1] * yCurveA[0] + 2 * c11x2 * yCurveA[2] * xCurveA[0] * yCurveA[0] * yCurveB[0] + c11x2 * xCurveA[1] * yCurveA[1] * yCurveA[0] * yCurveB[0] + 2 * c12x2 * yCurveB[3] * yCurveA[1] * yCurveA[0] * xCurveB[0] + 2 * c12x2 * xCurveB[2] * yCurveA[1] * yCurveA[0] * yCurveB[1] + 2 * c12x2 * yCurveA[1] * yCurveB[2] * xCurveB[1] * yCurveA[0] + c21x3 * c13y3 + 3 * c10x2 * c13y3 * xCurveB[0] - 3 * c10y2 * c13x3 * yCurveB[0] + 3 * c20x2 * c13y3 * xCurveB[0] + c11y3 * c13x2 * xCurveB[0] - c11x3 * c13y2 * yCurveB[0] - xCurveA[2] * c11y2 * c13x2 * yCurveB[0] + c11x2 * yCurveA[2] * c13y2 * xCurveB[0] - 3 * c10x2 * xCurveA[0] * c13y2 * yCurveB[0] + 3 * c10y2 * c13x2 * yCurveA[0] * xCurveB[0] - c11x2 * c12y2 * xCurveA[0] * yCurveB[0] + c11y2 * c12x2 * yCurveA[0] * xCurveB[0] - 3 * c21x2 * xCurveA[0] * yCurveB[2] * c13y2 - 3 * c20x2 * xCurveA[0] * c13y2 * yCurveB[0] + 3 * c20y2 * c13x2 * yCurveA[0] * xCurveB[0] + xCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * (6 * yCurveB[3] * yCurveB[0] + 6 * yCurveB[2] * yCurveB[1]) + c12x3 * yCurveA[0] * (-2 * yCurveB[3] * yCurveB[0] - 2 * yCurveB[2] * yCurveB[1]) + yCurveA[3] * c13x3 * (6 * yCurveB[3] * yCurveB[0] + 6 * yCurveB[2] * yCurveB[1]) + yCurveA[2] * xCurveA[1] * c13x2 * (-2 * yCurveB[3] * yCurveB[0] - 2 * yCurveB[2] * yCurveB[1]) + c12x2 * yCurveA[1] * xCurveA[0] * (2 * yCurveB[3] * yCurveB[0] + 2 * yCurveB[2] * yCurveB[1]) + xCurveA[2] * yCurveA[1] * c13x2 * (-4 * yCurveB[3] * yCurveB[0] - 4 * yCurveB[2] * yCurveB[1]) + xCurveA[3] * c13x2 * yCurveA[0] * (-6 * yCurveB[3] * yCurveB[0] - 6 * yCurveB[2] * yCurveB[1]) + xCurveB[3] * c13x2 * yCurveA[0] * (6 * yCurveB[3] * yCurveB[0] + 6 * yCurveB[2] * yCurveB[1]) + xCurveB[2] * c13x2 * yCurveA[0] * (6 * yCurveB[3] * yCurveB[1] + 3 * c21y2) + c13x3 * (-2 * yCurveB[3] * yCurveB[2] * yCurveB[1] - c20y2 * yCurveB[0] - yCurveB[2] * (2 * yCurveB[3] * yCurveB[1] + c21y2) - yCurveB[3] * (2 * yCurveB[3] * yCurveB[0] + 2 * yCurveB[2] * yCurveB[1])),
                /* x² */ -xCurveA[3] * xCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[1] + xCurveA[3] * yCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[1] + 6 * xCurveA[3] * yCurveA[2] * yCurveA[1] * xCurveA[0] * xCurveB[1] * yCurveA[0] - 6 * yCurveA[3] * xCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[1] - yCurveA[3] * xCurveA[2] * yCurveA[1] * xCurveA[0] * xCurveB[1] * yCurveA[0] + yCurveA[3] * yCurveA[2] * xCurveA[1] * xCurveA[0] * xCurveB[1] * yCurveA[0] + xCurveA[2] * yCurveA[2] * xCurveA[1] * yCurveA[1] * xCurveA[0] * yCurveB[1] - xCurveA[2] * yCurveA[2] * xCurveA[1] * yCurveA[1] * xCurveB[1] * yCurveA[0] + xCurveA[2] * xCurveB[3] * yCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[1] + xCurveA[2] * yCurveB[3] * yCurveA[1] * xCurveA[0] * xCurveB[1] * yCurveA[0] + xCurveA[2] * xCurveB[2] * yCurveA[1] * xCurveA[0] * yCurveB[2] * yCurveA[0] - xCurveB[3] * yCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[1] - 6 * xCurveB[3] * yCurveA[2] * yCurveA[1] * xCurveA[0] * xCurveB[1] * yCurveA[0] - yCurveA[2] * xCurveA[1] * yCurveB[3] * xCurveA[0] * xCurveB[1] * yCurveA[0] - yCurveA[2] * xCurveA[1] * xCurveB[2] * xCurveA[0] * yCurveB[2] * yCurveA[0] - 6 * xCurveA[3] * xCurveB[3] * xCurveB[1] * c13y3 - 2 * xCurveA[3] * c12y3 * xCurveA[0] * xCurveB[1] + 2 * xCurveB[3] * c12y3 * xCurveA[0] * xCurveB[1] + 2 * yCurveA[3] * c12x3 * yCurveA[0] * yCurveB[1] - 6 * xCurveA[3] * yCurveA[3] * xCurveA[0] * xCurveB[1] * c13y2 + 3 * xCurveA[3] * xCurveA[2] * xCurveA[1] * c13y2 * yCurveB[1] - 2 * xCurveA[3] * xCurveA[2] * yCurveA[1] * xCurveB[1] * c13y2 - 4 * xCurveA[3] * yCurveA[2] * xCurveA[1] * xCurveB[1] * c13y2 + 3 * yCurveA[3] * xCurveA[2] * xCurveA[1] * xCurveB[1] * c13y2 + 6 * xCurveA[3] * yCurveA[3] * c13x2 * yCurveA[0] * yCurveB[1] + 6 * xCurveA[3] * xCurveB[3] * xCurveA[0] * c13y2 * yCurveB[1] - 3 * xCurveA[3] * yCurveA[2] * yCurveA[1] * c13x2 * yCurveB[1] + 2 * xCurveA[3] * xCurveA[1] * c12y2 * xCurveA[0] * yCurveB[1] + 2 * xCurveA[3] * xCurveA[1] * c12y2 * xCurveB[1] * yCurveA[0] + 6 * xCurveA[3] * yCurveB[3] * xCurveA[0] * xCurveB[1] * c13y2 + 6 * xCurveA[3] * xCurveB[2] * xCurveA[0] * yCurveB[2] * c13y2 + 4 * yCurveA[3] * xCurveA[2] * yCurveA[1] * c13x2 * yCurveB[1] + 6 * yCurveA[3] * xCurveB[3] * xCurveA[0] * xCurveB[1] * c13y2 + 2 * yCurveA[3] * yCurveA[2] * xCurveA[1] * c13x2 * yCurveB[1] - 3 * yCurveA[3] * yCurveA[2] * yCurveA[1] * c13x2 * xCurveB[1] + 2 * yCurveA[3] * xCurveA[1] * c12y2 * xCurveA[0] * xCurveB[1] - 3 * xCurveA[2] * xCurveB[3] * xCurveA[1] * c13y2 * yCurveB[1] + 2 * xCurveA[2] * xCurveB[3] * yCurveA[1] * xCurveB[1] * c13y2 + xCurveA[2] * yCurveA[2] * c12y2 * xCurveA[0] * xCurveB[1] - 3 * xCurveA[2] * xCurveA[1] * yCurveB[3] * xCurveB[1] * c13y2 - 3 * xCurveA[2] * xCurveA[1] * xCurveB[2] * yCurveB[2] * c13y2 + 4 * xCurveB[3] * yCurveA[2] * xCurveA[1] * xCurveB[1] * c13y2 - 2 * xCurveA[3] * c12x2 * yCurveA[1] * yCurveA[0] * yCurveB[1] - 6 * yCurveA[3] * xCurveB[3] * c13x2 * yCurveA[0] * yCurveB[1] - 6 * yCurveA[3] * yCurveB[3] * c13x2 * xCurveB[1] * yCurveA[0] - 6 * yCurveA[3] * xCurveB[2] * c13x2 * yCurveB[2] * yCurveA[0] - 2 * yCurveA[3] * c12x2 * yCurveA[1] * xCurveA[0] * yCurveB[1] - 2 * yCurveA[3] * c12x2 * yCurveA[1] * xCurveB[1] * yCurveA[0] - xCurveA[2] * yCurveA[2] * c12x2 * yCurveA[0] * yCurveB[1] - 2 * xCurveA[2] * c11y2 * xCurveA[0] * xCurveB[1] * yCurveA[0] + 3 * xCurveB[3] * yCurveA[2] * yCurveA[1] * c13x2 * yCurveB[1] - 2 * xCurveB[3] * xCurveA[1] * c12y2 * xCurveA[0] * yCurveB[1] - 2 * xCurveB[3] * xCurveA[1] * c12y2 * xCurveB[1] * yCurveA[0] - 6 * xCurveB[3] * yCurveB[3] * xCurveA[0] * xCurveB[1] * c13y2 - 6 * xCurveB[3] * xCurveB[2] * xCurveA[0] * yCurveB[2] * c13y2 + 3 * yCurveA[2] * yCurveB[3] * yCurveA[1] * c13x2 * xCurveB[1] + 3 * yCurveA[2] * xCurveB[2] * yCurveA[1] * c13x2 * yCurveB[2] - 2 * xCurveA[1] * yCurveB[3] * c12y2 * xCurveA[0] * xCurveB[1] - 2 * xCurveA[1] * xCurveB[2] * c12y2 * xCurveA[0] * yCurveB[2] - c11y2 * xCurveA[1] * yCurveA[1] * xCurveA[0] * xCurveB[1] + 2 * xCurveB[3] * c12x2 * yCurveA[1] * yCurveA[0] * yCurveB[1] - 3 * yCurveA[2] * c21x2 * yCurveA[1] * xCurveA[0] * yCurveA[0] + 6 * yCurveB[3] * xCurveB[2] * c13x2 * yCurveB[2] * yCurveA[0] + 2 * c11x2 * yCurveA[2] * xCurveA[0] * yCurveA[0] * yCurveB[1] + c11x2 * xCurveA[1] * yCurveA[1] * yCurveA[0] * yCurveB[1] + 2 * c12x2 * yCurveB[3] * yCurveA[1] * xCurveB[1] * yCurveA[0] + 2 * c12x2 * xCurveB[2] * yCurveA[1] * yCurveB[2] * yCurveA[0] - 3 * xCurveA[3] * c21x2 * c13y3 + 3 * xCurveB[3] * c21x2 * c13y3 + 3 * c10x2 * xCurveB[1] * c13y3 - 3 * c10y2 * c13x3 * yCurveB[1] + 3 * c20x2 * xCurveB[1] * c13y3 + c21x2 * c12y3 * xCurveA[0] + c11y3 * c13x2 * xCurveB[1] - c11x3 * c13y2 * yCurveB[1] + 3 * yCurveA[3] * c21x2 * xCurveA[0] * c13y2 - xCurveA[2] * c11y2 * c13x2 * yCurveB[1] + xCurveA[2] * c21x2 * yCurveA[1] * c13y2 + 2 * yCurveA[2] * xCurveA[1] * c21x2 * c13y2 + c11x2 * yCurveA[2] * xCurveB[1] * c13y2 - xCurveA[1] * c21x2 * c12y2 * yCurveA[0] - 3 * yCurveB[3] * c21x2 * xCurveA[0] * c13y2 - 3 * c10x2 * xCurveA[0] * c13y2 * yCurveB[1] + 3 * c10y2 * c13x2 * xCurveB[1] * yCurveA[0] - c11x2 * c12y2 * xCurveA[0] * yCurveB[1] + c11y2 * c12x2 * xCurveB[1] * yCurveA[0] - 3 * c20x2 * xCurveA[0] * c13y2 * yCurveB[1] + 3 * c20y2 * c13x2 * xCurveB[1] * yCurveA[0] + c12x2 * yCurveA[1] * xCurveA[0] * (2 * yCurveB[3] * yCurveB[1] + c21y2) + xCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * (6 * yCurveB[3] * yCurveB[1] + 3 * c21y2) + c12x3 * yCurveA[0] * (-2 * yCurveB[3] * yCurveB[1] - c21y2) + yCurveA[3] * c13x3 * (6 * yCurveB[3] * yCurveB[1] + 3 * c21y2) + yCurveA[2] * xCurveA[1] * c13x2 * (-2 * yCurveB[3] * yCurveB[1] - c21y2) + xCurveA[2] * yCurveA[1] * c13x2 * (-4 * yCurveB[3] * yCurveB[1] - 2 * c21y2) + xCurveA[3] * c13x2 * yCurveA[0] * (-6 * yCurveB[3] * yCurveB[1] - 3 * c21y2) + xCurveB[3] * c13x2 * yCurveA[0] * (6 * yCurveB[3] * yCurveB[1] + 3 * c21y2) + c13x3 * (-2 * yCurveB[3] * c21y2 - c20y2 * yCurveB[1] - yCurveB[3] * (2 * yCurveB[3] * yCurveB[1] + c21y2)),
                /* x¹ */ -xCurveA[3] * xCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveB[2] * yCurveA[0] + xCurveA[3] * yCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveB[2] * yCurveA[0] + 6 * xCurveA[3] * yCurveA[2] * xCurveB[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] - 6 * yCurveA[3] * xCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveB[2] * yCurveA[0] - yCurveA[3] * xCurveA[2] * xCurveB[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] + yCurveA[3] * yCurveA[2] * xCurveA[1] * xCurveB[2] * xCurveA[0] * yCurveA[0] - xCurveA[2] * yCurveA[2] * xCurveA[1] * xCurveB[2] * yCurveA[1] * yCurveA[0] + xCurveA[2] * yCurveA[2] * xCurveA[1] * yCurveA[1] * xCurveA[0] * yCurveB[2] + xCurveA[2] * xCurveB[3] * yCurveA[1] * xCurveA[0] * yCurveB[2] * yCurveA[0] + 6 * xCurveA[2] * xCurveA[1] * yCurveB[3] * xCurveA[0] * yCurveB[2] * yCurveA[0] + xCurveA[2] * yCurveB[3] * xCurveB[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] - xCurveB[3] * yCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveB[2] * yCurveA[0] - 6 * xCurveB[3] * yCurveA[2] * xCurveB[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] - yCurveA[2] * xCurveA[1] * yCurveB[3] * xCurveB[2] * xCurveA[0] * yCurveA[0] - 6 * xCurveA[3] * xCurveB[3] * xCurveB[2] * c13y3 - 2 * xCurveA[3] * xCurveB[2] * c12y3 * xCurveA[0] + 6 * yCurveA[3] * yCurveB[3] * c13x3 * yCurveB[2] + 2 * xCurveB[3] * xCurveB[2] * c12y3 * xCurveA[0] + 2 * yCurveA[3] * c12x3 * yCurveB[2] * yCurveA[0] - 2 * c12x3 * yCurveB[3] * yCurveB[2] * yCurveA[0] - 6 * xCurveA[3] * yCurveA[3] * xCurveB[2] * xCurveA[0] * c13y2 + 3 * xCurveA[3] * xCurveA[2] * xCurveA[1] * yCurveB[2] * c13y2 - 2 * xCurveA[3] * xCurveA[2] * xCurveB[2] * yCurveA[1] * c13y2 - 4 * xCurveA[3] * yCurveA[2] * xCurveA[1] * xCurveB[2] * c13y2 + 3 * yCurveA[3] * xCurveA[2] * xCurveA[1] * xCurveB[2] * c13y2 + 6 * xCurveA[3] * yCurveA[3] * c13x2 * yCurveB[2] * yCurveA[0] + 6 * xCurveA[3] * xCurveB[3] * xCurveA[0] * yCurveB[2] * c13y2 - 3 * xCurveA[3] * yCurveA[2] * yCurveA[1] * c13x2 * yCurveB[2] + 2 * xCurveA[3] * xCurveA[1] * xCurveB[2] * c12y2 * yCurveA[0] + 2 * xCurveA[3] * xCurveA[1] * c12y2 * xCurveA[0] * yCurveB[2] + 6 * xCurveA[3] * yCurveB[3] * xCurveB[2] * xCurveA[0] * c13y2 + 4 * yCurveA[3] * xCurveA[2] * yCurveA[1] * c13x2 * yCurveB[2] + 6 * yCurveA[3] * xCurveB[3] * xCurveB[2] * xCurveA[0] * c13y2 + 2 * yCurveA[3] * yCurveA[2] * xCurveA[1] * c13x2 * yCurveB[2] - 3 * yCurveA[3] * yCurveA[2] * xCurveB[2] * yCurveA[1] * c13x2 + 2 * yCurveA[3] * xCurveA[1] * xCurveB[2] * c12y2 * xCurveA[0] - 3 * xCurveA[2] * xCurveB[3] * xCurveA[1] * yCurveB[2] * c13y2 + 2 * xCurveA[2] * xCurveB[3] * xCurveB[2] * yCurveA[1] * c13y2 + xCurveA[2] * yCurveA[2] * xCurveB[2] * c12y2 * xCurveA[0] - 3 * xCurveA[2] * xCurveA[1] * yCurveB[3] * xCurveB[2] * c13y2 + 4 * xCurveB[3] * yCurveA[2] * xCurveA[1] * xCurveB[2] * c13y2 - 6 * xCurveA[3] * yCurveB[3] * c13x2 * yCurveB[2] * yCurveA[0] - 2 * xCurveA[3] * c12x2 * yCurveA[1] * yCurveB[2] * yCurveA[0] - 6 * yCurveA[3] * xCurveB[3] * c13x2 * yCurveB[2] * yCurveA[0] - 6 * yCurveA[3] * yCurveB[3] * xCurveB[2] * c13x2 * yCurveA[0] - 2 * yCurveA[3] * c12x2 * xCurveB[2] * yCurveA[1] * yCurveA[0] - 2 * yCurveA[3] * c12x2 * yCurveA[1] * xCurveA[0] * yCurveB[2] - xCurveA[2] * yCurveA[2] * c12x2 * yCurveB[2] * yCurveA[0] - 4 * xCurveA[2] * yCurveB[3] * yCurveA[1] * c13x2 * yCurveB[2] - 2 * xCurveA[2] * c11y2 * xCurveB[2] * xCurveA[0] * yCurveA[0] + 3 * xCurveB[3] * yCurveA[2] * yCurveA[1] * c13x2 * yCurveB[2] - 2 * xCurveB[3] * xCurveA[1] * xCurveB[2] * c12y2 * yCurveA[0] - 2 * xCurveB[3] * xCurveA[1] * c12y2 * xCurveA[0] * yCurveB[2] - 6 * xCurveB[3] * yCurveB[3] * xCurveB[2] * xCurveA[0] * c13y2 - 2 * yCurveA[2] * xCurveA[1] * yCurveB[3] * c13x2 * yCurveB[2] + 3 * yCurveA[2] * yCurveB[3] * xCurveB[2] * yCurveA[1] * c13x2 - 2 * xCurveA[1] * yCurveB[3] * xCurveB[2] * c12y2 * xCurveA[0] - c11y2 * xCurveA[1] * xCurveB[2] * yCurveA[1] * xCurveA[0] + 6 * xCurveB[3] * yCurveB[3] * c13x2 * yCurveB[2] * yCurveA[0] + 2 * xCurveB[3] * c12x2 * yCurveA[1] * yCurveB[2] * yCurveA[0] + 2 * c11x2 * yCurveA[2] * xCurveA[0] * yCurveB[2] * yCurveA[0] + c11x2 * xCurveA[1] * yCurveA[1] * yCurveB[2] * yCurveA[0] + 2 * c12x2 * yCurveB[3] * xCurveB[2] * yCurveA[1] * yCurveA[0] + 2 * c12x2 * yCurveB[3] * yCurveA[1] * xCurveA[0] * yCurveB[2] + 3 * c10x2 * xCurveB[2] * c13y3 - 3 * c10y2 * c13x3 * yCurveB[2] + 3 * c20x2 * xCurveB[2] * c13y3 + c11y3 * xCurveB[2] * c13x2 - c11x3 * yCurveB[2] * c13y2 - 3 * c20y2 * c13x3 * yCurveB[2] - xCurveA[2] * c11y2 * c13x2 * yCurveB[2] + c11x2 * yCurveA[2] * xCurveB[2] * c13y2 - 3 * c10x2 * xCurveA[0] * yCurveB[2] * c13y2 + 3 * c10y2 * xCurveB[2] * c13x2 * yCurveA[0] - c11x2 * c12y2 * xCurveA[0] * yCurveB[2] + c11y2 * c12x2 * xCurveB[2] * yCurveA[0] - 3 * c20x2 * xCurveA[0] * yCurveB[2] * c13y2 + 3 * c20y2 * xCurveB[2] * c13x2 * yCurveA[0],
                /* c  */ xCurveA[3] * yCurveA[3] * xCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] - xCurveA[3] * yCurveA[3] * yCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] + xCurveA[3] * xCurveA[2] * yCurveA[2] * xCurveA[1] * yCurveA[1] * yCurveA[0] - yCurveA[3] * xCurveA[2] * yCurveA[2] * xCurveA[1] * yCurveA[1] * xCurveA[0] - xCurveA[3] * xCurveA[2] * yCurveB[3] * yCurveA[1] * xCurveA[0] * yCurveA[0] + 6 * xCurveA[3] * xCurveB[3] * yCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] + xCurveA[3] * yCurveA[2] * xCurveA[1] * yCurveB[3] * xCurveA[0] * yCurveA[0] - yCurveA[3] * xCurveA[2] * xCurveB[3] * yCurveA[1] * xCurveA[0] * yCurveA[0] - 6 * yCurveA[3] * xCurveA[2] * xCurveA[1] * yCurveB[3] * xCurveA[0] * yCurveA[0] + yCurveA[3] * xCurveB[3] * yCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] - xCurveA[2] * xCurveB[3] * yCurveA[2] * xCurveA[1] * yCurveA[1] * yCurveA[0] + xCurveA[2] * yCurveA[2] * xCurveA[1] * yCurveB[3] * yCurveA[1] * xCurveA[0] + xCurveA[2] * xCurveB[3] * yCurveB[3] * yCurveA[1] * xCurveA[0] * yCurveA[0] - xCurveB[3] * yCurveA[2] * xCurveA[1] * yCurveB[3] * xCurveA[0] * yCurveA[0] - 2 * xCurveA[3] * xCurveB[3] * c12y3 * xCurveA[0] + 2 * yCurveA[3] * c12x3 * yCurveB[3] * yCurveA[0] - 3 * xCurveA[3] * yCurveA[3] * xCurveA[2] * xCurveA[1] * c13y2 - 6 * xCurveA[3] * yCurveA[3] * xCurveB[3] * xCurveA[0] * c13y2 + 3 * xCurveA[3] * yCurveA[3] * yCurveA[2] * yCurveA[1] * c13x2 - 2 * xCurveA[3] * yCurveA[3] * xCurveA[1] * c12y2 * xCurveA[0] - 2 * xCurveA[3] * xCurveA[2] * xCurveB[3] * yCurveA[1] * c13y2 - xCurveA[3] * xCurveA[2] * yCurveA[2] * c12y2 * xCurveA[0] + 3 * xCurveA[3] * xCurveA[2] * xCurveA[1] * yCurveB[3] * c13y2 - 4 * xCurveA[3] * xCurveB[3] * yCurveA[2] * xCurveA[1] * c13y2 + 3 * yCurveA[3] * xCurveA[2] * xCurveB[3] * xCurveA[1] * c13y2 + 6 * xCurveA[3] * yCurveA[3] * yCurveB[3] * c13x2 * yCurveA[0] + 2 * xCurveA[3] * yCurveA[3] * c12x2 * yCurveA[1] * yCurveA[0] + 2 * xCurveA[3] * xCurveA[2] * c11y2 * xCurveA[0] * yCurveA[0] + 2 * xCurveA[3] * xCurveB[3] * xCurveA[1] * c12y2 * yCurveA[0] + 6 * xCurveA[3] * xCurveB[3] * yCurveB[3] * xCurveA[0] * c13y2 - 3 * xCurveA[3] * yCurveA[2] * yCurveB[3] * yCurveA[1] * c13x2 + 2 * xCurveA[3] * xCurveA[1] * yCurveB[3] * c12y2 * xCurveA[0] + xCurveA[3] * c11y2 * xCurveA[1] * yCurveA[1] * xCurveA[0] + yCurveA[3] * xCurveA[2] * yCurveA[2] * c12x2 * yCurveA[0] + 4 * yCurveA[3] * xCurveA[2] * yCurveB[3] * yCurveA[1] * c13x2 - 3 * yCurveA[3] * xCurveB[3] * yCurveA[2] * yCurveA[1] * c13x2 + 2 * yCurveA[3] * xCurveB[3] * xCurveA[1] * c12y2 * xCurveA[0] + 2 * yCurveA[3] * yCurveA[2] * xCurveA[1] * yCurveB[3] * c13x2 + xCurveA[2] * xCurveB[3] * yCurveA[2] * c12y2 * xCurveA[0] - 3 * xCurveA[2] * xCurveB[3] * xCurveA[1] * yCurveB[3] * c13y2 - 2 * xCurveA[3] * c12x2 * yCurveB[3] * yCurveA[1] * yCurveA[0] - 6 * yCurveA[3] * xCurveB[3] * yCurveB[3] * c13x2 * yCurveA[0] - 2 * yCurveA[3] * xCurveB[3] * c12x2 * yCurveA[1] * yCurveA[0] - 2 * yCurveA[3] * c11x2 * yCurveA[2] * xCurveA[0] * yCurveA[0] - yCurveA[3] * c11x2 * xCurveA[1] * yCurveA[1] * yCurveA[0] - 2 * yCurveA[3] * c12x2 * yCurveB[3] * yCurveA[1] * xCurveA[0] - 2 * xCurveA[2] * xCurveB[3] * c11y2 * xCurveA[0] * yCurveA[0] - xCurveA[2] * yCurveA[2] * c12x2 * yCurveB[3] * yCurveA[0] + 3 * xCurveB[3] * yCurveA[2] * yCurveB[3] * yCurveA[1] * c13x2 - 2 * xCurveB[3] * xCurveA[1] * yCurveB[3] * c12y2 * xCurveA[0] - xCurveB[3] * c11y2 * xCurveA[1] * yCurveA[1] * xCurveA[0] + 3 * c10y2 * xCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] + 3 * xCurveA[2] * xCurveA[1] * c20y2 * xCurveA[0] * yCurveA[0] + 2 * xCurveB[3] * c12x2 * yCurveB[3] * yCurveA[1] * yCurveA[0] - 3 * c10x2 * yCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] + 2 * c11x2 * yCurveA[2] * yCurveB[3] * xCurveA[0] * yCurveA[0] + c11x2 * xCurveA[1] * yCurveB[3] * yCurveA[1] * yCurveA[0] - 3 * c20x2 * yCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] - c10x3 * c13y3 + c10y3 * c13x3 + c20x3 * c13y3 - c20y3 * c13x3 - 3 * xCurveA[3] * c20x2 * c13y3 - xCurveA[3] * c11y3 * c13x2 + 3 * c10x2 * xCurveB[3] * c13y3 + yCurveA[3] * c11x3 * c13y2 + 3 * yCurveA[3] * c20y2 * c13x3 + xCurveB[3] * c11y3 * c13x2 + c10x2 * c12y3 * xCurveA[0] - 3 * c10y2 * yCurveB[3] * c13x3 - c10y2 * c12x3 * yCurveA[0] + c20x2 * c12y3 * xCurveA[0] - c11x3 * yCurveB[3] * c13y2 - c12x3 * c20y2 * yCurveA[0] - xCurveA[3] * c11x2 * yCurveA[2] * c13y2 + yCurveA[3] * xCurveA[2] * c11y2 * c13x2 - 3 * xCurveA[3] * c10y2 * c13x2 * yCurveA[0] - xCurveA[3] * c11y2 * c12x2 * yCurveA[0] + yCurveA[3] * c11x2 * c12y2 * xCurveA[0] - xCurveA[2] * c11y2 * yCurveB[3] * c13x2 + 3 * c10x2 * yCurveA[3] * xCurveA[0] * c13y2 + c10x2 * xCurveA[2] * yCurveA[1] * c13y2 + 2 * c10x2 * yCurveA[2] * xCurveA[1] * c13y2 - 2 * c10y2 * xCurveA[2] * yCurveA[1] * c13x2 - c10y2 * yCurveA[2] * xCurveA[1] * c13x2 + c11x2 * xCurveB[3] * yCurveA[2] * c13y2 - 3 * xCurveA[3] * c20y2 * c13x2 * yCurveA[0] + 3 * yCurveA[3] * c20x2 * xCurveA[0] * c13y2 + xCurveA[2] * c20x2 * yCurveA[1] * c13y2 - 2 * xCurveA[2] * c20y2 * yCurveA[1] * c13x2 + xCurveB[3] * c11y2 * c12x2 * yCurveA[0] - yCurveA[2] * xCurveA[1] * c20y2 * c13x2 - c10x2 * xCurveA[1] * c12y2 * yCurveA[0] - 3 * c10x2 * yCurveB[3] * xCurveA[0] * c13y2 + 3 * c10y2 * xCurveB[3] * c13x2 * yCurveA[0] + c10y2 * c12x2 * yCurveA[1] * xCurveA[0] - c11x2 * yCurveB[3] * c12y2 * xCurveA[0] + 2 * c20x2 * yCurveA[2] * xCurveA[1] * c13y2 + 3 * xCurveB[3] * c20y2 * c13x2 * yCurveA[0] - c20x2 * xCurveA[1] * c12y2 * yCurveA[0] - 3 * c20x2 * yCurveB[3] * xCurveA[0] * c13y2 + c12x2 * c20y2 * yCurveA[1] * xCurveA[0]
            ).Trim().RootsInInterval();

            foreach (var s in roots)
            {
                var point = new Point2D(
                    xCurveB[0] * s * s * s + xCurveB[1] * s * s + xCurveB[2] * s + xCurveB[3],
                    yCurveB[0] * s * s * s + yCurveB[1] * s * s + yCurveB[2] * s + yCurveB[3]);

                var xRoots = (xCurveA - point.X).Trim().Roots();
                var yRoots = (yCurveA - point.Y).Trim().Roots();

                // ToDo: Figure out why the xRoots can be larger than 1 or smaller than 0 and still work...
                if (xRoots.Length > 0 && yRoots.Length > 0)
                {
                    // Find the nearest matching x and y roots in the ranges 0 < x < 1; 0 < y < 1.
                    foreach (var xRoot in xRoots)
                    {
                        if (0 <= xRoot && xRoot <= 1)
                        {
                            foreach (var yRoot in yRoots)
                            {
                                var t = xRoot - yRoot;
                                if ((t >= 0 ? t : -t) < tolerance)
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
            {
                result.State = IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Finds the intersection between a cubic bezier and a polygon.
        /// </summary>
        /// <param name="b1X"></param>
        /// <param name="b1Y"></param>
        /// <param name="b2X"></param>
        /// <param name="b2Y"></param>
        /// <param name="b3X"></param>
        /// <param name="b3Y"></param>
        /// <param name="b4X"></param>
        /// <param name="b4Y"></param>
        /// <param name="points"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierSegmentPolygonIntersection(
            double b1X, double b1Y, double b2X, double b2Y, double b3X, double b3Y, double b4X, double b4Y,
            List<Point2D> points,
            double epsilon = Epsilon)
            => CubicBezierSegmentPolygonIntersection(
                Polynomial.Cubic(b1X, b2X, b3X, b4X),
                Polynomial.Cubic(b1Y, b2Y, b3Y, b4Y),
                points,
                epsilon);

        /// <summary>
        /// Finds the intersection between a cubic bezier and a polygon.
        /// </summary>
        /// <param name="xCurve"></param>
        /// <param name="yCurve"></param>
        /// <param name="points"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierSegmentPolygonIntersection(
            Polynomial xCurve, Polynomial yCurve,
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

                intersections.UnionWith(LineSegmentCubicBezierSegmentIntersection(a1.X, a1.Y, a2.X, a2.Y, xCurve, yCurve, epsilon).Points);

                a1 = a2;
            }

            // ToDo: Return IntersectionState.Inside if both end points are inside the Polygon and there are no intersections.

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Count > 0)
            {
                result.State |= IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a cubic bezier and a rectangle.
        /// </summary>
        /// <param name="xCurve"></param>
        /// <param name="yCurve"></param>
        /// <param name="t1X"></param>
        /// <param name="t1Y"></param>
        /// <param name="t2X"></param>
        /// <param name="t2Y"></param>
        /// <param name="t3X"></param>
        /// <param name="t3Y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierSegmentTriangleIntersection(
            Polynomial xCurve, Polynomial yCurve,
            double t1X, double t1Y, double t2X, double t2Y, double t3X, double t3Y,
            double epsilon = Epsilon)
        {
            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();

            intersections.UnionWith(LineSegmentCubicBezierSegmentIntersection(t1X, t1Y, t2X, t2Y, xCurve, yCurve, epsilon).Points);
            intersections.UnionWith(LineSegmentCubicBezierSegmentIntersection(t2X, t2Y, t3X, t3Y, xCurve, yCurve, epsilon).Points);
            intersections.UnionWith(LineSegmentCubicBezierSegmentIntersection(t3X, t3Y, t1X, t1Y, xCurve, yCurve, epsilon).Points);

            // ToDo: Return IntersectionState.Inside if both end points are inside the rectangle and there are no intersections.

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a cubic bezier and a rectangle.
        /// </summary>
        /// <param name="b1X"></param>
        /// <param name="b1Y"></param>
        /// <param name="b2X"></param>
        /// <param name="b2Y"></param>
        /// <param name="b3X"></param>
        /// <param name="b3Y"></param>
        /// <param name="b4X"></param>
        /// <param name="b4Y"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierSegmentRectangleIntersection(
            double b1X, double b1Y, double b2X, double b2Y, double b3X, double b3Y, double b4X, double b4Y,
            double r1X, double r1Y, double r2X, double r2Y,
            double epsilon = Epsilon)
            => CubicBezierSegmentRectangleIntersection(
                Polynomial.Cubic(b1X, b2X, b3X, b4X),
                Polynomial.Cubic(b1Y, b2Y, b3Y, b4Y),
                r1X, r1Y, r2X, r2Y,
                epsilon);

        /// <summary>
        /// Find the intersection between a cubic bezier and a rectangle.
        /// </summary>
        /// <param name="xCurve"></param>
        /// <param name="yCurve"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierSegmentRectangleIntersection(
            Polynomial xCurve, Polynomial yCurve,
            double r1X, double r1Y, double r2X, double r2Y,
            double epsilon = Epsilon)
        {
            var (minX, minY) = MinPoint(r1X, r1Y, r2X, r2Y);
            var (maxX, maxY) = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(maxX, minY);
            var bottomLeft = new Point2D(minX, maxY);

            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            intersections.UnionWith(LineSegmentCubicBezierSegmentIntersection(minX, minY, topRight.X, topRight.Y, xCurve, yCurve, epsilon).Points);
            intersections.UnionWith(LineSegmentCubicBezierSegmentIntersection(topRight.X, topRight.Y, maxX, maxY, xCurve, yCurve, epsilon).Points);
            intersections.UnionWith(LineSegmentCubicBezierSegmentIntersection(maxX, maxY, bottomLeft.X, bottomLeft.Y, xCurve, yCurve, epsilon).Points);
            intersections.UnionWith(LineSegmentCubicBezierSegmentIntersection(bottomLeft.X, bottomLeft.Y, minX, minY, xCurve, yCurve, epsilon).Points);

            // ToDo: Return IntersectionState.Inside if both end points are inside the rectangle and there are no intersections.

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between two triangles.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="x4"></param>
        /// <param name="y4"></param>
        /// <param name="x5"></param>
        /// <param name="y5"></param>
        /// <param name="x6"></param>
        /// <param name="y6"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection TriangleTriangleIntersection(
            double x1, double y1, double x2, double y2, double x3, double y3,
            double x4, double y4, double x5, double y5, double x6, double y6,
            double epsilon = Epsilon)
        {
            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();

            intersections.UnionWith(LineSegmentLineSegmentIntersection(x1, y1, x2, y2, x4, y4, x5, y5, epsilon).Points);
            intersections.UnionWith(LineSegmentLineSegmentIntersection(x1, y1, x2, y2, x5, y5, x6, y6, epsilon).Points);
            intersections.UnionWith(LineSegmentLineSegmentIntersection(x1, y1, x2, y2, x4, y4, x6, y6, epsilon).Points);

            intersections.UnionWith(LineSegmentLineSegmentIntersection(x2, y2, x3, y3, x4, y4, x5, y5, epsilon).Points);
            intersections.UnionWith(LineSegmentLineSegmentIntersection(x2, y2, x3, y3, x5, y5, x6, y6, epsilon).Points);
            intersections.UnionWith(LineSegmentLineSegmentIntersection(x2, y2, x3, y3, x4, y4, x6, y6, epsilon).Points);

            intersections.UnionWith(LineSegmentLineSegmentIntersection(x1, y1, x3, y3, x4, y4, x5, y5, epsilon).Points);
            intersections.UnionWith(LineSegmentLineSegmentIntersection(x1, y1, x3, y3, x5, y5, x6, y6, epsilon).Points);
            intersections.UnionWith(LineSegmentLineSegmentIntersection(x1, y1, x3, y3, x4, y4, x6, y6, epsilon).Points);

            // ToDo: Return IntersectionState.Inside if all of the points of one rectangle are inside the other rectangle.

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a triangle and a rectangle.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection TriangleRectangleIntersection(
            double x1, double y1, double x2, double y2, double x3, double y3,
            double left, double top, double right, double bottom,
            double epsilon = Epsilon)
        {
            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();

            intersections.UnionWith(LineSegmentLineSegmentIntersection(x1, y1, x2, y2, left, top, right, top, epsilon).Points);
            intersections.UnionWith(LineSegmentLineSegmentIntersection(x1, y1, x2, y2, right, top, right, bottom, epsilon).Points);
            intersections.UnionWith(LineSegmentLineSegmentIntersection(x1, y1, x2, y2, right, bottom, left, bottom, epsilon).Points);
            intersections.UnionWith(LineSegmentLineSegmentIntersection(x1, y1, x2, y2, left, bottom, left, top, epsilon).Points);

            intersections.UnionWith(LineSegmentLineSegmentIntersection(x2, y2, x3, y3, left, top, right, top, epsilon).Points);
            intersections.UnionWith(LineSegmentLineSegmentIntersection(x2, y2, x3, y3, right, top, right, bottom, epsilon).Points);
            intersections.UnionWith(LineSegmentLineSegmentIntersection(x2, y2, x3, y3, right, bottom, left, bottom, epsilon).Points);
            intersections.UnionWith(LineSegmentLineSegmentIntersection(x2, y2, x3, y3, left, bottom, left, top, epsilon).Points);

            intersections.UnionWith(LineSegmentLineSegmentIntersection(x1, y1, x3, y3, left, top, right, top, epsilon).Points);
            intersections.UnionWith(LineSegmentLineSegmentIntersection(x1, y1, x3, y3, right, top, right, bottom, epsilon).Points);
            intersections.UnionWith(LineSegmentLineSegmentIntersection(x1, y1, x3, y3, right, bottom, left, bottom, epsilon).Points);
            intersections.UnionWith(LineSegmentLineSegmentIntersection(x1, y1, x3, y3, left, bottom, left, top, epsilon).Points);

            // ToDo: Return IntersectionState.Inside if all of the points of one rectangle are inside the other rectangle.

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a triangle and a polygon contour.
        /// </summary>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="a2X"></param>
        /// <param name="a2Y"></param>
        /// <param name="a3X"></param>
        /// <param name="a3Y"></param>
        /// <param name="points"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection TrianglePolygonContourIntersection(
            double a1X, double a1Y, double a2X, double a2Y, double a3X, double a3Y,
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
                intersections.UnionWith(LineSegmentLineSegmentIntersection(b1.X, b1.Y, b2.X, b2.Y, a2X, a2Y, a3X, a3Y, epsilon).Points);
                intersections.UnionWith(LineSegmentLineSegmentIntersection(b1.X, b1.Y, b2.X, b2.Y, a1X, a1Y, a3X, a3Y, epsilon).Points);

                b1 = b2;
            }

            // ToDo: Return IntersectionState.Inside if both end points are inside the polygon, and there are no intersections.

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Count > 0)
            {
                result.State |= IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a triangle and a circle.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="angle"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection TriangleCircleIntersection(
            double x0, double y0, double x1, double y1, double x2, double y2,
            double cX, double cY, double r, double angle,
            double epsilon = Epsilon)
        {
            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();

            intersections.UnionWith(LineSegmentCircleIntersection(x0, y0, x1, y1, cX, cY, r, angle, epsilon).Points);
            intersections.UnionWith(LineSegmentCircleIntersection(x1, y1, x2, y2, cX, cY, r, angle, epsilon).Points);
            intersections.UnionWith(LineSegmentCircleIntersection(x2, y2, x0, y0, cX, cY, r, angle, epsilon).Points);

            // ToDo: Return IntersectionState.Inside if all of the points of one rectangle are inside the other rectangle.

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between an ellipse and a triangle.
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
        /// <param name="r3X"></param>
        /// <param name="r3Y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection TriangleEllipseIntersection(
            double r1X, double r1Y, double r2X, double r2Y, double r3X, double r3Y,
            double cX, double cY, double rx, double ry, double cosA, double sinA,
            double epsilon = Epsilon)
        {
            var result = new Intersection(IntersectionState.NoIntersection);

            result.AppendPoints(LineSegmentEllipseIntersection(cX, cY, rx, ry, cosA, sinA, r1X, r1Y, r2X, r2Y, epsilon).Points);
            result.AppendPoints(LineSegmentEllipseIntersection(cX, cY, rx, ry, cosA, sinA, r2X, r2Y, r3X, r3Y, epsilon).Points);
            result.AppendPoints(LineSegmentEllipseIntersection(cX, cY, rx, ry, cosA, sinA, r3X, r3Y, r1X, r1Y, epsilon).Points);

            if (result.Points.Count > 0)
            {
                result.State = IntersectionState.Intersection;
            }

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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
            var (minX, minY) = MinPoint(a1X, a1Y, a2X, a2Y);
            var (maxX, maxY) = MaxPoint(a1X, a1Y, a2X, a2Y);
            var topRight = new Point2D(maxX, minY);
            var bottomLeft = new Point2D(minX, maxY);

            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            intersections.UnionWith(LineSegmentRectangleIntersection(minX, minY, topRight.X, topRight.Y, b1X, b1Y, b2X, b2Y, epsilon).Points);
            intersections.UnionWith(LineSegmentRectangleIntersection(topRight.X, topRight.Y, maxX, maxY, b1X, b1Y, b2X, b2Y, epsilon).Points);
            intersections.UnionWith(LineSegmentRectangleIntersection(maxX, maxY, bottomLeft.X, bottomLeft.Y, b1X, b1Y, b2X, b2Y, epsilon).Points);
            intersections.UnionWith(LineSegmentRectangleIntersection(bottomLeft.X, bottomLeft.Y, minX, minY, b1X, b1Y, b2X, b2Y, epsilon).Points);

            // ToDo: Return IntersectionState.Inside if all of the points of one rectangle are inside the other rectangle.

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between two polygon contours.
        /// </summary>
        /// <param name="points1"></param>
        /// <param name="points2"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
            {
                result.State |= IntersectionState.Intersection;
            }

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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
            var (minX, minY) = MinPoint(r1X, r1Y, r2X, r2Y);
            var (maxX, maxY) = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(maxX, minY);
            var bottomLeft = new Point2D(minX, maxY);

            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            intersections.UnionWith(LineSegmentPolygonContourIntersection(minX, minY, topRight.X, topRight.Y, points, epsilon).Points);
            intersections.UnionWith(LineSegmentPolygonContourIntersection(topRight.X, topRight.Y, maxX, maxY, points, epsilon).Points);
            intersections.UnionWith(LineSegmentPolygonContourIntersection(maxX, maxY, bottomLeft.X, bottomLeft.Y, points, epsilon).Points);
            intersections.UnionWith(LineSegmentPolygonContourIntersection(bottomLeft.X, bottomLeft.Y, minX, minY, points, epsilon).Points);

            // ToDo: Return IntersectionState.Inside if all of the end points are contained inside the rectangle, or the points of the rectangle are inside the polygon, and there are no intersections.

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionState.Intersection;
            }

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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
            var (minX, minY) = MinPoint(r1X, r1Y, r2X, r2Y);
            var (maxX, maxY) = MinPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(maxX, minY);
            var bottomLeft = new Point2D(minX, maxY);

            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            intersections.UnionWith(LineSegmentCircleIntersection(minX, minY, topRight.X, topRight.Y, cX, cY, r, angle, epsilon).Points);
            intersections.UnionWith(LineSegmentCircleIntersection(topRight.X, topRight.Y, maxX, maxY, cX, cY, r, angle, epsilon).Points);
            intersections.UnionWith(LineSegmentCircleIntersection(maxX, maxY, bottomLeft.X, bottomLeft.Y, cX, cY, r, angle, epsilon).Points);
            intersections.UnionWith(LineSegmentCircleIntersection(bottomLeft.X, bottomLeft.Y, minX, minY, cX, cY, r, angle, epsilon).Points);

            // ToDo: Return IntersectionState.Inside if all of the points of the rectangle are contained within the circle.

            var result = new Intersection(IntersectionState.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionState.Intersection;
            }
            else
            {
                result.State = LineSegmentCircleIntersection(minX, minY, topRight.X, topRight.Y, cX, cY, r, angle, epsilon).State;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a circle and a polygon.
        /// </summary>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="r"></param>
        /// <param name="angle"></param>
        /// <param name="points"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CirclePolygonIntersection(
            double cX, double cY, double r, double angle,
            List<Point2D> points,
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
            {
                result.State |= IntersectionState.Intersection;
            }
            else
            {
                result.State = inter.State;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a circle and a polygon contour.
        /// </summary>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <param name="radius"></param>
        /// <param name="angle"></param>
        /// <param name="points"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
            {
                result.State |= IntersectionState.Intersection;
            }
            else
            {
                result.State = inter.State;
            }

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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
            {
                return result;
            }

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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
                var (x, y) = Lerp(c1X, c1Y, c2X, c2Y, a / c_dist);
                var b = h / c_dist;
                result.AppendPoint(new Point2D(x - b * (c2Y - c1Y), y + b * (c2X - c1X)));
                result.AppendPoint(new Point2D(x + b * (c2Y - c1Y), y - b * (c2X - c1X)));
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="cosA"></param>
        /// <param name="sinA"></param>
        /// <param name="points"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
            {
                result.State |= IntersectionState.Intersection;
            }
            else
            {
                result.State = inter.State;
            }

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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
            var (minX, minY) = MinPoint(r1X, r1Y, r2X, r2Y);
            var (maxX, maxY) = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(maxX, minY);
            var bottomLeft = new Point2D(minX, maxY);

            var result = new Intersection(IntersectionState.NoIntersection);
            result.AppendPoints(EllipseRectangleIntersection(cX, cY, rx, ry, cosA, sinA, minX, minY, topRight.X, topRight.Y, epsilon).Points);
            result.AppendPoints(EllipseRectangleIntersection(cX, cY, rx, ry, cosA, sinA, topRight.X, topRight.Y, maxX, maxY, epsilon).Points);
            result.AppendPoints(EllipseRectangleIntersection(cX, cY, rx, ry, cosA, sinA, maxX, maxY, bottomLeft.X, bottomLeft.Y, epsilon).Points);
            result.AppendPoints(EllipseRectangleIntersection(cX, cY, rx, ry, cosA, sinA, bottomLeft.X, bottomLeft.Y, minX, minY, epsilon).Points);

            if (result.Points.Count > 0)
            {
                result.State = IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a quadratic bezier and an unrotated ellipse.
        /// </summary>
        /// <param name="b1X"></param>
        /// <param name="b1Y"></param>
        /// <param name="b2X"></param>
        /// <param name="b2Y"></param>
        /// <param name="b3X"></param>
        /// <param name="b3Y"></param>
        /// <param name="ecX"></param>
        /// <param name="ecY"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentUnrotatedEllipseIntersection(
            double b1X, double b1Y, double b2X, double b2Y, double b3X, double b3Y,
            double ecX, double ecY, double rx, double ry,
            double epsilon = Epsilon)
            => QuadraticBezierSegmentUnrotatedEllipseIntersection(
                Polynomial.Quadratic(b1X, b2X, b3X),
                Polynomial.Quadratic(b1Y, b2Y, b3Y),
                ecX, ecY, rx, ry,
                epsilon);

        /// <summary>
        /// Find the intersection between a quadratic bezier and an unrotated ellipse.
        /// </summary>
        /// <param name="xCurve">The set of Polynomial Bezier Coefficients of the x coordinates of the Bezier curve.</param>
        /// <param name="yCurve">The set of Polynomial Bezier Coefficients of the y coordinates of the Bezier curve.</param>
        /// <param name="ecX"></param>
        /// <param name="ecY"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentUnrotatedEllipseIntersection(
            Polynomial xCurve, Polynomial yCurve,
            double ecX, double ecY, double rx, double ry,
            double epsilon = Epsilon)
        {
            // Initialize intersection.
            var result = new Intersection(IntersectionState.NoIntersection);

            var rxrx = rx * rx;
            var ryry = ry * ry;

            // Find the polynomial that represents the intersections.
            var roots = new Polynomial(
                /* x⁴ */ ryry * xCurve[0] * xCurve[0] + rxrx * yCurve[0] * yCurve[0],
                /* x³ */ 2 * (ryry * xCurve[0] * xCurve[1] + rxrx * yCurve[0] * yCurve[1]),
                /* x² */ ryry * (2 * xCurve[0] * xCurve[2] + xCurve[1] * xCurve[1]) + rxrx * (2 * yCurve[0] * yCurve[2] + yCurve[1] * yCurve[1]) - 2 * (ryry * ecX * xCurve[0] + rxrx * ecY * yCurve[0]),
                /* x¹ */ 2 * (ryry * xCurve[1] * (xCurve[2] - ecX) + rxrx * yCurve[1] * (yCurve[2] - ecY)),
                /* c  */ ryry * (xCurve[2] * xCurve[2] + ecY * ecY) + rxrx * (yCurve[2] * yCurve[2] + ecY * ecY) - 2 * (ryry * ecX * xCurve[2] + rxrx * ecY * yCurve[2]) - rxrx * ryry
                ).Trim().Roots();

            foreach (var s in roots)
            {
                var point = new Point2D(
                    xCurve[0] * s * s + xCurve[1] * s + xCurve[2],
                    yCurve[0] * s * s + yCurve[1] * s + yCurve[2]);

                if (0 <= s && s <= 1)
                {
                    result.Points.Add(point);
                }
            }

            if (result.Points.Count > 0)
            {
                result.State = IntersectionState.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a cubic bezier and an unrotated ellipse.
        /// </summary>
        /// <param name="b1X"></param>
        /// <param name="b1Y"></param>
        /// <param name="b2X"></param>
        /// <param name="b2Y"></param>
        /// <param name="b3X"></param>
        /// <param name="b3Y"></param>
        /// <param name="b4X"></param>
        /// <param name="b4Y"></param>
        /// <param name="ecX"></param>
        /// <param name="ecY"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierSegmentUnrotatedEllipseIntersection(
            double b1X, double b1Y, double b2X, double b2Y, double b3X, double b3Y, double b4X, double b4Y,
            double ecX, double ecY, double rx, double ry,
            double epsilon = Epsilon)
            => CubicBezierSegmentUnrotatedEllipseIntersection(
                Polynomial.Cubic(b1X, b2X, b3X, b4X),
                Polynomial.Cubic(b1Y, b2Y, b3Y, b4Y),
                ecX, ecY, rx, ry,
                epsilon);

        /// <summary>
        /// Find the intersection between a cubic bezier and an unrotated ellipse.
        /// </summary>
        /// <param name="xCurve">The set of Polynomial Bezier Coefficients of the x coordinates of the Bezier curve.</param>
        /// <param name="yCurve">The set of Polynomial Bezier Coefficients of the y coordinates of the Bezier curve.</param>
        /// <param name="ecX"></param>
        /// <param name="ecY"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierSegmentUnrotatedEllipseIntersection(
            Polynomial xCurve, Polynomial yCurve,
            double ecX, double ecY, double rx, double ry,
            double epsilon = Epsilon)
        {
            // Initialize intersection.
            var result = new Intersection(IntersectionState.NoIntersection);

            var rxrx = rx * rx;
            var ryry = ry * ry;

            // Find the polynomial that represents the intersections.
            var roots = new Polynomial(
                /* x⁶ */ xCurve[0] * xCurve[0] * ryry + yCurve[0] * yCurve[0] * rxrx,
                /* x⁵ */ 2 * (xCurve[0] * xCurve[1] * ryry + yCurve[0] * yCurve[1] * rxrx),
                /* x⁴ */ 2 * (xCurve[0] * xCurve[2] * ryry + yCurve[0] * yCurve[2] * rxrx) + xCurve[1] * xCurve[1] * ryry + yCurve[1] * yCurve[1] * rxrx,
                /* x³ */ 2 * xCurve[0] * ryry * (xCurve[3] - ecX) + 2 * yCurve[0] * rxrx * (yCurve[3] - ecY) + 2 * (xCurve[1] * xCurve[2] * ryry + yCurve[1] * yCurve[2] * rxrx),
                /* x² */ 2 * xCurve[1] * ryry * (xCurve[3] - ecX) + 2 * yCurve[1] * rxrx * (yCurve[3] - ecY) + xCurve[2] * xCurve[2] * ryry + yCurve[2] * yCurve[2] * rxrx,
                /* x¹ */ 2 * xCurve[2] * ryry * (xCurve[3] - ecX) + 2 * yCurve[2] * rxrx * (yCurve[3] - ecY),
                /* c  */ xCurve[3] * xCurve[3] * ryry - 2 * yCurve[3] * ecY * rxrx - 2 * xCurve[3] * ecX * ryry + yCurve[3] * yCurve[3] * rxrx + ecX * ecX * ryry + ecY * ecY * rxrx - rxrx * ryry
                ).Trim().RootsInInterval();

            foreach (var s in roots)
            {
                var point = new Point2D(
                    xCurve[0] * s * s * s + xCurve[1] * s * s + xCurve[2] * s + xCurve[3],
                    yCurve[0] * s * s * s + yCurve[1] * s * s + yCurve[2] * s + yCurve[3]);

                result.Points.Add(point);
            }

            if (result.Points.Count > 0)
            {
                result.State = IntersectionState.Intersection;
            }

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
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
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
            var result = new Intersection(IntersectionState.NoIntersection);

            double[] a = new double[] { ry1 * ry1, 0, rx1 * rx1, -2 * ry1 * ry1 * c1X, -2 * rx1 * rx1 * c1Y, ry1 * ry1 * c1X * c1X + rx1 * rx1 * c1Y * c1Y - rx1 * rx1 * ry1 * ry1 };
            double[] b = new double[] { ry2 * ry2, 0, rx2 * rx2, -2 * ry2 * ry2 * c2X, -2 * rx2 * rx2 * c2Y, ry2 * ry2 * c2X * c2X + rx2 * rx2 * c2Y * c2Y - rx2 * rx2 * ry2 * ry2 };

            var yRoots = Bezout(a, b).Trim().Roots();

            var norm0 = (a[0] * a[0] + 2 * a[1] * a[1] + a[2] * a[2]) * epsilon;
            var norm1 = (b[0] * b[0] + 2 * b[1] * b[1] + b[2] * b[2]) * epsilon;

            for (var y = 0; y < yRoots.Length; y++)
            {
                var xRoots = new Polynomial(
                    a[0],
                    a[3] + yRoots[y] * a[1],
                    a[5] + yRoots[y] * (a[4] + yRoots[y] * a[2]),
                    epsilon).Trim().Roots();
                for (var x = 0; x < xRoots.Length; x++)
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
            {
                result.State = IntersectionState.Intersection;
            }

            return result;
        }

        #endregion

        #region Parametrized Intersection Index t Methods

        /// <summary>
        /// Find the intersection parameters of the intersection between two points.
        /// </summary>
        /// <param name="p0x"></param>
        /// <param name="p0y"></param>
        /// <param name="p1x"></param>
        /// <param name="p1y"></param>
        /// <param name="epsilon"></param>
        /// <returns>Returns a pair of arrays of t indexes on the shapes where intersections occur.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double[] a, double[] b) PointPointIntersectionIndexes(
            double p0x, double p0y,
            double p1x, double p1y,
            double epsilon = Epsilon)
        {
            if (p0x == p1x && p0y == p1y)
            {
                return (new double[] { 1d }, new double[] { 1d });
            }

            return (null, null);
        }

        /// <summary>
        /// Find the intersection parameters of the intersection between a point and a line.
        /// </summary>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="lx"></param>
        /// <param name="ly"></param>
        /// <param name="li"></param>
        /// <param name="lj"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns a pair of arrays of t indexes on the shapes where intersections occur.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double[] a, double[] b) PointLineIntersectionIndexes(
            double px, double py,
            double lx, double ly, double li, double lj,
            double epsilon = Epsilon)
        {
            // Vector a -> p
            (var vi, var vj) = (lx - px, ly - py);

            // Get the determinant or squared length of the line segment.
            var d = li * li + lj * lj;

            // Get the length of the line segment.
            var length = Sqrt(d);

            // Find the interpolation value.
            var t = -(vi * li + vj * lj) / d;

            // Check whether the closest point falls between the ends of line segment.
            // Return the t values if the distance to the nearest point on the line segment is within epsilon.
            return (length == 0) ? (Sqrt(vi * vi + vj * vj) < epsilon ? (new double[] { 1 }, new double[] { t }) : (new double[] { }, new double[] { }))
                : ((Abs(li * vj - vi * lj) / length) < epsilon ? (new double[] { 1 }, new double[] { t }) : (new double[] { }, new double[] { }));
        }

        /// <summary>
        /// Find the intersection parameters of the intersection between a point and a ray.
        /// </summary>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="ax"></param>
        /// <param name="ay"></param>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns a pair of arrays of t indexes on the shapes where intersections occur.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double[] a, double[] b) PointRayIntersectionIndexes(
            double px, double py,
            double ax, double ay, double bx, double by,
            double epsilon = Epsilon)
        {
            // Vector of line segment a -> b
            (var ui, var uj) = (bx - ax, by - ay);

            // Vector a -> p
            (var vi, var vj) = (ax - px, ay - py);

            // Get the determinant or squared length of the line segment.
            var d = ui * ui + uj * uj;

            // Get the length of the line segment.
            var length = Sqrt(d);

            // Find the interpolation value.
            var t = -(vi * ui + vj * uj) / d;

            // Check whether the closest point falls between the ends of line segment.
            // Return the t values if the distance to the nearest point on the line segment is within epsilon.
            return (t < 0d) ? (new double[] { }, new double[] { })
                : (length == 0) ? (Sqrt(vi * vi + vj * vj) < epsilon ? (new double[] { 1 }, new double[] { t }) : (new double[] { }, new double[] { }))
                : ((Abs(ui * vj - vi * uj) / length) < epsilon ? (new double[] { 1 }, new double[] { t }) : (new double[] { }, new double[] { }));
        }

        /// <summary>
        /// Find the intersection parameters of the intersections between a point and a line.
        /// </summary>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="ax"></param>
        /// <param name="ay"></param>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns a pair of arrays of t indexes on the shapes where intersections occur.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double[] a, double[] b) PointLineSegmentIntersectionIndexes(
            double px, double py,
            double ax, double ay, double bx, double by,
            double epsilon = Epsilon)
        {
            // Vector of line segment a -> b
            (var ui, var uj) = (bx - ax, by - ay);

            // Vector a -> p
            (var vi, var vj) = (ax - px, ay - py);

            // Get the determinant or squared length of the line segment.
            var d = ui * ui + uj * uj;

            // Get the length of the line segment.
            var length = Sqrt(d);

            // Find the interpolation value.
            var t = -(vi * ui + vj * uj) / d;

            // Check whether the closest point falls between the ends of line segment.
            // Return the t values if the distance to the nearest point on the line segment is within epsilon.
            return (t < 0d || t > 1d) ? (new double[] { }, new double[] { })
                : (length == 0) ? (Sqrt(vi * vi + vj * vj) < epsilon ? (new double[] { 1 }, new double[] { t }) : (new double[] { }, new double[] { }))
                : ((Abs(ui * vj - vi * uj) / length) < epsilon ? (new double[] { 1 }, new double[] { t }) : (new double[] { }, new double[] { }));
        }

        /// <summary>
        /// Find the intersection parameters of the intersection between two lines.
        /// </summary>
        /// <param name="ax"></param>
        /// <param name="ay"></param>
        /// <param name="ai"></param>
        /// <param name="aj"></param>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="bi"></param>
        /// <param name="bj"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns a pair of arrays of t indexes on the shapes where intersections occur.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double[] a, double[] b) LineLineIntersectionIndexes(
            double ax, double ay, double ai, double aj,
            double bx, double by, double bi, double bj,
            double epsilon = Epsilon)
        {
            var result = (a: new double[] { }, b: new double[] { });

            var ua = bi * (ay - by) - bj * (ax - bx);
            var ub = ai * (ay - by) - aj * (ax - bx);

            var determinant = bj * ai - bi * aj;

            if (determinant != 0)
            {
                var ta = ua / determinant;
                var tb = ub / determinant;

                result = (a: new double[] { ta }, b: new double[] { tb });
            }

            return result;
        }

        /// <summary>
        /// Find the intersection parameters of the intersection between a lane and a ray.
        /// </summary>
        /// <param name="lx">The x component of the first point of the second line.</param>
        /// <param name="ly">The y component of the first point of the second line.</param>
        /// <param name="li">The x component of the second point of the second line.</param>
        /// <param name="lj">The y component of the second point of the second line.</param>
        /// <param name="rx">The x component of the first point of the ray.</param>
        /// <param name="ry">The y component of the first point of the ray.</param>
        /// <param name="ri">The x component of the second point of the ray.</param>
        /// <param name="rj">The y component of the second point of the ray.</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns a pair of arrays of t indexes on the shapes where intersections occur.</returns>
        /// <acknowledgment>
        /// http://www.vb-helper.com/howto_segments_intersect.html
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double[] a, double[] b) LineRayIntersectionIndexes(
            double lx, double ly, double li, double lj,
            double rx, double ry, double ri, double rj,
            double epsilon = Epsilon)
        {
            // Intersection cross product.
            var ua = (li * (ry - ly)) - (lj * (rx - lx)); // Line
            var ub = (ri * (ry - ly)) - (rj * (rx - lx)); // Ray

            // Calculate the determinant of the coefficient matrix.
            var determinant = (lj * ri) - (li * rj);

            // Check if the lines are parallel.
            if (Abs(determinant) < epsilon)
            {
                if (ua == 0 || ub == 0)
                {
                    // Ray is coincident to the Line. There are an infinite number of intersections, but we only care about the start and end points of the line segment.
                    return (a: new double[] { ua }, b: new double[] { 0 });
                }
            }
            else
            {
                // Find the index where the intersection point lies on the line.
                var ta = ua / determinant;
                var tb = ub / determinant;

                if (ta >= 0 /*&& ta <= 1 && tb >= 0 && tb <= 1*/)
                {
                    // One intersection.
                    return (a: new double[] { ta }, b: new double[] { tb });
                }
            }

            return (a: new double[] { }, b: new double[] { });
        }

        /// <summary>
        /// Find the intersection parameters of the intersection between two line segments.
        /// </summary>
        /// <param name="lx"></param>
        /// <param name="ly"></param>
        /// <param name="li"></param>
        /// <param name="lj"></param>
        /// <param name="s0X"></param>
        /// <param name="s0Y"></param>
        /// <param name="s1X"></param>
        /// <param name="s1Y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns a pair of arrays of t indexes on the shapes where intersections occur.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double[] a, double[] b) LineLineSegmentIntersectionIndexes(
            double lx, double ly, double li, double lj,
            double s0X, double s0Y, double s1X, double s1Y,
            double epsilon = Epsilon)
        {
            // Translate lines to origin.
            var vi = s1X - s0X;
            var vj = s1Y - s0Y;

            var ua = vi * (ly - s0Y) - vj * (lx - s0X);
            var ub = li * (ly - s0Y) - lj * (lx - s0X);

            // Calculate the determinant of the coefficient matrix.
            var determinant = (vj * li) - (vi * lj);

            // Check if the lines are parallel.
            if (Abs(determinant) < epsilon)
            {
                if (ua == 0 || ub == 0)
                {
                    // Line segment is coincident to the Line. There are an infinite number of intersections, but we only care about the start and end points of the line segment.
                    return (a: new double[] { ua /* Is this right? */, ub /* Is this right? */ }, b: new double[] { 0, 1 });
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
                    return (a: new double[] { ta }, b: new double[] { tb });
                }
            }

            return (a: new double[] { }, b: new double[] { });
        }

        /// <summary>
        /// Find the intersection parameters of the intersection between two rays.
        /// </summary>
        /// <param name="ax"></param>
        /// <param name="ay"></param>
        /// <param name="ai"></param>
        /// <param name="aj"></param>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="bi"></param>
        /// <param name="bj"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns a pair of arrays of t indexes on the shapes where intersections occur.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double[] a, double[] b) RayRayIntersectionIndexes(
            double ax, double ay, double ai, double aj,
            double bx, double by, double bi, double bj,
            double epsilon = Epsilon)
        {
            var result = (a: new double[] { }, b: new double[] { });

            var ua = bi * (ay - by) - bj * (ax - bx);
            var ub = ai * (ay - by) - aj * (ax - bx);

            var determinant = bj * ai - bi * aj;

            if (determinant != 0)
            {
                var ta = ua / determinant;
                var tb = ub / determinant;

                if (ta >= 0 /*&& ta <= 1*/ && tb >= 0 /*&& tb <= 1*/)
                {
                    result = (a: new double[] { ta }, b: new double[] { tb });
                }
            }

            return result;
        }

        /// <summary>
        /// Find the intersection parameters of the intersection between a ray and a line segment.
        /// </summary>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="ri"></param>
        /// <param name="rj"></param>
        /// <param name="s1X"></param>
        /// <param name="s1Y"></param>
        /// <param name="s2X"></param>
        /// <param name="s2Y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns a pair of arrays of t indexes on the shapes where intersections occur.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double[] a, double[] b) RayLineSegmentIntersectionIndexes(
            double rx, double ry, double ri, double rj,
            double s1X, double s1Y, double s2X, double s2Y,
            double epsilon = Epsilon)
        {
            // Translate line segment to origin.
            var u = s2X - s1X;
            var v = s2Y - s1Y;

            // Intersection cross product.
            var ua = u * (ry - s1Y) - v * (rx - s1X);
            var ub = ri * (ry - s1Y) - rj * (rx - s1X);

            // Calculate the determinant of the coefficient matrix.
            var determinant = (v * ri) - (u * rj);

            // Check if the lines are parallel.
            if (Abs(determinant) < epsilon)
            {
                if (ua == 0 || ub == 0)
                {
                    // Line segment is coincident to the Line. There are an infinite number of intersections, but we only care about the start and end points of the line segment.
                    return (a: new double[] { }, b: new double[] { });
                    // ToDo: Figure out which end points intersect which ray/line segment.
                }
            }
            else
            {
                // Find the index where the intersection point lies on the line.
                var ta = ua / determinant;
                var tb = ub / determinant;

                if (ta >= 0 /*&& ta <= 1*/ && tb >= 0 && tb <= 1)
                {
                    // One intersection.
                    return (a: new double[] { ta }, b: new double[] { tb });
                }
            }

            return (a: new double[] { }, b: new double[] { });
        }

        /// <summary>
        /// Find the intersection parameters of the intersection between two line segments.
        /// </summary>
        /// <param name="aax"></param>
        /// <param name="aay"></param>
        /// <param name="abx"></param>
        /// <param name="aby"></param>
        /// <param name="bax"></param>
        /// <param name="bay"></param>
        /// <param name="bbx"></param>
        /// <param name="bby"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns a pair of arrays of t indexes on the shapes where intersections occur.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double[] a, double[] b) LineSegmentLineSegmentIntersectionIndexes(
            double aax, double aay, double abx, double aby,
            double bax, double bay, double bbx, double bby,
            double epsilon = Epsilon)
        {
            var result = (a: new double[] { }, b: new double[] { });

            var ua = (bbx - bax) * (aay - bay) - (bby - bay) * (aax - bax);
            var ub = (abx - aax) * (aay - bay) - (aby - aay) * (aax - bax);

            var determinant = (bby - bay) * (abx - aax) - (bbx - bax) * (aby - aay);

            if (determinant != 0)
            {
                var ta = ua / determinant;
                var tb = ub / determinant;

                if (0 <= ta && ta <= 1 && 0 <= tb && tb <= 1)
                {
                    result = (a: new double[] { ta }, b: new double[] { tb });
                }
            }

            return result;
        }

        /// <summary>
        /// Find the intersection parameters of the self intersection of a cubic bezier curve.
        /// </summary>
        /// <param name="xCurve"></param>
        /// <param name="yCurve"></param>
        /// <param name="epsilon"></param>
        /// <returns>Returns an array of t values for locating the self intersection indexes on the curve.</returns>
        /// <acknowledgment>
        /// https://groups.google.com/d/msg/comp.graphics.algorithms/SRm97nRWlw4/R1Rn38ep8n0J
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[] CubicBezierSelfIntersectionIndexes(
            Polynomial xCurve, Polynomial yCurve,
            double epsilon = Epsilon)
        {
            (var a, var b) = (xCurve[0] == 0d) ? (xCurve[1], xCurve[2]) : (xCurve[1] / xCurve[0], xCurve[2] / xCurve[0]);
            (var p, var q) = (yCurve[0] == 0d) ? (yCurve[1], yCurve[2]) : (yCurve[1] / yCurve[0], yCurve[2] / yCurve[0]);

            if (a == p || q == b)
            {
                return new double[0];
            }

            var k = (q - b) / (a - p);

            var roots = new Polynomial(
                2,
                -3 * k,
                3 * k * k + 2 * k * a + 2 * b,
                -k * k * k - a * k * k - b * k
                ).Roots().OrderByDescending(c => c).ToArray();

            if (roots.Length != 3)
            {
                return null;
            }

            if (roots[0] >= 0.0 && roots[0] <= 1.0 && roots[2] >= 0.0 && roots[2] <= 1.0)
            {
                // ToDo: Work out whether to go the more complex route and compare the points at the t values. 
                return new double[] { roots[0], roots[2] };
            }

            return new double[0];
        }

        #endregion

        #region Scan-beam Intersection Methods

        /// <summary>
        /// Find the scan-beam intersections of a point.
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
            {
                scanlist.Add(x);
            }
        }

        /// <summary>
        /// Find the scan-beam intersections of a line.
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
        /// Find the scan-beam intersections of a line segment.
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
        /// Find the scan-beam intersections of a quadratic bezier curve segment.
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="b0x"></param>
        /// <param name="b0y"></param>
        /// <param name="b1x"></param>
        /// <param name="b1y"></param>
        /// <param name="b2x"></param>
        /// <param name="b2y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamQuadraticBezierSegment(
            ref List<double> scanlist,
            double x, double y,
            double b0x, double b0y, double b1x, double b1y, double b2x, double b2y,
            double epsilon = Epsilon)
            => ScanbeamQuadraticBezierSegment(
                ref scanlist,
                x, y,
                Polynomial.Quadratic(b0x, b1x, b2x),
                Polynomial.Quadratic(b0y, b1y, b2y),
                epsilon);

        /// <summary>
        /// Find the scan-beam intersections of a quadratic bezier curve segment.
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="xCurve"></param>
        /// <param name="yCurve"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamQuadraticBezierSegment(
            ref List<double> scanlist,
            double x, double y,
            Polynomial xCurve, Polynomial yCurve,
            double epsilon = Epsilon)
        {
            var c = x * (y - (y + 0)) + y * (x + 1 - x);
            var roots = (yCurve - c).Trim().Roots();
            foreach (var s in roots)
            {
                // Add intersection point.
                if (!(s < 0 || s > 1d))
                {
                    scanlist.Add(xCurve[0] * s * s + xCurve[1] * s + xCurve[2]);
                }
            }
        }

        /// <summary>
        /// Find the scan-beam intersections of a cubic bezier curve segment.
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="b0x"></param>
        /// <param name="b0y"></param>
        /// <param name="b1x"></param>
        /// <param name="b1y"></param>
        /// <param name="b2x"></param>
        /// <param name="b2y"></param>
        /// <param name="b3x"></param>
        /// <param name="b3y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamCubicBezierSegment(
            ref List<double> scanlist,
            double x, double y,
            double b0x, double b0y, double b1x, double b1y, double b2x, double b2y, double b3x, double b3y,
            double epsilon = Epsilon)
            => ScanbeamCubicBezierSegment(
                ref scanlist,
                x, y,
                Polynomial.Cubic(b0x, b1x, b2x, b3x),
                Polynomial.Cubic(b0y, b1y, b2y, b3y),
                epsilon);

        /// <summary>
        /// Find the scan-beam intersections of a cubic bezier curve segment.
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="xCurve"></param>
        /// <param name="yCurve"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ScanbeamCubicBezierSegment(
            ref List<double> scanlist,
            double x, double y,
            Polynomial xCurve, Polynomial yCurve,
            double epsilon = Epsilon)
        {
            // Translate the line to the origin.
            var c = x * (y - (y + 0)) + y * (x + 1 - x);
            var roots = (yCurve - c).Trim().Roots();
            foreach (var s in roots)
            {
                // Add intersection point.
                if (!(s < 0 || s > 1d))
                {
                    scanlist.Add(xCurve[0] * s * s * s + xCurve[1] * s * s + xCurve[2] * s + xCurve[3]);
                }
            }
        }

        /// <summary>
        /// Find the scan-beam intersections of a circle.
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
            {
                return;
            }

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
        /// Find the scan-beam intersections of a circular arc.
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
            {
                return;
            }

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
        /// Find the scan-beam intersections of an ellipse.
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
            {
                return;
            }

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
        /// Find the scan-beam intersections of an elliptical arc.
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
            {
                return;
            }

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
                {
                    scanlist.Add(px);
                }

                // Find the point.
                px = u0 + (u1 - u0) * t2 + cx;
                py = v0 + (v1 - v0) * t2 + cy;

                // Find the determinant of the matrix representing the chord.
                determinant = (sx - px) * (ey - py) - (ex - px) * (sy - py);

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle))
                {
                    scanlist.Add(px);
                }
            }
        }

        /// <summary>
        /// Find the scan-beam intersections of a rectangle.
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
            var (minX, minY) = MinPoint(r1X, r1Y, r2X, r2Y);
            var (maxX, maxY) = MaxPoint(r1X, r1Y, r2X, r2Y);
            ScanbeamLineSegment(ref scanlist, x, y, maxX, minY, maxX, maxY, epsilon);
            ScanbeamLineSegment(ref scanlist, x, y, minX, maxY, minX, minY, epsilon);
        }

        /// <summary>
        /// Find the scan-beam intersections of a polygon contour.
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
        /// Find the scan-beam intersections of a polycurve contour.
        /// </summary>
        /// <param name="scanlist"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="curve"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
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
        /// Find the scan-beam points to the left of a point.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftPoint(double x, double y, double px, double py, double epsilon = Epsilon)
            => (((y - py) / (x - px) == 1) && (px <= x)) ? 1 : 0;

        /// <summary>
        /// Find the scan-beam points to the left of a line.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
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
                {
                    return 1;
                }
            }

            return 0;
        }

        /// <summary>
        /// Find the scan-beam points to the left of a line segment.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
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
                    {
                        result++;
                    }

                    if (x1 <= x)
                    {
                        result++;
                    }
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
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Find the scan-beam points to the left of a quadratic bezier segment.
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
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftQuadraticBezierSegment(
            double x, double y,
            double p0x, double p0y, double p1x, double p1y, double p2x, double p2y,
            double epsilon = Epsilon)
            => ScanbeamPointsToLeftQuadraticBezierSegment(
                x, y,
                Polynomial.Quadratic(p0x, p1x, p2x),
                Polynomial.Quadratic(p0y, p1y, p2y),
                epsilon);

        /// <summary>
        /// Find the scan-beam points to the left of a quadratic bezier segment.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="xCurve"></param>
        /// <param name="yCurve"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftQuadraticBezierSegment(
            double x, double y,
            Polynomial xCurve, Polynomial yCurve,
            double epsilon = Epsilon)
        {
            var c = x * (y - (y + 0)) + y * (x + 1 - x);
            var roots = (yCurve - c).Trim().Roots();
            var result = 0;
            foreach (var s in roots)
            {
                // Add intersection point.
                if (!(s < 0 || s > 1d) && (xCurve[0] * s * s + xCurve[1] * s + xCurve[2] <= x))
                {
                    result++;
                }
            }

            return result;
        }

        /// <summary>
        /// Find the scan-beam points to the left of a cubic bezier segment.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="b0x"></param>
        /// <param name="b0y"></param>
        /// <param name="b1x"></param>
        /// <param name="b1y"></param>
        /// <param name="b2x"></param>
        /// <param name="b2y"></param>
        /// <param name="b3x"></param>
        /// <param name="b3y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftCubicBezierSegment(
            double x, double y,
            double b0x, double b0y, double b1x, double b1y, double b2x, double b2y, double b3x, double b3y,
            double epsilon = Epsilon)
            => ScanbeamPointsToLeftCubicBezierSegment(
                x, y,
                Polynomial.Cubic(b0x, b1x, b2x, b3x),
                Polynomial.Cubic(b0y, b1y, b2y, b3y),
                epsilon);

        /// <summary>
        /// Find the scan-beam points to the left of a cubic bezier segment.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="xCurve"></param>
        /// <param name="yCurve"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftCubicBezierSegment(
            double x, double y,
            Polynomial xCurve, Polynomial yCurve,
            double epsilon = Epsilon)
        {
            // Translate the line to the origin.
            var c = x * (y - (y + 0)) + y * (x + 1 - x);
            var roots = (yCurve - c).Trim().Roots();
            var results = 0;
            foreach (var s in roots)
            {
                // Add intersection point.
                if (!(s < 0 || s > 1d) && (xCurve[0] * s * s * s + xCurve[1] * s * s + xCurve[2] * s + xCurve[3]) <= x)
                {
                    results++;
                }
            }

            return results;
        }

        /// <summary>
        /// Find the scan-beam points to the left of a circle.
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
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftCircle(double x, double y, double cX, double cY, double r, double angle, double startAngle, double sweepAngle, double epsilon = Epsilon)
        {
            // If the circle or line segment are empty, return no intersections.
            if ((r == 0d) || ((x == x + 1) && (y == x + 0)))
            {
                return 0;
            }

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
                {
                    result++;
                }

                if (x + t2 * 1 <= x)
                {
                    result++;
                }
            }

            return result;
        }

        /// <summary>
        /// Find the scan-beam points to the left of a circular arc.
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
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftCircularArc(double x, double y, double cX, double cY, double r, double angle, double startAngle, double sweepAngle, double epsilon = Epsilon)
        {
            // If the circle or line segment are empty, return no intersections.
            if (r == 0d)
            {
                return 0;
            }

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
        /// Find the scan-beam points to the left of an ellipse.
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
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftEllipse(double x, double y, double cx, double cy, double rx, double ry, double cosA, double sinA, double epsilon = Epsilon)
        {
            // If the ellipse is empty, return no intersections.
            if ((rx == 0d) || (ry == 0d))
            {
                return 0;
            }

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
                {
                    results++;
                }

                if ((u1 + (u2 - u1) * t + cx) <= x)
                {
                    results++;
                }
            }
            else if (discriminant > 0)
            {
                // Two real possible solutions.
                var t1 = (OneHalf * (-b + Sqrt(discriminant)) / a);
                var t2 = (OneHalf * (-b - Sqrt(discriminant)) / a);

                // Add the points.
                if (u1 + (u2 - u1) * t1 + cx <= x)
                {
                    results++;
                }

                if (u1 + (u2 - u1) * t2 + cx <= x)
                {
                    results++;
                }
            }

            return results;
        }

        /// <summary>
        /// Find the scan-beam points to the left of an elliptical arc.
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
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftEllipticalArc(double x, double y, double cx, double cy, double rx, double ry, double cosA, double sinA, double startAngle, double sweepAngle, double epsilon = Epsilon)
        {
            // If the ellipse or line segment are empty, return no intersections.
            if ((sweepAngle == 0d) || (rx == 0d) || (ry == 0d))
            {
                return 0;
            }

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
                {
                    results++;
                }

                // Find the point.
                px = u0 + (u1 - u0) * t2 + cx;
                py = v0 + (v1 - v0) * t2 + cy;

                // Find the determinant of the matrix representing the chord.
                determinant = (sx - px) * (ey - py) - (ex - px) * (sy - py);

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle) && px <= x)
                {
                    results++;
                }
            }

            return results;
        }

        /// <summary>
        /// Find the scan-beam points to the left of a rectangle.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToLeftRectangle(double x, double y, double r1X, double r1Y, double r2X, double r2Y, double epsilon = Epsilon)
        {
            var results = 0;
            var (minX, minY) = MinPoint(r1X, r1Y, r2X, r2Y);
            var (maxX, maxY) = MaxPoint(r1X, r1Y, r2X, r2Y);
            results += ScanbeamPointsToLeftLineSegment(x, y, maxX, minY, maxX, maxY, epsilon);
            results += ScanbeamPointsToLeftLineSegment(x, y, minX, maxY, minX, minY, epsilon);
            return results;
        }

        /// <summary>
        /// Find the scan-beam points to the left of a polygon contour.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="points"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
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
        /// Find the scan-beam points to the left of a polycurve contour.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="curve"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the left of the x axis along the y plane of the scan-beam.</returns>
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
        /// Find the scan-beam points to the right of a point.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightPoint(double x, double y, double px, double py, double epsilon = Epsilon)
            => (((y - py) / (x - px) == 1) && (px >= x)) ? 1 : 0;

        /// <summary>
        /// Find the scan-beam points to the right of a line.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
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
                {
                    return 1;
                }
            }

            return 0;
        }

        /// <summary>
        /// Find the scan-beam points to the right of a line segment.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
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
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Find the scan-beam points to the right of a quadratic bezier curve segment.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="b0x"></param>
        /// <param name="b0y"></param>
        /// <param name="b1x"></param>
        /// <param name="b1y"></param>
        /// <param name="b2x"></param>
        /// <param name="b2y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightQuadraticBezierSegment(
            double x, double y,
            double b0x, double b0y, double b1x, double b1y, double b2x, double b2y,
            double epsilon = Epsilon)
            => ScanbeamPointsToRightQuadraticBezierSegment(
                x, y,
                Polynomial.Quadratic(b0x, b1x, b2x),
                Polynomial.Quadratic(b0y, b1y, b2y),
                epsilon);

        /// <summary>
        /// Find the scan-beam points to the right of a quadratic bezier curve segment.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="xCurve"></param>
        /// <param name="yCurve"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightQuadraticBezierSegment(
            double x, double y,
            Polynomial xCurve, Polynomial yCurve,
            double epsilon = Epsilon)
        {
            var c = x * (y - (y + 0)) + y * (x + 1 - x);
            var roots = (yCurve - c).Trim().Roots();
            var result = 0;
            foreach (var s in roots)
            {
                // Add intersection point.
                if (!(s < 0 || s > 1d) && ((xCurve[0] * s * s + xCurve[1] * s + xCurve[2]) >= x))
                {
                    result++;
                }
            }

            return result;
        }

        /// <summary>
        /// Find the scan-beam points to the right of a cubic bezier curve segment.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="b0x"></param>
        /// <param name="b0y"></param>
        /// <param name="b1x"></param>
        /// <param name="b1y"></param>
        /// <param name="b2x"></param>
        /// <param name="b2y"></param>
        /// <param name="b3x"></param>
        /// <param name="b3y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightCubicBezierSegment(
            double x, double y,
            double b0x, double b0y, double b1x, double b1y, double b2x, double b2y, double b3x, double b3y,
            double epsilon = Epsilon)
            => ScanbeamPointsToRightCubicBezierSegment(
                x, y,
                Polynomial.Cubic(b0x, b1x, b2x, b3x),
                Polynomial.Cubic(b0y, b1y, b2y, b3y),
                epsilon);

        /// <summary>
        /// Find the scan-beam points to the right of a cubic bezier curve segment.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="xCurve"></param>
        /// <param name="yCurve"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightCubicBezierSegment(
            double x, double y,
            Polynomial xCurve, Polynomial yCurve,
            double epsilon = Epsilon)
        {
            // Translate the line to the origin.
            var c = x * (y - (y + 0)) + y * (x + 1 - x);
            var roots = (yCurve - c).Trim().Roots();
            var results = 0;
            foreach (var s in roots)
            {
                // Add intersection point.
                if (!(s < 0 || s > 1d) && (xCurve[0] * s * s * s + xCurve[1] * s * s + xCurve[2] * s + xCurve[3]) >= x)
                {
                    results++;
                }
            }

            return results;
        }

        /// <summary>
        /// Find the scan-beam points to the right of a circle.
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
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightCircle(double x, double y, double cX, double cY, double r, double angle, double startAngle, double sweepAngle, double epsilon = Epsilon)
        {
            // If the circle or line segment are empty, return no intersections.
            if ((r == 0d) || ((x == x + 1) && (y == x + 0)))
            {
                return 0;
            }

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
                {
                    result++;
                }

                if (x + t2 * 1 >= x)
                {
                    result++;
                }
            }

            return result;
        }

        /// <summary>
        /// Find the scan-beam points to the right of a circular arc.
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
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightCircularArc(double x, double y, double cX, double cY, double r, double angle, double startAngle, double sweepAngle, double epsilon = Epsilon)
        {
            // If the circle or line segment are empty, return no intersections.
            if (r == 0d)
            {
                return 0;
            }

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
        /// Find the scan-beam points to the right of an ellipse.
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
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightEllipse(double x, double y, double cx, double cy, double rx, double ry, double cosA, double sinA, double epsilon = Epsilon)
        {
            // If the ellipse is empty, return no intersections.
            if ((rx == 0d) || (ry == 0d))
            {
                return 0;
            }

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
                {
                    results++;
                }

                if ((u1 + (u2 - u1) * t + cx) >= x)
                {
                    results++;
                }
            }
            else if (discriminant > 0)
            {
                // Two real possible solutions.
                var t1 = (OneHalf * (-b + Sqrt(discriminant)) / a);
                if (u1 + (u2 - u1) * t1 + cx >= x)
                {
                    results++;
                }

                var t2 = (OneHalf * (-b - Sqrt(discriminant)) / a);
                if (u1 + (u2 - u1) * t2 + cx >= x)
                {
                    results++;
                }
            }

            return results;
        }

        /// <summary>
        /// Find the scan-beam points to the right of an elliptical arc.
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
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightEllipticalArc(double x, double y, double cx, double cy, double rx, double ry, double cosA, double sinA, double startAngle, double sweepAngle, double epsilon = Epsilon)
        {
            // If the ellipse or line segment are empty, return no intersections.
            if ((sweepAngle == 0d) || (rx == 0d) || (ry == 0d))
            {
                return 0;
            }

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
                {
                    results++;
                }

                // Find the point.
                px = u0 + (u1 - u0) * t2 + cx;
                py = v0 + (v1 - v0) * t2 + cy;

                // Find the determinant of the matrix representing the chord.
                determinant = (sx - px) * (ey - py) - (ex - px) * (sy - py);

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle) && px >= x)
                {
                    results++;
                }
            }

            return results;
        }

        /// <summary>
        /// Find the scan-beam points to the right of a rectangle.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ScanbeamPointsToRightRectangle(double x, double y, double r1X, double r1Y, double r2X, double r2Y, double epsilon = Epsilon)
        {
            var results = 0;
            var (minX, minY) = MinPoint(r1X, r1Y, r2X, r2Y);
            var (maxX, maxY) = MaxPoint(r1X, r1Y, r2X, r2Y);
            results += ScanbeamPointsToRightLineSegment(x, y, maxX, minY, maxX, maxY, epsilon);
            results += ScanbeamPointsToRightLineSegment(x, y, minX, maxY, minX, minY, epsilon);
            return results;
        }

        /// <summary>
        /// Find the scan-beam points to the right of a polygon contour.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="points"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
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
        /// Find the scan-beam points to the right of a polycurve contour.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="curve"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns the number of intersections to the right of the x axis along the y plane of the scan-beam.</returns>
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
        /// Calculate the bezier curve polynomial of ellipses.
        /// </summary>
        /// <param name="e1">First Ellipse parameters.</param>
        /// <param name="e2">Second Ellipse parameters.</param>
        /// <returns>Returns a <see cref="Polynomial"/> of the ellipse.</returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// This code is based on MgcIntr2DElpElp.cpp written by David Eberly.
        /// His code along with many other excellent examples formerly available
        /// at his site but the latest version now at: https://www.geometrictools.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Bezout(double[] e1, double[] e2)
        {
            var ab = e1[0] * e2[1] - e2[0] * e1[1];
            var ac = e1[0] * e2[2] - e2[0] * e1[2];
            var ad = e1[0] * e2[3] - e2[0] * e1[3];
            var ae = e1[0] * e2[4] - e2[0] * e1[4];
            var af = e1[0] * e2[5] - e2[0] * e1[5];

            var bc = e1[1] * e2[2] - e2[1] * e1[2];
            var be = e1[1] * e2[4] - e2[1] * e1[4];
            var bf = e1[1] * e2[5] - e2[1] * e1[5];

            var cd = e1[2] * e2[3] - e2[2] * e1[3];

            var de = e1[3] * e2[4] - e2[3] * e1[4];
            var df = e1[3] * e2[5] - e2[3] * e1[5];

            var bfPde = bf + de;
            var beMcd = be - cd;

            return new Polynomial(
                /* x⁴ */ ab * bc - ac * ac,
                /* x³ */ ab * beMcd + ad * bc - 2 * ac * ae,
                /* x² */ ab * bfPde + ad * beMcd - ae * ae - 2 * ac * af,
                /* x¹ */ ab * df + ad * bfPde - 2 * ae * af,
                /* c  */ ad * df - af * af);
        }

        #endregion
    }
}
