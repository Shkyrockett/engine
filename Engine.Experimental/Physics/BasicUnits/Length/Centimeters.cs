// <copyright file="Centimeters.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Engine;

/// <summary>
/// The centimeters struct.
/// </summary>
/// <seealso cref="ILength{T}" />
/// <seealso cref="IFormattable" />
/// <seealso cref="IEquatable{T}" />
public struct Centimeters<T>
    : ILength<T>, IFormattable, IEquatable<Centimeters<T>>
    where T : INumber<T>
{
    #region Constants
    /// <summary>
    /// The mil (const). Value: MilsInCentimeter.
    /// </summary>
    public static readonly T Mil = LengthUnits<T>.MilsInCentimeter; // 393.700787d;

    /// <summary>
    /// The centimeter (const). Value: 1d.
    /// </summary>
    public static readonly T Centimeter = T.One;

    /// <summary>
    /// The inch (const). Value: 1d / 2.54d.
    /// </summary>
    public static readonly T Inch = T.One / T.CreateSaturating(2.54);

    /// <summary>
    /// The foot (const). Value: 1d / 30.48d.
    /// </summary>
    public static readonly T Foot = T.One / T.CreateSaturating(30.48);

    /// <summary>
    /// The yard (const). Value: 1d / 91.44d.
    /// </summary>
    public static readonly T Yard = T.One / T.CreateSaturating(91.44);

    /// <summary>
    /// The meter (const). Value: 1d / 100d.
    /// </summary>
    public static readonly T Meter = T.One / T.CreateSaturating(100);

    /// <summary>
    /// The smoot (const). Value: Inch * 67d.
    /// </summary>
    public static readonly T Smoot = Inch * T.CreateSaturating(67);

    /// <summary>
    /// The kilometer (const). Value: 1d / 100000d.
    /// </summary>
    public static readonly T Kilometer = T.One / T.CreateSaturating(100000);

    /// <summary>
    /// The mile (const). Value: 1d / 160934.4d.
    /// </summary>
    public static readonly T Mile = T.One / T.CreateSaturating(160934.4);

    /// <summary>
    /// The nautical mile (const). Value: 1d / 185200d.
    /// </summary>
    public static readonly T NauticalMile = T.One / T.CreateSaturating(185200);
    #endregion Constants

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Centimeters" /> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public Centimeters(T value)
    {
        Value = value;
    }
    #endregion Constructors

    #region Properties
    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>
    /// The value.
    /// </value>
    public T Value { get; set; }

    /// <summary>
    /// Gets or sets the mils.
    /// </summary>
    /// <value>
    /// The mils.
    /// </value>
    public T Mils
    {
        readonly get { return Value * Mil; }
        set { Value = value / Mil; }
    }

    /// <summary>
    /// Gets or sets the inches.
    /// </summary>
    /// <value>
    /// The inches.
    /// </value>
    public T Inches
    {
        readonly get { return Value * Inch; }
        set { Value = value / Inch; }
    }

    /// <summary>
    /// Gets or sets the feet.
    /// </summary>
    /// <value>
    /// The feet.
    /// </value>
    public T Feet
    {
        readonly get { return Value * Foot; }
        set { Value = value / Foot; }
    }

    /// <summary>
    /// Gets or sets the yards.
    /// </summary>
    /// <value>
    /// The yards.
    /// </value>
    public T Yards
    {
        readonly get { return Value * Yard; }
        set { Value = value / Yard; }
    }

    /// <summary>
    /// Gets or sets the meters.
    /// </summary>
    /// <value>
    /// The meters.
    /// </value>
    public T Meters
    {
        readonly get { return Value * Meter; }
        set { Value = value / Meter; }
    }

    /// <summary>
    /// Gets or sets the smoots.
    /// </summary>
    /// <value>
    /// The smoots.
    /// </value>
    public T Smoots
    {
        readonly get { return Value * Smoot; }
        set { Value = value / Smoot; }
    }

    /// <summary>
    /// Gets or sets the kilometers.
    /// </summary>
    /// <value>
    /// The kilometers.
    /// </value>
    public T Kilometers
    {
        readonly get { return Value * Kilometer; }
        set { Value = value / Kilometer; }
    }

    /// <summary>
    /// Gets or sets the miles.
    /// </summary>
    /// <value>
    /// The miles.
    /// </value>
    public T Miles
    {
        readonly get { return Value * Mile; }
        set { Value = value / Mile; }
    }

    /// <summary>
    /// Gets or sets the nautical miles.
    /// </summary>
    /// <value>
    /// The nautical miles.
    /// </value>
    public T NauticalMiles
    {
        readonly get { return Value * NauticalMile; }
        set { Value = value / NauticalMile; }
    }

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly string Name
        => nameof(Centimeters<T>);

    /// <summary>
    /// Gets the abbreviation.
    /// </summary>
    /// <value>
    /// The abbreviation.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly string Abbreviation
        => "cm";
    #endregion Properties

    #region Operators
    /// <summary>
    /// Performs an implicit conversion from <see cref="double"/> to <see cref="Centimeters"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static implicit operator Centimeters<T>(T value)
        => new(value);

    /// <summary>
    /// Performs an explicit conversion from <see cref="Mils"/> to <see cref="Centimeters"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static explicit operator Centimeters<T>(Mils<T> value)
        => value.Centimeters;

    /// <summary>
    /// Performs an explicit conversion from <see cref="Meters"/> to <see cref="Centimeters"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static explicit operator Centimeters<T>(Meters<T> value)
        => value.Centimeters;

    /// <summary>
    /// Performs an explicit conversion from <see cref="Smoots"/> to <see cref="Centimeters"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static explicit operator Centimeters<T>(Smoots<T> value)
        => value.Centimeters;

    /// <summary>
    /// Performs an explicit conversion from <see cref="Inches"/> to <see cref="Centimeters"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static explicit operator Centimeters<T>(Inches<T> value)
        => value.Centimeters;

    /// <summary>
    /// Performs an explicit conversion from <see cref="Feet"/> to <see cref="Centimeters"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static explicit operator Centimeters<T>(Feet<T> value)
        => value.Centimeters;

    /// <summary>
    /// Performs an explicit conversion from <see cref="Yards"/> to <see cref="Centimeters"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static explicit operator Centimeters<T>(Yards<T> value)
        => value.Centimeters;

    /// <summary>
    /// Performs an explicit conversion from <see cref="Kilometers"/> to <see cref="Centimeters"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static explicit operator Centimeters<T>(Kilometers<T> value)
        => value.Centimeters;

    /// <summary>
    /// Performs an explicit conversion from <see cref="Miles"/> to <see cref="Centimeters"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static explicit operator Centimeters<T>(Miles<T> value)
        => value.Centimeters;

    /// <summary>
    /// Performs an explicit conversion from <see cref="NauticalMiles"/> to <see cref="Centimeters"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static explicit operator Centimeters<T>(NauticalMiles<T> value)
        => value.Centimeters;

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(Centimeters<T> left, Centimeters<T> right) => left.Equals(right);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(Centimeters<T> left, Centimeters<T> right) => !(left == right);
    #endregion Operators

    #region Methods
    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override readonly bool Equals(object obj) => obj is Centimeters<T> centimeters && Equals(centimeters);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public readonly bool Equals([AllowNull] Centimeters<T> other) => Value == other.Value;

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override readonly int GetHashCode() => HashCode.Combine(Value);

    /// <summary>
    /// Creates a human-readable string that represents this <see cref="Centimeters" /> struct.
    /// </summary>
    /// <returns>
    /// A string representation of this <see cref="Centimeters" /> struct.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override string ToString()
        => ConvertToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

    /// <summary>
    /// Creates a string representation of this <see cref="Centimeters" /> struct based on the IFormatProvider
    /// passed in. If the provider is null, the CurrentCulture is used.
    /// </summary>
    /// <param name="provider">The provider to use to format the value.-or- A null reference (Nothing in Visual
    /// Basic) to obtain the numeric format information from the current locale setting
    /// of the operating system.system.</param>
    /// <returns>
    /// A string representation of this <see cref="Centimeters" /> struct.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public string ToString(IFormatProvider provider)
        => ConvertToString(string.Empty /* format string */, provider);

    /// <summary>
    /// Creates a string representation of this <see cref="Centimeters" /> struct based on the format string
    /// and IFormatProvider passed in. If the provider is null, the CurrentCulture is used.
    /// </summary>
    /// <param name="format">The format to use.-or- A null reference (Nothing in Visual Basic) to use the
    /// default format defined for the type of the System.IFormattable implementation.</param>
    /// <param name="provider">The provider to use to format the value.-or- A null reference (Nothing in Visual
    /// Basic) to obtain the numeric format information from the current locale setting
    /// of the operating system.system.</param>
    /// <returns>
    /// A string representation of this <see cref="Centimeters" /> struct.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public string ToString(string format, IFormatProvider provider)
        => ConvertToString(format /* format string */, provider /* format provider */);

    /// <summary>
    /// Creates a string representation of this <see cref="Centimeters" /> struct based on the format string
    /// and IFormatProvider passed in. If the provider is null, the CurrentCulture is used.
    /// </summary>
    /// <param name="format">The format to use.-or- A null reference (Nothing in Visual Basic) to use the
    /// default format defined for the type of the System.IFormattable implementation.</param>
    /// <param name="provider">The provider to use to format the value.-or- A null reference (Nothing in Visual
    /// Basic) to obtain the numeric format information from the current locale setting
    /// of the operating system.system.</param>
    /// <returns>
    /// A string representation of this <see cref="Centimeters" /> struct.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly string ConvertToString(string format, IFormatProvider provider)
        => $"{Value.ToString(format, provider)} {Abbreviation}";
    #endregion Methods
}
