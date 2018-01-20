﻿// <copyright file="CIExyY.cs" company="Shkyrockett" >
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
    /// The CI exy y struct.
    /// </summary>
    public struct CIExyY
        : IColor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CIExyY"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="y2">The y2.</param>
        public CIExyY(double x, double y1, double y2)
        {
            X = x;
            Y1 = y1;
            Y2 = y2;
        }

        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the y1.
        /// </summary>
        public double Y1 { get; set; }

        /// <summary>
        /// Gets or sets the y2.
        /// </summary>
        public double Y2 { get; set; }

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
