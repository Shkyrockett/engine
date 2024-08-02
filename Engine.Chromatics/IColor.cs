// <copyright file="IColor.cs" company="Shkyrockett" >
// Copyright © 2013 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Globalization;

namespace Engine;

/// <summary>
/// The IColor interface.
/// </summary>
/// <seealso cref="IFormattable" />
/// <seealso cref="IEquatable{T}" />
public interface IColor
    : IFormattable, //IComparable<IColor>, //IConvertible,
    IEquatable<IColor>
{
    /// <summary>
    /// The to RGBA tuple.
    /// </summary>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3, T4}" />.
    /// </returns>
    (byte Red, byte Green, byte Blue, byte Alpha) ToRGBATuple();

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    string? ToString() => ToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <param name="formatProvider">The provider.</param>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    string? ToString(IFormatProvider? formatProvider) => ToString(string.Empty /* format string */, formatProvider);

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="formatProvider">The provider.</param>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    new string? ToString(string? format, IFormatProvider? formatProvider);
}
