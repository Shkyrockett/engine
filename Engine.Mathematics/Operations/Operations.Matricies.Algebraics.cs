// <copyright file="Operations.MatriciesAlgebraics.cs" company="Shkyrockett" >
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
    /// <summary>
    /// Converts to jagged array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="twoDimensionalArray">The two dimensional array.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// https://stackoverflow.com/a/25995025
    /// </acknowledgment>
    public static T[][] ToJaggedArray<T>(this T[,] twoDimensionalArray)
    {
        var rowsFirstIndex = twoDimensionalArray.GetLowerBound(0);
        var rowsLastIndex = twoDimensionalArray.GetUpperBound(0);
        var numberOfRows = rowsLastIndex + 1;

        var columnsFirstIndex = twoDimensionalArray.GetLowerBound(1);
        var columnsLastIndex = twoDimensionalArray.GetUpperBound(1);
        var numberOfColumns = columnsLastIndex + 1;

        var jaggedArray = new T[numberOfRows][];
        for (var i = rowsFirstIndex; i <= rowsLastIndex; i++)
        {
            jaggedArray[i] = new T[numberOfColumns];

            for (var j = columnsFirstIndex; j <= columnsLastIndex; j++)
            {
                jaggedArray[i][j] = twoDimensionalArray[i, j];
            }
        }
        return jaggedArray;
    }

    #region Adjoint
    /// <summary>
    /// Function to get adjoint of the specified matrix.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// https://www.geeksforgeeks.org/adjoint-inverse-matrix/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T[,] Adjoint<T>(T[,] matrix)
        where T : INumber<T>
    {
        var rows = matrix.GetLength(0);
        var cols = matrix.GetLength(1);

        if (rows == 1)
        {
            return new T[1, 1] { { T.One } };
        }

        var adj = new T[rows, cols];

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                // Get cofactor of A[i,j] 
                var temp = Cofactor(matrix, i, j);

                // Sign of adj[j,i] positive if sum of row and column indexes is even. 
                var sign = ((i + j) % 2 == 0) ? 1 : -1;

                // Interchanging rows and columns to get the  transpose of the cofactor matrix 
                adj[j, i] = T.CreateChecked(sign * Cast<T, double>(Determinant(temp)));
            }
        }

        return adj;
    }

    /// <summary>
    /// Function to get adjoint of the specified matrix.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// https://www.geeksforgeeks.org/adjoint-inverse-matrix/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T[][] Adjoint<T>(T[][] matrix)
        where T : INumber<T>
    {
        var rows = matrix.Length;
        var cols = matrix[0].Length;

        if (rows == 1)
        {
            return [[T.One]];
        }

        var adj = new T[rows][];

        for (var i = 0; i < rows; i++)
        {
            adj[i] = new T[cols];
            for (var j = 0; j < cols; j++)
            {
                // Get cofactor of A[i,j] 
                var temp = Cofactor(matrix, i, j);

                // Sign of adj[j,i] positive if sum of row and column indexes is even. 
                var sign = ((i + j) % 2 == 0) ? 1 : -1;

                // Interchanging rows and columns to get the  transpose of the cofactor matrix 
                adj[j][i] = T.CreateChecked(sign * Cast<T, double>(Determinant(temp)));
            }
        }

        return adj;
    }

    /// <summary>
    /// The adjoint.
    /// </summary>
    /// <param name="m1x1">The M1X1.</param>
    /// <param name="m1x2">The M1X2.</param>
    /// <param name="m2x1">The M2X1.</param>
    /// <param name="m2x2">The M2X2.</param>
    /// <returns>
    /// The <see cref="Matrix3x3{T}" />.
    /// </returns>
    /// <acknowledgment>
    /// https://sites.google.com/site/physics2d/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m1x1, T m1x2,
        T m2x1, T m2x2)
        AdjointMatrix<T>(
        T m1x1, T m1x2,
        T m2x1, T m2x2)
        where T : INumber<T>
        => (
            m2x2, -m1x2,
            -m2x1, m1x1);

    /// <summary>
    /// The adjoint.
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
    /// The <see cref="Matrix3x3{T}" />.
    /// </returns>
    /// <acknowledgment>
    /// https://sites.google.com/site/physics2d/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m1x1, T m1x2, T m1x3,
        T m2x1, T m2x2, T m2x3,
        T m3x1, T m3x2, T m3x3)
        AdjointMatrix<T>(
        T m1x1, T m1x2, T m1x3,
        T m2x1, T m2x2, T m2x3,
        T m3x1, T m3x2, T m3x3)
        where T : INumber<T>
        => (
            (m2x2 * m3x3) - (m2x3 * m3x2), -((m1x2 * m3x3) - (m1x3 * m3x2)), (m1x2 * m2x3) - (m1x3 * m2x2),
            -((m2x1 * m3x3) - (m2x3 * m3x1)), (m1x1 * m3x3) - (m1x3 * m3x1), -((m1x1 * m2x3) - (m1x3 * m2x1)),
            (m2x1 * m3x2) - (m2x2 * m3x1), -((m1x1 * m3x2) - (m1x2 * m3x1)), (m1x1 * m2x2) - (m1x2 * m2x1));

    /// <summary>
    /// Used to generate the adjoint of this matrix.
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
    /// The adjoint matrix of the current instance.
    /// </returns>
    /// <acknowledgment>
    /// https://sites.google.com/site/physics2d/
    /// This is an expanded version of the Ogre adjoint() method.
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m1x1, T m1x2, T m1x3, T m1x4,
        T m2x1, T m2x2, T m2x3, T m2x4,
        T m3x1, T m3x2, T m3x3, T m3x4,
        T m4x1, T m4x2, T m4x3, T m4x4)
        AdjointMatrix<T>(
        T m1x1, T m1x2, T m1x3, T m1x4,
        T m2x1, T m2x2, T m2x3, T m2x4,
        T m3x1, T m3x2, T m3x3, T m3x4,
        T m4x1, T m4x2, T m4x3, T m4x4)
        where T : INumber<T>
    {
        var m33m44m43m34 = (m3x3 * m4x4) - (m4x3 * m3x4);
        var m32m44m42m34 = (m3x2 * m4x4) - (m4x2 * m3x4);
        var m32m43m42m33 = (m3x2 * m4x3) - (m4x2 * m3x3);

        var m23m44m43m24 = (m2x3 * m4x4) - (m4x3 * m2x4);
        var m22m44m42m24 = (m2x2 * m4x4) - (m4x2 * m2x4);
        var m22m43m42m23 = (m2x2 * m4x3) - (m4x2 * m2x3);

        var m23m34m33m24 = (m2x3 * m3x4) - (m3x3 * m2x4);
        var m22m34m32m24 = (m2x2 * m3x4) - (m3x2 * m2x4);
        var m22m33m32m23 = (m2x2 * m3x3) - (m3x2 * m2x3);

        var m31m44m41m34 = (m3x1 * m4x4) - (m4x1 * m3x4);
        var m31m43m41m33 = (m3x1 * m4x3) - (m4x1 * m3x3);
        var m21m44m41m24 = (m2x1 * m4x4) - (m4x1 * m2x4);

        var m21m43m41m23 = (m2x1 * m4x3) - (m4x1 * m2x3);
        var m21m34m31m24 = (m2x1 * m3x4) - (m3x1 * m2x4);
        var m21m33m31m23 = (m2x1 * m3x3) - (m3x1 * m2x3);

        var m31m42m41m32 = (m3x1 * m4x2) - (m4x1 * m3x2);
        var m21m42m41m22 = (m2x1 * m4x2) - (m4x1 * m2x2);
        var m21m32m31m22 = (m2x1 * m3x2) - (m3x1 * m2x2);

        return (
              (m2x2 * m33m44m43m34) - (m2x3 * m32m44m42m34) + (m2x4 * m32m43m42m33), -((m1x2 * m33m44m43m34) - (m1x3 * m32m44m42m34) + (m1x4 * m32m43m42m33)), (m1x2 * m23m44m43m24) - (m1x3 * m22m44m42m24) + (m1x4 * m22m43m42m23), -((m1x2 * m23m34m33m24) - (m1x3 * m22m34m32m24) + (m1x4 * m22m33m32m23)),
            -((m2x1 * m33m44m43m34) - (m2x3 * m31m44m41m34) + (m2x4 * m31m43m41m33)), (m1x1 * m33m44m43m34) - (m1x3 * m31m44m41m34) + (m1x4 * m31m43m41m33), -((m1x1 * m23m44m43m24) - (m1x3 * m21m44m41m24) + (m1x4 * m21m43m41m23)), (m1x1 * m23m34m33m24) - (m1x3 * m21m34m31m24) + (m1x4 * m21m33m31m23),
              (m2x1 * m32m44m42m34) - (m2x2 * m31m44m41m34) + (m2x4 * m31m42m41m32), -((m1x1 * m32m44m42m34) - (m1x2 * m31m44m41m34) + (m1x4 * m31m42m41m32)), (m1x1 * m22m44m42m24) - (m1x2 * m21m44m41m24) + (m1x4 * m21m42m41m22), -((m1x1 * m22m34m32m24) - (m1x2 * m21m34m31m24) + (m1x4 * m21m32m31m22)),
            -((m2x1 * m32m43m42m33) - (m2x2 * m31m43m41m33) + (m2x3 * m31m42m41m32)), (m1x1 * m32m43m42m33) - (m1x2 * m31m43m41m33) + (m1x3 * m31m42m41m32), -((m1x1 * m22m43m42m23) - (m1x2 * m21m43m41m23) + (m1x3 * m21m42m41m22)), (m1x1 * m22m33m32m23) - (m1x2 * m21m33m31m23) + (m1x3 * m21m32m31m22)
            );
    }

    /// <summary>
    /// Adjoints the specified matrix.
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
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5)
        AdjointMatrix<T>(
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5)
        where T : INumber<T>
    {
        var m = Adjoint(new T[,]
            {
                {m1x1, m1x2, m1x3, m1x4, m1x5},
                {m2x1, m2x2, m2x3, m2x4, m2x5},
                {m3x1, m3x2, m3x3, m3x4, m3x5},
                {m4x1, m4x2, m4x3, m4x4, m4x5},
                {m5x1, m5x2, m5x3, m5x4, m5x5}
            });
        return (m[0, 0], m[0, 1], m[0, 2], m[0, 3], m[0, 4],
                m[1, 0], m[1, 1], m[1, 2], m[1, 3], m[1, 4],
                m[2, 0], m[2, 1], m[2, 2], m[2, 3], m[2, 4],
                m[3, 0], m[3, 1], m[3, 2], m[3, 3], m[3, 4],
                m[4, 0], m[4, 1], m[4, 2], m[4, 3], m[4, 4]);
    }

    /// <summary>
    /// Adjoints the specified M0X0.
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
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6)
        AdjointMatrix<T>(
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6)
        where T : INumber<T>
    {
        var m = Adjoint(new T[,]
            {
                {m1x1, m1x2, m1x3, m1x4, m1x5, m1x6},
                {m2x1, m2x2, m2x3, m2x4, m2x5, m2x6},
                {m3x1, m3x2, m3x3, m3x4, m3x5, m3x6},
                {m4x1, m4x2, m4x3, m4x4, m4x5, m4x6},
                {m5x1, m5x2, m5x3, m5x4, m5x5, m5x6},
                {m6x1, m6x2, m6x3, m6x4, m6x5, m6x6}
            });
        return (m[0, 0], m[0, 1], m[0, 2], m[0, 3], m[0, 4], m[0, 5],
                m[1, 0], m[1, 1], m[1, 2], m[1, 3], m[1, 4], m[1, 5],
                m[2, 0], m[2, 1], m[2, 2], m[2, 3], m[2, 4], m[2, 5],
                m[3, 0], m[3, 1], m[3, 2], m[3, 3], m[3, 4], m[3, 5],
                m[4, 0], m[4, 1], m[4, 2], m[4, 3], m[4, 4], m[4, 5],
                m[5, 0], m[5, 1], m[5, 2], m[5, 3], m[5, 4], m[5, 5]);
    }
    #endregion Adjoint

    #region Cofactor
    /// <summary>
    /// Cofactors the specified a.
    /// </summary>
    /// <param name="matrix">a.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// https://www.geeksforgeeks.org/determinant-of-a-matrix/
    /// https://www.geeksforgeeks.org/adjoint-inverse-matrix/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T[,] Cofactor<T>(T[,] matrix)
        where T : INumber<T>
    {
        var i = 0;
        var j = 0;
        var rows = matrix.GetLength(0);
        var cols = matrix.GetLength(1);
        var temp = new T[rows, cols];

        // Looping for each element of the matrix 
        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < cols; col++)
            {
                temp[i, j++] = matrix[row, col];

                // Row is filled, so increase row index and 
                // reset col index 
                if (j == cols - 1)
                {
                    j = 0;
                    i++;
                }
            }
        }

        return temp;
    }

    /// <summary>
    /// Cofactors the specified a.
    /// </summary>
    /// <param name="matrix">a.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// https://www.geeksforgeeks.org/determinant-of-a-matrix/
    /// https://www.geeksforgeeks.org/adjoint-inverse-matrix/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T[][] Cofactor<T>(T[][] matrix)
        where T : INumber<T>
    {
        var i = 0;
        var j = 0;
        var rows = matrix.Length;
        var cols = matrix[0].Length;
        var temp = new T[rows][];

        // Looping for each element of the matrix 
        for (var row = 0; row < rows; row++)
        {
            temp[row] = new T[cols];
            for (var col = 0; col < cols; col++)
            {
                temp[i][j++] = matrix[row][col];

                // Row is filled, so increase row index and 
                // reset col index 
                if (j == cols - 1)
                {
                    j = 0;
                    i++;
                }
            }
        }

        return temp;
    }

    /// <summary>
    /// Cofactors the specified a.
    /// </summary>
    /// <param name="matrix">a.</param>
    /// <param name="p">The p.</param>
    /// <param name="q">The q.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// https://www.geeksforgeeks.org/determinant-of-a-matrix/
    /// https://www.geeksforgeeks.org/adjoint-inverse-matrix/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T[,] Cofactor<T>(T[,] matrix, int p, int q)
        where T : INumber<T>
    {
        var i = 0;
        var j = 0;
        var rows = matrix.GetLength(0);
        var cols = matrix.GetLength(1);
        var temp = new T[rows, cols];

        // Looping for each element of the matrix 
        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < cols; col++)
            {
                // Copying into temporary matrix only those element 
                // which are not in given row and column 
                if (row != p && col != q)
                {
                    temp[i, j++] = matrix[row, col];

                    // Row is filled, so increase row index and 
                    // reset col index 
                    if (j == cols - 1)
                    {
                        j = 0;
                        i++;
                    }
                }
            }
        }

        return temp;
    }

    /// <summary>
    /// Cofactors the specified a.
    /// </summary>
    /// <param name="matrix">a.</param>
    /// <param name="p">The p.</param>
    /// <param name="q">The q.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// https://www.geeksforgeeks.org/determinant-of-a-matrix/
    /// https://www.geeksforgeeks.org/adjoint-inverse-matrix/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T[][] Cofactor<T>(T[][] matrix, int p, int q)
        where T : INumber<T>
    {
        var i = 0;
        var j = 0;
        var rows = matrix.GetLength(0);
        var cols = matrix.GetLength(1);
        var temp = new T[rows][];

        // Looping for each element of the matrix 
        for (var row = 0; row < rows; row++)
        {
            temp[row] = new T[cols];
            for (var col = 0; col < cols; col++)
            {
                // Copying into temporary matrix only those element 
                // which are not in given row and column 
                if (row != p && col != q)
                {
                    temp[i][j++] = matrix[row][col];

                    // Row is filled, so increase row index and 
                    // reset col index 
                    if (j == cols - 1)
                    {
                        j = 0;
                        i++;
                    }
                }
            }
        }

        return temp;
    }

    /// <summary>
    /// The cofactor.
    /// </summary>
    /// <param name="m1x1">The M0X0.</param>
    /// <param name="m1x2">The M0X1.</param>
    /// <param name="m2x1">The M1X0.</param>
    /// <param name="m2x2">The M1X1.</param>
    /// <returns>
    /// The <see cref="Matrix2x2{T}" />.
    /// </returns>
    /// <acknowledgment>
    /// https://sites.google.com/site/physics2d/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m1x1, T m1x2,
        T m2x1, T m2x2)
        CofactorMatrix<T>(
        T m1x1, T m1x2,
        T m2x1, T m2x2)
        where T : INumber<T>
        => (-m2x2, m1x2,
            m2x1, -m1x1);

    /// <summary>
    /// The cofactor.
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
    /// The <see cref="Matrix3x3{T}" />.
    /// </returns>
    /// <acknowledgment>
    /// https://sites.google.com/site/physics2d/
    /// This is an expanded version of the Ogre determinant() method, to give better performance in C#. Generated using a script.
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m1x1, T m1x2, T m1x3,
        T m2x1, T m2x2, T m2x3,
        T m3x1, T m3x2, T m3x3)
        CofactorMatrix<T>(
        T m1x1, T m1x2, T m1x3,
        T m2x1, T m2x2, T m2x3,
        T m3x1, T m3x2, T m3x3)
        where T : INumber<T>
        => (-((m2x2 * m3x3) - (m2x3 * m3x2)), (m1x2 * m3x3) - (m1x3 * m3x2), -((m1x2 * m2x3) - (m1x3 * m2x2)),
              (m2x1 * m3x3) - (m2x3 * m3x1), -((m1x1 * m3x3) - (m1x3 * m3x1)), (m1x1 * m2x3) - (m1x3 * m2x1),
            -((m2x1 * m3x2) - (m2x2 * m3x1)), (m1x1 * m3x2) - (m1x2 * m3x1), -((m1x1 * m2x2) - (m1x2 * m2x1)));

    /// <summary>
    /// The cofactor.
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
    /// The <see cref="Matrix4x4{T}" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m1x1, T m1x2, T m1x3, T m1x4,
        T m2x1, T m2x2, T m2x3, T m2x4,
        T m3x1, T m3x2, T m3x3, T m3x4,
        T m4x1, T m4x2, T m4x3, T m4x4)
        CofactorMatrix<T>(
        T m1x1, T m1x2, T m1x3, T m1x4,
        T m2x1, T m2x2, T m2x3, T m2x4,
        T m3x1, T m3x2, T m3x3, T m3x4,
        T m4x1, T m4x2, T m4x3, T m4x4)
        where T : INumber<T>
    {
        var m33m44m43m34 = (m3x3 * m4x4) - (m4x3 * m3x4);
        var m32m44m42m34 = (m3x2 * m4x4) - (m4x2 * m3x4);
        var m32m43m42m33 = (m3x2 * m4x3) - (m4x2 * m3x3);
        var m23m44m43m24 = (m2x3 * m4x4) - (m4x3 * m2x4);

        var m22m44m42m24 = (m2x2 * m4x4) - (m4x2 * m2x4);
        var m22m43m42m23 = (m2x2 * m4x3) - (m4x2 * m2x3);
        var m23m34m33m24 = (m2x3 * m3x4) - (m3x3 * m2x4);
        var m22m34m32m24 = (m2x2 * m3x4) - (m3x2 * m2x4);

        var m22m33m32m23 = (m2x2 * m3x3) - (m3x2 * m2x3);
        var m31m44m41m34 = (m3x1 * m4x4) - (m4x1 * m3x4);
        var m31m43m41m33 = (m3x1 * m4x3) - (m4x1 * m3x3);
        var m21m44m41m24 = (m2x1 * m4x4) - (m4x1 * m2x4);

        var m21m43m41m23 = (m2x1 * m4x3) - (m4x1 * m2x3);
        var m21m34m31m24 = (m2x1 * m3x4) - (m3x1 * m2x4);
        var m21m33m31m23 = (m2x1 * m3x3) - (m3x1 * m2x3);
        var m31m42m41m32 = (m3x1 * m4x2) - (m4x1 * m3x2);

        var m21m42m41m22 = (m2x1 * m4x2) - (m4x1 * m2x2);
        var m21m32m31m22 = (m2x1 * m3x2) - (m3x1 * m2x2);

        return (
            -((m2x2 * m33m44m43m34) - (m2x3 * m32m44m42m34) + (m2x4 * m32m43m42m33)), (m1x2 * m33m44m43m34) - (m1x3 * m32m44m42m34) + (m1x4 * m32m43m42m33), -((m1x2 * m23m44m43m24) - (m1x3 * m22m44m42m24) + (m1x4 * m22m43m42m23)), (m1x2 * m23m34m33m24) - (m1x3 * m22m34m32m24) + (m1x4 * m22m33m32m23),
            (m2x1 * m33m44m43m34) - (m2x3 * m31m44m41m34) + (m2x4 * m31m43m41m33), -((m1x1 * m33m44m43m34) - (m1x3 * m31m44m41m34) + (m1x4 * m31m43m41m33)), (m1x1 * m23m44m43m24) - (m1x3 * m21m44m41m24) + (m1x4 * m21m43m41m23), -((m1x1 * m23m34m33m24) - (m1x3 * m21m34m31m24) + (m1x4 * m21m33m31m23)),
            -((m2x1 * m32m44m42m34) - (m2x2 * m31m44m41m34) + (m2x4 * m31m42m41m32)), (m1x1 * m32m44m42m34) - (m1x2 * m31m44m41m34) + (m1x4 * m31m42m41m32), -((m1x1 * m22m44m42m24) - (m1x2 * m21m44m41m24) + (m1x4 * m21m42m41m22)), (m1x1 * m22m34m32m24) - (m1x2 * m21m34m31m24) + (m1x4 * m21m32m31m22),
            (m2x1 * m32m43m42m33) - (m2x2 * m31m43m41m33) + (m2x3 * m31m42m41m32), -((m1x1 * m32m43m42m33) - (m1x2 * m31m43m41m33) + (m1x3 * m31m42m41m32)), (m1x1 * m22m43m42m23) - (m1x2 * m21m43m41m23) + (m1x3 * m21m42m41m22), -((m1x1 * m22m33m32m23) - (m1x2 * m21m33m31m23) + (m1x3 * m21m32m31m22)));
    }

    /// <summary>
    /// Cofactors the specified M0X0.
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
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5)
        CofactorMatrix<T>(
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5)
        where T : INumber<T>
    {
        var m = Cofactor(new T[,]
            {
                {m1x1, m1x2, m1x3, m1x4, m1x5},
                {m2x1, m2x2, m2x3, m2x4, m2x5},
                {m3x1, m3x2, m3x3, m3x4, m3x5},
                {m4x1, m4x2, m4x3, m4x4, m4x5},
                {m5x1, m5x2, m5x3, m5x4, m5x5}
            });
        return (m[0, 0], m[0, 1], m[0, 2], m[0, 3], m[0, 4],
                m[1, 0], m[1, 1], m[1, 2], m[1, 3], m[1, 4],
                m[2, 0], m[2, 1], m[2, 2], m[2, 3], m[2, 4],
                m[3, 0], m[3, 1], m[3, 2], m[3, 3], m[3, 4],
                m[4, 0], m[4, 1], m[4, 2], m[4, 3], m[4, 4]);
    }

    /// <summary>
    /// Cofactors the matrix.
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
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6)
        CofactorMatrix<T>(
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6)
        where T : INumber<T>
    {
        var m = Cofactor(new T[,]
            {
                {m1x1, m1x2, m1x3, m1x4, m1x5, m1x6},
                {m2x1, m2x2, m2x3, m2x4, m2x5, m2x6},
                {m3x1, m3x2, m3x3, m3x4, m3x5, m3x6},
                {m4x1, m4x2, m4x3, m4x4, m4x5, m4x6},
                {m5x1, m5x2, m5x3, m5x4, m5x5, m5x6},
                {m6x1, m6x2, m6x3, m6x4, m6x5, m6x6}
            });
        return (m[0, 0], m[0, 1], m[0, 2], m[0, 3], m[0, 4], m[0, 5],
                m[1, 0], m[1, 1], m[1, 2], m[1, 3], m[1, 4], m[1, 5],
                m[2, 0], m[2, 1], m[2, 2], m[2, 3], m[2, 4], m[2, 5],
                m[3, 0], m[3, 1], m[3, 2], m[3, 3], m[3, 4], m[3, 5],
                m[4, 0], m[4, 1], m[4, 2], m[4, 3], m[4, 4], m[4, 5],
                m[5, 0], m[5, 1], m[5, 2], m[5, 3], m[5, 4], m[5, 5]);
    }
    #endregion Cofactor

    #region Inverse
    /// <summary>
    /// Function to calculate the inverse of the specified matrix.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <returns></returns>
    /// <exception cref="Exception">Singular matrix, can't find its inverse</exception>
    /// <acknowledgment>
    /// https://www.geeksforgeeks.org/adjoint-inverse-matrix/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T[,] Inverse<T>(T[,] matrix)
        where T : INumber<T>
    {
        // Find determinant of [,]A 
        var det = Determinant(matrix);
        if (det == T.Zero)
            throw new Exception("Singular matrix, can't find its inverse");

        // Find adjoint 
        var adj = Adjoint(matrix);

        var rows = matrix.GetLength(0);
        var cols = matrix.GetLength(1);
        var inverse = new T[rows, cols];

        // Find Inverse using formula "inverse(A) = adj(A)/det(A)" 
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                inverse[i, j] = adj[i, j] / det;
            }
        }

        return inverse;
    }

    /// <summary>
    /// Function to calculate the inverse of the specified matrix.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <returns></returns>
    /// <exception cref="Exception">Singular matrix, can't find its inverse</exception>
    /// <acknowledgment>
    /// https://www.geeksforgeeks.org/adjoint-inverse-matrix/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T[][] Inverse<T>(T[][] matrix)
        where T : INumber<T>
    {
        // Find determinant of [,]A 
        var det = Determinant(matrix);
        if (det == T.Zero)
            throw new Exception("Singular matrix, can't find its inverse");

        // Find adjoint 
        var adj = Adjoint(matrix);

        var rows = matrix.Length;
        var cols = matrix[0].Length;
        var inverse = new T[rows][];

        // Find Inverse using formula "inverse(A) = adj(A)/det(A)" 
        for (var i = 0; i < rows; i++)
        {
            inverse[i] = new T[cols];
            for (var j = 0; j < cols; j++)
            {
                inverse[i][j] = adj[i][j] / det;
            }
        }

        return inverse;
    }

    /// <summary>
    /// The invert.
    /// </summary>
    /// <param name="m1x1">The M0X0.</param>
    /// <param name="m1x2">The M0X1.</param>
    /// <param name="m2x1">The M1X0.</param>
    /// <param name="m2x2">The M1X1.</param>
    /// <returns>
    /// The <see cref="Matrix2x2{T}" />.
    /// </returns>
    /// <acknowledgment>
    /// https://sites.google.com/site/physics2d/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m1x1, T m1x2,
        T m2x1, T m2x2)
        InverseMatrix<T>(
        T m1x1, T m1x2,
        T m2x1, T m2x2)
        where T : INumber<T>
    {
        var detInv = T.One / ((m1x1 * m2x2) - (m1x2 * m2x1));
        return (
            detInv * m2x2, detInv * -m1x2,
            detInv * -m2x1, detInv * m1x1);
    }

    /// <summary>
    /// The invert.
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
    /// The <see cref="Matrix3x3{T}" />.
    /// </returns>
    /// <acknowledgment>
    /// https://sites.google.com/site/physics2d/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m1x1, T m1x2, T m1x3,
        T m2x1, T m2x2, T m2x3,
        T m3x1, T m3x2, T m3x3)
        InverseMatrix<T>(
        T m1x1, T m1x2, T m1x3,
        T m2x1, T m2x2, T m2x3,
        T m3x1, T m3x2, T m3x3)
        where T : INumber<T>
    {
        var m11m22m12m21 = (m2x2 * m3x3) - (m2x3 * m3x2);
        var m10m22m12m20 = (m2x1 * m3x3) - (m2x3 * m3x1);
        var m10m21m11m20 = (m2x1 * m3x2) - (m2x2 * m3x1);

        var detInv = T.One / ((m1x1 * m11m22m12m21) - (m1x2 * m10m22m12m20) + (m1x3 * m10m21m11m20));

        return (
            detInv * m11m22m12m21, detInv * (-((m1x2 * m3x3) - (m1x3 * m3x2))), detInv * ((m1x2 * m2x3) - (m1x3 * m2x2)),
            detInv * (-m10m22m12m20), detInv * ((m1x1 * m3x3) - (m1x3 * m3x1)), detInv * (-((m1x1 * m2x3) - (m1x3 * m2x1))),
            detInv * m10m21m11m20, detInv * (-((m1x1 * m3x2) - (m1x2 * m3x1))), detInv * ((m1x1 * m2x2) - (m1x2 * m2x1)));
    }

    /// <summary>
    /// The invert.
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
    /// The <see cref="Matrix4x4{T}" />.
    /// </returns>
    /// <acknowledgment>
    /// https://sites.google.com/site/physics2d/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m1x1, T m1x2, T m1x3, T m1x4,
        T m2x1, T m2x2, T m2x3, T m2x4,
        T m3x1, T m3x2, T m3x3, T m3x4,
        T m4x1, T m4x2, T m4x3, T m4x4)
        InverseMatrix<T>(
        T m1x1, T m1x2, T m1x3, T m1x4,
        T m2x1, T m2x2, T m2x3, T m2x4,
        T m3x1, T m3x2, T m3x3, T m3x4,
        T m4x1, T m4x2, T m4x3, T m4x4)
        where T : INumber<T>
    {
        var m22m33m32m23 = (m3x3 * m4x4) - (m4x3 * m3x4);
        var m21m33m31m23 = (m3x2 * m4x4) - (m4x2 * m3x4);
        var m21m32m31m22 = (m3x2 * m4x3) - (m4x2 * m3x3);

        var m12m33m32m13 = (m2x3 * m4x4) - (m4x3 * m2x4);
        var m11m33m31m13 = (m2x2 * m4x4) - (m4x2 * m2x4);
        var m11m32m31m12 = (m2x2 * m4x3) - (m4x2 * m2x3);

        var m12m23m22m13 = (m2x3 * m3x4) - (m3x3 * m2x4);
        var m11m23m21m13 = (m2x2 * m3x4) - (m3x2 * m2x4);
        var m11m22m21m12 = (m2x2 * m3x3) - (m3x2 * m2x3);

        var m20m33m30m23 = (m3x1 * m4x4) - (m4x1 * m3x4);
        var m20m32m30m22 = (m3x1 * m4x3) - (m4x1 * m3x3);
        var m10m33m30m13 = (m2x1 * m4x4) - (m4x1 * m2x4);

        var m10m32m30m12 = (m2x1 * m4x3) - (m4x1 * m2x3);
        var m10m23m20m13 = (m2x1 * m3x4) - (m3x1 * m2x4);
        var m10m22m20m12 = (m2x1 * m3x3) - (m3x1 * m2x3);

        var m20m31m30m21 = (m3x1 * m4x2) - (m4x1 * m3x2);
        var m10m31m30m11 = (m2x1 * m4x2) - (m4x1 * m2x2);
        var m10m21m20m11 = (m2x1 * m3x2) - (m3x1 * m2x2);

        var detInv = T.One /
        ((m1x1 * ((m2x2 * m22m33m32m23) - (m2x3 * m21m33m31m23) + (m2x4 * m21m32m31m22))) -
        (m1x2 * ((m2x1 * m22m33m32m23) - (m2x3 * m20m33m30m23) + (m2x4 * m20m32m30m22))) +
        (m1x3 * ((m2x1 * m21m33m31m23) - (m2x2 * m20m33m30m23) + (m2x4 * m20m31m30m21))) -
        (m1x4 * ((m2x1 * m21m32m31m22) - (m2x2 * m20m32m30m22) + (m2x3 * m20m31m30m21))));

        return (
            detInv * ((m2x2 * m22m33m32m23) - (m2x3 * m21m33m31m23) + (m2x4 * m21m32m31m22)), detInv * (-((m1x2 * m22m33m32m23) - (m1x3 * m21m33m31m23) + (m1x4 * m21m32m31m22))), detInv * ((m1x2 * m12m33m32m13) - (m1x3 * m11m33m31m13) + (m1x4 * m11m32m31m12)), detInv * (-((m1x2 * m12m23m22m13) - (m1x3 * m11m23m21m13) + (m1x4 * m11m22m21m12))),
            detInv * (-((m2x1 * m22m33m32m23) - (m2x3 * m20m33m30m23) + (m2x4 * m20m32m30m22))), detInv * ((m1x1 * m22m33m32m23) - (m1x3 * m20m33m30m23) + (m1x4 * m20m32m30m22)), detInv * (-((m1x1 * m12m33m32m13) - (m1x3 * m10m33m30m13) + (m1x4 * m10m32m30m12))), detInv * ((m1x1 * m12m23m22m13) - (m1x3 * m10m23m20m13) + (m1x4 * m10m22m20m12)),
            detInv * ((m2x1 * m21m33m31m23) - (m2x2 * m20m33m30m23) + (m2x4 * m20m31m30m21)), detInv * (-((m1x1 * m21m33m31m23) - (m1x2 * m20m33m30m23) + (m1x4 * m20m31m30m21))), detInv * ((m1x1 * m11m33m31m13) - (m1x2 * m10m33m30m13) + (m1x4 * m10m31m30m11)), detInv * (-((m1x1 * m11m23m21m13) - (m1x2 * m10m23m20m13) + (m1x4 * m10m21m20m11))),
            detInv * (-((m2x1 * m21m32m31m22) - (m2x2 * m20m32m30m22) + (m2x3 * m20m31m30m21))), detInv * ((m1x1 * m21m32m31m22) - (m1x2 * m20m32m30m22) + (m1x3 * m20m31m30m21)), detInv * (-((m1x1 * m11m32m31m12) - (m1x2 * m10m32m30m12) + (m1x3 * m10m31m30m11))), detInv * ((m1x1 * m11m22m21m12) - (m1x2 * m10m22m20m12) + (m1x3 * m10m21m20m11)));
    }

    /// <summary>
    /// Inverts the specified M0X0.
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
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5)
        InverseMatrix<T>(
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5)
        where T : INumber<T>
    {
        var m = Inverse(new T[,]
            {
                {m1x1, m1x2, m1x3, m1x4, m1x5},
                {m2x1, m2x2, m2x3, m2x4, m2x5},
                {m3x1, m3x2, m3x3, m3x4, m3x5},
                {m4x1, m4x2, m4x3, m4x4, m4x5},
                {m5x1, m5x2, m5x3, m5x4, m5x5}
            });
        return (m[0, 0], m[0, 1], m[0, 2], m[0, 3], m[0, 4],
                m[1, 0], m[1, 1], m[1, 2], m[1, 3], m[1, 4],
                m[2, 0], m[2, 1], m[2, 2], m[2, 3], m[2, 4],
                m[3, 0], m[3, 1], m[3, 2], m[3, 3], m[3, 4],
                m[4, 0], m[4, 1], m[4, 2], m[4, 3], m[4, 4]);
    }

    /// <summary>
    /// Inverts the matrix.
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
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6)
        InverseMatrix<T>(
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6)
        where T : INumber<T>
    {
        var m = Inverse(new T[,]
            {
                {m1x1, m1x2, m1x3, m1x4, m1x5, m1x6},
                {m2x1, m2x2, m2x3, m2x4, m2x5, m2x6},
                {m3x1, m3x2, m3x3, m3x4, m3x5, m3x6},
                {m4x1, m4x2, m4x3, m4x4, m4x5, m4x6},
                {m5x1, m5x2, m5x3, m5x4, m5x5, m5x6},
                {m6x1, m6x2, m6x3, m6x4, m6x5, m6x6}
            });
        return (m[0, 0], m[0, 1], m[0, 2], m[0, 3], m[0, 4], m[0, 5],
                m[1, 0], m[1, 1], m[1, 2], m[1, 3], m[1, 4], m[1, 5],
                m[2, 0], m[2, 1], m[2, 2], m[2, 3], m[2, 4], m[2, 5],
                m[3, 0], m[3, 1], m[3, 2], m[3, 3], m[3, 4], m[3, 5],
                m[4, 0], m[4, 1], m[4, 2], m[4, 3], m[4, 4], m[4, 5],
                m[5, 0], m[5, 1], m[5, 2], m[5, 3], m[5, 4], m[5, 5]);
    }
    #endregion Invert

    #region Determinant
    /// <summary>
    /// Recursive function for finding determinant of a matrix.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// https://www.answers.com/Q/Determinant_of_matrix_in_java
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Determinant<T>(T[,] matrix)
        where T : INumber<T>
    //{
    //    var result = T.Zero;
    //    if (matrix.GetLength(0) == 2)
    //    {
    //        result = matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
    //        return result;
    //    }
    //    for (var i = 0; i < matrix.GetLength(1); i++)
    //    {
    //        var temp = new double[matrix.Length - 1, matrix.GetLength(1) - 1];
    //        for (var j = 1; j < matrix.Length; j++)
    //        {
    //            Array.Copy(matrix, 0, temp[j - 1], 0, i);
    //            Array.Copy(matrix, i + 1, temp[j - 1], i, matrix.GetLength(1) - i - 1);
    //        }

    //        result += matrix[0, i] * Math.Pow(-1, i) * Determinant(temp);
    //    }

    //    return result;
    //}
    => Determinant(matrix.ToJaggedArray()); // Convert to jagged array until the above code can be fixed.

    /// <summary>
    /// Recursive function for finding determinant of a matrix.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// https://www.answers.com/Q/Determinant_of_matrix_in_java
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Determinant<T>(T[][] matrix)
        where T : INumber<T>
    {
        var result = T.Zero;
        if (matrix.Length == 2)
        {
            result = matrix[0][0] * matrix[1][1] - matrix[0][1] * matrix[1][0];
            return result;
        }
        for (var i = 0; i < matrix[0].Length; i++)
        {
            var temp = new T[matrix.Length - 1][];
            for (var j = 1; j < matrix.Length; j++)
            {
                temp[j - 1] = new T[matrix[j].Length - 1];
                Array.Copy(matrix[j], 0, temp[j - 1], 0, i);
                Array.Copy(matrix[j], i + 1, temp[j - 1], i, matrix[j].Length - i - 1);
            }

            result += matrix[0][i] * T.CreateChecked(Math.Pow(-1d, i)) * Determinant(temp);
        }

        return result;
    }

    /// <summary>
    /// Find the determinant of a 2 by 2 matrix.
    /// </summary>
    /// <param name="m1x1">The M1X1.</param>
    /// <param name="m1x2">The M1X2.</param>
    /// <param name="m2x1">The M2X1.</param>
    /// <param name="m2x2">The M2X2.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// https://github.com/onlyuser/Legacy/blob/master/msvb/Dex3d/Math.bas
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T MatrixDeterminant<T>(
        T m1x1, T m1x2,
        T m2x1, T m2x2)
        where T : INumber<T>
        => (m1x1 * m2x2)
          - (m1x2 * m2x1);

    /// <summary>
    /// Find the determinant of a 3 by 3 matrix.
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
    /// <returns></returns>
    /// <acknowledgment>
    /// https://sites.google.com/site/physics2d/
    /// This is an expanded version of the Ogre determinant() method, to give better performance in C#. Generated using a script.
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T MatrixDeterminant<T>(
        T m1x1, T m1x2, T m1x3,
        T m2x1, T m2x2, T m2x3,
        T m3x1, T m3x2, T m3x3)
        where T : INumber<T>
        => (m1x1 * ((m2x2 * m3x3) - (m2x3 * m3x2)))
         - (m1x2 * ((m2x1 * m3x3) - (m2x3 * m3x1)))
         + (m1x3 * ((m2x1 * m3x2) - (m2x2 * m3x1)));

    /// <summary>
    /// Find the determinant of a 4 by 4 matrix.
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
    /// <returns></returns>
    /// <acknowledgment>
    /// https://sites.google.com/site/physics2d/
    /// This is an expanded version of the Ogre determinant() method, to give better performance in C#. Generated using a script.
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T MatrixDeterminant<T>(
        T m1x1, T m1x2, T m1x3, T m1x4,
        T m2x1, T m2x2, T m2x3, T m2x4,
        T m3x1, T m3x2, T m3x3, T m3x4,
        T m4x1, T m4x2, T m4x3, T m4x4)
        where T : INumber<T>
        => (m1x1 * ((m2x2 * ((m3x3 * m4x4) - (m4x3 * m3x4))) - (m2x3 * ((m3x2 * m4x4) - (m4x2 * m3x4))) + (m2x4 * ((m3x2 * m4x3) - (m4x2 * m3x3)))))
         - (m1x2 * ((m2x1 * ((m3x3 * m4x4) - (m4x3 * m3x4))) - (m2x3 * ((m3x1 * m4x4) - (m4x1 * m3x4))) + (m2x4 * ((m3x1 * m4x3) - (m4x1 * m3x3)))))
         + (m1x3 * ((m2x1 * ((m3x2 * m4x4) - (m4x2 * m3x4))) - (m2x2 * ((m3x1 * m4x4) - (m4x1 * m3x4))) + (m2x4 * ((m3x1 * m4x2) - (m4x1 * m3x2)))))
         - (m1x4 * ((m2x1 * ((m3x2 * m4x3) - (m4x2 * m3x3))) - (m2x2 * ((m3x1 * m4x3) - (m4x1 * m3x3))) + (m2x3 * ((m3x1 * m4x2) - (m4x1 * m3x2)))));

    /// <summary>
    /// Find the determinant of a 5 by 5 matrix.
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
    /// <returns></returns>
    /// <acknowledgment>
    /// https://github.com/onlyuser/Legacy/blob/master/msvb/Dex3d/Math.bas
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T MatrixDeterminant<T>(
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5)
        where T : INumber<T>
        => (m1x1 * MatrixDeterminant(m2x2, m2x3, m2x4, m2x5, m3x2, m3x3, m3x4, m3x5, m4x2, m4x3, m4x4, m4x5, m5x2, m5x3, m5x4, m5x5))
         - (m1x2 * MatrixDeterminant(m2x1, m2x3, m2x4, m2x5, m3x1, m3x3, m3x4, m3x5, m4x1, m4x3, m4x4, m4x5, m5x1, m5x3, m5x4, m5x5))
         + (m1x3 * MatrixDeterminant(m2x1, m2x2, m2x4, m2x5, m3x1, m3x2, m3x4, m3x5, m4x1, m4x2, m4x4, m4x5, m5x1, m5x2, m5x4, m5x5))
         - (m1x4 * MatrixDeterminant(m2x1, m2x2, m2x3, m2x5, m3x1, m3x2, m3x3, m3x5, m4x1, m4x2, m4x3, m4x5, m5x1, m5x2, m5x3, m5x5))
         + (m1x5 * MatrixDeterminant(m2x1, m2x2, m2x3, m2x4, m3x1, m3x2, m3x3, m3x4, m4x1, m4x2, m4x3, m4x4, m5x1, m5x2, m5x3, m5x4));

    /// <summary>
    /// Find the determinant of a 6 by 6 matrix.
    /// </summary>
    /// <param name="m1x1">a.</param>
    /// <param name="m1x2">The b.</param>
    /// <param name="m1x3">The c.</param>
    /// <param name="m1x4">The d.</param>
    /// <param name="m1x5">The e.</param>
    /// <param name="m1x6">The f.</param>
    /// <param name="m2x1">The g.</param>
    /// <param name="m2x2">The h.</param>
    /// <param name="m2x3">The i.</param>
    /// <param name="m2x4">The j.</param>
    /// <param name="m2x5">The k.</param>
    /// <param name="m2x6">The l.</param>
    /// <param name="m3x1">The m.</param>
    /// <param name="m3x2">The n.</param>
    /// <param name="m3x3">The o.</param>
    /// <param name="m3x4">The p.</param>
    /// <param name="m3x5">The q.</param>
    /// <param name="m3x6">The r.</param>
    /// <param name="m4x1">The s.</param>
    /// <param name="m4x2">The t.</param>
    /// <param name="m4x3">The u.</param>
    /// <param name="m4x4">The v.</param>
    /// <param name="m4x5">The w.</param>
    /// <param name="m4x6">The x.</param>
    /// <param name="m5x1">The y.</param>
    /// <param name="m5x2">The z.</param>
    /// <param name="m5x3">The aa.</param>
    /// <param name="m5x4">The bb.</param>
    /// <param name="m5x5">The cc.</param>
    /// <param name="m5x6">The dd.</param>
    /// <param name="m6x1">The ee.</param>
    /// <param name="m6x2">The ff.</param>
    /// <param name="m6x3">The gg.</param>
    /// <param name="m6x4">The hh.</param>
    /// <param name="m6x5">The ii.</param>
    /// <param name="m6x6">The jj.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// https://github.com/onlyuser/Legacy/blob/master/msvb/Dex3d/Math.bas
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T MatrixDeterminant<T>(
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6)
        where T : INumber<T>
        => (m1x1 * MatrixDeterminant(m2x2, m2x3, m2x4, m2x5, m2x6, m3x2, m3x3, m3x4, m3x5, m3x6, m4x2, m4x3, m4x4, m4x5, m4x6, m5x2, m5x3, m5x4, m5x5, m5x6, m6x2, m6x3, m6x4, m6x5, m6x6))
         - (m1x2 * MatrixDeterminant(m2x1, m2x3, m2x4, m2x5, m2x6, m3x1, m3x3, m3x4, m3x5, m3x6, m4x1, m4x3, m4x4, m4x5, m4x6, m5x1, m5x3, m5x4, m5x5, m5x6, m6x1, m6x3, m6x4, m6x5, m6x6))
         + (m1x3 * MatrixDeterminant(m2x1, m2x2, m2x4, m2x5, m2x6, m3x1, m3x2, m3x4, m3x5, m3x6, m4x1, m4x2, m4x4, m4x5, m4x6, m5x1, m5x2, m5x4, m5x5, m5x6, m6x1, m6x2, m6x4, m6x5, m6x6))
         - (m1x4 * MatrixDeterminant(m2x1, m2x2, m2x3, m2x5, m2x6, m3x1, m3x2, m3x3, m3x5, m3x6, m4x1, m4x2, m4x3, m4x5, m4x6, m5x1, m5x2, m5x3, m5x5, m5x6, m6x1, m6x2, m6x3, m6x5, m6x6))
         + (m1x5 * MatrixDeterminant(m2x1, m2x2, m2x3, m2x4, m2x6, m3x1, m3x2, m3x3, m3x4, m3x6, m4x1, m4x2, m4x3, m4x4, m4x6, m5x1, m5x2, m5x3, m5x4, m5x6, m6x1, m6x2, m6x3, m6x4, m6x6))
         - (m1x6 * MatrixDeterminant(m2x1, m2x2, m2x3, m2x4, m2x5, m3x1, m3x2, m3x3, m3x4, m3x5, m4x1, m4x2, m4x3, m4x4, m4x5, m5x1, m5x2, m5x3, m5x4, m5x5, m6x1, m6x2, m6x3, m6x4, m6x5));
    #endregion Determinant

    #region Inverse Determinant
    /// <summary>
    /// Find the inverse of the determinant of a 2 by 2 matrix.
    /// </summary>
    /// <param name="m1x1">a.</param>
    /// <param name="m1x2">The b.</param>
    /// <param name="m2x1">The c.</param>
    /// <param name="m2x2">The d.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T MatrixInverseDeterminant<T>(
        T m1x1, T m1x2,
        T m2x1, T m2x2)
        where T : IFloatingPointIeee754<T>
        => T.One / ((m1x1 * m2x2)
          - (m1x2 * m2x1));

    /// <summary>
    /// Find the inverse of the determinant of a 3 by 3 matrix.
    /// </summary>
    /// <param name="m1x1">a.</param>
    /// <param name="m1x2">The b.</param>
    /// <param name="m1x3">The c.</param>
    /// <param name="m2x1">The d.</param>
    /// <param name="m2x2">The e.</param>
    /// <param name="m2x3">The f.</param>
    /// <param name="m3x1">The g.</param>
    /// <param name="m3x2">The h.</param>
    /// <param name="m3x3">The i.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// https://github.com/onlyuser/Legacy/blob/master/msvb/Dex3d/Math.bas
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T MatrixInverseDeterminant<T>(
        T m1x1, T m1x2, T m1x3,
        T m2x1, T m2x2, T m2x3,
        T m3x1, T m3x2, T m3x3)
        where T : IFloatingPointIeee754<T>
        => T.One / ((m1x1 * MatrixDeterminant(m2x2, m2x3, m3x2, m3x3))
          - (m1x2 * MatrixDeterminant(m2x1, m2x3, m3x1, m3x3))
          + (m1x3 * MatrixDeterminant(m2x1, m2x2, m3x1, m3x2)));

    /// <summary>
    /// Find the inverse of the determinant of a 4 by 4 matrix.
    /// </summary>
    /// <param name="m1x1">a.</param>
    /// <param name="m1x2">The b.</param>
    /// <param name="m1x3">The c.</param>
    /// <param name="m1x4">The d.</param>
    /// <param name="m2x1">The e.</param>
    /// <param name="m2x2">The f.</param>
    /// <param name="m2x3">The g.</param>
    /// <param name="m2x4">The h.</param>
    /// <param name="m3x1">The i.</param>
    /// <param name="m3x2">The j.</param>
    /// <param name="m3x3">The k.</param>
    /// <param name="m3x4">The l.</param>
    /// <param name="m4x1">The m.</param>
    /// <param name="m4x2">The n.</param>
    /// <param name="m4x3">The o.</param>
    /// <param name="m4x4">The p.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// https://github.com/onlyuser/Legacy/blob/master/msvb/Dex3d/Math.bas
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T MatrixInverseDeterminant<T>(
        T m1x1, T m1x2, T m1x3, T m1x4,
        T m2x1, T m2x2, T m2x3, T m2x4,
        T m3x1, T m3x2, T m3x3, T m3x4,
        T m4x1, T m4x2, T m4x3, T m4x4)
        where T : IFloatingPointIeee754<T>
        => T.One / ((m1x1 * MatrixDeterminant(m2x2, m2x3, m2x4, m3x2, m3x3, m3x4, m4x2, m4x3, m4x4))
          - (m1x2 * MatrixDeterminant(m2x1, m2x3, m2x4, m3x1, m3x3, m3x4, m4x1, m4x3, m4x4))
          + (m1x3 * MatrixDeterminant(m2x1, m2x2, m2x4, m3x1, m3x2, m3x4, m4x1, m4x2, m4x4))
          - (m1x4 * MatrixDeterminant(m2x1, m2x2, m2x3, m3x1, m3x2, m3x3, m4x1, m4x2, m4x3)));

    /// <summary>
    /// Find the inverse of the determinant of a 5 by 5 matrix.
    /// </summary>
    /// <param name="m1x1">a.</param>
    /// <param name="m1x2">The b.</param>
    /// <param name="m1x3">The c.</param>
    /// <param name="m1x4">The d.</param>
    /// <param name="m1x5">The e.</param>
    /// <param name="m2x1">The f.</param>
    /// <param name="m2x2">The g.</param>
    /// <param name="m2x3">The h.</param>
    /// <param name="m2x4">The i.</param>
    /// <param name="m2x5">The j.</param>
    /// <param name="m3x1">The k.</param>
    /// <param name="m3x2">The l.</param>
    /// <param name="m3x3">The m.</param>
    /// <param name="m3x4">The n.</param>
    /// <param name="m3x5">The o.</param>
    /// <param name="m4x1">The p.</param>
    /// <param name="m4x2">The q.</param>
    /// <param name="m4x3">The r.</param>
    /// <param name="m4x4">The s.</param>
    /// <param name="m4x5">The t.</param>
    /// <param name="m5x1">The u.</param>
    /// <param name="m5x2">The v.</param>
    /// <param name="m5x3">The w.</param>
    /// <param name="m5x4">The x.</param>
    /// <param name="m5x5">The y.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T MatrixInverseDeterminant<T>(
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5)
        where T : IFloatingPointIeee754<T>
        => T.One / ((m1x1 * MatrixDeterminant(m2x2, m2x3, m2x4, m2x5, m3x2, m3x3, m3x4, m3x5, m4x2, m4x3, m4x4, m4x5, m5x2, m5x3, m5x4, m5x5))
          - (m1x2 * MatrixDeterminant(m2x1, m2x3, m2x4, m2x5, m3x1, m3x3, m3x4, m3x5, m4x1, m4x3, m4x4, m4x5, m5x1, m5x3, m5x4, m5x5))
          + (m1x3 * MatrixDeterminant(m2x1, m2x2, m2x4, m2x5, m3x1, m3x2, m3x4, m3x5, m4x1, m4x2, m4x4, m4x5, m5x1, m5x2, m5x4, m5x5))
          - (m1x4 * MatrixDeterminant(m2x1, m2x2, m2x3, m2x5, m3x1, m3x2, m3x3, m3x5, m4x1, m4x2, m4x3, m4x5, m5x1, m5x2, m5x3, m5x5))
          + (m1x5 * MatrixDeterminant(m2x1, m2x2, m2x3, m2x4, m3x1, m3x2, m3x3, m3x4, m4x1, m4x2, m4x3, m4x4, m5x1, m5x2, m5x3, m5x4)));

    /// <summary>
    /// Find the inverse of the determinant of a 6 by 6 matrix.
    /// </summary>
    /// <param name="m1x1">a.</param>
    /// <param name="m1x2">The b.</param>
    /// <param name="m1x3">The c.</param>
    /// <param name="m1x4">The d.</param>
    /// <param name="m1x5">The e.</param>
    /// <param name="m1x6">The f.</param>
    /// <param name="m2x1">The g.</param>
    /// <param name="m2x2">The h.</param>
    /// <param name="m2x3">The i.</param>
    /// <param name="m2x4">The j.</param>
    /// <param name="m2x5">The k.</param>
    /// <param name="m2x6">The l.</param>
    /// <param name="m3x1">The m.</param>
    /// <param name="m3x2">The n.</param>
    /// <param name="m3x3">The o.</param>
    /// <param name="m3x4">The p.</param>
    /// <param name="m3x5">The q.</param>
    /// <param name="m3x6">The r.</param>
    /// <param name="m4x1">The s.</param>
    /// <param name="m4x2">The t.</param>
    /// <param name="m4x3">The u.</param>
    /// <param name="m4x4">The v.</param>
    /// <param name="m4x5">The w.</param>
    /// <param name="m4x6">The x.</param>
    /// <param name="m5x1">The y.</param>
    /// <param name="m5x2">The z.</param>
    /// <param name="m5x3">The aa.</param>
    /// <param name="m5x4">The bb.</param>
    /// <param name="m5x5">The cc.</param>
    /// <param name="m5x6">The dd.</param>
    /// <param name="m6x1">The ee.</param>
    /// <param name="m6x2">The ff.</param>
    /// <param name="m6x3">The gg.</param>
    /// <param name="m6x4">The hh.</param>
    /// <param name="m6x5">The ii.</param>
    /// <param name="m6x6">The jj.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T MatrixInverseDeterminant<T>(
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6)
        where T : IFloatingPointIeee754<T>
        => T.One / ((m1x1 * MatrixDeterminant(m2x2, m2x3, m2x4, m2x5, m2x6, m3x2, m3x3, m3x4, m3x5, m3x6, m4x2, m4x3, m4x4, m4x5, m4x6, m5x2, m5x3, m5x4, m5x5, m5x6, m6x2, m6x3, m6x4, m6x5, m6x6))
          - (m1x2 * MatrixDeterminant(m2x1, m2x3, m2x4, m2x5, m2x6, m3x1, m3x3, m3x4, m3x5, m3x6, m4x1, m4x3, m4x4, m4x5, m4x6, m5x1, m5x3, m5x4, m5x5, m5x6, m6x1, m6x3, m6x4, m6x5, m6x6))
          + (m1x3 * MatrixDeterminant(m2x1, m2x2, m2x4, m2x5, m2x6, m3x1, m3x2, m3x4, m3x5, m3x6, m4x1, m4x2, m4x4, m4x5, m4x6, m5x1, m5x2, m5x4, m5x5, m5x6, m6x1, m6x2, m6x4, m6x5, m6x6))
          - (m1x4 * MatrixDeterminant(m2x1, m2x2, m2x3, m2x5, m2x6, m3x1, m3x2, m3x3, m3x5, m3x6, m4x1, m4x2, m4x3, m4x5, m4x6, m5x1, m5x2, m5x3, m5x5, m5x6, m6x1, m6x2, m6x3, m6x5, m6x6))
          + (m1x5 * MatrixDeterminant(m2x1, m2x2, m2x3, m2x4, m2x6, m3x1, m3x2, m3x3, m3x4, m3x6, m4x1, m4x2, m4x3, m4x4, m4x6, m5x1, m5x2, m5x3, m5x4, m5x6, m6x1, m6x2, m6x3, m6x4, m6x6))
          - (m1x6 * MatrixDeterminant(m2x1, m2x2, m2x3, m2x4, m2x5, m3x1, m3x2, m3x3, m3x4, m3x5, m4x1, m4x2, m4x3, m4x4, m4x5, m5x1, m5x2, m5x3, m5x4, m5x5, m6x1, m6x2, m6x3, m6x4, m6x5)));
    #endregion Inverse Determinant

    #region Lerp
    /// <summary>
    /// Lerps the specified a.
    /// </summary>
    /// <param name="a">a.</param>
    /// <param name="b">The b.</param>
    /// <param name="amount">The amount.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T[,] Lerp<T>(T[,] a, T[,] b, T amount)
        where T : INumber<T>
    {
        var aRows = a.GetLength(0);
        var bRows = b.GetLength(0);
        var aCols = a.GetLength(1);
        var bCols = b.GetLength(1);
        if (aRows != bRows || aCols != bCols) throw new Exception();

        var results = new T[aRows, bRows];
        for (var i = 0; i < aRows; i++)
        {
            for (var j = 0; j < aCols; j++)
            {
                results[i, j] = a[i, j] + ((b[i, j] - a[i, j]) * amount);
            }
        }

        return results;
    }

    /// <summary>
    /// Lerps the specified a.
    /// </summary>
    /// <param name="a">a.</param>
    /// <param name="b">The b.</param>
    /// <param name="amount">The amount.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T[][] Lerp<T>(T[][] a, T[][] b, T amount)
        where T : INumber<T>
    {
        var aRows = a.Length;
        var bRows = b[0].Length;
        var aCols = a.Length;
        var bCols = b[0].Length;
        if (aRows != bRows || aCols != bCols) throw new Exception();

        var results = new T[aRows][];
        for (var i = 0; i < aRows; i++)
        {
            results[i] = new T[bRows];
            for (var j = 0; j < aCols; j++)
            {
                results[i][j] = a[i][j] + ((b[i][j] - a[i][j]) * amount);
            }
        }

        return results;
    }

    /// <summary>
    /// Linearly interpolates between the corresponding values of two matrices.
    /// </summary>
    /// <param name="m1x1A">The M1X1 a.</param>
    /// <param name="m1x2A">The M1X2 a.</param>
    /// <param name="m2x1A">The M2X1 a.</param>
    /// <param name="m2x2A">The M2X2 a.</param>
    /// <param name="m1x1B">The M1X1 b.</param>
    /// <param name="m1x2B">The M1X2 b.</param>
    /// <param name="m2x1B">The M2X1 b.</param>
    /// <param name="m2x2B">The M2X2 b.</param>
    /// <param name="amount">The relative weight of the second source matrix.</param>
    /// <returns>
    /// The interpolated matrix.
    /// </returns>
    /// <acknowledgment>
    /// https://referencesource.microsoft.com/#System.Numerics/System/Numerics/Matrix4x4.cs,48ce53b7e55d0436
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m1x1, T m1x2,
        T m2x1, T m2x2)
        LerpMatrix2x2<T>(
        T m1x1A, T m1x2A,
        T m2x1A, T m2x2A,
        T m1x1B, T m1x2B,
        T m2x1B, T m2x2B,
        T amount)
        where T : INumber<T> => (
            // First row
            m1x1A + ((m1x1B - m1x1A) * amount),
            m1x2A + ((m1x2B - m1x2A) * amount),

            // Second row
            m2x1A + ((m2x1B - m2x1A) * amount),
            m2x2A + ((m2x2B - m2x2A) * amount)
        );

    /// <summary>
    /// Linearly interpolates between the corresponding values of two matrices.
    /// </summary>
    /// <param name="m1x1A">The M1X1 a.</param>
    /// <param name="m1x2A">The M1X2 a.</param>
    /// <param name="m1x3A">The M1X3 a.</param>
    /// <param name="m2x1A">The M2X1 a.</param>
    /// <param name="m2x2A">The M2X2 a.</param>
    /// <param name="m2x3A">The M2X3 a.</param>
    /// <param name="m3x1A">The M3X1 a.</param>
    /// <param name="m3x2A">The M3X2 a.</param>
    /// <param name="m3x3A">The M3X3 a.</param>
    /// <param name="m1x1B">The M1X1 b.</param>
    /// <param name="m1x2B">The M1X2 b.</param>
    /// <param name="m1x3B">The M1X3 b.</param>
    /// <param name="m2x1B">The M2X1 b.</param>
    /// <param name="m2x2B">The M2X2 b.</param>
    /// <param name="m2x3B">The M2X3 b.</param>
    /// <param name="m3x1B">The M3X1 b.</param>
    /// <param name="m3x2B">The M3X2 b.</param>
    /// <param name="m3x3B">The M3X3 b.</param>
    /// <param name="amount">The relative weight of the second source matrix.</param>
    /// <returns>
    /// The interpolated matrix.
    /// </returns>
    /// <acknowledgment>
    /// https://referencesource.microsoft.com/#System.Numerics/System/Numerics/Matrix4x4.cs,48ce53b7e55d0436
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m1x1, T m1x2, T m1x3,
        T m2x1, T m2x2, T m2x3,
        T m3x1, T m3x2, T m3x3)
        LerpMatrix3x3<T>(
        T m1x1A, T m1x2A, T m1x3A,
        T m2x1A, T m2x2A, T m2x3A,
        T m3x1A, T m3x2A, T m3x3A,
        T m1x1B, T m1x2B, T m1x3B,
        T m2x1B, T m2x2B, T m2x3B,
        T m3x1B, T m3x2B, T m3x3B,
        T amount)
        where T : INumber<T> => (
            // First row
            m1x1A + ((m1x1B - m1x1A) * amount),
            m1x2A + ((m1x2B - m1x2A) * amount),
            m1x3A + ((m1x3B - m1x3A) * amount),

            // Second row
            m2x1A + ((m2x1B - m2x1A) * amount),
            m2x2A + ((m2x2B - m2x2A) * amount),
            m2x3A + ((m2x3B - m2x3A) * amount),

            // Third row
            m3x1A + ((m3x1B - m3x1A) * amount),
            m3x2A + ((m3x2B - m3x2A) * amount),
            m3x3A + ((m3x3B - m3x3A) * amount)
        );

    /// <summary>
    /// Linearly interpolates between the corresponding values of two matrices.
    /// </summary>
    /// <param name="m1x1A">The M1X1 a.</param>
    /// <param name="m1x2A">The M1X2 a.</param>
    /// <param name="m1x3A">The M1X3 a.</param>
    /// <param name="m1x4A">The M1X4 a.</param>
    /// <param name="m2x1A">The M2X1 a.</param>
    /// <param name="m2x2A">The M2X2 a.</param>
    /// <param name="m2x3A">The M2X3 a.</param>
    /// <param name="m2x4A">The M2X4 a.</param>
    /// <param name="m3x1A">The M3X1 a.</param>
    /// <param name="m3x2A">The M3X2 a.</param>
    /// <param name="m3x3A">The M3X3 a.</param>
    /// <param name="m3x4A">The M3X4 a.</param>
    /// <param name="m4x1A">The M4X1 a.</param>
    /// <param name="m4x2A">The M4X2 a.</param>
    /// <param name="m4x3A">The M4X3 a.</param>
    /// <param name="m4x4A">The M4X4 a.</param>
    /// <param name="m1x1B">The M1X1 b.</param>
    /// <param name="m1x2B">The M1X2 b.</param>
    /// <param name="m1x3B">The M1X3 b.</param>
    /// <param name="m1x4B">The M1X4 b.</param>
    /// <param name="m2x1B">The M2X1 b.</param>
    /// <param name="m2x2B">The M2X2 b.</param>
    /// <param name="m2x3B">The M2X3 b.</param>
    /// <param name="m2x4B">The M2X4 b.</param>
    /// <param name="m3x1B">The M3X1 b.</param>
    /// <param name="m3x2B">The M3X2 b.</param>
    /// <param name="m3x3B">The M3X3 b.</param>
    /// <param name="m3x4B">The M3X4 b.</param>
    /// <param name="m4x1B">The M4X1 b.</param>
    /// <param name="m4x2B">The M4X2 b.</param>
    /// <param name="m4x3B">The M4X3 b.</param>
    /// <param name="m4x4B">The M4X4 b.</param>
    /// <param name="amount">The relative weight of the second source matrix.</param>
    /// <returns>
    /// The interpolated matrix.
    /// </returns>
    /// <acknowledgment>
    /// https://referencesource.microsoft.com/#System.Numerics/System/Numerics/Matrix4x4.cs,48ce53b7e55d0436
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m1x1, T m1x2, T m1x3, T m1x4,
        T m2x1, T m2x2, T m2x3, T m2x4,
        T m3x1, T m3x2, T m3x3, T m3x4,
        T m4x1, T m4x2, T m4x3, T m4x4)
        LerpMatrix4x4<T>(
        T m1x1A, T m1x2A, T m1x3A, T m1x4A,
        T m2x1A, T m2x2A, T m2x3A, T m2x4A,
        T m3x1A, T m3x2A, T m3x3A, T m3x4A,
        T m4x1A, T m4x2A, T m4x3A, T m4x4A,
        T m1x1B, T m1x2B, T m1x3B, T m1x4B,
        T m2x1B, T m2x2B, T m2x3B, T m2x4B,
        T m3x1B, T m3x2B, T m3x3B, T m3x4B,
        T m4x1B, T m4x2B, T m4x3B, T m4x4B,
        T amount)
        where T : INumber<T> => (
            // First row
            m1x1A + ((m1x1B - m1x1A) * amount),
            m1x2A + ((m1x2B - m1x2A) * amount),
            m1x3A + ((m1x3B - m1x3A) * amount),
            m1x4A + ((m1x4B - m1x4A) * amount),

            // Second row
            m2x1A + ((m2x1B - m2x1A) * amount),
            m2x2A + ((m2x2B - m2x2A) * amount),
            m2x3A + ((m2x3B - m2x3A) * amount),
            m2x4A + ((m2x4B - m2x4A) * amount),

            // Third row
            m3x1A + ((m3x1B - m3x1A) * amount),
            m3x2A + ((m3x2B - m3x2A) * amount),
            m3x3A + ((m3x3B - m3x3A) * amount),
            m3x4A + ((m3x4B - m3x4A) * amount),

            // Fourth row
            m4x1A + ((m4x1B - m4x1A) * amount),
            m4x2A + ((m4x2B - m4x2A) * amount),
            m4x3A + ((m4x3B - m4x3A) * amount),
            m4x4A + ((m4x4B - m4x4A) * amount)
        );

    /// <summary>
    /// Linearly interpolates between the corresponding values of two matrices.
    /// </summary>
    /// <param name="m1x1A">The M1X1 a.</param>
    /// <param name="m1x2A">The M1X2 a.</param>
    /// <param name="m1x3A">The M1X3 a.</param>
    /// <param name="m1x4A">The M1X4 a.</param>
    /// <param name="m1x5A">The M1X5 a.</param>
    /// <param name="m2x1A">The M2X1 a.</param>
    /// <param name="m2x2A">The M2X2 a.</param>
    /// <param name="m2x3A">The M2X3 a.</param>
    /// <param name="m2x4A">The M2X4 a.</param>
    /// <param name="m2x5A">The M2X5 a.</param>
    /// <param name="m3x1A">The M3X1 a.</param>
    /// <param name="m3x2A">The M3X2 a.</param>
    /// <param name="m3x3A">The M3X3 a.</param>
    /// <param name="m3x4A">The M3X4 a.</param>
    /// <param name="m3x5A">The M3X5 a.</param>
    /// <param name="m4x1A">The M4X1 a.</param>
    /// <param name="m4x2A">The M4X2 a.</param>
    /// <param name="m4x3A">The M4X3 a.</param>
    /// <param name="m4x4A">The M4X4 a.</param>
    /// <param name="m4x5A">The M4X5 a.</param>
    /// <param name="m5x1A">The M5X1 a.</param>
    /// <param name="m5x2A">The M5X2 a.</param>
    /// <param name="m5x3A">The M5X3 a.</param>
    /// <param name="m5x4A">The M5X4 a.</param>
    /// <param name="m5x5A">The M5X5 a.</param>
    /// <param name="m1x1B">The M1X1 b.</param>
    /// <param name="m1x2B">The M1X2 b.</param>
    /// <param name="m1x3B">The M1X3 b.</param>
    /// <param name="m1x4B">The M1X4 b.</param>
    /// <param name="m1x5B">The M1X5 b.</param>
    /// <param name="m2x1B">The M2X1 b.</param>
    /// <param name="m2x2B">The M2X2 b.</param>
    /// <param name="m2x3B">The M2X3 b.</param>
    /// <param name="m2x4B">The M2X4 b.</param>
    /// <param name="m2x5B">The M2X5 b.</param>
    /// <param name="m3x1B">The M3X1 b.</param>
    /// <param name="m3x2B">The M3X2 b.</param>
    /// <param name="m3x3B">The M3X3 b.</param>
    /// <param name="m3x4B">The M3X4 b.</param>
    /// <param name="m3x5B">The M3X5 b.</param>
    /// <param name="m4x1B">The M4X1 b.</param>
    /// <param name="m4x2B">The M4X2 b.</param>
    /// <param name="m4x3B">The M4X3 b.</param>
    /// <param name="m4x4B">The M4X4 b.</param>
    /// <param name="m4x5B">The M4X5 b.</param>
    /// <param name="m5x1B">The M5X1 b.</param>
    /// <param name="m5x2B">The M5X2 b.</param>
    /// <param name="m5x3B">The M5X3 b.</param>
    /// <param name="m5x4B">The M5X4 b.</param>
    /// <param name="m5x5B">The M5X5 b.</param>
    /// <param name="amount">The relative weight of the second source matrix.</param>
    /// <returns>
    /// The interpolated matrix.
    /// </returns>
    /// <acknowledgment>
    /// https://referencesource.microsoft.com/#System.Numerics/System/Numerics/Matrix4x4.cs,48ce53b7e55d0436
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5)
        LerpMatrix5x5<T>(
        T m1x1A, T m1x2A, T m1x3A, T m1x4A, T m1x5A,
        T m2x1A, T m2x2A, T m2x3A, T m2x4A, T m2x5A,
        T m3x1A, T m3x2A, T m3x3A, T m3x4A, T m3x5A,
        T m4x1A, T m4x2A, T m4x3A, T m4x4A, T m4x5A,
        T m5x1A, T m5x2A, T m5x3A, T m5x4A, T m5x5A,
        T m1x1B, T m1x2B, T m1x3B, T m1x4B, T m1x5B,
        T m2x1B, T m2x2B, T m2x3B, T m2x4B, T m2x5B,
        T m3x1B, T m3x2B, T m3x3B, T m3x4B, T m3x5B,
        T m4x1B, T m4x2B, T m4x3B, T m4x4B, T m4x5B,
        T m5x1B, T m5x2B, T m5x3B, T m5x4B, T m5x5B,
        T amount)
        where T : INumber<T> => (
            // First row
            m1x1A + ((m1x1B - m1x1A) * amount),
            m1x2A + ((m1x2B - m1x2A) * amount),
            m1x3A + ((m1x3B - m1x3A) * amount),
            m1x4A + ((m1x4B - m1x4A) * amount),
            m1x5A + ((m1x5B - m1x5A) * amount),

            // Second row
            m2x1A + ((m2x1B - m2x1A) * amount),
            m2x2A + ((m2x2B - m2x2A) * amount),
            m2x3A + ((m2x3B - m2x3A) * amount),
            m2x4A + ((m2x4B - m2x4A) * amount),
            m2x5A + ((m2x5B - m2x5A) * amount),

            // Third row
            m3x1A + ((m3x1B - m3x1A) * amount),
            m3x2A + ((m3x2B - m3x2A) * amount),
            m3x3A + ((m3x3B - m3x3A) * amount),
            m3x4A + ((m3x4B - m3x4A) * amount),
            m3x5A + ((m3x5B - m3x5A) * amount),

            // Fourth row
            m4x1A + ((m4x1B - m4x1A) * amount),
            m4x2A + ((m4x2B - m4x2A) * amount),
            m4x3A + ((m4x3B - m4x3A) * amount),
            m4x4A + ((m4x4B - m4x4A) * amount),
            m4x5A + ((m4x5B - m4x5A) * amount),

            // Fifth row
            m5x1A + ((m5x1B - m5x1A) * amount),
            m5x2A + ((m5x2B - m5x2A) * amount),
            m5x3A + ((m5x3B - m5x3A) * amount),
            m5x4A + ((m5x4B - m5x4A) * amount),
            m5x5A + ((m5x5B - m5x5A) * amount)
        );

    /// <summary>
    /// Linearly interpolates between the corresponding values of two matrices.
    /// </summary>
    /// <param name="m1x1A">The M1X1 a.</param>
    /// <param name="m1x2A">The M1X2 a.</param>
    /// <param name="m1x3A">The M1X3 a.</param>
    /// <param name="m1x4A">The M1X4 a.</param>
    /// <param name="m1x5A">The M1X5 a.</param>
    /// <param name="m1x6A">The M1X6 a.</param>
    /// <param name="m2x1A">The M2X1 a.</param>
    /// <param name="m2x2A">The M2X2 a.</param>
    /// <param name="m2x3A">The M2X3 a.</param>
    /// <param name="m2x4A">The M2X4 a.</param>
    /// <param name="m2x5A">The M2X5 a.</param>
    /// <param name="m2x6A">The M2X6 a.</param>
    /// <param name="m3x1A">The M3X1 a.</param>
    /// <param name="m3x2A">The M3X2 a.</param>
    /// <param name="m3x3A">The M3X3 a.</param>
    /// <param name="m3x4A">The M3X4 a.</param>
    /// <param name="m3x5A">The M3X5 a.</param>
    /// <param name="m3x6A">The M3X6 a.</param>
    /// <param name="m4x1A">The M4X1 a.</param>
    /// <param name="m4x2A">The M4X2 a.</param>
    /// <param name="m4x3A">The M4X3 a.</param>
    /// <param name="m4x4A">The M4X4 a.</param>
    /// <param name="m4x5A">The M4X5 a.</param>
    /// <param name="m4x6A">The M4X6 a.</param>
    /// <param name="m5x1A">The M5X1 a.</param>
    /// <param name="m5x2A">The M5X2 a.</param>
    /// <param name="m5x3A">The M5X3 a.</param>
    /// <param name="m5x4A">The M5X4 a.</param>
    /// <param name="m5x5A">The M5X5 a.</param>
    /// <param name="m5x6A">The M5X6 a.</param>
    /// <param name="m6x1A">The M6X1 a.</param>
    /// <param name="m6x2A">The M6X2 a.</param>
    /// <param name="m6x3A">The M6X3 a.</param>
    /// <param name="m6x4A">The M6X4 a.</param>
    /// <param name="m6x5A">The M6X5 a.</param>
    /// <param name="m6x6A">The M6X6 a.</param>
    /// <param name="m1x1B">The M1X1 b.</param>
    /// <param name="m1x2B">The M1X2 b.</param>
    /// <param name="m1x3B">The M1X3 b.</param>
    /// <param name="m1x4B">The M1X4 b.</param>
    /// <param name="m1x5B">The M1X5 b.</param>
    /// <param name="m1x6B">The M1X6 b.</param>
    /// <param name="m2x1B">The M2X1 b.</param>
    /// <param name="m2x2B">The M2X2 b.</param>
    /// <param name="m2x3B">The M2X3 b.</param>
    /// <param name="m2x4B">The M2X4 b.</param>
    /// <param name="m2x5B">The M2X5 b.</param>
    /// <param name="m2x6B">The M2X6 b.</param>
    /// <param name="m3x1B">The M3X1 b.</param>
    /// <param name="m3x2B">The M3X2 b.</param>
    /// <param name="m3x3B">The M3X3 b.</param>
    /// <param name="m3x4B">The M3X4 b.</param>
    /// <param name="m3x5B">The M3X5 b.</param>
    /// <param name="m3x6B">The M3X6 b.</param>
    /// <param name="m4x1B">The M4X1 b.</param>
    /// <param name="m4x2B">The M4X2 b.</param>
    /// <param name="m4x3B">The M4X3 b.</param>
    /// <param name="m4x4B">The M4X4 b.</param>
    /// <param name="m4x5B">The M4X5 b.</param>
    /// <param name="m4x6B">The M4X6 b.</param>
    /// <param name="m5x1B">The M5X1 b.</param>
    /// <param name="m5x2B">The M5X2 b.</param>
    /// <param name="m5x3B">The M5X3 b.</param>
    /// <param name="m5x4B">The M5X4 b.</param>
    /// <param name="m5x5B">The M5X5 b.</param>
    /// <param name="m5x6B">The M5X6 b.</param>
    /// <param name="m6x1B">The M6X1 b.</param>
    /// <param name="m6x2B">The M6X2 b.</param>
    /// <param name="m6x3B">The M6X3 b.</param>
    /// <param name="m6x4B">The M6X4 b.</param>
    /// <param name="m6x5B">The M6X5 b.</param>
    /// <param name="m6x6B">The M6X6 b.</param>
    /// <param name="amount">The relative weight of the second source matrix.</param>
    /// <returns>
    /// The interpolated matrix.
    /// </returns>
    /// <acknowledgment>
    /// https://referencesource.microsoft.com/#System.Numerics/System/Numerics/Matrix4x4.cs,48ce53b7e55d0436
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m1x1, T m1x2, T m1x3, T m1x4, T m1x5, T m1x6,
        T m2x1, T m2x2, T m2x3, T m2x4, T m2x5, T m2x6,
        T m3x1, T m3x2, T m3x3, T m3x4, T m3x5, T m3x6,
        T m4x1, T m4x2, T m4x3, T m4x4, T m4x5, T m4x6,
        T m5x1, T m5x2, T m5x3, T m5x4, T m5x5, T m5x6,
        T m6x1, T m6x2, T m6x3, T m6x4, T m6x5, T m6x6)
        LerpMatrix6x6<T>(
        T m1x1A, T m1x2A, T m1x3A, T m1x4A, T m1x5A, T m1x6A,
        T m2x1A, T m2x2A, T m2x3A, T m2x4A, T m2x5A, T m2x6A,
        T m3x1A, T m3x2A, T m3x3A, T m3x4A, T m3x5A, T m3x6A,
        T m4x1A, T m4x2A, T m4x3A, T m4x4A, T m4x5A, T m4x6A,
        T m5x1A, T m5x2A, T m5x3A, T m5x4A, T m5x5A, T m5x6A,
        T m6x1A, T m6x2A, T m6x3A, T m6x4A, T m6x5A, T m6x6A,
        T m1x1B, T m1x2B, T m1x3B, T m1x4B, T m1x5B, T m1x6B,
        T m2x1B, T m2x2B, T m2x3B, T m2x4B, T m2x5B, T m2x6B,
        T m3x1B, T m3x2B, T m3x3B, T m3x4B, T m3x5B, T m3x6B,
        T m4x1B, T m4x2B, T m4x3B, T m4x4B, T m4x5B, T m4x6B,
        T m5x1B, T m5x2B, T m5x3B, T m5x4B, T m5x5B, T m5x6B,
        T m6x1B, T m6x2B, T m6x3B, T m6x4B, T m6x5B, T m6x6B,
        T amount)
        where T : INumber<T> => (
            // First row
            m1x1A + ((m1x1B - m1x1A) * amount),
            m1x2A + ((m1x2B - m1x2A) * amount),
            m1x3A + ((m1x3B - m1x3A) * amount),
            m1x4A + ((m1x4B - m1x4A) * amount),
            m1x5A + ((m1x5B - m1x5A) * amount),
            m1x6A + ((m1x6B - m1x6A) * amount),

            // Second row
            m2x1A + ((m2x1B - m2x1A) * amount),
            m2x2A + ((m2x2B - m2x2A) * amount),
            m2x3A + ((m2x3B - m2x3A) * amount),
            m2x4A + ((m2x4B - m2x4A) * amount),
            m2x5A + ((m2x5B - m2x5A) * amount),
            m2x6A + ((m2x6B - m2x6A) * amount),

            // Third row
            m3x1A + ((m3x1B - m3x1A) * amount),
            m3x2A + ((m3x2B - m3x2A) * amount),
            m3x3A + ((m3x3B - m3x3A) * amount),
            m3x4A + ((m3x4B - m3x4A) * amount),
            m3x5A + ((m3x5B - m3x5A) * amount),
            m3x6A + ((m3x6B - m3x6A) * amount),

            // Fourth row
            m4x1A + ((m4x1B - m4x1A) * amount),
            m4x2A + ((m4x2B - m4x2A) * amount),
            m4x3A + ((m4x3B - m4x3A) * amount),
            m4x4A + ((m4x4B - m4x4A) * amount),
            m4x5A + ((m4x5B - m4x5A) * amount),
            m4x6A + ((m4x6B - m4x6A) * amount),

            // Fifth row
            m5x1A + ((m5x1B - m5x1A) * amount),
            m5x2A + ((m5x2B - m5x2A) * amount),
            m5x3A + ((m5x3B - m5x3A) * amount),
            m5x4A + ((m5x4B - m5x4A) * amount),
            m5x5A + ((m5x5B - m5x5A) * amount),
            m5x6A + ((m5x6B - m5x6A) * amount),

            // Sixth row
            m6x1A + ((m6x1B - m6x1A) * amount),
            m6x2A + ((m6x2B - m6x2A) * amount),
            m6x3A + ((m6x3B - m6x3A) * amount),
            m6x4A + ((m6x4B - m6x4A) * amount),
            m6x5A + ((m6x5B - m6x5A) * amount),
            m6x6A + ((m6x6B - m6x6A) * amount)
        );
    #endregion

    #region Dot Product
    /// <summary>
    /// Dots the product.
    /// </summary>
    /// <param name="a">a.</param>
    /// <param name="b">The b.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T DotProduct<T>(T[,] a, T[,] b)
        where T : INumber<T>
    {
        var rowsA = a?.GetLength(0);
        var colsA = a?.GetLength(1);
        var rowsB = b?.GetLength(0);
        var colsB = b?.GetLength(1);

        if (rowsA != rowsB || colsA != colsB) throw new Exception();

        T result = T.Zero;
        for (var i = 0; i < rowsA; i++)
        {
            for (var j = 0; j < colsA; j++)
            {
                result += a[i, j] * b[i, j];
            }
        }

        return result;
    }

    /// <summary>
    /// Dots the product.
    /// </summary>
    /// <param name="a">a.</param>
    /// <param name="b">The b.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T DotProduct<T>(T[][] a, T[][] b)
        where T : INumber<T>
    {
        var rowsA = a?.Length;
        var colsA = a[0]?.Length;
        var rowsB = b?.Length;
        var colsB = b[0]?.Length;

        if (rowsA != rowsB || colsA != colsB) throw new Exception();

        T result = T.Zero;
        for (var i = 0; i < rowsA; i++)
        {
            for (var j = 0; j < colsA; j++)
            {
                result += a[i][j] * b[i][j];
            }
        }

        return result;
    }

    /// <summary>
    /// Dots the product matrix2x2.
    /// </summary>
    /// <param name="m1x1A">The M1X1 a.</param>
    /// <param name="m1x2A">The M1X2 a.</param>
    /// <param name="m2x1A">The M2X1 a.</param>
    /// <param name="m2x2A">The M2X2 a.</param>
    /// <param name="m1x1B">The M1X1 b.</param>
    /// <param name="m1x2B">The M1X2 b.</param>
    /// <param name="m2x1B">The M2X1 b.</param>
    /// <param name="m2x2B">The M2X2 b.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// https://www.physicsforums.com/threads/dot-product-2x2-matrix.688717/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T DotProductMatrix2x2<T>(
        T m1x1A, T m1x2A,
        T m2x1A, T m2x2A,
        T m1x1B, T m1x2B,
        T m2x1B, T m2x2B)
        where T : INumber<T>
        => (m1x1A * m1x1B) + (m1x2A * m1x2B)
         + (m2x1A * m2x1B) + (m2x2A * m2x2B);

    /// <summary>
    /// Dots the product matrix3x3.
    /// </summary>
    /// <param name="m1x1A">The M1X1 a.</param>
    /// <param name="m1x2A">The M1X2 a.</param>
    /// <param name="m1x3A">The M1X3 a.</param>
    /// <param name="m2x1A">The M2X1 a.</param>
    /// <param name="m2x2A">The M2X2 a.</param>
    /// <param name="m2x3A">The M2X3 a.</param>
    /// <param name="m3x1A">The M3X1 a.</param>
    /// <param name="m3x2A">The M3X2 a.</param>
    /// <param name="m3x3A">The M3X3 a.</param>
    /// <param name="m1x1B">The M1X1 b.</param>
    /// <param name="m1x2B">The M1X2 b.</param>
    /// <param name="m1x3B">The M1X3 b.</param>
    /// <param name="m2x1B">The M2X1 b.</param>
    /// <param name="m2x2B">The M2X2 b.</param>
    /// <param name="m2x3B">The M2X3 b.</param>
    /// <param name="m3x1B">The M3X1 b.</param>
    /// <param name="m3x2B">The M3X2 b.</param>
    /// <param name="m3x3B">The M3X3 b.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// https://www.physicsforums.com/threads/dot-product-2x2-matrix.688717/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T DotProductMatrix3x3<T>(
        T m1x1A, T m1x2A, T m1x3A,
        T m2x1A, T m2x2A, T m2x3A,
        T m3x1A, T m3x2A, T m3x3A,
        T m1x1B, T m1x2B, T m1x3B,
        T m2x1B, T m2x2B, T m2x3B,
        T m3x1B, T m3x2B, T m3x3B)
        where T : INumber<T>
        => (m1x1A * m1x1B) + (m1x2A * m1x2B) + (m1x3A * m1x3B)
         + (m2x1A * m2x1B) + (m2x2A * m2x2B) + (m2x3A * m2x3B)
         + (m3x1A * m3x1B) + (m3x2A * m3x2B) + (m3x3A * m3x3B);

    /// <summary>
    /// Dots the product matrix4x4.
    /// </summary>
    /// <param name="m1x1A">The M1X1 a.</param>
    /// <param name="m1x2A">The M1X2 a.</param>
    /// <param name="m1x3A">The M1X3 a.</param>
    /// <param name="m1x4A">The M1X4 a.</param>
    /// <param name="m2x1A">The M2X1 a.</param>
    /// <param name="m2x2A">The M2X2 a.</param>
    /// <param name="m2x3A">The M2X3 a.</param>
    /// <param name="m2x4A">The M2X4 a.</param>
    /// <param name="m3x1A">The M3X1 a.</param>
    /// <param name="m3x2A">The M3X2 a.</param>
    /// <param name="m3x3A">The M3X3 a.</param>
    /// <param name="m3x4A">The M3X4 a.</param>
    /// <param name="m4x1A">The M4X1 a.</param>
    /// <param name="m4x2A">The M4X2 a.</param>
    /// <param name="m4x3A">The M4X3 a.</param>
    /// <param name="m4x4A">The M4X4 a.</param>
    /// <param name="m1x1B">The M1X1 b.</param>
    /// <param name="m1x2B">The M1X2 b.</param>
    /// <param name="m1x3B">The M1X3 b.</param>
    /// <param name="m1x4B">The M1X4 b.</param>
    /// <param name="m2x1B">The M2X1 b.</param>
    /// <param name="m2x2B">The M2X2 b.</param>
    /// <param name="m2x3B">The M2X3 b.</param>
    /// <param name="m2x4B">The M2X4 b.</param>
    /// <param name="m3x1B">The M3X1 b.</param>
    /// <param name="m3x2B">The M3X2 b.</param>
    /// <param name="m3x3B">The M3X3 b.</param>
    /// <param name="m3x4B">The M3X4 b.</param>
    /// <param name="m4x1B">The M4X1 b.</param>
    /// <param name="m4x2B">The M4X2 b.</param>
    /// <param name="m4x3B">The M4X3 b.</param>
    /// <param name="m4x4B">The M4X4 b.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// https://www.physicsforums.com/threads/dot-product-2x2-matrix.688717/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T DotProductMatrix4x4<T>(
        T m1x1A, T m1x2A, T m1x3A, T m1x4A,
        T m2x1A, T m2x2A, T m2x3A, T m2x4A,
        T m3x1A, T m3x2A, T m3x3A, T m3x4A,
        T m4x1A, T m4x2A, T m4x3A, T m4x4A,
        T m1x1B, T m1x2B, T m1x3B, T m1x4B,
        T m2x1B, T m2x2B, T m2x3B, T m2x4B,
        T m3x1B, T m3x2B, T m3x3B, T m3x4B,
        T m4x1B, T m4x2B, T m4x3B, T m4x4B)
        where T : INumber<T>
        => (m1x1A * m1x1B) + (m1x2A * m1x2B) + (m1x3A * m1x3B) + (m1x4A * m1x4B)
         + (m2x1A * m2x1B) + (m2x2A * m2x2B) + (m2x3A * m2x3B) + (m2x4A * m2x4B)
         + (m3x1A * m3x1B) + (m3x2A * m3x2B) + (m3x3A * m3x3B) + (m3x4A * m3x4B)
         + (m4x1A * m4x1B) + (m4x2A * m4x2B) + (m4x3A * m4x3B) + (m4x4A * m4x4B);

    /// <summary>
    /// Dots the product matrix5x5.
    /// </summary>
    /// <param name="m1x1A">The M1X1 a.</param>
    /// <param name="m1x2A">The M1X2 a.</param>
    /// <param name="m1x3A">The M1X3 a.</param>
    /// <param name="m1x4A">The M1X4 a.</param>
    /// <param name="m1x5A">The M1X5 a.</param>
    /// <param name="m2x1A">The M2X1 a.</param>
    /// <param name="m2x2A">The M2X2 a.</param>
    /// <param name="m2x3A">The M2X3 a.</param>
    /// <param name="m2x4A">The M2X4 a.</param>
    /// <param name="m2x5A">The M2X5 a.</param>
    /// <param name="m3x1A">The M3X1 a.</param>
    /// <param name="m3x2A">The M3X2 a.</param>
    /// <param name="m3x3A">The M3X3 a.</param>
    /// <param name="m3x4A">The M3X4 a.</param>
    /// <param name="m3x5A">The M3X5 a.</param>
    /// <param name="m4x1A">The M4X1 a.</param>
    /// <param name="m4x2A">The M4X2 a.</param>
    /// <param name="m4x3A">The M4X3 a.</param>
    /// <param name="m4x4A">The M4X4 a.</param>
    /// <param name="m4x5A">The M4X5 a.</param>
    /// <param name="m5x1A">The M5X1 a.</param>
    /// <param name="m5x2A">The M5X2 a.</param>
    /// <param name="m5x3A">The M5X3 a.</param>
    /// <param name="m5x4A">The M5X4 a.</param>
    /// <param name="m5x5A">The M5X5 a.</param>
    /// <param name="m1x1B">The M1X1 b.</param>
    /// <param name="m1x2B">The M1X2 b.</param>
    /// <param name="m1x3B">The M1X3 b.</param>
    /// <param name="m1x4B">The M1X4 b.</param>
    /// <param name="m1x5B">The M1X5 b.</param>
    /// <param name="m2x1B">The M2X1 b.</param>
    /// <param name="m2x2B">The M2X2 b.</param>
    /// <param name="m2x3B">The M2X3 b.</param>
    /// <param name="m2x4B">The M2X4 b.</param>
    /// <param name="m2x5B">The M2X5 b.</param>
    /// <param name="m3x1B">The M3X1 b.</param>
    /// <param name="m3x2B">The M3X2 b.</param>
    /// <param name="m3x3B">The M3X3 b.</param>
    /// <param name="m3x4B">The M3X4 b.</param>
    /// <param name="m3x5B">The M3X5 b.</param>
    /// <param name="m4x1B">The M4X1 b.</param>
    /// <param name="m4x2B">The M4X2 b.</param>
    /// <param name="m4x3B">The M4X3 b.</param>
    /// <param name="m4x4B">The M4X4 b.</param>
    /// <param name="m4x5B">The M4X5 b.</param>
    /// <param name="m5x1B">The M5X1 b.</param>
    /// <param name="m5x2B">The M5X2 b.</param>
    /// <param name="m5x3B">The M5X3 b.</param>
    /// <param name="m5x4B">The M5X4 b.</param>
    /// <param name="m5x5B">The M5X5 b.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// https://www.physicsforums.com/threads/dot-product-2x2-matrix.688717/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T DotProductMatrix5x5<T>(
        T m1x1A, T m1x2A, T m1x3A, T m1x4A, T m1x5A,
        T m2x1A, T m2x2A, T m2x3A, T m2x4A, T m2x5A,
        T m3x1A, T m3x2A, T m3x3A, T m3x4A, T m3x5A,
        T m4x1A, T m4x2A, T m4x3A, T m4x4A, T m4x5A,
        T m5x1A, T m5x2A, T m5x3A, T m5x4A, T m5x5A,
        T m1x1B, T m1x2B, T m1x3B, T m1x4B, T m1x5B,
        T m2x1B, T m2x2B, T m2x3B, T m2x4B, T m2x5B,
        T m3x1B, T m3x2B, T m3x3B, T m3x4B, T m3x5B,
        T m4x1B, T m4x2B, T m4x3B, T m4x4B, T m4x5B,
        T m5x1B, T m5x2B, T m5x3B, T m5x4B, T m5x5B)
        where T : INumber<T>
        => (m1x1A * m1x1B) + (m1x2A * m1x2B) + (m1x3A * m1x3B) + (m1x4A * m1x4B) + (m1x5A * m1x5B)
         + (m2x1A * m2x1B) + (m2x2A * m2x2B) + (m2x3A * m2x3B) + (m2x4A * m2x4B) + (m2x5A * m2x5B)
         + (m3x1A * m3x1B) + (m3x2A * m3x2B) + (m3x3A * m3x3B) + (m3x4A * m3x4B) + (m3x5A * m3x5B)
         + (m4x1A * m4x1B) + (m4x2A * m4x2B) + (m4x3A * m4x3B) + (m4x4A * m4x4B) + (m4x5A * m4x5B)
         + (m5x1A * m5x1B) + (m5x2A * m5x2B) + (m5x3A * m5x3B) + (m5x4A * m5x4B) + (m5x5A * m5x5B);

    /// <summary>
    /// Dots the product matrix6x6.
    /// </summary>
    /// <param name="m1x1A">The M1X1 a.</param>
    /// <param name="m1x2A">The M1X2 a.</param>
    /// <param name="m1x3A">The M1X3 a.</param>
    /// <param name="m1x4A">The M1X4 a.</param>
    /// <param name="m1x5A">The M1X5 a.</param>
    /// <param name="m1x6A">The M1X6 a.</param>
    /// <param name="m2x1A">The M2X1 a.</param>
    /// <param name="m2x2A">The M2X2 a.</param>
    /// <param name="m2x3A">The M2X3 a.</param>
    /// <param name="m2x4A">The M2X4 a.</param>
    /// <param name="m2x5A">The M2X5 a.</param>
    /// <param name="m2x6A">The M2X6 a.</param>
    /// <param name="m3x1A">The M3X1 a.</param>
    /// <param name="m3x2A">The M3X2 a.</param>
    /// <param name="m3x3A">The M3X3 a.</param>
    /// <param name="m3x4A">The M3X4 a.</param>
    /// <param name="m3x5A">The M3X5 a.</param>
    /// <param name="m3x6A">The M3X6 a.</param>
    /// <param name="m4x1A">The M4X1 a.</param>
    /// <param name="m4x2A">The M4X2 a.</param>
    /// <param name="m4x3A">The M4X3 a.</param>
    /// <param name="m4x4A">The M4X4 a.</param>
    /// <param name="m4x5A">The M4X5 a.</param>
    /// <param name="m4x6A">The M4X6 a.</param>
    /// <param name="m5x1A">The M5X1 a.</param>
    /// <param name="m5x2A">The M5X2 a.</param>
    /// <param name="m5x3A">The M5X3 a.</param>
    /// <param name="m5x4A">The M5X4 a.</param>
    /// <param name="m5x5A">The M5X5 a.</param>
    /// <param name="m5x6A">The M5X6 a.</param>
    /// <param name="m6x1A">The M6X1 a.</param>
    /// <param name="m6x2A">The M6X2 a.</param>
    /// <param name="m6x3A">The M6X3 a.</param>
    /// <param name="m6x4A">The M6X4 a.</param>
    /// <param name="m6x5A">The M6X5 a.</param>
    /// <param name="m6x6A">The M6X6 a.</param>
    /// <param name="m1x1B">The M1X1 b.</param>
    /// <param name="m1x2B">The M1X2 b.</param>
    /// <param name="m1x3B">The M1X3 b.</param>
    /// <param name="m1x4B">The M1X4 b.</param>
    /// <param name="m1x5B">The M1X5 b.</param>
    /// <param name="m1x6B">The M1X6 b.</param>
    /// <param name="m2x1B">The M2X1 b.</param>
    /// <param name="m2x2B">The M2X2 b.</param>
    /// <param name="m2x3B">The M2X3 b.</param>
    /// <param name="m2x4B">The M2X4 b.</param>
    /// <param name="m2x5B">The M2X5 b.</param>
    /// <param name="m2x6B">The M2X6 b.</param>
    /// <param name="m3x1B">The M3X1 b.</param>
    /// <param name="m3x2B">The M3X2 b.</param>
    /// <param name="m3x3B">The M3X3 b.</param>
    /// <param name="m3x4B">The M3X4 b.</param>
    /// <param name="m3x5B">The M3X5 b.</param>
    /// <param name="m3x6B">The M3X6 b.</param>
    /// <param name="m4x1B">The M4X1 b.</param>
    /// <param name="m4x2B">The M4X2 b.</param>
    /// <param name="m4x3B">The M4X3 b.</param>
    /// <param name="m4x4B">The M4X4 b.</param>
    /// <param name="m4x5B">The M4X5 b.</param>
    /// <param name="m4x6B">The M4X6 b.</param>
    /// <param name="m5x1B">The M5X1 b.</param>
    /// <param name="m5x2B">The M5X2 b.</param>
    /// <param name="m5x3B">The M5X3 b.</param>
    /// <param name="m5x4B">The M5X4 b.</param>
    /// <param name="m5x5B">The M5X5 b.</param>
    /// <param name="m5x6B">The M5X6 b.</param>
    /// <param name="m6x1B">The M6X1 b.</param>
    /// <param name="m6x2B">The M6X2 b.</param>
    /// <param name="m6x3B">The M6X3 b.</param>
    /// <param name="m6x4B">The M6X4 b.</param>
    /// <param name="m6x5B">The M6X5 b.</param>
    /// <param name="m6x6B">The M6X6 b.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// https://www.physicsforums.com/threads/dot-product-2x2-matrix.688717/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T DotProductMatrix6x6<T>(
        T m1x1A, T m1x2A, T m1x3A, T m1x4A, T m1x5A, T m1x6A,
        T m2x1A, T m2x2A, T m2x3A, T m2x4A, T m2x5A, T m2x6A,
        T m3x1A, T m3x2A, T m3x3A, T m3x4A, T m3x5A, T m3x6A,
        T m4x1A, T m4x2A, T m4x3A, T m4x4A, T m4x5A, T m4x6A,
        T m5x1A, T m5x2A, T m5x3A, T m5x4A, T m5x5A, T m5x6A,
        T m6x1A, T m6x2A, T m6x3A, T m6x4A, T m6x5A, T m6x6A,
        T m1x1B, T m1x2B, T m1x3B, T m1x4B, T m1x5B, T m1x6B,
        T m2x1B, T m2x2B, T m2x3B, T m2x4B, T m2x5B, T m2x6B,
        T m3x1B, T m3x2B, T m3x3B, T m3x4B, T m3x5B, T m3x6B,
        T m4x1B, T m4x2B, T m4x3B, T m4x4B, T m4x5B, T m4x6B,
        T m5x1B, T m5x2B, T m5x3B, T m5x4B, T m5x5B, T m5x6B,
        T m6x1B, T m6x2B, T m6x3B, T m6x4B, T m6x5B, T m6x6B)
        where T : INumber<T>
        => (m1x1A * m1x1B) + (m1x2A * m1x2B) + (m1x3A * m1x3B) + (m1x4A * m1x4B) + (m1x5A * m1x5B) + (m1x6A * m1x6B)
         + (m2x1A * m2x1B) + (m2x2A * m2x2B) + (m2x3A * m2x3B) + (m2x4A * m2x4B) + (m2x5A * m2x5B) + (m2x6A * m2x6B)
         + (m3x1A * m3x1B) + (m3x2A * m3x2B) + (m3x3A * m3x3B) + (m3x4A * m3x4B) + (m3x5A * m3x5B) + (m3x6A * m3x6B)
         + (m4x1A * m4x1B) + (m4x2A * m4x2B) + (m4x3A * m4x3B) + (m4x4A * m4x4B) + (m4x5A * m4x5B) + (m4x6A * m4x6B)
         + (m5x1A * m5x1B) + (m5x2A * m5x2B) + (m5x3A * m5x3B) + (m5x4A * m5x4B) + (m5x5A * m5x5B) + (m5x6A * m5x6B)
         + (m6x1A * m6x1B) + (m6x2A * m6x2B) + (m6x3A * m6x3B) + (m6x4A * m6x4B) + (m6x5A * m6x5B) + (m6x6A * m6x6B);
    #endregion

    /// <summary>
    /// Compute LU decomposition on a squared matrix
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// https://github.com/SarahFrem/AutoRegressive_model_cs/blob/master/Matrix.cs#L219
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T[,] Lower, T[,] Upper) DecomposeToLowerUpper<T>(T[,] matrix)
        where T : INumber<T>
    {
        var rows = matrix.GetLength(0);
        var cols = matrix.GetLength(1);

        if (!IsSquareMatrix(matrix))
        {
            return (new T[1, 1], new T[1, 1]);
        }
        else
        {
            var lower = new T[rows, cols];
            var upper = new T[rows, cols];

            for (var i = 0; i < rows; i++)
            {
                lower[i, i] = T.One;
            }

            for (var i = 0; i < rows; i++)
            {
                for (var j = i; j < rows; j++)
                {
                    var sumU = T.Zero;
                    for (var k = 0; k < i; k++)
                    {
                        sumU += lower[i, k] * upper[k, j];
                    }

                    upper[i, j] = matrix[i, j] - sumU;
                }

                for (var j = i; j < rows; j++)
                {
                    var sumL = T.Zero;
                    for (var k = 0; k < i; k++)
                    {
                        sumL += lower[j, k] * upper[k, i];
                    }

                    lower[j, i] = (matrix[j, i] - sumL) / upper[i, i];
                }
            }

            return (lower, upper);
        }
    }

    /// <summary>
    /// Compute LU decomposition on a squared matrix
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// https://github.com/SarahFrem/AutoRegressive_model_cs/blob/master/Matrix.cs#L219
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T[][] Lower, T[][] Upper) DecomposeToLowerUpper<T>(T[][] matrix)
        where T : INumber<T>
    {
        var rows = matrix.Length;
        var cols = matrix[0].Length;

        if (!IsSquareMatrix(matrix))
        {
            return (new T[1][] { new T[1] }, new T[1][] { new T[1] });
        }
        else
        {
            var lower = new T[rows][];
            var upper = new T[rows][];

            for (var i = 0; i < rows; i++)
            {
                lower[i] = new T[cols];
                lower[i][i] = T.One;
            }

            for (var i = 0; i < rows; i++)
            {
                upper[i] = new T[cols];
                for (var j = i; j < rows; j++)
                {
                    var sumU = T.Zero;
                    for (var k = 0; k < i; k++)
                    {
                        sumU += lower[i][k] * upper[k][j];
                    }

                    upper[i][j] = matrix[i][j] - sumU;
                }

                for (var j = i; j < rows; j++)
                {
                    var sumL = T.Zero;
                    for (var k = 0; k < i; k++)
                    {
                        sumL += lower[j][k] * upper[k][i];
                    }

                    lower[j][i] = (matrix[j][i] - sumL) / upper[i][i];
                }
            }

            return (lower, upper);
        }
    }
}
