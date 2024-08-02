// <copyright file="Operations.Vectors.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using Engine.Mathematics;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using static System.Math;

namespace Engine;

/// <summary>
/// Some standard shared operations to perform on Points, Vectors, and Sizes.
/// </summary>
public static partial class Operations
{
    #region Is Empty Vector
    /// <summary>
    /// Determines whether the specified vector is empty.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="i">The i.</param>
    /// <param name="j">The j.</param>
    /// <returns>
    ///   <see langword="true"/> if [is empty vector] [the specified i]; otherwise, <see langword="false"/>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsEmptyVector<T>(T i, T j)
        where T : INumber<T> => T.Zero switch
        {
            _ when T.Zero is char or sbyte or byte or short or ushort or int or uint or long or ulong or Int128 or UInt128 or nint or nuint or BigInteger => i == T.Zero && j == T.Zero,
            _ when T.Zero is float => T.Abs(i) < T.CreateSaturating(float.Epsilon) && T.Abs(j) < T.CreateSaturating(float.Epsilon),
            _ when T.Zero is double => T.Abs(i) < T.CreateSaturating(double.Epsilon) && T.Abs(j) < T.CreateSaturating(double.Epsilon),
            _ when T.Zero is decimal => T.Abs(i) < T.CreateSaturating(NumericTraits<decimal>.Epsilon) && T.Abs(j) < T.CreateSaturating(NumericTraits<decimal>.Epsilon),
            _ when T.Zero is Complex => T.Abs(i) < T.CreateSaturating(NumericTraits<double>.Epsilon) && T.Abs(j) < T.CreateSaturating(NumericTraits<double>.Epsilon),
            _ => throw new NotSupportedException($"Type {typeof(T)} is not supported")
        };

    /// <summary>
    /// Determines whether the specified vector is empty.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="i">The i.</param>
    /// <param name="j">The j.</param>
    /// <param name="k">The k.</param>
    /// <returns>
    ///   <see langword="true" /> if [is empty vector] [the specified i]; otherwise, <see langword="false" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsEmptyVector<T>(T i, T j, T k)
        where T : INumber<T> => T.Zero switch
        {
            _ when T.Zero is char or sbyte or byte or short or ushort or int or uint or long or ulong or Int128 or UInt128 or nint or nuint or BigInteger => i == T.Zero && j == T.Zero && k == T.Zero,
            _ when T.Zero is float => T.Abs(i) < T.CreateSaturating(float.Epsilon) && T.Abs(j) < T.CreateSaturating(float.Epsilon) && T.Abs(k) < T.CreateSaturating(float.Epsilon),
            _ when T.Zero is double => T.Abs(i) < T.CreateSaturating(double.Epsilon) && T.Abs(j) < T.CreateSaturating(double.Epsilon) && T.Abs(k) < T.CreateSaturating(double.Epsilon),
            _ when T.Zero is decimal => T.Abs(i) < T.CreateSaturating(NumericTraits<decimal>.Epsilon) && T.Abs(j) < T.CreateSaturating(NumericTraits<decimal>.Epsilon) && T.Abs(k) < T.CreateSaturating(NumericTraits<decimal>.Epsilon),
            _ when T.Zero is Complex => T.Abs(i) < T.CreateSaturating(NumericTraits<double>.Epsilon) && T.Abs(j) < T.CreateSaturating(NumericTraits<double>.Epsilon) && T.Abs(k) < T.CreateSaturating(NumericTraits<double>.Epsilon),
            _ => throw new NotSupportedException($"Type {typeof(T)} is not supported")
        };

    /// <summary>
    /// Determines whether the specified vector is empty.
    /// </summary>
    /// <param name="i">The i.</param>
    /// <param name="j">The j.</param>
    /// <param name="k">The k.</param>
    /// <param name="l">The l.</param>
    /// <returns>
    ///   <see langword="true"/> if [is empty vector] [the specified i]; otherwise, <see langword="false"/>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsEmptyVector<T>(T i, T j, T k, T l)
        where T : INumber<T> => T.Zero switch
        {
            _ when T.Zero is char or sbyte or byte or short or ushort or int or uint or long or ulong or Int128 or UInt128 or nint or nuint or BigInteger => i == T.Zero && j == T.Zero && k == T.Zero && l == T.Zero,
            _ when T.Zero is float => T.Abs(i) < T.CreateSaturating(float.Epsilon) && T.Abs(j) < T.CreateSaturating(float.Epsilon) && T.Abs(k) < T.CreateSaturating(float.Epsilon) && T.Abs(l) < T.CreateSaturating(float.Epsilon),
            _ when T.Zero is double => T.Abs(i) < T.CreateSaturating(double.Epsilon) && T.Abs(j) < T.CreateSaturating(double.Epsilon) && T.Abs(k) < T.CreateSaturating(double.Epsilon) && T.Abs(l) < T.CreateSaturating(double.Epsilon),
            _ when T.Zero is decimal => T.Abs(i) < T.CreateSaturating(NumericTraits<decimal>.Epsilon) && T.Abs(j) < T.CreateSaturating(NumericTraits<decimal>.Epsilon) && T.Abs(k) < T.CreateSaturating(NumericTraits<decimal>.Epsilon) && T.Abs(l) < T.CreateSaturating(NumericTraits<decimal>.Epsilon),
            _ when T.Zero is Complex => T.Abs(i) < T.CreateSaturating(NumericTraits<double>.Epsilon) && T.Abs(j) < T.CreateSaturating(NumericTraits<double>.Epsilon) && T.Abs(k) < T.CreateSaturating(NumericTraits<double>.Epsilon) && T.Abs(l) < T.CreateSaturating(NumericTraits<double>.Epsilon),
            _ => throw new NotSupportedException($"Type {typeof(T)} is not supported")
        };

    /// <summary>
    /// Determines whether the specified vector is empty.
    /// </summary>
    /// <param name="i">The i.</param>
    /// <param name="j">The j.</param>
    /// <param name="k">The k.</param>
    /// <param name="l">The l.</param>
    /// <param name="m">The m.</param>
    /// <returns>
    ///   <see langword="true"/> if [is empty vector] [the specified i]; otherwise, <see langword="false"/>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsEmptyVector<T>(T i, T j, T k, T l, T m)
        where T : INumber<T> => T.Zero switch
        {
            _ when T.Zero is char or sbyte or byte or short or ushort or int or uint or long or ulong or Int128 or UInt128 or nint or nuint or BigInteger => i == T.Zero && j == T.Zero && k == T.Zero && l == T.Zero && m == T.Zero,
            _ when T.Zero is float => T.Abs(i) < T.CreateSaturating(float.Epsilon) && T.Abs(j) < T.CreateSaturating(float.Epsilon) && T.Abs(k) < T.CreateSaturating(float.Epsilon) && T.Abs(l) < T.CreateSaturating(float.Epsilon) && T.Abs(m) < T.CreateSaturating(float.Epsilon),
            _ when T.Zero is double => T.Abs(i) < T.CreateSaturating(double.Epsilon) && T.Abs(j) < T.CreateSaturating(double.Epsilon) && T.Abs(k) < T.CreateSaturating(double.Epsilon) && T.Abs(l) < T.CreateSaturating(double.Epsilon) && T.Abs(m) < T.CreateSaturating(double.Epsilon),
            _ when T.Zero is decimal => T.Abs(i) < T.CreateSaturating(NumericTraits<decimal>.Epsilon) && T.Abs(j) < T.CreateSaturating(NumericTraits<decimal>.Epsilon) && T.Abs(k) < T.CreateSaturating(NumericTraits<decimal>.Epsilon) && T.Abs(l) < T.CreateSaturating(NumericTraits<decimal>.Epsilon) && T.Abs(m) < T.CreateSaturating(NumericTraits<decimal>.Epsilon),
            _ when T.Zero is Complex => T.Abs(i) < T.CreateSaturating(NumericTraits<double>.Epsilon) && T.Abs(j) < T.CreateSaturating(NumericTraits<double>.Epsilon) && T.Abs(k) < T.CreateSaturating(NumericTraits<double>.Epsilon) && T.Abs(l) < T.CreateSaturating(NumericTraits<double>.Epsilon) && T.Abs(m) < T.CreateSaturating(NumericTraits<double>.Epsilon),
            _ => throw new NotSupportedException($"Type {typeof(T)} is not supported")
        };

    /// <summary>
    /// Determines whether the specified vector is empty.
    /// </summary>
    /// <param name="i">The i.</param>
    /// <param name="j">The j.</param>
    /// <param name="k">The k.</param>
    /// <param name="l">The l.</param>
    /// <param name="m">The m.</param>
    /// <param name="n">The n.</param>
    /// <returns>
    ///   <see langword="true"/> if [is empty vector] [the specified i]; otherwise, <see langword="false"/>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsEmptyVector<T>(T i, T j, T k, T l, T m, T n)
        where T : INumber<T> => T.Zero switch
        {
            _ when T.Zero is char or sbyte or byte or short or ushort or int or uint or long or ulong or Int128 or UInt128 or nint or nuint or BigInteger => i == T.Zero && j == T.Zero && k == T.Zero && l == T.Zero && m == T.Zero && n == T.Zero,
            _ when T.Zero is float => T.Abs(i) < T.CreateSaturating(float.Epsilon) && T.Abs(j) < T.CreateSaturating(float.Epsilon) && T.Abs(k) < T.CreateSaturating(float.Epsilon) && T.Abs(l) < T.CreateSaturating(float.Epsilon) && T.Abs(m) < T.CreateSaturating(float.Epsilon) && T.Abs(n) < T.CreateSaturating(float.Epsilon),
            _ when T.Zero is double => T.Abs(i) < T.CreateSaturating(double.Epsilon) && T.Abs(j) < T.CreateSaturating(double.Epsilon) && T.Abs(k) < T.CreateSaturating(double.Epsilon) && T.Abs(l) < T.CreateSaturating(double.Epsilon) && T.Abs(m) < T.CreateSaturating(double.Epsilon) && T.Abs(n) < T.CreateSaturating(double.Epsilon),
            _ when T.Zero is decimal => T.Abs(i) < T.CreateSaturating(NumericTraits<decimal>.Epsilon) && T.Abs(j) < T.CreateSaturating(NumericTraits<decimal>.Epsilon) && T.Abs(k) < T.CreateSaturating(NumericTraits<decimal>.Epsilon) && T.Abs(l) < T.CreateSaturating(NumericTraits<decimal>.Epsilon) && T.Abs(m) < T.CreateSaturating(NumericTraits<decimal>.Epsilon) && T.Abs(n) < T.CreateSaturating(NumericTraits<decimal>.Epsilon),
            _ when T.Zero is Complex => T.Abs(i) < T.CreateSaturating(NumericTraits<double>.Epsilon) && T.Abs(j) < T.CreateSaturating(NumericTraits<double>.Epsilon) && T.Abs(k) < T.CreateSaturating(NumericTraits<double>.Epsilon) && T.Abs(l) < T.CreateSaturating(NumericTraits<double>.Epsilon) && T.Abs(m) < T.CreateSaturating(NumericTraits<double>.Epsilon) && T.Abs(n) < T.CreateSaturating(NumericTraits<double>.Epsilon),
            _ => throw new NotSupportedException($"Type {typeof(T)} is not supported")
        };
    #endregion

    #region Is Unit Vector
    ///// <summary>
    ///// The is unit vector.
    ///// </summary>
    ///// <param name="i">The i1.</param>
    ///// <param name="j">The j1.</param>
    ///// <returns>
    ///// The <see cref="bool" />.
    ///// </returns>
    ///// <acknowledgment>
    ///// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
    ///// </acknowledgment>
    //[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    //public static bool IsUnitVector<T>(T i, T j) where T : IFloatingPointIeee754<T>
    //{
    //    return T.Abs(Magnitude(i, j) - T.One) < T.Epsilon;
    //}

    /// <summary>
    /// The is unit vector.
    /// </summary>
    /// <param name="i">The i1.</param>
    /// <param name="j">The j1.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsUnitVector<T>(T i, T j)
        where T : INumber<T> => T.Zero switch
        {
            _ when T.Zero is char or sbyte or byte or short or ushort => float.Abs(Magnitude<T, float>(i, j) - 1f) < float.Epsilon,
            _ when T.Zero is int or uint or long or ulong or Int128 or UInt128 or nint or nuint or BigInteger => double.Abs(Magnitude<T, double>(i, j) - 1d) < double.Epsilon,
            _ when (i, j) is (float a, float b) => float.Abs(Magnitude(a, b) - 1f) < float.Epsilon,
            _ when (i, j) is (double a, double b) => double.Abs(Magnitude(a, b) - 1d) < double.Epsilon,
            _ when (i, j) is (decimal a, decimal b) => double.Abs(Magnitude<decimal, double>(a, b) - 1d) < double.Epsilon,
            _ when (i, j) is (Complex a, Complex b) => double.Abs(Magnitude(a, b) - 1d) < double.Epsilon,
            _ => throw new NotSupportedException($"Type {typeof(T)} is not supported")
        };

    /// <summary>
    /// The is unit vector.
    /// </summary>
    /// <param name="i">The i1.</param>
    /// <param name="j">The j1.</param>
    /// <param name="k">The k1.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    /// <acknowledgment>
    /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsUnitVector<T>(T i, T j, T k) where T : IFloatingPointIeee754<T> => T.Abs(Magnitude(i, j, k) - T.One) < T.Epsilon;

