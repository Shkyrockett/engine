// <copyright file="AHSL.cs" company="Shkyrockett" >
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
    /// AHSL Color
    /// </summary>
    public struct AHSL
        : IColor
    {
        /// <summary>
        ///
        /// </summary>
        public static readonly AHSL Empty = new AHSL();

        /// <summary>
        /// Alpha color component.
        /// </summary>
        private double alpha;

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
        ///// Initializes a new instance of the <see cref="AHSL"/> class.
        ///// </summary>
        //public AHSL()
        //    : this(0, 0, 0, 0)
        //{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="AHSL"/> class Converted from RGB to HSL.
        /// </summary>
        /// <param name="color">A Color to convert</param>
        /// <remarks>
        /// Takes advantage of whats already built in to .NET by using the Color.GetHue,
        /// Color.GetSaturation and Color.GetBrightness methods
        /// we store hue as 0-1 as opposed to 0-360
        /// </remarks>
        /// <returns>An HSL value</returns>
        public AHSL(ARGB color)
            : this(color.Alpha, color.GetHue() / 360.0, color.GetSaturation(), color.GetBrightness())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AHSL"/> class.
        /// </summary>
        /// <param name="hue">Hue color component.</param>
        /// <param name="saturation">Saturation color component.</param>
        /// <param name="luminance">Luminance color component.</param>
        public AHSL(double hue, double saturation, double luminance)
            : this(0, hue, saturation, luminance)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AHSL"/> class.
        /// </summary>
        /// <param name="alpha">Alpha color component.</param>
        /// <param name="hue">Hue color component.</param>
        /// <param name="saturation">Saturation color component.</param>
        /// <param name="luminance">Luminance color component.</param>
        public AHSL(double alpha, double hue, double saturation, double luminance)
        {
            this.alpha = alpha > 1 ? 1 : alpha < 0 ? 0 : alpha;
            this.hue = hue > 1 ? 1 : hue < 0 ? 0 : hue;
            this.saturation = saturation > 1 ? 1 : saturation < 0 ? 0 : saturation;
            this.luminance = luminance > 1 ? 1 : luminance < 0 ? 0 : luminance;
        }

        /// <summary>
        /// Gets or sets the alpha color value.
        /// </summary>
        public double Alpha
        {
            get { return alpha; }
            set
            {
                //alpha = value;
                alpha = value > 1 ? 1 : value < 0 ? 0 : value;
            }
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
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IColor other)
        {
            var a = ToARGBTuple();
            var b = other.ToARGBTuple();
            return a.A == b.A && a.R == b.R && a.G == b.G && a.B == b.B;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public (byte A, byte R, byte G, byte B) ToARGBTuple() => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string ToString(string format, IFormatProvider formatProvider)
            => throw new NotImplementedException();
    }
}
