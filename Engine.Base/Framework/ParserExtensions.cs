// <copyright file="ParserExtensions.cs" company="Shkyrockett" >
// Copyright © 2008 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Globalization;
using System.Runtime.CompilerServices;

namespace Engine;

/// <summary>
/// Extension Methods for Parsing.
/// </summary>
public static class ParserExtensions
{
    /// <summary>
    /// Parse the <see cref="bool"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool? ParseBool(this string text) => bool.TryParse(text, out var value) ? value : (bool?)null;

    /// <summary>
    /// Parse the <see cref="char"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The <see cref="char"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static char? ParseChar(this string text) => char.TryParse(text, out var value) ? value : (char?)null;

    /// <summary>
    /// Parse the <see cref="sbyte"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The <see cref="sbyte"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static sbyte? ParseSByte(this string text) => ParseSByte(text, NumberStyles.Integer, CultureInfo.InvariantCulture);

    /// <summary>
    /// Parse the <see cref="sbyte"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="formatProvider"></param>
    /// <returns>The <see cref="sbyte"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static sbyte? ParseSByte(this string text, IFormatProvider formatProvider) => ParseSByte(text, NumberStyles.Integer, formatProvider);

    /// <summary>
    /// Parse the <see cref="sbyte"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="styles"></param>
    /// <param name="formatProvider"></param>
    /// <returns>The <see cref="sbyte"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static sbyte? ParseSByte(this string text, NumberStyles styles, IFormatProvider formatProvider) => sbyte.TryParse(text, styles, formatProvider, out var value) ? value : (sbyte?)null;

    /// <summary>
    /// Parse the <see cref="byte"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The <see cref="byte"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static byte? ParseByte(this string text) => ParseByte(text, NumberStyles.Integer, CultureInfo.InvariantCulture);

    /// <summary>
    /// Parse the <see cref="byte"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="formatProvider"></param>
    /// <returns>The <see cref="byte"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static byte? ParseByte(this string text, IFormatProvider formatProvider) => ParseByte(text, NumberStyles.Integer, formatProvider);

    /// <summary>
    /// Parse the <see cref="byte"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="styles"></param>
    /// <param name="formatProvider"></param>
    /// <returns>The <see cref="byte"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static byte? ParseByte(this string text, NumberStyles styles, IFormatProvider formatProvider) => byte.TryParse(text, styles, formatProvider, out var value) ? value : (byte?)null;

    /// <summary>
    /// Parse the <see cref="ushort"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The <see cref="ushort"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ushort? ParseUShort(this string text) => ParseUShort(text, NumberStyles.Integer, CultureInfo.InvariantCulture);

    /// <summary>
    /// Parse the <see cref="ushort"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="formatProvider"></param>
    /// <returns>The <see cref="ushort"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ushort? ParseUShort(this string text, IFormatProvider formatProvider) => ParseUShort(text, NumberStyles.Integer, formatProvider);

    /// <summary>
    /// Parse the <see cref="ushort"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="styles"></param>
    /// <param name="formatProvider"></param>
    /// <returns>The <see cref="ushort"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ushort? ParseUShort(this string text, NumberStyles styles, IFormatProvider formatProvider) => ushort.TryParse(text, styles, formatProvider, out var value) ? value : (ushort?)null;

    /// <summary>
    /// Parse the <see cref="short"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The <see cref="short"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static short? ParseShort(this string text) => ParseShort(text, NumberStyles.Integer, CultureInfo.InvariantCulture);

    /// <summary>
    /// Parse the <see cref="short"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="formatProvider"></param>
    /// <returns>The <see cref="short"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static short? ParseShort(this string text, IFormatProvider formatProvider) => ParseShort(text, NumberStyles.Integer, formatProvider);

    /// <summary>
    /// Parse the <see cref="short"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="styles"></param>
    /// <param name="formatProvider"></param>
    /// <returns>The <see cref="short"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static short? ParseShort(this string text, NumberStyles styles, IFormatProvider formatProvider) => short.TryParse(text, styles, formatProvider, out var value) ? value : (short?)null;

    /// <summary>
    /// Parse the <see cref="uint"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The <see cref="uint"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static uint? ParseUInt(this string text) => ParseUInt(text, NumberStyles.Integer, CultureInfo.InvariantCulture);

    /// <summary>
    /// Parse the <see cref="uint"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="formatProvider"></param>
    /// <returns>The <see cref="uint"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static uint? ParseUInt(this string text, IFormatProvider formatProvider) => ParseUInt(text, NumberStyles.Integer, formatProvider);

    /// <summary>
    /// Parse the <see cref="uint"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="styles"></param>
    /// <param name="formatProvider"></param>
    /// <returns>The <see cref="uint"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static uint? ParseUInt(this string text, NumberStyles styles, IFormatProvider formatProvider) => uint.TryParse(text, styles, formatProvider, out var value) ? value : (uint?)null;

