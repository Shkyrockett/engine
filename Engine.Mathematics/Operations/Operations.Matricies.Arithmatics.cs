// <copyright file="Operations.Matricies.Arithmatics.cs" company="Shkyrockett" >
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
        /// Applies the plus operator to the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://www.geeksforgeeks.org/different-operation-matrices/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[,] Plus(double[,] augend)
        {
            if (augend is null)
            {
               return augend;
            }

            var rows = augend.GetLength(0);
            var cols = augend.GetLength(1);

            var result = new double[rows, cols];
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    result[i, j] = +augend[i, j];
                }
            }

            return result;
        }

        #region Add
        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://www.geeksforgeeks.org/different-operation-matrices/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[,] Add(double[,] augend, double[,] addend)
        {
            if (augend is null)
            {
                return addend;
            }

            if (addend is null)
            {
                return augend;
            }

            var rows = augend.GetLength(0);
            var cols = augend.GetLength(1);

            var result = new double[rows, cols];
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    result[i, j] = augend[i, j] + addend[i, j];
                }
            }

            return result;
        }

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
        /// <returns></returns>
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
            double addendScaleX, double addendScaleY) => (
                augendX + addendX, augendY + addendY,
                augendSkewX + addendSkewX, augendSkewY + addendSkewY,
                augendScaleX * addendScaleX, augendScaleY * addendScaleY);

        /// <summary>
        /// Used to add two matrices together.
        /// </summary>
        /// <param name="augendM1x1">The augend M1X1.</param>
        /// <param name="augendM1x2">The augend M1X2.</param>
        /// <param name="augendM2x1">The augend M2X1.</param>
        /// <param name="augendM2x2">The augend M2X2.</param>
        /// <param name="addendM1x1">The addend M1X1.</param>
        /// <param name="addendM1x2">The addend M1X2.</param>
        /// <param name="addendM2x1">The addend M2X1.</param>
        /// <param name="addendM2x2">The addend M2X2.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2,
            double m2x1, double m2x2
            ) AddMatrix(
            double augendM1x1, double augendM1x2,
            double augendM2x1, double augendM2x2,
            double addendM1x1, double addendM1x2,
            double addendM2x1, double addendM2x2)
            => (augendM1x1 + addendM1x1, augendM1x2 + addendM1x2,
                augendM2x1 + addendM2x1, augendM2x2 + addendM2x2);

        /// <summary>
        /// Used to add two matrices together.
        /// </summary>
        /// <param name="augendM1x1">The augend M1X1.</param>
        /// <param name="augendM1x2">The augend M1X2.</param>
        /// <param name="augendM1x3">The augend M1X3.</param>
        /// <param name="augendM2x1">The augend M2X1.</param>
        /// <param name="augendM2x2">The augend M2X2.</param>
        /// <param name="augendM2x3">The augend M2X3.</param>
        /// <param name="augendM3x1">The augend M3X1.</param>
        /// <param name="augendM3x2">The augend M3X2.</param>
        /// <param name="augendM3x3">The augend M3X3.</param>
        /// <param name="addendM1x1">The addend M1X1.</param>
        /// <param name="addendM1x2">The addend M1X2.</param>
        /// <param name="addendM1x3">The addend M1X3.</param>
        /// <param name="addendM2x1">The addend M2X1.</param>
        /// <param name="addendM2x2">The addend M2X2.</param>
        /// <param name="addendM2x3">The addend M2X3.</param>
        /// <param name="addendM3x1">The addend M3X1.</param>
        /// <param name="addendM3x2">The addend M3X2.</param>
        /// <param name="addendM3x3">The addend M3X3.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3
            ) AddMatrix(
            double augendM1x1, double augendM1x2, double augendM1x3,
            double augendM2x1, double augendM2x2, double augendM2x3,
            double augendM3x1, double augendM3x2, double augendM3x3,
            double addendM1x1, double addendM1x2, double addendM1x3,
            double addendM2x1, double addendM2x2, double addendM2x3,
            double addendM3x1, double addendM3x2, double addendM3x3)
            => (augendM1x1 + addendM1x1, augendM1x2 + addendM1x2, augendM1x3 + addendM1x3,
                augendM2x1 + addendM2x1, augendM2x2 + addendM2x2, augendM2x3 + addendM2x3,
                augendM3x1 + addendM3x1, augendM3x2 + addendM3x2, augendM3x3 + addendM3x3);

        /// <summary>
        /// Used to add two matrices together.
        /// </summary>
        /// <param name="augendM1x1">The augend M1X1.</param>
        /// <param name="augendM1x2">The augend M1X2.</param>
        /// <param name="augendM1x3">The augend M1X3.</param>
        /// <param name="augendM1x4">The augend M1X4.</param>
        /// <param name="augendM2x1">The augend M2X1.</param>
        /// <param name="augendM2x2">The augend M2X2.</param>
        /// <param name="augendM2x3">The augend M2X3.</param>
        /// <param name="augendM2x4">The augend M2X4.</param>
        /// <param name="augendM3x1">The augend M3X1.</param>
        /// <param name="augendM3x2">The augend M3X2.</param>
        /// <param name="augendM3x3">The augend M3X3.</param>
        /// <param name="augendM3x4">The augend M3X4.</param>
        /// <param name="augendM4x1">The augend M4X1.</param>
        /// <param name="augendM4x2">The augend M4X2.</param>
        /// <param name="augendM4x3">The augend M4X3.</param>
        /// <param name="augendM4x4">The augend M4X4.</param>
        /// <param name="addendM1x1">The addend M1X1.</param>
        /// <param name="addendM1x2">The addend M1X2.</param>
        /// <param name="addendM1x3">The addend M1X3.</param>
        /// <param name="addendM1x4">The addend M1X4.</param>
        /// <param name="addendM2x1">The addend M2X1.</param>
        /// <param name="addendM2x2">The addend M2X2.</param>
        /// <param name="addendM2x3">The addend M2X3.</param>
        /// <param name="addendM2x4">The addend M2X4.</param>
        /// <param name="addendM3x1">The addend M3X1.</param>
        /// <param name="addendM3x2">The addend M3X2.</param>
        /// <param name="addendM3x3">The addend M3X3.</param>
        /// <param name="addendM3x4">The addend M3X4.</param>
        /// <param name="addendM4x1">The addend M4X1.</param>
        /// <param name="addendM4x2">The addend M4X2.</param>
        /// <param name="addendM4x3">The addend M4X3.</param>
        /// <param name="addendM4x4">The addend M4X4.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4
            ) AddMatrix(
            double augendM1x1, double augendM1x2, double augendM1x3, double augendM1x4,
            double augendM2x1, double augendM2x2, double augendM2x3, double augendM2x4,
            double augendM3x1, double augendM3x2, double augendM3x3, double augendM3x4,
            double augendM4x1, double augendM4x2, double augendM4x3, double augendM4x4,
            double addendM1x1, double addendM1x2, double addendM1x3, double addendM1x4,
            double addendM2x1, double addendM2x2, double addendM2x3, double addendM2x4,
            double addendM3x1, double addendM3x2, double addendM3x3, double addendM3x4,
            double addendM4x1, double addendM4x2, double addendM4x3, double addendM4x4)
            => (augendM1x1 + addendM1x1, augendM1x2 + addendM1x2, augendM1x3 + addendM1x3, augendM1x4 + addendM1x4,
                augendM2x1 + addendM2x1, augendM2x2 + addendM2x2, augendM2x3 + addendM2x3, augendM2x4 + addendM2x4,
                augendM3x1 + addendM3x1, augendM3x2 + addendM3x2, augendM3x3 + addendM3x3, augendM3x4 + addendM3x4,
                augendM4x1 + addendM4x1, augendM4x2 + addendM4x2, augendM4x3 + addendM4x3, augendM4x4 + addendM4x4);

        /// <summary>
        /// Used to add two matrices together.
        /// </summary>
        /// <param name="augendM1x1">The augend M1X1.</param>
        /// <param name="augendM1x2">The augend M1X2.</param>
        /// <param name="augendM1x3">The augend M1X3.</param>
        /// <param name="augendM1x4">The augend M1X4.</param>
        /// <param name="augendM1x5">The augend M1X5.</param>
        /// <param name="augendM2x1">The augend M2X1.</param>
        /// <param name="augendM2x2">The augend M2X2.</param>
        /// <param name="augendM2x3">The augend M2X3.</param>
        /// <param name="augendM2x4">The augend M2X4.</param>
        /// <param name="augendM2x5">The augend M2X5.</param>
        /// <param name="augendM3x1">The augend M3X1.</param>
        /// <param name="augendM3x2">The augend M3X2.</param>
        /// <param name="augendM3x3">The augend M3X3.</param>
        /// <param name="augendM3x4">The augend M3X4.</param>
        /// <param name="augendM3x5">The augend M3X5.</param>
        /// <param name="augendM4x1">The augend M4X1.</param>
        /// <param name="augendM4x2">The augend M4X2.</param>
        /// <param name="augendM4x3">The augend M4X3.</param>
        /// <param name="augendM4x4">The augend M4X4.</param>
        /// <param name="augendM4x5">The augend M4X5.</param>
        /// <param name="augendM5x1">The augend M5X1.</param>
        /// <param name="augendM5x2">The augend M5X2.</param>
        /// <param name="augendM5x3">The augend M5X3.</param>
        /// <param name="augendM5x4">The augend M5X4.</param>
        /// <param name="augendM5x5">The augend M5X5.</param>
        /// <param name="addendM1x1">The addend M1X1.</param>
        /// <param name="addendM1x2">The addend M1X2.</param>
        /// <param name="addendM1x3">The addend M1X3.</param>
        /// <param name="addendM1x4">The addend M1X4.</param>
        /// <param name="addendM1x5">The addend M1X5.</param>
        /// <param name="addendM2x1">The addend M2X1.</param>
        /// <param name="addendM2x2">The addend M2X2.</param>
        /// <param name="addendM2x3">The addend M2X3.</param>
        /// <param name="addendM2x4">The addend M2X4.</param>
        /// <param name="addendM2x5">The addend M2X5.</param>
        /// <param name="addendM3x1">The addend M3X1.</param>
        /// <param name="addendM3x2">The addend M3X2.</param>
        /// <param name="addendM3x3">The addend M3X3.</param>
        /// <param name="addendM3x4">The addend M3X4.</param>
        /// <param name="addendM3x5">The addend M3X5.</param>
        /// <param name="addendM4x1">The addend M4X1.</param>
        /// <param name="addendM4x2">The addend M4X2.</param>
        /// <param name="addendM4x3">The addend M4X3.</param>
        /// <param name="addendM4x4">The addend M4X4.</param>
        /// <param name="addendM4x5">The addend M4X5.</param>
        /// <param name="addendM5x1">The addend M5X1.</param>
        /// <param name="addendM5x2">The addend M5X2.</param>
        /// <param name="addendM5x3">The addend M5X3.</param>
        /// <param name="addendM5x4">The addend M5X4.</param>
        /// <param name="addendM5x5">The addend M5X5.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5
            ) AddMatrix(
            double augendM1x1, double augendM1x2, double augendM1x3, double augendM1x4, double augendM1x5,
            double augendM2x1, double augendM2x2, double augendM2x3, double augendM2x4, double augendM2x5,
            double augendM3x1, double augendM3x2, double augendM3x3, double augendM3x4, double augendM3x5,
            double augendM4x1, double augendM4x2, double augendM4x3, double augendM4x4, double augendM4x5,
            double augendM5x1, double augendM5x2, double augendM5x3, double augendM5x4, double augendM5x5,
            double addendM1x1, double addendM1x2, double addendM1x3, double addendM1x4, double addendM1x5,
            double addendM2x1, double addendM2x2, double addendM2x3, double addendM2x4, double addendM2x5,
            double addendM3x1, double addendM3x2, double addendM3x3, double addendM3x4, double addendM3x5,
            double addendM4x1, double addendM4x2, double addendM4x3, double addendM4x4, double addendM4x5,
            double addendM5x1, double addendM5x2, double addendM5x3, double addendM5x4, double addendM5x5)
            => (augendM1x1 + addendM1x1, augendM1x2 + addendM1x2, augendM1x3 + addendM1x3, augendM1x4 + addendM1x4, augendM1x5 + addendM1x5,
                augendM2x1 + addendM2x1, augendM2x2 + addendM2x2, augendM2x3 + addendM2x3, augendM2x4 + addendM2x4, augendM2x5 + addendM2x5,
                augendM3x1 + addendM3x1, augendM3x2 + addendM3x2, augendM3x3 + addendM3x3, augendM3x4 + addendM3x4, augendM3x5 + addendM3x5,
                augendM4x1 + addendM4x1, augendM4x2 + addendM4x2, augendM4x3 + addendM4x3, augendM4x4 + addendM4x4, augendM4x5 + addendM4x5,
                augendM5x1 + addendM5x1, augendM5x2 + addendM5x2, augendM5x3 + addendM5x3, augendM5x4 + addendM5x4, augendM5x5 + addendM5x5);

        /// <summary>
        /// Used to add two matrices together.
        /// </summary>
        /// <param name="augendM1x1">The augend M1X1.</param>
        /// <param name="augendM1x2">The augend M1X2.</param>
        /// <param name="augendM1x3">The augend M1X3.</param>
        /// <param name="augendM1x4">The augend M1X4.</param>
        /// <param name="augendM1x5">The augend M1X5.</param>
        /// <param name="augendM1x6">The augend M1X6.</param>
        /// <param name="augendM2x1">The augend M2X1.</param>
        /// <param name="augendM2x2">The augend M2X2.</param>
        /// <param name="augendM2x3">The augend M2X3.</param>
        /// <param name="augendM2x4">The augend M2X4.</param>
        /// <param name="augendM2x5">The augend M2X5.</param>
        /// <param name="augendM2x6">The augend M2X6.</param>
        /// <param name="augendM3x1">The augend M3X1.</param>
        /// <param name="augendM3x2">The augend M3X2.</param>
        /// <param name="augendM3x3">The augend M3X3.</param>
        /// <param name="augendM3x4">The augend M3X4.</param>
        /// <param name="augendM3x5">The augend M3X5.</param>
        /// <param name="augendM3x6">The augend M3X6.</param>
        /// <param name="augendM4x1">The augend M4X1.</param>
        /// <param name="augendM4x2">The augend M4X2.</param>
        /// <param name="augendM4x3">The augend M4X3.</param>
        /// <param name="augendM4x4">The augend M4X4.</param>
        /// <param name="augendM4x5">The augend M4X5.</param>
        /// <param name="augendM4x6">The augend M4X6.</param>
        /// <param name="augendM5x1">The augend M5X1.</param>
        /// <param name="augendM5x2">The augend M5X2.</param>
        /// <param name="augendM5x3">The augend M5X3.</param>
        /// <param name="augendM5x4">The augend M5X4.</param>
        /// <param name="augendM5x5">The augend M5X5.</param>
        /// <param name="augendM5x6">The augend M5X6.</param>
        /// <param name="augendM6x1">The augend M6X1.</param>
        /// <param name="augendM6x2">The augend M6X2.</param>
        /// <param name="augendM6x3">The augend M6X3.</param>
        /// <param name="augendM6x4">The augend M6X4.</param>
        /// <param name="augendM6x5">The augend M6X5.</param>
        /// <param name="augendM6x6">The augend M6X6.</param>
        /// <param name="addendM1x1">The addend M1X1.</param>
        /// <param name="addendM1x2">The addend M1X2.</param>
        /// <param name="addendM1x3">The addend M1X3.</param>
        /// <param name="addendM1x4">The addend M1X4.</param>
        /// <param name="addendM1x5">The addend M1X5.</param>
        /// <param name="addendM1x6">The addend M1X6.</param>
        /// <param name="addendM2x1">The addend M2X1.</param>
        /// <param name="addendM2x2">The addend M2X2.</param>
        /// <param name="addendM2x3">The addend M2X3.</param>
        /// <param name="addendM2x4">The addend M2X4.</param>
        /// <param name="addendM2x5">The addend M2X5.</param>
        /// <param name="addendM2x6">The addend M2X6.</param>
        /// <param name="addendM3x1">The addend M3X1.</param>
        /// <param name="addendM3x2">The addend M3X2.</param>
        /// <param name="addendM3x3">The addend M3X3.</param>
        /// <param name="addendM3x4">The addend M3X4.</param>
        /// <param name="addendM3x5">The addend M3X5.</param>
        /// <param name="addendM3x6">The addend M3X6.</param>
        /// <param name="addendM4x1">The addend M4X1.</param>
        /// <param name="addendM4x2">The addend M4X2.</param>
        /// <param name="addendM4x3">The addend M4X3.</param>
        /// <param name="addendM4x4">The addend M4X4.</param>
        /// <param name="addendM4x5">The addend M4X5.</param>
        /// <param name="addendM4x6">The addend M4X6.</param>
        /// <param name="addendM5x1">The addend M5X1.</param>
        /// <param name="addendM5x2">The addend M5X2.</param>
        /// <param name="addendM5x3">The addend M5X3.</param>
        /// <param name="addendM5x4">The addend M5X4.</param>
        /// <param name="addendM5x5">The addend M5X5.</param>
        /// <param name="addendM5x6">The addend M5X6.</param>
        /// <param name="addendM6x1">The addend M6X1.</param>
        /// <param name="addendM6x2">The addend M6X2.</param>
        /// <param name="addendM6x3">The addend M6X3.</param>
        /// <param name="addendM6x4">The addend M6X4.</param>
        /// <param name="addendM6x5">The addend M6X5.</param>
        /// <param name="addendM6x6">The addend M6X6.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6)
            AddMatrix(
            double augendM1x1, double augendM1x2, double augendM1x3, double augendM1x4, double augendM1x5, double augendM1x6,
            double augendM2x1, double augendM2x2, double augendM2x3, double augendM2x4, double augendM2x5, double augendM2x6,
            double augendM3x1, double augendM3x2, double augendM3x3, double augendM3x4, double augendM3x5, double augendM3x6,
            double augendM4x1, double augendM4x2, double augendM4x3, double augendM4x4, double augendM4x5, double augendM4x6,
            double augendM5x1, double augendM5x2, double augendM5x3, double augendM5x4, double augendM5x5, double augendM5x6,
            double augendM6x1, double augendM6x2, double augendM6x3, double augendM6x4, double augendM6x5, double augendM6x6,
            double addendM1x1, double addendM1x2, double addendM1x3, double addendM1x4, double addendM1x5, double addendM1x6,
            double addendM2x1, double addendM2x2, double addendM2x3, double addendM2x4, double addendM2x5, double addendM2x6,
            double addendM3x1, double addendM3x2, double addendM3x3, double addendM3x4, double addendM3x5, double addendM3x6,
            double addendM4x1, double addendM4x2, double addendM4x3, double addendM4x4, double addendM4x5, double addendM4x6,
            double addendM5x1, double addendM5x2, double addendM5x3, double addendM5x4, double addendM5x5, double addendM5x6,
            double addendM6x1, double addendM6x2, double addendM6x3, double addendM6x4, double addendM6x5, double addendM6x6)
            => (augendM1x1 + addendM1x1, augendM1x2 + addendM1x2, augendM1x3 + addendM1x3, augendM1x4 + addendM1x4, augendM1x5 + addendM1x5, augendM1x6 + addendM1x6,
                augendM2x1 + addendM2x1, augendM2x2 + addendM2x2, augendM2x3 + addendM2x3, augendM2x4 + addendM2x4, augendM2x5 + addendM2x5, augendM2x6 + addendM2x6,
                augendM3x1 + addendM3x1, augendM3x2 + addendM3x2, augendM3x3 + addendM3x3, augendM3x4 + addendM3x4, augendM3x5 + addendM3x5, augendM3x6 + addendM3x6,
                augendM4x1 + addendM4x1, augendM4x2 + addendM4x2, augendM4x3 + addendM4x3, augendM4x4 + addendM4x4, augendM4x5 + addendM4x5, augendM4x6 + addendM4x6,
                augendM5x1 + addendM5x1, augendM5x2 + addendM5x2, augendM5x3 + addendM5x3, augendM5x4 + addendM5x4, augendM5x5 + addendM5x5, augendM5x6 + addendM5x6,
                augendM6x1 + addendM6x1, augendM6x2 + addendM6x2, augendM6x3 + addendM6x3, augendM6x4 + addendM6x4, augendM6x5 + addendM6x5, augendM6x6 + addendM6x6);
        #endregion Add

        /// <summary>
        /// Applies the plus operator to the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://www.geeksforgeeks.org/different-operation-matrices/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[,] Negate(double[,] augend)
        {
            var rows = augend.GetLength(0);
            var cols = augend.GetLength(1);

            var result = new double[rows, cols];
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    result[i, j] = -augend[i, j];
                }
            }

            return result;
        }

        #region Subtract
        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subtrahend">The subtrahend.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://www.geeksforgeeks.org/different-operation-matrices/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[,] Subtract(double[,] minuend, double[,] subtrahend)
        {
            var rows = minuend.GetLength(0);
            var cols = minuend.GetLength(1);

            var result = new double[rows, cols];
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    result[i, j] = minuend[i, j] - subtrahend[i, j];
                }
            }

            return result;
        }

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
        /// <param name="minuendM1x1">The minuend M1X1.</param>
        /// <param name="minuendM1x2">The minuend M1X2.</param>
        /// <param name="minuendM2x1">The minuend M2X1.</param>
        /// <param name="minuendM2x2">The minuend M2X2.</param>
        /// <param name="subtrahendM1x1">The subtrahend M1X1.</param>
        /// <param name="subtrahendM1x2">The subtrahend M1X2.</param>
        /// <param name="subtrahendM2x1">The subtrahend M2X1.</param>
        /// <param name="subtrahendM2x2">The subtrahend M2X2.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2,
            double m2x1, double m2x2
            ) Subtract2x2x2x2(
            double minuendM1x1, double minuendM1x2,
            double minuendM2x1, double minuendM2x2,
            double subtrahendM1x1, double subtrahendM1x2,
            double subtrahendM2x1, double subtrahendM2x2)
            => (minuendM1x1 - subtrahendM1x1, minuendM1x2 - subtrahendM1x2,
                minuendM2x1 - subtrahendM2x1, minuendM2x2 - subtrahendM2x2);

        /// <summary>
        /// Used to subtract two matrices.
        /// </summary>
        /// <param name="minuendM1x1">The minuend M1X1.</param>
        /// <param name="minuendM1x2">The minuend M1X2.</param>
        /// <param name="minuendM1x3">The minuend M1X3.</param>
        /// <param name="minuendM2x1">The minuend M2X1.</param>
        /// <param name="minuendM2x2">The minuend M2X2.</param>
        /// <param name="minuendM2x3">The minuend M2X3.</param>
        /// <param name="minuendM3x1">The minuend M3X1.</param>
        /// <param name="minuendM3x2">The minuend M3X2.</param>
        /// <param name="minuendM3x3">The minuend M3X3.</param>
        /// <param name="subtrahendM1x1">The subtrahend M1X1.</param>
        /// <param name="subtrahendM1x2">The subtrahend M1X2.</param>
        /// <param name="subtrahendM1x3">The subtrahend M1X3.</param>
        /// <param name="subtrahendM2x1">The subtrahend M2X1.</param>
        /// <param name="subtrahendM2x2">The subtrahend M2X2.</param>
        /// <param name="subtrahendM2x3">The subtrahend M2X3.</param>
        /// <param name="subtrahendM3x1">The subtrahend M3X1.</param>
        /// <param name="subtrahendM3x2">The subtrahend M3X2.</param>
        /// <param name="subtrahendM3x3">The subtrahend M3X3.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3
            ) Subtract3x3x3x3(
            double minuendM1x1, double minuendM1x2, double minuendM1x3,
            double minuendM2x1, double minuendM2x2, double minuendM2x3,
            double minuendM3x1, double minuendM3x2, double minuendM3x3,
            double subtrahendM1x1, double subtrahendM1x2, double subtrahendM1x3,
            double subtrahendM2x1, double subtrahendM2x2, double subtrahendM2x3,
            double subtrahendM3x1, double subtrahendM3x2, double subtrahendM3x3)
            => (minuendM1x1 - subtrahendM1x1, minuendM1x2 - subtrahendM1x2, minuendM1x3 - subtrahendM1x3,
                minuendM2x1 - subtrahendM2x1, minuendM2x2 - subtrahendM2x2, minuendM2x3 - subtrahendM2x3,
                minuendM3x1 - subtrahendM3x1, minuendM3x2 - subtrahendM3x2, minuendM3x3 - subtrahendM3x3);

        /// <summary>
        /// Used to subtract two matrices.
        /// </summary>
        /// <param name="minuendM1x1">The minuend M1X1.</param>
        /// <param name="minuendM1x2">The minuend M1X2.</param>
        /// <param name="minuendM1x3">The minuend M1X3.</param>
        /// <param name="minuendM1x4">The minuend M1X4.</param>
        /// <param name="minuendM2x1">The minuend M2X1.</param>
        /// <param name="minuendM2x2">The minuend M2X2.</param>
        /// <param name="minuendM2x3">The minuend M2X3.</param>
        /// <param name="minuendM2x4">The minuend M2X4.</param>
        /// <param name="minuendM3x1">The minuend M3X1.</param>
        /// <param name="minuendM3x2">The minuend M3X2.</param>
        /// <param name="minuendM3x3">The minuend M3X3.</param>
        /// <param name="minuendM3x4">The minuend M3X4.</param>
        /// <param name="minuendM4x1">The minuend M4X1.</param>
        /// <param name="minuendM4x2">The minuend M4X2.</param>
        /// <param name="minuendM4x3">The minuend M4X3.</param>
        /// <param name="minuendM4x4">The minuend M4X4.</param>
        /// <param name="subtrahendM1x1">The subtrahend M1X1.</param>
        /// <param name="subtrahendM1x2">The subtrahend M1X2.</param>
        /// <param name="subtrahendM1x3">The subtrahend M1X3.</param>
        /// <param name="subtrahendM1x4">The subtrahend M1X4.</param>
        /// <param name="subtrahendM2x1">The subtrahend M2X1.</param>
        /// <param name="subtrahendM2x2">The subtrahend M2X2.</param>
        /// <param name="subtrahendM2x3">The subtrahend M2X3.</param>
        /// <param name="subtrahendM2x4">The subtrahend M2X4.</param>
        /// <param name="subtrahendM3x1">The subtrahend M3X1.</param>
        /// <param name="subtrahendM3x2">The subtrahend M3X2.</param>
        /// <param name="subtrahendM3x3">The subtrahend M3X3.</param>
        /// <param name="subtrahendM3x4">The subtrahend M3X4.</param>
        /// <param name="subtrahendM4x1">The subtrahend M4X1.</param>
        /// <param name="subtrahendM4x2">The subtrahend M4X2.</param>
        /// <param name="subtrahendM4x3">The subtrahend M4X3.</param>
        /// <param name="subtrahendM4x4">The subtrahend M4X4.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4)
            Subtract4x4x4x4(
            double minuendM1x1, double minuendM1x2, double minuendM1x3, double minuendM1x4,
            double minuendM2x1, double minuendM2x2, double minuendM2x3, double minuendM2x4,
            double minuendM3x1, double minuendM3x2, double minuendM3x3, double minuendM3x4,
            double minuendM4x1, double minuendM4x2, double minuendM4x3, double minuendM4x4,
            double subtrahendM1x1, double subtrahendM1x2, double subtrahendM1x3, double subtrahendM1x4,
            double subtrahendM2x1, double subtrahendM2x2, double subtrahendM2x3, double subtrahendM2x4,
            double subtrahendM3x1, double subtrahendM3x2, double subtrahendM3x3, double subtrahendM3x4,
            double subtrahendM4x1, double subtrahendM4x2, double subtrahendM4x3, double subtrahendM4x4)
            => (minuendM1x1 - subtrahendM1x1, minuendM1x2 - subtrahendM1x2, minuendM1x3 - subtrahendM1x3, minuendM1x4 - subtrahendM1x4,
                minuendM2x1 - subtrahendM2x1, minuendM2x2 - subtrahendM2x2, minuendM2x3 - subtrahendM2x3, minuendM2x4 - subtrahendM2x4,
                minuendM3x1 - subtrahendM3x1, minuendM3x2 - subtrahendM3x2, minuendM3x3 - subtrahendM3x3, minuendM3x4 - subtrahendM3x4,
                minuendM4x1 - subtrahendM4x1, minuendM4x2 - subtrahendM4x2, minuendM4x3 - subtrahendM4x3, minuendM4x4 - subtrahendM4x4);

        /// <summary>
        /// Used to subtract two matrices.
        /// </summary>
        /// <param name="minuendM1x1">The minuend M1X1.</param>
        /// <param name="minuendM1x2">The minuend M1X2.</param>
        /// <param name="minuendM1x3">The minuend M1X3.</param>
        /// <param name="minuendM1x4">The minuend M1X4.</param>
        /// <param name="minuendM1x5">The minuend M1X5.</param>
        /// <param name="minuendM2x1">The minuend M2X1.</param>
        /// <param name="minuendM2x2">The minuend M2X2.</param>
        /// <param name="minuendM2x3">The minuend M2X3.</param>
        /// <param name="minuendM2x4">The minuend M2X4.</param>
        /// <param name="minuendM2x5">The minuend M2X5.</param>
        /// <param name="minuendM3x1">The minuend M3X1.</param>
        /// <param name="minuendM3x2">The minuend M3X2.</param>
        /// <param name="minuendM3x3">The minuend M3X3.</param>
        /// <param name="minuendM3x4">The minuend M3X4.</param>
        /// <param name="minuendM3x5">The minuend M3X5.</param>
        /// <param name="minuendM4x1">The minuend M4X1.</param>
        /// <param name="minuendM4x2">The minuend M4X2.</param>
        /// <param name="minuendM4x3">The minuend M4X3.</param>
        /// <param name="minuendM4x4">The minuend M4X4.</param>
        /// <param name="minuendM4x5">The minuend M4X5.</param>
        /// <param name="minuendM5x1">The minuend M5X1.</param>
        /// <param name="minuendM5x2">The minuend M5X2.</param>
        /// <param name="minuendM5x3">The minuend M5X3.</param>
        /// <param name="minuendM5x4">The minuend M5X4.</param>
        /// <param name="minuendM5x5">The minuend M5X5.</param>
        /// <param name="subtrahendM1x1">The subtrahend M1X1.</param>
        /// <param name="subtrahendM1x2">The subtrahend M1X2.</param>
        /// <param name="subtrahendM1x3">The subtrahend M1X3.</param>
        /// <param name="subtrahendM1x4">The subtrahend M1X4.</param>
        /// <param name="subtrahendM1x5">The subtrahend M1X5.</param>
        /// <param name="subtrahendM2x1">The subtrahend M2X1.</param>
        /// <param name="subtrahendM2x2">The subtrahend M2X2.</param>
        /// <param name="subtrahendM2x3">The subtrahend M2X3.</param>
        /// <param name="subtrahendM2x4">The subtrahend M2X4.</param>
        /// <param name="subtrahendM2x5">The subtrahend M2X5.</param>
        /// <param name="subtrahendM3x1">The subtrahend M3X1.</param>
        /// <param name="subtrahendM3x2">The subtrahend M3X2.</param>
        /// <param name="subtrahendM3x3">The subtrahend M3X3.</param>
        /// <param name="subtrahendM3x4">The subtrahend M3X4.</param>
        /// <param name="subtrahendM3x5">The subtrahend M3X5.</param>
        /// <param name="subtrahendM4x1">The subtrahend M4X1.</param>
        /// <param name="subtrahendM4x2">The subtrahend M4X2.</param>
        /// <param name="subtrahendM4x3">The subtrahend M4X3.</param>
        /// <param name="subtrahendM4x4">The subtrahend M4X4.</param>
        /// <param name="subtrahendM4x5">The subtrahend M4X5.</param>
        /// <param name="subtrahendM5x1">The subtrahend M5X1.</param>
        /// <param name="subtrahendM5x2">The subtrahend M5X2.</param>
        /// <param name="subtrahendM5x3">The subtrahend M5X3.</param>
        /// <param name="subtrahendM5x4">The subtrahend M5X4.</param>
        /// <param name="subtrahendM5x5">The subtrahend M5X5.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5)
            Subtract5x5x5x5(
            double minuendM1x1, double minuendM1x2, double minuendM1x3, double minuendM1x4, double minuendM1x5,
            double minuendM2x1, double minuendM2x2, double minuendM2x3, double minuendM2x4, double minuendM2x5,
            double minuendM3x1, double minuendM3x2, double minuendM3x3, double minuendM3x4, double minuendM3x5,
            double minuendM4x1, double minuendM4x2, double minuendM4x3, double minuendM4x4, double minuendM4x5,
            double minuendM5x1, double minuendM5x2, double minuendM5x3, double minuendM5x4, double minuendM5x5,
            double subtrahendM1x1, double subtrahendM1x2, double subtrahendM1x3, double subtrahendM1x4, double subtrahendM1x5,
            double subtrahendM2x1, double subtrahendM2x2, double subtrahendM2x3, double subtrahendM2x4, double subtrahendM2x5,
            double subtrahendM3x1, double subtrahendM3x2, double subtrahendM3x3, double subtrahendM3x4, double subtrahendM3x5,
            double subtrahendM4x1, double subtrahendM4x2, double subtrahendM4x3, double subtrahendM4x4, double subtrahendM4x5,
            double subtrahendM5x1, double subtrahendM5x2, double subtrahendM5x3, double subtrahendM5x4, double subtrahendM5x5)
            => (minuendM1x1 - subtrahendM1x1, minuendM1x2 - subtrahendM1x2, minuendM1x3 - subtrahendM1x3, minuendM1x4 - subtrahendM1x4, minuendM1x5 - subtrahendM1x5,
                minuendM2x1 - subtrahendM2x1, minuendM2x2 - subtrahendM2x2, minuendM2x3 - subtrahendM2x3, minuendM2x4 - subtrahendM2x4, minuendM2x5 - subtrahendM2x5,
                minuendM3x1 - subtrahendM3x1, minuendM3x2 - subtrahendM3x2, minuendM3x3 - subtrahendM3x3, minuendM3x4 - subtrahendM3x4, minuendM3x5 - subtrahendM3x5,
                minuendM4x1 - subtrahendM4x1, minuendM4x2 - subtrahendM4x2, minuendM4x3 - subtrahendM4x3, minuendM4x4 - subtrahendM4x4, minuendM4x5 - subtrahendM4x5,
                minuendM5x1 - subtrahendM5x1, minuendM5x2 - subtrahendM5x2, minuendM5x3 - subtrahendM5x3, minuendM5x4 - subtrahendM5x4, minuendM5x5 - subtrahendM5x5);

        /// <summary>
        /// Used to subtract two matrices.
        /// </summary>
        /// <param name="minuendM1x1">The minuend M1X1.</param>
        /// <param name="minuendM1x2">The minuend M1X2.</param>
        /// <param name="minuendM1x3">The minuend M1X3.</param>
        /// <param name="minuendM1x4">The minuend M1X4.</param>
        /// <param name="minuendM1x5">The minuend M1X5.</param>
        /// <param name="minuendM1x6">The minuend M1X6.</param>
        /// <param name="minuendM2x1">The minuend M2X1.</param>
        /// <param name="minuendM2x2">The minuend M2X2.</param>
        /// <param name="minuendM2x3">The minuend M2X3.</param>
        /// <param name="minuendM2x4">The minuend M2X4.</param>
        /// <param name="minuendM2x5">The minuend M2X5.</param>
        /// <param name="minuendM2x6">The minuend M2X6.</param>
        /// <param name="minuendM3x1">The minuend M3X1.</param>
        /// <param name="minuendM3x2">The minuend M3X2.</param>
        /// <param name="minuendM3x3">The minuend M3X3.</param>
        /// <param name="minuendM3x4">The minuend M3X4.</param>
        /// <param name="minuendM3x5">The minuend M3X5.</param>
        /// <param name="minuendM3x6">The minuend M3X6.</param>
        /// <param name="minuendM4x1">The minuend M4X1.</param>
        /// <param name="minuendM4x2">The minuend M4X2.</param>
        /// <param name="minuendM4x3">The minuend M4X3.</param>
        /// <param name="minuendM4x4">The minuend M4X4.</param>
        /// <param name="minuendM4x5">The minuend M4X5.</param>
        /// <param name="minuendM4x6">The minuend M4X6.</param>
        /// <param name="minuendM5x1">The minuend M5X1.</param>
        /// <param name="minuendM5x2">The minuend M5X2.</param>
        /// <param name="minuendM5x3">The minuend M5X3.</param>
        /// <param name="minuendM5x4">The minuend M5X4.</param>
        /// <param name="minuendM5x5">The minuend M5X5.</param>
        /// <param name="minuendM5x6">The minuend M5X6.</param>
        /// <param name="minuendM6x1">The minuend M6X1.</param>
        /// <param name="minuendM6x2">The minuend M6X2.</param>
        /// <param name="minuendM6x3">The minuend M6X3.</param>
        /// <param name="minuendM6x4">The minuend M6X4.</param>
        /// <param name="minuendM6x5">The minuend M6X5.</param>
        /// <param name="minuendM6x6">The minuend M6X6.</param>
        /// <param name="subtrahendM1x1">The subtrahend M1X1.</param>
        /// <param name="subtrahendM1x2">The subtrahend M1X2.</param>
        /// <param name="subtrahendM1x3">The subtrahend M1X3.</param>
        /// <param name="subtrahendM1x4">The subtrahend M1X4.</param>
        /// <param name="subtrahendM1x5">The subtrahend M1X5.</param>
        /// <param name="subtrahendM1x6">The subtrahend M1X6.</param>
        /// <param name="subtrahendM2x1">The subtrahend M2X1.</param>
        /// <param name="subtrahendM2x2">The subtrahend M2X2.</param>
        /// <param name="subtrahendM2x3">The subtrahend M2X3.</param>
        /// <param name="subtrahendM2x4">The subtrahend M2X4.</param>
        /// <param name="subtrahendM2x5">The subtrahend M2X5.</param>
        /// <param name="subtrahendM2x6">The subtrahend M2X6.</param>
        /// <param name="subtrahendM3x1">The subtrahend M3X1.</param>
        /// <param name="subtrahendM3x2">The subtrahend M3X2.</param>
        /// <param name="subtrahendM3x3">The subtrahend M3X3.</param>
        /// <param name="subtrahendM3x4">The subtrahend M3X4.</param>
        /// <param name="subtrahendM3x5">The subtrahend M3X5.</param>
        /// <param name="subtrahendM3x6">The subtrahend M3X6.</param>
        /// <param name="subtrahendM4x1">The subtrahend M4X1.</param>
        /// <param name="subtrahendM4x2">The subtrahend M4X2.</param>
        /// <param name="subtrahendM4x3">The subtrahend M4X3.</param>
        /// <param name="subtrahendM4x4">The subtrahend M4X4.</param>
        /// <param name="subtrahendM4x5">The subtrahend M4X5.</param>
        /// <param name="subtrahendM4x6">The subtrahend M4X6.</param>
        /// <param name="subtrahendM5x1">The subtrahend M5X1.</param>
        /// <param name="subtrahendM5x2">The subtrahend M5X2.</param>
        /// <param name="subtrahendM5x3">The subtrahend M5X3.</param>
        /// <param name="subtrahendM5x4">The subtrahend M5X4.</param>
        /// <param name="subtrahendM5x5">The subtrahend M5X5.</param>
        /// <param name="subtrahendM5x6">The subtrahend M5X6.</param>
        /// <param name="subtrahendM6x1">The subtrahend M6X1.</param>
        /// <param name="subtrahendM6x2">The subtrahend M6X2.</param>
        /// <param name="subtrahendM6x3">The subtrahend M6X3.</param>
        /// <param name="subtrahendM6x4">The subtrahend M6X4.</param>
        /// <param name="subtrahendM6x5">The subtrahend M6X5.</param>
        /// <param name="subtrahendM6x6">The subtrahend M6X6.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6)
            Subtract6x6x6x6(
            double minuendM1x1, double minuendM1x2, double minuendM1x3, double minuendM1x4, double minuendM1x5, double minuendM1x6,
            double minuendM2x1, double minuendM2x2, double minuendM2x3, double minuendM2x4, double minuendM2x5, double minuendM2x6,
            double minuendM3x1, double minuendM3x2, double minuendM3x3, double minuendM3x4, double minuendM3x5, double minuendM3x6,
            double minuendM4x1, double minuendM4x2, double minuendM4x3, double minuendM4x4, double minuendM4x5, double minuendM4x6,
            double minuendM5x1, double minuendM5x2, double minuendM5x3, double minuendM5x4, double minuendM5x5, double minuendM5x6,
            double minuendM6x1, double minuendM6x2, double minuendM6x3, double minuendM6x4, double minuendM6x5, double minuendM6x6,
            double subtrahendM1x1, double subtrahendM1x2, double subtrahendM1x3, double subtrahendM1x4, double subtrahendM1x5, double subtrahendM1x6,
            double subtrahendM2x1, double subtrahendM2x2, double subtrahendM2x3, double subtrahendM2x4, double subtrahendM2x5, double subtrahendM2x6,
            double subtrahendM3x1, double subtrahendM3x2, double subtrahendM3x3, double subtrahendM3x4, double subtrahendM3x5, double subtrahendM3x6,
            double subtrahendM4x1, double subtrahendM4x2, double subtrahendM4x3, double subtrahendM4x4, double subtrahendM4x5, double subtrahendM4x6,
            double subtrahendM5x1, double subtrahendM5x2, double subtrahendM5x3, double subtrahendM5x4, double subtrahendM5x5, double subtrahendM5x6,
            double subtrahendM6x1, double subtrahendM6x2, double subtrahendM6x3, double subtrahendM6x4, double subtrahendM6x5, double subtrahendM6x6)
            => (minuendM1x1 - subtrahendM1x1, minuendM1x2 - subtrahendM1x2, minuendM1x3 - subtrahendM1x3, minuendM1x4 - subtrahendM1x4, minuendM1x5 - subtrahendM1x5, minuendM1x6 - subtrahendM1x6,
                minuendM2x1 - subtrahendM2x1, minuendM2x2 - subtrahendM2x2, minuendM2x3 - subtrahendM2x3, minuendM2x4 - subtrahendM2x4, minuendM2x5 - subtrahendM2x5, minuendM2x6 - subtrahendM2x6,
                minuendM3x1 - subtrahendM3x1, minuendM3x2 - subtrahendM3x2, minuendM3x3 - subtrahendM3x3, minuendM3x4 - subtrahendM3x4, minuendM3x5 - subtrahendM3x5, minuendM3x6 - subtrahendM3x6,
                minuendM4x1 - subtrahendM4x1, minuendM4x2 - subtrahendM4x2, minuendM4x3 - subtrahendM4x3, minuendM4x4 - subtrahendM4x4, minuendM4x5 - subtrahendM4x5, minuendM4x6 - subtrahendM4x6,
                minuendM5x1 - subtrahendM5x1, minuendM5x2 - subtrahendM5x2, minuendM5x3 - subtrahendM5x3, minuendM5x4 - subtrahendM5x4, minuendM5x5 - subtrahendM5x5, minuendM5x6 - subtrahendM5x6,
                minuendM6x1 - subtrahendM6x1, minuendM6x2 - subtrahendM6x2, minuendM6x3 - subtrahendM6x3, minuendM6x4 - subtrahendM6x4, minuendM6x5 - subtrahendM6x5, minuendM6x6 - subtrahendM6x6);
        #endregion

        #region Scale
        /// <summary>
        /// Multiplies the specified multiplicand by a scalar.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Columns of multiplicand must be the same length as the rows of multiplier.</exception>
        /// <acknowledgment>
        /// https://www.geeksforgeeks.org/different-operation-matrices/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[,] Scale(double[,] multiplicand, double multiplier)
        {
            var multiplicandRows = multiplicand.GetLength(0);
            var multiplicandCols = multiplicand.GetLength(1);

            var result = new double[multiplicandRows, multiplicandCols];
            for (var i = 0; i < multiplicandRows; i++)
            {
                for (var j = 0; j < multiplicandCols; j++)
                {
                    result[i, j] = multiplicand[i, j] * multiplier;
                }
            }

            return result;
        }

        /// <summary>
        /// Multiplies the specified multiplier by a scalar.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Columns of multiplicand must be the same length as the rows of multiplier.</exception>
        /// <acknowledgment>
        /// https://www.geeksforgeeks.org/different-operation-matrices/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[,] Scale(double multiplicand, double[,] multiplier)
        {
            var multiplicandRows = multiplier.GetLength(0);
            var multiplicandCols = multiplier.GetLength(1);

            var result = new double[multiplicandRows, multiplicandCols];
            for (var i = 0; i < multiplicandRows; i++)
            {
                for (var j = 0; j < multiplicandCols; j++)
                {
                    result[i, j] = multiplicand * multiplier[i, j];
                }
            }

            return result;
        }

        /// <summary>
        /// Used to multiply a Matrix2x2 object by a multiplier value.
        /// </summary>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2,
            double m2x1, double m2x2)
            Scale2x2(
            double m1x1, double m1x2,
            double m2x1, double m2x2,
            double multiplier)
            => (
                m1x1 * multiplier, m1x2 * multiplier,
                m2x1 * multiplier, m2x2 * multiplier
            );

        /// <summary>
        /// Used to multiply a Matrix3x3 object by a multiplier value.
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
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3)
            Scale3x3(
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3,
            double multiplier)
            => (
                m1x1 * multiplier, m1x2 * multiplier, m1x3 * multiplier,
                m2x1 * multiplier, m2x2 * multiplier, m2x3 * multiplier,
                m3x1 * multiplier, m3x2 * multiplier, m3x3 * multiplier
            );

        /// <summary>
        /// Used to multiply (concatenate) two <see cref="Matrix4x4D" />s.
        /// </summary>
        /// <param name="multiplicandM1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicandM1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicandM1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicandM1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicandM2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicandM2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicandM2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicandM2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicandM3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicandM3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicandM3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicandM3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicandM4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicandM4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicandM4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicandM4x4">The multiplicand M4X4.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4
            ) Scale4x4(
            double multiplicandM1x1, double multiplicandM1x2, double multiplicandM1x3, double multiplicandM1x4,
            double multiplicandM2x1, double multiplicandM2x2, double multiplicandM2x3, double multiplicandM2x4,
            double multiplicandM3x1, double multiplicandM3x2, double multiplicandM3x3, double multiplicandM3x4,
            double multiplicandM4x1, double multiplicandM4x2, double multiplicandM4x3, double multiplicandM4x4,
            double multiplier)
            => (
                multiplicandM1x1 * multiplier, multiplicandM1x2 * multiplier, multiplicandM1x3 * multiplier, multiplicandM1x4 * multiplier,
                multiplicandM2x1 * multiplier, multiplicandM2x2 * multiplier, multiplicandM2x3 * multiplier, multiplicandM2x4 * multiplier,
                multiplicandM3x1 * multiplier, multiplicandM3x2 * multiplier, multiplicandM3x3 * multiplier, multiplicandM3x4 * multiplier,
                multiplicandM4x1 * multiplier, multiplicandM4x2 * multiplier, multiplicandM4x3 * multiplier, multiplicandM4x4 * multiplier
            );

        /// <summary>
        /// Used to multiply (concatenate) two Matrix5x5Ds.
        /// </summary>
        /// <param name="multiplicandM1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicandM1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicandM1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicandM1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicandM1x5">The multiplicand M1X5.</param>
        /// <param name="multiplicandM2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicandM2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicandM2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicandM2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicandM2x5">The multiplicand M2X5.</param>
        /// <param name="multiplicandM3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicandM3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicandM3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicandM3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicandM3x5">The multiplicand M3X5.</param>
        /// <param name="multiplicandM4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicandM4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicandM4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicandM4x4">The multiplicand M4X4.</param>
        /// <param name="multiplicandM4x5">The multiplicand M4X5.</param>
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
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5
            ) Scale5x5(
            double multiplicandM1x1, double multiplicandM1x2, double multiplicandM1x3, double multiplicandM1x4, double multiplicandM1x5,
            double multiplicandM2x1, double multiplicandM2x2, double multiplicandM2x3, double multiplicandM2x4, double multiplicandM2x5,
            double multiplicandM3x1, double multiplicandM3x2, double multiplicandM3x3, double multiplicandM3x4, double multiplicandM3x5,
            double multiplicandM4x1, double multiplicandM4x2, double multiplicandM4x3, double multiplicandM4x4, double multiplicandM4x5,
            double multiplicandM5x1, double multiplicandM5x2, double multiplicandM5x3, double multiplicandM5x4, double multiplicandM5x5,
            double multiplier)
            => (
                multiplicandM1x1 * multiplier, multiplicandM1x2 * multiplier, multiplicandM1x3 * multiplier, multiplicandM1x4 * multiplier, multiplicandM1x5 * multiplier,
                multiplicandM2x1 * multiplier, multiplicandM2x2 * multiplier, multiplicandM2x3 * multiplier, multiplicandM2x4 * multiplier, multiplicandM2x5 * multiplier,
                multiplicandM3x1 * multiplier, multiplicandM3x2 * multiplier, multiplicandM3x3 * multiplier, multiplicandM3x4 * multiplier, multiplicandM3x5 * multiplier,
                multiplicandM4x1 * multiplier, multiplicandM4x2 * multiplier, multiplicandM4x3 * multiplier, multiplicandM4x4 * multiplier, multiplicandM4x5 * multiplier,
                multiplicandM5x1 * multiplier, multiplicandM5x2 * multiplier, multiplicandM5x3 * multiplier, multiplicandM5x4 * multiplier, multiplicandM5x5 * multiplier
            );

        /// <summary>
        /// Used to multiply (concatenate) two Matrix6x6Ds.
        /// </summary>
        /// <param name="multiplicandM1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicandM1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicandM1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicandM1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicandM1x5">The multiplicand M1X5.</param>
        /// <param name="multiplicandM1x6">The multiplicand M1X6.</param>
        /// <param name="multiplicandM2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicandM2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicandM2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicandM2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicandM2x5">The multiplicand M2X5.</param>
        /// <param name="multiplicandM2x6">The multiplicand M2X6.</param>
        /// <param name="multiplicandM3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicandM3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicandM3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicandM3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicandM3x5">The multiplicand M3X5.</param>
        /// <param name="multiplicandM3x6">The multiplicand M3X6.</param>
        /// <param name="multiplicandM4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicandM4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicandM4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicandM4x4">The multiplicand M4X4.</param>
        /// <param name="multiplicandM4x5">The multiplicand M4X5.</param>
        /// <param name="multiplicandM4x6">The multiplicand M4X6.</param>
        /// <param name="multiplicandM5x1">The multiplicand M5X1.</param>
        /// <param name="multiplicandM5x2">The multiplicand M5X2.</param>
        /// <param name="multiplicandM5x3">The multiplicand M5X3.</param>
        /// <param name="multiplicandM5x4">The multiplicand M5X4.</param>
        /// <param name="multiplicandM5x5">The multiplicand M5X5.</param>
        /// <param name="multiplicandM5x6">The multiplicand M5X6.</param>
        /// <param name="multiplicandM6x1">The multiplicand M6X1.</param>
        /// <param name="multiplicandM6x2">The multiplicand M6X2.</param>
        /// <param name="multiplicandM6x3">The multiplicand M6X3.</param>
        /// <param name="multiplicandM6x4">The multiplicand M6X4.</param>
        /// <param name="multiplicandM6x5">The multiplicand M6X5.</param>
        /// <param name="multiplicandM6x6">The multiplicand M6X6.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6
            ) Scale6x6(
            double multiplicandM1x1, double multiplicandM1x2, double multiplicandM1x3, double multiplicandM1x4, double multiplicandM1x5, double multiplicandM1x6,
            double multiplicandM2x1, double multiplicandM2x2, double multiplicandM2x3, double multiplicandM2x4, double multiplicandM2x5, double multiplicandM2x6,
            double multiplicandM3x1, double multiplicandM3x2, double multiplicandM3x3, double multiplicandM3x4, double multiplicandM3x5, double multiplicandM3x6,
            double multiplicandM4x1, double multiplicandM4x2, double multiplicandM4x3, double multiplicandM4x4, double multiplicandM4x5, double multiplicandM4x6,
            double multiplicandM5x1, double multiplicandM5x2, double multiplicandM5x3, double multiplicandM5x4, double multiplicandM5x5, double multiplicandM5x6,
            double multiplicandM6x1, double multiplicandM6x2, double multiplicandM6x3, double multiplicandM6x4, double multiplicandM6x5, double multiplicandM6x6,
            double multiplier)
            => (
                multiplicandM1x1 * multiplier, multiplicandM1x2 * multiplier, multiplicandM1x3 * multiplier, multiplicandM1x4 * multiplier, multiplicandM1x5 * multiplier, multiplicandM1x6 * multiplier,
                multiplicandM2x1 * multiplier, multiplicandM2x2 * multiplier, multiplicandM2x3 * multiplier, multiplicandM2x4 * multiplier, multiplicandM2x5 * multiplier, multiplicandM2x6 * multiplier,
                multiplicandM3x1 * multiplier, multiplicandM3x2 * multiplier, multiplicandM3x3 * multiplier, multiplicandM3x4 * multiplier, multiplicandM3x5 * multiplier, multiplicandM3x6 * multiplier,
                multiplicandM4x1 * multiplier, multiplicandM4x2 * multiplier, multiplicandM4x3 * multiplier, multiplicandM4x4 * multiplier, multiplicandM4x5 * multiplier, multiplicandM4x6 * multiplier,
                multiplicandM5x1 * multiplier, multiplicandM5x2 * multiplier, multiplicandM5x3 * multiplier, multiplicandM5x4 * multiplier, multiplicandM5x5 * multiplier, multiplicandM5x6 * multiplier,
                multiplicandM6x1 * multiplier, multiplicandM6x2 * multiplier, multiplicandM6x3 * multiplier, multiplicandM6x4 * multiplier, multiplicandM6x5 * multiplier, multiplicandM6x6 * multiplier
            );
        #endregion Scale

        #region Multiply Matrix by Vertical Vector
        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Columns of multiplicand must be the same length as the rows of multiplier.</exception>
        /// <acknowledgment>
        /// https://www.geeksforgeeks.org/different-operation-matrices/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[] Multiply(double[,] multiplicand, double[] multiplier)
        {
            var multiplicandRows = multiplicand.GetLength(0);
            var multiplicandCols = multiplicand.GetLength(1);
            var multiplierRows = multiplier.Length;

            if (multiplicandCols != multiplierRows) throw new Exception("Columns of multiplicand must be the same length as the rows of multiplier.");

            var multiplierCols = multiplier.GetLength(1);

            var result = new double[multiplicandRows];
            for (var i = 0; i < multiplicandRows; i++)
            {
                for (var j = 0; j < multiplierCols; j++)
                {
                    result[i] = 0;
                    for (var k = 0; k < multiplicandCols; k++)
                    {
                        result[i] += multiplicand[i, k] * multiplier[j];
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Multiplies a matrix2x2 by a Vertical vector2.
        /// </summary>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://matrixmultiplication.xyz/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) MultiplyMatrix2x2ByVerticalVector2D(
            double m1x1, double m1x2,
            double m2x1, double m2x2,
            double x, double y)
            => (
                (x * m1x1) + (y * m1x2),
                (x * m2x1) + (y * m2x2)
            );

        /// <summary>
        /// Multiplies the matrix3x3 by vertical vector3 d.
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
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://matrixmultiplication.xyz/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) MultiplyMatrix3x3ByVerticalVector3D(
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3,
            double x, double y, double z)
            => (
                (x * m1x1) + (y * m1x2) + (z * m1x3),
                (x * m2x1) + (y * m2x2) + (z * m2x3),
                (x * m3x1) + (y * m3x2) + (z * m3x3)
            );

        /// <summary>
        /// Multiplies the matrix4x4 by vertical vector4 d.
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
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="w">The w.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W) MultiplyMatrix4x4ByVerticalVector4D(
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4,
            double x, double y, double z, double w)
            => (
                (x * m1x1) + (y * m1x2) + (z * m1x3) + (w * m1x4),
                (x * m2x1) + (y * m2x2) + (z * m2x3) + (w * m2x4),
                (x * m3x1) + (y * m3x2) + (z * m3x3) + (w * m3x4),
                (x * m4x1) + (y * m4x2) + (z * m4x3) + (w * m4x4)
            );

        /// <summary>
        /// Multiplies the matrix5x5 by vertical vector5 d.
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
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="w">The w.</param>
        /// <param name="v">The v.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://matrixmultiplication.xyz/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W, double V) MultiplyMatrix5x5ByVerticalVector5D(
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5,
            double x, double y, double z, double w, double v)
            => (
                (x * m1x1) + (y * m1x2) + (z * m1x3) + (w * m1x4) + (v * m1x5),
                (x * m2x1) + (y * m2x2) + (z * m2x3) + (w * m2x4) + (v * m2x5),
                (x * m3x1) + (y * m3x2) + (z * m3x3) + (w * m3x4) + (v * m3x5),
                (x * m4x1) + (y * m4x2) + (z * m4x3) + (w * m4x4) + (v * m4x5),
                (x * m5x1) + (y * m5x2) + (z * m5x3) + (w * m5x4) + (v * m5x5)
            );

        /// <summary>
        /// Multiplies the matrix6x6 by vertical vector6 d.
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
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="w">The w.</param>
        /// <param name="v">The v.</param>
        /// <param name="u">The u.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://matrixmultiplication.xyz/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W, double V, double U) MultiplyMatrix6x6ByVerticalVector6D(
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6,
            double x, double y, double z, double w, double v, double u)
            => (
                (x * m1x1) + (y * m1x2) + (z * m1x3) + (w * m1x4) + (v * m1x5) + (u * m1x6),
                (x * m2x1) + (y * m2x2) + (z * m2x3) + (w * m2x4) + (v * m2x5) + (u * m2x6),
                (x * m3x1) + (y * m3x2) + (z * m3x3) + (w * m3x4) + (v * m3x5) + (u * m3x6),
                (x * m4x1) + (y * m4x2) + (z * m4x3) + (w * m4x4) + (v * m4x5) + (u * m4x6),
                (x * m5x1) + (y * m5x2) + (z * m5x3) + (w * m5x4) + (v * m5x5) + (u * m5x6),
                (x * m6x1) + (y * m6x2) + (z * m6x3) + (w * m6x4) + (v * m6x5) + (u * m6x6)
            );
        #endregion

        #region Multiply Horizontal Vector by Matrix
        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Columns of multiplicand must be the same length as the rows of multiplier.</exception>
        /// <acknowledgment>
        /// https://www.geeksforgeeks.org/different-operation-matrices/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[] Multiply(double[] multiplicand, double[,] multiplier)
        {
            var multiplicandRows = 1;
            var multiplicandCols = multiplicand.Length;
            var multiplierRows = multiplier.GetLength(0);

            if (multiplicandCols != multiplierRows) throw new Exception("Columns of multiplicand must be the same length as the rows of multiplier.");

            var multiplierCols = multiplier.GetLength(1);

            var result = new double[multiplicandCols];
            for (var i = 0; i < multiplicandRows; i++)
            {
                for (var j = 0; j < multiplierCols; j++)
                {
                    result[j] = 0;
                    for (var k = 0; k < multiplicandCols; k++)
                    {
                        result[j] += multiplicand[i] * multiplier[k, j];
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Multiplies the horizontal vector2 d by matrix2x2.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="m1x1">The M1X1.</param>
        /// <param name="m1x2">The M1X2.</param>
        /// <param name="m2x1">The M2X1.</param>
        /// <param name="m2x2">The M2X2.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://matrixmultiplication.xyz/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) MultiplyHorizontalVector2DByMatrix2x2(
            double x, double y,
            double m1x1, double m1x2,
            double m2x1, double m2x2)
            => (
                (x * m1x1) + (y * m2x1),
                (x * m1x2) + (y * m2x2)
            );

        /// <summary>
        /// Multiplies the horizontal vector3 d by matrix3x3.
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
        /// <acknowledgment>
        /// http://matrixmultiplication.xyz/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) MultiplyHorizontalVector3DByMatrix3x3(
            double x, double y, double z,
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3)
            => (
                (x * m1x1) + (y * m2x1) + (z * m3x1),
                (x * m1x2) + (y * m2x2) + (z * m3x2),
                (x * m1x3) + (y * m2x3) + (z * m3x3)
            );

        /// <summary>
        /// Multiplies the horizontal vector4 d by matrix4x4.
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
        /// <acknowledgment>
        /// http://matrixmultiplication.xyz/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W) MultiplyHorizontalVector4DByMatrix4x4(
            double x, double y, double z, double w,
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4)
            => (
                (x * m1x1) + (x * m2x1) + (x * m3x1) + (x * m4x1),
                (y * m1x2) + (y * m2x2) + (y * m3x2) + (y * m4x2),
                (z * m1x3) + (z * m2x3) + (z * m3x3) + (z * m4x3),
                (w * m1x4) + (w * m2x4) + (w * m3x4) + (w * m4x4)
            );

        /// <summary>
        /// Multiplies the horizontal vector5 d by matrix5x5.
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
        /// <acknowledgment>
        /// http://matrixmultiplication.xyz/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W, double V) MultiplyHorizontalVector5DByMatrix5x5(
            double x, double y, double z, double w, double v,
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5)
            => (
                (x * m1x1) + (y * m2x1) + (z * m3x1) + (w * m4x1) + (v * m5x1),
                (x * m1x2) + (y * m2x2) + (z * m3x2) + (w * m4x2) + (v * m5x2),
                (x * m1x3) + (y * m2x3) + (z * m3x3) + (w * m4x3) + (v * m5x3),
                (x * m1x4) + (y * m2x4) + (z * m3x4) + (w * m4x4) + (v * m5x4),
                (x * m1x5) + (y * m2x5) + (z * m3x5) + (w * m4x5) + (v * m5x5)
            );

        /// <summary>
        /// Multiplies the horizontal vector6 d by matrix6x6.
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
        /// <acknowledgment>
        /// http://matrixmultiplication.xyz/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W, double V, double U) MultiplyHorizontalVector6DByMatrix6x6(
            double x, double y, double z, double w, double v, double u,
            double m1x1, double m1x2, double m1x3, double m1x4, double m1x5, double m1x6,
            double m2x1, double m2x2, double m2x3, double m2x4, double m2x5, double m2x6,
            double m3x1, double m3x2, double m3x3, double m3x4, double m3x5, double m3x6,
            double m4x1, double m4x2, double m4x3, double m4x4, double m4x5, double m4x6,
            double m5x1, double m5x2, double m5x3, double m5x4, double m5x5, double m5x6,
            double m6x1, double m6x2, double m6x3, double m6x4, double m6x5, double m6x6)
            => (
                (x * m1x1) + (y * m2x1) + (z * m3x1) + (w * m4x1) + (v * m5x1) + (u * m6x1),
                (x * m1x2) + (y * m2x2) + (z * m3x2) + (w * m4x2) + (v * m5x2) + (u * m6x2),
                (x * m1x3) + (y * m2x3) + (z * m3x3) + (w * m4x3) + (v * m5x3) + (u * m6x3),
                (x * m1x4) + (y * m2x4) + (z * m3x4) + (w * m4x4) + (v * m5x4) + (u * m6x4),
                (x * m1x5) + (y * m2x5) + (z * m3x5) + (w * m4x5) + (v * m5x5) + (u * m6x5),
                (x * m1x6) + (y * m2x6) + (z * m3x6) + (w * m4x6) + (v * m5x6) + (u * m6x6)
            );
        #endregion

        #region Multiply Matrix by Matrix
        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Columns of multiplicand must be the same length as the rows of multiplier.</exception>
        /// <acknowledgment>
        /// https://www.geeksforgeeks.org/different-operation-matrices/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[,] Multiply(double[,] multiplicand, double[,] multiplier)
        {
            var multiplicandRows = multiplicand.GetLength(0);
            var multiplicandCols = multiplicand.GetLength(1);
            var multiplierRows = multiplier.GetLength(0);

            if (multiplicandCols != multiplierRows) throw new Exception("Columns of multiplicand must be the same length as the rows of multiplier.");

            var multiplierCols = multiplier.GetLength(1);

            var result = new double[multiplicandRows, multiplierCols];
            for (var i = 0; i < multiplicandRows; i++)
            {
                for (var j = 0; j < multiplierCols; j++)
                {
                    result[i, j] = 0;
                    for (var k = 0; k < multiplicandCols; k++)
                    {
                        result[i, j] += multiplicand[i, k] * multiplier[k, j];
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Multiply2x2x2x2s the specified multiplicand1x1.
        /// </summary>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2,
            double m2x1, double m2x2
            ) Multiply2x2x2x2(
            double multiplicand1x1, double multiplicand1x2,
            double multiplicand2x1, double multiplicand2x2,
            double multiplier1x1, double multiplier1x2,
            double multiplier2x1, double multiplier2x2)
            => (
                (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1), (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2),
                (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1), (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2)
            );

        /// <summary>
        /// Multiply3x3x2x2s the specified multiplicand1x1.
        /// </summary>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3
            ) Multiply3x3x2x2(
            double multiplicand1x1, double multiplicand1x2, double multiplicand1x3,
            double multiplicand2x1, double multiplicand2x2, double multiplicand2x3,
            double multiplicand3x1, double multiplicand3x2, double multiplicand3x3,
            double multiplier1x1, double multiplier1x2,
            double multiplier2x1, double multiplier2x2)
            => (
                (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1), (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2), multiplicand1x3,
                (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1), (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2), multiplicand2x3,
                (multiplicand3x1 * multiplier1x1) + (multiplicand3x2 * multiplier2x1), (multiplicand3x1 * multiplier1x2) + (multiplicand3x2 * multiplier2x2), multiplicand3x3
            );

        /// <summary>
        /// Multiply2x2x3x3s the specified multiplicand1x1.
        /// </summary>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3)
            Multiply2x2x3x3(
            double multiplicand1x1, double multiplicand1x2,
            double multiplicand2x1, double multiplicand2x2,
            double multiplier1x1, double multiplier1x2, double multiplier1x3,
            double multiplier2x1, double multiplier2x2, double multiplier2x3,
            double multiplier3x1, double multiplier3x2, double multiplier3x3)
            => (
                (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1), (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2), (multiplicand1x1 * multiplier1x3) + (multiplicand1x2 * multiplier2x3),
                (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1), (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2), (multiplicand2x1 * multiplier1x3) + (multiplicand2x2 * multiplier2x3),
                multiplier3x1, multiplier3x2, multiplier3x3
            );

        /// <summary>
        /// Multiply3x3x3x3s the specified multiplicand1x1.
        /// </summary>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3,
            double m2x1, double m2x2, double m2x3,
            double m3x1, double m3x2, double m3x3)
            Multiply3x3x3x3(
            double multiplicand1x1, double multiplicand1x2, double multiplicand1x3,
            double multiplicand2x1, double multiplicand2x2, double multiplicand2x3,
            double multiplicand3x1, double multiplicand3x2, double multiplicand3x3,
            double multiplier1x1, double multiplier1x2, double multiplier1x3,
            double multiplier2x1, double multiplier2x2, double multiplier2x3,
            double multiplier3x1, double multiplier3x2, double multiplier3x3)
            => (
                (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1) + (multiplicand1x3 * multiplier3x1), (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2) + (multiplicand1x3 * multiplier3x2), (multiplicand1x1 * multiplier1x3) + (multiplicand1x2 * multiplier2x3) + (multiplicand1x3 * multiplier3x3),
                (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1) + (multiplicand2x3 * multiplier3x1), (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2) + (multiplicand2x3 * multiplier3x2), (multiplicand2x1 * multiplier1x3) + (multiplicand2x2 * multiplier2x3) + (multiplicand2x3 * multiplier3x3),
                (multiplicand3x1 * multiplier1x1) + (multiplicand3x2 * multiplier2x1) + (multiplicand3x3 * multiplier3x1), (multiplicand3x1 * multiplier1x2) + (multiplicand3x2 * multiplier2x2) + (multiplicand3x3 * multiplier3x2), (multiplicand3x1 * multiplier1x3) + (multiplicand3x2 * multiplier2x3) + (multiplicand3x3 * multiplier3x3)
            );

        /// <summary>
        /// Multiply2x2x4x4s the specified multiplicand1x1.
        /// </summary>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier1x4">The multiplier M1X4.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier2x4">The multiplier M2X4.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <param name="multiplier3x4">The multiplier M3X4.</param>
        /// <param name="multiplier4x1">The multiplier M4X1.</param>
        /// <param name="multiplier4x2">The multiplier M4X2.</param>
        /// <param name="multiplier4x3">The multiplier M4X3.</param>
        /// <param name="multiplier4x4">The multiplier M4X4.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4)
            Multiply2x2x4x4(
            double multiplicand1x1, double multiplicand1x2,
            double multiplicand2x1, double multiplicand2x2,
            double multiplier1x1, double multiplier1x2, double multiplier1x3, double multiplier1x4,
            double multiplier2x1, double multiplier2x2, double multiplier2x3, double multiplier2x4,
            double multiplier3x1, double multiplier3x2, double multiplier3x3, double multiplier3x4,
            double multiplier4x1, double multiplier4x2, double multiplier4x3, double multiplier4x4)
            => (
                (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1), (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2), (multiplicand1x1 * multiplier1x3) + (multiplicand1x2 * multiplier2x3), (multiplicand1x1 * multiplier1x4) + (multiplicand1x2 * multiplier2x4),
                (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1), (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2), (multiplicand2x1 * multiplier1x3) + (multiplicand2x2 * multiplier2x3), (multiplicand2x1 * multiplier1x4) + (multiplicand2x2 * multiplier2x4),
                multiplier3x1, multiplier3x2, multiplier3x3, multiplier3x4,
                multiplier4x1, multiplier4x2, multiplier4x3, multiplier4x4
            );

        /// <summary>
        /// Multiply3x3x4x4s the specified multiplicand1x1.
        /// </summary>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier1x4">The multiplier M1X4.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier2x4">The multiplier M2X4.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <param name="multiplier3x4">The multiplier M3X4.</param>
        /// <param name="multiplier4x1">The multiplier M4X1.</param>
        /// <param name="multiplier4x2">The multiplier M4X2.</param>
        /// <param name="multiplier4x3">The multiplier M4X3.</param>
        /// <param name="multiplier4x4">The multiplier M4X4.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4)
            Multiply3x3x4x4(
            double multiplicand1x1, double multiplicand1x2, double multiplicand1x3,
            double multiplicand2x1, double multiplicand2x2, double multiplicand2x3,
            double multiplicand3x1, double multiplicand3x2, double multiplicand3x3,
            double multiplier1x1, double multiplier1x2, double multiplier1x3, double multiplier1x4,
            double multiplier2x1, double multiplier2x2, double multiplier2x3, double multiplier2x4,
            double multiplier3x1, double multiplier3x2, double multiplier3x3, double multiplier3x4,
            double multiplier4x1, double multiplier4x2, double multiplier4x3, double multiplier4x4)
            => (
                (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1) + (multiplicand1x3 * multiplier3x1), (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2) + (multiplicand1x3 * multiplier3x2), (multiplicand1x1 * multiplier1x3) + (multiplicand1x2 * multiplier2x3) + (multiplicand1x3 * multiplier3x3), (multiplicand1x1 * multiplier1x4) + (multiplicand1x2 * multiplier2x4) + (multiplicand1x3 * multiplier3x4),
                (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1) + (multiplicand2x3 * multiplier3x1), (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2) + (multiplicand2x3 * multiplier3x2), (multiplicand2x1 * multiplier1x3) + (multiplicand2x2 * multiplier2x3) + (multiplicand2x3 * multiplier3x3), (multiplicand2x1 * multiplier1x4) + (multiplicand2x2 * multiplier2x4) + (multiplicand2x3 * multiplier3x4),
                (multiplicand3x1 * multiplier1x1) + (multiplicand3x2 * multiplier2x1) + (multiplicand3x3 * multiplier3x1), (multiplicand3x1 * multiplier1x2) + (multiplicand3x2 * multiplier2x2) + (multiplicand3x3 * multiplier3x2), (multiplicand3x1 * multiplier1x3) + (multiplicand3x2 * multiplier2x3) + (multiplicand3x3 * multiplier3x3), (multiplicand3x1 * multiplier1x4) + (multiplicand3x2 * multiplier2x4) + (multiplicand3x3 * multiplier3x4),
                multiplier4x1, multiplier4x2, multiplier4x3, multiplier4x4
            );

        /// <summary>
        /// Multiply4x4x2x2s the specified multiplicand1x1.
        /// </summary>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicand3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicand4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicand4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicand4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicand4x4">The multiplicand M4X4.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4)
            Multiply4x4x2x2(
            double multiplicand1x1, double multiplicand1x2, double multiplicand1x3, double multiplicand1x4,
            double multiplicand2x1, double multiplicand2x2, double multiplicand2x3, double multiplicand2x4,
            double multiplicand3x1, double multiplicand3x2, double multiplicand3x3, double multiplicand3x4,
            double multiplicand4x1, double multiplicand4x2, double multiplicand4x3, double multiplicand4x4,
            double multiplier1x1, double multiplier1x2,
            double multiplier2x1, double multiplier2x2)
            => (
                (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1), (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2), multiplicand1x3, multiplicand1x4,
                (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1), (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2), multiplicand2x3, multiplicand2x4,
                (multiplicand3x1 * multiplier1x1) + (multiplicand3x2 * multiplier2x1), (multiplicand3x1 * multiplier1x2) + (multiplicand3x2 * multiplier2x2), multiplicand3x3, multiplicand3x4,
                (multiplicand4x1 * multiplier1x1) + (multiplicand4x2 * multiplier2x1), (multiplicand4x1 * multiplier1x2) + (multiplicand4x2 * multiplier2x2), multiplicand4x3, multiplicand4x4
            );

        /// <summary>
        /// Multiply4x4x3x3s the specified multiplicand1x1.
        /// </summary>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicand3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicand4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicand4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicand4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicand4x4">The multiplicand M4X4.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4)
            Multiply4x4x3x3(
            double multiplicand1x1, double multiplicand1x2, double multiplicand1x3, double multiplicand1x4,
            double multiplicand2x1, double multiplicand2x2, double multiplicand2x3, double multiplicand2x4,
            double multiplicand3x1, double multiplicand3x2, double multiplicand3x3, double multiplicand3x4,
            double multiplicand4x1, double multiplicand4x2, double multiplicand4x3, double multiplicand4x4,
            double multiplier1x1, double multiplier1x2, double multiplier1x3,
            double multiplier2x1, double multiplier2x2, double multiplier2x3,
            double multiplier3x1, double multiplier3x2, double multiplier3x3)
            => (
                (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1) + (multiplicand1x3 * multiplier3x1), (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2) + (multiplicand1x3 * multiplier3x2), (multiplicand1x1 * multiplier1x3) + (multiplicand1x2 * multiplier2x3) + (multiplicand1x3 * multiplier3x3), multiplicand1x4,
                (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1) + (multiplicand2x3 * multiplier3x1), (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2) + (multiplicand2x3 * multiplier3x2), (multiplicand2x1 * multiplier1x3) + (multiplicand2x2 * multiplier2x3) + (multiplicand2x3 * multiplier3x3), multiplicand2x4,
                (multiplicand3x1 * multiplier1x1) + (multiplicand3x2 * multiplier2x1) + (multiplicand3x3 * multiplier3x1), (multiplicand3x1 * multiplier1x2) + (multiplicand3x2 * multiplier2x2) + (multiplicand3x3 * multiplier3x2), (multiplicand3x1 * multiplier1x3) + (multiplicand3x2 * multiplier2x3) + (multiplicand3x3 * multiplier3x3), multiplicand3x4,
                (multiplicand4x1 * multiplier1x1) + (multiplicand4x2 * multiplier2x1) + (multiplicand4x3 * multiplier3x1), (multiplicand4x1 * multiplier1x2) + (multiplicand4x2 * multiplier2x2) + (multiplicand4x3 * multiplier3x2), (multiplicand4x1 * multiplier1x3) + (multiplicand4x2 * multiplier2x3) + (multiplicand4x3 * multiplier3x3), multiplicand4x4
            );

        /// <summary>
        /// Used to multiply (concatenate) two <see cref="Matrix4x4D" />s.
        /// </summary>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicand3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicand4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicand4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicand4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicand4x4">The multiplicand M4X4.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier1x4">The multiplier M1X4.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier2x4">The multiplier M2X4.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <param name="multiplier3x4">The multiplier M3X4.</param>
        /// <param name="multiplier4x1">The multiplier M4X1.</param>
        /// <param name="multiplier4x2">The multiplier M4X2.</param>
        /// <param name="multiplier4x3">The multiplier M4X3.</param>
        /// <param name="multiplier4x4">The multiplier M4X4.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m1x1, double m1x2, double m1x3, double m1x4,
            double m2x1, double m2x2, double m2x3, double m2x4,
            double m3x1, double m3x2, double m3x3, double m3x4,
            double m4x1, double m4x2, double m4x3, double m4x4)
            Multiply4x4x4x4(
            double multiplicand1x1, double multiplicand1x2, double multiplicand1x3, double multiplicand1x4,
            double multiplicand2x1, double multiplicand2x2, double multiplicand2x3, double multiplicand2x4,
            double multiplicand3x1, double multiplicand3x2, double multiplicand3x3, double multiplicand3x4,
            double multiplicand4x1, double multiplicand4x2, double multiplicand4x3, double multiplicand4x4,
            double multiplier1x1, double multiplier1x2, double multiplier1x3, double multiplier1x4,
            double multiplier2x1, double multiplier2x2, double multiplier2x3, double multiplier2x4,
            double multiplier3x1, double multiplier3x2, double multiplier3x3, double multiplier3x4,
            double multiplier4x1, double multiplier4x2, double multiplier4x3, double multiplier4x4)
            => (
                (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1) + (multiplicand1x3 * multiplier3x1) + (multiplicand1x4 * multiplier4x1), (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2) + (multiplicand1x3 * multiplier3x2) + (multiplicand1x4 * multiplier4x2), (multiplicand1x1 * multiplier1x3) + (multiplicand1x2 * multiplier2x3) + (multiplicand1x3 * multiplier3x3) + (multiplicand1x4 * multiplier4x3), (multiplicand1x1 * multiplier1x4) + (multiplicand1x2 * multiplier2x4) + (multiplicand1x3 * multiplier3x4) + (multiplicand1x4 * multiplier4x4),
                (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1) + (multiplicand2x3 * multiplier3x1) + (multiplicand2x4 * multiplier4x1), (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2) + (multiplicand2x3 * multiplier3x2) + (multiplicand2x4 * multiplier4x2), (multiplicand2x1 * multiplier1x3) + (multiplicand2x2 * multiplier2x3) + (multiplicand2x3 * multiplier3x3) + (multiplicand2x4 * multiplier4x3), (multiplicand2x1 * multiplier1x4) + (multiplicand2x2 * multiplier2x4) + (multiplicand2x3 * multiplier3x4) + (multiplicand2x4 * multiplier4x4),
                (multiplicand3x1 * multiplier1x1) + (multiplicand3x2 * multiplier2x1) + (multiplicand3x3 * multiplier3x1) + (multiplicand3x4 * multiplier4x1), (multiplicand3x1 * multiplier1x2) + (multiplicand3x2 * multiplier2x2) + (multiplicand3x3 * multiplier3x2) + (multiplicand3x4 * multiplier4x2), (multiplicand3x1 * multiplier1x3) + (multiplicand3x2 * multiplier2x3) + (multiplicand3x3 * multiplier3x3) + (multiplicand3x4 * multiplier4x3), (multiplicand3x1 * multiplier1x4) + (multiplicand3x2 * multiplier2x4) + (multiplicand3x3 * multiplier3x4) + (multiplicand3x4 * multiplier4x4),
                (multiplicand4x1 * multiplier1x1) + (multiplicand4x2 * multiplier2x1) + (multiplicand4x3 * multiplier3x1) + (multiplicand4x4 * multiplier4x1), (multiplicand4x1 * multiplier1x2) + (multiplicand4x2 * multiplier2x2) + (multiplicand4x3 * multiplier3x2) + (multiplicand4x4 * multiplier4x2), (multiplicand4x1 * multiplier1x3) + (multiplicand4x2 * multiplier2x3) + (multiplicand4x3 * multiplier3x3) + (multiplicand4x4 * multiplier4x3), (multiplicand4x1 * multiplier1x4) + (multiplicand4x2 * multiplier2x4) + (multiplicand4x3 * multiplier3x4) + (multiplicand4x4 * multiplier4x4)
            );

        /// <summary>
        /// Multiply2x2x5x5s the specified multiplicand M1X1.
        /// </summary>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier1x4">The multiplier M1X4.</param>
        /// <param name="multiplier1x5">The multiplier M1X5.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier2x4">The multiplier M2X4.</param>
        /// <param name="multiplier2x5">The multiplier M2X5.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <param name="multiplier3x4">The multiplier M3X4.</param>
        /// <param name="multiplier3x5">The multiplier M3X5.</param>
        /// <param name="multiplier4x1">The multiplier M4X1.</param>
        /// <param name="multiplier4x2">The multiplier M4X2.</param>
        /// <param name="multiplier4x3">The multiplier M4X3.</param>
        /// <param name="multiplier4x4">The multiplier M4X4.</param>
        /// <param name="multiplier4x5">The multiplier M4X5.</param>
        /// <param name="multiplier5x1">The multiplier M5X1.</param>
        /// <param name="multiplier5x2">The multiplier M5X2.</param>
        /// <param name="multiplier5x3">The multiplier M5X3.</param>
        /// <param name="multiplier5x4">The multiplier M5X4.</param>
        /// <param name="multiplier5x5">The multiplier M5X5.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M1x1, double M1x2, double M1x3, double M1x4, double M1x5,
            double M2x1, double M2x2, double M2x3, double M2x4, double M2x5,
            double M3x1, double M3x2, double M3x3, double M3x4, double M3x5,
            double M4x1, double M4x2, double M4x3, double M4x4, double M4x5,
            double M5x1, double M5x2, double M5x3, double M5x4, double M5x5)
            Multiply2x2x5x5(
            double multiplicand1x1, double multiplicand1x2,
            double multiplicand2x1, double multiplicand2x2,
            double multiplier1x1, double multiplier1x2, double multiplier1x3, double multiplier1x4, double multiplier1x5,
            double multiplier2x1, double multiplier2x2, double multiplier2x3, double multiplier2x4, double multiplier2x5,
            double multiplier3x1, double multiplier3x2, double multiplier3x3, double multiplier3x4, double multiplier3x5,
            double multiplier4x1, double multiplier4x2, double multiplier4x3, double multiplier4x4, double multiplier4x5,
            double multiplier5x1, double multiplier5x2, double multiplier5x3, double multiplier5x4, double multiplier5x5)
            => (
                (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1) + multiplier3x1 + multiplier4x1 + multiplier5x1,
                (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2) + multiplier3x2 + multiplier4x2 + multiplier5x2,
                (multiplicand1x1 * multiplier1x3) + (multiplicand1x2 * multiplier2x3) + multiplier3x3 + multiplier4x3 + multiplier5x3,
                (multiplicand1x1 * multiplier1x4) + (multiplicand1x2 * multiplier2x4) + multiplier3x4 + multiplier4x4 + multiplier5x4,
                (multiplicand1x1 * multiplier1x5) + (multiplicand1x2 * multiplier2x5) + multiplier3x5 + multiplier4x5 + multiplier5x5,
                (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1) + multiplier3x1 + multiplier4x1 + multiplier5x1,
                (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2) + multiplier3x2 + multiplier4x2 + multiplier5x2,
                (multiplicand2x1 * multiplier1x3) + (multiplicand2x2 * multiplier2x3) + multiplier3x3 + multiplier4x3 + multiplier5x3,
                (multiplicand2x1 * multiplier1x4) + (multiplicand2x2 * multiplier2x4) + multiplier3x4 + multiplier4x4 + multiplier5x4,
                (multiplicand2x1 * multiplier1x5) + (multiplicand2x2 * multiplier2x5) + multiplier3x5 + multiplier4x5 + multiplier5x5,
                multiplier1x1 + multiplier2x1 + multiplier3x1 + multiplier4x1 + multiplier5x1,
                multiplier1x2 + multiplier2x2 + multiplier3x2 + multiplier4x2 + multiplier5x2,
                multiplier1x3 + multiplier2x3 + multiplier3x3 + multiplier4x3 + multiplier5x3,
                multiplier1x4 + multiplier2x4 + multiplier3x4 + multiplier4x4 + multiplier5x4,
                multiplier1x5 + multiplier2x5 + multiplier3x5 + multiplier4x5 + multiplier5x5,
                multiplier1x1 + multiplier2x1 + multiplier3x1 + multiplier4x1 + multiplier5x1,
                multiplier1x2 + multiplier2x2 + multiplier3x2 + multiplier4x2 + multiplier5x2,
                multiplier1x3 + multiplier2x3 + multiplier3x3 + multiplier4x3 + multiplier5x3,
                multiplier1x4 + multiplier2x4 + multiplier3x4 + multiplier4x4 + multiplier5x4,
                multiplier1x5 + multiplier2x5 + multiplier3x5 + multiplier4x5 + multiplier5x5,
                multiplier1x1 + multiplier2x1 + multiplier3x1 + multiplier4x1 + multiplier5x1,
                multiplier1x2 + multiplier2x2 + multiplier3x2 + multiplier4x2 + multiplier5x2,
                multiplier1x3 + multiplier2x3 + multiplier3x3 + multiplier4x3 + multiplier5x3,
                multiplier1x4 + multiplier2x4 + multiplier3x4 + multiplier4x4 + multiplier5x4,
                multiplier1x5 + multiplier2x5 + multiplier3x5 + multiplier4x5 + multiplier5x5
            );

        /// <summary>
        /// Multiply5x5x2x2s the specified multiplicand M1X1.
        /// </summary>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicand1x5">The multiplicand M1X5.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicand2x5">The multiplicand M2X5.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicand3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicand3x5">The multiplicand M3X5.</param>
        /// <param name="multiplicand4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicand4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicand4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicand4x4">The multiplicand M4X4.</param>
        /// <param name="multiplicand4x5">The multiplicand M4X5.</param>
        /// <param name="multiplicand5x1">The multiplicand M5X1.</param>
        /// <param name="multiplicand5x2">The multiplicand M5X2.</param>
        /// <param name="multiplicand5x3">The multiplicand M5X3.</param>
        /// <param name="multiplicand5x4">The multiplicand M5X4.</param>
        /// <param name="multiplicand5x5">The multiplicand M5X5.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M1x1, double M1x2, double M1x3, double M1x4, double M1x5,
            double M2x1, double M2x2, double M2x3, double M2x4, double M2x5,
            double M3x1, double M3x2, double M3x3, double M3x4, double M3x5,
            double M4x1, double M4x2, double M4x3, double M4x4, double M4x5,
            double M5x1, double M5x2, double M5x3, double M5x4, double M5x5)
            Multiply5x5x2x2(
            double multiplicand1x1, double multiplicand1x2, double multiplicand1x3, double multiplicand1x4, double multiplicand1x5,
            double multiplicand2x1, double multiplicand2x2, double multiplicand2x3, double multiplicand2x4, double multiplicand2x5,
            double multiplicand3x1, double multiplicand3x2, double multiplicand3x3, double multiplicand3x4, double multiplicand3x5,
            double multiplicand4x1, double multiplicand4x2, double multiplicand4x3, double multiplicand4x4, double multiplicand4x5,
            double multiplicand5x1, double multiplicand5x2, double multiplicand5x3, double multiplicand5x4, double multiplicand5x5,
            double multiplier1x1, double multiplier1x2,
            double multiplier2x1, double multiplier2x2)
            => (
                (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1) + multiplicand1x3 + multiplicand1x4 + multiplicand1x5,
                (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2) + multiplicand1x3 + multiplicand1x4 + multiplicand1x5,
                multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5,
                multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5,
                multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5,
                (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1) + multiplicand2x3 + multiplicand2x4 + multiplicand2x5,
                (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2) + multiplicand2x3 + multiplicand2x4 + multiplicand2x5,
                multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5,
                multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5,
                multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5,
                (multiplicand3x1 * multiplier1x1) + (multiplicand3x2 * multiplier2x1) + multiplicand3x3 + multiplicand3x4 + multiplicand3x5,
                (multiplicand3x1 * multiplier1x2) + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5,
                multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5,
                multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5,
                multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5,
                (multiplicand4x1 * multiplier1x1) + (multiplicand4x2 * multiplier2x1) + multiplicand4x3 + multiplicand4x4 + multiplicand4x5,
                (multiplicand4x1 * multiplier1x2) + (multiplicand4x2 * multiplier2x2) + multiplicand4x3 + multiplicand4x4 + multiplicand4x5,
                multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5,
                multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5,
                multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5,
                (multiplicand5x1 * multiplier1x1) + (multiplicand5x2 * multiplier2x1) + multiplicand5x3 + multiplicand5x4 + multiplicand5x5,
                (multiplicand5x1 * multiplier1x2) + (multiplicand5x2 * multiplier2x2) + multiplicand5x3 + multiplicand5x4 + multiplicand5x5,
                multiplicand5x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5,
                multiplicand5x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5,
                multiplicand5x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5
            );

        /// <summary>
        /// Multiply3x3x5x5s the specified multiplicand M1X1.
        /// </summary>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier1x4">The multiplier M1X4.</param>
        /// <param name="multiplier1x5">The multiplier M1X5.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier2x4">The multiplier M2X4.</param>
        /// <param name="multiplier2x5">The multiplier M2X5.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <param name="multiplier3x4">The multiplier M3X4.</param>
        /// <param name="multiplier3x5">The multiplier M3X5.</param>
        /// <param name="multiplier4x1">The multiplier M4X1.</param>
        /// <param name="multiplier4x2">The multiplier M4X2.</param>
        /// <param name="multiplier4x3">The multiplier M4X3.</param>
        /// <param name="multiplier4x4">The multiplier M4X4.</param>
        /// <param name="multiplier4x5">The multiplier M4X5.</param>
        /// <param name="multiplier5x1">The multiplier M5X1.</param>
        /// <param name="multiplier5x2">The multiplier M5X2.</param>
        /// <param name="multiplier5x3">The multiplier M5X3.</param>
        /// <param name="multiplier5x4">The multiplier M5X4.</param>
        /// <param name="multiplier5x5">The multiplier M5X5.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M1x1, double M1x2, double M1x3, double M1x4, double M1x5,
            double M2x1, double M2x2, double M2x3, double M2x4, double M2x5,
            double M3x1, double M3x2, double M3x3, double M3x4, double M3x5,
            double M4x1, double M4x2, double M4x3, double M4x4, double M4x5,
            double M5x1, double M5x2, double M5x3, double M5x4, double M5x5)
            Multiply3x3x5x5(
            double multiplicand1x1, double multiplicand1x2, double multiplicand1x3,
            double multiplicand2x1, double multiplicand2x2, double multiplicand2x3,
            double multiplicand3x1, double multiplicand3x2, double multiplicand3x3,
            double multiplier1x1, double multiplier1x2, double multiplier1x3, double multiplier1x4, double multiplier1x5,
            double multiplier2x1, double multiplier2x2, double multiplier2x3, double multiplier2x4, double multiplier2x5,
            double multiplier3x1, double multiplier3x2, double multiplier3x3, double multiplier3x4, double multiplier3x5,
            double multiplier4x1, double multiplier4x2, double multiplier4x3, double multiplier4x4, double multiplier4x5,
            double multiplier5x1, double multiplier5x2, double multiplier5x3, double multiplier5x4, double multiplier5x5)
            => (
                (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1) + (multiplicand1x3 * multiplier3x1) + multiplier4x1 + multiplier5x1,
                (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2) + (multiplicand1x3 * multiplier3x2) + multiplier4x2 + multiplier5x2,
                (multiplicand1x1 * multiplier1x3) + (multiplicand1x2 * multiplier2x3) + (multiplicand1x3 * multiplier3x3) + multiplier4x3 + multiplier5x3,
                (multiplicand1x1 * multiplier1x4) + (multiplicand1x2 * multiplier2x4) + (multiplicand1x3 * multiplier3x4) + multiplier4x4 + multiplier5x4,
                (multiplicand1x1 * multiplier1x5) + (multiplicand1x2 * multiplier2x5) + (multiplicand1x3 * multiplier3x5) + multiplier4x5 + multiplier5x5,
                (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1) + (multiplicand2x3 * multiplier3x1) + multiplier4x1 + multiplier5x1,
                (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2) + (multiplicand2x3 * multiplier3x2) + multiplier4x2 + multiplier5x2,
                (multiplicand2x1 * multiplier1x3) + (multiplicand2x2 * multiplier2x3) + (multiplicand2x3 * multiplier3x3) + multiplier4x3 + multiplier5x3,
                (multiplicand2x1 * multiplier1x4) + (multiplicand2x2 * multiplier2x4) + (multiplicand2x3 * multiplier3x4) + multiplier4x4 + multiplier5x4,
                (multiplicand2x1 * multiplier1x5) + (multiplicand2x2 * multiplier2x5) + (multiplicand2x3 * multiplier3x5) + multiplier4x5 + multiplier5x5,
                (multiplicand3x1 * multiplier1x1) + (multiplicand3x2 * multiplier2x1) + (multiplicand3x3 * multiplier3x1) + multiplier4x1 + multiplier5x1,
                (multiplicand3x1 * multiplier1x2) + (multiplicand3x2 * multiplier2x2) + (multiplicand3x3 * multiplier3x2) + multiplier4x2 + multiplier5x2,
                (multiplicand3x1 * multiplier1x3) + (multiplicand3x2 * multiplier2x3) + (multiplicand3x3 * multiplier3x3) + multiplier4x3 + multiplier5x3,
                (multiplicand3x1 * multiplier1x4) + (multiplicand3x2 * multiplier2x4) + (multiplicand3x3 * multiplier3x4) + multiplier4x4 + multiplier5x4,
                (multiplicand3x1 * multiplier1x5) + (multiplicand3x2 * multiplier2x5) + (multiplicand3x3 * multiplier3x5) + multiplier4x5 + multiplier5x5,
                multiplier1x1 + multiplier2x1 + multiplier3x1 + multiplier4x1 + multiplier5x1,
                multiplier1x2 + multiplier2x2 + multiplier3x2 + multiplier4x2 + multiplier5x2,
                multiplier1x3 + multiplier2x3 + multiplier3x3 + multiplier4x3 + multiplier5x3,
                multiplier1x4 + multiplier2x4 + multiplier3x4 + multiplier4x4 + multiplier5x4,
                multiplier1x5 + multiplier2x5 + multiplier3x5 + multiplier4x5 + multiplier5x5,
                multiplier1x1 + multiplier2x1 + multiplier3x1 + multiplier4x1 + multiplier5x1,
                multiplier1x2 + multiplier2x2 + multiplier3x2 + multiplier4x2 + multiplier5x2,
                multiplier1x3 + multiplier2x3 + multiplier3x3 + multiplier4x3 + multiplier5x3,
                multiplier1x4 + multiplier2x4 + multiplier3x4 + multiplier4x4 + multiplier5x4,
                multiplier1x5 + multiplier2x5 + multiplier3x5 + multiplier4x5 + multiplier5x5
            );

        /// <summary>
        /// Multiply5x5x3x3s the specified multiplicand M1X1.
        /// </summary>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicand1x5">The multiplicand M1X5.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicand2x5">The multiplicand M2X5.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicand3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicand3x5">The multiplicand M3X5.</param>
        /// <param name="multiplicand4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicand4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicand4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicand4x4">The multiplicand M4X4.</param>
        /// <param name="multiplicand4x5">The multiplicand M4X5.</param>
        /// <param name="multiplicand5x1">The multiplicand M5X1.</param>
        /// <param name="multiplicand5x2">The multiplicand M5X2.</param>
        /// <param name="multiplicand5x3">The multiplicand M5X3.</param>
        /// <param name="multiplicand5x4">The multiplicand M5X4.</param>
        /// <param name="multiplicand5x5">The multiplicand M5X5.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M1x1, double M1x2, double M1x3, double M1x4, double M1x5,
            double M2x1, double M2x2, double M2x3, double M2x4, double M2x5,
            double M3x1, double M3x2, double M3x3, double M3x4, double M3x5,
            double M4x1, double M4x2, double M4x3, double M4x4, double M4x5,
            double M5x1, double M5x2, double M5x3, double M5x4, double M5x5)
            Multiply5x5x3x3(
            double multiplicand1x1, double multiplicand1x2, double multiplicand1x3, double multiplicand1x4, double multiplicand1x5,
            double multiplicand2x1, double multiplicand2x2, double multiplicand2x3, double multiplicand2x4, double multiplicand2x5,
            double multiplicand3x1, double multiplicand3x2, double multiplicand3x3, double multiplicand3x4, double multiplicand3x5,
            double multiplicand4x1, double multiplicand4x2, double multiplicand4x3, double multiplicand4x4, double multiplicand4x5,
            double multiplicand5x1, double multiplicand5x2, double multiplicand5x3, double multiplicand5x4, double multiplicand5x5,
            double multiplier1x1, double multiplier1x2, double multiplier1x3,
            double multiplier2x1, double multiplier2x2, double multiplier2x3,
            double multiplier3x1, double multiplier3x2, double multiplier3x3)
            => (
                (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1) + (multiplicand1x3 * multiplier3x1) + multiplicand1x4 + multiplicand1x5,
                (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2) + (multiplicand1x3 * multiplier3x2) + multiplicand1x4 + multiplicand1x5,
                (multiplicand1x1 * multiplier1x3) + (multiplicand1x2 * multiplier2x3) + (multiplicand1x3 * multiplier3x3) + multiplicand1x4 + multiplicand1x5,
                multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5,
                multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5,
                (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1) + (multiplicand2x3 * multiplier3x1) + multiplicand2x4 + multiplicand2x5,
                (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2) + (multiplicand2x3 * multiplier3x2) + multiplicand2x4 + multiplicand2x5,
                (multiplicand2x1 * multiplier1x3) + (multiplicand2x2 * multiplier2x3) + (multiplicand2x3 * multiplier3x3) + multiplicand2x4 + multiplicand2x5,
                multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5,
                multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5,
                (multiplicand3x1 * multiplier1x1) + (multiplicand3x2 * multiplier2x1) + (multiplicand3x3 * multiplier3x1) + multiplicand3x4 + multiplicand3x5,
                (multiplicand3x1 * multiplier1x2) + (multiplicand3x2 * multiplier2x2) + (multiplicand3x3 * multiplier3x2) + multiplicand3x4 + multiplicand3x5,
                (multiplicand3x1 * multiplier1x3) + (multiplicand3x2 * multiplier2x3) + (multiplicand3x3 * multiplier3x3) + multiplicand3x4 + multiplicand3x5,
                multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5,
                multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5,
                (multiplicand4x1 * multiplier1x1) + (multiplicand4x2 * multiplier2x1) + (multiplicand4x3 * multiplier3x1) + multiplicand4x4 + multiplicand4x5,
                (multiplicand4x1 * multiplier1x2) + (multiplicand4x2 * multiplier2x2) + (multiplicand4x3 * multiplier3x2) + multiplicand4x4 + multiplicand4x5,
                (multiplicand4x1 * multiplier1x3) + (multiplicand4x2 * multiplier2x3) + (multiplicand4x3 * multiplier3x3) + multiplicand4x4 + multiplicand4x5,
                multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5,
                multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5,
                (multiplicand5x1 * multiplier1x1) + (multiplicand5x2 * multiplier2x1) + (multiplicand5x3 * multiplier3x1) + multiplicand5x4 + multiplicand5x5,
                (multiplicand5x1 * multiplier1x2) + (multiplicand5x2 * multiplier2x2) + (multiplicand5x3 * multiplier3x2) + multiplicand5x4 + multiplicand5x5,
                (multiplicand5x1 * multiplier1x3) + (multiplicand5x2 * multiplier2x3) + (multiplicand5x3 * multiplier3x3) + multiplicand5x4 + multiplicand5x5,
                multiplicand5x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5,
                multiplicand5x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5
            );

        /// <summary>
        /// Multiply4x4x5x5s the specified multiplicand M1X1.
        /// </summary>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicand3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicand4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicand4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicand4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicand4x4">The multiplicand M4X4.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier1x4">The multiplier M1X4.</param>
        /// <param name="multiplier1x5">The multiplier M1X5.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier2x4">The multiplier M2X4.</param>
        /// <param name="multiplier2x5">The multiplier M2X5.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <param name="multiplier3x4">The multiplier M3X4.</param>
        /// <param name="multiplier3x5">The multiplier M3X5.</param>
        /// <param name="multiplier4x1">The multiplier M4X1.</param>
        /// <param name="multiplier4x2">The multiplier M4X2.</param>
        /// <param name="multiplier4x3">The multiplier M4X3.</param>
        /// <param name="multiplier4x4">The multiplier M4X4.</param>
        /// <param name="multiplier4x5">The multiplier M4X5.</param>
        /// <param name="multiplier5x1">The multiplier M5X1.</param>
        /// <param name="multiplier5x2">The multiplier M5X2.</param>
        /// <param name="multiplier5x3">The multiplier M5X3.</param>
        /// <param name="multiplier5x4">The multiplier M5X4.</param>
        /// <param name="multiplier5x5">The multiplier M5X5.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M1x1, double M1x2, double M1x3, double M1x4, double M1x5,
            double M2x1, double M2x2, double M2x3, double M2x4, double M2x5,
            double M3x1, double M3x2, double M3x3, double M3x4, double M3x5,
            double M4x1, double M4x2, double M4x3, double M4x4, double M4x5,
            double M5x1, double M5x2, double M5x3, double M5x4, double M5x5)
            Multiply4x4x5x5(
            double multiplicand1x1, double multiplicand1x2, double multiplicand1x3, double multiplicand1x4,
            double multiplicand2x1, double multiplicand2x2, double multiplicand2x3, double multiplicand2x4,
            double multiplicand3x1, double multiplicand3x2, double multiplicand3x3, double multiplicand3x4,
            double multiplicand4x1, double multiplicand4x2, double multiplicand4x3, double multiplicand4x4,
            double multiplier1x1, double multiplier1x2, double multiplier1x3, double multiplier1x4, double multiplier1x5,
            double multiplier2x1, double multiplier2x2, double multiplier2x3, double multiplier2x4, double multiplier2x5,
            double multiplier3x1, double multiplier3x2, double multiplier3x3, double multiplier3x4, double multiplier3x5,
            double multiplier4x1, double multiplier4x2, double multiplier4x3, double multiplier4x4, double multiplier4x5,
            double multiplier5x1, double multiplier5x2, double multiplier5x3, double multiplier5x4, double multiplier5x5)
            => (
                (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1) + (multiplicand1x3 * multiplier3x1) + (multiplicand1x4 * multiplier4x1) + multiplier5x1,
                (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2) + (multiplicand1x3 * multiplier3x2) + (multiplicand1x4 * multiplier4x2) + multiplier5x2,
                (multiplicand1x1 * multiplier1x3) + (multiplicand1x2 * multiplier2x3) + (multiplicand1x3 * multiplier3x3) + (multiplicand1x4 * multiplier4x3) + multiplier5x3,
                (multiplicand1x1 * multiplier1x4) + (multiplicand1x2 * multiplier2x4) + (multiplicand1x3 * multiplier3x4) + (multiplicand1x4 * multiplier4x4) + multiplier5x4,
                (multiplicand1x1 * multiplier1x5) + (multiplicand1x2 * multiplier2x5) + (multiplicand1x3 * multiplier3x5) + (multiplicand1x4 * multiplier4x5) + multiplier5x5,
                (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1) + (multiplicand2x3 * multiplier3x1) + (multiplicand2x4 * multiplier4x1) + multiplier5x1,
                (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2) + (multiplicand2x3 * multiplier3x2) + (multiplicand2x4 * multiplier4x2) + multiplier5x2,
                (multiplicand2x1 * multiplier1x3) + (multiplicand2x2 * multiplier2x3) + (multiplicand2x3 * multiplier3x3) + (multiplicand2x4 * multiplier4x3) + multiplier5x3,
                (multiplicand2x1 * multiplier1x4) + (multiplicand2x2 * multiplier2x4) + (multiplicand2x3 * multiplier3x4) + (multiplicand2x4 * multiplier4x4) + multiplier5x4,
                (multiplicand2x1 * multiplier1x5) + (multiplicand2x2 * multiplier2x5) + (multiplicand2x3 * multiplier3x5) + (multiplicand2x4 * multiplier4x5) + multiplier5x5,
                (multiplicand3x1 * multiplier1x1) + (multiplicand3x2 * multiplier2x1) + (multiplicand3x3 * multiplier3x1) + (multiplicand3x4 * multiplier4x1) + multiplier5x1,
                (multiplicand3x1 * multiplier1x2) + (multiplicand3x2 * multiplier2x2) + (multiplicand3x3 * multiplier3x2) + (multiplicand3x4 * multiplier4x2) + multiplier5x2,
                (multiplicand3x1 * multiplier1x3) + (multiplicand3x2 * multiplier2x3) + (multiplicand3x3 * multiplier3x3) + (multiplicand3x4 * multiplier4x3) + multiplier5x3,
                (multiplicand3x1 * multiplier1x4) + (multiplicand3x2 * multiplier2x4) + (multiplicand3x3 * multiplier3x4) + (multiplicand3x4 * multiplier4x4) + multiplier5x4,
                (multiplicand3x1 * multiplier1x5) + (multiplicand3x2 * multiplier2x5) + (multiplicand3x3 * multiplier3x5) + (multiplicand3x4 * multiplier4x5) + multiplier5x5,
                (multiplicand4x1 * multiplier1x1) + (multiplicand4x2 * multiplier2x1) + (multiplicand4x3 * multiplier3x1) + (multiplicand4x4 * multiplier4x1) + multiplier5x1,
                (multiplicand4x1 * multiplier1x2) + (multiplicand4x2 * multiplier2x2) + (multiplicand4x3 * multiplier3x2) + (multiplicand4x4 * multiplier4x2) + multiplier5x2,
                (multiplicand4x1 * multiplier1x3) + (multiplicand4x2 * multiplier2x3) + (multiplicand4x3 * multiplier3x3) + (multiplicand4x4 * multiplier4x3) + multiplier5x3,
                (multiplicand4x1 * multiplier1x4) + (multiplicand4x2 * multiplier2x4) + (multiplicand4x3 * multiplier3x4) + (multiplicand4x4 * multiplier4x4) + multiplier5x4,
                (multiplicand4x1 * multiplier1x5) + (multiplicand4x2 * multiplier2x5) + (multiplicand4x3 * multiplier3x5) + (multiplicand4x4 * multiplier4x5) + multiplier5x5,
                multiplier1x1 + multiplier2x1 + multiplier3x1 + multiplier4x1 + multiplier5x1,
                multiplier1x2 + multiplier2x2 + multiplier3x2 + multiplier4x2 + multiplier5x2,
                multiplier1x3 + multiplier2x3 + multiplier3x3 + multiplier4x3 + multiplier5x3,
                multiplier1x4 + multiplier2x4 + multiplier3x4 + multiplier4x4 + multiplier5x4,
                multiplier1x5 + multiplier2x5 + multiplier3x5 + multiplier4x5 + multiplier5x5
            );

        /// <summary>
        /// Multiply5x5x4x4s the specified multiplicand M1X1.
        /// </summary>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicand1x5">The multiplicand M1X5.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicand2x5">The multiplicand M2X5.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicand3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicand3x5">The multiplicand M3X5.</param>
        /// <param name="multiplicand4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicand4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicand4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicand4x4">The multiplicand M4X4.</param>
        /// <param name="multiplicand4x5">The multiplicand M4X5.</param>
        /// <param name="multiplicand5x1">The multiplicand M5X1.</param>
        /// <param name="multiplicand5x2">The multiplicand M5X2.</param>
        /// <param name="multiplicand5x3">The multiplicand M5X3.</param>
        /// <param name="multiplicand5x4">The multiplicand M5X4.</param>
        /// <param name="multiplicand5x5">The multiplicand M5X5.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier1x4">The multiplier M1X4.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier2x4">The multiplier M2X4.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <param name="multiplier3x4">The multiplier M3X4.</param>
        /// <param name="multiplier4x1">The multiplier M4X1.</param>
        /// <param name="multiplier4x2">The multiplier M4X2.</param>
        /// <param name="multiplier4x3">The multiplier M4X3.</param>
        /// <param name="multiplier4x4">The multiplier M4X4.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M1x1, double M1x2, double M1x3, double M1x4, double M1x5,
            double M2x1, double M2x2, double M2x3, double M2x4, double M2x5,
            double M3x1, double M3x2, double M3x3, double M3x4, double M3x5,
            double M4x1, double M4x2, double M4x3, double M4x4, double M4x5,
            double M5x1, double M5x2, double M5x3, double M5x4, double M5x5)
            Multiply5x5x4x4(
            double multiplicand1x1, double multiplicand1x2, double multiplicand1x3, double multiplicand1x4, double multiplicand1x5,
            double multiplicand2x1, double multiplicand2x2, double multiplicand2x3, double multiplicand2x4, double multiplicand2x5,
            double multiplicand3x1, double multiplicand3x2, double multiplicand3x3, double multiplicand3x4, double multiplicand3x5,
            double multiplicand4x1, double multiplicand4x2, double multiplicand4x3, double multiplicand4x4, double multiplicand4x5,
            double multiplicand5x1, double multiplicand5x2, double multiplicand5x3, double multiplicand5x4, double multiplicand5x5,
            double multiplier1x1, double multiplier1x2, double multiplier1x3, double multiplier1x4,
            double multiplier2x1, double multiplier2x2, double multiplier2x3, double multiplier2x4,
            double multiplier3x1, double multiplier3x2, double multiplier3x3, double multiplier3x4,
            double multiplier4x1, double multiplier4x2, double multiplier4x3, double multiplier4x4)
            => (
                (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1) + (multiplicand1x3 * multiplier3x1) + (multiplicand1x4 * multiplier4x1) + multiplicand1x5,
                (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2) + (multiplicand1x3 * multiplier3x2) + (multiplicand1x4 * multiplier4x2) + multiplicand1x5,
                (multiplicand1x1 * multiplier1x3) + (multiplicand1x2 * multiplier2x3) + (multiplicand1x3 * multiplier3x3) + (multiplicand1x4 * multiplier4x3) + multiplicand1x5,
                (multiplicand1x1 * multiplier1x4) + (multiplicand1x2 * multiplier2x4) + (multiplicand1x3 * multiplier3x4) + (multiplicand1x4 * multiplier4x4) + multiplicand1x5,
                multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5,
                (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1) + (multiplicand2x3 * multiplier3x1) + (multiplicand2x4 * multiplier4x1) + multiplicand2x5,
                (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2) + (multiplicand2x3 * multiplier3x2) + (multiplicand2x4 * multiplier4x2) + multiplicand2x5,
                (multiplicand2x1 * multiplier1x3) + (multiplicand2x2 * multiplier2x3) + (multiplicand2x3 * multiplier3x3) + (multiplicand2x4 * multiplier4x3) + multiplicand2x5,
                (multiplicand2x1 * multiplier1x4) + (multiplicand2x2 * multiplier2x4) + (multiplicand2x3 * multiplier3x4) + (multiplicand2x4 * multiplier4x4) + multiplicand2x5,
                multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5,
                (multiplicand3x1 * multiplier1x1) + (multiplicand3x2 * multiplier2x1) + (multiplicand3x3 * multiplier3x1) + (multiplicand3x4 * multiplier4x1) + multiplicand3x5,
                (multiplicand3x1 * multiplier1x2) + (multiplicand3x2 * multiplier2x2) + (multiplicand3x3 * multiplier3x2) + (multiplicand3x4 * multiplier4x2) + multiplicand3x5,
                (multiplicand3x1 * multiplier1x3) + (multiplicand3x2 * multiplier2x3) + (multiplicand3x3 * multiplier3x3) + (multiplicand3x4 * multiplier4x3) + multiplicand3x5,
                (multiplicand3x1 * multiplier1x4) + (multiplicand3x2 * multiplier2x4) + (multiplicand3x3 * multiplier3x4) + (multiplicand3x4 * multiplier4x4) + multiplicand3x5,
                multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5,
                (multiplicand4x1 * multiplier1x1) + (multiplicand4x2 * multiplier2x1) + (multiplicand4x3 * multiplier3x1) + (multiplicand4x4 * multiplier4x1) + multiplicand4x5,
                (multiplicand4x1 * multiplier1x2) + (multiplicand4x2 * multiplier2x2) + (multiplicand4x3 * multiplier3x2) + (multiplicand4x4 * multiplier4x2) + multiplicand4x5,
                (multiplicand4x1 * multiplier1x3) + (multiplicand4x2 * multiplier2x3) + (multiplicand4x3 * multiplier3x3) + (multiplicand4x4 * multiplier4x3) + multiplicand4x5,
                (multiplicand4x1 * multiplier1x4) + (multiplicand4x2 * multiplier2x4) + (multiplicand4x3 * multiplier3x4) + (multiplicand4x4 * multiplier4x4) + multiplicand4x5,
                multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5,
                (multiplicand5x1 * multiplier1x1) + (multiplicand5x2 * multiplier2x1) + (multiplicand5x3 * multiplier3x1) + (multiplicand5x4 * multiplier4x1) + multiplicand5x5,
                (multiplicand5x1 * multiplier1x2) + (multiplicand5x2 * multiplier2x2) + (multiplicand5x3 * multiplier3x2) + (multiplicand5x4 * multiplier4x2) + multiplicand5x5,
                (multiplicand5x1 * multiplier1x3) + (multiplicand5x2 * multiplier2x3) + (multiplicand5x3 * multiplier3x3) + (multiplicand5x4 * multiplier4x3) + multiplicand5x5,
                (multiplicand5x1 * multiplier1x4) + (multiplicand5x2 * multiplier2x4) + (multiplicand5x3 * multiplier3x4) + (multiplicand5x4 * multiplier4x4) + multiplicand5x5,
                multiplicand5x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5
            );

        /// <summary>
        /// Multiply5x5x5x5s the specified multiplicand M1X1.
        /// </summary>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicand1x5">The multiplicand M1X5.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicand2x5">The multiplicand M2X5.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicand3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicand3x5">The multiplicand M3X5.</param>
        /// <param name="multiplicand4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicand4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicand4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicand4x4">The multiplicand M4X4.</param>
        /// <param name="multiplicand4x5">The multiplicand M4X5.</param>
        /// <param name="multiplicand5x1">The multiplicand M5X1.</param>
        /// <param name="multiplicand5x2">The multiplicand M5X2.</param>
        /// <param name="multiplicand5x3">The multiplicand M5X3.</param>
        /// <param name="multiplicand5x4">The multiplicand M5X4.</param>
        /// <param name="multiplicand5x5">The multiplicand M5X5.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier1x4">The multiplier M1X4.</param>
        /// <param name="multiplier1x5">The multiplier M1X5.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier2x4">The multiplier M2X4.</param>
        /// <param name="multiplier2x5">The multiplier M2X5.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <param name="multiplier3x4">The multiplier M3X4.</param>
        /// <param name="multiplier3x5">The multiplier M3X5.</param>
        /// <param name="multiplier4x1">The multiplier M4X1.</param>
        /// <param name="multiplier4x2">The multiplier M4X2.</param>
        /// <param name="multiplier4x3">The multiplier M4X3.</param>
        /// <param name="multiplier4x4">The multiplier M4X4.</param>
        /// <param name="multiplier4x5">The multiplier M4X5.</param>
        /// <param name="multiplier5x1">The multiplier M5X1.</param>
        /// <param name="multiplier5x2">The multiplier M5X2.</param>
        /// <param name="multiplier5x3">The multiplier M5X3.</param>
        /// <param name="multiplier5x4">The multiplier M5X4.</param>
        /// <param name="multiplier5x5">The multiplier M5X5.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M1x1, double M1x2, double M1x3, double M1x4, double M1x5,
            double M2x1, double M2x2, double M2x3, double M2x4, double M2x5,
            double M3x1, double M3x2, double M3x3, double M3x4, double M3x5,
            double M4x1, double M4x2, double M4x3, double M4x4, double M4x5,
            double M5x1, double M5x2, double M5x3, double M5x4, double M5x5)
            Multiply5x5x5x5(
            double multiplicand1x1, double multiplicand1x2, double multiplicand1x3, double multiplicand1x4, double multiplicand1x5,
            double multiplicand2x1, double multiplicand2x2, double multiplicand2x3, double multiplicand2x4, double multiplicand2x5,
            double multiplicand3x1, double multiplicand3x2, double multiplicand3x3, double multiplicand3x4, double multiplicand3x5,
            double multiplicand4x1, double multiplicand4x2, double multiplicand4x3, double multiplicand4x4, double multiplicand4x5,
            double multiplicand5x1, double multiplicand5x2, double multiplicand5x3, double multiplicand5x4, double multiplicand5x5,
            double multiplier1x1, double multiplier1x2, double multiplier1x3, double multiplier1x4, double multiplier1x5,
            double multiplier2x1, double multiplier2x2, double multiplier2x3, double multiplier2x4, double multiplier2x5,
            double multiplier3x1, double multiplier3x2, double multiplier3x3, double multiplier3x4, double multiplier3x5,
            double multiplier4x1, double multiplier4x2, double multiplier4x3, double multiplier4x4, double multiplier4x5,
            double multiplier5x1, double multiplier5x2, double multiplier5x3, double multiplier5x4, double multiplier5x5)
            => (
                (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1) + (multiplicand1x3 * multiplier3x1) + (multiplicand1x4 * multiplier4x1) + (multiplicand1x5 * multiplier5x1),
                (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2) + (multiplicand1x3 * multiplier3x2) + (multiplicand1x4 * multiplier4x2) + (multiplicand1x5 * multiplier5x2),
                (multiplicand1x1 * multiplier1x3) + (multiplicand1x2 * multiplier2x3) + (multiplicand1x3 * multiplier3x3) + (multiplicand1x4 * multiplier4x3) + (multiplicand1x5 * multiplier5x3),
                (multiplicand1x1 * multiplier1x4) + (multiplicand1x2 * multiplier2x4) + (multiplicand1x3 * multiplier3x4) + (multiplicand1x4 * multiplier4x4) + (multiplicand1x5 * multiplier5x4),
                (multiplicand1x1 * multiplier1x5) + (multiplicand1x2 * multiplier2x5) + (multiplicand1x3 * multiplier3x5) + (multiplicand1x4 * multiplier4x5) + (multiplicand1x5 * multiplier5x5),
                (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1) + (multiplicand2x3 * multiplier3x1) + (multiplicand2x4 * multiplier4x1) + (multiplicand2x5 * multiplier5x1),
                (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2) + (multiplicand2x3 * multiplier3x2) + (multiplicand2x4 * multiplier4x2) + (multiplicand2x5 * multiplier5x2),
                (multiplicand2x1 * multiplier1x3) + (multiplicand2x2 * multiplier2x3) + (multiplicand2x3 * multiplier3x3) + (multiplicand2x4 * multiplier4x3) + (multiplicand2x5 * multiplier5x3),
                (multiplicand2x1 * multiplier1x4) + (multiplicand2x2 * multiplier2x4) + (multiplicand2x3 * multiplier3x4) + (multiplicand2x4 * multiplier4x4) + (multiplicand2x5 * multiplier5x4),
                (multiplicand2x1 * multiplier1x5) + (multiplicand2x2 * multiplier2x5) + (multiplicand2x3 * multiplier3x5) + (multiplicand2x4 * multiplier4x5) + (multiplicand2x5 * multiplier5x5),
                (multiplicand3x1 * multiplier1x1) + (multiplicand3x2 * multiplier2x1) + (multiplicand3x3 * multiplier3x1) + (multiplicand3x4 * multiplier4x1) + (multiplicand3x5 * multiplier5x1),
                (multiplicand3x1 * multiplier1x2) + (multiplicand3x2 * multiplier2x2) + (multiplicand3x3 * multiplier3x2) + (multiplicand3x4 * multiplier4x2) + (multiplicand3x5 * multiplier5x2),
                (multiplicand3x1 * multiplier1x3) + (multiplicand3x2 * multiplier2x3) + (multiplicand3x3 * multiplier3x3) + (multiplicand3x4 * multiplier4x3) + (multiplicand3x5 * multiplier5x3),
                (multiplicand3x1 * multiplier1x4) + (multiplicand3x2 * multiplier2x4) + (multiplicand3x3 * multiplier3x4) + (multiplicand3x4 * multiplier4x4) + (multiplicand3x5 * multiplier5x4),
                (multiplicand3x1 * multiplier1x5) + (multiplicand3x2 * multiplier2x5) + (multiplicand3x3 * multiplier3x5) + (multiplicand3x4 * multiplier4x5) + (multiplicand3x5 * multiplier5x5),
                (multiplicand4x1 * multiplier1x1) + (multiplicand4x2 * multiplier2x1) + (multiplicand4x3 * multiplier3x1) + (multiplicand4x4 * multiplier4x1) + (multiplicand4x5 * multiplier5x1),
                (multiplicand4x1 * multiplier1x2) + (multiplicand4x2 * multiplier2x2) + (multiplicand4x3 * multiplier3x2) + (multiplicand4x4 * multiplier4x2) + (multiplicand4x5 * multiplier5x2),
                (multiplicand4x1 * multiplier1x3) + (multiplicand4x2 * multiplier2x3) + (multiplicand4x3 * multiplier3x3) + (multiplicand4x4 * multiplier4x3) + (multiplicand4x5 * multiplier5x3),
                (multiplicand4x1 * multiplier1x4) + (multiplicand4x2 * multiplier2x4) + (multiplicand4x3 * multiplier3x4) + (multiplicand4x4 * multiplier4x4) + (multiplicand4x5 * multiplier5x4),
                (multiplicand4x1 * multiplier1x5) + (multiplicand4x2 * multiplier2x5) + (multiplicand4x3 * multiplier3x5) + (multiplicand4x4 * multiplier4x5) + (multiplicand4x5 * multiplier5x5),
                (multiplicand5x1 * multiplier1x1) + (multiplicand5x2 * multiplier2x1) + (multiplicand5x3 * multiplier3x1) + (multiplicand5x4 * multiplier4x1) + (multiplicand5x5 * multiplier5x1),
                (multiplicand5x1 * multiplier1x2) + (multiplicand5x2 * multiplier2x2) + (multiplicand5x3 * multiplier3x2) + (multiplicand5x4 * multiplier4x2) + (multiplicand5x5 * multiplier5x2),
                (multiplicand5x1 * multiplier1x3) + (multiplicand5x2 * multiplier2x3) + (multiplicand5x3 * multiplier3x3) + (multiplicand5x4 * multiplier4x3) + (multiplicand5x5 * multiplier5x3),
                (multiplicand5x1 * multiplier1x4) + (multiplicand5x2 * multiplier2x4) + (multiplicand5x3 * multiplier3x4) + (multiplicand5x4 * multiplier4x4) + (multiplicand5x5 * multiplier5x4),
                (multiplicand5x1 * multiplier1x5) + (multiplicand5x2 * multiplier2x5) + (multiplicand5x3 * multiplier3x5) + (multiplicand5x4 * multiplier4x5) + (multiplicand5x5 * multiplier5x5)
            );

        /// <summary>
        /// Multiply2x2x6x6s the specified multiplicand M1X1.
        /// </summary>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier1x4">The multiplier M1X4.</param>
        /// <param name="multiplier1x5">The multiplier M1X5.</param>
        /// <param name="multiplier1x6">The multiplier M1X6.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier2x4">The multiplier M2X4.</param>
        /// <param name="multiplier2x5">The multiplier M2X5.</param>
        /// <param name="multiplier2x6">The multiplier M2X6.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <param name="multiplier3x4">The multiplier M3X4.</param>
        /// <param name="multiplier3x5">The multiplier M3X5.</param>
        /// <param name="multiplier3x6">The multiplier M3X6.</param>
        /// <param name="multiplier4x1">The multiplier M4X1.</param>
        /// <param name="multiplier4x2">The multiplier M4X2.</param>
        /// <param name="multiplier4x3">The multiplier M4X3.</param>
        /// <param name="multiplier4x4">The multiplier M4X4.</param>
        /// <param name="multiplier4x5">The multiplier M4X5.</param>
        /// <param name="multiplier4x6">The multiplier M4X6.</param>
        /// <param name="multiplier5x1">The multiplier M5X1.</param>
        /// <param name="multiplier5x2">The multiplier M5X2.</param>
        /// <param name="multiplier5x3">The multiplier M5X3.</param>
        /// <param name="multiplier5x4">The multiplier M5X4.</param>
        /// <param name="multiplier5x5">The multiplier M5X5.</param>
        /// <param name="multiplier5x6">The multiplier M5X6.</param>
        /// <param name="multiplier6x1">The multiplier M6X1.</param>
        /// <param name="multiplier6x2">The multiplier M6X2.</param>
        /// <param name="multiplier6x3">The multiplier M6X3.</param>
        /// <param name="multiplier6x4">The multiplier M6X4.</param>
        /// <param name="multiplier6x5">The multiplier M6X5.</param>
        /// <param name="multiplier6x6">The multiplier M6X6.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M1x1, double M1x2, double M1x3, double M1x4, double M1x5, double M1x6,
            double M2x1, double M2x2, double M2x3, double M2x4, double M2x5, double M2x6,
            double M3x1, double M3x2, double M3x3, double M3x4, double M3x5, double M3x6,
            double M4x1, double M4x2, double M4x3, double M4x4, double M4x5, double M4x6,
            double M5x1, double M5x2, double M5x3, double M5x4, double M5x5, double M5x6,
            double M6x1, double M6x2, double M6x3, double M6x4, double M6x5, double M6x6)
            Multiply2x2x6x6(
            double multiplicand1x1, double multiplicand1x2,
            double multiplicand2x1, double multiplicand2x2,
            double multiplier1x1, double multiplier1x2, double multiplier1x3, double multiplier1x4, double multiplier1x5, double multiplier1x6,
            double multiplier2x1, double multiplier2x2, double multiplier2x3, double multiplier2x4, double multiplier2x5, double multiplier2x6,
            double multiplier3x1, double multiplier3x2, double multiplier3x3, double multiplier3x4, double multiplier3x5, double multiplier3x6,
            double multiplier4x1, double multiplier4x2, double multiplier4x3, double multiplier4x4, double multiplier4x5, double multiplier4x6,
            double multiplier5x1, double multiplier5x2, double multiplier5x3, double multiplier5x4, double multiplier5x5, double multiplier5x6,
            double multiplier6x1, double multiplier6x2, double multiplier6x3, double multiplier6x4, double multiplier6x5, double multiplier6x6)
            => (
                (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1) + multiplier3x1 + (multiplier4x1 * multiplier5x1) + multiplier6x1,
                (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2) + multiplier3x2 + (multiplier4x2 * multiplier5x2) + multiplier6x2,
                (multiplicand1x1 * multiplier1x3) + (multiplicand1x2 * multiplier2x3) + multiplier3x3 + (multiplier4x3 * multiplier5x3) + multiplier6x3,
                (multiplicand1x1 * multiplier1x4) + (multiplicand1x2 * multiplier2x4) + multiplier3x4 + (multiplier4x4 * multiplier5x4) + multiplier6x4,
                (multiplicand1x1 * multiplier1x5) + (multiplicand1x2 * multiplier2x5) + multiplier3x5 + (multiplier4x5 * multiplier5x5) + multiplier6x5,
                (multiplicand1x1 * multiplier1x6) + (multiplicand1x2 * multiplier2x6) + multiplier3x6 + (multiplier4x6 * multiplier5x6) + multiplier6x6,
                (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1) + multiplier3x1 + (multiplier4x1 * multiplier5x1) + multiplier6x1,
                (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2) + multiplier3x2 + (multiplier4x2 * multiplier5x2) + multiplier6x2,
                (multiplicand2x1 * multiplier1x3) + (multiplicand2x2 * multiplier2x3) + multiplier3x3 + (multiplier4x3 * multiplier5x3) + multiplier6x3,
                (multiplicand2x1 * multiplier1x4) + (multiplicand2x2 * multiplier2x4) + multiplier3x4 + (multiplier4x4 * multiplier5x4) + multiplier6x4,
                (multiplicand2x1 * multiplier1x5) + (multiplicand2x2 * multiplier2x5) + multiplier3x5 + (multiplier4x5 * multiplier5x5) + multiplier6x5,
                (multiplicand2x1 * multiplier1x6) + (multiplicand2x2 * multiplier2x6) + multiplier3x6 + (multiplier4x6 * multiplier5x6) + multiplier6x6,
                multiplier1x1 + multiplier2x1 + multiplier3x1 + (multiplier4x1 * multiplier5x1) + multiplier6x1,
                multiplier1x2 + multiplier2x2 + multiplier3x2 + (multiplier4x2 * multiplier5x2) + multiplier6x2,
                multiplier1x3 + multiplier2x3 + multiplier3x3 + (multiplier4x3 * multiplier5x3) + multiplier6x3,
                multiplier1x4 + multiplier2x4 + multiplier3x4 + (multiplier4x4 * multiplier5x4) + multiplier6x4,
                multiplier1x5 + multiplier2x5 + multiplier3x5 + (multiplier4x5 * multiplier5x5) + multiplier6x5,
                multiplier1x6 + multiplier2x6 + multiplier3x6 + (multiplier4x6 * multiplier5x6) + multiplier6x6,
                multiplier1x1 + multiplier2x1 + multiplier3x1 + (multiplier4x1 * multiplier5x1) + multiplier6x1,
                multiplier1x2 + multiplier2x2 + multiplier3x2 + (multiplier4x2 * multiplier5x2) + multiplier6x2,
                multiplier1x3 + multiplier2x3 + multiplier3x3 + (multiplier4x3 * multiplier5x3) + multiplier6x3,
                multiplier1x4 + multiplier2x4 + multiplier3x4 + (multiplier4x4 * multiplier5x4) + multiplier6x4,
                multiplier1x5 + multiplier2x5 + multiplier3x5 + (multiplier4x5 * multiplier5x5) + multiplier6x5,
                multiplier1x6 + multiplier2x6 + multiplier3x6 + (multiplier4x6 * multiplier5x6) + multiplier6x6,
                multiplier1x1 + multiplier2x1 + multiplier3x1 + (multiplier4x1 * multiplier5x1) + multiplier6x1,
                multiplier1x2 + multiplier2x2 + multiplier3x2 + (multiplier4x2 * multiplier5x2) + multiplier6x2,
                multiplier1x3 + multiplier2x3 + multiplier3x3 + (multiplier4x3 * multiplier5x3) + multiplier6x3,
                multiplier1x4 + multiplier2x4 + multiplier3x4 + (multiplier4x4 * multiplier5x4) + multiplier6x4,
                multiplier1x5 + multiplier2x5 + multiplier3x5 + (multiplier4x5 * multiplier5x5) + multiplier6x5,
                multiplier1x6 + multiplier2x6 + multiplier3x6 + (multiplier4x6 * multiplier5x6) + multiplier6x6,
                multiplier1x1 + multiplier2x1 + multiplier3x1 + (multiplier4x1 * multiplier5x1) + multiplier6x1,
                multiplier1x2 + multiplier2x2 + multiplier3x2 + (multiplier4x2 * multiplier5x2) + multiplier6x2,
                multiplier1x3 + multiplier2x3 + multiplier3x3 + (multiplier4x3 * multiplier5x3) + multiplier6x3,
                multiplier1x4 + multiplier2x4 + multiplier3x4 + (multiplier4x4 * multiplier5x4) + multiplier6x4,
                multiplier1x5 + multiplier2x5 + multiplier3x5 + (multiplier4x5 * multiplier5x5) + multiplier6x5,
                multiplier1x6 + multiplier2x6 + multiplier3x6 + (multiplier4x6 * multiplier5x6) + multiplier6x6
            );

        /// <summary>
        /// Multiply6x6x2x2s the specified multiplicand M1X1.
        /// </summary>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicand1x5">The multiplicand M1X5.</param>
        /// <param name="multiplicand1x6">The multiplicand M1X6.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicand2x5">The multiplicand M2X5.</param>
        /// <param name="multiplicand2x6">The multiplicand M2X6.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicand3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicand3x5">The multiplicand M3X5.</param>
        /// <param name="multiplicand3x6">The multiplicand M3X6.</param>
        /// <param name="multiplicand4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicand4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicand4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicand4x4">The multiplicand M4X4.</param>
        /// <param name="multiplicand4x5">The multiplicand M4X5.</param>
        /// <param name="multiplicand4x6">The multiplicand M4X6.</param>
        /// <param name="multiplicand5x1">The multiplicand M5X1.</param>
        /// <param name="multiplicand5x2">The multiplicand M5X2.</param>
        /// <param name="multiplicand5x3">The multiplicand M5X3.</param>
        /// <param name="multiplicand5x4">The multiplicand M5X4.</param>
        /// <param name="multiplicand5x5">The multiplicand M5X5.</param>
        /// <param name="multiplicand5x6">The multiplicand M5X6.</param>
        /// <param name="multiplicand6x1">The multiplicand M6X1.</param>
        /// <param name="multiplicand6x2">The multiplicand M6X2.</param>
        /// <param name="multiplicand6x3">The multiplicand M6X3.</param>
        /// <param name="multiplicand6x4">The multiplicand M6X4.</param>
        /// <param name="multiplicand6x5">The multiplicand M6X5.</param>
        /// <param name="multiplicand6x6">The multiplicand M6X6.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M1x1, double M1x2, double M1x3, double M1x4, double M1x5, double M1x6,
            double M2x1, double M2x2, double M2x3, double M2x4, double M2x5, double M2x6,
            double M3x1, double M3x2, double M3x3, double M3x4, double M3x5, double M3x6,
            double M4x1, double M4x2, double M4x3, double M4x4, double M4x5, double M4x6,
            double M5x1, double M5x2, double M5x3, double M5x4, double M5x5, double M5x6,
            double M6x1, double M6x2, double M6x3, double M6x4, double M6x5, double M6x6)
            Multiply6x6x2x2(
            double multiplicand1x1, double multiplicand1x2, double multiplicand1x3, double multiplicand1x4, double multiplicand1x5, double multiplicand1x6,
            double multiplicand2x1, double multiplicand2x2, double multiplicand2x3, double multiplicand2x4, double multiplicand2x5, double multiplicand2x6,
            double multiplicand3x1, double multiplicand3x2, double multiplicand3x3, double multiplicand3x4, double multiplicand3x5, double multiplicand3x6,
            double multiplicand4x1, double multiplicand4x2, double multiplicand4x3, double multiplicand4x4, double multiplicand4x5, double multiplicand4x6,
            double multiplicand5x1, double multiplicand5x2, double multiplicand5x3, double multiplicand5x4, double multiplicand5x5, double multiplicand5x6,
            double multiplicand6x1, double multiplicand6x2, double multiplicand6x3, double multiplicand6x4, double multiplicand6x5, double multiplicand6x6,
            double multiplier1x1, double multiplier1x2,
            double multiplier2x1, double multiplier2x2)
            => (
                (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1) + multiplicand1x3 + multiplicand1x4 + multiplicand1x5 + multiplicand1x6,
                (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2) + multiplicand1x3 + multiplicand1x4 + multiplicand1x5 + multiplicand1x6,
                multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5 + multiplicand1x6,
                multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5 + multiplicand1x6,
                multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5 + multiplicand1x6,
                multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5 + multiplicand1x6,
                (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1) + multiplicand2x3 + multiplicand2x4 + multiplicand2x5 + multiplicand2x6,
                (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2) + multiplicand2x3 + multiplicand2x4 + multiplicand2x5 + multiplicand2x6,
                multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5 + multiplicand2x6,
                multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5 + multiplicand2x6,
                multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5 + multiplicand2x6,
                multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5 + multiplicand2x6,
                (multiplicand3x1 * multiplier1x1) + (multiplicand3x2 * multiplier2x1) + multiplicand3x3 + multiplicand3x4 + multiplicand3x5 + multiplicand3x6,
                (multiplicand3x1 * multiplier1x2) + (multiplicand3x2 * multiplier2x2) + multiplicand3x3 + multiplicand3x4 + multiplicand3x5 + multiplicand3x6,
                multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5 + multiplicand3x6,
                multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5 + multiplicand3x6,
                multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5 + multiplicand3x6,
                multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5 + multiplicand3x6,
                (multiplicand4x1 * multiplier1x1) + (multiplicand4x2 * multiplier2x1) + multiplicand4x3 + multiplicand4x4 + multiplicand4x5 + multiplicand4x6,
                (multiplicand4x1 * multiplier1x2) + (multiplicand4x2 * multiplier2x2) + multiplicand4x3 + multiplicand4x4 + multiplicand4x5 + multiplicand4x6,
                multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5 + multiplicand4x6,
                multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5 + multiplicand4x6,
                multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5 + multiplicand4x6,
                multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5 + multiplicand4x6,
                (multiplicand5x1 * multiplier1x1) + (multiplicand5x2 * multiplier2x1) + multiplicand5x3 + multiplicand5x4 + multiplicand5x5 + multiplicand5x6,
                (multiplicand5x1 * multiplier1x2) + (multiplicand5x2 * multiplier2x2) + multiplicand5x3 + multiplicand5x4 + multiplicand5x5 + multiplicand5x6,
                multiplicand5x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5 + multiplicand5x6,
                multiplicand5x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5 + multiplicand5x6,
                multiplicand5x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5 + multiplicand5x6,
                multiplicand5x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5 + multiplicand5x6,
                (multiplicand6x1 * multiplier1x1) + (multiplicand6x2 * multiplier2x1) + multiplicand6x3 + multiplicand6x4 + multiplicand6x5 + multiplicand6x6,
                (multiplicand6x1 * multiplier1x2) + (multiplicand6x2 * multiplier2x2) + multiplicand6x3 + multiplicand6x4 + multiplicand6x5 + multiplicand6x6,
                multiplicand6x1 + multiplicand6x2 + multiplicand6x3 + multiplicand6x4 + multiplicand6x5 + multiplicand6x6,
                multiplicand6x1 + multiplicand6x2 + multiplicand6x3 + multiplicand6x4 + multiplicand6x5 + multiplicand6x6,
                multiplicand6x1 + multiplicand6x2 + multiplicand6x3 + multiplicand6x4 + multiplicand6x5 + multiplicand6x6,
                multiplicand6x1 + multiplicand6x2 + multiplicand6x3 + multiplicand6x4 + multiplicand6x5 + multiplicand6x6
            );

        /// <summary>
        /// Multiply3x3x6x6s the specified multiplicand M1X1.
        /// </summary>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier1x4">The multiplier M1X4.</param>
        /// <param name="multiplier1x5">The multiplier M1X5.</param>
        /// <param name="multiplier1x6">The multiplier M1X6.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier2x4">The multiplier M2X4.</param>
        /// <param name="multiplier2x5">The multiplier M2X5.</param>
        /// <param name="multiplier2x6">The multiplier M2X6.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <param name="multiplier3x4">The multiplier M3X4.</param>
        /// <param name="multiplier3x5">The multiplier M3X5.</param>
        /// <param name="multiplier3x6">The multiplier M3X6.</param>
        /// <param name="multiplier4x1">The multiplier M4X1.</param>
        /// <param name="multiplier4x2">The multiplier M4X2.</param>
        /// <param name="multiplier4x3">The multiplier M4X3.</param>
        /// <param name="multiplier4x4">The multiplier M4X4.</param>
        /// <param name="multiplier4x5">The multiplier M4X5.</param>
        /// <param name="multiplier4x6">The multiplier M4X6.</param>
        /// <param name="multiplier5x1">The multiplier M5X1.</param>
        /// <param name="multiplier5x2">The multiplier M5X2.</param>
        /// <param name="multiplier5x3">The multiplier M5X3.</param>
        /// <param name="multiplier5x4">The multiplier M5X4.</param>
        /// <param name="multiplier5x5">The multiplier M5X5.</param>
        /// <param name="multiplier5x6">The multiplier M5X6.</param>
        /// <param name="multiplier6x1">The multiplier M6X1.</param>
        /// <param name="multiplier6x2">The multiplier M6X2.</param>
        /// <param name="multiplier6x3">The multiplier M6X3.</param>
        /// <param name="multiplier6x4">The multiplier M6X4.</param>
        /// <param name="multiplier6x5">The multiplier M6X5.</param>
        /// <param name="multiplier6x6">The multiplier M6X6.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M1x1, double M1x2, double M1x3, double M1x4, double M1x5, double M1x6,
            double M2x1, double M2x2, double M2x3, double M2x4, double M2x5, double M2x6,
            double M3x1, double M3x2, double M3x3, double M3x4, double M3x5, double M3x6,
            double M4x1, double M4x2, double M4x3, double M4x4, double M4x5, double M4x6,
            double M5x1, double M5x2, double M5x3, double M5x4, double M5x5, double M5x6,
            double M6x1, double M6x2, double M6x3, double M6x4, double M6x5, double M6x6)
            Multiply3x3x6x6(
            double multiplicand1x1, double multiplicand1x2, double multiplicand1x3,
            double multiplicand2x1, double multiplicand2x2, double multiplicand2x3,
            double multiplicand3x1, double multiplicand3x2, double multiplicand3x3,
            double multiplier1x1, double multiplier1x2, double multiplier1x3, double multiplier1x4, double multiplier1x5, double multiplier1x6,
            double multiplier2x1, double multiplier2x2, double multiplier2x3, double multiplier2x4, double multiplier2x5, double multiplier2x6,
            double multiplier3x1, double multiplier3x2, double multiplier3x3, double multiplier3x4, double multiplier3x5, double multiplier3x6,
            double multiplier4x1, double multiplier4x2, double multiplier4x3, double multiplier4x4, double multiplier4x5, double multiplier4x6,
            double multiplier5x1, double multiplier5x2, double multiplier5x3, double multiplier5x4, double multiplier5x5, double multiplier5x6,
            double multiplier6x1, double multiplier6x2, double multiplier6x3, double multiplier6x4, double multiplier6x5, double multiplier6x6)
            => (
                (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1) + (multiplicand1x3 * multiplier3x1) + multiplier4x1 + multiplier5x1 + multiplier6x1,
                (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2) + (multiplicand1x3 * multiplier3x2) + multiplier4x2 + multiplier5x2 + multiplier6x2,
                (multiplicand1x1 * multiplier1x3) + (multiplicand1x2 * multiplier2x3) + (multiplicand1x3 * multiplier3x3) + multiplier4x3 + multiplier5x3 + multiplier6x3,
                (multiplicand1x1 * multiplier1x4) + (multiplicand1x2 * multiplier2x4) + (multiplicand1x3 * multiplier3x4) + multiplier4x4 + multiplier5x4 + multiplier6x4,
                (multiplicand1x1 * multiplier1x5) + (multiplicand1x2 * multiplier2x5) + (multiplicand1x3 * multiplier3x5) + multiplier4x5 + multiplier5x5 + multiplier6x5,
                (multiplicand1x1 * multiplier1x6) + (multiplicand1x2 * multiplier2x6) + (multiplicand1x3 * multiplier3x6) + multiplier4x6 + multiplier5x6 + multiplier6x6,
                (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1) + (multiplicand2x3 * multiplier3x1) + multiplier4x1 + multiplier5x1 + multiplier6x1,
                (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2) + (multiplicand2x3 * multiplier3x2) + multiplier4x2 + multiplier5x2 + multiplier6x2,
                (multiplicand2x1 * multiplier1x3) + (multiplicand2x2 * multiplier2x3) + (multiplicand2x3 * multiplier3x3) + multiplier4x3 + multiplier5x3 + multiplier6x3,
                (multiplicand2x1 * multiplier1x4) + (multiplicand2x2 * multiplier2x4) + (multiplicand2x3 * multiplier3x4) + multiplier4x4 + multiplier5x4 + multiplier6x4,
                (multiplicand2x1 * multiplier1x5) + (multiplicand2x2 * multiplier2x5) + (multiplicand2x3 * multiplier3x5) + multiplier4x5 + multiplier5x5 + multiplier6x5,
                (multiplicand2x1 * multiplier1x6) + (multiplicand2x2 * multiplier2x6) + (multiplicand2x3 * multiplier3x6) + multiplier4x6 + multiplier5x6 + multiplier6x6,
                (multiplicand3x1 * multiplier1x1) + (multiplicand3x2 * multiplier2x1) + (multiplicand3x3 * multiplier3x1) + multiplier4x1 + multiplier5x1 + multiplier6x1,
                (multiplicand3x1 * multiplier1x2) + (multiplicand3x2 * multiplier2x2) + (multiplicand3x3 * multiplier3x2) + multiplier4x2 + multiplier5x2 + multiplier6x2,
                (multiplicand3x1 * multiplier1x3) + (multiplicand3x2 * multiplier2x3) + (multiplicand3x3 * multiplier3x3) + multiplier4x3 + multiplier5x3 + multiplier6x3,
                (multiplicand3x1 * multiplier1x4) + (multiplicand3x2 * multiplier2x4) + (multiplicand3x3 * multiplier3x4) + multiplier4x4 + multiplier5x4 + multiplier6x4,
                (multiplicand3x1 * multiplier1x5) + (multiplicand3x2 * multiplier2x5) + (multiplicand3x3 * multiplier3x5) + multiplier4x5 + multiplier5x5 + multiplier6x5,
                (multiplicand3x1 * multiplier1x6) + (multiplicand3x2 * multiplier2x6) + (multiplicand3x3 * multiplier3x6) + multiplier4x6 + multiplier5x6 + multiplier6x6,
                multiplier1x1 + multiplier2x1 + multiplier3x1 + multiplier4x1 + multiplier5x1 + multiplier6x1,
                multiplier1x2 + multiplier2x2 + multiplier3x2 + multiplier4x2 + multiplier5x2 + multiplier6x2,
                multiplier1x3 + multiplier2x3 + multiplier3x3 + multiplier4x3 + multiplier5x3 + multiplier6x3,
                multiplier1x4 + multiplier2x4 + multiplier3x4 + multiplier4x4 + multiplier5x4 + multiplier6x4,
                multiplier1x5 + multiplier2x5 + multiplier3x5 + multiplier4x5 + multiplier5x5 + multiplier6x5,
                multiplier1x6 + multiplier2x6 + multiplier3x6 + multiplier4x6 + multiplier5x6 + multiplier6x6,
                multiplier1x1 + multiplier2x1 + multiplier3x1 + multiplier4x1 + multiplier5x1 + multiplier6x1,
                multiplier1x2 + multiplier2x2 + multiplier3x2 + multiplier4x2 + multiplier5x2 + multiplier6x2,
                multiplier1x3 + multiplier2x3 + multiplier3x3 + multiplier4x3 + multiplier5x3 + multiplier6x3,
                multiplier1x4 + multiplier2x4 + multiplier3x4 + multiplier4x4 + multiplier5x4 + multiplier6x4,
                multiplier1x5 + multiplier2x5 + multiplier3x5 + multiplier4x5 + multiplier5x5 + multiplier6x5,
                multiplier1x6 + multiplier2x6 + multiplier3x6 + multiplier4x6 + multiplier5x6 + multiplier6x6,
                multiplier1x1 + multiplier2x1 + multiplier3x1 + multiplier4x1 + multiplier5x1 + multiplier6x1,
                multiplier1x2 + multiplier2x2 + multiplier3x2 + multiplier4x2 + multiplier5x2 + multiplier6x2,
                multiplier1x3 + multiplier2x3 + multiplier3x3 + multiplier4x3 + multiplier5x3 + multiplier6x3,
                multiplier1x4 + multiplier2x4 + multiplier3x4 + multiplier4x4 + multiplier5x4 + multiplier6x4,
                multiplier1x5 + multiplier2x5 + multiplier3x5 + multiplier4x5 + multiplier5x5 + multiplier6x5,
                multiplier1x6 + multiplier2x6 + multiplier3x6 + multiplier4x6 + multiplier5x6 + multiplier6x6
            );

        /// <summary>
        /// Multiply6x6x3x3s the specified multiplicand M1X1.
        /// </summary>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicand1x5">The multiplicand M1X5.</param>
        /// <param name="multiplicand1x6">The multiplicand M1X6.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicand2x5">The multiplicand M2X5.</param>
        /// <param name="multiplicand2x6">The multiplicand M2X6.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicand3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicand3x5">The multiplicand M3X5.</param>
        /// <param name="multiplicand3x6">The multiplicand M3X6.</param>
        /// <param name="multiplicand4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicand4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicand4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicand4x4">The multiplicand M4X4.</param>
        /// <param name="multiplicand4x5">The multiplicand M4X5.</param>
        /// <param name="multiplicand4x6">The multiplicand M4X6.</param>
        /// <param name="multiplicand5x1">The multiplicand M5X1.</param>
        /// <param name="multiplicand5x2">The multiplicand M5X2.</param>
        /// <param name="multiplicand5x3">The multiplicand M5X3.</param>
        /// <param name="multiplicand5x4">The multiplicand M5X4.</param>
        /// <param name="multiplicand5x5">The multiplicand M5X5.</param>
        /// <param name="multiplicand5x6">The multiplicand M5X6.</param>
        /// <param name="multiplicand6x1">The multiplicand M6X1.</param>
        /// <param name="multiplicand6x2">The multiplicand M6X2.</param>
        /// <param name="multiplicand6x3">The multiplicand M6X3.</param>
        /// <param name="multiplicand6x4">The multiplicand M6X4.</param>
        /// <param name="multiplicand6x5">The multiplicand M6X5.</param>
        /// <param name="multiplicand6x6">The multiplicand M6X6.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M1x1, double M1x2, double M1x3, double M1x4, double M1x5, double M1x6,
            double M2x1, double M2x2, double M2x3, double M2x4, double M2x5, double M2x6,
            double M3x1, double M3x2, double M3x3, double M3x4, double M3x5, double M3x6,
            double M4x1, double M4x2, double M4x3, double M4x4, double M4x5, double M4x6,
            double M5x1, double M5x2, double M5x3, double M5x4, double M5x5, double M5x6,
            double M6x1, double M6x2, double M6x3, double M6x4, double M6x5, double M6x6)
            Multiply6x6x3x3(
            double multiplicand1x1, double multiplicand1x2, double multiplicand1x3, double multiplicand1x4, double multiplicand1x5, double multiplicand1x6,
            double multiplicand2x1, double multiplicand2x2, double multiplicand2x3, double multiplicand2x4, double multiplicand2x5, double multiplicand2x6,
            double multiplicand3x1, double multiplicand3x2, double multiplicand3x3, double multiplicand3x4, double multiplicand3x5, double multiplicand3x6,
            double multiplicand4x1, double multiplicand4x2, double multiplicand4x3, double multiplicand4x4, double multiplicand4x5, double multiplicand4x6,
            double multiplicand5x1, double multiplicand5x2, double multiplicand5x3, double multiplicand5x4, double multiplicand5x5, double multiplicand5x6,
            double multiplicand6x1, double multiplicand6x2, double multiplicand6x3, double multiplicand6x4, double multiplicand6x5, double multiplicand6x6,
            double multiplier1x1, double multiplier1x2, double multiplier1x3,
            double multiplier2x1, double multiplier2x2, double multiplier2x3,
            double multiplier3x1, double multiplier3x2, double multiplier3x3)
            => (
                (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1) + (multiplicand1x3 * multiplier3x1) + multiplicand1x4 + multiplicand1x5 + multiplicand1x6,
                (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2) + (multiplicand1x3 * multiplier3x2) + multiplicand1x4 + multiplicand1x5 + multiplicand1x6,
                (multiplicand1x1 * multiplier1x3) + (multiplicand1x2 * multiplier2x3) + (multiplicand1x3 * multiplier3x3) + multiplicand1x4 + multiplicand1x5 + multiplicand1x6,
                multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5 + multiplicand1x6,
                multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5 + multiplicand1x6,
                multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5 + multiplicand1x6,
                (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1) + (multiplicand2x3 * multiplier3x1) + multiplicand2x4 + multiplicand2x5 + multiplicand2x6,
                (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2) + (multiplicand2x3 * multiplier3x2) + multiplicand2x4 + multiplicand2x5 + multiplicand2x6,
                (multiplicand2x1 * multiplier1x3) + (multiplicand2x2 * multiplier2x3) + (multiplicand2x3 * multiplier3x3) + multiplicand2x4 + multiplicand2x5 + multiplicand2x6,
                multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5 + multiplicand2x6,
                multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5 + multiplicand2x6,
                multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5 + multiplicand2x6,
                (multiplicand3x1 * multiplier1x1) + (multiplicand3x2 * multiplier2x1) + (multiplicand3x3 * multiplier3x1) + multiplicand3x4 + multiplicand3x5 + multiplicand3x6,
                (multiplicand3x1 * multiplier1x2) + (multiplicand3x2 * multiplier2x2) + (multiplicand3x3 * multiplier3x2) + multiplicand3x4 + multiplicand3x5 + multiplicand3x6,
                (multiplicand3x1 * multiplier1x3) + (multiplicand3x2 * multiplier2x3) + (multiplicand3x3 * multiplier3x3) + multiplicand3x4 + multiplicand3x5 + multiplicand3x6,
                multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5 + multiplicand3x6,
                multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5 + multiplicand3x6,
                multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5 + multiplicand3x6,
                (multiplicand4x1 * multiplier1x1) + (multiplicand4x2 * multiplier2x1) + (multiplicand4x3 * multiplier3x1) + multiplicand4x4 + multiplicand4x5 + multiplicand4x6,
                (multiplicand4x1 * multiplier1x2) + (multiplicand4x2 * multiplier2x2) + (multiplicand4x3 * multiplier3x2) + multiplicand4x4 + multiplicand4x5 + multiplicand4x6,
                (multiplicand4x1 * multiplier1x3) + (multiplicand4x2 * multiplier2x3) + (multiplicand4x3 * multiplier3x3) + multiplicand4x4 + multiplicand4x5 + multiplicand4x6,
                multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5 + multiplicand4x6,
                multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5 + multiplicand4x6,
                multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5 + multiplicand4x6,
                (multiplicand5x1 * multiplier1x1) + (multiplicand5x2 * multiplier2x1) + (multiplicand5x3 * multiplier3x1) + multiplicand5x4 + multiplicand5x5 + multiplicand5x6,
                (multiplicand5x1 * multiplier1x2) + (multiplicand5x2 * multiplier2x2) + (multiplicand5x3 * multiplier3x2) + multiplicand5x4 + multiplicand5x5 + multiplicand5x6,
                (multiplicand5x1 * multiplier1x3) + (multiplicand5x2 * multiplier2x3) + (multiplicand5x3 * multiplier3x3) + multiplicand5x4 + multiplicand5x5 + multiplicand5x6,
                multiplicand5x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5 + multiplicand5x6,
                multiplicand5x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5 + multiplicand5x6,
                multiplicand5x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5 + multiplicand5x6,
                (multiplicand6x1 * multiplier1x1) + (multiplicand6x2 * multiplier2x1) + (multiplicand6x3 * multiplier3x1) + multiplicand6x4 + multiplicand6x5 + multiplicand6x6,
                (multiplicand6x1 * multiplier1x2) + (multiplicand6x2 * multiplier2x2) + (multiplicand6x3 * multiplier3x2) + multiplicand6x4 + multiplicand6x5 + multiplicand6x6,
                (multiplicand6x1 * multiplier1x3) + (multiplicand6x2 * multiplier2x3) + (multiplicand6x3 * multiplier3x3) + multiplicand6x4 + multiplicand6x5 + multiplicand6x6,
                multiplicand6x1 + multiplicand6x2 + multiplicand6x3 + multiplicand6x4 + multiplicand6x5 + multiplicand6x6,
                multiplicand6x1 + multiplicand6x2 + multiplicand6x3 + multiplicand6x4 + multiplicand6x5 + multiplicand6x6,
                multiplicand6x1 + multiplicand6x2 + multiplicand6x3 + multiplicand6x4 + multiplicand6x5 + multiplicand6x6
            );

        /// <summary>
        /// Multiply4x4x6x6s the specified multiplicand M1X1.
        /// </summary>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicand3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicand4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicand4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicand4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicand4x4">The multiplicand M4X4.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier1x4">The multiplier M1X4.</param>
        /// <param name="multiplier1x5">The multiplier M1X5.</param>
        /// <param name="multiplier1x6">The multiplier M1X6.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier2x4">The multiplier M2X4.</param>
        /// <param name="multiplier2x5">The multiplier M2X5.</param>
        /// <param name="multiplier2x6">The multiplier M2X6.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <param name="multiplier3x4">The multiplier M3X4.</param>
        /// <param name="multiplier3x5">The multiplier M3X5.</param>
        /// <param name="multiplier3x6">The multiplier M3X6.</param>
        /// <param name="multiplier4x1">The multiplier M4X1.</param>
        /// <param name="multiplier4x2">The multiplier M4X2.</param>
        /// <param name="multiplier4x3">The multiplier M4X3.</param>
        /// <param name="multiplier4x4">The multiplier M4X4.</param>
        /// <param name="multiplier4x5">The multiplier M4X5.</param>
        /// <param name="multiplier4x6">The multiplier M4X6.</param>
        /// <param name="multiplier5x1">The multiplier M5X1.</param>
        /// <param name="multiplier5x2">The multiplier M5X2.</param>
        /// <param name="multiplier5x3">The multiplier M5X3.</param>
        /// <param name="multiplier5x4">The multiplier M5X4.</param>
        /// <param name="multiplier5x5">The multiplier M5X5.</param>
        /// <param name="multiplier5x6">The multiplier M5X6.</param>
        /// <param name="multiplier6x1">The multiplier M6X1.</param>
        /// <param name="multiplier6x2">The multiplier M6X2.</param>
        /// <param name="multiplier6x3">The multiplier M6X3.</param>
        /// <param name="multiplier6x4">The multiplier M6X4.</param>
        /// <param name="multiplier6x5">The multiplier M6X5.</param>
        /// <param name="multiplier6x6">The multiplier M6X6.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M1x1, double M1x2, double M1x3, double M1x4, double M1x5, double M1x6,
            double M2x1, double M2x2, double M2x3, double M2x4, double M2x5, double M2x6,
            double M3x1, double M3x2, double M3x3, double M3x4, double M3x5, double M3x6,
            double M4x1, double M4x2, double M4x3, double M4x4, double M4x5, double M4x6,
            double M5x1, double M5x2, double M5x3, double M5x4, double M5x5, double M5x6,
            double M6x1, double M6x2, double M6x3, double M6x4, double M6x5, double M6x6)
            Multiply4x4x6x6(
            double multiplicand1x1, double multiplicand1x2, double multiplicand1x3, double multiplicand1x4,
            double multiplicand2x1, double multiplicand2x2, double multiplicand2x3, double multiplicand2x4,
            double multiplicand3x1, double multiplicand3x2, double multiplicand3x3, double multiplicand3x4,
            double multiplicand4x1, double multiplicand4x2, double multiplicand4x3, double multiplicand4x4,
            double multiplier1x1, double multiplier1x2, double multiplier1x3, double multiplier1x4, double multiplier1x5, double multiplier1x6,
            double multiplier2x1, double multiplier2x2, double multiplier2x3, double multiplier2x4, double multiplier2x5, double multiplier2x6,
            double multiplier3x1, double multiplier3x2, double multiplier3x3, double multiplier3x4, double multiplier3x5, double multiplier3x6,
            double multiplier4x1, double multiplier4x2, double multiplier4x3, double multiplier4x4, double multiplier4x5, double multiplier4x6,
            double multiplier5x1, double multiplier5x2, double multiplier5x3, double multiplier5x4, double multiplier5x5, double multiplier5x6,
            double multiplier6x1, double multiplier6x2, double multiplier6x3, double multiplier6x4, double multiplier6x5, double multiplier6x6)
            => (
                (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1) + (multiplicand1x3 * multiplier3x1) + (multiplicand1x4 * multiplier4x1) + multiplier5x1 + multiplier6x1,
                (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2) + (multiplicand1x3 * multiplier3x2) + (multiplicand1x4 * multiplier4x2) + multiplier5x2 + multiplier6x2,
                (multiplicand1x1 * multiplier1x3) + (multiplicand1x2 * multiplier2x3) + (multiplicand1x3 * multiplier3x3) + (multiplicand1x4 * multiplier4x3) + multiplier5x3 + multiplier6x3,
                (multiplicand1x1 * multiplier1x4) + (multiplicand1x2 * multiplier2x4) + (multiplicand1x3 * multiplier3x4) + (multiplicand1x4 * multiplier4x4) + multiplier5x4 + multiplier6x4,
                (multiplicand1x1 * multiplier1x5) + (multiplicand1x2 * multiplier2x5) + (multiplicand1x3 * multiplier3x5) + (multiplicand1x4 * multiplier4x5) + multiplier5x5 + multiplier6x5,
                (multiplicand1x1 * multiplier1x6) + (multiplicand1x2 * multiplier2x6) + (multiplicand1x3 * multiplier3x6) + (multiplicand1x4 * multiplier4x6) + multiplier5x6 + multiplier6x6,
                (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1) + (multiplicand2x3 * multiplier3x1) + (multiplicand2x4 * multiplier4x1) + multiplier5x1 + multiplier6x1,
                (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2) + (multiplicand2x3 * multiplier3x2) + (multiplicand2x4 * multiplier4x2) + multiplier5x2 + multiplier6x2,
                (multiplicand2x1 * multiplier1x3) + (multiplicand2x2 * multiplier2x3) + (multiplicand2x3 * multiplier3x3) + (multiplicand2x4 * multiplier4x3) + multiplier5x3 + multiplier6x3,
                (multiplicand2x1 * multiplier1x4) + (multiplicand2x2 * multiplier2x4) + (multiplicand2x3 * multiplier3x4) + (multiplicand2x4 * multiplier4x4) + multiplier5x4 + multiplier6x4,
                (multiplicand2x1 * multiplier1x5) + (multiplicand2x2 * multiplier2x5) + (multiplicand2x3 * multiplier3x5) + (multiplicand2x4 * multiplier4x5) + multiplier5x5 + multiplier6x5,
                (multiplicand2x1 * multiplier1x6) + (multiplicand2x2 * multiplier2x6) + (multiplicand2x3 * multiplier3x6) + (multiplicand2x4 * multiplier4x6) + multiplier5x6 + multiplier6x6,
                (multiplicand3x1 * multiplier1x1) + (multiplicand3x2 * multiplier2x1) + (multiplicand3x3 * multiplier3x1) + (multiplicand3x4 * multiplier4x1) + multiplier5x1 + multiplier6x1,
                (multiplicand3x1 * multiplier1x2) + (multiplicand3x2 * multiplier2x2) + (multiplicand3x3 * multiplier3x2) + (multiplicand3x4 * multiplier4x2) + multiplier5x2 + multiplier6x2,
                (multiplicand3x1 * multiplier1x3) + (multiplicand3x2 * multiplier2x3) + (multiplicand3x3 * multiplier3x3) + (multiplicand3x4 * multiplier4x3) + multiplier5x3 + multiplier6x3,
                (multiplicand3x1 * multiplier1x4) + (multiplicand3x2 * multiplier2x4) + (multiplicand3x3 * multiplier3x4) + (multiplicand3x4 * multiplier4x4) + multiplier5x4 + multiplier6x4,
                (multiplicand3x1 * multiplier1x5) + (multiplicand3x2 * multiplier2x5) + (multiplicand3x3 * multiplier3x5) + (multiplicand3x4 * multiplier4x5) + multiplier5x5 + multiplier6x5,
                (multiplicand3x1 * multiplier1x6) + (multiplicand3x2 * multiplier2x6) + (multiplicand3x3 * multiplier3x6) + (multiplicand3x4 * multiplier4x6) + multiplier5x6 + multiplier6x6,
                (multiplicand4x1 * multiplier1x1) + (multiplicand4x2 * multiplier2x1) + (multiplicand4x3 * multiplier3x1) + (multiplicand4x4 * multiplier4x1) + multiplier5x1 + multiplier6x1,
                (multiplicand4x1 * multiplier1x2) + (multiplicand4x2 * multiplier2x2) + (multiplicand4x3 * multiplier3x2) + (multiplicand4x4 * multiplier4x2) + multiplier5x2 + multiplier6x2,
                (multiplicand4x1 * multiplier1x3) + (multiplicand4x2 * multiplier2x3) + (multiplicand4x3 * multiplier3x3) + (multiplicand4x4 * multiplier4x3) + multiplier5x3 + multiplier6x3,
                (multiplicand4x1 * multiplier1x4) + (multiplicand4x2 * multiplier2x4) + (multiplicand4x3 * multiplier3x4) + (multiplicand4x4 * multiplier4x4) + multiplier5x4 + multiplier6x4,
                (multiplicand4x1 * multiplier1x5) + (multiplicand4x2 * multiplier2x5) + (multiplicand4x3 * multiplier3x5) + (multiplicand4x4 * multiplier4x5) + multiplier5x5 + multiplier6x5,
                (multiplicand4x1 * multiplier1x6) + (multiplicand4x2 * multiplier2x6) + (multiplicand4x3 * multiplier3x6) + (multiplicand4x4 * multiplier4x6) + multiplier5x6 + multiplier6x6,
                multiplier1x1 + multiplier2x1 + multiplier3x1 + multiplier4x1 + multiplier5x1 + multiplier6x1,
                multiplier1x2 + multiplier2x2 + multiplier3x2 + multiplier4x2 + multiplier5x2 + multiplier6x2,
                multiplier1x3 + multiplier2x3 + multiplier3x3 + multiplier4x3 + multiplier5x3 + multiplier6x3,
                multiplier1x4 + multiplier2x4 + multiplier3x4 + multiplier4x4 + multiplier5x4 + multiplier6x4,
                multiplier1x5 + multiplier2x5 + multiplier3x5 + multiplier4x5 + multiplier5x5 + multiplier6x5,
                multiplier1x6 + multiplier2x6 + multiplier3x6 + multiplier4x6 + multiplier5x6 + multiplier6x6,
                multiplier1x1 + multiplier2x1 + multiplier3x1 + multiplier4x1 + multiplier5x1 + multiplier6x1,
                multiplier1x2 + multiplier2x2 + multiplier3x2 + multiplier4x2 + multiplier5x2 + multiplier6x2,
                multiplier1x3 + multiplier2x3 + multiplier3x3 + multiplier4x3 + multiplier5x3 + multiplier6x3,
                multiplier1x4 + multiplier2x4 + multiplier3x4 + multiplier4x4 + multiplier5x4 + multiplier6x4,
                multiplier1x5 + multiplier2x5 + multiplier3x5 + multiplier4x5 + multiplier5x5 + multiplier6x5,
                multiplier1x6 + multiplier2x6 + multiplier3x6 + multiplier4x6 + multiplier5x6 + multiplier6x6
            );

        /// <summary>
        /// Multiply6x6x4x4s the specified multiplicand M1X1.
        /// </summary>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicand1x5">The multiplicand M1X5.</param>
        /// <param name="multiplicand1x6">The multiplicand M1X6.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicand2x5">The multiplicand M2X5.</param>
        /// <param name="multiplicand2x6">The multiplicand M2X6.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicand3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicand3x5">The multiplicand M3X5.</param>
        /// <param name="multiplicand3x6">The multiplicand M3X6.</param>
        /// <param name="multiplicand4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicand4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicand4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicand4x4">The multiplicand M4X4.</param>
        /// <param name="multiplicand4x5">The multiplicand M4X5.</param>
        /// <param name="multiplicand4x6">The multiplicand M4X6.</param>
        /// <param name="multiplicand5x1">The multiplicand M5X1.</param>
        /// <param name="multiplicand5x2">The multiplicand M5X2.</param>
        /// <param name="multiplicand5x3">The multiplicand M5X3.</param>
        /// <param name="multiplicand5x4">The multiplicand M5X4.</param>
        /// <param name="multiplicand5x5">The multiplicand M5X5.</param>
        /// <param name="multiplicand5x6">The multiplicand M5X6.</param>
        /// <param name="multiplicand6x1">The multiplicand M6X1.</param>
        /// <param name="multiplicand6x2">The multiplicand M6X2.</param>
        /// <param name="multiplicand6x3">The multiplicand M6X3.</param>
        /// <param name="multiplicand6x4">The multiplicand M6X4.</param>
        /// <param name="multiplicand6x5">The multiplicand M6X5.</param>
        /// <param name="multiplicand6x6">The multiplicand M6X6.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier1x4">The multiplier M1X4.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier2x4">The multiplier M2X4.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <param name="multiplier3x4">The multiplier M3X4.</param>
        /// <param name="multiplier4x1">The multiplier M4X1.</param>
        /// <param name="multiplier4x2">The multiplier M4X2.</param>
        /// <param name="multiplier4x3">The multiplier M4X3.</param>
        /// <param name="multiplier4x4">The multiplier M4X4.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M1x1, double M1x2, double M1x3, double M1x4, double M1x5, double M1x6,
            double M2x1, double M2x2, double M2x3, double M2x4, double M2x5, double M2x6,
            double M3x1, double M3x2, double M3x3, double M3x4, double M3x5, double M3x6,
            double M4x1, double M4x2, double M4x3, double M4x4, double M4x5, double M4x6,
            double M5x1, double M5x2, double M5x3, double M5x4, double M5x5, double M5x6,
            double M6x1, double M6x2, double M6x3, double M6x4, double M6x5, double M6x6)
            Multiply6x6x4x4(
            double multiplicand1x1, double multiplicand1x2, double multiplicand1x3, double multiplicand1x4, double multiplicand1x5, double multiplicand1x6,
            double multiplicand2x1, double multiplicand2x2, double multiplicand2x3, double multiplicand2x4, double multiplicand2x5, double multiplicand2x6,
            double multiplicand3x1, double multiplicand3x2, double multiplicand3x3, double multiplicand3x4, double multiplicand3x5, double multiplicand3x6,
            double multiplicand4x1, double multiplicand4x2, double multiplicand4x3, double multiplicand4x4, double multiplicand4x5, double multiplicand4x6,
            double multiplicand5x1, double multiplicand5x2, double multiplicand5x3, double multiplicand5x4, double multiplicand5x5, double multiplicand5x6,
            double multiplicand6x1, double multiplicand6x2, double multiplicand6x3, double multiplicand6x4, double multiplicand6x5, double multiplicand6x6,
            double multiplier1x1, double multiplier1x2, double multiplier1x3, double multiplier1x4,
            double multiplier2x1, double multiplier2x2, double multiplier2x3, double multiplier2x4,
            double multiplier3x1, double multiplier3x2, double multiplier3x3, double multiplier3x4,
            double multiplier4x1, double multiplier4x2, double multiplier4x3, double multiplier4x4)
            => (
                (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1) + (multiplicand1x3 * multiplier3x1) + (multiplicand1x4 * multiplier4x1) + multiplicand1x5 + multiplicand1x6,
                (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2) + (multiplicand1x3 * multiplier3x2) + (multiplicand1x4 * multiplier4x2) + multiplicand1x5 + multiplicand1x6,
                (multiplicand1x1 * multiplier1x3) + (multiplicand1x2 * multiplier2x3) + (multiplicand1x3 * multiplier3x3) + (multiplicand1x4 * multiplier4x3) + multiplicand1x5 + multiplicand1x6,
                (multiplicand1x1 * multiplier1x4) + (multiplicand1x2 * multiplier2x4) + (multiplicand1x3 * multiplier3x4) + (multiplicand1x4 * multiplier4x4) + multiplicand1x5 + multiplicand1x6,
                multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5 + multiplicand1x6,
                multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5 + multiplicand1x6,
                (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1) + (multiplicand2x3 * multiplier3x1) + (multiplicand2x4 * multiplier4x1) + multiplicand2x5 + multiplicand2x6,
                (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2) + (multiplicand2x3 * multiplier3x2) + (multiplicand2x4 * multiplier4x2) + multiplicand2x5 + multiplicand2x6,
                (multiplicand2x1 * multiplier1x3) + (multiplicand2x2 * multiplier2x3) + (multiplicand2x3 * multiplier3x3) + (multiplicand2x4 * multiplier4x3) + multiplicand2x5 + multiplicand2x6,
                (multiplicand2x1 * multiplier1x4) + (multiplicand2x2 * multiplier2x4) + (multiplicand2x3 * multiplier3x4) + (multiplicand2x4 * multiplier4x4) + multiplicand2x5 + multiplicand2x6,
                multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5 + multiplicand2x6,
                multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5 + multiplicand2x6,
                (multiplicand3x1 * multiplier1x1) + (multiplicand3x2 * multiplier2x1) + (multiplicand3x3 * multiplier3x1) + (multiplicand3x4 * multiplier4x1) + multiplicand3x5 + multiplicand3x6,
                (multiplicand3x1 * multiplier1x2) + (multiplicand3x2 * multiplier2x2) + (multiplicand3x3 * multiplier3x2) + (multiplicand3x4 * multiplier4x2) + multiplicand3x5 + multiplicand3x6,
                (multiplicand3x1 * multiplier1x3) + (multiplicand3x2 * multiplier2x3) + (multiplicand3x3 * multiplier3x3) + (multiplicand3x4 * multiplier4x3) + multiplicand3x5 + multiplicand3x6,
                (multiplicand3x1 * multiplier1x4) + (multiplicand3x2 * multiplier2x4) + (multiplicand3x3 * multiplier3x4) + (multiplicand3x4 * multiplier4x4) + multiplicand3x5 + multiplicand3x6,
                multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5 + multiplicand3x6,
                multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5 + multiplicand3x6,
                (multiplicand4x1 * multiplier1x1) + (multiplicand4x2 * multiplier2x1) + (multiplicand4x3 * multiplier3x1) + (multiplicand4x4 * multiplier4x1) + multiplicand4x5 + multiplicand4x6,
                (multiplicand4x1 * multiplier1x2) + (multiplicand4x2 * multiplier2x2) + (multiplicand4x3 * multiplier3x2) + (multiplicand4x4 * multiplier4x2) + multiplicand4x5 + multiplicand4x6,
                (multiplicand4x1 * multiplier1x3) + (multiplicand4x2 * multiplier2x3) + (multiplicand4x3 * multiplier3x3) + (multiplicand4x4 * multiplier4x3) + multiplicand4x5 + multiplicand4x6,
                (multiplicand4x1 * multiplier1x4) + (multiplicand4x2 * multiplier2x4) + (multiplicand4x3 * multiplier3x4) + (multiplicand4x4 * multiplier4x4) + multiplicand4x5 + multiplicand4x6,
                multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5 + multiplicand4x6,
                multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5 + multiplicand4x6,
                (multiplicand5x1 * multiplier1x1) + (multiplicand5x2 * multiplier2x1) + (multiplicand5x3 * multiplier3x1) + (multiplicand5x4 * multiplier4x1) + multiplicand5x5 + multiplicand5x6,
                (multiplicand5x1 * multiplier1x2) + (multiplicand5x2 * multiplier2x2) + (multiplicand5x3 * multiplier3x2) + (multiplicand5x4 * multiplier4x2) + multiplicand5x5 + multiplicand5x6,
                (multiplicand5x1 * multiplier1x3) + (multiplicand5x2 * multiplier2x3) + (multiplicand5x3 * multiplier3x3) + (multiplicand5x4 * multiplier4x3) + multiplicand5x5 + multiplicand5x6,
                (multiplicand5x1 * multiplier1x4) + (multiplicand5x2 * multiplier2x4) + (multiplicand5x3 * multiplier3x4) + (multiplicand5x4 * multiplier4x4) + multiplicand5x5 + multiplicand5x6,
                multiplicand5x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5 + multiplicand5x6,
                multiplicand5x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5 + multiplicand5x6,
                (multiplicand6x1 * multiplier1x1) + (multiplicand6x2 * multiplier2x1) + (multiplicand6x3 * multiplier3x1) + (multiplicand6x4 * multiplier4x1) + multiplicand6x5 + multiplicand6x6,
                (multiplicand6x1 * multiplier1x2) + (multiplicand6x2 * multiplier2x2) + (multiplicand6x3 * multiplier3x2) + (multiplicand6x4 * multiplier4x2) + multiplicand6x5 + multiplicand6x6,
                (multiplicand6x1 * multiplier1x3) + (multiplicand6x2 * multiplier2x3) + (multiplicand6x3 * multiplier3x3) + (multiplicand6x4 * multiplier4x3) + multiplicand6x5 + multiplicand6x6,
                (multiplicand6x1 * multiplier1x4) + (multiplicand6x2 * multiplier2x4) + (multiplicand6x3 * multiplier3x4) + (multiplicand6x4 * multiplier4x4) + multiplicand6x5 + multiplicand6x6,
                multiplicand6x1 + multiplicand6x2 + multiplicand6x3 + multiplicand6x4 + multiplicand6x5 + multiplicand6x6,
                multiplicand6x1 + multiplicand6x2 + multiplicand6x3 + multiplicand6x4 + multiplicand6x5 + multiplicand6x6
            );

        /// <summary>
        /// Multiply5x5x6x6s the specified multiplicand M1X1.
        /// </summary>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicand1x5">The multiplicand M1X5.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicand2x5">The multiplicand M2X5.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicand3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicand3x5">The multiplicand M3X5.</param>
        /// <param name="multiplicand4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicand4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicand4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicand4x4">The multiplicand M4X4.</param>
        /// <param name="multiplicand4x5">The multiplicand M4X5.</param>
        /// <param name="multiplicand5x1">The multiplicand M5X1.</param>
        /// <param name="multiplicand5x2">The multiplicand M5X2.</param>
        /// <param name="multiplicand5x3">The multiplicand M5X3.</param>
        /// <param name="multiplicand5x4">The multiplicand M5X4.</param>
        /// <param name="multiplicand5x5">The multiplicand M5X5.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier1x4">The multiplier M1X4.</param>
        /// <param name="multiplier1x5">The multiplier M1X5.</param>
        /// <param name="multiplier1x6">The multiplier M1X6.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier2x4">The multiplier M2X4.</param>
        /// <param name="multiplier2x5">The multiplier M2X5.</param>
        /// <param name="multiplier2x6">The multiplier M2X6.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <param name="multiplier3x4">The multiplier M3X4.</param>
        /// <param name="multiplier3x5">The multiplier M3X5.</param>
        /// <param name="multiplier3x6">The multiplier M3X6.</param>
        /// <param name="multiplier4x1">The multiplier M4X1.</param>
        /// <param name="multiplier4x2">The multiplier M4X2.</param>
        /// <param name="multiplier4x3">The multiplier M4X3.</param>
        /// <param name="multiplier4x4">The multiplier M4X4.</param>
        /// <param name="multiplier4x5">The multiplier M4X5.</param>
        /// <param name="multiplier4x6">The multiplier M4X6.</param>
        /// <param name="multiplier5x1">The multiplier M5X1.</param>
        /// <param name="multiplier5x2">The multiplier M5X2.</param>
        /// <param name="multiplier5x3">The multiplier M5X3.</param>
        /// <param name="multiplier5x4">The multiplier M5X4.</param>
        /// <param name="multiplier5x5">The multiplier M5X5.</param>
        /// <param name="multiplier5x6">The multiplier M5X6.</param>
        /// <param name="multiplier6x1">The multiplier M6X1.</param>
        /// <param name="multiplier6x2">The multiplier M6X2.</param>
        /// <param name="multiplier6x3">The multiplier M6X3.</param>
        /// <param name="multiplier6x4">The multiplier M6X4.</param>
        /// <param name="multiplier6x5">The multiplier M6X5.</param>
        /// <param name="multiplier6x6">The multiplier M6X6.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M1x1, double M1x2, double M1x3, double M1x4, double M1x5, double M1x6,
            double M2x1, double M2x2, double M2x3, double M2x4, double M2x5, double M2x6,
            double M3x1, double M3x2, double M3x3, double M3x4, double M3x5, double M3x6,
            double M4x1, double M4x2, double M4x3, double M4x4, double M4x5, double M4x6,
            double M5x1, double M5x2, double M5x3, double M5x4, double M5x5, double M5x6,
            double M6x1, double M6x2, double M6x3, double M6x4, double M6x5, double M6x6)
            Multiply5x5x6x6(
            double multiplicand1x1, double multiplicand1x2, double multiplicand1x3, double multiplicand1x4, double multiplicand1x5,
            double multiplicand2x1, double multiplicand2x2, double multiplicand2x3, double multiplicand2x4, double multiplicand2x5,
            double multiplicand3x1, double multiplicand3x2, double multiplicand3x3, double multiplicand3x4, double multiplicand3x5,
            double multiplicand4x1, double multiplicand4x2, double multiplicand4x3, double multiplicand4x4, double multiplicand4x5,
            double multiplicand5x1, double multiplicand5x2, double multiplicand5x3, double multiplicand5x4, double multiplicand5x5,
            double multiplier1x1, double multiplier1x2, double multiplier1x3, double multiplier1x4, double multiplier1x5, double multiplier1x6,
            double multiplier2x1, double multiplier2x2, double multiplier2x3, double multiplier2x4, double multiplier2x5, double multiplier2x6,
            double multiplier3x1, double multiplier3x2, double multiplier3x3, double multiplier3x4, double multiplier3x5, double multiplier3x6,
            double multiplier4x1, double multiplier4x2, double multiplier4x3, double multiplier4x4, double multiplier4x5, double multiplier4x6,
            double multiplier5x1, double multiplier5x2, double multiplier5x3, double multiplier5x4, double multiplier5x5, double multiplier5x6,
            double multiplier6x1, double multiplier6x2, double multiplier6x3, double multiplier6x4, double multiplier6x5, double multiplier6x6)
            => (
                (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1) + (multiplicand1x3 * multiplier3x1) + (multiplicand1x4 * multiplier4x1) + (multiplicand1x5 * multiplier5x1) + multiplier6x1,
                (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2) + (multiplicand1x3 * multiplier3x2) + (multiplicand1x4 * multiplier4x2) + (multiplicand1x5 * multiplier5x2) + multiplier6x2,
                (multiplicand1x1 * multiplier1x3) + (multiplicand1x2 * multiplier2x3) + (multiplicand1x3 * multiplier3x3) + (multiplicand1x4 * multiplier4x3) + (multiplicand1x5 * multiplier5x3) + multiplier6x3,
                (multiplicand1x1 * multiplier1x4) + (multiplicand1x2 * multiplier2x4) + (multiplicand1x3 * multiplier3x4) + (multiplicand1x4 * multiplier4x4) + (multiplicand1x5 * multiplier5x4) + multiplier6x4,
                (multiplicand1x1 * multiplier1x5) + (multiplicand1x2 * multiplier2x5) + (multiplicand1x3 * multiplier3x5) + (multiplicand1x4 * multiplier4x5) + (multiplicand1x5 * multiplier5x5) + multiplier6x5,
                (multiplicand1x1 * multiplier1x6) + (multiplicand1x2 * multiplier2x6) + (multiplicand1x3 * multiplier3x6) + (multiplicand1x4 * multiplier4x6) + (multiplicand1x5 * multiplier5x6) + multiplier6x6,
                (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1) + (multiplicand2x3 * multiplier3x1) + (multiplicand2x4 * multiplier4x1) + (multiplicand2x5 * multiplier5x1) + multiplier6x1,
                (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2) + (multiplicand2x3 * multiplier3x2) + (multiplicand2x4 * multiplier4x2) + (multiplicand2x5 * multiplier5x2) + multiplier6x2,
                (multiplicand2x1 * multiplier1x3) + (multiplicand2x2 * multiplier2x3) + (multiplicand2x3 * multiplier3x3) + (multiplicand2x4 * multiplier4x3) + (multiplicand2x5 * multiplier5x3) + multiplier6x3,
                (multiplicand2x1 * multiplier1x4) + (multiplicand2x2 * multiplier2x4) + (multiplicand2x3 * multiplier3x4) + (multiplicand2x4 * multiplier4x4) + (multiplicand2x5 * multiplier5x4) + multiplier6x4,
                (multiplicand2x1 * multiplier1x5) + (multiplicand2x2 * multiplier2x5) + (multiplicand2x3 * multiplier3x5) + (multiplicand2x4 * multiplier4x5) + (multiplicand2x5 * multiplier5x5) + multiplier6x5,
                (multiplicand2x1 * multiplier1x6) + (multiplicand2x2 * multiplier2x6) + (multiplicand2x3 * multiplier3x6) + (multiplicand2x4 * multiplier4x6) + (multiplicand2x5 * multiplier5x6) + multiplier6x6,
                (multiplicand3x1 * multiplier1x1) + (multiplicand3x2 * multiplier2x1) + (multiplicand3x3 * multiplier3x1) + (multiplicand3x4 * multiplier4x1) + (multiplicand3x5 * multiplier5x1) + multiplier6x1,
                (multiplicand3x1 * multiplier1x2) + (multiplicand3x2 * multiplier2x2) + (multiplicand3x3 * multiplier3x2) + (multiplicand3x4 * multiplier4x2) + (multiplicand3x5 * multiplier5x2) + multiplier6x2,
                (multiplicand3x1 * multiplier1x3) + (multiplicand3x2 * multiplier2x3) + (multiplicand3x3 * multiplier3x3) + (multiplicand3x4 * multiplier4x3) + (multiplicand3x5 * multiplier5x3) + multiplier6x3,
                (multiplicand3x1 * multiplier1x4) + (multiplicand3x2 * multiplier2x4) + (multiplicand3x3 * multiplier3x4) + (multiplicand3x4 * multiplier4x4) + (multiplicand3x5 * multiplier5x4) + multiplier6x4,
                (multiplicand3x1 * multiplier1x5) + (multiplicand3x2 * multiplier2x5) + (multiplicand3x3 * multiplier3x5) + (multiplicand3x4 * multiplier4x5) + (multiplicand3x5 * multiplier5x5) + multiplier6x5,
                (multiplicand3x1 * multiplier1x6) + (multiplicand3x2 * multiplier2x6) + (multiplicand3x3 * multiplier3x6) + (multiplicand3x4 * multiplier4x6) + (multiplicand3x5 * multiplier5x6) + multiplier6x6,
                (multiplicand4x1 * multiplier1x1) + (multiplicand4x2 * multiplier2x1) + (multiplicand4x3 * multiplier3x1) + (multiplicand4x4 * multiplier4x1) + (multiplicand4x5 * multiplier5x1) + multiplier6x1,
                (multiplicand4x1 * multiplier1x2) + (multiplicand4x2 * multiplier2x2) + (multiplicand4x3 * multiplier3x2) + (multiplicand4x4 * multiplier4x2) + (multiplicand4x5 * multiplier5x2) + multiplier6x2,
                (multiplicand4x1 * multiplier1x3) + (multiplicand4x2 * multiplier2x3) + (multiplicand4x3 * multiplier3x3) + (multiplicand4x4 * multiplier4x3) + (multiplicand4x5 * multiplier5x3) + multiplier6x3,
                (multiplicand4x1 * multiplier1x4) + (multiplicand4x2 * multiplier2x4) + (multiplicand4x3 * multiplier3x4) + (multiplicand4x4 * multiplier4x4) + (multiplicand4x5 * multiplier5x4) + multiplier6x4,
                (multiplicand4x1 * multiplier1x5) + (multiplicand4x2 * multiplier2x5) + (multiplicand4x3 * multiplier3x5) + (multiplicand4x4 * multiplier4x5) + (multiplicand4x5 * multiplier5x5) + multiplier6x5,
                (multiplicand4x1 * multiplier1x6) + (multiplicand4x2 * multiplier2x6) + (multiplicand4x3 * multiplier3x6) + (multiplicand4x4 * multiplier4x6) + (multiplicand4x5 * multiplier5x6) + multiplier6x6,
                (multiplicand5x1 * multiplier1x1) + (multiplicand5x2 * multiplier2x1) + (multiplicand5x3 * multiplier3x1) + (multiplicand5x4 * multiplier4x1) + (multiplicand5x5 * multiplier5x1) + multiplier6x1,
                (multiplicand5x1 * multiplier1x2) + (multiplicand5x2 * multiplier2x2) + (multiplicand5x3 * multiplier3x2) + (multiplicand5x4 * multiplier4x2) + (multiplicand5x5 * multiplier5x2) + multiplier6x2,
                (multiplicand5x1 * multiplier1x3) + (multiplicand5x2 * multiplier2x3) + (multiplicand5x3 * multiplier3x3) + (multiplicand5x4 * multiplier4x3) + (multiplicand5x5 * multiplier5x3) + multiplier6x3,
                (multiplicand5x1 * multiplier1x4) + (multiplicand5x2 * multiplier2x4) + (multiplicand5x3 * multiplier3x4) + (multiplicand5x4 * multiplier4x4) + (multiplicand5x5 * multiplier5x4) + multiplier6x4,
                (multiplicand5x1 * multiplier1x5) + (multiplicand5x2 * multiplier2x5) + (multiplicand5x3 * multiplier3x5) + (multiplicand5x4 * multiplier4x5) + (multiplicand5x5 * multiplier5x5) + multiplier6x5,
                (multiplicand5x1 * multiplier1x6) + (multiplicand5x2 * multiplier2x6) + (multiplicand5x3 * multiplier3x6) + (multiplicand5x4 * multiplier4x6) + (multiplicand5x5 * multiplier5x6) + multiplier6x6,
                multiplier1x1 + multiplier2x1 + multiplier3x1 + multiplier4x1 + multiplier5x1 + multiplier6x1,
                multiplier1x2 + multiplier2x2 + multiplier3x2 + multiplier4x2 + multiplier5x2 + multiplier6x2,
                multiplier1x3 + multiplier2x3 + multiplier3x3 + multiplier4x3 + multiplier5x3 + multiplier6x3,
                multiplier1x4 + multiplier2x4 + multiplier3x4 + multiplier4x4 + multiplier5x4 + multiplier6x4,
                multiplier1x5 + multiplier2x5 + multiplier3x5 + multiplier4x5 + multiplier5x5 + multiplier6x5,
                multiplier1x6 + multiplier2x6 + multiplier3x6 + multiplier4x6 + multiplier5x6 + multiplier6x6
            );

        /// <summary>
        /// Multiply6x6x6x6s the specified multiplicand M1X1.
        /// </summary>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicand1x5">The multiplicand M1X5.</param>
        /// <param name="multiplicand1x6">The multiplicand M1X6.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicand2x5">The multiplicand M2X5.</param>
        /// <param name="multiplicand2x6">The multiplicand M2X6.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicand3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicand3x5">The multiplicand M3X5.</param>
        /// <param name="multiplicand3x6">The multiplicand M3X6.</param>
        /// <param name="multiplicand4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicand4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicand4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicand4x4">The multiplicand M4X4.</param>
        /// <param name="multiplicand4x5">The multiplicand M4X5.</param>
        /// <param name="multiplicand4x6">The multiplicand M4X6.</param>
        /// <param name="multiplicand5x1">The multiplicand M5X1.</param>
        /// <param name="multiplicand5x2">The multiplicand M5X2.</param>
        /// <param name="multiplicand5x3">The multiplicand M5X3.</param>
        /// <param name="multiplicand5x4">The multiplicand M5X4.</param>
        /// <param name="multiplicand5x5">The multiplicand M5X5.</param>
        /// <param name="multiplicand5x6">The multiplicand M5X6.</param>
        /// <param name="multiplicand6x1">The multiplicand M6X1.</param>
        /// <param name="multiplicand6x2">The multiplicand M6X2.</param>
        /// <param name="multiplicand6x3">The multiplicand M6X3.</param>
        /// <param name="multiplicand6x4">The multiplicand M6X4.</param>
        /// <param name="multiplicand6x5">The multiplicand M6X5.</param>
        /// <param name="multiplicand6x6">The multiplicand M6X6.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier1x4">The multiplier M1X4.</param>
        /// <param name="multiplier1x5">The multiplier M1X5.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier2x4">The multiplier M2X4.</param>
        /// <param name="multiplier2x5">The multiplier M2X5.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <param name="multiplier3x4">The multiplier M3X4.</param>
        /// <param name="multiplier3x5">The multiplier M3X5.</param>
        /// <param name="multiplier4x1">The multiplier M4X1.</param>
        /// <param name="multiplier4x2">The multiplier M4X2.</param>
        /// <param name="multiplier4x3">The multiplier M4X3.</param>
        /// <param name="multiplier4x4">The multiplier M4X4.</param>
        /// <param name="multiplier4x5">The multiplier M4X5.</param>
        /// <param name="multiplier5x1">The multiplier M5X1.</param>
        /// <param name="multiplier5x2">The multiplier M5X2.</param>
        /// <param name="multiplier5x3">The multiplier M5X3.</param>
        /// <param name="multiplier5x4">The multiplier M5X4.</param>
        /// <param name="multiplier5x5">The multiplier M5X5.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M1x1, double M1x2, double M1x3, double M1x4, double M1x5, double M1x6,
            double M2x1, double M2x2, double M2x3, double M2x4, double M2x5, double M2x6,
            double M3x1, double M3x2, double M3x3, double M3x4, double M3x5, double M3x6,
            double M4x1, double M4x2, double M4x3, double M4x4, double M4x5, double M4x6,
            double M5x1, double M5x2, double M5x3, double M5x4, double M5x5, double M5x6,
            double M6x1, double M6x2, double M6x3, double M6x4, double M6x5, double M6x6)
            Multiply6x6x5x5(
            double multiplicand1x1, double multiplicand1x2, double multiplicand1x3, double multiplicand1x4, double multiplicand1x5, double multiplicand1x6,
            double multiplicand2x1, double multiplicand2x2, double multiplicand2x3, double multiplicand2x4, double multiplicand2x5, double multiplicand2x6,
            double multiplicand3x1, double multiplicand3x2, double multiplicand3x3, double multiplicand3x4, double multiplicand3x5, double multiplicand3x6,
            double multiplicand4x1, double multiplicand4x2, double multiplicand4x3, double multiplicand4x4, double multiplicand4x5, double multiplicand4x6,
            double multiplicand5x1, double multiplicand5x2, double multiplicand5x3, double multiplicand5x4, double multiplicand5x5, double multiplicand5x6,
            double multiplicand6x1, double multiplicand6x2, double multiplicand6x3, double multiplicand6x4, double multiplicand6x5, double multiplicand6x6,
            double multiplier1x1, double multiplier1x2, double multiplier1x3, double multiplier1x4, double multiplier1x5,
            double multiplier2x1, double multiplier2x2, double multiplier2x3, double multiplier2x4, double multiplier2x5,
            double multiplier3x1, double multiplier3x2, double multiplier3x3, double multiplier3x4, double multiplier3x5,
            double multiplier4x1, double multiplier4x2, double multiplier4x3, double multiplier4x4, double multiplier4x5,
            double multiplier5x1, double multiplier5x2, double multiplier5x3, double multiplier5x4, double multiplier5x5)
            => (
                (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1) + (multiplicand1x3 * multiplier3x1) + (multiplicand1x4 * multiplier4x1) + (multiplicand1x5 * multiplier5x1) + multiplicand1x6,
                (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2) + (multiplicand1x3 * multiplier3x2) + (multiplicand1x4 * multiplier4x2) + (multiplicand1x5 * multiplier5x2) + multiplicand1x6,
                (multiplicand1x1 * multiplier1x3) + (multiplicand1x2 * multiplier2x3) + (multiplicand1x3 * multiplier3x3) + (multiplicand1x4 * multiplier4x3) + (multiplicand1x5 * multiplier5x3) + multiplicand1x6,
                (multiplicand1x1 * multiplier1x4) + (multiplicand1x2 * multiplier2x4) + (multiplicand1x3 * multiplier3x4) + (multiplicand1x4 * multiplier4x4) + (multiplicand1x5 * multiplier5x4) + multiplicand1x6,
                (multiplicand1x1 * multiplier1x5) + (multiplicand1x2 * multiplier2x5) + (multiplicand1x3 * multiplier3x5) + (multiplicand1x4 * multiplier4x5) + (multiplicand1x5 * multiplier5x5) + multiplicand1x6,
                multiplicand1x1 + multiplicand1x2 + multiplicand1x3 + multiplicand1x4 + multiplicand1x5 + multiplicand1x6,
                (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1) + (multiplicand2x3 * multiplier3x1) + (multiplicand2x4 * multiplier4x1) + (multiplicand2x5 * multiplier5x1) + multiplicand2x6,
                (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2) + (multiplicand2x3 * multiplier3x2) + (multiplicand2x4 * multiplier4x2) + (multiplicand2x5 * multiplier5x2) + multiplicand2x6,
                (multiplicand2x1 * multiplier1x3) + (multiplicand2x2 * multiplier2x3) + (multiplicand2x3 * multiplier3x3) + (multiplicand2x4 * multiplier4x3) + (multiplicand2x5 * multiplier5x3) + multiplicand2x6,
                (multiplicand2x1 * multiplier1x4) + (multiplicand2x2 * multiplier2x4) + (multiplicand2x3 * multiplier3x4) + (multiplicand2x4 * multiplier4x4) + (multiplicand2x5 * multiplier5x4) + multiplicand2x6,
                (multiplicand2x1 * multiplier1x5) + (multiplicand2x2 * multiplier2x5) + (multiplicand2x3 * multiplier3x5) + (multiplicand2x4 * multiplier4x5) + (multiplicand2x5 * multiplier5x5) + multiplicand2x6,
                multiplicand2x1 + multiplicand2x2 + multiplicand2x3 + multiplicand2x4 + multiplicand2x5 + multiplicand2x6,
                (multiplicand3x1 * multiplier1x1) + (multiplicand3x2 * multiplier2x1) + (multiplicand3x3 * multiplier3x1) + (multiplicand3x4 * multiplier4x1) + (multiplicand3x5 * multiplier5x1) + multiplicand3x6,
                (multiplicand3x1 * multiplier1x2) + (multiplicand3x2 * multiplier2x2) + (multiplicand3x3 * multiplier3x2) + (multiplicand3x4 * multiplier4x2) + (multiplicand3x5 * multiplier5x2) + multiplicand3x6,
                (multiplicand3x1 * multiplier1x3) + (multiplicand3x2 * multiplier2x3) + (multiplicand3x3 * multiplier3x3) + (multiplicand3x4 * multiplier4x3) + (multiplicand3x5 * multiplier5x3) + multiplicand3x6,
                (multiplicand3x1 * multiplier1x4) + (multiplicand3x2 * multiplier2x4) + (multiplicand3x3 * multiplier3x4) + (multiplicand3x4 * multiplier4x4) + (multiplicand3x5 * multiplier5x4) + multiplicand3x6,
                (multiplicand3x1 * multiplier1x5) + (multiplicand3x2 * multiplier2x5) + (multiplicand3x3 * multiplier3x5) + (multiplicand3x4 * multiplier4x5) + (multiplicand3x5 * multiplier5x5) + multiplicand3x6,
                multiplicand3x1 + multiplicand3x2 + multiplicand3x3 + multiplicand3x4 + multiplicand3x5 + multiplicand3x6,
                (multiplicand4x1 * multiplier1x1) + (multiplicand4x2 * multiplier2x1) + (multiplicand4x3 * multiplier3x1) + (multiplicand4x4 * multiplier4x1) + (multiplicand4x5 * multiplier5x1) + multiplicand4x6,
                (multiplicand4x1 * multiplier1x2) + (multiplicand4x2 * multiplier2x2) + (multiplicand4x3 * multiplier3x2) + (multiplicand4x4 * multiplier4x2) + (multiplicand4x5 * multiplier5x2) + multiplicand4x6,
                (multiplicand4x1 * multiplier1x3) + (multiplicand4x2 * multiplier2x3) + (multiplicand4x3 * multiplier3x3) + (multiplicand4x4 * multiplier4x3) + (multiplicand4x5 * multiplier5x3) + multiplicand4x6,
                (multiplicand4x1 * multiplier1x4) + (multiplicand4x2 * multiplier2x4) + (multiplicand4x3 * multiplier3x4) + (multiplicand4x4 * multiplier4x4) + (multiplicand4x5 * multiplier5x4) + multiplicand4x6,
                (multiplicand4x1 * multiplier1x5) + (multiplicand4x2 * multiplier2x5) + (multiplicand4x3 * multiplier3x5) + (multiplicand4x4 * multiplier4x5) + (multiplicand4x5 * multiplier5x5) + multiplicand4x6,
                multiplicand4x1 + multiplicand4x2 + multiplicand4x3 + multiplicand4x4 + multiplicand4x5 + multiplicand4x6,
                (multiplicand5x1 * multiplier1x1) + (multiplicand5x2 * multiplier2x1) + (multiplicand5x3 * multiplier3x1) + (multiplicand5x4 * multiplier4x1) + (multiplicand5x5 * multiplier5x1) + multiplicand5x6,
                (multiplicand5x1 * multiplier1x2) + (multiplicand5x2 * multiplier2x2) + (multiplicand5x3 * multiplier3x2) + (multiplicand5x4 * multiplier4x2) + (multiplicand5x5 * multiplier5x2) + multiplicand5x6,
                (multiplicand5x1 * multiplier1x3) + (multiplicand5x2 * multiplier2x3) + (multiplicand5x3 * multiplier3x3) + (multiplicand5x4 * multiplier4x3) + (multiplicand5x5 * multiplier5x3) + multiplicand5x6,
                (multiplicand5x1 * multiplier1x4) + (multiplicand5x2 * multiplier2x4) + (multiplicand5x3 * multiplier3x4) + (multiplicand5x4 * multiplier4x4) + (multiplicand5x5 * multiplier5x4) + multiplicand5x6,
                (multiplicand5x1 * multiplier1x5) + (multiplicand5x2 * multiplier2x5) + (multiplicand5x3 * multiplier3x5) + (multiplicand5x4 * multiplier4x5) + (multiplicand5x5 * multiplier5x5) + multiplicand5x6,
                multiplicand5x1 + multiplicand5x2 + multiplicand5x3 + multiplicand5x4 + multiplicand5x5 + multiplicand5x6,
                (multiplicand6x1 * multiplier1x1) + (multiplicand6x2 * multiplier2x1) + (multiplicand6x3 * multiplier3x1) + (multiplicand6x4 * multiplier4x1) + (multiplicand6x5 * multiplier5x1) + multiplicand6x6,
                (multiplicand6x1 * multiplier1x2) + (multiplicand6x2 * multiplier2x2) + (multiplicand6x3 * multiplier3x2) + (multiplicand6x4 * multiplier4x2) + (multiplicand6x5 * multiplier5x2) + multiplicand6x6,
                (multiplicand6x1 * multiplier1x3) + (multiplicand6x2 * multiplier2x3) + (multiplicand6x3 * multiplier3x3) + (multiplicand6x4 * multiplier4x3) + (multiplicand6x5 * multiplier5x3) + multiplicand6x6,
                (multiplicand6x1 * multiplier1x4) + (multiplicand6x2 * multiplier2x4) + (multiplicand6x3 * multiplier3x4) + (multiplicand6x4 * multiplier4x4) + (multiplicand6x5 * multiplier5x4) + multiplicand6x6,
                (multiplicand6x1 * multiplier1x5) + (multiplicand6x2 * multiplier2x5) + (multiplicand6x3 * multiplier3x5) + (multiplicand6x4 * multiplier4x5) + (multiplicand6x5 * multiplier5x5) + multiplicand6x6,
                multiplicand6x1 + multiplicand6x2 + multiplicand6x3 + multiplicand6x4 + multiplicand6x5 + multiplicand6x6
            );

        /// <summary>
        /// Multiply6x6x6x6s the specified multiplicand M1X1.
        /// </summary>
        /// <param name="multiplicand1x1">The multiplicand M1X1.</param>
        /// <param name="multiplicand1x2">The multiplicand M1X2.</param>
        /// <param name="multiplicand1x3">The multiplicand M1X3.</param>
        /// <param name="multiplicand1x4">The multiplicand M1X4.</param>
        /// <param name="multiplicand1x5">The multiplicand M1X5.</param>
        /// <param name="multiplicand1x6">The multiplicand M1X6.</param>
        /// <param name="multiplicand2x1">The multiplicand M2X1.</param>
        /// <param name="multiplicand2x2">The multiplicand M2X2.</param>
        /// <param name="multiplicand2x3">The multiplicand M2X3.</param>
        /// <param name="multiplicand2x4">The multiplicand M2X4.</param>
        /// <param name="multiplicand2x5">The multiplicand M2X5.</param>
        /// <param name="multiplicand2x6">The multiplicand M2X6.</param>
        /// <param name="multiplicand3x1">The multiplicand M3X1.</param>
        /// <param name="multiplicand3x2">The multiplicand M3X2.</param>
        /// <param name="multiplicand3x3">The multiplicand M3X3.</param>
        /// <param name="multiplicand3x4">The multiplicand M3X4.</param>
        /// <param name="multiplicand3x5">The multiplicand M3X5.</param>
        /// <param name="multiplicand3x6">The multiplicand M3X6.</param>
        /// <param name="multiplicand4x1">The multiplicand M4X1.</param>
        /// <param name="multiplicand4x2">The multiplicand M4X2.</param>
        /// <param name="multiplicand4x3">The multiplicand M4X3.</param>
        /// <param name="multiplicand4x4">The multiplicand M4X4.</param>
        /// <param name="multiplicand4x5">The multiplicand M4X5.</param>
        /// <param name="multiplicand4x6">The multiplicand M4X6.</param>
        /// <param name="multiplicand5x1">The multiplicand M5X1.</param>
        /// <param name="multiplicand5x2">The multiplicand M5X2.</param>
        /// <param name="multiplicand5x3">The multiplicand M5X3.</param>
        /// <param name="multiplicand5x4">The multiplicand M5X4.</param>
        /// <param name="multiplicand5x5">The multiplicand M5X5.</param>
        /// <param name="multiplicand5x6">The multiplicand M5X6.</param>
        /// <param name="multiplicand6x1">The multiplicand M6X1.</param>
        /// <param name="multiplicand6x2">The multiplicand M6X2.</param>
        /// <param name="multiplicand6x3">The multiplicand M6X3.</param>
        /// <param name="multiplicand6x4">The multiplicand M6X4.</param>
        /// <param name="multiplicand6x5">The multiplicand M6X5.</param>
        /// <param name="multiplicand6x6">The multiplicand M6X6.</param>
        /// <param name="multiplier1x1">The multiplier M1X1.</param>
        /// <param name="multiplier1x2">The multiplier M1X2.</param>
        /// <param name="multiplier1x3">The multiplier M1X3.</param>
        /// <param name="multiplier1x4">The multiplier M1X4.</param>
        /// <param name="multiplier1x5">The multiplier M1X5.</param>
        /// <param name="multiplier1x6">The multiplier M1X6.</param>
        /// <param name="multiplier2x1">The multiplier M2X1.</param>
        /// <param name="multiplier2x2">The multiplier M2X2.</param>
        /// <param name="multiplier2x3">The multiplier M2X3.</param>
        /// <param name="multiplier2x4">The multiplier M2X4.</param>
        /// <param name="multiplier2x5">The multiplier M2X5.</param>
        /// <param name="multiplier2x6">The multiplier M2X6.</param>
        /// <param name="multiplier3x1">The multiplier M3X1.</param>
        /// <param name="multiplier3x2">The multiplier M3X2.</param>
        /// <param name="multiplier3x3">The multiplier M3X3.</param>
        /// <param name="multiplier3x4">The multiplier M3X4.</param>
        /// <param name="multiplier3x5">The multiplier M3X5.</param>
        /// <param name="multiplier3x6">The multiplier M3X6.</param>
        /// <param name="multiplier4x1">The multiplier M4X1.</param>
        /// <param name="multiplier4x2">The multiplier M4X2.</param>
        /// <param name="multiplier4x3">The multiplier M4X3.</param>
        /// <param name="multiplier4x4">The multiplier M4X4.</param>
        /// <param name="multiplier4x5">The multiplier M4X5.</param>
        /// <param name="multiplier4x6">The multiplier M4X6.</param>
        /// <param name="multiplier5x1">The multiplier M5X1.</param>
        /// <param name="multiplier5x2">The multiplier M5X2.</param>
        /// <param name="multiplier5x3">The multiplier M5X3.</param>
        /// <param name="multiplier5x4">The multiplier M5X4.</param>
        /// <param name="multiplier5x5">The multiplier M5X5.</param>
        /// <param name="multiplier5x6">The multiplier M5X6.</param>
        /// <param name="multiplier6x1">The multiplier M6X1.</param>
        /// <param name="multiplier6x2">The multiplier M6X2.</param>
        /// <param name="multiplier6x3">The multiplier M6X3.</param>
        /// <param name="multiplier6x4">The multiplier M6X4.</param>
        /// <param name="multiplier6x5">The multiplier M6X5.</param>
        /// <param name="multiplier6x6">The multiplier M6X6.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double M1x1, double M1x2, double M1x3, double M1x4, double M1x5, double M1x6,
            double M2x1, double M2x2, double M2x3, double M2x4, double M2x5, double M2x6,
            double M3x1, double M3x2, double M3x3, double M3x4, double M3x5, double M3x6,
            double M4x1, double M4x2, double M4x3, double M4x4, double M4x5, double M4x6,
            double M5x1, double M5x2, double M5x3, double M5x4, double M5x5, double M5x6,
            double M6x1, double M6x2, double M6x3, double M6x4, double M6x5, double M6x6)
            Multiply6x6x6x6(
            double multiplicand1x1, double multiplicand1x2, double multiplicand1x3, double multiplicand1x4, double multiplicand1x5, double multiplicand1x6,
            double multiplicand2x1, double multiplicand2x2, double multiplicand2x3, double multiplicand2x4, double multiplicand2x5, double multiplicand2x6,
            double multiplicand3x1, double multiplicand3x2, double multiplicand3x3, double multiplicand3x4, double multiplicand3x5, double multiplicand3x6,
            double multiplicand4x1, double multiplicand4x2, double multiplicand4x3, double multiplicand4x4, double multiplicand4x5, double multiplicand4x6,
            double multiplicand5x1, double multiplicand5x2, double multiplicand5x3, double multiplicand5x4, double multiplicand5x5, double multiplicand5x6,
            double multiplicand6x1, double multiplicand6x2, double multiplicand6x3, double multiplicand6x4, double multiplicand6x5, double multiplicand6x6,
            double multiplier1x1, double multiplier1x2, double multiplier1x3, double multiplier1x4, double multiplier1x5, double multiplier1x6,
            double multiplier2x1, double multiplier2x2, double multiplier2x3, double multiplier2x4, double multiplier2x5, double multiplier2x6,
            double multiplier3x1, double multiplier3x2, double multiplier3x3, double multiplier3x4, double multiplier3x5, double multiplier3x6,
            double multiplier4x1, double multiplier4x2, double multiplier4x3, double multiplier4x4, double multiplier4x5, double multiplier4x6,
            double multiplier5x1, double multiplier5x2, double multiplier5x3, double multiplier5x4, double multiplier5x5, double multiplier5x6,
            double multiplier6x1, double multiplier6x2, double multiplier6x3, double multiplier6x4, double multiplier6x5, double multiplier6x6)
            => (
                (multiplicand1x1 * multiplier1x1) + (multiplicand1x2 * multiplier2x1) + (multiplicand1x3 * multiplier3x1) + (multiplicand1x4 * multiplier4x1) + (multiplicand1x5 * multiplier5x1) + (multiplicand1x6 * multiplier6x1),
                (multiplicand1x1 * multiplier1x2) + (multiplicand1x2 * multiplier2x2) + (multiplicand1x3 * multiplier3x2) + (multiplicand1x4 * multiplier4x2) + (multiplicand1x5 * multiplier5x2) + (multiplicand1x6 * multiplier6x2),
                (multiplicand1x1 * multiplier1x3) + (multiplicand1x2 * multiplier2x3) + (multiplicand1x3 * multiplier3x3) + (multiplicand1x4 * multiplier4x3) + (multiplicand1x5 * multiplier5x3) + (multiplicand1x6 * multiplier6x3),
                (multiplicand1x1 * multiplier1x4) + (multiplicand1x2 * multiplier2x4) + (multiplicand1x3 * multiplier3x4) + (multiplicand1x4 * multiplier4x4) + (multiplicand1x5 * multiplier5x4) + (multiplicand1x6 * multiplier6x4),
                (multiplicand1x1 * multiplier1x5) + (multiplicand1x2 * multiplier2x5) + (multiplicand1x3 * multiplier3x5) + (multiplicand1x4 * multiplier4x5) + (multiplicand1x5 * multiplier5x5) + (multiplicand1x6 * multiplier6x5),
                (multiplicand1x1 * multiplier1x6) + (multiplicand1x2 * multiplier2x6) + (multiplicand1x3 * multiplier3x6) + (multiplicand1x4 * multiplier4x6) + (multiplicand1x5 * multiplier5x6) + (multiplicand1x6 * multiplier6x6),
                (multiplicand2x1 * multiplier1x1) + (multiplicand2x2 * multiplier2x1) + (multiplicand2x3 * multiplier3x1) + (multiplicand2x4 * multiplier4x1) + (multiplicand2x5 * multiplier5x1) + (multiplicand2x6 * multiplier6x1),
                (multiplicand2x1 * multiplier1x2) + (multiplicand2x2 * multiplier2x2) + (multiplicand2x3 * multiplier3x2) + (multiplicand2x4 * multiplier4x2) + (multiplicand2x5 * multiplier5x2) + (multiplicand2x6 * multiplier6x2),
                (multiplicand2x1 * multiplier1x3) + (multiplicand2x2 * multiplier2x3) + (multiplicand2x3 * multiplier3x3) + (multiplicand2x4 * multiplier4x3) + (multiplicand2x5 * multiplier5x3) + (multiplicand2x6 * multiplier6x3),
                (multiplicand2x1 * multiplier1x4) + (multiplicand2x2 * multiplier2x4) + (multiplicand2x3 * multiplier3x4) + (multiplicand2x4 * multiplier4x4) + (multiplicand2x5 * multiplier5x4) + (multiplicand2x6 * multiplier6x4),
                (multiplicand2x1 * multiplier1x5) + (multiplicand2x2 * multiplier2x5) + (multiplicand2x3 * multiplier3x5) + (multiplicand2x4 * multiplier4x5) + (multiplicand2x5 * multiplier5x5) + (multiplicand2x6 * multiplier6x5),
                (multiplicand2x1 * multiplier1x6) + (multiplicand2x2 * multiplier2x6) + (multiplicand2x3 * multiplier3x6) + (multiplicand2x4 * multiplier4x6) + (multiplicand2x5 * multiplier5x6) + (multiplicand2x6 * multiplier6x6),
                (multiplicand3x1 * multiplier1x1) + (multiplicand3x2 * multiplier2x1) + (multiplicand3x3 * multiplier3x1) + (multiplicand3x4 * multiplier4x1) + (multiplicand3x5 * multiplier5x1) + (multiplicand3x6 * multiplier6x1),
                (multiplicand3x1 * multiplier1x2) + (multiplicand3x2 * multiplier2x2) + (multiplicand3x3 * multiplier3x2) + (multiplicand3x4 * multiplier4x2) + (multiplicand3x5 * multiplier5x2) + (multiplicand3x6 * multiplier6x2),
                (multiplicand3x1 * multiplier1x3) + (multiplicand3x2 * multiplier2x3) + (multiplicand3x3 * multiplier3x3) + (multiplicand3x4 * multiplier4x3) + (multiplicand3x5 * multiplier5x3) + (multiplicand3x6 * multiplier6x3),
                (multiplicand3x1 * multiplier1x4) + (multiplicand3x2 * multiplier2x4) + (multiplicand3x3 * multiplier3x4) + (multiplicand3x4 * multiplier4x4) + (multiplicand3x5 * multiplier5x4) + (multiplicand3x6 * multiplier6x4),
                (multiplicand3x1 * multiplier1x5) + (multiplicand3x2 * multiplier2x5) + (multiplicand3x3 * multiplier3x5) + (multiplicand3x4 * multiplier4x5) + (multiplicand3x5 * multiplier5x5) + (multiplicand3x6 * multiplier6x5),
                (multiplicand3x1 * multiplier1x6) + (multiplicand3x2 * multiplier2x6) + (multiplicand3x3 * multiplier3x6) + (multiplicand3x4 * multiplier4x6) + (multiplicand3x5 * multiplier5x6) + (multiplicand3x6 * multiplier6x6),
                (multiplicand4x1 * multiplier1x1) + (multiplicand4x2 * multiplier2x1) + (multiplicand4x3 * multiplier3x1) + (multiplicand4x4 * multiplier4x1) + (multiplicand4x5 * multiplier5x1) + (multiplicand4x6 * multiplier6x1),
                (multiplicand4x1 * multiplier1x2) + (multiplicand4x2 * multiplier2x2) + (multiplicand4x3 * multiplier3x2) + (multiplicand4x4 * multiplier4x2) + (multiplicand4x5 * multiplier5x2) + (multiplicand4x6 * multiplier6x2),
                (multiplicand4x1 * multiplier1x3) + (multiplicand4x2 * multiplier2x3) + (multiplicand4x3 * multiplier3x3) + (multiplicand4x4 * multiplier4x3) + (multiplicand4x5 * multiplier5x3) + (multiplicand4x6 * multiplier6x3),
                (multiplicand4x1 * multiplier1x4) + (multiplicand4x2 * multiplier2x4) + (multiplicand4x3 * multiplier3x4) + (multiplicand4x4 * multiplier4x4) + (multiplicand4x5 * multiplier5x4) + (multiplicand4x6 * multiplier6x4),
                (multiplicand4x1 * multiplier1x5) + (multiplicand4x2 * multiplier2x5) + (multiplicand4x3 * multiplier3x5) + (multiplicand4x4 * multiplier4x5) + (multiplicand4x5 * multiplier5x5) + (multiplicand4x6 * multiplier6x5),
                (multiplicand4x1 * multiplier1x6) + (multiplicand4x2 * multiplier2x6) + (multiplicand4x3 * multiplier3x6) + (multiplicand4x4 * multiplier4x6) + (multiplicand4x5 * multiplier5x6) + (multiplicand4x6 * multiplier6x6),
                (multiplicand5x1 * multiplier1x1) + (multiplicand5x2 * multiplier2x1) + (multiplicand5x3 * multiplier3x1) + (multiplicand5x4 * multiplier4x1) + (multiplicand5x5 * multiplier5x1) + (multiplicand5x6 * multiplier6x1),
                (multiplicand5x1 * multiplier1x2) + (multiplicand5x2 * multiplier2x2) + (multiplicand5x3 * multiplier3x2) + (multiplicand5x4 * multiplier4x2) + (multiplicand5x5 * multiplier5x2) + (multiplicand5x6 * multiplier6x2),
                (multiplicand5x1 * multiplier1x3) + (multiplicand5x2 * multiplier2x3) + (multiplicand5x3 * multiplier3x3) + (multiplicand5x4 * multiplier4x3) + (multiplicand5x5 * multiplier5x3) + (multiplicand5x6 * multiplier6x3),
                (multiplicand5x1 * multiplier1x4) + (multiplicand5x2 * multiplier2x4) + (multiplicand5x3 * multiplier3x4) + (multiplicand5x4 * multiplier4x4) + (multiplicand5x5 * multiplier5x4) + (multiplicand5x6 * multiplier6x4),
                (multiplicand5x1 * multiplier1x5) + (multiplicand5x2 * multiplier2x5) + (multiplicand5x3 * multiplier3x5) + (multiplicand5x4 * multiplier4x5) + (multiplicand5x5 * multiplier5x5) + (multiplicand5x6 * multiplier6x5),
                (multiplicand5x1 * multiplier1x6) + (multiplicand5x2 * multiplier2x6) + (multiplicand5x3 * multiplier3x6) + (multiplicand5x4 * multiplier4x6) + (multiplicand5x5 * multiplier5x6) + (multiplicand5x6 * multiplier6x6),
                (multiplicand6x1 * multiplier1x1) + (multiplicand6x2 * multiplier2x1) + (multiplicand6x3 * multiplier3x1) + (multiplicand6x4 * multiplier4x1) + (multiplicand6x5 * multiplier5x1) + (multiplicand6x6 * multiplier6x1),
                (multiplicand6x1 * multiplier1x2) + (multiplicand6x2 * multiplier2x2) + (multiplicand6x3 * multiplier3x2) + (multiplicand6x4 * multiplier4x2) + (multiplicand6x5 * multiplier5x2) + (multiplicand6x6 * multiplier6x2),
                (multiplicand6x1 * multiplier1x3) + (multiplicand6x2 * multiplier2x3) + (multiplicand6x3 * multiplier3x3) + (multiplicand6x4 * multiplier4x3) + (multiplicand6x5 * multiplier5x3) + (multiplicand6x6 * multiplier6x3),
                (multiplicand6x1 * multiplier1x4) + (multiplicand6x2 * multiplier2x4) + (multiplicand6x3 * multiplier3x4) + (multiplicand6x4 * multiplier4x4) + (multiplicand6x5 * multiplier5x4) + (multiplicand6x6 * multiplier6x4),
                (multiplicand6x1 * multiplier1x5) + (multiplicand6x2 * multiplier2x5) + (multiplicand6x3 * multiplier3x5) + (multiplicand6x4 * multiplier4x5) + (multiplicand6x5 * multiplier5x5) + (multiplicand6x6 * multiplier6x5),
                (multiplicand6x1 * multiplier1x6) + (multiplicand6x2 * multiplier2x6) + (multiplicand6x3 * multiplier3x6) + (multiplicand6x4 * multiplier4x6) + (multiplicand6x5 * multiplier5x6) + (multiplicand6x6 * multiplier6x6)
            );
        #endregion Multiply
    }
}
