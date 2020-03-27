// <copyright file="Ray.cs" company="Shkyrockett" >
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
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Engine
{
    /// <summary>
    /// The ray class.
    /// </summary>
    [GraphicsObject]
    [DataContract, Serializable]
    [TypeConverter(typeof(StructConverter<Ray2D>))]
    [DebuggerDisplay("{ToString()}")]
    public struct Ray2D
        : IShapeSegment, IPropertyCaching, IEquatable<Ray2D>
    {
        #region Implementations
        /// <summary>
        /// Represents a Engine.Geometry.Segment that is null.
        /// </summary>
        public static readonly Ray2D Empty = new Ray2D(0d, 0d, 0d, 0d);
        #endregion Implementations

        #region Event Delegates
        /// <summary>
        /// The property changed event of the <see cref="PropertyChangedEventHandler" />.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The property changing event of the <see cref="PropertyChangingEventHandler" />.
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
        /// Initializes a new instance of the <see cref="Ray2D" /> class.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="direction">The direction.</param>
        public Ray2D(Point2D location, Vector2D direction)
            : this()
        {
            this.location = location;
            this.direction = direction;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ray2D" /> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        public Ray2D(double x, double y, double i, double j)
            : this(new Point2D(x, y), new Vector2D(i, j))
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
        /// <value>
        /// The location.
        /// </value>
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
        /// <value>
        /// The direction.
        /// </value>
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
        /// <value>
        /// The bounds.
        /// </value>
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
        public static bool operator ==(Ray2D left, Ray2D right) => left.Equals(right);

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
        public static bool operator !=(Ray2D left, Ray2D right) => !(left == right);
        #endregion

        #region Operator Backing Methods
        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is Ray2D d && Equals(d);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Ray2D other) => location.Equals(other.location) && direction.Equals(other.direction);

        /// <summary>
        /// The to line.
        /// </summary>
        /// <returns>
        /// The <see cref="Line2D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Line2D ToLine() => new Line2D(location, direction);
        #endregion

        #region Methods
        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>
        /// The <see cref="Point2D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        /// Creates a string representation of this <see cref="Ray2D" /> struct based on the format string
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
            if (this == null)
            {
                return $"{nameof(Ray2D)}";
            }

            var sep = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(Ray2D)}={{{nameof(Location)}={Location.ToString(format, provider)}{sep}{nameof(Direction)}={Direction.ToString(format, provider)}}}";
        }
        #endregion Methods
    }
}