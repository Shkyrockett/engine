using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct Days
        : ITime
    {
        /// <summary>
        /// 
        /// </summary>
        public const double Second = 86400d;

        /// <summary>
        /// 
        /// </summary>
        public const double Minute = 1440d;

        /// <summary>
        /// 
        /// </summary>
        public const double Hour = 24d;

        /// <summary>
        /// 
        /// </summary>
        public const double Day = 1d;

        /// <summary>
        /// 
        /// </summary>
        public const double Year = 1d / 365.25d;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Days(double value)
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
        public double Minutes
        {
            get { return Value * Minute; }
            set { Value = value / Minute; }
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
        public double Years
        {
            get { return Value * Year; }
            set { Value = value / Year; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Days(double value) => new Days(value);

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Days";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => "days";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value} days";
    }
}
