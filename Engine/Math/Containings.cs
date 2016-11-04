// <copyright file="Containings.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using static Engine.Maths;
using static System.Math;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    public static class Containings
    {
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
            => EllipsePoint(ellipse.Center.X, ellipse.Center.Y, ellipse.RX, ellipse.RY, ellipse.Angle, point.X, point.Y);

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
            // ToDo: Convert this to an Inclusion return.
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
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion FigurePoint(Figure figure, Point2D point)
        {
            Inclusion included = PolygonPoint(figure.Nodes, point.X, point.Y);
            foreach (var item in figure?.Items)
            {
                switch (item)
                {
                    case FigureArc t:
                        // This produces false negitives at the Polygon boundaries. But that is better than false positives.
                        var arc = t.Contains(point);
                        if (included == Inclusion.Boundary & arc == Inclusion.Inside) included = Inclusion.Inside;
                        var line = Intersections.LineSegmentPoint(t.Start.X, t.Start.Y, t.End.X, t.End.Y, point.X, point.Y);
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
        /// 
        /// </summary>
        /// <param name="figure"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Inclusion Contains(this Figure figure, Point2D point)
            => FigurePoint(figure, point);

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
    }
}
