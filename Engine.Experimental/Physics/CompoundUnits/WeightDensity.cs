// <copyright file="WeightDensity.cs" company="Shkyrockett" >
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
    /// The weight density struct.
    /// </summary>
    public struct WeightDensity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeightDensity"/> class.
        /// </summary>
        /// <param name="weight">The weight.</param>
        /// <param name="volume">The volume.</param>
        public WeightDensity(IMass weight, IVolume volume)
        {
            Weight = weight;
            Volume = volume;
        }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        public IMass Weight { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        public IVolume Volume { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public double Value => Weight.Value / Volume.Value;

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => "Weight Density";

        /// <summary>
        /// Gets the abreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => $"{Weight.Abreviation}/{Volume.Abreviation}³";

        /// <returns></returns>
        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString() => $"{Value}{Weight.Abreviation}/{Volume.Abreviation}³";
    }
}
