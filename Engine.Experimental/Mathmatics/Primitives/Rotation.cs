﻿// <copyright file="Rotation.cs" company="Shkyrockett" >
// Copyright © 2017 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Diagnostics.CodeAnalysis;

namespace Engine;

/// <summary>
/// The rotation struct.
/// </summary>
/// <seealso cref="IEquatable{T}" />
public struct Rotation
    : IEquatable<Rotation>
{
    /// <summary>
    /// The radians.
    /// </summary>
    private double radians;

    /// <summary>
    /// The cos.
    /// </summary>
    private double? cos;

    /// <summary>
    /// The sin.
    /// </summary>
    private double? sin;

    /// <summary>
    /// Initializes a new instance of the <see cref="Rotation" /> class.
    /// </summary>
    /// <param name="radians">The radians.</param>
    public Rotation(double radians)
    {
        this.radians = radians;
        cos = null;
        sin = null;
    }

    /// <summary>
    /// Gets or sets the radians.
    /// </summary>
    /// <value>
    /// The radians.
    /// </value>
    public double Radians
    {
        readonly get { return radians; }
        set
        {
            radians = value;
            cos = null;
            sin = null;
        }
    }

    /// <summary>
    /// Gets or sets the degrees.
    /// </summary>
    /// <value>
    /// The degrees.
    /// </value>
    public double Degrees
    {
        readonly get { return radians.RadiansToDegrees(); }
        set
        {
            radians = value.DegreesToRadians();
            cos = null;
            sin = null;
        }
    }

    /// <summary>
    /// Gets the cosine.
    /// </summary>
    public double Cosine
        => cos ??= Math.Sin(radians);

    /// <summary>
    /// Gets the sine.
    /// </summary>
    public double Sine
        => sin ??= Math.Sin(radians);

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(Rotation left, Rotation right) => left.Equals(right);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(Rotation left, Rotation right) => !(left == right);

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override readonly bool Equals(object obj) => obj is Rotation rotation && Equals(rotation);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public readonly bool Equals([AllowNull] Rotation other) => radians == other.radians;

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override readonly int GetHashCode() => HashCode.Combine(radians);
}
