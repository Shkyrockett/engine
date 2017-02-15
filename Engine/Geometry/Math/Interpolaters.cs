﻿// <copyright file="Interpolaters.cs" company="Shkyrockett" >
//     Copyright (c) 2016 - 2017 Shkyrockett. All rights reserved.
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
    /// Interpolaters
    /// </summary>
    public static class Interpolaters
    {
        /// <summary>
        /// Retrieves a list of points interpolated from a function.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="count">The number of points desired.</param>
        /// <returns></returns>
        public static List<Point2D> Interpolate0to1(Func<double, Point2D> func, int count)
            => new List<Point2D>(
            from i in Enumerable.Range(0, count)
            select func((1d / count) * i));

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
            => Circle(cX, cY, r, (startAngle + (sweepAngle * t)));

        /// <summary>
        /// Interpolate a point on a circle.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r">Radius of circle.</param>
        /// <param name="t">Theta of interpolation.</param>
        /// <returns>Interpolated point at theta.</returns>
        // Convert from unit iteration, to Pi radians.
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) UnitCircle(
        double cX, double cY,
        double r,
        double t)
            => Circle(cX, cY, r, Tau * t);

        /// <summary>
        /// Interpolate a point on a circle.
        /// </summary>
        /// <param name="cX">Center x-coordinate.</param>
        /// <param name="cY">Center y-coordinate.</param>
        /// <param name="r">Radius of circle.</param>
        /// <param name="t">Theta of interpolation.</param>
        /// <returns>Interpolated point at theta.</returns>
        // Apply translation to equation of circle at origin.
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Circle(
            double cX, double cY,
            double r,
            double t) => (
                cX + (Cos(t) * r),
                cY + (Sin(t) * r));

        #endregion

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
            double phi = startAngle + (sweepAngle * t);
            double theta = phi % PI;

            double tanAngle = Abs(Tan(theta));
            double x = Sqrt(((r1 * r1) * (r2 * r2)) / ((r2 * r2) + (r1 * r1) * (tanAngle * tanAngle)));
            double y = x * tanAngle;

            if ((theta >= 0d) && (theta < 90d.ToRadians()))
                return (cX + x, cY + y);
            else if ((theta >= 90d.ToRadians()) && (theta < 180d.ToRadians()))
                return (cX - x, cY + y);
            else if ((theta >= 180d.ToRadians()) && (theta < 270d.ToRadians()))
                return (cX - x, cY - y);
            else
                return (cX + x, cY - y);
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
           => Ellipse(cX, cY, r1, r2, angle, EllipsePolarAngle(t, r1, r2));

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
        {
            // Get the ellipse rotation transform.
            double cosT = Cos(angle);
            double sinT = Sin(angle);

            // Ellipse equation for an ellipse at origin.
            double u = r1 * Cos(t);
            double v = -(r2 * Sin(t));

            // Apply the rotation transformation and translate to new center.
            return (
                cX + (u * cosT + v * sinT),
                cY + (u * sinT - v * cosT));
        }

        #endregion

        #region Catmull-Rom Spline Interpolation

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// </summary>
        /// <param name="v1">The first position in the interpolation.</param>
        /// <param name="v2">The second position in the interpolation.</param>
        /// <param name="v3">The third position in the interpolation.</param>
        /// <param name="v4">The fourth position in the interpolation.</param>
        /// <param name="t">Weighting factor.</param>
        /// <returns>A position that is the result of the Catmull-Rom interpolation.</returns>
        /// <remarks>http://www.mvps.org/directx/articles/catmull/</remarks>
        public static double CatmullRom(
            double v1,
            double v2,
            double v3,
            double v4,
            double t)
        {
            double tSquared = t * t;
            double tCubed = tSquared * t;
            return (
                0.5d * (2d * v2
                + (v3 - v1) * t
                + (2d * v1 - 5d * v2 + 4d * v3 - v4) * tSquared
                + (3d * v2 - v1 - 3.0d * v3 + v4) * tCubed));
        }

        /// <summary>
        /// Calculates interpolated point between two points using Catmull-Rom Spline
        /// </summary>
        /// <param name="t0X">First Point</param>
        /// <param name="t0Y">First Point</param>
        /// <param name="p1X">Second Point</param>
        /// <param name="p1Y">Second Point</param>
        /// <param name="p2X">Third Point</param>
        /// <param name="p2Y">Third Point</param>
        /// <param name="t3X">Fourth Point</param>
        /// <param name="t3Y">Fourth Point</param>
        /// <param name="t">
        /// Normalized distance between second and third point
        /// where the spline point will be calculated
        /// </param>
        /// <returns>
        /// Calculated Spline Point
        /// </returns>
        /// <remarks>
        /// Points calculated exist on the spline between points two and three.
        /// From: http://tehc0dez.blogspot.com/2010/04/nice-curves-catmullrom-spline-in-c.html
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) CatmullRom(
            double t0X, double t0Y,
            double p1X, double p1Y,
            double p2X, double p2Y,
            double t3X, double t3Y,
            double t)
        {
            double tSquared = t * t;
            double tCubed = tSquared * t;
            return (
                0.5d * (2d * p1X
                + (-t0X + p2X) * t
                + (2d * t0X - 5d * p1X + 4d * p2X - t3X) * tSquared
                + (-t0X + 3d * p1X - 3d * p2X + t3X) * tCubed),
                0.5d * (2d * p1Y
                + (-t0Y + p2Y) * t
                + (2d * t0Y - 5d * p1Y + 4d * p2Y - t3Y) * tSquared
                + (-t0Y + 3d * p1Y - 3d * p2Y + t3Y) * tCubed));
        }

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// </summary>
        /// <param name="x1">The first position in the interpolation.</param>
        /// <param name="y1">The first position in the interpolation.</param>
        /// <param name="z1">The first position in the interpolation.</param>
        /// <param name="x2">The second position in the interpolation.</param>
        /// <param name="y2">The second position in the interpolation.</param>
        /// <param name="z2">The second position in the interpolation.</param>
        /// <param name="x3">The third position in the interpolation.</param>
        /// <param name="y3">The third position in the interpolation.</param>
        /// <param name="z3">The third position in the interpolation.</param>
        /// <param name="x4">The fourth position in the interpolation.</param>
        /// <param name="y4">The fourth position in the interpolation.</param>
        /// <param name="z4">The fourth position in the interpolation.</param>
        /// <param name="t">Weighting factor.</param>
        /// <returns>A position that is the result of the Catmull-Rom interpolation.</returns>
        /// <remarks>http://www.mvps.org/directx/articles/catmull/</remarks>
        public static (double X, double Y, double Z) CatmullRom(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double x3, double y3, double z3,
            double x4, double y4, double z4,
            double t)
        {
            double tSquared = t * t;
            double tCubed = tSquared * t;
            return (
                0.5d * (2d * x2
                + (x3 - x1) * t
                + (2d * x1 - 5d * x2 + 4d * x3 - x4) * tSquared
                + (3d * x2 - x1 - 3d * x3 + x4) * tCubed),
                0.5d * (2d * x2
                + (y3 - y1) * t
                + (2d * y1 - 5d * y2 + 4d * y3 - y4) * tSquared
                + (3d * y2 - y1 - 3d * y3 + y4) * tCubed),
                0.5d * (2d * z2
                + (z3 - z1) * t
                + (2d * z1 - 5d * z2 + 4d * z3 - z4) * tSquared
                + (3d * z2 - z1 - 3d * z3 + z4) * tCubed));
        }

        /// <summary>
        /// </summary>
        /// <param name="positionA"></param>
        /// <param name="tangentA"></param>
        /// <param name="positionB"></param>
        /// <param name="tangentB"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>
        /// From: http://tehc0dez.blogspot.com/2010/04/nice-curves-catmullrom-spline-in-c.html
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D CatmullRom(
            Point2D tangentA,
            Point2D positionA,
            Point2D positionB,
            Point2D tangentB,
            double t)
        {
            double tSquared = t * t;
            double tCubed = tSquared * t;
            return new Point2D(
                0.5d * (2d * positionA.X
                + (-tangentA.X + positionB.X) * t + (2d * tangentA.X - 5d * positionA.X
                + 4d * positionB.X - tangentB.X) * tSquared
                + (-tangentA.X + 3d * positionA.X - 3d * positionB.X + tangentB.X) * tCubed),
                0.5d * (2d * positionA.Y
                + (-tangentA.Y + positionB.Y) * t + (2d * tangentA.Y - 5d * positionA.Y
                + 4d * positionB.Y - tangentB.Y) * tSquared
                + (-tangentA.Y + 3d * positionA.Y - 3d * positionB.Y + tangentB.Y) * tCubed)
            );
        }

        #endregion

        #region Cosine Interpolation

        /// <summary>
        ///
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cosine(
            double v1,
            double v2,
            double t)
        {
            double mu2 = (1 - Cos(t * PI)) / 2;
            return v1 * (1 - mu2) + v2 * mu2;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Cosine(
            double x1, double y1,
            double x2, double y2,
            double t)
        {
            double mu2 = (1 - Cos(t * PI)) / 2;
            return (
                x1 * (1 - mu2) + x2 * mu2,
                y1 * (1 - mu2) + y2 * mu2
                );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Cosine(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double t)
        {
            double mu2 = (1 - Cos(t * PI)) / 2;
            return (
                x1 * (1 - mu2) + x2 * mu2,
                y1 * (1 - mu2) + y2 * mu2,
                z1 * (1 - mu2) + z2 * mu2);
        }

        #endregion

        #region Cubic Interpolation

        /// <summary>
        ///
        /// </summary>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cubic(
            double v0,
            double v1,
            double v2,
            double v3,
            double t)
        {
            double a0, a1, a2, a3, mu2;

            mu2 = t * t;
            a0 = v3 - v2 - v0 + v1;
            a1 = v0 - v1 - a0;
            a2 = v2 - v0;
            a3 = v1;

            return (a0 * t * mu2 + a1 * mu2 + a2 * t + a3);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Cubic(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double t)
        {
            double mu2 = t * t;

            double aX0 = x3 - x2 - x0 + x1;
            double aY0 = y3 - y2 - y0 + y1;
            double aX1 = x0 - x1 - aX0;
            double aY1 = y0 - y1 - aY0;
            double aX2 = x2 - x0;
            double aY2 = y2 - y0;

            return (
                aX0 * t * mu2 + aX1 * mu2 + aX2 * t + x1,
                aY0 * t * mu2 + aY1 * mu2 + aY2 * t + y1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="z0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="z3"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Cubic(
            double x0, double y0, double z0,
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double x3, double y3, double z3,
            double t)
        {
            double mu2 = t * t;

            double aX0 = x3 - x2 - x0 + x1;
            double aY0 = y3 - y2 - y0 + y1;
            double aZ0 = z3 - z2 - z0 + z1;
            double aX1 = x0 - x1 - aX0;
            double aY1 = y0 - y1 - aY0;
            double aZ1 = z0 - z1 - aZ0;
            double aX2 = x2 - x0;
            double aY2 = y2 - y0;
            double aZ2 = z2 - z0;

            return (
                aX0 * t * mu2 + aX1 * mu2 + aX2 * t + x1,
                aY0 * t * mu2 + aY1 * mu2 + aY2 * t + y1,
                aZ0 * t * mu2 + aZ1 * mu2 + aZ2 * t + z1);
        }

        #endregion

        #region Cubic Bezier Interpolation

        /// <summary>
        /// Four control point Bezier interpolation mu ranges from 0 to 1, start to end of curve.
        /// </summary>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <history>
        /// </history>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CubicBezier(
            double v0,
            double v1,
            double v2,
            double v3,
            double t)
        {
            double mum1 = 1 - t;
            double mum13 = mum1 * mum1 * mum1;
            double mu3 = t * t * t;

            return (mum13 * v0 + 3 * t * mum1 * mum1 * v1 + 3 * t * t * mum1 * v2 + mu3 * v3);
        }

        /// <summary>
        /// Four control point Bezier interpolation mu ranges from 0 to 1, start to end of curve.
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <history>
        /// </history>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) CubicBezier(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double t)
        {
            double mum1 = 1 - t;
            double mum13 = mum1 * mum1 * mum1;
            double mu3 = t * t * t;

            return (
                (mum13 * x0 + 3 * t * mum1 * mum1 * x1 + 3 * t * t * mum1 * x2 + mu3 * x3),
                (mum13 * y0 + 3 * t * mum1 * mum1 * y1 + 3 * t * t * mum1 * y2 + mu3 * y3)
                );
        }

        /// <summary>
        /// Four control point Bezier interpolation mu ranges from 0 to 1, start to end of curve.
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="z0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="z3"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        /// <history>
        /// </history>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) CubicBezier(
            double x0, double y0, double z0,
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double x3, double y3, double z3,
            double t)
        {
            double mum1 = 1 - t;
            double mum13 = mum1 * mum1 * mum1;
            double mu3 = t * t * t;

            return (
                (mum13 * x0 + 3 * t * mum1 * mum1 * x1 + 3 * t * t * mum1 * x2 + mu3 * x3),
                (mum13 * y0 + 3 * t * mum1 * mum1 * y1 + 3 * t * t * mum1 * y2 + mu3 * y3),
                (mum13 * z0 + 3 * t * mum1 * mum1 * z1 + 3 * t * t * mum1 * z2 + mu3 * z3)
                );
        }

        /// <summary>
        /// General Bezier curve Number of control points is n+1 0 less than or equal to mu less than 1
        /// IMPORTANT, the last point is not computed.
        /// </summary>
        /// <param name="points"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D CubicBSpline(List<Point2D> points, double t)
        {
            int n = points.Count - 1;
            int kn;
            int nn;
            int nkn;

            double blend;
            double muk = 1;
            double munk = Pow(1 - t, n);

            var b = new Point2D(0.0f, 0.0f);

            for (int k = 0; k <= n; k++)
            {
                nn = n;
                kn = k;
                nkn = n - k;
                blend = muk * munk;
                muk *= t;
                munk /= (1 - t);
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

            return (b);
        }

        #endregion

        #region Hermite Interpolation

        /// <summary>
        ///
        /// </summary>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <param name="time"></param>
        /// <param name="tension">1 is high, 0 normal, -1 is low</param>
        /// <param name="bias">0 is even,positive is towards first segment, negative towards the other</param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Hermite(
            double v0,
            double v1,
            double v2,
            double v3,
            double time, double tension = 0, double bias = 0)
        {
            double m0, m1, mu2, mu3;
            double a0, a1, a2, a3;

            mu2 = time * time;
            mu3 = mu2 * time;
            m0 = (v1 - v0) * (1 + bias) * (1 - tension) / 2;
            m0 += (v2 - v1) * (1 - bias) * (1 - tension) / 2;
            m1 = (v2 - v1) * (1 + bias) * (1 - tension) / 2;
            m1 += (v3 - v2) * (1 - bias) * (1 - tension) / 2;
            a0 = 2 * mu3 - 3 * mu2 + 1;
            a1 = mu3 - 2 * mu2 + time;
            a2 = mu3 - mu2;
            a3 = -2 * mu3 + 3 * mu2;

            return (a0 * v1 + a1 * m0 + a2 * m1 + a3 * v2);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="time"></param>
        /// <param name="tension">1 is high, 0 normal, -1 is low</param>
        /// <param name="bias">0 is even,positive is towards first segment, negative towards the other</param>
        /// <returns></returns>
        /// <remarks>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Hermite(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double time, double tension = 0, double bias = 0)
        {
            double mu2 = time * time;
            double mu3 = mu2 * time;

            double mX0 = (x1 - x0) * (1 + bias) * (1 - tension) / 2;
            mX0 += (x2 - x1) * (1 - bias) * (1 - tension) / 2;

            double mY0 = (y1 - y0) * (1 + bias) * (1 - tension) / 2;
            mY0 += (y2 - y1) * (1 - bias) * (1 - tension) / 2;

            double mX1 = (x2 - x1) * (1 + bias) * (1 - tension) / 2;
            mX1 += (x3 - x2) * (1 - bias) * (1 - tension) / 2;

            double mY1 = (y2 - y1) * (1 + bias) * (1 - tension) / 2;
            mY1 += (y3 - y2) * (1 - bias) * (1 - tension) / 2;

            double a0 = 2 * mu3 - 3 * mu2 + 1;
            double a1 = mu3 - 2 * mu2 + time;
            double a2 = mu3 - mu2;
            double a3 = -2 * mu3 + 3 * mu2;

            return (
                a0 * x1 + a1 * mX0 + a2 * mX1 + a3 * x2,
                a0 * y1 + a1 * mY0 + a2 * mY1 + a3 * y2);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="z0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="z3"></param>
        /// <param name="time"></param>
        /// <param name="tension">1 is high, 0 normal, -1 is low</param>
        /// <param name="bias">0 is even,positive is towards first segment, negative towards the other</param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Hermite(
            double x0, double y0, double z0,
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double x3, double y3, double z3,
            double time, double tension = 0, double bias = 0)
        {
            double mu2 = time * time;
            double mu3 = mu2 * time;

            double mX0 = (x1 - x0) * (1 + bias) * (1 - tension) / 2;
            mX0 += (x2 - x1) * (1 - bias) * (1 - tension) / 2;
            double mY0 = (y1 - y0) * (1 + bias) * (1 - tension) / 2;
            mY0 += (y2 - y1) * (1 - bias) * (1 - tension) / 2;
            double mZ0 = (z1 - z0) * (1 + bias) * (1 - tension) / 2;
            mZ0 += (z2 - z1) * (1 - bias) * (1 - tension) / 2;
            double mX1 = (x2 - x1) * (1 + bias) * (1 - tension) / 2;
            mX1 += (x3 - x2) * (1 - bias) * (1 - tension) / 2;
            double mY1 = (y2 - y1) * (1 + bias) * (1 - tension) / 2;
            mY1 += (y3 - y2) * (1 - bias) * (1 - tension) / 2;
            double mZ1 = (z2 - z1) * (1 + bias) * (1 - tension) / 2;
            mZ1 += (z3 - z2) * (1 - bias) * (1 - tension) / 2;
            double a0 = 2 * mu3 - 3 * mu2 + 1;
            double a1 = mu3 - 2 * mu2 + time;
            double a2 = mu3 - mu2;
            double a3 = -2 * mu3 + 3 * mu2;

            return (
                a0 * x1 + a1 * mX0 + a2 * mX1 + a3 * x2,
                a0 * y1 + a1 * mY0 + a2 * mY1 + a3 * y2,
                a0 * z1 + a1 * mZ0 + a2 * mZ1 + a3 * z2);
        }

        #endregion

        #region Linear Interpolation

        /// <summary>
        ///
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Linear(
                double v1, double v2,
                double t)
                => (1 - t) * v1 + t * v2;

        /// <summary>
        ///
        /// </summary>
        /// <param name="t"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D Linear(Point2D v1, Point2D v2, double t)
            => new Point2D(Linear(v1.X, v1.Y, v2.X, v2.Y, t));

        /// <summary>
        ///
        /// </summary>
        /// <param name="t"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D Linear(Point3D v1, Point3D v2, double t)
            => new Point3D(Linear(v1.X, v1.Y, v1.Z, v2.X, v2.Y, v2.Z, t));

        /// <summary>
        ///
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Linear(
            double x1, double y1,
            double x2, double y2,
            double t)
            => (
                (1 - t) * x1 + t * x2,
                (1 - t) * y1 + t * y2);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Linear(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double t)
            => (
                (1 - t) * x1 + t * x2,
                (1 - t) * y1 + t * y2,
                (1 - t) * z1 + t * z2);

        #endregion

        #region Quadratic Bezier Interpolation

        /// <summary>
        /// Three control point Bezier interpolation mu ranges from 0 to 1, start to end of the curve.
        /// </summary>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double QuadraticBezier(
            double v0,
            double v1,
            double v2,
            double t)
        {
            double mu1 = 1 - t;
            double mu12 = mu1 * mu1;
            double mu2 = t * t;

            return v0 * mu12 + 2 * v1 * mu1 * t + v2 * mu2;
        }

        /// <summary>
        /// Three control point Bezier interpolation mu ranges from 0 to 1, start to end of the curve.
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) QuadraticBezier(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double t)
        {
            double mu1 = 1 - t;
            double mu12 = mu1 * mu1;
            double mu2 = t * t;

            return (
                (x0 * mu12 + 2 * x1 * mu1 * t + x2 * mu2),
                (y0 * mu12 + 2 * y1 * mu1 * t + y2 * mu2)
                );
        }

        /// <summary>
        /// Three control point Bezier interpolation mu ranges from 0 to 1, start to end of the curve.
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="z0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) QuadraticBezier(
            double x0, double y0, double z0,
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double t)
        {
            double mu1 = 1 - t;
            double mu12 = mu1 * mu1;
            double mu2 = t * t;

            return (
                (x0 * mu12 + 2 * x1 * mu1 * t + x2 * mu2),
                (y0 * mu12 + 2 * y1 * mu1 * t + y2 * mu2),
                (z0 * mu12 + 2 * z1 * mu1 * t + z2 * mu2));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static List<(double X, double Y)> QuadraticBezierToCubicBezier(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2)
            => new List<(double X, double Y)>()
            {
                (x0, y0),
                (x0 + TwoThirds * (x1 - x0), y0 + TwoThirds * (y1 - y0)),
                (x2 + TwoThirds * (x1 - x2), y2 + TwoThirds * (y1 - y2)),
                (x2, y2)
            };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Point2D[] QuadraticBezierToCubicBezier(Point2D a, Point2D b, Point2D c)
            => new Point2D[]
            {
                a,
                new Point2D((int)(a.X + TwoThirds * (b.X - a.X)), (int)(a.Y + TwoThirds * (b.Y - a.Y))),
                new Point2D((int)(c.X + TwoThirds * (b.X - c.X)), (int)(c.Y + TwoThirds * (b.Y - c.Y))),
                c
            };

        #endregion

        #region Sine Interpolation

        /// <summary>
        ///
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://paulbourke.net/miscellaneous/interpolation/
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sine(
            double v1,
            double v2,
            double t)
        {
            double mu2 = (1 - Sin(t * PI)) / 2;
            return v1 * (1 - mu2) + v2 * mu2;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) Sine(
            double x1, double y1,
            double x2, double y2,
            double t)
        {
            double mu2 = (1 - Sin(t * PI)) / 2;
            return (
                x1 * (1 - mu2) + x2 * mu2,
                y1 * (1 - mu2) + y2 * mu2
                );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y, double Z) Sine(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double t)
        {
            double mu2 = (1 - Sin(t * PI)) / 2;
            return (
                x1 * (1 - mu2) + x2 * mu2,
                y1 * (1 - mu2) + y2 * mu2,
                z1 * (1 - mu2) + z2 * mu2);
        }

        #endregion

        #region Rectangle

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="fulcrumX"></param>
        /// <param name="fulcrumY"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<Point2D> RotatedRectangle(
            double x, double y,
            double width, double height,
            double fulcrumX, double fulcrumY,
            double angle)
        {
            // ToDo: Figure out how to properly include the location point.
            var points = new List<Point2D>();

            var xaxis = new Point2D(Cos(angle), Sin(angle));
            var yaxis = new Point2D(-Sin(angle), Cos(angle));

            // Apply the rotation transformation and translate to new center.
            points.Add(new Point2D(
                fulcrumX + ((-width / 2) * xaxis.X + (-height / 2) * xaxis.Y),
                fulcrumY + ((-width / 2) * yaxis.X + (-height / 2) * yaxis.Y)
                ));
            points.Add(new Point2D(
                fulcrumX + ((width / 2) * xaxis.X + (-height / 2) * xaxis.Y),
                fulcrumY + ((width / 2) * yaxis.X + (-height / 2) * yaxis.Y)
                ));
            points.Add(new Point2D(
                fulcrumX + ((width / 2) * xaxis.X + (height / 2) * xaxis.Y),
                fulcrumY + ((width / 2) * yaxis.X + (height / 2) * yaxis.Y)
                ));
            points.Add(new Point2D(
                fulcrumX + ((-width / 2) * xaxis.X + (height / 2) * xaxis.Y),
                fulcrumY + ((-width / 2) * yaxis.X + (height / 2) * yaxis.Y)
                ));

            return points;
        }

        #endregion
    }
}