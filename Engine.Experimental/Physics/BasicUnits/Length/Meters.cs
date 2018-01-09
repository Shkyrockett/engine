// <copyright file="Meters.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
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
using static Engine.Physics.LengthUnits;

namespace Engine.Physics
{
    /// <summary>
    ///
    /// </summary>
    public struct Meters
        : ILength, IFormattable
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

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Meters"/> Struct.
        /// </summary>
        /// <param name="value">The distance in <see cref="Meters"/>.</param>
        public Meters(double value)
        {
            Value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The numarical value of the distance in <see cref="Meters"/>.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        ///
        /// </summary>
        public double Mils
        {
            get { return Value * Mil; }
            set { Value = value / Mil; }
        }

        /// <summary>
        ///
        /// </summary>
        public double Centimeters
        {
            get { return Value * Centimeter; }
            set { Value = value / Centimeter; }
        }

        /// <summary>
        ///
        /// </summary>
        public double Inches
        {
            get { return Value * Inch; }
            set { Value = value / Inch; }
        }

        /// <summary>
        ///
        /// </summary>
        public double Feet
        {
            get { return Value * Foot; }
            set { Value = value / Foot; }
        }

        /// <summary>
        ///
        /// </summary>
        public double Yards
        {
            get { return Value * Yard; }
            set { Value = value / Yard; }
        }

        /// <summary>
        ///
        /// </summary>
        public double Smoots
        {
            get { return Value * Smoot; }
            set { Value = value / Smoot; }
        }

        /// <summary>
        ///
        /// </summary>
        public double Kilometers
        {
            get { return Value * Kilometer; }
            set { Value = value / Kilometer; }
        }

        /// <summary>
        ///
        /// </summary>
        public double Miles
        {
            get { return Value * Mile; }
            set { Value = value / Mile; }
        }

        /// <summary>
        ///
        /// </summary>
        public double NauticalMiles
        {
            get { return Value * NauticalMile; }
            set { Value = value / NauticalMile; }
        }

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name
            => nameof(Meters);

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => "m";

        #endregion

        #region Operators

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Meters operator +(Meters value)
            => +value.Value;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Meters operator -(Meters value)
            => -value.Value;

        /// <summary>
        /// Add an amount to both values to the <see cref="Meters"/> struct.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Meters operator +(Meters value, double addend)
            => value.Value + addend;

        /// <summary>
        /// Add an amount to both values to the <see cref="Meters"/> struct.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Meters operator +(Meters value, ILength addend)
            => value.Value + ((Meters)addend).Value;

        /// <summary>
        /// Subtract a <see cref="Meters"/> from a <see cref="double"/> value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Meters operator -(Meters value, double subend)
            => value.Value - subend;

        /// <summary>
        /// Subtract a <see cref="Meters"/> from an <see cref="ILength"/> value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Meters operator -(Meters value, ILength subend)
            => value.Value - ((Meters)subend).Value;

        /// <summary>
        /// Multiply the <see cref="Meters"/> value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Meters operator *(Meters value, double factor)
            => value.Value - factor;

        /// <summary>
        /// Multiply the <see cref="Meters"/> value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Meters operator *(Meters value, ILength factor)
            => value.Value - ((Meters)factor).Value;

        /// <summary>
        /// Divide a <see cref="Meters"/> vlue by a <see cref="double"/> value.
        /// </summary>
        /// <param name="divisor">The divisor value.</param>
        /// <param name="dividend">The dividend value.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Meters operator /(Meters divisor, double dividend)
            => divisor.Value / dividend;

        /// <summary>
        /// Divide a <see cref="Meters"/> vlue by a <see cref="double"/> value.
        /// </summary>
        /// <param name="divisor">The divisor value.</param>
        /// <param name="dividend">The dividend value.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Meters operator /(Meters divisor, ILength dividend)
            => divisor.Value / ((Meters)dividend).Value;

        /// <summary>
        /// Compares two <see cref="Meters"/> objects.
        /// The result specifies whether the values of the X and Y
        /// values of the two <see cref="Meters"/> objects are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Meters left, ILength right)
            => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="Meters"/> objects.
        /// The result specifies whether the values of the two <see cref="Meters"/> structs are unequal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Meters left, ILength right)
            => !Equals(left, right);

        /// <summary>
        /// Compares two <see cref="Meters"/> objects.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Meters a, ILength b)
            => Equals(a, b);

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Meters a, ILength b)
            => a.Value == ((Meters)b).Value;

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => (obj is ILength && Equals(this, (Meters)obj));

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ILength value)
            => Equals(this, value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Meters(double value)
            => new Meters(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Meters(Mils value)
            => value.Meters;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Meters(Centimeters value)
            => value.Meters;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Meters(Inches value)
            => value.Meters;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Meters(Feet value)
            => value.Meters;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Meters(Yards value)
            => value.Meters;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Meters(Smoots value)
            => value.Meters;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Meters(Kilometers value)
            => value.Meters;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Meters(Miles value)
            => value.Meters;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Meters(NauticalMiles value)
            => value.Meters;

        #endregion

        #region Methods

        /// <summary>
        /// Returns the hash code for this instance of the <see cref="Meters"/> value.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
            => Value.GetHashCode();

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Meters"/> struct.
        /// </summary>
        /// <returns>
        /// A string representation of this <see cref="Meters"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Meters"/> struct based on the IFormatProvider
        /// passed in. If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="provider">
        /// The provider to use to format the value.-or- A null reference (Nothing in Visual
        /// Basic) to obtain the numeric format information from the current locale setting
        /// of the operating system.system.
        /// </param>
        /// <returns>
        /// A string representation of this <see cref="Meters"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Meters"/> struct based on the format string
        /// and IFormatProvider passed in. If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="format">
        /// The format to use.-or- A null reference (Nothing in Visual Basic) to use the
        /// default format defined for the type of the System.IFormattable implementation.
        /// </param>
        /// <param name="provider">
        /// The provider to use to format the value.-or- A null reference (Nothing in Visual
        /// Basic) to obtain the numeric format information from the current locale setting
        /// of the operating system.system.
        /// </param>
        /// <returns>
        /// A string representation of this <see cref="Meters"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        string IFormattable.ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Meters"/> struct based on the format string
        /// and IFormatProvider passed in. If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="format">
        /// The format to use.-or- A null reference (Nothing in Visual Basic) to use the
        /// default format defined for the type of the System.IFormattable implementation.
        /// </param>
        /// <param name="provider">
        /// The provider to use to format the value.-or- A null reference (Nothing in Visual
        /// Basic) to obtain the numeric format information from the current locale setting
        /// of the operating system.system.
        /// </param>
        /// <returns>
        /// A string representation of this <see cref="Meters"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ConvertToString(string format, IFormatProvider provider)
            => $"{Value.ToString(format, provider)} {Abreviation}";

        #endregion
    }
}
