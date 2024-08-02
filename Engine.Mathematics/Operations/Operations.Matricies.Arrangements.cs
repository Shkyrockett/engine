// <copyright file="Operations.Matricies.Arrangements.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Engine;

/// <summary>
/// The Operations class.
/// </summary>
public static partial class Operations
{
    #region Truncate Matrix
    /// <summary>
    /// Truncate a matrix
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <param name="rowStart">The row start.</param>
    /// <param name="rowEnd">The row end.</param>
    /// <param name="columnStart">The column start.</param>
    /// <param name="columnEnd">The column end.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// https://github.com/SarahFrem/AutoRegressive_model_cs/blob/master/Matrix.cs#L100
    /// </acknowledgment>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double[,] Truncate(double[,] matrix, int rowStart, int rowEnd, int columnStart, int columnEnd)
    {
        var rows = matrix.GetLength(0);
        var cols = matrix.GetLength(1);

        var minimumIndex = rowStart == 0 || rowEnd == 0 || columnStart == 0 || columnEnd == 0;
        var coherenceIndex = rowEnd < rowStart || columnEnd < columnStart;
        var boundIndex = rowEnd > rows || columnEnd > cols;

        if (minimumIndex || coherenceIndex || boundIndex)
        {
            return new double[1, 1];
        }

        var result = new double[rowEnd - rowStart + 1, columnEnd - columnStart + 1];

        for (var i = rowStart - 1; i < rowEnd; i++)
        {
            for (var j = columnStart - 1; j < columnEnd; j++)
            {
                result[i - rowStart + 1, j - columnStart + 1] = matrix[i, j];
            }
        }

        return result;
    }

    /// <summary>
    /// Truncate a matrix
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <param name="rowStart">The row start.</param>
    /// <param name="rowEnd">The row end.</param>
    /// <param name="columnStart">The column start.</param>
    /// <param name="columnEnd">The column end.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// https://github.com/SarahFrem/AutoRegressive_model_cs/blob/master/Matrix.cs#L100
    /// </acknowledgment>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double[][] Truncate(double[][] matrix, int rowStart, int rowEnd, int columnStart, int columnEnd)
    {
        var rows = matrix.GetLength(0);
        var cols = matrix.GetLength(1);

        var minimumIndex = rowStart == 0 || rowEnd == 0 || columnStart == 0 || columnEnd == 0;
        var coherenceIndex = rowEnd < rowStart || columnEnd < columnStart;
        var boundIndex = rowEnd > rows || columnEnd > cols;

        if (minimumIndex || coherenceIndex || boundIndex)
        {
            return [[0d]];
        }

        var result = new double[rowEnd - rowStart + 1][];

        for (var i = rowStart - 1; i < rowEnd; i++)
        {
            result[i] = new double[columnEnd - columnStart + 1];
            for (var j = columnStart - 1; j < columnEnd; j++)
            {
                result[i - rowStart + 1][j - columnStart + 1] = matrix[i][j];
            }
        }

        return result;
    }
    #endregion

    #region Transpose Matrix
    /// <summary>
    /// Returns the transpose of a matrix
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// https://github.com/SarahFrem/AutoRegressive_model_cs/blob/master/Matrix.cs#L100
    /// </acknowledgment>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double[,] Transpose(double[,] matrix)
    {
        var rows = matrix.GetLength(0);
        var cols = matrix.GetLength(1);

        var res = new double[cols, rows];
        for (var i = 0; i < cols; i++)
        {
            for (var j = 0; j < rows; j++)
            {
                res[i, j] = matrix[j, i];
            }
        }

        return res;
    }

    /// <summary>
    /// Returns the transpose of a matrix
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// https://github.com/SarahFrem/AutoRegressive_model_cs/blob/master/Matrix.cs#L100
    /// </acknowledgment>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double[][] Transpose(double[][] matrix)
    {
        var rows = matrix.Length;
        var cols = matrix[0].Length;

        var res = new double[cols][];
        for (var i = 0; i < cols; i++)
        {
            res[i] = new double[rows];
            for (var j = 0; j < rows; j++)
            {
                res[i][j] = matrix[j][i];
            }
        }

        return res;
    }

