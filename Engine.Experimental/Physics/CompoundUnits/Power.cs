// <copyright file="Power.cs" company="Shkyrockett" >
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
/// The power struct.
/// </summary>
/// <seealso cref="IPower{T}" />
/// <seealso cref="IEquatable{T}" />
public struct Power<T>
    : IPower<T>, IEquatable<Power<T>>
    where T : INumber<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Power{T}" /> class.
    /// </summary>
    /// <param name="work">The work.</param>
    /// <param name="time">The time.</param>
    public Power(IEnergy<T> work, ITime<T> time)
    {
        Work = work;
        Time = time;
    }

    /// <summary>
    /// Gets or sets the work.
    /// </summary>
    /// <value>
    /// The work.
    /// </value>
    public IEnergy<T> Work { get; set; }

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
    public readonly T Value => Work.Value / Time.Value;

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static string Name => nameof(Power<T>);

    /// <summary>
    /// Gets the abbreviation.
    /// </summary>
    /// <value>
    /// The abbreviation.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly string Abbreviation => "J";

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(Power<T> left, Power<T> right) => left.Equals(right);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(Power<T> left, Power<T> right) => !(left == right);

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override readonly bool Equals(object? obj) => obj is Power<T> power && Equals(power);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public readonly bool Equals([AllowNull] Power<T> other) => EqualityComparer<IEnergy<T>>.Default.Equals(Work, other.Work) && EqualityComparer<ITime<T>>.Default.Equals(Time, other.Time);

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override readonly int GetHashCode() => HashCode.Combine(Work, Time);

    /// <summary>
    /// The to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString() => $"{Value}";
}
