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
using System.Linq;
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
    /// Internally the polynomial is represented by an array of the coefficients in reverse order.
    /// When accessed externally, the coefficients are generally in forward order.
    /// </remarks>
    /// <seealso cref="IEquatable{T}" />
    [DataContract, Serializable]
    [TypeConverter(typeof(StructConverter<Polynomial>))]
    [DebuggerDisplay("{ToString()}")]
    public class Polynomial
        : IEquatable<Polynomial>
    {
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

        #region Constants
        /// <summary>
        /// The array form of the Identity <see cref="Polynomial" />.
        /// </summary>
        public static readonly double[] Identity = { 1, 0 };
        #endregion

        #region Fields
        /// <summary>
        /// Cache for the real order degree value.
        /// </summary>
        private PolynomialDegree? degree;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> class.
        /// </summary>
        /// <param name="numbers">The numbers.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial(params double[] numbers)
        {
            Coefficients = numbers;
            Array.Reverse(Coefficients);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> class.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial((double A, double B) tuple)
            : this(tuple.A, tuple.B)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> class.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial((double A, double B, double C) tuple)
            : this(tuple.A, tuple.B, tuple.C)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> class.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial((double A, double B, double C, double D) tuple)
            : this(tuple.A, tuple.B, tuple.C, tuple.D)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> class.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial((double A, double B, double C, double D, double E) tuple)
            : this(tuple.A, tuple.B, tuple.C, tuple.D, tuple.E)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> class.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial((double A, double B, double C, double D, double E, double F) tuple)
            : this(tuple.A, tuple.B, tuple.C, tuple.D, tuple.E, tuple.F)
        { }

        ///// <summary>
        ///// Initializes a new instance of the <see cref="Polynomial"/> class.
        ///// </summary>
        ///// <param name="tuple">The tuple.</param>
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public Polynomial((double A, double B, double C, double D, double E, double F, double G) tuple)
        //    : this(tuple.A, tuple.B, tuple.C, tuple.D, tuple.E, tuple.F, tuple.G)
        //{ }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the coefficients.
        /// </summary>
        /// <value>
        /// The coefficients.
        /// </value>
        public double[] Coefficients { get; set; }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count => Coefficients.Length;

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
        /// Gets or sets a value indicating whether this instance is readonly.
        /// </summary>
        /// <value>
        ///   <see langword="true" /> if this instance is readonly; otherwise, <see langword="false" />.
        /// </value>
        internal bool IsReadonly { get; set; }
        #endregion

        #region Indexers
        /// <summary>
        /// Gets or sets the <see cref="double"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="double"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public double this[int index]
        {
            get { return Coefficients[index]; }
            set { Coefficients[index] = value; }
        }
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
        public static Polynomial operator +(Polynomial value) => value;

        /// <summary>
        /// Add an amount to both values in the <see cref="Polynomial" /> structs.
        /// </summary>
        /// <param name="value">The original value.</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns>
        /// The <see cref="Polynomial" />.
        /// </returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator +(Polynomial value, double addend) => addend + value;

        /// <summary>
        /// Add an amount to both values in the <see cref="Polynomial" /> structs.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns>
        /// The <see cref="Polynomial" />.
        /// </returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator +(double value, Polynomial addend)
        {
            var res = new double[addend.Count];
            Array.Copy(addend.Coefficients, res, addend.Count);
            res[0] += value;
            return new Polynomial { Coefficients = res, IsReadonly = addend.IsReadonly };
        }

        /// <summary>
        /// Add an amount to both values in the <see cref="Polynomial" /> structs.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns>
        /// The <see cref="Polynomial" />.
        /// </returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator +(Polynomial value, Polynomial addend)
        {
            var res = new double[Max(value.Count, addend.Count)];
            for (var i = 0; i < res.Length; i++)
            {
                double p = 0;
                if (i < value.Count)
                {
                    p += value.Coefficients[i];
                }

                if (i < addend.Count)
                {
                    p += addend.Coefficients[i];
                }

                res[i] = p;
            }
            return new Polynomial { Coefficients = res, IsReadonly = value.IsReadonly | addend.IsReadonly };
        }

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
        public static Polynomial operator -(Polynomial value)
        {
            var res = new double[value.Count];
            for (var i = 0; i < res.Length; i++)
            {
                res[i] = -value.Coefficients[i];
            }

            return new Polynomial { Coefficients = res, IsReadonly = value.IsReadonly };
        }

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
        public static Polynomial operator -(double value, Polynomial subend)
        {
            var res = new double[subend.Count];
            for (var i = 0; i < res.Length; i++)
            {
                res[i] = -subend.Coefficients[i];
            }

            res[0] += value;
            return new Polynomial { Coefficients = res, IsReadonly = subend.IsReadonly };
        }

        /// <summary>
        /// Subtract a <see cref="Polynomial" /> from another <see cref="Polynomial" /> class.
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
        public static Polynomial operator -(Polynomial value, Polynomial subend)
        {
            var res = new double[Max(value.Count, subend.Count)];
            for (var i = 0; i < res.Length; i++)
            {
                double p = 0;
                if (i < value.Count)
                {
                    p += value.Coefficients[i];
                }

                if (i < subend.Count)
                {
                    p -= subend.Coefficients[i];
                }

                res[i] = p;
            }

            return new Polynomial { Coefficients = res, IsReadonly = value.IsReadonly | subend.IsReadonly };
        }

        internal static object Bezier(double[] v) => throw new NotImplementedException();

        /// <summary>
        /// Scale a <see cref="Polynomial" /> by a value.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="factor">The factor value.</param>
        /// <returns>
        /// The <see cref="Polynomial" />.
        /// </returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator *(Polynomial value, double factor) => factor * value;

        /// <summary>
        /// Scale a <see cref="Polynomial" /> by a value.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="factor">The factor value.</param>
        /// <returns>
        /// The <see cref="Polynomial" />.
        /// </returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator *(double value, Polynomial factor)
        {
            var res = new double[factor.Count];
            for (var i = 0; i < res.Length; i++)
            {
                res[i] = value * factor.Coefficients[i];
            }

            return new Polynomial { Coefficients = res, IsReadonly = factor.IsReadonly };
        }

        /// <summary>
        /// Multiply two <see cref="Polynomial" />s together.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="factor">The factor value.</param>
        /// <returns>
        /// The <see cref="Polynomial" />.
        /// </returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator *(Polynomial value, Polynomial factor)
        {
            var res = new double[value.Count + factor.Count - 1];
            for (var i = 0; i < value.Count; i++)
            {
                for (var j = 0; j < factor.Count; j++)
                {
                    var mul = value.Coefficients[i] * factor.Coefficients[j];
                    res[i + j] += mul;
                }
            }

            return new Polynomial { Coefficients = res, IsReadonly = value.IsReadonly | factor.IsReadonly };
        }

        /// <summary>
        /// Divide a <see cref="Polynomial" /> by a value.
        /// </summary>
        /// <param name="divisor">The divisor value</param>
        /// <param name="dividend">The dividend to add.</param>
        /// <returns>
        /// The <see cref="Polynomial" />.
        /// </returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator /(Polynomial divisor, double dividend)
        {
            var res = new double[divisor.Count];
            for (var i = 0; i < res.Length; i++)
            {
                res[i] = divisor.Coefficients[i] / dividend;
            }

            return new Polynomial { Coefficients = res, IsReadonly = divisor.IsReadonly };
        }

        /// <summary>
        /// Divide a <see cref="Polynomial" /> by a value.
        /// </summary>
        /// <param name="divisor">The divisor value</param>
        /// <param name="dividend">The dividend to add.</param>
        /// <returns>
        /// The <see cref="Polynomial" />.
        /// </returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator /(double divisor, Polynomial dividend)
        {
            var res = new double[dividend.Count];
            for (var i = 0; i < res.Length; i++)
            {
                res[i] = divisor / dividend.Coefficients[i];
            }

            return new Polynomial { Coefficients = res, IsReadonly = dividend.IsReadonly };
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Polynomial left, Polynomial right) => EqualityComparer<Polynomial>.Default.Equals(left, right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
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

        ///// <summary>
        ///// Performs an implicit conversion from <see cref="System.ValueTuple{T, T, T, T, T, T, T}"/> to <see cref="Polynomial"/>.
        ///// </summary>
        ///// <param name="tuple">The tuple.</param>
        ///// <returns>
        ///// The result of the conversion.
        ///// </returns>
        //[DebuggerStepThrough]
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static implicit operator Polynomial((double A, double B, double C, double D, double E, double F, double G) tuple) => new Polynomial(tuple);
        #endregion

        #region Operator Backing Methods
        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <see langword="true" /> if the specified <see cref="object" /> is equal to this instance; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals([AllowNull] object obj) => Equals(obj as Polynomial);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Polynomial other) => other != null &&
                   EqualityComparer<double[]>.Default.Equals(Coefficients, other.Coefficients);
        #endregion

        #region Methods
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
            if (Coefficients is null || Coefficients.Length < 1)
            {
                return new Polynomial(0) { IsReadonly = IsReadonly };
            }

            // Get the real degree to skip any leading zero coefficients.
            var order = (int)(degree ??= RealOrder(epsilon)) + 1; /*Warning! Side effect!*/

            // Copy the remaining coefficients to a new array and return it.
            var coeffs = new double[order];
            Array.Copy(Coefficients, 0, coeffs, 0, order);
            return new Polynomial { Coefficients = coeffs, IsReadonly = IsReadonly };
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
                    if (Abs(Coefficients[i]) <= epsilon)
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
            return (PolynomialDegree)(Coefficients?.Length - pos ?? 0);
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
                    if (Coefficients is null)
                    {
                        return Array.Empty<double>();
                    }

                    return new double[] { Coefficients[0] };
                case PolynomialDegree.Linear:
                    return Operations.LinearRoots(Coefficients[1], Coefficients[0], epsilon).ToArray();
                case PolynomialDegree.Quadratic:
                    return Operations.QuadraticRoots(Coefficients[2], Coefficients[1], Coefficients[0], epsilon).ToArray();
                case PolynomialDegree.Cubic:
                    return Operations.CubicRoots(Coefficients[3], Coefficients[2], Coefficients[1], Coefficients[0], epsilon).ToArray();
                case PolynomialDegree.Quartic:
                    return Operations.QuarticRoots(Coefficients[4], Coefficients[3], Coefficients[2], Coefficients[1], Coefficients[0], epsilon).ToArray();
                case PolynomialDegree.Quintic:
                    return Operations.QuinticRoots(Coefficients[5], Coefficients[4], Coefficients[3], Coefficients[2], Coefficients[1], Coefficients[0], epsilon).ToArray();
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
                res[i - 1] = i * Coefficients[i];
            }

            return new Polynomial { Coefficients = res, IsReadonly = IsReadonly };
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
                result = (result * x) + Coefficients[i];
            }

            return result;
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

        public static Polynomial Plus(Polynomial item) => throw new NotImplementedException();

        public static Polynomial Add(Polynomial left, Polynomial right) => throw new NotImplementedException();

        public static Polynomial Negate(Polynomial item) => throw new NotImplementedException();

        public static Polynomial Subtract(Polynomial left, Polynomial right) => throw new NotImplementedException();

        public static Polynomial Multiply(Polynomial left, Polynomial right) => throw new NotImplementedException();

        public static Polynomial Divide(Polynomial left, Polynomial right) => throw new NotImplementedException();

        public Polynomial ToPolynomial() => throw new NotImplementedException();
        #endregion
    }
}