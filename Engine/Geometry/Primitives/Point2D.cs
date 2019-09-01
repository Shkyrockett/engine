// <copyright file="Point2D.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static Engine.Mathematics;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The <see cref="Point2D" /> struct.
    /// </summary>
    /// <seealso cref="IVector{T}" />
    [ComVisible(true)]
    [DataContract, Serializable]
    //[TypeConverter(typeof(Point2DConverter))]
    [TypeConverter(typeof(StructConverter<Point2D>))]
    [DebuggerDisplay("{ToString()}")]
    public struct Point2D
        : IVector<Point2D>
    {
        #region Implementations
        /// <summary>
        /// Represents a <see cref="Point2D" /> that has <see cref="X" />, and <see cref="Y" /> values set to zero.
        /// </summary>
        public static readonly Point2D Empty = new Point2D(0d, 0d);

        /// <summary>
        /// Represents a <see cref="Point2D" /> that has <see cref="X" />, and <see cref="Y" /> values set to 1.
        /// </summary>
        public static readonly Point2D Unit = new Point2D(1d, 1d);

        /// <summary>
        /// Represents a <see cref="Point2D" /> that has <see cref="X" />, and <see cref="Y" /> values set to NaN.
        /// </summary>
        public static readonly Point2D NaN = new Point2D(double.NaN, double.NaN);
        #endregion Implementations

        #region Constructors
        /// <summary>
        /// Initializes a new  instance of the <see cref="Point2D" /> class.
        /// </summary>
        /// <param name="point">The Point2D.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point2D(Point2D point)
            : this(point.X, point.Y)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point2D" /> class.
        /// </summary>
        /// <param name="x">The x component of the Point2D.</param>
        /// <param name="y">The y component of the Point2D.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point2D(double x, double y)
            : this()
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point2D" /> class.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point2D((double X, double Y) tuple)
            : this()
        {
            (X, Y) = tuple;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Point2D" /> to a <see cref="ValueTuple{T1, T2}" />.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double x, out double y)
        {
            x = X;
            y = Y;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the X component of a <see cref="Point2D" /> coordinate.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the Y component of a <see cref="Point2D" /> coordinate.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Y { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Point2D" /> is empty.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if this instance is empty; otherwise, <see langword="false"/>.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        public bool IsEmpty
            => Abs(X) < Epsilon
            && Abs(Y) < Epsilon;
        #endregion Properties

        #region Operators
        /// <summary>
        /// Unary addition operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator +(Point2D value) => Operations.UnaryAdd2D(value.X, value.Y);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point2D" /> classes.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator +(Point2D value, double addend) => Operations.Add2D(value.X, value.Y, addend);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point2D" /> classes.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator +(double value, Point2D addend) => Operations.Add2D(addend.X, addend.Y, value);

        /// <summary>
        /// Add two <see cref="Point2D" /> classes together.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator +(Point2D value, Point2D addend) => Operations.Add2D(value.X, value.Y, addend.X, addend.Y);

        /// <summary>
        /// Operator Point + Vector
        /// </summary>
        /// <param name="point">The Point to be added to the Vector</param>
        /// <param name="vector">The Vector to be added to the Point</param>
        /// <returns>
        /// Point - The result of the addition
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator +(Point2D point, Vector2D vector) => Operations.Add2D(point.X, point.Y, vector.I, vector.J);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator +(Vector2D value, Point2D addend) => Operations.Add2D(value.I, value.J, addend.X, addend.Y);

        /// <summary>
        /// Unary subtraction operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator -(Point2D value) => Operations.UnaryNegate2D(value.X, value.Y);

        /// <summary>
        /// Subtract a <see cref="Point2D" /> from a <see cref="double" /> value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator -(Point2D value, double subend) => Operations.SubtractSubtrahend2D(value.X, value.Y, subend);

        /// <summary>
        /// Subtract a <see cref="Point2D" /> from a <see cref="double" /> value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator -(double value, Point2D subend) => Operations.SubtractFromMinuend2D(value, subend.X, subend.Y);

        /// <summary>
        /// Subtract a <see cref="Point2D" /> from another <see cref="Point2D" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2D operator -(Point2D value, Point2D subend) => Operations.Subtract2D(value.X, value.Y, subend.X, subend.Y);

        /// <summary>
        /// Subtract a <see cref="Point2D" /> from another <see cref="Point2D" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator -(Point2D value, Vector2D subend) => Operations.Subtract2D(value.X, value.Y, subend.I, subend.J);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator -(Vector2D value, Point2D subend) => Operations.Subtract2D(value.I, value.J, subend.X, subend.Y);

        /// <summary>
        /// Scale a point
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="factor">The factor.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator *(double value, Point2D factor) => Operations.Scale2D(factor.X, factor.Y, value);

        /// <summary>
        /// Scale a point.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="factor">The factor.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator *(Point2D value, double factor) => Operations.Scale2D(value.X, value.Y, factor);

        /// <summary>
        /// Divide a <see cref="Point2D" /> by a value.
        /// </summary>
        /// <param name="divisor">The divisor value</param>
        /// <param name="dividend">The dividend to add.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator /(Point2D divisor, double dividend) => Operations.DivideByDividend2D(divisor.X, divisor.Y, dividend);

        /// <summary>
        /// Divide a Point2D
        /// </summary>
        /// <param name="divisor">The <see cref="Point2D" /></param>
        /// <param name="dividend">The divisor</param>
        /// <returns>
        /// A Point2D divided by the divisor
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D operator /(double divisor, Point2D dividend) => Operations.DivideDivisor2D(divisor, dividend.X, dividend.Y);

        /// <summary>
        /// Compares two <see cref="Point2D" /> objects.
        /// The result specifies whether the values of the <see cref="X" />, and <see cref="Y" />
        /// values of the two <see cref="Point2D" /> objects are equal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Point2D left, Point2D right) => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="Point2D" /> objects.
        /// The result specifies whether the values of the <see cref="X" /> or <see cref="Y" />
        /// values of the two <see cref="Point2D" /> objects are unequal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Point2D left, Point2D right) => !Equals(left, right);

        /// <summary>
        /// Explicit conversion of the specified <see cref="Vector2D" /> structure to a <see cref="Point2D" /> structure.
        /// </summary>
        /// <param name="vector">The <see cref="Vector2D" /> to be converted.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Point2D(Vector2D vector) => new Point2D(vector.I, vector.J);

        /// <summary>
        /// Explicit conversion to Vector
        /// </summary>
        /// <param name="point">Point - the Point to convert to a Vector</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector2D(Point2D point) => new Vector2D(point.X, point.Y);

        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Point2D((double X, double Y) tuple) => new Point2D(tuple);

        /// <summary>
        /// Converts the specified <see cref="Point2D" /> structure to a <see cref="ValueTuple{T1, T2}" /> structure.
        /// </summary>
        /// <param name="point">The <see cref="Point2D" /> to be converted.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator (double X, double Y)(Point2D point) => (point.X, point.Y);
        #endregion Operators

        #region Factories
        /// <summary>
        /// Parse a string for a <see cref="Point2D" /> value.
        /// </summary>
        /// <param name="source"><see cref="string" /> with <see cref="Point2D" /> data</param>
        /// <returns>
        /// Returns an instance of the <see cref="Point2D" /> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
        /// </returns>
        [ParseMethod]
        public static Point2D Parse(string source)
            => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse a string for a <see cref="Point2D" /> value.
        /// </summary>
        /// <param name="source"><see cref="string" /> with <see cref="Point2D" /> data</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// Returns an instance of the <see cref="Point2D" /> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
        /// </returns>
        public static Point2D Parse(string source, IFormatProvider provider)
        {
            var tokenizer = new Tokenizer(source, provider);
            var firstToken = tokenizer.NextTokenRequired();

            var value = new Point2D(
                Convert.ToDouble(firstToken, provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider)
                );

            // There should be no more tokens in this string.
            tokenizer.LastTokenRequired();
            return value;
        }
        #endregion Factories

        #region Methods
        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>
        /// The <see cref="int" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();

        /// <summary>
        /// The compare.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Point2D a, Point2D b)
            => Equals(a, b);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Point2D a, Point2D b) => (a.X == b.X) & (a.Y == b.Y);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is Point2D && Equals(this, (Point2D)obj);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Point2D value)
            => Equals(this, value);

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>
        /// The <see cref="Point2D" />.
        /// </returns>
        internal Point2D Clone() => new Point2D(X, Y);

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Point2D" /> struct.
        /// </summary>
        /// <returns>
        /// A string representation of this <see cref="Point2D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Point2D" /> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="provider">The <see cref="CultureInfo" /> provider.</param>
        /// <returns>
        /// A string representation of this <see cref="Point2D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider) => ToString("R" /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Point2D" /> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The <see cref="CultureInfo" /> provider.</param>
        /// <returns>
        /// A string representation of this <see cref="Point2D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Point2D);
            var s = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(Point2D)}({nameof(X)}:{X.ToString(format, provider)}{s} {nameof(Y)}:{Y.ToString(format, provider)})";
        }

        /// <summary>
        /// Pluses the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static Point2D Plus(Point2D item) => +item;

        /// <summary>
        /// Adds the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Vector2D Add(Point2D left, Point2D right) => left + right;

        /// <summary>
        /// Adds the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Point2D Add(Vector2D left, Point2D right) => left + right;

        /// <summary>
        /// Adds the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Point2D Add(Point2D left, Vector2D right) => left + right;

        /// <summary>
        /// Negates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static Point2D Negate(Point2D item) => -item;

        /// <summary>
        /// Subtracts the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Vector2D Subtract(Point2D left, Point2D right) => left - right;

        /// <summary>
        /// Subtracts the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Point2D Subtract(Vector2D left, Point2D right) => left - right;

        /// <summary>
        /// Subtracts the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Point2D Subtract(Point2D left, Vector2D right) => left - right;

        /// <summary>
        /// Multiplies the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Point2D Multiply(double left, Point2D right) => left * right;

        /// <summary>
        /// Multiplies the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Point2D Multiply(Point2D left, double right) => left * right;

        /// <summary>
        /// Divides the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Point2D Divide(double left, Point2D right) => left / right;

        /// <summary>
        /// Divides the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Point2D Divide(Point2D left, double right) => left / right;
        #endregion Methods
    }
}
