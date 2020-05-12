// <copyright file="Point5D.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static Engine.Operations;

namespace Engine
{
    /// <summary>
    /// The <see cref="Point5D" /> struct.
    /// </summary>
    /// <seealso cref="Engine.IVector{T}" />
    /// <seealso cref="IVector{Point5D}" />
    [DataContract, Serializable]
    [TypeConverter(typeof(Point5DConverter))]
    [DebuggerDisplay("{ToString()}")]
    public struct Point5D
        : IVector<Point5D>
    {
        #region Implementations
        /// <summary>
        /// Represents a <see cref="Point5D" /> that has <see cref="X" />, <see cref="Y" />, <see cref="Z" />, <see cref="W" />, and <see cref="V" /> values set to zero.
        /// </summary>
        public static readonly Point5D Empty = new Point5D(0d, 0d, 0d, 0d, 0d);

        /// <summary>
        /// Represents a <see cref="Point5D" /> that has <see cref="X" />, <see cref="Y" />, <see cref="Z" />, <see cref="W" />, and <see cref="V" /> values set to 1.
        /// </summary>
        public static readonly Point5D Unit = new Point5D(1d, 1d, 1d, 1d, 1d);

        /// <summary>
        /// Represents a <see cref="Point5D" /> that has <see cref="X" />, <see cref="Y" />, <see cref="Z" />, <see cref="W" />, and <see cref="V" /> values set to NaN.
        /// </summary>
        public static readonly Point5D NaN = new Point5D(double.NaN, double.NaN, double.NaN, double.NaN, double.NaN);
        #endregion Implementations

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Point5D" /> class.
        /// </summary>
        /// <param name="point">The point.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point5D(Point5D point)
            : this(point.X, point.Y, point.Z, point.W, point.V)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point5D" /> class.
        /// </summary>
        /// <param name="x">The <paramref name="x" /> component of the <see cref="Point5D" />.</param>
        /// <param name="y">The <paramref name="y" /> component of the <see cref="Point5D" />.</param>
        /// <param name="z">The <paramref name="z" /> component of the <see cref="Point5D" />.</param>
        /// <param name="w">The <paramref name="w" /> component of the <see cref="Point5D" />.</param>
        /// <param name="v">The v.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point5D(double x, double y, double z, double w, double v)
            : this()
        {
            (X, Y, Z, W, V) = (x, y, z, w, v);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point5D" /> class.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point5D((double X, double Y, double Z, double W, double V) tuple)
            : this()
        {
            (X, Y, Z, W, V) = tuple;
        }
        #endregion Constructors

        #region Deconstructors
        /// <summary>
        /// Deconstruct this <see cref="Point5D" /> to a <see cref="ValueTuple{T1, T2, T3, T4}" />.
        /// </summary>
        /// <param name="x">The <paramref name="x" />.</param>
        /// <param name="y">The <paramref name="y" />.</param>
        /// <param name="z">The <paramref name="z" />.</param>
        /// <param name="w">The <paramref name="w" />.</param>
        /// <param name="v">The v.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out double x, out double y, out double z, out double w, out double v) => (x, y, z, w, v) = (X, Y, Z, W, V);
        #endregion Deconstructors

        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="X" /> component of a <see cref="Point5D" /> coordinate.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        [DataMember(Name = nameof(X)), XmlAttribute(nameof(X)), SoapAttribute(nameof(X))]
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Y" /> component of a <see cref="Point5D" /> coordinate.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        [DataMember(Name = nameof(Y)), XmlAttribute(nameof(Y)), SoapAttribute(nameof(Y))]
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Z" /> component of a <see cref="Point5D" /> coordinate.
        /// </summary>
        /// <value>
        /// The z.
        /// </value>
        [DataMember(Name = nameof(Z)), XmlAttribute(nameof(Z)), SoapAttribute(nameof(Z))]
        public double Z { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="W" /> component of a <see cref="Point5D" /> coordinate.
        /// </summary>
        /// <value>
        /// The w.
        /// </value>
        [DataMember(Name = nameof(W)), XmlAttribute(nameof(W)), SoapAttribute(nameof(W))]
        public double W { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="V" /> component of a <see cref="Point5D" /> coordinate.
        /// </summary>
        /// <value>
        /// The v.
        /// </value>
        [DataMember(Name = nameof(V)), XmlAttribute(nameof(V)), SoapAttribute(nameof(V))]
        public double V { get; set; }
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
        public static Point5D operator +(Point5D value) => Plus(value);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point5D" /> classes.
        /// </summary>
        /// <param name="augend">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D operator +(Point5D augend, double addend) => Add(augend, addend);

        /// <summary>
        /// Add an amount to both values in the <see cref="Point5D" /> classes.
        /// </summary>
        /// <param name="augend">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D operator +(double augend, Point5D addend) => Add(addend, augend);

        /// <summary>
        /// Add two <see cref="Point5D" /> classes together.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D operator +(Point5D augend, Point5D addend) => Add(augend, addend);

        /// <summary>
        /// Add two <see cref="Point5D" /> classes together.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D operator +(Point5D augend, Vector5D addend) => Add(augend, addend);

        /// <summary>
        /// Add Points
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D operator +(Vector5D augend, Point5D addend) => Add(augend, addend);

        /// <summary>
        /// Unary subtraction operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D operator -(Point5D value) => Negate(value);

        /// <summary>
        /// Subtract a <see cref="Point5D" /> from a <see cref="double" /> value.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D operator -(Point5D minuend, double subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="Point3D" /> from a <see cref="double" /> value.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D operator -(double minuend, Point5D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="Point5D" /> from another <see cref="Point5D" /> class.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D operator -(Point5D minuend, Point5D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract a <see cref="Point5D" /> from another <see cref="Point5D" /> class.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D operator -(Point5D minuend, Vector5D subend) => Subtract(minuend, subend);

        /// <summary>
        /// Subtract Points
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D operator -(Vector5D minuend, Point5D subend) => Vector5D(minuend, subend);

        /// <summary>
        /// Scale a point
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D operator *(double multiplicand, Point5D multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Scale a point.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D operator *(Point5D multiplicand, double multiplier) => Multiply(multiplicand, multiplier);

        /// <summary>
        /// Divide a <see cref="Point5D" /> by a value.
        /// </summary>
        /// <param name="dividend">The divisor value</param>
        /// <param name="divisor">The dividend value.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D operator /(Point5D dividend, double divisor) => Divide(dividend, divisor);

        /// <summary>
        /// Divide a <see cref="Point5D" />
        /// </summary>
        /// <param name="dividend">The <see cref="Point5D" /></param>
        /// <param name="divisor">The divisor</param>
        /// <returns>
        /// A <see cref="Point5D" /> divided by the divisor
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D operator /(double dividend, Point5D divisor) => Divide(dividend, divisor);

        /// <summary>
        /// Compares two <see cref="Point5D" /> objects.
        /// The result specifies whether the values of the <see cref="X" /> and <see cref="Y" />
        /// values of the two <see cref="Point5D" /> objects are equal.
        /// </summary>
        /// <param name="comparand">The comparand.</param>
        /// <param name="comparanda">The comparanda.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Point5D comparand, Point5D comparanda) => Equals(comparand, comparanda);

        /// <summary>
        /// Compares two <see cref="Point5D" /> objects.
        /// The result specifies whether the values of the <see cref="X" />, <see cref="Y" />, <see cref="Z" />, or <see cref="W" />
        /// values of the two <see cref="Point5D" /> objects are unequal.
        /// </summary>
        /// <param name="comparand">The comparand.</param>
        /// <param name="comparanda">The comparanda.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Point5D comparand, Point5D comparanda) => !Equals(comparand, comparanda);

        /// <summary>
        /// Explicit conversion from the specified <see cref="Vector5D" /> structure to a <see cref="Point5D" /> structure.
        /// </summary>
        /// <param name="vector">The <see cref="Vector5D" /> to be converted.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Point5D(Vector5D vector) => new Point5D(vector.I, vector.J, vector.K, vector.L, vector.M);

        /// <summary>
        /// Explicit conversion to Vector
        /// </summary>
        /// <param name="point">Point - the Point to convert to a Vector</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Vector5D(Point5D point) => new Vector5D(point.X, point.Y, point.Z, point.W, point.V);

        /// <summary>
        /// Implicit conversion from tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Point5D((double X, double Y, double Z, double W, double V) tuple) => new Point5D(tuple);

        /// <summary>
        /// Converts the specified <see cref="Point5D" /> structure to a <see cref="ValueTuple{T1, T2, T3, T4, T5}" /> structure.
        /// </summary>
        /// <param name="point">The <see cref="Point5D" /> to be converted.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator (double X, double Y, double Z, double W, double V)(Point5D point) => (point.X, point.Y, point.Z, point.W, point.V);
        #endregion Operators

        #region Operator Backing Methods
        /// <summary>
        /// Pluses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D Plus(Point5D value) => Operations.Plus(value.X, value.Y, value.Z, value.W, value.V);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D Add(Point5D augend, double addend) => AddVectorUniform(augend.X, augend.Y, augend.Z, augend.W, augend.V, addend);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D Add(double augend, Point5D addend) => AddVectorUniform(addend.X, addend.Y, addend.Z, addend.W, addend.V, augend);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Add(Point5D augend, Point5D addend) => AddVectors(augend.X, augend.Y, augend.Z, augend.W, augend.V, addend.X, addend.Y, addend.Z, addend.W, addend.V);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D Add(Point5D augend, Vector5D addend) => AddVectors(augend.X, augend.Y, augend.Z, augend.W, augend.V, addend.I, addend.J, addend.K, addend.L, addend.M);

        /// <summary>
        /// Adds the specified augend.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D Add(Vector5D augend, Point5D addend) => AddVectors(augend.I, augend.J, augend.K, augend.L, augend.M, addend.X, addend.Y, addend.Z, addend.W, addend.V);

        /// <summary>
        /// Negates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Negate(Point5D value) => Operations.Negate(value.X, value.Y, value.Z, value.W, value.V);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D Subtract(Point5D minuend, double subend) => SubtractVectorUniform(minuend.X, minuend.Y, minuend.Z, minuend.W, minuend.V, subend);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D Subtract(double minuend, Point5D subend) => SubtractFromMinuend(minuend, subend.X, subend.Y, subend.Z, subend.W, subend.V);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector5D Subtract(Point5D minuend, Point5D subend) => SubtractVector(minuend.X, minuend.Y, minuend.Z, minuend.W, minuend.V, subend.X, subend.Y, subend.Z, subend.W, subend.V);

        /// <summary>
        /// Subtracts the specified minuend.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D Subtract(Point5D minuend, Vector5D subend) => SubtractVector(minuend.X, minuend.Y, minuend.Z, minuend.W, minuend.V, subend.I, subend.J, subend.K, subend.L, subend.M);

        /// <summary>
        /// Vector4s the d.
        /// </summary>
        /// <param name="minuend">The minuend.</param>
        /// <param name="subend">The subend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D Vector5D(Vector5D minuend, Point5D subend) => SubtractVector(minuend.I, minuend.J, minuend.K, minuend.L, minuend.M, subend.X, subend.Y, subend.Z, subend.W, subend.V);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D Multiply(double multiplicand, Point5D multiplier) => ScaleVector(multiplier.X, multiplier.Y, multiplier.Z, multiplier.W, multiplier.V, multiplicand);

        /// <summary>
        /// Multiplies the specified multiplicand.
        /// </summary>
        /// <param name="multiplicand">The multiplicand.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D Multiply(Point5D multiplicand, double multiplier) => ScaleVector(multiplicand.X, multiplicand.Y, multiplicand.Z, multiplicand.W, multiplicand.V, multiplier);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D Divide(Point5D dividend, double divisor) => DivideVectorUniform(dividend.X, dividend.Y, dividend.Z, dividend.W, dividend.V, divisor);

        /// <summary>
        /// Divides the specified dividend.
        /// </summary>
        /// <param name="dividend">The dividend.</param>
        /// <param name="divisor">The divisor.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D Divide(double dividend, Point5D divisor) => DivideByVectorUniform(dividend, divisor.X, divisor.Y, divisor.Z, divisor.W, divisor.V);

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([AllowNull] object obj) => obj is Point5D d && Equals(d);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals([AllowNull] Point5D other) => X == other.X && Y == other.Y && Z == other.Z && W == other.W && W == other.V;

        /// <summary>
        /// Froms the vector4 d.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D FromVector5D(Vector5D vector) => new Point5D(vector.I, vector.J, vector.K, vector.L, vector.M);

        /// <summary>
        /// Converts to vector5D.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector5D ToVector5D() => new Vector5D(X, Y, Z, W, V);

        /// <summary>
        /// Froms the value tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point5D FromValueTuple((double X, double Y, double Z, double W, double V) tuple) => new Point5D(tuple);

        /// <summary>
        /// Converts to valuetuple.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (double X, double Y, double Z, double W, double V) ToValueTuple() => (X, Y, Z, W, V);
        #endregion

        #region Factories
        /// <summary>
        /// Parse a string for a <see cref="Point5D" /> value.
        /// </summary>
        /// <param name="source"><see cref="string" /> with <see cref="Point5D" /> data</param>
        /// <returns>
        /// Returns an instance of the <see cref="Point5D" /> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
        /// </returns>
        [ParseMethod]
        public static Point5D Parse(string source) => Parse(source, CultureInfo.InvariantCulture);

        /// <summary>
        /// Parse a string for a <see cref="Point5D" /> value.
        /// </summary>
        /// <param name="source"><see cref="string" /> with <see cref="Point5D" /> data</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// Returns an instance of the <see cref="Point5D" /> struct converted
        /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
        /// </returns>
        public static Point5D Parse(string source, IFormatProvider provider)
        {
            var tokenizer = new Tokenizer(source, provider);
            var value = new Point5D(
                Convert.ToDouble(tokenizer.NextTokenRequired(), provider),
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

        #region Standard Methods
        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>
        /// The <see cref="int" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(X, Y, Z, W, V);

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Point5D" /> struct.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> representation of this <see cref="Point5D" /> instance.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Point5D" /> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The format provider.</param>
        /// <returns>
        /// A string representation of this <see cref="Point5D" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (this == null) return nameof(Point5D);
            var s = Tokenizer.GetNumericListSeparator(formatProvider);
            return $"{nameof(Point5D)}({nameof(X)}: {X.ToString(format, formatProvider)}{s} {nameof(Y)}: {Y.ToString(format, formatProvider)}{s} {nameof(Z)}: {Z.ToString(format, formatProvider)}{s} {nameof(W)}: {W.ToString(format, formatProvider)}{s} {nameof(V)}: {V.ToString(format, formatProvider)})";
        }
        #endregion
    }
}
