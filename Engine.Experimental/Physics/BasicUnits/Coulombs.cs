// <copyright file="Coulombs.cs" company="Shkyrockett" >
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
    /// Unit of Charge
    /// </summary>
    public struct Coulombs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Coulombs"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Coulombs(double value)
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
            => nameof(Coulombs);

        /// <param name="value"></param>
        public static implicit operator Coulombs(double value)
            => new Coulombs(value);

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
            => $"{Value} C";
    }
}
