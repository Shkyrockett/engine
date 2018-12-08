// <copyright file="Interpolators.cs" company="Shkyrockett" >
//     Copyright © 2016 - 2018 Shkyrockett. All rights reserved.
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
using static Engine.Maths;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// Interpolators
    /// </summary>
    public static class Interpolators
    {
        /// <summary>
        /// Retrieves a list of points interpolated from a function.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="count">The number of points desired.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> Interpolate0to1(Func<double, Point2D> func, int count)
            => new List<Point2D>(
            from i in Enumerable.Range(0, count)
            select func(1d / count * i));

        #region Linear Interpolation
        /// <summary>
        /// Two control point 1D Linear interpolation for ranges from 0 to 1, start to end of curve.
        /// </summary>
        /// <param name="aV">The first anchor value.</param>
        /// <param name="bV">The second anchor value.</param>
        /// <param name="t">The t index of the linear curve.</param>
        /// <returns>Returns a <see cref="double"/> representing a point on the linear curve at the t index.</returns>

        /// <acknowledgment>
        /// http://paulbourke.net/geometry/bezier/index.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Linear(
            double aV,
            double bV,
            double t)
            => (1d - t) * aV + t * bV;

        /// <summary>
        /// Two control point 2D Linear interpolation for ranges from 0 to 1, start to end of curve.
        /// </summary>
        /// <param name="aX">The first anchor point x value.</param>
        /// <param name="aY">The first anchor point y value.</param>
        /// <param name="bX">The second anchor point x value.</param>
        /// <param name="bY">The second anchor point y value.</param>
        /// <param name="t">The t index of the linear curve.</param>
        /// <returns>Returns a <see cref="ValueTuple{T1, T2}"/> representing a point on the linear curve at the t index.</returns>

        /// <acknowledgment>
        /// http://paulbourke.net/geometry/bezier/index.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Linear(
            double aX, double aY,
            double bX, double bY,
            double t)
            => ((1d - t) * aX + t * bX,
                (1d - t) * aY + t * bY);

        /// <summary>
        /// Two control point 3D Linear interpolation for ranges from 0 to 1, start to end of curve.
        /// </summary>
        /// <param name="aX">The first anchor point x value.</param>
        /// <param name="aY">The first anchor point y value.</param>
        /// <param name="aZ">The first anchor point z value.</param>
        /// <param name="bX">The second anchor point x value.</param>
        /// <param name="bY">The second anchor point y value.</param>
        /// <param name="bZ">The second anchor point z value.</param>
        /// <param name="t">The t index of the linear curve.</param>
        /// <returns>Returns a <see cref="ValueTuple{T1, T2, T3}"/> representing a point on the linear curve at the t index.</returns>

        /// <acknowledgment>
        /// http://paulbourke.net/geometry/bezier/index.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Linear(
            double aX, double aY, double aZ,
            double bX, double bY, double bZ,
            double t)
            => ((1d - t) * aX + t * bX,
                (1d - t) * aY + t * bY,
                (1d - t) * aZ + t * bZ);

        /// <summary>
        /// Two control point 2D Linear interpolation for ranges from 0 to 1, start to end of curve.
        /// </summary>
        /// <param name="a">The first anchor point.</param>
        /// <param name="b">The second anchor point value.</param>
        /// <param name="t">The t index of the linear curve.</param>
        /// <returns>Returns a <see cref="Point2D"/> representing a point on the linear curve at the t index.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Linear(Point2D a, Point2D b, double t)
            => new Point2D(Linear(a.X, a.Y, b.X, b.Y, t));

        /// <summary>
        /// Two control point 3D Linear interpolation for ranges from 0 to 1, start to end of curve.
        /// </summary>
        /// <param name="a">The first anchor point.</param>
        /// <param name="b">The second anchor point value.</param>
        /// <param name="t">The t index of the linear curve.</param>
        /// <returns>Returns a <see cref="Point3D"/> representing a point on the linear curve at the t index.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D Linear(Point3D a, Point3D b, double t)
            => new Point3D(Linear(a.X, a.Y, a.Z, b.X, b.Y, b.Z, t));
        #endregion Linear Interpolation

        #region Normalized Linear Interpolation
        /// <summary>
        /// The nlerp.
        /// </summary>
        /// <param name="aV">The start.</param>
        /// <param name="bV">The end.</param>
        /// <param name="t">The percent.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        /// <acknowledgment>
        /// https://keithmaggio.wordpress.com/2011/02/15/math-magician-lerp-slerp-and-nlerp/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Nlerp(double aV, double bV, double t)
            => Normalize1D(Linear(aV, bV, t));

        /// <summary>
        /// The nlerp.
        /// </summary>
        /// <param name="aX">The startX.</param>
        /// <param name="aY">The startY.</param>
        /// <param name="bX">The endX.</param>
        /// <param name="bY">The endY.</param>
        /// <param name="t">The percent.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        /// <acknowledgment>
        /// https://keithmaggio.wordpress.com/2011/02/15/math-magician-lerp-slerp-and-nlerp/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Nlerp(double aX, double aY, double bX, double bY, double t)
        {
            var (X, Y) = Linear(aX, aY, bX, bY, t);
            return Normalize2D(X, Y);
        }

        /// <summary>
        /// The nlerp.
        /// </summary>
        /// <param name="aX">The startX.</param>
        /// <param name="aY">The startY.</param>
        /// <param name="aZ">The startZ.</param>
        /// <param name="bX">The endX.</param>
        /// <param name="bY">The endY.</param>
        /// <param name="bZ">The endZ.</param>
        /// <param name="t">The percent.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <acknowledgment>
        /// https://keithmaggio.wordpress.com/2011/02/15/math-magician-lerp-slerp-and-nlerp/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Nlerp(double aX, double aY, double aZ, double bX, double bY, double bZ, double t)
        {
            var (X, Y, Z) = Linear(aX, aY, aZ, bX, bY, bZ, t);
            return Normalize3D(X, Y, Z);
        }

        /// <summary>
        /// The nlerp.
        /// </summary>
        /// <param name="a">The start.</param>
        /// <param name="b">The end.</param>
        /// <param name="t">The percent.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        /// <acknowledgment>
        /// https://keithmaggio.wordpress.com/2011/02/15/math-magician-lerp-slerp-and-nlerp/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Nlerp(Point2D a, Point2D b, double t)
            => (Point2D)((Vector2D)Linear(a, b, t)).Normalize();

        /// <summary>
        /// The nlerp.
        /// </summary>
        /// <param name="a">The start.</param>
        /// <param name="b">The end.</param>
        /// <param name="t">The percent.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        /// <acknowledgment>
        /// https://keithmaggio.wordpress.com/2011/02/15/math-magician-lerp-slerp-and-nlerp/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D Nlerp(Point3D a, Point3D b, double t)
            => (Point3D)((Vector3D)Linear(a, b, t)).Normalize();
        #endregion Normalized Linear Interpolation

        #region Quaternian S Linear Interpolation
        /// <summary>
        /// The slerp.
        /// </summary>
        /// <param name="a">The start.</param>
        /// <param name="b">The end.</param>
        /// <param name="percent">The percent.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        /// <acknowledgment>
        /// https://keithmaggio.wordpress.com/2011/02/15/math-magician-lerp-slerp-and-nlerp/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Slerp(Point2D a, Point2D b, double percent)
        {
            // Dot product - the cosine of the angle between 2 vectors.
            // Clamp it to be in the range of Acos()
            // This may be unnecessary, but floating point
            // precision can be a fickle mistress.
            var dot = Maths.Clamp(Primitives.DotProduct(a, b), -1d, 1d);

            // Acos(dot) returns the angle between start and end,
            // And multiplying that by percent returns the angle between
            // start and the final result.
            var theta = Acos(dot) * percent;
            var RelativeVec = b - a * dot;

            // Orthonormal basis
            RelativeVec.Normalize();

            // The final result.
            return (a * Cos(theta)) + (RelativeVec * Sin(theta));
        }

        /// <summary>
        /// The slerp.
        /// </summary>
        /// <param name="a">The start.</param>
        /// <param name="b">The end.</param>
        /// <param name="percent">The percent.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>
        /// <acknowledgment>
        /// https://keithmaggio.wordpress.com/2011/02/15/math-magician-lerp-slerp-and-nlerp/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D Slerp(Point3D a, Point3D b, double percent)
        {
            // Dot product - the cosine of the angle between 2 vectors.
            // Clamp it to be in the range of Acos()
            // This may be unnecessary, but floating point
            // precision can be a fickle mistress.
            var dot = Maths.Clamp(DotProduct(a.X, a.Y, a.Y, b.X, b.Y, b.Z), -1d, 1d);

            // Acos(dot) returns the angle between start and end,
            // And multiplying that by percent returns the angle between
            // start and the final result.
            var theta = Acos(dot) * percent;
            var RelativeVec = b - a * dot;

            // Orthonormal basis
            Normalize3D(RelativeVec.X, RelativeVec.Y, RelativeVec.Z);

            // The final result.
            return (a * Cos(theta)) + (RelativeVec * Sin(theta));
        }
        #endregion Quaternian S Linear Interpolation

        #region Curve Interpolation
        /// <summary>
        /// The curve.
        /// </summary>
        /// <param name="vCurve">The vCurve.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="double"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Curve(Polynomial vCurve, double t)
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
        /// <param name="xCurve">The xCurve.</param>
        /// <param name="yCurve">The yCurve.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double x, double y) Curve(Polynomial xCurve, Polynomial yCurve, double t)
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
        /// <param name="xCurve">The xCurve.</param>
        /// <param name="yCurve">The yCurve.</param>
        /// <param name="zCurve">The zCurve.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double x, double y, double z) Curve(Polynomial xCurve, Polynomial yCurve, Polynomial zCurve, double t)
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
        /// <param name="aV">The first parameter.</param>
        /// <param name="bV">The second parameter.</param>
        /// <param name="cV">The third parameter.</param>
        /// <param name="t">The time parameter.</param>
        /// <returns>Returns a value interpolated from a Quadratic Bézier.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/geometry/bezier/index.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuadraticBezier(
            double aV,
            double bV,
            double cV,
            double t)
        {
            // The inverse of t.
            var ti = 1d - t;

            return aV * ti * ti + 2d * bV * ti * t + cV * t * t;
        }

        /// <summary>
        /// Three control point Bézier interpolation mu ranges from 0 to 1, start to end of the curve.
        /// </summary>
        /// <param name="aX">The x-component of the first parameter.</param>
        /// <param name="aY">The y-component of the first parameter.</param>
        /// <param name="bX">The x-component of the second parameter.</param>
        /// <param name="bY">The y-component of the second parameter.</param>
        /// <param name="cX">The x-component of the third parameter.</param>
        /// <param name="cY">The y component of the third parameter.</param>
        /// <param name="t">The time parameter.</param>
        /// <returns>Returns a point at t position of a Quadratic Bézier curve.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/geometry/bezier/index.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) QuadraticBezier(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double t)
        {
            // The inverse of t.
            var ti = 1d - t;

            // The inverse of t squared.
            var ti2 = ti * ti;

            // The t squared.
            var t2 = t * t;

            return (
                aX * ti2 + 2d * bX * ti * t + cX * t2,
                aY * ti2 + 2d * bY * ti * t + cY * t2
                );
        }

        /// <summary>
        /// Three control point Bézier interpolation mu ranges from 0 to 1, start to end of the curve.
        /// </summary>
        /// <param name="x0">The x-component of the first point on a Bézier curve.</param>
        /// <param name="y0">The y-component of the first point on a Bézier curve.</param>
        /// <param name="z0">The z-component of the first point on a Bézier curve.</param>
        /// <param name="x1">The x-component of the handle point of the Bézier curve.</param>
        /// <param name="y1">The y-component of the handle point of the Bézier curve.</param>
        /// <param name="z1">The z-component of the handle point of the Bézier curve.</param>
        /// <param name="x2">The x-component of the last point on the Bézier curve.</param>
        /// <param name="y2">The y-component of the last point on the Bézier curve.</param>
        /// <param name="z2">The z-component of the last point on the Bézier curve.</param>
        /// <param name="t">The time parameter of the Bézier curve.</param>
        /// <returns>Returns a point on the Bézier curve.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/geometry/bezier/index.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) QuadraticBezier(
            double x0, double y0, double z0,
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double t)
        {
            // The inverse of t.
            var ti = 1d - t;

            // The inverse of t squared.
            var ti2 = ti * ti;

            // The t squared.
            var t2 = t * t;

            return (
                x0 * ti2 + 2d * x1 * ti * t + x2 * t2,
                y0 * ti2 + 2d * y1 * ti * t + y2 * t2,
                z0 * ti2 + 2d * z1 * ti * t + z2 * t2);
        }
        #endregion Quadratic Bézier Interpolation

        #region Cubic Interpolation
        /// <summary>
        /// The cubic.
        /// </summary>
        /// <param name="aV">The aV.</param>
        /// <param name="bV">The bV.</param>
        /// <param name="cV">The cV.</param>
        /// <param name="dV">The dV.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="double"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cubic(
            double aV,
            double bV,
            double cV,
            double dV,
            double t)
        {
            var t2 = t * t;
            var a0 = dV - cV - aV + bV;
            return a0 * t * t2 + (aV - bV - a0) * t2 + (cV - aV) * t + bV;
        }

        /// <summary>
        /// The cubic.
        /// </summary>
        /// <param name="aX">The aX.</param>
        /// <param name="aY">The aY.</param>
        /// <param name="bX">The bX.</param>
        /// <param name="bY">The bY.</param>
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        /// <param name="dX">The dX.</param>
        /// <param name="dY">The dY.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Cubic(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double dX, double dY,
            double t)
        {
            var t2 = t * t;
            var aX0 = dX - cX - aX + bX;
            var aY0 = dY - cY - aY + bY;
            return (
                aX0 * t * t2 + (aX - bX - aX0) * t2 + (cX - aX) * t + bX,
                aY0 * t * t2 + (aY - bY - aY0) * t2 + (cY - aY) * t + bY);
        }

        /// <summary>
        /// The cubic.
        /// </summary>
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
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Cubic(
            double aX, double aY, double aZ,
            double bX, double bY, double bZ,
            double cX, double cY, double cZ,
            double dX, double dY, double dZ,
            double t)
        {
            var t2 = t * t;
            var aX0 = dX - cX - aX + bX;
            var aY0 = dY - cY - aY + bY;
            var aZ0 = dZ - cZ - aZ + bZ;
            return (
                aX0 * t * t2 + (aX - bX - aX0) * t2 + (cX - aX) * t + bX,
                aY0 * t * t2 + (aY - bY - aY0) * t2 + (cY - aY) * t + bY,
                aZ0 * t * t2 + (aZ - bZ - aZ0) * t2 + (cZ - aZ) * t + bZ);
        }
        #endregion Cubic Interpolation

        #region Cubic Bézier Interpolation
        /// <summary>
        /// Four control point 1D Quadratic Bézier interpolation for ranges from 0 to 1, start to end of curve.
        /// </summary>
        /// <param name="v0">The first anchor value.</param>
        /// <param name="v1">The first handle value.</param>
        /// <param name="v2">The second handle value.</param>
        /// <param name="v3">The second anchor value.</param>
        /// <param name="t">The t index of the curve.</param>
        /// <returns>Returns a <see cref="double"/> representing a value on the Bézier curve at the t index.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/geometry/bezier/index.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CubicBezier(
            double v0,
            double v1,
            double v2,
            double v3,
            double t)
        {
            // The inverse of t.
            var ti = 1d - t;

            // The inverse of t cubed.
            var ti3 = ti * ti * ti;

            // The t cubed.
            var t3 = t * t * t;

            return ti3 * v0 + 3d * t * ti * ti * v1 + 3d * t * t * ti * v2 + t3 * v3;
        }

        /// <summary>
        /// Four control point 2D Quadratic Bézier interpolation for ranges from 0 to 1, start to end of curve.
        /// </summary>
        /// <param name="x0">The first anchor point x value.</param>
        /// <param name="y0">The first anchor point y value.</param>
        /// <param name="x1">The first handle point x value.</param>
        /// <param name="y1">The first handle point y value.</param>
        /// <param name="x2">The second handle point x value.</param>
        /// <param name="y2">The second handle point y value.</param>
        /// <param name="x3">The second anchor point x value.</param>
        /// <param name="y3">The second anchor point y value.</param>
        /// <param name="t">The t index of the curve.</param>
        /// <returns>Returns a <see cref="ValueTuple{T1, T2}"/> representing a point on the Bézier curve at the t index.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/geometry/bezier/index.html
        /// https://github.com/burningmime/curves
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) CubicBezier(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double t)
        {
            // The inverse of t.
            var ti = 1d - t;

            // The inverse of t cubed.
            var ti3 = ti * ti * ti;

            // The t cubed.
            var t3 = t * t * t;

            return (
                X: ti3 * x0 + 3d * ti * ti * t * x1 + 3d * ti * t * t * x2 + t3 * x3,
                Y: ti3 * y0 + 3d * ti * ti * t * y1 + 3d * ti * t * t * y2 + t3 * y3
                );

            //return (Point2D)(ti3 * A + 3d * ti * ti * t * B + 3d * ti * t * t * C + t3 * D);
        }

        /// <summary>
        /// Four control point 2D Quadratic Bézier interpolation for ranges from 0 to 1, start to end of curve.
        /// </summary>
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
        /// <param name="t">The t index of the curve.</param>
        /// <returns>Returns a <see cref="ValueTuple{T1, T2, T3}"/> representing a point on the Bézier curve at the t index.</returns>
        /// <acknowledgment>
        /// http://paulbourke.net/geometry/bezier/index.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) CubicBezier(
            double x0, double y0, double z0,
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double x3, double y3, double z3,
            double t)
        {
            // The inverse of t.
            var ti = 1d - t;

            // The inverse of t cubed.
            var ti3 = ti * ti * ti;

            // The t cubed.
            var t3 = t * t * t;

            return (
                ti3 * x0 + 3d * t * ti * ti * x1 + 3d * t * t * ti * x2 + t3 * x3,
                ti3 * y0 + 3d * t * ti * ti * y1 + 3d * t * t * ti * y2 + t3 * y3,
                ti3 * z0 + 3d * t * ti * ti * z1 + 3d * t * t * ti * z2 + t3 * z3
                );
        }
        #endregion Cubic Bézier Interpolation

        #region N Bézier Interpolation
        /// <summary>
        /// General Bézier curve Number of control points is n+1 0 less than or equal to mu less than 1
        /// IMPORTANT, the last point is not computed.
        /// </summary>
        /// <param name="points"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <acknowledgment>
        /// http://paulbourke.net/geometry/bezier/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D CubicBezierSpline(List<Point2D> points, double t)
        {
            var n = points.Count - 1;
            int kn;
            int nn;
            int nkn;

            double blend;
            double muk = 1;
            var munk = Pow(1 - t, n);

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
                    b.X + points[k].X * blend,
                    b.Y + points[k].Y * blend
                );
            }

            return b;
        }
        #endregion N Bézier Interpolation

        #region Catmull-Rom Spline Interpolation
        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// </summary>
        /// <param name="aV">The first position in the interpolation.</param>
        /// <param name="bV">The second position in the interpolation.</param>
        /// <param name="cV">The third position in the interpolation.</param>
        /// <param name="dV">The fourth position in the interpolation.</param>
        /// <param name="t">Weighting factor.</param>
        /// <returns>A position that is the result of the Catmull-Rom interpolation.</returns>
        /// <acknowledgment>
        /// http://www.mvps.org/directx/articles/catmull/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CatmullRom(
            double aV,
            double bV,
            double cV,
            double dV,
            double t)
        {
            var t2 = t * t;
            var t3 = t2 * t;
            return 
                0.5d * (2d * bV
                + (cV - aV) * t
                + (2d * aV - 5d * bV + 4d * cV - dV) * t2
                + (3d * bV - aV - 3.0d * cV + dV) * t3);
        }

        /// <summary>
        /// Calculates interpolated point between two points using Catmull-Rom Spline
        /// </summary>
        /// <param name="aX">First Point</param>
        /// <param name="aY">First Point</param>
        /// <param name="bX">Second Point</param>
        /// <param name="bY">Second Point</param>
        /// <param name="cX">Third Point</param>
        /// <param name="cY">Third Point</param>
        /// <param name="dX">Fourth Point</param>
        /// <param name="dY">Fourth Point</param>
        /// <param name="t">
        /// Normalized distance between second and third point
        /// where the spline point will be calculated
        /// </param>
        /// <returns>
        /// Calculated Spline Point
        /// </returns>
        /// <remarks>
        /// Points calculated exist on the spline between points two and three.
        /// </remarks>
        /// <acknowledgment>
        /// From: http://tehc0dez.blogspot.com/2010/04/nice-curves-catmullrom-spline-in-c.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) CatmullRom(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double dX, double dY,
            double t)
        {
            var t2 = t * t;
            var t3 = t2 * t;
            return (
                0.5d * (2d * bX
                + (-aX + cX) * t
                + (2d * aX - 5d * bX + 4d * cX - dX) * t2
                + (-aX + 3d * bX - 3d * cX + dX) * t3),
                0.5d * (2d * bY
                + (-aY + cY) * t
                + (2d * aY - 5d * bY + 4d * cY - dY) * t2
                + (-aY + 3d * bY - 3d * cY + dY) * t3));
        }

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// </summary>
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
        /// <param name="t">Weighting factor.</param>
        /// <returns>A position that is the result of the Catmull-Rom interpolation.</returns>
        /// <acknowledgment>
        /// http://www.mvps.org/directx/articles/catmull/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) CatmullRom(
            double aX, double aY, double aZ,
            double bX, double bY, double bZ,
            double cX, double cY, double cZ,
            double dX, double dY, double dZ,
            double t)
        {
            var t2 = t * t;
            var t3 = t2 * t;
            return (
                0.5d * (2d * bX
                + (cX - aX) * t
                + (2d * aX - 5d * bX + 4d * cX - dX) * t2
                + (3d * bX - aX - 3d * cX + dX) * t3),
                0.5d * (2d * bX
                + (cY - aY) * t
                + (2d * aY - 5d * bY + 4d * cY - dY) * t2
                + (3d * bY - aY - 3d * cY + dY) * t3),
                0.5d * (2d * bZ
                + (cZ - aZ) * t
                + (2d * aZ - 5d * bZ + 4d * cZ - dZ) * t2
                + (3d * bZ - aZ - 3d * cZ + dZ) * t3));
        }

        /// <summary>
        /// The catmull rom.
        /// </summary>
        /// <param name="tangentA">The tangentA.</param>
        /// <param name="positionA">The positionA.</param>
        /// <param name="positionB">The positionB.</param>
        /// <param name="tangentB">The tangentB.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="Point2D"/>.</returns>

        /// <acknowledgment>
        /// From: http://tehc0dez.blogspot.com/2010/04/nice-curves-catmullrom-spline-in-c.html
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D CatmullRom(
            Point2D tangentA,
            Point2D positionA,
            Point2D positionB,
            Point2D tangentB,
            double t)
        {
            var t2 = t * t;
            var t3 = t2 * t;
            return new Point2D(
                0.5d * (2d * positionA.X
                + (-tangentA.X + positionB.X) * t + (2d * tangentA.X - 5d * positionA.X
                + 4d * positionB.X - tangentB.X) * t2
                + (-tangentA.X + 3d * positionA.X - 3d * positionB.X + tangentB.X) * t3),
                0.5d * (2d * positionA.Y
                + (-tangentA.Y + positionB.Y) * t + (2d * tangentA.Y - 5d * positionA.Y
                + 4d * positionB.Y - tangentB.Y) * t2
                + (-tangentA.Y + 3d * positionA.Y - 3d * positionB.Y + tangentB.Y) * t3)
            );
        }
        #endregion Catmull-Rom Spline Interpolation

        #region Hermite Interpolation
        /// <summary>
        /// The hermite.
        /// </summary>
        /// <param name="aV">The aV.</param>
        /// <param name="bV">The bV.</param>
        /// <param name="cV">The cV.</param>
        /// <param name="dV">The dV.</param>
        /// <param name="t">The t time index parameter.</param>
        /// <param name="tension">1 is high, 0 normal, -1 is low</param>
        /// <param name="bias">0 is even,positive is towards first segment, negative towards the other</param>
        /// <returns>The <see cref="double"/>.</returns>

        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Hermite(
            double aV,
            double bV,
            double cV,
            double dV,
            double t, double tension = 0d, double bias = 0d)
        {
            var t2 = t * t;
            var t3 = t2 * t;

            var m0 = (bV - aV) * (1d + bias) * (1d - tension) * 0.5d;
            m0 += (cV - bV) * (1d - bias) * (1d - tension) * 0.5d;

            var m1 = (cV - bV) * (1d + bias) * (1d - tension) * 0.5d;
            m1 += (dV - cV) * (1d - bias) * (1d - tension) * 0.5d;

            return (2d * t3 - 3d * t2 + 1d) * bV + (t3 - 2d * t2 + t) * m0 + (t3 - t2) * m1 + (-2d * t3 + 3d * t2) * cV;
        }

        /// <summary>
        /// The hermite.
        /// </summary>
        /// <param name="aX">The aX.</param>
        /// <param name="aY">The aY.</param>
        /// <param name="bX">The bX.</param>
        /// <param name="bY">The bY.</param>
        /// <param name="cX">The cX.</param>
        /// <param name="cY">The cY.</param>
        /// <param name="dX">The dX.</param>
        /// <param name="dY">The dY.</param>
        /// <param name="t">The t time index parameter.</param>
        /// <param name="tension">1 is high, 0 normal, -1 is low</param>
        /// <param name="bias">0 is even,positive is towards first segment, negative towards the other</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>

        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Hermite(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double dX, double dY,
            double t, double tension = 0d, double bias = 0d)
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

            var a0 = 2d * t3 - 3d * t2 + 1d;
            var a1 = t3 - 2d * t2 + t;
            var a2 = t3 - t2;
            var a3 = -2d * t3 + 3d * t2;

            return (
                a0 * bX + a1 * mX0 + a2 * mX1 + a3 * cX,
                a0 * bY + a1 * mY0 + a2 * mY1 + a3 * cY);
        }

        /// <summary>
        /// The hermite.
        /// </summary>
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
        /// <param name="t">The t time index parameter.</param>
        /// <param name="tension">1 is high, 0 normal, -1 is low</param>
        /// <param name="bias">0 is even, positive is towards first segment, negative towards the other</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>

        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Hermite(
            double aX, double aY, double aZ,
            double bX, double bY, double bZ,
            double cX, double cY, double cZ,
            double dX, double dY, double dZ,
            double t, double tension = 0d, double bias = 0d)
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

            var a0 = 2d * t3 - 3d * t2 + 1d;
            var a1 = t3 - 2d * t2 + t;
            var a2 = t3 - t2;
            var a3 = -2d * t3 + 3d * t2;

            return (
                a0 * bX + a1 * mX0 + a2 * mX1 + a3 * cX,
                a0 * bY + a1 * mY0 + a2 * mY1 + a3 * cY,
                a0 * bZ + a1 * mZ0 + a2 * mZ1 + a3 * cZ);
        }
        #endregion Hermite Interpolation

        #region Circle Interpolation
        /// <summary>
        /// Interpolates the Arc.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r">Radius of circle.</param>
        /// <param name="startAngle">The angle to start the arc.</param>
        /// <param name="sweepAngle">The difference of the angle to where the arc should end.</param>
        /// <param name="t">Theta of interpolation.</param>
        /// <returns>Interpolated point at theta.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) CircularArc(
            double cX, double cY,
            double r,
            double startAngle, double sweepAngle,
            double t)
            => Circle(cX, cY, r, startAngle + (sweepAngle * t));

        /// <summary>
        /// Interpolate a point on a circle, converting from unit iteration, to Pi radians.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r">Radius of circle.</param>
        /// <param name="t">Theta of interpolation.</param>
        /// <returns>Interpolated point at theta.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) UnitCircle(
        double cX, double cY,
        double r,
        double t)
            => Circle(cX, cY, r, Tau * t);

        /// <summary>
        /// Interpolate a point on a circle, applying translation to equation of circle at origin.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r">Radius of circle.</param>
        /// <param name="t">Theta of interpolation.</param>
        /// <returns>Interpolated point at theta.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Circle(
            double cX, double cY,
            double r,
            double t) => (
                cX + (Cos(t) * r),
                cY + (Sin(t) * r));
        #endregion Circle Interpolation

        #region Ellipse Interpolation
        /// <summary>
        /// Interpolates the unrotated elliptical Arc.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="startAngle">The angle to start the arc.</param>
        /// <param name="sweepAngle">The difference of the angle to where the arc should end.</param>
        /// <param name="t">Theta of interpolation.</param>
        /// <returns>Interpolated point at theta.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) EllipticalArc(
            double cX, double cY,
            double r1, double r2,
            double startAngle, double sweepAngle,
            double t)
        {
            var phi = startAngle + (sweepAngle * t);
            var theta = phi % PI;

            var tanAngle = Abs(Tan(theta));
            var x = Sqrt(r1 * r1 * (r2 * r2) / ((r2 * r2) + r1 * r1 * (tanAngle * tanAngle)));
            var y = x * tanAngle;

            return (theta >= 0d) && (theta < 90d.ToRadians())
                ? (cX + x, cY + y)
                : (theta >= 90d.ToRadians()) && (theta < 180d.ToRadians())
                ? (cX - x, cY + y)
                : (theta >= 180d.ToRadians()) && (theta < 270d.ToRadians()) ? (cX - x, cY - y) : (cX + x, cY - y);
        }

        /// <summary>
        /// Interpolates the Elliptical Arc, corrected for Polar coordinates.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <param name="startAngle">The angle to start the arc.</param>
        /// <param name="sweepAngle">The difference of the angle to where the arc should end.</param>
        /// <param name="t">Theta of interpolation.</param>
        /// <returns>Interpolated point at theta.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) EllipticalArc(
            double cX, double cY,
            double r1, double r2,
            double angle,
            double startAngle, double sweepAngle,
            double t)
            => PolarEllipse(cX, cY, r1, r2, angle, startAngle + (sweepAngle * t));

        /// <summary>
        /// Interpolates the Elliptical Arc, corrected for Polar coordinates.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="cosAngle">Horizontal rotation transform of the Ellipse.</param>
        /// <param name="sinAngle">Vertical rotation transform of the Ellipse.</param>
        /// <param name="startAngle">The angle to start the arc.</param>
        /// <param name="sweepAngle">The difference of the angle to where the arc should end.</param>
        /// <param name="t">Theta of interpolation.</param>
        /// <returns>Interpolated point at theta.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) EllipticalArc(
            double cX, double cY,
            double r1, double r2,
            double cosAngle, double sinAngle,
            double startAngle, double sweepAngle,
            double t)
            => PolarEllipse(cX, cY, r1, r2, cosAngle, sinAngle, startAngle + (sweepAngle * t));

        /// <summary>
        /// Interpolate a point on an Ellipse with Polar correction using a range from 0 to 1 for unit interpolation.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <param name="t">Theta of interpolation.</param>
        /// <returns>Interpolated point at theta adjusted to Polar angles.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) UnitPolarEllipse(
            double cX, double cY,
            double r1, double r2,
            double angle,
            double t)
           => PolarEllipse(cX, cY, r1, r2, angle, Tau * t);

        /// <summary>
        /// Interpolate a point on an Ellipse with Polar correction using a range from 0 to 1 for unit interpolation.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="cosAngle">Horizontal rotation transform of the Ellipse.</param>
        /// <param name="sinAngle">Vertical rotation transform of the Ellipse.</param>
        /// <param name="t">Theta of interpolation.</param>
        /// <returns>Interpolated point at theta adjusted to Polar angles.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) UnitPolarEllipse(
            double cX, double cY,
            double r1, double r2,
            double cosAngle, double sinAngle,
            double t)
           => PolarEllipse(cX, cY, r1, r2, cosAngle, sinAngle, Tau * t);

        /// <summary>
        /// Interpolate a point on an Ellipse with Polar correction.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <param name="t">Theta of interpolation.</param>
        /// <returns>Interpolated point at theta adjusted to Polar angles.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) PolarEllipse(
            double cX, double cY,
            double r1, double r2,
            double angle,
            double t)
           => Ellipse(cX, cY, r1, r2, angle, EllipticalPolarAngle(t, r1, r2));

        /// <summary>
        /// Interpolate a point on an Ellipse with Polar correction.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="cosAngle">Horizontal rotation transform of the Ellipse.</param>
        /// <param name="sinAngle">Vertical rotation transform of the Ellipse.</param>
        /// <param name="t">Theta of interpolation.</param>
        /// <returns>Interpolated point at theta adjusted to Polar angles.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) PolarEllipse(
            double cX, double cY,
            double r1, double r2,
            double cosAngle, double sinAngle,
            double t)
            => Ellipse(cX, cY, r1, r2, cosAngle, sinAngle, EllipticalPolarAngle(t, r1, r2));

        /// <summary>
        /// Interpolate a point on an Ellipse.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="angle">Angle of rotation of Ellipse about it's center.</param>
        /// <param name="t">Theta of interpolation.</param>
        /// <returns>Interpolated point at theta.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Ellipse(
            double cX, double cY,
            double r1, double r2,
            double angle,
            double t)
            => Ellipse(cX, cY, r1, r2, Cos(angle), Sin(angle), Cos(t), Sin(t));

        /// <summary>
        /// Interpolate a point on an Ellipse.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="cosAngle">Horizontal rotation transform of the Ellipse.</param>
        /// <param name="sinAngle">Vertical rotation transform of the Ellipse.</param>
        /// <param name="t">Theta of interpolation.</param>
        /// <returns>Interpolated point at theta.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Ellipse(
            double cX, double cY,
            double r1, double r2,
            double cosAngle, double sinAngle,
            double t)
            => Ellipse(cX, cY, r1, r2, cosAngle, sinAngle, Cos(t), Sin(t));

        /// <summary>
        /// Interpolate a point on an Ellipse.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r1">The first radius of the Ellipse.</param>
        /// <param name="r2">The second radius of the Ellipse.</param>
        /// <param name="cosAngle">Horizontal rotation transform of the Ellipse.</param>
        /// <param name="sinAngle">Vertical rotation transform of the Ellipse.</param>
        /// <param name="cosTheta">Theta cosine of interpolation.</param>
        /// <param name="sinTheta">Theta sine of interpolation.</param>
        /// <returns>Interpolated point at theta.</returns>
        /// <acknowledgment>
        /// http://www.vbforums.com/showthread.php?686351-RESOLVED-Elliptical-orbit
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Ellipse(
            double cX, double cY,
            double r1, double r2,
            double cosAngle, double sinAngle,
            double cosTheta, double sinTheta)
        {
            // Ellipse equation for an ellipse at origin.
            var u = r1 * cosTheta;
            var v = -(r2 * sinTheta);

            // Apply the rotation transformation and translate to new center.
            return (
                cX + (u * cosAngle + v * sinAngle),
                cY + (u * sinAngle - v * cosAngle));
        }
        #endregion Ellipse Interpolation

        #region Cosine Interpolation
        /// <summary>
        /// The cosine.
        /// </summary>
        /// <param name="aV">The aV.</param>
        /// <param name="bV">The bV.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="double"/>.</returns>

        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cosine(
            double aV,
            double bV,
            double t)
        {
            var mu2 = 0.5d * (1d - Cos(t * PI));
            return aV * (1d - mu2) + bV * mu2;
        }

        /// <summary>
        /// The cosine.
        /// </summary>
        /// <param name="aX">The aX.</param>
        /// <param name="aY">The aY.</param>
        /// <param name="bX">The bX.</param>
        /// <param name="bY">The bY.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2}"/>.</returns>

        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Cosine(
            double aX, double aY,
            double bX, double bY,
            double t)
        {
            var mu2 = 0.5d * (1d - Cos(t * PI));
            return (aX * (1d - mu2) + bX * mu2,
                    aY * (1d - mu2) + bY * mu2);
        }

        /// <summary>
        /// The cosine.
        /// </summary>
        /// <param name="aX">The aX.</param>
        /// <param name="aY">The aY.</param>
        /// <param name="aZ">The aZ.</param>
        /// <param name="bX">The bX.</param>
        /// <param name="bY">The bY.</param>
        /// <param name="bZ">The bZ.</param>
        /// <param name="t">The t.</param>
        /// <returns>The <see cref="ValueTuple{T1, T2, T3}"/>.</returns>

        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Cosine(
            double aX, double aY, double aZ,
            double bX, double bY, double bZ,
            double t)
        {
            var mu2 = 0.5d * (1d - Cos(t * PI));
            return (
                aX * (1d - mu2) + bX * mu2,
                aY * (1d - mu2) + bY * mu2,
                aZ * (1d - mu2) + bZ * mu2);
        }
        #endregion Cosine Interpolation

        #region Sine Interpolation
        /// <summary>
        /// Interpolate a sine wave.
        /// </summary>
        /// <param name="v1">The first parameter.</param>
        /// <param name="v2">The second Parameter.</param>
        /// <param name="t">The time parameter.</param>
        /// <returns>Returns a value of a Sine wave at t.</returns>

        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sine(
            double v1,
            double v2,
            double t)
        {
            var mu2 = (1d - Sin(t * PI)) * 0.5d;
            return v1 * (1d - mu2) + v2 * mu2;
        }

        /// <summary>
        /// Interpolates a sine wave in 2 dimensions.
        /// </summary>
        /// <param name="x1">The first x component.</param>
        /// <param name="y1">The first y-component.</param>
        /// <param name="x2">The second x-component.</param>
        /// <param name="y2">The second y-component.</param>
        /// <param name="t">The t parameter.</param>
        /// <returns>Returns a point interpolated of a Sine wave.</returns>

        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Sine(
            double x1, double y1,
            double x2, double y2,
            double t)
        {
            var mu2 = (1d - Sin(t * PI)) * 0.5d;
            return (
                x1 * (1d - mu2) + x2 * mu2,
                y1 * (1d - mu2) + y2 * mu2);
        }

        /// <summary>
        /// Interpolates a Sine wave at t parameter.
        /// </summary>
        /// <param name="x1">The x-component of the first parameter.</param>
        /// <param name="y1">The y-component of the first parameter.</param>
        /// <param name="z1">The z-component of the first parameter.</param>
        /// <param name="x2">The x-component of the second parameter.</param>
        /// <param name="y2">The y-component of the second parameter.</param>
        /// <param name="z2">The z-component of the second parameter.</param>
        /// <param name="t">The time parameter.</param>
        /// <returns>Returns a point in 2 dimensional space interpolated from a sine wave.</returns>

        /// <acknowledgment>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Sine(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double t)
        {
            var mu2 = (1d - Sin(t * PI)) * 0.5d;
            return (
                x1 * (1d - mu2) + x2 * mu2,
                y1 * (1d - mu2) + y2 * mu2,
                z1 * (1d - mu2) + z2 * mu2);
        }
        #endregion Sine Interpolation

        #region Rectangle
        /// <summary>
        /// Rotates the points of the corners of a rectangle about the fulcrum point.
        /// </summary>
        /// <param name="x">The x-component of the top left corner of the rectangle.</param>
        /// <param name="y">The y-component of the top left corner of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="fulcrumX">The x-component of the rotation fulcrum point.</param>
        /// <param name="fulcrumY">The x-component of the rotation fulcrum point.</param>
        /// <param name="angle">The angle to rotate the points.</param>
        /// <returns>Returns a list of points from the rectangle, rotated about the fulcrum.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> RotatedRectangle(
            double x, double y,
            double width, double height,
            double fulcrumX, double fulcrumY,
            double angle)
            => RotatedRectangle(x, y, width, height, fulcrumX, fulcrumY, Cos(angle), Sin(angle));

        /// <summary>
        /// Rotates the points of the corners of a rectangle about the fulcrum point.
        /// </summary>
        /// <param name="x">The x-component of the top left corner of the rectangle.</param>
        /// <param name="y">The y-component of the top left corner of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="fulcrumX">The x-component of the rotation fulcrum point.</param>
        /// <param name="fulcrumY">The x-component of the rotation fulcrum point.</param>
        /// <param name="cosAngle"></param>
        /// <param name="sinAngle"></param>
        /// <returns>Returns a list of points from the rectangle, rotated about the fulcrum.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> RotatedRectangle(
            double x, double y,
            double width, double height,
            double fulcrumX, double fulcrumY,
            double cosAngle, double sinAngle)
        {
            // ToDo: Figure out how to properly include the location point.
            var points = new List<Point2D>();

            var xaxis = new Point2D(cosAngle, sinAngle);
            var yaxis = new Point2D(-sinAngle, cosAngle);

            // Apply the rotation transformation and translate to new center.
            points.Add(new Point2D(
                fulcrumX + (-width * 0.5d * xaxis.X + -height * 0.5d * xaxis.Y),
                fulcrumY + (-width * 0.5d * yaxis.X + -height * 0.5d * yaxis.Y)
                ));
            points.Add(new Point2D(
                fulcrumX + (width * 0.5d * xaxis.X + -height * 0.5d * xaxis.Y),
                fulcrumY + (width * 0.5d * yaxis.X + -height * 0.5d * yaxis.Y)
                ));
            points.Add(new Point2D(
                fulcrumX + (width * 0.5d * xaxis.X + height * 0.5d * xaxis.Y),
                fulcrumY + (width * 0.5d * yaxis.X + height * 0.5d * yaxis.Y)
                ));
            points.Add(new Point2D(
                fulcrumX + (-width * 0.5d * xaxis.X + height * 0.5d * xaxis.Y),
                fulcrumY + (-width * 0.5d * yaxis.X + height * 0.5d * yaxis.Y)
                ));

            return points;
        }
        #endregion Rectangle

        /// <summary>
        /// Linearly tweens between two cubic Bézier curves, from key1 to key2.
        /// </summary>
        /// <param name="key1">The first cubic Bézier key.</param>
        /// <param name="key2">The second cubic Bézier key.</param>
        /// <param name="t">The t index.</param>
        /// <returns>The <see cref="T:Point2D[]"/>.</returns>
        public static CubicBezier TweenCubic(CubicBezier key1, CubicBezier key2, double t)
            => new CubicBezier(
                 key1.A + t * (key2.A - key1.A),
                 key1.B + t * (key2.B - key1.B),
                 key1.C + t * (key2.C - key1.C),
                 key1.D + t * (key2.D - key1.D)
                );
    }
}
