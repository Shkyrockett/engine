﻿// <copyright file="CIELCh.cs" company="Shkyrockett" >
// Copyright © 2013 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Diagnostics.CodeAnalysis;

namespace Engine.ColorSpace;

/// <summary>
/// Lightness Chromatically and Hue color space structure.
/// </summary>
/// <seealso cref="IColor" />
/// <seealso cref="IEquatable{T}" />
public struct CIELCh
    : IColor, IEquatable<CIELCh>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CIELCh" /> class.
    /// </summary>
    /// <param name="lightness">The lightness.</param>
    /// <param name="chromaticity">The chromaticity.</param>
    /// <param name="hue">The hue.</param>
    public CIELCh(double lightness, double chromaticity, double hue)
    {
        Lightness = lightness;
        Chromaticity = chromaticity;
        Hue = hue;
    }

    /// <summary>
    /// Gets or sets the lightness.
    /// </summary>
    /// <value>
    /// The lightness.
    /// </value>
    public double Lightness { get; set; }

    /// <summary>
    /// Gets or sets the chromaticity.
    /// </summary>
    /// <value>
    /// The chromaticity.
    /// </value>
    public double Chromaticity { get; set; }

    /// <summary>
    /// Gets or sets the hue.
    /// </summary>
    /// <value>
    /// The hue.
    /// </value>
    public double Hue { get; set; }

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(CIELCh left, CIELCh right) => left.Equals(right);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(CIELCh left, CIELCh right) => !(left == right);

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override readonly bool Equals(object obj) => obj is CIELCh ch && Equals(ch);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public readonly bool Equals([AllowNull] CIELCh other) => Lightness == other.Lightness && Chromaticity == other.Chromaticity && Hue == other.Hue;

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="other">The other.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    public readonly bool Equals(IColor? other)
    {
        var (r0, g0, b0, a0) = ToRGBATuple();
        var (r1, g1, b1, a1) = (other?.ToRGBATuple()).Value;
        return r0 == r1 && g0 == g1 && b0 == b1 && a0 == a1;
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override readonly int GetHashCode() => HashCode.Combine(Lightness, Chromaticity, Hue);

    /// <summary>
    /// The to RGBA tuple.
    /// </summary>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3, T4}" />.
    /// </returns>
    /// <exception cref="NotImplementedException"></exception>
    public readonly (byte Red, byte Green, byte Blue, byte Alpha) ToRGBATuple() => throw new NotImplementedException();

    /// <summary>
    /// The to string.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="formatProvider">The formatProvider.</param>
    /// <returns>
    /// The <see cref="string" />.
    /// </returns>
    /// <exception cref="NotImplementedException"></exception>
    public readonly string ToString(string? format, IFormatProvider? formatProvider) => throw new NotImplementedException();
}
