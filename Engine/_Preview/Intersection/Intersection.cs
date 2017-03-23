// <copyright file="Intersections.cs" >
//     Copyright (c) 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>

// <copyright company="kevlindev" >
//     The original implementation was based on methods found on Kevin Lindsey's site http://www.kevlindev.com/gui/math/intersection/. 
//     Copyright (c) 2000 - 2003 Kevin Lindsey. All rights reserved.
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
            state = this.State;
            points = this.Points;
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
            => Points.Count;

        #endregion

        #region Mutators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        public void AppendPoint(Point2D point)
            => points.Add(point);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public void AppendPoints(List<Point2D> points)
        {
            if (this.points == null) this.points = points;
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
        public override int GetHashCode()
            => state.GetHashCode()
            ^ points.GetHashCode();

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Intersection"/>.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
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
