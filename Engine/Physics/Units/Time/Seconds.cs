using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct Seconds
        : ITime
    {
        /// <summary>
        /// 
        /// </summary>
        public const double Minute = 1d / 60d;

        /// <summary>
        /// 
        /// </summary>
        public const double Hour = 1d / 3600d;

        /// <summary>
        /// 
        /// </summary>
        public const double Day = 1d / 86400d;

        /// <summary>
        /// 
        /// </summary>
        public const double Year = 1d / (365.25d * Day);

        /// <summary>
        /// 
        /// </summary>
        private double value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Seconds(double value)
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
        public double Years
        {
            get { return value * Year; }
            set { this.value = value / Year; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Seconds(double value)
        {
            return new Seconds(value);
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "Seconds"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return "s"; } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} s", value);
        }
    }
}
