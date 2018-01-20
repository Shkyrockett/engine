// <copyright file="YUVA.cs" company="Shkyrockett" >
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
    /// The YUVA struct.
    /// </summary>
    public struct YUVA
        : IColor
    {
        /// <summary>
        /// The empty (readonly). Value: new YUVA().
        /// </summary>
        public static readonly YUVA Empty = new YUVA();

        /// <summary>
        /// Initializes a new instance of the <see cref="AYUV"/> class.
        /// </summary>
        /// <param name="value">A standard color.</param>
        public YUVA(RGBA value)
        {
            //() = Colorspaces.RGBAFColorToYUVAColor()
            Alpha = value.Alpha;
            Y = (byte)(0.299 * value.Red + 0.587 * value.Green + 0.114 * value.Blue);
            U = (byte)(-0.1687 * value.Red - 0.3313 * value.Green + 0.5 * value.Blue + 128);
            V = (byte)(0.5 * value.Red - 0.4187 * value.Green - 0.0813 * value.Blue + 128);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YUVA"/> class.
        /// </summary>
        /// <param name="y">Y color component.</param>
        /// <param name="u">U color component.</param>
        /// <param name="v">V color component.</param>
        public YUVA(byte y, byte u, byte v)
            : this(y, u, v, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="YUVA"/> class.
        /// </summary>
        /// <param name="y">Y color component.</param>
        /// <param name="u">U color component.</param>
        /// <param name="v">V color component.</param>
        /// <param name="alpha">Alpha color component.</param>
        public YUVA(byte y, byte u, byte v, byte alpha)
        {
            Y = y;
            U = u;
            V = v;
            Alpha = alpha;
        }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        public byte Y { get; set; }

        /// <summary>
        /// Gets or sets the v.
        /// </summary>
        public byte V { get; set; }

        /// <summary>
        /// Gets or sets the u.
        /// </summary>
        public byte U { get; set; }

        /// <summary>
        /// Gets or sets the alpha.
        /// </summary>
        public byte Alpha { get; set; }

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
        /// The to color.
        /// </summary>
        /// <returns>The <see cref="RGBA"/>.</returns>
        public RGBA ToColor()
            => new RGBA(ToRGBATuple());

        /// <summary>
        /// The to color rounded.
        /// </summary>
        /// <returns>The <see cref="RGBA"/>.</returns>
        public RGBA ToColorRounded()
        {
            var r = (byte)(Y + 1.140 * V);
            var g = (byte)(Y - 0.395 * U - 0.581 * V);
            var b = (byte)(Y + 2.032 * U);
            return new RGBA(r, g, b, Alpha);
        }

        /// <summary>
        /// The to RGBA tuple.
        /// </summary>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4}"/>.</returns>
        public (byte red, byte green, byte blue, byte alpha) ToRGBATuple()
            => (
            (byte)(Y + 0 * U + 1.13983 * V),
            (byte)(Y + -0.39465 * U + -0.58060 * V),
            (byte)(Y + -0.03211 * U + 0 * V),
            Alpha);

        /// <summary>
        /// The to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The formatProvider.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string ToString(string format, IFormatProvider formatProvider)
            => throw new NotImplementedException();
    }
}
