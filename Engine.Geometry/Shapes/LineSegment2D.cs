// <copyright file="LineSegment2D.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
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
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static Engine.Operations;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// 2D Line Segment Structure
    /// </summary>
    /// <seealso cref="Engine.Shape2D" />
    /// <seealso cref="Engine.IShapeSegment" />
    /// <structure>Engine.Geometry.Segment2D</structure>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(LineSegment2D))]
    [XmlType(TypeName = "line-segment")]
    [TypeConverter(typeof(StructConverter<LineSegment2D>))]
    [DebuggerDisplay("{ToString()}")]
    public class LineSegment2D
        : Shape2D, IShapeSegment
    {
        #region Implementations
        /// <summary>
        /// Represents a Engine.Geometry.Segment that is null.
        /// </summary>
        public static readonly LineSegment2D Empty = new LineSegment2D(0d, 0d, 0d, 0d);
        #endregion Implementations

        #region Fields
        /// <summary>
        /// The a x.
        /// </summary>
        private double aX;

        /// <summary>
        /// The a y.
        /// </summary>
        private double aY;

        /// <summary>
        /// The b x.
        /// </summary>
        private double bX;

        /// <summary>
        /// The b y.
        /// </summary>
        private double bY;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment2D" /> class.
        /// </summary>
        public LineSegment2D()
            : this(Point2D.Empty, Point2D.Empty)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment2D" /> class.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        public LineSegment2D((double x1, double y1, double x2, double y2) tuple)
            : this(tuple.x1, tuple.y1, tuple.x2, tuple.y2)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment2D" /> class.
        /// </summary>
        /// <param name="x1">Horizontal component of starting point</param>
        /// <param name="y1">Vertical component of starting point</param>
        /// <param name="X2">Horizontal component of ending point</param>
        /// <param name="Y2">Vertical component of ending point</param>
        public LineSegment2D(double x1, double y1, double X2, double Y2)
            : this(new Point2D(x1, y1), new Point2D(X2, Y2))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment2D" /> class.
        /// </summary>
        /// <param name="Point">Starting Point</param>
        /// <param name="RadAngle">Ending Angle</param>
        /// <param name="Radius">Ending Line Segment Length</param>
        public LineSegment2D(Point2D Point, double RadAngle, double Radius)
            : this(new Point2D(Point.X, Point.Y), new Point2D(Point.X + (Radius * Cos(RadAngle)), Point.Y + (Radius * Sin(RadAngle))))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment2D" /> class.
        /// </summary>
        /// <param name="a">Starting Point</param>
        /// <param name="b">Ending Point</param>
        public LineSegment2D(Point2D a, Point2D b)
        {
            A = a;
            B = b;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// The deconstruct.
        /// </summary>
        /// <param name="aX">The aX.</param>
        /// <param name="aY">The aY.</param>
        /// <param name="bX">The bX.</param>
        /// <param name="bY">The bY.</param>
        public void Deconstruct(out double aX, out double aY, out double bX, out double bY)
        {
            aX = this.aX;
            aY = this.aY;
            bX = this.bX;
            bY = this.bY;
        }
        #endregion Deconstructors

        #region Indexers
        /// <summary>
        /// The Indexer.
        /// </summary>
        /// <value>
        /// The <see cref="Point2D"/>.
        /// </value>
        /// <param name="index">The index index.</param>
        /// <returns>
        /// One element of type Point2D.
        /// </returns>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        public Point2D this[int index]
        {
            get { return Points[index]; }
            set
            {
                Points[index] = value;
                OnPropertyChanged(nameof(Points));
                update?.Invoke();
            }
        }
        #endregion Indexers

        #region Properties
        /// <summary>
        /// Get or sets an array of points representing a line segment.
        /// </summary>
        /// <value>
        /// The points.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
        public List<Point2D> Points
        {
            get { return new List<Point2D> { A, B }; }
            set
            {
                if (value is not null)
                {
                    A = value[0];
                    B = value[1];
                    ClearCache();
                    OnPropertyChanged(nameof(Points));
                }
            }
        }

        /// <summary>
        /// First Point of a line segment
        /// </summary>
        /// <value>
        /// a.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
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
        /// <value>
        /// The ax.
        /// </value>
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
        /// <value>
        /// The ay.
        /// </value>
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
        /// <value>
        /// The b.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
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
        /// <value>
        /// The bx.
        /// </value>
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
        /// <value>
        /// The by.
        /// </value>
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
        /// <value>
        /// The bounds.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds => (Rectangle2D)CachingProperty(() => Measurements.LineSegmentBounds(A.X, A.Y, B.X, B.Y));

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Length => (double)CachingProperty(() => Measurements.Distance(A.X, A.Y, B.X, B.Y));

        /// <summary>
        /// Gets the length squared.
        /// </summary>
        /// <value>
        /// The length squared.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double LengthSquared => (double)CachingProperty(() => Measurements.SquareDistance(A.X, A.Y, B.X, B.Y));

        /// <summary>
        /// Gets the dot product.
        /// </summary>
        /// <value>
        /// The dot product.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double DotProduct => (double)CachingProperty(() => DotProduct(aX, aY, bX, bY));

        /// <summary>
        /// "a.X * b.Y - b.X * a.Y" This would be the Z-component of (⟪a.X, a.Y, 0⟫ ⨯ ⟪b.X, b.Y, 0⟫) in 3-space.
        /// </summary>
        /// <value>
        /// The cross product.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double CrossProduct => (double)CachingProperty(() => CrossProduct(aX, aY, bX, bY));

        /// <summary>
        /// Gets the complex product.
        /// </summary>
        /// <value>
        /// The complex product.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public (double x, double y) ComplexProduct => ((double x, double y))CachingProperty(() => ComplexProduct(aX, aY, bX, bY));

        /// <summary>
        /// Gets the curve x.
        /// </summary>
        /// <value>
        /// The curve x.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Polynomial CurveX
        {
            get
            {
                var curveX = (Polynomial)CachingProperty(() => Polynomials.Bezier(Points.Select(p => p.X).ToArray()));
                curveX.IsReadonly = true;
                return curveX;
            }
        }

        /// <summary>
        /// Gets the curve y.
        /// </summary>
        /// <value>
        /// The curve y.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Polynomial CurveY
        {
            get
            {
                var curveY = (Polynomial)CachingProperty(() => Polynomials.Bezier(Points.Select(p => p.Y).ToArray()));
                curveY.IsReadonly = true;
                return curveY;
            }
        }

        /// <summary>
        /// Return the point of the segment with lexicographically smallest coordinate.
        /// </summary>
        /// <value>
        /// The minimum.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Point2D Min => (aX < bX) || (aX == bX && aY < bY) ? new Point2D(aX, aY) : new Point2D(bX, bY);

        /// <summary>
        /// Return the point of the segment with lexicographically largest coordinate.
        /// </summary>
        /// <value>
        /// The maximum.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Point2D Max => (aX > bX) || (aX == bX && aY > bY) ? new Point2D(aX, aY) : new Point2D(bX, bY);

        /// <summary>
        /// Gets the degree.
        /// </summary>
        /// <value>
        /// The degree.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public PolynomialDegree Degree => PolynomialDegree.Linear;

        /// <summary>
        /// Gets a value indicating whether
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if degenerate; otherwise, <see langword="false"/>.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public bool Degenerate => aX == bX && aY == bY;

        /// <summary>
        /// Gets a value indicating whether
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if this instance is horizontal; otherwise, <see langword="false"/>.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public bool IsHorizontal => aY == bY;

        /// <summary>
        /// Gets a value indicating whether
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if this instance is vertical; otherwise, <see langword="false"/>.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public bool IsVertical => aX == bX;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IShapeSegment" /> position should be calculated relative to the last item, or from Origin.
        /// </summary>
        /// <value>
        ///   <see langword="true" /> if relative; otherwise, <see langword="false" />.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public bool Relative { get; set; }

        /// <summary>
        /// Gets or sets a reference to the segment after this segment.
        /// </summary>
        /// <value>
        /// The before.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public IShapeSegment Before { get; set; }

        /// <summary>
        /// Gets or sets a reference to the segment before this segment.
        /// </summary>
        /// <value>
        /// The after.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public IShapeSegment After { get; set; }

        /// <summary>
        /// Gets or sets the head point.
        /// </summary>
        /// <value>
        /// The head.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Point2D Head { get; set; }

        /// <summary>
        /// Gets or sets the next to first point from the head point.
        /// </summary>
        /// <value>
        /// The next to head point.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Point2D NextToHead { get; set; }

        /// <summary>
        /// Gets or sets the next to last point to the tail point.
        /// </summary>
        /// <value>
        /// The next to tail point.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Point2D NextToTail { get; set; }

        /// <summary>
        /// Gets or sets the tail point.
        /// </summary>
        /// <value>
        /// The tail.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Point2D Tail { get; set; }
        #endregion Properties

        #region Operators
        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <returns></returns>
        /// <param name="tuple"></param>
        public static implicit operator LineSegment2D((double I, double J, double K, double L) tuple) => new LineSegment2D(tuple);
        #endregion Operators

        #region Interpolators
        /// <summary>
        /// Interpolates a shape.
        /// </summary>
        /// <param name="t">Index of the point to interpolate.</param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        public override Point2D Interpolate(double t) => Interpolators.Linear(t, A, B);
        #endregion Interpolators

        #region Mutators
        /// <summary>
        /// The reverse.
        /// </summary>
        public void Reverse()
        {
            var temp = A;
            A = B;
            B = temp;
            ClearCache();
            update?.Invoke();
        }
        #endregion Mutators

        #region Methods
        /// <summary>
        /// The to array.
        /// </summary>
        /// <returns>The <see cref="Array"/>.</returns>
        public Point2D[] ToArray() => new Point2D[] { A, B };

        /// <summary>
        /// The to line.
        /// </summary>
        /// <returns>The <see cref="Line2D"/>.</returns>
        public Line2D ToLine() => new Line2D(aX, aY, aX - bX, aY - bY);

        /// <summary>
        /// Creates a string representation of this <see cref="LineSegment2D"/> struct based on the format string
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
            if (this is null)
            {
                return nameof(LineSegment2D);
            }

            var sep = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(LineSegment2D)}{{{nameof(A)}={A.ToString(format, provider)}{sep}{nameof(B)}={B.ToString(format, provider)}}}";
        }
        #endregion Methods
    }
}

