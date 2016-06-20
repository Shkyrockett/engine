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
        /// <param name="value"></param>
        public Meters(double value)
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
        public string Name => "Meters";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => "m";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Meters(double value) => new Meters(value);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value} m";
    }
}
