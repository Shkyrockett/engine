using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// The circle struct.
    /// </summary>
    [ComVisible(true)]
    [DataContract, Serializable]
    [DebuggerDisplay("{ToString()}")]
    public struct Circle2D
        : IClosedShape
    {
        #region Implementations
        /// <summary>
        /// The empty.
        /// </summary>
        public static Circle2D Empty = new Circle2D(0, 0, 0);
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Circle2D"/> class.
        /// </summary>
        /// <param name="circle"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Circle2D(Circle2D circle)
            : this(circle.X, circle.Y, circle.Radius)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle2D"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="radius">The radius.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Circle2D(double x, double y, double radius)
            : this()
        {
            X = x;
            Y = y;
            Radius = radius;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle2D"/> class.
        /// </summary>
        /// <param name="tuple"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Circle2D((double x, double y, double radius) tuple)
            : this(tuple.x, tuple.y, tuple.radius)
        { }
        #endregion

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Circle2D"/> to a <see cref="ValueTuple{T1, T2, T3}"/>.
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Radius"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double X, out double Y, out double Radius)
        {
            X = this.X;
            Y = this.Y;
            Radius = this.Radius;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the center <see cref="X"/> coordinate.
        /// </summary>
        [DataMember(Name = nameof(X)), XmlAttribute(nameof(X)), SoapAttribute(nameof(X))]
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the center <see cref="Y"/> coordinate.
        /// </summary>
        [DataMember(Name = nameof(Y)), XmlAttribute(nameof(Y)), SoapAttribute(nameof(Y))]
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        [DataMember(Name = nameof(Radius)), XmlAttribute(nameof(Radius)), SoapAttribute(nameof(Radius))]
        public double Radius { get; set; }

        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Point2D Center { get { return (X, Y); } internal set { (X, Y) = value; } }

        /// <summary>
        /// Property cache for commonly used properties that may take time to calculate.
        /// </summary>
        [Browsable(false)]
        [field: NonSerialized]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        Dictionary<object, object> IShape.PropertyCache { get; set; }
        #endregion

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is Circle2D && Equals(this, (Circle2D)obj);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Circle2D left, Circle2D right) => left.X == right.X && left.Y == right.Y && left.Radius == right.Radius;

        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(X, Y, Radius);

        /// <summary>
        /// Creates a <see cref="string"/> representation of this <see cref="IShape"/> interface based on the current culture.
        /// </summary>
        /// <returns>A <see cref="string"/> representation of this instance of the <see cref="IShape"/> object.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            //if (this is null)
            //{
            //    return nameof(Circle2D);
            //}

            var sep = Tokenizer.GetNumericListSeparator(formatProvider);
            return $"{nameof(Circle2D)}({nameof(X)}: {X.ToString(format, formatProvider)}{sep} {nameof(Y)}: {Y.ToString(format, formatProvider)}{sep} {nameof(Radius)}: {Radius.ToString(format, formatProvider)})";
        }
    }
}

