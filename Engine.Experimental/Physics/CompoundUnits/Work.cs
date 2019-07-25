// <copyright file="Work.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
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
    /// The work struct.
    /// </summary>
    public struct Work
        : IEnergy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Work"/> class.
        /// </summary>
        /// <param name="force">The force.</param>
        /// <param name="distance">The distance.</param>
        public Work(IForce force, ILength distance)
        {
            Force = force;
            Distance = distance;
        }

        /// <summary>
        /// Gets or sets the force.
        /// </summary>
        public IForce Force { get; set; }

        /// <summary>
        /// Gets or sets the distance.
        /// </summary>
        public ILength Distance { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public double Value => Force.Value * Distance.Value;

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string Name => nameof(Work);

        /// <summary>
        /// Gets the abreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => $"{Value}{Force.Abreviation}";

        /// <returns></returns>
        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString() => $"{Value} {Force.Abreviation}{Distance.Abreviation}";
    }
}
