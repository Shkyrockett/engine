// <copyright file="Operations.Matricies.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// The Operations class.
    /// </summary>
    public static partial class Operations
    {
        #region Unary Add
        /// <summary>
        /// Posates a <see cref="Matrix2x2D" />.
        /// </summary>
        /// <param name="sourceM0x0">The source M0X0.</param>
        /// <param name="sourceM0x1">The source M0X1.</param>
        /// <param name="sourceM1x0">The source M1X0.</param>
        /// <param name="sourceM1x1">The source M1X1.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1,
            double m1x0, double m1x1
            ) UnaryAdd(
            double sourceM0x0, double sourceM0x1,
            double sourceM1x0, double sourceM1x1)
            => (+sourceM0x0, +sourceM0x1,
                +sourceM1x0, +sourceM1x1);

        /// <summary>
        /// Posates a <see cref="Matrix3x3D" />.
        /// </summary>
        /// <param name="sourceM0x0">The source M0X0.</param>
        /// <param name="sourceM0x1">The source M0X1.</param>
        /// <param name="sourceM0x2">The source M0X2.</param>
        /// <param name="sourceM1x0">The source M1X0.</param>
        /// <param name="sourceM1x1">The source M1X1.</param>
        /// <param name="sourceM1x2">The source M1X2.</param>
        /// <param name="sourceM2x0">The source M2X0.</param>
        /// <param name="sourceM2x1">The source M2X1.</param>
        /// <param name="sourceM2x2">The source M2X2.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2
            ) UnaryAdd(
            double sourceM0x0, double sourceM0x1, double sourceM0x2,
            double sourceM1x0, double sourceM1x1, double sourceM1x2,
            double sourceM2x0, double sourceM2x1, double sourceM2x2)
            => (+sourceM0x0, +sourceM0x1, +sourceM0x2,
                +sourceM1x0, +sourceM1x1, +sourceM1x2,
                +sourceM2x0, +sourceM2x1, +sourceM2x2);

        /// <summary>
        /// Posates a <see cref="Matrix4x4D" />.
        /// </summary>
        /// <param name="sourceM0x0">The source M0X0.</param>
        /// <param name="sourceM0x1">The source M0X1.</param>
        /// <param name="sourceM0x2">The source M0X2.</param>
        /// <param name="sourceM0x3">The source M0X3.</param>
        /// <param name="sourceM1x0">The source M1X0.</param>
        /// <param name="sourceM1x1">The source M1X1.</param>
        /// <param name="sourceM1x2">The source M1X2.</param>
        /// <param name="sourceM1x3">The source M1X3.</param>
        /// <param name="sourceM2x0">The source M2X0.</param>
        /// <param name="sourceM2x1">The source M2X1.</param>
        /// <param name="sourceM2x2">The source M2X2.</param>
        /// <param name="sourceM2x3">The source M2X3.</param>
        /// <param name="sourceM3x0">The source M3X0.</param>
        /// <param name="sourceM3x1">The source M3X1.</param>
        /// <param name="sourceM3x2">The source M3X2.</param>
        /// <param name="sourceM3x3">The source M3X3.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3,
            double m1x0, double m1x1, double m1x2, double m1x3,
            double m2x0, double m2x1, double m2x2, double m2x3,
            double m3x0, double m3x1, double m3x2, double m3x3
            ) UnaryAdd(
            double sourceM0x0, double sourceM0x1, double sourceM0x2, double sourceM0x3,
            double sourceM1x0, double sourceM1x1, double sourceM1x2, double sourceM1x3,
            double sourceM2x0, double sourceM2x1, double sourceM2x2, double sourceM2x3,
            double sourceM3x0, double sourceM3x1, double sourceM3x2, double sourceM3x3)
            => (+sourceM0x0, +sourceM0x1, +sourceM0x2, +sourceM0x3,
                +sourceM1x0, +sourceM1x1, +sourceM1x2, +sourceM1x3,
                +sourceM2x0, +sourceM2x1, +sourceM2x2, +sourceM2x3,
                +sourceM3x0, +sourceM3x1, +sourceM3x2, +sourceM3x3);

        /// <summary>
        /// Posates a Matrix5x5D.
        /// </summary>
        /// <param name="sourceM0x0">The source M0X0.</param>
        /// <param name="sourceM0x1">The source M0X1.</param>
        /// <param name="sourceM0x2">The source M0X2.</param>
        /// <param name="sourceM0x3">The source M0X3.</param>
        /// <param name="sourceM0x4">The source M0X4.</param>
        /// <param name="sourceM1x0">The source M1X0.</param>
        /// <param name="sourceM1x1">The source M1X1.</param>
        /// <param name="sourceM1x2">The source M1X2.</param>
        /// <param name="sourceM1x3">The source M1X3.</param>
        /// <param name="sourceM1x4">The source M1X4.</param>
        /// <param name="sourceM2x0">The source M2X0.</param>
        /// <param name="sourceM2x1">The source M2X1.</param>
        /// <param name="sourceM2x2">The source M2X2.</param>
        /// <param name="sourceM2x3">The source M2X3.</param>
        /// <param name="sourceM2x4">The source M2X4.</param>
        /// <param name="sourceM3x0">The source M3X0.</param>
        /// <param name="sourceM3x1">The source M3X1.</param>
        /// <param name="sourceM3x2">The source M3X2.</param>
        /// <param name="sourceM3x3">The source M3X3.</param>
        /// <param name="sourceM3x4">The source M3X4.</param>
        /// <param name="sourceM4x0">The source M4X0.</param>
        /// <param name="sourceM4x1">The source M4X1.</param>
        /// <param name="sourceM4x2">The source M4X2.</param>
        /// <param name="sourceM4x3">The source M4X3.</param>
        /// <param name="sourceM4x4">The source M4X4.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4
            ) UnaryAdd(
            double sourceM0x0, double sourceM0x1, double sourceM0x2, double sourceM0x3, double sourceM0x4,
            double sourceM1x0, double sourceM1x1, double sourceM1x2, double sourceM1x3, double sourceM1x4,
            double sourceM2x0, double sourceM2x1, double sourceM2x2, double sourceM2x3, double sourceM2x4,
            double sourceM3x0, double sourceM3x1, double sourceM3x2, double sourceM3x3, double sourceM3x4,
            double sourceM4x0, double sourceM4x1, double sourceM4x2, double sourceM4x3, double sourceM4x4)
            => (+sourceM0x0, +sourceM0x1, +sourceM0x2, +sourceM0x3, +sourceM0x4,
                +sourceM1x0, +sourceM1x1, +sourceM1x2, +sourceM1x3, +sourceM1x4,
                +sourceM2x0, +sourceM2x1, +sourceM2x2, +sourceM2x3, +sourceM2x4,
                +sourceM3x0, +sourceM3x1, +sourceM3x2, +sourceM3x3, +sourceM3x4,
                +sourceM4x0, +sourceM4x1, +sourceM4x2, +sourceM4x3, +sourceM4x4);

        /// <summary>
        /// Posates a Matrix6x6D.
        /// </summary>
        /// <param name="sourceM0x0">The source M0X0.</param>
        /// <param name="sourceM0x1">The source M0X1.</param>
        /// <param name="sourceM0x2">The source M0X2.</param>
        /// <param name="sourceM0x3">The source M0X3.</param>
        /// <param name="sourceM0x4">The source M0X4.</param>
        /// <param name="sourceM0x5">The source M0X5.</param>
        /// <param name="sourceM1x0">The source M1X0.</param>
        /// <param name="sourceM1x1">The source M1X1.</param>
        /// <param name="sourceM1x2">The source M1X2.</param>
        /// <param name="sourceM1x3">The source M1X3.</param>
        /// <param name="sourceM1x4">The source M1X4.</param>
        /// <param name="sourceM1x5">The source M1X5.</param>
        /// <param name="sourceM2x0">The source M2X0.</param>
        /// <param name="sourceM2x1">The source M2X1.</param>
        /// <param name="sourceM2x2">The source M2X2.</param>
        /// <param name="sourceM2x3">The source M2X3.</param>
        /// <param name="sourceM2x4">The source M2X4.</param>
        /// <param name="sourceM2x5">The source M2X5.</param>
        /// <param name="sourceM3x0">The source M3X0.</param>
        /// <param name="sourceM3x1">The source M3X1.</param>
        /// <param name="sourceM3x2">The source M3X2.</param>
        /// <param name="sourceM3x3">The source M3X3.</param>
        /// <param name="sourceM3x4">The source M3X4.</param>
        /// <param name="sourceM3x5">The source M3X5.</param>
        /// <param name="sourceM4x0">The source M4X0.</param>
        /// <param name="sourceM4x1">The source M4X1.</param>
        /// <param name="sourceM4x2">The source M4X2.</param>
        /// <param name="sourceM4x3">The source M4X3.</param>
        /// <param name="sourceM4x4">The source M4X4.</param>
        /// <param name="sourceM4x5">The source M4X5.</param>
        /// <param name="sourceM5x0">The source M5X0.</param>
        /// <param name="sourceM5x1">The source M5X1.</param>
        /// <param name="sourceM5x2">The source M5X2.</param>
        /// <param name="sourceM5x3">The source M5X3.</param>
        /// <param name="sourceM5x4">The source M5X4.</param>
        /// <param name="sourceM5x5">The source M5X5.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4, double m0x5,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x0, double m5x1, double m5x2, double m5x3, double m5x4, double m5x5
            ) UnaryAdd(
            double sourceM0x0, double sourceM0x1, double sourceM0x2, double sourceM0x3, double sourceM0x4, double sourceM0x5,
            double sourceM1x0, double sourceM1x1, double sourceM1x2, double sourceM1x3, double sourceM1x4, double sourceM1x5,
            double sourceM2x0, double sourceM2x1, double sourceM2x2, double sourceM2x3, double sourceM2x4, double sourceM2x5,
            double sourceM3x0, double sourceM3x1, double sourceM3x2, double sourceM3x3, double sourceM3x4, double sourceM3x5,
            double sourceM4x0, double sourceM4x1, double sourceM4x2, double sourceM4x3, double sourceM4x4, double sourceM4x5,
            double sourceM5x0, double sourceM5x1, double sourceM5x2, double sourceM5x3, double sourceM5x4, double sourceM5x5)
            => (+sourceM0x0, +sourceM0x1, +sourceM0x2, +sourceM0x3, +sourceM0x4, +sourceM0x5,
                +sourceM1x0, +sourceM1x1, +sourceM1x2, +sourceM1x3, +sourceM1x4, +sourceM1x5,
                +sourceM2x0, +sourceM2x1, +sourceM2x2, +sourceM2x3, +sourceM2x4, +sourceM2x5,
                +sourceM3x0, +sourceM3x1, +sourceM3x2, +sourceM3x3, +sourceM3x4, +sourceM3x5,
                +sourceM4x0, +sourceM4x1, +sourceM4x2, +sourceM4x3, +sourceM4x4, +sourceM4x5,
                +sourceM5x0, +sourceM5x1, +sourceM5x2, +sourceM5x3, +sourceM5x4, +sourceM5x5);
        #endregion

        #region Add
        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="augendX">The augend x.</param>
        /// <param name="augendY">The augend y.</param>
        /// <param name="augendSkewX">The augend skew x.</param>
        /// <param name="augendSkewY">The augend skew y.</param>
        /// <param name="augendScaleX">The augend scale x.</param>
        /// <param name="augendScaleY">The augend scale y.</param>
        /// <param name="addendX">The addend x.</param>
        /// <param name="addendY">The addend y.</param>
        /// <param name="addendSkewX">The addend skew x.</param>
        /// <param name="addendSkewY">The addend skew y.</param>
        /// <param name="addendScaleX">The addend scale x.</param>
        /// <param name="addendScaleY">The addend scale y.</param>
        /// <returns>
        /// The <see cref="Transform2D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double X, double Y,
            double SkewX, double SkewY,
            double ScaleX, double ScaleY
            ) AddTransformMatrix(
            double augendX, double augendY,
            double augendSkewX, double augendSkewY,
            double augendScaleX, double augendScaleY,
            double addendX, double addendY,
            double addendSkewX, double addendSkewY,
            double addendScaleX, double addendScaleY) => (augendX + addendX, augendY + addendY, augendSkewX + addendSkewX, augendSkewY + addendSkewY, augendScaleX * addendScaleX, augendScaleY * addendScaleY);

        /// <summary>
        /// Used to add two matrices together.
        /// </summary>
        /// <param name="augendM0x0">The augend M0X0.</param>
        /// <param name="augendM0x1">The augend M0X1.</param>
        /// <param name="augendM1x0">The augend M1X0.</param>
        /// <param name="augendM1x1">The augend M1X1.</param>
        /// <param name="addendM0x0">The addend M0X0.</param>
        /// <param name="addendM0x1">The addend M0X1.</param>
        /// <param name="addendM1x0">The addend M1X0.</param>
        /// <param name="addendM1x1">The addend M1X1.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1,
            double m1x0, double m1x1
            ) AddMatrix(
            double augendM0x0, double augendM0x1,
            double augendM1x0, double augendM1x1,
            double addendM0x0, double addendM0x1,
            double addendM1x0, double addendM1x1)
            => (augendM0x0 + addendM0x0, augendM0x1 + addendM0x1,
                augendM1x0 + addendM1x0, augendM1x1 + addendM1x1);

        /// <summary>
        /// Used to add two matrices together.
        /// </summary>
        /// <param name="augendM0x0">The augend M0X0.</param>
        /// <param name="augendM0x1">The augend M0X1.</param>
        /// <param name="augendM0x2">The augend M0X2.</param>
        /// <param name="augendM1x0">The augend M1X0.</param>
        /// <param name="augendM1x1">The augend M1X1.</param>
        /// <param name="augendM1x2">The augend M1X2.</param>
        /// <param name="augendM2x0">The augend M2X0.</param>
        /// <param name="augendM2x1">The augend M2X1.</param>
        /// <param name="augendM2x2">The augend M2X2.</param>
        /// <param name="addendM0x0">The addend M0X0.</param>
        /// <param name="addendM0x1">The addend M0X1.</param>
        /// <param name="addendM0x2">The addend M0X2.</param>
        /// <param name="addendM1x0">The addend M1X0.</param>
        /// <param name="addendM1x1">The addend M1X1.</param>
        /// <param name="addendM1x2">The addend M1X2.</param>
        /// <param name="addendM2x0">The addend M2X0.</param>
        /// <param name="addendM2x1">The addend M2X1.</param>
        /// <param name="addendM2x2">The addend M2X2.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2
            ) AddMatrix(
            double augendM0x0, double augendM0x1, double augendM0x2,
            double augendM1x0, double augendM1x1, double augendM1x2,
            double augendM2x0, double augendM2x1, double augendM2x2,
            double addendM0x0, double addendM0x1, double addendM0x2,
            double addendM1x0, double addendM1x1, double addendM1x2,
            double addendM2x0, double addendM2x1, double addendM2x2)
            => (augendM0x0 + addendM0x0, augendM0x1 + addendM0x1, augendM0x2 + addendM0x2,
                augendM1x0 + addendM1x0, augendM1x1 + addendM1x1, augendM1x2 + addendM1x2,
                augendM2x0 + addendM2x0, augendM2x1 + addendM2x1, augendM2x2 + addendM2x2);

        /// <summary>
        /// Used to add two matrices together.
        /// </summary>
        /// <param name="augendM0x0">The augend M0X0.</param>
        /// <param name="augendM0x1">The augend M0X1.</param>
        /// <param name="augendM0x2">The augend M0X2.</param>
        /// <param name="augendM0x3">The augend M0X3.</param>
        /// <param name="augendM1x0">The augend M1X0.</param>
        /// <param name="augendM1x1">The augend M1X1.</param>
        /// <param name="augendM1x2">The augend M1X2.</param>
        /// <param name="augendM1x3">The augend M1X3.</param>
        /// <param name="augendM2x0">The augend M2X0.</param>
        /// <param name="augendM2x1">The augend M2X1.</param>
        /// <param name="augendM2x2">The augend M2X2.</param>
        /// <param name="augendM2x3">The augend M2X3.</param>
        /// <param name="augendM3x0">The augend M3X0.</param>
        /// <param name="augendM3x1">The augend M3X1.</param>
        /// <param name="augendM3x2">The augend M3X2.</param>
        /// <param name="augendM3x3">The augend M3X3.</param>
        /// <param name="addendM0x0">The addend M0X0.</param>
        /// <param name="addendM0x1">The addend M0X1.</param>
        /// <param name="addendM0x2">The addend M0X2.</param>
        /// <param name="addendM0x3">The addend M0X3.</param>
        /// <param name="addendM1x0">The addend M1X0.</param>
        /// <param name="addendM1x1">The addend M1X1.</param>
        /// <param name="addendM1x2">The addend M1X2.</param>
        /// <param name="addendM1x3">The addend M1X3.</param>
        /// <param name="addendM2x0">The addend M2X0.</param>
        /// <param name="addendM2x1">The addend M2X1.</param>
        /// <param name="addendM2x2">The addend M2X2.</param>
        /// <param name="addendM2x3">The addend M2X3.</param>
        /// <param name="addendM3x0">The addend M3X0.</param>
        /// <param name="addendM3x1">The addend M3X1.</param>
        /// <param name="addendM3x2">The addend M3X2.</param>
        /// <param name="addendM3x3">The addend M3X3.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3,
            double m1x0, double m1x1, double m1x2, double m1x3,
            double m2x0, double m2x1, double m2x2, double m2x3,
            double m3x0, double m3x1, double m3x2, double m3x3
            ) AddMatrix(
            double augendM0x0, double augendM0x1, double augendM0x2, double augendM0x3,
            double augendM1x0, double augendM1x1, double augendM1x2, double augendM1x3,
            double augendM2x0, double augendM2x1, double augendM2x2, double augendM2x3,
            double augendM3x0, double augendM3x1, double augendM3x2, double augendM3x3,
            double addendM0x0, double addendM0x1, double addendM0x2, double addendM0x3,
            double addendM1x0, double addendM1x1, double addendM1x2, double addendM1x3,
            double addendM2x0, double addendM2x1, double addendM2x2, double addendM2x3,
            double addendM3x0, double addendM3x1, double addendM3x2, double addendM3x3)
            => (augendM0x0 + addendM0x0, augendM0x1 + addendM0x1, augendM0x2 + addendM0x2, augendM0x3 + addendM0x3,
                augendM1x0 + addendM1x0, augendM1x1 + addendM1x1, augendM1x2 + addendM1x2, augendM1x3 + addendM1x3,
                augendM2x0 + addendM2x0, augendM2x1 + addendM2x1, augendM2x2 + addendM2x2, augendM2x3 + addendM2x3,
                augendM3x0 + addendM3x0, augendM3x1 + addendM3x1, augendM3x2 + addendM3x2, augendM3x3 + addendM3x3);

        /// <summary>
        /// Used to add two matrices together.
        /// </summary>
        /// <param name="augendM0x0">The augend M0X0.</param>
        /// <param name="augendM0x1">The augend M0X1.</param>
        /// <param name="augendM0x2">The augend M0X2.</param>
        /// <param name="augendM0x3">The augend M0X3.</param>
        /// <param name="augendM0x4">The augend M0X4.</param>
        /// <param name="augendM1x0">The augend M1X0.</param>
        /// <param name="augendM1x1">The augend M1X1.</param>
        /// <param name="augendM1x2">The augend M1X2.</param>
        /// <param name="augendM1x3">The augend M1X3.</param>
        /// <param name="augendM1x4">The augend M1X4.</param>
        /// <param name="augendM2x0">The augend M2X0.</param>
        /// <param name="augendM2x1">The augend M2X1.</param>
        /// <param name="augendM2x2">The augend M2X2.</param>
        /// <param name="augendM2x3">The augend M2X3.</param>
        /// <param name="augendM2x4">The augend M2X4.</param>
        /// <param name="augendM3x0">The augend M3X0.</param>
        /// <param name="augendM3x1">The augend M3X1.</param>
        /// <param name="augendM3x2">The augend M3X2.</param>
        /// <param name="augendM3x3">The augend M3X3.</param>
        /// <param name="augendM3x4">The augend M3X4.</param>
        /// <param name="augendM4x0">The augend M4X0.</param>
        /// <param name="augendM4x1">The augend M4X1.</param>
        /// <param name="augendM4x2">The augend M4X2.</param>
        /// <param name="augendM4x3">The augend M4X3.</param>
        /// <param name="augendM4x4">The augend M4X4.</param>
        /// <param name="addendM0x0">The addend M0X0.</param>
        /// <param name="addendM0x1">The addend M0X1.</param>
        /// <param name="addendM0x2">The addend M0X2.</param>
        /// <param name="addendM0x3">The addend M0X3.</param>
        /// <param name="addendM0x4">The addend M0X4.</param>
        /// <param name="addendM1x0">The addend M1X0.</param>
        /// <param name="addendM1x1">The addend M1X1.</param>
        /// <param name="addendM1x2">The addend M1X2.</param>
        /// <param name="addendM1x3">The addend M1X3.</param>
        /// <param name="addendM1x4">The addend M1X4.</param>
        /// <param name="addendM2x0">The addend M2X0.</param>
        /// <param name="addendM2x1">The addend M2X1.</param>
        /// <param name="addendM2x2">The addend M2X2.</param>
        /// <param name="addendM2x3">The addend M2X3.</param>
        /// <param name="addendM2x4">The addend M2X4.</param>
        /// <param name="addendM3x0">The addend M3X0.</param>
        /// <param name="addendM3x1">The addend M3X1.</param>
        /// <param name="addendM3x2">The addend M3X2.</param>
        /// <param name="addendM3x3">The addend M3X3.</param>
        /// <param name="addendM3x4">The addend M3X4.</param>
        /// <param name="addendM4x0">The addend M4X0.</param>
        /// <param name="addendM4x1">The addend M4X1.</param>
        /// <param name="addendM4x2">The addend M4X2.</param>
        /// <param name="addendM4x3">The addend M4X3.</param>
        /// <param name="addendM4x4">The addend M4X4.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4
            ) AddMatrix(
            double augendM0x0, double augendM0x1, double augendM0x2, double augendM0x3, double augendM0x4,
            double augendM1x0, double augendM1x1, double augendM1x2, double augendM1x3, double augendM1x4,
            double augendM2x0, double augendM2x1, double augendM2x2, double augendM2x3, double augendM2x4,
            double augendM3x0, double augendM3x1, double augendM3x2, double augendM3x3, double augendM3x4,
            double augendM4x0, double augendM4x1, double augendM4x2, double augendM4x3, double augendM4x4,
            double addendM0x0, double addendM0x1, double addendM0x2, double addendM0x3, double addendM0x4,
            double addendM1x0, double addendM1x1, double addendM1x2, double addendM1x3, double addendM1x4,
            double addendM2x0, double addendM2x1, double addendM2x2, double addendM2x3, double addendM2x4,
            double addendM3x0, double addendM3x1, double addendM3x2, double addendM3x3, double addendM3x4,
            double addendM4x0, double addendM4x1, double addendM4x2, double addendM4x3, double addendM4x4)
            => (augendM0x0 + addendM0x0, augendM0x1 + addendM0x1, augendM0x2 + addendM0x2, augendM0x3 + addendM0x3, augendM0x4 + addendM0x4,
                augendM1x0 + addendM1x0, augendM1x1 + addendM1x1, augendM1x2 + addendM1x2, augendM1x3 + addendM1x3, augendM1x4 + addendM1x4,
                augendM2x0 + addendM2x0, augendM2x1 + addendM2x1, augendM2x2 + addendM2x2, augendM2x3 + addendM2x3, augendM2x4 + addendM2x4,
                augendM3x0 + addendM3x0, augendM3x1 + addendM3x1, augendM3x2 + addendM3x2, augendM3x3 + addendM3x3, augendM3x4 + addendM3x4,
                augendM4x0 + addendM4x0, augendM4x1 + addendM4x1, augendM4x2 + addendM4x2, augendM4x3 + addendM4x3, augendM4x4 + addendM4x4);

        /// <summary>
        /// Used to add two matrices together.
        /// </summary>
        /// <param name="augendM0x0">The augend M0X0.</param>
        /// <param name="augendM0x1">The augend M0X1.</param>
        /// <param name="augendM0x2">The augend M0X2.</param>
        /// <param name="augendM0x3">The augend M0X3.</param>
        /// <param name="augendM0x4">The augend M0X4.</param>
        /// <param name="augendM0x5">The augend M0X5.</param>
        /// <param name="augendM1x0">The augend M1X0.</param>
        /// <param name="augendM1x1">The augend M1X1.</param>
        /// <param name="augendM1x2">The augend M1X2.</param>
        /// <param name="augendM1x3">The augend M1X3.</param>
        /// <param name="augendM1x4">The augend M1X4.</param>
        /// <param name="augendM1x5">The augend M1X5.</param>
        /// <param name="augendM2x0">The augend M2X0.</param>
        /// <param name="augendM2x1">The augend M2X1.</param>
        /// <param name="augendM2x2">The augend M2X2.</param>
        /// <param name="augendM2x3">The augend M2X3.</param>
        /// <param name="augendM2x4">The augend M2X4.</param>
        /// <param name="augendM2x5">The augend M2X5.</param>
        /// <param name="augendM3x0">The augend M3X0.</param>
        /// <param name="augendM3x1">The augend M3X1.</param>
        /// <param name="augendM3x2">The augend M3X2.</param>
        /// <param name="augendM3x3">The augend M3X3.</param>
        /// <param name="augendM3x4">The augend M3X4.</param>
        /// <param name="augendM3x5">The augend M3X5.</param>
        /// <param name="augendM4x0">The augend M4X0.</param>
        /// <param name="augendM4x1">The augend M4X1.</param>
        /// <param name="augendM4x2">The augend M4X2.</param>
        /// <param name="augendM4x3">The augend M4X3.</param>
        /// <param name="augendM4x4">The augend M4X4.</param>
        /// <param name="augendM4x5">The augend M4X5.</param>
        /// <param name="augendM5x0">The augend M5X0.</param>
        /// <param name="augendM5x1">The augend M5X1.</param>
        /// <param name="augendM5x2">The augend M5X2.</param>
        /// <param name="augendM5x3">The augend M5X3.</param>
        /// <param name="augendM5x4">The augend M5X4.</param>
        /// <param name="augendM5x5">The augend M5X5.</param>
        /// <param name="addendM0x0">The addend M0X0.</param>
        /// <param name="addendM0x1">The addend M0X1.</param>
        /// <param name="addendM0x2">The addend M0X2.</param>
        /// <param name="addendM0x3">The addend M0X3.</param>
        /// <param name="addendM0x4">The addend M0X4.</param>
        /// <param name="addendM0x5">The addend M0X5.</param>
        /// <param name="addendM1x0">The addend M1X0.</param>
        /// <param name="addendM1x1">The addend M1X1.</param>
        /// <param name="addendM1x2">The addend M1X2.</param>
        /// <param name="addendM1x3">The addend M1X3.</param>
        /// <param name="addendM1x4">The addend M1X4.</param>
        /// <param name="addendM1x5">The addend M1X5.</param>
        /// <param name="addendM2x0">The addend M2X0.</param>
        /// <param name="addendM2x1">The addend M2X1.</param>
        /// <param name="addendM2x2">The addend M2X2.</param>
        /// <param name="addendM2x3">The addend M2X3.</param>
        /// <param name="addendM2x4">The addend M2X4.</param>
        /// <param name="addendM2x5">The addend M2X5.</param>
        /// <param name="addendM3x0">The addend M3X0.</param>
        /// <param name="addendM3x1">The addend M3X1.</param>
        /// <param name="addendM3x2">The addend M3X2.</param>
        /// <param name="addendM3x3">The addend M3X3.</param>
        /// <param name="addendM3x4">The addend M3X4.</param>
        /// <param name="addendM3x5">The addend M3X5.</param>
        /// <param name="addendM4x0">The addend M4X0.</param>
        /// <param name="addendM4x1">The addend M4X1.</param>
        /// <param name="addendM4x2">The addend M4X2.</param>
        /// <param name="addendM4x3">The addend M4X3.</param>
        /// <param name="addendM4x4">The addend M4X4.</param>
        /// <param name="addendM4x5">The addend M4X5.</param>
        /// <param name="addendM5x0">The addend M5X0.</param>
        /// <param name="addendM5x1">The addend M5X1.</param>
        /// <param name="addendM5x2">The addend M5X2.</param>
        /// <param name="addendM5x3">The addend M5X3.</param>
        /// <param name="addendM5x4">The addend M5X4.</param>
        /// <param name="addendM5x5">The addend M5X5.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4, double m0x5,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x0, double m5x1, double m5x2, double m5x3, double m5x4, double m5x5
            ) AddMatrix(
            double augendM0x0, double augendM0x1, double augendM0x2, double augendM0x3, double augendM0x4, double augendM0x5,
            double augendM1x0, double augendM1x1, double augendM1x2, double augendM1x3, double augendM1x4, double augendM1x5,
            double augendM2x0, double augendM2x1, double augendM2x2, double augendM2x3, double augendM2x4, double augendM2x5,
            double augendM3x0, double augendM3x1, double augendM3x2, double augendM3x3, double augendM3x4, double augendM3x5,
            double augendM4x0, double augendM4x1, double augendM4x2, double augendM4x3, double augendM4x4, double augendM4x5,
            double augendM5x0, double augendM5x1, double augendM5x2, double augendM5x3, double augendM5x4, double augendM5x5,
            double addendM0x0, double addendM0x1, double addendM0x2, double addendM0x3, double addendM0x4, double addendM0x5,
            double addendM1x0, double addendM1x1, double addendM1x2, double addendM1x3, double addendM1x4, double addendM1x5,
            double addendM2x0, double addendM2x1, double addendM2x2, double addendM2x3, double addendM2x4, double addendM2x5,
            double addendM3x0, double addendM3x1, double addendM3x2, double addendM3x3, double addendM3x4, double addendM3x5,
            double addendM4x0, double addendM4x1, double addendM4x2, double addendM4x3, double addendM4x4, double addendM4x5,
            double addendM5x0, double addendM5x1, double addendM5x2, double addendM5x3, double addendM5x4, double addendM5x5)
            => (augendM0x0 + addendM0x0, augendM0x1 + addendM0x1, augendM0x2 + addendM0x2, augendM0x3 + addendM0x3, augendM0x4 + addendM0x4, augendM0x5 + addendM0x5,
                augendM1x0 + addendM1x0, augendM1x1 + addendM1x1, augendM1x2 + addendM1x2, augendM1x3 + addendM1x3, augendM1x4 + addendM1x4, augendM1x5 + addendM1x5,
                augendM2x0 + addendM2x0, augendM2x1 + addendM2x1, augendM2x2 + addendM2x2, augendM2x3 + addendM2x3, augendM2x4 + addendM2x4, augendM2x5 + addendM2x5,
                augendM3x0 + addendM3x0, augendM3x1 + addendM3x1, augendM3x2 + addendM3x2, augendM3x3 + addendM3x3, augendM3x4 + addendM3x4, augendM3x5 + addendM3x5,
                augendM4x0 + addendM4x0, augendM4x1 + addendM4x1, augendM4x2 + addendM4x2, augendM4x3 + addendM4x3, augendM4x4 + addendM4x4, augendM4x5 + addendM4x5,
                augendM5x0 + addendM5x0, augendM5x1 + addendM5x1, augendM5x2 + addendM5x2, augendM5x3 + addendM5x3, augendM5x4 + addendM5x4, augendM5x5 + addendM5x5);
        #endregion Add

        #region Unary Negate
        /// <summary>
        /// Negates a <see cref="Matrix2x2D" />.
        /// </summary>
        /// <param name="sourceM0x0">The source M0X0.</param>
        /// <param name="sourceM0x1">The source M0X1.</param>
        /// <param name="sourceM1x0">The source M1X0.</param>
        /// <param name="sourceM1x1">The source M1X1.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1,
            double m1x0, double m1x1
            ) Negate(
            double sourceM0x0, double sourceM0x1,
            double sourceM1x0, double sourceM1x1)
            => (-sourceM0x0, -sourceM0x1,
                -sourceM1x0, -sourceM1x1);

        /// <summary>
        /// Negates a <see cref="Matrix3x3D" />.
        /// </summary>
        /// <param name="sourceM0x0">The source M0X0.</param>
        /// <param name="sourceM0x1">The source M0X1.</param>
        /// <param name="sourceM0x2">The source M0X2.</param>
        /// <param name="sourceM1x0">The source M1X0.</param>
        /// <param name="sourceM1x1">The source M1X1.</param>
        /// <param name="sourceM1x2">The source M1X2.</param>
        /// <param name="sourceM2x0">The source M2X0.</param>
        /// <param name="sourceM2x1">The source M2X1.</param>
        /// <param name="sourceM2x2">The source M2X2.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2
            ) Negate(
            double sourceM0x0, double sourceM0x1, double sourceM0x2,
            double sourceM1x0, double sourceM1x1, double sourceM1x2,
            double sourceM2x0, double sourceM2x1, double sourceM2x2)
            => (-sourceM0x0, -sourceM0x1, -sourceM0x2,
                -sourceM1x0, -sourceM1x1, -sourceM1x2,
                -sourceM2x0, -sourceM2x1, -sourceM2x2);

        /// <summary>
        /// Negates a <see cref="Matrix4x4D" />.
        /// </summary>
        /// <param name="sourceM0x0">The source M0X0.</param>
        /// <param name="sourceM0x1">The source M0X1.</param>
        /// <param name="sourceM0x2">The source M0X2.</param>
        /// <param name="sourceM0x3">The source M0X3.</param>
        /// <param name="sourceM1x0">The source M1X0.</param>
        /// <param name="sourceM1x1">The source M1X1.</param>
        /// <param name="sourceM1x2">The source M1X2.</param>
        /// <param name="sourceM1x3">The source M1X3.</param>
        /// <param name="sourceM2x0">The source M2X0.</param>
        /// <param name="sourceM2x1">The source M2X1.</param>
        /// <param name="sourceM2x2">The source M2X2.</param>
        /// <param name="sourceM2x3">The source M2X3.</param>
        /// <param name="sourceM3x0">The source M3X0.</param>
        /// <param name="sourceM3x1">The source M3X1.</param>
        /// <param name="sourceM3x2">The source M3X2.</param>
        /// <param name="sourceM3x3">The source M3X3.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3,
            double m1x0, double m1x1, double m1x2, double m1x3,
            double m2x0, double m2x1, double m2x2, double m2x3,
            double m3x0, double m3x1, double m3x2, double m3x3
            ) Negate(
            double sourceM0x0, double sourceM0x1, double sourceM0x2, double sourceM0x3,
            double sourceM1x0, double sourceM1x1, double sourceM1x2, double sourceM1x3,
            double sourceM2x0, double sourceM2x1, double sourceM2x2, double sourceM2x3,
            double sourceM3x0, double sourceM3x1, double sourceM3x2, double sourceM3x3)
            => (-sourceM0x0, -sourceM0x1, -sourceM0x2, -sourceM0x3,
                -sourceM1x0, -sourceM1x1, -sourceM1x2, -sourceM1x3,
                -sourceM2x0, -sourceM2x1, -sourceM2x2, -sourceM2x3,
                -sourceM3x0, -sourceM3x1, -sourceM3x2, -sourceM3x3);

        /// <summary>
        /// Negates a Matrix5x5D.
        /// </summary>
        /// <param name="sourceM0x0">The source M0X0.</param>
        /// <param name="sourceM0x1">The source M0X1.</param>
        /// <param name="sourceM0x2">The source M0X2.</param>
        /// <param name="sourceM0x3">The source M0X3.</param>
        /// <param name="sourceM0x4">The source M0X4.</param>
        /// <param name="sourceM1x0">The source M1X0.</param>
        /// <param name="sourceM1x1">The source M1X1.</param>
        /// <param name="sourceM1x2">The source M1X2.</param>
        /// <param name="sourceM1x3">The source M1X3.</param>
        /// <param name="sourceM1x4">The source M1X4.</param>
        /// <param name="sourceM2x0">The source M2X0.</param>
        /// <param name="sourceM2x1">The source M2X1.</param>
        /// <param name="sourceM2x2">The source M2X2.</param>
        /// <param name="sourceM2x3">The source M2X3.</param>
        /// <param name="sourceM2x4">The source M2X4.</param>
        /// <param name="sourceM3x0">The source M3X0.</param>
        /// <param name="sourceM3x1">The source M3X1.</param>
        /// <param name="sourceM3x2">The source M3X2.</param>
        /// <param name="sourceM3x3">The source M3X3.</param>
        /// <param name="sourceM3x4">The source M3X4.</param>
        /// <param name="sourceM4x0">The source M4X0.</param>
        /// <param name="sourceM4x1">The source M4X1.</param>
        /// <param name="sourceM4x2">The source M4X2.</param>
        /// <param name="sourceM4x3">The source M4X3.</param>
        /// <param name="sourceM4x4">The source M4X4.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4
            ) Negate(
            double sourceM0x0, double sourceM0x1, double sourceM0x2, double sourceM0x3, double sourceM0x4,
            double sourceM1x0, double sourceM1x1, double sourceM1x2, double sourceM1x3, double sourceM1x4,
            double sourceM2x0, double sourceM2x1, double sourceM2x2, double sourceM2x3, double sourceM2x4,
            double sourceM3x0, double sourceM3x1, double sourceM3x2, double sourceM3x3, double sourceM3x4,
            double sourceM4x0, double sourceM4x1, double sourceM4x2, double sourceM4x3, double sourceM4x4)
            => (-sourceM0x0, -sourceM0x1, -sourceM0x2, -sourceM0x3, -sourceM0x4,
                -sourceM1x0, -sourceM1x1, -sourceM1x2, -sourceM1x3, -sourceM1x4,
                -sourceM2x0, -sourceM2x1, -sourceM2x2, -sourceM2x3, -sourceM2x4,
                -sourceM3x0, -sourceM3x1, -sourceM3x2, -sourceM3x3, -sourceM3x4,
                -sourceM4x0, -sourceM4x1, -sourceM4x2, -sourceM4x3, -sourceM4x4);

        /// <summary>
        /// Negates a Matrix6x6D.
        /// </summary>
        /// <param name="sourceM0x0">The source M0X0.</param>
        /// <param name="sourceM0x1">The source M0X1.</param>
        /// <param name="sourceM0x2">The source M0X2.</param>
        /// <param name="sourceM0x3">The source M0X3.</param>
        /// <param name="sourceM0x4">The source M0X4.</param>
        /// <param name="sourceM0x5">The source M0X5.</param>
        /// <param name="sourceM1x0">The source M1X0.</param>
        /// <param name="sourceM1x1">The source M1X1.</param>
        /// <param name="sourceM1x2">The source M1X2.</param>
        /// <param name="sourceM1x3">The source M1X3.</param>
        /// <param name="sourceM1x4">The source M1X4.</param>
        /// <param name="sourceM1x5">The source M1X5.</param>
        /// <param name="sourceM2x0">The source M2X0.</param>
        /// <param name="sourceM2x1">The source M2X1.</param>
        /// <param name="sourceM2x2">The source M2X2.</param>
        /// <param name="sourceM2x3">The source M2X3.</param>
        /// <param name="sourceM2x4">The source M2X4.</param>
        /// <param name="sourceM2x5">The source M2X5.</param>
        /// <param name="sourceM3x0">The source M3X0.</param>
        /// <param name="sourceM3x1">The source M3X1.</param>
        /// <param name="sourceM3x2">The source M3X2.</param>
        /// <param name="sourceM3x3">The source M3X3.</param>
        /// <param name="sourceM3x4">The source M3X4.</param>
        /// <param name="sourceM3x5">The source M3X5.</param>
        /// <param name="sourceM4x0">The source M4X0.</param>
        /// <param name="sourceM4x1">The source M4X1.</param>
        /// <param name="sourceM4x2">The source M4X2.</param>
        /// <param name="sourceM4x3">The source M4X3.</param>
        /// <param name="sourceM4x4">The source M4X4.</param>
        /// <param name="sourceM4x5">The source M4X5.</param>
        /// <param name="sourceM5x0">The source M5X0.</param>
        /// <param name="sourceM5x1">The source M5X1.</param>
        /// <param name="sourceM5x2">The source M5X2.</param>
        /// <param name="sourceM5x3">The source M5X3.</param>
        /// <param name="sourceM5x4">The source M5X4.</param>
        /// <param name="sourceM5x5">The source M5X5.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4, double m0x5,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x0, double m5x1, double m5x2, double m5x3, double m5x4, double m5x5
            ) Negate(
            double sourceM0x0, double sourceM0x1, double sourceM0x2, double sourceM0x3, double sourceM0x4, double sourceM0x5,
            double sourceM1x0, double sourceM1x1, double sourceM1x2, double sourceM1x3, double sourceM1x4, double sourceM1x5,
            double sourceM2x0, double sourceM2x1, double sourceM2x2, double sourceM2x3, double sourceM2x4, double sourceM2x5,
            double sourceM3x0, double sourceM3x1, double sourceM3x2, double sourceM3x3, double sourceM3x4, double sourceM3x5,
            double sourceM4x0, double sourceM4x1, double sourceM4x2, double sourceM4x3, double sourceM4x4, double sourceM4x5,
            double sourceM5x0, double sourceM5x1, double sourceM5x2, double sourceM5x3, double sourceM5x4, double sourceM5x5)
            => (-sourceM0x0, -sourceM0x1, -sourceM0x2, -sourceM0x3, -sourceM0x4, -sourceM0x5,
                -sourceM1x0, -sourceM1x1, -sourceM1x2, -sourceM1x3, -sourceM1x4, -sourceM1x5,
                -sourceM2x0, -sourceM2x1, -sourceM2x2, -sourceM2x3, -sourceM2x4, -sourceM2x5,
                -sourceM3x0, -sourceM3x1, -sourceM3x2, -sourceM3x3, -sourceM3x4, -sourceM3x5,
                -sourceM4x0, -sourceM4x1, -sourceM4x2, -sourceM4x3, -sourceM4x4, -sourceM4x5,
                -sourceM5x0, -sourceM5x1, -sourceM5x2, -sourceM5x3, -sourceM5x4, -sourceM5x5);
        #endregion Unary Negate

        #region Subtract
        /// <summary>
        /// Subtracts the transform matrix.
        /// </summary>
        /// <param name="minuendX">The minuend x.</param>
        /// <param name="minuendY">The minuend y.</param>
        /// <param name="minuendSkewX">The minuend skew x.</param>
        /// <param name="minuendSkewY">The minuend skew y.</param>
        /// <param name="minuendScaleX">The minuend scale x.</param>
        /// <param name="minuendScaleY">The minuend scale y.</param>
        /// <param name="subtrahendX">The subtrahend x.</param>
        /// <param name="subtrahendY">The subtrahend y.</param>
        /// <param name="subtrahendSkewX">The subtrahend skew x.</param>
        /// <param name="subtrahendSkewY">The subtrahend skew y.</param>
        /// <param name="subtrahendScaleX">The subtrahend scale x.</param>
        /// <param name="subtrahendScaleY">The subtrahend scale y.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double X, double Y,
            double SkewX, double SkewY,
            double ScaleX, double ScaleY
            ) SubtractTransformMatrix(
            double minuendX, double minuendY,
            double minuendSkewX, double minuendSkewY,
            double minuendScaleX, double minuendScaleY,
            double subtrahendX, double subtrahendY,
            double subtrahendSkewX, double subtrahendSkewY,
            double subtrahendScaleX, double subtrahendScaleY)
            => (
            minuendX - subtrahendX, minuendY - subtrahendY,
            NormalizeRadian(minuendSkewX - subtrahendSkewX), NormalizeRadian(minuendSkewY - subtrahendSkewY),
            minuendScaleX / subtrahendScaleX, minuendScaleY / subtrahendScaleY);

        /// <summary>
        /// Used to subtract two matrices.
        /// </summary>
        /// <param name="minuendM0x0">The minuend M0X0.</param>
        /// <param name="minuendM0x1">The minuend M0X1.</param>
        /// <param name="minuendM1x0">The minuend M1X0.</param>
        /// <param name="minuendM1x1">The minuend M1X1.</param>
        /// <param name="subtrahendM0x0">The subtrahend M0X0.</param>
        /// <param name="subtrahendM0x1">The subtrahend M0X1.</param>
        /// <param name="subtrahendM1x0">The subtrahend M1X0.</param>
        /// <param name="subtrahendM1x1">The subtrahend M1X1.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1,
            double m1x0, double m1x1
            ) Subtract2x2x2x2(
            double minuendM0x0, double minuendM0x1,
            double minuendM1x0, double minuendM1x1,
            double subtrahendM0x0, double subtrahendM0x1,
            double subtrahendM1x0, double subtrahendM1x1)
            => (minuendM0x0 - subtrahendM0x0, minuendM0x1 - subtrahendM0x1,
                minuendM1x0 - subtrahendM1x0, minuendM1x1 - subtrahendM1x1);

        /// <summary>
        /// Used to subtract two matrices.
        /// </summary>
        /// <param name="minuendM0x0">The minuend M0X0.</param>
        /// <param name="minuendM0x1">The minuend M0X1.</param>
        /// <param name="minuendM0x2">The minuend M0X2.</param>
        /// <param name="minuendM1x0">The minuend M1X0.</param>
        /// <param name="minuendM1x1">The minuend M1X1.</param>
        /// <param name="minuendM1x2">The minuend M1X2.</param>
        /// <param name="minuendM2x0">The minuend M2X0.</param>
        /// <param name="minuendM2x1">The minuend M2X1.</param>
        /// <param name="minuendM2x2">The minuend M2X2.</param>
        /// <param name="subtrahendM0x0">The subtrahend M0X0.</param>
        /// <param name="subtrahendM0x1">The subtrahend M0X1.</param>
        /// <param name="subtrahendM0x2">The subtrahend M0X2.</param>
        /// <param name="subtrahendM1x0">The subtrahend M1X0.</param>
        /// <param name="subtrahendM1x1">The subtrahend M1X1.</param>
        /// <param name="subtrahendM1x2">The subtrahend M1X2.</param>
        /// <param name="subtrahendM2x0">The subtrahend M2X0.</param>
        /// <param name="subtrahendM2x1">The subtrahend M2X1.</param>
        /// <param name="subtrahendM2x2">The subtrahend M2X2.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2
            ) Subtract3x3x3x3(
            double minuendM0x0, double minuendM0x1, double minuendM0x2,
            double minuendM1x0, double minuendM1x1, double minuendM1x2,
            double minuendM2x0, double minuendM2x1, double minuendM2x2,
            double subtrahendM0x0, double subtrahendM0x1, double subtrahendM0x2,
            double subtrahendM1x0, double subtrahendM1x1, double subtrahendM1x2,
            double subtrahendM2x0, double subtrahendM2x1, double subtrahendM2x2)
            => (minuendM0x0 - subtrahendM0x0, minuendM0x1 - subtrahendM0x1, minuendM0x2 - subtrahendM0x2,
                minuendM1x0 - subtrahendM1x0, minuendM1x1 - subtrahendM1x1, minuendM1x2 - subtrahendM1x2,
                minuendM2x0 - subtrahendM2x0, minuendM2x1 - subtrahendM2x1, minuendM2x2 - subtrahendM2x2);

        /// <summary>
        /// Used to subtract two matrices.
        /// </summary>
        /// <param name="minuendM0x0">The minuend M0X0.</param>
        /// <param name="minuendM0x1">The minuend M0X1.</param>
        /// <param name="minuendM0x2">The minuend M0X2.</param>
        /// <param name="minuendM0x3">The minuend M0X3.</param>
        /// <param name="minuendM1x0">The minuend M1X0.</param>
        /// <param name="minuendM1x1">The minuend M1X1.</param>
        /// <param name="minuendM1x2">The minuend M1X2.</param>
        /// <param name="minuendM1x3">The minuend M1X3.</param>
        /// <param name="minuendM2x0">The minuend M2X0.</param>
        /// <param name="minuendM2x1">The minuend M2X1.</param>
        /// <param name="minuendM2x2">The minuend M2X2.</param>
        /// <param name="minuendM2x3">The minuend M2X3.</param>
        /// <param name="minuendM3x0">The minuend M3X0.</param>
        /// <param name="minuendM3x1">The minuend M3X1.</param>
        /// <param name="minuendM3x2">The minuend M3X2.</param>
        /// <param name="minuendM3x3">The minuend M3X3.</param>
        /// <param name="subtrahendM0x0">The subtrahend M0X0.</param>
        /// <param name="subtrahendM0x1">The subtrahend M0X1.</param>
        /// <param name="subtrahendM0x2">The subtrahend M0X2.</param>
        /// <param name="subtrahendM0x3">The subtrahend M0X3.</param>
        /// <param name="subtrahendM1x0">The subtrahend M1X0.</param>
        /// <param name="subtrahendM1x1">The subtrahend M1X1.</param>
        /// <param name="subtrahendM1x2">The subtrahend M1X2.</param>
        /// <param name="subtrahendM1x3">The subtrahend M1X3.</param>
        /// <param name="subtrahendM2x0">The subtrahend M2X0.</param>
        /// <param name="subtrahendM2x1">The subtrahend M2X1.</param>
        /// <param name="subtrahendM2x2">The subtrahend M2X2.</param>
        /// <param name="subtrahendM2x3">The subtrahend M2X3.</param>
        /// <param name="subtrahendM3x0">The subtrahend M3X0.</param>
        /// <param name="subtrahendM3x1">The subtrahend M3X1.</param>
        /// <param name="subtrahendM3x2">The subtrahend M3X2.</param>
        /// <param name="subtrahendM3x3">The subtrahend M3X3.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3,
            double m1x0, double m1x1, double m1x2, double m1x3,
            double m2x0, double m2x1, double m2x2, double m2x3,
            double m3x0, double m3x1, double m3x2, double m3x3
            ) Subtract4x4x4x4(
            double minuendM0x0, double minuendM0x1, double minuendM0x2, double minuendM0x3,
            double minuendM1x0, double minuendM1x1, double minuendM1x2, double minuendM1x3,
            double minuendM2x0, double minuendM2x1, double minuendM2x2, double minuendM2x3,
            double minuendM3x0, double minuendM3x1, double minuendM3x2, double minuendM3x3,
            double subtrahendM0x0, double subtrahendM0x1, double subtrahendM0x2, double subtrahendM0x3,
            double subtrahendM1x0, double subtrahendM1x1, double subtrahendM1x2, double subtrahendM1x3,
            double subtrahendM2x0, double subtrahendM2x1, double subtrahendM2x2, double subtrahendM2x3,
            double subtrahendM3x0, double subtrahendM3x1, double subtrahendM3x2, double subtrahendM3x3)
            => (minuendM0x0 - subtrahendM0x0, minuendM0x1 - subtrahendM0x1, minuendM0x2 - subtrahendM0x2, minuendM0x3 - subtrahendM0x3,
                minuendM1x0 - subtrahendM1x0, minuendM1x1 - subtrahendM1x1, minuendM1x2 - subtrahendM1x2, minuendM1x3 - subtrahendM1x3,
                minuendM2x0 - subtrahendM2x0, minuendM2x1 - subtrahendM2x1, minuendM2x2 - subtrahendM2x2, minuendM2x3 - subtrahendM2x3,
                minuendM3x0 - subtrahendM3x0, minuendM3x1 - subtrahendM3x1, minuendM3x2 - subtrahendM3x2, minuendM3x3 - subtrahendM3x3);

        /// <summary>
        /// Used to subtract two matrices.
        /// </summary>
        /// <param name="minuendM0x0">The minuend M0X0.</param>
        /// <param name="minuendM0x1">The minuend M0X1.</param>
        /// <param name="minuendM0x2">The minuend M0X2.</param>
        /// <param name="minuendM0x3">The minuend M0X3.</param>
        /// <param name="minuendM0x4">The minuend M0X4.</param>
        /// <param name="minuendM1x0">The minuend M1X0.</param>
        /// <param name="minuendM1x1">The minuend M1X1.</param>
        /// <param name="minuendM1x2">The minuend M1X2.</param>
        /// <param name="minuendM1x3">The minuend M1X3.</param>
        /// <param name="minuendM1x4">The minuend M1X4.</param>
        /// <param name="minuendM2x0">The minuend M2X0.</param>
        /// <param name="minuendM2x1">The minuend M2X1.</param>
        /// <param name="minuendM2x2">The minuend M2X2.</param>
        /// <param name="minuendM2x3">The minuend M2X3.</param>
        /// <param name="minuendM2x4">The minuend M2X4.</param>
        /// <param name="minuendM3x0">The minuend M3X0.</param>
        /// <param name="minuendM3x1">The minuend M3X1.</param>
        /// <param name="minuendM3x2">The minuend M3X2.</param>
        /// <param name="minuendM3x3">The minuend M3X3.</param>
        /// <param name="minuendM3x4">The minuend M3X4.</param>
        /// <param name="minuendM4x0">The minuend M4X0.</param>
        /// <param name="minuendM4x1">The minuend M4X1.</param>
        /// <param name="minuendM4x2">The minuend M4X2.</param>
        /// <param name="minuendM4x3">The minuend M4X3.</param>
        /// <param name="minuendM4x4">The minuend M4X4.</param>
        /// <param name="subtrahendM0x0">The subtrahend M0X0.</param>
        /// <param name="subtrahendM0x1">The subtrahend M0X1.</param>
        /// <param name="subtrahendM0x2">The subtrahend M0X2.</param>
        /// <param name="subtrahendM0x3">The subtrahend M0X3.</param>
        /// <param name="subtrahendM0x4">The subtrahend M0X4.</param>
        /// <param name="subtrahendM1x0">The subtrahend M1X0.</param>
        /// <param name="subtrahendM1x1">The subtrahend M1X1.</param>
        /// <param name="subtrahendM1x2">The subtrahend M1X2.</param>
        /// <param name="subtrahendM1x3">The subtrahend M1X3.</param>
        /// <param name="subtrahendM1x4">The subtrahend M1X4.</param>
        /// <param name="subtrahendM2x0">The subtrahend M2X0.</param>
        /// <param name="subtrahendM2x1">The subtrahend M2X1.</param>
        /// <param name="subtrahendM2x2">The subtrahend M2X2.</param>
        /// <param name="subtrahendM2x3">The subtrahend M2X3.</param>
        /// <param name="subtrahendM2x4">The subtrahend M2X4.</param>
        /// <param name="subtrahendM3x0">The subtrahend M3X0.</param>
        /// <param name="subtrahendM3x1">The subtrahend M3X1.</param>
        /// <param name="subtrahendM3x2">The subtrahend M3X2.</param>
        /// <param name="subtrahendM3x3">The subtrahend M3X3.</param>
        /// <param name="subtrahendM3x4">The subtrahend M3X4.</param>
        /// <param name="subtrahendM4x0">The subtrahend M4X0.</param>
        /// <param name="subtrahendM4x1">The subtrahend M4X1.</param>
        /// <param name="subtrahendM4x2">The subtrahend M4X2.</param>
        /// <param name="subtrahendM4x3">The subtrahend M4X3.</param>
        /// <param name="subtrahendM4x4">The subtrahend M4X4.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4
            ) Subtract5x5x5x5(
            double minuendM0x0, double minuendM0x1, double minuendM0x2, double minuendM0x3, double minuendM0x4,
            double minuendM1x0, double minuendM1x1, double minuendM1x2, double minuendM1x3, double minuendM1x4,
            double minuendM2x0, double minuendM2x1, double minuendM2x2, double minuendM2x3, double minuendM2x4,
            double minuendM3x0, double minuendM3x1, double minuendM3x2, double minuendM3x3, double minuendM3x4,
            double minuendM4x0, double minuendM4x1, double minuendM4x2, double minuendM4x3, double minuendM4x4,
            double subtrahendM0x0, double subtrahendM0x1, double subtrahendM0x2, double subtrahendM0x3, double subtrahendM0x4,
            double subtrahendM1x0, double subtrahendM1x1, double subtrahendM1x2, double subtrahendM1x3, double subtrahendM1x4,
            double subtrahendM2x0, double subtrahendM2x1, double subtrahendM2x2, double subtrahendM2x3, double subtrahendM2x4,
            double subtrahendM3x0, double subtrahendM3x1, double subtrahendM3x2, double subtrahendM3x3, double subtrahendM3x4,
            double subtrahendM4x0, double subtrahendM4x1, double subtrahendM4x2, double subtrahendM4x3, double subtrahendM4x4)
            => (minuendM0x0 - subtrahendM0x0, minuendM0x1 - subtrahendM0x1, minuendM0x2 - subtrahendM0x2, minuendM0x3 - subtrahendM0x3, minuendM0x4 - subtrahendM0x4,
                minuendM1x0 - subtrahendM1x0, minuendM1x1 - subtrahendM1x1, minuendM1x2 - subtrahendM1x2, minuendM1x3 - subtrahendM1x3, minuendM1x4 - subtrahendM1x4,
                minuendM2x0 - subtrahendM2x0, minuendM2x1 - subtrahendM2x1, minuendM2x2 - subtrahendM2x2, minuendM2x3 - subtrahendM2x3, minuendM2x4 - subtrahendM2x4,
                minuendM3x0 - subtrahendM3x0, minuendM3x1 - subtrahendM3x1, minuendM3x2 - subtrahendM3x2, minuendM3x3 - subtrahendM3x3, minuendM3x4 - subtrahendM3x4,
                minuendM4x0 - subtrahendM4x0, minuendM4x1 - subtrahendM4x1, minuendM4x2 - subtrahendM4x2, minuendM4x3 - subtrahendM4x3, minuendM4x4 - subtrahendM4x4);

        /// <summary>
        /// Used to subtract two matrices.
        /// </summary>
        /// <param name="minuendM0x0">The minuend M0X0.</param>
        /// <param name="minuendM0x1">The minuend M0X1.</param>
        /// <param name="minuendM0x2">The minuend M0X2.</param>
        /// <param name="minuendM0x3">The minuend M0X3.</param>
        /// <param name="minuendM0x4">The minuend M0X4.</param>
        /// <param name="minuendM0x5">The minuend M0X5.</param>
        /// <param name="minuendM1x0">The minuend M1X0.</param>
        /// <param name="minuendM1x1">The minuend M1X1.</param>
        /// <param name="minuendM1x2">The minuend M1X2.</param>
        /// <param name="minuendM1x3">The minuend M1X3.</param>
        /// <param name="minuendM1x4">The minuend M1X4.</param>
        /// <param name="minuendM1x5">The minuend M1X5.</param>
        /// <param name="minuendM2x0">The minuend M2X0.</param>
        /// <param name="minuendM2x1">The minuend M2X1.</param>
        /// <param name="minuendM2x2">The minuend M2X2.</param>
        /// <param name="minuendM2x3">The minuend M2X3.</param>
        /// <param name="minuendM2x4">The minuend M2X4.</param>
        /// <param name="minuendM2x5">The minuend M2X5.</param>
        /// <param name="minuendM3x0">The minuend M3X0.</param>
        /// <param name="minuendM3x1">The minuend M3X1.</param>
        /// <param name="minuendM3x2">The minuend M3X2.</param>
        /// <param name="minuendM3x3">The minuend M3X3.</param>
        /// <param name="minuendM3x4">The minuend M3X4.</param>
        /// <param name="minuendM3x5">The minuend M3X5.</param>
        /// <param name="minuendM4x0">The minuend M4X0.</param>
        /// <param name="minuendM4x1">The minuend M4X1.</param>
        /// <param name="minuendM4x2">The minuend M4X2.</param>
        /// <param name="minuendM4x3">The minuend M4X3.</param>
        /// <param name="minuendM4x4">The minuend M4X4.</param>
        /// <param name="minuendM4x5">The minuend M4X5.</param>
        /// <param name="minuendM5x0">The minuend M5X0.</param>
        /// <param name="minuendM5x1">The minuend M5X1.</param>
        /// <param name="minuendM5x2">The minuend M5X2.</param>
        /// <param name="minuendM5x3">The minuend M5X3.</param>
        /// <param name="minuendM5x4">The minuend M5X4.</param>
        /// <param name="minuendM5x5">The minuend M5X5.</param>
        /// <param name="subtrahendM0x0">The subtrahend M0X0.</param>
        /// <param name="subtrahendM0x1">The subtrahend M0X1.</param>
        /// <param name="subtrahendM0x2">The subtrahend M0X2.</param>
        /// <param name="subtrahendM0x3">The subtrahend M0X3.</param>
        /// <param name="subtrahendM0x4">The subtrahend M0X4.</param>
        /// <param name="subtrahendM0x5">The subtrahend M0X5.</param>
        /// <param name="subtrahendM1x0">The subtrahend M1X0.</param>
        /// <param name="subtrahendM1x1">The subtrahend M1X1.</param>
        /// <param name="subtrahendM1x2">The subtrahend M1X2.</param>
        /// <param name="subtrahendM1x3">The subtrahend M1X3.</param>
        /// <param name="subtrahendM1x4">The subtrahend M1X4.</param>
        /// <param name="subtrahendM1x5">The subtrahend M1X5.</param>
        /// <param name="subtrahendM2x0">The subtrahend M2X0.</param>
        /// <param name="subtrahendM2x1">The subtrahend M2X1.</param>
        /// <param name="subtrahendM2x2">The subtrahend M2X2.</param>
        /// <param name="subtrahendM2x3">The subtrahend M2X3.</param>
        /// <param name="subtrahendM2x4">The subtrahend M2X4.</param>
        /// <param name="subtrahendM2x5">The subtrahend M2X5.</param>
        /// <param name="subtrahendM3x0">The subtrahend M3X0.</param>
        /// <param name="subtrahendM3x1">The subtrahend M3X1.</param>
        /// <param name="subtrahendM3x2">The subtrahend M3X2.</param>
        /// <param name="subtrahendM3x3">The subtrahend M3X3.</param>
        /// <param name="subtrahendM3x4">The subtrahend M3X4.</param>
        /// <param name="subtrahendM3x5">The subtrahend M3X5.</param>
        /// <param name="subtrahendM4x0">The subtrahend M4X0.</param>
        /// <param name="subtrahendM4x1">The subtrahend M4X1.</param>
        /// <param name="subtrahendM4x2">The subtrahend M4X2.</param>
        /// <param name="subtrahendM4x3">The subtrahend M4X3.</param>
        /// <param name="subtrahendM4x4">The subtrahend M4X4.</param>
        /// <param name="subtrahendM4x5">The subtrahend M4X5.</param>
        /// <param name="subtrahendM5x0">The subtrahend M5X0.</param>
        /// <param name="subtrahendM5x1">The subtrahend M5X1.</param>
        /// <param name="subtrahendM5x2">The subtrahend M5X2.</param>
        /// <param name="subtrahendM5x3">The subtrahend M5X3.</param>
        /// <param name="subtrahendM5x4">The subtrahend M5X4.</param>
        /// <param name="subtrahendM5x5">The subtrahend M5X5.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4, double m0x5,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x0, double m5x1, double m5x2, double m5x3, double m5x4, double m5x5
            ) Subtract6x6x6x6(
            double minuendM0x0, double minuendM0x1, double minuendM0x2, double minuendM0x3, double minuendM0x4, double minuendM0x5,
            double minuendM1x0, double minuendM1x1, double minuendM1x2, double minuendM1x3, double minuendM1x4, double minuendM1x5,
            double minuendM2x0, double minuendM2x1, double minuendM2x2, double minuendM2x3, double minuendM2x4, double minuendM2x5,
            double minuendM3x0, double minuendM3x1, double minuendM3x2, double minuendM3x3, double minuendM3x4, double minuendM3x5,
            double minuendM4x0, double minuendM4x1, double minuendM4x2, double minuendM4x3, double minuendM4x4, double minuendM4x5,
            double minuendM5x0, double minuendM5x1, double minuendM5x2, double minuendM5x3, double minuendM5x4, double minuendM5x5,
            double subtrahendM0x0, double subtrahendM0x1, double subtrahendM0x2, double subtrahendM0x3, double subtrahendM0x4, double subtrahendM0x5,
            double subtrahendM1x0, double subtrahendM1x1, double subtrahendM1x2, double subtrahendM1x3, double subtrahendM1x4, double subtrahendM1x5,
            double subtrahendM2x0, double subtrahendM2x1, double subtrahendM2x2, double subtrahendM2x3, double subtrahendM2x4, double subtrahendM2x5,
            double subtrahendM3x0, double subtrahendM3x1, double subtrahendM3x2, double subtrahendM3x3, double subtrahendM3x4, double subtrahendM3x5,
            double subtrahendM4x0, double subtrahendM4x1, double subtrahendM4x2, double subtrahendM4x3, double subtrahendM4x4, double subtrahendM4x5,
            double subtrahendM5x0, double subtrahendM5x1, double subtrahendM5x2, double subtrahendM5x3, double subtrahendM5x4, double subtrahendM5x5)
            => (minuendM0x0 - subtrahendM0x0, minuendM0x1 - subtrahendM0x1, minuendM0x2 - subtrahendM0x2, minuendM0x3 - subtrahendM0x3, minuendM0x4 - subtrahendM0x4, minuendM0x5 - subtrahendM0x5,
                minuendM1x0 - subtrahendM1x0, minuendM1x1 - subtrahendM1x1, minuendM1x2 - subtrahendM1x2, minuendM1x3 - subtrahendM1x3, minuendM1x4 - subtrahendM1x4, minuendM1x5 - subtrahendM1x5,
                minuendM2x0 - subtrahendM2x0, minuendM2x1 - subtrahendM2x1, minuendM2x2 - subtrahendM2x2, minuendM2x3 - subtrahendM2x3, minuendM2x4 - subtrahendM2x4, minuendM2x5 - subtrahendM2x5,
                minuendM3x0 - subtrahendM3x0, minuendM3x1 - subtrahendM3x1, minuendM3x2 - subtrahendM3x2, minuendM3x3 - subtrahendM3x3, minuendM3x4 - subtrahendM3x4, minuendM3x5 - subtrahendM3x5,
                minuendM4x0 - subtrahendM4x0, minuendM4x1 - subtrahendM4x1, minuendM4x2 - subtrahendM4x2, minuendM4x3 - subtrahendM4x3, minuendM4x4 - subtrahendM4x4, minuendM4x5 - subtrahendM4x5,
                minuendM5x0 - subtrahendM5x0, minuendM5x1 - subtrahendM5x1, minuendM5x2 - subtrahendM5x2, minuendM5x3 - subtrahendM5x3, minuendM5x4 - subtrahendM5x4, minuendM5x5 - subtrahendM5x5);
        #endregion

        #region Scale
        /// <summary>
        /// Used to multiply a Matrix2x2 object by a multiplier value.
        /// </summary>
        /// <param name="m0x0">The M0X0.</param>
        /// <param name="m0x1">The M0X1.</param>
        /// <param name="m1x0">The M1X0.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double m0x0, double m0x1, double m1x0, double m1x1) Scale2x2(
            double m0x0, double m0x1,
            double m1x0, double m1x1,
            double multiplier)
            => (
                m0x0 * multiplier, m0x1 * multiplier,
                m1x0 * multiplier, m1x1 * multiplier
            );

        /// <summary>
        /// Used to multiply a Matrix3x3 object by a multiplier value.
        /// </summary>
        /// <param name="m0x0">The M0X0.</param>
        /// <param name="m0x1">The M0X1.</param>
        /// <param name="m0x2">The M0X2.</param>
        /// <param name="m1x0">The M1X0.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m2x0">The M2X0.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double m0x0, double m0x1, double m0x2, double m1x0, double m1x1, double m1x2, double m2x0, double m2x1, double m2x2) Scale3x3(
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2,
            double multiplier)
            => (
                m0x0 * multiplier, m0x1 * multiplier, m0x2 * multiplier,
                m1x0 * multiplier, m1x1 * multiplier, m1x2 * multiplier,
                m2x0 * multiplier, m2x1 * multiplier, m2x2 * multiplier
            );

        /// <summary>
        /// Used to multiply (concatenate) two <see cref="Matrix4x4D" />s.
        /// </summary>
        /// <param name="multiplicandM0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicandM0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicandM0x2">The multiplicand M0X2.</param>
        /// <param name="multiplicandM0x3">The multiplicand M0X3.</param>
        /// <param name="multiplicandM1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicandM1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicandM1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicandM1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicandM2x0">The multiplicand M2X0.</param>
        /// <param name="multiplicandM2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicandM2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicandM2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicandM3x0">The multiplicand M3X0.</param>
        /// <param name="multiplicandM3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicandM3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicandM3x3">The multiplicand M3X3.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3,
            double m1x0, double m1x1, double m1x2, double m1x3,
            double m2x0, double m2x1, double m2x2, double m2x3,
            double m3x0, double m3x1, double m3x2, double m3x3
            ) Scale4x4(
            double multiplicandM0x0, double multiplicandM0x1, double multiplicandM0x2, double multiplicandM0x3,
            double multiplicandM1x0, double multiplicandM1x1, double multiplicandM1x2, double multiplicandM1x3,
            double multiplicandM2x0, double multiplicandM2x1, double multiplicandM2x2, double multiplicandM2x3,
            double multiplicandM3x0, double multiplicandM3x1, double multiplicandM3x2, double multiplicandM3x3,
            double multiplier)
            => (
                multiplicandM0x0 * multiplier, multiplicandM0x1 * multiplier, multiplicandM0x2 * multiplier, multiplicandM0x3 * multiplier,
                multiplicandM1x0 * multiplier, multiplicandM1x1 * multiplier, multiplicandM1x2 * multiplier, multiplicandM1x3 * multiplier,
                multiplicandM2x0 * multiplier, multiplicandM2x1 * multiplier, multiplicandM2x2 * multiplier, multiplicandM2x3 * multiplier,
                multiplicandM3x0 * multiplier, multiplicandM3x1 * multiplier, multiplicandM3x2 * multiplier, multiplicandM3x3 * multiplier
            );

        /// <summary>
        /// Used to multiply (concatenate) two Matrix5x5Ds.
        /// </summary>
        /// <param name="multiplicandM0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicandM0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicandM0x2">The multiplicand M0X2.</param>
        /// <param name="multiplicandM0x3">The multiplicand M0X3.</param>
        /// <param name="multiplicandM0x4">The multiplicand M0X4.</param>
        /// <param name="multiplicandM1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicandM1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicandM1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicandM1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicandM1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicandM2x0">The multiplicand M2X0.</param>
        /// <param name="multiplicandM2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicandM2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicandM2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicandM2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicandM3x0">The multiplicand M3X0.</param>
        /// <param name="multiplicandM3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicandM3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicandM3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicandM3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicandM4x0">The multiplicand M4X0.</param>
        /// <param name="multiplicandM4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicandM4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicandM4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicandM4x4">The multiplicand M4X4.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4
            ) Scale5x5(
            double multiplicandM0x0, double multiplicandM0x1, double multiplicandM0x2, double multiplicandM0x3, double multiplicandM0x4,
            double multiplicandM1x0, double multiplicandM1x1, double multiplicandM1x2, double multiplicandM1x3, double multiplicandM1x4,
            double multiplicandM2x0, double multiplicandM2x1, double multiplicandM2x2, double multiplicandM2x3, double multiplicandM2x4,
            double multiplicandM3x0, double multiplicandM3x1, double multiplicandM3x2, double multiplicandM3x3, double multiplicandM3x4,
            double multiplicandM4x0, double multiplicandM4x1, double multiplicandM4x2, double multiplicandM4x3, double multiplicandM4x4,
            double multiplier)
            => (
                multiplicandM0x0 * multiplier, multiplicandM0x1 * multiplier, multiplicandM0x2 * multiplier, multiplicandM0x3 * multiplier, multiplicandM0x4 * multiplier,
                multiplicandM1x0 * multiplier, multiplicandM1x1 * multiplier, multiplicandM1x2 * multiplier, multiplicandM1x3 * multiplier, multiplicandM1x4 * multiplier,
                multiplicandM2x0 * multiplier, multiplicandM2x1 * multiplier, multiplicandM2x2 * multiplier, multiplicandM2x3 * multiplier, multiplicandM2x4 * multiplier,
                multiplicandM3x0 * multiplier, multiplicandM3x1 * multiplier, multiplicandM3x2 * multiplier, multiplicandM3x3 * multiplier, multiplicandM3x4 * multiplier,
                multiplicandM4x0 * multiplier, multiplicandM4x1 * multiplier, multiplicandM4x2 * multiplier, multiplicandM4x3 * multiplier, multiplicandM4x4 * multiplier
            );

        /// <summary>
        /// Used to multiply (concatenate) two Matrix5x5Ds.
        /// </summary>
        /// <param name="multiplicandM0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicandM0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicandM0x2">The multiplicand M0X2.</param>
        /// <param name="multiplicandM0x3">The multiplicand M0X3.</param>
        /// <param name="multiplicandM0x4">The multiplicand M0X4.</param>
        /// <param name="multiplicandM0x5">The multiplicand M0X5.</param>
        /// <param name="multiplicandM1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicandM1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicandM1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicandM1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicandM1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicandM1x5">The multiplicand M1X5.</param>
        /// <param name="multiplicandM2x0">The multiplicand M2X0.</param>
        /// <param name="multiplicandM2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicandM2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicandM2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicandM2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicandM2x5">The multiplicand M2X5.</param>
        /// <param name="multiplicandM3x0">The multiplicand M3X0.</param>
        /// <param name="multiplicandM3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicandM3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicandM3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicandM3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicandM3x5">The multiplicand M3X5.</param>
        /// <param name="multiplicandM4x0">The multiplicand M4X0.</param>
        /// <param name="multiplicandM4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicandM4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicandM4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicandM4x4">The multiplicand M4X4.</param>
        /// <param name="multiplicandM4x5">The multiplicand M4X5.</param>
        /// <param name="multiplicandM5x0">The multiplicand M5X0.</param>
        /// <param name="multiplicandM5x1">The multiplicand M5X1.</param>
        /// <param name="multiplicandM5x2">The multiplicand M5X2.</param>
        /// <param name="multiplicandM5x3">The multiplicand M5X3.</param>
        /// <param name="multiplicandM5x4">The multiplicand M5X4.</param>
        /// <param name="multiplicandM5x5">The multiplicand M5X5.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4, double m0x5,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x0, double m5x1, double m5x2, double m5x3, double m5x4, double m5x5
            ) Scale6x6(
            double multiplicandM0x0, double multiplicandM0x1, double multiplicandM0x2, double multiplicandM0x3, double multiplicandM0x4, double multiplicandM0x5,
            double multiplicandM1x0, double multiplicandM1x1, double multiplicandM1x2, double multiplicandM1x3, double multiplicandM1x4, double multiplicandM1x5,
            double multiplicandM2x0, double multiplicandM2x1, double multiplicandM2x2, double multiplicandM2x3, double multiplicandM2x4, double multiplicandM2x5,
            double multiplicandM3x0, double multiplicandM3x1, double multiplicandM3x2, double multiplicandM3x3, double multiplicandM3x4, double multiplicandM3x5,
            double multiplicandM4x0, double multiplicandM4x1, double multiplicandM4x2, double multiplicandM4x3, double multiplicandM4x4, double multiplicandM4x5,
            double multiplicandM5x0, double multiplicandM5x1, double multiplicandM5x2, double multiplicandM5x3, double multiplicandM5x4, double multiplicandM5x5,
            double multiplier)
            => (
                multiplicandM0x0 * multiplier, multiplicandM0x1 * multiplier, multiplicandM0x2 * multiplier, multiplicandM0x3 * multiplier, multiplicandM0x4 * multiplier, multiplicandM0x5 * multiplier,
                multiplicandM1x0 * multiplier, multiplicandM1x1 * multiplier, multiplicandM1x2 * multiplier, multiplicandM1x3 * multiplier, multiplicandM1x4 * multiplier, multiplicandM1x5 * multiplier,
                multiplicandM2x0 * multiplier, multiplicandM2x1 * multiplier, multiplicandM2x2 * multiplier, multiplicandM2x3 * multiplier, multiplicandM2x4 * multiplier, multiplicandM2x5 * multiplier,
                multiplicandM3x0 * multiplier, multiplicandM3x1 * multiplier, multiplicandM3x2 * multiplier, multiplicandM3x3 * multiplier, multiplicandM3x4 * multiplier, multiplicandM3x5 * multiplier,
                multiplicandM4x0 * multiplier, multiplicandM4x1 * multiplier, multiplicandM4x2 * multiplier, multiplicandM4x3 * multiplier, multiplicandM4x4 * multiplier, multiplicandM4x5 * multiplier,
                multiplicandM5x0 * multiplier, multiplicandM5x1 * multiplier, multiplicandM5x2 * multiplier, multiplicandM5x3 * multiplier, multiplicandM5x4 * multiplier, multiplicandM5x5 * multiplier
            );
        #endregion Scale

        #region Multiply
        /// <summary>
        /// Multiplies a vector2 and matrix2x2.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) MultiplyVector2DMatrix2x2(
            double x, double y,
            double m1x1, double m1x2,
            double m2x1, double m2x2)
            => (
                (x * m1x1) + (x * m1x2),
                (y * m2x1) + (y * m2x2)
            );

        /// <summary>
        /// Multiplies a vector3 and matrix3x3.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m1x3">The M1X3.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <param name="m2x3">The M2X3.</param>
        /// <param name="m3x1">The M3X1.</param>
        /// <param name="m3x2">The M3X2.</param>
        /// <param name="m3x3">The M3X3.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) MultiplyVector3DMatrix3x3(
            double x, double y, double z,
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3)
            => (
                (x * m1x1) + (x * m1x2) + (x * m1x3),
                (y * m2x1) + (y * m2x2) + (y * m2x3),
                (z * m3x1) + (z * m3x2) + (z * m3x3)
            );

        /// <summary>
        /// Multiplies a vector4 and matrix4x4.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="w">The w.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m1x3">The M1X3.</param>
        /// <param name="m1x4">The M1X4.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <param name="m2x3">The M2X3.</param>
        /// <param name="m2x4">The M2X4.</param>
        /// <param name="m3x1">The M3X1.</param>
        /// <param name="m3x2">The M3X2.</param>
        /// <param name="m3x3">The M3X3.</param>
        /// <param name="m3x4">The M3X4.</param>
        /// <param name="m4x1">The M4X1.</param>
        /// <param name="m4x2">The M4X2.</param>
        /// <param name="m4x3">The M4X3.</param>
        /// <param name="m4x4">The M4X4.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W) MultiplyVector4DMatrix4x4(
            double x, double y, double z, double w,
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4)
            => (
                (x * m1x1) + (x * m1x2) + (x * m1x3) + (x * m1x4),
                (y * m2x1) + (y * m2x2) + (y * m2x3) + (y * m2x4),
                (z * m3x1) + (z * m3x2) + (z * m3x3) + (z * m3x4),
                (w * m4x1) + (w * m4x2) + (w * m4x3) + (w * m4x4)
            );

        /// <summary>
        /// Multiplies a vector5 and matrix5x5.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="w">The w.</param>
        /// <param name="v">The v.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m1x3">The M1X3.</param>
        /// <param name="m1x4">The M1X4.</param>
        /// <param name="m1x5">The M1X5.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <param name="m2x3">The M2X3.</param>
        /// <param name="m2x4">The M2X4.</param>
        /// <param name="m2x5">The M2X5.</param>
        /// <param name="m3x1">The M3X1.</param>
        /// <param name="m3x2">The M3X2.</param>
        /// <param name="m3x3">The M3X3.</param>
        /// <param name="m3x4">The M3X4.</param>
        /// <param name="m3x5">The M3X5.</param>
        /// <param name="m4x1">The M4X1.</param>
        /// <param name="m4x2">The M4X2.</param>
        /// <param name="m4x3">The M4X3.</param>
        /// <param name="m4x4">The M4X4.</param>
        /// <param name="m4x5">The M4X5.</param>
        /// <param name="m5x1">The M5X1.</param>
        /// <param name="m5x2">The M5X2.</param>
        /// <param name="m5x3">The M5X3.</param>
        /// <param name="m5x4">The M5X4.</param>
        /// <param name="m5x5">The M5X5.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W, double V) MultiplyVector5DMatrix5x5(
            double x, double y, double z, double w, double v,
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5)
            => (
                (x * m1x1) + (x * m1x2) + (x * m1x3) + (x * m1x4) + (x * m1x5),
                (y * m2x1) + (y * m2x2) + (y * m2x3) + (y * m2x4) + (y * m2x5),
                (z * m3x1) + (z * m3x2) + (z * m3x3) + (z * m3x4) + (z * m3x5),
                (w * m4x1) + (w * m4x2) + (w * m4x3) + (w * m4x4) + (w * m4x5),
                (w * m5x1) + (v * m5x2) + (v * m5x3) + (v * m5x4) + (v * m5x5)
            );

        /// <summary>
        /// Multiplies a vector6 and matrix6x6.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="w">The w.</param>
        /// <param name="v">The v.</param>
        /// <param name="u">The u.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m1x3">The M1X3.</param>
        /// <param name="m1x4">The M1X4.</param>
        /// <param name="m1x5">The M1X5.</param>
        /// <param name="m1x6">The M1X6.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <param name="m2x3">The M2X3.</param>
        /// <param name="m2x4">The M2X4.</param>
        /// <param name="m2x5">The M2X5.</param>
        /// <param name="m2x6">The M2X6.</param>
        /// <param name="m3x1">The M3X1.</param>
        /// <param name="m3x2">The M3X2.</param>
        /// <param name="m3x3">The M3X3.</param>
        /// <param name="m3x4">The M3X4.</param>
        /// <param name="m3x5">The M3X5.</param>
        /// <param name="m3x6">The M3X6.</param>
        /// <param name="m4x1">The M4X1.</param>
        /// <param name="m4x2">The M4X2.</param>
        /// <param name="m4x3">The M4X3.</param>
        /// <param name="m4x4">The M4X4.</param>
        /// <param name="m4x5">The M4X5.</param>
        /// <param name="m4x6">The M4X6.</param>
        /// <param name="m5x1">The M5X1.</param>
        /// <param name="m5x2">The M5X2.</param>
        /// <param name="m5x3">The M5X3.</param>
        /// <param name="m5x4">The M5X4.</param>
        /// <param name="m5x5">The M5X5.</param>
        /// <param name="m5x6">The M5X6.</param>
        /// <param name="m6x1">The M6X1.</param>
        /// <param name="m6x2">The M6X2.</param>
        /// <param name="m6x3">The M6X3.</param>
        /// <param name="m6x4">The M6X4.</param>
        /// <param name="m6x5">The M6X5.</param>
        /// <param name="m6x6">The M6X6.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W, double V, double U) MultiplyVector6DMatrix6x6(
            double x, double y, double z, double w, double v, double u,
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6)
            => (
                (x * m1x1) + (x * m1x2) + (x * m1x3) + (x * m1x4) + (x * m1x5) + (x * m1x6),
                (y * m2x1) + (y * m2x2) + (y * m2x3) + (y * m2x4) + (y * m2x5) + (y * m2x6),
                (z * m3x1) + (z * m3x2) + (z * m3x3) + (z * m3x4) + (z * m3x5) + (z * m3x6),
                (w * m4x1) + (w * m4x2) + (w * m4x3) + (w * m4x4) + (w * m4x5) + (w * m4x6),
                (w * m5x1) + (v * m5x2) + (v * m5x3) + (v * m5x4) + (v * m5x5) + (v * m5x6),
                (w * m6x1) + (u * m6x2) + (u * m6x3) + (u * m6x4) + (u * m6x5) + (u * m6x6)
            );

        /// <summary>
        /// Used to multiply (concatenate) two Matrix2x2s.
        /// </summary>
        /// <param name="multiplicand0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicand0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicand1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplier0x0">The multiplier M0X0.</param>
        /// <param name="multiplier0x1">The multiplier M0X1.</param>
        /// <param name="multiplier1x0">The multiplier M1X0.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1,
            double m1x0, double m1x1
            ) Multiply2x2x2x2(
            double multiplicand0x0, double multiplicand0x1,
            double multiplicand1x0, double multiplicand1x1,
            double multiplier0x0, double multiplier0x1,
            double multiplier1x0, double multiplier1x1)
            => (
                (multiplicand0x0 * multiplier0x0) + (multiplicand0x1 * multiplier1x0), (multiplicand0x0 * multiplier0x1) + (multiplicand0x1 * multiplier1x1),
                (multiplicand1x0 * multiplier0x0) + (multiplicand1x1 * multiplier1x0), (multiplicand1x0 * multiplier0x1) + (multiplicand1x1 * multiplier1x1)
            );

        /// <summary>
        /// Used to multiply (concatenate) two Matrix3x3s.
        /// </summary>
        /// <param name="multiplicand0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicand0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicand0x2">The multiplicand M0X2.</param>
        /// <param name="multiplicand1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand2x0">The multiplicand M2X0.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplier0x0">The multiplier M0X0.</param>
        /// <param name="multiplier0x1">The multiplier M0X1.</param>
        /// <param name="multiplier1x0">The multiplier M1X0.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2
            ) Multiply3x3x2x2(
            double multiplicand0x0, double multiplicand0x1, double multiplicand0x2,
            double multiplicand1x0, double multiplicand1x1, double multiplicand1x2,
            double multiplicand2x0, double multiplicand2x1, double multiplicand2x2,
            double multiplier0x0, double multiplier0x1,
            double multiplier1x0, double multiplier1x1)
            => (
                (multiplicand0x0 * multiplier0x0) + (multiplicand0x1 * multiplier1x0), (multiplicand0x0 * multiplier0x1) + (multiplicand0x1 * multiplier1x1), multiplicand0x2,
                (multiplicand1x0 * multiplier0x0) + (multiplicand1x1 * multiplier1x0), (multiplicand1x0 * multiplier0x1) + (multiplicand1x1 * multiplier1x1), multiplicand1x2,
                (multiplicand2x0 * multiplier0x0) + (multiplicand2x1 * multiplier1x0), (multiplicand2x0 * multiplier0x1) + (multiplicand2x1 * multiplier1x1), multiplicand2x2
            );

        /// <summary>
        /// Used to multiply (concatenate) two Matrix3x3s.
        /// </summary>
        /// <param name="multiplicand0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicand0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicand1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplier0x0">The multiplier M0X0.</param>
        /// <param name="multiplier0x1">The multiplier M0X1.</param>
        /// <param name="multiplier0x2">The multiplier M0X2.</param>
        /// <param name="multiplier1x0">The multiplier M1X0.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier2x0">The multiplier M2X0.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2
            ) Multiply2x2x3x3(
            double multiplicand0x0, double multiplicand0x1,
            double multiplicand1x0, double multiplicand1x1,
            double multiplier0x0, double multiplier0x1, double multiplier0x2,
            double multiplier1x0, double multiplier1x1, double multiplier1x2,
            double multiplier2x0, double multiplier2x1, double multiplier2x2)
            => (
                (multiplicand0x0 * multiplier0x0) + (multiplicand0x1 * multiplier1x0), (multiplicand0x0 * multiplier0x1) + (multiplicand0x1 * multiplier1x1), (multiplicand0x0 * multiplier0x2) + (multiplicand0x1 * multiplier1x2),
                (multiplicand1x0 * multiplier0x0) + (multiplicand1x1 * multiplier1x0), (multiplicand1x0 * multiplier0x1) + (multiplicand1x1 * multiplier1x1), (multiplicand1x0 * multiplier0x2) + (multiplicand1x1 * multiplier1x2),
                multiplier2x0, multiplier2x1, multiplier2x2
            );

        /// <summary>
        /// Used to multiply (concatenate) two <see cref="Matrix3x3D" />s.
        /// </summary>
        /// <param name="multiplicand0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicand0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicand0x2">The multiplicand M0X2.</param>
        /// <param name="multiplicand1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand2x0">The multiplicand M2X0.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplier0x0">The multiplier M0X0.</param>
        /// <param name="multiplier0x1">The multiplier M0X1.</param>
        /// <param name="multiplier0x2">The multiplier M0X2.</param>
        /// <param name="multiplier1x0">The multiplier M1X0.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier2x0">The multiplier M2X0.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2
            ) Multiply3x3x3x3(
            double multiplicand0x0, double multiplicand0x1, double multiplicand0x2,
            double multiplicand1x0, double multiplicand1x1, double multiplicand1x2,
            double multiplicand2x0, double multiplicand2x1, double multiplicand2x2,
            double multiplier0x0, double multiplier0x1, double multiplier0x2,
            double multiplier1x0, double multiplier1x1, double multiplier1x2,
            double multiplier2x0, double multiplier2x1, double multiplier2x2)
            => (
                (multiplicand0x0 * multiplier0x0) + (multiplicand0x1 * multiplier1x0) + (multiplicand0x2 * multiplier2x0), (multiplicand0x0 * multiplier0x1) + (multiplicand0x1 * multiplier1x1) + (multiplicand0x2 * multiplier2x1), (multiplicand0x0 * multiplier0x2) + (multiplicand0x1 * multiplier1x2) + (multiplicand0x2 * multiplier2x2),
                (multiplicand1x0 * multiplier0x0) + (multiplicand1x1 * multiplier1x0) + (multiplicand1x2 * multiplier2x0), (multiplicand1x0 * multiplier0x1) + (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1), (multiplicand1x0 * multiplier0x2) + (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2),
                (multiplicand2x0 * multiplier0x0) + (multiplicand2x1 * multiplier1x0) + (multiplicand2x2 * multiplier2x0), (multiplicand2x0 * multiplier0x1) + (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1), (multiplicand2x0 * multiplier0x2) + (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2)
            );

        /// <summary>
        /// Used to multiply (concatenate) two <see cref="Matrix4x4D" />s.
        /// </summary>
        /// <param name="multiplicand0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicand0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicand1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplier0x0">The multiplier M0X0.</param>
        /// <param name="multiplier0x1">The multiplier M0X1.</param>
        /// <param name="multiplier0x2">The multiplier M0X2.</param>
        /// <param name="multiplier0x3">The multiplier M0X3.</param>
        /// <param name="multiplier1x0">The multiplier M1X0.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier2x0">The multiplier M2X0.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier3x0">The multiplier M3X0.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3,
            double m1x0, double m1x1, double m1x2, double m1x3,
            double m2x0, double m2x1, double m2x2, double m2x3,
            double m3x0, double m3x1, double m3x2, double m3x3
            ) Multiply2x2x4x4(
            double multiplicand0x0, double multiplicand0x1,
            double multiplicand1x0, double multiplicand1x1,
            double multiplier0x0, double multiplier0x1, double multiplier0x2, double multiplier0x3,
            double multiplier1x0, double multiplier1x1, double multiplier1x2, double multiplier1x3,
            double multiplier2x0, double multiplier2x1, double multiplier2x2, double multiplier2x3,
            double multiplier3x0, double multiplier3x1, double multiplier3x2, double multiplier3x3)
            => (
                (multiplicand0x0 * multiplier0x0) + (multiplicand0x1 * multiplier1x0), (multiplicand0x0 * multiplier0x1) + (multiplicand0x1 * multiplier1x1), (multiplicand0x0 * multiplier0x2) + (multiplicand0x1 * multiplier1x2), (multiplicand0x0 * multiplier0x3) + (multiplicand0x1 * multiplier1x3),
                (multiplicand1x0 * multiplier0x0) + (multiplicand1x1 * multiplier1x0), (multiplicand1x0 * multiplier0x1) + (multiplicand1x1 * multiplier1x1), (multiplicand1x0 * multiplier0x2) + (multiplicand1x1 * multiplier1x2), (multiplicand1x0 * multiplier0x3) + (multiplicand1x1 * multiplier1x3),
                multiplier2x0, multiplier2x1, multiplier2x2, multiplier2x3,
                multiplier3x0, multiplier3x1, multiplier3x2, multiplier3x3
            );

        /// <summary>
        /// Used to multiply (concatenate) two <see cref="Matrix4x4D" />s.
        /// </summary>
        /// <param name="multiplicand0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicand0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicand0x2">The multiplicand M0X2.</param>
        /// <param name="multiplicand1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand2x0">The multiplicand M2X0.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplier0x0">The multiplier M0X0.</param>
        /// <param name="multiplier0x1">The multiplier M0X1.</param>
        /// <param name="multiplier0x2">The multiplier M0X2.</param>
        /// <param name="multiplier0x3">The multiplier M0X3.</param>
        /// <param name="multiplier1x0">The multiplier M1X0.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier2x0">The multiplier M2X0.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier3x0">The multiplier M3X0.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3,
            double m1x0, double m1x1, double m1x2, double m1x3,
            double m2x0, double m2x1, double m2x2, double m2x3,
            double m3x0, double m3x1, double m3x2, double m3x3
            ) Multiply3x3x4x4(
            double multiplicand0x0, double multiplicand0x1, double multiplicand0x2,
            double multiplicand1x0, double multiplicand1x1, double multiplicand1x2,
            double multiplicand2x0, double multiplicand2x1, double multiplicand2x2,
            double multiplier0x0, double multiplier0x1, double multiplier0x2, double multiplier0x3,
            double multiplier1x0, double multiplier1x1, double multiplier1x2, double multiplier1x3,
            double multiplier2x0, double multiplier2x1, double multiplier2x2, double multiplier2x3,
            double multiplier3x0, double multiplier3x1, double multiplier3x2, double multiplier3x3)
            => (
                (multiplicand0x0 * multiplier0x0) + (multiplicand0x1 * multiplier1x0) + (multiplicand0x2 * multiplier2x0), (multiplicand0x0 * multiplier0x1) + (multiplicand0x1 * multiplier1x1) + (multiplicand0x2 * multiplier2x1), (multiplicand0x0 * multiplier0x2) + (multiplicand0x1 * multiplier1x2) + (multiplicand0x2 * multiplier2x2), (multiplicand0x0 * multiplier0x3) + (multiplicand0x1 * multiplier1x3) + (multiplicand0x2 * multiplier2x3),
                (multiplicand1x0 * multiplier0x0) + (multiplicand1x1 * multiplier1x0) + (multiplicand1x2 * multiplier2x0), (multiplicand1x0 * multiplier0x1) + (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1), (multiplicand1x0 * multiplier0x2) + (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2), (multiplicand1x0 * multiplier0x3) + (multiplicand1x1 * multiplier1x3) + (multiplicand1x2 * multiplier2x3),
                (multiplicand2x0 * multiplier0x0) + (multiplicand2x1 * multiplier1x0) + (multiplicand2x2 * multiplier2x0), (multiplicand2x0 * multiplier0x1) + (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1), (multiplicand2x0 * multiplier0x2) + (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2), (multiplicand2x0 * multiplier0x3) + (multiplicand2x1 * multiplier1x3) + (multiplicand2x2 * multiplier2x3),
                multiplier3x0, multiplier3x1, multiplier3x2, multiplier3x3
            );

        /// <summary>
        /// Used to multiply (concatenate) two <see cref="Matrix4x4D" />s.
        /// </summary>
        /// <param name="multiplicand0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicand0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicand0x2">The multiplicand M0X2.</param>
        /// <param name="multiplicand0x3">The multiplicand M0X3.</param>
        /// <param name="multiplicand1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand2x0">The multiplicand M2X0.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand3x0">The multiplicand M3X0.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplier0x0">The multiplier M0X0.</param>
        /// <param name="multiplier0x1">The multiplier M0X1.</param>
        /// <param name="multiplier1x0">The multiplier M1X0.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3,
            double m1x0, double m1x1, double m1x2, double m1x3,
            double m2x0, double m2x1, double m2x2, double m2x3,
            double m3x0, double m3x1, double m3x2, double m3x3
            ) Multiply4x4x2x2(
            double multiplicand0x0, double multiplicand0x1, double multiplicand0x2, double multiplicand0x3,
            double multiplicand1x0, double multiplicand1x1, double multiplicand1x2, double multiplicand1x3,
            double multiplicand2x0, double multiplicand2x1, double multiplicand2x2, double multiplicand2x3,
            double multiplicand3x0, double multiplicand3x1, double multiplicand3x2, double multiplicand3x3,
            double multiplier0x0, double multiplier0x1,
            double multiplier1x0, double multiplier1x1)
            => (
                (multiplicand0x0 * multiplier0x0) + (multiplicand0x1 * multiplier1x0), (multiplicand0x0 * multiplier0x1) + (multiplicand0x1 * multiplier1x1), multiplicand0x2, multiplicand0x3,
                (multiplicand1x0 * multiplier0x0) + (multiplicand1x1 * multiplier1x0), (multiplicand1x0 * multiplier0x1) + (multiplicand1x1 * multiplier1x1), multiplicand1x2, multiplicand1x3,
                (multiplicand2x0 * multiplier0x0) + (multiplicand2x1 * multiplier1x0), (multiplicand2x0 * multiplier0x1) + (multiplicand2x1 * multiplier1x1), multiplicand2x2, multiplicand2x3,
                (multiplicand3x0 * multiplier0x0) + (multiplicand3x1 * multiplier1x0), (multiplicand3x0 * multiplier0x1) + (multiplicand3x1 * multiplier1x1), multiplicand3x2, multiplicand3x3
            );

        /// <summary>
        /// Used to multiply (concatenate) two <see cref="Matrix4x4D" />s.
        /// </summary>
        /// <param name="multiplicand0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicand0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicand0x2">The multiplicand M0X2.</param>
        /// <param name="multiplicand0x3">The multiplicand M0X3.</param>
        /// <param name="multiplicand1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand2x0">The multiplicand M2X0.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand3x0">The multiplicand M3X0.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplier0x0">The multiplier M0X0.</param>
        /// <param name="multiplier0x1">The multiplier M0X1.</param>
        /// <param name="multiplier0x2">The multiplier M0X2.</param>
        /// <param name="multiplier1x0">The multiplier M1X0.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier2x0">The multiplier M2X0.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3,
            double m1x0, double m1x1, double m1x2, double m1x3,
            double m2x0, double m2x1, double m2x2, double m2x3,
            double m3x0, double m3x1, double m3x2, double m3x3
            ) Multiply4x4x3x3(
            double multiplicand0x0, double multiplicand0x1, double multiplicand0x2, double multiplicand0x3,
            double multiplicand1x0, double multiplicand1x1, double multiplicand1x2, double multiplicand1x3,
            double multiplicand2x0, double multiplicand2x1, double multiplicand2x2, double multiplicand2x3,
            double multiplicand3x0, double multiplicand3x1, double multiplicand3x2, double multiplicand3x3,
            double multiplier0x0, double multiplier0x1, double multiplier0x2,
            double multiplier1x0, double multiplier1x1, double multiplier1x2,
            double multiplier2x0, double multiplier2x1, double multiplier2x2)
            => (
                (multiplicand0x0 * multiplier0x0) + (multiplicand0x1 * multiplier1x0) + (multiplicand0x2 * multiplier2x0), (multiplicand0x0 * multiplier0x1) + (multiplicand0x1 * multiplier1x1) + (multiplicand0x2 * multiplier2x1), (multiplicand0x0 * multiplier0x2) + (multiplicand0x1 * multiplier1x2) + (multiplicand0x2 * multiplier2x2), multiplicand0x3,
                (multiplicand1x0 * multiplier0x0) + (multiplicand1x1 * multiplier1x0) + (multiplicand1x2 * multiplier2x0), (multiplicand1x0 * multiplier0x1) + (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1), (multiplicand1x0 * multiplier0x2) + (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2), multiplicand1x3,
                (multiplicand2x0 * multiplier0x0) + (multiplicand2x1 * multiplier1x0) + (multiplicand2x2 * multiplier2x0), (multiplicand2x0 * multiplier0x1) + (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1), (multiplicand2x0 * multiplier0x2) + (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2), multiplicand2x3,
                (multiplicand3x0 * multiplier0x0) + (multiplicand3x1 * multiplier1x0) + (multiplicand3x2 * multiplier2x0), (multiplicand3x0 * multiplier0x1) + (multiplicand3x1 * multiplier1x1) + (multiplicand3x2 * multiplier2x1), (multiplicand3x0 * multiplier0x2) + (multiplicand3x1 * multiplier1x2) + (multiplicand3x2 * multiplier2x2), multiplicand3x3
            );

        /// <summary>
        /// Used to multiply (concatenate) two <see cref="Matrix4x4D" />s.
        /// </summary>
        /// <param name="multiplicand0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicand0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicand0x2">The multiplicand M0X2.</param>
        /// <param name="multiplicand0x3">The multiplicand M0X3.</param>
        /// <param name="multiplicand1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand2x0">The multiplicand M2X0.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand3x0">The multiplicand M3X0.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplier0x0">The multiplier M0X0.</param>
        /// <param name="multiplier0x1">The multiplier M0X1.</param>
        /// <param name="multiplier0x2">The multiplier M0X2.</param>
        /// <param name="multiplier0x3">The multiplier M0X3.</param>
        /// <param name="multiplier1x0">The multiplier M1X0.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier2x0">The multiplier M2X0.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier3x0">The multiplier M3X0.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3,
            double m1x0, double m1x1, double m1x2, double m1x3,
            double m2x0, double m2x1, double m2x2, double m2x3,
            double m3x0, double m3x1, double m3x2, double m3x3
            ) Multiply4x4x4x4(
            double multiplicand0x0, double multiplicand0x1, double multiplicand0x2, double multiplicand0x3,
            double multiplicand1x0, double multiplicand1x1, double multiplicand1x2, double multiplicand1x3,
            double multiplicand2x0, double multiplicand2x1, double multiplicand2x2, double multiplicand2x3,
            double multiplicand3x0, double multiplicand3x1, double multiplicand3x2, double multiplicand3x3,
            double multiplier0x0, double multiplier0x1, double multiplier0x2, double multiplier0x3,
            double multiplier1x0, double multiplier1x1, double multiplier1x2, double multiplier1x3,
            double multiplier2x0, double multiplier2x1, double multiplier2x2, double multiplier2x3,
            double multiplier3x0, double multiplier3x1, double multiplier3x2, double multiplier3x3)
            => (
                (multiplicand0x0 * multiplier0x0) + (multiplicand0x1 * multiplier1x0) + (multiplicand0x2 * multiplier2x0) + (multiplicand0x3 * multiplier3x0), (multiplicand0x0 * multiplier0x1) + (multiplicand0x1 * multiplier1x1) + (multiplicand0x2 * multiplier2x1) + (multiplicand0x3 * multiplier3x1), (multiplicand0x0 * multiplier0x2) + (multiplicand0x1 * multiplier1x2) + (multiplicand0x2 * multiplier2x2) + (multiplicand0x3 * multiplier3x2), (multiplicand0x0 * multiplier0x3) + (multiplicand0x1 * multiplier1x3) + (multiplicand0x2 * multiplier2x3) + (multiplicand0x3 * multiplier3x3),
                (multiplicand1x0 * multiplier0x0) + (multiplicand1x1 * multiplier1x0) + (multiplicand1x2 * multiplier2x0) + (multiplicand1x3 * multiplier3x0), (multiplicand1x0 * multiplier0x1) + (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1) + (multiplicand1x3 * multiplier3x1), (multiplicand1x0 * multiplier0x2) + (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2) + (multiplicand1x3 * multiplier3x2), (multiplicand1x0 * multiplier0x3) + (multiplicand1x1 * multiplier1x3) + (multiplicand1x2 * multiplier2x3) + (multiplicand1x3 * multiplier3x3),
                (multiplicand2x0 * multiplier0x0) + (multiplicand2x1 * multiplier1x0) + (multiplicand2x2 * multiplier2x0) + (multiplicand2x3 * multiplier3x0), (multiplicand2x0 * multiplier0x1) + (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1) + (multiplicand2x3 * multiplier3x1), (multiplicand2x0 * multiplier0x2) + (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2) + (multiplicand2x3 * multiplier3x2), (multiplicand2x0 * multiplier0x3) + (multiplicand2x1 * multiplier1x3) + (multiplicand2x2 * multiplier2x3) + (multiplicand2x3 * multiplier3x3),
                (multiplicand3x0 * multiplier0x0) + (multiplicand3x1 * multiplier1x0) + (multiplicand3x2 * multiplier2x0) + (multiplicand3x3 * multiplier3x0), (multiplicand3x0 * multiplier0x1) + (multiplicand3x1 * multiplier1x1) + (multiplicand3x2 * multiplier2x1) + (multiplicand3x3 * multiplier3x1), (multiplicand3x0 * multiplier0x2) + (multiplicand3x1 * multiplier1x2) + (multiplicand3x2 * multiplier2x2) + (multiplicand3x3 * multiplier3x2), (multiplicand3x0 * multiplier0x3) + (multiplicand3x1 * multiplier1x3) + (multiplicand3x2 * multiplier2x3) + (multiplicand3x3 * multiplier3x3)
            );

        /// <summary>
        /// Multiply2x2x5x5s the specified multiplicand M0X0.
        /// </summary>
        /// <param name="multiplicand0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicand0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicand1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplier0x0">The multiplier M0X0.</param>
        /// <param name="multiplier0x1">The multiplier M0X1.</param>
        /// <param name="multiplier0x2">The multiplier M0X2.</param>
        /// <param name="multiplier0x3">The multiplier M0X3.</param>
        /// <param name="multiplier0x4">The multiplier M0X4.</param>
        /// <param name="multiplier1x0">The multiplier M1X0.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier1x4">The multiplier M1X4.</param>
        /// <param name="multiplier2x0">The multiplier M2X0.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier2x4">The multiplier M2X4.</param>
        /// <param name="multiplier3x0">The multiplier M3X0.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <param name="multiplier3x4">The multiplier M3X4.</param>
        /// <param name="multiplier4x0">The multiplier M4X0.</param>
        /// <param name="multiplier4x1">The multiplier M4X1.</param>
        /// <param name="multiplier4x2">The multiplier M4X2.</param>
        /// <param name="multiplier4x3">The multiplier M4X3.</param>
        /// <param name="multiplier4x4">The multiplier M4X4.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M0x0, double M0x1, double M0x2, double M0x3, double M0x4,
            double M1x0, double M1x1, double M1x2, double M1x3, double M1x4,
            double M2x0, double M2x1, double M2x2, double M2x3, double M2x4,
            double M3x0, double M3x1, double M3x2, double M3x3, double M3x4,
            double M4x0, double M4x1, double M4x2, double M4x3, double M4x4
            ) Multiply2x2x5x5(
            double multiplicand0x0, double multiplicand0x1,
            double multiplicand1x0, double multiplicand1x1,
            double multiplier0x0, double multiplier0x1, double multiplier0x2, double multiplier0x3, double multiplier0x4,
            double multiplier1x0, double multiplier1x1, double multiplier1x2, double multiplier1x3, double multiplier1x4,
            double multiplier2x0, double multiplier2x1, double multiplier2x2, double multiplier2x3, double multiplier2x4,
            double multiplier3x0, double multiplier3x1, double multiplier3x2, double multiplier3x3, double multiplier3x4,
            double multiplier4x0, double multiplier4x1, double multiplier4x2, double multiplier4x3, double multiplier4x4)
            => (
                (multiplicand0x0 * multiplier0x0) + (multiplicand0x1 * multiplier1x0) + multiplier2x0 + multiplier3x0 + multiplier4x0,
                (multiplicand0x0 * multiplier0x1) + (multiplicand0x1 * multiplier1x1) + multiplier2x1 + multiplier3x1 + multiplier4x1,
                (multiplicand0x0 * multiplier0x2) + (multiplicand0x1 * multiplier1x2) + multiplier2x2 + multiplier3x2 + multiplier4x2,
                (multiplicand0x0 * multiplier0x3) + (multiplicand0x1 * multiplier1x3) + multiplier2x3 + multiplier3x3 + multiplier4x3,
                (multiplicand0x0 * multiplier0x4) + (multiplicand0x1 * multiplier1x4) + multiplier2x4 + multiplier3x4 + multiplier4x4,
                (multiplicand1x0 * multiplier0x0) + (multiplicand1x1 * multiplier1x0) + multiplier2x0 + multiplier3x0 + multiplier4x0,
                (multiplicand1x0 * multiplier0x1) + (multiplicand1x1 * multiplier1x1) + multiplier2x1 + multiplier3x1 + multiplier4x1,
                (multiplicand1x0 * multiplier0x2) + (multiplicand1x1 * multiplier1x2) + multiplier2x2 + multiplier3x2 + multiplier4x2,
                (multiplicand1x0 * multiplier0x3) + (multiplicand1x1 * multiplier1x3) + multiplier2x3 + multiplier3x3 + multiplier4x3,
                (multiplicand1x0 * multiplier0x4) + (multiplicand1x1 * multiplier1x4) + multiplier2x4 + multiplier3x4 + multiplier4x4,
                multiplier0x0 + multiplier1x0 + multiplier2x0 + multiplier3x0 + multiplier4x0,
                multiplier0x1 + multiplier1x1 + multiplier2x1 + multiplier3x1 + multiplier4x1,
                multiplier0x2 + multiplier1x2 + multiplier2x2 + multiplier3x2 + multiplier4x2,
                multiplier0x3 + multiplier1x3 + multiplier2x3 + multiplier3x3 + multiplier4x3,
                multiplier0x4 + multiplier1x4 + multiplier2x4 + multiplier3x4 + multiplier4x4,
                multiplier0x0 + multiplier1x0 + multiplier2x0 + multiplier3x0 + multiplier4x0,
                multiplier0x1 + multiplier1x1 + multiplier2x1 + multiplier3x1 + multiplier4x1,
                multiplier0x2 + multiplier1x2 + multiplier2x2 + multiplier3x2 + multiplier4x2,
                multiplier0x3 + multiplier1x3 + multiplier2x3 + multiplier3x3 + multiplier4x3,
                multiplier0x4 + multiplier1x4 + multiplier2x4 + multiplier3x4 + multiplier4x4,
                multiplier0x0 + multiplier1x0 + multiplier2x0 + multiplier3x0 + multiplier4x0,
                multiplier0x1 + multiplier1x1 + multiplier2x1 + multiplier3x1 + multiplier4x1,
                multiplier0x2 + multiplier1x2 + multiplier2x2 + multiplier3x2 + multiplier4x2,
                multiplier0x3 + multiplier1x3 + multiplier2x3 + multiplier3x3 + multiplier4x3,
                multiplier0x4 + multiplier1x4 + multiplier2x4 + multiplier3x4 + multiplier4x4
            );

        /// <summary>
        /// Multiply5x5x2x2s the specified multiplicand M0X0.
        /// </summary>
        /// <param name="multiplicand0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicand0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicand0x2">The multiplicand M0X2.</param>
        /// <param name="multiplicand0x3">The multiplicand M0X3.</param>
        /// <param name="multiplicand0x4">The multiplicand M0X4.</param>
        /// <param name="multiplicand1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicand2x0">The multiplicand M2X0.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicand3x0">The multiplicand M3X0.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicand3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicand4x0">The multiplicand M4X0.</param>
        /// <param name="multiplicand4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicand4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicand4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicand4x4">The multiplicand M4X4.</param>
        /// <param name="multiplier0x0">The multiplier M0X0.</param>
        /// <param name="multiplier0x1">The multiplier M0X1.</param>
        /// <param name="multiplier1x0">The multiplier M1X0.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M0x0, double M0x1, double M0x2, double M0x3, double M0x4,
            double M1x0, double M1x1, double M1x2, double M1x3, double M1x4,
            double M2x0, double M2x1, double M2x2, double M2x3, double M2x4,
            double M3x0, double M3x1, double M3x2, double M3x3, double M3x4,
            double M4x0, double M4x1, double M4x2, double M4x3, double M4x4
            ) Multiply5x5x2x2(
            double multiplicand0x0, double multiplicand0x1, double multiplicand0x2, double multiplicand0x3, double multiplicand0x4,
            double multiplicand1x0, double multiplicand1x1, double multiplicand1x2, double multiplicand1x3, double multiplicand1x4,
            double multiplicand2x0, double multiplicand2x1, double multiplicand2x2, double multiplicand2x3, double multiplicand2x4,
            double multiplicand3x0, double multiplicand3x1, double multiplicand3x2, double multiplicand3x3, double multiplicand3x4,
            double multiplicand4x0, double multiplicand4x1, double multiplicand4x2, double multiplicand4x3, double multiplicand4x4,
            double multiplier0x0, double multiplier0x1,
            double multiplier1x0, double multiplier1x1)
            => (
                (multiplicand0x0 * multiplier0x0) + (multiplicand0x1 * multiplier1x0) + multiplicand0x2 + multiplicand0x3 + multiplicand0x4,
                (multiplicand0x0 * multiplier0x1) + (multiplicand0x1 * multiplier1x1) + multiplicand0x2 + multiplicand0x3 + multiplicand0x4,
                multiplicand0x0 + multiplicand0x1 + multiplicand0x2 + multiplicand0x3 + multiplicand0x4,
                multiplicand0x0 + multiplicand0x1 + multiplicand0x2 + multiplicand0x3 + multiplicand0x4,
                multiplicand0x0 + multiplicand0x1 + multiplicand0x2 + multiplicand0x3 + multiplicand0x4,
                (multiplicand1x0 * multiplier0x0) + (multiplicand1x1 * multiplier1x0) + multiplicand1x2 + multiplicand1x3 + multiplicand1x4,
                (multiplicand1x0 * multiplier0x1) + (multiplicand1x1 * multiplier1x1) + multiplicand1x2 + multiplicand1x3 + multiplicand1x4,
                multiplicand1x0 + multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4,
                multiplicand1x0 + multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4,
                multiplicand1x0 + multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4,
                (multiplicand2x0 * multiplier0x0) + (multiplicand2x1 * multiplier1x0) + multiplicand2x2 + multiplicand2x3 + multiplicand2x4,
                (multiplicand2x0 * multiplier0x1) + multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4,
                multiplicand2x0 + multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4,
                multiplicand2x0 + multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4,
                multiplicand2x0 + multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4,
                (multiplicand3x0 * multiplier0x0) + (multiplicand3x1 * multiplier1x0) + multiplicand3x2 + multiplicand3x3 + multiplicand3x4,
                (multiplicand3x0 * multiplier0x1) + (multiplicand3x1 * multiplier1x1) + multiplicand3x2 + multiplicand3x3 + multiplicand3x4,
                multiplicand3x0 + multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4,
                multiplicand3x0 + multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4,
                multiplicand3x0 + multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4,
                (multiplicand4x0 * multiplier0x0) + (multiplicand4x1 * multiplier1x0) + multiplicand4x2 + multiplicand4x3 + multiplicand4x4,
                (multiplicand4x0 * multiplier0x1) + (multiplicand4x1 * multiplier1x1) + multiplicand4x2 + multiplicand4x3 + multiplicand4x4,
                multiplicand4x0 + multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4,
                multiplicand4x0 + multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4,
                multiplicand4x0 + multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4
            );

        /// <summary>
        /// Multiply3x3x5x5s the specified multiplicand M0X0.
        /// </summary>
        /// <param name="multiplicand0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicand0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicand0x2">The multiplicand M0X2.</param>
        /// <param name="multiplicand1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand2x0">The multiplicand M2X0.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplier0x0">The multiplier M0X0.</param>
        /// <param name="multiplier0x1">The multiplier M0X1.</param>
        /// <param name="multiplier0x2">The multiplier M0X2.</param>
        /// <param name="multiplier0x3">The multiplier M0X3.</param>
        /// <param name="multiplier0x4">The multiplier M0X4.</param>
        /// <param name="multiplier1x0">The multiplier M1X0.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier1x4">The multiplier M1X4.</param>
        /// <param name="multiplier2x0">The multiplier M2X0.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier2x4">The multiplier M2X4.</param>
        /// <param name="multiplier3x0">The multiplier M3X0.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <param name="multiplier3x4">The multiplier M3X4.</param>
        /// <param name="multiplier4x0">The multiplier M4X0.</param>
        /// <param name="multiplier4x1">The multiplier M4X1.</param>
        /// <param name="multiplier4x2">The multiplier M4X2.</param>
        /// <param name="multiplier4x3">The multiplier M4X3.</param>
        /// <param name="multiplier4x4">The multiplier M4X4.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M0x0, double M0x1, double M0x2, double M0x3, double M0x4,
            double M1x0, double M1x1, double M1x2, double M1x3, double M1x4,
            double M2x0, double M2x1, double M2x2, double M2x3, double M2x4,
            double M3x0, double M3x1, double M3x2, double M3x3, double M3x4,
            double M4x0, double M4x1, double M4x2, double M4x3, double M4x4
            ) Multiply3x3x5x5(
            double multiplicand0x0, double multiplicand0x1, double multiplicand0x2,
            double multiplicand1x0, double multiplicand1x1, double multiplicand1x2,
            double multiplicand2x0, double multiplicand2x1, double multiplicand2x2,
            double multiplier0x0, double multiplier0x1, double multiplier0x2, double multiplier0x3, double multiplier0x4,
            double multiplier1x0, double multiplier1x1, double multiplier1x2, double multiplier1x3, double multiplier1x4,
            double multiplier2x0, double multiplier2x1, double multiplier2x2, double multiplier2x3, double multiplier2x4,
            double multiplier3x0, double multiplier3x1, double multiplier3x2, double multiplier3x3, double multiplier3x4,
            double multiplier4x0, double multiplier4x1, double multiplier4x2, double multiplier4x3, double multiplier4x4)
            => (
                (multiplicand0x0 * multiplier0x0) + (multiplicand0x1 * multiplier1x0) + (multiplicand0x2 * multiplier2x0) + multiplier3x0 + multiplier4x0,
                (multiplicand0x0 * multiplier0x1) + (multiplicand0x1 * multiplier1x1) + (multiplicand0x2 * multiplier2x1) + multiplier3x1 + multiplier4x1,
                (multiplicand0x0 * multiplier0x2) + (multiplicand0x1 * multiplier1x2) + (multiplicand0x2 * multiplier2x2) + multiplier3x2 + multiplier4x2,
                (multiplicand0x0 * multiplier0x3) + (multiplicand0x1 * multiplier1x3) + (multiplicand0x2 * multiplier2x3) + multiplier3x3 + multiplier4x3,
                (multiplicand0x0 * multiplier0x4) + (multiplicand0x1 * multiplier1x4) + (multiplicand0x2 * multiplier2x4) + multiplier3x4 + multiplier4x4,
                (multiplicand1x0 * multiplier0x0) + (multiplicand1x1 * multiplier1x0) + (multiplicand1x2 * multiplier2x0) + multiplier3x0 + multiplier4x0,
                (multiplicand1x0 * multiplier0x1) + (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1) + multiplier3x1 + multiplier4x1,
                (multiplicand1x0 * multiplier0x2) + (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2) + multiplier3x2 + multiplier4x2,
                (multiplicand1x0 * multiplier0x3) + (multiplicand1x1 * multiplier1x3) + (multiplicand1x2 * multiplier2x3) + multiplier3x3 + multiplier4x3,
                (multiplicand1x0 * multiplier0x4) + (multiplicand1x1 * multiplier1x4) + (multiplicand1x2 * multiplier2x4) + multiplier3x4 + multiplier4x4,
                (multiplicand2x0 * multiplier0x0) + (multiplicand2x1 * multiplier1x0) + (multiplicand2x2 * multiplier2x0) + multiplier3x0 + multiplier4x0,
                (multiplicand2x0 * multiplier0x1) + (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1) + multiplier3x1 + multiplier4x1,
                (multiplicand2x0 * multiplier0x2) + (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2) + multiplier3x2 + multiplier4x2,
                (multiplicand2x0 * multiplier0x3) + (multiplicand2x1 * multiplier1x3) + (multiplicand2x2 * multiplier2x3) + multiplier3x3 + multiplier4x3,
                (multiplicand2x0 * multiplier0x4) + (multiplicand2x1 * multiplier1x4) + (multiplicand2x2 * multiplier2x4) + multiplier3x4 + multiplier4x4,
                multiplier0x0 + multiplier1x0 + multiplier2x0 + multiplier3x0 + multiplier4x0,
                multiplier0x1 + multiplier1x1 + multiplier2x1 + multiplier3x1 + multiplier4x1,
                multiplier0x2 + multiplier1x2 + multiplier2x2 + multiplier3x2 + multiplier4x2,
                multiplier0x3 + multiplier1x3 + multiplier2x3 + multiplier3x3 + multiplier4x3,
                multiplier0x4 + multiplier1x4 + multiplier2x4 + multiplier3x4 + multiplier4x4,
                multiplier0x0 + multiplier1x0 + multiplier2x0 + multiplier3x0 + multiplier4x0,
                multiplier0x1 + multiplier1x1 + multiplier2x1 + multiplier3x1 + multiplier4x1,
                multiplier0x2 + multiplier1x2 + multiplier2x2 + multiplier3x2 + multiplier4x2,
                multiplier0x3 + multiplier1x3 + multiplier2x3 + multiplier3x3 + multiplier4x3,
                multiplier0x4 + multiplier1x4 + multiplier2x4 + multiplier3x4 + multiplier4x4
            );

        /// <summary>
        /// Multiply5x5x3x3s the specified multiplicand M0X0.
        /// </summary>
        /// <param name="multiplicand0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicand0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicand0x2">The multiplicand M0X2.</param>
        /// <param name="multiplicand0x3">The multiplicand M0X3.</param>
        /// <param name="multiplicand0x4">The multiplicand M0X4.</param>
        /// <param name="multiplicand1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicand2x0">The multiplicand M2X0.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicand3x0">The multiplicand M3X0.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicand3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicand4x0">The multiplicand M4X0.</param>
        /// <param name="multiplicand4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicand4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicand4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicand4x4">The multiplicand M4X4.</param>
        /// <param name="multiplier0x0">The multiplier M0X0.</param>
        /// <param name="multiplier0x1">The multiplier M0X1.</param>
        /// <param name="multiplier0x2">The multiplier M0X2.</param>
        /// <param name="multiplier1x0">The multiplier M1X0.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier2x0">The multiplier M2X0.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M0x0, double M0x1, double M0x2, double M0x3, double M0x4,
            double M1x0, double M1x1, double M1x2, double M1x3, double M1x4,
            double M2x0, double M2x1, double M2x2, double M2x3, double M2x4,
            double M3x0, double M3x1, double M3x2, double M3x3, double M3x4,
            double M4x0, double M4x1, double M4x2, double M4x3, double M4x4
            ) Multiply5x5x3x3(
            double multiplicand0x0, double multiplicand0x1, double multiplicand0x2, double multiplicand0x3, double multiplicand0x4,
            double multiplicand1x0, double multiplicand1x1, double multiplicand1x2, double multiplicand1x3, double multiplicand1x4,
            double multiplicand2x0, double multiplicand2x1, double multiplicand2x2, double multiplicand2x3, double multiplicand2x4,
            double multiplicand3x0, double multiplicand3x1, double multiplicand3x2, double multiplicand3x3, double multiplicand3x4,
            double multiplicand4x0, double multiplicand4x1, double multiplicand4x2, double multiplicand4x3, double multiplicand4x4,
            double multiplier0x0, double multiplier0x1, double multiplier0x2,
            double multiplier1x0, double multiplier1x1, double multiplier1x2,
            double multiplier2x0, double multiplier2x1, double multiplier2x2)
            => (
                (multiplicand0x0 * multiplier0x0) + (multiplicand0x1 * multiplier1x0) + (multiplicand0x2 * multiplier2x0) + multiplicand0x3 + multiplicand0x4,
                (multiplicand0x0 * multiplier0x1) + (multiplicand0x1 * multiplier1x1) + (multiplicand0x2 * multiplier2x1) + multiplicand0x3 + multiplicand0x4,
                (multiplicand0x0 * multiplier0x2) + (multiplicand0x1 * multiplier1x2) + (multiplicand0x2 * multiplier2x2) + multiplicand0x3 + multiplicand0x4,
                multiplicand0x0 + multiplicand0x1 + multiplicand0x2 + multiplicand0x3 + multiplicand0x4,
                multiplicand0x0 + multiplicand0x1 + multiplicand0x2 + multiplicand0x3 + multiplicand0x4,
                (multiplicand1x0 * multiplier0x0) + (multiplicand1x1 * multiplier1x0) + (multiplicand1x2 * multiplier2x0) + multiplicand1x3 + multiplicand1x4,
                (multiplicand1x0 * multiplier0x1) + (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1) + multiplicand1x3 + multiplicand1x4,
                (multiplicand1x0 * multiplier0x2) + (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2) + multiplicand1x3 + multiplicand1x4,
                multiplicand1x0 + multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4,
                multiplicand1x0 + multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4,
                (multiplicand2x0 * multiplier0x0) + (multiplicand2x1 * multiplier1x0) + (multiplicand2x2 * multiplier2x0) + multiplicand2x3 + multiplicand2x4,
                (multiplicand2x0 * multiplier0x1) + (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1) + multiplicand2x3 + multiplicand2x4,
                (multiplicand2x0 * multiplier0x2) + (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2) + multiplicand2x3 + multiplicand2x4,
                multiplicand2x0 + multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4,
                multiplicand2x0 + multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4,
                (multiplicand3x0 * multiplier0x0) + (multiplicand3x1 * multiplier1x0) + (multiplicand3x2 * multiplier2x0) + multiplicand3x3 + multiplicand3x4,
                (multiplicand3x0 * multiplier0x1) + (multiplicand3x1 * multiplier1x1) + (multiplicand3x2 * multiplier2x1) + multiplicand3x3 + multiplicand3x4,
                (multiplicand3x0 * multiplier0x2) + (multiplicand3x1 * multiplier1x2) + (multiplicand3x2 * multiplier2x2) + multiplicand3x3 + multiplicand3x4,
                multiplicand3x0 + multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4,
                multiplicand3x0 + multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4,
                (multiplicand4x0 * multiplier0x0) + (multiplicand4x1 * multiplier1x0) + (multiplicand4x2 * multiplier2x0) + multiplicand4x3 + multiplicand4x4,
                (multiplicand4x0 * multiplier0x1) + (multiplicand4x1 * multiplier1x1) + (multiplicand4x2 * multiplier2x1) + multiplicand4x3 + multiplicand4x4,
                (multiplicand4x0 * multiplier0x2) + (multiplicand4x1 * multiplier1x2) + (multiplicand4x2 * multiplier2x2) + multiplicand4x3 + multiplicand4x4,
                multiplicand4x0 + multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4,
                multiplicand4x0 + multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4
            );

        /// <summary>
        /// Multiply4x4x5x5s the specified multiplicand M0X0.
        /// </summary>
        /// <param name="multiplicand0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicand0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicand0x2">The multiplicand M0X2.</param>
        /// <param name="multiplicand0x3">The multiplicand M0X3.</param>
        /// <param name="multiplicand1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand2x0">The multiplicand M2X0.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand3x0">The multiplicand M3X0.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplier0x0">The multiplier M0X0.</param>
        /// <param name="multiplier0x1">The multiplier M0X1.</param>
        /// <param name="multiplier0x2">The multiplier M0X2.</param>
        /// <param name="multiplier0x3">The multiplier M0X3.</param>
        /// <param name="multiplier0x4">The multiplier M0X4.</param>
        /// <param name="multiplier1x0">The multiplier M1X0.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier1x4">The multiplier M1X4.</param>
        /// <param name="multiplier2x0">The multiplier M2X0.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier2x4">The multiplier M2X4.</param>
        /// <param name="multiplier3x0">The multiplier M3X0.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <param name="multiplier3x4">The multiplier M3X4.</param>
        /// <param name="multiplier4x0">The multiplier M4X0.</param>
        /// <param name="multiplier4x1">The multiplier M4X1.</param>
        /// <param name="multiplier4x2">The multiplier M4X2.</param>
        /// <param name="multiplier4x3">The multiplier M4X3.</param>
        /// <param name="multiplier4x4">The multiplier M4X4.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M0x0, double M0x1, double M0x2, double M0x3, double M0x4,
            double M1x0, double M1x1, double M1x2, double M1x3, double M1x4,
            double M2x0, double M2x1, double M2x2, double M2x3, double M2x4,
            double M3x0, double M3x1, double M3x2, double M3x3, double M3x4,
            double M4x0, double M4x1, double M4x2, double M4x3, double M4x4
            ) Multiply4x4x5x5(
            double multiplicand0x0, double multiplicand0x1, double multiplicand0x2, double multiplicand0x3,
            double multiplicand1x0, double multiplicand1x1, double multiplicand1x2, double multiplicand1x3,
            double multiplicand2x0, double multiplicand2x1, double multiplicand2x2, double multiplicand2x3,
            double multiplicand3x0, double multiplicand3x1, double multiplicand3x2, double multiplicand3x3,
            double multiplier0x0, double multiplier0x1, double multiplier0x2, double multiplier0x3, double multiplier0x4,
            double multiplier1x0, double multiplier1x1, double multiplier1x2, double multiplier1x3, double multiplier1x4,
            double multiplier2x0, double multiplier2x1, double multiplier2x2, double multiplier2x3, double multiplier2x4,
            double multiplier3x0, double multiplier3x1, double multiplier3x2, double multiplier3x3, double multiplier3x4,
            double multiplier4x0, double multiplier4x1, double multiplier4x2, double multiplier4x3, double multiplier4x4)
            => (
                (multiplicand0x0 * multiplier0x0) + (multiplicand0x1 * multiplier1x0) + (multiplicand0x2 * multiplier2x0) + (multiplicand0x3 * multiplier3x0) + multiplier4x0,
                (multiplicand0x0 * multiplier0x1) + (multiplicand0x1 * multiplier1x1) + (multiplicand0x2 * multiplier2x1) + (multiplicand0x3 * multiplier3x1) + multiplier4x1,
                (multiplicand0x0 * multiplier0x2) + (multiplicand0x1 * multiplier1x2) + (multiplicand0x2 * multiplier2x2) + (multiplicand0x3 * multiplier3x2) + multiplier4x2,
                (multiplicand0x0 * multiplier0x3) + (multiplicand0x1 * multiplier1x3) + (multiplicand0x2 * multiplier2x3) + (multiplicand0x3 * multiplier3x3) + multiplier4x3,
                (multiplicand0x0 * multiplier0x4) + (multiplicand0x1 * multiplier1x4) + (multiplicand0x2 * multiplier2x4) + (multiplicand0x3 * multiplier3x4) + multiplier4x4,
                (multiplicand1x0 * multiplier0x0) + (multiplicand1x1 * multiplier1x0) + (multiplicand1x2 * multiplier2x0) + (multiplicand1x3 * multiplier3x0) + multiplier4x0,
                (multiplicand1x0 * multiplier0x1) + (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1) + (multiplicand1x3 * multiplier3x1) + multiplier4x1,
                (multiplicand1x0 * multiplier0x2) + (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2) + (multiplicand1x3 * multiplier3x2) + multiplier4x2,
                (multiplicand1x0 * multiplier0x3) + (multiplicand1x1 * multiplier1x3) + (multiplicand1x2 * multiplier2x3) + (multiplicand1x3 * multiplier3x3) + multiplier4x3,
                (multiplicand1x0 * multiplier0x4) + (multiplicand1x1 * multiplier1x4) + (multiplicand1x2 * multiplier2x4) + (multiplicand1x3 * multiplier3x4) + multiplier4x4,
                (multiplicand2x0 * multiplier0x0) + (multiplicand2x1 * multiplier1x0) + (multiplicand2x2 * multiplier2x0) + (multiplicand2x3 * multiplier3x0) + multiplier4x0,
                (multiplicand2x0 * multiplier0x1) + (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1) + (multiplicand2x3 * multiplier3x1) + multiplier4x1,
                (multiplicand2x0 * multiplier0x2) + (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2) + (multiplicand2x3 * multiplier3x2) + multiplier4x2,
                (multiplicand2x0 * multiplier0x3) + (multiplicand2x1 * multiplier1x3) + (multiplicand2x2 * multiplier2x3) + (multiplicand2x3 * multiplier3x3) + multiplier4x3,
                (multiplicand2x0 * multiplier0x4) + (multiplicand2x1 * multiplier1x4) + (multiplicand2x2 * multiplier2x4) + (multiplicand2x3 * multiplier3x4) + multiplier4x4,
                (multiplicand3x0 * multiplier0x0) + (multiplicand3x1 * multiplier1x0) + (multiplicand3x2 * multiplier2x0) + (multiplicand3x3 * multiplier3x0) + multiplier4x0,
                (multiplicand3x0 * multiplier0x1) + (multiplicand3x1 * multiplier1x1) + (multiplicand3x2 * multiplier2x1) + (multiplicand3x3 * multiplier3x1) + multiplier4x1,
                (multiplicand3x0 * multiplier0x2) + (multiplicand3x1 * multiplier1x2) + (multiplicand3x2 * multiplier2x2) + (multiplicand3x3 * multiplier3x2) + multiplier4x2,
                (multiplicand3x0 * multiplier0x3) + (multiplicand3x1 * multiplier1x3) + (multiplicand3x2 * multiplier2x3) + (multiplicand3x3 * multiplier3x3) + multiplier4x3,
                (multiplicand3x0 * multiplier0x4) + (multiplicand3x1 * multiplier1x4) + (multiplicand3x2 * multiplier2x4) + (multiplicand3x3 * multiplier3x4) + multiplier4x4,
                multiplier0x0 + multiplier1x0 + multiplier2x0 + multiplier3x0 + multiplier4x0,
                multiplier0x1 + multiplier1x1 + multiplier2x1 + multiplier3x1 + multiplier4x1,
                multiplier0x2 + multiplier1x2 + multiplier2x2 + multiplier3x2 + multiplier4x2,
                multiplier0x3 + multiplier1x3 + multiplier2x3 + multiplier3x3 + multiplier4x3,
                multiplier0x4 + multiplier1x4 + multiplier2x4 + multiplier3x4 + multiplier4x4
            );

        /// <summary>
        /// Multiply5x5x4x4s the specified multiplicand M0X0.
        /// </summary>
        /// <param name="multiplicand0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicand0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicand0x2">The multiplicand M0X2.</param>
        /// <param name="multiplicand0x3">The multiplicand M0X3.</param>
        /// <param name="multiplicand0x4">The multiplicand M0X4.</param>
        /// <param name="multiplicand1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicand2x0">The multiplicand M2X0.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicand3x0">The multiplicand M3X0.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicand3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicand4x0">The multiplicand M4X0.</param>
        /// <param name="multiplicand4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicand4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicand4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicand4x4">The multiplicand M4X4.</param>
        /// <param name="multiplier0x0">The multiplier M0X0.</param>
        /// <param name="multiplier0x1">The multiplier M0X1.</param>
        /// <param name="multiplier0x2">The multiplier M0X2.</param>
        /// <param name="multiplier0x3">The multiplier M0X3.</param>
        /// <param name="multiplier1x0">The multiplier M1X0.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier2x0">The multiplier M2X0.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier3x0">The multiplier M3X0.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M0x0, double M0x1, double M0x2, double M0x3, double M0x4,
            double M1x0, double M1x1, double M1x2, double M1x3, double M1x4,
            double M2x0, double M2x1, double M2x2, double M2x3, double M2x4,
            double M3x0, double M3x1, double M3x2, double M3x3, double M3x4,
            double M4x0, double M4x1, double M4x2, double M4x3, double M4x4
            ) Multiply5x5x4x4(
            double multiplicand0x0, double multiplicand0x1, double multiplicand0x2, double multiplicand0x3, double multiplicand0x4,
            double multiplicand1x0, double multiplicand1x1, double multiplicand1x2, double multiplicand1x3, double multiplicand1x4,
            double multiplicand2x0, double multiplicand2x1, double multiplicand2x2, double multiplicand2x3, double multiplicand2x4,
            double multiplicand3x0, double multiplicand3x1, double multiplicand3x2, double multiplicand3x3, double multiplicand3x4,
            double multiplicand4x0, double multiplicand4x1, double multiplicand4x2, double multiplicand4x3, double multiplicand4x4,
            double multiplier0x0, double multiplier0x1, double multiplier0x2, double multiplier0x3,
            double multiplier1x0, double multiplier1x1, double multiplier1x2, double multiplier1x3,
            double multiplier2x0, double multiplier2x1, double multiplier2x2, double multiplier2x3,
            double multiplier3x0, double multiplier3x1, double multiplier3x2, double multiplier3x3)
            => (
                (multiplicand0x0 * multiplier0x0) + (multiplicand0x1 * multiplier1x0) + (multiplicand0x2 * multiplier2x0) + (multiplicand0x3 * multiplier3x0) + multiplicand0x4,
                (multiplicand0x0 * multiplier0x1) + (multiplicand0x1 * multiplier1x1) + (multiplicand0x2 * multiplier2x1) + (multiplicand0x3 * multiplier3x1) + multiplicand0x4,
                (multiplicand0x0 * multiplier0x2) + (multiplicand0x1 * multiplier1x2) + (multiplicand0x2 * multiplier2x2) + (multiplicand0x3 * multiplier3x2) + multiplicand0x4,
                (multiplicand0x0 * multiplier0x3) + (multiplicand0x1 * multiplier1x3) + (multiplicand0x2 * multiplier2x3) + (multiplicand0x3 * multiplier3x3) + multiplicand0x4,
                multiplicand0x0 + multiplicand0x1 + multiplicand0x2 + multiplicand0x3 + multiplicand0x4,
                (multiplicand1x0 * multiplier0x0) + (multiplicand1x1 * multiplier1x0) + (multiplicand1x2 * multiplier2x0) + (multiplicand1x3 * multiplier3x0) + multiplicand1x4,
                (multiplicand1x0 * multiplier0x1) + (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1) + (multiplicand1x3 * multiplier3x1) + multiplicand1x4,
                (multiplicand1x0 * multiplier0x2) + (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2) + (multiplicand1x3 * multiplier3x2) + multiplicand1x4,
                (multiplicand1x0 * multiplier0x3) + (multiplicand1x1 * multiplier1x3) + (multiplicand1x2 * multiplier2x3) + (multiplicand1x3 * multiplier3x3) + multiplicand1x4,
                multiplicand1x0 + multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4,
                (multiplicand2x0 * multiplier0x0) + (multiplicand2x1 * multiplier1x0) + (multiplicand2x2 * multiplier2x0) + (multiplicand2x3 * multiplier3x0) + multiplicand2x4,
                (multiplicand2x0 * multiplier0x1) + (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1) + (multiplicand2x3 * multiplier3x1) + multiplicand2x4,
                (multiplicand2x0 * multiplier0x2) + (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2) + (multiplicand2x3 * multiplier3x2) + multiplicand2x4,
                (multiplicand2x0 * multiplier0x3) + (multiplicand2x1 * multiplier1x3) + (multiplicand2x2 * multiplier2x3) + (multiplicand2x3 * multiplier3x3) + multiplicand2x4,
                multiplicand2x0 + multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4,
                (multiplicand3x0 * multiplier0x0) + (multiplicand3x1 * multiplier1x0) + (multiplicand3x2 * multiplier2x0) + (multiplicand3x3 * multiplier3x0) + multiplicand3x4,
                (multiplicand3x0 * multiplier0x1) + (multiplicand3x1 * multiplier1x1) + (multiplicand3x2 * multiplier2x1) + (multiplicand3x3 * multiplier3x1) + multiplicand3x4,
                (multiplicand3x0 * multiplier0x2) + (multiplicand3x1 * multiplier1x2) + (multiplicand3x2 * multiplier2x2) + (multiplicand3x3 * multiplier3x2) + multiplicand3x4,
                (multiplicand3x0 * multiplier0x3) + (multiplicand3x1 * multiplier1x3) + (multiplicand3x2 * multiplier2x3) + (multiplicand3x3 * multiplier3x3) + multiplicand3x4,
                multiplicand3x0 + multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4,
                (multiplicand4x0 * multiplier0x0) + (multiplicand4x1 * multiplier1x0) + (multiplicand4x2 * multiplier2x0) + (multiplicand4x3 * multiplier3x0) + multiplicand4x4,
                (multiplicand4x0 * multiplier0x1) + (multiplicand4x1 * multiplier1x1) + (multiplicand4x2 * multiplier2x1) + (multiplicand4x3 * multiplier3x1) + multiplicand4x4,
                (multiplicand4x0 * multiplier0x2) + (multiplicand4x1 * multiplier1x2) + (multiplicand4x2 * multiplier2x2) + (multiplicand4x3 * multiplier3x2) + multiplicand4x4,
                (multiplicand4x0 * multiplier0x3) + (multiplicand4x1 * multiplier1x3) + (multiplicand4x2 * multiplier2x3) + (multiplicand4x3 * multiplier3x3) + multiplicand4x4,
                multiplicand4x0 + multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4
            );

        /// <summary>
        /// Multiply5x5x5x5s the specified multiplicand M0X0.
        /// </summary>
        /// <param name="multiplicand0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicand0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicand0x2">The multiplicand M0X2.</param>
        /// <param name="multiplicand0x3">The multiplicand M0X3.</param>
        /// <param name="multiplicand0x4">The multiplicand M0X4.</param>
        /// <param name="multiplicand1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicand2x0">The multiplicand M2X0.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicand3x0">The multiplicand M3X0.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicand3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicand4x0">The multiplicand M4X0.</param>
        /// <param name="multiplicand4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicand4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicand4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicand4x4">The multiplicand M4X4.</param>
        /// <param name="multiplier0x0">The multiplier M0X0.</param>
        /// <param name="multiplier0x1">The multiplier M0X1.</param>
        /// <param name="multiplier0x2">The multiplier M0X2.</param>
        /// <param name="multiplier0x3">The multiplier M0X3.</param>
        /// <param name="multiplier0x4">The multiplier M0X4.</param>
        /// <param name="multiplier1x0">The multiplier M1X0.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier1x4">The multiplier M1X4.</param>
        /// <param name="multiplier2x0">The multiplier M2X0.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier2x4">The multiplier M2X4.</param>
        /// <param name="multiplier3x0">The multiplier M3X0.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <param name="multiplier3x4">The multiplier M3X4.</param>
        /// <param name="multiplier4x0">The multiplier M4X0.</param>
        /// <param name="multiplier4x1">The multiplier M4X1.</param>
        /// <param name="multiplier4x2">The multiplier M4X2.</param>
        /// <param name="multiplier4x3">The multiplier M4X3.</param>
        /// <param name="multiplier4x4">The multiplier M4X4.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M0x0, double M0x1, double M0x2, double M0x3, double M0x4,
            double M1x0, double M1x1, double M1x2, double M1x3, double M1x4,
            double M2x0, double M2x1, double M2x2, double M2x3, double M2x4,
            double M3x0, double M3x1, double M3x2, double M3x3, double M3x4,
            double M4x0, double M4x1, double M4x2, double M4x3, double M4x4
            ) Multiply5x5x5x5(
            double multiplicand0x0, double multiplicand0x1, double multiplicand0x2, double multiplicand0x3, double multiplicand0x4,
            double multiplicand1x0, double multiplicand1x1, double multiplicand1x2, double multiplicand1x3, double multiplicand1x4,
            double multiplicand2x0, double multiplicand2x1, double multiplicand2x2, double multiplicand2x3, double multiplicand2x4,
            double multiplicand3x0, double multiplicand3x1, double multiplicand3x2, double multiplicand3x3, double multiplicand3x4,
            double multiplicand4x0, double multiplicand4x1, double multiplicand4x2, double multiplicand4x3, double multiplicand4x4,
            double multiplier0x0, double multiplier0x1, double multiplier0x2, double multiplier0x3, double multiplier0x4,
            double multiplier1x0, double multiplier1x1, double multiplier1x2, double multiplier1x3, double multiplier1x4,
            double multiplier2x0, double multiplier2x1, double multiplier2x2, double multiplier2x3, double multiplier2x4,
            double multiplier3x0, double multiplier3x1, double multiplier3x2, double multiplier3x3, double multiplier3x4,
            double multiplier4x0, double multiplier4x1, double multiplier4x2, double multiplier4x3, double multiplier4x4)
            => (
                (multiplicand0x0 * multiplier0x0) + (multiplicand0x1 * multiplier1x0) + (multiplicand0x2 * multiplier2x0) + (multiplicand0x3 * multiplier3x0) + (multiplicand0x4 * multiplier4x0),
                (multiplicand0x0 * multiplier0x1) + (multiplicand0x1 * multiplier1x1) + (multiplicand0x2 * multiplier2x1) + (multiplicand0x3 * multiplier3x1) + (multiplicand0x4 * multiplier4x1),
                (multiplicand0x0 * multiplier0x2) + (multiplicand0x1 * multiplier1x2) + (multiplicand0x2 * multiplier2x2) + (multiplicand0x3 * multiplier3x2) + (multiplicand0x4 * multiplier4x2),
                (multiplicand0x0 * multiplier0x3) + (multiplicand0x1 * multiplier1x3) + (multiplicand0x2 * multiplier2x3) + (multiplicand0x3 * multiplier3x3) + (multiplicand0x4 * multiplier4x3),
                (multiplicand0x0 * multiplier0x4) + (multiplicand0x1 * multiplier1x4) + (multiplicand0x2 * multiplier2x4) + (multiplicand0x3 * multiplier3x4) + (multiplicand0x4 * multiplier4x4),
                (multiplicand1x0 * multiplier0x0) + (multiplicand1x1 * multiplier1x0) + (multiplicand1x2 * multiplier2x0) + (multiplicand1x3 * multiplier3x0) + (multiplicand1x4 * multiplier4x0),
                (multiplicand1x0 * multiplier0x1) + (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1) + (multiplicand1x3 * multiplier3x1) + (multiplicand1x4 * multiplier4x1),
                (multiplicand1x0 * multiplier0x2) + (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2) + (multiplicand1x3 * multiplier3x2) + (multiplicand1x4 * multiplier4x2),
                (multiplicand1x0 * multiplier0x3) + (multiplicand1x1 * multiplier1x3) + (multiplicand1x2 * multiplier2x3) + (multiplicand1x3 * multiplier3x3) + (multiplicand1x4 * multiplier4x3),
                (multiplicand1x0 * multiplier0x4) + (multiplicand1x1 * multiplier1x4) + (multiplicand1x2 * multiplier2x4) + (multiplicand1x3 * multiplier3x4) + (multiplicand1x4 * multiplier4x4),
                (multiplicand2x0 * multiplier0x0) + (multiplicand2x1 * multiplier1x0) + (multiplicand2x2 * multiplier2x0) + (multiplicand2x3 * multiplier3x0) + (multiplicand2x4 * multiplier4x0),
                (multiplicand2x0 * multiplier0x1) + (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1) + (multiplicand2x3 * multiplier3x1) + (multiplicand2x4 * multiplier4x1),
                (multiplicand2x0 * multiplier0x2) + (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2) + (multiplicand2x3 * multiplier3x2) + (multiplicand2x4 * multiplier4x2),
                (multiplicand2x0 * multiplier0x3) + (multiplicand2x1 * multiplier1x3) + (multiplicand2x2 * multiplier2x3) + (multiplicand2x3 * multiplier3x3) + (multiplicand2x4 * multiplier4x3),
                (multiplicand2x0 * multiplier0x4) + (multiplicand2x1 * multiplier1x4) + (multiplicand2x2 * multiplier2x4) + (multiplicand2x3 * multiplier3x4) + (multiplicand2x4 * multiplier4x4),
                (multiplicand3x0 * multiplier0x0) + (multiplicand3x1 * multiplier1x0) + (multiplicand3x2 * multiplier2x0) + (multiplicand3x3 * multiplier3x0) + (multiplicand3x4 * multiplier4x0),
                (multiplicand3x0 * multiplier0x1) + (multiplicand3x1 * multiplier1x1) + (multiplicand3x2 * multiplier2x1) + (multiplicand3x3 * multiplier3x1) + (multiplicand3x4 * multiplier4x1),
                (multiplicand3x0 * multiplier0x2) + (multiplicand3x1 * multiplier1x2) + (multiplicand3x2 * multiplier2x2) + (multiplicand3x3 * multiplier3x2) + (multiplicand3x4 * multiplier4x2),
                (multiplicand3x0 * multiplier0x3) + (multiplicand3x1 * multiplier1x3) + (multiplicand3x2 * multiplier2x3) + (multiplicand3x3 * multiplier3x3) + (multiplicand3x4 * multiplier4x3),
                (multiplicand3x0 * multiplier0x4) + (multiplicand3x1 * multiplier1x4) + (multiplicand3x2 * multiplier2x4) + (multiplicand3x3 * multiplier3x4) + (multiplicand3x4 * multiplier4x4),
                (multiplicand4x0 * multiplier0x0) + (multiplicand4x1 * multiplier1x0) + (multiplicand4x2 * multiplier2x0) + (multiplicand4x3 * multiplier3x0) + (multiplicand4x4 * multiplier4x0),
                (multiplicand4x0 * multiplier0x1) + (multiplicand4x1 * multiplier1x1) + (multiplicand4x2 * multiplier2x1) + (multiplicand4x3 * multiplier3x1) + (multiplicand4x4 * multiplier4x1),
                (multiplicand4x0 * multiplier0x2) + (multiplicand4x1 * multiplier1x2) + (multiplicand4x2 * multiplier2x2) + (multiplicand4x3 * multiplier3x2) + (multiplicand4x4 * multiplier4x2),
                (multiplicand4x0 * multiplier0x3) + (multiplicand4x1 * multiplier1x3) + (multiplicand4x2 * multiplier2x3) + (multiplicand4x3 * multiplier3x3) + (multiplicand4x4 * multiplier4x3),
                (multiplicand4x0 * multiplier0x4) + (multiplicand4x1 * multiplier1x4) + (multiplicand4x2 * multiplier2x4) + (multiplicand4x3 * multiplier3x4) + (multiplicand4x4 * multiplier4x4)
            );

        /// <summary>
        /// Multiply2x2x6x6s the specified multiplicand M0X0.
        /// </summary>
        /// <param name="multiplicand0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicand0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicand1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplier0x0">The multiplier M0X0.</param>
        /// <param name="multiplier0x1">The multiplier M0X1.</param>
        /// <param name="multiplier0x2">The multiplier M0X2.</param>
        /// <param name="multiplier0x3">The multiplier M0X3.</param>
        /// <param name="multiplier0x4">The multiplier M0X4.</param>
        /// <param name="multiplier0x5">The multiplier M0X5.</param>
        /// <param name="multiplier1x0">The multiplier M1X0.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier1x4">The multiplier M1X4.</param>
        /// <param name="multiplier1x5">The multiplier M1X5.</param>
        /// <param name="multiplier2x0">The multiplier M2X0.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier2x4">The multiplier M2X4.</param>
        /// <param name="multiplier2x5">The multiplier M2X5.</param>
        /// <param name="multiplier3x0">The multiplier M3X0.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <param name="multiplier3x4">The multiplier M3X4.</param>
        /// <param name="multiplier3x5">The multiplier M3X5.</param>
        /// <param name="multiplier4x0">The multiplier M4X0.</param>
        /// <param name="multiplier4x1">The multiplier M4X1.</param>
        /// <param name="multiplier4x2">The multiplier M4X2.</param>
        /// <param name="multiplier4x3">The multiplier M4X3.</param>
        /// <param name="multiplier4x4">The multiplier M4X4.</param>
        /// <param name="multiplier4x5">The multiplier M4X5.</param>
        /// <param name="multiplier5x0">The multiplier M5X0.</param>
        /// <param name="multiplier5x1">The multiplier M5X1.</param>
        /// <param name="multiplier5x2">The multiplier M5X2.</param>
        /// <param name="multiplier5x3">The multiplier M5X3.</param>
        /// <param name="multiplier5x4">The multiplier M5X4.</param>
        /// <param name="multiplier5x5">The multiplier M5X5.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M0x0, double M0x1, double M0x2, double M0x3, double M0x4, double M0x5,
            double M1x0, double M1x1, double M1x2, double M1x3, double M1x4, double M1x5,
            double M2x0, double M2x1, double M2x2, double M2x3, double M2x4, double M2x5,
            double M3x0, double M3x1, double M3x2, double M3x3, double M3x4, double M3x5,
            double M4x0, double M4x1, double M4x2, double M4x3, double M4x4, double M4x5,
            double M5x0, double M5x1, double M5x2, double M5x3, double M5x4, double M5x5
            ) Multiply2x2x6x6(
            double multiplicand0x0, double multiplicand0x1,
            double multiplicand1x0, double multiplicand1x1,
            double multiplier0x0, double multiplier0x1, double multiplier0x2, double multiplier0x3, double multiplier0x4, double multiplier0x5,
            double multiplier1x0, double multiplier1x1, double multiplier1x2, double multiplier1x3, double multiplier1x4, double multiplier1x5,
            double multiplier2x0, double multiplier2x1, double multiplier2x2, double multiplier2x3, double multiplier2x4, double multiplier2x5,
            double multiplier3x0, double multiplier3x1, double multiplier3x2, double multiplier3x3, double multiplier3x4, double multiplier3x5,
            double multiplier4x0, double multiplier4x1, double multiplier4x2, double multiplier4x3, double multiplier4x4, double multiplier4x5,
            double multiplier5x0, double multiplier5x1, double multiplier5x2, double multiplier5x3, double multiplier5x4, double multiplier5x5)
            => (
                multiplicand0x0 * multiplier0x0 + multiplicand0x1 * multiplier1x0 + multiplier2x0 + multiplier3x0 * multiplier4x0 + multiplier5x0,
                multiplicand0x0 * multiplier0x1 + multiplicand0x1 * multiplier1x1 + multiplier2x1 + multiplier3x1 * multiplier4x1 + multiplier5x1,
                multiplicand0x0 * multiplier0x2 + multiplicand0x1 * multiplier1x2 + multiplier2x2 + multiplier3x2 * multiplier4x2 + multiplier5x2,
                multiplicand0x0 * multiplier0x3 + multiplicand0x1 * multiplier1x3 + multiplier2x3 + multiplier3x3 * multiplier4x3 + multiplier5x3,
                multiplicand0x0 * multiplier0x4 + multiplicand0x1 * multiplier1x4 + multiplier2x4 + multiplier3x4 * multiplier4x4 + multiplier5x4,
                multiplicand0x0 * multiplier0x5 + multiplicand0x1 * multiplier1x5 + multiplier2x5 + multiplier3x5 * multiplier4x5 + multiplier5x5,
                multiplicand1x0 * multiplier0x0 + multiplicand1x1 * multiplier1x0 + multiplier2x0 + multiplier3x0 * multiplier4x0 + multiplier5x0,
                multiplicand1x0 * multiplier0x1 + multiplicand1x1 * multiplier1x1 + multiplier2x1 + multiplier3x1 * multiplier4x1 + multiplier5x1,
                multiplicand1x0 * multiplier0x2 + multiplicand1x1 * multiplier1x2 + multiplier2x2 + multiplier3x2 * multiplier4x2 + multiplier5x2,
                multiplicand1x0 * multiplier0x3 + multiplicand1x1 * multiplier1x3 + multiplier2x3 + multiplier3x3 * multiplier4x3 + multiplier5x3,
                multiplicand1x0 * multiplier0x4 + multiplicand1x1 * multiplier1x4 + multiplier2x4 + multiplier3x4 * multiplier4x4 + multiplier5x4,
                multiplicand1x0 * multiplier0x5 + multiplicand1x1 * multiplier1x5 + multiplier2x5 + multiplier3x5 * multiplier4x5 + multiplier5x5,
                multiplier0x0 + multiplier1x0 + multiplier2x0 + multiplier3x0 * multiplier4x0 + multiplier5x0,
                multiplier0x1 + multiplier1x1 + multiplier2x1 + multiplier3x1 * multiplier4x1 + multiplier5x1,
                multiplier0x2 + multiplier1x2 + multiplier2x2 + multiplier3x2 * multiplier4x2 + multiplier5x2,
                multiplier0x3 + multiplier1x3 + multiplier2x3 + multiplier3x3 * multiplier4x3 + multiplier5x3,
                multiplier0x4 + multiplier1x4 + multiplier2x4 + multiplier3x4 * multiplier4x4 + multiplier5x4,
                multiplier0x5 + multiplier1x5 + multiplier2x5 + multiplier3x5 * multiplier4x5 + multiplier5x5,
                multiplier0x0 + multiplier1x0 + multiplier2x0 + multiplier3x0 * multiplier4x0 + multiplier5x0,
                multiplier0x1 + multiplier1x1 + multiplier2x1 + multiplier3x1 * multiplier4x1 + multiplier5x1,
                multiplier0x2 + multiplier1x2 + multiplier2x2 + multiplier3x2 * multiplier4x2 + multiplier5x2,
                multiplier0x3 + multiplier1x3 + multiplier2x3 + multiplier3x3 * multiplier4x3 + multiplier5x3,
                multiplier0x4 + multiplier1x4 + multiplier2x4 + multiplier3x4 * multiplier4x4 + multiplier5x4,
                multiplier0x5 + multiplier1x5 + multiplier2x5 + multiplier3x5 * multiplier4x5 + multiplier5x5,
                multiplier0x0 + multiplier1x0 + multiplier2x0 + multiplier3x0 * multiplier4x0 + multiplier5x0,
                multiplier0x1 + multiplier1x1 + multiplier2x1 + multiplier3x1 * multiplier4x1 + multiplier5x1,
                multiplier0x2 + multiplier1x2 + multiplier2x2 + multiplier3x2 * multiplier4x2 + multiplier5x2,
                multiplier0x3 + multiplier1x3 + multiplier2x3 + multiplier3x3 * multiplier4x3 + multiplier5x3,
                multiplier0x4 + multiplier1x4 + multiplier2x4 + multiplier3x4 * multiplier4x4 + multiplier5x4,
                multiplier0x5 + multiplier1x5 + multiplier2x5 + multiplier3x5 * multiplier4x5 + multiplier5x5,
                multiplier0x0 + multiplier1x0 + multiplier2x0 + multiplier3x0 * multiplier4x0 + multiplier5x0,
                multiplier0x1 + multiplier1x1 + multiplier2x1 + multiplier3x1 * multiplier4x1 + multiplier5x1,
                multiplier0x2 + multiplier1x2 + multiplier2x2 + multiplier3x2 * multiplier4x2 + multiplier5x2,
                multiplier0x3 + multiplier1x3 + multiplier2x3 + multiplier3x3 * multiplier4x3 + multiplier5x3,
                multiplier0x4 + multiplier1x4 + multiplier2x4 + multiplier3x4 * multiplier4x4 + multiplier5x4,
                multiplier0x5 + multiplier1x5 + multiplier2x5 + multiplier3x5 * multiplier4x5 + multiplier5x5
            );

        /// <summary>
        /// Multiply6x6x2x2s the specified multiplicand M0X0.
        /// </summary>
        /// <param name="multiplicand0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicand0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicand0x2">The multiplicand M0X2.</param>
        /// <param name="multiplicand0x3">The multiplicand M0X3.</param>
        /// <param name="multiplicand0x4">The multiplicand M0X4.</param>
        /// <param name="multiplicand0x5">The multiplicand M0X5.</param>
        /// <param name="multiplicand1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicand1x5">The multiplicand M1X5.</param>
        /// <param name="multiplicand2x0">The multiplicand M2X0.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicand2x5">The multiplicand M2X5.</param>
        /// <param name="multiplicand3x0">The multiplicand M3X0.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicand3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicand3x5">The multiplicand M3X5.</param>
        /// <param name="multiplicand4x0">The multiplicand M4X0.</param>
        /// <param name="multiplicand4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicand4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicand4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicand4x4">The multiplicand M4X4.</param>
        /// <param name="multiplicand4x5">The multiplicand M4X5.</param>
        /// <param name="multiplicand5x0">The multiplicand M5X0.</param>
        /// <param name="multiplicand5x1">The multiplicand M5X1.</param>
        /// <param name="multiplicand5x2">The multiplicand M5X2.</param>
        /// <param name="multiplicand5x3">The multiplicand M5X3.</param>
        /// <param name="multiplicand5x4">The multiplicand M5X4.</param>
        /// <param name="multiplicand5x5">The multiplicand M5X5.</param>
        /// <param name="multiplier0x0">The multiplier M0X0.</param>
        /// <param name="multiplier0x1">The multiplier M0X1.</param>
        /// <param name="multiplier1x0">The multiplier M1X0.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M0x0, double M0x1, double M0x2, double M0x3, double M0x4, double M0x5,
            double M1x0, double M1x1, double M1x2, double M1x3, double M1x4, double M1x5,
            double M2x0, double M2x1, double M2x2, double M2x3, double M2x4, double M2x5,
            double M3x0, double M3x1, double M3x2, double M3x3, double M3x4, double M3x5,
            double M4x0, double M4x1, double M4x2, double M4x3, double M4x4, double M4x5,
            double M5x0, double M5x1, double M5x2, double M5x3, double M5x4, double M5x5
            ) Multiply6x6x2x2(
            double multiplicand0x0, double multiplicand0x1, double multiplicand0x2, double multiplicand0x3, double multiplicand0x4, double multiplicand0x5,
            double multiplicand1x0, double multiplicand1x1, double multiplicand1x2, double multiplicand1x3, double multiplicand1x4, double multiplicand1x5,
            double multiplicand2x0, double multiplicand2x1, double multiplicand2x2, double multiplicand2x3, double multiplicand2x4, double multiplicand2x5,
            double multiplicand3x0, double multiplicand3x1, double multiplicand3x2, double multiplicand3x3, double multiplicand3x4, double multiplicand3x5,
            double multiplicand4x0, double multiplicand4x1, double multiplicand4x2, double multiplicand4x3, double multiplicand4x4, double multiplicand4x5,
            double multiplicand5x0, double multiplicand5x1, double multiplicand5x2, double multiplicand5x3, double multiplicand5x4, double multiplicand5x5,
            double multiplier0x0, double multiplier0x1,
            double multiplier1x0, double multiplier1x1)
            => (
                multiplicand0x0 * multiplier0x0 + multiplicand0x1 * multiplier1x0 + multiplicand0x2 + multiplicand0x3 + multiplicand0x4 + multiplicand0x5,
                multiplicand0x0 * multiplier0x1 + multiplicand0x1 * multiplier1x1 + multiplicand0x2 + multiplicand0x3 + multiplicand0x4 + multiplicand0x5,
                multiplicand0x0 + multiplicand0x1 + multiplicand0x2 + multiplicand0x3 + multiplicand0x4 + multiplicand0x5,
                multiplicand0x0 + multiplicand0x1 + multiplicand0x2 + multiplicand0x3 + multiplicand0x4 + multiplicand0x5,
                multiplicand0x0 + multiplicand0x1 + multiplicand0x2 + multiplicand0x3 + multiplicand0x4 + multiplicand0x5,
                multiplicand0x0 + multiplicand0x1 + multiplicand0x2 + multiplicand0x3 + multiplicand0x4 + multiplicand0x5,
                multiplicand1x0 * multiplier0x0 + multiplicand1x1 * multiplier1x0 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5,
                multiplicand1x0 * multiplier0x1 + multiplicand1x1 * multiplier1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5,
                multiplicand1x0 + multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5,
                multiplicand1x0 + multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5,
                multiplicand1x0 + multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5,
                multiplicand1x0 + multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5,
                multiplicand2x0 * multiplier0x0 + multiplicand2x1 * multiplier1x0 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5,
                multiplicand2x0 * multiplier0x1 + multiplicand2x1 * multiplier1x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5,
                multiplicand2x0 + multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5,
                multiplicand2x0 + multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5,
                multiplicand2x0 + multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5,
                multiplicand2x0 + multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5,
                multiplicand3x0 * multiplier0x0 + multiplicand3x1 * multiplier1x0 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5,
                multiplicand3x0 * multiplier0x1 + multiplicand3x1 * multiplier1x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5,
                multiplicand3x0 + multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5,
                multiplicand3x0 + multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5,
                multiplicand3x0 + multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5,
                multiplicand3x0 + multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5,
                multiplicand4x0 * multiplier0x0 + multiplicand4x1 * multiplier1x0 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5,
                multiplicand4x0 * multiplier0x1 + multiplicand4x1 * multiplier1x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5,
                multiplicand4x0 + multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5,
                multiplicand4x0 + multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5,
                multiplicand4x0 + multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5,
                multiplicand4x0 + multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5,
                multiplicand5x0 * multiplier0x0 + multiplicand5x1 * multiplier1x0 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5,
                multiplicand5x0 * multiplier0x1 + multiplicand5x1 * multiplier1x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5,
                multiplicand5x0 + multiplicand5x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5,
                multiplicand5x0 + multiplicand5x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5,
                multiplicand5x0 + multiplicand5x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5,
                multiplicand5x0 + multiplicand5x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5
            );

        /// <summary>
        /// Multiply3x3x6x6s the specified multiplicand M0X0.
        /// </summary>
        /// <param name="multiplicand0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicand0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicand0x2">The multiplicand M0X2.</param>
        /// <param name="multiplicand1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand2x0">The multiplicand M2X0.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplier0x0">The multiplier M0X0.</param>
        /// <param name="multiplier0x1">The multiplier M0X1.</param>
        /// <param name="multiplier0x2">The multiplier M0X2.</param>
        /// <param name="multiplier0x3">The multiplier M0X3.</param>
        /// <param name="multiplier0x4">The multiplier M0X4.</param>
        /// <param name="multiplier0x5">The multiplier M0X5.</param>
        /// <param name="multiplier1x0">The multiplier M1X0.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier1x4">The multiplier M1X4.</param>
        /// <param name="multiplier1x5">The multiplier M1X5.</param>
        /// <param name="multiplier2x0">The multiplier M2X0.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier2x4">The multiplier M2X4.</param>
        /// <param name="multiplier2x5">The multiplier M2X5.</param>
        /// <param name="multiplier3x0">The multiplier M3X0.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <param name="multiplier3x4">The multiplier M3X4.</param>
        /// <param name="multiplier3x5">The multiplier M3X5.</param>
        /// <param name="multiplier4x0">The multiplier M4X0.</param>
        /// <param name="multiplier4x1">The multiplier M4X1.</param>
        /// <param name="multiplier4x2">The multiplier M4X2.</param>
        /// <param name="multiplier4x3">The multiplier M4X3.</param>
        /// <param name="multiplier4x4">The multiplier M4X4.</param>
        /// <param name="multiplier4x5">The multiplier M4X5.</param>
        /// <param name="multiplier5x0">The multiplier M5X0.</param>
        /// <param name="multiplier5x1">The multiplier M5X1.</param>
        /// <param name="multiplier5x2">The multiplier M5X2.</param>
        /// <param name="multiplier5x3">The multiplier M5X3.</param>
        /// <param name="multiplier5x4">The multiplier M5X4.</param>
        /// <param name="multiplier5x5">The multiplier M5X5.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M0x0, double M0x1, double M0x2, double M0x3, double M0x4, double M0x5,
            double M1x0, double M1x1, double M1x2, double M1x3, double M1x4, double M1x5,
            double M2x0, double M2x1, double M2x2, double M2x3, double M2x4, double M2x5,
            double M3x0, double M3x1, double M3x2, double M3x3, double M3x4, double M3x5,
            double M4x0, double M4x1, double M4x2, double M4x3, double M4x4, double M4x5,
            double M5x0, double M5x1, double M5x2, double M5x3, double M5x4, double M5x5
            ) Multiply3x3x6x6(
            double multiplicand0x0, double multiplicand0x1, double multiplicand0x2,
            double multiplicand1x0, double multiplicand1x1, double multiplicand1x2,
            double multiplicand2x0, double multiplicand2x1, double multiplicand2x2,
            double multiplier0x0, double multiplier0x1, double multiplier0x2, double multiplier0x3, double multiplier0x4, double multiplier0x5,
            double multiplier1x0, double multiplier1x1, double multiplier1x2, double multiplier1x3, double multiplier1x4, double multiplier1x5,
            double multiplier2x0, double multiplier2x1, double multiplier2x2, double multiplier2x3, double multiplier2x4, double multiplier2x5,
            double multiplier3x0, double multiplier3x1, double multiplier3x2, double multiplier3x3, double multiplier3x4, double multiplier3x5,
            double multiplier4x0, double multiplier4x1, double multiplier4x2, double multiplier4x3, double multiplier4x4, double multiplier4x5,
            double multiplier5x0, double multiplier5x1, double multiplier5x2, double multiplier5x3, double multiplier5x4, double multiplier5x5)
            => (
                multiplicand0x0 * multiplier0x0 + multiplicand0x1 * multiplier1x0 + multiplicand0x2 * multiplier2x0 + multiplier3x0 + multiplier4x0 + multiplier5x0,
                multiplicand0x0 * multiplier0x1 + multiplicand0x1 * multiplier1x1 + multiplicand0x2 * multiplier2x1 + multiplier3x1 + multiplier4x1 + multiplier5x1,
                multiplicand0x0 * multiplier0x2 + multiplicand0x1 * multiplier1x2 + multiplicand0x2 * multiplier2x2 + multiplier3x2 + multiplier4x2 + multiplier5x2,
                multiplicand0x0 * multiplier0x3 + multiplicand0x1 * multiplier1x3 + multiplicand0x2 * multiplier2x3 + multiplier3x3 + multiplier4x3 + multiplier5x3,
                multiplicand0x0 * multiplier0x4 + multiplicand0x1 * multiplier1x4 + multiplicand0x2 * multiplier2x4 + multiplier3x4 + multiplier4x4 + multiplier5x4,
                multiplicand0x0 * multiplier0x5 + multiplicand0x1 * multiplier1x5 + multiplicand0x2 * multiplier2x5 + multiplier3x5 + multiplier4x5 + multiplier5x5,
                multiplicand1x0 * multiplier0x0 + multiplicand1x1 * multiplier1x0 + multiplicand1x2 * multiplier2x0 + multiplier3x0 + multiplier4x0 + multiplier5x0,
                multiplicand1x0 * multiplier0x1 + multiplicand1x1 * multiplier1x1 + multiplicand1x2 * multiplier2x1 + multiplier3x1 + multiplier4x1 + multiplier5x1,
                multiplicand1x0 * multiplier0x2 + multiplicand1x1 * multiplier1x2 + multiplicand1x2 * multiplier2x2 + multiplier3x2 + multiplier4x2 + multiplier5x2,
                multiplicand1x0 * multiplier0x3 + multiplicand1x1 * multiplier1x3 + multiplicand1x2 * multiplier2x3 + multiplier3x3 + multiplier4x3 + multiplier5x3,
                multiplicand1x0 * multiplier0x4 + multiplicand1x1 * multiplier1x4 + multiplicand1x2 * multiplier2x4 + multiplier3x4 + multiplier4x4 + multiplier5x4,
                multiplicand1x0 * multiplier0x5 + multiplicand1x1 * multiplier1x5 + multiplicand1x2 * multiplier2x5 + multiplier3x5 + multiplier4x5 + multiplier5x5,
                multiplicand2x0 * multiplier0x0 + multiplicand2x1 * multiplier1x0 + multiplicand2x2 * multiplier2x0 + multiplier3x0 + multiplier4x0 + multiplier5x0,
                multiplicand2x0 * multiplier0x1 + multiplicand2x1 * multiplier1x1 + multiplicand2x2 * multiplier2x1 + multiplier3x1 + multiplier4x1 + multiplier5x1,
                multiplicand2x0 * multiplier0x2 + multiplicand2x1 * multiplier1x2 + multiplicand2x2 * multiplier2x2 + multiplier3x2 + multiplier4x2 + multiplier5x2,
                multiplicand2x0 * multiplier0x3 + multiplicand2x1 * multiplier1x3 + multiplicand2x2 * multiplier2x3 + multiplier3x3 + multiplier4x3 + multiplier5x3,
                multiplicand2x0 * multiplier0x4 + multiplicand2x1 * multiplier1x4 + multiplicand2x2 * multiplier2x4 + multiplier3x4 + multiplier4x4 + multiplier5x4,
                multiplicand2x0 * multiplier0x5 + multiplicand2x1 * multiplier1x5 + multiplicand2x2 * multiplier2x5 + multiplier3x5 + multiplier4x5 + multiplier5x5,
                multiplier0x0 + multiplier1x0 + multiplier2x0 + multiplier3x0 + multiplier4x0 + multiplier5x0,
                multiplier0x1 + multiplier1x1 + multiplier2x1 + multiplier3x1 + multiplier4x1 + multiplier5x1,
                multiplier0x2 + multiplier1x2 + multiplier2x2 + multiplier3x2 + multiplier4x2 + multiplier5x2,
                multiplier0x3 + multiplier1x3 + multiplier2x3 + multiplier3x3 + multiplier4x3 + multiplier5x3,
                multiplier0x4 + multiplier1x4 + multiplier2x4 + multiplier3x4 + multiplier4x4 + multiplier5x4,
                multiplier0x5 + multiplier1x5 + multiplier2x5 + multiplier3x5 + multiplier4x5 + multiplier5x5,
                multiplier0x0 + multiplier1x0 + multiplier2x0 + multiplier3x0 + multiplier4x0 + multiplier5x0,
                multiplier0x1 + multiplier1x1 + multiplier2x1 + multiplier3x1 + multiplier4x1 + multiplier5x1,
                multiplier0x2 + multiplier1x2 + multiplier2x2 + multiplier3x2 + multiplier4x2 + multiplier5x2,
                multiplier0x3 + multiplier1x3 + multiplier2x3 + multiplier3x3 + multiplier4x3 + multiplier5x3,
                multiplier0x4 + multiplier1x4 + multiplier2x4 + multiplier3x4 + multiplier4x4 + multiplier5x4,
                multiplier0x5 + multiplier1x5 + multiplier2x5 + multiplier3x5 + multiplier4x5 + multiplier5x5,
                multiplier0x0 + multiplier1x0 + multiplier2x0 + multiplier3x0 + multiplier4x0 + multiplier5x0,
                multiplier0x1 + multiplier1x1 + multiplier2x1 + multiplier3x1 + multiplier4x1 + multiplier5x1,
                multiplier0x2 + multiplier1x2 + multiplier2x2 + multiplier3x2 + multiplier4x2 + multiplier5x2,
                multiplier0x3 + multiplier1x3 + multiplier2x3 + multiplier3x3 + multiplier4x3 + multiplier5x3,
                multiplier0x4 + multiplier1x4 + multiplier2x4 + multiplier3x4 + multiplier4x4 + multiplier5x4,
                multiplier0x5 + multiplier1x5 + multiplier2x5 + multiplier3x5 + multiplier4x5 + multiplier5x5
            );

        /// <summary>
        /// Multiply6x6x3x3s the specified multiplicand M0X0.
        /// </summary>
        /// <param name="multiplicand0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicand0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicand0x2">The multiplicand M0X2.</param>
        /// <param name="multiplicand0x3">The multiplicand M0X3.</param>
        /// <param name="multiplicand0x4">The multiplicand M0X4.</param>
        /// <param name="multiplicand0x5">The multiplicand M0X5.</param>
        /// <param name="multiplicand1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicand1x5">The multiplicand M1X5.</param>
        /// <param name="multiplicand2x0">The multiplicand M2X0.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicand2x5">The multiplicand M2X5.</param>
        /// <param name="multiplicand3x0">The multiplicand M3X0.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicand3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicand3x5">The multiplicand M3X5.</param>
        /// <param name="multiplicand4x0">The multiplicand M4X0.</param>
        /// <param name="multiplicand4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicand4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicand4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicand4x4">The multiplicand M4X4.</param>
        /// <param name="multiplicand4x5">The multiplicand M4X5.</param>
        /// <param name="multiplicand5x0">The multiplicand M5X0.</param>
        /// <param name="multiplicand5x1">The multiplicand M5X1.</param>
        /// <param name="multiplicand5x2">The multiplicand M5X2.</param>
        /// <param name="multiplicand5x3">The multiplicand M5X3.</param>
        /// <param name="multiplicand5x4">The multiplicand M5X4.</param>
        /// <param name="multiplicand5x5">The multiplicand M5X5.</param>
        /// <param name="multiplier0x0">The multiplier M0X0.</param>
        /// <param name="multiplier0x1">The multiplier M0X1.</param>
        /// <param name="multiplier0x2">The multiplier M0X2.</param>
        /// <param name="multiplier1x0">The multiplier M1X0.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier2x0">The multiplier M2X0.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M0x0, double M0x1, double M0x2, double M0x3, double M0x4, double M0x5,
            double M1x0, double M1x1, double M1x2, double M1x3, double M1x4, double M1x5,
            double M2x0, double M2x1, double M2x2, double M2x3, double M2x4, double M2x5,
            double M3x0, double M3x1, double M3x2, double M3x3, double M3x4, double M3x5,
            double M4x0, double M4x1, double M4x2, double M4x3, double M4x4, double M4x5,
            double M5x0, double M5x1, double M5x2, double M5x3, double M5x4, double M5x5
            ) Multiply6x6x3x3(
            double multiplicand0x0, double multiplicand0x1, double multiplicand0x2, double multiplicand0x3, double multiplicand0x4, double multiplicand0x5,
            double multiplicand1x0, double multiplicand1x1, double multiplicand1x2, double multiplicand1x3, double multiplicand1x4, double multiplicand1x5,
            double multiplicand2x0, double multiplicand2x1, double multiplicand2x2, double multiplicand2x3, double multiplicand2x4, double multiplicand2x5,
            double multiplicand3x0, double multiplicand3x1, double multiplicand3x2, double multiplicand3x3, double multiplicand3x4, double multiplicand3x5,
            double multiplicand4x0, double multiplicand4x1, double multiplicand4x2, double multiplicand4x3, double multiplicand4x4, double multiplicand4x5,
            double multiplicand5x0, double multiplicand5x1, double multiplicand5x2, double multiplicand5x3, double multiplicand5x4, double multiplicand5x5,
            double multiplier0x0, double multiplier0x1, double multiplier0x2,
            double multiplier1x0, double multiplier1x1, double multiplier1x2,
            double multiplier2x0, double multiplier2x1, double multiplier2x2)
            => (
                multiplicand0x0 * multiplier0x0 + multiplicand0x1 * multiplier1x0 + multiplicand0x2 * multiplier2x0 + multiplicand0x3 + multiplicand0x4 + multiplicand0x5,
                multiplicand0x0 * multiplier0x1 + multiplicand0x1 * multiplier1x1 + multiplicand0x2 * multiplier2x1 + multiplicand0x3 + multiplicand0x4 + multiplicand0x5,
                multiplicand0x0 * multiplier0x2 + multiplicand0x1 * multiplier1x2 + multiplicand0x2 * multiplier2x2 + multiplicand0x3 + multiplicand0x4 + multiplicand0x5,
                multiplicand0x0 + multiplicand0x1 + multiplicand0x2 + multiplicand0x3 + multiplicand0x4 + multiplicand0x5,
                multiplicand0x0 + multiplicand0x1 + multiplicand0x2 + multiplicand0x3 + multiplicand0x4 + multiplicand0x5,
                multiplicand0x0 + multiplicand0x1 + multiplicand0x2 + multiplicand0x3 + multiplicand0x4 + multiplicand0x5,
                multiplicand1x0 * multiplier0x0 + multiplicand1x1 * multiplier1x0 + multiplicand1x2 * multiplier2x0 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5,
                multiplicand1x0 * multiplier0x1 + multiplicand1x1 * multiplier1x1 + multiplicand1x2 * multiplier2x1 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5,
                multiplicand1x0 * multiplier0x2 + multiplicand1x1 * multiplier1x2 + multiplicand1x2 * multiplier2x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5,
                multiplicand1x0 + multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5,
                multiplicand1x0 + multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5,
                multiplicand1x0 + multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5,
                multiplicand2x0 * multiplier0x0 + multiplicand2x1 * multiplier1x0 + multiplicand2x2 * multiplier2x0 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5,
                multiplicand2x0 * multiplier0x1 + multiplicand2x1 * multiplier1x1 + multiplicand2x2 * multiplier2x1 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5,
                multiplicand2x0 * multiplier0x2 + multiplicand2x1 * multiplier1x2 + multiplicand2x2 * multiplier2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5,
                multiplicand2x0 + multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5,
                multiplicand2x0 + multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5,
                multiplicand2x0 + multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5,
                multiplicand3x0 * multiplier0x0 + multiplicand3x1 * multiplier1x0 + multiplicand3x2 * multiplier2x0 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5,
                multiplicand3x0 * multiplier0x1 + multiplicand3x1 * multiplier1x1 + multiplicand3x2 * multiplier2x1 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5,
                multiplicand3x0 * multiplier0x2 + multiplicand3x1 * multiplier1x2 + multiplicand3x2 * multiplier2x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5,
                multiplicand3x0 + multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5,
                multiplicand3x0 + multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5,
                multiplicand3x0 + multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5,
                multiplicand4x0 * multiplier0x0 + multiplicand4x1 * multiplier1x0 + multiplicand4x2 * multiplier2x0 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5,
                multiplicand4x0 * multiplier0x1 + multiplicand4x1 * multiplier1x1 + multiplicand4x2 * multiplier2x1 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5,
                multiplicand4x0 * multiplier0x2 + multiplicand4x1 * multiplier1x2 + multiplicand4x2 * multiplier2x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5,
                multiplicand4x0 + multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5,
                multiplicand4x0 + multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5,
                multiplicand4x0 + multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5,
                multiplicand5x0 * multiplier0x0 + multiplicand5x1 * multiplier1x0 + multiplicand5x2 * multiplier2x0 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5,
                multiplicand5x0 * multiplier0x1 + multiplicand5x1 * multiplier1x1 + multiplicand5x2 * multiplier2x1 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5,
                multiplicand5x0 * multiplier0x2 + multiplicand5x1 * multiplier1x2 + multiplicand5x2 * multiplier2x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5,
                multiplicand5x0 + multiplicand5x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5,
                multiplicand5x0 + multiplicand5x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5,
                multiplicand5x0 + multiplicand5x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5
            );

        /// <summary>
        /// Multiply4x4x6x6s the specified multiplicand M0X0.
        /// </summary>
        /// <param name="multiplicand0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicand0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicand0x2">The multiplicand M0X2.</param>
        /// <param name="multiplicand0x3">The multiplicand M0X3.</param>
        /// <param name="multiplicand1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand2x0">The multiplicand M2X0.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand3x0">The multiplicand M3X0.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplier0x0">The multiplier M0X0.</param>
        /// <param name="multiplier0x1">The multiplier M0X1.</param>
        /// <param name="multiplier0x2">The multiplier M0X2.</param>
        /// <param name="multiplier0x3">The multiplier M0X3.</param>
        /// <param name="multiplier0x4">The multiplier M0X4.</param>
        /// <param name="multiplier0x5">The multiplier M0X5.</param>
        /// <param name="multiplier1x0">The multiplier M1X0.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier1x4">The multiplier M1X4.</param>
        /// <param name="multiplier1x5">The multiplier M1X5.</param>
        /// <param name="multiplier2x0">The multiplier M2X0.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier2x4">The multiplier M2X4.</param>
        /// <param name="multiplier2x5">The multiplier M2X5.</param>
        /// <param name="multiplier3x0">The multiplier M3X0.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <param name="multiplier3x4">The multiplier M3X4.</param>
        /// <param name="multiplier3x5">The multiplier M3X5.</param>
        /// <param name="multiplier4x0">The multiplier M4X0.</param>
        /// <param name="multiplier4x1">The multiplier M4X1.</param>
        /// <param name="multiplier4x2">The multiplier M4X2.</param>
        /// <param name="multiplier4x3">The multiplier M4X3.</param>
        /// <param name="multiplier4x4">The multiplier M4X4.</param>
        /// <param name="multiplier4x5">The multiplier M4X5.</param>
        /// <param name="multiplier5x0">The multiplier M5X0.</param>
        /// <param name="multiplier5x1">The multiplier M5X1.</param>
        /// <param name="multiplier5x2">The multiplier M5X2.</param>
        /// <param name="multiplier5x3">The multiplier M5X3.</param>
        /// <param name="multiplier5x4">The multiplier M5X4.</param>
        /// <param name="multiplier5x5">The multiplier M5X5.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M0x0, double M0x1, double M0x2, double M0x3, double M0x4, double M0x5,
            double M1x0, double M1x1, double M1x2, double M1x3, double M1x4, double M1x5,
            double M2x0, double M2x1, double M2x2, double M2x3, double M2x4, double M2x5,
            double M3x0, double M3x1, double M3x2, double M3x3, double M3x4, double M3x5,
            double M4x0, double M4x1, double M4x2, double M4x3, double M4x4, double M4x5,
            double M5x0, double M5x1, double M5x2, double M5x3, double M5x4, double M5x5
            ) Multiply4x4x6x6(
            double multiplicand0x0, double multiplicand0x1, double multiplicand0x2, double multiplicand0x3,
            double multiplicand1x0, double multiplicand1x1, double multiplicand1x2, double multiplicand1x3,
            double multiplicand2x0, double multiplicand2x1, double multiplicand2x2, double multiplicand2x3,
            double multiplicand3x0, double multiplicand3x1, double multiplicand3x2, double multiplicand3x3,
            double multiplier0x0, double multiplier0x1, double multiplier0x2, double multiplier0x3, double multiplier0x4, double multiplier0x5,
            double multiplier1x0, double multiplier1x1, double multiplier1x2, double multiplier1x3, double multiplier1x4, double multiplier1x5,
            double multiplier2x0, double multiplier2x1, double multiplier2x2, double multiplier2x3, double multiplier2x4, double multiplier2x5,
            double multiplier3x0, double multiplier3x1, double multiplier3x2, double multiplier3x3, double multiplier3x4, double multiplier3x5,
            double multiplier4x0, double multiplier4x1, double multiplier4x2, double multiplier4x3, double multiplier4x4, double multiplier4x5,
            double multiplier5x0, double multiplier5x1, double multiplier5x2, double multiplier5x3, double multiplier5x4, double multiplier5x5)
            => (
                multiplicand0x0 * multiplier0x0 + multiplicand0x1 * multiplier1x0 + multiplicand0x2 * multiplier2x0 + multiplicand0x3 * multiplier3x0 + multiplier4x0 + multiplier5x0,
                multiplicand0x0 * multiplier0x1 + multiplicand0x1 * multiplier1x1 + multiplicand0x2 * multiplier2x1 + multiplicand0x3 * multiplier3x1 + multiplier4x1 + multiplier5x1,
                multiplicand0x0 * multiplier0x2 + multiplicand0x1 * multiplier1x2 + multiplicand0x2 * multiplier2x2 + multiplicand0x3 * multiplier3x2 + multiplier4x2 + multiplier5x2,
                multiplicand0x0 * multiplier0x3 + multiplicand0x1 * multiplier1x3 + multiplicand0x2 * multiplier2x3 + multiplicand0x3 * multiplier3x3 + multiplier4x3 + multiplier5x3,
                multiplicand0x0 * multiplier0x4 + multiplicand0x1 * multiplier1x4 + multiplicand0x2 * multiplier2x4 + multiplicand0x3 * multiplier3x4 + multiplier4x4 + multiplier5x4,
                multiplicand0x0 * multiplier0x5 + multiplicand0x1 * multiplier1x5 + multiplicand0x2 * multiplier2x5 + multiplicand0x3 * multiplier3x5 + multiplier4x5 + multiplier5x5,
                multiplicand1x0 * multiplier0x0 + multiplicand1x1 * multiplier1x0 + multiplicand1x2 * multiplier2x0 + multiplicand1x3 * multiplier3x0 + multiplier4x0 + multiplier5x0,
                multiplicand1x0 * multiplier0x1 + multiplicand1x1 * multiplier1x1 + multiplicand1x2 * multiplier2x1 + multiplicand1x3 * multiplier3x1 + multiplier4x1 + multiplier5x1,
                multiplicand1x0 * multiplier0x2 + multiplicand1x1 * multiplier1x2 + multiplicand1x2 * multiplier2x2 + multiplicand1x3 * multiplier3x2 + multiplier4x2 + multiplier5x2,
                multiplicand1x0 * multiplier0x3 + multiplicand1x1 * multiplier1x3 + multiplicand1x2 * multiplier2x3 + multiplicand1x3 * multiplier3x3 + multiplier4x3 + multiplier5x3,
                multiplicand1x0 * multiplier0x4 + multiplicand1x1 * multiplier1x4 + multiplicand1x2 * multiplier2x4 + multiplicand1x3 * multiplier3x4 + multiplier4x4 + multiplier5x4,
                multiplicand1x0 * multiplier0x5 + multiplicand1x1 * multiplier1x5 + multiplicand1x2 * multiplier2x5 + multiplicand1x3 * multiplier3x5 + multiplier4x5 + multiplier5x5,
                multiplicand2x0 * multiplier0x0 + multiplicand2x1 * multiplier1x0 + multiplicand2x2 * multiplier2x0 + multiplicand2x3 * multiplier3x0 + multiplier4x0 + multiplier5x0,
                multiplicand2x0 * multiplier0x1 + multiplicand2x1 * multiplier1x1 + multiplicand2x2 * multiplier2x1 + multiplicand2x3 * multiplier3x1 + multiplier4x1 + multiplier5x1,
                multiplicand2x0 * multiplier0x2 + multiplicand2x1 * multiplier1x2 + multiplicand2x2 * multiplier2x2 + multiplicand2x3 * multiplier3x2 + multiplier4x2 + multiplier5x2,
                multiplicand2x0 * multiplier0x3 + multiplicand2x1 * multiplier1x3 + multiplicand2x2 * multiplier2x3 + multiplicand2x3 * multiplier3x3 + multiplier4x3 + multiplier5x3,
                multiplicand2x0 * multiplier0x4 + multiplicand2x1 * multiplier1x4 + multiplicand2x2 * multiplier2x4 + multiplicand2x3 * multiplier3x4 + multiplier4x4 + multiplier5x4,
                multiplicand2x0 * multiplier0x5 + multiplicand2x1 * multiplier1x5 + multiplicand2x2 * multiplier2x5 + multiplicand2x3 * multiplier3x5 + multiplier4x5 + multiplier5x5,
                multiplicand3x0 * multiplier0x0 + multiplicand3x1 * multiplier1x0 + multiplicand3x2 * multiplier2x0 + multiplicand3x3 * multiplier3x0 + multiplier4x0 + multiplier5x0,
                multiplicand3x0 * multiplier0x1 + multiplicand3x1 * multiplier1x1 + multiplicand3x2 * multiplier2x1 + multiplicand3x3 * multiplier3x1 + multiplier4x1 + multiplier5x1,
                multiplicand3x0 * multiplier0x2 + multiplicand3x1 * multiplier1x2 + multiplicand3x2 * multiplier2x2 + multiplicand3x3 * multiplier3x2 + multiplier4x2 + multiplier5x2,
                multiplicand3x0 * multiplier0x3 + multiplicand3x1 * multiplier1x3 + multiplicand3x2 * multiplier2x3 + multiplicand3x3 * multiplier3x3 + multiplier4x3 + multiplier5x3,
                multiplicand3x0 * multiplier0x4 + multiplicand3x1 * multiplier1x4 + multiplicand3x2 * multiplier2x4 + multiplicand3x3 * multiplier3x4 + multiplier4x4 + multiplier5x4,
                multiplicand3x0 * multiplier0x5 + multiplicand3x1 * multiplier1x5 + multiplicand3x2 * multiplier2x5 + multiplicand3x3 * multiplier3x5 + multiplier4x5 + multiplier5x5,
                multiplier0x0 + multiplier1x0 + multiplier2x0 + multiplier3x0 + multiplier4x0 + multiplier5x0,
                multiplier0x1 + multiplier1x1 + multiplier2x1 + multiplier3x1 + multiplier4x1 + multiplier5x1,
                multiplier0x2 + multiplier1x2 + multiplier2x2 + multiplier3x2 + multiplier4x2 + multiplier5x2,
                multiplier0x3 + multiplier1x3 + multiplier2x3 + multiplier3x3 + multiplier4x3 + multiplier5x3,
                multiplier0x4 + multiplier1x4 + multiplier2x4 + multiplier3x4 + multiplier4x4 + multiplier5x4,
                multiplier0x5 + multiplier1x5 + multiplier2x5 + multiplier3x5 + multiplier4x5 + multiplier5x5,
                multiplier0x0 + multiplier1x0 + multiplier2x0 + multiplier3x0 + multiplier4x0 + multiplier5x0,
                multiplier0x1 + multiplier1x1 + multiplier2x1 + multiplier3x1 + multiplier4x1 + multiplier5x1,
                multiplier0x2 + multiplier1x2 + multiplier2x2 + multiplier3x2 + multiplier4x2 + multiplier5x2,
                multiplier0x3 + multiplier1x3 + multiplier2x3 + multiplier3x3 + multiplier4x3 + multiplier5x3,
                multiplier0x4 + multiplier1x4 + multiplier2x4 + multiplier3x4 + multiplier4x4 + multiplier5x4,
                multiplier0x5 + multiplier1x5 + multiplier2x5 + multiplier3x5 + multiplier4x5 + multiplier5x5
            );

        /// <summary>
        /// Multiply6x6x4x4s the specified multiplicand M0X0.
        /// </summary>
        /// <param name="multiplicand0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicand0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicand0x2">The multiplicand M0X2.</param>
        /// <param name="multiplicand0x3">The multiplicand M0X3.</param>
        /// <param name="multiplicand0x4">The multiplicand M0X4.</param>
        /// <param name="multiplicand0x5">The multiplicand M0X5.</param>
        /// <param name="multiplicand1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicand1x5">The multiplicand M1X5.</param>
        /// <param name="multiplicand2x0">The multiplicand M2X0.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicand2x5">The multiplicand M2X5.</param>
        /// <param name="multiplicand3x0">The multiplicand M3X0.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicand3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicand3x5">The multiplicand M3X5.</param>
        /// <param name="multiplicand4x0">The multiplicand M4X0.</param>
        /// <param name="multiplicand4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicand4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicand4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicand4x4">The multiplicand M4X4.</param>
        /// <param name="multiplicand4x5">The multiplicand M4X5.</param>
        /// <param name="multiplicand5x0">The multiplicand M5X0.</param>
        /// <param name="multiplicand5x1">The multiplicand M5X1.</param>
        /// <param name="multiplicand5x2">The multiplicand M5X2.</param>
        /// <param name="multiplicand5x3">The multiplicand M5X3.</param>
        /// <param name="multiplicand5x4">The multiplicand M5X4.</param>
        /// <param name="multiplicand5x5">The multiplicand M5X5.</param>
        /// <param name="multiplier0x0">The multiplier M0X0.</param>
        /// <param name="multiplier0x1">The multiplier M0X1.</param>
        /// <param name="multiplier0x2">The multiplier M0X2.</param>
        /// <param name="multiplier0x3">The multiplier M0X3.</param>
        /// <param name="multiplier1x0">The multiplier M1X0.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier2x0">The multiplier M2X0.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier3x0">The multiplier M3X0.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M0x0, double M0x1, double M0x2, double M0x3, double M0x4, double M0x5,
            double M1x0, double M1x1, double M1x2, double M1x3, double M1x4, double M1x5,
            double M2x0, double M2x1, double M2x2, double M2x3, double M2x4, double M2x5,
            double M3x0, double M3x1, double M3x2, double M3x3, double M3x4, double M3x5,
            double M4x0, double M4x1, double M4x2, double M4x3, double M4x4, double M4x5,
            double M5x0, double M5x1, double M5x2, double M5x3, double M5x4, double M5x5
            ) Multiply6x6x4x4(
            double multiplicand0x0, double multiplicand0x1, double multiplicand0x2, double multiplicand0x3, double multiplicand0x4, double multiplicand0x5,
            double multiplicand1x0, double multiplicand1x1, double multiplicand1x2, double multiplicand1x3, double multiplicand1x4, double multiplicand1x5,
            double multiplicand2x0, double multiplicand2x1, double multiplicand2x2, double multiplicand2x3, double multiplicand2x4, double multiplicand2x5,
            double multiplicand3x0, double multiplicand3x1, double multiplicand3x2, double multiplicand3x3, double multiplicand3x4, double multiplicand3x5,
            double multiplicand4x0, double multiplicand4x1, double multiplicand4x2, double multiplicand4x3, double multiplicand4x4, double multiplicand4x5,
            double multiplicand5x0, double multiplicand5x1, double multiplicand5x2, double multiplicand5x3, double multiplicand5x4, double multiplicand5x5,
            double multiplier0x0, double multiplier0x1, double multiplier0x2, double multiplier0x3,
            double multiplier1x0, double multiplier1x1, double multiplier1x2, double multiplier1x3,
            double multiplier2x0, double multiplier2x1, double multiplier2x2, double multiplier2x3,
            double multiplier3x0, double multiplier3x1, double multiplier3x2, double multiplier3x3)
            => (
                multiplicand0x0 * multiplier0x0 + multiplicand0x1 * multiplier1x0 + multiplicand0x2 * multiplier2x0 + multiplicand0x3 * multiplier3x0 + multiplicand0x4 + multiplicand0x5,
                multiplicand0x0 * multiplier0x1 + multiplicand0x1 * multiplier1x1 + multiplicand0x2 * multiplier2x1 + multiplicand0x3 * multiplier3x1 + multiplicand0x4 + multiplicand0x5,
                multiplicand0x0 * multiplier0x2 + multiplicand0x1 * multiplier1x2 + multiplicand0x2 * multiplier2x2 + multiplicand0x3 * multiplier3x2 + multiplicand0x4 + multiplicand0x5,
                multiplicand0x0 * multiplier0x3 + multiplicand0x1 * multiplier1x3 + multiplicand0x2 * multiplier2x3 + multiplicand0x3 * multiplier3x3 + multiplicand0x4 + multiplicand0x5,
                multiplicand0x0 + multiplicand0x1 + multiplicand0x2 + multiplicand0x3 + multiplicand0x4 + multiplicand0x5,
                multiplicand0x0 + multiplicand0x1 + multiplicand0x2 + multiplicand0x3 + multiplicand0x4 + multiplicand0x5,
                multiplicand1x0 * multiplier0x0 + multiplicand1x1 * multiplier1x0 + multiplicand1x2 * multiplier2x0 + multiplicand1x3 * multiplier3x0 + multiplicand1x4 + multiplicand1x5,
                multiplicand1x0 * multiplier0x1 + multiplicand1x1 * multiplier1x1 + multiplicand1x2 * multiplier2x1 + multiplicand1x3 * multiplier3x1 + multiplicand1x4 + multiplicand1x5,
                multiplicand1x0 * multiplier0x2 + multiplicand1x1 * multiplier1x2 + multiplicand1x2 * multiplier2x2 + multiplicand1x3 * multiplier3x2 + multiplicand1x4 + multiplicand1x5,
                multiplicand1x0 * multiplier0x3 + multiplicand1x1 * multiplier1x3 + multiplicand1x2 * multiplier2x3 + multiplicand1x3 * multiplier3x3 + multiplicand1x4 + multiplicand1x5,
                multiplicand1x0 + multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5,
                multiplicand1x0 + multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5,
                multiplicand2x0 * multiplier0x0 + multiplicand2x1 * multiplier1x0 + multiplicand2x2 * multiplier2x0 + multiplicand2x3 * multiplier3x0 + multiplicand2x4 + multiplicand2x5,
                multiplicand2x0 * multiplier0x1 + multiplicand2x1 * multiplier1x1 + multiplicand2x2 * multiplier2x1 + multiplicand2x3 * multiplier3x1 + multiplicand2x4 + multiplicand2x5,
                multiplicand2x0 * multiplier0x2 + multiplicand2x1 * multiplier1x2 + multiplicand2x2 * multiplier2x2 + multiplicand2x3 * multiplier3x2 + multiplicand2x4 + multiplicand2x5,
                multiplicand2x0 * multiplier0x3 + multiplicand2x1 * multiplier1x3 + multiplicand2x2 * multiplier2x3 + multiplicand2x3 * multiplier3x3 + multiplicand2x4 + multiplicand2x5,
                multiplicand2x0 + multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5,
                multiplicand2x0 + multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5,
                multiplicand3x0 * multiplier0x0 + multiplicand3x1 * multiplier1x0 + multiplicand3x2 * multiplier2x0 + multiplicand3x3 * multiplier3x0 + multiplicand3x4 + multiplicand3x5,
                multiplicand3x0 * multiplier0x1 + multiplicand3x1 * multiplier1x1 + multiplicand3x2 * multiplier2x1 + multiplicand3x3 * multiplier3x1 + multiplicand3x4 + multiplicand3x5,
                multiplicand3x0 * multiplier0x2 + multiplicand3x1 * multiplier1x2 + multiplicand3x2 * multiplier2x2 + multiplicand3x3 * multiplier3x2 + multiplicand3x4 + multiplicand3x5,
                multiplicand3x0 * multiplier0x3 + multiplicand3x1 * multiplier1x3 + multiplicand3x2 * multiplier2x3 + multiplicand3x3 * multiplier3x3 + multiplicand3x4 + multiplicand3x5,
                multiplicand3x0 + multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5,
                multiplicand3x0 + multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5,
                multiplicand4x0 * multiplier0x0 + multiplicand4x1 * multiplier1x0 + multiplicand4x2 * multiplier2x0 + multiplicand4x3 * multiplier3x0 + multiplicand4x4 + multiplicand4x5,
                multiplicand4x0 * multiplier0x1 + multiplicand4x1 * multiplier1x1 + multiplicand4x2 * multiplier2x1 + multiplicand4x3 * multiplier3x1 + multiplicand4x4 + multiplicand4x5,
                multiplicand4x0 * multiplier0x2 + multiplicand4x1 * multiplier1x2 + multiplicand4x2 * multiplier2x2 + multiplicand4x3 * multiplier3x2 + multiplicand4x4 + multiplicand4x5,
                multiplicand4x0 * multiplier0x3 + multiplicand4x1 * multiplier1x3 + multiplicand4x2 * multiplier2x3 + multiplicand4x3 * multiplier3x3 + multiplicand4x4 + multiplicand4x5,
                multiplicand4x0 + multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5,
                multiplicand4x0 + multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5,
                multiplicand5x0 * multiplier0x0 + multiplicand5x1 * multiplier1x0 + multiplicand5x2 * multiplier2x0 + multiplicand5x3 * multiplier3x0 + multiplicand5x4 + multiplicand5x5,
                multiplicand5x0 * multiplier0x1 + multiplicand5x1 * multiplier1x1 + multiplicand5x2 * multiplier2x1 + multiplicand5x3 * multiplier3x1 + multiplicand5x4 + multiplicand5x5,
                multiplicand5x0 * multiplier0x2 + multiplicand5x1 * multiplier1x2 + multiplicand5x2 * multiplier2x2 + multiplicand5x3 * multiplier3x2 + multiplicand5x4 + multiplicand5x5,
                multiplicand5x0 * multiplier0x3 + multiplicand5x1 * multiplier1x3 + multiplicand5x2 * multiplier2x3 + multiplicand5x3 * multiplier3x3 + multiplicand5x4 + multiplicand5x5,
                multiplicand5x0 + multiplicand5x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5,
                multiplicand5x0 + multiplicand5x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5
            );

        /// <summary>
        /// Multiply5x5x6x6s the specified multiplicand M0X0.
        /// </summary>
        /// <param name="multiplicand0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicand0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicand0x2">The multiplicand M0X2.</param>
        /// <param name="multiplicand0x3">The multiplicand M0X3.</param>
        /// <param name="multiplicand0x4">The multiplicand M0X4.</param>
        /// <param name="multiplicand1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicand2x0">The multiplicand M2X0.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicand3x0">The multiplicand M3X0.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicand3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicand4x0">The multiplicand M4X0.</param>
        /// <param name="multiplicand4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicand4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicand4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicand4x4">The multiplicand M4X4.</param>
        /// <param name="multiplier0x0">The multiplier M0X0.</param>
        /// <param name="multiplier0x1">The multiplier M0X1.</param>
        /// <param name="multiplier0x2">The multiplier M0X2.</param>
        /// <param name="multiplier0x3">The multiplier M0X3.</param>
        /// <param name="multiplier0x4">The multiplier M0X4.</param>
        /// <param name="multiplier0x5">The multiplier M0X5.</param>
        /// <param name="multiplier1x0">The multiplier M1X0.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier1x4">The multiplier M1X4.</param>
        /// <param name="multiplier1x5">The multiplier M1X5.</param>
        /// <param name="multiplier2x0">The multiplier M2X0.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier2x4">The multiplier M2X4.</param>
        /// <param name="multiplier2x5">The multiplier M2X5.</param>
        /// <param name="multiplier3x0">The multiplier M3X0.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <param name="multiplier3x4">The multiplier M3X4.</param>
        /// <param name="multiplier3x5">The multiplier M3X5.</param>
        /// <param name="multiplier4x0">The multiplier M4X0.</param>
        /// <param name="multiplier4x1">The multiplier M4X1.</param>
        /// <param name="multiplier4x2">The multiplier M4X2.</param>
        /// <param name="multiplier4x3">The multiplier M4X3.</param>
        /// <param name="multiplier4x4">The multiplier M4X4.</param>
        /// <param name="multiplier4x5">The multiplier M4X5.</param>
        /// <param name="multiplier5x0">The multiplier M5X0.</param>
        /// <param name="multiplier5x1">The multiplier M5X1.</param>
        /// <param name="multiplier5x2">The multiplier M5X2.</param>
        /// <param name="multiplier5x3">The multiplier M5X3.</param>
        /// <param name="multiplier5x4">The multiplier M5X4.</param>
        /// <param name="multiplier5x5">The multiplier M5X5.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M0x0, double M0x1, double M0x2, double M0x3, double M0x4, double M0x5,
            double M1x0, double M1x1, double M1x2, double M1x3, double M1x4, double M1x5,
            double M2x0, double M2x1, double M2x2, double M2x3, double M2x4, double M2x5,
            double M3x0, double M3x1, double M3x2, double M3x3, double M3x4, double M3x5,
            double M4x0, double M4x1, double M4x2, double M4x3, double M4x4, double M4x5,
            double M5x0, double M5x1, double M5x2, double M5x3, double M5x4, double M5x5
            ) Multiply5x5x6x6(
            double multiplicand0x0, double multiplicand0x1, double multiplicand0x2, double multiplicand0x3, double multiplicand0x4,
            double multiplicand1x0, double multiplicand1x1, double multiplicand1x2, double multiplicand1x3, double multiplicand1x4,
            double multiplicand2x0, double multiplicand2x1, double multiplicand2x2, double multiplicand2x3, double multiplicand2x4,
            double multiplicand3x0, double multiplicand3x1, double multiplicand3x2, double multiplicand3x3, double multiplicand3x4,
            double multiplicand4x0, double multiplicand4x1, double multiplicand4x2, double multiplicand4x3, double multiplicand4x4,
            double multiplier0x0, double multiplier0x1, double multiplier0x2, double multiplier0x3, double multiplier0x4, double multiplier0x5,
            double multiplier1x0, double multiplier1x1, double multiplier1x2, double multiplier1x3, double multiplier1x4, double multiplier1x5,
            double multiplier2x0, double multiplier2x1, double multiplier2x2, double multiplier2x3, double multiplier2x4, double multiplier2x5,
            double multiplier3x0, double multiplier3x1, double multiplier3x2, double multiplier3x3, double multiplier3x4, double multiplier3x5,
            double multiplier4x0, double multiplier4x1, double multiplier4x2, double multiplier4x3, double multiplier4x4, double multiplier4x5,
            double multiplier5x0, double multiplier5x1, double multiplier5x2, double multiplier5x3, double multiplier5x4, double multiplier5x5)
            => (
                multiplicand0x0 * multiplier0x0 + multiplicand0x1 * multiplier1x0 + multiplicand0x2 * multiplier2x0 + multiplicand0x3 * multiplier3x0 + multiplicand0x4 * multiplier4x0 + multiplier5x0,
                multiplicand0x0 * multiplier0x1 + multiplicand0x1 * multiplier1x1 + multiplicand0x2 * multiplier2x1 + multiplicand0x3 * multiplier3x1 + multiplicand0x4 * multiplier4x1 + multiplier5x1,
                multiplicand0x0 * multiplier0x2 + multiplicand0x1 * multiplier1x2 + multiplicand0x2 * multiplier2x2 + multiplicand0x3 * multiplier3x2 + multiplicand0x4 * multiplier4x2 + multiplier5x2,
                multiplicand0x0 * multiplier0x3 + multiplicand0x1 * multiplier1x3 + multiplicand0x2 * multiplier2x3 + multiplicand0x3 * multiplier3x3 + multiplicand0x4 * multiplier4x3 + multiplier5x3,
                multiplicand0x0 * multiplier0x4 + multiplicand0x1 * multiplier1x4 + multiplicand0x2 * multiplier2x4 + multiplicand0x3 * multiplier3x4 + multiplicand0x4 * multiplier4x4 + multiplier5x4,
                multiplicand0x0 * multiplier0x5 + multiplicand0x1 * multiplier1x5 + multiplicand0x2 * multiplier2x5 + multiplicand0x3 * multiplier3x5 + multiplicand0x4 * multiplier4x5 + multiplier5x5,
                multiplicand1x0 * multiplier0x0 + multiplicand1x1 * multiplier1x0 + multiplicand1x2 * multiplier2x0 + multiplicand1x3 * multiplier3x0 + multiplicand1x4 * multiplier4x0 + multiplier5x0,
                multiplicand1x0 * multiplier0x1 + multiplicand1x1 * multiplier1x1 + multiplicand1x2 * multiplier2x1 + multiplicand1x3 * multiplier3x1 + multiplicand1x4 * multiplier4x1 + multiplier5x1,
                multiplicand1x0 * multiplier0x2 + multiplicand1x1 * multiplier1x2 + multiplicand1x2 * multiplier2x2 + multiplicand1x3 * multiplier3x2 + multiplicand1x4 * multiplier4x2 + multiplier5x2,
                multiplicand1x0 * multiplier0x3 + multiplicand1x1 * multiplier1x3 + multiplicand1x2 * multiplier2x3 + multiplicand1x3 * multiplier3x3 + multiplicand1x4 * multiplier4x3 + multiplier5x3,
                multiplicand1x0 * multiplier0x4 + multiplicand1x1 * multiplier1x4 + multiplicand1x2 * multiplier2x4 + multiplicand1x3 * multiplier3x4 + multiplicand1x4 * multiplier4x4 + multiplier5x4,
                multiplicand1x0 * multiplier0x5 + multiplicand1x1 * multiplier1x5 + multiplicand1x2 * multiplier2x5 + multiplicand1x3 * multiplier3x5 + multiplicand1x4 * multiplier4x5 + multiplier5x5,
                multiplicand2x0 * multiplier0x0 + multiplicand2x1 * multiplier1x0 + multiplicand2x2 * multiplier2x0 + multiplicand2x3 * multiplier3x0 + multiplicand2x4 * multiplier4x0 + multiplier5x0,
                multiplicand2x0 * multiplier0x1 + multiplicand2x1 * multiplier1x1 + multiplicand2x2 * multiplier2x1 + multiplicand2x3 * multiplier3x1 + multiplicand2x4 * multiplier4x1 + multiplier5x1,
                multiplicand2x0 * multiplier0x2 + multiplicand2x1 * multiplier1x2 + multiplicand2x2 * multiplier2x2 + multiplicand2x3 * multiplier3x2 + multiplicand2x4 * multiplier4x2 + multiplier5x2,
                multiplicand2x0 * multiplier0x3 + multiplicand2x1 * multiplier1x3 + multiplicand2x2 * multiplier2x3 + multiplicand2x3 * multiplier3x3 + multiplicand2x4 * multiplier4x3 + multiplier5x3,
                multiplicand2x0 * multiplier0x4 + multiplicand2x1 * multiplier1x4 + multiplicand2x2 * multiplier2x4 + multiplicand2x3 * multiplier3x4 + multiplicand2x4 * multiplier4x4 + multiplier5x4,
                multiplicand2x0 * multiplier0x5 + multiplicand2x1 * multiplier1x5 + multiplicand2x2 * multiplier2x5 + multiplicand2x3 * multiplier3x5 + multiplicand2x4 * multiplier4x5 + multiplier5x5,
                multiplicand3x0 * multiplier0x0 + multiplicand3x1 * multiplier1x0 + multiplicand3x2 * multiplier2x0 + multiplicand3x3 * multiplier3x0 + multiplicand3x4 * multiplier4x0 + multiplier5x0,
                multiplicand3x0 * multiplier0x1 + multiplicand3x1 * multiplier1x1 + multiplicand3x2 * multiplier2x1 + multiplicand3x3 * multiplier3x1 + multiplicand3x4 * multiplier4x1 + multiplier5x1,
                multiplicand3x0 * multiplier0x2 + multiplicand3x1 * multiplier1x2 + multiplicand3x2 * multiplier2x2 + multiplicand3x3 * multiplier3x2 + multiplicand3x4 * multiplier4x2 + multiplier5x2,
                multiplicand3x0 * multiplier0x3 + multiplicand3x1 * multiplier1x3 + multiplicand3x2 * multiplier2x3 + multiplicand3x3 * multiplier3x3 + multiplicand3x4 * multiplier4x3 + multiplier5x3,
                multiplicand3x0 * multiplier0x4 + multiplicand3x1 * multiplier1x4 + multiplicand3x2 * multiplier2x4 + multiplicand3x3 * multiplier3x4 + multiplicand3x4 * multiplier4x4 + multiplier5x4,
                multiplicand3x0 * multiplier0x5 + multiplicand3x1 * multiplier1x5 + multiplicand3x2 * multiplier2x5 + multiplicand3x3 * multiplier3x5 + multiplicand3x4 * multiplier4x5 + multiplier5x5,
                multiplicand4x0 * multiplier0x0 + multiplicand4x1 * multiplier1x0 + multiplicand4x2 * multiplier2x0 + multiplicand4x3 * multiplier3x0 + multiplicand4x4 * multiplier4x0 + multiplier5x0,
                multiplicand4x0 * multiplier0x1 + multiplicand4x1 * multiplier1x1 + multiplicand4x2 * multiplier2x1 + multiplicand4x3 * multiplier3x1 + multiplicand4x4 * multiplier4x1 + multiplier5x1,
                multiplicand4x0 * multiplier0x2 + multiplicand4x1 * multiplier1x2 + multiplicand4x2 * multiplier2x2 + multiplicand4x3 * multiplier3x2 + multiplicand4x4 * multiplier4x2 + multiplier5x2,
                multiplicand4x0 * multiplier0x3 + multiplicand4x1 * multiplier1x3 + multiplicand4x2 * multiplier2x3 + multiplicand4x3 * multiplier3x3 + multiplicand4x4 * multiplier4x3 + multiplier5x3,
                multiplicand4x0 * multiplier0x4 + multiplicand4x1 * multiplier1x4 + multiplicand4x2 * multiplier2x4 + multiplicand4x3 * multiplier3x4 + multiplicand4x4 * multiplier4x4 + multiplier5x4,
                multiplicand4x0 * multiplier0x5 + multiplicand4x1 * multiplier1x5 + multiplicand4x2 * multiplier2x5 + multiplicand4x3 * multiplier3x5 + multiplicand4x4 * multiplier4x5 + multiplier5x5,
                multiplier0x0 + multiplier1x0 + multiplier2x0 + multiplier3x0 + multiplier4x0 + multiplier5x0,
                multiplier0x1 + multiplier1x1 + multiplier2x1 + multiplier3x1 + multiplier4x1 + multiplier5x1,
                multiplier0x2 + multiplier1x2 + multiplier2x2 + multiplier3x2 + multiplier4x2 + multiplier5x2,
                multiplier0x3 + multiplier1x3 + multiplier2x3 + multiplier3x3 + multiplier4x3 + multiplier5x3,
                multiplier0x4 + multiplier1x4 + multiplier2x4 + multiplier3x4 + multiplier4x4 + multiplier5x4,
                multiplier0x5 + multiplier1x5 + multiplier2x5 + multiplier3x5 + multiplier4x5 + multiplier5x5
            );

        /// <summary>
        /// Multiply6x6x5x5s the specified multiplicand M0X0.
        /// </summary>
        /// <param name="multiplicand0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicand0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicand0x2">The multiplicand M0X2.</param>
        /// <param name="multiplicand0x3">The multiplicand M0X3.</param>
        /// <param name="multiplicand0x4">The multiplicand M0X4.</param>
        /// <param name="multiplicand0x5">The multiplicand M0X5.</param>
        /// <param name="multiplicand1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicand1x5">The multiplicand M1X5.</param>
        /// <param name="multiplicand2x0">The multiplicand M2X0.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicand2x5">The multiplicand M2X5.</param>
        /// <param name="multiplicand3x0">The multiplicand M3X0.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicand3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicand3x5">The multiplicand M3X5.</param>
        /// <param name="multiplicand4x0">The multiplicand M4X0.</param>
        /// <param name="multiplicand4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicand4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicand4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicand4x4">The multiplicand M4X4.</param>
        /// <param name="multiplicand4x5">The multiplicand M4X5.</param>
        /// <param name="multiplicand5x0">The multiplicand M5X0.</param>
        /// <param name="multiplicand5x1">The multiplicand M5X1.</param>
        /// <param name="multiplicand5x2">The multiplicand M5X2.</param>
        /// <param name="multiplicand5x3">The multiplicand M5X3.</param>
        /// <param name="multiplicand5x4">The multiplicand M5X4.</param>
        /// <param name="multiplicand5x5">The multiplicand M5X5.</param>
        /// <param name="multiplier0x0">The multiplier M0X0.</param>
        /// <param name="multiplier0x1">The multiplier M0X1.</param>
        /// <param name="multiplier0x2">The multiplier M0X2.</param>
        /// <param name="multiplier0x3">The multiplier M0X3.</param>
        /// <param name="multiplier0x4">The multiplier M0X4.</param>
        /// <param name="multiplier1x0">The multiplier M1X0.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier1x4">The multiplier M1X4.</param>
        /// <param name="multiplier2x0">The multiplier M2X0.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier2x4">The multiplier M2X4.</param>
        /// <param name="multiplier3x0">The multiplier M3X0.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <param name="multiplier3x4">The multiplier M3X4.</param>
        /// <param name="multiplier4x0">The multiplier M4X0.</param>
        /// <param name="multiplier4x1">The multiplier M4X1.</param>
        /// <param name="multiplier4x2">The multiplier M4X2.</param>
        /// <param name="multiplier4x3">The multiplier M4X3.</param>
        /// <param name="multiplier4x4">The multiplier M4X4.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M0x0, double M0x1, double M0x2, double M0x3, double M0x4, double M0x5,
            double M1x0, double M1x1, double M1x2, double M1x3, double M1x4, double M1x5,
            double M2x0, double M2x1, double M2x2, double M2x3, double M2x4, double M2x5,
            double M3x0, double M3x1, double M3x2, double M3x3, double M3x4, double M3x5,
            double M4x0, double M4x1, double M4x2, double M4x3, double M4x4, double M4x5,
            double M5x0, double M5x1, double M5x2, double M5x3, double M5x4, double M5x5
            ) Multiply6x6x5x5(
            double multiplicand0x0, double multiplicand0x1, double multiplicand0x2, double multiplicand0x3, double multiplicand0x4, double multiplicand0x5,
            double multiplicand1x0, double multiplicand1x1, double multiplicand1x2, double multiplicand1x3, double multiplicand1x4, double multiplicand1x5,
            double multiplicand2x0, double multiplicand2x1, double multiplicand2x2, double multiplicand2x3, double multiplicand2x4, double multiplicand2x5,
            double multiplicand3x0, double multiplicand3x1, double multiplicand3x2, double multiplicand3x3, double multiplicand3x4, double multiplicand3x5,
            double multiplicand4x0, double multiplicand4x1, double multiplicand4x2, double multiplicand4x3, double multiplicand4x4, double multiplicand4x5,
            double multiplicand5x0, double multiplicand5x1, double multiplicand5x2, double multiplicand5x3, double multiplicand5x4, double multiplicand5x5,
            double multiplier0x0, double multiplier0x1, double multiplier0x2, double multiplier0x3, double multiplier0x4,
            double multiplier1x0, double multiplier1x1, double multiplier1x2, double multiplier1x3, double multiplier1x4,
            double multiplier2x0, double multiplier2x1, double multiplier2x2, double multiplier2x3, double multiplier2x4,
            double multiplier3x0, double multiplier3x1, double multiplier3x2, double multiplier3x3, double multiplier3x4,
            double multiplier4x0, double multiplier4x1, double multiplier4x2, double multiplier4x3, double multiplier4x4)
            => (
                multiplicand0x0 * multiplier0x0 + multiplicand0x1 * multiplier1x0 + multiplicand0x2 * multiplier2x0 + multiplicand0x3 * multiplier3x0 + multiplicand0x4 * multiplier4x0 + multiplicand0x5,
                multiplicand0x0 * multiplier0x1 + multiplicand0x1 * multiplier1x1 + multiplicand0x2 * multiplier2x1 + multiplicand0x3 * multiplier3x1 + multiplicand0x4 * multiplier4x1 + multiplicand0x5,
                multiplicand0x0 * multiplier0x2 + multiplicand0x1 * multiplier1x2 + multiplicand0x2 * multiplier2x2 + multiplicand0x3 * multiplier3x2 + multiplicand0x4 * multiplier4x2 + multiplicand0x5,
                multiplicand0x0 * multiplier0x3 + multiplicand0x1 * multiplier1x3 + multiplicand0x2 * multiplier2x3 + multiplicand0x3 * multiplier3x3 + multiplicand0x4 * multiplier4x3 + multiplicand0x5,
                multiplicand0x0 * multiplier0x4 + multiplicand0x1 * multiplier1x4 + multiplicand0x2 * multiplier2x4 + multiplicand0x3 * multiplier3x4 + multiplicand0x4 * multiplier4x4 + multiplicand0x5,
                multiplicand0x0 + multiplicand0x1 + multiplicand0x2 + multiplicand0x3 + multiplicand0x4 + multiplicand0x5,
                multiplicand1x0 * multiplier0x0 + multiplicand1x1 * multiplier1x0 + multiplicand1x2 * multiplier2x0 + multiplicand1x3 * multiplier3x0 + multiplicand1x4 * multiplier4x0 + multiplicand1x5,
                multiplicand1x0 * multiplier0x1 + multiplicand1x1 * multiplier1x1 + multiplicand1x2 * multiplier2x1 + multiplicand1x3 * multiplier3x1 + multiplicand1x4 * multiplier4x1 + multiplicand1x5,
                multiplicand1x0 * multiplier0x2 + multiplicand1x1 * multiplier1x2 + multiplicand1x2 * multiplier2x2 + multiplicand1x3 * multiplier3x2 + multiplicand1x4 * multiplier4x2 + multiplicand1x5,
                multiplicand1x0 * multiplier0x3 + multiplicand1x1 * multiplier1x3 + multiplicand1x2 * multiplier2x3 + multiplicand1x3 * multiplier3x3 + multiplicand1x4 * multiplier4x3 + multiplicand1x5,
                multiplicand1x0 * multiplier0x4 + multiplicand1x1 * multiplier1x4 + multiplicand1x2 * multiplier2x4 + multiplicand1x3 * multiplier3x4 + multiplicand1x4 * multiplier4x4 + multiplicand1x5,
                multiplicand1x0 + multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5,
                multiplicand2x0 * multiplier0x0 + multiplicand2x1 * multiplier1x0 + multiplicand2x2 * multiplier2x0 + multiplicand2x3 * multiplier3x0 + multiplicand2x4 * multiplier4x0 + multiplicand2x5,
                multiplicand2x0 * multiplier0x1 + multiplicand2x1 * multiplier1x1 + multiplicand2x2 * multiplier2x1 + multiplicand2x3 * multiplier3x1 + multiplicand2x4 * multiplier4x1 + multiplicand2x5,
                multiplicand2x0 * multiplier0x2 + multiplicand2x1 * multiplier1x2 + multiplicand2x2 * multiplier2x2 + multiplicand2x3 * multiplier3x2 + multiplicand2x4 * multiplier4x2 + multiplicand2x5,
                multiplicand2x0 * multiplier0x3 + multiplicand2x1 * multiplier1x3 + multiplicand2x2 * multiplier2x3 + multiplicand2x3 * multiplier3x3 + multiplicand2x4 * multiplier4x3 + multiplicand2x5,
                multiplicand2x0 * multiplier0x4 + multiplicand2x1 * multiplier1x4 + multiplicand2x2 * multiplier2x4 + multiplicand2x3 * multiplier3x4 + multiplicand2x4 * multiplier4x4 + multiplicand2x5,
                multiplicand2x0 + multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5,
                multiplicand3x0 * multiplier0x0 + multiplicand3x1 * multiplier1x0 + multiplicand3x2 * multiplier2x0 + multiplicand3x3 * multiplier3x0 + multiplicand3x4 * multiplier4x0 + multiplicand3x5,
                multiplicand3x0 * multiplier0x1 + multiplicand3x1 * multiplier1x1 + multiplicand3x2 * multiplier2x1 + multiplicand3x3 * multiplier3x1 + multiplicand3x4 * multiplier4x1 + multiplicand3x5,
                multiplicand3x0 * multiplier0x2 + multiplicand3x1 * multiplier1x2 + multiplicand3x2 * multiplier2x2 + multiplicand3x3 * multiplier3x2 + multiplicand3x4 * multiplier4x2 + multiplicand3x5,
                multiplicand3x0 * multiplier0x3 + multiplicand3x1 * multiplier1x3 + multiplicand3x2 * multiplier2x3 + multiplicand3x3 * multiplier3x3 + multiplicand3x4 * multiplier4x3 + multiplicand3x5,
                multiplicand3x0 * multiplier0x4 + multiplicand3x1 * multiplier1x4 + multiplicand3x2 * multiplier2x4 + multiplicand3x3 * multiplier3x4 + multiplicand3x4 * multiplier4x4 + multiplicand3x5,
                multiplicand3x0 + multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5,
                multiplicand4x0 * multiplier0x0 + multiplicand4x1 * multiplier1x0 + multiplicand4x2 * multiplier2x0 + multiplicand4x3 * multiplier3x0 + multiplicand4x4 * multiplier4x0 + multiplicand4x5,
                multiplicand4x0 * multiplier0x1 + multiplicand4x1 * multiplier1x1 + multiplicand4x2 * multiplier2x1 + multiplicand4x3 * multiplier3x1 + multiplicand4x4 * multiplier4x1 + multiplicand4x5,
                multiplicand4x0 * multiplier0x2 + multiplicand4x1 * multiplier1x2 + multiplicand4x2 * multiplier2x2 + multiplicand4x3 * multiplier3x2 + multiplicand4x4 * multiplier4x2 + multiplicand4x5,
                multiplicand4x0 * multiplier0x3 + multiplicand4x1 * multiplier1x3 + multiplicand4x2 * multiplier2x3 + multiplicand4x3 * multiplier3x3 + multiplicand4x4 * multiplier4x3 + multiplicand4x5,
                multiplicand4x0 * multiplier0x4 + multiplicand4x1 * multiplier1x4 + multiplicand4x2 * multiplier2x4 + multiplicand4x3 * multiplier3x4 + multiplicand4x4 * multiplier4x4 + multiplicand4x5,
                multiplicand4x0 + multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5,
                multiplicand5x0 * multiplier0x0 + multiplicand5x1 * multiplier1x0 + multiplicand5x2 * multiplier2x0 + multiplicand5x3 * multiplier3x0 + multiplicand5x4 * multiplier4x0 + multiplicand5x5,
                multiplicand5x0 * multiplier0x1 + multiplicand5x1 * multiplier1x1 + multiplicand5x2 * multiplier2x1 + multiplicand5x3 * multiplier3x1 + multiplicand5x4 * multiplier4x1 + multiplicand5x5,
                multiplicand5x0 * multiplier0x2 + multiplicand5x1 * multiplier1x2 + multiplicand5x2 * multiplier2x2 + multiplicand5x3 * multiplier3x2 + multiplicand5x4 * multiplier4x2 + multiplicand5x5,
                multiplicand5x0 * multiplier0x3 + multiplicand5x1 * multiplier1x3 + multiplicand5x2 * multiplier2x3 + multiplicand5x3 * multiplier3x3 + multiplicand5x4 * multiplier4x3 + multiplicand5x5,
                multiplicand5x0 * multiplier0x4 + multiplicand5x1 * multiplier1x4 + multiplicand5x2 * multiplier2x4 + multiplicand5x3 * multiplier3x4 + multiplicand5x4 * multiplier4x4 + multiplicand5x5,
                multiplicand5x0 + multiplicand5x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5
            );

        /// <summary>
        /// Multiply6x6x6x6s the specified multiplicand M0X0.
        /// </summary>
        /// <param name="multiplicand0x0">The multiplicand M0X0.</param>
        /// <param name="multiplicand0x1">The multiplicand M0X1.</param>
        /// <param name="multiplicand0x2">The multiplicand M0X2.</param>
        /// <param name="multiplicand0x3">The multiplicand M0X3.</param>
        /// <param name="multiplicand0x4">The multiplicand M0X4.</param>
        /// <param name="multiplicand0x5">The multiplicand M0X5.</param>
        /// <param name="multiplicand1x0">The multiplicand M1X0.</param>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicand1x5">The multiplicand M1X5.</param>
        /// <param name="multiplicand2x0">The multiplicand M2X0.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicand2x5">The multiplicand M2X5.</param>
        /// <param name="multiplicand3x0">The multiplicand M3X0.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicand3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicand3x5">The multiplicand M3X5.</param>
        /// <param name="multiplicand4x0">The multiplicand M4X0.</param>
        /// <param name="multiplicand4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicand4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicand4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicand4x4">The multiplicand M4X4.</param>
        /// <param name="multiplicand4x5">The multiplicand M4X5.</param>
        /// <param name="multiplicand5x0">The multiplicand M5X0.</param>
        /// <param name="multiplicand5x1">The multiplicand M5X1.</param>
        /// <param name="multiplicand5x2">The multiplicand M5X2.</param>
        /// <param name="multiplicand5x3">The multiplicand M5X3.</param>
        /// <param name="multiplicand5x4">The multiplicand M5X4.</param>
        /// <param name="multiplicand5x5">The multiplicand M5X5.</param>
        /// <param name="multiplier0x0">The multiplier M0X0.</param>
        /// <param name="multiplier0x1">The multiplier M0X1.</param>
        /// <param name="multiplier0x2">The multiplier M0X2.</param>
        /// <param name="multiplier0x3">The multiplier M0X3.</param>
        /// <param name="multiplier0x4">The multiplier M0X4.</param>
        /// <param name="multiplier0x5">The multiplier M0X5.</param>
        /// <param name="multiplier1x0">The multiplier M1X0.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier1x4">The multiplier M1X4.</param>
        /// <param name="multiplier1x5">The multiplier M1X5.</param>
        /// <param name="multiplier2x0">The multiplier M2X0.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier2x4">The multiplier M2X4.</param>
        /// <param name="multiplier2x5">The multiplier M2X5.</param>
        /// <param name="multiplier3x0">The multiplier M3X0.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <param name="multiplier3x4">The multiplier M3X4.</param>
        /// <param name="multiplier3x5">The multiplier M3X5.</param>
        /// <param name="multiplier4x0">The multiplier M4X0.</param>
        /// <param name="multiplier4x1">The multiplier M4X1.</param>
        /// <param name="multiplier4x2">The multiplier M4X2.</param>
        /// <param name="multiplier4x3">The multiplier M4X3.</param>
        /// <param name="multiplier4x4">The multiplier M4X4.</param>
        /// <param name="multiplier4x5">The multiplier M4X5.</param>
        /// <param name="multiplier5x0">The multiplier M5X0.</param>
        /// <param name="multiplier5x1">The multiplier M5X1.</param>
        /// <param name="multiplier5x2">The multiplier M5X2.</param>
        /// <param name="multiplier5x3">The multiplier M5X3.</param>
        /// <param name="multiplier5x4">The multiplier M5X4.</param>
        /// <param name="multiplier5x5">The multiplier M5X5.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M0x0, double M0x1, double M0x2, double M0x3, double M0x4, double M0x5,
            double M1x0, double M1x1, double M1x2, double M1x3, double M1x4, double M1x5,
            double M2x0, double M2x1, double M2x2, double M2x3, double M2x4, double M2x5,
            double M3x0, double M3x1, double M3x2, double M3x3, double M3x4, double M3x5,
            double M4x0, double M4x1, double M4x2, double M4x3, double M4x4, double M4x5,
            double M5x0, double M5x1, double M5x2, double M5x3, double M5x4, double M5x5
            ) Multiply6x6x6x6(
            double multiplicand0x0, double multiplicand0x1, double multiplicand0x2, double multiplicand0x3, double multiplicand0x4, double multiplicand0x5,
            double multiplicand1x0, double multiplicand1x1, double multiplicand1x2, double multiplicand1x3, double multiplicand1x4, double multiplicand1x5,
            double multiplicand2x0, double multiplicand2x1, double multiplicand2x2, double multiplicand2x3, double multiplicand2x4, double multiplicand2x5,
            double multiplicand3x0, double multiplicand3x1, double multiplicand3x2, double multiplicand3x3, double multiplicand3x4, double multiplicand3x5,
            double multiplicand4x0, double multiplicand4x1, double multiplicand4x2, double multiplicand4x3, double multiplicand4x4, double multiplicand4x5,
            double multiplicand5x0, double multiplicand5x1, double multiplicand5x2, double multiplicand5x3, double multiplicand5x4, double multiplicand5x5,
            double multiplier0x0, double multiplier0x1, double multiplier0x2, double multiplier0x3, double multiplier0x4, double multiplier0x5,
            double multiplier1x0, double multiplier1x1, double multiplier1x2, double multiplier1x3, double multiplier1x4, double multiplier1x5,
            double multiplier2x0, double multiplier2x1, double multiplier2x2, double multiplier2x3, double multiplier2x4, double multiplier2x5,
            double multiplier3x0, double multiplier3x1, double multiplier3x2, double multiplier3x3, double multiplier3x4, double multiplier3x5,
            double multiplier4x0, double multiplier4x1, double multiplier4x2, double multiplier4x3, double multiplier4x4, double multiplier4x5,
            double multiplier5x0, double multiplier5x1, double multiplier5x2, double multiplier5x3, double multiplier5x4, double multiplier5x5)
            => (
                multiplicand0x0 * multiplier0x0 + multiplicand0x1 * multiplier1x0 + multiplicand0x2 * multiplier2x0 + multiplicand0x3 * multiplier3x0 + multiplicand0x4 * multiplier4x0 + multiplicand0x5 * multiplier5x0,
                multiplicand0x0 * multiplier0x1 + multiplicand0x1 * multiplier1x1 + multiplicand0x2 * multiplier2x1 + multiplicand0x3 * multiplier3x1 + multiplicand0x4 * multiplier4x1 + multiplicand0x5 * multiplier5x1,
                multiplicand0x0 * multiplier0x2 + multiplicand0x1 * multiplier1x2 + multiplicand0x2 * multiplier2x2 + multiplicand0x3 * multiplier3x2 + multiplicand0x4 * multiplier4x2 + multiplicand0x5 * multiplier5x2,
                multiplicand0x0 * multiplier0x3 + multiplicand0x1 * multiplier1x3 + multiplicand0x2 * multiplier2x3 + multiplicand0x3 * multiplier3x3 + multiplicand0x4 * multiplier4x3 + multiplicand0x5 * multiplier5x3,
                multiplicand0x0 * multiplier0x4 + multiplicand0x1 * multiplier1x4 + multiplicand0x2 * multiplier2x4 + multiplicand0x3 * multiplier3x4 + multiplicand0x4 * multiplier4x4 + multiplicand0x5 * multiplier5x4,
                multiplicand0x0 * multiplier0x5 + multiplicand0x1 * multiplier1x5 + multiplicand0x2 * multiplier2x5 + multiplicand0x3 * multiplier3x5 + multiplicand0x4 * multiplier4x5 + multiplicand0x5 * multiplier5x5,
                multiplicand1x0 * multiplier0x0 + multiplicand1x1 * multiplier1x0 + multiplicand1x2 * multiplier2x0 + multiplicand1x3 * multiplier3x0 + multiplicand1x4 * multiplier4x0 + multiplicand1x5 * multiplier5x0,
                multiplicand1x0 * multiplier0x1 + multiplicand1x1 * multiplier1x1 + multiplicand1x2 * multiplier2x1 + multiplicand1x3 * multiplier3x1 + multiplicand1x4 * multiplier4x1 + multiplicand1x5 * multiplier5x1,
                multiplicand1x0 * multiplier0x2 + multiplicand1x1 * multiplier1x2 + multiplicand1x2 * multiplier2x2 + multiplicand1x3 * multiplier3x2 + multiplicand1x4 * multiplier4x2 + multiplicand1x5 * multiplier5x2,
                multiplicand1x0 * multiplier0x3 + multiplicand1x1 * multiplier1x3 + multiplicand1x2 * multiplier2x3 + multiplicand1x3 * multiplier3x3 + multiplicand1x4 * multiplier4x3 + multiplicand1x5 * multiplier5x3,
                multiplicand1x0 * multiplier0x4 + multiplicand1x1 * multiplier1x4 + multiplicand1x2 * multiplier2x4 + multiplicand1x3 * multiplier3x4 + multiplicand1x4 * multiplier4x4 + multiplicand1x5 * multiplier5x4,
                multiplicand1x0 * multiplier0x5 + multiplicand1x1 * multiplier1x5 + multiplicand1x2 * multiplier2x5 + multiplicand1x3 * multiplier3x5 + multiplicand1x4 * multiplier4x5 + multiplicand1x5 * multiplier5x5,
                multiplicand2x0 * multiplier0x0 + multiplicand2x1 * multiplier1x0 + multiplicand2x2 * multiplier2x0 + multiplicand2x3 * multiplier3x0 + multiplicand2x4 * multiplier4x0 + multiplicand2x5 * multiplier5x0,
                multiplicand2x0 * multiplier0x1 + multiplicand2x1 * multiplier1x1 + multiplicand2x2 * multiplier2x1 + multiplicand2x3 * multiplier3x1 + multiplicand2x4 * multiplier4x1 + multiplicand2x5 * multiplier5x1,
                multiplicand2x0 * multiplier0x2 + multiplicand2x1 * multiplier1x2 + multiplicand2x2 * multiplier2x2 + multiplicand2x3 * multiplier3x2 + multiplicand2x4 * multiplier4x2 + multiplicand2x5 * multiplier5x2,
                multiplicand2x0 * multiplier0x3 + multiplicand2x1 * multiplier1x3 + multiplicand2x2 * multiplier2x3 + multiplicand2x3 * multiplier3x3 + multiplicand2x4 * multiplier4x3 + multiplicand2x5 * multiplier5x3,
                multiplicand2x0 * multiplier0x4 + multiplicand2x1 * multiplier1x4 + multiplicand2x2 * multiplier2x4 + multiplicand2x3 * multiplier3x4 + multiplicand2x4 * multiplier4x4 + multiplicand2x5 * multiplier5x4,
                multiplicand2x0 * multiplier0x5 + multiplicand2x1 * multiplier1x5 + multiplicand2x2 * multiplier2x5 + multiplicand2x3 * multiplier3x5 + multiplicand2x4 * multiplier4x5 + multiplicand2x5 * multiplier5x5,
                multiplicand3x0 * multiplier0x0 + multiplicand3x1 * multiplier1x0 + multiplicand3x2 * multiplier2x0 + multiplicand3x3 * multiplier3x0 + multiplicand3x4 * multiplier4x0 + multiplicand3x5 * multiplier5x0,
                multiplicand3x0 * multiplier0x1 + multiplicand3x1 * multiplier1x1 + multiplicand3x2 * multiplier2x1 + multiplicand3x3 * multiplier3x1 + multiplicand3x4 * multiplier4x1 + multiplicand3x5 * multiplier5x1,
                multiplicand3x0 * multiplier0x2 + multiplicand3x1 * multiplier1x2 + multiplicand3x2 * multiplier2x2 + multiplicand3x3 * multiplier3x2 + multiplicand3x4 * multiplier4x2 + multiplicand3x5 * multiplier5x2,
                multiplicand3x0 * multiplier0x3 + multiplicand3x1 * multiplier1x3 + multiplicand3x2 * multiplier2x3 + multiplicand3x3 * multiplier3x3 + multiplicand3x4 * multiplier4x3 + multiplicand3x5 * multiplier5x3,
                multiplicand3x0 * multiplier0x4 + multiplicand3x1 * multiplier1x4 + multiplicand3x2 * multiplier2x4 + multiplicand3x3 * multiplier3x4 + multiplicand3x4 * multiplier4x4 + multiplicand3x5 * multiplier5x4,
                multiplicand3x0 * multiplier0x5 + multiplicand3x1 * multiplier1x5 + multiplicand3x2 * multiplier2x5 + multiplicand3x3 * multiplier3x5 + multiplicand3x4 * multiplier4x5 + multiplicand3x5 * multiplier5x5,
                multiplicand4x0 * multiplier0x0 + multiplicand4x1 * multiplier1x0 + multiplicand4x2 * multiplier2x0 + multiplicand4x3 * multiplier3x0 + multiplicand4x4 * multiplier4x0 + multiplicand4x5 * multiplier5x0,
                multiplicand4x0 * multiplier0x1 + multiplicand4x1 * multiplier1x1 + multiplicand4x2 * multiplier2x1 + multiplicand4x3 * multiplier3x1 + multiplicand4x4 * multiplier4x1 + multiplicand4x5 * multiplier5x1,
                multiplicand4x0 * multiplier0x2 + multiplicand4x1 * multiplier1x2 + multiplicand4x2 * multiplier2x2 + multiplicand4x3 * multiplier3x2 + multiplicand4x4 * multiplier4x2 + multiplicand4x5 * multiplier5x2,
                multiplicand4x0 * multiplier0x3 + multiplicand4x1 * multiplier1x3 + multiplicand4x2 * multiplier2x3 + multiplicand4x3 * multiplier3x3 + multiplicand4x4 * multiplier4x3 + multiplicand4x5 * multiplier5x3,
                multiplicand4x0 * multiplier0x4 + multiplicand4x1 * multiplier1x4 + multiplicand4x2 * multiplier2x4 + multiplicand4x3 * multiplier3x4 + multiplicand4x4 * multiplier4x4 + multiplicand4x5 * multiplier5x4,
                multiplicand4x0 * multiplier0x5 + multiplicand4x1 * multiplier1x5 + multiplicand4x2 * multiplier2x5 + multiplicand4x3 * multiplier3x5 + multiplicand4x4 * multiplier4x5 + multiplicand4x5 * multiplier5x5,
                multiplicand5x0 * multiplier0x0 + multiplicand5x1 * multiplier1x0 + multiplicand5x2 * multiplier2x0 + multiplicand5x3 * multiplier3x0 + multiplicand5x4 * multiplier4x0 + multiplicand5x5 * multiplier5x0,
                multiplicand5x0 * multiplier0x1 + multiplicand5x1 * multiplier1x1 + multiplicand5x2 * multiplier2x1 + multiplicand5x3 * multiplier3x1 + multiplicand5x4 * multiplier4x1 + multiplicand5x5 * multiplier5x1,
                multiplicand5x0 * multiplier0x2 + multiplicand5x1 * multiplier1x2 + multiplicand5x2 * multiplier2x2 + multiplicand5x3 * multiplier3x2 + multiplicand5x4 * multiplier4x2 + multiplicand5x5 * multiplier5x2,
                multiplicand5x0 * multiplier0x3 + multiplicand5x1 * multiplier1x3 + multiplicand5x2 * multiplier2x3 + multiplicand5x3 * multiplier3x3 + multiplicand5x4 * multiplier4x3 + multiplicand5x5 * multiplier5x3,
                multiplicand5x0 * multiplier0x4 + multiplicand5x1 * multiplier1x4 + multiplicand5x2 * multiplier2x4 + multiplicand5x3 * multiplier3x4 + multiplicand5x4 * multiplier4x4 + multiplicand5x5 * multiplier5x4,
                multiplicand5x0 * multiplier0x5 + multiplicand5x1 * multiplier1x5 + multiplicand5x2 * multiplier2x5 + multiplicand5x3 * multiplier3x5 + multiplicand5x4 * multiplier4x5 + multiplicand5x5 * multiplier5x5
            );
        #endregion Multiply

        #region Is Identity
        /// <summary>
        /// Tests whether or not a given transform is an identity transform matrix.
        /// </summary>
        /// <param name="m0x0">The M0X0.</param>
        /// <param name="m0x1">The M0X1.</param>
        /// <param name="m1x0">The M1X0.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <returns>
        ///   <c>true</c> if the specified M0X0 is identity; otherwise, <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMatrixIdentity(
            double m0x0, double m0x1,
            double m1x0, double m1x1)
            => Math.Abs(m0x0 - 1d) < double.Epsilon && Math.Abs(m0x1) < double.Epsilon
            && Math.Abs(m1x0) < double.Epsilon && Math.Abs(m1x1 - 1d) < double.Epsilon;

        /// <summary>
        /// Tests whether or not a given transform is an identity transform matrix.
        /// </summary>
        /// <param name="m0x0">The M0X0.</param>
        /// <param name="m0x1">The M0X1.</param>
        /// <param name="m0x2">The M0X2.</param>
        /// <param name="m1x0">The M1X0.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m2x0">The M2X0.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <returns>
        ///   <c>true</c> if the specified M0X0 is identity; otherwise, <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMatrixIdentity(
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2)
            => Math.Abs(m0x0 - 1d) < double.Epsilon && Math.Abs(m0x1) < double.Epsilon && Math.Abs(m0x2) < double.Epsilon
            && Math.Abs(m1x0) < double.Epsilon && Math.Abs(m1x1 - 1d) < double.Epsilon && Math.Abs(m1x2) < double.Epsilon
            && Math.Abs(m2x0) < double.Epsilon && Math.Abs(m2x1) < double.Epsilon && Math.Abs(m2x2 - 1d) < double.Epsilon;

        /// <summary>
        /// Tests whether or not a given transform is an identity transform matrix.
        /// </summary>
        /// <param name="m0x0">The M0X0.</param>
        /// <param name="m0x1">The M0X1.</param>
        /// <param name="m0x2">The M0X2.</param>
        /// <param name="m0x3">The M0X3.</param>
        /// <param name="m1x0">The M1X0.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m1x3">The M1X3.</param>
        /// <param name="m2x0">The M2X0.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <param name="m2x3">The M2X3.</param>
        /// <param name="m3x0">The M3X0.</param>
        /// <param name="m3x1">The M3X1.</param>
        /// <param name="m3x2">The M3X2.</param>
        /// <param name="m3x3">The M3X3.</param>
        /// <returns>
        ///   <c>true</c> if the specified M0X0 is identity; otherwise, <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMatrixIdentity(
            double m0x0, double m0x1, double m0x2, double m0x3,
            double m1x0, double m1x1, double m1x2, double m1x3,
            double m2x0, double m2x1, double m2x2, double m2x3,
            double m3x0, double m3x1, double m3x2, double m3x3)
            => Math.Abs(m0x0 - 1d) < double.Epsilon && Math.Abs(m0x1) < double.Epsilon && Math.Abs(m0x2) < double.Epsilon && Math.Abs(m0x3) < double.Epsilon
            && Math.Abs(m1x0) < double.Epsilon && Math.Abs(m1x1 - 1d) < double.Epsilon && Math.Abs(m1x2) < double.Epsilon && Math.Abs(m1x3) < double.Epsilon
            && Math.Abs(m2x0) < double.Epsilon && Math.Abs(m2x1) < double.Epsilon && Math.Abs(m2x2 - 1d) < double.Epsilon && Math.Abs(m2x3) < double.Epsilon
            && Math.Abs(m3x0) < double.Epsilon && Math.Abs(m3x1) < double.Epsilon && Math.Abs(m3x2) < double.Epsilon && Math.Abs(m3x3 - 1d) < double.Epsilon;

        /// <summary>
        /// Tests whether or not a given transform is an identity transform matrix.
        /// </summary>
        /// <param name="m0x0">The M0X0.</param>
        /// <param name="m0x1">The M0X1.</param>
        /// <param name="m0x2">The M0X2.</param>
        /// <param name="m0x3">The M0X3.</param>
        /// <param name="m0x4">The M0X4.</param>
        /// <param name="m1x0">The M1X0.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m1x3">The M1X3.</param>
        /// <param name="m1x4">The M1X4.</param>
        /// <param name="m2x0">The M2X0.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <param name="m2x3">The M2X3.</param>
        /// <param name="m2x4">The M2X4.</param>
        /// <param name="m3x0">The M3X0.</param>
        /// <param name="m3x1">The M3X1.</param>
        /// <param name="m3x2">The M3X2.</param>
        /// <param name="m3x3">The M3X3.</param>
        /// <param name="m3x4">The M3X4.</param>
        /// <param name="m4x0">The M4X0.</param>
        /// <param name="m4x1">The M4X1.</param>
        /// <param name="m4x2">The M4X2.</param>
        /// <param name="m4x3">The M4X3.</param>
        /// <param name="m4x4">The M4X4.</param>
        /// <returns>
        ///   <c>true</c> if the specified M0X0 is identity; otherwise, <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMatrixIdentity(
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4)
            => Math.Abs(m0x0 - 1d) < double.Epsilon && Math.Abs(m0x1) < double.Epsilon && Math.Abs(m0x2) < double.Epsilon && Math.Abs(m0x3) < double.Epsilon && Math.Abs(m0x4) < double.Epsilon
            && Math.Abs(m1x0) < double.Epsilon && Math.Abs(m1x1 - 1d) < double.Epsilon && Math.Abs(m1x2) < double.Epsilon && Math.Abs(m1x3) < double.Epsilon && Math.Abs(m1x4) < double.Epsilon
            && Math.Abs(m2x0) < double.Epsilon && Math.Abs(m2x1) < double.Epsilon && Math.Abs(m2x2 - 1d) < double.Epsilon && Math.Abs(m2x3) < double.Epsilon && Math.Abs(m2x4) < double.Epsilon
            && Math.Abs(m3x0) < double.Epsilon && Math.Abs(m3x1) < double.Epsilon && Math.Abs(m3x2) < double.Epsilon && Math.Abs(m3x3 - 1d) < double.Epsilon && Math.Abs(m3x4) < double.Epsilon
            && Math.Abs(m4x0) < double.Epsilon && Math.Abs(m4x1) < double.Epsilon && Math.Abs(m4x2) < double.Epsilon && Math.Abs(m4x3) < double.Epsilon && Math.Abs(m4x4 - 1d) < double.Epsilon;

        /// <summary>
        /// Tests whether or not a given transform is an identity transform matrix.
        /// </summary>
        /// <param name="m0x0">The M0X0.</param>
        /// <param name="m0x1">The M0X1.</param>
        /// <param name="m0x2">The M0X2.</param>
        /// <param name="m0x3">The M0X3.</param>
        /// <param name="m0x4">The M0X4.</param>
        /// <param name="m0x5">The M0X5.</param>
        /// <param name="m1x0">The M1X0.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m1x3">The M1X3.</param>
        /// <param name="m1x4">The M1X4.</param>
        /// <param name="m1x5">The M1X5.</param>
        /// <param name="m2x0">The M2X0.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <param name="m2x3">The M2X3.</param>
        /// <param name="m2x4">The M2X4.</param>
        /// <param name="m2x5">The M2X5.</param>
        /// <param name="m3x0">The M3X0.</param>
        /// <param name="m3x1">The M3X1.</param>
        /// <param name="m3x2">The M3X2.</param>
        /// <param name="m3x3">The M3X3.</param>
        /// <param name="m3x4">The M3X4.</param>
        /// <param name="m3x5">The M3X5.</param>
        /// <param name="m4x0">The M4X0.</param>
        /// <param name="m4x1">The M4X1.</param>
        /// <param name="m4x2">The M4X2.</param>
        /// <param name="m4x3">The M4X3.</param>
        /// <param name="m4x4">The M4X4.</param>
        /// <param name="m4x5">The M4X5.</param>
        /// <param name="m5x0">The M5X0.</param>
        /// <param name="m5x1">The M5X1.</param>
        /// <param name="m5x2">The M5X2.</param>
        /// <param name="m5x3">The M5X3.</param>
        /// <param name="m5x4">The M5X4.</param>
        /// <param name="m5x5">The M5X5.</param>
        /// <returns>
        ///   <c>true</c> if the specified M0X0 is identity; otherwise, <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMatrixIdentity(
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4, double m0x5,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x0, double m5x1, double m5x2, double m5x3, double m5x4, double m5x5)
            => Math.Abs(m0x0 - 1d) < double.Epsilon && Math.Abs(m0x1) < double.Epsilon && Math.Abs(m0x2) < double.Epsilon && Math.Abs(m0x3) < double.Epsilon && Math.Abs(m0x4) < double.Epsilon && Math.Abs(m0x5) < double.Epsilon
            && Math.Abs(m1x0) < double.Epsilon && Math.Abs(m1x1 - 1d) < double.Epsilon && Math.Abs(m1x2) < double.Epsilon && Math.Abs(m1x3) < double.Epsilon && Math.Abs(m1x4) < double.Epsilon && Math.Abs(m1x5) < double.Epsilon
            && Math.Abs(m2x0) < double.Epsilon && Math.Abs(m2x1) < double.Epsilon && Math.Abs(m2x2 - 1d) < double.Epsilon && Math.Abs(m2x3) < double.Epsilon && Math.Abs(m2x4) < double.Epsilon && Math.Abs(m2x5) < double.Epsilon
            && Math.Abs(m3x0) < double.Epsilon && Math.Abs(m3x1) < double.Epsilon && Math.Abs(m3x2) < double.Epsilon && Math.Abs(m3x3 - 1d) < double.Epsilon && Math.Abs(m3x4) < double.Epsilon && Math.Abs(m3x5) < double.Epsilon
            && Math.Abs(m4x0) < double.Epsilon && Math.Abs(m4x1) < double.Epsilon && Math.Abs(m4x2) < double.Epsilon && Math.Abs(m4x3) < double.Epsilon && Math.Abs(m4x4 - 1d) < double.Epsilon && Math.Abs(m4x5) < double.Epsilon
            && Math.Abs(m5x0) < double.Epsilon && Math.Abs(m5x1) < double.Epsilon && Math.Abs(m5x2) < double.Epsilon && Math.Abs(m5x3) < double.Epsilon && Math.Abs(m5x4) < double.Epsilon && Math.Abs(m5x5 - 1d) < double.Epsilon;
        #endregion Is Identity

        #region Adjoint
        /// <summary>
        /// The adjoint.
        /// </summary>
        /// <param name="m0x0">The M0X0.</param>
        /// <param name="m0x1">The M0X1.</param>
        /// <param name="m1x0">The M1X0.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <returns>
        /// The <see cref="Matrix2x2D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1,
            double m1x0, double m1x1
            ) AdjointMatrix(
            double m0x0, double m0x1,
            double m1x0, double m1x1)
            => (
                m1x1, -m0x1,
                -m1x0, m0x0);

        /// <summary>
        /// The adjoint.
        /// </summary>
        /// <param name="m0x0">The M0X0.</param>
        /// <param name="m0x1">The M0X1.</param>
        /// <param name="m0x2">The M0X2.</param>
        /// <param name="m1x0">The M1X0.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m2x0">The M2X0.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <returns>
        /// The <see cref="Matrix3x3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2
            ) AdjointMatrix(
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2)
            => (
                (m1x1 * m2x2) - (m1x2 * m2x1), -((m0x1 * m2x2) - (m0x2 * m2x1)), (m0x1 * m1x2) - (m0x2 * m1x1),
                -((m1x0 * m2x2) - (m1x2 * m2x0)), (m0x0 * m2x2) - (m0x2 * m2x0), -((m0x0 * m1x2) - (m0x2 * m1x0)),
                (m1x0 * m2x1) - (m1x1 * m2x0), -((m0x0 * m2x1) - (m0x1 * m2x0)), (m0x0 * m1x1) - (m0x1 * m1x0));

        /// <summary>
        /// Used to generate the adjoint of this matrix.
        /// </summary>
        /// <param name="m0x0">The M0X0.</param>
        /// <param name="m0x1">The M0X1.</param>
        /// <param name="m0x2">The M0X2.</param>
        /// <param name="m0x3">The M0X3.</param>
        /// <param name="m1x0">The M1X0.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m1x3">The M1X3.</param>
        /// <param name="m2x0">The M2X0.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <param name="m2x3">The M2X3.</param>
        /// <param name="m3x0">The M3X0.</param>
        /// <param name="m3x1">The M3X1.</param>
        /// <param name="m3x2">The M3X2.</param>
        /// <param name="m3x3">The M3X3.</param>
        /// <returns>
        /// The adjoint matrix of the current instance.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3,
            double m1x0, double m1x1, double m1x2, double m1x3,
            double m2x0, double m2x1, double m2x2, double m2x3,
            double m3x0, double m3x1, double m3x2, double m3x3
            ) AdjointMatrix(
            double m0x0, double m0x1, double m0x2, double m0x3,
            double m1x0, double m1x1, double m1x2, double m1x3,
            double m2x0, double m2x1, double m2x2, double m2x3,
            double m3x0, double m3x1, double m3x2, double m3x3)
        {
            var m22m33m32m23 = (m2x2 * m3x3) - (m3x2 * m2x3);
            var m21m33m31m23 = (m2x1 * m3x3) - (m3x1 * m2x3);
            var m21m32m31m22 = (m2x1 * m3x2) - (m3x1 * m2x2);

            var m12m33m32m13 = (m1x2 * m3x3) - (m3x2 * m1x3);
            var m11m33m31m13 = (m1x1 * m3x3) - (m3x1 * m1x3);
            var m11m32m31m12 = (m1x1 * m3x2) - (m3x1 * m1x2);

            var m12m23m22m13 = (m1x2 * m2x3) - (m2x2 * m1x3);
            var m11m23m21m13 = (m1x1 * m2x3) - (m2x1 * m1x3);
            var m11m22m21m12 = (m1x1 * m2x2) - (m2x1 * m1x2);

            var m20m33m30m23 = (m2x0 * m3x3) - (m3x0 * m2x3);
            var m20m32m30m22 = (m2x0 * m3x2) - (m3x0 * m2x2);
            var m10m33m30m13 = (m1x0 * m3x3) - (m3x0 * m1x3);

            var m10m32m30m12 = (m1x0 * m3x2) - (m3x0 * m1x2);
            var m10m23m20m13 = (m1x0 * m2x3) - (m2x0 * m1x3);
            var m10m22m20m12 = (m1x0 * m2x2) - (m2x0 * m1x2);

            var m20m31m30m21 = (m2x0 * m3x1) - (m3x0 * m2x1);
            var m10m31m30m11 = (m1x0 * m3x1) - (m3x0 * m1x1);
            var m10m21m20m11 = (m1x0 * m2x1) - (m2x0 * m1x1);

            return (
                (m1x1 * m22m33m32m23) - (m1x2 * m21m33m31m23) + (m1x3 * m21m32m31m22),
                -((m0x1 * m22m33m32m23) - (m0x2 * m21m33m31m23) + (m0x3 * m21m32m31m22)),
                (m0x1 * m12m33m32m13) - (m0x2 * m11m33m31m13) + (m0x3 * m11m32m31m12),
                -((m0x1 * m12m23m22m13) - (m0x2 * m11m23m21m13) + (m0x3 * m11m22m21m12)),
                -((m1x0 * m22m33m32m23) - (m1x2 * m20m33m30m23) + (m1x3 * m20m32m30m22)),
                (m0x0 * m22m33m32m23) - (m0x2 * m20m33m30m23) + (m0x3 * m20m32m30m22),
                -((m0x0 * m12m33m32m13) - (m0x2 * m10m33m30m13) + (m0x3 * m10m32m30m12)),
                (m0x0 * m12m23m22m13) - (m0x2 * m10m23m20m13) + (m0x3 * m10m22m20m12),
                (m1x0 * m21m33m31m23) - (m1x1 * m20m33m30m23) + (m1x3 * m20m31m30m21),
                -((m0x0 * m21m33m31m23) - (m0x1 * m20m33m30m23) + (m0x3 * m20m31m30m21)),
                (m0x0 * m11m33m31m13) - (m0x1 * m10m33m30m13) + (m0x3 * m20m31m30m21),
                -((m0x0 * m11m23m21m13) - (m0x1 * m10m23m20m13) + (m0x3 * m10m21m20m11)),
                -((m1x0 * m21m32m31m22) - (m1x1 * m20m32m30m22) + (m1x2 * m20m31m30m21)),
                (m0x0 * m21m32m31m22) - (m0x1 * m20m32m30m22) + (m0x2 * m20m31m30m21),
                -((m0x0 * m11m32m31m12) - (m0x1 * m10m32m30m12) + (m0x2 * m10m31m30m11)),
                (m0x0 * m11m22m21m12) - (m0x1 * m10m22m20m12) + (m0x2 * m10m21m20m11));
        }

        /// <summary>
        /// Adjoints the specified M0X0.
        /// </summary>
        /// <param name="m0x0">The M0X0.</param>
        /// <param name="m0x1">The M0X1.</param>
        /// <param name="m0x2">The M0X2.</param>
        /// <param name="m0x3">The M0X3.</param>
        /// <param name="m0x4">The M0X4.</param>
        /// <param name="m1x0">The M1X0.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m1x3">The M1X3.</param>
        /// <param name="m1x4">The M1X4.</param>
        /// <param name="m2x0">The M2X0.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <param name="m2x3">The M2X3.</param>
        /// <param name="m2x4">The M2X4.</param>
        /// <param name="m3x0">The M3X0.</param>
        /// <param name="m3x1">The M3X1.</param>
        /// <param name="m3x2">The M3X2.</param>
        /// <param name="m3x3">The M3X3.</param>
        /// <param name="m3x4">The M3X4.</param>
        /// <param name="m4x0">The M4X0.</param>
        /// <param name="m4x1">The M4X1.</param>
        /// <param name="m4x2">The M4X2.</param>
        /// <param name="m4x3">The M4X3.</param>
        /// <param name="m4x4">The M4X4.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4
            ) AdjointMatrix(
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4
            ) => throw new NotImplementedException();

        /// <summary>
        /// Adjoints the specified M0X0.
        /// </summary>
        /// <param name="m0x0">The M0X0.</param>
        /// <param name="m0x1">The M0X1.</param>
        /// <param name="m0x2">The M0X2.</param>
        /// <param name="m0x3">The M0X3.</param>
        /// <param name="m0x4">The M0X4.</param>
        /// <param name="m0x5">The M0X5.</param>
        /// <param name="m1x0">The M1X0.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m1x3">The M1X3.</param>
        /// <param name="m1x4">The M1X4.</param>
        /// <param name="m1x5">The M1X5.</param>
        /// <param name="m2x0">The M2X0.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <param name="m2x3">The M2X3.</param>
        /// <param name="m2x4">The M2X4.</param>
        /// <param name="m2x5">The M2X5.</param>
        /// <param name="m3x0">The M3X0.</param>
        /// <param name="m3x1">The M3X1.</param>
        /// <param name="m3x2">The M3X2.</param>
        /// <param name="m3x3">The M3X3.</param>
        /// <param name="m3x4">The M3X4.</param>
        /// <param name="m3x5">The M3X5.</param>
        /// <param name="m4x0">The M4X0.</param>
        /// <param name="m4x1">The M4X1.</param>
        /// <param name="m4x2">The M4X2.</param>
        /// <param name="m4x3">The M4X3.</param>
        /// <param name="m4x4">The M4X4.</param>
        /// <param name="m4x5">The M4X5.</param>
        /// <param name="m5x0">The M5X0.</param>
        /// <param name="m5x1">The M5X1.</param>
        /// <param name="m5x2">The M5X2.</param>
        /// <param name="m5x3">The M5X3.</param>
        /// <param name="m5x4">The M5X4.</param>
        /// <param name="m5x5">The M5X5.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4, double m0x5,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x0, double m5x1, double m5x2, double m5x3, double m5x4, double m5x5
            ) AdjointMatrix(
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4, double m0x5,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x0, double m5x1, double m5x2, double m5x3, double m5x4, double m5x5
            ) => throw new NotImplementedException();
        #endregion Adjoint

        #region Cofactor
        /// <summary>
        /// The cofactor.
        /// </summary>
        /// <param name="m0x0">The M0X0.</param>
        /// <param name="m0x1">The M0X1.</param>
        /// <param name="m1x0">The M1X0.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <returns>
        /// The <see cref="Matrix2x2D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1,
            double m1x0, double m1x1
            ) CofactorMatrix(
            double m0x0, double m0x1,
            double m1x0, double m1x1)
            => (-m1x1, m0x1,
                m1x0, -m0x0);

        /// <summary>
        /// The cofactor.
        /// </summary>
        /// <param name="m0x0">The M0X0.</param>
        /// <param name="m0x1">The M0X1.</param>
        /// <param name="m0x2">The M0X2.</param>
        /// <param name="m1x0">The M1X0.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m2x0">The M2X0.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <returns>
        /// The <see cref="Matrix3x3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2
            ) CofactorMatrix(
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2)
            => (-((m1x1 * m2x2) - (m1x2 * m2x1)), (m0x1 * m2x2) - (m0x2 * m2x1), -((m0x1 * m1x2) - (m0x2 * m1x1)),
                (m1x0 * m2x2) - (m1x2 * m2x0), -((m0x0 * m2x2) - (m0x2 * m2x0)), (m0x0 * m1x2) - (m0x2 * m1x0),
                -((m1x0 * m2x1) - (m1x1 * m2x0)), (m0x0 * m2x1) - (m0x1 * m2x0), -((m0x0 * m1x1) - (m0x1 * m1x0)));

        /// <summary>
        /// The cofactor.
        /// </summary>
        /// <param name="m0x0">The M0X0.</param>
        /// <param name="m0x1">The M0X1.</param>
        /// <param name="m0x2">The M0X2.</param>
        /// <param name="m0x3">The M0X3.</param>
        /// <param name="m1x0">The M1X0.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m1x3">The M1X3.</param>
        /// <param name="m2x0">The M2X0.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <param name="m2x3">The M2X3.</param>
        /// <param name="m3x0">The M3X0.</param>
        /// <param name="m3x1">The M3X1.</param>
        /// <param name="m3x2">The M3X2.</param>
        /// <param name="m3x3">The M3X3.</param>
        /// <returns>
        /// The <see cref="Matrix4x4D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3,
            double m1x0, double m1x1, double m1x2, double m1x3,
            double m2x0, double m2x1, double m2x2, double m2x3,
            double m3x0, double m3x1, double m3x2, double m3x3
            ) CofactorMatrix(
            double m0x0, double m0x1, double m0x2, double m0x3,
            double m1x0, double m1x1, double m1x2, double m1x3,
            double m2x0, double m2x1, double m2x2, double m2x3,
            double m3x0, double m3x1, double m3x2, double m3x3)
        {
            var m22m33m32m23 = (m2x2 * m3x3) - (m3x2 * m2x3);
            var m21m33m31m23 = (m2x1 * m3x3) - (m3x1 * m2x3);
            var m21m32m31m22 = (m2x1 * m3x2) - (m3x1 * m2x2);
            var m12m33m32m13 = (m1x2 * m3x3) - (m3x2 * m1x3);

            var m11m33m31m13 = (m1x1 * m3x3) - (m3x1 * m1x3);
            var m11m32m31m12 = (m1x1 * m3x2) - (m3x1 * m1x2);
            var m12m23m22m13 = (m1x2 * m2x3) - (m2x2 * m1x3);
            var m11m23m21m13 = (m1x1 * m2x3) - (m2x1 * m1x3);

            var m11m22m21m12 = (m1x1 * m2x2) - (m2x1 * m1x2);
            var m20m33m30m23 = (m2x0 * m3x3) - (m3x0 * m2x3);
            var m20m32m30m22 = (m2x0 * m3x2) - (m3x0 * m2x2);
            var m10m33m30m13 = (m1x0 * m3x3) - (m3x0 * m1x3);

            var m10m32m30m12 = (m1x0 * m3x2) - (m3x0 * m1x2);
            var m10m23m20m13 = (m1x0 * m2x3) - (m2x0 * m1x3);
            var m10m22m20m12 = (m1x0 * m2x2) - (m2x0 * m1x2);
            var m20m31m30m21 = (m2x0 * m3x1) - (m3x0 * m2x1);

            var m10m31m30m11 = (m1x0 * m3x1) - (m3x0 * m1x1);
            var m10m21m20m11 = (m1x0 * m2x1) - (m2x0 * m1x1);

            return (
                -((m1x1 * m22m33m32m23) - (m1x2 * m21m33m31m23) + (m1x3 * m21m32m31m22)),
                (m0x1 * m22m33m32m23) - (m0x2 * m21m33m31m23) + (m0x3 * m21m32m31m22),
                -((m0x1 * m12m33m32m13) - (m0x2 * m11m33m31m13) + (m0x3 * m11m32m31m12)),
                (m0x1 * m12m23m22m13) - (m0x2 * m11m23m21m13) + (m0x3 * m11m22m21m12),
                (m1x0 * m22m33m32m23) - (m1x2 * m20m33m30m23) + (m1x3 * m20m32m30m22),
                -((m0x0 * m22m33m32m23) - (m0x2 * m20m33m30m23) + (m0x3 * m20m32m30m22)),
                (m0x0 * m12m33m32m13) - (m0x2 * m10m33m30m13) + (m0x3 * m10m32m30m12),
                -((m0x0 * m12m23m22m13) - (m0x2 * m10m23m20m13) + (m0x3 * m10m22m20m12)),
                -((m1x0 * m21m33m31m23) - (m1x1 * m20m33m30m23) + (m1x3 * m20m31m30m21)),
                (m0x0 * m21m33m31m23) - (m0x1 * m20m33m30m23) + (m0x3 * m20m31m30m21),
                -((m0x0 * m11m33m31m13) - (m0x1 * m10m33m30m13) + (m0x3 * m20m31m30m21)),
                (m0x0 * m11m23m21m13) - (m0x1 * m10m23m20m13) + (m0x3 * m10m21m20m11),
                (m1x0 * m21m32m31m22) - (m1x1 * m20m32m30m22) + (m1x2 * m20m31m30m21),
                -((m0x0 * m21m32m31m22) - (m0x1 * m20m32m30m22) + (m0x2 * m20m31m30m21)),
                (m0x0 * m11m32m31m12) - (m0x1 * m10m32m30m12) + (m0x2 * m10m31m30m11),
                -((m0x0 * m11m22m21m12) - (m0x1 * m10m22m20m12) + (m0x2 * m10m21m20m11)));
        }

        /// <summary>
        /// Cofactors the specified M0X0.
        /// </summary>
        /// <param name="m0x0">The M0X0.</param>
        /// <param name="m0x1">The M0X1.</param>
        /// <param name="m0x2">The M0X2.</param>
        /// <param name="m0x3">The M0X3.</param>
        /// <param name="m0x4">The M0X4.</param>
        /// <param name="m1x0">The M1X0.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m1x3">The M1X3.</param>
        /// <param name="m1x4">The M1X4.</param>
        /// <param name="m2x0">The M2X0.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <param name="m2x3">The M2X3.</param>
        /// <param name="m2x4">The M2X4.</param>
        /// <param name="m3x0">The M3X0.</param>
        /// <param name="m3x1">The M3X1.</param>
        /// <param name="m3x2">The M3X2.</param>
        /// <param name="m3x3">The M3X3.</param>
        /// <param name="m3x4">The M3X4.</param>
        /// <param name="m4x0">The M4X0.</param>
        /// <param name="m4x1">The M4X1.</param>
        /// <param name="m4x2">The M4X2.</param>
        /// <param name="m4x3">The M4X3.</param>
        /// <param name="m4x4">The M4X4.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4
            ) CofactorMatrix(
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4
            ) => throw new NotImplementedException();

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4, double m0x5,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x0, double m5x1, double m5x2, double m5x3, double m5x4, double m5x5
            ) CofactorMatrix(
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4, double m0x5,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x0, double m5x1, double m5x2, double m5x3, double m5x4, double m5x5
            ) => throw new NotImplementedException();
        #endregion Cofactor

        #region Invert
        /// <summary>
        /// The invert.
        /// </summary>
        /// <param name="m0x0">The M0X0.</param>
        /// <param name="m0x1">The M0X1.</param>
        /// <param name="m1x0">The M1X0.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <returns>
        /// The <see cref="Matrix2x2D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1,
            double m1x0, double m1x1
            ) InvertMatrix(
            double m0x0, double m0x1,
            double m1x0, double m1x1)
        {
            var detInv = 1d / ((m0x0 * m1x1) - (m0x1 * m1x0));
            return (
                detInv * m1x1, detInv * -m0x1,
                detInv * -m1x0, detInv * m0x0);
        }

        /// <summary>
        /// The invert.
        /// </summary>
        /// <param name="m0x0">The M0X0.</param>
        /// <param name="m0x1">The M0X1.</param>
        /// <param name="m0x2">The M0X2.</param>
        /// <param name="m1x0">The M1X0.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m2x0">The M2X0.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <returns>
        /// The <see cref="Matrix3x3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2
            ) InvertMatrix(
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2)
        {
            var m11m22m12m21 = (m1x1 * m2x2) - (m1x2 * m2x1);
            var m10m22m12m20 = (m1x0 * m2x2) - (m1x2 * m2x0);
            var m10m21m11m20 = (m1x0 * m2x1) - (m1x1 * m2x0);
            var detInv = 1d / ((m0x0 * m11m22m12m21) - (m0x1 * m10m22m12m20) + (m0x2 * m10m21m11m20));
            return (
                detInv * m11m22m12m21, detInv * (-((m0x1 * m2x2) - (m0x2 * m2x1))), detInv * ((m0x1 * m1x2) - (m0x2 * m1x1)),
                detInv * (-m10m22m12m20), detInv * ((m0x0 * m2x2) - (m0x2 * m2x0)), detInv * (-((m0x0 * m1x2) - (m0x2 * m1x0))),
                detInv * m10m21m11m20, detInv * (-((m0x0 * m2x1) - (m0x1 * m2x0))), detInv * ((m0x0 * m1x1) - (m0x1 * m1x0)));
        }

        /// <summary>
        /// The invert.
        /// </summary>
        /// <param name="m0x0">The M0X0.</param>
        /// <param name="m0x1">The M0X1.</param>
        /// <param name="m0x2">The M0X2.</param>
        /// <param name="m0x3">The M0X3.</param>
        /// <param name="m1x0">The M1X0.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m1x3">The M1X3.</param>
        /// <param name="m2x0">The M2X0.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <param name="m2x3">The M2X3.</param>
        /// <param name="m3x0">The M3X0.</param>
        /// <param name="m3x1">The M3X1.</param>
        /// <param name="m3x2">The M3X2.</param>
        /// <param name="m3x3">The M3X3.</param>
        /// <returns>
        /// The <see cref="Matrix4x4D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3,
            double m1x0, double m1x1, double m1x2, double m1x3,
            double m2x0, double m2x1, double m2x2, double m2x3,
            double m3x0, double m3x1, double m3x2, double m3x3
            ) InvertMatrix(
            double m0x0, double m0x1, double m0x2, double m0x3,
            double m1x0, double m1x1, double m1x2, double m1x3,
            double m2x0, double m2x1, double m2x2, double m2x3,
            double m3x0, double m3x1, double m3x2, double m3x3)
        {
            var m22m33m32m23 = (m2x2 * m3x3) - (m3x2 * m2x3);
            var m21m33m31m23 = (m2x1 * m3x3) - (m3x1 * m2x3);
            var m21m32m31m22 = (m2x1 * m3x2) - (m3x1 * m2x2);

            var m12m33m32m13 = (m1x2 * m3x3) - (m3x2 * m1x3);
            var m11m33m31m13 = (m1x1 * m3x3) - (m3x1 * m1x3);
            var m11m32m31m12 = (m1x1 * m3x2) - (m3x1 * m1x2);

            var m12m23m22m13 = (m1x2 * m2x3) - (m2x2 * m1x3);
            var m11m23m21m13 = (m1x1 * m2x3) - (m2x1 * m1x3);
            var m11m22m21m12 = (m1x1 * m2x2) - (m2x1 * m1x2);

            var m20m33m30m23 = (m2x0 * m3x3) - (m3x0 * m2x3);
            var m20m32m30m22 = (m2x0 * m3x2) - (m3x0 * m2x2);
            var m10m33m30m13 = (m1x0 * m3x3) - (m3x0 * m1x3);

            var m10m32m30m12 = (m1x0 * m3x2) - (m3x0 * m1x2);
            var m10m23m20m13 = (m1x0 * m2x3) - (m2x0 * m1x3);
            var m10m22m20m12 = (m1x0 * m2x2) - (m2x0 * m1x2);

            var m20m31m30m21 = (m2x0 * m3x1) - (m3x0 * m2x1);
            var m10m31m30m11 = (m1x0 * m3x1) - (m3x0 * m1x1);
            var m10m21m20m11 = (m1x0 * m2x1) - (m2x0 * m1x1);

            var detInv = 1d /
            ((m0x0 * ((m1x1 * m22m33m32m23) - (m1x2 * m21m33m31m23) + (m1x3 * m21m32m31m22))) -
            (m0x1 * ((m1x0 * m22m33m32m23) - (m1x2 * m20m33m30m23) + (m1x3 * m20m32m30m22))) +
            (m0x2 * ((m1x0 * m21m33m31m23) - (m1x1 * m20m33m30m23) + (m1x3 * m20m31m30m21))) -
            (m0x3 * ((m1x0 * m21m32m31m22) - (m1x1 * m20m32m30m22) + (m1x2 * m20m31m30m21))));

            return (
                detInv * ((m1x1 * m22m33m32m23) - (m1x2 * m21m33m31m23) + (m1x3 * m21m32m31m22)),
                detInv * (-((m0x1 * m22m33m32m23) - (m0x2 * m21m33m31m23) + (m0x3 * m21m32m31m22))),
                detInv * ((m0x1 * m12m33m32m13) - (m0x2 * m11m33m31m13) + (m0x3 * m11m32m31m12)),
                detInv * (-((m0x1 * m12m23m22m13) - (m0x2 * m11m23m21m13) + (m0x3 * m11m22m21m12))),
                detInv * (-((m1x0 * m22m33m32m23) - (m1x2 * m20m33m30m23) + (m1x3 * m20m32m30m22))),
                detInv * ((m0x0 * m22m33m32m23) - (m0x2 * m20m33m30m23) + (m0x3 * m20m32m30m22)),
                detInv * (-((m0x0 * m12m33m32m13) - (m0x2 * m10m33m30m13) + (m0x3 * m10m32m30m12))),
                detInv * ((m0x0 * m12m23m22m13) - (m0x2 * m10m23m20m13) + (m0x3 * m10m22m20m12)),
                detInv * ((m1x0 * m21m33m31m23) - (m1x1 * m20m33m30m23) + (m1x3 * m20m31m30m21)),
                detInv * (-((m0x0 * m21m33m31m23) - (m0x1 * m20m33m30m23) + (m0x3 * m20m31m30m21))),
                detInv * ((m0x0 * m11m33m31m13) - (m0x1 * m10m33m30m13) + (m0x3 * m20m31m30m21)),
                detInv * (-((m0x0 * m11m23m21m13) - (m0x1 * m10m23m20m13) + (m0x3 * m10m21m20m11))),
                detInv * (-((m1x0 * m21m32m31m22) - (m1x1 * m20m32m30m22) + (m1x2 * m20m31m30m21))),
                detInv * ((m0x0 * m21m32m31m22) - (m0x1 * m20m32m30m22) + (m0x2 * m20m31m30m21)),
                detInv * (-((m0x0 * m11m32m31m12) - (m0x1 * m10m32m30m12) + (m0x2 * m10m31m30m11))),
                detInv * ((m0x0 * m11m22m21m12) - (m0x1 * m10m22m20m12) + (m0x2 * m10m21m20m11)));
        }

        /// <summary>
        /// Inverts the specified M0X0.
        /// </summary>
        /// <param name="m0x0">The M0X0.</param>
        /// <param name="m0x1">The M0X1.</param>
        /// <param name="m0x2">The M0X2.</param>
        /// <param name="m0x3">The M0X3.</param>
        /// <param name="m0x4">The M0X4.</param>
        /// <param name="m1x0">The M1X0.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m1x3">The M1X3.</param>
        /// <param name="m1x4">The M1X4.</param>
        /// <param name="m2x0">The M2X0.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <param name="m2x3">The M2X3.</param>
        /// <param name="m2x4">The M2X4.</param>
        /// <param name="m3x0">The M3X0.</param>
        /// <param name="m3x1">The M3X1.</param>
        /// <param name="m3x2">The M3X2.</param>
        /// <param name="m3x3">The M3X3.</param>
        /// <param name="m3x4">The M3X4.</param>
        /// <param name="m4x0">The M4X0.</param>
        /// <param name="m4x1">The M4X1.</param>
        /// <param name="m4x2">The M4X2.</param>
        /// <param name="m4x3">The M4X3.</param>
        /// <param name="m4x4">The M4X4.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4
            ) InvertMatrix(
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4
            ) => throw new NotImplementedException();

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4, double m0x5,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x0, double m5x1, double m5x2, double m5x3, double m5x4, double m5x5
            ) InvertMatrix(
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4, double m0x5,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x0, double m5x1, double m5x2, double m5x3, double m5x4, double m5x5
            ) => throw new NotImplementedException();
        #endregion Invert

        #region Transpose
        /// <summary>
        /// Swap the rows of the matrix with the columns.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <returns>
        /// A transposed Matrix.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double a, double b,
            double c, double d)
            TransposeMatrix(
            double a, double b,
            double c, double d)
            => (
                a, c,
                b, d
            );

        /// <summary>
        /// Swap the rows of the matrix with the columns.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="e">The e.</param>
        /// <param name="f">The f.</param>
        /// <param name="g">The g.</param>
        /// <param name="h">The h.</param>
        /// <param name="i">The i.</param>
        /// <returns>
        /// A transposed Matrix.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double a, double b, double c,
            double d, double e, double f,
            double g, double h, double i
            ) TransposeMatrix(
            double a, double b, double c,
            double d, double e, double f,
            double g, double h, double i)
            => (
                a, d, g,
                b, e, h,
                c, f, i
            );

        /// <summary>
        /// Swap the rows of the matrix with the columns.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="e">The e.</param>
        /// <param name="f">The f.</param>
        /// <param name="g">The g.</param>
        /// <param name="h">The h.</param>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        /// <param name="l">The l.</param>
        /// <param name="m">The m.</param>
        /// <param name="n">The n.</param>
        /// <param name="o">The o.</param>
        /// <param name="p">The p.</param>
        /// <returns>
        /// A transposed Matrix.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double a, double b, double c, double d,
            double e, double f, double g, double h,
            double i, double j, double k, double l,
            double m, double n, double o, double p
            ) TransposeMatrix(
            double a, double b, double c, double d,
            double e, double f, double g, double h,
            double i, double j, double k, double l,
            double m, double n, double o, double p)
            => (
                a, e, i, m,
                b, f, j, n,
                c, g, k, o,
                d, h, l, p
            );

        /// <summary>
        /// Swap the rows of the matrix with the columns.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="e">The e.</param>
        /// <param name="f">The f.</param>
        /// <param name="g">The g.</param>
        /// <param name="h">The h.</param>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        /// <param name="l">The l.</param>
        /// <param name="m">The m.</param>
        /// <param name="n">The n.</param>
        /// <param name="o">The o.</param>
        /// <param name="p">The p.</param>
        /// <param name="q">The q.</param>
        /// <param name="r">The r.</param>
        /// <param name="s">The s.</param>
        /// <param name="t">The t.</param>
        /// <param name="u">The u.</param>
        /// <param name="v">The v.</param>
        /// <param name="w">The w.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>
        /// A transposed Matrix.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double a, double b, double c, double d, double e,
            double f, double g, double h, double i, double j,
            double k, double l, double m, double n, double o,
            double p, double q, double r, double s, double t,
            double u, double v, double w, double x, double y
            ) TransposeMatrix(
            double a, double b, double c, double d, double e,
            double f, double g, double h, double i, double j,
            double k, double l, double m, double n, double o,
            double p, double q, double r, double s, double t,
            double u, double v, double w, double x, double y)
            => (
                a, f, k, p, u,
                b, g, l, q, v,
                c, h, m, r, w,
                d, i, n, s, x,
                e, j, o, t, y
            );

        /// <summary>
        /// Swap the rows of the matrix with the columns.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="e">The e.</param>
        /// <param name="f">The f.</param>
        /// <param name="g">The g.</param>
        /// <param name="h">The h.</param>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        /// <param name="l">The l.</param>
        /// <param name="m">The m.</param>
        /// <param name="n">The n.</param>
        /// <param name="o">The o.</param>
        /// <param name="p">The p.</param>
        /// <param name="q">The q.</param>
        /// <param name="r">The r.</param>
        /// <param name="s">The s.</param>
        /// <param name="t">The t.</param>
        /// <param name="u">The u.</param>
        /// <param name="v">The v.</param>
        /// <param name="w">The w.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="aa">The aa.</param>
        /// <param name="bb">The bb.</param>
        /// <param name="cc">The cc.</param>
        /// <param name="dd">The dd.</param>
        /// <param name="ee">The ee.</param>
        /// <param name="ff">The ff.</param>
        /// <param name="gg">The gg.</param>
        /// <param name="hh">The hh.</param>
        /// <param name="ii">The ii.</param>
        /// <param name="jj">The jj.</param>
        /// <returns>
        /// A transposed Matrix.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double a, double b, double c, double d, double e, double f,
            double g, double h, double i, double j, double k, double l,
            double m, double n, double o, double p, double q, double r,
            double s, double t, double u, double v, double w, double x,
            double y, double z, double aa, double bb, double cc, double dd,
            double ee, double ff, double gg, double hh, double ii, double jj
            ) TransposeMatrix(
            double a, double b, double c, double d, double e, double f,
            double g, double h, double i, double j, double k, double l,
            double m, double n, double o, double p, double q, double r,
            double s, double t, double u, double v, double w, double x,
            double y, double z, double aa, double bb, double cc, double dd,
            double ee, double ff, double gg, double hh, double ii, double jj)
            => (
                a, g, m, s, y, ee,
                b, h, n, t, z, ff,
                c, i, o, u, aa, gg,
                d, j, p, v, bb, hh,
                e, k, q, w, cc, ii,
                f, l, r, x, dd, jj
            );
        #endregion Transpose

        #region Determinant
        /// <summary>
        /// Find the determinant of a 2 by 2 matrix.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/onlyuser/Legacy/blob/master/msvb/Dex3d/Math.bas
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MatrixDeterminant(
            double a, double b,
            double c, double d)
            => (a * d)
              - (b * c);

        /// <summary>
        /// Find the determinant of a 3 by 3 matrix.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="e">The e.</param>
        /// <param name="f">The f.</param>
        /// <param name="g">The g.</param>
        /// <param name="h">The h.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/onlyuser/Legacy/blob/master/msvb/Dex3d/Math.bas
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MatrixDeterminant(
            double a, double b, double c,
            double d, double e, double f,
            double g, double h, double i)
            => (a * MatrixDeterminant(e, f, h, i))
              - (b * MatrixDeterminant(d, f, g, i))
              + (c * MatrixDeterminant(d, e, g, h));

        /// <summary>
        /// Find the determinant of a 4 by 4 matrix.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="e">The e.</param>
        /// <param name="f">The f.</param>
        /// <param name="g">The g.</param>
        /// <param name="h">The h.</param>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        /// <param name="l">The l.</param>
        /// <param name="m">The m.</param>
        /// <param name="n">The n.</param>
        /// <param name="o">The o.</param>
        /// <param name="p">The p.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/onlyuser/Legacy/blob/master/msvb/Dex3d/Math.bas
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MatrixDeterminant(
            double a, double b, double c, double d,
            double e, double f, double g, double h,
            double i, double j, double k, double l,
            double m, double n, double o, double p)
            => (a * MatrixDeterminant(f, g, h, j, k, l, n, o, p))
              - (b * MatrixDeterminant(e, g, h, i, k, l, m, o, p))
              + (c * MatrixDeterminant(e, f, h, i, j, l, m, n, p))
              - (d * MatrixDeterminant(e, f, g, i, j, k, m, n, o));

        /// <summary>
        /// Find the determinant of a 5 by 5 matrix.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="e">The e.</param>
        /// <param name="f">The f.</param>
        /// <param name="g">The g.</param>
        /// <param name="h">The h.</param>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        /// <param name="l">The l.</param>
        /// <param name="m">The m.</param>
        /// <param name="n">The n.</param>
        /// <param name="o">The o.</param>
        /// <param name="p">The p.</param>
        /// <param name="q">The q.</param>
        /// <param name="r">The r.</param>
        /// <param name="s">The s.</param>
        /// <param name="t">The t.</param>
        /// <param name="u">The u.</param>
        /// <param name="v">The v.</param>
        /// <param name="w">The w.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MatrixDeterminant(
            double a, double b, double c, double d, double e,
            double f, double g, double h, double i, double j,
            double k, double l, double m, double n, double o,
            double p, double q, double r, double s, double t,
            double u, double v, double w, double x, double y)
            => (a * MatrixDeterminant(g, h, i, j, l, m, n, o, q, r, s, t, v, w, x, y))
              - (b * MatrixDeterminant(f, h, i, j, k, m, n, o, p, r, s, t, u, w, x, y))
              + (c * MatrixDeterminant(f, g, i, j, k, l, n, o, p, q, s, t, u, v, x, y))
              - (d * MatrixDeterminant(f, g, h, j, k, l, m, o, p, q, r, t, u, v, w, y))
              + (e * MatrixDeterminant(f, g, h, i, k, l, m, n, p, q, r, s, u, v, w, x));

        /// <summary>
        /// Find the determinant of a 6 by 6 matrix.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="e">The e.</param>
        /// <param name="f">The f.</param>
        /// <param name="g">The g.</param>
        /// <param name="h">The h.</param>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        /// <param name="l">The l.</param>
        /// <param name="m">The m.</param>
        /// <param name="n">The n.</param>
        /// <param name="o">The o.</param>
        /// <param name="p">The p.</param>
        /// <param name="q">The q.</param>
        /// <param name="r">The r.</param>
        /// <param name="s">The s.</param>
        /// <param name="t">The t.</param>
        /// <param name="u">The u.</param>
        /// <param name="v">The v.</param>
        /// <param name="w">The w.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="aa">The aa.</param>
        /// <param name="bb">The bb.</param>
        /// <param name="cc">The cc.</param>
        /// <param name="dd">The dd.</param>
        /// <param name="ee">The ee.</param>
        /// <param name="ff">The ff.</param>
        /// <param name="gg">The gg.</param>
        /// <param name="hh">The hh.</param>
        /// <param name="ii">The ii.</param>
        /// <param name="jj">The jj.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MatrixDeterminant(
            double a, double b, double c, double d, double e, double f,
            double g, double h, double i, double j, double k, double l,
            double m, double n, double o, double p, double q, double r,
            double s, double t, double u, double v, double w, double x,
            double y, double z, double aa, double bb, double cc, double dd,
            double ee, double ff, double gg, double hh, double ii, double jj)
            => (a * MatrixDeterminant(h, i, j, k, l, n, o, p, q, r, t, u, v, w, x, z, aa, bb, cc, dd, ff, gg, hh, ii, jj))
              - (b * MatrixDeterminant(g, i, j, k, l, m, o, p, q, r, s, u, v, w, x, y, aa, bb, cc, dd, ee, gg, hh, ii, jj))
              + (c * MatrixDeterminant(g, h, j, k, l, m, n, p, q, r, s, t, v, w, x, y, z, bb, cc, dd, ee, ff, hh, ii, jj))
              - (d * MatrixDeterminant(g, h, i, k, l, m, n, o, q, r, s, t, u, w, x, y, z, aa, cc, dd, ee, ff, gg, ii, jj))
              + (e * MatrixDeterminant(g, h, i, j, l, m, n, o, p, r, s, t, u, v, x, y, z, aa, bb, dd, ee, ff, gg, hh, jj))
              - (f * MatrixDeterminant(g, h, i, j, k, m, n, o, p, q, s, t, u, v, w, y, z, aa, bb, cc, ee, ff, gg, hh, ii));
        #endregion Determinant

        #region Inverse Determinant
        /// <summary>
        /// Find the inverse of the determinant of a 2 by 2 matrix.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MatrixInverseDeterminant(
            double a, double b,
            double c, double d)
            => 1d / ((a * d)
              - (b * c));

        /// <summary>
        /// Find the inverse of the determinant of a 3 by 3 matrix.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="e">The e.</param>
        /// <param name="f">The f.</param>
        /// <param name="g">The g.</param>
        /// <param name="h">The h.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/onlyuser/Legacy/blob/master/msvb/Dex3d/Math.bas
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MatrixInverseDeterminant(
            double a, double b, double c,
            double d, double e, double f,
            double g, double h, double i)
            => 1d / ((a * MatrixDeterminant(e, f, h, i))
              - (b * MatrixDeterminant(d, f, g, i))
              + (c * MatrixDeterminant(d, e, g, h)));

        /// <summary>
        /// Find the inverse of the determinant of a 4 by 4 matrix.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="e">The e.</param>
        /// <param name="f">The f.</param>
        /// <param name="g">The g.</param>
        /// <param name="h">The h.</param>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        /// <param name="l">The l.</param>
        /// <param name="m">The m.</param>
        /// <param name="n">The n.</param>
        /// <param name="o">The o.</param>
        /// <param name="p">The p.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/onlyuser/Legacy/blob/master/msvb/Dex3d/Math.bas
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MatrixInverseDeterminant(
            double a, double b, double c, double d,
            double e, double f, double g, double h,
            double i, double j, double k, double l,
            double m, double n, double o, double p)
            => 1d / ((a * MatrixDeterminant(f, g, h, j, k, l, n, o, p))
              - (b * MatrixDeterminant(e, g, h, i, k, l, m, o, p))
              + (c * MatrixDeterminant(e, f, h, i, j, l, m, n, p))
              - (d * MatrixDeterminant(e, f, g, i, j, k, m, n, o)));

        /// <summary>
        /// Find the inverse of the determinant of a 5 by 5 matrix.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="e">The e.</param>
        /// <param name="f">The f.</param>
        /// <param name="g">The g.</param>
        /// <param name="h">The h.</param>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        /// <param name="l">The l.</param>
        /// <param name="m">The m.</param>
        /// <param name="n">The n.</param>
        /// <param name="o">The o.</param>
        /// <param name="p">The p.</param>
        /// <param name="q">The q.</param>
        /// <param name="r">The r.</param>
        /// <param name="s">The s.</param>
        /// <param name="t">The t.</param>
        /// <param name="u">The u.</param>
        /// <param name="v">The v.</param>
        /// <param name="w">The w.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MatrixInverseDeterminant(
            double a, double b, double c, double d, double e,
            double f, double g, double h, double i, double j,
            double k, double l, double m, double n, double o,
            double p, double q, double r, double s, double t,
            double u, double v, double w, double x, double y)
            => 1d / ((a * MatrixDeterminant(g, h, i, j, l, m, n, o, q, r, s, t, v, w, x, y))
              - (b * MatrixDeterminant(f, h, i, j, k, m, n, o, p, r, s, t, u, w, x, y))
              + (c * MatrixDeterminant(f, g, i, j, k, l, n, o, p, q, s, t, u, v, x, y))
              - (d * MatrixDeterminant(f, g, h, j, k, l, m, o, p, q, r, t, u, v, w, y))
              + (e * MatrixDeterminant(f, g, h, i, k, l, m, n, p, q, r, s, u, v, w, x)));

        /// <summary>
        /// Find the inverse of the determinant of a 6 by 6 matrix.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="e">The e.</param>
        /// <param name="f">The f.</param>
        /// <param name="g">The g.</param>
        /// <param name="h">The h.</param>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        /// <param name="l">The l.</param>
        /// <param name="m">The m.</param>
        /// <param name="n">The n.</param>
        /// <param name="o">The o.</param>
        /// <param name="p">The p.</param>
        /// <param name="q">The q.</param>
        /// <param name="r">The r.</param>
        /// <param name="s">The s.</param>
        /// <param name="t">The t.</param>
        /// <param name="u">The u.</param>
        /// <param name="v">The v.</param>
        /// <param name="w">The w.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="aa">The aa.</param>
        /// <param name="bb">The bb.</param>
        /// <param name="cc">The cc.</param>
        /// <param name="dd">The dd.</param>
        /// <param name="ee">The ee.</param>
        /// <param name="ff">The ff.</param>
        /// <param name="gg">The gg.</param>
        /// <param name="hh">The hh.</param>
        /// <param name="ii">The ii.</param>
        /// <param name="jj">The jj.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MatrixInverseDeterminant(
            double a, double b, double c, double d, double e, double f,
            double g, double h, double i, double j, double k, double l,
            double m, double n, double o, double p, double q, double r,
            double s, double t, double u, double v, double w, double x,
            double y, double z, double aa, double bb, double cc, double dd,
            double ee, double ff, double gg, double hh, double ii, double jj)
            => 1d / ((a * MatrixDeterminant(h, i, j, k, l, n, o, p, q, r, t, u, v, w, x, z, aa, bb, cc, dd, ff, gg, hh, ii, jj))
              - (b * MatrixDeterminant(g, i, j, k, l, m, o, p, q, r, s, u, v, w, x, y, aa, bb, cc, dd, ee, gg, hh, ii, jj))
              + (c * MatrixDeterminant(g, h, j, k, l, m, n, p, q, r, s, t, v, w, x, y, z, bb, cc, dd, ee, ff, hh, ii, jj))
              - (d * MatrixDeterminant(g, h, i, k, l, m, n, o, q, r, s, t, u, w, x, y, z, aa, cc, dd, ee, ff, gg, ii, jj))
              + (e * MatrixDeterminant(g, h, i, j, l, m, n, o, p, r, s, t, u, v, x, y, z, aa, bb, dd, ee, ff, gg, hh, jj))
              - (f * MatrixDeterminant(g, h, i, j, k, m, n, o, p, q, s, t, u, v, w, y, z, aa, bb, cc, ee, ff, gg, hh, ii)));
        #endregion Inverse Determinant
    }
}