    /// <summary>
    /// Swap the rows of the matrix with the columns.
    /// </summary>
    /// <param name="m1x1">The M1X1.</param>
    /// <param name="m1x2">The M1X2.</param>
    /// <param name="m2x1">The M2X1.</param>
    /// <param name="m2x2">The M2X2.</param>
    /// <returns>
    /// A transposed Matrix.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        double m1x1, double m1x2,
        double m2x1, double m2x2)
        TransposeMatrix(
        double m1x1, double m1x2,
        double m2x1, double m2x2)
        => (m1x1, m2x1,
            m1x2, m2x2);

    /// <summary>
    /// Swap the rows of the matrix with the columns.
    /// </summary>
    /// <param name="m1x1">The M1X1.</param>
    /// <param name="m1x2">The M1X2.</param>
    /// <param name="m1x3">The M1X3.</param>
    /// <param name="m2x1">The M2X1.</param>
    /// <param name="m2x2">The M2X2.</param>
    /// <param name="m2x3">The M2X3.</param>
    /// <param name="m3x1">The M3X1.</param>
    /// <param name="m3x2">The M3X2.</param>
    /// <param name="m3x3">The M3X3.</param>
    /// <returns>
    /// A transposed Matrix.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        double m1x1, double m1x2, double m1x3,
        double m2x1, double m2x2, double m2x3,
        double m3x1, double m3x2, double m3x3)
        TransposeMatrix(
        double m1x1, double m1x2, double m1x3,
        double m2x1, double m2x2, double m2x3,
        double m3x1, double m3x2, double m3x3)
        => (m1x1, m2x1, m3x1,
            m1x2, m2x2, m3x2,
            m1x3, m2x3, m3x3);

    /// <summary>
    /// Swap the rows of the matrix with the columns.
    /// </summary>
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
    /// <returns>
    /// A transposed Matrix.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        double m1x1, double m1x2, double m1x3, double m1x4,
        double m2x1, double m2x2, double m2x3, double m2x4,
        double m3x1, double m3x2, double m3x3, double m3x4,
        double m4x1, double m4x2, double m4x3, double m4x4)
        TransposeMatrix(
        double m1x1, double m1x2, double m1x3, double m1x4,
        double m2x1, double m2x2, double m2x3, double m2x4,
        double m3x1, double m3x2, double m3x3, double m3x4,
        double m4x1, double m4x2, double m4x3, double m4x4)
        => (m1x1, m2x1, m3x1, m4x1,
            m1x2, m2x2, m3x2, m4x2,
            m1x3, m2x3, m3x3, m4x3,
            m1x4, m2x4, m3x4, m4x4);

    /// <summary>
    /// Swap the rows of the matrix with the columns.
    /// </summary>
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
    /// <returns>
    /// A transposed Matrix.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
        double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
        double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
        double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
        double m5x1, double m5x2, double m5x3, double m5x4, double m5x5)
        TransposeMatrix(
        double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
        double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
        double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
        double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
        double m5x1, double m5x2, double m5x3, double m5x4, double m5x5)
        => (m1x1, m2x1, m3x1, m4x1, m5x1,
            m1x2, m2x2, m3x2, m4x2, m5x2,
            m1x3, m2x3, m3x3, m4x3, m5x3,
            m1x4, m2x4, m3x4, m4x4, m5x4,
            m1x5, m2x5, m3x5, m4x5, m5x5);

    /// <summary>
    /// Swap the rows of the matrix with the columns.
    /// </summary>
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
    /// <returns>
    /// A transposed Matrix.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
        double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
        double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
        double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
        double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
        double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6
        ) TransposeMatrix(
        double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
        double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
        double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
        double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
        double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
        double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6)
        => (m1x1, m2x1, m3x1, m4x1, m5x1, m6x1,
            m1x2, m2x2, m3x2, m4x2, m5x2, m6x2,
            m1x3, m2x3, m3x3, m4x3, m5x3, m6x3,
            m1x4, m2x4, m3x4, m4x4, m5x4, m6x4,
            m1x5, m2x5, m3x5, m4x5, m5x5, m6x5,
            m1x6, m2x6, m3x6, m4x6, m5x6, m6x6);
    #endregion Transpose

    #region Flip Matrix Horizontally
    /// <summary>
    /// Flip the columns of the matrix.
    /// </summary>
    /// <param name="m1x1">The M1X1.</param>
    /// <param name="m1x2">The M1X2.</param>
    /// <param name="m2x1">The M2X1.</param>
    /// <param name="m2x2">The M2X2.</param>
    /// <returns>
    /// A flipped Matrix.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        double m1x1, double m1x2,
        double m2x1, double m2x2)
        FlipMatrixHorizontal(
        double m1x1, double m1x2,
        double m2x1, double m2x2)
        => (m1x2, m1x1,
            m2x2, m2x1);

    /// <summary>
    /// Flip the columns of the matrix.
    /// </summary>
    /// <param name="m1x1">The M1X1.</param>
    /// <param name="m1x2">The M1X2.</param>
    /// <param name="m1x3">The M1X3.</param>
    /// <param name="m2x1">The M2X1.</param>
    /// <param name="m2x2">The M2X2.</param>
    /// <param name="m2x3">The M2X3.</param>
    /// <param name="m3x1">The M3X1.</param>
    /// <param name="m3x2">The M3X2.</param>
    /// <param name="m3x3">The M3X3.</param>
    /// <returns>
    /// A flipped Matrix.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        double m1x1, double m1x2, double m1x3,
        double m2x1, double m2x2, double m2x3,
        double m3x1, double m3x2, double m3x3)
        FlipMatrixHorizontal(
        double m1x1, double m1x2, double m1x3,
        double m2x1, double m2x2, double m2x3,
        double m3x1, double m3x2, double m3x3)
        => (m1x3, m1x2, m1x1,
            m2x3, m2x2, m2x1,
            m3x3, m3x2, m3x1);

    /// <summary>
    /// Flip the columns of the matrix.
    /// </summary>
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
    /// <returns>
    /// A flipped Matrix.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        double m1x1, double m1x2, double m1x3, double m1x4,
        double m2x1, double m2x2, double m2x3, double m2x4,
        double m3x1, double m3x2, double m3x3, double m3x4,
        double m4x1, double m4x2, double m4x3, double m4x4)
        FlipMatrixHorizontal(
        double m1x1, double m1x2, double m1x3, double m1x4,
        double m2x1, double m2x2, double m2x3, double m2x4,
        double m3x1, double m3x2, double m3x3, double m3x4,
        double m4x1, double m4x2, double m4x3, double m4x4)
        => (m1x4, m1x3, m1x2, m1x1,
            m2x4, m2x3, m2x2, m2x1,
            m3x4, m3x3, m3x2, m3x1,
            m4x4, m4x3, m4x2, m4x1);

    /// <summary>
    /// Flip the columns of the matrix.
    /// </summary>
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
    /// <returns>
    /// A flipped Matrix.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
        double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
        double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
        double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
        double m5x1, double m5x2, double m5x3, double m5x4, double m5x5)
        FlipMatrixHorizontal(
        double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
        double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
        double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
        double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
        double m5x1, double m5x2, double m5x3, double m5x4, double m5x5)
        => (m1x5, m1x4, m1x3, m1x2, m1x1,
            m2x5, m2x4, m2x3, m2x2, m2x1,
            m3x5, m3x4, m3x3, m3x2, m3x1,
            m4x5, m4x4, m4x3, m4x2, m4x1,
            m5x5, m5x4, m5x3, m5x2, m5x1);

    /// <summary>
    /// Flip the columns of the matrix.
    /// </summary>
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
    /// <returns>
    /// A flipped Matrix.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
        double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
        double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
        double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
        double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
        double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6)
        FlipMatrixHorizontal(
        double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
        double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
        double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
        double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
        double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
        double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6)
        => (m1x6, m1x5, m1x4, m1x3, m1x2, m1x1,
            m2x6, m2x5, m2x4, m2x3, m2x2, m2x1,
            m3x6, m3x5, m3x4, m3x3, m3x2, m3x1,
            m4x6, m4x5, m4x4, m4x3, m4x2, m4x1,
            m5x6, m5x5, m5x4, m5x3, m5x2, m5x1,
            m6x6, m6x5, m6x4, m6x3, m6x2, m6x1);
    #endregion

    #region Flip Matrix Vertically
    /// <summary>
    /// Flip the rows of the matrix.
    /// </summary>
    /// <param name="m1x1">The M1X1.</param>
    /// <param name="m1x2">The M1X2.</param>
    /// <param name="m2x1">The M2X1.</param>
    /// <param name="m2x2">The M2X2.</param>
    /// <returns>
    /// A flipped Matrix.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        double m1x1, double m1x2,
        double m2x1, double m2x2)
        FlipMatrixVertical(
        double m1x1, double m1x2,
        double m2x1, double m2x2)
        => (m2x1, m2x2,
            m1x1, m1x2);

    /// <summary>
    /// Flip the rows of the matrix.
    /// </summary>
    /// <param name="m1x1">The M1X1.</param>
    /// <param name="m1x2">The M1X2.</param>
    /// <param name="m1x3">The M1X3.</param>
    /// <param name="m2x1">The M2X1.</param>
    /// <param name="m2x2">The M2X2.</param>
    /// <param name="m2x3">The M2X3.</param>
    /// <param name="m3x1">The M3X1.</param>
    /// <param name="m3x2">The M3X2.</param>
    /// <param name="m3x3">The M3X3.</param>
    /// <returns>
    /// A flipped Matrix.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        double m1x1, double m1x2, double m1x3,
        double m2x1, double m2x2, double m2x3,
        double m3x1, double m3x2, double m3x3)
        FlipMatrixVertical(
        double m1x1, double m1x2, double m1x3,
        double m2x1, double m2x2, double m2x3,
        double m3x1, double m3x2, double m3x3)
        => (m3x1, m3x2, m3x3,
            m2x1, m2x2, m2x3,
            m1x1, m1x2, m1x3);

    /// <summary>
    /// Flip the rows of the matrix.
    /// </summary>
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
    /// <returns>
    /// A flipped Matrix.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        double m1x1, double m1x2, double m1x3, double m1x4,
        double m2x1, double m2x2, double m2x3, double m2x4,
        double m3x1, double m3x2, double m3x3, double m3x4,
        double m4x1, double m4x2, double m4x3, double m4x4)
        FlipMatrixVertical(
        double m1x1, double m1x2, double m1x3, double m1x4,
        double m2x1, double m2x2, double m2x3, double m2x4,
        double m3x1, double m3x2, double m3x3, double m3x4,
        double m4x1, double m4x2, double m4x3, double m4x4)
        => (m4x1, m4x2, m4x3, m4x4,
            m3x1, m3x2, m3x3, m3x4,
            m2x1, m2x2, m2x3, m2x4,
            m1x1, m1x2, m1x3, m1x4);

    /// <summary>
    /// Flip the rows of the matrix.
    /// </summary>
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
    /// <returns>
    /// A flipped Matrix.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
        double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
        double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
        double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
        double m5x1, double m5x2, double m5x3, double m5x4, double m5x5)
        FlipMatrixVertical(
        double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
        double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
        double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
        double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
        double m5x1, double m5x2, double m5x3, double m5x4, double m5x5)
        => (m5x1, m5x2, m5x3, m5x4, m5x5,
            m4x1, m4x2, m4x3, m4x4, m4x5,
            m3x1, m3x2, m3x3, m3x4, m3x5,
            m2x1, m2x2, m2x3, m2x4, m2x5,
            m1x1, m1x2, m1x3, m1x4, m1x5);

    /// <summary>
    /// Flip the rows of the matrix.
    /// </summary>
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
    /// <returns>
    /// A flipped Matrix.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
        double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
        double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
        double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
        double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
        double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6)
        FlipMatrixVertical(
        double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
        double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
        double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
        double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
        double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
        double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6)
        => (m6x1, m6x2, m6x3, m6x4, m6x5, m6x6,
            m5x1, m5x2, m5x3, m5x4, m5x5, m5x6,
            m4x1, m4x2, m4x3, m4x4, m4x5, m4x6,
            m3x1, m3x2, m3x3, m3x4, m3x5, m3x6,
            m2x1, m2x2, m2x3, m2x4, m2x5, m2x6,
            m1x1, m1x2, m1x3, m1x4, m1x5, m1x6);
    #endregion
}
