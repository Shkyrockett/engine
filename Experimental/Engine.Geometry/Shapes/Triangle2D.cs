// <copyright file="Triangle2D.cs" company="Shkyrockett" >
//     Copyright © 2013 - 2020 Shkyrockett. All rights reserved.
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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// The triangle class.
    /// </summary>
    /// <seealso cref="Engine.IClosedShape" />
    /// <seealso cref="Engine.IPropertyCaching" />
    /// <seealso cref="System.IEquatable{T}" />
    [GraphicsObject]
    [DataContract, Serializable]
    [TypeConverter(typeof(StructConverter<Triangle2D>))]
    [DebuggerDisplay("{ToString()}")]
    public struct Triangle2D
        : IClosedShape, IPropertyCaching, IEquatable<Triangle2D>
    {
        #region Implementations
        /// <summary>
        /// The empty
        /// </summary>
        public static readonly Triangle2D Empty = new Triangle2D(Point2D.Empty, Point2D.Empty, Point2D.Empty);
        #endregion

        #region Fields
        /// <summary>
        /// a
        /// </summary>
        private Point2D a;

        /// <summary>
        /// The b
        /// </summary>
        private Point2D b;

        /// <summary>
        /// The c
        /// </summary>
        private Point2D c;
        #endregion

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

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle2D" /> class.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Triangle2D(Point2D a, Point2D b, Point2D c)
            : this()
        {
            (this.a, this.b, this.c) = (a, b, c);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle2D"/> struct.
        /// </summary>
        /// <param name="aX">a x.</param>
        /// <param name="aY">a y.</param>
        /// <param name="bX">The b x.</param>
        /// <param name="bY">The b y.</param>
        /// <param name="cX">The c x.</param>
        /// <param name="cY">The c y.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Triangle2D(double aX, double aY, double bX, double bY, double cX, double cY)
            : this(new Point2D(aX, aY), new Point2D(bX, bY), new Point2D(cX, cY))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle2D"/> class.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Triangle2D(PolygonContour2D polygon)
            : this(polygon.Points)
        {
            if (polygon.Points.Count > 3)
            {
                throw new IndexOutOfRangeException();
            }

            if (polygon.Points.Count < 3)
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle2D"/> class.
        /// </summary>
        /// <param name="polyline">The polyline.</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Triangle2D(Polyline2D polyline)
            : this(polyline.Points)
        {
            if (polyline.Points.Count > 3)
            {
                throw new IndexOutOfRangeException();
            }

            if (polyline.Points.Count < 3)
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle2D"/> class.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Triangle2D(params Point2D[] points)
            : this(points as IList<Point2D>)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle2D"/> class.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Triangle2D(IList<Point2D> points)
            : this()
        {
            if (points?.Count > 3 || points?.Count < 3)
            {
                throw new IndexOutOfRangeException();
            }

            (a, b, c) = (points[0], points[1], points[2]);
        }
        #endregion

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Triangle2D"/> to a <see cref="ValueTuple{T1, T2, T3}"/>.
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out Point2D A, out Point2D B, out Point2D C)
        {
            A = this.A;
            B = this.B;
            C = this.C;
        }

        /// <summary>
        /// The deconstruct.
        /// </summary>
        /// <param name="aX">The aX.</param>
        /// <param name="aY">The aY.</param>
        /// <param name="bX">The bX.</param>
        /// <param name="bY">The bY.</param>
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        public void Deconstruct(out double aX, out double aY, out double bX, out double bY, out double cX, out double cY)
        {
            aX = A.X;
            aY = A.Y;
            bX = B.X;
            bY = B.Y;
            cX = C.X;
            cY = C.Y;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the a.
        /// </summary>
        /// <value>
        /// a.
        /// </value>
        [DataMember(Name = nameof(A)), XmlElement(nameof(A)), SoapElement(nameof(A))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Point2D A
        {
            get { return a; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                a = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the b.
        /// </summary>
        /// <value>
        /// The b.
        /// </value>
        [DataMember(Name = nameof(B)), XmlElement(nameof(B)), SoapElement(nameof(B))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Point2D B
        {
            get { return b; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                b = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the c.
        /// </summary>
        /// <value>
        /// The c.
        /// </value>
        [DataMember(Name = nameof(C)), XmlElement(nameof(C)), SoapElement(nameof(C))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [TypeConverter(typeof(Point2DConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public Point2D C
        {
            get { return c; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                c = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Property cache for commonly used properties that may take time to calculate.
        /// </summary>
        /// <value>
        /// The property cache.
        /// </value>
        [Browsable(false)]
        [field: NonSerialized]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        Dictionary<object, object> IPropertyCaching.PropertyCache { get; set; }
        #endregion

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
        public static bool operator ==(Triangle2D left, Triangle2D right) => left.Equals(right);

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
        public static bool operator !=(Triangle2D left, Triangle2D right) => !(left == right);

        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator PolygonContour2D(Triangle2D triangle) => new PolygonContour2D(triangle.A, triangle.B, triangle.C);
        #endregion

        #region Operator Backing Methods
        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([AllowNull] object obj) => obj is Triangle2D d && Equals(d);

        public static explicit operator Triangle2D(PolygonContour2D v) => throw new NotImplementedException();

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals([AllowNull] Triangle2D other) => A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C);
        #endregion

        #region Methods
        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(Point2D point) => Intersections.TriangleContainsPoint(A.X, A.Y, B.X, B.Y, C.X, C.Y, point.X, point.Y) is Inclusions o && o == Inclusions.Inside || 0 == Inclusions.Boundary;
        #endregion

        #region Standard Methods
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
        public override int GetHashCode() => HashCode.Combine(A, B, C);

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
        /// Creates a string representation of this <see cref="PolygonContour2D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (this == null) { return nameof(Triangle2D); }
            var sep = Tokenizer.GetNumericListSeparator(formatProvider);
            return $"{nameof(Triangle2D)}({string.Join(sep, new[] { A.ToString(format, formatProvider), B.ToString(format, formatProvider), C.ToString(format, formatProvider) })})";
        }
        #endregion
    }
}
