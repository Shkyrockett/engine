using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct DistanceFreeFall
        : ILength
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
        public DistanceFreeFall(IAcceleration acceleration, ITime time)
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
            get { return acceleration.Value * time.Value * time.Value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "Instantanious Speed"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return string.Format("{0}{1}²", Value, acceleration.Abreviation, time.Abreviation); } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} {1}{2}²", Value, acceleration.Abreviation, time.Abreviation);
        }
    }
}
