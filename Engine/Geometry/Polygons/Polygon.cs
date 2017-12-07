﻿// <copyright file="PolygonSet.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2017 Shkyrockett. All rights reserved.
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
    [DisplayName(nameof(Polygon))]
    [XmlType(TypeName = "polygonSet", Namespace = "http://www.w3.org/2000/svg")]
    public class Polygon
        : Shape, IEnumerable<PolygonContour>
    {
        #region Private Fields

        /// <summary>
        /// An array of Polygon Contours.
        /// </summary>
        /// <remarks></remarks>
        [DataMember, XmlAttribute, SoapAttribute]
        private List<PolygonContour> contours;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a default instance of the <see cref="Polygon"/> class.
        /// </summary>
        public Polygon()
            : this(new List<PolygonContour>())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class with a single <see cref="PolygonContour"/> of a set of <see cref="Point2D"/>s from a parameter list.
        /// </summary>
        /// <param name="points"></param>
        public Polygon(params Point2D[] points)
            : this(new[] { points })
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class with a single <see cref="PolygonContour"/> from a set of <see cref="Point2D"/>s.
        /// </summary>
        /// <param name="points"></param>
        public Polygon(IEnumerable<Point2D> points)
            : this(new IEnumerable<Point2D>[] { points })
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class.
        /// </summary>
        public Polygon(IEnumerable<PolygonContour> contours)
        {
            this.contours = contours as List<PolygonContour>;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class from a parameter list.
        /// </summary>
        /// <param name="contours"></param>
        public Polygon(params IEnumerable<Point2D>[] contours)
        {
            this.contours = new List<PolygonContour>() ?? throw new ArgumentNullException(nameof(contours), "Argument must not be null.");

            foreach (var list in contours)
            {
                this.contours.Add(new PolygonContour(list));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class.
        /// </summary>
        /// <param name="contours"></param>
        public Polygon(IEnumerable<List<Point2D>> contours)
        {
            this.contours = new List<PolygonContour>() ?? throw new ArgumentNullException(nameof(contours), "Argument must not be null.");

            foreach (var list in contours)
            {
                this.contours.Add(new PolygonContour(list));
            }
        }

        #endregion

        #region Indexers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public PolygonContour this[int index]
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

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<PolygonContour> Contours
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
        /// 
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
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int Count
            => contours.Count;

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int Capacity { get { return contours.Capacity; } set { contours.Capacity = value; } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public int VerticesCount
        {
            get
            {
                var verticesCount = 0;
                foreach (var c in contours)
                    verticesCount += c.Points.Count;

                return verticesCount;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override double Perimeter
            => contours.Sum(p => p.Perimeter);

        /// <summary>
        /// 
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

                Rectangle2D bounds(List<PolygonContour> contours)
                {
                    Rectangle2D bb = contours[0].Bounds;

                    foreach (PolygonContour c in contours)
                        bb = bb.Union(c.Bounds);

                    return bb;
                }
            }
        }

        #endregion

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
        /// 
        /// </summary>
        /// <param name="pathDefinition"></param>
        /// <returns></returns>
        public static List<PolygonContour> ParsePathDefString(string pathDefinition)
            => ParsePathDefString(pathDefinition, CultureInfo.InvariantCulture);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathDefinition"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static List<PolygonContour> ParsePathDefString(string pathDefinition, IFormatProvider provider)
        {
            // These letters are valid PolyBezier commands. Split the tokens at these.
            var separators = @"(?=[M])";
            //string separators = @"(?=[MZ])";

            var contours = new List<PolygonContour>();

            // Split the definition string into shape tokens.
            foreach (var token in Regex.Split(pathDefinition, separators).Where(t => !string.IsNullOrWhiteSpace(t)))
            {
                // Get the token type.
                var cmd = token.Take(1).Single();

                switch (cmd)
                {
                    case 'M':
                        // M is Move to.
                        contours.Add(new PolygonContour(PolygonContour.ParsePathDefString(token.Substring(1))));
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
        /// 
        /// </summary>
        /// <returns></returns>
        private String ToPathDefString()
            => ToPathDefString(null, CultureInfo.InvariantCulture);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        private String ToPathDefString(string format, IFormatProvider provider)
        {
            var output = new StringBuilder();

            foreach (var contour in contours)
            {
                output.Append($"M{contour.Definition} ");
            }

            return output.ToString().TrimEnd();
        }

        #region Mutators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contour"></param>
        public void Add(PolygonContour contour)
        {
            contours.Add(contour);
            update?.Invoke();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contour"></param>
        public void Add(List<Point2D> contour)
        {
            contours.Add(new PolygonContour(contour));
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
        /// 
        /// </summary>
        public void Reverse()
        {
            foreach (var poly in contours)
            {
                poly.Reverse();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Contains(Point2D point)
            => Intersections.Contains(this, point) != Inclusion.Outside;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polygon Clone()
            => new Polygon(Contours.ToArray() as IEnumerable<PolygonContour>);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<PolygonContour> GetEnumerator()
            => contours.GetEnumerator();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
            => contours.GetEnumerator();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Polygon);
            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Polygon)}{{{string.Join(sep.ToString(), Contours)}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
