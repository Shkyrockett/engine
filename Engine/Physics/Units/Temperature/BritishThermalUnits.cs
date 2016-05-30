using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct BritishThermalUnits
        : ITemperature
    {
        /// <summary>
        /// 
        /// </summary>
        private double value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public BritishThermalUnits(double value)
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
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "British Thermal Units"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return "Btu"; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator BritishThermalUnits(double value)
        {
            return new BritishThermalUnits(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} Btu", value);
        }
    }
}
