// <copyright file="AverageSpeed.cs" company="Shkyrockett" >
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
/// The average speed struct.
/// </summary>
/// <seealso cref="ISpeed" />
/// <seealso cref="IEquatable{T}" />
public struct AverageSpeed<T>
    : ISpeed<T>, IEquatable<AverageSpeed<T>>
    where T : INumber<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AverageSpeed{T}" /> class.
    /// </summary>
    /// <param name="speed">The speed.</param>
    /// <param name="time">The time.</param>
    public AverageSpeed(List<ISpeed<T>> speed, ITime<T> time)
    {
        Speed = speed;
        Time = time;
    }

    /// <summary>
    /// Gets or sets the speed.
    /// </summary>
    /// <value>
    /// The speed.
    /// </value>
    public List<ISpeed<T>> Speed { get; set; }

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
    {
        get
        {
            T rSpeed = T.Zero;
            foreach (var cSpeed in Speed)
            {
                rSpeed += cSpeed.Value;
            }

            return rSpeed / Time.Value;
        }
    }

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static string Name => "Average Speed";

    /// <summary>
    /// Gets the abbreviation.
    /// </summary>
    /// <value>
    /// The abbreviation.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly string Abbreviation => $"∆{Value}/{Speed[0].Abbreviation}";

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(AverageSpeed<T> left, AverageSpeed<T> right) => left.Equals(right);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(AverageSpeed<T> left, AverageSpeed<T> right) => !(left == right);

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override readonly bool Equals(object obj) => obj is AverageSpeed<T> speed && Equals(speed);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public readonly bool Equals([AllowNull] AverageSpeed<T> other) => EqualityComparer<List<ISpeed<T>>>.Default.Equals(Speed, other.Speed) && EqualityComparer<ITime<T>>.Default.Equals(Time, other.Time);

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override readonly int GetHashCode() => HashCode.Combine(Speed, Time);

    /// <summary>
    /// The to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override readonly string ToString() => $"{Value} ∆{Speed[0].Abbreviation}/{Time.Abbreviation}";
}
