// <copyright file="Colorspaces.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

// <copyright company="dystopiancode" >
//     Some of the color conversion methods were adapted from https://github.com/dystopiancode/colorspace-conversions/.
//     Copyright © 2012 bogdan. All rights reserved.
// </copyright>
// <author id="thelonious">bogdan</author>
// <license>
//     License not listed for project.
// </license>

using System;
using System.Runtime.CompilerServices;
using static Engine.Mathematics;
using static Engine.Operations;
using static System.Math;

namespace Engine.Colorspace
{
    /// <summary>
    /// The color spaces class.
    /// </summary>
    public static class Colorspaces
    {
        #region ARGB Retrievers and Modifiers
        /// <summary>
        /// Get the brightness.
        /// </summary>
        /// <returns>The <see cref="float"/>.</returns>
        /// <remarks>
        /// <para>https://referencesource.microsoft.com/#System.Drawing/commonui/System/Drawing/Color.cs</para>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetLuminance(byte red, byte green, byte blue)
        {
            var r = red / 255d;
            var g = green / 255d;
            var b = blue / 255d;

            var max = r;
            if (g > max)
            {
                max = g;
            }

            if (b > max)
            {
                max = b;
            }

            var min = r;
            if (g < min)
            {
                min = g;
            }

            if (b < min)
            {
                min = b;
            }

            return (max + min) / 2d;
        }

        /// <summary>
        /// Get the brightness.
        /// </summary>
        /// <returns>The <see cref="double"/>.</returns>
        /// <remarks>
        /// <para>https://referencesource.microsoft.com/#System.Drawing/commonui/System/Drawing/Color.cs</para>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetLuminanceFloat(double red, double green, double blue)
        {
            var max = red;
            if (green > max)
            {
                max = green;
            }

            if (blue > max)
            {
                max = blue;
            }

            var min = red;
            if (green < min)
            {
                min = green;
            }

            if (blue < min)
            {
                min = blue;
            }

            return (max + min) / 2d;
        }

        /// <summary>
        /// Get the hue.
        /// </summary>
        /// <returns>The <see cref="float"/>.</returns>
        /// <remarks>
        /// <para>https://referencesource.microsoft.com/#System.Drawing/commonui/System/Drawing/Color.cs</para>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetHue(byte red, byte green, byte blue)
        {
            if (red == green && green == blue)
            {
                return 0; // 0 makes as good an UNDEFINED value as any
            }

            var r = red / 255d;
            var g = green / 255d;
            var b = blue / 255d;

            var hue = 0d;

            var max = r;
            var min = r;

            if (g > max)
            {
                max = g;
            }

            if (b > max)
            {
                max = b;
            }

            if (g < min)
            {
                min = g;
            }

            if (b < min)
            {
                min = b;
            }

            var delta = max - min;

            if (r == max)
            {
                hue = (g - b) / delta;
            }
            else if (g == max)
            {
                hue = 2d + ((b - r) / delta);
            }
            else if (b == max)
            {
                hue = 4d + ((r - g) / delta);
            }
            hue *= 60d;

            if (hue < 0d)
            {
                hue += 360d;
            }
            return hue;
        }

        /// <summary>
        /// Get the hue.
        /// </summary>
        /// <returns>The <see cref="float"/>.</returns>
        /// <remarks>
        /// <para>https://referencesource.microsoft.com/#System.Drawing/commonui/System/Drawing/Color.cs</para>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetHue(double red, double green, double blue)
        {
            if (red == green && green == blue)
            {
                return 0; // 0 makes as good an UNDEFINED value as any
            }

            var hue = 0d;

            var max = red;
            var min = red;

            if (green > max)
            {
                max = green;
            }

            if (blue > max)
            {
                max = blue;
            }

            if (green < min)
            {
                min = green;
            }

            if (blue < min)
            {
                min = blue;
            }

            var delta = max - min;

            if (red == max)
            {
                hue = (green - blue) / delta;
            }
            else if (green == max)
            {
                hue = 2 + ((blue - red) / delta);
            }
            else if (blue == max)
            {
                hue = 4 + ((red - green) / delta);
            }
            hue *= 60d;

            if (hue < 0d)
            {
                hue += 360d;
            }
            return hue;
        }

        /// <summary>
        /// Get the saturation.
        /// </summary>
        /// <returns>The <see cref="double"/>.</returns>
        /// <remarks>
        /// <para>https://referencesource.microsoft.com/#System.Drawing/commonui/System/Drawing/Color.cs</para>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetSaturation(byte red, byte green, byte blue)
        {
            var r = red / 255d;
            var g = green / 255d;
            var b = blue / 255d;
            var max = r;
            var min = r;

            if (g > max)
            {
                max = g;
            }

            if (b > max)
            {
                max = b;
            }

            if (g < min)
            {
                min = g;
            }

            if (b < min)
            {
                min = b;
            }

            var s = 0d;
            // if max == min, then there is no color and
            // the saturation is zero.
            if (max != min)
            {
                var l = (max + min) * 0.5d;

                s = l <= 0.5d ? (max - min) / (max + min) : (max - min) / (2d - max - min);
            }
            return s;
        }

        /// <summary>
        /// Get the saturation.
        /// </summary>
        /// <returns>The <see cref="float"/>.</returns>
        /// <remarks>
        /// <para>https://referencesource.microsoft.com/#System.Drawing/commonui/System/Drawing/Color.cs</para>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetSaturation(double red, double green, double blue)
        {
            var max = red;
            if (green > max)
            {
                max = green;
            }

            if (blue > max)
            {
                max = blue;
            }

            var min = red;
            if (green < min)
            {
                min = green;
            }

            if (blue < min)
            {
                min = blue;
            }

            var s = 0d;
            // if max == min, then there is no color and
            // the saturation is zero.
            if (max != min)
            {
                var l = (max + min) / 2d;
                s = l <= 0.5d ? (max - min) / (max + min) : (max - min) / (2d - max - min);
            }
            return s;
        }

        /// <summary>
        /// Sets the absolute brightness of a color
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <param name="alpha"></param>
        /// <param name="luminance">The luminance level to impose</param>
        /// <returns>an adjusted color</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (byte red, byte green, byte blue, byte alpha) SetLuminance(byte red, byte green, byte blue, byte alpha, double luminance)
        {
            var hsl = RGBAColorToHSLAColor(red, green, blue, alpha);
            return HSLAColorToRGBAColor(hsl.hue, hsl.saturation, luminance, hsl.alpha);
        }

        /// <summary>
        /// Modifies an existing brightness level
        /// </summary>
        /// <remarks>
        /// <para>To reduce brightness use a number smaller than 1. To increase brightness use a number larger tan 1.</para>
        /// </remarks>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <param name="alpha"></param>
        /// <param name="luminance">The luminance delta</param>
        /// <returns>An adjusted color</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (byte red, byte green, byte blue, byte alpha) ModifyLuminance(byte red, byte green, byte blue, byte alpha, double luminance)
        {
            var hsl = RGBAColorToHSLAColor(red, green, blue, alpha);
            hsl.luminance *= luminance;
            return HSLAColorToRGBAColor(hsl.hue, hsl.saturation, hsl.luminance, hsl.alpha);
        }

