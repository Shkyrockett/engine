// <copyright file="Polycurve.cs" company="Shkyrockett" >
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
    /// <remarks></remarks>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(Polycurve))]
    [XmlType(TypeName = "PolycurveSet", Namespace = "http://www.w3.org/2000/svg")]
    public class Polycurve
        : Shape, IEnumerable<PolycurveContour>
    {
        #region Private Fields
        /// <summary>
        /// An array of Polygon Contours.
        /// </summary>
        /// <remarks></remarks>
        [DataMember, XmlAttribute, SoapAttribute]
        private List<PolycurveContour> contours;
        #endregion Private Fields

        #region Constructors
        /// <summary>
        /// Initializes a default instance of the <see cref="Polycurve"/> class.
        /// </summary>
        public Polycurve()
            : this(new List<PolycurveContour>())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polycurve"/> class.
        /// </summary>
        public Polycurve(IEnumerable<PolycurveContour> contours)
        {
            this.contours = contours as List<PolycurveContour>;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// The deconstruct.
        /// </summary>
        /// <param name="items">The items.</param>
        public void Deconstruct(out List<PolycurveContour> items)
            => items = contours;
        #endregion Deconstructors

        #region Indexers
        /// <summary>
        /// The Indexer.
        /// </summary>
        /// <param name="index">The index index.</param>
        /// <returns>One element of type PolycurveContour.</returns>
        public PolycurveContour this[int index]
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
        public List<PolycurveContour> Contours
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
                    verticesCount += c.Nodes.Count;

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

                Rectangle2D bounds(List<PolycurveContour> contours)
                {
                    var bb = contours[0].Bounds;

                    foreach (var c in contours)
                        bb = bb.Union(c.Bounds);

                    return bb;
                }
            }
        }
        #endregion Properties

        //#region Serialization

        ///// <summary>
        ///// Sends an event indicating that this value went into the data file during serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerializing()]
        //private void OnSerializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(Polygon)} is being serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was reset after serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerialized()]
        //private void OnSerialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(Polygon)} has been serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set during deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserializing()]
        //private void OnDeserializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(Polygon)} is being deserialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set after deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserialized()]
        //private void OnDeserialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(Polygon)} has been deserialized.");
        //}

        //#endregion

        /// <summary>
        /// Parse the path def string.
        /// </summary>
        /// <param name="pathDefinition">The pathDefinition.</param>
        /// <returns>The <see cref="T:List{PolycurveContour}"/>.</returns>
        public static List<PolycurveContour> ParsePathDefString(string pathDefinition)
            => ParsePathDefString(pathDefinition, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse the path def string.
        /// </summary>
        /// <param name="pathDefinition">The pathDefinition.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>The <see cref="T:List{PolycurveContour}"/>.</returns>
        public static List<PolycurveContour> ParsePathDefString(string pathDefinition, IFormatProvider provider)
        {
            // These letters are valid PolyBezier commands. Split the tokens at these.
            const string separators = @"(?=[MZ])";

            var contours = new List<PolycurveContour>();

            // Split the definition string into shape tokens.
            foreach (var token in Regex.Split(pathDefinition, separators).Where(t => !string.IsNullOrWhiteSpace(t)))
            {
                // Get the token type.
                var cmd = token.Take(1).Single();

                switch (cmd)
                {
                    case 'M':
                        // M is Move to.
                        contours.Add(new PolycurveContour(PolycurveContour.ParsePathDefString(token.Substring(1)).Item1));
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
        private string ToPathDefString()
            => ToPathDefString(null, CultureInfo.InvariantCulture);

        /// <summary>
        /// The to path def string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private string ToPathDefString(string format, IFormatProvider provider)
        {
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
        public void Add(PolycurveContour contour)
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
        public Polycurve Clone()
            => new Polycurve(Contours.ToArray() as IEnumerable<PolycurveContour>);

        /// <summary>
        /// Get the enumerator.
        /// </summary>
        /// <returns>The <see cref="T:IEnumerator{PolycurveContour}"/>.</returns>
        public IEnumerator<PolycurveContour> GetEnumerator()
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
            if (this is null) return nameof(Polycurve);
            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Polycurve)}{{{string.Join(sep.ToString(), Contours)}}}";
            return formatable.ToString(format, provider);
        }
        #endregion Methods
    }
}
