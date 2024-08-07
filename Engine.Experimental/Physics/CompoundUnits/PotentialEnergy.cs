﻿// <copyright file="PotentialEnergy.cs" company="Shkyrockett" >
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
/// The potential energy struct.
/// </summary>
/// <seealso cref="IEnergy{T}" />
/// <seealso cref="IEquatable{T}" />
public struct PotentialEnergy<T>
    : IEnergy<T>, IEquatable<PotentialEnergy<T>>
    where T : INumber<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PotentialEnergy{T}" /> class.
    /// </summary>
    /// <param name="height">The height.</param>
    /// <param name="weight">The weight.</param>
    public PotentialEnergy(ILength<T> height, IMass<T> weight)
    {
        Height = height;
        Weight = weight;
    }

    /// <summary>
    /// Gets or sets the height.
    /// </summary>
    /// <value>
    /// The height.
    /// </value>
    public ILength<T> Height { get; set; }

    /// <summary>
    /// Gets or sets the weight.
    /// </summary>
    /// <value>
    /// The weight.
    /// </value>
    public IMass<T> Weight { get; set; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <value>
    /// The value.
    /// </value>
    public readonly T Value => Weight.Value * Height.Value;

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static string Name => "Potential Energy";

    /// <summary>
    /// Gets the abbreviation.
    /// </summary>
    /// <value>
    /// The abbreviation.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly string Abbreviation => $"{Value}{Weight.Abbreviation}";

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(PotentialEnergy<T> left, PotentialEnergy<T> right) => left.Equals(right);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(PotentialEnergy<T> left, PotentialEnergy<T> right) => !(left == right);

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override readonly bool Equals(object? obj) => obj is PotentialEnergy<T> energy && Equals(energy);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public readonly bool Equals([AllowNull] PotentialEnergy<T> other) => EqualityComparer<ILength<T>>.Default.Equals(Height, other.Height) && EqualityComparer<IMass<T>>.Default.Equals(Weight, other.Weight);

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override readonly int GetHashCode() => HashCode.Combine(Height, Weight);

    /// <summary>
    /// The to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override readonly string ToString() => $"{Value} {Weight.Abbreviation}{Height.Abbreviation}";
}