        /// <summary>
        /// Sets the absolute saturation level
        /// </summary>
        /// <remarks><para>Accepted values 0-1</para></remarks>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <param name="alpha"></param>
        /// <param name="Saturation">The saturation value to impose</param>
        /// <returns>An adjusted color</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (byte red, byte green, byte blue, byte alpha) SetSaturation(byte red, byte green, byte blue, byte alpha, double Saturation)
        {
            var hsl = RGBAColorToHSLAColor(red, green, blue, alpha);
            return HSLAColorToRGBAColor(hsl.hue, Saturation, hsl.luminance, hsl.alpha);
        }

        /// <summary>
        /// Modifies an existing Saturation level.
        /// </summary>
        /// <remarks>
        /// <para>To reduce Saturation use a number smaller than 1. To increase Saturation use a number larger tan 1.</para>
        /// </remarks>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <param name="alpha"></param>
        /// <param name="Saturation">The saturation delta</param>
        /// <returns>An adjusted color</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (byte red, byte green, byte blue, byte alpha) ModifySaturation(byte red, byte green, byte blue, byte alpha, double Saturation)
        {
            var hsl = RGBAColorToHSLAColor(red, green, blue, alpha);
            hsl.saturation *= Saturation;
            return HSLAColorToRGBAColor(hsl.hue, hsl.saturation, hsl.luminance, hsl.alpha);
        }

        /// <summary>
        /// Sets the absolute Hue level.
        /// </summary>
        /// <remarks><para>Accepted values 0-1</para></remarks>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <param name="alpha"></param>
        /// <param name="Hue">The Hue value to impose</param>
        /// <returns>An adjusted color</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (byte red, byte green, byte blue, byte alpha) SetHue(byte red, byte green, byte blue, byte alpha, double Hue)
        {
            var hsl = RGBAColorToHSLAColor(red, green, blue, alpha);
            return HSLAColorToRGBAColor(Hue, hsl.saturation, hsl.luminance, hsl.alpha);
        }

        /// <summary>
        /// Modifies an existing Hue level
        /// </summary>
        /// <remarks>
        /// <para>To reduce Hue use a number smaller than 1. To increase Hue use a number larger tan 1</para>
        /// </remarks>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <param name="alpha"></param>
        /// <param name="Hue">The Hue delta</param>
        /// <returns>An adjusted color</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (byte red, byte green, byte blue, byte alpha) ModifyHue(byte red, byte green, byte blue, byte alpha, double Hue)
        {
            var hsl = RGBAColorToHSLAColor(red, green, blue, alpha);
            hsl.hue *= Hue;
            return HSLAColorToRGBAColor(hsl.hue, hsl.saturation, hsl.luminance, hsl.alpha);
        }
        #endregion ARGB Retrievers and Modifiers

        #region Validation
        /// <summary>
        /// Check whether a red green blue color is valid.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="g">The g.</param>
        /// <param name="b">The b.</param>
        /// <param name="a"></param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ValidateRGBA(byte r, byte g, byte b, byte a)
            => Between(a, RGBMin, RGBMax)
            && Between(r, RGBMin, RGBMax)
            && Between(g, RGBMin, RGBMax)
            && Between(b, RGBMin, RGBMax);

        /// <summary>
        /// Check whether a red green blue double floating point color is valid.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="g">The g.</param>
        /// <param name="b">The b.</param>
        /// <param name="a"></param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ValidateRGBAF(double r, double g, double b, double a)
            => Between(a, PercentMin, PercentMax)
            && Between(r, PercentMin, PercentMax)
            && Between(g, PercentMin, PercentMax)
            && Between(b, PercentMin, PercentMax);

        /// <summary>
        /// Check whether a cyan yellow magenta black color is valid.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="y">The y.</param>
        /// <param name="m">The m.</param>
        /// <param name="k">The k.</param>
        /// <param name="a"></param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <remarks><para>https://www.codeproject.com/articles/4488/xcmyk-cmyk-to-rgb-calculator-with-source-code</para></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ValidateCMYKA(byte c, byte y, byte m, byte k, byte a)
            => Between(a, CMYKMin, CMYKMax)
            && Between(c, CMYKMin, CMYKMax)
            && Between(y, CMYKMin, CMYKMax)
            && Between(m, CMYKMin, CMYKMax)
            && Between(k, CMYKMin, CMYKMax);

        /// <summary>
        /// Check whether a hue saturation intensity color is valid.
        /// </summary>
        /// <param name="h">The h.</param>
        /// <param name="s">The s.</param>
        /// <param name="i">The i.</param>
        /// <param name="a"></param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ValidateHSIA(double h, double s, double i, double a)
            => Between(a, PercentMin, PercentMax)
            && Between(h, HueMin, HueMax)
            && Between(s, PercentMin, PercentMax)
            && Between(i, PercentMin, PercentMax);

        /// <summary>
        /// Check whether a hue saturation luminance color is valid.
        /// </summary>
        /// <param name="h">The h.</param>
        /// <param name="s">The s.</param>
        /// <param name="l">The l.</param>
        /// <param name="a"></param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ValidateHSLA(double h, double s, double l, double a)
            => Between(a, PercentMin, PercentMax)
            && Between(h, HueMin, HueMax)
            && Between(s, PercentMin, PercentMax)
            && Between(l, PercentMin, PercentMax);

        /// <summary>
        /// Check whether a hue saturation value color is valid.
        /// </summary>
        /// <param name="h">The h.</param>
        /// <param name="s">The s.</param>
        /// <param name="v">The v.</param>
        /// <param name="a"></param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ValidateHSVA(double h, double s, double v, double a)
            => Between(a, PercentMin, PercentMax)
            && Between(h, HueMin, HueMax)
            && Between(s, PercentMin, PercentMax)
            && Between(v, PercentMin, PercentMax);

        /// <summary>
        /// Check whether a yiq color is valid.
        /// </summary>
        /// <param name="y">The y.</param>
        /// <param name="i">The i.</param>
        /// <param name="q">The q.</param>
        /// <param name="a"></param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ValidateYIQA(double y, double i, double q, double a)
            => Between(a, PercentMin, PercentMax)
            && Between(y, PercentMin, PercentMax)
            && Between(i, YIQMinI, YIQMaxI)
            && Between(q, YIQMinQ, YIQMaxQ);

        /// <summary>
        /// Check whether a yuv color is valid.
        /// </summary>
        /// <param name="y">The y.</param>
        /// <param name="u">The u.</param>
        /// <param name="v">The v.</param>
        /// <param name="a"></param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ValidateYUVA(double y, double u, double v, double a)
            => Between(a, PercentMin, PercentMax)
            && Between(y, PercentMin, PercentMax)
            && Between(u, YUVMinU, YUVMaxU)
            && Between(v, YUVMinV, YUVMaxV);
        #endregion Validation

        #region Clamp Methods
        /// <summary>
        /// Clamp a red green blue color to max and min.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="g">The g.</param>
        /// <param name="b">The b.</param>
        /// <param name="a"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (byte r, byte g, byte b, byte a) ClampRGBA(byte r, byte g, byte b, byte a)
            => (
            Operations.Clamp(a, RGBMin, RGBMax),
            Operations.Clamp(r, RGBMin, RGBMax),
            Operations.Clamp(g, RGBMin, RGBMax),
            Operations.Clamp(b, RGBMin, RGBMax)
            );

        /// <summary>
        /// Clamps an RGBA Float to max and min.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="g">The g.</param>
        /// <param name="b">The b.</param>
        /// <param name="a"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double r, double g, double b, double a) ClampRGBAF(double r, double g, double b, double a)
            => (
            Operations.Clamp(a, PercentMin, PercentMax),
            Operations.Clamp(r, PercentMin, PercentMax),
            Operations.Clamp(g, PercentMin, PercentMax),
            Operations.Clamp(b, PercentMin, PercentMax)
            );

