// <copyright file="CIEXYZTriple.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Engine.Colorspace
{
    /// <summary>
    /// The CIEXYZ triple struct.
    /// </summary>
    /// <seealso cref="IColor" />
    /// <seealso cref="IEquatable{T}" />
    public struct CIEXYZTriple
        : IColor, IEquatable<CIEXYZTriple>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CIEXYZTriple" /> class.
        /// </summary>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        public CIEXYZTriple(CIEXYZ red, CIEXYZ green, CIEXYZ blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        /// <summary>
        /// Gets or sets the red.
        /// </summary>
        /// <value>
        /// The red.
        /// </value>
        public CIEXYZ Red { get; set; }

        /// <summary>
        /// Gets or sets the green.
        /// </summary>
        /// <value>
        /// The green.
        /// </value>
        public CIEXYZ Green { get; set; }

        /// <summary>
        /// Gets or sets the blue.
        /// </summary>
        /// <value>
        /// The blue.
        /// </value>
        public CIEXYZ Blue { get; set; }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(CIEXYZTriple left, CIEXYZTriple right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(CIEXYZTriple left, CIEXYZTriple right) => !(left == right);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => obj is CIEXYZTriple triple && Equals(triple);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals([AllowNull] CIEXYZTriple other) => EqualityComparer<CIEXYZ>.Default.Equals(Red, other.Red) && EqualityComparer<CIEXYZ>.Default.Equals(Green, other.Green) && EqualityComparer<CIEXYZ>.Default.Equals(Blue, other.Blue);

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
            var (r1, g1, b1, a1) = (other?.ToRGBATuple()).Value;
            return r0 == r1 && g0 == g1 && b0 == b1 && a0 == a1;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode() => HashCode.Combine(Red, Green, Blue);

        /// <summary>
        /// The to RGBA tuple.
        /// </summary>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2, T3, T4}" />.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public (byte Red, byte Green, byte Blue, byte Alpha) ToRGBATuple() => throw new NotImplementedException();

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
