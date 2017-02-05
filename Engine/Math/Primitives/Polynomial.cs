// <copyright file="Polynomial.cs" company="Shkyrockett" >
//     Copyright (c) 2014 - 2017 Shkyrockett. All rights reserved.
// </copyright>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary>
//     Based on classes from https://github.com/superlloyd/Poly, http://www.kevlindev.com/geometry/2D/intersections/, and https://github.com/Pomax/bezierjs.
// </summary>

using System;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Globalization;
using System.Diagnostics;
using System.Collections.Generic;
using static System.Math;
using static Engine.Maths;
using System.Numerics;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class Polynomial
    {
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
            => coefficients = new double[] { 0 };

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
        public CurveDegree Degree
            => (CurveDegree)(coefficients.Length - 1);

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
            for (int i = 0; i < res.Length; i++)
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
            for (int i = 0; i < res.Length; i++)
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
            for (int i = 0; i < res.Length; i++)
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
            for (int i = 0; i < res.Length; i++)
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
            for (int i = 0; i < res.Length; i++)
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
            for (int i = 0; i < res.Length; i++)
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
            for (int i = 0; i < res.Length; i++)
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

            for (int i = 0; i < dim; i++)
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
            for (int i = 0; i < ys.Length; i++)
            {
                var e = new Polynomial(1);
                for (int j = 0; j < ys.Length; j++)
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
            for (int i = 0; i < degree; i++)
                coeffs[i] = 0d;
            coeffs[degree] = 1d;
            return new Polynomial(coeffs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Polynomial Linear(double a, double b)
            => new Polynomial(a, b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Polynomial Quadratic(double a, double b, double c)
            => new Polynomial(a, b, c);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Polynomial Cubic(double a, double b, double c, double d)
            => new Polynomial(a, b, c, d);

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
            => new Polynomial(a, b, c, d, e);

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
            => new Polynomial(a, b, c, d, e, f);

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
        private static Polynomial Sextic(double a, double b, double c, double d, double e, double f, double g)
            => new Polynomial(a, b, c, d, e, f, g);

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
        private static Polynomial Septic(double a, double b, double c, double d, double e, double f, double g, double h)
            => new Polynomial(a, b, c, d, e, f, g, h);

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
        private static Polynomial Octic(double a, double b, double c, double d, double e, double f, double g, double h, double i)
            => new Polynomial(a, b, c, d, e, f, g, h, i);

        #endregion

        #region Methods

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
            int order = 0;
            double high = 1;
            for (int i = 0; i < Count; i++)
            {
                if (Abs(coefficients[i]) > epsilon)
                {
                    order = i;
                    high = coefficients[i];
                }
            }
            var res = new double[order + 1];
            for (int i = 0; i < res.Length; i++)
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
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial Derivate()
        {
            var res = new double[Max(1, (int)Degree)];
            for (int i = 1; i < Count; i++)
                res[i - 1] = i * coefficients[i];
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
            for (int i = 0; i < Count; i++)
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
            if (n < 0)
                throw new ArgumentOutOfRangeException($"{nameof(n)} cannot be negitive.");
            var order = (int)Degree;
            var res = new double[order * n + 1];
            var tmp = new double[order * n + 1];
            res[0] = 1;
            for (int pow = 0; pow < n; pow++)
            {
                int porder = pow * order;
                for (var i = 0; i <= order; i++)
                    for (var j = 0; j <= porder; j++)
                    {
                        var mul = Coefficients[i] * res[j];
                        tmp[i + j] += mul;
                    }
                for (int i = 0; i <= porder + order; i++)
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Evaluate(double x)
        {
            if (Double.IsNaN(x))
                throw new Exception($"{nameof(Evaluate)}: parameter must be a number");

            var z = 0d;
            for (var i = Degree; i >= 0; i--)
                z = z * x + coefficients[(int)i];

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
        public double Compute(double x)
        {
            if (Double.IsNaN(x))
                throw new Exception($"{nameof(Compute)}: parameter must be a number");

            double z = 0;
            double xcoef = 1;
            for (int i = 0; i < Count; i++)
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
            Complex res = 0;
            Complex xcoef = 1;
            for (int i = 0; i < Count; i++)
            {
                res += Coefficients[i] * xcoef;
                xcoef *= x;
            }
            return res;
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
        /// <remarks> http://www.kevlindev.com/geometry/2D/intersections/ </remarks>
        public List<double> Roots(double epsilon = Epsilon)
        {
            Simplify(epsilon);
            switch (Degree)
            {
                case CurveDegree.Constant:
                    return new List<double>();
                case CurveDegree.Linear:
                    return LinearRoot(epsilon);
                case CurveDegree.Quadratic:
                    return QuadraticRoots(epsilon);
                case CurveDegree.Cubic:
                    return CubicRoots(epsilon);
                case CurveDegree.Quartic:
                    return QuarticRoots(epsilon);
                case CurveDegree.Quintic:
                    // ToDo: Uncomment when Quintic roots are implemented.
                    //return QuinticRoots(epsilon);
                case CurveDegree.Sextic:
                    // ToDo: Uncomment when Sextic roots are implemented.
                    //return SexticRoots(epsilon);
                case CurveDegree.Septic:
                    // ToDo: Uncomment when Septic roots are implemented.
                    //return SepticRoots(epsilon);
                case CurveDegree.Octic:
                    // ToDo: Uncomment when Octic roots are implemented.
                    //return OcticRoots(epsilon);
                default:
                    // ToDo: If a general root finding algorithm can be found, call it here instead of returning an empty list.
                    return new List<double>();
            }
        }

        /// <summary>
        /// This method use the Durand-Kerner aka Weierstrass algorithm to find approximate root of this polynomial.
        /// http://en.wikipedia.org/wiki/Durand%E2%80%93Kerner_method
        /// </summary>
        public Complex[] ComplexRoots(double epsilon = Epsilon)
        {
            var p = Normalize();
            if (p.coefficients.Length == 1) return new Complex[0];

            Complex x0 = 1;
            Complex xMul = 0.4 + 0.9 * Complex.ImaginaryOne;
            var R0 = new Complex[p.coefficients.Length - 1];
            for (int i = 0; i < R0.Length; i++)
            {
                R0[i] = x0;
                x0 *= xMul;
            }

            var R1 = new Complex[p.coefficients.Length - 1];
            Func<int, Complex> divider = i =>
            {
                Complex div = 1;
                for (int j = 0; j < R0.Length; j++)
                {
                    if (j == i) continue;
                    div *= R0[i] - R0[j];
                }
                return div;
            };
            Action step = () =>
            {
                for (int i = 0; i < R0.Length; i++)
                {
                    R1[i] = R0[i] - p.Compute(R0[i]) / divider(i);
                }
            };
            Func<bool> closeEnough = () =>
            {
                for (int i = 0; i < R0.Length; i++)
                {
                    var c = R0[i] - R1[i];
                    if (Abs(c.Real) > epsilon || Abs(c.Imaginary) > epsilon) return false;
                }
                return true;
            };
            bool close = false;
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
        /// <remarks> http://www.kevlindev.com/geometry/2D/intersections/ </remarks>
        public List<double> RootsInInterval(double min, double max, double epsilon = Epsilon)
        {
            var roots = new List<double>();
            double? root;
            if (Degree == CurveDegree.Linear)
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
                    for (int i = 0; i <= droots.Count - 2; i++)
                    {
                        root = Bisection(droots[i], droots[i + 1], epsilon);
                        if (root != null) roots.Add(root.Value);
                    }
                    root = Bisection(droots[droots.Count - 1], max, epsilon);
                    if (root != null) roots.Add(root.Value);
                }
                else
                {
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
        /// <remarks> http://www.kevlindev.com/geometry/2D/intersections/ </remarks>
        private List<double> LinearRoot(double epsilon = Epsilon)
        {
            var result = new List<double>();
            var a = coefficients[1];
            if (!(Abs(a) <= epsilon))
                result.Add(-coefficients[0] / a);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/geometry/2D/intersections/ </remarks>
        private List<double> QuadraticRoots(double epsilon = Epsilon)
        {
            var results = new List<double>();
            if (Degree == CurveDegree.Quadratic)
            {
                var a = coefficients[2];
                var b = coefficients[1] / a;
                var c = coefficients[0] / a;
                var descriminant = b * b - 4d * c;
                if (Abs(descriminant) <= epsilon)
                    descriminant = 0;
                if (descriminant > 0)
                {
                    var e = Sqrt(descriminant);
                    results.Add(OneHalf * (-b + e));
                    results.Add(OneHalf * (-b - e));
                }
                else if (descriminant == 0)
                {
                    results.Add(OneHalf * -b);
                }
            }

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/geometry/2D/intersections/ </remarks>
        private List<double> CubicRoots(double epsilon = Epsilon)
        {
            var results = new List<double>();
            if (Degree == CurveDegree.Cubic)
            {
                var c3 = coefficients[3];
                var c2 = coefficients[2] / c3;
                var c1 = coefficients[1] / c3;
                var c0 = coefficients[0] / c3;
                var a = (3 * c1 - c2 * c2) * OneThird;
                var b = (2 * c2 * c2 * c2 - 9 * c1 * c2 + 27 * c0) * OneTwentySeventh;
                var offset = c2 * OneThird;
                var discriminant = b * b * OneQuarter + a * a * a * OneTwentySeventh;
                var halfB = OneHalf * b;
                if (Abs(discriminant) <= epsilon)
                    discriminant = 0;
                if (discriminant > 0)
                {
                    var e = Sqrt(discriminant);
                    double tmp = -halfB + e;
                    double root;
                    if (tmp >= 0)
                        root = Pow(tmp, OneThird);
                    else
                        root = -Pow(-tmp, OneThird);
                    tmp = -halfB - e;
                    if (tmp >= 0)
                        root += Pow(tmp, OneThird);
                    else
                        root -= Pow(-tmp, OneThird);
                    results.Add(root - offset);
                }
                else if (discriminant < 0)
                {
                    var distance = Sqrt(-a * OneThird);
                    var angle = Atan2(Sqrt(-discriminant), -halfB) * OneThird;
                    var cos = Cos(angle);
                    var sin = Sin(angle);
                    results.Add(2 * distance * cos - offset);
                    results.Add(-distance * (cos + Sqrt3 * sin) - offset);
                    results.Add(-distance * (cos - Sqrt3 * sin) - offset);
                }
                else
                {
                    double tmp;
                    if (halfB >= 0)
                        tmp = -Pow(halfB, OneThird);
                    else tmp = Pow(-halfB, OneThird);
                    results.Add(2 * tmp - offset);
                    results.Add(-tmp - offset);
                }
            }
            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks> http://www.kevlindev.com/geometry/2D/intersections/ </remarks>
        private List<double> QuarticRoots(double epsilon = Epsilon)
        {
            var results = new List<double>();
            if (Degree == CurveDegree.Quartic)
            {
                var c4 = coefficients[4];
                var c3 = coefficients[3] / c4;
                var c2 = coefficients[2] / c4;
                var c1 = coefficients[1] / c4;
                var c0 = coefficients[0] / c4;
                var resolveRoots = new Polynomial(1, -c2, c3 * c1 - 4d * c0, -c3 * c3 * c0 + 4 * c2 * c0 - c1 * c1).CubicRoots();
                var y = resolveRoots[0];
                var discriminant = c3 * c3 * OneQuarter - c2 + y;
                if (Abs(discriminant) <= epsilon)
                    discriminant = 0d;
                if (discriminant > 0)
                {
                    var e = Sqrt(discriminant);
                    var t1 = 3 * c3 * c3 * OneQuarter - e * e - 2 * c2;
                    var t2 = (4 * c3 * c2 - 8 * c1 - c3 * c3 * c3) / (4 * e);
                    var plus = t1 + t2;
                    var minus = t1 - t2;
                    if (Abs(plus) <= epsilon)
                        plus = 0;
                    if (Abs(minus) <= epsilon)
                        minus = 0;
                    if (plus >= 0)
                    {
                        var f = Sqrt(plus);
                        results.Add(-c3 * OneQuarter + (e + f) * OneHalf);
                        results.Add(-c3 * OneQuarter + (e - f) * OneHalf);
                    }
                    if (minus >= 0)
                    {
                        var f = Sqrt(minus);
                        results.Add(-c3 * OneQuarter + (f - e) * OneHalf);
                        results.Add(-c3 * OneQuarter - (f + e) * OneHalf);
                    }
                }
                else if (discriminant < 0)
                {
                }
                else
                {
                    var t2 = y * y - 4 * c0;
                    if (t2 >= -epsilon)
                    {
                        if (t2 < 0) t2 = 0;
                        t2 = 2d * Sqrt(t2);
                        double t1 = 3 * c3 * c3 * OneQuarter - 2d * c2;
                        if (t1 + t2 >= epsilon)
                        {
                            var d = Sqrt(t1 + t2);
                            results.Add(-c3 * OneQuarter + d * OneHalf);
                            results.Add(-c3 * OneQuarter - d * OneHalf);
                        }
                        if (t1 - t2 >= epsilon)
                        {
                            var d = Sqrt(t1 - t2);
                            results.Add(-c3 * OneQuarter + d * OneHalf);
                            results.Add(-c3 * OneQuarter - d * OneHalf);
                        }
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// ToDo: Translate code found at: http://abecedarical.com/javascript/script_quintic.html and http://jwezorek.com/2015/01/my-code-for-doing-two-things-that-sooner-or-later-you-will-want-to-do-with-bezier-curves/:
        /// This method computes complex and real roots for any quintic polynomial.
        /// It applies the Lin-Bairstow algorithm which iteratively solves for the 
        /// roots starting from random guesses for a solution. 
        /// The calculator is designed to solve for the roots of a quintic polynomial
        /// with the form: x⁵ + ax⁴ + bx³ + cx² + dx + e = 0
        /// ⁰¹²³⁴⁵⁶⁷⁸⁹
        /// </summary>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks>
        ///     http://abecedarical.com/javascript/script_quintic.html
        ///     http://jwezorek.com/2015/01/my-code-for-doing-two-things-that-sooner-or-later-you-will-want-to-do-with-bezier-curves/
        /// </remarks>
        private List<double> QuinticRoots(double epsilon = Epsilon)
        {
            var results = new List<double>();
            if (Degree == CurveDegree.Quintic)
            {
                // ToDo: Translate code found at: http://abecedarical.com/javascript/script_quintic.html and http://jwezorek.com/2015/01/my-code-for-doing-two-things-that-sooner-or-later-you-will-want-to-do-with-bezier-curves/
            }

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        private List<double> SexticRoots(double epsilon = Epsilon)
        {
            var results = new List<double>();
            if (Degree == CurveDegree.Sextic)
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
        private List<double> SepticRoots(double epsilon = Epsilon)
        {
            var results = new List<double>();
            if (Degree == CurveDegree.Septic)
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
        private List<double> OcticRoots(double epsilon = Epsilon)
        {
            var results = new List<double>();
            if (Degree == CurveDegree.Octic)
            {
                // ToDo: Find implementation for finding Octic Roots.
            }

            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coefficients"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public static Polynomial Trim(double[] coefficients, double epsilon = Epsilon)
        {
            int order = 0;
            for (int i = 0; i < coefficients.Length; i++)
            {
                if (Abs(coefficients[i]) > epsilon)
                {
                    order = i;
                }
            }
            var res = new double[order + 1];
            for (int i = 0; i < res.Length; i++)
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
        public static int RealOrder(double[] coefficients, double epsilon = Epsilon)
        {
            if (coefficients == null)
                return 0;
            int order = 0;
            for (int i = 0; i < coefficients.Length; i++)
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
            int order = 0;
            for (int i = 0; i < coefficients.Length; i++)
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
            var points = Derivate()
                .Roots()
                .Where(t => t > x0 && t < x1)
                .Concat(new[] { x0, x1 });
            bool first = true;
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
                if (Abs(coefficients[i]) <= epsilon)
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
            int order = 0;
            for (int i = 0; i < Count; i++)
            {
                if (Abs(coefficients[i]) > Epsilon)
                {
                    order = i;
                }
            }
            var res = new double[order + 1];
            for (int i = 0; i < res.Length; i++)
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
            int order = 0;
            for (int i = 0; i < polynomial.Count; i++)
            {
                if (Abs(coefficients[i]) > Epsilon)
                {
                    order = i;
                }
            }
            var res = new double[order + 1];
            for (int i = 0; i < res.Length; i++)
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
            for (int i = 0; i < a.Coefficients.Length; i++)
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
                double value = coefficients[i];
                string valueString = value.ToString();
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