        /// <summary>
        /// Clamps a cyan yellow magenta black color to max and min.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="y">The y.</param>
        /// <param name="m">The m.</param>
        /// <param name="k">The k.</param>
        /// <param name="a"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (byte c, byte y, byte m, byte k, byte a) ClampCMYKA(byte c, byte y, byte m, byte k, byte a)
            => (
            Operations.Clamp(a, CMYKMin, CMYKMax),
            Operations.Clamp(c, CMYKMin, CMYKMax),
            Operations.Clamp(y, CMYKMin, CMYKMax),
            Operations.Clamp(m, CMYKMin, CMYKMax),
            Operations.Clamp(k, CMYKMin, CMYKMax)
            );

        /// <summary>
        /// Clamp a hue saturation intensity color to max and min.
        /// </summary>
        /// <param name="h">The h.</param>
        /// <param name="s">The s.</param>
        /// <param name="i">The i.</param>
        /// <param name="a"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double h, double s, double i, double a) ClampHSIA(double h, double s, double i, double a)
            => (
            Operations.Clamp(a, PercentMin, PercentMax),
            Operations.Clamp(h, HueMin, HueMax),
            Operations.Clamp(s, PercentMin, PercentMax),
            Operations.Clamp(i, PercentMin, PercentMax)
            );

        /// <summary>
        /// Clamp a hue saturation luminance color to max and min.
        /// </summary>
        /// <param name="h">The h.</param>
        /// <param name="s">The s.</param>
        /// <param name="l">The l.</param>
        /// <param name="a"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double h, double s, double l, double a) ClampHSLA(double h, double s, double l, double a)
            => (
            Operations.Clamp(a, PercentMin, PercentMax),
            Operations.Clamp(h, HueMin, HueMax),
            Operations.Clamp(s, PercentMin, PercentMax),
            Operations.Clamp(l, PercentMin, PercentMax)
            );

        /// <summary>
        /// Clamp a hue saturation value color to min and max.
        /// </summary>
        /// <param name="h">The h.</param>
        /// <param name="s">The s.</param>
        /// <param name="v">The v.</param>
        /// <param name="a"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double h, double s, double v, double a) ClampHSVA(double h, double s, double v, double a)
            => (
            Operations.Clamp(a, PercentMin, PercentMax),
            Operations.Clamp(h, HueMin, HueMax),
            Operations.Clamp(s, PercentMin, PercentMax),
            Operations.Clamp(v, PercentMin, PercentMax)
            );

        /// <summary>
        /// Clamp a yiq color to max and min.
        /// </summary>
        /// <param name="y">The y.</param>
        /// <param name="i">The i.</param>
        /// <param name="q">The q.</param>
        /// <param name="a"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double y, double i, double q, double a) ClampYIQA(double y, double i, double q, double a)
            => (
            Operations.Clamp(a, PercentMin, PercentMax),
            Operations.Clamp(y, PercentMin, PercentMax),
            Operations.Clamp(i, YIQMinI, YIQMaxI),
            Operations.Clamp(q, YIQMinQ, YIQMaxQ)
            );

        /// <summary>
        /// Clamp a yuv color is valid.
        /// </summary>
        /// <param name="y">The y.</param>
        /// <param name="u">The u.</param>
        /// <param name="v">The v.</param>
        /// <param name="a"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double y, double u, double v, double a) ClampYUVA(double y, double u, double v, double a)
            => (
            Operations.Clamp(a, PercentMin, PercentMax),
            Operations.Clamp(y, PercentMin, PercentMax),
            Operations.Clamp(u, YUVMinU, YUVMaxU),
            Operations.Clamp(v, YUVMinV, YUVMaxV)
            );
        #endregion

        #region Conversion Methods
        /// <summary>
        /// Converts a byte red green blue alpha color to the double floating point form.
        /// </summary>
        /// <param name="red">The red channel.</param>
        /// <param name="green">The green channel.</param>
        /// <param name="blue">The blue channel.</param>
        /// <param name="alpha">The alpha channel.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4}"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double red, double green, double blue, double alpha) RGBAColorToRGBAFColor(byte red, byte green, byte blue, byte alpha)
        {
            (red, green, blue, alpha) = ClampRGBA(red, green, blue, alpha);
            var d = 1d / RGBMax;
            return (
                red: red * d,
                green: green * d,
                blue: blue * d,
                alpha: alpha * d
                );
        }

        /// <summary>
        /// Convert an red green blue alpha byte color format to red green blue alpha color in double precision float format.
        /// </summary>
        /// <param name="red">The red component.</param>
        /// <param name="green">The green component.</param>
        /// <param name="blue">The blue component.</param>
        /// <param name="alpha">The alpha component.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4}"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double red, double green, double blue, double alpha) RGBAColorToRGBAFColor2(byte red, byte green, byte blue, byte alpha) => (red: red / 255d, green: green / 255d, blue: blue / 255d, alpha: alpha / 255d);

        /// <summary>
        /// Convert an red green blue alpha color from double floating point format to byte.
        /// </summary>
        /// <param name="red">The r.</param>
        /// <param name="green">The g.</param>
        /// <param name="blue">The b.</param>
        /// <param name="alpha"></param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (byte red, byte green, byte blue, byte alpha) RGBAFColorToRGBAColor(double red, double green, double blue, double alpha)
        {
            (red, green, blue, alpha) = ClampRGBAF(red, green, blue, alpha);
            var d = RGBMax + 0.5d;
            return (
                red: (byte)(red * d),
                green: (byte)(green * d),
                blue: (byte)(blue * d),
                alpha: (byte)(alpha * d)
                );
        }

        /// <summary>
        /// Convert an red green blue alpha color from double floating point format to byte.
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (byte red, byte green, byte blue, byte alpha) RGBAFColorToRGBAColor((double red, double green, double blue, double alpha) tuple)
        {
            tuple = ClampRGBAF(tuple.red, tuple.green, tuple.blue, tuple.alpha);
            var d = RGBMax + 0.5d;
            return (
                red: (byte)(tuple.red * d),
                green: (byte)(tuple.green * d),
                blue: (byte)(tuple.blue * d),
                alpha: (byte)(tuple.alpha * d)
                );
        }

        /// <summary>
        /// Convert an alpha cyan yellow magenta black color format to alpha red green blue byte color format.
        /// </summary>
        /// <param name="cyan">The cyan.</param>
        /// <param name="yellow">The yellow.</param>
        /// <param name="magenta">The magenta.</param>
        /// <param name="black">The black.</param>
        /// <param name="alpha">The alpha.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <remarks>
        /// <para>Red   = 1-minimum(1,Cyan*(1-Black)+Black)
        /// Green = 1-minimum(1,Magenta*(1-Black)+Black)
        /// Blue  = 1-minimum(1,Yellow*(1-Black)+Black)</para>
        /// </remarks>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/4488/XCmyk-CMYK-to-RGB-Calculator-with-source-code
        /// The algorithms for these routines were taken from: http://web.archive.org/web/20030416004239/http://www.neuro.sfc.keio.ac.jp/~aly/polygon/info/color-space-faq.html
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (byte red, byte green, byte blue, byte alpha) CMYKAColorToRGBAColor(byte cyan, byte yellow, byte magenta, byte black, byte alpha)
        {
            var d = 1d / 100d;//255d;

            // ToDo: Figure out if messing with alpha like this is worth while.
            var a = alpha * d;
            var c = cyan * d;
            var m = magenta * d;
            var y = yellow * d;
            var k = black * d;

            a = (a * (1d - k)) + k;
            var r = (c * (1d - k)) + k;
            var g = (m * (1d - k)) + k;
            var b = (y * (1d - k)) + k;

            return (
                red: (byte)(((1d - r) * 255d) + 0.5d),
                green: (byte)(((1d - g) * 255d) + 0.5d),
                blue: (byte)(((1d - b) * 255d) + 0.5d),
                alpha: (byte)(((1d - a) * 255d) + 0.5d)
                );
        }

