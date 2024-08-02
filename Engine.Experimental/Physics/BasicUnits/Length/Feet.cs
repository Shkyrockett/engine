// <copyright file="Feet.cs" company="Shkyrockett" >
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
/// The feet struct.
/// </summary>
/// <seealso cref="ILength{T}" />
/// <seealso cref="IFormattable" />
/// <seealso cref="IEquatable{T}" />
public struct Feet<T>
    : ILength<T>, IFormattable, IEquatable<Feet<T>>
    where T : INumber<T>
{
    #region Constants
    /// <summary>
    /// The mil (static readonly). Value: MilsInFoot.
    /// </summary>
    public static readonly T Mil = LengthUnits<T>.MilsInFoot; // 12000d;

    /// <summary>
    /// The centimeter (static readonly). Value: 30.48d.
    /// </summary>
    public static readonly T Centimeter = T.CreateSaturating(30.48);//LengthUnits<T>.FeetInCentimeter

    /// <summary>
    /// The inch (static readonly). Value: 12d.
    /// </summary>
    public static readonly T Inch = LengthUnits<T>.InchesInFoot;

    /// <summary>
    /// The foot (static readonly). Value:T.One.
    /// </summary>
    public static readonly T Foot = T.One;

    /// <summary>
    /// The yard (static readonly). Value:T.One / 3d.
    /// </summary>
    public static readonly T Yard = T.One / LengthUnits<T>.FeetInYard;

    /// <summary>
    /// The meter (static readonly). Value:T.One / 3.2808399d.
    /// </summary>
    public static readonly T Meter = T.One / T.CreateSaturating(3.2808399);

    /// <summary>
    /// The smoot (static readonly). Value: Inch * 67d.
    /// </summary>
    public static readonly T Smoot = Inch * T.CreateSaturating(67);

    /// <summary>
    /// The kilometer (static readonly). Value:T.One / 3280.8399d.
    /// </summary>
    public static readonly T Kilometer = T.One / T.CreateSaturating(3280.8399);

    /// <summary>
    /// The mile (static readonly). Value:T.One / 5280d.
    /// </summary>
    public static readonly T Mile = T.One / T.CreateSaturating(5280);

    /// <summary>
    /// The nautical mile (static readonly). Value:T.One / 6076.11549d.
    /// </summary>
    public static readonly T NauticalMile = T.One / T.CreateSaturating(6076.11549);
    #endregion Constants

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Feet" /> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public Feet(T value)
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
    /// Gets or sets the centimeters.
    /// </summary>
    /// <value>
    /// The centimeters.
    /// </value>
    public T Centimeters
    {
        readonly get { return Value * Centimeter; }
        set { Value = value / Centimeter; }
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
        => nameof(Feet<T>);

    /// <summary>
    /// Gets the abbreviation.
    /// </summary>
    /// <value>
    /// The abbreviation.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly string Abbreviation
        => "ft";
    #endregion Properties

    #region Operators
    /// <summary>
    /// Performs an implicit conversion from <see cref="T"/> to <see cref="Feet"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static implicit operator Feet<T>(T value)
        => new(value);

    /// <summary>
    /// Performs an explicit conversion from <see cref="Mils"/> to <see cref="Feet"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static explicit operator Feet<T>(Mils<T> value)
        => value.Feet;

    /// <summary>
    /// Performs an explicit conversion from <see cref="Centimeters"/> to <see cref="Feet"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static explicit operator Feet<T>(Centimeters<T> value)
        => value.Feet;

    /// <summary>
    /// Performs an explicit conversion from <see cref="Meters"/> to <see cref="Feet"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static explicit operator Feet<T>(Meters<T> value)
        => value.Feet;

    /// <summary>
    /// Performs an explicit conversion from <see cref="Smoots"/> to <see cref="Feet"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static explicit operator Feet<T>(Smoots<T> value)
        => value.Feet;

    /// <summary>
    /// Performs an explicit conversion from <see cref="Inches"/> to <see cref="Feet"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static explicit operator Feet<T>(Inches<T> value)
        => value.Feet;

    /// <summary>
    /// Performs an explicit conversion from <see cref="Yards"/> to <see cref="Feet"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static explicit operator Feet<T>(Yards<T> value)
        => value.Feet;

    /// <summary>
    /// Performs an explicit conversion from <see cref="Kilometers"/> to <see cref="Feet"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static explicit operator Feet<T>(Kilometers<T> value)
        => value.Feet;

    /// <summary>
    /// Performs an explicit conversion from <see cref="Miles"/> to <see cref="Feet"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static explicit operator Feet<T>(Miles<T> value)
        => value.Feet;

    /// <summary>
    /// Performs an explicit conversion from <see cref="NauticalMiles"/> to <see cref="Feet"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static explicit operator Feet<T>(NauticalMiles<T> value)
        => value.Feet;

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(Feet<T> left, Feet<T> right) => left.Equals(right);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(Feet<T> left, Feet<T> right) => !(left == right);
    #endregion Operators

    #region Methods
    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override readonly bool Equals(object obj) => obj is Feet<T> feet && Equals(feet);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public readonly bool Equals([AllowNull] Feet<T> other) => Value == other.Value;

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override readonly int GetHashCode() => HashCode.Combine(Value);

    /// <summary>
    /// Creates a human-readable string that represents this <see cref="Feet" /> struct.
    /// </summary>
    /// <returns>
    /// A string representation of this <see cref="Feet" /> struct.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override string ToString()
        => ConvertToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

    /// <summary>
    /// Creates a string representation of this <see cref="Feet" /> struct based on the IFormatProvider
    /// passed in. If the provider is null, the CurrentCulture is used.
    /// </summary>
    /// <param name="provider">The provider to use to format the value.-or- A null reference (Nothing in Visual
    /// Basic) to obtain the numeric format information from the current locale setting
    /// of the operating system.system.</param>
    /// <returns>
    /// A string representation of this <see cref="Feet" /> struct.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public string ToString(IFormatProvider provider)
        => ConvertToString(string.Empty /* format string */, provider);

    /// <summary>
    /// Creates a string representation of this <see cref="Feet" /> struct based on the format string
    /// and IFormatProvider passed in. If the provider is null, the CurrentCulture is used.
    /// </summary>
    /// <param name="format">The format to use.-or- A null reference (Nothing in Visual Basic) to use the
    /// default format defined for the type of the System.IFormattable implementation.</param>
    /// <param name="provider">The provider to use to format the value.-or- A null reference (Nothing in Visual
    /// Basic) to obtain the numeric format information from the current locale setting
    /// of the operating system.system.</param>
    /// <returns>
    /// A string representation of this <see cref="Feet" /> struct.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public string ToString(string? format, IFormatProvider? provider)
        => ConvertToString(format /* format string */, provider /* format provider */);

    /// <summary>
    /// Creates a string representation of this <see cref="Feet" /> struct based on the format string
    /// and IFormatProvider passed in. If the provider is null, the CurrentCulture is used.
    /// </summary>
    /// <param name="format">The format to use.-or- A null reference (Nothing in Visual Basic) to use the
    /// default format defined for the type of the System.IFormattable implementation.</param>
    /// <param name="provider">The provider to use to format the value.-or- A null reference (Nothing in Visual
    /// Basic) to obtain the numeric format information from the current locale setting
    /// of the operating system.system.</param>
    /// <returns>
    /// A string representation of this <see cref="Feet" /> struct.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly string ConvertToString(string format, IFormatProvider provider)
        => $"{Value.ToString(format, provider)} {Abbreviation}";
    #endregion Methods
}
