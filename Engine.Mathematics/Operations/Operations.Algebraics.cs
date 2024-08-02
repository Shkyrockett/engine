// <copyright file="Operations.Algebraics.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

// <copyright company="kevlindev" >
// Many of the Roots methods were adapted from Kevin Lindsey's site http://www.kevlindev.com/gui/math/intersection/.
// Copyright © 2000 - 2003 Kevin Lindsey. All rights reserved.
// </copyright>
// <author id="thelonious">Kevin Lindsey</author>
// <license>
// Licensed under the BSD-3-Clause https://github.com/thelonious/kld-intersections/blob/development/LICENSE
// </license>

using System.Numerics;
using System.Runtime.CompilerServices;
using static System.Math;

namespace Engine;

/// <summary>
/// Algebraic Operations class.
/// </summary>
public static partial class Operations
{
    /// <summary>
    /// The inverse sqrt.
    /// </summary>
    /// <param name="number">The number.</param>
    /// <returns>
    /// The <see cref="double" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T InverseSqrt<T>(T number) where T : IFloatingPointIeee754<T> => T.One / T.Sqrt(number);

    /// <summary>
    /// Returns the specified root a specified number.
    /// </summary>
    /// <param name="x">A double-precision floating-point number to find the specified root of.</param>
    /// <param name="y">A double-precision floating-point number that specifies a root.</param>
    /// <returns>
    /// The y root of the number x.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Root<T>(T x, T y) where T : IFloatingPointIeee754<T> => (x < T.Zero && T.Abs((y % T.CreateTruncating(2)) - T.One) < T.Epsilon) ? -T.Pow(-x, T.One / y) : T.Pow(x, T.One / y);

    /// <summary>
    /// Cube root equivalent of the sqrt function. (note that there are actually
    /// three roots: one real, two complex, and we don't care about the latter):
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <acknowledgment>
    /// http://stackoverflow.com/questions/26823024/cubic-bezier-reverse-getpoint-equation-float-for-vector-vector-for-float?answertab=active#tab-top
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T CubeRoot<T>(T value) where T : IFloatingPointIeee754<T> => value < T.Zero ? -T.Cbrt(-value) : T.Cbrt(value);

    /// <summary>
    /// The inverse cube root.
    /// </summary>
    /// <param name="number">The number.</param>
    /// <returns>
    /// The <see cref="double" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T InverseCubeRoot<T>(T number) where T : IFloatingPointIeee754<T> => T.One / CubeRoot(number);

    /// <summary>
    /// Calculates the real order or degree of the polynomial.
    /// </summary>
    /// <param name="coefficients">The coefficients.</param>
    /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
    /// <returns>
    /// Returns a <see cref="PolynomialDegree" /> value representing the order of degree of the polynomial.
    /// </returns>
    /// <remarks>
    /// <para>Primarily used to locate where to trim off any leading zero coefficients of the internal coefficients array.</para>
    /// </remarks>
    /// <acknowledgment>
    /// A hodge-podge helper method based on Simplify from of: http://www.kevlindev.com/
    /// as well as Trim and RealOrder from: https://github.com/superlloyd/Poly
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static PolynomialDegree DegreeRealOrder<T>(Span<T> coefficients, T? epsilon = default)
        where T : IFloatingPointIeee754<T>
    {
        var pos = 1;
        var count = coefficients.Length;

        if (epsilon is null || epsilon == default) epsilon = T.Epsilon; // If only T.Epsilon could be a default parameter value...

        // Monomial can be a zero constant, skip them and check the rest.
        if (count > 1)
        {
            // Count the number of leading zeros. Because the coefficients array is reversed, start at the end because there should generally be fewer leading zeros than other coefficients.
            for (var i = count - 1; i >= 1 /* Monomials can be 0. */; i--)
            {
                // Check if coefficient is a leading zero.
                if (T.Abs(coefficients[i]) <= epsilon)
                {
                    pos++;
                }
                else
                {
                    // Break early if a non zero value was found. This indicates the end of any leading zeros.
                    break;
                }
            }
        }

        // If coefficients is empty return constant, otherwise return the calculated order of degree of the polynomial.
        return (PolynomialDegree)(count - pos);
    }

