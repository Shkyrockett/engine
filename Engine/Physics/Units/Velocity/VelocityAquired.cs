using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public class VelocityAquired
        : IVelocity
    {
        /// <summary>
        /// 
        /// </summary>
        private IAcceleration acceleration;

        /// <summary>
        /// 
        /// </summary>
        private IDirection direction;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="direction"></param>
        public VelocityAquired(IAcceleration speed, IDirection direction)
        {
            this.direction = direction;
            this.acceleration = speed;
        }

        /// <summary>
        /// 
        /// </summary>
        public IAcceleration Acceleration
        {
            get { return acceleration; }
            set { acceleration = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IDirection Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Value
        {
            get { return acceleration.Value * direction.Value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "Velocity"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return "ft"; } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} {1}{2}", Value, acceleration, direction);
        }
    }
}
