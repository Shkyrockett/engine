// <copyright file="Polygon.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Polygon))]
    public class Polygon
        : Shape, IClosedShape
    {
        #region Private Fields

        /// <summary>
        /// 
        /// </summary>
        private List<Point2D> points;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public Polygon()
            : this(new List<Point2D>())
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        public Polygon(Polygon polygon)
            : this(polygon.points)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polyline"></param>
        public Polygon(Polyline polyline)
            : this(polyline.Points)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public Polygon(params Point2D[] points)
            : this(new List<Point2D>(points))
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public Polygon(IEnumerable<Point2D> points)
        {
            this.points = points as List<Point2D>;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polylines"></param>
        public Polygon(IEnumerable<Polyline> polylines)
        {
            points = new List<Point2D>();
            foreach (Polyline polyline in polylines)
                points.Concat(polyline.Points);
        }

        #endregion

        #region Indexers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        public Point2D this[int index]
        {
            get { return (points as List<Point2D>)[index]; }
            set
            {
                (points as List<Point2D>)[index] = value;
                update?.Invoke();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public List<Point2D> Points
        {
            get { return points; }
            set
            {
                points = value;
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override double Perimeter
            => Perimeters.PolygonPerimeter(points);

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => Boundings.Polygon(points);

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override double Area
            => Areas.Polygon(points);

        #endregion

        #region Mutators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        public void Add(Point2D point)
        {
            Points.Add(point);
            update?.Invoke();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Reverse()
        {
            Points.Reverse();
            update?.Invoke();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Contains(Point2D point)
            => Intersections.Contains(this, point) != Inclusion.Outside;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polygon Clone()
            => new Polygon(points.ToArray());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        [Pure]
        public virtual Polygon Offset(double offset)
        {
            List<Point2D> points = (this.points as List<Point2D>);

            var polyline = new Polygon();

            LineSegment offsetLine = Primitives.OffsetSegment(points[Points.Count - 1], points[0], offset);
            LineSegment startLine = offsetLine;

            for (int i = 1; i < Points.Count; i++)
            {
                LineSegment newOffsetLine = Primitives.OffsetSegment(points[i - 1], points[i], offset);
                polyline.Add(Intersections.LineLine(offsetLine.A.X, offsetLine.A.Y, offsetLine.B.X, offsetLine.B.Y, newOffsetLine.A.X, newOffsetLine.A.Y, newOffsetLine.B.X, newOffsetLine.B.Y).Item2);
                offsetLine = newOffsetLine;
            }

            polyline.Add(Intersections.LineLine(offsetLine.A.X, offsetLine.A.Y, offsetLine.B.X, offsetLine.B.Y, startLine.A.X, startLine.A.Y, startLine.B.X, startLine.B.Y).Item2);

            return polyline;
        }

        /// <summary>
        /// Creates a string representation of this <see cref="Polygon"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Polygon);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Polygon)}{{{string.Join(sep.ToString(), Points)}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