    #region D Root Finding
    /// <summary>
    /// The d roots.
    /// </summary>
    /// <param name="coefficients">The coefficients.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static IList<T> DRoots<T>(params IList<T> coefficients)
        where T : IFloatingPointIeee754<T>
        // ToDo: What are DRoots?
        => coefficients?.Count switch
        {
            2 => LinearDRoots(coefficients[0], coefficients[1], T.Epsilon),
            3 => QuadraticDRoots(coefficients[0], coefficients[1], coefficients[2], T.Epsilon),
            _ => Array.Empty<T>(),
        };

    /// <summary>
    /// The d roots.
    /// </summary>
    /// <param name="coefficients">The coefficients.</param>
    /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
    /// <returns>
    /// The <see cref="IList{T}" />.
    /// </returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static IList<T> DRoots<T>(IList<T> coefficients, T? epsilon = default)
        // ToDo: What are DRoots?
        where T : IFloatingPointIeee754<T>
        => coefficients?.Count switch
        {
            2 => LinearDRoots(coefficients[0], coefficients[1], epsilon),
            3 => QuadraticDRoots(coefficients[0], coefficients[1], coefficients[2], epsilon),
            _ => Array.Empty<T>(),
        };

    /// <summary>
    /// The linear d roots.
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
    /// <returns>
    /// The <see cref="IList{T}" />.
    /// </returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static IList<T> LinearDRoots<T>(T a, T b, T? epsilon = default)
        where T : IFloatingPointIeee754<T>
    {
        // ToDo: What are DRoots?
        _ = epsilon;
        return a != b ? ([a / (a - b)]) : Array.Empty<T>();
    }

    /// <summary>
    /// The quadratic d roots.
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    /// <param name="c">The c.</param>
    /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
    /// <returns>
    /// The <see cref="IList{T}" />.
    /// </returns>
    /// <acknowledgment>
    /// http://pomax.github.io/bezierinfo/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static IList<T> QuadraticDRoots<T>(T a, T b, T c, T? epsilon = default)
        where T : IFloatingPointIeee754<T>
    {
        // ToDo: What are DRoots?
        _ = epsilon;
        var det = a - (T.CreateTruncating(2) * b) + c;
        if (det != T.Zero)
        {
            // Negative square root discriminant. Missing the 4?
            var sqrtd = -T.Sqrt((b * b) - (a * c));
            var m2 = b - a;
            var v1 = -(m2 + sqrtd) / det;
            var v2 = -(m2 - sqrtd) / det;
            return [v1, v2];
        }
        else if (b != c && det == T.Zero)
        {
            return [((T.CreateTruncating(2) * b) - c) / (T.CreateTruncating(2) * (b - c))];
        }

        return Array.Empty<T>();
    }
    #endregion D Root Finding

    #region Root Finding
    /// <summary>
    /// Find the Roots of up to Quintic degree <see cref="Polynomial"/>s.
    /// </summary>
    /// <param name="coefficients">The coefficients.</param>
    /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
    /// <returns>The <see cref="Array"/>.</returns>
    /// <acknowledgment>
    /// http://www.kevlindev.com/geometry/2D/intersections/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static unsafe Span<T> Roots<T>(Span<T> coefficients, T? epsilon = default)
        where T : struct, IFloatingPointIeee754<T>
        => DegreeRealOrder(coefficients) switch
        {
            PolynomialDegree.Constant => new T[] { coefficients[0] },
            PolynomialDegree.Linear => LinearRoots(ref coefficients[1], ref coefficients[0], epsilon),
            PolynomialDegree.Quadratic => QuadraticRoots(ref coefficients[2], ref coefficients[1], ref coefficients[0], epsilon),
            PolynomialDegree.Cubic => CubicRoots(ref coefficients[3], ref coefficients[2], ref coefficients[1], ref coefficients[0], epsilon),
            PolynomialDegree.Quartic => QuarticRoots(ref coefficients[4], ref coefficients[3], ref coefficients[2], ref coefficients[1], ref coefficients[0], epsilon),
            PolynomialDegree.Quintic => QuinticRoots(ref coefficients[5], ref coefficients[4], ref coefficients[3], ref coefficients[2], ref coefficients[1], ref coefficients[0], epsilon),
            //PolynomialDegree.Sextic => Array.Empty<double>(), //SexticRoots(epsilon),
            //PolynomialDegree.Septic => Array.Empty<double>(), //SepticRoots(epsilon),
            //PolynomialDegree.Octic => Array.Empty<double>(), //OcticRoots(epsilon),
            _ => [], // Should try Newton's method and/or bisection
        };

