// <copyright file="Operations.Trignometrics.cs" company="Shkyrockett" >
//     Copyright © 2005 - 2020 Shkyrockett. All rights reserved.
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
using static Engine.Maths;
using static System.Math;

namespace Engine
{
    /// <summary>
    /// The maths class.
    /// </summary>
    public static partial class Operations
    {
        /// <summary>
        /// Rotates the angle vector.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="cos">The cos.</param>
        /// <param name="sin">The sin.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double cos, double sin) RotateAngleVector(double x, double y, double cos, double sin) => ((x * cos) - (y * sin), (x * sin) + (y * cos));

        /// <summary>
        /// Find the incidence category of vector Angles.
        /// </summary>
        /// <param name="cos1">The cos1.</param>
        /// <param name="sin1">The sin1.</param>
        /// <param name="cos2">The cos2.</param>
        /// <param name="sin2">The sin2.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Incidence AngleVectorIncidence(double cos1, double sin1, double cos2, double sin2, double epsilon = double.Epsilon)
        {
            var crossProduct = CrossProduct(cos1, sin1, cos2, sin2);
            return Math.Abs(crossProduct) < epsilon
                ? Incidence.Parallel
                : Math.Abs(1d - crossProduct) < epsilon
                ? Incidence.Perpendicular : Incidence.Oblique;
        }

        /// <summary>
        /// Convert Degrees to Radians.
        /// </summary>
        /// <param name="degrees">Angle in Degrees.</param>
        /// <returns>
        /// Angle in Radians.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DegreesToRadians(this double degrees) => degrees * Radian;

        /// <summary>
        /// Convert Radians to Degrees.
        /// </summary>
        /// <param name="radians">Angle in Radians.</param>
        /// <returns>
        /// Angle in Degrees.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double RadiansToDegrees(this double radians) => radians * Degree;

        /// <summary>
        /// Slopes to radians.
        /// </summary>
        /// <param name="slope">The slope.</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SlopeToRadians(this double slope) => Atan(slope);

        /// <summary>
        /// The polar to Cartesian.
        /// </summary>
        /// <param name="centerX">The centerX.</param>
        /// <param name="centerY">The centerY.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="theta">The angleInRadians.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2}" />.
        /// </returns>
        /// <acknowledgment>
        /// https://codereview.stackexchange.com/q/183
        /// </acknowledgment>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) PolarToCartesian(double centerX, double centerY, double radius, double theta)
        {
            var sin = Sin(theta);

            // This is faster than:
            // double cos = Math.Cos(theta);
            var cos = -Sqrt(1d - (sin * sin));
            return (
                X: centerX + (radius * cos),
                Y: centerY + (radius * sin)
                );
        }

        /// <summary>
        /// The Cartesian to polar.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="centerX">The centerX.</param>
        /// <param name="centerY">The centerY.</param>
        /// <returns>
        /// The <see cref="ValueTuple{T1, T2}" />.
        /// </returns>
        /// <acknowledgment>
        /// https://stackoverflow.com/a/34315013
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double Radius, double Theta) CartesianToPolar(double x, double y, double centerX = 0d, double centerY = 0d)
        {
            var dx = x - centerX;
            var dy = y - centerY;
            var radius = Sqrt((dx * dx) + (dy * dy));
            var angle = Atan2(dy, dx);
            return (radius, angle);
        }

        /// <summary>
        /// Find the absolute positive value of a radian angle.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <returns>
        /// The absolute positive angle in radians.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AbsoluteAngle(this double angle)
        {
            if (double.IsNaN(angle))
            {
                return angle;
            }
            // ToDo: Need to do some testing to figure out which method is more appropriate.
            //double value = angle % Tau;
            //double value = IEEERemainder(angle, Tau);
            // The active ingredient of the IEEERemainder method is extracted here.
            var value = angle - (Tau * Math.Round(angle * InverseTau));
            return value < 0d ? value + Tau : value;
        }

        /// <summary>
        /// The normalize radian.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double NormalizeRadian(double angle)
        {
            var value = (angle + PI) % Tau;
            value += value > 0d ? -PI : PI;
            return value;
        }

