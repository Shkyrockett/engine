namespace Engine.Physics
{
    using System.ComponentModel;

    /// <summary>
    /// 
    /// </summary>
    public struct Density
    {
        /// <summary>
        /// 
        /// </summary>
        private IMass mass;

        /// <summary>
        /// 
        /// </summary>
        private IVolume volume;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="volume"></param>
        public Density(IMass mass, IVolume volume)
        {
            this.mass = mass;
            this.volume = volume;
        }

        /// <summary>
        /// 
        /// </summary>
        public IMass Mass
        {
            get { return mass; }
            set { mass = value; }
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
            get { return mass.Value / volume.Value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name { get { return "Density"; } }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation { get { return string.Format("{0}/{1}³", this.mass.Abreviation, this.volume.Abreviation); } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}{1}/{2}³", Value, this.mass.Abreviation, this.volume.Abreviation);
        }
    }
}
