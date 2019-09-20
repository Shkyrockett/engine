// <copyright file="Intersections.Contains.cs" >
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
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static System.Math;
using static Engine.Mathematics;
using static Engine.Measurements;
using static Engine.Operations;

namespace Engine
{
    /// <summary>
    /// The intersections class.
    /// </summary>
    public static partial class Intersections
    {
        #region Contains Extension Method Overloads
        /// <summary>
        /// Determines whether the specified point is contained within the region defined by this <see cref="Point2D"/>.
        /// </summary>
        /// <param name="a">First Point to test.</param>
        /// <param name="b">Second Point to test.</param>
        /// <returns>Returns an <see cref="Inclusions"/> object with the points of intersection, and relationship status.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusions Contains(this Point2D a, Point2D b)
            => a == b ? Inclusions.Boundary : Inclusions.Outside;

        /// <summary>
        /// Determines whether the specified point is contained within the region defined by this <see cref="LineSegment"/>.
        /// </summary>
        /// <param name="seg">Line segment to test.</param>
        /// <param name="point">Point to test.</param>
        /// <returns>Returns an <see cref="Inclusions"/> object with the points of intersection, and relationship status.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusions Contains(this LineSegment seg, Point2D point)
            => PointLineSegmentIntersects(point.X, point.Y, seg.AX, seg.AY, seg.BX, seg.BY) ? Inclusions.Boundary : Inclusions.Outside;

        /// <summary>
        /// Determines whether the specified point is contained within the rectangular region defined by this <see cref="Rectangle2D"/>.
        /// </summary>
        /// <param name="rectangle"><see cref="Rectangle2D"/> class.</param>
        /// <param name="point">Point to test.</param>
        /// <returns>Returns an <see cref="Inclusions"/> object with the points of intersection, and relationship status.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusions Contains(this Rectangle2D rectangle, Point2D point)
            => RectangleContainsPoint(rectangle.X, rectangle.Y, rectangle.Right, rectangle.Bottom, point.X, point.Y);

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="PolygonContour"/>.
        /// </summary>
        /// <param name="polygon"><see cref="PolygonContour"/> class.</param>
        /// <param name="point">Point to test.</param>
        /// <returns>Returns an <see cref="Inclusions"/> object with the points of intersection, and relationship status.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusions Contains(this PolygonContour polygon, Point2D point)
            => PolygonContourContainsPoint(polygon.Points, point.X, point.Y);

        /// <summary>
        /// Determines whether the specified point is contained within the region defined by this <see cref="PolyBezierContour"/>.
        /// </summary>
        /// <param name="figure"><see cref="PolyBezierContour"/> class.</param>
        /// <param name="point">Point to test.</param>
        /// <returns>Returns an <see cref="Inclusions"/> object with the points of intersection, and relationship status.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusions Contains(this PolycurveContour figure, Point2D point)
            => PolycurveContourContainsPoint(figure, point);

        /// <summary>
        /// Determines whether the specified point is contained withing the set of regions defined by this <see cref="Polygon"/>.
        /// </summary>
        /// <param name="polygons">List of <see cref="PolygonContour"/> classes.</param>
        /// <param name="point">Point to test.</param>
        /// <returns>Returns an <see cref="Inclusions"/> object with the points of intersection, and relationship status.</returns>
        /// <remarks><para>This function automatically knows that enclosed polygons are "no-go" areas.</para></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusions Contains(this Polygon polygons, Point2D point)
            => PolygonContainsPoint(polygons.Contours, point.X, point.Y);

        /// <summary>
        /// Determines whether the specified point is contained within the region defined by this <see cref="Circle"/>.
        /// </summary>
        /// <param name="circle"><see cref="Circle"/> class.</param>
        /// <param name="point">Point to test.</param>
        /// <returns>Returns an <see cref="Inclusions"/> object with the points of intersection, and relationship status.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusions Contains(this Circle circle, Point2D point)
            => CircleContainsPoint(circle.X, circle.Y, circle.Radius, point.X, point.Y);

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="Ellipse"/>.
        /// </summary>
        /// <param name="ellipse"><see cref="Ellipse"/> class.</param>
        /// <param name="point">Point to test.</param>
        /// <returns>Returns an <see cref="Inclusions"/> object with the points of intersection, and relationship status.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusions Contains(this Ellipse ellipse, Point2D point)
            => EllipseContainsPoint(ellipse.Center.X, ellipse.Center.Y, ellipse.RX, ellipse.RY, ellipse.CosAngle, ellipse.SinAngle, point.X, point.Y);

