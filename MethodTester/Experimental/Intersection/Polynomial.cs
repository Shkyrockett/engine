﻿// <copyright file="Polynomial.cs" >
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
using System.Linq;
using System.Security;

namespace Engine.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    public class Polynomial
        : IFormattable
    {
        private const double TOLERANCE = 1e-6;

        private const double ACCURACY = 15;

        #region Fields

        /// <summary>
        /// Coefficients a_0,...,a_n of a polynomial p, such that
        /// p(x) = a_0 + a_1*x + a_2*x^2 + ... + a_n*x^n.
        /// </summary>
        public Complex[] Coefficients;

        #endregion

        #region Constructors

        /// <summary>
        /// Inits zero polynomial p = 0.
        /// </summary>
        public Polynomial()
        {
            Coefficients = new Complex[1];
            Coefficients[0] = Complex.Zero;
        }

        /// <summary>
        /// Inits polynomial from given complex coefficient array.
        /// </summary>
        /// <param name="coeffs"></param>
        public Polynomial(params Complex[] coeffs)
        {
            if (coeffs == null || coeffs.Length < 1)
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
        public Polynomial(params double[] coeffs)
        {
            if (coeffs == null || coeffs.Length < 1)
            {
                Coefficients = new Complex[1];
                Coefficients[0] = Complex.Zero;
            }
            else
            {
                Coefficients = new Complex[coeffs.Length];
                for (int i = 0; i < coeffs.Length; i++)
                    Coefficients[i] = new Complex(coeffs[i]);
            }
        }

        /// <summary>
        /// Inits constant polynomial.
        /// </summary>
        /// <param name="coeffs"></param>
        public Polynomial(Complex coeffs)
        {
            Coefficients = new Complex[1];

            if (coeffs == null)
                Coefficients[0] = Complex.Zero;
            else
                Coefficients[0] = coeffs;
        }

        /// <summary>
        /// Inits constant polynomial.
        /// </summary>
        /// <param name="coeffs"></param>
        public Polynomial(double coeffs)
        {
            Coefficients = new Complex[1];

            Coefficients[0] = new Complex(coeffs);
        }

        #endregion

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
                for (int i = 0; i < Coefficients.Length; i++)
                    if (Coefficients[i] != 0) return false;

                return true;
            }
        }

        #endregion

        #region Operators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public static Polynomial operator +(Polynomial p, Polynomial q)
        {

            int degree = Math.Max(p.Degree, q.Degree);

            Complex[] coeffs = new Complex[degree + 1];

            for (int i = 0; i <= degree; i++)
            {
                if (i > p.Degree) coeffs[i] = q.Coefficients[i];
                else if (i > q.Degree) coeffs[i] = p.Coefficients[i];
                else coeffs[i] = p.Coefficients[i] + q.Coefficients[i];
            }

            return new Polynomial(coeffs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public static Polynomial operator -(Polynomial p, Polynomial q)
            => p + (-q);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Polynomial operator -(Polynomial p)
        {
            Complex[] coeffs = new Complex[p.Degree + 1];

            for (int i = 0; i < coeffs.Length; i++)
                coeffs[i] = -p.Coefficients[i];

            return new Polynomial(coeffs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Polynomial operator *(Complex d, Polynomial p)
        {
            Complex[] coeffs = new Complex[p.Degree + 1];

            for (int i = 0; i < coeffs.Length; i++)
                coeffs[i] = d * p.Coefficients[i];

            return new Polynomial(coeffs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Polynomial operator *(Polynomial p, Complex d)
        {
            Complex[] coeffs = new Complex[p.Degree + 1];

            for (int i = 0; i < coeffs.Length; i++)
                coeffs[i] = d * p.Coefficients[i];

            return new Polynomial(coeffs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Polynomial operator *(double d, Polynomial p)
        {
            Complex[] coeffs = new Complex[p.Degree + 1];

            for (int i = 0; i < coeffs.Length; i++)
                coeffs[i] = d * p.Coefficients[i];

            return new Polynomial(coeffs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Polynomial operator *(Polynomial p, double d)
        {
            Complex[] coeffs = new Complex[p.Degree + 1];

            for (int i = 0; i < coeffs.Length; i++)
                coeffs[i] = d * p.Coefficients[i];

            return new Polynomial(coeffs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Polynomial operator /(Polynomial p, Complex d)
        {
            Complex[] coeffs = new Complex[p.Degree + 1];

            for (int i = 0; i < coeffs.Length; i++)
                coeffs[i] = p.Coefficients[i] / d;

            return new Polynomial(coeffs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Polynomial operator /(Polynomial p, double d)
        {
            Complex[] coeffs = new Complex[p.Degree + 1];

            for (int i = 0; i < coeffs.Length; i++)
                coeffs[i] = p.Coefficients[i] / d;

            return new Polynomial(coeffs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public static Polynomial operator *(Polynomial p, Polynomial q)
        {
            int degree = p.Degree + q.Degree;

            Polynomial r = new Polynomial();


            for (int i = 0; i <= p.Degree; i++)
                for (int j = 0; j <= q.Degree; j++)
                    r += (p.Coefficients[i] * q.Coefficients[j]) * Monomial(i + j);

            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static Polynomial operator ^(Polynomial p, uint k)
        {
            if (k == 0)
                return Monomial(0);
            else if (k == 1)
                return p;
            else
                return p * (p ^ (k - 1));
        }

        #endregion

        #region dynamics

        /// <summary>
        /// Computes value of the differentiated polynomial at x.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public Complex Differentiate(Complex x)
        {
            Complex[] buf = new Complex[Degree];

            for (int i = 0; i < buf.Length; i++)
                buf[i] = (i + 1) * Coefficients[i + 1];

            return (new Polynomial(buf)).Evaluate(x);
        }

        /// <summary>
        /// Computes the definite integral within the borders a and b.
        /// </summary>
        /// <param name="a">Left integration border.</param>
        /// <param name="b">Right integration border.</param>
        /// <returns></returns>
        public Complex Integrate(Complex a, Complex b)
        {
            Complex[] buf = new Complex[Degree + 2];
            buf[0] = Complex.Zero; // this value can be arbitrary, in fact

            for (int i = 1; i < buf.Length; i++)
                buf[i] = Coefficients[i - 1] / i;

            Polynomial p = new Polynomial(buf);

            return (p.Evaluate(b) - p.Evaluate(a));
        }

        /// <summary>
        /// Evaluates polynomial by using the horner scheme.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public Complex Evaluate(Complex x)
        {
            Complex buf = Coefficients[Degree];

            for (int i = Degree - 1; i >= 0; i--)
            {
                buf = Coefficients[i] + x * buf;
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
                for (int k = 0; k <= Degree; k++)
                    Coefficients[k] /= Coefficients[Degree];
        }

        /// <summary>
        /// Removes unnecessary zero terms.
        /// </summary>
        public void Clean()
        {
            int i;

            for (i = Degree; i >= 0 && Coefficients[i] == 0; i--) ;

            Complex[] coeffs = new Complex[i + 1];

            for (int k = 0; k <= i; k++)
                coeffs[k] = Coefficients[k];

            Coefficients = (Complex[])coeffs.Clone();
        }

        /// <summary>
        /// Factorizes polynomial to its linear factors.
        /// </summary>
        /// <returns></returns>
        public FactorizedPolynomial Factorize()
        {
            // this is to be returned
            FactorizedPolynomial p = new FactorizedPolynomial();

            // cannot factorize polynomial of degree 0 or 1
            if (Degree <= 1)
            {
                p.Factor = new Polynomial[] { this };
                p.Power = new int[] { 1 };

                return p;
            }

            Complex[] roots = Roots(this);

            //ArrayList rootlist = new ArrayList();
            //foreach (Complex z in roots) rootlist.Add(z);

            //roots = null; // don't need you anymore

            //rootlist.Sort();

            //// number of different roots
            //int num = 1; // ... at least one

            //// ...or more?
            //for (int i = 1; i < rootlist.Count; i++)
            //    if (rootlist[i] != rootlist[i - 1]) num++;

            Polynomial[] factor = new Polynomial[roots.Length];
            int[] power = new int[roots.Length];

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
            factor[0] = new Polynomial(new Complex[] { -Coefficients[Degree] * (Complex)roots[0], Coefficients[Degree] });

            for (int i = 1; i < roots.Length; i++)
            {
                power[i] = 1;
                factor[i] = new Polynomial(new Complex[] { -(Complex)roots[i], Complex.One });
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
            double tolerance = 1e-12;
            int max_iterations = 30;

            Polynomial q = Normalize(this);
            //Polynomial q = p;

            Complex[] z = new Complex[q.Degree]; // approx. for roots
            Complex[] w = new Complex[q.Degree]; // Weierstraß corrections

            // init z
            for (int k = 0; k < q.Degree; k++)
                //z[k] = (new Complex(.4, .9)) ^ k;
                z[k] = Complex.Exp(2 * Math.PI * Complex.I * k / q.Degree);


            for (int iter = 0; iter < max_iterations
                && MaxValue(q, z) > tolerance; iter++)
                for (int i = 0; i < 10; i++)
                {
                    for (int k = 0; k < q.Degree; k++)
                        w[k] = q.Evaluate(z[k]) / WeierNull(z, k);

                    for (int k = 0; k < q.Degree; k++)
                        z[k] -= w[k];
                }

            // clean...
            for (int k = 0; k < q.Degree; k++)
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
        /// the computation effort if desired pecision is hard to achieve.</param>
        /// <returns></returns>
        public Complex[] Roots(double tolerance, int max_iterations)
        {
            Polynomial q = Normalize(this);

            Complex[] z = new Complex[q.Degree]; // approx. for roots
            Complex[] w = new Complex[q.Degree]; // Weierstraß corrections

            // init z
            for (int k = 0; k < q.Degree; k++)
                //z[k] = (new Complex(.4, .9)) ^ k;
                z[k] = Complex.Exp(2 * Math.PI * Complex.I * k / q.Degree);


            for (int iter = 0; iter < max_iterations
                && MaxValue(q, z) > tolerance; iter++)
                for (int i = 0; i < 10; i++)
                {
                    for (int k = 0; k < q.Degree; k++)
                        w[k] = q.Evaluate(z[k]) / WeierNull(z, k);

                    for (int k = 0; k < q.Degree; k++)
                        z[k] -= w[k];
                }

            // clean...
            for (int k = 0; k < q.Degree; k++)
            {
                z[k].Real = Math.Round(z[k].Real, 12);
                z[k].Imaginary = Math.Round(z[k].Imaginary, 12);
            }

            return z;
        }

        #endregion

        #region statics

        /// <summary>
        /// Expands factorized polynomial p_1(x)^(k_1)*...*p_r(x)^(k_r) to its normal form a_0 + a_1 x + ... + a_n x^n.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Polynomial Expand(FactorizedPolynomial p)
        {
            Polynomial q = new Polynomial(new Complex[] { Complex.One });

            for (int i = 0; i < p.Factor.Length; i++)
            {
                for (int j = 0; j < p.Power[i]; j++)
                    q *= p.Factor[i];

                q.Clean();
            }

            // clean...
            for (int k = 0; k <= q.Degree; k++)
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
        /// <returns></returns>
        public static Complex Evaluate(FactorizedPolynomial p, Complex x)
        {
            Complex z = Complex.One;

            for (int i = 0; i < p.Factor.Length; i++)
            {
                z *= Complex.Pow(p.Factor[i].Evaluate(x), p.Power[i]);
            }

            return z;
        }

        /// <summary>
        /// Removes unncessary leading zeros.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Polynomial Clean(Polynomial p)
        {
            int i;

            for (i = p.Degree; i >= 0 && p.Coefficients[i] == 0; i--) ;

            Complex[] coeffs = new Complex[i + 1];

            for (int k = 0; k <= i; k++)
                coeffs[k] = p.Coefficients[k];

            return new Polynomial(coeffs);
        }

        /// <summary>
        /// Normalizes the polynomial, e.i. divides each coefficient by the
        /// coefficient of a_n the greatest term if a_n != 1.
        /// </summary>
        public static Polynomial Normalize(Polynomial p)
        {
            Polynomial q = Clean(p);

            if (q.Coefficients[q.Degree] != Complex.One)
                for (int k = 0; k <= q.Degree; k++)
                    q.Coefficients[k] /= q.Coefficients[q.Degree];

            return q;
        }

        /// <summary>
        /// Computes the roots of polynomial p via Weierstrass iteration.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Complex[] Roots(Polynomial p)
        {
            double tolerance = 1e-12;
            int max_iterations = 30;

            Polynomial q = Normalize(p);
            //Polynomial q = p;

            Complex[] z = new Complex[q.Degree]; // approx. for roots
            Complex[] w = new Complex[q.Degree]; // Weierstraß corrections

            // init z
            for (int k = 0; k < q.Degree; k++)
                //z[k] = (new Complex(.4, .9)) ^ k;
                z[k] = Complex.Exp(2 * Math.PI * Complex.I * k / q.Degree);


            for (int iter = 0; iter < max_iterations
                && MaxValue(q, z) > tolerance; iter++)
                for (int i = 0; i < 10; i++)
                {
                    for (int k = 0; k < q.Degree; k++)
                        w[k] = q.Evaluate(z[k]) / WeierNull(z, k);

                    for (int k = 0; k < q.Degree; k++)
                        z[k] -= w[k];
                }

            // clean...
            for (int k = 0; k < q.Degree; k++)
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
        /// the computation effort if desired pecision is hard to achieve.</param>
        /// <returns></returns>
        public static Complex[] Roots(Polynomial p, double tolerance, int max_iterations)
        {
            Polynomial q = Normalize(p);

            Complex[] z = new Complex[q.Degree]; // approx. for roots
            Complex[] w = new Complex[q.Degree]; // Weierstraß corrections

            // init z
            for (int k = 0; k < q.Degree; k++)
                //z[k] = (new Complex(.4, .9)) ^ k;
                z[k] = Complex.Exp(2 * Math.PI * Complex.I * k / q.Degree);


            for (int iter = 0; iter < max_iterations
                && MaxValue(q, z) > tolerance; iter++)
                for (int i = 0; i < 10; i++)
                {
                    for (int k = 0; k < q.Degree; k++)
                        w[k] = q.Evaluate(z[k]) / WeierNull(z, k);

                    for (int k = 0; k < q.Degree; k++)
                        z[k] -= w[k];
                }

            // clean...
            for (int k = 0; k < q.Degree; k++)
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
        public static double MaxValue(Polynomial p, Complex[] z)
        {
            double buf = 0;


            for (int i = 0; i < z.Length; i++)
            {
                if (Complex.Abs(p.Evaluate(z[i])) > buf)
                    buf = Complex.Abs(p.Evaluate(z[i]));
            }

            return buf;
        }

        /// <summary>
        /// For g(x) = (x-z_0)*...*(x-z_n), this method returns
        /// g'(z_k) = \prod_{j != k} (z_k - z_j).
        /// </summary>
        /// <param name="z"></param>
        /// <returns></returns>
        private static Complex WeierNull(Complex[] z, int k)
        {
            if (k < 0 || k >= z.Length)
                throw new ArgumentOutOfRangeException();

            Complex buf = Complex.One;

            for (int j = 0; j < z.Length; j++)
                if (j != k) buf *= (z[k] - z[j]);

            return buf;
        }

        /// <summary>
        /// Differentiates given polynomial p.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Polynomial Derivative(Polynomial p)
        {
            Complex[] buf = new Complex[p.Degree];

            for (int i = 0; i < buf.Length; i++)
                buf[i] = (i + 1) * p.Coefficients[i + 1];

            return new Polynomial(buf);
        }

        /// <summary>
        /// Integrates given polynomial p.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Polynomial Integral(Polynomial p)
        {
            Complex[] buf = new Complex[p.Degree + 2];
            buf[0] = Complex.Zero; // this value can be arbitrary, in fact

            for (int i = 1; i < buf.Length; i++)
                buf[i] = p.Coefficients[i - 1] / i;

            return new Polynomial(buf);
        }

        /// <summary>
        /// Computes the monomial x^degree.
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public static Polynomial Monomial(int degree)
        {
            if (degree == 0) return new Polynomial(1);

            Complex[] coeffs = new Complex[degree + 1];

            for (int i = 0; i < degree; i++)
                coeffs[i] = Complex.Zero;

            coeffs[degree] = Complex.One;

            return new Polynomial(coeffs);
        }

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

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xs"></param>
        /// <param name="ys"></param>
        /// <param name="n"></param>
        /// <param name="offset"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/thelonious/kld-polynomial
        /// </remarks>
        public static (Complex y, Complex dy) Interpolate(Complex[] xs, Complex[] ys, int n, int offset, double x)
        {
            if (!(xs is Array) || !(ys is Array))
                throw new Exception("Polynomial.interpolate: xs and ys must be arrays");
            if (double.IsNaN(n) || double.IsNaN(offset) || double.IsNaN(x))
                throw new Exception("Polynomial.interpolate: n, offset, and x must be numbers");

            Complex y = 0d;
            Complex dy = 0d;
            var c = new Complex[n];
            var d = new Complex[n];
            int ns = 0;
            (double y, double dy) result;

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
            y = ys[offset + ns].Real;
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
                dy = (2 * (ns + 1) < (n - m)) ? c[ns + 1] : d[ns--];
                y += dy;
            }

            return (y: y, dy: dy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/thelonious/kld-polynomial
        /// </remarks>
        public Complex Eval(Complex x)
        {
            if (double.IsNaN(x.Real))
                throw new Exception("Polynomial.eval: parameter must be a number");

            Complex result = Complex.Zero;

            for (var i = Coefficients.Length - 1; i >= 0; i--)
                result = result * x + Coefficients[i];

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="that"></param>
        /// <returns></returns>
        public Polynomial Add(Polynomial that)
        {
            var result = new Polynomial();
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
        /// 
        /// </summary>
        /// <param name="that"></param>
        /// <returns></returns>
        public Polynomial Multiply(Polynomial that)
        {
            var result = new Polynomial();
            var lst = new List<Complex>(that.Coefficients);

            for (var i = 0; i <= GetDegree() + that.GetDegree(); i++)
                lst.Add(0);

            result.Coefficients = lst.ToArray();

            for (var i = 0; i <= GetDegree(); i++)
                for (var j = 0; j <= that.GetDegree(); j++)
                    result.Coefficients[i + j] += Coefficients[i] * that.Coefficients[j];

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public Polynomial Divide(double scalar)
        {
            for (var i = 0; i < Coefficients.Length; i++)
                Coefficients[i] /= scalar;
            return this;
        }

        /// <summary>
        /// 
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
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public Complex Bisection(Complex min, Complex max)
        {
            var minValue = Eval(min);
            var maxValue = Eval(max);
            Complex result = 0;

            if (Math.Abs(minValue.Real) <= TOLERANCE)
                result = min;
            else if (Math.Abs(maxValue.Real) <= TOLERANCE)
                result = max;
            else if (minValue.Real * maxValue.Real <= 0)
            {
                var tmp1 = Math.Log(max.Real - min.Real);
                var tmp2 = Maths.LN10 * ACCURACY;
                var iters = Math.Ceiling((tmp1 + tmp2) / Maths.LN2);

                for (var i = 0; i < iters; i++)
                {
                    result = 0.5 * (min + max);
                    var value = Eval(result);

                    if (Math.Abs(value.Real) <= TOLERANCE)
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
        /// 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public Complex Trapezoid(Complex min, Complex max, int n)
        {
            if (double.IsNaN(min.Real) || double.IsNaN(max.Real) || double.IsNaN(n))
                throw new Exception("Polynomial.trapezoid: parameters must be numbers");

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
                var x = min + 0.5 * delta;
                Complex sum = 0;

                for (var i = 0; i < it; i++)
                {
                    sum += Eval(x);
                    x += delta;
                }
                _s = 0.5 * (_s + range * sum / it);
            }

            if (double.IsNaN(_s.Real))
                throw new Exception("Polynomial.trapezoid: this._s is NaN");

            return _s;
        }

        /**
         *  simpson
         *  Based on trapzd in "Numerical Recipes in C", page 139
         */
        public Complex Simpson(Complex min, Complex max)
        {
            if (double.IsNaN(min.Real) || double.IsNaN(max.Real))
                throw new Exception("Polynomial.simpson: parameters must be numbers");

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
                var x = min + 0.5 * delta;
                Complex sum = 0;

                for (var i = 1; i <= it; i++)
                {
                    sum += Eval(x);
                    x += delta;
                }

                t = 0.5 * (t + range * sum / it);
                st = t;
                s = (4.0 * st - ost) / 3.0;

                if (Math.Abs(s.Real - os.Real) < TOLERANCE * Math.Abs(os.Real))
                    break;

                os = s;
                ost = st;
                it <<= 1;
            }

            return s;
        }


        /**
         *  romberg
         */
        public Complex Romberg(Complex min, Complex max)
        {
            if (double.IsNaN(min.Real) || double.IsNaN(max.Real))
                throw new Exception("Polynomial.romberg: parameters must be numbers");

            var MAX = 20;
            var K = 3;
            var TOLERANCE = 1e-6;
            var s = new Complex[MAX + 1];
            var h = new Complex[MAX + 1];
            (Complex y, Complex dy) result = (y: 0, dy: 0);

            h[0] = 1.0;
            for (int j = 1; j <= MAX; j++)
            {
                s[j - 1] = Trapezoid(min, max, j);
                if (j >= K)
                {
                    result = Interpolate(h, s, K, j - K, 0.0);
                    if (Math.Abs(result.dy.Real) <= TOLERANCE * result.y.Real) break;
                }
                s[j] = s[j - 1];
                h[j] = 0.25 * h[j - 1];
            }

            return result.y;
        }

        // getters and setters

        /**
         *  get degree
         */
        public int GetDegree()
            => Coefficients.Length - 1;

        /**
         *  getDerivative
         */
        public Polynomial GetDerivative()
        {
            var lst = new List<Complex>();

            for (var i = 1; i < Coefficients.Length; i++)
            {
                lst.Add(i * Coefficients[i]);
            }
            var derivative = new Polynomial(lst.ToArray());

            return derivative;
        }

        /**
         *  getRoots
         */
        public Complex[] GetRoots()
        {
            Complex[] result;

            Simplify();
            switch (GetDegree())
            {
                case 0:
                    result = new Complex[0];
                    break;
                case 1:
                    result = GetLinearRoot();
                    break;
                case 2:
                    result = GetQuadraticRoots();
                    break;
                case 3:
                    result = GetCubicRoots();
                    break;
                case 4:
                    result = GetQuarticRoots();
                    break;
                default:
                    result = new Complex[0];
                    break;
                    // should try Newton's method and/or bisection
            }

            return result;
        }

        /**
         *  getRootsInInterval
         */
        public Complex[] GetRootsInInterval(Complex min, Complex max)
        {
            var roots = new List<Complex>();
            Complex root;

            if (GetDegree() == 1)
            {
                root = Bisection(min, max);
                if (root != null) roots.Add(root);
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
                    if (root != null) roots.Add(root);

                    // find root on [droots[i],droots[i+1]] for 0 <= i <= count-2
                    for (int i = 0; i <= droots.Length - 2; i++)
                    {
                        root = Bisection(droots[i].Real, droots[i + 1].Real);
                        if (root != null) roots.Add(root);
                    }

                    // find root on [droots[count-1],xmax]
                    root = Bisection(droots[droots.Length - 1].Real, max);
                    if (root != null) roots.Add(root);
                }
                else
                {
                    // polynomial is monotone on [min,max], has at most one root
                    root = Bisection(min, max);
                    if (root != null) roots.Add(root);
                }
            }

            return roots.ToArray();
        }

        /**
         *  getLinearRoot
         */
        public Complex[] GetLinearRoot()
        {
            var result = new List<Complex>();
            var a = Coefficients[1];

            if (a != 0)
                result.Add(-Coefficients[0] / a);

            return result.ToArray();
        }

        /**
         *  getQuadraticRoots
         */
        public Complex[] GetQuadraticRoots()
        {
            var results = new List<Complex>();

            if (GetDegree() == 2)
            {
                var a = Coefficients[2];
                var b = Coefficients[1] / a;
                var c = Coefficients[0] / a;
                var d = b * b - 4d * c;

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

        /**
         *  getCubicRoots
         *
         *  This code is based on MgcPolynomial.cpp written by David Eberly.  His
         *  code along with many other excellent examples are avaiable at his site:
         *  http://www.geometrictools.com
         */
        public Complex[] GetCubicRoots()
        {
            var results = new List<Complex>();

            if (GetDegree() == 3)
            {
                var c3 = Coefficients[3];
                var c2 = Coefficients[2] / c3;
                var c1 = Coefficients[1] / c3;
                var c0 = Coefficients[0] / c3;

                var a = (3 * c1 - c2 * c2) / 3;
                var b = (2 * c2 * c2 * c2 - 9 * c1 * c2 + 27 * c0) / 27;
                var offset = c2 / 3;
                var discrim = b * b / 4 + a * a * a / 27;
                var halfB = b / 2;

                var ZEROepsilon = zeroErrorEstimate();
                if (Math.Abs(discrim.Real) <= ZEROepsilon) discrim = 0;

                if (discrim.Real > 0)
                {
                    var e = Math.Sqrt(discrim.Real);

                    double root;
                    var tmp = -halfB + e;
                    if (tmp.Real >= 0)
                        root = Math.Pow(tmp.Real, 1d / 3d);
                    else
                        root = -Math.Pow(-tmp.Real, 1d / 3d);

                    tmp = -halfB - e;
                    if (tmp.Real >= 0)
                        root += Math.Pow(tmp.Real, 1d / 3d);
                    else
                        root -= Math.Pow(-tmp.Real, 1d / 3d);

                    results.Add(root - offset);
                }
                else if (discrim.Real < 0)
                {
                    var distance = Math.Sqrt(-a.Real / 3d);
                    var angle = Math.Atan2(Math.Sqrt(-discrim.Real), -halfB.Real) / 3;
                    var cos = Math.Cos(angle);
                    var sin = Math.Sin(angle);
                    var sqrt3 = Math.Sqrt(3);

                    results.Add(2 * distance * cos - offset);
                    results.Add(-distance * (cos + sqrt3 * sin) - offset);
                    results.Add(-distance * (cos - sqrt3 * sin) - offset);
                }
                else
                {
                    double tmp;

                    if (halfB.Real >= 0)
                        tmp = -Math.Pow(halfB.Real, 1 / 3);
                    else
                        tmp = Math.Pow(-halfB.Real, 1 / 3);

                    results.Add(2 * tmp - offset);
                    // really should return next root twice, but we return only one
                    results.Add(-tmp - offset);
                }
            }

            return results.ToArray();
        }

        ///////////////////////////////////////////////////////////////////
        /**
            Calculates roots of quartic polynomial. <br/>
            First, derivative roots are found, then used to split quartic polynomial 
            into segments, each containing one root of quartic polynomial.
            Segments are then passed to newton's method to find roots.

            @returns {Array<Number>} roots
        */
        public Complex[] GetQuarticRoots()
        {
            var results = new List<Complex>();

            var n = GetDegree();
            if (n == 4)
            {
                var poly = new Polynomial();
                poly.Coefficients = Coefficients.Slice();
                poly.Divide(poly.Coefficients[n].Real);
                var ERRF = 1e-15;
                if (Math.Abs(poly.Coefficients[0].Real) < 10 * ERRF * Math.Abs(poly.Coefficients[3].Real))
                    poly.Coefficients[0] = 0;
                var poly_d = poly.GetDerivative();
                var derrt = new List<Complex>(poly_d.GetRoots());
                derrt.Sort();
                var dery = new List<Complex>();
                int nr = derrt.Count - 1;
                int i;
                var rb = Bounds();
                var maxabsX = Math.Max(Math.Abs(rb.minX), Math.Abs(rb.maxX));
                var ZEROepsilon = zeroErrorEstimate(maxabsX);

                for (i = 0; i <= nr; i++)
                {
                    dery.Add(poly.Eval(derrt[i]));
                }

                for (i = 0; i <= nr; i++)
                {
                    if (Math.Abs(dery[i].Real) < ZEROepsilon)
                        dery[i] = 0;
                }

                i = 0;
                var dx = Math.Max(0.1 * (rb.maxX - rb.minX) / n, ERRF);
                var guesses = new List<Complex>();
                var minmax = new List<(double, double)>();
                if (nr > -1)
                {
                    if (dery[0] != 0)
                    {
                        if (Math.Sign(dery[0].Real) != Math.Sign(poly.Eval(derrt[0] - dx).Real - dery[0].Real))
                        {
                            guesses.Add(derrt[0] - dx);
                            minmax.Add((rb.minX, derrt[0].Real));
                        }
                    }
                    else
                    {
                        results.Add(derrt[0], derrt[0]);
                        i++;
                    }

                    for (; i < nr; i++)
                    {
                        if (dery[i + 1] == 0)
                        {
                            results.Add(derrt[i + 1], derrt[i + 1]);
                            i++;
                        }
                        else if (Math.Sign(dery[i].Real) != Math.Sign(dery[i + 1].Real))
                        {
                            guesses.Add((derrt[i] + derrt[i + 1]) / 2);
                            minmax.Add((derrt[i].Real, derrt[i + 1].Real));
                        }
                    }
                    if (dery[nr] != 0 && Math.Sign(dery[nr].Real) != Math.Sign(poly.Eval(derrt[nr].Real + dx).Real - dery[nr].Real))
                    {
                        guesses.Add(derrt[nr] + dx);
                        minmax.Add((derrt[nr].Real, rb.maxX));
                    }
                }

                if (guesses.Count > 0)
                {
                    for (i = 0; i < guesses.Count; i++)
                    {
                        guesses[i] = Polynomial.NewtonSecantBisection(guesses[i], (x, f) => poly.Eval(x), (x, df) => poly_d.Eval(x), 32, minmax[i][0], minmax[i][1]);
                    }
                }

                results = results.Concat(guesses).ToList();
            }
            return results.ToArray();
        }

        ///////////////////////////////////////////////////////////////////
        /**
            Estimate what is the maximum polynomial evaluation error value under which polynomial evaluation could be in fact 0.
            
            @returns {Number} 
*/
        public double zeroErrorEstimate()
        {
            var poly = this;
            var ERRF = 1e-15;
            var rb = poly.Bounds();
            double maxabsX = Math.Max(Math.Abs(rb.minX), Math.Abs(rb.maxX));
            if (maxabsX < 0.001)
            {
                return 2 * Math.Abs(poly.Eval(ERRF).Real);
            }
            var n = poly.Coefficients.Length - 1;
            var an = poly.Coefficients[n];
            return 10 * ERRF * poly.Coefficients.Aggregate((m, v, i) =>
            {
                var nm = v / an * Math.Pow(maxabsX, i);
                return nm > m ? nm : m;
            }, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxabsX"></param>
        /// <returns></returns>
        public double zeroErrorEstimate(double maxabsX)
        {
            var poly = this;
            var ERRF = 1e-15;
            if (maxabsX < 0.001)
            {
                return 2 * Math.Abs(poly.Eval(ERRF).Real);
            }
            var n = poly.Coefficients.Length - 1;
            var an = poly.Coefficients[n];
            return 10 * ERRF * poly.Coefficients.Aggregate((m, v, i) =>
            {
                var nm = v / an * Math.Pow(maxabsX, i);
                return nm > m ? nm : m;
            }, 0);
        }

        ///////////////////////////////////////////////////////////////////
        /**
            Calculates upper Real roots bounds. <br/>
            Real roots are in interval [negX, posX]. Determined by Fujiwara method.
            @see {@link http://en.wikipedia.org/wiki/Properties_of_polynomial_roots}

            @returns {{ negX: Number, posX: Number }}
*/
        public (double negX, double posX) BoundsUpperRealFujiwara()
        {
            var a = Coefficients;
            var n = a.Length - 1;
            var an = a[n];
            if (an != 1)
            {
                a = Coefficients.Select((v) => { return v / an; }).ToArray();
            }
            var b = a.Select((v, i) => { return (i < n) ? Math.Pow(Math.Abs((i == 0) ? v.Real / 2 : v.Real), 1 / (n - i)) : v; });

            //var find2Max = (acc, bi, i) =>
            // {
            //     if (coefSelectionFunc(i))
            //     {
            //         if (acc.max < bi)
            //         {
            //             acc.nearmax = acc.max;
            //             acc.max = bi;
            //         }
            //         else if (acc.nearmax < bi)
            //         {
            //             acc.nearmax = bi;
            //         }
            //     }
            //     return acc;
            // };

            (Complex max, Complex nearmax) zed = (max: 0, nearmax: 0);

            //coefSelectionFunc(i)=> { return i < n && a[i] < 0; };
            List<Complex> max_nearmax_pos = b.Aggregate(((Complex max, Complex nearmax) acc, Complex bi, int i) => {
                if ((i) => { return i < n && a[i] < 0; })
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
            }, zed).ToList();

            //coefSelectionFunc(i)=> { return i < n && ((n % 2 == i % 2) ? a[i] < 0 : a[i] > 0); };
            var max_nearmax_neg = b.Aggregate((double acc, double bi, int i) =>
            {
                if ((i) => { return i < n && ((n % 2 == i % 2) ? a[i] < 0 : a[i] > 0); })
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
            }, zed);

            return (negX: -2 * max_nearmax_neg.max, posX: 2 * max_nearmax_pos.max);
        }

        ///////////////////////////////////////////////////////////////////
        /** 
            Calculates lower Real roots bounds. <br/>
            There are no Real roots in interval <negX, posX>. Determined by Fujiwara method.
            @see {@link http://en.wikipedia.org/wiki/Properties_of_polynomial_roots}

            @returns {{ negX: Number, posX: Number }}
        */
        public (double negX, double posX) BoundsLowerRealFujiwara()
        {
            var poly = new Polynomial();
            poly.Coefficients = Coefficients.Slice();
            Array.Reverse(poly.Coefficients);
            var res = poly.BoundsUpperRealFujiwara();
            res.negX = 1 / res.negX;
            res.posX = 1 / res.posX;
            return res;
        }

        ///////////////////////////////////////////////////////////////////
        /** 
            Calculates left and right Real roots bounds. <br/>
            Real roots are in interval [minX, maxX]. Combines Fujiwara lower and upper bounds to get minimal interval.
            @see {@link http://en.wikipedia.org/wiki/Properties_of_polynomial_roots}

            @returns {{ minX: Number, maxX: Number }}
*/
        public (double minX, double maxX) Bounds()
        {
            var urb = BoundsUpperRealFujiwara();
            var rb = (minX: urb.negX, maxX: urb.posX);
            if (urb.negX == 0 && urb.posX == 0)
                return rb;
            if (urb.negX == 0)
            {
                rb.minX = BoundsLowerRealFujiwara().posX;
            }
            else if (urb.posX == 0)
            {
                rb.maxX = BoundsLowerRealFujiwara().negX;
            }
            if (rb.minX > rb.maxX)
            {
                //console.log('public void bounds: poly has no real roots? or floating point error?');
                rb.minX = rb.maxX = 0;
            }
            return rb;
            // TODO: if sure that there are no complex roots 
            // (maybe by using Sturm's theorem) use:
            //return this.bounds_Real_Laguerre();
        }

        /////////////////////////////////////////////////////////////////// 
        /**
            Newton's (Newton-Raphson) method for finding Real roots on univariate function. <br/>
            When using bounds, algorithm falls back to secant if newton goes out of range.
            Bisection is fallback for secant when determined secant is not efficient enough.
            @see {@link http://en.wikipedia.org/wiki/Newton%27s_method}
            @see {@link http://en.wikipedia.org/wiki/Secant_method}
            @see {@link http://en.wikipedia.org/wiki/Bisection_method}

            @param {Number} x0 - Inital root guess
            @param {function(x)} f - Function which root we are trying to find
            @param {function(x)} df - Derivative of function f
            @param {Number} max_iterations - Maximum number of algorithm iterations
            @param {Number} [min_x] - Left bound value
            @param {Number} [max_x] - Right bound value
            @returns {Number} - root
*/
        public double NewtonSecantBisection(double x0, Func<double, double> f, Func<double, double> df, int max_iterations, double min, double max)
        {
            double prev_dfx = 0, dfx, prev_x_ef_correction = 0, x_correction, x_new;
            double v, y_atmin = 0, y_atmax = 0;
            double x = x0;
            var ACCURACY = 14;
            var min_correction_factor = Math.Pow(10, -ACCURACY);
            var isBounded = (min is double && max is double);
            if (isBounded)
            {
                if (min > max)
                    throw new Exception("newton root finding: min must be greater than max");
                y_atmin = f(min);
                y_atmax = f(max);
                if (Math.Sign(y_atmin) == Math.Sign(y_atmax))
                    throw new Exception("newton root finding: y values of bounds must be of opposite sign");
            }

            int i;
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
                x_new = x - x_correction;
                if ((Math.Abs(x_correction) <= min_correction_factor * Math.Abs(x))
                        || (prev_x_ef_correction == (x - x_correction) - x))
                {
                    break;
                }

                if (isBounded)
                {
                    if (Math.Sign(y) == Math.Sign(y_atmax))
                    {
                        max = x;
                        y_atmax = y;
                    }
                    else if (Math.Sign(y) == Math.Sign(y_atmin))
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
                        if (Math.Sign(y_atmin) == Math.Sign(y_atmax))
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
                            x_correction = x - (min + dx * 0.5);
                        }
                        else if (Math.Abs(dy / Math.Min(y_atmin, y_atmax)) > RATIO_LIMIT)
                        {
                            //stepMethod = 'aimed bisect';
                            x_correction = x - (min + dx * (0.5 + (Math.Abs(y_atmin) < Math.Abs(y_atmax) ? -AIMED_BISECT_OFFSET : AIMED_BISECT_OFFSET)));
                        }
                        else
                        {
                            //stepMethod = 'secant'; 
                            x_correction = x - (min - y_atmin / dy * dx);
                        }
                        x_new = x - x_correction;

                        if ((Math.Abs(x_correction) <= min_correction_factor * Math.Abs(x))
                        || (prev_x_ef_correction == (x - x_correction) - x))
                        {
                            break;
                        }
                    }
                }
                //details.Add([stepMethod, i, x, x_new, x_correction, min, max, y]);
                prev_x_ef_correction = x - x_new;
                x = x_new;
            }
            //details.Add([stepMethod, i, x, x_new, x_correction, min, max, y]);
            //console.log(details.join('\r\n'));
            //if (i == max_iterations)
            //    console.log('newt: steps=' + ((i==max_iterations)? i:(i + 1)));
            return x;
        }

        #region Overrides

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
            => (ToString() == ((Polynomial)obj).ToString());

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [SecuritySafeCritical]
        public override int GetHashCode()
            => Coefficients.GetHashCode();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => ConvertToString(String.Empty, CultureInfo.In­variantCulture);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string ToString(string format)
            => ConvertToString(format, CultureInfo.In­variantCulture);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string ToString(String format, IFormatProvider formatProvider)
            => ConvertToString(format, formatProvider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string ConvertToString(string format, IFormatProvider formatProvider)
        {
            if (IsZero) return "0";
            else
            {
                string s = "";

                for (int i = 0; i < Degree + 1; i++)
                {
                    if (Coefficients[i] != Complex.Zero)
                    {
                        if (Coefficients[i] == Complex.I)
                            s += "i";
                        else if (Coefficients[i] != Complex.One)
                        {
                            if (Coefficients[i].IsReal && Coefficients[i].Real > 0)
                                s += Coefficients[i].ToString(format, formatProvider);
                            else
                                s += "(" + Coefficients[i].ToString(format, formatProvider) + ")";

                        }
                        else if (/*Coefficients[i] == Complex.One && */i == 0)
                            s += 1;

                        if (i == 1)
                            s += "x";
                        else if (i > 1)
                            s += "x^" + i.ToString(format, formatProvider);
                    }

                    if (i < Degree && Coefficients[i + 1] != 0 && s.Length > 0)
                        s += " + ";
                }

                return s;
            }
        }

        #endregion
    }
}