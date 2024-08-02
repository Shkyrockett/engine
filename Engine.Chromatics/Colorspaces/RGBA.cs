// <copyright file="RGBA.cs" company="Shkyrockett" >
// Copyright © 2013 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Engine.ColorSpace;

/// <summary>
/// Red Green Blue Alpha color struct.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public struct RGBA
    : IColor, IEquatable<RGBA>
{
    #region Implementations
    /// <summary>
    /// The empty Value: new RGBA(0, 0, 0, 0).
    /// </summary>
    public static readonly RGBA Empty = new(0, 0, 0, 0);
    #endregion Implementations

    #region Constants
    /// <summary>
    /// The red shift value.
    /// </summary>
    private const int redShift = 0x18;

    /// <summary>
    /// The green shift value.
    /// </summary>
    private const int greenShift = 0x10;

    /// <summary>
    /// The blue shift value.
    /// </summary>
    private const int blueShift = 0x8;

    /// <summary>
    /// The alpha shift Value.
    /// </summary>
    private const int alphaShift = 0x0;
    #endregion Constants

    #region Fields
    /// <summary>
    /// The integer value of the color.
    /// </summary>
    private int value;

    /// <summary>
    /// The name of the color.
    /// </summary>
    private string name;
    #endregion Fields

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="RGBA"/> class.
    /// </summary>
    /// <param name="value">A standard color.</param>
    /// <param name="name">The name of the color.</param>
    public RGBA(int value, string name = "")
    {
        this.name = name;
        this.value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RGBA"/> class.
    /// </summary>
    /// <param name="red">Red color component.</param>
    /// <param name="green">Green color component.</param>
    /// <param name="blue">Blue color component.</param>
    /// <param name="name">The name of the color.</param>
    public RGBA(byte red, byte green, byte blue, string name = "")
        : this(red, green, blue, 0, name)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="RGBA"/> class.
    /// </summary>
    /// <param name="tuple"><see cref="ValueTuple{T1, T2, T3, T4}"/> representing the Red, Green, Blue, and Alpha components in an RGBA color.</param>
    /// <param name="name">The name of the color.</param>
    public RGBA((byte red, byte green, byte blue, byte alpha) tuple, string name = "")
        : this(tuple.red, tuple.green, tuple.blue, tuple.alpha, name)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="RGBA"/> class.
    /// </summary>
    /// <param name="red">Red color component.</param>
    /// <param name="green">Green color component.</param>
    /// <param name="blue">Blue color component.</param>
    /// <param name="alpha">Alpha color component.</param>
    /// <param name="name">The name of the color.</param>
    public RGBA(byte red, byte green, byte blue, byte alpha, string name = "")
    {
        this.name = name;
        value =
            (red << redShift)
            | (green << greenShift)
            | (blue << blueShift)
            | (alpha << alphaShift);// & 0xffffffff;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="color"></param>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public static RGBA FromRGBA(RGBA color, byte alpha) => new(color.Red, color.Green, color.Blue, alpha);
    #endregion Constructors

    #region Properties
    /// <summary>
    /// Gets or sets the red color value.
    /// </summary>
    public byte Red
    {
        get => (byte)((Value >> redShift) & 0xFF);
        set => this.value |= value << redShift;
    }

    /// <summary>
    /// Gets or sets the green color value.
    /// </summary>
    public byte Green
    {
        get => (byte)((Value >> greenShift) & 0xFF);
        set => this.value |= value << greenShift;
    }

    /// <summary>
    /// Gets or sets the blue color value.
    /// </summary>
    public byte Blue
    {
        get => (byte)((Value >> blueShift) & 0xFF);
        set => this.value |= value << blueShift;
    }

    /// <summary>
    /// Gets or sets the alpha color value.
    /// </summary>
    public byte Alpha
    {
        get => (byte)((Value >> alphaShift) & 0xFF);
        set => this.value |= value << alphaShift;
    }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    public int Value
    { readonly get => value;
        set => this.value = value;
    }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name
    { readonly get => name;
        set => name = value;
    }
    #endregion Properties

    #region Operators
    /// <summary>
    /// Compares two <see cref="RGBA" /> objects.
    /// The result specifies whether the color values of the two <see cref="RGBA" /> objects are equal.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool operator ==(RGBA left, RGBA right) => Equals(left, right);

    /// <summary>
    /// Compares two <see cref="RGBA" /> objects.
    /// The result specifies whether the color values of the two <see cref="RGBA" /> objects are unequal.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool operator !=(RGBA left, RGBA right) => !Equals(left, right);
    #endregion Operators

    #region Methods
    /// <summary>
    /// Get the brightness.
    /// </summary>
    /// <returns>The <see cref="double"/>.</returns>
    public double GetBrightness() => Colorspaces.GetLuminance(Red, Green, Blue);

    /// <summary>
    /// Get the hue.
    /// </summary>
    /// <returns>The <see cref="double"/>.</returns>
    public double GetHue() => Colorspaces.GetHue(Red, Green, Blue);

    /// <summary>
    /// Get the saturation.
    /// </summary>
    /// <returns>The <see cref="double"/>.</returns>
    public double GetSaturation() => Colorspaces.GetSaturation(Red, Green, Blue);
    #endregion Methods

    #region Standard Methods
    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>The <see cref="int"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override readonly int GetHashCode() => value.GetHashCode();

    /// <summary>
    /// Compares two <see cref="RGBA"/> colors
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool Compare(RGBA a, RGBA b) => Equals(a, b);

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool Equals(RGBA a, RGBA b) => a.value == b.value;

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public readonly bool Equals(RGBA other) => value == other.value;

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="obj">The obj.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Equals(object? obj) => obj is RGBA color && Equals(color);

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly bool Equals(IColor value) => Equals(this, value);

    /// <summary>
    /// The to RGBA tuple.
    /// </summary>
    /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4}"/>.</returns>
    public (byte Red, byte Green, byte Blue, byte Alpha) ToRGBATuple() => (Red, Green, Blue, Alpha);

    /// <summary>
    /// Creates a human-readable string that represents this <see cref="RGBA"/> struct.
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override string ToString() => ConvertToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

    /// <summary>
    /// Creates a string representation of this <see cref="RGBA"/> struct based on the IFormatProvider
    /// passed in.  If the provider is null, the CurrentCulture is used.
    /// </summary>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public string ToString(IFormatProvider? formatProvider) => ConvertToString(string.Empty /* format string */, formatProvider);

    /// <summary>
    /// Creates a string representation of this <see cref="RGBA"/> class based on the format string
    /// and IFormatProvider passed in.
    /// If the provider is null, the CurrentCulture is used.
    /// See the documentation for IFormattable for more information.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="formatProvider"></param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public string ToString(string? format, IFormatProvider? formatProvider) => ConvertToString(format, formatProvider);

    /// <summary>
    /// Creates a string representation of this <see cref="RGBA"/> struct based on the format string
    /// and IFormatProvider passed in.
    /// If the provider is null, the CurrentCulture is used.
    /// See the documentation for IFormattable for more information.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="formatProvider"></param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public string ConvertToString(string? format, IFormatProvider? formatProvider)
    {
        var sep = Tokenizer.GetNumericListSeparator(formatProvider);
        return $"{nameof(RGBA)}{{{nameof(Red)}={Red.ToString(format, formatProvider)}{sep}{nameof(Green)}={Green.ToString(format, formatProvider)}{sep}{nameof(Blue)}={Blue.ToString(format, formatProvider)}{sep}{nameof(Alpha)}={Alpha.ToString(format, formatProvider)}{sep}{nameof(Name)}={name}}}";
    }
    #endregion Standard Methods
}
