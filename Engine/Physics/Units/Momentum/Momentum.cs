using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct Momentum
        : IMomentum
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="velocity"></param>
        public Momentum(IMass mass, ISpeed velocity)
        {
            Mass = mass;
            Velocity = velocity;
        }

        /// <summary>
        /// 
        /// </summary>
        public IMass Mass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ISpeed Velocity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Value => Mass.Value * Velocity.Value;

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Momentum";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => $"{Value}{Mass.Abreviation}";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value} {Mass.Abreviation}{Velocity.Abreviation}";
    }
}
