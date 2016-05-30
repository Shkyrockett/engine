using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct Impulse
        : IForce
    {
        /// <summary>
        /// 
        /// </summary>
        private IForce force;

        /// <summary>
        /// 
        /// </summary>
        private ITime time;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="force"></param>
        /// <param name="time"></param>
        public Impulse(IForce force, ITime time)
        {
            this.force = force;
            this.time = time;
        }

        /// <summary>
        /// 
        /// </summary>
        public IForce Force
        {
            get { return force; }
            set { force = value; }
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
            get { return force.Value * time.Value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "Instantaneous Speed"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return string.Format("{0}{1}", Value, force.Abreviation, time.Abreviation); } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} {1}{2}", Value, force.Abreviation, time.Abreviation);
        }
    }
}
