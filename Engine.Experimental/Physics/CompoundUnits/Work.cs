﻿// <copyright file="Work.cs" company="Shkyrockett" >
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
    public struct Work
        : IEnergy
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="force"></param>
        /// <param name="distance"></param>
        public Work(IForce force, ILength distance)
        {
            Force = force;
            Distance = distance;
        }

        /// <summary>
        ///
        /// </summary>
        public IForce Force { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ILength Distance { get; set; }

        /// <summary>
        ///
        /// </summary>
        public double Value => Force.Value * Distance.Value;

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => nameof(Work);

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => $"{Value}{Force.Abreviation}";

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value} {Force.Abreviation}{Distance.Abreviation}";
    }
}