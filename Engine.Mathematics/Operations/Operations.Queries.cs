// <copyright file="Operations.Queries.cs" company="Shkyrockett" >
//    Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//    Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static System.Math;
using static Engine.Maths;

namespace Engine
{
    /// <summary>
    /// Extended Math processing library.
    /// </summary>
    public static partial class Operations
    {
        /// <summary>
        /// Check whether the double value is between lower and upper bounds.
        /// </summary>
        /// <param name="value">The <paramref name="value"/>.</param>
        /// <param name="lowerLimit">The lower limit.</param>
        /// <param name="upperLimit">The upper limit.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// https://github.com/dystopiancode/colorspace-conversions/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool BetweenLowerUpper(double value, double lowerLimit, double upperLimit) => value >= lowerLimit && value <= upperLimit;

        /// <summary>
        /// Check whether the integer value is between lower and upper bounds.
        /// </summary>
        /// <param name="value">The <paramref name="value"/>.</param>
        /// <param name="lowerLimit">The lower limit.</param>
        /// <param name="upperLimit">The upper limit.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// https://github.com/dystopiancode/colorspace-conversions/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool BetweenLowerUpper(int value, int lowerLimit, int upperLimit) => value >= lowerLimit && value <= upperLimit;

        /// <summary>
        /// Check whether the byte value is between lower and upper bounds.
        /// </summary>
        /// <param name="value">The <paramref name="value"/>.</param>
        /// <param name="lowerLimit">The lower limit.</param>
        /// <param name="upperLimit">The upper limit.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// https://github.com/dystopiancode/colorspace-conversions/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool BetweenLowerUpper(byte value, byte lowerLimit, byte upperLimit) => value >= lowerLimit && value <= upperLimit;

        /// <summary>
        /// Return true iff c is between a and b.  Normalize all parameters wrt c, then ask if a and b are on opposite sides of zero.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/c/5567955982876672
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Between(double c, double a, double b) => (a - c) * (b - c) <= 0;

        /// <summary>
        /// Return true iff c is between a and b.  Normalize all parameters wrt c, then ask if a and b are on opposite sides of zero.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/c/5567955982876672
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Between(int c, int a, int b) => (a - c) * (b - c) <= 0;

        /// <summary>
        /// Return true iff c is between a and b.  Normalize all parameters wrt c, then ask if a and b are on opposite sides of zero.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/c/5567955982876672
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Between(byte c, byte a, byte b) => (a - c) * (b - c) <= 0;

        #region Numeric Digits
        /// <summary>
        /// Counts the number of base 10 digits an integer is represented by.
        /// </summary>
        /// <param name="value">The integer value to count the digits.</param>
        /// <returns>An integer value representing the number of digits that would be printed out.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Digits(this int value)
            => value == 0 ? 1 : (int)Floor(Log10(Math.Abs(value)) + 1);
        #endregion Numeric Digits

        #region Is Valid
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
        #endregion Is Valid

        #region Is Odd
        /// <summary>
        /// The is odd.
        /// </summary>
        /// <param name="val">The val.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://www.angusj.com
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOdd(int val)
            => val % 2 != 0;
        #endregion Is Odd