        /// <summary>
        /// Reduces a given angle to a value between 2π and -2π.
        /// </summary>
        /// <param name="angle">The angle to reduce, in radians.</param>
        /// <returns>
        /// The new angle, in radians.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double WrapAngleModulus(this double angle)
        {
            if (double.IsNaN(angle))
            {
                return angle;
            }

            var value = angle % Tau;
            return (value <= -PI) ? value + Tau : value - Tau;
        }

        /// <summary>
        /// Reduces a given angle to a value between 2π and -2π.
        /// </summary>
        /// <param name="angle">The angle to reduce, in radians.</param>
        /// <returns>
        /// The new angle, in radians.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double WrapAngle(this double angle)
        {
            if (double.IsNaN(angle))
            {
                return angle;
            }
            // The IEEERemainder method works better than the % modulus operator in this case, even if it is slower.
            //double value = IEEERemainder(angle, Tau);
            // The active ingredient of the IEEERemainder method is extracted here for performance reasons.
            var value = angle - (Tau * Math.Round(angle * InverseTau));
            return (value <= -PI) ? value + Tau : value - Tau;
        }

        /// <summary>
        /// Imitation of Excel's Mod Operator
        /// </summary>
        /// <param name="valueA">Source parameter</param>
        /// <param name="valueB">Destination parameter</param>
        /// <returns>
        /// Returns the same Modulus Result that Excel returns.
        /// </returns>
        /// <remarks>
        /// <para>Created after finding out Excel returns a different value for the Mod Operator than .Net</para>
        /// </remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Modulo(this double valueA, double valueB) => ((valueA %= valueB) < 0d) ? valueA + valueB : valueA;

        /// <summary>
        /// The angle.
        /// </summary>
        /// <param name="cos">The Cosine.</param>
        /// <param name="sin">The Sine.</param>
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Angle(double cos, double sin) => Atan2(-sin, cos);

