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
    /// The density struct.
    /// </summary>
    public struct Density
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Density"/> class.
        /// </summary>
        /// <param name="mass">The mass.</param>
        /// <param name="volume">The volume.</param>
        public Density(IMass mass, IVolume volume)
        {
            Mass = mass;
            Volume = volume;
        }

        /// <summary>
        /// Gets or sets the mass.
        /// </summary>
        public IMass Mass { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        public IVolume Volume { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public double Value => Mass.Value / Volume.Value;

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name => nameof(Density);

        /// <summary>
        /// Gets the abreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation => $"{Mass.Abreviation}/{Volume.Abreviation}³";

        /// <returns></returns>
        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString() => $"{Value}{Mass.Abreviation}/{Volume.Abreviation}³";
    }
}
