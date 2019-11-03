// <copyright file="Intersections.cs" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
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
//     Some of the methods came from Rod Stephens excellent blogs vb-helper(http://vb-helper.com), and c sharp helper (http://csharphelper.com), as well as from his books.
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
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static System.Math;
using static Engine.Mathematics;
using static Engine.Measurements;
using static Engine.Operations;
using static Engine.Polynomials;

namespace Engine
{
    /// <summary>
    /// A collection of methods for collecting the interactions of shapes.
    /// </summary>
    public static partial class Intersections
    {
        #region Intersection Extension Method Overloads
        /// <summary>
        /// Find the intersection of two Points.
        /// </summary>
        /// <param name="a">The first point <paramref name="a"/>.</param>
        /// <param name="b">The second point <paramref name="b"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Point2D a, Point2D b, double epsilon = Epsilon) => PointPointIntersection(a.X, a.Y, b.X, b.Y, epsilon);

        /// <summary>
        /// Find the intersection of a point and a line.
        /// </summary>
        /// <param name="p">The point <paramref name="p"/>.</param>
        /// <param name="l">The line <paramref name="l"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Point2D p, Line l, double epsilon = Epsilon) => PointLineIntersection(p.X, p.Y, (l?.Location).Value.X, l.Location.Y, l.Direction.I, l.Direction.J, epsilon);

        /// <summary>
        /// Find the intersection of a Point and a Line segment.
        /// </summary>
        /// <param name="p">The point <paramref name="p"/>.</param>
        /// <param name="s">The line segment <paramref name="s"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Point2D p, LineSegment s, double epsilon = Epsilon) => PointLineSegmentIntersection(p.X, p.Y, (s?.AX).Value, s.AY, s.BX, s.BY, epsilon);

        /// <summary>
        /// Find the intersection of a Point and a Line segment.
        /// </summary>
        /// <param name="p">The point <paramref name="p"/>.</param>
        /// <param name="r">The <paramref name="r"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Point2D p, Ray r, double epsilon = Epsilon) => PointRayIntersection(p.X, p.Y, (r?.Location).Value.X, r.Location.Y, r.Direction.I, r.Direction.J, epsilon);

        /// <summary>
        /// Find the intersection of two Bézier segments.
        /// </summary>
        /// <param name="a">The first Bézier segment <paramref name="a"/>.</param>
        /// <param name="b">The second Bézier segment <paramref name="b"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this BezierSegmentX a, BezierSegmentX b, double epsilon = Epsilon)
        {
            switch (a.Degree)
            {
                case PolynomialDegree.Linear:
                    switch (b.Degree)
                    {
                        case PolynomialDegree.Linear:
                            return LineSegmentLineSegmentIntersection(b[0].X, b[0].Y, b[1].X, b[1].Y, a[0].X, a[0].Y, a[1].X, a[1].Y, epsilon);
                        case PolynomialDegree.Quadratic:
                            return LineSegmentQuadraticBezierSegmentIntersection(b[0].X, b[0].Y, b[1].X, b[1].Y, a.CurveX, a.CurveY, epsilon);
                        case PolynomialDegree.Cubic:
                            return LineSegmentCubicBezierSegmentIntersection(b[0].X, b[0].Y, b[1].X, b[1].Y, a.CurveX, a.CurveY, epsilon);
                        default:
                            return new Intersection(IntersectionStates.NoIntersection);
                    }
                case PolynomialDegree.Quadratic:
                    switch (b.Degree)
                    {
                        case PolynomialDegree.Linear:
                            return LineSegmentQuadraticBezierSegmentIntersection(b[0].X, b[0].Y, b[1].X, b[1].Y, a.CurveX, a.CurveY, epsilon);
                        case PolynomialDegree.Quadratic:
                            return QuadraticBezierSegmentQuadraticBezierSegmentIntersection(a.CurveX, a.CurveY, b.CurveX, b.CurveY, epsilon);
                        case PolynomialDegree.Cubic:
                            return QuadraticBezierSegmentCubicBezierSegmentIntersection(a.CurveX, a.CurveY, b.CurveX, b.CurveY, epsilon);
                        default:
                            return new Intersection(IntersectionStates.NoIntersection);
                    }
                case PolynomialDegree.Cubic:
                    switch (b.Degree)
                    {
                        case PolynomialDegree.Linear:
                            return LineSegmentCubicBezierSegmentIntersection(b[0].X, b[0].Y, b[1].X, b[1].Y, a.CurveX, a.CurveY, epsilon);
                        case PolynomialDegree.Quadratic:
                            return QuadraticBezierSegmentCubicBezierSegmentIntersection(b.CurveX, b.CurveY, a.CurveX, a.CurveY, epsilon);
                        case PolynomialDegree.Cubic:
                            return CubicBezierSegmentCubicBezierSegmentIntersection(a.CurveX, a.CurveY, b.CurveX, b.CurveY, epsilon);
                        default:
                            return new Intersection(IntersectionStates.NoIntersection);
                    }
                default:
                    return new Intersection(IntersectionStates.NoIntersection);
            }
        }

        /// <summary>
        /// Find the intersection of a Bézier segment and a line segment.
        /// </summary>
        /// <param name="b">The Bézier segment <paramref name="b"/>.</param>
        /// <param name="s">The line segment <paramref name="s"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this BezierSegmentX b, LineSegment s, double epsilon = Epsilon)
        {
            switch (b.Degree)
            {
                case PolynomialDegree.Linear:
                    return LineSegmentLineSegmentIntersection(s.AX, s.AY, s.BX, s.BY, b[0].X, b[0].Y, b[1].X, b[1].Y, epsilon);
                case PolynomialDegree.Quadratic:
                    return LineSegmentQuadraticBezierSegmentIntersection(s.AX, s.AY, s.BX, s.BY, b.CurveX, b.CurveY, epsilon);
                case PolynomialDegree.Cubic:
                    return LineSegmentCubicBezierSegmentIntersection(s.AX, s.AY, s.BX, s.BY, b.CurveX, b.CurveY, epsilon);
                default:
                    return new Intersection(IntersectionStates.NoIntersection);
            }
        }

        /// <summary>
        /// Find the intersection of a Bézier segment and a Quadratic Bézier.
        /// </summary>
        /// <param name="b">The Bézier segment <paramref name="b" />.</param>
        /// <param name="q">The quadratic Bézier curve segment <paramref name="q" />.</param>
        /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
        /// <returns>
        /// Returns an <see cref="Engine.Intersection" /> struct with a <see cref="Intersection.State" />, and an array of <see cref="Point2D" /> structs containing any points of intersection found.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this BezierSegmentX b, QuadraticBezier q, double epsilon = Epsilon)
        {
            switch (b.Degree)
            {
                case PolynomialDegree.Linear:
                    return LineSegmentQuadraticBezierSegmentIntersection(b[0].X, b[0].Y, b[1].X, b[1].Y, q.CurveX, q.CurveY, epsilon);
                case PolynomialDegree.Quadratic:
                    return QuadraticBezierSegmentQuadraticBezierSegmentIntersection(q.CurveX, q.CurveY, b.CurveX, b.CurveY, epsilon);
                case PolynomialDegree.Cubic:
                    return QuadraticBezierSegmentCubicBezierSegmentIntersection(q.CurveX, q.CurveY, b.CurveX, b.CurveY, epsilon);
                default:
                    return new Intersection(IntersectionStates.NoIntersection);
            }
        }

