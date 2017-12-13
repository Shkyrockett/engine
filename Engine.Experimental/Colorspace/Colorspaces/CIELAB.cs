// <copyright file="CIELAB.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2017 Shkyrockett. All rights reserved.
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
    /// </summary>
    public struct CIELAB
        : IColor
    {
        ///// <summary>
        ///// Initializes a new instance of the Lightness and Channels A and B color space structure.
        ///// </summary>
        //public CIELAB()
        //    : this(0, 0, 0)
        //{ }

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
            var (A0, R0, G0, B0) = ToARGBTuple();
            var (A1, R1, G1, B1) = other.ToARGBTuple();
            return A0 == A1 && R0 == R1 && G0 == G1 && B0 == B1;
        }

        /// <summary>
        /// The to ARGB tuple.
        /// </summary>
        /// <returns>The <see cref="(byte A, byte R, byte G, byte B)"/>.</returns>
        public (byte A, byte R, byte G, byte B) ToARGBTuple()
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
