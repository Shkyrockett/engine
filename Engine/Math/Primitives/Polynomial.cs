using System.Collections.Generic;
using static System.Math;
using static Engine.Maths;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Numerics;
using System.Text;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using System.Globalization;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class Polynomial
        : IFormattable
    {
        /// <summary>
        /// 
        /// </summary>
        bool isReadonly;

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
        private static Polynomial Sextic(double a, double b, double c, double d, double e, double f)
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
        private static Polynomial Septic(double a, double b, double c, double d, double e, double f, double g)
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
        private static Polynomial Octic(double a, double b, double c, double d, double e, double f, double g, double h)
            => new Polynomial(a, b, c, d, e, f, g, h);

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
                throw new ArgumentOutOfRangeException();
            var res = new double[power + 1];
            res[power] = coefficient;
            return new Polynomial(res);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        public static Polynomial X() { return new Polynomial(0, 1); }

        /// <summary>
        /// 
        /// </summary>
        public Polynomial()
        {
            Coefficients = new List<double>(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coefficients"></param>
        public Polynomial(params double[] coefficients)
        {
            if (coefficients?.Length == 0) Coefficients = new List<double>(1);
            else Coefficients = new List<double>(coefficients);
        }

        /// <summary>
        /// 
        /// </summary>
        public int Degree
            => Coefficients.Count - 1;

        /// <summary>
        /// 
        /// </summary>
        public List<double> Coefficients { get; set; }

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
            var res = new double[p.Coefficients.Count];
            for (int i = 0; i < res.Length; i++)
                res[i] = m * p.Coefficients[i];
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
            var res = new double[p.Coefficients.Count];
            for (int i = 0; i < res.Length; i++)
                res[i] = p.Coefficients[i] / m;
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
            var res = new double[a.Coefficients.Count + b.Coefficients.Count - 1];
            for (var i = 0; i < a.Coefficients.Count; i++)
                for (var j = 0; j < b.Coefficients.Count; j++)
                {
                    var mul = a.Coefficients[i] * b.Coefficients[j];
                    res[i + j] += mul;
                }
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
            var res = new double[Max(a.Coefficients.Count, b.Coefficients.Count)];
            for (int i = 0; i < res.Length; i++)
            {
                double p = 0;
                if (i < a.Coefficients.Count) p += a.Coefficients[i];
                if (i < b.Coefficients.Count) p += b.Coefficients[i];
                res[i] = p;
            }
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
            var res = new double[Max(a.Coefficients.Count, b.Coefficients.Count)];
            for (int i = 0; i < res.Length; i++)
            {
                double p = 0;
                if (i < a.Coefficients.Count) p += a.Coefficients[i];
                if (i < b.Coefficients.Count) p -= b.Coefficients[i];
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
            var res = new double[a.Coefficients.Count];
            for (int i = 0; i < res.Length; i++)
                res[i] = -a.Coefficients[i];
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
            var res = new double[b.Coefficients.Count];
            for (int i = 0; i < res.Length; i++) res[i] = b.Coefficients[i];
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
            var res = new double[a.Coefficients.Count];
            for (int i = 0; i < res.Length; i++) res[i] = -a.Coefficients[i];
            res[0] += b;
            return new Polynomial(res);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, this)) return true;
            var p = obj as Polynomial;
            if (p == null) return false;
            if (Coefficients.Count != p.Coefficients.Count) return false;
            for (int i = 0; i < Coefficients.Count; i++)
                if (Abs(Coefficients[i] - p.Coefficients[i]) > Epsilon) return false;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override Int32 GetHashCode()
            => Coefficients.GetHashCode();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial Normalize()
        {
            int order = 0;
            double high = 1;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                if (Abs(Coefficients[i]) > Epsilon)
                {
                    order = i;
                    high = Coefficients[i];
                }
            }
            var res = new double[order + 1];
            for (int i = 0; i < res.Length; i++)
            {
                if (Abs(Coefficients[i]) > Epsilon)
                {
                    res[i] = Coefficients[i] / high;
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
        public Polynomial Trim() { return Trim(Epsilon); }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial Trim(double epsilon)
        {
            int order = 0;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                if (Abs(Coefficients[i]) > Epsilon)
                {
                    order = i;
                }
            }
            var res = new double[order + 1];
            for (int i = 0; i < res.Length; i++)
            {
                if (Abs(Coefficients[i]) > Epsilon)
                {
                    res[i] = Coefficients[i];
                }
            }

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
        public Polynomial Pow(int n)
        {
            if (n < 0)
                throw new ArgumentOutOfRangeException();
            var order = Coefficients.Count - 1;
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
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Polynomial Derivate()
        {
            var res = new double[Max(1, Coefficients.Count - 1)];
            for (int i = 1; i < Coefficients.Count; i++)
                res[i - 1] = i * Coefficients[i];
            return new Polynomial(res);
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
            double res = 0;
            double xcoef = 1;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                res += Coefficients[i] * xcoef;
                xcoef *= x;
            }
            return res;
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
            for (int i = 0; i < Coefficients.Count; i++)
            {
                res += Coefficients[i] * xcoef;
                xcoef *= x;
            }
            return res;
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
            var res = new double[Coefficients.Count + 1];
            res[0] = term0;
            for (int i = 0; i < Coefficients.Count; i++)
                res[i + 1] = Coefficients[i] / (i + 1);
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
        public int RealOrder()
            => RealOrder(Coefficients.ToArray());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coefficients"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RealOrder(params double[] coefficients)
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
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CanSolveRealRoots()
            => (RealOrder() <= 4);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<double> SolveRealRoots()
            => SolveRealRoots(Coefficients.ToArray());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coefficients"></param>
        /// <returns></returns>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<double> SolveRealRoots(params double[] coefficients)
        {
            switch (RealOrder(coefficients))
            {
                case 0:
                    break;
                case 1:
                    yield return -coefficients[0] / coefficients[1];
                    break;
                case 2:
                    {
                        var delta = coefficients[1] * coefficients[1] - 4 * coefficients[2] * coefficients[0];
                        if (delta < 0)
                            yield break;
                        var sd = Sqrt(delta);
                        yield return (-coefficients[1] - sd) / 2 / coefficients[2];
                        if (sd > Epsilon)
                            yield return (-coefficients[1] + sd) / 2 / coefficients[2];
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
                        var p = -a * a / 3 + b;
                        var q = (2 * a * a * a - 9 * a * b + 27 * c) / 27;
                        if (Abs(p) < Epsilon)
                        {
                            // t^3 + q = 0  => t = -q^1/3 => x = -q^1/3 - a/3
                            yield return -Crt(p) - a / 3;
                        }
                        else if (Abs(q) < Epsilon)
                        {
                            // t^3 + pt = 0  => t (t^2 + p) = 0
                            // t = 0, t = +/- (-p)^1/2
                            yield return -a / 3;
                            if (p < 0)
                            {
                                var root = Crt(p);
                                yield return root - a / 3;
                                yield return -root - a / 3;
                            }
                        }
                        else
                        {
                            var disc = q * q / 4 + p * p * p / 27;
                            if (disc < -Epsilon)
                            {
                                // 3 roots
                                var r = Sqrt(-p * p * p / 27);
                                var phi = Acos(MinMax(-q / 2 / r, -1, 1));
                                var t1 = 2 * Crt(r);
                                yield return t1 * Cos(phi / 3) - a / 3;
                                yield return t1 * Cos((phi + 2 * PI) / 3) - a / 3;
                                yield return t1 * Cos((phi + 4 * PI) / 3) - a / 3;
                            }
                            else if (disc < Epsilon)
                            {
                                // 2 real roots
                                var cq = Crt(q / 2);
                                yield return -2 * cq - a / 3;
                                yield return cq - a / 3;
                            }
                            else
                            {
                                // 1 real root
                                var sd = Sqrt(disc);
                                yield return Crt(-q / 2 + sd) - Crt(q / 2 + sd) - a / 3;
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
                        var p = c - 3 * b * b / 8;
                        var q = (b * b * b - 4 * b * c + 8 * d) / 8;
                        var r = (-3 * b * b * b * b + 256 * e - 64 * b * d + 16 * b * b * c) / 256;
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
                                yield return y - b / 4;
                                yield return -y - b / 4;
                            }
                            yield break;
                        }
                        // <=> (y^2 + p + m)^2 = (p + 2m) y^2 -q y + (m^2 + 2 m p + p^2 - r)
                        // where m is **arbitrary** choose it to make perfect square
                        // i.e. m^3 + 5/2 p m^2 + (2 p^2 - r) m + (p^3/2 - p r /2 - q^2 / 8) = 0
                        var m = SolveRealRoots(p * p * p / 2 - p * r / 2 - q * q / 8, 2 * p * p - r, 5.0 / 2.0 * p, 1)
                            .Where(x => p + 2 * x > Epsilon)
                            .First();
                        // <=> (y^2 + y Sqrt(p+2m) + p+m - q/2/sqrt(p+2m)) (y^2 - y Sqrt(p+2m) + p+m + q/2/sqrt(p+2m))
                        var sqrt = Sqrt(p + 2 * m);
                        var poly1 = SolveRealRoots(p + m - q / 2 / sqrt, sqrt, 1);
                        var poly2 = SolveRealRoots(p + m + q / 2 / sqrt, -sqrt, 1);
                        foreach (var y in poly1.Concat(poly2))
                            yield return y - b / 4;
                    }
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Will try to solve root analytically, and if it can will use numerical approach.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<double> SolveOrFindRealRoots()
            => (CanSolveRealRoots()) ? SolveRealRoots() : FindRoots().Where(c => Abs(c.Imaginary) < Epsilon).Select(c => c.Real);

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
            if (p.Coefficients.Count == 1) return new Complex[0];

            Complex x0 = 1;
            Complex xMul = 0.4 + 0.9 * Complex.ImaginaryOne;
            var R0 = new Complex[p.Coefficients.Count - 1];
            for (int i = 0; i < R0.Length; i++)
            {
                R0[i] = x0;
                x0 *= xMul;
            }

            var R1 = new Complex[p.Coefficients.Count - 1];
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
                    if (Abs(c.Real) > Epsilon || Abs(c.Imaginary) > Epsilon) return false;
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
        /// Construct a polynomial P such as ys[i] = P.Compute(i).
        /// </summary>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        /// <param name="x0"></param>
        /// <param name="x1"></param>
        /// <param name="minY"></param>
        /// <param name="maxY"></param>
        /// <remarks>
        /// https://github.com/superlloyd/Poly
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetMinMax(double x0, double x1, out double minY, out double maxY)
        {
            var points = Derivate().SolveOrFindRealRoots().Where(t => t > x0 && t < x1).Concat(new[] { x0, x1 });
            bool first = true;
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
                        minY = y;
                    if (y > maxY)
                        maxY = y;
                }
            }
        }

        /// <summary>
        /// Creates a human-readable string that represents this <see cref="Polynomial"/> inherited class.
        /// </summary>
        /// <returns></returns>        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
            => ConvertToString(null /* format string */, CultureInfo.InvariantCulture /* format provider */);

        /// <summary>
        /// Creates a string representation of this <see cref="Polynomial"/> inherited class based on the IFormatProvider
        /// passed in.  If the provider is null, the CurrentCulture is used.
        /// </summary>
        /// <returns>
        /// A string representation of this object.
        /// </returns>        [DebuggerStepThrough]
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
        /// </returns>        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        string IFormattable.ToString(string format, IFormatProvider provider)
            => ConvertToString(format, provider);

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
        /// </returns>        public virtual string ConvertToString(string format, IFormatProvider provider)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < Coefficients.Count; i++)
            {
                var val = Coefficients[i];
                if (Abs(val) < Epsilon)
                    continue;
                if (val > 0 && sb.Length > 0)
                    sb.Append('+');
                if (i > 0 && (Abs(val) - 1) < Epsilon)
                {
                    if (val < 0)
                        sb.Append('-');
                }
                else
                {
                    sb.Append(val);
                }
                if (i > 0)
                    sb.Append('x');
                if (i > 1)
                    sb.Append('^').Append(i);
            }
            if (sb.Length == 0)
                sb.Append('0');
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public List<double> RootsInInterval(double min, double max)
        {
            var roots = new List<double>();
            double? root;

            if (Degree == 1)
            {
                root = Bisection(min, max);
                if (root != null) roots.Add(root.Value);
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
                    if (root != null) roots.Add(root.Value);

                    // find root on [droots[i],droots[i+1]] for 0 <= i <= count-2
                    for (int i = 0; i <= droots.Count - 2; i++)
                    {
                        root = Bisection(droots[i], droots[i + 1]);
                        if (root != null) roots.Add(root.Value);
                    }

                    // find root on [droots[count-1],xmax]
                    root = Bisection(droots[droots.Count - 1], max);
                    if (root != null) roots.Add(root.Value);
                }
                else
                {
                    // polynomial is monotone on [min,max], has at most one root
                    root = Bisection(min, max);
                    if (root != null) roots.Add(root.Value);
                }
            }

            return roots;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public double? Bisection(double min, double max)
        {
            var minValue = Eval(min);
            var maxValue = Eval(max);
            double? result = null;

            if (Abs(minValue) <= Tolerance)
                result = min;
            else if (Abs(maxValue) <= Tolerance)
                result = max;
            else if (minValue * maxValue <= 0)
            {
                var tmp1 = Log(max - min);
                var tmp2 = LN10 * Accuracy;
                var iters = Ceiling((tmp1 + tmp2) / LN2);

                for (var i = 0; i < iters; i++)
                {
                    result = 0.5 * (min + max);
                    var value = Eval(result.Value);

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
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double Eval(double x)
        {
            if (Double.IsNaN(x))
                throw new Exception("Polynomial.Eval: parameter must be a number");

            var result = 0d;

            for (var i = Coefficients.Count - 1; i >= 0; i--)
                result = result * x + Coefficients[i];

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roots"></param>
        public void RemoveMultipleRootsIn01(List<double> roots)
        {
            var ZEROepsilon = 1e-15;
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
        /// 
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        /// <returns></returns>
        public static Polynomial Bezout(double[] e1, double[] e2)
        {
            var AB = e1[0] * e2[1] - e2[0] * e1[1];
            var AC = e1[0] * e2[2] - e2[0] * e1[2];
            var AD = e1[0] * e2[3] - e2[0] * e1[3];
            var AE = e1[0] * e2[4] - e2[0] * e1[4];
            var AF = e1[0] * e2[5] - e2[0] * e1[5];
            var BC = e1[1] * e2[2] - e2[1] * e1[2];
            var BE = e1[1] * e2[4] - e2[1] * e1[4];
            var BF = e1[1] * e2[5] - e2[1] * e1[5];
            var CD = e1[2] * e2[3] - e2[2] * e1[3];
            var DE = e1[3] * e2[4] - e2[3] * e1[4];
            var DF = e1[3] * e2[5] - e2[3] * e1[5];
            var BFpDE = BF + DE;
            var BEmCD = BE - CD;

            return new Polynomial(
                AB * BC - AC * AC,
                AB * BEmCD + AD * BC - 2 * AC * AE,
                AB * BFpDE + AD * BEmCD - AE * AE - 2 * AC * AF,
                AB * DF + AD * BFpDE - 2 * AE * AF,
                AD * DF - AF * AF
            );
        }
    }
}
