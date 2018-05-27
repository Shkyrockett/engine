// <copyright file="Feet.cs" company="Shkyrockett" >
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
    /// The feet struct.
    /// </summary>
    public struct Feet
        : ILength, IFormattable
    {
        #region Constants
        /// <summary>
        ///
        /// </summary>
        public const double Mil = MilsInFoot; // 12000d;

        /// <summary>
        ///
        /// </summary>
        public const double Centimeter = 30.48d;

        /// <summary>
        ///
        /// </summary>
        public const double Inch = 12d;

        /// <summary>
        ///
        /// </summary>
        public const double Foot = 1d;

        /// <summary>
        ///
        /// </summary>
        public const double Yard = 1d / 3d;

        /// <summary>
        ///
        /// </summary>
        public const double Meter = 1d / 3.2808399d;

        /// <summary>
        ///
        /// </summary>
        public const double Smoot = Inch * 67d;

        /// <summary>
        ///
        /// </summary>
        public const double Kilometer = 1d / 3280.8399d;

        /// <summary>
        ///
        /// </summary>
        public const double Mile = 1d / 5280d;

        /// <summary>
        ///
        /// </summary>
        public const double NauticalMile = 1d / 6076.11549d;
        #endregion Constants

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Feet"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Feet(double value)
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
            => nameof(Feet);

        /// <summary>
        /// Gets the abreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => "ft";
        #endregion Properties

        #region Operators
        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Feet(double value)
            => new Feet(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Feet(Mils value)
            => value.Feet;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Feet(Centimeters value)
            => value.Feet;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Feet(Meters value)
            => value.Feet;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Feet(Smoots value)
            => value.Feet;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Feet(Inches value)
            => value.Feet;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Feet(Yards value)
            => value.Feet;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Feet(Kilometers value)
            => value.Feet;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Feet(Miles value)
            => value.Feet;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Feet(NauticalMiles value)
            => value.Feet;
        #endregion Operators

        #region Methods
        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Feet"/> struct.
        /// </summary>
        /// <returns>
        /// A string representation of this <see cref="Feet"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Feet"/> struct based on the IFormatProvider
        /// passed in. If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="provider">
        /// The provider to use to format the value.-or- A null reference (Nothing in Visual
        /// Basic) to obtain the numeric format information from the current locale setting
        /// of the operating system.system.
        /// </param>
        /// <returns>
        /// A string representation of this <see cref="Feet"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Feet"/> struct based on the format string
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
        /// A string representation of this <see cref="Feet"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Feet"/> struct based on the format string
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
        /// A string representation of this <see cref="Feet"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ConvertToString(string format, IFormatProvider provider)
            => $"{Value.ToString(format, provider)} {Abreviation}";
        #endregion Methods
    }
}
