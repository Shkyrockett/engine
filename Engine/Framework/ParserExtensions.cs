// <copyright file="ParserExtensions.cs" company="Shkyrockett" >
//    Copyright © 2008 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//    Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace Engine
{
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool? ParseBool(this string text) => bool.TryParse(text, out var value) ? value : (bool?)null;

        /// <summary>
        /// Parse the <see cref="char"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The <see cref="char"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static char? ParseChar(this string text) => char.TryParse(text, out var value) ? value : (char?)null;

        /// <summary>
        /// Parse the <see cref="sbyte"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The <see cref="sbyte"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte? ParseSByte(this string text) => ParseSByte(text, NumberStyles.Integer, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse the <see cref="sbyte"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="provider"></param>
        /// <returns>The <see cref="sbyte"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte? ParseSByte(this string text, IFormatProvider provider) => ParseSByte(text, NumberStyles.Integer, provider);

        /// <summary>
        /// Parse the <see cref="sbyte"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="styles"></param>
        /// <param name="provider"></param>
        /// <returns>The <see cref="sbyte"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte? ParseSByte(this string text, NumberStyles styles, IFormatProvider provider) => sbyte.TryParse(text, styles, provider, out var value) ? value : (sbyte?)null;

        /// <summary>
        /// Parse the <see cref="byte"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The <see cref="byte"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte? ParseByte(this string text) => ParseByte(text, NumberStyles.Integer, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse the <see cref="byte"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="provider"></param>
        /// <returns>The <see cref="byte"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte? ParseByte(this string text, IFormatProvider provider) => ParseByte(text, NumberStyles.Integer, provider);

        /// <summary>
        /// Parse the <see cref="byte"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="styles"></param>
        /// <param name="provider"></param>
        /// <returns>The <see cref="byte"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte? ParseByte(this string text, NumberStyles styles, IFormatProvider provider) => byte.TryParse(text, styles, provider, out var value) ? value : (byte?)null;

        /// <summary>
        /// Parse the <see cref="ushort"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The <see cref="ushort"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort? ParseUShort(this string text) => ParseUShort(text, NumberStyles.Integer, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse the <see cref="ushort"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="provider"></param>
        /// <returns>The <see cref="ushort"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort? ParseUShort(this string text, IFormatProvider provider) => ParseUShort(text, NumberStyles.Integer, provider);

        /// <summary>
        /// Parse the <see cref="ushort"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="styles"></param>
        /// <param name="provider"></param>
        /// <returns>The <see cref="ushort"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort? ParseUShort(this string text, NumberStyles styles, IFormatProvider provider) => ushort.TryParse(text, styles, provider, out var value) ? value : (ushort?)null;

        /// <summary>
        /// Parse the <see cref="short"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The <see cref="short"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short? ParseShort(this string text) => ParseShort(text, NumberStyles.Integer, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse the <see cref="short"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="provider"></param>
        /// <returns>The <see cref="short"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short? ParseShort(this string text, IFormatProvider provider) => ParseShort(text, NumberStyles.Integer, provider);

        /// <summary>
        /// Parse the <see cref="short"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="styles"></param>
        /// <param name="provider"></param>
        /// <returns>The <see cref="short"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short? ParseShort(this string text, NumberStyles styles, IFormatProvider provider) => short.TryParse(text, styles, provider, out var value) ? value : (short?)null;

        /// <summary>
        /// Parse the <see cref="uint"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint? ParseUInt(this string text) => ParseUInt(text, NumberStyles.Integer, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse the <see cref="uint"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="provider"></param>
        /// <returns>The <see cref="uint"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint? ParseUInt(this string text, IFormatProvider provider) => ParseUInt(text, NumberStyles.Integer, provider);

        /// <summary>
        /// Parse the <see cref="uint"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="styles"></param>
        /// <param name="provider"></param>
        /// <returns>The <see cref="uint"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint? ParseUInt(this string text, NumberStyles styles, IFormatProvider provider) => uint.TryParse(text, styles, provider, out var value) ? value : (uint?)null;

        /// <summary>
        /// Parse the <see cref="int"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int? ParseInt(this string text) => ParseInt(text, NumberStyles.Integer, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse the <see cref="int"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="provider"></param>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int? ParseInt(this string text, IFormatProvider provider) => ParseInt(text, NumberStyles.Integer, provider);

        /// <summary>
        /// Parse the <see cref="int"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="styles"></param>
        /// <param name="provider"></param>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int? ParseInt(this string text, NumberStyles styles, IFormatProvider provider) => int.TryParse(text, styles, provider, out var value) ? value : (int?)null;

        /// <summary>
        /// Parse the <see cref="ulong"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The <see cref="ulong"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong? ParseULong(this string text) => ParseULong(text, NumberStyles.Integer, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse the <see cref="ulong"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="provider"></param>
        /// <returns>The <see cref="ulong"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong? ParseULong(this string text, IFormatProvider provider) => ParseULong(text, NumberStyles.Integer, provider);

        /// <summary>
        /// Parse the <see cref="ulong"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="styles"></param>
        /// <param name="provider"></param>
        /// <returns>The <see cref="ulong"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong? ParseULong(this string text, NumberStyles styles, IFormatProvider provider) => ulong.TryParse(text, styles, provider, out var value) ? value : (ulong?)null;

        /// <summary>
        /// Parse the <see cref="long"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The <see cref="long"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long? ParseLong(this string text) => ParseLong(text, NumberStyles.Integer, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse the <see cref="long"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="provider"></param>
        /// <returns>The <see cref="long"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long? ParseLong(this string text, IFormatProvider provider) => ParseLong(text, NumberStyles.Integer, provider);

        /// <summary>
        /// Parse the <see cref="long"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="styles"></param>
        /// <param name="provider"></param>
        /// <returns>The <see cref="long"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long? ParseLong(this string text, NumberStyles styles, IFormatProvider provider) => long.TryParse(text, styles, provider, out var value) ? value : (long?)null;

        /// <summary>
        /// Parse the <see cref="float"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The <see cref="float"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float? ParseFloat(this string text) => ParseFloat(text, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse the <see cref="float"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="provider"></param>
        /// <returns>The <see cref="float"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float? ParseFloat(this string text, IFormatProvider provider) => ParseFloat(text, NumberStyles.Float | NumberStyles.AllowThousands, provider);

        /// <summary>
        /// Parse the <see cref="float"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="styles"></param>
        /// <param name="provider"></param>
        /// <returns>The <see cref="float"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float? ParseFloat(this string text, NumberStyles styles, IFormatProvider provider) => float.TryParse(text, styles, provider, out var value) ? value : (float?)null;

        /// <summary>
        /// Parse the <see cref="double"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double? ParseDouble(this string text) => ParseDouble(text, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse the <see cref="double"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="provider"></param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double? ParseDouble(this string text, IFormatProvider provider) => ParseDouble(text, NumberStyles.Float | NumberStyles.AllowThousands, provider);

        /// <summary>
        /// Parse the <see cref="double"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="styles"></param>
        /// <param name="provider">The provider.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double? ParseDouble(this string text, NumberStyles styles, IFormatProvider provider) => double.TryParse(text, styles, provider, out var value) ? value : (double?)null;

        /// <summary>
        /// Parse the <see cref="decimal"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The <see cref="decimal"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal? ParseDecimal(this string text) => ParseDecimal(text, NumberStyles.Number, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse the <see cref="decimal"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="provider"></param>
        /// <returns>The <see cref="decimal"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal? ParseDecimal(this string text, IFormatProvider provider) => ParseDecimal(text, NumberStyles.Number, provider);

        /// <summary>
        /// Parse the <see cref="decimal"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="styles"></param>
        /// <param name="provider">The provider.</param>
        /// <returns>The <see cref="decimal"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal? ParseDecimal(this string text, NumberStyles styles, IFormatProvider provider) => decimal.TryParse(text, styles, provider, out var value) ? value : (decimal?)null;
    }
}
