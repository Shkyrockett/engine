// <copyright file="PotentialEnergy.cs" company="Shkyrockett" >
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
    /// The potential energy struct.
    /// </summary>
    public struct PotentialEnergy
        : IEnergy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PotentialEnergy"/> class.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <param name="weight">The weight.</param>
        public PotentialEnergy(ILength height, IMass weight)
        {
            Height = height;
            Weight = weight;
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public ILength Height { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        public IMass Weight { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public double Value => Weight.Value * Height.Value;

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Potential Energy";

        /// <summary>
        /// Gets the abreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => $"{Value}{Weight.Abreviation}";

        /// <returns></returns>
        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString() => $"{Value} {Weight.Abreviation}{Height.Abreviation}";
    }
}
