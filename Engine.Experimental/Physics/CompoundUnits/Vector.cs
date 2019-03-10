// <copyright file="Vector.cs" company="Shkyrockett" >
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
    /// The vector struct.
    /// </summary>
    public struct Vector
        : IVector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector"/> class.
        /// </summary>
        /// <param name="magnitude">The magnitude.</param>
        /// <param name="direction">The direction.</param>
        public Vector(double magnitude, double direction)
        {
            Magnitude = magnitude;
            Direction = direction;
        }

        /// <summary>
        /// Gets or sets the magnitude.
        /// </summary>
        public double Magnitude { get; set; }

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        public double Direction { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public double Value
            => Magnitude * Direction;

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name
            => nameof(Vector);

        /// <summary>
        /// Gets the abreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => string.Empty;

        /// <returns></returns>
        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
            => $"|{Value}|";
    }
}
