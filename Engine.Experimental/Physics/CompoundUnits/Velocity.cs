// <copyright file="Velocity.cs" company="Shkyrockett" >
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
    /// The velocity class.
    /// </summary>
    public class Velocity
        : IVelocity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Velocity"/> class.
        /// </summary>
        /// <param name="speed">The speed.</param>
        /// <param name="direction">The direction.</param>
        public Velocity(ISpeed speed, IDirection direction)
        {
            Direction = direction;
            Acceleration = speed;
        }

        /// <summary>
        /// Gets or sets the acceleration.
        /// </summary>
        public ISpeed Acceleration { get; set; }

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        public IDirection Direction { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public double Value
            => Acceleration.Value * Direction.Value;

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Name
            => nameof(Velocity);

        /// <summary>
        /// Gets the abreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abreviation
            => $"{Acceleration.Abreviation} {Direction.Abreviation}";

        /// <returns></returns>
        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString() => $"{Value} {Abreviation}";
    }
}
