// <copyright file="BobLyonCommon.cs" company="BobLyon" >
// Copyright © 2016 - 2018 Bob Lyon. All rights reserved.
// </copyright>
// <author id="BobLyon">Bob Lyon</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//     I am honored any time anybody uses my code for any purpose.
//     Copy freely! All my programs are at
//     https://www.khanacademy.org/profile/BobLyon/programs
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Runtime.CompilerServices;
using static Engine.Operations;
using static System.Math;

namespace Engine;

/// <summary>
/// The bob lyon common class.
/// </summary>
public static class BobLyonCommon
{
    /// <summary>
    /// Does the quartic function described by y = z4* x⁴ + z3* x³ + z2* x² + z1* x + z0 have *any* real solutions?
    /// </summary>
    /// <param name="z4">The z4.</param>
    /// <param name="z3">The z3.</param>
    /// <param name="z2">The z2.</param>
    /// <param name="z1">The z1.</param>
    /// <param name="z0">The z0.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    /// <acknowledgment>
    /// https://www.khanacademy.org/computer-programming/handbook-of-collisions-and-interiors/5567955982876672
    /// Thanks to Dr.David Goldberg for the conversion to a depressed quartic!
    /// See http://en.wikipedia.org/wiki/Quartic_function
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool HasAzero(double z4, double z3, double z2, double z1, double z0)
    {
        // First trivial checks for z0 or z4 being zero.
        if (z0 == 0d)
        {
            // zero is a root!
            return true;
        }
        if (z4 == 0d)
        {
            if (z3 != 0d)
            {
                // cubics always have roots
                return true;
            }
            if (z2 != 0d)
            {
                // quadratic
                return ((z1 * z1) - (4d * z2 * z0)) >= 0d;
            }

            // sloped lines have one root
            return z1 != 0d;
        }
        var a = z3 / z4;
        var b = z2 / z4;
        var c = z1 / z4;
        var d = z0 / z4;
        var p = ((8d * b) - (3d * a * a)) / 8d;
        var q = ((a * a * a) - (4d * a * b) + (8d * c)) / 8d;
        var r = ((-3d * a * a * a * a) + (256d * d) - (64d * c * a) + (16d * a * a * b)) / 256d;

        //  x⁴ +        p*x² + q*x + r
        // a*x⁴ + b*x³ + c*x² + d*x + e
        // so a=1  b=0  c=p  d=q  e=r
        // That is, we have a depressed quartic.
        var discrim = (256d * r * r * r) - (128d * p * p * r * r) + (144d * p * q * q * r)
            - (27d * q * q * q * q) + (16d * p * p * p * p * r) - (4d * p * p * p * q * q);
        var P = 8d * p;
        var D = (64d * r) - (16d * p * p);

        return discrim < 0d || (discrim > 0d && P < 0 && D < 0d) || (discrim == 0d && (D != 0d || P <= 0d));
    }

