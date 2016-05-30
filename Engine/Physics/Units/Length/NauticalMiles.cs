﻿using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct NauticalMiles
        : ILength
    {
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
        public const double Kilometer = 1.852d;

        /// <summary>
        /// 
        /// </summary>
        public const double Mile = 1.15077945d;

        /// <summary>
        /// 
        /// </summary>
        public const double NauticalMile = 1d;

        /// <summary>
        /// 
        /// </summary>
        private double value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public NauticalMiles(double value)
        {
            this.value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public double Value
        {
            get { return value; }
            set { this.value = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Mils
        {
            get { return value * Mil; }
            set { this.value = value / Mil; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Centimeters
        {
            get { return value * Centimeter; }
            set { this.value = value / Centimeter; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Inches
        {
            get { return value * Inch; }
            set { this.value = value / Inch; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Feet
        {
            get { return value * Foot; }
            set { this.value = value / Foot; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Yards
        {
            get { return value * Yard; }
            set { this.value = value / Yard; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Meters
        {
            get { return value * Meter; }
            set { this.value = value / Meter; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Kilometers
        {
            get { return value * Kilometer; }
            set { this.value = value / Kilometer; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Miles
        {
            get { return value * Mile; }
            set { this.value = value / Mile; }
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "Nautical Miles"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return "Nm"; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator NauticalMiles(double value)
        {
            return new NauticalMiles(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} Nm", value);
        }
    }
}
