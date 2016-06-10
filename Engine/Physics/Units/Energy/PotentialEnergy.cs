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
        /// <param name="height"></param>
        /// <param name="weight"></param>
        public PotentialEnergy(ILength height, IMass weight)
        {
            Height = height;
            Weight = weight;
        }

        /// <summary>
        /// 
        /// </summary>
        public ILength Height { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IMass Weight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Value => Weight.Value * Height.Value;

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Potential Energy";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => string.Format("{0}{1}", Value, Weight.Abreviation, Height.Abreviation);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => string.Format("{0} {1}{2}", Value, Weight.Abreviation, Height.Abreviation);
    }
}
