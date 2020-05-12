// <copyright file="CIELAB.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Diagnostics.CodeAnalysis;

namespace Engine.Colorspace
{
    /// <summary>
    /// <see cref="Lightness"/> and Channels A and B color spaces.
    /// Wikipedia: The intention of <see cref="CIELAB"/> (or L*a*b* or Lab) is to produce a color space that is more perceptually linear than other color spaces. Perceptually linear means that a change of the same amount in a color value should produce a change of about the same visual importance. CIELAB has almost entirely replaced an alternative related Lab color space "Hunter Lab". This space is commonly used for surface colors, but not for mixtures of (transmitted) light.
    /// </summary>
    public struct CIELAB
        : IColor, IEquatable<CIELAB>
    {
        /// <summary>
        /// Initializes a new instance of the Lightness and Channels A and B color space structure.
        /// </summary>
        /// <param name="lightness">Lightness component.</param>
        /// <param name="a">Channel A.</param>
        /// <param name="b">Channel B.</param>
        public CIELAB(byte lightness, byte a, byte b)
        {
            Lightness = lightness;
            ChannelA = a;
            ChannelB = b;
        }

        /// <summary>
        /// Lightness component.
        /// </summary>
        public double Lightness { get; set; }

        /// <summary>
        /// Channel A.
        /// </summary>
        public double ChannelA { get; set; }

        /// <summary>
        /// Channel B.
        /// </summary>
        public double ChannelB { get; set; }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(CIELAB left, CIELAB right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(CIELAB left, CIELAB right) => !(left == right);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => obj is CIELAB color && Equals(color);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals([AllowNull] CIELAB other) => Lightness == other.Lightness && ChannelA == other.ChannelA && ChannelB == other.ChannelB;

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode() => HashCode.Combine(Lightness, ChannelA, ChannelB);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Equals(IColor other)
        {
            var (r0, g0, b0, a0) = ToRGBATuple();
            var (r1, g1, b1, a1) = (other?.ToRGBATuple()).Value;
            return r0 == r1 && g0 == g1 && b0 == b1 && a0 == a1;
        }

        /// <summary>
        /// The to RGBA tuple.
        /// </summary>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4}"/>.</returns>
        public (byte Red, byte Green, byte Blue, byte Alpha) ToRGBATuple() => throw new NotImplementedException();

        /// <summary>
        /// The to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The formatProvider.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string ToString(string format, IFormatProvider formatProvider) => $"{nameof(Lightness)}: {Lightness}, {nameof(ChannelA)}: {ChannelA}, {nameof(ChannelB)}: {ChannelB}";
    }
}
