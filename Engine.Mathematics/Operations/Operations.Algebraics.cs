// <copyright file="Operations.Algebraics.cs" company="Shkyrockett" >
//    Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//    Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

// <copyright company="kevlindev" >
//     Many of the Roots methods were adapted from Kevin Lindsey's site http://www.kevlindev.com/gui/math/intersection/.
//     Copyright © 2000 - 2003 Kevin Lindsey. All rights reserved.
// </copyright>
// <author id="thelonious">Kevin Lindsey</author>
// <license>
//     Licensed under the BSD-3-Clause https://github.com/thelonious/kld-intersections/blob/development/LICENSE
// </license>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using static Engine.Mathematics;
using static System.Math;

namespace Engine
{
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseSqrt(double number) => 1d / Sqrt(number);

        /// <summary>
        /// Returns the specified root a specified number.
        /// </summary>
        /// <param name="x">A double-precision floating-point number to find the specified root of.</param>
        /// <param name="y">A double-precision floating-point number that specifies a root.</param>
        /// <returns>
        /// The y root of the number x.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Root(double x, double y) => (x < 0d && Math.Abs((y % 2d) - 1d) < double.Epsilon) ? -Pow(-x, 1d / y) : Pow(x, 1d / y);

        /// <summary>
        /// Cube root equivalent of the sqrt function. (note that there are actually
        /// three roots: one real, two complex, and we don't care about the latter):
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/26823024/cubic-bezier-reverse-getpoint-equation-float-for-vector-vector-for-float?answertab=active#tab-top
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CubeRoot(double value) => value < 0d ? -Cbrt(-value) : Cbrt(value);

        /// <summary>
        /// The inverse cube root.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseCubeRoot(double number) => 1d / CubeRoot(number);