        /// <summary>
        /// Determines whether the specified point is contained within the region defined by this <see cref="Circle"/>.
        /// </summary>
        /// <param name="arc"><see cref="Circle"/> class.</param>
        /// <param name="point">Point to test.</param>
        /// <returns>Returns an <see cref="Inclusions"/> object with the points of intersection, and relationship status.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusions Contains(this CircularArc arc, Point2D point)
            => CircularArcSectorContainsPoint(arc.X, arc.Y, arc.Radius, arc.StartAngle, arc.SweepAngle, point.X, point.Y);

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="EllipticalArc"/>.
        /// </summary>
        /// <param name="ellipseArc"><see cref="Ellipse"/> class.</param>
        /// <param name="point">Point to test.</param>
        /// <returns>Returns an <see cref="Inclusions"/> object with the points of intersection, and relationship status.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusions Contains(this EllipticalArc ellipseArc, Point2D point)
            => EllipticalArcContainsPoint(ellipseArc.Center.X, ellipseArc.Center.Y, ellipseArc.RX, ellipseArc.RY, ellipseArc.CosAngle, ellipseArc.SinAngle, ellipseArc.StartAngleCos, ellipseArc.StartAngleSin, ellipseArc.EndAngleCos, ellipseArc.EndAngleSin, ellipseArc.SweepAngle, point.X, point.Y, Epsilon);

        /// <summary>
        /// Determines whether the specified <see cref="Rectangle2D"/> is contained withing the region defined by this <see cref="Rectangle2D"/>.
        /// </summary>
        /// <param name="a"><see cref="Rectangle2D"/> class.</param>
        /// <param name="b"><see cref="Rectangle2D"/> to test.</param>
        /// <returns>Returns an <see cref="Inclusions"/> object with the points of intersection, and relationship status.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(this Rectangle2D a, Rectangle2D b)
            => RectangleContainsRectangle(a.X, a.Y, a.Width, a.Height, b.X, b.Y, b.Width, b.Height);
        #endregion Contains Extension Method Overloads

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
        public static bool PointInTriangle(
            LineSegment s, Point2D o, Point2D p,
            double epsilon = Epsilon)
        {
            _ = epsilon;
            var x = Sign(s.A, s.B, p);
            return (x == Sign(s.B, o, p)) && (x == Sign(o, s.A, p));
        }

