// <copyright file="HSLA.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Engine.Colorspace
{
    /// <summary>
    /// <see cref="HSLA"/> Color
    /// </summary>
    public struct HSLA
        : IColor
    {
        #region Implementations
        /// <summary>
        /// The empty (readonly). Value: new HSLA().
        /// </summary>
        public static readonly HSLA Empty = new HSLA();
        #endregion Implementations

        #region Fields
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
        /// Alpha color component.
        /// </summary>
        private double alpha;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="HSLA"/> class Converted from RGB to HSL.
        /// </summary>
        /// <param name="color">A Color to convert</param>
        /// <remarks>
        /// Takes advantage of whats already built in to .NET by using the Color.GetHue,
        /// Color.GetSaturation and Color.GetBrightness methods
        /// we store hue as 0-1 as opposed to 0-360
        /// </remarks>
        /// <returns>An HSL value</returns>
        public HSLA(RGBA color)
        {
            (hue, saturation, luminance, alpha) = Colorspaces.RGBAColorToHSLAColor(color.Red, color.Green, color.Blue, color.Alpha);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HSLA"/> class.
        /// </summary>
        /// <param name="hue">Hue color component.</param>
        /// <param name="saturation">Saturation color component.</param>
        /// <param name="luminance">Luminance color component.</param>
        public HSLA(double hue, double saturation, double luminance)
            : this(hue, saturation, luminance, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HSLA"/> class.
        /// </summary>
        /// <param name="hue">Hue color component.</param>
        /// <param name="saturation">Saturation color component.</param>
        /// <param name="luminance">Luminance color component.</param>
        /// <param name="alpha">Alpha color component.</param>
        public HSLA(double hue, double saturation, double luminance, double alpha)
        {
            this.hue = hue > 1 ? 1 : hue < 0 ? 0 : hue;
            this.saturation = saturation > 1 ? 1 : saturation < 0 ? 0 : saturation;
            this.luminance = luminance > 1 ? 1 : luminance < 0 ? 0 : luminance;
            this.alpha = alpha > 1 ? 1 : alpha < 0 ? 0 : alpha;
        }
        #endregion Constructors

        #region Properties
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
        #endregion Properties

        #region Methods
        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Equals(IColor other)
        {
            var (r0, g0, b0, a0) = ToRGBATuple();
            var (r1, g1, b1, a1) = other.ToRGBATuple();
            return r0 == r1 && g0 == g1 && b0 == b1 && a0 == a1;
        }

        /// <summary>
        /// The to RGBA tuple.
        /// </summary>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4}"/>.</returns>
        public (byte red, byte green, byte blue, byte alpha) ToRGBATuple()
            => Colorspaces.HSLAColorToRGBAColor(hue, saturation, luminance, alpha);

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="HSLA"/> struct.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="HSLA"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="HSLA"/> class based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        string IFormattable.ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="HSLA"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal string ConvertToString(string format, IFormatProvider provider)
        {
            var sep = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(HSLA)}{{{nameof(Hue)}={hue.ToString(format, provider)}{sep}{nameof(Saturation)}={saturation.ToString(format, provider)}{sep}{nameof(Luminance)}={luminance.ToString(format, provider)}{sep}{nameof(Alpha)}={Alpha.ToString(format, provider)}}}";
        }
        #endregion Methods
    }
}
