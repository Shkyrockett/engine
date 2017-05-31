// <copyright file="CIELAB.cs" company="Shkyrockett" >
//     Copyright (c) 2013 - 2017 Shkyrockett. All rights reserved.
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
        : IColor<CIELAB>
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

        public bool Equals(CIELAB other) => throw new NotImplementedException();
        public string ToString(string format, IFormatProvider formatProvider) => throw new NotImplementedException();
    }
}
