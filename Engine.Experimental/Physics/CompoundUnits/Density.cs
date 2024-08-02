// <copyright file="Density.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace Engine;

/// <summary>
/// The density struct.
/// </summary>
public struct Density<T>
    : IEquatable<Density<T>>
    where T : INumber<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Density{T}"/> class.
    /// </summary>
    /// <param name="mass">The mass.</param>
    /// <param name="volume">The volume.</param>
    public Density(IMass<T> mass, IVolume<T> volume)
    {
        Mass = mass;
        Volume = volume;
    }

    /// <summary>
    /// Gets or sets the mass.
    /// </summary>
    public IMass<T> Mass { get; set; }

    /// <summary>
    /// Gets or sets the volume.
    /// </summary>
    public IVolume<T> Volume { get; set; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    public readonly T Value => Mass.Value / Volume.Value;

    /// <summary>
    /// Gets the name.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static string Name => nameof(Density<T>);

    /// <summary>
    /// Gets the abbreviation.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly string Abbreviation => $"{Mass.Abbreviation}/{Volume.Abbreviation}³";

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(Density<T> left, Density<T> right) => left.Equals(right);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(Density<T> left, Density<T> right) => !(left == right);

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override readonly bool Equals(object? obj) => obj is Density<T> density && Equals(density);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public readonly bool Equals([AllowNull] Density<T> other) => EqualityComparer<IMass<T>>.Default.Equals(Mass, other.Mass) && EqualityComparer<IVolume<T>>.Default.Equals(Volume, other.Volume);

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override readonly int GetHashCode() => HashCode.Combine(Mass, Volume);

    /// <summary>
    /// The to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override readonly string ToString() => $"{Value}{Mass.Abbreviation}/{Volume.Abbreviation}³";
}
