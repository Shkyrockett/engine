// <copyright file="HSLA.cs" company="Shkyrockett" >
// Copyright © 2013 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Engine.ColorSpace;

/// <summary>
///   <see cref="HSLA" /> Color
/// </summary>
/// <seealso cref="IColor" />
/// <seealso cref="IEquatable{T}" />
[DebuggerDisplay("{ToString()}")]
public struct HSLA
    : IColor, IEquatable<HSLA>
{
    #region Implementations
    /// <summary>
    /// The empty (readonly). Value: new HSLA().
    /// </summary>
    public static readonly HSLA Empty;
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
    /// Initializes a new instance of the <see cref="HSLA" /> class Converted from RGB to HSL.
    /// </summary>
    /// <param name="color">A Color to convert</param>
    /// <remarks>
    /// <para>Takes advantage of whats already built in to .NET by using the Color.GetHue,
    /// Color.GetSaturation and Color.GetBrightness methods
    /// we store hue as 0-1 as opposed to 0-360</para>
    /// </remarks>
    /// <returns>An HSL value</returns>
    public HSLA(RGBA color)
    {
        (hue, saturation, luminance, alpha) = Colorspaces.RGBAColorToHSLAColor(color.Red, color.Green, color.Blue, color.Alpha);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HSLA" /> class.
    /// </summary>
    /// <param name="hue">Hue color component.</param>
    /// <param name="saturation">Saturation color component.</param>
    /// <param name="luminance">Luminance color component.</param>
    public HSLA(double hue, double saturation, double luminance)
        : this(hue, saturation, luminance, 0)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="HSLA" /> class.
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
    /// <value>
    /// The hue.
    /// </value>
    public double Hue
    { readonly get => hue;
        set =>
            //hue = value;
            hue = value > 1 ? 1 : value < 0 ? 0 : value;
    }

    /// <summary>
    /// Gets or sets the saturation color value.
    /// </summary>
    /// <value>
    /// The saturation.
    /// </value>
    public double Saturation
    { readonly get => saturation;
        set =>
            //saturation = value;
            saturation = value > 1 ? 1 : value < 0 ? 0 : value;
    }

    /// <summary>
    /// Gets or sets the luminance color value.
    /// </summary>
    /// <value>
    /// The luminance.
    /// </value>
    public double Luminance
    { readonly get => luminance;
        set =>
            //luminance = value;
            luminance = value > 1 ? 1 : value < 0 ? 0 : value;
    }

    /// <summary>
    /// Gets or sets the alpha color value.
    /// </summary>
    /// <value>
    /// The alpha.
    /// </value>
    public double Alpha
    { readonly get => alpha;
        set =>
            //alpha = value;
            alpha = value > 1 ? 1 : value < 0 ? 0 : value;
    }
    #endregion Properties

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(HSLA left, HSLA right) => left.Equals(right);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(HSLA left, HSLA right) => !(left == right);

    #region Methods
    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals(object? obj) => obj is HSLA color && Equals(color);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public readonly bool Equals([AllowNull] HSLA other) => hue == other.hue && saturation == other.saturation && luminance == other.luminance && alpha == other.alpha;

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="other">The other.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    public bool Equals(IColor? other)
    {
        var (r0, g0, b0, a0) = ToRGBATuple();
        var (r1, g1, b1, a1) = (other?.ToRGBATuple()).Value;
        return r0 == r1 && g0 == g1 && b0 == b1 && a0 == a1;
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override readonly int GetHashCode() => HashCode.Combine(hue, saturation, luminance, alpha);

    /// <summary>
    /// The to RGBA tuple.
    /// </summary>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3, T4}" />.
    /// </returns>
    public readonly (byte Red, byte Green, byte Blue, byte Alpha) ToRGBATuple() => Colorspaces.HSLAColorToRGBAColor(hue, saturation, luminance, alpha);

    /// <summary>
    /// Creates a human-readable string that represents this <see cref="HSLA" /> struct.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override string ToString() => ConvertToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

    /// <summary>
    /// Creates a string representation of this <see cref="HSLA" /> struct based on the IFormatProvider
    /// passed in.  If the provider is null, the CurrentCulture is used.
    /// </summary>
    /// <param name="formatProvider">The provider.</param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public string ToString(IFormatProvider? formatProvider) => ConvertToString(string.Empty /* format string */, formatProvider);

    /// <summary>
    /// Creates a string representation of this <see cref="HSLA" /> class based on the format string
    /// and IFormatProvider passed in.
    /// If the provider is null, the CurrentCulture is used.
    /// See the documentation for IFormattable for more information.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="formatProvider">The provider.</param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public string ToString(string? format, IFormatProvider? formatProvider) => ConvertToString(format /* format string */, formatProvider /* format provider */);

    /// <summary>
    /// Creates a string representation of this <see cref="HSLA" /> struct based on the format string
    /// and IFormatProvider passed in.
    /// If the provider is null, the CurrentCulture is used.
    /// See the documentation for IFormattable for more information.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="formatProvider">The provider.</param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public string ConvertToString(string? format, IFormatProvider? formatProvider)
    {
        var sep = Tokenizer.GetNumericListSeparator(formatProvider);
        return $"{nameof(HSLA)}{{{nameof(Hue)}={hue.ToString(format, formatProvider)}{sep}{nameof(Saturation)}={saturation.ToString(format, formatProvider)}{sep}{nameof(Luminance)}={luminance.ToString(format, formatProvider)}{sep}{nameof(Alpha)}={Alpha.ToString(format, formatProvider)}}}";
    }
    #endregion Methods
}
