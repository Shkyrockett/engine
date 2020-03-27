using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Engine.IClosedShape" />
    /// <seealso cref="Engine.IPropertyCaching" />
    /// <seealso cref="System.IEquatable{Engine.Triangle2D}" />
    [GraphicsObject]
    [DataContract, Serializable]
    [TypeConverter(typeof(StructConverter<Triangle2D>))]
    [DebuggerDisplay("{ToString()}")]
    public struct Triangle2D
        : IClosedShape, IPropertyCaching, IEquatable<Triangle2D>
    {
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
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Triangle2D(Point2D a, Point2D b, Point2D c)
            : this()
        {
            A = a;
            B = b;
            C = c;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Triangle2D(double aX, double aY, double bX, double bY, double cX, double cY)
            : this(new Point2D(aX, aY), new Point2D(bX, bY), new Point2D(cX, cY))
        { }
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
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets a.
        /// </summary>
        /// <value>
        /// a.
        /// </value>
        [DataMember(Name = nameof(A)), XmlElement(nameof(A)), SoapElement(nameof(A))]
        public Point2D A { get; set; }

        /// <summary>
        /// Gets or sets the b.
        /// </summary>
        /// <value>
        /// The b.
        /// </value>
        [DataMember(Name = nameof(B)), XmlElement(nameof(B)), SoapElement(nameof(B))]
        public Point2D B { get; set; }

        /// <summary>
        /// Gets or sets the c.
        /// </summary>
        /// <value>
        /// The c.
        /// </value>
        [DataMember(Name = nameof(C)), XmlElement(nameof(C)), SoapElement(nameof(C))]
        public Point2D C { get; set; }

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
        public static bool operator ==(Triangle2D left, Triangle2D right)
        {
            return left.Equals(right);
        }

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
        public static bool operator !=(Triangle2D left, Triangle2D right)
        {
            return !(left == right);
        }

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
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <see langword="false" />.
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
        /// Converts to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
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
