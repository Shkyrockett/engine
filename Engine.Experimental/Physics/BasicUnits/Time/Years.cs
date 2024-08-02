// <copyright file="Years.cs" company="Shkyrockett" >
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
/// The years struct.
/// </summary>
/// <seealso cref="ITime{T}" />
/// <seealso cref="IEquatable{T}" />
public struct Years<T>
    : ITime<T>, IEquatable<Years<T>>
    where T : INumber<T>
{
    /// <summary>
    /// The second (const). Value: 31557600d.
    /// </summary>
    public static readonly T Second = T.CreateSaturating(31557600);

    /// <summary>
    /// The minute (const). Value: 525960d.
    /// </summary>
    public static readonly T Minute = T.CreateSaturating(525960);

    /// <summary>
    /// The hour (const). Value: 8766d.
    /// </summary>
    public static readonly T Hour = T.CreateSaturating(8766);

    /// <summary>
    /// The day (const). Value: 365.25d.
    /// </summary>
    public static readonly T Day = T.CreateSaturating(365.25);

    /// <summary>
    /// The year (const). Value: 1d.
    /// </summary>
    public static readonly T Year = T.One;

    /// <summary>
    /// Initializes a new instance of the <see cref="Years" /> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public Years(T value)
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
    /// Gets or sets the hours.
    /// </summary>
    /// <value>
    /// The hours.
    /// </value>
    public T Hours
    {
        readonly get { return Value * Hour; }
        set { Value = value / Hour; }
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
    /// Performs an implicit conversion from <see cref="double"/> to <see cref="Years"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static implicit operator Years<T>(T value) => new(value);

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(Years<T> left, Years<T> right) => left.Equals(right);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(Years<T> left, Years<T> right) => !(left == right);

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static string Name => nameof(Years<T>);

    /// <summary>
    /// Gets the abbreviation.
    /// </summary>
    /// <value>
    /// The abbreviation.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly string Abbreviation => "years";

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override readonly bool Equals(object obj) => obj is Years<T> years && Equals(years);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public readonly bool Equals([AllowNull] Years<T> other) => other is Years<T> years && Value == years.Value;

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
    public override readonly string ToString() => $"{Value} years";
}
