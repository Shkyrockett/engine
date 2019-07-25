// <copyright file="Polynomial.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2019 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <date></date>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using static Engine.Mathematics;
using static Engine.Operations;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The polynomialx class.
    /// </summary>
    public class Polynomialx
        : IFormattable
    {
        #region Fields
        /// <summary>
        /// The is readonly.
        /// </summary>
        private bool isReadonly;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a default instance of the <see cref="Polynomialx"/> class.
        /// </summary>
        public Polynomialx()
        {
            Coefficients = new List<double>(1);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Polynomialx"/> class.
        /// </summary>
        /// <param name="coefficients"></param>
        public Polynomialx(params double[] coefficients)
        {
            Coefficients = coefficients?.Length == 0 ? new List<double>(1) : new List<double>(coefficients);
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the coefficients.
        /// </summary>
        public List<double> Coefficients { get; set; }

        /// <summary>
        /// Gets the degree.
        /// </summary>
        public int Degree
            => Coefficients.Count - 1;

        /// <summary>
        /// Gets a value indicating whether 
        /// </summary>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public bool CanSolveRealRoots
            => RealOrder() <= 4;

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
                {
                    return;
                }

                if (value)
                {
                    isReadonly = true;
                }
            }
        }
        #endregion Properties

        #region Operators
        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomialx operator +(Polynomialx a)
            => a;

        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <param name="a">The a.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomialx operator +(Polynomialx b, double a)
            => a + b;

        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomialx operator +(double a, Polynomialx b)
        {
            var res = new double[b.Coefficients.Count];
            for (var i = 0; i < res.Length; i++)
            {
                res[i] = b.Coefficients[i];
            }

            res[0] += a;
            return new Polynomialx(res);
        }

        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomialx operator +(Polynomialx a, Polynomialx b)
        {
            var res = new double[Max(a.Coefficients.Count, b.Coefficients.Count)];
            for (var i = 0; i < res.Length; i++)
            {
                double p = 0;
                if (i < a.Coefficients.Count)
                {
                    p += a.Coefficients[i];
                }

                if (i < b.Coefficients.Count)
                {
                    p += b.Coefficients[i];
                }

                res[i] = p;
            }
            return new Polynomialx(res);
        }

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomialx operator -(Polynomialx a)
        {
            var res = new double[a.Coefficients.Count];
            for (var i = 0; i < res.Length; i++)
            {
                res[i] = -a.Coefficients[i];
            }

            return new Polynomialx(res);
        }

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomialx operator -(Polynomialx a, double b)
            => a + (-b);

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="b">The b.</param>
        /// <param name="a">The a.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomialx operator -(double b, Polynomialx a)
        {
            var res = new double[a.Coefficients.Count];
            for (var i = 0; i < res.Length; i++)
            {
                res[i] = -a.Coefficients[i];
            }

            res[0] += b;
            return new Polynomialx(res);
        }

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomialx operator -(Polynomialx a, Polynomialx b)
        {
            var res = new double[Max(a.Coefficients.Count, b.Coefficients.Count)];
            for (var i = 0; i < res.Length; i++)
            {
                double p = 0;
                if (i < a.Coefficients.Count)
                {
                    p += a.Coefficients[i];
                }

                if (i < b.Coefficients.Count)
                {
                    p -= b.Coefficients[i];
                }

                res[i] = p;
            }
            return new Polynomialx(res);
        }

        /// <summary>
        /// The operator *.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="m">The m.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomialx operator *(Polynomialx p, double m)
            => m * p;

        /// <summary>
        /// The operator *.
        /// </summary>
        /// <param name="m">The m.</param>
        /// <param name="p">The p.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomialx operator *(double m, Polynomialx p)
        {
            var res = new double[p.Coefficients.Count];
            for (var i = 0; i < res.Length; i++)
            {
                res[i] = m * p.Coefficients[i];
            }

            return new Polynomialx(res);
        }

        /// <summary>
        /// The operator *.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomialx operator *(Polynomialx a, Polynomialx b)
        {
            var res = new double[a.Coefficients.Count + b.Coefficients.Count - 1];
            for (var i = 0; i < a.Coefficients.Count; i++)
            {
                for (var j = 0; j < b.Coefficients.Count; j++)
                {
                    var mul = a.Coefficients[i] * b.Coefficients[j];
                    res[i + j] += mul;
                }
            }

            return new Polynomialx(res);
        }

        /// <summary>
        /// The operator /.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="m">The m.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomialx operator /(Polynomialx p, double m)
        {
            var res = new double[p.Coefficients.Count];
            for (var i = 0; i < res.Length; i++)
            {
                res[i] = p.Coefficients[i] / m;
            }

            return new Polynomialx(res);
        }
        #endregion Operators

        #region Factories
        /// <summary>
        /// The term.
        /// </summary>
        /// <param name="power">The power.</param>
        /// <param name="coefficient">The coefficient.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomialx Term(int power, double coefficient = 1)
        {
            if (power < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            var res = new double[power + 1];
            res[power] = coefficient;
            return new Polynomialx(res);
        }

        /// <summary>
        /// Construct a polynomial P such as ys[i] = P.Compute(i).
        /// </summary>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomialx Interpolate(params double[] ys)
        {
            if (ys is null || ys.Length < 2)
            {
                throw new ArgumentNullException("At least 2 different points must be given");
            }

            var res = new Polynomialx();
            for (var i = 0; i < ys.Length; i++)
            {
                var e = new Polynomialx(1);
                for (var j = 0; j < ys.Length; j++)
                {
                    if (j == i)
                    {
                        continue;
                    }

                    e *= new Polynomialx(-j, 1) / (i - j);
                }
                res += ys[i] * e;
            }
            return res.Trim();
        }

        /// <summary>
        /// The linear.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        public static Polynomialx Linear(double a, double b)
            => new Polynomialx(a, b);

        /// <summary>
        /// The quadratic.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        public static Polynomialx Quadratic(double a, double b, double c)
            => new Polynomialx(a, b, c);

        /// <summary>
        /// The cubic.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        public static Polynomialx Cubic(double a, double b, double c, double d)
            => new Polynomialx(a, b, c, d);

        /// <summary>
        /// The quartic.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="e">The e.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        public static Polynomialx Quartic(double a, double b, double c, double d, double e)
            => new Polynomialx(a, b, c, d, e);

        /// <summary>
        /// The sextic.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="e">The e.</param>
        /// <param name="f">The f.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        public static Polynomialx Sextic(double a, double b, double c, double d, double e, double f)
            => new Polynomialx(a, b, c, d, e, f);

        /// <summary>
        /// The septic.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="e">The e.</param>
        /// <param name="f">The f.</param>
        /// <param name="g">The g.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        public static Polynomialx Septic(double a, double b, double c, double d, double e, double f, double g)
            => new Polynomialx(a, b, c, d, e, f, g);

        /// <summary>
        /// The octic.
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="e">The e.</param>
        /// <param name="f">The f.</param>
        /// <param name="g">The g.</param>
        /// <param name="h">The h.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        public static Polynomialx Octic(double a, double b, double c, double d, double e, double f, double g, double h)
            => new Polynomialx(a, b, c, d, e, f, g, h);

        /// <summary>
        /// The bezout.
        /// </summary>
        /// <param name="e1">The e1.</param>
        /// <param name="e2">The e2.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        public static Polynomialx Bezout(double[] e1, double[] e2)
        {
            var AB = (e1[0] * e2[1]) - (e2[0] * e1[1]);
            var AC = (e1[0] * e2[2]) - (e2[0] * e1[2]);
            var AD = (e1[0] * e2[3]) - (e2[0] * e1[3]);
            var AE = (e1[0] * e2[4]) - (e2[0] * e1[4]);
            var AF = (e1[0] * e2[5]) - (e2[0] * e1[5]);
            var BC = (e1[1] * e2[2]) - (e2[1] * e1[2]);
            var BE = (e1[1] * e2[4]) - (e2[1] * e1[4]);
            var BF = (e1[1] * e2[5]) - (e2[1] * e1[5]);
            var CD = (e1[2] * e2[3]) - (e2[2] * e1[3]);
            var DE = (e1[3] * e2[4]) - (e2[3] * e1[4]);
            var DF = (e1[3] * e2[5]) - (e2[3] * e1[5]);
            var BFpDE = BF + DE;
            var BEmCD = BE - CD;

            return new Polynomialx(
                (AB * BC) - (AC * AC),
                (AB * BEmCD) + (AD * BC) - (2 * AC * AE),
                (AB * BFpDE) + (AD * BEmCD) - (AE * AE) - (2 * AC * AF),
                (AB * DF) + (AD * BFpDE) - (2 * AE * AF),
                (AD * DF) - (AF * AF)
            );
        }
        #endregion Factories

        #region Specific Methods
        /// <summary>
        /// The normalize.
        /// </summary>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomialx Normalize()
        {
            var order = 0;
            double high = 1;
            for (var i = 0; i < Coefficients.Count; i++)
            {
                if (Abs(Coefficients[i]) > Epsilon)
                {
                    order = i;
                    high = Coefficients[i];
                }
            }
            var res = new double[order + 1];
            for (var i = 0; i < res.Length; i++)
            {
                if (Abs(Coefficients[i]) > Epsilon)
                {
                    res[i] = Coefficients[i] / high;
                }
            }
            return new Polynomialx(res);
        }

        /// <summary>
        /// The derivate.
        /// </summary>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomialx Derivate()
        {
            var res = new double[Max(1, Coefficients.Count - 1)];
            for (var i = 1; i < Coefficients.Count; i++)
            {
                res[i - 1] = i * Coefficients[i];
            }

            return new Polynomialx(res);
        }

        /// <summary>
        /// The integrate.
        /// </summary>
        /// <param name="term0">The term0.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomialx Integrate(double term0 = 0)
        {
            var res = new double[Coefficients.Count + 1];
            res[0] = term0;
            for (var i = 0; i < Coefficients.Count; i++)
            {
                res[i + 1] = Coefficients[i] / (i + 1);
            }

            return new Polynomialx(res);
        }

        /// <summary>
        /// The pow.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomialx Pow(int n)
        {
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            var order = Coefficients.Count - 1;
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
                        var mul = Coefficients[i] * res[j];
                        tmp[i + j] += mul;
                    }
                }

                for (var i = 0; i <= porder + order; i++)
                {
                    res[i] = tmp[i];
                    tmp[i] = 0;
                }
            }
            return new Polynomialx(res);
        }

        /// <summary>
        /// The evaluate.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <exception cref="Exception">Polynomial.Eval: parameter must be a number</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Evaluate(double x)
        {
            if (double.IsNaN(x))
            {
                throw new Exception("Polynomial.Eval: parameter must be a number");
            }

            var result = 0d;

            for (var i = Degree; i >= 0; i--)
            {
                result = (result * x) + Coefficients[i];
            }

            return result;
        }

        /// <summary>
        /// The compute.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Compute(double x)
        {
            double z = 0;
            double xcoef = 1;
            for (var i = 0; i < Coefficients.Count; i++)
            {
                z += Coefficients[i] * xcoef;
                xcoef *= x;
            }
            return z;
        }

        /// <summary>
        /// The compute.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex Compute(Complex x)
        {
            Complex res = 0;
            Complex xcoef = 1;
            for (var i = 0; i < Coefficients.Count; i++)
            {
                res += Coefficients[i] * xcoef;
                xcoef *= x;
            }
            return res;
        }

        /// <summary>
        /// The bisection.
        /// </summary>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double? Bisection(double min, double max)
        {
            var minValue = Evaluate(min);
            var maxValue = Evaluate(max);
            double? result = null;

            if (Abs(minValue) <= Tolerance)
            {
                result = min;
            }
            else if (Abs(maxValue) <= Tolerance)
            {
                result = max;
            }
            else if (minValue * maxValue <= 0)
            {
                var tmp1 = Log(max - min);
                const double tmp2 = LN10 * Accuracy;
                var iters = Ceiling((tmp1 + tmp2) / LN2);

                for (var i = 0; i < iters; i++)
                {
                    result = 0.5 * (min + max);
                    var value = Evaluate(result.Value);

                    if (Abs(value) <= Tolerance)
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
        /// The real order.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int RealOrder()
            => RealOrder(Coefficients.ToArray());

        /// <summary>
        /// The real order.
        /// </summary>
        /// <param name="coefficients">The coefficients.</param>
        /// <returns>The <see cref="int"/>.</returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RealOrder(params double[] coefficients)
        {
            if (coefficients is null)
            {
                return 0;
            }

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
        /// Get the min max.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="minY">The minY.</param>
        /// <param name="maxY">The maxY.</param>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetMinMax(double x0, double x1, out double minY, out double maxY)
        {
            var points = Derivate().SolveOrFindRealRoots().Where(t => t > x0 && t < x1).Concat(new[] { x0, x1 });
            var first = true;
            minY = 0;
            maxY = 0;
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
                    {
                        minY = y;
                    }

                    if (y > maxY)
                    {
                        maxY = y;
                    }
                }
            }
        }
        #endregion Specific Methods

        /// <summary>
        /// Will try to solve root analytically, and if it can will use numerical approach.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<double> SolveOrFindRealRoots()
            => CanSolveRealRoots ? SolveRealRoots() : FindRoots().Where(c => Abs(c.Imaginary) < Epsilon).Select(c => c.Real);

        /// <summary>
        /// The solve real roots.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{T}"/>.</returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<double> SolveRealRoots()
            => SolveRealRoots(Coefficients.ToArray());

        /// <summary>
        /// The solve real roots.
        /// </summary>
        /// <param name="coefficients">The coefficients.</param>
        /// <returns>The <see cref="IEnumerable{T}"/>.</returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<double> SolveRealRoots(params double[] coefficients)
        {
            switch (RealOrder(coefficients))
            {
                case 1:
                    yield return -coefficients[0] / coefficients[1];
                    break;
                case 2:
                    {
                        var descriminant = (coefficients[1] * coefficients[1]) - (4 * coefficients[2] * coefficients[0]);
                        if (descriminant < 0)
                        {
                            yield break;
                        }

                        var sd = Sqrt(descriminant);
                        yield return (-coefficients[1] - sd) / 2 / coefficients[2];
                        if (sd > Epsilon)
                        {
                            yield return (-coefficients[1] + sd) / 2 / coefficients[2];
                        }
                    }
                    break;
                case 3:
                    {
                        // http://www.trans4mind.com/personal_development/mathematics/polynomials/cubicAlgebra.htm
                        // http://pomax.github.io/bezierinfo/#extremities
                        // x^3 + a x^2 + b x + c = 0
                        var a = coefficients[2] / coefficients[3];
                        var b = coefficients[1] / coefficients[3];
                        var c = coefficients[0] / coefficients[3];
                        // x = t - a/3
                        // t3 + p t + q = 0
                        var p = (-a * a / 3) + b;
                        var q = ((2 * a * a * a) - (9 * a * b) + (27 * c)) / 27;
                        if (Abs(p) < Epsilon)
                        {
                            // t^3 + q = 0  => t = -q^1/3 => x = -q^1/3 - a/3
                            yield return -Crt(p) - (a / 3);
                        }
                        else if (Abs(q) < Epsilon)
                        {
                            // t^3 + pt = 0  => t (t^2 + p) = 0
                            // t = 0, t = +/- (-p)^1/2
                            yield return -a / 3;
                            if (p < 0)
                            {
                                var root = Crt(p);
                                yield return root - (a / 3);
                                yield return -root - (a / 3);
                            }
                        }
                        else
                        {
                            var disc = (q * q / 4) + (p * p * p / 27);
                            if (disc < -Epsilon)
                            {
                                // 3 roots
                                var r = Sqrt(-p * p * p / 27);
                                var phi = Acos(Operations.MinMax(-q / 2 / r, -1, 1));
                                var t1 = 2 * Crt(r);
                                yield return (t1 * Cos(phi / 3)) - (a / 3);
                                yield return (t1 * Cos((phi + (2 * PI)) / 3)) - (a / 3);
                                yield return (t1 * Cos((phi + (4 * PI)) / 3)) - (a / 3);
                            }
                            else if (disc < Epsilon)
                            {
                                // 2 real roots
                                var cq = Crt(q / 2);
                                yield return (-2 * cq) - (a / 3);
                                yield return cq - (a / 3);
                            }
                            else
                            {
                                // 1 real root
                                var sd = Sqrt(disc);
                                yield return Crt((-q / 2) + sd) - Crt((q / 2) + sd) - (a / 3);
                            }
                        }
                    }
                    break;
                case 4:
                    {
                        // https://en.wikipedia.org/wiki/Quartic_function#General_formula_for_roots
                        // x^4 + b x^3 + c x^2 + d x + e = 0
                        var b = coefficients[3] / coefficients[4];
                        var c = coefficients[2] / coefficients[4];
                        var d = coefficients[1] / coefficients[4];
                        var e = coefficients[0] / coefficients[4];
                        // <=> y^4 + p x^2 + q x + r = 0,
                        // where x = y - b / 4
                        var p = c - (3 * b * b / 8);
                        var q = ((b * b * b) - (4 * b * c) + (8 * d)) / 8;
                        var r = ((-3 * b * b * b * b) + (256 * e) - (64 * b * d) + (16 * b * b * c)) / 256;
                        if (Abs(q) <= Epsilon)
                        {
                            // z = y^2, x = +/- sqrt(z) - b/4, z^2 + p z + r = 0
                            foreach (var z in SolveRealRoots(r, p, 1))
                            {
                                if (z < -Epsilon)
                                {
                                    continue;
                                }
                                if (z <= Epsilon)
                                {
                                    yield return -b / 4;
                                    continue;
                                }
                                var y = Sqrt(z);
                                yield return y - (b / 4);
                                yield return -y - (b / 4);
                            }
                            yield break;
                        }
                        // <=> (y^2 + p + m)^2 = (p + 2m) y^2 -q y + (m^2 + 2 m p + p^2 - r)
                        // where m is **arbitrary** choose it to make perfect square
                        // i.e. m^3 + 5/2 p m^2 + (2 p^2 - r) m + (p^3/2 - p r /2 - q^2 / 8) = 0
                        var m = SolveRealRoots((p * p * p / 2) - (p * r / 2) - (q * q / 8), (2 * p * p) - r, 5.0 / 2.0 * p, 1)
                            .Where(x => p + (2 * x) > Epsilon)
                            .First();
                        // <=> (y^2 + y Sqrt(p+2m) + p+m - q/2/sqrt(p+2m)) (y^2 - y Sqrt(p+2m) + p+m + q/2/sqrt(p+2m))
                        var sqrt = Sqrt(p + (2 * m));
                        var poly1 = SolveRealRoots(p + m - (q / 2 / sqrt), sqrt, 1);
                        var poly2 = SolveRealRoots(p + m + (q / 2 / sqrt), -sqrt, 1);
                        foreach (var y in poly1.Concat(poly2))
                        {
                            yield return y - (b / 4);
                        }
                    }
                    break;
                case 0:
                default:
                    break;
            }
        }

        /// <summary>
        /// This method use the Durand-Kerner aka Weierstrass algorithm to find approximate root of this polynomial.
        /// http://en.wikipedia.org/wiki/Durand%E2%80%93Kerner_method
        /// </summary>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Complex[] FindRoots()
        {
            var p = Normalize();
            if (p.Coefficients.Count == 1)
            {
                return Array.Empty<Complex>();
            }

            Complex x0 = 1;
            var xMul = 0.4 + (0.9 * Complex.ImaginaryOne);
            var R0 = new Complex[p.Coefficients.Count - 1];
            for (var i = 0; i < R0.Length; i++)
            {
                R0[i] = x0;
                x0 *= xMul;
            }

            var R1 = new Complex[p.Coefficients.Count - 1];
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
                    R1[i] = R0[i] - (p.Compute(R0[i]) / divider(i));
                }
            }
            bool closeEnough()
            {
                for (var i = 0; i < R0.Length; i++)
                {
                    var c = R0[i] - R1[i];
                    if (Abs(c.Real) > Epsilon || Abs(c.Imaginary) > Epsilon)
                    {
                        return false;
                    }
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
        /// The roots in interval.
        /// </summary>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public List<double> RootsInInterval(double min, double max)
        {
            var roots = new List<double>();
            double? root;

            if (Degree == 1)
            {
                root = Bisection(min, max);
                if (root != null)
                {
                    roots.Add(root.Value);
                }
            }
            else
            {
                // get roots of derivative
                var deriv = Derivate();
                var droots = deriv.RootsInInterval(min, max);

                if (droots.Count > 0)
                {
                    // find root on [min, droots[0]]
                    root = Bisection(min, droots[0]);
                    if (root != null)
                    {
                        roots.Add(root.Value);
                    }

                    // find root on [droots[i],droots[i+1]] for 0 <= i <= count-2
                    for (var i = 0; i <= droots.Count - 2; i++)
                    {
                        root = Bisection(droots[i], droots[i + 1]);
                        if (root != null)
                        {
                            roots.Add(root.Value);
                        }
                    }

                    // find root on [droots[count-1],xmax]
                    root = Bisection(droots[droots.Count - 1], max);
                    if (root != null)
                    {
                        roots.Add(root.Value);
                    }
                }
                else
                {
                    // polynomial is monotone on [min,max], has at most one root
                    root = Bisection(min, max);
                    if (root != null)
                    {
                        roots.Add(root.Value);
                    }
                }
            }

            return roots;
        }

        /// <summary>
        /// Remove the multiple roots in01.
        /// </summary>
        /// <param name="roots">The roots.</param>
        public static void RemoveMultipleRootsIn01(List<double> roots)
        {
            const double ZEROepsilon = 1e-15;
            roots.Sort();// (a, b)=> { return a - b; });
            for (var i = 1; i < roots.Count;)
            {
                if (Abs(roots[i] - roots[i - 1]) < ZEROepsilon)
                {
                    roots.Remove(i);
                }
                else
                {
                    i++;
                }
            }
        }

        /// <summary>
        /// The trim.
        /// </summary>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomialx Trim()
            => Trim(Epsilon);

        /// <summary>
        /// The trim.
        /// </summary>
        /// <param name="epsilon">The <paramref name="epsilon"/> or minimal value to represent a change.</param>
        /// <returns>The <see cref="Polynomialx"/>.</returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomialx Trim(double epsilon)
        {
            var order = 0;
            for (var i = 0; i < Coefficients.Count; i++)
            {
                if (Abs(Coefficients[i]) > epsilon)
                {
                    order = i;
                }
            }
            var res = new double[order + 1];
            for (var i = 0; i < res.Length; i++)
            {
                if (Abs(Coefficients[i]) > epsilon)
                {
                    res[i] = Coefficients[i];
                }
            }

            return new Polynomialx(res);
        }

        #region Standard Methods
        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, this))
            {
                return true;
            }

            var p = obj as Polynomialx;
            if (p is null)
            {
                return false;
            }

            if (Coefficients.Count != p.Coefficients.Count)
            {
                return false;
            }

            for (var i = 0; i < Coefficients.Count; i++)
            {
                if (Abs(Coefficients[i] - p.Coefficients[i]) > Epsilon)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public override int GetHashCode()
            => Coefficients.GetHashCode();

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Polynomialx"/> inherited class.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(string.Empty /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Polynomialx"/> inherited class based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(IFormatProvider provider)
            => ConvertToString(string.Empty /* format string */, provider);

        /// <summary>
        /// Creates a string representation of this <see cref="Polynomialx"/> inherited class based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
            => ConvertToString(format /* format string */, provider /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Polynomialx"/> inherited class based on the format string
        /// and IFormatProvider passed in.
        /// If the provider is null, the CurrentCulture is used.
        /// See the documentation for IFormattable for more information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// A string representation of this object.
        /// </returns>
        public virtual string ConvertToString(string format, IFormatProvider provider)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < Coefficients.Count; i++)
            {
                var val = Coefficients[i];
                if (Abs(val) < Epsilon)
                {
                    continue;
                }

                if (val > 0 && sb.Length > 0)
                {
                    sb.Append('+');
                }

                if (i > 0 && (Abs(val) - 1) < Epsilon)
                {
                    if (val < 0)
                    {
                        sb.Append('-');
                    }
                }
                else
                {
                    sb.Append(val);
                }
                if (i > 0)
                {
                    sb.Append('x');
                }

                if (i > 1)
                {
                    sb.Append('^').Append(i);
                }
            }
            if (sb.Length == 0)
            {
                sb.Append('0');
            }

            return sb.ToString();
        }
        #endregion Standard Methods
    }
}
