namespace Engine.Physics
{
    using System.ComponentModel;

    /// <summary>
    /// Unit of Charge
    /// </summary>
    public struct Coulombs
    {
        /// <summary>
        /// 
        /// </summary>
        private double value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Coulombs(double value)
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
        public string Name { get { return "Coulombs"; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Coulombs(double value)
        {
            return new Coulombs(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} C", value);
        }
    }
}
