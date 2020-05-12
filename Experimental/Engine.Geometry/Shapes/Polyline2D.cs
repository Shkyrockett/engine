// <copyright file="Polyline2D.cs" company="Shkyrockett" >
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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// The polyline struct.
    /// </summary>
    /// <seealso cref="Engine.IShapeSegment" />
    /// <seealso cref="System.IEquatable{T}" />
    [GraphicsObject]
    [DataContract, Serializable]
    [XmlType(TypeName = "polyline", Namespace = "http://www.w3.org/2000/svg")]
    [DebuggerDisplay("{ToString()}")]
    public struct Polyline2D
        : IShapeSegment, IPropertyCaching, IEnumerable, IEnumerable<Point2D>, IEquatable<Polyline2D>
    {
        #region Fields
        /// <summary>
        /// The points.
        /// </summary>
        private List<Point2D> points;
        #endregion

        #region Event Delegates
        /// <summary>
        /// The property changed event of the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The property changing event of the <see cref="PropertyChangingEventHandler"/>.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Polyline2D"/> struct.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        public Polyline2D(PolygonContour2D polygon)
            : this(polygon.Points)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polyline2D"/> struct.
        /// </summary>
        /// <param name="polyline">The polyline.</param>
        public Polyline2D(Polyline2D polyline)
            : this(polyline.points)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polyline2D"/> struct.
        /// </summary>
        /// <param name="points">The points.</param>
        public Polyline2D(params Point2D[] points)
            : this(new List<Point2D>(points))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polyline2D"/> struct.
        /// </summary>
        /// <param name="points">The points.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polyline2D(IEnumerable<Point2D> points)
            : this()
        {
            this.points = points as List<Point2D> ?? new List<Point2D>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polyline2D"/> struct.
        /// </summary>
        /// <param name="polylines">The polylines.</param>
        public Polyline2D(IEnumerable<Polyline2D> polylines)
            : this()
        {
            if (polylines is null)
            {
                throw new ArgumentNullException(nameof(polylines));
            }

            points = new List<Point2D>();
            foreach (var polyline in polylines)
            {
                points.AddRange(polyline.Points);
            }
        }
        #endregion

        #region Deconstructors
        /// <summary>
        /// The deconstruct.
        /// </summary>
        /// <param name="points">The points.</param>
        public void Deconstruct(out List<Point2D> points) => points = this.points;
        #endregion Deconstructors

        #region Indexers
        /// <summary>
        /// The Indexer.
        /// </summary>
        /// <value>
        /// The <see cref="Point2D"/>.
        /// </value>
        /// <param name="index">The index index.</param>
        /// <returns>
        /// One element of type Point2D.
        /// </returns>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        public Point2D this[int index]
        {
            get { return points[index]; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                points[index] = value;
                OnPropertyChanged();
            }
        }
        #endregion Indexers

        #region Properties
        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>
        /// The points.
        /// </value>
        [XmlArray]
        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<Point2D> Points
        {
            get { return points; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                points = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the perimeter.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public double Perimeter
        {
            get
            {
                var points = this.points;
                return (double)(this as IPropertyCaching).CachingProperty(() => points.Zip(points.Skip(1), Measurements.Distance).Sum());
            }
        }

        /// <summary>
        /// Gets the bounds.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public Rectangle2D Bounds
        {
            get
            {
                var points = this.points;
                return (Rectangle2D)(this as IPropertyCaching).CachingProperty(() => Measurements.PolylineBounds(points));
            }
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public int Count => points.Count;

        /// <summary>
        /// Property cache for commonly used properties that may take time to calculate.
        /// </summary>
        /// <value>
        /// The property cache.
        /// </value>
        [Browsable(false)]
        [field: NonSerialized]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        Dictionary<object, object> IPropertyCaching.PropertyCache { get; set; }
        public IShapeSegment Leading { get; set; }
        public IShapeSegment Trailing { get; set; }
        public Point2D? Head { get; set; }
        public Point2D? Tail { get; set; }
        public double Length { get; set; }
        #endregion

        #region Operators
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
        public static bool operator ==(Polyline2D left, Polyline2D right) => left.Equals(right);

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
        public static bool operator !=(Polyline2D left, Polyline2D right) => !(left == right);
        #endregion

        #region Operator Backing Methods
        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([AllowNull] object obj) => obj is Polyline2D d && Equals(d);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals([AllowNull] Polyline2D other) => EqualityComparer<List<Point2D>>.Default.Equals(Points, other.Points);
        #endregion

        #region Mutators
        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>
        /// The <see cref="Polyline2D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polyline2D Add(Point2D point)
        {
            OnPropertyChanging();
            (this as IPropertyCaching).ClearCache();
            points.Add(point);
            OnPropertyChanged();
            return this;
        }

        /// <summary>
        /// The reverse.
        /// </summary>
        /// <returns>
        /// The <see cref="Polyline2D" />.
        /// </returns>
        public Polyline2D Reverse()
        {
            OnPropertyChanging();
            (this as IPropertyCaching).ClearCache();
            Points.Reverse();
            OnPropertyChanged();
            return this;
        }
        #endregion Mutators

        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>
        /// The <see cref="Point2D" />.
        /// </returns>
        public Point2D Interpolate(double t)
        {
            switch (t)
            {
                case 0:
                    return points[0];
                case 1:
                    return points[^1];
                default:
                    break;
            }

            var weights = new (double length, double accumulated)[points.Count];
            weights[0] = (0, 0);
            var cursor = points[0];
            double accumulatedLength = 0;

            // Build up the weights map.
            for (var i = 1; i < points.Count; i++)
            {
                var curentLength = Measurements.Distance(cursor, points[i]);
                accumulatedLength += curentLength;
                weights[i] = (curentLength, accumulatedLength);
                cursor = points[i];
            }

            var accumulatedLengthT = accumulatedLength * t;

            // Find the segment.
            for (var i = points.Count - 1; i >= 0; i--)
            {
                if (weights[i].accumulated <= accumulatedLengthT)
                {
                    // Interpolate the position.
                    var th = (accumulatedLengthT - weights[i].accumulated) / weights[i + 1].length;
                    cursor = Interpolators.Linear(th, points[i], points[i + 1]);
                    break;
                }
            }

            return cursor;
        }

        /// <summary>
        /// Translate.
        /// </summary>
        /// <param name="delta">The delta.</param>
        /// <returns>The <see cref="Polyline2D"/>.</returns>
        public Polyline2D Translate(Point2D delta) => Translate(this, delta);

        /// <summary>
        /// Translate.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="delta">The delta.</param>
        /// <returns>The <see cref="Polyline2D"/>.</returns>
        public static Polyline2D Translate(Polyline2D path, Point2D delta)
        {
            var outPath = new List<Point2D>(path.points.Count);
            for (var i = 0; i < path.points.Count; i++)
            {
                outPath.Add((path[i].X + delta.X, path[i].Y + delta.Y));
            }

            return new Polyline2D(outPath);
        }

        /// <summary>
        /// The offset.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <returns>The <see cref="Polyline2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polyline2D Offset(double offset) => Offsets.Offset(this, offset);

        #region Methods
        /// <summary>
        /// Raises the property changing event.
        /// </summary>
        /// <param name="name">The name.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnPropertyChanging([CallerMemberName] string name = "") => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="name">The name.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(Points);

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator{T}"/>.</returns>
        public IEnumerator<Point2D> GetEnumerator() => points.GetEnumerator();

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator() => points.GetEnumerator();

        /// <summary>
        /// Creates a string representation of this <see cref="Polyline2D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The format provider.</param>
        /// <returns>
        /// A <see cref="string" /> representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
        {
            if (this == null)
            {
                return nameof(Polyline2D);
            }

            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Polyline2D)}{{{string.Join(sep.ToString(provider), Points)}}}";
            return formatable.ToString(format, provider);
        }
        #endregion Methods
    }
}