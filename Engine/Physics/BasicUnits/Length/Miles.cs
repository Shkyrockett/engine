// <copyright file="Miles.cs" >
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
    public struct Miles
        : ILength
    {
        #region Constants

        /// <summary>
        ///
        /// </summary>
        public const double Mil = 63360000d;

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

        #endregion

        #region Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public Miles(double value)
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
            => "Miles";

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => "mile";

        #endregion

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
