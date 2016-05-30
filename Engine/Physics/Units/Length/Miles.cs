using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct Miles
        : ILength
    {
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
        public const double Kilometer = 1.609344d;

        /// <summary>
        /// 
        /// </summary>
        public const double Mile = 1d;

        /// <summary>
        /// 
        /// </summary>
        public const double NauticalMile = 1d / 1.15077945d;

        /// <summary>
        /// 
        /// </summary>
        private double value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Miles(double value)
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
        public double NauticalMiles
        {
            get { return value * NauticalMile; }
            set { this.value = value / NauticalMile; }
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "Miles"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return "mile"; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Miles(double value)
        {
            return new Miles(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} mile", value);
        }
    }
}
