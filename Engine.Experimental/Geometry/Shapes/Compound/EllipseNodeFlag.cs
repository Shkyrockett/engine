﻿using System.Diagnostics.CodeAnalysis;

namespace Engine.Experimental;

/// <summary>
/// The ellipse node flag struct.
/// </summary>
/// <seealso cref="INodeItem" />
/// <seealso cref="IEquatable{T}" />
public struct EllipseNodeFlag
    : INodeItem, IEquatable<EllipseNodeFlag>
{
    /// <summary>
    /// Gets or sets the elements.
    /// </summary>
    /// <value>
    /// The elements.
    /// </value>
    public int Elements { get; set; }

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(EllipseNodeFlag left, EllipseNodeFlag right) => left.Equals(right);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(EllipseNodeFlag left, EllipseNodeFlag right) => !(left == right);

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override readonly bool Equals(object obj) => obj is EllipseNodeFlag flag && Equals(flag);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public readonly bool Equals([AllowNull] EllipseNodeFlag other) => Elements == other.Elements;

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override readonly int GetHashCode() => HashCode.Combine(Elements);

    /// <summary>
    /// The to string.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="formatProvider">The formatProvider.</param>
    /// <returns>
    /// The <see cref="string" />.
    /// </returns>
    public readonly string ToString(string format, IFormatProvider formatProvider) => $"{nameof(EllipseNodeFlag)}";
}