        #region Is Addition Safe
        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe(sbyte a, sbyte b)
        {
            if (a == 0 || b == 0 || a == -0 || b == -0)
            {
                return true;
            }

            if (a < 0)
            {
                return b >= (sbyte.MinValue - a);
            }

            if (a > 0)
            {
                return b <= (sbyte.MaxValue - a);
            }

            return true;
        }

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe(byte a, byte b)
        {
            if (a == 0 || b == 0)
            {
                return true;
            }

            if (a > 0)
            {
                return b <= (byte.MaxValue - a);
            }

            return true;
        }

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe(short a, short b)
        {
            if (a == 0 || b == 0 || a == -0 || b == -0)
            {
                return true;
            }

            if (a < 0)
            {
                return b >= (short.MinValue - a);
            }

            if (a > 0)
            {
                return b <= (short.MaxValue - a);
            }

            return true;
        }

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe(ushort a, ushort b)
        {
            if (a == 0 || b == 0)
            {
                return true;
            }

            if (a > 0)
            {
                return b <= (ushort.MaxValue - a);
            }

            return true;
        }

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe(int a, int b)
        {
            if (a == 0 || b == 0 || a == -0 || b == -0)
            {
                return true;
            }

            if (a < 0)
            {
                return b >= (int.MinValue - a);
            }

            if (a > 0)
            {
                return b <= (int.MaxValue - a);
            }

            return true;
        }

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe(uint a, uint b)
        {
            if (a == 0u || b == 0u)
            {
                return true;
            }

            if (a > 0u)
            {
                return b <= (uint.MaxValue - a);
            }

            return true;
        }

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe(long a, long b)
        {
            if (a == 0L || b == 0L)
            {
                return true;
            }

            if (a < 0L)
            {
                return b >= (long.MinValue - a);
            }

            if (a > 0L)
            {
                return b <= (long.MaxValue - a);
            }

            return true;
        }

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe(ulong a, ulong b)
        {
            if (a == 0ul || b == 0ul)
            {
                return true;
            }

            if (a > 0ul)
            {
                return b <= (ulong.MaxValue - a);
            }

            return true;
        }

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe(float a, float b)
        {
            if (a == 0f || b == 0f || a == -0f || b == -0f)
            {
                return true;
            }

            if (a < 0f)
            {
                return b >= (float.MinValue - a);
            }

            if (a > 0f)
            {
                return b <= (float.MaxValue - a);
            }

            return true;
        }

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe(double a, double b)
        {
            if (a == 0d || b == 0d || a == -0d || b == -0d)
            {
                return true;
            }

            if (a < 0d)
            {
                return b >= (double.MinValue - a);
            }

            if (a > 0d)
            {
                return b <= (double.MaxValue - a);
            }

            return true;
        }

        /// <summary>
        /// Test whether an addition of two values is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/15920639/how-to-check-if-ab-exceed-long-long-both-a-and-b-is-long-long
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAdditionSafe(decimal a, decimal b)
        {
            if (a == decimal.Zero || b == decimal.Zero || a == -decimal.Zero || b == -decimal.Zero)
            {
                return true;
            }

            if (a < decimal.Zero)
            {
                return b >= (decimal.MinValue - a);
            }

            if (a > decimal.Zero)
            {
                return b <= (decimal.MaxValue - a);
            }

            return true;
        }
        #endregion Is Addition Safe

        #region Is Multiplication Safe
        /// <summary>
        /// Test whether a multiplication operation is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMultiplicationSafe(int a, int b)
        {
            if (a == 0 || b == 0)
            {
                return true;
            }
            // a * b would overflow
            return b > int.MaxValue / a;
        }

        /// <summary>
        /// Test whether a multiplication operation is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMultiplicationSafe(uint a, uint b)
        {
            if (a == 0 || b == 0)
            {
                return true;
            }
            // a * b would overflow
            return b > uint.MaxValue / a;
        }

        /// <summary>
        /// Test whether a multiplication operation is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMultiplicationSafe(long a, long b)
        {
            if (a == 0 || b == 0)
            {
                return true;
            }
            // a * b would overflow
            return b > long.MaxValue / a;
        }

        /// <summary>
        /// Test whether a multiplication operation is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMultiplicationSafe(ulong a, ulong b)
        {
            if (a == 0 || b == 0)
            {
                return true;
            }
            // a * b would overflow
            return b > ulong.MaxValue / a;
        }

        /// <summary>
        /// Test whether a multiplication operation is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMultiplicationSafe(float a, float b)
        {
            if (a == 0 || b == 0)
            {
                return true;
            }
            // a * b would overflow
            return b > float.MaxValue / a;
        }

        /// <summary>
        /// Test whether a multiplication operation is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMultiplicationSafe(double a, double b)
        {
            if (a == 0 || b == 0)
            {
                return true;
            }
            // a * b would overflow
            return b > double.MaxValue / a;
        }

        /// <summary>
        /// Test whether a multiplication operation is likely to overflow.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/199333/how-to-detect-integer-overflow-in-c-c
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMultiplicationSafe(decimal a, decimal b)
        {
            if (a == 0 || b == 0)
            {
                return true;
            }
            // a * b would overflow
            return b > decimal.MaxValue / a;
        }
        #endregion Is Multiplication Safe

        #region Is Subtraction Safe
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
            if (a == 0 || b == 0 || a == -0 || b == -0)
            {
                return true;
            }

            if (a < 0)
            {
                return b >= (sbyte.MinValue + a);
            }

            if (a > 0)
            {
                return b <= (sbyte.MaxValue + a);
            }

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
            if (a == 0 && b == 0)
            {
                return true;
            }

            if (a > 0)
            {
                return b <= (byte.MaxValue + a);
            }

            if (a == 0)
            {
                return false;
            }

