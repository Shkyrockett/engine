// <copyright file="Vector.cs" company="Shkyrockett" >
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
/// The vector struct.
/// </summary>
/// <seealso cref="IVector{T}" />
/// <seealso cref="IEquatable{T}" />
public struct Vector<T>
    : IVector<T>, IEquatable<Vector<T>>
    where T : INumber<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Vector{T}" /> class.
    /// </summary>
    /// <param name="magnitude">The magnitude.</param>
    /// <param name="direction">The direction.</param>
    public Vector(T magnitude, T direction)
    {
        Magnitude = magnitude;
        Direction = direction;
    }

    /// <summary>
    /// Gets or sets the magnitude.
    /// </summary>
    /// <value>
    /// The magnitude.
    /// </value>
    public T Magnitude { get; set; }

    /// <summary>
    /// Gets or sets the direction.
    /// </summary>
    /// <value>
    /// The direction.
    /// </value>
    public T Direction { get; set; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <value>
    /// The value.
    /// </value>
    public readonly T Value
        => Magnitude * Direction;

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static string Name
        => nameof(Vector<T>);

    /// <summary>
    /// Gets the abbreviation.
    /// </summary>
    /// <value>
    /// The abbreviation.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static string Abbreviation
        => string.Empty;

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(Vector<T> left, Vector<T> right) => left.Equals(right);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(Vector<T> left, Vector<T> right) => !(left == right);

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override readonly bool Equals(object? obj) => obj is Vector<T> vector && Equals(vector);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public readonly bool Equals([AllowNull] Vector<T> other) => Magnitude == other.Magnitude && Direction == other.Direction;

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override readonly int GetHashCode() => HashCode.Combine(Magnitude, Direction);

    /// <summary>
    /// The to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override readonly string ToString()
        => $"|{Value}|";
}
