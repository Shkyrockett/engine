// <copyright file="Maths.cs" company="Shkyrockett" >
//    Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//    Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
        #region Random

        /// <summary>
        /// Initialize random number generator with seed based on time.
        /// </summary>
        public static Random RandomNumberGenerator = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

        /// <summary>
        ///
        /// </summary>
        /// <param name="Lower"></param>
        /// <param name="Upper"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Random(this double Lower, double Upper)
            => ((RandomNumberGenerator.Next() * ((Upper - Lower) + 1)) + Lower);

        #endregion

        #region Array Math

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Min(params double[] values)
            => Enumerable.Min(values);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Min(List<double> values)
            => Enumerable.Min(values);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Min(IEnumerable<double> values)
            => Enumerable.Min(values);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Max(params double[] values)
            => Enumerable.Max(values);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Max(List<double> values)
            => Enumerable.Max(values);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Max(IEnumerable<double> values)
            => Enumerable.Max(values);

        /// <summary>
        /// Find the sum of an array of Numbers
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sum(params double[] values)
            => values.Sum();

        /// <summary>
        /// Find the sum of an array of Numbers
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sum(List<double> values)
            => values.Sum();

        /// <summary>
        /// Find the sum of an array of Numbers
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sum(IEnumerable<double> values)
            => values.Sum();

        /// <summary>
        /// Returns the average value of a numeric array.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <remarks>Note: Uses Following Sum Function as well.</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Average(params double[] values)
            => (values.Sum() / values.Length);

        /// <summary>
        /// Returns the average value of a numeric array.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <remarks>Note: Uses Following Sum Function as well.</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Average(this List<double> values)
            => (values.Sum() / values.Count);

        /// <summary>
        /// Returns the average value of a numeric array.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <remarks>Note: Uses Following Sum Function as well.</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Average(this IEnumerable<double> values)
            => values.Sum() / values.Count();

        #endregion

        /// <summary>
        /// Swap left and right values so the left object has the value of the right object and visa-versa.
        /// </summary>
        /// <param name="a">The left value.</param>
        /// <param name="b">The right value.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Swap<T>(ref T a, ref T b)
        {
            T swap = a;
            a = b;
            b = swap;
        }

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
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseSqrt(double number)
            => 1 / Sqrt(number);

        /// <summary>
        /// Cube root equivalent of the sqrt function. (note that there are actually
        /// three roots: one real, two complex, and we don't care about the latter):
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/26823024/cubic-bezier-reverse-getpoint-equation-float-for-vector-vector-for-float?answertab=active#tab-top</remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Crt(double value)
            => value < 0
            ? -Pow(-value, OneThird)
            : Pow(value, OneThird);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseCrt(double number)
            => 1 / Crt(number);

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double S) QuadraticEquation(double a, double b, double c)
            => (
            (-b + Sqrt(b * b - (4 * a * c))) / (2 * a),
            (-b - Sqrt(b * b - (4 * a * c))) / (2 * a));

        #region Parsing.

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ParseFloat(this string text)
            => float.Parse(text, CultureInfo.InvariantCulture);

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ParseFloat(this string text, IFormatProvider provider)
            => float.Parse(text, provider);

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ParseDouble(this string text)
            => double.Parse(text, CultureInfo.InvariantCulture);

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ParseDouble(this string text, IFormatProvider provider)
            => double.Parse(text, provider);

        #endregion

        /// <summary>
        ///
        /// </summary>
        /// <param name="normalI1"></param>
        /// <param name="normalJ1"></param>
        /// <param name="normalK1"></param>
        /// <param name="lineOfSightI2"></param>
        /// <param name="lineOfSightJ2"></param>
        /// <param name="lineOfSightK2"></param>
        /// <returns></returns>
        /// <remarks>http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C</remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBackFace(
            double normalI1, double normalJ1, double normalK1,
            double lineOfSightI2, double lineOfSightJ2, double lineOfSightK2)
            => DotProduct(normalI1, normalJ1, normalK1, lineOfSightI2, lineOfSightJ2, lineOfSightK2) < 0;
    }
}
