﻿using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct KineticEnergy
        : IEnergy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="velocity"></param>
        public KineticEnergy(IMass mass, ISpeed velocity)
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
        public double Value => 0.5 * (Mass.Value * Velocity.Value * Velocity.Value);

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Kinetic energy";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => "J Ke";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value} J Ke";
    }
}
