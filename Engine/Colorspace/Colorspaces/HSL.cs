// <copyright file="HSL.cs" company="Shkyrockett" >
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
    /// HSL Color
    /// </summary>
    public struct HSL
        : IColor
    {
        /// <summary>
        /// The empty (readonly). Value: new HSL().
        /// </summary>
        public static readonly HSL Empty = new HSL();

        /// <summary>
        /// Hue color component.
        /// </summary>
        private double hue;

        /// <summary>
        /// Saturation color component.
        /// </summary>
        private double saturation;

        /// <summary>
        /// Luminance color component.
        /// </summary>
        private double luminance;

        ///// <summary>
        ///// Initializes a new instance of the <see cref="HSL"/> class.
        ///// </summary>
        //public HSL()
        //    : this(0, 0, 0)
        //{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="HSL"/> class Converted from RGB to HSL.
        /// </summary>
        /// <param name="color">A Color to convert</param>
        /// <remarks>
        /// Takes advantage of whats already built in to .NET by using the Color.GetHue,
        /// Color.GetSaturation and Color.GetBrightness methods.
        /// Note: Storing hue as 0-1 as opposed to 0-360.
        /// </remarks>
        /// <returns>An HSL value</returns>
        public HSL(ARGB color)
            : this(color.GetHue() / 360d, color.GetSaturation(), color.GetBrightness())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HSL"/> class.
        /// </summary>
        /// <param name="hue">Hue color component.</param>
        /// <param name="saturation">Saturation color component.</param>
        /// <param name="luminance">Luminance color component.</param>
        public HSL(double hue, double saturation, double luminance)
        {
            this.hue = hue;
            this.saturation = saturation;
            this.luminance = luminance;
        }

        /// <summary>
        /// Gets or sets the hue color value.
        /// </summary>
        public double Hue
        {
            get { return hue; }
            set
            {
                //hue = value;
                hue = value > 1 ? 1 : value < 0 ? 0 : value;
            }
        }

        /// <summary>
        /// Gets or sets the saturation color value.
        /// </summary>
        public double Saturation
        {
            get { return saturation; }
            set
            {
                //saturation = value;
                saturation = value > 1 ? 1 : value < 0 ? 0 : value;
            }
        }

        /// <summary>
        /// Gets or sets the luminance color value.
        /// </summary>
        public double Luminance
        {
            get { return luminance; }
            set
            {
                //luminance = value;
                luminance = value > 1 ? 1 : value < 0 ? 0 : value;
            }
        }

        /// <summary>
        /// Converts RGB to HSL
        /// </summary>
        /// <remarks>Takes advantage of whats already built in to .NET by using the Color.GetHue, Color.GetSaturation and Color.GetBrightness methods</remarks>
        /// <param name="c">A Color to convert</param>
        /// <returns>An HSL value</returns>
        public static HSL FromRGB(ARGB c)
            => new HSL(c);

        /// <summary>
        /// The to ARGB tuple.
        /// </summary>
        /// <returns>The <see cref="(byte A, byte R, byte G, byte B)"/>.</returns>
        public (byte A, byte R, byte G, byte B) ToARGBTuple()
            => throw new NotImplementedException();

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
        /// The to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The formatProvider.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string ToString(string format, IFormatProvider formatProvider)
            => throw new NotImplementedException();
    }
}
