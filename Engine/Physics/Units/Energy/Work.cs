using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct Work
        : IEnergy
    {
        /// <summary>
        /// 
        /// </summary>
        private IForce force;

        /// <summary>
        /// 
        /// </summary>
        private ILength distance;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="force"></param>
        /// <param name="distance"></param>
        public Work(IForce force, ILength distance)
        {
            this.force = force;
            this.distance = distance;
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
        public ILength Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Value
        {
            get { return force.Value * distance.Value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "Work"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return string.Format("{0}{1}", Value, force.Abreviation, distance.Abreviation); } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} {1}{2}", Value, force.Abreviation, distance.Abreviation);
        }
    }
}
