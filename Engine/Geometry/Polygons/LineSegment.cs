// <copyright file="LineSegment.cs" company="Shkyrockett" >
//     Copyright (c) 2005 - 2017 Shkyrockett. All rights reserved.
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
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// 2D Line Segment Structure
    /// </summary>
    /// <structure>Engine.Geometry.Segment2D</structure>
    /// <remarks></remarks>
    [Serializable]
    [GraphicsObject]
    [DisplayName(nameof(LineSegment))]
    [XmlType(TypeName = "line-segment")]
    public class LineSegment
        : Shape, IOpenShape
    {
        #region Static Fields
        /// <summary>
        /// Represents a Engine.Geometry.Segment that is null.
        /// </summary>
        /// <remarks></remarks>
        public static readonly LineSegment Empty = new LineSegment();
        #endregion

        #region Private Fields

        /// <summary>
        /// 
        /// </summary>
        private double aX;

        /// <summary>
        /// 
        /// </summary>
        private double aY;

        /// <summary>
        /// 
        /// </summary>
        private double bX;

        /// <summary>
        /// 
        /// </summary>
        private double bY;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment"/> class.
        /// </summary>
        public LineSegment()
            : this(Point2D.Empty, Point2D.Empty)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment"/> class.
        /// </summary>
        /// <param name="tuple"></param>
        /// <remarks></remarks>
        public LineSegment((double x1, double y1, double x2, double y2) tuple)
            : this(tuple.x1, tuple.y1, tuple.x2, tuple.y2)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment"/> class.
        /// </summary>
        /// <param name="x1">Horizontal component of starting point</param>
        /// <param name="y1">Vertical component of starting point</param>
        /// <param name="X2">Horizontal component of ending point</param>
        /// <param name="Y2">Vertical component of ending point</param>
        /// <remarks></remarks>
        public LineSegment(double x1, double y1, double X2, double Y2)
            : this(new Point2D(x1, y1), new Point2D(X2, Y2))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment"/> class.
        /// </summary>
        /// <param name="Point">Starting Point</param>
        /// <param name="RadAngle">Ending Angle</param>
        /// <param name="Radius">Ending Line Segment Length</param>
        /// <remarks></remarks>
        public LineSegment(Point2D Point, double RadAngle, double Radius)
            : this(new Point2D(Point.X, Point.Y), new Point2D((Point.X + (Radius * Cos(RadAngle))), (Point.Y + (Radius * Sin(RadAngle)))))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment"/> class.
        /// </summary>
        /// <param name="a">Starting Point</param>
        /// <param name="b">Ending Point</param>
        /// <remarks></remarks>
        public LineSegment(Point2D a, Point2D b)
        {
            A = a;
            B = b;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        public LineSegment(List<Point2D> points)
            => Points = points;

        #endregion

        #region Deconstructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        public void Deconstruct(out double aX, out double aY, out double bX, out double bY)
        {
            aX = this.aX;
            aY = this.aY;
            bX = this.bX;
            bY = this.bY;
        }

        #endregion

        #region Indexers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        public Point2D this[int index]
        {
            get { return (Points as List<Point2D>)[index]; }
            set
            {
                (Points as List<Point2D>)[index] = value;
                OnPropertyChanged(nameof(Points));
                update?.Invoke();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get or sets an array of points representing a line segment.
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore, SoapIgnore]
        public List<Point2D> Points
        {
            get { return new List<Point2D> { A, B }; }
            set
            {
                A = value[0];
                B = value[1];
                ClearCache();
                OnPropertyChanged(nameof(Points));
            }
        }

        /// <summary>
        /// First Point of a line segment
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The first Point of a line segment")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        public Point2D A
        {
            get { return new Point2D(aX, aY); }
            set
            {
                aX = value.X;
                aY = value.Y;
                ClearCache();
                OnPropertyChanged(nameof(A));
            }
        }

        /// <summary>
        /// Gets or sets the X coordinate of the first Point of a line segment.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute("ax")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The X coordinate of the first Point of a line segment.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double AX
        {
            get { return aX; }
            set
            {
                aX = value;
                ClearCache();
                OnPropertyChanged(nameof(AX));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the Y coordinate of the first Point of a line segment.
        /// </summary>
        [XmlAttribute("ay")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The y coordinate of the first Point of a line segment.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double AY
        {
            get { return aY; }
            set
            {
                aY = value;
                ClearCache();
                OnPropertyChanged(nameof(AY));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Ending Point of a Line Segment
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore, SoapIgnore]
        [Category("Properties")]
        [Description("The ending Point of a Line Segment")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        public Point2D B
        {
            get { return new Point2D(bX, bY); }
            set
            {
                bX = value.X;
                bY = value.Y;
                ClearCache();
                OnPropertyChanged(nameof(B));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the X coordinate of the second Point of a line segment.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute("bx")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The X coordinate of the second Point of a line segment.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double BX
        {
            get { return bX; }
            set
            {
                bX = value;
                ClearCache();
                OnPropertyChanged(nameof(BX));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or sets the Y coordinate of the second Point of a line segment.
        /// </summary>
        [XmlAttribute("by")]
        [Browsable(true)]
        [Category("Elements")]
        [Description("The y coordinate of the second Point of a line segment.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [RefreshProperties(RefreshProperties.All)]
        public double BY
        {
            get { return bY; }
            set
            {
                bY = value;
                ClearCache();
                OnPropertyChanged(nameof(BY));
                update?.Invoke();
            }
        }

        /// <summary>
        /// Gets or the size and location of the segment, in floating-point pixels, relative to the parent canvas.
        /// </summary>
        /// <returns>A System.Drawing.RectangleF in floating-point pixels relative to the parent canvas that represents the size and location of the segment.</returns>
        /// <remarks></remarks>
        [XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => (Rectangle2D)CachingProperty(() => Measurements.LineSegmentBounds(A.X, A.Y, B.X, B.Y));

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public double Length
            => (double)CachingProperty(() => Measurements.Distance(A.X, A.Y, B.X, B.Y));

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Polynomial CurveX
        {
            get
            {
                var curveX = (Polynomial)CachingProperty(() => Polynomial.Bezier(Points.Select(p => p.X).ToArray()));
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
                var curveY = (Polynomial)CachingProperty(() => Polynomial.Bezier(Points.Select(p => p.Y).ToArray()));
                curveY.IsReadonly = true;
                return curveY;
            }
        }

        /// <summary>
        /// Return the point of the segment with lexicographically smallest coordinate.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Point2D Min
            => (aX < bX) || (aX == bX && aY < bY) ? new Point2D(aX, aY) : new Point2D(bX, bY);

        /// <summary>
        /// Return the point of the segment with lexicographically largest coordinate.
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public Point2D Max
            => (aX > bX) || (aX == bX && aY > bY) ? new Point2D(aX, aY) : new Point2D(bX, bY);

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore, SoapIgnore]
        public PolynomialDegree Degree
            => PolynomialDegree.Linear;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [XmlIgnore, SoapIgnore]
        public bool Degenerate
            => aX == bX && aY == bY;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [XmlIgnore, SoapIgnore]
        public bool IsHorizontal
            => aY == bY;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [XmlIgnore, SoapIgnore]
        public bool IsVertical
            => aX == bX;

        #endregion

        #region Operators

        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <returns></returns>
        /// <param name="tuple"></param>
        public static implicit operator LineSegment((double I, double J, double K, double L) tuple)
            => new LineSegment(tuple);

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

        #region Interpolators

        /// <summary>
        /// Interpolates a shape.
        /// </summary>
        /// <param name="t">Index of the point to interpolate.</param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        public override Point2D Interpolate(double t)
            => Interpolaters.Linear(A, B, t);

        #endregion

        #region Mutators

        /// <summary>
        /// 
        /// </summary>
        public void Reverse()
        {
            Point2D temp = A;
            A = B;
            B = temp;
            update?.Invoke();
            Refresh();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns>an array of points</returns>
        /// <remarks></remarks>
        public Point2D[] ToArray()
            => new Point2D[] { A, B };

        /// <summary>
        /// Creates a string representation of this <see cref="LineSegment"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public override string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(LineSegment);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(LineSegment)}{{{nameof(A)}={A.ConvertToString(format, provider)},{nameof(B)}={B.ConvertToString(format, provider)}}}";
        }

        #endregion
    }
}