    /// <summary>
    /// Parse the <see cref="int"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The <see cref="int"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static int? ParseInt(this string text) => ParseInt(text, NumberStyles.Integer, CultureInfo.InvariantCulture);

    /// <summary>
    /// Parse the <see cref="int"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="formatProvider"></param>
    /// <returns>The <see cref="int"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static int? ParseInt(this string text, IFormatProvider formatProvider) => ParseInt(text, NumberStyles.Integer, formatProvider);

    /// <summary>
    /// Parse the <see cref="int"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="styles"></param>
    /// <param name="formatProvider"></param>
    /// <returns>The <see cref="int"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static int? ParseInt(this string text, NumberStyles styles, IFormatProvider formatProvider) => int.TryParse(text, styles, formatProvider, out var value) ? value : (int?)null;

    /// <summary>
    /// Parse the <see cref="ulong"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The <see cref="ulong"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ulong? ParseULong(this string text) => ParseULong(text, NumberStyles.Integer, CultureInfo.InvariantCulture);

    /// <summary>
    /// Parse the <see cref="ulong"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="formatProvider"></param>
    /// <returns>The <see cref="ulong"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ulong? ParseULong(this string text, IFormatProvider formatProvider) => ParseULong(text, NumberStyles.Integer, formatProvider);

    /// <summary>
    /// Parse the <see cref="ulong"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="styles"></param>
    /// <param name="formatProvider"></param>
    /// <returns>The <see cref="ulong"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static ulong? ParseULong(this string text, NumberStyles styles, IFormatProvider formatProvider) => ulong.TryParse(text, styles, formatProvider, out var value) ? value : (ulong?)null;

    /// <summary>
    /// Parse the <see cref="long"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The <see cref="long"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static long? ParseLong(this string text) => ParseLong(text, NumberStyles.Integer, CultureInfo.InvariantCulture);

    /// <summary>
    /// Parse the <see cref="long"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="formatProvider"></param>
    /// <returns>The <see cref="long"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static long? ParseLong(this string text, IFormatProvider formatProvider) => ParseLong(text, NumberStyles.Integer, formatProvider);

    /// <summary>
    /// Parse the <see cref="long"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="styles"></param>
    /// <param name="formatProvider"></param>
    /// <returns>The <see cref="long"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static long? ParseLong(this string text, NumberStyles styles, IFormatProvider formatProvider) => long.TryParse(text, styles, formatProvider, out var value) ? value : (long?)null;

    /// <summary>
    /// Parse the <see cref="float"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The <see cref="float"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static float? ParseFloat(this string text) => ParseFloat(text, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);

    /// <summary>
    /// Parse the <see cref="float"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="formatProvider"></param>
    /// <returns>The <see cref="float"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static float? ParseFloat(this string text, IFormatProvider formatProvider) => ParseFloat(text, NumberStyles.Float | NumberStyles.AllowThousands, formatProvider);

    /// <summary>
    /// Parse the <see cref="float"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="styles"></param>
    /// <param name="formatProvider"></param>
    /// <returns>The <see cref="float"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static float? ParseFloat(this string text, NumberStyles styles, IFormatProvider formatProvider) => float.TryParse(text, styles, formatProvider, out var value) ? value : (float?)null;

    /// <summary>
    /// Parse the <see cref="double"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The <see cref="double"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double? ParseDouble(this string text) => ParseDouble(text, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);

    /// <summary>
    /// Parse the <see cref="double"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="formatProvider"></param>
    /// <returns>The <see cref="double"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double? ParseDouble(this string text, IFormatProvider formatProvider) => ParseDouble(text, NumberStyles.Float | NumberStyles.AllowThousands, formatProvider);

    /// <summary>
    /// Parse the <see cref="double"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="styles"></param>
    /// <param name="formatProvider">The provider.</param>
    /// <returns>The <see cref="double"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double? ParseDouble(this string text, NumberStyles styles, IFormatProvider formatProvider) => double.TryParse(text, styles, formatProvider, out var value) ? value : (double?)null;

    /// <summary>
    /// Parse the <see cref="decimal"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The <see cref="decimal"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static decimal? ParseDecimal(this string text) => ParseDecimal(text, NumberStyles.Number, CultureInfo.InvariantCulture);

    /// <summary>
    /// Parse the <see cref="decimal"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="formatProvider"></param>
    /// <returns>The <see cref="decimal"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static decimal? ParseDecimal(this string text, IFormatProvider formatProvider) => ParseDecimal(text, NumberStyles.Number, formatProvider);

    /// <summary>
    /// Parse the <see cref="decimal"/>.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="styles"></param>
    /// <param name="formatProvider">The provider.</param>
    /// <returns>The <see cref="decimal"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static decimal? ParseDecimal(this string text, NumberStyles styles, IFormatProvider formatProvider) => decimal.TryParse(text, styles, formatProvider, out var value) ? value : (decimal?)null;
}