    /// <summary>
    /// The linear roots.
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
    /// <returns>The <see cref="List{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static unsafe Span<T> LinearRoots<T>(T a, T b, T? epsilon = default) where T : struct, IFloatingPointIeee754<T> => LinearRoots(ref a, ref b, epsilon);

    /// <summary>
    /// The linear roots.
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
    /// <returns>The <see cref="List{T}"/>.</returns>
    /// <acknowledgment>
    /// http://www.kevlindev.com/geometry/2D/intersections/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static unsafe Span<T> LinearRoots<T>(ref T a, ref T b, T? epsilon = default)
        where T : struct, IFloatingPointIeee754<T>
    {
        epsilon ??= T.Epsilon;
        return T.Abs(a) < epsilon ? T.Abs(b) < epsilon ? Array.Empty<T>() : [b] : [-b / a];
    }

    /// <summary>
    /// The quadratic roots.
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    /// <param name="c">The c.</param>
    /// <param name = "epsilon"> The minimal value to represent a change.</param>
    /// <returns>The <see cref="List{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static unsafe Span<T> QuadraticRoots<T>(T a, T b, T c, T? epsilon = default) where T : struct, IFloatingPointIeee754<T> => QuadraticRoots(ref a, ref b, ref c, epsilon);

    /// <summary>
    /// The quadratic roots.
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    /// <param name="c">The c.</param>
    /// <param name = "epsilon"> The minimal value to represent a change.</param>
    /// <returns>The <see cref="List{T}"/>.</returns>
    /// <acknowledgment>
    /// http://www.kevlindev.com/geometry/2D/intersections/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static unsafe Span<T> QuadraticRoots<T>(ref T a, ref T b, ref T c, T? epsilon = default)
        where T : struct, IFloatingPointIeee754<T>
    {
        epsilon ??= T.Epsilon;

        // Is the coefficient of the highest term zero?
        if (T.Abs(a) < epsilon)
        {
            // If the highest term coefficient is 0, then it is a lower degree polynomial.
            return LinearRoots(ref b, ref c, epsilon);
        }

        var ba = b / a;
        var ca = c / a;

        // Polynomial discriminant
        var discriminant = (ba * ba) - (T.CreateTruncating(4) * ca);

        if (T.Abs(discriminant) <= epsilon)
        {
            discriminant = T.Zero;
        }

        switch (discriminant)
        {
            case 0:
                return new T[] { T.CreateSaturating(Floats<double>.OneHalf) * -ba };
            case T v when v > T.Zero:
                {
                    var e = T.Sqrt(discriminant);
                    return new T[] { T.CreateSaturating(Floats<double>.OneHalf) * (-ba + e), T.CreateSaturating(Floats<double>.OneHalf) * (-ba - e) };
                }
            default:
                {
                    var e = -T.Sqrt(-discriminant);
                    return new T[] { T.CreateSaturating(Floats<double>.OneHalf) * (-ba + e), T.CreateSaturating(Floats<double>.OneHalf) * (-ba - e) };
                }
        }
    }

    /// <summary>
    /// Cubic Roots
    /// </summary>
    /// <param name="a">t^3</param>
    /// <param name="b">t^2</param>
    /// <param name="c">t</param>
    /// <param name="d">1</param>
    /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static unsafe Span<T> CubicRoots<T>(T a, T b, T c, T d, T? epsilon = default) where T : struct, IFloatingPointIeee754<T> => CubicRoots(ref a, ref b, ref c, ref d, epsilon);

