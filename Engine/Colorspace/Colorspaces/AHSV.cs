// <copyright file="AHSV.cs" company="Shkyrockett" >
//     Copyright (c) 2013 - 2017 Shkyrockett. All rights reserved.
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
    /// Alpha Hue Saturation Value color.
    /// </summary>
    public struct AHSV
        : IColor<AHSV>
    {
        /// <summary>
        ///
        /// </summary>
        public static readonly AHSV Empty = new AHSV();

        ///// <summary>
        ///// Initializes a new instance of the <see cref="AHSV"/> class.
        ///// </summary>
        //public AHSV()
        //    : this(0, 0, 0, 0)
        //{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="AHSV"/> class.
        /// </summary>
        /// <param name="color"></param>
        /// <remarks>
        /// https://www.cs.rit.edu/~ncs/color/t_convert.html
        /// h = [0,360], s = [0,1], v = [0,1]
        ///		if s == 0, then h = -1 (undefined)
        /// </remarks>
        public AHSV(ARGB color)
        {
            var red = 1.0 - (color.Red / 255.0);
            var green = 1.0 - (color.Green / 255.0);
            var blue = 1.0 - (color.Blue / 255.0);

            var min = Min(red, green);
            min = Min(min, blue);
            var max = Max(red, green);
            max = Max(max, blue);
            double h;
            double s;
            var v = max;               // v
            var delta = max - min;
            if (max != 0)
            {
                s = delta / max;       // s
            }
            else
            {
                // r = g = b = 0		// s = 0, v is undefined
                s = 0;
                h = -1;

                Alpha = color.Alpha;
                Hue = h;
                Saturation = s;
                Value = v;
            }

            if (red == max)
                h = (green - blue) / delta;       // between yellow & magenta
            else if (green == max)
                h = 2 + (blue - red) / delta;   // between cyan & yellow
            else
                h = 4 + (red - green) / delta;   // between magenta & cyan
            h *= 60;               // degrees
            if (h < 0)
                h += 360;

            Alpha = color.Alpha;
            Hue = h;
            Saturation = s;
            Value = v;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AHSV"/> class.
        /// </summary>
        /// <param name="hue">Hue color component.</param>
        /// <param name="saturation">Saturation color component.</param>
        /// <param name="value">Value color component.</param>
        public AHSV(double hue, double saturation, double value)
            : this(0, hue, saturation, value)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AHSV"/> class.
        /// </summary>
        /// <param name="alpha">Alpha color component.</param>
        /// <param name="hue">Hue color component.</param>
        /// <param name="saturation">Saturation color component.</param>
        /// <param name="value">Value color component.</param>
        public AHSV(byte alpha, double hue, double saturation, double value)
        {
            Alpha = alpha;
            Hue = hue;
            Saturation = saturation;
            Value = value;
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
        /// Gets or sets the value color value.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// https://www.cs.rit.edu/~ncs/color/t_convert.html
        /// h = [0,360], s = [0,1], v = [0,1]
        ///		if s == 0, then h = -1 (undefined)
        /// </remarks>
        public ARGB ToColor()
        {
            double r;
            double g;
            double b;
            int i;
            double f, p, q, t;
            if (Saturation == 0)
            {
                // achromatic (gray)
                r = g = b = Value;

                r = (1.0 - r) * 255.0 + 0.5;
                g = (1.0 - g) * 255.0 + 0.5;
                b = (1.0 - b) * 255.0 + 0.5;

                return new ARGB(Alpha, (byte)r, (byte)g, (byte)b);
            }

            Hue /= 60;            // sector 0 to 5
            i = (int)Floor(Hue);
            f = Hue - i;          // factorial part of h
            p = Value * (1 - Saturation);
            q = Value * (1 - Saturation * f);
            t = Value * (1 - Saturation * (1 - f));
            switch (i)
            {
                case 0:
                    r = Value;
                    g = t;
                    b = p;
                    break;
                case 1:
                    r = q;
                    g = Value;
                    b = p;
                    break;
                case 2:
                    r = p;
                    g = Value;
                    b = t;
                    break;
                case 3:
                    r = p;
                    g = q;
                    b = Value;
                    break;
                case 4:
                    r = t;
                    g = p;
                    b = Value;
                    break;
                case 5:
                default:
                    r = Value;
                    g = p;
                    b = q;
                    break;
            }

            r = (1.0 - r) * 255.0 + 0.5;
            g = (1.0 - g) * 255.0 + 0.5;
            b = (1.0 - b) * 255.0 + 0.5;

            return new ARGB(Alpha, (byte)r, (byte)g, (byte)b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(AHSV other)
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
