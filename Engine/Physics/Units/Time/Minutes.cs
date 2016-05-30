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
        private double value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Minutes(double value)
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
        public double Seconds
        {
            get { return value * Second; }
            set { this.value = value / Second; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Hours
        {
            get { return value * Hour; }
            set { this.value = value / Hour; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Days
        {
            get { return value * Day; }
            set { this.value = value / Day; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Years
        {
            get { return value * Year; }
            set { this.value = value / Year; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Minutes(double value)
        {
            return new Minutes(value);
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "Minutes"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return "min"; } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} min", value);
        }
    }
}
