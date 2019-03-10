// <copyright file="PolyBezier.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
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
    /// A closed Polygon made up of sets of Bezier Contours.
    /// </summary>
    /// <structure>Engine.Geometry.PolyGon2D</structure>
    [DataContract, Serializable]
    //[GraphicsObject]
    //[DisplayName(nameof(PolyBezier))]
    [XmlType(TypeName = "polybezier", Namespace = "shapes")]
    public class PolyBezier
        : Shape, IEnumerable<PolyBezierContour>
    {
        #region Fields
        /// <summary>
        /// An array of Polygon Contours.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        private List<PolyBezierContour> contours;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a default instance of the <see cref="PolyBezier"/> class.
        /// </summary>
        public PolyBezier()
            : this(new List<PolyBezierContour>())
        { }

        /// <summary>
        /// Initializes a default instance of the <see cref="PolyBezier"/> class.
        /// </summary>
        public PolyBezier(string definition)
        {
            contours = ParsePathDefString(definition);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolyBezier"/> class with a single <see cref="PolygonContour"/> of a set of <see cref="Point2D"/>s from a parameter list.
        /// </summary>
        /// <param name="points"></param>
        public PolyBezier(params Point2D[] points)
            : this(new[] { points })
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolyBezier"/> class with a single <see cref="PolygonContour"/> from a set of <see cref="Point2D"/>s.
        /// </summary>
        /// <param name="points"></param>
        public PolyBezier(IEnumerable<Point2D> points)
            : this(new IEnumerable<Point2D>[] { points })
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolyBezier"/> class.
        /// </summary>
        public PolyBezier(IEnumerable<PolyBezierContour> contours)
        {
            this.contours = contours as List<PolyBezierContour> ?? new List<PolyBezierContour>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolyBezier"/> class from a parameter list.
        /// </summary>
        /// <param name="contours"></param>
        public PolyBezier(params IEnumerable<Point2D>[] contours)
            : this(new List<List<Point2D>>(contours as List<Point2D>[]))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolyBezier"/> class.
        /// </summary>
        /// <param name="contours"></param>
        public PolyBezier(IEnumerable<List<Point2D>> contours)
        {
            if (contours is null)
            {
                throw new ArgumentNullException(nameof(contours));
            }

            this.contours = new List<PolyBezierContour>();

            foreach (var list in contours)
            {
                var contour = new PolyBezierContour();
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
        /// <returns>One element of type PolyBezierContour.</returns>
        public PolyBezierContour this[int index]
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
        public List<PolyBezierContour> Contours
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
                contours = ParsePathDefString(value);
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

                Rectangle2D bounds(List<PolyBezierContour> contours)
                {
                    var box = contours[0].Bounds;
                    foreach (PolyBezierContour contour in contours)
                    {
                        box = box.Union(contour.Bounds);
                    }

                    return box;
                }
            }
        }
        #endregion Properties

        //#region Serialization

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerializing()]
        //private void OnSerializing(StreamingContext context)
        //{
        //    // Assert("This value went into the data file during serialization.");
        //}

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerialized()]
        //private void OnSerialized(StreamingContext context)
        //{
        //    // Assert("This value was reset after serialization.");
        //}

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserializing()]
        //private void OnDeserializing(StreamingContext context)
        //{
        //    // Assert("This value was set during deserialization");
        //}

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserialized()]
        //private void OnDeserialized(StreamingContext context)
        //{
        //    // Assert("This value was set after deserialization.");
        //}

        //#endregion

        #region Mutators
        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="contour">The contour.</param>
        public void Add(PolyBezierContour contour)
        {
            contours.Add(contour);
            ClearCache();
            update?.Invoke();
        }

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="contour">The contour.</param>
        public void Add(List<BezierSegmentX> contour)
        {
            contours.Add(new PolyBezierContour(contour));
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
                PolyBezierContour.Reverse();
            }

            ClearCache();
        }
        #endregion Mutators

        /// <summary>
        /// Parse the path def string.
        /// </summary>
        /// <param name="pathDefinition">The pathDefinition.</param>
        /// <returns>The <see cref="T:List{PolyBezierContour}"/>.</returns>
        public static List<PolyBezierContour> ParsePathDefString(string pathDefinition)
            => ParsePathDefString(pathDefinition, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse the path def string.
        /// </summary>
        /// <param name="pathDefinition">The pathDefinition.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>The <see cref="T:List{PolyBezierContour}"/>.</returns>
        public static List<PolyBezierContour> ParsePathDefString(string pathDefinition, IFormatProvider provider)
        {
            // These letters are valid PolyBezier commands. Split the tokens at these.
            const string separators = @"(?=[MZLCQ])";

            // Discard whitespace and comma but keep the - minus sign.
            var argSeparators = $@"[\s{Tokenizer.GetNumericListSeparator(provider)}]|(?=-)";

            var poly = new List<PolyBezierContour>();
            var contour = new PolyBezierContour();
            var segment = new BezierSegmentX();
            var startPoint = new Point2D();
            var newContour = false;

            // Split the definition string into shape tokens.
            foreach (var token in Regex.Split(pathDefinition, separators).Where(t => !string.IsNullOrWhiteSpace(t)))
            {
                // Get the token type.
                var cmd = token.Take(1).Single();

                // Retrieve the values.
                var args = Regex.Split(token.Substring(1), argSeparators).Where(t => !string.IsNullOrEmpty(t)).Select(arg => double.Parse(arg)).ToArray();

                switch (cmd)
                {
                    case 'M':
                        // M is Move to.
                        contour = new PolyBezierContour(new Point2D(args[0], args[1]));
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
        public PolyBezier Clone()
            => new PolyBezier(Contours.ToArray() as IEnumerable<PolyBezierContour>);

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="T:IEnumerator{PolyBezierContour}"/>.</returns>
        public IEnumerator<PolyBezierContour> GetEnumerator()
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
                return nameof(PolyBezier);
            }

            return $"{nameof(PolyBezier)}{{{ToPathDefString(format, provider)}}}";
        }
        #endregion Methods
    }
}
