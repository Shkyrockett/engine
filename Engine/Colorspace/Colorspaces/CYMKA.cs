// <copyright file="CYMKA.cs" company="Shkyrockett" >
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
    /// CYMKA color class.
    /// </summary>
    public struct CYMKA
        : IColor
    {
        #region Implementations

        /// <summary>
        /// The empty (readonly). Value: new ACYMK().
        /// </summary>
        public static readonly CYMKA Empty = new CYMKA(0, 0, 0, 0);

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CYMKA"/> class.
        /// </summary>
        /// <param name="color">The color.</param>
        public CYMKA(RGBA color)
        {
            (Alpha, Cyan, Yellow, Magenta, Black) = Colorspaces.RGBAColorToCYMKAColor(color.Red, color.Green, color.Blue, color.Alpha);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CYMKA"/> class.
        /// </summary>
        /// <param name="cyan">Cyan color component.</param>
        /// <param name="yellow">Yellow color component.</param>
        /// <param name="magenta">Magenta color component.</param>
        /// <param name="black">Black color component.</param>
        public CYMKA(byte cyan, byte yellow, byte magenta, byte black)
            : this(cyan, yellow, magenta, black, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CYMKA"/> class.
        /// </summary>
        /// <param name="cyan">Cyan color component.</param>
        /// <param name="yellow">Yellow color component.</param>
        /// <param name="magenta">Magenta color component.</param>
        /// <param name="black">Black color component.</param>
        /// <param name="alpha">Alpha color component.</param>
        public CYMKA(byte cyan, byte yellow, byte magenta, byte black, byte alpha)
        {
            Cyan = cyan;
            Yellow = yellow;
            Magenta = magenta;
            Black = black;
            Alpha = alpha;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the cyan color value.
        /// </summary>
        public byte Cyan { get; set; }

        /// <summary>
        /// Gets or sets the green color value.
        /// </summary>
        public byte Yellow { get; set; }

        /// <summary>
        /// Gets or sets the blue color value.
        /// </summary>
        public byte Magenta { get; set; }

        /// <summary>
        /// Gets or sets the black color value.
        /// </summary>
        public byte Black { get; set; }

        /// <summary>
        /// Gets or sets the alpha color value.
        /// </summary>
        public byte Alpha { get; set; }

        #endregion

        #region Operators

        /// <summary>
        /// Compares two <see cref="CYMKA"/> objects.
        /// The result specifies whether the color values of the two <see cref="CYMKA"/> objects are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(CYMKA left, CYMKA right)
            => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="CYMKA"/> objects.
        /// The result specifies whether the color values of the two <see cref="CYMKA"/> objects are unequal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(CYMKA left, CYMKA right)
            => !Equals(left, right);

        #endregion

        #region Methods

        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
            => Cyan.GetHashCode() | Yellow.GetHashCode() | Magenta.GetHashCode() | Black.GetHashCode() | Alpha.GetHashCode();

        /// <summary>
        /// Compares two <see cref="CYMKA"/> colors
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(CYMKA a, CYMKA b)
            => Equals(a, b);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(CYMKA a, CYMKA b)
            => (a.Cyan == b.Cyan && a.Yellow == b.Yellow && a.Magenta == b.Magenta && a.Black == b.Black && a.Alpha == b.Alpha);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is CYMKA && Equals(this, (CYMKA)obj);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(IColor other)
        {
            var (r0, g0, b0, a0) = ToRGBATuple();
            var (r1, g1, b1, a1) = other.ToRGBATuple();
            return r0 == r1 && g0 == g1 && b0 == b1 && a0 == a1;
        }

        /// <summary>
        /// Converts the <see cref="CYMKA"/> class to a <see cref="RGBA"/> class.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGBA ToARGB()
            => new RGBA(ToRGBATuple());

        /// <summary>
        /// Converts the <see cref="CYMKA"/> class to a <see cref="RGBA"/> class.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// CMYK --> RGB
        /// Red   = 1-minimum(1,Cyan*(1-Black)+Black)
        /// Green = 1-minimum(1,Magenta*(1-Black)+Black)
        /// Blue  = 1-minimum(1,Yellow*(1-Black)+Black)
        /// </remarks>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/4488/XCmyk-CMYK-to-RGB-Calculator-with-source-code
        /// The algorithms for these routines were taken from: http://web.archive.org/web/20030416004239/http://www.neuro.sfc.keio.ac.jp/~aly/polygon/info/color-space-faq.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (byte red, byte green, byte blue, byte alpha) ToRGBATuple()
            => Colorspaces.CYMKAColorToRGBAColor(Cyan, Yellow, Magenta, Black, Alpha);

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="CYMKA"/> struct.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="CYMKA"/> struct based on the IFormatProvider
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
        /// Creates a string representation of this <see cref="CYMKA"/> class based on the format string
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
        /// Creates a string representation of this <see cref="CYMKA"/> struct based on the format string
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
            return $"{nameof(CYMKA)}{{{nameof(Cyan)}={Cyan.ToString(format, provider)}{sep}{nameof(Yellow)}={Yellow.ToString(format, provider)}{sep}{nameof(Magenta)}={Magenta.ToString(format, provider)}{sep}{nameof(Black)}={Black.ToString(format, provider)}{sep}{nameof(Alpha)}={Alpha.ToString(format, provider)}}}";
        }

        #endregion
    }
}
