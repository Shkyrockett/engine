// <copyright file="VelocityAquired.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Engine
{
    /// <summary>
    /// The velocity aquired class.
    /// </summary>
    public class VelocityAquired
        : IVelocity, IEquatable<VelocityAquired>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VelocityAquired"/> class.
        /// </summary>
        /// <param name="speed">The speed.</param>
        /// <param name="direction">The direction.</param>
        public VelocityAquired(IAcceleration speed, IDirection direction)
        {
            Direction = direction;
            Acceleration = speed;
        }

        /// <summary>
        /// Gets or sets the acceleration.
        /// </summary>
        public IAcceleration Acceleration { get; set; }

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        public IDirection Direction { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public double Value => Acceleration.Value * Direction.Value;

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string Name => nameof(Velocity);

        /// <summary>
        /// Gets the abbreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abbreviation => "ft";

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(VelocityAquired left, VelocityAquired right) => EqualityComparer<VelocityAquired>.Default.Equals(left, right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(VelocityAquired left, VelocityAquired right) => !(left == right);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => Equals(obj as VelocityAquired);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.
        /// </returns>
        public bool Equals(VelocityAquired other) => other is not null && EqualityComparer<IAcceleration>.Default.Equals(Acceleration, other.Acceleration) && EqualityComparer<IDirection>.Default.Equals(Direction, other.Direction);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode() => HashCode.Combine(Acceleration, Direction);

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"{Value} {Acceleration}{Direction}";
    }
}