    /// <summary>
    /// Does the quartic function described by z have *any* real solutions?
    /// </summary>
    /// <param name="z">The z.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    /// <acknowledgment>
    /// https://www.khanacademy.org/computer-programming/handbook-of-collisions-and-interiors/5567955982876672
    /// https://www.khanacademy.org/computer-programming/ellipse-collision-detector/5514890244521984
    /// Thanks to Dr. David Goldberg for the conversion to a depressed quartic!
    /// See http://en.wikipedia.org/wiki/Quartic_function
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool HasAzero((double a, double b, double c, double d, double e) z)
    {
        // First trivial checks for z0 or z4 being zero
        if (z.a == 0d)
        {
            // zero is a root!
            return true;
        }
        if (z.e == 0d)
        {
            if (z.d != 0d)
            {
                // cubics always have roots
                return true;
            }
            if (z.c != 0d)
            {
                // quadratic
                return ((z.b * z.b) - (4d * z.c * z.a)) >= 0d;
            }

            // sloped lines have one root
            return z.b != 0d;
        }
        var a = z.d / z.e;
        var b = z.c / z.e;
        var c = z.b / z.e;
        var d = z.a / z.e;
        var p = ((8d * b) - (3d * a * a)) / 8d;
        var q = ((a * a * a) - (4d * a * b) + (8d * c)) / 8d;
        var r = ((-3d * a * a * a * a) + (256d * d) - (64d * c * a) + (16d * a * a * b)) / 256d;

        //  x⁴ +        p*x² + q*x + r
        // a*x⁴ + b*x³ + c*x² + d*x + e
        // so a=1  b=0  c=p  d=q  e=r
        // That is, we have a depressed quartic.
        var descrim = (256d * r * r * r) - (128d * p * p * r * r) + (144d * p * q * q * r)
            - (27d * q * q * q * q) + (16d * p * p * p * p * r) - (4d * p * p * p * q * q);
        var P = 8d * p;
        var D = (64d * r) - (16d * p * p);

        return descrim < 0d || (descrim > 0d && P < 0d && D < 0d) || (descrim == 0d && (D != 0d || P <= 0d));
    }

    /// <summary>
    /// Is the Y coordinate(s) of the intersection of two conic
    /// sections real? They are in their bivariate form,
    /// ax²  + bxy  + cx²  + dx  + ey  + f = 0
    /// For now, a and a1 cannot be zero.
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    /// <param name="c">The c.</param>
    /// <param name="d">The d.</param>
    /// <param name="e">The e.</param>
    /// <param name="f">The f.</param>
    /// <param name="a1">The a1.</param>
    /// <param name="b1">The b1.</param>
    /// <param name="c1">The c1.</param>
    /// <param name="d1">The d1.</param>
    /// <param name="e1">The e1.</param>
    /// <param name="f1">The f1.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    /// <acknowledgment>
    /// https://www.khanacademy.org/computer-programming/handbook-of-collisions-and-interiors/5567955982876672
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool DoConicsYIntersect(
        double a, double b, double c, double d, double e, double f,
        double a1, double b1, double c1, double d1, double e1, double f1)
    {
        // Normalize the conics by their first coefficient, a.
        // Then get the difference of the two equations.
        var deltaB = (b1 /= a1) - (b /= a);
        var deltaC = (c1 /= a1) - (c /= a);
        var deltaD = (d1 /= a1) - (d /= a);
        var deltaE = (e1 /= a1) - (e /= a);
        var deltaF = (f1 /= a1) - (f /= a);

        // Special case for b's and d's being equal
        if (deltaB == 0 && deltaD == 0)
        {
            return HasAzero(0, 0, deltaC, deltaE, deltaF);
        }

        var a3 = (b * c1) - (b1 * c);
        var a2 = (b * e1) + (d * c1) - (b1 * e) - (d1 * c);
        var aa1 = (b * f1) + (d * e1) - (b1 * f) - (d1 * e);
        var a0 = (d * f1) - (d1 * f);

        var A = (deltaC * deltaC) - (a3 * deltaB);
        var B = (2 * deltaC * deltaE) - (deltaB * a2) - (deltaD * a3);
        var C = (deltaE * deltaE) + (2 * deltaC * deltaF) - (deltaB * aa1) - (deltaD * a2);
        var D = (2 * deltaE * deltaF) - (deltaD * aa1) - (deltaB * a0);
        var E = (deltaF * deltaF) - (deltaD * a0);
        return HasAzero(A, B, C, D, E);
    }

