// <copyright file="Speed.cs" company="Shkyrockett" >
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
/// The speed struct.
/// </summary>
public struct Speed<T>
    : ISpeed<T>, IEquatable<Speed<T>>
    where T : INumber<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Speed{T}"/> class.
    /// </summary>
    /// <param name="distance">The distance.</param>
    /// <param name="time">The time.</param>
    public Speed(ILength<T> distance, ITime<T> time)
    {
        Distance = distance;
        Time = time;
    }

    /// <summary>
    /// Gets or sets the distance.
    /// </summary>
    public ILength<T> Distance { get; set; }

    /// <summary>
    /// Gets or sets the time.
    /// </summary>
    public ITime<T> Time { get; set; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    public readonly T Value => Distance.Value / Time.Value;

    /// <summary>
    /// Gets the name.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static string Name => nameof(Seconds<T>);

    /// <summary>
    /// Gets the abbreviation.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly string Abbreviation => $"{Distance.Abbreviation}/{Time.Abbreviation}";

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(Speed<T> left, Speed<T> right) => left.Equals(right);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(Speed<T> left, Speed<T> right) => !(left == right);

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override readonly bool Equals(object? obj) => obj is Speed<T> speed && Equals(speed);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public readonly bool Equals([AllowNull] Speed<T> other) => EqualityComparer<ILength<T>>.Default.Equals(Distance, other.Distance) && EqualityComparer<ITime<T>>.Default.Equals(Time, other.Time);

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override readonly int GetHashCode() => HashCode.Combine(Distance, Time);

    /// <summary>
    /// The to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override readonly string ToString() => $"{Value} {Distance.Abbreviation}/{Time.Abbreviation}";
}
