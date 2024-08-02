// <copyright file="Operations.Vectors.cs" company="Shkyrockett" >
// Copyright © 2023 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Numerics;

namespace Engine.Mathematics;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public static class NumericTraits<T>
    where T : struct, INumber<T>
{
    /// <summary>
    /// Determines the minimum of the type.
    /// </summary>
    public static readonly T MinValue = T.Zero switch
    {
        char => T.CreateChecked(char.MinValue),
        sbyte => T.CreateChecked(sbyte.MinValue),
        byte => T.CreateChecked(byte.MinValue),
        short => T.CreateChecked(short.MinValue),
        ushort => T.CreateChecked(ushort.MinValue),
        int => T.CreateChecked(int.MinValue),
        uint => T.CreateChecked(uint.MinValue),
        long => T.CreateChecked(long.MinValue),
        ulong => T.CreateChecked(ulong.MinValue),
        Int128 => T.CreateChecked(Int128.MinValue),
        UInt128 => T.CreateChecked(UInt128.MinValue),
        nint => T.CreateChecked(nint.MinValue),
        nuint => T.CreateChecked(nuint.MinValue),
        Half => T.CreateChecked(Half.MinValue),
        float => T.CreateChecked(float.MinValue),
        double => T.CreateChecked(double.MinValue),
        decimal => T.CreateChecked(decimal.MinValue),
        Complex => T.CreateChecked(double.MinValue),
        BigInteger => throw new InvalidOperationException($"{nameof(BigInteger)} does not have a minimum value."),
        _ => throw new InvalidOperationException("Unsupported type or non-numeric type.")
    };

    /// <summary>
    /// Determines the maximum of the type.
    /// </summary>
    public static readonly T MaxValue = T.Zero switch
    {
        char => T.CreateChecked(char.MaxValue),
        sbyte => T.CreateChecked(sbyte.MaxValue),
        byte => T.CreateChecked(byte.MaxValue),
        short => T.CreateChecked(short.MaxValue),
        ushort => T.CreateChecked(ushort.MaxValue),
        int => T.CreateChecked(int.MaxValue),
        uint => T.CreateChecked(uint.MaxValue),
        long => T.CreateChecked(long.MaxValue),
        ulong => T.CreateChecked(ulong.MaxValue),
        Int128 => T.CreateChecked(Int128.MaxValue),
        UInt128 => T.CreateChecked(UInt128.MaxValue),
        nint => T.CreateChecked(nint.MaxValue),
        nuint => T.CreateChecked(nuint.MaxValue),
        Half => T.CreateChecked(Half.MaxValue),
        float => T.CreateChecked(float.MaxValue),
        double => T.CreateChecked(double.MaxValue),
        decimal => T.CreateChecked(decimal.MaxValue),
        Complex => T.CreateChecked(double.MaxValue),
        BigInteger => throw new InvalidOperationException($"{nameof(BigInteger)} does not have a maximum value."),
        _ => throw new InvalidOperationException("Unsupported type or non-numeric type.")
    };

    /// <summary>
    /// Determines the epsilon of the type.
    /// </summary>
    public static readonly T Epsilon = T.Zero switch
    {
        char => T.Zero,
        sbyte => T.Zero,
        byte => T.Zero,
        short => T.Zero,
        ushort => T.Zero,
        int => T.Zero,
        uint => T.Zero,
        long => T.Zero,
        ulong => T.Zero,
        Int128 => T.Zero,
        UInt128 => T.Zero,
        nint => T.Zero,
        nuint => T.Zero,
        BigInteger => T.Zero,
        Half => T.CreateChecked(Half.Epsilon),
        float => T.CreateChecked(float.Epsilon),
        double => T.CreateChecked(double.Epsilon),
        decimal => T.CreateChecked(1m / (decimal)Math.Pow(10, 28)),
        Complex => T.CreateChecked(double.Epsilon),
        _ => throw new InvalidOperationException("Unsupported or non-numeric type.")
    };
}
