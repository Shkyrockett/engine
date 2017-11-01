// <copyright file="Polynomial.cs" company="Shkyrockett" >
//     Copyright © 2014 - 2017 Shkyrockett. All rights reserved.
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
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using static Engine.Maths;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// A Polynomial representation of a series of numbers.
    /// </summary>
    /// <remarks>
    /// Internally the polynomial is represented by an array of the coefficients in reverse order. 
    /// When accessed externally, the coefficients are generally in forward order.
    /// </remarks>
    [DataContract, Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public struct Polynomial
    {
        #region Constants

        /// <summary>
        /// The array form of the Identity <see cref="Polynomial"/>.
        /// </summary>
        public static readonly double[] Identity = new double[] { 1, 0 };

        /// <summary>
        /// The Bisection accuracy.
        /// </summary>
        private const double accuracy = 6;

        #endregion

        #region Implementations

        /// <summary>
        /// An empty polynomial.
        /// </summary>
        public static Polynomial Empty = new Polynomial(0);

        /// <summary>
        /// The T Identity polynomial.
        /// </summary>
        public static readonly Polynomial T = new Polynomial(Identity);

        /// <summary>
        /// One minus the T Identity polynomial.
        /// </summary>
        public static readonly Polynomial OneMinusT = 1 - T;

        #endregion

        #region Fields

        /// <summary>
        /// The coefficients of the polynomial in lowest degree to highest degree order.
        /// </summary>
        private double[] coefficients;

        /// <summary>
        /// Semaphore indicating whether the polynomial can be edited.
        /// </summary>
        bool isReadonly;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> class using left to right letter order, descending in degrees.
        /// </summary>
        /// <param name="coefficients">The coefficients of the polynomial.</param>
        /// <remarks>
        /// While the coefficients are entered in left to right letter order, they are 
        /// stored in degree order to simplify operations on <see cref="Polynomial"/> structs.
        /// </remarks>
        [DebuggerStepThrough]
        public Polynomial(params double[] coefficients)
        {
            // If the coefficients array is empty this is an Empty polynomial, otherwise copy the coefficients over.
            // Reverse the coefficients so they are in order of degree smallest to largest.
            this.coefficients = (coefficients == null || coefficients.Length == 0)
                ? this.coefficients = new double[] { 0 }
                : coefficients.Reverse().ToArray();

            // Not initially read only.
            isReadonly = false;
        }

        #endregion

        #region Indexers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <remarks>
        /// The indexer is in highest degree to lowest format.
        /// </remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        public double this[int index]
        {
            get
            {
                if (index < 0)
                    throw new ArgumentOutOfRangeException();
                if (index > coefficients.Length)
                    return 0;
                return coefficients[(coefficients.Length - 1) - index];
            }
            set
            {
                if (IsReadonly)
                    throw new InvalidOperationException($"{nameof(Polynomial)} is Read-only.");
                if (index < 0 || index > coefficients.Length)
                    throw new ArgumentOutOfRangeException();
                coefficients[(coefficients.Length - 1) - index] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <remarks>
        /// The <see cref="PolynomialTerm"/> indexer is in highest degree to lowest format.
        /// </remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        public double this[PolynomialTerm index]
        {
            get
            {
                if (index < 0)
                    throw new ArgumentOutOfRangeException();
                if ((int)index >= coefficients.Length)
                    return 0;
                return coefficients[(coefficients.Length - 1) - (int)index];
            }
            set
            {
                if (IsReadonly)
                    throw new InvalidOperationException($"{nameof(Polynomial)} is Read-only.");
                if (index < 0 || (int)index > coefficients.Length)
                    throw new ArgumentOutOfRangeException();
                coefficients[(coefficients.Length - 1) - (int)index] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <remarks>
        /// The <see cref="PolynomialDegree"/> indexer is in lowest degree to highest format.
        /// </remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        public double this[PolynomialDegree index]
        {
            get
            {
                if (index < 0)
                    throw new ArgumentOutOfRangeException();
                if ((int)index >= coefficients.Length)
                    return 0;
                return coefficients[(int)index];
            }
            set
            {
                if (IsReadonly)
                    throw new InvalidOperationException($"{nameof(Polynomial)} is Read-only.");
                if (index < 0 || (int)index > coefficients.Length)
                    throw new ArgumentOutOfRangeException();
                coefficients[(int)index] = value;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the degree of the polynomial.
        /// </summary>
        /// <returns></returns>
        public PolynomialDegree Degree
            => (PolynomialDegree)(coefficients?.Length - 1 ?? 0);

        /// <summary>
        /// Gets the number of coefficients found in the polynomial.
        /// </summary>
        /// <returns></returns>
        public int Count
            => coefficients?.Length ?? 0;

        /// <summary>
        /// Gets a value indicating whether there are real roots for the polynomial.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        public bool CanSolveRealRoots
            => (RealOrder() <= 4);

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="Polynomial"/> struct is read only.
        /// Useful for class that want to expose internal value that must not change.
        /// </summary>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        public bool IsReadonly
        {
            get { return isReadonly; }
            set
            {
                if (IsReadonly)
                    return;
                if (value)
                    isReadonly = true;
            }
        }

#if DEBUG

        /// <summary>
        /// Gets or sets the coefficients of the polynomial from highest degree to lowest degree order.
        /// </summary>
        /// <remarks>
        /// This property presents the <see cref="Coefficients"/> in the reverse order than they are internally stored.
        /// </remarks>
        [TypeConverter(typeof(ArrayConverter))]
        public double[] Coefficients
        {
            get { return coefficients.Reverse().ToArray(); }
            set
            {
                if (IsReadonly)
                    throw new InvalidOperationException($"{nameof(Polynomial)} is Read-only.");
                coefficients = value.Reverse().ToArray();
            }
        }

        /// <summary>
        /// Gets a debug string that represents the text version of the <see cref="Polynomial"/>.
        /// </summary>
        public string Text => ToString();

#endif

        #endregion

        #region Operators

        /// <summary>
        /// Unary addition operator.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator +(Polynomial value)
            => value;

        /// <summary>
        /// Add an amount to both values in the <see cref="Polynomial"/> structs.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator +(Polynomial value, double addend)
            => addend + value;

        /// <summary>
        /// Add an amount to both values in the <see cref="Polynomial"/> structs.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator +(double value, Polynomial addend)
        {
            var res = new double[addend.Count];
            Array.Copy(addend.coefficients, res, addend.Count);
            res[0] += value;
            return new Polynomial() { coefficients = res, isReadonly = addend.isReadonly };
        }

        /// <summary>
        /// Add an amount to both values in the <see cref="Polynomial"/> structs.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="addend">The amount to add.</param>
        /// <returns></returns>
        /// <remarks></remarks>
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
                    p += value.coefficients[i];
                }

                if (i < addend.Count)
                {
                    p += addend.coefficients[i];
                }

                res[i] = p;
            }
            return new Polynomial() { coefficients = res, isReadonly = value.isReadonly | addend.isReadonly };
        }

        /// <summary>
        /// Unary subtraction operator.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <returns></returns>
        /// <remarks></remarks>
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
                res[i] = -value.coefficients[i];
            }

            return new Polynomial() { coefficients = res, isReadonly = value.isReadonly };
        }

        /// <summary>
        /// Subtract a <see cref="Polynomial"/> from a <see cref="double"/> value.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="subend">The amount to subtract.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator -(Polynomial value, double subend)
            => value + (-subend);

        /// <summary>
        /// Subtract a <see cref="double"/> from a <see cref="Polynomial"/> value.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="subend">The amount to subtract.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator -(double value, Polynomial subend)
        {
            var res = new double[subend.Count];
            for (var i = 0; i < res.Length; i++)
            {
                res[i] = -subend.coefficients[i];
            }

            res[0] += value;
            return new Polynomial() { coefficients = res, isReadonly = subend.isReadonly };
        }

        /// <summary>
        /// Subtract a <see cref="Polynomial"/> from another <see cref="Polynomial"/> class.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="subend">The amount to subtract.</param>
        /// <returns></returns>
        /// <remarks></remarks>
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
                    p += value.coefficients[i];
                }

                if (i < subend.Count)
                {
                    p -= subend.coefficients[i];
                }

                res[i] = p;
            }

            return new Polynomial() { coefficients = res, isReadonly = value.isReadonly | subend.isReadonly };
        }

        /// <summary>
        /// Scale a <see cref="Polynomial"/> by a value.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="factor">The factor value.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial operator *(Polynomial value, double factor)
            => factor * value;

        /// <summary>
        /// Scale a <see cref="Polynomial"/> by a value.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="factor">The factor value.</param>
        /// <returns></returns>
        /// <remarks></remarks>
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
                res[i] = value * factor.coefficients[i];
            }

            return new Polynomial() { coefficients = res, isReadonly = factor.isReadonly };
        }

        /// <summary>
        /// Multiply two <see cref="Polynomial"/>s together.
        /// </summary>
        /// <param name="value">The original value</param>
        /// <param name="factor">The factor value.</param>
        /// <returns></returns>
        /// <remarks></remarks>
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
                    var mul = value.coefficients[i] * factor.coefficients[j];
                    res[i + j] += mul;
                }
            }

            return new Polynomial() { coefficients = res, isReadonly = value.isReadonly | factor.isReadonly };
        }

        /// <summary>
        /// Divide a <see cref="Polynomial"/> by a value.
        /// </summary>
        /// <param name="divisor">The divisor value</param>
        /// <param name="dividend">The dividend to add.</param>
        /// <returns></returns>
        /// <remarks></remarks>
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
                res[i] = divisor.coefficients[i] / dividend;
            }

            return new Polynomial() { coefficients = res, isReadonly = divisor.isReadonly };
        }

        /// <summary>
        /// Compares two <see cref="Polynomial"/> objects.
        /// The result specifies whether the values of the <see cref="X"/> and <see cref="Y"/>
        /// values of the two <see cref="Polynomial"/> objects are equal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Polynomial left, Polynomial right)
            => Equals(left, right);

        /// <summary>
        /// Compares two <see cref="Polynomial"/> objects.
        /// The result specifies whether the values of the <see cref="X"/> or <see cref="Y"/>
        /// values of the two <see cref="Polynomial"/> objects are unequal.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Polynomial left, Polynomial right)
            => !Equals(left, right);

        /// <summary>
        /// Implicit conversion from a binomial tuple to a polynomial.
        /// </summary>
        /// <param name="tuple"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Polynomial((double a, double b) tuple)
            => new Polynomial(tuple.a, tuple.b);

        /// <summary>
        /// Implicit conversion from a trinomial tuple to a polynomial.
        /// </summary>
        /// <param name="tuple"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Polynomial((double a, double b, double c) tuple)
            => new Polynomial(tuple.a, tuple.b, tuple.c);

        /// <summary>
        /// Implicit conversion from a quadrinomial tuple to a polynomial.
        /// </summary>
        /// <param name="tuple"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Polynomial((double a, double b, double c, double d) tuple)
            => new Polynomial(tuple.a, tuple.b, tuple.c, tuple.d);

        /// <summary>
        /// Implicit conversion from a quintinomial tuple to a polynomial.
        /// </summary>
        /// <param name="tuple"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Polynomial((double a, double b, double c, double d, double e) tuple)
            => new Polynomial(tuple.a, tuple.b, tuple.c, tuple.d, tuple.e);

        /// <summary>
        /// Implicit conversion from a sextomial tuple to a polynomial.
        /// </summary>
        /// <param name="tuple"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Polynomial((double a, double b, double c, double d, double e, double f) tuple)
            => new Polynomial(tuple.a, tuple.b, tuple.c, tuple.d, tuple.e, tuple.f);

        /// <summary>
        /// Implicit conversion from a septomial tuple to a polynomial.
        /// </summary>
        /// <param name="tuple"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Polynomial((double a, double b, double c, double d, double e, double f, double g) tuple)
            => new Polynomial(tuple.a, tuple.b, tuple.c, tuple.d, tuple.e, tuple.f, tuple.g);

        /// <summary>
        /// Implicit conversion from a octomial tuple to a polynomial.
        /// </summary>
        /// <param name="tuple"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Polynomial((double a, double b, double c, double d, double e, double f, double g, double h) tuple)
            => new Polynomial(tuple.a, tuple.b, tuple.c, tuple.d, tuple.e, tuple.f, tuple.g, tuple.h);

        /// <summary>
        /// Implicit conversion from a nonomial tuple to a polynomial.
        /// </summary>
        /// <param name="tuple"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Polynomial((double a, double b, double c, double d, double e, double f, double g, double h, double i) tuple)
            => new Polynomial(tuple.a, tuple.b, tuple.c, tuple.d, tuple.e, tuple.f, tuple.g, tuple.h, tuple.i);

        /// <summary>
        /// Implicit conversion from a decomial tuple to a polynomial.
        /// </summary>
        /// <param name="tuple"></param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Polynomial((double a, double b, double c, double d, double e, double f, double g, double h, double i, double j) tuple)
            => new Polynomial(tuple.a, tuple.b, tuple.c, tuple.d, tuple.e, tuple.f, tuple.g, tuple.h, tuple.i, tuple.j);

        #endregion

        #region Operations

        /// <summary>
        /// Trim off any leading zero coefficient terms from the Polynomial.
        /// </summary>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns>Returns a <see cref="Polynomial"/> with any leading zero coefficient terms removed.</returns>
        /// <acknowledgment>
        /// A hodge-podge method based on Simplify from of: http://www.kevlindev.com/
        /// and Trim from: https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial Trim(double epsilon = Epsilon)
        {
            var pos = 0;

            // Count the number of leading zeros. Because the coefficients array is reversed, start at the end.
            for (var i = Count - 1; i >= 0; i--)
            {
                if (Abs(this.coefficients[i]) <= epsilon)
                    pos++;
                else
                    break;
            }

            // Copy the remaining coefficients to a new array and return it.
            var coefficients = new double[Count - pos];
            Array.Copy(this.coefficients, 0, coefficients, 0, Count - pos);
            return new Polynomial() { coefficients = coefficients, isReadonly = this.isReadonly };
        }

        /// <summary>
        /// Find the derivative polynomial of a <see cref="Polynomial"/>.
        /// </summary>
        /// <returns>Returns a new <see cref="Polynomial"/> struct calculated as the derivative of the source <see cref="Polynomial"/>.</returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial Derivate()
        {
            var res = new double[Max(1, Count - 1)];

            for (var i = 1; i < Count; i++)
            {
                res[i - 1] = i * coefficients[i];
            }

            return new Polynomial() { coefficients = res, isReadonly = this.isReadonly };
        }

        /// <summary>
        /// Normalizes a <see cref="Polynomial"/>.
        /// </summary>
        /// <returns>Returns a normalized <see cref="Polynomial"/> calculated from the source <see cref="Polynomial"/>.</returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial Normalize(double epsilon = Epsilon)
        {
            var order = 0;
            double high = 1;
            for (var i = 0; i < Count; i++)
            {
                if (Abs(coefficients[i]) > epsilon)
                {
                    order = i;
                    high = coefficients[i];
                }
            }

            var res = new double[order + 1];
            for (var i = 0; i < res.Length; i++)
            {
                if (Abs(coefficients[i]) > epsilon)
                {
                    res[i] = coefficients[i] / high;
                }
            }

            return new Polynomial() { coefficients = res, isReadonly = this.isReadonly };
        }

        /// <summary>
        /// Integrates a <see cref="Polynomial"/>.
        /// </summary>
        /// <param name="term0"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial Integrate(double term0 = 0)
        {
            var res = new double[Count + 1];
            res[0] = term0;
            for (var i = 0; i < Count; i++)
            {
                res[i + 1] = coefficients[i] / (i + 1);
            }

            return new Polynomial() { coefficients = res, isReadonly = this.isReadonly };
        }

        /// <summary>
        /// Raises a Polynomial to a <see cref="Power"/>.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial Power(int n)
        {
            if (n < 0) throw new ArgumentOutOfRangeException($"{nameof(n)} cannot be negative.");
            if (Double.IsNaN(n)) throw new ArithmeticException($"{nameof(Evaluate)}: parameter {nameof(n)} must be a number");

            var order = (int)Degree;
            var res = new double[order * n + 1];
            var tmp = new double[order * n + 1];
            res[0] = 1;
            for (var pow = 0; pow < n; pow++)
            {
                var porder = pow * order;
                for (var i = 0; i <= order; i++)
                    for (var j = 0; j <= porder; j++)
                    {
                        var mul = coefficients[i] * res[j];
                        tmp[i + j] += mul;
                    }
                for (var i = 0; i <= porder + order; i++)
                {
                    res[i] = tmp[i];
                    tmp[i] = 0;
                }
            }

            return new Polynomial() { coefficients = res, isReadonly = this.isReadonly };
        }

        // ToDo: Figure out if Evaluate and Compute are the same.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/thelonious/kld-polynomial
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Evaluate(double x)
        {
            if (Double.IsNaN(x)) throw new ArithmeticException($"{nameof(Evaluate)}: parameter {nameof(x)} must be a number");

            var result = 0d;
            for (var i = (int)Degree; i >= 0; i--)
            {
                result = result * x + coefficients[i];
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Compute(double x)
        {
            if (Double.IsNaN(x)) throw new ArithmeticException($"{nameof(Compute)}: parameter {nameof(x)} must be a number");

            var result = 0d;
            var ncoef = 1d;
            for (var i = 0; i < Count; i++)
            {
                result += coefficients[i] * ncoef;
                ncoef *= x;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex Compute(Complex x)
        {
            Complex result = Complex.Zero;
            Complex ncoef = Complex.One;
            for (var i = Count - 1; i >= 0; i--)
            {
                result += coefficients[i] * ncoef;
                ncoef *= x;
            }

            return result;
        }

        /// <summary>
        /// Computes value of the differentiated polynomial at x.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/thelonious/kld-polynomial
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Differentiate(double x)
        {
            if (Double.IsNaN(x)) throw new ArithmeticException($"{nameof(Differentiate)}: parameter {nameof(x)} must be a number");

            return Derivate().Evaluate(x);
        }

        /// <summary>
        /// Finds the Minimum and Maximum values of the <see cref="Polynomial"/>.
        /// </summary>
        /// <param name="minX">The lower bound minimum.</param>
        /// <param name="maxX">The upper bound maximum.</param>
        /// <returns></returns>
        /// <remarks>
        /// Do not use this method on a polynomial that has been simplified or trimmed.
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

            (double minY, double maxY) = (minX, minX);

            foreach (var t in roots)
            {
                var y = Compute(t);
                if (first)
                {
                    first = false;
                    minY = maxY = y;
                }
                else
                {
                    if (y < minY) minY = y;
                    if (y > maxY) maxY = y;
                }
            }

            return (minY, maxY);
        }

        #endregion

        #region Factories

        /// <summary>
        /// 
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial[] GetStandardBase(int degree)
        {
            if (degree < 1)
                throw new ArgumentException($"{nameof(degree)} expected to be greater than zero.");

            Polynomial[] buf = new Polynomial[degree];

            for (var i = 0; i < degree; i++)
            {
                buf[i] = Monomial((PolynomialDegree)i);
            }

            return buf;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="degree"></param>
        /// <param name="coefficient"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Term(PolynomialDegree degree, double coefficient = 1)
        {
            if (degree < 0)
                throw new ArgumentOutOfRangeException($"{nameof(degree)} cannot be negative.");
            var d = (int)degree;
            var res = new double[d + 1];
            res[d] = coefficient;
            return new Polynomial() { coefficients = res };
        }

        /// <summary>
        /// Construct a polynomial P such as y[i] = P.Compute(i).
        /// </summary>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Interpolate(params double[] ys)
        {
            if (ys == null || ys.Length < 2)
                throw new ArgumentNullException($"{nameof(ys)}: At least 2 different points must be given");

            var res = new Polynomial();
            for (var i = 0; i < ys.Length; i++)
            {
                var e = new Polynomial(1);
                for (var j = 0; j < ys.Length; j++)
                {
                    if (j == i)
                        continue;
                    e *= new Polynomial(1, -j) / (i - j); // Don't know if 1,-j should be swapped.
                }
                res += ys[i] * e;
            }
            return res.Trim();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Monomial(PolynomialDegree degree)
        {
            if (degree == 0)
                return new Polynomial(1);

            var d = (int)degree;
            double[] coeffs = new double[d + 1];

            for (var i = 0; i < d; i++)
            {
                coeffs[i] = 0d;
            }

            coeffs[d] = 1d;
            return new Polynomial() { coefficients = coeffs };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Bezier(params double[] values)
        {
            if (values == null || values.Length < 1)
                throw new ArgumentNullException("At least 2 different points must be given");

            return Bezier(0, values.Length - 1, values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Bezier(int from, int to, double[] values)
            => (from == to)
            ? new Polynomial(values[from])
            : OneMinusT * Bezier(from, to - 1, values) + T * Bezier(from + 1, to, values);

        /// <summary>
        /// Interpolate the polynomial of a Linear Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Linear(double a, double b)
            => OneMinusT * a + T * b;

        /// <summary>
        /// Interpolate the polynomial of a Quadratic Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Quadratic(double a, double b, double c)
            => OneMinusT * Linear(a, b) + T * Linear(b, c);

        /// <summary>
        /// Interpolate the polynomial of a Cubic Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Cubic(double a, double b, double c, double d)
            => OneMinusT * Quadratic(a, b, c) + T * Quadratic(b, c, d);

        /// <summary>
        /// Interpolate the polynomial of a Quartic Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Quartic(double a, double b, double c, double d, double e)
            => OneMinusT * Cubic(a, b, c, d) + T * Cubic(b, c, d, e);

        /// <summary>
        /// Interpolate the polynomial of a Quintic Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Quintic(double a, double b, double c, double d, double e, double f)
            => OneMinusT * Quartic(a, b, c, d, e) + T * Quartic(b, c, d, e, f);

        /// <summary>
        /// Interpolate the polynomial of a Sextic Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Sextic(double a, double b, double c, double d, double e, double f, double g)
            => OneMinusT * Quintic(a, b, c, d, e, f) + T * Quintic(b, c, d, e, f, g);

        /// <summary>
        /// Interpolate the polynomial of a Septic Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Septic(double a, double b, double c, double d, double e, double f, double g, double h)
            => OneMinusT * Sextic(a, b, c, d, e, f, g) + T * Sextic(b, c, d, e, f, g, h);

        /// <summary>
        /// Interpolate the polynomial of a Octic Bezier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <param name="h"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Octic(double a, double b, double c, double d, double e, double f, double g, double h, double i)
            => OneMinusT * Septic(a, b, c, d, e, f, g, h) + T * Septic(b, c, d, e, f, g, h, i);

        #endregion

        //#region Serialization

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerializing()]
        //private void OnSerializing(StreamingContext context)
        //{
        //    // Assert("This value went into the data file during serialization.");
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="context"></param>
        //[OnSerialized()]
        //private void OnSerialized(StreamingContext context)
        //{
        //    // Assert("This value was reset after serialization.");
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserializing()]
        //private void OnDeserializing(StreamingContext context)
        //{
        //    // Assert("This value was set during deserialization");
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="context"></param>
        //[OnDeserialized()]
        //private void OnDeserialized(StreamingContext context)
        //{
        //    // Assert("This value was set after deserialization.");
        //}

        //#endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/thelonious/kld-polynomial
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double? Bisection(double min, double max, double epsilon = Epsilon)
        {
            var minValue = Evaluate(min);
            var maxValue = Evaluate(max);
            double? result = null;
            if (Abs(minValue) <= epsilon)
                result = min;
            else if (Abs(maxValue) <= epsilon)
                result = max;
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
                        maxValue = value;
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
        /// <returns></returns>
        /// <acknowledgment>
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<double> RealOrComplexRoots(double epsilon = Epsilon)
        {
            if (CanSolveRealRoots)
                return Roots();
            return ComplexRoots()
                .Where(c => Abs(c.Imaginary) < epsilon)
                .Select(c => c.Real);
        }

        /// <summary>
        /// This method use the Durand-Kerner aka Weierstrass algorithm to find approximate root of this polynomial.
        /// </summary>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// http://en.wikipedia.org/wiki/Durand%E2%80%93Kerner_method
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex[] ComplexRoots(double epsilon = Epsilon)
        {
            var p = Normalize();
            if (p.coefficients.Length == 1) return new Complex[0];

            Complex x0 = 1;
            Complex xMul = 0.4 + 0.9 * Complex.ImaginaryOne;
            var R0 = new Complex[p.coefficients.Length - 1];
            for (var i = 0; i < R0.Length; i++)
            {
                R0[i] = x0;
                x0 *= xMul;
            }

            var R1 = new Complex[p.coefficients.Length - 1];
            Complex divider(int i)
            {
                Complex div = 1;
                for (var j = 0; j < R0.Length; j++)
                {
                    if (j == i) continue;
                    div *= R0[i] - R0[j];
                }
                return div;
            }
            void step()
            {
                for (var i = 0; i < R0.Length; i++)
                {
                    R1[i] = R0[i] - p.Compute(R0[i]) / divider(i);
                }
            }
            bool closeEnough()
            {
                for (var i = 0; i < R0.Length; i++)
                {
                    var c = R0[i] - R1[i];
                    if (Abs(c.Real) > epsilon || Abs(c.Imaginary) > epsilon) return false;
                }
                return true;
            }
            var close = false;
            do
            {
                step();
                close = closeEnough();

                var tmp = R0;
                R0 = R1;
                R1 = tmp;
            }
            while (!close);

            return R0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/geometry/2D/intersections/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] RootsInInterval(double min = 0, double max = 1, double epsilon = Epsilon)
        {
            var roots = new List<double>();
            double? root;
            if (Degree == PolynomialDegree.Linear)
            {
                root = Bisection(min, max, epsilon);
                if (root != null) roots.Add(root.Value);
            }
            else
            {
                // get roots of derivative
                var deriv = Derivate();
                var droots = deriv.RootsInInterval(min, max, epsilon);
                if (droots.Length > 0)
                {
                    root = Bisection(min, droots[0], epsilon);
                    if (root != null) roots.Add(root.Value);
                    // Find root on [droots[i],droots[i+1]] for 0 <= i <= count-2
                    for (var i = 0; i <= droots.Length - 2; i++)
                    {
                        root = Bisection(droots[i], droots[i + 1], epsilon);
                        if (root != null) roots.Add(root.Value);
                    }
                    // Find root on [droots[count-1],xmax]
                    root = Bisection(droots[droots.Length - 1], max, epsilon);
                    if (root != null) roots.Add(root.Value);
                }
                else
                {
                    // Polynomial is monotone on [min,max], has at most one root
                    root = Bisection(min, max, epsilon);
                    if (root != null) roots.Add(root.Value);
                }
            }

            return roots.ToArray();
        }

        /// <summary>
        /// Find the Roots of up to Quintic degree <see cref="Polynomial"/>s.
        /// </summary>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.kevlindev.com/geometry/2D/intersections/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] Roots(double epsilon = Epsilon)
        {
            switch (Degree)
            {
                case PolynomialDegree.Constant:
                    if (coefficients == null)
                        return new double[] { };
                    return new double[] { coefficients[0] };
                case PolynomialDegree.Linear:
                    return LinearRoots(coefficients[1], coefficients[0], epsilon).ToArray();
                case PolynomialDegree.Quadratic:
                    return QuadraticRoots(coefficients[2], coefficients[1], coefficients[0], epsilon).ToArray();
                case PolynomialDegree.Cubic:
                    return CubicRoots(coefficients[3], coefficients[2], coefficients[1], coefficients[0], epsilon).ToArray();
                case PolynomialDegree.Quartic:
                    return QuarticRoots(coefficients[4], coefficients[3], coefficients[2], coefficients[1], coefficients[0], epsilon).ToArray();
                case PolynomialDegree.Quintic:
                    return QuinticRoots(coefficients[5], coefficients[4], coefficients[3], coefficients[2], coefficients[1], coefficients[0], epsilon).ToArray();
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
                    return new double[] { };
            }
            // should try Newton's method and/or bisection
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int RealOrder()
        {
            if (coefficients == null)
                return 0;
            var order = 0;
            for (var i = 0; i < coefficients.Length; i++)
            {
                if (Abs(coefficients[i]) > Epsilon)
                {
                    order = i;
                }
            }

            return order;
        }

#if Quazistax

        /// <summary>
        /// interpolate
        /// </summary>
        /// <param name="xs"></param>
        /// <param name="ys"></param>
        /// <param name="n"></param>
        /// <param name="offset"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/thelonious/kld-polynomial
        /// Based on trapzd in "Numerical Recipes in C", page 139
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (double y, double dy) Interpolate(List<double> xs, List<double> ys, int n, int offset, double x)
        {
            if (double.IsNaN(n) || double.IsNaN(offset) || double.IsNaN(x))
                throw new Exception($"{nameof(Interpolate)}: {nameof(n)}, {nameof(offset)}, and {nameof(x)} must be numbers");

            var y = 0d;
            var dy = 0d;
            var c = new List<double>(n);
            var d = new List<double>(n);
            var ns = 0;
            var result = (y: y, dy: dy);

            var diff = Abs(x - xs[offset]);
            for (var i = 0; i < n; i++)
            {
                var dift = Abs(x - xs[offset + i]);

                if (dift < diff)
                {
                    ns = i;
                    diff = dift;
                }
                c[i] = d[i] = ys[offset + i];
            }
            y = ys[offset + ns];
            ns--;

            for (var m = 1; m < n; m++)
            {
                for (var i = 0; i < n - m; i++)
                {
                    var ho = xs[offset + i] - x;
                    var hp = xs[offset + i + m] - x;
                    var w = c[i + 1] - d[i];
                    var den = ho - hp;

                    if (den == 0.0)
                    {
                        result = (y: 0, dy: 0);
                        break;
                    }

                    den = w / den;
                    d[i] = hp * den;
                    c[i] = ho * den;
                }
                dy = (2 * (ns + 1) < (n - m)) ? c[ns + 1] : d[ns--];
                y += dy;
            }

            return (y: y, dy: dy);
        }

        /// <summary>
        /// Estimate what is the maximum polynomial evaluation error value under which polynomial evaluation could be in fact 0.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/thelonious/kld-polynomial
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ZeroErrorEstimate()
        {
            var poly = this;
            var ERRF = 1e-15;
            var rb = poly.Bounds();
            var maxabsX = Max(Abs(rb.minX), Abs(rb.maxX));
            if (maxabsX < 0.001)
            {
                return 2 * Abs(poly.Evaluate(ERRF));
            }
            var n = poly.coefficients.Count() - 1;
            var an = poly.coefficients[n];
            var i = 0d;
            return 10 * ERRF * poly.coefficients.Reduce((m, v) => (int)process(m, v, i++), 0);
            double process(double m, double v, double itter)
            {
                var nm = v / an * Pow(maxabsX, itter);
                return nm > m ? nm : m;
            }
        }

        /// <summary>
        /// Calculates left and right Real roots bounds. <br/>
        /// Real roots are in interval [minX, maxX]. Combines Fujiwara lower and upper bounds to get minimal interval.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/thelonious/kld-polynomial
        /// http://en.wikipedia.org/wiki/Properties_of_polynomial_roots
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (double minX, double maxX) Bounds()
        {
            var urb = Bounds_UpperReal_Fujiwara();
            var rb = (minX: urb.negX, maxX: urb.posX);
            if (urb.negX == 0 && urb.posX == 0)
                return rb;
            if (urb.negX == 0)
            {
                rb.minX = Bounds_LowerReal_Fujiwara().posX;
            }
            else if (urb.posX == 0)
            {
                rb.maxX = Bounds_LowerReal_Fujiwara().negX;
            }
            if (rb.minX > rb.maxX)
            {
                //console.log('Polynomial.prototype.bounds: poly has no real roots? or floating point error?');
                rb.minX = rb.maxX = 0;
            }
            return rb;
            // TODO: if sure that there are no complex roots 
            // (maybe by using Sturm's theorem) use:
            //return this.bounds_Real_Laguerre();
        }

        /// <summary>
        /// Calculates upper Real roots bounds. <br/>
        /// Real roots are in interval [negX, posX]. Determined by Fujiwara method.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/thelonious/kld-polynomial
        /// http://en.wikipedia.org/wiki/Properties_of_polynomial_roots
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (double negX, double posX) Bounds_UpperReal_Fujiwara()
        {
            var a = coefficients;
            var n = a.Length - 1;
            var an = a[n];
            if (an != 1)
            {
                a = coefficients.Map((v) => v / an).ToArray();
            }
            var i = -1;
            var b = a.Map((v) => { return (++i < n) ? Pow(Abs((i == 0) ? v / 2 : v), 1 / (n - i)) : v; });

            //var coefSelectionFunc;
            //var bi = 0;
            (double max, double nearmax) find2Max(Func<int, bool> coefSelectionFunc, (double max, double nearmax) acc, double bi, int itter)
            {
                if (coefSelectionFunc(itter))
                {
                    if (acc.max < bi)
                    {
                        acc.nearmax = acc.max;
                        acc.max = bi;
                    }
                    else if (acc.nearmax < bi)
                    {
                        acc.nearmax = bi;
                    }
                }
                return acc;
            };

            var max_nearmax_pos = b.Reduce((bi, acc) => find2Max((itter) => coefSelectionFunc0(itter), acc, bi, i++), (max: 0d, nearmax: 0d));
            bool coefSelectionFunc0(int itter) { return itter < n && a[itter] < 0; };

            var max_nearmax_neg = b.Reduce((bi, acc) => find2Max((itter) => coefSelectionFunc1(itter), acc, bi, i++), (max: 0d, nearmax: 0d));
            bool coefSelectionFunc1(int itter) { return itter < n && ((n % 2 == itter % 2) ? a[itter] < 0 : a[itter] > 0); };

            return (negX: -2 * max_nearmax_neg.max, posX: 2 * max_nearmax_pos.max);
        }

        /// <summary>
        /// Calculates lower Real roots bounds. <br/>
        /// There are no Real roots in interval (negX, posX). Determined by Fujiwara method.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/thelonious/kld-polynomial
        /// http://en.wikipedia.org/wiki/Properties_of_polynomial_roots
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (double negX, double posX) Bounds_LowerReal_Fujiwara()
        {
            var poly = new Polynomial()
            {
                coefficients = coefficients.Slice()
            };
            poly.coefficients.Reverse();
            var res = poly.Bounds_UpperReal_Fujiwara();
            res.negX = 1 / res.negX;
            res.posX = 1 / res.posX;
            return res;
        }

        /// <summary>
        /// trapezoid
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="n"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/thelonious/kld-polynomial
        /// Based on trapzd in "Numerical Recipes in C", page 137
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Trapezoid(double min, double max, int n, double epsilon = Epsilon)
        {
            if (double.IsNaN(min) || double.IsNaN(max) || double.IsNaN(n))
                throw new Exception($"{nameof(Trapezoid)}: parameters must be numbers.");

            var range = max - min;

            var s = 0d;

            if (n == 1)
            {
                var minValue = Evaluate(min);
                var maxValue = Evaluate(max);
                s = 0.5d * range * (minValue + maxValue);
            }
            else
            {
                var it = 1 << (n - 2);
                var delta = range / it;
                var x = min + 0.5 * delta;
                var sum = 0d;

                for (var i = 0; i < it; i++)
                {
                    sum += Evaluate(x);
                    x += delta;
                }
                s = 0.5d * (s + range * sum / it);
            }

            if (double.IsNaN(s))
                throw new Exception($"{nameof(Trapezoid)}: {nameof(s)} is NaN");

            return s;
        }

        /// <summary>
        /// Simpson
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/thelonious/kld-polynomial
        /// Based on trapzd in "Numerical Recipes in C", page 139
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Simpson(double min, double max, double epsilon = Epsilon)
        {
            if (double.IsNaN(min) || double.IsNaN(max))
                throw new Exception($"{nameof(Simpson)}: parameters must be numbers");

            var range = max - min;
            var st = 0.5 * range * (Evaluate(min) + Evaluate(max));
            var t = st;
            var s = 4.0 * st / 3.0;
            var os = s;
            var ost = st;

            var it = 1;
            for (var n = 2; n <= 20; n++)
            {
                var delta = range / it;
                var x = min + 0.5 * delta;
                var sum = 0d;

                for (var i = 1; i <= it; i++)
                {
                    sum += Evaluate(x);
                    x += delta;
                }

                t = 0.5 * (t + range * sum / it);
                st = t;
                s = (4.0 * st - ost) / 3.0;

                if (Abs(s - os) < epsilon * Abs(os))
                    break;

                os = s;
                ost = st;
                it <<= 1;
            }

            return s;
        }

        /// <summary>
        /// Romberg
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="epsilon">The minimal value to represent a change.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// https://github.com/thelonious/kld-polynomial
        /// Based on trapzd in "Numerical Recipes in C", page 139
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Romberg(double min, double max, double epsilon = Epsilon)
        {
            if (double.IsNaN(min) || double.IsNaN(max))
                throw new Exception($"{nameof(Romberg)}: parameters must be numbers");

            var MAX = 20;
            var K = 3;

            var s = new List<double>(MAX + 1);
            var h = new List<double>(MAX + 1);
            (double y, double dy) result = (y: 0, dy: 0);

            h[0] = 1.0;
            for (var j = 1; j <= MAX; j++)
            {
                s[j - 1] = Trapezoid(min, max, j);
                if (j >= K)
                {
                    result = Interpolate(h, s, K, j - K, 0.0);
                    if (Abs(result.dy) <= epsilon * result.y) break;
                }
                s[j] = s[j - 1];
                h[j] = 0.25 * h[j - 1];
            }

            return result.y;
        }

#endif

        #endregion

        #region Standard Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override Int32 GetHashCode()
            => coefficients?.GetHashCode() ?? 0;

        /// <summary>
        /// Compares two Polynomials.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Compare(Polynomial a, Polynomial b)
            => Equals(a, b);

        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(Polynomial a, Polynomial b)
        {
            if (b == null) return false;
            if (a.Count != b.Count) return false;
            for (var i = 0; i < a.Count; i++)
                if (Abs(a.coefficients[i] - b.coefficients[i]) > Epsilon) return false;
            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
            => obj is Polynomial && Equals(this, (Polynomial)obj);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Polynomial value)
            => Equals(this, value);

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Polynomial"/> struct.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Polynomial"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider)
            => ConvertToString(null /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Polynomial"/> struct based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Polynomial"/> struct based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal string ConvertToString(string format, IFormatProvider provider)
        {
            // ¹²³⁴⁵⁶⁷⁸⁹⁰ ⁱ ₁₂₃₄₅₆₇₈₉₀ ⁻⁼⁺⁽⁾ⁿ‽ ₊₋₌₍₎ₓ
            var coefs = new List<string>();
            var signs = new List<string>();
            for (var i = (coefficients?.Length ?? 0) - 1; i >= 0; i--)
            {
                var value = coefficients[i];
                var valueString = value.ToString();
                var powStr = i.ToString();
                powStr = powStr.Replace("1", "¹");
                powStr = powStr.Replace("2", "²");
                powStr = powStr.Replace("3", "³");
                powStr = powStr.Replace("4", "⁴");
                powStr = powStr.Replace("5", "⁵");
                powStr = powStr.Replace("6", "⁶");
                powStr = powStr.Replace("7", "⁷");
                powStr = powStr.Replace("8", "⁸");
                powStr = powStr.Replace("9", "⁹");
                powStr = powStr.Replace("0", "⁰");
                if (value != 0)
                {
                    var sign = (value < 0) ? " - " : " + ";
                    value = Abs(value);
                    valueString = value.ToString();
                    if (i > 0)
                        if (value == 1)
                            valueString = "x";
                        else valueString += "x";
                    if (i > 1)
                        valueString += powStr;
                    signs.Add(sign);
                    coefs.Add(valueString);
                }
            }
            if (signs.Count > 0)
                signs[0] = (signs[0] == " + ") ? "" : "-";
            var result = string.Empty;
            for (var i = 0; i < coefs.Count; i++)
                result += signs[i] + coefs[i];
            return result;
        }

        #endregion
    }
}
