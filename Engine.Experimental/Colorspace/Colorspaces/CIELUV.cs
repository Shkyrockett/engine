// <copyright file="CIELUV.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2018 Shkyrockett. All rights reserved.
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
    /// </summary>
    public struct CIELUV
        : IColor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CIELUV"/> class.
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
            var (A0, R0, G0, B0) = ToARGBTuple();
            var (A1, R1, G1, B1) = other.ToARGBTuple();
            return A0 == A1 && R0 == R1 && G0 == G1 && B0 == B1;
        }

        /// <summary>
        /// The to ARGB tuple.
        /// </summary>
        /// <returns>The <see cref="(byte A, byte R, byte G, byte B)"/>.</returns>
        public (byte A, byte R, byte G, byte B) ToARGBTuple() => throw new NotImplementedException();

        /// <summary>
        /// The to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The formatProvider.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string ToString(string format, IFormatProvider formatProvider) => throw new NotImplementedException();
    }
}
