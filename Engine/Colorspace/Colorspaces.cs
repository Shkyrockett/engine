﻿// <copyright file="Colorspaces.cs" company="Shkyrockett" >
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
    ///
    /// </summary>
    public static class Colorspaces
    {
        /// <summary>
        /// Sets the absolute brightness of a color
        /// </summary>
        /// <param name="color">Original color</param>
        /// <param name="brightness">The luminance level to impose</param>
        /// <returns>an adjusted color</returns>
        public static ARGB SetBrightness(this ARGB color, double brightness)
        {
            var hsl = new AHSL(color)
            {
                Luminance = brightness
            };

            return ToARGB(hsl);
        }

        /// <summary>
        /// Modifies an existing brightness level
        /// </summary>
        /// <remarks>
        /// To reduce brightness use a number smaller than 1. To increase brightness use a number larger tan 1.
        /// </remarks>
        /// <param name="color">The original color</param>
        /// <param name="brightness">The luminance delta</param>
        /// <returns>An adjusted color</returns>
        public static ARGB ModifyBrightness(this ARGB color, double brightness)
        {
            var hsl = new AHSL(color);
            hsl.Luminance *= brightness;
            return ToARGB(hsl);
        }

        /// <summary>
        /// Sets the absolute saturation level
        /// </summary>
        /// <remarks>Accepted values 0-1</remarks>
        /// <param name="color">An original color</param>
        /// <param name="Saturation">The saturation value to impose</param>
        /// <returns>An adjusted color</returns>
        public static ARGB SetSaturation(this ARGB color, double Saturation)
        {
            var hsl = new AHSL(color)
            {
                Saturation = Saturation
            };

            return ToARGB(hsl);
        }

        /// <summary>
        /// Modifies an existing Saturation level.
        /// </summary>
        /// <remarks>
        /// To reduce Saturation use a number smaller than 1. To increase Saturation use a number larger tan 1.
        /// </remarks>
        /// <param name="color">The original color</param>
        /// <param name="Saturation">The saturation delta</param>
        /// <returns>An adjusted color</returns>
        public static ARGB ModifySaturation(this ARGB color, double Saturation)
        {
            var hsl = new AHSL(color);
            hsl.Saturation *= Saturation;
            return ToARGB(hsl);
        }

        /// <summary>
        /// Sets the absolute Hue level.
        /// </summary>
        /// <remarks>Accepted values 0-1</remarks>
        /// <param name="color">An original color</param>
        /// <param name="Hue">The Hue value to impose</param>
        /// <returns>An adjusted color</returns>
        public static ARGB SetHue(this ARGB color, double Hue)
        {
            var hsl = new AHSL(color)
            {
                Hue = Hue
            };

            return ToARGB(hsl);
        }

        /// <summary>
        /// Modifies an existing Hue level
        /// </summary>
        /// <remarks>
        /// To reduce Hue use a number smaller than 1. To increase Hue use a number larger tan 1
        /// </remarks>
        /// <param name="color">The original color</param>
        /// <param name="Hue">The Hue delta</param>
        /// <returns>An adjusted color</returns>
        public static ARGB ModifyHue(this ARGB color, double Hue)
        {
            var hsl = new AHSL(color);
            hsl.Hue *= Hue;
            return ToARGB(hsl);
        }

        /// <summary>
        /// Converts a color from HSL to RGB.
        /// </summary>
        /// <remarks>Adapted from the algorithm in Foley and Van-Dam</remarks>
        /// <param name="hsl">The HSL value</param>
        /// <returns>A Color structure containing the equivalent RGB values</returns>
        public static ARGB ToARGB(this HSL hsl)
        {
            double red = 0;
            double green = 0;
            double blue = 0;
            double temp1;
            double temp2;

            if (hsl.Luminance == 0)
            {
                red = green = blue = 0;
            }
            else
            {
                if (hsl.Saturation == 0)
                {
                    red = green = blue = hsl.Luminance;
                }
                else
                {
                    temp2 = ((hsl.Luminance <= 0.5) ? hsl.Luminance * (1.0 + hsl.Saturation) : hsl.Luminance + hsl.Saturation - (hsl.Luminance * hsl.Saturation));
                    temp1 = 2.0 * hsl.Luminance - temp2;
                    var t3 = new double[] { hsl.Hue + 1.0 / 3.0, hsl.Hue, hsl.Hue - 1.0 / 3.0 };
                    var clr = new double[] { 0, 0, 0 };
                    for (var i = 0; i < 3; i++)
                    {
                        if (t3[i] < 0)
                            t3[i] += 1.0;
                        if (t3[i] > 1)
                            t3[i] -= 1.0;
                        if (6.0 * t3[i] < 1.0)
                            clr[i] = temp1 + (temp2 - temp1) * t3[i] * 6.0;
                        else if (2.0 * t3[i] < 1.0)
                            clr[i] = temp2;
                        else if (3.0 * t3[i] < 2.0)
                            clr[i] = (temp1 + (temp2 - temp1) * ((2.0 / 3.0) - t3[i]) * 6.0);
                        else
                            clr[i] = temp1;
                    }

                    red = clr[0];
                    green = clr[1];
                    blue = clr[2];
                }
            }

            return new ARGB((byte)(255 * red), (byte)(255 * green), (byte)(255 * blue));
        }

        /// <summary>
        /// Converts a color from HSL to RGB
        /// </summary>
        /// <remarks>Adapted from the algorithm in Foley and Van-Dam</remarks>
        /// <param name="hsl">The HSL value</param>
        /// <returns>A Color structure containing the equivalent RGB values</returns>
        public static ARGB ToARGB(this AHSL hsl)
        {
            double red = 0;
            double green = 0;
            double blue = 0;
            double temp1;
            double temp2;

            if (hsl.Luminance == 0)
            {
                red = green = blue = 0;
            }
            else
            {
                if (hsl.Saturation == 0)
                {
                    red = green = blue = hsl.Luminance;
                }
                else
                {
                    temp2 = ((hsl.Luminance <= 0.5) ? hsl.Luminance * (1.0 + hsl.Saturation) : hsl.Luminance + hsl.Saturation - (hsl.Luminance * hsl.Saturation));
                    temp1 = 2.0 * hsl.Luminance - temp2;
                    var t3 = new double[] { hsl.Hue + 1.0 / 3.0, hsl.Hue, hsl.Hue - 1.0 / 3.0 };
                    var clr = new double[] { 0, 0, 0 };
                    for (var i = 0; i < 3; i++)
                    {
                        if (t3[i] < 0)
                            t3[i] += 1.0;
                        if (t3[i] > 1)
                            t3[i] -= 1.0;
                        if (6.0 * t3[i] < 1.0)
                            clr[i] = temp1 + (temp2 - temp1) * t3[i] * 6.0;
                        else if (2.0 * t3[i] < 1.0)
                            clr[i] = temp2;
                        else if (3.0 * t3[i] < 2.0)
                            clr[i] = (temp1 + (temp2 - temp1) * ((2.0 / 3.0) - t3[i]) * 6.0);
                        else
                            clr[i] = temp1;
                    }

                    red = clr[0];
                    green = clr[1];
                    blue = clr[2];
                }
            }

            return new ARGB((byte)hsl.Alpha, (byte)(255 * red), (byte)(255 * green), (byte)(255 * blue));
        }

        /// <summary>
        /// Given H,S,L in range of 0-1
        /// Returns a Color (RGB class) in range of 0-255
        /// </summary>
        /// <param name="alpha">Alpha value.</param>
        /// <param name="hue">Hue value.</param>
        /// <param name="saturation">Saturation value.</param>
        /// <param name="luminance">Luminance value.</param>
        /// <returns>An ARGB color structure.</returns>
        /// <acknowledgment>
        /// http://www.geekymonkey.com/Programming/CSharp/RGB2HSL_HSL2RGB.htm
        /// </acknowledgment>
        public static ARGB AHSLToARGB(byte alpha, double hue, double saturation, double luminance)
        {
            // default to gray
            var red = luminance;
            var green = luminance;
            var blue = luminance;
            var vertex = (luminance <= 0.5) ? (luminance * (1.0 + saturation)) : (luminance + saturation - (luminance * saturation));

            if (vertex > 0)
            {
                var m = luminance + luminance - vertex;
                var sv = (vertex - m) / vertex;
                hue *= 6.0;
                var sextant = (int)hue;
                var fract = hue - sextant;
                var vsf = vertex * sv * fract;
                var mid1 = m + vsf;
                var mid2 = vertex - vsf;
                switch (sextant)
                {
                    case 0:
                        red = vertex;
                        green = mid1;
                        blue = m;
                        break;
                    case 1:
                        red = mid2;
                        green = vertex;
                        blue = m;
                        break;
                    case 2:
                        red = m;
                        green = vertex;
                        blue = mid1;
                        break;
                    case 3:
                        red = m;
                        green = mid2;
                        blue = vertex;
                        break;
                    case 4:
                        red = mid1;
                        green = m;
                        blue = vertex;
                        break;
                    case 5:
                        red = vertex;
                        green = m;
                        blue = mid2;
                        break;
                }
            }

            var rgb = new ARGB(alpha,
                Convert.ToByte(red * 255.0f),
                Convert.ToByte(green * 255.0f),
                Convert.ToByte(blue * 255.0f));
            return rgb;
        }

        /// <summary>
        /// Given a Color (RGB class) in range of 0-255 Return H,S,L in range of 0-1
        /// </summary>
        /// <param name="rgb">ARGB color.</param>
        /// <param name="alpha">Alpha value out.</param>
        /// <param name="hue">Hue value out.</param>
        /// <param name="saturation">Saturation value out.</param>
        /// <param name="luminance">Luminance value out.</param>
        public static void ARGBToAHSL(ARGB rgb, out byte alpha, out double hue, out double saturation, out double luminance)
        {
            alpha = rgb.Alpha;
            var red = rgb.Red / 255.0;
            var green = rgb.Green / 255.0;
            var blue = rgb.Blue / 255.0;
            double vertexMin;

            hue = 0; // default to black
            saturation = 0;
            luminance = 0;
            var vertex = Max(Max(red, green), blue);
            var min = Min(Min(red, green), blue);
            luminance = (min + vertex) / 2.0;
            if (luminance <= 0.0)
                return;

            vertexMin = vertex - min;
            saturation = vertexMin;
            if (saturation > 0.0)
                saturation /= (luminance <= 0.5) ? (vertex + min) : (2.0 - vertex - min);
            else
                return;

            var red2 = (vertex - red) / vertexMin;
            var green2 = (vertex - green) / vertexMin;
            var blue2 = (vertex - blue) / vertexMin;
            if (red == vertex)
                hue = green == min ? 5.0 + blue2 : 1.0 - green2;
            else if (green == vertex)
                hue = blue == min ? 1.0 + red2 : 3.0 - blue2;
            else
                hue = red == min ? 3.0 + green2 : 5.0 - red2;

            hue /= 6.0;
        }

        /// <summary>
        /// RGB --> CMYK
        /// Black   = minimum(1-Red,1-Green,1-Blue)
        /// Cyan    = (1-Red-Black)/(1-Black)
        /// Magenta = (1-Green-Black)/(1-Black)
        /// Yellow  = (1-Blue-Black)/(1-Black)
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/4488/XCmyk-CMYK-to-RGB-Calculator-with-source-code
        /// The algorithms for these routines were taken from: http://web.archive.org/web/20030416004239/http://www.neuro.sfc.keio.ac.jp/~aly/polygon/info/color-space-faq.html
        /// </acknowledgment>
        public static ACYMK RGB2CMYK(this ARGB color)
        {
            double R = color.Red;
            double G = color.Green;
            double B = color.Blue;

            R = 1.0 - (R / 255.0);
            G = 1.0 - (G / 255.0);
            B = 1.0 - (B / 255.0);

            double C, M, Y, K;
            if (R < G)
                K = R;
            else
                K = G;
            if (B < K)
                K = B;

            C = (R - K) / (1.0 - K);
            M = (G - K) / (1.0 - K);
            Y = (B - K) / (1.0 - K);

            C = (C * 100) + 0.5;
            M = (M * 100) + 0.5;
            Y = (Y * 100) + 0.5;
            K = (K * 100) + 0.5;

            return new ACYMK(color.Alpha, (byte)C, (byte)Y, (byte)M, (byte)K);
        }

        /// <summary>
        /// RGB --> CMYK
        /// Black   = minimum(1-Red,1-Green,1-Blue)
        /// Cyan    = (1-Red-Black)/(1-Black)
        /// Magenta = (1-Green-Black)/(1-Black)
        /// Yellow  = (1-Blue-Black)/(1-Black)
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/4488/XCmyk-CMYK-to-RGB-Calculator-with-source-code
        /// The algorithms for these routines were taken from: http://web.archive.org/web/20030416004239/http://www.neuro.sfc.keio.ac.jp/~aly/polygon/info/color-space-faq.html
        /// </acknowledgment>
        public static ACYMK RGB2CMYK(byte alpha, byte red, byte green, byte blue)
        {
            double R = red;
            double G = green;
            double B = blue;

            R = 1.0 - (R / 255.0);
            G = 1.0 - (G / 255.0);
            B = 1.0 - (B / 255.0);

            double C, M, Y, K;
            if (R < G)
                K = R;
            else
                K = G;
            if (B < K)
                K = B;

            C = (R - K) / (1.0 - K);
            M = (G - K) / (1.0 - K);
            Y = (B - K) / (1.0 - K);

            C = (C * 100) + 0.5;
            M = (M * 100) + 0.5;
            Y = (Y * 100) + 0.5;
            K = (K * 100) + 0.5;

            return new ACYMK(alpha, (byte)C, (byte)Y, (byte)M, (byte)K);
        }

        /// <summary>
        /// CMYK --> RGB
        /// Red   = 1-minimum(1,Cyan*(1-Black)+Black)
        /// Green = 1-minimum(1,Magenta*(1-Black)+Black)
        /// Blue  = 1-minimum(1,Yellow*(1-Black)+Black)
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="cyan"></param>
        /// <param name="yellow"></param>
        /// <param name="magenta"></param>
        /// <param name="black"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/4488/XCmyk-CMYK-to-RGB-Calculator-with-source-code
        /// The algorithms for these routines were taken from: http://web.archive.org/web/20030416004239/http://www.neuro.sfc.keio.ac.jp/~aly/polygon/info/color-space-faq.html
        /// </acknowledgment>
        public static ARGB CMYK2RGB(byte alpha, byte cyan, byte yellow, byte magenta, byte black)
        {
            double R, G, B;
            double C, M, Y, K;

            C = cyan;
            M = magenta;
            Y = yellow;
            K = black;

            C /= 255.0;
            M /= 255.0;
            Y /= 255.0;
            K /= 255.0;

            R = C * (1.0 - K) + K;
            G = M * (1.0 - K) + K;
            B = Y * (1.0 - K) + K;

            R = (1.0 - R) * 255.0 + 0.5;
            G = (1.0 - G) * 255.0 + 0.5;
            B = (1.0 - B) * 255.0 + 0.5;

            return new ARGB((byte)R, (byte)G, (byte)B);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="hue"></param>
        /// <param name="saturation"></param>
        /// <param name="value"></param>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/359612/how-to-change-rgb-color-to-hsv
        /// </acknowledgment>
        public static void ColorToHSV(this ARGB color, out double hue, out double saturation, out double value)
        {
            int max = Max(color.Red, Max(color.Green, color.Blue));
            int min = Min(color.Red, Min(color.Green, color.Blue));

            hue = color.GetHue();
            saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            value = max / 255d;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hue"></param>
        /// <param name="saturation"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/359612/how-to-change-rgb-color-to-hsv
        /// </acknowledgment>
        public static ARGB ColorFromHSV(double hue, double saturation, double value)
        {
            var hi = Convert.ToInt32(Floor(hue / 60)) % 6;
            var f = hue / 60 - Floor(hue / 60);

            value *= 255;
            var v = Convert.ToByte(value);
            var p = Convert.ToByte(value * (1 - saturation));
            var q = Convert.ToByte(value * (1 - f * saturation));
            var t = Convert.ToByte(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return new ARGB(255, v, t, p);
            else if (hi == 1)
                return new ARGB(255, q, v, p);
            else if (hi == 2)
                return new ARGB(255, p, v, t);
            else if (hi == 3)
                return new ARGB(255, p, q, v);
            else if (hi == 4)
                return new ARGB(255, t, p, v);
            else
                return new ARGB(255, v, p, q);
        }

        /// <summary>
        /// h = [0,360], s = [0,1], v = [0,1]
        ///		if s == 0, then h = -1 (undefined)
        /// </summary>
        /// <param name="color"></param>
        /// <acknowledgment>
        /// https://www.cs.rit.edu/~ncs/color/t_convert.html
        /// </acknowledgment>
        public static AHSV? ARGBtoAHSV(ARGB color)
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
                return null;
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
            return new AHSV(color.Alpha, h, s, v);
        }

        /// <summary>
        /// h = [0,360], s = [0,1], v = [0,1]
        ///		if s == 0, then h = -1 (undefined)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="h"></param>
        /// <param name="s"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://www.cs.rit.edu/~ncs/color/t_convert.html
        /// </acknowledgment>
        public static ARGB AHSVtoRGB(byte a, double h, double s, double v)
        {
            double r;
            double g;
            double b;
            int i;
            double f, p, q, t;
            if (s == 0)
            {
                // achromatic (gray)
                r = g = b = v;

                r = (1.0 - r) * 255.0 + 0.5;
                g = (1.0 - g) * 255.0 + 0.5;
                b = (1.0 - b) * 255.0 + 0.5;

                return new ARGB(a, (byte)r, (byte)g, (byte)b);
            }

            h /= 60;            // sector 0 to 5
            i = (int)Floor(h);
            f = h - i;          // factorial part of h
            p = v * (1 - s);
            q = v * (1 - s * f);
            t = v * (1 - s * (1 - f));
            switch (i)
            {
                case 0:
                    r = v;
                    g = t;
                    b = p;
                    break;
                case 1:
                    r = q;
                    g = v;
                    b = p;
                    break;
                case 2:
                    r = p;
                    g = v;
                    b = t;
                    break;
                case 3:
                    r = p;
                    g = q;
                    b = v;
                    break;
                case 4:
                    r = t;
                    g = p;
                    b = v;
                    break;
                case 5:
                default:
                    r = v;
                    g = p;
                    b = q;
                    break;
            }

            r = (1.0 - r) * 255.0 + 0.5;
            g = (1.0 - g) * 255.0 + 0.5;
            b = (1.0 - b) * 255.0 + 0.5;

            return new ARGB(a, (byte)r, (byte)g, (byte)b);
        }

        /// <summary>
        /// Function example takes H, S, I, and a pointer to the
        /// in the calling function. After calling hsi2rgb
        /// the vector RGB will contain red, green, and blue
        /// calculated values.
        /// </summary>
        /// <param name="color"></param>
        /// <returns>RGB color-space converted vector.</returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://blog.saikoled.com/post/44677718712/how-to-convert-from-hsi-to-rgb-white
        /// </acknowledgment>
        public static ARGB Hsi2rgb(AHSI color)
        {
            var H = color.Hue;
            var S = color.Saturation;
            var I = color.Intensity;

            byte r, g, b;
            H = IEEERemainder(H, 360); // cycle H around to 0-360 degrees
            H = 3.14159 * H / (float)180; // Convert to radians.
            S = S > 0 ? (S < 1 ? S : 1) : 0; // clamp S and I to interval [0,1]
            I = I > 0 ? (I < 1 ? I : 1) : 0;

            // Math! Thanks in part to Kyle Miller.
            if (H < 2.09439)
            {
                r = (byte)(255 * I / 3 * (1 + S * Cos(H) / Cos(1.047196667 - H)));
                g = (byte)(255 * I / 3 * (1 + S * (1 - Cos(H) / Cos(1.047196667 - H))));
                b = (byte)(255 * I / 3 * (1 - S));
            }
            else if (H < 4.188787)
            {
                H -= 2.09439;
                g = (byte)(255 * I / 3 * (1 + S * Cos(H) / Cos(1.047196667 - H)));
                b = (byte)(255 * I / 3 * (1 + S * (1 - Cos(H) / Cos(1.047196667 - H))));
                r = (byte)(255 * I / 3 * (1 - S));
            }
            else
            {
                H -= 4.188787;
                b = (byte)(255 * I / 3 * (1 + S * Cos(H) / Cos(1.047196667 - H)));
                r = (byte)(255 * I / 3 * (1 + S * (1 - Cos(H) / Cos(1.047196667 - H))));
                g = (byte)(255 * I / 3 * (1 - S));
            }

            return new ARGB(color.Alpha, r, g, b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://dystopiancode.blogspot.com/2012/02/hsi-rgb-conversion-algorithms-in-c.html
        /// https://github.com/dystopiancode/colorspace-conversions
        /// </acknowledgment>
        public static AHSI Hsi_CreateFromRgbF(byte a, double r, double g, double b)
        {
            var m = Min(r, g);
            m = Min(m, b);
            var M = Max(r, g);
            M = Max(m, b);
            var c = M - m;

            var I = (1.0 / 3.0) * (r + g + b);
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
                    H = IEEERemainder(((g - b) / c), 6.0);
                else if (M == g)
                    H = (b - r) / c + 2.0;
                else if (M == b)
                    H = (r - g) / c + 4.0;
                H *= 60.0;
                S = 1.0 - (m / I);
            }

            return new AHSI(a, H, S, I);
        }

        /// <summary>
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://dystopiancode.blogspot.com/2012/02/hsi-rgb-conversion-algorithms-in-c.html
        /// https://github.com/dystopiancode/colorspace-conversions
        /// </acknowledgment>
        public static ARGB RgbF_CreateFromHsi(AHSI color)
        {
            double R = 0;
            double G = 0;
            double B = 0;
            var h = color.Hue;
            var s = color.Saturation;
            var i = color.Intensity;
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

            return new ARGB(color.Alpha, (byte)R, (byte)G, (byte)B);
        }
    }
}