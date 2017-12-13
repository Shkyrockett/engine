// <copyright file="CIExyYTriple.cs" company="Shkyrockett" >
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
    /// The CI exy y triple struct.
    /// </summary>
    public struct CIExyYTriple
        : IColor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CIExyYTriple"/> class.
        /// </summary>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        public CIExyYTriple(CIExyY red, CIExyY green, CIExyY blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        /// <summary>
        /// Gets or sets the red.
        /// </summary>
        public CIExyY Red { get; set; }

        /// <summary>
        /// Gets or sets the green.
        /// </summary>
        public CIExyY Green { get; set; }

        /// <summary>
        /// Gets or sets the blue.
        /// </summary>
        public CIExyY Blue { get; set; }

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
