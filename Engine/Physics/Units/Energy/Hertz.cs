using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct Hertz
        : IEnergy
    {
        /// <summary>
        /// 
        /// </summary>
        private double value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Hertz(double value)
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
        public string Name { get { return "Hertz"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return "Hz"; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Hertz(double value)
        {
            return new Hertz(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} Hz", value);
        }
    }
}
