// <copyright file="Point4D.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static Engine.Operations;

namespace Engine;

/// <summary>
/// The <see cref="Point4D" /> struct.
/// </summary>
/// <seealso cref="IVector{T}" />
/// <seealso cref="IVector{Point4D}" />
[DataContract, Serializable]
[TypeConverter(typeof(Point4DConverter))]
[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public struct Point4D
    : IVector<Point4D>
{
    #region Implementations
    /// <summary>
    /// Represents a <see cref="Point4D" /> that has <see cref="X" />, <see cref="Y" />, <see cref="Z" />, and <see cref="W" /> values set to zero.
    /// </summary>
    public static readonly Point4D Empty = new(0d, 0d, 0d, 0d);

    /// <summary>
    /// Represents a <see cref="Point4D" /> that has <see cref="X" />, <see cref="Y" />, <see cref="Z" />, and <see cref="W" /> values set to 1.
    /// </summary>
    public static readonly Point4D Unit = new(1d, 1d, 1d, 1d);

    /// <summary>
    /// Represents a <see cref="Point4D" /> that has <see cref="X" />, <see cref="Y" />, <see cref="Z" />, and <see cref="W" /> values set to NaN.
    /// </summary>
    public static readonly Point4D NaN = new(double.NaN, double.NaN, double.NaN, double.NaN);
    #endregion Implementations

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Point4D" /> class.
    /// </summary>
    /// <param name="point">The point.</param>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Point4D(Point4D point)
        : this(point.X, point.Y, point.Z, point.W)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Point4D" /> class.
    /// </summary>
    /// <param name="x">The <paramref name="x" /> component of the <see cref="Point4D" />.</param>
    /// <param name="y">The <paramref name="y" /> component of the <see cref="Point4D" />.</param>
    /// <param name="z">The <paramref name="z" /> component of the <see cref="Point4D" />.</param>
    /// <param name="w">The <paramref name="w" /> component of the <see cref="Point4D" />.</param>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Point4D(double x, double y, double z, double w)
        : this()
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Point4D" /> class.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Point4D((double X, double Y, double Z, double W) tuple)
        : this()
    {
        (X, Y, Z, W) = tuple;
    }
    #endregion Constructors

    #region Deconstructors
    /// <summary>
    /// Deconstruct this <see cref="Point4D" /> to a <see cref="ValueTuple{T1, T2, T3, T4}" />.
    /// </summary>
    /// <param name="x">The <paramref name="x" />.</param>
    /// <param name="y">The <paramref name="y" />.</param>
    /// <param name="z">The <paramref name="z" />.</param>
    /// <param name="w">The <paramref name="w" />.</param>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly void Deconstruct(out double x, out double y, out double z, out double w)
    {
        x = X;
        y = Y;
        z = Z;
        w = W;
    }
    #endregion Deconstructors

    #region Properties
    /// <summary>
    /// Gets or sets the <see cref="X" /> component of a <see cref="Point4D" /> coordinate.
    /// </summary>
    /// <value>
    /// The x.
    /// </value>
    [DataMember(Name = nameof(X)), XmlAttribute(nameof(X)), SoapAttribute(nameof(X))]
    public double X { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Y" /> component of a <see cref="Point4D" /> coordinate.
    /// </summary>
    /// <value>
    /// The y.
    /// </value>
    [DataMember(Name = nameof(Y)), XmlAttribute(nameof(Y)), SoapAttribute(nameof(Y))]
    public double Y { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Z" /> component of a <see cref="Point4D" /> coordinate.
    /// </summary>
    /// <value>
    /// The z.
    /// </value>
    [DataMember(Name = nameof(Z)), XmlAttribute(nameof(Z)), SoapAttribute(nameof(Z))]
    public double Z { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="W" /> component of a <see cref="Point4D" /> coordinate.
    /// </summary>
    /// <value>
    /// The w.
    /// </value>
    [DataMember(Name = nameof(W)), XmlAttribute(nameof(W)), SoapAttribute(nameof(W))]
    public double W { get; set; }

    /// <summary>
    /// Gets the number of components in the Vector.
    /// </summary>
    /// <value>
    /// The count.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public readonly int Count => 4;
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D operator +(Point4D value) => Plus(value);

    /// <summary>
    /// Add an amount to both values in the <see cref="Point4D" /> classes.
    /// </summary>
    /// <param name="augend">The original value</param>
    /// <param name="addend">The amount to add.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D operator +(Point4D augend, double addend) => Add(augend, addend);

    /// <summary>
    /// Add an amount to both values in the <see cref="Point4D" /> classes.
    /// </summary>
    /// <param name="augend">The original value</param>
    /// <param name="addend">The amount to add.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D operator +(double augend, Point4D addend) => Add(addend, augend);

    /// <summary>
    /// Add two <see cref="Point4D" /> classes together.
    /// </summary>
    /// <param name="augend">The augend.</param>
    /// <param name="addend">The addend.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector4D operator +(Point4D augend, Point4D addend) => Add(augend, addend);

    /// <summary>
    /// Add two <see cref="Point4D" /> classes together.
    /// </summary>
    /// <param name="augend">The augend.</param>
    /// <param name="addend">The addend.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D operator +(Point4D augend, Vector4D addend) => Add(augend, addend);

    /// <summary>
    /// Add Points
    /// </summary>
    /// <param name="augend">The augend.</param>
    /// <param name="addend">The addend.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D operator +(Vector4D augend, Point4D addend) => Add(augend, addend);

    /// <summary>
    /// Unary subtraction operator.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector4D operator -(Point4D value) => Negate(value);

    /// <summary>
    /// Subtract a <see cref="Point4D" /> from a <see cref="double" /> value.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subend">The subend.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D operator -(Point4D minuend, double subend) => Subtract(minuend, subend);

    /// <summary>
    /// Subtract a <see cref="Point3D" /> from a <see cref="double" /> value.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subend">The subend.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D operator -(double minuend, Point4D subend) => Subtract(minuend, subend);

    /// <summary>
    /// Subtract a <see cref="Point4D" /> from another <see cref="Point4D" /> class.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subend">The subend.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector4D operator -(Point4D minuend, Point4D subend) => Subtract(minuend, subend);

    /// <summary>
    /// Subtract a <see cref="Point4D" /> from another <see cref="Point4D" /> class.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subend">The subend.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D operator -(Point4D minuend, Vector4D subend) => Subtract(minuend, subend);

    /// <summary>
    /// Subtract Points
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subend">The subend.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D operator -(Vector4D minuend, Point4D subend) => Subtract(minuend, subend);

    /// <summary>
    /// Scale a point
    /// </summary>
    /// <param name="multiplicand">The multiplicand.</param>
    /// <param name="multiplier">The multiplier.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D operator *(double multiplicand, Point4D multiplier) => Multiply(multiplicand, multiplier);

    /// <summary>
    /// Scale a point.
    /// </summary>
    /// <param name="multiplicand">The multiplicand.</param>
    /// <param name="multiplier">The multiplier.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D operator *(Point4D multiplicand, double multiplier) => Multiply(multiplicand, multiplier);

    /// <summary>
    /// Implements the operator *.
    /// </summary>
    /// <param name="multiplicand">The multiplicand.</param>
    /// <param name="multiplier">The multiplier.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D operator *(Matrix4x4D multiplicand, Point4D multiplier) => Multiply(multiplicand, multiplier);

    /// <summary>
    /// Implements the operator *.
    /// </summary>
    /// <param name="multiplicand">The multiplicand.</param>
    /// <param name="multiplier">The multiplier.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D operator *(Point4D multiplicand, Matrix4x4D multiplier) => Multiply(multiplicand, multiplier);

    /// <summary>
    /// Divide a <see cref="Point4D" /> by a value.
    /// </summary>
    /// <param name="dividend">The divisor value</param>
    /// <param name="divisor">The dividend value.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D operator /(Point4D dividend, double divisor) => Divide(dividend, divisor);

    /// <summary>
    /// Divide a <see cref="Point4D" />
    /// </summary>
    /// <param name="dividend">The <see cref="Point4D" /></param>
    /// <param name="divisor">The divisor</param>
    /// <returns>
    /// A <see cref="Point4D" /> divided by the divisor
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D operator /(double dividend, Point4D divisor) => Divide(dividend, divisor);

    /// <summary>
    /// Compares two <see cref="Point4D" /> objects.
    /// The result specifies whether the values of the <see cref="X" /> and <see cref="Y" />
    /// values of the two <see cref="Point4D" /> objects are equal.
    /// </summary>
    /// <param name="comparand">The comparand.</param>
    /// <param name="comparanda">The comparanda.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool operator ==(Point4D comparand, Point4D comparanda) => Equals(comparand, comparanda);

    /// <summary>
    /// Compares two <see cref="Point4D" /> objects.
    /// The result specifies whether the values of the <see cref="X" />, <see cref="Y" />, <see cref="Z" />, or <see cref="W" />
    /// values of the two <see cref="Point4D" /> objects are unequal.
    /// </summary>
    /// <param name="comparand">The comparand.</param>
    /// <param name="comparanda">The comparanda.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool operator !=(Point4D comparand, Point4D comparanda) => !Equals(comparand, comparanda);

    /// <summary>
    /// Explicit conversion from the specified <see cref="Subtract" /> structure to a <see cref="Point4D" /> structure.
    /// </summary>
    /// <param name="vector">The <see cref="Subtract" /> to be converted.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static explicit operator Point4D(Vector4D vector) => new(vector.I, vector.J, vector.K, vector.L);

    /// <summary>
    /// Explicit conversion to Vector
    /// </summary>
    /// <param name="point">Point - the Point to convert to a Vector</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static explicit operator Vector4D(Point4D point) => new(point.X, point.Y, point.Z, point.W);

    /// <summary>
    /// Implicit conversion from tuple.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static implicit operator Point4D((double X, double Y, double Z, double W) tuple) => new(tuple);

    /// <summary>
    /// Converts the specified <see cref="Point4D" /> structure to a <see cref="ValueTuple{T1, T2, T3, T4}" /> structure.
    /// </summary>
    /// <param name="point">The <see cref="Point4D" /> to be converted.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static implicit operator (double X, double Y, double Z, double W)(Point4D point) => (point.X, point.Y, point.Z, point.W);
    #endregion Operators

    #region Operator Backing Methods
    /// <summary>
    /// Pluses the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D Plus(Point4D value) => Operations.Plus(value.X, value.Y, value.Z, value.W);

    /// <summary>
    /// Adds the specified augend.
    /// </summary>
    /// <param name="augend">The augend.</param>
    /// <param name="addend">The addend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D Add(Point4D augend, double addend) => AddVectorUniform(augend.X, augend.Y, augend.Z, augend.W, addend);

    /// <summary>
    /// Adds the specified augend.
    /// </summary>
    /// <param name="augend">The augend.</param>
    /// <param name="addend">The addend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D Add(double augend, Point4D addend) => AddVectorUniform(addend.X, addend.Y, addend.Z, addend.W, augend);

    /// <summary>
    /// Adds the specified augend.
    /// </summary>
    /// <param name="augend">The augend.</param>
    /// <param name="addend">The addend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector4D Add(Point4D augend, Point4D addend) => AddVectors(augend.X, augend.Y, augend.Z, augend.W, addend.X, addend.Y, addend.Z, addend.W);

    /// <summary>
    /// Adds the specified augend.
    /// </summary>
    /// <param name="augend">The augend.</param>
    /// <param name="addend">The addend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D Add(Point4D augend, Vector4D addend) => AddVectors(augend.X, augend.Y, augend.Z, augend.W, addend.I, addend.J, addend.K, addend.L);

    /// <summary>
    /// Adds the specified augend.
    /// </summary>
    /// <param name="augend">The augend.</param>
    /// <param name="addend">The addend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D Add(Vector4D augend, Point4D addend) => AddVectors(augend.I, augend.J, augend.K, augend.L, addend.X, addend.Y, addend.Z, addend.W);

    /// <summary>
    /// Negates the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector4D Negate(Point4D value) => Operations.Negate(value.X, value.Y, value.Z, value.W);

    /// <summary>
    /// Subtracts the specified minuend.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subend">The subend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D Subtract(Point4D minuend, double subend) => SubtractVectorUniform(minuend.X, minuend.Y, minuend.Z, minuend.W, subend);

    /// <summary>
    /// Subtracts the specified minuend.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subend">The subend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D Subtract(double minuend, Point4D subend) => SubtractFromMinuend(minuend, subend.X, subend.Y, subend.Z, subend.W);

    /// <summary>
    /// Subtracts the specified minuend.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subend">The subend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector4D Subtract(Point4D minuend, Point4D subend) => SubtractVector(minuend.X, minuend.Y, minuend.Z, minuend.W, subend.X, subend.Y, subend.Z, subend.W);

    /// <summary>
    /// Subtracts the specified minuend.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subend">The subend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D Subtract(Point4D minuend, Vector4D subend) => SubtractVector(minuend.X, minuend.Y, minuend.Z, minuend.W, subend.I, subend.J, subend.K, subend.L);

    /// <summary>
    /// Vector4s the d.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subend">The subend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D Subtract(Vector4D minuend, Point4D subend) => SubtractVector(minuend.I, minuend.J, minuend.K, minuend.L, subend.X, subend.Y, subend.Z, subend.W);

    /// <summary>
    /// Multiplies the specified multiplicand.
    /// </summary>
    /// <param name="multiplicand">The multiplicand.</param>
    /// <param name="multiplier">The multiplier.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D Multiply(double multiplicand, Point4D multiplier) => ScaleVector(multiplier.X, multiplier.Y, multiplier.Z, multiplier.W, multiplicand);

    /// <summary>
    /// Multiplies the specified multiplicand.
    /// </summary>
    /// <param name="multiplicand">The multiplicand.</param>
    /// <param name="multiplier">The multiplier.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D Multiply(Point4D multiplicand, double multiplier) => ScaleVector(multiplicand.X, multiplicand.Y, multiplicand.Z, multiplicand.W, multiplier);

    /// <summary>
    /// Multiplies the specified multiplicand.
    /// </summary>
    /// <param name="multiplicand">The multiplicand.</param>
    /// <param name="multiplier">The multiplier.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D Multiply(Matrix4x4D multiplicand, Point4D multiplier) => MultiplyMatrix4x4ByVerticalVector4D(multiplicand.M0x0, multiplicand.M0x1, multiplicand.M0x2, multiplicand.M0x3, multiplicand.M1x0, multiplicand.M1x1, multiplicand.M1x2, multiplicand.M1x3, multiplicand.M2x0, multiplicand.M2x1, multiplicand.M2x2, multiplicand.M2x3, multiplicand.M3x0, multiplicand.M3x1, multiplicand.M3x2, multiplicand.M3x3, multiplier.X, multiplier.Y, multiplier.Z, multiplier.W);

    /// <summary>
    /// Multiplies the specified multiplicand.
    /// </summary>
    /// <param name="multiplicand">The multiplicand.</param>
    /// <param name="multiplier">The multiplier.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D Multiply(Point4D multiplicand, Matrix4x4D multiplier) => MultiplyHorizontalVector4DByMatrix4x4(multiplicand.X, multiplicand.Y, multiplicand.Z, multiplicand.W, multiplier.M0x0, multiplier.M0x1, multiplier.M0x2, multiplier.M0x3, multiplier.M1x0, multiplier.M1x1, multiplier.M1x2, multiplier.M1x3, multiplier.M2x0, multiplier.M2x1, multiplier.M2x2, multiplier.M2x3, multiplier.M3x0, multiplier.M3x1, multiplier.M3x2, multiplier.M3x3);

    /// <summary>
    /// Divides the specified dividend.
    /// </summary>
    /// <param name="dividend">The dividend.</param>
    /// <param name="divisor">The divisor.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D Divide(Point4D dividend, double divisor) => DivideVectorUniform(dividend.X, dividend.Y, dividend.Z, dividend.W, divisor);

    /// <summary>
    /// Divides the specified dividend.
    /// </summary>
    /// <param name="dividend">The dividend.</param>
    /// <param name="divisor">The divisor.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D Divide(double dividend, Point4D divisor) => DivideByVectorUniform(dividend, divisor.X, divisor.Y, divisor.Z, divisor.W);

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Equals([AllowNull] object obj) => obj is Point4D d && Equals(d);

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="other">The other.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly bool Equals([AllowNull] Point4D other) => X == other.X && Y == other.Y && Z == other.Z && W == other.W;

    /// <summary>
    /// Froms the vector4 d.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D FromVector4D(Vector4D vector) => new(vector.I, vector.J, vector.K, vector.L);

    /// <summary>
    /// Converts to vector4d.
    /// </summary>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly Vector4D ToVector4D() => new(X, Y, Z, W);

    /// <summary>
    /// Froms the value tuple.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point4D FromValueTuple((double X, double Y, double Z, double W) tuple) => new(tuple);

    /// <summary>
    /// Converts to valuetuple.
    /// </summary>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly (double X, double Y, double Z, double W) ToValueTuple() => (X, Y, Z, W);
    #endregion

    #region Factories
    /// <summary>
    /// Parse a string for a <see cref="Point4D" /> value.
    /// </summary>
    /// <param name="source"><see cref="string" /> with <see cref="Point4D" /> data</param>
    /// <returns>
    /// Returns an instance of the <see cref="Point4D" /> struct converted
    /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
    /// </returns>
    [ParseMethod]
    public static Point4D Parse(string source) => Parse(source, CultureInfo.InvariantCulture);

    /// <summary>
    /// Parse a string for a <see cref="Point4D" /> value.
    /// </summary>
    /// <param name="source"><see cref="string" /> with <see cref="Point4D" /> data</param>
    /// <param name="formatProvider">The provider.</param>
    /// <returns>
    /// Returns an instance of the <see cref="Point4D" /> struct converted
    /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
    /// </returns>
    public static Point4D Parse(string source, IFormatProvider formatProvider)
    {
        var tokenizer = new Tokenizer(source, formatProvider);
        var value = new Point4D(
            Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
            Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
            Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider),
            Convert.ToDouble(tokenizer.NextTokenRequired(), formatProvider)
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override readonly int GetHashCode() => HashCode.Combine(X, Y, Z, W);

    /// <summary>
    /// Creates a human-readable string that represents this <see cref="Point4D" /> struct.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> representation of this <see cref="Point4D" /> instance.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

    /// <summary>
    /// Creates a string representation of this <see cref="Point4D" /> struct based on the format string
    /// and IFormatProvider passed in.
    /// If the provider is null, the CurrentCulture is used.
    /// See the documentation for IFormattable for more information.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="formatProvider">The format provider.</param>
    /// <returns>
    /// A string representation of this <see cref="Point4D" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly string ToString(string format, IFormatProvider formatProvider)
    {
        if (this == null) return nameof(Point4D);
        var s = Tokenizer.GetNumericListSeparator(formatProvider);
        return $"{nameof(Point4D)}({nameof(X)}: {X.ToString(format, formatProvider)}{s} {nameof(Y)}: {Y.ToString(format, formatProvider)}{s} {nameof(Z)}: {Z.ToString(format, formatProvider)}{s} {nameof(W)}: {W.ToString(format, formatProvider)})";
    }

    /// <summary>
    /// Gets the debugger display.
    /// </summary>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private string GetDebuggerDisplay() => ToString();
    #endregion
}
