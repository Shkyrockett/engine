// <copyright file="VelocityAquired.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
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

namespace Engine.Physics
{
    /// <summary>
    /// The velocity acquired class.
    /// </summary>
    public class VelocityAcquired
        : IVelocity, IEquatable<VelocityAcquired>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VelocityAcquired"/> class.
        /// </summary>
        /// <param name="speed">The speed.</param>
        /// <param name="direction">The direction.</param>
        public VelocityAcquired(IAcceleration speed, IDirection direction)
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
        public static bool operator ==(VelocityAcquired left, VelocityAcquired right) => EqualityComparer<VelocityAcquired>.Default.Equals(left, right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(VelocityAcquired left, VelocityAcquired right) => !(left == right);

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => Equals(obj as VelocityAcquired);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.
        /// </returns>
        public bool Equals(VelocityAcquired other) => other != null && EqualityComparer<IAcceleration>.Default.Equals(Acceleration, other.Acceleration) && EqualityComparer<IDirection>.Default.Equals(Direction, other.Direction);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = 2120939087;
            hashCode = hashCode * -1521134295 + EqualityComparer<IAcceleration>.Default.GetHashCode(Acceleration);
            hashCode = hashCode * -1521134295 + EqualityComparer<IDirection>.Default.GetHashCode(Direction);
            return hashCode;
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"{Value} {Acceleration}{Direction}";
    }
}
