// <copyright file="KineticEnergy.cs" company="Shkyrockett" >
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
    /// The kinetic energy struct.
    /// </summary>
    public struct KineticEnergy
        : IEnergy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KineticEnergy"/> class.
        /// </summary>
        /// <param name="mass">The mass.</param>
        /// <param name="velocity">The velocity.</param>
        public KineticEnergy(IMass mass, ISpeed velocity)
        {
            Mass = mass;
            Velocity = velocity;
        }

        /// <summary>
        /// Gets or sets the mass.
        /// </summary>
        public IMass Mass { get; set; }

        /// <summary>
        /// Gets or sets the velocity.
        /// </summary>
        public ISpeed Velocity { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public double Value => 0.5d * (Mass.Value * Velocity.Value * Velocity.Value);

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Kinetic energy";

        /// <summary>
        /// Gets the abreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => "J Ke";

        /// <returns></returns>
        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString() => $"{Value} J Ke";
    }
}
