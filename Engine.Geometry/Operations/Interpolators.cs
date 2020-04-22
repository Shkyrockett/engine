// <copyright file="Interpolators.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2020 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using static Engine.Mathematics;
using static Engine.Operations;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// Interpolators
    /// </summary>
    public static partial class Interpolators
    {
        /// <summary>
        /// Retrieves a list of points interpolated from a function.
        /// </summary>
        /// <param name="count">The number of points desired.</param>
        /// <param name="func">The function.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> Interpolate0to1(int count, Func<double, Point2D> func) => new List<Point2D>(from i in Enumerable.Range(0, count) select func(1d / count * i));

        #region Linear Interpolation
        /// <summary>
        /// Two control point 1D Linear interpolation for ranges from 0 to 1, start to end of curve.
        /// </summary>
        /// <param name="t">The t index of the linear curve.</param>
        /// <param name="aV">The first anchor value.</param>
        /// <param name="bV">The second anchor value.</param>
        /// <returns>
        /// Returns a <see cref="double" /> representing a point on the linear curve at the t index.
        /// </returns>
        /// <acknowledgment>
        /// http://paulbourke.net/geometry/bezier/index.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Linear(double t, double aV, double bV) => ((1d - t) * aV) + (t * bV);

        /// <summary>
        /// Two control point 2D Linear interpolation for ranges from 0 to 1, start to end of curve.
        /// </summary>
        /// <param name="t">The t index of the linear curve.</param>
        /// <param name="aX">The first anchor point x value.</param>
        /// <param name="aY">The first anchor point y value.</param>
        /// <param name="bX">The second anchor point x value.</param>
        /// <param name="bY">The second anchor point y value.</param>
        /// <returns>Returns a <see cref="ValueTuple{T1, T2}"/> representing a point on the linear curve at the t index.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/geometry/bezier/index.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Linear(double t, double aX, double aY, double bX, double bY) => (((1d - t) * aX) + (t * bX), ((1d - t) * aY) + (t * bY));

        /// <summary>
        /// Two control point 3D Linear interpolation for ranges from 0 to 1, start to end of curve.
        /// </summary>
        /// <param name="t">The t index of the linear curve.</param>
        /// <param name="aX">The first anchor point x value.</param>
        /// <param name="aY">The first anchor point y value.</param>
        /// <param name="aZ">The first anchor point z value.</param>
        /// <param name="bX">The second anchor point x value.</param>
        /// <param name="bY">The second anchor point y value.</param>
        /// <param name="bZ">The second anchor point z value.</param>
        /// <returns>
        /// Returns a <see cref="ValueTuple{T1, T2, T3}" /> representing a point on the linear curve at the t index.
        /// </returns>
        /// <acknowledgment>
        /// http://paulbourke.net/geometry/bezier/index.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Linear(double t, double aX, double aY, double aZ, double bX, double bY, double bZ) => (((1d - t) * aX) + (t * bX), ((1d - t) * aY) + (t * bY), ((1d - t) * aZ) + (t * bZ));

        /// <summary>
        /// Two control point 2D Linear interpolation for ranges from 0 to 1, start to end of curve.
        /// </summary>
        /// <param name="t">The t index of the linear curve.</param>
        /// <param name="a">The first anchor point.</param>
        /// <param name="b">The second anchor point value.</param>
        /// <returns>
        /// Returns a <see cref="Point2D" /> representing a point on the linear curve at the t index.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Linear(double t, Point2D a, Point2D b) => new Point2D(Linear(t, a.X, a.Y, b.X, b.Y));

        /// <summary>
        /// Two control point 3D Linear interpolation for ranges from 0 to 1, start to end of curve.
        /// </summary>
        /// <param name="t">The t index of the linear curve.</param>
        /// <param name="a">The first anchor point.</param>
        /// <param name="b">The second anchor point value.</param>
        /// <returns>Returns a <see cref="Point3D"/> representing a point on the linear curve at the t index.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D Linear(double t, Point3D a, Point3D b) => new Point3D(Linear(t, a.X, a.Y, a.Z, b.X, b.Y, b.Z));
        #endregion Linear Interpolation

        #region Normalized Linear Interpolation
        /// <summary>
        /// The nlerp.
        /// </summary>
        /// <param name="t">The percent.</param>
        /// <param name="aV">The start.</param>
        /// <param name="bV">The end.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        /// <acknowledgment>
        /// https://keithmaggio.wordpress.com/2011/02/15/math-magician-lerp-slerp-and-nlerp/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Nlerp(double t, double aV, double bV) => Normalize(Linear(t, aV, bV));

        /// <summary>
        /// The nlerp.
        /// </summary>
        /// <param name="t">The percent.</param>
        /// <param name="aX">The startX.</param>
        /// <param name="aY">The startY.</param>
        /// <param name="bX">The endX.</param>
        /// <param name="bY">The endY.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        /// <acknowledgment>
        /// https://keithmaggio.wordpress.com/2011/02/15/math-magician-lerp-slerp-and-nlerp/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Nlerp(double t, double aX, double aY, double bX, double bY)
        {
            var (X, Y) = Linear(t, aX, aY, bX, bY);
            return Normalize(X, Y);
        }

        /// <summary>
        /// The nlerp.
        /// </summary>
        /// <param name="t">The percent.</param>
        /// <param name="aX">The startX.</param>
        /// <param name="aY">The startY.</param>
        /// <param name="aZ">The startZ.</param>
        /// <param name="bX">The endX.</param>
        /// <param name="bY">The endY.</param>
        /// <param name="bZ">The endZ.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <acknowledgment>
        /// https://keithmaggio.wordpress.com/2011/02/15/math-magician-lerp-slerp-and-nlerp/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Nlerp(double t, double aX, double aY, double aZ, double bX, double bY, double bZ)
        {
            var (X, Y, Z) = Linear(t, aX, aY, aZ, bX, bY, bZ);
            return Normalize(X, Y, Z);
        }

        /// <summary>
        /// The nlerp.
        /// </summary>
        /// <param name="t">The percent.</param>
        /// <param name="a">The start.</param>
        /// <param name="b">The end.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        /// <acknowledgment>
        /// https://keithmaggio.wordpress.com/2011/02/15/math-magician-lerp-slerp-and-nlerp/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Nlerp(double t, Point2D a, Point2D b) => Normalize(Linear(t, a, b));

        /// <summary>
        /// The nlerp.
        /// </summary>
        /// <param name="t">The percent.</param>
        /// <param name="a">The start.</param>
        /// <param name="b">The end.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        /// <acknowledgment>
        /// https://keithmaggio.wordpress.com/2011/02/15/math-magician-lerp-slerp-and-nlerp/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D Nlerp(double t, Point3D a, Point3D b) => Normalize(Linear(t, a, b));
        #endregion Normalized Linear Interpolation

        #region Quaternion S Linear Interpolation
        /// <summary>
        /// The slerp.
        /// </summary>
        /// <param name="percent">The percent.</param>
        /// <param name="a">The start.</param>
        /// <param name="b">The end.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        /// <acknowledgment>
        /// https://keithmaggio.wordpress.com/2011/02/15/math-magician-lerp-slerp-and-nlerp/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Slerp(double percent, Point2D a, Point2D b)
        {
            // Dot product - the cosine of the angle between 2 vectors.
            // Clamp it to be in the range of Acos()
            // This may be unnecessary, but floating point
            // precision can be a fickle mistress.
            var dot = Operations.Clamp(DotProduct(a.X, a.Y, b.X, b.Y), -1d, 1d);

            // Acos(dot) returns the angle between start and end,
            // And multiplying that by percent returns the angle between
            // start and the final result.
            var theta = Acos(dot) * percent;
            var RelativeVec = b - (a * dot);

            // Orthonormal basis
            Normalize(RelativeVec);

            // The final result.
            return (a * Cos(theta)) + (RelativeVec * Sin(theta));
        }

        /// <summary>
        /// The slerp.
        /// </summary>
        /// <param name="percent">The percent.</param>
        /// <param name="a">The start.</param>
        /// <param name="b">The end.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        /// <acknowledgment>
        /// https://keithmaggio.wordpress.com/2011/02/15/math-magician-lerp-slerp-and-nlerp/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D Slerp(double percent, Point3D a, Point3D b)
        {
            // Dot product - the cosine of the angle between 2 vectors.
            // Clamp it to be in the range of Acos()
            // This may be unnecessary, but floating point
            // precision can be a fickle mistress.
            var dot = Operations.Clamp(Operations.DotProduct(a.X, a.Y, a.Y, b.X, b.Y, b.Z), -1d, 1d);

            // Acos(dot) returns the angle between start and end,
            // And multiplying that by percent returns the angle between
            // start and the final result.
            var theta = Acos(dot) * percent;
            var RelativeVec = b - (a * dot);

            // Orthonormal basis
            Operations.Normalize(RelativeVec.I, RelativeVec.J, RelativeVec.K);

            // The final result.
            return (a * Cos(theta)) + (RelativeVec * Sin(theta));
        }
        #endregion Quaternion S Linear Interpolation

        #region Curve Interpolation
        /// <summary>
        /// The curve.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="vCurve">The vCurve.</param>
        /// <returns>The <see cref="double"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Curve(double t, Polynomial vCurve)
        {
            var v = 0d;
            for (int s = vCurve.Count - 1, d = 0; s >= 0; s--, d++)
            {
                var r = 0d;
                for (var i = 0; i < d; i++)
                {
                    r *= t;
                }

                v += vCurve[s] * r;
            }

            return v;
        }

        /// <summary>
        /// The curve.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="xCurve">The xCurve.</param>
        /// <param name="yCurve">The yCurve.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double x, double y) Curve(double t, Polynomial xCurve, Polynomial yCurve)
        {
            var (x, y) = (0d, 0d);
            for (int s = xCurve.Count - 1, d = 0; s >= 0; s--, d++)
            {
                var r = 0d;
                for (var i = 0; i < d; i++)
                {
                    r *= t;
                }

                x += xCurve[s] * r;
                y += yCurve[s] * r;
            }

            return (x, y);
        }

        /// <summary>
        /// The curve.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="xCurve">The xCurve.</param>
        /// <param name="yCurve">The yCurve.</param>
        /// <param name="zCurve">The zCurve.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double x, double y, double z) Curve(double t, Polynomial xCurve, Polynomial yCurve, Polynomial zCurve)
        {
            var (x, y, z) = (0d, 0d, 0d);
            for (int s = xCurve.Count - 1, d = 0; s >= 0; s--, d++)
            {
                var r = 0d;
                for (var i = 0; i < d; i++)
                {
                    r *= t;
                }

                x += xCurve[s] * r;
                y += yCurve[s] * r;
                z += zCurve[s] * r;
            }

            return (x, y, z);
        }
        #endregion Curve Interpolation

        #region Quadratic Bézier Interpolation
        /// <summary>
        /// Three control point Bézier interpolation mu ranges from 0 to 1, start to end of the curve.
        /// </summary>
        /// <param name="t">The time parameter.</param>
        /// <param name="aV">The first parameter.</param>
        /// <param name="bV">The second parameter.</param>
        /// <param name="cV">The third parameter.</param>
        /// <returns>Returns a value interpolated from a Quadratic Bézier.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/geometry/bezier/index.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuadraticBezier(double t, double aV, double bV, double cV)
        {
            // The inverse of t.
            var ti = 1d - t;

            return (aV * ti * ti) + (2d * bV * ti * t) + (cV * t * t);
        }

        /// <summary>
        /// Three control point Bézier interpolation mu ranges from 0 to 1, start to end of the curve.
        /// </summary>
        /// <param name="t">The time parameter.</param>
        /// <param name="aX">The x-component of the first parameter.</param>
        /// <param name="aY">The y-component of the first parameter.</param>
        /// <param name="bX">The x-component of the second parameter.</param>
        /// <param name="bY">The y-component of the second parameter.</param>
        /// <param name="cX">The x-component of the third parameter.</param>
        /// <param name="cY">The y component of the third parameter.</param>
        /// <returns>Returns a point at t position of a Quadratic Bézier curve.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/geometry/bezier/index.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) QuadraticBezier(double t, double aX, double aY, double bX, double bY, double cX, double cY)
        {
            // The inverse of t.
            var ti = 1d - t;

            // The inverse of t squared.
            var ti2 = ti * ti;

            // The t squared.
            var t2 = t * t;

            return (
                (aX * ti2) + (2d * bX * ti * t) + (cX * t2),
                (aY * ti2) + (2d * bY * ti * t) + (cY * t2)
                );
        }

        /// <summary>
        /// Three control point Bézier interpolation mu ranges from 0 to 1, start to end of the curve.
        /// </summary>
        /// <param name="t">The time parameter of the Bézier curve.</param>
        /// <param name="x0">The x-component of the first point on a Bézier curve.</param>
        /// <param name="y0">The y-component of the first point on a Bézier curve.</param>
        /// <param name="z0">The z-component of the first point on a Bézier curve.</param>
        /// <param name="x1">The x-component of the handle point of the Bézier curve.</param>
        /// <param name="y1">The y-component of the handle point of the Bézier curve.</param>
        /// <param name="z1">The z-component of the handle point of the Bézier curve.</param>
        /// <param name="x2">The x-component of the last point on the Bézier curve.</param>
        /// <param name="y2">The y-component of the last point on the Bézier curve.</param>
        /// <param name="z2">The z-component of the last point on the Bézier curve.</param>
        /// <returns>Returns a point on the Bézier curve.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/geometry/bezier/index.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) QuadraticBezier(double t, double x0, double y0, double z0, double x1, double y1, double z1, double x2, double y2, double z2)
        {
            // The inverse of t.
            var ti = 1d - t;

            // The inverse of t squared.
            var ti2 = ti * ti;

            // The t squared.
            var t2 = t * t;

            return (
                (x0 * ti2) + (2d * x1 * ti * t) + (x2 * t2),
                (y0 * ti2) + (2d * y1 * ti * t) + (y2 * t2),
                (z0 * ti2) + (2d * z1 * ti * t) + (z2 * t2));
        }
        #endregion Quadratic Bézier Interpolation

        #region Cubic Interpolation
        /// <summary>
        /// The cubic.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="aV">The aV.</param>
        /// <param name="bV">The bV.</param>
        /// <param name="cV">The cV.</param>
        /// <param name="dV">The dV.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cubic(double t, double aV, double bV, double cV, double dV)
        {
            var t2 = t * t;
            var a0 = dV - cV - aV + bV;
            return (a0 * t * t2) + ((aV - bV - a0) * t2) + ((cV - aV) * t) + bV;
        }

        /// <summary>
        /// The cubic.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="aX">The aX.</param>
        /// <param name="aY">The aY.</param>
        /// <param name="bX">The bX.</param>
        /// <param name="bY">The bY.</param>
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        /// <param name="dX">The dX.</param>
        /// <param name="dY">The dY.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Cubic(double t, double aX, double aY, double bX, double bY, double cX, double cY, double dX, double dY)
        {
            var t2 = t * t;
            var aX0 = dX - cX - aX + bX;
            var aY0 = dY - cY - aY + bY;
            return (
                (aX0 * t * t2) + ((aX - bX - aX0) * t2) + ((cX - aX) * t) + bX,
                (aY0 * t * t2) + ((aY - bY - aY0) * t2) + ((cY - aY) * t) + bY);
        }

        /// <summary>
        /// The cubic.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="aX">The aX.</param>
        /// <param name="aY">The aY.</param>
        /// <param name="aZ">The aZ.</param>
        /// <param name="bX">The bX.</param>
        /// <param name="bY">The bY.</param>
        /// <param name="bZ">The bZ.</param>
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        /// <param name="cZ">The cZ.</param>
        /// <param name="dX">The dX.</param>
        /// <param name="dY">The dY.</param>
        /// <param name="dZ">The dZ.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Cubic(double t, double aX, double aY, double aZ, double bX, double bY, double bZ, double cX, double cY, double cZ, double dX, double dY, double dZ)
        {
            var t2 = t * t;
            var aX0 = dX - cX - aX + bX;
            var aY0 = dY - cY - aY + bY;
            var aZ0 = dZ - cZ - aZ + bZ;
            return (
                (aX0 * t * t2) + ((aX - bX - aX0) * t2) + ((cX - aX) * t) + bX,
                (aY0 * t * t2) + ((aY - bY - aY0) * t2) + ((cY - aY) * t) + bY,
                (aZ0 * t * t2) + ((aZ - bZ - aZ0) * t2) + ((cZ - aZ) * t) + bZ);
        }
        #endregion Cubic Interpolation

        #region Cubic Bézier Interpolation
        /// <summary>
        /// Four control point 1D Quadratic Bézier interpolation for ranges from 0 to 1, start to end of curve.
        /// </summary>
        /// <param name="t">The t index of the curve.</param>
        /// <param name="v0">The first anchor value.</param>
        /// <param name="v1">The first handle value.</param>
        /// <param name="v2">The second handle value.</param>
        /// <param name="v3">The second anchor value.</param>
        /// <returns>Returns a <see cref="double"/> representing a value on the Bézier curve at the t index.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/geometry/bezier/index.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CubicBezier(double t, double v0, double v1, double v2, double v3)
        {
            // The inverse of t.
            var ti = 1d - t;

            // The inverse of t cubed.
            var ti3 = ti * ti * ti;

            // The t cubed.
            var t3 = t * t * t;

            return (ti3 * v0) + (3d * t * ti * ti * v1) + (3d * t * t * ti * v2) + (t3 * v3);
        }

        /// <summary>
        /// Four control point 2D Quadratic Bézier interpolation for ranges from 0 to 1, start to end of curve.
        /// </summary>
        /// <param name="t">The t index of the curve.</param>
        /// <param name="x0">The first anchor point x value.</param>
        /// <param name="y0">The first anchor point y value.</param>
        /// <param name="x1">The first handle point x value.</param>
        /// <param name="y1">The first handle point y value.</param>
        /// <param name="x2">The second handle point x value.</param>
        /// <param name="y2">The second handle point y value.</param>
        /// <param name="x3">The second anchor point x value.</param>
        /// <param name="y3">The second anchor point y value.</param>
        /// <returns>Returns a <see cref="ValueTuple{T1, T2}"/> representing a point on the Bézier curve at the t index.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/geometry/bezier/index.html
        /// https://github.com/burningmime/curves
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) CubicBezier(double t, double x0, double y0, double x1, double y1, double x2, double y2, double x3, double y3)
        {
            // The inverse of t.
            var ti = 1d - t;

            // The inverse of t cubed.
            var ti3 = ti * ti * ti;

            // The t cubed.
            var t3 = t * t * t;

            return (
                X: (ti3 * x0) + (3d * ti * ti * t * x1) + (3d * ti * t * t * x2) + (t3 * x3),
                Y: (ti3 * y0) + (3d * ti * ti * t * y1) + (3d * ti * t * t * y2) + (t3 * y3)
                );

            //return (Point2D)(ti3 * A + 3d * ti * ti * t * B + 3d * ti * t * t * C + t3 * D);
        }

        /// <summary>
        /// Four control point 2D Quadratic Bézier interpolation for ranges from 0 to 1, start to end of curve.
        /// </summary>
        /// <param name="t">The t index of the curve.</param>
        /// <param name="x0">The first anchor point x value.</param>
        /// <param name="y0">The first anchor point y value.</param>
        /// <param name="z0">The first anchor point z value..</param>
        /// <param name="x1">The first handle point x value.</param>
        /// <param name="y1">The first handle point y value.</param>
        /// <param name="z1">The first handle point z value.</param>
        /// <param name="x2">The second handle point x value.</param>
        /// <param name="y2">The second handle point y value.</param>
        /// <param name="z2">The second handle point z value.</param>
        /// <param name="x3">The second anchor point x value.</param>
        /// <param name="y3">The second anchor point y value.</param>
        /// <param name="z3">The second anchor point z value.</param>
        /// <returns>Returns a <see cref="ValueTuple{T1, T2, T3}"/> representing a point on the Bézier curve at the t index.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/geometry/bezier/index.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) CubicBezier(double t, double x0, double y0, double z0, double x1, double y1, double z1, double x2, double y2, double z2, double x3, double y3, double z3)
        {
            // The inverse of t.
            var ti = 1d - t;

            // The inverse of t cubed.
            var ti3 = ti * ti * ti;

            // The t cubed.
            var t3 = t * t * t;

            return (
                (ti3 * x0) + (3d * t * ti * ti * x1) + (3d * t * t * ti * x2) + (t3 * x3),
                (ti3 * y0) + (3d * t * ti * ti * y1) + (3d * t * t * ti * y2) + (t3 * y3),
                (ti3 * z0) + (3d * t * ti * ti * z1) + (3d * t * t * ti * z2) + (t3 * z3)
                );
        }
        #endregion Cubic Bézier Interpolation

        #region Quintic Bézier Interpolation
        /// <summary>
        /// Quintics the bezier.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="p0X">The p0 x.</param>
        /// <param name="p0Y">The p0 y.</param>
        /// <param name="p1X">The p1 x.</param>
        /// <param name="p1Y">The p1 y.</param>
        /// <param name="p2X">The p2 x.</param>
        /// <param name="p2Y">The p2 y.</param>
        /// <param name="p3X">The p3 x.</param>
        /// <param name="p3Y">The p3 y.</param>
        /// <param name="p4X">The p4 x.</param>
        /// <param name="p4Y">The p4 y.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) QuinticBezier(double t, double p0X, double p0Y, double p1X, double p1Y, double p2X, double p2Y, double p3X, double p3Y, double p4X, double p4Y) => CubicBezierSpline(t, new List<Point2D> { (p0X, p0Y), (p1X, p1Y), (p2X, p2Y), (p3X, p3Y), (p4X, p4Y) });
        #endregion

        #region N Bézier Interpolation
        /// <summary>
        /// General Bézier curve Number of control points is n+1 0 less than or equal to mu less than 1
        /// IMPORTANT, the last point is not computed.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://paulbourke.net/geometry/bezier/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D CubicBezierSpline(double t, List<Point2D> points)
        {
            var n = points.Count - 1;
            int kn;
            int nn;
            int nkn;

            double blend;
            var muk = 1d;
            var munk = Pow(1d - t, n);

            var b = Point2D.Empty;

            for (var k = 0; k <= n; k++)
            {
                nn = n;
                kn = k;
                nkn = n - k;
                blend = muk * munk;
                muk *= t;
                munk /= 1d - t;
                while (nn >= 1)
                {
                    blend *= nn;
                    nn--;
                    if (kn > 1)
                    {
                        blend /= kn;
                        kn--;
                    }
                    if (nkn > 1)
                    {
                        blend /= nkn;
                        nkn--;
                    }
                }

                b = new Point2D(
                    b.X + (points[k].X * blend),
                    b.Y + (points[k].Y * blend)
                );
            }

            return b;
        }
        #endregion N Bézier Interpolation

        #region Catmull-Rom Spline Interpolation
        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// </summary>
        /// <param name="t">Weighting factor.</param>
        /// <param name="aV">The first position in the interpolation.</param>
        /// <param name="bV">The second position in the interpolation.</param>
        /// <param name="cV">The third position in the interpolation.</param>
        /// <param name="dV">The fourth position in the interpolation.</param>
        /// <returns>A position that is the result of the Catmull-Rom interpolation.</returns>
        /// <acknowledgment>
        /// http://www.mvps.org/directx/articles/catmull/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CatmullRom(double t, double aV, double bV, double cV, double dV)
        {
            var t2 = t * t;
            var t3 = t2 * t;
            return
                0.5d * ((2d * bV)
                + ((cV - aV) * t)
                + (((2d * aV) - (5d * bV) + (4d * cV) - dV) * t2)
                + (((3d * bV) - aV - (3.0d * cV) + dV) * t3));
        }

        /// <summary>
        /// Calculates interpolated point between two points using Catmull-Rom Spline
        /// </summary>
        /// <param name="t">
        /// Normalized distance between second and third point
        /// where the spline point will be calculated
        /// </param>
        /// <param name="aX">First Point</param>
        /// <param name="aY">First Point</param>
        /// <param name="bX">Second Point</param>
        /// <param name="bY">Second Point</param>
        /// <param name="cX">Third Point</param>
        /// <param name="cY">Third Point</param>
        /// <param name="dX">Fourth Point</param>
        /// <param name="dY">Fourth Point</param>
        /// <returns>
        /// Calculated Spline Point
        /// </returns>
        /// <remarks>
        /// <para>Points calculated exist on the spline between points two and three.</para>
        /// </remarks>
        /// <acknowledgment>
        /// From: http://tehc0dez.blogspot.com/2010/04/nice-curves-catmullrom-spline-in-c.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) CatmullRom(double t, double aX, double aY, double bX, double bY, double cX, double cY, double dX, double dY)
        {
            var t2 = t * t;
            var t3 = t2 * t;
            return (
                0.5d * ((2d * bX)
                + ((-aX + cX) * t)
                + (((2d * aX) - (5d * bX) + (4d * cX) - dX) * t2)
                + ((-aX + (3d * bX) - (3d * cX) + dX) * t3)),
                0.5d * ((2d * bY)
                + ((-aY + cY) * t)
                + (((2d * aY) - (5d * bY) + (4d * cY) - dY) * t2)
                + ((-aY + (3d * bY) - (3d * cY) + dY) * t3)));
        }

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// </summary>
        /// <param name="t">Weighting factor.</param>
        /// <param name="aX">The first position in the interpolation.</param>
        /// <param name="aY">The first position in the interpolation.</param>
        /// <param name="aZ">The first position in the interpolation.</param>
        /// <param name="bX">The second position in the interpolation.</param>
        /// <param name="bY">The second position in the interpolation.</param>
        /// <param name="bZ">The second position in the interpolation.</param>
        /// <param name="cX">The third position in the interpolation.</param>
        /// <param name="cY">The third position in the interpolation.</param>
        /// <param name="cZ">The third position in the interpolation.</param>
        /// <param name="dX">The fourth position in the interpolation.</param>
        /// <param name="dY">The fourth position in the interpolation.</param>
        /// <param name="dZ">The fourth position in the interpolation.</param>
        /// <returns>A position that is the result of the Catmull-Rom interpolation.</returns>
        /// <acknowledgment>
        /// http://www.mvps.org/directx/articles/catmull/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) CatmullRom(double t, double aX, double aY, double aZ, double bX, double bY, double bZ, double cX, double cY, double cZ, double dX, double dY, double dZ)
        {
            var t2 = t * t;
            var t3 = t2 * t;
            return (
                0.5d * ((2d * bX)
                + ((cX - aX) * t)
                + (((2d * aX) - (5d * bX) + (4d * cX) - dX) * t2)
                + (((3d * bX) - aX - (3d * cX) + dX) * t3)),
                0.5d * ((2d * bX)
                + ((cY - aY) * t)
                + (((2d * aY) - (5d * bY) + (4d * cY) - dY) * t2)
                + (((3d * bY) - aY - (3d * cY) + dY) * t3)),
                0.5d * ((2d * bZ)
                + ((cZ - aZ) * t)
                + (((2d * aZ) - (5d * bZ) + (4d * cZ) - dZ) * t2)
                + (((3d * bZ) - aZ - (3d * cZ) + dZ) * t3)));
        }

        /// <summary>
        /// The catmull rom.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="tangentA">The tangentA.</param>
        /// <param name="positionA">The positionA.</param>
        /// <param name="positionB">The positionB.</param>
        /// <param name="tangentB">The tangentB.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        /// <acknowledgment>
        /// http://tehc0dez.blogspot.com/2010/04/nice-curves-catmullrom-spline-in-c.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D CatmullRom(double t, Point2D tangentA, Point2D positionA, Point2D positionB, Point2D tangentB)
        {
            var t2 = t * t;
            var t3 = t2 * t;
            return new Point2D(
                0.5d * ((2d * positionA.X)
                + ((-tangentA.X + positionB.X) * t) + (((2d * tangentA.X) - (5d * positionA.X)
                + (4d * positionB.X) - tangentB.X) * t2)
                + ((-tangentA.X + (3d * positionA.X) - (3d * positionB.X) + tangentB.X) * t3)),
                0.5d * ((2d * positionA.Y)
                + ((-tangentA.Y + positionB.Y) * t) + (((2d * tangentA.Y) - (5d * positionA.Y)
                + (4d * positionB.Y) - tangentB.Y) * t2)
                + ((-tangentA.Y + (3d * positionA.Y) - (3d * positionB.Y) + tangentB.Y) * t3))
            );
        }
        #endregion Catmull-Rom Spline Interpolation

        #region Hermite Interpolation
        /// <summary>
        /// The hermite.
        /// </summary>
        /// <param name="t">The t time index parameter.</param>
        /// <param name="aV">The aV.</param>
        /// <param name="bV">The bV.</param>
        /// <param name="cV">The cV.</param>
        /// <param name="dV">The dV.</param>
        /// <param name="tension">1 is high, 0 normal, -1 is low</param>
        /// <param name="bias">0 is even,positive is towards first segment, negative towards the other</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Hermite(double t, double aV, double bV, double cV, double dV, double tension = 0d, double bias = 0d)
        {
            var t2 = t * t;
            var t3 = t2 * t;

            var m0 = (bV - aV) * (1d + bias) * (1d - tension) * 0.5d;
            m0 += (cV - bV) * (1d - bias) * (1d - tension) * 0.5d;

            var m1 = (cV - bV) * (1d + bias) * (1d - tension) * 0.5d;
            m1 += (dV - cV) * (1d - bias) * (1d - tension) * 0.5d;

            return (((2d * t3) - (3d * t2) + 1d) * bV) + ((t3 - (2d * t2) + t) * m0) + ((t3 - t2) * m1) + (((-2d * t3) + (3d * t2)) * cV);
        }

        /// <summary>
        /// The hermite.
        /// </summary>
        /// <param name="t">The t time index parameter.</param>
        /// <param name="aX">The aX.</param>
        /// <param name="aY">The aY.</param>
        /// <param name="bX">The bX.</param>
        /// <param name="bY">The bY.</param>
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        /// <param name="dX">The dX.</param>
        /// <param name="dY">The dY.</param>
        /// <param name="tension">1 is high, 0 normal, -1 is low</param>
        /// <param name="bias">0 is even,positive is towards first segment, negative towards the other</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Hermite(double t, double aX, double aY, double bX, double bY, double cX, double cY, double dX, double dY, double tension = 0d, double bias = 0d)
        {
            var t2 = t * t;
            var t3 = t2 * t;

            var mX0 = (bX - aX) * (1d + bias) * (1d - tension) * 0.5d;
            mX0 += (cX - bX) * (1d - bias) * (1d - tension) * 0.5d;

            var mY0 = (bY - aY) * (1d + bias) * (1d - tension) * 0.5d;
            mY0 += (cY - bY) * (1d - bias) * (1d - tension) * 0.5d;

            var mX1 = (cX - bX) * (1d + bias) * (1d - tension) * 0.5d;
            mX1 += (dX - cX) * (1d - bias) * (1d - tension) * 0.5d;

            var mY1 = (cY - bY) * (1d + bias) * (1d - tension) * 0.5d;
            mY1 += (dY - cY) * (1d - bias) * (1d - tension) * 0.5d;

            var a0 = (2d * t3) - (3d * t2) + 1d;
            var a1 = t3 - (2d * t2) + t;
            var a2 = t3 - t2;
            var a3 = (-2d * t3) + (3d * t2);

            return (
                (a0 * bX) + (a1 * mX0) + (a2 * mX1) + (a3 * cX),
                (a0 * bY) + (a1 * mY0) + (a2 * mY1) + (a3 * cY));
        }

        /// <summary>
        /// The hermite.
        /// </summary>
        /// <param name="t">The t time index parameter.</param>
        /// <param name="aX">The aX.</param>
        /// <param name="aY">The aY.</param>
        /// <param name="aZ">The aZ.</param>
        /// <param name="bX">The bX.</param>
        /// <param name="bY">The bY.</param>
        /// <param name="bZ">The bZ.</param>
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        /// <param name="cZ">The cZ.</param>
        /// <param name="dX">The dX.</param>
        /// <param name="dY">The dY.</param>
        /// <param name="dZ">The dZ.</param>
        /// <param name="tension">1 is high, 0 normal, -1 is low</param>
        /// <param name="bias">0 is even, positive is towards first segment, negative towards the other</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Hermite(double t, double aX, double aY, double aZ, double bX, double bY, double bZ, double cX, double cY, double cZ, double dX, double dY, double dZ, double tension = 0d, double bias = 0d)
        {
            var t2 = t * t;
            var t3 = t2 * t;

            var mX0 = (bX - aX) * (1d + bias) * (1d - tension) * 0.5d;
            mX0 += (cX - bX) * (1d - bias) * (1d - tension) * 0.5d;

            var mY0 = (bY - aY) * (1d + bias) * (1d - tension) * 0.5d;
            mY0 += (cY - bY) * (1d - bias) * (1d - tension) * 0.5d;

            var mZ0 = (bZ - aZ) * (1d + bias) * (1d - tension) * 0.5d;
            mZ0 += (cZ - bZ) * (1d - bias) * (1d - tension) * 0.5d;

            var mX1 = (cX - bX) * (1d + bias) * (1 - tension) * 0.5d;
            mX1 += (dX - cX) * (1d - bias) * (1d - tension) * 0.5d;

            var mY1 = (cY - bY) * (1d + bias) * (1d - tension) * 0.5d;
            mY1 += (dY - cY) * (1d - bias) * (1d - tension) * 0.5d;

            var mZ1 = (cZ - bZ) * (1d + bias) * (1d - tension) * 0.5d;
            mZ1 += (dZ - cZ) * (1d - bias) * (1d - tension) * 0.5d;

            var a0 = (2d * t3) - (3d * t2) + 1d;
            var a1 = t3 - (2d * t2) + t;
            var a2 = t3 - t2;
            var a3 = (-2d * t3) + (3d * t2);

            return (
                (a0 * bX) + (a1 * mX0) + (a2 * mX1) + (a3 * cX),
                (a0 * bY) + (a1 * mY0) + (a2 * mY1) + (a3 * cY),
                (a0 * bZ) + (a1 * mZ0) + (a2 * mZ1) + (a3 * cZ));
        }
        #endregion Hermite Interpolation

        #region Circle Interpolation
        /// <summary>
        /// Interpolates the Arc.
        /// </summary>
        /// <param name="t">Theta of interpolation.</param>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r">Radius of circle.</param>
        /// <param name="startAngle">The angle to start the arc.</param>
        /// <param name="sweepAngle">The difference of the angle to where the arc should end.</param>
        /// <returns>Interpolated point at theta.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) CircularArc(double t, double cX, double cY, double r, double startAngle, double sweepAngle) => Circle(startAngle + (sweepAngle * t), cX, cY, r);

        /// <summary>
        /// Interpolate a point on a circle, converting from unit iteration, to Pi radians.
        /// </summary>
        /// <param name="t">Theta of interpolation.</param>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r">Radius of circle.</param>
        /// <returns>Interpolated point at theta.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) UnitCircle(double t, double cX, double cY, double r) => Circle(Tau * t, cX, cY, r);

        /// <summary>
        /// Interpolate a point on a circle, applying translation to equation of circle at origin.
        /// </summary>
        /// <param name="t">Theta of interpolation.</param>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r">Radius of circle.</param>
        /// <returns>Interpolated point at theta.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Circle(double t, double cX, double cY, double r) => (cX + (Cos(t) * r), cY + (Sin(t) * r));
        #endregion Circle Interpolation

        #region Ellipse Interpolation
        /// <summary>
        /// Interpolates the orthogonal elliptical Arc.
        /// </summary>
        /// <param name="t">Theta of interpolation.</param>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="startAngle">The angle to start the arc.</param>
        /// <param name="sweepAngle">The difference of the angle to where the arc should end.</param>
        /// <returns>Interpolated point at theta.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) EllipticalArc(double t, double cX, double cY, double r1, double r2, double startAngle, double sweepAngle)
        {
            var phi = startAngle + (sweepAngle * t);
            var theta = phi % PI;

            var tanAngle = Abs(Tan(theta));
            var x = Sqrt(r1 * r1 * (r2 * r2) / ((r2 * r2) + (r1 * r1 * (tanAngle * tanAngle))));
            var y = x * tanAngle;

            return (theta >= 0d) && (theta < 90d.DegreesToRadians())
                ? (cX + x, cY + y)
                : (theta >= 90d.DegreesToRadians()) && (theta < 180d.DegreesToRadians())
                ? (cX - x, cY + y)
                : (theta >= 180d.DegreesToRadians()) && (theta < 270d.DegreesToRadians()) ? (cX - x, cY - y) : (cX + x, cY - y);
        }

        /// <summary>
        /// Interpolates the Elliptical Arc, corrected for Polar coordinates.
        /// </summary>
        /// <param name="t">Theta of interpolation.</param>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <param name="startAngle">The angle to start the arc.</param>
        /// <param name="sweepAngle">The difference of the angle to where the arc should end.</param>
        /// <returns>Interpolated point at theta.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) EllipticalArc(double t, double cX, double cY, double r1, double r2, double angle, double startAngle, double sweepAngle) => PolarEllipse(startAngle + (sweepAngle * t), cX, cY, r1, r2, angle);

        /// <summary>
        /// Interpolates the Elliptical Arc, corrected for Polar coordinates.
        /// </summary>
        /// <param name="t">Theta of interpolation.</param>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="cosAngle">Horizontal rotation transform of the Ellipse.</param>
        /// <param name="sinAngle">Vertical rotation transform of the Ellipse.</param>
        /// <param name="startAngle">The angle to start the arc.</param>
        /// <param name="sweepAngle">The difference of the angle to where the arc should end.</param>
        /// <returns>Interpolated point at theta.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) EllipticalArc(double t, double cX, double cY, double r1, double r2, double cosAngle, double sinAngle, double startAngle, double sweepAngle) => PolarEllipse(startAngle + (sweepAngle * t), cX, cY, r1, r2, cosAngle, sinAngle);

        /// <summary>
        /// Interpolate a point on an Ellipse with Polar correction using a range from 0 to 1 for unit interpolation.
        /// </summary>
        /// <param name="t">Theta of interpolation.</param>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <returns>Interpolated point at theta adjusted to Polar angles.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) UnitPolarEllipse(double t, double cX, double cY, double r1, double r2, double angle) => PolarEllipse(Tau * t, cX, cY, r1, r2, angle);

        /// <summary>
        /// Interpolate a point on an Ellipse with Polar correction using a range from 0 to 1 for unit interpolation.
        /// </summary>
        /// <param name="t">Theta of interpolation.</param>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="cosAngle">Horizontal rotation transform of the Ellipse.</param>
        /// <param name="sinAngle">Vertical rotation transform of the Ellipse.</param>
        /// <returns>Interpolated point at theta adjusted to Polar angles.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) UnitPolarEllipse(double t, double cX, double cY, double r1, double r2, double cosAngle, double sinAngle) => PolarEllipse(Tau * t, cX, cY, r1, r2, cosAngle, sinAngle);

        /// <summary>
        /// Interpolate a point on an Ellipse with Polar correction.
        /// </summary>
        /// <param name="t">Theta of interpolation.</param>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <returns>Interpolated point at theta adjusted to Polar angles.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) PolarEllipse(double t, double cX, double cY, double r1, double r2, double angle) => Ellipse(Operations.EllipticalPolarAngle(t, r1, r2), cX, cY, r1, r2, angle);

        /// <summary>
        /// Interpolate a point on an Ellipse with Polar correction.
        /// </summary>
        /// <param name="t">Theta of interpolation.</param>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="cosAngle">Horizontal rotation transform of the Ellipse.</param>
        /// <param name="sinAngle">Vertical rotation transform of the Ellipse.</param>
        /// <returns>Interpolated point at theta adjusted to Polar angles.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) PolarEllipse(double t, double cX, double cY, double r1, double r2, double cosAngle, double sinAngle) => Ellipse(Operations.EllipticalPolarAngle(t, r1, r2), cX, cY, r1, r2, cosAngle, sinAngle);

        /// <summary>
        /// Interpolate a point on an Ellipse.
        /// </summary>
        /// <param name="t">Theta of interpolation.</param>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <returns>Interpolated point at theta.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Ellipse(double t, double cX, double cY, double r1, double r2, double angle) => Ellipse(Cos(t), Sin(t), cX, cY, r1, r2, Cos(angle), Sin(angle));

        /// <summary>
        /// Interpolate a point on an Ellipse.
        /// </summary>
        /// <param name="t">Theta of interpolation.</param>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="cosAngle">Horizontal rotation transform of the Ellipse.</param>
        /// <param name="sinAngle">Vertical rotation transform of the Ellipse.</param>
        /// <returns>Interpolated point at theta.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Ellipse(double t, double cX, double cY, double r1, double r2, double cosAngle, double sinAngle) => Ellipse(Cos(t), Sin(t), cX, cY, r1, r2, cosAngle, sinAngle);

        /// <summary>
        /// Interpolate a point on an Ellipse.
        /// </summary>
        /// <param name="cosTheta">Theta cosine of interpolation.</param>
        /// <param name="sinTheta">Theta sine of interpolation.</param>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="cosAngle">Horizontal rotation transform of the Ellipse.</param>
        /// <param name="sinAngle">Vertical rotation transform of the Ellipse.</param>
        /// <returns>Interpolated point at theta.</returns>
        /// <acknowledgment>
        /// http://www.vbforums.com/showthread.php?686351-RESOLVED-Elliptical-orbit
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Ellipse(double cosTheta, double sinTheta, double cX, double cY, double r1, double r2, double cosAngle, double sinAngle)
        {
            // Ellipse equation for an ellipse at origin.
            var u = r1 * cosTheta;
            var v = -(r2 * sinTheta);

            // Apply the rotation transformation and translate to new center.
            return (
                cX + ((u * cosAngle) + (v * sinAngle)),
                cY + ((u * sinAngle) - (v * cosAngle)));
        }
        #endregion Ellipse Interpolation

        #region Cosine Interpolation
        /// <summary>
        /// The cosine.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="aV">The aV.</param>
        /// <param name="bV">The bV.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cosine(double t, double aV, double bV)
        {
            var mu2 = 0.5d * (1d - Cos(t * PI));
            return (aV * (1d - mu2)) + (bV * mu2);
        }

        /// <summary>
        /// The cosine.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="aX">The aX.</param>
        /// <param name="aY">The aY.</param>
        /// <param name="bX">The bX.</param>
        /// <param name="bY">The bY.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Cosine(double t, double aX, double aY, double bX, double bY)
        {
            var mu2 = 0.5d * (1d - Cos(t * PI));
            return ((aX * (1d - mu2)) + (bX * mu2),
                    (aY * (1d - mu2)) + (bY * mu2));
        }

        /// <summary>
        /// The cosine.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="aX">The aX.</param>
        /// <param name="aY">The aY.</param>
        /// <param name="aZ">The aZ.</param>
        /// <param name="bX">The bX.</param>
        /// <param name="bY">The bY.</param>
        /// <param name="bZ">The bZ.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Cosine(double t, double aX, double aY, double aZ, double bX, double bY, double bZ)
        {
            var mu2 = 0.5d * (1d - Cos(t * PI));
            return (
                (aX * (1d - mu2)) + (bX * mu2),
                (aY * (1d - mu2)) + (bY * mu2),
                (aZ * (1d - mu2)) + (bZ * mu2));
        }
        #endregion Cosine Interpolation

        #region Sine Interpolation
        /// <summary>
        /// Interpolate a sine wave.
        /// </summary>
        /// <param name="t">The time parameter.</param>
        /// <param name="v1">The first parameter.</param>
        /// <param name="v2">The second Parameter.</param>
        /// <returns>Returns a value of a Sine wave at t.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sine(double t, double v1, double v2)
        {
            var mu2 = (1d - Sin(t * PI)) * 0.5d;
            return (v1 * (1d - mu2)) + (v2 * mu2);
        }

        /// <summary>
        /// Interpolates a sine wave in 2 dimensions.
        /// </summary>
        /// <param name="t">The t parameter.</param>
        /// <param name="x1">The first x component.</param>
        /// <param name="y1">The first y-component.</param>
        /// <param name="x2">The second x-component.</param>
        /// <param name="y2">The second y-component.</param>
        /// <returns>Returns a point interpolated of a Sine wave.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Sine(double t, double x1, double y1, double x2, double y2)
        {
            var mu2 = (1d - Sin(t * PI)) * 0.5d;
            return (
                (x1 * (1d - mu2)) + (x2 * mu2),
                (y1 * (1d - mu2)) + (y2 * mu2));
        }

        /// <summary>
        /// Interpolates a Sine wave at t parameter.
        /// </summary>
        /// <param name="t">The time parameter.</param>
        /// <param name="x1">The x-component of the first parameter.</param>
        /// <param name="y1">The y-component of the first parameter.</param>
        /// <param name="z1">The z-component of the first parameter.</param>
        /// <param name="x2">The x-component of the second parameter.</param>
        /// <param name="y2">The y-component of the second parameter.</param>
        /// <param name="z2">The z-component of the second parameter.</param>
        /// <returns>Returns a point in 2 dimensional space interpolated from a sine wave.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Sine(double t, double x1, double y1, double z1, double x2, double y2, double z2)
        {
            var mu2 = (1d - Sin(t * PI)) * 0.5d;
            return (
                (x1 * (1d - mu2)) + (x2 * mu2),
                (y1 * (1d - mu2)) + (y2 * mu2),
                (z1 * (1d - mu2)) + (z2 * mu2));
        }
        #endregion Sine Interpolation

        #region Parabola
        /// <summary>
        /// Interpolate a parabola from the standard parabolic equation.
        /// </summary>
        /// <param name="t">The <paramref name="t" />ime index of the iteration.</param>
        /// <param name="a">The <paramref name="a" /> component of the parabola.</param>
        /// <param name="b">The <paramref name="b" /> component of the parabola.</param>
        /// <param name="c">The <paramref name="c" /> component of the parabola.</param>
        /// <param name="x1">The <paramref name="x1" />imum x value to interpolate.</param>
        /// <param name="x2">The <paramref name="x2" />imum x value to interpolate.</param>
        /// <returns>
        /// Returns a <see cref="ValueTuple{T1, T2}" /> representing the interpolated point at the t index.
        /// </returns>
        /// <example>
        ///   <code>
        /// var a = 0.0125d;
        /// var h = 100d;
        /// var k = 100d;
        /// var b = -2d * a * h;
        /// var c = (b * b / (4 * a)) + k;
        /// var min = -100d;
        /// var max = 100d;
        /// var list = new List&lt;(double X, double Y)&gt;();
        /// for (int i = 0; i &lt; 100; i++)
        /// {
        /// list.Add(InterpolateVertexParabola(a, b, c, -100, 100, 1d / i));
        /// }
        /// </code>
        /// </example>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) InterpolateStandardParabola(double t, double a, double b, double c, double x1, double x2)
        {
            // Scale the t index to the segment range.
            var x = x1 + ((x2 - x1) * t);
            return (x, Y: (a * (x * x)) + ((b * x) + c));
        }

        /// <summary>
        /// Interpolate a parabola from the general vertex form of the parabolic equation.
        /// </summary>
        /// <param name="t">The <paramref name="t" />ime index of the iteration.</param>
        /// <param name="a">The <paramref name="a" /> component of the parabola.</param>
        /// <param name="h">The horizontal component of the parabola vertex.</param>
        /// <param name="k">The vertical component of the parabola vertex.</param>
        /// <param name="x1">The <paramref name="x1" />imum x value to interpolate.</param>
        /// <param name="x2">The <paramref name="x2" />imum x value to interpolate.</param>
        /// <returns>
        /// Returns a <see cref="ValueTuple{T1, T2}" /> representing the interpolated point at the t index.
        /// </returns>
        /// <example>
        ///   <code>
        /// var a = 0.0125d;
        /// var h = 100d;
        /// var k = 100d;
        /// var min = -100d;
        /// var max = 100d;
        /// var list = new List&lt;(double X, double Y)&gt;();
        /// for (int i = 0; i &lt; 100; i++)
        /// {
        /// list.Add(InterpolateVertexParabola(a, h, k, -100, 100, 1d / i));
        /// }
        /// </code>
        /// </example>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) InterpolateVertexParabola(double t, double a, double h, double k, double x1, double x2)
        {
            // Scale the t index to the segment range.
            var x = x1 + ((x2 - x1) * t);
            return (x, Y: (a * (x - h) * (x - h)) + k);
        }

        /// <summary>
        /// Interpolates the parabola.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="k">The k.</param>
        /// <returns></returns>
        public static (double X, double Y) InterpolateParabola(double t, double x1, double y1, double x2, double y2, double k)
        {
            var parabolicT = (t * 2d) - 1d;
            var (dX, dY) = (x2 - x1, y2 - y1);
            if (Abs(dX) < Epsilon && Abs(dY) < Epsilon)
            {
                // In place Vertical Throw.
                return (x1, y1 + k * ((-4d * t * t) + (4d * t)));
            }
            if (Abs(dX) < Epsilon)
            {
                // Vertical throw with different heights.
                return (x1, y1 + (t * dY) + (((-parabolicT * parabolicT) + 1d) * k));
            }
            else if (Abs(dY) < Epsilon && y1 == k)
            {
                // Horizontal slide.
                return (((1d - t) * x1) + (t * x2), y1);
            }
            else if (Abs(dY) < Epsilon)
            {
                // Start and end are roughly level, pretend they are - simpler solution with fewer steps.
                return (x1 + (t * dX), y1 + (t * dY) + (((-parabolicT * parabolicT) + 1d) * k));
            }
            else
            {
                // Other parabola.
                return (double.NaN, double.NaN);
            }
        }
        #endregion

        /// <summary>
        /// Linearly tweens between two cubic Bézier curves, from key1 to key2.
        /// </summary>
        /// <param name="t">The t index.</param>
        /// <param name="key1">The first cubic Bézier key.</param>
        /// <param name="key2">The second cubic Bézier key.</param>
        /// <returns>
        /// The <see cref="Array" />.
        /// </returns>
        public static CubicBezier2D TweenCubic(double t, CubicBezier2D key1, CubicBezier2D key2)
            => new CubicBezier2D(
                 key1.A + (t * (key2.A - key1.A)),
                 key1.B + (t * (key2.B - key1.B)),
                 key1.C + (t * (key2.C - key1.C)),
                 key1.D + (t * (key2.D - key1.D))
                );
    }
}
