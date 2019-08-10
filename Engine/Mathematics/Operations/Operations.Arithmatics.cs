// <copyright file="Operations.Arithmatics.cs" company="Shkyrockett" >
//    Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
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

namespace Engine
{
    /// <summary>
    /// Extended Math processing library.
    /// </summary>
    public static partial class Operations
    {
        #region Min
        /// <summary>
        /// Find the minimum value of three variables.
        /// </summary>
        /// <param name="x">The first variable.</param>
        /// <param name="y">The second variable.</param>
        /// <param name="z">The third variable.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Min(double x, double y, double z)
            => x < y ? x < z ? x : z : y < z ? y : z;

        /// <summary>
        /// Find the minimum value of three variables.
        /// </summary>
        /// <param name="x">The first variable.</param>
        /// <param name="y">The second variable.</param>
        /// <param name="z">The third variable.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Min(int x, int y, int z)
            => x < y ? x < z ? x : z : y < z ? y : z;

        /// <summary>
        /// Find the minimum value of three variables.
        /// </summary>
        /// <param name="x">The first variable.</param>
        /// <param name="y">The second variable.</param>
        /// <param name="z">The third variable.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Min(byte x, byte y, byte z)
            => x < y ? x < z ? x : z : y < z ? y : z;

        /// <summary>
        /// Find the minimum value of three variables.
        /// </summary>
        /// <param name="x">The first variable.</param>
        /// <param name="y">The second variable.</param>
        /// <param name="z">The third variable.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/6800838/in-c-sharp-is-there-a-method-to-find-the-max-of-3-numbers
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Min2(double x, double y, double z)
            => Math.Min(x, Math.Min(y, z));

        /// <summary>
        /// Find the minimum value of four variables.
        /// </summary>
        /// <param name="w">The first variable.</param>
        /// <param name="x">The second variable.</param>
        /// <param name="y">The third variable.</param>
        /// <param name="z">The fourth variable.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Min(double w, double x, double y, double z)
        {
            var t = w;
            if (x < t)
            {
                t = x;
            }

            if (y < t)
            {
                t = y;
            }

            if (z < t)
            {
                t = z;
            }

            return t;
        }

        /// <summary>
        /// Find the minimum value of four variables.
        /// </summary>
        /// <param name="w">The first variable.</param>
        /// <param name="x">The second variable.</param>
        /// <param name="y">The third variable.</param>
        /// <param name="z">The fourth variable.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/6800838/in-c-sharp-is-there-a-method-to-find-the-max-of-3-numbers
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Min2(double w, double x, double y, double z)
            => Math.Min(w, Math.Min(x, Math.Max(y, z)));
        #endregion Min

        #region Max
        /// <summary>
        /// Find the maximum value of three variables.
        /// </summary>
        /// <param name="x">The first variable.</param>
        /// <param name="y">The second variable.</param>
        /// <param name="z">The third variable.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Max(double x, double y, double z)
            => x > y ? x > z ? x : z : y > z ? y : z;

        /// <summary>
        /// Find the maximum value of three variables.
        /// </summary>
        /// <param name="x">The first variable.</param>
        /// <param name="y">The second variable.</param>
        /// <param name="z">The third variable.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Max(int x, int y, int z)
            => x > y ? x > z ? x : z : y > z ? y : z;

        /// <summary>
        /// Find the maximum value of three variables.
        /// </summary>
        /// <param name="x">The first variable.</param>
        /// <param name="y">The second variable.</param>
        /// <param name="z">The third variable.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Max(byte x, byte y, byte z)
            => x > y ? x > z ? x : z : y > z ? y : z;

        /// <summary>
        /// Find the maximum value of three variables.
        /// </summary>
        /// <param name="x">The first variable.</param>
        /// <param name="y">The second variable.</param>
        /// <param name="z">The third variable.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/6800838/in-c-sharp-is-there-a-method-to-find-the-max-of-3-numbers
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Max2(double x, double y, double z)
            => Math.Max(x, Math.Max(y, z));

