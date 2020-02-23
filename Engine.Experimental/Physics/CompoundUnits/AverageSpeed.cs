// <copyright file="AverageSpeed.cs" company="Shkyrockett" >
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
    /// The average speed struct.
    /// </summary>
    /// <seealso cref="ISpeed" />
    /// <seealso cref="IEquatable{T}" />
    public struct AverageSpeed
        : ISpeed, IEquatable<AverageSpeed>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AverageSpeed" /> class.
        /// </summary>
        /// <param name="speed">The speed.</param>
        /// <param name="time">The time.</param>
        public AverageSpeed(List<ISpeed> speed, ITime time)
        {
            Speed = speed;
            Time = time;
        }

        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        /// <value>
        /// The speed.
        /// </value>
        public List<ISpeed> Speed { get; set; }

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
        public double Value
        {
            get
            {
                double rSpeed = 0;
                foreach (var cSpeed in Speed)
                {
                    rSpeed += cSpeed.Value;
                }

                return rSpeed / Time.Value;
            }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string Name => "Average Speed";

        /// <summary>
        /// Gets the abbreviation.
        /// </summary>
        /// <value>
        /// The abbreviation.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abbreviation => $"∆{Value}/{Speed[0].Abbreviation}";

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(AverageSpeed left, AverageSpeed right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(AverageSpeed left, AverageSpeed right) => !(left == right);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => obj is AverageSpeed speed && Equals(speed);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(AverageSpeed other) => EqualityComparer<List<ISpeed>>.Default.Equals(Speed, other.Speed) && EqualityComparer<ITime>.Default.Equals(Time, other.Time);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = 1732708644;
            hashCode = hashCode * -1521134295 + EqualityComparer<List<ISpeed>>.Default.GetHashCode(Speed);
            hashCode = hashCode * -1521134295 + EqualityComparer<ITime>.Default.GetHashCode(Time);
            return hashCode;
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"{Value} ∆{Speed[0].Abbreviation}/{Time.Abbreviation}";
    }
}