    /// <summary>
    /// Cubic Roots
    /// </summary>
    /// <param name="a">t^3</param>
    /// <param name="b">t^2</param>
    /// <param name="c">t</param>
    /// <param name="d">1</param>
    /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// based on http://abecedarical.com/javascript/script_exact_cubic.html
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static unsafe Span<T> CubicRoots<T>(ref T a, ref T b, ref T c, ref T d, T? epsilon = default)
        where T : struct, IFloatingPointIeee754<T>
    {
        epsilon ??= T.Epsilon;

        // Is the coefficient of the highest term zero?
        if (T.Abs(a) < epsilon)
        {
            // If the highest term coefficient is 0, then it is a lower degree polynomial.
            return QuadraticRoots(ref b, ref c, ref d, epsilon);
        }

        var ba = b / a;
        var ca = c / a;
        var da = d / a;

        var q = ((T.CreateTruncating(3) * ca) - (ba * ba)) / T.CreateTruncating(9);
        var r = (-(T.CreateTruncating(2) * ba * ba * ba) + (T.CreateTruncating(9) * ba * ca) - (T.CreateTruncating(27) * da)) / T.CreateTruncating(54);

        var offset = ba * T.CreateTruncating(Floats<double>.OneThird);

        // Polynomial discriminant
        var discriminant = (r * r) + (q * q * q);

        if (T.Abs(discriminant) <= epsilon)
        {
            discriminant = T.Zero;
        }

        switch (discriminant)
        {
            case T v when v == T.Zero:
                {
                    var t = T.CreateTruncating(T.Sign(r)) * T.Cbrt(T.Abs(r));
                    return new T[] {
                     -offset + (t + t),
                      -offset - ((t + t) * T.CreateSaturating(Floats<double>.OneHalf))
                     };
                }
            case T v when v > T.Zero:
                {
                    var e = T.Sqrt(discriminant);
                    var s = T.CreateTruncating(T.Sign(r + e)) * T.Cbrt(T.Abs(r + e));
                    var t = T.CreateTruncating(T.Sign(r - e)) * T.Cbrt(T.Abs(r - e));
                    var im = T.Abs(T.Sqrt(T.CreateTruncating(3)) * (s - t) * T.CreateSaturating(Floats<double>.OneHalf));
                    //return im == T.Zero ?
                    //    new T[] { -offset + (s + t) } 
                    //    : new T[] { -offset + (s + t), -offset - ((s + t) * T.CreateSaturating(OneHalf)) };
                    // Complex part of root pair.
                    if (im == T.Zero)
                    {
                        // Real part of complex root.
                        return new T[] {
                        -offset + (s + t),
                        -offset - ((s + t) * T.CreateSaturating(Floats<double>.OneHalf))
                        };
                    }
                    else
                    {
                        return new T[] { -offset + (s + t) };
                    }
                }
            default:
                {
                    var th = T.Acos(r / T.Sqrt(-q * q * q));
                    return new T[] {
                        (T.CreateTruncating(2d) * T.Sqrt(-q) * T.Cos(th * T.CreateSaturating(Floats<double>.OneThird))) - offset,
                        (T.CreateTruncating(2d) * T.Sqrt(-q) * T.Cos((th + T.Tau) * T.CreateSaturating(Floats<double>.OneThird))) - offset,
                        (T.CreateTruncating(2d) * T.Sqrt(-q) * T.Cos((th + (T.CreateTruncating(4) * T.Pi)) * T.CreateSaturating(Floats<double>.OneThird))) - offset };
                }
        }
    }

    /// <summary>
    /// The quartic roots.
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    /// <param name="c">The c.</param>
    /// <param name="d">The d.</param>
    /// <param name="e">The e.</param>
    /// <param name = "epsilon"> The minimal value to represent a change.</param>
    /// <returns>The <see cref="List{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static unsafe Span<T> QuarticRoots<T>(T a, T b, T c, T d, T e, T? epsilon = default) where T : struct, IFloatingPointIeee754<T> => QuarticRoots(ref a, ref b, ref c, ref d, ref e, epsilon);

