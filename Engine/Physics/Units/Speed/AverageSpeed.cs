using System.Collections.Generic;
using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct AverageSpeed
        : ISpeed
    {
        /// <summary>
        /// 
        /// </summary>
        private List<ISpeed> speed;

        /// <summary>
        /// 
        /// </summary>
        private ITime time;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="time"></param>
        public AverageSpeed(List<ISpeed> speed, ITime time)
        {
            this.speed = speed;
            this.time = time;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<ISpeed> Speed
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
                double rSpeed = 0;
                foreach (ISpeed cSpeed in speed)
                {
                    rSpeed += cSpeed.Value;
                }

                return rSpeed / time.Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "Average Speed"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return string.Format("∆{0}/{1}", Value, speed[0].Abreviation, time.Abreviation); } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} ∆{1}/{2}", Value, speed[0].Abreviation, time.Abreviation);
        }
    }
}
