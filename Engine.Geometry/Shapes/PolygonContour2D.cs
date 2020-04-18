// <copyright file="Polygon2D.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
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
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// The polygon contour class.
    /// </summary>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(PolygonContour2D))]
    [XmlType(TypeName = "polygon", Namespace = "http://www.w3.org/2000/svg")]
    [DebuggerDisplay("{ToString()}")]
    public class PolygonContour2D
        : Shape2D, IEnumerable<Point2D>
    {
        #region Fields
        /// <summary>
        /// The points.
        /// </summary>
        private List<Point2D> points;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonContour2D"/> class.
        /// </summary>
        public PolygonContour2D()
            : this(new List<Point2D>())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonContour2D"/> class.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        public PolygonContour2D(PolygonContour2D polygon)
            : this(polygon?.points)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonContour2D"/> class.
        /// </summary>
        /// <param name="polyline">The polyline.</param>
        public PolygonContour2D(Polyline2D polyline)
            : this(polyline?.Points)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonContour2D"/> class.
        /// </summary>
        /// <param name="points">The points.</param>
        public PolygonContour2D(params Point2D[] points)
            : this(new List<Point2D>(points))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonContour2D"/> class.
        /// </summary>
        /// <param name="points">The points.</param>
        public PolygonContour2D(IEnumerable<Point2D> points)
        {
            this.points = points as List<Point2D> ?? new List<Point2D>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonContour2D"/> class.
        /// </summary>
        /// <param name="polylines">The polylines.</param>
        public PolygonContour2D(IEnumerable<Polyline2D> polylines)
        {
            if (polylines is null)
            {
                throw new ArgumentNullException(nameof(polylines));
            }

            points = new List<Point2D>();
            foreach (var polyline in polylines)
            {
                points.Concat(polyline.Points);
            }
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
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<Point2D> Points
        {
            get { return points; }
            set
            {
                points = value;
                ClearCache();
                OnPropertyChanged(nameof(Points));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the definition.
        /// </summary>
        [Browsable(false)]
        [XmlAttribute(nameof(points)), SoapAttribute(nameof(points))]
        [RefreshProperties(RefreshProperties.All)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Definition
        {
            get { return ToPathDefString(); }
            set
            {
                points = ParsePathDefString(value, CultureInfo.InvariantCulture);
                ClearCache();
                OnPropertyChanged(nameof(Definition));
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
        /// Gets or sets the capacity.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public int Capacity { get { return points.Capacity; } set { points.Capacity = value; } }

        /// <summary>
        /// Gets the perimeter.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override double Perimeter
            => (double)CachingProperty(() => Measurements.PolygonContourPerimeter(points));

        /// <summary>
        /// Gets the bounds.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => (points != null) ? (Rectangle2D)CachingProperty(() => Measurements.PolygonBounds(points)) : null;

        /// <summary>
        /// Gets the area.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override double Area
            => (double)CachingProperty(() => Math.Abs(Measurements.SignedPolygonArea(points)));

        /// <summary>
        /// Gets the signed area.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public double SignedArea
            => (double)CachingProperty(() => Measurements.SignedPolygonArea(points));

        /// <summary>
        /// Gets the orientation.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public RotationDirection Orientation
            => (RotationDirection)CachingProperty(() => (RotationDirection)Math.Sign(Measurements.SignedPolygonArea(points)));
        #endregion Properties

        #region Mutators
        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="PolygonContour2D"/>.</returns>
        public PolygonContour2D Add(Point2D point)
        {
            Points.Add(point);
            ClearCache();
            OnPropertyChanged(nameof(Add));
            update?.Invoke();
            return this;
        }

        /// <summary>
        /// Clears the points of the contour.
        /// </summary>
        public void Clear()
            // Clear the elements of the array so that the garbage colector can reclaim the references.
            => points.Clear();

        /// <summary>
        /// The reverse.
        /// </summary>
        /// <returns>The <see cref="PolygonContour2D"/>.</returns>
        public PolygonContour2D Reverse()
        {
            Points.Reverse();
            ClearCache();
            OnPropertyChanged(nameof(Reverse));
            update?.Invoke();
            return this;
        }

        /// <summary>
        /// Translate.
        /// </summary>
        /// <param name="delta">The delta.</param>
        /// <returns>The <see cref="PolygonContour2D"/>.</returns>
        public PolygonContour2D Translate(Point2D delta)
            => Translate(this, delta);

        /// <summary>
        /// Translate.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="delta">The delta.</param>
        /// <returns>The <see cref="PolygonContour2D"/>.</returns>
        public static PolygonContour2D Translate(PolygonContour2D path, Point2D delta)
        {
            var outPath = new List<Point2D>((path?.points).Count);
            for (var i = 0; i < path.points.Count; i++)
            {
                outPath.Add((path[i].X + delta.X, path[i].Y + delta.Y));
            }

            return new PolygonContour2D(outPath);
        }
        #endregion Mutators

        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Point2D Interpolate(double t)
        {
            if (t == 0 || t == 1)
            {
                return points[0];
            }

            var weights = new (double length, double accumulated)[points.Count + 1];
            weights[0] = (0, 0);
            var cursor = points[0];
            var accumulatedLength = 0d;

            // Build up the weights map.
            for (var i = 1; i < points.Count + 1; i++)
            {
                var curentLength = Measurements.Distance(cursor, (i == points.Count) ? points[0] : points[i]);
                accumulatedLength += curentLength;
                weights[i] = (curentLength, accumulatedLength);
                cursor = (i == points.Count) ? points[0] : points[i];
            }

            var accumulatedLengthT = accumulatedLength * t;

            // Find the segment.
            for (var i = points.Count - 1; i >= 0; i--)
            {
                if (weights[i].accumulated <= accumulatedLengthT)
                {
                    // Interpolate the position.
                    var th = (accumulatedLengthT - weights[i].accumulated) / weights[i + 1].length;
                    cursor = Interpolators.Linear(th, points[i], (i == points.Count - 1) ? points[0] : points[i + 1]);
                    break;
                }
            }

            return cursor;
        }

        /// <summary>
        /// The segment.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The <see cref="LineSegment2D"/>.</returns>
        public LineSegment2D Segment(int index) => (index == points.Count - 1) ? new LineSegment2D(points[^1], points[0]) : new LineSegment2D(points[index], points[index + 1]);

        #region Methods
        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Contains(Point2D point) => Intersections.Contains(this, point) != Inclusions.Outside;

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>The <see cref="PolygonContour2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PolygonContour2D Clone() => new PolygonContour2D(points.ToArray());

        /// <summary>
        /// The offset.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <returns>The <see cref="PolygonContour2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual PolygonContour2D Offset(double offset) => Offsets.Offset(this, offset);

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator{T}"/>.</returns>
        public IEnumerator<Point2D> GetEnumerator() => points.GetEnumerator();

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Parse the path def string.
        /// </summary>
        /// <param name="pathDefinition">The pathDefinition.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<Point2D> ParsePathDefString(string pathDefinition) => ParsePathDefString(pathDefinition, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse the path def string.
        /// </summary>
        /// <param name="pathDefinition">The pathDefinition.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        /// <exception cref="Exception">Polygon definitions must be in sets of two numbers.</exception>
        public static List<Point2D> ParsePathDefString(string pathDefinition, IFormatProvider provider)
        {
            // Discard whitespace and comma but keep the - minus sign.
            var separators = $@"[\s{Tokenizer.GetNumericListSeparator(provider)}]|(?=-)";

            var poly = new List<Point2D>();

            // Split the definition string into shape tokens.
            var list = Regex.Split(pathDefinition, separators).Where(t => !string.IsNullOrEmpty(t)).Select(arg => double.Parse(arg, CultureInfo.InvariantCulture)).ToArray();

            if (list.Length % 2 != 0)
            {
                throw new Exception("Polygon definitions must be in sets of two numbers.");
            }

            for (var i = 0; i < list.Length - 1; i = i = 2)
            {
                poly.Add(new Point2D(list[i], list[i + 1]));
            }

            return poly;
        }

        /// <summary>
        /// The to path def string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        private string ToPathDefString()
            => ToPathDefString(string.Empty, CultureInfo.InvariantCulture);

        /// <summary>
        /// The to path def string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private string ToPathDefString(string format, IFormatProvider provider)
        {
            _ = format;
            var output = new StringBuilder();

            var sep = Tokenizer.GetNumericListSeparator(provider);

            foreach (var item in points)
            {
                // M is Move to.
                output.Append($"{item.X}{sep}{item.Y} ");
            }

            // Minus signs are valid separators in SVG path definitions which can be
            // used in place of commas to shrink the length of the string. 
            output.Replace($"{sep}-", "-");
            return output.ToString().TrimEnd();
        }

        /// <summary>
        /// Creates a string representation of this <see cref="PolygonContour2D"/> struct based on the format string
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
            if (this is null)
            {
                return nameof(PolygonContour2D);
            }

            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(PolygonContour2D)}{{{string.Join(sep.ToString(CultureInfo.InvariantCulture), Points)}}}";
            return formatable.ToString(format, provider);
        }
        #endregion Methods
    }
}
