// <copyright file="Work.cs" company="Shkyrockett" >
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
    /// The work struct.
    /// </summary>
    public struct Work
        : IEnergy, IEquatable<Work>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Work"/> class.
        /// </summary>
        /// <param name="force">The force.</param>
        /// <param name="distance">The distance.</param>
        public Work(IForce force, ILength distance)
        {
            Force = force;
            Distance = distance;
        }

        /// <summary>
        /// Gets or sets the force.
        /// </summary>
        public IForce Force { get; set; }

        /// <summary>
        /// Gets or sets the distance.
        /// </summary>
        public ILength Distance { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public double Value => Force.Value * Distance.Value;

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string Name => nameof(Work);

        /// <summary>
        /// Gets the abbreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abbreviation => $"{Value}{Force.Abbreviation}";

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Work left, Work right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Work left, Work right) => !(left == right);

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => obj is Work work && Equals(work);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.
        /// </returns>
        public bool Equals(Work other) => EqualityComparer<IForce>.Default.Equals(Force, other.Force) && EqualityComparer<ILength>.Default.Equals(Distance, other.Distance);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = -1543019450;
            hashCode = hashCode * -1521134295 + EqualityComparer<IForce>.Default.GetHashCode(Force);
            hashCode = hashCode * -1521134295 + EqualityComparer<ILength>.Default.GetHashCode(Distance);
            return hashCode;
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"{Value} {Force.Abbreviation}{Distance.Abbreviation}";
    }
}
