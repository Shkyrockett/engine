// <copyright file="BritishThermalUnits.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// 
    /// </summary>
    public struct BritishThermalUnits
        : ITemperature
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public BritishThermalUnits(double value)
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
            => "British Thermal Units";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => "Btu";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator BritishThermalUnits(double value)
            => new BritishThermalUnits(value);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{Value} Btu";
    }
}