    /// <summary>
    /// The quartic roots.
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    /// <param name="c">The c.</param>
    /// <param name="d">The d.</param>
    /// <param name="e">The e.</param>
    /// <param name = "epsilon"> The minimal value to represent a change.</param>
    /// <returns>The <see cref="List{T}"/>.</returns>
    /// <remarks>
    /// <para>ToDo: Translate code found at: https://web.archive.org/web/20150504111126/http://abecedarical.com/javascript/script_quintic.html
    /// and http://jwezorek.com/2015/01/my-code-for-doing-two-things-that-sooner-or-later-you-will-want-to-do-with-bezier-curves/:
    /// This method computes complex and real roots for any quintic polynomial. Then returns the real roots.
    /// It applies the Lin-Bairstow algorithm which iteratively solves for the
    /// roots starting from random guesses for a solution.
    /// The calculator is designed to solve for the roots of a quintic polynomial
    /// with the form: x⁵ + ax⁴ + bx³ + cx² + dx + e = 0
    /// ⁰¹²³⁴⁵⁶⁷⁸⁹</para>
    /// </remarks>
    /// <acknowledgment>
    /// http://www.kevlindev.com/geometry/2D/intersections/
    /// https://web.archive.org/web/20150504111126/http://abecedarical.com/javascript/script_quintic.html
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static unsafe Span<T> QuarticRoots<T>(ref T a, ref T b, ref T c, ref T d, ref T e, T? epsilon = default)
        where T : struct, IFloatingPointIeee754<T>
    {
        epsilon ??= T.Epsilon;

        // Is the coefficient of the highest term zero?
        if (T.Abs(a) < epsilon)
        {
            // If the highest term coefficient is 0, then it is a lower degree polynomial.
            return CubicRoots(ref b, ref c, ref d, ref e, epsilon);
        }

        // ToDo: Translate code found at: https://web.archive.org/web/20150504111126/http://abecedarical.com/javascript/script_quintic.html
        // and http://jwezorek.com/2015/01/my-code-for-doing-two-things-that-sooner-or-later-you-will-want-to-do-with-bezier-curves/

        var ba = b / a;
        var ca = c / a;
        var da = d / a;
        var ea = e / a;

        var resolveRoots = CubicRoots(
            T.One,
            -ca,
            (ba * da) - (T.CreateTruncating(4) * ea),
            (-ba * ba * ea) + (T.CreateTruncating(4) * ca * ea) - (da * da),
            epsilon);
        var y = resolveRoots[0];
        var discriminant = (ba * ba * T.CreateSaturating(Floats<double>.OneQuarter)) - ca + y;

        // ToDo: May need to switch from a hash set to a list for scan-beams.
        var results = new HashSet<T>();

        if (T.Abs(discriminant) <= epsilon)
        {
            discriminant = T.Zero;
        }

        switch (discriminant)
        {
            case T v when v == T.Zero:
                {
                    var t2 = (y * y) - (T.CreateTruncating(4) * ea);
                    if (t2 >= -epsilon)
                    {
                        if (t2 < T.Zero)
                        {
                            t2 = T.Zero;
                        }

                        t2 = T.CreateTruncating(2) * T.Sqrt(t2);
                        var t1 = (T.CreateTruncating(3) * ba * ba * T.CreateSaturating(Floats<double>.OneQuarter)) - (T.CreateTruncating(2) * ca);
                        if (t1 + t2 >= epsilon)
                        {
                            var d0 = T.Sqrt(t1 + t2);
                            results.Add((-ba * T.CreateSaturating(Floats<double>.OneQuarter)) + (d0 * T.CreateSaturating(Floats<double>.OneHalf)));
                            results.Add((-ba * T.CreateSaturating(Floats<double>.OneQuarter)) - (d0 * T.CreateSaturating(Floats<double>.OneHalf)));
                        }
                        if (t1 - t2 >= epsilon)
                        {
                            var d1 = T.Sqrt(t1 - t2);
                            results.Add((-ba * T.CreateSaturating(Floats<double>.OneQuarter)) + (d1 * T.CreateSaturating(Floats<double>.OneHalf)));
                            results.Add((-ba * T.CreateSaturating(Floats<double>.OneQuarter)) - (d1 * T.CreateSaturating(Floats<double>.OneHalf)));
                        }
                    }
                    return results.ToArray();
                }
            case T v when v > T.Zero:
                {
                    var ee = T.Sqrt(discriminant);
                    var t1 = (T.CreateTruncating(3) * ba * ba * T.CreateSaturating(Floats<double>.OneQuarter)) - (ee * ee) - (T.CreateTruncating(2) * ca);
                    var t2 = ((T.CreateTruncating(4) * ba * ca) - (T.CreateTruncating(8) * da) - (ba * ba * ba)) / (T.CreateTruncating(4) * ee);
                    var plus = t1 + t2;
                    var minus = t1 - t2;

                    if (T.Abs(plus) <= epsilon)
                    {
                        plus = T.Zero;
                    }

                    if (T.Abs(minus) <= epsilon)
                    {
                        minus = T.Zero;
                    }

                    if (plus >= T.Zero)
                    {
                        var f = T.Sqrt(plus);
                        results.Add((-ba * T.CreateSaturating(Floats<double>.OneQuarter)) + ((ee + f) * T.CreateSaturating(Floats<double>.OneHalf)));
                        results.Add((-ba * T.CreateSaturating(Floats<double>.OneQuarter)) + ((ee - f) * T.CreateSaturating(Floats<double>.OneHalf)));
                    }
                    if (minus >= T.Zero)
                    {
                        var f = T.Sqrt(minus);
                        results.Add((-ba * T.CreateSaturating(Floats<double>.OneQuarter)) + ((f - ee) * T.CreateSaturating(Floats<double>.OneHalf)));
                        results.Add((-ba * T.CreateSaturating(Floats<double>.OneQuarter)) - ((f + ee) * T.CreateSaturating(Floats<double>.OneHalf)));
                    }
                    return results.ToArray();
                }
            default:
                {
                    // Imaginary roots?
                    return results.ToArray();
                }
        }
    }

