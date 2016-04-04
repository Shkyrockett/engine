﻿using System;
using System.Drawing;

namespace Engine.Imaging.ColorSpace
{
    /// <summary>
    /// AHSI color structure.
    /// </summary>
    public class AHSI
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
        /// Intensity color component.
        /// </summary>
        private double intensity;

        /// <summary>
        /// Initializes a new instance of the <see cref="AHSI"/> class.
        /// </summary>
        public AHSI()
            : this(0, 0, 0, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AHSI"/> class.
        /// </summary>
        /// <param name="color"></param>
        /// <remarks>
        /// http://dystopiancode.blogspot.com/2012/02/hsi-rgb-conversion-algorithms-in-c.html
        /// https://github.com/dystopiancode/colorspace-conversions
        /// </remarks>
        public AHSI(Color color)
        {
            double r = color.R;
            double g = color.G;
            double b = color.B;
            double m = Math.Min(r, g);
            m = Math.Min(m, b);
            double M = Math.Max(r, g);
            M = Math.Max(m, b);
            double c = M - m;

            double I = (1.0 / 3.0) * (r + g + b);
            double H = 0;
            double S;
            if (c == 0)
            {
                H = 0.0;
                S = 0.0;
            }
            else
            {
                if (M == r)
                {
                    H = Math.IEEERemainder(((g - b) / c), 6.0);
                }
                else if (M == g)
                {
                    H = (b - r) / c + 2.0;
                }
                else if (M == b)
                {
                    H = (r - g) / c + 4.0;
                }
                H *= 60.0;
                S = 1.0 - (m / I);
            }

            alpha = color.A;
            hue = H;
            saturation = S;
            intensity = I;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AHSI"/> class.
        /// </summary>
        /// <param name="hue">Hue color component.</param>
        /// <param name="saturation">Saturation color component.</param>
        /// <param name="intensity">Intensity color component.</param>
        public AHSI(double hue, double saturation, double intensity)
            : this(0, hue, saturation, intensity)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AHSI"/> class.
        /// </summary>
        /// <param name="alpha">Alpha color component.</param>
        /// <param name="hue">Hue color component.</param>
        /// <param name="saturation">Saturation color component.</param>
        /// <param name="intensity">Intensity color component.</param>
        public AHSI(byte alpha, double hue, double saturation, double intensity)
        {
            this.alpha = alpha;
            this.hue = hue;
            this.saturation = saturation;
            this.intensity = intensity;
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
        /// Gets or sets the intensity color value.
        /// </summary>
        public double Intensity
        {
            get { return intensity; }
            set { intensity = value; }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://dystopiancode.blogspot.com/2012/02/hsi-rgb-conversion-algorithms-in-c.html
        /// https://github.com/dystopiancode/colorspace-conversions
        /// </remarks>
        public Color ToColor()
        {
            double R = 0;
            double G = 0;
            double B = 0;
            double h = hue;
            double s = saturation;
            double i = intensity;
            double HUE_UPPER_LIMIT = 360.0;

            if (h >= 0.0 && h <= (HUE_UPPER_LIMIT / 3.0))
            {
                B = (1.0 / 3.0) * (1.0 - s);
                R = (1.0 / 3.0) * ((s * Math.Cos(h)) / Math.Cos(60.0 - h));
                G = 1.0 - (B + R);
            }
            else if (h > (HUE_UPPER_LIMIT / 3.0) && h <= (2.0 * HUE_UPPER_LIMIT / 3.0))
            {
                h -= (HUE_UPPER_LIMIT / 3.0);
                R = (1.0 / 3.0) * (1.0 - s);
                G = (1.0 / 3.0) * ((s * Math.Cos(h)) / Math.Cos(60.0 - h));
                B = 1.0 - (G + R);

            }
            else /* h>240 h<360 */
            {
                h -= (2.0 * HUE_UPPER_LIMIT / 3.0);
                G = (1.0 / 3.0) * (1.0 - s);
                B = (1.0 / 3.0) * ((s * Math.Cos(h)) / Math.Cos(60.0 - h));
                R = 1.0 - (G + B);
            }

            return Color.FromArgb(alpha, (byte)R, (byte)G, (byte)B);
        }
    }
}
