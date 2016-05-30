using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct Speed
        : ISpeed
    {
        /// <summary>
        /// 
        /// </summary>
        private ILength distance;

        /// <summary>
        /// 
        /// </summary>
        private ITime time;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="time"></param>
        public Speed(ILength distance, ITime time)
        {
            this.distance = distance;
            this.time = time;
        }

        /// <summary>
        /// 
        /// </summary>
        public ILength Distance
        {
            get { return distance; }
            set { distance = value; }
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
            get { return distance.Value / time.Value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "Seconds"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return string.Format("{1}/{2}", Value, distance.Abreviation, time.Abreviation); } }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} {1}/{2}", Value, distance.Abreviation, time.Abreviation);
        }
    }
}
