using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct PotentialEnergy
        : IEnergy
    {
        /// <summary>
        /// 
        /// </summary>
        private ILength height;

        /// <summary>
        /// 
        /// </summary>
        private IMass weight;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="height"></param>
        /// <param name="weight"></param>
        public PotentialEnergy(ILength height, IMass weight)
        {
            this.height = height;
            this.weight = weight;
        }

        /// <summary>
        /// 
        /// </summary>
        public ILength Height
        {
            get { return height; }
            set { height = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IMass Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Value
        {
            get { return weight.Value * height.Value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "Potential Energy"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return string.Format("{0}{1}", Value, weight.Abreviation, height.Abreviation); } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} {1}{2}", Value, weight.Abreviation, height.Abreviation);
        }
    }
}
