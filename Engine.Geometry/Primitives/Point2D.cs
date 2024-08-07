﻿// <copyright file="Point2D.cs" company="Shkyrockett" >
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
/// The <see cref="Point2D" /> struct.
/// </summary>
/// <seealso cref="IShapeSegment" />
/// <seealso cref="IVector{T}" />
/// <seealso cref="IVector{T}" />
[DataContract, Serializable]
[TypeConverter(typeof(Point2DConverter))]
[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class Point2D
    : IShapeSegment, IVector<Point2D>
{
    #region Implementations
    /// <summary>
    /// Represents a <see cref="Point2D" /> that has <see cref="X" />, and <see cref="Y" /> values set to zero.
    /// </summary>
    public static readonly Point2D Empty = new(0d, 0d);

    /// <summary>
    /// Represents a <see cref="Point2D" /> that has <see cref="X" />, and <see cref="Y" /> values set to 1.
    /// </summary>
    public static readonly Point2D Unit = new(1d, 1d);

    /// <summary>
    /// Represents a <see cref="Point2D" /> that has <see cref="X" />, and <see cref="Y" /> values set to NaN.
    /// </summary>
    public static readonly Point2D NaN = new(double.NaN, double.NaN);
    #endregion Implementations

    #region Constructors
    /// <summary>
    /// Initializes a new  instance of the <see cref="Point2D" /> class.
    /// </summary>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Point2D()
        : this(0d, 0d)
    { }

    ///// <summary>
    ///// Initializes a new  instance of the <see cref="Point2D" /> class.
    ///// </summary>
    ///// <param name="point">The Point2D.</param>
    //[DebuggerStepThrough]
    //[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    //public Point2D(Point2D point)
    //    : this(point.X, point.Y)
    //{ }

    /// <summary>
    /// Initializes a new instance of the <see cref="Point2D" /> class.
    /// </summary>
    /// <param name="x">The x component of the Point2D.</param>
    /// <param name="y">The y component of the Point2D.</param>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Point2D(double x, double y)
    //: this()
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Point2D" /> class.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Point2D((double X, double Y) tuple)
    //: this()
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
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
    [DataMember(Name = nameof(X)), XmlAttribute(nameof(X)), SoapAttribute(nameof(X))]
    public double X { get; set; }

    /// <summary>
    /// Gets or sets the Y component of a <see cref="Point2D" /> coordinate.
    /// </summary>
    /// <value>
    /// The y.
    /// </value>
    [DataMember(Name = nameof(Y)), XmlAttribute(nameof(Y)), SoapAttribute(nameof(Y))]
    public double Y { get; set; }

    /// <summary>
    /// Gets a value indicating whether this <see cref="Point2D" /> is empty.
    /// </summary>
    /// <value>
    ///   <see langword="true" /> if this instance is empty; otherwise, <see langword="false" />.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(false)]
    public bool IsEmpty => Abs(X) < double.Epsilon && Abs(Y) < double.Epsilon;

    /// <summary>
    /// Gets the number of components in the Vector.
    /// </summary>
    /// <value>
    /// The count.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public int Count => 2;

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="IShapeSegment" /> position should be calculated relative to the last item, or from Origin.
    /// </summary>
    /// <value>
    ///   <see langword="true" /> if relative; otherwise, <see langword="false" />.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public bool Relative { get; set; }

    /// <summary>
    /// Gets or sets a reference to the segment after this segment.
    /// </summary>
    /// <value>
    /// The before.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public IShapeSegment Before { get; set; }

    /// <summary>
    /// Gets or sets a reference to the segment before this segment.
    /// </summary>
    /// <value>
    /// The after.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public IShapeSegment After { get; set; }

    /// <summary>
    /// Gets or sets the head point.
    /// </summary>
    /// <value>
    /// The head.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public Point2D Head { get; set; }

    /// <summary>
    /// Gets or sets the next to first point from the head point.
    /// </summary>
    /// <value>
    /// The next to head point.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public Point2D NextToHead { get; set; }

    /// <summary>
    /// Gets or sets the next to last point to the tail point.
    /// </summary>
    /// <value>
    /// The next to tail point.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public Point2D NextToTail { get; set; }

    /// <summary>
    /// Gets or sets the tail point.
    /// </summary>
    /// <value>
    /// The tail.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public Point2D Tail { get; set; }
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
    public static Point2D operator +(Point2D value) => Plus(value);

    /// <summary>
    /// Add an amount to both values in the <see cref="Point2D" /> classes.
    /// </summary>
    /// <param name="augend">The original value</param>
    /// <param name="addend">The amount to add.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D operator +(Point2D augend, double addend) => Add(augend, addend);

    /// <summary>
    /// Add an amount to both values in the <see cref="Point2D" /> classes.
    /// </summary>
    /// <param name="augend">The original value</param>
    /// <param name="addend">The amount to add.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D operator +(double augend, Point2D addend) => Add(addend, augend);

    /// <summary>
    /// Add two <see cref="Point2D" /> classes together.
    /// </summary>
    /// <param name="augend">The value.</param>
    /// <param name="addend">The addend.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector2D operator +(Point2D augend, Point2D addend) => Add(augend, addend);

    /// <summary>
    /// Operator Point + Vector
    /// </summary>
    /// <param name="augend">The Point to be added to the Vector</param>
    /// <param name="addend">The Vector to be added to the Point</param>
    /// <returns>
    /// Point - The result of the addition
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D operator +(Point2D augend, Vector2D addend) => Add(augend, addend);

    /// <summary>
    /// Add Points
    /// </summary>
    /// <param name="augend">The value.</param>
    /// <param name="addend">The addend.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D operator +(Vector2D augend, Point2D addend) => Add(augend, addend);

    /// <summary>
    /// Unary subtraction operator.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D operator -(Point2D value) => Negate(value);

    /// <summary>
    /// Subtract a <see cref="Point2D" /> from a <see cref="double" /> value.
    /// </summary>
    /// <param name="minuend">The value.</param>
    /// <param name="subend">The subend.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D operator -(Point2D minuend, double subend) => Subtract(minuend, subend);

    /// <summary>
    /// Subtract a <see cref="Point2D" /> from a <see cref="double" /> value.
    /// </summary>
    /// <param name="minuend">The value.</param>
    /// <param name="subend">The subend.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D operator -(double minuend, Point2D subend) => Subtract(minuend, subend);

    /// <summary>
    /// Subtract a <see cref="Point2D" /> from another <see cref="Point2D" /> class.
    /// </summary>
    /// <param name="minuend">The value.</param>
    /// <param name="subend">The subend.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector2D operator -(Point2D minuend, Point2D subend) => Subtract(minuend, subend);

    /// <summary>
    /// Subtract a <see cref="Point2D" /> from another <see cref="Point2D" /> class.
    /// </summary>
    /// <param name="minuend">The value.</param>
    /// <param name="subend">The subend.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D operator -(Point2D minuend, Vector2D subend) => Subtract(minuend, subend);

    /// <summary>
    /// Subtract Points
    /// </summary>
    /// <param name="minuend">The value.</param>
    /// <param name="subend">The subend.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D operator -(Vector2D minuend, Point2D subend) => Subtract(minuend, subend);

    /// <summary>
    /// Scale a point.
    /// </summary>
    /// <param name="multiplicand">The value.</param>
    /// <param name="multiplier">The factor.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D operator *(Point2D multiplicand, double multiplier) => Multiply(multiplicand, multiplier);

    /// <summary>
    /// Scale a point
    /// </summary>
    /// <param name="multiplicand">The value.</param>
    /// <param name="multiplier">The factor.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D operator *(double multiplicand, Point2D multiplier) => Multiply(multiplier, multiplicand);

    /// <summary>
    /// Divide a <see cref="Point2D" /> by a value.
    /// </summary>
    /// <param name="dividend">The divisor value</param>
    /// <param name="divisor">The dividend to add.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D operator /(Point2D dividend, double divisor) => Divide(dividend, divisor);

    /// <summary>
    /// Divide a Point2D
    /// </summary>
    /// <param name="dividend">The <see cref="Point2D" /></param>
    /// <param name="divisor">The divisor</param>
    /// <returns>
    /// A Point2D divided by the divisor
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D operator /(double dividend, Point2D divisor) => Divide(dividend, divisor);

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
    public static Point2D operator *(Matrix2x2D multiplicand, Point2D multiplier) => Multiply(multiplicand, multiplier);

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
    public static Point2D operator *(Point2D multiplicand, Matrix2x2D multiplier) => Multiply(multiplier, multiplicand);

    /// <summary>
    /// Compares two <see cref="Point2D" /> objects.
    /// The result specifies whether the values of the <see cref="X" />, and <see cref="Y" />
    /// values of the two <see cref="Point2D" /> objects are equal.
    /// </summary>
    /// <param name="comparand">The left.</param>
    /// <param name="comparanda">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool operator ==(Point2D comparand, Point2D comparanda) => Equals(comparand, comparanda);

    /// <summary>
    /// Compares two <see cref="Point2D" /> objects.
    /// The result specifies whether the values of the <see cref="X" /> or <see cref="Y" />
    /// values of the two <see cref="Point2D" /> objects are unequal.
    /// </summary>
    /// <param name="comparand">The left.</param>
    /// <param name="comparanda">The right.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool operator !=(Point2D comparand, Point2D comparanda) => !Equals(comparand, comparanda);

    /// <summary>
    /// Explicit conversion of the specified <see cref="Vector2D" /> structure to a <see cref="Point2D" /> structure.
    /// </summary>
    /// <param name="vector">The <see cref="Vector2D" /> to be converted.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static explicit operator Point2D(Vector2D vector) => new(vector.I, vector.J);

    /// <summary>
    /// Explicit conversion to Vector
    /// </summary>
    /// <param name="point">Point - the Point to convert to a Vector</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static explicit operator Vector2D(Point2D point) => new(point.X, point.Y);

    /// <summary>
    /// Implicit conversion from tuple.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static implicit operator Point2D((double X, double Y) tuple) => new(tuple);

    /// <summary>
    /// Converts the specified <see cref="Point2D" /> structure to a <see cref="ValueTuple{T1, T2}" /> structure.
    /// </summary>
    /// <param name="point">The <see cref="Point2D" /> to be converted.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static implicit operator (double X, double Y)(Point2D point) => (point.X, point.Y);
    #endregion Operators

    #region Operator Backing Methods
    /// <summary>
    /// Pluses the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D Plus(Point2D value) => Operations.Plus(value.X, value.Y);

    /// <summary>
    /// Adds the specified augend.
    /// </summary>
    /// <param name="augend">The augend.</param>
    /// <param name="addend">The addend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D Add(Point2D augend, double addend) => AddVectorUniform(augend.X, augend.Y, addend);

    /// <summary>
    /// Adds the specified augend.
    /// </summary>
    /// <param name="augend">The augend.</param>
    /// <param name="addend">The addend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D Add(double augend, Point2D addend) => AddVectorUniform(addend.X, addend.Y, augend);

    /// <summary>
    /// Adds the specified augend.
    /// </summary>
    /// <param name="augend">The augend.</param>
    /// <param name="addend">The addend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector2D Add(Point2D augend, Point2D addend) => AddVectors(augend.X, augend.Y, addend.X, addend.Y);

    /// <summary>
    /// Adds the specified augend.
    /// </summary>
    /// <param name="augend">The augend.</param>
    /// <param name="addend">The addend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D Add(Point2D augend, Vector2D addend) => AddVectors(augend.X, augend.Y, addend.I, addend.J);

    /// <summary>
    /// Adds the specified augend.
    /// </summary>
    /// <param name="augend">The augend.</param>
    /// <param name="addend">The addend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D Add(Vector2D augend, Point2D addend) => AddVectors(augend.I, augend.J, addend.X, addend.Y);

    /// <summary>
    /// Negates the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D Negate(Point2D value) => Operations.Negate(value.X, value.Y);

    /// <summary>
    /// Subtracts the specified minuend.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subend">The subend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D Subtract(Point2D minuend, double subend) => SubtractVectorUniform(minuend.X, minuend.Y, subend);

    /// <summary>
    /// Subtracts the specified minuend.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subend">The subend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D Subtract(double minuend, Point2D subend) => SubtractFromMinuend(minuend, subend.X, subend.Y);

    /// <summary>
    /// Subtracts the specified minuend.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subend">The subend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector2D Subtract(Point2D minuend, Point2D subend) => SubtractVector(minuend.X, minuend.Y, subend.X, subend.Y);

    /// <summary>
    /// Subtracts the specified minuend.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subend">The subend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D Subtract(Point2D minuend, Vector2D subend) => SubtractVector(minuend.X, minuend.Y, subend.I, subend.J);

    /// <summary>
    /// Subtracts the specified minuend.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subend">The subend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D Subtract(Vector2D minuend, Point2D subend) => SubtractVector(minuend.I, minuend.J, subend.X, subend.Y);

    /// <summary>
    /// Multiplies the specified multiplicand.
    /// </summary>
    /// <param name="multiplicand">The multiplicand.</param>
    /// <param name="multiplier">The multiplier.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D Multiply(Point2D multiplicand, double multiplier) => ScaleVector(multiplicand.X, multiplicand.Y, multiplier);

    /// <summary>
    /// Multiplies the specified multiplicand.
    /// </summary>
    /// <param name="multiplicand">The multiplicand.</param>
    /// <param name="multiplier">The multiplier.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D Multiply(double multiplicand, Point2D multiplier) => ScaleVector(multiplier.X, multiplier.Y, multiplicand);

    /// <summary>
    /// Multiplies the specified multiplicand.
    /// </summary>
    /// <param name="multiplicand">The multiplicand.</param>
    /// <param name="multiplier">The multiplier.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D Multiply(Matrix2x2D multiplicand, Point2D multiplier) => MultiplyMatrix2x2ByVerticalVector2D(multiplicand.M0x0, multiplicand.M0x1, multiplicand.M1x0, multiplicand.M1x1, multiplier.X, multiplier.Y);

    /// <summary>
    /// Multiplies the specified multiplicand.
    /// </summary>
    /// <param name="multiplicand">The multiplicand.</param>
    /// <param name="multiplier">The multiplier.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D Multiply(Point2D multiplicand, Matrix2x2D multiplier) => MultiplyHorizontalVector2DByMatrix2x2(multiplicand.X, multiplicand.Y, multiplier.M0x0, multiplier.M0x1, multiplier.M1x0, multiplier.M1x1);

    /// <summary>
    /// Divides the specified dividend.
    /// </summary>
    /// <param name="dividend">The dividend.</param>
    /// <param name="divisor">The divisor.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D Divide(Point2D dividend, double divisor) => DivideVectorUniform(dividend.X, dividend.Y, divisor);

    /// <summary>
    /// Divides the specified dividend.
    /// </summary>
    /// <param name="dividend">The dividend.</param>
    /// <param name="divisor">The divisor.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D Divide(double dividend, Point2D divisor) => DivideByVectorUniform(dividend, divisor.X, divisor.Y);

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true" /> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Equals([AllowNull] object obj) => obj is Point2D d && Equals(d);

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="other">The other.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Equals([AllowNull] Point2D other) => X == other.X && Y == other.Y;

    /// <summary>
    /// Converts to point2d.
    /// </summary>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Point2D ToPoint2D() => new(X, Y);

    /// <summary>
    /// Froms the vector2 d.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D FromVector2D(Vector2D vector) => new(vector.I, vector.J);

    /// <summary>
    /// Converts to vector2d.
    /// </summary>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Vector2D ToVector2D() => new(X, Y);

    /// <summary>
    /// Creates a new <see cref="Point2D" /> from a <see cref="ValueTuple{T1, T2}" />.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Point2D FromValueTuple((double X, double Y) tuple) => new(tuple);

    /// <summary>
    /// Converts to valuetuple.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public (double X, double Y) ToValueTuple() => (X, Y);
    #endregion

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
    public static Point2D Parse(string source) => Parse(source, CultureInfo.InvariantCulture);

    /// <summary>
    /// Parse a string for a <see cref="Point2D" /> value.
    /// </summary>
    /// <param name="source"><see cref="string" /> with <see cref="Point2D" /> data</param>
    /// <param name="formatProvider">The provider.</param>
    /// <returns>
    /// Returns an instance of the <see cref="Point2D" /> struct converted
    /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
    /// </returns>
    public static Point2D Parse(string source, IFormatProvider formatProvider)
    {
        var tokenizer = new Tokenizer(source, formatProvider);
        var firstToken = tokenizer.NextTokenRequired();

        var value = new Point2D(
            Convert.ToDouble(firstToken, formatProvider),
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
    public override int GetHashCode() => HashCode.Combine(X, Y);

    /// <summary>
    /// Creates a human-readable string that represents this <see cref="Point2D" /> struct.
    /// </summary>
    /// <returns>
    /// A string representation of this <see cref="Point2D" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

    /// <summary>
    /// Creates a string representation of this <see cref="Point2D" /> struct based on the IFormatProvider
    /// passed in.  If the provider is null, the CurrentCulture is used.
    /// </summary>
    /// <param name="formatProvider">The <see cref="CultureInfo" /> provider.</param>
    /// <returns>
    /// A string representation of this <see cref="Point2D" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public string ToString(IFormatProvider formatProvider) => ToString("R" /* format string */, formatProvider);

    /// <summary>
    /// Creates a string representation of this <see cref="Point2D" /> struct based on the format string
    /// and IFormatProvider passed in.
    /// If the provider is null, the CurrentCulture is used.
    /// See the documentation for IFormattable for more information.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="formatProvider">The format provider.</param>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public string ToString(string format, IFormatProvider formatProvider)
    {
        if (this == null) return nameof(Point2D);
        var s = Tokenizer.GetNumericListSeparator(formatProvider);
        return $"{nameof(Point2D)}({nameof(X)}: {X.ToString(format, formatProvider)}{s} {nameof(Y)}: {Y.ToString(format, formatProvider)})";
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
