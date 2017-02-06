// <copyright file="DegreesFahrenheit.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct DegreesFahrenheit
        : ITemperature
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public DegreesFahrenheit(double value)
            => Value = value;

        /// <summary>
        /// 
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name
            => "Degrees Fahrenheit";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => "°F";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator DegreesFahrenheit(double value)
            => new DegreesFahrenheit(value);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{Value} °F";
    }
}
