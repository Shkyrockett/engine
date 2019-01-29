// <copyright file="Mils.cs" company="Shkyrockett" >
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
    /// The mils struct.
    /// </summary>
    public struct Mils
        : ILength, IFormattable
    {
        #region Constants
        /// <summary>
        /// The mil (const). Value: 1d.
        /// </summary>
        public const double Mil = 1d;

        /// <summary>
        /// The centimeter (const). Value: 1d / MilsInCentimeter.
        /// </summary>
        public const double Centimeter = 1d / MilsInCentimeter; // 1d / 393.700787d;

        /// <summary>
        /// The inch (const). Value: 1d / 1000d.
        /// </summary>
        public const double Inch = 1d / 1000d;

        /// <summary>
        /// The foot (const). Value: 1d / 12000d.
        /// </summary>
        public const double Foot = 1d / 12000d;

        /// <summary>
        /// The yard (const). Value: 1d / 36000d.
        /// </summary>
        public const double Yard = 1d / 36000d;

        /// <summary>
        /// The meter (const). Value: 1d / 39370.0787d.
        /// </summary>
        public const double Meter = 1d / 39370.0787d;

        /// <summary>
        /// The smoot (const). Value: Inch * 67d.
        /// </summary>
        public const double Smoot = Inch * 67d;

        /// <summary>
        /// The kilometer (const). Value: 1d / 3.9370E+7d.
        /// </summary>
        public const double Kilometer = 1d / 3.9370E+7d;

        /// <summary>
        /// The mile (const). Value: 1d / 63360000d.
        /// </summary>
        public const double Mile = 1d / 63360000d;

        /// <summary>
        /// The nautical mile (const). Value: 1d / 7.2913E+7d.
        /// </summary>
        public const double NauticalMile = 1d / 7.2913E+7d;
        #endregion Constants

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Mils"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Mils(double value)
        {
            Value = value;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the centimeters.
        /// </summary>
        public double Centimeters
        {
            get { return Value * Centimeter; }
            set { Value = value / Centimeter; }
        }

        /// <summary>
        /// Gets or sets the inches.
        /// </summary>
        public double Inches
        {
            get { return Value * Inch; }
            set { Value = value / Inch; }
        }

        /// <summary>
        /// Gets or sets the feet.
        /// </summary>
        public double Feet
        {
            get { return Value * Foot; }
            set { Value = value / Foot; }
        }

        /// <summary>
        /// Gets or sets the yards.
        /// </summary>
        public double Yards
        {
            get { return Value * Yard; }
            set { Value = value / Yard; }
        }

        /// <summary>
        /// Gets or sets the meters.
        /// </summary>
        public double Meters
        {
            get { return Value * Meter; }
            set { Value = value / Meter; }
        }

        /// <summary>
        /// Gets or sets the smoots.
        /// </summary>
        public double Smoots
        {
            get { return Value * Smoot; }
            set { Value = value / Smoot; }
        }

        /// <summary>
        /// Gets or sets the kilometers.
        /// </summary>
        public double Kilometers
        {
            get { return Value * Kilometer; }
            set { Value = value / Kilometer; }
        }

        /// <summary>
        /// Gets or sets the miles.
        /// </summary>
        public double Miles
        {
            get { return Value * Mile; }
            set { Value = value / Mile; }
        }

        /// <summary>
        /// Gets or sets the nautical miles.
        /// </summary>
        public double NauticalMiles
        {
            get { return Value * NauticalMile; }
            set { Value = value / NauticalMile; }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name
            => nameof(Mils);

        /// <summary>
        /// Gets the abreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => "mil";
        #endregion Properties

        #region Operators
        /// <param name="value"></param>
        public static implicit operator Mils(double value)
            => new Mils(value);

        /// <param name="value"></param>
        public static explicit operator Mils(Centimeters value)
            => value.Mils;

        /// <param name="value"></param>
        public static explicit operator Mils(Inches value)
            => value.Mils;

        /// <param name="value"></param>
        public static explicit operator Mils(Meters value)
            => value.Mils;

        /// <param name="value"></param>
        public static explicit operator Mils(Smoots value)
            => value.Mils;

        /// <param name="value"></param>
        public static explicit operator Mils(Feet value)
            => value.Mils;

        /// <param name="value"></param>
        public static explicit operator Mils(Yards value)
            => value.Mils;

        /// <param name="value"></param>
        public static explicit operator Mils(Miles value)
            => value.Mils;

        /// <param name="value"></param>
        public static explicit operator Mils(Kilometers value)
            => value.Mils;

        /// <param name="value"></param>
        public static explicit operator Mils(NauticalMiles value)
            => value.Mils;
        #endregion Operators

        #region Methods
        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Mils"/> struct.
        /// </summary>
        /// <returns>
        /// A string representation of this <see cref="Mils"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Mils"/> struct based on the IFormatProvider
        /// passed in. If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="provider">
        /// The provider to use to format the value.-or- A null reference (Nothing in Visual
        /// Basic) to obtain the numeric format information from the current locale setting
        /// of the operating system.system.
        /// </param>
        /// <returns>
        /// A string representation of this <see cref="Mils"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider)
            => ConvertToString(string.Empty /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Mils"/> struct based on the format string
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
        /// A string representation of this <see cref="Mils"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
            => ConvertToString(format /* format string */, provider /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Mils"/> struct based on the format string
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
        /// A string representation of this <see cref="Mils"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ConvertToString(string format, IFormatProvider provider)
            => $"{Value.ToString(format, provider)} {Abreviation}";
        #endregion Methods
    }
}
