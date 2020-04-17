// <copyright file="Generators.cs" company="Shkyrockett" >
//     Copyright © 2017 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Engine.IClosedShape" />
    /// <seealso cref="System.IEquatable{Engine.PolycurveContour}" />
    public struct PolycurveContour
        : IClosedShape, IEquatable<PolycurveContour>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolycurveContour"/> struct.
        /// </summary>
        /// <param name="first">The first.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PolycurveContour(List<IShapeSegment> first)
            : this()
        {
            Curves = first;
        }

        /// <summary>
        /// Gets or sets the curves.
        /// </summary>
        /// <value>
        /// The curves.
        /// </value>
        public List<IShapeSegment> Curves { get; set; }

        /// <summary>
        /// Gets or sets the first.
        /// </summary>
        /// <value>
        /// The first.
        /// </value>
        public IShapeSegment First { get; set; }

        /// <summary>
        /// Gets or sets the last.
        /// </summary>
        /// <value>
        /// The last.
        /// </value>
        public IShapeSegment Last { get; set; }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(PolycurveContour left, PolycurveContour right) => left.Equals(right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(PolycurveContour left, PolycurveContour right) => !(left == right);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([AllowNull] object obj) => obj is PolycurveContour contour && Equals(contour);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals([AllowNull] PolycurveContour other) => EqualityComparer<List<IShapeSegment>>.Default.Equals(Curves, other.Curves) && EqualityComparer<IShapeSegment>.Default.Equals(First, other.First) && EqualityComparer<IShapeSegment>.Default.Equals(Last, other.Last);

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(Curves, First, Last);

        /// <summary>
        /// Adds the cubic beziers.
        /// </summary>
        /// <param name="last">The last.</param>
        /// <exception cref="NotImplementedException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AddCubicBeziers(object last) => throw new NotImplementedException();

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider) => throw new NotImplementedException();
    }
}