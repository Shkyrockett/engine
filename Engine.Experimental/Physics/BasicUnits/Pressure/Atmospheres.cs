// <copyright file="Atmospheres.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

namespace Engine.Physics
{
    using System.ComponentModel;

    /// <summary>
    /// The atmospheres struct.
    /// </summary>
    public struct Atmospheres
        : IPressure
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Atmospheres"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Atmospheres(double value)
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
            => nameof(Atmospheres);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Atmospheres(double value)
            => new Atmospheres(value);

        /// <returns></returns>
        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
            => $"{Value} atm";
    }
}
