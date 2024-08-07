﻿// <copyright file="CubicControlPoint2D.cs" company="Shkyrockett" >
// Copyright © 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Engine;

/// <summary>
/// The control point class.
/// </summary>
/// <seealso cref="IPrimitive" />
/// <seealso cref="IEquatable{T}" />
[DebuggerDisplay("{ToString()}")]
public struct CubicControlPoint2D
    : IPrimitive, IEquatable<CubicControlPoint2D>
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="CubicControlPoint" /> class.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <param name="anchorA">The anchorA.</param>
    /// <param name="anchorB">The anchorB.</param>
    /// <param name="global">if set to <see langword="true"/> [global].</param>
    public CubicControlPoint2D(Point2D point, Point2D anchorA, Point2D anchorB, bool global = false)
    {
        Point = point;
        if (global)
        {
            AnchorA = GlobalToLocal(anchorA, Point);
            AnchorB = GlobalToLocal(anchorB, Point);
        }
        else
        {
            AnchorA = anchorA;
            AnchorB = anchorB;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CubicControlPoint" /> class.
    /// </summary>
    /// <param name="point">The point.</param>
    public CubicControlPoint2D(Point2D point)
    {
        Point = point;
        AnchorA = point;
        AnchorB = point;
    }
    #endregion Constructors

    #region Properties
    /// <summary>
    /// Gets or sets the point.
    /// </summary>
    /// <value>
    /// The point.
    /// </value>
    public Point2D Point { get; set; }

    /// <summary>
    /// Gets or sets the horizontal anchor.
    /// </summary>
    /// <value>
    /// The anchor a.
    /// </value>
    public Point2D AnchorA { get; set; }

    /// <summary>
    /// Gets or sets the vertical anchor.
    /// </summary>
    /// <value>
    /// The anchor b.
    /// </value>
    public Point2D AnchorB { get; set; }

    /// <summary>
    /// Gets or sets the global horizontal anchor.
    /// </summary>
    /// <value>
    /// The anchor a global.
    /// </value>
    public Point2D AnchorAGlobal { readonly get { return LocalToGlobal(AnchorA, Point); } set { AnchorA = GlobalToLocal(value, Point); } }

    /// <summary>
    /// Gets or sets the global vertical anchor.
    /// </summary>
    /// <value>
    /// The anchor b global.
    /// </value>
    public Point2D AnchorBGlobal { readonly get { return LocalToGlobal(AnchorB, Point); } set { AnchorB = GlobalToLocal(value, Point); } }
    #endregion Properties

    #region Operators
    /// <summary>
    /// The operator ==.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    public static bool operator ==(CubicControlPoint2D left, CubicControlPoint2D right) => left.Equals(right);

    /// <summary>
    /// The operator !=.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    public static bool operator !=(CubicControlPoint2D left, CubicControlPoint2D right) => !(left == right);
    #endregion Operators

    #region Methods
    /// <summary>
    /// The local to global method.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <param name="reference">The reference.</param>
    /// <returns>
    /// The <see cref="Point2D" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static Point2D LocalToGlobal(Point2D point, Point2D reference) => new(point.X + reference.X, point.Y + reference.Y);

    /// <summary>
    /// The global to local method.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <param name="reference">The reference.</param>
    /// <returns>
    /// The <see cref="Point2D" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static Point2D GlobalToLocal(Point2D point, Point2D reference)
        => new(point.X - reference.X, point.Y - reference.Y);

    /// <summary>
    /// Get the hash code.
    /// </summary>
    /// <returns>
    /// The <see cref="int" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override readonly int GetHashCode() => HashCode.Combine(Point, AnchorA, AnchorB);

    /// <summary>
    /// The compare.
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool Compare(CubicControlPoint2D a, CubicControlPoint2D b) => Equals(a, b);

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool Equals(CubicControlPoint2D a, CubicControlPoint2D b) => (a.Point == b.Point) & (a.AnchorA == b.AnchorA) & (a.AnchorB == b.AnchorB);

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="obj">The obj.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override readonly bool Equals(object obj) => obj is CubicControlPoint2D d && Equals(this, d);

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly bool Equals(CubicControlPoint2D value) => Equals(this, value);

    /// <summary>
    /// Creates a human-readable string that represents this <see cref="CubicControlPoint2D" />.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

    /// <summary>
    /// Creates a string representation of this <see cref="CubicControlPoint" /> struct based on the IFormatProvider
    /// passed in.  If the provider is null, the CurrentCulture is used.
    /// </summary>
    /// <param name="provider">The provider.</param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public string ToString(IFormatProvider provider) => ToString("R" /* format string */, provider);

    /// <summary>
    /// Creates a string representation of this <see cref="CubicControlPoint" /> class based on the format string
    /// and IFormatProvider passed in.
    /// If the provider is null, the CurrentCulture is used.
    /// See the documentation for IFormattable for more information.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="provider">The provider.</param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public string ToString(string format, IFormatProvider provider) => ConvertToString(format /* format string */, provider /* format provider */);

    /// <summary>
    /// Creates a string representation of this <see cref="CubicControlPoint" /> class based on the format string
    /// and IFormatProvider passed in.
    /// If the provider is null, the CurrentCulture is used.
    /// See the documentation for IFormattable for more information.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="provider">The provider.</param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private readonly string ConvertToString(string format, IFormatProvider provider)
    {
        if (this == null) return nameof(CubicControlPoint2D);
        var sep = Tokenizer.GetNumericListSeparator(provider);
        return $"{nameof(CubicControlPoint2D)}{{{nameof(Point)}={Point.ToString(format, provider)}{sep}{nameof(AnchorA)}={AnchorA.ToString(format, provider)}{sep}{nameof(AnchorB)}={AnchorB.ToString(format, provider)}}}";
    }
    #endregion Methods
}
