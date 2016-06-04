﻿using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class TestPoint2D
        : IFormattable
    {
        #region Implementations

        /// <summary>
        /// An Empty <see cref="TestPoint2D"/>.
        /// </summary>
        public static readonly TestPoint2D Empty = new TestPoint2D();

        /// <summary>
        /// A Unit <see cref="TestPoint2D"/>.
        /// </summary>
        public static readonly TestPoint2D Unit = new TestPoint2D(1, 1);

        #endregion

        #region Fields

        /// <summary>
        /// X component of a <see cref="TestPoint2D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        private double x;

        /// <summary>
        /// Y component of a <see cref="TestPoint2D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        private double y;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestPoint2D"/> class.
        /// </summary>
        /// <remarks></remarks>
        public TestPoint2D()
            : this(0, 0)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestPoint2D"/> class.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <remarks></remarks>
        public TestPoint2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        #endregion

        #region Properties

        /// <summary>
        /// X component of a <see cref="TestPoint2D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute]
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// Y component of a <see cref="TestPoint2D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute]
        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
        public double TotalDistance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
        public int Previous { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Point2D"/> is empty.
        /// </summary>
        [XmlIgnore]
        [Browsable(false)]
        public bool IsEmpty
        {
            get { return x == 0 && y == 0; }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Compares two <see cref="TestPoint2D"/> objects. 
        /// The result specifies whether the values of the <see cref="X"/> and <see cref="Y"/> 
        /// values of the two <see cref="TestPoint2D"/> objects are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(TestPoint2D left, TestPoint2D right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Compares two <see cref="TestPoint2D"/> objects. 
        /// The result specifies whether the values of the <see cref="X"/> or <see cref="Y"/> 
        /// values of the two <see cref="TestPoint2D"/> objects are unequal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(TestPoint2D left, TestPoint2D right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Compares two Vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(TestPoint2D a, TestPoint2D b)
        {
            return Equals(a, b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(TestPoint2D a, TestPoint2D b)
        {
            return a?.X == b?.X & a?.Y == b?.Y & a?.Previous == b?.Previous & a?.TotalDistance == b?.TotalDistance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            return obj is TestPoint2D && Equals(this, obj as TestPoint2D);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(TestPoint2D value)
        {
            return Equals(this, value);
        }

        /// <summary>
        /// Explicit conversion to Point2D.
        /// </summary>
        /// <returns>
        /// </returns>
        /// <param name="point"></param>
        public static explicit operator Point2D(TestPoint2D point)
        {
            return new Point2D(point.X, point.Y);
        }

        /// <summary>
        /// Implicit conversion to ItPoint2D.
        /// </summary>
        /// <returns>
        /// </returns>
        /// <param name="point"></param>
        public static implicit operator TestPoint2D(Point2D point)
        {
            return new TestPoint2D(point.X, point.Y);
        }

        #endregion

        #region Factories

        /// <summary>
        /// Parse a string for a <see cref="TestPoint2D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="TestPoint2D"/> data </param>
        /// <returns>
        /// Returns an instance of the <see cref="TestPoint2D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        [Pure]
        public static Point2D Parse(string source)
        {
            Tokenizer tokenizer = new Tokenizer(source, CultureInfo.InvariantCulture);
            Point2D value = new Point2D(
                Convert.ToDouble(tokenizer.NextTokenRequired(), CultureInfo.InvariantCulture),
                Convert.ToDouble(tokenizer.NextTokenRequired(), CultureInfo.InvariantCulture));
            // There should be no more tokens in this string.
            tokenizer.LastTokenRequired();
            return value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Pure]
        public override int GetHashCode()
        {
            return X.GetHashCode() ^
                   Y.GetHashCode();
        }

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="TestPoint2D"/> struct.
        /// </summary>
        /// <returns></returns>
        [Pure]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="TestPoint2D"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [Pure]
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="TestPoint2D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [Pure]
        string IFormattable.ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="TestPoint2D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [Pure]
        internal string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(TestPoint2D);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Point2D)}{{{nameof(X)}={X}{sep}{nameof(Y)}={Y}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}