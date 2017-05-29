// <copyright file="Polynomial.cs" company="Shkyrockett" >
//     Copyright (c) 2014 - 2017 Shkyrockett. All rights reserved.
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
using System.Runtime.CompilerServices;
using System.Linq;
using System.Globalization;
using System.Diagnostics;
using System.Collections.Generic;
using static System.Math;
using static Engine.Maths;
using System.Numerics;
using System.Runtime.Serialization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract, Serializable]
    public class Polynomial
    {
        /// <summary>
        /// 
        /// </summary>
        internal static readonly double[] Identity = new double[] { 0, 1 };

        /// <summary>
        /// 
        /// </summary>
        internal static readonly Polynomial T = new Polynomial(Identity);

        /// <summary>
        /// 
        /// </summary>
        internal static readonly Polynomial OneMinusT = 1 - T;

        /// <summary>
        /// 
        /// </summary>
        private const double accuracy = 6;

        #region Fields

        /// <summary>
        /// The coefficients of the polynomial.
        /// </summary>
        private double[] coefficients;

        /// <summary>
        /// Semaphore indicating whether the polynomial can be edited.
        /// </summary>
        bool isReadonly;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a default instance of the <see cref="Polynomial"/> class.
        /// </summary>
        public Polynomial()
        {
            coefficients = new double[] { 0 };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomial"/> class.
        /// </summary>
        /// <param name="coefficients">The coefficients of the polynomial.</param>
        public Polynomial(params double[] coefficients)
        {
            this.coefficients = coefficients;
            if (coefficients == null || coefficients.Length == 0)
                this.coefficients = new double[] { 0 };
        }

        #endregion

        #region Indexers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public double this[int index]
        {
            get
            {
                if (index < 0)
                    throw new ArgumentOutOfRangeException();
                if (index >= coefficients.Length)
                    return 0;
                return coefficients[index];
            }
            set
            {
                if (IsReadonly)
                    throw new InvalidOperationException($"{nameof(Polynomial)} is Read-only.");
                if (index < 0 || index > coefficients.Length)
                    throw new ArgumentOutOfRangeException();
                coefficients[index] = value;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public double[] Coefficients
        {
            get { return coefficients; }
            set { coefficients = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public PolynomialDegree Degree
            => (PolynomialDegree)(coefficients.Length - 1);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Count
            => coefficients.Length;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public bool CanSolveRealRoots
            => (RealOrder() <= 4);

        /// <summary>
        /// Useful for class that want to expose internal value that must not change.
        /// </summary>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
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

        #endregion

        #region Operators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomial operator +(Polynomial a)
            => a;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomial operator +(Polynomial b, double a)
            => a + b;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomial operator +(double a, Polynomial b)
        {
            var res = new double[b.Count];
            for (var i = 0; i < res.Length; i++)
                res[i] = b[i];
            res[0] += a;
            return new Polynomial(res);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomial operator +(Polynomial a, Polynomial b)
        {
            var res = new double[Max(a.Count, b.Count)];
            for (var i = 0; i < res.Length; i++)
            {
                double p = 0;
                if (i < a.Count)
                    p += a[i];
                if (i < b.Count)
                    p += b[i];
                res[i] = p;
            }
            return new Polynomial(res);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomial operator -(Polynomial a)
        {
            var res = new double[a.Count];
            for (var i = 0; i < res.Length; i++)
                res[i] = -a[i];
            return new Polynomial(res);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomial operator -(Polynomial a, double b)
            => a + (-b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomial operator -(double b, Polynomial a)
        {
            var res = new double[a.Count];
            for (var i = 0; i < res.Length; i++)
                res[i] = -a[i];
            res[0] += b;
            return new Polynomial(res);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomial operator -(Polynomial a, Polynomial b)
        {
            var res = new double[Max(a.Count, b.Count)];
            for (var i = 0; i < res.Length; i++)
            {
                double p = 0;
                if (i < a.Count) p += a[i];
                if (i < b.Count) p -= b[i];
                res[i] = p;
            }
            return new Polynomial(res);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomial operator *(Polynomial p, double m)
            => m * p;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomial operator *(double m, Polynomial p)
        {
            var res = new double[p.Count];
            for (var i = 0; i < res.Length; i++)
                res[i] = m * p[i];
            return new Polynomial(res);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomial operator *(Polynomial a, Polynomial b)
        {
            var res = new double[a.Count + b.Count - 1];
            for (var i = 0; i < a.Count; i++)
                for (var j = 0; j < b.Count; j++)
                {
                    var mul = a[i] * b[j];
                    res[i + j] += mul;
                }
            return new Polynomial(res);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomial operator /(Polynomial p, double m)
        {
            var res = new double[p.Count];
            for (var i = 0; i < res.Length; i++)
                res[i] = p[i] / m;
            return new Polynomial(res);
        }

        #endregion

        #region Factories

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dim"></param>
        /// <returns></returns>
        public static Polynomial[] GetStandardBase(int dim)
        {
            if (dim < 1)
                throw new ArgumentException("Dimension expected to be greater than zero.");

            Polynomial[] buf = new Polynomial[dim];

            for (var i = 0; i < dim; i++)
                buf[i] = Monomial(i);

            return buf;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="power"></param>
        /// <param name="coefficient"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomial Term(int power, double coefficient = 1)
        {
            if (power < 0)
                throw new ArgumentOutOfRangeException($"{nameof(power)} cannot be negitive.");
            var res = new double[power + 1];
            res[power] = coefficient;
            return new Polynomial(res);
        }

        /// <summary>
        /// Construct a polynomial P such as ys[i] = P.Compute(i).
        /// </summary>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomial Interpolate(params double[] ys)
        {
            if (ys == null || ys.Length < 2)
                throw new ArgumentNullException("At least 2 different points must be given");

            var res = new Polynomial();
            for (var i = 0; i < ys.Length; i++)
            {
                var e = new Polynomial(1);
                for (var j = 0; j < ys.Length; j++)
                {
                    if (j == i)
                        continue;
                    e *= new Polynomial(-j, 1) / (i - j);
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
        public static Polynomial Monomial(int degree)
        {
            if (degree == 0) return new Polynomial(1);
            double[] coeffs = new double[degree + 1];
            for (var i = 0; i < degree; i++)
                coeffs[i] = 0d;
            coeffs[degree] = 1d;
            return new Polynomial(coeffs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Polynomial Bezier(params double[] values)
        {
            if (values == null || values.Length < 1)
                throw new ArgumentNullException();
            return Bezier(0, values.Length - 1, values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Polynomial Bezier(int from, int to, double[] values)
            => (from == to)
            ? new Polynomial(values[from])
            : OneMinusT * Bezier(from, to - 1, values) + T * Bezier(from + 1, to, values);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Polynomial Linear(double a, double b)
            => OneMinusT * a + T * b;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Polynomial Quadratic(double a, double b, double c)
            => OneMinusT * Linear(a, b) + T * Linear(b, c);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Polynomial Cubic(double a, double b, double c, double d)
            => OneMinusT * Quadratic(a, b, c) + T * Quadratic(b, c, d);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static Polynomial Quartic(double a, double b, double c, double d, double e)
            => OneMinusT * Cubic(a, b, c, d) + T * Cubic(b, c, d, e);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Polynomial Quintic(double a, double b, double c, double d, double e, double f)
            => OneMinusT * Quartic(a, b, c, d, e) + T * Quartic(b, c, d, e, f);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        public static Polynomial Sextic(double a, double b, double c, double d, double e, double f, double g)
            => OneMinusT * Quintic(a, b, c, d, e, f) + T * Quintic(b, c, d, e, f, g);

        /// <summary>
        /// 
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
        public static Polynomial Septic(double a, double b, double c, double d, double e, double f, double g, double h)
            => OneMinusT * Sextic(a, b, c, d, e, f, g) + T * Sextic(b, c, d, e, f, g, h);

        /// <summary>
        /// 
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
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial Derivate()
        {
            var res = new double[Max(1, Count - 1)];
            for (var i = 1; i < Count; i++)
            {
                res[i - 1] = i * coefficients[i];
            }

            return new Polynomial(res);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
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
            return new Polynomial(res);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="term0"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial Integrate(double term0 = 0)
        {
            var res = new double[Count + 1];
            res[0] = term0;
            for (var i = 0; i < Count; i++)
                res[i + 1] = coefficients[i] / (i + 1);
            return new Polynomial(res);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial Power(int n)
        {
            if (n < 0) throw new ArgumentOutOfRangeException($"{nameof(n)} cannot be negative.");

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
                        var mul = Coefficients[i] * res[j];
                        tmp[i + j] += mul;
                    }
                for (var i = 0; i <= porder + order; i++)
                {
                    res[i] = tmp[i];
                    tmp[i] = 0;
                }
            }

            return new Polynomial(res);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/thelonious/kld-polynomial
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Evaluate(double x)
        {
            if (Double.IsNaN(x)) throw new Exception($"{nameof(Evaluate)}: parameter must be a number");

            var result = 0d;
            for (var i = Degree; i >= 0; i--)
                result = result * x + coefficients[(int)i];

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Compute(double x)
        {
            if (Double.IsNaN(x)) throw new Exception($"{nameof(Evaluate)}: parameter must be a number");

            var z = 0d;
            var xcoef = 1d;
            for (var i = 0; i < Count; i++)
            {
                z += Coefficients[i] * xcoef;
                xcoef *= x;
            }

            return z;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex Compute(Complex x)
        {
            Complex z = 0;
            Complex xcoef = 1;
            for (var i = 0; i < Count; i++)
            {
                z += Coefficients[i] * xcoef;
                xcoef *= x;
            }
            return z;
        }

        /// <summary>
        /// Computes value of the differentiated polynomial at x.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Diferentiate(double x)
            => Derivate().Evaluate(x);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/thelonious/kld-polynomial
        /// </remarks>
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
        /// interpolate
        /// </summary>
        /// <param name="xs"></param>
        /// <param name="ys"></param>
        /// <param name="n"></param>
        /// <param name="offset"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/thelonious/kld-polynomial
        /// Based on trapzd in "Numerical Recipes in C", page 139
        /// </remarks>
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
        /// trapezoid
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="n"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/thelonious/kld-polynomial
        /// Based on trapzd in "Numerical Recipes in C", page 137
        /// </remarks>
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
        /// simpson
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/thelonious/kld-polynomial
        /// Based on trapzd in "Numerical Recipes in C", page 139
        /// </remarks>
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
        /// romberg
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/thelonious/kld-polynomial
        /// Based on trapzd in "Numerical Recipes in C", page 139
        /// </remarks>
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

        /// <summary>
        /// Will try to solve root analytically, and if it can will use numerical approach.
        /// </summary>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>http://www.kevlindev.com/geometry/2D/intersections/</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public List<double> Roots(double epsilon = Epsilon)
        {
            Simplify(epsilon);
            switch (Degree)
            {
                case PolynomialDegree.Constant:
                    return new List<double>();
                case PolynomialDegree.Linear:
                    return LinearRoots(epsilon);
                case PolynomialDegree.Quadratic:
                    return QuadraticRoots(epsilon);
                case PolynomialDegree.Cubic:
                    return CubicRoots(epsilon);
                case PolynomialDegree.Quartic:
                    return QuarticRoots(epsilon);
                case PolynomialDegree.Quintic:
                    // ToDo: Uncomment when Quintic roots are implemented.
                    return QuinticRoots(epsilon);
                case PolynomialDegree.Sextic:
                // ToDo: Uncomment when Sextic roots are implemented.
                //return SexticRoots(epsilon);
                case PolynomialDegree.Septic:
                // ToDo: Uncomment when Septic roots are implemented.
                //return SepticRoots(epsilon);
                case PolynomialDegree.Octic:
                // ToDo: Uncomment when Octic roots are implemented.
                //return OcticRoots(epsilon);
                default:
                    // ToDo: If a general root finding algorithm can be found, call it here instead of returning an empty list.
                    return new List<double>();
            }
            // should try Newton's method and/or bisection
        }

        /// <summary>
        /// This method use the Durand-Kerner aka Weierstrass algorithm to find approximate root of this polynomial.
        /// http://en.wikipedia.org/wiki/Durand%E2%80%93Kerner_method
        /// </summary>
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
            Func<int, Complex> divider = i =>
            {
                Complex div = 1;
                for (var j = 0; j < R0.Length; j++)
                {
                    if (j == i) continue;
                    div *= R0[i] - R0[j];
                }
                return div;
            };
            Action step = () =>
            {
                for (var i = 0; i < R0.Length; i++)
                {
                    R1[i] = R0[i] - p.Compute(R0[i]) / divider(i);
                }
            };
            Func<bool> closeEnough = () =>
            {
                for (var i = 0; i < R0.Length; i++)
                {
                    var c = R0[i] - R1[i];
                    if (Abs(c.Real) > epsilon || Abs(c.Imaginary) > epsilon) return false;
                }
                return true;
            };
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
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks>http://www.kevlindev.com/geometry/2D/intersections/</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public List<double> RootsInInterval(double min, double max, double epsilon = Epsilon)
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
                var deriv = Derivate();
                var droots = deriv.RootsInInterval(min, max, epsilon);
                if (droots.Count > 0)
                {
                    root = Bisection(min, droots[0], epsilon);
                    if (root != null) roots.Add(root.Value);
                    // find root on [droots[i],droots[i+1]] for 0 <= i <= count-2
                    for (var i = 0; i <= droots.Count - 2; i++)
                    {
                        root = Bisection(droots[i], droots[i + 1], epsilon);
                        if (root != null) roots.Add(root.Value);
                    }
                    // find root on [droots[count-1],xmax]
                    root = Bisection(droots[droots.Count - 1], max, epsilon);
                    if (root != null) roots.Add(root.Value);
                }
                else
                {
                    // polynomial is monotone on [min,max], has at most one root
                    root = Bisection(min, max, epsilon);
                    if (root != null) roots.Add(root.Value);
                }
            }

            return roots;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private List<double> LinearRoots(double epsilon = Epsilon)
        {
            if (Degree == PolynomialDegree.Linear)
                return Maths.LinearRoots(coefficients[1], coefficients[0], epsilon);
            return new List<double>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private List<double> QuadraticRoots(double epsilon = Epsilon)
        {
            if (Degree == PolynomialDegree.Quadratic)
                return Maths.QuadraticRoots(coefficients[2], coefficients[1], coefficients[0], epsilon);
            return new List<double>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public List<double> CubicRoots(double epsilon = Epsilon)
        {
            if (Degree == PolynomialDegree.Cubic)
                return Maths.CubicRoots(coefficients[3], coefficients[2], coefficients[1], coefficients[0], epsilon);
            return new List<double>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private List<double> QuarticRoots(double epsilon = Epsilon)
        {
            if (Degree == PolynomialDegree.Cubic)
                return Maths.QuarticRoots(coefficients[4], coefficients[3], coefficients[2], coefficients[1], coefficients[0], epsilon);
            return new List<double>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private List<double> QuinticRoots(double epsilon = Epsilon)
        {
            if (Degree == PolynomialDegree.Quintic)
                return Maths.QuinticRoots(coefficients[5], coefficients[4], coefficients[3], coefficients[2], coefficients[1], coefficients[0], epsilon);
            return new List<double>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private List<double> SexticRoots(double epsilon = Epsilon)
        {
            var results = new List<double>();
            if (Degree == PolynomialDegree.Sextic)
            {
                // ToDo: Find implementation for finding Sextic Roots.
            }

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private List<double> SepticRoots(double epsilon = Epsilon)
        {
            var results = new List<double>();
            if (Degree == PolynomialDegree.Septic)
            {
                // ToDo: Find implementation for finding Septic Roots.
            }

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private List<double> OcticRoots(double epsilon = Epsilon)
        {
            var results = new List<double>();
            if (Degree == PolynomialDegree.Octic)
            {
                // ToDo: Find implementation for finding Octic Roots.
            }

            return results;
        }

        /// <summary>
        /// Newton's (Newton-Raphson) method for finding Real roots on univariate function. <br/>
        /// When using bounds, algorithm falls back to secant if newton goes out of range.
        /// Bisection is fall-back for secant when determined secant is not efficient enough.
        /// </summary>
        /// <param name="x0">Initial root guess</param>
        /// <param name="f">Function which root we are trying to find</param>
        /// <param name="df">Derivative of function f</param>
        /// <param name="max_iterations">Maximum number of algorithm iterations</param>
        /// <param name="min">Left bound value</param>
        /// <param name="max">Right bound value</param>
        /// <returns>root</returns>
        /// <remarks>
        /// https://github.com/thelonious/kld-polynomial
        /// http://en.wikipedia.org/wiki/Newton%27s_method
        /// http://en.wikipedia.org/wiki/Secant_method
        /// http://en.wikipedia.org/wiki/Bisection_method
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Newton_secant_bisection(double x0, Func<double, double> f, Func<double, double> df, int max_iterations, double? min = null, double? max = null)
        {
            var prev_dfx = 0d;
            var dfx = 0d;
            var prev_x_ef_correction = 0d;
            var x_correction = 0d;
            var y_atmin = 0d;
            var y_atmax = 0d;
            var x = x0;
            var ACCURACY = 14;
            var min_correction_factor = Pow(10, -ACCURACY);
            var isBounded = (min != null && max != null);
            if (isBounded)
            {
                if (min > max)
                    throw new Exception("newton root finding: min must be greater than max");
                y_atmin = f(min.Value);
                y_atmax = f(max.Value);
                if (Sign(y_atmin) == Sign(y_atmax))
                    throw new Exception("newton root finding: y values of bounds must be of opposite sign");
            }

            bool isEnoughCorrection()
            {
                // stop if correction is too small
                // or if correction is in simple loop
                return (Abs(x_correction) <= min_correction_factor * Abs(x))
                    || (prev_x_ef_correction == (x - x_correction) - x);
            };

            var i = 0;
            //var stepMethod;
            //var details = [];
            for (i = 0; i < max_iterations; i++)
            {
                dfx = df(x);
                if (dfx == 0)
                {
                    if (prev_dfx == 0)
                    {
                        // error
                        throw new Exception("newton root finding: df(x) is zero");
                        //return null;
                    }
                    else
                    {
                        // use previous derivation value
                        dfx = prev_dfx;
                    }
                    // or move x a little?
                    //dfx = df(x != 0 ? x + x * 1e-15 : 1e-15);
                }
                //stepMethod = 'newton';
                prev_dfx = dfx;
                var y = f(x);
                x_correction = y / dfx;
                var x_new = x - x_correction;
                if (isEnoughCorrection())
                {
                    break;
                }

                if (isBounded)
                {
                    if (Sign(y) == Sign(y_atmax))
                    {
                        max = x;
                        y_atmax = y;
                    }
                    else if (Sign(y) == Sign(y_atmin))
                    {
                        min = x;
                        y_atmin = y;
                    }
                    else
                    {
                        x = x_new;
                        //console.log("newton root finding: sign(y) not matched.");
                        break;
                    }

                    if ((x_new < min) || (x_new > max))
                    {
                        if (Sign(y_atmin) == Sign(y_atmax))
                        {
                            break;
                        }

                        var RATIO_LIMIT = 50;
                        var AIMED_BISECT_OFFSET = 0.25; // [0, 0.5)
                        var dy = y_atmax - y_atmin;
                        var dx = max - min;

                        if (dy == 0)
                        {
                            //stepMethod = 'bisect';
                            x_correction = x - (min.Value + dx.Value * 0.5);
                        }
                        else if (Abs(dy / Min(y_atmin, y_atmax)) > RATIO_LIMIT)
                        {
                            //stepMethod = 'aimed bisect';
                            x_correction = x - (min.Value + dx.Value * (0.5 + (Abs(y_atmin) < Abs(y_atmax) ? -AIMED_BISECT_OFFSET : AIMED_BISECT_OFFSET)));
                        }
                        else
                        {
                            //stepMethod = 'secant'; 
                            x_correction = x - (min.Value - y_atmin / dy * dx.Value);
                        }
                        x_new = x - x_correction;

                        if (isEnoughCorrection())
                        {
                            break;
                        }
                    }
                }
                //details.push([stepMethod, i, x, x_new, x_correction, min, max, y]);
                prev_x_ef_correction = x - x_new;
                x = x_new;
            }
            //details.push([stepMethod, i, x, x_new, x_correction, min, max, y]);
            //console.log(details.join('\r\n'));
            //if (i == max_iterations)
            //    console.log('newt: steps=' + ((i==max_iterations)? i:(i + 1)));
            return x;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coefficients"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Trim(double[] coefficients, double epsilon = Epsilon)
        {
            var order = 0;
            for (var i = 0; i < coefficients.Length; i++)
            {
                if (Abs(coefficients[i]) > epsilon)
                {
                    order = i;
                }
            }
            var res = new double[order + 1];
            for (var i = 0; i < res.Length; i++)
            {
                if (Abs(coefficients[i]) > epsilon)
                {
                    res[i] = coefficients[i];
                }
            }
            return new Polynomial(res);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coefficients"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RealOrder(double[] coefficients, double epsilon = Epsilon)
        {
            if (coefficients == null)
                return 0;
            var order = 0;
            for (var i = 0; i < coefficients.Length; i++)
            {
                if (Abs(coefficients[i]) > epsilon)
                {
                    order = i;
                }
            }
            return order;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="x1"></param>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (double minY, double maxY) GetMinMax(double x0, double x1)
        {
            var points = Derivate().Roots()
                .Where(t => t > x0 && t < x1)
                .Concat(new[] { x0, x1 });
            var first = true;
            (double minY, double maxY) = (0, 0);
            foreach (var t in points)
            {
                var y = Compute(t);
                if (first)
                {
                    first = false;
                    minY = maxY = y;
                }
                else
                {
                    if (y < minY)
                        minY = y;
                    if (y > maxY)
                        maxY = y;
                }
            }
            return (minY, maxY);
        }

        /// <summary>
        /// Estimate what is the maximum polynomial evaluation error value under which polynomial evaluation could be in fact 0.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/thelonious/kld-polynomial
        /// </remarks>
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
        /// Estimate what is the maximum polynomial evaluation error value under which polynomial evaluation could be in fact 0.
        /// </summary>
        /// <param name="maxabsX"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/thelonious/kld-polynomial
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ZeroErrorEstimate(double maxabsX)
        {
            var poly = this;
            var ERRF = 1e-15;
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
        /// Calculates upper Real roots bounds. <br/>
        /// Real roots are in interval [negX, posX]. Determined by Fujiwara method.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/thelonious/kld-polynomial
        /// http://en.wikipedia.org/wiki/Properties_of_polynomial_roots
        /// </remarks>
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
        /// <remarks>
        /// https://github.com/thelonious/kld-polynomial
        /// http://en.wikipedia.org/wiki/Properties_of_polynomial_roots
        /// </remarks>
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
        /// Calculates left and right Real roots bounds. <br/>
        /// Real roots are in interval [minX, maxX]. Combines Fujiwara lower and upper bounds to get minimal interval.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/thelonious/kld-polynomial
        /// http://en.wikipedia.org/wiki/Properties_of_polynomial_roots
        /// </remarks>
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

        #endregion

        #region Mutators

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Simplify(double epsilon = Epsilon)
        {
            for (var i = (int)Degree; i >= 0; i--)
            {
                var term = Abs(coefficients[i]);
                if (term <= epsilon)
                    coefficients = coefficients.RemoveAt(i);
                else break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Simplify(Polynomial polynomial, double epsilon = Epsilon)
        {
            var coefficients = new double[polynomial.Count];
            Array.Copy(polynomial.coefficients, coefficients, (int)polynomial.Degree);
            for (var i = polynomial.Degree; i >= 0; i--)
            {
                if (Abs(coefficients[(int)i]) <= epsilon)
                    coefficients = coefficients.RemoveAt((int)i);
                else break;
            }
            return new Polynomial(coefficients);
        }

        /// <summary>
        /// This might be the same as Simplify?
        /// </summary>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial Trim(double epsilon = Epsilon)
        {
            var order = 0;
            for (var i = 0; i < Count; i++)
            {
                if (Abs(coefficients[i]) > Epsilon)
                {
                    order = i;
                }
            }
            var res = new double[order + 1];
            for (var i = 0; i < res.Length; i++)
            {
                if (Abs(coefficients[i]) > Epsilon)
                {
                    res[i] = coefficients[i];
                }
            }

            return new Polynomial(res);
        }

        /// <summary>
        /// This might be the same as Simplify?
        /// </summary>
        /// <param name="polynomial"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial Trim(Polynomial polynomial, double epsilon = Epsilon)
        {
            var coefficients = new double[polynomial.Count];
            Array.Copy(polynomial.coefficients, coefficients, (int)polynomial.Degree);
            var order = 0;
            for (var i = 0; i < polynomial.Count; i++)
            {
                if (Abs(coefficients[i]) > Epsilon)
                {
                    order = i;
                }
            }
            var res = new double[order + 1];
            for (var i = 0; i < res.Length; i++)
            {
                if (Abs(coefficients[i]) > Epsilon)
                {
                    res[i] = coefficients[i];
                }
            }

            return new Polynomial(res);
        }

        #endregion

        #region Standard Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override Int32 GetHashCode()
            => coefficients.GetHashCode();

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
            if (a.Coefficients.Length != b.Coefficients.Length) return false;
            for (var i = 0; i < a.Coefficients.Length; i++)
                if (Abs(a.Coefficients[i] - b.Coefficients[i]) > Epsilon) return false;
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
        /// Creates a human-readable string that represents this <see cref="Polynomial"/> inherited class.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Polynomial"/> inherited class based on the IFormatProvider
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
        /// Creates a string representation of this <see cref="Polynomial"/> inherited class based on the format string
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
        public string ConvertToString(string format, IFormatProvider provider)
        {
            var coefs = new List<string>();
            var signs = new List<string>();
            for (var i = coefficients.Length - 1; i >= 0; i--)
            {
                var value = coefficients[i];
                var valueString = value.ToString();
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
                        valueString += "^" + i;
                    signs.Add(sign);
                    coefs.Add(valueString);
                }
            }
            signs[0] = (signs[0] == " + ") ? "" : "-";
            var result = "";
            for (var i = 0; i < coefs.Count; i++)
                result += signs[i] + coefs[i];
            return result;
        }

        #endregion
    }
}
