﻿/*******************************************************************************
* Author    :  Angus Johnson                                                   *
* Version   :  10.0 (beta)                                                     *
* Date      :  12 November 2017                                                 *
* Website   :  http://www.angusj.com                                           *
* Copyright :  Angus Johnson 2010-2017                                         *
* Purpose   :  Base clipping module                                            *
* License   :  http://www.boost.org/LICENSE_1_0.txt                            *
*******************************************************************************/

using System.Runtime.CompilerServices;

namespace Engine.Experimental;

/// <summary>
/// The intersect node struct.
/// </summary>
/// <seealso cref="IComparable" />
/// <seealso cref="IComparable{T}" />
/// <seealso cref="IEquatable{T}" />
public struct IntersectNode
    : IComparable, IComparable<IntersectNode>, IEquatable<IntersectNode>
{
    #region Properties
    /// <summary>
    /// Gets or sets the point of intersection.
    /// </summary>
    /// <value>
    /// The point.
    /// </value>
    public Point2D Point { get; set; }

    /// <summary>
    /// Gets or sets the first edge.
    /// </summary>
    /// <value>
    /// The edge a.
    /// </value>
    public Edge EdgeA { get; set; }

    /// <summary>
    /// Gets or sets the second edge.
    /// </summary>
    /// <value>
    /// The edge b.
    /// </value>
    public Edge EdgeB { get; set; }
    #endregion Properties

    #region Operators
    /// <summary>
    /// The == operator compares two <see cref="IntersectNode" /> instances for exact equality.
    /// </summary>
    /// <param name="left">The first <see cref="IntersectNode" /> to compare</param>
    /// <param name="right">The second <see cref="IntersectNode" /> to compare</param>
    /// <returns>
    /// Returns a boolean value indicating whether the two <see cref="IntersectNode" /> instances are exactly equal.
    /// The return value is true if they are equal, false otherwise.
    /// </returns>
    /// <remarks>
    /// <para>Note that double values can acquire error when operated upon, such that
    /// an exact comparison between two values which are logically equal may fail.
    /// Furthermore, using this equality operator, Double.NaN is not equal to itself.</para>
    /// </remarks>
    public static bool operator ==(IntersectNode left, IntersectNode right) => left.Equals(right);

    /// <summary>
    /// The != operator compares two <see cref="IntersectNode" /> instances for exact inequality.
    /// </summary>
    /// <param name="left">The first <see cref="IntersectNode" /> to compare</param>
    /// <param name="right">The second <see cref="IntersectNode" /> to compare</param>
    /// <returns>
    /// Returns a boolean value indicating whether the two <see cref="IntersectNode" /> instances are exactly unequal.
    /// The return value is true if they are unequal, false otherwise.
    /// </returns>
    /// <remarks>
    /// <para>Note that double values can acquire error when operated upon, such that
    /// an exact comparison between two values which are logically equal may fail.
    /// Furthermore, using this equality operator, Double.NaN is not equal to itself.</para>
    /// </remarks>
    public static bool operator !=(IntersectNode left, IntersectNode right) => !left.Equals(right);

    /// <summary>
    /// The operator &lt; returns a value that indicates whether a specified <see cref="IntersectNode" /> value
    /// is less than another specified <see cref="IntersectNode" /> value.
    /// </summary>
    /// <param name="left">The first <see cref="IntersectNode" /> to compare.</param>
    /// <param name="right">The second <see cref="IntersectNode" /> to compare.</param>
    /// <returns>
    /// Returns a boolean value indicating true if left is less than right; otherwise, false.
    /// </returns>
    public static bool operator <(IntersectNode left, IntersectNode right) => left.CompareTo(right) < 0;

    /// <summary>
    /// The operator &gt; returns a value that indicates whether a specified <see cref="IntersectNode" /> value
    /// is greater than another specified <see cref="IntersectNode" /> value.
    /// </summary>
    /// <param name="left">The first <see cref="IntersectNode" /> to compare.</param>
    /// <param name="right">The second <see cref="IntersectNode" /> to compare.</param>
    /// <returns>
    /// Returns a boolean value indicating true if left is greater than right; otherwise, false.
    /// </returns>
    public static bool operator >(IntersectNode left, IntersectNode right) => left.CompareTo(right) > 0;

    /// <summary>
    /// The &lt;= operator returns a value that indicates whether a specified <see cref="IntersectNode" /> value
    /// is less than or equal to another specified <see cref="IntersectNode" /> value.
    /// </summary>
    /// <param name="left">The first <see cref="IntersectNode" /> to compare.</param>
    /// <param name="right">The second <see cref="IntersectNode" /> to compare.</param>
    /// <returns>
    /// Returns a boolean value indicating true if left is less than or equal to right; otherwise, false.
    /// </returns>
    public static bool operator <=(IntersectNode left, IntersectNode right) => left.CompareTo(right) <= 0;

    /// <summary>
    /// The &gt;= operator returns a value that indicates whether a specified <see cref="IntersectNode" /> value
    /// is greater than or equal to another specified <see cref="IntersectNode" /> value.
    /// </summary>
    /// <param name="left">The first <see cref="IntersectNode" /> to compare.</param>
    /// <param name="right">The second <see cref="IntersectNode" /> to compare.</param>
    /// <returns>
    /// Returns a boolean value indicating true if left is greater than or equal to right; otherwise, false.
    /// </returns>
    public static bool operator >=(IntersectNode left, IntersectNode right) => left.CompareTo(right) >= 0;
    #endregion Operators

    #region Methods
    /// <summary>
    /// The edges adjacent in SEL.
    /// </summary>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    public readonly bool EdgesAdjacentInSEL() => (EdgeA.nextInSEL == EdgeB) || (EdgeA.prevInSEL == EdgeB);

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>
    /// Returns a 32-bit signed integer hash code.
    /// </returns>
    public override readonly int GetHashCode() => HashCode.Combine(Point, EdgeA, EdgeB);

    /// <summary>
    /// Compares this <see cref="IntersectNode" /> with the passed in object.
    /// </summary>
    /// <param name="obj">The object to compare to this <see cref="IntersectNode" /> to.</param>
    /// <returns>
    /// A boolean value indicating whether the two <see cref="IntersectNode" /> instances are exactly unequal.
    /// The return value is true if they are unequal, false otherwise.
    /// </returns>
    /// <remarks>
    /// <para>Note that double values can acquire error when operated upon, such that
    /// an exact comparison between two values which are logically equal may fail.
    /// In this equality Double.NaN is equal to itself, unlike in numeric equality.</para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Equals(object obj) => obj is IntersectNode intersectNode && Equals(intersectNode);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly bool Equals(IntersectNode other) => CompareTo(other) == 0;

    /// <summary>
    /// Compares this <see cref="IntersectNode" /> to another object, returning a value indicating the relation.
    /// Null is considered less than any instance.
    /// If object is not of type <see cref="IntersectNode" />, this method throws an ArgumentException.
    /// </summary>
    /// <param name="other">The object to compare to this <see cref="IntersectNode" /> to.</param>
    /// <returns>
    /// Returns an <see cref="int" /> value less than zero if this <see cref="IntersectNode" /> is less than the object,
    /// zero if this <see cref="IntersectNode" /> is the same value as the object, or a value greater than zero if this
    /// <see cref="IntersectNode" /> is greater than the object.
    /// </returns>
    /// <remarks>
    /// <para>Note that double values can acquire error when operated upon, such that
    /// an exact comparison between two values which are logically equal may fail.
    /// In this equality Double.NaN is equal to itself, unlike in numeric equality.</para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly int CompareTo(object other) => other is null ? 1 : other is IntersectNode node ? CompareTo(node) : throw new ArgumentException("Object must be an IntersectNode.", nameof(other));

    /// <summary>
    /// Compares this <see cref="IntersectNode" /> to another <see cref="IntersectNode" />, returning a value indicating the relation.
    /// Null is considered less than any instance.
    /// </summary>
    /// <param name="other">The <see cref="IntersectNode" /> to compare to this <see cref="IntersectNode" /> to.</param>
    /// <returns>
    /// Returns an <see cref="int" /> value less than zero if this <see cref="IntersectNode" /> is less than the other <see cref="IntersectNode" />,
    /// zero if this <see cref="IntersectNode" /> is the same value as the other <see cref="IntersectNode" />, or a value greater than zero if this
    /// <see cref="IntersectNode" /> is greater than the other <see cref="IntersectNode" />.
    /// </returns>
    /// <remarks>
    /// <para>Note that double values can acquire error when operated upon, such that
    /// an exact comparison between two values which are logically equal may fail.
    /// In this equality Double.NaN is equal to itself, unlike in numeric equality.</para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly int CompareTo(IntersectNode other) => Compare(this, other);

    /// <summary>
    /// Compares two <see cref="IntersectNode" /> objects, returning a value indicating the relation.
    /// Null is considered less than any instance.
    /// </summary>
    /// <param name="left">The <see cref="IntersectNode" /> to compare.</param>
    /// <param name="right">The <see cref="IntersectNode" /> to compare against.</param>
    /// <returns>
    /// Returns an <see cref="int" /> value less than zero if the left <see cref="IntersectNode" /> is less than the right <see cref="IntersectNode" />,
    /// zero if the left <see cref="IntersectNode" /> is the same value as the right <see cref="IntersectNode" />, or a value greater than zero if the left
    /// <see cref="IntersectNode" /> is greater than the right <see cref="IntersectNode" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static int Compare(IntersectNode left, IntersectNode right) => right.Point.Y.CompareTo(left.Point.Y); // Soft descending sort.
    #endregion Methods
}
