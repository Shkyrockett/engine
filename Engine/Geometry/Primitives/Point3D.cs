// <copyright file="Point3D.cs" company="Shkyrockett" >
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
using static Engine.Operations;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The <see cref="Point3D" /> struct.
    /// </summary>
    /// <seealso cref="IVector{T}" />
    [ComVisible(true)]
    [DataContract, Serializable]
    [TypeConverter(typeof(StructConverter<Point3D>))]
    [DebuggerDisplay("{ToString()}")]
    public struct Point3D
        : IVector<Point3D>
    {
        #region Implementations
        /// <summary>
        /// Represents a <see cref="Point3D" /> that has <see cref="X" />, <see cref="Y" />, and <see cref="Z" /> values set to zero.
        /// </summary>
        public static readonly Point3D Empty = new Point3D(0d, 0d, 0d);

        /// <summary>
        /// Represents a <see cref="Point3D" /> that has <see cref="X" />, <see cref="Y" />, and <see cref="Z" /> values set to 1.
        /// </summary>
        public static readonly Point3D Unit = new Point3D(1d, 1d, 1d);

        /// <summary>
        /// Represents a <see cref="Point3D" /> that has <see cref="X" />, <see cref="Y" />, and <see cref="Z" /> values set to NaN.
        /// </summary>
        public static readonly Point3D NaN = new Point3D(double.NaN, double.NaN, double.NaN);
        #endregion Implementations

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Point3D" /> class.
        /// </summary>
        /// <param name="point">The point.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point3D(Point3D point)
            : this(point.X, point.Y, point.Z)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point3D" /> class.
        /// </summary>
        /// <param name="x">The x component of the Point3D.</param>
        /// <param name="y">The y component of the Point3D.</param>
        /// <param name="z">The z component of the Point3D.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point3D(double x, double y, double z)
            : this()
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point3D" /> class.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point3D((double X, double Y, double Z) tuple)
            : this()
        {
            (X, Y, Z) = tuple;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Point3D" /> to a <see cref="ValueTuple{T1, T2, T3}" />.
        /// </summary>
        /// <param name="x">The <paramref name="x" />.</param>
        /// <param name="y">The <paramref name="y" />.</param>
        /// <param name="z">The <paramref name="z" />.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double x, out double y, out double z)
        {
            x = X;
            y = Y;
            z = Z;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the X component of a <see cref="Point3D" /> coordinate.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the Y component of a <see cref="Point3D" /> coordinate.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the Z component of a <see cref="Point3D" /> coordinate.
        /// </summary>
        /// <value>
        /// The z.
        /// </value>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Z { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Point3D" /> is empty.
        /// </summary>
        /// <value>
        ///   <see langword="true"/> if this instance is empty; otherwise, <see langword="false"/>.
        /// </value>
        [IgnoreDataMember, XmlIgnore, SoapIgnore]
        [Browsable(false)]
        public bool IsEmpty
            => Abs(X) < Epsilon
            && Abs(Y) < Epsilon
            && Abs(Z) < Epsilon;
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
        public static Point3D operator +(Point3D value) => UnaryAdd3D(value.X, value.Y, value.Z);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point3D" /> classes.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D operator +(Point3D value, double addend) => Add3D(value.X, value.Y, value.Z, addend);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point3D" /> classes.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D operator +(double value, Point3D addend) => Add3D(addend.X, addend.Y, addend.Z, value);

        /// <summary>
        /// Add two <see cref="Point3D" /> classes together.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator +(Point3D value, Point3D addend) => Add3D(value.X, value.Y, value.Z, addend.X, addend.Y, addend.Z);

        /// <summary>
        /// Operator Point + Vector
        /// </summary>
        /// <param name="value">The Point to be added to the Vector</param>
        /// <param name="addend">The Vector to be added to the Point</param>
        /// <returns>
        /// Point - The result of the addition
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D operator +(Point3D value, Vector3D addend) => Add3D(value.X, value.Y, value.Z, addend.I, addend.J, addend.K);

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
        public static Point3D operator +(Vector3D value, Point3D addend) => Add3D(value.I, value.J, value.K, addend.X, addend.Y, addend.Z);

        /// <summary>
        /// Unary subtraction operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D operator -(Point3D value) => UnaryNegate3D(value.X, value.Y, value.Z);

        /// <summary>
        /// Subtract a <see cref="Point3D" /> from a <see cref="double" /> value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D operator -(Point3D value, double subend) => SubtractSubtrahend3D(value.X, value.Y, value.Z, subend);

        /// <summary>
        /// Subtract a <see cref="Point3D" /> from a <see cref="double" /> value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D operator -(double value, Point3D subend) => SubtractFromMinuend3D(value, subend.X, subend.Y, subend.Z);

        /// <summary>
        /// Subtract a <see cref="Point3D" /> from another <see cref="Point3D" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3D operator -(Point3D value, Point3D subend) => Subtract3D(value.X, value.Y, value.Z, subend.X, subend.Y, subend.Z);

        /// <summary>
        /// Subtract a <see cref="Point3D" /> from another <see cref="Point3D" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D operator -(Point3D value, Vector3D subend) => new Point3D(value.X - subend.I, value.Y - subend.J, value.Z - subend.K);

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
        public static Point3D operator -(Vector3D value, Point3D subend) => Subtract3D(value.I, value.J, value.K, subend.X, subend.Y, subend.Z);

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
        public static Point3D operator *(double value, Point3D factor) => Scale3D(factor.X, factor.Y, factor.Z, value);

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
        public static Point3D operator *(Point3D value, double factor) => Scale3D(value.X, value.Y, value.Z, factor);

        /// <summary>
        /// Divide a <see cref="Point3D" /> by a value.
        /// </summary>
        /// <param name="divisor">The divisor value</param>
        /// <param name="dividend">The dividend to add.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D operator /(Point3D divisor, double dividend) => DivideByDividend3D(divisor.X, divisor.Y, divisor.Z, dividend);

        /// <summary>
        /// Divide a <see cref="Point3D" />
        /// </summary>
        /// <param name="divisor">The <see cref="Point3D" /></param>
        /// <param name="dividend">The divisor</param>
        /// <returns>
        /// A <see cref="Point3D" /> divided by the divisor
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D operator /(double divisor, Point3D dividend) => DivideDivisor3D(divisor, dividend.X, dividend.Y, dividend.Z);

        /// <summary>
        /// Compares two <see cref="Point3D" /> objects.
        /// The result specifies whether the values of the <see cref="X" /> and <see cref="Y" />
        /// values of the two <see cref="Point3D" /> objects are equal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Point3D left, Point3D right) => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="Point3D" /> objects.
        /// The result specifies whether the values of the <see cref="X" /> or <see cref="Y" />
        /// values of the two <see cref="Point3D" /> objects are unequal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Point3D left, Point3D right) => !Equals(left, right);

        /// <summary>
        /// Explicit conversion of the specified <see cref="Vector3D" /> structure to a <see cref="Point3D" /> structure.
        /// </summary>
        /// <param name="vector">The <see cref="Vector3D" /> to be converted.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Point3D(Vector3D vector) => new Point3D(vector.I, vector.J, vector.K);

        /// <summary>
        /// Explicit conversion to Vector
        /// </summary>
        /// <param name="point">Point - the Point to convert to a Vector</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector3D(Point3D point) => new Vector3D(point.X, point.Y, point.Z);

        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Point3D((double X, double Y, double Z) tuple) => new Point3D(tuple);

        /// <summary>
        /// Converts the specified <see cref="Point3D" /> structure to a <see cref="ValueTuple{T1, T2, T3}" /> structure.
        /// </summary>
        /// <param name="point">The <see cref="Point3D" /> to be converted.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator (double X, double Y, double Z)(Point3D point) => (point.X, point.Y, point.Z);
        #endregion Operators

        #region Factories
        /// <summary>
        /// Parse a string for a <see cref="Point3D" /> value.
        /// </summary>
        /// <param name="source"><see cref="string" /> with <see cref="Point3D" /> data</param>
        /// <returns>
        /// Returns an instance of the <see cref="Point3D" /> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
        /// </returns>
        [ParseMethod]
        public static Point3D Parse(string source)
            => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse a string for a <see cref="Point3D" /> value.
        /// </summary>
        /// <param name="source"><see cref="string" /> with <see cref="Point3D" /> data</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// Returns an instance of the <see cref="Point3D" /> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
        /// </returns>
        public static Point3D Parse(string source, IFormatProvider provider)
        {
            var tokenizer = new Tokenizer(source, provider);
            var firstToken = tokenizer.NextTokenRequired();

            var value = new Point3D(
                Convert.ToDouble(firstToken, provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider)
                );

            // There should be no more tokens in this string.
            tokenizer.LastTokenRequired();
            return value;
        }
        #endregion Factories

        #region Methods
        /// <summary>
        /// Compares two Vectors
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Point3D a, Point3D b)
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
        public static bool Equals(Point3D a, Point3D b) => (a.X == b.X) && (a.Y == b.Y) && (a.Z == b.Z);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is Point3D && Equals(this, (Point3D)obj);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Point3D value) => Equals(this, value);

        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>
        /// The <see cref="int" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Point3D" /> struct.
        /// </summary>
        /// <returns>
        /// A string representation of this <see cref="Point3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Point3D" /> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="provider">The <see cref="CultureInfo" /> provider.</param>
        /// <returns>
        /// A string representation of this <see cref="Point3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider) => ToString("R" /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Point3D" /> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The <see cref="CultureInfo" /> provider.</param>
        /// <returns>
        /// A string representation of this <see cref="Point3D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
        {
            if (this == null) return nameof(Point3D);
            var s = Tokenizer.GetNumericListSeparator(provider);
            return $"{nameof(Point3D)}({nameof(X)}:{X.ToString(format, provider)}{s} {nameof(Y)}:{Y.ToString(format, provider)}{s} {nameof(Z)}:{Z.ToString(format, provider)})";
        }

        /// <summary>
        /// Pluses the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static Point3D Plus(Point3D item) => +item;

        /// <summary>
        /// Adds the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Vector3D Add(Point3D left, Point3D right) => left + right;

        /// <summary>
        /// Negates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static Point3D Negate(Point3D item) => -item;

        /// <summary>
        /// Subtracts the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Vector3D Subtract(Point3D left, Point3D right) => left - right;

        /// <summary>
        /// Multiplies the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Point3D Multiply(double left, Point3D right) => left * right;

        /// <summary>
        /// Multiplies the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Point3D Multiply(Point3D left, double right) => left * right;

        /// <summary>
        /// Divides the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Point3D Divide(double left, Point3D right) => left / right;

        /// <summary>
        /// Divides the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static Point3D Divide(Point3D left, double right) => left / right;
        #endregion Methods
    }
}