            if (b == 0)
            {
                return true;
            }

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
            if (a == 0 || b == 0 || a == -0 || b == -0)
            {
                return true;
            }

            if (a < 0)
            {
                return b >= (short.MinValue + a);
            }

            if (a > 0)
            {
                return b <= (short.MaxValue + a);
            }

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
            if (a == 0 && b == 0)
            {
                return true;
            }

            if (a == 0)
            {
                return false;
            }

            if (b == 0)
            {
                return true;
            }

            if (a > 0)
            {
                return b <= (ushort.MaxValue + a);
            }

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
            if (a == 0 || b == 0 || a == -0 || b == -0)
            {
                return true;
            }

            if (a < 0)
            {
                return b >= (int.MinValue + a);
            }

            if (a > 0)
            {
                return b <= (int.MaxValue + a);
            }

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
            if (a == 0 && b == 0)
            {
                return true;
            }

            if (a == 0u)
            {
                return false;
            }

            if (b == 0u)
            {
                return true;
            }

            if (a > 0u)
            {
                return b <= (uint.MaxValue + a);
            }

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
            if (a == 0L || b == 0L)
            {
                return true;
            }

            if (a < 0L)
            {
                return b >= (long.MinValue + a);
            }

            if (a > 0L)
            {
                return b <= (long.MaxValue + a);
            }

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
            if (a == 0 && b == 0)
            {
                return true;
            }

            if (a == 0ul)
            {
                return false;
            }

            if (b == 0ul)
            {
                return true;
            }

            if (a > 0ul)
            {
                return b <= (ulong.MaxValue + a);
            }

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
            if (a == 0f || b == 0f || a == -0f || b == -0f)
            {
                return true;
            }

            if (a < 0f)
            {
                return b >= (float.MinValue + a);
            }

            if (a > 0f)
            {
                return b <= (float.MaxValue + a);
            }

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
            if (a == 0d || b == 0d || a == -0d || b == -0d)
            {
                return true;
            }

            if (a < 0d)
            {
                return b >= (double.MinValue + a);
            }

            if (a > 0d)
            {
                return b <= (double.MaxValue + a);
            }

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
            if (a == decimal.Zero || b == decimal.Zero || a == -decimal.Zero || b == -decimal.Zero)
            {
                return true;
            }

            if (a < decimal.Zero)
            {
                return b >= (decimal.MinValue + a);
            }

            if (a > decimal.Zero)
            {
                return b <= (decimal.MaxValue + a);
            }

            return true;
        }
        #endregion Is Subtraction Safe

        #region Are Close
        /// <summary>
        /// The approximately.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="precision">The precision.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Approximately(double a, double b, double precision = double.Epsilon) => Math.Abs(a - b) <= precision;

        /// <summary>
        /// AreClose - Returns whether or not two doubles are "close".  That is, whether or
        /// not they are within epsilon of each other.  Note that this epsilon is proportional
        /// to the numbers themselves to that AreClose survives scalar multiplication.
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this
        /// returns false.  This is important enough to repeat:
        /// Important: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
        /// used for optimizations *only*.
        /// </summary>
        /// <returns>
        /// bool - the result of the AreClose comparison.
        /// </returns>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AreClose(this float value1, float value2, float epsilon = float.Epsilon)
        {
            // In case they are Infinities (then epsilon check does not work)
            if (Math.Abs(value1 - value2) < epsilon)
            {
                return true;
            }
            // This computes (|value1-value2| / (|value1| + |value2| + 10.0)) < DBL_EPSILON
            var eps = (Math.Abs(value1) + Math.Abs(value2) + 10f) * epsilon;
            var delta = value1 - value2;
            return (-eps < delta) && (eps > delta);
        }

        /// <summary>
        /// AreClose - Returns whether or not two doubles are "close".  That is, whether or
        /// not they are within epsilon of each other.  Note that this epsilon is proportional
        /// to the numbers themselves to that AreClose survives scalar multiplication.
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this
        /// returns false.  This is important enough to repeat:
        /// Important: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
        /// used for optimizations *only*.
        /// </summary>
        /// <returns>
        /// bool - the result of the AreClose comparison.
        /// </returns>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AreClose(this double value1, double value2, double epsilon = double.Epsilon)
        {
            // in case they are Infinities (then epsilon check does not work)
            if (Math.Abs(value1 - value2) < epsilon)
            {
                return true;
            }
            // This computes (|value1-value2| / (|value1| + |value2| + 10.0)) < DBL_EPSILON
            var eps = (Math.Abs(value1) + Math.Abs(value2) + 10d) * epsilon;
            var delta = value1 - value2;
            return (-eps < delta) && (eps > delta);
        }

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="aX"></param>
        ///// <param name="aY"></param>
        ///// <param name="bX"></param>
        ///// <param name="bY"></param>
        ///// <param name="epsilonSqrd"></param>
        ///// <returns></returns>
        ////[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static bool AreClose(double aX, double aY, double bX, double bY, double epsilonSqrd = double.Epsilon * double.Epsilon) => (Distances.SquareDistance(aX, aY, bX, bY) <= epsilonSqrd);
        #endregion Are Close

