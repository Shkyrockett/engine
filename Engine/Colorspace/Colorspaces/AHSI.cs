// <copyright file="AHSI.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using static System.Math;

namespace Engine.Colorspace
{
    /// <summary>
    /// AHSI color structure.
    /// </summary>
    public struct AHSI
        : IColor<AHSI>
    {
        /// <summary>
        ///
        /// </summary>
        public static readonly AHSI Empty = new AHSI();

        ///// <summary>
        ///// Initializes a new instance of the <see cref="AHSI"/> class.
        ///// </summary>
        //public AHSI()
        //    : this(0, 0, 0, 0)
        //{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="AHSI"/> class.
        /// </summary>
        /// <param name="color"></param>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://dystopiancode.blogspot.com/2012/02/hsi-rgb-conversion-algorithms-in-c.html
        /// https://github.com/dystopiancode/colorspace-conversions
        /// </acknowledgment>
        public AHSI(ARGB color)
        {
            double r = color.Red;
            double g = color.Green;
            double b = color.Blue;
            var m = Min(r, g);
            m = Min(m, b);
            var M = Max(r, g);
            M = Max(m, b);
            var c = M - m;

            var I = (1d / 3d) * (r + g + b);
            double H = 0;
            double S;
            if (c == 0)
            {
                H = 0d;
                S = 0d;
            }
            else
            {
                if (M == r)
                    H = IEEERemainder(((g - b) / c), 6d);
                else if (M == g)
                    H = (b - r) / c + 2d;
                else if (M == b)
                    H = (r - g) / c + 4d;
                H *= 60d;
                S = 1d - (m / I);
            }

            Alpha = color.Alpha;
            Hue = H;
            Saturation = S;
            Intensity = I;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AHSI"/> class.
        /// </summary>
        /// <param name="hue">Hue color component.</param>
        /// <param name="saturation">Saturation color component.</param>
        /// <param name="intensity">Intensity color component.</param>
        public AHSI(double hue, double saturation, double intensity)
            : this(0, hue, saturation, intensity)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AHSI"/> class.
        /// </summary>
        /// <param name="alpha">Alpha color component.</param>
        /// <param name="hue">Hue color component.</param>
        /// <param name="saturation">Saturation color component.</param>
        /// <param name="intensity">Intensity color component.</param>
        public AHSI(byte alpha, double hue, double saturation, double intensity)
        {
            Alpha = alpha;
            Hue = hue;
            Saturation = saturation;
            Intensity = intensity;
        }

        /// <summary>
        /// Gets or sets the alpha color value.
        /// </summary>
        public byte Alpha { get; set; }

        /// <summary>
        /// Gets or sets the hue color value.
        /// </summary>
        public double Hue { get; set; }

        /// <summary>
        /// Gets or sets the saturation color value.
        /// </summary>
        public double Saturation { get; set; }

        /// <summary>
        /// Gets or sets the intensity color value.
        /// </summary>
        public double Intensity { get; set; }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://dystopiancode.blogspot.com/2012/02/hsi-rgb-conversion-algorithms-in-c.html
        /// https://github.com/dystopiancode/colorspace-conversions
        /// </acknowledgment>
        public ARGB ToColor()
        {
            var R = 0d;
            var G = 0d;
            var B = 0d;
            var h = Hue;
            var s = Saturation;
            var i = Intensity;
            var HUE_UPPER_LIMIT = 360.0;

            if (h >= 0.0 && h <= (HUE_UPPER_LIMIT / 3.0))
            {
                B = (1.0 / 3.0) * (1.0 - s);
                R = (1.0 / 3.0) * ((s * Cos(h)) / Cos(60.0 - h));
                G = 1.0 - (B + R);
            }
            else if (h > (HUE_UPPER_LIMIT / 3.0) && h <= (2.0 * HUE_UPPER_LIMIT / 3.0))
            {
                h -= (HUE_UPPER_LIMIT / 3.0);
                R = (1.0 / 3.0) * (1.0 - s);
                G = (1.0 / 3.0) * ((s * Cos(h)) / Cos(60.0 - h));
                B = 1.0 - (G + R);
            }
            else /* h>240 h<360 */
            {
                h -= (2.0 * HUE_UPPER_LIMIT / 3.0);
                G = (1.0 / 3.0) * (1.0 - s);
                B = (1.0 / 3.0) * ((s * Cos(h)) / Cos(60.0 - h));
                R = 1.0 - (G + B);
            }

            return new ARGB(Alpha, (byte)R, (byte)G, (byte)B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(AHSI other)
            => throw new NotImplementedException();

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
