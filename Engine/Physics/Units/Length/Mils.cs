﻿// <copyright file="Mils.cs" >
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
    public struct Mils
        : ILength
    {
        /// <summary>
        /// 
        /// </summary>
        public const double Mil = 1d;

        /// <summary>
        /// 
        /// </summary>
        public const double Centimeter = 1d / 393.700787d;

        /// <summary>
        /// 
        /// </summary>
        public const double Inch = 1d / 1000d;

        /// <summary>
        /// 
        /// </summary>
        public const double Foot = 1d / 12000d;

        /// <summary>
        /// 
        /// </summary>
        public const double Yard = 1d / 36000d;

        /// <summary>
        /// 
        /// </summary>
        public const double Meter = 1d / 39370.0787d;

        /// <summary>
        /// 
        /// </summary>
        public const double Kilometer = 1d / 3.9370E+7d;

        /// <summary>
        /// 
        /// </summary>
        public const double Mile = 1d / 63360000d;

        /// <summary>
        /// 
        /// </summary>
        public const double NauticalMile = 1d / 7.2913E+7d;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Mils(double value)
        {
            Value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public double Value { get; set; }

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
        public string Name => "Mils";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => "mil";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Mils(double value) => new Mils(value);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value} mil";
    }
}
