// <copyright file="Centimeters.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.CompilerServices;
using static Engine.Physics.LengthUnits;

namespace Engine.Physics
{
    /// <summary>
    ///
    /// </summary>
    public struct Centimeters
        : ILength, IFormattable
    {
        #region Constants

        /// <summary>
        ///
        /// </summary>
        public const double Mil = MilsInCentimeter; // 393.700787d;

        /// <summary>
        ///
        /// </summary>
        public const double Centimeter = 1d;

        /// <summary>
        ///
        /// </summary>
        public const double Inch = 1d / 2.54d;

        /// <summary>
        ///
        /// </summary>
        public const double Foot = 1d / 30.48d;

        /// <summary>
        ///
        /// </summary>
        public const double Yard = 1d / 91.44d;

        /// <summary>
        ///
        /// </summary>
        public const double Meter = 1d / 100d;

        /// <summary>
        ///
        /// </summary>
        public const double Smoot = Inch * 67d;

        /// <summary>
        ///
        /// </summary>
        public const double Kilometer = 1d / 100000d;

        /// <summary>
        ///
        /// </summary>
        public const double Mile = 1d / 160934.4d;

        /// <summary>
        ///
        /// </summary>
        public const double NauticalMile = 1d / 185200d;

        #endregion

        #region Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public Centimeters(double value)
        {
            Value = value;
        }

        #endregion

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
            => "Centimeters";

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => "cm";

        #endregion

        #region Operators

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Centimeters(double value)
            => new Centimeters(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Centimeters(Mils value)
            => value.Centimeters;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Centimeters(Meters value)
            => value.Centimeters;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Centimeters(Smoots value)
            => value.Centimeters;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Centimeters(Inches value)
            => value.Centimeters;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Centimeters(Feet value)
            => value.Centimeters;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Centimeters(Yards value)
            => value.Centimeters;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Centimeters(Kilometers value)
            => value.Centimeters;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Centimeters(Miles value)
            => value.Centimeters;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Centimeters(NauticalMiles value)
            => value.Centimeters;

        #endregion

        #region Methods

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Centimeters"/> struct.
        /// </summary>
        /// <returns>
        /// A string representation of this <see cref="Centimeters"/> struct.
        /// </returns>        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Centimeters"/> struct based on the IFormatProvider
        /// passed in. If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="provider">
        /// The provider to use to format the value.-or- A null reference (Nothing in Visual
        /// Basic) to obtain the numeric format information from the current locale setting
        /// of the operating system.system.
        /// </param>
        /// <returns>
        /// A string representation of this <see cref="Centimeters"/> struct.
        /// </returns>        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Centimeters"/> struct based on the format string
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
        /// A string representation of this <see cref="Centimeters"/> struct.
        /// </returns>        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        string IFormattable.ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Centimeters"/> struct based on the format string
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
        /// A string representation of this <see cref="Centimeters"/> struct.
        /// </returns>        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ConvertToString(string format, IFormatProvider provider)
            => $"{Value.ToString(format, provider)} {Abreviation}";

        #endregion
    }
}
