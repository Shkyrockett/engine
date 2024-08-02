// <copyright file="Operations.Matricies.Queries.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Numerics;
using System.Runtime.CompilerServices;

namespace Engine;

/// <summary>
/// The Operations class.
/// </summary>
public static partial class Operations
{
    #region Is Square Matrix
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsSquareMatrix<T>(T[,] matrix)
        where T : INumber<T>
    {
        var rows = matrix?.GetLength(0);
        var cols = matrix?.GetLength(1);
        return rows == cols;
    }

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsSquareMatrix<T>(T[][] matrix)
        where T : INumber<T>
    {
        var rows = matrix?.Length;
        var cols = matrix[0]?.Length;
        return rows == cols;
    }
    #endregion

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsIdentity<T>(T[,] matrix)
        where T : INumber<T>
    {
        var rows = matrix?.GetLength(0);
        var cols = matrix?.GetLength(1);

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                if (matrix[i, j] != T.One && matrix[j, i] != T.Zero)
                {
                    return false;
                }
            }
        }

        return true;
    }

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsIdentity<T>(T[][] matrix)
        where T : INumber<T>
    {
        var rows = matrix?.Length;
        var cols = matrix[0]?.Length;

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                if (matrix[i][j] != T.One && matrix[j][i] != T.Zero)
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsMatrixIdentity<T>(
        T m1x1, T m1x2,
        T m2x1, T m2x2)
        where T : INumber<T>
        => m1x1 == T.One && m2x2 == T.One /* Check diagonals first to exit sooner. */
        && m1x2 == T.Zero && m2x1 == T.Zero;

    /// <summary>
    /// Determines whether the specified M0X0 is identity.
    /// </summary>
    /// <param name="m0x0">The M0X0.</param>
    /// <param name="m0x1">The M0X1.</param>
    /// <param name="m1x0">The M1X0.</param>
    /// <param name="m1x1">The M1X1.</param>
    /// <param name="offsetX">The offset x.</param>
    /// <param name="offsetY">The offset y.</param>
    /// <returns>
    ///   <see langword="true" /> if the specified M0X0 is identity; otherwise, <see langword="false" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsMatrixIdentity<T>(T m0x0, T m0x1, T m1x0, T m1x1, T offsetX, T offsetY)
        where T : INumber<T>
        => m0x0 == T.One && m1x1 == T.One /* Check diagonals first to exit sooner. */
        && m0x1 == T.Zero && m1x0 == T.Zero
        && offsetX == T.Zero && offsetY == T.Zero;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsMatrixIdentity<T>(
        T m1x1, T m1x2, T m1x3,
        T m2x1, T m2x2, T m2x3,
        T m3x1, T m3x2, T m3x3)
        where T : INumber<T>
        => m1x1 == T.One && m2x2 == T.One && m3x3 == T.One /* Check diagonals first to exit sooner. */
        && m1x2 == T.Zero && m1x3 == T.Zero && m2x1 == T.Zero
        && m2x3 == T.Zero && m3x1 == T.Zero && m3x2 == T.Zero;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsMatrixIdentity<T>(
        T m1x1, T m1x2, T m1x3, T m1x4,
        T m2x1, T m2x2, T m2x3, T m2x4,
        T m3x1, T m3x2, T m3x3, T m3x4,
        T m4x1, T m4x2, T m4x3, T m4x4)
        where T : INumber<T>
        => m1x1 == T.One && m2x2 == T.One && m3x3 == T.One && m4x4 == T.One /* Check diagonals first to exit sooner. */
        && m1x2 == T.Zero && m1x3 == T.Zero && m1x4 == T.Zero && m2x1 == T.Zero
        && m2x3 == T.Zero && m2x4 == T.Zero && m3x1 == T.Zero && m3x2 == T.Zero
        && m3x4 == T.Zero && m4x1 == T.Zero && m4x2 == T.Zero && m4x3 == T.Zero;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsMatrixIdentity<T>(
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5)
        where T : INumber<T>
        => m1x1 == T.One && m2x2 == T.One && m3x3 == T.One && m4x4 == T.One && m5x5 == T.One /* Check diagonals first to exit sooner. */
        && m1x2 == T.Zero && m1x3 == T.Zero && m1x4 == T.Zero && m1x5 == T.Zero && m2x1 == T.Zero
        && m2x3 == T.Zero && m2x4 == T.Zero && m2x5 == T.Zero && m3x1 == T.Zero && m3x2 == T.Zero
        && m3x4 == T.Zero && m3x5 == T.Zero && m4x1 == T.Zero && m4x2 == T.Zero && m4x3 == T.Zero
        && m4x5 == T.Zero && m5x1 == T.Zero && m5x2 == T.Zero && m5x3 == T.Zero && m5x4 == T.Zero;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsMatrixIdentity<T>(
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6)
        where T : INumber<T>
        => m1x1 == T.One && m2x2 == T.One && m3x3 == T.One && m4x4 == T.One && m5x5 == T.One && m6x6 == T.One /* Check diagonals first to exit sooner. */
        && m1x2 == T.Zero && m1x3 == T.Zero && m1x4 == T.Zero && m1x5 == T.Zero && m1x6 == T.Zero && m2x1 == T.Zero
        && m2x3 == T.Zero && m2x4 == T.Zero && m2x5 == T.Zero && m2x6 == T.Zero && m3x1 == T.Zero && m3x2 == T.Zero
        && m3x4 == T.Zero && m3x5 == T.Zero && m3x6 == T.Zero && m4x1 == T.Zero && m4x2 == T.Zero && m4x3 == T.Zero
        && m4x5 == T.Zero && m4x6 == T.Zero && m5x1 == T.Zero && m5x2 == T.Zero && m5x3 == T.Zero && m5x4 == T.Zero
        && m5x6 == T.Zero && m6x1 == T.Zero && m6x2 == T.Zero && m6x3 == T.Zero && m6x4 == T.Zero && m6x5 == T.Zero;
    #endregion Is Identity

    #region Is Identity With Epsilon
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsIdentityEpsilon<T>(T[,] matrix)
        where T : IFloatingPointIeee754<T>
    {
        var rows = matrix?.GetLength(0);
        var cols = matrix?.GetLength(1);

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                if (!(T.Abs(matrix[i, j] - T.One) < T.Epsilon) && !(T.Abs(matrix[j, i]) < T.Epsilon))
                {
                    return false;
                }
            }
        }

        return true;
    }

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsIdentityEpsilon<T>(T[][] matrix)
        where T : IFloatingPointIeee754<T>
    {
        var rows = matrix?.Length;
        var cols = matrix[0]?.Length;

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                if (!(T.Abs(matrix[i][j] - T.One) < T.Epsilon) && !(T.Abs(matrix[j][i]) < T.Epsilon))
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsMatrixIdentityEpsilon<T>(
        T m1x1, T m1x2,
        T m2x1, T m2x2)
        where T : IFloatingPointIeee754<T>
        => T.Abs(m1x1 - T.One) < T.Epsilon && T.Abs(m2x2 - T.One) < T.Epsilon /* Check diagonals first to exit sooner. */
        && T.Abs(m1x2) < T.Epsilon && T.Abs(m2x1) < T.Epsilon;

    /// <summary>
    /// Determines whether the specified M0X0 is identity.
    /// </summary>
    /// <param name="m0x0">The M0X0.</param>
    /// <param name="m0x1">The M0X1.</param>
    /// <param name="m1x0">The M1X0.</param>
    /// <param name="m1x1">The M1X1.</param>
    /// <param name="offsetX">The offset x.</param>
    /// <param name="offsetY">The offset y.</param>
    /// <returns>
    ///   <see langword="true" /> if the specified M0X0 is identity; otherwise, <see langword="false" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsMatrixIdentityEpsilon<T>(T m0x0, T m0x1, T m1x0, T m1x1, T offsetX, T offsetY)
        where T : IFloatingPointIeee754<T> =>
            T.Abs(m0x0 - T.One) < T.Epsilon
            && T.Abs(m0x1) < T.Epsilon
            && T.Abs(m1x0) < T.Epsilon
            && T.Abs(m1x1 - T.One) < T.Epsilon
            && T.Abs(offsetX) < T.Epsilon
            && T.Abs(offsetY) < T.Epsilon;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsMatrixIdentityEpsilon<T>(
        T m1x1, T m1x2, T m1x3,
        T m2x1, T m2x2, T m2x3,
        T m3x1, T m3x2, T m3x3)
        where T : IFloatingPointIeee754<T>
        => T.Abs(m1x1 - T.One) < T.Epsilon && T.Abs(m2x2 - T.One) < T.Epsilon && T.Abs(m3x3 - T.One) < T.Epsilon /* Check diagonals first to exit sooner. */
        && T.Abs(m1x2) < T.Epsilon && T.Abs(m1x3) < T.Epsilon && T.Abs(m2x1) < T.Epsilon
        && T.Abs(m2x3) < T.Epsilon && T.Abs(m3x1) < T.Epsilon && T.Abs(m3x2) < T.Epsilon;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsMatrixIdentityEpsilon<T>(
        T m1x1, T m1x2, T m1x3, T m1x4,
        T m2x1, T m2x2, T m2x3, T m2x4,
        T m3x1, T m3x2, T m3x3, T m3x4,
        T m4x1, T m4x2, T m4x3, T m4x4)
        where T : IFloatingPointIeee754<T>
        => T.Abs(m1x1 - T.One) < T.Epsilon && T.Abs(m2x2 - T.One) < T.Epsilon && T.Abs(m3x3 - T.One) < T.Epsilon && T.Abs(m4x4 - T.One) < T.Epsilon /* Check diagonals first to exit sooner. */
        && T.Abs(m1x2) < T.Epsilon && T.Abs(m1x3) < T.Epsilon && T.Abs(m1x4) < T.Epsilon && T.Abs(m2x1) < T.Epsilon
        && T.Abs(m2x3) < T.Epsilon && T.Abs(m2x4) < T.Epsilon && T.Abs(m3x1) < T.Epsilon && T.Abs(m3x2) < T.Epsilon
        && T.Abs(m3x4) < T.Epsilon && T.Abs(m4x1) < T.Epsilon && T.Abs(m4x2) < T.Epsilon && T.Abs(m4x3) < T.Epsilon;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsMatrixIdentityEpsilon<T>(
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5)
        where T : IFloatingPointIeee754<T>
        => T.Abs(m1x1 - T.One) < T.Epsilon && T.Abs(m2x2 - T.One) < T.Epsilon && T.Abs(m3x3 - T.One) < T.Epsilon && T.Abs(m4x4 - T.One) < T.Epsilon && T.Abs(m5x5 - T.One) < T.Epsilon /* Check diagonals first to exit sooner. */
        && T.Abs(m1x2) < T.Epsilon && T.Abs(m1x3) < T.Epsilon && T.Abs(m1x4) < T.Epsilon && T.Abs(m1x5) < T.Epsilon && T.Abs(m2x1) < T.Epsilon
        && T.Abs(m2x3) < T.Epsilon && T.Abs(m2x4) < T.Epsilon && T.Abs(m2x5) < T.Epsilon && T.Abs(m3x1) < T.Epsilon && T.Abs(m3x2) < T.Epsilon
        && T.Abs(m3x4) < T.Epsilon && T.Abs(m3x5) < T.Epsilon && T.Abs(m4x1) < T.Epsilon && T.Abs(m4x2) < T.Epsilon && T.Abs(m4x3) < T.Epsilon
        && T.Abs(m4x5) < T.Epsilon && T.Abs(m5x1) < T.Epsilon && T.Abs(m5x2) < T.Epsilon && T.Abs(m5x3) < T.Epsilon && T.Abs(m5x4) < T.Epsilon;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsMatrixIdentityEpsilon<T>(
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6)
        where T : IFloatingPointIeee754<T>
        => T.Abs(m1x1 - T.One) < T.Epsilon && T.Abs(m2x2 - T.One) < T.Epsilon && T.Abs(m3x3 - T.One) < T.Epsilon && T.Abs(m4x4 - T.One) < T.Epsilon && T.Abs(m5x5 - T.One) < T.Epsilon && T.Abs(m6x6 - T.One) < T.Epsilon /* Check diagonals first to exit sooner. */
        && T.Abs(m1x2) < T.Epsilon && T.Abs(m1x3) < T.Epsilon && T.Abs(m1x4) < T.Epsilon && T.Abs(m1x5) < T.Epsilon && T.Abs(m1x6) < T.Epsilon && T.Abs(m2x1) < T.Epsilon
        && T.Abs(m2x3) < T.Epsilon && T.Abs(m2x4) < T.Epsilon && T.Abs(m2x5) < T.Epsilon && T.Abs(m2x6) < T.Epsilon && T.Abs(m3x1) < T.Epsilon && T.Abs(m3x2) < T.Epsilon
        && T.Abs(m3x4) < T.Epsilon && T.Abs(m3x5) < T.Epsilon && T.Abs(m3x6) < T.Epsilon && T.Abs(m4x1) < T.Epsilon && T.Abs(m4x2) < T.Epsilon && T.Abs(m4x3) < T.Epsilon
        && T.Abs(m4x5) < T.Epsilon && T.Abs(m4x6) < T.Epsilon && T.Abs(m5x1) < T.Epsilon && T.Abs(m5x2) < T.Epsilon && T.Abs(m5x3) < T.Epsilon && T.Abs(m5x4) < T.Epsilon
        && T.Abs(m5x6) < T.Epsilon && T.Abs(m6x1) < T.Epsilon && T.Abs(m6x2) < T.Epsilon && T.Abs(m6x3) < T.Epsilon && T.Abs(m6x4) < T.Epsilon && T.Abs(m6x5) < T.Epsilon;
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsLowerMatrix<T>(T[,] matrix)
        where T : INumber<T>
    {
        var rows = matrix?.GetLength(0);
        var cols = matrix?.GetLength(1);

        for (var i = 0; i < rows; i++)
        {
            for (var j = i + 1; j < cols; j++)
            {
                if (matrix[i, j] != T.Zero)
                {
                    return false;
                }
            }
        }

        return true;
    }

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsLowerMatrix<T>(T[][] matrix)
        where T : INumber<T>
    {
        var rows = matrix?.Length;
        var cols = matrix[0]?.Length;

        for (var i = 0; i < rows; i++)
        {
            for (var j = i + 1; j < cols; j++)
            {
                if (matrix[i][j] != T.Zero)
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsLowerMatrix<T>(
        T m1x1, T m1x2,
        T m2x1, T m2x2)
        where T : INumber<T>
        => /*m1x2 == T.Zero
        &&*/ m1x1 != T.Zero
        && m2x1 != T.Zero && m2x2 != T.Zero;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsLowerMatrix<T>(
        T m1x1, T m1x2, T m1x3,
        T m2x1, T m2x2, T m2x3,
        T m3x1, T m3x2, T m3x3)
        where T : INumber<T>
        => /*m1x2 == T.Zero && m1x3 == T.Zero
        && m2x3 == T.Zero
        &&*/ m1x1 != T.Zero
        && m2x1 != T.Zero && m2x2 != T.Zero
        && m3x1 != T.Zero && m3x2 != T.Zero && m3x3 != T.Zero;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsLowerMatrix<T>(
        T m1x1, T m1x2, T m1x3, T m1x4,
        T m2x1, T m2x2, T m2x3, T m2x4,
        T m3x1, T m3x2, T m3x3, T m3x4,
        T m4x1, T m4x2, T m4x3, T m4x4)
        where T : INumber<T>
        => /*m1x2 == T.Zero && m1x3 == T.Zero && m1x4 == T.Zero
        && m2x3 == T.Zero && m2x4 == T.Zero
        && m3x4 == T.Zero
        &&*/ m1x1 != T.Zero
        && m2x1 != T.Zero && m2x2 != T.Zero
        && m3x1 != T.Zero && m3x2 != T.Zero && m3x3 != T.Zero
        && m4x1 != T.Zero && m4x2 != T.Zero && m4x3 != T.Zero && m4x4 != T.Zero;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsLowerMatrix<T>(
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5)
        where T : INumber<T>
        => /*m1x2 == T.Zero && m1x3 == T.Zero && m1x4 == T.Zero && m1x5 == T.Zero
        && m2x3 == T.Zero && m2x4 == T.Zero && m2x5 == T.Zero
        && m3x4 == T.Zero && m3x5 == T.Zero
        && m4x5 == T.Zero
        &&*/ m1x1 != T.Zero
        && m2x1 != T.Zero && m2x2 != T.Zero
        && m3x1 != T.Zero && m3x2 != T.Zero && m3x3 != T.Zero
        && m4x1 != T.Zero && m4x2 != T.Zero && m4x3 != T.Zero && m4x4 != T.Zero
        && m5x1 != T.Zero && m5x2 != T.Zero && m5x3 != T.Zero && m5x4 != T.Zero && m5x5 != T.Zero;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsLowerMatrix<T>(
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6)
        where T : INumber<T>
        => /*m1x2 == T.Zero && m1x3 == T.Zero && m1x4 == T.Zero && m1x5 == T.Zero && m1x6 == T.Zero
        && m2x3 == T.Zero && m2x4 == T.Zero && m2x5 == T.Zero && m2x6 == T.Zero
        && m3x4 == T.Zero && m3x5 == T.Zero && m3x6 == T.Zero
        && m4x5 == T.Zero && m4x6 == T.Zero
        && m5x6 == T.Zero
        &&*/ m1x1 != T.Zero
        && m2x1 != T.Zero && m2x2 != T.Zero
        && m3x1 != T.Zero && m3x2 != T.Zero && m3x3 != T.Zero
        && m4x1 != T.Zero && m4x2 != T.Zero && m4x3 != T.Zero && m4x4 != T.Zero
        && m5x1 != T.Zero && m5x2 != T.Zero && m5x3 != T.Zero && m5x4 != T.Zero && m5x5 != T.Zero
        && m6x1 != T.Zero && m6x2 != T.Zero && m6x3 != T.Zero && m6x4 != T.Zero && m6x5 != T.Zero && m6x6 != T.Zero;
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsUpperMatrix<T>(T[,] matrix)
        where T : INumber<T>
    {
        var rows = matrix?.GetLength(0);
        //var cols = matrix?.GetLength(1);

        for (var i = 1; i < rows; i++)
        {
            for (var j = 0; j < i; j++)
            {
                if (matrix[i, j] != T.Zero)
                {
                    return false;
                }
            }
        }

        return true;
    }

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsUpperMatrix<T>(T[][] matrix)
        where T : INumber<T>
    {
        var rows = matrix?.Length;
        //var cols = matrix[0]?.Length;

        for (var i = 1; i < rows; i++)
        {
            for (var j = 0; j < i; j++)
            {
                if (matrix[i][j] != T.Zero)
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsUpperMatrix<T>(
        T m1x1, T m1x2,
        T m2x1, T m2x2)
        where T : INumber<T>
        => m1x2 != T.Zero && m2x2 != T.Zero
        && m1x1 != T.Zero
        /*&& m2x1 == T.Zero*/;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsUpperMatrix<T>(
        T m1x1, T m1x2, T m1x3,
        T m2x1, T m2x2, T m2x3,
        T m3x1, T m3x2, T m3x3)
        where T : INumber<T>
        => m1x2 != T.Zero && m1x3 != T.Zero && m3x3 != T.Zero
        && m2x3 != T.Zero && m2x2 != T.Zero
        && m1x1 != T.Zero
        /*&& m2x1 == T.Zero
        && m3x1 == T.Zero && m3x2 == T.Zero*/;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsUpperMatrix<T>(
        T m1x1, T m1x2, T m1x3, T m1x4,
        T m2x1, T m2x2, T m2x3, T m2x4,
        T m3x1, T m3x2, T m3x3, T m3x4,
        T m4x1, T m4x2, T m4x3, T m4x4)
        where T : INumber<T>
        => m1x2 != T.Zero && m1x3 != T.Zero && m1x4 != T.Zero && m4x4 != T.Zero
        && m2x3 != T.Zero && m2x4 != T.Zero && m3x3 != T.Zero
        && m3x4 != T.Zero && m2x2 != T.Zero
        && m1x1 != T.Zero
        /*&& m2x1 == T.Zero
        && m3x1 == T.Zero && m3x2 == T.Zero
        && m4x1 == T.Zero && m4x2 == T.Zero && m4x3 == T.Zero*/;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsUpperMatrix<T>(
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5)
        where T : INumber<T>
        => m1x2 != T.Zero && m1x3 != T.Zero && m1x4 != T.Zero && m1x5 != T.Zero && m5x5 != T.Zero
        && m2x3 != T.Zero && m2x4 != T.Zero && m2x5 != T.Zero && m4x4 != T.Zero
        && m3x4 != T.Zero && m3x5 != T.Zero && m3x3 != T.Zero
        && m4x5 != T.Zero && m2x2 != T.Zero
        && m1x1 != T.Zero
        /*&& m2x1 == T.Zero
        && m3x1 == T.Zero && m3x2 == T.Zero
        && m4x1 == T.Zero && m4x2 == T.Zero && m4x3 == T.Zero
        && m5x1 == T.Zero && m5x2 == T.Zero && m5x3 == T.Zero && m5x4 == T.Zero*/;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsUpperMatrix<T>(
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6)
        where T : INumber<T>
        => m1x2 != T.Zero && m1x3 != T.Zero && m1x4 != T.Zero && m1x5 != T.Zero && m1x6 != T.Zero && m6x6 != T.Zero
        && m2x3 != T.Zero && m2x4 != T.Zero && m2x5 != T.Zero && m2x6 != T.Zero && m5x5 != T.Zero
        && m3x4 != T.Zero && m3x5 != T.Zero && m3x6 != T.Zero && m4x4 != T.Zero
        && m4x5 != T.Zero && m4x6 != T.Zero && m3x3 != T.Zero
        && m5x6 != T.Zero && m2x2 != T.Zero
        && m1x1 != T.Zero
        /*&& m2x1 == T.Zero
        && m3x1 == T.Zero && m3x2 == T.Zero
        && m4x1 == T.Zero && m4x2 == T.Zero && m4x3 == T.Zero
        && m5x1 == T.Zero && m5x2 == T.Zero && m5x3 == T.Zero && m5x4 == T.Zero
        && m6x1 == T.Zero && m6x2 == T.Zero && m6x3 == T.Zero && m6x4 == T.Zero && m6x5 == T.Zero*/;
    #endregion
}
