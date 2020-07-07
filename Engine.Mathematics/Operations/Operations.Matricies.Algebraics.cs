// <copyright file="Operations.MatriciesAlgebraics.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel.DataAnnotations;
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

        #region Bézier Bernstein Basis Matrices
        /// <summary>
        /// The linear Bézier Bernstein basis matrix.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2,
            double m2x1, double m2x2)
            LinearBezierBernsteinBasisMatrix
            = (1d, 0d,
              -1d, 1d);

        /// <summary>
        /// The quadratic Bézier Bernstein basis matrix.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3)
            QuadraticBezierBernsteinBasisMatrix
            = (1d, 0d, 0d,
               -2d, 2d, 0d,
               1d, -2d, 1d);

        /// <summary>
        /// The cubic Bézier Bernstein basis matrix.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4)
            CubicBezierBernsteinBasisMatrix
            = (1d, 0d, 0d, 0d,
               -3d, 3d, 0d, 0d,
               3d, -6d, 3d, 0d,
               -1d, 3d, -3d, 1d);

        /// <summary>
        /// The quartic Bézier Bernstein basis matrix.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5)
            QuarticBezierBernsteinBasisMatrix
            = (1d, 0d, 0d, 0d, 0d,
               -4d, 4d, 0d, 0d, 0d,
               6d, -12d, 6d, 0d, 0d,
               -4d, 12d, -12d, 4d, 0d,
               1d, -4d, 6d, -4d, 1d);

        /// <summary>
        /// The quintic Bézier Bernstein basis matrix.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6)
            QuinticBezierBernsteinBasisMatrix
            = (1d, 0d, 0d, 0d, 0d, 0d,
               -5d, 5d, 0d, 0d, 0d, 0d,
               10d, -20d, 10d, 0d, 0d, 0d,
               -10d, 30d, -30d, 10d, 0d, 0d,
               5d, -20d, 30d, -20d, 5d, 0d,
               -1d, 5d, -10d, 10d, -5d, 1d);

        /// <summary>
        /// The sextic Bézier Bernstein basis matrix.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6, double m1x7,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6, double m2x7,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6, double m3x7,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6, double m4x7,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6, double m5x7,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6, double m6x7,
            double m7x1, double m7x2, double m7x3, double m7x4, double m7x5, double m7x6, double m7x7)
            SexticBezierBernsteinBasisMatrix
            = (1d, 0d, 0d, 0d, 0d, 0d, 0d,
               -6d, 6d, 0d, 0d, 0d, 0d, 0d,
               15d, -30d, 15d, 0d, 0d, 0d, 0d,
               -20d, 60d, -60d, 20d, 0d, 0d, 0d,
               15d, -60d, 90d, -60d, 15d, 0d, 0d,
               -6d, 30d, -60d, 60d, -30d, 6d, 0d,
               1d, -6d, 15d, -20d, 15d, -6d, 1d);

        /// <summary>
        /// The septic Bézier Bernstein basis matrix.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6, double m1x7, double m1x8,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6, double m2x7, double m2x8,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6, double m3x7, double m3x8,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6, double m4x7, double m4x8,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6, double m5x7, double m5x8,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6, double m6x7, double m6x8,
            double m7x1, double m7x2, double m7x3, double m7x4, double m7x5, double m7x6, double m7x7, double m7x8,
            double m8x1, double m8x2, double m8x3, double m8x4, double m8x5, double m8x6, double m8x7, double m8x8)
            SepticBezierBernsteinBasisMatrix
            = (1d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
               -7d, 7d, 0d, 0d, 0d, 0d, 0d, 0d,
               21d, -42d, 21d, 0d, 0d, 0d, 0d, 0d,
               -35d, 105d, -105d, 35d, 0d, 0d, 0d, 0d,
               35d, -140d, 210d, -140d, 35d, 0d, 0d, 0d,
               -21d, 105d, -210d, 210d, -105d, 21d, 0d, 0d,
               7d, -42d, 105d, -140d, 105d, -42d, 7d, 0d,
               -1d, 7d, -21d, 35d, -35d, 21d, -7d, 1d);

        /// <summary>
        /// The octic Bézier Bernstein basis matrix.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6, double m1x7, double m1x8, double m1x9,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6, double m2x7, double m2x8, double m2x9,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6, double m3x7, double m3x8, double m3x9,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6, double m4x7, double m4x8, double m4x9,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6, double m5x7, double m5x8, double m5x9,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6, double m6x7, double m6x8, double m6x9,
            double m7x1, double m7x2, double m7x3, double m7x4, double m7x5, double m7x6, double m7x7, double m7x8, double m7x9,
            double m8x1, double m8x2, double m8x3, double m8x4, double m8x5, double m8x6, double m8x7, double m8x8, double m8x9,
            double m9x1, double m9x2, double m9x3, double m9x4, double m9x5, double m9x6, double m9x7, double m9x8, double m9x9)
            OcticBezierBernsteinBasisMatrix
            = (1d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
               -8d, 8d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
               28d, -56d, 28d, 0d, 0d, 0d, 0d, 0d, 0d,
               -56d, 168d, -168d, 56d, 0d, 0d, 0d, 0d, 0d,
               70d, -280d, 240d, -280d, 70d, 0d, 0d, 0d, 0d,
               -56d, 280d, -560d, 560d, -280d, 56d, 0d, 0d, 0d,
               28d, -168d, 420d, -560d, 420d, -168d, 28d, 0d, 0d,
               -8d, 56d, -168d, 280d, -280d, 168d, -56d, 8d, 0d,
               1d, -8d, 28d, -56d, 70d, -56d, 28d, -8d, 1d);

        /// <summary>
        /// The nonic Bézier Bernstein basis matrix.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6, double m1x7, double m1x8, double m1x9, double m1x10,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6, double m2x7, double m2x8, double m2x9, double m2x10,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6, double m3x7, double m3x8, double m3x9, double m3x10,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6, double m4x7, double m4x8, double m4x9, double m4x10,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6, double m5x7, double m5x8, double m5x9, double m5x10,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6, double m6x7, double m6x8, double m6x9, double m6x10,
            double m7x1, double m7x2, double m7x3, double m7x4, double m7x5, double m7x6, double m7x7, double m7x8, double m7x9, double m7x10,
            double m8x1, double m8x2, double m8x3, double m8x4, double m8x5, double m8x6, double m8x7, double m8x8, double m8x9, double m8x10,
            double m9x1, double m9x2, double m9x3, double m9x4, double m9x5, double m9x6, double m9x7, double m9x8, double m9x9, double m9x10,
            double m10x1, double m10x2, double m10x3, double m10x4, double m10x5, double m10x6, double m10x7, double m10x8, double m10x9, double m10x10)
            NonicBezierBernsteinBasisMatrix
            = (1d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
               -9d, 9d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
               36d, -72d, 36d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
               -84d, 252d, -252d, 84d, 0d, 0d, 0d, 0d, 0d, 0d,
               126d, -504d, 756d, -504d, 126d, 0d, 0d, 0d, 0d, 0d,
               -126d, 630d, -1260d, 1260d, -630d, 126d, 0d, 0d, 0d, 0d,
               84d, -504d, 1260d, -1680d, 1260d, -504d, 84d, 0d, 0d, 0d,
               -36d, 252d, -756d, 1260d, -1260d, 756d, -252d, 36d, 0d, 0d,
               9d, -27d, 252d, -504d, 360d, -504d, 252d, -72d, 9d, 0d,
               -1d, 9d, -36d, 84d, -126d, 126d, -84d, 36d, -9d, 1d);

        /// <summary>
        /// The decic Bézier Bernstein basis matrix.
        /// </summary>
        public static readonly (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6, double m1x7, double m1x8, double m1x9, double m1x10, double m1x11,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6, double m2x7, double m2x8, double m2x9, double m2x10, double m2x11,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6, double m3x7, double m3x8, double m3x9, double m3x10, double m3x11,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6, double m4x7, double m4x8, double m4x9, double m4x10, double m4x11,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6, double m5x7, double m5x8, double m5x9, double m5x10, double m5x11,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6, double m6x7, double m6x8, double m6x9, double m6x10, double m6x11,
            double m7x1, double m7x2, double m7x3, double m7x4, double m7x5, double m7x6, double m7x7, double m7x8, double m7x9, double m7x10, double m7x11,
            double m8x1, double m8x2, double m8x3, double m8x4, double m8x5, double m8x6, double m8x7, double m8x8, double m8x9, double m8x10, double m8x11,
            double m9x1, double m9x2, double m9x3, double m9x4, double m9x5, double m9x6, double m9x7, double m9x8, double m9x9, double m9x10, double m9x11,
            double m10x1, double m10x2, double m10x3, double m10x4, double m10x5, double m10x6, double m10x7, double m10x8, double m10x9, double m10x10, double m10x11,
            double m11x1, double m11x2, double m11x3, double m11x4, double m11x5, double m11x6, double m11x7, double m11x8, double m11x9, double m11x10, double m11x11)
            DecicBezierBernsteinBasisMatrix
            = (1d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
               -10d, 10d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
               45d, -90d, 45d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
               -120d, 360d, -360d, 120d, 0d, 0d, 0d, 0d, 0d, 0d, 0d,
               210d, -840d, 1260d, -840d, 210d, 0d, 0d, 0d, 0d, 0d, 0d,
               -252d, 1260d, -2520d, 2520d, -1260d, 252d, 0d, 0d, 0d, 0d, 0d,
               210d, -1260d, 3150d, -4200d, 3150d, -1260d, 210d, 0d, 0d, 0d, 0d,
               -120d, 840d, -2520d, 4200d, -4200d, 2520d, -840d, 120d, 0d, 0d, 0d,
               45d, -360d, 1260d, -2520d, 3150d, -2520d, 1260d, -360d, 45d, 0d, 0d,
               -10d, 90d, -360d, 840d, -1260d, 1260d, -840d, 360d, -90d, 10d, 0d,
               1d, -10d, 45d, -120d, 210d, -252d, 210d, -120d, 45d, -10d, 1d);
        #endregion

        #region Adjoint
        /// <summary>
        /// Function to get adjoint of the specified matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://www.geeksforgeeks.org/adjoint-inverse-matrix/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[,] Adjoint(double[,] matrix)
        {
            var rows = matrix.GetLength(0);
            var cols = matrix.GetLength(1);

            if (rows == 1)
            {
                return new double[1, 1] { { 1d } };
            }

            var adj = new double[rows, cols];

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    // Get cofactor of A[i,j] 
                    var temp = Cofactor(matrix, i, j);

                    // Sign of adj[j,i] positive if sum of row and column indexes is even. 
                    var sign = ((i + j) % 2d == 0d) ? 1d : -1d;

                    // Interchanging rows and columns to get the  transpose of the cofactor matrix 
                    adj[j, i] = sign * Determinant(temp);
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[][] Adjoint(double[][] matrix)
        {
            var rows = matrix.Length;
            var cols = matrix[0].Length;

            if (rows == 1)
            {
                return new double[1][] { new double[] { 1d } };
            }

            var adj = new double[rows][];

            for (var i = 0; i < rows; i++)
            {
                adj[i] = new double[cols];
                for (var j = 0; j < cols; j++)
                {
                    // Get cofactor of A[i,j] 
                    var temp = Cofactor(matrix, i, j);

                    // Sign of adj[j,i] positive if sum of row and column indexes is even. 
                    var sign = ((i + j) % 2d == 0d) ? 1d : -1d;

                    // Interchanging rows and columns to get the  transpose of the cofactor matrix 
                    adj[j][i] = sign * Determinant(temp);
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
        /// The <see cref="Matrix3x3D" />.
        /// </returns>
        /// <acknowledgment>
        /// https://sites.google.com/site/physics2d/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2,
            double m2x1, double m2x2)
            AdjointMatrix(
            double m1x1, double m1x2,
            double m2x1, double m2x2)
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
        /// The <see cref="Matrix3x3D" />.
        /// </returns>
        /// <acknowledgment>
        /// https://sites.google.com/site/physics2d/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3)
            AdjointMatrix(
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3)
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4)
            AdjointMatrix(
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4)
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5)
            AdjointMatrix(
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5)
        {
            var m = Adjoint(new double[,]
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6)
            AdjointMatrix(
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6)
        {
            var m = Adjoint(new double[,]
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[,] Cofactor(double[,] matrix)
        {
            var i = 0;
            var j = 0;
            var rows = matrix.GetLength(0);
            var cols = matrix.GetLength(1);
            var temp = new double[rows, cols];

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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[][] Cofactor(double[][] matrix)
        {
            var i = 0;
            var j = 0;
            var rows = matrix.Length;
            var cols = matrix[0].Length;
            var temp = new double[rows][];

            // Looping for each element of the matrix 
            for (var row = 0; row < rows; row++)
            {
                temp[row] = new double[cols];
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[,] Cofactor(double[,] matrix, int p, int q)
        {
            var i = 0;
            var j = 0;
            var rows = matrix.GetLength(0);
            var cols = matrix.GetLength(1);
            var temp = new double[rows, cols];

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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[][] Cofactor(double[][] matrix, int p, int q)
        {
            var i = 0;
            var j = 0;
            var rows = matrix.GetLength(0);
            var cols = matrix.GetLength(1);
            var temp = new double[rows][];

            // Looping for each element of the matrix 
            for (var row = 0; row < rows; row++)
            {
                temp[row] = new double[cols];
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
        /// The <see cref="Matrix2x2D" />.
        /// </returns>
        /// <acknowledgment>
        /// https://sites.google.com/site/physics2d/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2,
            double m2x1, double m2x2)
            CofactorMatrix(
            double m1x1, double m1x2,
            double m2x1, double m2x2)
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
        /// The <see cref="Matrix3x3D" />.
        /// </returns>
        /// <acknowledgment>
        /// https://sites.google.com/site/physics2d/
        /// This is an expanded version of the Ogre determinant() method, to give better performance in C#. Generated using a script.
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3)
            CofactorMatrix(
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3)
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
        /// The <see cref="Matrix4x4D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4)
            CofactorMatrix(
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4)
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5)
            CofactorMatrix(
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5)
        {
            var m = Cofactor(new double[,]
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6)
            CofactorMatrix(
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6)
        {
            var m = Cofactor(new double[,]
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[,] Inverse(double[,] matrix)
        {
            // Find determinant of [,]A 
            var det = Determinant(matrix);
            if (det == 0)
                throw new Exception("Singular matrix, can't find its inverse");

            // Find adjoint 
            var adj = Adjoint(matrix);

            var rows = matrix.GetLength(0);
            var cols = matrix.GetLength(1);
            var inverse = new double[rows, cols];

            // Find Inverse using formula "inverse(A) = adj(A)/det(A)" 
            for (var i = 0; i < rows; i++)
                for (var j = 0; j < cols; j++)
                    inverse[i, j] = adj[i, j] / det;

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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[][] Inverse(double[][] matrix)
        {
            // Find determinant of [,]A 
            var det = Determinant(matrix);
            if (det == 0)
                throw new Exception("Singular matrix, can't find its inverse");

            // Find adjoint 
            var adj = Adjoint(matrix);

            var rows = matrix.Length;
            var cols = matrix[0].Length;
            var inverse = new double[rows][];

            // Find Inverse using formula "inverse(A) = adj(A)/det(A)" 
            for (var i = 0; i < rows; i++)
            {
                inverse[i] = new double[cols];
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
        /// The <see cref="Matrix2x2D" />.
        /// </returns>
        /// <acknowledgment>
        /// https://sites.google.com/site/physics2d/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2,
            double m2x1, double m2x2)
            InverseMatrix(
            double m1x1, double m1x2,
            double m2x1, double m2x2)
        {
            var detInv = 1d / ((m1x1 * m2x2) - (m1x2 * m2x1));
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
        /// The <see cref="Matrix3x3D" />.
        /// </returns>
        /// <acknowledgment>
        /// https://sites.google.com/site/physics2d/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3)
            InverseMatrix(
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3)
        {
            var m11m22m12m21 = (m2x2 * m3x3) - (m2x3 * m3x2);
            var m10m22m12m20 = (m2x1 * m3x3) - (m2x3 * m3x1);
            var m10m21m11m20 = (m2x1 * m3x2) - (m2x2 * m3x1);

            var detInv = 1d / ((m1x1 * m11m22m12m21) - (m1x2 * m10m22m12m20) + (m1x3 * m10m21m11m20));

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
        /// The <see cref="Matrix4x4D" />.
        /// </returns>
        /// <acknowledgment>
        /// https://sites.google.com/site/physics2d/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4)
            InverseMatrix(
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4)
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

            var detInv = 1d /
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5)
            InverseMatrix(
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5)
        {
            var m = Inverse(new double[,]
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6)
            InverseMatrix(
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6)
        {
            var m = Inverse(new double[,]
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Determinant(double[,] matrix)
        //{
        //    var result = 0d;
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Determinant(double[][] matrix)
        {
            var result = 0d;
            if (matrix.Length == 2)
            {
                result = matrix[0][0] * matrix[1][1] - matrix[0][1] * matrix[1][0];
                return result;
            }
            for (var i = 0; i < matrix[0].Length; i++)
            {
                var temp = new double[matrix.Length - 1][];
                for (var j = 1; j < matrix.Length; j++)
                {
                    temp[j - 1] = new double[matrix[j].Length - 1];
                    Array.Copy(matrix[j], 0, temp[j - 1], 0, i);
                    Array.Copy(matrix[j], i + 1, temp[j - 1], i, matrix[j].Length - i - 1);
                }

                result += matrix[0][i] * Math.Pow(-1, i) * Determinant(temp);
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MatrixDeterminant(
            double m1x1, double m1x2,
            double m2x1, double m2x2)
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MatrixDeterminant(
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3)
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MatrixDeterminant(
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4)
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MatrixDeterminant(
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5)
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MatrixDeterminant(
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6)
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MatrixInverseDeterminant(
            double m1x1, double m1x2,
            double m2x1, double m2x2)
            => 1d / ((m1x1 * m2x2)
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MatrixInverseDeterminant(
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3)
            => 1d / ((m1x1 * MatrixDeterminant(m2x2, m2x3, m3x2, m3x3))
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MatrixInverseDeterminant(
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4)
            => 1d / ((m1x1 * MatrixDeterminant(m2x2, m2x3, m2x4, m3x2, m3x3, m3x4, m4x2, m4x3, m4x4))
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MatrixInverseDeterminant(
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5)
            => 1d / ((m1x1 * MatrixDeterminant(m2x2, m2x3, m2x4, m2x5, m3x2, m3x3, m3x4, m3x5, m4x2, m4x3, m4x4, m4x5, m5x2, m5x3, m5x4, m5x5))
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MatrixInverseDeterminant(
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6)
            => 1d / ((m1x1 * MatrixDeterminant(m2x2, m2x3, m2x4, m2x5, m2x6, m3x2, m3x3, m3x4, m3x5, m3x6, m4x2, m4x3, m4x4, m4x5, m4x6, m5x2, m5x3, m5x4, m5x5, m5x6, m6x2, m6x3, m6x4, m6x5, m6x6))
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[,] Lerp(double[,] a, double[,] b, double amount)
        {
            var aRows = a.GetLength(0);
            var bRows = b.GetLength(0);
            var aCols = a.GetLength(1);
            var bCols = b.GetLength(1);
            if (aRows != bRows || aCols != bCols) throw new Exception();

            var results = new double[aRows, bRows];
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[][] Lerp(double[][] a, double[][] b, double amount)
        {
            var aRows = a.Length;
            var bRows = b[0].Length;
            var aCols = a.Length;
            var bCols = b[0].Length;
            if (aRows != bRows || aCols != bCols) throw new Exception();

            var results = new double[aRows][];
            for (var i = 0; i < aRows; i++)
            {
                results[i] = new double[bRows];
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2,
            double m2x1, double m2x2)
            LerpMatrix2x2(
            double m1x1A, double m1x2A,
            double m2x1A, double m2x2A,
            double m1x1B, double m1x2B,
            double m2x1B, double m2x2B,
            double amount) => (
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3)
            LerpMatrix3x3(
            double m1x1A, double m1x2A, double m1x3A,
            double m2x1A, double m2x2A, double m2x3A,
            double m3x1A, double m3x2A, double m3x3A,
            double m1x1B, double m1x2B, double m1x3B,
            double m2x1B, double m2x2B, double m2x3B,
            double m3x1B, double m3x2B, double m3x3B,
            double amount) => (
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4)
            LerpMatrix4x4(
            double m1x1A, double m1x2A, double m1x3A, double m1x4A,
            double m2x1A, double m2x2A, double m2x3A, double m2x4A,
            double m3x1A, double m3x2A, double m3x3A, double m3x4A,
            double m4x1A, double m4x2A, double m4x3A, double m4x4A,
            double m1x1B, double m1x2B, double m1x3B, double m1x4B,
            double m2x1B, double m2x2B, double m2x3B, double m2x4B,
            double m3x1B, double m3x2B, double m3x3B, double m3x4B,
            double m4x1B, double m4x2B, double m4x3B, double m4x4B,
            double amount) => (
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5)
            LerpMatrix5x5(
            double m1x1A, double m1x2A, double m1x3A, double m1x4A, double m1x5A,
            double m2x1A, double m2x2A, double m2x3A, double m2x4A, double m2x5A,
            double m3x1A, double m3x2A, double m3x3A, double m3x4A, double m3x5A,
            double m4x1A, double m4x2A, double m4x3A, double m4x4A, double m4x5A,
            double m5x1A, double m5x2A, double m5x3A, double m5x4A, double m5x5A,
            double m1x1B, double m1x2B, double m1x3B, double m1x4B, double m1x5B,
            double m2x1B, double m2x2B, double m2x3B, double m2x4B, double m2x5B,
            double m3x1B, double m3x2B, double m3x3B, double m3x4B, double m3x5B,
            double m4x1B, double m4x2B, double m4x3B, double m4x4B, double m4x5B,
            double m5x1B, double m5x2B, double m5x3B, double m5x4B, double m5x5B,
            double amount) => (
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6)
            LerpMatrix6x6(
            double m1x1A, double m1x2A, double m1x3A, double m1x4A, double m1x5A, double m1x6A,
            double m2x1A, double m2x2A, double m2x3A, double m2x4A, double m2x5A, double m2x6A,
            double m3x1A, double m3x2A, double m3x3A, double m3x4A, double m3x5A, double m3x6A,
            double m4x1A, double m4x2A, double m4x3A, double m4x4A, double m4x5A, double m4x6A,
            double m5x1A, double m5x2A, double m5x3A, double m5x4A, double m5x5A, double m5x6A,
            double m6x1A, double m6x2A, double m6x3A, double m6x4A, double m6x5A, double m6x6A,
            double m1x1B, double m1x2B, double m1x3B, double m1x4B, double m1x5B, double m1x6B,
            double m2x1B, double m2x2B, double m2x3B, double m2x4B, double m2x5B, double m2x6B,
            double m3x1B, double m3x2B, double m3x3B, double m3x4B, double m3x5B, double m3x6B,
            double m4x1B, double m4x2B, double m4x3B, double m4x4B, double m4x5B, double m4x6B,
            double m5x1B, double m5x2B, double m5x3B, double m5x4B, double m5x5B, double m5x6B,
            double m6x1B, double m6x2B, double m6x3B, double m6x4B, double m6x5B, double m6x6B,
            double amount) => (
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(double[,] a, double[,] b)
        {
            var rowsA = a?.GetLength(0);
            var colsA = a?.GetLength(1);
            var rowsB = b?.GetLength(0);
            var colsB = b?.GetLength(1);

            if (rowsA != rowsB || colsA != colsB) throw new Exception();

            double result = 0;
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(double[][] a, double[][] b)
        {
            var rowsA = a?.Length;
            var colsA = a[0]?.Length;
            var rowsB = b?.Length;
            var colsB = b[0]?.Length;

            if (rowsA != rowsB || colsA != colsB) throw new Exception();

            double result = 0;
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProductMatrix2x2(
            double m1x1A, double m1x2A,
            double m2x1A, double m2x2A,
            double m1x1B, double m1x2B,
            double m2x1B, double m2x2B)
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProductMatrix3x3(
            double m1x1A, double m1x2A, double m1x3A,
            double m2x1A, double m2x2A, double m2x3A,
            double m3x1A, double m3x2A, double m3x3A,
            double m1x1B, double m1x2B, double m1x3B,
            double m2x1B, double m2x2B, double m2x3B,
            double m3x1B, double m3x2B, double m3x3B)
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProductMatrix4x4(
            double m1x1A, double m1x2A, double m1x3A, double m1x4A,
            double m2x1A, double m2x2A, double m2x3A, double m2x4A,
            double m3x1A, double m3x2A, double m3x3A, double m3x4A,
            double m4x1A, double m4x2A, double m4x3A, double m4x4A,
            double m1x1B, double m1x2B, double m1x3B, double m1x4B,
            double m2x1B, double m2x2B, double m2x3B, double m2x4B,
            double m3x1B, double m3x2B, double m3x3B, double m3x4B,
            double m4x1B, double m4x2B, double m4x3B, double m4x4B)
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProductMatrix5x5(
            double m1x1A, double m1x2A, double m1x3A, double m1x4A, double m1x5A,
            double m2x1A, double m2x2A, double m2x3A, double m2x4A, double m2x5A,
            double m3x1A, double m3x2A, double m3x3A, double m3x4A, double m3x5A,
            double m4x1A, double m4x2A, double m4x3A, double m4x4A, double m4x5A,
            double m5x1A, double m5x2A, double m5x3A, double m5x4A, double m5x5A,
            double m1x1B, double m1x2B, double m1x3B, double m1x4B, double m1x5B,
            double m2x1B, double m2x2B, double m2x3B, double m2x4B, double m2x5B,
            double m3x1B, double m3x2B, double m3x3B, double m3x4B, double m3x5B,
            double m4x1B, double m4x2B, double m4x3B, double m4x4B, double m4x5B,
            double m5x1B, double m5x2B, double m5x3B, double m5x4B, double m5x5B)
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProductMatrix6x6(
            double m1x1A, double m1x2A, double m1x3A, double m1x4A, double m1x5A, double m1x6A,
            double m2x1A, double m2x2A, double m2x3A, double m2x4A, double m2x5A, double m2x6A,
            double m3x1A, double m3x2A, double m3x3A, double m3x4A, double m3x5A, double m3x6A,
            double m4x1A, double m4x2A, double m4x3A, double m4x4A, double m4x5A, double m4x6A,
            double m5x1A, double m5x2A, double m5x3A, double m5x4A, double m5x5A, double m5x6A,
            double m6x1A, double m6x2A, double m6x3A, double m6x4A, double m6x5A, double m6x6A,
            double m1x1B, double m1x2B, double m1x3B, double m1x4B, double m1x5B, double m1x6B,
            double m2x1B, double m2x2B, double m2x3B, double m2x4B, double m2x5B, double m2x6B,
            double m3x1B, double m3x2B, double m3x3B, double m3x4B, double m3x5B, double m3x6B,
            double m4x1B, double m4x2B, double m4x3B, double m4x4B, double m4x5B, double m4x6B,
            double m5x1B, double m5x2B, double m5x3B, double m5x4B, double m5x5B, double m5x6B,
            double m6x1B, double m6x2B, double m6x3B, double m6x4B, double m6x5B, double m6x6B)
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double[,] Lower, double[,] Upper) DecomposeToLowerUpper(double[,] matrix)
        {
            var rows = matrix.GetLength(0);
            var cols = matrix.GetLength(1);

            if (!IsSquareMatrix(matrix))
            {
                return (new double[1, 1], new double[1, 1]);
            }
            else
            {
                var lower = new double[rows, cols];
                var upper = new double[rows, cols];

                for (var i = 0; i < rows; i++)
                {
                    lower[i, i] = 1;
                }

                for (var i = 0; i < rows; i++)
                {
                    for (var j = i; j < rows; j++)
                    {
                        var sumU = 0d;
                        for (var k = 0; k < i; k++)
                        {
                            sumU += lower[i, k] * upper[k, j];
                        }

                        upper[i, j] = matrix[i, j] - sumU;
                    }

                    for (var j = i; j < rows; j++)
                    {
                        var sumL = 0d;
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double[][] Lower, double[][] Upper) DecomposeToLowerUpper(double[][] matrix)
        {
            var rows = matrix.Length;
            var cols = matrix[0].Length;

            if (!IsSquareMatrix(matrix))
            {
                return (new double[1][] { new double[1] }, new double[1][] { new double[1] });
            }
            else
            {
                var lower = new double[rows][];
                var upper = new double[rows][];

                for (var i = 0; i < rows; i++)
                {
                    lower[i] = new double[cols];
                    lower[i][i] = 1;
                }

                for (var i = 0; i < rows; i++)
                {
                    upper[i] = new double[cols];
                    for (var j = i; j < rows; j++)
                    {
                        var sumU = 0d;
                        for (var k = 0; k < i; k++)
                        {
                            sumU += lower[i][k] * upper[k][j];
                        }

                        upper[i][j] = matrix[i][j] - sumU;
                    }

                    for (var j = i; j < rows; j++)
                    {
                        var sumL = 0d;
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
}
