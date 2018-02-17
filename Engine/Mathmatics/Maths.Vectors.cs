// <copyright file="Maths.Vectors.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2018 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The maths class.
    /// </summary>
    public partial class Maths
    {
        /// <summary>
        /// The linear interpolation method.
        /// </summary>
        /// <param name="u0">The u0.</param>
        /// <param name="u1">The u1.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Lerp(double u0, double u1, double t)
            => u0 + (u1 - u0) * t;

        /// <summary>
        /// The linear interpolation method.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Lerp(
            double x0, double y0,
            double x1, double y1,
            double t)
            => (x0 + (x1 - x0) * t, y0 + (y1 - y0) * t);

        /// <summary>
        /// The linear interpolation method.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="z0">The z0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Lerp(
            double x0, double y0, double z0,
            double x1, double y1, double z1,
            double t)
            => (x0 + (x1 - x0) * t, y0 + (y1 - y0) * t, z0 + (z1 - z0) * t);

        /// <summary>
        /// The linear interpolation method.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="z0">The z0.</param>
        /// <param name="w0">The w0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="w1">The w1.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3, T4}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W) Lerp(
            double x0, double y0, double z0, double w0,
            double x1, double y1, double z1, double w1,
            double t)
            => (x0 + (x1 - x0) * t, y0 + (y1 - y0) * t, z0 + (z1 - z0) * t, w0 + (w1 - w0) * t);

        /// <summary>
        /// The complex product.
        /// </summary>
        /// <param name="x0">The x0.</param>
        /// <param name="y0">The y0.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        /// <acknowledgment>
        /// http://stackoverflow.com/questions/1476497/multiply-two-point-objects
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) ComplexProduct(
            double x0, double y0,
            double x1, double y1)
            => (x0 * x1 - y0 * y1, x0 * y1 + y0 * x1);

        /// <summary>
        /// Cross Product of two points.
        /// </summary>
        /// <param name="x1">First Point X component.</param>
        /// <param name="y1">First Point Y component.</param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <returns>the cross product AB · BC.</returns>
        /// <remarks>Note that AB · BC = |AB| * |BC| * Cos(theta).</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CrossProduct(
            double x1, double y1,
            double x2, double y2)
            => (x1 * y2) - (y1 * x2);

        /// <summary>
        /// The cross product.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="z2">The z2.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double, double, double) CrossProduct(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => (
                    (y1 * z2) - (z1 * y2), // X
                    (z1 * x2) - (x1 * z2), // Y
                    (x1 * y2) - (y1 * x2)  // Z
                );

        /// <summary>
        /// The cross product vector.
        /// The cross product is a vector perpendicular to AB
        /// and BC having length |AB| * |BC| * Sin(theta) and
        /// with direction given by the right-hand rule.
        /// For two vectors in the X-Y plane, the result is a
        /// vector with X and Y components 0 so the Z component
        /// gives the vector's length and direction.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <returns>Return the cross product AB x BC. The <see cref="double"/>.</returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CrossProductVector(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
            => ((x1 - x2) * (y3 - y2) - (y1 - y2) * (x3 - x2));

        /// <summary>
        /// The cross product vector0.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <acknowledgment>
        /// http://www.angusj.com
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CrossProductVector0(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
            => ((x2 - x1) * (y1 - y3) - (x1 - x3) * (y2 - y1));

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="x1">First Point X component.</param>
        /// <param name="y1">First Point Y component.</param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <returns>The Dot Product.</returns>
        /// <remarks>The dot product "·" is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(
            double x1, double y1,
            double x2, double y2)
            => ((x1 * x2) + (y1 * y2));

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="x1">First Point X component.</param>
        /// <param name="y1">First Point Y component.</param>
        /// <param name="z1">First Point Z component.</param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <param name="z2">Second Point Z component.</param>
        /// <returns>The Dot Product.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => ((x1 * x2) + (y1 * y2) + (z1 * z2));

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="tuple">X, Y, Z components in tuple form.</param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <param name="z2">Second Point Z component.</param>
        /// <returns>The Dot Product.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(
            (double X, double Y, double Z) tuple,
            double x2, double y2, double z2)
            => DotProduct(tuple.X, tuple.Y, tuple.Z, x2, y2, z2);

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector.
        /// </summary>
        /// <param name="tuple1">First set of X, Y, Z components in tuple form.</param>
        /// <param name="tuple2">Second set of X, Y, Z components in tuple form.</param>
        /// <returns>The Dot Product.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(
            (double X, double Y, double Z) tuple1,
            (double X, double Y, double Z) tuple2)
            => DotProduct(
                tuple1.X, tuple1.Y, tuple1.Z,
                tuple2.X, tuple2.Y, tuple2.Z
                );

        /// <summary>
        /// The Dot Product of the vector of three points
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <returns>Return the dot product AB · BC.</returns>
        /// <remarks>Note that AB · BC = |AB| * |BC| * Cos(theta).</remarks>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProductVector(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
            => (((x1 - x2) * (x3 - x2)) + ((y1 - y2) * (y3 - y2)));

        /// <summary>
        /// The mixed product.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="z2">The z2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <param name="z3">The z3.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MixedProduct(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double x3, double y3, double z3)
            => DotProduct(CrossProduct(x1, y1, z1, x2, y2, z2), x3, y3, z3);

        /// <summary>
        /// The Magnitude of a two dimensional Vector.
        /// </summary>
        /// <param name="i">The i component of the vector.</param>
        /// <param name="j">The j component of the vector.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Magnitude(double i, double j)
            => Sqrt((i * i) + (j * j));

        /// <summary>
        /// The Magnitude of a three dimensional Vector.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Magnitude(double i, double j, double k)
            => Sqrt((i * i) + (j * j) + (k * k));

        /// <summary>
        /// The Modulus of a Vector.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Modulus(double i, double j)
            => Magnitude(i, j);

        /// <summary>
        /// The Modulus of a Vector.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        /// <returns>The <see cref="double"/>.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Modulus(double i, double j, double k)
            => Magnitude(i, j, k);

        /// <summary>
        /// Get the unit normal.
        /// </summary>
        /// <param name="pt1X">The pt1X.</param>
        /// <param name="pt1Y">The pt1Y.</param>
        /// <param name="pt2X">The pt2X.</param>
        /// <param name="pt2Y">The pt2Y.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        /// <acknowledgment>
        /// http://www.angusj.com
        /// </acknowledgment>
        public static (double, double) GetUnitNormal(double pt1X, double pt1Y, double pt2X, double pt2Y)
        {
            var dx = (pt2X - pt1X);
            var dy = (pt2Y - pt1Y);
            if ((dx == 0d) && (dy == 0d)) return (0d, 0d);

            var f = 1d / Sqrt(dx * dx + dy * dy);
            dx *= f;
            dy *= f;

            return (dy, -dx);
        }

        /// <summary>
        /// The Unitize a Vector.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Unitize(double i, double j)
            => Normalize2D(i, j);

        /// <summary>
        /// Unitize a Vector.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Unitize(double i, double j, double k)
            => Normalize3D(i, j, k);

        /// <summary>
        /// Normalize a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Normalize1D(double i)
            => i / Sqrt(i * i);

        /// <summary>
        /// Normalize a Vector.
        /// </summary>
        /// <param name="i1"></param>
        /// <param name="i2"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Normalize1D(double i1, double i2)
            => i1 / Sqrt(i1 * i2);

        /// <summary>
        /// Normalize a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Normalize2D(
            double i, double j)
            => (i / Sqrt((i * i) + (j * j)),
                j / Sqrt((i * i) + (j * j)));

        /// <summary>
        /// Find the Normal of Two points.
        /// </summary>
        /// <param name="i1">The x component of the first Point.</param>
        /// <param name="j1">The y component of the first Point.</param>
        /// <param name="i2">The x component of the second Point.</param>
        /// <param name="j2">The y component of the second Point.</param>
        /// <returns>The Normal of two Points</returns>
        /// <remarks></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Normalize2D(
            double i1, double j1,
            double i2, double j2)
            => (
                i1 / Sqrt(((i1 * i2) + (j1 * j2))),
                j1 / Sqrt(((i1 * i2) + (j1 * j2)))
                );

        /// <summary>
        /// Normalize a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Normalize3D(
            double i, double j, double k)
            => (i / Sqrt((i * i) + (j * j) + (k * k)),
                j / Sqrt((i * i) + (j * j) + (k * k)),
                k / Sqrt((i * i) + (j * j) + (k * k)));

        /// <summary>
        /// Find the Normal of Two vectors.
        /// </summary>
        /// <param name="i1">The x component of the first Point.</param>
        /// <param name="j1">The y component of the first Point.</param>
        /// <param name="k1">The z component of the second Point.</param>
        /// <param name="i2">The x component of the second Point.</param>
        /// <param name="j2">The y component of the second Point.</param>
        /// <param name="k2">The z component of the second Point.</param>
        /// <returns>The Normal of two Points</returns>
        /// <remarks></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Normalize3D(
            double i1, double j1, double k1,
            double i2, double j2, double k2)
            => (
                i1 / Sqrt(((i1 * i2) + (j1 * j2) + (k1 * k2))),
                j1 / Sqrt(((i1 * i2) + (j1 * j2) + (k1 * k2))),
                k1 / Sqrt(((i1 * i2) + (j1 * j2) + (k1 * k2))));

        /// <summary>
        /// Normalize a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W) Normalize4D(
            double i, double j, double k, double l)
            => (i / Sqrt((i * i) + (j * j) + (k * k) + (l * l)),
                j / Sqrt((i * i) + (j * j) + (k * k) + (l * l)),
                k / Sqrt((i * i) + (j * j) + (k * k) + (l * l)),
                l / Sqrt((i * i) + (j * j) + (k * k) + (l * l)));

        /// <summary>
        /// Find the Normal of Two vectors.
        /// </summary>
        /// <param name="i1">The x component of the first Point.</param>
        /// <param name="j1">The y component of the first Point.</param>
        /// <param name="k1">The z component of the first Point.</param>
        /// <param name="l1"></param>
        /// <param name="i2">The x component of the second Point.</param>
        /// <param name="j2">The y component of the second Point.</param>
        /// <param name="k2">The z component of the second Point.</param>
        /// <param name="l2"></param>
        /// <returns>The Normal of two Points</returns>
        /// <remarks></remarks>
        /// <acknowledgment>
        /// http://www.fundza.com/vectors/normalize/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z, double W) Normalize4D(
            double i1, double j1, double k1, double l1,
            double i2, double j2, double k2, double l2)
            => (
                i1 / Sqrt((i1 * i2) + (j1 * j2) + (k1 * k2) + (l1 * l2)),
                j1 / Sqrt((i1 * i2) + (j1 * j2) + (k1 * k2) + (l1 * l2)),
                k1 / Sqrt((i1 * i2) + (j1 * j2) + (k1 * k2) + (l1 * l2)),
                l1 / Sqrt((i1 * i2) + (j1 * j2) + (k1 * k2) + (l1 * l2)));

        /// <summary>
        /// Find the Clockwise Perpendicular of a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <remarks>To get the perpendicular vector in two dimensions use I = -J, J = I</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) PerpendicularClockwise(double i, double j)
            => (-j, i);

        /// <summary>
        /// Find the Counter Clockwise Perpendicular of a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <remarks>To get the perpendicular vector in two dimensions use I = -J, J = I</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) PerpendicularCounterClockwise(double i, double j)
            => (j, -i);

        /// <summary>
        /// The projection.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="z2">The z2.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Projection(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => (
                x2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2),
                y2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2),
                z2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2)
                );

        /// <summary>
        /// The rejection.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="z2">The z2.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Rejection(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => (
                x1 - x2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2),
                z1 - y2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2),
                z1 - z2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2)
                );

        /// <summary>
        /// The reflection.
        /// </summary>
        /// <param name="i1">The i1.</param>
        /// <param name="j1">The j1.</param>
        /// <param name="k1">The k1.</param>
        /// <param name="i2">The i2.</param>
        /// <param name="j2">The j2.</param>
        /// <param name="k2">The k2.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Reflection(
            double i1, double j1, double k1,
            double i2, double j2, double k2)
        {
            // if v2 has a right angle to vector, return -vector and stop
            if (Math.Abs(Math.Abs(Angle(i1, j1, k1, i2, j2, k2)) - PI / 2) < double.Epsilon)
                return (-i1, -j1, -k1);

            (var x, var y, var z) = Projection(i1, j1, k1, i2, j2, k2);
            return (
                (2 * x - i1) * Magnitude(i1, j1, k1),
                (2 * y - j1) * Magnitude(i1, j1, k1),
                (2 * z - k1) * Magnitude(i1, j1, k1)
                );
        }

        /// <summary>
        /// The rotate x.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="rad">The rad.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) RotateX(double x1, double y1, double z1, double rad)
            => (
                x1,
                (y1 * Cos(rad)) - (z1 * Sin(rad)),
                (y1 * Sin(rad)) + (z1 * Cos(rad))
                );

        /// <summary>
        /// The pitch.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="rad">The rad.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Pitch(double x1, double y1, double z1, double rad)
            => RotateX(x1, y1, z1, rad);

        /// <summary>
        /// The rotate y.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="rad">The rad.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) RotateY(double x1, double y1, double z1, double rad)
            => (
                (z1 * Sin(rad)) + (x1 * Cos(rad)),
                y1,
                (z1 * Cos(rad)) - (x1 * Sin(rad))
                );

        /// <summary>
        /// The yaw.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="rad">The rad.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Yaw(double x1, double y1, double z1, double rad)
            => RotateY(x1, y1, z1, rad);

        /// <summary>
        /// The rotate z.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="rad">The rad.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) RotateZ(double x1, double y1, double z1, double rad)
            => (
                (x1 * Cos(rad)) - (y1 * Sin(rad)),
                (x1 * Sin(rad)) + (y1 * Cos(rad)),
                z1
                );

        /// <summary>
        /// The roll.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="rad">The rad.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Roll(double x1, double y1, double z1, double rad)
            => RotateZ(x1, y1, z1, rad);

        /// <summary>
        /// The rotate x.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="yOff">The yOff.</param>
        /// <param name="zOff">The zOff.</param>
        /// <param name="rad">The rad.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) RotateX(double x1, double y1, double z1, double yOff, double zOff, double rad)
            => (
                x1,
                (y1 * Cos(rad)) - (z1 * Sin(rad)) + (yOff * (1 - Cos(rad)) + zOff * Sin(rad)),
                (y1 * Sin(rad)) + (z1 * Cos(rad)) + (zOff * (1 - Cos(rad)) - yOff * Sin(rad))
                );

        /// <summary>
        /// The rotate y.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="xOff">The xOff.</param>
        /// <param name="zOff">The zOff.</param>
        /// <param name="rad">The rad.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) RotateY(double x1, double y1, double z1, double xOff, double zOff, double rad)
            => (
                (z1 * Sin(rad)) + (x1 * Cos(rad)) + (xOff * (1 - Cos(rad)) - zOff * Sin(rad)),
                y1,
                (z1 * Cos(rad)) - (x1 * Sin(rad)) + (zOff * (1 - Cos(rad)) + xOff * Sin(rad))
                );

        /// <summary>
        /// The rotate z.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="xOff">The xOff.</param>
        /// <param name="yOff">The yOff.</param>
        /// <param name="rad">The rad.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) RotateZ(double x1, double y1, double z1, double xOff, double yOff, double rad)
            => (
                (x1 * Cos(rad)) - (y1 * Sin(rad)) + (xOff * (1 - Cos(rad)) + yOff * Sin(rad)),
                (x1 * Sin(rad)) + (y1 * Cos(rad)) + (yOff * (1 - Cos(rad)) - xOff * Sin(rad)),
                z1
                );

        /// <summary>
        /// The is unit vector.
        /// </summary>
        /// <param name="i1">The i1.</param>
        /// <param name="j1">The j1.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUnitVector(double i1, double j1)
            => Math.Abs(Magnitude(i1, j1) - 1) < Epsilon;

        /// <summary>
        /// The is unit vector.
        /// </summary>
        /// <param name="i1">The i1.</param>
        /// <param name="j1">The j1.</param>
        /// <param name="k1">The k1.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUnitVector(double i1, double j1, double k1)
            => Math.Abs(Magnitude(i1, j1, k1) - 1) < Epsilon;
    }
}
