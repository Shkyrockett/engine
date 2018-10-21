// <copyright file="NauticalMiles.cs" company="Shkyrockett" >
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
    /// The nautical miles struct.
    /// </summary>
    public struct NauticalMiles
        : ILength, IFormattable
    {
        #region Constants
        /// <summary>
        /// The mil (const). Value: MilsInNauticalMile.
        /// </summary>
        public const double Mil = MilsInNauticalMile; // 7.2913E+7d;

        /// <summary>
        /// The centimeter (const). Value: 185200d.
        /// </summary>
        public const double Centimeter = 185200d;

        /// <summary>
        /// The inch (const). Value: 72913.3858d.
        /// </summary>
        public const double Inch = 72913.3858d;

        /// <summary>
        /// The foot (const). Value: 6076.11549d.
        /// </summary>
        public const double Foot = 6076.11549d;

        /// <summary>
        /// The yard (const). Value: 2025.37183d.
        /// </summary>
        public const double Yard = 2025.37183d;

        /// <summary>
        /// The meter (const). Value: 1852d.
        /// </summary>
        public const double Meter = 1852d;

        /// <summary>
        /// The smoot (const). Value: Inch * 67d.
        /// </summary>
        public const double Smoot = Inch * 67d;

        /// <summary>
        /// The kilometer (const). Value: 1.852d.
        /// </summary>
        public const double Kilometer = 1.852d;

        /// <summary>
        /// The mile (const). Value: 1.15077945d.
        /// </summary>
        public const double Mile = 1.15077945d;

        /// <summary>
        /// The nautical mile (const). Value: 1d.
        /// </summary>
        public const double NauticalMile = 1d;
        #endregion Constants

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NauticalMiles"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public NauticalMiles(double value)
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
        /// Gets or sets the mils.
        /// </summary>
        public double Mils
        {
            get { return Value * Mil; }
            set { Value = value / Mil; }
        }

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
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name
            => "Nautical Miles";

        /// <summary>
        /// Gets the abreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => "Nm";
        #endregion Properties

        #region Operators
        /// <param name="value"></param>
        public static implicit operator NauticalMiles(double value)
            => new NauticalMiles(value);

        /// <param name="value"></param>
        public static explicit operator NauticalMiles(Mils value)
            => value.NauticalMiles;

        /// <param name="value"></param>
        public static explicit operator NauticalMiles(Centimeters value)
            => value.NauticalMiles;

        /// <param name="value"></param>
        public static explicit operator NauticalMiles(Inches value)
            => value.NauticalMiles;

        /// <param name="value"></param>
        public static explicit operator NauticalMiles(Meters value)
            => value.NauticalMiles;

        /// <param name="value"></param>
        public static explicit operator NauticalMiles(Smoots value)
            => value.NauticalMiles;

        /// <param name="value"></param>
        public static explicit operator NauticalMiles(Feet value)
            => value.NauticalMiles;

        /// <param name="value"></param>
        public static explicit operator NauticalMiles(Yards value)
            => value.NauticalMiles;

        /// <param name="value"></param>
        public static explicit operator NauticalMiles(Kilometers value)
            => value.NauticalMiles;

        /// <param name="value"></param>
        public static explicit operator NauticalMiles (Miles value)
            => value.NauticalMiles;
        #endregion Operators

        #region Methods
        /// <summary>
        /// Creates a human-readable string that represents this <see cref="NauticalMiles"/> struct.
        /// </summary>
        /// <returns>
        /// A string representation of this <see cref="NauticalMiles"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="NauticalMiles"/> struct based on the IFormatProvider
        /// passed in. If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="provider">
        /// The provider to use to format the value.-or- A null reference (Nothing in Visual
        /// Basic) to obtain the numeric format information from the current locale setting
        /// of the operating system.system.
        /// </param>
        /// <returns>
        /// A string representation of this <see cref="NauticalMiles"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="NauticalMiles"/> struct based on the format string
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
        /// A string representation of this <see cref="NauticalMiles"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="NauticalMiles"/> struct based on the format string
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
        /// A string representation of this <see cref="NauticalMiles"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ConvertToString(string format, IFormatProvider provider)
            => $"{Value.ToString(format, provider)} {Abreviation}";
        #endregion Methods
    }
}
