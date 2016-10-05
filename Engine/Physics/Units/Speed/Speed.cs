﻿// <copyright file="Speed.cs" >
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
    public struct Speed
        : ISpeed
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="time"></param>
        public Speed(ILength distance, ITime time)
        {
            Distance = distance;
            Time = time;
        }

        /// <summary>
        /// 
        /// </summary>
        public ILength Distance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ITime Time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Value => Distance.Value / Time.Value;

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Seconds";

        /// <summary>
        /// 
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => $"{Distance.Abreviation}/{Time.Abreviation}";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value} {Distance.Abreviation}/{Time.Abreviation}";
    }
}
