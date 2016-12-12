// <copyright file="Point2D.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using static Engine.Maths;
using static System.Math;

namespace Engine
{
    /// <summary>
    ///
    /// </summary>
    [Serializable]
    [ComVisible(true)]
    [DisplayName(nameof(Point2D))]
    [TypeConverter(typeof(Point2DConverter))]
    public class Point2D
        : IFormattable
    {
        #region Implementations

        /// <summary>
        /// An Empty <see cref="Point2D"/>.
        /// </summary>
        public static readonly Point2D Empty = new Point2D();

        /// <summary>
        /// A Unit <see cref="Point2D"/>.
        /// </summary>
        public static readonly Point2D Unit = new Point2D(1, 1);

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new default instance of the <see cref="Point2D"/> class.
        /// </summary>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public Point2D()
            : this(0, 0)
        { }

        /// <summary>
        /// Initializes a new  instance of the <see cref="Point2D"/> class.
        /// </summary>
        /// <param name="point"></param>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public Point2D(Point2D point)
            : this(point.X, point.Y)
        { }

        /// <summary>
        /// Initializes a new  instance of the <see cref="Point2D"/> class.
        /// </summary>
        /// <param name="tuple"></param>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public Point2D((double X, double Y) tuple)
            : this(tuple.Item1, tuple.Item2)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point2D"/> class.
        /// </summary>
        /// <param name="x">The x component of the Point.</param>
        /// <param name="y">The y component of the Point.</param>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Properties

        /// <summary>
        /// X component of a <see cref="Point2D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute]
        public double X { get; set; }

        /// <summary>
        /// Y component of a <see cref="Point2D"/> coordinate.
        /// </summary>
        /// <remarks></remarks>
        [XmlAttribute]
        public double Y { get; set; }

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
        /// Unary addition operator.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator +(Point2D value)
            => new Point2D(+value.X, +value.Y);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point2D"/> classes.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator +(Point2D value, double addend)
            => value.Add(addend);

        /// <summary>
        /// Add two <see cref="Point2D"/> classes together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator +(Point2D value, Point2D addend)
            => value.Add(addend);

        /// <summary>
        /// Add two <see cref="Point2D"/> classes together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator +(Point2D value, Size2D addend)
            => value.Add(addend);

        /// <summary>
        /// Operator Point + Vector
        /// </summary>
        /// <param name="point"> The Point to be added to the Vector </param>
        /// <param name="vector"> The Vector to be added to the Point </param>
        /// <returns>
        /// Point - The result of the addition
        /// </returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator +(Point2D point, Vector2D vector)
            => new Point2D(point.X + vector.I, point.Y + vector.J);

        /// <summary>
        /// Unary subtraction operator.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator -(Point2D value)
            => new Point2D(-value.X, -value.Y);

        /// <summary>
        /// Subtract a <see cref="Point2D"/> from a <see cref="double"/> value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator -(Point2D value, double subend)
            => value.Subtract(subend);

        /// <summary>
        /// Subtract a <see cref="Point2D"/> from another <see cref="Point2D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator -(Point2D value, Point2D subend)
            => value.Subtract(subend);

        /// <summary>
        /// Subtract a <see cref="Point2D"/> from another <see cref="Point2D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator -(Point2D value, Size2D subend)
            => value.Subtract(subend);

        /// <summary>
        /// Subtract a <see cref="Point2D"/> from another <see cref="Point2D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator -(Point2D value, Vector2D subend)
            => value.Subtract(subend);

        /// <summary>
        /// Scale a point
        /// </summary>
        /// <param name="factor"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator *(double value, Point2D factor)
            => new Point2D(value * factor.X, value * factor.Y);

        /// <summary>
        /// Scale a point.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator *(Point2D value, double factor)
            => new Point2D(value.X * factor, value.Y * factor);

        /// <summary>
        /// Multiply a point by a matrix.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator *(Point2D value, Matrix2D matrix)
            => matrix.Transform(value);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point2D"/> classes.
        /// </summary>
        /// <param name="divisor">The original value</param>
        /// <param name="dividend">The amount to add.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator /(Point2D divisor, double dividend)
            => new Point2D(divisor.X / dividend, divisor.Y / dividend);

        /// <summary>
        /// Compares two <see cref="Point2D"/> objects.
        /// The result specifies whether the values of the <see cref="X"/> and <see cref="Y"/>
        /// values of the two <see cref="Point2D"/> objects are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Point2D left, Point2D right)
            => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="Point2D"/> objects.
        /// The result specifies whether the values of the <see cref="X"/> or <see cref="Y"/>
        /// values of the two <see cref="Point2D"/> objects are unequal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Point2D left, Point2D right)
            => !Equals(left, right);

        /// <summary>
        /// Compares two Vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Point2D a, Point2D b)
            => Equals(a, b);

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Point2D a, Point2D b)
            => (a?.X == b?.X) & (a?.Y == b?.Y);

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is Point2D && Equals(this, obj as Point2D);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Point2D value)
            => Equals(this, value);

        /// <summary>
        /// Explicit conversion to <see cref="Size2D"/>.
        /// Note that since Size cannot contain negative values,
        /// the resulting size will contains the absolute values of X and Y
        /// </summary>
        /// <param name="point"> Point - the Point to convert to a Size </param>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Size2D(Point2D point)
            => new Size2D(Abs(point.X), Abs(point.Y));

        /// <summary>
        /// Explicit conversion to Vector
        /// </summary>
        /// <param name="point"> Point - the Point to convert to a Vector </param>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector2D(Point2D point)
            => new Vector2D(point.X, point.Y);

        /// <summary>
        /// Explicit conversion from Vector2D to Point2D.
        /// </summary>
        /// <param name="point"> Point - the Point to convert to a Vector </param>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Point2D(Vector2D point)
            => new Point2D(point.I, point.J);

        /// <summary>
        ///
        /// </summary>
        /// <param name="tuple"></param>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Point2D((double X, double Y) tuple)
            => new Point2D(tuple.Item1, tuple.Item2);

        /// <summary>
        ///
        /// </summary>
        /// <param name="that"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point2D Min(Point2D that)
            => new Point2D(Math.Min(X, that.X), Math.Min(Y, that.Y));

        /// <summary>
        ///
        /// </summary>
        /// <param name="that"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point2D Max(Point2D that)
            => new Point2D(Math.Max(X, that.X), Math.Max(Y, that.Y));

        #endregion

        #region Factories

        /// <summary>
        /// Create a Random <see cref="Point2D"/>.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Random()
            => new Point2D((2 * RandomNumberGenerator.NextDouble()) - 1, (2 * RandomNumberGenerator.NextDouble()) - 1);

        /// <summary>
        /// Parse a string for a <see cref="Point2D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Point2D"/> data </param>
        /// <returns>
        /// Returns an instance of the <see cref="Point2D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        [Pure]
        public static Point2D Parse(string source)
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
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
            => X.GetHashCode()
            ^ Y.GetHashCode();

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Point2D"/>.
        /// </summary>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Point2D"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Point2D"/> class based on the format string
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        string IFormattable.ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Point2D"/> class based on the format string
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal string ConvertToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Point2D);
            char sep = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(Point2D)}{{{nameof(X)}={X.ToString(format, provider)}{sep}{nameof(Y)}={Y.ToString(format, provider)}}}";
        }

        #endregion
    }
}
