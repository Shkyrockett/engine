// <copyright file="Polynomial.cs" company="Shkyrockett" >
// Copyright © 2014 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary>
//     Based on classes from https://github.com/superlloyd/Poly, http://www.kevlindev.com/geometry/2D/intersections/, and https://github.com/Pomax/bezierjs.
// </summary>
// <remarks></remarks>

using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using static Engine.Operations;
using static System.Math;

namespace Engine;

/// <summary>
/// A Polynomial<U> representation of a series of numbers.
/// </summary>
/// <seealso cref="IEquatable{T}" />
/// <remarks>
/// <para>Internally the polynomial is represented by an array of the coefficients in reverse order.
/// When accessed externally, the coefficients are generally in forward order.</para>
/// </remarks>
/// <seealso cref="IEquatable{T}" />
[DataContract, Serializable]
//[TypeConverter(typeof(StructConverter<Polynomial<T>>))]
[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public struct Polynomial<U>
    : IEquatable<Polynomial<U>>
    where U : struct, IFloatingPointIeee754<U>
{
    #region Constants
    /// <summary>
    /// The array form of the Identity <see cref="Polynomial{T}" />.
    /// </summary>
    public static readonly U[] Identity = [U.One, U.Zero];
    #endregion

    #region Implementations
    /// <summary>
    /// An empty polynomial.
    /// </summary>
    public static readonly Polynomial<U> Empty = new(U.Zero);

    /// <summary>
    /// The T Identity polynomial.
    /// </summary>
    public static readonly Polynomial<U> T = new(Identity);

    /// <summary>
    /// One minus the T Identity polynomial.
    /// </summary>
    public static readonly Polynomial<U> OneMinusT = U.One - T;
    #endregion

    #region Fields
    /// <summary>
    /// The coefficients of the polynomial in lowest degree to highest degree order.
    /// </summary>
    private U[] coefficients;

    /// <summary>
    /// Cache for the real order degree value.
    /// </summary>
    private PolynomialDegree? degree;

    /// <summary>
    /// Semaphore indicating whether the polynomial can be edited.
    /// </summary>
    private bool isReadonly;
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Polynomial" /> class using left to right letter order, descending in degrees.
    /// </summary>
    /// <param name="coefficients">The coefficients of the polynomial.</param>
    /// <exception cref="ArgumentNullException">coefficients</exception>
    /// <remarks>
    /// <para>While the coefficients are entered in left to right letter order, they are
    /// stored in degree order to simplify operations on <see cref="Polynomial" /> structs.</para>
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Polynomial(params U[] coefficients)
    {
        ArgumentNullException.ThrowIfNull(coefficients);

        // If the coefficients array is empty this is an Empty polynomial, otherwise copy the coefficients over.
        // Reverse the coefficients so they are in order of degree smallest to largest.
        this.coefficients = (coefficients is null || coefficients.Length == 0)
            ? this.coefficients = [U.Zero]
            : coefficients.Reverse().ToArray();
        // Not initially read only.
        isReadonly = false;
        degree = null;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Polynomial{U}"/> struct.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Polynomial((U A, U B) tuple)
        : this(tuple.A, tuple.B)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Polynomial{U}"/> struct.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Polynomial((U A, U B, U C) tuple)
        : this(tuple.A, tuple.B, tuple.C)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Polynomial{U}"/> struct.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Polynomial((U A, U B, U C, U D) tuple)
        : this(tuple.A, tuple.B, tuple.C, tuple.D)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Polynomial{U}"/> struct.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Polynomial((U A, U B, U C, U D, U E) tuple)
        : this(tuple.A, tuple.B, tuple.C, tuple.D, tuple.E)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Polynomial{U}"/> struct.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Polynomial((U A, U B, U C, U D, U E, U F) tuple)
        : this(tuple.A, tuple.B, tuple.C, tuple.D, tuple.E, tuple.F)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Polynomial{U}"/> struct.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Polynomial((U A, U B, U C, U D, U E, U F, U G) tuple)
        : this(tuple.A, tuple.B, tuple.C, tuple.D, tuple.E, tuple.F, tuple.G)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Polynomial{U}"/> struct.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Polynomial((U A, U B, U C, U D, U E, U F, U G, U H) tuple)
        : this(tuple.A, tuple.B, tuple.C, tuple.D, tuple.E, tuple.F, tuple.G, tuple.H)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Polynomial{U}"/> struct.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Polynomial((U A, U B, U C, U D, U E, U F, U G, U H, U I) tuple)
        : this(tuple.A, tuple.B, tuple.C, tuple.D, tuple.E, tuple.F, tuple.G, tuple.H, tuple.I)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Polynomial{U}"/> struct.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Polynomial((U A, U B, U C, U D, U E, U F, U G, U H, U I, U J) tuple)
        : this(tuple.A, tuple.B, tuple.C, tuple.D, tuple.E, tuple.F, tuple.G, tuple.H, tuple.I, tuple.J)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Polynomial{U}"/> struct.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Polynomial((U A, U B, U C, U D, U E, U F, U G, U H, U I, U J, U K) tuple)
        : this(tuple.A, tuple.B, tuple.C, tuple.D, tuple.E, tuple.F, tuple.G, tuple.H, tuple.I, tuple.J, tuple.K)
    { }
    #endregion

    #region Indexers
    /// <summary>
    /// Gets or sets the coefficient at the given index.
    /// </summary>
    /// <value>
    /// The <see cref="U"/>.
    /// </value>
    /// <param name="index">The index of the coefficient to retrieve.</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="ArgumentOutOfRangeException">index</exception>
    /// <remarks>
    /// <para>The indexer is in highest degree to lowest format.</para>
    /// </remarks>
    /// <acknowledgment>
    /// modified from the indexer used in Super Lloyd's Poly class https://github.com/superlloyd/Poly
    /// </acknowledgment>
    public U this[int index]
    {
        readonly get
        {
            if (index < 0 || index >= coefficients.Length)
            {
                return U.Zero;
            }

            return coefficients[coefficients.Length - 1 - index];
        }
        set
        {
            if (IsReadonly)
            {
                throw new InvalidOperationException($"{nameof(Polynomial<U>)} is Read-only.");
            }

            if (index < 0 || index > coefficients.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            coefficients[coefficients.Length - 1 - index] = value;
            degree = null;
        }
    }

    /// <summary>
    /// Gets or sets the coefficient at the given term index.
    /// </summary>
    /// <value>
    /// The <see cref="U"/>.
    /// </value>
    /// <param name="index">The term index of the coefficient to retrieve.</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="ArgumentOutOfRangeException">index</exception>
    /// <remarks>
    /// <para>The <see cref="PolynomialTerm" /> indexer is in highest degree to lowest format.</para>
    /// </remarks>
    /// <acknowledgment>
    /// modified from the indexer used in Super Lloyd's Poly class https://github.com/superlloyd/Poly
    /// </acknowledgment>
    public U this[PolynomialTerm index]
    {
        readonly get
        {
            if (index < 0 || (int)index >= coefficients.Length)
            {
                return U.Zero;
            }

            return coefficients[coefficients.Length - 1 - (int)index];
        }
        set
        {
            if (IsReadonly)
            {
                throw new InvalidOperationException($"{nameof(Polynomial<U>)} is Read-only.");
            }

            if (index < 0 || (int)index > coefficients.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            coefficients[coefficients.Length - 1 - (int)index] = value;
            degree = null;
        }
    }

    /// <summary>
    /// Gets or sets the coefficient of the given degree index.
    /// </summary>
    /// <value>
    /// The <see cref="U"/>.
    /// </value>
    /// <param name="index">The degree of the coefficient to retrieve.</param>
    /// <returns>
    /// The value of the coefficient of the requested degree.
    /// </returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="ArgumentOutOfRangeException">index</exception>
    /// <remarks>
    /// <para>The <see cref="PolynomialDegree" /> indexer is in lowest degree to highest format.</para>
    /// </remarks>
    /// <acknowledgment>
    /// modified from the indexer used in Super Lloyd's Poly class https://github.com/superlloyd/Poly
    /// </acknowledgment>
    public U this[PolynomialDegree index]
    {
        readonly get
        {
            if (index < 0 || (int)index >= coefficients.Length)
            {
                return U.Zero;
            }

            return coefficients[(int)index];
        }
        set
        {
            if (IsReadonly)
            {
                throw new InvalidOperationException($"{nameof(Polynomial<U>)} is Read-only.");
            }

            if (index < 0 || (int)index > coefficients.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            coefficients[(int)index] = value;
            degree = null;
        }
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the degree of the polynomial.
    /// </summary>
    /// <value>
    /// The degree.
    /// </value>
    /// <remarks>
    /// <para>If degree uninitialized look up the real order then cache it and return.</para>
    /// </remarks>
    public PolynomialDegree Degree => degree ??= RealOrder(U.Epsilon);

    /// <summary>
    /// Gets the raw number of coefficients found in the polynomial, including any leading zero coefficients.
    /// </summary>
    /// <value>
    /// The count.
    /// </value>
    public readonly int Count => coefficients?.Length ?? 0;

    /// <summary>
    /// Gets a value indicating whether the real roots for the polynomial can be solved with available methods.
    /// </summary>
    /// <value>
    ///   <see langword="true"/> if this instance can solve real roots; otherwise, <see langword="false"/>.
    /// </value>
    /// <acknowledgment>
    /// https://github.com/superlloyd/Poly
    /// </acknowledgment>
    public bool CanSolveRealRoots => Degree <= PolynomialDegree.Quintic;

    /// <summary>
    /// Gets or sets a value indicating whether the <see cref="Polynomial" /> struct is read only.
    /// Useful for class that want to expose internal value that must not change.
    /// </summary>
    /// <value>
    ///   <see langword="true"/> if this instance is readonly; otherwise, <see langword="false"/>.
    /// </value>
    /// <remarks>
    /// <para>Once set, this cannot become writable again.</para>
    /// </remarks>
    /// <acknowledgment>
    /// https://github.com/superlloyd/Poly
    /// </acknowledgment>
    public bool IsReadonly
    {
        readonly get { return isReadonly; }
        set
        {
            if (IsReadonly)
            {
                return;
            }

            isReadonly |= value;
        }
    }

#if DEBUG
    /// <summary>
    /// Gets or sets the coefficients of the polynomial from highest degree to lowest degree order.
    /// </summary>
    /// <value>
    /// The coefficients.
    /// </value>
    /// <remarks>
    /// <para>This property presents the <see cref="Coefficients" /> in the reverse order than they are internally stored.</para>
    /// </remarks>
    [TypeConverter(typeof(ArrayConverter))]
    public U[] Coefficients
    {
        readonly get { return coefficients.Reverse().ToArray(); }
        set
        {
            if (IsReadonly)
            {
                throw new InvalidOperationException($"{nameof(Polynomial<U>)} is Read-only.");
            }

            coefficients = value.Reverse().ToArray();
        }
    }

    /// <summary>
    /// Gets a debug string that represents the text version of the <see cref="Polynomial" />.
    /// </summary>
    /// <value>
    /// The text.
    /// </value>
    public readonly string Text => ToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);
#endif
    #endregion

    #region Operators
    /// <summary>
    /// Unary addition operator.
    /// </summary>
    /// <param name="value">The original value.</param>
    /// <returns>
    /// The <see cref="Polynomial" />.
    /// </returns>
    /// <acknowledgment>
    /// https://github.com/superlloyd/Poly
    /// </acknowledgment>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> operator +(Polynomial<U> value) => Plus(value);

    /// <summary>
    /// Add an amount to both values in the <see cref="Polynomial" /> structs.
    /// </summary>
    /// <param name="augend">The original value.</param>
    /// <param name="addend">The amount to add.</param>
    /// <returns>
    /// The <see cref="Polynomial" />.
    /// </returns>
    /// <acknowledgment>
    /// https://github.com/superlloyd/Poly
    /// </acknowledgment>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> operator +(Polynomial<U> augend, U addend) => Add(augend, addend);

    /// <summary>
    /// Add an amount to both values in the <see cref="Polynomial" /> structs.
    /// </summary>
    /// <param name="augend">The original value</param>
    /// <param name="addend">The amount to add.</param>
    /// <returns>
    /// The <see cref="Polynomial" />.
    /// </returns>
    /// <acknowledgment>
    /// https://github.com/superlloyd/Poly
    /// </acknowledgment>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> operator +(U augend, Polynomial<U> addend) => Add(augend, addend);

    /// <summary>
    /// Add an amount to both values in the <see cref="Polynomial" /> structs.
    /// </summary>
    /// <param name="augend">The original value</param>
    /// <param name="addend">The amount to add.</param>
    /// <returns>
    /// The <see cref="Polynomial" />.
    /// </returns>
    /// <acknowledgment>
    /// https://github.com/superlloyd/Poly
    /// </acknowledgment>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> operator +(Polynomial<U> augend, Polynomial<U> addend) => Add(augend, addend);

    /// <summary>
    /// Unary subtraction operator.
    /// </summary>
    /// <param name="value">The original value</param>
    /// <returns>
    /// The <see cref="Polynomial" />.
    /// </returns>
    /// <acknowledgment>
    /// https://github.com/superlloyd/Poly
    /// </acknowledgment>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> operator -(Polynomial<U> value) => Negate(value);

    /// <summary>
    /// Subtract a <see cref="Polynomial" /> from a <see cref="U" /> value.
    /// </summary>
    /// <param name="value">The original value</param>
    /// <param name="subend">The amount to subtract.</param>
    /// <returns>
    /// The <see cref="Polynomial" />.
    /// </returns>
    /// <acknowledgment>
    /// https://github.com/superlloyd/Poly
    /// </acknowledgment>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> operator -(Polynomial<U> value, U subend) => value + (-subend);

    /// <summary>
    /// Subtract a <see cref="U" /> from a <see cref="Polynomial" /> value.
    /// </summary>
    /// <param name="minuend">The original value</param>
    /// <param name="subend">The amount to subtract.</param>
    /// <returns>
    /// The <see cref="Polynomial" />.
    /// </returns>
    /// <acknowledgment>
    /// https://github.com/superlloyd/Poly
    /// </acknowledgment>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> operator -(U minuend, Polynomial<U> subend) => Subtract(minuend, subend);

    /// <summary>
    /// Subtract a <see cref="Polynomial" /> from another <see cref="Polynomial" /> class.
    /// </summary>
    /// <param name="minuend">The original value</param>
    /// <param name="subend">The amount to subtract.</param>
    /// <returns>
    /// The <see cref="Polynomial" />.
    /// </returns>
    /// <acknowledgment>
    /// https://github.com/superlloyd/Poly
    /// </acknowledgment>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> operator -(Polynomial<U> minuend, Polynomial<U> subend) => Subtract(minuend, subend);

    /// <summary>
    /// Scale a <see cref="Polynomial" /> by a value.
    /// </summary>
    /// <param name="multiplicand">The original value</param>
    /// <param name="multiplier">The factor value.</param>
    /// <returns>
    /// The <see cref="Polynomial" />.
    /// </returns>
    /// <acknowledgment>
    /// https://github.com/superlloyd/Poly
    /// </acknowledgment>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> operator *(Polynomial<U> multiplicand, U multiplier) => Multiply(multiplicand, multiplier);

    /// <summary>
    /// Scale a <see cref="Polynomial" /> by a value.
    /// </summary>
    /// <param name="multiplicand">The original value</param>
    /// <param name="multiplier">The factor value.</param>
    /// <returns>
    /// The <see cref="Polynomial" />.
    /// </returns>
    /// <acknowledgment>
    /// https://github.com/superlloyd/Poly
    /// </acknowledgment>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> operator *(U multiplicand, Polynomial<U> multiplier) => Multiply(multiplicand, multiplier);

    /// <summary>
    /// Multiply two <see cref="Polynomial" />s together.
    /// </summary>
    /// <param name="multiplicand">The multiplicand.</param>
    /// <param name="multiplier">The factor value.</param>
    /// <returns>
    /// The <see cref="Polynomial" />.
    /// </returns>
    /// <acknowledgment>
    /// https://github.com/superlloyd/Poly
    /// </acknowledgment>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> operator *(Polynomial<U> multiplicand, Polynomial<U> multiplier) => Multiply(multiplicand, multiplier);

    /// <summary>
    /// Divide a <see cref="Polynomial" /> by a value.
    /// </summary>
    /// <param name="dividend">The divisor value</param>
    /// <param name="divisor">The dividend to add.</param>
    /// <returns>
    /// The <see cref="Polynomial" />.
    /// </returns>
    /// <acknowledgment>
    /// https://github.com/superlloyd/Poly
    /// </acknowledgment>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> operator /(Polynomial<U> dividend, U divisor) => Divide(dividend, divisor);

    /// <summary>
    /// Divide a <see cref="Polynomial" /> by a value.
    /// </summary>
    /// <param name="dividend">The divisor value</param>
    /// <param name="divisor">The dividend to add.</param>
    /// <returns>
    /// The <see cref="Polynomial" />.
    /// </returns>
    /// <acknowledgment>
    /// https://github.com/superlloyd/Poly
    /// </acknowledgment>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> operator /(U dividend, Polynomial<U> divisor) => Divide(dividend, divisor);

    /// <summary>
    /// Compares two <see cref="Polynomial{U}"/> objects.
    /// The result specifies whether the values of the x and x
    /// values of the two <see cref="Polynomial{U}"/> objects are equal.
    /// </summary>
    /// <param name="left">The left Polynomial.</param>
    /// <param name="right">The right Polynomial.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool operator ==(Polynomial<U> left, Polynomial<U> right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="Polynomial" /> objects.
    /// The result specifies whether the values of the x or y
    /// values of the two <see cref="Polynomial" /> objects are unequal.
    /// </summary>
    /// <param name="left">The left Polynomial.</param>
    /// <param name="right">The right Polynomial.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool operator !=(Polynomial<U> left, Polynomial<U> right) => !(left == right);

    /// <summary>
    /// Performs an implicit conversion from <see cref="System.ValueTuple{T, T}"/> to <see cref="Polynomial{U}"/>.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static implicit operator Polynomial<U>((U A, U B) tuple) => new(tuple);

    /// <summary>
    /// Performs an implicit conversion from <see cref="System.ValueTuple{T, T, T}"/> to <see cref="Polynomial{U}"/>.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static implicit operator Polynomial<U>((U A, U B, U C) tuple) => new(tuple);

    /// <summary>
    /// Performs an implicit conversion from <see cref="System.ValueTuple{T, T, T, T}"/> to <see cref="Polynomial{U}"/>.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static implicit operator Polynomial<U>((U A, U B, U C, U D) tuple) => new(tuple);

    /// <summary>
    /// Performs an implicit conversion from <see cref="System.ValueTuple{T, T, T, T, T}"/> to <see cref="Polynomial{U}"/>.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static implicit operator Polynomial<U>((U A, U B, U C, U D, U E) tuple) => new(tuple);

    /// <summary>
    /// Performs an implicit conversion from <see cref="System.ValueTuple{T, T, T, T, T, T}"/> to <see cref="Polynomial{U}"/>.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static implicit operator Polynomial<U>((U A, U B, U C, U D, U E, U F) tuple) => new(tuple);

    /// <summary>
    /// Performs an implicit conversion from <see cref="System.ValueTuple{T, T, T, T, T, T, T}"/> to <see cref="Polynomial{U}"/>.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static implicit operator Polynomial<U>((U A, U B, U C, U D, U E, U F, U G) tuple) => new(tuple);

    /// <summary>
    /// Performs an implicit conversion from <see cref="System.ValueTuple{T, T, T, T, T, T, T, T}"/> to <see cref="Polynomial{U}"/>.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static implicit operator Polynomial<U>((U A, U B, U C, U D, U E, U F, U G, U H) tuple) => new(tuple);

    /// <summary>
    /// Performs an implicit conversion from <see cref="System.ValueTuple{T, T, T, T, T, T, T, T}"/> to <see cref="Polynomial{U}"/>.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static implicit operator Polynomial<U>((U A, U B, U C, U D, U E, U F, U G, U H, U I) tuple) => new(tuple);

    /// <summary>
    /// Performs an implicit conversion from <see cref="System.ValueTuple{T, T, T, T, T, T, T, T}"/> to <see cref="Polynomial{U}"/>.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static implicit operator Polynomial<U>((U A, U B, U C, U D, U E, U F, U G, U H, U I, U J) tuple) => new(tuple);

    /// <summary>
    /// Performs an implicit conversion from <see cref="System.ValueTuple{T, T, T, T, T, T, T, T}"/> to <see cref="Polynomial{U}"/>.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    [DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static implicit operator Polynomial<U>((U A, U B, U C, U D, U E, U F, U G, U H, U I, U J, U K) tuple) => new(tuple);
    #endregion

    #region Operator Backing Methods
    /// <summary>
    /// Pluses the specified item.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> Plus(Polynomial<U> value)
    {
        var res = new U[value.Count];
        for (var i = 0; i < res.Length; i++)
        {
            res[i] = +value.coefficients[i];
        }

        return new Polynomial<U> { coefficients = res, isReadonly = value.isReadonly };
    }

    /// <summary>
    /// Adds the specified left.
    /// </summary>
    /// <param name="augend">The augend.</param>
    /// <param name="addend">The addend.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> Add(Polynomial<U> augend, U addend)
    {
        var res = new U[augend.Count];
        Array.Copy(augend.coefficients, res, augend.Count);
        res[0] += addend;
        return new Polynomial<U> { coefficients = res, isReadonly = augend.isReadonly };
    }

    /// <summary>
    /// Adds the specified left.
    /// </summary>
    /// <param name="augend">The left.</param>
    /// <param name="addend">The right.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> Add(U augend, Polynomial<U> addend)
    {
        var res = new U[addend.Count];
        Array.Copy(addend.coefficients, res, addend.Count);
        res[0] += augend;
        return new Polynomial<U> { coefficients = res, isReadonly = addend.isReadonly };
    }

    /// <summary>
    /// Adds the specified left.
    /// </summary>
    /// <param name="augend">The left.</param>
    /// <param name="addend">The right.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> Add(Polynomial<U> augend, Polynomial<U> addend)
    {
        var res = new U[Max(augend.Count, addend.Count)];
        for (var i = 0; i < res.Length; i++)
        {
            U p = U.Zero;
            if (i < augend.Count)
            {
                p += augend.coefficients[i];
            }

            if (i < addend.Count)
            {
                p += addend.coefficients[i];
            }

            res[i] = p;
        }
        return new Polynomial<U> { coefficients = res, isReadonly = augend.isReadonly | addend.isReadonly };
    }

    /// <summary>
    /// Negates the specified item.
    /// </summary>
    /// <param name="value">The item.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> Negate(Polynomial<U> value)
    {
        var res = new U[value.Count];
        for (var i = 0; i < res.Length; i++)
        {
            res[i] = -value.coefficients[i];
        }

        return new Polynomial<U> { coefficients = res, isReadonly = value.isReadonly };
    }

    /// <summary>
    /// Subtracts the specified left.
    /// </summary>
    /// <param name="minuend">The left.</param>
    /// <param name="subend">The right.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> Subtract(Polynomial<U> minuend, U subend)
    {
        var res = new U[minuend.Count];
        for (var i = 0; i < res.Length; i++)
        {
            res[i] = -minuend.coefficients[i];
        }

        res[0] += subend;
        return new Polynomial<U> { coefficients = res, isReadonly = minuend.isReadonly };
    }

    /// <summary>
    /// Subtracts the specified left.
    /// </summary>
    /// <param name="minuend">The left.</param>
    /// <param name="subend">The right.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> Subtract(U minuend, Polynomial<U> subend)
    {
        var res = new U[subend.Count];
        for (var i = 0; i < res.Length; i++)
        {
            res[i] = -subend.coefficients[i];
        }

        res[0] += minuend;
        return new Polynomial<U> { coefficients = res, isReadonly = subend.isReadonly };
    }

    /// <summary>
    /// Subtracts the specified left.
    /// </summary>
    /// <param name="minuend">The left.</param>
    /// <param name="subend">The right.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> Subtract(Polynomial<U> minuend, Polynomial<U> subend)
    {
        var res = new U[Max(minuend.Count, subend.Count)];
        for (var i = 0; i < res.Length; i++)
        {
            U p = U.Zero;
            if (i < minuend.Count)
            {
                p += minuend.coefficients[i];
            }

            if (i < subend.Count)
            {
                p -= subend.coefficients[i];
            }

            res[i] = p;
        }

        return new Polynomial<U> { coefficients = res, isReadonly = minuend.isReadonly | subend.isReadonly };
    }

    /// <summary>
    /// Multiplies the specified left.
    /// </summary>
    /// <param name="multiplicand">The left.</param>
    /// <param name="multiplier">The right.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> Multiply(Polynomial<U> multiplicand, U multiplier)
    {
        var res = new U[multiplicand.Count];
        for (var i = 0; i < res.Length; i++)
        {
            res[i] = multiplicand.coefficients[i] * multiplier;
        }

        return new Polynomial<U> { coefficients = res, isReadonly = multiplicand.isReadonly };
    }

    /// <summary>
    /// Multiplies the specified left.
    /// </summary>
    /// <param name="multiplicand">The left.</param>
    /// <param name="multiplier">The right.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> Multiply(U multiplicand, Polynomial<U> multiplier)
    {
        var res = new U[multiplier.Count];
        for (var i = 0; i < res.Length; i++)
        {
            res[i] = multiplicand * multiplier.coefficients[i];
        }

        return new Polynomial<U> { coefficients = res, isReadonly = multiplier.isReadonly };
    }

    /// <summary>
    /// Multiplies the specified left.
    /// </summary>
    /// <param name="multiplicand">The left.</param>
    /// <param name="multiplier">The right.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> Multiply(Polynomial<U> multiplicand, Polynomial<U> multiplier)
    {
        var res = new U[multiplicand.Count + multiplier.Count - 1];
        for (var i = 0; i < multiplicand.Count; i++)
        {
            for (var j = 0; j < multiplier.Count; j++)
            {
                var mul = multiplicand.coefficients[i] * multiplier.coefficients[j];
                res[i + j] += mul;
            }
        }

        return new Polynomial<U> { coefficients = res, isReadonly = multiplicand.isReadonly | multiplier.isReadonly };
    }

    /// <summary>
    /// Divides the specified left.
    /// </summary>
    /// <param name="dividend">The left.</param>
    /// <param name="divisor">The right.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> Divide(Polynomial<U> dividend, U divisor)
    {
        var res = new U[dividend.Count];
        for (var i = 0; i < res.Length; i++)
        {
            res[i] = dividend.coefficients[i] / divisor;
        }

        return new Polynomial<U> { coefficients = res, isReadonly = dividend.isReadonly };
    }

    /// <summary>
    /// Divides the specified left.
    /// </summary>
    /// <param name="dividend">The left.</param>
    /// <param name="divisor">The right.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> Divide(U dividend, Polynomial<U> divisor)
    {
        var res = new U[divisor.Count];
        for (var i = 0; i < res.Length; i++)
        {
            res[i] = dividend / divisor.coefficients[i];
        }

        return new Polynomial<U> { coefficients = res, isReadonly = divisor.isReadonly };
    }

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <see langword="true"/> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false"/>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override bool Equals([AllowNull] object obj) => obj is Polynomial<U> d && Equals(d);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public bool Equals([AllowNull] Polynomial<U> other)
    {
        var t1 = Trim();
        var t2 = other.Trim();

        if (t1.Count != t2.Count)
        {
            return false;
        }

        for (var i = 0; i < t1.Count; i++)
        {
            if (U.Abs(t1.coefficients[i] - t2.coefficients[i]) > U.Epsilon)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Froms the value tuple.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> FromValueTuple((U A, U B) tuple) => new(tuple);

    /// <summary>
    /// Froms the value tuple.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> FromValueTuple((U A, U B, U C) tuple) => new(tuple);

    /// <summary>
    /// Froms the value tuple.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> FromValueTuple((U A, U B, U C, U D) tuple) => new(tuple);

    /// <summary>
    /// Froms the value tuple.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> FromValueTuple((U A, U B, U C, U D, U E) tuple) => new(tuple);

    /// <summary>
    /// Froms the value tuple.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> FromValueTuple((U A, U B, U C, U D, U E, U F) tuple) => new(tuple);

    /// <summary>
    /// Froms the value tuple.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> FromValueTuple((U A, U B, U C, U D, U E, U F, U G) tuple) => new(tuple);

    /// <summary>
    /// Froms the value tuple.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> FromValueTuple((U A, U B, U C, U D, U E, U F, U G, U H) tuple) => new(tuple);

    /// <summary>
    /// Froms the value tuple.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> FromValueTuple((U A, U B, U C, U D, U E, U F, U G, U H, U I) tuple) => new(tuple);

    /// <summary>
    /// Froms the value tuple.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> FromValueTuple((U A, U B, U C, U D, U E, U F, U G, U H, U I, U J) tuple) => new(tuple);

    /// <summary>
    /// Froms the value tuple.
    /// </summary>
    /// <param name="tuple">The tuple.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> FromValueTuple((U A, U B, U C, U D, U E, U F, U G, U H, U I, U J, U K) tuple) => new(tuple);
    #endregion

    #region Operations
    /// <summary>
    /// Trim off any leading zero coefficient terms from the Polynomial.
    /// </summary>
    /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
    /// <returns>
    /// Returns a <see cref="Polynomial" /> with any leading zero coefficient terms removed.
    /// </returns>
    /// <acknowledgment>
    /// A hodge-podge method based on Simplify from of: http://www.kevlindev.com/
    /// and Trim from: https://github.com/superlloyd/Poly
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Polynomial<U> Trim(U? epsilon = default)
    {
        epsilon ??= U.Epsilon;

        Contract.Ensures(Contract.Result<Polynomial<U>>() != null);
        Contract.EndContractBlock();

        // If there are no coefficients then this is a Monomial of 0.
        if (coefficients is null || coefficients.Length < 1)
        {
            return new Polynomial<U>(U.Zero) { isReadonly = isReadonly };
        }

        // Get the real degree to skip any leading zero coefficients.
        var order = (int)(degree ??= RealOrder(epsilon)) + 1; /*Warning! Side effect!*/

        // Copy the remaining coefficients to a new array and return it.
        var coeffs = new U[order];
        Array.Copy(coefficients, 0, coeffs, 0, order);
        return new Polynomial<U> { coefficients = coeffs, isReadonly = isReadonly };
    }

    /// <summary>
    /// Find the derivative polynomial of a <see cref="Polynomial" />.
    /// </summary>
    /// <returns>
    /// Returns a new <see cref="Polynomial" /> struct calculated as the derivative of the source <see cref="Polynomial" />.
    /// </returns>
    /// <acknowledgment>
    /// https://github.com/superlloyd/Poly
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Polynomial<U> Derivate()
    {
        Contract.Ensures(Contract.Result<Polynomial<U>>() != null);
        Contract.EndContractBlock();

        // Get the real degree to skip any leading zero coefficients.
        var order = (int)Degree;
        var res = new U[Max(1, order)];
        for (var i = 1; i < order + 1; i++)
        {
            res[i - 1] = U.CreateSaturating(i) * coefficients[i];
        }

        return new Polynomial<U> { coefficients = res, isReadonly = isReadonly };
    }

    /// <summary>
    /// Normalizes a <see cref="Polynomial" />.
    /// </summary>
    /// <param name="epsilon">The epsilon.</param>
    /// <returns>
    /// Returns a normalized <see cref="Polynomial" /> calculated from the source <see cref="Polynomial" />.
    /// </returns>
    /// <acknowledgment>
    /// https://github.com/superlloyd/Poly
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Polynomial<U> Normalize(U? epsilon = default)
    {
        //Contract.Ensures(Contract.Result<Polynomial<U>>().Coefficients is not null);
        //Contract.EndContractBlock();

        epsilon ??= U.Epsilon;

        var order = (int)Degree; /* Get the real degree to skip any leading zero coefficients. */
        var high = coefficients[order];
        var res = new U[order + 1];
        for (var i = 0; i < res.Length; i++)
        {
            if (U.Abs(coefficients[i]) > epsilon)
            {
                res[i] = coefficients[i] / high;
            }
        }

        return new Polynomial<U> { coefficients = res, isReadonly = isReadonly };
    }

    /// <summary>
    /// Integrates a <see cref="Polynomial" />.
    /// </summary>
    /// <param name="term0">The term0.</param>
    /// <returns>
    /// The <see cref="Polynomial" />.
    /// </returns>
    /// <acknowledgment>
    /// https://github.com/superlloyd/Poly
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Polynomial<U> Integrate(U? term0 = default)
    {
        term0 ??= U.Zero;

        Contract.Ensures(Contract.Result<Polynomial<U>>() != null);
        //Contract.EndContractBlock();

        //ToDo: Figure out if the real order should be used or if the leading zero coefficients are needed.

        //var order = (int)Degree; /* Get the real degree to skip any leading zero coefficients. */
        var order = Count;
        var res = new U[order + 1];
        res[0] = term0.Value;
        for (var i = 0; i < order; i++)
        {
            res[i + 1] = coefficients[i] / (U.CreateSaturating(i) + U.One);
        }

        return new Polynomial<U> { coefficients = res, isReadonly = isReadonly };
    }

    /// <summary>
    /// Raises a Polynomial<U> to a <see cref="Power" />.
    /// </summary>
    /// <param name="n">The n.</param>
    /// <returns>
    /// The <see cref="Polynomial" />.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <exception cref="ArithmeticException"></exception>
    /// <acknowledgment>
    /// https://github.com/superlloyd/Poly
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Polynomial<U> Power(int n)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(n);

        if (double.IsNaN(n))
        {
            throw new ArithmeticException($"{nameof(Evaluate)}: parameter {nameof(n)} must be a number");
        }

        Contract.Ensures(Contract.Result<Polynomial<U>>() != null);
        //Contract.EndContractBlock();

        var order = (int)Degree; /* Get the real degree to skip any leading zero coefficients. */
        var res = new U[(order * n) + 1];
        var tmp = new U[(order * n) + 1];
        res[0] = U.One;
        for (var pow = 0; pow < n; pow++)
        {
            var porder = pow * order;
            for (var i = 0; i <= order; i++)
            {
                for (var j = 0; j <= porder; j++)
                {
                    var mul = coefficients[i] * res[j];
                    tmp[i + j] += mul;
                }
            }

            for (var i = 0; i <= porder + order; i++)
            {
                res[i] = tmp[i];
                tmp[i] = U.Zero;
            }
        }

        return new Polynomial<U> { coefficients = res, isReadonly = isReadonly };
    }

    /// <summary>
    /// An implementation of Horner's Evaluate method.
    /// </summary>
    /// <param name="x">The value to evaluate.</param>
    /// <returns>
    /// Returns a value calculated by evaluating the polynomial.
    /// </returns>
    /// <exception cref="ArithmeticException"></exception>
    /// <acknowledgment>
    /// https://en.wikipedia.org/wiki/Horner%27s_method
    /// https://github.com/thelonious/kld-polynomial
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public U Evaluate(U x)
    {
        if (U.IsNaN(x))
        {
            throw new ArithmeticException($"{nameof(Evaluate)}: parameter {nameof(x)} must be a number");
        }

        var order = (int)Degree;
        var result = U.Zero;

        //foreach (var coefficient in coefficients[^1..-1..0])
        //{
        //    result = result * x + coefficient;
        //}

        for (var i = order; i >= 0; i--)
        {
            result = (result * x) + coefficients[i];
        }

        return result;
    }

    /// <summary>
    /// An implementation of Horner's Evaluate method for Complex numbers.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <returns>
    /// The <see cref="Complex" />.
    /// </returns>
    /// <acknowledgment>
    /// https://github.com/superlloyd/Poly
    /// http://rosettacode.org/wiki/Horner%27s_rule_for_polynomial_evaluation
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public Complex Evaluate(Complex x)
    {
        Contract.Ensures(Contract.Result<Complex>() != null);

        var result = Complex.Zero;

        for (var i = (int)Degree; i >= 0; i--)
        {
            result = (result * x) + double.CreateSaturating(coefficients[i]);
        }

        return result;
    }

    /// <summary>
    /// Computes value of the differentiated polynomial at x.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <returns>
    /// The <see cref="U" />.
    /// </returns>
    /// <acknowledgment>
    /// https://github.com/thelonious/kld-polynomial
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public U Differentiate(U x) => Derivate().Evaluate(x);

    /// <summary>
    /// Finds the Minimum and Maximum values of the <see cref="Polynomial" />.
    /// </summary>
    /// <param name="minX">The lower bound minimum.</param>
    /// <param name="maxX">The upper bound maximum.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2}" />.
    /// </returns>
    /// <remarks>
    /// <para>Do not use this method on a polynomial that has been simplified or trimmed.</para>
    /// </remarks>
    /// <acknowledgment>
    /// https://github.com/superlloyd/Poly
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public (U minY, U maxY) MinMax(U? minX = default, U? maxX = default)
    {
        minX ??= U.Zero;
        maxX ??= U.One;

        var roots = Derivate().Trim().Roots().ToArray()
            .Where(t => t > minX && t < maxX)
            .Concat(Identity).ToArray();

        var first = true;

        (var minY, var maxY) = (minX, minX);

        foreach (var t in roots)
        {
            var y = Evaluate(t);
            if (first)
            {
                first = false;
                minY = maxY = y;
            }
            else
            {
                if (y < minY)
                {
                    minY = y;
                }

                if (y > maxY)
                {
                    maxY = y;
                }
            }
        }

        return (minY.Value, maxY.Value);
    }

    /// <summary>
    /// Calculates the real order or degree of the polynomial.
    /// </summary>
    /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
    /// <returns>
    /// Returns a <see cref="PolynomialDegree" /> value representing the order of degree of the polynomial.
    /// </returns>
    /// <remarks>
    /// <para>Primarily used to locate where to trim off any leading zero coefficients of the internal coefficients array.</para>
    /// </remarks>
    /// <acknowledgment>
    /// A hodge-podge helper method based on Simplify from of: http://www.kevlindev.com/
    /// as well as Trim and RealOrder from: https://github.com/superlloyd/Poly
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public PolynomialDegree RealOrder(U? epsilon = default)
    {
        epsilon ??= U.Epsilon;

        var pos = 1;

        // Monomial can be a zero constant, skip them and check the rest.
        if (Count > 1)
        {
            // Count the number of leading zeros. Because the coefficients array is reversed, start at the end because there should generally be fewer leading zeros than other coefficients.
            for (var i = Count - 1; i >= 1 /* Monomials can be 0. */; i--)
            {
                // Check if coefficient is a leading zero.
                if (U.Abs(coefficients[i]) <= epsilon)
                {
                    pos++;
                }
                else
                {
                    // Break early if a non zero value was found. This indicates the end of any leading zeros.
                    break;
                }
            }
        }

        // If coefficients is empty return constant, otherwise return the calculated order of degree of the polynomial.
        return (PolynomialDegree)(coefficients?.Length - pos ?? 0);
    }
    #endregion Operations

    #region Factories
    /// <summary>
    /// Get the standard base.
    /// </summary>
    /// <param name="degree">The degree.</param>
    /// <returns>
    /// The <see cref="Array" />.
    /// </returns>
    /// <exception cref="ArgumentException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U>[] GetStandardBase(int degree)
    {
        if (degree < 1)
        {
            throw new ArgumentException($"{nameof(degree)} expected to be greater than zero.");
        }

        var buf = new Polynomial<U>[degree];

        for (var i = 0; i < degree; i++)
        {
            buf[i] = Monomial((PolynomialDegree)i);
        }

        return buf;
    }

    /// <summary>
    /// The term.
    /// </summary>
    /// <param name="degree">The degree.</param>
    /// <param name="coefficient">The coefficient.</param>
    /// <returns>
    /// The <see cref="Polynomial" />.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <acknowledgment>
    /// https://github.com/superlloyd/Poly
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> Term(PolynomialDegree degree, U? coefficient = default)
    {
        coefficient ??= U.One;

        if (degree < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(degree), $"{nameof(degree)} cannot be negative.");
        }

        var d = (int)degree;
        var res = new U[d + 1];
        res[d] = coefficient.Value;
        return new Polynomial<U> { coefficients = res };
    }

    /// <summary>
    /// Construct a polynomial P such as y[i] = P.Compute(i).
    /// </summary>
    /// <param name="ys">The ys.</param>
    /// <returns>
    /// The <see cref="Polynomial" />.
    /// </returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <acknowledgment>
    /// https://github.com/superlloyd/Poly
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> Interpolate(params U[] ys)
    {
        if (ys is null || ys.Length < 2)
        {
            throw new ArgumentNullException($"{nameof(ys)}: At least 2 different points must be given");
        }

        var res = new Polynomial<U>();
        for (var i = 0; i < ys.Length; i++)
        {
            var e = new Polynomial<U>(U.One);
            for (var j = 0; j < ys.Length; j++)
            {
                if (j == i)
                {
                    continue;
                }

                e *= new Polynomial<U>(U.One, U.CreateSaturating(-j)) / U.CreateSaturating(i - j); // Don't know if 1,-j should be swapped.
            }
            res += ys[i] * e;
        }
        return res.Trim();
    }

    /// <summary>
    /// The monomial.
    /// </summary>
    /// <param name="degree">The degree.</param>
    /// <returns>
    /// The <see cref="Polynomial" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Polynomial<U> Monomial(PolynomialDegree degree)
    {
        //Contract.Ensures(Contract.Result<Polynomial<U>>() is not null);

        if (degree == 0)
        {
            return new Polynomial<U>(U.One);
        }

        var d = (int)degree;
        var coeffs = new U[d + 1];

        for (var i = 0; i < d; i++)
        {
            coeffs[i] = U.Zero;
        }

        coeffs[d] = U.One;
        return new Polynomial<U> { coefficients = coeffs };
    }
    #endregion Factories

    #region Methods
    /// <summary>
    /// The bisection.
    /// </summary>
    /// <param name="min">The min.</param>
    /// <param name="max">The max.</param>
    /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
    /// <returns>
    /// The <see cref="U" />.
    /// </returns>
    /// <acknowledgment>
    /// https://github.com/thelonious/kld-polynomial
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public U? Bisection(U min, U max, U? epsilon = default)
    {
        epsilon ??= U.Epsilon;

        U accuracy = Integers<U>.Six;
        var minValue = Evaluate(min);
        var maxValue = Evaluate(max);
        U? result = default;

        if (U.Abs(minValue) <= epsilon)
        {
            return min;
        }
        else if (U.Abs(maxValue) <= epsilon)
        {
            return max;
        }
        else if (minValue * maxValue <= U.Zero)
        {
            var tmp1 = U.Log(max - min);
            var tmp2 = Floats<U>.LogTen * accuracy;
            var iters = U.Ceiling((tmp1 + tmp2) * Floats<U>.InverseLogTwo);
            for (var i = U.Zero; i < iters; i++)
            {
                result = Floats<U>.OneHalf * (min + max);
                var value = Evaluate(result.Value);
                if (U.Abs(value) <= epsilon)
                {
                    break;
                }
                if (value * minValue < U.Zero)
                {
                    max = result.Value;
                    //maxValue = value; VS reports this isn't used?
                }
                else
                {
                    min = result.Value;
                    minValue = value;
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Will try to solve root analytically, and if it can will use numerical approach.
    /// </summary>
    /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
    /// <returns>
    /// The <see cref="IEnumerable{T}" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public unsafe Span<U> RealOrComplexRoots(U? epsilon = default)
    {
        epsilon ??= U.Epsilon;

        if (CanSolveRealRoots)
        {
            return Roots();
        }

        return ComplexRoots().ToArray()
            .Where(c => U.Abs(U.CreateSaturating(c.Imaginary)) < epsilon)
            .Select(c => U.CreateSaturating(c.Real)).ToArray();
    }

    /// <summary>
    /// This method use the Durand-Kerner aka Weierstrass algorithm to find approximate root of this polynomial.
    /// </summary>
    /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
    /// <returns>
    /// The <see cref="Array" />.
    /// </returns>
    /// <acknowledgment>
    /// https://github.com/superlloyd/Poly
    /// http://en.wikipedia.org/wiki/Durand%E2%80%93Kerner_method
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public unsafe Span<Complex> ComplexRoots(U? epsilon = default)
    {
        epsilon ??= U.Epsilon;

        var poly = Normalize();
        if (poly.Count == 1)
        {
            return [];
        }

        var order = (int)poly.Degree;
        Complex x0 = 1;
        var xMul = 0.4d + (0.9d * Complex.ImaginaryOne);
        var R0 = new Complex[order];
        for (var i = 0; i < R0.Length; i++)
        {
            R0[i] = x0;
            x0 *= xMul;
        }

        var R1 = new Complex[order];
        bool close;
        do
        {
            step();
            close = closeEnough();

            var tmp = R0;
            R0 = R1;
            R1 = tmp;
        } while (!close);

        return R0;

        Complex divider(int i)
        {
            Complex div = 1;
            for (var j = 0; j < R0.Length; j++)
            {
                if (j == i)
                {
                    continue;
                }

                div *= R0[i] - R0[j];
            }
            return div;
        }

        void step()
        {
            for (var i = 0; i < R0.Length; i++)
            {
                R1[i] = R0[i] - (poly.Evaluate(R0[i]) / divider(i));
            }
        }

        bool closeEnough()
        {
            for (var i = 0; i < R0.Length; i++)
            {
                var c = R0[i] - R1[i];
                if (U.Abs(U.CreateSaturating(c.Real)) > epsilon || U.Abs(U.CreateSaturating(c.Imaginary)) > epsilon)
                {
                    return false;
                }
            }
            return true;
        }
    }

    /// <summary>
    /// The roots in interval.
    /// </summary>
    /// <param name="min">The min.</param>
    /// <param name="max">The max.</param>
    /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
    /// <returns>
    /// The <see cref="Array" />.
    /// </returns>
    /// <acknowledgment>
    /// http://www.kevlindev.com/geometry/2D/intersections/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public unsafe Span<U> RootsInInterval(U? min = default, U? max = default, U? epsilon = default)
    {
        min ??= U.Zero;
        max ??= U.One;
        epsilon ??= U.Epsilon;

        var roots = new HashSet<U>();
        U? root;
        if (Degree == PolynomialDegree.Linear)
        {
            root = Bisection(min.Value, max.Value, epsilon);
            if (root is not null)
            {
                roots.Add(root.Value);
            }
        }
        else
        {
            // get roots of derivative
            var deriv = Derivate();
            var droots = deriv.Count == 1 && deriv[0] == U.Zero ? [] : deriv.RootsInInterval(min, max, epsilon);
            if (droots.Length > 0)
            {
                root = Bisection(min.Value, droots[0], epsilon);
                if (root is not null)
                {
                    roots.Add(root.Value);
                }
                // Find root on [droots[i],droots[i+1]] for 0 <= i <= count-2
                for (var i = 0; i <= droots.Length - 2; i++)
                {
                    root = Bisection(droots[i], droots[i + 1], epsilon);
                    if (root is not null)
                    {
                        roots.Add(root.Value);
                    }
                }
                // Find root on [droots[count-1],xmax]
                root = Bisection(droots[^1], max.Value, epsilon);
                if (root is not null)
                {
                    roots.Add(root.Value);
                }
            }
            else
            {
                // Polynomial<U> is monotone on [min,max], has at most one root
                root = Bisection(min.Value, max.Value, epsilon);
                if (root is not null)
                {
                    roots.Add(root.Value);
                }
            }
        }

        return roots.ToArray();
    }

    /// <summary>
    /// Find the Roots of up to Quintic degree <see cref="Polynomial" />s.
    /// </summary>
    /// <param name="epsilon">The <paramref name="epsilon" /> or minimal value to represent a change.</param>
    /// <returns>
    /// The <see cref="Array" />.
    /// </returns>
    /// <acknowledgment>
    /// http://www.kevlindev.com/geometry/2D/intersections/
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public unsafe Span<U> Roots(U? epsilon = default)
    {
        epsilon ??= U.Epsilon;

        switch (Degree)
        {
            case PolynomialDegree.Constant:
                if (coefficients is null)
                {
                    return [];
                }

                return new U[] { coefficients[0] };
            case PolynomialDegree.Linear:
                return LinearRoots(ref coefficients[1], ref coefficients[0], epsilon).ToArray();
            case PolynomialDegree.Quadratic:
                return QuadraticRoots(ref coefficients[2], ref coefficients[1], ref coefficients[0], epsilon).ToArray();
            case PolynomialDegree.Cubic:
                return CubicRoots(ref coefficients[3], ref coefficients[2], ref coefficients[1], ref coefficients[0], epsilon).ToArray();
            case PolynomialDegree.Quartic:
                return QuarticRoots(ref coefficients[4], ref coefficients[3], ref coefficients[2], ref coefficients[1], ref coefficients[0], epsilon).ToArray();
            case PolynomialDegree.Quintic:
                return QuinticRoots(ref coefficients[5], ref coefficients[4], ref coefficients[3], ref coefficients[2], ref coefficients[1], ref coefficients[0], epsilon).ToArray();
            case PolynomialDegree.Sextic:
            // ToDo: Uncomment when Sextic roots are implemented.
            //return poly.SexticRoots(epsilon);
            case PolynomialDegree.Septic:
            // ToDo: Uncomment when Septic roots are implemented.
            //return poly.SepticRoots(epsilon);
            case PolynomialDegree.Octic:
            // ToDo: Uncomment when Octic roots are implemented.
            //return poly.OcticRoots(epsilon);
            default:
                // ToDo: If a general root finding algorithm can be found, call it here instead of returning an empty list.
                return [];
        }

        // should try Newton's method and/or bisection
    }

    /// <summary>
    /// Clears the coefficients of the polynomial.
    /// </summary>
    public void Clear()
    {
        var size = coefficients.Length;
        if (size > 0)
        {
            // Clear the elements of the array so that the garbage collector can reclaim the references.
            Array.Clear(coefficients, 0, size);
        }

        degree = null;
    }
    #endregion

    #region Standard Methods
    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override int GetHashCode() => HashCode.Combine(Coefficients);

    /// <summary>
    /// Creates a human-readable string that represents this <see cref="Polynomial" /> struct.
    /// </summary>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public override readonly string ToString() => ToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

    /// <summary>
    /// Creates a string representation of this <see cref="Polynomial" /> struct based on the IFormatProvider
    /// passed in.  If the provider is null, the CurrentCulture is used.
    /// </summary>
    /// <param name="formatProvider">The provider.</param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly string ToString(IFormatProvider formatProvider) => ToString(string.Empty /* format string */, formatProvider /* format provider */);

    /// <summary>
    /// Creates a string representation of this <see cref="Polynomial" /> struct based on the IFormatProvider
    /// passed in.  If the provider is null, the CurrentCulture is used.
    /// </summary>
    /// <param name="format">The format.</param>
    /// <param name="formatProvider">The provider.</param>
    /// <returns>
    /// A string representation of this object.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public readonly string ToString(string format, IFormatProvider formatProvider)
    {
        // ¹²³⁴⁵⁶⁷⁸⁹⁰ ⁱ ₁₂₃₄₅₆₇₈₉₀ ⁻⁼⁺⁽⁾ⁿ‽ ₊₋₌₍₎ₓ
        var coefs = new List<string>();
        var signs = new List<string>();
        for (var i = (coefficients?.Length ?? 0) - 1; i >= 0; i--)
        {
            var value = coefficients[i];
            var powStr = i.ToString(format, formatProvider);
            powStr = powStr.Replace("1", "¹", StringComparison.OrdinalIgnoreCase);
            powStr = powStr.Replace("2", "²", StringComparison.OrdinalIgnoreCase);
            powStr = powStr.Replace("3", "³", StringComparison.OrdinalIgnoreCase);
            powStr = powStr.Replace("4", "⁴", StringComparison.OrdinalIgnoreCase);
            powStr = powStr.Replace("5", "⁵", StringComparison.OrdinalIgnoreCase);
            powStr = powStr.Replace("6", "⁶", StringComparison.OrdinalIgnoreCase);
            powStr = powStr.Replace("7", "⁷", StringComparison.OrdinalIgnoreCase);
            powStr = powStr.Replace("8", "⁸", StringComparison.OrdinalIgnoreCase);
            powStr = powStr.Replace("9", "⁹", StringComparison.OrdinalIgnoreCase);
            powStr = powStr.Replace("0", "⁰", StringComparison.OrdinalIgnoreCase);
            if (value != U.Zero)
            {
                var sign = (value < U.Zero) ? " - " : " + ";
                value = U.Abs(value);
                var valueString = value.ToString(format, formatProvider);
                if (i > 0)
                {
                    if (value == U.One)
                    {
                        valueString = "x";
                    }
                    else
                    {
                        valueString += "x";
                    }
                }

                if (i > 1)
                {
                    valueString += powStr;
                }

                signs.Add(sign);
                coefs.Add(valueString);
            }
        }
        if (signs.Count > 0)
        {
            signs[0] = (signs[0] == " + ") ? "" : "-";
        }

        var result = string.Empty;
        for (var i = 0; i < coefs.Count; i++)
        {
            result += signs[i] + coefs[i];
        }

        return result;
    }

    /// <summary>
    /// Gets the debugger display.
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private readonly string GetDebuggerDisplay() => ToString();
    #endregion
}
