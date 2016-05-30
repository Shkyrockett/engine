namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public class PhysicsObject
    {
        /// <summary>
        /// 
        /// </summary>
        public Momentum p; // p = momentum.
    }

    /// <summary>
    /// 
    /// </summary>
    public class UnitStrings
    {
        /// <summary>
        /// 
        /// </summary>
        public const string UnitString = "{0} {1}*{2}";

        /// <summary>
        /// 
        /// </summary>
        public const string Momentum = "{0} kg*m/s";

        /// <summary>
        /// 
        /// </summary>
        public const string ImpulseNs = "{0} kg*m/s"; // also N*s

        /// <summary>
        /// 
        /// </summary>
        public const string ImpulseFt = "{0} N*s";

        /// <summary>
        /// 
        /// </summary>
        public const string FrictionForce = "{0} N";
    }
}
