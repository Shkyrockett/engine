using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Math;
using static Engine.Mathematics;
using static Engine.Measurements;
using static Engine.Operations;

namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class Polynomials
    {
        #region Bézier Polynomial Overloads
        /// <summary>
        /// Coefficients for a Linear Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) BezierPolynomial(double a, double b) => LinearBezierPolynomial(a, b);

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
        public static (double A, double B, double C) BezierPolynomial(double a, double b, double c) => QuadraticBezierPolynomial(a, b, c);

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
        public static (double A, double B, double C, double D) BezierPolynomial(double a, double b, double c, double d) => CubicBezierPolynomial(a, b, c, d);

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
        public static (double A, double B, double C, double D, double E) BezierPolynomial(double a, double b, double c, double d, double e) => QuarticBezierPolynomial(a, b, c, d, e);

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
        public static (double A, double B, double C, double D, double E, double F) BezierPolynomial(double a, double b, double c, double d, double e, double f) => QuinticBezierPolynomial(a, b, c, d, e, f);

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
        public static (double A, double B, double C, double D, double E, double F, double G) BezierPolynomial(double a, double b, double c, double d, double e, double f, double g) => SexticBezierPolynomial(a, b, c, d, e, f, g);

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
        public static (double A, double B, double C, double D, double E, double F, double G, double H) BezierPolynomial(double a, double b, double c, double d, double e, double f, double g, double h) => SepticBezierPolynomial(a, b, c, d, e, f, g, h);

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
        public static (double A, double B, double C, double D, double E, double F, double G, double H, double I) BezierPolynomial(double a, double b, double c, double d, double e, double f, double g, double h, double i) => OcticBezierPolynomial(a, b, c, d, e, f, g, h, i);

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
        public static (double A, double B, double C, double D, double E, double F, double G, double H, double I, double J) BezierPolynomial(double a, double b, double c, double d, double e, double f, double g, double h, double i, double j) => NonicBezierPolynomial(a, b, c, d, e, f, g, h, i, j);

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
        public static (double A, double B, double C, double D, double E, double F, double G, double H, double I, double J, double K) BezierPolynomial(double a, double b, double c, double d, double e, double f, double g, double h, double i, double j, double k) => DecicBezierPolynomial(a, b, c, d, e, f, g, h, i, j, k);
        #endregion Bézier Polynomial Overloads

        #region Bézier Polynomial Stack
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
        public static Polynomial LinearBezierPolynomialStack(double a, double b) => (Polynomial.OneMinusT * a) + (Polynomial.T * b);

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
        public static Polynomial QuadraticBezierPolynomialStack(double a, double b, double c) => (Polynomial.OneMinusT * LinearBezierPolynomialStack(a, b)) + (Polynomial.T * LinearBezierPolynomialStack(b, c));

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
        public static Polynomial CubicBezierPolynomialStack(double a, double b, double c, double d) => (Polynomial.OneMinusT * QuadraticBezierPolynomialStack(a, b, c)) + (Polynomial.T * QuadraticBezierPolynomialStack(b, c, d));

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
        public static Polynomial QuarticBezierPolynomialStack(double a, double b, double c, double d, double e) => (Polynomial.OneMinusT * CubicBezierPolynomialStack(a, b, c, d)) + (Polynomial.T * CubicBezierPolynomialStack(b, c, d, e));

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
        public static Polynomial QuinticBezierPolynomialStack(double a, double b, double c, double d, double e, double f) => (Polynomial.OneMinusT * QuarticBezierPolynomialStack(a, b, c, d, e)) + (Polynomial.T * QuarticBezierPolynomialStack(b, c, d, e, f));

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
        public static Polynomial SexticBezierPolynomialStack(double a, double b, double c, double d, double e, double f, double g) => (Polynomial.OneMinusT * QuinticBezierPolynomialStack(a, b, c, d, e, f)) + (Polynomial.T * QuinticBezierPolynomialStack(b, c, d, e, f, g));

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
        public static Polynomial SepticBezierPolynomialStack(double a, double b, double c, double d, double e, double f, double g, double h) => (Polynomial.OneMinusT * SexticBezierPolynomialStack(a, b, c, d, e, f, g)) + (Polynomial.T * SexticBezierPolynomialStack(b, c, d, e, f, g, h));

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
        public static Polynomial OcticBezierPolynomialStack(double a, double b, double c, double d, double e, double f, double g, double h, double i) => (Polynomial.OneMinusT * SepticBezierPolynomialStack(a, b, c, d, e, f, g, h)) + (Polynomial.T * SepticBezierPolynomialStack(b, c, d, e, f, g, h, i));

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
        public static Polynomial NonicBezierPolynomialStack(double a, double b, double c, double d, double e, double f, double g, double h, double i, double j) => (Polynomial.OneMinusT * OcticBezierPolynomialStack(a, b, c, d, e, f, g, h, i)) + (Polynomial.T * OcticBezierPolynomialStack(b, c, d, e, f, g, h, i, j));

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
        public static Polynomial DecicBezierPolynomialStack(double a, double b, double c, double d, double e, double f, double g, double h, double i, double j, double k) => (Polynomial.OneMinusT * NonicBezierPolynomialStack(a, b, c, d, e, f, g, h, i, j)) + (Polynomial.T * NonicBezierPolynomialStack(b, c, d, e, f, g, h, i, j, k));
        #endregion Bézier Polynomial Stack

        #region Bézier Polynomial
        /// <summary>
        /// Coefficients for a Linear Bézier curve.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://fontforge.github.io/bezier.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double A, double B) LinearBezierPolynomial(double a, double b)
            => (b - a,
                a);

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
        public static (double A, double B, double C) QuadraticBezierPolynomial(double a, double b, double c)
            => (c - (2d * b) + a,
                2d * (b - a),
                a);

        /// <summary>
        /// Coefficients for a Cubic Bézier curve.
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
        public static (double A, double B, double C, double D) CubicBezierPolynomial(double a, double b, double c, double d)
            => (d - (3d * c) + (3d * b) - a,
                (3d * c) - (6d * b) + (3d * a),
                3d * (b - a),
                a);

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
        public static (double A, double B, double C, double D, double E) QuarticBezierPolynomial(double a, double b, double c, double d, double e)
            => (e - (4d * d) + (6d * c) - (4d * b) + a,
                (4d * d) - (12d * c) + (12d * b) - (4d * a),
                (6d * c) - (12d * b) + (6d * a),
                4d * (b - a),
                a);

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
        public static (double A, double B, double C, double D, double E, double F) QuinticBezierPolynomial(double a, double b, double c, double d, double e, double f)
            => (f - (5d * e) + (10d * d) - (10d * c) + (5d * b) - a,
                (5d * e) - (20d * d) + (30d * c) - (20d * b) + (5d * a),
                (10d * d) - (30d * c) + (30d * b) - (10d * a),
                (10d * c) - (20d * b) + (10d * a),
                5d * (b - a),
                a);

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
        public static (double A, double B, double C, double D, double E, double F, double G) SexticBezierPolynomial(double a, double b, double c, double d, double e, double f, double g)
            => (g - (6d * f) + (15d * e) - (20d * d) + (15d * c) - (6d * b) + a,
                    (6d * f) - (30d * e) + (60d * d) - (60d * c) + (30d * b) - (6d * a),
                    (15d * e) - (60d * d) + (90d * c) - (60d * b) + (15d * a),
                    (20d * d) - (60d * c) + (60d * b) - (20d * a),
                    (15d * c) - (30d * b) + (15d * a),
                    (6d * b) - (6d * a),
                    a);

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
        public static (double A, double B, double C, double D, double E, double F, double G, double H) SepticBezierPolynomial(double a, double b, double c, double d, double e, double f, double g, double h)
            => (h - (7d * g) + (21d * f) - (35d * e) + (35d * d) - (21d * c) + (7d * b) - a,
                (7d * g) - (42d * f) + (105d * e) - (140d * d) + (105d * c) - (42d * b) + (7d * a),
                (21d * f) - (105d * e) + (210d * d) - (210d * c) + (105d * b) - (21d * a),
                (35d * e) - (140d * d) + (210d * c) - (140d * b) + (35d * a),
                (35d * d) - (105d * c) + (105d * b) - (35d * a),
                (21d * c) - (42d * b) + (21d * a),
                (7d * b) - (7d * a),
                a);

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
        public static (double A, double B, double C, double D, double E, double F, double G, double H, double I) OcticBezierPolynomial(double a, double b, double c, double d, double e, double f, double g, double h, double i)
            => (i - (8d * h) + (28d * g) - (56d * f) + (70d * e) - (56d * d) + (28d * c) - (8d * b) + a,
                (8d * h) - (56d * g) + (168d * f) - (280d * e) + (280d * d) - (168d * c) + (56d * b) - (8d * a),
                (28d * g) - (168d * f) + (420d * e) - (560d * d) + (420d * c) - (168d * b) + (28d * a),
                (56d * f) - (280d * e) + (560d * d) - (560d * c) + (280d * b) - (56d * a),
                (70d * e) - (280d * d) + (420d * c) - (280d * b) + (70d * a),
                (56d * d) - (168d * c) + (168d * b) - (56d * a),
                (28d * c) - (56d * b) + (28d * a),
                (8d * b) - (8d * a),
                a);

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
        public static (double A, double B, double C, double D, double E, double F, double G, double H, double I, double J) NonicBezierPolynomial(double a, double b, double c, double d, double e, double f, double g, double h, double i, double j)
            => (j - (9d * i) + (36d * h) - (84d * g) + (126d * f) - (126d * e) + (84d * d) - (36d * c) + (9d * b) - a,
                (9d * i) - (72d * h) + (252d * g) - (504d * f) + (630d * e) - (504d * d) + (252d * c) - (72d * b) + (9d * a),
                (36d * h) - (252d * g) + (756d * f) - (1260d * e) + (1260d * d) - (756d * c) + (252 * b) - (36 * a),
                (84d * g) - (504 * f) + (1260d * e) - (1680d * d) + (1260d * c) - (504d * b) + (84d * a),
                (126d * f) - (630d * e) + (1260d * d) - (1260d * c) + (630d * b) - (126d * a),
                (126d * e) - (504d * d) + (756d * c) - (504d * b) + (126d * a),
                (84d * d) - (252d * c) + (252d * b) - (84d * a),
                (36d * c) - (72d * b) + (36d * a),
                (9d * b) - (9d * a),
                a);

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
        public static (double A, double B, double C, double D, double E, double F, double G, double H, double I, double J, double K) DecicBezierPolynomial(double a, double b, double c, double d, double e, double f, double g, double h, double i, double j, double k)
            => (k - (10 * j) + (45 * i) - (120 * h) + (210 * g) - (252 * f) + (210 * e) - (120 * d) + (45 * c) - (10 * b) + a,
                (10 * j) - (90d * i) + (360d * h) - (840d * g) + (1260d * f) - (1260d * e) + (840d * d) - (360d * c) + (90d * b) - (10 * a),
                (45d * i) - (360d * h) + (1260d * g) - (2520d * f) + (3150d * e) - (2520d * d) + (1260d * c) - (360d * b) + (45d * a),
                (120d * h) - (840d * g) + (2520d * f) - (4200d * e) + (4200d * d) - (2520d * c) + (840d * b) - (120d * a),
                (210d * g) - (1260d * f) + (3150d * e) - (4200d * d) + (3150d * c) - (1260d * b) + (210d * a),
                (252d * f) - (1260d * e) + (2520d * d) - (2520d * c) + (1260d * b) - (252d * a),
                (210d * e) - (840d * d) + (1260d * c) - (840d * b) + (210d * a),
                (120d * d) - (360d * c) + (360d * b) - (120d * a),
                (45d * c) - (90d * b) + (45d * a),
                (10d * b) - (10d * a),
                a);
        #endregion Bézier Polynomial

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
        public static (double a, double b, double c, double d, double e, double f) EllipseConicSectionPolynomial(double h, double k, double a, double b, double angle)
            => EllipseConicSectionPolynomial(h, k, a, b, Cos(angle), Sin(angle));

        /// <summary>
        /// Calculates a conic section polynomial that represents the provided rotated ellipse.
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
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e, double f) EllipseConicSectionPolynomial(double h, double k, double a, double b, double cosA, double sinA)
        {
            // Fix imprecise handling of Cos(PI/2) which breaks ellipses at right angles to each other.
            if (sinA == 1d || sinA == -1d) cosA = 0d;

            var coefA = a * a * sinA * sinA + b * b * cosA * cosA;
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
        #endregion Conic Section Polynomials

        #region Helpers
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
        public static Polynomial Bezout(
            (double a, double b, double c, double d, double e, double f) a,
            (double a, double b, double c, double d, double e, double f) b)
        {
            // 1 | a | b | c | d | e | f |
            // 2 | a | b | c | d | e | f |

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

            return new Polynomial(
                /* x⁴ */ (ab * bc) - (ac * ac),
                /* x³ */ (ab * beMcd) + (ad * bc) - (2d * ac * ae),
                /* x² */ (ab * bfPde) + (ad * beMcd) - (ae * ae) - (2d * ac * af),
                /* x¹ */ (ab * df) + (ad * bfPde) - (2d * ae * af),
                /* c  */ (ad * df) - (af * af));
            // (-a2 * d1 * d1 * f2) + (a1 * d2) + (a2 * f1) - (a1 * f2) + (a1 * a2 f1 * f2) - (d2 * f1)
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
        public static (double a, double b, double c, double d, double e) GetEllipseQuartic(
            (double a, double b, double c, double d, double e, double f) a,
            (double a, double b, double c, double d, double e, double f) b)
            => (
                /* x⁴ */ a: (a.f * a.a * b.d * b.d) + (a.a * a.a * b.f * b.f) - (a.d * a.a * b.d * b.f) + (b.a * b.a * a.f * a.f) - (2d * a.a * b.f * b.a * a.f) - (a.d * b.d * b.a * a.f) + (b.a * a.d * a.d * b.f),
                /* x³ */ b: (b.e * a.d * a.d * b.a) - (b.f * b.d * a.a * a.b) - (2d * a.a * b.f * b.a * a.e) - (a.f * b.a * b.b * a.d) + (2d * b.d * b.b * a.a * a.f) + (2d * b.e * b.f * a.a * a.a) + (b.d * b.d * a.a * a.e) - (b.e * b.d * a.a * a.d) - (2d * a.a * b.e * b.a * a.f) - (a.f * b.a * b.d * a.b) + (2d * a.f * a.e * b.a * b.a) - (b.f * b.b * a.a * a.d) - (a.e * b.a * b.d * a.d) + (2d * b.f * a.b * b.a * a.d),
                /* x² */ c: (b.e * b.e * a.a * a.a) + (2d * b.c * b.f * a.a * a.a) - (a.e * b.a * b.d * a.b) + (b.f * b.a * a.b * a.b) - (a.e * b.a * b.b * a.d) - (b.f * b.b * a.a * a.b) - (2d * a.a * b.e * b.a * a.e) + (2d * b.d * b.b * a.a * a.e) - (b.c * b.d * a.a * a.d) - (2d * a.a * b.c * b.a * a.f) + (b.b * b.b * a.a * a.f) + (2d * b.e * a.b * b.a * a.d) + (a.e * a.e * b.a * b.a) - (a.c * b.a * b.d * a.d) - (b.e * b.b * a.a * a.d) + (2d * a.f * a.c * b.a * b.a) - (a.f * b.a * b.b * a.b) + (b.c * a.d * a.d * b.a) + (b.d * b.d * a.a * a.c) - (b.e * b.d * a.a * a.b) - (2d * a.a * b.f * b.a * a.c),
                /* x¹ */ d: (-2d * a.a * b.a * a.c * b.e) + (b.e * b.a * a.b * a.b) + (2d * b.c * a.b * b.a * a.d) - (a.c * b.a * b.b * a.d) + (b.b * b.b * a.a * a.e) - (b.e * b.b * a.a * a.b) - (2d * a.a * b.c * b.a * a.e) - (a.e * b.a * b.b * a.b) - (b.c * b.b * a.a * a.d) + (2d * b.e * b.c * a.a * a.a) + (2d * a.e * a.c * b.a * b.a) - (a.c * b.a * b.d * a.b) + (2d * b.d * b.b * a.a * a.c) - (b.c * b.d * a.a * a.b),
                /* c  */ e: (a.a * a.a * b.c * b.c) - (2d * a.a * b.c * b.a * a.c) + (b.a * b.a * a.c * a.c) - (a.b * a.a * b.b * b.c) - (a.b * b.b * b.a * a.c) + (a.b * a.b * b.a * b.c) + (a.c * a.a * b.b * b.b)
                );
        #endregion Helpers
    }
}
