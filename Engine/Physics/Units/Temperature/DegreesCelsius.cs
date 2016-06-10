﻿using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct DegreesCelsius
        : ITemperature
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public DegreesCelsius(double value)
        {
            Value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Degrees Celsius";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => "°C";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator DegreesCelsius(double value) => new DegreesCelsius(value);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => string.Format("{0} °C", Value);
    }
}
