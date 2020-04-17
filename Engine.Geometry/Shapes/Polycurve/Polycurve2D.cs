// <copyright file="Polycurve2D.cs" company="Shkyrockett" >
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
    /// A closed Polygon2D made up of sets of Contours.
    /// </summary>
    /// <structure>Engine.Geometry.PolyGon2D</structure>

    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Polycurve2D))]
    [XmlType(TypeName = "PolycurveSet", Namespace = "http://www.w3.org/2000/svg")]
    [DebuggerDisplay("{ToString()}")]
    public class Polycurve2D
        : Shape2D, IEnumerable<PolycurveContour2D>
    {
        #region Private Fields
        /// <summary>
        /// An array of Polygon2D Contours.
        /// </summary>

        [DataMember, XmlAttribute, SoapAttribute]
        private List<PolycurveContour2D> contours;
        #endregion Private Fields

        #region Constructors
        /// <summary>
        /// Initializes a default instance of the <see cref="Polycurve"/> class.
        /// </summary>
        public Polycurve2D()
            : this(new List<PolycurveContour2D>())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polycurve"/> class.
        /// </summary>
        public Polycurve2D(IEnumerable<PolycurveContour2D> contours)
        {
            this.contours = contours as List<PolycurveContour2D> ?? new List<PolycurveContour2D>();
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// The deconstruct.
        /// </summary>
        /// <param name="items">The items.</param>
        public void Deconstruct(out List<PolycurveContour2D> items)
            => items = contours;
        #endregion Deconstructors

        #region Indexers
        /// <summary>
        /// The Indexer.
        /// </summary>
        /// <param name="index">The index index.</param>
        /// <returns>One element of type PolycurveContour2D.</returns>
        public PolycurveContour2D this[int index]
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
        public List<PolycurveContour2D> Contours
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

        ///// <summary>
        /////
        ///// </summary>
        //[Browsable(false)]
        //[XmlAttribute("d"), SoapAttribute("d")]
        //[RefreshProperties(RefreshProperties.All)]
        //[EditorBrowsable(EditorBrowsableState.Advanced)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public string Definition
        //{
        //    get { return ToPathDefString(); }
        //    set
        //    {
        //        contours = ParsePathDefString(value);
        //        ClearCache();
        //        OnPropertyChanged(nameof(Definition));
        //        update?.Invoke();
        //    }
        //}

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
                foreach (var c in contours)
                {
                    verticesCount += c.Nodes.Count;
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
                static Rectangle2D bounds(List<PolycurveContour2D> contours)
                {
                    var bb = contours[0].Bounds;

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
        public static List<PolycurveContour2D> ParsePathDefString(string pathDefinition)
            => ParsePathDefString(pathDefinition, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse the path def string.
        /// </summary>
        /// <param name="pathDefinition">The pathDefinition.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<PolycurveContour2D> ParsePathDefString(string pathDefinition, IFormatProvider provider)
        {
            _ = provider;
            // These letters are valid PolyBezier2D commands. Split the tokens at these.
            const string separators = @"(?=[MZ])";

            var contours = new List<PolycurveContour2D>();

            // Split the definition string into shape tokens.
            foreach (var token in Regex.Split(pathDefinition, separators).Where(t => !string.IsNullOrWhiteSpace(t)))
            {
                // Get the token type.
                var cmd = token.Take(1).Single();

                switch (cmd)
                {
                    case 'M':
                        // M is Move to.
                        contours.Add(new PolycurveContour2D(PolycurveContour2D.ParsePathDefString(token.Substring(1), provider).Item1));
                        break;
                    case 'Z':
                        // Z is End of Path.
                        //contours.Closed = true;
                        break;
                }

            }

            return contours;
        }

        /// <summary>
        /// The to path def string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string ToPathDefString()
            => ToPathDefString(string.Empty, CultureInfo.InvariantCulture);

        /// <summary>
        /// The to path def string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string ToPathDefString(string format, IFormatProvider provider)
        {
            _ = format;
            _ = provider;
            var output = new StringBuilder();

            foreach (var contour in contours)
            {
                output.Append($"M{contour.Definition} Z");
            }

            return output.ToString().TrimEnd();
        }

        #region Mutators
        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="contour">The contour.</param>
        public void Add(PolycurveContour2D contour)
        {
            contours.Add(contour);
            update?.Invoke();
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
        /// <returns>The <see cref="Polycurve"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polycurve2D Clone()
            => new Polycurve2D(Contours.ToArray() as IEnumerable<PolycurveContour2D>);

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator{T}"/>.</returns>
        public IEnumerator<PolycurveContour2D> GetEnumerator()
            => contours.GetEnumerator();

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

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
                return nameof(Polycurve2D);
            }

            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Polycurve2D)}{{{string.Join(sep.ToString(), Contours)}}}";
            return formatable.ToString(format, provider);
        }
        #endregion Methods
    }
}
