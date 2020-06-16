// <copyright file="Operations.MatriciesQueries.cs" company="Shkyrockett" >
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
        /// <summary>
        /// Check if a matrix is has the same number of rows as columns.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns>
        ///   <see langword="true" /> if [is square matrix] [the specified matrix]; otherwise, <see langword="false" />.
        /// </returns>
        /// <acknowledgment>
        /// https://github.com/SarahFrem/AutoRegressive_model_cs/blob/master/Matrix.cs#L173
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSquareMatrix(double[,] matrix)
        {
            var rows = matrix?.GetLength(0);
            var cols = matrix?.GetLength(1);
            return rows == cols;
        }

        #region Is Identity
        /// <summary>
        /// Determines whether the specified matrix is identity.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified matrix is identity; otherwise, <see langword="false"/>.
        /// </returns>
        /// <acknowledgment>
        /// https://www.tutorialgateway.org/c-program-to-check-matrix-is-an-identity-matrix/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsIdentity(double[,] matrix)
        {
            var rows = matrix?.GetLength(0);
            var cols = matrix?.GetLength(1);

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    if (matrix[i, j] != 1 && matrix[j, i] != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Tests whether or not a given transform is an identity transform matrix.
        /// </summary>
        /// <param name="m1x1">The M0X0.</param>
        /// <param name="m1x2">The M0X1.</param>
        /// <param name="m2x1">The M1X0.</param>
        /// <param name="m2x2">The M1X1.</param>
        /// <returns>
        ///   <see langword="true"/> if [is matrix identity] [the specified M0X0]; otherwise, <see langword="false"/>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMatrixIdentity(
            double m1x1, double m1x2,
            double m2x1, double m2x2)
            => m1x1 == 1d && m2x2 == 1d /* Check diagonals first to exit sooner. */
            && m1x2 == 0d && m2x1 == 0d;

        /// <summary>
        /// Tests whether or not a given transform is an identity transform matrix.
        /// </summary>
        /// <param name="m1x1">The M0X0.</param>
        /// <param name="m1x2">The M0X1.</param>
        /// <param name="m1x3">The M0X2.</param>
        /// <param name="m2x1">The M1X0.</param>
        /// <param name="m2x2">The M1X1.</param>
        /// <param name="m2x3">The M1X2.</param>
        /// <param name="m3x1">The M2X0.</param>
        /// <param name="m3x2">The M2X1.</param>
        /// <param name="m3x3">The M2X2.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified M0X0 is identity; otherwise, <see langword="false"/>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMatrixIdentity(
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3)
            => m1x1 == 1d && m2x2 == 1d && m3x3 == 1d /* Check diagonals first to exit sooner. */
            && m1x2 == 0d && m1x3 == 0d && m2x1 == 0d
            && m2x3 == 0d && m3x1 == 0d && m3x2 == 0d;

        /// <summary>
        /// Tests whether or not a given transform is an identity transform matrix.
        /// </summary>
        /// <param name="m1x1">The M0X0.</param>
        /// <param name="m1x2">The M0X1.</param>
        /// <param name="m1x3">The M0X2.</param>
        /// <param name="m1x4">The M0X3.</param>
        /// <param name="m2x1">The M1X0.</param>
        /// <param name="m2x2">The M1X1.</param>
        /// <param name="m2x3">The M1X2.</param>
        /// <param name="m2x4">The M1X3.</param>
        /// <param name="m3x1">The M2X0.</param>
        /// <param name="m3x2">The M2X1.</param>
        /// <param name="m3x3">The M2X2.</param>
        /// <param name="m3x4">The M2X3.</param>
        /// <param name="m4x1">The M3X0.</param>
        /// <param name="m4x2">The M3X1.</param>
        /// <param name="m4x3">The M3X2.</param>
        /// <param name="m4x4">The M3X3.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified M0X0 is identity; otherwise, <see langword="false"/>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMatrixIdentity(
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4)
            => m1x1 == 1d && m2x2 == 1d && m3x3 == 1d && m4x4 == 1d /* Check diagonals first to exit sooner. */
            && m1x2 == 0d && m1x3 == 0d && m1x4 == 0d && m2x1 == 0d
            && m2x3 == 0d && m2x4 == 0d && m3x1 == 0d && m3x2 == 0d
            && m3x4 == 0d && m4x1 == 0d && m4x2 == 0d && m4x3 == 0d;

        /// <summary>
        /// Tests whether or not a given transform is an identity transform matrix.
        /// </summary>
        /// <param name="m1x1">The M0X0.</param>
        /// <param name="m1x2">The M0X1.</param>
        /// <param name="m1x3">The M0X2.</param>
        /// <param name="m1x4">The M0X3.</param>
        /// <param name="m1x5">The M0X4.</param>
        /// <param name="m2x1">The M1X0.</param>
        /// <param name="m2x2">The M1X1.</param>
        /// <param name="m2x3">The M1X2.</param>
        /// <param name="m2x4">The M1X3.</param>
        /// <param name="m2x5">The M1X4.</param>
        /// <param name="m3x1">The M2X0.</param>
        /// <param name="m3x2">The M2X1.</param>
        /// <param name="m3x3">The M2X2.</param>
        /// <param name="m3x4">The M2X3.</param>
        /// <param name="m3x5">The M2X4.</param>
        /// <param name="m4x1">The M3X0.</param>
        /// <param name="m4x2">The M3X1.</param>
        /// <param name="m4x3">The M3X2.</param>
        /// <param name="m4x4">The M3X3.</param>
        /// <param name="m4x5">The M3X4.</param>
        /// <param name="m5x1">The M4X0.</param>
        /// <param name="m5x2">The M4X1.</param>
        /// <param name="m5x3">The M4X2.</param>
        /// <param name="m5x4">The M4X3.</param>
        /// <param name="m5x5">The M4X4.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified M0X0 is identity; otherwise, <see langword="false"/>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMatrixIdentity(
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5)
            => m1x1 == 1d && m2x2 == 1d && m3x3 == 1d && m4x4 == 1d && m5x5 == 1d /* Check diagonals first to exit sooner. */
            && m1x2 == 0d && m1x3 == 0d && m1x4 == 0d && m1x5 == 0d && m2x1 == 0d
            && m2x3 == 0d && m2x4 == 0d && m2x5 == 0d && m3x1 == 0d && m3x2 == 0d
            && m3x4 == 0d && m3x5 == 0d && m4x1 == 0d && m4x2 == 0d && m4x3 == 0d
            && m4x5 == 0d && m5x1 == 0d && m5x2 == 0d && m5x3 == 0d && m5x4 == 0d;

        /// <summary>
        /// Tests whether or not a given transform is an identity transform matrix.
        /// </summary>
        /// <param name="m1x1">The M0X0.</param>
        /// <param name="m1x2">The M0X1.</param>
        /// <param name="m1x3">The M0X2.</param>
        /// <param name="m1x4">The M0X3.</param>
        /// <param name="m1x5">The M0X4.</param>
        /// <param name="m1x6">The M0X5.</param>
        /// <param name="m2x1">The M1X0.</param>
        /// <param name="m2x2">The M1X1.</param>
        /// <param name="m2x3">The M1X2.</param>
        /// <param name="m2x4">The M1X3.</param>
        /// <param name="m2x5">The M1X4.</param>
        /// <param name="m2x6">The M1X5.</param>
        /// <param name="m3x1">The M2X0.</param>
        /// <param name="m3x2">The M2X1.</param>
        /// <param name="m3x3">The M2X2.</param>
        /// <param name="m3x4">The M2X3.</param>
        /// <param name="m3x5">The M2X4.</param>
        /// <param name="m3x6">The M2X5.</param>
        /// <param name="m4x1">The M3X0.</param>
        /// <param name="m4x2">The M3X1.</param>
        /// <param name="m4x3">The M3X2.</param>
        /// <param name="m4x4">The M3X3.</param>
        /// <param name="m4x5">The M3X4.</param>
        /// <param name="m4x6">The M3X5.</param>
        /// <param name="m5x1">The M4X0.</param>
        /// <param name="m5x2">The M4X1.</param>
        /// <param name="m5x3">The M4X2.</param>
        /// <param name="m5x4">The M4X3.</param>
        /// <param name="m5x5">The M4X4.</param>
        /// <param name="m5x6">The M4X5.</param>
        /// <param name="m6x1">The M5X0.</param>
        /// <param name="m6x2">The M5X1.</param>
        /// <param name="m6x3">The M5X2.</param>
        /// <param name="m6x4">The M5X3.</param>
        /// <param name="m6x5">The M5X4.</param>
        /// <param name="m6x6">The M5X5.</param>
        /// <returns>
        ///   <see langword="true"/> if [is matrix identity] [the specified M0X0]; otherwise, <see langword="false"/>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMatrixIdentity(
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6)
            => m1x1 == 1d && m2x2 == 1d && m3x3 == 1d && m4x4 == 1d && m5x5 == 1d && m6x6 == 1d /* Check diagonals first to exit sooner. */
            && m1x2 == 0d && m1x3 == 0d && m1x4 == 0d && m1x5 == 0d && m1x6 == 0d && m2x1 == 0d
            && m2x3 == 0d && m2x4 == 0d && m2x5 == 0d && m2x6 == 0d && m3x1 == 0d && m3x2 == 0d
            && m3x4 == 0d && m3x5 == 0d && m3x6 == 0d && m4x1 == 0d && m4x2 == 0d && m4x3 == 0d
            && m4x5 == 0d && m4x6 == 0d && m5x1 == 0d && m5x2 == 0d && m5x3 == 0d && m5x4 == 0d
            && m5x6 == 0d && m6x1 == 0d && m6x2 == 0d && m6x3 == 0d && m6x4 == 0d && m6x5 == 0d;
        #endregion Is Identity

        #region Is Identity With Epsilon
        /// <summary>
        /// Tests whether or not a given transform is an identity transform matrix.
        /// </summary>
        /// <param name="m1x1">The M0X0.</param>
        /// <param name="m1x2">The M0X1.</param>
        /// <param name="m2x1">The M1X0.</param>
        /// <param name="m2x2">The M1X1.</param>
        /// <returns>
        ///   <see langword="true"/> if [is matrix identity] [the specified M0X0]; otherwise, <see langword="false"/>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMatrixIdentityEpsilon(
            double m1x1, double m1x2,
            double m2x1, double m2x2)
            => Math.Abs(m1x1 - 1d) < double.Epsilon && Math.Abs(m2x2 - 1d) < double.Epsilon /* Check diagonals first to exit sooner. */
            && Math.Abs(m1x2) < double.Epsilon && Math.Abs(m2x1) < double.Epsilon;

        /// <summary>
        /// Tests whether or not a given transform is an identity transform matrix.
        /// </summary>
        /// <param name="m1x1">The M0X0.</param>
        /// <param name="m1x2">The M0X1.</param>
        /// <param name="m1x3">The M0X2.</param>
        /// <param name="m2x1">The M1X0.</param>
        /// <param name="m2x2">The M1X1.</param>
        /// <param name="m2x3">The M1X2.</param>
        /// <param name="m3x1">The M2X0.</param>
        /// <param name="m3x2">The M2X1.</param>
        /// <param name="m3x3">The M2X2.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified M0X0 is identity; otherwise, <see langword="false"/>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMatrixIdentityEpsilon(
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3)
            => Math.Abs(m1x1 - 1d) < double.Epsilon && Math.Abs(m2x2 - 1d) < double.Epsilon && Math.Abs(m3x3 - 1d) < double.Epsilon /* Check diagonals first to exit sooner. */
            && Math.Abs(m1x2) < double.Epsilon && Math.Abs(m1x3) < double.Epsilon && Math.Abs(m2x1) < double.Epsilon
            && Math.Abs(m2x3) < double.Epsilon && Math.Abs(m3x1) < double.Epsilon && Math.Abs(m3x2) < double.Epsilon;

        /// <summary>
        /// Tests whether or not a given transform is an identity transform matrix.
        /// </summary>
        /// <param name="m1x1">The M0X0.</param>
        /// <param name="m1x2">The M0X1.</param>
        /// <param name="m1x3">The M0X2.</param>
        /// <param name="m1x4">The M0X3.</param>
        /// <param name="m2x1">The M1X0.</param>
        /// <param name="m2x2">The M1X1.</param>
        /// <param name="m2x3">The M1X2.</param>
        /// <param name="m2x4">The M1X3.</param>
        /// <param name="m3x1">The M2X0.</param>
        /// <param name="m3x2">The M2X1.</param>
        /// <param name="m3x3">The M2X2.</param>
        /// <param name="m3x4">The M2X3.</param>
        /// <param name="m4x1">The M3X0.</param>
        /// <param name="m4x2">The M3X1.</param>
        /// <param name="m4x3">The M3X2.</param>
        /// <param name="m4x4">The M3X3.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified M0X0 is identity; otherwise, <see langword="false"/>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMatrixIdentityEpsilon(
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4)
            => Math.Abs(m1x1 - 1d) < double.Epsilon && Math.Abs(m2x2 - 1d) < double.Epsilon && Math.Abs(m3x3 - 1d) < double.Epsilon && Math.Abs(m4x4 - 1d) < double.Epsilon /* Check diagonals first to exit sooner. */
            && Math.Abs(m1x2) < double.Epsilon && Math.Abs(m1x3) < double.Epsilon && Math.Abs(m1x4) < double.Epsilon && Math.Abs(m2x1) < double.Epsilon
            && Math.Abs(m2x3) < double.Epsilon && Math.Abs(m2x4) < double.Epsilon && Math.Abs(m3x1) < double.Epsilon && Math.Abs(m3x2) < double.Epsilon
            && Math.Abs(m3x4) < double.Epsilon && Math.Abs(m4x1) < double.Epsilon && Math.Abs(m4x2) < double.Epsilon && Math.Abs(m4x3) < double.Epsilon;

        /// <summary>
        /// Tests whether or not a given transform is an identity transform matrix.
        /// </summary>
        /// <param name="m1x1">The M0X0.</param>
        /// <param name="m1x2">The M0X1.</param>
        /// <param name="m1x3">The M0X2.</param>
        /// <param name="m1x4">The M0X3.</param>
        /// <param name="m1x5">The M0X4.</param>
        /// <param name="m2x1">The M1X0.</param>
        /// <param name="m2x2">The M1X1.</param>
        /// <param name="m2x3">The M1X2.</param>
        /// <param name="m2x4">The M1X3.</param>
        /// <param name="m2x5">The M1X4.</param>
        /// <param name="m3x1">The M2X0.</param>
        /// <param name="m3x2">The M2X1.</param>
        /// <param name="m3x3">The M2X2.</param>
        /// <param name="m3x4">The M2X3.</param>
        /// <param name="m3x5">The M2X4.</param>
        /// <param name="m4x1">The M3X0.</param>
        /// <param name="m4x2">The M3X1.</param>
        /// <param name="m4x3">The M3X2.</param>
        /// <param name="m4x4">The M3X3.</param>
        /// <param name="m4x5">The M3X4.</param>
        /// <param name="m5x1">The M4X0.</param>
        /// <param name="m5x2">The M4X1.</param>
        /// <param name="m5x3">The M4X2.</param>
        /// <param name="m5x4">The M4X3.</param>
        /// <param name="m5x5">The M4X4.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified M0X0 is identity; otherwise, <see langword="false"/>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMatrixIdentityEpsilon(
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5)
            => Math.Abs(m1x1 - 1d) < double.Epsilon && Math.Abs(m2x2 - 1d) < double.Epsilon && Math.Abs(m3x3 - 1d) < double.Epsilon && Math.Abs(m4x4 - 1d) < double.Epsilon && Math.Abs(m5x5 - 1d) < double.Epsilon /* Check diagonals first to exit sooner. */
            && Math.Abs(m1x2) < double.Epsilon && Math.Abs(m1x3) < double.Epsilon && Math.Abs(m1x4) < double.Epsilon && Math.Abs(m1x5) < double.Epsilon && Math.Abs(m2x1) < double.Epsilon
            && Math.Abs(m2x3) < double.Epsilon && Math.Abs(m2x4) < double.Epsilon && Math.Abs(m2x5) < double.Epsilon && Math.Abs(m3x1) < double.Epsilon && Math.Abs(m3x2) < double.Epsilon
            && Math.Abs(m3x4) < double.Epsilon && Math.Abs(m3x5) < double.Epsilon && Math.Abs(m4x1) < double.Epsilon && Math.Abs(m4x2) < double.Epsilon && Math.Abs(m4x3) < double.Epsilon
            && Math.Abs(m4x5) < double.Epsilon && Math.Abs(m5x1) < double.Epsilon && Math.Abs(m5x2) < double.Epsilon && Math.Abs(m5x3) < double.Epsilon && Math.Abs(m5x4) < double.Epsilon;

        /// <summary>
        /// Tests whether or not a given transform is an identity transform matrix.
        /// </summary>
        /// <param name="m1x1">The M0X0.</param>
        /// <param name="m1x2">The M0X1.</param>
        /// <param name="m1x3">The M0X2.</param>
        /// <param name="m1x4">The M0X3.</param>
        /// <param name="m1x5">The M0X4.</param>
        /// <param name="m1x6">The M0X5.</param>
        /// <param name="m2x1">The M1X0.</param>
        /// <param name="m2x2">The M1X1.</param>
        /// <param name="m2x3">The M1X2.</param>
        /// <param name="m2x4">The M1X3.</param>
        /// <param name="m2x5">The M1X4.</param>
        /// <param name="m2x6">The M1X5.</param>
        /// <param name="m3x1">The M2X0.</param>
        /// <param name="m3x2">The M2X1.</param>
        /// <param name="m3x3">The M2X2.</param>
        /// <param name="m3x4">The M2X3.</param>
        /// <param name="m3x5">The M2X4.</param>
        /// <param name="m3x6">The M2X5.</param>
        /// <param name="m4x1">The M3X0.</param>
        /// <param name="m4x2">The M3X1.</param>
        /// <param name="m4x3">The M3X2.</param>
        /// <param name="m4x4">The M3X3.</param>
        /// <param name="m4x5">The M3X4.</param>
        /// <param name="m4x6">The M3X5.</param>
        /// <param name="m5x1">The M4X0.</param>
        /// <param name="m5x2">The M4X1.</param>
        /// <param name="m5x3">The M4X2.</param>
        /// <param name="m5x4">The M4X3.</param>
        /// <param name="m5x5">The M4X4.</param>
        /// <param name="m5x6">The M4X5.</param>
        /// <param name="m6x1">The M5X0.</param>
        /// <param name="m6x2">The M5X1.</param>
        /// <param name="m6x3">The M5X2.</param>
        /// <param name="m6x4">The M5X3.</param>
        /// <param name="m6x5">The M5X4.</param>
        /// <param name="m6x6">The M5X5.</param>
        /// <returns>
        ///   <see langword="true"/> if [is matrix identity] [the specified M0X0]; otherwise, <see langword="false"/>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMatrixIdentityEpsilon(
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6)
            => Math.Abs(m1x1 - 1d) < double.Epsilon && Math.Abs(m2x2 - 1d) < double.Epsilon && Math.Abs(m3x3 - 1d) < double.Epsilon && Math.Abs(m4x4 - 1d) < double.Epsilon && Math.Abs(m5x5 - 1d) < double.Epsilon && Math.Abs(m6x6 - 1d) < double.Epsilon /* Check diagonals first to exit sooner. */
            && Math.Abs(m1x2) < double.Epsilon && Math.Abs(m1x3) < double.Epsilon && Math.Abs(m1x4) < double.Epsilon && Math.Abs(m1x5) < double.Epsilon && Math.Abs(m1x6) < double.Epsilon && Math.Abs(m2x1) < double.Epsilon
            && Math.Abs(m2x3) < double.Epsilon && Math.Abs(m2x4) < double.Epsilon && Math.Abs(m2x5) < double.Epsilon && Math.Abs(m2x6) < double.Epsilon && Math.Abs(m3x1) < double.Epsilon && Math.Abs(m3x2) < double.Epsilon
            && Math.Abs(m3x4) < double.Epsilon && Math.Abs(m3x5) < double.Epsilon && Math.Abs(m3x6) < double.Epsilon && Math.Abs(m4x1) < double.Epsilon && Math.Abs(m4x2) < double.Epsilon && Math.Abs(m4x3) < double.Epsilon
            && Math.Abs(m4x5) < double.Epsilon && Math.Abs(m4x6) < double.Epsilon && Math.Abs(m5x1) < double.Epsilon && Math.Abs(m5x2) < double.Epsilon && Math.Abs(m5x3) < double.Epsilon && Math.Abs(m5x4) < double.Epsilon
            && Math.Abs(m5x6) < double.Epsilon && Math.Abs(m6x1) < double.Epsilon && Math.Abs(m6x2) < double.Epsilon && Math.Abs(m6x3) < double.Epsilon && Math.Abs(m6x4) < double.Epsilon && Math.Abs(m6x5) < double.Epsilon;
        #endregion Is Identity

        #region Is Lower Matrix
        /// <summary>
        /// Check whether a matrix is a lower matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns>
        ///   <see langword="true" /> if [is lower matrix] [the specified matrix]; otherwise, <see langword="false" />.
        /// </returns>
        /// <acknowledgment>
        /// https://github.com/SarahFrem/AutoRegressive_model_cs/blob/master/Matrix.cs#L181
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLowerMatrix(double[,] matrix)
        {
            var rows = matrix?.GetLength(0);
            var cols = matrix?.GetLength(1);

            for (var i = 0; i < rows; i++)
            {
                for (var j = i + 1; j < cols; j++)
                {
                    if (matrix[i, j] != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// </summary>
        /// <param name="m1x1">The M0X0.</param>
        /// <param name="m1x2">The M0X1.</param>
        /// <param name="m2x1">The M1X0.</param>
        /// <param name="m2x2">The M1X1.</param>
        /// <returns>
        ///   <see langword="true"/> if [is matrix identity] [the specified M0X0]; otherwise, <see langword="false"/>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLowerMatrix(
            double m1x1, double m1x2,
            double m2x1, double m2x2)
            => /*m1x2 == 0d
            &&*/ m1x1 != 0d
            && m2x1 != 0d && m2x2 != 0d;

        /// <summary>
        /// Determines whether [is lower matrix] [the specified M1X1].
        /// </summary>
        /// <param name="m1x1">The M0X0.</param>
        /// <param name="m1x2">The M0X1.</param>
        /// <param name="m1x3">The M0X2.</param>
        /// <param name="m2x1">The M1X0.</param>
        /// <param name="m2x2">The M1X1.</param>
        /// <param name="m2x3">The M1X2.</param>
        /// <param name="m3x1">The M2X0.</param>
        /// <param name="m3x2">The M2X1.</param>
        /// <param name="m3x3">The M2X2.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified M0X0 is identity; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLowerMatrix(
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3)
            => /*m1x2 == 0d && m1x3 == 0d
            && m2x3 == 0d
            &&*/ m1x1 != 0d
            && m2x1 != 0d && m2x2 != 0d
            && m3x1 != 0d && m3x2 != 0d && m3x3 != 0d;

        /// <summary>
        /// Determines whether [is lower matrix] [the specified M1X1].
        /// </summary>
        /// <param name="m1x1">The M0X0.</param>
        /// <param name="m1x2">The M0X1.</param>
        /// <param name="m1x3">The M0X2.</param>
        /// <param name="m1x4">The M0X3.</param>
        /// <param name="m2x1">The M1X0.</param>
        /// <param name="m2x2">The M1X1.</param>
        /// <param name="m2x3">The M1X2.</param>
        /// <param name="m2x4">The M1X3.</param>
        /// <param name="m3x1">The M2X0.</param>
        /// <param name="m3x2">The M2X1.</param>
        /// <param name="m3x3">The M2X2.</param>
        /// <param name="m3x4">The M2X3.</param>
        /// <param name="m4x1">The M3X0.</param>
        /// <param name="m4x2">The M3X1.</param>
        /// <param name="m4x3">The M3X2.</param>
        /// <param name="m4x4">The M3X3.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified M0X0 is identity; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLowerMatrix(
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4)
            => /*m1x2 == 0d && m1x3 == 0d && m1x4 == 0d
            && m2x3 == 0d && m2x4 == 0d
            && m3x4 == 0d
            &&*/ m1x1 != 0d
            && m2x1 != 0d && m2x2 != 0d
            && m3x1 != 0d && m3x2 != 0d && m3x3 != 0d
            && m4x1 != 0d && m4x2 != 0d && m4x3 != 0d && m4x4 != 0d;

        /// <summary>
        /// Determines whether [is lower matrix] [the specified M1X1].
        /// </summary>
        /// <param name="m1x1">The M0X0.</param>
        /// <param name="m1x2">The M0X1.</param>
        /// <param name="m1x3">The M0X2.</param>
        /// <param name="m1x4">The M0X3.</param>
        /// <param name="m1x5">The M0X4.</param>
        /// <param name="m2x1">The M1X0.</param>
        /// <param name="m2x2">The M1X1.</param>
        /// <param name="m2x3">The M1X2.</param>
        /// <param name="m2x4">The M1X3.</param>
        /// <param name="m2x5">The M1X4.</param>
        /// <param name="m3x1">The M2X0.</param>
        /// <param name="m3x2">The M2X1.</param>
        /// <param name="m3x3">The M2X2.</param>
        /// <param name="m3x4">The M2X3.</param>
        /// <param name="m3x5">The M2X4.</param>
        /// <param name="m4x1">The M3X0.</param>
        /// <param name="m4x2">The M3X1.</param>
        /// <param name="m4x3">The M3X2.</param>
        /// <param name="m4x4">The M3X3.</param>
        /// <param name="m4x5">The M3X4.</param>
        /// <param name="m5x1">The M4X0.</param>
        /// <param name="m5x2">The M4X1.</param>
        /// <param name="m5x3">The M4X2.</param>
        /// <param name="m5x4">The M4X3.</param>
        /// <param name="m5x5">The M4X4.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified M0X0 is identity; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLowerMatrix(
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5)
            => /*m1x2 == 0d && m1x3 == 0d && m1x4 == 0d && m1x5 == 0d
            && m2x3 == 0d && m2x4 == 0d && m2x5 == 0d
            && m3x4 == 0d && m3x5 == 0d
            && m4x5 == 0d
            &&*/ m1x1 != 0d
            && m2x1 != 0d && m2x2 != 0d
            && m3x1 != 0d && m3x2 != 0d && m3x3 != 0d
            && m4x1 != 0d && m4x2 != 0d && m4x3 != 0d && m4x4 != 0d
            && m5x1 != 0d && m5x2 != 0d && m5x3 != 0d && m5x4 != 0d && m5x5 != 0d;

        /// <summary>
        /// Determines whether [is lower matrix] [the specified M1X1].
        /// </summary>
        /// <param name="m1x1">The M0X0.</param>
        /// <param name="m1x2">The M0X1.</param>
        /// <param name="m1x3">The M0X2.</param>
        /// <param name="m1x4">The M0X3.</param>
        /// <param name="m1x5">The M0X4.</param>
        /// <param name="m1x6">The M0X5.</param>
        /// <param name="m2x1">The M1X0.</param>
        /// <param name="m2x2">The M1X1.</param>
        /// <param name="m2x3">The M1X2.</param>
        /// <param name="m2x4">The M1X3.</param>
        /// <param name="m2x5">The M1X4.</param>
        /// <param name="m2x6">The M1X5.</param>
        /// <param name="m3x1">The M2X0.</param>
        /// <param name="m3x2">The M2X1.</param>
        /// <param name="m3x3">The M2X2.</param>
        /// <param name="m3x4">The M2X3.</param>
        /// <param name="m3x5">The M2X4.</param>
        /// <param name="m3x6">The M2X5.</param>
        /// <param name="m4x1">The M3X0.</param>
        /// <param name="m4x2">The M3X1.</param>
        /// <param name="m4x3">The M3X2.</param>
        /// <param name="m4x4">The M3X3.</param>
        /// <param name="m4x5">The M3X4.</param>
        /// <param name="m4x6">The M3X5.</param>
        /// <param name="m5x1">The M4X0.</param>
        /// <param name="m5x2">The M4X1.</param>
        /// <param name="m5x3">The M4X2.</param>
        /// <param name="m5x4">The M4X3.</param>
        /// <param name="m5x5">The M4X4.</param>
        /// <param name="m5x6">The M4X5.</param>
        /// <param name="m6x1">The M5X0.</param>
        /// <param name="m6x2">The M5X1.</param>
        /// <param name="m6x3">The M5X2.</param>
        /// <param name="m6x4">The M5X3.</param>
        /// <param name="m6x5">The M5X4.</param>
        /// <param name="m6x6">The M5X5.</param>
        /// <returns>
        ///   <see langword="true" /> if [is matrix identity] [the specified M0X0]; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLowerMatrix(
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6)
            => /*m1x2 == 0d && m1x3 == 0d && m1x4 == 0d && m1x5 == 0d && m1x6 == 0d
            && m2x3 == 0d && m2x4 == 0d && m2x5 == 0d && m2x6 == 0d
            && m3x4 == 0d && m3x5 == 0d && m3x6 == 0d
            && m4x5 == 0d && m4x6 == 0d
            && m5x6 == 0d
            &&*/ m1x1 != 0d
            && m2x1 != 0d && m2x2 != 0d
            && m3x1 != 0d && m3x2 != 0d && m3x3 != 0d
            && m4x1 != 0d && m4x2 != 0d && m4x3 != 0d && m4x4 != 0d
            && m5x1 != 0d && m5x2 != 0d && m5x3 != 0d && m5x4 != 0d && m5x5 != 0d
            && m6x1 != 0d && m6x2 != 0d && m6x3 != 0d && m6x4 != 0d && m6x5 != 0d && m6x6 != 0d;
        #endregion

        #region Is Upper Matrix
        /// <summary>
        /// Check whether a matrix is an upper matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns>
        ///   <see langword="true" /> if [is upper matrix] [the specified matrix]; otherwise, <see langword="false" />.
        /// </returns>
        /// <acknowledgment>
        /// https://github.com/SarahFrem/AutoRegressive_model_cs/blob/master/Matrix.cs#L199
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUpperMatrix(double[,] matrix)
        {
            var rows = matrix?.GetLength(0);
            //var cols = matrix?.GetLength(1);

            for (var i = 1; i < rows; i++)
            {
                for (var j = 0; j < i; j++)
                {
                    if (matrix[i, j] != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether [is upper matrix] [the specified M1X1].
        /// </summary>
        /// <param name="m1x1">The M0X0.</param>
        /// <param name="m1x2">The M0X1.</param>
        /// <param name="m2x1">The M1X0.</param>
        /// <param name="m2x2">The M1X1.</param>
        /// <returns>
        ///   <see langword="true" /> if [is matrix identity] [the specified M0X0]; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUpperMatrix(
            double m1x1, double m1x2,
            double m2x1, double m2x2)
            => m1x2 != 0d && m2x2 != 0d
            && m1x1 != 0d
            /*&& m2x1 == 0d*/;

        /// <summary>
        /// Determines whether [is upper matrix] [the specified M1X1].
        /// </summary>
        /// <param name="m1x1">The M0X0.</param>
        /// <param name="m1x2">The M0X1.</param>
        /// <param name="m1x3">The M0X2.</param>
        /// <param name="m2x1">The M1X0.</param>
        /// <param name="m2x2">The M1X1.</param>
        /// <param name="m2x3">The M1X2.</param>
        /// <param name="m3x1">The M2X0.</param>
        /// <param name="m3x2">The M2X1.</param>
        /// <param name="m3x3">The M2X2.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified M0X0 is identity; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUpperMatrix(
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3)
            => m1x2 != 0d && m1x3 != 0d && m3x3 != 0d
            && m2x3 != 0d && m2x2 != 0d
            && m1x1 != 0d
            /*&& m2x1 == 0d
            && m3x1 == 0d && m3x2 == 0d*/;

        /// <summary>
        /// Determines whether [is upper matrix] [the specified M1X1].
        /// </summary>
        /// <param name="m1x1">The M0X0.</param>
        /// <param name="m1x2">The M0X1.</param>
        /// <param name="m1x3">The M0X2.</param>
        /// <param name="m1x4">The M0X3.</param>
        /// <param name="m2x1">The M1X0.</param>
        /// <param name="m2x2">The M1X1.</param>
        /// <param name="m2x3">The M1X2.</param>
        /// <param name="m2x4">The M1X3.</param>
        /// <param name="m3x1">The M2X0.</param>
        /// <param name="m3x2">The M2X1.</param>
        /// <param name="m3x3">The M2X2.</param>
        /// <param name="m3x4">The M2X3.</param>
        /// <param name="m4x1">The M3X0.</param>
        /// <param name="m4x2">The M3X1.</param>
        /// <param name="m4x3">The M3X2.</param>
        /// <param name="m4x4">The M3X3.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified M0X0 is identity; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUpperMatrix(
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4)
            => m1x2 != 0d && m1x3 != 0d && m1x4 != 0d && m4x4 != 0d
            && m2x3 != 0d && m2x4 != 0d && m3x3 != 0d
            && m3x4 != 0d && m2x2 != 0d
            && m1x1 != 0d
            /*&& m2x1 == 0d
            && m3x1 == 0d && m3x2 == 0d
            && m4x1 == 0d && m4x2 == 0d && m4x3 == 0d*/;

        /// <summary>
        /// Determines whether [is upper matrix] [the specified M1X1].
        /// </summary>
        /// <param name="m1x1">The M0X0.</param>
        /// <param name="m1x2">The M0X1.</param>
        /// <param name="m1x3">The M0X2.</param>
        /// <param name="m1x4">The M0X3.</param>
        /// <param name="m1x5">The M0X4.</param>
        /// <param name="m2x1">The M1X0.</param>
        /// <param name="m2x2">The M1X1.</param>
        /// <param name="m2x3">The M1X2.</param>
        /// <param name="m2x4">The M1X3.</param>
        /// <param name="m2x5">The M1X4.</param>
        /// <param name="m3x1">The M2X0.</param>
        /// <param name="m3x2">The M2X1.</param>
        /// <param name="m3x3">The M2X2.</param>
        /// <param name="m3x4">The M2X3.</param>
        /// <param name="m3x5">The M2X4.</param>
        /// <param name="m4x1">The M3X0.</param>
        /// <param name="m4x2">The M3X1.</param>
        /// <param name="m4x3">The M3X2.</param>
        /// <param name="m4x4">The M3X3.</param>
        /// <param name="m4x5">The M3X4.</param>
        /// <param name="m5x1">The M4X0.</param>
        /// <param name="m5x2">The M4X1.</param>
        /// <param name="m5x3">The M4X2.</param>
        /// <param name="m5x4">The M4X3.</param>
        /// <param name="m5x5">The M4X4.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified M0X0 is identity; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUpperMatrix(
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5)
            => m1x2 != 0d && m1x3 != 0d && m1x4 != 0d && m1x5 != 0d && m5x5 != 0d
            && m2x3 != 0d && m2x4 != 0d && m2x5 != 0d && m4x4 != 0d
            && m3x4 != 0d && m3x5 != 0d && m3x3 != 0d
            && m4x5 != 0d && m2x2 != 0d
            && m1x1 != 0d
            /*&& m2x1 == 0d
            && m3x1 == 0d && m3x2 == 0d
            && m4x1 == 0d && m4x2 == 0d && m4x3 == 0d
            && m5x1 == 0d && m5x2 == 0d && m5x3 == 0d && m5x4 == 0d*/;

        /// <summary>
        /// Determines whether [is upper matrix] [the specified M1X1].
        /// </summary>
        /// <param name="m1x1">The M0X0.</param>
        /// <param name="m1x2">The M0X1.</param>
        /// <param name="m1x3">The M0X2.</param>
        /// <param name="m1x4">The M0X3.</param>
        /// <param name="m1x5">The M0X4.</param>
        /// <param name="m1x6">The M0X5.</param>
        /// <param name="m2x1">The M1X0.</param>
        /// <param name="m2x2">The M1X1.</param>
        /// <param name="m2x3">The M1X2.</param>
        /// <param name="m2x4">The M1X3.</param>
        /// <param name="m2x5">The M1X4.</param>
        /// <param name="m2x6">The M1X5.</param>
        /// <param name="m3x1">The M2X0.</param>
        /// <param name="m3x2">The M2X1.</param>
        /// <param name="m3x3">The M2X2.</param>
        /// <param name="m3x4">The M2X3.</param>
        /// <param name="m3x5">The M2X4.</param>
        /// <param name="m3x6">The M2X5.</param>
        /// <param name="m4x1">The M3X0.</param>
        /// <param name="m4x2">The M3X1.</param>
        /// <param name="m4x3">The M3X2.</param>
        /// <param name="m4x4">The M3X3.</param>
        /// <param name="m4x5">The M3X4.</param>
        /// <param name="m4x6">The M3X5.</param>
        /// <param name="m5x1">The M4X0.</param>
        /// <param name="m5x2">The M4X1.</param>
        /// <param name="m5x3">The M4X2.</param>
        /// <param name="m5x4">The M4X3.</param>
        /// <param name="m5x5">The M4X4.</param>
        /// <param name="m5x6">The M4X5.</param>
        /// <param name="m6x1">The M5X0.</param>
        /// <param name="m6x2">The M5X1.</param>
        /// <param name="m6x3">The M5X2.</param>
        /// <param name="m6x4">The M5X3.</param>
        /// <param name="m6x5">The M5X4.</param>
        /// <param name="m6x6">The M5X5.</param>
        /// <returns>
        ///   <see langword="true" /> if [is matrix identity] [the specified M0X0]; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUpperMatrix(
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6)
            => m1x2 != 0d && m1x3 != 0d && m1x4 != 0d && m1x5 != 0d && m1x6 != 0d && m6x6 != 0d
            && m2x3 != 0d && m2x4 != 0d && m2x5 != 0d && m2x6 != 0d && m5x5 != 0d
            && m3x4 != 0d && m3x5 != 0d && m3x6 != 0d && m4x4 != 0d
            && m4x5 != 0d && m4x6 != 0d && m3x3 != 0d
            && m5x6 != 0d && m2x2 != 0d
            && m1x1 != 0d
            /*&& m2x1 == 0d
            && m3x1 == 0d && m3x2 == 0d
            && m4x1 == 0d && m4x2 == 0d && m4x3 == 0d
            && m5x1 == 0d && m5x2 == 0d && m5x3 == 0d && m5x4 == 0d
            && m6x1 == 0d && m6x2 == 0d && m6x3 == 0d && m6x4 == 0d && m6x5 == 0d*/;
        #endregion
    }
}