    /// <summary>
    /// The is unit vector.
    /// </summary>
    /// <param name="i">The i1.</param>
    /// <param name="j">The j1.</param>
    /// <param name="k">The k1.</param>
    /// <param name="l">The l.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    /// <acknowledgment>
    /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsUnitVector<T>(T i, T j, T k, T l) where T : IFloatingPointIeee754<T> => T.Abs(Magnitude(i, j, k, l) - T.One) < T.Epsilon;

    /// <summary>
    /// The is unit vector.
    /// </summary>
    /// <param name="i">The i1.</param>
    /// <param name="j">The j1.</param>
    /// <param name="k">The k1.</param>
    /// <param name="l">The l.</param>
    /// <param name="m">The m.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    /// <acknowledgment>
    /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsUnitVector<T>(T i, T j, T k, T l, T m) where T : IFloatingPointIeee754<T> => T.Abs(Magnitude(i, j, k, l, m) - T.One) < T.Epsilon;

    /// <summary>
    /// The is unit vector.
    /// </summary>
    /// <param name="i">The i1.</param>
    /// <param name="j">The j1.</param>
    /// <param name="k">The k1.</param>
    /// <param name="l">The l.</param>
    /// <param name="m">The m.</param>
    /// <param name="n">The n.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    /// <acknowledgment>
    /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsUnitVector<T>(T i, T j, T k, T l, T m, T n) where T : IFloatingPointIeee754<T> => T.Abs(Magnitude(i, j, k, l, m, n) - T.One) < T.Epsilon;
    #endregion

    #region Add Value To Vector
    /// <summary>
    /// Adds the vector uniformly.
    /// </summary>
    /// <param name="augendA">The augend a.</param>
    /// <param name="augendB">The augend b.</param>
    /// <param name="addend">The addend.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B) AddVectorUniform<T>(T augendA, T augendB, T addend) where T : INumberBase<T> => (augendA + addend, augendB + addend);

    /// <summary>
    /// Adds the vector uniformly.
    /// </summary>
    /// <param name="augendA">The augend a.</param>
    /// <param name="augendB">The augend b.</param>
    /// <param name="augendC">The augend c.</param>
    /// <param name="addend">The addend.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C) AddVectorUniform<T>(T augendA, T augendB, T augendC, T addend) where T : INumberBase<T> => (augendA + addend, augendB + addend, augendC + addend);

    /// <summary>
    /// Adds the vector uniformly.
    /// </summary>
    /// <param name="augendA">The augend a.</param>
    /// <param name="augendB">The augend b.</param>
    /// <param name="augendC">The augend c.</param>
    /// <param name="augendD">The augend d.</param>
    /// <param name="addend">The addend.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D) AddVectorUniform<T>(T augendA, T augendB, T augendC, T augendD, T addend) where T : INumberBase<T> => (augendA + addend, augendB + addend, augendC + addend, augendD + addend);

    /// <summary>
    /// Adds the vector uniformly.
    /// </summary>
    /// <param name="augendA">The augend a.</param>
    /// <param name="augendB">The augend b.</param>
    /// <param name="augendC">The augend c.</param>
    /// <param name="augendD">The augend d.</param>
    /// <param name="augendE">The augend e.</param>
    /// <param name="addend">The addend.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D, T E) AddVectorUniform<T>(T augendA, T augendB, T augendC, T augendD, T augendE, T addend) where T : INumberBase<T> => (augendA + addend, augendB + addend, augendC + addend, augendD + addend, augendE + addend);

    /// <summary>
    /// Adds the vector uniformly.
    /// </summary>
    /// <param name="augendA">The augend a.</param>
    /// <param name="augendB">The augend b.</param>
    /// <param name="augendC">The augend c.</param>
    /// <param name="augendD">The augend d.</param>
    /// <param name="augendE">The augend e.</param>
    /// <param name="augendF">The augend f.</param>
    /// <param name="addend">The addend.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D, T E, T F) AddVectorUniform<T>(T augendA, T augendB, T augendC, T augendD, T augendE, T augendF, T addend) where T : INumberBase<T> => (augendA + addend, augendB + addend, augendC + addend, augendD + addend, augendE + addend, augendF + addend);
    #endregion Add Value To Vector

    #region Add Two Vectors
    /// <summary>
    /// Adds the vectors.
    /// </summary>
    /// <param name="augendA">The augend a.</param>
    /// <param name="augendB">The augend b.</param>
    /// <param name="addendA">The addend a.</param>
    /// <param name="addendB">The addend b.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B) AddVectors<T>(T augendA, T augendB, T addendA, T addendB) where T : INumberBase<T> => (augendA + addendA, augendB + addendB);

    /// <summary>
    /// Adds the vectors.
    /// </summary>
    /// <param name="augendA">The augend a.</param>
    /// <param name="augendB">The augend b.</param>
    /// <param name="augendC">The augend c.</param>
    /// <param name="addendA">The addend a.</param>
    /// <param name="addendB">The addend b.</param>
    /// <param name="addendC">The addend c.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C) AddVectors<T>(T augendA, T augendB, T augendC, T addendA, T addendB, T addendC) where T : INumberBase<T> => (augendA + addendA, augendB + addendB, augendC + addendC);

    /// <summary>
    /// Adds the vectors.
    /// </summary>
    /// <param name="augendA">The augend a.</param>
    /// <param name="augendB">The augend b.</param>
    /// <param name="augendC">The augend c.</param>
    /// <param name="augendD">The augend d.</param>
    /// <param name="addendA">The addend a.</param>
    /// <param name="addendB">The addend b.</param>
    /// <param name="addendC">The addend c.</param>
    /// <param name="addendD">The addend d.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D) AddVectors<T>(T augendA, T augendB, T augendC, T augendD, T addendA, T addendB, T addendC, T addendD) where T : INumberBase<T> => (augendA + addendA, augendB + addendB, augendC + addendC, augendD + addendD);

    /// <summary>
    /// Adds the vectors.
    /// </summary>
    /// <param name="augendA">The augend a.</param>
    /// <param name="augendB">The augend b.</param>
    /// <param name="augendC">The augend c.</param>
    /// <param name="augendD">The augend d.</param>
    /// <param name="augendE">The augend e.</param>
    /// <param name="addendA">The addend a.</param>
    /// <param name="addendB">The addend b.</param>
    /// <param name="addendC">The addend c.</param>
    /// <param name="addendD">The addend d.</param>
    /// <param name="addendE">The addend e.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D, T E) AddVectors<T>(T augendA, T augendB, T augendC, T augendD, T augendE, T addendA, T addendB, T addendC, T addendD, T addendE) where T : INumberBase<T> => (augendA + addendA, augendB + addendB, augendC + addendC, augendD + addendD, augendE + addendE);

    /// <summary>
    /// Adds the vectors.
    /// </summary>
    /// <param name="augendA">The augend a.</param>
    /// <param name="augendB">The augend b.</param>
    /// <param name="augendC">The augend c.</param>
    /// <param name="augendD">The augend d.</param>
    /// <param name="augendE">The augend e.</param>
    /// <param name="augendF">The augend f.</param>
    /// <param name="addendA">The addend a.</param>
    /// <param name="addendB">The addend b.</param>
    /// <param name="addendC">The addend c.</param>
    /// <param name="addendD">The addend d.</param>
    /// <param name="addendE">The addend e.</param>
    /// <param name="addendF">The addend f.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D, T E, T F) AddVectors<T>(T augendA, T augendB, T augendC, T augendD, T augendE, T augendF, T addendA, T addendB, T addendC, T addendD, T addendE, T addendF) where T: INumberBase<T> => (augendA + addendA, augendB + addendB, augendC + addendC, augendD + addendD, augendE + addendE, augendF + addendF);
    #endregion Add Two Vectors

    #region Subtract Value From Vector
    /// <summary>
    /// Subtracts the vector uniformly.
    /// </summary>
    /// <param name="minuendA">The minuend a.</param>
    /// <param name="minuendB">The minuend b.</param>
    /// <param name="subend">The subend.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B) SubtractVectorUniform<T>(T minuendA, T minuendB, T subend) where T : INumberBase<T> => (minuendA - subend, minuendB - subend);

    /// <summary>
    /// Subtracts the vector uniformly.
    /// </summary>
    /// <param name="minuendA">The minuend a.</param>
    /// <param name="minuendB">The minuend b.</param>
    /// <param name="minuendC">The minuend c.</param>
    /// <param name="subend">The subend.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C) SubtractVectorUniform<T>(T minuendA, T minuendB, T minuendC, T subend) where T : INumberBase<T> => (minuendA - subend, minuendB - subend, minuendC - subend);

    /// <summary>
    /// Subtracts the vector uniformly.
    /// </summary>
    /// <param name="minuendA">The minuend a.</param>
    /// <param name="minuendB">The minuend b.</param>
    /// <param name="minuendC">The minuend c.</param>
    /// <param name="minuendD">The minuend d.</param>
    /// <param name="subend">The subend.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D) SubtractVectorUniform<T>(T minuendA, T minuendB, T minuendC, T minuendD, T subend) where T : INumberBase<T> => (minuendA - subend, minuendB - subend, minuendC - subend, minuendD - subend);

    /// <summary>
    /// Subtracts the vector uniformly.
    /// </summary>
    /// <param name="minuendA">The minuend a.</param>
    /// <param name="minuendB">The minuend b.</param>
    /// <param name="minuendC">The minuend c.</param>
    /// <param name="minuendD">The minuend d.</param>
    /// <param name="minuendE">The minuend e.</param>
    /// <param name="subend">The subend.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D, T E) SubtractVectorUniform<T>(T minuendA, T minuendB, T minuendC, T minuendD, T minuendE, T subend) where T : INumberBase<T> => (minuendA - subend, minuendB - subend, minuendC - subend, minuendD - subend, minuendE - subend);

    /// <summary>
    /// Subtracts the vector uniformly.
    /// </summary>
    /// <param name="minuendA">The minuend a.</param>
    /// <param name="minuendB">The minuend b.</param>
    /// <param name="minuendC">The minuend c.</param>
    /// <param name="minuendD">The minuend d.</param>
    /// <param name="minuendE">The minuend e.</param>
    /// <param name="minuendF">The minuend f.</param>
    /// <param name="subend">The subend.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D, T E, T F) SubtractVectorUniform<T>(T minuendA, T minuendB, T minuendC, T minuendD, T minuendE, T minuendF, T subend) where T : INumberBase<T> => (minuendA - subend, minuendB - subend, minuendC - subend, minuendD - subend, minuendE - subend, minuendF - subend);
    #endregion Subtract Value From Vector

    #region Subtract Vector From Value
    /// <summary>
    /// Subtracts from minuend2 d.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subendA">The subend a.</param>
    /// <param name="subendB">The subend b.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B) SubtractFromMinuend<T>(T minuend, T subendA, T subendB) where T : INumber<T> => (minuend - subendA, minuend - subendB);

    /// <summary>
    /// Subtracts from minuend3 d.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subendA">The subend a.</param>
    /// <param name="subendB">The subend b.</param>
    /// <param name="subendC">The subend c.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C) SubtractFromMinuend<T>(T minuend, T subendA, T subendB, T subendC) where T : INumber<T> => (minuend - subendA, minuend - subendB, minuend - subendC);

    /// <summary>
    /// Subtracts from minuend4 d.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subendA">The subend a.</param>
    /// <param name="subendB">The subend b.</param>
    /// <param name="subendC">The subend c.</param>
    /// <param name="subendD">The subend d.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D) SubtractFromMinuend<T>(T minuend, T subendA, T subendB, T subendC, T subendD) where T : INumber<T> => (minuend - subendA, minuend - subendB, minuend - subendC, minuend - subendD);

    /// <summary>
    /// Subtracts from minuend5 d.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subendA">The subend a.</param>
    /// <param name="subendB">The subend b.</param>
    /// <param name="subendC">The subend c.</param>
    /// <param name="subendD">The subend d.</param>
    /// <param name="subendE">The subend e.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D, T E) SubtractFromMinuend<T>(T minuend, T subendA, T subendB, T subendC, T subendD, T subendE) where T : INumber<T> => (minuend - subendA, minuend - subendB, minuend - subendC, minuend - subendD, minuend - subendE);

    /// <summary>
    /// Subtracts from minuend6 d.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subendA">The subend a.</param>
    /// <param name="subendB">The subend b.</param>
    /// <param name="subendC">The subend c.</param>
    /// <param name="subendD">The subend d.</param>
    /// <param name="subendE">The subend e.</param>
    /// <param name="subendF">The subend f.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D, T E, T F) SubtractFromMinuend<T>(T minuend, T subendA, T subendB, T subendC, T subendD, T subendE, T subendF) where T : INumber<T> => (minuend - subendA, minuend - subendB, minuend - subendC, minuend - subendD, minuend - subendE, minuend - subendF);
    #endregion Subtract Vector From Value

    #region Subtract Two Vectors
    /// <summary>
    /// Subtracts the specified minuend.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subend">The subend.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y) Subtract<T>((T X, T Y) minuend, (T X, T Y) subend) where T : INumberBase<T> => SubtractVector(minuend.X, minuend.Y, subend.X, subend.Y);

