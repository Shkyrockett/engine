// <copyright file="Maths.Algebraic.cs" company="Shkyrockett" >
//    Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
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
using static System.Math;

namespace Engine
{
    /// <summary>
    /// Extended Math processing library.
    /// </summary>
    public static partial class Maths
    {
        /// <summary>
        /// The inverse sqrt.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseSqrt(double number)
            => 1 / Sqrt(number);

        /// <summary>
        /// Returns the specified root a specified number.
        /// </summary>
        /// <param name="x">A double-precision floating-point number to find the specified root of.</param>
        /// <param name="y">A double-precision floating-point number that specifies a root.</param>
        /// <returns>The y root of the number x.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Root(double x, double y)
            => (x < 0 && Math.Abs(y % 2 - 1) < Epsilon) ? -Pow(-x, (1d / y)) : Pow(x, (1d / y));

        /// <summary>
        /// Cube root equivalent of the sqrt function. (note that there are actually
        /// three roots: one real, two complex, and we don't care about the latter):
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/26823024/cubic-bezier-reverse-getpoint-equation-float-for-vector-vector-for-float?answertab=active#tab-top
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Crt(double value)
            => value < 0
            ? -Pow(-value, OneThird)
            : Pow(value, OneThird);

        /// <summary>
        /// The inverse cube root.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>The <see cref="double"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseCrt(double number)
            => 1 / Crt(number);

        /// <summary>
        /// The quadratic equation.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <returns>The <see cref="(double A, double S)"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double S) QuadraticEquation(double a, double b, double c)
            => (
            (-b + Sqrt(b * b - (4 * a * c))) / (2 * a),
            (-b - Sqrt(b * b - (4 * a * c))) / (2 * a));