        /// <summary>
        /// CMYK --> RGB
        /// Red   = 1-minimum(1,Cyan*(1-Black)+Black)
        /// Green = 1-minimum(1,Magenta*(1-Black)+Black)
        /// Blue  = 1-minimum(1,Yellow*(1-Black)+Black)
        /// </summary>
        /// <param name="cyan"></param>
        /// <param name="yellow"></param>
        /// <param name="magenta"></param>
        /// <param name="black"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/4488/XCmyk-CMYK-to-RGB-Calculator-with-source-code
        /// The algorithms for these routines were taken from: http://web.archive.org/web/20030416004239/http://www.neuro.sfc.keio.ac.jp/~aly/polygon/info/color-space-faq.html
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double red, double green, double blue, double alpha) CMYKAColorToRGBAFColor(byte cyan, byte yellow, byte magenta, byte black, byte alpha)
        {
            var c = cyan / 100d; //255d;
            var m = magenta / 100d; //255d;
            var y = yellow / 100d; //255d;
            var k = black / 100d; //255d;

            var r = (c * (1d - k)) + k;
            var g = (m * (1d - k)) + k;
            var b = (y * (1d - k)) + k;
            var a = (alpha / 100d * 255d) + 0.5d;

            r = ((1d - r) * 255d) + 0.5d;
            g = ((1d - g) * 255d) + 0.5d;
            b = ((1d - b) * 255d) + 0.5d;

            return ((byte)r, (byte)g, (byte)b, a);
        }

        /// <summary>
        /// Convert an alpha red green blue byte color format to alpha cyan yellow magenta black format.
        /// </summary>
        /// <param name="red">The red component.</param>
        /// <param name="green">The green component.</param>
        /// <param name="blue">The blue component.</param>
        /// <param name="alpha">The alpha component.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4, T5}"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (byte cyan, byte yellow, byte magenta, byte black, byte alpha) RGBAColorToCMYKAColor(byte red, byte green, byte blue, byte alpha)
            => RGBAFColorToCMYKAColor(red / 255d, green / 255d, blue / 255d, alpha / 255d);

        /// <summary>
        /// RGB --> CMYK
        /// Black   = minimum(1-Red,1-Green,1-Blue)
        /// Cyan    = (1-Red-Black)/(1-Black)
        /// Magenta = (1-Green-Black)/(1-Black)
        /// Yellow  = (1-Blue-Black)/(1-Black)
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/4488/XCmyk-CMYK-to-RGB-Calculator-with-source-code
        /// The algorithms for these routines were taken from: http://web.archive.org/web/20030416004239/http://www.neuro.sfc.keio.ac.jp/~aly/polygon/info/color-space-faq.html
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (byte cyan, byte yellow, byte magenta, byte black, byte alpha) RGBAColorToCMYKAColor2(byte red, byte green, byte blue, byte alpha)
        {
            var r = 1d - (red / 255d);
            var g = 1d - (green / 255d);
            var b = 1d - (blue / 255d);

            var K = r < g ? r : g;
            if (b < K)
            {
                K = b;
            }

            var c = (r - K) / (1d - K);
            var m = (g - K) / (1d - K);
            var y = (b - K) / (1d - K);

            c = (c * 100d) + 0.5d;
            m = (m * 100d) + 0.5d;
            y = (y * 100d) + 0.5d;
            K = (K * 100d) + 0.5d;

            return ((byte)c, (byte)y, (byte)m, (byte)K, alpha);
        }

        /// <summary>
        /// Convert an alpha red green blue color in double precision float format to alpha cyan yellow magenta black color format.
        /// </summary>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="alpha">The alpha.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4, T5}"/>.</returns>
        /// <remarks>
        /// <para>Black   = minimum(1-Red, 1-Green, 1-Blue)
        /// Cyan    = (1-Red-Black)/(1-Black)
        /// Magenta = (1-Green-Black)/(1-Black)
        /// Yellow  = (1-Blue-Black)/(1-Black)</para>
        /// </remarks>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/4488/XCmyk-CMYK-to-RGB-Calculator-with-source-code
        /// The algorithms for these routines were taken from: http://web.archive.org/web/20030416004239/http://www.neuro.sfc.keio.ac.jp/~aly/polygon/info/color-space-faq.html
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (byte cyan, byte yellow, byte magenta, byte black, byte alpha) RGBAFColorToCMYKAColor(double red, double green, double blue, double alpha)
        {
            var k = red < green ? red : green;
            k = blue < k ? blue : k;
            var d = 1d / (1d - k);

            // ToDo: Figure out if messing with alpha like this is worth while.
            var a = (1d - alpha - k) * d;
            var c = (1d - red - k) * d;
            var m = (1d - green - k) * d;
            var y = (1d - blue - k) * d;

            return (
                cyan: (byte)((c * 100) + 0.5),
                yellow: (byte)((y * 100) + 0.5),
                magenta: (byte)((m * 100) + 0.5),
                black: (byte)((k * 100) + 0.5),
                alpha: (byte)((a * 100) + 0.5)
                );
        }

        /// <summary>
        /// The rgbaf create from hsi.
        /// </summary>
        /// <param name="hue">The h.</param>
        /// <param name="saturation">The s.</param>
        /// <param name="intensity">The i.</param>
        /// <param name="alpha"></param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <remarks>
        /// <para>https://github.com/dystopiancode/colorspace-conversions/
        /// Correction from: https://gist.github.com/rzhukov/9129585</para>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double red, double green, double blue, double alpha) HSIAColorToRGBAFColor(double hue, double saturation, double intensity, double alpha)
        {
            (hue, saturation, intensity, alpha) = ClampHSIA(hue, saturation, intensity, alpha);
            var x = intensity * (1d - saturation);
            if (hue < 2d * PI / 3d)
            {
                var y = intensity * (1d + (saturation * Cos(hue) / Cos((PI / 3d) - hue)));
                var z = (3d * intensity) - (x + y);
                return (red: y, green: z, blue: x, alpha);
            }
            else if (hue < 4d * PI / 3d)
            {
                var y = intensity * (1d + (saturation * Cos(hue - (2d * PI / 3d)) / Cos((PI / 3d) - (hue - (2d * PI / 3d)))));
                var z = (3d * intensity) - (x + y);
                return (red: x, green: y, blue: z, alpha);
            }
            else
            {
                var y = intensity * (1d + (saturation * Cos(hue - (4d * PI / 3d)) / Cos((PI / 3d) - (hue - (4d * PI / 3d)))));
                var z = (3 * intensity) - (x + y);
                return (red: z, green: x, blue: y, alpha);
            }
        }

