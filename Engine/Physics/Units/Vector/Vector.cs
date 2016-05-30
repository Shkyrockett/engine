using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct Vector
        : IVector
    {
        /// <summary>
        /// 
        /// </summary>
        private double magnitude;

        /// <summary>
        /// 
        /// </summary>
        private double direction;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="magnitude"></param>
        /// <param name="direction"></param>
        public Vector(double magnitude, double direction)
        {
            this.magnitude = magnitude;
            this.direction = direction;
        }

        /// <summary>
        /// 
        /// </summary>
        public double Magnitude
        {
            get { return magnitude; }
            set { magnitude = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Value
        {
            get { return magnitude * Direction; }
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "Vector"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return string.Format("{0}{1}", "?", "?"); } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} K", Value);
        }
    }
}