    /// <summary>
    /// Subtracts the vector.
    /// </summary>
    /// <param name="minuendA">The minuend a.</param>
    /// <param name="minuendB">The minuend b.</param>
    /// <param name="subendA">The subend a.</param>
    /// <param name="subendB">The subend b.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B) SubtractVector<T>(T minuendA, T minuendB, T subendA, T subendB) where T : INumberBase<T> => (minuendA - subendA, minuendB - subendB);

    /// <summary>
    /// Subtracts the vector.
    /// </summary>
    /// <param name="minuendA">The minuend a.</param>
    /// <param name="minuendB">The minuend b.</param>
    /// <param name="minuendC">The minuend c.</param>
    /// <param name="subendA">The subend a.</param>
    /// <param name="subendB">The subend b.</param>
    /// <param name="subendC">The subend c.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C) SubtractVector<T>(T minuendA, T minuendB, T minuendC, T subendA, T subendB, T subendC) where T : INumberBase<T> => (minuendA - subendA, minuendB - subendB, minuendC - subendC);

    /// <summary>
    /// Subtracts the vector.
    /// </summary>
    /// <param name="minuendA">The minuend a.</param>
    /// <param name="minuendB">The minuend b.</param>
    /// <param name="minuendC">The minuend c.</param>
    /// <param name="minuendD">The minuend d.</param>
    /// <param name="subendA">The subend a.</param>
    /// <param name="subendB">The subend b.</param>
    /// <param name="subendC">The subend c.</param>
    /// <param name="subendD">The subend d.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D) SubtractVector<T>(T minuendA, T minuendB, T minuendC, T minuendD, T subendA, T subendB, T subendC, T subendD) where T : INumberBase<T> => (minuendA - subendA, minuendB - subendB, minuendC - subendC, minuendD - subendD);

    /// <summary>
    /// Subtracts the vector.
    /// </summary>
    /// <param name="minuendA">The minuend a.</param>
    /// <param name="minuendB">The minuend b.</param>
    /// <param name="minuendC">The minuend c.</param>
    /// <param name="minuendD">The minuend d.</param>
    /// <param name="minuendE">The minuend e.</param>
    /// <param name="subendA">The subend a.</param>
    /// <param name="subendB">The subend b.</param>
    /// <param name="subendC">The subend c.</param>
    /// <param name="subendD">The subend d.</param>
    /// <param name="subendE">The subend e.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D, T E) SubtractVector<T>(T minuendA, T minuendB, T minuendC, T minuendD, T minuendE, T subendA, T subendB, T subendC, T subendD, T subendE) where T : INumberBase<T> => (minuendA - subendA, minuendB - subendB, minuendC - subendC, minuendD - subendD, minuendE - subendE);

    /// <summary>
    /// Subtracts the vector.
    /// </summary>
    /// <param name="minuendA">The minuend a.</param>
    /// <param name="minuendB">The minuend b.</param>
    /// <param name="minuendC">The minuend c.</param>
    /// <param name="minuendD">The minuend d.</param>
    /// <param name="minuendE">The minuend e.</param>
    /// <param name="minuendF">The minuend f.</param>
    /// <param name="subendA">The subend a.</param>
    /// <param name="subendB">The subend b.</param>
    /// <param name="subendC">The subend c.</param>
    /// <param name="subendD">The subend d.</param>
    /// <param name="subendE">The subend e.</param>
    /// <param name="subendF">The subend f.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D, T E, T F) SubtractVector<T>(T minuendA, T minuendB, T minuendC, T minuendD, T minuendE, T minuendF, T subendA, T subendB, T subendC, T subendD, T subendE, T subendF) where T : INumberBase<T> => (minuendA - subendA, minuendB - subendB, minuendC - subendC, minuendD - subendD, minuendE - subendE, minuendF - subendF);
    #endregion Subtract Two Vectors

