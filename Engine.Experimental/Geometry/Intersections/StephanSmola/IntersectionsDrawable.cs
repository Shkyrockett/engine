// <copyright file="IntersectionsDrawable.cs" company="Drawable.IO" >
// Copyright © 2014 - 2015 Stephan Smola. All rights reserved.
// </copyright>
// <author id="drawableIO">Stephan Smola</author>
// <license>
// Licensed not listed.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Numerics;
using System.Runtime.CompilerServices;
using static Engine.BobLyonCommon;
using static Engine.Measurements;
using static Engine.Operations;
using static System.Math;

namespace Engine;

/// <summary>
/// The intersections class.
/// </summary>
public static partial class IntersectionsDrawable
{
    /// <summary>
    /// Intersect two arbitrarily rotated ellipses. In the general case this boils down to solving a quartic equation. This can have complex results, that are ignored.
    /// Special cases like intersecting two circles or intersecting two congruent ellipses are considered. The latter can be reduced to intersecting a line with one of the ellipses.
    /// This could numerically be improved by not carrying so many intermediate results, I guess. Also the rotation to avoid problems with ellipses that are not rotated introduces numeric error as well.
    /// </summary>
    /// <param name="ellipse1">The ellipse1.</param>
    /// <param name="ellipse2">The ellipse2.</param>
    /// <param name="epsilon">The epsilon.</param>
    /// <returns>
    /// The <see cref="Array" />.
    /// </returns>
    /// <acknowledgment>
    /// https://gist.github.com/drawable/92792f59b6ff8869d8b1
    /// https://elliotnoma.wordpress.com/2013/04/10/a-closed-form-solution-for-the-intersections-of-two-ellipses/
    /// http://en.wikipedia.org/wiki/Quartic_function#General_formula_for_roots
    /// ... and some Wolfram alpha.
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double x, double y)[] IntersectEllipseEllipse(((double x, double y) origin, double radiusX, double radiusY, double angle) ellipse1, ((double x, double y) origin, double radiusX, double radiusY, double angle) ellipse2, double epsilon = double.Epsilon)
    {
        _ = epsilon;

        (double cos, double sin) rotation1 = (Cos(ellipse1.angle), Sin(ellipse1.angle));
        (double cos, double sin) rotation2 = (Cos(ellipse2.angle), Sin(ellipse2.angle));
        var wrapedAngle1 = ellipse1.angle % PI;
        var wrapedAngle2 = ellipse2.angle % PI;

        //var intersects = IntersectionsBobLyon.EllipseEllipseIntersects(ellipse1.origin.x, ellipse1.origin.y, ellipse1.radiusX, ellipse1.radiusY, rotation1.cos, rotation1.sin, ellipse2.origin.x, ellipse2.origin.y, ellipse2.radiusX, ellipse2.radiusY, rotation2.cos, rotation2.sin);

        (double a, double b, double c, double d, double e, double f) e1;
        (double a, double b, double c, double d, double e, double f) e2;

        // General case
        // Test for one situation:
        // One of the ellipses axis is parallel to x- or y-axis.
        // To avoid special cases testing in getY we simply rotate everything around ellipse1 origin by something and later
        // rotate the results back.
        var corr = ((wrapedAngle1 == 0) || (wrapedAngle2 == 0)) ? 0.05d : 0d;

        // Check if one of the ellipses has an axis parallel to the x-or y - axis.
        if (corr != 0d)
        {
            // If that is the case rotate the two ellipses by some angle to save checking of some special cases.
            e1 = GetQuadratic(ellipse1.origin, ellipse1.radiusX, ellipse1.radiusY, ellipse1.angle + corr);
            e2 = GetQuadratic(RotatePoint2D(ellipse2.origin.x, ellipse2.origin.y, corr, ellipse1.origin.x, ellipse1.origin.y), ellipse2.radiusX, ellipse2.radiusY, ellipse2.angle + corr);
        }
        else
        {
            e1 = GetQuadratic(ellipse1.origin, ellipse1.radiusX, ellipse1.radiusY, rotation1);
            e2 = GetQuadratic(ellipse2.origin, ellipse2.radiusX, ellipse2.radiusY, rotation2);
        }

        var (a, b, c, d, e) = GetQuartic(e1, e2);
        var y = QuarticRoots((a, b, c, d, e)).ToArray();

        var v = new List<(double x, double y)>();
        v.AddRange(CalculatePoints(y, e1, e2));

        if (corr != 0d)
        {
            for (var i = 0; i < v.Count; i++)
            {
                v[i] = RotatePoint2D(v[i].x, v[i].y, -corr, ellipse1.origin.x, ellipse1.origin.y);
            }
        }

        return [.. v];
    }

    /// <summary>
    /// Interpolates the ellipse y.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="cX">The h.</param>
    /// <param name="cY">The k.</param>
    /// <param name="r1">a.</param>
    /// <param name="r2">The b.</param>
    /// <param name="angle">The angle.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double x, double y)[] InterpolateEllipseY(double x, double cX, double cY, double r1, double r2, double angle)
    {
        var a2 = r1 * r1;
        var b2 = r2 * r2;
        (var cos, var sin) = (Cos(angle), Sin(angle));
        (var cos2, var sin2) = (cos * cos, sin * sin);

        var ac = a2 * cos2;
        var bs = b2 * sin2;
        var rotCX = cX * sin * cos;
        var rotX = x * sin * cos;
        var scalar = a2 * b2 / 2d * (ac + bs);
        var discriminant = 2d * 2d * (ac - bs - (cX * cX) + (2d * cX * x) - (x * x));
        var divisor = r1 * r2;
        var t1 = 2d * (rotCX + (cY * sin2) - rotX) / a2;
        var t2 = 2d * (-rotCX + (cY * cos2) + rotX) / b2;

        var y1 = scalar * ((-Sqrt(discriminant) / divisor) + t1 + t2);
        var y2 = scalar * ((Sqrt(discriminant) / divisor) + t1 + t2);

        return [(x, y1), (x, y2)];
    }

    /// <summary>
    /// Intersect two arbitrarily rotated ellipses. In the general case this boils down to solving a quartic equation. This can have complex results, that are ignored.
    /// Special cases like intersecting two circles or intersecting two congruent ellipses are considered. The latter can be reduced to intersecting a line with one of the ellipses.
    /// This could numerically be improved by not carrying so many intermediate results, I guess. Also the rotation to avoid problems with ellipses that are not rotated introduces numeric error as well.
    /// </summary>
    /// <param name="ellipse1">The ellipse1.</param>
    /// <param name="ellipse2">The ellipse2.</param>
    /// <param name="epsilon">The epsilon.</param>
    /// <returns>
    /// The <see cref="Array" />.
    /// </returns>
    /// <acknowledgment>
    /// https://gist.github.com/drawable/92792f59b6ff8869d8b1
    /// https://elliotnoma.wordpress.com/2013/04/10/a-closed-form-solution-for-the-intersections-of-two-ellipses/
    /// http://en.wikipedia.org/wiki/Quartic_function#General_formula_for_roots
    /// ... and some Wolfram alpha.
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double x, double y)[] IntersectEllipseEllipseMoc(((double x, double y) origin, double radiusX, double radiusY, double angle) ellipse1, ((double x, double y) origin, double radiusX, double radiusY, double angle) ellipse2, double epsilon = double.Epsilon)
    {
        _ = epsilon;
        var v = new List<(double x, double y)>();

        (double cos, double sin) rotation1 = (Cos(ellipse1.angle), Sin(ellipse1.angle));
        (double cos, double sin) rotation2 = (Cos(ellipse2.angle), Sin(ellipse2.angle));

        if ((ellipse1.radiusX == ellipse1.radiusY) && (ellipse2.radiusX == ellipse2.radiusY))
        {
            // Special case: Two circles
            return IntersectCircleCircle((ellipse1.origin, ellipse1.radiusX), (ellipse2.origin, ellipse2.radiusX));
        }
        else if ((ellipse1.angle == 0 || ellipse1.angle == PI) && (ellipse2.angle == 0 || ellipse2.angle == PI))
        {
            // Special case, congruent orthogonal ellipses.
            return OrthogonalEllipseOrthogonalEllipseIntersection(ellipse1.origin.x, ellipse1.origin.y, ellipse1.radiusX, ellipse1.radiusY, ellipse2.origin.x, ellipse2.origin.y, ellipse2.radiusX, ellipse2.radiusY);
        }
        else if ((ellipse1.angle == 0 || ellipse1.angle == PI) && (ellipse2.angle == Floats<double>.Hau || ellipse2.angle == Floats<double>.Pau))
        {
            // Special case, congruent ellipses, one is rotated 90 degrees.
            return OrthogonalEllipseOrthogonalEllipseIntersection(ellipse1.origin.x, ellipse1.origin.y, ellipse1.radiusX, ellipse1.radiusY, ellipse2.origin.x, ellipse2.origin.y, ellipse2.radiusY, ellipse2.radiusX);
        }
        else if ((ellipse1.angle == Floats<double>.Hau || ellipse1.angle == Floats<double>.Pau) && (ellipse2.angle == 0 || ellipse2.angle == 0))
        {
            // Special case, congruent ellipses, one is rotated 90 degrees.
            return OrthogonalEllipseOrthogonalEllipseIntersection(ellipse1.origin.x, ellipse1.origin.y, ellipse1.radiusY, ellipse1.radiusX, ellipse2.origin.x, ellipse2.origin.y, ellipse2.radiusX, ellipse2.radiusY);
        }
        else if ((ellipse1.angle == Floats<double>.Hau || ellipse1.angle == Floats<double>.Pau) && (ellipse2.angle == Floats<double>.Hau || ellipse2.angle == Floats<double>.Pau))
        {
            // Special case, congruent ellipses, both are rotated 90 degrees.
            return OrthogonalEllipseOrthogonalEllipseIntersection(ellipse1.origin.x, ellipse1.origin.y, ellipse1.radiusY, ellipse1.radiusX, ellipse2.origin.x, ellipse2.origin.y, ellipse2.radiusY, ellipse2.radiusX);
        }
        else if (((ellipse1.angle == ellipse2.angle) || Abs(ellipse1.angle - ellipse2.angle) == PI) && (ellipse1.radiusX == ellipse2.radiusX) && (ellipse1.radiusY == ellipse2.radiusY))
        {
            // Special cases congruent ellipses incl. rotation
            // There are at max two intersection points: We can construct a line that runs through these points
            var l = GetLine(rotation1, ellipse1.radiusX, ellipse1.radiusY, ellipse1.origin, ellipse2.origin);
            return IntersectLineEllipse(l, ellipse1);
        }
        else if (((Abs(ellipse1.angle - ellipse2.angle) == Floats<double>.Hau) || Abs(ellipse1.angle - ellipse2.angle) == PI * 3d / 2d) && (ellipse1.radiusX == ellipse2.radiusY) && (ellipse1.radiusY == ellipse2.radiusX))
        {
            // Special cases congruent ellipses incl. rotation but one is 90 rotated and the radius sizes are swapped
            // There are at max two intersection points: We can construct a line that runs through these points
            var l = GetLine(rotation2, ellipse1.radiusX, ellipse1.radiusY, ellipse1.origin, ellipse2.origin);
            return IntersectLineEllipse(l, ellipse1);
        }
        else
        {
            // General case
            // Test for one situation:
            // One of the ellipses axis is parallel to x- or y-axis.
            // To avoid special cases testing in getY we simply rotate everything around ellipse1 origin by something and later
            // rotate the results back.
            var mPI1 = ellipse1.angle % PI;
            var mPI2 = ellipse2.angle % PI;
            var corr = 0d;

            if ((mPI1 == 0) || (mPI2 == 0))
            {
                corr = 0.05d;
            }

            (double a, double b, double c, double d, double e, double f) e1;
            (double a, double b, double c, double d, double e, double f) e2;

            // Check if one of the ellipses has an axis parallel to the x- or y-axis.
            if (corr != 0d)
            {
                // If that is the case rotate the two ellipses by some angle to save checking of some special cases.
                e1 = GetQuadratic(ellipse1.origin, ellipse1.radiusX, ellipse1.radiusY, ellipse1.angle + corr);
                e2 = GetQuadratic(RotatePoint2D(ellipse2.origin.x, ellipse2.origin.y, corr, ellipse1.origin.x, ellipse1.origin.y), ellipse2.radiusX, ellipse2.radiusY, ellipse2.angle + corr);
            }
            else
            {
                e1 = GetQuadratic(ellipse1.origin, ellipse1.radiusX, ellipse1.radiusY, rotation1);
                e2 = GetQuadratic(ellipse2.origin, ellipse2.radiusX, ellipse2.radiusY, rotation2);
            }

            var (a, b, c, d, e) = GetQuartic(e1, e2);
            var y = Operations.QuarticRoots(ref a, ref b, ref c, ref d, ref e).ToArray();

            v.AddRange(CalculatePoints(y, e1, e2));

            if (corr != 0d)
            {
                for (var i = 0; i < v.Count; i++)
                {
                    v[i] = RotatePoint2D(v[i].x, v[i].y, -corr, ellipse1.origin.x, ellipse1.origin.y);
                }
            }
        }

        return [.. v];
    }

    /// <summary>
    /// Intersects the line ellipse.
    /// </summary>
    /// <param name="l">The l.</param>
    /// <param name="ellipse">The ellipse.</param>
    /// <param name="epsilon">The epsilon.</param>
    /// <returns>
    /// The <see cref="Array" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static (double x, double y)[] IntersectLineEllipse(((double x, double y) origin, double angle) l, ((double x, double y) origin, double radiusX, double radiusY, double angle) ellipse, double epsilon = double.Epsilon)
        => IntersectLineEllipse((l.origin, (Cos(l.angle), Sin(l.angle))), (ellipse.origin, ellipse.radiusX, ellipse.radiusY, (Cos(ellipse.angle), Sin(ellipse.angle))), epsilon);

    /// <summary>
    /// Intersects the line ellipse.
    /// </summary>
    /// <param name="l">The l.</param>
    /// <param name="ellipse">The ellipse1.</param>
    /// <param name="epsilon">The epsilon.</param>
    /// <returns>
    /// The <see cref="Array" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static (double x, double y)[] IntersectLineEllipse(((double x, double y) origin, (double cos, double sin) angle) l, ((double x, double y) origin, double radiusX, double radiusY, (double cos, double sin) angle) ellipse, double epsilon = double.Epsilon)
    {
        _ = epsilon;
        var cx = ellipse.origin.x;
        var cy = ellipse.origin.y;
        var rx = ellipse.radiusX;
        var ry = ellipse.radiusY;
        var cosA = ellipse.angle.cos;
        var sinA = ellipse.angle.sin;
        var lx = l.origin.x;
        var ly = l.origin.y;
        var li = l.angle.cos;
        var lj = l.angle.sin;

        // Initialize the resulting intersection structure.
        var result = new List<(double x, double y)>();

        // If the ellipse or line segment are empty, return no intersections.
        if ((rx == 0d) || (ry == 0d) ||
            ((lx == li) && (ly == lj)))
        {
            return [.. result];
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
        if ((a <= double.Epsilon) || (discriminant < 0))
        {
            // No real solutions.
            //result.State |= IntersectionState.Outside;
            return [.. result];
        }
        else if (discriminant == 0)
        {
            // One real possible solution.
            var t = -b / (2d * a);

            // Add the point.
            result.Add((u1 + ((u2 - u1) * t) + cx, v1 + ((v2 - v1) * t) + cy));
        }
        else if (discriminant > 0)
        {
            // Two real possible solutions.
            var t1 = (-b + Sqrt(discriminant)) / (2d * a);
            var t2 = (-b - Sqrt(discriminant)) / (2d * a);

            // Add the points.
            result.Add((u1 + ((u2 - u1) * t1) + cx, v1 + ((v2 - v1) * t1) + cy));
            result.Add((u1 + ((u2 - u1) * t2) + cx, v1 + ((v2 - v1) * t2) + cy));
        }

        // ToDo: Return IntersectionState.Inside if both points are inside the Ellipse.

        //// Return the intersections.
        //if (result.Count > 0)
        //{
        //    result.State |= IntersectionState.Intersection;
        //}

        return [.. result];
    }

    /// <summary>
    /// Intersects the circle circle.
    /// </summary>
    /// <param name="c1">The p1.</param>
    /// <param name="c2">The p2.</param>
    /// <param name="epsilon">The epsilon.</param>
    /// <returns>
    /// The <see cref="Array" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static (double x, double y)[] IntersectCircleCircle(((double x, double y) origin, double radius) c1, ((double x, double y) origin, double radius) c2, double epsilon = double.Epsilon)
    {
        _ = epsilon;
        var result = new List<(double x, double y)>();

        var r_max = c1.radius + c2.radius;
        var r_min = Abs(c1.radius - c2.radius);
        var c_dist = Distance(c1.origin.x, c1.origin.y, c2.origin.x, c2.origin.y);

        if (c_dist > r_max)
        {
            //result = new Intersection(IntersectionState.Outside);
        }
        else if (c_dist < r_min)
        {
            //result = new Intersection(IntersectionState.Inside);
        }
        else
        {
            //result = new Intersection(IntersectionState.Intersection);
            var a = ((c1.radius * c1.radius) - (c2.radius * c2.radius) + (c_dist * c_dist)) / (2d * c_dist);
            var h = Sqrt((c1.radius * c1.radius) - (a * a));
            var (x, y) = Lerp(c1.origin.x, c1.origin.y, c2.origin.x, c2.origin.y, a / c_dist);
            var b = h / c_dist;
            result.Add((x - (b * (c2.origin.y - c1.origin.y)), y + (b * (c2.origin.x - c1.origin.x))));
            result.Add((x + (b * (c2.origin.y - c1.origin.y)), y - (b * (c2.origin.x - c1.origin.x))));
        }

        return [.. result];
    }

    /// <summary>
    /// This basically calculates the rational roots of the quartic.
    /// </summary>
    /// <param name="quartics">The quartics.</param>
    /// <param name="epsilon">The epsilon.</param>
    /// <returns>
    /// The <see cref="Array" />.
    /// </returns>
    /// <acknowledgment>
    /// https://gist.github.com/drawable/92792f59b6ff8869d8b1
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double[] QuarticRoots((double a, double b, double c, double d, double e) quartics, double epsilon = double.Epsilon)
    {
        (var a, var b, var c, var d, var e) = quartics;
        //var delta = (256d * a * a * a * e * e * e) - (192d * a * a * b * d * e * e) - (128d * a * a * c * c * e * e) + (144d * a * a * c * d * d * e) - (27d * a * a * d * d * d * d) + (144d * a * b * b * c * e * e) - (6d * a * b * b * d * d * e) - (80d * a * b * c * c * d * e) + (18d * a * b * c * d * d * d) + (16d * a * c * c * c * c * e) - (4d * a * c * c * c * d * d) - (27d * b * b * b * b * e * e) + (18d * b * b * b * c * d * e) - (4d * b * b * b * d * d * d) - (4d * b * b * c * c * c * e) + (b * b * c * c * d * d);
        //var P = (8d * a * c) - (3d * b * b);
        //var D = (64d * a * a * a * e) - (16d * a * a * c * c) + (16d * a * b * b * c) - (16d * a * a * b * d) - (3d * b * b * b * b);
        var d0 = (c * c) - (3d * b * d) + (12d * a * e);
        var d1 = (2d * c * c * c) - (9d * b * c * d) + (27d * b * b * e) + (27d * a * d * d) - (72d * a * c * e);
        var p = ((8 * a * c) - (3d * b * b)) / (8d * a * a);
        var q = ((b * b * b) - (4d * a * b * c) + (8 * a * a * d)) / (8d * a * a * a);
        var phi = Acos(d1 / (2d * Sqrt(d0 * d0 * d0)));

        double S;
        if (double.IsNaN(phi) && (d1 == 0d))
        {
            // if (delta < 0) I guess the new test is okay because we're only interested in real roots
            var Q = d1 + Sqrt((d1 * d1) - (4d * d0 * d0 * d0));
            Q /= 2d;
            Q = Cbrt(Q);
            S = 0.5d * Sqrt((-2d / 3d * p) + (1d / (3d * a) * (Q + (d0 / Q))));
        }
        else
        {
            S = 0.5d * Sqrt((-2d / 3d * p) + (2d / (3d * a) * Sqrt(d0) * Cos(phi / 3d)));
        }

        var y = new List<double>();
        if (S != 0d)
        {
            var R = (-4d * S * S) - (2d * p) + (q / S);

            if (Abs(R) < epsilon)
            {
                R = 0d;
            }

            if (R > 0d)
            {
                R = 0.5d * Sqrt(R);
                y.Add((-b / (4 * a)) - S + R);
                y.Add((-b / (4 * a)) - S - R);
            }
            else if (Abs(R) < epsilon)
            {
                y.Add((-b / (4d * a)) - S);
            }

            R = (-4d * S * S) - (2d * p) - (q / S);

            if (Abs(R) < epsilon)
            {
                R = 0d;
            }

            if (R > 0d)
            {
                R = 0.5d * Sqrt(R);
                y.Add((-b / (4d * a)) + S + R);
                y.Add((-b / (4d * a)) + S - R);
            }
            else if (R == 0d)
            {
                y.Add((-b / (4d * a)) + S);
            }
        }

        return [.. y];
    }

    /// <summary>
    /// Calculate the x coordinates for the given y coordinates.
    /// </summary>
    /// <param name="y">The y.</param>
    /// <param name="el1">The el1.</param>
    /// <param name="e2">The e2.</param>
    /// <param name="epsilon">The epsilon.</param>
    /// <returns>
    /// The <see cref="Array" />.
    /// </returns>
    /// <acknowledgment>
    /// https://gist.github.com/drawable/92792f59b6ff8869d8b1
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double x, double y)[] CalculatePoints(double[] y, (double a, double b, double c, double d, double e, double f) el1, (double a, double b, double c, double d, double e, double f) e2, double epsilon = double.Epsilon)
    {
        _ = epsilon;
        var r = new List<(double x, double y)>();
        for (var i = 0; i < y?.Length; i++)
        {
            var x = -((el1.a * e2.f) + (el1.a * e2.c * y[i] * y[i]) - (e2.a * el1.c * y[i] * y[i]) + (el1.a * e2.e * y[i]) - (e2.a * el1.e * y[i]) - (e2.a * el1.f)) / ((el1.a * e2.b * y[i]) + (el1.a * e2.d) - (e2.a * el1.b * y[i]) - (e2.a * el1.d));
            r.Add((x, y[i]));
        }
        return [.. r];
    }

    /// <summary>
    /// Calculates the line that runs through the intersection points of two congruent ellipses with the same rotation.
    /// </summary>
    /// <param name="rotation">The rotation.</param>
    /// <param name="rx">The rx.</param>
    /// <param name="ry">The ry.</param>
    /// <param name="o1">The o1.</param>
    /// <param name="o2">The o2.</param>
    /// <param name="epsilon">The epsilon.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2}" />.
    /// </returns>
    /// <acknowledgment>
    /// https://gist.github.com/drawable/92792f59b6ff8869d8b1
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ((double x, double y) v, double angle) GetLine(double rotation, double rx, double ry, (double x, double y) o1, (double x, double y) o2, double epsilon = double.Epsilon)
        => GetLine((Cos(rotation), Sin(rotation)), rx, ry, o1, o2, epsilon);

    /// <summary>
    /// Calculates the line that runs through the intersection points of two congruent ellipses with the same rotation.
    /// </summary>
    /// <param name="rotation">The rotation.</param>
    /// <param name="rx">The rx.</param>
    /// <param name="ry">The ry.</param>
    /// <param name="o1">The o1.</param>
    /// <param name="o2">The o2.</param>
    /// <param name="epsilon">The epsilon.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2}" />.
    /// </returns>
    /// <acknowledgment>
    /// https://gist.github.com/drawable/92792f59b6ff8869d8b1
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ((double x, double y) p, double angle) GetLine((double cos, double sin) rotation, double rx, double ry, (double x, double y) o1, (double x, double y) o2, double epsilon = double.Epsilon)
    {
        _ = epsilon;
        //var a = rx;
        // a squared.
        var a2 = rx * rx;

        //var b = ry;
        // b squared.
        var b2 = ry * ry;

        // Sine/Cosine vector.
        var (cosT, sinT) = (rotation.cos, rotation.sin);

        // The polynomial coefficients for the initial rotation.
        var aa = (cosT * cosT / a2) + (sinT * sinT / b2);
        var bb = (2d * cosT * sinT / b2) - (2d * cosT * sinT / a2);
        var cc = (cosT * cosT / b2) + (sinT * sinT / a2);

        // Working with Ellipse 1.
        var x1 = (bb * o1.y) - (2d * aa * o1.x);
        var y1 = (bb * o1.x) + (2d * cc * o1.y);
        var v = (aa * o1.x * o1.x) + (bb * o1.x * o1.y) + (cc * o1.y * o1.y);

        // Working with Ellipse 2.
        var x2 = (bb * o2.y) - (2d * aa * o2.x);
        var y2 = (bb * o2.x) + (2d * cc * o2.y);
        var z = (aa * o2.x * o2.x) + (bb * o2.x * o2.y) + (cc * o2.y * o2.y);

        var angle = z - v; // Is z-v an angle? Can I get a direction vector instead?

        return ((o1.x + x1 - x2, o1.y + y2 - y1), angle.DegreesToRadians()); // Why don't the y values make any sense? They seem to always be near 0.
    }

    /// <summary>
    /// Find the intersection between two orthogonal ellipses.
    /// </summary>
    /// <param name="c1X">The c1X.</param>
    /// <param name="c1Y">The c1Y.</param>
    /// <param name="rx1">The rx1.</param>
    /// <param name="ry1">The ry1.</param>
    /// <param name="c2X">The c2X.</param>
    /// <param name="c2Y">The c2Y.</param>
    /// <param name="rx2">The rx2.</param>
    /// <param name="ry2">The ry2.</param>
    /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
    /// <returns>Returns an <see cref="Intersection"/> struct with a <see cref="Intersection.State"/>, and an array of <see cref="Point2D"/> structs containing any points of intersection found.</returns>
    /// <acknowledgment>
    /// http://www.kevlindev.com/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double x, double y)[] OrthogonalEllipseOrthogonalEllipseIntersection(
        double c1X, double c1Y, double rx1, double ry1,
        double c2X, double c2Y, double rx2, double ry2,
        double epsilon = double.Epsilon)
    {
        var result = new List<(double x, double y)>();

        var a = new double[] { ry1 * ry1, 0d, rx1 * rx1, -2 * ry1 * ry1 * c1X, -2d * rx1 * rx1 * c1Y, (ry1 * ry1 * c1X * c1X) + (rx1 * rx1 * c1Y * c1Y) - (rx1 * rx1 * ry1 * ry1) };
        var b = new double[] { ry2 * ry2, 0d, rx2 * rx2, -2 * ry2 * ry2 * c2X, -2d * rx2 * rx2 * c2Y, (ry2 * ry2 * c2X * c2X) + (rx2 * rx2 * c2Y * c2Y) - (rx2 * rx2 * ry2 * ry2) };

        var yRoots = Bezout(a, b).Trim().Roots();

        var norm0 = ((a[0] * a[0]) + (2d * a[1] * a[1]) + (a[2] * a[2])) * epsilon;
        //var norm1 = ((b[0] * b[0]) + (2d * b[1] * b[1]) + (b[2] * b[2])) * epsilon;

        for (var y = 0; y < yRoots.Length; y++)
        {
            var xRoots = new Polynomial<double>(
                a[0],
                a[3] + (yRoots[y] * a[1]),
                a[5] + (yRoots[y] * (a[4] + (yRoots[y] * a[2]))),
                epsilon).Trim().Roots();
            for (var x = 0; x < xRoots.Length; x++)
            {
                var test = (((a[0] * xRoots[x]) + (a[1] * yRoots[y]) + a[3]) * xRoots[x]) + (((a[2] * yRoots[y]) + a[4]) * yRoots[y]) + a[5];
                if (Abs(test) < norm0)
                {
                    test = (((b[0] * xRoots[x]) + (b[1] * yRoots[y]) + b[3]) * xRoots[x]) + (((b[2] * yRoots[y]) + b[4]) * yRoots[y]) + b[5];
                    if (Abs(test) < 1)//norm1) // Using norm1 breaks when an ellipse intersects another ellipse that 
                    {
                        result.Add((xRoots[x], yRoots[y]));
                    }
                }
            }
        }

        return [.. result];
    }

    /// <summary>
    /// Calculate the Bézier curve polynomial of ellipses.
    /// </summary>
    /// <param name="e1">First Ellipse parameters.</param>
    /// <param name="e2">Second Ellipse parameters.</param>
    /// <returns>Returns a <see cref="Polynomial"/> of the ellipse.</returns>
    /// <acknowledgment>
    /// http://www.kevlindev.com/
    /// This code is based on MgcIntr2DElpElp.cpp written by David Eberly.
    /// His code along with many other excellent examples formerly available
    /// at his site but the latest version now at: https://www.geometrictools.com/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<T> Bezout<T>(T[] e1, T[] e2)
       where T : struct, IFloatingPointIeee754<T>
    {
        if (e1 is null || e2 is null) return Polynomial<T>.Empty;
        var ab = (e1[0] * e2[1]) - (e2[0] * e1[1]);
        var ac = (e1[0] * e2[2]) - (e2[0] * e1[2]);
        var ad = (e1[0] * e2[3]) - (e2[0] * e1[3]);
        var ae = (e1[0] * e2[4]) - (e2[0] * e1[4]);
        var af = (e1[0] * e2[5]) - (e2[0] * e1[5]);

        var bc = (e1[1] * e2[2]) - (e2[1] * e1[2]);
        var be = (e1[1] * e2[4]) - (e2[1] * e1[4]);
        var bf = (e1[1] * e2[5]) - (e2[1] * e1[5]);

        var cd = (e1[2] * e2[3]) - (e2[2] * e1[3]);

        var de = (e1[3] * e2[4]) - (e2[3] * e1[4]);
        var df = (e1[3] * e2[5]) - (e2[3] * e1[5]);

        var bfPde = bf + de;
        var beMcd = be - cd;

        return new Polynomial<T>(
            /* x⁴ */ (ab * bc) - (ac * ac),
            /* x³ */ (ab * beMcd) + (ad * bc) - (Integers<T>.Two * ac * ae),
            /* x² */ (ab * bfPde) + (ad * beMcd) - (ae * ae) - (Integers<T>.Two * ac * af),
            /* x¹ */ (ab * df) + (ad * bfPde) - (Integers<T>.Two * ae * af),
            /* c  */ (ad * df) - (af * af));
    }
}