    /// <summary>
    /// The quintic roots.
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    /// <param name="c">The c.</param>
    /// <param name="d">The d.</param>
    /// <param name="e">The e.</param>
    /// <param name="f">The f.</param>
    /// <param name = "epsilon"> The minimal value to represent a change.</param>
    /// <returns>The <see cref="List{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static unsafe Span<T> QuinticRoots<T>(T a, T b, T c, T d, T e, T f, T? epsilon = default) where T : struct, IFloatingPointIeee754<T> => QuinticRoots(ref a, ref b, ref c, ref d, ref e, ref f, epsilon);

    /// <summary>
    /// The quintic roots.
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    /// <param name="c">The c.</param>
    /// <param name="d">The d.</param>
    /// <param name="e">The e.</param>
    /// <param name="f">The f.</param>
    /// <param name = "epsilon"> The minimal value to represent a change.</param>
    /// <returns>The <see cref="List{T}"/>.</returns>
    /// <acknowledgment>
    /// This is a Copy and paste port of the method found at:
    /// https://web.archive.org/web/20150504111126/http://abecedarical.com/javascript/script_quintic.html
    /// http://www.convertalot.com/quintic_root_calculator.html
    /// There has been little attempt to fix it up and get it working correctly.
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static unsafe Span<T> QuinticRoots<T>(ref T a, ref T b, ref T c, ref T d, ref T e, ref T f, T? epsilon = default)
        where T : struct, IFloatingPointIeee754<T>
    {
        epsilon ??= T.Epsilon;

        // Is the coefficient of the highest term zero?
        if (T.Abs(a) < epsilon)
        {
            // If the highest term coefficient is 0, then it is a lower degree polynomial.
            return QuarticRoots(ref b, ref c, ref d, ref e, ref f, epsilon);
        }

        //var coeff = new List<double> { a, b, c, d, e, f };

        // Order
        var n = 4; // 5;
        var n1 = 5; // 6;
        var n2 = 6; // 7;

        var a_ = new Span<T>([f, e, d, c, b, a]);
        var b_ = new Span<T>([T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero]);
        //var c_ = new Span<T>(new T[] { T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero }) ;
        var d_ = new Span<T>([T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero]);
        var real = new Span<T>([T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero]);
        var imag = new Span<T>([T.Zero, T.Zero, T.Zero, T.Zero, T.Zero, T.Zero]);

        ////  Copy into working array
        //for (var i = 0; i <= n; i++)
        //{
        //    a_[a_.Count - 1 - i] = coeff[i];
        //}

        // Initialize root counter
        var count = 0;

        // Start the main Lin-Bairstow iteration loop
        do
        {
            // Initialize the counter and guesses for the coefficients of quadratic factor: p(x) = x^2 + alfa1*x + beta1
            // ToDo: The random alphas make this method non-deterministic. Need a better guess method.
            var alfa1 = Maths.Random(T.CreateSaturating(Floats<double>.OneHalf), T.One);
            var beta1 = Maths.Random(T.CreateSaturating(Floats<double>.OneHalf), T.One);
            var limit = 1000;

            T delta1;
            do
            {
                b_[0] = T.Zero;
                d_[0] = T.Zero;
                b_[1] = T.One;
                d_[1] = T.One;

                for (int i = 2, j = 1, k = 0; i < a_.Length; i++)
                {
                    b_[i] = a_[i] - (alfa1 * b_[j]) - (beta1 * b_[k]);
                    d_[i] = b_[i] - (alfa1 * d_[j]) - (beta1 * d_[k]);
                    j += 1;
                    k += 1;
                }

                T alfa2;

                T beta2;
                {
                    var j = n - 1;
                    var k = n - 2;
                    delta1 = (d_[j] * d_[j]) - ((d_[n] - b_[n]) * d_[k]);
                    alfa2 = ((b_[n] * d_[j]) - (b_[n1] * d_[k])) / delta1;
                    beta2 = ((b_[n1] * d_[j]) - ((d_[n] - b_[n]) * b_[n])) / delta1;
                    alfa1 += alfa2;
                    beta1 += beta2;
                }

                if (--limit < 0)
                {
                    // Cannot solve
                    return new Span<T>();
                }

                if (T.Abs(alfa2) < epsilon && T.Abs(beta2) < epsilon)
                {
                    break;
                }
            }
            while (true);

            delta1 = (alfa1 * alfa1) - (T.CreateTruncating(4) * beta1);

            T delta2;
            // Imaginary roots
            if (delta1 < T.Zero)
            {
                delta2 = T.Sqrt(T.Abs(delta1)) * T.CreateSaturating(Floats<double>.OneHalf);
                var delta3 = -alfa1 * T.CreateSaturating(Floats<double>.OneHalf);

                real[count] = delta3;
                imag[count] = delta2;

                real[count + 1] = delta3;
                // Sign is inverted on display
                imag[count + 1] = delta2;
            }
            else
            {
                // Roots are real
                delta2 = T.Sqrt(delta1);

                real[count] = (delta2 - alfa1) * T.CreateSaturating(Floats<double>.OneHalf);
                imag[count] = T.Zero;

                real[count + 1] = (delta2 + alfa1) * -T.CreateSaturating(Floats<double>.OneHalf);
                imag[count + 1] = T.Zero;
            }

            // Update root counter
            count += 2;

            // Reduce polynomial order
            n -= 2;
            n1 -= 2;
            n2 -= 2;

            // For n >= 2 calculate coefficients of
            // The new polynomial
            if (n >= 2)
            {
                for (var i = 1; i <= n1; i++)
                {
                    a_[i] = b_[i];
                }
            }

            if (n < 2)
            {
                break;
            }
        }
        while (true);

        if (n == 1)
        {
            // Obtain last single real root
            real[count] = -b_[2];
            imag[count] = T.Zero;
        }

        return real;
    }
    #endregion Root Finding

