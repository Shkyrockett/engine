// <copyright file="Polygon2D.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
    /// A closed Polygon made up of sets of Contours.
    /// </summary>
    /// <seealso cref="Engine.IClosedShape" />
    /// <seealso cref="System.IEquatable{Engine.Polygon2D}" />
    [GraphicsObject]
    [DataContract, Serializable]
    [XmlType(TypeName = "polygonSet", Namespace = "http://www.w3.org/2000/svg")]
    [DebuggerDisplay("{ToString()}")]
    public struct Polygon2D
        : IClosedShape, IPropertyCaching, IEnumerable, IEnumerable<PolygonContour2D>, IEquatable<Polygon2D>
    {
        #region Fields
        /// <summary>
        /// An array of Polygon Contours.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        private List<PolygonContour2D> contours;
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
        /// Initializes a new instance of the <see cref="Polygon2D"/> class with a single <see cref="PolygonContour2D"/> of a set of <see cref="Point2D"/>s from a parameter list.
        /// </summary>
        /// <param name="points"></param>
        public Polygon2D(params Point2D[] points)
            : this(new[] { points })
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon2D"/> class with a single <see cref="PolygonContour2D"/> from a set of <see cref="Point2D"/>s.
        /// </summary>
        /// <param name="points"></param>
        public Polygon2D(IEnumerable<Point2D> points)
            : this(new IEnumerable<Point2D>[] { points })
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon2D"/> class.
        /// </summary>
        public Polygon2D(IEnumerable<PolygonContour2D> contours)
            : this()
        {
            this.contours = contours as List<PolygonContour2D> ?? new List<PolygonContour2D>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon2D"/> class from a parameter list.
        /// </summary>
        /// <param name="contours">The contours.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polygon2D(params IEnumerable<Point2D>[] contours)
            : this()
        {
            this.contours = new List<PolygonContour2D>() ?? throw new ArgumentNullException(nameof(contours), "Argument must not be null.");

            foreach (var list in contours)
            {
                this.contours.Add(new PolygonContour2D(list));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon2D"/> class.
        /// </summary>
        /// <param name="contours">The contours.</param>
        public Polygon2D(IEnumerable<List<Point2D>> contours)
            : this()
        {
            this.contours = new List<PolygonContour2D>() ?? throw new ArgumentNullException(nameof(contours), "Argument must not be null.");

            if (contours is null)
            {
                throw new ArgumentNullException(nameof(contours));
            }

            foreach (var list in contours)
            {
                this.contours.Add(new PolygonContour2D(list));
            }
        }
        #endregion

        #region Deconstructors
        /// <summary>
        /// The deconstruct.
        /// </summary>
        /// <param name="points">The points.</param>
        public void Deconstruct(out List<PolygonContour2D> points) => points = this.contours;
        #endregion Deconstructors

        #region Indexers
        /// <summary>
        /// The Indexer.
        /// </summary>
        /// <value>
        /// The <see cref="PolygonContour2D"/>.
        /// </value>
        /// <param name="index">The index index.</param>
        /// <returns>
        /// One element of type PolygonContour2D.
        /// </returns>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        public PolygonContour2D this[int index]
        {
            get { return contours[index]; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                contours[index] = value;
                OnPropertyChanged();
            }
        }
        #endregion Indexers

        #region Properties
        /// <summary>
        /// Gets or sets the contours.
        /// </summary>
        /// <value>
        /// The contours.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<PolygonContour2D> Contours
        {
            get { return contours; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                contours = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the definition.
        /// </summary>
        [Browsable(false)]
        [XmlAttribute("d"), SoapAttribute("d")]
        [RefreshProperties(RefreshProperties.All)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Definition
        {
            get { return ToPathDefString(); }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                contours = ParsePathDefString(value, CultureInfo.InvariantCulture);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int Count => contours.Count;

        /// <summary>
        /// Gets or sets the capacity.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int Capacity { get { return contours.Capacity; } set { contours.Capacity = value; } }

        /// <summary>
        /// Gets the vertices count.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int VerticesCount
        {
            get
            {
                var verticesCount = 0;
                foreach (var c in contours)
                {
                    verticesCount += c.Points.Count;
                }

                return verticesCount;
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
                return contours.Sum(p => p.Perimeter);
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
                var contours = this.contours;
                return (Rectangle2D)(this as IPropertyCaching).CachingProperty(() => bounds(contours));
                static Rectangle2D bounds(List<PolygonContour2D> contours)
                {
                    if (contours.Count == 0)
                    {
                        return Rectangle2D.Empty;
                    }

                    var bb = contours[0].Bounds;

                    foreach (var c in contours)
                    {
                        bb = bb.Union(c.Bounds);
                    }

                    return bb;
                }
            }
        }

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
        public static bool operator ==(Polygon2D left, Polygon2D right) => left.Equals(right);

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
        public static bool operator !=(Polygon2D left, Polygon2D right) => !(left == right);
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
        public override bool Equals([AllowNull] object obj) => obj is Polygon2D d && Equals(d);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals([AllowNull] Polygon2D other) => EqualityComparer<List<PolygonContour2D>>.Default.Equals(Contours, other.Contours);
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
        public Polygon2D Add(PolygonContour2D contour)
        {
            OnPropertyChanging();
            (this as IPropertyCaching).ClearCache();
            contours.Add(contour);
            OnPropertyChanged();
            return this;
        }

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="contour">The contour.</param>
        public Polygon2D Add(List<Point2D> contour)
        {
            OnPropertyChanging();
            (this as IPropertyCaching).ClearCache();
            contours.Add(new PolygonContour2D(contour));
            OnPropertyChanged();
            return this;
        }

        /// <summary>
        /// Clears the contours of the polygon.
        /// </summary>
        public void Clear()
        {
            var size = contours.Count;
            if (size > 0)
            {
                // Clear the elements of the array so that the garbage collector can reclaim the references.
                foreach (var contour in contours)
                {
                    contour.Clear();
                }
            }

            contours.Clear();
        }

        /// <summary>
        /// The reverse.
        /// </summary>
        /// <returns>
        /// The <see cref="Polyline2D" />.
        /// </returns>
        public Polygon2D Reverse()
        {
            OnPropertyChanging();
            (this as IPropertyCaching).ClearCache();
            contours.Reverse();
            OnPropertyChanged();
            return this;
        }

        /// <summary>
        /// Reverses the contours.
        /// </summary>
        /// <returns></returns>
        public Polygon2D ReverseContours()
        {
            OnPropertyChanging();
            (this as IPropertyCaching).ClearCache();
            foreach (var poly in contours)
            {
                poly.Reverse();
            }
            OnPropertyChanged();
            return this;
        }
        #endregion Mutators

        /// <summary>
        /// Parse the path def string.
        /// </summary>
        /// <param name="pathDefinition">The pathDefinition.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<PolygonContour2D> ParsePathDefString(string pathDefinition) => ParsePathDefString(pathDefinition, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse the path def string.
        /// </summary>
        /// <param name="pathDefinition">The pathDefinition.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<PolygonContour2D> ParsePathDefString(string pathDefinition, IFormatProvider provider)
        {
            // These letters are valid PolyBezier2D commands. Split the tokens at these.
            const string separators = @"(?=[M])";
            //string separators = @"(?=[MZ])";

            var contours = new List<PolygonContour2D>();

            // Split the definition string into shape tokens.
            foreach (var token in Regex.Split(pathDefinition, separators).Where(t => !string.IsNullOrWhiteSpace(t)))
            {
                // Get the token type.
                var cmd = token.Take(1).Single();

                switch (cmd)
                {
                    case 'M':
                        // M is Move to.
                        contours.Add(new PolygonContour2D(PolygonContour2D.ParsePathDefString(token.Substring(1), provider)));
                        break;
                        //case 'Z':
                        //    // Z is End of Path.
                        //    contours.Closed = true;
                        //    break;
                }

            }

            return contours;
        }

        /// <summary>
        /// The to path def string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        private string ToPathDefString() => ToPathDefString(string.Empty, CultureInfo.InvariantCulture);

        /// <summary>
        /// The to path def string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private string ToPathDefString(string format, IFormatProvider provider)
        {
            _ = format;
            _ = provider;
            var output = new StringBuilder();

            foreach (var contour in contours)
            {
                output.Append($"M{contour.Definition} ");
            }

            return output.ToString().TrimEnd();
        }

        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(Point2D point) => Intersections.PolygonContainsPoint(this.contours, point.X, point.Y) != Inclusions.Outside;

        #region Standard Methods
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
        public override int GetHashCode() => HashCode.Combine(Contours);

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator{T}"/>.</returns>
        public IEnumerator<PolygonContour2D> GetEnumerator() => contours.GetEnumerator();

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator() => contours.GetEnumerator();

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
        public string ToString(string format, IFormatProvider provider)
        {
            if (this == null)
            {
                return nameof(Polygon2D);
            }

            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Polygon2D)}{{{string.Join(sep.ToString(provider), Contours)}}}";
            return formatable.ToString(format, provider);
        }
        #endregion
    }
}