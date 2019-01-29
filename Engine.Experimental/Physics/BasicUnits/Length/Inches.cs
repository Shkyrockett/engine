// <copyright file="Inches.cs" company="Shkyrockett" >
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
    /// The inches struct.
    /// </summary>
    public struct Inches
        : ILength, IFormattable
    {
        #region Constants
        /// <summary>
        /// The mil (const). Value: MilsInInch.
        /// </summary>
        public const double Mil = MilsInInch; // 1000d;

        /// <summary>
        /// The centimeter (const). Value: 2.54d.
        /// </summary>
        public const double Centimeter = 2.54d;

        /// <summary>
        /// The inch (const). Value: 1d.
        /// </summary>
        public const double Inch = 1d;

        /// <summary>
        /// The foot (const). Value: 1d / 12d.
        /// </summary>
        public const double Foot = 1d / 12d;

        /// <summary>
        /// The yard (const). Value: 1d / 36d.
        /// </summary>
        public const double Yard = 1d / 36d;

        /// <summary>
        /// The meter (const). Value: 1d / 39.3700787d.
        /// </summary>
        public const double Meter = 1d / 39.3700787d;

        /// <summary>
        /// The smoot (const). Value: Inch * 67d.
        /// </summary>
        public const double Smoot = Inch * 67d;

        /// <summary>
        /// The kilometer (const). Value: 1d / 39370.0787d.
        /// </summary>
        public const double Kilometer = 1d / 39370.0787d;

        /// <summary>
        /// The mile (const). Value: 1d / 63360d.
        /// </summary>
        public const double Mile = 1d / 63360d;

        /// <summary>
        /// The nautical mile (const). Value: 1d / 72913.3858d.
        /// </summary>
        public const double NauticalMile = 1d / 72913.3858d;
        #endregion Constants

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Inches"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Inches(double value)
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
            => nameof(Inches);

        /// <summary>
        /// Gets the abreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => "in";
        #endregion Properties

        #region Operators
        /// <param name="value"></param>
        public static implicit operator Inches(double value)
            => new Inches(value);

        /// <param name="value"></param>
        public static explicit operator Inches(Mils value)
            => value.Inches;

        /// <param name="value"></param>
        public static explicit operator Inches(Centimeters value)
            => value.Inches;

        /// <param name="value"></param>
        public static explicit operator Inches(Meters value)
            => value.Inches;

        /// <param name="value"></param>
        public static explicit operator Inches(Smoots value)
            => value.Inches;

        /// <param name="value"></param>
        public static explicit operator Inches(Feet value)
            => value.Inches;

        /// <param name="value"></param>
        public static explicit operator Inches(Yards value)
            => value.Inches;

        /// <param name="value"></param>
        public static explicit operator Inches(Kilometers value)
            => value.Inches;

        /// <param name="value"></param>
        public static explicit operator Inches(Miles value)
            => value.Inches;

        /// <param name="value"></param>
        public static explicit operator Inches(NauticalMiles value)
            => value.Inches;
        #endregion Operators

        #region Methods
        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Inches"/> struct.
        /// </summary>
        /// <returns>
        /// A string representation of this <see cref="Inches"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Inches"/> struct based on the IFormatProvider
        /// passed in. If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="provider">
        /// The provider to use to format the value.-or- A null reference (Nothing in Visual
        /// Basic) to obtain the numeric format information from the current locale setting
        /// of the operating system.system.
        /// </param>
        /// <returns>
        /// A string representation of this <see cref="Inches"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider)
            => ConvertToString(string.Empty /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Inches"/> struct based on the format string
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
        /// A string representation of this <see cref="Inches"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
            => ConvertToString(format /* format string */, provider /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Inches"/> struct based on the format string
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
        /// A string representation of this <see cref="Inches"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ConvertToString(string format, IFormatProvider provider)
            => $"{Value.ToString(format, provider)} {Abreviation}";
        #endregion Methods
    }
}
