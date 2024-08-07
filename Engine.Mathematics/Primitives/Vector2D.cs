﻿// <copyright file="Vector2D.cs" company="Shkyrockett" >
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
/// The Vector2D struct. Represents a vector in 2D coordinate space (double precision floating-point coordinates).
/// </summary>
/// <seealso cref="Engine.IVector{T}" />
[DataContract, Serializable]
[TypeConverter(typeof(Vector2DConverter))]
[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public struct Vector2D
    : IVector<Vector2D>
{
    #region Static Fields
    /// <summary>
    /// Represents a <see cref="Vector2D" /> that has <see cref="I" />, and <see cref="J" /> values set to zero.
    /// </summary>
    public static readonly Vector2D Empty = new(0d, 0d);

    /// <summary>
    /// Represents a <see cref="Vector2D" /> that has <see cref="I" />, and <see cref="J" /> values set to 1.
    /// </summary>
    public static readonly Vector2D Unit = new(1d, 1d);

    /// <summary>
    /// Represents a <see cref="Vector2D" /> that has <see cref="I" />, and <see cref="J" /> values set to NaN.
    /// </summary>
    public static readonly Vector2D NaN = new(double.NaN, double.NaN);

    /// <summary>
    /// Represents a <see cref="Vector2D" /> that has <see cref="I" /> to 1, and <see cref="J" /> set to 0.
    /// </summary>
    public static readonly Vector2D XAxis = new(1d, 0d);

    /// <summary>
    /// Represents a <see cref="Vector2D" /> that has <see cref="I" /> to 0, and <see cref="J" /> set to 1.
    /// </summary>
    public static readonly Vector2D YAxis = new(0d, 1d);
    #endregion Static Fields

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Vector2D" /> struct.
    /// </summary>
    /// <param name="vector2D">A <see cref="Vector2D" /> class to clone.</param>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Vector2D(Vector2D vector2D)
        : this(vector2D.I, vector2D.J)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Vector2D" /> struct.
    /// </summary>
    /// <param name="i">The <see cref="I" /> component of the <see cref="Vector2D" /> class.</param>
    /// <param name="j">The <see cref="J" /> component of the <see cref="Vector2D" /> class.</param>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Vector2D(double i, double j)
        : this()
    {
        (I, J) = (i, j);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Vector2D" /> struct.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Vector2D((double X, double Y) tuple)
        : this()
    {
        (I, J) = tuple;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Vector2D" /> struct.
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Vector2D((double X, double Y) a, (double X, double Y) b)
        : this(a.X, a.Y, b.X, b.Y)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Vector2D" /> struct.
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Vector2D(Vector2D a, Vector2D b) :
        this(a.I, a.J, b.I, b.J)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Vector2D" /> struct.
    /// </summary>
    /// <param name="aI">The aI.</param>
    /// <param name="aJ">The aJ.</param>
    /// <param name="bI">The bI.</param>
    /// <param name="bJ">The bJ.</param>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Vector2D(double aI, double aJ, double bI, double bJ)
        : this()
    {
        // This creates a normalized vector. It is debatable that it is what we actually want. We may only want the first line.

        // Find the new vector.
        (var i, var j) = (bI - aI, bJ - aJ);

        // Get the length of the vector.
        var d = Sqrt((i * i) + (j * j));

        // Calculate the normalized vector.
        (I, J) = d == 0 ? (i, j) : (i * 1d / d, j * 1d / d);
    }
    #endregion Constructors

    #region Deconstructors
    /// <summary>
    /// Deconstruct this <see cref="Vector2D" /> to a <see cref="ValueTuple{T1, T2}" />.
    /// </summary>
    /// <param name="i">The i.</param>
    /// <param name="j">The j.</param>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public readonly void Deconstruct(out double i, out double j)
    {
        i = I;
        j = J;
    }
    #endregion Deconstructors

    #region Properties
    /// <summary>
    /// Gets or sets the I or first component of a 2D Vector.
    /// </summary>
    /// <value>
    /// The i.
    /// </value>
    [DataMember(Name = nameof(I)), XmlAttribute(nameof(I)), SoapAttribute(nameof(I))]
    public double I { get; set; }

    /// <summary>
    /// Gets or sets the j or second Component of a 2D Vector.
    /// </summary>
    /// <value>
    /// The j.
    /// </value>
    [DataMember(Name = nameof(J)), XmlAttribute(nameof(J)), SoapAttribute(nameof(J))]
    public double J { get; set; }

    /// <summary>
    /// Gets a value indicating whether this <see cref="Vector2D" /> is empty.
    /// </summary>
    /// <value>
    ///   <see langword="true"/> if this instance is empty; otherwise, <see langword="false"/>.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(false)]
    public readonly bool IsEmpty => IsEmptyVector(I, J);

    /// <summary>
    /// Gets the number of components in the Vector.
    /// </summary>
    /// <value>
    /// The count.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    public readonly int Count => 2;

    /// <summary>
    /// Gets the magnitude.
    /// </summary>
    /// <value>
    /// The magnitude.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(false)]
    public readonly double Magnitude => VectorMagnitude(I, J);

    /// <summary>
    /// Length Property - the length of this Vector
    /// </summary>
    /// <value>
    /// The length.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(false)]
    public readonly double Length => VectorMagnitude(I, J);

    /// <summary>
    /// LengthSquared Property - the squared length of this Vector
    /// </summary>
    /// <value>
    /// The length squared.
    /// </value>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(false)]
    public readonly double LengthSquared => VectorMagnitudeSquared(I, J);

    /// <summary>
    /// This returns the Normalized Vector2D that is passed. This is also known as a Unit Vector.
    /// </summary>
    /// <value>
    /// The normalized.
    /// </value>
    /// <remarks>
    ///   <para><seealso href="http://en.wikipedia.org/wiki/Vector_%28spatial%29#Unit_vector" /></para>
    /// </remarks>
    [IgnoreDataMember, XmlIgnore, SoapIgnore]
    [Browsable(false)]
    public readonly Vector2D Normalized => Normalize(I, J);
    #endregion Properties

    #region Operators
    /// <summary>
    /// The operator +.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The <see cref="Vector2D" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector2D operator +(Vector2D value) => Plus(value);

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
    public static Vector2D operator +(Vector2D augend, double addend) => Add(augend, addend);

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
    public static Vector2D operator +(double augend, Vector2D addend) => Add(augend, addend);

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
    public static Vector2D operator +(Vector2D augend, Vector2D addend) => Add(augend, addend);

    /// <summary>
    /// The operator -.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The <see cref="Vector2D" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector2D operator -(Vector2D value) => Negate(value);

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
    public static Vector2D operator -(Vector2D minuend, double subend) => Subtract(minuend, subend);

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
    public static Vector2D operator -(double minuend, Vector2D subend) => Subtract(minuend, subend);

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
    public static Vector2D operator -(Vector2D minuend, Vector2D subend) => Subtract(minuend, subend);

    /// <summary>
    /// Scale a Vector
    /// </summary>
    /// <param name="multiplicand">The Point</param>
    /// <param name="multiplier">The Multiplier</param>
    /// <returns>
    /// A Point Multiplied by the Multiplier
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector2D operator *(Vector2D multiplicand, double multiplier) => Multiply(multiplicand, multiplier);

    /// <summary>
    /// Scale a Vector
    /// </summary>
    /// <param name="multiplicand">The Multiplier</param>
    /// <param name="multiplier">The Point</param>
    /// <returns>
    /// A Point Multiplied by the Multiplier
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector2D operator *(double multiplicand, Vector2D multiplier) => Multiply(multiplicand, multiplier);

    /// <summary>
    /// Divide a Vector2D
    /// </summary>
    /// <param name="dividend">The Vector2D</param>
    /// <param name="divisor">The dividend</param>
    /// <returns>
    /// A Vector2D divided by the divisor
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector2D operator /(Vector2D dividend, double divisor) => Divide(dividend, divisor);

    /// <summary>
    /// Divide a Vector2D
    /// </summary>
    /// <param name="dividend">The Vector2D</param>
    /// <param name="divisor">The divisor</param>
    /// <returns>
    /// A Vector2D divided by the divisor
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector2D operator /(double dividend, Vector2D divisor) => Divide(dividend, divisor);

    /// <summary>
    /// The operator ==.
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool operator ==(Vector2D a, Vector2D b) => Equals(a, b);

    /// <summary>
    /// The operator !=.
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool operator !=(Vector2D a, Vector2D b) => !Equals(a, b);

    /// <summary>
    /// Converts the specified <see cref="Vector2D" /> structure to a <see cref="ValueTuple{T1, T2}" /> structure.
    /// </summary>
    /// <param name="vector">The <see cref="Vector2D" /> to be converted.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static implicit operator (double I, double J)(Vector2D vector) => (vector.I, vector.J);

    /// <summary>
    /// Tuple to <see cref="Vector2D" />.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static implicit operator Vector2D((double X, double Y) tuple) => new(tuple);
    #endregion Operators

    #region Operator Backing Methods
    /// <summary>
    /// Pluses this instance.
    /// </summary>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly Vector2D Plus() => Operations.Plus(I, J);

    /// <summary>
    /// Pluses the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector2D Plus(Vector2D value) => Operations.Plus(value.I, value.J);

    /// <summary>
    /// Adds the specified addend.
    /// </summary>
    /// <param name="addend">The addend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly Vector2D Add(double addend) => AddVectorUniform(I, J, addend);

    /// <summary>
    /// Adds the specified augend.
    /// </summary>
    /// <param name="augend">The augend.</param>
    /// <param name="addend">The addend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector2D Add(Vector2D augend, double addend) => AddVectorUniform(augend.I, augend.J, addend);

    /// <summary>
    /// Adds the specified augend.
    /// </summary>
    /// <param name="augend">The augend.</param>
    /// <param name="addend">The addend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector2D Add(double augend, Vector2D addend) => AddVectorUniform(addend.I, addend.J, augend);

    /// <summary>
    /// Adds the specified addend.
    /// </summary>
    /// <param name="addend">The addend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly Vector2D Add(Vector2D addend) => AddVectors(I, J, addend.I, addend.J);

    /// <summary>
    /// Adds the specified augend.
    /// </summary>
    /// <param name="augend">The augend.</param>
    /// <param name="addend">The addend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector2D Add(Vector2D augend, Vector2D addend) => AddVectors(augend.I, augend.J, addend.I, addend.J);

    /// <summary>
    /// Negates this instance.
    /// </summary>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly Vector2D Negate() => Operations.Negate(I, J);

    /// <summary>
    /// Negates the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector2D Negate(Vector2D value) => Operations.Negate(value.I, value.J);

    /// <summary>
    /// Subtracts the specified subend.
    /// </summary>
    /// <param name="subend">The subend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly Vector2D Subtract(double subend) => SubtractVectorUniform(I, J, subend);

    /// <summary>
    /// Subtracts the specified minuend.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subend">The subend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector2D Subtract(Vector2D minuend, double subend) => SubtractVectorUniform(minuend.I, minuend.J, subend);

    /// <summary>
    /// Subtracts the specified minuend.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subend">The subend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector2D Subtract(double minuend, Vector2D subend) => SubtractFromMinuend(minuend, subend.I, subend.J);

    /// <summary>
    /// Subtracts the specified subend.
    /// </summary>
    /// <param name="subend">The subend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly Vector2D Subtract(Vector2D subend) => SubtractVector(I, J, subend.I, subend.J);

    /// <summary>
    /// Subtracts the specified minuend.
    /// </summary>
    /// <param name="minuend">The minuend.</param>
    /// <param name="subend">The subend.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector2D Subtract(Vector2D minuend, Vector2D subend) => SubtractVector(minuend.I, minuend.J, subend.I, subend.J);

    /// <summary>
    /// Multiplies the specified multiplier.
    /// </summary>
    /// <param name="multiplier">The multiplier.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly Vector2D Multiply(double multiplier) => ScaleVector(I, J, multiplier);

    /// <summary>
    /// Multiplies the specified multiplicand.
    /// </summary>
    /// <param name="multiplicand">The multiplicand.</param>
    /// <param name="multiplier">The multiplier.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector2D Multiply(Vector2D multiplicand, double multiplier) => ScaleVector(multiplicand.I, multiplicand.J, multiplier);

    /// <summary>
    /// Multiplies the specified multiplicand.
    /// </summary>
    /// <param name="multiplicand">The multiplicand.</param>
    /// <param name="multiplier">The multiplier.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector2D Multiply(double multiplicand, Vector2D multiplier) => ScaleVector(multiplier.I, multiplier.J, multiplicand);

    /// <summary>
    /// Divides the specified divisor.
    /// </summary>
    /// <param name="divisor">The divisor.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly Vector2D Divide(double divisor) => DivideVectorUniform(I, J, divisor);

    /// <summary>
    /// Divides the specified dividend.
    /// </summary>
    /// <param name="dividend">The dividend.</param>
    /// <param name="divisor">The divisor.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector2D Divide(Vector2D dividend, double divisor) => DivideVectorUniform(dividend.I, dividend.J, divisor);

    /// <summary>
    /// Divides the specified dividend.
    /// </summary>
    /// <param name="dividend">The dividend.</param>
    /// <param name="divisor">The divisor.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector2D Divide(double dividend, Vector2D divisor) => DivideByVectorUniform(dividend, divisor.I, divisor.I);

    /// <summary>
    /// Dots the product.
    /// </summary>
    /// <param name="right">The right.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly double DotProduct(Vector2D right) => Operations.DotProduct(I, J, right.I, right.J);

    /// <summary>
    /// Dots the product.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double DotProduct(Vector2D left, Vector2D right) => Operations.DotProduct(left.I, left.J, right.I, right.J);

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true" /> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Equals([AllowNull] object obj) => obj is Vector2D d && Equals(d);

    /// <summary>
    /// The equals.
    /// </summary>
    /// <param name="other">The other.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly bool Equals([AllowNull] Vector2D other) => I == other.I && J == other.J;

    /// <summary>
    /// Converts to valuetuple.
    /// </summary>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly (double I, double J) ToValueTuple() => (I, J);

    /// <summary>
    /// Froms the value tuple.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Vector2D FromValueTuple((double X, double Y) tuple) => new(tuple);

    /// <summary>
    /// Converts to vector2d.
    /// </summary>
    /// <returns></returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly Vector2D ToVector2D() => new(I, J);
    #endregion

    #region Factories
    /// <summary>
    /// Parse a string for a <see cref="Vector2D" /> value.
    /// </summary>
    /// <param name="source"><see cref="string" /> with <see cref="Vector2D" /> data</param>
    /// <returns>
    /// Returns an instance of the <see cref="Vector2D" /> struct converted
    /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
    /// </returns>
    [ParseMethod]
    public static Vector2D Parse(string source) => Parse(source, CultureInfo.InvariantCulture);

    /// <summary>
    /// Parse a string for a <see cref="Vector2D" /> value.
    /// </summary>
    /// <param name="source"><see cref="string" /> with <see cref="Vector2D" /> data</param>
    /// <param name="formatProvider">The provider.</param>
    /// <returns>
    /// Returns an instance of the <see cref="Vector2D" /> struct converted
    /// from the provided string using the <see cref="CultureInfo.InvariantCulture" />.
    /// </returns>
    public static Vector2D Parse(string source, IFormatProvider formatProvider)
    {
        var tokenizer = new Tokenizer(source, formatProvider);
        var firstToken = tokenizer.NextTokenRequired();

        var value = new Vector2D(
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
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>
    /// A 32-bit signed <see cref="int" /> hash code.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override readonly int GetHashCode() => HashCode.Combine(I, J);

    /// <summary>
    /// Creates a human-readable string that represents this <see cref="Vector2D" />.
    /// </summary>
    /// <returns>
    /// A string representation of this <see cref="Vector2D" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override string ToString() => ToString("R" /* format string */, CultureInfo.InvariantCulture /* format provider */);

    /// <summary>
    /// Creates a string representation of this <see cref="Vector2D" /> struct based on the IFormatProvider
    /// passed in.  If the provider is null, the CurrentCulture is used.
    /// </summary>
    /// <param name="formatProvider">The <see cref="CultureInfo" /> provider.</param>
    /// <returns>
    /// A string representation of this <see cref="Vector2D" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public string ToString(IFormatProvider formatProvider) => ToString("R" /* format string */, formatProvider);

    /// <summary>
    /// Creates a string representation of this <see cref="Vector2D" /> struct based on the format string
    /// and IFormatProvider passed in.
    /// If the provider is null, the CurrentCulture is used.
    /// See the documentation for IFormattable for more information.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="formatProvider">The <see cref="CultureInfo"/> provider.</param>
    /// <returns>
    /// A string representation of this <see cref="Vector2D" />.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly string ToString(string format, IFormatProvider formatProvider)
    {
        if (this == null) return nameof(Vector2D);
        var s = Tokenizer.GetNumericListSeparator(formatProvider);
        return $"{nameof(Vector2D)}({nameof(I)}: {I.ToString(format, formatProvider)}{s} {nameof(J)}: {J.ToString(format, formatProvider)})";
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