        #region Less Than
        /// <summary>
        /// LessThan - Returns whether or not the first double is less than the second double.
        /// That is, whether or not the first is strictly less than *and* not within epsilon of
        /// the other number.  Note that this epsilon is proportional to the numbers themselves
        /// to that AreClose survives scalar multiplication.  Note,
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this
        /// returns false.  This is important enough to repeat:
        /// Important: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
        /// used for optimizations *only*.
        /// </summary>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        /// <returns>
        /// bool - the result of the LessThan comparison.
        /// </returns>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThan(this float value1, float value2)
            => (value1 < value2) && !AreClose(value1, value2);

        /// <summary>
        /// LessThan - Returns whether or not the first double is less than the second double.
        /// That is, whether or not the first is strictly less than *and* not within epsilon of
        /// the other number.  Note that this epsilon is proportional to the numbers themselves
        /// to that AreClose survives scalar multiplication.  Note,
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this
        /// returns false.  This is important enough to repeat:
        /// Important: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
        /// used for optimizations *only*.
        /// </summary>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        /// <returns>
        /// bool - the result of the LessThan comparison.
        /// </returns>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThan(this double value1, double value2)
            => (value1 < value2) && !AreClose(value1, value2);
        #endregion Less Than

        #region Greater Than
        /// <summary>
        /// GreaterThan - Returns whether or not the first double is greater than the second double.
        /// That is, whether or not the first is strictly greater than *and* not within epsilon of
        /// the other number.  Note that this epsilon is proportional to the numbers themselves
        /// to that AreClose survives scalar multiplication.  Note,
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this
        /// returns false.  This is important enough to repeat:
        /// Important: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
        /// used for optimizations *only*.
        /// </summary>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        /// <returns>
        /// bool - the result of the GreaterThan comparison.
        /// </returns>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThan(this float value1, float value2)
            => (value1 > value2) && !AreClose(value1, value2);

        /// <summary>
        /// GreaterThan - Returns whether or not the first double is greater than the second double.
        /// That is, whether or not the first is strictly greater than *and* not within epsilon of
        /// the other number.  Note that this epsilon is proportional to the numbers themselves
        /// to that AreClose survives scalar multiplication.  Note,
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this
        /// returns false.  This is important enough to repeat:
        /// Important: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
        /// used for optimizations *only*.
        /// </summary>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        /// <returns>
        /// bool - the result of the GreaterThan comparison.
        /// </returns>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThan(this double value1, double value2)
            => (value1 > value2) && !AreClose(value1, value2);
        #endregion Greater Than

        #region Less Than Or Close
        /// <summary>
        /// LessThanOrClose - Returns whether or not the first double is less than or close to
        /// the second double.  That is, whether or not the first is strictly less than or within
        /// epsilon of the other number.  Note that this epsilon is proportional to the numbers
        /// themselves to that AreClose survives scalar multiplication.  Note,
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this
        /// returns false.  This is important enough to repeat:
        /// Important: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
        /// used for optimizations *only*.
        /// </summary>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        /// <returns>
        /// bool - the result of the LessThanOrClose comparison.
        /// </returns>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThanOrClose(this float value1, float value2)
            => (value1 < value2) || AreClose(value1, value2);

        /// <summary>
        /// LessThanOrClose - Returns whether or not the first double is less than or close to
        /// the second double.  That is, whether or not the first is strictly less than or within
        /// epsilon of the other number.  Note that this epsilon is proportional to the numbers
        /// themselves to that AreClose survives scalar multiplication.  Note,
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this
        /// returns false.  This is important enough to repeat:
        /// Important: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
        /// used for optimizations *only*.
        /// </summary>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        /// <returns>
        /// bool - the result of the LessThanOrClose comparison.
        /// </returns>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThanOrClose(this double value1, double value2)
            => (value1 < value2) || AreClose(value1, value2);
        #endregion Less Than Or Close

