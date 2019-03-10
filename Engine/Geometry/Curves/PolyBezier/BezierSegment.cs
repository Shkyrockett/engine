// <copyright file="BezierSegment.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Engine
{
    /// <summary>
    /// The bezier segment class.
    /// </summary>
    /// <remarks>
    /// https://github.com/superlloyd/Poly
    /// http://pomax.github.io/bezierinfo/
    /// </remarks>
    [DataContract, Serializable]
    [GraphicsObject]
    //[DisplayName(nameof(BezierSegment))]
    [XmlType(TypeName = "bezier-Segment")]
    public class BezierSegment
        : Shape
    {
        #region Fields
        /// <summary>
        /// The points.
        /// </summary>
        private Point2D[] points;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="BezierSegment"/> class.
        /// </summary>
        public BezierSegment()
            : this(new Point2D[] { })
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BezierSegment"/> class.
        /// </summary>
        /// <param name="points">The points.</param>
        public BezierSegment(IEnumerable<Point2D> points)
            : this(points.ToArray())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BezierSegment"/> class.
        /// </summary>
        /// <param name="points">The points.</param>
        public BezierSegment(params Point2D[] points)
        {
            this.points = points ?? throw new ArgumentNullException(nameof(points));
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// The deconstruct.
        /// </summary>
        /// <param name="points">The points.</param>
        public void Deconstruct(out Point2D[] points)
            => points = this.points;
        #endregion Deconstructors

        #region Indexers
        /// <summary>
        /// The Indexer.
        /// </summary>
        /// <param name="index">The index index.</param>
        /// <returns>One element of type Point2D.</returns>
        public Point2D this[int index]
        {
            get { return points[index]; }
            set { points[index] = value; }
        }
        #endregion Indexers

        #region Properties
        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        [XmlArray]
        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(ArrayConverter))]
        public Point2D[] Points
        {
            get { return points; }
            set
            {
                points = value;
                OnPropertyChanged(nameof(Points));
            }
        }

        /// <summary>
        /// Gets the bounds.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public override Rectangle2D Bounds
            => (Rectangle2D)CachingProperty(() => Measurements.BezierBounds(CurveX, CurveY));

        /// <summary>
        /// Gets the curve x.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Polynomial CurveX
        {
            get
            {
                var curveX = (Polynomial)CachingProperty(() => Polynomial.Bezier(points.Select(p => p.X).ToArray()));
                curveX.IsReadonly = true;
                return curveX;
            }
        }

        /// <summary>
        /// Gets the curve y.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Polynomial CurveY
        {
            get
            {
                var curveY = (Polynomial)CachingProperty(() => Polynomial.Bezier(points.Select(p => p.Y).ToArray()));
                curveY.IsReadonly = true;
                return curveY;
            }
        }

        /// <summary>
        /// Gets the degree.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public PolynomialDegree Degree
            => (PolynomialDegree)(Points.Length - 1);
        #endregion Properties

        //#region Serialization

        ///// <summary>
        ///// Sends an event indicating that this value went into the data file during serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerializing()]
        //private void OnSerializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(BezierSegment)} is being serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was reset after serialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerialized()]
        //private void OnSerialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(BezierSegment)} has been serialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set during deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserializing()]
        //private void OnDeserializing(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(BezierSegment)} is being deserialized.");
        //}

        ///// <summary>
        ///// Sends an event indicating that this value was set after deserialization.
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserialized()]
        //private void OnDeserialized(StreamingContext context)
        //{
        //    Debug.WriteLine($"{nameof(BezierSegment)} has been deserialized.");
        //}

        //#endregion

        #region Methods
        /// <summary>
        /// Creates a string representation of this <see cref="PolygonContour"/> struct based on the format string
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
                return nameof(BezierSegment);
            }

            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(BezierSegment)}{{{string.Join(sep.ToString(), points)}}}";
            return formatable.ToString(format, provider);
        }
        #endregion Methods
    }
}
