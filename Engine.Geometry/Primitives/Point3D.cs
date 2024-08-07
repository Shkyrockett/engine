﻿// <copyright file="Point3D.cs" company="Shkyrockett" >
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
using static System.Math;

namespace Engine;

/// <summary>
/// The <see cref="Point3D" /> struct.
/// </summary>
/// <seealso cref="IVector{T}" />
/// <seealso cref="IVector{T}" />
[DataContract, Serializable]
[TypeConverter(typeof(Point3DConverter))]
[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public struct Point3D
    : IVector<Point3D>
{
    #region Implementations
    /// <summary>
    /// Represents a <see cref="Point3D" /> that has <see cref="X" />, <see cref="Y" />, and <see cref="Z" /> values set to zero.
    /// </summary>
    public static readonly Point3D Empty = new(0d, 0d, 0d);

    /// <summary>
    /// Represents a <see cref="Point3D" /> that has <see cref="X" />, <see cref="Y" />, and <see cref="Z" /> values set to 1.
    /// </summary>
    public static readonly Point3D Unit = new(1d, 1d, 1d);

    /// <summary>
    /// Represents a <see cref="Point3D" /> that has <see cref="X" />, <see cref="Y" />, and <see cref="Z" /> values set to NaN.
    /// </summary>
    public static readonly Point3D NaN = new(double.NaN, double.NaN, double.NaN);
    #endregion Implementations

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Point3D" /> class.
    /// </summary>
    /// <param name="point">The point.</param>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly void Deconstruct(out double x, out double y, out double z)
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
    [DataMember(Name = nameof(X)), XmlAttribute(nameof(X)), SoapAttribute(nameof(X))]
    public double X { get; set; }

    /// <summary>
    /// Gets or sets the Y component of a <see cref="Point3D" /> coordinate.
    /// </summary>
    /// <value>
    /// The y.
    /// </value>
    [DataMember(Name = nameof(Y)), XmlAttribute(nameof(Y)), SoapAttribute(nameof(Y))]
    public double Y { get; set; }

    /// <summary>
    /// Gets or sets the Z component of a <see cref="Point3D" /> coordinate.
    /// </summary>
    /// <value>
    /// The z.
    /// </value>
    [DataMember(Name = nameof(Z)), XmlAttribute(nameof(Z)), SoapAttribute(nameof(Z))]
    public double Z { get; set; }

    /// <summary>
    /// Gets a value indicating whether this <see cref="Point3D" /> is empty.
    /// </summary>
    /// <value>
    /// <see langword="true"/> if this instance is empty; otherwise, <see langword="false"/>.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(false)]
    public readonly bool IsEmpty => Abs(X) < double.Epsilon && Abs(Y) < double.Epsilon && Abs(Z) < double.Epsilon;

    /// <summary>
    /// Gets the number of components in the Vector.
    /// </summary>
    /// <value>
    /// The count.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public readonly int Count => 3;
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
    public static Point3D operator +(Point3D value) => Plus(value);

    /// <summary>
    /// Add an amount to both values in the <see cref="Point3D" /> classes.
    /// </summary>
    /// <param name="augend">The original value</param>
    /// <param name="addend">The amount to add.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point3D operator +(Point3D augend, double addend) => Add(augend, addend);

    /// <summary>
    /// Add an amount to both values in the <see cref="Point3D" /> classes.
    /// </summary>
    /// <param name="augend">The original value</param>
    /// <param name="addend">The amount to add.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point3D operator +(double augend, Point3D addend) => Add(addend, augend);

    /// <summary>
    /// Add two <see cref="Point3D" /> classes together.
    /// </summary>
    /// <param name="augend">The original value</param>
    /// <param name="addend">The amount to add.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector3D operator +(Point3D augend, Point3D addend) => Add(augend, addend);

    /// <summary>
    /// Operator Point + Vector
    /// </summary>
    /// <param name="augend">The original value</param>
    /// <param name="addend">The amount to add.</param>
    /// <returns>
    /// Point - The result of the addition
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point3D operator +(Point3D augend, Vector3D addend) => Add(augend, addend);

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
    public static Point3D operator +(Vector3D augend, Point3D addend) => Add(augend, addend);

    /// <summary>
    /// Unary subtraction operator.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point3D operator -(Point3D value) => Negate(value);

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
    public static Point3D operator -(Point3D minuend, double subend) => Subtract(minuend, subend);

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
    public static Point3D operator -(double minuend, Point3D subend) => Subtract(minuend, subend);

    /// <summary>
    /// Subtract a <see cref="Point3D" /> from another <see cref="Point3D" /> class.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subend">The subend.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector3D operator -(Point3D minuend, Point3D subend) => Subtract(minuend, subend);

    /// <summary>
    /// Subtract a <see cref="Point3D" /> from another <see cref="Point3D" /> class.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subend">The subend.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point3D operator -(Point3D minuend, Vector3D subend) => Subtract(minuend, subend);

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
    public static Point3D operator -(Vector3D minuend, Point3D subend) => Subtract(minuend, subend);

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
    public static Point3D operator *(Point3D multiplicand, double multiplier) => Multiply(multiplicand, multiplier);

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
    public static Point3D operator *(Matrix3x3D multiplicand, Point3D multiplier) => Multiply(multiplicand, multiplier);

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
    public static Point3D operator *(Point3D multiplicand, Matrix3x3D multiplier) => Multiply(multiplicand, multiplier);

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
    public static Point3D operator *(double multiplicand, Point3D multiplier) => Multiply(multiplier, multiplicand);

    /// <summary>
    /// Divide a <see cref="Point3D" /> by a value.
    /// </summary>
    /// <param name="divisor">The divisor value</param>
    /// <param name="dividend">The dividend to add.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point3D operator /(Point3D divisor, double dividend) => Divide(divisor, dividend);

    /// <summary>
    /// Divide a <see cref="Point3D" />
    /// </summary>
    /// <param name="divisor">The <see cref="Point3D" /></param>
    /// <param name="dividend">The divisor</param>
    /// <returns>
    /// A <see cref="Point3D" /> divided by the divisor
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point3D operator /(double divisor, Point3D dividend) => Divide(divisor, dividend);

    /// <summary>
    /// Compares two <see cref="Point3D" /> objects.
    /// The result specifies whether the values of the <see cref="X" /> and <see cref="Y" />
    /// values of the two <see cref="Point3D" /> objects are equal.
    /// </summary>
    /// <param name="comparand">The comparand.</param>
    /// <param name="comparanda">The comparanda.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool operator ==(Point3D comparand, Point3D comparanda) => Equals(comparand, comparanda);

    /// <summary>
    /// Compares two <see cref="Point3D" /> objects.
    /// The result specifies whether the values of the <see cref="X" /> or <see cref="Y" />
    /// values of the two <see cref="Point3D" /> objects are unequal.
    /// </summary>
    /// <param name="comparand">The comparand.</param>
    /// <param name="comparanda">The comparanda.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool operator !=(Point3D comparand, Point3D comparanda) => !Equals(comparand, comparanda);

    /// <summary>
    /// Explicit conversion of the specified <see cref="Vector3D" /> structure to a <see cref="Point3D" /> structure.
    /// </summary>
    /// <param name="vector">The <see cref="Vector3D" /> to be converted.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static explicit operator Point3D(Vector3D vector) => new(vector.I, vector.J, vector.K);

    /// <summary>
    /// Explicit conversion to Vector
    /// </summary>
    /// <param name="point">Point - the Point to convert to a Vector</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static explicit operator Vector3D(Point3D point) => new(point.X, point.Y, point.Z);

    /// <summary>
    /// Implicit conversion from tuple.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static implicit operator Point3D((double X, double Y, double Z) tuple) => new(tuple);

    /// <summary>
    /// Converts the specified <see cref="Point3D" /> structure to a <see cref="ValueTuple{T1, T2, T3}" /> structure.
    /// </summary>
    /// <param name="point">The <see cref="Point3D" /> to be converted.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static implicit operator (double X, double Y, double Z)(Point3D point) => (point.X, point.Y, point.Z);
    #endregion Operators

    #region Operator Backing Methods
    /// <summary>
    /// Pluses the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point3D Plus(Point3D value) => Operations.Plus(value.X, value.Y, value.Z);

    /// <summary>
    /// Adds the specified augend.
    /// </summary>
    /// <param name="augend">The augend.</param>
    /// <param name="addend">The addend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point3D Add(Point3D augend, double addend) => AddVectorUniform(augend.X, augend.Y, augend.Z, addend);

    /// <summary>
    /// Adds the specified augend.
    /// </summary>
    /// <param name="augend">The augend.</param>
    /// <param name="addend">The addend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point3D Add(double augend, Point3D addend) => AddVectorUniform(addend.X, addend.Y, addend.Z, augend);

    /// <summary>
    /// Adds the specified augend.
    /// </summary>
    /// <param name="augend">The augend.</param>
    /// <param name="addend">The addend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector3D Add(Point3D augend, Point3D addend) => AddVectors(augend.X, augend.Y, augend.Z, addend.X, addend.Y, addend.Z);

    /// <summary>
    /// Adds the specified augend.
    /// </summary>
    /// <param name="augend">The augend.</param>
    /// <param name="addend">The addend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point3D Add(Point3D augend, Vector3D addend) => AddVectors(augend.X, augend.Y, augend.Z, addend.I, addend.J, addend.K);

    /// <summary>
    /// Adds the specified augend.
    /// </summary>
    /// <param name="augend">The augend.</param>
    /// <param name="addend">The addend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point3D Add(Vector3D augend, Point3D addend) => AddVectors(augend.I, augend.J, augend.K, addend.X, addend.Y, addend.Z);

    /// <summary>
    /// Negates the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point3D Negate(Point3D value) => Operations.Negate(value.X, value.Y, value.Z);

    /// <summary>
    /// Subtracts the specified minuend.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subend">The subend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point3D Subtract(Point3D minuend, double subend) => SubtractVectorUniform(minuend.X, minuend.Y, minuend.Z, subend);

    /// <summary>
    /// Subtracts the specified minuend.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subend">The subend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point3D Subtract(double minuend, Point3D subend) => SubtractFromMinuend(minuend, subend.X, subend.Y, subend.Z);

    /// <summary>
    /// Subtracts the specified minuend.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subend">The subend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector3D Subtract(Point3D minuend, Point3D subend) => SubtractVector(minuend.X, minuend.Y, minuend.Z, subend.X, subend.Y, subend.Z);

    /// <summary>
    /// Subtracts the specified minuend.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subend">The subend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point3D Subtract(Point3D minuend, Vector3D subend) => SubtractVector(minuend.X, minuend.Y, minuend.Z, subend.I, subend.J, subend.K);

    /// <summary>
    /// Subtracts the specified minuend.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subend">The subend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point3D Subtract(Vector3D minuend, Point3D subend) => SubtractVector(minuend.I, minuend.J, minuend.K, subend.X, subend.Y, subend.Z);

    /// <summary>
    /// Multiplies the specified multiplicand.
    /// </summary>
    /// <param name="multiplicand">The multiplicand.</param>
    /// <param name="multiplier">The multiplier.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point3D Multiply(Point3D multiplicand, double multiplier) => ScaleVector(multiplicand.X, multiplicand.Y, multiplicand.Z, multiplier);

    /// <summary>
    /// Multiplies the specified multiplicand.
    /// </summary>
    /// <param name="multiplicand">The multiplicand.</param>
    /// <param name="multiplier">The multiplier.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point3D Multiply(double multiplicand, Point3D multiplier) => ScaleVector(multiplier.X, multiplier.Y, multiplier.Z, multiplicand);

    /// <summary>
    /// Multiplies the specified multiplicand.
    /// </summary>
    /// <param name="multiplicand">The multiplicand.</param>
    /// <param name="multiplier">The multiplier.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point3D Multiply(Matrix3x3D multiplicand, Point3D multiplier) => MultiplyMatrix3x3ByVerticalVector3D(multiplicand.M0x0, multiplicand.M0x1, multiplicand.M0x2, multiplicand.M1x0, multiplicand.M1x1, multiplicand.M1x2, multiplicand.M2x0, multiplicand.M2x1, multiplicand.M2x2, multiplier.X, multiplier.Y, multiplier.Z);

    /// <summary>
    /// Multiplies the specified multiplicand.
    /// </summary>
    /// <param name="multiplicand">The multiplicand.</param>
    /// <param name="multiplier">The multiplier.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point3D Multiply(Point3D multiplicand, Matrix3x3D multiplier) => MultiplyHorizontalVector3DByMatrix3x3(multiplicand.X, multiplicand.Y, multiplicand.Z, multiplier.M0x0, multiplier.M0x1, multiplier.M0x2, multiplier.M1x0, multiplier.M1x1, multiplier.M1x2, multiplier.M2x0, multiplier.M2x1, multiplier.M2x2);

    /// <summary>
    /// Divides the specified divisor.
    /// </summary>
    /// <param name="divisor">The divisor.</param>
    /// <param name="dividend">The dividend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point3D Divide(Point3D divisor, double dividend) => DivideVectorUniform(divisor.X, divisor.Y, divisor.Z, dividend);

    /// <summary>
    /// Divides the specified divisor.
    /// </summary>
    /// <param name="divisor">The divisor.</param>
    /// <param name="dividend">The dividend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point3D Divide(double divisor, Point3D dividend) => DivideByVectorUniform(divisor, dividend.X, dividend.Y, dividend.Z);

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true" /> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Equals([AllowNull] object obj) => obj is Point3D d && Equals(d);

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="other">The other.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly bool Equals([AllowNull] Point3D other) => X == other.X && Y == other.Y && Z == other.Z;

    /// <summary>
    /// Converts to point3d.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point3D FromVector3D(Vector3D vector) => new(vector.I, vector.J, vector.K);

    /// <summary>
    /// Converts to vector3d.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly Vector3D ToVector3D() => new(X, Y, Z);

    /// <summary>
    /// Creates a new <see cref="Point3D" /> from a <see cref="ValueTuple{T1, T2, T3}" />.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point3D FromValueTuple((double X, double Y, double Z) tuple) => new(tuple);

    /// <summary>
    /// Converts to valuetuple.
    /// </summary>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly (double X, double Y, double Z) ToValueTuple() => (X, Y, Z);
    #endregion

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
    public static Point3D Parse(string source) => Parse(source, CultureInfo.InvariantCulture);

    /// <summary>
    /// Parse a string for a <see cref="Point3D" /> value.
    /// </summary>
    /// <param name="source"><see cref="string" /> with <see cref="Point3D" /> data</param>
    /// <param name="formatProvider">The provider.</param>
    /// <returns>
    /// Returns an instance of the <see cref="Point3D" /> struct converted
    /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
    /// </returns>
    public static Point3D Parse(string source, IFormatProvider formatProvider)
    {
        var tokenizer = new Tokenizer(source, formatProvider);
        var firstToken = tokenizer.NextTokenRequired();

        var value = new Point3D(
            Convert.ToDouble(firstToken, formatProvider),
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
    /// Creates a string representation of this <see cref="Point3D" /> struct based on the IFormatProvider
    /// passed in.  If the provider is null, the CurrentCulture is used.
    /// </summary>
    /// <param name="formatProvider">The <see cref="CultureInfo" /> provider.</param>
    /// <returns>
    /// A string representation of this <see cref="Point3D" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public string ToString(IFormatProvider formatProvider) => ToString("R" /* format string */, formatProvider);

    /// <summary>
    /// Get the hash code.
    /// </summary>
    /// <returns>
    /// The <see cref="int" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override readonly int GetHashCode() => HashCode.Combine(X, Y, Z);

    /// <summary>
    /// Creates a human-readable string that represents this <see cref="Point3D" /> struct.
    /// </summary>
    /// <returns>
    /// A string representation of this <see cref="Point3D" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

    /// <summary>
    /// Creates a string representation of this <see cref="Point3D" /> struct based on the format string
    /// and IFormatProvider passed in.
    /// If the provider is null, the CurrentCulture is used.
    /// See the documentation for IFormattable for more information.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="formatProvider">The format provider.</param>
    /// <returns>
    /// A string representation of this <see cref="Point3D" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly string ToString(string format, IFormatProvider formatProvider)
    {
        if (this == null) return nameof(Point3D);
        var s = Tokenizer.GetNumericListSeparator(formatProvider);
        return $"{nameof(Point3D)}({nameof(X)}: {X.ToString(format, formatProvider)}{s} {nameof(Y)}: {Y.ToString(format, formatProvider)}{s} {nameof(Z)}: {Z.ToString(format, formatProvider)})";
    }

    /// <summary>
    /// Gets the debugger display.
    /// </summary>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private string GetDebuggerDisplay() => ToString();
    #endregion Methods
}
