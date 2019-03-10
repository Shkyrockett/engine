// <copyright file="ElectronVolt.cs" company="Shkyrockett" >
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
    /// The electron volt struct.
    /// </summary>
    public struct ElectronVolt
        : IEnergy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElectronVolt"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public ElectronVolt(double value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name
            => "Electron Volt";

        /// <summary>
        /// Gets the abreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => "eV";

        /// <param name="value"></param>
        public static implicit operator ElectronVolt(double value)
            => new ElectronVolt(value);

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
            => $"{Value} eV";
    }
}
