// <copyright file="YIQ.cs" company="Shkyrockett" >
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
    /// The YIQ struct.
    /// </summary>
    public struct YIQ
        : IColor
    {
        /// <summary>
        /// The empty (readonly). Value: new YIQ().
        /// </summary>
        public static readonly YIQ Empty = new YIQ();

        ///// <summary>
        /////
        ///// </summary>
        //public YIQ()
        //    : this(0, 0, 0, 0)
        //{ }


        /// <summary>
        /// Initializes a new instance of the <see cref="YIQ"/> class.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <acknowledgment>
        /// https://github.com/dystopiancode/colorspace-conversions/blob/master/colorspace-conversions/colorspace-conversions.c
        /// </acknowledgment>
        public YIQ(ARGB color)
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
        /// Initializes a new instance of the <see cref="YIQ"/> class.
        /// </summary>
        /// <param name="y">The y.</param>
        /// <param name="i">The i.</param>
        /// <param name="q">The q.</param>
        public YIQ(double y, double i, double q)
            : this(0, y, i, q)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="YIQ"/> class.
        /// </summary>
        /// <param name="alpha">The alpha.</param>
        /// <param name="y">The y.</param>
        /// <param name="i">The i.</param>
        /// <param name="q">The q.</param>
        public YIQ(byte alpha, double y, double i, double q)
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
            var (A0, R0, G0, B0) = ToARGBTuple();
            var (A1, R1, G1, B1) = other.ToARGBTuple();
            return A0 == A1 && R0 == R1 && G0 == G1 && B0 == B1;
        }

        /// <summary>
        /// The to color.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="y">The y.</param>
        /// <param name="i">The i.</param>
        /// <param name="q">The q.</param>
        /// <returns>The <see cref="ARGB"/>.</returns>
        /// <acknowledgment>
        /// https://github.com/dystopiancode/colorspace-conversions/blob/master/colorspace-conversions/colorspace-conversions.c
        /// </acknowledgment>
        public ARGB ToColor(byte a, double y, double i, double q)
        {
            var r = (byte)(y + 0.9563 * i + 0.6210 * q);
            var g = (byte)(y - 0.2721 * i - 0.6474 * q);
            var b = (byte)(y - 1.1070 * i + 1.7046 * q);

            return new ARGB(a, r, g, b);
        }

        /// <summary>
        /// The to ARGB tuple.
        /// </summary>
        /// <returns>The <see cref="(byte A, byte R, byte G, byte B)"/>.</returns>
        public (byte A, byte R, byte G, byte B) ToARGBTuple()
            => (Alpha, (byte)(Y + 0.9563 * I + 0.6210 * Q), (byte)(Y - 0.2721 * I - 0.6474 * Q), (byte)(Y - 1.1070 * I + 1.7046 * Q));

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