        /// <summary>
        /// Calculates the real order or degree of the polynomial.
        /// </summary>
        /// <param name="coefficients">The coefficients.</param>
        /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
        /// <returns>
        /// Returns a <see cref="PolynomialDegree" /> value representing the order of degree of the polynomial.
        /// </returns>
        /// <remarks>
        /// Primarily used to locate where to trim off any leading zero coefficients of the internal coefficients array.
        /// </remarks>
        /// <acknowledgment>
        /// A hodge-podge helper method based on Simplify from of: http://www.kevlindev.com/
        /// as well as Trim and RealOrder from: https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PolynomialDegree DegreeRealOrder(Span<double> coefficients, double epsilon = double.Epsilon)
        {
            var pos = 1;
            var count = coefficients.Length;

            // Monomial can be a zero constant, skip them and check the rest.
            if (count > 1)
            {
                // Count the number of leading zeros. Because the coefficients array is reversed, start at the end because there should generally be fewer leading zeros than other coefficients.
                for (var i = count - 1; i >= 1 /* Monomials can be 0. */; i--)
                {
                    // Check if coefficient is a leading zero.
                    if (Math.Abs(coefficients[i]) <= epsilon)
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
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<double> DRoots(IList<double> coefficients, double epsilon = double.Epsilon)
            // ToDo: What are DRoots?
            => coefficients?.Count switch
            {
                2 => LinearDRoots(coefficients[0], coefficients[1], epsilon),
                3 => QuadraticDRoots(coefficients[0], coefficients[1], coefficients[2], epsilon),
                _ => Array.Empty<double>(),
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<double> LinearDRoots(double a, double b, double epsilon = double.Epsilon)
        {
            // ToDo: What are DRoots?
            _ = epsilon;
            return a != b ? (new double[] { a / (a - b) }) : Array.Empty<double>();
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<double> QuadraticDRoots(double a, double b, double c, double epsilon = double.Epsilon)
        {
            // ToDo: What are DRoots?
            _ = epsilon;
            var det = a - (2d * b) + c;
            if (det != 0)
            {
                // Negative square root discriminant. Missing the 4?
                var sqrtd = -Sqrt((b * b) - (a * c));
                var m2 = b - a;
                var v1 = -(m2 + sqrtd) / det;
                var v2 = -(m2 - sqrtd) / det;
                return new double[] { v1, v2 };
            }
            else if (b != c && det == 0)
            {
                return new double[] { ((2d * b) - c) / (2d * (b - c)) };
            }

            return Array.Empty<double>();
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<double> Roots(Span<double> coefficients, double epsilon = double.Epsilon)
            => DegreeRealOrder(coefficients) switch
            {
                PolynomialDegree.Constant => new double[] { coefficients[0] },
                PolynomialDegree.Linear => LinearRoots(ref coefficients[1], ref coefficients[0], epsilon),
                PolynomialDegree.Quadratic => QuadraticRoots(ref coefficients[2], ref coefficients[1], ref coefficients[0], epsilon),
                PolynomialDegree.Cubic => CubicRoots(ref coefficients[3], ref coefficients[2], ref coefficients[1], ref coefficients[0], epsilon),
                PolynomialDegree.Quartic => QuarticRoots(ref coefficients[4], ref coefficients[3], ref coefficients[2], ref coefficients[1], ref coefficients[0], epsilon),
                PolynomialDegree.Quintic => QuinticRoots(ref coefficients[5], ref coefficients[4], ref coefficients[3], ref coefficients[2], ref coefficients[1], ref coefficients[0], epsilon),
                //PolynomialDegree.Sextic => Array.Empty<double>(), //SexticRoots(epsilon),
                //PolynomialDegree.Septic => Array.Empty<double>(), //SepticRoots(epsilon),
                //PolynomialDegree.Octic => Array.Empty<double>(), //OcticRoots(epsilon),
                _ => Array.Empty<double>(), // Should try Newton's method and/or bisection
            };

        /// <summary>
        /// The linear roots.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<double> LinearRoots(double a, double b, double epsilon = double.Epsilon) => LinearRoots(ref a, ref b, epsilon);

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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<double> LinearRoots(ref double a, ref double b, double epsilon = double.Epsilon) => Math.Abs(a) < epsilon ? Math.Abs(b) < epsilon ? Array.Empty<double>() : new double[] { b } : new double[] { -b / a };

        /// <summary>
        /// The quadratic roots.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name = "epsilon"> The minimal value to represent a change.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<double> QuadraticRoots(double a, double b, double c, double epsilon = double.Epsilon) => QuadraticRoots(ref a, ref b, ref c, epsilon);

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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<double> QuadraticRoots(ref double a, ref double b, ref double c, double epsilon = double.Epsilon)
        {
            // Is the coefficient of the highest term zero?
            if (Math.Abs(a) < epsilon)
            {
                // If the highest term coefficient is 0, then it is a lower degree polynomial.
                return LinearRoots(ref b, ref c, epsilon);
            }

            var ba = b / a;
            var ca = c / a;

            // Polynomial discriminant
            var discriminant = (ba * ba) - (4d * ca);

            if (Math.Abs(discriminant) <= epsilon)
            {
                discriminant = 0;
            }

            switch (discriminant)
            {
                case 0:
                    return new double[] { OneHalf * -ba };
                case double v when v > 0:
                    {
                        var e = Sqrt(discriminant);
                        return new double[] { OneHalf * (-ba + e), OneHalf * (-ba - e) };
                    }
                default:
                    {
                        var e = -Sqrt(-discriminant);
                        return new double[] { OneHalf * (-ba + e), OneHalf * (-ba - e) };
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<double> CubicRoots(double a, double b, double c, double d, double epsilon = double.Epsilon) => CubicRoots(ref a, ref b, ref c, ref d, epsilon);

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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<double> CubicRoots(ref double a, ref double b, ref double c, ref double d, double epsilon = double.Epsilon)
        {
            // Is the coefficient of the highest term zero?
            if (Math.Abs(a) < epsilon)
            {
                // If the highest term coefficient is 0, then it is a lower degree polynomial.
                return QuadraticRoots(ref b, ref c, ref d, epsilon);
            }

            var ba = b / a;
            var ca = c / a;
            var da = d / a;

            var q = ((3d * ca) - (ba * ba)) / 9d;
            var r = (-(2d * ba * ba * ba) + (9d * ba * ca) - (27d * da)) / 54d;

            var offset = ba * OneThird;

            // Polynomial discriminant
            var discriminant = (r * r) + (q * q * q);

            if (Math.Abs(discriminant) <= epsilon)
            {
                discriminant = 0d;
            }

            switch (discriminant)
            {
                case 0:
                    {
                        var t = Sign(r) * Cbrt(Math.Abs(r));
                        return new double[] {
                         -offset + (t + t),
                          -offset - ((t + t) * OneHalf)
                         };
                    }
                case double v when v > 0:
                    {
                        var e = Sqrt(discriminant);
                        var s = Sign(r + e) * Cbrt(Math.Abs(r + e));
                        var t = Sign(r - e) * Cbrt(Math.Abs(r - e));
                        var im = Math.Abs(Sqrt(3d) * (s - t) * OneHalf);
                        //return im == 0d ?
                        //    new double[] { -offset + (s + t) } 
                        //    : new double[] { -offset + (s + t), -offset - ((s + t) * OneHalf) };
                        // Complex part of root pair.
                        if (im == 0d)
                        {
                            // Real part of complex root.
                            return new double[] {
                            -offset + (s + t),
                            -offset - ((s + t) * OneHalf)
                            };
                        }
                        else
                        {
                            return new double[] { -offset + (s + t) };
                        }
                    }
                default:
                    {
                        var th = Acos(r / Sqrt(-q * q * q));
                        return new double[] {
                            (2d * Sqrt(-q) * Cos(th * OneThird)) - offset,
                            (2d * Sqrt(-q) * Cos((th + Tau) * OneThird)) - offset,
                            (2d * Sqrt(-q) * Cos((th + (4d * PI)) * OneThird)) - offset };
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<double> QuarticRoots(double a, double b, double c, double d, double e, double epsilon = double.Epsilon) => QuarticRoots(ref a, ref b, ref c, ref d, ref e, epsilon);

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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<double> QuarticRoots(ref double a, ref double b, ref double c, ref double d, ref double e, double epsilon = double.Epsilon)
        {
            // Is the coefficient of the highest term zero?
            if (Math.Abs(a) < epsilon)
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
                1d,
                -ca,
                (ba * da) - (4d * ea),
                (-ba * ba * ea) + (4d * ca * ea) - (da * da),
                epsilon);
            var y = resolveRoots[0];
            var discriminant = (ba * ba * OneQuarter) - ca + y;

            // ToDo: May need to switch from a hash set to a list for scan-beams.
            var results = new HashSet<double>();

            if (Math.Abs(discriminant) <= epsilon)
            {
                discriminant = 0d;
            }

            switch (discriminant)
            {
                case 0:
                    {
                        var t2 = (y * y) - (4d * ea);
                        if (t2 >= -epsilon)
                        {
                            if (t2 < 0)
                            {
                                t2 = 0d;
                            }

                            t2 = 2d * Sqrt(t2);
                            var t1 = (3d * ba * ba * OneQuarter) - (2d * ca);
                            if (t1 + t2 >= epsilon)
                            {
                                var d0 = Sqrt(t1 + t2);
                                results.Add((-ba * OneQuarter) + (d0 * OneHalf));
                                results.Add((-ba * OneQuarter) - (d0 * OneHalf));
                            }
                            if (t1 - t2 >= epsilon)
                            {
                                var d1 = Sqrt(t1 - t2);
                                results.Add((-ba * OneQuarter) + (d1 * OneHalf));
                                results.Add((-ba * OneQuarter) - (d1 * OneHalf));
                            }
                        }
                        return results.ToArray();
                    }
                case double v when v > 0:
                    {
                        var ee = Sqrt(discriminant);
                        var t1 = (3d * ba * ba * OneQuarter) - (ee * ee) - (2d * ca);
                        var t2 = ((4d * ba * ca) - (8d * da) - (ba * ba * ba)) / (4d * ee);
                        var plus = t1 + t2;
                        var minus = t1 - t2;

                        if (Math.Abs(plus) <= epsilon)
                        {
                            plus = 0d;
                        }

                        if (Math.Abs(minus) <= epsilon)
                        {
                            minus = 0d;
                        }

                        if (plus >= 0d)
                        {
                            var f = Sqrt(plus);
                            results.Add((-ba * OneQuarter) + ((ee + f) * OneHalf));
                            results.Add((-ba * OneQuarter) + ((ee - f) * OneHalf));
                        }
                        if (minus >= 0d)
                        {
                            var f = Sqrt(minus);
                            results.Add((-ba * OneQuarter) + ((f - ee) * OneHalf));
                            results.Add((-ba * OneQuarter) - ((f + ee) * OneHalf));
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<double> QuinticRoots(double a, double b, double c, double d, double e, double f, double epsilon = double.Epsilon) => QuinticRoots(ref a, ref b, ref c, ref d, ref e, ref f, epsilon);

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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<double> QuinticRoots(ref double a, ref double b, ref double c, ref double d, ref double e, ref double f, double epsilon = double.Epsilon)
        {
            // Is the coefficient of the highest term zero?
            if (Math.Abs(a) < epsilon)
            {
                // If the highest term coefficient is 0, then it is a lower degree polynomial.
                return QuarticRoots(ref b, ref c, ref d, ref e, ref f, epsilon);
            }

            //var coeff = new List<double> { a, b, c, d, e, f };

            // Order
            var n = 4; // 5;
            var n1 = 5; // 6;
            var n2 = 6; // 7;

            var a_ = new List<double> { f, e, d, c, b, a };
            var b_ = new List<double> { 0d, 0d, 0d, 0d, 0d, 0d };
            //var c_ = new List<double> { 0d, 0d, 0d, 0d, 0d, 0d };
            var d_ = new List<double> { 0d, 0d, 0d, 0d, 0d, 0d };
            var real = new List<double> { 0d, 0d, 0d, 0d, 0d, 0d };
            var imag = new List<double> { 0d, 0d, 0d, 0d, 0d, 0d };

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
                var alfa1 = Mathematics.Random(OneHalf, 1d);
                var beta1 = Mathematics.Random(OneHalf, 1d);
                var limit = 1000;

                double delta1;
                do
                {
                    b_[0] = 0d;
                    d_[0] = 0d;
                    b_[1] = 1d;
                    d_[1] = 1d;

                    for (int i = 2, j = 1, k = 0; i < a_.Count; i++)
                    {
                        b_[i] = a_[i] - (alfa1 * b_[j]) - (beta1 * b_[k]);
                        d_[i] = b_[i] - (alfa1 * d_[j]) - (beta1 * d_[k]);
                        j += 1;
                        k += 1;
                    }

                    double alfa2;

                    double beta2;
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
                        return new List<double>();
                    }

                    if (Math.Abs(alfa2) < epsilon && Math.Abs(beta2) < epsilon)
                    {
                        break;
                    }
                }
                while (true);

                delta1 = (alfa1 * alfa1) - (4d * beta1);

                double delta2;
                // Imaginary roots
                if (delta1 < 0)
                {
                    delta2 = Sqrt(Math.Abs(delta1)) * OneHalf;
                    var delta3 = -alfa1 * OneHalf;

                    real[count] = delta3;
                    imag[count] = delta2;

                    real[count + 1] = delta3;
                    // Sign is inverted on display
                    imag[count + 1] = delta2;
                }
                else
                {
                    // Roots are real
                    delta2 = Sqrt(delta1);

                    real[count] = (delta2 - alfa1) * OneHalf;
                    imag[count] = 0;

                    real[count + 1] = (delta2 + alfa1) * -OneHalf;
                    imag[count + 1] = 0;
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
                imag[count] = 0;
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double NewtonSecantBisection(double x0, Func<double, double> f, Func<double, double> df, int maxIterations, double? min = null, double? max = null)
        {
            if (f is null)
            {
                throw new ArgumentNullException(nameof(f));
            }

            var prev_dfx = 0d;
            var prev_x_ef_correction = 0d;
            var y_atmin = 0d;
            var y_atmax = 0d;
            var x = x0;
            const int ACCURACY = 14;
            var min_correction_factor = Pow(10, -ACCURACY);
            var isBounded = min != null && max != null;
            if (isBounded)
            {
                if (min > max)
                {
                    throw new Exception("newton root finding: min must be greater than max");
                }

                y_atmin = f(min.Value);
                y_atmax = f(max.Value);
                if (Sign(y_atmin) == Sign(y_atmax))
                {
                    throw new Exception("newton root finding: y values of bounds must be of opposite sign");
                }
            }

            double x_correction;
            bool isEnoughCorrection()
            {
                // stop if correction is too small
                // or if correction is in simple loop
                return (Math.Abs(x_correction) <= min_correction_factor * Math.Abs(x))
                    || (prev_x_ef_correction == x - x_correction - x);
            }

            //var stepMethod;
            //var details = [];
            for (var i = 0; i < maxIterations; i++)
            {
                var dfx = df(x);
                if (dfx == 0)
                {
                    if (prev_dfx == 0)
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
                    if (Sign(y) == Sign(y_atmax))
                    {
                        max = x;
                        y_atmax = y;
                    }
                    else if (Sign(y) == Sign(y_atmin))
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
                        if (Sign(y_atmin) == Sign(y_atmax))
                        {
                            break;
                        }

                        const int RATIO_LIMIT = 50;
                        const double AIMED_BISECT_OFFSET = 0.25; // [0, 0.5)
                        var dy = y_atmax - y_atmin;
                        var dx = max - min;

                        x_correction = dy == 0 ? x - (min.Value + (dx.Value * 0.5)) : Math.Abs(dy / Math.Min(y_atmin, y_atmax)) > RATIO_LIMIT ? x - (min.Value + (dx.Value * (0.5 + (Math.Abs(y_atmin) < Math.Abs(y_atmax) ? -AIMED_BISECT_OFFSET : AIMED_BISECT_OFFSET)))) : x - (min.Value - (y_atmin / dy * dx.Value));
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
}
