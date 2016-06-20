using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct Yards
        : ILength
    {
        /// <summary>
        /// 
        /// </summary>
        public const double Mil = 36000d;

        /// <summary>
        /// 
        /// </summary>
        public const double Centimeter = 91.44d;

        /// <summary>
        /// 
        /// </summary>
        public const double Inch = 36d;

        /// <summary>
        /// 
        /// </summary>
        public const double Foot = 3d;

        /// <summary>
        /// 
        /// </summary>
        public const double Yard = 1d;

        /// <summary>
        /// 
        /// </summary>
        public const double Meter = 1d / 1.0936133d;

        /// <summary>
        /// 
        /// </summary>
        public const double Kilometer = 1d / 1093.6133d;

        /// <summary>
        /// 
        /// </summary>
        public const double Mile = 1d / 1760d;

        /// <summary>
        /// 
        /// </summary>
        public const double NauticalMile = 1d / 2025.37183d;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Yards(double value)
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
        public string Name => "Yards";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => "yd";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Yards(double value) => new Yards(value);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value} yd";
    }
}
