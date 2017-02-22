// <copyright file="BezierSegment.cs" >
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
using static System.Math;
using static Engine.Maths;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Engine
{
    /// <summary>
    /// Utility class about 2D Bezier curves
    /// Aka a segment, or quadratic or cubic bezier curve.
    /// Extensive Bezier explanation can be found at http://pomax.github.io/bezierinfo/
    /// </summary>
    /// <remarks> https://github.com/superlloyd/Poly </remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(QuadraticBezier))]
    [XmlType(TypeName = "Polybezier-Segment")]
    public class PolyBezierSegment
        : Shape, IOpenShape
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        Point2D[] handles;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a default instance of the <see cref="PolyBezierSegment"/> class with no terms.
        /// </summary>
        public PolyBezierSegment()
        {
            handles = new Point2D[] { };
            Previous = this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolyBezierSegment"/> class as a single point constant term curve.
        /// </summary>
        public PolyBezierSegment(Point2D point)
        {
            handles = new[] { point };
            Previous = this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolyBezierSegment"/> class as a line segment from the previous curve.
        /// </summary>
        /// <param name="previous">The previous curve.</param>
        /// <param name="point">The next point.</param>
        public PolyBezierSegment(PolyBezierSegment previous, Point2D point)
            : this(previous, new[] { point })
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolyBezierSegment"/> class as a quadratic bezier curve from the previous curve.
        /// </summary>
        /// <param name="previous">The previous curve.</param>
        /// <param name="handle">The quadratic curve handle.</param>
        /// <param name="point">The next point.</param>
        public PolyBezierSegment(PolyBezierSegment previous, Point2D handle, Point2D point)
            : this(previous, new[] { handle, point })
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolyBezierSegment"/> class as a quadratic bezier curve from the previous curve.
        /// </summary>
        /// <param name="previous">The previous curve.</param>
        /// <param name="handle1">The first cubic curve handle.</param>
        /// <param name="handle2">The second cubic curve handle.</param>
        /// <param name="point">The next point.</param>
        public PolyBezierSegment(PolyBezierSegment previous, Point2D handle1, Point2D handle2, Point2D point)
            : this(previous, new[] { handle1, handle2, point })
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PolyBezierSegment"/> class using a parameter array.
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="points"></param>
        public PolyBezierSegment(PolyBezierSegment previous, params Point2D[] points)
            : base()
            => this.handles = points;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="points"></param>
        public PolyBezierSegment(PolyBezierSegment previous, IEnumerable<Point2D> points)
            : this(previous, points.ToArray())
        { }

        #endregion

        #region Deconstructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public void Deconstruct(out Point2D[] points)
        {
            points = this.handles;
        }

        #endregion

        #region Indexers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Point2D this[int index]
        {
            get { return handles[index]; }
            set { handles[index] = value; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a reference to the previous geometric item.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public PolyBezierSegment Previous { get; set; }

        /// <summary>
        /// Gets or sets a reference to the end point of the previous <see cref="PolyBezierSegment"/> to use as the stating point.
        /// </summary>
        [XmlElement, SoapElement]
        public Point2D? Start
        {
            get { return Previous.End; }
            set
            {
                Previous.End = value;
                ClearCache();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlArray]
        [RefreshProperties(RefreshProperties.All)]
        public Point2D[] Handles
        {
            get { return handles.Slice(0, handles.Length - 2); }
            set
            {
                var temp = new[] { handles[handles.Length - 1] };
                handles = value.Concat(temp).ToArray();
                ClearCache();
                OnPropertyChanged(nameof(Handles));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Point2D? NextToEnd
        {
            get { return (handles.Length >= 2) ? handles[handles.Length - 2] : Start; }
            set
            {
                if (handles.Length >= 2) handles[handles.Length - 2] = value.Value;
                else Start = value.Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Point2D? End
        {
            get { return handles[handles.Length - 1]; }
            set { handles[handles.Length - 1] = value.Value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
        {
            get
            {
                return (Rectangle2D)CachingProperty(() => bounds());

                Rectangle2D bounds()
                {
                    (double x0, double x1) = CurveX.GetMinMax(0, 1);
                    (double y0, double y1) = CurveY.GetMinMax(0, 1);
                    return new Rectangle2D(x0, y0, x1 - x0, y1 - y0);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public double Length
            => 0;

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Polynomial CurveX
        {
            get
            {
                var curveX = (Polynomial)CachingProperty(() => Polynomial.Bezier(handles.Concat(new[] { Start.Value }).Select(p => p.X).ToArray()));
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
                var curveY = (Polynomial)CachingProperty(() => Polynomial.Bezier(handles.Concat(new[] { Start.Value }).Select(p => p.Y).ToArray()));
                curveY.IsReadonly = true;
                return curveY;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public PolynomialDegree Degree
            => (PolynomialDegree)(Handles.Length);

        #endregion

        #region Serialization

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnSerializing()]
        protected void OnSerializing(StreamingContext context)
        {
            // Assert("This value went into the data file during serialization.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnSerialized()]
        protected void OnSerialized(StreamingContext context)
        {
            // Assert("This value was reset after serialization.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnDeserializing()]
        protected void OnDeserializing(StreamingContext context)
        {
            // Assert("This value was set during deserialization");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        [OnDeserialized()]
        protected void OnDeserialized(StreamingContext context)
        {
            // Assert("This value was set after deserialization.");
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
            IFormattable formatable = $"{nameof(BezierSegment)}{{{string.Join(sep.ToString(), handles)}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
