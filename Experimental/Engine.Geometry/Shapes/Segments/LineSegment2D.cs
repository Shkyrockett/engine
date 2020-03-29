// <copyright file="LineSegment.cs" company="Shkyrockett" >
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
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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
    [GraphicsObject]
    [DataContract, Serializable]
    [DisplayName(nameof(LineSegment2D))]
    [XmlType(TypeName = "line-segment")]
    [TypeConverter(typeof(StructConverter<LineSegment2D>))]
    [DebuggerDisplay("{ToString()}")]
    public class LineSegment2D
        : IShapeSegment, IPropertyCaching, IEquatable<LineSegment2D>
    {
        #region Implementations
        /// <summary>
        /// Represents a Engine.Geometry.Segment that is null.
        /// </summary>
        public static readonly LineSegment2D Empty = new LineSegment2D(0d, 0d, 0d, 0d);
        #endregion Implementations

        #region Event Delegates
        /// <summary>
        /// The property changed event of the <see cref="PropertyChangedEventHandler"/>.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The property changing event of the <see cref="PropertyChangingEventHandler"/>.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;
        #endregion

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
        /// Initializes a new instance of the <see cref="LineSegment2D"/> class.
        /// </summary>
        public LineSegment2D()
            : this(Point2D.Empty, Point2D.Empty)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment2D"/> class.
        /// </summary>
        /// <param name="tuple"></param>
        public LineSegment2D((double x1, double y1, double x2, double y2) tuple)
            : this(tuple.x1, tuple.y1, tuple.x2, tuple.y2)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment2D"/> class.
        /// </summary>
        /// <param name="x1">Horizontal component of starting point</param>
        /// <param name="y1">Vertical component of starting point</param>
        /// <param name="X2">Horizontal component of ending point</param>
        /// <param name="Y2">Vertical component of ending point</param>
        public LineSegment2D(double x1, double y1, double X2, double Y2)
            : this(new Point2D(x1, y1), new Point2D(X2, Y2))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment2D"/> class.
        /// </summary>
        /// <param name="Point">Starting Point</param>
        /// <param name="RadAngle">Ending Angle</param>
        /// <param name="Radius">Ending Line Segment Length</param>
        public LineSegment2D(Point2D Point, double RadAngle, double Radius)
            : this(new Point2D(Point.X, Point.Y), new Point2D(Point.X + (Radius * Cos(RadAngle)), Point.Y + (Radius * Sin(RadAngle))))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment2D"/> class.
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
        /// <param name="index">The index index.</param>
        /// <returns>One element of type Point2D.</returns>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        public Point2D this[int index]
        {
            get { return Points[index]; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                Points[index] = value;
                OnPropertyChanged();
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
                if (!(value is null))
                {
                    OnPropertyChanging();
                    (this as IPropertyCaching).ClearCache();
                    A = value[0];
                    B = value[1];
                    OnPropertyChanged();
                }
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
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                aX = value.X;
                aY = value.Y;
                OnPropertyChanged();
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
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                aX = value;
                OnPropertyChanged();
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
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                aY = value;
                OnPropertyChanged();
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
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                bX = value.X;
                bY = value.Y;
                OnPropertyChanged();
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
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                bX = value;
                OnPropertyChanged();
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
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                bY = value;
                OnPropertyChanged();
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
        public Rectangle2D Bounds => (Rectangle2D)(this as IPropertyCaching).CachingProperty(() => Measurements.LineSegmentBounds(AX, A.Y, B.X, B.Y));

        /// <summary>
        /// Gets the length.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double Length => (double)(this as IPropertyCaching).CachingProperty(() => Measurements.Distance(A.X, A.Y, B.X, B.Y));

        /// <summary>
        /// Gets the length squared.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double LengthSquared => (double)(this as IPropertyCaching).CachingProperty(() => Measurements.SquareDistance(A.X, A.Y, B.X, B.Y));

        /// <summary>
        /// Gets the dot product.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double DotProduct => (double)(this as IPropertyCaching).CachingProperty(() => DotProduct(aX, aY, bX, bY));

        /// <summary>
        /// "a.X * b.Y - b.X * a.Y" This would be the Z-component of (⟪a.X, a.Y, 0⟫ ⨯ ⟪b.X, b.Y, 0⟫) in 3-space.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double CrossProduct => (double)(this as IPropertyCaching).CachingProperty(() => CrossProduct(aX, aY, bX, bY));

        /// <summary>
        /// Gets the complex product.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public (double x, double y) ComplexProduct => ((double x, double y))(this as IPropertyCaching).CachingProperty(() => ComplexProduct(aX, aY, bX, bY));

        /// <summary>
        /// Gets the curve x.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Polynomial CurveX
        {
            get
            {
                var curveX = (Polynomial)(this as IPropertyCaching).CachingProperty(() => Polynomial.Bezier(Points.Select(p => p.X).ToArray()));
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
                var curveY = (Polynomial)(this as IPropertyCaching).CachingProperty(() => Polynomial.Bezier(Points.Select(p => p.Y).ToArray()));
                curveY.IsReadonly = true;
                return curveY;
            }
        }

        /// <summary>
        /// Return the point of the segment with lexicographically smallest coordinate.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Point2D Min => (aX < bX) || (aX == bX && aY < bY) ? new Point2D(aX, aY) : new Point2D(bX, bY);

        /// <summary>
        /// Return the point of the segment with lexicographically largest coordinate.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Point2D Max => (aX > bX) || (aX == bX && aY > bY) ? new Point2D(aX, aY) : new Point2D(bX, bY);

        /// <summary>
        /// Gets the degree.
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public PolynomialDegree Degree => PolynomialDegree.Linear;

        /// <summary>
        /// Gets a value indicating whether 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public bool Degenerate => aX == bX && aY == bY;

        /// <summary>
        /// Gets a value indicating whether 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public bool IsHorizontal => aY == bY;

        /// <summary>
        /// Gets a value indicating whether 
        /// </summary>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public bool IsVertical => aX == bX;

        /// <summary>
        /// Gets or sets the property cache.
        /// </summary>
        /// <value>
        /// The property cache.
        /// </value>
        [Browsable(false)]
        [field: NonSerialized]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        Dictionary<object, object> IPropertyCaching.PropertyCache { get; set; }
        #endregion Properties

        #region Operators
        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(LineSegment2D left, LineSegment2D right) => EqualityComparer<LineSegment2D>.Default.Equals(left, right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(LineSegment2D left, LineSegment2D right) => !(left == right);

        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator LineSegment2D((double I, double J, double K, double L) tuple) => new LineSegment2D(tuple);
        #endregion Operators

        #region Operator Baking Methods
        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is LineSegment2D d && Equals(d);

        /// <summary>
        /// Equalses the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(LineSegment2D obj) => aX == obj.aX && aY == obj.aY && bX == obj.bX && bY == obj.bY;

        /// <summary>
        /// The to array.
        /// </summary>
        /// <returns>
        /// The <see cref="Array" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point2D[] ToArray() => new Point2D[] { A, B };

        /// <summary>
        /// The to line.
        /// </summary>
        /// <returns>
        /// The <see cref="Line2D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Line2D ToLine() => new Line2D(aX, aY, aX - bX, aY - bY);
        #endregion

        #region Interpolators
        /// <summary>
        /// Interpolates a shape.
        /// </summary>
        /// <param name="t">Index of the point to interpolate.</param>
        /// <returns>Returns the interpolated point of the index value.</returns>
        public Point2D Interpolate(double t) => Interpolators.Linear(t, A, B);
        #endregion Interpolators

        #region Mutators
        /// <summary>
        /// The reverse.
        /// </summary>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reverse()
        {
            OnPropertyChanging();
            (this as IPropertyCaching).ClearCache();
            var temp = A;
            A = B;
            B = temp;
            OnPropertyChanged();
        }
        #endregion Mutators

        #region Methods
        /// <summary>
        /// Raises the property changing event.
        /// </summary>
        /// <param name="name">The name.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnPropertyChanging([CallerMemberName] string name = "") => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="name">The name.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(aX, aY, bX, bY);

        /// <summary>
        /// Creates a <see cref="string" /> representation of this <see cref="IShape" /> interface based on the current culture.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> representation of this instance of the <see cref="IShape" /> object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="LineSegment2D" /> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
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