        /// <summary>
        /// Returns the Angle of a line.
        /// </summary>
        /// <param name="x1">Horizontal Component of Point Starting Point</param>
        /// <param name="y1">Vertical Component of Point Starting Point</param>
        /// <param name="x2">Horizontal Component of Ending Point</param>
        /// <param name="y2">Vertical Component of Ending Point</param>
        /// <returns>
        /// Returns the Angle of a line.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Angle(
            double x1, double y1,
            double x2, double y2) => Atan2(y1 - y2, x1 - x2);

        /// <summary>
        /// The angle.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="z1">The z1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="z2">The z2.</param>
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        /// <acknowledgment>
        /// http://www.codeproject.com/Articles/17425/A-Vector-Type-for-C
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Angle(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => (Math.Abs(x1 - x2) < double.Epsilon
            && Math.Abs(y1 - y2) < double.Epsilon
            && Math.Abs(z1 - z2) < double.Epsilon) ? 0d : Acos(Math.Min(1d, DotProduct(Normalize(x1, y1, z1), Normalize(x2, y2, z2))));

        /// <summary>
        /// The angle vector.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="x3">The x3.</param>
        /// <param name="y3">The y3.</param>
        /// <returns>
        /// Return the angle ABC.
        /// Return a value between PI and -PI.
        /// Note that the value is the opposite of what you might
        /// expect because Y coordinates increase downward.
        /// </returns>
        /// <acknowledgment>
        /// http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AngleVector(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3) => Atan2(CrossProductTriple(x1, y1, x2, y2, x3, y3), DotProductTriple(x1, y1, x2, y2, x3, y3));

        /// <summary>
        /// Find the absolute positive value of a radian angle from two points.
        /// </summary>
        /// <param name="x1">Horizontal Component of Point Starting Point</param>
        /// <param name="y1">Vertical Component of Point Starting Point</param>
        /// <param name="x2">Horizontal Component of Ending Point</param>
        /// <param name="y2">Vertical Component of Ending Point</param>
        /// <returns>
        /// The absolute angle of a line in radians.
        /// </returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AbsoluteAngle(
            double x1, double y1,
            double x2, double y2)
        {
            // Find the angle of point a and point b.
            var test = -Angle(x1, y1, x2, y2) % PI;
            return test < 0d ? test += PI : test;
        }

        /// <summary>
        /// Finds the angle between two vectors.
        /// </summary>
        /// <param name="uX">The uX.</param>
        /// <param name="uY">The uY.</param>
        /// <param name="vX">The vX.</param>
        /// <param name="vY">The vY.</param>
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        /// <acknowledgment>
        /// http://james-ramsden.com/angle-between-two-vectors/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AngleBetween(
            double uX, double uY,
            double vX, double vY) => Acos(((uX * vX) + (uY * vY)) / Sqrt(((uX * uX) + (uY * uY)) * ((vX * vX) + (vY * vY))));

        /// <summary>
        /// Finds the angle between two vectors.
        /// </summary>
        /// <param name="uX">The uX.</param>
        /// <param name="uY">The uY.</param>
        /// <param name="uZ">The uZ.</param>
        /// <param name="vX">The vX.</param>
        /// <param name="vY">The vY.</param>
        /// <param name="vZ">The vZ.</param>
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        /// <acknowledgment>
        /// http://james-ramsden.com/angle-between-two-vectors/
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AngleBetween(
            double uX, double uY, double uZ,
            double vX, double vY, double vZ) => Acos(((uX * vX) + (uY * vY) + (uZ * vZ)) / Sqrt(((uX * uX) + (uY * uY) + (uZ * uZ)) * ((vX * vX) + (vY * vY) + (vZ * vZ))));

        /// <summary>
        /// Find the elliptical t that matches the coordinates of a circular angle.
        /// </summary>
        /// <param name="angle">The angle to transform into elliptic angle.</param>
        /// <param name="rx">The first radius of the ellipse.</param>
        /// <param name="ry">The second radius of the ellipse.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Based on the answer by flup at: https://stackoverflow.com/a/17762156/7004229
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double EllipticalPolarAngle(double angle, double rx, double ry)
        {
            // Wrap the angle between -2PI and 2PI.
            var theta = angle % Tau;

            // Find the elliptical t that matches the circular angle.
            if (Math.Abs(theta) == HalfPi || Math.Abs(theta) == Pau)
            {
                return angle;
            }

            if (theta > HalfPi && theta < Pau)
            {
                return Atan(rx * Tan(theta) / ry) + PI;
            }

            if (theta < -HalfPi && theta > -Pau)
            {
                return Atan(rx * Tan(theta) / ry) - PI;
            }

            return Atan(rx * Tan(theta) / ry);
        }

        /// <summary>
        /// Find the elliptical (cos(t), sin(t)) that matches the coordinates of a circular angle.
        /// </summary>
        /// <param name="cosA">The cos a.</param>
        /// <param name="sinA">The sin a.</param>
        /// <param name="rx">The first radius of the ellipse.</param>
        /// <param name="ry">The second radius of the ellipse.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Vectorized version of the answer by flup at: https://stackoverflow.com/a/17762156/7004229
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double cosT, double sinT) EllipticalPolarVector(double cosA, double sinA, double rx, double ry)
        {
            // Find the elliptical t that matches the circular angle.
            if (cosA > -1d && cosA < 0d || cosA > 0d && cosA < 1d)
            {
                var d = Sign(cosA);
                return (d / Sqrt(1d + (rx * rx * sinA * sinA / (ry * ry * cosA * cosA))),
                        d * (rx * sinA / (ry * cosA * Sqrt(1d + (rx * rx * sinA * sinA / (ry * ry * cosA * cosA))))));
            }

            return (cosA, sinA);
        }

        /// <summary>
        /// Return a "correction" angle that converts a subtended angle to a parametric angle for an
        /// ellipse with radii a and b.
        /// </summary>
        /// <param name="subtended">The subtended.</param>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SubtendedToParametric(double subtended, double a, double b) => SubtendedToParametric(Cos(subtended), Sin(subtended), a, b);

        /// <summary>
        /// Return a "correction" angle that converts a subtended angle to a parametric angle for an
        /// ellipse with radii a and b.
        /// </summary>
        /// <param name="subtendedCos">The subtended cos.</param>
        /// <param name="subtendedSin">The subtended sin.</param>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Code ported from: https://www.khanacademy.org/computer-programming/e/6221186997551104
        /// Math from: http://mathworld.wolfram.com/Ellipse-LineIntersection.html
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SubtendedToParametric(double subtendedCos, double subtendedSin, double a, double b)
        {
            if (a == b)
            {
                // Circle needs no correction.
                return 0;
            }

            // A ray from the origin.
            var rx = subtendedCos;
            var ry = subtendedSin;
            var e = a * b / Sqrt((a * a * ry * ry) + (b * b * rx * rx));

            // Where ray intersects ellipse.
            var ex = e * rx;
            var ey = e * ry;

            // Normalized.
            var parametric = Atan2(a * ey, b * ex);
            var subtended = Atan2(ry, rx);
            return parametric - subtended;
        }

        #region Reflect
        /// <summary>
        /// Calculates the reflection of a point off a line segment
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="axisX">The axis x.</param>
        /// <param name="axisY">The axis y.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J) Reflect(double x1, double y1, double x2, double y2, double axisX, double axisY)
        {
            var (i, j) = DeltaVector(x1, y1, x2, y2);
            var magnatude = 0.5d * DotProduct(i, j, i, j);
            var reflection = CrossProduct(i, j, CrossProduct(x2, y2, x1, y1), DotProduct(axisX, axisY, i, j));
            return ((magnatude * reflection) - axisX,
                    (magnatude * reflection) - axisY);
        }
        #endregion Reflect

        #region Rotate Point
        /// <summary>
        /// Rotate a point around the world origin.
        /// </summary>
        /// <param name="x">The x component of the point to rotate.</param>
        /// <param name="y">The y component of the point to rotate.</param>
        /// <param name="angle">The angle to rotate in pi radians.</param>
        /// <returns>
        /// A point rotated about the origin by the specified pi radian angle.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) RotatePoint2D(double x, double y, double angle) => RotatePoint2D(x, y, Cos(angle), Sin(angle), 0d, 0d);

        /// <summary>
        /// Rotate a point around the world origin.
        /// </summary>
        /// <param name="x">The x component of the point to rotate.</param>
        /// <param name="y">The y component of the point to rotate.</param>
        /// <param name="cos">The cos.</param>
        /// <param name="sin">The sin.</param>
        /// <returns>
        /// A point rotated about the origin by the specified pi radian angle.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) RotatePoint2D(double x, double y, double cos, double sin) => RotatePoint2D(x, y, cos, sin, 0d, 0d);

        /// <summary>
        /// Rotate a point around a fulcrum point.
        /// </summary>
        /// <param name="x">The x component of the point to rotate.</param>
        /// <param name="y">The y component of the point to rotate.</param>
        /// <param name="angle">The angle to rotate the point in pi radians.</param>
        /// <param name="cx">The x component of the fulcrum point to rotate the point around.</param>
        /// <param name="cy">The y component of the fulcrum point to rotate the point around.</param>
        /// <returns>
        /// A point rotated about the fulcrum point by the specified pi radian angle.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) RotatePoint2D(double x, double y, double angle, double cx, double cy) => RotatePoint2D(x, y, Cos(angle), Sin(angle), cx, cy);

        /// <summary>
        /// Rotate a point around a fulcrum point.
        /// </summary>
        /// <param name="x">The x component of the point to rotate.</param>
        /// <param name="y">The y component of the point to rotate.</param>
        /// <param name="cos">The cos.</param>
        /// <param name="sin">The sin.</param>
        /// <param name="cx">The x component of the fulcrum point to rotate the point around.</param>
        /// <param name="cy">The y component of the fulcrum point to rotate the point around.</param>
        /// <returns>
        /// A point rotated about the fulcrum point by the specified pi radian angle.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double X, double Y) RotatePoint2D(double x, double y, double cos, double sin, double cx, double cy)
        {
            var deltaX = x - cx;
            var deltaY = y - cy;
            return (cx + ((deltaX * cos) - (deltaY * sin)),
                    cy + ((deltaX * sin) + (deltaY * cos)));
        }
        #endregion Rotate Point

        #region Slope
        /// <summary>
        /// Calculates the Slope of a vector.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <returns>
        /// Returns the slope angle of a vector.
        /// </returns>
        /// <remarks>
        /// <para>The slope is calculated with Slope = Y / X or rise over run
        /// If the line is vertical, return something close to infinity
        /// (Close to the largest value allowed for the data type).
        /// Otherwise calculate and return the slope.</para>
        /// </remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Slope(double i, double j) => Math.Abs(i) < double.Epsilon ? SlopeMax : (j / i);

        /// <summary>
        /// Returns the slope angle of a line.
        /// </summary>
        /// <param name="x1">Horizontal Component of Point Starting Point</param>
        /// <param name="y1">Vertical Component of Point Starting Point</param>
        /// <param name="x2">Horizontal Component of Ending Point</param>
        /// <param name="y2">Vertical Component of Ending Point</param>
        /// <returns>
        /// Returns the slope angle of a line.
        /// </returns>
        /// <remarks>
        /// <para>If the Line is Vertical return something close to infinity (Close to
        /// the largest value allowed for the data type).
        /// Otherwise calculate and return the slope.</para>
        /// </remarks>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Slope(
            double x1, double y1,
            double x2, double y2) => (Math.Abs(x1 - x2) < double.Epsilon) ? SlopeMax : ((y2 - y1) / (x2 - x1));
        #endregion Slope

        #region Unit
        /// <summary>
        /// Unit of a 2D Vector.
        /// </summary>
        /// <param name="i">The i component of the Vector to Unitize.</param>
        /// <param name="j">The j component of the Vector to Unitize.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J) Unit(double i, double j) => ScaleVector(i, j, 1d / Sqrt((i * i) + (j * j)));

        /// <summary>
        /// Unit of a 3D Vector.
        /// </summary>
        /// <param name="i">The i component of the Vector to Unitize.</param>
        /// <param name="j">The j component of the Vector to Unitize.</param>
        /// <param name="k">The k component of the Vector to Unitize.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J, double K) Unit(double i, double j, double k) => ScaleVector(i, j, k, 1d / Sqrt((i * i) + (j * j) + (k * k)));

        /// <summary>
        /// Unit of a 4D Vector.
        /// </summary>
        /// <param name="i">The i component of the Vector to Unitize.</param>
        /// <param name="j">The j component of the Vector to Unitize.</param>
        /// <param name="k">The k component of the Vector to Unitize.</param>
        /// <param name="l">The l component of the Vector to Unitize.</param>
        /// <returns></returns>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (double I, double J, double K, double L) Unit(double i, double j, double k, double l) => ScaleVector(i, j, k, l, 1d / Sqrt((i * i) + (j * j) + (k * k) + (l * l)));
        #endregion Unit

        #region Derived Equivalent Math Functions
        /// <summary>
        /// The abs.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Abs(double i, double j) => Magnitude(i, j);

        /// <summary>
        /// The abs.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <param name="k">The k.</param>
        /// <returns>
        /// The <see cref="double" />.
        /// </returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Abs(double i, double j, double k) => Magnitude(i, j, k);

        /// <summary>
        /// Derived math functions equivalent Secant
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Secant(double value)
            => (value % PI == HalfPi)
            && (value % PI == -HalfPi)
            ? (1d / Cos(value)) : 0d;

        /// <summary>
        /// Derived math functions equivalent  Co-secant
        /// </summary>
        /// <param name="Value">The value.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cosecant(double Value)
            => (Value % PI == 0d)
            && (Value % PI == PI)
            ? (1d / Sin(Value)) : 0d;

        /// <summary>
        /// Derived math functions equivalent Cotangent
        /// </summary>
        /// <param name="Value">The value.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Cotangent(double Value)
            => (Value % PI == 0d)
            && (Value % PI == PI)
            ? (1d / Tan(Value)) : 0d;

        /// <summary>
        /// Derived math functions equivalent Inverse Sine
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseSine(double value)
        {
            if (value == 1d)
            {
                return HalfPi;
            }
            else if (value == -1d)
            {
                return -HalfPi;
            }
            else if (Math.Abs(value) < 1d)
            {
                // Arc-sin(X)
                return Atan(value / Sqrt((-value * value) + 1d));
            }

            return 0d;
        }

        /// <summary>
        /// Derived math functions equivalent Inverse Cosine
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseCosine(double value)
        {
            if (value == 1d)
            {
                return 0d;
            }
            else if (value == -1d)
            {
                return PI;
            }
            else if (Math.Abs(value) < 1d)
            {
                // Arc-cos(X)
                return Atan(-value / Sqrt((-value * value) + 1d)) + (2d * Atan(1));
            }

            return 0d;
        }

        /// <summary>
        /// Derived math functions equivalent Inverse Secant
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseSecant(double value)
        {
            if (value == 1d)
            {
                return 0d;
            }
            else if (value == -1d)
            {
                return PI;
            }
            else if (Math.Abs(value) < 1d)
            {
                // Arc-sec(X)
                return Atan(value / Sqrt((value * value) - 1d)) + (Sin(value - 1d) * (2d * Atan(1)));
            }

            return 0d;
        }

        /// <summary>
        /// Derived math functions equivalent Inverse Co-secant
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseCosecant(double value)
        {
            if (value == 1d)
            {
                return HalfPi;
            }
            else if (value == -1d)
            {
                return -HalfPi;
            }
            else if (Math.Abs(value) < 1d)
            {
                // Arc-co-sec(X)
                return Atan(value / Sqrt((value * value) - 1d)) + ((Sin(value) - 1d) * (2d * Atan(1)));
            }

            return 0d;
        }

        /// <summary>
        /// Derived math functions equivalent Inverse Cotangent
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <remarks>
        /// <para>Arc-co-tan(X)</para>
        /// </remarks>
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseCotangent(double value) => Atan(value) + (2d * Atan(1d));

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Sine
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <remarks>
        /// <para>HSin(X)</para>
        /// </remarks>
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double HyperbolicSine(double value) => (Exp(value) - Exp(value * -1d)) * 0.5d;

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Cosine
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <remarks>
        /// <para>HCos(X)</para>
        /// </remarks>
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double HyperbolicCosine(double value) => (Exp(value) + Exp(value * -1d)) * 0.5d;

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Tangent
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <remarks>
        /// <para>HTan(X)</para>
        /// </remarks>
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double HyperbolicTangent(double value) => (Exp(value) - Exp(value * -1d)) / (Exp(value) + Exp(value * -1d));

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Secant
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <remarks>
        /// <para>HSec(X)</para>
        /// </remarks>
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double HyperbolicSecant(double value) => 0.5d * (Exp(value) + Exp(value * -1d));

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Co-secant
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <remarks>
        /// <para>HCosec(X)</para>
        /// </remarks>
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double HyperbolicCosecant(double value) => 0.5d * (Exp(value) - Exp(value * -1d));

        /// <summary>
        /// Derived math functions equivalent Hyperbolic Cotangent
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <remarks>
        /// <para>HCotan(X)</para>
        /// </remarks>
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double HyperbolicCotangent(double value) => (Exp(value) + Exp(value * -1d)) / (Exp(value) - Exp(value * -1d));

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Sine
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <remarks>
        /// <para>HArcsin(X)</para>
        /// </remarks>
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseHyperbolicSine(double value) => Log(value + Sqrt((value * value) + 1d));

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Cosine
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <remarks>
        /// <para>HArccos(X)</para>
        /// </remarks>
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseHyperbolicCosine(double value) => Log(value + Sqrt((value * value) - 1d));

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Tangent
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <remarks>
        /// <para>HArctan(X)</para>
        /// </remarks>
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseHyperbolicTangent(double value) => Log((1d + value) / (1d - value)) * 0.5d;

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Secant
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <remarks>
        /// <para>HArcsec(X)</para>
        /// </remarks>
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseHyperbolicSecant(double value) => Log((Sqrt((value * value * -1d) + 1d) + 1d) / value);

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Co-secant
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <remarks>
        /// <para>HArccosec(X)</para>
        /// </remarks>
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseHyperbolicCosecant(double value) => Log(((Sin(value) * Sqrt((value * value) + 1d)) + 1d) / value);

        /// <summary>
        /// Derived math functions equivalent Inverse Hyperbolic Cotangent
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <remarks>
        /// <para>HArccotan(X)</para>
        /// </remarks>
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double InverseHyperbolicCotangent(double value) => Log((value + 1d) / (value - 1d)) * 0.5d;

        /// <summary>
        /// Derived math functions equivalent Base N Logarithm
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="numberBase">The number base.</param>
        /// <returns></returns>
        /// <remarks>
        /// <para>LogN(X)
        /// Return Log(Value) / Log(NumberBase)</para>
        /// </remarks>
        /// <acknowledgment>
        /// Translated from old Microsoft VB code examples that I have since lost.
        /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
        /// </acknowledgment>
        //[DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LogarithmTobaseN(double value, double numberBase) => (numberBase == 1d) ? (Log(value) / Log(numberBase)) : 0d;
        #endregion Derived Equivalent Math Functions
    }
}
