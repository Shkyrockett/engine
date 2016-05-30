using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct Inches
        : ILength
    {
        /// <summary>
        /// 
        /// </summary>
        public const double Mil = 1000d;

        /// <summary>
        /// 
        /// </summary>
        public const double Centimeter = 2.54d;

        /// <summary>
        /// 
        /// </summary>
        public const double Inch = 1d;

        /// <summary>
        /// 
        /// </summary>
        public const double Foot = 1d / 12d;

        /// <summary>
        /// 
        /// </summary>
        public const double Yard = 1d / 36d;

        /// <summary>
        /// 
        /// </summary>
        public const double Meter = 1d / 39.3700787d;

        /// <summary>
        /// 
        /// </summary>
        public const double Kilometer = 1d / 39370.0787d;

        /// <summary>
        /// 
        /// </summary>
        public const double Mile = 1d / 63360d;

        /// <summary>
        /// 
        /// </summary>
        public const double NauticalMile = 1d / 72913.3858d;

        /// <summary>
        /// 
        /// </summary>
        private double value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Inches(double value)
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
        public string Name { get { return "Inches"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return "in"; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Inches(double value)
        {
            return new Inches(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} in", value);
        }
    }
}
