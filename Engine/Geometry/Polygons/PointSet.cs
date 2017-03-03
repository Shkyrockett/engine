// <copyright file="PointSet.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(PointSet))]
    public class PointSet
        : Shape, IEnumerable<Point2D>
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private List<Point2D> points;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public PointSet()
            : this(new List<Point2D>())
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        public PointSet(Contour polygon)
            : this(polygon.Points)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polyline"></param>
        public PointSet(Polyline polyline)
            : this(polyline.Points)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygons"></param>
        public PointSet(Polygon polygons)
            : this(polygons.Contours)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygons"></param>
        public PointSet(PolylineSet polygons)
            : this(polygons.Polylines)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public PointSet(params Point2D[] points)
            : this(new List<Point2D>(points))
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public PointSet(IEnumerable<Point2D> points)
        {
            this.points = points as List<Point2D>;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polylines"></param>
        public PointSet(IEnumerable<Polyline> polylines)
        {
            points = new List<Point2D>();
            foreach (var polyline in polylines)
                points.Concat(polyline.Points);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygons"></param>
        public PointSet(IEnumerable<Contour> polygons)
        {
            points = new List<Point2D>();
            foreach (var polygon in polygons)
                points.Concat(polygon.Points);
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
                OnPropertyChanged(nameof(Points));
                update?.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public int Count
            => points.Count;

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override double Perimeter
            => 0;

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => (Rectangle2D) CachingProperty(() => Measurements.PolygonBounds(points));

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override double Area
            => 0;

        #endregion

        #region Serialization

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnSerializing()]
        protected void OnSerializing(StreamingContext context)
        {
            // Assert("This value went into the data file during serialization.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnSerialized()]
        protected void OnSerialized(StreamingContext context)
        {
            // Assert("This value was reset after serialization.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnDeserializing()]
        protected void OnDeserializing(StreamingContext context)
        {
            // Assert("This value was set during deserialization");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnDeserialized()]
        protected void OnDeserialized(StreamingContext context)
        {
            // Assert("This value was set after deserialization.");
        }

        #endregion

        #region Mutators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        public PointSet Add(Point2D point)
        {
            Points.Add(point);
            OnPropertyChanged(nameof(Add));
            update?.Invoke();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public PointSet Reverse()
        {
            Points.Reverse();
            OnPropertyChanged(nameof(Reverse));
            update?.Invoke();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delta"></param>
        /// <returns></returns>
        public PointSet Translate(Point2D delta)
            => Translate(this, delta);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="delta"></param>
        /// <returns></returns>
        public static PointSet Translate(PointSet path, Point2D delta)
        {
            List<Point2D> outPath = new List<Point2D>(path.points.Count);
            for (int i = 0; i < path.points.Count; i++)
                outPath.Add((path[i].X + delta.X, path[i].Y + delta.Y));
            return new PointSet(outPath);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Point2D Interpolate(double t)
        {
            int place = (int)Math.Round(Points.Count * t, 0, MidpointRounding.AwayFromZero) - 1;
            return Points[place];
        }

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Contains(Point2D point)
        {
            bool value = false;
            foreach (var item in Points)
            {
                if (point == item)
                {
                    value = true;
                    break;
                }
            }

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PointSet Clone()
            => new PointSet(points.ToArray());

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Point2D> GetEnumerator()
            => points.GetEnumerator();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        /// <summary>
        /// Creates a string representation of this <see cref="PointSet"/> struct based on the format string
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
        public override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(PointSet);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(PointSet)}{{{string.Join(sep.ToString(), Points)}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
