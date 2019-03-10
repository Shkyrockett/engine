// <copyright file="CIELAB.cs" company="Shkyrockett" >
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
    /// Lightness and Channels A and B color spaces.
    /// Wikipedia: The intention of CIELAB (or L*a*b* or Lab) is to produce a color space that is more perceptually linear than other color spaces. Perceptually linear means that a change of the same amount in a color value should produce a change of about the same visual importance. CIELAB has almost entirely replaced an alternative related Lab color space "Hunter Lab". This space is commonly used for surface colors, but not for mixtures of (transmitted) light.
    /// </summary>
    public struct CIELAB
        : IColor
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
        /// The equals.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Equals(IColor other)
        {
            var (r0, g0, b0, a0) = ToRGBATuple();
            var (r1, g1, b1, a1) = other.ToRGBATuple();
            return r0 == r1 && g0 == g1 && b0 == b1 && a0 == a1;
        }

        /// <summary>
        /// The to RGBA tuple.
        /// </summary>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4}"/>.</returns>
        public (byte red, byte green, byte blue, byte alpha) ToRGBATuple()
            => throw new NotImplementedException();

        /// <summary>
        /// The to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The formatProvider.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string ToString(string format, IFormatProvider formatProvider) => throw new NotImplementedException();
    }
}
