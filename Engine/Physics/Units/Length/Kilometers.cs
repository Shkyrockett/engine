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
        /// <param name="value"></param>
        public Kilometers(double value)
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
        public double Inches
        {
            get { return Value * Inch; }
            set { Value = value / Inch; }
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
        public double Meters
        {
            get { return Value * Meter; }
            set { Value = value / Meter; }
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
        public string Name => "Kilometers";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => "km";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Kilometers(double value) => new Kilometers(value);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => string.Format("{0} km", Value);
    }
}
