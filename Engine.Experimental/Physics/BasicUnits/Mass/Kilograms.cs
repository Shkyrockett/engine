// <copyright file="Kilograms.cs" company="Shkyrockett" >
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
/// The kilograms struct.
/// </summary>
/// <seealso cref="IMass{T}" />
/// <seealso cref="IEquatable{T}" />
public struct Kilograms<T>
    : IMass<T>, IEquatable<Kilograms<T>>
    where   T : INumber<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Kilograms{T}" /> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public Kilograms(T value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>
    /// The value.
    /// </value>
    public T Value { get; set; }

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static string Name
        => nameof(Kilograms<T>);

    /// <summary>
    /// Gets the abbreviation.
    /// </summary>
    /// <value>
    /// The abbreviation.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly string Abbreviation
        => "Kg";

    /// <summary>
    /// Performs an implicit conversion from <see cref="T"/> to <see cref="Kilograms{T}"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static implicit operator Kilograms<T>(T value)
        => new(value);

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(Kilograms<T> left, Kilograms<T> right) => left.Equals(right);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(Kilograms<T> left, Kilograms<T> right) => !(left == right);

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override readonly bool Equals(object? obj) => obj is Kilograms<T> kilograms && Equals(kilograms);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public readonly bool Equals([AllowNull] Kilograms<T> other) => Value == other.Value;

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override readonly int GetHashCode() => HashCode.Combine(Value);

    /// <summary>
    /// The to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override readonly string ToString()
        => $"{Value} kg";
}
