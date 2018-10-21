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
    /// The bob lyon common class.
    /// </summary>
    public static class BobLyonCommon
    {
        /// <summary>
        /// Calculate the coefficient of the quartic.
        /// The solution to intersecting ellipses are the solutions to f(y), a quartic function where f(y) = z0 + z1 * y + z2 * y^2 + z3 * y^3 + z4 * y^4  = 0
        /// getQuartic generates the coefficients z0 .. z4 given the two ellipses el and el1 in "bivariate" form.
        /// See http://www.math.niu.edu/~rusin/known-math/99/2ellipses
        /// </summary>
        /// <param name="el">The el.</param>
        /// <param name="el1">The el1.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4, T5}"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/ellipse-collision-detector/5514890244521984
        /// https://gist.github.com/drawable/92792f59b6ff8869d8b1
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e)
            GetQuartic(
            (double a, double b, double c, double d, double e, double f) el,
            (double a, double b, double c, double d, double e, double f) el1)
            => (
                a: el.f * el.a * el1.d * el1.d + el.a * el.a * el1.f * el1.f - el.d * el.a * el1.d * el1.f + el1.a * el1.a * el.f * el.f - 2 * el.a * el1.f * el1.a * el.f - el.d * el1.d * el1.a * el.f + el1.a * el.d * el.d * el1.f,
                b: el1.e * el.d * el.d * el1.a - el1.f * el1.d * el.a * el.b - 2 * el.a * el1.f * el1.a * el.e - el.f * el1.a * el1.b * el.d + 2 * el1.d * el1.b * el.a * el.f + 2 * el1.e * el1.f * el.a * el.a + el1.d * el1.d * el.a * el.e - el1.e * el1.d * el.a * el.d - 2 * el.a * el1.e * el1.a * el.f - el.f * el1.a * el1.d * el.b + 2 * el.f * el.e * el1.a * el1.a - el1.f * el1.b * el.a * el.d - el.e * el1.a * el1.d * el.d + 2 * el1.f * el.b * el1.a * el.d,
                c: el1.e * el1.e * el.a * el.a + 2 * el1.c * el1.f * el.a * el.a - el.e * el1.a * el1.d * el.b + el1.f * el1.a * el.b * el.b - el.e * el1.a * el1.b * el.d - el1.f * el1.b * el.a * el.b - 2 * el.a * el1.e * el1.a * el.e + 2 * el1.d * el1.b * el.a * el.e - el1.c * el1.d * el.a * el.d - 2 * el.a * el1.c * el1.a * el.f + el1.b * el1.b * el.a * el.f + 2 * el1.e * el.b * el1.a * el.d + el.e * el.e * el1.a * el1.a - el.c * el1.a * el1.d * el.d - el1.e * el1.b * el.a * el.d + 2 * el.f * el.c * el1.a * el1.a - el.f * el1.a * el1.b * el.b + el1.c * el.d * el.d * el1.a + el1.d * el1.d * el.a * el.c - el1.e * el1.d * el.a * el.b - 2 * el.a * el1.f * el1.a * el.c,
                d: -2 * el.a * el1.a * el.c * el1.e + el1.e * el1.a * el.b * el.b + 2 * el1.c * el.b * el1.a * el.d - el.c * el1.a * el1.b * el.d + el1.b * el1.b * el.a * el.e - el1.e * el1.b * el.a * el.b - 2 * el.a * el1.c * el1.a * el.e - el.e * el1.a * el1.b * el.b - el1.c * el1.b * el.a * el.d + 2 * el1.e * el1.c * el.a * el.a + 2 * el.e * el.c * el1.a * el1.a - el.c * el1.a * el1.d * el.b + 2 * el1.d * el1.b * el.a * el.c - el1.c * el1.d * el.a * el.b,
                e: el.a * el.a * el1.c * el1.c - 2 * el.a * el1.c * el1.a * el.c + el1.a * el1.a * el.c * el.c - el.b * el.a * el1.b * el1.c - el.b * el1.b * el1.a * el.c + el.b * el.b * el1.a * el1.c + el.c * el.a * el1.b * el1.b
            );

        /// <summary>
        /// Calculate the coefficient of the quartic.
        /// </summary>
        /// <param name="el1">The el1.</param>
        /// <param name="el2">The el2.</param>
        /// <param name="epsilon"></param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4, T5}"/>.</returns>
        /// <acknowledgment>
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
        /// Does the quartic function described by z have *any* real solutions?
        /// See http://en.wikipedia.org/wiki/Quartic_function
        /// Thanks to Dr. David Goldberg for the convertion to a depressed quartic!
        /// </summary>
        /// <param name="z">The z.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/ellipse-collision-detector/5514890244521984
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAzero((double a, double b, double c, double d, double e) z)
        {
            // First trivial checks for z0 or z4 being zero
            if (z.a == 0)
            {
                // zero is a root!
                return true;
            }
            if (z.e == 0)
            {
                if (z.d != 0)
                {
                    // cubics always have roots
                    return true;
                }
                if (z.c != 0)
                {
                    // quad
                    return ((z.b * z.b) - 4 * z.c * z.a) >= 0;
                }
                // sloped lines have one root
                return z.b != 0;
            }
            var a = z.d / z.e;
            var b = z.c / z.e;
            var c = z.b / z.e;
            var d = z.a / z.e;
            var p = (8 * b - 3 * a * a) / 8d;
            var q = (a * a * a - 4 * a * b + 8 * c) / 8d;
            var r = (-3 * a * a * a * a + 256 * d - 64 * c * a + 16 * a * a * b) / 256d;

            // x ^ 4 + p * x ^ 2 + q * x + r
            // a * x ^ 4 + b * x ^ 3 + c * x ^ 2 + d * x + e
            // so a = 1  b = 0  c = p  d = q  e = r
            // That is, we have a depessed quartic.
            var descrim = 256d * r * r * r - 128d * p * p * r * r + 144d * p * q * q * r - 27d * q * q * q * q + 16d * p * p * p * p * r - 4d * p * p * p * q * q;
            var P = 8d * p;
            var D = 64d * r - 16d * p * p;

            return descrim < 0 || (descrim > 0d && P < 0d && D < 0d) || (descrim == 0d && (D != 0 || P <= 0));
        }

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
        /// See http://elliotnoma.wordpress.com/2013/04/10/a-closed-form-solution-for-the-intersections-of-two-ellipses
        /// </summary>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="rx">The rx.</param>
        /// <param name="ry">The ry.</param>
        /// <param name="angle">The angle.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4, T5, T6}"/>.</returns>
        /// <acknowledgment>
        /// https://www.khanacademy.org/computer-programming/ellipse-collision-detector/5514890244521984
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double a, double b, double c, double d, double e, double f)
            BivariateForm(double cx, double cy, double rx, double ry, double angle)
            => BivariateForm(cx, cy, rx, ry, Cos(angle), Sin(angle));

        /// <summary>
        /// Express the traditional KA ellipse rotated by rot in terms of a "bivariate" polynomial that sums to zero.
        /// See http://elliotnoma.wordpress.com/2013/04/10/a-closed-form-solution-for-the-intersections-of-two-ellipses
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
    }
}
