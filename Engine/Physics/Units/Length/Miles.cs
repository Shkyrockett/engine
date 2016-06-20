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
        /// <param name="value"></param>
        public Miles(double value)
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
        public double NauticalMiles
        {
            get { return Value * NauticalMile; }
            set { Value = value / NauticalMile; }
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Miles";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => "mile";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Miles(double value) => new Miles(value);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value} mile";
    }
}
