﻿// <copyright file="Impulse.cs" company="Shkyrockett" >
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
    public struct Impulse
        : IForce
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="force"></param>
        /// <param name="time"></param>
        public Impulse(IForce force, ITime time)
        {
            Force = force;
            Time = time;
        }

        /// <summary>
        ///
        /// </summary>
        public IForce Force { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ITime Time { get; set; }

        /// <summary>
        ///
        /// </summary>
        public double Value => Force.Value * Time.Value;

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Instantaneous Speed";

        /// <summary>
        ///
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => $"{Value}{Force.Abreviation}";

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Value} {Force.Abreviation}{Time.Abreviation}";
    }
}
