using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct InstantaniousSpeed
        : ISpeed
    {
        /// <summary>
        /// 
        /// </summary>
        private IAcceleration acceleration;

        /// <summary>
        /// 
        /// </summary>
        private ITime time;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="acceleration"></param>
        /// <param name="time"></param>
        public InstantaniousSpeed(IAcceleration acceleration, ITime time)
        {
            this.acceleration = acceleration;
            this.time = time;
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
            get { return acceleration.Value / time.Value; }
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
        public string Abreviation { get { return string.Format("{0}/{1}", Value, acceleration.Abreviation, time.Abreviation); } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} {1}/{2}", Value, acceleration.Abreviation, time.Abreviation);
        }
    }
}
