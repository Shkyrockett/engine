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

        public bool Equals(IColor other)
        {
            var a = ToARGBTuple();
            var b = other.ToARGBTuple();
            return a.A == b.A && a.R == b.R && a.G == b.G && a.B == b.B;
        }

        public (byte A, byte R, byte G, byte B) ToARGBTuple() => throw new NotImplementedException();

        public string ToString(string format, IFormatProvider formatProvider) => throw new NotImplementedException();
    }
}
