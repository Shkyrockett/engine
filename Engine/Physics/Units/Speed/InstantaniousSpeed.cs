﻿using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct InstantaniousSpeed
        : ISpeed
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="acceleration"></param>
        /// <param name="time"></param>
        public InstantaniousSpeed(IAcceleration acceleration, ITime time)
        {
            Acceleration = acceleration;
            Time = time;
        }

        /// <summary>
        /// 
        /// </summary>
        public IAcceleration Acceleration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ITime Time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Value => Acceleration.Value / Time.Value;

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Instantaneous Speed";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => $"{Value}/{Acceleration.Abreviation}";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value} {Acceleration.Abreviation}/{Time.Abreviation}";
    }
}
