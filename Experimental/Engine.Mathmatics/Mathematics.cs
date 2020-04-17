// <copyright file="Mathematics.cs" company="Shkyrockett" >
//    Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
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
using System.Linq;
using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// Extended Math processing library.
    /// </summary>
    public static partial class Mathematics
    {
        #region Random
        /// <summary>
        /// Initialize random number generator with seed based on time.
        /// </summary>
        [ThreadStatic]
        public static readonly Random RandomNumberGenerator = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

        /// <summary>
        /// The random.
        /// </summary>
        /// <param name="Lower">The Lower.</param>
        /// <param name="Upper">The Upper.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Random(this double Lower, double Upper) => (RandomNumberGenerator.Next() * (Upper - Lower + 1)) + Lower;
        #endregion Random

        #region Array Math
        /// <summary>
        /// The min.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Min(params double[] values) => values.Min();

        /// <summary>
        /// The min.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Min(List<double> values) => values.Min();

        /// <summary>
        /// The min.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Min(IEnumerable<double> values) => values.Min();

        /// <summary>
        /// The max.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Max(params double[] values) => values.Max();

        /// <summary>
        /// The max.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Max(List<double> values) => values.Max();

        /// <summary>
        /// The max.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Max(IEnumerable<double> values) => values.Max();

        /// <summary>
        /// The sum.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sum(params double[] values) => values.Sum();

        /// <summary>
        /// Find the sum of an array of Numbers
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sum(List<double> values) => values.Sum();

        /// <summary>
        /// Find the sum of an array of Numbers
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sum(IEnumerable<double> values) => values.Sum();

        /// <summary>
        /// Returns the average value of a numeric array.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <remarks><para>Note: Uses Following Sum Function as well.</para></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Average(params double[] values) => values.Sum() / values.Length;

        /// <summary>
        /// Returns the average value of a numeric array.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <remarks><para>Note: Uses Following Sum Function as well.</para></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Average(this List<double> values) => values.Sum() / values.Count;

        /// <summary>
        /// Returns the average value of a numeric array.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <remarks><para>Note: Uses Following Sum Function as well.</para></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Average(this IEnumerable<double> values) => values.Sum() / values.Count();
        #endregion Array Math
    }
}
