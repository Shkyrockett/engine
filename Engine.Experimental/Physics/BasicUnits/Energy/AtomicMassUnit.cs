// <copyright file="AtomicMassUnit.cs" company="Shkyrockett" >
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
    /// The atomic mass unit struct.
    /// </summary>
    public struct AtomicMassUnit
        : IEnergy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AtomicMassUnit"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public AtomicMassUnit(double value)
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
            => "Atomic Mass Unit";

        /// <summary>
        /// Gets the abreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => "amu";

        /// <param name="value"></param>
        public static implicit operator AtomicMassUnit(double value)
            => new AtomicMassUnit(value);

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString() => $"{Value} amu";
    }
}
