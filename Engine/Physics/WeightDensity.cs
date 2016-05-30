using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct WeightDensity
    {
        /// <summary>
        /// 
        /// </summary>
        private IMass weight;

        /// <summary>
        /// 
        /// </summary>
        private IVolume volume;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="volume"></param>
        public WeightDensity(IMass weight, IVolume volume)
        {
            this.weight = weight;
            this.volume = volume;
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
        public IVolume Volume
        {
            get { return volume; }
            set { volume = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double Value
        {
            get { return weight.Value / volume.Value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "Weight Density"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return string.Format("{0}/{1}³", this.weight.Abreviation, this.volume.Abreviation); } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}{1}/{2}³", Value, this.weight.Abreviation, this.volume.Abreviation);
        }
    }
}
