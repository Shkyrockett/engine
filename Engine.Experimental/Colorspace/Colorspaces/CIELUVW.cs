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
    public struct CIELUVW
        : IColor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CIELUVW"/> class.
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
        public double Luminance { get; set; }

        /// <summary>
        /// Gets or sets the u.
        /// </summary>
        public double U { get; set; }

        /// <summary>
        /// Gets or sets the v or blue to yellow value.
        /// </summary>
        public double V { get; set; }

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
        public (byte red, byte green, byte blue, byte alpha) ToRGBATuple() => throw new NotImplementedException();

        /// <summary>
        /// The to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The formatProvider.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string ToString(string format, IFormatProvider formatProvider) => throw new NotImplementedException();
    }
}