        #region Root Finding

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/geometry/2D/intersections/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<double> LinearRoots(double a, double b, double epsilon = Epsilon)
        {
            var result = new HashSet<double>();
            if (!(Math.Abs(a) <= epsilon))
                result.Add(-b / a);
            return result.ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/geometry/2D/intersections/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<double> QuadraticRoots(double a, double b, double c, double epsilon = Epsilon)
        {
            var b_ = b / a;
            var c_ = c / a;

            // Polynomial discriminant
            var discriminant = b_ * b_ - 4d * c_;

            // ToDo: May need to switch from a hash set to a list for scanbeams.
            var results = new HashSet<double>();

            if (Math.Abs(discriminant) <= epsilon)
                discriminant = 0;

            if (discriminant > 0)
            {
                // Complex or duplicate roots
                var e = Sqrt(discriminant);
                results.Add(OneHalf * (-b_ + e));
                results.Add(OneHalf * (-b_ - e));
            }
            else if (discriminant == 0)
            {
                // really two roots with same value, but we only return one
                results.Add(OneHalf * -b_);
            }

            return results.ToList();
        }

        /// <summary>
        /// Cubic Roots
        /// </summary>
        /// <param name="a">t^3</param>
        /// <param name="b">t^2</param>
        /// <param name="c">t</param>
        /// <param name="d">1</param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// based on http://abecedarical.com/javascript/script_exact_cubic.html
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<double> CubicRoots(double a, double b, double c, double d, double epsilon = Epsilon)
        {
            var A = b / a;
            var B = c / a;
            var C = d / a;

            var Q = ((3d * B) - (A * A)) / 9d;
            var R = (-(2d * A * A * A) + (9d * A * B) - (27d * C)) / 54d;

            var offset = A * OneThird;

            // Polynomial discriminant
            var discriminant = R * R + Q * Q * Q;

            // ToDo: May need to switch from a hash set to a list for scanbeams.
            var results = new HashSet<double>();

            if (Math.Abs(discriminant) <= epsilon)
                discriminant = 0d;

            if (discriminant == 0d)
            {
                var t = Sign(R) * Pow(Math.Abs(R), OneThird);

                // Real root.
                results.Add(-offset + (t + t));

                // Real part of complex root.
                results.Add(-offset - (t + t) * OneHalf);
            }
            if (discriminant > 0)
            {
                var s = Sign(R + Sqrt(discriminant)) * Pow(Math.Abs(R + Sqrt(discriminant)), OneThird);
                var t = Sign(R - Sqrt(discriminant)) * Pow(Math.Abs(R - Sqrt(discriminant)), OneThird);

                // Real root.
                results.Add(-offset + (s + t));

                // Complex part of root pair.
                var Im = Math.Abs(Sqrt(3) * (s - t) * OneHalf);
                if (Im == 0d)
                {
                    // Real part of complex root.
                    results.Add(-offset - (s + t) * OneHalf);
                }
            }
            else if (discriminant < 0)
            {
                // Distinct real roots.
                var th = Acos(R / Sqrt(-Q * Q * Q));

                results.Add(2 * Sqrt(-Q) * Cos(th * OneThird) - offset);
                results.Add(2 * Sqrt(-Q) * Cos((th + Tau) * OneThird) - offset);
                results.Add(2 * Sqrt(-Q) * Cos((th + 4 * PI) * OneThird) - offset);
            }

            return results.ToList();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// ToDo: Translate code found at: http://abecedarical.com/javascript/script_quintic.html and http://jwezorek.com/2015/01/my-code-for-doing-two-things-that-sooner-or-later-you-will-want-to-do-with-bezier-curves/:
        /// This method computes complex and real roots for any quintic polynomial.
        /// It applies the Lin-Bairstow algorithm which iteratively solves for the
        /// roots starting from random guesses for a solution.
        /// The calculator is designed to solve for the roots of a quintic polynomial
        /// with the form: x⁵ + ax⁴ + bx³ + cx² + dx + e = 0
        /// ⁰¹²³⁴⁵⁶⁷⁸⁹
        /// </remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/geometry/2D/intersections/
        /// http://abecedarical.com/javascript/script_quartic.html
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<double> QuarticRoots(double a, double b, double c, double d, double e, double epsilon = Epsilon)
        {
            // ToDo: Translate code found at: http://abecedarical.com/javascript/script_quintic.html
            // and http://jwezorek.com/2015/01/my-code-for-doing-two-things-that-sooner-or-later-you-will-want-to-do-with-bezier-curves/

            var A = b / a;
            var B = c / a;
            var C = d / a;
            var D = e / a;

            var resolveRoots = CubicRoots(
                1,
                -B,
                A * C - 4d * D,
                -A * A * D + 4 * B * D - C * C,
                epsilon);
            var y = resolveRoots[0];
            var discriminant = A * A * OneQuarter - B + y;

            // ToDo: May need to switch from a hash set to a list for scanbeams.
            var results = new HashSet<double>();

            if (Math.Abs(discriminant) <= epsilon)
                discriminant = 0d;
            if (discriminant > 0)
            {
                var ee = Sqrt(discriminant);
                var t1 = 3 * A * A * OneQuarter - ee * ee - 2 * B;
                var t2 = (4 * A * B - 8 * C - A * A * A) / (4 * ee);
                var plus = t1 + t2;
                var minus = t1 - t2;
                if (Math.Abs(plus) <= epsilon)
                    plus = 0;
                if (Math.Abs(minus) <= epsilon)
                    minus = 0;
                if (plus >= 0)
                {
                    var f = Sqrt(plus);
                    results.Add(-A * OneQuarter + (ee + f) * OneHalf);
                    results.Add(-A * OneQuarter + (ee - f) * OneHalf);
                }
                if (minus >= 0)
                {
                    var f = Sqrt(minus);
                    results.Add(-A * OneQuarter + (f - ee) * OneHalf);
                    results.Add(-A * OneQuarter - (f + ee) * OneHalf);
                }
            }
            else if (discriminant < 0)
            {
            }
            else
            {
                var t2 = y * y - 4 * D;
                if (t2 >= -epsilon)
                {
                    if (t2 < 0) t2 = 0;
                    t2 = 2d * Sqrt(t2);
                    var t1 = 3 * A * A * OneQuarter - 2d * B;
                    if (t1 + t2 >= epsilon)
                    {
                        var d0 = Sqrt(t1 + t2);
                        results.Add(-A * OneQuarter + d0 * OneHalf);
                        results.Add(-A * OneQuarter - d0 * OneHalf);
                    }
                    if (t1 - t2 >= epsilon)
                    {
                        var d1 = Sqrt(t1 - t2);
                        results.Add(-A * OneQuarter + d1 * OneHalf);
                        results.Add(-A * OneQuarter - d1 * OneHalf);
                    }
                }
            }

            return results.ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// This is a Copy and paste port of the method found at:
        /// https://web.archive.org/web/20150504111126/http://abecedarical.com/javascript/script_quintic.html
        /// There has been little attempt to fix it up and get it working correctly.
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<double> QuinticRoots(double a, double b, double c, double d, double e, double f, double epsilon = Epsilon)
        {
            var A = b / a;
            var B = c / a;
            var C = d / a;
            var D = e / a;
            var E = f / a;

            var coeff = new List<double> { a, b, c, d, e, f };

            var beta2 = 0d;
            var delta1 = 0d;
            var delta2 = 0d;
            var delta3 = 0d;

            var i = 0;
            var j = 0;
            var k = 0;

            var n = 4;// 5;       // order
            var n1 = 5;// 6;
            var n2 = 6;// 7;

            var a_ = new List<double> { 0d, 0d, 0d, 0d, 0d, 0d };
            var b_ = new List<double> { 0d, 0d, 0d, 0d, 0d, 0d };
            var c_ = new List<double> { 0d, 0d, 0d, 0d, 0d, 0d };
            var d_ = new List<double> { 0d, 0d, 0d, 0d, 0d, 0d };
            var real = new List<double> { 0d, 0d, 0d, 0d, 0d, 0d };
            var imag = new List<double> { 0d, 0d, 0d, 0d, 0d, 0d };

            // is the coefficient of the highest term zero?
            if (Math.Abs(coeff[0]) < epsilon)
                return new List<double>();

            for (i = 0; i <= n; i++)      //  copy into working array
                a_[(a_.Count - 1) - i] = coeff[i];

            var count = 0;             // initialize root counter

            // start the main Lin-Bairstow iteration loop
            do
            {
                // initialize the counter and guesses for the
                // coefficients of quadratic factor:
                //
                // p(x) = x^2 + alfa1*x + beta1
                var alfa1 = Random(0.5, 1);
                var alfa2 = 0d;
                var beta1 = Random(0.5, 1);
                var limit = 1000;

                do
                {
                    b_[0] = 0;
                    d_[0] = 0;
                    b_[1] = 1;
                    d_[1] = 1;

                    j = 1;
                    k = 0;

                    for (i = 2; i < a_.Count; i++)
                    {
                        b_[i] = a_[i] - alfa1 * b_[j] - beta1 * b_[k];
                        d_[i] = b_[i] - alfa1 * d_[j] - beta1 * d_[k];
                        j = j + 1;
                        k = k + 1;
                    }

                    j = n - 1;
                    k = n - 2;
                    delta1 = d_[j] * d_[j] - (d_[n] - b_[n]) * d_[k];
                    alfa2 = (b_[n] * d_[j] - b_[n1] * d_[k]) / delta1;
                    beta2 = (b_[n1] * d_[j] - (d_[n] - b_[n]) * b_[n]) / delta1;
                    alfa1 = alfa1 + alfa2;
                    beta1 = beta1 + beta2;

                    if (--limit < 0)         // cannot solve
                        return new List<double>();

                    if (Math.Abs(alfa2) < epsilon && Math.Abs(beta2) < epsilon)
                        break;
                }
                while (true);

                delta1 = alfa1 * alfa1 - 4 * beta1;

                if (delta1 < 0)              // imaginary roots
                {
                    delta2 = Sqrt(Math.Abs(delta1)) / 2;
                    delta3 = -alfa1 / 2;

                    real[count] = delta3;
                    imag[count] = delta2;

                    real[count + 1] = delta3;
                    imag[count + 1] = delta2;  // sign is inverted on display
                }
                else                          // roots are real
                {
                    delta2 = Sqrt(delta1);

                    real[count] = (delta2 - alfa1) / 2;
                    imag[count] = 0;

                    real[count + 1] = (delta2 + alfa1) / (-2);
                    imag[count + 1] = 0;
                }


                count = count + 2;            // update root counter

                n = n - 2;                  // reduce polynomial order
                n1 = n1 - 2;
                n2 = n2 - 2;

                // for n >= 2 calculate coefficients of
                //  the new polynomial
                if (n >= 2)
                {
                    for (i = 1; i <= n1; i++)
                        a_[i] = b_[i];
                }

                if (n < 2) break;
            }
            while (true);

            if (n == 1)
            {
                // obtain last single real root
                real[count] = -b_[2];
                imag[count] = 0;
            }

            return real;
        }

        #endregion

        #region Bezier Coefficients Overloads

        /// <summary>
        /// Coefficients for a Linear Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) BezierCoefficients(double a, double b)
            => LinearBezierCoefficients(a, b);

        /// <summary>
        /// Coefficients for a Quadratic Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) BezierCoefficients(double a, double b, double c)
            => QuadraticBezierCoefficients(a, b, c);

        /// <summary>
        /// Coefficients for a Cubic Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) BezierCoefficients(double a, double b, double c, double d)
            => CubicBezierCoefficients(a, b, c, d);

        /// <summary>
        /// Coefficients for a Quartic Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E) BezierCoefficients0(double a, double b, double c, double d, double e)
            => QuarticBezierCoefficients(a, b, c, d, e);

        /// <summary>
        /// Coefficients for a Quintic Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F) BezierCoefficients(double a, double b, double c, double d, double e, double f)
            => QuinticBezierCoefficients(a, b, c, d, e, f);

        #endregion

        #region Bezier Coefficients

        /// <summary>
        /// Coefficients for a Linear Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial LinearBezierCoefficientsStack(double a, double b)
            => Polynomial.OneMinusT * a + Polynomial.T * b;

        /// <summary>
        /// Coefficients for a Linear Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://fontforge.github.io/bezier.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) LinearBezierCoefficients(double a, double b)
            => (b - a,
                a);

        /// <summary>
        /// Coefficients for a Quadratic Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial QuadraticBezierCoefficientsStack(double a, double b, double c)
            => Polynomial.OneMinusT * LinearBezierCoefficientsStack(a, b) + Polynomial.T * LinearBezierCoefficientsStack(b, c);

        /// <summary>
        /// Coefficients for a Quadratic Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <acknowledgment>
        /// http://fontforge.github.io/bezier.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) QuadraticBezierCoefficients(double a, double b, double c)
            => (c - (2d * b) + a,
                2d * (b - a),
                a);

        /// <summary>
        /// Coefficients for a Cubic Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial CubicBezierCoefficientsStack(double a, double b, double c, double d)
            => (Polynomial.OneMinusT * QuadraticBezierCoefficientsStack(a, b, c) + Polynomial.T * QuadraticBezierCoefficientsStack(b, c, d));

        /// <summary>
        /// Coefficients for a Cubic Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.gamedev.net/topic/643117-coefficients-for-bezier-curves/
        /// http://fontforge.github.io/bezier.html
        /// http://idav.ucdavis.edu/education/CAGDNotes/Matrix-Cubic-Bezier-Curve/Matrix-Cubic-Bezier-Curve.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) CubicBezierCoefficients(double a, double b, double c, double d)
            => (d - (3d * c) + (3d * b) - a,
                (3d * c) - (6d * b) + (3d * a),
                3d * (b - a),
                a);

        /// <summary>
        /// Coefficients for a Quartic Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial QuarticBezierCoefficientsStack(double a, double b, double c, double d, double e)
            => (Polynomial.OneMinusT * CubicBezierCoefficientsStack(a, b, c, d) + Polynomial.T * CubicBezierCoefficientsStack(b, c, d, e));

        /// <summary>
        /// Coefficients for a Quartic Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Coefficient calculation found in the matrix representation at:
        /// http://www.dglr.de/publikationen/2016/420062.pdf
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E) QuarticBezierCoefficients(double a, double b, double c, double d, double e)
            => (e - (4d * d) + (6d * c) - (4d * b) + a,
                (4d * d) - (12d * c) + (12d * b) - (4d * a),
                (6d * c) - (12d * b) + (6d * a),
                4d * (b - a),
                a);

        /// <summary>
        /// Coefficients for a Quintic Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial QuinticBezierCoefficientsStack(double a, double b, double c, double d, double e, double f)
            => (Polynomial.OneMinusT * QuarticBezierCoefficientsStack(a, b, c, d, e) + Polynomial.T * QuarticBezierCoefficientsStack(b, c, d, e, f));

        /// <summary>
        /// Coefficients for a Quintic Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Based off of pseudocode for the matrix found at:
        /// https://simtk.org/api_docs/opensim/api_docs/classOpenSim_1_1SegmentedQuinticBezierToolkit.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F) QuinticBezierCoefficients(double a, double b, double c, double d, double e, double f)
            => (f - (5d * e) + (10d * d) - (10d * c) + (5d * b) - a,
                (5d * e) - (20d * d) + (30d * c) - (20d * b) + (5 * a),
                (10d * d) - (30d * c) + (30d * b) - (10d * a),
                (10d * c) - (20d * b) + (10d * a),
                5d * (b - a),
                a);

        /// <summary>
        /// Coefficients for a Sextic Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial SexticBezierCoefficientsStack(double a, double b, double c, double d, double e, double f, double g)
            => (Polynomial.OneMinusT * QuinticBezierCoefficientsStack(a, b, c, d, e, f) + Polynomial.T * QuinticBezierCoefficientsStack(b, c, d, e, f, g));

        /// <summary>
        /// Coefficients for a Septic Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial SepticBezierCoefficientsStack(double a, double b, double c, double d, double e, double f, double g, double h)
            => (Polynomial.OneMinusT * SexticBezierCoefficientsStack(a, b, c, d, e, f, g) + Polynomial.T * SexticBezierCoefficientsStack(b, c, d, e, f, g, h));

        /// <summary>
        /// Coefficients for a Octic Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <param name="h"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial OcticBezierCoefficientsStack(double a, double b, double c, double d, double e, double f, double g, double h, double i)
            => (Polynomial.OneMinusT * SepticBezierCoefficientsStack(a, b, c, d, e, f, g, h) + Polynomial.T * SepticBezierCoefficientsStack(b, c, d, e, f, g, h, i));

        /// <summary>
        /// Coefficients for a Nonic Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <param name="h"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial NonicBezierCoefficientsStack(double a, double b, double c, double d, double e, double f, double g, double h, double i, double j)
            => (Polynomial.OneMinusT * OcticBezierCoefficientsStack(a, b, c, d, e, f, g, h, i) + Polynomial.T * OcticBezierCoefficientsStack(b, c, d, e, f, g, h, i, j));

        /// <summary>
        /// Coefficients for a Decic Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <param name="h"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial DecicBezierCoefficientsStack(double a, double b, double c, double d, double e, double f, double g, double h, double i, double j, double k)
            => (Polynomial.OneMinusT * NonicBezierCoefficientsStack(a, b, c, d, e, f, g, h, i, j) + Polynomial.T * NonicBezierCoefficientsStack(b, c, d, e, f, g, h, i, j, k));

        #endregion

        /// <summary>
        /// Newton's (Newton-Raphson) method for finding Real roots on univariate function. <br/>
        /// When using bounds, algorithm falls back to secant if newton goes out of range.
        /// Bisection is fall-back for secant when determined secant is not efficient enough.
        /// </summary>
        /// <param name="x0">Initial root guess</param>
        /// <param name="f">Function which root we are trying to find</param>
        /// <param name="df">Derivative of function f</param>
        /// <param name="max_iterations">Maximum number of algorithm iterations</param>
        /// <param name="min">Left bound value</param>
        /// <param name="max">Right bound value</param>
        /// <returns>root</returns>
        /// <remarks>
        /// https://github.com/thelonious/kld-polynomial
        /// http://en.wikipedia.org/wiki/Newton%27s_method
        /// http://en.wikipedia.org/wiki/Secant_method
        /// http://en.wikipedia.org/wiki/Bisection_method
        /// </remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Newton_secant_bisection(double x0, Func<double, double> f, Func<double, double> df, int max_iterations, double? min = null, double? max = null)
        {
            var prev_dfx = 0d;
            var dfx = 0d;
            var prev_x_ef_correction = 0d;
            var x_correction = 0d;
            var y_atmin = 0d;
            var y_atmax = 0d;
            var x = x0;
            const int ACCURACY = 14;
            var min_correction_factor = Pow(10, -ACCURACY);
            var isBounded = (min != null && max != null);
            if (isBounded)
            {
                if (min > max)
                    throw new Exception("newton root finding: min must be greater than max");
                y_atmin = f(min.Value);
                y_atmax = f(max.Value);
                if (Sign(y_atmin) == Sign(y_atmax))
                    throw new Exception("newton root finding: y values of bounds must be of opposite sign");
            }

            bool isEnoughCorrection()
            {
                // stop if correction is too small
                // or if correction is in simple loop
                return (Math.Abs(x_correction) <= min_correction_factor * Math.Abs(x))
                    || (prev_x_ef_correction == (x - x_correction) - x);
            };

            var i = 0;
            //var stepMethod;
            //var details = [];
            for (i = 0; i < max_iterations; i++)
            {
                dfx = df(x);
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
                    //dfx = df(x != 0 ? x + x * 1e-15 : 1e-15);
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

                        x_correction = dy == 0 ? x - (min.Value + dx.Value * 0.5) : Math.Abs(dy / Min(y_atmin, y_atmax)) > RATIO_LIMIT ? x - (min.Value + dx.Value * (0.5 + (Math.Abs(y_atmin) < Math.Abs(y_atmax) ? -AIMED_BISECT_OFFSET : AIMED_BISECT_OFFSET))) : x - (min.Value - y_atmin / dy * dx.Value);
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