        /// <summary>
        /// The triangle contains point.
        /// </summary>
        /// <param name="aX">The aX.</param>
        /// <param name="aY">The aY.</param>
        /// <param name="bX">The bX.</param>
        /// <param name="bY">The bY.</param>
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        /// <param name="pX">The pX.</param>
        /// <param name="pY">The pY.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>The <see cref="Inclusions"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusions TriangleContainsPoint(
            double aX, double aY, double bX, double bY, double cX, double cY,
            double pX, double pY,
            double epsilon = Epsilon)
        {
            _ = epsilon;
            var a = new Point2D(aX, aY);
            var b = new Point2D(bX, bY);
            var c = new Point2D(cX, cY);
            var p = new Point2D(pX, pY);
            if (Intersects(p, a, b) || Intersects(p, b, c) || Intersects(p, c, a))
            {
                return Inclusions.Boundary;
            }

            var clockwise = (b - a).CrossProduct(p - b) >= 0;
            return !((((c - b).CrossProduct(p - c) >= 0) ^ clockwise) && (((a - c).CrossProduct(p - a) >= 0) ^ clockwise)) ? Inclusions.Inside : Inclusions.Outside;
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
        public static Inclusions RectangleContainsPoint(
            double left, double top, double right, double bottom,
            double pX, double pY,
            double epsilon = Epsilon)
        {
            _ = epsilon;
            return (((left == pX || right == pX) && ((top <= pY) == (bottom >= pY)))
                    || ((top == pY || bottom == pY) && ((left <= pX) == (right >= pX)))) ? Inclusions.Boundary
                    : (left <= pX && pX < right && top <= pY && pY < bottom) ? Inclusions.Inside : Inclusions.Outside;
        }

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="PolygonContour"/>.
        /// </summary>
        /// <param name="points">The points that form the corners of the polygon.</param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>
        /// Returns Outside (0) if false, Inside (+1) if true, Boundary (-1) if the point is on a polygon boundary.
        /// </returns>
        /// <acknowledgment>
        /// Adapted from Clipper library: http://www.angusj.com/delphi/clipper.php
        /// See "The Point in Polygon Problem for Arbitrary Polygons" by Hormann and Agathos
        /// http://www.inf.usi.ch/hormann/papers/Hormann.2001.TPI.pdf
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusions PolygonContourContainsPoint(
            List<Point2D> points,
            double pX, double pY,
            double epsilon = Epsilon)
        {
            // Default value is no inclusion.
            var result = Inclusions.Outside;

            // Special cases for points and line segments.
            if (points.Count < 3)
            {
                if (points.Count == 1)
                {
                    // If the polygon has 1 point, it is a point and has no interior, but a point can intersect a point.
                    return (pX == points[0].X && pY == points[0].Y) ? Inclusions.Boundary : Inclusions.Outside;
                }
                else if (points.Count == 2)
                {
                    // If the polygon has 2 points, it is a line and has no interior, but a point can intersect a line.
                    return ((pX == points[0].X) && (pY == points[0].Y))
                        || ((pX == points[1].X) && (pY == points[1].Y))
                        || (((pX > points[0].X) == (pX < points[1].X))
                        && ((pY > points[0].Y) == (pY < points[1].Y))
                        && ((pX - points[0].X) * (points[1].Y - points[0].Y) == (pY - points[0].Y) * (points[1].X - points[0].X))) ? Inclusions.Boundary : Inclusions.Outside;
                }
                else
                {
                    // Empty geometry.
                    return Inclusions.Outside;
                }
            }

            // Loop through each line segment.
            var curPoint = points[0];
            for (var i = 1; i <= points.Count; ++i)
            {
                var nextPoint = i == points.Count ? points[0] : points[i];

                // Special case for horizontal lines. Check whether the point is on one of the ends, or whether the point is on the segment, if the line is horizontal.
                if (curPoint.Y == pY && (curPoint.X == pX || ((nextPoint.Y == pY) && ((curPoint.X > pX) == (nextPoint.X < pX)))))
                //if ((Abs(nextPoint.Y - pY) < epsilon) && ((Abs(nextPoint.X - pX) < epsilon) || (Abs(curPoint.Y - pY) < epsilon && ((nextPoint.X > pX) == (curPoint.X < pX)))))
                {
                    return Inclusions.Boundary;
                }

                // If Point between start and end points horizontally.
                //if ((curPoint.Y < pY) == (nextPoint.Y >= pY))
                if ((nextPoint.Y < pY) != (curPoint.Y < pY)) // At least one point is below the Y threshold and the other is above or equal
                {
                    // Optimization: at least one point must be to the right of the test point
                    // If point between start and end points vertically.
                    if (nextPoint.X >= pX)
                    {
                        if (curPoint.X > pX)
                        {
                            result = 1 - result;
                        }
                        else
                        {
                            var determinant = ((nextPoint.X - pX) * (curPoint.Y - pY)) - ((curPoint.X - pX) * (nextPoint.Y - pY));
                            if (Abs(determinant) < epsilon)
                            {
                                return Inclusions.Boundary;
                            }
                            else if ((determinant > 0) == (curPoint.Y > nextPoint.Y))
                            {
                                result = 1 - result;
                            }
                        }
                    }
                    else if (curPoint.X > pX)
                    {
                        var determinant = ((nextPoint.X - pX) * (curPoint.Y - pY)) - ((curPoint.X - pX) * (nextPoint.Y - pY));
                        if (Abs(determinant) < epsilon)
                        {
                            return Inclusions.Boundary;
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

        /// <summary>
        /// The monotones.
        /// </summary>
        /// <param name="arc">The arc.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        /// <acknowledgment>
        /// https://stackoverflow.com/a/34884949
        /// </acknowledgment>
        public static EllipticalArc[] Monotones(EllipticalArc arc)
        {
            var angles = EllipticalArcVerticalExtremeAngles(arc.RX, arc.RY, arc.Angle, arc.StartAngle, arc.SweepAngle);
            return arc.Split(angles);
        }

        /// <summary>
        /// The polycurve contour contains point.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="point">The point.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>The <see cref="Inclusions"/>.</returns>
        public static Inclusions PolycurveContourContainsPoint(
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
                        //inside += ScanbeamPointsToRightLineSegment(point.X, point.Y, l.Start.Value.X, l.Start.Value.Y, l.End.Value.X, l.End.Value.Y);
                        {
                            // Special case for horizontal lines. Check whether the point is on one of the ends, or whether the point is on the segment, if the line is horizontal.
                            if ((l.Tail.Value.Y == point.Y) && ((l.Tail.Value.X == point.X) || ((l.Head.Value.Y == point.Y) && ((l.Tail.Value.X > point.X) == (l.Head.Value.X < point.X)))))
                            //if ((Abs(nextPoint.Y - pY) < epsilon) && ((Abs(nextPoint.X - pX) < epsilon) || (Abs(curPoint.Y - pY) < epsilon && ((nextPoint.X > pX) == (curPoint.X < pX)))))
                            {
                                return Inclusions.Boundary;
                            }

                            // At least one point is below the Y threshold and the other is above or equal
                            if ((l.Head.Value.Y < point.Y) != (l.Tail.Value.Y < point.Y))
                            {
                                // At least one point must be to the right of the test point
                                if (l.Head.Value.X >= point.X)
                                {
                                    if (l.Tail.Value.X > point.X)
                                    {
                                        inside = 1 - inside;
                                    }
                                    else
                                    {
                                        var determinant = ((l.Head.Value.X - point.X) * (l.Tail.Value.Y - point.Y)) - ((l.Tail.Value.X - point.X) * (l.Head.Value.Y - point.Y));
                                        if (Abs(determinant) < epsilon)
                                        {
                                            return Inclusions.Boundary;
                                        }
                                        else if ((determinant > 0) == (l.Tail.Value.Y > l.Head.Value.Y))
                                        {
                                            inside = 1 - inside;
                                        }
                                    }
                                }
                                else if (l.Tail.Value.X > point.X)
                                {
                                    var determinant = ((l.Head.Value.X - point.X) * (l.Tail.Value.Y - point.Y)) - ((l.Tail.Value.X - point.X) * (l.Head.Value.Y - point.Y));
                                    if (Abs(determinant) < epsilon)
                                    {
                                        return Inclusions.Boundary;
                                    }

                                    if ((determinant > 0) == (l.Tail.Value.Y > l.Head.Value.Y))
                                    {
                                        inside = 1 - inside;
                                    }
                                }
                            }
                        }
                        break;
                    case ArcSegment a:
                        {
                            // https://stackoverflow.com/a/34884949
                            //var monotones = Monotones(a.ToEllipticalArc());
                            //foreach (var m in monotones)
                            var m = a.ToEllipticalArc();
                            {
                                //if (Intersections.EllipticalArcContainsPoint(m.Center.X, m.Center.Y, m.RX, m.RY, m.CosAngle, m.SinAngle, Cos(m.StartAngle), Sin(m.StartAngle), Cos(m.SweepAngle), Sin(m.SweepAngle), point.X, point.Y, epsilon) == Inclusion.Boundary)
                                //    return Inclusion.Boundary;

                                var extreams = EllipseExtremePoints(m.Center.X, m.Center.Y, m.RX, m.RY, m.CosAngle, m.SinAngle);
                                if (extreams.Contains(m.StartPoint))
                                {
                                    inside--;
                                }

                                if (extreams.Contains(m.EndPoint))
                                {
                                    inside--;
                                }

                                //if ((m.StartPoint.Y > point.Y != m.EndPoint.Y > point.Y))
                                inside += ScanbeamPointsToRightEllipticalArc(point.X, point.Y, m.Center.X, m.Center.Y, m.RX, m.RY, m.CosAngle, m.SinAngle, m.StartAngle, m.SweepAngle, epsilon);

                            }
                        }
                        break;
                    case QuadraticBezierSegment q:
                        {
                            inside += ScanbeamPointsToRightQuadraticBezierSegment(point.X, point.Y, q.Head.Value.X, q.Head.Value.Y, q.Handle.Value.X, q.Handle.Value.Y, q.Tail.Value.X, q.Tail.Value.Y);
                        }
                        break;
                    case CubicBezierSegment c:
                        {
                            inside += ScanbeamPointsToRightCubicBezierSegment(point.X, point.Y, c.Head.Value.X, c.Head.Value.Y, c.Handle1.X, c.Handle1.Y, c.Handle2.Value.X, c.Handle2.Value.Y, c.Tail.Value.X, c.Tail.Value.Y);
                        }
                        break;
                    default:
                        break;
                }
            }

            return inside % 2 == 1 ? Inclusions.Inside : Inclusions.Outside;
        }

        /// <summary>
        /// The polycurve contour contains point2.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="point">The point.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>The <see cref="Inclusions"/>.</returns>
        public static Inclusions PolycurveContourContainsPoint2(
            PolycurveContour path,
            Point2D point,
            double epsilon = Epsilon)
        {
            var result = Inclusions.Outside;
            //const Inclusion boundary = Inclusion.Outside;

            if (path.Count < 2)
            {
                return Contains(path[0].Head.Value, point);
            }

            foreach (var item in path)
            {
                switch (item)
                {
                    case PointSegment p:
                        {
                            if (path[0].Head.Value == point)
                            {
                                return Inclusions.Boundary;
                            }

                            break;
                        }
                    case LineCurveSegment l:
                        {
                            // Special case for horizontal lines. Check whether the point is on one of the ends, or whether the point is on the segment, if the line is horizontal.
                            if (l.Tail.Value.Y == point.Y && (l.Tail.Value.X == point.X || ((l.Head.Value.Y == point.Y) && ((l.Tail.Value.X > point.X) == (l.Head.Value.X < point.X)))))
                            //if ((Abs(nextPoint.Y - pY) < epsilon) && ((Abs(nextPoint.X - pX) < epsilon) || (Abs(curPoint.Y - pY) < epsilon && ((nextPoint.X > pX) == (curPoint.X < pX)))))
                            {
                                return Inclusions.Boundary;
                            }

                            // If Point between start and end points horizontally.
                            //if ((curPoint.Y < pY) == (nextPoint.Y >= pY))
                            if ((l.Head.Value.Y < point.Y) != (l.Tail.Value.Y < point.Y))
                            {
                                // If point between start and end points vertically.
                                if (l.Head.Value.X >= point.X)
                                {
                                    if (l.Tail.Value.X > point.X)
                                    {
                                        result = 1 - result;
                                    }
                                    else
                                    {
                                        var determinant = ((l.Head.Value.X - point.X) * (l.Tail.Value.Y - point.Y)) - ((l.Tail.Value.X - point.X) * (l.Head.Value.Y - point.Y));
                                        if (Abs(determinant) < epsilon)
                                        {
                                            return Inclusions.Boundary;
                                        }
                                        else if ((determinant > 0) == (l.Tail.Value.Y > l.Head.Value.Y))
                                        {
                                            result = 1 - result;
                                        }
                                    }
                                }
                                else if (l.Tail.Value.X > point.X)
                                {
                                    var determinant = ((l.Head.Value.X - point.X) * (l.Tail.Value.Y - point.Y)) - ((l.Tail.Value.X - point.X) * (l.Head.Value.Y - point.Y));
                                    if (Abs(determinant) < epsilon)
                                    {
                                        return Inclusions.Boundary;
                                    }

                                    if ((determinant > 0) == (l.Tail.Value.Y > l.Head.Value.Y))
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
                            var sX = t.Center.X + ((u1 * cosT) + (v1 * sinT));
                            var sY = t.Center.Y + ((u1 * sinT) - (v1 * cosT));
                            var eX = t.Center.X + ((u2 * cosT) + (v2 * sinT));
                            var eY = t.Center.Y + ((u2 * sinT) - (v2 * cosT));

                            // Find the determinant of the chord.
                            var determinant = ((sX - point.X) * (eY - point.Y)) - ((eX - point.X) * (sY - point.Y));

                            // Check whether the point is on the side of the chord as the center.
                            if (Sign(-determinant) == Sign(t.SweepAngle))
                            {
                                // Translate points to origin.
                                var u0 = point.X - t.Center.X;
                                var v0 = point.Y - t.Center.Y;

                                // Apply the rotation transformation.
                                var a = (u0 * cosT) + (v0 * sinT);
                                var b = (u0 * sinT) - (v0 * cosT);

                                // Normalize the radius.
                                var normalizedRadius
                                    = (a * a / (t.RX * t.RX))
                                    + (b * b / (t.RY * t.RY));

                                if (Abs(normalizedRadius - 1d) < epsilon)
                                {
                                    return Inclusions.Boundary;
                                }

                                if (normalizedRadius < 1d)
                                {
                                    result = 1 - result;
                                }
                            }

                            // If Point between start and end points horizontally.
                            //if ((curPoint.Y < pY) == (nextPoint.Y >= pY))
                            if ((t.Head.Value.Y < point.Y) != (t.Tail.Value.Y < point.Y))
                            {
                                // If point between start and end points vertically.
                                if (t.Head.Value.X >= point.X)
                                {
                                    if (t.Tail.Value.X > point.X)
                                    {
                                        result = 1 - result;
                                    }
                                    else
                                    {
                                        var determinant2 = ((t.Head.Value.X - point.X) * (t.Tail.Value.Y - point.Y)) - ((t.Tail.Value.X - point.X) * (t.Head.Value.Y - point.Y));
                                        if ((determinant2 > 0) == (t.Tail.Value.Y > t.Head.Value.Y))
                                        {
                                            result = 1 - result;
                                        }
                                    }
                                }
                                else if (t.Tail.Value.X > point.X)
                                {
                                    var determinant2 = ((t.Head.Value.X - point.X) * (t.Tail.Value.Y - point.Y)) - ((t.Tail.Value.X - point.X) * (t.Head.Value.Y - point.Y));
                                    if ((determinant2 > 0) == (t.Tail.Value.Y > t.Head.Value.Y))
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

                //if (boundary == Inclusion.Boundary)
                //{
                //    result = boundary;
                //    return result;
                //}
            }
            return result;
        }

        /// <summary>
        /// Determines whether the specified point is contained withing the set of regions defined by this <see cref="Polygon"/>.
        /// </summary>
        /// <param name="polygons">List of polygons.</param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusions PolygonContainsPoint(
            List<PolygonContour> polygons,
            double pX, double pY,
            double epsilon = Epsilon)
        {
            var returnValue = Inclusions.Outside;

            foreach (var poly in polygons)
            {
                // Use alternating rule with XOR to determine if the point is in a polygon or a hole.
                // If the point is in an odd number of polygons, it is inside. If even, it is a hole.
                returnValue ^= PolygonContourContainsPoint(poly.Points, pX, pY, epsilon);

                // Any point on any boundary is on a boundary.
                if (returnValue == Inclusions.Boundary)
                {
                    return Inclusions.Boundary;
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
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/481144/equation-for-testing-if-a-point-is-inside-a-circle
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusions CircleContainsPoint(
            double x, double y, double r,
            double pX, double pY,
            double epsilon = Epsilon)
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
                return (radiusSquared >= distanceSquared) ? ((Abs(radiusSquared - distanceSquared) < epsilon) ? Inclusions.Boundary : Inclusions.Inside) : Inclusions.Outside;
            }

            return Inclusions.Outside;
        }

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="Ellipse"/>.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="rx">The first radius of the Ellipse.</param>
        /// <param name="ry">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <param name="epsilon"></param>
        /// <acknowledgment>
        /// Based off of: http://stackoverflow.com/questions/7946187/point-and-ellipse-rotated-position-test-algorithm
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusions EllipseContainsPoint(
            double cX, double cY, double rx, double ry, double angle,
            double pX, double pY,
            double epsilon = Epsilon)
            => EllipseContainsPoint(cX, cY, rx, ry, Cos(angle), Sin(angle), pX, pY, epsilon);

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="Ellipse"/>.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="rx">The first radius of the Ellipse.</param>
        /// <param name="ry">The second radius of the Ellipse.</param>
        /// <param name="cosT">The cosine of the angle of rotation of Ellipse about it's center.</param>
        /// <param name="sinT">The sine of the angle of rotation of Ellipse about it's center.</param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Based off of: http://stackoverflow.com/questions/7946187/point-and-ellipse-rotated-position-test-algorithm
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusions EllipseContainsPoint(
            double cX, double cY, double rx, double ry, double cosT, double sinT,
            double pX, double pY,
            double epsilon = Epsilon)
        {
            if (rx <= 0d || ry <= 0d)
            {
                return Inclusions.Outside;
            }

            // Translate point to origin.
            var u = pX - cX;
            var v = pY - cY;

            // Apply the rotation transformation.
            var a = (u * cosT) + (v * sinT);
            var b = (u * sinT) - (v * cosT);

            var normalizedRadius = (a * a / (rx * rx)) + (b * b / (ry * ry));

            return (normalizedRadius <= 1d)
                ? ((Abs(normalizedRadius - 1d) < epsilon)
                ? Inclusions.Boundary : Inclusions.Inside) : Inclusions.Outside;
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
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/481144/equation-for-testing-if-a-point-is-inside-a-circle
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusions CircularArcSectorContainsPoint(
            double x, double y, double r, double startAngle, double sweepAngle,
            double pX, double pY,
            double epsilon = Epsilon)
        {
            if (r <= 0d)
            {
                return Inclusions.Outside;
            }

            // Check if it is within the bounding rectangle.
            if (pX >= x - r && pX <= x + r
                && pY >= y - r && pY <= y + r)
            {
                // Find the points of the chord.
                var (startPointX, startPointY) = Interpolators.CircularArc(0, x, y, r, startAngle, sweepAngle);
                var (endPointX, endPointY) = Interpolators.CircularArc(1, x, y, r, startAngle, sweepAngle);

                // Find the determinant of the chord and point.
                var determinant = ((startPointX - pX) * (endPointY - pY)) - ((endPointX - pX) * (startPointY - pY));

                // Check if the point is on the chord.
                if (Abs(determinant) < epsilon)
                {
                    return Inclusions.Boundary;
                }
                // Check whether the point is on the same side of the chord as the center.
                else if (Sign(determinant) == Sign(sweepAngle))
                {
                    return Inclusions.Outside;
                }

                var dx = x - pX;
                var dy = y - pY;
                dx *= dx;
                dy *= dy;
                var distanceSquared = dx + dy;
                var radiusSquared = r * r;
                return (radiusSquared >= distanceSquared) ? ((Abs(radiusSquared - distanceSquared) < epsilon) ? Inclusions.Boundary : Inclusions.Inside) : Inclusions.Outside;
            }

            return Inclusions.Outside;
        }

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="EllipticalArc"/>.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <param name="startAngle">The polar angle of where to start the arc.</param>
        /// <param name="sweepAngle">The polar angle of how far the arc should go about the ellipse.</param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Based off of: http://stackoverflow.com/questions/7946187/point-and-ellipse-rotated-position-test-algorithm
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusions EllipticalArcContainsPoint(
            double cX, double cY,
            double r1, double r2,
            double angle,
            double startAngle,
            double sweepAngle,
            double pX, double pY,
            double epsilon = Epsilon)
            => EllipticalArcContainsPoint(cX, cY, r1, r2, Cos(angle), Sin(angle), Cos(startAngle), Sin(startAngle), Cos(startAngle + sweepAngle), Sin(startAngle + sweepAngle), sweepAngle, pX, pY, epsilon);

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="EllipticalArc"/>.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="sinT">The sine of the angle of rotation of Ellipse about it's center.</param>
        /// <param name="cosT">The cosine of the angle of rotation of Ellipse about it's center.</param>
        /// <param name="startCosT">The cosine of the start angle.</param>
        /// <param name="startSinT">The sine of the start angle.</param>
        /// <param name="endCosT">The cosine of the end angle.</param>
        /// <param name="endSinT">The sine of the end angle.</param>
        /// <param name="sweepAngle">The sweep angle, to check the direction of rotation.</param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Based off of: http://stackoverflow.com/questions/7946187/point-and-ellipse-rotated-position-test-algorithm
        /// https://math.stackexchange.com/a/1760296
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusions EllipticalArcContainsPoint(
            double cX, double cY,
            double r1, double r2,
            double cosT, double sinT,
            double startCosT, double startSinT,
            double endCosT, double endSinT,
            double sweepAngle,
            double pX, double pY,
            double epsilon = Epsilon)
        {
            // If the ellipse is empty it can't contain anything.
            if (r1 <= 0d || r2 <= 0d)
            {
                return Inclusions.Outside;
            }

            // If the Sweep angle is Tau, the EllipticalArc must be an Ellipse.
            //if (Abs(sweepCosT - 1d) < epsilon && Abs(sweepSinT) < epsilon)
            if (Abs(sweepAngle) >= Tau)
            {
                return EllipseContainsPoint(cX, cY, r1, r2, cosT, sinT, pX, pY);
            }

            // Find the start and end angles.
            var sa = EllipticalPolarVector(startCosT, startSinT, r1, r2);
            var ea = EllipticalPolarVector(endCosT, endSinT, r1, r2);

            // Ellipse equation for an ellipse at origin for the chord end points.
            var u1 = r1 * sa.cosT;
            var v1 = -(r2 * sa.sinT);
            var u2 = r1 * ea.cosT;
            var v2 = -(r2 * ea.sinT);

            // Find the points of the chord.
            var sX = cX + ((u1 * cosT) + (v1 * sinT));
            var sY = cY + ((u1 * sinT) - (v1 * cosT));
            var eX = cX + ((u2 * cosT) + (v2 * sinT));
            var eY = cY + ((u2 * sinT) - (v2 * cosT));

            // Find the determinant of the chord.
            var determinant = ((sX - pX) * (eY - pY)) - ((eX - pX) * (sY - pY));

            // Check whether the point is on the same side of the chord as the center.
            if (Sign(determinant) == Sign(sweepAngle))
            {
                return Inclusions.Outside;
            }

            // Translate point to origin.
            var u0 = pX - cX;
            var v0 = pY - cY;

            // Apply the rotation transformation to the point at the origin.
            var a = (u0 * cosT) + (v0 * sinT);
            var b = (u0 * sinT) - (v0 * cosT);

            // Normalize the radius.
            var normalizedRadius
                = (a * a / (r1 * r1))
                + (b * b / (r2 * r2));

            return (normalizedRadius <= 1d)
                ? ((Abs(normalizedRadius - 1d) < epsilon)
                ? Inclusions.Boundary : Inclusions.Inside) : Inclusions.Outside;
        }

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="EllipticalArc"/>.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <param name="startAngle">The polar angle of where to start the arc.</param>
        /// <param name="sweepAngle">The polar angle of how far the arc should go about the ellipse.</param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Based off of: http://stackoverflow.com/questions/7946187/point-and-ellipse-rotated-position-test-algorithm
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusions EllipticalArcSectorContainsPoint(
            double cX, double cY,
            double r1, double r2,
            double angle,
            double startAngle,
            double sweepAngle,
            double pX, double pY,
            double epsilon = Epsilon)
            => EllipticalArcSectorContainsPoint(cX, cY, r1, r2, Cos(angle), Sin(angle), Cos(startAngle), Sin(startAngle), Cos(startAngle + sweepAngle), Sin(startAngle + sweepAngle), sweepAngle, pX, pY, epsilon);

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="EllipticalArc"/>.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="cosT">The cosine of the angle of rotation of Ellipse about it's center.</param>
        /// <param name="sinT">The sine of the angle of rotation of Ellipse about it's center.</param>
        /// <param name="startCosT">The cosine of the start angle.</param>
        /// <param name="startSinT">The sine of the start angle.</param>
        /// <param name="endCosT">The cosine of the end angle.</param>
        /// <param name="endSinT">The sine of the end angle.</param>
        /// <param name="sweepAngle">The sweep angle, to check the direction of rotation.</param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Based off of: http://stackoverflow.com/questions/7946187/point-and-ellipse-rotated-position-test-algorithm
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusions EllipticalArcSectorContainsPoint(
            double cX, double cY,
            double r1, double r2,
            double cosT, double sinT,
            double startCosT, double startSinT,
            double endCosT, double endSinT,
            double sweepAngle,
            double pX, double pY,
            double epsilon = Epsilon)
        {
            // If the ellipse is empty it can't contain anything.
            if (r1 <= 0d || r2 <= 0d)
            {
                return Inclusions.Outside;
            }

            // If the Sweep angle is Tau, the EllipticalArc must be an Ellipse.
            if (Abs(sweepAngle) >= Tau)
            {
                return EllipseContainsPoint(cX, cY, r1, r2, cosT, sinT, pX, pY);
            }

            //var endSinT = sweepSinT * startCosT + sweepCosT * startSinT;
            //var endCosT = sweepCosT * startCosT - sweepSinT * startSinT;

            // Find the start and end angles.
            var sa = EllipticalPolarVector(startCosT, startSinT, r1, r2);
            var ea = EllipticalPolarVector(endCosT, endSinT, r1, r2);

            // Ellipse equation for an ellipse at origin for the chord end points.
            var u1 = r1 * sa.cosT;
            var v1 = -(r2 * sa.sinT);
            var u2 = r1 * ea.cosT;
            var v2 = -(r2 * ea.sinT);

            // Find the points of the chord.
            var sX = cX + ((u1 * cosT) + (v1 * sinT));
            var sY = cY + ((u1 * sinT) - (v1 * cosT));
            var eX = cX + ((u2 * cosT) + (v2 * sinT));
            var eY = cY + ((u2 * sinT) - (v2 * cosT));

            // Find the determinant of the chord.
            var determinant = ((sX - pX) * (eY - pY)) - ((eX - pX) * (sY - pY));

            // Check if the point is on the chord.
            if (Abs(determinant) <= epsilon)
            {
                return (sX < eX) ?
                (sX <= pX && pX <= eX) ? Inclusions.Boundary : Inclusions.Outside :
                (eX <= pX && pX <= sX) ? Inclusions.Boundary : Inclusions.Outside;
            }

            // Check whether the point is on the side of the chord as the center.
            if (Sign(determinant) == Sign(sweepAngle))
            {
                return Inclusions.Outside;
            }

            // Translate points to origin.
            var u0 = pX - cX;
            var v0 = pY - cY;

            // Apply the rotation transformation.
            var a = (u0 * cosT) + (v0 * sinT);
            var b = (u0 * sinT) - (v0 * cosT);

            // Normalize the radius.
            var normalizedRadius
                = (a * a / (r1 * r1))
                + (b * b / (r2 * r2));

            return (normalizedRadius <= 1d)
                ? ((Abs(normalizedRadius - 1d) < epsilon)
                ? Inclusions.Boundary : Inclusions.Inside) : Inclusions.Outside;
        }

        /// <summary>
        /// The polygon set contains points.
        /// This function should be called with the full set of *all* relevant polygons.
        /// (The algorithm automatically knows that enclosed polygons are “no-go” areas.)
        /// Note:  As much as possible, this algorithm tries to return YES when the
        /// test line-segment is exactly on the border of the polygon, particularly
        /// if the test line-segment *is* a side of a polygon.
        /// </summary>
        /// <param name="polygons">The polygons.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>The <see cref="Inclusions"/>.</returns>
        /// <acknowledgment>
        /// Public-domain code by Darel Rex Finley, 2006.
        /// http://alienryderflex.com/shortest_path/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusions PolygonSetContainsPoints(
            this Polygon polygons,
            Point2D start, Point2D end,
            double epsilon = Epsilon)
        {
            end.X -= start.X;
            end.Y -= start.Y;
            var dist = Sqrt((end.X * end.X) + (end.Y * end.Y));
            var theCos = end.X / dist;
            var theSin = end.Y / dist;

            foreach (var poly in polygons.Contours)
            {
                for (var i = 0; i < poly.Points.Count; i++)
                {
                    var j = i + 1;
                    if (j == poly.Points.Count)
                    {
                        j = 0;
                    }

                    var sX = poly.Points[i].X - start.X;
                    var sY = poly.Points[i].Y - start.Y;
                    var eX = poly.Points[j].X - start.X;
                    var eY = poly.Points[j].Y - start.Y;

                    if (Abs(sX) < epsilon && Abs(sY) < epsilon
                        && Abs(eX - end.X) < epsilon && Abs(eY - end.Y) < epsilon
                        || Abs(eX) < epsilon
                        && Abs(eY) < epsilon && Abs(sX - end.X) < epsilon
                        && Abs(sY - end.Y) < epsilon)
                    {
                        return Inclusions.Inside;
                    }

                    var rotSX = (sX * theCos) + (sY * theSin);
                    var rotSY = (sY * theCos) - (sX * theSin);
                    var rotEX = (eX * theCos) + (eY * theSin);
                    var rotEY = (eY * theCos) - (eX * theSin);

                    if (rotSY < 0.0 && rotEY > 0.0
                    || rotEY < 0.0 && rotSY > 0.0)
                    {
                        var crossX = rotSX + ((rotEX - rotSX) * (0.0 - rotSY) / (rotEY - rotSY));
                        if (crossX >= 0.0 && crossX <= dist)
                        {
                            return Inclusions.Outside;
                        }
                    }

                    if (Abs(rotSY) < epsilon
                        && Abs(rotEY) < epsilon
                        && (rotSX >= 0.0 || rotEX >= 0.0)
                        && (rotSX <= dist || rotEX <= dist)
                        && (rotSX < 0.0 || rotEX < 0.0
                        || rotSX > dist || rotEX > dist))
                    {
                        return Inclusions.Outside;
                    }
                }
            }

            return PolygonContainsPoint(polygons.Contours, start.X + (end.X / 2.0), start.Y + (end.Y / 2.0));
        }

        /// <summary>
        /// Determines if the rectangular region is entirely contained within the <see cref="Rectangle2D"/> region represented by another <see cref="Rectangle2D"/>.
        /// </summary>
        /// <param name="aX">The aX.</param>
        /// <param name="aY">The aY.</param>
        /// <param name="aWidth">The aWidth.</param>
        /// <param name="aHeight">The aHeight.</param>
        /// <param name="bX">The bX.</param>
        /// <param name="bY">The bY.</param>
        /// <param name="bWidth">The bWidth.</param>
        /// <param name="bHeight">The bHeight.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RectangleContainsRectangle(
            double aX, double aY,
            double aWidth, double aHeight,
            double bX, double bY,
            double bWidth, double bHeight,
            double epsilon = Epsilon)
        {
            _ = epsilon;
            return (aX <= bX)
                   && ((bX + bWidth) <= (aX + aWidth))
                   && (aY <= bY)
                   && ((bY + bHeight) <= (aY + aHeight));
        }
        #endregion Contains Methods
    }
}
