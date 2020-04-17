// <copyright file="PolyBezier2D.cs" company="Shkyrockett" >
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
    /// A closed Polygon2D made up of sets of Bezier Contours.
    /// </summary>
    /// <structure>Engine.Geometry.PolyGon2D</structure>

    [DataContract, Serializable]
    //[GraphicsObject]
    [DisplayName(nameof(PolyBezier2D))]
    [XmlType(TypeName = "polybezier", Namespace = "shapes")]
    [DebuggerDisplay("{ToString()}")]
    public class PolyBezier2D
        : Shape2D, IEnumerable<PolyBezierContour2D>
    {
        #region Fields
        /// <summary>
        /// An array of Polygon2D Contours.
        /// </summary>

        [DataMember, XmlArray, SoapElement]
        private List<PolyBezierContour2D> contours;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a default instance of the <see cref="PolyBezier"/> class.
        /// </summary>
        public PolyBezier2D()
            : this(new List<PolyBezierContour2D>())
        { }

        /// <summary>
        /// Initializes a default instance of the <see cref="PolyBezier"/> class.
        /// </summary>
        public PolyBezier2D(string definition)
        {
            contours = ParsePathDefString(definition, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolyBezier"/> class with a single <see cref="PolygonContour"/> of a set of <see cref="Point2D"/>s from a parameter list.
        /// </summary>
        /// <param name="points"></param>
        public PolyBezier2D(params Point2D[] points)
            : this(new[] { points })
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolyBezier"/> class with a single <see cref="PolygonContour"/> from a set of <see cref="Point2D"/>s.
        /// </summary>
        /// <param name="points"></param>
        public PolyBezier2D(IEnumerable<Point2D> points)
            : this(new IEnumerable<Point2D>[] { points })
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolyBezier"/> class.
        /// </summary>
        public PolyBezier2D(IEnumerable<PolyBezierContour2D> contours)
        {
            this.contours = contours as List<PolyBezierContour2D> ?? new List<PolyBezierContour2D>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolyBezier"/> class from a parameter list.
        /// </summary>
        /// <param name="contours"></param>
        public PolyBezier2D(params IEnumerable<Point2D>[] contours)
            : this(new List<List<Point2D>>(contours as List<Point2D>[]))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolyBezier"/> class.
        /// </summary>
        /// <param name="contours"></param>
        public PolyBezier2D(IEnumerable<List<Point2D>> contours)
        {
            if (contours is null)
            {
                throw new ArgumentNullException(nameof(contours));
            }

            this.contours = new List<PolyBezierContour2D>();

            foreach (var list in contours)
            {
                var contour = new PolyBezierContour2D();
                foreach (var item in list)
                {
                    contour.AddLineSegment(item);
                }

                this.contours.Add(contour);
            }
        }
        #endregion Constructors

        #region Indexers
        /// <summary>
        /// The Indexer.
        /// </summary>
        /// <param name="index">The index index.</param>
        /// <returns>One element of type PolyBezierContour2D.</returns>
        public PolyBezierContour2D this[int index]
        {
            get { return contours[index]; }
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
        public List<PolyBezierContour2D> Contours
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
        /// Gets the vertices count.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int VerticesCount
        {
            get
            {
                var verticesCount = 0;
                foreach (var contour in contours)
                {
                    verticesCount += contour.Nodes.Count;
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
                static Rectangle2D bounds(List<PolyBezierContour2D> contours)
                {
                    var box = contours[0].Bounds;
                    foreach (var contour in contours)
                    {
                        box = box.Union(contour.Bounds);
                    }

                    return box;
                }
            }
        }
        #endregion Properties

        #region Mutators
        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="contour">The contour.</param>
        public void Add(PolyBezierContour2D contour)
        {
            contours.Add(contour);
            ClearCache();
            update?.Invoke();
        }

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="contour">The contour.</param>
        public void Add(List<BezierSegmentX2D> contour)
        {
            contours.Add(new PolyBezierContour2D(contour));
            ClearCache();
            update?.Invoke();
        }

        /// <summary>
        /// The reverse.
        /// </summary>
        public void Reverse()
        {
            foreach (var poly in contours)
            {
                PolyBezierContour2D.Reverse();
            }

            ClearCache();
        }
        #endregion Mutators

        /// <summary>
        /// Parse the path def string.
        /// </summary>
        /// <param name="pathDefinition">The pathDefinition.</param>
        /// <returns>The <see cref="List{PolyBezierContour}"/>.</returns>
        public static List<PolyBezierContour2D> ParsePathDefString(string pathDefinition)
            => ParsePathDefString(pathDefinition, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse the path def string.
        /// </summary>
        /// <param name="pathDefinition">The pathDefinition.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>The <see cref="List{PolyBezierContour}"/>.</returns>
        public static List<PolyBezierContour2D> ParsePathDefString(string pathDefinition, IFormatProvider provider)
        {
            // These letters are valid PolyBezier2D commands. Split the tokens at these.
            const string separators = @"(?=[MZLCQ])";

            // Discard whitespace and comma but keep the - minus sign.
            var argSeparators = $@"[\s{Tokenizer.GetNumericListSeparator(provider)}]|(?=-)";

            var poly = new List<PolyBezierContour2D>();
            var contour = new PolyBezierContour2D();
            var segment = new BezierSegmentX2D();
            var startPoint = new Point2D();
            var newContour = false;

            // Split the definition string into shape tokens.
            foreach (var token in Regex.Split(pathDefinition, separators).Where(t => !string.IsNullOrWhiteSpace(t)))
            {
                // Get the token type.
                var cmd = token.Take(1).Single();

                // Retrieve the values.
                var args = Regex.Split(token.Substring(1), argSeparators).Where(t => !string.IsNullOrEmpty(t)).Select(arg => double.Parse(arg, NumberStyles.Float, provider)).ToArray();

                switch (cmd)
                {
                    case 'M':
                        // M is Move to.
                        contour = new PolyBezierContour2D(new Point2D(args[0], args[1]));
                        startPoint = segment.Start.Value;
                        newContour = false;
                        break;
                    case 'Z':
                        // Z is End of Path.
                        contour.Closed = true;
                        poly.Add(contour);
                        newContour = true;
                        break;
                    case 'L':
                        // L is a linear curve.
                        contour.AddLineSegment(new Point2D(args[0], args[1]));
                        newContour = false;
                        break;
                    case 'Q':
                        // Q is for Quadratic.
                        contour.AddQuadraticBezier(new Point2D(args[0], args[1]), new Point2D(args[2], args[3]));
                        newContour = false;
                        break;
                    case 'C':
                        // C is for Cubic.
                        contour.AddCubicBezier(new Point2D(args[0], args[1]), new Point2D(args[2], args[3]), new Point2D(args[4], args[5]));
                        newContour = false;
                        break;
                    default:
                        // Unknown element.
                        break;
                }

                if (!newContour)
                {
                    poly.Add(contour);
                }
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
            var output = new StringBuilder();

            foreach (var item in contours)
            {
                output.Append($"{item.ToPathDefString(format, provider)} ");
            }

            return output.ToString().TrimEnd();
        }

        #region Methods
        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="point"></param>
        ///// <returns></returns>
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public override bool Contains(Point2D point)
        //    => Intersections.Contains(this, point) != Inclusion.Outside;

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>The <see cref="PolyBezier"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PolyBezier2D Clone()
            => new PolyBezier2D(Contours.ToArray() as IEnumerable<PolyBezierContour2D>);

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator{PolyBezierContour}"/>.</returns>
        public IEnumerator<PolyBezierContour2D> GetEnumerator()
            => contours.GetEnumerator();

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator()
            => contours.GetEnumerator();

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
                return nameof(PolyBezier2D);
            }

            return $"{nameof(PolyBezier2D)}{{{ToPathDefString(format, provider)}}}";
        }
        #endregion Methods
    }
}
