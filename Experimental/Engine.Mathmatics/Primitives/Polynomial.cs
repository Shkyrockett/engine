// <copyright file="Polynomial.cs" company="Shkyrockett" >
//     Copyright © 2014 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary>
//     Based on classes from https://github.com/superlloyd/Poly, http://www.kevlindev.com/geometry/2D/intersections/, and https://github.com/Pomax/bezierjs.
// </summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using static Engine.Mathematics;
using static Engine.Operations;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// A Polynomial representation of a series of numbers.
    /// </summary>
    /// <seealso cref="System.IEquatable{Engine.Polynomial}" />
    /// <remarks>
    /// <para>Internally the polynomial is represented by an array of the coefficients in reverse order.
    /// When accessed externally, the coefficients are generally in forward order.</para>
    /// </remarks>
    /// <seealso cref="IEquatable{T}" />
    [DataContract, Serializable]
    [TypeConverter(typeof(StructConverter<Polynomial>))]
    [DebuggerDisplay("{ToString()}")]
    public struct Polynomial
        : IEquatable<Polynomial>
    {
        #region Constants
        /// <summary>
        /// The array form of the Identity <see cref="Polynomial" />.
        /// </summary>
        public static readonly double[] Identity = { 1, 0 };
        #endregion

        #region Implementations
        /// <summary>
        /// An empty polynomial.
        /// </summary>
        public static readonly Polynomial Empty = new Polynomial(0);

        /// <summary>
        /// The T Identity polynomial.
        /// </summary>
        public static readonly Polynomial T = new Polynomial(Identity);

        /// <summary>
        /// One minus the T Identity polynomial.
        /// </summary>
        public static readonly Polynomial OneMinusT = 1d - T;
        #endregion

        #region Fields
        /// <summary>
        /// The coefficients of the polynomial in lowest degree to highest degree order.
        /// </summary>
        private double[] coefficients;

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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial(params double[] coefficients)
        {
            if (coefficients is null)
            {
                throw new ArgumentNullException(nameof(coefficients));
            }

            // If the coefficients array is empty this is an Empty polynomial, otherwise copy the coefficients over.
            // Reverse the coefficients so they are in order of degree smallest to largest.
            this.coefficients = (coefficients is null || coefficients.Length == 0)
                ? this.coefficients = new double[] { 0 }
                : coefficients.Reverse().ToArray();
            // Not initially read only.
            isReadonly = false;
            degree = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> struct.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial((double A, double B) tuple)
            : this(tuple.A, tuple.B)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> struct.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial((double A, double B, double C) tuple)
            : this(tuple.A, tuple.B, tuple.C)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> struct.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial((double A, double B, double C, double D) tuple)
            : this(tuple.A, tuple.B, tuple.C, tuple.D)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> struct.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial((double A, double B, double C, double D, double E) tuple)
            : this(tuple.A, tuple.B, tuple.C, tuple.D, tuple.E)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> struct.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial((double A, double B, double C, double D, double E, double F) tuple)
            : this(tuple.A, tuple.B, tuple.C, tuple.D, tuple.E, tuple.F)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> struct.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial((double A, double B, double C, double D, double E, double F, double G) tuple)
            : this(tuple.A, tuple.B, tuple.C, tuple.D, tuple.E, tuple.F, tuple.G)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> struct.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial((double A, double B, double C, double D, double E, double F, double G, double H) tuple)
            : this(tuple.A, tuple.B, tuple.C, tuple.D, tuple.E, tuple.F, tuple.G, tuple.H)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> struct.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial((double A, double B, double C, double D, double E, double F, double G, double H, double I) tuple)
            : this(tuple.A, tuple.B, tuple.C, tuple.D, tuple.E, tuple.F, tuple.G, tuple.H, tuple.I)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> struct.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial((double A, double B, double C, double D, double E, double F, double G, double H, double I, double J) tuple)
            : this(tuple.A, tuple.B, tuple.C, tuple.D, tuple.E, tuple.F, tuple.G, tuple.H, tuple.I, tuple.J)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> struct.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial((double A, double B, double C, double D, double E, double F, double G, double H, double I, double J, double K) tuple)
            : this(tuple.A, tuple.B, tuple.C, tuple.D, tuple.E, tuple.F, tuple.G, tuple.H, tuple.I, tuple.J, tuple.K)
        { }
        #endregion

        #region Indexers
        /// <summary>
        /// Gets or sets the coefficient at the given index.
        /// </summary>
        /// <value>
        /// The <see cref="double"/>.
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
        public double this[int index]
        {
            get
            {
                if (index < 0 || index >= coefficients.Length)
                {
                    return 0d;
                }

                return coefficients[coefficients.Length - 1 - index];
            }
            set
            {
                if (IsReadonly)
                {
                    throw new InvalidOperationException($"{nameof(Polynomial)} is Read-only.");
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
        /// The <see cref="double"/>.
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
        public double this[PolynomialTerm index]
        {
            get
            {
                if (index < 0 || (int)index >= coefficients.Length)
                {
                    return 0;
                }

                return coefficients[coefficients.Length - 1 - (int)index];
            }
            set
            {
                if (IsReadonly)
                {
                    throw new InvalidOperationException($"{nameof(Polynomial)} is Read-only.");
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
        /// The <see cref="double"/>.
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
        public double this[PolynomialDegree index]
        {
            get
            {
                if (index < 0 || (int)index >= coefficients.Length)
                {
                    return 0;
                }

                return coefficients[(int)index];
            }
            set
            {
                if (IsReadonly)
                {
                    throw new InvalidOperationException($"{nameof(Polynomial)} is Read-only.");
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
        /// If degree uninitialized look up the real order then cache it and return.
        /// </remarks>
        public PolynomialDegree Degree => degree ??= RealOrder(double.Epsilon);

        /// <summary>
        /// Gets the raw number of coefficients found in the polynomial, including any leading zero coefficients.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count => coefficients?.Length ?? 0;

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
            get { return isReadonly; }
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
        public double[] Coefficients
        {
            get { return coefficients.Reverse().ToArray(); }
            set
            {
                if (IsReadonly)
                {
                    throw new InvalidOperationException($"{nameof(Polynomial)} is Read-only.");
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
        public string Text => ToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator +(Polynomial value) => Plus(value);

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator +(Polynomial augend, double addend) => Add(augend, addend);

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator +(double augend, Polynomial addend) => Add(augend, addend);

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator +(Polynomial augend, Polynomial addend) => Add(augend, addend);

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator -(Polynomial value) => Negate(value);

        /// <summary>
        /// Subtract a <see cref="Polynomial" /> from a <see cref="double" /> value.
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator -(Polynomial value, double subend) => value + (-subend);

        /// <summary>
        /// Subtract a <see cref="double" /> from a <see cref="Polynomial" /> value.
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator -(double minuend, Polynomial subend) => Subtract(minuend, subend);

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator -(Polynomial minuend, Polynomial subend) => Subtract(minuend, subend);

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator *(Polynomial multiplicand, double multiplier) => Multiply(multiplicand, multiplier);

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator *(double multiplicand, Polynomial multiplier) => Multiply(multiplicand, multiplier);

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator *(Polynomial multiplicand, Polynomial multiplier) => Multiply(multiplicand, multiplier);

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator /(Polynomial dividend, double divisor) => Divide(dividend, divisor);

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator /(double dividend, Polynomial divisor) => Divide(dividend, divisor);

        /// <summary>
        /// Compares two <see cref="Polynomial"/> objects.
        /// The result specifies whether the values of the x and x
        /// values of the two <see cref="Polynomial"/> objects are equal.
        /// </summary>
        /// <param name="left">The left Polynomial.</param>
        /// <param name="right">The right Polynomial.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Polynomial left, Polynomial right) => left.Equals(right);

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Polynomial left, Polynomial right) => !(left == right);

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.ValueTuple{T, T}"/> to <see cref="Polynomial"/>.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Polynomial((double A, double B) tuple) => new Polynomial(tuple);

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.ValueTuple{T, T, T}"/> to <see cref="Polynomial"/>.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Polynomial((double A, double B, double C) tuple) => new Polynomial(tuple);

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.ValueTuple{T, T, T, T}"/> to <see cref="Polynomial"/>.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Polynomial((double A, double B, double C, double D) tuple) => new Polynomial(tuple);

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.ValueTuple{T, T, T, T, T}"/> to <see cref="Polynomial"/>.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Polynomial((double A, double B, double C, double D, double E) tuple) => new Polynomial(tuple);

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.ValueTuple{T, T, T, T, T, T}"/> to <see cref="Polynomial"/>.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Polynomial((double A, double B, double C, double D, double E, double F) tuple) => new Polynomial(tuple);

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.ValueTuple{T, T, T, T, T, T, T}"/> to <see cref="Polynomial"/>.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Polynomial((double A, double B, double C, double D, double E, double F, double G) tuple) => new Polynomial(tuple);

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.ValueTuple{T, T, T, T, T, T, T, T}"/> to <see cref="Polynomial"/>.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Polynomial((double A, double B, double C, double D, double E, double F, double G, double H) tuple) => new Polynomial(tuple);

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.ValueTuple{T, T, T, T, T, T, T, T}"/> to <see cref="Polynomial"/>.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Polynomial((double A, double B, double C, double D, double E, double F, double G, double H, double I) tuple) => new Polynomial(tuple);

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.ValueTuple{T, T, T, T, T, T, T, T}"/> to <see cref="Polynomial"/>.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Polynomial((double A, double B, double C, double D, double E, double F, double G, double H, double I, double J) tuple) => new Polynomial(tuple);

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.ValueTuple{T, T, T, T, T, T, T, T}"/> to <see cref="Polynomial"/>.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Polynomial((double A, double B, double C, double D, double E, double F, double G, double H, double I, double J, double K) tuple) => new Polynomial(tuple);
        #endregion

        #region Operator Backing Methods
        /// <summary>
        /// Pluses the specified item.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Plus(Polynomial value)
        {
            var res = new double[value.Count];
            for (var i = 0; i < res.Length; i++)
            {
                res[i] = +value.coefficients[i];
            }

            return new Polynomial { coefficients = res, isReadonly = value.isReadonly };
        }

        /// <summary>
        /// Adds the specified left.
        /// </summary>
        /// <param name="augend">The augend.</param>
        /// <param name="addend">The addend.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Add(Polynomial augend, double addend)
        {
            var res = new double[augend.Count];
            Array.Copy(augend.coefficients, res, augend.Count);
            res[0] += addend;
            return new Polynomial { coefficients = res, isReadonly = augend.isReadonly };
        }

        /// <summary>
        /// Adds the specified left.
        /// </summary>
        /// <param name="augend">The left.</param>
        /// <param name="addend">The right.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Add(double augend, Polynomial addend)
        {
            var res = new double[addend.Count];
            Array.Copy(addend.coefficients, res, addend.Count);
            res[0] += augend;
            return new Polynomial { coefficients = res, isReadonly = addend.isReadonly };
        }

        /// <summary>
        /// Adds the specified left.
        /// </summary>
        /// <param name="augend">The left.</param>
        /// <param name="addend">The right.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Add(Polynomial augend, Polynomial addend)
        {
            var res = new double[Max(augend.Count, addend.Count)];
            for (var i = 0; i < res.Length; i++)
            {
                double p = 0;
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
            return new Polynomial { coefficients = res, isReadonly = augend.isReadonly | addend.isReadonly };
        }

        /// <summary>
        /// Negates the specified item.
        /// </summary>
        /// <param name="value">The item.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Negate(Polynomial value)
        {
            var res = new double[value.Count];
            for (var i = 0; i < res.Length; i++)
            {
                res[i] = -value.coefficients[i];
            }

            return new Polynomial { coefficients = res, isReadonly = value.isReadonly };
        }

        /// <summary>
        /// Subtracts the specified left.
        /// </summary>
        /// <param name="minuend">The left.</param>
        /// <param name="subend">The right.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Subtract(Polynomial minuend, double subend)
        {
            var res = new double[minuend.Count];
            for (var i = 0; i < res.Length; i++)
            {
                res[i] = -minuend.coefficients[i];
            }

            res[0] += subend;
            return new Polynomial { coefficients = res, isReadonly = minuend.isReadonly };
        }

        /// <summary>
        /// Subtracts the specified left.
        /// </summary>
        /// <param name="minuend">The left.</param>
        /// <param name="subend">The right.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Subtract(double minuend, Polynomial subend)
        {
            var res = new double[subend.Count];
            for (var i = 0; i < res.Length; i++)
            {
                res[i] = -subend.coefficients[i];
            }

            res[0] += minuend;
            return new Polynomial { coefficients = res, isReadonly = subend.isReadonly };
        }

        /// <summary>
        /// Subtracts the specified left.
        /// </summary>
        /// <param name="minuend">The left.</param>
        /// <param name="subend">The right.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Subtract(Polynomial minuend, Polynomial subend)
        {
            var res = new double[Max(minuend.Count, subend.Count)];
            for (var i = 0; i < res.Length; i++)
            {
                double p = 0;
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

            return new Polynomial { coefficients = res, isReadonly = minuend.isReadonly | subend.isReadonly };
        }

        /// <summary>
        /// Multiplies the specified left.
        /// </summary>
        /// <param name="multiplicand">The left.</param>
        /// <param name="multiplier">The right.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Multiply(Polynomial multiplicand, double multiplier)
        {
            var res = new double[multiplicand.Count];
            for (var i = 0; i < res.Length; i++)
            {
                res[i] = multiplicand.coefficients[i] * multiplier;
            }

            return new Polynomial { coefficients = res, isReadonly = multiplicand.isReadonly };
        }

        /// <summary>
        /// Multiplies the specified left.
        /// </summary>
        /// <param name="multiplicand">The left.</param>
        /// <param name="multiplier">The right.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Multiply(double multiplicand, Polynomial multiplier)
        {
            var res = new double[multiplier.Count];
            for (var i = 0; i < res.Length; i++)
            {
                res[i] = multiplicand * multiplier.coefficients[i];
            }

            return new Polynomial { coefficients = res, isReadonly = multiplier.isReadonly };
        }

        /// <summary>
        /// Multiplies the specified left.
        /// </summary>
        /// <param name="multiplicand">The left.</param>
        /// <param name="multiplier">The right.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Multiply(Polynomial multiplicand, Polynomial multiplier)
        {
            var res = new double[multiplicand.Count + multiplier.Count - 1];
            for (var i = 0; i < multiplicand.Count; i++)
            {
                for (var j = 0; j < multiplier.Count; j++)
                {
                    var mul = multiplicand.coefficients[i] * multiplier.coefficients[j];
                    res[i + j] += mul;
                }
            }

            return new Polynomial { coefficients = res, isReadonly = multiplicand.isReadonly | multiplier.isReadonly };
        }

        /// <summary>
        /// Divides the specified left.
        /// </summary>
        /// <param name="dividend">The left.</param>
        /// <param name="divisor">The right.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Divide(Polynomial dividend, double divisor)
        {
            var res = new double[dividend.Count];
            for (var i = 0; i < res.Length; i++)
            {
                res[i] = dividend.coefficients[i] / divisor;
            }

            return new Polynomial { coefficients = res, isReadonly = dividend.isReadonly };
        }

        /// <summary>
        /// Divides the specified left.
        /// </summary>
        /// <param name="dividend">The left.</param>
        /// <param name="divisor">The right.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Divide(double dividend, Polynomial divisor)
        {
            var res = new double[divisor.Count];
            for (var i = 0; i < res.Length; i++)
            {
                res[i] = dividend / divisor.coefficients[i];
            }

            return new Polynomial { coefficients = res, isReadonly = divisor.isReadonly };
        }

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([AllowNull] object obj) => obj is Polynomial d && Equals(d);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals([AllowNull] Polynomial other)
        {
            var t1 = this.Trim();
            var t2 = other.Trim();

            if (t1.Count != t2.Count)
            {
                return false;
            }

            for (var i = 0; i < t1.Count; i++)
            {
                if (Abs(t1.coefficients[i] - t2.coefficients[i]) > Epsilon)
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial FromValueTuple((double A, double B) tuple) => new Polynomial(tuple);

        /// <summary>
        /// Froms the value tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial FromValueTuple((double A, double B, double C) tuple) => new Polynomial(tuple);

        /// <summary>
        /// Froms the value tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial FromValueTuple((double A, double B, double C, double D) tuple) => new Polynomial(tuple);

        /// <summary>
        /// Froms the value tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial FromValueTuple((double A, double B, double C, double D, double E) tuple) => new Polynomial(tuple);

        /// <summary>
        /// Froms the value tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial FromValueTuple((double A, double B, double C, double D, double E, double F) tuple) => new Polynomial(tuple);

        /// <summary>
        /// Froms the value tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial FromValueTuple((double A, double B, double C, double D, double E, double F, double G) tuple) => new Polynomial(tuple);

        /// <summary>
        /// Froms the value tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial FromValueTuple((double A, double B, double C, double D, double E, double F, double G, double H) tuple) => new Polynomial(tuple);

        /// <summary>
        /// Froms the value tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial FromValueTuple((double A, double B, double C, double D, double E, double F, double G, double H, double I) tuple) => new Polynomial(tuple);

        /// <summary>
        /// Froms the value tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial FromValueTuple((double A, double B, double C, double D, double E, double F, double G, double H, double I, double J) tuple) => new Polynomial(tuple);

        /// <summary>
        /// Froms the value tuple.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial FromValueTuple((double A, double B, double C, double D, double E, double F, double G, double H, double I, double J, double K) tuple) => new Polynomial(tuple);
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial Trim(double epsilon = double.Epsilon)
        {
            Contract.Ensures(Contract.Result<Polynomial>() != null);
            Contract.EndContractBlock();

            // If there are no coefficients then this is a Monomial of 0.
            if (coefficients is null || coefficients.Length < 1)
            {
                return new Polynomial(0) { isReadonly = isReadonly };
            }

            // Get the real degree to skip any leading zero coefficients.
            var order = (int)(degree ??= RealOrder(epsilon)) + 1; /*Warning! Side effect!*/

            // Copy the remaining coefficients to a new array and return it.
            var coeffs = new double[order];
            Array.Copy(coefficients, 0, coeffs, 0, order);
            return new Polynomial { coefficients = coeffs, isReadonly = isReadonly };
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial Derivate()
        {
            Contract.Ensures(Contract.Result<Polynomial>() != null);
            Contract.EndContractBlock();

            var order = (int)Degree; /* Get the real degree to skip any leading zero coefficients. */
            var res = new double[Max(1, order)];
            for (var i = 1; i < order + 1; i++)
            {
                res[i - 1] = i * coefficients[i];
            }

            return new Polynomial { coefficients = res, isReadonly = isReadonly };
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial Normalize(double epsilon = Epsilon)
        {
            //Contract.Ensures(Contract.Result<Polynomial>().Coefficients != null);
            //Contract.EndContractBlock();

            var order = (int)Degree; /* Get the real degree to skip any leading zero coefficients. */
            var high = coefficients[order];
            var res = new double[order + 1];
            for (var i = 0; i < res.Length; i++)
            {
                if (Abs(coefficients[i]) > epsilon)
                {
                    res[i] = coefficients[i] / high;
                }
            }

            return new Polynomial { coefficients = res, isReadonly = isReadonly };
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial Integrate(double term0 = 0)
        {
            Contract.Ensures(Contract.Result<Polynomial>() != null);
            //Contract.EndContractBlock();

            //ToDo: Figure out if the real order should be used or if the leading zero coefficients are needed.

            //var order = (int)Degree; /* Get the real degree to skip any leading zero coefficients. */
            var order = Count;
            var res = new double[order + 1];
            res[0] = term0;
            for (var i = 0; i < order; i++)
            {
                res[i + 1] = coefficients[i] / (i + 1);
            }

            return new Polynomial { coefficients = res, isReadonly = isReadonly };
        }

        /// <summary>
        /// Raises a Polynomial to a <see cref="Power" />.
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial Power(int n)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(n)} cannot be negative.");
            }

            if (double.IsNaN(n))
            {
                throw new ArithmeticException($"{nameof(Evaluate)}: parameter {nameof(n)} must be a number");
            }

            Contract.Ensures(Contract.Result<Polynomial>() != null);
            //Contract.EndContractBlock();

            var order = (int)Degree; /* Get the real degree to skip any leading zero coefficients. */
            var res = new double[(order * n) + 1];
            var tmp = new double[(order * n) + 1];
            res[0] = 1;
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
                    tmp[i] = 0;
                }
            }

            return new Polynomial { coefficients = res, isReadonly = isReadonly };
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Evaluate(double x)
        {
            if (double.IsNaN(x))
            {
                throw new ArithmeticException($"{nameof(Evaluate)}: parameter {nameof(x)} must be a number");
            }

            var order = (int)Degree;
            var result = 0d;

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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex Evaluate(Complex x)
        {
            Contract.Ensures(Contract.Result<Complex>() != null);

            var result = Complex.Zero;

            for (var i = (int)Degree; i >= 0; i--)
            {
                result = (result * x) + coefficients[i];
            }

            return result;
        }

        /// <summary>
        /// Computes value of the differentiated polynomial at x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        /// <acknowledgment>
        /// https://github.com/thelonious/kld-polynomial
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Differentiate(double x) => Derivate().Evaluate(x);

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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (double minY, double maxY) MinMax(double minX = 0, double maxX = 1)
        {
            var roots = Derivate().Trim().Roots()
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

            return (minY, maxY);
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PolynomialDegree RealOrder(double epsilon = double.Epsilon)
        {
            var pos = 1;

            // Monomial can be a zero constant, skip them and check the rest.
            if (Count > 1)
            {
                // Count the number of leading zeros. Because the coefficients array is reversed, start at the end because there should generally be fewer leading zeros than other coefficients.
                for (var i = Count - 1; i >= 1 /* Monomials can be 0. */; i--)
                {
                    // Check if coefficient is a leading zero.
                    if (Abs(coefficients[i]) <= epsilon)
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial[] GetStandardBase(int degree)
        {
            if (degree < 1)
            {
                throw new ArgumentException($"{nameof(degree)} expected to be greater than zero.");
            }

            var buf = new Polynomial[degree];

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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Term(PolynomialDegree degree, double coefficient = 1)
        {
            if (degree < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(degree), $"{nameof(degree)} cannot be negative.");
            }

            var d = (int)degree;
            var res = new double[d + 1];
            res[d] = coefficient;
            return new Polynomial { coefficients = res };
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Interpolate(params double[] ys)
        {
            if (ys is null || ys.Length < 2)
            {
                throw new ArgumentNullException($"{nameof(ys)}: At least 2 different points must be given");
            }

            var res = new Polynomial();
            for (var i = 0; i < ys.Length; i++)
            {
                var e = new Polynomial(1);
                for (var j = 0; j < ys.Length; j++)
                {
                    if (j == i)
                    {
                        continue;
                    }

                    e *= new Polynomial(1, -j) / (i - j); // Don't know if 1,-j should be swapped.
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Monomial(PolynomialDegree degree)
        {
            Contract.Ensures(Contract.Result<Polynomial>() != null);

            if (degree == 0)
            {
                return new Polynomial(1);
            }

            var d = (int)degree;
            var coeffs = new double[d + 1];

            for (var i = 0; i < d; i++)
            {
                coeffs[i] = 0d;
            }

            coeffs[d] = 1d;
            return new Polynomial { coefficients = coeffs };
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
        /// The <see cref="double" />.
        /// </returns>
        /// <acknowledgment>
        /// https://github.com/thelonious/kld-polynomial
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double? Bisection(double min, double max, double epsilon = double.Epsilon)
        {
            const double accuracy = 6;
            var minValue = Evaluate(min);
            var maxValue = Evaluate(max);
            double? result = null;

            if (Abs(minValue) <= epsilon)
            {
                return min;
            }
            else if (Abs(maxValue) <= epsilon)
            {
                return max;
            }
            else if (minValue * maxValue <= 0)
            {
                var tmp1 = Log(max - min);
                var tmp2 = LogTen * accuracy;
                var iters = Ceiling((tmp1 + tmp2) * InverseLogTwo);
                for (var i = 0; i < iters; i++)
                {
                    result = OneHalf * (min + max);
                    var value = Evaluate(result.Value);
                    if (Abs(value) <= epsilon)
                    {
                        break;
                    }
                    if (value * minValue < 0)
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<double> RealOrComplexRoots(double epsilon = Epsilon)
        {
            if (CanSolveRealRoots)
            {
                return Roots();
            }

            return ComplexRoots()
                .Where(c => Abs(c.Imaginary) < epsilon)
                .Select(c => c.Real);
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex[] ComplexRoots(double epsilon = Epsilon)
        {
            var poly = Normalize();
            if (poly.Count == 1)
            {
                return Array.Empty<Complex>();
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
                    if (Abs(c.Real) > epsilon || Abs(c.Imaginary) > epsilon)
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
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] RootsInInterval(double min = 0, double max = 1, double epsilon = double.Epsilon)
        {
            var roots = new HashSet<double>();
            double? root;
            if (Degree == PolynomialDegree.Linear)
            {
                root = Bisection(min, max, epsilon);
                if (root != null)
                {
                    roots.Add(root.Value);
                }
            }
            else
            {
                // get roots of derivative
                var deriv = Derivate();
                var droots = deriv.Count == 1 && deriv[0] == 0 ? Array.Empty<double>() : deriv.RootsInInterval(min, max, epsilon);
                if (droots.Length > 0)
                {
                    root = Bisection(min, droots[0], epsilon);
                    if (root != null)
                    {
                        roots.Add(root.Value);
                    }
                    // Find root on [droots[i],droots[i+1]] for 0 <= i <= count-2
                    for (var i = 0; i <= droots.Length - 2; i++)
                    {
                        root = Bisection(droots[i], droots[i + 1], epsilon);
                        if (root != null)
                        {
                            roots.Add(root.Value);
                        }
                    }
                    // Find root on [droots[count-1],xmax]
                    root = Bisection(droots[^1], max, epsilon);
                    if (root != null)
                    {
                        roots.Add(root.Value);
                    }
                }
                else
                {
                    // Polynomial is monotone on [min,max], has at most one root
                    root = Bisection(min, max, epsilon);
                    if (root != null)
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] Roots(double epsilon = double.Epsilon)
        {
            switch (Degree)
            {
                case PolynomialDegree.Constant:
                    if (coefficients is null)
                    {
                        return Array.Empty<double>();
                    }

                    return new double[] { coefficients[0] };
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
                    return Array.Empty<double>();
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => HashCode.Combine(Coefficients);

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Polynomial" /> struct.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => ToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Polynomial" /> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider) => ToString(string.Empty /* format string */, provider /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Polynomial" /> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
        {
            // ¹²³⁴⁵⁶⁷⁸⁹⁰ ⁱ ₁₂₃₄₅₆₇₈₉₀ ⁻⁼⁺⁽⁾ⁿ‽ ₊₋₌₍₎ₓ
            var coefs = new List<string>();
            var signs = new List<string>();
            for (var i = (coefficients?.Length ?? 0) - 1; i >= 0; i--)
            {
                var value = coefficients[i];
                var powStr = i.ToString(format, provider);
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
                if (value != 0)
                {
                    var sign = (value < 0) ? " - " : " + ";
                    value = Abs(value);
                    var valueString = value.ToString(format, provider);
                    if (i > 0)
                    {
                        if (value == 1)
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
        #endregion
    }
}