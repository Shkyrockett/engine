// <copyright file="VelocityFreeFall.cs" company="Shkyrockett" >
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
    /// The velocity free fall class.
    /// </summary>
    public class VelocityFreeFall
        : ISpeed, IEquatable<VelocityFreeFall>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VelocityFreeFall"/> class.
        /// </summary>
        /// <param name="gravity">The gravity.</param>
        /// <param name="time">The time.</param>
        public VelocityFreeFall(IAcceleration gravity, ITime time)
        {
            Gravity = gravity;
            Time = time;
        }

        /// <summary>
        /// Gets or sets the gravity.
        /// </summary>
        public IAcceleration Gravity { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public ITime Time { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public double Value => Gravity.Value * Time.Value;

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string Name => "Velocity at free fall";

        /// <summary>
        /// Gets the abbreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abbreviation => $"{Value}{Gravity.Abbreviation}";

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(VelocityFreeFall left, VelocityFreeFall right) => EqualityComparer<VelocityFreeFall>.Default.Equals(left, right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(VelocityFreeFall left, VelocityFreeFall right) => !(left == right);

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => Equals(obj as VelocityFreeFall);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.
        /// </returns>
        public bool Equals(VelocityFreeFall other) => other != null && EqualityComparer<IAcceleration>.Default.Equals(Gravity, other.Gravity) && EqualityComparer<ITime>.Default.Equals(Time, other.Time);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = -541537103;
            hashCode = hashCode * -1521134295 + EqualityComparer<IAcceleration>.Default.GetHashCode(Gravity);
            hashCode = hashCode * -1521134295 + EqualityComparer<ITime>.Default.GetHashCode(Time);
            return hashCode;
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"{Value} {Time.Abbreviation}{Time.Abbreviation}";
    }
}
