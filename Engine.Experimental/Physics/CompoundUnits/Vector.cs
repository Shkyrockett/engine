// <copyright file="Vector.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;

namespace Engine.Physics
{
    /// <summary>
    /// The vector struct.
    /// </summary>
    public struct Vector
        : IVector, IEquatable<Vector>
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
        public static string Name
            => nameof(Vector);

        /// <summary>
        /// Gets the abbreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string Abbreviation
            => string.Empty;

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Vector left, Vector right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Vector left, Vector right) => !(left == right);

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => obj is Vector vector && Equals(vector);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.
        /// </returns>
        public bool Equals(Vector other) => Magnitude == other.Magnitude && Direction == other.Direction;

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = -1720283545;
            hashCode = hashCode * -1521134295 + Magnitude.GetHashCode();
            hashCode = hashCode * -1521134295 + Direction.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
            => $"|{Value}|";
    }
}
