using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct Feet
        : ILength
    {
        /// <summary>
        /// 
        /// </summary>
        public const double Mil = 12000d;

        /// <summary>
        /// 
        /// </summary>
        public const double Centimeter = 30.48d;

        /// <summary>
        /// 
        /// </summary>
        public const double Inch = 12d;

        /// <summary>
        /// 
        /// </summary>
        public const double Foot = 1d;

        /// <summary>
        /// 
        /// </summary>
        public const double Yard = 1d / 3d;

        /// <summary>
        /// 
        /// </summary>
        public const double Meter = 1d / 3.2808399d;

        /// <summary>
        /// 
        /// </summary>
        public const double Kilometer = 1d / 3280.8399d;

        /// <summary>
        /// 
        /// </summary>
        public const double Mile = 1d / 5280d;

        /// <summary>
        /// 
        /// </summary>
        public const double NauticalMile = 1d / 6076.11549d;

        /// <summary>
        /// 
        /// </summary>
        private double value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Feet(double value)
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
        public string Name { get { return "Feet"; } }
        
        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return "ft"; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Feet(double value)
        {
            return new Feet(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} ft", value);
        }
    }
}
