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
        private IntersectionStatus status;

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
        public Intersection(IntersectionStatus status)
            : this(IntersectionStatus.NoIntersection, new List<Point2D>())
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="points"></param>
        public Intersection(IntersectionStatus status, params Point2D[] points)
            : this(status, new List<Point2D>(points))
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="points"></param>
        public Intersection(IntersectionStatus status, List<Point2D> points)
        {
            this.status = status;
            this.points = points;
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
        public IntersectionStatus Status
        {
            get { return status; }
            set { status = value; }
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
            => this.points.Add(point);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public void AppendPoints(List<Point2D> points)
            => this.points = points.Concat(points).ToList();

        #endregion

        #region Standard Class Methods

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        public override int GetHashCode()
            => status.GetHashCode()
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
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Intersection)}{{{nameof(Status)}: {status.ToString()}, {string.Join(sep.ToString(), points)}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
