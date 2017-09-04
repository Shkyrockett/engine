// <copyright file="Intersections.cs" >
//     Copyright © 2017 Shkyrockett. All rights reserved.
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
    /// 
    /// </summary>
    public struct Intersection
        : IFormattable
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private IntersectionState state;

        /// <summary>
        /// 
        /// </summary>
        private List<Point2D> points;

        #endregion

        #region Constructors

        ///// <summary>
        ///// 
        ///// </summary>
        //public Intersection()
        //    : this(IntersectionStatus.NoIntersection)
        //{ }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        public Intersection(IntersectionState status)
            : this(status, new List<Point2D>())
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="points"></param>
        public Intersection(IntersectionState state, params Point2D[] points)
            : this(state, new List<Point2D>(points))
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="points"></param>
        public Intersection(IntersectionState state, IEnumerable<Point2D> points)
            : this(state, new List<Point2D>(points))
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="points"></param>
        public Intersection(IntersectionState state, List<Point2D> points)
        {
            this.state = state;
            this.points = points;
        }

        #endregion

        #region Deconstructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="points"></param>
        public void Deconstruct(out IntersectionState state, out List<Point2D> points)
        {
            state = State;
            points = Points;
        }

        #endregion

        #region Indexers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Point2D this[int index]
        {
            get { return points[index]; }
            set { points[index] = value; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public IntersectionState State
        {
            get { return state; }
            set { state = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Point2D> Points
        {
            get { return points; }
            set { points = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count
            => (Points == null) ? 0 : Points.Count;

        #endregion

        #region Operators

        /// <summary>
        /// Compares two <see cref="Intersection"/> objects.
        /// The result specifies whether the values of the two <see cref="Intersection"/> objects are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Intersection left, Intersection right)
            => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="Intersection"/> objects.
        /// The result specifies whether the values the two <see cref="Intersection"/> objects are unequal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Intersection left, Intersection right)
            => !Equals(left, right);

        #endregion

        #region Mutators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        public void AppendPoint(Point2D point)
        {
            if (points == null)
                points = new List<Point2D>();
            points.Add(point);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public void AppendPoints(List<Point2D> points)
        {
            if (points == null)
                return;
            if (this.points == null)
                this.points = points;
            else
                this.points.AddRange(points);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public void AppendPoints(HashSet<Point2D> points)
        {
            if (this.points == null) this.points = points.ToList();
            else
                this.points.AddRange(points);
        }

        #endregion

        #region Standard Class Methods

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
            => state.GetHashCode()
            ^ points.GetHashCode();

        /// <summary>
        /// Compares two Intersections.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Point2D a, Point2D b)
            => Equals(a, b);

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Intersection a, Intersection b)
            => (a.state == b.state) & (a.Points == b.Points);

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is Intersection && Equals(this, (Intersection)obj);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Intersection value)
            => Equals(this, value);

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Intersection"/>.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Intersection"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Intersection"/> class based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        string IFormattable.ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Intersection"/> class based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ConvertToString(string format, IFormatProvider provider)
        {
            //if (this == null) return nameof(Intersection);
            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Intersection)}{{{nameof(State)}: {state.ToString()}, {string.Join(sep.ToString(), points)}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
