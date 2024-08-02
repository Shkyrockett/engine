// <copyright file="Hours.cs" company="Shkyrockett" >
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
/// The hours struct.
/// </summary>
/// <seealso cref="ITime{T}" />
/// <seealso cref="IEquatable{T}" />
public struct Hours<T>
    : ITime<T>, IEquatable<Hours<T>>
    where T : INumber<T>
{
    /// <summary>
    /// The second (const). Value: 3600d.
    /// </summary>
    public static readonly T Second = T.CreateSaturating(3600);

    /// <summary>
    /// The minute (const). Value: 60d.
    /// </summary>
    public static readonly T Minute = T.CreateSaturating(60);

    /// <summary>
    /// The hour (const). Value: 1d.
    /// </summary>
    public static readonly T Hour = T.One;

    /// <summary>
    /// The day (const). Value: 24d.
    /// </summary>
    public static readonly T Day = T.CreateSaturating(24d);

    /// <summary>
    /// The year (const). Value: 365.25d * Day.
    /// </summary>
    public static readonly T Year = T.CreateSaturating(365.25) * Day;

    /// <summary>
    /// Initializes a new instance of the <see cref="Hours" /> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public Hours(T value)
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
    /// Gets or sets the seconds.
    /// </summary>
    /// <value>
    /// The seconds.
    /// </value>
    public T Seconds
    {
        readonly get { return Value * Second; }
        set { Value = value / Second; }
    }

    /// <summary>
    /// Gets or sets the minutes.
    /// </summary>
    /// <value>
    /// The minutes.
    /// </value>
    public T Minutes
    {
        readonly get { return Value * Minute; }
        set { Value = value / Minute; }
    }

    /// <summary>
    /// Gets or sets the days.
    /// </summary>
    /// <value>
    /// The days.
    /// </value>
    public T Days
    {
        readonly get { return Value * Day; }
        set { Value = value / Day; }
    }

    /// <summary>
    /// Gets or sets the years.
    /// </summary>
    /// <value>
    /// The years.
    /// </value>
    public T Years
    {
        readonly get { return Value * Year; }
        set { Value = value / Year; }
    }

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static string Name => nameof(Hours<T>);

    /// <summary>
    /// Gets the abbreviation.
    /// </summary>
    /// <value>
    /// The abbreviation.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly string Abbreviation => "h";

    /// <summary>
    /// Performs an implicit conversion from <see cref="double"/> to <see cref="Hours{T}"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static implicit operator Hours<T>(T value) => new(value);

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(Hours<T> left, Hours<T> right) => left.Equals(right);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(Hours<T> left, Hours<T> right) => !(left == right);

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override readonly bool Equals(object obj) => obj is Hours<T> hours && Equals(hours);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public readonly bool Equals([AllowNull] Hours<T> other) => Value == other.Value;

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
    public override readonly string ToString() => $"{Value} h";
}
