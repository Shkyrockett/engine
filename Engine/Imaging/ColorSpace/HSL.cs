// <copyright file="HSL.cs" company="Shkyrockett" >
//     Copyright (c) 2013 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Drawing;

namespace Engine.Imaging.ColorSpace
{
    /// <summary>
    /// HSL Color
    /// </summary>
    public class HSL
    {
        /// <summary>
        ///
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

        /// <summary>
        /// Initializes a new instance of the <see cref="HSL"/> class.
        /// </summary>
        public HSL()
            : this(0, 0, 0)
        { }

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
        public HSL(Color color)
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
        public static HSL FromRGB(Color c) => new HSL(c);
    }
}
