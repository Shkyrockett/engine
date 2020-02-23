// <copyright file="CIELUV.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;

namespace Engine.Colorspace
{
    /// <summary>
    /// The CIELUV struct.
    /// Wikipedia: Measurements over a larger field of view than the "CIE 1931 XYZ" color space which produces slightly different results.
    /// </summary>
    /// <seealso cref="IColor" />
    /// <seealso cref="IEquatable{T}" />
    public struct CIELUVW
        : IColor, IEquatable<CIELUVW>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CIELUVW" /> class.
        /// </summary>
        /// <param name="luminance">The luminance.</param>
        /// <param name="u">The u.</param>
        /// <param name="v">The v.</param>
        public CIELUVW(double luminance, double u, double v)
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
        public static bool operator ==(CIELUVW left, CIELUVW right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(CIELUVW left, CIELUVW right) => !(left == right);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => obj is CIELUVW cIELUVW && Equals(cIELUVW);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(CIELUVW other) => Luminance == other.Luminance && U == other.U && V == other.V;

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public bool Equals(IColor other)
        {
            var (r0, g0, b0, a0) = ToRGBATuple();
            var (r1, g1, b1, a1) = other.ToRGBATuple();
            return r0 == r1 && g0 == g1 && b0 == b1 && a0 == a1;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = 771089770;
            hashCode = hashCode * -1521134295 + Luminance.GetHashCode();
            hashCode = hashCode * -1521134295 + U.GetHashCode();
            hashCode = hashCode * -1521134295 + V.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// The to RGBA tuple.
        /// </summary>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3, T4}" />.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public (byte red, byte green, byte blue, byte alpha) ToRGBATuple() => throw new NotImplementedException();

        /// <summary>
        /// The to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The formatProvider.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public string ToString(string format, IFormatProvider formatProvider) => throw new NotImplementedException();
    }
}