        /// <summary>
        /// The rgb f create from hsi.
        /// </summary>
        /// <param name="hue"></param>
        /// <param name="saturation"></param>
        /// <param name="intensity"></param>
        /// <param name="alpha"></param>
        /// <returns>The <see cref="RGBA"/>.</returns>
        /// <acknowledgment>
        /// http://dystopiancode.blogspot.com/2012/02/hsi-rgb-conversion-algorithms-in-c.html
        /// https://github.com/dystopiancode/colorspace-conversions
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double red, double green, double blue, double alpha) HSIAColorToRGBAFColor2(double hue, double saturation, double intensity, double alpha)
        {
            const double HueUpperLimit = 360d;
            var h = hue;
            var s = saturation;
            var i = intensity;
            double r;
            double g;
            double b;
            if (h >= 0d && h <= (HueUpperLimit / 3d))
            {
                b = i * (1d - s) / 3d;
                r = i * (s * Cos(h) / Cos(60d - h)) / 3d;
                g = i - (b + r);
            }
            else if (h > (HueUpperLimit / 3d) && h <= (2d * HueUpperLimit / 3d))
            {
                h -= HueUpperLimit / 3d;
                r = i * (1d - s) / 3d;
                g = i * (s * Cos(h) / Cos(60d - h)) / 3d;
                b = i - (g + r);
            }
            else /* h>240 h<360 */
            {
                h -= 2d * HueUpperLimit / 3d;
                g = i * (1d - s) / 3d;
                b = i * (s * Cos(h) / Cos(60d - h)) / 3d;
                r = i - (g + b);
            }

            return (r, g, b, alpha);
        }

        /// <summary>
        /// Function example takes H, S, I, and a pointer to the
        /// in the calling function. After calling hsi2rgb
        /// the vector RGB will contain red, green, and blue
        /// calculated values.
        /// </summary>
        /// <param name="hue"></param>
        /// <param name="saturation"></param>
        /// <param name="intensity"></param>
        /// <param name="alpha"></param>
        /// <returns>RGB color-space converted vector.</returns>
        /// <acknowledgment>
        /// http://blog.saikoled.com/post/44677718712/how-to-convert-from-hsi-to-rgb-white
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (byte red, byte green, byte blue, byte alpha) HSIAColorToRGBAColor(double hue, double saturation, double intensity, double alpha)
        {
            var h = hue;
            var s = saturation;
            var i = intensity;

            h = IEEERemainder(h, 360d); // cycle H around to 0-360 degrees
            h = PI * h / 180d; // Convert to radians.
            s = s > 0 ? (s < 1d ? s : 1d) : 0d; // clamp S and I to interval [0,1]
            i = i > 0 ? (i < 1d ? i : 1d) : 0d;

            byte r, g, b;

            // Math! Thanks in part to Kyle Miller.
            if (h < 2.09439d)
            {
                r = (byte)(255d * i / 3d * (1d + (s * Cos(h) / Cos(1.047196667d - h))));
                g = (byte)(255d * i / 3d * (1d + (s * (1d - (Cos(h) / Cos(1.047196667d - h))))));
                b = (byte)(255d * i / 3d * (1d - s));
            }
            else if (h < 4.188787d)
            {
                h -= 2.09439d;
                g = (byte)(255d * i / 3d * (1 + (s * Cos(h) / Cos(1.047196667d - h))));
                b = (byte)(255d * i / 3d * (1 + (s * (1 - (Cos(h) / Cos(1.047196667d - h))))));
                r = (byte)(255d * i / 3d * (1 - s));
            }
            else
            {
                h -= 4.188787d;
                b = (byte)(255d * i / 3d * (1d + (s * Cos(h) / Cos(1.047196667d - h))));
                r = (byte)(255d * i / 3d * (1d + (s * (1d - (Cos(h) / Cos(1.047196667d - h))))));
                g = (byte)(255d * i / 3d * (1d - s));
            }

            return (r, g, b, (byte)(255 * alpha));
        }

        /// <summary>
        /// The rgb f create from hsl.
        /// </summary>
        /// <param name="hue">The h.</param>
        /// <param name="saturation">The s.</param>
        /// <param name="luminance">The l.</param>
        /// <param name="alpha"></param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double red, double green, double blue, double alpha) HSLAColorToRGBAFColor(double hue, double saturation, double luminance, double alpha)
        {
            (double red, double green, double blue, double alpha) color = (0d, 0d, 0d, alpha);
            if (ValidateHSLA(hue, saturation, luminance, alpha) == true)
            {
                var c = (1d - Abs((2d * luminance) - 1d)) * saturation;
                var m = 1d * (luminance - (0.5d * c));
                var x = c * (1d - Abs(IEEERemainder(hue / 60d, 2) - 1d));
                if (hue >= 0d && hue < (HueMax / 6d))
                {
                    color = (c + m, x + m, m, alpha);
                }
                else if (hue >= (HueMax / 6d) && hue < (HueMax / 3d))
                {
                    color = (x + m, c + m, m, alpha);
                }
                else if (hue < (HueMax / 3d) && hue < (HueMax / 2d))
                {
                    color = (m, c + m, x + m, alpha);
                }
                else if (hue >= (HueMax / 2d) && hue < (2d * HueMax / 3d))
                {
                    color = (m, x + m, c + m, alpha);
                }
                else if (hue >= (2d * HueMax / 3d) && hue < (5d * HueMax / 6d))
                {
                    color = (x + m, m, c + m, alpha);
                }
                else if (hue >= (5d * HueMax / 6d) && hue < HueMax)
                {
                    color = (c + m, m, x + m, alpha);
                }
                else
                {
                    color = (m, m, m, alpha);
                }
            }
            return color;
        }

        /// <summary>
        /// Converts a color from HSL to RGB.
        /// </summary>
        /// <remarks><para>Adapted from the algorithm in Foley and Van-Dam</para></remarks>
        /// <param name="hue"></param>
        /// <param name="saturation"></param>
        /// <param name="luminance"></param>
        /// <param name="alpha"></param>
        /// <returns>A Color structure containing the equivalent RGB values</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (byte red, byte green, byte blue, byte alpha) HSLAColorToRGBAColor3(double hue, double saturation, double luminance, double alpha)
        {
            double red;
            double green;
            double blue;
            if (luminance == 0d)
            {
                red = green = blue = 0d;
            }
            else
            {
                if (saturation == 0d)
                {
                    red = green = blue = luminance;
                }
                else
                {
                    var temp2 = (luminance <= 0.5d) ? luminance * (1d + saturation) : luminance + saturation - (luminance * saturation);
                    var temp1 = (2d * luminance) - temp2;
                    var t3 = new double[] { hue + (1d / 3d), hue, hue - (1d / 3d) };
                    var clr = new double[] { 0, 0, 0 };
                    for (var i = 0; i < 3; i++)
                    {
                        if (t3[i] < 0)
                        {
                            t3[i] += 1d;
                        }

                        if (t3[i] > 1)
                        {
                            t3[i] -= 1d;
                        }

                        if (6d * t3[i] < 1d)
                        {
                            clr[i] = temp1 + ((temp2 - temp1) * t3[i] * 6d);
                        }
                        else if (2d * t3[i] < 1d)
                        {
                            clr[i] = temp2;
                        }
                        else if (3d * t3[i] < 2d)
                        {
                            clr[i] = temp1 + ((temp2 - temp1) * ((2d / 3d) - t3[i]) * 6d);
                        }
                        else
                        {
                            clr[i] = temp1;
                        }
                    }

                    red = clr[0];
                    green = clr[1];
                    blue = clr[2];
                }
            }

            return ((byte)(255 * red), (byte)(255 * green), (byte)(255 * blue), (byte)(255 * alpha));
        }

        /// <summary>
        /// Converts a color from HSL to RGB
        /// </summary>
        /// <remarks><para>Adapted from the algorithm in Foley and Van-Dam</para></remarks>
        /// <param name="hue"></param>
        /// <param name="saturation"></param>
        /// <param name="luminance"></param>
        /// <param name="alpha"></param>
        /// <returns>A Color structure containing the equivalent RGB values</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (byte red, byte green, byte blue, byte alpha) HSLAColorToRGBAColor2(double hue, double saturation, double luminance, double alpha)
        {
            double red;
            double green;
            double blue;
            if (luminance == 0d)
            {
                red = green = blue = 0d;
            }
            else
            {
                if (saturation == 0d)
                {
                    red = green = blue = luminance;
                }
                else
                {
                    var temp2 = (luminance <= 0.5d) ? luminance * (1d + saturation) : luminance + saturation - (luminance * saturation);
                    var temp1 = 2d * luminance - temp2;
                    var t3 = new double[] { hue + 1d / 3d, hue, hue - 1d / 3d };
                    var clr = new double[] { 0, 0, 0 };
                    for (var i = 0; i < 3; i++)
                    {
                        if (t3[i] < 0d)
                        {
                            t3[i] += 1d;
                        }

                        if (t3[i] > 1d)
                        {
                            t3[i] -= 1d;
                        }

                        if (6d * t3[i] < 1d)
                        {
                            clr[i] = temp1 + (temp2 - temp1) * t3[i] * 6d;
                        }
                        else if (2d * t3[i] < 1d)
                        {
                            clr[i] = temp2;
                        }
                        else if (3d * t3[i] < 2d)
                        {
                            clr[i] = temp1 + (temp2 - temp1) * ((2d / 3d) - t3[i]) * 6d;
                        }
                        else
                        {
                            clr[i] = temp1;
                        }
                    }

                    red = clr[0];
                    green = clr[1];
                    blue = clr[2];
                }
            }

            return ((byte)(255 * red), (byte)(255 * green), (byte)(255 * blue), (byte)(255 * alpha));
        }

        /// <summary>
        /// Given H,S,L in range of 0-1
        /// Returns a Color (RGB class) in range of 0-255
        /// </summary>
        /// <param name="hue">Hue value.</param>
        /// <param name="saturation">Saturation value.</param>
        /// <param name="luminance">Luminance value.</param>
        /// <param name="alpha">Alpha value.</param>
        /// <returns>An ARGB color structure.</returns>
        /// <acknowledgment>
        /// http://www.geekymonkey.com/Programming/CSharp/RGB2HSL_HSL2RGB.htm
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (byte red, byte green, byte blue, byte alpha) HSLAColorToRGBAColor(double hue, double saturation, double luminance, double alpha)
        {
            // default to gray
            var red = luminance;
            var green = luminance;
            var blue = luminance;
            var vertex = (luminance <= 0.5d) ? (luminance * (1d + saturation)) : (luminance + saturation - (luminance * saturation));

            if (vertex > 0)
            {
                var m = luminance + luminance - vertex;
                var sv = (vertex - m) / vertex;
                hue *= 6d;
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

            return (
                Convert.ToByte(red * 255d),
                Convert.ToByte(green * 255d),
                Convert.ToByte(blue * 255d),
                Convert.ToByte(alpha * 255d)
                );
        }

        /// <summary>
        /// The rgb f create from hsv.
        /// </summary>
        /// <param name="hue">The h.</param>
        /// <param name="saturaion">The s.</param>
        /// <param name="value">The v.</param>
        /// <param name="alpha"></param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double red, double green, double blue, double alpha) HSVAColorToRGBAFColor(double hue, double saturaion, double value, double alpha)
        {
            (double red, double green, double blue, double alpha) color = (0d, 0d, 0d, alpha);
            if (ValidateHSVA(hue, saturaion, value, alpha) == true)
            {
                var c = value * saturaion;
                var x = c * (1d - Abs(IEEERemainder(hue / 60d, 2) - 1d));
                var m = value - c;
                if (hue >= 0d && hue < 60d)
                {
                    color = (c + m, x + m, m, alpha);
                }
                else if (hue >= 60d && hue < 120d)
                {
                    color = (x + m, c + m, m, alpha);
                }
                else if (hue >= 120d && hue < 180d)
                {
                    color = (m, c + m, x + m, alpha);
                }
                else if (hue >= 180d && hue < 240d)
                {
                    color = (m, x + m, c + m, alpha);
                }
                else if (hue >= 240d && hue < 300d)
                {
                    color = (x + m, m, c + m, alpha);
                }
                else if (hue >= 300d && hue < 360d)
                {
                    color = (c + m, m, x + m, alpha);
                }
                else
                {
                    color = (m, m, m, alpha);
                }
            }
            return color;
        }

        /// <summary>
        /// The to RGBA tuple.
        /// </summary>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4}"/>.</returns>
        /// <remarks>
        /// <para>h = [0,360], s = [0,1], v = [0,1]
        ///		if s == 0, then h = -1 (undefined)</para>
        /// </remarks>
        /// <acknowledgment>
        /// https://www.cs.rit.edu/~ncs/color/t_convert.html
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (byte red, byte green, byte blue, byte alpha) HSVAColorToRGBAColor3(double hue, double saturation, double value, double alpha)
        {
            double a;
            double r;
            double g;
            double b;
            int i;
            double f, p, q, t;
            if (saturation == 0)
            {
                // achromatic (gray)
                r = g = b = value;

                a = ((1d - alpha) * 255d) + 0.5d;
                r = ((1d - r) * 255d) + 0.5d;
                g = ((1d - g) * 255d) + 0.5d;
                b = ((1d - b) * 255d) + 0.5d;

                return ((byte)r, (byte)g, (byte)b, (byte)a);
            }

            hue /= 60;            // sector 0 to 5
            i = (int)Floor(hue);
            f = hue - i;          // factorial part of h
            p = value * (1d - saturation);
            q = value * (1d - (saturation * f));
            t = value * (1d - (saturation * (1d - f)));
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

            a = ((1d - alpha) * 255d) + 0.5d;
            r = ((1d - r) * 255d) + 0.5d;
            g = ((1d - g) * 255d) + 0.5d;
            b = ((1d - b) * 255d) + 0.5d;

            return ((byte)r, (byte)g, (byte)b, (byte)a);
        }

        /// <summary>
        /// The color from HSV.
        /// </summary>
        /// <param name="hue">The hue.</param>
        /// <param name="saturation">The saturation.</param>
        /// <param name="value">The value.</param>
        /// <param name="alpha"></param>
        /// <returns>The <see cref="RGBA"/>.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/359612/how-to-change-rgb-color-to-hsv
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (byte red, byte green, byte blue, byte alpha) HSVAColorToRGBAColor2(double hue, double saturation, double value, double alpha)
        {
            var hi = Convert.ToInt32(Floor(hue / 60)) % 6;
            var f = (hue / 60d) - Floor(hue / 60d);

            value *= 255d;
            var v = Convert.ToByte(value);
            var p = Convert.ToByte(value * (1d - saturation));
            var q = Convert.ToByte(value * (1d - (f * saturation)));
            var t = Convert.ToByte(value * (1d - ((1d - f) * saturation)));
            var a = Convert.ToByte(((1d - alpha) * 255d) + 0.5d);

            return hi switch
            {
                0 => (v, t, p, a),
                1 => (q, v, p, a),
                2 => (p, v, t, a),
                3 => (p, q, v, a),
                4 => (t, p, v, a),
                _ => (v, p, q, a),
            };
        }

        /// <summary>
        /// The AHS vto RGB.
        /// </summary>
        /// <param name="hue">The h.</param>
        /// <param name="saturation">The s.</param>
        /// <param name="value">The v.</param>
        /// <param name="alpha">The a.</param>
        /// <returns>The <see cref="RGBA"/>.</returns>
        /// <remarks>
        /// <para>h = [0,360], s = [0,1], v = [0,1]
        ///		if s == 0, then h = -1 (undefined)</para>
        /// </remarks>
        /// <acknowledgment>
        /// https://www.cs.rit.edu/~ncs/color/t_convert.html
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (byte red, byte green, byte blue, byte alpha) HSVAColorToRGBAColor(double hue, double saturation, double value, double alpha)
        {
            double a;
            double r;
            double g;
            double b;

            if (saturation == 0)
            {
                // achromatic (gray)
                r = g = b = value;

                a = ((1d - alpha) * 255d) + 0.5d;
                r = ((1d - r) * 255d) + 0.5d;
                g = ((1d - g) * 255d) + 0.5d;
                b = ((1d - b) * 255d) + 0.5d;

                return ((byte)r, (byte)g, (byte)b, (byte)a);
            }

            hue /= 60;            // sector 0 to 5
            var i = (int)Floor(hue);
            var f = hue - i;          // factorial part of h
            var p = value * (1d - saturation);
            var q = value * (1d - (saturation * f));
            var t = value * (1d - (saturation * (1d - f)));

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

            a = ((1d - alpha) * 255d) + 0.5d;
            r = ((1d - r) * 255d) + 0.5d;
            g = ((1d - g) * 255d) + 0.5d;
            b = ((1d - b) * 255d) + 0.5d;

            return ((byte)r, (byte)g, (byte)b, (byte)a);
        }

        /// <summary>
        /// The rgb f create from yiq.
        /// </summary>
        /// <param name="y">The y.</param>
        /// <param name="i">The i.</param>
        /// <param name="q">The q.</param>
        /// <param name="alpha"></param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double red, double green, double blue, double alpha) YIQAColorToRGBAFColor(double y, double i, double q, double alpha)
        {
            (y, i, q, alpha) = ClampYIQA(y, i, q, alpha);
            return (
                red: y + (0.9563d * i) + (0.6210d * q),
                green: y - (0.2721d * i) - (0.6474d * q),
                blue: y - (1.1070d * i) + (1.7046d * q), alpha
                );
        }

        /// <summary>
        /// The rgb f create from yuv.
        /// </summary>
        /// <param name="y">The y.</param>
        /// <param name="u">The u.</param>
        /// <param name="v">The v.</param>
        /// <param name="alpha"></param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double red, double green, double blue, double alpha) YUVAColorToRGBAFColor(double y, double u, double v, double alpha)
        {
            (y, u, v, alpha) = ClampYUVA(y, u, v, alpha);
            return (
                red: y + (1.140d * v),
                green: y - (0.395d * u) - (0.581d * v),
                blue: y + (2.032d * u), alpha
                );
        }

        /// <summary>
        /// The hsi create from rgb.
        /// </summary>
        /// <param name="red">The r.</param>
        /// <param name="green">The g.</param>
        /// <param name="blue">The b.</param>
        /// <param name="alpha"></param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double hue, double saturation, double intensity, double alpha) RGBAColorToHSIAColor(byte red, byte green, byte blue, byte alpha)
            => RGBAFColorToHSIAColor(red / 256d, green / 256d, blue / 256d, alpha / 256d);

        /// <summary>
        /// The hsi create from rgb f.
        /// </summary>
        /// <param name="red">The r.</param>
        /// <param name="green">The g.</param>
        /// <param name="blue">The b.</param>
        /// <param name="alpha"></param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double hue, double saturation, double intensity, double alpha) RGBAFColorToHSIAColor(double red, double green, double blue, double alpha)
        {
            (double hue, double saturation, double intensity, double alpha) color = (0d, 0d, 0d, alpha);
            var m = Min(red, green, blue);
            var M = Max(red, green, blue);
            var c = M - m;
            if (ValidateRGBAF(red, green, blue, alpha) == true)
            {
                color.intensity = 1d / 3d * (red + green + blue);
                if (c == 0)
                {
                    color.hue = 0d;
                    color.saturation = 0d;
                }
                else
                {
                    if (M == red)
                    {
                        color.hue = IEEERemainder((green - blue) / c, 6d);
                    }
                    else if (M == green)
                    {
                        color.hue = ((blue - red) / c) + 2d;
                    }
                    else if (M == blue)
                    {
                        color.hue = ((red - green) / c) + 4d;
                    }
                    color.hue *= 60d;
                    color.saturation = 1d - (m / color.intensity);
                }
            }
            return color;
        }

        /// <summary>
        /// The hsi create from rgb f.
        /// </summary>
        /// <param name="red">The r.</param>
        /// <param name="green">The g.</param>
        /// <param name="blue">The b.</param>
        /// <param name="alpha">The a.</param>
        /// <returns>The <see cref="HSIA"/>.</returns>
        /// <acknowledgment>
        /// http://dystopiancode.blogspot.com/2012/02/hsi-rgb-conversion-algorithms-in-c.html
        /// https://github.com/dystopiancode/colorspace-conversions
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double hue, double saturation, double intensity, double alpha) RGBAFColorToHSIAColor2(double red, double green, double blue, double alpha)
        {
            var m = Min(red, green);
            m = Min(m, blue);
            var M = Max(red, green);
            M = Max(M, blue);
            var c = M - m;

            var i = 1d / 3d * (red + green + blue);
            double h = 0;
            double s;
            if (c == 0)
            {
                h = 0d;
                s = 0d;
            }
            else
            {
                if (M == red)
                {
                    h = IEEERemainder((green - blue) / c, 6d);
                }
                else if (M == green)
                {
                    h = ((blue - red) / c) + 2d;
                }
                else if (M == blue)
                {
                    h = ((red - green) / c) + 4d;
                }

                h *= 60d;
                s = 1d - (m / i);
            }

            return (h, s, i, alpha);
        }

        /// <summary>
        /// The RGB fto HSI v2.
        /// </summary>
        /// <param name="red">The r.</param>
        /// <param name="green">The g.</param>
        /// <param name="blue">The b.</param>
        /// <param name="alpha"></param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <remarks><para>https://gist.github.com/rzhukov/9129585</para></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double hue, double saturation, double intensity, double alpha) RGBAFColorToHSIAColor3(double red, double green, double blue, double alpha)
        {
            var i = (red + green + blue) / 3d;
            var rn = red / (red + green + blue);
            var gn = green / (red + green + blue);
            var bn = blue / (red + green + blue);
            var h = Acos(0.5d * (rn - gn + (rn - bn)) / Sqrt(((rn - gn) * (rn - gn)) + ((rn - bn) * (gn - bn))));
            if (blue > green)
            {
                h = (2d * PI) - h;
            }
            var s = 1d - (3d * Min(rn, gn, bn));
            return (h, s, i, alpha);
        }

        /// <summary>
        /// The hsl create from rgb f.
        /// </summary>
        /// <param name="red">The r.</param>
        /// <param name="green">The g.</param>
        /// <param name="blue">The b.</param>
        /// <param name="alpha"></param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double hue, double saturation, double lumanance, double alpha) RGBAFColorToHSLAColor(double red, double green, double blue, double alpha)
        {
            (double hue, double saturation, double lumanance, double alpha) color = (0d, 0d, 0d, alpha);
            if (ValidateRGBAF(red, green, blue, alpha) == true)
            {
                var M = Max(red, green, blue);
                var m = Min(red, green, blue);
                var c = M - m;
                color.lumanance = 0.5d * (M + m);
                if (c != 0d)
                {
                    if (M == red)
                    {
                        color.hue = IEEERemainder((green - blue) / c, 6d);
                    }
                    else if (M == green)
                    {
                        color.hue = ((blue - red) / c) + 2d;
                    }
                    else/*if(M==b)*/
                    {
                        color.hue = ((red - green) / c) + 4d;
                    }
                    color.hue *= 60d;
                    color.saturation = c / (1d - Abs((2d * color.lumanance) - 1d));
                }
            }
            return color;
        }

        /// <summary>
        /// Given a Color (RGB class) in range of 0-255 Return H,S,L in range of 0-1
        /// </summary>
        /// <param name="alpha">Alpha value out.</param>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double hue, double saturation, double luminance, double alpha) RGBAColorToHSLAColor(double red, double green, double blue, double alpha)
        {
            var a = alpha / 255d;
            var r = red / 255d;
            var g = green / 255d;
            var b = blue / 255d;
            double vertexMin;

            var h = 0d; // default to black
            var s = 0d;
            var vertex = Max(Max(r, g), b);
            var min = Min(Min(r, g), b);
            var l = (min + vertex) / 2d;
            if (l <= 0d)
            {
                return (h, s, l, a);
            }

            vertexMin = vertex - min;
            s = vertexMin;
            if (s > 0d)
            {
                s /= (l <= 0.5d) ? (vertex + min) : (2d - vertex - min);
            }
            else
            {
                return (h, s, l, a);
            }

            var red2 = (vertex - r) / vertexMin;
            var green2 = (vertex - g) / vertexMin;
            var blue2 = (vertex - b) / vertexMin;
            if (r == vertex)
            {
                h = g == min ? 5d + blue2 : 1d - green2;
            }
            else
            {
                h = g == vertex ? b == min ? 1d + red2 : 3d - blue2 : r == min ? 3d + green2 : 5d - red2;
            }

            h /= 6d;
            return (h, s, l, a);
        }

        /// <summary>
        /// The hsv create from rgb f.
        /// </summary>
        /// <param name="red">The r.</param>
        /// <param name="green">The g.</param>
        /// <param name="blue">The b.</param>
        /// <param name="alpha"></param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <remarks><para>https://github.com/dystopiancode/colorspace-conversions/</para></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double hue, double saturation, double value, double alpha) RGBAFColorToHSVAColor(double red, double green, double blue, double alpha)
        {
            (double hue, double saturation, double value, double alpha) color = (0d, 0d, 0d, alpha);
            if (ValidateRGBAF(red, green, blue, alpha) == true)
            {
                var M = Max(red, green, blue);
                var m = Min(red, green, blue);
                var c = M - m;
                color.value = M;
                if (c != 0d)
                {
                    if (M == red)
                    {
                        color.hue = IEEERemainder((green - blue) / c, 6d);
                    }
                    else if (M == green)
                    {
                        color.hue = ((blue - red) / c) + 2d;
                    }
                    else /*if(M==b)*/
                    {
                        color.hue = ((red - green) / c) + 4d;
                    }
                    color.hue *= 60d;
                    color.saturation = c / color.value;
                }
            }
            return color;
        }

        /// <summary>
        /// The color to HSV.
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <param name="alpha"></param>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/359612/how-to-change-rgb-color-to-hsv
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double hue, double saturation, double value, double alpha) RGBAColorToHSVAColor(byte red, byte green, byte blue, byte alpha)
        {
            var max = Max(red, green, blue);
            var min = Min(red, green, blue);

            var hue = GetHue(red, green, blue);
            var saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            var value = max / 255d;

            return (hue, saturation, value, alpha / 255d);
        }

        /// <summary>
        /// The ARG bto AHSV.
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <param name="alpha"></param>
        /// <returns>The <see cref="HSVA"/>.</returns>
        /// <remarks>
        /// <para>h = [0,360], s = [0,1], v = [0,1]
        ///		if s == 0, then h = -1 (undefined)</para>
        /// </remarks>
        /// <acknowledgment>
        /// https://www.cs.rit.edu/~ncs/color/t_convert.html
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double hue, double saturation, double value, double alpha) RGBAColorToHSVAColor2(byte red, byte green, byte blue, byte alpha)
        {
            var a = 1d - (alpha / 255d);
            var r = 1d - (red / 255d);
            var g = 1d - (green / 255d);
            var b = 1d - (blue / 255d);

            var min = Min(r, g);
            min = Min(min, b);
            var max = Max(r, g);
            max = Max(max, b);
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
                return (h, s, v, a);
            }

            if (r == max)
            {
                h = (g - b) / delta;       // between yellow & magenta
            }
            else
            {
                h = g == max ? 2 + ((b - r) / delta) : 4 + ((r - g) / delta);   // between magenta & cyan
            }

            h *= 60;               // degrees
            if (h < 0)
            {
                h += 360;
            }

            return (h, s, v, a);
        }

        /// <summary>
        /// The yiq create from rgb f.
        /// </summary>
        /// <param name="red">The r.</param>
        /// <param name="green">The g.</param>
        /// <param name="blue">The b.</param>
        /// <param name="alpha"></param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <remarks>
        /// <para>https://github.com/dystopiancode/colorspace-conversions/
        /// Correction from: https://stackoverflow.com/q/22131920</para>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double y, double i, double q, double alpha) RGBAFColorToYIQAColor(double red, double green, double blue, double alpha)
        {
            (red, green, blue, alpha) = ClampRGBAF(red, green, blue, alpha);
            return (
                y: (0.299900d * red) + (0.587000d * green) + (0.114000d * blue),
                i: (0.595716d * red) - (0.274453d * green) - (0.321264d * blue),
                q: (0.211456d * red) - (0.522591d * green) + (0.311350d * blue),
                alpha
                );
        }

        /// <summary>
        /// The yuv create from rgb f.
        /// </summary>
        /// <param name="red">The r.</param>
        /// <param name="green">The g.</param>
        /// <param name="blue">The b.</param>
        /// <param name="alpha"></param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <remarks>
        /// <para>https://github.com/dystopiancode/colorspace-conversions/
        /// Correction found at: https://www.fourcc.org/fccyvrgb.php</para>
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double y, double u, double v, double alpha) RGBAFColorToYUVAColor(double red, double green, double blue, double alpha)
        {
            (red, green, blue, alpha) = ClampRGBAF(red, green, blue, alpha);
            var y = (0.299d * red) + (0.587d * green) + (0.114d * blue);
            return (
                y,
                u: 0.492d * (blue - y), // u: 0.565 * (b - y),
                v: 0.877d * (red - y), // v: 0.713d * (r - y)
                 alpha
                );
        }
        #endregion Conversion Methods
    }
}
