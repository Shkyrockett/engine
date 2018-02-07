// <copyright file="YIQA.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
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
    /// The YIQA struct.
    /// </summary>
    public struct YIQA
        : IColor
    {
        #region Implementations
        /// <summary>
        /// The empty (readonly). Value: new YIQ().
        /// </summary>
        public static readonly YIQA Empty = new YIQA();
        #endregion Implementations

        /// <summary>
        /// Initializes a new instance of the <see cref="YIQA"/> class.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <acknowledgment>
        /// https://github.com/dystopiancode/colorspace-conversions/blob/master/colorspace-conversions/colorspace-conversions.c
        /// </acknowledgment>
        public YIQA(RGBA color)
        {
            double r = color.Red;
            double g = color.Green;
            double b = color.Blue;

            Alpha = color.Alpha;
            Y = 0.299900 * r + 0.587000 * g + 0.114000 * b;
            I = 0.595716 * r - 0.274453 * g - 0.321264 * b;
            Q = 0.211456 * r - 0.522591 * g + 0.311350 * b;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YIQA"/> class.
        /// </summary>
        /// <param name="y">The y.</param>
        /// <param name="i">The i.</param>
        /// <param name="q">The q.</param>
        public YIQA(double y, double i, double q)
            : this(0, y, i, q)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="YIQA"/> class.
        /// </summary>
        /// <param name="alpha">The alpha.</param>
        /// <param name="y">The y.</param>
        /// <param name="i">The i.</param>
        /// <param name="q">The q.</param>
        public YIQA(byte alpha, double y, double i, double q)
        {
            Alpha = alpha;
            Y = y;
            I = i;
            Q = q;
        }

        /// <summary>
        /// Gets or sets the alpha.
        /// </summary>
        public byte Alpha { get; set; }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the I.
        /// </summary>
        public double I { get; set; }

        /// <summary>
        /// Gets or sets the q.
        /// </summary>
        public double Q { get; set; }

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
        /// <param name="a">The a.</param>
        /// <param name="y">The y.</param>
        /// <param name="i">The i.</param>
        /// <param name="q">The q.</param>
        /// <returns>The <see cref="RGBA"/>.</returns>
        /// <acknowledgment>
        /// https://github.com/dystopiancode/colorspace-conversions/blob/master/colorspace-conversions/colorspace-conversions.c
        /// </acknowledgment>
        public RGBA ToColor(byte a, double y, double i, double q)
        {
            var r = (byte)(y + 0.9563 * i + 0.6210 * q);
            var g = (byte)(y - 0.2721 * i - 0.6474 * q);
            var b = (byte)(y - 1.1070 * i + 1.7046 * q);
            return new RGBA(r, g, b, a);
        }

        /// <summary>
        /// The to RGBA tuple.
        /// </summary>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4}"/>.</returns>
        public (byte red, byte green, byte blue, byte alpha) ToRGBATuple()
            => Colorspaces.RGBAFColorToRGBAColor(Colorspaces.YIQAColorToRGBAFColor(Y, I, Q, Alpha));

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
