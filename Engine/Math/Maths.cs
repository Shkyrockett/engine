// <copyright file="Maths.cs" >
//     Copyright (c) 2005 - 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using Engine.Geometry;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// Extended Math processing library.
    /// </summary>
    public static partial class Maths
    {
        #region Random

        /// <summary>
        /// Initialize random number generator with seed based on time.
        /// </summary>
        internal static Random RandomNumberGenerator = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Lower"></param>
        /// <param name="Upper"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Random(this double Lower, double Upper)
            => ((RandomNumberGenerator.Next() * ((Upper - Lower) + 1)) + Lower);

        #endregion

        #region Geometric Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double, double> OffsetSegment(
            double aX, double aY,
            double bX, double bY,
            double distance)
            => new Tuple<double, double, double, double>(
                (aX + 0.5 * -((bY - aY) / Distance(aX, aY, bX, bY)) * distance),
                (aY + 0.5 * ((bX - aX) / Distance(aX, aY, bX, bY)) * distance),
                (bX + 0.5 * -((bY - aY) / Distance(aX, aY, bX, bY)) * distance),
                (bY + 0.5 * ((bX - aX) / Distance(aX, aY, bX, bY)) * distance)
                );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="aZ"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="bZ"></param>
        /// <param name="distanceX"></param>
        /// <param name="distanceY"></param>
        /// <param name="distanceZ"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double, double, double, double> OffsetSegment(
            double aX, double aY, double aZ,
            double bX, double bY, double bZ,
            double distanceX, double distanceY, double distanceZ)
            => new Tuple<double, double, double, double, double, double>(
                (aX + 0.5 * -((bY - aY) / Distance(aX, aY, aZ, bX, bY, bZ)) * distanceX),
                (aY + 0.5 * ((bX - aX) / Distance(aX, aY, aZ, bX, bY, bZ)) * distanceY),
                (aZ + 0.5 * ((bZ - aZ) / Distance(aX, aY, aZ, bX, bY, bZ)) * distanceZ),
                (bX + 0.5 * -((bY - aY) / Distance(aX, aY, aZ, bX, bY, bZ)) * distanceX),
                (bY + 0.5 * ((bX - aX) / Distance(aX, aY, aZ, bX, bY, bZ)) * distanceY),
                (bZ + 0.5 * ((bZ - aZ) / Distance(aX, aY, aZ, bX, bY, bZ)) * distanceZ)
                );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public static double Angle(double i, double j)
            => Atan2(i, -j);

        /// <summary>
        /// Returns the Angle of a line.
        /// </summary>
        /// <param name="x1">Horizontal Component of Point Starting Point</param>
        /// <param name="y1">Vertical Component of Point Starting Point</param>
        /// <param name="x2">Horizontal Component of Ending Point</param>
        /// <param name="y2">Vertical Component of Ending Point</param>
        /// <returns>Returns the Angle of a line.</returns>
        /// <remarks></remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Angle(
            double x1, double y1,
            double x2, double y2)
            => Atan2((y1 - y2), (x1 - x2));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <returns></returns>
        /// <remarks>http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C</remarks>
        [Pure]
        public static double Angle(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => (Math.Abs(x1 - x2) < Epsilon
            && Math.Abs(y1 - y2) < Epsilon
            && Math.Abs(z1 - z2) < Epsilon) ? 0 : Acos(Min(1.0d, DotProduct(Normalize(x1, y1, z1), Normalize(x2, y2, z2))));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <returns>
        /// Return the angle ABC.
        /// Return a value between PI and -PI.
        /// Note that the value is the opposite of what you might
        /// expect because Y coordinates increase downward.
        /// </returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AngleVector(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
            => Atan2(CrossProductVector(x1, y1, x2, y2, x3, y3), DotProductVector(x1, y1, x2, y2, x3, y3));

        /// <summary>
        /// Find the absolute positive value of a radian angle from two points.
        /// </summary>
        /// <param name="x1">Horizontal Component of Point Starting Point</param>
        /// <param name="y1">Vertical Component of Point Starting Point</param>
        /// <param name="x2">Horizontal Component of Ending Point</param>
        /// <param name="y2">Vertical Component of Ending Point</param>
        /// <returns>The absolute angle of a line in radians.</returns>
        /// <remarks></remarks>
        [Pure]
        public static double AbsoluteAngle(
            double x1, double y1,
            double x2, double y2)
        {
            // Find the angle of point a and point b. 
            double test = -Angle(x1, y1, x2, y2) % PI;
            return test < 0 ? test += PI : test;
        }

        /// <summary>
        /// Finds the angle between two vectors.
        /// </summary>
        /// <param name="uX"></param>
        /// <param name="uY"></param>
        /// <param name="vX"></param>
        /// <param name="vY"></param>
        /// <returns></returns>
        /// <remarks>http://james-ramsden.com/angle-between-two-vectors/</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AngleBetween(
            double uX, double uY,
            double vX, double vY)
            => Acos((uX * vX + uY * vY) / Sqrt((uX * uX + uY * uY) * (vX * vX + vY * vY)));

        /// <summary>
        /// Finds the angle between two vectors.
        /// </summary>
        /// <param name="uX"></param>
        /// <param name="uY"></param>
        /// <param name="uZ"></param>
        /// <param name="vX"></param>
        /// <param name="vY"></param>
        /// <param name="vZ"></param>
        /// <returns></returns>
        /// <remarks>http://james-ramsden.com/angle-between-two-vectors/</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AngleBetween(
            double uX, double uY, double uZ,
            double vX, double vY, double vZ)
            => Acos((uX * vX + uY * vY + uZ * vZ) / Sqrt((uX * uX + uY * uY + uZ * uZ) * (vX * vX + vY * vY + vZ * vZ)));

        /// <summary>
        /// Find the elliptical t that matches the coordinates of a circular angle.
        /// </summary>
        /// <param name="angle">The angle to transform into elliptic angle.</param>
        /// <param name="rx">The first radius of the ellipse.</param>
        /// <param name="ry">The second radius of the ellipse.</param>
        /// <returns></returns>
        /// <remarks>
        /// Based on the answer by flup at: 
        /// http://stackoverflow.com/questions/17762077/how-to-find-the-point-on-ellipse-given-the-angle
        /// </remarks>
        public static double EllipsePolarAngle(double angle, double rx, double ry)
        {
            // Wrap the angle between -2PI and PI.
            var theta = WrapAngle(angle);

            // Find the elliptical t that matches the circular angle.
            if (Math.Abs(angle) == HalfPi || Math.Abs(angle) == Pau)
                return angle;
            else if (angle > HalfPi && angle < Pau)
                return Atan(rx * Tan(theta) / ry) + PI;
            else if (angle < -HalfPi && angle > -Pau)
                return Atan(rx * Tan(theta) / ry) - PI;
            else
                return Atan(rx * Tan(theta) / ry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/1476497/multiply-two-point-objects
        /// </remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double> ComplexProduct(double x0, double y0, double x1, double y1)
            => new Tuple<double, double>(x0 * x1 - y0 * y1, x0 * y1 + y0 * x1);

        /// <summary>
        /// Cross Product of two points.
        /// </summary>
        /// <param name="x1">First Point X component.</param>
        /// <param name="y1">First Point Y component.</param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <returns>the cross product AB · BC.</returns>
        /// <remarks>Note that AB · BC = |AB| * |BC| * Cos(theta).</remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CrossProduct(
            double x1, double y1,
            double x2, double y2)
            => (x1 * y2) - (y1 * x2);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double> CrossProduct(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => new Tuple<double, double, double>(
                    (y1 * z2) - (z1 * y2), // X
                    (z1 * x2) - (x1 * z2), // Y
                    (x1 * y2) - (y1 * x2)  // Z
                );

        /// <summary>
        /// The cross product is a vector perpendicular to AB
        /// and BC having length |AB| * |BC| * Sin(theta) and
        /// with direction given by the right-hand rule.
        /// For two vectors in the X-Y plane, the result is a
        /// vector with X and Y components 0 so the Z component
        /// gives the vector's length and direction.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <returns>
        /// Return the cross product AB x BC.
        /// </returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CrossProductVector(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
            => ((x1 - x2) * (y3 - y2) - (y1 - y2) * (x3 - x2));

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector. 
        /// </summary>
        /// <param name="x1">First Point X component.</param>
        /// <param name="y1">First Point Y component.</param>
        /// <param name="x2">Second Point X component.</param>
        /// <param name="y2">Second Point Y component.</param>
        /// <returns>The Dot Product.</returns>
        /// <remarks>The dot product "·" is calculated with DotProduct = X ^ 2 + Y ^ 2</remarks>
        [Pure]
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
        [Pure]
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
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(
            Tuple<double, double, double> tuple,
            double x2, double y2, double z2)
            => DotProduct(tuple.Item1, tuple.Item2, tuple.Item3, x2, y2, z2);

        /// <summary>
        /// Calculates the dot Aka. scalar or inner product of a vector. 
        /// </summary>
        /// <param name="tuple1">First set of X, Y, Z components in tuple form.</param>
        /// <param name="tuple2">Second set of X, Y, Z components in tuple form.</param>
        /// <returns>The Dot Product.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(
            Tuple<double, double, double> tuple1,
            Tuple<double, double, double> tuple2)
            => DotProduct(
                tuple1.Item1, tuple1.Item2, tuple1.Item3,
                tuple2.Item1, tuple2.Item2, tuple2.Item3
                );

        /// <summary>
        /// Dot Product of the vector of three points
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <returns>
        /// Return the dot product AB · BC.
        /// </returns>
        /// <remarks>
        /// Note that AB · BC = |AB| * |BC| * Cos(theta).
        /// http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/
        /// </remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProductVector(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
            => (((x1 - x2) * (x3 - x2)) + ((y1 - y2) * (y3 - y2)));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="z3"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double MixedProduct(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double x3, double y3, double z3)
            => DotProduct(CrossProduct(x1, y1, z1, x2, y2, z2), x3, y3, z3);

        /// <summary>
        /// Distance between two points.
        /// </summary>
        /// <param name="x1">First X component.</param>
        /// <param name="y1">First Y component.</param>
        /// <param name="x2">Second X component.</param>
        /// <param name="y2">Second Y component.</param>
        /// <returns>The distance between two points.</returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(
            double x1, double y1,
            double x2, double y2)
            => Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));

        /// <summary>
        /// Distance between two points.
        /// </summary>
        /// <param name="x1">First X component.</param>
        /// <param name="y1">First Y component.</param>
        /// <param name="z1">First Z component.</param>
        /// <param name="x2">Second X component.</param>
        /// <param name="y2">Second Y component.</param>
        /// <param name="z2">Second Z component.</param>
        /// <returns>The distance between two points.</returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1) + (z2 - z1) * (z2 - z1));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(Point3D p1, Point3D p2)
            => Distance(p1.X, p1.Y, p1.Z, p2.X, p2.Y, p2.Z);

        /// <summary>
        /// The square of the distance between two points.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SquareDistance(
            double x1, double y1,
            double x2, double y2)
            => ((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));

        /// <summary>
        /// Find the square of the distance of a point from a line.
        /// </summary>
        /// <param name="x1">The x component of the Point.</param>
        /// <param name="y1">The y component of the Point.</param>
        /// <param name="lx2">The x component of the first point on the line.</param>
        /// <param name="ly2">The y component of the first point on the line.</param>
        /// <param name="lx3">The x component of the second point on the line.</param>
        /// <param name="ly3">The y component of the second point on the line.</param>
        /// <returns></returns>
        [Pure]
        public static double SquareDistanceToLine(
            double x1, double y1,
            double lx2, double ly2,
            double lx3, double ly3)
        {
            double A = ly2 - ly3;
            double B = lx3 - lx2;
            double C = (A * x1 + B * y1) - (A * lx2 + B * ly2);
            return (C * C) / (A * A + B * B);
        }

        /// <summary>
        /// Calculates the Slope of a vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>Returns the slope angle of a vector.</returns>
        /// <remarks>
        /// The slope is calculated with Slope = Y / X or rise over run
        /// If the line is vertical, return something close to infinity
        /// (Close to the largest value allowed for the data type).
        /// Otherwise calculate and return the slope.
        /// </remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Slope(double i, double j)
            => Math.Abs(i) < Epsilon ? SlopeMax : (j / i);

        /// <summary>
        /// Returns the slope angle of a line.
        /// </summary>
        /// <param name="x1">Horizontal Component of Point Starting Point</param>
        /// <param name="y1">Vertical Component of Point Starting Point</param>
        /// <param name="x2">Horizontal Component of Ending Point</param>
        /// <param name="y2">Vertical Component of Ending Point</param>
        /// <returns>Returns the slope angle of a line.</returns>
        /// <remarks>
        /// If the Line is Vertical return something close to infinity (Close to 
        /// the largest value allowed for the data type).
        /// Otherwise calculate and return the slope.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Pure]
        public static double Slope(
            double x1, double y1,
            double x2, double y2)
            => (Math.Abs(x1 - x2) < Epsilon) ? SlopeMax : ((y2 - y1) / (x2 - x1));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Abs(double i, double j)
            => Magnitude(i, j);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        /// <remarks>http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C</remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Abs(double i, double j, double k)
            => Magnitude(i, j, k);

        /// <summary>
        /// Magnitude of a two dimensional Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Magnitude(double i, double j)
            => Sqrt((i * i) + (j * j));

        /// <summary>
        /// Magnitude of a three dimensional Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Magnitude(double i, double j, double k)
            => Sqrt((i * i) + (j * j) + (k * k));

        /// <summary>
        /// Modulus of a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Modulus(double i, double j)
            => Magnitude(i, j);

        /// <summary>
        /// Modulus of a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Modulus(double i, double j, double k)
            => Magnitude(i, j, k);

        /// <summary>
        /// Unitize a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double> Unitize(double i, double j)
            => Normalize(i, j);

        /// <summary>
        /// Unitize a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double> Unitize(double i, double j, double k)
            => Normalize(i, j, k);

        /// <summary>
        /// Normalize a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double> Normalize(
            double i, double j)
            => new Tuple<double, double>(
                i / Sqrt((i * i) + (j * j)),
                j / Sqrt((i * i) + (j * j))
                );

        /// <summary>
        /// Normalize a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double> Normalize(
            double i, double j, double k)
            => new Tuple<double, double, double>(
                i / Sqrt((i * i) + (j * j) + (k * k)),
                j / Sqrt((i * i) + (j * j) + (k * k)),
                k / Sqrt((i * i) + (j * j) + (k * k))
                );

        /// <summary>
        /// Find the Normal of Two points.
        /// </summary>
        /// <param name="x1">The x component of the first Point.</param>
        /// <param name="y1">The y component of the first Point.</param>
        /// <param name="x2">The x component of the second Point.</param>
        /// <param name="y2">The y component of the second Point.</param>
        /// <returns>The Normal of two Points</returns>
        /// <remarks></remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double> Normalize(
            double x1, double y1,
            double x2, double y2)
            => new Tuple<double, double>(
                x1 / Sqrt(((x1 * x2) + (y1 * y2))),
                y1 / Sqrt(((x1 * x2) + (y1 * y2)))
                );

        /// <summary>
        /// Find the Normal of Two points.
        /// </summary>
        /// <param name="x1">The x component of the first Point.</param>
        /// <param name="y1">The y component of the first Point.</param>
        /// <param name="z1">The z component of the first Point.</param>
        /// <param name="x2">The x component of the second Point.</param>
        /// <param name="y2">The y component of the second Point.</param>
        /// <param name="z2">The z component of the second Point.</param>
        /// <returns>The Normal of two Points</returns>
        /// <remarks>http://www.fundza.com/vectors/normalize/</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double> Normalize(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => new Tuple<double, double, double>(
                x1 / Sqrt((x1 * x2) + (y1 * y2) + (z1 * z2)),
                y1 / Sqrt((x1 * x2) + (y1 * y2) + (z1 * z2)),
                z1 / Sqrt((x1 * x2) + (y1 * y2) + (z1 * z2))
                );

        /// <summary>
        /// Find the Clockwise Perpendicular of a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <remarks>To get the perpendicular vector in two dimensions use I = -J, J = I</remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double> PerpendicularClockwise(double i, double j)
            => new Tuple<double, double>(-j, i);

        /// <summary>
        /// Find the Counter Clockwise Perpendicular of a Vector.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        /// <remarks>To get the perpendicular vector in two dimensions use I = -J, J = I</remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double> PerpendicularCounterClockwise(double i, double j)
            => new Tuple<double, double>(j, -i);

        /// <summary>
        /// Find the determinant of a 2 by 2 matrix.
        /// </summary>
        /// <param name="m1x1"></param>
        /// <param name="m1x2"></param>
        /// <param name="m2x1"></param>
        /// <param name="m2x2"></param>
        /// <returns></returns>
        /// <remarks>https://github.com/onlyuser/Legacy/blob/master/msvb/Dex3d/Math.bas</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Determinant(
            double m1x1, double m1x2,
            double m2x1, double m2x2)
            => ((m1x1 * m2x2)
              - (m1x2 * m2x1));

        /// <summary>
        /// Find the determinant of a 3 by 3 matrix.
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
        /// <remarks>https://github.com/onlyuser/Legacy/blob/master/msvb/Dex3d/Math.bas</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Determinant(
            double a, double b, double c,
            double d, double e, double f,
            double g, double h, double i)
            => ((a * Determinant(e, f, h, i))
              - (b * Determinant(d, f, g, i))
              + (c * Determinant(d, e, g, h))
            );

        /// <summary>
        /// Find the determinant of a 4 by 4 matrix.
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
        /// <param name="l"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="o"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        /// <remarks>https://github.com/onlyuser/Legacy/blob/master/msvb/Dex3d/Math.bas</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Determinant(
            double a, double b, double c, double d,
            double e, double f, double g, double h,
            double i, double j, double k, double l,
            double m, double n, double o, double p)
            => ((a * Determinant(f, g, h, j, k, l, n, o, p))
              - (b * Determinant(e, g, h, i, k, l, m, o, p))
              + (c * Determinant(e, f, h, i, j, l, m, n, p))
              - (d * Determinant(e, f, g, i, j, k, m, n, o))
            );

        /// <summary>
        /// Find the determinant of a 5 by 5 matrix.
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
        /// <param name="l"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="o"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="r"></param>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="w"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Determinant(
            double a, double b, double c, double d, double e,
            double f, double g, double h, double i, double j,
            double k, double l, double m, double n, double o,
            double p, double q, double r, double s, double t,
            double u, double v, double w, double x, double y)
            => ((a * Determinant(g, h, i, j, l, m, n, o, q, r, s, t, v, w, x, y))
              - (b * Determinant(f, h, i, j, k, m, n, o, p, r, s, t, u, w, x, y))
              + (c * Determinant(f, g, i, j, k, l, n, o, p, q, s, t, u, v, x, y))
              - (d * Determinant(f, g, h, j, k, l, m, o, p, q, r, t, u, v, w, y))
              + (e * Determinant(f, g, h, i, k, l, m, n, p, q, r, s, u, v, w, x))
            );

        /// <summary>
        /// Find the determinant of a 6 by 6 matrix.
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
        /// <param name="l"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="o"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="r"></param>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="w"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="aa"></param>
        /// <param name="bb"></param>
        /// <param name="cc"></param>
        /// <param name="dd"></param>
        /// <param name="ee"></param>
        /// <param name="ff"></param>
        /// <param name="gg"></param>
        /// <param name="hh"></param>
        /// <param name="ii"></param>
        /// <param name="jj"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Determinant(
            double a, double b, double c, double d, double e, double f,
            double g, double h, double i, double j, double k, double l,
            double m, double n, double o, double p, double q, double r,
            double s, double t, double u, double v, double w, double x,
            double y, double z, double aa, double bb, double cc, double dd,
            double ee, double ff, double gg, double hh, double ii, double jj)
            => ((a * Determinant(h, i, j, k, l, n, o, p, q, r, t, u, v, w, x, z, aa, bb, cc, dd, ff, gg, hh, ii, jj))
              - (b * Determinant(g, i, j, k, l, m, o, p, q, r, s, u, v, w, x, y, aa, bb, cc, dd, ee, gg, hh, ii, jj))
              + (c * Determinant(g, h, j, k, l, m, n, p, q, r, s, t, v, w, x, y, z, bb, cc, dd, ee, ff, hh, ii, jj))
              - (d * Determinant(g, h, i, k, l, m, n, o, q, r, s, t, u, w, x, y, z, aa, cc, dd, ee, ff, gg, ii, jj))
              + (e * Determinant(g, h, i, j, l, m, n, o, p, r, s, t, u, v, x, y, z, aa, bb, dd, ee, ff, gg, hh, jj))
              - (f * Determinant(g, h, i, j, k, m, n, o, p, q, s, t, u, v, w, y, z, aa, bb, cc, ee, ff, gg, hh, ii))
            );

        /// <summary>
        /// Find the inverse of the determinant of a 2 by 2 matrix.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseDeterminant(
            double a, double b,
            double c, double d)
            => 1 / ((a * d)
              - (b * c)
            );

        /// <summary>
        /// Find the inverse of the determinant of a 3 by 3 matrix.
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
        /// <remarks>https://github.com/onlyuser/Legacy/blob/master/msvb/Dex3d/Math.bas</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseDeterminant(
            double a, double b, double c,
            double d, double e, double f,
            double g, double h, double i)
            => 1 / ((a * Determinant(e, f, h, i))
              - (b * Determinant(d, f, g, i))
              + (c * Determinant(d, e, g, h))
            );

        /// <summary>
        /// Find the inverse of the determinant of a 4 by 4 matrix.
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
        /// <param name="l"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="o"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        /// <remarks>https://github.com/onlyuser/Legacy/blob/master/msvb/Dex3d/Math.bas</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseDeterminant(
            double a, double b, double c, double d,
            double e, double f, double g, double h,
            double i, double j, double k, double l,
            double m, double n, double o, double p)
            => 1 / ((a * Determinant(f, g, h, j, k, l, n, o, p))
              - (b * Determinant(e, g, h, i, k, l, m, o, p))
              + (c * Determinant(e, f, h, i, j, l, m, n, p))
              - (d * Determinant(e, f, g, i, j, k, m, n, o))
            );

        /// <summary>
        /// Find the inverse of the determinant of a 5 by 5 matrix.
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
        /// <param name="l"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="o"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="r"></param>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="w"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseDeterminant(
            double a, double b, double c, double d, double e,
            double f, double g, double h, double i, double j,
            double k, double l, double m, double n, double o,
            double p, double q, double r, double s, double t,
            double u, double v, double w, double x, double y)
            => 1d / ((a * Determinant(g, h, i, j, l, m, n, o, q, r, s, t, v, w, x, y))
              - (b * Determinant(f, h, i, j, k, m, n, o, p, r, s, t, u, w, x, y))
              + (c * Determinant(f, g, i, j, k, l, n, o, p, q, s, t, u, v, x, y))
              - (d * Determinant(f, g, h, j, k, l, m, o, p, q, r, t, u, v, w, y))
              + (e * Determinant(f, g, h, i, k, l, m, n, p, q, r, s, u, v, w, x))
            );

        /// <summary>
        /// Find the inverse of the determinant of a 6 by 6 matrix.
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
        /// <param name="l"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="o"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <param name="r"></param>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="w"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="aa"></param>
        /// <param name="bb"></param>
        /// <param name="cc"></param>
        /// <param name="dd"></param>
        /// <param name="ee"></param>
        /// <param name="ff"></param>
        /// <param name="gg"></param>
        /// <param name="hh"></param>
        /// <param name="ii"></param>
        /// <param name="jj"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseDeterminant(
            double a, double b, double c, double d, double e, double f,
            double g, double h, double i, double j, double k, double l,
            double m, double n, double o, double p, double q, double r,
            double s, double t, double u, double v, double w, double x,
            double y, double z, double aa, double bb, double cc, double dd,
            double ee, double ff, double gg, double hh, double ii, double jj)
            => 1d / ((a * Determinant(h, i, j, k, l, n, o, p, q, r, t, u, v, w, x, z, aa, bb, cc, dd, ff, gg, hh, ii, jj))
              - (b * Determinant(g, i, j, k, l, m, o, p, q, r, s, u, v, w, x, y, aa, bb, cc, dd, ee, gg, hh, ii, jj))
              + (c * Determinant(g, h, j, k, l, m, n, p, q, r, s, t, v, w, x, y, z, bb, cc, dd, ee, ff, hh, ii, jj))
              - (d * Determinant(g, h, i, k, l, m, n, o, q, r, s, t, u, w, x, y, z, aa, cc, dd, ee, ff, gg, ii, jj))
              + (e * Determinant(g, h, i, j, l, m, n, o, p, r, s, t, u, v, x, y, z, aa, bb, dd, ee, ff, gg, hh, jj))
              - (f * Determinant(g, h, i, j, k, m, n, o, p, q, s, t, u, v, w, y, z, aa, bb, cc, ee, ff, gg, hh, ii))
            );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <returns></returns>
        /// <remarks>http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double> Projection(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => new Tuple<double, double, double>(
                x2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2),
                y2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2),
                z2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2)
                );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="z2"></param>
        /// <returns></returns>
        /// <remarks>http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double> Rejection(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => new Tuple<double, double, double>(
                x1 - x2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2),
                z1 - y2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2),
                z1 - z2 * DotProduct(x1, y1, z1, x2, y2, z2) / Magnitude(x2, y2, z2) * Magnitude(x2, y2, z2)
                );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i1"></param>
        /// <param name="j1"></param>
        /// <param name="k1"></param>
        /// <param name="i2"></param>
        /// <param name="j2"></param>
        /// <param name="k2"></param>
        /// <returns></returns>
        /// <remarks>http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C</remarks>
        [Pure]
        public static Tuple<double, double, double> Reflection(
            double i1, double j1, double k1,
            double i2, double j2, double k2)
        {
            // if v2 has a right angle to vector, return -vector and stop
            if (Math.Abs(Math.Abs(Angle(i1, j1, k1, i2, j2, k2)) - PI / 2) < double.Epsilon)
                return new Tuple<double, double, double>(-i1, -j1, -k1);

            Tuple<double, double, double> projection = Projection(i1, j1, k1, i2, j2, k2);
            return new Tuple<double, double, double>(
                (2 * projection.Item1 - i1) * Magnitude(i1, j1, k1),
                (2 * projection.Item2 - j1) * Magnitude(i1, j1, k1),
                (2 * projection.Item3 - k1) * Magnitude(i1, j1, k1)
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="rad"></param>
        /// <returns></returns>
        /// <remarks>http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double> RotateX(double x1, double y1, double z1, double rad)
            => new Tuple<double, double, double>(
                x1,
                (y1 * Cos(rad)) - (z1 * Sin(rad)),
                (y1 * Sin(rad)) + (z1 * Cos(rad))
                );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="rad"></param>
        /// <returns></returns>
        /// <remarks>http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double> Pitch(double x1, double y1, double z1, double rad)
            => RotateX(x1, y1, z1, rad);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="rad"></param>
        /// <returns></returns>
        /// <remarks>http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double> RotateY(double x1, double y1, double z1, double rad)
            => new Tuple<double, double, double>(
                (z1 * Sin(rad)) + (x1 * Cos(rad)),
                y1,
                (z1 * Cos(rad)) - (x1 * Sin(rad))
                );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="rad"></param>
        /// <returns></returns>
        /// <remarks>http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double> Yaw(double x1, double y1, double z1, double rad)
            => RotateY(x1, y1, z1, rad);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="rad"></param>
        /// <returns></returns>
        /// <remarks>http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double> RotateZ(double x1, double y1, double z1, double rad)
            => new Tuple<double, double, double>(
                (x1 * Cos(rad)) - (y1 * Sin(rad)),
                (x1 * Sin(rad)) + (y1 * Cos(rad)),
                z1
                );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="rad"></param>
        /// <returns></returns>
        /// <remarks>http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double> Roll(double x1, double y1, double z1, double rad)
            => RotateZ(x1, y1, z1, rad);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="yOff"></param>
        /// <param name="zOff"></param>
        /// <param name="rad"></param>
        /// <returns></returns>
        /// <remarks>http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double> RotateX(double x1, double y1, double z1, double yOff, double zOff, double rad)
            => new Tuple<double, double, double>(
                x1,
                (y1 * Cos(rad)) - (z1 * Sin(rad)) + (yOff * (1 - Cos(rad)) + zOff * Sin(rad)),
                (y1 * Sin(rad)) + (z1 * Cos(rad)) + (zOff * (1 - Cos(rad)) - yOff * Sin(rad))
                );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="xOff"></param>
        /// <param name="zOff"></param>
        /// <param name="rad"></param>
        /// <returns></returns>
        /// <remarks>http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double> RotateY(double x1, double y1, double z1, double xOff, double zOff, double rad)
            => new Tuple<double, double, double>(
                (z1 * Sin(rad)) + (x1 * Cos(rad)) + (xOff * (1 - Cos(rad)) - zOff * Sin(rad)),
                y1,
                (z1 * Cos(rad)) - (x1 * Sin(rad)) + (zOff * (1 - Cos(rad)) + xOff * Sin(rad))
                );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="z1"></param>
        /// <param name="xOff"></param>
        /// <param name="yOff"></param>
        /// <param name="rad"></param>
        /// <returns></returns>
        /// <remarks>http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double> RotateZ(double x1, double y1, double z1, double xOff, double yOff, double rad)
            => new Tuple<double, double, double>(
                (x1 * Cos(rad)) - (y1 * Sin(rad)) + (xOff * (1 - Cos(rad)) + yOff * Sin(rad)),
                (x1 * Sin(rad)) + (y1 * Cos(rad)) + (yOff * (1 - Cos(rad)) - xOff * Sin(rad)),
                z1
                );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="normalI1"></param>
        /// <param name="normalJ1"></param>
        /// <param name="normalK1"></param>
        /// <param name="lineOfSightI2"></param>
        /// <param name="lineOfSightJ2"></param>
        /// <param name="lineOfSightK2"></param>
        /// <returns></returns>
        /// <remarks>http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBackFace(
            double normalI1, double normalJ1, double normalK1,
            double lineOfSightI2, double lineOfSightJ2, double lineOfSightK2)
            => DotProduct(normalI1, normalJ1, normalK1, lineOfSightI2, lineOfSightJ2, lineOfSightK2) < 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i1"></param>
        /// <param name="j1"></param>
        /// <returns></returns>
        /// <remarks>http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUnitVector(double i1, double j1)
            => Math.Abs(Magnitude(i1, j1) - 1) < Epsilon;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i1"></param>
        /// <param name="j1"></param>
        /// <param name="k1"></param>
        /// <returns></returns>
        /// <remarks>http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUnitVector(double i1, double j1, double k1)
            => Math.Abs(Magnitude(i1, j1, k1) - 1) < Epsilon;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point2D LinearInterpolate(Point2D v1, Point2D v2, double t)
            => new Point2D(Interpolaters.Linear(v1.X, v1.Y, v2.X, v2.Y, t));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point3D LinearInterpolate(Point3D v1, Point3D v2, double t)
            => new Point3D(Interpolaters.Linear(v1.X, v1.Y, v1.Z, v2.X, v2.Y, v2.Z, t));

        #endregion

        #region Array Math

        /// <summary>
        /// Returns the average value of a numeric array.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <remarks>Note: Uses Following Sum Function as well.</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Average(this double[] values)
            => (values.Sum() / values.Length);

        /// <summary>
        /// Returns the average value of a numeric array.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <remarks>Note: Uses Following Sum Function as well.</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Average(this List<double> values)
            => (values.Sum() / values.Count);

        /// <summary>
        /// Returns the average value of a numeric array.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <remarks>Note: Uses Following Sum Function as well.</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Average(this IEnumerable<double> values)
            => values.Sum() / values.Count();

        /// <summary>
        /// Find the sum of an array of Numbers
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sum(double[] values)
            => values.Sum();

        /// <summary>
        /// Find the sum of an array of Numbers
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sum(List<double> values)
            => values.Sum();

        /// <summary>
        /// Find the sum of an array of Numbers
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Sum(IEnumerable<double> values)
            => values.Sum();

        #endregion

        #region Derived Equivalent Math Functions

        // Derived equivalent Math Functions The following is a list of non-intrinsic math functions that can be derived from the intrinsic math functions:

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Tuple<double, double> QuadraticEquation(double a, double b, double c)
            => new Tuple<double, double>(
            (-b + Sqrt(b * b - (4 * a * c))) / (2 * a),
            (-b - Sqrt(b * b - (4 * a * c))) / (2 * a));

        /// <summary>
        /// Returns the specified root a specified number.
        /// </summary>
        /// <param name="x">A double-precision floating-point number to find the specified root of.</param>
        /// <param name="y">A double-precision floating-point number that specifies a root.</param>
        /// <returns>The y root of the number x.</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Root(double x, double y)
            => (x < 0 && Math.Abs(y % 2 - 1) < Epsilon) ? -Pow(-x, (1d / y)) : Pow(x, (1d / y));

        /// <summary>
        /// Cube root equivalent of the sqrt function. (note that there are actually
        /// three roots: one real, two complex, and we don't care about the latter):
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/26823024/cubic-bezier-reverse-getpoint-equation-float-for-vector-vector-for-float?answertab=active#tab-top</remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Crt(double value)
            => value < 0 ? -Pow(-value, 1d / 3d) : Pow(value, 1d / 3d);

        /// <summary>
        /// Derived math functions equivalent Secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Secant(double value)
            => ((Math.Abs(value % PI - HalfPi) > Epsilon) && (Math.Abs(value % PI - -HalfPi) > Epsilon)) ? (1 / Cos(value)) : 0;

        /// <summary>
        /// Derived math functions equivalent  Co-secant
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cosecant(double Value)
            => ((Math.Abs(Value % PI) > Epsilon) && (Math.Abs(Value % PI - PI) > Epsilon)) ? (1 / Sin(Value)) : 0;

        /// <summary>
        /// Derived math functions equivalent Cotangent
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cotangent(double Value)
            => ((Math.Abs(Value % PI) > Epsilon) && (Math.Abs(Value % PI - PI) > Epsilon)) ? (1 / Tan(Value)) : 0;

        /// <summary>
        /// Derived math functions equivalent Inverse Sine
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        public static double InverseSine(double value)
        {
            //  Arc-sin(X) 
            // Return Atan(Value / Sqrt(-Value * Value + 1))
            if (Math.Abs(value - 1) < Epsilon)
                return HalfPi;
            if (Math.Abs(value - -1) < Epsilon)
                return -HalfPi;
            if (Math.Abs(value) < 1)
                return Atan(value / Sqrt(-value * value + 1));

            return 0;
        }

        /// <summary>
        /// Derived math functions equivalent Inverse Cosine
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        public static double InverseCosine(double value)
        {
            //  Arc-cos(X) 
            // Return Atan(-Value / Sqrt(-Value * Value + 1)) + 2 * Atan(1)
            if (Math.Abs(value - 1) < Epsilon)
                return 0;
            if (Math.Abs(value - -1) < Epsilon)
                return PI;
            if (Math.Abs(value) < 1)
                return Atan(-value / Sqrt(-value * value + 1)) + 2 * Atan(1);

            return 0;
        }

        /// <summary>
        /// Derived math functions equivalent Inverse Secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        public static double InverseSecant(double value)
        {
            //  Arc-sec(X) 
            // Return Atan(Value / Sqrt(Value * Value - 1)) + Sign((Value) - 1) * (2 * Atan(1))
            if (Math.Abs(value - 1) < Epsilon)
                return 0;
            if (Math.Abs(value - -1) < Epsilon)
                return PI;
            if (Math.Abs(value) < 1)
                return Atan(value / Sqrt(value * value - 1)) + Sin((value) - 1) * (2 * Atan(1));

            return 0;
        }

        /// <summary>
        /// Derived math functions equivalent Inverse Co-secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [Pure]
        public static double InverseCosecant(double value)
        {
            //  Arc-co-sec(X) 
            // Return Atan(Value / Sqrt(Value * Value - 1)) + (Sign(Value) - 1) * (2 * Atan(1))
            if (Math.Abs(value - 1) < Epsilon)
                return HalfPi;
            if (Math.Abs(value - -1) < Epsilon)
                return -HalfPi;
            if (Math.Abs(value) < 1)
                return Atan(value / Sqrt(value * value - 1)) + (Sin(value) - 1) * (2 * Atan(1));
            return 0;
        }

        /// <summary>
        /// Derived math functions equivalent Inverse Cotangent
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>Arc-co-tan(X)</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseCotangent(double value)
            => (Atan(value) + (2 * Atan(1)));

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Sine
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HSin(X)</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double HyperbolicSine(double value)
            => ((Exp(value) - Exp((value * -1))) * 0.5d);

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Cosine
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HCos(X)</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double HyperbolicCosine(double value)
            => ((Exp(value) + Exp((value * -1))) * 0.5d);

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Tangent
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HTan(X)</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double HyperbolicTangent(double value)
            => ((Exp(value) - Exp((value * -1))) / (Exp(value) + Exp((value * -1))));

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HSec(X)</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double HyperbolicSecant(double value)
            => (0.5d * (Exp(value) + Exp((value * -1))));

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Co-secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HCosec(X)</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double HyperbolicCosecant(double value)
            => (0.5d * (Exp(value) - Exp((value * -1))));

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Cotangent
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HCotan(X) </remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double HyperbolicCotangent(double value)
            => ((Exp(value) + Exp((value * -1))) / (Exp(value) - Exp((value * -1))));

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Sine
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HArcsin(X)</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseHyperbolicSine(double value)
            => Log((value + Sqrt(((value * value) + 1))));

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Cosine
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HArccos(X)</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseHyperbolicCosine(double value)
            => Log((value + Sqrt(((value * value) - 1))));

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Tangent
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HArctan(X)</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseHyperbolicTangent(double value)
            => (Log(((1 + value) / (1 - value))) * 0.5d);

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HArcsec(X) </remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseHyperbolicSecant(double value)
            => Log(((Sqrt((((value * value) * -1) + 1)) + 1) / value));

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Co-secant
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HArccosec(X)</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseHyperbolicCosecant(double value)
            => Log((((Sin(value) * Sqrt(((value * value) + 1))) + 1) / value));

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Cotangent
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>HArccotan(X)</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseHyperbolicCotangent(double value)
            => (Log(((value + 1) / (value - 1))) * 0.5d);

        /// <summary>
        /// Derived math functions equivalent Base N Logarithm
        /// </summary>
        /// <param name="value"></param>
        /// <param name="numberBase"></param>
        /// <returns></returns>
        /// <remarks>
        /// LogN(X)
        /// Return Log(Value) / Log(NumberBase)
        /// </remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LogarithmTobaseN(double value, double numberBase)
            => (Math.Abs(numberBase - 1) > Epsilon) ? (Log(value) / Log(numberBase)) : 0;

        #endregion

        #region Conversion Extensions

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [Pure]
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Clamp(this double value, double min, double max)
            => value > max ? max : value < min ? min : value;

        /// <summary>
        /// Keep the value between the maximum and minimum.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The lower limit the value should be above.</param>
        /// <param name="max">The upper limit the value should be under.</param>
        /// <returns>A value clamped between the maximum and minimum values.</returns>
        [Pure]
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Clamp<T>(this T value, T min, T max)
            where T : IComparable
            => (value?.CompareTo(min) < 0) ? min : (value?.CompareTo(max) > 0) ? max : value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [Pure]
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Wrap(this double value, double min, double max)
            => (value < min) ? max - (min - value) % (max - min) : min + (value - min) % (max - min);

        /// <summary>
        /// Find the absolute positive value of a radian angle.
        /// </summary>
        /// <param name="angle"></param>
        /// <returns>The absolute positive angle in radians.</returns>
        /// <remarks></remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AbsoluteAngle(this double angle)
        {
            double value = angle % Tau;
            return value < 0 ? value + Tau : value;
        }

        /// <summary>
        /// Reduces a given angle to a value between 2π and -2π.
        /// </summary>
        /// <param name="angle">The angle to reduce, in radians.</param>
        /// <returns>The new angle, in radians.</returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double WrapAngle(this double angle)
        {
            double value = IEEERemainder(angle, Tau);
            return (value <= -PI) ? value + Tau : value - Tau;
        }

        /// <summary>
        /// Imitation of Excel's Mod Operator
        /// </summary>
        /// <param name="valueA">Source parameter</param>
        /// <param name="valueB">Destination parameter</param>
        /// <returns>Returns the same Modulus Result that Excel returns.</returns>
        /// <remarks>Created after finding out Excel returns a different value for the Mod Operator than .Net</remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Modulo(this double valueA, double valueB)
            => ((valueA %= valueB) < 0) ? valueA + valueB : valueA;

        /// <summary>
        /// Convert Degrees to Radians.
        /// </summary>
        /// <param name="degrees">Angle in Degrees.</param>
        /// <returns>Angle in Radians.</returns>
        /// <remarks></remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ToRadians(this double degrees)
            => degrees * Radien;

        /// <summary>
        /// Convert Radians to Degrees.
        /// </summary>
        /// <param name="radiens">Angle in Radians.</param>
        /// <returns>Angle in Degrees.</returns>
        /// <remarks></remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ToDegrees(this double radiens)
            => radiens * Degree;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerStepThrough]
        public static double Round(this float val)
            => (0f < val) ? (int)(val + 0.5f) : (int)(val - 0.5f);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Round(this double val)
            => (0d < val) ? (int)(val + 0.5d) : (int)(val - 0.5d);

        /// <summary>
        /// Round a value to the nearest multiple of a number.
        /// </summary>
        /// <param name="value">The value to round.</param>
        /// <param name="multiple">The multiple to round to.</param>
        /// <returns>Returns a value rounded to an interval of the multiple.</returns>
        /// <remarks>Using Convert.ToInt32 because it is faster and guarantees bankers rounding.</remarks>
        [Pure]
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double RoundToMultiple(this double value, double multiple)
            => Convert.ToInt32(value / multiple) * multiple;

        /// <summary>
        /// Swap left and right values so the left object has the value of the right object and visa-versa. 
        /// </summary>
        /// <param name="a">The left value.</param>
        /// <param name="b">The right value.</param>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Swap<T>(ref T a, ref T b)
        {
            T swap = a;
            a = b;
            b = swap;
        }

        #endregion

        #region Parsing.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ParseFloat(this string text)
            => float.Parse(text, CultureInfo.InvariantCulture);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ParseFloat(this string text, IFormatProvider provider)
            => float.Parse(text, provider);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ParseDouble(this string text)
            => double.Parse(text, CultureInfo.InvariantCulture);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ParseDouble(this string text, IFormatProvider provider)
            => double.Parse(text, provider);

        #endregion

        #region Comparisons

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        /// <remarks>http://pomax.github.io/bezierinfo</remarks>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool approximately(double a, double b, double precision = Epsilon)
            => Math.Abs(a - b) <= precision;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="epsilonSqrd"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AreClose(double aX, double aY, double bX, double bY, double epsilonSqrd = Epsilon)
            => (SquareDistance(aX, aY, bX, bY) <= epsilonSqrd);

        /// <summary>
        /// AreClose - Returns whether or not two doubles are "close".  That is, whether or 
        /// not they are within epsilon of each other.  Note that this epsilon is proportional
        /// to the numbers themselves to that AreClose survives scalar multiplication.
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this 
        /// returns false.  This is important enough to repeat:
        /// NB: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
        /// used for optimizations *only*.
        /// </summary>
        /// <returns>
        /// bool - the result of the AreClose comparison.
        /// </returns>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        /// <param name="epsilon"></param>
        /// <remarks></remarks>
        [Pure]
        public static bool AreClose(this float value1, float value2, float epsilon = FloatEpsilon)
        {
            // in case they are Infinities (then epsilon check does not work)
            if (Math.Abs(value1 - value2) < Epsilon)
                return true;
            // This computes (|value1-value2| / (|value1| + |value2| + 10.0)) < DBL_EPSILON
            float eps = (Math.Abs(value1) + Math.Abs(value2) + 10f) * epsilon;
            float delta = value1 - value2;
            return (-eps < delta) && (eps > delta);
        }

        /// <summary>
        /// AreClose - Returns whether or not two doubles are "close".  That is, whether or 
        /// not they are within epsilon of each other.  Note that this epsilon is proportional
        /// to the numbers themselves to that AreClose survives scalar multiplication.
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this 
        /// returns false.  This is important enough to repeat:
        /// NB: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
        /// used for optimizations *only*.
        /// </summary>
        /// <returns>
        /// bool - the result of the AreClose comparison.
        /// </returns>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        /// <param name="epsilon"></param>
        /// <remarks></remarks>
        [Pure]
        public static bool AreClose(this double value1, double value2, double epsilon = Epsilon)
        {
            // in case they are Infinities (then epsilon check does not work)
            if (Math.Abs(value1 - value2) < Epsilon)
                return true;
            // This computes (|value1-value2| / (|value1| + |value2| + 10.0)) < DBL_EPSILON
            double eps = (Math.Abs(value1) + Math.Abs(value2) + 10d) * epsilon;
            double delta = value1 - value2;
            return (-eps < delta) && (eps > delta);
        }

        /// <summary>
        /// LessThan - Returns whether or not the first double is less than the second double.
        /// That is, whether or not the first is strictly less than *and* not within epsilon of
        /// the other number.  Note that this epsilon is proportional to the numbers themselves
        /// to that AreClose survives scalar multiplication.  Note,
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this 
        /// returns false.  This is important enough to repeat:
        /// NB: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
        /// used for optimizations *only*.
        /// </summary>
        /// <returns>
        /// bool - the result of the LessThan comparison.
        /// </returns>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThan(this float value1, float value2)
            => (value1 < value2) && !AreClose(value1, value2);

        /// <summary>
        /// LessThan - Returns whether or not the first double is less than the second double.
        /// That is, whether or not the first is strictly less than *and* not within epsilon of
        /// the other number.  Note that this epsilon is proportional to the numbers themselves
        /// to that AreClose survives scalar multiplication.  Note,
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this 
        /// returns false.  This is important enough to repeat:
        /// NB: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
        /// used for optimizations *only*.
        /// </summary>
        /// <returns>
        /// bool - the result of the LessThan comparison.
        /// </returns>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThan(this double value1, double value2)
            => (value1 < value2) && !AreClose(value1, value2);

        /// <summary>
        /// GreaterThan - Returns whether or not the first double is greater than the second double.
        /// That is, whether or not the first is strictly greater than *and* not within epsilon of
        /// the other number.  Note that this epsilon is proportional to the numbers themselves
        /// to that AreClose survives scalar multiplication.  Note,
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this 
        /// returns false.  This is important enough to repeat:
        /// NB: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
        /// used for optimizations *only*.
        /// </summary>
        /// <returns>
        /// bool - the result of the GreaterThan comparison.
        /// </returns>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThan(this float value1, float value2)
            => (value1 > value2) && !AreClose(value1, value2);

        /// <summary>
        /// GreaterThan - Returns whether or not the first double is greater than the second double.
        /// That is, whether or not the first is strictly greater than *and* not within epsilon of
        /// the other number.  Note that this epsilon is proportional to the numbers themselves
        /// to that AreClose survives scalar multiplication.  Note,
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this 
        /// returns false.  This is important enough to repeat:
        /// NB: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
        /// used for optimizations *only*.
        /// </summary>
        /// <returns>
        /// bool - the result of the GreaterThan comparison.
        /// </returns>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThan(this double value1, double value2)
            => (value1 > value2) && !AreClose(value1, value2);

        /// <summary>
        /// LessThanOrClose - Returns whether or not the first double is less than or close to
        /// the second double.  That is, whether or not the first is strictly less than or within
        /// epsilon of the other number.  Note that this epsilon is proportional to the numbers 
        /// themselves to that AreClose survives scalar multiplication.  Note,
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this 
        /// returns false.  This is important enough to repeat:
        /// NB: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
        /// used for optimizations *only*.
        /// </summary>
        /// <returns>
        /// bool - the result of the LessThanOrClose comparison.
        /// </returns>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThanOrClose(this float value1, float value2)
            => (value1 < value2) || AreClose(value1, value2);

        /// <summary>
        /// LessThanOrClose - Returns whether or not the first double is less than or close to
        /// the second double.  That is, whether or not the first is strictly less than or within
        /// epsilon of the other number.  Note that this epsilon is proportional to the numbers 
        /// themselves to that AreClose survives scalar multiplication.  Note,
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this 
        /// returns false.  This is important enough to repeat:
        /// NB: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
        /// used for optimizations *only*.
        /// </summary>
        /// <returns>
        /// bool - the result of the LessThanOrClose comparison.
        /// </returns>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LessThanOrClose(this double value1, double value2)
            => (value1 < value2) || AreClose(value1, value2);

        /// <summary>
        /// GreaterThanOrClose - Returns whether or not the first float is greater than or close to
        /// the second float.  That is, whether or not the first is strictly greater than or within
        /// epsilon of the other number.  Note that this epsilon is proportional to the numbers 
        /// themselves to that AreClose survives scalar multiplication.  Note,
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this 
        /// returns false.  This is important enough to repeat:
        /// NB: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
        /// used for optimizations *only*.
        /// </summary>
        /// <returns>
        /// bool - the result of the GreaterThanOrClose comparison.
        /// </returns>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThanOrClose(this float value1, float value2)
            => (value1 > value2) || AreClose(value1, value2);

        /// <summary>
        /// GreaterThanOrClose - Returns whether or not the first double is greater than or close to
        /// the second double.  That is, whether or not the first is strictly greater than or within
        /// epsilon of the other number.  Note that this epsilon is proportional to the numbers 
        /// themselves to that AreClose survives scalar multiplication.  Note,
        /// There are plenty of ways for this to return false even for numbers which
        /// are theoretically identical, so no code calling this should fail to work if this 
        /// returns false.  This is important enough to repeat:
        /// NB: NO CODE CALLING THIS FUNCTION SHOULD DEPEND ON ACCURATE RESULTS - this should be
        /// used for optimizations *only*.
        /// </summary>
        /// <returns>
        /// bool - the result of the GreaterThanOrClose comparison.
        /// </returns>
        /// <param name="value1"> The first double to compare. </param>
        /// <param name="value2"> The second double to compare. </param>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool GreaterThanOrClose(this double value1, double value2)
            => (value1 > value2) || AreClose(value1, value2);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NearZero(double value, double epsilon = NearZeroEpsilon)
            => (value > -epsilon) && (value < -epsilon);

        /// <summary>
        /// IsZero - Returns whether or not the double is "close" to 0.  Same as AreClose(double, 0),
        /// but this is faster.
        /// </summary>
        /// <returns>
        /// bool - the result of the AreClose comparison.
        /// </returns>
        /// <param name="value"> The double to compare to 0. </param>
        /// <param name="epsilon"></param>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero(this float value, float epsilon = FloatEpsilon)
            => Math.Abs(value) < 10f * epsilon;

        /// <summary>
        /// IsZero - Returns whether or not the double is "close" to 0.  Same as AreClose(double, 0),
        /// but this is faster.
        /// </summary>
        /// <returns>
        /// bool - the result of the AreClose comparison.
        /// </returns>
        /// <param name="value"> The double to compare to 0. </param>
        /// <param name="epsilon"></param>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero(this double value, double epsilon = Epsilon)
            => Math.Abs(value) < 10d * epsilon;

        /// <summary>
        /// IsOne - Returns whether or not the double is "close" to 1.  Same as AreClose(double, 1),
        /// but this is faster.
        /// </summary>
        /// <returns>
        /// bool - the result of the AreClose comparison.
        /// </returns>
        /// <param name="value"> The double to compare to 1. </param>
        /// <param name="epsilon"></param>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOne(this float value, float epsilon = FloatEpsilon)
            => Math.Abs(value - 1f) < 10f * epsilon;

        /// <summary>
        /// IsOne - Returns whether or not the double is "close" to 1.  Same as AreClose(double, 1),
        /// but this is faster.
        /// </summary>
        /// <returns>
        /// bool - the result of the AreClose comparison.
        /// </returns>
        /// <param name="value"> The double to compare to 1. </param>
        /// <param name="epsilon"></param>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOne(this double value, double epsilon = Epsilon)
            => Math.Abs(value - 1d) < 10d * epsilon;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBetweenZeroAndOne(this float val)
            => (GreaterThanOrClose(val, 0f) && LessThanOrClose(val, 1));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBetweenZeroAndOne(this double val)
            => (GreaterThanOrClose(val, 0d) && LessThanOrClose(val, 1));

        #endregion
    }
}
