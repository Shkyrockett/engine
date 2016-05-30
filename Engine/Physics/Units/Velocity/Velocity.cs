using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public class Velocity
        : IVelocity
    {
        /// <summary>
        /// 
        /// </summary>
        private ISpeed speed;

        /// <summary>
        /// 
        /// </summary>
        private IDirection direction;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="direction"></param>
        public Velocity(ISpeed speed, IDirection direction)
        {
            this.direction = direction;
            this.speed = speed;
        }

        /// <summary>
        /// 
        /// </summary>
        public ISpeed Acceleration
        {
            get { return speed; }
            set { speed = value; }
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
            get { return speed.Value * direction.Value; }
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
        public string Abreviation { get { return string.Format("{0} {1}", speed.Abreviation, direction.Abreviation); } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Value, speed.Abreviation, direction.Abreviation);
        }
    }
}
