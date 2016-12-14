// <copyright file="TestPoint2D.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using static System.Math;
using static Engine.Maths;

namespace Engine
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
            X = x;
            Y = y;
        }

        #endregion

        #region Properties

        /// <summary>
        /// X component of a <see cref="TestPoint2D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute]
        public double X { get; set; }

        /// <summary>
        /// Y component of a <see cref="TestPoint2D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute]
        public double Y { get; set; }

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
            => Abs(X) < Epsilon
            && Abs(Y) < Epsilon;

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
            => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="TestPoint2D"/> objects. 
        /// The result specifies whether the values of the <see cref="X"/> or <see cref="Y"/> 
        /// values of the two <see cref="TestPoint2D"/> objects are unequal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(TestPoint2D left, TestPoint2D right)
            => !Equals(left, right);

        /// <summary>
        /// Compares two Vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(TestPoint2D a, TestPoint2D b)
            => Equals(a, b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(TestPoint2D a, TestPoint2D b)
            => a?.X == b?.X & a?.Y == b?.Y & a?.Previous == b?.Previous & a?.TotalDistance == b?.TotalDistance;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is TestPoint2D && Equals(this, obj as TestPoint2D);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(TestPoint2D value)
            => Equals(this, value);

        /// <summary>
        /// Explicit conversion to Point2D.
        /// </summary>
        /// <returns>
        /// </returns>
        /// <param name="point"></param>
        public static explicit operator Point2D(TestPoint2D point)
            => new Point2D(point.X, point.Y);

        /// <summary>
        /// Implicit conversion to ItPoint2D.
        /// </summary>
        /// <returns>
        /// </returns>
        /// <param name="point"></param>
        public static implicit operator TestPoint2D(Point2D point)
            => new TestPoint2D(point.X, point.Y);

        #endregion

        #region Factories

        /// <summary>
        /// Parse a string for a <see cref="TestPoint2D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="TestPoint2D"/> data </param>
        /// <returns>
        /// Returns an instance of the <see cref="TestPoint2D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>        public static Point2D Parse(string source)
        {
            var tokenizer = new Tokenizer(source, CultureInfo.InvariantCulture);
            var value = new Point2D(
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
        /// <returns></returns>        public override int GetHashCode()
            => X.GetHashCode()
            ^ Y.GetHashCode();

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="TestPoint2D"/> struct.
        /// </summary>
        /// <returns></returns>        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="TestPoint2D"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>        public string ToString(IFormatProvider provider)
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
        /// </returns>        string IFormattable.ToString(string format, IFormatProvider provider)
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
        /// </returns>        internal string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(TestPoint2D);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            IFormattable formatable = $"{nameof(Point2D)}{{{nameof(X)}={X}{sep}{nameof(Y)}={Y}}}";
            return formatable.ToString(format, provider);
        }

        #endregion
    }
}
