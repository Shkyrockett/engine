// <copyright file="Meters.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using static Engine.LengthUnits;

namespace Engine
{
    /// <summary>
    /// The meters struct.
    /// </summary>
    /// <seealso cref="ILength" />
    /// <seealso cref="IFormattable" />
    /// <seealso cref="IEquatable{T}" />
    public struct Meters
        : ILength, IFormattable, IEquatable<Meters>
    {
        #region Constants
        /// <summary>
        /// The number of Mill in a Meter.
        /// </summary>
        public const double Mil = MilsInMeter; // 39370.0787d;

        /// <summary>
        /// The number of Centimeters in a Meter.
        /// </summary>
        public const double Centimeter = CentimetersInMeter; // 100d;

        /// <summary>
        /// The number of Inches in a Meter.
        /// </summary>
        public const double Inch = InchesInMeter; // 39.3700787d;

        /// <summary>
        /// The number of Feet in a Meter.
        /// </summary>
        public const double Foot = FeetInMeter; // 3.2808399d;

        /// <summary>
        /// The number of Yards in a Meter.
        /// </summary>
        public const double Yard = YardsInMeter; // 1.0936133d;

        /// <summary>
        /// The number of Meters in a Meter.
        /// </summary>
        public const double Meter = 1d;

        /// <summary>
        /// The number of Smoots in a Meter.
        /// </summary>
        public const double Smoot = 1d / MetersInSmoot; // Inch * 67d;

        /// <summary>
        /// The fraction of Kilometers in a Meter.
        /// </summary>
        public const double Kilometer = 1d / MetersInKilometer; // 1d / 1000d;

        /// <summary>
        /// The fraction of Miles in a Meter.
        /// </summary>
        public const double Mile = 1d / MetersInMile; // 1d / 1609.344d;

        /// <summary>
        /// The fraction of NauticalcMiles in a Meter.
        /// </summary>
        public const double NauticalMile = 1d / MetersInNauticalMile; // 1d / 1852d;
        #endregion Constants

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Meters" /> Struct.
        /// </summary>
        /// <param name="value">The distance in <see cref="Meters" />.</param>
        public Meters(double value)
        {
            Value = value;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// The numerical value of the distance in <see cref="Meters" />.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the mils.
        /// </summary>
        /// <value>
        /// The mils.
        /// </value>
        public double Mils
        {
            get { return Value * Mil; }
            set { Value = value / Mil; }
        }

        /// <summary>
        /// Gets or sets the centimeters.
        /// </summary>
        /// <value>
        /// The centimeters.
        /// </value>
        public double Centimeters
        {
            get { return Value * Centimeter; }
            set { Value = value / Centimeter; }
        }

        /// <summary>
        /// Gets or sets the inches.
        /// </summary>
        /// <value>
        /// The inches.
        /// </value>
        public double Inches
        {
            get { return Value * Inch; }
            set { Value = value / Inch; }
        }

        /// <summary>
        /// Gets or sets the feet.
        /// </summary>
        /// <value>
        /// The feet.
        /// </value>
        public double Feet
        {
            get { return Value * Foot; }
            set { Value = value / Foot; }
        }

        /// <summary>
        /// Gets or sets the yards.
        /// </summary>
        /// <value>
        /// The yards.
        /// </value>
        public double Yards
        {
            get { return Value * Yard; }
            set { Value = value / Yard; }
        }

        /// <summary>
        /// Gets or sets the smoots.
        /// </summary>
        /// <value>
        /// The smoots.
        /// </value>
        public double Smoots
        {
            get { return Value * Smoot; }
            set { Value = value / Smoot; }
        }

        /// <summary>
        /// Gets or sets the kilometers.
        /// </summary>
        /// <value>
        /// The kilometers.
        /// </value>
        public double Kilometers
        {
            get { return Value * Kilometer; }
            set { Value = value / Kilometer; }
        }

        /// <summary>
        /// Gets or sets the miles.
        /// </summary>
        /// <value>
        /// The miles.
        /// </value>
        public double Miles
        {
            get { return Value * Mile; }
            set { Value = value / Mile; }
        }

        /// <summary>
        /// Gets or sets the nautical miles.
        /// </summary>
        /// <value>
        /// The nautical miles.
        /// </value>
        public double NauticalMiles
        {
            get { return Value * NauticalMile; }
            set { Value = value / NauticalMile; }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name
            => nameof(Meters);

        /// <summary>
        /// Gets the abbreviation.
        /// </summary>
        /// <value>
        /// The abbreviation.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abbreviation
            => "m";
        #endregion Properties

        #region Operators
        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="Meters" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Meters operator +(Meters value)
            => +value.Value;

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="Meters" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Meters operator -(Meters value)
            => -value.Value;

        /// <summary>
        /// Add an amount to both values to the <see cref="Meters" /> struct.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Meters operator +(Meters value, double addend)
            => value.Value + addend;

        /// <summary>
        /// Add an amount to both values to the <see cref="Meters" /> struct.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Meters operator +(Meters value, ILength addend)
            => value.Value + ((Meters)addend).Value;

        /// <summary>
        /// Subtract a <see cref="Meters" /> from a <see cref="double" /> value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Meters operator -(Meters value, double subend)
            => value.Value - subend;

        /// <summary>
        /// Subtract a <see cref="Meters" /> from an <see cref="ILength" /> value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Meters operator -(Meters value, ILength subend)
            => value.Value - ((Meters)subend).Value;

        /// <summary>
        /// Multiply the <see cref="Meters" /> value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="factor">The factor.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Meters operator *(Meters value, double factor)
            => value.Value - factor;

        /// <summary>
        /// Multiply the <see cref="Meters" /> value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="factor">The factor.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Meters operator *(Meters value, ILength factor)
            => value.Value - ((Meters)factor).Value;

        /// <summary>
        /// Divide a <see cref="Meters" /> vlue by a <see cref="double" /> value.
        /// </summary>
        /// <param name="divisor">The divisor value.</param>
        /// <param name="dividend">The dividend value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Meters operator /(Meters divisor, double dividend)
            => divisor.Value / dividend;

        /// <summary>
        /// Divide a <see cref="Meters" /> vlue by a <see cref="double" /> value.
        /// </summary>
        /// <param name="divisor">The divisor value.</param>
        /// <param name="dividend">The dividend value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Meters operator /(Meters divisor, ILength dividend)
            => divisor.Value / ((Meters)dividend).Value;

        /// <summary>
        /// Compares two <see cref="Meters" /> objects.
        /// The result specifies whether the values of the X and Y
        /// values of the two <see cref="Meters" /> objects are equal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Meters left, ILength right)
            => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="Meters" /> objects.
        /// The result specifies whether the values of the two <see cref="Meters" /> structs are unequal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Meters left, ILength right)
            => !Equals(left, right);

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Meters left, Meters right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Meters left, Meters right) => !(left == right);

        /// <summary>
        /// Performs an implicit conversion from <see cref="double"/> to <see cref="Meters"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Meters(double value)
            => new Meters(value);

        /// <summary>
        /// Performs an explicit conversion from <see cref="Mils"/> to <see cref="Meters"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Meters(Mils value)
            => value.Meters;

        /// <summary>
        /// Performs an explicit conversion from <see cref="Centimeters"/> to <see cref="Meters"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Meters(Centimeters value)
            => value.Meters;

        /// <summary>
        /// Performs an explicit conversion from <see cref="Inches"/> to <see cref="Meters"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Meters(Inches value)
            => value.Meters;

        /// <summary>
        /// Performs an explicit conversion from <see cref="Feet"/> to <see cref="Meters"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Meters(Feet value)
            => value.Meters;

        /// <summary>
        /// Performs an explicit conversion from <see cref="Yards"/> to <see cref="Meters"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Meters(Yards value)
            => value.Meters;

        /// <summary>
        /// Performs an explicit conversion from <see cref="Smoots"/> to <see cref="Meters"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Meters(Smoots value)
            => value.Meters;

        /// <summary>
        /// Performs an explicit conversion from <see cref="Kilometers"/> to <see cref="Meters"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Meters(Kilometers value)
            => value.Meters;

        /// <summary>
        /// Performs an explicit conversion from <see cref="Miles"/> to <see cref="Meters"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Meters(Miles value)
            => value.Meters;

        /// <summary>
        /// Performs an explicit conversion from <see cref="NauticalMiles"/> to <see cref="Meters"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Meters(NauticalMiles value)
            => value.Meters;
        #endregion Operators

        #region Methods
        /// <summary>
        /// Compares two <see cref="Meters" /> objects.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Meters a, ILength b)
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
        public static bool Equals(Meters a, ILength b)
            => a.Value == ((Meters)b).Value;

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is ILength && Equals(this, (Meters)obj);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ILength value)
            => Equals(this, value);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(Meters other) => Value == other.Value;

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode() => -1937169414 + Value.GetHashCode();

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Meters" /> struct.
        /// </summary>
        /// <returns>
        /// A string representation of this <see cref="Meters" /> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Meters" /> struct based on the IFormatProvider
        /// passed in. If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="provider">The provider to use to format the value.-or- A null reference (Nothing in Visual
        /// Basic) to obtain the numeric format information from the current locale setting
        /// of the operating system.system.</param>
        /// <returns>
        /// A string representation of this <see cref="Meters" /> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider)
            => ConvertToString(string.Empty /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Meters" /> struct based on the format string
        /// and IFormatProvider passed in. If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="format">The format to use.-or- A null reference (Nothing in Visual Basic) to use the
        /// default format defined for the type of the System.IFormattable implementation.</param>
        /// <param name="provider">The provider to use to format the value.-or- A null reference (Nothing in Visual
        /// Basic) to obtain the numeric format information from the current locale setting
        /// of the operating system.system.</param>
        /// <returns>
        /// A string representation of this <see cref="Meters" /> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
            => ConvertToString(format /* format string */, provider /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Meters" /> struct based on the format string
        /// and IFormatProvider passed in. If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="format">The format to use.-or- A null reference (Nothing in Visual Basic) to use the
        /// default format defined for the type of the System.IFormattable implementation.</param>
        /// <param name="provider">The provider to use to format the value.-or- A null reference (Nothing in Visual
        /// Basic) to obtain the numeric format information from the current locale setting
        /// of the operating system.system.</param>
        /// <returns>
        /// A string representation of this <see cref="Meters" /> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ConvertToString(string format, IFormatProvider provider)
            => $"{Value.ToString(format, provider)} {Abbreviation}";

        /// <summary>
        /// Pluses the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static Meters Plus(Meters item) => +item;

        /// <summary>
        /// Negates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static Meters Negate(Meters item) => -item;

        /// <summary>
        /// Adds the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static Meters Add(Meters left, Meters right) => left + right;

        /// <summary>
        /// Subtracts the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static Meters Subtract(Meters left, Meters right) => left - right;

        /// <summary>
        /// Multiplies the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static Meters Multiply(Meters left, Meters right) => left * right;

        /// <summary>
        /// Divides the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static Meters Divide(Meters left, Meters right) => left / right;
        #endregion Methods
    }
}
