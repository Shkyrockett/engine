using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct KineticEnergy
        : IEnergy
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
        public KineticEnergy(IMass mass, ISpeed velocity)
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
            get { return 0.5 * (mass.Value * velocity.Value * velocity.Value); }
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "Kinetic energy"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return "J Ke"; } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} J Ke", Value);
        }
    }
}
