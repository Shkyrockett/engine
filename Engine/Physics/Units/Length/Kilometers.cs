using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct Kilometers
        : ILength
    {
        /// <summary>
        /// 
        /// </summary>
        public const double Mil = 3.9370E+7d;

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
        public const double Kilometer = 1d;

        /// <summary>
        /// 
        /// </summary>
        public const double Mile = 1d / 1.609344d;

        /// <summary>
        /// 
        /// </summary>
        public const double NauticalMile = 1d / 1.852d;

        /// <summary>
        /// 
        /// </summary>
        private double value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Kilometers(double value)
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
        public double Inches
        {
            get { return value * Inch; }
            set { this.value = value / Inch; }
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
        public string Name { get { return "Kilometers"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return "km"; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Kilometers(double value)
        {
            return new Kilometers(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} km", value);
        }
    }
}
