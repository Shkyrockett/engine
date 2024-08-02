// <copyright file="KineticEnergy.cs" company="Shkyrockett" >
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
/// The kinetic energy struct.
/// </summary>
/// <seealso cref="IEnergy{T}" />
/// <seealso cref="IEquatable{T}" />
public struct KineticEnergy<T>
    : IEnergy<T>, IEquatable<KineticEnergy<T>>
    where T : INumber<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KineticEnergy{T}" /> class.
    /// </summary>
    /// <param name="mass">The mass.</param>
    /// <param name="velocity">The velocity.</param>
    public KineticEnergy(IMass<T> mass, ISpeed<T> velocity)
    {
        Mass = mass;
        Velocity = velocity;
    }

    /// <summary>
    /// Gets or sets the mass.
    /// </summary>
    /// <value>
    /// The mass.
    /// </value>
    public IMass<T> Mass { get; set; }

    /// <summary>
    /// Gets or sets the velocity.
    /// </summary>
    /// <value>
    /// The velocity.
    /// </value>
    public ISpeed<T> Velocity { get; set; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <value>
    /// The value.
    /// </value>
    public readonly T Value => T.CreateSaturating(0.5) * (Mass.Value * Velocity.Value * Velocity.Value);

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static string Name => "Kinetic energy";

    /// <summary>
    /// Gets the abbreviation.
    /// </summary>
    /// <value>
    /// The abbreviation.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static string Abbreviation => "J Ke";

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(KineticEnergy<T> left, KineticEnergy<T> right) => left.Equals(right);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(KineticEnergy<T> left, KineticEnergy<T> right) => !(left == right);

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override readonly bool Equals(object? obj) => obj is KineticEnergy<T> energy && Equals(energy);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public readonly bool Equals([AllowNull] KineticEnergy<T> other) => EqualityComparer<IMass<T>>.Default.Equals(Mass, other.Mass) && EqualityComparer<ISpeed<T>>.Default.Equals(Velocity, other.Velocity);

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override readonly int GetHashCode() => HashCode.Combine(Mass, Velocity);

    /// <summary>
    /// The to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override readonly string ToString() => $"{Value} J Ke";
}
