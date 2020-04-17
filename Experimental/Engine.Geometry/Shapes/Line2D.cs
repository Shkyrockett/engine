// <copyright file="Line.cs" company="Shkyrockett" >
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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// 2D Line Structure
    /// </summary>
    /// <structure>Engine.Geometry.Line2D</structure>
    [GraphicsObject]
    [DataContract, Serializable]
    [TypeConverter(typeof(StructConverter<Line2D>))]
    [XmlType(TypeName = "line", Namespace = "http://www.w3.org/2000/svg")]
    [DebuggerDisplay("{ToString()}")]
    public struct Line2D
        : IShapeSegment, IPropertyCaching, IEquatable<Line2D>
    {
        #region Implementations
        /// <summary>
        /// Represents a Engine.Geometry.Segment that is null.
        /// </summary>
        public static readonly Line2D Empty = new Line2D(0d, 0d, 0d, 0d);
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
        /// The location.
        /// </summary>
        private Point2D location;

        /// <summary>
        /// The direction.
        /// </summary>
        private Vector2D direction;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Line2D"/> class.
        /// </summary>
        /// <param name="tuple"></param>
        public Line2D((double x, double y, double i, double j) tuple)
            : this(tuple.x, tuple.y, tuple.i, tuple.j)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Line2D"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        public Line2D(double x, double y, double i, double j)
            : this(new Point2D(x, y), new Vector2D(i, j))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment2D"/> class.
        /// </summary>
        /// <param name="Point">Starting Point</param>
        /// <param name="RadAngle">Ending Angle</param>
        public Line2D(Point2D Point, double RadAngle)
            : this(Point.X, Point.Y, Cos(RadAngle), Sin(RadAngle))
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Line2D"/> class.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="direction">The direction.</param>
        public Line2D(Point2D location, Vector2D direction)
            : this()
        {
            this.location = location;
            this.direction = direction;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineSegment2D"/> class.
        /// </summary>
        /// <param name="a">Starting Point</param>
        /// <param name="b">Ending Point</param>
        public Line2D(Point2D a, Point2D b)
            : this(a.X, a.Y, b.X - a.X, b.Y - a.Y)
        { }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// The deconstruct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        public void Deconstruct(out double x, out double y, out double i, out double j)
        {
            x = Location.X;
            y = Location.Y;
            i = Direction.I;
            j = Direction.J;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        [DataMember, XmlElement, SoapElement]
        public Point2D Location
        {
            get { return location; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                location = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        [DataMember, XmlElement, SoapElement]
        public Vector2D Direction
        {
            get { return direction; }
            set
            {
                OnPropertyChanging();
                (this as IPropertyCaching).ClearCache();
                direction = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the bounds.
        /// </summary>
        [DataMember, XmlElement, SoapElement]
        public Rectangle2D Bounds => new Rectangle2D(location, location + direction);

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
        public static bool operator ==(Line2D left, Line2D right) => left.Equals(right);

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
        public static bool operator !=(Line2D left, Line2D right) => !(left == right);
        #endregion

        #region Operator Backing Methods
        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([AllowNull] object obj) => obj is Line2D d && Equals(d);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals([AllowNull] Line2D other) => location.Equals(other.location) && direction.Equals(other.direction);
        #endregion

        #region Methods
        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>
        /// The <see cref="Point2D" />.
        /// </returns>
        public Point2D Interpolate(double t) => Interpolators.Linear(t, location, location + direction);
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
        public override int GetHashCode() => HashCode.Combine(location, direction);

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
                return $"{nameof(Line2D)}";
            }

            var sep = Tokenizer.GetNumericListSeparator(formatProvider);
            return $"{nameof(Line2D)}={{{nameof(Location)}={Location.ToString(format, formatProvider)}{sep}{nameof(Direction)}={Direction.ToString(format, formatProvider)}}}";
        }
        #endregion Methods
    }
}