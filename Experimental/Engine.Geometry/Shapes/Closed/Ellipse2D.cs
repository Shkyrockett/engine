// <copyright file="Ellipse2D.cs" >
//    Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The circle struct.
    /// </summary>
    /// <seealso cref="Engine.IClosedShape" />
    /// <seealso cref="Engine.IPropertyCaching" />
    /// <seealso cref="System.IEquatable{Engine.Ellipse2D}" />
    [GraphicsObject]
    [DataContract, Serializable]
    [TypeConverter(typeof(StructConverter<Ellipse2D>))]
    [DebuggerDisplay("{ToString()}")]
    public struct Ellipse2D
        : IClosedShape, IPropertyCaching, IEquatable<Ellipse2D>
    {
        #region Implementations
        /// <summary>
        /// The empty.
        /// </summary>
        public static Ellipse2D Empty = new Ellipse2D(0, 0, 0, 0);
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
        /// Initializes a new instance of the <see cref="Ellipse2D" /> class.
        /// </summary>
        /// <param name="center">The center.</param>
        /// <param name="radiusA">The radius a.</param>
        /// <param name="radiusB">The radius b.</param>
        /// <param name="angle">The angle the <see cref="Ellipse2D" /> is rotated about the center.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Ellipse2D(Point2D center, double radiusA, double radiusB, double angle = 0)
            : this()
        {
            X = center.X;
            Y = center.Y;
            RadiusA = radiusA;
            RadiusB = radiusB;
            Angle = angle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ellipse2D"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="radiusA">The radius a.</param>
        /// <param name="radiusB">The radius b.</param>
        /// <param name="angle">The angle the <see cref="Ellipse2D"/> is rotated about the center.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Ellipse2D(double x, double y, double radiusA, double radiusB, double angle = 0)
            : this()
        {
            X = x;
            Y = y;
            RadiusA = radiusA;
            RadiusB = radiusB;
            Angle = angle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ellipse2D" /> class.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Ellipse2D((double x, double y, double radiusA, double radiusB, double angle) tuple)
            : this()
        {
            X = tuple.x;
            Y = tuple.y;
            RadiusA = tuple.radiusA;
            RadiusB = tuple.radiusB;
            Angle = tuple.angle;
        }
        #endregion

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Ellipse2D"/> to a <see cref="ValueTuple{T1, T2, T3, T4, T5}"/>.
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="RadiusA"></param>
        /// <param name="RadiusB"></param>
        /// <param name="Angle"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double X, out double Y, out double RadiusA, out double RadiusB, out double Angle)
        {
            X = this.X;
            Y = this.Y;
            RadiusA = this.RadiusA;
            RadiusB = this.RadiusB;
            Angle = this.Angle;
        }

        /// <summary>
        /// Deconstruct this <see cref="Ellipse2D"/> to a <see cref="ValueTuple{T1, T2, T3, T4}"/>.
        /// </summary>
        /// <param name="X">The left.</param>
        /// <param name="Y">The top.</param>
        /// <param name="RadiusA">The radius a.</param>
        /// <param name="RadiusB">The radius b.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double X, out double Y, out double RadiusA, out double RadiusB)
        {
            X = this.X;
            Y = this.Y;
            RadiusA = this.RadiusA;
            RadiusB = this.RadiusB;
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
        /// Gets or sets the center of the <see cref="Ellipse2D"/>.
        /// </summary>
        /// <value>
        /// The center.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Point2D Center { get => new Point2D(X, Y); set => (X, Y) = value; }

        /// <summary>
        /// Gets or sets the a radius.
        /// </summary>
        [DataMember(Name = nameof(RadiusA)), XmlAttribute(nameof(RadiusA)), SoapAttribute(nameof(RadiusA))]
        public double RadiusA { get; set; }

        /// <summary>
        /// Gets or sets the b radius.
        /// </summary>
        [DataMember(Name = nameof(RadiusB)), XmlAttribute(nameof(RadiusB)), SoapAttribute(nameof(RadiusB))]
        public double RadiusB { get; set; }

        /// <summary>
        /// Gets or sets the angle the <see cref="Ellipse2D"/> is rotated about the center.
        /// </summary>
        [DataMember(Name = nameof(Angle)), XmlAttribute(nameof(Angle)), SoapAttribute(nameof(Angle))]
        public double Angle { get; set; }

        /// <summary>
        /// Gets the cosine of the angle.
        /// </summary>
        /// <value>
        /// The cos angle.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double CosAngle
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var a = Angle;
                return (double)(this as IPropertyCaching).CachingProperty(() => Cos(a));
            }
        }

        /// <summary>
        /// Gets the sine of the angle.
        /// </summary>
        /// <value>
        /// The sin angle.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double SinAngle
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var a = Angle;
                return (double)(this as IPropertyCaching).CachingProperty(() => Sin(a));
            }
        }

        /// <summary>
        /// Property cache for commonly used properties that may take time to calculate.
        /// </summary>
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
        public static bool operator ==(Ellipse2D left, Ellipse2D right) => left.Equals(right);

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
        public static bool operator !=(Ellipse2D left, Ellipse2D right) => !(left == right);

        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Ellipse2D((double X, double Y, double RX, double RY, double Angle) tuple) => FromValueTuple(tuple);
        #endregion

        #region Operator Backing Methods
        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([AllowNull] object obj) => obj is Ellipse2D && Equals((Ellipse2D)obj);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals([AllowNull] Ellipse2D other) => X == other.X && Y == other.Y && RadiusA == other.RadiusA && RadiusB == other.RadiusB && Angle == other.Angle && CosAngle == other.CosAngle && SinAngle == other.SinAngle;

        /// <summary>
        /// Creates a new <see cref="Ellipse2D"/> from a <see cref="ValueTuple{T1, T2, T3, T4, T5}"/>.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ellipse2D FromValueTuple((double X, double Y, double RX, double RY, double Angle) tuple) => new Ellipse2D(tuple);
        #endregion

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
        /// Get the hash code.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(X, Y, RadiusA, RadiusB, Angle);

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
            if (this == null)
            {
                return nameof(Ellipse2D);
            }

            var sep = Tokenizer.GetNumericListSeparator(formatProvider);
            return $"{nameof(Ellipse2D)}({nameof(X)}: {X.ToString(format, formatProvider)}{sep} {nameof(Y)}: {Y.ToString(format, formatProvider)}{sep} {nameof(RadiusA)}: {RadiusA.ToString(format, formatProvider)}{sep} {nameof(RadiusB)}: {RadiusB.ToString(format, formatProvider)}{sep} {nameof(Angle)}: {Angle.ToString(format, formatProvider)})";
        }
        #endregion
    }
}
