// <copyright file="Maths.Arithmatic.cs" company="Shkyrockett" >
//    Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
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
    public static partial class Maths
    {
        #region Wrapping
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

        /// <summary>
        /// Round a value to the nearest multiple of a number.
        /// </summary>
        /// <param name="value">The value to round.</param>
        /// <param name="multiple">The multiple to round to.</param>
        /// <returns>Returns a value rounded to an interval of the multiple.</returns>
        /// <remarks>Using Convert.ToInt32 because it is faster and guarantees bankers rounding.</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double RoundToMultiple(this double value, double multiple)
            => Convert.ToInt32(value / multiple) * multiple;

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        //[DebuggerStepThrough]
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Clamp<T>(this T value, T min, T max)
            where T : IComparable
            => (value?.CompareTo(min) < 0) ? min : (value?.CompareTo(max) > 0) ? max : value;

        /// <summary>
        /// The wrap.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="double"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Wrap(this double value, double min, double max)
            => (value < min) ? max - (min - value) % (max - min) : min + (value - min) % (max - min);
        #endregion Wrapping

        #region Negate
        /// <summary>
        ///	Negates a <see cref="Matrix2x2D"/>.
        /// </summary>
        /// <param name="sourceM0x0"></param>
        /// <param name="sourceM0x1"></param>
        /// <param name="sourceM1x0"></param>
        /// <param name="sourceM1x1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double, double, double, double) Negate(
            double sourceM0x0, double sourceM0x1,
            double sourceM1x0, double sourceM1x1)
            => (-sourceM0x0,
                -sourceM0x1,
                -sourceM1x0,
                -sourceM1x1);

        /// <summary>
        ///	Negates a <see cref="Matrix3x3D"/>.
        /// </summary>
        /// <param name="sourceM0x0"></param>
        /// <param name="sourceM0x1"></param>
        /// <param name="sourceM0x2"></param>
        /// <param name="sourceM1x0"></param>
        /// <param name="sourceM1x1"></param>
        /// <param name="sourceM1x2"></param>
        /// <param name="sourceM2x0"></param>
        /// <param name="sourceM2x1"></param>
        /// <param name="sourceM2x2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double, double, double, double, double, double, double, double, double) Negate(
            double sourceM0x0, double sourceM0x1, double sourceM0x2,
            double sourceM1x0, double sourceM1x1, double sourceM1x2,
            double sourceM2x0, double sourceM2x1, double sourceM2x2)
            => (-sourceM0x0,
                -sourceM0x1,
                -sourceM0x2,
                -sourceM1x0,
                -sourceM1x1,
                -sourceM1x2,
                -sourceM2x0,
                -sourceM2x1,
                -sourceM2x2);

        /// <summary>
        ///	Negates a <see cref="Matrix4x4D"/>.
        /// </summary>
        /// <param name="sourceM0x0"></param>
        /// <param name="sourceM0x1"></param>
        /// <param name="sourceM0x2"></param>
        /// <param name="sourceM0x3"></param>
        /// <param name="sourceM1x0"></param>
        /// <param name="sourceM1x1"></param>
        /// <param name="sourceM1x2"></param>
        /// <param name="sourceM1x3"></param>
        /// <param name="sourceM2x0"></param>
        /// <param name="sourceM2x1"></param>
        /// <param name="sourceM2x2"></param>
        /// <param name="sourceM2x3"></param>
        /// <param name="sourceM3x0"></param>
        /// <param name="sourceM3x1"></param>
        /// <param name="sourceM3x2"></param>
        /// <param name="sourceM3x3"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double, double, double, double, double, double, double, double, double, double, double, double, double, double, double, double) Negate(
            double sourceM0x0, double sourceM0x1, double sourceM0x2, double sourceM0x3,
            double sourceM1x0, double sourceM1x1, double sourceM1x2, double sourceM1x3,
            double sourceM2x0, double sourceM2x1, double sourceM2x2, double sourceM2x3,
            double sourceM3x0, double sourceM3x1, double sourceM3x2, double sourceM3x3)
            => (-sourceM0x0,
                -sourceM0x1,
                -sourceM0x2,
                -sourceM0x3,
                -sourceM1x0,
                -sourceM1x1,
                -sourceM1x2,
                -sourceM1x3,
                -sourceM2x0,
                -sourceM2x1,
                -sourceM2x2,
                -sourceM2x3,
                -sourceM3x0,
                -sourceM3x1,
                -sourceM3x2,
                -sourceM3x3);
        #endregion Negate

        #region Addition
        /// <summary>
        /// The add2d.
        /// </summary>
        /// <param name="augendI">The augendI.</param>
        /// <param name="augendJ">The augendJ.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J) Add2D(
            double augendI, double augendJ,
            double addend)
            => (augendI + addend, augendJ + addend);

        /// <summary>
        /// The add3d.
        /// </summary>
        /// <param name="augendI">The augendI.</param>
        /// <param name="augendJ">The augendJ.</param>
        /// <param name="augendK">The augendK.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J, double K) Add3D(
            double augendI, double augendJ, double augendK,
            double addend)
            => (augendI + addend, augendJ + addend, augendK + addend);

        /// <summary>
        /// The add4d.
        /// </summary>
        /// <param name="augendI">The augendI.</param>
        /// <param name="augendJ">The augendJ.</param>
        /// <param name="augendK">The augendK.</param>
        /// <param name="augendL">The augendL.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J, double K, double L) Add4D(
            double augendI, double augendJ, double augendK, double augendL,
            double addend)
            => (augendI + addend, augendJ + addend, augendK + addend, augendL + addend);

        /// <summary>
        /// The add2d.
        /// </summary>
        /// <param name="augendI">The augendI.</param>
        /// <param name="augendJ">The augendJ.</param>
        /// <param name="addendI">The addendI.</param>
        /// <param name="addendJ">The addendJ.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J) Add2D(
            double augendI, double augendJ,
            double addendI, double addendJ)
            => (augendI + addendI, augendJ + addendJ);

        /// <summary>
        /// The add3d.
        /// </summary>
        /// <param name="augendI">The augendI.</param>
        /// <param name="augendJ">The augendJ.</param>
        /// <param name="augendK">The augendK.</param>
        /// <param name="addendI">The addendI.</param>
        /// <param name="addendJ">The addendJ.</param>
        /// <param name="addendK">The addendK.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J, double K) Add3D(
            double augendI, double augendJ, double augendK,
            double addendI, double addendJ, double addendK)
            => (augendI + addendI, augendJ + addendJ, augendK + addendK);

        /// <summary>
        /// The add4d.
        /// </summary>
        /// <param name="augendI">The augendI.</param>
        /// <param name="augendJ">The augendJ.</param>
        /// <param name="augendK">The augendK.</param>
        /// <param name="augendL">The augendL.</param>
        /// <param name="addendI">The addendI.</param>
        /// <param name="addendJ">The addendJ.</param>
        /// <param name="addendK">The addendK.</param>
        /// <param name="addendL">The addendL.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J, double K, double L) Add4D(
            double augendI, double augendJ, double augendK, double augendL,
            double addendI, double addendJ, double addendK, double addendL)
            => (augendI + addendI, augendJ + addendJ, augendK + addendK, augendL + addendL);

        /// <summary>
        /// Used to add two matrices together.
        /// </summary>
        /// <param name="augendM0x0"></param>
        /// <param name="augendM0x1"></param>
        /// <param name="augendM1x0"></param>
        /// <param name="augendM1x1"></param>
        /// <param name="addendM0x0"></param>
        /// <param name="addendM0x1"></param>
        /// <param name="addendM1x0"></param>
        /// <param name="addendM1x1"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double, double, double, double) Add2x2x2x2(
            double augendM0x0, double augendM0x1,
            double augendM1x0, double augendM1x1,
            double addendM0x0, double addendM0x1,
            double addendM1x0, double addendM1x1)
            => (augendM0x0 + addendM0x0,
                augendM0x1 + addendM0x1,
                augendM1x0 + addendM1x0,
                augendM1x1 + addendM1x1);

        /// <summary>
        /// Used to add two matrices together.
        /// </summary>
        /// <param name="augendM0x0"></param>
        /// <param name="augendM0x1"></param>
        /// <param name="augendM0x2"></param>
        /// <param name="augendM1x0"></param>
        /// <param name="augendM1x1"></param>
        /// <param name="augendM1x2"></param>
        /// <param name="augendM2x0"></param>
        /// <param name="augendM2x1"></param>
        /// <param name="augendM2x2"></param>
        /// <param name="addendM0x0"></param>
        /// <param name="addendM0x1"></param>
        /// <param name="addendM0x2"></param>
        /// <param name="addendM1x0"></param>
        /// <param name="addendM1x1"></param>
        /// <param name="addendM1x2"></param>
        /// <param name="addendM2x0"></param>
        /// <param name="addendM2x1"></param>
        /// <param name="addendM2x2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double, double, double, double, double, double, double, double, double) Add3x3x3x3(
            double augendM0x0, double augendM0x1, double augendM0x2,
            double augendM1x0, double augendM1x1, double augendM1x2,
            double augendM2x0, double augendM2x1, double augendM2x2,
            double addendM0x0, double addendM0x1, double addendM0x2,
            double addendM1x0, double addendM1x1, double addendM1x2,
            double addendM2x0, double addendM2x1, double addendM2x2)
            => (augendM0x0 + addendM0x0,
                augendM0x1 + addendM0x1,
                augendM0x2 + addendM0x2,
                augendM1x0 + addendM1x0,
                augendM1x1 + addendM1x1,
                augendM1x2 + addendM1x2,
                augendM2x0 + addendM2x0,
                augendM2x1 + addendM2x1,
                augendM2x2 + addendM2x2);

        /// <summary>
        /// Used to add two matrices together.
        /// </summary>
        /// <param name="augendM0x0"></param>
        /// <param name="augendM0x1"></param>
        /// <param name="augendM0x2"></param>
        /// <param name="augendM0x3"></param>
        /// <param name="augendM1x0"></param>
        /// <param name="augendM1x1"></param>
        /// <param name="augendM1x2"></param>
        /// <param name="augendM1x3"></param>
        /// <param name="augendM2x0"></param>
        /// <param name="augendM2x1"></param>
        /// <param name="augendM2x2"></param>
        /// <param name="augendM2x3"></param>
        /// <param name="augendM3x0"></param>
        /// <param name="augendM3x1"></param>
        /// <param name="augendM3x2"></param>
        /// <param name="augendM3x3"></param>
        /// <param name="addendM0x0"></param>
        /// <param name="addendM0x1"></param>
        /// <param name="addendM0x2"></param>
        /// <param name="addendM0x3"></param>
        /// <param name="addendM1x0"></param>
        /// <param name="addendM1x1"></param>
        /// <param name="addendM1x2"></param>
        /// <param name="addendM1x3"></param>
        /// <param name="addendM2x0"></param>
        /// <param name="addendM2x1"></param>
        /// <param name="addendM2x2"></param>
        /// <param name="addendM2x3"></param>
        /// <param name="addendM3x0"></param>
        /// <param name="addendM3x1"></param>
        /// <param name="addendM3x2"></param>
        /// <param name="addendM3x3"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double, double, double, double, double, double, double, double, double, double, double, double, double, double, double, double) Add4x4x4x4(
            double augendM0x0, double augendM0x1, double augendM0x2, double augendM0x3,
            double augendM1x0, double augendM1x1, double augendM1x2, double augendM1x3,
            double augendM2x0, double augendM2x1, double augendM2x2, double augendM2x3,
            double augendM3x0, double augendM3x1, double augendM3x2, double augendM3x3,
            double addendM0x0, double addendM0x1, double addendM0x2, double addendM0x3,
            double addendM1x0, double addendM1x1, double addendM1x2, double addendM1x3,
            double addendM2x0, double addendM2x1, double addendM2x2, double addendM2x3,
            double addendM3x0, double addendM3x1, double addendM3x2, double addendM3x3)
            => (augendM0x0 + addendM0x0,
                augendM0x1 + addendM0x1,
                augendM0x2 + addendM0x2,
                augendM0x3 + addendM0x3,
                augendM1x0 + addendM1x0,
                augendM1x1 + addendM1x1,
                augendM1x2 + addendM1x2,
                augendM1x3 + addendM1x3,
                augendM2x0 + addendM2x0,
                augendM2x1 + addendM2x1,
                augendM2x2 + addendM2x2,
                augendM2x3 + addendM2x3,
                augendM3x0 + addendM3x0,
                augendM3x1 + addendM3x1,
                augendM3x2 + addendM3x2,
                augendM3x3 + addendM3x3);
        #endregion Addition

        #region Subtraction
        /// <summary>
        /// Finds the Delta of two 2D Vectors.
        /// </summary>
        /// <param name="i1"></param>
        /// <param name="j1"></param>
        /// <param name="i2"></param>
        /// <param name="j2"></param>
        /// <returns>Returns the Difference Between PointA and PointB</returns>
        /// <remarks></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J) Delta(double i1, double j1, double i2, double j2)
            => Subtract2D(i2, j2, i1, j1);

        /// <summary>
        /// Finds the Delta of two 3D Vectors.
        /// </summary>
        /// <param name="i1"></param>
        /// <param name="j1"></param>
        /// <param name="k1"></param>
        /// <param name="i2"></param>
        /// <param name="j2"></param>
        /// <param name="k2"></param>
        /// <returns>Returns the Difference Between PointA and PointB</returns>
        /// <remarks></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J, double K) Delta(double i1, double j1, double k1, double i2, double j2, double k2)
            => Subtract3D(i2, j2, k2, i1, j1, k1);

        /// <summary>
        /// Finds the Delta of two 3D Vectors.
        /// </summary>
        /// <param name="i1"></param>
        /// <param name="j1"></param>
        /// <param name="k1"></param>
        /// <param name="l1"></param>
        /// <param name="i2"></param>
        /// <param name="j2"></param>
        /// <param name="k2"></param>
        /// <param name="l2"></param>
        /// <returns>Returns the Difference Between PointA and PointB</returns>
        /// <remarks></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J, double K, double L) Delta(double i1, double j1, double k1, double l1, double i2, double j2, double k2, double l2)
            => Subtract4D(i2, j2, k2, l2, i1, j1, k1, l1);

        /// <summary>
        /// The subtract2d.
        /// </summary>
        /// <param name="minuendI">The minuendI.</param>
        /// <param name="minuendJ">The minuendJ.</param>
        /// <param name="subtrahend">The subtrahend.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J) Subtract2D(double minuendI, double minuendJ, double subtrahend)
            => (minuendI - subtrahend, minuendJ - subtrahend);

        /// <summary>
        /// The subtract3d.
        /// </summary>
        /// <param name="minuendI">The minuendI.</param>
        /// <param name="minuendJ">The minuendJ.</param>
        /// <param name="minuendK">The minuendK.</param>
        /// <param name="subtrahend">The subtrahend.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J, double K) Subtract3D(double minuendI, double minuendJ, double minuendK, double subtrahend)
            => (minuendI - subtrahend, minuendJ - subtrahend, minuendK - subtrahend);

        /// <summary>
        /// The subtract4d.
        /// </summary>
        /// <param name="minuendI">The minuendI.</param>
        /// <param name="minuendJ">The minuendJ.</param>
        /// <param name="minuendK">The minuendK.</param>
        /// <param name="minuendL">The minuendL.</param>
        /// <param name="subtrahend">The subtrahend.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J, double K, double L) Subtract4D(double minuendI, double minuendJ, double minuendK, double minuendL, double subtrahend)
            => (minuendI - subtrahend, minuendJ - subtrahend, minuendK - subtrahend, minuendL - subtrahend);

        /// <summary>
        /// The subtract2d.
        /// </summary>
        /// <param name="minuendI">The minuendI.</param>
        /// <param name="minuendJ">The minuendJ.</param>
        /// <param name="subtrahendI">The subtrahendI.</param>
        /// <param name="subtrahendJ">The subtrahendJ.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J) Subtract2D(double minuendI, double minuendJ, double subtrahendI, double subtrahendJ)
            => (minuendI - subtrahendI, minuendJ - subtrahendJ);

        /// <summary>
        /// The subtract3d.
        /// </summary>
        /// <param name="minuendI">The minuendI.</param>
        /// <param name="minuendJ">The minuendJ.</param>
        /// <param name="minuendK">The minuendK.</param>
        /// <param name="subtrahendI">The subtrahendI.</param>
        /// <param name="subtrahendJ">The subtrahendJ.</param>
        /// <param name="subtrahendK">The subtrahendK.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J, double K) Subtract3D(double minuendI, double minuendJ, double minuendK, double subtrahendI, double subtrahendJ, double subtrahendK)
            => (minuendI - subtrahendI, minuendJ - subtrahendJ, minuendK - subtrahendK);

        /// <summary>
        /// The subtract4d.
        /// </summary>
        /// <param name="minuendI">The minuendI.</param>
        /// <param name="minuendJ">The minuendJ.</param>
        /// <param name="minuendK">The minuendK.</param>
        /// <param name="minuendL">The minuendL.</param>
        /// <param name="subtrahendI">The subtrahendI.</param>
        /// <param name="subtrahendJ">The subtrahendJ.</param>
        /// <param name="subtrahendK">The subtrahendK.</param>
        /// <param name="subtrahendL">The subtrahendL.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J, double K, double L) Subtract4D(double minuendI, double minuendJ, double minuendK, double minuendL, double subtrahendI, double subtrahendJ, double subtrahendK, double subtrahendL)
            => (minuendI - subtrahendI, minuendJ - subtrahendJ, minuendK - subtrahendK, minuendL - subtrahendL);

        /// <summary>
        /// Used to subtract two matrices.
        /// </summary>
        /// <param name="minuendM0x0"></param>
        /// <param name="minuendM0x1"></param>
        /// <param name="minuendM1x0"></param>
        /// <param name="minuendM1x1"></param>
        /// <param name="subtrahendM0x0"></param>
        /// <param name="subtrahendM0x1"></param>
        /// <param name="subtrahendM1x0"></param>
        /// <param name="subtrahendM1x1"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double, double, double, double) Subtract2x2x2x2(
            double minuendM0x0, double minuendM0x1,
            double minuendM1x0, double minuendM1x1,
            double subtrahendM0x0, double subtrahendM0x1,
            double subtrahendM1x0, double subtrahendM1x1)
            => (minuendM0x0 - subtrahendM0x0,
                minuendM0x1 - subtrahendM0x1,
                minuendM1x0 - subtrahendM1x0,
                minuendM1x1 - subtrahendM1x1);

        /// <summary>
        /// Used to subtract two matrices.
        /// </summary>
        /// <param name="minuendM0x0"></param>
        /// <param name="minuendM0x1"></param>
        /// <param name="minuendM0x2"></param>
        /// <param name="minuendM1x0"></param>
        /// <param name="minuendM1x1"></param>
        /// <param name="minuendM1x2"></param>
        /// <param name="minuendM2x0"></param>
        /// <param name="minuendM2x1"></param>
        /// <param name="minuendM2x2"></param>
        /// <param name="subtrahendM0x0"></param>
        /// <param name="subtrahendM0x1"></param>
        /// <param name="subtrahendM0x2"></param>
        /// <param name="subtrahendM1x0"></param>
        /// <param name="subtrahendM1x1"></param>
        /// <param name="subtrahendM1x2"></param>
        /// <param name="subtrahendM2x0"></param>
        /// <param name="subtrahendM2x1"></param>
        /// <param name="subtrahendM2x2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double, double, double, double, double, double, double, double, double) Subtract3x3x3x3(
            double minuendM0x0, double minuendM0x1, double minuendM0x2,
            double minuendM1x0, double minuendM1x1, double minuendM1x2,
            double minuendM2x0, double minuendM2x1, double minuendM2x2,
            double subtrahendM0x0, double subtrahendM0x1, double subtrahendM0x2,
            double subtrahendM1x0, double subtrahendM1x1, double subtrahendM1x2,
            double subtrahendM2x0, double subtrahendM2x1, double subtrahendM2x2)
            => (minuendM0x0 - subtrahendM0x0,
                minuendM0x1 - subtrahendM0x1,
                minuendM0x2 - subtrahendM0x2,
                minuendM1x0 - subtrahendM1x0,
                minuendM1x1 - subtrahendM1x1,
                minuendM1x2 - subtrahendM1x2,
                minuendM2x0 - subtrahendM2x0,
                minuendM2x1 - subtrahendM2x1,
                minuendM2x2 - subtrahendM2x2);

        /// <summary>
        /// Used to subtract two matrices.
        /// </summary>
        /// <param name="minuendM0x0"></param>
        /// <param name="minuendM0x1"></param>
        /// <param name="minuendM0x2"></param>
        /// <param name="minuendM0x3"></param>
        /// <param name="minuendM1x0"></param>
        /// <param name="minuendM1x1"></param>
        /// <param name="minuendM1x2"></param>
        /// <param name="minuendM1x3"></param>
        /// <param name="minuendM2x0"></param>
        /// <param name="minuendM2x1"></param>
        /// <param name="minuendM2x2"></param>
        /// <param name="minuendM2x3"></param>
        /// <param name="minuendM3x0"></param>
        /// <param name="minuendM3x1"></param>
        /// <param name="minuendM3x2"></param>
        /// <param name="minuendM3x3"></param>
        /// <param name="subtrahendM0x0"></param>
        /// <param name="subtrahendM0x1"></param>
        /// <param name="subtrahendM0x2"></param>
        /// <param name="subtrahendM0x3"></param>
        /// <param name="subtrahendM1x0"></param>
        /// <param name="subtrahendM1x1"></param>
        /// <param name="subtrahendM1x2"></param>
        /// <param name="subtrahendM1x3"></param>
        /// <param name="subtrahendM2x0"></param>
        /// <param name="subtrahendM2x1"></param>
        /// <param name="subtrahendM2x2"></param>
        /// <param name="subtrahendM2x3"></param>
        /// <param name="subtrahendM3x0"></param>
        /// <param name="subtrahendM3x1"></param>
        /// <param name="subtrahendM3x2"></param>
        /// <param name="subtrahendM3x3"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double, double, double, double, double, double, double, double, double, double, double, double, double, double, double, double) Subtract4x4x4x4(
            double minuendM0x0, double minuendM0x1, double minuendM0x2, double minuendM0x3,
            double minuendM1x0, double minuendM1x1, double minuendM1x2, double minuendM1x3,
            double minuendM2x0, double minuendM2x1, double minuendM2x2, double minuendM2x3,
            double minuendM3x0, double minuendM3x1, double minuendM3x2, double minuendM3x3,
            double subtrahendM0x0, double subtrahendM0x1, double subtrahendM0x2, double subtrahendM0x3,
            double subtrahendM1x0, double subtrahendM1x1, double subtrahendM1x2, double subtrahendM1x3,
            double subtrahendM2x0, double subtrahendM2x1, double subtrahendM2x2, double subtrahendM2x3,
            double subtrahendM3x0, double subtrahendM3x1, double subtrahendM3x2, double subtrahendM3x3)
            => (minuendM0x0 - subtrahendM0x0,
                minuendM0x1 - subtrahendM0x1,
                minuendM0x2 - subtrahendM0x2,
                minuendM0x3 - subtrahendM0x3,
                minuendM1x0 - subtrahendM1x0,
                minuendM1x1 - subtrahendM1x1,
                minuendM1x2 - subtrahendM1x2,
                minuendM1x3 - subtrahendM1x3,
                minuendM2x0 - subtrahendM2x0,
                minuendM2x1 - subtrahendM2x1,
                minuendM2x2 - subtrahendM2x2,
                minuendM2x3 - subtrahendM2x3,
                minuendM3x0 - subtrahendM3x0,
                minuendM3x1 - subtrahendM3x1,
                minuendM3x2 - subtrahendM3x2,
                minuendM3x3 - subtrahendM3x3);
        #endregion Subtraction

        #region Multiplication
        /// <summary>
        /// Inflates a vector by a given factor.
        /// </summary>
        /// <param name="i">The x value to inflate.</param>
        /// <param name="j">The y value to inflate.</param>
        /// <param name="factor">The factor to inflate the vector.</param>
        /// <returns>Returns a tuple with the values inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J) Scale2D(double i, double j, double factor)
            => ((i * factor), (j * factor));

        /// <summary>
        /// Inflates a vector by a given factor.
        /// </summary>
        /// <param name="i">The x value to inflate.</param>
        /// <param name="j">The y value to inflate.</param>
        /// <param name="k">The z value to inflate.</param>
        /// <param name="factor">The factor to inflate the vector.</param>
        /// <returns>Returns a tuple with the values inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J, double K) Scale3D(double i, double j, double k, double factor)
            => ((i * factor), (j * factor), (k * factor));

        /// <summary>
        /// Inflates a vector by a given factor.
        /// </summary>
        /// <param name="i">The x value to inflate.</param>
        /// <param name="j">The y value to inflate.</param>
        /// <param name="k">The z value to inflate.</param>
        /// <param name="l">The w value to inflate.</param>
        /// <param name="factor">The factor to inflate the vector.</param>
        /// <returns>Returns a tuple structure inflated by the factor provided.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J, double K, double L) Scale4D(double i, double j, double k, double l, double factor)
            => ((i * factor), (j * factor), (k * factor), (l * factor));

        /// <summary>
        /// Used to multiply a Matrix2x2 object by a scalar value.
        /// </summary>
        /// <param name="m0x0"></param>
        /// <param name="m0x1"></param>
        /// <param name="m1x0"></param>
        /// <param name="m1x1"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double, double, double, double) Scale2x2(
            double m0x0, double m0x1,
            double m1x0, double m1x1,
            double scalar)
            => ((m0x0 * scalar),
                m0x1 * scalar,
                m1x0 * scalar,
                m1x1 * scalar);

        /// <summary>
        /// Used to multiply (concatenate) two Matrix2x2s.
        /// </summary>
        /// <param name="leftM0x0"></param>
        /// <param name="leftM0x1"></param>
        /// <param name="leftM1x0"></param>
        /// <param name="leftM1x1"></param>
        /// <param name="rightM0x0"></param>
        /// <param name="rightM0x1"></param>
        /// <param name="rightM1x0"></param>
        /// <param name="rightM1x1"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double, double, double, double) Multiply2x2x2x2(
            double leftM0x0, double leftM0x1,
            double leftM1x0, double leftM1x1,
            double rightM0x0, double rightM0x1,
            double rightM1x0, double rightM1x1)
            => (leftM0x0 * rightM0x0 + leftM0x1 * rightM1x0,
            leftM0x0 * rightM0x1 + leftM0x1 * rightM1x1,
            leftM1x0 * rightM0x0 + leftM1x1 * rightM1x0,
            leftM1x0 * rightM0x1 + leftM1x1 * rightM1x1);

        /// <summary>
        /// Used to multiply a Matrix3x3 object by a scalar value.
        /// </summary>
        /// <param name="m0x0"></param>
        /// <param name="m0x1"></param>
        /// <param name="m0x2"></param>
        /// <param name="m1x0"></param>
        /// <param name="m1x1"></param>
        /// <param name="m1x2"></param>
        /// <param name="m2x0"></param>
        /// <param name="m2x1"></param>
        /// <param name="m2x2"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double, double, double, double, double, double, double, double, double) Scale3x3(
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2,
            double scalar)
            => ((m0x0 * scalar),
                m0x1 * scalar,
                m0x2 * scalar,
                m1x0 * scalar,
                m1x1 * scalar,
                m1x2 * scalar,
                m2x0 * scalar,
                m2x1 * scalar,
                m2x2 * scalar);

        /// <summary>
        /// Used to multiply (concatenate) two Matrix3x3s.
        /// </summary>
        /// <param name="leftM0x0"></param>
        /// <param name="leftM0x1"></param>
        /// <param name="leftM0x2"></param>
        /// <param name="leftM1x0"></param>
        /// <param name="leftM1x1"></param>
        /// <param name="leftM1x2"></param>
        /// <param name="leftM2x0"></param>
        /// <param name="leftM2x1"></param>
        /// <param name="leftM2x2"></param>
        /// <param name="rightM0x0"></param>
        /// <param name="rightM0x1"></param>
        /// <param name="rightM1x0"></param>
        /// <param name="rightM1x1"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double, double, double, double, double, double, double, double, double) Multiply3x3x2x2(
            double leftM0x0, double leftM0x1, double leftM0x2,
            double leftM1x0, double leftM1x1, double leftM1x2,
            double leftM2x0, double leftM2x1, double leftM2x2,
            double rightM0x0, double rightM0x1,
            double rightM1x0, double rightM1x1)
            => (leftM0x0 * rightM0x0 + leftM0x1 * rightM1x0,
                leftM0x0 * rightM0x1 + leftM0x1 * rightM1x1,
                leftM0x2,
                leftM1x0 * rightM0x0 + leftM1x1 * rightM1x0,
                leftM1x0 * rightM0x1 + leftM1x1 * rightM1x1,
                leftM1x2,
                leftM2x0 * rightM0x0 + leftM2x1 * rightM1x0,
                leftM2x0 * rightM0x1 + leftM2x1 * rightM1x1,
                leftM2x2);

        /// <summary>
        /// Used to multiply (concatenate) two Matrix3x3s.
        /// </summary>
        /// <param name="leftM0x0"></param>
        /// <param name="leftM0x1"></param>
        /// <param name="leftM1x0"></param>
        /// <param name="leftM1x1"></param>
        /// <param name="rightM0x0"></param>
        /// <param name="rightM0x1"></param>
        /// <param name="rightM0x2"></param>
        /// <param name="rightM1x0"></param>
        /// <param name="rightM1x1"></param>
        /// <param name="rightM1x2"></param>
        /// <param name="rightM2x0"></param>
        /// <param name="rightM2x1"></param>
        /// <param name="rightM2x2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double, double, double, double, double, double, double, double, double) Multiply2x2x3x3(
            double leftM0x0, double leftM0x1,
            double leftM1x0, double leftM1x1,
            double rightM0x0, double rightM0x1, double rightM0x2,
            double rightM1x0, double rightM1x1, double rightM1x2,
            double rightM2x0, double rightM2x1, double rightM2x2)
            => (leftM0x0 * rightM0x0 + leftM0x1 * rightM1x0,
                leftM0x0 * rightM0x1 + leftM0x1 * rightM1x1,
                leftM0x0 * rightM0x2 + leftM0x1 * rightM1x2,
                leftM1x0 * rightM0x0 + leftM1x1 * rightM1x0,
                leftM1x0 * rightM0x1 + leftM1x1 * rightM1x1,
                leftM1x0 * rightM0x2 + leftM1x1 * rightM1x2,
                rightM2x0,
                rightM2x1,
                rightM2x2);

        /// <summary>
        /// Used to multiply (concatenate) two <see cref="Matrix3x3D"/>s.
        /// </summary>
        /// <param name="leftM0x0"></param>
        /// <param name="leftM0x1"></param>
        /// <param name="leftM0x2"></param>
        /// <param name="leftM1x0"></param>
        /// <param name="leftM1x1"></param>
        /// <param name="leftM1x2"></param>
        /// <param name="leftM2x0"></param>
        /// <param name="leftM2x1"></param>
        /// <param name="leftM2x2"></param>
        /// <param name="rightM0x0"></param>
        /// <param name="rightM0x1"></param>
        /// <param name="rightM0x2"></param>
        /// <param name="rightM1x0"></param>
        /// <param name="rightM1x1"></param>
        /// <param name="rightM1x2"></param>
        /// <param name="rightM2x0"></param>
        /// <param name="rightM2x1"></param>
        /// <param name="rightM2x2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double, double, double, double, double, double, double, double, double) Multiply3x3x3x3(
            double leftM0x0, double leftM0x1, double leftM0x2,
            double leftM1x0, double leftM1x1, double leftM1x2,
            double leftM2x0, double leftM2x1, double leftM2x2,
            double rightM0x0, double rightM0x1, double rightM0x2,
            double rightM1x0, double rightM1x1, double rightM1x2,
            double rightM2x0, double rightM2x1, double rightM2x2)
            => (leftM0x0 * rightM0x0 + leftM0x1 * rightM1x0 + leftM0x2 * rightM2x0,
                leftM0x0 * rightM0x1 + leftM0x1 * rightM1x1 + leftM0x2 * rightM2x1,
                leftM0x0 * rightM0x2 + leftM0x1 * rightM1x2 + leftM0x2 * rightM2x2,
                leftM1x0 * rightM0x0 + leftM1x1 * rightM1x0 + leftM1x2 * rightM2x0,
                leftM1x0 * rightM0x1 + leftM1x1 * rightM1x1 + leftM1x2 * rightM2x1,
                leftM1x0 * rightM0x2 + leftM1x1 * rightM1x2 + leftM1x2 * rightM2x2,
                leftM2x0 * rightM0x0 + leftM2x1 * rightM1x0 + leftM2x2 * rightM2x0,
                leftM2x0 * rightM0x1 + leftM2x1 * rightM1x1 + leftM2x2 * rightM2x1,
                leftM2x0 * rightM0x2 + leftM2x1 * rightM1x2 + leftM2x2 * rightM2x2);

        /// <summary>
        /// Used to multiply (concatenate) two <see cref="Matrix4x4D"/>s.
        /// </summary>
        /// <param name="leftM0x0"></param>
        /// <param name="leftM0x1"></param>
        /// <param name="leftM0x2"></param>
        /// <param name="leftM0x3"></param>
        /// <param name="leftM1x0"></param>
        /// <param name="leftM1x1"></param>
        /// <param name="leftM1x2"></param>
        /// <param name="leftM1x3"></param>
        /// <param name="leftM2x0"></param>
        /// <param name="leftM2x1"></param>
        /// <param name="leftM2x2"></param>
        /// <param name="leftM2x3"></param>
        /// <param name="leftM3x0"></param>
        /// <param name="leftM3x1"></param>
        /// <param name="leftM3x2"></param>
        /// <param name="leftM3x3"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double, double, double, double, double, double, double, double, double, double, double, double, double, double, double, double) Scale4x4(
            double leftM0x0, double leftM0x1, double leftM0x2, double leftM0x3,
            double leftM1x0, double leftM1x1, double leftM1x2, double leftM1x3,
            double leftM2x0, double leftM2x1, double leftM2x2, double leftM2x3,
            double leftM3x0, double leftM3x1, double leftM3x2, double leftM3x3,
            double scalar)
            => ((leftM0x0 * scalar),
                leftM0x1 * scalar,
                leftM0x2 * scalar,
                leftM0x3 * scalar,
                leftM1x0 * scalar,
                leftM1x1 * scalar,
                leftM1x2 * scalar,
                leftM1x3 * scalar,
                leftM2x0 * scalar,
                leftM2x1 * scalar,
                leftM2x2 * scalar,
                leftM2x3 * scalar,
                leftM3x0 * scalar,
                leftM3x1 * scalar,
                leftM3x2 * scalar,
                leftM3x3 * scalar);

        /// <summary>
        /// Used to multiply (concatenate) two <see cref="Matrix4x4D"/>s.
        /// </summary>
        /// <param name="leftM0x0"></param>
        /// <param name="leftM0x1"></param>
        /// <param name="leftM1x0"></param>
        /// <param name="leftM1x1"></param>
        /// <param name="rightM0x0"></param>
        /// <param name="rightM0x1"></param>
        /// <param name="rightM0x2"></param>
        /// <param name="rightM0x3"></param>
        /// <param name="rightM1x0"></param>
        /// <param name="rightM1x1"></param>
        /// <param name="rightM1x2"></param>
        /// <param name="rightM1x3"></param>
        /// <param name="rightM2x0"></param>
        /// <param name="rightM2x1"></param>
        /// <param name="rightM2x2"></param>
        /// <param name="rightM2x3"></param>
        /// <param name="rightM3x0"></param>
        /// <param name="rightM3x1"></param>
        /// <param name="rightM3x2"></param>
        /// <param name="rightM3x3"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double, double, double, double, double, double, double, double, double, double, double, double, double, double, double, double) Multiply2x2x4x4(
            double leftM0x0, double leftM0x1,
            double leftM1x0, double leftM1x1,
            double rightM0x0, double rightM0x1, double rightM0x2, double rightM0x3,
            double rightM1x0, double rightM1x1, double rightM1x2, double rightM1x3,
            double rightM2x0, double rightM2x1, double rightM2x2, double rightM2x3,
            double rightM3x0, double rightM3x1, double rightM3x2, double rightM3x3)
            => (leftM0x0 * rightM0x0 + leftM0x1 * rightM1x0,
                leftM0x0 * rightM0x1 + leftM0x1 * rightM1x1,
                leftM0x0 * rightM0x2 + leftM0x1 * rightM1x2,
                leftM0x0 * rightM0x3 + leftM0x1 * rightM1x3,
                leftM1x0 * rightM0x0 + leftM1x1 * rightM1x0,
                leftM1x0 * rightM0x1 + leftM1x1 * rightM1x1,
                leftM1x0 * rightM0x2 + leftM1x1 * rightM1x2,
                leftM1x0 * rightM0x3 + leftM1x1 * rightM1x3,
                rightM2x0,
                rightM2x1,
                rightM2x2,
                rightM2x3,
                rightM3x0,
                rightM3x1,
                rightM3x2,
                rightM3x3);

        /// <summary>
        /// Used to multiply (concatenate) two <see cref="Matrix4x4D"/>s.
        /// </summary>
        /// <param name="leftM0x0"></param>
        /// <param name="leftM0x1"></param>
        /// <param name="leftM0x2"></param>
        /// <param name="leftM1x0"></param>
        /// <param name="leftM1x1"></param>
        /// <param name="leftM1x2"></param>
        /// <param name="leftM2x0"></param>
        /// <param name="leftM2x1"></param>
        /// <param name="leftM2x2"></param>
        /// <param name="rightM0x0"></param>
        /// <param name="rightM0x1"></param>
        /// <param name="rightM0x2"></param>
        /// <param name="rightM0x3"></param>
        /// <param name="rightM1x0"></param>
        /// <param name="rightM1x1"></param>
        /// <param name="rightM1x2"></param>
        /// <param name="rightM1x3"></param>
        /// <param name="rightM2x0"></param>
        /// <param name="rightM2x1"></param>
        /// <param name="rightM2x2"></param>
        /// <param name="rightM2x3"></param>
        /// <param name="rightM3x0"></param>
        /// <param name="rightM3x1"></param>
        /// <param name="rightM3x2"></param>
        /// <param name="rightM3x3"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double, double, double, double, double, double, double, double, double, double, double, double, double, double, double, double) Multiply3x3x4x4(
            double leftM0x0, double leftM0x1, double leftM0x2,
            double leftM1x0, double leftM1x1, double leftM1x2,
            double leftM2x0, double leftM2x1, double leftM2x2,
            double rightM0x0, double rightM0x1, double rightM0x2, double rightM0x3,
            double rightM1x0, double rightM1x1, double rightM1x2, double rightM1x3,
            double rightM2x0, double rightM2x1, double rightM2x2, double rightM2x3,
            double rightM3x0, double rightM3x1, double rightM3x2, double rightM3x3)
            => (leftM0x0 * rightM0x0 + leftM0x1 * rightM1x0 + leftM0x2 * rightM2x0,
                leftM0x0 * rightM0x1 + leftM0x1 * rightM1x1 + leftM0x2 * rightM2x1,
                leftM0x0 * rightM0x2 + leftM0x1 * rightM1x2 + leftM0x2 * rightM2x2,
                leftM0x0 * rightM0x3 + leftM0x1 * rightM1x3 + leftM0x2 * rightM2x3,
                leftM1x0 * rightM0x0 + leftM1x1 * rightM1x0 + leftM1x2 * rightM2x0,
                leftM1x0 * rightM0x1 + leftM1x1 * rightM1x1 + leftM1x2 * rightM2x1,
                leftM1x0 * rightM0x2 + leftM1x1 * rightM1x2 + leftM1x2 * rightM2x2,
                leftM1x0 * rightM0x3 + leftM1x1 * rightM1x3 + leftM1x2 * rightM2x3,
                leftM2x0 * rightM0x0 + leftM2x1 * rightM1x0 + leftM2x2 * rightM2x0,
                leftM2x0 * rightM0x1 + leftM2x1 * rightM1x1 + leftM2x2 * rightM2x1,
                leftM2x0 * rightM0x2 + leftM2x1 * rightM1x2 + leftM2x2 * rightM2x2,
                leftM2x0 * rightM0x3 + leftM2x1 * rightM1x3 + leftM2x2 * rightM2x3,
                rightM3x0,
                rightM3x1,
                rightM3x2,
                rightM3x3);

        /// <summary>
        /// Used to multiply (concatenate) two <see cref="Matrix4x4D"/>s.
        /// </summary>
        /// <param name="leftM0x0"></param>
        /// <param name="leftM0x1"></param>
        /// <param name="leftM0x2"></param>
        /// <param name="leftM0x3"></param>
        /// <param name="leftM1x0"></param>
        /// <param name="leftM1x1"></param>
        /// <param name="leftM1x2"></param>
        /// <param name="leftM1x3"></param>
        /// <param name="leftM2x0"></param>
        /// <param name="leftM2x1"></param>
        /// <param name="leftM2x2"></param>
        /// <param name="leftM2x3"></param>
        /// <param name="leftM3x0"></param>
        /// <param name="leftM3x1"></param>
        /// <param name="leftM3x2"></param>
        /// <param name="leftM3x3"></param>
        /// <param name="rightM0x0"></param>
        /// <param name="rightM0x1"></param>
        /// <param name="rightM1x0"></param>
        /// <param name="rightM1x1"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double, double, double, double, double, double, double, double, double, double, double, double, double, double, double, double) Multiply4x4x2x2(
            double leftM0x0, double leftM0x1, double leftM0x2, double leftM0x3,
            double leftM1x0, double leftM1x1, double leftM1x2, double leftM1x3,
            double leftM2x0, double leftM2x1, double leftM2x2, double leftM2x3,
            double leftM3x0, double leftM3x1, double leftM3x2, double leftM3x3,
            double rightM0x0, double rightM0x1,
            double rightM1x0, double rightM1x1)
            => (leftM0x0 * rightM0x0 + leftM0x1 * rightM1x0,
                leftM0x0 * rightM0x1 + leftM0x1 * rightM1x1,
                leftM0x2,
                leftM0x3,
                leftM1x0 * rightM0x0 + leftM1x1 * rightM1x0,
                leftM1x0 * rightM0x1 + leftM1x1 * rightM1x1,
                leftM1x2,
                leftM1x3,
                leftM2x0 * rightM0x0 + leftM2x1 * rightM1x0,
                leftM2x0 * rightM0x1 + leftM2x1 * rightM1x1,
                leftM2x2,
                leftM2x3,
                leftM3x0 * rightM0x0 + leftM3x1 * rightM1x0,
                leftM3x0 * rightM0x1 + leftM3x1 * rightM1x1,
                leftM3x2,
                leftM3x3);

        /// <summary>
        /// Used to multiply (concatenate) two <see cref="Matrix4x4D"/>s.
        /// </summary>
        /// <param name="leftM0x0"></param>
        /// <param name="leftM0x1"></param>
        /// <param name="leftM0x2"></param>
        /// <param name="leftM0x3"></param>
        /// <param name="leftM1x0"></param>
        /// <param name="leftM1x1"></param>
        /// <param name="leftM1x2"></param>
        /// <param name="leftM1x3"></param>
        /// <param name="leftM2x0"></param>
        /// <param name="leftM2x1"></param>
        /// <param name="leftM2x2"></param>
        /// <param name="leftM2x3"></param>
        /// <param name="leftM3x0"></param>
        /// <param name="leftM3x1"></param>
        /// <param name="leftM3x2"></param>
        /// <param name="leftM3x3"></param>
        /// <param name="rightM0x0"></param>
        /// <param name="rightM0x1"></param>
        /// <param name="rightM0x2"></param>
        /// <param name="rightM1x0"></param>
        /// <param name="rightM1x1"></param>
        /// <param name="rightM1x2"></param>
        /// <param name="rightM2x0"></param>
        /// <param name="rightM2x1"></param>
        /// <param name="rightM2x2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double, double, double, double, double, double, double, double, double, double, double, double, double, double, double, double) Multiply4x4x3x3(
            double leftM0x0, double leftM0x1, double leftM0x2, double leftM0x3,
            double leftM1x0, double leftM1x1, double leftM1x2, double leftM1x3,
            double leftM2x0, double leftM2x1, double leftM2x2, double leftM2x3,
            double leftM3x0, double leftM3x1, double leftM3x2, double leftM3x3,
            double rightM0x0, double rightM0x1, double rightM0x2,
            double rightM1x0, double rightM1x1, double rightM1x2,
            double rightM2x0, double rightM2x1, double rightM2x2)
            => (
                leftM0x0 * rightM0x0 + leftM0x1 * rightM1x0 + leftM0x2 * rightM2x0,
                leftM0x0 * rightM0x1 + leftM0x1 * rightM1x1 + leftM0x2 * rightM2x1,
                leftM0x0 * rightM0x2 + leftM0x1 * rightM1x2 + leftM0x2 * rightM2x2,
                leftM0x3,
                leftM1x0 * rightM0x0 + leftM1x1 * rightM1x0 + leftM1x2 * rightM2x0,
                leftM1x0 * rightM0x1 + leftM1x1 * rightM1x1 + leftM1x2 * rightM2x1,
                leftM1x0 * rightM0x2 + leftM1x1 * rightM1x2 + leftM1x2 * rightM2x2,
                leftM1x3,
                leftM2x0 * rightM0x0 + leftM2x1 * rightM1x0 + leftM2x2 * rightM2x0,
                leftM2x0 * rightM0x1 + leftM2x1 * rightM1x1 + leftM2x2 * rightM2x1,
                leftM2x0 * rightM0x2 + leftM2x1 * rightM1x2 + leftM2x2 * rightM2x2,
                leftM2x3,
                leftM3x0 * rightM0x0 + leftM3x1 * rightM1x0 + leftM3x2 * rightM2x0,
                leftM3x0 * rightM0x1 + leftM3x1 * rightM1x1 + leftM3x2 * rightM2x1,
                leftM3x0 * rightM0x2 + leftM3x1 * rightM1x2 + leftM3x2 * rightM2x2,
                leftM3x3
            );

        /// <summary>
        /// Used to multiply (concatenate) two <see cref="Matrix4x4D"/>s.
        /// </summary>
        /// <param name="leftM0x0"></param>
        /// <param name="leftM0x1"></param>
        /// <param name="leftM0x2"></param>
        /// <param name="leftM0x3"></param>
        /// <param name="leftM1x0"></param>
        /// <param name="leftM1x1"></param>
        /// <param name="leftM1x2"></param>
        /// <param name="leftM1x3"></param>
        /// <param name="leftM2x0"></param>
        /// <param name="leftM2x1"></param>
        /// <param name="leftM2x2"></param>
        /// <param name="leftM2x3"></param>
        /// <param name="leftM3x0"></param>
        /// <param name="leftM3x1"></param>
        /// <param name="leftM3x2"></param>
        /// <param name="leftM3x3"></param>
        /// <param name="rightM0x0"></param>
        /// <param name="rightM0x1"></param>
        /// <param name="rightM0x2"></param>
        /// <param name="rightM0x3"></param>
        /// <param name="rightM1x0"></param>
        /// <param name="rightM1x1"></param>
        /// <param name="rightM1x2"></param>
        /// <param name="rightM1x3"></param>
        /// <param name="rightM2x0"></param>
        /// <param name="rightM2x1"></param>
        /// <param name="rightM2x2"></param>
        /// <param name="rightM2x3"></param>
        /// <param name="rightM3x0"></param>
        /// <param name="rightM3x1"></param>
        /// <param name="rightM3x2"></param>
        /// <param name="rightM3x3"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double, double, double, double, double, double, double, double, double, double, double, double, double, double, double, double) Multiply4x4x4x4(
            double leftM0x0, double leftM0x1, double leftM0x2, double leftM0x3,
            double leftM1x0, double leftM1x1, double leftM1x2, double leftM1x3,
            double leftM2x0, double leftM2x1, double leftM2x2, double leftM2x3,
            double leftM3x0, double leftM3x1, double leftM3x2, double leftM3x3,
            double rightM0x0, double rightM0x1, double rightM0x2, double rightM0x3,
            double rightM1x0, double rightM1x1, double rightM1x2, double rightM1x3,
            double rightM2x0, double rightM2x1, double rightM2x2, double rightM2x3,
            double rightM3x0, double rightM3x1, double rightM3x2, double rightM3x3)
            => (leftM0x0 * rightM0x0 + leftM0x1 * rightM1x0 + leftM0x2 * rightM2x0 + leftM0x3 * rightM3x0,
                leftM0x0 * rightM0x1 + leftM0x1 * rightM1x1 + leftM0x2 * rightM2x1 + leftM0x3 * rightM3x1,
                leftM0x0 * rightM0x2 + leftM0x1 * rightM1x2 + leftM0x2 * rightM2x2 + leftM0x3 * rightM3x2,
                leftM0x0 * rightM0x3 + leftM0x1 * rightM1x3 + leftM0x2 * rightM2x3 + leftM0x3 * rightM3x3,
                leftM1x0 * rightM0x0 + leftM1x1 * rightM1x0 + leftM1x2 * rightM2x0 + leftM1x3 * rightM3x0,
                leftM1x0 * rightM0x1 + leftM1x1 * rightM1x1 + leftM1x2 * rightM2x1 + leftM1x3 * rightM3x1,
                leftM1x0 * rightM0x2 + leftM1x1 * rightM1x2 + leftM1x2 * rightM2x2 + leftM1x3 * rightM3x2,
                leftM1x0 * rightM0x3 + leftM1x1 * rightM1x3 + leftM1x2 * rightM2x3 + leftM1x3 * rightM3x3,
                leftM2x0 * rightM0x0 + leftM2x1 * rightM1x0 + leftM2x2 * rightM2x0 + leftM2x3 * rightM3x0,
                leftM2x0 * rightM0x1 + leftM2x1 * rightM1x1 + leftM2x2 * rightM2x1 + leftM2x3 * rightM3x1,
                leftM2x0 * rightM0x2 + leftM2x1 * rightM1x2 + leftM2x2 * rightM2x2 + leftM2x3 * rightM3x2,
                leftM2x0 * rightM0x3 + leftM2x1 * rightM1x3 + leftM2x2 * rightM2x3 + leftM2x3 * rightM3x3,
                leftM3x0 * rightM0x0 + leftM3x1 * rightM1x0 + leftM3x2 * rightM2x0 + leftM3x3 * rightM3x0,
                leftM3x0 * rightM0x1 + leftM3x1 * rightM1x1 + leftM3x2 * rightM2x1 + leftM3x3 * rightM3x1,
                leftM3x0 * rightM0x2 + leftM3x1 * rightM1x2 + leftM3x2 * rightM2x2 + leftM3x3 * rightM3x2,
                leftM3x0 * rightM0x3 + leftM3x1 * rightM1x3 + leftM3x2 * rightM2x3 + leftM3x3 * rightM3x3);
        #endregion Multiplication

        #region Division
        /// <summary>
        /// The divide2d1d.
        /// </summary>
        /// <param name="divisorI">The divisorI.</param>
        /// <param name="divisorJ">The divisorJ.</param>
        /// <param name="dividend">The dividend.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J) Divide2D1D(double divisorI, double divisorJ, double dividend)
            => (divisorI / dividend, divisorJ / dividend);

        /// <summary>
        /// The divide1d2d.
        /// </summary>
        /// <param name="divisor">The divisor.</param>
        /// <param name="dividendI">The divedendI.</param>
        /// <param name="dividendJ">The divedendJ.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J) Divide1D2D(double divisor, double dividendI, double dividendJ)
            => (divisor / dividendI, divisor / dividendJ);

        /// <summary>
        /// The divide3d1d.
        /// </summary>
        /// <param name="divisorI">The divisorI.</param>
        /// <param name="divisorJ">The divisorJ.</param>
        /// <param name="divisorK">The divisorK.</param>
        /// <param name="dividend">The dividend.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J, double K) Divide3D1D(double divisorI, double divisorJ, double divisorK, double dividend)
            => (divisorI / dividend, divisorJ / dividend, divisorK / dividend);

        /// <summary>
        /// The divide1d3d.
        /// </summary>
        /// <param name="divisor">The divisor.</param>
        /// <param name="dividendI">The divedendI.</param>
        /// <param name="dividendJ">The divedendJ.</param>
        /// <param name="dividendK">The divedendK.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J, double K) Divide1D3D(double divisor, double dividendI, double dividendJ, double dividendK)
            => (divisor / dividendI, divisor / dividendJ, divisor / dividendK);

        /// <summary>
        /// The divide4d1d.
        /// </summary>
        /// <param name="divisorI">The divisorI.</param>
        /// <param name="divisorJ">The divisorJ.</param>
        /// <param name="divisorK">The divisorK.</param>
        /// <param name="divisorL">The divisorL.</param>
        /// <param name="dividend">The dividend.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J, double K, double L) Divide4D1D(double divisorI, double divisorJ, double divisorK, double divisorL, double dividend)
            => (divisorI / dividend, divisorJ / dividend, divisorK / dividend, divisorL / dividend);

        /// <summary>
        /// The divide1d4d.
        /// </summary>
        /// <param name="divisor">The divisor.</param>
        /// <param name="dividendI">The divedendI.</param>
        /// <param name="dividendJ">The divedendJ.</param>
        /// <param name="dividendK">The divedendK.</param>
        /// <param name="dividendL">The divedendL.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J, double K, double L) Divide1D4D(double divisor, double dividendI, double dividendJ, double dividendK, double dividendL)
            => (divisor / dividendI, divisor / dividendJ, divisor / dividendK, divisor / dividendL);
        #endregion Division

        #region Comparisons
        /// <summary>
        /// Find the maximum value of three variables.
        /// </summary>
        /// <param name="x">The first variable.</param>
        /// <param name="y">The second variable.</param>
        /// <param name="z">The third variable.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <remarks>https://github.com/dystopiancode/colorspace-conversions/</remarks>
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
        /// <remarks>https://github.com/dystopiancode/colorspace-conversions/</remarks>
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
        /// <remarks>https://github.com/dystopiancode/colorspace-conversions/</remarks>
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
        /// <remarks></remarks>
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
        /// <remarks>https://github.com/dystopiancode/colorspace-conversions/</remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Max(double w, double x, double y, double z)
        {
            var t = w;
            if (x > w) t = x;
            if (y > w) t = y;
            if (z > w) t = z;
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
        /// <remarks>https://github.com/dystopiancode/colorspace-conversions/</remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Max(int w, int x, int y, int z)
        {
            var t = w;
            if (x > w) t = x;
            if (y > w) t = y;
            if (z > w) t = z;
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
        /// <remarks>https://github.com/dystopiancode/colorspace-conversions/</remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Max(byte w, byte x, byte y, byte z)
        {
            var t = w;
            if (x > w) t = x;
            if (y > w) t = y;
            if (z > w) t = z;
            return t;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="w"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/6800838/in-c-sharp-is-there-a-method-to-find-the-max-of-3-numbers
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Max2(double w, double x, double y, double z)
            => Math.Max(w, Math.Max(x, Math.Max(y, z)));

        /// <summary>
        ///
        /// </summary>
        /// <param name="point1X"></param>
        /// <param name="point1Y"></param>
        /// <param name="point2X"></param>
        /// <param name="point2Y"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) MaxPoint(double point1X, double point1Y, double point2X, double point2Y)
            => (Math.Max(point1X, point2X), Math.Max(point1Y, point2Y));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MinMax(double x, double min, double max)
            => (x < min)
            ? min
            : (x > max)
            ? max
            : x;

        /// <summary>
        /// Find the minimum value of three variables.
        /// </summary>
        /// <param name="x">The first variable.</param>
        /// <param name="y">The second variable.</param>
        /// <param name="z">The third variable.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <remarks>https://github.com/dystopiancode/colorspace-conversions/</remarks>
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
        /// <remarks>https://github.com/dystopiancode/colorspace-conversions/</remarks>
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
        /// <remarks>https://github.com/dystopiancode/colorspace-conversions/</remarks>
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
        /// <remarks></remarks>
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
        /// <remarks>https://github.com/dystopiancode/colorspace-conversions/</remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Min(double w, double x, double y, double z)
        {
            var t = w;
            if (x < t) t = x;
            if (y < t) t = y;
            if (z < t) t = z;
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
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/6800838/in-c-sharp-is-there-a-method-to-find-the-max-of-3-numbers
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Min2(double w, double x, double y, double z)
            => Math.Min(w, Math.Min(x, Math.Max(y, z)));

        /// <summary>
        ///
        /// </summary>
        /// <param name="point1X"></param>
        /// <param name="point1Y"></param>
        /// <param name="point2X"></param>
        /// <param name="point2Y"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) MinPoint(double point1X, double point1Y, double point2X, double point2Y)
            => (Math.Min(point1X, point2X), Math.Min(point1Y, point2Y));

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://pomax.github.io/bezierinfo
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Approximately(double a, double b, double precision = Epsilon)
            => Math.Abs(a - b) <= precision;

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
        //public static bool AreClose(double aX, double aY, double bX, double bY, double epsilonSqrd = Epsilon)
        //    => (Distances.SquareDistance(aX, aY, bX, bY) <= epsilonSqrd);

        /// <summary>
        /// AreClose - Returns whether or not two doubles are "close".  That is, whether or
        /// not they are within epsilon of each other.  Note that this epsilon is proportional
        /// to the numbers themselves to that AreClose survives scalar multiplication.
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this
        /// returns false.  This is important enough to repeat:
        /// NB: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
        /// used for optimizations *only*.
        /// </summary>
        /// <returns>
        /// bool - the result of the AreClose comparison.
        /// </returns>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AreClose(this float value1, float value2, float epsilon = FloatEpsilon)
        {
            // In case they are Infinities (then epsilon check does not work)
            if (Math.Abs(value1 - value2) < Epsilon)
                return true;
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
        /// NB: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
        /// used for optimizations *only*.
        /// </summary>
        /// <returns>
        /// bool - the result of the AreClose comparison.
        /// </returns>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AreClose(this double value1, double value2, double epsilon = Epsilon)
        {
            // in case they are Infinities (then epsilon check does not work)
            if (Math.Abs(value1 - value2) < Epsilon)
                return true;
            // This computes (|value1-value2| / (|value1| + |value2| + 10.0)) < DBL_EPSILON
            var eps = (Math.Abs(value1) + Math.Abs(value2) + 10d) * epsilon;
            var delta = value1 - value2;
            return (-eps < delta) && (eps > delta);
        }

        /// <summary>
        /// LessThan - Returns whether or not the first double is less than the second double.
        /// That is, whether or not the first is strictly less than *and* not within epsilon of
        /// the other number.  Note that this epsilon is proportional to the numbers themselves
        /// to that AreClose survives scalar multiplication.  Note,
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this
        /// returns false.  This is important enough to repeat:
        /// NB: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
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
        /// NB: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
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

        /// <summary>
        /// GreaterThan - Returns whether or not the first double is greater than the second double.
        /// That is, whether or not the first is strictly greater than *and* not within epsilon of
        /// the other number.  Note that this epsilon is proportional to the numbers themselves
        /// to that AreClose survives scalar multiplication.  Note,
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this
        /// returns false.  This is important enough to repeat:
        /// NB: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
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
        /// NB: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
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

        /// <summary>
        /// LessThanOrClose - Returns whether or not the first double is less than or close to
        /// the second double.  That is, whether or not the first is strictly less than or within
        /// epsilon of the other number.  Note that this epsilon is proportional to the numbers
        /// themselves to that AreClose survives scalar multiplication.  Note,
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this
        /// returns false.  This is important enough to repeat:
        /// NB: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
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
        /// NB: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
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

        /// <summary>
        /// GreaterThanOrClose - Returns whether or not the first float is greater than or close to
        /// the second float.  That is, whether or not the first is strictly greater than or within
        /// epsilon of the other number.  Note that this epsilon is proportional to the numbers
        /// themselves to that AreClose survives scalar multiplication.  Note,
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this
        /// returns false.  This is important enough to repeat:
        /// NB: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
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
        /// NB: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
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
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>
        /// bool - the result of the AreClose comparison.
        /// </returns>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero(this float value, float epsilon = FloatEpsilon)
            => Math.Abs(value) < 10f * epsilon;

        /// <summary>
        /// IsZero - Returns whether or not the double is "close" to 0.  Same as AreClose(double, 0),
        /// but this is faster.
        /// </summary>
        /// <param name="value"> The double to compare to 0. </param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>
        /// bool - the result of the AreClose comparison.
        /// </returns>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero(this double value, double epsilon = Epsilon)
            => Math.Abs(value) < 10d * epsilon;

        /// <summary>
        /// IsOne - Returns whether or not the double is "close" to 1.  Same as AreClose(double, 1),
        /// but this is faster.
        /// </summary>
        /// <param name="value"> The double to compare to 1. </param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>
        /// bool - the result of the AreClose comparison.
        /// </returns>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOne(this float value, float epsilon = FloatEpsilon)
            => Math.Abs(value - 1f) < 10f * epsilon;

        /// <summary>
        /// IsOne - Returns whether or not the double is "close" to 1.  Same as AreClose(double, 1),
        /// but this is faster.
        /// </summary>
        /// <param name="value"> The double to compare to 1. </param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>
        /// bool - the result of the AreClose comparison.
        /// </returns>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOne(this double value, double epsilon = Epsilon)
            => Math.Abs(value - 1d) < 10d * epsilon;

        /// <summary>
        ///
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBetweenZeroAndOne(this float val)
            => (GreaterThanOrClose(val, 0f) && LessThanOrClose(val, 1));

        /// <summary>
        ///
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBetweenZeroAndOne(this double val)
            => (GreaterThanOrClose(val, 0d) && LessThanOrClose(val, 1));
        #endregion Comparisons
    }
}