    /// <summary>
    /// Newton's (Newton-Raphson) method for finding Real roots on univariate function. <br/>
    /// When using bounds, algorithm falls back to secant if newton goes out of range.
    /// Bisection is fall-back for secant when determined secant is not efficient enough.
    /// </summary>
    /// <param name="x0">Initial root guess</param>
    /// <param name="f">Function which root we are trying to find</param>
    /// <param name="df">Derivative of function f</param>
    /// <param name="maxIterations">Maximum number of algorithm iterations</param>
    /// <param name="min">Left bound value</param>
    /// <param name="max">Right bound value</param>
    /// <returns>
    /// root
    /// </returns>
    /// <remarks>
    /// <para>https://github.com/thelonious/kld-polynomial
    /// http://en.wikipedia.org/wiki/Newton%27s_method
    /// http://en.wikipedia.org/wiki/Secant_method
    /// http://en.wikipedia.org/wiki/Bisection_method</para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T NewtonSecantBisection<T>(T x0, Func<T, T> f, Func<T, T> df, int maxIterations, T? min = default, T? max = default)
        where T : IFloatingPointIeee754<T>
    {
        ArgumentNullException.ThrowIfNull(f);

        var prev_dfx = T.Zero;
        var prev_x_ef_correction = T.Zero;
        var y_atmin = T.Zero;
        var y_atmax = T.Zero;
        var x = x0;
        T ACCURACY = T.CreateTruncating(14);
        var min_correction_factor = T.Pow(T.CreateTruncating(10), -ACCURACY);
        var isBounded = min is not null && max is not null;
        if (isBounded)
        {
            if (min > max)
            {
                throw new Exception("newton root finding: min must be greater than max");
            }

            y_atmin = f(min);
            y_atmax = f(max);
            if (T.Sign(y_atmin) == T.Sign(y_atmax))
            {
                throw new Exception("newton root finding: y values of bounds must be of opposite sign");
            }
        }

        T x_correction;
        bool isEnoughCorrection()
        {
            // stop if correction is too small
            // or if correction is in simple loop
            return (T.Abs(x_correction) <= min_correction_factor * T.Abs(x))
                || (prev_x_ef_correction == x - x_correction - x);
        }

        //var stepMethod;
        //var details = [];
        for (var i = 0; i < maxIterations; i++)
        {
            var dfx = df(x);
            if (dfx == T.Zero)
            {
                if (prev_dfx == T.Zero)
                {
                    // error
                    throw new Exception("newton root finding: df(x) is zero");
                    //return null;
                }
                else
                {
                    // use previous derivation value
                    dfx = prev_dfx;
                }
                // or move x a little?
                // dfx = df(x != 0 ? x + x * 1e-15 : 1e-15);
            }
            //stepMethod = 'newton';
            prev_dfx = dfx;
            var y = f(x);
            x_correction = y / dfx;
            var x_new = x - x_correction;
            if (isEnoughCorrection())
            {
                break;
            }

            if (isBounded)
            {
                if (T.Sign(y) == T.Sign(y_atmax))
                {
                    max = x;
                    y_atmax = y;
                }
                else if (T.Sign(y) == T.Sign(y_atmin))
                {
                    min = x;
                    y_atmin = y;
                }
                else
                {
                    x = x_new;
                    //console.log("newton root finding: sign(y) not matched.");
                    break;
                }

                if ((x_new < min) || (x_new > max))
                {
                    if (T.Sign(y_atmin) == T.Sign(y_atmax))
                    {
                        break;
                    }

                    T RATIO_LIMIT = T.CreateTruncating(50);
                    T AIMED_BISECT_OFFSET = T.CreateTruncating(0.25); // [0, 0.5)
                    var dy = y_atmax - y_atmin;
                    var dx = max - min;

                    x_correction = dy == T.Zero ? x - (min + (dx * T.CreateSaturating(0.5))) : T.Abs(dy / T.Min(y_atmin, y_atmax)) > RATIO_LIMIT ? x - (min + (dx * (T.CreateSaturating(0.5) + (T.Abs(y_atmin) < T.Abs(y_atmax) ? -AIMED_BISECT_OFFSET : AIMED_BISECT_OFFSET)))) : x - (min - (y_atmin / dy * dx));
                    x_new = x - x_correction;

                    if (isEnoughCorrection())
                    {
                        break;
                    }
                }
            }
            //details.push([stepMethod, i, x, x_new, x_correction, min, max, y]);
            prev_x_ef_correction = x - x_new;
            x = x_new;
        }
        //details.push([stepMethod, i, x, x_new, x_correction, min, max, y]);
        //console.log(details.join('\r\n'));
        //if (i == max_iterations)
        //    console.log('newt: steps=' + ((i==max_iterations)? i:(i + 1)));
        return x;
    }
}
