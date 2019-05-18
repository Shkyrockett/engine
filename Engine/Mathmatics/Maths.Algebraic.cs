// <copyright file="Maths.Algebraic.cs" company="Shkyrockett" >
//    Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
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
            => 1d / Sqrt(number);

        /// <summary>
        /// Returns the specified root a specified number.
        /// </summary>
        /// <param name="x">A double-precision floating-point number to find the specified root of.</param>
        /// <param name="y">A double-precision floating-point number that specifies a root.</param>
        /// <returns>The y root of the number x.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Root(double x, double y)
            => (x < 0d && Math.Abs((y % 2d) - 1d) < Epsilon) ? -Pow(-x, 1d / y) : Pow(x, 1d / y);

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
            => 1d / Crt(number);

        /// <summary>
        /// Calculates the real order or degree of the polynomial.
        /// </summary>
        /// <param name="coefficients">The coefficients.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>Returns a <see cref="PolynomialDegree"/> value representing the order of degree of the polynomial.</returns>
        /// <remarks>Primarily used to locate where to trim off any leading zero coefficients of the internal coefficients array.</remarks>
        /// <acknowledgment>
        /// A hodge-podge helper method based on Simplify from of: http://www.kevlindev.com/
        /// as well as Trim and RealOrder from: https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PolynomialDegree DegreeRealOrder(IList<double> coefficients, double epsilon = Epsilon)
        {
            var pos = 1;
            var count = coefficients.Count;

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
            return (PolynomialDegree)(coefficients?.Count - pos ?? 0);
        }

        /// <summary>
        /// Align points to a line.
        /// </summary>
        /// <param name="points">The points to align.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <returns>The <see cref="T:List{Point2D}"/>.</returns>
        /// <acknowledgment>
        /// https://pomax.github.io/bezierinfo/#aligning
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<Point2D> AlignPoints(IList<Point2D> points, double x1, double y1, double x2, double y2)
        {
            //var angle = -Atan2(y2 - y1, x2 - x1);
            //var sinA = Sin(angle);
            //var cosA = Cos(angle);

            // Atan2, Sin and Cos are kind of slow. In theory this should be faster.
            var dx = x2 - x1;
            var dy = y2 - y1;
            var det = (dx * dx) + (dy * dy);
            // I believe det should only be 0 if the line is a point.
            var sinA = det == 0 ? 0 : -dy / det;
            var cosA = det == 0 ? 1 : -dx / det;

            var results = new List<Point2D>();

            foreach (var point in points)
            {
                results.Add(new Point2D(
                    ((point.X - x1) * cosA) - ((point.Y - y1) * sinA),
                    ((point.X - x1) * sinA) + ((point.Y - y1) * cosA))
                    );
            }

            return results;
        }

        #region D Root Finding
        /// <summary>
        /// The d roots.
        /// </summary>
        /// <param name="coefficients">The coefficients.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>The <see cref="T:double[]"/>.</returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<double> DRoots(IList<double> coefficients, double epsilon = Epsilon)
        {
            // ToDo: What are DRoots?
            switch (coefficients.Count)
            {
                case 2:
                    return LinearDRoots(coefficients[0], coefficients[1], epsilon);
                case 3:
                    return QuadraticDRoots(coefficients[0], coefficients[1], coefficients[2], epsilon);
                default:
                    return new double[] { };
            }
        }

        /// <summary>
        /// The linear d roots.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>The <see cref="T:IList{double}"/>.</returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<double> LinearDRoots(double a, double b, double epsilon = Epsilon)
        {
            // ToDo: What are DRoots?
            _ = epsilon;
            return a != b ? (new double[] { a / (a - b) }) : (new double[] { });
        }

        /// <summary>
        /// The quadratic d roots.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>The <see cref="T:IList{double}"/>.</returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<double> QuadraticDRoots(double a, double b, double c, double epsilon = Epsilon)
        {
            // ToDo: What are DRoots?
            _ = epsilon;
            var det = a - (2 * b) + c;
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
                return new double[] { ((2 * b) - c) / (2 * (b - c)) };
            }

            return new double[] { };
        }
        #endregion D Root Finding

        #region Root Finding
        /// <summary>
        /// Find the Roots of up to Quintic degree <see cref="Polynomial"/>s.
        /// </summary>
        /// <param name="coefficients">The coefficients.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>The <see cref="T:double[]"/>.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/geometry/2D/intersections/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<double> Roots(IList<double> coefficients, double epsilon = Epsilon)
        {
            switch (DegreeRealOrder(coefficients))
            {
                case PolynomialDegree.Constant:
                    if (coefficients is null)
                    {
                        return Array.Empty<double>();
                    }

                    return new double[] { coefficients[0] };
                case PolynomialDegree.Linear:
                    return LinearRoots(coefficients[1], coefficients[0], epsilon);
                case PolynomialDegree.Quadratic:
                    return QuadraticRoots(coefficients[2], coefficients[1], coefficients[0], epsilon);
                case PolynomialDegree.Cubic:
                    return CubicRoots(coefficients[3], coefficients[2], coefficients[1], coefficients[0], epsilon);
                case PolynomialDegree.Quartic:
                    return QuarticRoots(coefficients[4], coefficients[3], coefficients[2], coefficients[1], coefficients[0], epsilon);
                case PolynomialDegree.Quintic:
                    return QuinticRoots(coefficients[5], coefficients[4], coefficients[3], coefficients[2], coefficients[1], coefficients[0], epsilon);
                case PolynomialDegree.Sextic:
                // ToDo: Uncomment when Sextic roots are implemented.
                //return poly.SexticRoots(epsilon);
                case PolynomialDegree.Septic:
                // ToDo: Uncomment when Septic roots are implemented.
                //return poly.SepticRoots(epsilon);
                case PolynomialDegree.Octic:
                // ToDo: Uncomment when Octic roots are implemented.
                //return poly.OcticRoots(epsilon);
                default:
                    // ToDo: If a general root finding algorithm can be found, call it here instead of returning an empty list.
                    return Array.Empty<double>();
            }

            // should try Newton's method and/or bisection
        }

        /// <summary>
        /// The linear roots.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>The <see cref="T:List{double}"/>.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/geometry/2D/intersections/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<double> LinearRoots(double a, double b, double epsilon = Epsilon)
        {
            var result = new HashSet<double>();
            if (!(Math.Abs(a) <= epsilon))
            {
                result.Add(-b / a);
            }

            return result.ToList();
        }

        /// <summary>
        /// The quadratic roots.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name = "epsilon"> The minimal value to represent a change.</param>
        /// <returns>The <see cref="T:List{double}"/>.</returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/geometry/2D/intersections/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<double> QuadraticRoots(double a, double b, double c, double epsilon = Epsilon)
        {
            var b_ = b / a;
            var c_ = c / a;

            // Polynomial discriminant
            var discriminant = (b_ * b_) - (4d * c_);

            // ToDo: May need to switch from a hash set to a list for scan-beams.
            var results = new HashSet<double>();

            if (Math.Abs(discriminant) <= epsilon)
            {
                discriminant = 0;
            }

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
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// based on http://abecedarical.com/javascript/script_exact_cubic.html
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<double> CubicRoots(double a, double b, double c, double d, double epsilon = Epsilon)
        {
            var A = b / a;
            var B = c / a;
            var C = d / a;

            var Q = ((3d * B) - (A * A)) / 9d;
            var R = (-(2d * A * A * A) + (9d * A * B) - (27d * C)) / 54d;

            var offset = A * OneThird;

            // Polynomial discriminant
            var discriminant = (R * R) + (Q * Q * Q);

            // ToDo: May need to switch from a hash set to a list for scan-beams.
            var results = new HashSet<double>();

            if (Math.Abs(discriminant) <= epsilon)
            {
                discriminant = 0d;
            }

            if (discriminant == 0d)
            {
                var t = Sign(R) * Pow(Math.Abs(R), OneThird);

                // Real root.
                results.Add(-offset + (t + t));

                // Real part of complex root.
                results.Add(-offset - ((t + t) * OneHalf));
            }
            if (discriminant > 0)
            {
                var s = Sign(R + Sqrt(discriminant)) * Pow(Math.Abs(R + Sqrt(discriminant)), OneThird);
                var t = Sign(R - Sqrt(discriminant)) * Pow(Math.Abs(R - Sqrt(discriminant)), OneThird);

                // Real root.
                results.Add(-offset + (s + t));

                // Complex part of root pair.
                var Im = Math.Abs(Sqrt(3d) * (s - t) * OneHalf);
                if (Im == 0d)
                {
                    // Real part of complex root.
                    results.Add(-offset - ((s + t) * OneHalf));
                }
            }
            else if (discriminant < 0)
            {
                // Distinct real roots.
                var th = Acos(R / Sqrt(-Q * Q * Q));

                results.Add((2d * Sqrt(-Q) * Cos(th * OneThird)) - offset);
                results.Add((2d * Sqrt(-Q) * Cos((th + Tau) * OneThird)) - offset);
                results.Add((2d * Sqrt(-Q) * Cos((th + (4d * PI)) * OneThird)) - offset);
            }

            return results.ToList();
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
        /// <returns>The <see cref="T:List{double}"/>.</returns>
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
        public static IList<double> QuarticRoots(double a, double b, double c, double d, double e, double epsilon = Epsilon)
        {
            // ToDo: Translate code found at: http://abecedarical.com/javascript/script_quintic.html
            // and http://jwezorek.com/2015/01/my-code-for-doing-two-things-that-sooner-or-later-you-will-want-to-do-with-bezier-curves/

            var A = b / a;
            var B = c / a;
            var C = d / a;
            var D = e / a;

            var resolveRoots = CubicRoots(
                1d,
                -B,
                (A * C) - (4d * D),
                (-A * A * D) + (4d * B * D) - (C * C),
                epsilon);
            var y = resolveRoots[0];
            var discriminant = (A * A * OneQuarter) - B + y;

            // ToDo: May need to switch from a hash set to a list for scan-beams.
            var results = new HashSet<double>();

            if (Math.Abs(discriminant) <= epsilon)
            {
                discriminant = 0d;
            }

            if (discriminant > 0d)
            {
                var ee = Sqrt(discriminant);
                var t1 = (3d * A * A * OneQuarter) - (ee * ee) - (2d * B);
                var t2 = ((4d * A * B) - (8d * C) - (A * A * A)) / (4d * ee);
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
                    results.Add((-A * OneQuarter) + ((ee + f) * OneHalf));
                    results.Add((-A * OneQuarter) + ((ee - f) * OneHalf));
                }
                if (minus >= 0d)
                {
                    var f = Sqrt(minus);
                    results.Add((-A * OneQuarter) + ((f - ee) * OneHalf));
                    results.Add((-A * OneQuarter) - ((f + ee) * OneHalf));
                }
            }
            else if (discriminant < 0d)
            {
            }
            else
            {
                var t2 = (y * y) - (4d * D);
                if (t2 >= -epsilon)
                {
                    if (t2 < 0)
                    {
                        t2 = 0d;
                    }

                    t2 = 2d * Sqrt(t2);
                    var t1 = (3d * A * A * OneQuarter) - (2d * B);
                    if (t1 + t2 >= epsilon)
                    {
                        var d0 = Sqrt(t1 + t2);
                        results.Add((-A * OneQuarter) + (d0 * OneHalf));
                        results.Add((-A * OneQuarter) - (d0 * OneHalf));
                    }
                    if (t1 - t2 >= epsilon)
                    {
                        var d1 = Sqrt(t1 - t2);
                        results.Add((-A * OneQuarter) + (d1 * OneHalf));
                        results.Add((-A * OneQuarter) - (d1 * OneHalf));
                    }
                }
            }

            return results.ToList();
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
        /// <returns>The <see cref="T:List{double}"/>.</returns>
        /// <acknowledgment>
        /// This is a Copy and paste port of the method found at:
        /// https://web.archive.org/web/20150504111126/http://abecedarical.com/javascript/script_quintic.html
        /// There has been little attempt to fix it up and get it working correctly.
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<double> QuinticRoots(double a, double b, double c, double d, double e, double f, double epsilon = Epsilon)
        {
            //var A = b / a;
            //var B = c / a;
            //var C = d / a;
            //var D = e / a;
            //var E = f / a;

            var coeff = new List<double> { a, b, c, d, e, f };

            // order
            var n = 4;// 5;
            var n1 = 5;// 6;
            var n2 = 6;// 7;

            var a_ = new List<double> { 0d, 0d, 0d, 0d, 0d, 0d };
            var b_ = new List<double> { 0d, 0d, 0d, 0d, 0d, 0d };
            //var c_ = new List<double> { 0d, 0d, 0d, 0d, 0d, 0d };
            var d_ = new List<double> { 0d, 0d, 0d, 0d, 0d, 0d };
            var real = new List<double> { 0d, 0d, 0d, 0d, 0d, 0d };
            var imag = new List<double> { 0d, 0d, 0d, 0d, 0d, 0d };

            // is the coefficient of the highest term zero?
            if (Math.Abs(coeff[0]) < epsilon)
            {
                return new List<double>();
            }

            //  copy into working array
            for (var i = 0; i <= n; i++)
            {
                a_[a_.Count - 1 - i] = coeff[i];
            }

            // initialize root counter
            var count = 0;

            // start the main Lin-Bairstow iteration loop
            do
            {
                // initialize the counter and guesses for the coefficients of quadratic factor: p(x) = x^2 + alfa1*x + beta1
                var alfa1 = Random(OneHalf, 1d);
                var beta1 = Random(OneHalf, 1d);
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
                        // cannot solve
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
                // imaginary roots
                if (delta1 < 0)
                {
                    delta2 = Sqrt(Math.Abs(delta1)) * OneHalf;
                    var delta3 = -alfa1 * OneHalf;

                    real[count] = delta3;
                    imag[count] = delta2;

                    real[count + 1] = delta3;
                    // sign is inverted on display
                    imag[count + 1] = delta2;
                }
                else
                {
                    // roots are real
                    delta2 = Sqrt(delta1);

                    real[count] = (delta2 - alfa1) * OneHalf;
                    imag[count] = 0;

                    real[count + 1] = (delta2 + alfa1) * -OneHalf;
                    imag[count + 1] = 0;
                }

                // update root counter
                count += 2;

                // reduce polynomial order
                n -= 2;
                n1 -= 2;
                n2 -= 2;

                // for n >= 2 calculate coefficients of
                //  the new polynomial
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
                // obtain last single real root
                real[count] = -b_[2];
                imag[count] = 0;
            }

            return real;
        }
        #endregion Root Finding

        #region Bézier Coefficients Overloads
        /// <summary>
        /// Coefficients for a Linear Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) BezierCoefficients(double a, double b)
            => LinearBezierCoefficients(a, b);

        /// <summary>
        /// Coefficients for a Quadratic Bézier curve.
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
        /// Coefficients for a Cubic Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) BezierFormulaCoefficients(double a, double b, double c, double d)
            => CubicBezierCoefficients(a, b, c, d);

        /// <summary>
        /// Coefficients for a Quartic Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E) BezierFormulaCoefficients(double a, double b, double c, double d, double e)
            => QuarticBezierCoefficients(a, b, c, d, e);

        /// <summary>
        /// Coefficients for a Quintic Bézier curve.
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
        public static (double A, double B, double C, double D, double E, double F) BezierFormulaCoefficients(double a, double b, double c, double d, double e, double f)
            => QuinticBezierCoefficients(a, b, c, d, e, f);

        /// <summary>
        /// Coefficients for a Sextic Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F, double G) BezierFormulaCoefficients(double a, double b, double c, double d, double e, double f, double g)
            => SexticBezierCoefficients(a, b, c, d, e, f, g);

        /// <summary>
        /// Coefficients for a Sextic Bézier curve.
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F, double G, double H) BezierFormulaCoefficients(double a, double b, double c, double d, double e, double f, double g, double h)
            => SepticBezierCoefficients(a, b, c, d, e, f, g, h);

        /// <summary>
        /// Coefficients for a Octic Bézier curve.
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F, double G, double H, double I) BezierFormulaCoefficients(double a, double b, double c, double d, double e, double f, double g, double h, double i)
            => OcticBezierCoefficients(a, b, c, d, e, f, g, h, i);

        /// <summary>
        /// Coefficients for a Nonic Bézier curve.
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F, double G, double H, double I, double J) BezierFormulaCoefficients(double a, double b, double c, double d, double e, double f, double g, double h, double i, double j)
            => NonicBezierCoefficients(a, b, c, d, e, f, g, h, i, j);

        /// <summary>
        /// Coefficients for a Decic Bézier curve.
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F, double G, double H, double I, double J, double K) BezierFormulaCoefficients(double a, double b, double c, double d, double e, double f, double g, double h, double i, double j, double k)
            => DecicBezierCoefficients(a, b, c, d, e, f, g, h, i, j, k);
        #endregion Bézier Coefficients Overloads

        #region Bézier Coefficients Stack
        /// <summary>
        /// Coefficients for a Linear Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial LinearBezierCoefficientsStack(double a, double b)
            => (Polynomial.OneMinusT * a) + (Polynomial.T * b);

        /// <summary>
        /// Coefficients for a Quadratic Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial QuadraticBezierCoefficientsStack(double a, double b, double c)
            => (Polynomial.OneMinusT * LinearBezierCoefficientsStack(a, b)) + (Polynomial.T * LinearBezierCoefficientsStack(b, c));

        /// <summary>
        /// Coefficients for a Cubic Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial CubicBezierCoefficientsStack(double a, double b, double c, double d)
            => (Polynomial.OneMinusT * QuadraticBezierCoefficientsStack(a, b, c)) + (Polynomial.T * QuadraticBezierCoefficientsStack(b, c, d));

        /// <summary>
        /// Coefficients for a Quartic Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial QuarticBezierCoefficientsStack(double a, double b, double c, double d, double e)
            => (Polynomial.OneMinusT * CubicBezierCoefficientsStack(a, b, c, d)) + (Polynomial.T * CubicBezierCoefficientsStack(b, c, d, e));

        /// <summary>
        /// Coefficients for a Quintic Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial QuinticBezierCoefficientsStack(double a, double b, double c, double d, double e, double f)
            => (Polynomial.OneMinusT * QuarticBezierCoefficientsStack(a, b, c, d, e)) + (Polynomial.T * QuarticBezierCoefficientsStack(b, c, d, e, f));

        /// <summary>
        /// Coefficients for a Sextic Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial SexticBezierCoefficientsStack(double a, double b, double c, double d, double e, double f, double g)
            => (Polynomial.OneMinusT * QuinticBezierCoefficientsStack(a, b, c, d, e, f)) + (Polynomial.T * QuinticBezierCoefficientsStack(b, c, d, e, f, g));

        /// <summary>
        /// Coefficients for a Septic Bézier curve.
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
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial SepticBezierCoefficientsStack(double a, double b, double c, double d, double e, double f, double g, double h)
            => (Polynomial.OneMinusT * SexticBezierCoefficientsStack(a, b, c, d, e, f, g)) + (Polynomial.T * SexticBezierCoefficientsStack(b, c, d, e, f, g, h));

        /// <summary>
        /// Coefficients for a Octic Bézier curve.
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
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial OcticBezierCoefficientsStack(double a, double b, double c, double d, double e, double f, double g, double h, double i)
            => (Polynomial.OneMinusT * SepticBezierCoefficientsStack(a, b, c, d, e, f, g, h)) + (Polynomial.T * SepticBezierCoefficientsStack(b, c, d, e, f, g, h, i));

        /// <summary>
        /// Coefficients for a Nonic Bézier curve.
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
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial NonicBezierCoefficientsStack(double a, double b, double c, double d, double e, double f, double g, double h, double i, double j)
            => (Polynomial.OneMinusT * OcticBezierCoefficientsStack(a, b, c, d, e, f, g, h, i)) + (Polynomial.T * OcticBezierCoefficientsStack(b, c, d, e, f, g, h, i, j));

        /// <summary>
        /// Coefficients for a Decic Bézier curve.
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
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial DecicBezierCoefficientsStack(double a, double b, double c, double d, double e, double f, double g, double h, double i, double j, double k)
            => (Polynomial.OneMinusT * NonicBezierCoefficientsStack(a, b, c, d, e, f, g, h, i, j)) + (Polynomial.T * NonicBezierCoefficientsStack(b, c, d, e, f, g, h, i, j, k));
        #endregion Bézier Coefficients Stack

        #region Bézier Coefficients
        /// <summary>
        /// Coefficients for a Linear Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://fontforge.github.io/bezier.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) LinearBezierCoefficients(double a, double b)
            => (b - a,
                a);

        /// <summary>
        /// Coefficients for a Quadratic Bézier curve.
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
        /// Coefficients for a Cubic Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
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
        /// Coefficients for a Quartic Bézier curve.
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
        /// Coefficients for a Quintic Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Based off of pseudo-code for the matrix found at:
        /// https://simtk.org/api_docs/opensim/api_docs/classOpenSim_1_1SegmentedQuinticBezierToolkit.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F) QuinticBezierCoefficients(double a, double b, double c, double d, double e, double f)
            => (f - (5d * e) + (10d * d) - (10d * c) + (5d * b) - a,
                (5d * e) - (20d * d) + (30d * c) - (20d * b) + (5d * a),
                (10d * d) - (30d * c) + (30d * b) - (10d * a),
                (10d * c) - (20d * b) + (10d * a),
                5d * (b - a),
                a);

        /// <summary>
        /// Coefficients for a Sextic Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F, double G) SexticBezierCoefficients(double a, double b, double c, double d, double e, double f, double g)
            => (g - (6d * f) + (15d * e) - (20d * d) + (15d * c) - (6d * b) + a,
                    (6d * f) - (30d * e) + (60d * d) - (60d * c) + (30d * b) - (6d * a),
                    (15d * e) - (60d * d) + (90d * c) - (60d * b) + (15d * a),
                    (20d * d) - (60d * c) + (60d * b) - (20d * a),
                    (15d * c) - (30d * b) + (15d * a),
                    (6d * b) - (6d * a),
                    a);

        /// <summary>
        /// Coefficients for a Septic Bézier curve.
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
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F, double G, double H) SepticBezierCoefficients(double a, double b, double c, double d, double e, double f, double g, double h)
            => (h - (7d * g) + (21d * f) - (35d * e) + (35d * d) - (21d * c) + (7d * b) - a,
                    (7d * g) - (42d * f) + (105d * e) - (140d * d) + (105d * c) - (42d * b) + (7d * a),
                    (21d * f) - (105d * e) + (210d * d) - (210d * c) + (105d * b) - (21d * a),
                    (35d * e) - (140d * d) + (210d * c) - (140d * b) + (35d * a),
                    (35d * d) - (105d * c) + (105d * b) - (35d * a),
                    (21d * c) - (42d * b) + (21d * a),
                    (7d * b) - (7d * a),
                    a);

        /// <summary>
        /// Coefficients for a Octic Bézier curve.
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
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F, double G, double H, double I) OcticBezierCoefficients(double a, double b, double c, double d, double e, double f, double g, double h, double i)
            => (i - (8d * h) + (28d * g) - (56d * f) + (70d * e) - (56d * d) + (28d * c) - (8d * b) + a,
                    (8d * h) - (56d * g) + (168d * f) - (280d * e) + (280d * d) - (168d * c) + (56d * b) - (8d * a),
                    (28d * g) - (168d * f) + (420d * e) - (560d * d) + (420d * c) - (168d * b) + (28d * a),
                    (56d * f) - (280d * e) + (560d * d) - (560d * c) + (280d * b) - (56d * a),
                    (70d * e) - (280d * d) + (420d * c) - (280d * b) + (70d * a),
                    (56d * d) - (168d * c) + (168d * b) - (56d * a),
                    (28d * c) - (56d * b) + (28d * a),
                    (8d * b) - (8d * a),
                    a);

        /// <summary>
        /// Coefficients for a Nonic Bézier curve.
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
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F, double G, double H, double I, double J) NonicBezierCoefficients(double a, double b, double c, double d, double e, double f, double g, double h, double i, double j)
            => (j - (9d * i) + (36d * h) - (84d * g) + (126d * f) - (126d * e) + (84d * d) - (36d * c) + (9d * b) - a,
                    (9d * i) - (72d * h) + (252d * g) - (504d * f) + (630d * e) - (504d * d) + (252d * c) - (72d * b) + (9d * a),
                    (36d * h) - (252d * g) + (756d * f) - (1260d * e) + (1260d * d) - (756d * c) + (252 * b) - (36 * a),
                    (84d * g) - (504 * f) + (1260d * e) - (1680d * d) + (1260d * c) - (504d * b) + (84d * a),
                    (126d * f) - (630d * e) + (1260d * d) - (1260d * c) + (630d * b) - (126d * a),
                    (126d * e) - (504d * d) + (756d * c) - (504d * b) + (126d * a),
                    (84d * d) - (252d * c) + (252d * b) - (84d * a),
                    (36d * c) - (72d * b) + (36d * a),
                    (9d * b) - (9d * a),
                    a);

        /// <summary>
        /// Coefficients for a Decic Bézier curve.
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
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F, double G, double H, double I, double J, double K) DecicBezierCoefficients(double a, double b, double c, double d, double e, double f, double g, double h, double i, double j, double k)
            => (k - (10 * j) + (45 * i) - (120 * h) + (210 * g) - (252 * f) + (210 * e) - (120 * d) + (45 * c) - (10 * b) + a,
                    (10 * j) - (90d * i) + (360d * h) - (840d * g) + (1260d * f) - (1260d * e) + (840d * d) - (360d * c) + (90d * b) - (10 * a),
                    (45d * i) - (360d * h) + (1260d * g) - (2520d * f) + (3150d * e) - (2520d * d) + (1260d * c) - (360d * b) + (45d * a),
                    (120d * h) - (840d * g) + (2520d * f) - (4200d * e) + (4200d * d) - (2520d * c) + (840d * b) - (120d * a),
                    (210d * g) - (1260d * f) + (3150d * e) - (4200d * d) + (3150d * c) - (1260d * b) + (210d * a),
                    (252d * f) - (1260d * e) + (2520d * d) - (2520d * c) + (1260d * b) - (252d * a),
                    (210d * e) - (840d * d) + (1260d * c) - (840d * b) + (210d * a),
                    (120d * d) - (360d * c) + (360d * b) - (120d * a),
                    (45d * c) - (90d * b) + (45d * a),
                    (10d * b) - (10d * a),
                    a);
        #endregion Bézier Coefficients

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
            for (var i = 0; i < max_iterations; i++)
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

                        x_correction = dy == 0 ? x - (min.Value + (dx.Value * 0.5)) : Math.Abs(dy / Min(y_atmin, y_atmax)) > RATIO_LIMIT ? x - (min.Value + (dx.Value * (0.5 + (Math.Abs(y_atmin) < Math.Abs(y_atmax) ? -AIMED_BISECT_OFFSET : AIMED_BISECT_OFFSET)))) : x - (min.Value - (y_atmin / dy * dx.Value));
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
