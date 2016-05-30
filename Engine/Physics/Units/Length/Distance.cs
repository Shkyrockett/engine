using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct Distance
        : ILength
    {
        /// <summary>
        /// 
        /// </summary>
        private ISpeed speed;

        /// <summary>
        /// 
        /// </summary>
        private ITime time;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="time"></param>
        public Distance(ISpeed speed, ITime time)
        {
            this.speed = speed;
            this.time = time;
        }

        /// <summary>
        /// 
        /// </summary>
        public ISpeed Speed
        {
            get { return speed; }
            set { speed = value; }
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
            get
            {
                return time.Value * speed.Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "Distance"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return "Distance"; } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} {1}{2}", Value, speed.Abreviation, time.Abreviation);
        }
    }
}
