// <copyright file="WeightDensity.cs" company="Shkyrockett" >
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
/// The weight density struct.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="System.IEquatable{T}" />
public struct WeightDensity<T>
    : IEquatable<WeightDensity<T>>
    where T : INumber<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WeightDensity{T}"/> class.
    /// </summary>
    /// <param name="weight">The weight.</param>
    /// <param name="volume">The volume.</param>
    public WeightDensity(IMass<T> weight, IVolume<T> volume)
    {
        Weight = weight;
        Volume = volume;
    }

    /// <summary>
    /// Gets or sets the weight.
    /// </summary>
    public IMass<T> Weight { get; set; }

    /// <summary>
    /// Gets or sets the volume.
    /// </summary>
    public IVolume<T> Volume { get; set; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    public readonly T Value => Weight.Value / Volume.Value;

    /// <summary>
    /// Gets the name.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static string Name => "Weight Density";

    /// <summary>
    /// Gets the abbreviation.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly string Abbreviation => $"{Weight.Abbreviation}/{Volume.Abbreviation}³";

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(WeightDensity<T> left, WeightDensity<T> right) => left.Equals(right);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(WeightDensity<T> left, WeightDensity<T> right) => !(left == right);

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override readonly bool Equals(object? obj) => obj is WeightDensity<T> density && Equals(density);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public readonly bool Equals([AllowNull] WeightDensity<T> other) => EqualityComparer<IMass<T>>.Default.Equals(Weight, other.Weight) && EqualityComparer<IVolume<T>>.Default.Equals(Volume, other.Volume);

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override readonly int GetHashCode() => HashCode.Combine(Weight, Volume);


    /// <summary>
    /// The to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString() => $"{Value}{Weight.Abbreviation}/{Volume.Abbreviation}³";
}
