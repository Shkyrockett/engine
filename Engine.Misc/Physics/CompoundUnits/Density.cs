// <copyright file="Density.cs" company="Shkyrockett" >
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
    public struct Density
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="volume"></param>
        public Density(IMass mass, IVolume volume)
        {
            Mass = mass;
            Volume = volume;
        }

        /// <summary>
        /// 
        /// </summary>
        public IMass Mass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IVolume Volume { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Value => Mass.Value / Volume.Value;

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => nameof(Density);

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => $"{Mass.Abreviation}/{Volume.Abreviation}³";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value}{Mass.Abreviation}/{Volume.Abreviation}³";
    }
}
