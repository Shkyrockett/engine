// <copyright file="LineSegment.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
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
    /// <structure>Engine.Geometry.Segment2D</structure>
    [DataContract, Serializable]
    [GraphicsObject]
    [DisplayName(nameof(LineSegment))]
    [XmlType(TypeName = "line-segment")]
    [DebuggerDisplay("{ToString()}")]
    public class LineSegment
        : Shape
    {
        #region Implementations
        /// <summary>
        /// Represents a Engine.Geometry.Segment that is null.
        /// </summary>
        public static readonly LineSegment Empty = new LineSegment();
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
        /// Initializes a new instance of the <see cref="LineSegment"/> class.
        /// </summary>
        public LineSegment()
            : this(Point2D.Empty, Point2D.Empty)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment"/> class.
        /// </summary>
        /// <param name="tuple"></param>
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
        public LineSegment(double x1, double y1, double X2, double Y2)
            : this(new Point2D(x1, y1), new Point2D(X2, Y2))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment"/> class.
        /// </summary>
        /// <param name="Point">Starting Point</param>
        /// <param name="RadAngle">Ending Angle</param>
        /// <param name="Radius">Ending Line Segment Length</param>
        public LineSegment(Point2D Point, double RadAngle, double Radius)
            : this(new Point2D(Point.X, Point.Y), new Point2D(Point.X + (Radius * Cos(RadAngle)), Point.Y + (Radius * Sin(RadAngle))))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment"/> class.
        /// </summary>
        /// <param name="a">Starting Point</param>
        /// <param name="b">Ending Point</param>
        public LineSegment(Point2D a, Point2D b)
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
        /// <param name="index">The index index.</param>
        /// <returns>One element of type Point2D.</returns>
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
        #endregion Indexers

        #region Properties
        /// <summary>
        /// Get or sets an array of points representing a line segment.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [TypeConverter(typeof(ExpandableCollectionConverter))]
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
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Rectangle2DConverter))]
        public override Rectangle2D Bounds
            => (Rectangle2D)CachingProperty(() => Measurements.LineSegmentBounds(A.X, A.Y, B.X, B.Y));

        /// <summary>
        /// Gets the length.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Length
            => (double)CachingProperty(() => Measurements.Distance(A.X, A.Y, B.X, B.Y));

        /// <summary>
        /// Gets the length squared.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double LengthSquared
            => (double)CachingProperty(() => Measurements.SquareDistance(A.X, A.Y, B.X, B.Y));

        /// <summary>
        /// Gets the dot product.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double DotProduct
            => (double)CachingProperty(() => DotProduct(aX, aY, bX, bY));

        /// <summary>
        /// "a.X * b.Y - b.X * a.Y" This would be the Z-component of (⟪a.X, a.Y, 0⟫ ⨯ ⟪b.X, b.Y, 0⟫) in 3-space.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double CrossProduct
            => (double)CachingProperty(() => CrossProduct(aX, aY, bX, bY));

        /// <summary>
        /// Gets the complex product.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public (double x, double y) ComplexProduct
            => ((double x, double y))CachingProperty(() => ComplexProduct(aX, aY, bX, bY));

        /// <summary>
        /// Gets the curve x.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
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
        /// Gets the curve y.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
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
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Point2D Min
            => (aX < bX) || (aX == bX && aY < bY) ? new Point2D(aX, aY) : new Point2D(bX, bY);

        /// <summary>
        /// Return the point of the segment with lexicographically largest coordinate.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Point2D Max
            => (aX > bX) || (aX == bX && aY > bY) ? new Point2D(aX, aY) : new Point2D(bX, bY);

        /// <summary>
        /// Gets the degree.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public static PolynomialDegree Degree
            => PolynomialDegree.Linear;

        /// <summary>
        /// Gets a value indicating whether 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public bool Degenerate
            => aX == bX && aY == bY;

        /// <summary>
        /// Gets a value indicating whether 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public bool IsHorizontal
            => aY == bY;

        /// <summary>
        /// Gets a value indicating whether 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public bool IsVertical
            => aX == bX;
        #endregion Properties

        #region Operators
        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <returns></returns>
        /// <param name="tuple"></param>
        public static implicit operator LineSegment((double I, double J, double K, double L) tuple)
            => new LineSegment(tuple);
        #endregion Operators

        #region Interpolators
        /// <summary>
        /// Interpolates a shape.
        /// </summary>
        /// <param name="t">Index of the point to interpolate.</param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        public override Point2D Interpolate(double t)
            => Interpolators.Linear(t, A, B);
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
        public Point2D[] ToArray()
            => new Point2D[] { A, B };

        /// <summary>
        /// The to line.
        /// </summary>
        /// <returns>The <see cref="Line"/>.</returns>
        public Line ToLine()
            => new Line(aX, aY, aX - bX, aY - bY);

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
            if (this is null)
            {
                return nameof(LineSegment);
            }

            var sep = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(LineSegment)}{{{nameof(A)}={A.ToString(format, provider)}{sep}{nameof(B)}={B.ToString(format, provider)}}}";
        }
        #endregion Methods
    }
}