    #region Difference Between Two Vectors
    /// <summary>
    /// Finds the Delta of two 2D Vectors.
    /// </summary>
    /// <param name="subendA">The subend a.</param>
    /// <param name="subendB">The subend b.</param>
    /// <param name="minuendA">The minuend a.</param>
    /// <param name="minuendB">The minuend b.</param>
    /// <returns>
    /// Returns the Difference Between PointA and PointB
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B) DeltaVector<T>(T subendA, T subendB, T minuendA, T minuendB) where T : INumberBase<T> => SubtractVector(minuendA, minuendB, subendA, subendB);

    /// <summary>
    /// Finds the Delta of two 3D Vectors.
    /// </summary>
    /// <param name="subendA">The subend a.</param>
    /// <param name="subendB">The subend b.</param>
    /// <param name="subendC">The subend c.</param>
    /// <param name="minuendA">The minuend a.</param>
    /// <param name="minuendB">The minuend b.</param>
    /// <param name="minuendC">The minuend c.</param>
    /// <returns>
    /// Returns the Difference Between PointA and PointB
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C) DeltaVector<T>(T subendA, T subendB, T subendC, T minuendA, T minuendB, T minuendC) where T : INumberBase<T> => SubtractVector(minuendA, minuendB, minuendC, subendA, subendB, subendC);

    /// <summary>
    /// Finds the Delta of two 4D Vectors.
    /// </summary>
    /// <param name="subendA">The subend a.</param>
    /// <param name="subendB">The subend b.</param>
    /// <param name="subendC">The subend c.</param>
    /// <param name="subendD">The subend d.</param>
    /// <param name="minuendA">The minuend a.</param>
    /// <param name="minuendB">The minuend b.</param>
    /// <param name="minuendC">The minuend c.</param>
    /// <param name="minuendD">The minuend d.</param>
    /// <returns>
    /// Returns the Difference Between PointA and PointB
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D) DeltaVector<T>(T subendA, T subendB, T subendC, T subendD, T minuendA, T minuendB, T minuendC, T minuendD) where T : INumberBase<T> => SubtractVector(minuendA, minuendB, minuendC, minuendD, subendA, subendB, subendC, subendD);

    /// <summary>
    /// Finds the Delta of two 5D Vectors.
    /// </summary>
    /// <param name="subendA">The subend a.</param>
    /// <param name="subendB">The subend b.</param>
    /// <param name="subendC">The subend c.</param>
    /// <param name="subendD">The subend d.</param>
    /// <param name="subendE">The subend e.</param>
    /// <param name="minuendA">The minuend a.</param>
    /// <param name="minuendB">The minuend b.</param>
    /// <param name="minuendC">The minuend c.</param>
    /// <param name="minuendD">The minuend d.</param>
    /// <param name="minuendE">The minuend e.</param>
    /// <returns>
    /// Returns the Difference Between PointA and PointB
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D, T E) DeltaVector<T>(T subendA, T subendB, T subendC, T subendD, T subendE, T minuendA, T minuendB, T minuendC, T minuendD, T minuendE) where T : INumberBase<T> => SubtractVector(minuendA, minuendB, minuendC, minuendD, minuendE, subendA, subendB, subendC, subendD, subendE);

    /// <summary>
    /// Finds the Delta of two 6D Vectors.
    /// </summary>
    /// <param name="subendA">The subend a.</param>
    /// <param name="subendB">The subend b.</param>
    /// <param name="subendC">The subend c.</param>
    /// <param name="subendD">The subend d.</param>
    /// <param name="subendE">The subend e.</param>
    /// <param name="subendF">The subend f.</param>
    /// <param name="minuendA">The minuend a.</param>
    /// <param name="minuendB">The minuend b.</param>
    /// <param name="minuendC">The minuend c.</param>
    /// <param name="minuendD">The minuend d.</param>
    /// <param name="minuendE">The minuend e.</param>
    /// <param name="minuendF">The minuend f.</param>
    /// <returns>
    /// Returns the Difference Between PointA and PointB
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D, T E, T F) DeltaVector<T>(T subendA, T subendB, T subendC, T subendD, T subendE, T subendF, T minuendA, T minuendB, T minuendC, T minuendD, T minuendE, T minuendF) where T : INumberBase<T> => SubtractVector(minuendA, minuendB, minuendC, minuendD, minuendE, minuendF, subendA, subendB, subendC, subendD, subendE, subendF);
    #endregion Difference Between Two Vectors

    #region Multiply A Vector by a Scalar Value
    /// <summary>
    /// Scales the vector.
    /// </summary>
    /// <param name="multiplicandA">The multiplicand a.</param>
    /// <param name="multiplicandB">The multiplicand b.</param>
    /// <param name="scalar">The scalar.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B) ScaleVector<T>(T multiplicandA, T multiplicandB, T scalar) where T : INumber<T> => (multiplicandA * scalar, multiplicandB * scalar);

    /// <summary>
    /// Scales the vector.
    /// </summary>
    /// <param name="multiplicandA">The multiplicand a.</param>
    /// <param name="multiplicandB">The multiplicand b.</param>
    /// <param name="multiplicandC">The multiplicand c.</param>
    /// <param name="scalar">The scalar.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C) ScaleVector<T>(T multiplicandA, T multiplicandB, T multiplicandC, T scalar) where T : INumber<T> => (multiplicandA * scalar, multiplicandB * scalar, multiplicandC * scalar);

    /// <summary>
    /// Scales the vector.
    /// </summary>
    /// <param name="multiplicandA">The multiplicand a.</param>
    /// <param name="multiplicandB">The multiplicand b.</param>
    /// <param name="multiplicandC">The multiplicand c.</param>
    /// <param name="multiplicandD">The multiplicand d.</param>
    /// <param name="scalar">The scalar.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D) ScaleVector<T>(T multiplicandA, T multiplicandB, T multiplicandC, T multiplicandD, T scalar) where T : INumber<T> => (multiplicandA * scalar, multiplicandB * scalar, multiplicandC * scalar, multiplicandD * scalar);

    /// <summary>
    /// Scales the vector.
    /// </summary>
    /// <param name="multiplicandA">The multiplicand a.</param>
    /// <param name="multiplicandB">The multiplicand b.</param>
    /// <param name="multiplicandC">The multiplicand c.</param>
    /// <param name="multiplicandD">The multiplicand d.</param>
    /// <param name="multiplicandE">The multiplicand e.</param>
    /// <param name="scalar">The scalar.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D, T E) ScaleVector<T>(T multiplicandA, T multiplicandB, T multiplicandC, T multiplicandD, T multiplicandE, T scalar) where T : INumber<T> => (multiplicandA * scalar, multiplicandB * scalar, multiplicandC * scalar, multiplicandD * scalar, multiplicandE * scalar);

    /// <summary>
    /// Scales the vector.
    /// </summary>
    /// <param name="multiplicandA">The multiplicand a.</param>
    /// <param name="multiplicandB">The multiplicand b.</param>
    /// <param name="multiplicandC">The multiplicand c.</param>
    /// <param name="multiplicandD">The multiplicand d.</param>
    /// <param name="multiplicandE">The multiplicand e.</param>
    /// <param name="multiplicandF">The multiplicand f.</param>
    /// <param name="scalar">The scalar.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D, T E, T F) ScaleVector<T>(T multiplicandA, T multiplicandB, T multiplicandC, T multiplicandD, T multiplicandE, T multiplicandF, T scalar) where T : INumber<T> => (multiplicandA * scalar, multiplicandB * scalar, multiplicandC * scalar, multiplicandD * scalar, multiplicandE * scalar, multiplicandF * scalar);
    #endregion Multiply A Vector by a Value

    #region Multiply Each Vector Component By The One in Another Vector
    /// <summary>
    /// Scales the vector parametrically.
    /// </summary>
    /// <param name="multiplicandA">The multiplicand a.</param>
    /// <param name="multiplicandB">The multiplicand b.</param>
    /// <param name="multiplierA">The multiplier a.</param>
    /// <param name="multiplierB">The multiplier b.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B) ScaleVectorParametric<T>(T multiplicandA, T multiplicandB, T multiplierA, T multiplierB) where T : INumber<T> => (multiplicandA * multiplierA, multiplicandB * multiplierB);

    /// <summary>
    /// Scales the vector parametrically.
    /// </summary>
    /// <param name="multiplicandA">The multiplicand a.</param>
    /// <param name="multiplicandB">The multiplicand b.</param>
    /// <param name="multiplicandC">The multiplicand c.</param>
    /// <param name="multiplierA">The multiplier a.</param>
    /// <param name="multiplierB">The multiplier b.</param>
    /// <param name="multiplierC">The multiplier c.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C) ScaleVectorParametric<T>(T multiplicandA, T multiplicandB, T multiplicandC, T multiplierA, T multiplierB, T multiplierC) where T : INumber<T> => (multiplicandA * multiplierA, multiplicandB * multiplierB, multiplicandC * multiplierC);

    /// <summary>
    /// Scales the vector parametrically.
    /// </summary>
    /// <param name="multiplicandA">The multiplicand a.</param>
    /// <param name="multiplicandB">The multiplicand b.</param>
    /// <param name="multiplicandC">The multiplicand c.</param>
    /// <param name="multiplicandD">The multiplicand d.</param>
    /// <param name="multiplierA">The multiplier a.</param>
    /// <param name="multiplierB">The multiplier b.</param>
    /// <param name="multiplierC">The multiplier c.</param>
    /// <param name="multiplierD">The multiplier d.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D) ScaleVectorParametric<T>(T multiplicandA, T multiplicandB, T multiplicandC, T multiplicandD, T multiplierA, T multiplierB, T multiplierC, T multiplierD) where T : INumber<T> => (multiplicandA * multiplierA, multiplicandB * multiplierB, multiplicandC * multiplierC, multiplicandD * multiplierD);

    /// <summary>
    /// Scales the vector parametrically.
    /// </summary>
    /// <param name="multiplicandA">The multiplicand a.</param>
    /// <param name="multiplicandB">The multiplicand b.</param>
    /// <param name="multiplicandC">The multiplicand c.</param>
    /// <param name="multiplicandD">The multiplicand d.</param>
    /// <param name="multiplicandE">The multiplicand e.</param>
    /// <param name="multiplierA">The multiplier a.</param>
    /// <param name="multiplierB">The multiplier b.</param>
    /// <param name="multiplierC">The multiplier c.</param>
    /// <param name="multiplierD">The multiplier d.</param>
    /// <param name="multiplierE">The multiplier e.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D, T E) ScaleVectorParametric<T>(T multiplicandA, T multiplicandB, T multiplicandC, T multiplicandD, T multiplicandE, T multiplierA, T multiplierB, T multiplierC, T multiplierD, T multiplierE) where T : INumber<T> => (multiplicandA * multiplierA, multiplicandB * multiplierB, multiplicandC * multiplierC, multiplicandD * multiplierD, multiplicandE * multiplierE);

    /// <summary>
    /// Scales the vector parametrically.
    /// </summary>
    /// <param name="multiplicandA">The multiplicand a.</param>
    /// <param name="multiplicandB">The multiplicand b.</param>
    /// <param name="multiplicandC">The multiplicand c.</param>
    /// <param name="multiplicandD">The multiplicand d.</param>
    /// <param name="multiplicandE">The multiplicand e.</param>
    /// <param name="multiplicandF">The multiplicand f.</param>
    /// <param name="multiplierA">The multiplier a.</param>
    /// <param name="multiplierB">The multiplier b.</param>
    /// <param name="multiplierC">The multiplier c.</param>
    /// <param name="multiplierD">The multiplier d.</param>
    /// <param name="multiplierE">The multiplier e.</param>
    /// <param name="multiplierF">The multiplier f.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D, T E, T F) ScaleVectorParametric<T>(T multiplicandA, T multiplicandB, T multiplicandC, T multiplicandD, T multiplicandE, T multiplicandF, T multiplierA, T multiplierB, T multiplierC, T multiplierD, T multiplierE, T multiplierF) where T : INumber<T> => (multiplicandA * multiplierA, multiplicandB * multiplierB, multiplicandC * multiplierC, multiplicandD * multiplierD, multiplicandE * multiplierE, multiplicandF * multiplierF);
    #endregion Multiply Each Vector Component By The One in Another Vector

    #region Divide Vector By Value
    /// <summary>
    /// Divides the vector uniform.
    /// </summary>
    /// <param name="divisorA">The divisor a.</param>
    /// <param name="divisorB">The divisor b.</param>
    /// <param name="dividend">The dividend.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B) DivideVectorUniform<T>(T divisorA, T divisorB, T dividend) where T : INumber<T> => (divisorA / dividend, divisorB / dividend);

    /// <summary>
    /// Divides the vector uniform.
    /// </summary>
    /// <param name="divisorA">The divisor a.</param>
    /// <param name="divisorB">The divisor b.</param>
    /// <param name="divisorC">The divisor c.</param>
    /// <param name="dividend">The dividend.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C) DivideVectorUniform<T>(T divisorA, T divisorB, T divisorC, T dividend) where T : INumber<T> => (divisorA / dividend, divisorB / dividend, divisorC / dividend);

    /// <summary>
    /// Divides the vector uniform.
    /// </summary>
    /// <param name="divisorA">The divisor a.</param>
    /// <param name="divisorB">The divisor b.</param>
    /// <param name="divisorC">The divisor c.</param>
    /// <param name="divisorD">The divisor d.</param>
    /// <param name="dividend">The dividend.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D) DivideVectorUniform<T>(T divisorA, T divisorB, T divisorC, T divisorD, T dividend) where T : INumber<T> => (divisorA / dividend, divisorB / dividend, divisorC / dividend, divisorD / dividend);

    /// <summary>
    /// Divides the vector uniform.
    /// </summary>
    /// <param name="divisorA">The divisor a.</param>
    /// <param name="divisorB">The divisor b.</param>
    /// <param name="divisorC">The divisor c.</param>
    /// <param name="divisorD">The divisor d.</param>
    /// <param name="divisorE">The divisor e.</param>
    /// <param name="dividend">The dividend.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D, T E) DivideVectorUniform<T>(T divisorA, T divisorB, T divisorC, T divisorD, T divisorE, T dividend) where T : INumber<T> => (divisorA / dividend, divisorB / dividend, divisorC / dividend, divisorD / dividend, divisorE / dividend);

    /// <summary>
    /// Divides the vector uniform.
    /// </summary>
    /// <param name="divisorA">The divisor a.</param>
    /// <param name="divisorB">The divisor b.</param>
    /// <param name="divisorC">The divisor c.</param>
    /// <param name="divisorD">The divisor d.</param>
    /// <param name="divisorE">The divisor e.</param>
    /// <param name="divisorF">The divisor f.</param>
    /// <param name="dividend">The dividend.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D, T E, T F) DivideVectorUniform<T>(T divisorA, T divisorB, T divisorC, T divisorD, T divisorE, T divisorF, T dividend) where T : INumber<T> => (divisorA / dividend, divisorB / dividend, divisorC / dividend, divisorD / dividend, divisorE / dividend, divisorF / dividend);
    #endregion Divide Vector By Value

    #region Divide Value into Vector Components
    /// <summary>
    /// Divides the by vector uniformly.
    /// </summary>
    /// <param name="divisor">The divisor.</param>
    /// <param name="dividendA">The dividend a.</param>
    /// <param name="dividendB">The dividend b.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B) DivideByVectorUniform<T>(T divisor, T dividendA, T dividendB) where T : INumber<T> => (divisor / dividendA, divisor / dividendB);

    /// <summary>
    /// Divides the by vector uniformly.
    /// </summary>
    /// <param name="divisor">The divisor.</param>
    /// <param name="dividendA">The dividend a.</param>
    /// <param name="dividendB">The dividend b.</param>
    /// <param name="dividendC">The dividend c.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C) DivideByVectorUniform<T>(T divisor, T dividendA, T dividendB, T dividendC) where T : INumber<T> => (divisor / dividendA, divisor / dividendB, divisor / dividendC);

    /// <summary>
    /// Divides the by vector uniformly.
    /// </summary>
    /// <param name="divisor">The divisor.</param>
    /// <param name="dividendA">The dividend a.</param>
    /// <param name="dividendB">The dividend b.</param>
    /// <param name="dividendC">The dividend c.</param>
    /// <param name="dividendD">The dividend d.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D) DivideByVectorUniform<T>(T divisor, T dividendA, T dividendB, T dividendC, T dividendD) where T : INumber<T> => (divisor / dividendA, divisor / dividendB, divisor / dividendC, divisor / dividendD);

    /// <summary>
    /// Divides the by vector uniformly.
    /// </summary>
    /// <param name="divisor">The divisor.</param>
    /// <param name="dividendA">The dividend a.</param>
    /// <param name="dividendB">The dividend b.</param>
    /// <param name="dividendC">The dividend c.</param>
    /// <param name="dividendD">The dividend d.</param>
    /// <param name="dividendE">The dividend e.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D, T E) DivideByVectorUniform<T>(T divisor, T dividendA, T dividendB, T dividendC, T dividendD, T dividendE) where T : INumber<T> => (divisor / dividendA, divisor / dividendB, divisor / dividendC, divisor / dividendD, divisor / dividendE);

    /// <summary>
    /// Divides the by vector uniformly.
    /// </summary>
    /// <param name="divisor">The divisor.</param>
    /// <param name="dividendA">The dividend a.</param>
    /// <param name="dividendB">The dividend b.</param>
    /// <param name="dividendC">The dividend c.</param>
    /// <param name="dividendD">The dividend d.</param>
    /// <param name="dividendE">The dividend e.</param>
    /// <param name="dividendF">The dividend f.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D, T E, T F) DivideByVectorUniform<T>(T divisor, T dividendA, T dividendB, T dividendC, T dividendD, T dividendE, T dividendF) where T : INumber<T> => (divisor / dividendA, divisor / dividendB, divisor / dividendC, divisor / dividendD, divisor / dividendE, divisor / dividendF);
    #endregion Divide Value into Vector Components

    #region Divide Each Of The Components Of A Vector By The Same Components Of Another Vector
    /// <summary>
    /// Divides the vector parametrically.
    /// </summary>
    /// <param name="divisorA">The divisor a.</param>
    /// <param name="divisorB">The divisor b.</param>
    /// <param name="dividendA">The dividend a.</param>
    /// <param name="dividendB">The dividend b.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B) DivideVectorParametric<T>(T divisorA, T divisorB, T dividendA, T dividendB) where T : INumber<T> => (divisorA / dividendA, divisorB / dividendB);

    /// <summary>
    /// Divides the vector parametrically.
    /// </summary>
    /// <param name="divisorA">The divisor a.</param>
    /// <param name="divisorB">The divisor b.</param>
    /// <param name="divisorC">The divisor c.</param>
    /// <param name="dividendA">The dividend a.</param>
    /// <param name="dividendB">The dividend b.</param>
    /// <param name="dividendC">The dividend c.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C) DivideVectorParametric<T>(T divisorA, T divisorB, T divisorC, T dividendA, T dividendB, T dividendC) where T : INumber<T> => (divisorA / dividendA, divisorB / dividendB, divisorC / dividendC);

    /// <summary>
    /// Divides the vector parametrically.
    /// </summary>
    /// <param name="divisorA">The divisor a.</param>
    /// <param name="divisorB">The divisor b.</param>
    /// <param name="divisorC">The divisor c.</param>
    /// <param name="divisorD">The divisor d.</param>
    /// <param name="dividendA">The dividend a.</param>
    /// <param name="dividendB">The dividend b.</param>
    /// <param name="dividendC">The dividend c.</param>
    /// <param name="dividendD">The dividend d.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D) DivideVectorParametric<T>(T divisorA, T divisorB, T divisorC, T divisorD, T dividendA, T dividendB, T dividendC, T dividendD) where T : INumber<T> => (divisorA / dividendA, divisorB / dividendB, divisorC / dividendC, divisorD / dividendD);

    /// <summary>
    /// Divides the vector parametrically.
    /// </summary>
    /// <param name="divisorA">The divisor a.</param>
    /// <param name="divisorB">The divisor b.</param>
    /// <param name="divisorC">The divisor c.</param>
    /// <param name="divisorD">The divisor d.</param>
    /// <param name="divisorE">The divisor e.</param>
    /// <param name="dividendA">The dividend a.</param>
    /// <param name="dividendB">The dividend b.</param>
    /// <param name="dividendC">The dividend c.</param>
    /// <param name="dividendD">The dividend d.</param>
    /// <param name="dividendE">The dividend e.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D, T E) DivideVectorParametric<T>(T divisorA, T divisorB, T divisorC, T divisorD, T divisorE, T dividendA, T dividendB, T dividendC, T dividendD, T dividendE) where T : INumber<T> => (divisorA / dividendA, divisorB / dividendB, divisorC / dividendC, divisorD / dividendD, divisorE / dividendE);

    /// <summary>
    /// Divides the vector parametrically.
    /// </summary>
    /// <param name="divisorA">The divisor a.</param>
    /// <param name="divisorB">The divisor b.</param>
    /// <param name="divisorC">The divisor c.</param>
    /// <param name="divisorD">The divisor d.</param>
    /// <param name="divisorE">The divisor e.</param>
    /// <param name="divisorF">The divisor f.</param>
    /// <param name="dividendA">The dividend a.</param>
    /// <param name="dividendB">The dividend b.</param>
    /// <param name="dividendC">The dividend c.</param>
    /// <param name="dividendD">The dividend d.</param>
    /// <param name="dividendE">The dividend e.</param>
    /// <param name="dividendF">The dividend f.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T A, T B, T C, T D, T E, T F) DivideVectorParametric<T>(T divisorA, T divisorB, T divisorC, T divisorD, T divisorE, T divisorF, T dividendA, T dividendB, T dividendC, T dividendD, T dividendE, T dividendF) where T : INumber<T> => (divisorA / dividendA, divisorB / dividendB, divisorC / dividendC, divisorD / dividendD, divisorE / dividendE, divisorF / dividendF);
    #endregion Divide Each Of The Components Of A Vector By The Same Components Of Another Vector

    #region Modulus Magnitude
    /// <summary>
    /// The Magnitude of a two dimensional Vector.
    /// </summary>
    /// <param name="z">The z.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double Magnitude(Complex z)
    {
        double a = z.Real;
        double b = z.Imaginary;

        return double.Sqrt((a * a) + b * b);
    }

    /// <summary>
    /// The Magnitude of a two dimensional Vector.
    /// </summary>
    /// <param name="z1">The z1 component of the vector.</param>
    /// <param name="z2">The j component of the vector.</param>
    /// <returns>
    /// The <see cref="double" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double Magnitude(Complex z1, Complex z2)
    {
        Complex difference = z2 - z1;  // Compute z2 - z1

        double magnitude = difference.Magnitude;  // Compute the magnitude |z2 - z1|

        return magnitude;
    }

    /// <summary>
    /// The Magnitude of a two dimensional Vector.
    /// </summary>
    /// <param name="i">The i component of the vector.</param>
    /// <param name="j">The j component of the vector.</param>
    /// <returns>
    /// The <see cref="double" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Magnitude<T>(T i, T j) where T : IRootFunctions<T> => T.Sqrt((i * i) + (j * j));

    /// <summary>
    /// The Magnitude of a two dimensional Vector.
    /// </summary>
    /// <param name="i">The i component of the vector.</param>
    /// <param name="j">The j component of the vector.</param>
    /// <returns>
    /// The <see cref="double" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static U Magnitude<T, U>(T i, T j) where T : INumber<T> where U : IRootFunctions<U> => U.Sqrt(U.CreateSaturating((i * i) + (j * j)));

    /// <summary>
    /// The Magnitude of a three dimensional Vector.
    /// </summary>
    /// <param name="i">The i.</param>
    /// <param name="j">The j.</param>
    /// <param name="k">The k.</param>
    /// <returns>
    /// The <see cref="double" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Magnitude<T>(T i, T j, T k) where T : IRootFunctions<T> => T.Sqrt((i * i) + (j * j) + (k * k));

    /// <summary>
    /// The Magnitude of a three dimensional Vector.
    /// </summary>
    /// <param name="i">The i.</param>
    /// <param name="j">The j.</param>
    /// <param name="k">The k.</param>
    /// <returns>
    /// The <see cref="double" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static U Magnitude<T, U>(T i, T j, T k) where T : INumber<T> where U : IRootFunctions<U> => U.Sqrt(U.CreateSaturating((i * i) + (j * j) + (k * k)));

    /// <summary>
    /// The Magnitude of a four dimensional Vector.
    /// </summary>
    /// <param name="i">The i.</param>
    /// <param name="j">The j.</param>
    /// <param name="k">The k.</param>
    /// <param name="l">The l.</param>
    /// <returns>
    /// The <see cref="double" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Magnitude<T>(T i, T j, T k, T l) where T : IRootFunctions<T> => T.Sqrt((i * i) + (j * j) + (k * k) + (l * l));

    /// <summary>
    /// The Magnitude of a four dimensional Vector.
    /// </summary>
    /// <param name="i">The i.</param>
    /// <param name="j">The j.</param>
    /// <param name="k">The k.</param>
    /// <param name="l">The l.</param>
    /// <returns>
    /// The <see cref="double" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static U Magnitude<T, U>(T i, T j, T k, T l) where T : INumber<T> where U : IRootFunctions<U> => U.Sqrt(U.CreateSaturating((i * i) + (j * j) + (k * k) + (l * l)));

    /// <summary>
    /// The Magnitude of a five dimensional Vector.
    /// </summary>
    /// <param name="i">The i.</param>
    /// <param name="j">The j.</param>
    /// <param name="k">The k.</param>
    /// <param name="l">The l.</param>
    /// <param name="m">The m.</param>
    /// <returns>
    /// The <see cref="double" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Magnitude<T>(T i, T j, T k, T l, T m) where T : IRootFunctions<T> => T.Sqrt((i * i) + (j * j) + (k * k) + (l * l) + (m * m));

    /// <summary>
    /// The Magnitude of a five dimensional Vector.
    /// </summary>
    /// <param name="i">The i.</param>
    /// <param name="j">The j.</param>
    /// <param name="k">The k.</param>
    /// <param name="l">The l.</param>
    /// <param name="m">The m.</param>
    /// <returns>
    /// The <see cref="double" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static U Magnitude<T, U>(T i, T j, T k, T l, T m) where T : INumber<T> where U : IRootFunctions<U> => U.Sqrt(U.CreateSaturating((i * i) + (j * j) + (k * k) + (l * l) + (m * m)));

    /// <summary>
    /// The Magnitude of a six dimensional Vector.
    /// </summary>
    /// <param name="i">The i.</param>
    /// <param name="j">The j.</param>
    /// <param name="k">The k.</param>
    /// <param name="l">The l.</param>
    /// <param name="m">The m.</param>
    /// <param name="n">The n.</param>
    /// <returns>
    /// The <see cref="double" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Magnitude<T>(T i, T j, T k, T l, T m, T n) where T : IRootFunctions<T> => T.Sqrt((i * i) + (j * j) + (k * k) + (l * l) + (m * m) + (n * n));

    /// <summary>
    /// The Magnitude of a six dimensional Vector.
    /// </summary>
    /// <param name="i">The i.</param>
    /// <param name="j">The j.</param>
    /// <param name="k">The k.</param>
    /// <param name="l">The l.</param>
    /// <param name="m">The m.</param>
    /// <param name="n">The n.</param>
    /// <returns>
    /// The <see cref="double" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static U Magnitude<T, U>(T i, T j, T k, T l, T m, T n) where T : INumber<T> where U : IRootFunctions<U> => U.Sqrt(U.CreateSaturating((i * i) + (j * j) + (k * k) + (l * l) + (m * m) + (n * n)));
    #endregion Modulus Magnitude

    #region Invert
    /// <summary>
    /// Inverts the vector parametrically.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y) InvertVectorParametric<T>(T x, T y) where T : IFloatingPointIeee754<T> => (T.One / x, T.One / y);

    /// <summary>
    /// Inverts the vector parametrically.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="z">The z.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z) InvertVectorParametric<T>(T x, T y, T z) where T : IFloatingPointIeee754<T> => (T.One / x, T.One / y, T.One / z);

    /// <summary>
    /// Inverts the vector parametrically.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="z">The z.</param>
    /// <param name="w">The w.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z, T W) InvertVectorParametric<T>(T x, T y, T z, T w) where T : IFloatingPointIeee754<T> => (T.One / x, T.One / y, T.One / z, T.One / w);

    /// <summary>
    /// Inverts the vector parametrically.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="z">The z.</param>
    /// <param name="w">The w.</param>
    /// <param name="v">The v.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z, T W, T V) InvertVectorParametric<T>(T x, T y, T z, T w, T v) where T : IFloatingPointIeee754<T> => (T.One / x, T.One / y, T.One / z, T.One / w, T.One / v);

    /// <summary>
    /// Inverts the vector parametrically.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="z">The z.</param>
    /// <param name="w">The w.</param>
    /// <param name="v">The v.</param>
    /// <param name="u">The u.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z, T W, T V, T U) InvertVectorParametric<T>(T x, T y, T z, T w, T v, T u) where T : IFloatingPointIeee754<T> => (T.One / x, T.One / y, T.One / z, T.One / w, T.One / v, T.One / u);
    #endregion Invert

    #region Linear Interpolate
    /// <summary>
    /// The linear interpolation method.
    /// </summary>
    /// <param name="u0">The u0.</param>
    /// <param name="u1">The u1.</param>
    /// <param name="t">The t.</param>
    /// <returns>The <typeparamref name="T"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Lerp<T>(T u0, T u1, T t) where T : INumber<T> => u0 + ((u1 - u0) * t);

    /// <summary>
    /// The linear interpolation method.
    /// </summary>
    /// <param name="x0">The x0.</param>
    /// <param name="y0">The y0.</param>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="t">The t.</param>
    /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y) Lerp<T>(T x0, T y0, T x1, T y1, T t) where T : INumber<T> => (x0 + ((x1 - x0) * t), y0 + ((y1 - y0) * t));

    /// <summary>
    /// The linear interpolation method.
    /// </summary>
    /// <param name="x0">The x0.</param>
    /// <param name="y0">The y0.</param>
    /// <param name="z0">The z0.</param>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="z1">The z1.</param>
    /// <param name="t">The t.</param>
    /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z) Lerp<T>(T x0, T y0, T z0, T x1, T y1, T z1, T t) where T : INumber<T> => (x0 + ((x1 - x0) * t), y0 + ((y1 - y0) * t), z0 + ((z1 - z0) * t));

    /// <summary>
    /// The linear interpolation method.
    /// </summary>
    /// <param name="x0">The x0.</param>
    /// <param name="y0">The y0.</param>
    /// <param name="z0">The z0.</param>
    /// <param name="w0">The w0.</param>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="z1">The z1.</param>
    /// <param name="w1">The w1.</param>
    /// <param name="t">The t.</param>
    /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z, T W) Lerp<T>(T x0, T y0, T z0, T w0, T x1, T y1, T z1, T w1, T t) where T : INumber<T> => (x0 + ((x1 - x0) * t), y0 + ((y1 - y0) * t), z0 + ((z1 - z0) * t), w0 + ((w1 - w0) * t));

    /// <summary>
    /// The linear interpolation method.
    /// </summary>
    /// <param name="x0">The x0.</param>
    /// <param name="y0">The y0.</param>
    /// <param name="z0">The z0.</param>
    /// <param name="w0">The w0.</param>
    /// <param name="v0">The v0.</param>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="z1">The z1.</param>
    /// <param name="w1">The w1.</param>
    /// <param name="v1">The v1.</param>
    /// <param name="t">The t.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3, T4, T5}" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z, T W, T V) Lerp<T>(T x0, T y0, T z0, T w0, T v0, T x1, T y1, T z1, T w1, T v1, T t) where T : INumber<T> => (x0 + ((x1 - x0) * t), y0 + ((y1 - y0) * t), z0 + ((z1 - z0) * t), w0 + ((w1 - w0) * t), v0 + ((v1 - v0) * t));

    /// <summary>
    /// The linear interpolation method.
    /// </summary>
    /// <param name="x0">The x0.</param>
    /// <param name="y0">The y0.</param>
    /// <param name="z0">The z0.</param>
    /// <param name="w0">The w0.</param>
    /// <param name="v0">The v0.</param>
    /// <param name="u0">The u0.</param>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="z1">The z1.</param>
    /// <param name="w1">The w1.</param>
    /// <param name="v1">The v1.</param>
    /// <param name="u1">The u1.</param>
    /// <param name="t">The t.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z, T W, T V, T U) Lerp<T>(T x0, T y0, T z0, T w0, T v0, T u0, T x1, T y1, T z1, T w1, T v1, T u1, T t) where T : INumber<T> => (x0 + ((x1 - x0) * t), y0 + ((y1 - y0) * t), z0 + ((z1 - z0) * t), w0 + ((w1 - w0) * t), v0 + ((v1 - v0) * t), u0 + ((u1 - u0) * t));
    #endregion Linear Interpolate

    #region Magnitude
    /// <summary>
    /// Calculates the magnitude or length of a vector.
    /// </summary>
    /// <param name="i">The i parameter of the vector.</param>
    /// <param name="j">The j parameter of the vector.</param>
    /// <returns>Returns the magnitude of the vector.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double VectorMagnitude(double i, double j) => Sqrt((i * i) + (j * j));

    /// <summary>
    /// Calculates the magnitude or length of a vector.
    /// </summary>
    /// <param name="i">The i parameter of the vector.</param>
    /// <param name="j">The j parameter of the vector.</param>
    /// <param name="k">The k parameter of the vector.</param>
    /// <returns>Returns the magnitude of the vector.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double VectorMagnitude(double i, double j, double k) => Sqrt((i * i) + (j * j) + (k * k));

    /// <summary>
    /// Calculates the magnitude or length of a vector.
    /// </summary>
    /// <param name="i">The i parameter of the vector.</param>
    /// <param name="j">The j parameter of the vector.</param>
    /// <param name="k">The k parameter of the vector.</param>
    /// <param name="l">The l parameter of the vector.</param>
    /// <returns>Returns the magnitude of the vector.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double VectorMagnitude(double i, double j, double k, double l) => Sqrt((i * i) + (j * j) + (k * k) + (l * l));

    /// <summary>
    /// Calculates the magnitude or length of a vector.
    /// </summary>
    /// <param name="i">The i parameter of the vector.</param>
    /// <param name="j">The j parameter of the vector.</param>
    /// <param name="k">The k parameter of the vector.</param>
    /// <param name="l">The l parameter of the vector.</param>
    /// <param name="m">The m.</param>
    /// <returns>
    /// Returns the magnitude of the vector.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double VectorMagnitude(double i, double j, double k, double l, double m) => Sqrt((i * i) + (j * j) + (k * k) + (l * l) + (m * m));

    /// <summary>
    /// Calculates the magnitude or length of a vector.
    /// </summary>
    /// <param name="i">The i parameter of the vector.</param>
    /// <param name="j">The j parameter of the vector.</param>
    /// <param name="k">The k parameter of the vector.</param>
    /// <param name="l">The l parameter of the vector.</param>
    /// <param name="m">The m.</param>
    /// <param name="n">The n.</param>
    /// <returns>
    /// Returns the magnitude of the vector.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double VectorMagnitude(double i, double j, double k, double l, double m, double n) => Sqrt((i * i) + (j * j) + (k * k) + (l * l) + (m * m) + (n * n));
    #endregion

    #region Magnitude Squared
    /// <summary>
    /// Calculates the Magnitude or squared length of a vector.
    /// </summary>
    /// <param name="i">The i parameter.</param>
    /// <param name="j">The j parameter.</param>
    /// <returns>Returns the squared magnitude of the vector.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double VectorMagnitudeSquared(double i, double j) => (i * i) + (j * j);

    /// <summary>
    /// Calculates the Magnitude or squared length of a vector.
    /// </summary>
    /// <param name="i">The i parameter.</param>
    /// <param name="j">The j parameter.</param>
    /// <param name="k">The k parameter.</param>
    /// <returns>Returns the squared magnitude of the vector.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double VectorMagnitudeSquared(double i, double j, double k) => (i * i) + (j * j) + (k * k);

    /// <summary>
    /// Calculates the Magnitude or squared length of a vector.
    /// </summary>
    /// <param name="i">The i parameter.</param>
    /// <param name="j">The j parameter.</param>
    /// <param name="k">The k parameter.</param>
    /// <param name="l">The l parameter.</param>
    /// <returns>Returns the squared magnitude of the vector.</returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double VectorMagnitudeSquared(double i, double j, double k, double l) => (i * i) + (j * j) + (k * k) + (l * l);

    /// <summary>
    /// Calculates the Magnitude or squared length of a vector.
    /// </summary>
    /// <param name="i">The i parameter.</param>
    /// <param name="j">The j parameter.</param>
    /// <param name="k">The k parameter.</param>
    /// <param name="l">The l parameter.</param>
    /// <param name="m">The m.</param>
    /// <returns>
    /// Returns the squared magnitude of the vector.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double VectorMagnitudeSquared(double i, double j, double k, double l, double m) => (i * i) + (j * j) + (k * k) + (l * l) + (m * m);

    /// <summary>
    /// Calculates the Magnitude or squared length of a vector.
    /// </summary>
    /// <param name="i">The i parameter.</param>
    /// <param name="j">The j parameter.</param>
    /// <param name="k">The k parameter.</param>
    /// <param name="l">The l parameter.</param>
    /// <param name="m">The m.</param>
    /// <param name="n">The n.</param>
    /// <returns>
    /// Returns the squared magnitude of the vector.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double VectorMagnitudeSquared(double i, double j, double k, double l, double m, double n) => (i * i) + (j * j) + (k * k) + (l * l) + (m * m) + (n * n);
    #endregion

    #region Dot Product
    /// <summary>
    /// Calculates the dot Aka. scalar or inner product of a vector.
    /// </summary>
    /// <param name="tuple">X, Y, Z components in tuple form.</param>
    /// <param name="x2">Second Point X component.</param>
    /// <param name="y2">Second Point Y component.</param>
    /// <returns>
    /// The Dot Product.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T DotProduct<T>((T X, T Y) tuple, T x2, T y2) where T : INumberBase<T> => DotProduct(tuple.X, tuple.Y, x2, y2);

    /// <summary>
    /// Calculates the dot Aka. scalar or inner product of a vector.
    /// </summary>
    /// <param name="tuple1">First set of X, Y, Z components in tuple form.</param>
    /// <param name="tuple2">Second set of X, Y, Z components in tuple form.</param>
    /// <returns>
    /// The Dot Product.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T DotProduct<T>((T X, T Y) tuple1, (T X, T Y) tuple2) where T : INumberBase<T> => DotProduct(tuple1.X, tuple1.Y, tuple2.X, tuple2.Y);

    /// <summary>
    /// Calculates the dot Aka. scalar or inner product of a vector.
    /// </summary>
    /// <param name="tuple">X, Y, Z components in tuple form.</param>
    /// <param name="x2">Second Point X component.</param>
    /// <param name="y2">Second Point Y component.</param>
    /// <param name="z2">Second Point Z component.</param>
    /// <returns>
    /// The Dot Product.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T DotProduct<T>((T X, T Y, T Z) tuple, T x2, T y2, T z2) where T : INumberBase<T> => DotProduct(tuple.X, tuple.Y, tuple.Z, x2, y2, z2);

    /// <summary>
    /// Calculates the dot Aka. scalar or inner product of a vector.
    /// </summary>
    /// <param name="tuple1">First set of X, Y, Z components in tuple form.</param>
    /// <param name="tuple2">Second set of X, Y, Z components in tuple form.</param>
    /// <returns>
    /// The Dot Product.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T DotProduct<T>((T X, T Y, T Z) tuple1, (T X, T Y, T Z) tuple2) where T : INumberBase<T> => DotProduct(tuple1.X, tuple1.Y, tuple1.Z, tuple2.X, tuple2.Y, tuple2.Z);

    /// <summary>
    /// Calculates the dot Aka. scalar or inner product of a vector.
    /// </summary>
    /// <param name="tuple">X, Y, Z components in tuple form.</param>
    /// <param name="x2">Second Point X component.</param>
    /// <param name="y2">Second Point Y component.</param>
    /// <param name="z2">Second Point Z component.</param>
    /// <param name="w2">The w2.</param>
    /// <returns>
    /// The Dot Product.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T DotProduct<T>((T X, T Y, T Z, T W) tuple, T x2, T y2, T z2, T w2) where T : INumberBase<T> => DotProduct(tuple.X, tuple.Y, tuple.Z, tuple.W, x2, y2, z2, w2);

    /// <summary>
    /// Calculates the dot Aka. scalar or inner product of a vector.
    /// </summary>
    /// <param name="tuple1">First set of X, Y, Z components in tuple form.</param>
    /// <param name="tuple2">Second set of X, Y, Z components in tuple form.</param>
    /// <returns>
    /// The Dot Product.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T DotProduct<T>((T X, T Y, T Z, T W) tuple1, (T X, T Y, T Z, T W) tuple2) where T : INumberBase<T> => DotProduct(tuple1.X, tuple1.Y, tuple1.Z, tuple1.W, tuple2.X, tuple2.Y, tuple2.Z, tuple2.W);

    /// <summary>
    /// Calculates the dot Aka. scalar or inner product of a vector.
    /// </summary>
    /// <param name="tuple">X, Y, Z components in tuple form.</param>
    /// <param name="x2">Second Point X component.</param>
    /// <param name="y2">Second Point Y component.</param>
    /// <param name="z2">Second Point Z component.</param>
    /// <param name="w2">The w2.</param>
    /// <param name="v2">The v2.</param>
    /// <returns>
    /// The Dot Product.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T DotProduct<T>((T X, T Y, T Z, T W, T V) tuple, T x2, T y2, T z2, T w2, T v2) where T : INumberBase<T> => DotProduct(tuple.X, tuple.Y, tuple.Z, tuple.W, tuple.V, x2, y2, z2, w2, v2);

    /// <summary>
    /// Calculates the dot Aka. scalar or inner product of a vector.
    /// </summary>
    /// <param name="tuple1">First set of X, Y, Z components in tuple form.</param>
    /// <param name="tuple2">Second set of X, Y, Z components in tuple form.</param>
    /// <returns>
    /// The Dot Product.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T DotProduct<T>((T X, T Y, T Z, T W, T V) tuple1, (T X, T Y, T Z, T W, T V) tuple2) where T : INumberBase<T> => DotProduct(tuple1.X, tuple1.Y, tuple1.Z, tuple1.W, tuple1.V, tuple2.X, tuple2.Y, tuple2.Z, tuple2.W, tuple2.V);

    /// <summary>
    /// Calculates the dot Aka. scalar or inner product of a vector.
    /// </summary>
    /// <param name="x1">First Point X component.</param>
    /// <param name="y1">First Point Y component.</param>
    /// <param name="x2">Second Point X component.</param>
    /// <param name="y2">Second Point Y component.</param>
    /// <returns>
    /// The Dot Product.
    /// </returns>
    /// <remarks>
    /// <para>The dot product "·" is calculated with DotProduct = X ^ 2 + Y ^ 2</para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T DotProduct<T>(T x1, T y1, T x2, T y2) where T : INumberBase<T> => (x1 * x2) + (y1 * y2);

    /// <summary>
    /// Calculates the dot Aka. scalar or inner product of a vector.
    /// </summary>
    /// <param name="x1">First Point X component.</param>
    /// <param name="y1">First Point Y component.</param>
    /// <param name="z1">First Point Z component.</param>
    /// <param name="x2">Second Point X component.</param>
    /// <param name="y2">Second Point Y component.</param>
    /// <param name="z2">Second Point Z component.</param>
    /// <returns>
    /// The Dot Product.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T DotProduct<T>(T x1, T y1, T z1, T x2, T y2, T z2) where T : INumberBase<T> => (x1 * x2) + (y1 * y2) + (z1 * z2);

    /// <summary>
    /// Calculates the dot Aka. scalar or inner product of a vector.
    /// </summary>
    /// <param name="x1">First Point X component.</param>
    /// <param name="y1">First Point Y component.</param>
    /// <param name="z1">First Point Z component.</param>
    /// <param name="w1">First Point W component.</param>
    /// <param name="x2">Second Point X component.</param>
    /// <param name="y2">Second Point Y component.</param>
    /// <param name="z2">Second Point Z component.</param>
    /// <param name="w2">Second Point W component.</param>
    /// <returns>
    /// The Dot Product.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T DotProduct<T>(T x1, T y1, T z1, T w1, T x2, T y2, T z2, T w2) where T : INumberBase<T> => (x1 * x2) + (y1 * y2) + (z1 * z2) + (w1 * w2);

    /// <summary>
    /// Calculates the dot Aka. scalar or inner product of a vector.
    /// </summary>
    /// <param name="x1">First Point X component.</param>
    /// <param name="y1">First Point Y component.</param>
    /// <param name="z1">First Point Z component.</param>
    /// <param name="w1">First Point W component.</param>
    /// <param name="v1">First Point V component.</param>
    /// <param name="x2">Second Point X component.</param>
    /// <param name="y2">Second Point Y component.</param>
    /// <param name="z2">Second Point Z component.</param>
    /// <param name="w2">Second Point W component.</param>
    /// <param name="v2">Second Point V component.</param>
    /// <returns>
    /// The Dot Product.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T DotProduct<T>(T x1, T y1, T z1, T w1, T v1, T x2, T y2, T z2, T w2, T v2) where T : INumberBase<T> => (x1 * x2) + (y1 * y2) + (z1 * z2) + (w1 * w2) + (v1 * v2);

    /// <summary>
    /// Calculates the dot Aka. scalar or inner product of a vector.
    /// </summary>
    /// <param name="x1">First Point X component.</param>
    /// <param name="y1">First Point Y component.</param>
    /// <param name="z1">First Point Z component.</param>
    /// <param name="w1">First Point W component.</param>
    /// <param name="v1">First Point V component.</param>
    /// <param name="u1">The u1.</param>
    /// <param name="x2">Second Point X component.</param>
    /// <param name="y2">Second Point Y component.</param>
    /// <param name="z2">Second Point Z component.</param>
    /// <param name="w2">Second Point W component.</param>
    /// <param name="v2">Second Point V component.</param>
    /// <param name="u2">The u2.</param>
    /// <returns>
    /// The Dot Product.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T DotProduct<T>(T x1, T y1, T z1, T w1, T v1, T u1, T x2, T y2, T z2, T w2, T v2, T u2) where T : INumberBase<T> => (x1 * x2) + (y1 * y2) + (z1 * z2) + (w1 * w2) + (v1 * v2) + (u1 * u2);
    #endregion Dot Product

    #region Dot Product Triple Vector
    /// <summary>
    /// The Dot Product of the vector of three points
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="x2">The x2.</param>
    /// <param name="y2">The y2.</param>
    /// <param name="x3">The x3.</param>
    /// <param name="y3">The y3.</param>
    /// <returns>
    /// Return the dot product AB · BC.
    /// </returns>
    /// <remarks>
    /// <para>Note that AB · BC = |AB| * |BC| * Cos(theta).</para>
    /// </remarks>
    /// <acknowledgment>
    /// http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T DotProductTriple<T>(T x1, T y1, T x2, T y2, T x3, T y3) where T : INumberBase<T> => ((x1 - x2) * (x3 - x2)) + ((y1 - y2) * (y3 - y2));

    /// <summary>
    /// The Dot Product of the vector of three points
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="z1">The z1.</param>
    /// <param name="x2">The x2.</param>
    /// <param name="y2">The y2.</param>
    /// <param name="z2">The z2.</param>
    /// <param name="x3">The x3.</param>
    /// <param name="y3">The y3.</param>
    /// <param name="z3">The z3.</param>
    /// <returns>
    /// Return the dot product AB · BC.
    /// </returns>
    /// <remarks>
    /// <para>Note that AB · BC = |AB| * |BC| * Cos(theta).</para>
    /// </remarks>
    /// <acknowledgment>
    /// http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T DotProductTriple<T>(T x1, T y1, T z1, T x2, T y2, T z2, T x3, T y3, T z3) where T : INumberBase<T> => ((x1 - x2) * (x3 - x2)) + ((y1 - y2) * (y3 - y2)) + ((z1 - z2) * (z3 - z2));

    /// <summary>
    /// The Dot Product of the vector of three points
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="z1">The z1.</param>
    /// <param name="w1">The w1.</param>
    /// <param name="x2">The x2.</param>
    /// <param name="y2">The y2.</param>
    /// <param name="z2">The z2.</param>
    /// <param name="w2">The w2.</param>
    /// <param name="x3">The x3.</param>
    /// <param name="y3">The y3.</param>
    /// <param name="z3">The z3.</param>
    /// <param name="w3">The w3.</param>
    /// <returns>
    /// Return the dot product AB · BC.
    /// </returns>
    /// <remarks>
    /// <para>Note that AB · BC = |AB| * |BC| * Cos(theta).</para>
    /// </remarks>
    /// <acknowledgment>
    /// http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T DotProductTriple<T>(T x1, T y1, T z1, T w1, T x2, T y2, T z2, T w2, T x3, T y3, T z3, T w3) where T : INumberBase<T> => ((x1 - x2) * (x3 - x2)) + ((y1 - y2) * (y3 - y2)) + ((z1 - z2) * (z3 - z2)) + ((w1 - w2) * (w3 - w2));

    /// <summary>
    /// The Dot Product of the vector of three points
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="z1">The z1.</param>
    /// <param name="w1">The w1.</param>
    /// <param name="v1">The v1.</param>
    /// <param name="x2">The x2.</param>
    /// <param name="y2">The y2.</param>
    /// <param name="z2">The z2.</param>
    /// <param name="w2">The w2.</param>
    /// <param name="v2">The v2.</param>
    /// <param name="x3">The x3.</param>
    /// <param name="y3">The y3.</param>
    /// <param name="z3">The z3.</param>
    /// <param name="w3">The w3.</param>
    /// <param name="v3">The v3.</param>
    /// <returns>
    /// Return the dot product AB · BC.
    /// </returns>
    /// <remarks>
    /// <para>Note that AB · BC = |AB| * |BC| * Cos(theta).</para>
    /// </remarks>
    /// <acknowledgment>
    /// http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T DotProductTriple<T>(T x1, T y1, T z1, T w1, T v1, T x2, T y2, T z2, T w2, T v2, T x3, T y3, T z3, T w3, T v3) where T : INumberBase<T> => ((x1 - x2) * (x3 - x2)) + ((y1 - y2) * (y3 - y2)) + ((z1 - z2) * (z3 - z2)) + ((w1 - w2) * (w3 - w2)) + ((v1 - v2) * (v3 - v2));

    /// <summary>
    /// The Dot Product of the vector of three points
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="z1">The z1.</param>
    /// <param name="w1">The w1.</param>
    /// <param name="v1">The v1.</param>
    /// <param name="u1">The u1.</param>
    /// <param name="x2">The x2.</param>
    /// <param name="y2">The y2.</param>
    /// <param name="z2">The z2.</param>
    /// <param name="w2">The w2.</param>
    /// <param name="v2">The v2.</param>
    /// <param name="u2">The u2.</param>
    /// <param name="x3">The x3.</param>
    /// <param name="y3">The y3.</param>
    /// <param name="z3">The z3.</param>
    /// <param name="w3">The w3.</param>
    /// <param name="v3">The v3.</param>
    /// <param name="u3">The u3.</param>
    /// <returns>
    /// Return the dot product AB · BC.
    /// </returns>
    /// <remarks>
    /// <para>Note that AB · BC = |AB| * |BC| * Cos(theta).</para>
    /// </remarks>
    /// <acknowledgment>
    /// http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T DotProductTriple<T>(T x1, T y1, T z1, T w1, T v1, T u1, T x2, T y2, T z2, T w2, T v2, T u2, T x3, T y3, T z3, T w3, T v3, T u3) where T : INumberBase<T> => ((x1 - x2) * (x3 - x2)) + ((y1 - y2) * (y3 - y2)) + ((z1 - z2) * (z3 - z2)) + ((w1 - w2) * (w3 - w2)) + ((v1 - v2) * (v3 - v2)) + ((u1 - u2) * (u3 - u2));
    #endregion Dot Product Triple Vector

    #region Cross Product
    /// <summary>
    /// Cross Product of two points.
    /// </summary>
    /// <param name="t1">The t1.</param>
    /// <param name="t2">The t2.</param>
    /// <returns>
    /// the cross product AB · BC.
    /// </returns>
    /// <remarks>
    /// <para>Note that AB · BC = |AB| * |BC| * Cos(theta).</para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T CrossProduct<T>((T x, T y) t1, (T x, T y) t2) where T : INumberBase<T> => CrossProduct(t1.x, t1.y, t2.x, t2.y);

    /// <summary>
    /// The cross product.
    /// </summary>
    /// <param name="t1">The t1.</param>
    /// <param name="t2">The t2.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3}" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z) CrossProduct<T>((T x, T y, T z) t1, (T x, T y, T z) t2) where T : INumberBase<T> => CrossProduct(t1.x, t1.y, t1.z, t2.x, t2.y, t2.z);

    /// <summary>
    /// Cross Product of two points.
    /// </summary>
    /// <param name="x1">First Point X component.</param>
    /// <param name="y1">First Point Y component.</param>
    /// <param name="x2">Second Point X component.</param>
    /// <param name="y2">Second Point Y component.</param>
    /// <returns>the cross product AB · BC.</returns>
    /// <remarks><para>Note that AB · BC = |AB| * |BC| * Cos(theta).</para></remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T CrossProduct<T>(T x1, T y1, T x2, T y2) where T : INumberBase<T> => (x1 * y2) - (y1 * x2);

    /// <summary>
    /// The cross product.
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="z1">The z1.</param>
    /// <param name="x2">The x2.</param>
    /// <param name="y2">The y2.</param>
    /// <param name="z2">The z2.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3}" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z) CrossProduct<T>(T x1, T y1, T z1, T x2, T y2, T z2) where T : INumberBase<T> => (X: (y1 * z2) - (z1 * y2), Y: (z1 * x2) - (x1 * z2), Z: (x1 * y2) - (y1 * x2));
    #endregion Cross Product

    #region Cross Product Triple
    /// <summary>
    /// The cross product vector.
    /// The cross product is a vector perpendicular to AB
    /// and BC having length |AB| * |BC| * Sin(theta) and
    /// with direction given by the right-hand rule.
    /// For two vectors in the X-Y plane, the result is a
    /// vector with X and Y components 0 so the Z component
    /// gives the vector's length and direction.
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="x2">The x2.</param>
    /// <param name="y2">The y2.</param>
    /// <param name="x3">The x3.</param>
    /// <param name="y3">The y3.</param>
    /// <returns>Return the cross product AB x BC. The <see cref="double"/>.</returns>
    /// <acknowledgment>
    /// http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T CrossProductTriple<T>(T x1, T y1, T x2, T y2, T x3, T y3) where T : INumberBase<T> => ((x1 - x2) * (y3 - y2)) - ((y1 - y2) * (x3 - x2));

    /// <summary>
    /// Cross4 computes the four-dimensional cross product of the three vectors U, V and W, in that order. It returns the resulting four-vector.
    /// https://web.archive.org/web/20040213224251/http://research.microsoft.com/~hollasch/thesis/chapter2.html
    /// </summary>
    /// <param name="uI">The u i.</param>
    /// <param name="uJ">The u j.</param>
    /// <param name="uK">The u k.</param>
    /// <param name="uL">The u l.</param>
    /// <param name="vI">The v i.</param>
    /// <param name="vJ">The v j.</param>
    /// <param name="vK">The v k.</param>
    /// <param name="vL">The v l.</param>
    /// <param name="wI">The w i.</param>
    /// <param name="wJ">The w j.</param>
    /// <param name="wK">The w k.</param>
    /// <param name="wL">The w l.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z, T W)
        CrossProductTriple<T>(
        T uI, T uJ, T uK, T uL,
        T vI, T vJ, T vK, T vL,
        T wI, T wJ, T wK, T wL)
        where T : INumberBase<T>
    {
        // Calculate intermediate values.
        var a = (vI * wJ) - (vJ * wI);
        var b = (vI * wK) - (vK * wI);
        var c = (vI * wL) - (vL * wI);
        var d = (vJ * wK) - (vK * wJ);
        var e = (vJ * wL) - (vL * wJ);
        var f = (vK * wL) - (vL * wK);

        // Calculate the result-vector components.
        return (
            (uJ * f) - (uK * e) + (uL * d),
            -(uI * f) + (uK * c) - (uL * b),
            (uI * e) - (uJ * c) + (uL * a),
            -(uI * d) + (uJ * b) - (uK * a)
         );
    }
    #endregion Cross Product Triple

    #region Complex Product
    /// <summary>
    /// The complex product.
    /// </summary>
    /// <param name="x0">The x0.</param>
    /// <param name="y0">The y0.</param>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2}" />.
    /// </returns>
    /// <acknowledgment>
    /// http://stackoverflow.com/questions/1476497/multiply-two-point-objects
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y) ComplexProduct<T>(
        T x0, T y0,
        T x1, T y1)
        where T : INumberBase<T>
        => (
            (x0 * x1) - (y0 * y1),
            (x0 * y1) + (y0 * x1)
            );
    #endregion Complex Product

    #region Mixed Product
    /// <summary>
    /// The mixed product.
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="z1">The z1.</param>
    /// <param name="x2">The x2.</param>
    /// <param name="y2">The y2.</param>
    /// <param name="z2">The z2.</param>
    /// <param name="x3">The x3.</param>
    /// <param name="y3">The y3.</param>
    /// <param name="z3">The z3.</param>
    /// <returns>
    /// The <see cref="double" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T MixedProduct<T>(T x1, T y1, T z1, T x2, T y2, T z2, T x3, T y3, T z3) where T : INumberBase<T> => DotProduct(CrossProduct(x1, y1, z1, x2, y2, z2), x3, y3, z3);
    #endregion Mixed Product

    #region Normalize Vector
    /// <summary>
    /// Normalize a Vector.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y) Normalize<T>((T i, T j) tuple) where T : INumber<T> => Normalize(tuple.i, tuple.j);

    /// <summary>
    /// Normalize a Vector.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z) Normalize<T>((T i, T j, T k) tuple) where T : INumber<T> => Normalize(tuple.i, tuple.j, tuple.k);

    /// <summary>
    /// Normalize a Vector.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z, T W) Normalize<T>((T i, T j, T k, T l) tuple) where T : INumber<T> => Normalize(tuple.i, tuple.j, tuple.k, tuple.l);

    /// <summary>
    /// Normalize a Vector.
    /// </summary>
    /// <param name="i">The i.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Normalize<T>(T i) where T : INumber<T> => i / T.CreateChecked(Sqrt(Cast<T, double>(i * i)));

    /// <summary>
    /// Normalize a Vector.
    /// </summary>
    /// <param name="i">The i.</param>
    /// <param name="j">The j.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y) Normalize<T>(T i, T j) where T : INumber<T> => (i / T.CreateChecked(Sqrt(Cast<T, double>((i * i) + (j * j)))), j / T.CreateChecked(Sqrt(Cast<T, double>((i * i) + (j * j)))));

    /// <summary>
    /// Normalize a Vector.
    /// </summary>
    /// <param name="i">The i.</param>
    /// <param name="j">The j.</param>
    /// <param name="k">The k.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z) Normalize<T>(T i, T j, T k) where T : INumber<T> => (i / T.CreateChecked(Sqrt(Cast<T, double>((i * i) + (j * j) + (k * k)))), j / T.CreateChecked(Sqrt(Cast<T, double>((i * i) + (j * j) + (k * k)))), k / T.CreateChecked(Sqrt(Cast<T, double>((i * i) + (j * j) + (k * k)))));

    /// <summary>
    /// Normalize a Vector.
    /// </summary>
    /// <param name="i">The i.</param>
    /// <param name="j">The j.</param>
    /// <param name="k">The k.</param>
    /// <param name="l">The l.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z, T W) Normalize<T>(T i, T j, T k, T l) where T : INumber<T> => (i / T.CreateChecked(Sqrt(Cast<T, double>((i * i) + (j * j) + (k * k) + (l * l)))), j / T.CreateChecked(Sqrt(Cast<T, double>((i * i) + (j * j) + (k * k) + (l * l)))), k / T.CreateChecked(Sqrt(Cast<T, double>((i * i) + (j * j) + (k * k) + (l * l)))), l / T.CreateChecked(Sqrt(Cast<T, double>((i * i) + (j * j) + (k * k) + (l * l)))));
    #endregion Normalize Vector

    #region Normalize Vectors
    /// <summary>
    /// Find the Normal of Two points.
    /// </summary>
    /// <param name="i1">The x component of the first Point.</param>
    /// <param name="j1">The y component of the first Point.</param>
    /// <param name="i2">The x component of the second Point.</param>
    /// <param name="j2">The y component of the second Point.</param>
    /// <returns>
    /// The Normal of two Points
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y) NormalizeVectors<T>(T i1, T j1, T i2, T j2) where T : IFloatingPointIeee754<T> => (i1 / T.Sqrt((i1 * i2) + (j1 * j2)), j1 / T.Sqrt((i1 * i2) + (j1 * j2)));

    /// <summary>
    /// Find the Normal of Two vectors.
    /// </summary>
    /// <param name="i1">The x component of the first Point.</param>
    /// <param name="j1">The y component of the first Point.</param>
    /// <param name="k1">The z component of the second Point.</param>
    /// <param name="i2">The x component of the second Point.</param>
    /// <param name="j2">The y component of the second Point.</param>
    /// <param name="k2">The z component of the second Point.</param>
    /// <returns>
    /// The Normal of two Points
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z) NormalizeVectors<T>(T i1, T j1, T k1, T i2, T j2, T k2) where T : IFloatingPointIeee754<T> => (i1 / T.Sqrt((i1 * i2) + (j1 * j2) + (k1 * k2)), j1 / T.Sqrt((i1 * i2) + (j1 * j2) + (k1 * k2)), k1 / T.Sqrt((i1 * i2) + (j1 * j2) + (k1 * k2)));

    /// <summary>
    /// Find the Normal of Two vectors.
    /// </summary>
    /// <param name="i1">The x component of the first Point.</param>
    /// <param name="j1">The y component of the first Point.</param>
    /// <param name="k1">The z component of the first Point.</param>
    /// <param name="l1">The l1.</param>
    /// <param name="i2">The x component of the second Point.</param>
    /// <param name="j2">The y component of the second Point.</param>
    /// <param name="k2">The z component of the second Point.</param>
    /// <param name="l2">The l2.</param>
    /// <returns>
    /// The Normal of two Points
    /// </returns>
    /// <acknowledgment>
    /// http://www.fundza.com/vectors/normalize/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z, T W) NormalizeVectors<T>(T i1, T j1, T k1, T l1, T i2, T j2, T k2, T l2) where T : IFloatingPointIeee754<T> => (i1 / T.Sqrt((i1 * i2) + (j1 * j2) + (k1 * k2) + (l1 * l2)), j1 / T.Sqrt((i1 * i2) + (j1 * j2) + (k1 * k2) + (l1 * l2)), k1 / T.Sqrt((i1 * i2) + (j1 * j2) + (k1 * k2) + (l1 * l2)), l1 / T.Sqrt((i1 * i2) + (j1 * j2) + (k1 * k2) + (l1 * l2)));
    #endregion Normalize Vectors

    #region Unit Normal
    /// <summary>
    /// Get the unit normal.
    /// </summary>
    /// <param name="pt1X">The pt1X.</param>
    /// <param name="pt1Y">The pt1Y.</param>
    /// <param name="pt2X">The pt2X.</param>
    /// <param name="pt2Y">The pt2Y.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2}" />.
    /// </returns>
    /// <acknowledgment>
    /// http://www.angusj.com
    /// </acknowledgment>
    public static (T X, T Y) UnitNormal<T>(T pt1X, T pt1Y, T pt2X, T pt2Y)
        where T : IFloatingPointIeee754<T>
    {
        var dx = pt2X - pt1X;
        var dy = pt2Y - pt1Y;
        if ((dx == T.Zero) && (dy == T.Zero))
        {
            return (T.Zero, T.Zero);
        }

        var f = T.One / T.Sqrt((dx * dx) + (dy * dy));
        dx *= f;
        dy *= f;

        return (dy, -dx);
    }
    #endregion Unit Normal

    #region Perpendicular Clockwise
    /// <summary>
    /// Find the Clockwise Perpendicular of a Vector.
    /// </summary>
    /// <param name="i">The i.</param>
    /// <param name="j">The j.</param>
    /// <returns></returns>
    /// <remarks>
    /// <para>To get the perpendicular vector in two dimensions use I = -J, J = I</para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y) PerpendicularClockwise<T>(T i, T j) where T : INumber<T> => (-j, i);
    #endregion Perpendicular Clockwise

    #region Perpendicular Counter Clockwise
    /// <summary>
    /// Find the Counter Clockwise Perpendicular of a Vector.
    /// </summary>
    /// <param name="i">The i.</param>
    /// <param name="j">The j.</param>
    /// <returns></returns>
    /// <remarks>
    /// <para>To get the perpendicular vector in two dimensions use I = -J, J = I</para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y) PerpendicularCounterClockwise<T>(T i, T j) where T : INumber<T> => (j, -i);
    #endregion Perpendicular Counter Clockwise

    #region Pitch Rotate X
    /// <summary>
    /// The rotate x.
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="z1">The z1.</param>
    /// <param name="yOff">The yOff.</param>
    /// <param name="zOff">The zOff.</param>
    /// <param name="rad">The rad.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3}" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z) PitchRotateX<T>(T x1, T y1, T z1, T yOff, T zOff, T rad) where T : IFloatingPointIeee754<T> => PitchRotateX(x1, y1, z1, yOff, zOff, T.Sin(rad), T.Cos(rad));

    /// <summary>
    /// The rotate x.
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="z1">The z1.</param>
    /// <param name="rad">The rad.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3}" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z) PitchRotateX<T>(T x1, T y1, T z1, T rad) where T : IFloatingPointIeee754<T> => PitchRotateX(x1, y1, z1, T.Sin(rad), T.Cos(rad));

    /// <summary>
    /// The rotate x.
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="z1">The z1.</param>
    /// <param name="sin">The sin.</param>
    /// <param name="cos">The cos.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3}" />.
    /// </returns>
    /// <acknowledgment>
    /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z) PitchRotateX<T>(T x1, T y1, T z1, T sin, T cos) where T : IFloatingPointIeee754<T> => (x1, (y1 * cos) - (z1 * sin), (y1 * sin) + (z1 * cos));

    /// <summary>
    /// The rotate x.
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="z1">The z1.</param>
    /// <param name="yOff">The yOff.</param>
    /// <param name="zOff">The zOff.</param>
    /// <param name="sin">The Sine of the angle.</param>
    /// <param name="cos">The Cosine of the angle.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3}" />.
    /// </returns>
    /// <acknowledgment>
    /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z) PitchRotateX<T>(T x1, T y1, T z1, T yOff, T zOff, T sin, T cos) where T : IFloatingPointIeee754<T> => (x1, (y1 * cos) - (z1 * sin) + ((yOff * (T.One - cos)) + (zOff * sin)), (y1 * sin) + (z1 * cos) + ((zOff * (T.One - cos)) - (yOff * sin)));
    #endregion Pitch Rotate X

    #region Yaw Rotate Y
    /// <summary>
    /// The rotate y.
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="z1">The z1.</param>
    /// <param name="rad">The rad.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3}" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z) YawRotateY<T>(T x1, T y1, T z1, T rad) where T : IFloatingPointIeee754<T> => YawRotateY(x1, y1, z1, T.Sin(rad), T.Cos(rad));

    /// <summary>
    /// The rotate y.
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="z1">The z1.</param>
    /// <param name="xOff">The xOff.</param>
    /// <param name="zOff">The zOff.</param>
    /// <param name="rad">The rad.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3}" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z) YawRotateY<T>(T x1, T y1, T z1, T xOff, T zOff, T rad) where T : IFloatingPointIeee754<T> => YawRotateY(x1, y1, z1, xOff, zOff, T.Sin(rad), T.Cos(rad));

    /// <summary>
    /// The rotate y.
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="z1">The z1.</param>
    /// <param name="sin">The Sine of the angle.</param>
    /// <param name="cos">The Cosine of the angle.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3}" />.
    /// </returns>
    /// <acknowledgment>
    /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z) YawRotateY<T>(T x1, T y1, T z1, T sin, T cos) where T : IFloatingPointIeee754<T> => ((z1 * sin) + (x1 * cos), y1, (z1 * cos) - (x1 * sin));

    /// <summary>
    /// The rotate y.
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="z1">The z1.</param>
    /// <param name="xOff">The xOff.</param>
    /// <param name="zOff">The zOff.</param>
    /// <param name="sin">The Sine of the angle.</param>
    /// <param name="cos">The Cosine of the angle.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3}" />.
    /// </returns>
    /// <acknowledgment>
    /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z) YawRotateY<T>(T x1, T y1, T z1, T xOff, T zOff, T sin, T cos) where T : IFloatingPointIeee754<T> => ((z1 * sin) + (x1 * cos) + ((xOff * (T.One - cos)) - (zOff * sin)), y1, (z1 * cos) - (x1 * sin) + ((zOff * (T.One - cos)) + (xOff * sin)));
    #endregion Yaw Rotate Y

    #region Roll Rotate Z
    /// <summary>
    /// The rotate z.
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="z1">The z1.</param>
    /// <param name="rad">The rad.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3}" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z) RollRotateZ<T>(T x1, T y1, T z1, T rad) where T : IFloatingPointIeee754<T> => RollRotateZ(x1, y1, z1, T.Sin(rad), T.Cos(rad));

    /// <summary>
    /// The rotate z.
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="z1">The z1.</param>
    /// <param name="xOff">The xOff.</param>
    /// <param name="yOff">The yOff.</param>
    /// <param name="rad">The rad.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3}" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z) RollRotateZ<T>(T x1, T y1, T z1, T xOff, T yOff, T rad) where T : IFloatingPointIeee754<T> => RollRotateZ(x1, y1, z1, xOff, yOff, T.Sin(rad), T.Cos(rad));

    /// <summary>
    /// The rotate z.
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="z1">The z1.</param>
    /// <param name="sin">The sin.</param>
    /// <param name="cos">The cos.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3}" />.
    /// </returns>
    /// <acknowledgment>
    /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z) RollRotateZ<T>(T x1, T y1, T z1, T sin, T cos) where T : IFloatingPointIeee754<T> => ((x1 * cos) - (y1 * sin), (x1 * sin) + (y1 * cos), z1);

    /// <summary>
    /// The rotate z.
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="z1">The z1.</param>
    /// <param name="xOff">The xOff.</param>
    /// <param name="yOff">The yOff.</param>
    /// <param name="sin">The Sine of the angle.</param>
    /// <param name="cos">The Cosine of the angle.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3}" />.
    /// </returns>
    /// <acknowledgment>
    /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z) RollRotateZ<T>(T x1, T y1, T z1, T xOff, T yOff, T sin, T cos) where T : IFloatingPointIeee754<T> => ((x1 * cos) - (y1 * sin) + ((xOff * (T.One - cos)) + (yOff * sin)), (x1 * sin) + (y1 * cos) + ((yOff * (T.One - cos)) - (xOff * sin)), z1);
    #endregion Roll Rotate Z

    #region Projection
    /// <summary>
    /// The projection.
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="z1">The z1.</param>
    /// <param name="x2">The x2.</param>
    /// <param name="y2">The y2.</param>
    /// <param name="z2">The z2.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3}" />.
    /// </returns>
    /// <acknowledgment>
    /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z) Projection<T>(T x1, T y1, T z1, T x2, T y2, T z2) where T : IFloatingPointIeee754<T> => (x2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2), y2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2), z2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2));
    #endregion Projection

    #region Rejection
    /// <summary>
    /// The rejection.
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <param name="z1">The z1.</param>
    /// <param name="x2">The x2.</param>
    /// <param name="y2">The y2.</param>
    /// <param name="z2">The z2.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3}" />.
    /// </returns>
    /// <acknowledgment>
    /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z) Rejection<T>(T x1, T y1, T z1, T x2, T y2, T z2) where T : IFloatingPointIeee754<T> => (x1 - (x2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2)), z1 - (y2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2)), z1 - (z2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2)));
    #endregion Rejection

    #region Reflection
    /// <summary>
    /// The reflection.
    /// </summary>
    /// <param name="i1">The i1.</param>
    /// <param name="j1">The j1.</param>
    /// <param name="k1">The k1.</param>
    /// <param name="i2">The i2.</param>
    /// <param name="j2">The j2.</param>
    /// <param name="k2">The k2.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3}" />.
    /// </returns>
    /// <acknowledgment>
    /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y, T Z) Reflection<T>(
        T i1, T j1, T k1,
        T i2, T j2, T k2)
        where T : IFloatingPointIeee754<T>
    {
        // if v2 has a right angle to vector, return -vector and stop
        if (T.Abs(T.Abs(Angle(i1, j1, k1, i2, j2, k2)) - (T.Pi / T.CreateChecked(2))) < T.Epsilon)
        {
            return (-i1, -j1, -k1);
        }

        (var x, var y, var z) = Projection(i1, j1, k1, i2, j2, k2);
        return (
            ((T.CreateChecked(2) * x) - i1) * Magnitude(i1, j1, k1),
            ((T.CreateChecked(2) * y) - j1) * Magnitude(i1, j1, k1),
            ((T.CreateChecked(2) * z) - k1) * Magnitude(i1, j1, k1)
            );
    }
    #endregion Reflection

    #region Min Point
    /// <summary>
    /// The min point.
    /// </summary>
    /// <param name="point1X">The point1X.</param>
    /// <param name="point1Y">The point1Y.</param>
    /// <param name="point2X">The point2X.</param>
    /// <param name="point2Y">The point2Y.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2}" />.
    /// </returns>
    /// <acknowledgment>
    /// http://www.kevlindev.com/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y) MinPoint<T>(T point1X, T point1Y, T point2X, T point2Y) where T : INumber<T> => (T.Min(point1X, point2X), T.Min(point1Y, point2Y));
    #endregion Min Point

    #region Max Point
    /// <summary>
    /// The max point.
    /// </summary>
    /// <param name="point1X">The point1X.</param>
    /// <param name="point1Y">The point1Y.</param>
    /// <param name="point2X">The point2X.</param>
    /// <param name="point2Y">The point2Y.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2}" />.
    /// </returns>
    /// <acknowledgment>
    /// http://www.kevlindev.com/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y) MaxPoint<T>(T point1X, T point1Y, T point2X, T point2Y) where T : INumber<T> => (T.Max(point1X, point2X), T.Max(point1Y, point2Y));
    #endregion Max Point
}
