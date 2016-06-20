using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct Years
        : ITime
    {
        /// <summary>
        /// 
        /// </summary>
        public const double Second = 31557600d;

        /// <summary>
        /// 
        /// </summary>
        public const double Minute = 525960d;

        /// <summary>
        /// 
        /// </summary>
        public const double Hour = 8766d;

        /// <summary>
        /// 
        /// </summary>
        public const double Day = 365.25d;

        /// <summary>
        /// 
        /// </summary>
        public const double Year = 1d;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Years(double value)
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
        public double Days
        {
            get { return Value * Day; }
            set { Value = value / Day; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Years(double value) => new Years(value);

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Years";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => "years";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value} years";
    }
}
