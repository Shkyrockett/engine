using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct Meters
        : ILength
    {
        /// <summary>
        /// 
        /// </summary>
        public const double Mil = 39370.0787d;

        /// <summary>
        /// 
        /// </summary>
        public const double Centimeter = 100d;

        /// <summary>
        /// 
        /// </summary>
        public const double Inch = 39.3700787d;

        /// <summary>
        /// 
        /// </summary>
        public const double Foot = 3.2808399d;

        /// <summary>
        /// 
        /// </summary>
        public const double Yard = 1.0936133d;

        /// <summary>
        /// 
        /// </summary>
        public const double Meter = 1d;

        /// <summary>
        /// 
        /// </summary>
        public const double Kilometer = 1d / 1000d;

        /// <summary>
        /// 
        /// </summary>
        public const double Mile = 1d / 1609.344d;

        /// <summary>
        /// 
        /// </summary>
        public const double NauticalMile = 1d / 1852d;

        /// <summary>
        /// 
        /// </summary>
        private double value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Meters(double value)
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
        public double NauticalMiles
        {
            get { return value * NauticalMile; }
            set { this.value = value / NauticalMile; }
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "Meters"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return "m"; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Meters(double value)
        {
            return new Meters(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} m", value);
        }
    }
}
