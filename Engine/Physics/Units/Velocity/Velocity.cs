using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public class Velocity
        : IVelocity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="direction"></param>
        public Velocity(ISpeed speed, IDirection direction)
        {
            Direction = direction;
            Acceleration = speed;
        }

        /// <summary>
        /// 
        /// </summary>
        public ISpeed Acceleration { get; set; }

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
        public string Abreviation => $"{Acceleration.Abreviation} {Direction.Abreviation}";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value} {Acceleration.Abreviation} {Direction.Abreviation}";
    }
}
