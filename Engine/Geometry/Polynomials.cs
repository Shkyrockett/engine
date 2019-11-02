using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class Polynomials
    {
        #region Bézier Bernstein Basis Overloads
        /// <summary>
        /// Coefficients for a Linear Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) BezierBernsteinBasis(double a, double b) => LinearBezierBernsteinBasis(a, b);

        /// <summary>
        /// Coefficients for a Quadratic Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) BezierBernsteinBasis(double a, double b, double c) => QuadraticBezierBernsteinBasis(a, b, c);

        /// <summary>
        /// Coefficients for a Cubic Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) BezierBernsteinBasis(double a, double b, double c, double d) => CubicBezierBernsteinBasis(a, b, c, d);

        /// <summary>
        /// Coefficients for a Quartic Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E) BezierBernsteinBasis(double a, double b, double c, double d, double e) => QuarticBezierBernsteinBasis(a, b, c, d, e);

        /// <summary>
        /// Coefficients for a Quintic Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F) BezierBernsteinBasis(double a, double b, double c, double d, double e, double f) => QuinticBezierBernsteinBasis(a, b, c, d, e, f);

        /// <summary>
        /// Coefficients for a Sextic Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F, double G) BezierBernsteinBasis(double a, double b, double c, double d, double e, double f, double g) => SexticBezierBernsteinBasis(a, b, c, d, e, f, g);

        /// <summary>
        /// Coefficients for a Sextic Bézier curve.
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F, double G, double H) BezierBernsteinBasis(double a, double b, double c, double d, double e, double f, double g, double h) => SepticBezierBernsteinBasis(a, b, c, d, e, f, g, h);

        /// <summary>
        /// Coefficients for a Octic Bézier curve.
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
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F, double G, double H, double I) BezierBernsteinBasis(double a, double b, double c, double d, double e, double f, double g, double h, double i) => OcticBezierBernsteinBasis(a, b, c, d, e, f, g, h, i);

        /// <summary>
        /// Coefficients for a Nonic Bézier curve.
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
        /// <param name="j"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F, double G, double H, double I, double J) BezierBernsteinBasis(double a, double b, double c, double d, double e, double f, double g, double h, double i, double j) => NonicBezierBernsteinBasis(a, b, c, d, e, f, g, h, i, j);

        /// <summary>
        /// Coefficients for a Decic Bézier curve.
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
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F, double G, double H, double I, double J, double K) BezierBernsteinBasis(double a, double b, double c, double d, double e, double f, double g, double h, double i, double j, double k) => DecicBezierBernsteinBasis(a, b, c, d, e, f, g, h, i, j, k);
        #endregion Bézier Bernstein Basis Overloads

        #region Bézier Bernstein Basis Recursive
        /// <summary>
        /// Coefficients for a Linear Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial LinearBezierBernsteinBasisRecursive(double a, double b) => (Polynomial.OneMinusT * a) + (Polynomial.T * b);

        /// <summary>
        /// Coefficients for a Quadratic Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial QuadraticBezierBernsteinBasisRecursive(double a, double b, double c) => (Polynomial.OneMinusT * LinearBezierBernsteinBasisRecursive(a, b)) + (Polynomial.T * LinearBezierBernsteinBasisRecursive(b, c));

        /// <summary>
        /// Coefficients for a Cubic Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial CubicBezierBernsteinBasisRecursive(double a, double b, double c, double d) => (Polynomial.OneMinusT * QuadraticBezierBernsteinBasisRecursive(a, b, c)) + (Polynomial.T * QuadraticBezierBernsteinBasisRecursive(b, c, d));

        /// <summary>
        /// Coefficients for a Quartic Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial QuarticBezierBernsteinBasisRecursive(double a, double b, double c, double d, double e) => (Polynomial.OneMinusT * CubicBezierBernsteinBasisRecursive(a, b, c, d)) + (Polynomial.T * CubicBezierBernsteinBasisRecursive(b, c, d, e));

        /// <summary>
        /// Coefficients for a Quintic Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial QuinticBezierBernsteinBasisRecursive(double a, double b, double c, double d, double e, double f) => (Polynomial.OneMinusT * QuarticBezierBernsteinBasisRecursive(a, b, c, d, e)) + (Polynomial.T * QuarticBezierBernsteinBasisRecursive(b, c, d, e, f));

        /// <summary>
        /// Coefficients for a Sextic Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial SexticBezierBernsteinBasisRecursive(double a, double b, double c, double d, double e, double f, double g) => (Polynomial.OneMinusT * QuinticBezierBernsteinBasisRecursive(a, b, c, d, e, f)) + (Polynomial.T * QuinticBezierBernsteinBasisRecursive(b, c, d, e, f, g));

        /// <summary>
        /// Coefficients for a Septic Bézier curve.
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
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial SepticBezierBernsteinBasisRecursive(double a, double b, double c, double d, double e, double f, double g, double h) => (Polynomial.OneMinusT * SexticBezierBernsteinBasisRecursive(a, b, c, d, e, f, g)) + (Polynomial.T * SexticBezierBernsteinBasisRecursive(b, c, d, e, f, g, h));

        /// <summary>
        /// Coefficients for a Octic Bézier curve.
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
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial OcticBezierBernsteinBasisRecursive(double a, double b, double c, double d, double e, double f, double g, double h, double i) => (Polynomial.OneMinusT * SepticBezierBernsteinBasisRecursive(a, b, c, d, e, f, g, h)) + (Polynomial.T * SepticBezierBernsteinBasisRecursive(b, c, d, e, f, g, h, i));

        /// <summary>
        /// Coefficients for a Nonic Bézier curve.
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
        /// <param name="j"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial NonicBezierBernsteinBasisRecursive(double a, double b, double c, double d, double e, double f, double g, double h, double i, double j) => (Polynomial.OneMinusT * OcticBezierBernsteinBasisRecursive(a, b, c, d, e, f, g, h, i)) + (Polynomial.T * OcticBezierBernsteinBasisRecursive(b, c, d, e, f, g, h, i, j));

        /// <summary>
        /// Coefficients for a Decic Bézier curve.
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
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial DecicBezierBernsteinBasisRecursive(double a, double b, double c, double d, double e, double f, double g, double h, double i, double j, double k) => (Polynomial.OneMinusT * NonicBezierBernsteinBasisRecursive(a, b, c, d, e, f, g, h, i, j)) + (Polynomial.T * NonicBezierBernsteinBasisRecursive(b, c, d, e, f, g, h, i, j, k));
        #endregion Bézier Bernstein Polynomial Recursive

        /// <summary>
        /// Coefficients for a Cubic B-Spline curve Bernstein basis.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) CubicBSplineBernsteinBasis(double a, double b, double c, double d)
            => (A: a + 4d * b + c,
                B: -3d * a + 3d * c,
                C: 3d * a - 6d * b + 3d * c,
                D: a + 3d * b - 3d * c + d);

        /// <summary>
        /// Coefficients for a Quadratic Hermite curve Bernstein basis.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) QuadraticHermiteBernsteinBasis(double a, double b, double c)
            => (A: c - a - (b - a),
                B: b - a,
                C: a);

        /// <summary>
        /// Coefficients for a Cubic Hermite curve Bernstein basis.
        /// </summary>
        /// <param name="p0">a.</param>
        /// <param name="t0">The b.</param>
        /// <param name="p1">The c.</param>
        /// <param name="t1">The d.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) CubicHermiteBernsteinBasis(double p0, double t0, double p1, double t1)
            => (A: 2d * p0 + t0 - 2d * p1 + t1,
                B: -3d * p0 - 2d * t0 + 3d * p1 - t1,
                C: t0,
                D: p0);

        #region Bézier Bernstein Basis
        /// <summary>
        /// Coefficients for a Linear Bézier curve Bernstein basis.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://fontforge.github.io/bezier.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) LinearBezierBernsteinBasis(double a, double b)
            => (A: b - a,
                B: a);

        /// <summary>
        /// Coefficients for a Quadratic Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <acknowledgment>
        /// http://fontforge.github.io/bezier.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C) QuadraticBezierBernsteinBasis(double a, double b, double c)
            => (A: c - (2d * b) + a,
                B: 2d * (b - a),
                C: a);

        /// <summary>
        /// Coefficients for a Cubic Bézier Bernstein curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://www.gamedev.net/topic/643117-coefficients-for-bezier-curves/
        /// http://fontforge.github.io/bezier.html
        /// http://idav.ucdavis.edu/education/CAGDNotes/Matrix-Cubic-Bezier-Curve/Matrix-Cubic-Bezier-Curve.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D) CubicBezierBernsteinBasis(double a, double b, double c, double d)
            => (A: d - (3d * c) + (3d * b) - a,
                B: (3d * c) - (6d * b) + (3d * a),
                C: 3d * (b - a),
                D: a);

        /// <summary>
        /// Coefficients for a Quartic Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Coefficient calculation found in the matrix representation at:
        /// http://www.dglr.de/publikationen/2016/420062.pdf
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E) QuarticBezierBernsteinBasis(double a, double b, double c, double d, double e)
            => (A: e - (4d * d) + (6d * c) - (4d * b) + a,
                B: (4d * d) - (12d * c) + (12d * b) - (4d * a),
                C: (6d * c) - (12d * b) + (6d * a),
                D: 4d * (b - a),
                E: a);

        /// <summary>
        /// Coefficients for a Quintic Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Based off of pseudo-code for the matrix found at:
        /// https://simtk.org/api_docs/opensim/api_docs/classOpenSim_1_1SegmentedQuinticBezierToolkit.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F) QuinticBezierBernsteinBasis(double a, double b, double c, double d, double e, double f)
            => (A: f - (5d * e) + (10d * d) - (10d * c) + (5d * b) - a,
                B: (5d * e) - (20d * d) + (30d * c) - (20d * b) + (5d * a),
                C: (10d * d) - (30d * c) + (30d * b) - (10d * a),
                D: (10d * c) - (20d * b) + (10d * a),
                E: 5d * (b - a),
                F: a);

        /// <summary>
        /// Coefficients for a Sextic Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <param name="f"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F, double G) SexticBezierBernsteinBasis(double a, double b, double c, double d, double e, double f, double g)
            => (A: g - (6d * f) + (15d * e) - (20d * d) + (15d * c) - (6d * b) + a,
                B: (6d * f) - (30d * e) + (60d * d) - (60d * c) + (30d * b) - (6d * a),
                C: (15d * e) - (60d * d) + (90d * c) - (60d * b) + (15d * a),
                D: (20d * d) - (60d * c) + (60d * b) - (20d * a),
                E: (15d * c) - (30d * b) + (15d * a),
                F: (6d * b) - (6d * a),
                G: a);

        /// <summary>
        /// Coefficients for a Septic Bézier curve.
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
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F, double G, double H) SepticBezierBernsteinBasis(double a, double b, double c, double d, double e, double f, double g, double h)
            => (A: h - (7d * g) + (21d * f) - (35d * e) + (35d * d) - (21d * c) + (7d * b) - a,
                B: (7d * g) - (42d * f) + (105d * e) - (140d * d) + (105d * c) - (42d * b) + (7d * a),
                C: (21d * f) - (105d * e) + (210d * d) - (210d * c) + (105d * b) - (21d * a),
                D: (35d * e) - (140d * d) + (210d * c) - (140d * b) + (35d * a),
                E: (35d * d) - (105d * c) + (105d * b) - (35d * a),
                F: (21d * c) - (42d * b) + (21d * a),
                G: (7d * b) - (7d * a),
                H: a);

        /// <summary>
        /// Coefficients for a Octic Bézier curve.
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
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F, double G, double H, double I) OcticBezierBernsteinBasis(double a, double b, double c, double d, double e, double f, double g, double h, double i)
            => (A: i - (8d * h) + (28d * g) - (56d * f) + (70d * e) - (56d * d) + (28d * c) - (8d * b) + a,
                B: (8d * h) - (56d * g) + (168d * f) - (280d * e) + (280d * d) - (168d * c) + (56d * b) - (8d * a),
                C: (28d * g) - (168d * f) + (420d * e) - (560d * d) + (420d * c) - (168d * b) + (28d * a),
                D: (56d * f) - (280d * e) + (560d * d) - (560d * c) + (280d * b) - (56d * a),
                E: (70d * e) - (280d * d) + (420d * c) - (280d * b) + (70d * a),
                F: (56d * d) - (168d * c) + (168d * b) - (56d * a),
                G: (28d * c) - (56d * b) + (28d * a),
                H: (8d * b) - (8d * a),
                I: a);

        /// <summary>
        /// Coefficients for a Nonic Bézier curve.
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
        /// <param name="j"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F, double G, double H, double I, double J) NonicBezierBernsteinBasis(double a, double b, double c, double d, double e, double f, double g, double h, double i, double j)
            => (A: j - (9d * i) + (36d * h) - (84d * g) + (126d * f) - (126d * e) + (84d * d) - (36d * c) + (9d * b) - a,
                B: (9d * i) - (72d * h) + (252d * g) - (504d * f) + (630d * e) - (504d * d) + (252d * c) - (72d * b) + (9d * a),
                C: (36d * h) - (252d * g) + (756d * f) - (1260d * e) + (1260d * d) - (756d * c) + (252d * b) - (36d * a),
                D: (84d * g) - (504d * f) + (1260d * e) - (1680d * d) + (1260d * c) - (504d * b) + (84d * a),
                E: (126d * f) - (630d * e) + (1260d * d) - (1260d * c) + (630d * b) - (126d * a),
                F: (126d * e) - (504d * d) + (756d * c) - (504d * b) + (126d * a),
                G: (84d * d) - (252d * c) + (252d * b) - (84d * a),
                H: (36d * c) - (72d * b) + (36d * a),
                I: (9d * b) - (9d * a),
                J: a);

        /// <summary>
        /// Coefficients for a Decic Bézier curve.
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
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://github.com/superlloyd/Poly
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B, double C, double D, double E, double F, double G, double H, double I, double J, double K) DecicBezierBernsteinBasis(double a, double b, double c, double d, double e, double f, double g, double h, double i, double j, double k)
            => (A: k - (10d * j) + (45d * i) - (120d * h) + (210d * g) - (252d * f) + (210d * e) - (120d * d) + (45d * c) - (10d * b) + a,
                B: (10d * j) - (90d * i) + (360d * h) - (840d * g) + (1260d * f) - (1260d * e) + (840d * d) - (360d * c) + (90d * b) - (10d * a),
                C: (45d * i) - (360d * h) + (1260d * g) - (2520d * f) + (3150d * e) - (2520d * d) + (1260d * c) - (360d * b) + (45d * a),
                D: (120d * h) - (840d * g) + (2520d * f) - (4200d * e) + (4200d * d) - (2520d * c) + (840d * b) - (120d * a),
                E: (210d * g) - (1260d * f) + (3150d * e) - (4200d * d) + (3150d * c) - (1260d * b) + (210d * a),
                F: (252d * f) - (1260d * e) + (2520d * d) - (2520d * c) + (1260d * b) - (252d * a),
                G: (210d * e) - (840d * d) + (1260d * c) - (840d * b) + (210d * a),
                H: (120d * d) - (360d * c) + (360d * b) - (120d * a),
                I: (45d * c) - (90d * b) + (45d * a),
                J: (10d * b) - (10d * a),
                K: a);
        #endregion Bézier Bernstein Basis

        #region Bezier Polynomial From Parametric
        /// <summary>
        /// Gets the linear polynomial from linear bezier.
        /// </summary>
        /// <param name="aX">a x.</param>
        /// <param name="aY">a y.</param>
        /// <param name="bX">The b x.</param>
        /// <param name="bY">The b y.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b) GetLinearPolynomialFromParametricLinearBezier(
            double aX, double aY,
            double bX, double bY)
        {
            return (
                a: aX * aX + aY * aY - 2 * aX * bX + bX * bX - 2 * aY * bY + bY * bY,
                b: -aX * aX - aY * aY + aX * bX + aY * bY
                );
        }

        /// <summary>
        /// Gets the cubic polynomial from quadratic bezier.
        /// </summary>
        /// <param name="aX">a x.</param>
        /// <param name="aY">a y.</param>
        /// <param name="bX">The b x.</param>
        /// <param name="bY">The b y.</param>
        /// <param name="cX">The c x.</param>
        /// <param name="cY">The c y.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://jwezorek.com/2015/01/my-code-for-doing-two-things-that-sooner-or-later-you-will-want-to-do-with-bezier-curves/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d) GetCubicPolynomialFromParametricQuadraticBezier(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY)
        {
            return (
               a: 2 * aX * aX + 2 * aY * aY - 8 * aX * bX + 8 * bX * bX - 8 * aY * bY + 8 * bY * bY + 4 * aX * cX - 8 * bX * cX + 2 * cX * cX + 4 * aY * cY - 8 * bY * cY + 2 * cY * cY,
               b: -6 * aX * aX - 6 * aY * aY + 18 * aX * bX - 12 * bX * bX + 18 * aY * bY - 12 * bY * bY - 6 * aX * cX + 6 * bX * cX - 6 * aY * cY + 6 * bY * cY,
               c: 6 * aX * aX + 6 * aY * aY - 12 * aX * bX + 4 * bX * bX - 12 * aY * bY + 4 * bY * bY + 2 * aX * cX + 2 * aY * cY,
               d: -2 * aX * aX - 2 * aY * aY + 2 * aX * bX + 2 * aY * bY
               );
        }

        /// <summary>
        /// Gets the quintic polynomial from cubic bezier and point.
        /// </summary>
        /// <param name="aX">a x.</param>
        /// <param name="aY">a y.</param>
        /// <param name="bX">The b x.</param>
        /// <param name="bY">The b y.</param>
        /// <param name="cX">The c x.</param>
        /// <param name="cY">The c y.</param>
        /// <param name="dX">The d x.</param>
        /// <param name="dY">The d y.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://jwezorek.com/2015/01/my-code-for-doing-two-things-that-sooner-or-later-you-will-want-to-do-with-bezier-curves/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e, double f) GetQuinticPolynomialFromParametricCubicBezier(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double dX, double dY)
        {
            return (
                a: 3 * aX * aX + 3 * aY * aY - 18 * aX * bX + 27 * bX * bX - 18 * aY * bY + 27 * bY * bY + 18 * aX * cX - 54 * bX * cX + 27 * cX * cX + 18 * aY * cY - 54 * bY * cY + 27 * cY * cY - 6 * aX * dX + 18 * bX * dX - 18 * cX * dX + 3 * dX * dX - 6 * aY * dY + 18 * bY * dY - 18 * cY * dY + 3 * dY * dY,
                b: -15 * aX * aX - 15 * aY * aY + 75 * aX * bX - 90 * bX * bX + 75 * aY * bY - 90 * bY * bY - 60 * aX * cX + 135 * bX * cX - 45 * cX * cX - 60 * aY * cY + 135 * bY * cY - 45 * cY * cY + 15 * aX * dX - 30 * bX * dX + 15 * cX * dX + 15 * aY * dY - 30 * bY * dY + 15 * cY * dY,
                c: 30 * aX * aX + 30 * aY * aY - 120 * aX * bX + 108 * bX * bX - 120 * aY * bY + 108 * bY * bY + 72 * aX * cX - 108 * bX * cX + 18 * cX * cX + 72 * aY * cY - 108 * bY * cY + 18 * cY * cY - 12 * aX * dX + 12 * bX * dX - 12 * aY * dY + 12 * bY * dY,
                d: -30 * aX * aX - 30 * aY * aY + 90 * aX * bX - 54 * bX * bX + 90 * aY * bY - 54 * bY * bY - 36 * aX * cX + 27 * bX * cX - 36 * aY * cY + 27 * bY * cY + 3 * aX * dX + 3 * aY * dY,
                e: 15 * aX * aX + 15 * aY * aY - 30 * aX * bX + 9 * bX * bX - 30 * aY * bY + 9 * bY * bY + 6 * aX * cX + 6 * aY * cY,
                f: -3 * aX * aX - 3 * aY * aY + 3 * aX * bX + 3 * aY * bY
                );
        }

        /// <summary>
        /// Gets the quintic polynomial from cubic bezier and point.
        /// </summary>
        /// <param name="aX">a x.</param>
        /// <param name="aY">a y.</param>
        /// <param name="bX">The b x.</param>
        /// <param name="bY">The b y.</param>
        /// <param name="cX">The c x.</param>
        /// <param name="cY">The c y.</param>
        /// <param name="dX">The d x.</param>
        /// <param name="dY">The d y.</param>
        /// <param name="eX">The e x.</param>
        /// <param name="eY">The e y.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://jwezorek.com/2015/01/my-code-for-doing-two-things-that-sooner-or-later-you-will-want-to-do-with-bezier-curves/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e, double f, double g, double h) GetSepticPolynomialFromQuarticBezier(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double dX, double dY,
            double eX, double eY)
        {
            return (
                a: 4 * aX * aX + 4 * aY * aY - 32 * aX * bX + 64 * bX * bX - 32 * aY * bY + 64 * bY * bY + 48 * aX * cX - 192 * bX * cX + 144 * cX * cX + 48 * aY * cY - 192 * bY * cY + 144 * cY * cY - 32 * aX * dX + 128 * bX * dX - 192 * cX * dX + 64 * dX * dX - 32 * aY * dY + 128 * bY * dY - 192 * cY * dY + 64 * dY * dY + 8 * aX * eX - 32 * bX * eX + 48 * cX * eX - 32 * dX * eX + 4 * eX * eX + 8 * aY * eY - 32 * bY * eY + 48 * cY * eY - 32 * dY * eY + 4 * eY * eY,
                b: -28 * aX * aX - 28 * aY * aY + 196 * aX * bX - 336 * bX * bX + 196 * aY * bY - 336 * bY * bY - 252 * aX * cX + 840 * bX * cX - 504 * cX * cX - 252 * aY * cY + 840 * bY * cY - 504 * cY * cY + 140 * aX * dX - 448 * bX * dX + 504 * cX * dX - 112 * dX * dX + 140 * aY * dY - 448 * bY * dY + 504 * cY * dY - 112 * dY * dY - 28 * aX * eX + 84 * bX * eX - 84 * cX * eX + 28 * dX * eX - 28 * aY * eY + 84 * bY * eY - 84 * cY * eY + 28 * dY * eY,
                c: 84 * aX * aX + 84 * aY * aY - 504 * aX * bX + 720 * bX * bX - 504 * aY * bY + 720 * bY * bY + 540 * aX * cX - 1440 * bX * cX + 648 * cX * cX + 540 * aY * cY - 1440 * bY * cY + 648 * cY * cY - 240 * aX * dX + 576 * bX * dX - 432 * cX * dX + 48 * dX * dX - 240 * aY * dY + 576 * bY * dY - 432 * cY * dY + 48 * dY * dY + 36 * aX * eX - 72 * bX * eX + 36 * cX * eX + 36 * aY * eY - 72 * bY * eY + 36 * cY * eY,
                d: -140 * aX * aX - 140 * aY * aY + 700 * aX * bX - 800 * bX * bX + 700 * aY * bY - 800 * bY * bY - 600 * aX * cX + 1200 * bX * cX - 360 * cX * cX - 600 * aY * cY + 1200 * bY * cY - 360 * cY * cY + 200 * aX * dX - 320 * bX * dX + 120 * cX * dX + 200 * aY * dY - 320 * bY * dY + 120 * cY * dY - 20 * aX * eX + 20 * bX * eX - 20 * aY * eY + 20 * bY * eY,
                e: 140 * aX * aX + 140 * aY * aY - 560 * aX * bX + 480 * bX * bX - 560 * aY * bY + 480 * bY * bY + 360 * aX * cX - 480 * bX * cX + 72 * cX * cX + 360 * aY * cY - 480 * bY * cY + 72 * cY * cY - 80 * aX * dX + 64 * bX * dX - 80 * aY * dY + 64 * bY * dY + 4 * aX * eX + 4 * aY * eY,
                f: -84 * aX * aX - 84 * aY * aY + 252 * aX * bX - 144 * bX * bX + 252 * aY * bY - 144 * bY * bY - 108 * aX * cX + 72 * bX * cX - 108 * aY * cY + 72 * bY * cY + 12 * aX * dX + 12 * aY * dY,
                g: 28 * aX * aX + 28 * aY * aY - 56 * aX * bX + 16 * bX * bX - 56 * aY * bY + 16 * bY * bY + 12 * aX * cX + 12 * aY * cY,
                h: -4 * aX * aX - 4 * aY * aY + 4 * aX * bX + 4 * aY * bY
                );
        }

        /// <summary>
        /// Gets the nonic polynomial from quintic bezier and point.
        /// </summary>
        /// <param name="aX">a x.</param>
        /// <param name="aY">a y.</param>
        /// <param name="bX">The b x.</param>
        /// <param name="bY">The b y.</param>
        /// <param name="cX">The c x.</param>
        /// <param name="cY">The c y.</param>
        /// <param name="dX">The d x.</param>
        /// <param name="dY">The d y.</param>
        /// <param name="eX">The e x.</param>
        /// <param name="fX">The f x.</param>
        /// <param name="fY">The f y.</param>
        /// <param name="eY">The e y.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://jwezorek.com/2015/01/my-code-for-doing-two-things-that-sooner-or-later-you-will-want-to-do-with-bezier-curves/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e, double f, double g, double h, double i, double j) GetNonicPolynomialFromParametricQuinticBezier(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double dX, double dY,
            double eX, double fX,
            double fY, double eY)
        {
            return (
                a: 5 * aX * aX + 5 * aY * aY - 50 * aX * bX + 125 * bX * bX - 50 * aY * bY + 125 * bY * bY + 100 * aX * cX - 500 * bX * cX + 500 * cX * cX + 100 * aY * cY - 500 * bY * cY + 500 * cY * cY - 100 * aX * dX + 500 * bX * dX - 1000 * cX * dX + 500 * dX * dX - 100 * aY * dY + 500 * bY * dY - 1000 * cY * dY + 500 * dY * dY + 50 * aX * eX - 250 * bX * eX + 500 * cX * eX - 500 * dX * eX + 125 * eX * eX + 50 * aY * eY - 250 * bY * eY + 500 * cY * eY - 500 * dY * eY + 125 * eY * eY - 10 * aX * fX + 50 * bX * fX - 100 * cX * fX + 100 * dX * fX - 50 * eX * fX + 5 * fX * fX - 10 * aY * fY + 50 * bY * fY - 100 * cY * fY + 100 * dY * fY - 50 * eY * fY + 5 * fY * fY,
                b: -45 * aX * aX - 45 * aY * aY + 405 * aX * bX - 900 * bX * bX + 405 * aY * bY - 900 * bY * bY - 720 * aX * cX + 3150 * bX * cX - 2700 * cX * cX - 720 * aY * cY + 3150 * bY * cY - 2700 * cY * cY + 630 * aX * dX - 2700 * bX * dX + 4500 * cX * dX - 1800 * dX * dX + 630 * aY * dY - 2700 * bY * dY + 4500 * cY * dY - 1800 * dY * dY - 270 * aX * eX + 1125 * bX * eX - 1800 * cX * eX + 1350 * dX * eX - 225 * eX * eX - 270 * aY * eY + 1125 * bY * eY - 1800 * cY * eY + 1350 * dY * eY - 225 * eY * eY + 45 * aX * fX - 180 * bX * fX + 270 * cX * fX - 180 * dX * fX + 45 * eX * fX + 45 * aY * fY - 180 * bY * fY + 270 * cY * fY - 180 * dY * fY + 45 * eY * fY,
                c: 180 * aX * aX + 180 * aY * aY - 1440 * aX * bX + 2800 * bX * bX - 1440 * aY * bY + 2800 * bY * bY + 2240 * aX * cX - 8400 * bX * cX + 6000 * cX * cX + 2240 * aY * cY - 8400 * bY * cY + 6000 * cY * cY - 1680 * aX * dX + 6000 * bX * dX - 8000 * cX * dX + 2400 * dX * dX - 1680 * aY * dY + 6000 * bY * dY - 8000 * cY * dY + 2400 * dY * dY + 600 * aX * eX - 2000 * bX * eX + 2400 * cX * eX - 1200 * dX * eX + 100 * eX * eX + 600 * aY * eY - 2000 * bY * eY + 2400 * cY * eY - 1200 * dY * eY + 100 * eY * eY - 80 * aX * fX + 240 * bX * fX - 240 * cX * fX + 80 * dX * fX - 80 * aY * fY + 240 * bY * fY - 240 * cY * fY + 80 * dY * fY,
                d: -420 * aX * aX - 420 * aY * aY + 2940 * aX * bX - 4900 * bX * bX + 2940 * aY * bY - 4900 * bY * bY - 3920 * aX * cX + 12250 * bX * cX - 7000 * cX * cX - 3920 * aY * cY + 12250 * bY * cY - 7000 * cY * cY + 2450 * aX * dX - 7000 * bX * dX + 7000 * cX * dX - 1400 * dX * dX + 2450 * aY * dY - 7000 * bY * dY + 7000 * cY * dY - 1400 * dY * dY - 700 * aX * eX + 1750 * bX * eX - 1400 * cX * eX + 350 * dX * eX - 700 * aY * eY + 1750 * bY * eY - 1400 * cY * eY + 350 * dY * eY + 70 * aX * fX - 140 * bX * fX + 70 * cX * fX + 70 * aY * fY - 140 * bY * fY + 70 * cY * fY,
                e: 630 * aX * aX + 630 * aY * aY - 3780 * aX * bX + 5250 * bX * bX - 3780 * aY * bY + 5250 * bY * bY + 4200 * aX * cX - 10500 * bX * cX + 4500 * cX * cX + 4200 * aY * cY - 10500 * bY * cY + 4500 * cY * cY - 2100 * aX * dX + 4500 * bX * dX - 3000 * cX * dX + 300 * dX * dX - 2100 * aY * dY + 4500 * bY * dY - 3000 * cY * dY + 300 * dY * dY + 450 * aX * eX - 750 * bX * eX + 300 * cX * eX + 450 * aY * eY - 750 * bY * eY + 300 * cY * eY - 30 * aX * fX + 30 * bX * fX - 30 * aY * fY + 30 * bY * fY,
                f: -630 * aX * aX - 630 * aY * aY + 3150 * aX * bX - 3500 * bX * bX + 3150 * aY * bY - 3500 * bY * bY - 2800 * aX * cX + 5250 * bX * cX - 1500 * cX * cX - 2800 * aY * cY + 5250 * bY * cY - 1500 * cY * cY + 1050 * aX * dX - 1500 * bX * dX + 500 * cX * dX + 1050 * aY * dY - 1500 * bY * dY + 500 * cY * dY - 150 * aX * eX + 125 * bX * eX - 150 * aY * eY + 125 * bY * eY + 5 * aX * fX + 5 * aY * fY,
                g: 420 * aX * aX + 420 * aY * aY - 1680 * aX * bX + 1400 * bX * bX - 1680 * aY * bY + 1400 * bY * bY + 1120 * aX * cX - 1400 * bX * cX + 200 * cX * cX + 1120 * aY * cY - 1400 * bY * cY + 200 * cY * cY - 280 * aX * dX + 200 * bX * dX - 280 * aY * dY + 200 * bY * dY + 20 * aX * eX + 20 * aY * eY,
                h: -180 * aX * aX - 180 * aY * aY + 540 * aX * bX - 300 * bX * bX + 540 * aY * bY - 300 * bY * bY - 240 * aX * cX + 150 * bX * cX - 240 * aY * cY + 150 * bY * cY + 30 * aX * dX + 30 * aY * dY,
                i: 45 * aX * aX + 45 * aY * aY - 90 * aX * bX + 25 * bX * bX - 90 * aY * bY + 25 * bY * bY + 20 * aX * cX + 20 * aY * cY,
                j: -5 * aX * aX - 5 * aY * aY + 5 * aX * bX + 5 * aY * bY
                );
        }

        /// <summary>
        /// Gets the linear polynomial from linear bezier and point.
        /// </summary>
        /// <param name="aX">a x.</param>
        /// <param name="aY">a y.</param>
        /// <param name="bX">The b x.</param>
        /// <param name="bY">The b y.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://jwezorek.com/2015/01/my-code-for-doing-two-things-that-sooner-or-later-you-will-want-to-do-with-bezier-curves/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b) GetLinearPolynomialFromLinearBezierAndPoint(
            double aX, double aY,
            double bX, double bY,
            double x, double y)
        {
            var t1 = (aX * aX) - (2d * aX * bX) + (bX * bX) + (aY * aY) - (2d * aY * bY) + (bY * bY);
            var t0 = (x * aX) - (aX * aX) - (x * bX) + (aX * bX) + (y * aY) - (aY * aY) - (y * bY) + (aY * bY);

            return (t1 == 0 ? t0 : t0 / t1, 1d);
        }

        /// <summary>
        /// Gets the cubic polynomial from quadratic bezier and point.
        /// </summary>
        /// <param name="aX">a x.</param>
        /// <param name="aY">a y.</param>
        /// <param name="bX">The b x.</param>
        /// <param name="bY">The b y.</param>
        /// <param name="cX">The c x.</param>
        /// <param name="cY">The c y.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://jwezorek.com/2015/01/my-code-for-doing-two-things-that-sooner-or-later-you-will-want-to-do-with-bezier-curves/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d) GetCubicPolynomialFromParametricQuadraticBezierAndPoint(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double x, double y)
        {
            var t3 = (2d * aX * aX) - (8d * aX * bX) + (8d * bX * bX) + (4d * aX * cX) - (8d * bX * cX) + (2d * cX * cX) + (2d * aY * aY) - (8d * aY * bY) + (8d * bY * bY) + (4d * aY * cY) - (8d * bY * cY) + (2d * cY * cY);
            var t2 = (-6d * aX * aX) + (18d * aX * bX) - (12d * bX * bX) - (6d * aX * cX) + (6d * bX * cX) - (6d * aY * aY) + (18d * aY * bY) - (12d * bY * bY) - (6d * aY * cY) + (6d * bY * cY);
            var t1 = (-2d * x * aX) + (6d * aX * aX) + (4d * x * bX) - (12d * aX * bX) + (4d * bX * bX) - (2d * x * cX) + (2d * aX * cX) - (2d * y * aY) + (6d * aY * aY) + (4d * y * bY) - (12d * aY * bY) + (4d * bY * bY) - (2d * y * cY) + (2d * aY * cY);
            var t0 = (2d * x * aX) - (2d * aX * aX) - (2d * x * bX) + (2d * aX * bX) + (2d * y * aY) - (2d * aY * aY) - (2d * y * bY) + (2d * aY * bY);

            return t3 == 0 ? (t0, t1, t2, 1d) : (t0 / t3, t1 / t3, t2 / t3, 1d);
        }

        /// <summary>
        /// Gets the quintic polynomial from cubic bezier and point.
        /// </summary>
        /// <param name="aX">a x.</param>
        /// <param name="aY">a y.</param>
        /// <param name="bX">The b x.</param>
        /// <param name="bY">The b y.</param>
        /// <param name="cX">The c x.</param>
        /// <param name="cY">The c y.</param>
        /// <param name="dX">The d x.</param>
        /// <param name="dY">The d y.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://jwezorek.com/2015/01/my-code-for-doing-two-things-that-sooner-or-later-you-will-want-to-do-with-bezier-curves/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e, double f) GetQuinticPolynomialFromParametricCubicBezierAndPoint(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double dX, double dY,
            double x, double y)
        {
            var t5 = (3d * aX * aX) - (18d * aX * bX) + (27d * bX * bX) + (18d * aX * cX) - (54d * bX * cX) + (27d * cX * cX) - (6d * aX * dX) + (18d * bX * dX) - (18d * cX * dX) + (3d * dX * dX) + (3d * aY * aY) - (18d * aY * bY) + (27d * bY * bY) + (18d * aY * cY) - (54d * bY * cY) + (27d * cY * cY) - (6d * aY * dY) + (18d * bY * dY) - (18d * cY * dY) + (3d * dY * dY);
            var t4 = (-15d * aX * aX) + (75d * aX * bX) - (90d * bX * bX) - (60d * aX * cX) + (135d * bX * cX) - (45d * cX * cX) + (15d * aX * dX) - (30d * bX * dX) + (15d * cX * dX) - (15d * aY * aY) + (75d * aY * bY) - (90d * bY * bY) - (60d * aY * cY) + (135d * bY * cY) - (45d * cY * cY) + (15d * aY * dY) - (30d * bY * dY) + (15d * cY * dY);
            var t3 = (30d * aX * aX) - (120d * aX * bX) + (108d * bX * bX) + (72d * aX * cX) - (108d * bX * cX) + (18d * cX * cX) - (12d * aX * dX) + (12d * bX * dX) + (30d * aY * aY) - (120d * aY * bY) + (108d * bY * bY) + (72d * aY * cY) - (108d * bY * cY) + (18d * cY * cY) - (12d * aY * dY) + (12d * bY * dY);
            var t2 = (3d * x * aX) - (30d * aX * aX) - (9d * x * bX) + (90d * aX * bX) - (54d * bX * bX) + (9d * x * cX) - (36d * aX * cX) + (27d * bX * cX) - (3d * x * dX) + (3d * aX * dX) + (3d * y * aY) - (30d * aY * aY) - (9d * y * bY) + (90d * aY * bY) - (54d * bY * bY) + (9d * y * cY) - (36d * aY * cY) + (27d * bY * cY) - (3d * y * dY) + (3d * aY * dY);
            var t1 = (-6d * x * aX) + (15d * aX * aX) + (12d * x * bX) - (30d * aX * bX) + (9d * bX * bX) - (6d * x * cX) + (6d * aX * cX) - (6d * y * aY) + (15d * aY * aY) + (12d * y * bY) - (30d * aY * bY) + (9d * bY * bY) - (6d * y * cY) + (6d * aY * cY);
            var t0 = (3d * x * aX) - (3d * aX * aX) - (3d * x * bX) + (3d * aX * bX) + (3d * y * aY) - (3d * aY * aY) - (3d * y * bY) + (3d * aY * bY);

            return t5 == 0 ? (t0, t1, t2, t3, t4, 1d) : (t0 / t5, t1 / t5, t2 / t5, t3 / t5, t4 / t5, 1d);
        }

        /// <summary>
        /// Gets the quintic polynomial from cubic bezier and point.
        /// </summary>
        /// <param name="aX">a x.</param>
        /// <param name="aY">a y.</param>
        /// <param name="bX">The b x.</param>
        /// <param name="bY">The b y.</param>
        /// <param name="cX">The c x.</param>
        /// <param name="cY">The c y.</param>
        /// <param name="dX">The d x.</param>
        /// <param name="dY">The d y.</param>
        /// <param name="eX">The e x.</param>
        /// <param name="eY">The e y.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://jwezorek.com/2015/01/my-code-for-doing-two-things-that-sooner-or-later-you-will-want-to-do-with-bezier-curves/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e, double f, double g, double h) GetSepticPolynomialFromParametricQuarticBezierAndPoint(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double dX, double dY,
            double eX, double eY,
            double x, double y)
        {
            var t7 = (4d * aX * aX) - (32d * aX * bX) + (64d * bX * bX) + (48d * aX * cX) - (192d * bX * cX) + (144d * cX * cX) - (32d * aX * dX) + (128d * bX * dX) - (192d * cX * dX) + (64d * dX * dX) + (8d * aX * eX) - (32d * bX * eX) + (48d * cX * eX) - (32d * dX * eX) + (4d * eX * eX) + (4d * aY * aY) - (32 * aY * bY) + (64d * bY * bY) + (48d * aY * cY) - (192d * bY * cY) + (144d * cY * cY) - (32d * aY * dY) + (128d * bY * dY) - (192d * cY * dY) + (64d * dY * dY) + (8d * aY * eY) - (32d * bY * eY) + (48d * cY * eY) - (32d * dY * eY) + (4d * eY * eY);
            var t6 = (-28d * aX * aX) + (196d * aX * bX) - (336d * bX * bX) - (252d * aX * cX) + (840d * bX * cX) - (504d * cX * cX) + (140d * aX * dX) - (448d * bX * dX) + (504d * cX * dX) - (112d * dX * dX) - (28d * aX * eX) + (84d * bX * eX) - (84d * cX * eX) + (28d * dX * eX) - (28d * aY * aY) + (196d * aY * bY) - (336d * bY * bY) - (252d * aY * cY) + (840d * bY * cY) - (504d * cY * cY) + (140d * aY * dY) - (448d * bY * dY) + (504d * cY * dY) - (112d * dY * dY) - (28d * aY * eY) + (84d * bY * eY) - (84d * cY * eY) + (28d * dY * eY);
            var t5 = (84d * aX * aX) - (504d * aX * bX) + (720d * bX * bX) + (540d * aX * cX) - (1440d * bX * cX) + (648d * cX * cX) - (240d * aX * dX) + (576d * bX * dX) - (432d * cX * dX) + (48d * dX * dX) + (36d * aX * eX) - (72d * bX * eX) + (36d * cX * eX) + (84d * aY * aY) - (504d * aY * bY) + (720d * bY * bY) + (540d * aY * cY) - (1440d * bY * cY) + (648d * cY * cY) - (240d * aY * dY) + (576d * bY * dY) - (432d * cY * dY) + (48d * dY * dY) + (36d * aY * eY) - (72d * bY * eY) + (36d * cY * eY);
            var t4 = (-140d * aX * aX) + (700d * aX * bX) - (800d * bX * bX) - (600d * aX * cX) + (1200d * bX * cX) - (360d * cX * cX) + (200d * aX * dX) - (320d * bX * dX) + (120d * cX * dX) - (20d * aX * eX) + (20d * bX * eX) - (140d * aY * aY) + (700d * aY * bY) - (800d * bY * bY) - (600d * aY * cY) + (1200d * bY * cY) - (360d * cY * cY) + (200d * aY * dY) - (320d * bY * dY) + (120d * cY * dY) - (20d * aY * eY) + (20d * bY * eY);
            var t3 = (-4d * x * aX) + (140d * aX * aX) + (16d * x * bX) - (560d * aX * bX) + (480d * bX * bX) - (24d * x * cX) + (360d * aX * cX) - (480d * bX * cX) + (72d * cX * cX) + (16d * x * dX) - (80d * aX * dX) + (64d * bX * dX) - (4d * x * eX) + (4d * aX * eX) - (4d * y * aY) + (140d * aY * aY) + (16d * y * bY) - (560d * aY * bY) + (480d * bY * bY) - (24d * y * cY) + (360d * aY * cY) - (480d * bY * cY) + (72d * cY * cY) + (16d * y * dY) - (80d * aY * dY) + (64d * bY * dY) - (4d * y * eY) + (4d * aY * eY);
            var t2 = (12d * x * aX) - (84d * aX * aX) - (36d * x * bX) + (252d * aX * bX) - (144d * bX * bX) + (36d * x * cX) - (108d * aX * cX) + (72d * bX * cX) - (12d * x * dX) + (12d * aX * dX) + (12d * y * aY) - (84d * aY * aY) - (36d * y * bY) + (252d * aY * bY) - (144d * bY * bY) + (36d * y * cY) - (108d * aY * cY) + (72d * bY * cY) - (12d * y * dY) + (12d * aY * dY);
            var t1 = (-12d * x * aX) + (28d * aX * aX) + (24d * x * bX) - (56d * aX * bX) + (16d * bX * bX) - (12d * x * cX) + (12d * aX * cX) - (12d * y * aY) + (28d * aY * aY) + (24d * y * bY) - (56d * aY * bY) + (16d * bY * bY) - (12d * y * cY) + (12d * aY * cY);
            var t0 = (4d * x * aX) - (4d * aX * aX) - (4d * x * bX) + (4d * aX * bX) + (4d * y * aY) - (4d * aY * aY) - (4d * y * bY) + (4d * aY * bY);

            return t7 == 0 ? (t0, t1, t2, t3, t4, t5, t6, 1d) : (t0 / t7, t1 / t7, t2 / t7, t3 / t7, t4 / t7, t5 / t7, t6 / t7, 1d);
        }

        /// <summary>
        /// Gets the nonic polynomial from quintic bezier and point.
        /// </summary>
        /// <param name="aX">a x.</param>
        /// <param name="aY">a y.</param>
        /// <param name="bX">The b x.</param>
        /// <param name="bY">The b y.</param>
        /// <param name="cX">The c x.</param>
        /// <param name="cY">The c y.</param>
        /// <param name="dX">The d x.</param>
        /// <param name="dY">The d y.</param>
        /// <param name="eX">The e x.</param>
        /// <param name="fX">The f x.</param>
        /// <param name="fY">The f y.</param>
        /// <param name="eY">The e y.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://jwezorek.com/2015/01/my-code-for-doing-two-things-that-sooner-or-later-you-will-want-to-do-with-bezier-curves/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e, double f, double g, double h, double i, double j) GetNonicPolynomialFromParametricQuinticBezierAndPoint(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double dX, double dY,
            double eX, double fX,
            double fY, double eY,
            double x, double y)
        {
            var t9 = 5 * aX * aX + 5 * aY * aY - 50 * aX * bX + 125 * bX * bX - 50 * aY * bY + 125 * bY * bY + 100 * aX * cX - 500 * bX * cX + 500 * cX * cX + 100 * aY * cY - 500 * bY * cY + 500 * cY * cY - 100 * aX * dX + 500 * bX * dX - 1000 * cX * dX + 500 * dX * dX - 100 * aY * dY + 500 * bY * dY - 1000 * cY * dY + 500 * dY * dY + 50 * aX * eX - 250 * bX * eX + 500 * cX * eX - 500 * dX * eX + 125 * eX * eX + 50 * aY * eY - 250 * bY * eY + 500 * cY * eY - 500 * dY * eY + 125 * eY * eY - 10 * aX * fX + 50 * bX * fX - 100 * cX * fX + 100 * dX * fX - 50 * eX * fX + 5 * fX * fX - 10 * aY * fY + 50 * bY * fY - 100 * cY * fY + 100 * dY * fY - 50 * eY * fY + 5 * fY * fY;
            var t8 = -45 * aX * aX - 45 * aY * aY + 405 * aX * bX - 900 * bX * bX + 405 * aY * bY - 900 * bY * bY - 720 * aX * cX + 3150 * bX * cX - 2700 * cX * cX - 720 * aY * cY + 3150 * bY * cY - 2700 * cY * cY + 630 * aX * dX - 2700 * bX * dX + 4500 * cX * dX - 1800 * dX * dX + 630 * aY * dY - 2700 * bY * dY + 4500 * cY * dY - 1800 * dY * dY - 270 * aX * eX + 1125 * bX * eX - 1800 * cX * eX + 1350 * dX * eX - 225 * eX * eX - 270 * aY * eY + 1125 * bY * eY - 1800 * cY * eY + 1350 * dY * eY - 225 * eY * eY + 45 * aX * fX - 180 * bX * fX + 270 * cX * fX - 180 * dX * fX + 45 * eX * fX + 45 * aY * fY - 180 * bY * fY + 270 * cY * fY - 180 * dY * fY + 45 * eY * fY;
            var t7 = 180 * aX * aX + 180 * aY * aY - 1440 * aX * bX + 2800 * bX * bX - 1440 * aY * bY + 2800 * bY * bY + 2240 * aX * cX - 8400 * bX * cX + 6000 * cX * cX + 2240 * aY * cY - 8400 * bY * cY + 6000 * cY * cY - 1680 * aX * dX + 6000 * bX * dX - 8000 * cX * dX + 2400 * dX * dX - 1680 * aY * dY + 6000 * bY * dY - 8000 * cY * dY + 2400 * dY * dY + 600 * aX * eX - 2000 * bX * eX + 2400 * cX * eX - 1200 * dX * eX + 100 * eX * eX + 600 * aY * eY - 2000 * bY * eY + 2400 * cY * eY - 1200 * dY * eY + 100 * eY * eY - 80 * aX * fX + 240 * bX * fX - 240 * cX * fX + 80 * dX * fX - 80 * aY * fY + 240 * bY * fY - 240 * cY * fY + 80 * dY * fY;
            var t6 = -420 * aX * aX - 420 * aY * aY + 2940 * aX * bX - 4900 * bX * bX + 2940 * aY * bY - 4900 * bY * bY - 3920 * aX * cX + 12250 * bX * cX - 7000 * cX * cX - 3920 * aY * cY + 12250 * bY * cY - 7000 * cY * cY + 2450 * aX * dX - 7000 * bX * dX + 7000 * cX * dX - 1400 * dX * dX + 2450 * aY * dY - 7000 * bY * dY + 7000 * cY * dY - 1400 * dY * dY - 700 * aX * eX + 1750 * bX * eX - 1400 * cX * eX + 350 * dX * eX - 700 * aY * eY + 1750 * bY * eY - 1400 * cY * eY + 350 * dY * eY + 70 * aX * fX - 140 * bX * fX + 70 * cX * fX + 70 * aY * fY - 140 * bY * fY + 70 * cY * fY;
            var t5 = 630 * aX * aX + 630 * aY * aY - 3780 * aX * bX + 5250 * bX * bX - 3780 * aY * bY + 5250 * bY * bY + 4200 * aX * cX - 10500 * bX * cX + 4500 * cX * cX + 4200 * aY * cY - 10500 * bY * cY + 4500 * cY * cY - 2100 * aX * dX + 4500 * bX * dX - 3000 * cX * dX + 300 * dX * dX - 2100 * aY * dY + 4500 * bY * dY - 3000 * cY * dY + 300 * dY * dY + 450 * aX * eX - 750 * bX * eX + 300 * cX * eX + 450 * aY * eY - 750 * bY * eY + 300 * cY * eY - 30 * aX * fX + 30 * bX * fX - 30 * aY * fY + 30 * bY * fY;
            var t4 = -630 * aX * aX - 630 * aY * aY + 3150 * aX * bX - 3500 * bX * bX + 3150 * aY * bY - 3500 * bY * bY - 2800 * aX * cX + 5250 * bX * cX - 1500 * cX * cX - 2800 * aY * cY + 5250 * bY * cY - 1500 * cY * cY + 1050 * aX * dX - 1500 * bX * dX + 500 * cX * dX + 1050 * aY * dY - 1500 * bY * dY + 500 * cY * dY - 150 * aX * eX + 125 * bX * eX - 150 * aY * eY + 125 * bY * eY + 5 * aX * fX + 5 * aY * fY + 5 * aX * x - 25 * bX * x + 50 * cX * x - 50 * dX * x + 25 * eX * x - 5 * fX * x + 5 * aY * y - 25 * bY * y + 50 * cY * y - 50 * dY * y + 25 * eY * y - 5 * fY * y;
            var t3 = 420 * aX * aX + 420 * aY * aY - 1680 * aX * bX + 1400 * bX * bX - 1680 * aY * bY + 1400 * bY * bY + 1120 * aX * cX - 1400 * bX * cX + 200 * cX * cX + 1120 * aY * cY - 1400 * bY * cY + 200 * cY * cY - 280 * aX * dX + 200 * bX * dX - 280 * aY * dY + 200 * bY * dY + 20 * aX * eX + 20 * aY * eY - 20 * aX * x + 80 * bX * x - 120 * cX * x + 80 * dX * x - 20 * eX * x - 20 * aY * y + 80 * bY * y - 120 * cY * y + 80 * dY * y - 20 * eY * y;
            var t2 = -180 * aX * aX - 180 * aY * aY + 540 * aX * bX - 300 * bX * bX + 540 * aY * bY - 300 * bY * bY - 240 * aX * cX + 150 * bX * cX - 240 * aY * cY + 150 * bY * cY + 30 * aX * dX + 30 * aY * dY + 30 * aX * x - 90 * bX * x + 90 * cX * x - 30 * dX * x + 30 * aY * y - 90 * bY * y + 90 * cY * y - 30 * dY * y;
            var t1 = 45 * aX * aX + 45 * aY * aY - 90 * aX * bX + 25 * bX * bX - 90 * aY * bY + 25 * bY * bY + 20 * aX * cX + 20 * aY * cY - 20 * aX * x + 40 * bX * x - 20 * cX * x - 20 * aY * y + 40 * bY * y - 20 * cY * y;
            var t0 = -5 * aX * aX - 5 * aY * aY + 5 * aX * bX + 5 * aY * bY + 5 * aX * x - 5 * bX * x + 5 * aY * y - 5 * bY * y;

            return t9 == 0 ? (t0, t1, t2, t3, t4, t5, t6, t7, t8, 1d) : (t0 / t9, t1 / t9, t2 / t9, t3 / t9, t4 / t9, t5 / t9, t6 / t9, t7 / t9, t8 / t9, 1d);
        }
        #endregion Bezier Polynomial From Parametric

        #region Conic Section Polynomials
        /// <summary>
        /// Calculates a conic section polynomial that represents the provided orthogonal ellipse.
        /// </summary>
        /// <param name="h">The center X coordinate.</param>
        /// <param name="k">The center Y coordinate.</param>
        /// <param name="r">The radius of the circle.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// https://www.geometrictools.com/Documentation/IntersectionOfEllipses.pdf
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e, double f) CircleConicSectionPolynomial(double h, double k, double r)
        {
            var r2 = r * r;
            return (
                a: r2,
                b: 0d,
                c: r2,
                d: -2d * r2 * h,
                e: -2d * r2 * k,
                f: r2 * h * h + r2 * k * k - r2 * r2);
        }

        /// <summary>
        /// Calculates a conic section polynomial that represents the provided orthogonal ellipse.
        /// </summary>
        /// <param name="h">The center X coordinate.</param>
        /// <param name="k">The center Y coordinate.</param>
        /// <param name="a">The width of the ellipse.</param>
        /// <param name="b">The height of the ellipse.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// https://www.geometrictools.com/Documentation/IntersectionOfEllipses.pdf
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e, double f) OrthogonalEllipseConicSectionPolynomial(double h, double k, double a, double b)
        {
            var coefA = b * b;
            var coefC = a * a;
            return (
                a: coefA,
                b: 0d,
                c: coefC,
                d: -2d * coefA * h,
                e: -2d * coefC * k,
                f: coefA * h * h + coefC * k * k - a * a * b * b);
        }

        /// <summary>
        /// Calculates a conic section polynomial that represents the provided angled ellipse.
        /// </summary>
        /// <param name="h">The center X coordinate.</param>
        /// <param name="k">The center Y coordinate.</param>
        /// <param name="a">The width of the ellipse.</param>
        /// <param name="b">The angle of the ellipse in pi radians.</param>
        /// <param name="angle">The angle.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e, double f) EllipseConicSectionPolynomial(double h, double k, double a, double b, double angle) => EllipseConicSectionPolynomial(h, k, a, b, Cos(angle), Sin(angle));

        /// <summary>
        /// Calculates a conic section polynomial that represents the provided rotated ellipse.
        /// </summary>
        /// <param name="h">The center X coordinate.</param>
        /// <param name="k">The center Y coordinate.</param>
        /// <param name="a">The width of the ellipse.</param>
        /// <param name="b">The height of the ellipse.</param>
        /// <param name="cos">The cosine of the angle of the ellipse.</param>
        /// <param name="sin">The sine of the angle of the ellipse.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://en.wikipedia.org/wiki/Ellipse#General_ellipse
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e, double f) EllipseConicSectionPolynomial(double h, double k, double a, double b, double cos, double sin)
        {
            // Fix imprecise handling of Cos(PI/2) which breaks ellipses at right angles to each other.
            // Partial Fix for Oblique Ellipse, Oblique Ellipse intersection Test Case 2.
            if (sin == 1d || sin == -1d) cos = 0d;

            var coefA = a * a * sin * sin + b * b * cos * cos;
            var coefB = 2d * (b * b - a * a) * sin * cos;
            var coefC = a * a * cos * cos + b * b * sin * sin;
            return (
                a: coefA,
                b: coefB,
                c: coefC,
                d: -2d * coefA * h - coefB * k,
                e: -2d * coefC * k - coefB * h,
                f: coefA * h * h + coefB * h * k + coefC * k * k - a * a * b * b);
        }

        /// <summary>
        /// Calculates a conic section polynomial that represents the provided rotated hyperbola.
        /// </summary>
        /// <param name="h">The center X coordinate.</param>
        /// <param name="k">The center Y coordinate.</param>
        /// <param name="a">The width of the ellipse.</param>
        /// <param name="b">The height of the ellipse.</param>
        /// <param name="cosA">The cosine of the angle of the ellipse.</param>
        /// <param name="sinA">The sine of the angle of the ellipse.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://en.wikipedia.org/wiki/Ellipse#General_ellipse
        /// https://people.richland.edu/james/lecture/m116/conics/conics.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e, double f) HyperbolaConicSectionPolynomial(double h, double k, double a, double b, double cosA, double sinA)
        {
            // Fix imprecise handling of Cos(PI/2) which breaks ellipses at right angles to each other.
            if (sinA == 1d || sinA == -1d) cosA = 0d;

            var coefA = -a * a * sinA * sinA + b * b * cosA * cosA;
            var coefB = 2d * (b * b - a * a) * sinA * cosA;
            var coefC = a * a * cosA * cosA + b * b * sinA * sinA;
            return (
                a: coefA,
                b: coefB,
                c: coefC,
                d: -2d * coefA * h - coefB * k,
                e: -2d * coefC * k - coefB * h,
                f: coefA * h * h + coefB * h * k + coefC * k * k - a * a * b * b);
        }

        /// <summary>
        /// Quadratics the orthogonal ellipse polynomial.
        /// </summary>
        /// <param name="ax">The ax.</param>
        /// <param name="ay">The ay.</param>
        /// <param name="bx">The bx.</param>
        /// <param name="by">The by.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="h">The h.</param>
        /// <param name="k">The k.</param>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e) QuadraticBernsteinBezierOrthogonalEllipsePolynomial(
            double ax, double ay, double bx, double by, double cx, double cy,
            double h, double k, double a, double b)
            => (a: ((b * b) * (ax * ax)) + ((a * a) * (ay * ay)),
                b: 2d * (((b * b) * (ax * bx)) + ((a * a) * (ay * by))),
                c: (b * b * ((2d * ax * cx) + (bx * bx))) + ((a * a) * ((2d * ay * cy) + (by * by))) - (2d * (((b * b) * h * ax) + ((a * a) * k * ay))),
                d: 2d * (((b * b) * bx * (cx - h)) + ((a * a) * by * (cy - k))),
                e: ((b * b) * ((cx * cx) + (k * k))) + ((a * a) * ((cy * cy) + (k * k))) - (2d * (((b * b) * h * cx) + ((a * a) * k * cy))) - ((a * a) * (b * b))
                );

        internal static (double a, double b, double c, double d, double e, double f, double g) EllipseCubicPolynomial(
            double ax, double bx, double cx, double dx,
            double ay, double by, double cy, double dy,
            double h, double k, double a, double b, double cosA, double sinA)
        {
            var oCoefA = (b * b) * (cosA * cosA) + (a * a) * (sinA * sinA);
            var oCoefB = 2d * ((b * b) + (a * a)) * sinA * cosA;
            return (
                a: oCoefA,
                b: oCoefB,
                c: oCoefB + oCoefA,
                d: 2d * oCoefA + 2d * ((b * b) * h) + 2d * ((a * a) * -k),
                e: 2d * ((b * b) * -h) + 2d * ((a * a) * -k) + oCoefA,
                f: 2d * ((b * b) * -h) + 2d * ((a * a) * -k),
                g: (b * b) - 2d * ((b * b) * h) - 2d * ((a * a) * k) + (a * a) + ((h * h) * (b * b)) + ((k * k) * (a * a)) - ((a * a) * (b * b))
                );

            var roots = (
                a: ((ax * ax) * (b * b)) + ((ay * ay) * (a * a)),
                b: 2d * ((ax * bx * (b * b)) + (ay * by * (a * a))),
                c: (2d * ((ax * cx * (b * b)) + (ay * cy * (a * a)))) + ((bx * bx) * (b * b)) + ((by * by) * (a * a)),
                d: 2d * (ax * (b * b) * (dx - h)) + 2d * (ay * (a * a) * (dy - k)) + (2d * (((bx * cx) * (b * b)) + ((by * cy) * (a * a)))),
                e: 2d * (bx * (b * b) * (dx - h)) + 2d * (by * (a * a) * (dy - k)) + ((cx * cx) * (b * b)) + ((cy * cy) * (a * a)),
                f: 2d * (cx * (b * b) * (dx - h)) + 2d * (cy * (a * a) * (dy - k)),
                g: (dx * dx * (b * b)) - 2d * (dy * k * (a * a)) - 2d * (dx * h * (b * b)) + ((dy * dy) * (a * a)) + ((h * h) * (b * b)) + ((k * k) * (a * a)) - ((a * a) * (b * b))
                );

            var line = (
                a: (ax * ax) + (ay * ay),
                b: 2d * ((ax * bx) + (ay * by)),
                c: 2d * ((ax * cx) + (ay * cy)) + (bx * bx) + (by * by),
                d: 2d * (ax * dx) + 2d * (ay * dy) + 2d * ((bx * cx) + (by * cy)),
                e: 2d * (bx * dx) + 2d * (by * dy) + (cx * cx) + (cy * cy),
                f: 2d * (cx * dx) + 2d * (cy * dy),
                g: (dx * dx) - 2d * dy - 2d * dx + (dy * dy)
                );

            var coefA = (b * b) * (cosA * cosA) + (a * a) * (sinA * sinA);
            var coefB = 2d * ((b * b) - (a * a)) * sinA * cosA;
            var coefC = (b * b) * (sinA * sinA) + (a * a) * (cosA * cosA);
            var curve = (
                a: coefA,
                b: coefB,
                c: coefC,
                d: -2d * coefA * h - coefB * k,
                e: -2d * coefC * k - coefB * h,
                f: coefA * (h * h) + coefB * h * k + coefC * (k * k) - (b * b) * (a * a));

            var coefA1 = (b * b) + (a * a);
            var coefB1 = 2d * ((b * b) - (a * a)) * 0d * 1d;
            var coefC1 = (a * a) + (b * b);
            var s = (
                a: coefA1,
                b: coefB1,
                c: coefC1,
                d: -2d * coefA1 * h - coefB1 * k,
                e: -2d * coefC1 * k - coefB1 * h,
                f: coefA1 * (h * h) + coefB1 * h * k + coefC1 * (k * k) - (a * a) * (b * b));
        }

        /// <summary>
        /// Produces a matrix representation of a conic in the form of: $Q(x,y)=Ax^2+Bxy+Cy^2+Dx+Ey+F=0$.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <param name="e">The e.</param>
        /// <param name="f">The f.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// https://en.wikipedia.org/wiki/Matrix_representation_of_conic_sections
        /// </acknowledgment>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (
            double m11, double m12, double m13,
            double m21, double m22, double m23,
            double m31, double m32, double m33)
            ConicToMatrix(double a, double b, double c, double d, double e, double f) => (
                a, b * 0.5d, d * 0.5d,
                b * 0.5d, c, e * 0.5d,
                d * 0.5d, e * 0.5d, f);
        #endregion Conic Section Polynomials

        #region Helpers
        /// <summary>
        /// Calculate the intersection polynomial coefficients of two ellipses.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial BezoutPolynomial(Polynomial a, Polynomial b) => new Polynomial(ConicSectionBezout((a[0], a[1], a[2], a[3], a[4], a[5]), (b[0], b[1], b[2], b[3], b[4], b[5])));

        /// <summary>
        /// Calculate the intersection polynomial coefficients of two ellipses.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Polynomial BezoutPolynomial((double a, double b, double c, double d, double e, double f) a, (double a, double b, double c, double d, double e, double f) b) => new Polynomial(ConicSectionBezout(a, b));

        /// <summary>
        /// Calculate the intersection polynomial coefficients of two ellipses.
        /// </summary>
        /// <param name="a">The first ellipse.</param>
        /// <param name="b">The first ellipse.</param>
        /// <returns>
        /// Returns a <see cref="Polynomial" /> of the ellipse.
        /// </returns>
        /// <acknowledgment>
        /// http://www.kevlindev.com/
        /// This code is based on MgcIntr2DElpElp.cpp written by David Eberly.
        /// His code along with many other excellent examples formerly available
        /// at his site but the latest version now at: https://www.geometrictools.com/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e) ConicSectionBezout(
            (double a, double b, double c, double d, double e, double f) a,
            (double a, double b, double c, double d, double e, double f) b)
        {
            // 1: | a | b | c | d | e | f |
            // 2: | a | b | c | d | e | f |

            var ab = (a.a * b.b) - (b.a * a.b);
            var ac = (a.a * b.c) - (b.a * a.c);
            var ad = (a.a * b.d) - (b.a * a.d);
            var ae = (a.a * b.e) - (b.a * a.e);
            var af = (a.a * b.f) - (b.a * a.f);

            var bc = (a.b * b.c) - (b.b * a.c);
            var be = (a.b * b.e) - (b.b * a.e);
            var bf = (a.b * b.f) - (b.b * a.f);

            var cd = (a.c * b.d) - (b.c * a.d);

            var de = (a.d * b.e) - (b.d * a.e);
            var df = (a.d * b.f) - (b.d * a.f);

            var bfPde = bf + de;
            var beMcd = be - cd;

            return (
                a: (ab * bc) - (ac * ac),
                b: (ab * beMcd) + (ad * bc) - (2d * ac * ae),
                c: (ab * bfPde) + (ad * beMcd) - (ae * ae) - (2d * ac * af),
                d: (ab * df) + (ad * bfPde) - (2d * ae * af),
                e: (ad * df) - (af * af));
        }

        /// <summary>
        /// Calculate the coefficient of the quartic.
        /// The solution to intersecting ellipses are the solutions to f(y), a quartic function where f(y) = z0 + z1 * y + z2 * y^2 + z3 * y^3 + z4 * y^4  = 0
        /// getQuartic generates the coefficients z0 .. z4 given the two ellipses el and el1 in "bivariate" form.
        /// See http://www.math.niu.edu/~rusin/known-math/99/2ellipses
        /// </summary>
        /// <param name="a">The el1.</param>
        /// <param name="b">The el2.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4, T5}"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/handbook-of-collisions-and-interiors/5567955982876672
        /// https://www.khanacademy.org/computer-programming/ellipse-collision-detector/5514890244521984
        /// https://www.khanacademy.org/computer-programming/c/5567955982876672
        /// https://gist.github.com/drawable/92792f59b6ff8869d8b1
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e) GetConicSectionQuartic(
            (double a, double b, double c, double d, double e, double f) a,
            (double a, double b, double c, double d, double e, double f) b)
        {
            // ToDo: This seems to be reversed from Bezout. IT seems like this might work with everything else if reversed. Needs testing.
            return (
                a: (a.f * a.a * b.d * b.d) + (a.a * a.a * b.f * b.f) - (a.d * a.a * b.d * b.f) + (b.a * b.a * a.f * a.f) - (2d * a.a * b.f * b.a * a.f) - (a.d * b.d * b.a * a.f) + (b.a * a.d * a.d * b.f),
                b: (b.e * a.d * a.d * b.a) - (b.f * b.d * a.a * a.b) - (2d * a.a * b.f * b.a * a.e) - (a.f * b.a * b.b * a.d) + (2d * b.d * b.b * a.a * a.f) + (2d * b.e * b.f * a.a * a.a) + (b.d * b.d * a.a * a.e) - (b.e * b.d * a.a * a.d) - (2d * a.a * b.e * b.a * a.f) - (a.f * b.a * b.d * a.b) + (2d * a.f * a.e * b.a * b.a) - (b.f * b.b * a.a * a.d) - (a.e * b.a * b.d * a.d) + (2d * b.f * a.b * b.a * a.d),
                c: (b.e * b.e * a.a * a.a) + (2d * b.c * b.f * a.a * a.a) - (a.e * b.a * b.d * a.b) + (b.f * b.a * a.b * a.b) - (a.e * b.a * b.b * a.d) - (b.f * b.b * a.a * a.b) - (2d * a.a * b.e * b.a * a.e) + (2d * b.d * b.b * a.a * a.e) - (b.c * b.d * a.a * a.d) - (2d * a.a * b.c * b.a * a.f) + (b.b * b.b * a.a * a.f) + (2d * b.e * a.b * b.a * a.d) + (a.e * a.e * b.a * b.a) - (a.c * b.a * b.d * a.d) - (b.e * b.b * a.a * a.d) + (2d * a.f * a.c * b.a * b.a) - (a.f * b.a * b.b * a.b) + (b.c * a.d * a.d * b.a) + (b.d * b.d * a.a * a.c) - (b.e * b.d * a.a * a.b) - (2d * a.a * b.f * b.a * a.c),
                d: (-2d * a.a * b.a * a.c * b.e) + (b.e * b.a * a.b * a.b) + (2d * b.c * a.b * b.a * a.d) - (a.c * b.a * b.b * a.d) + (b.b * b.b * a.a * a.e) - (b.e * b.b * a.a * a.b) - (2d * a.a * b.c * b.a * a.e) - (a.e * b.a * b.b * a.b) - (b.c * b.b * a.a * a.d) + (2d * b.e * b.c * a.a * a.a) + (2d * a.e * a.c * b.a * b.a) - (a.c * b.a * b.d * a.b) + (2d * b.d * b.b * a.a * a.c) - (b.c * b.d * a.a * a.b),
                e: (a.a * a.a * b.c * b.c) - (2d * a.a * b.c * b.a * a.c) + (b.a * b.a * a.c * a.c) - (a.b * a.a * b.b * b.c) - (a.b * b.b * b.a * a.c) + (a.b * a.b * b.a * b.c) + (a.c * a.a * b.b * b.b)
            );
        }
        #endregion Helpers
    }
}
