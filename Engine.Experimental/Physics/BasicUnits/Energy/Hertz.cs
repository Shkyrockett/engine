﻿// <copyright file="Hertz.cs" company="Shkyrockett" >
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
    /// The hertz struct.
    /// </summary>
    public struct Hertz
        : IEnergy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Hertz"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Hertz(double value)
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
            => nameof(Hertz);

        /// <summary>
        /// Gets the abreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => "Hz";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Hertz(double value)
            => new Hertz(value);

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
            => $"{Value} Hz";
    }
}
