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
        private double value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Years(double value)
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
        public double Days
        {
            get { return value * Day; }
            set { this.value = value / Day; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Years(double value)
        {
            return new Years(value);
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "Years"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return "years"; } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} years", value);
        }
    }
}
