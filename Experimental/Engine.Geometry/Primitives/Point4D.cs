// <copyright file="Point4D.cs" company="Shkyrockett" >
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
using static System.Math;
using static Engine.Mathematics;
using static Engine.Operations;

namespace Engine
{
    /// <summary>
    /// The <see cref="Point4D"/> struct.
    /// </summary>
    [ComVisible(true)]
    [DataContract, Serializable]
    [TypeConverter(typeof(Point4DConverter))]
    //[TypeConverter(typeof(StructConverter<Point4D>))]
    [DebuggerDisplay("{nameof(Point4D)}({nameof(X)}: {X ?? double.NaN}, {nameof(Y)}: {Y ?? double.NaN}, {nameof(Z)}: {Z ?? double.NaN}, {nameof(W)}: {W ?? double.NaN})")]
    public struct Point4D
        : IVector<Point4D>
    {
        #region Implementations
        /// <summary>
        /// Represents a <see cref="Point4D"/> that has <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, and <see cref="W"/> values set to zero.
        /// </summary>
        public static readonly Point4D Empty = new Point4D(0d, 0d, 0d, 0d);

        /// <summary>
        /// Represents a <see cref="Point4D"/> that has <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, and <see cref="W"/> values set to 1.
        /// </summary>
        public static readonly Point4D Unit = new Point4D(1d, 1d, 1d, 1d);

        /// <summary>
        /// Represents a <see cref="Point4D"/> that has <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, and <see cref="W"/> values set to NaN.
        /// </summary>
        public static readonly Point4D NaN = new Point4D(double.NaN, double.NaN, double.NaN, double.NaN);
        #endregion Implementations

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Point4D"/> class.
        /// </summary>
        /// <param name="point"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point4D(Point4D point)
            : this(point.X, point.Y, point.Z, point.W)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point4D"/> class.
        /// </summary>
        /// <param name="x">The <paramref name="x"/> component of the <see cref="Point4D"/>.</param>
        /// <param name="y">The <paramref name="y"/> component of the <see cref="Point4D"/>.</param>
        /// <param name="z">The <paramref name="z"/> component of the <see cref="Point4D"/>.</param>
        /// <param name="w">The <paramref name="w"/> component of the <see cref="Point4D"/>.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point4D(double x, double y, double z, double w)
            : this()
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point4D"/> class.
        /// </summary>
        /// <param name="tuple"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point4D((double X, double Y, double Z, double W) tuple)
            : this()
        {
            (X, Y, Z, W) = tuple;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Point4D"/> to a <see cref="ValueTuple{T1, T2, T3}"/>.
        /// </summary>
        /// <param name="x">The <paramref name="x"/>.</param>
        /// <param name="y">The <paramref name="y"/>.</param>
        /// <param name="z">The <paramref name="z"/>.</param>
        /// <param name="z">The <paramref name="w"/>.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double x, out double y, out double z, out double w)
        {
            x = X;
            y = Y;
            z = Z;
            w = W;
        }
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="X"/> component of a <see cref="Point4D"/> coordinate.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Y"/> component of a <see cref="Point4D"/> coordinate.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Z"/> component of a <see cref="Point4D"/> coordinate.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double Z { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="W"/> component of a <see cref="Point4D"/> coordinate.
        /// </summary>
        [DataMember, XmlAttribute, SoapAttribute]
        public double W { get; set; }
        #endregion Properties

        #region Operators
        /// <summary>
        /// Unary addition operator.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D operator +(Point4D value) => UnaryAdd4D(value.X, value.Y, value.Z, value.W);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point4D"/> classes.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D operator +(Point4D value, double addend) => Add4D(value.X, value.Y, value.Z, value.W, addend);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point4D"/> classes.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D operator +(double value, Point4D addend) => Add4D(addend.X, addend.Y, addend.Z, addend.W, value);

        /// <summary>
        /// Add two <see cref="Point4D"/> classes together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator +(Point4D value, Point4D addend) => Add4D(value.X, value.Y, value.Z, value.W, addend.X, addend.Y, addend.Z, addend.W);

        /// <summary>
        /// Add two <see cref="Point4D"/> classes together.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D operator +(Point4D value, Vector4D addend) => Add4D(value.X, value.Y, value.Z, value.W, addend.I, addend.J, addend.K, addend.L);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D operator +(Vector4D value, Point4D addend) => Add4D(value.I, value.J, value.K, value.L, addend.X, addend.Y, addend.Z, addend.W);

        /// <summary>
        /// Unary subtraction operator.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator -(Point4D value) => UnaryNegate4D(value.X, value.Y, value.Z, value.W);

        /// <summary>
        /// Subtract a <see cref="Point4D"/> from a <see cref="double"/> value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D operator -(Point4D value, double subend) => SubtractSubtrahend4D(value.X, value.Y, value.Z, value.W, subend);

        /// <summary>
        /// Subtract a <see cref="Point3D"/> from a <see cref="double"/> value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D operator -(double value, Point4D subend) => SubtractFromMinuend4D(value, subend.X, subend.Y, subend.Z, subend.W);

        /// <summary>
        /// Subtract a <see cref="Point4D"/> from another <see cref="Point4D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4D operator -(Point4D value, Point4D subend) => Subtract4D(value.X, value.Y, value.Z, value.W, subend.X, subend.Y, subend.Z, subend.W);

        /// <summary>
        /// Subtract a <see cref="Point4D"/> from another <see cref="Point4D"/> class.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D operator -(Point4D value, Vector4D subend) => new Point4D(value.X - subend.I, value.Y - subend.J, value.Z - subend.K, value.W - subend.L);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subend"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D operator -(Vector4D value, Point4D subend) => Subtract4D(value.I, value.J, value.K, value.L, subend.X, subend.Y, subend.Z, subend.W);

        /// <summary>
        /// Scale a point
        /// </summary>
        /// <param name="factor"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D operator *(double value, Point4D factor) => Scale4D(factor.X, factor.Y, factor.Z, factor.W, value);

        /// <summary>
        /// Scale a point.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D operator *(Point4D value, double factor) => Scale4D(value.X, value.Y, value.Z, value.W, factor);

        /// <summary>
        /// Divide a <see cref="Point4D"/> by a value.
        /// </summary>
        /// <param name="divisor">The divisor value</param>
        /// <param name="dividend">The dividend to add.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D operator /(Point4D divisor, double dividend) => DivideByDividend4D(divisor.X, divisor.Y, divisor.Z, divisor.W, dividend);

        /// <summary>
        /// Divide a <see cref="Point4D"/>
        /// </summary>
        /// <param name="divisor">The <see cref="Point4D"/></param>
        /// <param name="dividend">The divisor</param>
        /// <returns>A <see cref="Point4D"/> divided by the divisor</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point4D operator /(double divisor, Point4D dividend) => DivideDivisor4D(divisor, dividend.X, dividend.Y, dividend.Z, dividend.W);

        /// <summary>
        /// Compares two <see cref="Point4D"/> objects.
        /// The result specifies whether the values of the <see cref="X"/> and <see cref="Y"/>
        /// values of the two <see cref="Point4D"/> objects are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Point4D left, Point4D right) => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="Point4D"/> objects.
        /// The result specifies whether the values of the <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, or <see cref="W"/>
        /// values of the two <see cref="Point4D"/> objects are unequal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Point4D left, Point4D right) => !Equals(left, right);

        /// <summary>
        /// Explicit conversion from the specified <see cref="Vector4D"/> structure to a <see cref="Point4D"/> structure.
        /// </summary>
        /// <param name="vector">The <see cref="Vector4D"/> to be converted.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Point4D(Vector4D vector) => new Point4D(vector.I, vector.J, vector.K, vector.L);

        /// <summary>
        /// Explicit conversion to Vector
        /// </summary>
        /// <param name="point"> Point - the Point to convert to a Vector </param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector4D(Point4D point) => new Vector4D(point.X, point.Y, point.Z, point.W);

        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Point4D((double X, double Y, double Z, double W) tuple) => new Point4D(tuple);

        /// <summary>
        /// Converts the specified <see cref="Point4D"/> structure to a <see cref="ValueTuple{T1, T2, T3, T4}"/> structure.
        /// </summary>
        /// <param name="point">The <see cref="Point4D"/> to be converted.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator (double X, double Y, double Z, double W)(Point4D point) => (point.X, point.Y, point.Z, point.W);
        #endregion Operators

        #region Factories
        /// <summary>
        /// Parse a string for a <see cref="Point4D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Point4D"/> data </param>
        /// <returns>
        /// Returns an instance of the <see cref="Point4D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        [ParseMethod]
        public static Point4D Parse(string source)
            => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse a string for a <see cref="Point4D"/> value.
        /// </summary>
        /// <param name="source"><see cref="string"/> with <see cref="Point4D"/> data </param>
        /// <param name="provider"></param>
        /// <returns>
        /// Returns an instance of the <see cref="Point4D"/> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture"/>.
        /// </returns>
        public static Point4D Parse(string source, IFormatProvider provider)
        {
            var tokenizer = new Tokenizer(source, provider);
            var value = new Point4D(
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider),
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider),
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
        /// Get the hash code.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(X, Y, Z);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Point4D other) => X == other.X && Y == other.Y && Z == other.Z && W == other.W;

        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => base.Equals(obj);

        public override string ToString() => base.ToString();

        /// <summary>
        /// Creates a string representation of this <see cref="Point4D"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The <see cref="CultureInfo"/> provider.</param>
        /// <returns>A string representation of this <see cref="Point4D"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (this == null) return nameof(Point4D);
            var s = Tokenizer.GetNumericListSeparator(formatProvider);
            return $"{nameof(Point4D)}({nameof(X)}: {X.ToString(format, formatProvider)}{s} {nameof(Y)}: {Y.ToString(format, formatProvider)}{s} {nameof(Z)}: {Z.ToString(format, formatProvider)}{s} {nameof(W)}: {W.ToString(format, formatProvider)})";
        }
        #endregion Methods
    }
}
