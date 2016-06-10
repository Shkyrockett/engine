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
        /// <param name="value"></param>
        public Feet(double value)
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
        public string Name => "Feet";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => "ft";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Feet(double value) => new Feet(value);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => string.Format("{0} ft", Value);
    }
}
