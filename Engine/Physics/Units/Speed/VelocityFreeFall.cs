using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public class VelocityFreeFall
        : ISpeed
    {
        /// <summary>
        /// 
        /// </summary>
        private IAcceleration gravity;

        /// <summary>
        /// 
        /// </summary>
        private ITime time;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gravity"></param>
        /// <param name="time"></param>
        public VelocityFreeFall(IAcceleration gravity, ITime time)
        {
            this.gravity = gravity;
            this.time = time;
        }

        /// <summary>
        /// 
        /// </summary>
        public IAcceleration Gravity
        {
            get { return gravity; }
            set { gravity = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ITime Time
        {
            get { return time; }
            set { time = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Value
        {
            get { return gravity.Value * time.Value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "Velocity at free fall"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return string.Format("{0}{1}", Value, gravity.Abreviation, time.Abreviation); } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} {1}{2}", Value, time.Abreviation, time.Abreviation);
        }
    }
}