        #region Greater Than Or Close
        /// <summary>
        /// GreaterThanOrClose - Returns whether or not the first float is greater than or close to
        /// the second float.  That is, whether or not the first is strictly greater than or within
        /// epsilon of the other number.  Note that this epsilon is proportional to the numbers
        /// themselves to that AreClose survives scalar multiplication.  Note,
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this
        /// returns false.  This is important enough to repeat:
        /// Important: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
        /// used for optimizations *only*.
        /// </summary>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        /// <returns>
        /// bool - the result of the GreaterThanOrClose comparison.
        /// </returns>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThanOrClose(this float value1, float value2)
            => (value1 > value2) || AreClose(value1, value2);

        /// <summary>
        /// GreaterThanOrClose - Returns whether or not the first double is greater than or close to
        /// the second double.  That is, whether or not the first is strictly greater than or within
        /// epsilon of the other number.  Note that this epsilon is proportional to the numbers
        /// themselves to that AreClose survives scalar multiplication.  Note,
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this
        /// returns false.  This is important enough to repeat:
        /// Important: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
        /// used for optimizations *only*.
        /// </summary>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        /// <returns>
        /// bool - the result of the GreaterThanOrClose comparison.
        /// </returns>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThanOrClose(this double value1, double value2)
            => (value1 > value2) || AreClose(value1, value2);
        #endregion Greater Than Or Close

        #region Near Zero
        /// <summary>
        /// The near zero.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NearZero(double value, double epsilon = NearZeroEpsilon)
            => (value > -epsilon) && (value < -epsilon);

        /// <summary>
        /// IsZero - Returns whether or not the double is "close" to 0.  Same as AreClose(double, 0),
        /// but this is faster.
        /// </summary>
        /// <param name="value"> The double to compare to 0. </param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>
        /// bool - the result of the AreClose comparison.
        /// </returns>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero(this float value, float epsilon = float.Epsilon)
            => Math.Abs(value) < 10f * epsilon;

        /// <summary>
        /// IsZero - Returns whether or not the double is "close" to 0.  Same as AreClose(double, 0),
        /// but this is faster.
        /// </summary>
        /// <param name="value"> The double to compare to 0. </param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>
        /// bool - the result of the AreClose comparison.
        /// </returns>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero(this double value, double epsilon = double.Epsilon)
            => Math.Abs(value) < 10d * epsilon;
        #endregion Near Zero

        #region Near One
        /// <summary>
        /// IsOne - Returns whether or not the double is "close" to 1.  Same as AreClose(double, 1),
        /// but this is faster.
        /// </summary>
        /// <param name="value"> The double to compare to 1. </param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>
        /// bool - the result of the AreClose comparison.
        /// </returns>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOne(this float value, float epsilon = float.Epsilon)
            => Math.Abs(value - 1f) < 10f * epsilon;

        /// <summary>
        /// IsOne - Returns whether or not the double is "close" to 1.  Same as AreClose(double, 1),
        /// but this is faster.
        /// </summary>
        /// <param name="value"> The double to compare to 1. </param>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>
        /// bool - the result of the AreClose comparison.
        /// </returns>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOne(this double value, double epsilon = double.Epsilon)
            => Math.Abs(value - 1d) < 10d * epsilon;
        #endregion Near One

        #region Between Zero Or One
        /// <summary>
        /// The is between zero and one.
        /// </summary>
        /// <param name="val">The val.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBetweenZeroAndOne(this float val)
            => GreaterThanOrClose(val, 0f) && LessThanOrClose(val, 1);

        /// <summary>
        /// The is between zero and one.
        /// </summary>
        /// <param name="val">The val.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBetweenZeroAndOne(this double val)
            => GreaterThanOrClose(val, 0d) && LessThanOrClose(val, 1);
        #endregion Between Zero Or One

        /// <summary>
        /// The is back face.
        /// </summary>
        /// <param name="normalI1">The normalI1.</param>
        /// <param name="normalJ1">The normalJ1.</param>
        /// <param name="normalK1">The normalK1.</param>
        /// <param name="lineOfSightI2">The lineOfSightI2.</param>
        /// <param name="lineOfSightJ2">The lineOfSightJ2.</param>
        /// <param name="lineOfSightK2">The lineOfSightK2.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBackFace(
            double normalI1, double normalJ1, double normalK1,
            double lineOfSightI2, double lineOfSightJ2, double lineOfSightK2)
            => DotProduct(normalI1, normalJ1, normalK1, lineOfSightI2, lineOfSightJ2, lineOfSightK2) < 0;
    }
}
