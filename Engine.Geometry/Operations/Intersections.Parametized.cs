// <copyright file="Intersections.Parametized.cs" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>

// <copyright company="kevlindev" >
//     Many of the Intersections methods were adapted from Kevin Lindsey's site http://www.kevlindev.com/gui/math/intersection/.
// Copyright © 2000 - 2003 Kevin Lindsey. All rights reserved.
// </copyright>
// <author id="thelonious">Kevin Lindsey</author>
// <license>
// Licensed under the BSD-3-Clause https://github.com/thelonious/kld-intersections/blob/development/LICENSE
// </license>

// <copyright company="angusj" >
//     The Point in Polygon method is from the Clipper Library.
// Copyright © 2010 - 2014 Angus Johnson. All rights reserved.
// </copyright>
// <author id="angusj">Angus Johnson</author>
// <license id="Boost">
// Licensed under the Boost Software License (http://www.boost.org/LICENSE_1_0.txt).
// </license>

// <copyright company="vb-helper" >
//     Some of the methods came from Rod Stephens excellent blogs vb-helper(http://vb-helper.com), and csharphelper (http://csharphelper.com), as well as from his books.
// Copyright © Rod Stephens.
// </copyright>
// <author id="RodStephens">Rod Stephens</author>
// <license id="No Restrictions">
//     You can use the code you find on this site or in my books. I request but don’t require an acknowledgment.
//     I also recommend (but again don’t require) that you put the URL where you found the code in a comment inside your code in case you need to look it up later.
//     So really no restrictions. (http://csharphelper.com/blog/rod/)
// </license>

// <summary></summary>
// <remarks></remarks>

using System.Diagnostics;
using System.Runtime.CompilerServices;
using static System.Math;

namespace Engine;

