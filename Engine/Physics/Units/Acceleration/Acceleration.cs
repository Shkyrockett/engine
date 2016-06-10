using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct Acceleration
        : IAcceleration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="velocityChange"></param>
        /// <param name="timeInterval"></param>
        public Acceleration(IVelocity velocityChange, ITime timeInterval)
        {
            VelocityChange = velocityChange;
            TimeInterval = timeInterval;
        }

        /// <summary>
        /// 
        /// </summary>
        public IVelocity VelocityChange { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ITime TimeInterval { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Value => VelocityChange.Value / TimeInterval.Value;

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Acceleration";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => string.Format("∆{0}/∆{1}", Value, VelocityChange.Abreviation, TimeInterval.Abreviation);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => string.Format("{0} ∆{1}/∆{2}", Value, VelocityChange.Abreviation, TimeInterval.Abreviation);
    }
}
