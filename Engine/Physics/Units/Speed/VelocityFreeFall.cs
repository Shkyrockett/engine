using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public class VelocityFreeFall
        : ISpeed
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gravity"></param>
        /// <param name="time"></param>
        public VelocityFreeFall(IAcceleration gravity, ITime time)
        {
            Gravity = gravity;
            Time = time;
        }

        /// <summary>
        /// 
        /// </summary>
        public IAcceleration Gravity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ITime Time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Value => Gravity.Value * Time.Value;

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Velocity at free fall";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => string.Format("{0}{1}", Value, Gravity.Abreviation, Time.Abreviation);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => string.Format("{0} {1}{2}", Value, Time.Abreviation, Time.Abreviation);
    }
}
