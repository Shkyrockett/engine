// <copyright file="BezierSegment.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
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
    /// 
    /// </summary>
    /// <remarks>
    /// https://github.com/superlloyd/Poly
    /// http://pomax.github.io/bezierinfo/
    /// </remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(BezierSegment))]
    [XmlType(TypeName = "bezier-Segment")]
    public class BezierSegment
        : Shape
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        Point2D[] points;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public BezierSegment()
            : this(new Point2D[] { })
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public BezierSegment(IEnumerable<Point2D> points)
            : this(points.ToArray())
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public BezierSegment(params Point2D[] points)
            : base()
        {
            this.points = points;
        }

        #endregion

        #region Deconstructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public void Deconstruct(out Point2D[] points)
            => points = this.points;

        #endregion

        #region Indexers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Point2D this[int index]
        {
            get { return points[index]; }
            set { points[index] = value; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        [XmlArray]
        [RefreshProperties(RefreshProperties.All)]
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
        /// 
        /// </summary>
        /// <returns></returns>
        [XmlIgnore, SoapIgnore]
        public override Rectangle2D Bounds
            => (Rectangle2D)CachingProperty(() => Measurements.BezierBounds(CurveX, CurveY));

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
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
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
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
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public PolynomialDegree Degree
            => (PolynomialDegree)(Points.Length - 1);

        #endregion

        #region Serialization

        /// <summary>
        /// Sends an event indicating that this value went into the data file during serialization.
        /// </summary>
        /// <param name="context"></param>
        [OnSerializing()]
        private void OnSerializing(StreamingContext context)
        {
            Debug.WriteLine($"{nameof(BezierSegment)} is being serialized.");
        }

        /// <summary>
        /// Sends an event indicating that this value was reset after serialization.
        /// </summary>
        /// <param name="context"></param>
        [OnSerialized()]
        private void OnSerialized(StreamingContext context)
        {
            Debug.WriteLine($"{nameof(BezierSegment)} has been serialized.");
        }

        /// <summary>
        /// Sends an event indicating that this value was set during deserialization.
        /// </summary>
        /// <param name="context"></param>
        [OnDeserializing()]
        private void OnDeserializing(StreamingContext context)
        {
            Debug.WriteLine($"{nameof(BezierSegment)} is being deserialized.");
        }

        /// <summary>
        /// Sends an event indicating that this value was set after deserialization.
        /// </summary>
        /// <param name="context"></param>
        [OnDeserialized()]
        private void OnDeserialized(StreamingContext context)
        {
            Debug.WriteLine($"{nameof(BezierSegment)} has been deserialized.");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a string representation of this <see cref="Contour"/> struct based on the format string
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
            if (this == null) return nameof(BezierSegment);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(BezierSegment)}{{{string.Join(sep.ToString(), points)}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
