﻿// <copyright file="IntersectionsPreview.cs" company="Shkyrockett" >
// Copyright © 2017 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine;

/// <summary>
/// The intersections preview class.
/// </summary>
public static class IntersectionsPreview
{
    /// <summary>
    /// The intersection.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <param name="segment">The segment.</param>
    /// <returns>The <see cref="Locus"/>.</returns>
    public static Locus Intersection(Point2D point, LineSegment2D segment) => LineSegmentPoint((segment?.AX).Value, (segment?.AY).Value, (segment?.BX).Value, (segment?.BY).Value, point.X, point.Y);

    /// <summary>
    /// The intersection.
    /// </summary>
    /// <param name="segment">The segment.</param>
    /// <param name="point">The point.</param>
    /// <returns>The <see cref="Locus"/>.</returns>
    public static Locus Intersection(LineSegment2D segment, Point2D point) => LineSegmentPoint((segment?.AX).Value, (segment?.AY).Value, (segment?.BX).Value, (segment?.BY).Value, point.X, point.Y);

    /// <summary>
    /// The intersection.
    /// </summary>
    /// <param name="segmentA">The segmentA.</param>
    /// <param name="segmentB">The segmentB.</param>
    /// <returns>The <see cref="Locus"/>.</returns>
    public static Locus Intersection(LineSegment2D segmentA, LineSegment2D segmentB) => LineSegmentLineSegment((segmentA?.AX).Value, (segmentA?.AY).Value, (segmentA?.BX).Value, (segmentA?.BY).Value, (segmentB?.AX).Value, (segmentB?.AY).Value, (segmentB?.BX).Value, (segmentB?.BY).Value);

    /// <summary>
    /// The line segment point.
    /// </summary>
    /// <param name="aX">The aX.</param>
    /// <param name="aY">The aY.</param>
    /// <param name="bX">The bX.</param>
    /// <param name="bY">The bY.</param>
    /// <param name="pX">The pX.</param>
    /// <param name="pY">The pY.</param>
    /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
    /// <returns>The <see cref="Locus"/>.</returns>
    public static Locus LineSegmentPoint(
        double aX, double aY,
        double bX, double bY,
        double pX, double pY,
        double epsilon = double.Epsilon)
        => (Measurements.DistanceLinePoint(aX, aY, bX, bY, pX, pY) < epsilon)
            ? new PointLocus(pX, pY)
            : (Locus)new EmptyLocus();

    /// <summary>
    /// The line point.
    /// </summary>
    /// <param name="aX">The aX.</param>
    /// <param name="aY">The aY.</param>
    /// <param name="bX">The bX.</param>
    /// <param name="bY">The bY.</param>
    /// <param name="pX">The pX.</param>
    /// <param name="pY">The pY.</param>
    /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
    /// <returns>The <see cref="Locus"/>.</returns>
    /// <remarks> <para>http://stackoverflow.com/a/19633299</para> </remarks>
    public static Locus LinePoint(
        double aX, double aY,
        double bX, double bY,
        double pX, double pY,
        double epsilon = double.Epsilon)
        => (((pX - aX) * (pX - bX)) + ((pY - aY) * (pY - bY)) < epsilon)
            ? new PointLocus(pX, pY)
            : (Locus)new EmptyLocus();

    /// <summary>
    /// The line segment line segment.
    /// </summary>
    /// <param name="a0X">The a0X.</param>
    /// <param name="a0Y">The a0Y.</param>
    /// <param name="b0X">The b0X.</param>
    /// <param name="b0Y">The b0Y.</param>
    /// <param name="a1X">The a1X.</param>
    /// <param name="a1Y">The a1Y.</param>
    /// <param name="b1X">The b1X.</param>
    /// <param name="b1Y">The b1Y.</param>
    /// <returns>The <see cref="Locus"/>.</returns>
    public static Locus LineSegmentLineSegment(
        double a0X, double a0Y,
        double b0X, double b0Y,
        double a1X, double a1Y,
        double b1X, double b1Y)
    {
        // Check whether lines are points.
        if (a0X == b0X && a0Y == b0Y && a1X == b1X && a1Y == b1Y)
        {
            // Check whether the points are the same.
            return (a0X == a1X && a0Y == a1Y)
                // Points are the same any point will do.
                ? new PointLocus(a0X, a0Y) : (Locus)new EmptyLocus();
        }
        else if (a0X == b0X && a0Y == b0Y)
        {
            // Check for intersection.
            return ((Measurements.ConstrainedDistanceLineSegmentPoint(a1X, a1Y, b1X, b1Y, a0X, a0Y) ?? 1) <= double.Epsilon)
                // First line is a point.
                ? new PointLocus(a0X, a0Y) : (Locus)new EmptyLocus();
        }
        else if (a1X == b1X && a1Y == b1Y)
        {
            // Check for intersection.
            return ((Measurements.ConstrainedDistanceLineSegmentPoint(a0X, a0Y, b0X, b0Y, a1X, a1Y) ?? 1) <= double.Epsilon)
                // Second line is a point.
                ? new PointLocus(a1X, a1Y) : (Locus)new EmptyLocus();
        }

        // Translate lines to origin as vectors.
        var u1 = b0X - a0X;
        var v1 = b0Y - a0Y;

        var u2 = b1X - a1X;
        var v2 = b1Y - a1Y;

        // Difference of the starts of the two lines
        var u3 = a1X - a0X;
        var v3 = a1Y - a0Y;

        // Calculate the determinants of the coefficient matrix.
        var det1 = (v2 * u1) - (u2 * v1);

        var det2 = (u3 * v1) - (v3 * u1);
        var det3 = (u3 * v2) - (v3 * u2);

        // Check if the lines are parallel or coincident.
        if (det1 == 0)
        {
            return (det3 == 0 || det2 == 0)
                ? new CoincidentLineLocus()
                : (Locus)new ParallelLocus();
        }

        // Find the index where the intersection point lies on the line.
        var start = det2 / det1;
        var end = det3 / det1;

        return ((end >= 0d) && (end <= 1) && (start >= 0) && (start <= 1))
            ? new PointLocus(new Point2D(a0X + (end * u1), a0Y + (end * v1)))
            : (Locus)new EmptyLocus();
    }
}
