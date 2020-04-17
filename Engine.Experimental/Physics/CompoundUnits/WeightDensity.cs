// <copyright file="WeightDensity.cs" company="Shkyrockett" >
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
    /// The weight density struct.
    /// </summary>
    public struct WeightDensity
        : IEquatable<WeightDensity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeightDensity"/> class.
        /// </summary>
        /// <param name="weight">The weight.</param>
        /// <param name="volume">The volume.</param>
        public WeightDensity(IMass weight, IVolume volume)
        {
            Weight = weight;
            Volume = volume;
        }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        public IMass Weight { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        public IVolume Volume { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public double Value => Weight.Value / Volume.Value;

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string Name => "Weight Density";

        /// <summary>
        /// Gets the abbreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abbreviation => $"{Weight.Abbreviation}/{Volume.Abbreviation}³";

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(WeightDensity left, WeightDensity right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(WeightDensity left, WeightDensity right) => !(left == right);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => obj is WeightDensity density && Equals(density);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(WeightDensity other) => EqualityComparer<IMass>.Default.Equals(Weight, other.Weight) && EqualityComparer<IVolume>.Default.Equals(Volume, other.Volume);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = -1113466256;
            hashCode = hashCode * -1521134295 + EqualityComparer<IMass>.Default.GetHashCode(Weight);
            hashCode = hashCode * -1521134295 + EqualityComparer<IVolume>.Default.GetHashCode(Volume);
            return hashCode;
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"{Value}{Weight.Abbreviation}/{Volume.Abbreviation}³";
    }
}
