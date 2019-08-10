// <copyright file="CMYKA.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2019 Shkyrockett. All rights reserved.
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
    /// CMYKA color class.
    /// </summary>
    [DebuggerDisplay("{ToString()}")]
    public struct CMYKA
        : IColor, IEquatable<CMYKA>
    {
        #region Implementations
        /// <summary>
        /// The empty (readonly). Value: new ACMYK().
        /// </summary>
        public static readonly CMYKA Empty = new CMYKA(0, 0, 0, 0);
        #endregion Implementations

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CMYKA"/> class.
        /// </summary>
        /// <param name="color">The color.</param>
        public CMYKA(RGBA color)
        {
            (Alpha, Cyan, Yellow, Magenta, Black) = Colorspaces.RGBAColorToCMYKAColor(color.Red, color.Green, color.Blue, color.Alpha);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CMYKA"/> class.
        /// </summary>
        /// <param name="cyan">Cyan color component.</param>
        /// <param name="yellow">Yellow color component.</param>
        /// <param name="magenta">Magenta color component.</param>
        /// <param name="black">Black color component.</param>
        public CMYKA(byte cyan, byte yellow, byte magenta, byte black)
            : this(cyan, yellow, magenta, black, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CMYKA"/> class.
        /// </summary>
        /// <param name="cyan">Cyan color component.</param>
        /// <param name="yellow">Yellow color component.</param>
        /// <param name="magenta">Magenta color component.</param>
        /// <param name="black">Black color component.</param>
        /// <param name="alpha">Alpha color component.</param>
        public CMYKA(byte cyan, byte yellow, byte magenta, byte black, byte alpha)
        {
            Cyan = cyan;
            Yellow = yellow;
            Magenta = magenta;
            Black = black;
            Alpha = alpha;
        }
        #endregion Constructors

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
        #endregion Properties

        #region Operators
        /// <summary>
        /// Compares two <see cref="CMYKA" /> objects.
        /// The result specifies whether the color values of the two <see cref="CMYKA" /> objects are equal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(CMYKA left, CMYKA right)
            => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="CMYKA" /> objects.
        /// The result specifies whether the color values of the two <see cref="CMYKA" /> objects are unequal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(CMYKA left, CMYKA right)
            => !Equals(left, right);
        #endregion Operators

        #region Methods
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// The <see cref="int" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
            => Cyan.GetHashCode() | Yellow.GetHashCode() | Magenta.GetHashCode() | Black.GetHashCode() | Alpha.GetHashCode();

        /// <summary>
        /// Compares two <see cref="CMYKA" /> colors
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(CMYKA a, CMYKA b)
            => Equals(a, b);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(CMYKA a, CMYKA b)
            => a.Cyan == b.Cyan && a.Yellow == b.Yellow && a.Magenta == b.Magenta && a.Black == b.Black && a.Alpha == b.Alpha;

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is CMYKA color && Equals(color);

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
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.
        /// </returns>
        public bool Equals(CMYKA other) => Cyan == other.Cyan && Yellow == other.Yellow && Magenta == other.Magenta && Black == other.Black && Alpha == other.Alpha;

        /// <summary>
        /// Converts the <see cref="CMYKA"/> class to a <see cref="RGBA"/> class.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGBA ToRGBA()
            => new RGBA(ToRGBATuple());

        /// <summary>
        /// Converts the <see cref="CMYKA"/> class to a <see cref="RGBA"/> class.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// <para>CMYK --&gt; RGB
        /// Red   = 1-minimum(1,Cyan*(1-Black)+Black)
        /// Green = 1-minimum(1,Magenta*(1-Black)+Black)
        /// Blue  = 1-minimum(1,Yellow*(1-Black)+Black)</para>
        /// </remarks>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/4488/XCmyk-CMYK-to-RGB-Calculator-with-source-code
        /// The algorithms for these routines were taken from: http://web.archive.org/web/20030416004239/http://www.neuro.sfc.keio.ac.jp/~aly/polygon/info/color-space-faq.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (byte red, byte green, byte blue, byte alpha) ToRGBATuple()
            => Colorspaces.CMYKAColorToRGBAColor(Cyan, Yellow, Magenta, Black, Alpha);

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="CMYKA"/> struct.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="CMYKA"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider)
            => ConvertToString(string.Empty /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="CMYKA"/> class based on the format string
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
        public string ToString(string format, IFormatProvider provider)
            => ConvertToString(format /* format string */, provider /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="CMYKA"/> struct based on the format string
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
        public string ConvertToString(string format, IFormatProvider provider)
        {
            var sep = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(CMYKA)}{{{nameof(Cyan)}={Cyan.ToString(format, provider)}{sep}{nameof(Yellow)}={Yellow.ToString(format, provider)}{sep}{nameof(Magenta)}={Magenta.ToString(format, provider)}{sep}{nameof(Black)}={Black.ToString(format, provider)}{sep}{nameof(Alpha)}={Alpha.ToString(format, provider)}}}";
        }
        #endregion Methods
    }
}