        /// <summary>
        /// Find the maximum value of four variables.
        /// </summary>
        /// <param name="w">The first variable.</param>
        /// <param name="x">The second variable.</param>
        /// <param name="y">The third variable.</param>
        /// <param name="z">The fourth variable.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Max(double w, double x, double y, double z)
        {
            var t = w;
            if (x > w)
            {
                t = x;
            }

            if (y > w)
            {
                t = y;
            }

            if (z > w)
            {
                t = z;
            }

            return t;
        }

        /// <summary>
        /// Find the maximum value of four variables.
        /// </summary>
        /// <param name="w">The first variable.</param>
        /// <param name="x">The second variable.</param>
        /// <param name="y">The third variable.</param>
        /// <param name="z">The fourth variable.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Max(int w, int x, int y, int z)
        {
            var t = w;
            if (x > w)
            {
                t = x;
            }

            if (y > w)
            {
                t = y;
            }

            if (z > w)
            {
                t = z;
            }

            return t;
        }

        /// <summary>
        /// Find the maximum value of four variables.
        /// </summary>
        /// <param name="w">The first variable.</param>
        /// <param name="x">The second variable.</param>
        /// <param name="y">The third variable.</param>
        /// <param name="z">The fourth variable.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Max(byte w, byte x, byte y, byte z)
        {
            var t = w;
            if (x > w)
            {
                t = x;
            }

            if (y > w)
            {
                t = y;
            }

            if (z > w)
            {
                t = z;
            }

            return t;
        }

        /// <summary>
        /// The max2.
        /// </summary>
        /// <param name="w">The w.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/6800838/in-c-sharp-is-there-a-method-to-find-the-max-of-3-numbers
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Max2(double w, double x, double y, double z)
            => Math.Max(w, Math.Max(x, Math.Max(y, z)));
        #endregion Max

        #region Min Max
        /// <summary>
        /// The min max.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="double"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MinMax(double x, double min, double max)
            => (x < min)
            ? min
            : (x > max)
            ? max
            : x;
        #endregion Min Max

        #region Least Common Denominator
        /// <summary>
        /// The least common denominator. 
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="int"/>.</returns>
        /// <acknowledgment>
        /// https://www.codeproject.com/Articles/76878/Spirograph-Shapes-WPF-Bezier-Shapes-from-Math-Form
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LeastCommonDenominator(int a, int b)
            => a * b / GreatestCommonDenominator(a, b);

        /// <summary>
        /// The least common denominator. 
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="int"/>.</returns>
        /// <acknowledgment>
        /// https://www.codeproject.com/Articles/76878/Spirograph-Shapes-WPF-Bezier-Shapes-from-Math-Form
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LeastCommonDenominator(double a, double b)
            => a * b / GreatestCommonDenominator(a, b);
        #endregion Least Common Denominator

