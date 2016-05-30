using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct Centimeters
        : ILength
    {
        /// <summary>
        /// 
        /// </summary>
        public const double Mil = 393.700787d;

        /// <summary>
        /// 
        /// </summary>
        public const double Centimeter = 1d;

        /// <summary>
        /// 
        /// </summary>
        public const double Inch = 1d / 2.54d;

        /// <summary>
        /// 
        /// </summary>
        public const double Foot = 1d / 30.48d;

        /// <summary>
        /// 
        /// </summary>
        public const double Yard = 1d / 91.44d;

        /// <summary>
        /// 
        /// </summary>
        public const double Meter = 1d / 100d;

        /// <summary>
        /// 
        /// </summary>
        public const double Kilometer = 1d / 100000d;

        /// <summary>
        /// 
        /// </summary>
        public const double Mile = 1d / 160934.4d;

        /// <summary>
        /// 
        /// </summary>
        public const double NauticalMile = 1d / 185200d;

        /// <summary>
        /// 
        /// </summary>
        private double value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Centimeters(double value)
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
        public double NauticalMiles
        {
            get { return value * NauticalMile; }
            set { this.value = value / NauticalMile; }
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "Centimeters"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return "cm"; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Centimeters(double value)
        {
            return new Centimeters(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} cm", value);
        }
    }
}
