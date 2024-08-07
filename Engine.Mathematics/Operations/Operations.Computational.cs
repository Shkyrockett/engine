﻿// <copyright file="Operations.Computational.cs" company="Shkyrockett" >
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
    /// <summary>
    /// Casts the.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>A TResult.</returns>
    public static TResult Cast<T, TResult>(this T value)
        where T : INumber<T>
        where TResult : INumber<TResult> => TResult.CreateChecked(value);

    /// <summary>
    /// Swap left and right values so the left object has the value of the right object and visa-versa.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a">The left value.</param>
    /// <param name="b">The right value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static void Swap<T>(ref T a, ref T b) => (b, a) = (a, b);

    /// <summary>
    /// https://blogs.msdn.microsoft.com/mazhou/2017/12/12/c-7-series-part-7-ref-returns/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array">The array.</param>
    /// <param name="position">The position.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ref T ElementAt<T>(ref T[] array, int position) => ref array[position];

    /// <summary>
    /// Selects the higher.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a">a.</param>
    /// <param name="b">The b.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ref T SelectHigher<T>(ref T a, ref T b) where T : IComparable => ref a.CompareTo(b) >= 0 ? ref a : ref b;

    /// <summary>
    /// Selects the lower.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a">a.</param>
    /// <param name="b">The b.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ref T SelectLower<T>(ref T a, ref T b) where T : IComparable => ref a.CompareTo(b) <= 0 ? ref a : ref b;

    /// <summary>
    /// The high nibble.
    /// </summary>
    /// <param name="n">The n.</param>
    /// <returns>
    /// The <see cref="byte" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static byte HighNibble(this byte n) => (byte)(n >> 0x4);

    /// <summary>
    /// The low nibble.
    /// </summary>
    /// <param name="n">The n.</param>
    /// <returns>
    /// The <see cref="byte" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static byte LowNibble(this byte n) => (byte)(n & 0x0F);

    /// <summary>
    /// Get the Hight and Low nibbles as a <see cref="ValueTuple{T1, T2}" />.
    /// </summary>
    /// <param name="n">The n.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (byte High, byte Low) Nibbles(this byte n) => ((byte)(n >> 0x4), (byte)(n & 0x0F));

    /// <summary>
    /// The join nibbles.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns>
    /// The <see cref="byte" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static byte JoinNibbles((byte High, byte Low) tuple)
    {
        byte value = 0;
        value = (byte)((value & 0xF0) | (tuple.Low & 0x0F));
        value = (byte)((value & 0x0F) | (tuple.High << 4));
        return value;
    }

    /// <summary>
    /// The join nibbles.
    /// </summary>
    /// <param name="high">The h.</param>
    /// <param name="low">The l.</param>
    /// <returns>
    /// The <see cref="byte" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static byte JoinNibbles(byte high, byte low)
    {
        byte value = 0;
        value = (byte)((value & 0xF0) | (low & 0x0F));
        value = (byte)((value & 0x0F) | (high << 4));
        return value;
    }

    /// <summary>
    /// The high word.
    /// </summary>
    /// <param name="n">The n.</param>
    /// <returns>
    /// The <see cref="int" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static int HighWord(this int n) => (n >> 0x10) & 0xffff;

    /// <summary>
    /// The high word.
    /// </summary>
    /// <param name="n">The n.</param>
    /// <returns>
    /// The <see cref="uint" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static uint HighWord(this uint n) => (n >> 0x10) & 0xffff;

    /// <summary>
    /// The signed high word.
    /// </summary>
    /// <param name="n">The n.</param>
    /// <returns>
    /// The <see cref="int" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static int SignedHighWord(this int n) => (short)((n >> 0x10) & 0xffff);

    /// <summary>
    /// The signed high word.
    /// </summary>
    /// <param name="n">The n.</param>
    /// <returns>
    /// The <see cref="int" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static int SignedHighWord(this IntPtr n) => SignedHighWord((int)(long)n);

    /// <summary>
    /// The low word.
    /// </summary>
    /// <param name="n">The n.</param>
    /// <returns>
    /// The <see cref="int" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static int LowWord(this int n) => n & 0xffff;

    /// <summary>
    /// The low word.
    /// </summary>
    /// <param name="n">The n.</param>
    /// <returns>
    /// The <see cref="uint" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static uint LowWord(this uint n) => n & 0xffff;

    /// <summary>
    /// The signed low word.
    /// </summary>
    /// <param name="n">The n.</param>
    /// <returns>
    /// The <see cref="int" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static int SignedLowWord(this int n) => (short)(n & 0xffff);

    /// <summary>
    /// The signed low word.
    /// </summary>
    /// <param name="n">The n.</param>
    /// <returns>
    /// The <see cref="int" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static int SignedLowWord(this IntPtr n) => SignedLowWord((int)(long)n);
}