        #region Greatest Common Denominator
        /// <summary>
        /// The greatest common denominator. 
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="int"/>.</returns>
        /// <acknowledgment>
        /// https://www.codeproject.com/Articles/76878/Spirograph-Shapes-WPF-Bezier-Shapes-from-Math-Form
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GreatestCommonDenominator(int a, int b)
        {
            int temp;
            while (b != 0)
            {
                temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        /// <summary>
        /// The greatest common denominator. 
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="int"/>.</returns>
        /// <acknowledgment>
        /// https://www.codeproject.com/Articles/76878/Spirograph-Shapes-WPF-Bezier-Shapes-from-Math-Form
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GreatestCommonDenominator(double a, double b)
        {
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
        #endregion Greatest Common Denominator

        #region Round
        /// <summary>
        /// The round.
        /// </summary>
        /// <param name="val">The val.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <acknowledgment>
        /// http://www.angusj.com
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Round(this float val)
            => (0f < val) ? (int)(val + 0.5f) : (int)(val - 0.5f);

        /// <summary>
        /// The round.
        /// </summary>
        /// <param name="val">The val.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <acknowledgment>
        /// http://www.angusj.com
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Round(this double val)
            => (0d < val) ? (int)(val + 0.5d) : (int)(val - 0.5d);
        #endregion

        #region Round to Multiple
        /// <summary>
        /// Round a value to the nearest multiple of a number.
        /// </summary>
        /// <param name="value">The value to round.</param>
        /// <param name="multiple">The multiple to round to.</param>
        /// <returns>Returns a value rounded to an interval of the multiple.</returns>
        /// <remarks><para>Using Convert.ToInt32 because it is faster and guarantees bankers rounding.</para></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RoundToMultiple(this float value, float multiple)
            => Convert.ToInt32(value / multiple) * multiple;

        /// <summary>
        /// Round a value to the nearest multiple of a number.
        /// </summary>
        /// <param name="value">The value to round.</param>
        /// <param name="multiple">The multiple to round to.</param>
        /// <returns>Returns a value rounded to an interval of the multiple.</returns>
        /// <remarks><para>Using Convert.ToInt32 because it is faster and guarantees bankers rounding.</para></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double RoundToMultiple(this double value, double multiple)
            => Convert.ToInt32(value / multiple) * multiple;
        #endregion

        #region Clamp
        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte Clamp(this sbyte value, sbyte min, sbyte max)
            => value > max ? max : value < min ? min : value;

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Clamp(this byte value, byte min, byte max)
            => value > max ? max : value < min ? min : value;

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short Clamp(this short value, short min, short max)
            => value > max ? max : value < min ? min : value;

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort Clamp(this ushort value, ushort min, ushort max)
            => value > max ? max : value < min ? min : value;

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Clamp(this int value, int min, int max)
            => value > max ? max : value < min ? min : value;

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Clamp(this uint value, uint min, uint max)
            => value > max ? max : value < min ? min : value;

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Clamp(this long value, long min, long max)
            => value > max ? max : value < min ? min : value;

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Clamp(this ulong value, ulong min, ulong max)
            => value > max ? max : value < min ? min : value;

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Clamp(this float value, float min, float max)
            => value > max ? max : value < min ? min : value;

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Clamp(this double value, double min, double max)
            => value > max ? max : value < min ? min : value;

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Clamp<T>(this T value, T min, T max)
            where T : IComparable
            => (value?.CompareTo(min) < 0) ? min : (value?.CompareTo(max) > 0) ? max : value;
        #endregion

        #region Wrapping
        /// <summary>
        /// The wrap.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="sbyte"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte Wrap(this sbyte value, sbyte min, sbyte max)
            => (value < min) ? (sbyte)(max - ((min - value) % (max - min))) : (sbyte)(min + ((value - min) % (max - min)));

        /// <summary>
        /// The wrap.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="byte"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Wrap(this byte value, byte min, byte max)
            => (value < min) ? (byte)(max - ((min - value) % (max - min))) : (byte)(min + ((value - min) % (max - min)));

        /// <summary>
        /// The wrap.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="short"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short Wrap(this short value, short min, short max)
            => (value < min) ? (short)(max - ((min - value) % (max - min))) : (short)(min + ((value - min) % (max - min)));

        /// <summary>
        /// The wrap.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="ushort"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort Wrap(this ushort value, ushort min, ushort max)
            => (value < min) ? (ushort)(max - ((min - value) % (max - min))) : (ushort)(min + ((value - min) % (max - min)));

        /// <summary>
        /// The wrap.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Wrap(this int value, int min, int max)
            => (value < min) ? max - ((min - value) % (max - min)) : min + ((value - min) % (max - min));

        /// <summary>
        /// The wrap.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Wrap(this uint value, uint min, uint max)
            => (value < min) ? max - ((min - value) % (max - min)) : min + ((value - min) % (max - min));

        /// <summary>
        /// The wrap.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="long"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Wrap(this long value, long min, long max)
            => (value < min) ? max - ((min - value) % (max - min)) : min + ((value - min) % (max - min));

        /// <summary>
        /// The wrap.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="ulong"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Wrap(this ulong value, ulong min, ulong max)
            => (value < min) ? max - ((min - value) % (max - min)) : min + ((value - min) % (max - min));

        /// <summary>
        /// The wrap.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="float"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Wrap(this float value, float min, float max)
            => (value < min) ? max - ((min - value) % (max - min)) : min + ((value - min) % (max - min));

        /// <summary>
        /// The wrap.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Wrap(this double value, double min, double max)
            => (value < min) ? max - ((min - value) % (max - min)) : min + ((value - min) % (max - min));
        #endregion Wrapping
    }
}
