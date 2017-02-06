// <copyright file="Yards.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
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
    public struct Yards
        : ILength, IFormattable
    {
        #region Constants

        /// <summary>
        ///
        /// </summary>
        public const double Mil = MilsInYard; // 36000d;

        /// <summary>
        ///
        /// </summary>
        public const double Centimeter = 91.44d;

        /// <summary>
        ///
        /// </summary>
        public const double Inch = 36d;

        /// <summary>
        ///
        /// </summary>
        public const double Foot = 3d;

        /// <summary>
        ///
        /// </summary>
        public const double Yard = 1d;

        /// <summary>
        ///
        /// </summary>
        public const double Meter = 1d / 1.0936133d;

        /// <summary>
        ///
        /// </summary>
        public const double Smoot = Inch * 67d;

        /// <summary>
        ///
        /// </summary>
        public const double Kilometer = 1d / 1093.6133d;

        /// <summary>
        ///
        /// </summary>
        public const double Mile = 1d / 1760d;

        /// <summary>
        ///
        /// </summary>
        public const double NauticalMile = 1d / 2025.37183d;

        #endregion

        #region Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public Yards(double value)
            => Value = value;

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
            => "Yards";

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => "yd";

        #endregion

        #region Operators

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Yards(double value)
            => new Yards(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Yards(Mils value)
            => value.Yards;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Yards(Centimeters value)
            => value.Yards;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Yards(Meters value)
            => value.Yards;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Yards(Smoots value)
            => value.Yards;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Yards(Inches value)
            => value.Yards;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Yards (Feet value)
            => value.Yards;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Yards(Kilometers value)
            => value.Feet;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Yards(Miles value)
            => value.Yards;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Yards(NauticalMiles value)
            => value.Yards;

        #endregion

        #region Methods

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Yards"/> struct.
        /// </summary>
        /// <returns>
        /// A string representation of this <see cref="Yards"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Yards"/> struct based on the IFormatProvider
        /// passed in. If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="provider">
        /// The provider to use to format the value.-or- A null reference (Nothing in Visual
        /// Basic) to obtain the numeric format information from the current locale setting
        /// of the operating system.system.
        /// </param>
        /// <returns>
        /// A string representation of this <see cref="Yards"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Yards"/> struct based on the format string
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
        /// A string representation of this <see cref="Yards"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        string IFormattable.ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Yards"/> struct based on the format string
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
        /// A string representation of this <see cref="Yards"/> struct.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ConvertToString(string format, IFormatProvider provider)
            => $"{Value.ToString(format, provider)} {Abreviation}";

        #endregion
    }
}
