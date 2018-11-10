// <copyright file="Intersections.cs" >
//     Copyright © 2017 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>

// <copyright company="kevlindev" >
//     The original implementation was based on methods found on Kevin Lindsey's site http://www.kevlindev.com/gui/math/intersection/. 
//     Copyright © 2000 - 2003 Kevin Lindsey. All rights reserved.
// </copyright>
// <author id="thelonious">Kevin Lindsey</author>
// <license>
//     Licensed under the BSD-3-Clause https://github.com/thelonious/kld-intersections/blob/development/LICENSE
// </license>

// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Engine
{
    /// <summary>
    /// The intersection struct.
    /// </summary>
    public struct Intersection
        : IFormattable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Intersection"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Intersection(IntersectionState status)
            : this(status, new List<Point2D>())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Intersection"/> class.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="points">The points.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Intersection(IntersectionState state, params Point2D[] points)
            : this(state, new List<Point2D>(points))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Intersection"/> class.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="points">The points.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Intersection(IntersectionState state, IEnumerable<Point2D> points)
            : this(state, new List<Point2D>(points))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Intersection"/> class.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="points">The points.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Intersection(IntersectionState state, List<Point2D> points)
        {
            State = state;
            Points = points;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// The deconstruct.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="points">The points.</param>
        public void Deconstruct(out IntersectionState state, out List<Point2D> points)
        {
            state = State;
            points = Points;
        }
        #endregion Deconstructors

        #region Indexers
        /// <summary>
        /// The Indexer.
        /// </summary>
        /// <param name="index">The index index.</param>
        /// <returns>One element of type Point2D.</returns>
        public Point2D this[int index]
        {
            get { return Points[index]; }
            set { Points[index] = value; }
        }
        #endregion Indexers

        #region Properties
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public IntersectionState State { get; set; }

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        public List<Point2D> Points { get; set; }

        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count
            => (Points is null) ? 0 : Points.Count;
        #endregion Properties

        #region Operators
        /// <summary>
        /// The operator ==.
        /// Compares two <see cref="Intersection"/> objects.
        /// The result specifies whether the values of the two <see cref="Intersection"/> objects are equal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Intersection left, Intersection right)
            => Equals(left, right);

        /// <summary>
        /// The operator !=.
        /// Compares two <see cref="Intersection"/> objects.
        /// The result specifies whether the values the two <see cref="Intersection"/> objects are unequal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Intersection left, Intersection right)
            => !Equals(left, right);
        #endregion Operators

        #region Mutators
        /// <summary>
        /// The append point.
        /// </summary>
        /// <param name="point">The point.</param>
        public void AppendPoint(Point2D point)
        {
            if (Points is null)
                Points = new List<Point2D>();
            Points.Add(point);
        }

        /// <summary>
        /// The append points.
        /// </summary>
        /// <param name="points">The points.</param>
        public void AppendPoints(List<Point2D> points)
        {
            if (points is null)
                return;
            if (Points is null)
                Points = points;
            else
                Points.AddRange(points);
        }

        /// <summary>
        /// The append points.
        /// </summary>
        /// <param name="points">The points.</param>
        public void AppendPoints(HashSet<Point2D> points)
        {
            if (Points is null) Points = points.ToList();
            else
                Points.AddRange(points);
        }
        #endregion Mutators

        #region Standard Class Methods
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
            => State.GetHashCode()
            ^ Points.GetHashCode();

        ///// <summary>
        ///// Compares two Intersections.
        ///// </summary>
        ///// <param name="a">The a.</param>
        ///// <param name="b">The b.</param>
        ///// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static bool Compare(Point2D a, Point2D b)
        //    => Equals(a, b);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Intersection a, Intersection b)
            => (a.State == b.State) & (a.Points == b.Points);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is Intersection && Equals(this, (Intersection)obj);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Intersection value)
            => Equals(this, value);

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Intersection"/>.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Intersection"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <returns>A <see cref="string"/> representation of this object.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// The to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// Creates a <see cref="string"/> representation of this <see cref="Intersection"/> class based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Intersection"/> class based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>A <see cref="string"/> representation of this object.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ConvertToString(string format, IFormatProvider provider)
        {
            //if (this is null) return nameof(Intersection);
            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Intersection)}{{{nameof(State)}: {State.ToString()}, {string.Join(sep.ToString(), Points)}}}";
            return formatable.ToString(format, provider);
        }
        #endregion Standard Class Methods
    }
}
