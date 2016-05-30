using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct Momentum
        : IMomentum
    {
        /// <summary>
        /// 
        /// </summary>
        private IMass mass;

        /// <summary>
        /// 
        /// </summary>
        private ISpeed velocity;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="velocity"></param>
        public Momentum(IMass mass, ISpeed velocity)
        {
            this.mass = mass;
            this.velocity = velocity;
        }

        /// <summary>
        /// 
        /// </summary>
        public IMass Mass
        {
            get { return mass; }
            set { mass = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ISpeed Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Value
        {
            get { return mass.Value * velocity.Value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "Momentum"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return string.Format("{0}{1}", Value, mass.Abreviation, velocity.Abreviation); } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} {1}{2}", Value, mass.Abreviation, velocity.Abreviation);
        }
    }
}
