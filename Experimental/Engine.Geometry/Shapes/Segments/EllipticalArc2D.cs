// <copyright file="EllipticalArc2D.cs" >
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
    /// 
    /// </summary>
    /// <seealso cref="Engine.IClosedShape" />
    /// <seealso cref="System.IEquatable{T}" />
    [GraphicsObject]
    [DataContract, Serializable]
    [TypeConverter(typeof(StructConverter<EllipticalArc2D>))]
    [DebuggerDisplay("{ToString()}")]
    public struct EllipticalArc2D
        : IShapeSegment, IPropertyCaching, IEquatable<EllipticalArc2D>
    {
        #region Implementations
        /// <summary>
        /// The empty.
        /// </summary>
        public static EllipticalArc2D Empty = new EllipticalArc2D(0, 0, 0, 0, 0, 0, 0);
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
        /// Initializes a new instance of the <see cref="EllipticalArc2D"/> class.
        /// </summary>
        /// <param name="center">The center.</param>
        /// <param name="rX">The r x.</param>
        /// <param name="rY">The r y.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public EllipticalArc2D(Point2D center, double rX, double rY, double angle, double startAngle, double sweepAngle)
            : this()
        {
            X = center.X;
            Y = center.Y;
            RadiusA = rX;
            RadiusB = rY;
            Angle = angle;
            StartAngle = startAngle;
            SweepAngle = sweepAngle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalArc2D"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="rX">The r x.</param>
        /// <param name="rY">The r y.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public EllipticalArc2D(double x, double y, double rX, double rY, double angle, double startAngle, double sweepAngle)
            : this()
        {
            X = x;
            Y = y;
            RadiusA = rX;
            RadiusB = rY;
            Angle = angle;
            StartAngle = startAngle;
            SweepAngle = sweepAngle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipticalArc2D"/> class.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public EllipticalArc2D((double X, double Y, double RX, double RY, double Angle, double StartAngle, double SweepAngle) tuple)
            : this()
        {
            X = tuple.X;
            Y = tuple.Y;
            RadiusA = tuple.RX;
            RadiusB = tuple.RY;
            Angle = tuple.Angle;
            StartAngle = tuple.StartAngle;
            SweepAngle = tuple.SweepAngle;
        }
        #endregion

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="EllipticalArc2D" /> to a Tuple.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="radiusA">The radius a.</param>
        /// <param name="radiusB">The radius b.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="startAngle">The start angle.</param>
        /// <param name="sweepAngle">The sweep angle.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out double x, out double y, out double radiusA, out double radiusB, out double angle, out double startAngle, out double sweepAngle)
        {
            x = X;
            y = Y;
            radiusA = RadiusA;
            radiusB = RadiusB;
            angle = Angle;
            startAngle = StartAngle;
            sweepAngle = SweepAngle;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the center <see cref="X"/> coordinate.
        /// </summary>
        [DataMember(Name = nameof(X)), XmlAttribute(nameof(X)), SoapAttribute(nameof(X))]
        public double X { get; internal set; }

        /// <summary>
        /// Gets or sets the center of the <see cref="EllipticalArc2D"/>.
        /// </summary>
        /// <value>
        /// The center.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public Point2D Center { get => new Point2D(X, Y); set => (X, Y) = value; }

        /// <summary>
        /// Gets or sets the center <see cref="Y"/> coordinate.
        /// </summary>
        [DataMember(Name = nameof(Y)), XmlAttribute(nameof(Y)), SoapAttribute(nameof(Y))]
        public double Y { get; internal set; }

        /// <summary>
        /// Gets or sets the a radius.
        /// </summary>
        [DataMember(Name = nameof(RadiusA)), XmlAttribute(nameof(RadiusA)), SoapAttribute(nameof(RadiusA))]
        public double RadiusA { get; internal set; }

        /// <summary>
        /// Gets or sets the b radius.
        /// </summary>
        [DataMember(Name = nameof(RadiusB)), XmlAttribute(nameof(RadiusB)), SoapAttribute(nameof(RadiusB))]
        public double RadiusB { get; internal set; }

        /// <summary>
        /// Gets or sets the angle the <see cref="EllipticalArc2D"/> is rotated about the center.
        /// </summary>
        [RefreshProperties(RefreshProperties.All)]
        [DataMember(Name = nameof(Angle)), XmlAttribute(nameof(Angle)), SoapAttribute(nameof(Angle))]
        public double Angle { get; internal set; }

        /// <summary>
        /// Gets the cos angle.
        /// </summary>
        /// <value>
        /// The cosine of the angle.
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
        /// Gets the sin angle.
        /// </summary>
        /// <value>
        /// The sine of the angle.
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
        /// Gets the start angle.
        /// </summary>
        /// <value>
        /// The start angle.
        /// </value>
        [RefreshProperties(RefreshProperties.All)]
        [DataMember(Name = nameof(StartAngle)), XmlAttribute(nameof(StartAngle)), SoapAttribute(nameof(StartAngle))]
        public double StartAngle { get; internal set; }

        /// <summary>
        /// Gets the cosine of the start angle.
        /// </summary>
        /// <value>
        /// The cosine of the angle.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double CosStartAngle
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var a = StartAngle;
                return (double)(this as IPropertyCaching).CachingProperty(() => Cos(a));
            }
        }

        /// <summary>
        /// Gets the sine of the start angle.
        /// </summary>
        /// <value>
        /// The sine of the angle.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double SinStartAngle
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var a = StartAngle;
                return (double)(this as IPropertyCaching).CachingProperty(() => Sin(a));
            }
        }

        /// <summary>
        /// Gets or sets the end angle.
        /// </summary>
        /// <value>
        /// The end angle.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [RefreshProperties(RefreshProperties.All)]
        public double EndAngle
        {
            get { return StartAngle + SweepAngle; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                SweepAngle = value - StartAngle;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the cosine of the end angle.
        /// </summary>
        /// <value>
        /// The cosine of the end angle.
        /// </value>
        [Browsable(false)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double CosEndAngle
        {
            get
            {
                var a = EndAngle;
                return (double)(this as IPropertyCaching).CachingProperty(() => Cos(a));
            }
        }

        /// <summary>
        /// Gets the sine of the end angle.
        /// </summary>
        /// <value>
        /// The cosine of the end angle.
        /// </value>
        [Browsable(false)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double SinEndAngle
        {
            get
            {
                var a = EndAngle;
                return (double)(this as IPropertyCaching).CachingProperty(() => Sin(a));
            }
        }

        /// <summary>
        /// Gets the sweep angle.
        /// </summary>
        /// <value>
        /// The sweep angle.
        /// </value>
        [RefreshProperties(RefreshProperties.All)]
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double SweepAngle { get; internal set; }

        /// <summary>
        /// Gets the cosine of the sweep angle.
        /// </summary>
        /// <value>
        /// The cosine of the angle.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double CosSweepAngle
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var a = SweepAngle;
                return (double)(this as IPropertyCaching).CachingProperty(() => Cos(a));
            }
        }

        /// <summary>
        /// Gets the sine of the sweep angle.
        /// </summary>
        /// <value>
        /// The sine of the angle.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        public double SinSweepAngle
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var a = SweepAngle;
                return (double)(this as IPropertyCaching).CachingProperty(() => Sin(a));
            }
        }

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
        public static bool operator ==(EllipticalArc2D left, EllipticalArc2D right) => left.Equals(right);

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
        public static bool operator !=(EllipticalArc2D left, EllipticalArc2D right) => !(left == right);

        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator EllipticalArc2D((double X, double Y, double RX, double RY, double Angle, double StartAngle, double SweepAngle) tuple) => FromValueTuple(tuple);
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
        public override bool Equals([AllowNull] object obj) => obj is EllipticalArc2D d && Equals((EllipticalArc2D)d);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals([AllowNull] EllipticalArc2D other)
            => X == other.X &&
                Y == other.Y &&
                RadiusA == other.RadiusA &&
                RadiusB == other.RadiusB &&
                Angle == other.Angle &&
                CosAngle == other.CosAngle &&
                SinAngle == other.SinAngle &&
                StartAngle == other.StartAngle &&
                SweepAngle == other.SweepAngle;

        /// <summary>
        /// Creates a new <see cref="EllipticalArc2D"/> from a <see cref="ValueTuple{T1, T2, T3, T4, T5}"/>.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EllipticalArc2D FromValueTuple((double X, double Y, double RX, double RY, double Angle, double StartAngle, double SweepAngle) tuple) => new EllipticalArc2D(tuple);
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
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(X);
            hash.Add(Y);
            hash.Add(RadiusA);
            hash.Add(RadiusB);
            hash.Add(Angle);
            hash.Add(CosAngle);
            hash.Add(SinAngle);
            hash.Add(StartAngle);
            hash.Add(SweepAngle);
            return hash.ToHashCode();
        }

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
        /// A <see cref="System.String" /> that represents this instance.
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
            return $"{nameof(EllipticalArc2D)}({nameof(X)}: {X.ToString(format, formatProvider)}{sep} {nameof(Y)}: {Y.ToString(format, formatProvider)}{sep} {nameof(RadiusA)}: {RadiusA.ToString(format, formatProvider)}{sep} {nameof(RadiusB)}: {RadiusB.ToString(format, formatProvider)}{sep} {nameof(Angle)}: {Angle.ToString(format, formatProvider)}{sep} {nameof(StartAngle)}: {StartAngle.ToString(format, formatProvider)}{sep} {nameof(SweepAngle)}: {SweepAngle.ToString(format, formatProvider)})";
        }
        #endregion
    }
}