        /// <summary>
        /// Find the intersection of a Bézier segment and a Cubic Bézier.
        /// </summary>
        /// <param name="b">The Bézier segment <paramref name="b"/>.</param>
        /// <param name="c">The cubic Bézier curve segment <paramref name="c"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
                    return new Intersection(IntersectionStates.NoIntersection);
            }
        }

        /// <summary>
        /// Find the intersection of a Line segment and a Point.
        /// </summary>
        /// <param name="s">The line segment <paramref name="s"/>.</param>
        /// <param name="p">The point <paramref name="p"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, Point2D p, double epsilon = Epsilon) => PointLineSegmentIntersection(p.X, p.Y, (s?.AX).Value, s.AY, s.BX, s.BY, epsilon);

        /// <summary>
        /// Find the intersection of a Line segment and a Line.
        /// </summary>
        /// <param name="s">The line segment <paramref name="s"/>.</param>
        /// <param name="l">The line <paramref name="l"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, Line l, double epsilon = Epsilon) => LineLineSegmentIntersection((l?.Location).Value.X, l.Location.Y, l.Direction.I, l.Direction.J, (s?.AX).Value, s.AY, s.BX, s.BY, epsilon);

        /// <summary>
        /// Find the intersection of a Line segment and a Ray.
        /// </summary>
        /// <param name="s">The line segment <paramref name="s"/>.</param>
        /// <param name="r">The ray <paramref name="r"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, Ray r, double epsilon = Epsilon) => RayLineSegmentIntersection((r?.Location).Value.X, r.Location.Y, r.Direction.I, r.Direction.J, (s?.AX).Value, s.AY, s.BX, s.BY, epsilon);

        /// <summary>
        /// Find the intersection of a line segment and a Bézier segment.
        /// </summary>
        /// <param name="s">The line segment <paramref name="s"/>.</param>
        /// <param name="b">The Bézier curve segment <paramref name="b"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, BezierSegmentX b, double epsilon = Epsilon) => Intersection(b, s, epsilon);

        /// <summary>
        /// Find the intersection of two lines segments.
        /// </summary>
        /// <param name="a">The line segment <paramref name="a"/>.</param>
        /// <param name="b">The line segment <paramref name="b"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment a, LineSegment b, double epsilon = Epsilon) => LineSegmentLineSegmentIntersection((a?.A).Value.X, a.A.Y, a.B.X, a.B.Y, (b?.A).Value.X, b.A.Y, b.B.X, b.B.Y, epsilon);

        /// <summary>
        /// Find the intersection of a Line segment and a Quadratic Bézier.
        /// </summary>
        /// <param name="s">The line segment <paramref name="s"/>.</param>
        /// <param name="q">The quadratic Bézier curve segment <paramref name="q"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, QuadraticBezier q, double epsilon = Epsilon) => LineSegmentQuadraticBezierSegmentIntersection((s?.AX).Value, s.AY, s.BX, s.BY, (q?.CurveX).Value, q.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a Line segment and a Cubic Bézier.
        /// </summary>
        /// <param name="s">The line segment <paramref name="s"/>.</param>
        /// <param name="c">The cubic Bézier curve segment <paramref name="c"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, CubicBezier c, double epsilon = Epsilon) => LineSegmentCubicBezierSegmentIntersection((s?.AX).Value, s.AY, s.BX, s.BY, (c?.CurveX).Value, c.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a Line segment and a triangle.
        /// </summary>
        /// <param name="s">The line segment <paramref name="s"/>.</param>
        /// <param name="t">The rectangle <paramref name="t"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, Triangle t, double epsilon = Epsilon) => LineSegmentTriangleIntersection((s?.AX).Value, s.AY, s.BX, s.BY, (t?.A).Value.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, epsilon);

        /// <summary>
        /// Find the intersection of a Line segment and a Rectangle.
        /// </summary>
        /// <param name="s">The line segment <paramref name="s"/>.</param>
        /// <param name="a">The rectangle <paramref name="a"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, Rectangle2D a, double epsilon = Epsilon) => LineSegmentRectangleIntersection((s?.AX).Value, s.AY, s.BX, s.BY, (a?.X).Value, a.Y, a.Right, a.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Line segment and a Polygon contour.
        /// </summary>
        /// <param name="s">The line segment <paramref name="s"/>.</param>
        /// <param name="p">The polygon contour <paramref name="p"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, PolygonContour p, double epsilon = Epsilon) => LineSegmentPolygonContourIntersection((s?.AX).Value, s.AY, s.BX, s.BY, p?.Points, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a line segment.
        /// </summary>
        /// <param name="s">The line segment <paramref name="s"/>.</param>
        /// <param name="c">The circle <paramref name="c"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, Circle c, double epsilon = Epsilon) => LineSegmentCircleIntersection((s?.A).Value.X, s.A.Y, s.B.X, s.B.Y, (c?.X).Value, c.Y, c.Radius, 0, epsilon);

        /// <summary>
        /// Find the intersection of a line segment and a circular arc.
        /// </summary>
        /// <param name="s">The line segment <paramref name="s" />.</param>
        /// <param name="c">The circular arc <paramref name="c" />.</param>
        /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
        /// <returns>
        /// Returns an <see cref="Engine.Intersection" /> struct with a <see cref="Intersection.State" />, and an array of <see cref="Point2D" /> structs containing any points of intersection found.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, CircularArc c, double epsilon = Epsilon) => LineSegmentCircularArcIntersection((s?.A).Value.X, s.A.Y, (c?.X).Value, s.B.X, s.B.Y, c.Y, c.Radius, 0, c.StartAngle, c.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of a line segment and an ellipse.
        /// </summary>
        /// <param name="s">The line segment <paramref name="s"/>.</param>
        /// <param name="e">The ellipse <paramref name="e"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, Ellipse e, double epsilon = Epsilon) => LineSegmentObliqueEllipseIntersection((s?.A).Value.X, s.A.Y, s.B.X, s.B.Y, (e?.X).Value, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, epsilon);

        /// <summary>
        /// Find the intersection of a line segment and an elliptical arc.
        /// </summary>
        /// <param name="s">The line segment <paramref name="s"/>.</param>
        /// <param name="e">The elliptical arc <paramref name="e"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this LineSegment s, EllipticalArc e, double epsilon = Epsilon) => LineSegmentEllipticalArcIntersection((s?.A).Value.X, s.A.Y, s.B.X, s.B.Y, (e?.X).Value, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, e.StartAngle, e.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of a Ray and a Point.
        /// </summary>
        /// <param name="r">The ray <paramref name="r"/>.</param>
        /// <param name="p">The point <paramref name="p"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray r, Point2D p, double epsilon = Epsilon) => PointRayIntersection(p.X, p.Y, (r?.Location).Value.X, r.Location.Y, r.Direction.I, r.Direction.J, epsilon);

        /// <summary>
        /// Find the intersection of a <see cref="Ray"/> and a <see cref="LineSegment"/>.
        /// </summary>
        /// <param name="r">The ray <paramref name="r"/>.</param>
        /// <param name="s">The line segment <paramref name="s"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray r, LineSegment s, double epsilon = Epsilon) => LineRayIntersection((s?.AX).Value, s.AY, s.BX, s.BY, (r?.Location).Value.X, r.Location.Y, r.Direction.I, r.Direction.J, epsilon);

        /// <summary>
        /// Find the intersection of two Rays.
        /// </summary>
        /// <param name="a">The first ray <paramref name="a"/>.</param>
        /// <param name="b">The second ray <paramref name="b"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray a, Ray b, double epsilon = Epsilon) => RayRayIntersection((a?.Location).Value.X, a.Location.Y, a.Direction.I, a.Direction.J, (b?.Location).Value.X, b.Location.Y, b.Direction.I, b.Direction.J, epsilon);

        /// <summary>
        /// Find the intersection of a <see cref="Ray"/> and a <see cref="Line"/>.
        /// </summary>
        /// <param name="r">The ray <paramref name="r"/>.</param>
        /// <param name="l">The line <paramref name="l"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray r, Line l, double epsilon = Epsilon) => LineRayIntersection((l?.Location).Value.X, l.Location.Y, l.Direction.I, l.Direction.J, (r?.Location).Value.X, r.Location.Y, r.Direction.I, r.Direction.J, epsilon);

        /// <summary>
        /// Find the intersection of a ray and a Quadratic Bézier.
        /// </summary>
        /// <param name="r">The ray <paramref name="r"/>.</param>
        /// <param name="q">The quadratic Bézier curve segment <paramref name="q"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray r, QuadraticBezier q, double epsilon = Epsilon) => RayQuadraticBezierSegmentIntersection((r?.Location).Value.X, r.Location.Y, r.Direction.I, r.Direction.J, (q?.CurveX).Value, q.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a Ray and a Cubic Bézier.
        /// </summary>
        /// <param name="r">The ray <paramref name="r"/>.</param>
        /// <param name="c">The cubic Bézier curve segment <paramref name="c"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray r, CubicBezier c, double epsilon = Epsilon) => RayCubicBezierSegmentIntersection((r?.Location).Value.X, r.Location.Y, r.Direction.I, r.Direction.J, (c?.CurveX).Value, c.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a Ray and a triangle.
        /// </summary>
        /// <param name="s">The Ray <paramref name="s"/>.</param>
        /// <param name="t">The rectangle <paramref name="t"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray s, Triangle t, double epsilon = Epsilon) => LineSegmentTriangleIntersection((s?.Location).Value.X, s.Location.Y, s.Direction.I, s.Direction.J, (t?.A).Value.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, epsilon);

        /// <summary>
        /// Find the intersection of a <see cref="Ray"/> and a <see cref="Rectangle2D"/>.
        /// </summary>
        /// <param name="s">The line <paramref name="s"/>.</param>
        /// <param name="a">The rectangle <paramref name="a"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray s, Rectangle2D a, double epsilon = Epsilon) => RayRectangleIntersection((s?.Location).Value.X, s.Location.Y, s.Direction.I, s.Direction.J, (a?.X).Value, a.Y, a.Right, a.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Ray and a Polygon contour.
        /// </summary>
        /// <param name="r">The Ray <paramref name="r"/>.</param>
        /// <param name="p">The polygon contour <paramref name="p"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray r, PolygonContour p, double epsilon = Epsilon) => RayPolygonContourIntersection((r?.Location).Value.X, r.Location.Y, r.Direction.I, r.Direction.J, p?.Points, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a ray.
        /// </summary>
        /// <param name="r">The ray <paramref name="r"/>.</param>
        /// <param name="c">The circle <paramref name="c"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray r, Circle c, double epsilon = Epsilon) => RayCircleIntersection((r?.Location).Value.X, r.Location.Y, r.Direction.I, r.Direction.J, (c?.X).Value, c.Y, c.Radius, 0, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a ray.
        /// </summary>
        /// <param name="r">The line <paramref name="r"/>.</param>
        /// <param name="c">The circular arc <paramref name="c"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray r, CircularArc c, double epsilon = Epsilon) => RayCircularArcIntersection((r?.Location).Value.X, r.Location.Y, r.Direction.I, r.Direction.J, (c?.X).Value, c.Y, c.Radius, 0, c.StartAngle, c.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of a ray ellipse and an ellipse.
        /// </summary>
        /// <param name="r">The Ray <paramref name="r"/>.</param>
        /// <param name="e">The ellipse <paramref name="e"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray r, Ellipse e, double epsilon = Epsilon) => RayObliqueEllipseIntersection((r?.Location).Value.X, r.Location.Y, r.Direction.I, r.Direction.J, (e?.X).Value, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, epsilon);

        /// <summary>
        /// Find the intersection of a ray and an elliptical arc.
        /// </summary>
        /// <param name="r">The Ray <paramref name="r"/>.</param>
        /// <param name="e">The elliptical arc <paramref name="e"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ray r, EllipticalArc e, double epsilon = Epsilon) => LineEllipticalArcIntersection((r?.Location).Value.X, r.Location.Y, r.Direction.I, r.Direction.J, (e?.X).Value, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, e.StartAngle, e.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of a line and a point.
        /// </summary>
        /// <param name="l">The Line <paramref name="l"/>.</param>
        /// <param name="p">The Point <paramref name="p"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, Point2D p, double epsilon = Epsilon) => PointLineIntersection(p.X, p.Y, (l?.Location).Value.X, l.Location.Y, l.Direction.I, l.Direction.J, epsilon);

        /// <summary>
        /// Find the intersection of a Line and a Line segment.
        /// </summary>
        /// <param name="l">The line <paramref name="l"/>.</param>
        /// <param name="s">The line segment <paramref name="s"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, LineSegment s, double epsilon = Epsilon) => LineLineSegmentIntersection((l?.Location).Value.X, l.Location.Y, l.Direction.I, l.Direction.J, (s?.AX).Value, s.AY, s.BX, s.BY, epsilon);

        /// <summary>
        /// Find the intersection of a line and a ray.
        /// </summary>
        /// <param name="l">The line <paramref name="l"/>.</param>
        /// <param name="r">The ray <paramref name="r"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, Ray r, double epsilon = Epsilon) => LineRayIntersection((l?.Location).Value.X, l.Location.Y, l.Direction.I, l.Direction.J, (r?.Location).Value.X, r.Location.Y, r.Direction.I, r.Direction.J, epsilon);

        /// <summary>
        /// Find the intersection of to Lines.
        /// </summary>
        /// <param name="a">The first line <paramref name="a"/>.</param>
        /// <param name="b">The second line <paramref name="b"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line a, Line b, double epsilon = Epsilon) => LineLineIntersection((a?.Location).Value.X, a.Location.Y, a.Direction.I, a.Direction.J, (b?.Location).Value.X, b.Location.Y, b.Direction.I, b.Direction.J, epsilon);

        /// <summary>
        /// Find the intersection of a Line segment and a Quadratic Bézier.
        /// </summary>
        /// <param name="l">The line <paramref name="l"/>.</param>
        /// <param name="q">The quadratic Bézier curve segment <paramref name="q"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, QuadraticBezier q, double epsilon = Epsilon) => LineQuadraticBezierIntersection((l?.Location).Value.X, l.Location.Y, l.Direction.I, l.Direction.J, (q?.CurveX).Value, q.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a Line segment and a Cubic Bézier.
        /// </summary>
        /// <param name="l">The line <paramref name="l"/>.</param>
        /// <param name="c">The cubic Bézier curve segment <paramref name="c"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, CubicBezier c, double epsilon = Epsilon) => LineCubicBezierIntersection((l?.Location).Value.X, l.Location.Y, l.Direction.I, l.Direction.J, (c?.CurveX).Value, c.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a Line and a triangle.
        /// </summary>
        /// <param name="l">The line <paramref name="l"/>.</param>
        /// <param name="t">The rectangle <paramref name="t"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, Triangle t, double epsilon = Epsilon) => LineSegmentTriangleIntersection((l?.Location).Value.X, l.Location.Y, l.Direction.I, l.Direction.J, (t?.A).Value.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, epsilon);

        /// <summary>
        /// Find the intersection of a Line and a Rectangle.
        /// </summary>
        /// <param name="l">The line <paramref name="l"/>.</param>
        /// <param name="a">The rectangle <paramref name="a"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, Rectangle2D a, double epsilon = Epsilon) => LineRectangleIntersection((l?.Location).Value.X, l.Location.Y, l.Direction.I, l.Direction.J, (a?.X).Value, a.Y, a.Right, a.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Line and a Polygon contour.
        /// </summary>
        /// <param name="l">The line <paramref name="l"/>.</param>
        /// <param name="p">The polygon contour <paramref name="p"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, PolygonContour p, double epsilon = Epsilon) => LinePolygonContourIntersection((l?.Location).Value.X, l.Location.Y, l.Direction.I, l.Direction.J, p?.Points, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a line.
        /// </summary>
        /// <param name="l">The line <paramref name="l"/>.</param>
        /// <param name="c">The circle <paramref name="c"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, Circle c, double epsilon = Epsilon) => LineCircleIntersection((l?.Location).Value.X, l.Location.Y, l.Direction.I, l.Direction.J, (c?.X).Value, c.Y, c.Radius, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a line.
        /// </summary>
        /// <param name="l">The line <paramref name="l"/>.</param>
        /// <param name="c">The circular arc <paramref name="c"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, CircularArc c, double epsilon = Epsilon) => LineCircularArcIntersection((l?.Location).Value.X, l.Location.Y, l.Direction.I, l.Direction.J, (c?.X).Value, c.Y, c.Radius, 0, c.StartAngle, c.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of a line and an ellipse.
        /// </summary>
        /// <param name="l">The line <paramref name="l"/>.</param>
        /// <param name="e">The ellipse <paramref name="e"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, Ellipse e, double epsilon = Epsilon) => LineObliqueEllipseIntersection((l?.Location).Value.X, l.Location.Y, l.Direction.I, l.Direction.J, (e?.X).Value, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, epsilon);

        /// <summary>
        /// Find the intersection of a line and an elliptical arc.
        /// </summary>
        /// <param name="l">The line <paramref name="l"/>.</param>
        /// <param name="e">The elliptical arc <paramref name="e"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Line l, EllipticalArc e, double epsilon = Epsilon)
            => LineEllipticalArcIntersection((l?.Location).Value.X, l.Location.Y, l.Direction.I, l.Direction.J, (e?.X).Value, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, e.StartAngle, e.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bézier and a Bézier segment.
        /// </summary>
        /// <param name="q">The quadratic Bézier curve segment <paramref name="q"/>.</param>
        /// <param name="b">The Bézier segment <paramref name="b"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier q, BezierSegmentX b, double epsilon = Epsilon) => Intersection(b, q, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bézier and a Line segment.
        /// </summary>
        /// <param name="q">The quadratic Bézier curve segment <paramref name="q"/>.</param>
        /// <param name="l">The line segment <paramref name="l"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier q, LineSegment l, double epsilon = Epsilon) => LineSegmentQuadraticBezierSegmentIntersection((l?.AX).Value, l.AY, l.BX, l.BY, (q?.CurveX).Value, q.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a ray and a Quadratic Bézier.
        /// </summary>
        /// <param name="r">The ray <paramref name="r"/>.</param>
        /// <param name="q">The quadratic Bézier curve segment <paramref name="q"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier q, Ray r, double epsilon = Epsilon) => RayQuadraticBezierSegmentIntersection((r?.Location).Value.X, r.Location.Y, r.Direction.I, r.Direction.J, (q?.CurveX).Value, q.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bézier and a Line segment.
        /// </summary>
        /// <param name="q">The quadratic Bézier curve segment <paramref name="q"/>.</param>
        /// <param name="l">The line <paramref name="l"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier q, Line l, double epsilon = Epsilon) => LineQuadraticBezierIntersection((l?.Location).Value.X, l.Location.Y, l.Direction.I, l.Direction.J, (q?.CurveX).Value, q.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of two Quadratic Bézier curves.
        /// </summary>
        /// <param name="a">The <paramref name="a"/>.</param>
        /// <param name="b">The <paramref name="b"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier a, QuadraticBezier b, double epsilon = Epsilon) => QuadraticBezierSegmentQuadraticBezierSegmentIntersection((a?.CurveX).Value, a.CurveY, (b?.CurveX).Value, b.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bézier and a Cubic Bézier.
        /// </summary>
        /// <param name="q">The <paramref name="q"/>.</param>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier q, CubicBezier c, double epsilon = Epsilon) => QuadraticBezierSegmentCubicBezierSegmentIntersection((q?.CurveX).Value, q.CurveY, (c?.CurveX).Value, c.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bézier and a triangle.
        /// </summary>
        /// <param name="q">The b.</param>
        /// <param name="t">The t.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier q, Triangle t, double epsilon = Epsilon) => QuadraticBezierSegmentTriangleIntersection((q?.CurveX).Value, q.CurveY, (t?.A).Value.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bézier and a Rectangle.
        /// </summary>
        /// <param name="q">The <paramref name="q"/>.</param>
        /// <param name="a">The <paramref name="a"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier q, Rectangle2D a, double epsilon = Epsilon) => QuadraticBezierSegmentRectangleIntersection((q?.CurveX).Value, q.CurveY, (a?.X).Value, a.Y, a.Right, a.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bézier and a Polygon Contour.
        /// </summary>
        /// <param name="q">The <paramref name="q"/>.</param>
        /// <param name="p">The <paramref name="p"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier q, PolygonContour p, double epsilon = Epsilon) => QuadraticBezierSegmentPolygonContourIntersection((q?.CurveX).Value, q.CurveY, p?.Points, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bézier and a Circle.
        /// </summary>
        /// <param name="q">The <paramref name="q"/>.</param>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier q, Circle c, double epsilon = Epsilon) => QuadraticBezierSegmentOrthogonalEllipseIntersection((q?.CurveX).Value, q.CurveY, (c?.X).Value, c.Y, c.Radius, c.Radius, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bézier and an orthogonal Ellipse.
        /// </summary>
        /// <param name="q">The <paramref name="q"/>.</param>
        /// <param name="e">The <paramref name="e"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this QuadraticBezier q, Ellipse e, double epsilon = Epsilon) => QuadraticBezierSegmentObliqueEllipseIntersection(q.AX, q.AY, q.BX, q.BY, q.CX, q.CY, (e?.X).Value, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bézier and a Bézier segment.
        /// </summary>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="b">The <paramref name="b"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier c, BezierSegmentX b, double epsilon = Epsilon) => Intersection(b, c, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bézier and a Line segment.
        /// </summary>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="l">The <paramref name="l"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier c, LineSegment l, double epsilon = Epsilon) => LineSegmentCubicBezierSegmentIntersection((l?.AX).Value, l.AY, l.BX, l.BY, (c?.CurveX).Value, c.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a Ray and a Cubic Bézier.
        /// </summary>
        /// <param name="c">The cubic Bézier curve segment <paramref name="c"/>.</param>
        /// <param name="r">The ray <paramref name="r"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier c, Ray r, double epsilon = Epsilon) => RayCubicBezierSegmentIntersection((r?.Location).Value.X, r.Location.Y, r.Direction.I, r.Direction.J, (c?.CurveX).Value, c.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bézier and a Line segment.
        /// </summary>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="l">The <paramref name="l"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier c, Line l, double epsilon = Epsilon) => LineCubicBezierIntersection((l?.Location).Value.X, l.Location.Y, l.Direction.I, l.Direction.J, (c?.CurveX).Value, c.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bézier and a Quadratic Bézier.
        /// </summary>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="q">The <paramref name="q"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier c, QuadraticBezier q, double epsilon = Epsilon) => QuadraticBezierSegmentCubicBezierSegmentIntersection((q?.CurveX).Value, q.CurveY, (c?.CurveX).Value, c.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of two Cubic Bézier curves.
        /// </summary>
        /// <param name="a">The <paramref name="a"/>.</param>
        /// <param name="b">The <paramref name="b"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier a, CubicBezier b, double epsilon = Epsilon) => CubicBezierSegmentCubicBezierSegmentIntersection((a?.CurveX).Value, a.CurveY, (b?.CurveX).Value, b.CurveY, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bézier and a Triangle.
        /// </summary>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="t">The <paramref name="t"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier c, Triangle t, double epsilon = Epsilon) => CubicBezierSegmentTriangleIntersection((c?.CurveX).Value, c.CurveY, (t?.A).Value.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bézier and a Rectangle.
        /// </summary>
        /// <param name="c">The b.</param>
        /// <param name="r">The r.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier c, Rectangle2D r, double epsilon = Epsilon) => CubicBezierSegmentRectangleIntersection((c?.CurveX).Value, c.CurveY, (r?.X).Value, r.Y, r.Right, r.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bézier and a Polygon Contour.
        /// </summary>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="p">The <paramref name="p"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier c, PolygonContour p, double epsilon = Epsilon) => CubicBezierSegmentPolygonIntersection((c?.CurveX).Value, c.CurveY, p?.Points, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bézier and a Circle.
        /// </summary>
        /// <param name="b">The <paramref name="b"/>.</param>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier b, Circle c, double epsilon = Epsilon) => CubicBezierSegmentOrthogonalEllipseIntersection((b?.CurveX).Value, b.CurveY, (c?.X).Value, c.Y, c.Radius, c.Radius, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bézier and an orthogonal Ellipse.
        /// </summary>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="e">The <paramref name="e"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CubicBezier c, Ellipse e, double epsilon = Epsilon) => CubicBezierSegmentOrthogonalEllipseIntersection((c?.CurveX).Value, c.CurveY, (e?.X).Value, e.Y, e.RX, e.RY, epsilon);

        /// <summary>
        /// Find the intersection of a triangle and a Line segment.
        /// </summary>
        /// <param name="t">The triangle <paramref name="t"/>.</param>
        /// <param name="s">The line segment <paramref name="s"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Triangle t, LineSegment s, double epsilon = Epsilon) => LineSegmentTriangleIntersection((s?.AX).Value, s.AY, s.BX, s.BY, (t?.A).Value.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, epsilon);

        /// <summary>
        /// Find the intersection of a triangle and a Ray.
        /// </summary>
        /// <param name="t">The triangle <paramref name="t"/>.</param>
        /// <param name="r">The Ray <paramref name="r"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Triangle t, Ray r, double epsilon = Epsilon) => RayTriangleIntersection((r?.Location).Value.X, r.Location.Y, r.Direction.I, r.Direction.J, (t?.A).Value.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, epsilon);

        /// <summary>
        /// Find the intersection of a triangle and a Line.
        /// </summary>
        /// <param name="l">The line segment <paramref name="l"/>.</param>
        /// <param name="t">The triangle <paramref name="t"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Triangle t, Line l, double epsilon = Epsilon) => LineSegmentTriangleIntersection((l?.Location).Value.X, l.Location.Y, l.Direction.I, l.Direction.J, (t?.A).Value.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, epsilon);

        /// <summary>
        /// Find the intersection of a Quadratic Bézier and a triangle.
        /// </summary>
        /// <param name="t">The <paramref name="t"/>.</param>
        /// <param name="q">The <paramref name="q"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Triangle t, QuadraticBezier q, double epsilon = Epsilon) => QuadraticBezierSegmentTriangleIntersection((q?.CurveX).Value, q.CurveY, (t?.A).Value.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, epsilon);

        /// <summary>
        /// Find the intersection of a Cubic Bézier and a Triangle.
        /// </summary>
        /// <param name="t">The <paramref name="t"/>.</param>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Triangle t, CubicBezier c, double epsilon = Epsilon) => CubicBezierSegmentTriangleIntersection((c?.CurveX).Value, c.CurveY, (t?.A).Value.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, epsilon);

        /// <summary>
        /// Find the intersection of two triangles.
        /// </summary>
        /// <param name="a">The <paramref name="a"/>.</param>
        /// <param name="b">The <paramref name="b"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Triangle a, Triangle b, double epsilon = Epsilon) => TriangleTriangleIntersection((a?.A).Value.X, a.A.Y, a.B.X, a.B.Y, a.C.X, a.C.Y, (b?.A).Value.X, b.A.Y, b.B.X, b.B.Y, b.C.X, b.C.Y, epsilon);

        /// <summary>
        /// Find the intersection of a triangle and a rectangle.
        /// </summary>
        /// <param name="t">The <paramref name="t"/>.</param>
        /// <param name="a">The <paramref name="a"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Triangle t, Rectangle2D a, double epsilon = Epsilon) => TriangleRectangleIntersection((t?.A).Value.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, (a?.X).Value, a.Y, a.Right, a.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Polygon contour and a Rectangle.
        /// </summary>
        /// <param name="t">The <paramref name="t"/>.</param>
        /// <param name="p">The <paramref name="p"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Triangle t, PolygonContour p, double epsilon = Epsilon) => TrianglePolygonContourIntersection((t?.A).Value.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, p?.Points, epsilon);

        /// <summary>
        /// Find the intersection of a triangle and a circle.
        /// </summary>
        /// <param name="t">The <paramref name="t"/>.</param>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Triangle t, Circle c, double epsilon = Epsilon) => TriangleCircleIntersection((t?.A).Value.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, (c?.X).Value, c.Y, c.Radius, 0d, epsilon);

        /// <summary>
        /// Find the points of the intersection of an orthogonal ellipse and a triangle.
        /// </summary>
        /// <param name="t">The <paramref name="t"/>.</param>
        /// <param name="e">The <paramref name="e"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Triangle t, Ellipse e, double epsilon = Epsilon) => TriangleObliqueEllipseIntersection((e?.X).Value, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, (t?.A).Value.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, epsilon);

        /// <summary>
        /// Find the intersection of a Rectangle and a Line segment.
        /// </summary>
        /// <param name="a">The <paramref name="a"/>.</param>
        /// <param name="s">The <paramref name="s"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D a, LineSegment s, double epsilon = Epsilon) => LineSegmentRectangleIntersection((s?.AX).Value, s.AY, s.BX, s.BY, (a?.X).Value, a.Y, a.Right, a.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Rectangle and a line.
        /// </summary>
        /// <param name="a">The <paramref name="a"/>.</param>
        /// <param name="l">The <paramref name="l"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D a, Line l, double epsilon = Epsilon) => LineRectangleIntersection((l?.Location).Value.X, l.Location.Y, l.Direction.I, l.Direction.J, (a?.X).Value, a.Y, a.Right, a.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a <see cref="Ray"/> and a <see cref="Rectangle2D"/>.
        /// </summary>
        /// <param name="a">The rectangle <paramref name="a"/>.</param>
        /// <param name="r">The <see cref="Ray"/> <paramref name="r"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D a, Ray r, double epsilon = Epsilon) => RayRectangleIntersection((r?.Location).Value.X, r.Location.Y, r.Direction.I, r.Direction.J, (a?.X).Value, a.Y, a.Right, a.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Rectangle and a Quadratic Bézier.
        /// </summary>
        /// <param name="a">The <paramref name="a"/>.</param>
        /// <param name="q">The <paramref name="q"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D a, QuadraticBezier q, double epsilon = Epsilon) => QuadraticBezierSegmentRectangleIntersection((q?.CurveX).Value, q.CurveY, (a?.X).Value, a.Y, a.Right, a.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Rectangle and a Cubic Bézier.
        /// </summary>
        /// <param name="a">The <paramref name="a"/>.</param>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D a, CubicBezier c, double epsilon = Epsilon) => CubicBezierSegmentRectangleIntersection((c?.CurveX).Value, c.CurveY, (a?.X).Value, a.Y, a.Right, a.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Rectangle and a triangle.
        /// </summary>
        /// <param name="a">The <paramref name="a"/>.</param>
        /// <param name="t">The <paramref name="t"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D a, Triangle t, double epsilon = Epsilon) => TriangleRectangleIntersection((t?.A).Value.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, (a?.X).Value, a.Y, a.Right, a.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of two Rectangles.
        /// </summary>
        /// <param name="a">The <paramref name="a"/>.</param>
        /// <param name="b">The <paramref name="b"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D a, Rectangle2D b, double epsilon = Epsilon) => RectangleRectangleIntersection((a?.X).Value, a.Y, a.Right, a.Bottom, (b?.X).Value, b.Y, b.Right, b.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Rectangle and a Polygon contour.
        /// </summary>
        /// <param name="a">The <paramref name="a"/>.</param>
        /// <param name="p">The <paramref name="p"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D a, PolygonContour p, double epsilon = Epsilon) => PolygonContourRectangleIntersection(p?.Points, (a?.X).Value, a.Y, a.Right, a.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Rectangle and a Circle.
        /// </summary>
        /// <param name="a">The <paramref name="a"/>.</param>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D a, Circle c, double epsilon = Epsilon) => CircleRectangleIntersection((c?.X).Value, c.Y, c.Radius, 0, (a?.X).Value, a.Y, a.Right, a.Bottom, epsilon);

        /// <summary>
        /// Find the points of the intersection of an orthogonal ellipse and a rectangle.
        /// </summary>
        /// <param name="a">The <paramref name="a"/>.</param>
        /// <param name="e">The <paramref name="e"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Rectangle2D a, Ellipse e, double epsilon = Epsilon) => ObliqueEllipseRectangleIntersection((e?.X).Value, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, (a?.X).Value, a.Y, a.Right, a.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Polygon contour and a Line segment.
        /// </summary>
        /// <param name="p">The <paramref name="p"/>.</param>
        /// <param name="s">The <paramref name="s"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p, LineSegment s, double epsilon = Epsilon) => LineSegmentPolygonContourIntersection((s?.AX).Value, s.AY, s.BX, s.BY, p?.Points, epsilon);

        /// <summary>
        /// Find the intersection of a Polygon contour and a Ray.
        /// </summary>
        /// <param name="p">The <paramref name="p"/>.</param>
        /// <param name="r">The <paramref name="r"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p, Ray r, double epsilon = Epsilon) => RayPolygonContourIntersection((r?.Location).Value.X, r.Location.Y, r.Direction.I, r.Direction.J, p?.Points, epsilon);

        /// <summary>
        /// Find the intersection of a Polygon contour and a Line.
        /// </summary>
        /// <param name="p">The <paramref name="p"/>.</param>
        /// <param name="l">The <paramref name="l"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p, Line l, double epsilon = Epsilon) => LinePolygonContourIntersection((l?.Location).Value.X, l.Location.Y, l.Direction.I, l.Direction.J, p?.Points, epsilon);

        /// <summary>
        /// Find the intersection of a Polygon contour and a Quadratic Bézier.
        /// </summary>
        /// <param name="p">The <paramref name="p"/>.</param>
        /// <param name="q">The <paramref name="q"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p, QuadraticBezier q, double epsilon = Epsilon) => QuadraticBezierSegmentPolygonContourIntersection((q?.CurveX).Value, q.CurveY, p?.Points, epsilon);

        /// <summary>
        /// Find the intersection of a Polygon Contour and a Cubic Bézier.
        /// </summary>
        /// <param name="p">The <paramref name="p"/>.</param>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p, CubicBezier c, double epsilon = Epsilon) => CubicBezierSegmentPolygonIntersection((c?.CurveX).Value, c.CurveY, p?.Points, epsilon);

        /// <summary>
        /// Find the intersection of a Polygon contour and a Rectangle.
        /// </summary>
        /// <param name="p">The <paramref name="p"/>.</param>
        /// <param name="t">The <paramref name="t"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p, Triangle t, double epsilon = Epsilon) => TrianglePolygonContourIntersection((t?.A).Value.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, p?.Points, epsilon);

        /// <summary>
        /// Find the intersection of a Polygon contour and a Rectangle.
        /// </summary>
        /// <param name="p">The <paramref name="p"/>.</param>
        /// <param name="a">The <paramref name="a"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p, Rectangle2D a, double epsilon = Epsilon) => PolygonContourRectangleIntersection(p?.Points, (a?.X).Value, a.Y, a.Right, a.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of two Polygon contours.
        /// </summary>
        /// <param name="a">The <paramref name="a"/>.</param>
        /// <param name="b">The <paramref name="b"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour a, PolygonContour b, double epsilon = Epsilon) => PolygonContourPolygonContourIntersection(a?.Points, b?.Points, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a polygon.
        /// </summary>
        /// <param name="p">The <paramref name="p"/>.</param>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p, Circle c, double epsilon = Epsilon) => CirclePolygonContourIntersection((c?.X).Value, c.Y, c.Radius, 0, p?.Points, epsilon);

        /// <summary>
        /// Find the intersection of an ellipse and a polygon.
        /// </summary>
        /// <param name="p">The <paramref name="p"/>.</param>
        /// <param name="e">The <paramref name="e"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this PolygonContour p, Ellipse e, double epsilon = Epsilon) => ObliqueEllipsePolygonContourIntersection((e?.X).Value, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, p?.Points, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a line segment.
        /// </summary>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="s">The <paramref name="s"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, LineSegment s, double epsilon = Epsilon) => LineSegmentCircleIntersection((s?.A).Value.X, s.A.Y, s.B.X, s.B.Y, (c?.X).Value, c.Y, c.Radius, 0, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a ray.
        /// </summary>
        /// <param name="c">The circle <paramref name="c"/>.</param>
        /// <param name="r">The ray <paramref name="r"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, Ray r, double epsilon = Epsilon) => RayCircleIntersection((r?.Location).Value.X, r.Location.Y, r.Direction.I, r.Direction.J, (c?.X).Value, c.Y, c.Radius, 0, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a line.
        /// </summary>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="l">The <paramref name="l"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, Line l, double epsilon = Epsilon) => LineCircleIntersection((l?.Location).Value.X, l.Location.Y, l.Direction.I, l.Direction.J, (c?.X).Value, c.Y, c.Radius, epsilon);

        /// <summary>
        /// Find the intersection of a Circle and a Quadratic Bézier.
        /// </summary>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="q">The <paramref name="q"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, QuadraticBezier q, double epsilon = Epsilon) => QuadraticBezierSegmentOrthogonalEllipseIntersection((q?.CurveX).Value, q.CurveY, (c?.X).Value, c.Y, c.Radius, c.Radius, epsilon);

        /// <summary>
        /// Find the intersection of a Circle and a Cubic Bézier.
        /// </summary>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="b">The <paramref name="b"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, CubicBezier b, double epsilon = Epsilon) => CubicBezierSegmentOrthogonalEllipseIntersection((b?.CurveX).Value, b.CurveY, (c?.X).Value, c.Y, c.Radius, c.Radius, epsilon);

        /// <summary>
        /// Find the intersection of a Circle and a triangle.
        /// </summary>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="t">The <paramref name="t"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, Triangle t, double epsilon = Epsilon) => TriangleCircleIntersection((t?.A).Value.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, (c?.X).Value, c.Y, c.Radius, 0d, epsilon);

        /// <summary>
        /// Find the intersection of a Circle and a Rectangle.
        /// </summary>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="a">The <paramref name="a"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, Rectangle2D a, double epsilon = Epsilon) => CircleRectangleIntersection((c?.X).Value, c.Y, c.Radius, 0, (a?.X).Value, a.Y, a.Right, a.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of a Circle and a polygon contour.
        /// </summary>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="p">The <paramref name="p"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, PolygonContour p, double epsilon = Epsilon) => CirclePolygonContourIntersection((c?.X).Value, c.Y, c.Radius, 0, p?.Points, epsilon);

        /// <summary>
        /// Find the intersection between two circles.
        /// </summary>
        /// <param name="a">The <paramref name="a"/>.</param>
        /// <param name="b">The <paramref name="b"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle a, Circle b, double epsilon = Epsilon) => CircleCircleIntersection((a?.X).Value, a.Y, a.Radius, (b?.X).Value, b.Y, b.Radius, epsilon);

        /// <summary>
        /// Find the intersection of a Circle and an orthogonal Ellipse.
        /// </summary>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="e">The <paramref name="e"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Circle c, Ellipse e, double epsilon = Epsilon) => ObliqueEllipseObliqueEllipseIntersection((c?.Center).Value.X, c.Center.Y, c.Radius, c.Radius, 0d, (e?.Center).Value.X, e.Center.Y, e.RX, e.RY, e.Angle, epsilon);

        /// <summary>
        /// Find the intersection of an circular arc and a line segment.
        /// </summary>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="s">The <paramref name="s"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CircularArc c, LineSegment s, double epsilon = Epsilon) => LineSegmentCircularArcIntersection((s?.A).Value.X, s.A.Y, (c?.X).Value, s.B.X, s.B.Y, c.Y, c.Radius, 0, c.StartAngle, c.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a ray.
        /// </summary>
        /// <param name="r">The Ray <paramref name="r"/>.</param>
        /// <param name="c">The circular arc <paramref name="c"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CircularArc c, Ray r, double epsilon = Epsilon) => RayCircularArcIntersection((r?.Location).Value.X, r.Location.Y, r.Direction.I, r.Direction.J, (c?.X).Value, c.Y, c.Radius, 0, c.StartAngle, c.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of a circle and a line.
        /// </summary>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="s">The <paramref name="s"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this CircularArc c, Line s, double epsilon = Epsilon) => LineCircularArcIntersection((s?.Location).Value.X, s.Location.Y, s.Direction.I, s.Direction.J, (c?.X).Value, c.Y, c.Radius, 0, c.StartAngle, c.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of an ellipse and a line segment.
        /// </summary>
        /// <param name="e">The <paramref name="e"/>.</param>
        /// <param name="s">The <paramref name="s"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, LineSegment s, double epsilon = Epsilon) => LineSegmentObliqueEllipseIntersection((s?.A).Value.X, s.A.Y, s.B.X, s.B.Y, (e?.X).Value, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, epsilon);

        /// <summary>
        /// Find the intersection of an ellipse and a ray.
        /// </summary>
        /// <param name="r">The Ray <paramref name="r"/>.</param>
        /// <param name="e">The ellipse <paramref name="e"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, Ray r, double epsilon = Epsilon) => RayObliqueEllipseIntersection((r?.Location).Value.X, r.Location.Y, r.Direction.I, r.Direction.J, (e?.X).Value, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, epsilon);

        /// <summary>
        /// Find the intersection of an ellipse and a line segment.
        /// </summary>
        /// <param name="e">The <paramref name="e"/>.</param>
        /// <param name="l">The <paramref name="l"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, Line l, double epsilon = Epsilon) => LineObliqueEllipseIntersection((l?.Location).Value.X, l.Location.Y, l.Direction.I, l.Direction.J, (e?.X).Value, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, epsilon);

        /// <summary>
        /// Find the intersection of an orthogonal Ellipse and a Quadratic Bézier.
        /// </summary>
        /// <param name="e">The <paramref name="e"/>.</param>
        /// <param name="q">The <paramref name="q"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, QuadraticBezier q, double epsilon = Epsilon) => QuadraticBezierSegmentObliqueEllipseIntersection((q?.AX).Value, q.AY, q.BX, q.BY, q.CX, q.CY, (e?.X).Value, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, epsilon);

        /// <summary>
        /// Find the intersection of an orthogonal ellipse and a Cubic Bézier.
        /// </summary>
        /// <param name="e">The <paramref name="e"/>.</param>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, CubicBezier c, double epsilon = Epsilon) => CubicBezierSegmentObliqueEllipseIntersection((c?.AX).Value, c.AY, c.BX, c.BY, c.CX, c.CY, c.DX, c.DY, (e?.X).Value, e.Y, e.RX, e.RY, Cos(e.Angle), Sin(e.Angle), epsilon); //CubicBezierSegmentObliqueEllipseIntersection((c?.CurveX).Value, c.CurveY, (e?.X).Value, e.Y, e.RX, e.RY, Cos(e.Angle), Sin(e.Angle), epsilon);

        /// <summary>
        /// Find the points of the intersection of an ellipse and a triangle.
        /// </summary>
        /// <param name="e">The <paramref name="e"/>.</param>
        /// <param name="t">The <paramref name="t"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, Triangle t, double epsilon = Epsilon) => TriangleObliqueEllipseIntersection((e?.X).Value, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, (t?.A).Value.X, t.A.Y, t.B.X, t.B.Y, t.C.X, t.C.Y, epsilon);

        /// <summary>
        /// Find the intersection of an ellipse and a rectangle.
        /// </summary>
        /// <param name="e">The <paramref name="e"/>.</param>
        /// <param name="a">The <paramref name="a"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, Rectangle2D a, double epsilon = Epsilon) => ObliqueEllipseRectangleIntersection((e?.X).Value, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, (a?.X).Value, a.Y, a.Right, a.Bottom, epsilon);

        /// <summary>
        /// Find the intersection of an ellipse and a polygon.
        /// </summary>
        /// <param name="e">The <paramref name="e"/>.</param>
        /// <param name="p">The <paramref name="p"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, PolygonContour p, double epsilon = Epsilon) => ObliqueEllipsePolygonContourIntersection((e?.X).Value, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, p?.Points, epsilon);

        /// <summary>
        /// Find the intersection of an orthogonal Ellipse and a Circle.
        /// </summary>
        /// <param name="e">The <paramref name="e"/>.</param>
        /// <param name="c">The <paramref name="c"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse e, Circle c, double epsilon = Epsilon) => ObliqueEllipseObliqueEllipseIntersection((c?.Center).Value.X, c.Center.Y, c.Radius, c.Radius, 0d, (e?.Center).Value.X, e.Center.Y, e.RX, e.RY, e.Angle, epsilon);

        /// <summary>
        /// Find the intersection of two orthogonal Ellipses.
        /// </summary>
        /// <param name="a">The <paramref name="a"/>.</param>
        /// <param name="b">The <paramref name="b"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this Ellipse a, Ellipse b, double epsilon = Epsilon) => ObliqueEllipseObliqueEllipseIntersection((a?.Center).Value.X, a.Center.Y, a.RX, a.RY, a.Angle, (b?.Center).Value.X, b.Center.Y, b.RX, b.RY, b.Angle, epsilon);

        /// <summary>
        /// Find the intersection of an elliptical arc and a line segment.
        /// </summary>
        /// <param name="e">The <paramref name="e"/>.</param>
        /// <param name="s">The <paramref name="s"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this EllipticalArc e, LineSegment s, double epsilon = Epsilon) => LineSegmentEllipticalArcIntersection((s?.A).Value.X, s.A.Y, s.B.X, s.B.Y, (e?.X).Value, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, e.StartAngle, e.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of an elliptical arc and a ray.
        /// </summary>
        /// <param name="e">The elliptical arc <paramref name="e"/>.</param>
        /// <param name="r">The ray <paramref name="r"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this EllipticalArc e, Ray r, double epsilon = Epsilon) => LineEllipticalArcIntersection((r?.Location).Value.X, r.Location.Y, r.Direction.I, r.Direction.J, (e?.X).Value, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, e.StartAngle, e.SweepAngle, epsilon);

        /// <summary>
        /// Find the intersection of an elliptical arc and a line segment.
        /// </summary>
        /// <param name="e">The <paramref name="e"/>.</param>
        /// <param name="l">The <paramref name="l"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection Intersection(this EllipticalArc e, Line l, double epsilon = Epsilon) => LineEllipticalArcIntersection((l?.Location).Value.X, l.Location.Y, l.Direction.I, l.Direction.J, (e?.X).Value, e.Y, e.RX, e.RY, e.CosAngle, e.SinAngle, e.StartAngle, e.SweepAngle, epsilon);
        #endregion Intersection Extension Method Overloads

        #region Intersection Methods
        /// <summary>
        /// Find the intersection point between two points.
        /// </summary>
        /// <param name="p0X">The <paramref name="p0X"/>.</param>
        /// <param name="p0Y">The <paramref name="p0Y"/>.</param>
        /// <param name="p1X">The <paramref name="p1X"/>.</param>
        /// <param name="p1Y">The <paramref name="p1Y"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection PointPointIntersection(
            double p0X, double p0Y,
            double p1X, double p1Y,
            double epsilon = Epsilon)
        {
            _ = epsilon;
            return PointPointIntersects(p0X, p0Y, p1X, p1Y)
                       ? new Intersection(IntersectionStates.Intersection, new Point2D(p0X, p0Y))
                       : new Intersection(IntersectionStates.NoIntersection);
        }

        /// <summary>
        /// Find the intersection of a point and a line.
        /// </summary>
        /// <param name="pX">The <paramref name="pX"/>.</param>
        /// <param name="pY">The <paramref name="pY"/>.</param>
        /// <param name="lx">The <paramref name="lx"/>.</param>
        /// <param name="ly">The <paramref name="ly"/>.</param>
        /// <param name="i">The <paramref name="i"/>.</param>
        /// <param name="j">The <paramref name="j"/>.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection PointLineIntersection(
            double pX, double pY,
            double lx, double ly, double i, double j,
            double epsilon = Epsilon)
        {
            _ = epsilon;
            var result = new Intersection(IntersectionStates.NoIntersection);
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
                result.State |= IntersectionStates.Intersection;
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
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection PointRayIntersection(
            double pX, double pY,
            double lX, double lY, double i, double j,
            double epsilon = Epsilon)
            => PointRayIntersects(pX, pY, lX, lY, i, j, epsilon)
            ? new Intersection(IntersectionStates.Intersection, new Point2D(pX, pY))
            : new Intersection(IntersectionStates.NoIntersection);

        /// <summary>
        /// Find the intersection between a point and a line segment.
        /// </summary>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <param name="lAX"></param>
        /// <param name="lAY"></param>
        /// <param name="lBX"></param>
        /// <param name="lBY"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection PointLineSegmentIntersection(
            double pX, double pY,
            double lAX, double lAY, double lBX, double lBY,
            double epsilon = Epsilon)
            => PointLineSegmentIntersects(pX, pY, lAX, lAY, lBX, lBY, epsilon)
            ? new Intersection(IntersectionStates.Intersection, new Point2D(pX, pY))
            : new Intersection(IntersectionStates.NoIntersection);

        /// <summary>
        /// Find the intersection between a Point and a triangle.
        /// </summary>
        /// <param name="lx"></param>
        /// <param name="ly"></param>
        /// <param name="tx0"></param>
        /// <param name="ty0"></param>
        /// <param name="tx1"></param>
        /// <param name="ty1"></param>
        /// <param name="tx2"></param>
        /// <param name="ty2"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection PointTriangleIntersection(
            double lx, double ly,
            double tx0, double ty0, double tx1, double ty1, double tx2, double ty2,
            double epsilon = Epsilon)
        {
            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();

            intersections.UnionWith(PointLineSegmentIntersection(lx, ly, tx0, ty0, tx1, ty1, epsilon).Points);
            intersections.UnionWith(PointLineSegmentIntersection(lx, ly, tx1, ty1, tx2, ty2, epsilon).Points);
            intersections.UnionWith(PointLineSegmentIntersection(lx, ly, tx0, ty0, tx2, ty2, epsilon).Points);

            // ToDo: Return IntersectionState.Inside if all of the points of one rectangle are inside the other rectangle.

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a point and a rectangle.
        /// </summary>
        /// <param name="a1X"></param>
        /// <param name="a1Y"></param>
        /// <param name="r1X"></param>
        /// <param name="r1Y"></param>
        /// <param name="r2X"></param>
        /// <param name="r2Y"></param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection PointRectangleIntersection(
            double a1X, double a1Y,
            double r1X, double r1Y, double r2X, double r2Y,
            double epsilon = Epsilon)
        {
            var (minX, minY) = MinPoint(r1X, r1Y, r2X, r2Y);
            var (maxX, maxY) = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(maxX, minY);
            var bottomLeft = new Point2D(minX, maxY);

            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            intersections.UnionWith(PointLineSegmentIntersection(a1X, a1Y, minX, minY, topRight.X, topRight.Y, epsilon).Points);
            intersections.UnionWith(PointLineSegmentIntersection(a1X, a1Y, topRight.X, topRight.Y, maxX, maxY, epsilon).Points);
            intersections.UnionWith(PointLineSegmentIntersection(a1X, a1Y, maxX, maxY, bottomLeft.X, bottomLeft.Y, epsilon).Points);
            intersections.UnionWith(PointLineSegmentIntersection(a1X, a1Y, bottomLeft.X, bottomLeft.Y, minX, minY, epsilon).Points);

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a point and a polyline.
        /// </summary>
        /// <param name="a1X">The a1X.</param>
        /// <param name="a1Y">The a1Y.</param>
        /// <param name="points">The points.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection PointPolylineIntersection(
            double a1X, double a1Y,
            List<Point2D> points,
            double epsilon = Epsilon)
        {
            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            var length = points.Count;

            var p = points[0];
            for (var i = 1; i < length; ++i)
            {
                var b2 = points[i];

                intersections.UnionWith(PointLineSegmentIntersection(a1X, a1Y, p.X, p.Y, b2.X, b2.Y, epsilon).Points);

                p = b2;
            }

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a point and a Polygon Contour.
        /// </summary> 
        /// <param name="a1X">The a1X.</param>
        /// <param name="a1Y">The a1Y.</param>
        /// <param name="points">The points.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection PointPolygonContourIntersection(
            double a1X, double a1Y,
            List<Point2D> points,
            double epsilon = Epsilon)
        {
            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            var length = points.Count;

            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            var b1 = points[length - 1];
            for (var i = 0; i < length; ++i)
            {
                var b2 = points[i];

                intersections.UnionWith(PointLineSegmentIntersection(a1X, a1Y, b1.X, b1.Y, b2.X, b2.Y, epsilon).Points);

                b1 = b2;
            }

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

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
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            var result = new Intersection(IntersectionStates.NoIntersection);

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
            var t = (((lx1 - lx0) * lj1) + ((ly0 - ly1) * li1)) / determinant;

            // Return the intersection point.
            result.AppendPoint(new Point2D(lx0 + (t * li0), ly0 + (t * lj0)));
            result.State |= IntersectionStates.Intersection;
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
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            var result = new Intersection(IntersectionStates.Intersection);

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
                    result.State |= IntersectionStates.Coincident | IntersectionStates.Parallel | IntersectionStates.Intersection;
                }
                else
                {
                    // The Line and line segment are parallel. There are no intersections.
                    result.State |= IntersectionStates.Parallel | IntersectionStates.NoIntersection;
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
                    result.AppendPoint(new Point2D(rx + (ta * ri), ry + (ta * rj)));
                    result.State |= IntersectionStates.Intersection;
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
        /// <param name="lx">The lx.</param>
        /// <param name="ly">The ly.</param>
        /// <param name="li">The li.</param>
        /// <param name="lj">The lj.</param>
        /// <param name="s0X">The s0X.</param>
        /// <param name="s0Y">The s0Y.</param>
        /// <param name="s1X">The s1X.</param>
        /// <param name="s1Y">The s1Y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            var result = new Intersection(IntersectionStates.Intersection);

            // Translate lines to origin.
            (var vi, var vj) = (s1X - s0X, s1Y - s0Y);

            var ua = (vi * (ly - s0Y)) - (vj * (lx - s0X));
            var ub = (li * (ly - s0Y)) - (lj * (lx - s0X));

            // Calculate the determinant of the coefficient matrix.
            var determinant = (vj * li) - (vi * lj);

            // Check if the lines are parallel.
            if (Abs(determinant) < epsilon)
            {
                if (ua == 0 || ub == 0)
                {
                    // Line segment is coincident to the Line. There are an infinite number of intersections, but we only care about the start and end points of the line segment.
                    result.AppendPoints(new Point2D(s0X, s0Y), new Point2D(s1X, s1Y));
                    result.State |= IntersectionStates.Coincident | IntersectionStates.Parallel | IntersectionStates.Intersection;
                }
                else
                {
                    // The Line and line segment are parallel. There are no intersections.
                    result.State |= IntersectionStates.Parallel | IntersectionStates.NoIntersection;
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
                    result.AppendPoint(new Point2D(lx + (ta * li), ly + (ta * lj)));
                    result.State |= IntersectionStates.Intersection;
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
        /// Find the intersection between a line and a quadratic Bézier.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="b0x">The b0x.</param>
        /// <param name="b0y">The b0y.</param>
        /// <param name="b1x">The b1x.</param>
        /// <param name="b1y">The b1y.</param>
        /// <param name="b2x">The b2x.</param>
        /// <param name="b2y">The b2y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineQuadraticBezierIntersection(
            double x1, double y1, double x2, double y2,
            double b0x, double b0y, double b1x, double b1y, double b2x, double b2y,
            double epsilon = Epsilon)
            => LineQuadraticBezierIntersection(x1, y1, x2, y2, QuadraticBezierBernsteinBasis(b0x, b1x, b2x), QuadraticBezierBernsteinBasis(b0y, b1y, b2y), epsilon);

        /// <summary>
        /// Find the intersection between a line and a quadratic Bézier.
        /// </summary>
        /// <param name="lx">The lx.</param>
        /// <param name="ly">The ly.</param>
        /// <param name="li">The li.</param>
        /// <param name="lj">The lj.</param>
        /// <param name="xCurve">The set of Polynomial Bézier Coefficients of the x coordinates of the Bézier curve.</param>
        /// <param name="yCurve">The set of Polynomial Bézier Coefficients of the y coordinates of the Bézier curve.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            _ = epsilon;
            // Initialize intersection.
            var result = new Intersection(IntersectionStates.NoIntersection);

            var c = (lx * (ly - lj)) + (ly * (li - lx));

            // Find the polynomial that represents the intersections.
            var roots = ((lj * xCurve) - (li * yCurve) + c).Trim().Roots();

            foreach (var s in roots)
            {
                // Add intersection point.
                if (!(s < 0 /*start*/ || s > 1d /*end*/))
                {
                    result.AppendPoint(new Point2D(
                        (xCurve[0] * s * s) + (xCurve[1] * s) + xCurve[2],
                        (yCurve[0] * s * s) + (yCurve[1] * s) + yCurve[2]));
                }
            }

            // Return result.
            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a line and a cubic Bézier.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="b0x">The b0x.</param>
        /// <param name="b0y">The b0y.</param>
        /// <param name="b1x">The b1x.</param>
        /// <param name="b1y">The b1y.</param>
        /// <param name="b2x">The b2x.</param>
        /// <param name="b2y">The b2y.</param>
        /// <param name="b3x">The b3x.</param>
        /// <param name="b3y">The b3y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineCubicBezierIntersection(
            double x1, double y1, double x2, double y2,
            double b0x, double b0y, double b1x, double b1y, double b2x, double b2y, double b3x, double b3y,
            double epsilon = Epsilon)
            => LineCubicBezierIntersection(
                x1, y1, x2, y2,
                CubicBezierBernsteinBasis(b0x, b1x, b2x, b3x),
                CubicBezierBernsteinBasis(b0y, b1y, b2y, b3y),
                epsilon);

        /// <summary>
        /// Find the intersection between a line and a cubic Bézier.
        /// </summary>
        /// <param name="lx">The lx.</param>
        /// <param name="ly">The ly.</param>
        /// <param name="li">The li.</param>
        /// <param name="lj">The lj.</param>
        /// <param name="xCurve">The set of Polynomial Bézier Coefficients of the x coordinates of the Bézier curve.</param>
        /// <param name="yCurve">The set of Polynomial Bézier Coefficients of the y coordinates of the Bézier curve.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            _ = epsilon;
            // Initialize the intersection.
            var result = new Intersection(IntersectionStates.NoIntersection);

            var c = (lx * (ly - lj)) + (ly * (li - lx));

            // Find the polynomial that represents the intersections.
            var roots = ((lj * xCurve) - (li * yCurve) + c).Trim().Roots();

            foreach (var s in roots)
            {
                // Add intersection point.
                if (!(s < 0 || s > 1d))
                {
                    result.AppendPoint(new Point2D(
                    (xCurve[0] * s * s * s) + (xCurve[1] * s * s) + (xCurve[2] * s) + xCurve[3],
                    (yCurve[0] * s * s * s) + (yCurve[1] * s * s) + (yCurve[2] * s) + yCurve[3]));
                }
            }

            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a Line and a triangle.
        /// </summary>
        /// <param name="lx">The lx.</param>
        /// <param name="ly">The ly.</param>
        /// <param name="li">The li.</param>
        /// <param name="lj">The lj.</param>
        /// <param name="tx0">The tx0.</param>
        /// <param name="ty0">The ty0.</param>
        /// <param name="tx1">The tx1.</param>
        /// <param name="ty1">The ty1.</param>
        /// <param name="tx2">The tx2.</param>
        /// <param name="ty2">The ty2.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a line and a rectangle.
        /// </summary>
        /// <param name="a1X">The a1X.</param>
        /// <param name="a1Y">The a1Y.</param>
        /// <param name="a2X">The a2X.</param>
        /// <param name="a2Y">The a2Y.</param>
        /// <param name="r1X">The r1X.</param>
        /// <param name="r1Y">The r1Y.</param>
        /// <param name="r2X">The r2X.</param>
        /// <param name="r2Y">The r2Y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a line and a polygon contour.
        /// </summary>
        /// <param name="a1X">The a1X.</param>
        /// <param name="a1Y">The a1Y.</param>
        /// <param name="a2X">The a2X.</param>
        /// <param name="a2Y">The a2Y.</param>
        /// <param name="points">The points.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LinePolylineIntersection(
            double a1X, double a1Y, double a2X, double a2Y,
            List<Point2D> points,
            double epsilon = Epsilon)
        {
            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            var length = points.Count;

            var p = points[0];
            for (var i = 1; i < length; ++i)
            {
                var b2 = points[i];

                intersections.UnionWith(LineLineSegmentIntersection(a1X, a1Y, a2X, a2Y, p.X, p.Y, b2.X, b2.Y, epsilon).Points);

                p = b2;
            }

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a line and a polygon contour.
        /// </summary>
        /// <param name="a1X">The a1X.</param>
        /// <param name="a1Y">The a1Y.</param>
        /// <param name="a2X">The a2X.</param>
        /// <param name="a2Y">The a2Y.</param>
        /// <param name="points">The points.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            var b1 = points[length - 1];
            for (var i = 0; i < length; ++i)
            {
                var b2 = points[i];

                intersections.UnionWith(LineLineSegmentIntersection(a1X, a1Y, a2X, a2Y, b1.X, b1.Y, b2.X, b2.Y, epsilon).Points);

                b1 = b2;
            }

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a line and a circle.
        /// </summary>
        /// <param name="lx">The lx.</param>
        /// <param name="ly">The ly.</param>
        /// <param name="li">The li.</param>
        /// <param name="lj">The lj.</param>
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        /// <param name="r">The r.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            var result = new Intersection(IntersectionStates.NoIntersection);

            // If the circle or line segment are empty, return no intersections.
            if ((r == 0d) || ((lx == li) && (ly == lj)))
            {
                return result;
            }

            // Calculate the quadratic parameters.
            var a = (li * li) + (lj * lj);
            var b = 2d * ((li * (lx - cX)) + (lj * (ly - cY)));
            var c = ((lx - cX) * (lx - cX)) + ((ly - cY) * (ly - cY)) - (r * r);

            // Calculate the discriminant.
            var discriminant = (b * b) - (4d * a * c);

            if ((a <= epsilon) || (discriminant < 0d))
            {
                // No real solutions.
                result.State |= IntersectionStates.Outside;
                return result;
            }
            else if (discriminant == 0d)
            {
                // One possible solution.
                var t = -b / (2d * a);

                // Add the points.
                result = new Intersection(IntersectionStates.Intersection);
                result.AppendPoint(new Point2D(lx + (t * li), ly + (t * lj)));
            }
            else if (discriminant > 0d)
            {
                // Two possible solutions.
                var t1 = (-b + Sqrt(discriminant)) / (2d * a);
                var t2 = (-b - Sqrt(discriminant)) / (2d * a);

                // Add the points.
                result = new Intersection(IntersectionStates.Intersection);
                result.AppendPoint(new Point2D(lx + (t1 * li), ly + (t1 * lj)));
                result.AppendPoint(new Point2D(lx + (t2 * li), ly + (t2 * lj)));
            }

            // Return result.
            return result;
        }

        /// <summary>
        /// Find the intersection between a line and a circular arc.
        /// </summary>
        /// <param name="lx">The lx.</param>
        /// <param name="ly">The ly.</param>
        /// <param name="li">The li.</param>
        /// <param name="lj">The lj.</param>
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        /// <param name="r">The r.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="startAngle">The startAngle.</param>
        /// <param name="sweepAngle">The sweepAngle.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            var result = new Intersection(IntersectionStates.NoIntersection);

            // If the circle or line segment are empty, return no intersections.
            if ((r == 0d) || ((lx == li) && (ly == lj)))
            {
                return result;
            }

            // Calculate the quadratic parameters.
            var a = (li * li) + (lj * lj);
            var b = 2d * ((li * (lx - cX)) + (lj * (ly - cY)));
            var c = ((lx - cX) * (lx - cX)) + ((ly - cY) * (ly - cY)) - (r * r);

            // Find the points of the chord.
            var startPoint = new Point2D(cX + (Cos(angle + startAngle) * r), cY + (Sin(angle + startAngle) * r));
            var endPoint = new Point2D(cX + (Cos(angle + startAngle + sweepAngle) * r), cY + (Sin(angle + startAngle + sweepAngle) * r));

            // Calculate the discriminant.
            var discriminant = (b * b) - (4d * a * c);

            // Check for intersections.
            if ((a <= epsilon) || (discriminant < 0d))
            {
                // No real solutions.
                result.State |= IntersectionStates.Outside;
                return result;
            }
            else if (discriminant == 0d)
            {
                // One possible solution.
                var t = -b / (2d * a);

                // Find the point.
                var pX = lx + (t * li);
                var pY = ly + (t * lj);

                // Find the determinant of the chord and point.
                var determinant = ((startPoint.X - pX) * (endPoint.Y - pY)) - ((endPoint.X - pX) * (startPoint.Y - pY));

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle))
                {
                    // Add the point.
                    result.AppendPoint(new Point2D(pX, pY));
                }
            }
            else if (discriminant > 0d)
            {
                // Two possible solutions.
                var t1 = (-b + Sqrt(discriminant)) / (2d * a);
                var t2 = (-b - Sqrt(discriminant)) / (2d * a);

                // Find the point.
                var pX = lx + (t1 * li);
                var pY = ly + (t1 * lj);

                // Find the determinant of the chord and point.
                var determinant = ((startPoint.X - pX) * (endPoint.Y - pY)) - ((endPoint.X - pX) * (startPoint.Y - pY));

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle))
                {
                    // Add the point.
                    result.AppendPoint(new Point2D(pX, pY));
                }

                // Find the point.
                pX = lx + (t2 * li);
                pY = ly + (t2 * lj);

                // Find the determinant of the chord and point.
                determinant = ((startPoint.X - pX) * (endPoint.Y - pY)) - ((endPoint.X - pX) * (startPoint.Y - pY));

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
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a line and an ellipse.
        /// </summary>
        /// <param name="lx">The lx.</param>
        /// <param name="ly">The ly.</param>
        /// <param name="li">The li.</param>
        /// <param name="lj">The lj.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineObliqueEllipseIntersection(double lx, double ly, double li, double lj, double cx, double cy, double rx, double ry, double angle, double epsilon = Epsilon) => LineObliqueEllipseIntersection(lx, ly, li, lj, cx, cy, rx, ry, Cos(angle), Sin(angle), epsilon);

        /// <summary>
        /// Find the intersection between a line and an ellipse.
        /// </summary>
        /// <param name="lx">The lx.</param>
        /// <param name="ly">The ly.</param>
        /// <param name="li">The li.</param>
        /// <param name="lj">The lj.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="cosA">The cosA.</param>
        /// <param name="sinA">The sinA.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2012/09/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineObliqueEllipseIntersection(
            double lx, double ly, double li, double lj,
            double cx, double cy, double rx, double ry, double cosA, double sinA,
            double epsilon = Epsilon)
        {
            // Fix imprecise handling of Cos(PI/2).
            if (sinA == 1d || sinA == -1d) cosA = 0d;

            // Initialize the resulting intersection structure.
            var result = new Intersection(IntersectionStates.NoIntersection);

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
            var u1A = (u1 * cosA) - (v1 * -sinA);
            var v1A = (u1 * -sinA) + (v1 * cosA);
            var u2A = (u2 * cosA) - (v2 * -sinA);
            var v2A = (u2 * -sinA) + (v2 * cosA);

            // Calculate the quadratic parameters.
            var a = ((u2A - u1A) * (u2A - u1A) / (rx * rx)) + ((v2A - v1A) * (v2A - v1A) / (ry * ry));
            var b = (2d * u1A * (u2A - u1A) / (rx * rx)) + (2d * v1A * (v2A - v1A) / (ry * ry));
            var c = (u1A * u1A / (rx * rx)) + (v1A * v1A / (ry * ry)) - 1d;

            // Calculate the discriminant.
            var discriminant = (b * b) - (4d * a * c);

            // Find solutions.
            if ((a <= epsilon) || (discriminant < 0d))
            {
                // No real solutions.
                result.State |= IntersectionStates.Outside;
                return result;
            }
            else if (discriminant == 0d)
            {
                // One real possible solution.
                var t = OneHalf * -b / a;

                // Add the point.
                result.AppendPoint(new Point2D(u1 + ((u2 - u1) * t) + cx, v1 + ((v2 - v1) * t) + cy));
            }
            else if (discriminant > 0d)
            {
                // Two real possible solutions.
                var t1 = OneHalf * (-b + Sqrt(discriminant)) / a;
                var t2 = OneHalf * (-b - Sqrt(discriminant)) / a;

                // Add the points.
                result.AppendPoint(new Point2D(u1 + ((u2 - u1) * t1) + cx, v1 + ((v2 - v1) * t1) + cy));
                result.AppendPoint(new Point2D(u1 + ((u2 - u1) * t2) + cx, v1 + ((v2 - v1) * t2) + cy));
            }

            // ToDo: Return IntersectionState.Inside if both points are inside the Ellipse.

            // Return the intersections.
            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a line and an elliptical arc.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="startAngle">The startAngle.</param>
        /// <param name="sweepAngle">The sweepAngle.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineEllipticalArcIntersection(double x0, double y0, double x1, double y1, double cx, double cy, double rx, double ry, double angle, double startAngle, double sweepAngle, double epsilon = Epsilon) => LineEllipticalArcIntersection(x0, y0, x1, y1, cx, cy, rx, ry, Cos(angle), Sin(angle), startAngle, sweepAngle, epsilon);

        /// <summary>
        /// Find the intersection between a line and an elliptical arc.
        /// </summary>
        /// <param name="lx">The lx.</param>
        /// <param name="ly">The ly.</param>
        /// <param name="li">The li.</param>
        /// <param name="lj">The lj.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="cosA">The cosA.</param>
        /// <param name="sinA">The sinA.</param>
        /// <param name="startAngle">The startAngle.</param>
        /// <param name="sweepAngle">The sweepAngle.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            // Fix imprecise handling of Cos(PI/2).
            if (sinA == 1d || sinA == -1d) cosA = 0d;

            // Initialize the resulting intersection structure.
            var result = new Intersection(IntersectionStates.NoIntersection);

            // If the ellipse or line segment are empty, return no intersections.
            if ((sweepAngle == 0d) || (rx == 0d) || (ry == 0d) ||
                ((li == 0d) && (lj == 0d)))
            {
                return result;
            }

            // Translate the line to put it at the ellipse centered at the origin.
            var u0 = lx - cx;
            var v0 = ly - cy;
            var u1 = lx + li - cx;
            var v1 = ly + lj - cy;

            // Apply the rotation transformation to line at the origin.
            var u0A = (u0 * cosA) - (v0 * -sinA);
            var v0A = (u0 * -sinA) + (v0 * cosA);
            var u1A = (u1 * cosA) - (v1 * -sinA);
            var v1A = (u1 * -sinA) + (v1 * cosA);

            // Calculate the quadratic parameters.
            var a = ((u1A - u0A) * (u1A - u0A) / (rx * rx)) + ((v1A - v0A) * (v1A - v0A) / (ry * ry));
            var b = (2d * u0A * (u1A - u0A) / (rx * rx)) + (2d * v0A * (v1A - v0A) / (ry * ry));
            var c = (u0A * u0A / (rx * rx)) + (v0A * v0A / (ry * ry)) - 1d;

            // Calculate the discriminant of the quadratic.
            var discriminant = (b * b) - (4d * a * c);

            // Check whether line segment is outside of the ellipse.
            if ((a <= epsilon) || (discriminant < 0d))
            {
                // No real solutions.
                result.State |= IntersectionStates.Outside;
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
            var sx = cx + ((usa * cosA) + (vsa * sinA));
            var sy = cy + ((usa * sinA) - (vsa * cosA));
            var ex = cx + ((uea * cosA) + (vea * sinA));
            var ey = cy + ((uea * sinA) - (vea * cosA));

            if (discriminant == 0d)
            {
                // One real possible solution.
                var t = OneHalf * -b / a;

                // Find the point.
                var p = new Point2D(u0 + ((u1 - u0) * t) + cx, v0 + ((v1 - v0) * t) + cy);

                // Find the determinant of the matrix representing the chord.
                var determinant = ((sx - p.X) * (ey - p.Y)) - ((ex - p.X) * (sy - p.Y));

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle))
                {
                    result.AppendPoint(p);
                }
            }
            else if (discriminant > 0d)
            {
                // Two real possible solutions.
                var root = Sqrt(discriminant);
                var t1 = OneHalf * (-b + root) / a;
                var t2 = OneHalf * (-b - root) / a;

                // Find the point.
                var p = new Point2D(u0 + ((u1 - u0) * t1) + cx, v0 + ((v1 - v0) * t1) + cy);

                // Find the determinant of the matrix representing the chord.
                var determinant = ((sx - p.X) * (ey - p.Y)) - ((ex - p.X) * (sy - p.Y));

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle))
                {
                    result.AppendPoint(p);
                }

                // Find the point.
                p = new Point2D(u0 + ((u1 - u0) * t2) + cx, v0 + ((v1 - v0) * t2) + cy);

                // Find the determinant of the matrix representing the chord.
                determinant = ((sx - p.X) * (ey - p.Y)) - ((ex - p.X) * (sy - p.Y));

                // Add the point if it is on the sweep side of the chord.
                if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle))
                {
                    result.AppendPoint(p);
                }
            }

            // Return the intersections.
            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a line segment and a ray.
        /// </summary>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="ri">The ri.</param>
        /// <param name="rj">The rj.</param>
        /// <param name="s1X">The s1X.</param>
        /// <param name="s1Y">The s1Y.</param>
        /// <param name="s2X">The s2X.</param>
        /// <param name="s2Y">The s2Y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            var result = new Intersection(IntersectionStates.Intersection);

            // Translate line segment to origin.
            var u = s2X - s1X;
            var v = s2Y - s1Y;

            // Intersection cross product.
            var ua = (u * (ry - s1Y)) - (v * (rx - s1X));
            var ub = (ri * (ry - s1Y)) - (rj * (rx - s1X));

            // Calculate the determinant of the coefficient matrix.
            var determinant = (v * ri) - (u * rj);

            // Check if the lines are parallel.
            if (Abs(determinant) < epsilon)
            {
                if (ua == 0d || ub == 0d)
                {
                    //// Line segment is coincident to the Line. There are an infinite number of intersections, but we only care about the start and end points of the line segment.
                    //result.AppendPoints(new List<Point2D> { new Point2D(s1X, s1Y), new Point2D(s2X, s2Y) });
                    result.State |= IntersectionStates.Coincident | IntersectionStates.Parallel | IntersectionStates.Intersection;
                }
                else
                {
                    // The Line and line segment are parallel. There are no intersections.
                    result.State |= IntersectionStates.Parallel | IntersectionStates.NoIntersection;
                }
            }
            else
            {
                // Find the index where the intersection point lies on the line.
                var ta = ua / determinant;
                var tb = ub / determinant;

                if (ta >= 0d /*&& ta <= 1*/ && tb >= 0d && tb <= 1)
                {
                    // One intersection.
                    result.AppendPoint(new Point2D(rx + (ta * ri), ry + (ta * rj)));
                    result.State |= IntersectionStates.Intersection;
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
        /// <param name="r0x">The r0x.</param>
        /// <param name="r0y">The r0y.</param>
        /// <param name="r0i">The r0i.</param>
        /// <param name="r0j">The r0j.</param>
        /// <param name="r1x">The r1x.</param>
        /// <param name="r1y">The r1y.</param>
        /// <param name="r1i">The r1i.</param>
        /// <param name="r1j">The r1j.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            var result = new Intersection(IntersectionStates.NoIntersection);

            // Intersection cross product.
            var ua = (r1i * (r0y - r1y)) - (r1j * (r0x - r1x));
            var ub = (r0i * (r0y - r1y)) - (r0j * (r0x - r1x));

            // Calculate the determinant of the coefficient matrix.
            var determinant = (r1j * r0i) - (r1i * r0j);

            if (Abs(determinant) < epsilon)
            {
                if (ua == 0d || ub == 0d)
                {
                    result.State |= IntersectionStates.Coincident;
                }
                else
                {
                    result.State |= IntersectionStates.Parallel;
                }
            }
            else
            {
                var ta = ua / determinant;
                var tb = ub / determinant;

                if (ta >= 0d /*&& ta <= 1*/ && tb >= 0d /*&& tb <= 1*/)
                {
                    // One intersection.
                    result.State = IntersectionStates.Intersection;
                    result.AppendPoint(new Point2D(r0x + (ta * r0i), r0y + (ta * r0j)));
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
        /// Find the intersection between a Ray and a quadratic Bézier.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="i1">The i1.</param>
        /// <param name="j1">The j1.</param>
        /// <param name="xCurve">The set of Polynomial Bézier Coefficients of the x coordinates of the Bézier curve.</param>
        /// <param name="yCurve">The set of Polynomial Bézier Coefficients of the y coordinates of the Bézier curve.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            _ = epsilon;
            // Initialize the intersection.
            var result = new Intersection(IntersectionStates.NoIntersection);

            var c = (x1 * j1) + (y1 * i1);

            // Find the polynomial that represents the intersections.
            var roots = ((j1 * xCurve) - (i1 * yCurve) + c).Trim().Roots();

            foreach (var s in roots)
            {
                // Intersection point assuming it was an infinitely long line.
                var x = (xCurve[0] * s * s) + (xCurve[1] * s) + xCurve[2];
                var y = (yCurve[0] * s * s) + (yCurve[1] * s) + yCurve[2];

                double slope;
                // Special handling for vertical lines.
                slope = i1 != 0d ? (x - x1) / i1 : (y - y1) / j1;

                // Make sure we are in bounds of the line segment.
                if (!(s < 0d /*|| s > 1d*/ || slope < 0d || slope > 1d))
                {
                    // Add intersection point.
                    result.AppendPoint(new Point2D(x, y));
                }
            }

            // Return the result.
            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a ray and a cubic Bézier.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="i1">The i1.</param>
        /// <param name="j1">The j1.</param>
        /// <param name="xCurve">The set of Polynomial Bézier Coefficients of the x coordinates of the Bézier curve.</param>
        /// <param name="yCurve">The set of Polynomial Bézier Coefficients of the y coordinates of the Bézier curve.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            _ = epsilon;
            // Initialize the intersection.
            var result = new Intersection(IntersectionStates.NoIntersection);

            var c = (x1 * -j1) + (y1 * i1);

            // Find the polynomial that represents the intersections.
            var roots = ((j1 * xCurve) + (-i1 * yCurve) + c).Trim().Roots();

            foreach (var s in roots)
            {
                // Intersection point assuming infinitely long line segment.
                var point = new Point2D(
                    (xCurve[0] * s * s * s) + (xCurve[1] * s * s) + (xCurve[2] * s) + xCurve[3],
                    (yCurve[0] * s * s * s) + (yCurve[1] * s * s) + (yCurve[2] * s) + yCurve[3]);

                double slope;

                // Special handling for vertical lines.
                slope = i1 != 0d ? (point.X - x1) / i1 : (point.Y - y1) / j1;

                // Make sure we are in bounds of the line segment.
                if (!(s < 0d /*|| s > 1d*/ || slope < 0d || slope > 1d))
                {
                    // Add intersection point.
                    result.AppendPoint(point);
                }
            }

            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a ray and a triangle.
        /// </summary>
        /// <param name="lx0">The lx0.</param>
        /// <param name="ly0">The ly0.</param>
        /// <param name="lx1">The lx1.</param>
        /// <param name="ly1">The ly1.</param>
        /// <param name="tx0">The tx0.</param>
        /// <param name="ty0">The ty0.</param>
        /// <param name="tx1">The tx1.</param>
        /// <param name="ty1">The ty1.</param>
        /// <param name="tx2">The tx2.</param>
        /// <param name="ty2">The ty2.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
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

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a ray and a rectangle.
        /// </summary>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="ri">The ri.</param>
        /// <param name="rj">The rj.</param>
        /// <param name="r1X">The r1X.</param>
        /// <param name="r1Y">The r1Y.</param>
        /// <param name="r2X">The r2X.</param>
        /// <param name="r2Y">The r2Y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a ray and a polyline.
        /// </summary>
        /// <param name="a1X">The a1X.</param>
        /// <param name="a1Y">The a1Y.</param>
        /// <param name="a2X">The a2X.</param>
        /// <param name="a2Y">The a2Y.</param>
        /// <param name="points">The points.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection RayPolylineContourIntersection(
            double a1X, double a1Y, double a2X, double a2Y,
            List<Point2D> points,
            double epsilon = Epsilon)
        {
            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            var length = points.Count;

            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            var b1 = points[0];
            for (var i = 1; i < length; ++i)
            {
                var b2 = points[i];

                intersections.UnionWith(RayLineSegmentIntersection(b1.X, b1.Y, b2.X, b2.Y, a1X, a1Y, a2X, a2Y, epsilon).Points);

                b1 = b2;
            }

            // ToDo: Return IntersectionState.Inside if both end points are inside the polygon, and there are no intersections.

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a ray and a polygon contour.
        /// </summary>
        /// <param name="a1X">The a1X.</param>
        /// <param name="a1Y">The a1Y.</param>
        /// <param name="a2X">The a2X.</param>
        /// <param name="a2Y">The a2Y.</param>
        /// <param name="points">The points.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            var b1 = points[length - 1];
            for (var i = 0; i < length; ++i)
            {
                var b2 = points[i];

                intersections.UnionWith(RayLineSegmentIntersection(b1.X, b1.Y, b2.X, b2.Y, a1X, a1Y, a2X, a2Y, epsilon).Points);

                b1 = b2;
            }

            // ToDo: Return IntersectionState.Inside if both end points are inside the polygon, and there are no intersections.

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a ray and a circle.
        /// </summary>
        /// <param name="lAX">The lAX.</param>
        /// <param name="lAY">The lAY.</param>
        /// <param name="lBi">The lBi.</param>
        /// <param name="lBj">The lBj.</param>
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        /// <param name="r">The r.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            _ = angle;
            _ = epsilon;
            Intersection result;

            var a = (lBi * lBi) + (lBj * lBj);
            var b = 2 * ((lBi * (lAX - cX)) + (lBj * (lAY - cY)));
            var c = (cX * cX) + (cY * cY) + (lAX * lAX) + (lAY * lAY) - (2 * ((cX * lAX) + (cY * lAY))) - (r * r);

            var determinant = (b * b) - (4 * a * c);
            if (determinant < 0d)
            {
                result = new Intersection(IntersectionStates.Outside);
            }
            else if (determinant == 0d)
            {
                //result = new Intersection(IntersectionState.Tangent | IntersectionState.Intersection);
                var u1 = (-b) / (2d * a);
                if (u1 < 0d || u1 > 1d)
                {
                    result = (u1 < 0d) || (u1 > 1d) ? new Intersection(IntersectionStates.Outside) : new Intersection(IntersectionStates.Inside);
                }
                else
                {
                    result = new Intersection(IntersectionStates.Intersection);
                    if (0d <= u1 && u1 <= 1d)
                    {
                        result.Points.Add(Lerp(lAX, lAY, lBi, lBj, u1));
                    }
                }
            }
            else
            {
                var e = Sqrt(determinant);
                var u1 = (-b + e) / (2d * a);
                var u2 = (-b - e) / (2d * a);
                if ((u1 < 0d /*|| u1 > 1d*/) && (u2 < 0d || u2 > 1d))
                {
                    result = (u1 < 0d && u2 < 0d) || (u1 > 1d && u2 > 1d) ? new Intersection(IntersectionStates.Outside) : new Intersection(IntersectionStates.Inside);
                }
                else
                {
                    result = new Intersection(IntersectionStates.Intersection);
                    if (0d <= u1/* && u1 <= 1d*/)
                    {
                        result.Points.Add(Lerp(lAX, lAY, lBi, lBj, u1));
                    }

                    if (0d <= u2/* && u2 <= 1d*/)
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
        /// <param name="lAX">The lAX.</param>
        /// <param name="lAY">The lAY.</param>
        /// <param name="lBI">The lBI.</param>
        /// <param name="lBJ">The lBJ.</param>
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        /// <param name="r">The r.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="startAngle">The startAngle.</param>
        /// <param name="sweepAngle">The sweepAngle.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            _ = angle;
            var result = new Intersection(IntersectionStates.NoIntersection);

            // If the circle or line segment are empty, return no intersections.
            if ((r == 0d) || ((0d == lBI) && (0d == lBJ)))
            {
                return result;
            }

            // Calculate the quadratic parameters.
            var a = (lBI * lBI) + (lBJ * lBJ);
            var b = 2d * ((lBI * (lAX - cX)) + (lBJ * (lAY - cY)));
            var c = ((lAX - cX) * (lAX - cX)) + ((lAY - cY) * (lAY - cY)) - (r * r);

            // Find the points of the chord.
            Point2D startPoint = Interpolators.CircularArc(0d, cX, cY, r, startAngle, sweepAngle);
            Point2D endPoint = Interpolators.CircularArc(1d, cX, cY, r, startAngle, sweepAngle);

            // Calculate the discriminant.
            var discriminant = (b * b) - (4d * a * c);

            if ((a <= epsilon) || (discriminant < 0d))
            {
                // No real solutions.
            }
            else if (discriminant == 0d)
            {
                // One possible solution.
                var t = -b / (2d * a);

                // Find the point.
                var pX = lAX + (t * lBI);
                var pY = lAY + (t * lBJ);

                // Find the determinant of the chord and point.
                var determinant = ((startPoint.X - pX) * (endPoint.Y - pY)) - ((endPoint.X - pX) * (startPoint.Y - pY));

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle) && 0d <= t && t <= 1d)
                {
                    // Add the point.
                    result.AppendPoint(new Point2D(pX, pY));
                }
            }
            else if (discriminant > 0d)
            {
                // Two possible solutions.
                var t1 = (-b + Sqrt(discriminant)) / (2d * a);
                var t2 = (-b - Sqrt(discriminant)) / (2d * a);

                // Find the point.
                var pX = lAX + (t1 * lBI);
                var pY = lAY + (t1 * lBJ);

                // Find the determinant of the chord and point.
                var determinant = ((startPoint.X - pX) * (endPoint.Y - pY)) - ((endPoint.X - pX) * (startPoint.Y - pY));

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle) && 0d <= t1 && t1 <= 1d)
                {
                    // Add the point.
                    result.AppendPoint(new Point2D(pX, pY));
                }

                // Find the point.
                pX = lAX + (t2 * lBI);
                pY = lAY + (t2 * lBJ);

                // Find the determinant of the chord and point.
                determinant = ((startPoint.X - pX) * (endPoint.Y - pY)) - ((endPoint.X - pX) * (startPoint.Y - pY));

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle) && (0d <= t2/* && t2 <= 1d*/))
                {
                    // Add the point.
                    result.AppendPoint(new Point2D(pX, pY));
                }
            }

            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a ray and an ellipse.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="i0">The i0.</param>
        /// <param name="j0">The j0.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="cosA">The cosine of the rotation angle.</param>
        /// <param name="sinA">The sine of the rotation angle.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2012/09/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection RayObliqueEllipseIntersection(
            double x0, double y0, double i0, double j0,
            double cx, double cy, double rx, double ry, double cosA, double sinA,
            double epsilon = Epsilon)
        {
            // Fix imprecise handling of Cos(PI/2).
            if (sinA == 1d || sinA == -1d) cosA = 0d;

            // Initialize the resulting intersection structure.
            var result = new Intersection(IntersectionStates.NoIntersection);

            // If the ellipse or line segment are empty, return no intersections.
            if ((rx == 0d) || (ry == 0d) ||
                ((0d == i0) && (0d == j0)))
            {
                return result;
            }

            // Translate the line to put the ellipse centered at the origin.
            var u1 = x0 - cx;
            var v1 = y0 - cy;
            var u2 = x0 + i0 - cx;
            var v2 = y0 + j0 - cy;

            // Apply Rotation Transform to line at the origin.
            var u1A = 0d + ((u1 * cosA) - (v1 * -sinA));
            var v1A = 0d + ((u1 * -sinA) + (v1 * cosA));
            var u2A = 0d + ((u2 * cosA) - (v2 * -sinA));
            var v2A = 0d + ((u2 * -sinA) + (v2 * cosA));

            // Calculate the quadratic parameters.
            var a = ((u2A - u1A) * (u2A - u1A) / (rx * rx)) + ((v2A - v1A) * (v2A - v1A) / (ry * ry));
            var b = (2d * u1A * (u2A - u1A) / (rx * rx)) + (2d * v1A * (v2A - v1A) / (ry * ry));
            var c = (u1A * u1A / (rx * rx)) + (v1A * v1A / (ry * ry)) - 1d;

            // Calculate the discriminant.
            var discriminant = (b * b) - (4d * a * c);

            // Find solutions.
            if ((a <= epsilon) || (discriminant < 0d))
            {
                // No real solutions.
                result.State |= IntersectionStates.Outside;
                return result;
            }
            else if (discriminant == 0d)
            {
                // One real possible solution.
                var t = 0.5d * -b / a;

                // Add the point if it is between the end points of the line segment.
                if ((t >= 0d) && (t <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + ((u2 - u1) * t) + cx, v1 + ((v2 - v1) * t) + cy));
                }

            }
            else if (discriminant > 0d)
            {
                // Two real possible solutions.
                var t1 = 0.5d * (-b + Sqrt(discriminant)) / a;
                var t2 = 0.5d * (-b - Sqrt(discriminant)) / a;

                // Add the points if they are between the end points of the line segment.
                if (t1 >= 0d/* && (t1 <= 1d)*/)
                {
                    result.AppendPoint(new Point2D(u1 + ((u2 - u1) * t1) + cx, v1 + ((v2 - v1) * t1) + cy));
                }

                if (t2 >= 0d/* && (t2 <= 1d)*/)
                {
                    result.AppendPoint(new Point2D(u1 + ((u2 - u1) * t2) + cx, v1 + ((v2 - v1) * t2) + cy));
                }
            }

            // ToDo: Return IntersectionState.Inside if both end points are inside the ellipse.

            // Return the intersections.
            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a ray and an elliptical arc.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="i0">The i0.</param>
        /// <param name="j0">The j0.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="cosA">The cosine of the rotation angle.</param>
        /// <param name="sinA">The sine of the rotation angle.</param>
        /// <param name="startAngle">The startAngle.</param>
        /// <param name="sweepAngle">The sweepAngle.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            // Fix imprecise handling of Cos(PI/2).
            if (sinA == 1d || sinA == -1d) cosA = 0d;

            // Initialize the resulting intersection structure.
            var result = new Intersection(IntersectionStates.NoIntersection);

            // If the ellipse or line segment are empty, return no intersections.
            if ((sweepAngle == 0d) || (rx == 0d) || (ry == 0d) ||
                ((0d == i0) && (0d == j0)))
            {
                return result;
            }

            // Translate the line to move it to the ellipse centered at the origin.
            var u0 = x0 - cx;
            var v0 = y0 - cy;
            var u1 = x0 + i0 - cx;
            var v1 = y0 + j0 - cy;

            // Apply Rotation Transform to line at the origin to align it with the ellipse orthogonally.
            var u0A = (u0 * cosA) - (v0 * -sinA);
            var v0A = (u0 * -sinA) + (v0 * cosA);
            var u1A = (u1 * cosA) - (v1 * -sinA);
            var v1A = (u1 * -sinA) + (v1 * cosA);

            // Calculate the quadratic parameters.
            var a = ((u1A - u0A) * (u1A - u0A) / (rx * rx)) + ((v1A - v0A) * (v1A - v0A) / (ry * ry));
            var b = (2d * u0A * (u1A - u0A) / (rx * rx)) + (2d * v0A * (v1A - v0A) / (ry * ry));
            var c = (u0A * u0A / (rx * rx)) + (v0A * v0A / (ry * ry)) - 1d;

            // Calculate the discriminant of the quadratic.
            var discriminant = (b * b) - (4d * a * c);

            // Check whether line segment is outside of the ellipse.
            if ((a <= epsilon) || (discriminant < 0))
            {
                // No real solutions.
                result.State |= IntersectionStates.Outside;
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
            var sx = cx + ((usa * cosA) + (vsa * sinA));
            var sy = cy + ((usa * sinA) - (vsa * cosA));
            var ex = cx + ((uea * cosA) + (vea * sinA));
            var ey = cy + ((uea * sinA) - (vea * cosA));

            if (discriminant == 0d)
            {
                // One real possible solution.
                var t = OneHalf * -b / a;

                // Add the point if it is between the end points of the line segment.
                if (t >= 0d /*&& t <= 1d*/)
                {
                    // Find the point.
                    var p = new Point2D(u0 + ((u1 - u0) * t) + cx, v0 + ((v1 - v0) * t) + cy);

                    // Find the determinant of the matrix representing the chord.
                    var determinant = ((sx - p.X) * (ey - p.Y)) - ((ex - p.X) * (sy - p.Y));

                    // Add the point if it is on the sweep side of the chord.
                    if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle))
                    {
                        result.AppendPoint(p);
                    }
                }
            }
            else if (discriminant > 0d)
            {
                // Two real possible solutions.
                var root = Sqrt(discriminant);
                var t1 = OneHalf * (-b + root) / a;
                var t2 = OneHalf * (-b - root) / a;

                // Add the points if they are between the end points of the line segment.
                if (t1 >= 0d /*&& (t1 == 1d)*/)
                {
                    // Find the point.
                    var p = new Point2D(u0 + ((u1 - u0) * t1) + cx, v0 + ((v1 - v0) * t1) + cy);

                    // Find the determinant of the matrix representing the chord.
                    var determinant = ((sx - p.X) * (ey - p.Y)) - ((ex - p.X) * (sy - p.Y));

                    // Add the point if it is on the sweep side of the chord.
                    if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle))
                    {
                        result.AppendPoint(p);
                    }
                }

                if (t2 >= 0d /*&& (t2 <= 1d)*/)
                {
                    // Find the point.
                    var p = new Point2D(u0 + ((u1 - u0) * t2) + cx, v0 + ((v1 - v0) * t2) + cy);

                    // Find the determinant of the matrix representing the chord.
                    var determinant = ((sx - p.X) * (ey - p.Y)) - ((ex - p.X) * (sy - p.Y));

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
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between two line segments.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="b1X">The b1X.</param>
        /// <param name="b1Y">The b1Y.</param>
        /// <param name="b2X">The b2X.</param>
        /// <param name="b2Y">The b2Y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            _ = epsilon;
            var result = new Intersection(IntersectionStates.NoIntersection);

            var ua = ((b2X - b1X) * (y1 - b1Y)) - ((b2Y - b1Y) * (x1 - b1X));
            var ub = ((x2 - x1) * (y1 - b1Y)) - ((y2 - y1) * (x1 - b1X));

            var determinant = ((b2Y - b1Y) * (x2 - x1)) - ((b2X - b1X) * (y2 - y1));

            if (determinant == 0d)
            {
                if (ua == 0d || ub == 0d)
                {
                    result.State = IntersectionStates.Coincident;
                }
                else
                {
                    result.State = IntersectionStates.Parallel;
                }
            }
            else
            {
                var ta = ua / determinant;
                var tb = ub / determinant;

                if (0d <= ta && ta <= 1d && 0d <= tb && tb <= 1d)
                {
                    result.State = IntersectionStates.Intersection;
                    result.AppendPoint(new Point2D(x1 + (ta * (x2 - x1)), y1 + (ta * (y2 - y1))));
                }
                else
                {
                    result.State = IntersectionStates.NoIntersection;
                }
            }
            return result;
        }

        /// <summary>
        /// Find the intersection between a line segment and a quadratic Bézier.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="b0x">The b0x.</param>
        /// <param name="b0y">The b0y.</param>
        /// <param name="b1x">The b1x.</param>
        /// <param name="b1y">The b1y.</param>
        /// <param name="b2x">The b2x.</param>
        /// <param name="b2y">The b2y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentQuadraticBezierSegmentIntersection(
            double x1, double y1, double x2, double y2,
            double b0x, double b0y, double b1x, double b1y, double b2x, double b2y,
            double epsilon = Epsilon)
            => LineSegmentQuadraticBezierSegmentIntersection(
                x1, y1, x2, y2,
                QuadraticBezierBernsteinBasis(b0x, b1x, b2x),
                QuadraticBezierBernsteinBasis(b0y, b1y, b2y),
                epsilon);

        /// <summary>
        /// Find the intersection between a line segment and a quadratic Bézier.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="xCurve">The set of Polynomial Bézier Coefficients of the x coordinates of the Bézier curve.</param>
        /// <param name="yCurve">The set of Polynomial Bézier Coefficients of the y coordinates of the Bézier curve.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            _ = epsilon;
            // Initialize the intersection.
            var result = new Intersection(IntersectionStates.NoIntersection);

            // Translate the line to the origin.
            var a = y2 - y1;
            var b = x1 - x2;

            var c = (x1 * (y1 - y2)) + (y1 * (x2 - x1));

            // Find the roots of the polynomial that represents the intersections.
            var roots = ((a * xCurve) + (b * yCurve) + c).Trim().Roots();

            foreach (var s in roots)
            {
                // Intersection point assuming it was an infinitely long line.
                var x = (xCurve[0] * s * s) + (xCurve[1] * s) + xCurve[2];
                var y = (yCurve[0] * s * s) + (yCurve[1] * s) + yCurve[2];

                double slope;

                // Special handling for vertical lines.
                slope = (x2 - x1) != 0d ? (x - x1) / (x2 - x1) : (y - y1) / (y2 - y1);

                // Make sure we are in bounds of the line segment.
                if (!(s < 0d || s > 1d || slope < 0d || slope > 1d))
                {
                    // Add intersection point.
                    result.AppendPoint(new Point2D(x, y));
                }
            }

            // Return the result.
            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a line segment and a cubic Bézier.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="b0x">The b0x.</param>
        /// <param name="b0y">The b0y.</param>
        /// <param name="b1x">The b1x.</param>
        /// <param name="b1y">The b1y.</param>
        /// <param name="b2x">The b2x.</param>
        /// <param name="b2y">The b2y.</param>
        /// <param name="b3x">The b3x.</param>
        /// <param name="b3y">The b3y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentCubicBezierSegmentIntersection(
            double x1, double y1, double x2, double y2,
            double b0x, double b0y, double b1x, double b1y, double b2x, double b2y, double b3x, double b3y,
            double epsilon = Epsilon)
            => LineSegmentCubicBezierSegmentIntersection(
                x1, y1, x2, y2,
                CubicBezierBernsteinBasis(b0x, b1x, b2x, b3x),
                CubicBezierBernsteinBasis(b0y, b1y, b2y, b3y),
                epsilon);

        /// <summary>
        /// Find the intersection between a line segment and a cubic Bézier.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="xCurve">The set of Polynomial Bézier Coefficients of the x coordinates of the Bézier curve.</param>
        /// <param name="yCurve">The set of Polynomial Bézier Coefficients of the y coordinates of the Bézier curve.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            _ = epsilon;
            // Initialize the intersection.
            var result = new Intersection(IntersectionStates.NoIntersection);

            // Translate the line to the origin.
            var a = y2 - y1;
            var b = x1 - x2;

            var c = (x1 * (y1 - y2)) + (y1 * (x2 - x1));

            // Find the roots of the polynomial that represents the intersections.
            var roots = ((a * xCurve) + (b * yCurve) + c).Trim().Roots();

            foreach (var s in roots)
            {
                // Intersection point assuming infinitely long line segment.
                var point = new Point2D(
                    (xCurve[0] * s * s * s) + (xCurve[1] * s * s) + (xCurve[2] * s) + xCurve[3],
                    (yCurve[0] * s * s * s) + (yCurve[1] * s * s) + (yCurve[2] * s) + yCurve[3]);

                double slope;

                // Special handling for vertical lines.
                slope = (x2 - x1) != 0d ? (point.X - x1) / (x2 - x1) : (point.Y - y1) / (y2 - y1);

                // Make sure we are in bounds of the line segment.
                if (!(s < 0d || s > 1d || slope < 0d || slope > 1d))
                {
                    // Add intersection point.
                    result.AppendPoint(point);
                }
            }

            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a line segment and a triangle.
        /// </summary>
        /// <param name="lx0">The lx0.</param>
        /// <param name="ly0">The ly0.</param>
        /// <param name="lx1">The lx1.</param>
        /// <param name="ly1">The ly1.</param>
        /// <param name="tx0">The tx0.</param>
        /// <param name="ty0">The ty0.</param>
        /// <param name="tx1">The tx1.</param>
        /// <param name="ty1">The ty1.</param>
        /// <param name="tx2">The tx2.</param>
        /// <param name="ty2">The ty2.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
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

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a line segment and a rectangle.
        /// </summary>
        /// <param name="lAX">The lAX.</param>
        /// <param name="lAY">The lAY.</param>
        /// <param name="lBX">The lBX.</param>
        /// <param name="lBY">The lBY.</param>
        /// <param name="r1X">The r1X.</param>
        /// <param name="r1Y">The r1Y.</param>
        /// <param name="r2X">The r2X.</param>
        /// <param name="r2Y">The r2Y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a line segment and a polyline.
        /// </summary>
        /// <param name="a1X">The a1X.</param>
        /// <param name="a1Y">The a1Y.</param>
        /// <param name="a2X">The a2X.</param>
        /// <param name="a2Y">The a2Y.</param>
        /// <param name="points">The points.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentPolylineIntersection(
            double a1X, double a1Y, double a2X, double a2Y,
            List<Point2D> points,
            double epsilon = Epsilon)
        {
            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            var length = points.Count;

            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            var b1 = points[0];
            for (var i = 1; i < length; ++i)
            {
                var b2 = points[i];

                intersections.UnionWith(LineSegmentLineSegmentIntersection(b1.X, b1.Y, b2.X, b2.Y, a1X, a1Y, a2X, a2Y, epsilon).Points);

                b1 = b2;
            }

            // ToDo: Return IntersectionState.Inside if both end points are inside the polygon, and there are no intersections.

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a line segment and a polygon contour.
        /// </summary>
        /// <param name="a1X">The a1X.</param>
        /// <param name="a1Y">The a1Y.</param>
        /// <param name="a2X">The a2X.</param>
        /// <param name="a2Y">The a2Y.</param>
        /// <param name="points">The points.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            var b1 = points[length - 1];
            for (var i = 0; i < length; ++i)
            {
                var b2 = points[i];

                intersections.UnionWith(LineSegmentLineSegmentIntersection(b1.X, b1.Y, b2.X, b2.Y, a1X, a1Y, a2X, a2Y, epsilon).Points);

                b1 = b2;
            }

            // ToDo: Return IntersectionState.Inside if both end points are inside the polygon, and there are no intersections.

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a line segment and a circle.
        /// </summary>
        /// <param name="lAX">The lAX.</param>
        /// <param name="lAY">The lAY.</param>
        /// <param name="lBX">The lBX.</param>
        /// <param name="lBY">The lBY.</param>
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        /// <param name="r">The r.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            _ = angle;
            _ = epsilon;
            Intersection result;

            var a = ((lBX - lAX) * (lBX - lAX)) + ((lBY - lAY) * (lBY - lAY));
            var b = 2 * (((lBX - lAX) * (lAX - cX)) + ((lBY - lAY) * (lAY - cY)));
            var c = (cX * cX) + (cY * cY) + (lAX * lAX) + (lAY * lAY) - (2 * ((cX * lAX) + (cY * lAY))) - (r * r);

            var determinant = (b * b) - (4d * a * c);
            if (determinant < 0d)
            {
                result = new Intersection(IntersectionStates.Outside);
            }
            else if (determinant == 0d)
            {
                //result = new Intersection(IntersectionState.Tangent | IntersectionState.Intersection);
                var u1 = (-b) / (2d * a);
                if (u1 < 0d || u1 > 1d)
                {
                    result = (u1 < 0d) || (u1 > 1) ? new Intersection(IntersectionStates.Outside) : new Intersection(IntersectionStates.Inside);
                }
                else
                {
                    result = new Intersection(IntersectionStates.Intersection);
                    if (0d <= u1 && u1 <= 1d)
                    {
                        result.Points.Add(Lerp(lAX, lAY, lBX, lBY, u1));
                    }
                }
            }
            else
            {
                var e = Sqrt(determinant);
                var u1 = (-b + e) / (2d * a);
                var u2 = (-b - e) / (2d * a);
                if ((u1 < 0d || u1 > 1d) && (u2 < 0d || u2 > 1d))
                {
                    result = (u1 < 0d && u2 < 0d) || (u1 > 1d && u2 > 1d) ? new Intersection(IntersectionStates.Outside) : new Intersection(IntersectionStates.Inside);
                }
                else
                {
                    result = new Intersection(IntersectionStates.Intersection);
                    if (0d <= u1 && u1 <= 1d)
                    {
                        result.Points.Add(Lerp(lAX, lAY, lBX, lBY, u1));
                    }

                    if (0d <= u2 && u2 <= 1d)
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
        /// <param name="lAX">The lAX.</param>
        /// <param name="lAY">The lAY.</param>
        /// <param name="lBX">The lBX.</param>
        /// <param name="lBY">The lBY.</param>
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        /// <param name="r">The r.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="startAngle">The startAngle.</param>
        /// <param name="sweepAngle">The sweepAngle.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            _ = angle;
            var result = new Intersection(IntersectionStates.NoIntersection);

            // If the circle or line segment are empty, return no intersections.
            if ((r == 0d) || ((lAX == lBX) && (lAY == lBY)))
            {
                return result;
            }

            var dx = lBX - lAX;
            var dy = lBY - lAY;

            // Calculate the quadratic parameters.
            var a = (dx * dx) + (dy * dy);
            var b = 2 * ((dx * (lAX - cX)) + (dy * (lAY - cY)));
            var c = ((lAX - cX) * (lAX - cX)) + ((lAY - cY) * (lAY - cY)) - (r * r);

            // Find the points of the chord.
            Point2D startPoint = Interpolators.CircularArc(0, cX, cY, r, startAngle, sweepAngle);
            Point2D endPoint = Interpolators.CircularArc(1, cX, cY, r, startAngle, sweepAngle);

            // Calculate the discriminant.
            var discriminant = (b * b) - (4d * a * c);

            if ((a <= epsilon) || (discriminant < 0d))
            {
                // No real solutions.
            }
            else if (discriminant == 0d)
            {
                // One possible solution.
                var t = -b / (2d * a);

                // Find the point.
                var pX = lAX + (t * dx);
                var pY = lAY + (t * dy);

                // Find the determinant of the chord and point.
                var determinant = ((startPoint.X - pX) * (endPoint.Y - pY)) - ((endPoint.X - pX) * (startPoint.Y - pY));

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle) && 0d <= t && t <= 1d)
                {
                    // Add the point.
                    result.AppendPoint(new Point2D(pX, pY));
                }
            }
            else if (discriminant > 0d)
            {
                // Two possible solutions.
                var t1 = (-b + Sqrt(discriminant)) / (2d * a);
                var t2 = (-b - Sqrt(discriminant)) / (2d * a);

                // Find the point.
                var pX = lAX + (t1 * dx);
                var pY = lAY + (t1 * dy);

                // Find the determinant of the chord and point.
                var determinant = ((startPoint.X - pX) * (endPoint.Y - pY)) - ((endPoint.X - pX) * (startPoint.Y - pY));

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle) && 0d <= t1 && t1 <= 1d)
                {
                    // Add the point.
                    result.AppendPoint(new Point2D(pX, pY));
                }

                // Find the point.
                pX = lAX + (t2 * dx);
                pY = lAY + (t2 * dy);

                // Find the determinant of the chord and point.
                determinant = ((startPoint.X - pX) * (endPoint.Y - pY)) - ((endPoint.X - pX) * (startPoint.Y - pY));

                // Check whether the point is on the same side of the chord as the center.
                if (Sign(determinant) != Sign(sweepAngle) && 0d <= t2 && t2 <= 1d)
                {
                    // Add the point.
                    result.AppendPoint(new Point2D(pX, pY));
                }
            }

            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a line segment and an ellipse.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentObliqueEllipseIntersection(double x0, double y0, double x1, double y1, double cx, double cy, double rx, double ry, double angle, double epsilon = Epsilon) => LineSegmentObliqueEllipseIntersection(x0, y0, x1, y1, cx, cy, rx, ry, Cos(angle), Sin(angle), epsilon);

        /// <summary>
        /// Find the intersection between a line segment and an ellipse.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="cosA">The cosine of the rotation angle.</param>
        /// <param name="sinA">The sine of the rotation angle.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2012/09/calculate-where-a-line-segment-and-an-ellipse-intersect-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentObliqueEllipseIntersection(
            double x0, double y0, double x1, double y1,
            double cx, double cy, double rx, double ry, double cosA, double sinA,
            double epsilon = Epsilon)
        {
            // Fix imprecise handling of Cos(PI/2).
            if (sinA == 1d || sinA == -1d) cosA = 0d;

            // Initialize the resulting intersection structure.
            var result = new Intersection(IntersectionStates.NoIntersection);

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
            var u1A = 0d + ((u1 * cosA) - (v1 * -sinA));
            var v1A = 0d + ((u1 * -sinA) + (v1 * cosA));
            var u2A = 0d + ((u2 * cosA) - (v2 * -sinA));
            var v2A = 0d + ((u2 * -sinA) + (v2 * cosA));

            // Calculate the quadratic parameters.
            var a = ((u2A - u1A) * (u2A - u1A) / (rx * rx)) + ((v2A - v1A) * (v2A - v1A) / (ry * ry));
            var b = (2d * u1A * (u2A - u1A) / (rx * rx)) + (2d * v1A * (v2A - v1A) / (ry * ry));
            var c = (u1A * u1A / (rx * rx)) + (v1A * v1A / (ry * ry)) - 1d;

            // Calculate the discriminant.
            var discriminant = (b * b) - (4d * a * c);

            // Find solutions.
            if ((a <= epsilon) || (discriminant < 0d))
            {
                // No real solutions.
                result.State |= IntersectionStates.Outside;
                return result;
            }
            else if (discriminant == 0d)
            {
                // One real possible solution.
                var t = 0.5d * -b / a;

                // Add the point if it is between the end points of the line segment.
                if ((t >= 0d) && (t <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + ((u2 - u1) * t) + cx, v1 + ((v2 - v1) * t) + cy));
                }

            }
            else if (discriminant > 0d)
            {
                // Two real possible solutions.
                var t1 = 0.5d * (-b + Sqrt(discriminant)) / a;
                var t2 = 0.5d * (-b - Sqrt(discriminant)) / a;

                // Add the points if they are between the end points of the line segment.
                if ((t1 >= 0d) && (t1 <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + ((u2 - u1) * t1) + cx, v1 + ((v2 - v1) * t1) + cy));
                }

                if ((t2 >= 0d) && (t2 <= 1d))
                {
                    result.AppendPoint(new Point2D(u1 + ((u2 - u1) * t2) + cx, v1 + ((v2 - v1) * t2) + cy));
                }
            }

            // ToDo: Return IntersectionState.Inside if both end points are inside the ellipse.

            // Return the intersections.
            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a line segment and an elliptical arc.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="startAngle">The startAngle.</param>
        /// <param name="sweepAngle">The sweepAngle.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection LineSegmentEllipticalArcIntersection(
            double x0, double y0, double x1, double y1,
            double cx, double cy, double rx, double ry, double angle, double startAngle, double sweepAngle,
            double epsilon = Epsilon)
            => LineSegmentEllipticalArcIntersection(x0, y0, x1, y1, cx, cy, rx, ry, Cos(angle), Sin(angle), startAngle, sweepAngle, epsilon);

        /// <summary>
        /// Find the intersection between a line segment and an elliptical arc.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="cosA">The cosine of the rotation angle.</param>
        /// <param name="sinA">The sine of the rotation angle.</param>
        /// <param name="startAngle">The startAngle.</param>
        /// <param name="sweepAngle">The sweepAngle.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            // Fix imprecise handling of Cos(PI/2).
            if (sinA == 1d || sinA == -1d) cosA = 0d;

            // Initialize the resulting intersection structure.
            var result = new Intersection(IntersectionStates.NoIntersection);

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

            // Apply Rotation Transform to line at the origin to align it with the ellipse Orthogonally.
            var u0A = (u0 * cosA) - (v0 * -sinA);
            var v0A = (u0 * -sinA) + (v0 * cosA);
            var u1A = (u1 * cosA) - (v1 * -sinA);
            var v1A = (u1 * -sinA) + (v1 * cosA);

            // Calculate the quadratic parameters.
            var a = ((u1A - u0A) * (u1A - u0A) / (rx * rx)) + ((v1A - v0A) * (v1A - v0A) / (ry * ry));
            var b = (2d * u0A * (u1A - u0A) / (rx * rx)) + (2d * v0A * (v1A - v0A) / (ry * ry));
            var c = (u0A * u0A / (rx * rx)) + (v0A * v0A / (ry * ry)) - 1d;

            // Calculate the discriminant of the quadratic.
            var discriminant = (b * b) - (4d * a * c);

            // Check whether line segment is outside of the ellipse.
            if ((a <= epsilon) || (discriminant < 0d))
            {
                // No real solutions.
                result.State |= IntersectionStates.Outside;
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
            var sx = cx + ((usa * cosA) + (vsa * sinA));
            var sy = cy + ((usa * sinA) - (vsa * cosA));
            var ex = cx + ((uea * cosA) + (vea * sinA));
            var ey = cy + ((uea * sinA) - (vea * cosA));

            if (discriminant == 0d)
            {
                // One real possible solution.
                var t = OneHalf * -b / a;

                // Add the point if it is between the end points of the line segment.
                if (t >= 0d && t <= 1d)
                {
                    // Find the point.
                    var p = new Point2D(u0 + ((u1 - u0) * t) + cx, v0 + ((v1 - v0) * t) + cy);

                    // Find the determinant of the matrix representing the chord.
                    var determinant = ((sx - p.X) * (ey - p.Y)) - ((ex - p.X) * (sy - p.Y));

                    // Add the point if it is on the sweep side of the chord.
                    if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle))
                    {
                        result.AppendPoint(p);
                    }
                }
            }
            else if (discriminant > 0d)
            {
                // Two real possible solutions.
                var root = Sqrt(discriminant);
                var t1 = OneHalf * (-b + root) / a;
                var t2 = OneHalf * (-b - root) / a;

                // Add the points if they are between the end points of the line segment.
                if ((t1 >= 0d) && (t1 == 1d))
                {
                    // Find the point.
                    var p = new Point2D(u0 + ((u1 - u0) * t1) + cx, v0 + ((v1 - v0) * t1) + cy);

                    // Find the determinant of the matrix representing the chord.
                    var determinant = ((sx - p.X) * (ey - p.Y)) - ((ex - p.X) * (sy - p.Y));

                    // Add the point if it is on the sweep side of the chord.
                    if (Abs(determinant) < epsilon || Sign(determinant) != Sign(sweepAngle))
                    {
                        result.AppendPoint(p);
                    }
                }

                if ((t2 >= 0d) && (t2 <= 1d))
                {
                    // Find the point.
                    var p = new Point2D(u0 + ((u1 - u0) * t2) + cx, v0 + ((v1 - v0) * t2) + cy);

                    // Find the determinant of the matrix representing the chord.
                    var determinant = ((sx - p.X) * (ey - p.Y)) - ((ex - p.X) * (sy - p.Y));

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
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between two quadratic Bézier.
        /// </summary>
        /// <param name="a1X">The a1X.</param>
        /// <param name="a1Y">The a1Y.</param>
        /// <param name="a2X">The a2X.</param>
        /// <param name="a2Y">The a2Y.</param>
        /// <param name="a3X">The a3X.</param>
        /// <param name="a3Y">The a3Y.</param>
        /// <param name="b1X">The b1X.</param>
        /// <param name="b1Y">The b1Y.</param>
        /// <param name="b2X">The b2X.</param>
        /// <param name="b2Y">The b2Y.</param>
        /// <param name="b3X">The b3X.</param>
        /// <param name="b3Y">The b3Y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentQuadraticBezierSegmentIntersection(
            double a1X, double a1Y, double a2X, double a2Y, double a3X, double a3Y,
            double b1X, double b1Y, double b2X, double b2Y, double b3X, double b3Y,
            double epsilon = Epsilon)
            => QuadraticBezierSegmentQuadraticBezierSegmentIntersection(
                QuadraticBezierBernsteinBasis(a1X, a2X, a3X),
                QuadraticBezierBernsteinBasis(a1Y, a2Y, a3Y),
                QuadraticBezierBernsteinBasis(b1X, b2X, b3X),
                QuadraticBezierBernsteinBasis(b1Y, b2Y, b3Y),
                epsilon);

        /// <summary>
        /// Find the intersection between two quadratic Bézier.
        /// </summary>
        /// <param name="xCurveA">The set of Polynomial Bézier Coefficients of the x coordinates of the first Bézier curve.</param>
        /// <param name="yCurveA">The set of Polynomial Bézier Coefficients of the y coordinates of the first Bézier curve.</param>
        /// <param name="xCurveB">The set of Polynomial Bézier Coefficients of the x coordinates of the second Bézier curve.</param>
        /// <param name="yCurveB">The set of Polynomial Bézier Coefficients of the y coordinates of the second Bézier curve.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            var result = new Intersection(IntersectionStates.NoIntersection);

            // Bezout

            // Cross product of first coefficient of a and b.
            var e = (xCurveA[0] * yCurveB[0]) - (xCurveB[0] * yCurveA[0]);

            // Cross product of first coefficient of a and second coefficient of b.
            var f = (xCurveA[0] * yCurveB[1]) - (xCurveB[1] * yCurveA[0]);

            // Cross product of second coefficient of a and first coefficient of b.
            var v2 = (xCurveA[1] * yCurveA[0]) - (yCurveA[1] * xCurveA[0]);

            // Delta of third coefficients of curves b and a.
            var v3 = yCurveA[2] - yCurveB[2];

            // The difference of the first coefficients x and y of a times the deltas of the third coefficients of the x and y of curves a and b.
            var v4 = (yCurveA[0] * (xCurveA[2] - xCurveB[2])) - (xCurveA[0] * v3);

            var v5 = (-yCurveA[1] * v2) + (yCurveA[0] * v4);

            // Square of the second cross product.
            var v6 = v2 * v2;

            // Find the roots of the determinants of the polynomial that represents the intersections.
            var roots = new Polynomial(
                // Square of first cross product.
                /* x⁴ */ e * e,
                // Two times the first cross product times the second cross product.
                /* x³ */ 2 * e * f,
                /* x² */ ((-yCurveB[0] * v6) + (yCurveA[0] * f * f) + (yCurveA[0] * e * v4) + (e * v5)) / yCurveA[0],
                /* x¹ */ ((-yCurveB[1] * v6) + (yCurveA[0] * f * v4) + (f * v5)) / yCurveA[0],
                /* c  */ ((v3 * v6) + (v4 * v5)) / yCurveA[0]
            ).Trim().Roots();

            foreach (var s in roots)
            {
                // Interpolate the point at t of the root s on curve b.
                var point = new Point2D(
                    (xCurveB[0] * s * s) + (xCurveB[1] * s) + xCurveB[2],
                    (yCurveB[0] * s * s) + (yCurveB[1] * s) + yCurveB[2]);

                if (s >= 0d && s <= 1d)
                {
                    // Look for intersections on curve a at the same location.
                    var xRoots = (xCurveA - point.X).Trim().Roots();
                    var yRoots = (yCurveA - point.Y).Trim().Roots();

                    if (xRoots.Length > 0 && yRoots.Length > 0)
                    {
                        // Find the nearest matching x and y roots in the ranges 0 < x < 1; 0 < y < 1.
                        foreach (var xRoot in xRoots)
                        {
                            if (xRoot >= 0d && xRoot <= 1d)
                            {
                                foreach (var yRoot in yRoots)
                                {
                                    var t = xRoot - yRoot;
                                    if ((t >= 0d ? t : -t) < epsilon)
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
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a quadratic Bézier and a cubic Bézier.
        /// </summary>
        /// <param name="a1X">The a1X.</param>
        /// <param name="a1Y">The a1Y.</param>
        /// <param name="a2X">The a2X.</param>
        /// <param name="a2Y">The a2Y.</param>
        /// <param name="a3X">The a3X.</param>
        /// <param name="a3Y">The a3Y.</param>
        /// <param name="b1X">The b1X.</param>
        /// <param name="b1Y">The b1Y.</param>
        /// <param name="b2X">The b2X.</param>
        /// <param name="b2Y">The b2Y.</param>
        /// <param name="b3X">The b3X.</param>
        /// <param name="b3Y">The b3Y.</param>
        /// <param name="b4X">The b4X.</param>
        /// <param name="b4Y">The b4Y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentCubicBezierSegmentIntersection(
            double a1X, double a1Y, double a2X, double a2Y, double a3X, double a3Y,
            double b1X, double b1Y, double b2X, double b2Y, double b3X, double b3Y, double b4X, double b4Y,
            double epsilon = Epsilon)
            => QuadraticBezierSegmentCubicBezierSegmentIntersection(
                QuadraticBezierBernsteinBasis(a1X, a2X, a3X),
                QuadraticBezierBernsteinBasis(a1Y, a2Y, a3Y),
                CubicBezierBernsteinBasis(b1X, b2X, b3X, b4X),
                CubicBezierBernsteinBasis(b1Y, b2Y, b3Y, b4Y),
                epsilon);

        /// <summary>
        /// Find the intersection between a quadratic Bézier and a cubic Bézier.
        /// </summary>
        /// <param name="xCurveA">The set of Polynomial Bézier Coefficients of the x coordinates of the first Bézier curve.</param>
        /// <param name="yCurveA">The set of Polynomial Bézier Coefficients of the y coordinates of the first Bézier curve.</param>
        /// <param name="xCurveB">The set of Polynomial Bézier Coefficients of the x coordinates of the second Bézier curve.</param>
        /// <param name="yCurveB">The set of Polynomial Bézier Coefficients of the y coordinates of the second Bézier curve.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            var result = new Intersection(IntersectionStates.NoIntersection);

            // ToDo: The tolerance is off by too much. Need to find the error.
            var tolerance = 4294967295d * epsilon; // 1e-4;

            // Bezout
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

            // Find the roots of the determinants of the polynomial that represents the intersections.
            var roots = new Polynomial(
                /* x⁶ */ (-2d * xCurveA[0] * yCurveA[0] * xCurveB[0] * yCurveB[0]) + (cAAx2 * cBAy2) + (cAAy2 * cBAx2),
                /* x⁵ */ (-2d * xCurveA[0] * yCurveA[0] * xCurveB[1] * yCurveB[0]) - (2d * xCurveA[0] * yCurveA[0] * yCurveB[1] * xCurveB[0]) + (2d * cAAy2 * xCurveB[1] * xCurveB[0]) + (2d * cAAx2 * yCurveB[1] * yCurveB[0]),
                /* x⁴ */ (-2d * xCurveA[0] * xCurveB[2] * yCurveA[0] * yCurveB[0]) - (2d * xCurveA[0] * yCurveA[0] * yCurveB[2] * xCurveB[0]) - (2d * xCurveA[0] * yCurveA[0] * xCurveB[1] * yCurveB[1]) + (2d * xCurveB[2] * cAAy2 * xCurveB[0]) + (cAAy2 * cBBx2) + (cAAx2 * ((2d * yCurveB[2] * yCurveB[0]) + cBBy2)),
                /* x³ */ (2d * xCurveA[2] * xCurveA[0] * yCurveA[0] * yCurveB[0]) + (2d * yCurveA[2] * xCurveA[0] * yCurveA[0] * xCurveB[0]) + (xCurveA[1] * yCurveA[1] * xCurveA[0] * yCurveB[0]) + (xCurveA[1] * yCurveA[1] * yCurveA[0] * xCurveB[0]) - (2d * xCurveB[3] * xCurveA[0] * yCurveA[0] * yCurveB[0]) - (2d * xCurveA[0] * yCurveB[3] * yCurveA[0] * xCurveB[0]) - (2d * xCurveA[0] * xCurveB[2] * yCurveA[0] * yCurveB[1]) - (2d * xCurveA[0] * yCurveA[0] * yCurveB[2] * xCurveB[1]) - (2d * xCurveA[2] * cAAy2 * xCurveB[0]) - (2d * yCurveA[2] * cAAx2 * yCurveB[0]) + (2d * xCurveB[3] * cAAy2 * xCurveB[0]) + (2d * xCurveB[2] * cAAy2 * xCurveB[1]) - (cABy2 * xCurveA[0] * xCurveB[0]) - (cABx2 * yCurveA[0] * yCurveB[0]) + (cAAx2 * ((2d * yCurveB[3] * yCurveB[0]) + (2d * yCurveB[2] * yCurveB[1]))),
                /* x² */ (2d * xCurveA[2] * xCurveA[0] * yCurveA[0] * yCurveB[1]) + (2d * yCurveA[2] * xCurveA[0] * yCurveA[0] * xCurveB[1]) + (xCurveA[1] * yCurveA[1] * xCurveA[0] * yCurveB[1]) + (xCurveA[1] * yCurveA[1] * yCurveA[0] * xCurveB[1]) - (2d * xCurveB[3] * xCurveA[0] * yCurveA[0] * yCurveB[1]) - (2d * xCurveA[0] * yCurveB[3] * yCurveA[0] * xCurveB[1]) - (2d * xCurveA[0] * xCurveB[2] * yCurveA[0] * yCurveB[2]) - (2d * xCurveA[2] * cAAy2 * xCurveB[1]) - (2d * yCurveA[2] * cAAx2 * yCurveB[1]) + (2d * xCurveB[3] * cAAy2 * xCurveB[1]) - (cABy2 * xCurveA[0] * xCurveB[1]) - (cABx2 * yCurveA[0] * yCurveB[1]) + (cBCx2 * cAAy2) + (cAAx2 * ((2d * yCurveB[3] * yCurveB[1]) + cBCy2)),
                /* x¹ */ (2d * xCurveA[2] * xCurveA[0] * yCurveA[0] * yCurveB[2]) + (2d * yCurveA[2] * xCurveA[0] * xCurveB[2] * yCurveA[0]) + (xCurveA[1] * yCurveA[1] * xCurveA[0] * yCurveB[2]) + (xCurveA[1] * yCurveA[1] * xCurveB[2] * yCurveA[0]) - (2d * xCurveB[3] * xCurveA[0] * yCurveA[0] * yCurveB[2]) - (2d * xCurveA[0] * yCurveB[3] * xCurveB[2] * yCurveA[0]) - (2d * xCurveA[2] * xCurveB[2] * cAAy2) - (2 * yCurveA[2] * cAAx2 * yCurveB[2]) + (2d * xCurveB[3] * xCurveB[2] * cAAy2) - (cABy2 * xCurveA[0] * xCurveB[2]) - (cABx2 * yCurveA[0] * yCurveB[2]) + (2d * cAAx2 * yCurveB[3] * yCurveB[2]),
                /* c  */ (-2d * xCurveA[2] * yCurveA[2] * xCurveA[0] * yCurveA[0]) - (xCurveA[2] * xCurveA[1] * yCurveA[1] * yCurveA[0]) - (yCurveA[2] * xCurveA[1] * yCurveA[1] * xCurveA[0]) + (2d * xCurveA[2] * xCurveA[0] * yCurveB[3] * yCurveA[0]) + (2d * yCurveA[2] * xCurveB[3] * xCurveA[0] * yCurveA[0]) + (xCurveA[1] * xCurveB[3] * yCurveA[1] * yCurveA[0]) + (xCurveA[1] * yCurveA[1] * xCurveA[0] * yCurveB[3]) - (2d * xCurveB[3] * xCurveA[0] * yCurveB[3] * yCurveA[0]) - (2d * xCurveA[2] * xCurveB[3] * cAAy2) + (xCurveA[2] * cABy2 * xCurveA[0]) + (yCurveA[2] * cABx2 * yCurveA[0]) - (2d * yCurveA[2] * cAAx2 * yCurveB[3]) - (xCurveB[3] * cABy2 * xCurveA[0]) - (cABx2 * yCurveB[3] * yCurveA[0]) + (cACx2 * cAAy2) + (cACy2 * cAAx2) + (cBDx2 * cAAy2) + (cAAx2 * cBDy2)
            ).Trim().RootsInInterval();

            foreach (var s in roots)
            {
                var point = new Point2D(
                   (xCurveB[0] * s * s * s) + (xCurveB[1] * s * s) + (xCurveB[2] * s) + xCurveB[3],
                   (yCurveB[0] * s * s * s) + (yCurveB[1] * s * s) + (yCurveB[2] * s) + yCurveB[3]);

                var xRoots = (xCurveA - point.X).Trim().Roots(epsilon);
                var yRoots = (yCurveA - point.Y).Trim().Roots();

                if (xRoots.Length > 0 && yRoots.Length > 0)
                {
                    foreach (var xRoot in xRoots)
                    {
                        if (0d <= xRoot && xRoot <= 1d)
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
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a quadratic Bézier and a polyline.
        /// </summary>
        /// <param name="b1X">The b1X.</param>
        /// <param name="b1Y">The b1Y.</param>
        /// <param name="b2X">The b2X.</param>
        /// <param name="b2Y">The b2Y.</param>
        /// <param name="b3X">The b3X.</param>
        /// <param name="b3Y">The b3Y.</param>
        /// <param name="points">The points.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentPolylineIntersection(
            double b1X, double b1Y, double b2X, double b2Y, double b3X, double b3Y,
            List<Point2D> points,
            double epsilon = Epsilon)
            => QuadraticBezierSegmentPolylineIntersection(
                QuadraticBezierBernsteinBasis(b1X, b2X, b3X),
                QuadraticBezierBernsteinBasis(b1Y, b2Y, b3Y),
                points,
                epsilon);

        /// <summary>
        /// Find the intersection between a quadratic Bézier and a polyline.
        /// </summary>
        /// <param name="xCurve">The xCurve.</param>
        /// <param name="yCurve">The yCurve.</param>
        /// <param name="points">The points.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentPolylineIntersection(
            Polynomial xCurve, Polynomial yCurve,
            List<Point2D> points,
            double epsilon = Epsilon)
        {
            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            var length = points.Count;

            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            var a1 = points[0];
            for (var i = 1; i < length; ++i)
            {
                var a2 = points[i];

                intersections.UnionWith(LineSegmentQuadraticBezierSegmentIntersection(a1.X, a1.Y, a2.X, a2.Y, xCurve, yCurve, epsilon).Points);

                a1 = a2;
            }

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a quadratic Bézier and a polygon contour.
        /// </summary>
        /// <param name="b1X">The b1X.</param>
        /// <param name="b1Y">The b1Y.</param>
        /// <param name="b2X">The b2X.</param>
        /// <param name="b2Y">The b2Y.</param>
        /// <param name="b3X">The b3X.</param>
        /// <param name="b3Y">The b3Y.</param>
        /// <param name="points">The points.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
                QuadraticBezierBernsteinBasis(b1X, b2X, b3X),
                QuadraticBezierBernsteinBasis(b1Y, b2Y, b3Y),
                points,
                epsilon);

        /// <summary>
        /// Find the intersection between a quadratic Bézier and a polygon contour.
        /// </summary>
        /// <param name="xCurve">The xCurve.</param>
        /// <param name="yCurve">The yCurve.</param>
        /// <param name="points">The points.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            var a1 = points[length - 1];
            for (var i = 0; i < length; ++i)
            {
                var a2 = points[i];

                intersections.UnionWith(LineSegmentQuadraticBezierSegmentIntersection(a1.X, a1.Y, a2.X, a2.Y, xCurve, yCurve, epsilon).Points);

                a1 = a2;
            }

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a quadratic Bézier and a triangle.
        /// </summary>
        /// <param name="p1X">The p1X.</param>
        /// <param name="p1Y">The p1Y.</param>
        /// <param name="p2X">The p2X.</param>
        /// <param name="p2Y">The p2Y.</param>
        /// <param name="p3X">The p3X.</param>
        /// <param name="p3Y">The p3Y.</param>
        /// <param name="t1X">The t1X.</param>
        /// <param name="t1Y">The t1Y.</param>
        /// <param name="t2X">The t2X.</param>
        /// <param name="t2Y">The t2Y.</param>
        /// <param name="t3X">The t3X.</param>
        /// <param name="t3Y">The t3Y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentTriangleIntersection(
            double p1X, double p1Y, double p2X, double p2Y, double p3X, double p3Y,
            double t1X, double t1Y, double t2X, double t2Y, double t3X, double t3Y,
            double epsilon = Epsilon)
            => QuadraticBezierSegmentTriangleIntersection(
                QuadraticBezierBernsteinBasis(p1X, p2X, p3X),
                QuadraticBezierBernsteinBasis(p1Y, p2Y, p3Y),
                t1X, t1Y, t2X, t2Y, t3X, t3Y,
                epsilon);

        /// <summary>
        /// Find the intersection between a quadratic Bézier and a triangle.
        /// </summary>
        /// <param name="xCurve">The xCurve.</param>
        /// <param name="yCurve">The yCurve.</param>
        /// <param name="t1X">The t1X.</param>
        /// <param name="t1Y">The t1Y.</param>
        /// <param name="t2X">The t2X.</param>
        /// <param name="t2Y">The t2Y.</param>
        /// <param name="t3X">The t3X.</param>
        /// <param name="t3Y">The t3Y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a quadratic Bézier and a rectangle.
        /// </summary>
        /// <param name="p1X">The p1X.</param>
        /// <param name="p1Y">The p1Y.</param>
        /// <param name="p2X">The p2X.</param>
        /// <param name="p2Y">The p2Y.</param>
        /// <param name="p3X">The p3X.</param>
        /// <param name="p3Y">The p3Y.</param>
        /// <param name="r1X">The r1X.</param>
        /// <param name="r1Y">The r1Y.</param>
        /// <param name="r2X">The r2X.</param>
        /// <param name="r2Y">The r2Y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
                QuadraticBezierBernsteinBasis(p1X, p2X, p3X),
                QuadraticBezierBernsteinBasis(p1Y, p2Y, p3Y),
                r1X, r1Y, r2X, r2Y,
                epsilon);

        /// <summary>
        /// Find the intersection between a quadratic Bézier and a rectangle.
        /// </summary>
        /// <param name="xCurve">The xCurve.</param>
        /// <param name="yCurve">The yCurve.</param>
        /// <param name="r1X">The r1X.</param>
        /// <param name="r1Y">The r1Y.</param>
        /// <param name="r2X">The r2X.</param>
        /// <param name="r2Y">The r2Y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the point of self intersection of a cubic Bézier curve, if the cubic Bézier curve has self intersection.
        /// </summary>
        /// <param name="x0">The x-component of the starting point.</param>
        /// <param name="y0">The y-component of the starting point.</param>
        /// <param name="x1">The x-component of the first tangent handle.</param>
        /// <param name="y1">The y-component of the first tangent handle.</param>
        /// <param name="x2">The x-component of the second tangent handle.</param>
        /// <param name="y2">The y-component of the second tangent handle.</param>
        /// <param name="x3">The x-component of the ending point.</param>
        /// <param name="y3">The y-component of the ending point.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierSegmentSelfIntersection(
            double x0, double y0, double x1, double y1, double x2, double y2, double x3, double y3,
            double epsilon = Epsilon)
            => CubicBezierSegmentSelfIntersection(
                CubicBezierBernsteinBasis(x0, x1, x2, x3),
                CubicBezierBernsteinBasis(y0, y1, y2, y3),
                epsilon);

        /// <summary>
        /// Find the point of self intersection of a cubic Bézier curve, if the cubic Bézier curve has self intersection.
        /// </summary>
        /// <param name="xCurve">The xCurve.</param>
        /// <param name="yCurve">The yCurve.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            var result = new Intersection(IntersectionStates.NoIntersection);

            // Not sure why the difference between the two supposedly same points at different values of t can be so high. It seems to be a lot for floating point rounding. So far it only seems to happen at orthogonal cases.
            var tolerence = 98838707421d * epsilon; // 0.56183300455876406d

            // Bezout
            (var a, var b) = (xCurve[0] == 0d) ? (xCurve[1], xCurve[2]) : (xCurve[1] / xCurve[0], xCurve[2] / xCurve[0]);
            (var p, var q) = (yCurve[0] == 0d) ? (yCurve[1], yCurve[2]) : (yCurve[1] / yCurve[0], yCurve[2] / yCurve[0]);

            if (a == p || q == b)
            {
                return result;
            }

            var k = (q - b) / (a - p);

            // Find the roots of the determinants of the polynomial that represents the intersections.
            var roots = new Polynomial(
                2d,
                -3d * k,
                (3d * k * k) + (2d * k * a) + (2d * b),
                (-k * k * k) - (a * k * k) - (b * k)
                ).Trim().Roots();

            // ToDo: Figure out edge case. When all nodes are linear, even if there should be a flat loop, there is only one root. The locus of points overlap three times for a little ways, and possibly twice past an edge.
            if (roots.Length != 3)
            {
                return result;
            }

            if (roots[0] >= 0d && roots[0] <= 1d && roots[2] >= 0d && roots[2] <= 1d)
            {
                // Locate the points that overlap.
                var points = new List<Point2D>();

                // This loop is for the general case. For cubic curves, one should just be able to grab the point at root[0] or root[2], or lerp halfway between them.
                foreach (var s in roots)
                {
                    var point = new Point2D(
                        (xCurve[0] * s * s * s) + (xCurve[1] * s * s) + (xCurve[2] * s) + xCurve[3],
                        (yCurve[0] * s * s * s) + (yCurve[1] * s * s) + (yCurve[2] * s) + yCurve[3]);

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
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between two cubic Bézier.
        /// </summary>
        /// <param name="a1X">The a1X.</param>
        /// <param name="a1Y">The a1Y.</param>
        /// <param name="a2X">The a2X.</param>
        /// <param name="a2Y">The a2Y.</param>
        /// <param name="a3X">The a3X.</param>
        /// <param name="a3Y">The a3Y.</param>
        /// <param name="a4X">The a4X.</param>
        /// <param name="a4Y">The a4Y.</param>
        /// <param name="b1X">The b1X.</param>
        /// <param name="b1Y">The b1Y.</param>
        /// <param name="b2X">The b2X.</param>
        /// <param name="b2Y">The b2Y.</param>
        /// <param name="b3X">The b3X.</param>
        /// <param name="b3Y">The b3Y.</param>
        /// <param name="b4X">The b4X.</param>
        /// <param name="b4Y">The b4Y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierSegmentCubicBezierSegmentIntersection(
            double a1X, double a1Y, double a2X, double a2Y, double a3X, double a3Y, double a4X, double a4Y,
            double b1X, double b1Y, double b2X, double b2Y, double b3X, double b3Y, double b4X, double b4Y,
            double epsilon = Epsilon)
            => CubicBezierSegmentCubicBezierSegmentIntersection(
                CubicBezierBernsteinBasis(a1X, a2X, a3X, a4X),
                CubicBezierBernsteinBasis(a1Y, a2Y, a3Y, a4Y),
                CubicBezierBernsteinBasis(b1X, b2X, b3X, b4X),
                CubicBezierBernsteinBasis(b1Y, b2Y, b3Y, b4Y),
                epsilon);

        /// <summary>
        /// Find the intersection between two cubic Bézier.
        /// </summary>
        /// <param name="xCurveA">The set of Polynomial Bézier Coefficients of the x coordinates of the first Bézier curve.</param>
        /// <param name="yCurveA">The set of Polynomial Bézier Coefficients of the y coordinates of the first Bézier curve.</param>
        /// <param name="xCurveB">The set of Polynomial Bézier Coefficients of the x coordinates of the second Bézier curve.</param>
        /// <param name="yCurveB">The set of Polynomial Bézier Coefficients of the y coordinates of the second Bézier curve.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            var result = new Intersection(IntersectionStates.NoIntersection);

            // ToDo: The tolerance is off by too much. Need to find the error.
            var tolerance = 4194303 * epsilon;

            // Bezout
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

            // Find the roots of the determinants of the polynomial that represents the intersections.
            var roots = new Polynomial(
                /* x⁹ */ (-c13x3 * c23y3) + (c13y3 * c23x3) - (3d * xCurveA[0] * c13y2 * c23x2 * yCurveB[0]) + (3d * c13x2 * yCurveA[0] * xCurveB[0] * c23y2),
                /* x⁸ */ (-6d * xCurveA[0] * xCurveB[1] * c13y2 * xCurveB[0] * yCurveB[0]) + (6d * c13x2 * yCurveA[0] * yCurveB[1] * xCurveB[0] * yCurveB[0]) + (3d * xCurveB[1] * c13y3 * c23x2) - (3d * c13x3 * yCurveB[1] * c23y2) - (3d * xCurveA[0] * c13y2 * yCurveB[1] * c23x2) + (3d * c13x2 * xCurveB[1] * yCurveA[0] * c23y2),
                /* x⁷ */ (-6d * xCurveB[2] * xCurveA[0] * c13y2 * xCurveB[0] * yCurveB[0]) - (6d * xCurveA[0] * xCurveB[1] * c13y2 * yCurveB[1] * xCurveB[0]) + (6d * c13x2 * xCurveB[1] * yCurveA[0] * yCurveB[1] * yCurveB[0]) + (3d * xCurveB[2] * c13y3 * c23x2) + (3d * c22x2 * c13y3 * xCurveB[0]) + (3d * xCurveB[2] * c13x2 * yCurveA[0] * c23y2) - (3d * xCurveA[0] * yCurveB[2] * c13y2 * c23x2) - (3d * xCurveA[0] * c22x2 * c13y2 * yCurveB[0]) + (c13x2 * yCurveA[0] * xCurveB[0] * ((6d * yCurveB[2] * yCurveB[0]) + (3d * c22y2))) + (c13x3 * ((-yCurveB[2] * c23y2) - (2d * c22y2 * yCurveB[0]) - (yCurveB[0] * ((2d * yCurveB[2] * yCurveB[0]) + c22y2)))),
                /* x⁶ */ (xCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] * xCurveB[0] * yCurveB[0]) - (yCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * xCurveB[0] * yCurveB[0]) + (6d * xCurveB[2] * xCurveB[1] * c13y3 * xCurveB[0]) + (3d * xCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * c23y2) + (6d * xCurveA[3] * xCurveA[0] * c13y2 * xCurveB[0] * yCurveB[0]) - (3d * xCurveA[2] * xCurveA[1] * c13y2 * xCurveB[0] * yCurveB[0]) - (3d * yCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] * c23x2) - (6d * yCurveA[3] * c13x2 * yCurveA[0] * xCurveB[0] * yCurveB[0]) - (6d * xCurveB[3] * xCurveA[0] * c13y2 * xCurveB[0] * yCurveB[0]) + (3d * yCurveA[2] * yCurveA[1] * c13x2 * xCurveB[0] * yCurveB[0]) - (2d * xCurveA[1] * c12y2 * xCurveA[0] * xCurveB[0] * yCurveB[0]) - (6d * xCurveB[2] * xCurveA[0] * xCurveB[1] * c13y2 * yCurveB[0]) - (6d * xCurveB[2] * xCurveA[0] * c13y2 * yCurveB[1] * xCurveB[0]) - (6d * xCurveA[0] * yCurveB[2] * xCurveB[1] * c13y2 * xCurveB[0]) + (6d * xCurveB[2] * c13x2 * yCurveA[0] * yCurveB[1] * yCurveB[0]) + (2d * c12x2 * yCurveA[1] * yCurveA[0] * xCurveB[0] * yCurveB[0]) + (c22x3 * c13y3) - (3d * xCurveA[3] * c13y3 * c23x2) + (3d * yCurveA[3] * c13x3 * c23y2) + (3d * xCurveB[3] * c13y3 * c23x2) + (c12y3 * xCurveA[0] * c23x2) - (c12x3 * yCurveA[0] * c23y2) - (3d * xCurveA[3] * c13x2 * yCurveA[0] * c23y2) + (3d * yCurveA[3] * xCurveA[0] * c13y2 * c23x2) - (2d * xCurveA[2] * yCurveA[1] * c13x2 * c23y2) + (xCurveA[2] * yCurveA[1] * c13y2 * c23x2) - (yCurveA[2] * xCurveA[1] * c13x2 * c23y2) + (2d * yCurveA[2] * xCurveA[1] * c13y2 * c23x2) + (3d * xCurveB[3] * c13x2 * yCurveA[0] * c23y2) - (xCurveA[1] * c12y2 * yCurveA[0] * c23x2) - (3d * yCurveB[3] * xCurveA[0] * c13y2 * c23x2) + (c12x2 * yCurveA[1] * xCurveA[0] * c23y2) - (3d * xCurveA[0] * c22x2 * c13y2 * yCurveB[1]) + (c13x2 * yCurveA[0] * xCurveB[0] * ((6d * yCurveB[3] * yCurveB[0]) + (6d * yCurveB[2] * yCurveB[1]))) + (c13x2 * xCurveB[1] * yCurveA[0] * ((6d * yCurveB[2] * yCurveB[0]) + (3d * c22y2))) + (c13x3 * ((-2d * yCurveB[2] * yCurveB[1] * yCurveB[0]) - (yCurveB[3] * c23y2) - (yCurveB[1] * ((2d * yCurveB[2] * yCurveB[0]) + c22y2)) - (yCurveB[0] * ((2d * yCurveB[3] * yCurveB[0]) + (2d * yCurveB[2] * yCurveB[1]))))),
                /* x⁵ */ (6d * xCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[1] * yCurveB[0]) + (xCurveA[2] * yCurveA[1] * xCurveA[0] * xCurveB[1] * yCurveA[0] * yCurveB[0]) + (xCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[1] * xCurveB[0]) - (yCurveA[2] * xCurveA[1] * xCurveA[0] * xCurveB[1] * yCurveA[0] * yCurveB[0]) - (yCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[1] * xCurveB[0]) - (6d * yCurveA[2] * yCurveA[1] * xCurveA[0] * xCurveB[1] * yCurveA[0] * xCurveB[0]) - (6d * xCurveA[3] * xCurveB[1] * c13y3 * xCurveB[0]) + (6d * xCurveB[3] * xCurveB[1] * c13y3 * xCurveB[0]) + (6d * yCurveA[3] * c13x3 * yCurveB[1] * yCurveB[0]) + (2d * c12y3 * xCurveA[0] * xCurveB[1] * xCurveB[0]) - (2d * c12x3 * yCurveA[0] * yCurveB[1] * yCurveB[0]) + (6d * xCurveA[3] * xCurveA[0] * xCurveB[1] * c13y2 * yCurveB[0]) + (6d * xCurveA[3] * xCurveA[0] * c13y2 * yCurveB[1] * xCurveB[0]) + (6d * yCurveA[3] * xCurveA[0] * xCurveB[1] * c13y2 * xCurveB[0]) - (3d * xCurveA[2] * xCurveA[1] * xCurveB[1] * c13y2 * yCurveB[0]) - (3d * xCurveA[2] * xCurveA[1] * c13y2 * yCurveB[1] * xCurveB[0]) + (2d * xCurveA[2] * yCurveA[1] * xCurveB[1] * c13y2 * xCurveB[0]) + (4d * yCurveA[2] * xCurveA[1] * xCurveB[1] * c13y2 * xCurveB[0]) - (6d * xCurveA[3] * c13x2 * yCurveA[0] * yCurveB[1] * yCurveB[0]) - (6d * yCurveA[3] * c13x2 * xCurveB[1] * yCurveA[0] * yCurveB[0]) - (6d * yCurveA[3] * c13x2 * yCurveA[0] * yCurveB[1] * xCurveB[0]) - (4d * xCurveA[2] * yCurveA[1] * c13x2 * yCurveB[1] * yCurveB[0]) - (6d * xCurveB[3] * xCurveA[0] * xCurveB[1] * c13y2 * yCurveB[0]) - (6d * xCurveB[3] * xCurveA[0] * c13y2 * yCurveB[1] * xCurveB[0]) - (2d * yCurveA[2] * xCurveA[1] * c13x2 * yCurveB[1] * yCurveB[0]) + (3d * yCurveA[2] * yCurveA[1] * c13x2 * xCurveB[1] * yCurveB[0]) + (3d * yCurveA[2] * yCurveA[1] * c13x2 * yCurveB[1] * xCurveB[0]) - (2d * xCurveA[1] * c12y2 * xCurveA[0] * xCurveB[1] * yCurveB[0]) - (2d * xCurveA[1] * c12y2 * xCurveA[0] * yCurveB[1] * xCurveB[0]) - (2d * xCurveA[1] * c12y2 * xCurveB[1] * yCurveA[0] * xCurveB[0]) - (6d * yCurveB[3] * xCurveA[0] * xCurveB[1] * c13y2 * xCurveB[0]) - (6d * xCurveB[2] * xCurveA[0] * yCurveB[2] * c13y2 * xCurveB[0]) - (6d * xCurveB[2] * xCurveA[0] * xCurveB[1] * c13y2 * yCurveB[1]) + (6d * xCurveB[3] * c13x2 * yCurveA[0] * yCurveB[1] * yCurveB[0]) + (2d * c12x2 * yCurveA[1] * xCurveA[0] * yCurveB[1] * yCurveB[0]) + (2d * c12x2 * yCurveA[1] * xCurveB[1] * yCurveA[0] * yCurveB[0]) + (2d * c12x2 * yCurveA[1] * yCurveA[0] * yCurveB[1] * xCurveB[0]) + (3d * xCurveB[2] * c22x2 * c13y3) + (3d * c21x2 * c13y3 * xCurveB[0]) - (3d * xCurveA[0] * yCurveB[2] * c22x2 * c13y2) - (3d * c21x2 * xCurveA[0] * c13y2 * yCurveB[0]) + (c13x2 * xCurveB[1] * yCurveA[0] * ((6d * yCurveB[3] * yCurveB[0]) + (6d * yCurveB[2] * yCurveB[1]))) + (c13x2 * yCurveA[0] * xCurveB[0] * ((6d * yCurveB[3] * yCurveB[1]) + (3d * c21y2))) + (xCurveB[2] * c13x2 * yCurveA[0] * ((6d * yCurveB[2] * yCurveB[0]) + (3d * c22y2))) + (c13x3 * ((-2d * yCurveB[3] * yCurveB[1] * yCurveB[0]) - (yCurveB[0] * ((2d * yCurveB[3] * yCurveB[1]) + c21y2)) - (yCurveB[2] * ((2d * yCurveB[2] * yCurveB[0]) + c22y2)) - (yCurveB[1] * ((2d * yCurveB[3] * yCurveB[0]) + (2d * yCurveB[2] * yCurveB[1]))))),
                /* x⁴ */ (xCurveA[2] * xCurveB[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[0]) + (xCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveB[2] * yCurveA[0] * xCurveB[0]) + (xCurveA[2] * yCurveA[1] * xCurveA[0] * xCurveB[1] * yCurveA[0] * yCurveB[1]) - (yCurveA[2] * xCurveA[1] * xCurveB[2] * xCurveA[0] * yCurveA[0] * yCurveB[0]) - (yCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveB[2] * yCurveA[0] * xCurveB[0]) - (yCurveA[2] * xCurveA[1] * xCurveA[0] * xCurveB[1] * yCurveA[0] * yCurveB[1]) - (6d * yCurveA[2] * xCurveB[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] * xCurveB[0]) - (6d * xCurveA[3] * xCurveB[2] * c13y3 * xCurveB[0]) + (6d * xCurveB[3] * xCurveB[2] * c13y3 * xCurveB[0]) + (2d * xCurveB[2] * c12y3 * xCurveA[0] * xCurveB[0]) + (6d * xCurveA[3] * xCurveB[2] * xCurveA[0] * c13y2 * yCurveB[0]) + (6d * xCurveA[3] * xCurveA[0] * yCurveB[2] * c13y2 * xCurveB[0]) + (6d * xCurveA[3] * xCurveA[0] * xCurveB[1] * c13y2 * yCurveB[1]) + (6d * yCurveA[3] * xCurveB[2] * xCurveA[0] * c13y2 * xCurveB[0]) - (3d * xCurveA[2] * xCurveA[1] * xCurveB[2] * c13y2 * yCurveB[0]) - (3d * xCurveA[2] * xCurveA[1] * yCurveB[2] * c13y2 * xCurveB[0]) - (3d * xCurveA[2] * xCurveA[1] * xCurveB[1] * c13y2 * yCurveB[1]) + (2d * xCurveA[2] * xCurveB[2] * yCurveA[1] * c13y2 * xCurveB[0]) + (4d * yCurveA[2] * xCurveA[1] * xCurveB[2] * c13y2 * xCurveB[0]) - (6d * yCurveA[3] * xCurveB[2] * c13x2 * yCurveA[0] * yCurveB[0]) - (6d * yCurveA[3] * c13x2 * yCurveB[2] * yCurveA[0] * xCurveB[0]) - (6d * yCurveA[3] * c13x2 * xCurveB[1] * yCurveA[0] * yCurveB[1]) - (6d * xCurveB[3] * xCurveB[2] * xCurveA[0] * c13y2 * yCurveB[0]) - (6d * xCurveB[3] * xCurveA[0] * yCurveB[2] * c13y2 * xCurveB[0]) - (6d * xCurveB[3] * xCurveA[0] * xCurveB[1] * c13y2 * yCurveB[1]) + (3d * yCurveA[2] * xCurveB[2] * yCurveA[1] * c13x2 * yCurveB[0]) - (3d * yCurveA[2] * yCurveA[1] * xCurveA[0] * c22x2 * yCurveA[0]) + (3d * yCurveA[2] * yCurveA[1] * c13x2 * yCurveB[2] * xCurveB[0]) + (3d * yCurveA[2] * yCurveA[1] * c13x2 * xCurveB[1] * yCurveB[1]) - (2d * xCurveA[1] * xCurveB[2] * c12y2 * xCurveA[0] * yCurveB[0]) - (2d * xCurveA[1] * xCurveB[2] * c12y2 * yCurveA[0] * xCurveB[0]) - (2d * xCurveA[1] * c12y2 * xCurveA[0] * yCurveB[2] * xCurveB[0]) - (2d * xCurveA[1] * c12y2 * xCurveA[0] * xCurveB[1] * yCurveB[1]) - (6d * yCurveB[3] * xCurveB[2] * xCurveA[0] * c13y2 * xCurveB[0]) - (6d * xCurveB[2] * xCurveA[0] * yCurveB[2] * xCurveB[1] * c13y2) + (6d * yCurveB[3] * c13x2 * yCurveB[2] * yCurveA[0] * xCurveB[0]) + (2d * c12x2 * xCurveB[2] * yCurveA[1] * yCurveA[0] * yCurveB[0]) + (2d * c12x2 * yCurveA[1] * yCurveB[2] * yCurveA[0] * xCurveB[0]) + (2d * c12x2 * yCurveA[1] * xCurveB[1] * yCurveA[0] * yCurveB[1]) - (3d * xCurveA[3] * c22x2 * c13y3) + (3d * xCurveB[3] * c22x2 * c13y3) + (3d * c21x2 * xCurveB[1] * c13y3) + (c12y3 * xCurveA[0] * c22x2) + (3d * yCurveA[3] * xCurveA[0] * c22x2 * c13y2) + (xCurveA[2] * yCurveA[1] * c22x2 * c13y2) + (2 * yCurveA[2] * xCurveA[1] * c22x2 * c13y2) - (xCurveA[1] * c12y2 * c22x2 * yCurveA[0]) - (3d * yCurveB[3] * xCurveA[0] * c22x2 * c13y2) - (3d * c21x2 * xCurveA[0] * c13y2 * yCurveB[1]) + (c12x2 * yCurveA[1] * xCurveA[0] * ((2d * yCurveB[2] * yCurveB[0]) + c22y2)) + (xCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * ((6d * yCurveB[2] * yCurveB[0]) + (3d * c22y2))) + (xCurveB[2] * c13x2 * yCurveA[0] * ((6d * yCurveB[3] * yCurveB[0]) + (6d * yCurveB[2] * yCurveB[1]))) + (c12x3 * yCurveA[0] * ((-2d * yCurveB[2] * yCurveB[0]) - c22y2)) + (yCurveA[3] * c13x3 * ((6d * yCurveB[2] * yCurveB[0]) + (3d * c22y2))) + (yCurveA[2] * xCurveA[1] * c13x2 * ((-2d * yCurveB[2] * yCurveB[0]) - c22y2)) + (xCurveA[2] * yCurveA[1] * c13x2 * ((-4d * yCurveB[2] * yCurveB[0]) - (2d * c22y2))) + (xCurveA[3] * c13x2 * yCurveA[0] * ((-6d * yCurveB[2] * yCurveB[0]) - (3d * c22y2))) + (c13x2 * xCurveB[1] * yCurveA[0] * ((6d * yCurveB[3] * yCurveB[1]) + (3d * c21y2))) + (xCurveB[3] * c13x2 * yCurveA[0] * ((6d * yCurveB[2] * yCurveB[0]) + (3d * c22y2))) + (c13x3 * ((-2d * yCurveB[3] * yCurveB[2] * yCurveB[0]) - (yCurveB[1] * ((2d * yCurveB[3] * yCurveB[1]) + c21y2)) - (yCurveB[3] * ((2d * yCurveB[2] * yCurveB[0]) + c22y2)) - (yCurveB[2] * ((2d * yCurveB[3] * yCurveB[0]) + (2d * yCurveB[2] * yCurveB[1]))))),
                /* x³ */ (-xCurveA[3] * xCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[0]) + (xCurveA[3] * yCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[0]) + (6d * xCurveA[3] * yCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] * xCurveB[0]) - (6d * yCurveA[3] * xCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[0]) - (yCurveA[3] * xCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] * xCurveB[0]) + (yCurveA[3] * yCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * xCurveB[0]) + (xCurveA[2] * yCurveA[2] * xCurveA[1] * yCurveA[1] * xCurveA[0] * yCurveB[0]) - (xCurveA[2] * yCurveA[2] * xCurveA[1] * yCurveA[1] * yCurveA[0] * xCurveB[0]) + (xCurveA[2] * xCurveB[3] * yCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[0]) + (xCurveA[2] * yCurveB[3] * yCurveA[1] * xCurveA[0] * yCurveA[0] * xCurveB[0]) + (xCurveA[2] * xCurveB[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[1]) + (xCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveB[2] * xCurveB[1] * yCurveA[0]) - (xCurveB[3] * yCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[0]) - (6d * xCurveB[3] * yCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] * xCurveB[0]) - (yCurveA[2] * xCurveA[1] * yCurveB[3] * xCurveA[0] * yCurveA[0] * xCurveB[0]) - (yCurveA[2] * xCurveA[1] * xCurveB[2] * xCurveA[0] * yCurveA[0] * yCurveB[1]) - (yCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveB[2] * xCurveB[1] * yCurveA[0]) - (6d * yCurveA[2] * xCurveB[2] * yCurveA[1] * xCurveA[0] * xCurveB[1] * yCurveA[0]) - (6d * xCurveA[3] * xCurveB[3] * c13y3 * xCurveB[0]) - (6d * xCurveA[3] * xCurveB[2] * xCurveB[1] * c13y3) - (2d * xCurveA[3] * c12y3 * xCurveA[0] * xCurveB[0]) + (6d * xCurveB[3] * xCurveB[2] * xCurveB[1] * c13y3) + (2d * xCurveB[3] * c12y3 * xCurveA[0] * xCurveB[0]) + (2d * xCurveB[2] * c12y3 * xCurveA[0] * xCurveB[1]) + (2d * yCurveA[3] * c12x3 * yCurveA[0] * yCurveB[0]) - (6d * xCurveA[3] * yCurveA[3] * xCurveA[0] * c13y2 * xCurveB[0]) + (3d * xCurveA[3] * xCurveA[2] * xCurveA[1] * c13y2 * yCurveB[0]) - (2d * xCurveA[3] * xCurveA[2] * yCurveA[1] * c13y2 * xCurveB[0]) - (4d * xCurveA[3] * yCurveA[2] * xCurveA[1] * c13y2 * xCurveB[0]) + (3d * yCurveA[3] * xCurveA[2] * xCurveA[1] * c13y2 * xCurveB[0]) + (6d * xCurveA[3] * yCurveA[3] * c13x2 * yCurveA[0] * yCurveB[0]) + (6d * xCurveA[3] * xCurveB[3] * xCurveA[0] * c13y2 * yCurveB[0]) - (3d * xCurveA[3] * yCurveA[2] * yCurveA[1] * c13x2 * yCurveB[0]) + (2d * xCurveA[3] * xCurveA[1] * c12y2 * xCurveA[0] * yCurveB[0]) + (2d * xCurveA[3] * xCurveA[1] * c12y2 * yCurveA[0] * xCurveB[0]) + (6d * xCurveA[3] * yCurveB[3] * xCurveA[0] * c13y2 * xCurveB[0]) + (6d * xCurveA[3] * xCurveB[2] * xCurveA[0] * c13y2 * yCurveB[1]) + (6d * xCurveA[3] * xCurveA[0] * yCurveB[2] * xCurveB[1] * c13y2) + (4 * yCurveA[3] * xCurveA[2] * yCurveA[1] * c13x2 * yCurveB[0]) + (6d * yCurveA[3] * xCurveB[3] * xCurveA[0] * c13y2 * xCurveB[0]) + (2d * yCurveA[3] * yCurveA[2] * xCurveA[1] * c13x2 * yCurveB[0]) - (3d * yCurveA[3] * yCurveA[2] * yCurveA[1] * c13x2 * xCurveB[0]) + (2 * yCurveA[3] * xCurveA[1] * c12y2 * xCurveA[0] * xCurveB[0]) + (6d * yCurveA[3] * xCurveB[2] * xCurveA[0] * xCurveB[1] * c13y2) - (3d * xCurveA[2] * xCurveB[3] * xCurveA[1] * c13y2 * yCurveB[0]) + (2 * xCurveA[2] * xCurveB[3] * yCurveA[1] * c13y2 * xCurveB[0]) + (xCurveA[2] * yCurveA[2] * c12y2 * xCurveA[0] * xCurveB[0]) - (3d * xCurveA[2] * xCurveA[1] * yCurveB[3] * c13y2 * xCurveB[0]) - (3d * xCurveA[2] * xCurveA[1] * xCurveB[2] * c13y2 * yCurveB[1]) - (3d * xCurveA[2] * xCurveA[1] * yCurveB[2] * xCurveB[1] * c13y2) + (2d * xCurveA[2] * xCurveB[2] * yCurveA[1] * xCurveB[1] * c13y2) + (4d * xCurveB[3] * yCurveA[2] * xCurveA[1] * c13y2 * xCurveB[0]) + (4d * yCurveA[2] * xCurveA[1] * xCurveB[2] * xCurveB[1] * c13y2) - (2d * xCurveA[3] * c12x2 * yCurveA[1] * yCurveA[0] * yCurveB[0]) - (6d * yCurveA[3] * xCurveB[3] * c13x2 * yCurveA[0] * yCurveB[0]) - (6d * yCurveA[3] * yCurveB[3] * c13x2 * yCurveA[0] * xCurveB[0]) - (6d * yCurveA[3] * xCurveB[2] * c13x2 * yCurveA[0] * yCurveB[1]) - (2d * yCurveA[3] * c12x2 * yCurveA[1] * xCurveA[0] * yCurveB[0]) - (2d * yCurveA[3] * c12x2 * yCurveA[1] * yCurveA[0] * xCurveB[0]) - (6d * yCurveA[3] * c13x2 * yCurveB[2] * xCurveB[1] * yCurveA[0]) - (xCurveA[2] * yCurveA[2] * c12x2 * yCurveA[0] * yCurveB[0]) - (2d * xCurveA[2] * c11y2 * xCurveA[0] * yCurveA[0] * xCurveB[0]) + (3d * xCurveB[3] * yCurveA[2] * yCurveA[1] * c13x2 * yCurveB[0]) - (2d * xCurveB[3] * xCurveA[1] * c12y2 * xCurveA[0] * yCurveB[0]) - (2d * xCurveB[3] * xCurveA[1] * c12y2 * yCurveA[0] * xCurveB[0]) - (6d * xCurveB[3] * yCurveB[3] * xCurveA[0] * c13y2 * xCurveB[0]) - (6d * xCurveB[3] * xCurveB[2] * xCurveA[0] * c13y2 * yCurveB[1]) - (6d * xCurveB[3] * xCurveA[0] * yCurveB[2] * xCurveB[1] * c13y2) + (3d * yCurveA[2] * yCurveB[3] * yCurveA[1] * c13x2 * xCurveB[0]) + (3d * yCurveA[2] * xCurveB[2] * yCurveA[1] * c13x2 * yCurveB[1]) + (3d * yCurveA[2] * yCurveA[1] * c13x2 * yCurveB[2] * xCurveB[1]) - (2d * xCurveA[1] * yCurveB[3] * c12y2 * xCurveA[0] * xCurveB[0]) - (2d * xCurveA[1] * xCurveB[2] * c12y2 * xCurveA[0] * yCurveB[1]) - (2d * xCurveA[1] * xCurveB[2] * c12y2 * xCurveB[1] * yCurveA[0]) - (2d * xCurveA[1] * c12y2 * xCurveA[0] * yCurveB[2] * xCurveB[1]) - (6d * yCurveB[3] * xCurveB[2] * xCurveA[0] * xCurveB[1] * c13y2) - (c11y2 * xCurveA[1] * yCurveA[1] * xCurveA[0] * xCurveB[0]) + (2d * xCurveB[3] * c12x2 * yCurveA[1] * yCurveA[0] * yCurveB[0]) + (6d * yCurveB[3] * c13x2 * yCurveB[2] * xCurveB[1] * yCurveA[0]) + (2d * c11x2 * yCurveA[2] * xCurveA[0] * yCurveA[0] * yCurveB[0]) + (c11x2 * xCurveA[1] * yCurveA[1] * yCurveA[0] * yCurveB[0]) + (2d * c12x2 * yCurveB[3] * yCurveA[1] * yCurveA[0] * xCurveB[0]) + (2d * c12x2 * xCurveB[2] * yCurveA[1] * yCurveA[0] * yCurveB[1]) + (2d * c12x2 * yCurveA[1] * yCurveB[2] * xCurveB[1] * yCurveA[0]) + (c21x3 * c13y3) + (3d * c10x2 * c13y3 * xCurveB[0]) - (3d * c10y2 * c13x3 * yCurveB[0]) + (3d * c20x2 * c13y3 * xCurveB[0]) + (c11y3 * c13x2 * xCurveB[0]) - (c11x3 * c13y2 * yCurveB[0]) - (xCurveA[2] * c11y2 * c13x2 * yCurveB[0]) + (c11x2 * yCurveA[2] * c13y2 * xCurveB[0]) - (3d * c10x2 * xCurveA[0] * c13y2 * yCurveB[0]) + (3d * c10y2 * c13x2 * yCurveA[0] * xCurveB[0]) - (c11x2 * c12y2 * xCurveA[0] * yCurveB[0]) + (c11y2 * c12x2 * yCurveA[0] * xCurveB[0]) - (3d * c21x2 * xCurveA[0] * yCurveB[2] * c13y2) - (3d * c20x2 * xCurveA[0] * c13y2 * yCurveB[0]) + (3d * c20y2 * c13x2 * yCurveA[0] * xCurveB[0]) + (xCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * ((6d * yCurveB[3] * yCurveB[0]) + (6d * yCurveB[2] * yCurveB[1]))) + (c12x3 * yCurveA[0] * ((-2d * yCurveB[3] * yCurveB[0]) - (2d * yCurveB[2] * yCurveB[1]))) + (yCurveA[3] * c13x3 * ((6d * yCurveB[3] * yCurveB[0]) + (6d * yCurveB[2] * yCurveB[1]))) + (yCurveA[2] * xCurveA[1] * c13x2 * ((-2d * yCurveB[3] * yCurveB[0]) - (2d * yCurveB[2] * yCurveB[1]))) + (c12x2 * yCurveA[1] * xCurveA[0] * ((2d * yCurveB[3] * yCurveB[0]) + (2d * yCurveB[2] * yCurveB[1]))) + (xCurveA[2] * yCurveA[1] * c13x2 * ((-4d * yCurveB[3] * yCurveB[0]) - (4d * yCurveB[2] * yCurveB[1]))) + (xCurveA[3] * c13x2 * yCurveA[0] * ((-6d * yCurveB[3] * yCurveB[0]) - (6d * yCurveB[2] * yCurveB[1]))) + (xCurveB[3] * c13x2 * yCurveA[0] * ((6d * yCurveB[3] * yCurveB[0]) + (6d * yCurveB[2] * yCurveB[1]))) + (xCurveB[2] * c13x2 * yCurveA[0] * ((6d * yCurveB[3] * yCurveB[1]) + (3d * c21y2))) + (c13x3 * ((-2d * yCurveB[3] * yCurveB[2] * yCurveB[1]) - (c20y2 * yCurveB[0]) - (yCurveB[2] * ((2d * yCurveB[3] * yCurveB[1]) + c21y2)) - (yCurveB[3] * ((2d * yCurveB[3] * yCurveB[0]) + (2d * yCurveB[2] * yCurveB[1]))))),
                /* x² */ (-xCurveA[3] * xCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[1]) + (xCurveA[3] * yCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[1]) + (6d * xCurveA[3] * yCurveA[2] * yCurveA[1] * xCurveA[0] * xCurveB[1] * yCurveA[0]) - (6d * yCurveA[3] * xCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[1]) - (yCurveA[3] * xCurveA[2] * yCurveA[1] * xCurveA[0] * xCurveB[1] * yCurveA[0]) + (yCurveA[3] * yCurveA[2] * xCurveA[1] * xCurveA[0] * xCurveB[1] * yCurveA[0]) + (xCurveA[2] * yCurveA[2] * xCurveA[1] * yCurveA[1] * xCurveA[0] * yCurveB[1]) - (xCurveA[2] * yCurveA[2] * xCurveA[1] * yCurveA[1] * xCurveB[1] * yCurveA[0]) + (xCurveA[2] * xCurveB[3] * yCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[1]) + (xCurveA[2] * yCurveB[3] * yCurveA[1] * xCurveA[0] * xCurveB[1] * yCurveA[0]) + (xCurveA[2] * xCurveB[2] * yCurveA[1] * xCurveA[0] * yCurveB[2] * yCurveA[0]) - (xCurveB[3] * yCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * yCurveB[1]) - (6d * xCurveB[3] * yCurveA[2] * yCurveA[1] * xCurveA[0] * xCurveB[1] * yCurveA[0]) - (yCurveA[2] * xCurveA[1] * yCurveB[3] * xCurveA[0] * xCurveB[1] * yCurveA[0]) - (yCurveA[2] * xCurveA[1] * xCurveB[2] * xCurveA[0] * yCurveB[2] * yCurveA[0]) - (6d * xCurveA[3] * xCurveB[3] * xCurveB[1] * c13y3) - (2d * xCurveA[3] * c12y3 * xCurveA[0] * xCurveB[1]) + (2d * xCurveB[3] * c12y3 * xCurveA[0] * xCurveB[1]) + (2d * yCurveA[3] * c12x3 * yCurveA[0] * yCurveB[1]) - (6d * xCurveA[3] * yCurveA[3] * xCurveA[0] * xCurveB[1] * c13y2) + (3d * xCurveA[3] * xCurveA[2] * xCurveA[1] * c13y2 * yCurveB[1]) - (2d * xCurveA[3] * xCurveA[2] * yCurveA[1] * xCurveB[1] * c13y2) - (4d * xCurveA[3] * yCurveA[2] * xCurveA[1] * xCurveB[1] * c13y2) + (3d * yCurveA[3] * xCurveA[2] * xCurveA[1] * xCurveB[1] * c13y2) + (6d * xCurveA[3] * yCurveA[3] * c13x2 * yCurveA[0] * yCurveB[1]) + (6d * xCurveA[3] * xCurveB[3] * xCurveA[0] * c13y2 * yCurveB[1]) - (3d * xCurveA[3] * yCurveA[2] * yCurveA[1] * c13x2 * yCurveB[1]) + (2d * xCurveA[3] * xCurveA[1] * c12y2 * xCurveA[0] * yCurveB[1]) + (2d * xCurveA[3] * xCurveA[1] * c12y2 * xCurveB[1] * yCurveA[0]) + (6d * xCurveA[3] * yCurveB[3] * xCurveA[0] * xCurveB[1] * c13y2) + (6d * xCurveA[3] * xCurveB[2] * xCurveA[0] * yCurveB[2] * c13y2) + (4d * yCurveA[3] * xCurveA[2] * yCurveA[1] * c13x2 * yCurveB[1]) + (6d * yCurveA[3] * xCurveB[3] * xCurveA[0] * xCurveB[1] * c13y2) + (2d * yCurveA[3] * yCurveA[2] * xCurveA[1] * c13x2 * yCurveB[1]) - (3d * yCurveA[3] * yCurveA[2] * yCurveA[1] * c13x2 * xCurveB[1]) + (2d * yCurveA[3] * xCurveA[1] * c12y2 * xCurveA[0] * xCurveB[1]) - (3d * xCurveA[2] * xCurveB[3] * xCurveA[1] * c13y2 * yCurveB[1]) + (2d * xCurveA[2] * xCurveB[3] * yCurveA[1] * xCurveB[1] * c13y2) + (xCurveA[2] * yCurveA[2] * c12y2 * xCurveA[0] * xCurveB[1]) - (3d * xCurveA[2] * xCurveA[1] * yCurveB[3] * xCurveB[1] * c13y2) - (3d * xCurveA[2] * xCurveA[1] * xCurveB[2] * yCurveB[2] * c13y2) + (4d * xCurveB[3] * yCurveA[2] * xCurveA[1] * xCurveB[1] * c13y2) - (2d * xCurveA[3] * c12x2 * yCurveA[1] * yCurveA[0] * yCurveB[1]) - (6d * yCurveA[3] * xCurveB[3] * c13x2 * yCurveA[0] * yCurveB[1]) - (6d * yCurveA[3] * yCurveB[3] * c13x2 * xCurveB[1] * yCurveA[0]) - (6d * yCurveA[3] * xCurveB[2] * c13x2 * yCurveB[2] * yCurveA[0]) - (2d * yCurveA[3] * c12x2 * yCurveA[1] * xCurveA[0] * yCurveB[1]) - (2d * yCurveA[3] * c12x2 * yCurveA[1] * xCurveB[1] * yCurveA[0]) - (xCurveA[2] * yCurveA[2] * c12x2 * yCurveA[0] * yCurveB[1]) - (2d * xCurveA[2] * c11y2 * xCurveA[0] * xCurveB[1] * yCurveA[0]) + (3d * xCurveB[3] * yCurveA[2] * yCurveA[1] * c13x2 * yCurveB[1]) - (2d * xCurveB[3] * xCurveA[1] * c12y2 * xCurveA[0] * yCurveB[1]) - (2d * xCurveB[3] * xCurveA[1] * c12y2 * xCurveB[1] * yCurveA[0]) - (6d * xCurveB[3] * yCurveB[3] * xCurveA[0] * xCurveB[1] * c13y2) - (6d * xCurveB[3] * xCurveB[2] * xCurveA[0] * yCurveB[2] * c13y2) + (3d * yCurveA[2] * yCurveB[3] * yCurveA[1] * c13x2 * xCurveB[1]) + (3d * yCurveA[2] * xCurveB[2] * yCurveA[1] * c13x2 * yCurveB[2]) - (2d * xCurveA[1] * yCurveB[3] * c12y2 * xCurveA[0] * xCurveB[1]) - (2d * xCurveA[1] * xCurveB[2] * c12y2 * xCurveA[0] * yCurveB[2]) - (c11y2 * xCurveA[1] * yCurveA[1] * xCurveA[0] * xCurveB[1]) + (2d * xCurveB[3] * c12x2 * yCurveA[1] * yCurveA[0] * yCurveB[1]) - (3d * yCurveA[2] * c21x2 * yCurveA[1] * xCurveA[0] * yCurveA[0]) + (6d * yCurveB[3] * xCurveB[2] * c13x2 * yCurveB[2] * yCurveA[0]) + (2d * c11x2 * yCurveA[2] * xCurveA[0] * yCurveA[0] * yCurveB[1]) + (c11x2 * xCurveA[1] * yCurveA[1] * yCurveA[0] * yCurveB[1]) + (2d * c12x2 * yCurveB[3] * yCurveA[1] * xCurveB[1] * yCurveA[0]) + (2 * c12x2 * xCurveB[2] * yCurveA[1] * yCurveB[2] * yCurveA[0]) - (3d * xCurveA[3] * c21x2 * c13y3) + (3d * xCurveB[3] * c21x2 * c13y3) + (3d * c10x2 * xCurveB[1] * c13y3) - (3d * c10y2 * c13x3 * yCurveB[1]) + (3d * c20x2 * xCurveB[1] * c13y3) + (c21x2 * c12y3 * xCurveA[0]) + (c11y3 * c13x2 * xCurveB[1]) - (c11x3 * c13y2 * yCurveB[1]) + (3d * yCurveA[3] * c21x2 * xCurveA[0] * c13y2) - (xCurveA[2] * c11y2 * c13x2 * yCurveB[1]) + (xCurveA[2] * c21x2 * yCurveA[1] * c13y2) + (2d * yCurveA[2] * xCurveA[1] * c21x2 * c13y2) + (c11x2 * yCurveA[2] * xCurveB[1] * c13y2) - (xCurveA[1] * c21x2 * c12y2 * yCurveA[0]) - (3d * yCurveB[3] * c21x2 * xCurveA[0] * c13y2) - (3d * c10x2 * xCurveA[0] * c13y2 * yCurveB[1]) + (3d * c10y2 * c13x2 * xCurveB[1] * yCurveA[0]) - (c11x2 * c12y2 * xCurveA[0] * yCurveB[1]) + (c11y2 * c12x2 * xCurveB[1] * yCurveA[0]) - (3d * c20x2 * xCurveA[0] * c13y2 * yCurveB[1]) + (3d * c20y2 * c13x2 * xCurveB[1] * yCurveA[0]) + (c12x2 * yCurveA[1] * xCurveA[0] * ((2d * yCurveB[3] * yCurveB[1]) + c21y2)) + (xCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0] * ((6d * yCurveB[3] * yCurveB[1]) + (3d * c21y2))) + (c12x3 * yCurveA[0] * ((-2d * yCurveB[3] * yCurveB[1]) - c21y2)) + (yCurveA[3] * c13x3 * ((6d * yCurveB[3] * yCurveB[1]) + (3d * c21y2))) + (yCurveA[2] * xCurveA[1] * c13x2 * ((-2d * yCurveB[3] * yCurveB[1]) - c21y2)) + (xCurveA[2] * yCurveA[1] * c13x2 * ((-4d * yCurveB[3] * yCurveB[1]) - (2d * c21y2))) + (xCurveA[3] * c13x2 * yCurveA[0] * ((-6d * yCurveB[3] * yCurveB[1]) - (3d * c21y2))) + (xCurveB[3] * c13x2 * yCurveA[0] * ((6d * yCurveB[3] * yCurveB[1]) + (3d * c21y2))) + (c13x3 * ((-2d * yCurveB[3] * c21y2) - (c20y2 * yCurveB[1]) - (yCurveB[3] * ((2d * yCurveB[3] * yCurveB[1]) + c21y2)))),
                /* x¹ */ (-xCurveA[3] * xCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveB[2] * yCurveA[0]) + (xCurveA[3] * yCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveB[2] * yCurveA[0]) + (6d * xCurveA[3] * yCurveA[2] * xCurveB[2] * yCurveA[1] * xCurveA[0] * yCurveA[0]) - (6d * yCurveA[3] * xCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveB[2] * yCurveA[0]) - (yCurveA[3] * xCurveA[2] * xCurveB[2] * yCurveA[1] * xCurveA[0] * yCurveA[0]) + (yCurveA[3] * yCurveA[2] * xCurveA[1] * xCurveB[2] * xCurveA[0] * yCurveA[0]) - (xCurveA[2] * yCurveA[2] * xCurveA[1] * xCurveB[2] * yCurveA[1] * yCurveA[0]) + (xCurveA[2] * yCurveA[2] * xCurveA[1] * yCurveA[1] * xCurveA[0] * yCurveB[2]) + (xCurveA[2] * xCurveB[3] * yCurveA[1] * xCurveA[0] * yCurveB[2] * yCurveA[0]) + (6d * xCurveA[2] * xCurveA[1] * yCurveB[3] * xCurveA[0] * yCurveB[2] * yCurveA[0]) + (xCurveA[2] * yCurveB[3] * xCurveB[2] * yCurveA[1] * xCurveA[0] * yCurveA[0]) - (xCurveB[3] * yCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveB[2] * yCurveA[0]) - (6d * xCurveB[3] * yCurveA[2] * xCurveB[2] * yCurveA[1] * xCurveA[0] * yCurveA[0]) - (yCurveA[2] * xCurveA[1] * yCurveB[3] * xCurveB[2] * xCurveA[0] * yCurveA[0]) - (6d * xCurveA[3] * xCurveB[3] * xCurveB[2] * c13y3) - (2d * xCurveA[3] * xCurveB[2] * c12y3 * xCurveA[0]) + (6d * yCurveA[3] * yCurveB[3] * c13x3 * yCurveB[2]) + (2d * xCurveB[3] * xCurveB[2] * c12y3 * xCurveA[0]) + (2d * yCurveA[3] * c12x3 * yCurveB[2] * yCurveA[0]) - (2d * c12x3 * yCurveB[3] * yCurveB[2] * yCurveA[0]) - (6d * xCurveA[3] * yCurveA[3] * xCurveB[2] * xCurveA[0] * c13y2) + (3d * xCurveA[3] * xCurveA[2] * xCurveA[1] * yCurveB[2] * c13y2) - (2d * xCurveA[3] * xCurveA[2] * xCurveB[2] * yCurveA[1] * c13y2) - (4d * xCurveA[3] * yCurveA[2] * xCurveA[1] * xCurveB[2] * c13y2) + (3d * yCurveA[3] * xCurveA[2] * xCurveA[1] * xCurveB[2] * c13y2) + (6d * xCurveA[3] * yCurveA[3] * c13x2 * yCurveB[2] * yCurveA[0]) + (6d * xCurveA[3] * xCurveB[3] * xCurveA[0] * yCurveB[2] * c13y2) - (3d * xCurveA[3] * yCurveA[2] * yCurveA[1] * c13x2 * yCurveB[2]) + (2d * xCurveA[3] * xCurveA[1] * xCurveB[2] * c12y2 * yCurveA[0]) + (2d * xCurveA[3] * xCurveA[1] * c12y2 * xCurveA[0] * yCurveB[2]) + (6d * xCurveA[3] * yCurveB[3] * xCurveB[2] * xCurveA[0] * c13y2) + (4d * yCurveA[3] * xCurveA[2] * yCurveA[1] * c13x2 * yCurveB[2]) + (6d * yCurveA[3] * xCurveB[3] * xCurveB[2] * xCurveA[0] * c13y2) + (2d * yCurveA[3] * yCurveA[2] * xCurveA[1] * c13x2 * yCurveB[2]) - (3d * yCurveA[3] * yCurveA[2] * xCurveB[2] * yCurveA[1] * c13x2) + (2d * yCurveA[3] * xCurveA[1] * xCurveB[2] * c12y2 * xCurveA[0]) - (3d * xCurveA[2] * xCurveB[3] * xCurveA[1] * yCurveB[2] * c13y2) + (2d * xCurveA[2] * xCurveB[3] * xCurveB[2] * yCurveA[1] * c13y2) + (xCurveA[2] * yCurveA[2] * xCurveB[2] * c12y2 * xCurveA[0]) - (3d * xCurveA[2] * xCurveA[1] * yCurveB[3] * xCurveB[2] * c13y2) + (4d * xCurveB[3] * yCurveA[2] * xCurveA[1] * xCurveB[2] * c13y2) - (6d * xCurveA[3] * yCurveB[3] * c13x2 * yCurveB[2] * yCurveA[0]) - (2d * xCurveA[3] * c12x2 * yCurveA[1] * yCurveB[2] * yCurveA[0]) - (6d * yCurveA[3] * xCurveB[3] * c13x2 * yCurveB[2] * yCurveA[0]) - (6d * yCurveA[3] * yCurveB[3] * xCurveB[2] * c13x2 * yCurveA[0]) - (2d * yCurveA[3] * c12x2 * xCurveB[2] * yCurveA[1] * yCurveA[0]) - (2d * yCurveA[3] * c12x2 * yCurveA[1] * xCurveA[0] * yCurveB[2]) - (xCurveA[2] * yCurveA[2] * c12x2 * yCurveB[2] * yCurveA[0]) - (4d * xCurveA[2] * yCurveB[3] * yCurveA[1] * c13x2 * yCurveB[2]) - (2d * xCurveA[2] * c11y2 * xCurveB[2] * xCurveA[0] * yCurveA[0]) + (3d * xCurveB[3] * yCurveA[2] * yCurveA[1] * c13x2 * yCurveB[2]) - (2d * xCurveB[3] * xCurveA[1] * xCurveB[2] * c12y2 * yCurveA[0]) - (2d * xCurveB[3] * xCurveA[1] * c12y2 * xCurveA[0] * yCurveB[2]) - (6d * xCurveB[3] * yCurveB[3] * xCurveB[2] * xCurveA[0] * c13y2) - (2d * yCurveA[2] * xCurveA[1] * yCurveB[3] * c13x2 * yCurveB[2]) + (3d * yCurveA[2] * yCurveB[3] * xCurveB[2] * yCurveA[1] * c13x2) - (2d * xCurveA[1] * yCurveB[3] * xCurveB[2] * c12y2 * xCurveA[0]) - (c11y2 * xCurveA[1] * xCurveB[2] * yCurveA[1] * xCurveA[0]) + (6d * xCurveB[3] * yCurveB[3] * c13x2 * yCurveB[2] * yCurveA[0]) + (2d * xCurveB[3] * c12x2 * yCurveA[1] * yCurveB[2] * yCurveA[0]) + (2d * c11x2 * yCurveA[2] * xCurveA[0] * yCurveB[2] * yCurveA[0]) + (c11x2 * xCurveA[1] * yCurveA[1] * yCurveB[2] * yCurveA[0]) + (2d * c12x2 * yCurveB[3] * xCurveB[2] * yCurveA[1] * yCurveA[0]) + (2d * c12x2 * yCurveB[3] * yCurveA[1] * xCurveA[0] * yCurveB[2]) + (3d * c10x2 * xCurveB[2] * c13y3) - (3d * c10y2 * c13x3 * yCurveB[2]) + (3d * c20x2 * xCurveB[2] * c13y3) + (c11y3 * xCurveB[2] * c13x2) - (c11x3 * yCurveB[2] * c13y2) - (3d * c20y2 * c13x3 * yCurveB[2]) - (xCurveA[2] * c11y2 * c13x2 * yCurveB[2]) + (c11x2 * yCurveA[2] * xCurveB[2] * c13y2) - (3d * c10x2 * xCurveA[0] * yCurveB[2] * c13y2) + (3d * c10y2 * xCurveB[2] * c13x2 * yCurveA[0]) - (c11x2 * c12y2 * xCurveA[0] * yCurveB[2]) + (c11y2 * c12x2 * xCurveB[2] * yCurveA[0]) - (3d * c20x2 * xCurveA[0] * yCurveB[2] * c13y2) + (3d * c20y2 * xCurveB[2] * c13x2 * yCurveA[0]),
                /* c  */ (xCurveA[3] * yCurveA[3] * xCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveA[0]) - (xCurveA[3] * yCurveA[3] * yCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0]) + (xCurveA[3] * xCurveA[2] * yCurveA[2] * xCurveA[1] * yCurveA[1] * yCurveA[0]) - (yCurveA[3] * xCurveA[2] * yCurveA[2] * xCurveA[1] * yCurveA[1] * xCurveA[0]) - (xCurveA[3] * xCurveA[2] * yCurveB[3] * yCurveA[1] * xCurveA[0] * yCurveA[0]) + (6d * xCurveA[3] * xCurveB[3] * yCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveA[0]) + (xCurveA[3] * yCurveA[2] * xCurveA[1] * yCurveB[3] * xCurveA[0] * yCurveA[0]) - (yCurveA[3] * xCurveA[2] * xCurveB[3] * yCurveA[1] * xCurveA[0] * yCurveA[0]) - (6d * yCurveA[3] * xCurveA[2] * xCurveA[1] * yCurveB[3] * xCurveA[0] * yCurveA[0]) + (yCurveA[3] * xCurveB[3] * yCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0]) - (xCurveA[2] * xCurveB[3] * yCurveA[2] * xCurveA[1] * yCurveA[1] * yCurveA[0]) + (xCurveA[2] * yCurveA[2] * xCurveA[1] * yCurveB[3] * yCurveA[1] * xCurveA[0]) + (xCurveA[2] * xCurveB[3] * yCurveB[3] * yCurveA[1] * xCurveA[0] * yCurveA[0]) - (xCurveB[3] * yCurveA[2] * xCurveA[1] * yCurveB[3] * xCurveA[0] * yCurveA[0]) - (2 * xCurveA[3] * xCurveB[3] * c12y3 * xCurveA[0]) + (2d * yCurveA[3] * c12x3 * yCurveB[3] * yCurveA[0]) - (3d * xCurveA[3] * yCurveA[3] * xCurveA[2] * xCurveA[1] * c13y2) - (6d * xCurveA[3] * yCurveA[3] * xCurveB[3] * xCurveA[0] * c13y2) + (3d * xCurveA[3] * yCurveA[3] * yCurveA[2] * yCurveA[1] * c13x2) - (2d * xCurveA[3] * yCurveA[3] * xCurveA[1] * c12y2 * xCurveA[0]) - (2d * xCurveA[3] * xCurveA[2] * xCurveB[3] * yCurveA[1] * c13y2) - (xCurveA[3] * xCurveA[2] * yCurveA[2] * c12y2 * xCurveA[0]) + (3d * xCurveA[3] * xCurveA[2] * xCurveA[1] * yCurveB[3] * c13y2) - (4d * xCurveA[3] * xCurveB[3] * yCurveA[2] * xCurveA[1] * c13y2) + (3d * yCurveA[3] * xCurveA[2] * xCurveB[3] * xCurveA[1] * c13y2) + (6d * xCurveA[3] * yCurveA[3] * yCurveB[3] * c13x2 * yCurveA[0]) + (2d * xCurveA[3] * yCurveA[3] * c12x2 * yCurveA[1] * yCurveA[0]) + (2d * xCurveA[3] * xCurveA[2] * c11y2 * xCurveA[0] * yCurveA[0]) + (2d * xCurveA[3] * xCurveB[3] * xCurveA[1] * c12y2 * yCurveA[0]) + (6d * xCurveA[3] * xCurveB[3] * yCurveB[3] * xCurveA[0] * c13y2) - (3d * xCurveA[3] * yCurveA[2] * yCurveB[3] * yCurveA[1] * c13x2) + (2d * xCurveA[3] * xCurveA[1] * yCurveB[3] * c12y2 * xCurveA[0]) + (xCurveA[3] * c11y2 * xCurveA[1] * yCurveA[1] * xCurveA[0]) + (yCurveA[3] * xCurveA[2] * yCurveA[2] * c12x2 * yCurveA[0]) + (4d * yCurveA[3] * xCurveA[2] * yCurveB[3] * yCurveA[1] * c13x2) - (3d * yCurveA[3] * xCurveB[3] * yCurveA[2] * yCurveA[1] * c13x2) + (2d * yCurveA[3] * xCurveB[3] * xCurveA[1] * c12y2 * xCurveA[0]) + (2d * yCurveA[3] * yCurveA[2] * xCurveA[1] * yCurveB[3] * c13x2) + (xCurveA[2] * xCurveB[3] * yCurveA[2] * c12y2 * xCurveA[0]) - (3d * xCurveA[2] * xCurveB[3] * xCurveA[1] * yCurveB[3] * c13y2) - (2d * xCurveA[3] * c12x2 * yCurveB[3] * yCurveA[1] * yCurveA[0]) - (6d * yCurveA[3] * xCurveB[3] * yCurveB[3] * c13x2 * yCurveA[0]) - (2d * yCurveA[3] * xCurveB[3] * c12x2 * yCurveA[1] * yCurveA[0]) - (2d * yCurveA[3] * c11x2 * yCurveA[2] * xCurveA[0] * yCurveA[0]) - (yCurveA[3] * c11x2 * xCurveA[1] * yCurveA[1] * yCurveA[0]) - (2d * yCurveA[3] * c12x2 * yCurveB[3] * yCurveA[1] * xCurveA[0]) - (2d * xCurveA[2] * xCurveB[3] * c11y2 * xCurveA[0] * yCurveA[0]) - (xCurveA[2] * yCurveA[2] * c12x2 * yCurveB[3] * yCurveA[0]) + (3d * xCurveB[3] * yCurveA[2] * yCurveB[3] * yCurveA[1] * c13x2) - (2d * xCurveB[3] * xCurveA[1] * yCurveB[3] * c12y2 * xCurveA[0]) - (xCurveB[3] * c11y2 * xCurveA[1] * yCurveA[1] * xCurveA[0]) + (3d * c10y2 * xCurveA[2] * xCurveA[1] * xCurveA[0] * yCurveA[0]) + (3 * xCurveA[2] * xCurveA[1] * c20y2 * xCurveA[0] * yCurveA[0]) + (2 * xCurveB[3] * c12x2 * yCurveB[3] * yCurveA[1] * yCurveA[0]) - (3 * c10x2 * yCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveA[0]) + (2 * c11x2 * yCurveA[2] * yCurveB[3] * xCurveA[0] * yCurveA[0]) + (c11x2 * xCurveA[1] * yCurveB[3] * yCurveA[1] * yCurveA[0]) - (3d * c20x2 * yCurveA[2] * yCurveA[1] * xCurveA[0] * yCurveA[0]) - (c10x3 * c13y3) + (c10y3 * c13x3) + (c20x3 * c13y3) - (c20y3 * c13x3) - (3d * xCurveA[3] * c20x2 * c13y3) - (xCurveA[3] * c11y3 * c13x2) + (3d * c10x2 * xCurveB[3] * c13y3) + (yCurveA[3] * c11x3 * c13y2) + (3d * yCurveA[3] * c20y2 * c13x3) + (xCurveB[3] * c11y3 * c13x2) + (c10x2 * c12y3 * xCurveA[0]) - (3d * c10y2 * yCurveB[3] * c13x3) - (c10y2 * c12x3 * yCurveA[0]) + (c20x2 * c12y3 * xCurveA[0]) - (c11x3 * yCurveB[3] * c13y2) - (c12x3 * c20y2 * yCurveA[0]) - (xCurveA[3] * c11x2 * yCurveA[2] * c13y2) + (yCurveA[3] * xCurveA[2] * c11y2 * c13x2) - (3d * xCurveA[3] * c10y2 * c13x2 * yCurveA[0]) - (xCurveA[3] * c11y2 * c12x2 * yCurveA[0]) + (yCurveA[3] * c11x2 * c12y2 * xCurveA[0]) - (xCurveA[2] * c11y2 * yCurveB[3] * c13x2) + (3d * c10x2 * yCurveA[3] * xCurveA[0] * c13y2) + (c10x2 * xCurveA[2] * yCurveA[1] * c13y2) + (2d * c10x2 * yCurveA[2] * xCurveA[1] * c13y2) - (2d * c10y2 * xCurveA[2] * yCurveA[1] * c13x2) - (c10y2 * yCurveA[2] * xCurveA[1] * c13x2) + (c11x2 * xCurveB[3] * yCurveA[2] * c13y2) - (3d * xCurveA[3] * c20y2 * c13x2 * yCurveA[0]) + (3d * yCurveA[3] * c20x2 * xCurveA[0] * c13y2) + (xCurveA[2] * c20x2 * yCurveA[1] * c13y2) - (2d * xCurveA[2] * c20y2 * yCurveA[1] * c13x2) + (xCurveB[3] * c11y2 * c12x2 * yCurveA[0]) - (yCurveA[2] * xCurveA[1] * c20y2 * c13x2) - (c10x2 * xCurveA[1] * c12y2 * yCurveA[0]) - (3d * c10x2 * yCurveB[3] * xCurveA[0] * c13y2) + (3d * c10y2 * xCurveB[3] * c13x2 * yCurveA[0]) + (c10y2 * c12x2 * yCurveA[1] * xCurveA[0]) - (c11x2 * yCurveB[3] * c12y2 * xCurveA[0]) + (2d * c20x2 * yCurveA[2] * xCurveA[1] * c13y2) + (3d * xCurveB[3] * c20y2 * c13x2 * yCurveA[0]) - (c20x2 * xCurveA[1] * c12y2 * yCurveA[0]) - (3d * c20x2 * yCurveB[3] * xCurveA[0] * c13y2) + (c12x2 * c20y2 * yCurveA[1] * xCurveA[0])
            ).Trim().RootsInInterval();

            foreach (var s in roots)
            {
                var point = new Point2D(
                    (xCurveB[0] * s * s * s) + (xCurveB[1] * s * s) + (xCurveB[2] * s) + xCurveB[3],
                    (yCurveB[0] * s * s * s) + (yCurveB[1] * s * s) + (yCurveB[2] * s) + yCurveB[3]);

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
                                if ((t >= 0d ? t : -t) < tolerance)
                                {
                                    result.Points.Add(point);
                                    goto checkRoots; // Break through two levels of for each loops. Using goto for performance.
                                }
                            }
                        }
                    }
                checkRoots:;
                }
            }

            if (result.Points.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Finds the intersection between a cubic Bézier and a polyline.
        /// </summary>
        /// <param name="b1X">The b1X.</param>
        /// <param name="b1Y">The b1Y.</param>
        /// <param name="b2X">The b2X.</param>
        /// <param name="b2Y">The b2Y.</param>
        /// <param name="b3X">The b3X.</param>
        /// <param name="b3Y">The b3Y.</param>
        /// <param name="b4X">The b4X.</param>
        /// <param name="b4Y">The b4Y.</param>
        /// <param name="points">The points.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierSegmentPolylineIntersection(
            double b1X, double b1Y, double b2X, double b2Y, double b3X, double b3Y, double b4X, double b4Y,
            List<Point2D> points,
            double epsilon = Epsilon)
            => CubicBezierSegmentPolylineIntersection(
                CubicBezierBernsteinBasis(b1X, b2X, b3X, b4X),
                CubicBezierBernsteinBasis(b1Y, b2Y, b3Y, b4Y),
                points,
                epsilon);

        /// <summary>
        /// Finds the intersection between a cubic Bézier and a polyline.
        /// </summary>
        /// <param name="xCurve">The xCurve.</param>
        /// <param name="yCurve">The yCurve.</param>
        /// <param name="points">The points.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierSegmentPolylineIntersection(
            Polynomial xCurve, Polynomial yCurve,
            List<Point2D> points,
            double epsilon = Epsilon)
        {
            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            var length = points.Count;

            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            var a1 = points[0];
            for (var i = 1; i < length; ++i)
            {
                var a2 = points[i];

                intersections.UnionWith(LineSegmentCubicBezierSegmentIntersection(a1.X, a1.Y, a2.X, a2.Y, xCurve, yCurve, epsilon).Points);

                a1 = a2;
            }

            // ToDo: Return IntersectionState.Inside if both end points are inside the Polygon and there are no intersections.

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Finds the intersection between a cubic Bézier and a polygon.
        /// </summary>
        /// <param name="b1X">The b1X.</param>
        /// <param name="b1Y">The b1Y.</param>
        /// <param name="b2X">The b2X.</param>
        /// <param name="b2Y">The b2Y.</param>
        /// <param name="b3X">The b3X.</param>
        /// <param name="b3Y">The b3Y.</param>
        /// <param name="b4X">The b4X.</param>
        /// <param name="b4Y">The b4Y.</param>
        /// <param name="points">The points.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
                CubicBezierBernsteinBasis(b1X, b2X, b3X, b4X),
                CubicBezierBernsteinBasis(b1Y, b2Y, b3Y, b4Y),
                points,
                epsilon);

        /// <summary>
        /// Finds the intersection between a cubic Bézier and a polygon.
        /// </summary>
        /// <param name="xCurve">The xCurve.</param>
        /// <param name="yCurve">The yCurve.</param>
        /// <param name="points">The points.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            var a1 = points[length - 1];
            for (var i = 0; i < length; ++i)
            {
                var a2 = points[i];

                intersections.UnionWith(LineSegmentCubicBezierSegmentIntersection(a1.X, a1.Y, a2.X, a2.Y, xCurve, yCurve, epsilon).Points);

                a1 = a2;
            }

            // ToDo: Return IntersectionState.Inside if both end points are inside the Polygon and there are no intersections.

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a cubic Bézier and a rectangle.
        /// </summary>
        /// <param name="b1X">The b1X.</param>
        /// <param name="b1Y">The b1Y.</param>
        /// <param name="b2X">The b2X.</param>
        /// <param name="b2Y">The b2Y.</param>
        /// <param name="b3X">The b3X.</param>
        /// <param name="b3Y">The b3Y.</param>
        /// <param name="b4X">The b4X.</param>
        /// <param name="b4Y">The b4Y.</param>
        /// <param name="t1X">The t1X.</param>
        /// <param name="t1Y">The t1Y.</param>
        /// <param name="t2X">The t2X.</param>
        /// <param name="t2Y">The t2Y.</param>
        /// <param name="t3X">The t3X.</param>
        /// <param name="t3Y">The t3Y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierSegmentTriangleIntersection(
            double b1X, double b1Y, double b2X, double b2Y, double b3X, double b3Y, double b4X, double b4Y,
            double t1X, double t1Y, double t2X, double t2Y, double t3X, double t3Y,
            double epsilon = Epsilon)
            => CubicBezierSegmentTriangleIntersection(
                CubicBezierBernsteinBasis(b1X, b2X, b3X, b4X),
                CubicBezierBernsteinBasis(b1Y, b2Y, b3Y, b4Y),
                t1X, t1Y, t2X, t2Y, t3X, t3Y,
                epsilon);

        /// <summary>
        /// Find the intersection between a cubic Bézier and a rectangle.
        /// </summary>
        /// <param name="xCurve">The xCurve.</param>
        /// <param name="yCurve">The yCurve.</param>
        /// <param name="t1X">The t1X.</param>
        /// <param name="t1Y">The t1Y.</param>
        /// <param name="t2X">The t2X.</param>
        /// <param name="t2Y">The t2Y.</param>
        /// <param name="t3X">The t3X.</param>
        /// <param name="t3Y">The t3Y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a cubic Bézier and a rectangle.
        /// </summary>
        /// <param name="b1X">The b1X.</param>
        /// <param name="b1Y">The b1Y.</param>
        /// <param name="b2X">The b2X.</param>
        /// <param name="b2Y">The b2Y.</param>
        /// <param name="b3X">The b3X.</param>
        /// <param name="b3Y">The b3Y.</param>
        /// <param name="b4X">The b4X.</param>
        /// <param name="b4Y">The b4Y.</param>
        /// <param name="r1X">The r1X.</param>
        /// <param name="r1Y">The r1Y.</param>
        /// <param name="r2X">The r2X.</param>
        /// <param name="r2Y">The r2Y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
                CubicBezierBernsteinBasis(b1X, b2X, b3X, b4X),
                CubicBezierBernsteinBasis(b1Y, b2Y, b3Y, b4Y),
                r1X, r1Y, r2X, r2Y,
                epsilon);

        /// <summary>
        /// Find the intersection between a cubic Bézier and a rectangle.
        /// </summary>
        /// <param name="xCurve">The xCurve.</param>
        /// <param name="yCurve">The yCurve.</param>
        /// <param name="r1X">The r1X.</param>
        /// <param name="r1Y">The r1Y.</param>
        /// <param name="r2X">The r2X.</param>
        /// <param name="r2Y">The r2Y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between two triangles.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <param name="x4">The x4.</param>
        /// <param name="y4">The y4.</param>
        /// <param name="x5">The x5.</param>
        /// <param name="y5">The y5.</param>
        /// <param name="x6">The x6.</param>
        /// <param name="y6">The y6.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a triangle and a rectangle.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <param name="left">The left.</param>
        /// <param name="top">The top.</param>
        /// <param name="right">The right.</param>
        /// <param name="bottom">The bottom.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a triangle and a polyline.
        /// </summary>
        /// <param name="a1X">The a1X.</param>
        /// <param name="a1Y">The a1Y.</param>
        /// <param name="a2X">The a2X.</param>
        /// <param name="a2Y">The a2Y.</param>
        /// <param name="a3X">The a3X.</param>
        /// <param name="a3Y">The a3Y.</param>
        /// <param name="points">The points.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection TrianglePolylineIntersection(
            double a1X, double a1Y, double a2X, double a2Y, double a3X, double a3Y,
            List<Point2D> points,
            double epsilon = Epsilon)
        {
            // ToDo: Need to determine if duplicates are acceptable, or if this attempt at performance boost is going to waste.
            var intersections = new HashSet<Point2D>();
            var length = points.Count;

            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            var b1 = points[0];
            for (var i = 1; i < length; ++i)
            {
                var b2 = points[i];

                intersections.UnionWith(LineSegmentLineSegmentIntersection(b1.X, b1.Y, b2.X, b2.Y, a1X, a1Y, a2X, a2Y, epsilon).Points);
                intersections.UnionWith(LineSegmentLineSegmentIntersection(b1.X, b1.Y, b2.X, b2.Y, a2X, a2Y, a3X, a3Y, epsilon).Points);
                intersections.UnionWith(LineSegmentLineSegmentIntersection(b1.X, b1.Y, b2.X, b2.Y, a1X, a1Y, a3X, a3Y, epsilon).Points);

                b1 = b2;
            }

            // ToDo: Return IntersectionState.Inside if both end points are inside the polygon, and there are no intersections.

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a triangle and a polygon contour.
        /// </summary>
        /// <param name="a1X">The a1X.</param>
        /// <param name="a1Y">The a1Y.</param>
        /// <param name="a2X">The a2X.</param>
        /// <param name="a2Y">The a2Y.</param>
        /// <param name="a3X">The a3X.</param>
        /// <param name="a3Y">The a3Y.</param>
        /// <param name="points">The points.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            var b1 = points[length - 1];
            for (var i = 0; i < length; ++i)
            {
                var b2 = points[i];

                intersections.UnionWith(LineSegmentLineSegmentIntersection(b1.X, b1.Y, b2.X, b2.Y, a1X, a1Y, a2X, a2Y, epsilon).Points);
                intersections.UnionWith(LineSegmentLineSegmentIntersection(b1.X, b1.Y, b2.X, b2.Y, a2X, a2Y, a3X, a3Y, epsilon).Points);
                intersections.UnionWith(LineSegmentLineSegmentIntersection(b1.X, b1.Y, b2.X, b2.Y, a1X, a1Y, a3X, a3Y, epsilon).Points);

                b1 = b2;
            }

            // ToDo: Return IntersectionState.Inside if both end points are inside the polygon, and there are no intersections.

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a triangle and a circle.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        /// <param name="r">The r.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between an ellipse and a triangle.
        /// </summary>
        /// <param name="r1X">The r1X.</param>
        /// <param name="r1Y">The r1Y.</param>
        /// <param name="r2X">The r2X.</param>
        /// <param name="r2Y">The r2Y.</param>
        /// <param name="r3X">The r3X.</param>
        /// <param name="r3Y">The r3Y.</param>
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="cosA">The cosA.</param>
        /// <param name="sinA">The sinA.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection TriangleObliqueEllipseIntersection(
            double r1X, double r1Y, double r2X, double r2Y, double r3X, double r3Y,
            double cX, double cY, double rx, double ry, double cosA, double sinA,
            double epsilon = Epsilon)
        {
            var result = new Intersection(IntersectionStates.NoIntersection);

            result.AppendPoints(LineSegmentObliqueEllipseIntersection(cX, cY, rx, ry, cosA, sinA, r1X, r1Y, r2X, r2Y, epsilon).Points);
            result.AppendPoints(LineSegmentObliqueEllipseIntersection(cX, cY, rx, ry, cosA, sinA, r2X, r2Y, r3X, r3Y, epsilon).Points);
            result.AppendPoints(LineSegmentObliqueEllipseIntersection(cX, cY, rx, ry, cosA, sinA, r3X, r3Y, r1X, r1Y, epsilon).Points);

            if (result.Points.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between two rectangles.
        /// </summary>
        /// <param name="a1X">The a1X.</param>
        /// <param name="a1Y">The a1Y.</param>
        /// <param name="a2X">The a2X.</param>
        /// <param name="a2Y">The a2Y.</param>
        /// <param name="b1X">The b1X.</param>
        /// <param name="b1Y">The b1Y.</param>
        /// <param name="b2X">The b2X.</param>
        /// <param name="b2Y">The b2Y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between two polylines.
        /// </summary>
        /// <param name="points1">The points1.</param>
        /// <param name="points2">The points2.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection PolylinePolylineIntersection(
            List<Point2D> points1,
            List<Point2D> points2,
            double epsilon = Epsilon)
        {
            var intersections = new HashSet<Point2D>();
            var length = points1.Count;

            // We shouldn't care about the ordering, we can start with the last segment for a slight performance boost.
            var a1 = points1[0];
            for (var i = 1; i < length; ++i)
            {
                var a2 = points1[i];

                intersections.UnionWith(LineSegmentPolylineIntersection(a1.X, a1.Y, a2.X, a2.Y, points2, epsilon).Points);

                a1 = a2;
            }

            // ToDo: Return IntersectionState.Inside if all end points of a polygon are inside the other polygon and there are no intersections.

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a polygon contour and a polyline.
        /// </summary>
        /// <param name="points1">The points1.</param>
        /// <param name="points2">The points2.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection PolylinePolygonContourIntersection(
            List<Point2D> points1,
            List<Point2D> points2,
            double epsilon = Epsilon)
        {
            var intersections = new HashSet<Point2D>();
            var length = points1.Count;

            // We shouldn't care about the ordering, we can start with the last segment for a slight performance boost.
            var a1 = points1[0];
            for (var i = 1; i < length; ++i)
            {
                var a2 = points1[i];

                intersections.UnionWith(LineSegmentPolygonContourIntersection(a1.X, a1.Y, a2.X, a2.Y, points2, epsilon).Points);

                a1 = a2;
            }

            // ToDo: Return IntersectionState.Inside if all end points of a polygon are inside the other polygon and there are no intersections.

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a polyline and a rectangle.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="r1X">The r1X.</param>
        /// <param name="r1Y">The r1Y.</param>
        /// <param name="r2X">The r2X.</param>
        /// <param name="r2Y">The r2Y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection PolylineRectangleIntersection(
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
            intersections.UnionWith(LineSegmentPolylineIntersection(minX, minY, topRight.X, topRight.Y, points, epsilon).Points);
            intersections.UnionWith(LineSegmentPolylineIntersection(topRight.X, topRight.Y, maxX, maxY, points, epsilon).Points);
            intersections.UnionWith(LineSegmentPolylineIntersection(maxX, maxY, bottomLeft.X, bottomLeft.Y, points, epsilon).Points);
            intersections.UnionWith(LineSegmentPolylineIntersection(bottomLeft.X, bottomLeft.Y, minX, minY, points, epsilon).Points);

            // ToDo: Return IntersectionState.Inside if all of the end points are contained inside the rectangle, or the points of the rectangle are inside the polygon, and there are no intersections.

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between two polygon contours.
        /// </summary>
        /// <param name="points1">The points1.</param>
        /// <param name="points2">The points2.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            var a1 = points1[length - 1];
            for (var i = 0; i < length; ++i)
            {
                var a2 = points1[i];

                intersections.UnionWith(LineSegmentPolygonContourIntersection(a1.X, a1.Y, a2.X, a2.Y, points2, epsilon).Points);

                a1 = a2;
            }

            // ToDo: Return IntersectionState.Inside if all end points of a polygon are inside the other polygon and there are no intersections.

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a polygon and a rectangle.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="r1X">The r1X.</param>
        /// <param name="r1Y">The r1Y.</param>
        /// <param name="r2X">The r2X.</param>
        /// <param name="r2Y">The r2Y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a circle and a rectangle.
        /// </summary>
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        /// <param name="r">The r.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="r1X">The r1X.</param>
        /// <param name="r1Y">The r1Y.</param>
        /// <param name="r2X">The r2X.</param>
        /// <param name="r2Y">The r2Y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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

            var result = new Intersection(IntersectionStates.NoIntersection, intersections);
            if (result.Points.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
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
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        /// <param name="r">The r.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="points">The points.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CirclePolylineIntersection(
            double cX, double cY, double r, double angle,
            List<Point2D> points,
            double epsilon = Epsilon)
        {
            var result = new Intersection(IntersectionStates.NoIntersection);
            var inter = new Intersection(IntersectionStates.NoIntersection);
            var length = points.Count;

            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            var a1 = points[0];
            for (var i = 1; i < length; ++i)
            {
                var a2 = points[i];

                inter = LineSegmentCircleIntersection(a1.X, a1.Y, a2.X, a2.Y, cX, cY, r, angle, epsilon);
                result.AppendPoints(inter.Points);

                a1 = a2;
            }

            // ToDo: Return IntersectionState.Inside if all of the points of the polygon are inside the circle.

            if (result.Points.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
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
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="points">The points.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
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
            var result = new Intersection(IntersectionStates.NoIntersection);
            var inter = new Intersection(IntersectionStates.NoIntersection);
            var length = points.Count;

            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            var b1 = points[length - 1];
            for (var i = 0; i < points.Count; ++i)
            {
                var b2 = points[i];

                inter = LineSegmentCircleIntersection(b1.X, b1.Y, b2.X, b2.Y, cX, cY, radius, angle, epsilon);
                result.AppendPoints(inter.Points);

                b1 = b2;
            }

            // ToDo: Return IntersectionState.Inside if all of the points of th polygon are contained inside the circle, or the circle and there are no intersections.

            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }
            else
            {
                result.State = inter.State;
            }

            return result;
        }

        /// <summary>
        /// Finds the Intersection between two circles.
        /// </summary>
        /// <param name="h1">The c x1.</param>
        /// <param name="k1">The c y1.</param>
        /// <param name="r1">The r1.</param>
        /// <param name="h2">The c x2.</param>
        /// <param name="k2">The c y2.</param>
        /// <param name="r2">The r2.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CircleCircleIntersection(double h1, double k1, double r1, double h2, double k2, double r2, double epsilon = double.Epsilon)
        {
            var distance = Sqrt(((h2 - h1) * (h2 - h1)) + ((k2 - k1) * (k2 - k1)));
            switch (distance)
            {
                case double o when o > r1 + r2:
                    // No intersection. 
                    return new Intersection(IntersectionStates.Outside, Array.Empty<Point2D>());
                case double i when i < Abs(r1 - r2):
                    // One circle is inside the other. No intersection.
                    return new Intersection(IntersectionStates.Inside, Array.Empty<Point2D>());
                default:
                    // Either 1 or 2 intersections.
                    var a = ((r1 * r1) - (r2 * r2) + (distance * distance)) / (2d * distance);
                    var x = h1 + ((h2 - h1) * (a / distance));
                    var y = k1 + ((k2 - k1) * (a / distance));
                    var b = Sqrt((r1 * r1) - (a * a)) / distance;
                    // See if we have 1 or 2 solutions.
                    return Abs(distance - r1 + r2) < epsilon
                        ? new Intersection(IntersectionStates.Intersection, new Point2D[] { (x + (b * (k2 - k1)), y - (b * (h2 - h1))), })
                        : new Intersection(IntersectionStates.Intersection, new Point2D[] { (x + (b * (k2 - k1)), y - (b * (h2 - h1))),
                                                                                            (x - (b * (k2 - k1)), y + (b * (h2 - h1))), });
            }
        }

        /// <summary>
        /// Find the intersection between an ellipse and a polyline.
        /// </summary>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="points">The points.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection ObliqueEllipsePolylineIntersection(double cx, double cy, double rx, double ry, double angle, List<Point2D> points, double epsilon = Epsilon) => ObliqueEllipsePolylineIntersection(cx, cy, rx, ry, Cos(angle), Sin(angle), points, epsilon);

        /// <summary>
        /// Find the intersection between an ellipse and a polyline.
        /// </summary>
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="cosA">The cosA.</param>
        /// <param name="sinA">The sinA.</param>
        /// <param name="points">The points.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection ObliqueEllipsePolylineIntersection(
            double cX, double cY, double rx, double ry, double cosA, double sinA,
            List<Point2D> points,
            double epsilon = Epsilon)
        {
            var result = new Intersection(IntersectionStates.NoIntersection);
            var inter = new Intersection(IntersectionStates.NoIntersection);
            var length = points.Count;

            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            var b1 = points[0];
            for (var i = 1; i < length; ++i)
            {
                var b2 = points[i];

                inter = LineSegmentObliqueEllipseIntersection(b1.X, b1.Y, b2.X, b2.Y, cX, cY, rx, ry, cosA, sinA, epsilon);
                result.AppendPoints(inter.Points);

                b1 = b2;
            }

            // ToDo: Return IntersectionState.Inside if all of the points of the polygon are contained inside the ellipse, and there are no intersections.

            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
            }
            else
            {
                result.State = inter.State;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between an ellipse and a polygon contour.
        /// </summary>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="points">The points.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection ObliqueEllipsePolygonContourIntersection(double cx, double cy, double rx, double ry, double angle, List<Point2D> points, double epsilon = Epsilon) => ObliqueEllipsePolygonContourIntersection(cx, cy, rx, ry, Cos(angle), Sin(angle), points, epsilon);

        /// <summary>
        /// Find the intersection between an ellipse and a polygon contour.
        /// </summary>
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="cosA">The cosA.</param>
        /// <param name="sinA">The sinA.</param>
        /// <param name="points">The points.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection ObliqueEllipsePolygonContourIntersection(
            double cX, double cY, double rx, double ry, double cosA, double sinA,
            List<Point2D> points,
            double epsilon = Epsilon)
        {
            var result = new Intersection(IntersectionStates.NoIntersection);
            var inter = new Intersection(IntersectionStates.NoIntersection);
            var length = points.Count;

            // We shouldn't care about the ordering, we can start with the last segment for a performance boost.
            var b1 = points[length - 1];
            for (var i = 0; i < points.Count; ++i)
            {
                var b2 = points[i];

                inter = LineSegmentObliqueEllipseIntersection(b1.X, b1.Y, b2.X, b2.Y, cX, cY, rx, ry, cosA, sinA, epsilon);
                result.AppendPoints(inter.Points);

                b1 = b2;
            }

            // ToDo: Return IntersectionState.Inside if all of the points of the polygon are contained inside the ellipse, and there are no intersections.

            if (result.Count > 0)
            {
                result.State |= IntersectionStates.Intersection;
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
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="r1X">The r1X.</param>
        /// <param name="r1Y">The r1Y.</param>
        /// <param name="r2X">The r2X.</param>
        /// <param name="r2Y">The r2Y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection ObliqueEllipseRectangleIntersection(double cx, double cy, double rx, double ry, double angle, double r1X, double r1Y, double r2X, double r2Y, double epsilon = Epsilon) => ObliqueEllipseRectangleIntersection(cx, cy, rx, ry, Cos(angle), Sin(angle), r1X, r1Y, r2X, r2Y, epsilon);

        /// <summary>
        /// Find the intersection between an ellipse and a rectangle.
        /// </summary>
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="cosA">The cosA.</param>
        /// <param name="sinA">The sinA.</param>
        /// <param name="r1X">The r1X.</param>
        /// <param name="r1Y">The r1Y.</param>
        /// <param name="r2X">The r2X.</param>
        /// <param name="r2Y">The r2Y.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection ObliqueEllipseRectangleIntersection(
            double cX, double cY, double rx, double ry, double cosA, double sinA,
            double r1X, double r1Y, double r2X, double r2Y,
            double epsilon = Epsilon)
        {
            var (minX, minY) = MinPoint(r1X, r1Y, r2X, r2Y);
            var (maxX, maxY) = MaxPoint(r1X, r1Y, r2X, r2Y);
            var topRight = new Point2D(maxX, minY);
            var bottomLeft = new Point2D(minX, maxY);

            var result = new Intersection(IntersectionStates.NoIntersection);
            result.AppendPoints(ObliqueEllipseRectangleIntersection(cX, cY, rx, ry, cosA, sinA, minX, minY, topRight.X, topRight.Y, epsilon).Points);
            result.AppendPoints(ObliqueEllipseRectangleIntersection(cX, cY, rx, ry, cosA, sinA, topRight.X, topRight.Y, maxX, maxY, epsilon).Points);
            result.AppendPoints(ObliqueEllipseRectangleIntersection(cX, cY, rx, ry, cosA, sinA, maxX, maxY, bottomLeft.X, bottomLeft.Y, epsilon).Points);
            result.AppendPoints(ObliqueEllipseRectangleIntersection(cX, cY, rx, ry, cosA, sinA, bottomLeft.X, bottomLeft.Y, minX, minY, epsilon).Points);

            if (result.Points.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between a quadratic Bézier and an orthogonal ellipse.
        /// </summary>
        /// <param name="b1X">The b1X.</param>
        /// <param name="b1Y">The b1Y.</param>
        /// <param name="b2X">The b2X.</param>
        /// <param name="b2Y">The b2Y.</param>
        /// <param name="b3X">The b3X.</param>
        /// <param name="b3Y">The b3Y.</param>
        /// <param name="ecX">The ecX.</param>
        /// <param name="ecY">The ecY.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentOrthogonalEllipseIntersection(double b1X, double b1Y, double b2X, double b2Y, double b3X, double b3Y, double ecX, double ecY, double rx, double ry, double epsilon = Epsilon) => QuadraticBezierSegmentOrthogonalEllipseIntersection(QuadraticBezierBernsteinBasis(b1X, b2X, b3X), QuadraticBezierBernsteinBasis(b1Y, b2Y, b3Y), ecX, ecY, rx, ry, epsilon);

        /// <summary>
        /// Find the intersection between a quadratic Bézier and an orthogonal ellipse.
        /// </summary>
        /// <param name="xCurve">The set of Polynomial Bézier Coefficients of the x coordinates of the Bézier curve.</param>
        /// <param name="yCurve">The set of Polynomial Bézier Coefficients of the y coordinates of the Bézier curve.</param>
        /// <param name="h">The ecX.</param>
        /// <param name="k">The ecY.</param>
        /// <param name="a">The rx.</param>
        /// <param name="b">The ry.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentOrthogonalEllipseIntersection(
            Polynomial xCurve, Polynomial yCurve,
            double h, double k, double a, double b,
            double epsilon = Epsilon)
        {
            _ = epsilon;

            (var ax, var bx, var cx) = (xCurve[0], xCurve[1], xCurve[2]);
            (var ay, var by, var cy) = (yCurve[0], yCurve[1], yCurve[2]);

            var aa = a * a;
            var bb = b * b;

            // Find the polynomial that represents the intersections.
            var roots = new Polynomial((
                a: (bb * ax * ax) + (aa * ay * ay),
                b: 2d * ((bb * ax * bx) + (aa * ay * by)),
                c: (bb * ((2d * ax * cx) + (bx * bx))) + (aa * ((2d * ay * cy) + (by * by))) - (2d * ((bb * h * ax) + (aa * k * ay))),
                d: 2d * ((bb * bx * (cx - h)) + (aa * by * (cy - k))),
                e: (bb * ((cx * cx) + (h * h))) + (aa * ((cy * cy) + (k * k))) - (2d * ((bb * h * cx) + (aa * k * cy))) - (aa * bb)
            )).Trim().RootsInInterval();

            // Initialize intersection.
            var result = new Intersection(IntersectionStates.NoIntersection);
            foreach (var s in roots)
            {
                if (0d <= s && s <= 1d)
                {
                    result.Points.Add((
                        X: (ax * s * s) + (bx * s) + cx,
                        Y: (ay * s * s) + (by * s) + cy
                        ));
                }
            }

            if (result.Points.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Quadratics the bezier segment orthogonal ellipse intersection.
        /// </summary>
        /// <param name="xCurve">The x curve.</param>
        /// <param name="yCurve">The y curve.</param>
        /// <param name="h">The h.</param>
        /// <param name="k">The k.</param>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentOrthogonalEllipseIntersection(Polynomial xCurve, Polynomial yCurve, double h, double k, double a, double b, double angle, double epsilon = Epsilon) => QuadraticBezierSegmentOrthogonalEllipseIntersection(xCurve, yCurve, h, k, a, b, Cos(angle), Sin(angle), epsilon = Epsilon);

        /// <summary>
        /// Find the intersection between a quadratic Bézier and an orthogonal ellipse.
        /// </summary>
        /// <param name="xCurve">The set of Polynomial Bézier Coefficients of the x coordinates of the Bézier curve.</param>
        /// <param name="yCurve">The set of Polynomial Bézier Coefficients of the y coordinates of the Bézier curve.</param>
        /// <param name="h">The ecX.</param>
        /// <param name="k">The ecY.</param>
        /// <param name="a">The rx.</param>
        /// <param name="b">The ry.</param>
        /// <param name="cos">The cos.</param>
        /// <param name="sin">The sin.</param>
        /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
        /// <returns>
        /// Returns an <see cref="Engine.Intersection" /> struct with a <see cref="Intersection.State" />, and an array of <see cref="Point2D" /> structs containing any points of intersection found.
        /// </returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentOrthogonalEllipseIntersection(
            Polynomial xCurve, Polynomial yCurve,
            double h, double k, double a, double b, double cos, double sin,
            double epsilon = Epsilon)
        {
            _ = epsilon;

            (var ax, var bx, var cx) = (xCurve[0], xCurve[1], xCurve[2]);
            (var ay, var by, var cy) = (yCurve[0], yCurve[1], yCurve[2]);

            var cxh = (cos * (cx - h)) + (sin * (cy - k));
            var cyk = (cos * (cy - k)) - (sin * (cx - h));
            var aa = a * a;
            var bb = b * b;

            // Find the polynomial that represents the intersections.
            var roots = new Polynomial((
                a: (bb * (ax * ax)) + (aa * (ay * ay)),
                b: 2d * ((bb * (ax * bx)) + (aa * (ay * by))),
                c: (bb * ((2d * ax * cx) + (bx * bx))) - (2d * ((bb * h * ax) + (aa * k * ay))) + (aa * ((2d * ay * cy) + (by * by))),
                d: 2d * ((bb * bx * cxh) + (aa * by * cyk)),
                e: (bb * ((cx * cx) + (k * k))) - (2d * ((bb * h * cx) + (aa * k * cy))) + (aa * ((cy * cy) + (k * k))) - (aa * bb)
                )).Trim().Roots();

            // Initialize intersection.
            var result = new Intersection(IntersectionStates.NoIntersection);
            foreach (var s in roots)
            {
                var point = (
                    X: (ax * s * s) + (bx * s) + cx,
                    Y: (ay * s * s) + (by * s) + cy);

                if (0d <= s && s <= 1d)
                {
                    result.Points.Add(point);
                }
            }

            if (result.Points.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Quadratics the bezier segment oblique ellipse intersection.
        /// </summary>
        /// <param name="b1X">The b1 x.</param>
        /// <param name="b1Y">The b1 y.</param>
        /// <param name="b2X">The b2 x.</param>
        /// <param name="b2Y">The b2 y.</param>
        /// <param name="b3X">The b3 x.</param>
        /// <param name="b3Y">The b3 y.</param>
        /// <param name="h">The h.</param>
        /// <param name="k">The k.</param>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="cos">The cos.</param>
        /// <param name="sin">The sin.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection QuadraticBezierSegmentObliqueEllipseIntersection(
            double b1X, double b1Y, double b2X, double b2Y, double b3X, double b3Y,
            double h, double k, double a, double b, double cos, double sin, double epsilon = Epsilon)
        {
            // ToDo: This does work. But rotating the points seems inefficient.

            // Rotate the points of the Bezier in the reverse angle about the center of the ellipse to align with it.
            (b1X, b1Y) = RotatePoint2D(b1X, b1Y, cos, -sin, h, k);
            (b2X, b2Y) = RotatePoint2D(b2X, b2Y, cos, -sin, h, k);
            (b3X, b3Y) = RotatePoint2D(b3X, b3Y, cos, -sin, h, k);

            // Find the points of intersection.
            var intersection = QuadraticBezierSegmentOrthogonalEllipseIntersection(b1X, b1Y, b2X, b2Y, b3X, b3Y, h, k, a, b, epsilon);

            // Rotate the points back forwards to the locations of the intersection.
            for (var i = 0; i < intersection.Points.Count; i++)
            {
                // Rotate point.
                intersection.Points[i] = RotatePoint2D(intersection.Points[i].X, intersection.Points[i].Y, cos, sin, h, k);
            }

            // Return result.
            return intersection;
        }

        /// <summary>
        /// Find the intersection between a cubic Bézier and an orthogonal ellipse.
        /// </summary>
        /// <param name="b1X">The b1X.</param>
        /// <param name="b1Y">The b1Y.</param>
        /// <param name="b2X">The b2X.</param>
        /// <param name="b2Y">The b2Y.</param>
        /// <param name="b3X">The b3X.</param>
        /// <param name="b3Y">The b3Y.</param>
        /// <param name="b4X">The b4X.</param>
        /// <param name="b4Y">The b4Y.</param>
        /// <param name="h">The ecX.</param>
        /// <param name="k">The ecY.</param>
        /// <param name="a">The rx.</param>
        /// <param name="b">The ry.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierSegmentOrthogonalEllipseIntersection(double b1X, double b1Y, double b2X, double b2Y, double b3X, double b3Y, double b4X, double b4Y, double h, double k, double a, double b, double epsilon = Epsilon) => CubicBezierSegmentOrthogonalEllipseIntersection(CubicBezierBernsteinBasis(b1X, b2X, b3X, b4X), CubicBezierBernsteinBasis(b1Y, b2Y, b3Y, b4Y), h, k, a, b, epsilon);

        /// <summary>
        /// Find the intersection between a cubic Bézier and an orthogonal ellipse.
        /// </summary>
        /// <param name="xCurve">The set of Polynomial Bézier Coefficients of the x coordinates of the Bézier curve.</param>
        /// <param name="yCurve">The set of Polynomial Bézier Coefficients of the y coordinates of the Bézier curve.</param>
        /// <param name="h">The ecX.</param>
        /// <param name="k">The ecY.</param>
        /// <param name="a">The rx.</param>
        /// <param name="b">The ry.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierSegmentOrthogonalEllipseIntersection(
            Polynomial xCurve, Polynomial yCurve,
            double h, double k, double a, double b,
            double epsilon = Epsilon)
        {
            _ = epsilon;

            // Initialize intersection.
            var result = new Intersection(IntersectionStates.NoIntersection);

            (var ax, var bx, var cx, var dx) = (xCurve[0], xCurve[1], xCurve[2], xCurve[3]);
            (var ay, var by, var cy, var dy) = (yCurve[0], yCurve[1], yCurve[2], yCurve[3]);

            // Find the polynomial that represents the intersections.
            var roots = new Polynomial((
                a: ((ax * ax) * (b * b)) + ((ay * ay) * (a * a)),
                b: 2d * ((ax * bx * (b * b)) + (ay * by * (a * a))),
                c: (2d * ((ax * cx * (b * b)) + (ay * cy * (a * a)))) + ((bx * bx) * (b * b)) + ((by * by) * (a * a)),
                d: (2d * ax * (b * b) * (dx - h)) + (2d * ay * (a * a) * (dy - k)) + (2d * ((bx * cx * (b * b)) + (by * cy * (a * a)))),
                e: (2d * bx * (b * b) * (dx - h)) + (2d * by * (a * a) * (dy - k)) + ((cx * cx) * (b * b)) + ((cy * cy) * (a * a)),
                f: (2d * cx * (b * b) * (dx - h)) + (2d * cy * (a * a) * (dy - k)),
                g: (dx * dx * (b * b)) - (2d * dy * k * (a * a)) - (2d * dx * h * (b * b)) + ((dy * dy) * (a * a)) + ((h * h) * (b * b)) + ((k * k) * (a * a)) - ((a * a) * (b * b))
                )).Trim().RootsInInterval();

            foreach (var s in roots)
            {
                var point = new Point2D(
                    (ax * s * s * s) + (bx * s * s) + (cx * s) + dx,
                    (ay * s * s * s) + (by * s * s) + (cy * s) + dy);

                result.Points.Add(point);
            }

            if (result.Points.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b1X"></param>
        /// <param name="b1Y"></param>
        /// <param name="b2X"></param>
        /// <param name="b2Y"></param>
        /// <param name="b3X"></param>
        /// <param name="b3Y"></param>
        /// <param name="b4X"></param>
        /// <param name="b4Y"></param>
        /// <param name="h"></param>
        /// <param name="k"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="cos"></param>
        /// <param name="sin"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection CubicBezierSegmentObliqueEllipseIntersection(
            double b1X, double b1Y, double b2X, double b2Y, double b3X, double b3Y, double b4X, double b4Y,
            double h, double k, double a, double b, double cos, double sin, double epsilon = Epsilon)
        {
            // Rotate the points of the Bezier in the reverse angle about the center of the ellipse to align with it.
            (b1X, b1Y) = RotatePoint2D(b1X, b1Y, cos, -sin, h, k);
            (b2X, b2Y) = RotatePoint2D(b2X, b2Y, cos, -sin, h, k);
            (b3X, b3Y) = RotatePoint2D(b3X, b3Y, cos, -sin, h, k);
            (b4X, b4Y) = RotatePoint2D(b4X, b4Y, cos, -sin, h, k);

            // Find the points of intersection.
            var intersection = CubicBezierSegmentOrthogonalEllipseIntersection(b1X, b1Y, b2X, b2Y, b3X, b3Y, b4X, b4Y, h, k, a, b, epsilon);

            // Rotate the points back forwards to the locations of the intersection.
            for (var i = 0; i < intersection.Points.Count; i++)
            {
                // Rotate point.
                intersection.Points[i] = RotatePoint2D(intersection.Points[i].X, intersection.Points[i].Y, cos, sin, h, k);
            }

            // Return result.
            return intersection;
        }

        /// <summary>
        /// Find the intersection between a cubic Bézier and an rotated ellipse.
        /// </summary>
        /// <param name="xCurve">The set of Polynomial Bézier Coefficients of the x coordinates of the Bézier curve.</param>
        /// <param name="yCurve">The set of Polynomial Bézier Coefficients of the y coordinates of the Bézier curve.</param>
        /// <param name="h">The ecX.</param>
        /// <param name="k">The ecY.</param>
        /// <param name="a">The rx.</param>
        /// <param name="b">The ry.</param>
        /// <param name="cos">The cos a.</param>
        /// <param name="sin">The sin a.</param>
        /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
        /// <returns>
        /// Returns an <see cref="Engine.Intersection" /> struct with a <see cref="Intersection.State" />, and an array of <see cref="Point2D" /> structs containing any points of intersection found.
        /// </returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Intersection CubicBezierSegmentObliqueEllipseIntersectionPlayground(
            Polynomial xCurve, Polynomial yCurve,
            double h, double k, double a, double b, double cos, double sin,
            double epsilon = Epsilon)
        {
            if (sin == 1d || sin == -1d) cos = 0d;

            _ = epsilon;

            // The Cubic Bezier 
            (var ax, var bx, var cx, var dx) = (xCurve[0], xCurve[1], xCurve[2], xCurve[3]);
            (var ay, var by, var cy, var dy) = (yCurve[0], yCurve[1], yCurve[2], yCurve[3]);

            // ToDo: Need to figure out why the rotation transform is not working as expected.
            // The equation of a no-rotated transformed ellipse is: $\frac{(x-h)^2}{a^2}+\frac{(y-k)^2}{b^2}=1$
            // and the equation of a rotated ellipse is: $\frac{((x-h)\cos{t}+(y-k)\sin{t})^2}{a^2}+\frac{((x-h)\sin{t}-(y-k)\cos{t})^2}{b^2}=1$
            // So, I thought that switching out (x-h) and (y-k) for the rotated versions should work. But it only almost works.
            // The results seem to indicate that the rotation is possibly occurring about the d point of the curve?
            // What am I missing?

            // Apply the rotation transform to the ellipse.
            var dxh = (/*cos * cos **/ (dx - h) /*- sin * sin * (dy - k)*/);
            var dyk = (/*cos * cos **/ (dy - k)  /*- sin *sin * (dx - h)*/);
            var aa = a * a;
            var bb = b * b;

            // Find the polynomial that represents the intersections.
            var roots = new Polynomial((
                a: ((ax * ax) * bb) + ((ay * ay) * aa),
                b: (2d * (ax * bx * bb)) + (2d * (ay * by * aa)),
                c: (((2d * (ax * cx)) + (bx * bx)) * bb) + (((2d * (ay * cy)) + (by * by)) * aa),
                d: (2d * ((ax * dxh) + (bx * cx)) * bb) + (2d * ((ay * dyk) + (by * cy)) * aa),
                e: (((2d * (bx * dxh)) + (cx * cx)) * bb) + (((2d * (by * dyk)) + (cy * cy)) * aa),
                f: (2d * cx * bb * dxh) + (2d * cy * aa * dyk),
                g: (bb * (dxh * dxh)) + (aa * (dyk * dyk)) - (aa * bb)
                )).Trim().RootsInInterval();

            //var roots = new Polynomial((
            //    a: 2 * (ax - 3 * bx + 3 * cx - dx) * b * b * cos * cos * h + 2 * a * a * (ay - 3 * by + 3 * cy - dy) * cos * cos * k - 2 * a * a * (ax - 3 * bx + 3 * cx - dx) * (ay - 3 * by + 3 * cy - dy) * cos * sin + 2 * (ax - 3 * bx + 3 * cx - dx) * (ay - 3 * by + 3 * cy - dy) * b * b * cos * sin - 2 * a * a * (ay - 3 * by + 3 * cy - dy) * cos * h * sin + 2 * (ay - 3 * by + 3 * cy - dy) * b * b * cos * h * sin - 2 * a * a * (ax - 3 * bx + 3 * cx - dx) * cos * k * sin + 2 * (ax - 3 * bx + 3 * cx - dx) * b * b * cos * k * sin + 2 * a * a * (ax - 3 * bx + 3 * cx - dx) * h * sin * sin + 2 * (ay - 3 * by + 3 * cy - dy) * b * b * k * sin * sin,
            //    b: -6 * (ax - 2 * bx + cx) * b * b * cos * cos * h - 6 * a * a * (ay - 2 * by + cy) * cos * cos * k - 18 * a * a * (ax - 2 * bx + cx) * (ay - 2 * by + cy) * cos * sin + 18 * (ax - 2 * bx + cx) * (ay - 2 * by + cy) * b * b * cos * sin + 6 * a * a * (ay - 2 * by + cy) * cos * h * sin - 6 * (ay - 2 * by + cy) * b * b * cos * h * sin + 6 * a * a * (ax - 2 * bx + cx) * cos * k * sin - 6 * (ax - 2 * bx + cx) * b * b * cos * k * sin - 6 * a * a * (ax - 2 * bx + cx) * h * sin * sin - 6 * (ay - 2 * by + cy) * b * b * k * sin * sin,
            //    c: 6 * (ax - bx) * b * b * cos * cos * h + 6 * a * a * (ay - by) * cos * cos * k - 18 * a * a * (ax - bx) * (ay - by) * cos * sin + 18 * (ax - bx) * (ay - by) * b * b * cos * sin - 6 * a * a * (ay - by) * cos * h * sin + 6 * (ay - by) * b * b * cos * h * sin - 6 * a * a * (ax - bx) * cos * k * sin + 6 * (ax - bx) * b * b * cos * k * sin + 6 * a * a * (ax - bx) * h * sin * sin + 6 * (ay - by) * b * b * k * sin * sin,
            //    d: a * a * ay * ay * cos * cos + ax * ax * b * b * cos * cos - 6 * ax * b * b * bx * cos * cos + 9 * b * b * bx * bx * cos * cos - 6 * a * a * ay * by * cos * cos + 9 * a * a * by * by * cos * cos + 6 * ax * b * b * cx * cos * cos - 18 * b * b * bx * cx * cos * cos + 9 * b * b * cx * cx * cos * cos + 6 * a * a * ay * cy * cos * cos - 18 * a * a * by * cy * cos * cos + 9 * a * a * cy * cy * cos * cos - 2 * ax * b * b * cos * cos * dx + 6 * b * b * bx * cos * cos * dx - 6 * b * b * cx * cos * cos * dx + b * b * cos * cos * dx * dx - 2 * a * a * ay * cos * cos * dy + 6 * a * a * by * cos * cos * dy - 6 * a * a * cy * cos * cos * dy + a * a * cos * cos * dy * dy + ay * ay * b * b * sin * sin - 6 * ay * b * b * by * sin * sin + 9 * b * b * by * by * sin * sin + 6 * ay * b * b * cy * sin * sin - 18 * b * b * by * cy * sin * sin + 9 * b * b * cy * cy * sin * sin - 2 * ay * b * b * dy * sin * sin + 6 * b * b * by * dy * sin * sin - 6 * b * b * cy * dy * sin * sin + b * b * dy * dy * sin * sin + a * a * ax * ax * sin - 6 * a * a * ax * bx * sin + 9 * a * a * bx * bx * sin + 6 * a * a * ax * cx * sin - 18 * a * a * bx * cx * sin + 9 * a * a * cx * cx * sin - 2 * a * a * ax * dx * sin + 6 * a * a * bx * dx * sin - 6 * a * a * cx * dx * sin + a * a * dx * dx * sin,
            //    e: -6 * a * a * ay * ay * cos * cos - 6 * ax * ax * b * b * cos * cos + 30 * ax * b * b * bx * cos * cos - 36 * b * b * bx * bx * cos * cos + 30 * a * a * ay * by * cos * cos - 36 * a * a * by * by * cos * cos - 24 * ax * b * b * cx * cos * cos + 54 * b * b * bx * cx * cos * cos - 18 * b * b * cx * cx * cos * cos - 24 * a * a * ay * cy * cos * cos + 54 * a * a * by * cy * cos * cos - 18 * a * a * cy * cy * cos * cos + 6 * ax * b * b * cos * cos * dx - 12 * b * b * bx * cos * cos * dx + 6 * b * b * cx * cos * cos * dx + 6 * a * a * ay * cos * cos * dy - 12 * a * a * by * cos * cos * dy + 6 * a * a * cy * cos * cos * dy - 6 * ay * ay * b * b * sin * sin + 30 * ay * b * b * by * sin * sin - 36 * b * b * by * by * sin * sin - 24 * ay * b * b * cy * sin * sin + 54 * b * b * by * cy * sin * sin - 18 * b * b * cy * cy * sin * sin + 6 * ay * b * b * dy * sin * sin - 12 * b * b * by * dy * sin * sin + 6 * b * b * cy * dy * sin * sin - 6 * a * a * ax * ax * sin + 30 * a * a * ax * bx * sin - 36 * a * a * bx * bx * sin - 24 * a * a * ax * cx * sin + 54 * a * a * bx * cx * sin - 18 * a * a * cx * cx * sin + 6 * a * a * ax * dx * sin - 12 * a * a * bx * dx * sin + 6 * a * a * cx * dx * sin,
            //    f: 15 * a * a * ay * ay * cos * cos + 15 * ax * ax * b * b * cos * cos - 60 * ax * b * b * bx * cos * cos + 54 * b * b * bx * bx * cos * cos - 60 * a * a * ay * by * cos * cos + 54 * a * a * by * by * cos * cos + 36 * ax * b * b * cx * cos * cos - 54 * b * b * bx * cx * cos * cos + 9 * b * b * cx * cx * cos * cos + 36 * a * a * ay * cy * cos * cos - 54 * a * a * by * cy * cos * cos + 9 * a * a * cy * cy * cos * cos - 6 * ax * b * b * cos * cos * dx + 6 * b * b * bx * cos * cos * dx - 6 * a * a * ay * cos * cos * dy + 6 * a * a * by * cos * cos * dy + 15 * ay * ay * b * b * sin * sin - 60 * ay * b * b * by * sin * sin + 54 * b * b * by * by * sin * sin + 36 * ay * b * b * cy * sin * sin - 54 * b * b * by * cy * sin * sin + 9 * b * b * cy * cy * sin * sin - 6 * ay * b * b * dy * sin * sin + 6 * b * b * by * dy * sin * sin + 15 * a * a * ax * ax * sin - 60 * a * a * ax * bx * sin + 54 * a * a * bx * bx * sin + 36 * a * a * ax * cx * sin - 54 * a * a * bx * cx * sin + 9 * a * a * cx * cx * sin - 6 * a * a * ax * dx * sin + 6 * a * a * bx * dx * sin,
            //    g: -20 * a * a * ay * ay * cos * cos - 20 * ax * ax * b * b * cos * cos + 60 * ax * b * b * bx * cos * cos - 36 * b * b * bx * bx * cos * cos + 60 * a * a * ay * by * cos * cos - 36 * a * a * by * by * cos * cos - 24 * ax * b * b * cx * cos * cos + 18 * b * b * bx * cx * cos * cos - 24 * a * a * ay * cy * cos * cos + 18 * a * a * by * cy * cos * cos + 2 * ax * b * b * cos * cos * dx + 2 * a * a * ay * cos * cos * dy - 20 * ay * ay * b * b * sin * sin + 60 * ay * b * b * by * sin * sin - 36 * b * b * by * by * sin * sin - 24 * ay * b * b * cy * sin * sin + 18 * b * b * by * cy * sin * sin + 2 * ay * b * b * dy * sin * sin - 20 * a * a * ax * ax * sin + 60 * a * a * ax * bx * sin - 36 * a * a * bx * bx * sin - 24 * a * a * ax * cx * sin + 18 * a * a * bx * cx * sin + 2 * a * a * ax * dx * sin,
            //    h: 15 * a * a * ay * ay * cos * cos + 15 * ax * ax * b * b * cos * cos - 30 * ax * b * b * bx * cos * cos + 9 * b * b * bx * bx * cos * cos - 30 * a * a * ay * by * cos * cos + 9 * a * a * by * by * cos * cos + 6 * ax * b * b * cx * cos * cos + 6 * a * a * ay * cy * cos * cos + 15 * ay * ay * b * b * sin * sin - 30 * ay * b * b * by * sin * sin + 9 * b * b * by * by * sin * sin + 6 * ay * b * b * cy * sin * sin + 15 * a * a * ax * ax * sin - 30 * a * a * ax * bx * sin + 9 * a * a * bx * bx * sin + 6 * a * a * ax * cx * sin,
            //    i: -6 * a * a * ay * ay * cos * cos - 6 * ax * ax * b * b * cos * cos + 6 * ax * b * b * bx * cos * cos + 6 * a * a * ay * by * cos * cos - 6 * ay * ay * b * b * sin * sin + 6 * ay * b * b * by * sin * sin - 6 * a * a * ax * ax * sin + 6 * a * a * ax * bx * sin,
            //    j: a * a * ay * ay * cos * cos + ax * ax * b * b * cos * cos - 2 * ax * b * b * cos * cos * h + b * b * cos * cos * h * h - 2 * a * a * ay * cos * cos * k + a * a * cos * cos * k * k - 2 * a * a * ax * ay * cos * sin + 2 * ax * ay * b * b * cos * sin + 2 * a * a * ay * cos * h * sin - 2 * ay * b * b * cos * h * sin + 2 * a * a * ax * cos * k * sin - 2 * ax * b * b * cos * k * sin - 2 * a * a * cos * h * k * sin + 2 * b * b * cos * h * k * sin + ay * ay * b * b * sin * sin - 2 * a * a * ax * h * sin * sin + a * a * h * h * sin * sin - 2 * ay * b * b * k * sin * sin + b * b * k * k * sin * sin + a * a * ax * ax * sin - a * a * b * b
            //    )).Trim().RootsInInterval();

            // Initialize intersection.
            var result = new Intersection(IntersectionStates.NoIntersection);
            foreach (var s in roots)
            {
                var point = (
                    X: (ax * s * s * s) + (bx * s * s) + (cx * s) + dx,
                    Y: (ay * s * s * s) + (by * s * s) + (cy * s) + dy);

                result.Points.Add(point);
            }

            if (result.Points.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Find the intersection between two orthogonal ellipses.
        /// </summary>
        /// <param name="h1">The c1X.</param>
        /// <param name="k1">The c1Y.</param>
        /// <param name="a1">The rx1.</param>
        /// <param name="b1">The ry1.</param>
        /// <param name="h2">The c2X.</param>
        /// <param name="k2">The c2Y.</param>
        /// <param name="a2">The rx2.</param>
        /// <param name="b2">The ry2.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns an <see cref="Engine.Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection OrthogonalEllipseOrthogonalEllipseIntersection(
            double h1, double k1, double a1, double b1,
            double h2, double k2, double a2, double b2,
            double epsilon = Epsilon)
        {
            // If both of the radi on each ellipse are the same, they must be circles. Use the faster Circle intersection method.
            if (a1 == b1 && a2 == b2)
            {
                return CircleCircleIntersection(h1, k1, a1, h2, k2, a2, epsilon);
            }

            // Polynomials representing the orthogonal Ellipses.
            var a = OrthogonalEllipseConicSectionPolynomial(h1, k1, a1, b1);
            var b = OrthogonalEllipseConicSectionPolynomial(h2, k2, a2, b2);

            var yRoots = new Polynomial(ConicSectionBezout(a, b)).Trim().Roots();

            // Double epsilon is too small for here.
            epsilon = 1e-6; //1e-3;
            var norm0 = ((a.a * a.a) + (2d * a.b * a.b) + (a.c * a.c)) * epsilon;
            var norm1 = ((b.a * b.a) + (2d * b.b * b.b) + (b.c * b.c)) * epsilon;

            var result = new Intersection(IntersectionStates.NoIntersection);
            for (var y = 0; y < yRoots.Length; y++)
            {
                var xRoots = new Polynomial(
                     a.a,
                     a.d + (yRoots[y] * a.b),
                     a.f + (yRoots[y] * (a.e + (yRoots[y] * a.c))),
                     epsilon).Trim().Roots();
                for (var x = 0; x < xRoots.Length; x++)
                {
                    var test = (((a.a * xRoots[x]) + (a.b * yRoots[y]) + a.d) * xRoots[x]) + (((a.c * yRoots[y]) + a.e) * yRoots[y]) + a.f;
                    if (Abs(test) < norm0)
                    {
                        test = (((b.a * xRoots[x]) + (b.b * yRoots[y]) + b.d) * xRoots[x]) + (((b.c * yRoots[y]) + b.e) * yRoots[y]) + b.f;
                        if (Abs(test) < norm1)
                        {
                            result.AppendPoint(new Point2D(xRoots[x], yRoots[y]));
                        }
                    }
                }
            }

            if (result.Points.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }

        /// <summary>
        /// Ellipse, Ellipse intersection.
        /// </summary>
        /// <param name="h1">The c1 x.</param>
        /// <param name="k1">The c1 y.</param>
        /// <param name="a1">The RX1.</param>
        /// <param name="b1">The ry1.</param>
        /// <param name="angle1">The angle1.</param>
        /// <param name="h2">The c2 x.</param>
        /// <param name="k2">The c2 y.</param>
        /// <param name="a2">The RX2.</param>
        /// <param name="b2">The ry2.</param>
        /// <param name="angle2">The angle2.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection ObliqueEllipseObliqueEllipseIntersection(double h1, double k1, double a1, double b1, double angle1, double h2, double k2, double a2, double b2, double angle2, double epsilon = Epsilon) => ObliqueEllipseObliqueEllipseIntersection(h1, k1, a1, b1, Cos(angle1), Sin(angle1), h2, k2, a2, b2, Cos(angle2), Sin(angle2), epsilon);

        /// <summary>
        /// Find the intersection between two ellipses.
        /// </summary>
        /// <param name="h1">The h1.</param>
        /// <param name="k1">The k1.</param>
        /// <param name="a1">The r x1.</param>
        /// <param name="b1">The r y1.</param>
        /// <param name="cos1">The cos1.</param>
        /// <param name="sin1">The sin1.</param>
        /// <param name="h2">The h2.</param>
        /// <param name="k2">The k2.</param>
        /// <param name="a2">The r x2.</param>
        /// <param name="b2">The r y2.</param>
        /// <param name="cos2">The cos2.</param>
        /// <param name="sin2">The sin2.</param>
        /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
        /// <returns>
        /// Returns an <see cref="Engine.Intersection" /> struct with a <see cref="Intersection.State" />, and an array of <see cref="Point2D" /> structs containing any points of intersection found.
        /// </returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// https://www.geometrictools.com/Documentation/IntersectionOfEllipses.pdf
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Intersection ObliqueEllipseObliqueEllipseIntersection(
            double h1, double k1, double a1, double b1, double cos1, double sin1,
            double h2, double k2, double a2, double b2, double cos2, double sin2,
            double epsilon = Epsilon)
        {
            // If the ellipses aren't rotated, use the slightly faster Orthogonal method.
            if (cos1 == Cos0 && cos2 == Cos0 && sin1 == Sin0 && sin2 == Sin0)
            {
                return OrthogonalEllipseOrthogonalEllipseIntersection(h1, k1, a1, b1, h2, k2, a2, b2, epsilon);
            }

            // If the angles are reflections of each other with slight loss of precision in sin cos, and they are the same height and size, the angle needs to be corrected.
            if (Abs(cos1 - cos2) < epsilon && Abs(sin1 + sin2) < epsilon && k1 == k2 && a1 == a2 && b1 == b2)
            {
                // Fix for Case 0.
                cos1 = cos2;
                sin1 = -sin2;
                // ToDo: Verify this fix with more tests.
                // What happens when the ellipses are swapped?
            }

            // If the ellipses are rotated at 90 degrees to each other, and their sizes are opposites, everything gets messed up. So, let's use Orthogonal intersection code.
            if (CrossProduct(cos1, sin1, cos2, sin2) == 1d && a1 == b2 && a2 == b1)
            {
                // Rotate the center point of the second ellipse in the reverse angle about the center of the first ellipse to align with it.
                (h2, k2) = RotatePoint2D(h2, k2, cos1, -sin1, h1, k1);

                // Rotate second ellipse Orthogonally.
                Swap(ref a2, ref b2);

                // Find the points of intersection.
                var intersection = OrthogonalEllipseOrthogonalEllipseIntersection(h1, k1, a1, b1, h2, k2, a2, b2, epsilon);

                // Rotate the points back forwards to the locations of the intersection.
                for (var i = 0; i < intersection.Points.Count; i++)
                {
                    // Rotate point.
                    intersection.Points[i] = RotatePoint2D(intersection.Points[i].X, intersection.Points[i].Y, cos1, sin1, h1, k1);
                }

                // Return result.
                return intersection;
            }

            // Polynomials representing the Ellipses.
            var e1 = EllipseConicSectionPolynomial(h1, k1, a1, b1, cos1, sin1);
            var e2 = EllipseConicSectionPolynomial(h2, k2, a2, b2, cos2, sin2);

            var yRoots = new Polynomial(ConicSectionBezout(e1, e2)).Trim().Roots();

            // Double epsilon is too small for here.
            epsilon = 1e-6; //1e-3;
            var norm0 = ((e1.a * e1.a) + (2d * e1.b * e1.b) + (e1.c * e1.c)) * epsilon;
            var norm1 = ((e2.a * e2.a) + (2d * e2.b * e2.b) + (e2.c * e2.c)) * epsilon;

            var result = new Intersection(IntersectionStates.NoIntersection);
            foreach (var s in yRoots)
            {
                var xRoots = new Polynomial((
                    a: e1.a,
                    b: e1.d + s * e1.b,
                    c: e1.f + s * (e1.e + s * e1.c)
                )).Trim().Roots();
                foreach (var t in xRoots)
                {
                    var test = (((e1.a * t) + (e1.b * s) + e1.d) * t) + (((e1.c * s) + e1.e) * s) + e1.f;
                    if (Abs(test) < norm0)
                    {
                        test = (((e2.a * t) + (e2.b * s) + e2.d) * t) + (((e2.c * s) + e2.e) * s) + e2.f;
                        if (Abs(test) < norm1)
                        {
                            result.AppendPoint(new Point2D(t, s));
                        }
                    }
                }
            }

            if (result.Points.Count > 0)
            {
                result.State = IntersectionStates.Intersection;
            }

            return result;
        }
        #endregion Intersection Methods
    }
}
