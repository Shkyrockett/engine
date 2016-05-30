using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct Hours
        : ITime
    {
        /// <summary>
        /// 
        /// </summary>
        public const double Second = 3600d;

        /// <summary>
        /// 
        /// </summary>
        public const double Minute = 60d;

        /// <summary>
        /// 
        /// </summary>
        public const double Hour = 1d;

        /// <summary>
        /// 
        /// </summary>
        public const double Day = 24d;

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
        public Hours(double value)
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
        public double Minutes
        {
            get { return value * Minute; }
            set { this.value = value / Minute; }
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
        public static implicit operator Hours(double value)
        {
            return new Hours(value);
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "Hours"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return "h"; } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} h", value);
        }
    }
}
