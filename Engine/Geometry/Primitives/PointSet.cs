// <copyright file="PointSet.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
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
    /// The point set class.
    /// </summary>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(PointSet))]
    public class PointSet
        : Shape, IEnumerable<Point2D>
    {
        #region Fields
        /// <summary>
        /// The points.
        /// </summary>
        private List<Point2D> points;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PointSet"/> class.
        /// </summary>
        public PointSet()
            : this(new List<Point2D>())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointSet"/> class.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        public PointSet(PolygonContour polygon)
            : this(polygon.Points)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointSet"/> class.
        /// </summary>
        /// <param name="polyline">The polyline.</param>
        public PointSet(Polyline polyline)
            : this(polyline.Points)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointSet"/> class.
        /// </summary>
        /// <param name="polygons">The polygons.</param>
        public PointSet(Polygon polygons)
            : this(polygons.Contours)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointSet"/> class.
        /// </summary>
        /// <param name="polygons">The polygons.</param>
        public PointSet(PolylineSet polygons)
            : this(polygons.Polylines)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointSet"/> class.
        /// </summary>
        /// <param name="points">The points.</param>
        public PointSet(params Point2D[] points)
            : this(new List<Point2D>(points))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointSet"/> class.
        /// </summary>
        /// <param name="points">The points.</param>
        public PointSet(IEnumerable<Point2D> points)
        {
            this.points = points as List<Point2D>;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointSet"/> class.
        /// </summary>
        /// <param name="polylines">The polylines.</param>
        public PointSet(IEnumerable<Polyline> polylines)
        {
            points = new List<Point2D>();
            foreach (var polyline in polylines)
                points.Concat(polyline.Points);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointSet"/> class.
        /// </summary>
        /// <param name="polygons">The polygons.</param>
        public PointSet(IEnumerable<PolygonContour> polygons)
        {
            points = new List<Point2D>();
            foreach (var polygon in polygons)
                points.Concat(polygon.Points);
        }
        #endregion Constructors

        #region Indexers
        /// <summary>
        /// The Indexer.
        /// </summary>
        /// <param name="index">The index index.</param>
        /// <returns>One element of type Point2D.</returns>
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
        #endregion Indexers

        #region Properties
        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        [TypeConverter(typeof(ExpandableCollectionConverter))]
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
        /// Gets the count.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public int Count
            => points.Count;

        /// <summary>
        /// Gets the perimeter.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override double Perimeter
            => 0;

        /// <summary>
        /// Gets the bounds.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => (Rectangle2D) CachingProperty(() => Measurements.PolygonBounds(points));

        /// <summary>
        /// Gets the area.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override double Area
            => 0;
        #endregion Properties

        //#region Serialization

        ///// <summary>
        ///// Sends an event indicating that this value went into the data file during serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerializing()]
        //private void OnSerializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(PointSet)} is being serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was reset after serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerialized()]
        //private void OnSerialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(PointSet)} has been serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set during deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserializing()]
        //private void OnDeserializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(PointSet)} is being deserialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set after deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserialized()]
        //private void OnDeserialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(PointSet)} has been deserialized.");
        //}

        //#endregion

        #region Mutators
        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="PointSet"/>.</returns>
        public PointSet Add(Point2D point)
        {
            Points.Add(point);
            OnPropertyChanged(nameof(Add));
            update?.Invoke();
            return this;
        }

        /// <summary>
        /// The reverse.
        /// </summary>
        /// <returns>The <see cref="PointSet"/>.</returns>
        public PointSet Reverse()
        {
            Points.Reverse();
            OnPropertyChanged(nameof(Reverse));
            update?.Invoke();
            return this;
        }

        /// <summary>
        /// Translate.
        /// </summary>
        /// <param name="delta">The delta.</param>
        /// <returns>The <see cref="PointSet"/>.</returns>
        public PointSet Translate(Point2D delta)
            => Translate(this, delta);

        /// <summary>
        /// Translate.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="delta">The delta.</param>
        /// <returns>The <see cref="PointSet"/>.</returns>
        public static PointSet Translate(PointSet path, Point2D delta)
        {
            var outPath = new List<Point2D>(path.points.Count);
            for (var i = 0; i < path.points.Count; i++)
                outPath.Add((path[i].X + delta.X, path[i].Y + delta.Y));
            return new PointSet(outPath);
        }
        #endregion Mutators

        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        public override Point2D Interpolate(double t)
        {
            var place = (int)Math.Round(Points.Count * t, 0, MidpointRounding.AwayFromZero) - 1;
            return Points[place];
        }

        #region Methods
        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Contains(Point2D point)
        {
            var value = false;
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
        /// Clone.
        /// </summary>
        /// <returns>The <see cref="PointSet"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PointSet Clone()
            => new PointSet(points.ToArray());

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="T:IEnumerator{Point2D}"/>.</returns>
        public IEnumerator<Point2D> GetEnumerator()
            => points.GetEnumerator();

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator"/>.</returns>
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
            if (this is null) return nameof(PointSet);
            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(PointSet)}{{{string.Join(sep.ToString(), Points)}}}";
            return formatable.ToString(format, provider);
        }
        #endregion Methods
    }
}
