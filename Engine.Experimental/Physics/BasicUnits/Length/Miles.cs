// <copyright file="Miles.cs" company="Shkyrockett" >
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
    /// The miles struct.
    /// </summary>
    public struct Miles
        : ILength, IFormattable
    {
        #region Constants
        /// <summary>
        ///
        /// </summary>
        public const double Mil = MilsInMile; // 63360000d;

        /// <summary>
        ///
        /// </summary>
        public const double Centimeter = 160934.4d;

        /// <summary>
        ///
        /// </summary>
        public const double Inch = 63360d;

        /// <summary>
        ///
        /// </summary>
        public const double Foot = 5280d;

        /// <summary>
        ///
        /// </summary>
        public const double Yard = 1760d;

        /// <summary>
        ///
        /// </summary>
        public const double Meter = 1609.344d;

        /// <summary>
        ///
        /// </summary>
        public const double Smoot = Inch * 67d;

        /// <summary>
        ///
        /// </summary>
        public const double Kilometer = 1.609344d;

        /// <summary>
        ///
        /// </summary>
        public const double Mile = 1d;

        /// <summary>
        ///
        /// </summary>
        public const double NauticalMile = 1d / 1.15077945d;
        #endregion Constants

        #region Constructors
        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public Miles(double value)
        {
            Value = value;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        ///
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
        public double Meters
        {
            get { return Value * Meter; }
            set { Value = value / Meter; }
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
            => nameof(Miles);

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => "mile";
        #endregion Properties

        #region Operators
        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Miles(double value)
            => new Miles(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Miles(Mils value)
            => value.Miles;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Miles(Centimeters value)
            => value.Miles;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Miles(Inches value)
            => value.Miles;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Miles(Meters value)
            => value.Miles;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Miles(Smoots value)
            => value.Miles;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Miles(Feet value)
            => value.Miles;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Miles(Yards value)
            => value.Miles;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Miles(Kilometers value)
            => value.Miles;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Miles(NauticalMiles value)
            => value.Miles;
        #endregion Operators

        #region Methods
        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Miles"/> struct.
        /// </summary>
        /// <returns>
        /// A string representation of this <see cref="Miles"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Miles"/> struct based on the IFormatProvider
        /// passed in. If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="provider">
        /// The provider to use to format the value.-or- A null reference (Nothing in Visual
        /// Basic) to obtain the numeric format information from the current locale setting
        /// of the operating system.system.
        /// </param>
        /// <returns>
        /// A string representation of this <see cref="Miles"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Miles"/> struct based on the format string
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
        /// A string representation of this <see cref="Miles"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Miles"/> struct based on the format string
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
        /// A string representation of this <see cref="Miles"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ConvertToString(string format, IFormatProvider provider)
            => $"{Value.ToString(format, provider)} {Abreviation}";
        #endregion Methods
    }
}
