// <copyright file="Kilometers.cs" company="Shkyrockett" >
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
    /// The kilometers struct.
    /// </summary>
    public struct Kilometers
        : ILength, IFormattable
    {
        #region Constants
        /// <summary>
        ///
        /// </summary>
        public const double Mil = MilsInKilometer; //  3.9370E+7d;

        /// <summary>
        ///
        /// </summary>
        public const double Centimeter = 100000d;

        /// <summary>
        ///
        /// </summary>
        public const double Inch = 39370.0787d;

        /// <summary>
        ///
        /// </summary>
        public const double Foot = 3280.8399d;

        /// <summary>
        ///
        /// </summary>
        public const double Yard = 1093.6133d;

        /// <summary>
        ///
        /// </summary>
        public const double Meter = 1000d;

        /// <summary>
        ///
        /// </summary>
        public const double Smoot = Inch * 67d;

        /// <summary>
        ///
        /// </summary>
        public const double Kilometer = 1d;

        /// <summary>
        ///
        /// </summary>
        public const double Mile = 1d / 1.609344d;

        /// <summary>
        ///
        /// </summary>
        public const double NauticalMile = 1d / 1.852d;
        #endregion Constants

        #region Constructors
        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public Kilometers(double value)
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
        public double Inches
        {
            get { return Value * Inch; }
            set { Value = value / Inch; }
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
            => nameof(Kilometers);

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => "km";
        #endregion Properties

        #region Operators
        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Kilometers(double value)
            => new Kilometers(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Kilometers(Mils value)
            => value.Kilometers;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Kilometers(Centimeters value)
            => value.Kilometers;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Kilometers(Inches value)
            => value.Kilometers;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Kilometers(Meters value)
            => value.Kilometers;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Kilometers(Smoots value)
            => value.Kilometers;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Kilometers(Feet value)
            => value.Kilometers;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Kilometers(Yards value)
            => value.Kilometers;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Kilometers(Miles value)
            => value.Kilometers;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Kilometers(NauticalMiles value)
            => value.Kilometers;
        #endregion Operators

        #region Methods
        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Kilometers"/> struct.
        /// </summary>
        /// <returns>
        /// A string representation of this <see cref="Kilometers"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Kilometers"/> struct based on the IFormatProvider
        /// passed in. If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="provider">
        /// The provider to use to format the value.-or- A null reference (Nothing in Visual
        /// Basic) to obtain the numeric format information from the current locale setting
        /// of the operating system.system.
        /// </param>
        /// <returns>
        /// A string representation of this <see cref="Kilometers"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Kilometers"/> struct based on the format string
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
        /// A string representation of this <see cref="Kilometers"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Kilometers"/> struct based on the format string
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
        /// A string representation of this <see cref="Kilometers"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ConvertToString(string format, IFormatProvider provider)
            => $"{Value.ToString(format, provider)} {Abreviation}";
        #endregion Methods
    }
}
