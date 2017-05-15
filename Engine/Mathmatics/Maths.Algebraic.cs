// <copyright file="Maths.Algebraic.cs" company="Shkyrockett" >
//    Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//    Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

// <copyright company="kevlindev" >
//     Many of the Roots methods were adapted from Kevin Lindsey's site http://www.kevlindev.com/gui/math/intersection/. 
//     Copyright (c) 2000 - 2003 Kevin Lindsey. All rights reserved.
// </copyright>
// <author id="thelonious">Kevin Lindsey</author>
// <license>
//     Licensed under the BSD-3-Clause https://github.com/thelonious/kld-intersections/blob/development/LICENSE
// </license>

using System;
using System.Collections.Generic;
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
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/geometry/2D/intersections/ </remarks>
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
        /// <remarks> http://www.kevlindev.com/geometry/2D/intersections/ </remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<double> QuadraticRoots(double a, double b, double c, double epsilon = Epsilon)
        {
            var A = b / a;
            var B = c / a;

            // Polynomial discriminant
            var discriminant = A * A - 4d * B;

            // ToDo: May need to switch from a hash set to a list for scanbeams.
            var results = new HashSet<double>();

            if (Math.Abs(discriminant) <= epsilon)
                discriminant = 0;

            if (discriminant > 0)
            {
                // Complex or duplicate roots
                var e = Sqrt(discriminant);
                results.Add(OneHalf * (-A + e));
                results.Add(OneHalf * (-A - e));
            }
            else if (discriminant == 0)
            {
                // really two roots with same value, but we only return one
                results.Add(OneHalf * -A);
            }

            return results.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a">t^3</param>
        /// <param name="b">t^2</param>
        /// <param name="c">t</param>
        /// <param name="d">1</param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks>
        /// based on http://abecedarical.com/javascript/script_exact_cubic.html
        /// </remarks>
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
        /// ToDo: Translate code found at: http://abecedarical.com/javascript/script_quintic.html and http://jwezorek.com/2015/01/my-code-for-doing-two-things-that-sooner-or-later-you-will-want-to-do-with-bezier-curves/:
        /// This method computes complex and real roots for any quintic polynomial.
        /// It applies the Lin-Bairstow algorithm which iteratively solves for the 
        /// roots starting from random guesses for a solution. 
        /// The calculator is designed to solve for the roots of a quintic polynomial
        /// with the form: x⁵ + ax⁴ + bx³ + cx² + dx + e = 0
        /// ⁰¹²³⁴⁵⁶⁷⁸⁹
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://www.kevlindev.com/geometry/2D/intersections/
        /// http://abecedarical.com/javascript/script_quartic.html
        /// </remarks>
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

            var resolveRoots = new Polynomial(1, -B, A * C - 4d * D, -A * A * D + 4 * B * D - C * C).CubicRoots();
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
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks>
        /// This is a Copy and paste port of the method found at:
        /// http://abecedarical.com/javascript/script_quintic.html
        /// There has been little attempt to fix it up and get it working correctly.
        /// </remarks>
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

            //var LIMIT = 50d;
            var beta2 = 0d;
            var delta1 = 0d;
            var delta2 = 0d;
            var delta3 = 0d;

            var i = 0;
            var j = 0;
            var k = 0;

            var n = 5;       // order
            var n1 = 6;
            var n2 = 7;

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
                a_[n1 - i] = coeff[i];

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

                    for (i = 2; i <= n1; i++)
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

        /// <summary>
        /// Coefficients for a Linear Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://fontforge.github.io/bezier.html
        /// </remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) BezierCoefficients(double a, double b)
            => (b - a,
                a);

        /// <summary>
        /// Coefficients for a Quadratic Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://fontforge.github.io/bezier.html
        /// </remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) BezierCoefficients(double a, double b, double c)
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
        /// <remarks>
        /// http://www.gamedev.net/topic/643117-coefficients-for-bezier-curves/
        /// http://fontforge.github.io/bezier.html
        /// </remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) BezierCoefficients(double a, double b, double c, double d)
            => (d - (3d * c) + (3d * b) - a,
                (3d * c) - (6d * b) + (3d * a),
                3d * (b - a),
                a);
    }
}
