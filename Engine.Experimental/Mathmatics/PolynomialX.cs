// <copyright file="Polynomial.cs" >
//     Copyright (c) 2007 hanzzoid. All rights reserved.
// </copyright>
// <license>
//     Licensed under the Code Project Open License (CPOL). See http://www.codeproject.com/info/cpol10.aspx for full license information.
// </license>
// <author id="hanzzoid">hanzzoid</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security;

namespace Engine.Geometry
{
    /// <summary>
    /// The polynomial x class.
    /// </summary>
    public class PolynomialX
        : IFormattable
    {
        /// <summary>
        /// The TOLERANCE (const). Value: 1e-6.
        /// </summary>
        private const double tolerance = 1e-6;

        /// <summary>
        /// The ACCURACY (const). Value: 15.
        /// </summary>
        private const double accuracy = 15;

        #region Fields
        /// <summary>
        /// Coefficients a_0,...,a_n of a polynomial p, such that
        /// p(x) = a_0 + a_1*x + a_2*x^2 + ... + a_n*x^n.
        /// </summary>
        public Complex[] Coefficients;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Inits zero polynomial p = 0.
        /// </summary>
        public PolynomialX()
        {
            Coefficients = new Complex[1];
            Coefficients[0] = Complex.Zero;
        }

        /// <summary>
        /// Inits polynomial from given complex coefficient array.
        /// </summary>
        /// <param name="coeffs"></param>
        public PolynomialX(params Complex[] coeffs)
        {
            if (coeffs is null || coeffs.Length < 1)
            {
                Coefficients = new Complex[1];
                Coefficients[0] = Complex.Zero;
            }
            else
            {
                Coefficients = (Complex[])coeffs.Clone();
            }
        }

        /// <summary>
        /// Inits polynomial from given real coefficient array.
        /// </summary>
        /// <param name="coeffs"></param>
        public PolynomialX(params double[] coeffs)
        {
            if (coeffs is null || coeffs.Length < 1)
            {
                Coefficients = new Complex[1];
                Coefficients[0] = Complex.Zero;
            }
            else
            {
                Coefficients = new Complex[coeffs.Length];
                for (var i = 0; i < coeffs.Length; i++)
                {
                    Coefficients[i] = new Complex(coeffs[i]);
                }
            }
        }

        /// <summary>
        /// Inits constant polynomial.
        /// </summary>
        /// <param name="coeffs"></param>
        public PolynomialX(Complex coeffs)
        {
            Coefficients = new Complex[1];

            if (coeffs == null)
            {
                Coefficients[0] = Complex.Zero;
            }
            else
            {
                Coefficients[0] = coeffs;
            }
        }

        /// <summary>
        /// Inits constant polynomial.
        /// </summary>
        /// <param name="coeffs"></param>
        public PolynomialX(double coeffs)
        {
            Coefficients = new Complex[1];

            Coefficients[0] = new Complex(coeffs);
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Degree of the polynomial.
        /// </summary>
        public int Degree
            => Coefficients.Length - 1;

        /// <summary>
        /// Checks if given polynomial is zero.
        /// </summary>
        /// <returns></returns>
        public bool IsZero
        {
            get
            {
                for (var i = 0; i < Coefficients.Length; i++)
                {
                    if (Coefficients[i] != 0)
                    {
                        return false;
                    }
                }

                return true;
            }
        }
        #endregion Properties

        #region Operators
        /// <summary>
        /// The operator +.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="q">The q.</param>
        /// <returns>The <see cref="PolynomialX"/>.</returns>
        public static PolynomialX operator +(PolynomialX p, PolynomialX q)
        {
            if (p is null || q is null) return null;
            var degree = Math.Max(p.Degree, q.Degree);

            var coeffs = new Complex[degree + 1];

            for (var i = 0; i <= degree; i++)
            {
                if (i > p.Degree)
                {
                    coeffs[i] = q.Coefficients[i];
                }
                else if (i > q.Degree)
                {
                    coeffs[i] = p.Coefficients[i];
                }
                else
                {
                    coeffs[i] = p.Coefficients[i] + q.Coefficients[i];
                }
            }

            return new PolynomialX(coeffs);
        }

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="q">The q.</param>
        /// <returns>The <see cref="PolynomialX"/>.</returns>
        public static PolynomialX operator -(PolynomialX p, PolynomialX q)
            => p + (-q);

        ///// <summary>
        ///// The operator +.
        ///// </summary>
        ///// <param name="p">The p.</param>
        ///// <returns>The <see cref="PolynomialX"/>.</returns>
        //public static PolynomialX operator +(PolynomialX p)
        //{
        //    var coeffs = new Complex[p.Degree + 1];

        //    for (var i = 0; i < coeffs.Length; i++)
        //    {
        //        coeffs[i] = +p.Coefficients[i];
        //    }

        //    return new PolynomialX(coeffs);
        //}

        /// <summary>
        /// The operator -.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns>The <see cref="PolynomialX"/>.</returns>
        public static PolynomialX operator -(PolynomialX p)
        {
            if (p is null) return null;
            var coeffs = new Complex[p.Degree + 1];

            for (var i = 0; i < coeffs.Length; i++)
            {
                coeffs[i] = -p.Coefficients[i];
            }

            return new PolynomialX(coeffs);
        }

        /// <summary>
        /// The operator *.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="p">The p.</param>
        /// <returns>The <see cref="PolynomialX"/>.</returns>
        public static PolynomialX operator *(Complex d, PolynomialX p)
        {
            if (p is null) return null;
            var coeffs = new Complex[p.Degree + 1];

            for (var i = 0; i < coeffs.Length; i++)
            {
                coeffs[i] = d * p.Coefficients[i];
            }

            return new PolynomialX(coeffs);
        }

        /// <summary>
        /// The operator *.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="d">The d.</param>
        /// <returns>The <see cref="PolynomialX"/>.</returns>
        public static PolynomialX operator *(PolynomialX p, Complex d)
        {
            if (p is null) return null;
            var coeffs = new Complex[p.Degree + 1];

            for (var i = 0; i < coeffs.Length; i++)
            {
                coeffs[i] = d * p.Coefficients[i];
            }

            return new PolynomialX(coeffs);
        }

        /// <summary>
        /// The operator *.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="p">The p.</param>
        /// <returns>The <see cref="PolynomialX"/>.</returns>
        public static PolynomialX operator *(double d, PolynomialX p)
        {
            if (p is null) return null;
            var coeffs = new Complex[p.Degree + 1];

            for (var i = 0; i < coeffs.Length; i++)
            {
                coeffs[i] = d * p.Coefficients[i];
            }

            return new PolynomialX(coeffs);
        }

        /// <summary>
        /// The operator *.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="d">The d.</param>
        /// <returns>The <see cref="PolynomialX"/>.</returns>
        public static PolynomialX operator *(PolynomialX p, double d)
        {
            if (p is null) return null;
            var coeffs = new Complex[p.Degree + 1];

            for (var i = 0; i < coeffs.Length; i++)
            {
                coeffs[i] = d * p.Coefficients[i];
            }

            return new PolynomialX(coeffs);
        }

        /// <summary>
        /// The operator /.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="d">The d.</param>
        /// <returns>The <see cref="PolynomialX"/>.</returns>
        public static PolynomialX operator /(PolynomialX p, Complex d)
        {
            if (p is null) return null;
            var coeffs = new Complex[p.Degree + 1];

            for (var i = 0; i < coeffs.Length; i++)
            {
                coeffs[i] = p.Coefficients[i] / d;
            }

            return new PolynomialX(coeffs);
        }

        /// <summary>
        /// The operator /.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="d">The d.</param>
        /// <returns>The <see cref="PolynomialX"/>.</returns>
        public static PolynomialX operator /(PolynomialX p, double d)
        {
            if (p is null) return null;
            var coeffs = new Complex[p.Degree + 1];

            for (var i = 0; i < coeffs.Length; i++)
            {
                coeffs[i] = p.Coefficients[i] / d;
            }

            return new PolynomialX(coeffs);
        }

        /// <summary>
        /// The operator *.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="q">The q.</param>
        /// <returns>The <see cref="PolynomialX"/>.</returns>
        public static PolynomialX operator *(PolynomialX p, PolynomialX q)
        {
            if (p is null || q is null) return null;
            _ = p.Degree + q.Degree;

            var r = new PolynomialX();

            for (var i = 0; i <= p.Degree; i++)
            {
                for (var j = 0; j <= q.Degree; j++)
                {
                    r += p.Coefficients[i] * q.Coefficients[j] * Monomial(i + j);
                }
            }

            return r;
        }

        /// <summary>
        /// The operator ^.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="k">The k.</param>
        /// <returns>The <see cref="PolynomialX"/>.</returns>
        public static PolynomialX operator ^(PolynomialX p, uint k)
        {
            if (k == 0)
            {
                return Monomial(0);
            }
            else if (k == 1)
            {
                return p;
            }
            else
            {
                return p * (p ^ (k - 1));
            }
        }
        #endregion Operators

        #region Dynamics
        /// <summary>
        /// Computes value of the differentiated polynomial at x.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public Complex Differentiate(Complex x)
        {
            var buf = new Complex[Degree];

            for (var i = 0; i < buf.Length; i++)
            {
                buf[i] = (i + 1) * Coefficients[i + 1];
            }

            return new PolynomialX(buf).Evaluate(x);
        }

        /// <summary>
        /// Computes the definite integral within the borders a and b.
        /// </summary>
        /// <param name="a">Left integration border.</param>
        /// <param name="b">Right integration border.</param>
        /// <returns></returns>
        public Complex Integrate(Complex a, Complex b)
        {
            var buf = new Complex[Degree + 2];
            buf[0] = Complex.Zero; // this value can be arbitrary, in fact

            for (var i = 1; i < buf.Length; i++)
            {
                buf[i] = Coefficients[i - 1] / i;
            }

            var p = new PolynomialX(buf);

            return p.Evaluate(b) - p.Evaluate(a);
        }

        /// <summary>
        /// Evaluates polynomial by using the horner scheme.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public Complex Evaluate(Complex x)
        {
            var buf = Coefficients[Degree];

            for (var i = Degree - 1; i >= 0; i--)
            {
                buf = Coefficients[i] + (x * buf);
            }

            return buf;
        }

        /// <summary>
        /// Normalizes the polynomial, e.i. divides each coefficient by the
        /// coefficient of a_n the greatest term if a_n != 1.
        /// </summary>
        public void Normalize()
        {
            Clean();

            if (Coefficients[Degree] != Complex.One)
            {
                for (var k = 0; k <= Degree; k++)
                {
                    Coefficients[k] /= Coefficients[Degree];
                }
            }
        }

        /// <summary>
        /// Removes unnecessary zero terms.
        /// </summary>
        public void Clean()
        {
            int i;

            for (i = Degree; i >= 0 && Coefficients[i] == 0; i--)
            { }

            var coeffs = new Complex[i + 1];

            for (var k = 0; k <= i; k++)
            {
                coeffs[k] = Coefficients[k];
            }

            Coefficients = (Complex[])coeffs.Clone();
        }

        /// <summary>
        /// Factorizes polynomial to its linear factors.
        /// </summary>
        /// <returns></returns>
        public FactorizedPolynomialX Factorize()
        {
            // this is to be returned
            var p = new FactorizedPolynomialX();

            // cannot factorize polynomial of degree 0 or 1
            if (Degree <= 1)
            {
                p.Factor = new PolynomialX[] { this };
                p.Power = new int[] { 1 };

                return p;
            }

            var roots = Roots(this);

            //ArrayList rootlist = new ArrayList();
            //foreach (Complex z in roots) rootlist.Add(z);

            //roots = null; // don't need you anymore

            //rootlist.Sort();

            //// number of different roots
            //int num = 1; // ... at least one

            //// ...or more?
            //for (int i = 1; i < rootlist.Count; i++)
            //    if (rootlist[i] != rootlist[i - 1]) num++;

            var factor = new PolynomialX[roots.Length];
            var power = new int[roots.Length];

            //factor[0] = new Polynomial( new Complex[]{ -(Complex)rootlist[0] * Coefficients[Degree],
            //    Coefficients[Degree] } );
            //power[0] = 1;

            //num = 1;
            //len = 0;
            //for (int i = 1; i < rootlist.Count; i++)
            //{
            //    len++;
            //    if (rootlist[i] != rootlist[i - 1])
            //    {
            //        factor[num] = new Polynomial(new Complex[] { -(Complex)rootlist[i], Complex.One });
            //        power[num] = len;
            //        num++;
            //        len = 0;
            //    }
            //}

            power[0] = 1;
            factor[0] = new PolynomialX(new Complex[] { -Coefficients[Degree] * (Complex)roots[0], Coefficients[Degree] });

            for (var i = 1; i < roots.Length; i++)
            {
                power[i] = 1;
                factor[i] = new PolynomialX(new Complex[] { -(Complex)roots[i], Complex.One });
            }

            p.Factor = factor;
            p.Power = power;

            return p;
        }

        /// <summary>
        /// Computes the roots of polynomial via Weierstrass iteration.
        /// </summary>        
        /// <returns></returns>
        public Complex[] Roots()
        {
            var tolerance = 1e-12;
            var max_iterations = 30;

            var q = Normalize(this);
            //PolynomialX q = p;

            var z = new Complex[q.Degree]; // approx. for roots
            var w = new Complex[q.Degree]; // Weierstraß corrections

            // init z
            for (var k = 0; k < q.Degree; k++)
            {
                //z[k] = (new Complex(.4, .9)) ^ k;
                z[k] = Complex.Exp(2 * Math.PI * Complex.I * k / q.Degree);
            }

            for (var iter = 0; iter < max_iterations
                && MaxValue(q, z) > tolerance; iter++)
            {
                for (var i = 0; i < 10; i++)
                {
                    for (var k = 0; k < q.Degree; k++)
                    {
                        w[k] = q.Evaluate(z[k]) / WeierNull(z, k);
                    }

                    for (var k = 0; k < q.Degree; k++)
                    {
                        z[k] -= w[k];
                    }
                }
            }

            // clean...
            for (var k = 0; k < q.Degree; k++)
            {
                z[k].Real = Math.Round(z[k].Real, 12);
                z[k].Imaginary = Math.Round(z[k].Imaginary, 12);
            }

            return z;
        }

        /// <summary>
        /// Computes the roots of polynomial p via Weierstrass iteration.
        /// </summary>
        /// <param name="tolerance">Computation precision; e.g. 1e-12 denotes 12 exact digits.</param>
        /// <param name="max_iterations">Maximum number of iterations; this value is used to bound
        /// the computation effort if desired precision is hard to achieve.</param>
        /// <returns></returns>
        public Complex[] Roots(double tolerance, int max_iterations)
        {
            var q = Normalize(this);

            var z = new Complex[q.Degree]; // approx. for roots
            var w = new Complex[q.Degree]; // Weierstraß corrections

            // init z
            for (var k = 0; k < q.Degree; k++)
            {
                //z[k] = (new Complex(.4, .9)) ^ k;
                z[k] = Complex.Exp(2 * Math.PI * Complex.I * k / q.Degree);
            }

            for (var iter = 0; iter < max_iterations
                && MaxValue(q, z) > tolerance; iter++)
            {
                for (var i = 0; i < 10; i++)
                {
                    for (var k = 0; k < q.Degree; k++)
                    {
                        w[k] = q.Evaluate(z[k]) / WeierNull(z, k);
                    }

                    for (var k = 0; k < q.Degree; k++)
                    {
                        z[k] -= w[k];
                    }
                }
            }

            // clean...
            for (var k = 0; k < q.Degree; k++)
            {
                z[k].Real = Math.Round(z[k].Real, 12);
                z[k].Imaginary = Math.Round(z[k].Imaginary, 12);
            }

            return z;
        }
        #endregion Dynamics

        #region Statics
        /// <summary>
        /// Expands factorized polynomial p_1(x)^(k_1)*...*p_r(x)^(k_r) to its normal form a_0 + a_1 x + ... + a_n x^n.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static PolynomialX Expand(FactorizedPolynomialX p)
        {
            if (p is null) return null;
            var q = new PolynomialX(new Complex[] { Complex.One });

            for (var i = 0; i < p.Factor.Length; i++)
            {
                for (var j = 0; j < p.Power[i]; j++)
                {
                    q *= p.Factor[i];
                }

                q.Clean();
            }

            // clean...
            for (var k = 0; k <= q.Degree; k++)
            {
                q.Coefficients[k].Real = Math.Round(q.Coefficients[k].Real, 12);
                q.Coefficients[k].Imaginary = Math.Round(q.Coefficients[k].Imaginary, 12);
            }

            return q;
        }

        /// <summary>
        /// Evaluates factorized polynomial p at point x.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Complex Evaluate(FactorizedPolynomialX p, Complex x)
        {
            var z = Complex.One;

            for (var i = 0; i < p.Factor.Length; i++)
            {
                z *= Complex.Pow(p.Factor[i].Evaluate(x), p.Power[i]);
            }

            return z;
        }

        /// <summary>
        /// Removes unnecessary leading zeros.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static PolynomialX Clean(PolynomialX p)
        {
            if (p is null) return null;
            int i;

            for (i = p.Degree; i >= 0 && p.Coefficients[i] == 0; i--)
            {
                ;
            }

            var coeffs = new Complex[i + 1];

            for (var k = 0; k <= i; k++)
            {
                coeffs[k] = p.Coefficients[k];
            }

            return new PolynomialX(coeffs);
        }

        /// <summary>
        /// Normalizes the polynomial, e.i. divides each coefficient by the
        /// coefficient of a_n the greatest term if a_n != 1.
        /// </summary>
        public static PolynomialX Normalize(PolynomialX p)
        {
            var q = Clean(p);

            if (q.Coefficients[q.Degree] != Complex.One)
            {
                for (var k = 0; k <= q.Degree; k++)
                {
                    q.Coefficients[k] /= q.Coefficients[q.Degree];
                }
            }

            return q;
        }

        /// <summary>
        /// Computes the roots of polynomial p via Weierstrass iteration.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Complex[] Roots(PolynomialX p)
        {
            var tolerance = 1e-12;
            var max_iterations = 30;

            var q = Normalize(p);
            //PolynomialX q = p;

            var z = new Complex[q.Degree]; // approx. for roots
            var w = new Complex[q.Degree]; // Weierstraß corrections

            // init z
            for (var k = 0; k < q.Degree; k++)
            {
                //z[k] = (new Complex(.4, .9)) ^ k;
                z[k] = Complex.Exp(2 * Math.PI * Complex.I * k / q.Degree);
            }

            for (var iter = 0; iter < max_iterations
                && MaxValue(q, z) > tolerance; iter++)
            {
                for (var i = 0; i < 10; i++)
                {
                    for (var k = 0; k < q.Degree; k++)
                    {
                        w[k] = q.Evaluate(z[k]) / WeierNull(z, k);
                    }

                    for (var k = 0; k < q.Degree; k++)
                    {
                        z[k] -= w[k];
                    }
                }
            }

            // clean...
            for (var k = 0; k < q.Degree; k++)
            {
                z[k].Real = Math.Round(z[k].Real, 12);
                z[k].Imaginary = Math.Round(z[k].Imaginary, 12);
            }

            return z;
        }

        /// <summary>
        /// Computes the roots of polynomial p via Weierstrass iteration.
        /// </summary>
        /// <param name="p">Polynomial to compute the roots of.</param>
        /// <param name="tolerance">Computation precision; e.g. 1e-12 denotes 12 exact digits.</param>
        /// <param name="max_iterations">Maximum number of iterations; this value is used to bound
        /// the computation effort if desired precision is hard to achieve.</param>
        /// <returns></returns>
        public static Complex[] Roots(PolynomialX p, double tolerance, int max_iterations)
        {
            var q = Normalize(p);

            var z = new Complex[q.Degree]; // approx. for roots
            var w = new Complex[q.Degree]; // Weierstraß corrections

            // init z
            for (var k = 0; k < q.Degree; k++)
            {
                //z[k] = (new Complex(.4, .9)) ^ k;
                z[k] = Complex.Exp(2 * Math.PI * Complex.I * k / q.Degree);
            }

            for (var iter = 0; iter < max_iterations
                && MaxValue(q, z) > tolerance; iter++)
            {
                for (var i = 0; i < 10; i++)
                {
                    for (var k = 0; k < q.Degree; k++)
                    {
                        w[k] = q.Evaluate(z[k]) / WeierNull(z, k);
                    }

                    for (var k = 0; k < q.Degree; k++)
                    {
                        z[k] -= w[k];
                    }
                }
            }

            // clean...
            for (var k = 0; k < q.Degree; k++)
            {
                z[k].Real = Math.Round(z[k].Real, 12);
                z[k].Imaginary = Math.Round(z[k].Imaginary, 12);
            }

            return z;
        }

        /// <summary>
        /// Computes the greatest value |p(z_k)|.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static double MaxValue(PolynomialX p, Complex[] z)
        {
            double buf = 0;

            for (var i = 0; i < z.Length; i++)
            {
                if (Complex.Abs(p.Evaluate(z[i])) > buf)
                {
                    buf = Complex.Abs(p.Evaluate(z[i]));
                }
            }

            return buf;
        }

        /// <summary>
        /// For g(x) = (x-z_0)*...*(x-z_n), this method returns
        /// g'(z_k) = \prod_{j != k} (z_k - z_j).
        /// </summary>
        /// <param name="z"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        private static Complex WeierNull(Complex[] z, int k)
        {
            if (k < 0 || k >= z.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(k));
            }

            var buf = Complex.One;

            for (var j = 0; j < z.Length; j++)
            {
                if (j != k)
                {
                    buf *= z[k] - z[j];
                }
            }

            return buf;
        }

        /// <summary>
        /// Differentiates given polynomial p.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static PolynomialX Derivative(PolynomialX p)
        {
            if (p is null) return null;
            var buf = new Complex[p.Degree];

            for (var i = 0; i < buf.Length; i++)
            {
                buf[i] = (i + 1) * p.Coefficients[i + 1];
            }

            return new PolynomialX(buf);
        }

        /// <summary>
        /// Integrates given polynomial p.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static PolynomialX Integral(PolynomialX p)
        {
            if (p is null) return null;
            var buf = new Complex[p.Degree + 2];
            buf[0] = Complex.Zero; // this value can be arbitrary, in fact

            for (var i = 1; i < buf.Length; i++)
            {
                buf[i] = p.Coefficients[i - 1] / i;
            }

            return new PolynomialX(buf);
        }

        /// <summary>
        /// Computes the monomial x^degree.
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public static PolynomialX Monomial(int degree)
        {
            if (degree == 0)
            {
                return new PolynomialX(1);
            }

            var coeffs = new Complex[degree + 1];

            for (var i = 0; i < degree; i++)
            {
                coeffs[i] = Complex.Zero;
            }

            coeffs[degree] = Complex.One;

            return new PolynomialX(coeffs);
        }

        /// <summary>
        /// Get the standard base.
        /// </summary>
        /// <param name="dim">The dim.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        /// <exception cref="ArgumentException">Dimension expected to be greater than zero.</exception>
        public static PolynomialX[] GetStandardBase(int dim)
        {
            if (dim < 1)
            {
                throw new ArgumentException("Dimension expected to be greater than zero.");
            }

            var buf = new PolynomialX[dim];

            for (var i = 0; i < dim; i++)
            {
                buf[i] = Monomial(i);
            }

            return buf;
        }
        #endregion Statics

        /// <summary>
        /// The interpolate.
        /// </summary>
        /// <param name="xs">The xs.</param>
        /// <param name="ys">The ys.</param>
        /// <param name="n">The n.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="x">The x.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        /// <exception cref="Exception">Polynomial.interpolate: xs and ys must be arrays</exception>
        /// <exception cref="Exception">Polynomial.interpolate: n, offset, and x must be numbers</exception>
        /// <remarks>
        /// <para>https://github.com/thelonious/kld-polynomial</para>
        /// </remarks>
        public static (Complex y, Complex dy) Interpolate(Complex[] xs, Complex[] ys, int n, int offset, double x)
        {
            if (!(xs is Array) || !(ys is Array))
            {
                throw new Exception("Polynomial.interpolate: xs and ys must be arrays");
            }

            if (double.IsNaN(n) || double.IsNaN(offset) || double.IsNaN(x))
            {
                throw new Exception("Polynomial.interpolate: n, offset, and x must be numbers");
            }

            var c = new Complex[n];
            var d = new Complex[n];
            var ns = 0;
            (double y, double dy) result = (0, 0);

            var diff = Math.Abs(x - xs[offset].Real);
            for (var i = 0; i < n; i++)
            {
                var dift = Math.Abs(x - xs[offset + i].Real);

                if (dift < diff)
                {
                    ns = i;
                    diff = dift;
                }
                c[i] = d[i] = ys[offset + i].Real;
            }
            Complex y = ys[offset + ns].Real;
            ns--;

            for (var m = 1; m < n; m++)
            {
                for (var i = 0; i < n - m; i++)
                {
                    var ho = xs[offset + i] - x;
                    var hp = xs[offset + i + m] - x;
                    var w = c[i + 1] - d[i];
                    var den = ho - hp;

                    if (den == 0d)
                    {
                        result = (y: 0d, dy: 0d);
                        break;
                    }

                    den = w / den;
                    d[i] = hp * den;
                    c[i] = ho * den;
                }
                var dy = (2 * (ns + 1) < (n - m)) ? c[ns + 1] : d[ns--];
                y += dy;
            }

            return result;
        }

        /// <summary>
        /// The eval.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        /// <exception cref="Exception">Polynomial.eval: parameter must be a number</exception>
        /// <remarks>
        /// <para>https://github.com/thelonious/kld-polynomial</para>
        /// </remarks>
        public Complex Eval(Complex x)
        {
            if (double.IsNaN(x.Real))
            {
                throw new Exception("Polynomial.eval: parameter must be a number");
            }

            var result = Complex.Zero;

            for (var i = Coefficients.Length - 1; i >= 0; i--)
            {
                result = (result * x) + Coefficients[i];
            }

            return result;
        }

        /// <summary>
        /// Negates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static PolynomialX Negate(PolynomialX item) => -item;

        ///// <summary>
        ///// Pluses the specified item.
        ///// </summary>
        ///// <param name="item">The item.</param>
        ///// <returns></returns>
        //public static PolynomialX Plus(PolynomialX item) => +item;

        /// <summary>
        /// Subtracts the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static PolynomialX Subtract(PolynomialX left, PolynomialX right) => left - right;

        /// <summary>
        /// Adds the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static PolynomialX Add(PolynomialX left, PolynomialX right) => left + right;

        /// <summary>
        /// Xors the specified left.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns></returns>
        public static PolynomialX Xor(PolynomialX left, uint right) => left ^ right;

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="that">The that.</param>
        /// <returns>The <see cref="PolynomialX"/>.</returns>
        public PolynomialX Add(PolynomialX that)
        {
            if (that is null) return null;
            var result = new PolynomialX();
            var d1 = GetDegree();
            var d2 = that.GetDegree();
            var dmax = Math.Max(d1, d2);

            for (var i = 0; i <= dmax; i++)
            {
                var v1 = (i <= d1) ? Coefficients[i] : 0;
                var v2 = (i <= d2) ? that.Coefficients[i] : 0;

                result.Coefficients[i] = v1 + v2;
            }

            return result;
        }

        /// <summary>
        /// The multiply.
        /// </summary>
        /// <param name="that">The that.</param>
        /// <returns>The <see cref="PolynomialX"/>.</returns>
        public PolynomialX Multiply(PolynomialX that)
        {
            if (that is null) return null;
            var result = new PolynomialX();
            var lst = new List<Complex>(that.Coefficients);

            for (var i = 0; i <= GetDegree() + that.GetDegree(); i++)
            {
                lst.Add(0);
            }

            result.Coefficients = lst.ToArray();

            for (var i = 0; i <= GetDegree(); i++)
            {
                for (var j = 0; j <= that.GetDegree(); j++)
                {
                    result.Coefficients[i + j] += Coefficients[i] * that.Coefficients[j];
                }
            }

            return result;
        }

        /// <summary>
        /// The divide.
        /// </summary>
        /// <param name="scalar">The scalar.</param>
        /// <returns>The <see cref="PolynomialX"/>.</returns>
        public PolynomialX Divide(double scalar)
        {
            for (var i = 0; i < Coefficients.Length; i++)
            {
                Coefficients[i] /= scalar;
            }

            return this;
        }

        /// <summary>
        /// The simplify.
        /// </summary>
        public void Simplify()
        {
            var TOLERANCE = 1e-15;
            for (var i = GetDegree(); i >= 0; i--)
            {
                if (Math.Abs(Coefficients[i].Real) <= TOLERANCE)
                {
                    var temp = new Stack<Complex>(Coefficients);
                    temp.Pop();
                    Coefficients = temp.ToArray();
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// The bisection.
        /// </summary>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        public Complex Bisection(Complex min, Complex max)
        {
            var minValue = Eval(min);
            var maxValue = Eval(max);
            Complex result = 0;

            if (Math.Abs(minValue.Real) <= tolerance)
            {
                result = min;
            }
            else if (Math.Abs(maxValue.Real) <= tolerance)
            {
                result = max;
            }
            else if (minValue.Real * maxValue.Real <= 0)
            {
                var tmp1 = Math.Log(max.Real - min.Real);
                var tmp2 = Mathematics.LN10 * accuracy;
                var iters = Math.Ceiling((tmp1 + tmp2) / Mathematics.LN2);

                for (var i = 0; i < iters; i++)
                {
                    result = 0.5 * (min + max);
                    var value = Eval(result);

                    if (Math.Abs(value.Real) <= tolerance)
                    {
                        break;
                    }

                    if (value.Real * minValue.Real < 0)
                    {
                        max = result;
                        maxValue = value;
                    }
                    else
                    {
                        min = result;
                        minValue = value;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// The trapezoid.
        /// </summary>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <param name="n">The n.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        /// <exception cref="Exception">Polynomial.trapezoid: parameters must be numbers</exception>
        /// <exception cref="Exception">Polynomial.trapezoid: this._s is NaN</exception>
        public Complex Trapezoid(Complex min, Complex max, int n)
        {
            if (double.IsNaN(min.Real) || double.IsNaN(max.Real) || double.IsNaN(n))
            {
                throw new Exception("Polynomial.trapezoid: parameters must be numbers");
            }

            var range = max - min;
            Complex _s = 0;
            if (n == 1)
            {
                var minValue = Eval(min);
                var maxValue = Eval(max);
                _s = 0.5 * range * (minValue + maxValue);
            }
            else
            {
                var it = 1 << (n - 2);
                var delta = range / it;
                var x = min + (0.5 * delta);
                Complex sum = 0;

                for (var i = 0; i < it; i++)
                {
                    sum += Eval(x);
                    x += delta;
                }
                _s = 0.5 * (_s + (range * sum / it));
            }

            if (double.IsNaN(_s.Real))
            {
                throw new Exception("Polynomial.trapezoid: this._s is NaN");
            }

            return _s;
        }

        /// <summary>
        /// The simpson.
        /// </summary>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        /// <exception cref="Exception">Polynomial.simpson: parameters must be numbers</exception>
        public Complex Simpson(Complex min, Complex max)
        {
            if (double.IsNaN(min.Real) || double.IsNaN(max.Real))
            {
                throw new Exception("Polynomial.simpson: parameters must be numbers");
            }

            var range = max - min;
            var st = 0.5 * range * (Eval(min) + Eval(max));
            var t = st;
            var s = 4d * st / 3d;
            var os = s;
            var ost = st;
            var TOLERANCE = 1e-7;

            var it = 1;
            for (var n = 2; n <= 20; n++)
            {
                var delta = range / it;
                var x = min + (0.5 * delta);
                Complex sum = 0;

                for (var i = 1; i <= it; i++)
                {
                    sum += Eval(x);
                    x += delta;
                }

                t = 0.5 * (t + (range * sum / it));
                st = t;
                s = ((4.0 * st) - ost) / 3.0;

                if (Math.Abs(s.Real - os.Real) < TOLERANCE * Math.Abs(os.Real))
                {
                    break;
                }

                os = s;
                ost = st;
                it <<= 1;
            }

            return s;
        }

        /// <summary>
        /// The romberg.
        /// </summary>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="Complex"/>.</returns>
        /// <exception cref="Exception">Polynomial.romberg: parameters must be numbers</exception>
        public Complex Romberg(Complex min, Complex max)
        {
            if (double.IsNaN(min.Real) || double.IsNaN(max.Real))
            {
                throw new Exception("Polynomial.romberg: parameters must be numbers");
            }

            var MAX = 20;
            var K = 3;
            var TOLERANCE = 1e-6;
            var s = new Complex[MAX + 1];
            var h = new Complex[MAX + 1];
            (Complex y, Complex dy) result = (y: 0, dy: 0);

            h[0] = 1.0;
            for (var j = 1; j <= MAX; j++)
            {
                s[j - 1] = Trapezoid(min, max, j);
                if (j >= K)
                {
                    result = Interpolate(h, s, K, j - K, 0.0);
                    if (Math.Abs(result.dy.Real) <= TOLERANCE * result.y.Real)
                    {
                        break;
                    }
                }
                s[j] = s[j - 1];
                h[j] = 0.25 * h[j - 1];
            }

            return result.y;
        }

        // getters and setters

        /// <summary>
        /// Get the degree.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetDegree()
            => Coefficients.Length - 1;

        /// <summary>
        /// Get the derivative.
        /// </summary>
        /// <returns>The <see cref="PolynomialX"/>.</returns>
        public PolynomialX GetDerivative()
        {
            var lst = new List<Complex>();

            for (var i = 1; i < Coefficients.Length; i++)
            {
                lst.Add(i * Coefficients[i]);
            }
            var derivative = new PolynomialX(lst.ToArray());

            return derivative;
        }

        /// <summary>
        /// Get the roots.
        /// </summary>
        /// <returns>The <see cref="Array"/>.</returns>
        public Complex[] GetRoots()
        {
            Simplify();
            Complex[] result = (GetDegree()) switch
            {
                0 => Array.Empty<Complex>(),
                1 => GetLinearRoot(),
                2 => GetQuadraticRoots(),
                _ => Array.Empty<Complex>(),
            };
            return result;
        }

        /// <summary>
        /// Get the roots in interval.
        /// </summary>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        /// <returns>The <see cref="Array"/>.</returns>
        public Complex[] GetRootsInInterval(Complex min, Complex max)
        {
            var roots = new List<Complex>();
            Complex root;

            if (GetDegree() == 1)
            {
                root = Bisection(min, max);
                if (root != null)
                {
                    roots.Add(root);
                }
            }
            else
            {
                // get roots of derivative
                var deriv = GetDerivative();
                var droots = deriv.GetRootsInInterval(min, max);

                if (droots.Length > 0)
                {
                    // find root on [min, droots[0]]
                    root = Bisection(min, droots[0].Real);
                    if (root != null)
                    {
                        roots.Add(root);
                    }

                    // find root on [droots[i],droots[i+1]] for 0 <= i <= count-2
                    for (var i = 0; i <= droots.Length - 2; i++)
                    {
                        root = Bisection(droots[i].Real, droots[i + 1].Real);
                        if (root != null)
                        {
                            roots.Add(root);
                        }
                    }

                    // find root on [droots[count-1],xmax]
                    root = Bisection(droots[droots.Length - 1].Real, max);
                    if (root != null)
                    {
                        roots.Add(root);
                    }
                }
                else
                {
                    // polynomial is monotone on [min,max], has at most one root
                    root = Bisection(min, max);
                    if (root != null)
                    {
                        roots.Add(root);
                    }
                }
            }

            return roots.ToArray();
        }

        /// <summary>
        /// Get the linear root.
        /// </summary>
        /// <returns>The <see cref="Array"/>.</returns>
        public Complex[] GetLinearRoot()
        {
            var result = new List<Complex>();
            var a = Coefficients[1];

            if (a != 0)
            {
                result.Add(-Coefficients[0] / a);
            }

            return result.ToArray();
        }

        /// <summary>
        /// Get the quadratic roots.
        /// </summary>
        /// <returns>The <see cref="Array"/>.</returns>
        public Complex[] GetQuadraticRoots()
        {
            var results = new List<Complex>();

            if (GetDegree() == 2)
            {
                var a = Coefficients[2];
                var b = Coefficients[1] / a;
                var c = Coefficients[0] / a;
                var d = (b * b) - (4d * c);

                if (d.Real > 0)
                {
                    var e = Math.Sqrt(d.Real);

                    results.Add(0.5 * (-b + e));
                    results.Add(0.5 * (-b - e));
                }
                else if (d == 0)
                {
                    // really two roots with same value, but we only return one
                    results.Add(0.5 * -b);
                }
            }

            return results.ToArray();
        }

        /////**
        // *  getCubicRoots
        // *
        // *  This code is based on MgcPolynomial.cpp written by David Eberly.  His
        // *  code along with many other excellent examples are available at his site:
        // *  http://www.geometrictools.com
        // */
        //public Complex[] GetCubicRoots()
        //{
        //    var results = new List<Complex>();

        //    if (GetDegree() == 3)
        //    {
        //        var c3 = Coefficients[3];
        //        var c2 = Coefficients[2] / c3;
        //        var c1 = Coefficients[1] / c3;
        //        var c0 = Coefficients[0] / c3;

        //        var a = (3 * c1 - c2 * c2) / 3;
        //        var b = (2 * c2 * c2 * c2 - 9 * c1 * c2 + 27 * c0) / 27;
        //        var offset = c2 / 3;
        //        var discrim = b * b / 4 + a * a * a / 27;
        //        var halfB = b / 2;

        //        var ZEROepsilon = ZeroErrorEstimate();
        //        if (Math.Abs(discrim.Real) <= ZEROepsilon) discrim = 0;

        //        if (discrim.Real > 0)
        //        {
        //            var e = Math.Sqrt(discrim.Real);

        //            double root;
        //            var tmp = -halfB + e;
        //            if (tmp.Real >= 0)
        //                root = Math.Pow(tmp.Real, 1d / 3d);
        //            else
        //                root = -Math.Pow(-tmp.Real, 1d / 3d);

        //            tmp = -halfB - e;
        //            if (tmp.Real >= 0)
        //                root += Math.Pow(tmp.Real, 1d / 3d);
        //            else
        //                root -= Math.Pow(-tmp.Real, 1d / 3d);

        //            results.Add(root - offset);
        //        }
        //        else if (discrim.Real < 0)
        //        {
        //            var distance = Math.Sqrt(-a.Real / 3d);
        //            var angle = Math.Atan2(Math.Sqrt(-discrim.Real), -halfB.Real) / 3;
        //            var cos = Math.Cos(angle);
        //            var sin = Math.Sin(angle);
        //            var sqrt3 = Math.Sqrt(3);

        //            results.Add(2 * distance * cos - offset);
        //            results.Add(-distance * (cos + sqrt3 * sin) - offset);
        //            results.Add(-distance * (cos - sqrt3 * sin) - offset);
        //        }
        //        else
        //        {
        //            double tmp;

        //            if (halfB.Real >= 0)
        //                tmp = -Math.Pow(halfB.Real, 1 / 3);
        //            else
        //                tmp = Math.Pow(-halfB.Real, 1 / 3);

        //            results.Add(2 * tmp - offset);
        //            // really should return next root twice, but we return only one
        //            results.Add(-tmp - offset);
        //        }
        //    }

        //    return results.ToArray();
        //}

        ///////////////////////////////////////////////////////////////////
        /////**
        //    Calculates roots of quartic polynomial. <br/>
        //    First, derivative roots are found, then used to split quartic polynomial 
        //    into segments, each containing one root of quartic polynomial.
        //    Segments are then passed to newton's method to find roots.

        //    @returns {Array<Number>} roots
        //*/
        //public Complex[] GetQuarticRoots()
        //{
        //    var results = new List<Complex>();

        //    var n = GetDegree();
        //    if (n == 4)
        //    {
        //        var poly = new PolynomialX
        //        {
        //            Coefficients = Coefficients.Slice()
        //        };
        //        poly.Divide(poly.Coefficients[n].Real);
        //        var ERRF = 1e-15;
        //        if (Math.Abs(poly.Coefficients[0].Real) < 10 * ERRF * Math.Abs(poly.Coefficients[3].Real))
        //            poly.Coefficients[0] = 0;
        //        var poly_d = poly.GetDerivative();
        //        var derrt = new List<Complex>(poly_d.GetRoots());
        //        derrt.Sort();
        //        var dery = new List<Complex>();
        //        var nr = derrt.Count - 1;
        //        int i;
        //        var rb = Bounds();
        //        var maxabsX = Math.Max(Math.Abs(rb.minX), Math.Abs(rb.maxX));
        //        var ZEROepsilon = ZeroErrorEstimate(maxabsX);

        //        for (i = 0; i <= nr; i++)
        //        {
        //            dery.Add(poly.Eval(derrt[i]));
        //        }

        //        for (i = 0; i <= nr; i++)
        //        {
        //            if (Math.Abs(dery[i].Real) < ZEROepsilon)
        //                dery[i] = 0;
        //        }

        //        i = 0;
        //        var dx = Math.Max(0.1 * (rb.maxX - rb.minX) / n, ERRF);
        //        var guesses = new List<Complex>();
        //        var minmax = new List<(double, double)>();
        //        if (nr > -1)
        //        {
        //            if (dery[0] != 0)
        //            {
        //                if (Math.Sign(dery[0].Real) != Math.Sign(poly.Eval(derrt[0] - dx).Real - dery[0].Real))
        //                {
        //                    guesses.Add(derrt[0] - dx);
        //                    minmax.Add((rb.minX, derrt[0].Real));
        //                }
        //            }
        //            else
        //            {
        //                results.Add(derrt[0]);
        //                i++;
        //            }

        //            for (; i < nr; i++)
        //            {
        //                if (dery[i + 1] == 0)
        //                {
        //                    results.Add(derrt[i + 1]);
        //                    i++;
        //                }
        //                else if (Math.Sign(dery[i].Real) != Math.Sign(dery[i + 1].Real))
        //                {
        //                    guesses.Add((derrt[i] + derrt[i + 1]) / 2);
        //                    minmax.Add((derrt[i].Real, derrt[i + 1].Real));
        //                }
        //            }
        //            if (dery[nr] != 0 && Math.Sign(dery[nr].Real) != Math.Sign(poly.Eval(derrt[nr].Real + dx).Real - dery[nr].Real))
        //            {
        //                guesses.Add(derrt[nr] + dx);
        //                minmax.Add((derrt[nr].Real, rb.maxX));
        //            }
        //        }

        //        if (guesses.Count > 0)
        //        {
        //            for (i = 0; i < guesses.Count; i++)
        //            {
        //                guesses[i] = PolynomialX.NewtonSecantBisection(guesses[i], (x, f) => poly.Eval(x), (x, df) => poly_d.Eval(x), 32, minmax[i][0], minmax[i][1]);
        //            }
        //        }

        //        results = results.Concat(guesses).ToList();
        //    }
        //    return results.ToArray();
        //}

        ///////////////////////////////////////////////////////////////////
        //        /**
        //            Estimate what is the maximum polynomial evaluation error value under which polynomial evaluation could be in fact 0.

        //            @returns {Number} 
        //*/
        //        public double ZeroErrorEstimate()
        //        {
        //            var poly = this;
        //            var ERRF = 1e-15;
        //            var rb = poly.Bounds();
        //            var maxabsX = Math.Max(Math.Abs(rb.minX), Math.Abs(rb.maxX));
        //            if (maxabsX < 0.001)
        //            {
        //                return 2 * Math.Abs(poly.Eval(ERRF).Real);
        //            }
        //            var n = poly.Coefficients.Length - 1;
        //            var an = poly.Coefficients[n];
        //            return 10d * ERRF * poly.Coefficients.Aggregate((Complex m, Complex v, int i) =>
        //            {
        //                double nm = v / an * Math.Pow(maxabsX, i);
        //                return nm > m ? nm : m;
        //            }, 0d);
        //        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="maxabsX"></param>
        ///// <returns></returns>
        //public double ZeroErrorEstimate(double maxabsX)
        //{
        //    var poly = this;
        //    var ERRF = 1e-15;
        //    if (maxabsX < 0.001)
        //    {
        //        return 2 * Math.Abs(poly.Eval(ERRF).Real);
        //    }
        //    var n = poly.Coefficients.Length - 1;
        //    var an = poly.Coefficients[n];
        //    return 10 * ERRF * poly.Coefficients.Aggregate((Complex m, Complex v, int i) =>
        //    {
        //        var nm = v / an * Math.Pow(maxabsX, i);
        //        return nm > m ? nm : m;
        //    }, 0);
        //}

        ///////////////////////////////////////////////////////////////////
        //        /**
        //            Calculates upper Real roots bounds. <br/>
        //            Real roots are in interval [negX, posX]. Determined by Fujiwara method.
        //            @see {@link http://en.wikipedia.org/wiki/Properties_of_polynomial_roots}

        //            @returns {{ negX: Number, posX: Number }}
        //*/
        //        public (double negX, double posX) BoundsUpperRealFujiwara()
        //        {
        //            var a = Coefficients;
        //            var n = a.Length - 1;
        //            var an = a[n];
        //            if (an != 1)
        //            {
        //                a = Coefficients.Select((v) => { return v / an; }).ToArray();
        //            }
        //            var b = a.Select((v, i) => { return (i < n) ? Math.Pow(Math.Abs((i == 0) ? v.Real / 2 : v.Real), 1 / (n - i)) : v; });

        //            //var find2Max = (acc, bi, i) =>
        //            // {
        //            //     if (coefSelectionFunc(i))
        //            //     {
        //            //         if (acc.max < bi)
        //            //         {
        //            //             acc.nearmax = acc.max;
        //            //             acc.max = bi;
        //            //         }
        //            //         else if (acc.nearmax < bi)
        //            //         {
        //            //             acc.nearmax = bi;
        //            //         }
        //            //     }
        //            //     return acc;
        //            // };

        //            (Complex max, Complex nearmax) zed = (max: 0, nearmax: 0);

        //            //coefSelectionFunc(i)=> { return i < n && a[i] < 0; };
        //            List<Complex> max_nearmax_pos = b.Aggregate(((Complex max, Complex nearmax) acc, Complex bi, int i) => {
        //                if ((i) => { return i < n && a[i] < 0; })
        //                {
        //                    if (acc.max < bi)
        //                    {
        //                        acc.nearmax = acc.max;
        //                        acc.max = bi;
        //                    }
        //                    else if (acc.nearmax < bi)
        //                    {
        //                        acc.nearmax = bi;
        //                    }
        //                }
        //                return acc;
        //            }, zed).ToList();

        //            //coefSelectionFunc(i)=> { return i < n && ((n % 2 == i % 2) ? a[i] < 0 : a[i] > 0); };
        //            var max_nearmax_neg = b.Aggregate((Complex acc, Complex bi, int i) =>
        //            {
        //                if ((i) => { return i < n && ((n % 2 == i % 2) ? a[i] < 0 : a[i] > 0); })
        //                {
        //                    if (acc.max < bi)
        //                    {
        //                        acc.nearmax = acc.max;
        //                        acc.max = bi;
        //                    }
        //                    else if (acc.nearmax < bi)
        //                    {
        //                        acc.nearmax = bi;
        //                    }
        //                }
        //                return acc;
        //            }, zed);

        //            return (negX: -2 * max_nearmax_neg.max, posX: 2 * max_nearmax_pos.max);
        //        }

        ///////////////////////////////////////////////////////////////////
        /////** 
        //    Calculates lower Real roots bounds. <br/>
        //    There are no Real roots in interval <negX, posX>. Determined by Fujiwara method.
        //    @see {@link http://en.wikipedia.org/wiki/Properties_of_polynomial_roots}

        //    @returns {{ negX: Number, posX: Number }}
        //*/
        //public (double negX, double posX) BoundsLowerRealFujiwara()
        //{
        //    var poly = new PolynomialX();
        //    poly.Coefficients = Coefficients.Slice();
        //    Array.Reverse(poly.Coefficients);
        //    var res = poly.BoundsUpperRealFujiwara();
        //    res.negX = 1 / res.negX;
        //    res.posX = 1 / res.posX;
        //    return res;
        //}

        ///////////////////////////////////////////////////////////////////
        //        /** 
        //            Calculates left and right Real roots bounds. <br/>
        //            Real roots are in interval [minX, maxX]. Combines Fujiwara lower and upper bounds to get minimal interval.
        //            @see {@link http://en.wikipedia.org/wiki/Properties_of_polynomial_roots}

        //            @returns {{ minX: Number, maxX: Number }}
        //*/
        //        public (double minX, double maxX) Bounds()
        //        {
        //            var urb = BoundsUpperRealFujiwara();
        //            var rb = (minX: urb.negX, maxX: urb.posX);
        //            if (urb.negX == 0 && urb.posX == 0)
        //                return rb;
        //            if (urb.negX == 0)
        //            {
        //                rb.minX = BoundsLowerRealFujiwara().posX;
        //            }
        //            else if (urb.posX == 0)
        //            {
        //                rb.maxX = BoundsLowerRealFujiwara().negX;
        //            }
        //            if (rb.minX > rb.maxX)
        //            {
        //                //console.log('public void bounds: poly has no real roots? or floating point error?');
        //                rb.minX = rb.maxX = 0;
        //            }
        //            return rb;
        //            // ToDo: if sure that there are no complex roots 
        //            // (maybe by using Sturm's theorem) use:
        //            //return this.bounds_Real_Laguerre();
        //        }

        //        /////////////////////////////////////////////////////////////////// 
        //        /**
        //            Newton's (Newton-Raphson) method for finding Real roots on univariate function. <br/>
        //            When using bounds, algorithm falls back to secant if newton goes out of range.
        //            Bisection is fallback for secant when determined secant is not efficient enough.
        //            @see {@link http://en.wikipedia.org/wiki/Newton%27s_method}
        //            @see {@link http://en.wikipedia.org/wiki/Secant_method}
        //            @see {@link http://en.wikipedia.org/wiki/Bisection_method}

        //            @param {Number} x0 - Inital root guess
        //            @param {function(x)} f - Function which root we are trying to find
        //            @param {function(x)} df - Derivative of function f
        //            @param {Number} max_iterations - Maximum number of algorithm iterations
        //            @param {Number} [min_x] - Left bound value
        //            @param {Number} [max_x] - Right bound value
        //            @returns {Number} - root
        //*/
        //        public double NewtonSecantBisection(double x0, Func<double, double> f, Func<double, double> df, int max_iterations, double min, double max)
        //        {
        //            double prev_dfx = 0, dfx, prev_x_ef_correction = 0, x_correction, x_new;
        //            double v, y_atmin = 0, y_atmax = 0;
        //            var x = x0;
        //            var ACCURACY = 14;
        //            var min_correction_factor = Math.Pow(10, -ACCURACY);
        //            var isBounded = (min is double && max is double);
        //            if (isBounded)
        //            {
        //                if (min > max)
        //                    throw new Exception("newton root finding: min must be greater than max");
        //                y_atmin = f(min);
        //                y_atmax = f(max);
        //                if (Math.Sign(y_atmin) == Math.Sign(y_atmax))
        //                    throw new Exception("newton root finding: y values of bounds must be of opposite sign");
        //            }

        //            int i;
        //            //var stepMethod;
        //            //var details = [];
        //            for (i = 0; i < max_iterations; i++)
        //            {
        //                dfx = df(x);
        //                if (dfx == 0)
        //                {
        //                    if (prev_dfx == 0)
        //                    {
        //                        // error
        //                        throw new Exception("newton root finding: df(x) is zero");
        //                        //return null;
        //                    }
        //                    else
        //                    {
        //                        // use previous derivation value
        //                        dfx = prev_dfx;
        //                    }
        //                    // or move x a little?
        //                    //dfx = df(x != 0 ? x + x * 1e-15 : 1e-15);
        //                }
        //                //stepMethod = 'newton';
        //                prev_dfx = dfx;
        //                var y = f(x);
        //                x_correction = y / dfx;
        //                x_new = x - x_correction;
        //                if ((Math.Abs(x_correction) <= min_correction_factor * Math.Abs(x))
        //                        || (prev_x_ef_correction == (x - x_correction) - x))
        //                {
        //                    break;
        //                }

        //                if (isBounded)
        //                {
        //                    if (Math.Sign(y) == Math.Sign(y_atmax))
        //                    {
        //                        max = x;
        //                        y_atmax = y;
        //                    }
        //                    else if (Math.Sign(y) == Math.Sign(y_atmin))
        //                    {
        //                        min = x;
        //                        y_atmin = y;
        //                    }
        //                    else
        //                    {
        //                        x = x_new;
        //                        //console.log("newton root finding: sign(y) not matched.");
        //                        break;
        //                    }

        //                    if ((x_new < min) || (x_new > max))
        //                    {
        //                        if (Math.Sign(y_atmin) == Math.Sign(y_atmax))
        //                        {
        //                            break;
        //                        }

        //                        var RATIO_LIMIT = 50;
        //                        var AIMED_BISECT_OFFSET = 0.25; // [0, 0.5)
        //                        var dy = y_atmax - y_atmin;
        //                        var dx = max - min;

        //                        if (dy == 0)
        //                        {
        //                            //stepMethod = 'bisect';
        //                            x_correction = x - (min + dx * 0.5);
        //                        }
        //                        else if (Math.Abs(dy / Math.Min(y_atmin, y_atmax)) > RATIO_LIMIT)
        //                        {
        //                            //stepMethod = 'aimed bisect';
        //                            x_correction = x - (min + dx * (0.5 + (Math.Abs(y_atmin) < Math.Abs(y_atmax) ? -AIMED_BISECT_OFFSET : AIMED_BISECT_OFFSET)));
        //                        }
        //                        else
        //                        {
        //                            //stepMethod = 'secant'; 
        //                            x_correction = x - (min - y_atmin / dy * dx);
        //                        }
        //                        x_new = x - x_correction;

        //                        if ((Math.Abs(x_correction) <= min_correction_factor * Math.Abs(x))
        //                        || (prev_x_ef_correction == (x - x_correction) - x))
        //                        {
        //                            break;
        //                        }
        //                    }
        //                }
        //                //details.Add([stepMethod, i, x, x_new, x_correction, min, max, y]);
        //                prev_x_ef_correction = x - x_new;
        //                x = x_new;
        //            }
        //            //details.Add([stepMethod, i, x, x_new, x_correction, min, max, y]);
        //            //console.log(details.join('\r\n'));
        //            //if (i == max_iterations)
        //            //    console.log('newt: steps=' + ((i==max_iterations)? i:(i + 1)));
        //            return x;
        //        }

        #region Overrides
        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool Equals(object obj)
            => ToString() == ((Polynomial)obj).ToString(CultureInfo.InvariantCulture);

        /// <summary>
        /// Get the hash code.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        [SecuritySafeCritical]
        public override int GetHashCode()
            => Coefficients.GetHashCode();

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
            => ConvertToString(string.Empty, CultureInfo.In­variantCulture);

        /// <summary>
        /// The to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string ToString(string format)
            => ConvertToString(format, CultureInfo.In­variantCulture);

        /// <summary>
        /// The to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The formatProvider.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string ToString(string format, IFormatProvider formatProvider)
            => ConvertToString(format, formatProvider);

        /// <summary>
        /// Convert the to string.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="formatProvider">The formatProvider.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string ConvertToString(string format, IFormatProvider formatProvider)
        {
            if (IsZero)
            {
                return "0";
            }
            else
            {
                var s = "";

                for (var i = 0; i < Degree + 1; i++)
                {
                    if (Coefficients[i] != Complex.Zero)
                    {
                        if (Coefficients[i] == Complex.I)
                        {
                            s += "i";
                        }
                        else if (Coefficients[i] != Complex.One)
                        {
                            if (Coefficients[i].IsReal && Coefficients[i].Real > 0)
                            {
                                s += Coefficients[i].ToString(format, formatProvider);
                            }
                            else
                            {
                                s += "(" + Coefficients[i].ToString(format, formatProvider) + ")";
                            }
                        }
                        else if (/*Coefficients[i] == Complex.One && */i == 0)
                        {
                            s += 1;
                        }

                        if (i == 1)
                        {
                            s += "x";
                        }
                        else if (i > 1)
                        {
                            s += "x^" + i.ToString(format, formatProvider);
                        }
                    }

                    if (i < Degree && Coefficients[i + 1] != 0 && s.Length > 0)
                    {
                        s += " + ";
                    }
                }

                return s;
            }
        }
        #endregion Overrides
    }
}
