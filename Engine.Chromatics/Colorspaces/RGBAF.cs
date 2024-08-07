﻿// <copyright file="RGBAF.cs" company="Shkyrockett" >
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
/// Red Green Blue Alpha color class.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public struct RGBAF
    : IColor, IEquatable<RGBAF>
{
    #region Implementations
    /// <summary>
    /// The empty (readonly). Value: new RGBAF(0, 0, 0, 0).
    /// </summary>
    public static readonly RGBAF Empty = new(0, 0, 0, 0);
    #endregion Implementations

    #region Fields
    /// <summary>
    /// The name of the color.
    /// </summary>
    private string name;
    #endregion Fields

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="RGBAF"/> class.
    /// </summary>
    /// <param name="color"></param>
    public RGBAF(RGBA color)
    {
        (Red, Green, Blue, Alpha) = Colorspaces.RGBAColorToRGBAFColor(color.Red, color.Green, color.Blue, color.Alpha);
        name = color.Name;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RGBAF"/> class.
    /// </summary>
    /// <param name="red">Red color component.</param>
    /// <param name="green">Green color component.</param>
    /// <param name="blue">Blue color component.</param>
    /// <param name="name">The name of the color.</param>
    public RGBAF(double red, double green, double blue, string name = "")
        : this(red, green, blue, 0, name)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="RGBAF"/> class.
    /// </summary>
    /// <param name="tuple"><see cref="ValueTuple"/> representing the Alpha, Red, Green, and Blue components in an RGBA color.</param>
    /// <param name="name">The name of the color.</param>
    public RGBAF((double R, double G, double B, double A) tuple, string name = "")
        : this(tuple.R, tuple.G, tuple.B, tuple.A, name)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="RGBA"/> class.
    /// </summary>
    /// <param name="red">Red color component.</param>
    /// <param name="green">Green color component.</param>
    /// <param name="blue">Blue color component.</param>
    /// <param name="alpha">Alpha color component.</param>
    /// <param name="name">The name of the color.</param>
    public RGBAF(double red, double green, double blue, double alpha, string name = "")
    {
        this.name = name;
        Alpha = alpha;
        Red = red;
        Green = green;
        Blue = blue;
    }
    #endregion Constructors

    #region Properties
    /// <summary>
    /// Gets or sets the red color value.
    /// </summary>
    public double Red { get; set; }

    /// <summary>
    /// Gets or sets the green color value.
    /// </summary>
    public double Green { get; set; }

    /// <summary>
    /// Gets or sets the blue color value.
    /// </summary>
    public double Blue { get; set; }

    /// <summary>
    /// Gets or sets the alpha color value.
    /// </summary>
    public double Alpha { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { readonly get => name; set => name = value; }
    #endregion Properties

    #region Operators
    /// <summary>
    /// Compares two <see cref="RGBAF" /> objects.
    /// The result specifies whether the color values of the two <see cref="RGBAF" /> objects are equal.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool operator ==(RGBAF left, RGBAF right)
        => Equals(left, right);

    /// <summary>
    /// Compares two <see cref="RGBAF" /> objects.
    /// The result specifies whether the color values of the two <see cref="RGBAF" /> objects are unequal.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool operator !=(RGBAF left, RGBAF right)
        => !Equals(left, right);
    #endregion Operators

    #region Methods
    /// <summary>
    /// Get the brightness.
    /// </summary>
    /// <returns>The <see cref="double"/>.</returns>
    public readonly double GetBrightness()
        => Colorspaces.GetLuminanceFloat(Red, Green, Blue);

    /// <summary>
    /// Get the hue.
    /// </summary>
    /// <returns>The <see cref="double"/>.</returns>
    public readonly double GetHue()
        => Colorspaces.GetHue(Red, Green, Blue);

    /// <summary>
    /// Get the saturation.
    /// </summary>
    /// <returns>The <see cref="double"/>.</returns>
    public readonly double GetSaturation()
        => Colorspaces.GetSaturation(Red, Green, Blue);
    #endregion Methods

    #region Standard Methods
    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>The <see cref="int"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override readonly int GetHashCode() => Alpha.GetHashCode() | Red.GetHashCode() | Green.GetHashCode() | Blue.GetHashCode();

    /// <summary>
    /// Compares two <see cref="RGBA"/> colors
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool Compare(RGBAF a, RGBAF b) => Equals(a, b);

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool Equals(RGBAF a, RGBAF b) => a.Red == b.Red && a.Green == b.Green && a.Blue == b.Blue && a.Alpha == b.Alpha;

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public readonly bool Equals(RGBAF other) => Red == other.Red && Green == other.Green && Blue == other.Blue && Alpha == other.Alpha;

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="obj">The obj.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Equals(object? obj) => obj is RGBAF color && Equals(color);

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="other">The value.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Equals(IColor? other)
    {
        var (redA, greenA, blueA, alphaA) = ToRGBATuple();
        var (redB, greenB, blueB, alphaB) = (other?.ToRGBATuple()).Value;
        return alphaA == alphaB && redA == redB && greenA == greenB && blueA == blueB;
    }

    /// <summary>
    /// The to RGBA tuple.
    /// </summary>
    /// <returns>The <see cref="ValueTuple"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly (byte Red, byte Green, byte Blue, byte Alpha) ToRGBATuple() => ((byte)(Red * 255), (byte)(Green * 255), (byte)(Blue * 255), (byte)(Alpha * 255));

    /// <summary>
    /// Creates a human-readable string that represents this <see cref="RGBAF"/> struct.
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override string ToString() => ConvertToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

    /// <summary>
    /// Creates a string representation of this <see cref="RGBAF"/> struct based on the IFormatProvider
    /// passed in.  If the provider is null, the CurrentCulture is used.
    /// </summary>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public string ToString(IFormatProvider? provider) => ConvertToString(string.Empty /* format string */, provider);

    /// <summary>
    /// Creates a string representation of this <see cref="RGBAF"/> class based on the format string
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
    public string ToString(string? format, IFormatProvider? formatProvider) => ConvertToString(format /* format string */, formatProvider /* format provider */);

    /// <summary>
    /// Creates a string representation of this <see cref="RGBAF"/> struct based on the format string
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
    public readonly string ConvertToString(string? format, IFormatProvider? formatProvider)
    {
        var sep = Tokenizer.GetNumericListSeparator(formatProvider);
        return $"{nameof(RGBAF)}{{{nameof(Red)}={Red.ToString(format, formatProvider)}{sep}{nameof(Green)}={Green.ToString(format, formatProvider)}{sep}{nameof(Blue)}={Blue.ToString(format, formatProvider)}{sep}{nameof(Alpha)}={Alpha.ToString(format, formatProvider)}}}";
    }
    #endregion Standard Methods
}
