// <copyright file="NauticalMiles.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    ///
    /// </summary>
    public struct NauticalMiles
        : ILength
    {
        #region Constants

        /// <summary>
        ///
        /// </summary>
        public const double Mil = 7.2913E+7d;

        /// <summary>
        ///
        /// </summary>
        public const double Centimeter = 185200d;

        /// <summary>
        ///
        /// </summary>
        public const double Inch = 72913.3858d;

        /// <summary>
        ///
        /// </summary>
        public const double Foot = 6076.11549d;

        /// <summary>
        ///
        /// </summary>
        public const double Yard = 2025.37183d;

        /// <summary>
        ///
        /// </summary>
        public const double Meter = 1852d;

        /// <summary>
        ///
        /// </summary>
        public const double Smoot = Inch * 67d;

        /// <summary>
        ///
        /// </summary>
        public const double Kilometer = 1.852d;

        /// <summary>
        ///
        /// </summary>
        public const double Mile = 1.15077945d;

        /// <summary>
        ///
        /// </summary>
        public const double NauticalMile = 1d;

        #endregion

        #region Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public NauticalMiles(double value)
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
        public double Miles
        {
            get { return Value * Mile; }
            set { Value = value / Mile; }
        }

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name
            => "Nautical Miles";

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => "Nm";

        #endregion

        #region Operators

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator NauticalMiles(double value)
            => new NauticalMiles(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator NauticalMiles(Mils value)
            => value.NauticalMiles;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator NauticalMiles(Centimeters value)
            => value.NauticalMiles;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator NauticalMiles(Inches value)
            => value.NauticalMiles;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator NauticalMiles(Meters value)
            => value.NauticalMiles;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator NauticalMiles(Smoots value)
            => value.NauticalMiles;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator NauticalMiles(Feet value)
            => value.NauticalMiles;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator NauticalMiles(Yards value)
            => value.NauticalMiles;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator NauticalMiles(Kilometers value)
            => value.NauticalMiles;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator NauticalMiles (Miles value)
            => value.NauticalMiles;

        #endregion

        #region Methods

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{Value} {Abreviation}";

        #endregion
    }
}
