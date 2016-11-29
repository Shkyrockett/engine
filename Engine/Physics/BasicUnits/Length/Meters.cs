// <copyright file="Meters.cs" >
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
    public struct Meters
        : ILength
    {
        #region Constants

        /// <summary>
        /// The number of Mill in a Meter.
        /// </summary>
        public const double Mil = 39370.0787d;

        /// <summary>
        /// The number of Centimeters in a Meter.
        /// </summary>
        public const double Centimeter = 100d;

        /// <summary>
        /// The number of Inches in a Meter.
        /// </summary>
        public const double Inch = 39.3700787d;

        /// <summary>
        /// The number of Feet in a Meter.
        /// </summary>
        public const double Foot = 3.2808399d;

        /// <summary>
        /// The number of Yards in a Meter.
        /// </summary>
        public const double Yard = 1.0936133d;

        /// <summary>
        /// The number of Meters in a Meter.
        /// </summary>
        public const double Meter = 1d;

        /// <summary>
        ///
        /// </summary>
        public const double Smoot = Inch * 67d;

        /// <summary>
        /// The fraction of Kilometers in a Meter.
        /// </summary>
        public const double Kilometer = 1d / 1000d;

        /// <summary>
        /// The fraction of Miles in a Meter.
        /// </summary>
        public const double Mile = 1d / 1609.344d;

        /// <summary>
        /// The fraction of NauticalcMiles in a Meter.
        /// </summary>
        public const double NauticalMile = 1d / 1852d;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Meters"/> Struct.
        /// </summary>
        /// <param name="value">The distance in <see cref="Meters"/>.</param>
        public Meters(double value)
        {
            Value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The numarical value of the distance in <see cref="Meters"/>.
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
            => "Meters";

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => "m";

        #endregion

        #region Operators

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Meters(double value)
            => new Meters(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Meters(Mils value)
            => value.Meters;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Meters(Centimeters value)
            => value.Meters;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Meters(Inches value)
            => value.Meters;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Meters(Feet value)
            => value.Meters;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Meters(Yards value)
            => value.Meters;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Meters(Smoots value)
            => value.Meters;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Meters(Kilometers value)
            => value.Meters;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Meters(Miles value)
            => value.Meters;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Meters(NauticalMiles value)
            => value.Meters;

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
