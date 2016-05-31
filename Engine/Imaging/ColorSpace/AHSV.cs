// <copyright file="AHSV.cs" company="Shkyrockett"> 
//      Copyright © Shkyrockett 2013 all rights reserved. 
// </copyright> 
// <author id="shkyrockett">Alma Jenks</author>
// <summary></summary>

using System.Drawing;
using static System.Math;

namespace Engine.Imaging.ColorSpace
{
    /// <summary>
    /// Alpha Hue Saturation Value color.
    /// </summary>
    public class AHSV
    {
        /// <summary>
        /// Alpha color component.
        /// </summary>
        private byte alpha;

        /// <summary>
        /// Hue color component.
        /// </summary>
        private double hue;

        /// <summary>
        /// Saturation color component.
        /// </summary>
        private double saturation;

        /// <summary>
        /// Value color component.
        /// </summary>
        private double value;

        /// <summary>
        /// Initializes a new instance of the <see cref="AHSV"/> class.
        /// </summary>
        public AHSV()
            : this(0, 0, 0, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AHSV"/> class.
        /// </summary>
        /// <param name="color"></param>
        /// <remarks>
        /// https://www.cs.rit.edu/~ncs/color/t_convert.html
        /// h = [0,360], s = [0,1], v = [0,1]
        ///		if s == 0, then h = -1 (undefined)
        /// </remarks>
        public AHSV(Color color)
        {
            double red = 1.0 - (color.R / 255.0);
            double green = 1.0 - (color.G / 255.0);
            double blue = 1.0 - (color.B / 255.0);

            double min = Min(red, green);
            min = Min(min, blue);
            double max = Max(red, green);
            max = Max(max, blue);
            double h;
            double s;
            double v = max;               // v
            double delta = max - min;
            if (max != 0)
                s = delta / max;       // s
            else
            {
                // r = g = b = 0		// s = 0, v is undefined
                s = 0;
                h = -1;

                alpha = color.A;
                hue = h;
                saturation = s;
                value = v;
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

            alpha = color.A;
            hue = h;
            saturation = s;
            value = v;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AHSV"/> class.
        /// </summary>
        /// <param name="hue">Hue color component.</param>
        /// <param name="saturation">Saturation color component.</param>
        /// <param name="value">Value color component.</param>
        public AHSV(double hue, double saturation, double value)
            : this(0, hue, saturation, value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AHSV"/> class.
        /// </summary>
        /// <param name="alpha">Alpha color component.</param>
        /// <param name="hue">Hue color component.</param>
        /// <param name="saturation">Saturation color component.</param>
        /// <param name="value">Value color component.</param>
        public AHSV(byte alpha, double hue, double saturation, double value)
        {
            this.alpha = alpha;
            this.hue = hue;
            this.saturation = saturation;
            this.value = value;
        }

        /// <summary>
        /// Gets or sets the alpha color value.
        /// </summary>
        public byte Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }

        /// <summary>
        /// Gets or sets the hue color value.
        /// </summary>
        public double Hue
        {
            get { return hue; }
            set { hue = value; }
        }

        /// <summary>
        /// Gets or sets the saturation color value.
        /// </summary>
        public double Saturation
        {
            get { return saturation; }
            set { saturation = value; }
        }

        /// <summary>
        /// Gets or sets the value color value.
        /// </summary>
        public double Value
        {
            get { return value; }
            set { this.value = value; }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// https://www.cs.rit.edu/~ncs/color/t_convert.html
        /// h = [0,360], s = [0,1], v = [0,1]
        ///		if s == 0, then h = -1 (undefined)
        /// </remarks>
        public Color ToColor()
        {
            double r;
            double g;
            double b;
            int i;
            double f, p, q, t;
            if (saturation == 0)
            {
                // achromatic (gray)
                r = g = b = value;

                r = (1.0 - r) * 255.0 + 0.5;
                g = (1.0 - g) * 255.0 + 0.5;
                b = (1.0 - b) * 255.0 + 0.5;

                return Color.FromArgb(alpha, (byte)r, (byte)g, (byte)b);
            }

            hue /= 60;            // sector 0 to 5
            i = (int)Floor(hue);
            f = hue - i;          // factorial part of h
            p = value * (1 - saturation);
            q = value * (1 - saturation * f);
            t = value * (1 - saturation * (1 - f));
            switch (i)
            {
                case 0:
                    r = value;
                    g = t;
                    b = p;
                    break;
                case 1:
                    r = q;
                    g = value;
                    b = p;
                    break;
                case 2:
                    r = p;
                    g = value;
                    b = t;
                    break;
                case 3:
                    r = p;
                    g = q;
                    b = value;
                    break;
                case 4:
                    r = t;
                    g = p;
                    b = value;
                    break;
                case 5:
                default:
                    r = value;
                    g = p;
                    b = q;
                    break;
            }

            r = (1.0 - r) * 255.0 + 0.5;
            g = (1.0 - g) * 255.0 + 0.5;
            b = (1.0 - b) * 255.0 + 0.5;

            return Color.FromArgb(alpha, (byte)r, (byte)g, (byte)b);
        }
    }
}
