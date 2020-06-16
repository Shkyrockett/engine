// <copyright file="Operations.Arithmatics.cs" company="Shkyrockett" >
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
    public static partial class Operations
    {
        #region Unary Plus
        /// <summary>
        /// Unary Plus 2d.
        /// </summary>
        /// <param name="valueA">The value a.</param>
        /// <param name="valueB">The value b.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) Plus(double valueA, double valueB) => (+valueA, +valueB);

        /// <summary>
        /// Unary Plus 3d.
        /// </summary>
        /// <param name="valueA">The value a.</param>
        /// <param name="valueB">The value b.</param>
        /// <param name="valueC">The value c.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) Plus(double valueA, double valueB, double valueC) => (+valueA, +valueB, +valueC);

        /// <summary>
        /// Unary Plus 4d.
        /// </summary>
        /// <param name="valueA">The value a.</param>
        /// <param name="valueB">The value b.</param>
        /// <param name="valueC">The value c.</param>
        /// <param name="valueD">The value d.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) Plus(double valueA, double valueB, double valueC, double valueD) => (+valueA, +valueB, +valueC, +valueD);

        /// <summary>
        /// Unary Plus 5d.
        /// </summary>
        /// <param name="valueA">The value a.</param>
        /// <param name="valueB">The value b.</param>
        /// <param name="valueC">The value c.</param>
        /// <param name="valueD">The value d.</param>
        /// <param name="valueE">The value e.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E) Plus(double valueA, double valueB, double valueC, double valueD, double valueE) => (+valueA, +valueB, +valueC, +valueD, +valueE);

        /// <summary>
        /// Unary Plus 6d.
        /// </summary>
        /// <param name="valueA">The value a.</param>
        /// <param name="valueB">The value b.</param>
        /// <param name="valueC">The value c.</param>
        /// <param name="valueD">The value d.</param>
        /// <param name="valueE">The value e.</param>
        /// <param name="valueF">The value f.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F) Plus(double valueA, double valueB, double valueC, double valueD, double valueE, double valueF) => (+valueA, +valueB, +valueC, +valueD, +valueE, +valueF);

        /// <summary>
        /// Unary Plus a <see cref="Matrix3x3D" />.
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2
            ) Plus(
            double sourceM0x0, double sourceM0x1, double sourceM0x2,
            double sourceM1x0, double sourceM1x1, double sourceM1x2,
            double sourceM2x0, double sourceM2x1, double sourceM2x2)
            => (+sourceM0x0, +sourceM0x1, +sourceM0x2,
                +sourceM1x0, +sourceM1x1, +sourceM1x2,
                +sourceM2x0, +sourceM2x1, +sourceM2x2);

        /// <summary>
        /// Unary Plus a <see cref="Matrix4x4D" />.
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3,
            double m1x0, double m1x1, double m1x2, double m1x3,
            double m2x0, double m2x1, double m2x2, double m2x3,
            double m3x0, double m3x1, double m3x2, double m3x3
            ) Plus(
            double sourceM0x0, double sourceM0x1, double sourceM0x2, double sourceM0x3,
            double sourceM1x0, double sourceM1x1, double sourceM1x2, double sourceM1x3,
            double sourceM2x0, double sourceM2x1, double sourceM2x2, double sourceM2x3,
            double sourceM3x0, double sourceM3x1, double sourceM3x2, double sourceM3x3)
            => (+sourceM0x0, +sourceM0x1, +sourceM0x2, +sourceM0x3,
                +sourceM1x0, +sourceM1x1, +sourceM1x2, +sourceM1x3,
                +sourceM2x0, +sourceM2x1, +sourceM2x2, +sourceM2x3,
                +sourceM3x0, +sourceM3x1, +sourceM3x2, +sourceM3x3);

        /// <summary>
        /// Unary Plus a Matrix5x5D.
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4
            ) Plus(
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
        /// Unary Plus a Matrix6x6D.
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3, double m0x4, double m0x5,
            double m1x0, double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x0, double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x0, double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x0, double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x0, double m5x1, double m5x2, double m5x3, double m5x4, double m5x5
            ) Plus(
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

        #region Unary Negate
        /// <summary>
        /// Negates the vector.
        /// </summary>
        /// <param name="valueA">The value a.</param>
        /// <param name="valueB">The value b.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) Negate(double valueA, double valueB) => (-valueA, -valueB);

        /// <summary>
        /// Negates the vector.
        /// </summary>
        /// <param name="valueA">The value a.</param>
        /// <param name="valueB">The value b.</param>
        /// <param name="valueC">The value c.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) Negate(double valueA, double valueB, double valueC) => (-valueA, -valueB, -valueC);

        /// <summary>
        /// Negates the vector.
        /// </summary>
        /// <param name="valueA">The value a.</param>
        /// <param name="valueB">The value b.</param>
        /// <param name="valueC">The value c.</param>
        /// <param name="valueD">The value d.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) Negate(double valueA, double valueB, double valueC, double valueD) => (-valueA, -valueB, -valueC, -valueD);

        /// <summary>
        /// Negates the vector.
        /// </summary>
        /// <param name="valueA">The value a.</param>
        /// <param name="valueB">The value b.</param>
        /// <param name="valueC">The value c.</param>
        /// <param name="valueD">The value d.</param>
        /// <param name="valueE">The value e.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E) Negate(double valueA, double valueB, double valueC, double valueD, double valueE) => (-valueA, -valueB, -valueC, -valueD, -valueE);

        /// <summary>
        /// Negates the vector.
        /// </summary>
        /// <param name="valueA">The value a.</param>
        /// <param name="valueB">The value b.</param>
        /// <param name="valueC">The value c.</param>
        /// <param name="valueD">The value d.</param>
        /// <param name="valueE">The value e.</param>
        /// <param name="valueF">The value f.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F) Negate(double valueA, double valueB, double valueC, double valueD, double valueE, double valueF) => (-valueA, -valueB, -valueC, -valueD, -valueE, -valueF);

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
        [DebuggerStepThrough]
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
        [DebuggerStepThrough]
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
        [DebuggerStepThrough]
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
        [DebuggerStepThrough]
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
        public static double Min(double x, double y, double z) => x < y ? x < z ? x : z : y < z ? y : z;

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
        public static int Min(int x, int y, int z) => x < y ? x < z ? x : z : y < z ? y : z;

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
        public static byte Min(byte x, byte y, byte z) => x < y ? x < z ? x : z : y < z ? y : z;

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
        public static double Min2(double x, double y, double z) => Math.Min(x, Math.Min(y, z));

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
        public static double Min2(double w, double x, double y, double z) => Math.Min(w, Math.Min(x, Math.Max(y, z)));
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
        public static double Max(double x, double y, double z) => x > y ? x > z ? x : z : y > z ? y : z;

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
        public static int Max(int x, int y, int z) => x > y ? x > z ? x : z : y > z ? y : z;

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
        public static byte Max(byte x, byte y, byte z) => x > y ? x > z ? x : z : y > z ? y : z;

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
        public static double Max2(double x, double y, double z) => Math.Max(x, Math.Max(y, z));

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
        public static double Max2(double w, double x, double y, double z) => Math.Max(w, Math.Max(x, Math.Max(y, z)));
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
        public static double MinMax(double x, double min, double max) => (x < min) ? min : (x > max) ? max : x;
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
        public static int LeastCommonDenominator(int a, int b) => a * b / GreatestCommonDenominator(a, b);

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
        public static double LeastCommonDenominator(double a, double b) => a * b / GreatestCommonDenominator(a, b);
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
        public static double Round(this float val) => (0f < val) ? (int)(val + 0.5f) : (int)(val - 0.5f);

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
        public static double Round(this double val) => (0d < val) ? (int)(val + 0.5d) : (int)(val - 0.5d);
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
        public static float RoundToMultiple(this float value, float multiple) => Convert.ToInt32(value / multiple) * multiple;

        /// <summary>
        /// Round a value to the nearest multiple of a number.
        /// </summary>
        /// <param name="value">The value to round.</param>
        /// <param name="multiple">The multiple to round to.</param>
        /// <returns>Returns a value rounded to an interval of the multiple.</returns>
        /// <remarks><para>Using Convert.ToInt32 because it is faster and guarantees bankers rounding.</para></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double RoundToMultiple(this double value, double multiple) => Convert.ToInt32(value / multiple) * multiple;
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
        public static sbyte Clamp(this sbyte value, sbyte min, sbyte max) => value > max ? max : value < min ? min : value;

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Clamp(this byte value, byte min, byte max) => value > max ? max : value < min ? min : value;

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short Clamp(this short value, short min, short max) => value > max ? max : value < min ? min : value;

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort Clamp(this ushort value, ushort min, ushort max) => value > max ? max : value < min ? min : value;

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Clamp(this int value, int min, int max) => value > max ? max : value < min ? min : value;

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Clamp(this uint value, uint min, uint max) => value > max ? max : value < min ? min : value;

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Clamp(this long value, long min, long max) => value > max ? max : value < min ? min : value;

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Clamp(this ulong value, ulong min, ulong max) => value > max ? max : value < min ? min : value;

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Clamp(this float value, float min, float max) => value > max ? max : value < min ? min : value;

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Clamp(this double value, double min, double max) => value > max ? max : value < min ? min : value;

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Clamp<T>(this T value, T min, T max) where T : IComparable => (value?.CompareTo(min) < 0) ? min : (value?.CompareTo(max) > 0) ? max : value;
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
        public static sbyte Wrap(this sbyte value, sbyte min, sbyte max) => (value < min) ? (sbyte)(max - ((min - value) % (max - min))) : (sbyte)(min + ((value - min) % (max - min)));

        /// <summary>
        /// The wrap.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="byte"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Wrap(this byte value, byte min, byte max) => (value < min) ? (byte)(max - ((min - value) % (max - min))) : (byte)(min + ((value - min) % (max - min)));

        /// <summary>
        /// The wrap.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="short"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short Wrap(this short value, short min, short max) => (value < min) ? (short)(max - ((min - value) % (max - min))) : (short)(min + ((value - min) % (max - min)));

        /// <summary>
        /// The wrap.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="ushort"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort Wrap(this ushort value, ushort min, ushort max) => (value < min) ? (ushort)(max - ((min - value) % (max - min))) : (ushort)(min + ((value - min) % (max - min)));

        /// <summary>
        /// The wrap.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Wrap(this int value, int min, int max) => (value < min) ? max - ((min - value) % (max - min)) : min + ((value - min) % (max - min));

        /// <summary>
        /// The wrap.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Wrap(this uint value, uint min, uint max) => (value < min) ? max - ((min - value) % (max - min)) : min + ((value - min) % (max - min));

        /// <summary>
        /// The wrap.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="long"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Wrap(this long value, long min, long max) => (value < min) ? max - ((min - value) % (max - min)) : min + ((value - min) % (max - min));

        /// <summary>
        /// The wrap.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="ulong"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Wrap(this ulong value, ulong min, ulong max) => (value < min) ? max - ((min - value) % (max - min)) : min + ((value - min) % (max - min));

        /// <summary>
        /// The wrap.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="float"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Wrap(this float value, float min, float max) => (value < min) ? max - ((min - value) % (max - min)) : min + ((value - min) % (max - min));

        /// <summary>
        /// The wrap.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Wrap(this double value, double min, double max) => (value < min) ? max - ((min - value) % (max - min)) : min + ((value - min) % (max - min));
        #endregion Wrapping

        /// <summary>
        /// Ranges the specified minimum.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <param name="step">The step.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://stackoverflow.com/a/7552870
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> StepRange(double min, double max, double step)
        {
            double i;
            for (i = min; i <= max; i += step)
            {
                yield return i;
            }

            if (i != max + step) // Added only because you want max to be returned as last item
            {
                yield return max;
            }
        }

        /// <summary>
        /// Generates a range of double values from a minimum value to a maximum value.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <param name="numberOfSteps">The number of steps.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">numberOfSteps - Number of steps must be greater than zero.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> CountRange(double min, double max, int numberOfSteps)
        {
            if (numberOfSteps < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfSteps), "Number of steps must be greater than zero.");
            }

            var stepSize = (max - min) / numberOfSteps;
            return Enumerable.Range(0, numberOfSteps + 1).Select(stepIndex => min + (stepIndex * stepSize));
        }
    }
}
