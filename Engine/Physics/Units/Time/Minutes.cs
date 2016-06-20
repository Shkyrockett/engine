using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct Minutes
        : ITime
    {
        /// <summary>
        /// 
        /// </summary>
        public const double Second = 60d;

        /// <summary>
        /// 
        /// </summary>
        public const double Minute = 1d;

        /// <summary>
        /// 
        /// </summary>
        public const double Hour = 1d / 60d;

        /// <summary>
        /// 
        /// </summary>
        public const double Day = 1d / 1440d;

        /// <summary>
        /// 
        /// </summary>
        public const double Year = 365.25d * Day;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Minutes(double value)
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
        public double Seconds
        {
            get { return Value * Second; }
            set { Value = value / Second; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Hours
        {
            get { return Value * Hour; }
            set { Value = value / Hour; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Days
        {
            get { return Value * Day; }
            set { Value = value / Day; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Years
        {
            get { return Value * Year; }
            set { Value = value / Year; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Minutes(double value) => new Minutes(value);

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Minutes";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => "min";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value} min";
    }
}
