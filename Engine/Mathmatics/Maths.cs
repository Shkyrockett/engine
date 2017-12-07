// <copyright file="Maths.cs" company="Shkyrockett" >
//    Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
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
    }
}
