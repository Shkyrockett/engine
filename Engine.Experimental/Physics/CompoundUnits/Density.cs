// <copyright file="Density.cs" company="Shkyrockett" >
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
    /// The density struct.
    /// </summary>
    public struct Density : IEquatable<Density>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Density"/> class.
        /// </summary>
        /// <param name="mass">The mass.</param>
        /// <param name="volume">The volume.</param>
        public Density(IMass mass, IVolume volume)
        {
            Mass = mass;
            Volume = volume;
        }

        /// <summary>
        /// Gets or sets the mass.
        /// </summary>
        public IMass Mass { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        public IVolume Volume { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public double Value => Mass.Value / Volume.Value;

        /// <summary>
        /// Gets the name.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string Name => nameof(Density);

        /// <summary>
        /// Gets the abbreviation.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Abbreviation => $"{Mass.Abbreviation}/{Volume.Abbreviation}³";

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Density left, Density right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Density left, Density right) => !(left == right);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => obj is Density density && Equals(density);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(Density other) => EqualityComparer<IMass>.Default.Equals(Mass, other.Mass) && EqualityComparer<IVolume>.Default.Equals(Volume, other.Volume) && Value == other.Value && Abbreviation == other.Abbreviation;

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = -2108761471;
            hashCode = hashCode * -1521134295 + EqualityComparer<IMass>.Default.GetHashCode(Mass);
            hashCode = hashCode * -1521134295 + EqualityComparer<IVolume>.Default.GetHashCode(Volume);
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Abbreviation);
            return hashCode;
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"{Value}{Mass.Abbreviation}/{Volume.Abbreviation}³";
    }
}
