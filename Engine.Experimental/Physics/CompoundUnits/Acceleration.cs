// <copyright file="Acceleration.cs" company="Shkyrockett" >
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
    /// The acceleration struct.
    /// </summary>
    /// <seealso cref="IAcceleration" />
    /// <seealso cref="IEquatable{T}" />
    public struct Acceleration
        : IAcceleration, IEquatable<Acceleration>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Acceleration" /> class.
        /// </summary>
        /// <param name="velocityChange">The velocityChange.</param>
        /// <param name="timeInterval">The timeInterval.</param>
        public Acceleration(IVelocity velocityChange, ITime timeInterval)
        {
            VelocityChange = velocityChange;
            TimeInterval = timeInterval;
        }

        /// <summary>
        /// Gets or sets the velocity change.
        /// </summary>
        /// <value>
        /// The velocity change.
        /// </value>
        public IVelocity VelocityChange { get; set; }

        /// <summary>
        /// Gets or sets the time interval.
        /// </summary>
        /// <value>
        /// The time interval.
        /// </value>
        public ITime TimeInterval { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public double Value
            => VelocityChange.Value / TimeInterval.Value;

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string Name
            => nameof(Acceleration);

        /// <summary>
        /// Gets the abbreviation.
        /// </summary>
        /// <value>
        /// The abbreviation.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abbreviation
            => $"∆{Value}/∆{VelocityChange.Abbreviation}";

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Acceleration left, Acceleration right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Acceleration left, Acceleration right) => !(left == right);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => obj is Acceleration acceleration && Equals(acceleration);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(Acceleration other) => EqualityComparer<IVelocity>.Default.Equals(VelocityChange, other.VelocityChange) && EqualityComparer<ITime>.Default.Equals(TimeInterval, other.TimeInterval);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = -2114175781;
            hashCode = hashCode * -1521134295 + EqualityComparer<IVelocity>.Default.GetHashCode(VelocityChange);
            hashCode = hashCode * -1521134295 + EqualityComparer<ITime>.Default.GetHashCode(TimeInterval);
            return hashCode;
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public override string ToString()
            => $"{Value} {Abbreviation}";
    }
}
