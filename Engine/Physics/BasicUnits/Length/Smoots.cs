// <copyright file="Smoots.cs" >
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
    public struct Smoots
        : ILength
    {
        #region Constants

        /// <summary>
        ///
        /// </summary>
        public const double Mil = 67000d;

        /// <summary>
        ///
        /// </summary>
        public const double Centimeter = 170.18d;

        /// <summary>
        ///
        /// </summary>
        public const double Inch = 67d;

        /// <summary>
        ///
        /// </summary>
        public const double Foot = 5.58333333d;

        /// <summary>
        ///
        /// </summary>
        public const double Yard = 1.86111111;

        /// <summary>
        ///
        /// </summary>
        public const double Meter = 1.7018;

        /// <summary>
        ///
        /// </summary>
        public const double Smoot = 1;

        /// <summary>
        ///
        /// </summary>
        public const double Kilometer = 1d / 587.613116d;

        /// <summary>
        ///
        /// </summary>
        public const double Mile = 1d / 945.671642d;

        /// <summary>
        ///
        /// </summary>
        public const double NauticalMile = 1d / 1088.25949d;

        #endregion

        #region Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public Smoots(double value)
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
        public static implicit operator Smoots(double value)
            => new Smoots(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Smoots(Mils value)
            => value.Smoots;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Smoots(Centimeters value)
            => value.Smoots;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Smoots(Yards value)
            => value.Smoots;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Smoots(Meters value)
            => value.Smoots;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Smoots(Inches value)
            => value.Smoots;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Smoots(Feet value)
            => value.Smoots;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Smoots(Kilometers value)
            => value.Smoots;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Smoots(Miles value)
            => value.Smoots;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator Smoots(NauticalMiles value)
            => value.Smoots;

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
