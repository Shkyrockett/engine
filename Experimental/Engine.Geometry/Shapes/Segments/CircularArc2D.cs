// <copyright file="CircularArc2D.cs" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//    Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Engine.IShapeSegment" />
    /// <seealso cref="Engine.IPropertyCaching" />
    /// <seealso cref="System.IEquatable{Engine.CircularArc2D}" />
    [GraphicsObject]
    [DataContract, Serializable]
    [TypeConverter(typeof(StructConverter<CircularArc2D>))]
    [DebuggerDisplay("{ToString()}")]
    public struct CircularArc2D
        : IShapeSegment, IPropertyCaching, IEquatable<CircularArc2D>
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
        /// Initializes a new instance of the <see cref="CircularArc2D" /> class.
        /// </summary>
        /// <param name="center">The center.</param>
        /// <param name="radius">The r x.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CircularArc2D(Point2D center, double radius, double startAngle, double sweepAngle)
            : this()
        {
            X = center.X;
            Y = center.Y;
            Radius = radius;
            StartAngle = startAngle;
            SweepAngle = sweepAngle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularArc2D" /> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="radius">The r x.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CircularArc2D(double x, double y, double radius, double startAngle, double sweepAngle)
            : this()
        {
            X = x;
            Y = y;
            Radius = radius;
            StartAngle = startAngle;
            SweepAngle = sweepAngle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularArc2D" /> class.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CircularArc2D((double X, double Y, double Radius, double StartAngle, double SweepAngle) tuple)
            : this(tuple.X, tuple.Y, tuple.Radius, tuple.StartAngle, tuple.SweepAngle)
        { }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the center of the <see cref="CircularArc2D"/>.
        /// </summary>
        /// <value>
        /// The center.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Point2D Center { get => new Point2D(X, Y); set => (X, Y) = value; }

        public double Radius { get; internal set; }

        public double StartAngle { get; internal set; }

        public double SweepAngle { get; internal set; }

        public double X { get; internal set; }

        public double Y { get; internal set; }

        /// <summary>
        /// Property cache for commonly used properties that may take time to calculate.
        /// </summary>
        [Browsable(false)]
        [field: NonSerialized]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        Dictionary<object, object> IPropertyCaching.PropertyCache { get; set; }
        #endregion

        #region Operators
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(CircularArc2D left, CircularArc2D right) => left.Equals(right);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(CircularArc2D left, CircularArc2D right) => !(left == right);

        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator CircularArc2D((double X, double Y, double Radius, double StartAngle, double SweepAngle) tuple) => FromValueTuple(tuple);
        #endregion

        #region Operator Backing Methods
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is CircularArc2D d && Equals(d);

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(CircularArc2D other) => Center.Equals(other.Center) && Radius == other.Radius && StartAngle == other.StartAngle && SweepAngle == other.SweepAngle && X == other.X && Y == other.Y;

        /// <summary>
        /// Creates a new <see cref="CircularArc2D"/> from a <see cref="ValueTuple{T1, T2, T3, T4, T5}"/>.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CircularArc2D FromValueTuple((double X, double Y, double Radius, double StartAngle, double SweepAngle) tuple) => new CircularArc2D(tuple);
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
        public override int GetHashCode() => HashCode.Combine(Center, Radius, StartAngle, SweepAngle, X, Y);

        /// <summary>
        /// Creates a <see cref="string"/> representation of this <see cref="IShape"/> interface based on the current culture.
        /// </summary>
        /// <returns>A <see cref="string"/> representation of this instance of the <see cref="IShape"/> object.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a <see cref="string" /> representation of this <see cref="IShape" /> interface based on the current culture.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (this == null)
            {
                return nameof(EllipticalArc2D);
            }

            var sep = Tokenizer.GetNumericListSeparator(formatProvider);
            return $"{nameof(EllipticalArc2D)}({nameof(X)}: {X.ToString(format, formatProvider)}{sep} {nameof(Y)}: {Y.ToString(format, formatProvider)}{sep} {nameof(Radius)}: {Radius.ToString(format, formatProvider)}{sep} {nameof(StartAngle)}: {StartAngle.ToString(format, formatProvider)}{sep} {nameof(SweepAngle)}: {SweepAngle.ToString(format, formatProvider)})";
        }
        #endregion
    }
}