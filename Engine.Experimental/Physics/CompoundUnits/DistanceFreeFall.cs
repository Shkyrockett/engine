// <copyright file="DistanceFreeFall.cs" company="Shkyrockett" >
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
/// The distance free fall struct.
/// </summary>
/// <seealso cref="ILength{T}" />
/// <seealso cref="IEquatable{T}" />
public struct DistanceFreeFall<T>
    : ILength<T>, IEquatable<DistanceFreeFall<T>>
    where T : INumber<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DistanceFreeFall{T}" /> class.
    /// </summary>
    /// <param name="acceleration">The acceleration.</param>
    /// <param name="time">The time.</param>
    public DistanceFreeFall(IAcceleration<T> acceleration, ITime<T> time)
    {
        Acceleration = acceleration;
        Time = time;
    }

    /// <summary>
    /// Gets or sets the acceleration.
    /// </summary>
    /// <value>
    /// The acceleration.
    /// </value>
    public IAcceleration<T> Acceleration { get; set; }

    /// <summary>
    /// Gets or sets the time.
    /// </summary>
    /// <value>
    /// The time.
    /// </value>
    public ITime<T> Time { get; set; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <value>
    /// The value.
    /// </value>
    public readonly T Value
        => Acceleration.Value * Time.Value * Time.Value;

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly string Name
        => "Instantaneous Speed";

    /// <summary>
    /// Gets the abbreviation.
    /// </summary>
    /// <value>
    /// The abbreviation.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly string Abbreviation
        => $"{Value}{Acceleration.Abbreviation}²";

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(DistanceFreeFall<T> left, DistanceFreeFall<T> right) => left.Equals(right);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(DistanceFreeFall<T> left, DistanceFreeFall<T> right) => !(left == right);

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override readonly bool Equals(object? obj) => obj is DistanceFreeFall<T> fall && Equals(fall);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public readonly bool Equals([AllowNull] DistanceFreeFall<T> other) => EqualityComparer<IAcceleration<T>>.Default.Equals(Acceleration, other.Acceleration) && EqualityComparer<ITime<T>>.Default.Equals(Time, other.Time);

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override readonly int GetHashCode() => HashCode.Combine(Acceleration, Time);

    /// <summary>
    /// The to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override readonly string ToString()
        => $"{Value} {Acceleration.Abbreviation}{Time.Abbreviation}²";
}
