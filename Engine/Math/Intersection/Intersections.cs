// <copyright file="Intersections.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using static Engine.Maths;
using static System.Math;

namespace Engine.Geometry
{
    /// <summary>
    /// A collection of methods for checking the interactions of objects.
    /// </summary>
    public static class Intersections
    {
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
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(double angle, double startAngle, double sweepAngle)
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
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(Vector2D a, Vector2D b, Vector2D c)
            => VectorVectorVector(a.I, a.J, b.I, b.J, c.I, c.J);

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
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool VectorVectorVector(double i0, double j0, double i1, double j1, double i2, double j2)
            => ((i1 * j0) - (j1 * i0)) * ((i1 * j2) - (j1 * i2)) >= 0
            && ((i2 * j0) - (j2 * i0)) * ((i2 * j1) - (j2 * i1)) >= 0;

        /// <summary>
        /// Check whether a point is coincident to a line segment.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Inclusion Intersects(LineSegment s, Point2D p)
            => LineSegmentPoint(s.A.X, s.A.Y, s.B.X, s.B.Y, p.X, p.Y);

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
        public static Inclusion LineSegmentPoint(
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
                (segmentBX - segmentAX) * (pointY - segmentAY))) ? Inclusion.Boundary : Inclusion.Outside;

        /// <summary>
        /// Find the intersection point between two lines.
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static (bool Intersecting, Point2D Points) Intersects(LineSegment s1, LineSegment s2)
            => LineLine(s1.A.X, s1.A.Y, s1.B.X, s1.B.Y, s2.A.X, s2.A.Y, s2.B.X, s2.B.Y);

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
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool Intersecting, Point2D Points) LineLine(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
        {
            // Translate lines to origin.
            double deltaAI = (x1 - x0);
            double deltaAJ = (y1 - y0);
            double deltaBI = (x3 - x2);
            double deltaBJ = (y3 - y2);

            // Calculate the determinant of the coefficient matrix.
            double determinant = (deltaBJ * deltaAI) - (deltaBI * deltaAJ);

            // Check if the line are parallel.
            if (Abs(determinant) < Epsilon)
                return (false, null);

            // Find the index where the intersection point lies on the line.
            double s = ((x0 - x2) * deltaAJ + (y2 - y0) * deltaAI) / -determinant;
            double t = ((x2 - x0) * deltaBJ + (y0 - y2) * deltaBI) / determinant;

            return (
                 // Check whether the point is on the segment.
                 (t >= 0d) && (t <= 1d) && (s >= 0d) && (s <= 1d),
                // If it exists, the point of intersection is:
                new Point2D(x0 + t * deltaAI, y0 + t * deltaAJ));
        }

        /// <summary>
        /// Determines whether the specified point is contained within the region defined by this <see cref="Circle"/>.
        /// </summary>
        /// <param name="circle"><see cref="Circle"/> class.</param>
        /// <param name="point">Point to test.</param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this Circle circle, Point2D point)
            => CirclePoint(circle.X, circle.Y, circle.Radius, point.X, point.Y);

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
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion CirclePoint(double x, double y, double r, double pX, double pY)
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
        /// Determines whether the specified point is contained within the region defined by this <see cref="Circle"/>.
        /// </summary>
        /// <param name="arc"><see cref="Circle"/> class.</param>
        /// <param name="point">Point to test.</param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this CircularArc arc, Point2D point)
            => CircularArcSectorPoint(arc.X, arc.Y, arc.Radius, arc.StartAngle, arc.SweepAngle, point.X, point.Y);

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
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion CircularArcSectorPoint(double x, double y, double r, double startAngle, double sweepAngle, double pX, double pY)
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
        /// <param name="ellipseArc"><see cref="Ellipse"/> class.</param>
        /// <param name="point">Point to test.</param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this EllipticalArc ellipseArc, Point2D point)
            => EllipticalArcPoint(ellipseArc.Center.X, ellipseArc.Center.Y, ellipseArc.RX, ellipseArc.RY, ellipseArc.Angle, ellipseArc.StartAngle, ellipseArc.SweepAngle, point.X, point.Y);

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
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion EllipticalArcPoint(double cX, double cY, double r1, double r2, double angle, double startAngle, double sweepAngle, double pX, double pY)
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
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion EllipticalArcSectorPoint(double cX, double cY, double r1, double r2, double angle, double startAngle, double sweepAngle, double pX, double pY)
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
        /// Determines whether the specified point is contained withing the region defined by this <see cref="Ellipse"/>.
        /// </summary>
        /// <param name="ellipse"><see cref="Ellipse"/> class.</param>
        /// <param name="point">Point to test.</param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this Ellipse ellipse, Point2D point)
            => EllipsePoint(ellipse.Center.X, ellipse.Center.Y, ellipse.R1, ellipse.R2, ellipse.Angle, point.X, point.Y);

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
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion EllipsePoint(double x, double y, double r1, double r2, double angle, double pX, double pY)
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
        /// Determines whether the specified point is contained within the rectangular region defined by this <see cref="Rectangle2D"/>.
        /// </summary>
        /// <param name="rectangle"><see cref="Rectangle2D"/> class.</param>
        /// <param name="point">Point to test.</param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this Rectangle2D rectangle, Point2D point)
            => RectanglePoint(rectangle.X, rectangle.Y, rectangle.Right, rectangle.Bottom, point.X, point.Y);

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
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion RectanglePoint(double left, double top, double right, double bottom, double pX, double pY)
        {
            if (((
                Abs(left - pX) < Epsilon
                || Abs(bottom - pX) < Epsilon)
                && ((top <= pY) == (bottom >= pY)))
                || ((Abs(right - pY) < Epsilon
                || Abs(left - pY) < Epsilon)
                && ((left <= pX) == (right >= pX))))
            {
                return Inclusion.Boundary;
            }

            return (left <= pX
                && pX < right
                && top <= pY
                && pY < bottom) ? Inclusion.Inside : Inclusion.Outside;
        }

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="Polygon"/>.
        /// </summary>
        /// <param name="polygon"><see cref="Polygon"/> class.</param>
        /// <param name="point">Point to test.</param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this Polygon polygon, Point2D point)
            => PolygonPoint(polygon.Points, point.X, point.Y);

        /// <summary>
        /// Determines whether the specified point is contained withing the region defined by this <see cref="Polygon"/>.
        /// </summary>
        /// <param name="points">The points that form the corners of the polygon.</param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion PolygonPoint(List<Point2D> points, double pX, double pY)
        {
            // From Clipper library: http://www.angusj.com/delphi/clipper.php

            // returns 0 if false, +1 if true, -1 if pt ON polygon boundary
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
        /// Determines whether the specified point is contained withing the set of regions defined by this <see cref="PolygonSet"/>.
        /// </summary>
        /// <param name="polygons">List of <see cref="Polygon"/> classes.</param>
        /// <param name="point">Point to test.</param>
        /// <returns></returns>
        /// <remarks>This function automatically knows that enclosed polygons are "no-go" areas.</remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this PolygonSet polygons, Point2D point)
            => PolygonSetPoint(polygons.Polygons, point.X, point.Y);

        /// <summary>
        /// Determines whether the specified point is contained withing the set of regions defined by this <see cref="PolygonSet"/>.
        /// </summary>
        /// <param name="polygons">List of polygons.</param>
        /// <param name="pX">The x-coordinate of the test point.</param>
        /// <param name="pY">The y-coordinate of the test point.</param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion PolygonSetPoint(List<Polygon> polygons, double pX, double pY)
        {
            Inclusion returnValue = Inclusion.Outside;

            foreach (Polygon poly in polygons)
            {
                // Use alternating rule with XOR to determine if the point is in a polygon or a hole.
                // If the point is in an odd number of polygons, it is inside. If even, it is a hole.
                returnValue ^= PolygonPoint(poly.Points, pX, pY);

                // Any point on any boundary is on a boundary.
                if (returnValue == Inclusion.Boundary)
                    return Inclusion.Boundary;
            }

            return returnValue;
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
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(this PolygonSet polygons, Point2D start, Point2D end)
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

            foreach (Polygon poly in polygons.Polygons)
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
                        return true;
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
                            return false;
                    }

                    if (Abs(rotSY) < Epsilon
                        && Abs(rotEY) < Epsilon
                        && (rotSX >= 0.0 || rotEX >= 0.0)
                        && (rotSX <= dist || rotEX <= dist)
                        && (rotSX < 0.0 || rotEX < 0.0
                        || rotSX > dist || rotEX > dist))
                    {
                        return false;
                    }
                }
            }

            return polygons.Contains(new Point2D(start.X + end.X / 2.0, start.Y + end.Y / 2.0));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="figure"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Inclusion FigurePoint(this Figure figure, Point2D point)
        {
            Inclusion included = PolygonPoint(figure.Nodes, point.X, point.Y);
            foreach (var item in figure?.Items)
            {
                switch (item)
                {
                    case FigureArc t:
                        var arc = t.Contains(point);
                        if (included == Inclusion.Boundary & arc == Inclusion.Inside) included = Inclusion.Outside;
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
        /// Determines if the rectangular region represented by <paramref name="rect2"/> is entirely contained within the rectangular region represented by  this <see cref="Rectangle2D"/> .
        /// </summary>
        /// <param name="rect1"></param>
        /// <param name="rect2"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(this Rectangle2D rect1, Rectangle2D rect2)
            => (rect1.X <= rect2.X)
            && ((rect2.X + rect2.Width) <= (rect1.X + rect1.Width))
            && (rect1.Y <= rect2.Y)
            && ((rect2.Y + rect2.Height) <= (rect1.Y + rect1.Height));

        /// <summary>
        /// Determines if this rectangle interests with another rectangle.
        /// </summary>
        /// <param name="rect1"></param>
        /// <param name="rect2"></param>
        /// <returns></returns>
        public static bool Intersects(this Rectangle2D rect1, Rectangle2D rect2)
            => RectangleRectangle(rect1.X, rect1.Y, rect1.Width, rect1.Height, rect2.X, rect2.Y, rect2.Width, rect2.Height);

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
        public static (int Count, Point2D Intersecting1, Point2D Intersecting2) CircleCircle(
            double cx0,
            double cy0,
            double radius0,
            double cx1,
            double cy1,
            double radius1)
        {
            // Find the distance between the centers.
            double dx = cx0 - cx1;
            double dy = cy0 - cy1;
            double dist = Sqrt(dx * dx + dy * dy);

            Point2D intersection1;
            Point2D intersection2;

            // See how many solutions there are.
            if (dist > radius0 + radius1)
            {
                // No solutions, the circles are too far apart.
                intersection1 = new Point2D(double.NaN, double.NaN);
                intersection2 = new Point2D(double.NaN, double.NaN);
                return (0, intersection1, intersection2);
            }
            else if (dist < Abs(radius0 - radius1))
            {
                // No solutions, one circle contains the other.
                intersection1 = new Point2D(double.NaN, double.NaN);
                intersection2 = new Point2D(double.NaN, double.NaN);
                return (0, intersection1, intersection2);
            }
            else if ((Abs(dist) < Epsilon) && (Abs(radius0 - radius1) < Epsilon))
            {
                // No solutions, the circles coincide.
                intersection1 = new Point2D(double.NaN, double.NaN);
                intersection2 = new Point2D(double.NaN, double.NaN);
                return (0, intersection1, intersection2);
            }
            else
            {
                // Find a and h.
                double a = (radius0 * radius0
                    - radius1 * radius1 + dist * dist) / (2 * dist);
                double h = Sqrt(radius0 * radius0 - a * a);

                // Find P2.
                double cx2 = cx0 + a * (cx1 - cx0) / dist;
                double cy2 = cy0 + a * (cy1 - cy0) / dist;

                // Get the points P3.
                intersection1 = new Point2D(
                    (cx2 + h * (cy1 - cy0) / dist),
                    (cy2 - h * (cx1 - cx0) / dist));
                intersection2 = new Point2D(
                    (cx2 - h * (cy1 - cy0) / dist),
                    (cy2 + h * (cx1 - cx0) / dist));

                // See if we have 1 or 2 solutions.
                if (Abs(dist - radius0 + radius1) < Epsilon)
                    return (1, intersection1, intersection2);

                return (2, intersection1, intersection2);
            }
        }

        /// <summary>
        /// Find the points of the intersection of a circle and a line segment.
        /// </summary>
        /// <param name="c"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static (bool, (double, double)?, bool, (double, double)?) Intersects(this Circle c, LineSegment s)
            => CircleLineSegment(c.X, c.Y, c.Radius, s.A.X, s.A.Y, s.B.X, s.B.Y);

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
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool, (double, double)?, bool, (double, double)?) CircleLineSegment(
            double cX, double cY,
            double radius,
            double x1, double y1,
            double x2, double y2)
        {
            double dx = x2 - x1;
            double dy = y2 - y1;

            double a = dx * dx + dy * dy;
            double b = 2 * (dx * (x1 - cX) + dy * (y1 - cY));
            double c = (x1 - cX) * (x1 - cX) + (y1 - cY) * (y1 - cY) - radius * radius;

            double determinant = b * b - 4 * a * c;

            if ((a <= 0.0000001) || (determinant < 0))
            {
                // No real solutions.
                return (false, null, false, null);
            }
            else if (determinant == 0)
            {
                // One solution.
                double t = -b / (2 * a);
                return ((t >= 0d) && (t <= 1d), (x1 + t * dx, y1 + t * dy), false, null);
            }
            else
            {
                // Two solutions.
                double t1 = ((-b + Sqrt(determinant)) / (2 * a));
                double t2 = ((-b - Sqrt(determinant)) / (2 * a));
                return ((t1 >= 0d) && (t1 <= 1d), (x1 + t1 * dx, y1 + t1 * dy),
                        (t2 >= 0d) && (t2 <= 1d), (x1 + t2 * dx, y1 + t2 * dy));
            }
        }

        /// <summary>
        /// Find the points of the intersection of an unrotated ellipse and a line segment.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool, (double, double)?, bool, (double, double)?) Intersects(this Ellipse e, LineSegment s)
            => EllipseLineSegment(e.X, e.Y, e.R1, e.R2, s.A.X, s.A.Y, s.B.X, s.B.Y);

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
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (bool, (double, double)?, bool, (double, double)?) EllipseLineSegment(
            double cx, double cy,
            double rx, double ry,
            double x0, double y0,
            double x1, double y1)
        {
            // If the ellipse or line segment are empty, return no intersections.
            if ((rx == 0d) || (ry == 0d) ||
                ((x0 == x1) && (y0 == y1)))
                return (false, null, false, null);

            // Translate so the ellipse is centered at the origin.
            double p1X = x0 - cx;
            double p1Y = y0 - cy;
            double p2X = x1 - cx;
            double p2Y = y1 - cy;

            // Calculate the quadratic parameters.
            double a = (p2X - p1X) * (p2X - p1X) / rx / rx + (p2Y - p1Y) * (p2Y - p1Y) / ry / ry;
            double b = 2d * p1X * (p2X - p1X) / rx / rx + 2 * p1Y * (p2Y - p1Y) / ry / ry;
            double c = p1X * p1X / rx / rx + p1Y * p1Y / ry / ry - 1d;

            // Calculate the discriminant.
            double discriminant = b * b - 4d * a * c;

            if (discriminant == 0)
            {
                // One real solution.
                double t = 0.5d * -b / a;

                // Return the point. If the point is on the segment set the bool to true.
                return ((t >= 0d) && (t <= 1d), (p1X + (p2X - p1X) * t + cx, p1Y + (p2Y - p1Y) * t + cy), false, null);
            }
            else if (discriminant > 0)
            {
                // Two real solutions.
                double t1 = (0.5d * (-b + Sqrt(discriminant)) / a);
                double t2 = (0.5d * (-b - Sqrt(discriminant)) / a);

                // Return the points. If the points are on the segment set the bool to true.
                return ((t1 >= 0d) && (t1 <= 1d), (p1X + (p2X - p1X) * t1 + cx, p1Y + (p2Y - p1Y) * t1 + cy),
                        (t2 >= 0d) && (t2 <= 1d), (p1X + (p2X - p1X) * t2 + cx, p1Y + (p2Y - p1Y) * t2 + cy));
            }

            // No real solutions.
            return (false, null, false, null);
        }

        /// <summary>
        /// Sutherland Hodgman Intersection. This clips the subject polygon against the clip polygon (gets the intersection of the two polygons)
        /// </summary>
        /// <param name="subjectPoly">Can be concave or convex</param>
        /// <param name="clipPoly">Must be convex</param>
        /// <returns>The intersection of the two polygons (or null)</returns>
        /// <remarks>
        /// http://rosettacode.org/wiki/Sutherland-Hodgman_polygon_clipping#C.23
        /// Based on the psuedocode from:
        /// http://en.wikipedia.org/wiki/Sutherland%E2%80%93Hodgman
        /// </remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> PolygonPolygon(List<Point2D> subjectPoly, List<Point2D> clipPoly)
        {
            if (subjectPoly.Count < 3 || clipPoly.Count < 3)
                throw new ArgumentException($"The polygons passed in must have at least 3 points: subject={subjectPoly.Count}, clip={clipPoly.Count}");

            // clone it
            List<Point2D> outputList = subjectPoly.ToList();

            // Make sure it's clockwise
            if (!PolygonExtensions.IsClockwise(subjectPoly))
                outputList.Reverse();

            // Walk around the clip polygon clockwise
            foreach (LineSegment clipEdge in PolygonExtensions.IterateEdgesClockwise(clipPoly))
            {
                // clone it
                List<Point2D> inputList = outputList.ToList();
                outputList.Clear();

                // Sometimes when the polygons don't intersect, this list goes to zero.
                // Jump out to avoid an index out of range exception
                if (inputList.Count == 0)
                    break;

                Point2D S = inputList[inputList.Count - 1];

                foreach (Point2D e in inputList)
                {
                    if (PolygonExtensions.IsInside(clipEdge, e))
                    {
                        if (!PolygonExtensions.IsInside(clipEdge, S))
                        {
                            (bool Intersects, Point2D Point) point = LineLine(S.X, S.Y, e.X, e.Y, clipEdge.A.X, clipEdge.A.Y, clipEdge.B.X, clipEdge.B.Y);
                            if (point.Intersects == false)
                            {
                                // may be collinear, or may be a bug
                                throw new ApplicationException("Line segments don't intersect");
                            }
                            else
                            {
                                outputList.Add(point.Item2);
                            }
                        }

                        outputList.Add(e);
                    }
                    else if (PolygonExtensions.IsInside(clipEdge, S))
                    {
                        (bool Intersects, Point2D Point) point = LineLine(S.X, S.Y, e.X, e.Y, clipEdge.A.X, clipEdge.A.Y, clipEdge.B.X, clipEdge.B.Y);
                        if (point.Intersects == false)
                        {
                            // may be collinear, or may be a bug
                            throw new ApplicationException("Line segments don't intersect");
                        }
                        else
                        {
                            outputList.Add(point.Item2);
                        }
                    }

                    S = e;
                }
            }

            // Exit Function
            return outputList;
        }
    }
}
