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
//     Many of the Intersections methods were adapted from Kevin Lindsey's site http://www.kevlindev.com/gui/math/intersection/. 
//     Copyright (c) 2000 - 2003 Kevin Lindsey. All rights reserved.
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
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/geometry/2D/intersections/ </remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<double> QuarticRoots(double a, double b, double c, double d, double e, double epsilon = Epsilon)
        {
            var A = b / a;
            var B = c / a;
            var C = d / a;
            var D = e / a;

            var resolveRoots = new Polynomial(1, -B, A * C - 4d * D, -A * A * D + 4 * B * D - C * C).CubicRoots();
            var y = resolveRoots[0];
            var discriminant = A * A * OneQuarter - B + y;

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
