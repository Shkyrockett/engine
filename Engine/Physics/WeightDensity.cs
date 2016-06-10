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
        /// <param name="weight"></param>
        /// <param name="volume"></param>
        public WeightDensity(IMass weight, IVolume volume)
        {
            Weight = weight;
            Volume = volume;
        }

        /// <summary>
        /// 
        /// </summary>
        public IMass Weight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IVolume Volume { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Value => Weight.Value / Volume.Value;

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Weight Density";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => string.Format("{0}/{1}³", Weight.Abreviation, Volume.Abreviation);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => string.Format("{0}{1}/{2}³", Value, Weight.Abreviation, Volume.Abreviation);
    }
}
