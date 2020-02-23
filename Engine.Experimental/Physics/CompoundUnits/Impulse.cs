// <copyright file="Impulse.cs" company="Shkyrockett" >
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

namespace Engine
{
    /// <summary>
    /// The impulse struct.
    /// </summary>
    /// <seealso cref="IForce" />
    /// <seealso cref="IEquatable{T}" />
    public struct Impulse
        : IForce, IEquatable<Impulse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Impulse" /> class.
        /// </summary>
        /// <param name="force">The force.</param>
        /// <param name="time">The time.</param>
        public Impulse(IForce force, ITime time)
        {
            Force = force;
            Time = time;
        }

        /// <summary>
        /// Gets or sets the force.
        /// </summary>
        /// <value>
        /// The force.
        /// </value>
        public IForce Force { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public ITime Time { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public double Value => Force.Value * Time.Value;

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string Name => "Instantaneous Speed";

        /// <summary>
        /// Gets the abbreviation.
        /// </summary>
        /// <value>
        /// The abbreviation.
        /// </value>
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
        public static bool operator ==(Impulse left, Impulse right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Impulse left, Impulse right) => !(left == right);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => obj is Impulse impulse && Equals(impulse);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(Impulse other) => EqualityComparer<IForce>.Default.Equals(Force, other.Force) && EqualityComparer<ITime>.Default.Equals(Time, other.Time);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = -1340938296;
            hashCode = hashCode * -1521134295 + EqualityComparer<IForce>.Default.GetHashCode(Force);
            hashCode = hashCode * -1521134295 + EqualityComparer<ITime>.Default.GetHashCode(Time);
            return hashCode;
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"{Value} {Force.Abbreviation}{Time.Abbreviation}";
    }
}
