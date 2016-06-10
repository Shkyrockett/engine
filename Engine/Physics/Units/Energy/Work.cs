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
        /// <param name="force"></param>
        /// <param name="distance"></param>
        public Work(IForce force, ILength distance)
        {
            Force = force;
            Distance = distance;
        }

        /// <summary>
        /// 
        /// </summary>
        public IForce Force { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ILength Distance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Value => Force.Value * Distance.Value;

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Work";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => string.Format("{0}{1}", Value, Force.Abreviation, Distance.Abreviation);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => string.Format("{0} {1}{2}", Value, Force.Abreviation, Distance.Abreviation);
    }
}
