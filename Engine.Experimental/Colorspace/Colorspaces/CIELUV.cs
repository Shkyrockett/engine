// <copyright file="CIELUV.cs" company="Shkyrockett" >
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
/// The CIELUV struct.
/// Wikipedia: A modification of "CIE 1931 XYZ" to display color differences more conveniently. The CIELUV space is especially useful for additive mixtures of lights, due to its linear addition properties.
/// </summary>
/// <seealso cref="IColor" />
/// <seealso cref="IEquatable{T}" />
public struct CIELUV
    : IColor, IEquatable<CIELUV>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CIELUV" /> class.
    /// </summary>
    /// <param name="luminance">The luminance.</param>
    /// <param name="u">The u.</param>
    /// <param name="v">The v.</param>
    public CIELUV(double luminance, double u, double v)
    {
        Luminance = luminance;
        U = u;
        V = v;
    }

    /// <summary>
    /// Gets or sets the luminance.
    /// </summary>
    /// <value>
    /// The luminance.
    /// </value>
    public double Luminance { get; set; }

    /// <summary>
    /// Gets or sets the u.
    /// </summary>
    /// <value>
    /// The u.
    /// </value>
    public double U { get; set; }

    /// <summary>
    /// Gets or sets the v or blue to yellow value.
    /// </summary>
    /// <value>
    /// The v.
    /// </value>
    public double V { get; set; }

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(CIELUV left, CIELUV right) => left.Equals(right);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(CIELUV left, CIELUV right) => !(left == right);

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
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override readonly bool Equals(object? obj) => obj is CIELUV cIELUV && Equals(cIELUV);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public readonly bool Equals([AllowNull] CIELUV other) => Luminance == other.Luminance && U == other.U && V == other.V;

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override readonly int GetHashCode() => HashCode.Combine(Luminance, U, V);

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
