// <copyright file="Work.cs" >
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
        public string Name => "Work";

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
