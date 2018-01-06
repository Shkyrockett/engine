// <copyright file="WeightDensity.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
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
    public struct WeightDensity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="volume"></param>
        public WeightDensity(IMass weight, IVolume volume)
        {
            Weight = weight;
            Volume = volume;
        }

        /// <summary>
        /// 
        /// </summary>
        public IMass Weight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IVolume Volume { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Value => Weight.Value / Volume.Value;

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Weight Density";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => $"{Weight.Abreviation}/{Volume.Abreviation}³";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value}{Weight.Abreviation}/{Volume.Abreviation}³";
    }
}
