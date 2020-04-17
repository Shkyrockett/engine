// <copyright file="BezierSegment2D.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
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
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// The bezier segment class.
    /// </summary>
    /// <remarks>
    /// <para>https://github.com/superlloyd/Poly
    /// http://pomax.github.io/bezierinfo/</para>
    /// </remarks>
    [DataContract, Serializable]
    [GraphicsObject]
    //[DisplayName(nameof(BezierSegment2D))]
    [XmlType(TypeName = "bezier-Segment")]
    [DebuggerDisplay("{ToString()}")]
    public class BezierSegment2D
        : Shape2D
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
        public BezierSegment2D()
            : this(Array.Empty<Point2D>())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BezierSegment"/> class.
        /// </summary>
        /// <param name="points">The points.</param>
        public BezierSegment2D(IEnumerable<Point2D> points)
            : this(points.ToArray())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BezierSegment"/> class.
        /// </summary>
        /// <param name="points">The points.</param>
        public BezierSegment2D(params Point2D[] points)
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
        public override Rectangle2D Bounds => (Rectangle2D)CachingProperty(() => Measurements.BezierBounds(CurveX, CurveY));

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
        public PolynomialDegree Degree => (PolynomialDegree)(Points.Length - 1);
        #endregion Properties

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
                return nameof(BezierSegment2D);
            }

            var sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(BezierSegment2D)}{{{string.Join(sep.ToString(), points)}}}";
            return formatable.ToString(format, provider);
        }
        #endregion Methods
    }
}
