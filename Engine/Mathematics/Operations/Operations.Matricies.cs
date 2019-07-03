// <copyright file="Operations.Matricies.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
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
        #region Add
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
        public static (
            double m0x0, double m0x1,
            double m1x0, double m1x1
            ) Add2x2x2x2(
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
        public static (
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2
            ) Add3x3x3x3(
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
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3,
            double m1x0, double m1x1, double m1x2, double m1x3,
            double m2x0, double m2x1, double m2x2, double m2x3,
            double m3x0, double m3x1, double m3x2, double m3x3
            ) Add4x4x4x4(
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
        #endregion Add

        #region Unary Add
        /// <summary>
        ///	Negates a <see cref="Matrix2x2D"/>.
        /// </summary>
        /// <param name="sourceM0x0"></param>
        /// <param name="sourceM0x1"></param>
        /// <param name="sourceM1x0"></param>
        /// <param name="sourceM1x1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1,
            double m1x0, double m1x1
            ) UnaryAdd(
            double sourceM0x0, double sourceM0x1,
            double sourceM1x0, double sourceM1x1)
            => (+sourceM0x0,
                +sourceM0x1,
                +sourceM1x0,
                +sourceM1x1);

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
        public static (
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2
            ) UnaryAdd(
            double sourceM0x0, double sourceM0x1, double sourceM0x2,
            double sourceM1x0, double sourceM1x1, double sourceM1x2,
            double sourceM2x0, double sourceM2x1, double sourceM2x2)
            => (+sourceM0x0,
                +sourceM0x1,
                +sourceM0x2,
                +sourceM1x0,
                +sourceM1x1,
                +sourceM1x2,
                +sourceM2x0,
                +sourceM2x1,
                +sourceM2x2);

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
            => (+sourceM0x0,
                +sourceM0x1,
                +sourceM0x2,
                +sourceM0x3,
                +sourceM1x0,
                +sourceM1x1,
                +sourceM1x2,
                +sourceM1x3,
                +sourceM2x0,
                +sourceM2x1,
                +sourceM2x2,
                +sourceM2x3,
                +sourceM3x0,
                +sourceM3x1,
                +sourceM3x2,
                +sourceM3x3);
        #endregion Unary Negate

        #region Unary Negate
        /// <summary>
        ///	Negates a <see cref="Matrix2x2D"/>.
        /// </summary>
        /// <param name="sourceM0x0"></param>
        /// <param name="sourceM0x1"></param>
        /// <param name="sourceM1x0"></param>
        /// <param name="sourceM1x1"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1,
            double m1x0, double m1x1
            ) Negate(
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
        public static (
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2
            ) Negate(
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
        #endregion Unary Negate

        #region Subtract
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
        public static (
            double m0x0, double m0x1,
            double m1x0, double m1x1
            ) Subtract2x2x2x2(
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
        #endregion

        #region Scale
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
            => (m0x0 * scalar,
                m0x1 * scalar,
                m1x0 * scalar,
                m1x1 * scalar);

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
            => (m0x0 * scalar,
                m0x1 * scalar,
                m0x2 * scalar,
                m1x0 * scalar,
                m1x1 * scalar,
                m1x2 * scalar,
                m2x0 * scalar,
                m2x1 * scalar,
                m2x2 * scalar);

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
            => (leftM0x0 * scalar,
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
        #endregion Scale

        #region Multiply
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
            => ((leftM0x0 * rightM0x0) + (leftM0x1 * rightM1x0),
            (leftM0x0 * rightM0x1) + (leftM0x1 * rightM1x1),
            (leftM1x0 * rightM0x0) + (leftM1x1 * rightM1x0),
            (leftM1x0 * rightM0x1) + (leftM1x1 * rightM1x1));

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
            => ((leftM0x0 * rightM0x0) + (leftM0x1 * rightM1x0),
                (leftM0x0 * rightM0x1) + (leftM0x1 * rightM1x1),
                leftM0x2,
                (leftM1x0 * rightM0x0) + (leftM1x1 * rightM1x0),
                (leftM1x0 * rightM0x1) + (leftM1x1 * rightM1x1),
                leftM1x2,
                (leftM2x0 * rightM0x0) + (leftM2x1 * rightM1x0),
                (leftM2x0 * rightM0x1) + (leftM2x1 * rightM1x1),
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
            => ((leftM0x0 * rightM0x0) + (leftM0x1 * rightM1x0),
                (leftM0x0 * rightM0x1) + (leftM0x1 * rightM1x1),
                (leftM0x0 * rightM0x2) + (leftM0x1 * rightM1x2),
                (leftM1x0 * rightM0x0) + (leftM1x1 * rightM1x0),
                (leftM1x0 * rightM0x1) + (leftM1x1 * rightM1x1),
                (leftM1x0 * rightM0x2) + (leftM1x1 * rightM1x2),
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
            => ((leftM0x0 * rightM0x0) + (leftM0x1 * rightM1x0) + (leftM0x2 * rightM2x0),
                (leftM0x0 * rightM0x1) + (leftM0x1 * rightM1x1) + (leftM0x2 * rightM2x1),
                (leftM0x0 * rightM0x2) + (leftM0x1 * rightM1x2) + (leftM0x2 * rightM2x2),
                (leftM1x0 * rightM0x0) + (leftM1x1 * rightM1x0) + (leftM1x2 * rightM2x0),
                (leftM1x0 * rightM0x1) + (leftM1x1 * rightM1x1) + (leftM1x2 * rightM2x1),
                (leftM1x0 * rightM0x2) + (leftM1x1 * rightM1x2) + (leftM1x2 * rightM2x2),
                (leftM2x0 * rightM0x0) + (leftM2x1 * rightM1x0) + (leftM2x2 * rightM2x0),
                (leftM2x0 * rightM0x1) + (leftM2x1 * rightM1x1) + (leftM2x2 * rightM2x1),
                (leftM2x0 * rightM0x2) + (leftM2x1 * rightM1x2) + (leftM2x2 * rightM2x2));

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
            => ((leftM0x0 * rightM0x0) + (leftM0x1 * rightM1x0),
                (leftM0x0 * rightM0x1) + (leftM0x1 * rightM1x1),
                (leftM0x0 * rightM0x2) + (leftM0x1 * rightM1x2),
                (leftM0x0 * rightM0x3) + (leftM0x1 * rightM1x3),
                (leftM1x0 * rightM0x0) + (leftM1x1 * rightM1x0),
                (leftM1x0 * rightM0x1) + (leftM1x1 * rightM1x1),
                (leftM1x0 * rightM0x2) + (leftM1x1 * rightM1x2),
                (leftM1x0 * rightM0x3) + (leftM1x1 * rightM1x3),
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
            => ((leftM0x0 * rightM0x0) + (leftM0x1 * rightM1x0) + (leftM0x2 * rightM2x0),
                (leftM0x0 * rightM0x1) + (leftM0x1 * rightM1x1) + (leftM0x2 * rightM2x1),
                (leftM0x0 * rightM0x2) + (leftM0x1 * rightM1x2) + (leftM0x2 * rightM2x2),
                (leftM0x0 * rightM0x3) + (leftM0x1 * rightM1x3) + (leftM0x2 * rightM2x3),
                (leftM1x0 * rightM0x0) + (leftM1x1 * rightM1x0) + (leftM1x2 * rightM2x0),
                (leftM1x0 * rightM0x1) + (leftM1x1 * rightM1x1) + (leftM1x2 * rightM2x1),
                (leftM1x0 * rightM0x2) + (leftM1x1 * rightM1x2) + (leftM1x2 * rightM2x2),
                (leftM1x0 * rightM0x3) + (leftM1x1 * rightM1x3) + (leftM1x2 * rightM2x3),
                (leftM2x0 * rightM0x0) + (leftM2x1 * rightM1x0) + (leftM2x2 * rightM2x0),
                (leftM2x0 * rightM0x1) + (leftM2x1 * rightM1x1) + (leftM2x2 * rightM2x1),
                (leftM2x0 * rightM0x2) + (leftM2x1 * rightM1x2) + (leftM2x2 * rightM2x2),
                (leftM2x0 * rightM0x3) + (leftM2x1 * rightM1x3) + (leftM2x2 * rightM2x3),
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
            => ((leftM0x0 * rightM0x0) + (leftM0x1 * rightM1x0),
                (leftM0x0 * rightM0x1) + (leftM0x1 * rightM1x1),
                leftM0x2,
                leftM0x3,
                (leftM1x0 * rightM0x0) + (leftM1x1 * rightM1x0),
                (leftM1x0 * rightM0x1) + (leftM1x1 * rightM1x1),
                leftM1x2,
                leftM1x3,
                (leftM2x0 * rightM0x0) + (leftM2x1 * rightM1x0),
                (leftM2x0 * rightM0x1) + (leftM2x1 * rightM1x1),
                leftM2x2,
                leftM2x3,
                (leftM3x0 * rightM0x0) + (leftM3x1 * rightM1x0),
                (leftM3x0 * rightM0x1) + (leftM3x1 * rightM1x1),
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
                (leftM0x0 * rightM0x0) + (leftM0x1 * rightM1x0) + (leftM0x2 * rightM2x0),
                (leftM0x0 * rightM0x1) + (leftM0x1 * rightM1x1) + (leftM0x2 * rightM2x1),
                (leftM0x0 * rightM0x2) + (leftM0x1 * rightM1x2) + (leftM0x2 * rightM2x2),
                leftM0x3,
                (leftM1x0 * rightM0x0) + (leftM1x1 * rightM1x0) + (leftM1x2 * rightM2x0),
                (leftM1x0 * rightM0x1) + (leftM1x1 * rightM1x1) + (leftM1x2 * rightM2x1),
                (leftM1x0 * rightM0x2) + (leftM1x1 * rightM1x2) + (leftM1x2 * rightM2x2),
                leftM1x3,
                (leftM2x0 * rightM0x0) + (leftM2x1 * rightM1x0) + (leftM2x2 * rightM2x0),
                (leftM2x0 * rightM0x1) + (leftM2x1 * rightM1x1) + (leftM2x2 * rightM2x1),
                (leftM2x0 * rightM0x2) + (leftM2x1 * rightM1x2) + (leftM2x2 * rightM2x2),
                leftM2x3,
                (leftM3x0 * rightM0x0) + (leftM3x1 * rightM1x0) + (leftM3x2 * rightM2x0),
                (leftM3x0 * rightM0x1) + (leftM3x1 * rightM1x1) + (leftM3x2 * rightM2x1),
                (leftM3x0 * rightM0x2) + (leftM3x1 * rightM1x2) + (leftM3x2 * rightM2x2),
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
            => ((leftM0x0 * rightM0x0) + (leftM0x1 * rightM1x0) + (leftM0x2 * rightM2x0) + (leftM0x3 * rightM3x0),
                (leftM0x0 * rightM0x1) + (leftM0x1 * rightM1x1) + (leftM0x2 * rightM2x1) + (leftM0x3 * rightM3x1),
                (leftM0x0 * rightM0x2) + (leftM0x1 * rightM1x2) + (leftM0x2 * rightM2x2) + (leftM0x3 * rightM3x2),
                (leftM0x0 * rightM0x3) + (leftM0x1 * rightM1x3) + (leftM0x2 * rightM2x3) + (leftM0x3 * rightM3x3),
                (leftM1x0 * rightM0x0) + (leftM1x1 * rightM1x0) + (leftM1x2 * rightM2x0) + (leftM1x3 * rightM3x0),
                (leftM1x0 * rightM0x1) + (leftM1x1 * rightM1x1) + (leftM1x2 * rightM2x1) + (leftM1x3 * rightM3x1),
                (leftM1x0 * rightM0x2) + (leftM1x1 * rightM1x2) + (leftM1x2 * rightM2x2) + (leftM1x3 * rightM3x2),
                (leftM1x0 * rightM0x3) + (leftM1x1 * rightM1x3) + (leftM1x2 * rightM2x3) + (leftM1x3 * rightM3x3),
                (leftM2x0 * rightM0x0) + (leftM2x1 * rightM1x0) + (leftM2x2 * rightM2x0) + (leftM2x3 * rightM3x0),
                (leftM2x0 * rightM0x1) + (leftM2x1 * rightM1x1) + (leftM2x2 * rightM2x1) + (leftM2x3 * rightM3x1),
                (leftM2x0 * rightM0x2) + (leftM2x1 * rightM1x2) + (leftM2x2 * rightM2x2) + (leftM2x3 * rightM3x2),
                (leftM2x0 * rightM0x3) + (leftM2x1 * rightM1x3) + (leftM2x2 * rightM2x3) + (leftM2x3 * rightM3x3),
                (leftM3x0 * rightM0x0) + (leftM3x1 * rightM1x0) + (leftM3x2 * rightM2x0) + (leftM3x3 * rightM3x0),
                (leftM3x0 * rightM0x1) + (leftM3x1 * rightM1x1) + (leftM3x2 * rightM2x1) + (leftM3x3 * rightM3x1),
                (leftM3x0 * rightM0x2) + (leftM3x1 * rightM1x2) + (leftM3x2 * rightM2x2) + (leftM3x3 * rightM3x2),
                (leftM3x0 * rightM0x3) + (leftM3x1 * rightM1x3) + (leftM3x2 * rightM2x3) + (leftM3x3 * rightM3x3));
        #endregion Multiply

        #region Is Identity
        /// <summary>
        /// Tests whether or not a given transform is an identity transform matrix.
        /// </summary>
        public static bool IsIdentity(
            double m0x0, double m0x1,
            double m1x0, double m1x1)
            => Math.Abs(m0x0 - 1) < double.Epsilon && Math.Abs(m0x1) < double.Epsilon && Math.Abs(m1x0) < double.Epsilon && Math.Abs(m1x1 - 1) < double.Epsilon;
        #endregion Is Identity

        #region Adjoint
        /// <summary>
        /// The adjoint.
        /// </summary>
        /// <returns>The <see cref="Matrix2x2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1,
            double m1x0, double m1x1
            ) Adjoint(
            double m0x0, double m0x1,
            double m1x0, double m1x1)
            => (
                m1x1, -m0x1,
                -m1x0, m0x0);

        /// <summary>
        /// The adjoint.
        /// </summary>
        /// <returns>The <see cref="Matrix3x3D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2
            ) Adjoint(
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
        /// <returns>The adjoint matrix of the current instance.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3,
            double m1x0, double m1x1, double m1x2, double m1x3,
            double m2x0, double m2x1, double m2x2, double m2x3,
            double m3x0, double m3x1, double m3x2, double m3x3
            ) Adjoint(
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
        #endregion Adjoint

        #region Cofactor
        /// <summary>
        /// The cofactor.
        /// </summary>
        /// <returns>The <see cref="Matrix2x2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1,
            double m1x0, double m1x1
            ) Cofactor(
            double m0x0, double m0x1,
            double m1x0, double m1x1)
            => (-m1x1, m0x1,
                m1x0, -m0x0);

        /// <summary>
        /// The cofactor.
        /// </summary>
        /// <returns>The <see cref="Matrix3x3D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2
            ) Cofactor(
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2)
            => (-((m1x1 * m2x2) - (m1x2 * m2x1)),
                (m0x1 * m2x2) - (m0x2 * m2x1),
                -((m0x1 * m1x2) - (m0x2 * m1x1)),
                (m1x0 * m2x2) - (m1x2 * m2x0),
                -((m0x0 * m2x2) - (m0x2 * m2x0)),
                (m0x0 * m1x2) - (m0x2 * m1x0),
                -((m1x0 * m2x1) - (m1x1 * m2x0)),
                (m0x0 * m2x1) - (m0x1 * m2x0),
                -((m0x0 * m1x1) - (m0x1 * m1x0)));

        /// <summary>
        /// The cofactor.
        /// </summary>
        /// <returns>The <see cref="Matrix4x4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3,
            double m1x0, double m1x1, double m1x2, double m1x3,
            double m2x0, double m2x1, double m2x2, double m2x3,
            double m3x0, double m3x1, double m3x2, double m3x3
            ) Cofactor(
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
        #endregion Cofactor

        #region Invert
        /// <summary>
        /// The invert.
        /// </summary>
        /// <returns>The <see cref="Matrix2x2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1,
            double m1x0, double m1x1
            ) Invert(
            double m0x0, double m0x1,
            double m1x0, double m1x1)
        {
            var m11 = m1x1;
            var detInv = 1 / ((m0x0 * m11) - (m0x1 * m1x0));
            return (
                detInv * m11,
                detInv * -m0x1,
                detInv * -m1x0,
                detInv * m0x0);
        }

        /// <summary>
        /// The invert.
        /// </summary>
        /// <returns>The <see cref="Matrix3x3D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2
            ) Invert(
            double m0x0, double m0x1, double m0x2,
            double m1x0, double m1x1, double m1x2,
            double m2x0, double m2x1, double m2x2)
        {
            var m11m22m12m21 = (m1x1 * m2x2) - (m1x2 * m2x1);
            var m10m22m12m20 = (m1x0 * m2x2) - (m1x2 * m2x0);
            var m10m21m11m20 = (m1x0 * m2x1) - (m1x1 * m2x0);
            var detInv = 1 / ((m0x0 * m11m22m12m21) - (m0x1 * m10m22m12m20) + (m0x2 * m10m21m11m20));
            return (
                detInv * m11m22m12m21,
                detInv * (-((m0x1 * m2x2) - (m0x2 * m2x1))),
                detInv * ((m0x1 * m1x2) - (m0x2 * m1x1)),
                detInv * (-m10m22m12m20),
                detInv * ((m0x0 * m2x2) - (m0x2 * m2x0)),
                detInv * (-((m0x0 * m1x2) - (m0x2 * m1x0))),
                detInv * m10m21m11m20,
                detInv * (-((m0x0 * m2x1) - (m0x1 * m2x0))),
                detInv * ((m0x0 * m1x1) - (m0x1 * m1x0)));
        }

        /// <summary>
        /// The invert.
        /// </summary>
        /// <returns>The <see cref="Matrix4x4D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m0x0, double m0x1, double m0x2, double m0x3,
            double m1x0, double m1x1, double m1x2, double m1x3,
            double m2x0, double m2x1, double m2x2, double m2x3,
            double m3x0, double m3x1, double m3x2, double m3x3
            ) Invert(
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

            var detInv = 1 /
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
        #endregion Invert

        #region Transpose
        /// <summary>
        /// Swap the rows of the matrix with the columns.
        /// </summary>
        /// <returns>A transposed Matrix.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double a, double b,
            double c, double d)
            Transpose(
            double a, double b,
            double c, double d)
            => (
                a, c,
                b, d
                );

        /// <summary>
        /// Swap the rows of the matrix with the columns.
        /// </summary>
        /// <returns>A transposed Matrix.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double a, double b, double c,
            double d, double e, double f,
            double g, double h, double i
            ) Transpose(
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
        /// <returns>A transposed Matrix.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double a, double b, double c, double d,
            double e, double f, double g, double h,
            double i, double j, double k, double l,
            double m, double n, double o, double p
            ) Transpose(
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
        /// <returns>A transposed Matrix.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double a, double b, double c, double d, double e,
            double f, double g, double h, double i, double j,
            double k, double l, double m, double n, double o,
            double p, double q, double r, double s, double t,
            double u, double v, double w, double x, double y
            ) Transpose(
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
        /// <returns>A transposed Matrix.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double a, double b, double c, double d, double e, double f,
            double g, double h, double i, double j, double k, double l,
            double m, double n, double o, double p, double q, double r,
            double s, double t, double u, double v, double w, double x,
            double y, double z, double aa, double bb, double cc, double dd,
            double ee, double ff, double gg, double hh, double ii, double jj
            ) Transpose(
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
        /// <param name="m1x1"></param>
        /// <param name="m1x2"></param>
        /// <param name="m2x1"></param>
        /// <param name="m2x2"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/onlyuser/Legacy/blob/master/msvb/Dex3d/Math.bas
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Determinant(
            double m1x1, double m1x2,
            double m2x1, double m2x2)
            => (m1x1 * m2x2)
              - (m1x2 * m2x1);

        /// <summary>
        /// Find the determinant of a 3 by 3 matrix.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <param name="h"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/onlyuser/Legacy/blob/master/msvb/Dex3d/Math.bas
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Determinant(
            double a, double b, double c,
            double d, double e, double f,
            double g, double h, double i)
            => (a * Determinant(e, f, h, i))
              - (b * Determinant(d, f, g, i))
              + (c * Determinant(d, e, g, h));

        /// <summary>
        /// Find the determinant of a 4 by 4 matrix.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <param name="h"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <param name="l"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="o"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/onlyuser/Legacy/blob/master/msvb/Dex3d/Math.bas
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Determinant(
            double a, double b, double c, double d,
            double e, double f, double g, double h,
            double i, double j, double k, double l,
            double m, double n, double o, double p)
            => (a * Determinant(f, g, h, j, k, l, n, o, p))
              - (b * Determinant(e, g, h, i, k, l, m, o, p))
              + (c * Determinant(e, f, h, i, j, l, m, n, p))
              - (d * Determinant(e, f, g, i, j, k, m, n, o))
            ;

        /// <summary>
        /// Find the determinant of a 5 by 5 matrix.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <param name="h"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <param name="l"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="o"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="r"></param>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="w"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Determinant(
            double a, double b, double c, double d, double e,
            double f, double g, double h, double i, double j,
            double k, double l, double m, double n, double o,
            double p, double q, double r, double s, double t,
            double u, double v, double w, double x, double y)
            => (a * Determinant(g, h, i, j, l, m, n, o, q, r, s, t, v, w, x, y))
              - (b * Determinant(f, h, i, j, k, m, n, o, p, r, s, t, u, w, x, y))
              + (c * Determinant(f, g, i, j, k, l, n, o, p, q, s, t, u, v, x, y))
              - (d * Determinant(f, g, h, j, k, l, m, o, p, q, r, t, u, v, w, y))
              + (e * Determinant(f, g, h, i, k, l, m, n, p, q, r, s, u, v, w, x))
            ;

        /// <summary>
        /// Find the determinant of a 6 by 6 matrix.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <param name="h"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <param name="l"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="o"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="r"></param>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="w"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="aa"></param>
        /// <param name="bb"></param>
        /// <param name="cc"></param>
        /// <param name="dd"></param>
        /// <param name="ee"></param>
        /// <param name="ff"></param>
        /// <param name="gg"></param>
        /// <param name="hh"></param>
        /// <param name="ii"></param>
        /// <param name="jj"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Determinant(
            double a, double b, double c, double d, double e, double f,
            double g, double h, double i, double j, double k, double l,
            double m, double n, double o, double p, double q, double r,
            double s, double t, double u, double v, double w, double x,
            double y, double z, double aa, double bb, double cc, double dd,
            double ee, double ff, double gg, double hh, double ii, double jj)
            => (a * Determinant(h, i, j, k, l, n, o, p, q, r, t, u, v, w, x, z, aa, bb, cc, dd, ff, gg, hh, ii, jj))
              - (b * Determinant(g, i, j, k, l, m, o, p, q, r, s, u, v, w, x, y, aa, bb, cc, dd, ee, gg, hh, ii, jj))
              + (c * Determinant(g, h, j, k, l, m, n, p, q, r, s, t, v, w, x, y, z, bb, cc, dd, ee, ff, hh, ii, jj))
              - (d * Determinant(g, h, i, k, l, m, n, o, q, r, s, t, u, w, x, y, z, aa, cc, dd, ee, ff, gg, ii, jj))
              + (e * Determinant(g, h, i, j, l, m, n, o, p, r, s, t, u, v, x, y, z, aa, bb, dd, ee, ff, gg, hh, jj))
              - (f * Determinant(g, h, i, j, k, m, n, o, p, q, s, t, u, v, w, y, z, aa, bb, cc, ee, ff, gg, hh, ii))
            ;
        #endregion Determinant

        #region Inverse Determinant
        /// <summary>
        /// Find the inverse of the determinant of a 2 by 2 matrix.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseDeterminant(
            double a, double b,
            double c, double d)
            => 1d / ((a * d)
              - (b * c)
            );

        /// <summary>
        /// Find the inverse of the determinant of a 3 by 3 matrix.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <param name="h"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/onlyuser/Legacy/blob/master/msvb/Dex3d/Math.bas
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseDeterminant(
            double a, double b, double c,
            double d, double e, double f,
            double g, double h, double i)
            => 1d / ((a * Determinant(e, f, h, i))
              - (b * Determinant(d, f, g, i))
              + (c * Determinant(d, e, g, h))
            );

        /// <summary>
        /// Find the inverse of the determinant of a 4 by 4 matrix.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <param name="h"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <param name="l"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="o"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/onlyuser/Legacy/blob/master/msvb/Dex3d/Math.bas
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseDeterminant(
            double a, double b, double c, double d,
            double e, double f, double g, double h,
            double i, double j, double k, double l,
            double m, double n, double o, double p)
            => 1d / ((a * Determinant(f, g, h, j, k, l, n, o, p))
              - (b * Determinant(e, g, h, i, k, l, m, o, p))
              + (c * Determinant(e, f, h, i, j, l, m, n, p))
              - (d * Determinant(e, f, g, i, j, k, m, n, o))
            );

        /// <summary>
        /// Find the inverse of the determinant of a 5 by 5 matrix.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <param name="h"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <param name="l"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="o"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="r"></param>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="w"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseDeterminant(
            double a, double b, double c, double d, double e,
            double f, double g, double h, double i, double j,
            double k, double l, double m, double n, double o,
            double p, double q, double r, double s, double t,
            double u, double v, double w, double x, double y)
            => 1d / ((a * Determinant(g, h, i, j, l, m, n, o, q, r, s, t, v, w, x, y))
              - (b * Determinant(f, h, i, j, k, m, n, o, p, r, s, t, u, w, x, y))
              + (c * Determinant(f, g, i, j, k, l, n, o, p, q, s, t, u, v, x, y))
              - (d * Determinant(f, g, h, j, k, l, m, o, p, q, r, t, u, v, w, y))
              + (e * Determinant(f, g, h, i, k, l, m, n, p, q, r, s, u, v, w, x))
            );

        /// <summary>
        /// Find the inverse of the determinant of a 6 by 6 matrix.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <param name="h"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <param name="l"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="o"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="r"></param>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="w"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="aa"></param>
        /// <param name="bb"></param>
        /// <param name="cc"></param>
        /// <param name="dd"></param>
        /// <param name="ee"></param>
        /// <param name="ff"></param>
        /// <param name="gg"></param>
        /// <param name="hh"></param>
        /// <param name="ii"></param>
        /// <param name="jj"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseDeterminant(
            double a, double b, double c, double d, double e, double f,
            double g, double h, double i, double j, double k, double l,
            double m, double n, double o, double p, double q, double r,
            double s, double t, double u, double v, double w, double x,
            double y, double z, double aa, double bb, double cc, double dd,
            double ee, double ff, double gg, double hh, double ii, double jj)
            => 1d / ((a * Determinant(h, i, j, k, l, n, o, p, q, r, t, u, v, w, x, z, aa, bb, cc, dd, ff, gg, hh, ii, jj))
              - (b * Determinant(g, i, j, k, l, m, o, p, q, r, s, u, v, w, x, y, aa, bb, cc, dd, ee, gg, hh, ii, jj))
              + (c * Determinant(g, h, j, k, l, m, n, p, q, r, s, t, v, w, x, y, z, bb, cc, dd, ee, ff, hh, ii, jj))
              - (d * Determinant(g, h, i, k, l, m, n, o, q, r, s, t, u, w, x, y, z, aa, cc, dd, ee, ff, gg, ii, jj))
              + (e * Determinant(g, h, i, j, l, m, n, o, p, r, s, t, u, v, x, y, z, aa, bb, dd, ee, ff, gg, hh, jj))
              - (f * Determinant(g, h, i, j, k, m, n, o, p, q, s, t, u, v, w, y, z, aa, bb, cc, ee, ff, gg, hh, ii))
            );
        #endregion Inverse Determinant
    }
}
