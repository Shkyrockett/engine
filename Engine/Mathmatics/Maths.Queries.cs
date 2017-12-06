// <copyright file="Maths.Queries.cs" company="Shkyrockett" >
//    Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//    Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Runtime.CompilerServices;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// Extended Math processing library.
    /// </summary>
    public static partial class Maths
    {
        #region Queries

        /// <summary>
        /// Counts the number of base 10 digits an integer is represented by.
        /// </summary>
        /// <param name="value">The integer value to count the digits.</param>
        /// <returns>An integer value representing the number of digits that would be printed out.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Digits(this int value)
            => value == 0 ? 1 : (int)Floor(Log10(Math.Abs(value)) + 1);

        /// <summary>
        /// Make sure that a double number is not a NaN or infinity.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>
        /// true if the specified value is valid; otherwise, returns false.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValid(this float value)
            => !float.IsNaN(value) && !float.IsInfinity(value);

        /// <summary>
        /// Make sure that a double number is not a NaN or infinity.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>
        /// true if the specified value is valid; otherwise, returns false.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValid(this double value)
            => !double.IsNaN(value) && !double.IsInfinity(value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://www.angusj.com
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOdd(int val)
            => (val % 2 != 0);

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe(sbyte a, sbyte b)
        {
            if (a == 0 || b == 0 || a == -0 || b == -0) return true;
            if (a < 0) return b >= (sbyte.MinValue - a);
            if (a > 0) return b <= (sbyte.MaxValue - a);
            return true;
        }

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe(byte a, byte b)
        {
            if (a == 0 || b == 0) return true;
            if (a > 0) return b <= (byte.MaxValue - a);
            return true;
        }

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe(short a, short b)
        {
            if (a == 0 || b == 0 || a == -0 || b == -0) return true;
            if (a < 0) return b >= (short.MinValue - a);
            if (a > 0) return b <= (short.MaxValue - a);
            return true;
        }

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe(ushort a, ushort b)
        {
            if (a == 0 || b == 0) return true;
            if (a > 0) return b <= (ushort.MaxValue - a);
            return true;
        }

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe(int a, int b)
        {
            if (a == 0 || b == 0 || a == -0 || b == -0) return true;
            if (a < 0) return b >= (int.MinValue - a);
            if (a > 0) return b <= (int.MaxValue - a);
            return true;
        }

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe(uint a, uint b)
        {
            if (a == 0u || b == 0u) return true;
            if (a > 0u) return b <= (uint.MaxValue - a);
            return true;
        }

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe(long a, long b)
        {
            if (a == 0L || b == 0L) return true;
            if (a < 0L) return b >= (long.MinValue - a);
            if (a > 0L) return b <= (long.MaxValue - a);
            return true;
        }

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe(ulong a, ulong b)
        {
            if (a == 0ul || b == 0ul) return true;
            if (a > 0ul) return b <= (ulong.MaxValue - a);
            return true;
        }

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe(float a, float b)
        {
            if (a == 0f || b == 0f || a == -0f || b == -0f) return true;
            if (a < 0f) return b >= (float.MinValue - a);
            if (a > 0f) return b <= (float.MaxValue - a);
            return true;
        }

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe(double a, double b)
        {
            if (a == 0d || b == 0d || a == -0d || b == -0d) return true;
            if (a < 0d) return b >= (double.MinValue - a);
            if (a > 0d) return b <= (double.MaxValue - a);
            return true;
        }

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe(decimal a, decimal b)
        {
            if (a == decimal.Zero || b == decimal.Zero || a == -decimal.Zero || b == -decimal.Zero) return true;
            if (a < decimal.Zero) return b >= (decimal.MinValue - a);
            if (a > decimal.Zero) return b <= (decimal.MaxValue - a);
            return true;
        }

        /// <summary>
        /// Test whether a multiplication operation is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMultiplicationSafe(int a, int b)
        {
            if (a == 0 || b == 0) return true;
            // a * b would overflow
            return (b > int.MaxValue / a);
        }

        /// <summary>
        /// Test whether a multiplication operation is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMultiplicationSafe(uint a, uint b)
        {
            if (a == 0 || b == 0) return true;
            // a * b would overflow
            return (b > uint.MaxValue / a);
        }

        /// <summary>
        /// Test whether a multiplication operation is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMultiplicationSafe(long a, long b)
        {
            if (a == 0 || b == 0) return true;
            // a * b would overflow
            return (b > long.MaxValue / a);
        }

        /// <summary>
        /// Test whether a multiplication operation is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMultiplicationSafe(ulong a, ulong b)
        {
            if (a == 0 || b == 0) return true;
            // a * b would overflow
            return (b > ulong.MaxValue / a);
        }

        /// <summary>
        /// Test whether a multiplication operation is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMultiplicationSafe(float a, float b)
        {
            if (a == 0 || b == 0) return true;
            // a * b would overflow
            return (b > float.MaxValue / a);
        }

        /// <summary>
        /// Test whether a multiplication operation is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMultiplicationSafe(double a, double b)
        {
            if (a == 0 || b == 0) return true;
            // a * b would overflow
            return (b > double.MaxValue / a);
        }

        /// <summary>
        /// Test whether a multiplication operation is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMultiplicationSafe(decimal a, decimal b)
        {
            if (a == 0 || b == 0) return true;
            // a * b would overflow
            return (b > decimal.MaxValue / a);
        }

        /// <summary>
        /// Test whether a subtraction of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSubtractionSafe(sbyte a, sbyte b)
        {
            if (a == 0 || b == 0 || a == -0 || b == -0) return true;
            if (a < 0) return b >= (sbyte.MinValue + a);
            if (a > 0) return b <= (sbyte.MaxValue + a);
            return true;
        }

        /// <summary>
        /// Test whether a subtraction of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSubtractionSafe(byte a, byte b)
        {
            if (a == 0 && b == 0) return true;
            if (a > 0) return b <= (byte.MaxValue + a);
            if (a == 0) return false;
            if (b == 0) return true;
            return true;
        }

        /// <summary>
        /// Test whether a subtraction of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSubtractionSafe(short a, short b)
        {
            if (a == 0 || b == 0 || a == -0 || b == -0) return true;
            if (a < 0) return b >= (short.MinValue + a);
            if (a > 0) return b <= (short.MaxValue + a);
            return true;
        }

        /// <summary>
        /// Test whether a subtraction of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSubtractionSafe(ushort a, ushort b)
        {
            if (a == 0 && b == 0) return true;
            if (a == 0) return false;
            if (b == 0) return true;
            if (a > 0) return b <= (ushort.MaxValue + a);
            return true;
        }

        /// <summary>
        /// Test whether a subtraction of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSubtractionSafe(int a, int b)
        {
            if (a == 0 || b == 0 || a == -0 || b == -0) return true;
            if (a < 0) return b >= (int.MinValue + a);
            if (a > 0) return b <= (int.MaxValue + a);
            return true;
        }

        /// <summary>
        /// Test whether a subtraction of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSubtractionSafe(uint a, uint b)
        {
            if (a == 0 && b == 0) return true;
            if (a == 0u) return false;
            if (b == 0u) return true;
            if (a > 0u) return b <= (uint.MaxValue + a);
            return true;
        }

        /// <summary>
        /// Test whether a subtraction of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSubtractionSafe(long a, long b)
        {
            if (a == 0L || b == 0L) return true;
            if (a < 0L) return b >= (long.MinValue + a);
            if (a > 0L) return b <= (long.MaxValue + a);
            return true;
        }

        /// <summary>
        /// Test whether a subtraction of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSubtractionSafe(ulong a, ulong b)
        {
            if (a == 0 && b == 0) return true;
            if (a == 0ul) return false;
            if (b == 0ul) return true;
            if (a > 0ul) return b <= (ulong.MaxValue + a);
            return true;
        }

        /// <summary>
        /// Test whether a subtraction of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSubtractionSafe(float a, float b)
        {
            if (a == 0f || b == 0f || a == -0f || b == -0f) return true;
            if (a < 0f) return b >= (float.MinValue + a);
            if (a > 0f) return b <= (float.MaxValue + a);
            return true;
        }

        /// <summary>
        /// Test whether a subtraction of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSubtractionSafe(double a, double b)
        {
            if (a == 0d || b == 0d || a == -0d || b == -0d) return true;
            if (a < 0d) return b >= (double.MinValue + a);
            if (a > 0d) return b <= (double.MaxValue + a);
            return true;
        }

        /// <summary>
        /// Test whether a subtraction of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSubtractionSafe(decimal a, decimal b)
        {
            if (a == decimal.Zero || b == decimal.Zero || a == -decimal.Zero || b == -decimal.Zero) return true;
            if (a < decimal.Zero) return b >= (decimal.MinValue + a);
            if (a > decimal.Zero) return b <= (decimal.MaxValue + a);
            return true;
        }

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
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBackFace(
            double normalI1, double normalJ1, double normalK1,
            double lineOfSightI2, double lineOfSightJ2, double lineOfSightK2)
            => DotProduct(normalI1, normalJ1, normalK1, lineOfSightI2, lineOfSightJ2, lineOfSightK2) < 0;

        #endregion
    }
}
