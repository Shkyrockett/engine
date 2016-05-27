// <copyright file="Experiments.cs" >
//     Copyright (c) 2016 Shkyrockett. All rights reserved.
// </copyright>
// <license> 
//     Licensed under the MIT License. See LICENSE file in the project root for full license information. 
// </license>
// <author id="shkyrockett">Shkyrockett</author>
// <summary></summary>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static System.Math;

namespace MethodSpeedTester
{
    /// <summary>
    /// Class to contain experimental methods to test. 
    /// </summary>
    public class Experiments
    {
        #region Cosine Interpolation of 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the distance between two 3D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CosineInterpolateTests))]
        public static List<SpeedTester> CosineInterpolateTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => CosineInterpolate(0, 1, 0.5d),
                $"{nameof(Experiments.CosineInterpolate)}(0, 1, 0.5d)"),
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        public static double CosineInterpolate(double y1, double y2, double t)
        {
            double mu2 = (1 - Cos(t * PI)) / 2;
            return (y1 * (1 - mu2) + y2 * mu2);
        }

        #endregion

        #region Cubic Interpolation of 1D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the distance between two 3D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CosineInterpolateTests))]
        public static List<SpeedTester> CubicInterpolateTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => CubicInterpolate(0, 1, 2, 3, 0.5d),
                $"{nameof(Experiments.CubicInterpolate)}(0, 1, 2, 3, 0.5d)"),
            };
        }

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
        public static double CubicInterpolate(double v0, double v1, double v2, double v3, double t)
        {
            double a0, a1, a2, a3, mu2;

            mu2 = t * t;
            a0 = v3 - v2 - v0 + v1;
            a1 = v0 - v1 - a0;
            a2 = v2 - v0;
            a3 = v1;

            return (a0 * t * mu2 + a1 * mu2 + a2 * t + a3);
        }

        #endregion

        #region Cubic CatmulRom Spline Interpolation of 1D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the distance between two 3D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CubicInterpolateCatmullRomSplinesTests))]
        public static List<SpeedTester> CubicInterpolateCatmullRomSplinesTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => CubicInterpolateCatmullRomSplines(0, 1, 2, 3, 0.5d),
                $"{nameof(Experiments.CubicInterpolateCatmullRomSplines)}(0, 1, 2, 3, 0.5d)"),
            };
        }

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
        public static double CubicInterpolateCatmullRomSplines(double v0, double v1, double v2, double v3, double t)
        {
            double a0, a1, a2, a3, mu2;

            mu2 = t * t;
            a0 = -0.5 * v0 + 1.5 * v1 - 1.5 * v2 + 0.5 * v3;
            a1 = v0 - 2.5 * v1 + 2 * v2 - 0.5 * v3;
            a2 = -0.5 * v0 + 0.5 * v2;
            a3 = v1;

            return (a0 * t * mu2 + a1 * mu2 + a2 * t + a3);
        }

        #endregion

        #region Cubic Bezier Interpolation of 2D Points

        #endregion

        #region Distance Between Two 3D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the distance between two 3D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(Distance3DTests))]
        public static List<SpeedTester> Distance3DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => Distance3D_0(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.Distance3D_0)}(0, 0, 1, 0, 1, 1)"),
                new SpeedTester(() => Distance3D_1(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.Distance3D_1)}(0, 0, 1, 0, 1, 1)"),
                new SpeedTester(() => Distance3D_2(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.Distance3D_2)}(0, 0, 1, 0, 1, 1)"),
            };
        }

        /// <summary>
        /// Distance between two 3D points.
        /// </summary>
        /// <param name="x1">First X component.</param>
        /// <param name="y1">First Y component.</param>
        /// <param name="z1">First Z component.</param>
        /// <param name="x2">Second X component.</param>
        /// <param name="y2">Second Y component.</param>
        /// <param name="z2">Second Z component.</param>
        /// <returns>The distance between two points.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance3D_0(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => Sqrt((x2 - x1) * (x2 - x1)
                + (y2 - y1) * (y2 - y1)
                + (z2 - z1) * (z2 - z1));

        /// <summary>
        /// Distance between two 3D points.
        /// </summary>
        /// <param name="x1">First X component.</param>
        /// <param name="y1">First Y component.</param>
        /// <param name="z1">First Z component.</param>
        /// <param name="x2">Second X component.</param>
        /// <param name="y2">Second Y component.</param>
        /// <param name="z2">Second Z component.</param>
        /// <returns>The distance between two points.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance3D_1(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
        {
            return Sqrt((x2 - x1) * (x2 - x1)
                + (y2 - y1) * (y2 - y1)
                + (z2 - z1) * (z2 - z1));
        }

        /// <summary>
        /// Distance between two 3D points.
        /// </summary>
        /// <param name="x1">First X component.</param>
        /// <param name="y1">First Y component.</param>
        /// <param name="z1">First Z component.</param>
        /// <param name="x2">Second X component.</param>
        /// <param name="y2">Second Y component.</param>
        /// <param name="z2">Second Z component.</param>
        /// <returns>The distance between two points.</returns>
        [DebuggerStepThrough]
        public static double Distance3D_2(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
        {
            double x = (x2 - x1);
            double y = (y2 - y1);
            double z = (z2 - z1);
            return Sqrt(x * x + y * y + z * z);
        }

        #endregion

        #region Distance Between Two 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the distance between two 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(Distance2DTests))]
        public static List<SpeedTester> Distance2DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => Distance2D_0(0, 0, 1, 0),
                $"{nameof(Experiments.Distance2D_0)}(0, 0, 1, 0)"),
                new SpeedTester(() => Distance2D_1(0, 0, 1, 0),
                $"{nameof(Experiments.Distance2D_1)}(0, 0, 1, 0)"),
                new SpeedTester(() => Distance2D_2(0, 0, 1, 0),
                $"{nameof(Experiments.Distance2D_2)}(0, 0, 1, 0)"),
            };
        }

        /// <summary>
        /// Distance between two 2D points.
        /// </summary>
        /// <param name="x1">First X component.</param>
        /// <param name="y1">First Y component.</param>
        /// <param name="x2">Second X component.</param>
        /// <param name="y2">Second Y component.</param>
        /// <returns>The distance between two points.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance2D_0(
            double x1, double y1,
            double x2, double y2)
            => Sqrt((x2 - x1) * (x2 - x1)
                + (y2 - y1) * (y2 - y1));

        /// <summary>
        /// Distance between two 2D points.
        /// </summary>
        /// <param name="x1">First X component.</param>
        /// <param name="y1">First Y component.</param>
        /// <param name="x2">Second X component.</param>
        /// <param name="y2">Second Y component.</param>
        /// <returns>The distance between two points.</returns>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance2D_1(
            double x1, double y1,
            double x2, double y2)
        {
            return Sqrt((x2 - x1) * (x2 - x1)
                + (y2 - y1) * (y2 - y1));
        }

        /// <summary>
        /// Distance between two 2D points.
        /// </summary>
        /// <param name="x1">First X component.</param>
        /// <param name="y1">First Y component.</param>
        /// <param name="x2">Second X component.</param>
        /// <param name="y2">Second Y component.</param>
        /// <returns>The distance between two points.</returns>
        [DebuggerStepThrough]
        public static double Distance2D_2(
            double x1, double y1,
            double x2, double y2)
        {
            double x = (x2 - x1);
            double y = (y2 - y1);
            return Sqrt(x * x + y * y);
        }

        #endregion

        #region Dot Product of Two 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the distance between two 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(DotProduct2Points2DTests))]
        public static List<SpeedTester> DotProduct2Points2DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => DotProduct2Points2D_0(0, 0, 1, 0),
                $"{nameof(Experiments.DotProduct2Points2D_0)}(0, 0, 1, 0)"),
                new SpeedTester(() => DotProduct2Points2D_1(0, 0, 1, 0),
                $"{nameof(Experiments.DotProduct2Points2D_1)}(0, 0, 1, 0)"),
            };
        }

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
        public static double DotProduct2Points2D_0(
            double x1, double y1,
            double x2, double y2)
            => ((x1 * x2) + (y1 * y2));

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
        public static double DotProduct2Points2D_1(
            double x1, double y1,
            double x2, double y2)
        {
            return ((x1 * x2) + (y1 * y2));
        }

        #endregion

        #region Dot Product of Three 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the dot product of three 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(DotProduct3Points2DTests))]
        public static List<SpeedTester> DotProduct3Points2DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => DotProduct3Points2D_0(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.DotProduct3Points2D_0)}(0, 0, 1, 0, 1, 1)"),
                new SpeedTester(() => DotProduct3Points2D_1(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.DotProduct3Points2D_1)}(0, 0, 1, 0, 1, 1)"),
                new SpeedTester(() => DotProduct3Points2D_2(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.DotProduct3Points2D_2)}(0, 0, 1, 0, 1, 1)"),
            };
        }

        /// <summary>
        /// Return the dot product AB · BC.
        /// Note that AB · BC = |AB| * |BC| * Cos(theta).
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct3Points2D_0(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
            => ((x1 - x2) * (x3 - x2)
            + (y1 - y2) * (y3 - y2));

        /// <summary>
        /// Return the dot product AB · BC.
        /// Note that AB · BC = |AB| * |BC| * Cos(theta).
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct3Points2D_1(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
        {
            return ((x1 - x2) * (x3 - x2)
                + (y1 - y2) * (y3 - y2));
        }

        /// <summary>
        /// Return the dot product AB · BC.
        /// Note that AB · BC = |AB| * |BC| * Cos(theta).
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        [DebuggerStepThrough]
        public static double DotProduct3Points2D_2(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
        {
            // Get the vectors' coordinates.
            double BAx = x1 - x2;
            double BAy = y1 - y2;
            double BCx = x3 - x2;
            double BCy = y3 - y2;

            // Calculate the dot product.
            return (BAx * BCx + BAy * BCy);
        }

        #endregion

        #region Hermite Interpolation of 1D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the distance between two 3D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(HermiteInterpolateTests))]
        public static List<SpeedTester> HermiteInterpolateTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => HermiteInterpolate(0, 1, 2, 3, 0.5d, 1, 0),
                $"{nameof(Experiments.HermiteInterpolate)}(0, 1, 2, 3, 0.5d, 1, 0)"),
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="y0"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <param name="y3"></param>
        /// <param name="mu"></param>
        /// <param name="tension">1 is high, 0 normal, -1 is low</param>
        /// <param name="bias">0 is even,positive is towards first segment, negative towards the other</param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        static double HermiteInterpolate(double y0, double y1, double y2, double y3, double mu, double tension, double bias)
        {
            double m0, m1, mu2, mu3;
            double a0, a1, a2, a3;

            mu2 = mu * mu;
            mu3 = mu2 * mu;
            m0 = (y1 - y0) * (1 + bias) * (1 - tension) / 2;
            m0 += (y2 - y1) * (1 - bias) * (1 - tension) / 2;
            m1 = (y2 - y1) * (1 + bias) * (1 - tension) / 2;
            m1 += (y3 - y2) * (1 - bias) * (1 - tension) / 2;
            a0 = 2 * mu3 - 3 * mu2 + 1;
            a1 = mu3 - 2 * mu2 + mu;
            a2 = mu3 - mu2;
            a3 = -2 * mu3 + 3 * mu2;

            return (a0 * y1 + a1 * m0 + a2 * m1 + a3 * y2);
        }

        #endregion

        #region Linear Interpolation of Two 1D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the linear interpolation point for a value between two 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(LinearInterpolate1DTests))]
        public static List<SpeedTester> LinearInterpolate1DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => LinearInterpolate1D_0(0, 1, 0.5d),
                $"{nameof(Experiments.LinearInterpolate1D_0)}(0, 1, 0.5d)"),
                new SpeedTester(() => LinearInterpolate1D_1(0, 1, 0.5d),
                $"{nameof(Experiments.LinearInterpolate1D_1)}(0, 1, 0.5d)"),
                new SpeedTester(() => LinearInterpolate1D_2(0, 1, 0.5d),
                $"{nameof(Experiments.LinearInterpolate1D_2)}(0, 1, 0.5d)"),
            };
        }

        /// <summary>
        /// Precise method which guarantees v = v1 when t = 1.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>https://en.wikipedia.org/wiki/Linear_interpolation</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LinearInterpolate1D_0(
            double v1, double v2, double t)
            => (1 - t) * v1 + t * v2;

        /// <summary>
        /// Imprecise method which does not guarantee v = v1 when t = 1, due to floating-point arithmetic error.
        /// This form may be used when the hardware has a native Fused Multiply-Add instruction.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>https://en.wikipedia.org/wiki/Linear_interpolation</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LinearInterpolate1D_1(
            double v1, double v2, double t)
            => v1 + t * (v2 - v1);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>https://en.wikipedia.org/wiki/Linear_interpolation</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double LinearInterpolate1D_2(
            double v1, double v2, double t)
            => (v1 == v2) ? 0 : v1 - ((1 / (v1 - v2)) * t);

        #endregion

        #region Linear Interpolation of Two 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the linear interpolation point for a value between two 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(LinearInterpolate2DTests))]
        public static List<SpeedTester> LinearInterpolate2DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => LinearInterpolate2D_0(0, 0, 1, 1, 0.5d),
                $"{nameof(Experiments.LinearInterpolate2D_0)}(0, 0, 1, 1, 0.5d)"),
                new SpeedTester(() => LinearInterpolate2D_1(0, 0, 1, 1, 0.5d),
                $"{nameof(Experiments.LinearInterpolate2D_1)}(0, 0, 1, 1, 0.5d)"),
                new SpeedTester(() => LinearInterpolate2D_2(0, 0, 1, 1, 0.5d),
                $"{nameof(Experiments.LinearInterpolate2D_2)}(0, 0, 1, 1, 0.5d)"),
                new SpeedTester(() => LinearInterpolate2D_3(0, 0, 1, 1, 0.5d),
                $"{nameof(Experiments.LinearInterpolate2D_3)}(0, 0, 1, 1, 0.5d)"),
            };
        }

        /// <summary>
        /// Precise method which guarantees v = v1 when t = 1.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Tuple<double, double> LinearInterpolate2D_0(
            double x1, double y1,
            double x2, double y2,
            double t)
            => new Tuple<double, double>(
                (1 - t) * x1 + t * x2,
                (1 - t) * y1 + t * y2);

        /// <summary>
        /// Imprecise method which does not guarantee v = v1 when t = 1, due to floating-point arithmetic error.
        /// This form may be used when the hardware has a native Fused Multiply-Add instruction.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://www.cubic.org/docs/bezier.htm</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Tuple<double, double> LinearInterpolate2D_1(
            double x1, double y1,
            double x2, double y2,
            double t)
            => new Tuple<double, double>(
                x1 + t * (x2 - x1),
                y1 + t * (y2 - y1));

        /// <summary>
        /// simple linear interpolation between two points
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://www.cubic.org/docs/bezier.htm</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Tuple<double, double> LinearInterpolate2D_2(
            double x1, double y1,
            double x2, double y2,
            double t)
            => new Tuple<double, double>(
                LinearInterpolate1D_0(x1, x2, t),
                LinearInterpolate1D_0(y1, y2, t));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double> LinearInterpolate2D_3(
            double x1, double y1,
            double x2, double y2,
            double t)
            => new Tuple<double, double>(
                (x1 == x2) ? 0 : x1 - ((1 / (x1 - x2)) * t),
                (y1 == y2) ? 0 : y1 - ((1 / (y1 - y2)) * t));

        #endregion

        #region Linear Interpolation of Two 3D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the linear interpolation point for a value between two 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(LinearInterpolate3DTests))]
        public static List<SpeedTester> LinearInterpolate3DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => LinearInterpolate3D_0(0, 0, 0, 1, 1, 1, 0.5d),
                $"{nameof(Experiments.LinearInterpolate3D_0)}(0, 0, 0, 1, 1, 1, 0.5d)"),
                new SpeedTester(() => LinearInterpolate3D_1(0, 0, 0, 1, 1, 1, 0.5d),
                $"{nameof(Experiments.LinearInterpolate3D_1)}(0, 0, 0, 1, 1, 1, 0.5d)"),
                new SpeedTester(() => LinearInterpolate3D_2(0, 0, 0, 1, 1, 1, 0.5d),
                $"{nameof(Experiments.LinearInterpolate3D_2)}(0, 0, 0, 1, 1, 1, 0.5d)"),
                new SpeedTester(() => LinearInterpolate3D_3(0, 0, 0, 1, 1, 1, 0.5d),
                $"{nameof(Experiments.LinearInterpolate3D_3)}(0, 0, 0, 1, 1, 1, 0.5d)"),
            };
        }

        /// <summary>
        /// Precise method which guarantees v = v1 when t = 1.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Tuple<double, double, double> LinearInterpolate3D_0(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double t)
            => new Tuple<double, double, double>(
                (1 - t) * x1 + t * x2,
                (1 - t) * y1 + t * y2,
                (1 - t) * z1 + t * z2);

        /// <summary>
        /// Imprecise method which does not guarantee v = v1 when t = 1, due to floating-point arithmetic error.
        /// This form may be used when the hardware has a native Fused Multiply-Add instruction.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://www.cubic.org/docs/bezier.htm</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Tuple<double, double, double> LinearInterpolate3D_1(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double t)
            => new Tuple<double, double, double>(
                x1 + t * (x2 - x1),
                y1 + t * (y2 - y1),
                z1 + t * (z2 - z1));

        /// <summary>
        /// simple linear interpolation between two points
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://www.cubic.org/docs/bezier.htm</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Tuple<double, double, double> LinearInterpolate3D_2(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double t)
            => new Tuple<double, double, double>(
                LinearInterpolate1D_0(x1, x2, t),
                LinearInterpolate1D_0(y1, y2, t),
                LinearInterpolate1D_0(z1, z2, t));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<double, double, double> LinearInterpolate3D_3(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double t)
            => new Tuple<double, double, double>(
                (x1 == x2) ? 0 : x1 - ((1 / (x1 - x2)) * t),
                (y1 == y2) ? 0 : y1 - ((1 / (y1 - y2)) * t),
                (z1 == z2) ? 0 : z1 - ((1 / (z1 - z2)) * t));

        #endregion

        #region Quadratic Bezier Interpolation of 2D Points

        #endregion
    }
}
