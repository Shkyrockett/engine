using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct Acceleration
        : IAcceleration
    {
        /// <summary>
        /// 
        /// </summary>
        private IVelocity velocityChange;

        /// <summary>
        /// 
        /// </summary>
        private ITime timeInterval;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="velocityChange"></param>
        /// <param name="timeInterval"></param>
        public Acceleration(IVelocity velocityChange, ITime timeInterval)
        {
            this.velocityChange = velocityChange;
            this.timeInterval = timeInterval;
        }

        /// <summary>
        /// 
        /// </summary>
        public IVelocity VelocityChange
        {
            get { return velocityChange; }
            set { velocityChange = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ITime TimeInterval
        {
            get { return timeInterval; }
            set { timeInterval = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Value
        {
            get { return velocityChange.Value / timeInterval.Value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "Acceleration"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return string.Format("∆{0}/∆{1}", Value, velocityChange.Abreviation, timeInterval.Abreviation); } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} ∆{1}/∆{2}", Value, velocityChange.Abreviation, timeInterval.Abreviation);
        }
    }
}
