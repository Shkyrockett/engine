// <copyright file="BobLyonCommon.cs" company="BobLyon" >
//     Copyright © 2016 - 2018 Bob Lyon. All rights reserved.
// </copyright>
// <author id="BobLyon">Bob Lyon</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
//     I am honored any time anybody uses my code for any purpose.
//     Copy freely! All my programs are at
//     https://www.khanacademy.org/profile/BobLyon/programs
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static System.Math;
using static Engine.Maths;

namespace Engine
{
    /// <summary>
    /// The mode enum.
    /// </summary>
    public enum Mode
    {
        /// <summary>
        /// The CORNERS.
        /// </summary>
        CORNERS,

        /// <summary>
        /// The CENTER.
        /// </summary>
        CENTER,
    }

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
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/handbook-of-collisions-and-interiors/5567955982876672
        /// Thanks to Dr.David Goldberg for the convertion to a depressed quartic!
        /// See http://en.wikipedia.org/wiki/Quartic_function
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
                    return ((z1 * z1) - 4d * z2 * z0) >= 0d;
                }

                // sloped lines have one root
                return z1 != 0d;
            }
            var a = z3 / z4;
            var b = z2 / z4;
            var c = z1 / z4;
            var d = z0 / z4;
            var p = (8d * b - 3d * a * a) / 8d;
            var q = (a * a * a - 4d * a * b + 8d * c) / 8d;
            var r = (-3d * a * a * a * a + 256d * d - 64d * c * a + 16d * a * a * b) / 256d;

            //  x⁴ +        p*x² + q*x + r
            // a*x⁴ + b*x³ + c*x² + d*x + e
            // so a=1  b=0  c=p  d=q  e=r
            // That is, we have a depessed quartic.
            var discrim = 256d * r * r * r - 128d * p * p * r * r + 144d * p * q * q * r
                - 27d * q * q * q * q + 16d * p * p * p * p * r - 4d * p * p * p * q * q;
            var P = 8d * p;
            var D = 64d * r - 16d * p * p;

            return discrim < 0d || (discrim > 0d && P < 0 && D < 0d) || (discrim == 0d && (D != 0d || P <= 0d));
        }

        /// <summary>
        /// Does the quartic function described by z have *any* real solutions?
        /// </summary>
        /// <param name="z">The z.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/handbook-of-collisions-and-interiors/5567955982876672
        /// https://www.khanacademy.org/computer-programming/ellipse-collision-detector/5514890244521984
        /// Thanks to Dr. David Goldberg for the convertion to a depressed quartic!
        /// See http://en.wikipedia.org/wiki/Quartic_function
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
                    return ((z.b * z.b) - 4d * z.c * z.a) >= 0d;
                }

                // sloped lines have one root
                return z.b != 0d;
            }
            var a = z.d / z.e;
            var b = z.c / z.e;
            var c = z.b / z.e;
            var d = z.a / z.e;
            var p = (8d * b - 3d * a * a) / 8d;
            var q = (a * a * a - 4d * a * b + 8d * c) / 8d;
            var r = (-3d * a * a * a * a + 256d * d - 64d * c * a + 16d * a * a * b) / 256d;

            //  x⁴ +        p*x² + q*x + r
            // a*x⁴ + b*x³ + c*x² + d*x + e
            // so a=1  b=0  c=p  d=q  e=r
            // That is, we have a depessed quartic.
            var descrim = 256d * r * r * r - 128d * p * p * r * r + 144d * p * q * q * r
                - 27d * q * q * q * q + 16d * p * p * p * p * r - 4d * p * p * p * q * q;
            var P = 8d * p;
            var D = 64d * r - 16d * p * p;

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
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/handbook-of-collisions-and-interiors/5567955982876672
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DoConicsYIntersect(
            double a, double b, double c, double d, double e, double f,
            double a1, double b1, double c1, double d1, double e1, double f1)
        {
            // Normalize the conics by their first coefficient, a.
            // Then get the differnce of the two equations.
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

            var a3 = b * c1 - b1 * c;
            var a2 = b * e1 + d * c1 - b1 * e - d1 * c;
            var aa1 = b * f1 + d * e1 - b1 * f - d1 * e;
            var a0 = d * f1 - d1 * f;

            var A = deltaC * deltaC - a3 * deltaB;
            var B = 2 * deltaC * deltaE - deltaB * a2 - deltaD * a3;
            var C = deltaE * deltaE + 2 * deltaC * deltaF - deltaB * aa1 - deltaD * a2;
            var D = 2 * deltaE * deltaF - deltaD * aa1 - deltaB * a0;
            var E = deltaF * deltaF - deltaD * a0;
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
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/handbook-of-collisions-and-interiors/5567955982876672
        /// https://docs.google.com/file/d/0B7wsEy6bpVePSEt2Ql9hY0hFdjA/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DoConicsIntersect(
            (double a, double b, double c, double d, double e, double f) el,
            (double a, double b, double c, double d, double e, double f) el1)
        {
            // Check for real y intersects, then real x intersects.
            return DoConicsYIntersect(el.a, el.b, el.c, el.d, el.e, el.f, el1.a, el1.b, el1.c, el1.d, el1.e, el1.f) &&
                DoConicsYIntersect(el.c, el.b, el.a, el.e, el.d, el.f, el1.c, el1.b, el1.a, el1.e, el1.d, el1.f);
        }

        /// <summary>
        /// Calculate the coefficient of the quartic.
        /// The solution to intersecting ellipses are the solutions to f(y), a quartic function where f(y) = z0 + z1 * y + z2 * y^2 + z3 * y^3 + z4 * y^4  = 0
        /// getQuartic generates the coefficients z0 .. z4 given the two ellipses el and el1 in "bivariate" form.
        /// See http://www.math.niu.edu/~rusin/known-math/99/2ellipses
        /// </summary>
        /// <param name="el1">The el1.</param>
        /// <param name="el2">The el2.</param>
        /// <param name="epsilon"></param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4, T5}"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/handbook-of-collisions-and-interiors/5567955982876672
        /// https://www.khanacademy.org/computer-programming/ellipse-collision-detector/5514890244521984
        /// https://www.khanacademy.org/computer-programming/c/5567955982876672
        /// https://gist.github.com/drawable/92792f59b6ff8869d8b1
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e)
            GetQuartic(
            (double a, double b, double c, double d, double e, double f) el1,
            (double a, double b, double c, double d, double e, double f) el2, double epsilon = Epsilon)
            => (
                a: el1.f * el1.a * el2.d * el2.d + el1.a * el1.a * el2.f * el2.f - el1.d * el1.a * el2.d * el2.f + el2.a * el2.a * el1.f * el1.f - 2 * el1.a * el2.f * el2.a * el1.f - el1.d * el2.d * el2.a * el1.f + el2.a * el1.d * el1.d * el2.f,
                b: el2.e * el1.d * el1.d * el2.a - el2.f * el2.d * el1.a * el1.b - 2 * el1.a * el2.f * el2.a * el1.e - el1.f * el2.a * el2.b * el1.d + 2 * el2.d * el2.b * el1.a * el1.f + 2 * el2.e * el2.f * el1.a * el1.a + el2.d * el2.d * el1.a * el1.e - el2.e * el2.d * el1.a * el1.d - 2 * el1.a * el2.e * el2.a * el1.f - el1.f * el2.a * el2.d * el1.b + 2 * el1.f * el1.e * el2.a * el2.a - el2.f * el2.b * el1.a * el1.d - el1.e * el2.a * el2.d * el1.d + 2 * el2.f * el1.b * el2.a * el1.d,
                c: el2.e * el2.e * el1.a * el1.a + 2 * el2.c * el2.f * el1.a * el1.a - el1.e * el2.a * el2.d * el1.b + el2.f * el2.a * el1.b * el1.b - el1.e * el2.a * el2.b * el1.d - el2.f * el2.b * el1.a * el1.b - 2 * el1.a * el2.e * el2.a * el1.e + 2 * el2.d * el2.b * el1.a * el1.e - el2.c * el2.d * el1.a * el1.d - 2 * el1.a * el2.c * el2.a * el1.f + el2.b * el2.b * el1.a * el1.f + 2 * el2.e * el1.b * el2.a * el1.d + el1.e * el1.e * el2.a * el2.a - el1.c * el2.a * el2.d * el1.d - el2.e * el2.b * el1.a * el1.d + 2 * el1.f * el1.c * el2.a * el2.a - el1.f * el2.a * el2.b * el1.b + el2.c * el1.d * el1.d * el2.a + el2.d * el2.d * el1.a * el1.c - el2.e * el2.d * el1.a * el1.b - 2 * el1.a * el2.f * el2.a * el1.c,
                d: -2 * el1.a * el2.a * el1.c * el2.e + el2.e * el2.a * el1.b * el1.b + 2 * el2.c * el1.b * el2.a * el1.d - el1.c * el2.a * el2.b * el1.d + el2.b * el2.b * el1.a * el1.e - el2.e * el2.b * el1.a * el1.b - 2 * el1.a * el2.c * el2.a * el1.e - el1.e * el2.a * el2.b * el1.b - el2.c * el2.b * el1.a * el1.d + 2 * el2.e * el2.c * el1.a * el1.a + 2 * el1.e * el1.c * el2.a * el2.a - el1.c * el2.a * el2.d * el1.b + 2 * el2.d * el2.b * el1.a * el1.c - el2.c * el2.d * el1.a * el1.b,
                e: el1.a * el1.a * el2.c * el2.c - 2 * el1.a * el2.c * el2.a * el1.c + el2.a * el2.a * el1.c * el1.c - el1.b * el1.a * el2.b * el2.c - el1.b * el2.b * el2.a * el1.c + el1.b * el1.b * el2.a * el2.c + el1.c * el1.a * el2.b * el2.b
            );

        /// <summary>
        /// Create a general quadratic function for the ellipse a x^2 + b x y + c y^2 + d x + e y + c = 0
        /// </summary>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="rotation">The rotation.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/ellipse-collision-detector/5514890244521984
        /// https://gist.github.com/drawable/92792f59b6ff8869d8b1
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e, double f)
            GetQuadratic(double cx, double cy, double rx, double ry, double rotation)
        {
            var a = cx;
            var b = rx * rx;
            var c = cy;
            var d = ry * ry;
            var A = Cos(-rotation);
            var B = Sin(-rotation);

            return (
                /* x^2   */ a: A * A / b + B * B / d,
                /* x * y */ b: 2 * A * B / d - 2 * A * B / b,
                /* y^2   */ c: A * A / d + B * B / b,
                /* x     */ d: (2 * A * B * c - 2 * a * A * A) / b + (-2 * a * B * B - 2 * A * B * c) / d,
                /* y     */ e: (2 * a * A * B - 2 * B * B * c) / b + (-2 * a * A * B - 2 * A * A * c) / d,
                /* Const */ f: (a * a * A * A - 2 * a * A * B * c + B * B * c * c) / b + (a * a * B * B + 2 * a * A * B * c + A * A * c * c) / d - 1
            );
        }

        /// <summary>
        /// Create a general quadratic function for the ellipse a x^2 + b x y + c y^2 + d x + e y + c = 0
        /// </summary>
        /// <param name="origin">The origin.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="epsilon"></param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/c/5567955982876672
        /// https://gist.github.com/drawable/92792f59b6ff8869d8b1
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e, double f)
            GetQuadratic((double x, double y) origin, double rx, double ry, double rotation, double epsilon = Epsilon)
        {
            var (x, y) = origin;
            var a = x;
            var b = rx * rx;
            var c = y;
            var d = ry * ry;
            var A = Cos(-rotation);
            var B = Sin(-rotation);

            return (
                /* x^2   */ a: A * A / b + B * B / d,
                /* x * y */ b: 2 * A * B / d - 2 * A * B / b,
                /* y^2   */ c: A * A / d + B * B / b,
                /* x     */ d: (2 * A * B * c - 2 * a * A * A) / b + (-2 * a * B * B - 2 * A * B * c) / d,
                /* y     */ e: (2 * a * A * B - 2 * B * B * c) / b + (-2 * a * A * B - 2 * A * A * c) / d,
                /* Const */ f: (a * a * A * A - 2 * a * A * B * c + B * B * c * c) / b + (a * a * B * B + 2 * a * A * B * c + A * A * c * c) / d - 1
            );
        }

        /// <summary>
        /// Express the traditional KA ellipse rotated by rot in terms of a "bivariate" polynomial that sums to zero.
        /// </summary>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="angle">The angle.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/ellipse-collision-detector/5514890244521984
        /// See http://elliotnoma.wordpress.com/2013/04/10/a-closed-form-solution-for-the-intersections-of-two-ellipses
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e, double f)
            BivariateForm(double cx, double cy, double rx, double ry, double angle)
            => BivariateForm(cx, cy, rx, ry, Cos(angle), Sin(angle));

        /// <summary>
        /// Express the traditional KA ellipse rotated by rot in terms of a "bivariate" polynomial that sums to zero.
        /// </summary>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="cosA">The cosA.</param>
        /// <param name="sinA">The sinA.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/ellipse-collision-detector/5514890244521984
        /// See http://elliotnoma.wordpress.com/2013/04/10/a-closed-form-solution-for-the-intersections-of-two-ellipses
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e, double f)
            BivariateForm(double cx, double cy, double rx, double ry, double cosA, double sinA)
        {
            // Start by rotating the ellipse center by the OPPOSITE
            // of the desired angle. That way when the bivariate
            // computation transforms it back, it WILL be at the
            // correct(and original) coordinates.
            var a = cosA * cx + sinA * cy;
            var c = -sinA * cx + cosA * cy;

            // Now let the bivariate computation
            // rotate in the opposite direction.
            // NOTE the OPPOSITE angle
            var nSinA = -sinA;

            var b = rx * rx / 4d;
            var d = ry * ry / 4d;
            return (
                /* x^2 coefficient */ a: (cosA * cosA / b) + (nSinA * nSinA / d),
                /* xy  coefficient */ b: (-2d * cosA * nSinA / b) + (2d * cosA * nSinA / d),
                /* y^2 coefficient */ c: (nSinA * nSinA / b) + (cosA * cosA / d),
                /* x   coefficient */ d: (-2d * a * cosA / b) - (2d * c * nSinA / d),
                /* y   coefficient */ e: (2d * a * nSinA / b) - (2d * c * cosA / d),
                /*        constant */ f: (a * a / b) + (c * c / d) - 1d
                /* So, a*x^2 + b*x*y + c*y^2 + d*x + e*y + f = 0 */
                );
        }

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
        /// <param name="A">The A.</param>
        /// <param name="B">The B.</param>
        /// <returns>The <see cref="T:(double a, double b, double c, double d, double e, double f)"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e, double f)
            BivariateForm2(double x, double y, double width, double height, double A, double B)
        {
            // Start by rotating the ellipse center by the OPPOSITE
            // of the desired angle.  That way when the bivariate
            // computation transforms it back, it WILL be at the
            // correct (and original) coordinates.
            var r = RotatePoint(x, y, A, B);
            var a = r.x;
            var c = r.y;

            // Now let the bivariate computation
            // rotate in the opposite direction.
            B = -B;  /* A = cos(-rot); B = sin(-rot); */
            var b = width * width / 4;
            var d = height * height / 4;
            return (
                a: (A * A / b) + (B * B / d),  /* x©÷ coefficient */
                b: (-2 * A * B / b) + (2 * A * B / d),  /* xy coeff */
                c: (B * B / b) + (A * A / d),  /* y©÷ coeff */
                d: (-2 * a * A / b) - (2 * c * B / d),  /* x coeff */
                e: (2 * a * B / b) - (2 * c * A / d),  /* y coeff */
                f: (a * a / b) + (c * c / d) - 1  /* constant */
                                                  /* So, ax©÷ + bxy + cy©÷ + dx + ey + f = 0 */
            );
        }

        /// <summary>
        /// Convert pairs of X-Y coordinate parameters to an array of Points with x and y properties.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <returns>The <see cref="T:(double x, double y)[]"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/c/5567955982876672
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double x, double y)[] Coords2Points(double[] x1, double[] y1)
        {
            /* Pass any number of X - Y coordinate pairs. */
            var i = 0;
            var j = 0;
            var points = new List<(double x, double y)>();
            for (i = j = 0; i < x1.Length; j++)
            {
                points.Add((x: x1[i], y: y1[i++]));
            }

            return points.ToArray();
        }

        /// <summary>
        /// Convert pairs of X-Y coordinate parameters to an array of Points with x and y properties.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The <see cref="T:(double x, double y)[]"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/c/5567955982876672
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double x, double y)[] Coords2Points(params double[] arguments)
        {
            /* Pass any number of X - Y coordinate pairs. */
            var i = 0;
            var j = 0;
            var points = new List<(double x, double y)>();
            for (i = j = 0; i < arguments.Length; j++)
            {
                points.Add((x: arguments[i++], y: arguments[i++]));
            }

            return points.ToArray();
        }

        /// <summary>
        /// Return a "correction" angle that converts a
        /// subtended angle to a parametric angle for an
        /// ellipse with radii a and b.See
        /// http://mathworld.wolfram.com/Ellipse-LineIntersection.html
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="subtended">The subtended.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/c/5567955982876672
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Subtended2parametric(double a, double b, double subtended)
        {
            if (a == b)
            {
                return 0;  /* circle needs no correction */
            }
            var rx = Cos(subtended);  /* ray from the origin */
            var ry = Sin(subtended);
            var e = a * b / Sqrt(a * a * ry * ry + b * b * rx * rx);
            var ex = e * rx;  /* where ray intersects ellipse */
            var ey = e * ry;
            var parametric = Atan2(a * ey, b * ex);
            subtended = Atan2(ry, rx);  /* Normailzed! */
            return parametric - subtended;
        }

        /// <summary>
        /// The rotate vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="origin">The origin.</param>
        /// <param name="epsilon"></param>
        /// <returns>The <see cref="T:(double x, double y)"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double x, double y) RotateVector((double x, double y) vector, double angle, (double x, double y) origin, double epsilon = Epsilon)
            => RotateVector(vector, (Cos(angle), Sin(angle)), origin);

        /// <summary>
        /// The rotate vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="origin">The origin.</param>
        /// <param name="epsilon"></param>
        /// <returns>The <see cref="T:(double x, double y)"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double x, double y) RotateVector((double x, double y) vector, (double cos, double sin) angle, (double x, double y) origin, double epsilon = Epsilon)
        {
            var deltaX = vector.x - origin.x;
            var deltaY = vector.y - origin.y;
            (var angleCos, var angleSin) = angle;
            return (origin.x + (deltaX * angleCos - deltaY * angleSin),
                    origin.y + (deltaX * angleSin + deltaY * angleCos));
        }

        /// <summary>
        /// Rotate the point (x, y) by angle theta around the origin.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="theta">The theta.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/c/5567955982876672
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double x, double y) RotatePoint(double x, double y, double theta)
            => RotatePoint(x, y, Cos(theta), Sin(theta));

        /// <summary>
        /// Rotate the point (x, y) by angle theta around the origin.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="cosine">The cosine.</param>
        /// <param name="sine">The sine.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/c/5567955982876672
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double x, double y) RotatePoint(double x, double y, double cosine, double sine)
            => (x: (cosine * x) + (sine * y), y: (-sine * x) + (cosine * y));

        /// <summary>
        /// The dist.
        /// </summary>
        /// <param name="ax">The ax.</param>
        /// <param name="ay">The ay.</param>
        /// <param name="bx">The bx.</param>
        /// <param name="by">The by.</param>
        /// <returns>The <see cref="double"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Dist(double ax, double ay, double bx, double by)
        {
            var dx = ax - bx;
            var dy = ay - by;
            return Sqrt(dx * dx + dy * dy);
        }

        /// <summary>
        /// The constrain.
        /// </summary>
        /// <param name="aNumber">The aNumber.</param>
        /// <param name="aMin">The aMin.</param>
        /// <param name="aMax">The aMax.</param>
        /// <returns>The <see cref="double"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Constrain(double aNumber, double aMin, double aMax)
            => aNumber > aMax ? aMax : aNumber < aMin ? aMin : aNumber;

        /// <summary>
        /// Return true iff angle c is between angles
        /// a and b, inclusive. b always follows a in
        /// the positive rotational direction. Operations
        /// against an entire circle cannot be defined.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/c/5567955982876672
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAngleBetween(double c, double a, double b)
        {
            /* Make sure that a is in the range [0 .. tau). */
            for (a %= Tau; a < 0; a += Tau) { }
            /* Make sure that both b and c are not less than a. */
            for (b %= Tau; b < a; b += Tau) { }
            for (c %= Tau; c < a; c += Tau) { }
            return c <= b;
        }

        /// <summary>
        /// Return true iff c is between a and b.  Normalize all parameters wrt c, then ask if a and b are on opposite sides of zero.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/c/5567955982876672
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBetween(double c, double a, double b)
            => (a - c) * (b - c) <= 0;

        /// <summary>
        /// Return true iff range [a, b] overlaps [c, d].
        /// </summary>
        /// <param name="a">The a.</param>
        /// <param name="b">The b.</param>
        /// <param name="c">The c.</param>
        /// <param name="d">The d.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/c/5567955982876672
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Overlap(double a, double b, double c, double d)
            => IsBetween((c < d) ? c : d, a, b)
            || IsBetween((a < b) ? a : b, c, d);

        /// <summary>
        /// Compute and return the centroid of the polygon.  See
        /// http://wikipedia.org/wiki/Centroid
        /// </summary>
        /// <param name="poly">The poly.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/c/5567955982876672
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double x, double y) Centroid((double x, double y)[] poly)
        {
            var a = 0d;
            var area = 0d;
            var cx = 0d;
            var cy = 0d;
            for (int i = poly.Length - 1, j = 0; j < poly.Length; i = j, j++)
            {
                a = (poly[i].x * poly[j].y) - (poly[j].x * poly[i].y);
                cx += (poly[i].x + poly[j].x) * a;
                cy += (poly[i].y + poly[j].y) * a;
                area += a;
            }
            area *= 3;
            return (x: cx / area, y: cy / area);
        }

        /// <summary>
        /// Compute the incenter of triangle a-b-c.
        /// </summary>
        /// <param name="ax">The ax.</param>
        /// <param name="ay">The ay.</param>
        /// <param name="bx">The bx.</param>
        /// <param name="by">The by.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/c/5567955982876672
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double x, double y) TriangleCenter(double ax, double ay, double bx, double by, double cx, double cy)
        {
            var A = Dist(bx, by, cx, cy);
            var B = Dist(ax, ay, cx, cy);
            var C = Dist(bx, by, ax, ay);
            var P = A + B + C;
            return (
                x: (A * ax + B * bx + C * cx) / P,
                y: (A * ay + B * by + C * cy) / P
            );
        }

        /// <summary>
        /// Return true iff the polygon is convex.  It is
        /// iff all z axis cross products between adjacent
        /// sides have the same sign.
        /// </summary>
        /// <param name="poly">The poly.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/c/5567955982876672
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsConvex((double x, double y)[] poly)
        {
            var first = false;
            var prev = poly.Length - 1;
            var pprev = prev - 1;
            for (var i = 0; i < poly.Length; pprev = prev, prev = i, i++)
            {
                var dx1 = poly[prev].x - poly[pprev].x;
                var dy1 = poly[prev].y - poly[pprev].y;
                var dx2 = poly[i].x - poly[prev].x;
                var dy2 = poly[i].y - poly[prev].y;
                var zCross = dx1 * dy2 - dy1 * dx2;
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
        /// The rectangle.mode affects rects's origin.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="w">The w.</param>
        /// <param name="h">The h.</param>
        /// <param name="theta">The theta.</param>
        /// <param name="rectangleMode"></param>
        /// <returns>The <see cref="T:(double x, double y)[]"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double x, double y)[] Rect2Points(double x, double y, double w, double h, double theta, Mode rectangleMode)
        {
            var p = new (double x, double y)[] { };
            if (rectangleMode == Mode.CORNERS)
            {
                w -= x;
                h -= y;
            }
            if (theta != 0)
            {
                var cosine = Cos(-theta);  /* Compute once... */
                var sine = Sin(-theta);  /* ... use often. */
                if (rectangleMode == Mode.CENTER)
                {
                    /* Rotate four corners around the center, (x, y) */
                    w /= 2;
                    h /= 2;
                    p = new (double x, double y)[] {
                        RotatePoint(-w, -h, cosine, sine),
                        RotatePoint(+w, -h, cosine, sine),
                        RotatePoint(+w, +h, cosine, sine),
                        RotatePoint(-w, +h, cosine, sine) };
                }
                else
                {
                    /* Default CORNER mode. Rotate around corner (x, y) */
                    p = new (double x, double y)[] { (x: 0, y: 0), RotatePoint(w, 0, cosine, sine), RotatePoint(w, h, cosine, sine), RotatePoint(0, h, cosine, sine) };
                }
                /* Renormalize rotated points */
                for (var i = 0; i < p.Length; i++)
                {
                    p[i].x += x;
                    p[i].y += y;
                }
            }
            else if (rectangleMode == Mode.CENTER)
            {
                /* No rotation. (x, y) is the center of the rect. */
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
}