    /// <summary>
    /// Do two conics sections el and el1 intersect? Each are in
    /// bivariate form, ax²  + bxy  + cx²  + dx  + ey  + f = 0
    /// Solve by constructing a quartic that must have a real
    /// solution if they intersect.  This checks for real Y
    /// intersects, then flips the parameters around to check
    /// for real X intersects.
    /// </summary>
    /// <param name="el">The el.</param>
    /// <param name="el1">The el1.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    /// <acknowledgment>
    /// https://www.khanacademy.org/computer-programming/handbook-of-collisions-and-interiors/5567955982876672
    /// https://docs.google.com/file/d/0B7wsEy6bpVePSEt2Ql9hY0hFdjA/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool DoConicsIntersect(
        (double a, double b, double c, double d, double e, double f) el,
        (double a, double b, double c, double d, double e, double f) el1)
        // Check for real y intersects, then real x intersects.
        => DoConicsYIntersect(el.a, el.b, el.c, el.d, el.e, el.f, el1.a, el1.b, el1.c, el1.d, el1.e, el1.f) &&
            DoConicsYIntersect(el.c, el.b, el.a, el.e, el.d, el.f, el1.c, el1.b, el1.a, el1.e, el1.d, el1.f);

    /// <summary>
    /// Calculate the coefficient of the quartic.
    /// The solution to intersecting ellipses are the solutions to f(y), a quartic function where f(y) = z0 + z1 * y + z2 * y^2 + z3 * y^3 + z4 * y^4  = 0
    /// getQuartic generates the coefficients z0 .. z4 given the two ellipses el and el1 in "bivariate" form.
    /// See http://www.math.niu.edu/~rusin/known-math/99/2ellipses
    /// </summary>
    /// <param name="el1">The el1.</param>
    /// <param name="el2">The el2.</param>
    /// <param name="epsilon">The epsilon.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3, T4, T5}" />.
    /// </returns>
    /// <acknowledgment>
    /// https://www.khanacademy.org/computer-programming/handbook-of-collisions-and-interiors/5567955982876672
    /// https://www.khanacademy.org/computer-programming/ellipse-collision-detector/5514890244521984
    /// https://www.khanacademy.org/computer-programming/c/5567955982876672
    /// https://gist.github.com/drawable/92792f59b6ff8869d8b1
    /// http://jsfiddle.net/blyon/78kcn39s/
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double a, double b, double c, double d, double e) GetQuartic(
        (double a, double b, double c, double d, double e, double f) el1,
        (double a, double b, double c, double d, double e, double f) el2,
        double epsilon = double.Epsilon)
    {
        _ = epsilon;
        return (
            a: (el1.f * el1.a * el2.d * el2.d) + (el1.a * el1.a * el2.f * el2.f) - (el1.d * el1.a * el2.d * el2.f) + (el2.a * el2.a * el1.f * el1.f) - (2d * el1.a * el2.f * el2.a * el1.f) - (el1.d * el2.d * el2.a * el1.f) + (el2.a * el1.d * el1.d * el2.f),
            b: (el2.e * el1.d * el1.d * el2.a) - (el2.f * el2.d * el1.a * el1.b) - (2d * el1.a * el2.f * el2.a * el1.e) - (el1.f * el2.a * el2.b * el1.d) + (2d * el2.d * el2.b * el1.a * el1.f) + (2d * el2.e * el2.f * el1.a * el1.a) + (el2.d * el2.d * el1.a * el1.e) - (el2.e * el2.d * el1.a * el1.d) - (2d * el1.a * el2.e * el2.a * el1.f) - (el1.f * el2.a * el2.d * el1.b) + (2d * el1.f * el1.e * el2.a * el2.a) - (el2.f * el2.b * el1.a * el1.d) - (el1.e * el2.a * el2.d * el1.d) + (2d * el2.f * el1.b * el2.a * el1.d),
            c: (el2.e * el2.e * el1.a * el1.a) + (2d * el2.c * el2.f * el1.a * el1.a) - (el1.e * el2.a * el2.d * el1.b) + (el2.f * el2.a * el1.b * el1.b) - (el1.e * el2.a * el2.b * el1.d) - (el2.f * el2.b * el1.a * el1.b) - (2d * el1.a * el2.e * el2.a * el1.e) + (2d * el2.d * el2.b * el1.a * el1.e) - (el2.c * el2.d * el1.a * el1.d) - (2d * el1.a * el2.c * el2.a * el1.f) + (el2.b * el2.b * el1.a * el1.f) + (2d * el2.e * el1.b * el2.a * el1.d) + (el1.e * el1.e * el2.a * el2.a) - (el1.c * el2.a * el2.d * el1.d) - (el2.e * el2.b * el1.a * el1.d) + (2d * el1.f * el1.c * el2.a * el2.a) - (el1.f * el2.a * el2.b * el1.b) + (el2.c * el1.d * el1.d * el2.a) + (el2.d * el2.d * el1.a * el1.c) - (el2.e * el2.d * el1.a * el1.b) - (2d * el1.a * el2.f * el2.a * el1.c),
            d: (-2d * el1.a * el2.a * el1.c * el2.e) + (el2.e * el2.a * el1.b * el1.b) + (2d * el2.c * el1.b * el2.a * el1.d) - (el1.c * el2.a * el2.b * el1.d) + (el2.b * el2.b * el1.a * el1.e) - (el2.e * el2.b * el1.a * el1.b) - (2d * el1.a * el2.c * el2.a * el1.e) - (el1.e * el2.a * el2.b * el1.b) - (el2.c * el2.b * el1.a * el1.d) + (2d * el2.e * el2.c * el1.a * el1.a) + (2d * el1.e * el1.c * el2.a * el2.a) - (el1.c * el2.a * el2.d * el1.b) + (2d * el2.d * el2.b * el1.a * el1.c) - (el2.c * el2.d * el1.a * el1.b),
            e: (el1.a * el1.a * el2.c * el2.c) - (2d * el1.a * el2.c * el2.a * el1.c) + (el2.a * el2.a * el1.c * el1.c) - (el1.b * el1.a * el2.b * el2.c) - (el1.b * el2.b * el2.a * el1.c) + (el1.b * el1.b * el2.a * el2.c) + (el1.c * el1.a * el2.b * el2.b)
        );
    }

    /// <summary>
    /// Create a general quadratic function for the ellipse a x^2 + b x y + c y^2 + d x + e y + c = 0
    /// </summary>
    /// <param name="cx">The cx.</param>
    /// <param name="cy">The cy.</param>
    /// <param name="rx">The rx.</param>
    /// <param name="ry">The ry.</param>
    /// <param name="rotation">The rotation.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}" />.
    /// </returns>
    /// <acknowledgment>
    /// https://www.khanacademy.org/computer-programming/ellipse-collision-detector/5514890244521984
    /// https://gist.github.com/drawable/92792f59b6ff8869d8b1
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double a, double b, double c, double d, double e, double f) GetQuadratic(
        double cx, double cy, double rx, double ry, double rotation)
    {
        var rx2 = rx * rx;
        var ry2 = ry * ry;
        var cosT = Cos(-rotation);
        var sinT = Sin(-rotation);

        return (
            /* x^2   */ a: (cosT * cosT / rx2) + (sinT * sinT / ry2),
            /* x * y */ b: (2d * cosT * sinT / ry2) - (2d * cosT * sinT / rx2),
            /* y^2   */ c: (cosT * cosT / ry2) + (sinT * sinT / rx2),
            /* x     */ d: (((2d * cosT * sinT * cy) - (2d * cx * cosT * cosT)) / rx2) + (((-2d * cx * sinT * sinT) - (2d * cosT * sinT * cy)) / ry2),
            /* y     */ e: (((2d * cx * cosT * sinT) - (2d * sinT * sinT * cy)) / rx2) + (((-2d * cx * cosT * sinT) - (2d * cosT * cosT * cy)) / ry2),
            /* Const */ f: (((cx * cx * cosT * cosT) - (2d * cx * cosT * sinT * cy) + (sinT * sinT * cy * cy)) / rx2) + (((cx * cx * sinT * sinT) + (2d * cx * cosT * sinT * cy) + (cosT * cosT * cy * cy)) / ry2) - 1d
        );
    }

    /// <summary>
    /// Create a general quadratic function for the ellipse a x^2 + b x y + c y^2 + d x + e y + c = 0
    /// </summary>
    /// <param name="origin">The origin.</param>
    /// <param name="rx">The rx.</param>
    /// <param name="ry">The ry.</param>
    /// <param name="rotation">The rotation.</param>
    /// <param name="epsilon">The epsilon.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}" />.
    /// </returns>
    /// <acknowledgment>
    /// https://www.khanacademy.org/computer-programming/c/5567955982876672
    /// https://gist.github.com/drawable/92792f59b6ff8869d8b1
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double a, double b, double c, double d, double e, double f) GetQuadratic(
        (double x, double y) origin, double rx, double ry, (double cos, double sin) rotation, double epsilon = double.Epsilon)
    {
        _ = epsilon;
        var (x, y) = origin;

        // a squared.
        var a2 = rx * rx;

        // b squared.
        var b2 = ry * ry;

        var (cosT, sinT) = (-rotation.cos, -rotation.sin);

        return (
            /* x^2   */ a: (cosT * cosT / a2) + (sinT * sinT / b2),
            /* x * y */ b: (2d * cosT * sinT / b2) - (2d * cosT * sinT / a2),
            /* y^2   */ c: (cosT * cosT / b2) + (sinT * sinT / a2),

            /* x     */ d: (((2d * cosT * sinT * y) - (2d * cosT * cosT * x)) / a2) + (((-2d * sinT * sinT * x) - (2d * cosT * sinT * y)) / b2),
            /* y     */ e: (((2d * cosT * sinT * x) - (2d * sinT * sinT * y)) / a2) + (((-2d * cosT * sinT * x) - (2d * cosT * cosT * y)) / b2),
            /* Const */ f: (((cosT * cosT * x * x) - (2d * cosT * sinT * x * y) + (sinT * sinT * y * y)) / a2) + (((sinT * sinT * x * x) + (2d * cosT * sinT * x * y) + (cosT * cosT * y * y)) / b2) - 1d
        );
    }

    /// <summary>
    /// Create a general quadratic function for the ellipse a x^2 + b x y + c y^2 + d x + e y + c = 0
    /// </summary>
    /// <param name="origin">The origin.</param>
    /// <param name="rx">The rx.</param>
    /// <param name="ry">The ry.</param>
    /// <param name="rotation">The rotation.</param>
    /// <param name="epsilon">The epsilon.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}" />.
    /// </returns>
    /// <acknowledgment>
    /// https://www.khanacademy.org/computer-programming/c/5567955982876672
    /// https://gist.github.com/drawable/92792f59b6ff8869d8b1
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double a, double b, double c, double d, double e, double f) GetQuadratic(
        (double x, double y) origin, double rx, double ry, double rotation, double epsilon = double.Epsilon)
        => GetQuadratic(origin, rx, ry, (Cos(rotation), Sin(rotation)), epsilon);

    /// <summary>
    /// Express the traditional KA ellipse rotated by rot in terms of a "bivariate" polynomial that sums to zero.
    /// </summary>
    /// <param name="cx">The cx.</param>
    /// <param name="cy">The cy.</param>
    /// <param name="rx">The rx.</param>
    /// <param name="ry">The ry.</param>
    /// <param name="angle">The angle.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}" />.
    /// </returns>
    /// <acknowledgment>
    /// https://www.khanacademy.org/computer-programming/ellipse-collision-detector/5514890244521984
    /// See http://elliotnoma.wordpress.com/2013/04/10/a-closed-form-solution-for-the-intersections-of-two-ellipses
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double a, double b, double c, double d, double e, double f) BivariateForm(double cx, double cy, double rx, double ry, double angle)
        => BivariateForm(cx, cy, rx, ry, Cos(angle), Sin(angle));

    /// <summary>
    /// Express the traditional KA ellipse rotated by rot in terms of a "bivariate" polynomial that sums to zero.
    /// </summary>
    /// <param name="origin">The origin.</param>
    /// <param name="rx">The rx.</param>
    /// <param name="ry">The ry.</param>
    /// <param name="angle">The angle.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}" />.
    /// </returns>
    /// <acknowledgment>
    /// https://www.khanacademy.org/computer-programming/ellipse-collision-detector/5514890244521984
    /// See http://elliotnoma.wordpress.com/2013/04/10/a-closed-form-solution-for-the-intersections-of-two-ellipses
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double a, double b, double c, double d, double e, double f) BivariateForm((double x, double y) origin, double rx, double ry, double angle)
        => BivariateForm(origin, rx, ry, (Cos(angle), Sin(angle)));

    /// <summary>
    /// Express the traditional KA ellipse, rotated by an angle
    /// whose cosine and sine are A and B, in terms of a "bivariate"
    /// polynomial that sums to zero.  See
    /// http://elliotnoma.wordpress.com/2013/04/10/a-closed-form-solution-for-the-intersections-of-two-ellipses
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="cos">The A.</param>
    /// <param name="sin">The B.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}" />.
    /// </returns>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double a, double b, double c, double d, double e, double f) BivariateForm(double x, double y, double width, double height, double cos, double sin)
    {
        // Start by rotating the ellipse center by the OPPOSITE
        // of the desired angle.  That way when the bivariate
        // computation transforms it back, it WILL be at the
        // correct (and original) coordinates.
        (var h, var k) = RotatePoint2D(x, y, cos, sin);

        // Now let the bivariate computation
        // rotate in the opposite direction.
        sin = -sin;  /* A = cos(-rot); B = sin(-rot); */
        var b = width * width / 4d;
        var d = height * height / 4d;
        return (
            /* x² coefficient */ a: (cos * cos / b) + (sin * sin / d),
            /* xy coefficient */ b: (-2 * cos * sin / b) + (2 * cos * sin / d),
            /* y² coefficient */ c: (sin * sin / b) + (cos * cos / d),
            /* x coefficient */ d: (-2 * h * cos / b) - (2 * k * sin / d),
            /* y coefficient */ e: (2 * h * sin / b) - (2 * k * cos / d),
            /* constant */ f: (h * h / b) + (k * k / d) - 1
        /* So, ax² + bxy + cy² + dx + ey + f = 0 */
        );
    }

    /// <summary>
    /// Express the traditional KA ellipse, rotated by an angle
    /// whose cosine and sine are A and B, in terms of a "bivariate"
    /// polynomial that sums to zero.  See
    /// http://elliotnoma.wordpress.com/2013/04/10/a-closed-form-solution-for-the-intersections-of-two-ellipses
    /// </summary>
    /// <param name="origin">The origin.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="rotation">The rotation.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}" />.
    /// </returns>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double a, double b, double c, double d, double e, double f) BivariateForm((double x, double y) origin, double width, double height, (double cos, double sin) rotation)
    {
        var (x, y) = origin;
        var (cos, sin) = (rotation.cos, rotation.sin);

        // Start by rotating the ellipse center by the OPPOSITE
        // of the desired angle.  That way when the bivariate
        // computation transforms it back, it WILL be at the
        // correct (and original) coordinates.
        (var h, var k) = RotatePoint2D(x, y, cos, sin);

        // Now let the bivariate computation
        // rotate in the opposite direction.
        sin = -sin;  /* A = cos(-rot); B = sin(-rot); */
        var b = width * width / 4d;
        var d = height * height / 4d;
        return (
            /* x² coefficient */ a: (cos * cos / b) + (sin * sin / d),
            /* xy coefficient */ b: (-2 * cos * sin / b) + (2 * cos * sin / d),
            /* y² coefficient */ c: (sin * sin / b) + (cos * cos / d),
            /* x coefficient */ d: (-2 * h * cos / b) - (2 * k * sin / d),
            /* y coefficient */ e: (2 * h * sin / b) - (2 * k * cos / d),
            /* constant */ f: (h * h / b) + (k * k / d) - 1
        /* So, ax² + bxy + cy² + dx + ey + f = 0 */
        );
    }

    /// <summary>
    /// Express the traditional KA ellipse rotated by rot in terms of a "bivariate" polynomial that sums to zero.
    /// </summary>
    /// <param name="cx">The cx.</param>
    /// <param name="cy">The cy.</param>
    /// <param name="rx">The rx.</param>
    /// <param name="ry">The ry.</param>
    /// <param name="cosA">The cosA.</param>
    /// <param name="sinA">The sinA.</param>
    /// <returns>
    /// The <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}" />.
    /// </returns>
    /// <acknowledgment>
    /// https://www.khanacademy.org/computer-programming/ellipse-collision-detector/5514890244521984
    /// See http://elliotnoma.wordpress.com/2013/04/10/a-closed-form-solution-for-the-intersections-of-two-ellipses
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double a, double b, double c, double d, double e, double f) BivariateForm2(double cx, double cy, double rx, double ry, double cosA, double sinA)
    {
        // Start by rotating the ellipse center by the OPPOSITE
        // of the desired angle. That way when the bivariate
        // computation transforms it back, it WILL be at the
        // correct(and original) coordinates.
        var a = (cosA * cx) + (sinA * cy);
        var c = (-sinA * cx) + (cosA * cy);

        // Now let the bivariate computation
        // rotate in the opposite direction.
        // NOTE: the OPPOSITE angle
        var nSinA = -sinA;

        var b = rx * rx / 4d;
        var d = ry * ry / 4d;
        return (
            /* x^2 */ a: (cosA * cosA / b) + (nSinA * nSinA / d),
            /* xy  */ b: (-2d * cosA * nSinA / b) + (2d * cosA * nSinA / d),
            /* y^2 */ c: (nSinA * nSinA / b) + (cosA * cosA / d),

            /* x   */ d: (-2d * a * cosA / b) - (2d * c * nSinA / d),
            /* y   */ e: (2d * a * nSinA / b) - (2d * c * cosA / d),
            /* const */ f: (a * a / b) + (c * c / d) - 1d
            );
    }

    /// <summary>
    /// Convert pairs of X-Y coordinate parameters to an array of Points with x and y properties.
    /// </summary>
    /// <param name="x1">The x1.</param>
    /// <param name="y1">The y1.</param>
    /// <returns>
    /// The <see cref="Array" />.
    /// </returns>
    /// <acknowledgment>
    /// https://www.khanacademy.org/computer-programming/c/5567955982876672
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double x, double y)[] Coords2Points(double[] x1, double[] y1)
    {
        /* Pass any number of X - Y coordinate pairs. */
        var points = new List<(double x, double y)>();
        if (x1 is null || y1 is null) return [.. points];
        int j;
        for (var i = j = 0; i < x1?.Length; j++)
        {
            points.Add((x: x1[i], y: y1[i++]));
        }

        return [.. points];
    }

    /// <summary>
    /// Convert pairs of X-Y coordinate parameters to an array of Points with x and y properties.
    /// </summary>
    /// <param name="arguments">The arguments.</param>
    /// <returns>
    /// The <see cref="Array" />.
    /// </returns>
    /// <acknowledgment>
    /// https://www.khanacademy.org/computer-programming/c/5567955982876672
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double x, double y)[] Coords2Points(params double[] arguments)
    {
        /* Pass any number of X - Y coordinate pairs. */
        var points = new List<(double x, double y)>();
        int j;
        for (var i = j = 0; i < arguments.Length; j++)
        {
            points.Add((x: arguments[i++], y: arguments[i++]));
        }

        return [.. points];
    }

    /// <summary>
    /// The constrain.
    /// </summary>
    /// <param name="aNumber">The aNumber.</param>
    /// <param name="aMin">The aMin.</param>
    /// <param name="aMax">The aMax.</param>
    /// <returns>
    /// The <see cref="double" />.
    /// </returns>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static double Constrain(double aNumber, double aMin, double aMax)
        => aNumber > aMax ? aMax : aNumber < aMin ? aMin : aNumber;

    /// <summary>
    /// Return true iff range [a, b] overlaps [c, d].
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    /// <param name="c">The c.</param>
    /// <param name="d">The d.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    /// <acknowledgment>
    /// https://www.khanacademy.org/computer-programming/c/5567955982876672
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool Overlap(double a, double b, double c, double d)
        => Between((c < d) ? c : d, a, b)
        || Between((a < b) ? a : b, c, d);

    /// <summary>
    /// Return true iff the polygon is convex.  It is
    /// iff all z axis cross products between adjacent
    /// sides have the same sign.
    /// </summary>
    /// <param name="poly">The poly.</param>
    /// <returns>
    /// The <see cref="bool" />.
    /// </returns>
    /// <acknowledgment>
    /// https://www.khanacademy.org/computer-programming/c/5567955982876672
    /// </acknowledgment>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static bool IsConvex((double x, double y)[] poly)
    {
        var first = false;
        var prev = (poly?.Length).Value - 1;
        var pprev = prev - 1;
        for (var i = 0; i < poly.Length; pprev = prev, prev = i, i++)
        {
            var dx1 = poly[prev].x - poly[pprev].x;
            var dy1 = poly[prev].y - poly[pprev].y;
            var dx2 = poly[i].x - poly[prev].x;
            var dy2 = poly[i].y - poly[prev].y;
            var zCross = (dx1 * dy2) - (dy1 * dx2);
            if (i == 0)
            {
                first = zCross < 0;
            }
            else if ((zCross < 0) != first)
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Compute absolute vertices of the rotated rectangle.
    /// The first four parameters describe the rectangle.
    /// The rectangle.mode affects rectangles's origin.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="w">The w.</param>
    /// <param name="h">The h.</param>
    /// <param name="theta">The theta.</param>
    /// <param name="rectangleMode">The rectangle mode.</param>
    /// <returns>
    /// The <see cref="Array" />.
    /// </returns>
    //[DebuggerStepThrough]
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (double x, double y)[] Rect2Points(double x, double y, double w, double h, double theta, Mode rectangleMode)
    {
        if (rectangleMode == Mode.Corners)
        {
            w -= x;
            h -= y;
        }

        (double x, double y)[] p;
        if (theta != 0)
        {
            var cosine = Cos(-theta);  /* Compute once... */
            var sine = Sin(-theta);  /* ... use often. */
            if (rectangleMode == Mode.Center)
            {
                /* Rotate four corners around the center, (x, y) */
                w /= 2;
                h /= 2;
                p = [
                    RotatePoint2D(-w, -h, cosine, sine),
                    RotatePoint2D(+w, -h, cosine, sine),
                    RotatePoint2D(+w, +h, cosine, sine),
                    RotatePoint2D(-w, +h, cosine, sine) ];
            }
            else
            {
                /* Default CORNER mode. Rotate around corner (x, y) */
                p = [(x: 0, y: 0), RotatePoint2D(w, 0, cosine, sine), RotatePoint2D(w, h, cosine, sine), RotatePoint2D(0, h, cosine, sine)];
            }
            /* Re-normalize rotated points */
            for (var i = 0; i < p.Length; i++)
            {
                p[i].x += x;
                p[i].y += y;
            }
        }
        else if (rectangleMode == Mode.Center)
        {
            /* No rotation. (x, y) is the center of the rectangle. */
            w /= 2;
            h /= 2;
            p = Coords2Points(x - w, y - h, x + w, y - h, x + w, y + h, x - w, y + h);
        }
        else
        {
            /* No rotation. Default CORNER mode. */
            p = Coords2Points(x, y, x + w, y, x + w, y + h, x, y + h);
        }
        return p;
    }
}