/// <summary>
/// The intersections class.
/// </summary>
public static partial class Intersections
{
    #region Parametrized Intersection Index T Methods
    /// <summary>
    /// Find the intersection parameters of the intersection between two points.
    /// </summary>
    /// <param name="p0x">The P0X.</param>
    /// <param name="p0y">The p0y.</param>
    /// <param name="p1x">The P1X.</param>
    /// <param name="p1y">The p1y.</param>
    /// <param name="epsilon">The epsilon.</param>
    /// <returns>
    /// Returns a pair of arrays of t indexes on the shapes where intersections occur.
    /// </returns>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double[] a, double[] b) PointPointIntersectionIndexes(
        double p0x, double p0y,
        double p1x, double p1y,
        double epsilon = double.Epsilon)
    {
        _ = epsilon;
        return p0x == p1x && p0y == p1y ? (new double[] { 1d }, new double[] { 1d }) : ((double[] a, double[] b))(null, null);
    }

    /// <summary>
    /// Find the intersection parameters of the intersection between a point and a line.
    /// </summary>
    /// <param name="px">The px.</param>
    /// <param name="py">The py.</param>
    /// <param name="lx">The lx.</param>
    /// <param name="ly">The ly.</param>
    /// <param name="li">The li.</param>
    /// <param name="lj">The lj.</param>
    /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
    /// <returns>
    /// Returns a pair of arrays of t indexes on the shapes where intersections occur.
    /// </returns>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double[] a, double[] b) PointLineIntersectionIndexes(
        double px, double py,
        double lx, double ly, double li, double lj,
        double epsilon = double.Epsilon)
    {
        // Vector a -> p
        (var vi, var vj) = (lx - px, ly - py);

        // Get the determinant or squared length of the line segment.
        var d = (li * li) + (lj * lj);

        // Get the length of the line segment.
        var length = Sqrt(d);

        // Find the interpolation value.
        var t = -((vi * li) + (vj * lj)) / d;

        // Check whether the closest point falls between the ends of line segment.
        // Return the t values if the distance to the nearest point on the line segment is within epsilon.
        return (length == 0) ? (Sqrt((vi * vi) + (vj * vj)) < epsilon ? ([1], [t]) : (Array.Empty<double>(), Array.Empty<double>()))
            : ((Abs((li * vj) - (vi * lj)) / length) < epsilon ? ([1], [t]) : (Array.Empty<double>(), Array.Empty<double>()));
    }

    /// <summary>
    /// Find the intersection parameters of the intersection between a point and a ray.
    /// </summary>
    /// <param name="px">The px.</param>
    /// <param name="py">The py.</param>
    /// <param name="ax">The ax.</param>
    /// <param name="ay">The ay.</param>
    /// <param name="bx">The bx.</param>
    /// <param name="by">The by.</param>
    /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
    /// <returns>
    /// Returns a pair of arrays of t indexes on the shapes where intersections occur.
    /// </returns>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double[] a, double[] b) PointRayIntersectionIndexes(
        double px, double py,
        double ax, double ay, double bx, double by,
        double epsilon = double.Epsilon)
    {
        // Vector of line segment a -> b
        (var ui, var uj) = (bx - ax, by - ay);

        // Vector a -> p
        (var vi, var vj) = (ax - px, ay - py);

        // Get the determinant or squared length of the line segment.
        var d = (ui * ui) + (uj * uj);

        // Get the length of the line segment.
        var length = Sqrt(d);

        // Find the interpolation value.
        var t = -((vi * ui) + (vj * uj)) / d;

        // Check whether the closest point falls between the ends of line segment.
        // Return the t values if the distance to the nearest point on the line segment is within epsilon.
        return (t < 0d) ? ([], [])
            : (length == 0) ? (Sqrt((vi * vi) + (vj * vj)) < epsilon ? ([1], [t]) : (Array.Empty<double>(), Array.Empty<double>()))
            : ((Abs((ui * vj) - (vi * uj)) / length) < epsilon ? ([1], [t]) : (Array.Empty<double>(), Array.Empty<double>()));
    }

    /// <summary>
    /// Find the intersection parameters of the intersections between a point and a line.
    /// </summary>
    /// <param name="px">The px.</param>
    /// <param name="py">The py.</param>
    /// <param name="ax">The ax.</param>
    /// <param name="ay">The ay.</param>
    /// <param name="bx">The bx.</param>
    /// <param name="by">The by.</param>
    /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
    /// <returns>
    /// Returns a pair of arrays of t indexes on the shapes where intersections occur.
    /// </returns>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double[] a, double[] b) PointLineSegmentIntersectionIndexes(
        double px, double py,
        double ax, double ay, double bx, double by,
        double epsilon = double.Epsilon)
    {
        // Vector of line segment a -> b
        (var ui, var uj) = (bx - ax, by - ay);

        // Vector a -> p
        (var vi, var vj) = (ax - px, ay - py);

        // Get the determinant or squared length of the line segment.
        var d = (ui * ui) + (uj * uj);

        // Get the length of the line segment.
        var length = Sqrt(d);

        // Find the interpolation value.
        var t = -((vi * ui) + (vj * uj)) / d;

        // Check whether the closest point falls between the ends of line segment.
        // Return the t values if the distance to the nearest point on the line segment is within epsilon.
        return (t < 0d || t > 1d) ? ([], [])
            : (length == 0) ? (Sqrt((vi * vi) + (vj * vj)) < epsilon ? ([1], [t]) : (Array.Empty<double>(), Array.Empty<double>()))
            : ((Abs((ui * vj) - (vi * uj)) / length) < epsilon ? ([1], [t]) : (Array.Empty<double>(), Array.Empty<double>()));
    }

    /// <summary>
    /// Find the intersection parameters of the intersection between two lines.
    /// </summary>
    /// <param name="ax">The ax.</param>
    /// <param name="ay">The ay.</param>
    /// <param name="ai">The ai.</param>
    /// <param name="aj">The aj.</param>
    /// <param name="bx">The bx.</param>
    /// <param name="by">The by.</param>
    /// <param name="bi">The bi.</param>
    /// <param name="bj">The bj.</param>
    /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
    /// <returns>
    /// Returns a pair of arrays of t indexes on the shapes where intersections occur.
    /// </returns>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double[] a, double[] b) LineLineIntersectionIndexes(
        double ax, double ay, double ai, double aj,
        double bx, double by, double bi, double bj,
        double epsilon = double.Epsilon)
    {
        _ = epsilon;
        var result = (a: Array.Empty<double>(), b: Array.Empty<double>());

        var ua = (bi * (ay - by)) - (bj * (ax - bx));
        var ub = (ai * (ay - by)) - (aj * (ax - bx));

        var determinant = (bj * ai) - (bi * aj);

        if (determinant != 0d)
        {
            var ta = ua / determinant;
            var tb = ub / determinant;

            result = (a: [ta], b: [tb]);
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
    /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
    /// <returns>
    /// Returns a pair of arrays of t indexes on the shapes where intersections occur.
    /// </returns>
    /// <acknowledgment>
    /// http://www.vb-helper.com/howto_segments_intersect.html
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double[] a, double[] b) LineRayIntersectionIndexes(
        double lx, double ly, double li, double lj,
        double rx, double ry, double ri, double rj,
        double epsilon = double.Epsilon)
    {
        // Intersection cross product.
        var ua = (li * (ry - ly)) - (lj * (rx - lx)); // Line
        var ub = (ri * (ry - ly)) - (rj * (rx - lx)); // Ray

        // Calculate the determinant of the coefficient matrix.
        var determinant = (lj * ri) - (li * rj);

        // Check if the lines are parallel.
        if (Abs(determinant) < epsilon)
        {
            if (ua == 0d || ub == 0d)
            {
                // Ray is coincident to the Line. There are an infinite number of intersections, but we only care about the start and end points of the line segment.
                return (a: [ua], b: [0d]);
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
                return (a: [ta], b: [tb]);
            }
        }

        return (a: [], b: []);
    }

    /// <summary>
    /// Find the intersection parameters of the intersection between two line segments.
    /// </summary>
    /// <param name="lx">The lx.</param>
    /// <param name="ly">The ly.</param>
    /// <param name="li">The li.</param>
    /// <param name="lj">The lj.</param>
    /// <param name="s0X">The s0 x.</param>
    /// <param name="s0Y">The s0 y.</param>
    /// <param name="s1X">The s1 x.</param>
    /// <param name="s1Y">The s1 y.</param>
    /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
    /// <returns>
    /// Returns a pair of arrays of t indexes on the shapes where intersections occur.
    /// </returns>
    /// <acknowledgment>
    /// http://www.kevlindev.com/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double[] a, double[] b) LineLineSegmentIntersectionIndexes(
        double lx, double ly, double li, double lj,
        double s0X, double s0Y, double s1X, double s1Y,
        double epsilon = double.Epsilon)
    {
        // Translate lines to origin.
        var vi = s1X - s0X;
        var vj = s1Y - s0Y;

        var ua = (vi * (ly - s0Y)) - (vj * (lx - s0X));
        var ub = (li * (ly - s0Y)) - (lj * (lx - s0X));

        // Calculate the determinant of the coefficient matrix.
        var determinant = (vj * li) - (vi * lj);

        // Check if the lines are parallel.
        if (Abs(determinant) < epsilon)
        {
            if (ua == 0d || ub == 0d)
            {
                // Line segment is coincident to the Line. There are an infinite number of intersections, but we only care about the start and end points of the line segment.
                return (a: [ua /* Is this right? */, ub /* Is this right? */], b: [0d, 1d]);
            }
        }
        else
        {
            // Find the index where the intersection point lies on the line.
            var ta = ua / determinant;
            var tb = ub / determinant;

            if (tb >= 0d && tb <= 1d)
            {
                // One intersection.
                return (a: [ta], b: [tb]);
            }
        }

        return (a: [], b: []);
    }

    /// <summary>
    /// Find the intersection parameters of the intersection between two rays.
    /// </summary>
    /// <param name="ax">The ax.</param>
    /// <param name="ay">The ay.</param>
    /// <param name="ai">The ai.</param>
    /// <param name="aj">The aj.</param>
    /// <param name="bx">The bx.</param>
    /// <param name="by">The by.</param>
    /// <param name="bi">The bi.</param>
    /// <param name="bj">The bj.</param>
    /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
    /// <returns>
    /// Returns a pair of arrays of t indexes on the shapes where intersections occur.
    /// </returns>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double[] a, double[] b) RayRayIntersectionIndexes(
        double ax, double ay, double ai, double aj,
        double bx, double by, double bi, double bj,
        double epsilon = double.Epsilon)
    {
        _ = epsilon;
        var result = (a: Array.Empty<double>(), b: Array.Empty<double>());

        var ua = (bi * (ay - by)) - (bj * (ax - bx));
        var ub = (ai * (ay - by)) - (aj * (ax - bx));

        var determinant = (bj * ai) - (bi * aj);

        if (determinant != 0d)
        {
            var ta = ua / determinant;
            var tb = ub / determinant;

            if (ta >= 0d /*&& ta <= 1d*/ && tb >= 0d /*&& tb <= 1d*/)
            {
                result = (a: [ta], b: [tb]);
            }
        }

        return result;
    }

    /// <summary>
    /// Find the intersection parameters of the intersection between a ray and a line segment.
    /// </summary>
    /// <param name="rx">The rx.</param>
    /// <param name="ry">The ry.</param>
    /// <param name="ri">The ri.</param>
    /// <param name="rj">The rj.</param>
    /// <param name="s1X">The s1 x.</param>
    /// <param name="s1Y">The s1 y.</param>
    /// <param name="s2X">The s2 x.</param>
    /// <param name="s2Y">The s2 y.</param>
    /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
    /// <returns>
    /// Returns a pair of arrays of t indexes on the shapes where intersections occur.
    /// </returns>
    /// <acknowledgment>
    /// http://www.kevlindev.com/
    /// </acknowledgment>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double[] a, double[] b) RayLineSegmentIntersectionIndexes(
        double rx, double ry, double ri, double rj,
        double s1X, double s1Y, double s2X, double s2Y,
        double epsilon = double.Epsilon)
    {
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
                // Line segment is coincident to the Line. There are an infinite number of intersections, but we only care about the start and end points of the line segment.
                return (a: [], b: []);
                // ToDo: Figure out which end points intersect which ray/line segment.
            }
        }
        else
        {
            // Find the index where the intersection point lies on the line.
            var ta = ua / determinant;
            var tb = ub / determinant;

            if (ta >= 0d /*&& ta <= 1d*/ && tb >= 0d && tb <= 1d)
            {
                // One intersection.
                return (a: [ta], b: [tb]);
            }
        }

        return (a: [], b: []);
    }

    /// <summary>
    /// Find the intersection parameters of the intersection between two line segments.
    /// </summary>
    /// <param name="aax">The aax.</param>
    /// <param name="aay">The aay.</param>
    /// <param name="abx">The abx.</param>
    /// <param name="aby">The aby.</param>
    /// <param name="bax">The bax.</param>
    /// <param name="bay">The bay.</param>
    /// <param name="bbx">The BBX.</param>
    /// <param name="bby">The bby.</param>
    /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
    /// <returns>
    /// Returns a pair of arrays of t indexes on the shapes where intersections occur.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double[] a, double[] b) LineSegmentLineSegmentIntersectionIndexes(
        double aax, double aay, double abx, double aby,
        double bax, double bay, double bbx, double bby,
        double epsilon = double.Epsilon)
    {
        _ = epsilon;
        var result = (a: Array.Empty<double>(), b: Array.Empty<double>());

        var ua = ((bbx - bax) * (aay - bay)) - ((bby - bay) * (aax - bax));
        var ub = ((abx - aax) * (aay - bay)) - ((aby - aay) * (aax - bax));

        var determinant = ((bby - bay) * (abx - aax)) - ((bbx - bax) * (aby - aay));

        if (determinant != 0d)
        {
            var ta = ua / determinant;
            var tb = ub / determinant;

            if (0d <= ta && ta <= 1d && 0d <= tb && tb <= 1d)
            {
                result = (a: [ta], b: [tb]);
            }
        }

        return result;
    }

    /// <summary>
    /// Find the intersection parameters of the self intersection of a cubic Bézier curve.
    /// </summary>
    /// <param name="xCurve">The x curve.</param>
    /// <param name="yCurve">The y curve.</param>
    /// <param name="epsilon">The epsilon.</param>
    /// <returns>
    /// Returns an array of t values for locating the self intersection indexes on the curve.
    /// </returns>
    /// <acknowledgment>
    /// https://groups.google.com/d/msg/comp.graphics.algorithms/SRm97nRWlw4/R1Rn38ep8n0J
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double[] CubicBezierSelfIntersectionIndexes(
        Polynomial<double> xCurve, Polynomial<double> yCurve,
        double epsilon = double.Epsilon)
    {
        _ = epsilon;
        (var a, var b) = (xCurve[0] == 0d) ? (xCurve[1], xCurve[2]) : (xCurve[1] / xCurve[0], xCurve[2] / xCurve[0]);
        (var p, var q) = (yCurve[0] == 0d) ? (yCurve[1], yCurve[2]) : (yCurve[1] / yCurve[0], yCurve[2] / yCurve[0]);

        if (a == p || q == b)
        {
            return [];
        }

        var k = (q - b) / (a - p);

        var roots = new Polynomial<double>(
            2d,
            -3d * k,
            (3d * k * k) + (2d * k * a) + (2d * b),
            (-k * k * k) - (a * k * k) - (b * k)
            ).Roots().ToArray().OrderByDescending(c => c).ToArray();

        if (roots.Length != 3)
        {
            return null;
        }

        if (roots[0] >= 0d && roots[0] <= 1d && roots[2] >= 0d && roots[2] <= 1d)
        {
            // ToDo: Work out whether to go the more complex route and compare the points at the t values.
            return [roots[0], roots[2]];
        }

        return [];
    }
    #endregion Parametrized Intersection Index T Methods
}
