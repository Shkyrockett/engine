// <copyright file="Operations.Trignometrics.cs" company="Shkyrockett" >
// Copyright © 2005 - 2024 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks></remarks>

using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using static System.Math;

namespace Engine;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T cos, T sin) RotateAngleVector<T>(T x, T y, T cos, T sin)
        where T : IFloatingPointIeee754<T> => ((x * cos) - (y * sin), (x * sin) + (y * cos));

    /// <summary>
    /// Rotates the angle vector.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="cos">The cos.</param>
    /// <param name="sin">The sin.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (R cos, R sin) RotateAngleVector<T, R>(T x, T y, R cos, R sin)
        where T : INumberBase<T> where R : IFloatingPointIeee754<R> => ((R.CreateSaturating(x) * cos) - (R.CreateSaturating(y) * sin), (R.CreateSaturating(x) * sin) + (R.CreateSaturating(y) * cos));

    /// <summary>
    /// Find the incidence category of vector Angles.
    /// </summary>
    /// <param name="cos1">The cos1.</param>
    /// <param name="sin1">The sin1.</param>
    /// <param name="cos2">The cos2.</param>
    /// <param name="sin2">The sin2.</param>
    /// <param name="epsilon">The epsilon.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static Incidence AngleVectorIncidence<T>(T cos1, T sin1, T cos2, T sin2, T? epsilon = default)
        where T : IFloatingPointIeee754<T>
    {
        epsilon ??= T.Epsilon;

        var crossProduct = CrossProduct(cos1, sin1, cos2, sin2);
        return T.Abs(crossProduct) < epsilon
            ? Incidence.Parallel
            : T.Abs(T.One - crossProduct) < epsilon
            ? Incidence.Perpendicular : Incidence.Oblique;
    }

    /// <summary>
    /// Convert Degrees to Radians.
    /// </summary>
    /// <param name="degrees">Angle in Degrees.</param>
    /// <returns>
    /// Angle in Radians.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T DegreesToRadians<T>(this T degrees) where T : IFloatingPointIeee754<T> => degrees * Floats<T>.Radian;

    /// <summary>
    /// Convert Degrees to Radians.
    /// </summary>
    /// <param name="degrees">Angle in Degrees.</param>
    /// <returns>
    /// Angle in Radians.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static R DegreesToRadians<T, R>(this T degrees) where T : INumberBase<T> where R : IFloatingPointIeee754<R> => R.CreateSaturating(degrees) * Floats<R>.Radian;

    /// <summary>
    /// Convert Radians to Degrees.
    /// </summary>
    /// <param name="radians">Angle in Radians.</param>
    /// <returns>
    /// Angle in Degrees.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T RadiansToDegrees<T>(this T radians) where T : IFloatingPointIeee754<T> => radians * Floats<T>.Degree;

    /// <summary>
    /// Convert Radians to Degrees.
    /// </summary>
    /// <param name="radians">Angle in Radians.</param>
    /// <returns>
    /// Angle in Degrees.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static R RadiansToDegrees<T, R>(this T radians) where T : IFloatingPointIeee754<T> where R : INumberBase<R> => R.CreateSaturating(radians * Floats<T>.Degree);

    /// <summary>
    /// Slopes to radians.
    /// </summary>
    /// <param name="slope">The slope.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T SlopeToRadians<T>(this T slope) where T : IFloatingPointIeee754<T> => T.Atan(slope);

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T X, T Y) PolarToCartesian<T>(T centerX, T centerY, T radius, T theta)
        where T : IFloatingPointIeee754<T>
    {
        var sin = T.Sin(theta);

        // This is faster than:
        // double cos = Math.Cos(theta);
        var cos = -T.Sqrt(T.One - (sin * sin));
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T Radius, T Theta) CartesianToPolar<T>(T x, T y, T? centerX = default, T? centerY = default)
        where T : IFloatingPointIeee754<T>
    {
        centerX ??= T.Zero;
        centerY ??= T.Zero;

        var dx = x - centerX;
        var dy = y - centerY;
        var radius = T.Sqrt((dx * dx) + (dy * dy));
        var angle = T.Atan2(dy, dx);
        return (radius, angle);
    }

    /// <summary>
    /// Find the absolute positive value of a radian angle.
    /// </summary>
    /// <param name="angle">The angle.</param>
    /// <returns>
    /// The absolute positive angle in radians.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T AbsoluteAngle<T>(this T angle)
        where T : IFloatingPointIeee754<T>
    {
        if (T.IsNaN(angle))
        {
            return angle;
        }
        // ToDo: Need to do some testing to figure out which method is more appropriate.
        //T value = angle % T.Tau;
        //T value = IEEERemainder(angle, T.Tau);
        // The active ingredient of the IEEERemainder method is extracted here.
        var value = angle - (T.Tau * T.Round(angle * (T.One / T.Tau)));
        return value < T.Zero ? value + T.Tau : value;
    }

    /// <summary>
    /// The normalize radian.
    /// </summary>
    /// <param name="angle">The angle.</param>
    /// <returns>
    /// The <see cref="T" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T NormalizeRadian<T>(T angle)
        where T : IFloatingPointIeee754<T>
    {
        var value = (angle + T.Pi) % T.Tau;
        value += value > T.Zero ? -T.Pi : T.Pi;
        return value;
    }

    /// <summary>
    /// Reduces a given angle to a value between 2π and -2π.
    /// </summary>
    /// <param name="angle">The angle to reduce, in radians.</param>
    /// <returns>
    /// The new angle, in radians.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T WrapAngleModulus<T>(this T angle)
        where T : IFloatingPointIeee754<T>
    {
        if (T.IsNaN(angle))
        {
            return angle;
        }

        var value = angle % T.Tau;
        return (value <= -T.Pi) ? value + T.Tau : value - T.Tau;
    }

    /// <summary>
    /// Reduces a given angle to a value between 2π and -2π.
    /// </summary>
    /// <param name="angle">The angle to reduce, in radians.</param>
    /// <returns>
    /// The new angle, in radians.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T WrapAngle<T>(this T angle)
        where T : IFloatingPointIeee754<T>
    {
        if (T.IsNaN(angle))
        {
            return angle;
        }
        // The IEEERemainder method works better than the % modulus operator in this case, even if it is slower.
        //double value = IEEERemainder(angle, Tau);
        // The active ingredient of the IEEERemainder method is extracted here for performance reasons.
        var value = angle - (T.Tau * T.Round(angle * (T.One / T.Tau)));
        return (value <= -T.Pi) ? value + T.Tau : value - T.Tau;
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Modulo<T>(this T valueA, T valueB) where T : INumber<T> => ((valueA %= valueB) < T.Zero) ? valueA + valueB : valueA;

    /// <summary>
    /// The angle.
    /// </summary>
    /// <param name="cos">The Cosine.</param>
    /// <param name="sin">The Sine.</param>
    /// <returns>
    /// The <see cref="double" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Angle<T>(T cos, T sin) where T : IFloatingPointIeee754<T> => T.Atan2(-sin, cos);

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Angle<T>(
        T x1, T y1,
        T x2, T y2) where T : IFloatingPointIeee754<T> => T.Atan2(y1 - y2, x1 - x2);

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Angle<T>(
        T x1, T y1, T z1,
        T x2, T y2, T z2)
        where T : IFloatingPointIeee754<T>
        => (T.Abs(x1 - x2) < T.Epsilon
        && T.Abs(y1 - y2) < T.Epsilon
        && T.Abs(z1 - z2) < T.Epsilon) ? T.Zero : T.Acos(T.Min(T.One, DotProduct(Normalize(x1, y1, z1), Normalize(x2, y2, z2))));

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T AngleVector<T>(
        T x1, T y1,
        T x2, T y2,
        T x3, T y3)
        where T : IFloatingPointIeee754<T> => T.Atan2(CrossProductTriple(x1, y1, x2, y2, x3, y3), DotProductTriple(x1, y1, x2, y2, x3, y3));

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T AbsoluteAngle<T>(
        T x1, T y1,
        T x2, T y2)
        where T : IFloatingPointIeee754<T>
    {
        // Find the angle of point a and point b.
        var test = -Angle(x1, y1, x2, y2) % T.Pi;
        return test < T.Zero ? test += T.Pi : test;
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T AngleBetween<T>(
        T uX, T uY,
        T vX, T vY)
        where T : IFloatingPointIeee754<T> => T.Acos(((uX * vX) + (uY * vY)) / T.Sqrt(((uX * uX) + (uY * uY)) * ((vX * vX) + (vY * vY))));

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T AngleBetween<T>(
        T uX, T uY, T uZ,
        T vX, T vY, T vZ)
        where T : IFloatingPointIeee754<T> => T.Acos(((uX * vX) + (uY * vY) + (uZ * vZ)) / T.Sqrt(((uX * uX) + (uY * uY) + (uZ * uZ)) * ((vX * vX) + (vY * vY) + (vZ * vZ))));

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static R EllipticalPolarAngle<T, R>(R angle, T rx, T ry)
        where T : INumber<T>
        where R : IFloatingPointIeee754<R>
    {
        // Wrap the angle between -2PI and 2PI.
        var theta = angle % R.Tau;

        // Find the elliptical t that matches the circular angle.
        if (R.Abs(theta) == Floats<R>.Hau || R.Abs(theta) == Floats<R>.Pau)
        {
            return angle;
        }

        if (theta > Floats<R>.Hau && theta < Floats<R>.Pau)
        {
            return R.Atan(R.CreateSaturating(rx) * R.Tan(theta) / R.CreateSaturating(ry)) + R.Pi;
        }

        if (theta < -Floats<R>.Hau && theta > -Floats<R>.Pau)
        {
            return R.Atan(R.CreateSaturating(rx) * R.Tan(theta) / R.CreateSaturating(ry)) - R.Pi;
        }

        return R.Atan(R.CreateSaturating(rx) * R.Tan(theta) / R.CreateSaturating(ry));
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (R cosT, R sinT) EllipticalPolarVector<T, R>(R cosA, R sinA, T rx, T ry)
        where T : INumber<T>
        where R : IFloatingPointIeee754<R>
    {
        // Find the elliptical t that matches the circular angle.
        if (cosA > -R.One && cosA < R.Zero || cosA > R.Zero && cosA < R.One)
        {
            var d = R.CreateSaturating(R.Sign(cosA));
            return (d / R.Sqrt(R.One + (R.CreateSaturating(rx * rx) * sinA * sinA / (R.CreateSaturating(ry * ry) * cosA * cosA))),
                    d * (R.CreateSaturating(rx) * sinA / (R.CreateSaturating(ry) * cosA * R.Sqrt(R.One + (R.CreateSaturating(rx * rx) * sinA * sinA / (R.CreateSaturating(ry * ry) * cosA * cosA))))));
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

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T SubtendedToParametric<T>(T subtended, T a, T b) where T : IFloatingPointIeee754<T> => SubtendedToParametric(T.Cos(subtended), T.Sin(subtended), a, b);

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T SubtendedToParametric<T>(T subtendedCos, T subtendedSin, T a, T b)
        where T : IFloatingPointIeee754<T>
    {
        if (a == b)
        {
            // Circle needs no correction.
            return T.Zero;
        }

        // A ray from the origin.
        var rx = subtendedCos;
        var ry = subtendedSin;
        var e = a * b / T.Sqrt((a * a * ry * ry) + (b * b * rx * rx));

        // Where ray intersects ellipse.
        var ex = e * rx;
        var ey = e * ry;

        // Normalized.
        var parametric = T.Atan2(a * ey, b * ex);
        var subtended = T.Atan2(ry, rx);
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T I, T J) Reflect<T>(T x1, T y1, T x2, T y2, T axisX, T axisY)
        where T : INumberBase<T>
    {
        var (i, j) = DeltaVector(x1, y1, x2, y2);
        var magnitude = DotProduct(i, j, i, j) / T.CreateSaturating(2);
        var reflection = CrossProduct(i, j, CrossProduct(x2, y2, x1, y1), DotProduct(axisX, axisY, i, j));
        return ((magnitude * reflection) - axisX,
                (magnitude * reflection) - axisY);
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (R X, R Y) RotatePoint2D<T, R>(T x, T y, R angle) where T : INumberBase<T> where R : IFloatingPointIeee754<R> => RotatePoint2D(x, y, R.Cos(angle), R.Sin(angle), T.Zero, T.Zero);

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (R X, R Y) RotatePoint2D<T, R>(T x, T y, R cos, R sin) where T : INumberBase<T> where R : IFloatingPointIeee754<R> => RotatePoint2D(x, y, cos, sin, T.Zero, T.Zero);

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (R X, R Y) RotatePoint2D<T, R>(T x, T y, R angle, T cx, T cy) where T : INumberBase<T> where R : IFloatingPointIeee754<R> => RotatePoint2D(x, y, R.Cos(angle), R.Sin(angle), cx, cy);

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (R X, R Y) RotatePoint2D<T, R>(T x, T y, R cos, R sin, T cx, T cy)
        where T : INumberBase<T>
        where R : IFloatingPointIeee754<R>
    {
        var deltaX = R.CreateSaturating(x - cx);
        var deltaY = R.CreateSaturating(y - cy);
        return (R.CreateSaturating(cx) + ((deltaX * R.CreateSaturating(cos)) - (deltaY * R.CreateSaturating(sin))),
                R.CreateSaturating(cy) + ((deltaX * R.CreateSaturating(sin)) + (deltaY * R.CreateSaturating(cos))));
    }
    #endregion Rotate Point

    /// <summary>
    /// Transforms to elipse origin.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="H">The h.</param>
    /// <param name="K">The k.</param>
    /// <param name="Theta">The theta.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (R x, R y) TransformToEllipseOrigin<T, R>(T x, T y, T H, T K, R Theta)
        where T : struct, INumber<T>
        where R : struct, IFloatingPointIeee754<R>
    {
        var cosTheta = R.Cos(-Theta);
        var sinTheta = R.Sin(-Theta);

        var xPrime = cosTheta * R.CreateSaturating(x - H) - sinTheta * R.CreateSaturating(y - K);
        var yPrime = sinTheta * R.CreateSaturating(x - H) + cosTheta * R.CreateSaturating(y - K);

        return (xPrime, yPrime);
    }

    /// <summary>
    /// Transforms from elipse origin.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="H">The h.</param>
    /// <param name="K">The k.</param>
    /// <param name="Theta">The theta.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (R x, R y) TransformFromEllipseOrigin<T, R>(T x, T y, T H, T K, R Theta)
        where T : struct, INumber<T>
        where R : struct, IFloatingPointIeee754<R>
    {
        var cosTheta = R.Cos(Theta);
        var sinTheta = R.Sin(Theta);
        
        var xPrime = cosTheta * R.CreateSaturating(x) - sinTheta * R.CreateSaturating(y) + R.CreateSaturating(H);
        var yPrime = sinTheta * R.CreateSaturating(x) + cosTheta * R.CreateSaturating(y) + R.CreateSaturating(K);

        return (xPrime, yPrime);
    }

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Slope<T>(T i, T j) where T : IFloatingPointIeee754<T> => T.Abs(i) < T.Epsilon ? Floats<T>.SlopeMax : (j / i);

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Slope<T>(
        T x1, T y1,
        T x2, T y2) where T : IFloatingPointIeee754<T> => (T.Abs(x1 - x2) < T.Epsilon) ? Floats<T>.SlopeMax : ((y2 - y1) / (x2 - x1));

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static R Slope<T, R>(
        T x1, T y1,
        T x2, T y2) where T : INumberBase<T> where R : IFloatingPointIeee754<R> => (R.Abs(R.CreateSaturating(x1 - x2)) < R.Epsilon) ? Floats<R>.SlopeMax : (R.CreateSaturating(y2 - y1) / R.CreateSaturating(x2 - x1));
    #endregion Slope

    #region Unit
    /// <summary>
    /// Unit of a 2D Vector.
    /// </summary>
    /// <param name="i">The i component of the Vector to Unitize.</param>
    /// <param name="j">The j component of the Vector to Unitize.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T I, T J) Unit<T>(T i, T j) where T : IFloatingPointIeee754<T> => ScaleVector(i, j, T.One / T.Sqrt((i * i) + (j * j)));

    /// <summary>
    /// Unit of a 3D Vector.
    /// </summary>
    /// <param name="i">The i component of the Vector to Unitize.</param>
    /// <param name="j">The j component of the Vector to Unitize.</param>
    /// <param name="k">The k component of the Vector to Unitize.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T I, T J, T K) Unit<T>(T i, T j, T k) where T : IFloatingPointIeee754<T> => ScaleVector(i, j, k, T.One / T.Sqrt((i * i) + (j * j) + (k * k)));

    /// <summary>
    /// Unit of a 4D Vector.
    /// </summary>
    /// <param name="i">The i component of the Vector to Unitize.</param>
    /// <param name="j">The j component of the Vector to Unitize.</param>
    /// <param name="k">The k component of the Vector to Unitize.</param>
    /// <param name="l">The l component of the Vector to Unitize.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static (T I, T J, T K, T L) Unit<T>(T i, T j, T k, T l) where T : IFloatingPointIeee754<T> => ScaleVector(i, j, k, l, T.One / T.Sqrt((i * i) + (j * j) + (k * k) + (l * l)));
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Abs<T>(T i, T j) where T : IRootFunctions<T> => Magnitude(i, j);

    /// <summary>
    /// The abs.
    /// </summary>
    /// <param name="i">The i.</param>
    /// <param name="j">The j.</param>
    /// <param name="k">The k.</param>
    /// <returns>
    /// The <see cref="double" />.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Abs<T>(T i, T j, T k) where T : IRootFunctions<T> => Magnitude(i, j, k);

    /// <summary>
    /// Derived math functions equivalent Secant
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// Translated from old Microsoft VB code examples that I have since lost.
    /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Secant<T>(T value)
        where T : IFloatingPointIeee754<T>
        => (value % T.Pi == Floats<T>.Hau)
        && (value % T.Pi == -Floats<T>.Hau)
        ? (T.One / T.Cos(value)) : T.Zero;

    /// <summary>
    /// Derived math functions equivalent  Co-secant
    /// </summary>
    /// <param name="Value">The value.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// Translated from old Microsoft VB code examples that I have since lost.
    /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Cosecant<T>(T Value)
        where T : IFloatingPointIeee754<T>
        => (Value % T.Pi == T.Zero)
        && (Value % T.Pi == T.Pi)
        ? (T.One / T.Sin(Value)) : T.Zero;

    /// <summary>
    /// Derived math functions equivalent Cotangent
    /// </summary>
    /// <param name="Value">The value.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// Translated from old Microsoft VB code examples that I have since lost.
    /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T Cotangent<T>(T Value)
        where T : IFloatingPointIeee754<T>
        => (Value % T.Pi == T.Zero)
        && (Value % T.Pi == T.Pi)
        ? (T.One / T.Tan(Value)) : T.Zero;

    /// <summary>
    /// Derived math functions equivalent Inverse Sine
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    /// <acknowledgment>
    /// Translated from old Microsoft VB code examples that I have since lost.
    /// The latest incarnation seems to be: https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/keywords/derived-math-functions
    /// </acknowledgment>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T InverseSine<T>(T value)
        where T : IFloatingPointIeee754<T>
    {
        if (value == T.One)
        {
            return Floats<T>.Hau;
        }
        else if (value == T.NegativeOne)
        {
            return -Floats<T>.Hau;
        }
        else if (T.Abs(value) < T.One)
        {
            // Arc-sin(X)
            return T.Atan(value / T.Sqrt((-value * value) + T.One));
        }

        return T.Zero;
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T InverseCosine<T>(T value)
        where T : IFloatingPointIeee754<T>
    {
        if (value == T.One)
        {
            return T.Zero;
        }
        else if (value == T.NegativeOne)
        {
            return T.Pi;
        }
        else if (T.Abs(value) < T.One)
        {
            // Arc-cos(X)
            return T.Atan(-value / T.Sqrt((-value * value) + T.One)) + (T.CreateSaturating(2) * T.Atan(T.One));
        }

        return T.Zero;
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T InverseSecant<T>(T value)
        where T : IFloatingPointIeee754<T>
    {
        if (value == T.One)
        {
            return T.Zero;
        }
        else if (value == T.NegativeOne)
        {
            return T.Pi;
        }
        else if (T.Abs(value) < T.One)
        {
            // Arc-sec(X)
            return T.Atan(value / T.Sqrt((value * value) - T.One)) + (T.Sin(value - T.One) * (T.CreateSaturating(2) * T.Atan(T.One)));
        }

        return T.Zero;
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T InverseCosecant<T>(T value)
        where T : IFloatingPointIeee754<T>
    {
        if (value == T.One)
        {
            return Floats<T>.Hau;
        }
        else if (value == T.NegativeOne)
        {
            return -Floats<T>.Hau;
        }
        else if (T.Abs(value) < T.One)
        {
            // Arc-co-sec(X)
            return T.Atan(value / T.Sqrt((value * value) - T.One)) + ((T.Sin(value) - T.One) * (T.CreateSaturating(2) * T.Atan(T.One)));
        }

        return T.Zero;
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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T InverseCotangent<T>(T value) where T : IFloatingPointIeee754<T> => T.Atan(value) + (T.CreateSaturating(2) * T.Atan(T.One));

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T HyperbolicSine<T>(T value) where T : IFloatingPointIeee754<T> => (T.Exp(value) - T.Exp(value * T.NegativeOne)) / T.CreateSaturating(2);

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T HyperbolicCosine<T>(T value) where T : IFloatingPointIeee754<T> => (T.Exp(value) + T.Exp(value * T.NegativeOne)) / T.CreateSaturating(2);

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T HyperbolicTangent<T>(T value) where T : IFloatingPointIeee754<T> => (T.Exp(value) - T.Exp(value * T.NegativeOne)) / (T.Exp(value) + T.Exp(value * T.NegativeOne));

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T HyperbolicSecant<T>(T value) where T : IFloatingPointIeee754<T> => (T.Exp(value) + T.Exp(value * T.NegativeOne)) / T.CreateSaturating(2);

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T HyperbolicCosecant<T>(T value) where T : IFloatingPointIeee754<T> => (T.Exp(value) - T.Exp(value * T.NegativeOne)) / T.CreateSaturating(2);

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T HyperbolicCotangent<T>(T value) where T : IFloatingPointIeee754<T> => (T.Exp(value) + T.Exp(value * T.NegativeOne)) / (T.Exp(value) - T.Exp(value * T.NegativeOne));

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T InverseHyperbolicSine<T>(T value) where T : IFloatingPointIeee754<T> => T.Log(value + T.Sqrt((value * value) + T.One));

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T InverseHyperbolicCosine<T>(T value) where T : IFloatingPointIeee754<T> => T.Log(value + T.Sqrt((value * value) - T.One));

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T InverseHyperbolicTangent<T>(T value) where T : IFloatingPointIeee754<T> => T.Log((T.One + value) / (T.One - value)) / T.CreateSaturating(2);

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T InverseHyperbolicSecant<T>(T value) where T : IFloatingPointIeee754<T> => T.Log((T.Sqrt((value * value * T.NegativeOne) + T.One) + T.One) / value);

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T InverseHyperbolicCosecant<T>(T value) where T : IFloatingPointIeee754<T> => T.Log(((T.Sin(value) * T.Sqrt((value * value) + T.One)) + T.One) / value);

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T InverseHyperbolicCotangent<T>(T value) where T : IFloatingPointIeee754<T> => T.Log((value + T.One) / (value - T.One)) / T.CreateSaturating(2);

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
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    public static T LogarithmToBaseN<T>(T value, T numberBase) where T : IFloatingPointIeee754<T> => (numberBase == T.One) ? (T.Log(value) / T.Log(numberBase)) : T.Zero;
    #endregion Derived Equivalent Math Functions
}
