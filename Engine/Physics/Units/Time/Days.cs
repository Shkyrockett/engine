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
        private double value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Days(double value)
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
        public double Hours
        {
            get { return value * Hour; }
            set { this.value = value / Hour; }
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
        public static implicit operator Days(double value)
        {
            return new Days(value);
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "Days"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return "days"; } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} days", value);
        }
    }
}
