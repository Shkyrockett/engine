// <copyright file="PolygonSet.cs" company="Shkyrockett" >
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
    /// A closed Polygon made up of sets of Contours.
    /// </summary>
    /// <structure>Engine.Geometry.PolyGon2D</structure>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Polygon2D))]
    [XmlType(TypeName = "polygonSet", Namespace = "http://www.w3.org/2000/svg")]
    [DebuggerDisplay("{ToString()}")]
    public class Polygon2D
        : Shape2D, IEnumerable<PolygonContour2D>
    {
        #region Private Fields
        /// <summary>
        /// An array of Polygon Contours.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        private List<PolygonContour2D> contours;
        #endregion Private Fields

        #region Constructors
        /// <summary>
        /// Initializes a default instance of the <see cref="Polygon2D"/> class.
        /// </summary>
        public Polygon2D()
            : this(new List<PolygonContour2D>())
        { }

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
        {
            this.contours = contours as List<PolygonContour2D> ?? new List<PolygonContour2D>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon2D"/> class from a parameter list.
        /// </summary>
        /// <param name="contours">The contours.</param>
        public Polygon2D(params IEnumerable<Point2D>[] contours)
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
        #endregion Constructors

        #region Indexers
        /// <summary>
        /// The Indexer.
        /// </summary>
        /// <param name="index">The index index.</param>
        /// <returns>One element of type PolygonContour.</returns>
        public PolygonContour2D this[int index]
        {
            get
            {
                return contours[index];
            }

            set
            {
                contours[index] = value;
                update?.Invoke();
            }
        }
        #endregion Indexers

        #region Properties
        /// <summary>
        /// Gets or sets the contours.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<PolygonContour2D> Contours
        {
            get { return contours; }
            set
            {
                contours = value;
                ClearCache();
                OnPropertyChanged(nameof(Contours));
                update?.Invoke();
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
                contours = ParsePathDefString(value, CultureInfo.InvariantCulture);
                ClearCache();
                OnPropertyChanged(nameof(Definition));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int Count
            => contours.Count;

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
        public override double Perimeter
            => contours.Sum(p => p.Perimeter);

        /// <summary>
        /// Gets the bounds.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
        {
            get
            {
                return (Rectangle2D)CachingProperty(() => bounds(contours));
                static Rectangle2D bounds(List<PolygonContour2D> contours)
                {
                    if (contours.Count == 0)
                    {
                        return Rectangle2D.Empty;
                    }

                    var bb = contours[0]?.Bounds;

                    foreach (var c in contours)
                    {
                        bb = bb.Union(c.Bounds);
                    }

                    return bb;
                }
            }
        }
        #endregion Properties

        /// <summary>
        /// Parse the path def string.
        /// </summary>
        /// <param name="pathDefinition">The pathDefinition.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<PolygonContour2D> ParsePathDefString(string pathDefinition)
            => ParsePathDefString(pathDefinition, CultureInfo.InvariantCulture);

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
            _ = provider;
            var output = new StringBuilder();

            foreach (var contour in contours)
            {
                output.Append($"M{contour.Definition} ");
            }

            return output.ToString().TrimEnd();
        }

        #region Mutators
        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="contour">The contour.</param>
        public void Add(PolygonContour2D contour)
        {
            contours.Add(contour);
            update?.Invoke();
        }

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="contour">The contour.</param>
        public void Add(List<Point2D> contour)
        {
            contours.Add(new PolygonContour2D(contour));
            update?.Invoke();
        }

        /// <summary>
        /// Clears the contours of the polygon.
        /// </summary>
        public void Clear()
        {
            var size = contours.Count;
            if (size > 0)
            {
                // Clear the elements of the array so that the garbage colector can reclaim the references.
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
        public void Reverse()
        {
            foreach (var poly in contours)
            {
                poly.Reverse();
            }
        }
        #endregion Mutators

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
        /// <returns>The <see cref="Polygon2D"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polygon2D Clone() => new Polygon2D(Contours.ToArray() as IEnumerable<PolygonContour2D>);

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
        /// Convert the to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>The <see cref="string"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this is null)
            {
                return nameof(Polygon2D);
            }

            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Polygon2D)}{{{string.Join(sep.ToString(provider), Contours)}}}";
            return formatable.ToString(format, provider);
        }
        #endregion Methods
    }
}
