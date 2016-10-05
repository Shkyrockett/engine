// <copyright file="Power.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
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
    public struct Power
        : IPower
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="work"></param>
        /// <param name="time"></param>
        public Power(IEnergy work, ITime time)
        {
            Work = work;
            Time = time;
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnergy Work { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ITime Time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Value => Work.Value / Time.Value;

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Power";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => "J";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value}";
    }
}
