// <copyright file="Operations.Arithmatics.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Engine;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B) Plus<T>(T valueA, T valueB) where T : INumberBase<T> => (+valueA, +valueB);

    /// <summary>
    /// Unary Plus 3d.
    /// </summary>
    /// <param name="valueA">The value a.</param>
    /// <param name="valueB">The value b.</param>
    /// <param name="valueC">The value c.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C) Plus<T>(T valueA, T valueB, T valueC) where T : INumberBase<T> => (+valueA, +valueB, +valueC);

    /// <summary>
    /// Unary Plus 4d.
    /// </summary>
    /// <param name="valueA">The value a.</param>
    /// <param name="valueB">The value b.</param>
    /// <param name="valueC">The value c.</param>
    /// <param name="valueD">The value d.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D) Plus<T>(T valueA, T valueB, T valueC, T valueD) where T : INumberBase<T> => (+valueA, +valueB, +valueC, +valueD);

    /// <summary>
    /// Unary Plus 5d.
    /// </summary>
    /// <param name="valueA">The value a.</param>
    /// <param name="valueB">The value b.</param>
    /// <param name="valueC">The value c.</param>
    /// <param name="valueD">The value d.</param>
    /// <param name="valueE">The value e.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D, T E) Plus<T>(T valueA, T valueB, T valueC, T valueD, T valueE) where T : INumberBase<T> => (+valueA, +valueB, +valueC, +valueD, +valueE);

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D, T E, T F) Plus<T>(T valueA, T valueB, T valueC, T valueD, T valueE, T valueF) where T : INumberBase<T> => (+valueA, +valueB, +valueC, +valueD, +valueE, +valueF);

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m0x0, T m0x1, T m0x2,
        T m1x0, T m1x1, T m1x2,
        T m2x0, T m2x1, T m2x2
        ) Plus<T>(
        T sourceM0x0, T sourceM0x1, T sourceM0x2,
        T sourceM1x0, T sourceM1x1, T sourceM1x2,
        T sourceM2x0, T sourceM2x1, T sourceM2x2) where T : INumberBase<T>
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m0x0, T m0x1, T m0x2, T m0x3,
        T m1x0, T m1x1, T m1x2, T m1x3,
        T m2x0, T m2x1, T m2x2, T m2x3,
        T m3x0, T m3x1, T m3x2, T m3x3
        ) Plus<T>(
        T sourceM0x0, T sourceM0x1, T sourceM0x2, T sourceM0x3,
        T sourceM1x0, T sourceM1x1, T sourceM1x2, T sourceM1x3,
        T sourceM2x0, T sourceM2x1, T sourceM2x2, T sourceM2x3,
        T sourceM3x0, T sourceM3x1, T sourceM3x2, T sourceM3x3) where T : INumberBase<T>
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m0x0, T m0x1, T m0x2, T m0x3, T m0x4,
        T m1x0, T m1x1, T m1x2, T m1x3, T m1x4,
        T m2x0, T m2x1, T m2x2, T m2x3, T m2x4,
        T m3x0, T m3x1, T m3x2, T m3x3, T m3x4,
        T m4x0, T m4x1, T m4x2, T m4x3, T m4x4
        ) Plus<T>(
        T sourceM0x0, T sourceM0x1, T sourceM0x2, T sourceM0x3, T sourceM0x4,
        T sourceM1x0, T sourceM1x1, T sourceM1x2, T sourceM1x3, T sourceM1x4,
        T sourceM2x0, T sourceM2x1, T sourceM2x2, T sourceM2x3, T sourceM2x4,
        T sourceM3x0, T sourceM3x1, T sourceM3x2, T sourceM3x3, T sourceM3x4,
        T sourceM4x0, T sourceM4x1, T sourceM4x2, T sourceM4x3, T sourceM4x4) where T : INumberBase<T>
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m0x0, T m0x1, T m0x2, T m0x3, T m0x4, T m0x5,
        T m1x0, T m1x1, T m1x2, T m1x3, T m1x4, T m1x5,
        T m2x0, T m2x1, T m2x2, T m2x3, T m2x4, T m2x5,
        T m3x0, T m3x1, T m3x2, T m3x3, T m3x4, T m3x5,
        T m4x0, T m4x1, T m4x2, T m4x3, T m4x4, T m4x5,
        T m5x0, T m5x1, T m5x2, T m5x3, T m5x4, T m5x5
        ) Plus<T>(
        T sourceM0x0, T sourceM0x1, T sourceM0x2, T sourceM0x3, T sourceM0x4, T sourceM0x5,
        T sourceM1x0, T sourceM1x1, T sourceM1x2, T sourceM1x3, T sourceM1x4, T sourceM1x5,
        T sourceM2x0, T sourceM2x1, T sourceM2x2, T sourceM2x3, T sourceM2x4, T sourceM2x5,
        T sourceM3x0, T sourceM3x1, T sourceM3x2, T sourceM3x3, T sourceM3x4, T sourceM3x5,
        T sourceM4x0, T sourceM4x1, T sourceM4x2, T sourceM4x3, T sourceM4x4, T sourceM4x5,
        T sourceM5x0, T sourceM5x1, T sourceM5x2, T sourceM5x3, T sourceM5x4, T sourceM5x5) where T : INumberBase<T>
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B) Negate<T>(T valueA, T valueB) where T : INumberBase<T> => (-valueA, -valueB);

    /// <summary>
    /// Negates the vector.
    /// </summary>
    /// <param name="valueA">The value a.</param>
    /// <param name="valueB">The value b.</param>
    /// <param name="valueC">The value c.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C) Negate<T>(T valueA, T valueB, T valueC) where T : INumberBase<T> => (-valueA, -valueB, -valueC);

    /// <summary>
    /// Negates the vector.
    /// </summary>
    /// <param name="valueA">The value a.</param>
    /// <param name="valueB">The value b.</param>
    /// <param name="valueC">The value c.</param>
    /// <param name="valueD">The value d.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D) Negate<T>(T valueA, T valueB, T valueC, T valueD) where T : INumberBase<T> => (-valueA, -valueB, -valueC, -valueD);

    /// <summary>
    /// Negates the vector.
    /// </summary>
    /// <param name="valueA">The value a.</param>
    /// <param name="valueB">The value b.</param>
    /// <param name="valueC">The value c.</param>
    /// <param name="valueD">The value d.</param>
    /// <param name="valueE">The value e.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D, T E) Negate<T>(T valueA, T valueB, T valueC, T valueD, T valueE) where T : INumberBase<T> => (-valueA, -valueB, -valueC, -valueD, -valueE);

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D, T E, T F) Negate<T>(T valueA, T valueB, T valueC, T valueD, T valueE, T valueF) where T : INumberBase<T> => (-valueA, -valueB, -valueC, -valueD, -valueE, -valueF);

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m0x0, T m0x1, T m0x2,
        T m1x0, T m1x1, T m1x2,
        T m2x0, T m2x1, T m2x2
        ) Negate<T>(
        T sourceM0x0, T sourceM0x1, T sourceM0x2,
        T sourceM1x0, T sourceM1x1, T sourceM1x2,
        T sourceM2x0, T sourceM2x1, T sourceM2x2) where T : INumberBase<T>
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m0x0, T m0x1, T m0x2, T m0x3,
        T m1x0, T m1x1, T m1x2, T m1x3,
        T m2x0, T m2x1, T m2x2, T m2x3,
        T m3x0, T m3x1, T m3x2, T m3x3
        ) Negate<T>(
        T sourceM0x0, T sourceM0x1, T sourceM0x2, T sourceM0x3,
        T sourceM1x0, T sourceM1x1, T sourceM1x2, T sourceM1x3,
        T sourceM2x0, T sourceM2x1, T sourceM2x2, T sourceM2x3,
        T sourceM3x0, T sourceM3x1, T sourceM3x2, T sourceM3x3) where T : INumberBase<T>
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m0x0, T m0x1, T m0x2, T m0x3, T m0x4,
        T m1x0, T m1x1, T m1x2, T m1x3, T m1x4,
        T m2x0, T m2x1, T m2x2, T m2x3, T m2x4,
        T m3x0, T m3x1, T m3x2, T m3x3, T m3x4,
        T m4x0, T m4x1, T m4x2, T m4x3, T m4x4
        ) Negate<T>(
        T sourceM0x0, T sourceM0x1, T sourceM0x2, T sourceM0x3, T sourceM0x4,
        T sourceM1x0, T sourceM1x1, T sourceM1x2, T sourceM1x3, T sourceM1x4,
        T sourceM2x0, T sourceM2x1, T sourceM2x2, T sourceM2x3, T sourceM2x4,
        T sourceM3x0, T sourceM3x1, T sourceM3x2, T sourceM3x3, T sourceM3x4,
        T sourceM4x0, T sourceM4x1, T sourceM4x2, T sourceM4x3, T sourceM4x4) where T : INumberBase<T>
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (
        T m0x0, T m0x1, T m0x2, T m0x3, T m0x4, T m0x5,
        T m1x0, T m1x1, T m1x2, T m1x3, T m1x4, T m1x5,
        T m2x0, T m2x1, T m2x2, T m2x3, T m2x4, T m2x5,
        T m3x0, T m3x1, T m3x2, T m3x3, T m3x4, T m3x5,
        T m4x0, T m4x1, T m4x2, T m4x3, T m4x4, T m4x5,
        T m5x0, T m5x1, T m5x2, T m5x3, T m5x4, T m5x5
        ) Negate<T>(
        T sourceM0x0, T sourceM0x1, T sourceM0x2, T sourceM0x3, T sourceM0x4, T sourceM0x5,
        T sourceM1x0, T sourceM1x1, T sourceM1x2, T sourceM1x3, T sourceM1x4, T sourceM1x5,
        T sourceM2x0, T sourceM2x1, T sourceM2x2, T sourceM2x3, T sourceM2x4, T sourceM2x5,
        T sourceM3x0, T sourceM3x1, T sourceM3x2, T sourceM3x3, T sourceM3x4, T sourceM3x5,
        T sourceM4x0, T sourceM4x1, T sourceM4x2, T sourceM4x3, T sourceM4x4, T sourceM4x5,
        T sourceM5x0, T sourceM5x1, T sourceM5x2, T sourceM5x3, T sourceM5x4, T sourceM5x5) where T : INumberBase<T>
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
    /// <returns>The <see cref="T"/>.</returns>
    /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Min<T>(T x, T y, T z) where T : INumber<T> => x < y ? x < z ? x : z : y < z ? y : z;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Min2<T>(T x, T y, T z) where T : INumber<T> => T.Min(x, T.Min(y, z));

    /// <summary>
    /// Find the minimum value of four variables.
    /// </summary>
    /// <param name="w">The first variable.</param>
    /// <param name="x">The second variable.</param>
    /// <param name="y">The third variable.</param>
    /// <param name="z">The fourth variable.</param>
    /// <returns>The <see cref="T"/>.</returns>
    /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Min<T>(T w, T x, T y, T z)
        where T : INumber<T>
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Min2<T>(T w, T x, T y, T z) where T : INumber<T> => T.Min(w, T.Min(x, T.Max(y, z)));
    #endregion Min

    #region Max
    /// <summary>
    /// Find the maximum value of three variables.
    /// </summary>
    /// <param name="x">The first variable.</param>
    /// <param name="y">The second variable.</param>
    /// <param name="z">The third variable.</param>
    /// <returns>The <see cref="T"/>.</returns>
    /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Max<T>(T x, T y, T z) where T : INumber<T> => x > y ? x > z ? x : z : y > z ? y : z;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Max2<T>(T x, T y, T z) where T : INumber<T> => T.Max(x, T.Max(y, z));

    /// <summary>
    /// Find the maximum value of four variables.
    /// </summary>
    /// <param name="w">The first variable.</param>
    /// <param name="x">The second variable.</param>
    /// <param name="y">The third variable.</param>
    /// <param name="z">The fourth variable.</param>
    /// <returns>The <see cref="T"/>.</returns>
    /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Max<T>(T w, T x, T y, T z)
        where T : INumber<T>
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
    /// <returns>The <see cref="T"/>.</returns>
    /// <acknowledgment>
    /// http://stackoverflow.com/questions/6800838/in-c-sharp-is-there-a-method-to-find-the-max-of-3-numbers
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Max2<T>(T w, T x, T y, T z) where T : INumber<T> => T.Max(w, T.Max(x, T.Max(y, z)));
    #endregion Max

    #region Min Max
    /// <summary>
    /// The min max.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="min">The min.</param>
    /// <param name="max">The max.</param>
    /// <returns>The <see cref="T"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T MinMax<T>(T x, T min, T max) where T : INumber<T> => (x < min) ? min : (x > max) ? max : x;
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T LeastCommonDenominator<T>(T a, T b) where T : INumber<T> => a * b / GreatestCommonDenominator(a, b);
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T GreatestCommonDenominator<T>(T a, T b) where T : INumber<T>
    {
        T temp;
        while (b != T.Zero)
        {
            temp = b;
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
    /// <returns>The <see cref="T"/>.</returns>
    /// <acknowledgment>
    /// http://www.angusj.com
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Round<T>(this T val) where T : IFloatingPoint<T> => T.CreateSaturating((T.Zero < val) ? int.CreateTruncating(val + T.CreateSaturating(0.5)) : int.CreateTruncating(val - T.CreateSaturating(0.5)));
    #endregion

    #region Round to Multiple
    /// <summary>
    /// Round a value to the nearest multiple of a number.
    /// </summary>
    /// <param name="value">The value to round.</param>
    /// <param name="multiple">The multiple to round to.</param>
    /// <returns>Returns a value rounded to an interval of the multiple.</returns>
    /// <remarks><para>Using Convert.ToInt32 because it is faster and guarantees bankers rounding.</para></remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T RoundToMultiple<T>(this T value, T multiple) where T : IFloatingPointIeee754<T>  => T.Round(value / multiple, MidpointRounding.ToEven) * multiple;
    #endregion

    #region Clamp
    /// <summary>
    /// Keep the value between the maximum and minimum.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">The lower limit the value should be above.</param>
    /// <param name="max">The upper limit the value should be under.</param>
    /// <returns>A value clamped between the maximum and minimum values.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Clamp<T>(this T value, T min, T max) where T : INumber<T> => value > max ? max : value < min ? min : value;
    #endregion

    #region Wrapping
    /// <summary>
    /// The wrap.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="min">The min.</param>
    /// <param name="max">The max.</param>
    /// <returns>The <see cref="sbyte"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Wrap<T>(this T value, T min, T max) where T : INumber<T> => (value < min) ? (max - ((min - value) % (max - min))) : (min + ((value - min) % (max - min)));
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static IEnumerable<T> StepRange<T>(T min, T max, T step)
        where T : INumber<T>
    {
        T i;
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
    /// Generates a range of T values from a minimum value to a maximum value.
    /// </summary>
    /// <param name="min">The minimum.</param>
    /// <param name="max">The maximum.</param>
    /// <param name="numberOfSteps">The number of steps.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException">numberOfSteps - Number of steps must be greater than zero.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static IEnumerable<T> CountRange<T>(T min, T max, int numberOfSteps)
        where T : INumber<T>
    {
        if (numberOfSteps < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(numberOfSteps), "Number of steps must be greater than zero.");
        }

        var stepSize = (max - min) / T.CreateSaturating(numberOfSteps);
        return Enumerable.Range(0, numberOfSteps + 1).Select(stepIndex => min + (T.CreateSaturating(stepIndex) * stepSize));
    }
}
