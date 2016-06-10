using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public class VelocityAquired
        : IVelocity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="direction"></param>
        public VelocityAquired(IAcceleration speed, IDirection direction)
        {
            Direction = direction;
            Acceleration = speed;
        }

        /// <summary>
        /// 
        /// </summary>
        public IAcceleration Acceleration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IDirection Direction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Value => Acceleration.Value * Direction.Value;

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Velocity";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => "ft";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => string.Format("{0} {1}{2}", Value, Acceleration, Direction);
    }
}
