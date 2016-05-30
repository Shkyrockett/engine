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
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using static System.Math;

namespace MethodSpeedTester
{
    /// <summary>
    /// Class to contain experimental methods to test. 
    /// </summary>
    public class Experiments
    {
        #region Absolute Angle

        /// <summary>
        /// Find the absolute positive value of a radian angle from two points.
        /// </summary>
        /// <param name="aX">Horizontal Component of Point Starting Point</param>
        /// <param name="aY">Vertical Component of Point Starting Point</param>
        /// <param name="bX">Horizontal Component of Ending Point</param>
        /// <param name="bY">Vertical Component of Ending Point</param>
        /// <returns>The absolute angle of a line in radians.</returns>
        /// <remarks></remarks>
        public static double AbsoluteAngle0(double aX, double aY, double bX, double bY)
        {
            // Find the angle of point a and point b. 
            double test = -Angle(aX, aY, bX, bY) % PI;

            // This should only loop once using the modulus of pi.
            while (test < 0)
            {
                test += PI;
            }

            return test;
        }

        /// <summary>
        /// Find the absolute positive value of a radian angle from two points.
        /// </summary>
        /// <param name="aX">Horizontal Component of Point Starting Point</param>
        /// <param name="aY">Vertical Component of Point Starting Point</param>
        /// <param name="bX">Horizontal Component of Ending Point</param>
        /// <param name="bY">Vertical Component of Ending Point</param>
        /// <returns>The absolute angle of a line in radians.</returns>
        /// <remarks></remarks>
        public static double AbsoluteAngle1(double aX, double aY, double bX, double bY)
        {
            // Find the angle of point a and point b. 
            double test = -Angle(aX, aY, bX, bY) % PI;
            return test < 0 ? test += PI : test;
        }

        #endregion

        #region Angle Between Two 2D Points

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

        #endregion

        #region Angle Between Two 3D Points

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

        #endregion

        #region Angle of Two 2D Points

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

        #endregion

        #region Angle of Two 3D Points

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
            => (x1 == x2 && y1 == y2 && z1 == z2) ? 0 : Acos(Min(1.0d, DotProduct(Normalize(x1, y1, z1), Normalize(x2, y2, z2))));

        #endregion

        #region Angle of the vector of Three 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the angle of three 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(Angle3Points2DTests))]
        public static List<SpeedTester> Angle3Points2DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => AngleVector_0(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.AngleVector_0)}(0, 0, 1, 0, 1, 1)"),
                 new SpeedTester(() => AngleVector_1(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.AngleVector_1)}(0, 0, 1, 0, 1, 1)"),
           };
        }

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double AngleVector_0(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
            => Atan2(CrossProductVector2D_0(x1, y1, x2, y2, x3, y3), DotProductVector2D_0(x1, y1, x2, y2, x3, y3));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns>
        /// Return the angle ABC.
        /// Return a value between PI and -PI.
        /// Note that the value is the opposite of what you might
        /// expect because Y coordinates increase downward.
        /// </returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/</remarks>
        public static double AngleVector_1(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY)
        {
            // Get the dot product.
            double dotProduct = DotProductVector2D_0(aX, aY, bX, bY, cX, cY);

            // Get the cross product.
            double crossProduct = CrossProductVector2D_0(aX, aY, bX, bY, cX, cY);

            // Calculate the angle.
            return Atan2(crossProduct, dotProduct);
        }

        #endregion

        #region  Angle Tangent of Two deltas Atan2

        ///// <summary>
        ///// Angle with tangent opp/hyp
        ///// </summary>
        ///// <param name="opposite"></param>
        ///// <param name="adjacent"></param>
        ///// <returns>Return the angle with tangent opp/hyp. The returned value is between PI and -PI.</returns>
        ///// <remarks></remarks>
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static double Atan2(double opposite, double adjacent)
        //    => Math.Atan2(opposite, adjacent);

        ///// <summary>
        ///// Returns the Angle of two deltas.
        ///// </summary>
        ///// <param name="opposite">Delta Angle 1</param>
        ///// <param name="adjacent">Delta Angle 2</param>
        ///// <returns>Returns the Angle of a line.</returns>
        ///// <remarks></remarks>
        //public static double _Atan2(double opposite, double adjacent)
        //{
        //    if (((opposite == 0) && (adjacent == 0))) return 0;
        //    double Value = Asin(opposite / Sqrt(opposite * opposite + adjacent * adjacent));
        //    if ((adjacent < 0)) Value = (PI - Value);
        //    if ((Value < 0)) Value = (Value + (2 * PI));
        //    return Value;
        //}

        #endregion

        #region Complex Product of Two 2D Points

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

        #endregion

        #region Cosine Interpolation of 1D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 1D Cosine interpolation point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CosineInterpolateTests1D))]
        public static List<SpeedTester> CosineInterpolateTests1D()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => CosineInterpolate1D(0, 1, 0.5d),
                $"{nameof(Experiments.CosineInterpolate1D)}(0, 1, 0.5d)"),
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        public static double CosineInterpolate1D(double v1, double v2, double t)
        {
            double mu2 = (1 - Cos(t * PI)) / 2;
            return v1 * (1 - mu2) + v2 * mu2;
        }

        #endregion

        #region Cosine Interpolation of 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 2D Cosine interpolation point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CosineInterpolate2DTests))]
        public static List<SpeedTester> CosineInterpolate2DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => CosineInterpolate2D(0, 0, 1, 1, 0.5d),
                $"{nameof(Experiments.CosineInterpolate2D)}(0, 0, 1, 1, 0.5d)"),
            };
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
        public static Tuple<double, double> CosineInterpolate2D(
            double x1, double y1,
            double x2, double y2,
            double t)
        {
            double mu2 = (1 - Cos(t * PI)) / 2;
            return new Tuple<double, double>(
                x1 * (1 - mu2) + x2 * mu2,
                y1 * (1 - mu2) + y2 * mu2);
        }

        #endregion

        #region Cosine Interpolation of 3D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 3D Cosine interpolation point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CosineInterpolate3DTests))]
        public static List<SpeedTester> CosineInterpolate3DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => CosineInterpolate3D(0, 0, 0, 1, 1, 1, 0.5d),
                $"{nameof(Experiments.CosineInterpolate3D)}(0, 0, 0, 1, 1, 1, 0.5d)"),
            };
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
        public static Tuple<double, double, double> CosineInterpolate3D(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double t)
        {
            double mu2 = (1 - Cos(t * PI)) / 2;
            return new Tuple<double, double, double>(
                x1 * (1 - mu2) + x2 * mu2,
                y1 * (1 - mu2) + y2 * mu2,
                z1 * (1 - mu2) + z2 * mu2);
        }

        #endregion

        #region Cross Product of Two 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the cross product of two 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CrossProduct2Points2DTests))]
        public static List<SpeedTester> CrossProduct2Points2DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => CrossProduct2Points2D_0(0, 0, 1, 0),
                $"{nameof(Experiments.CrossProduct2Points2D_0)}(0, 0, 1, 0)"),
            };
        }

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
        public static double CrossProduct2Points2D_0(
            double x1, double y1,
            double x2, double y2)
            => (x1 * y2) - (y1 * x2);

        #endregion

        #region Cross Product of Two 3D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the cross product of two 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CrossProduct2Points3DTests))]
        public static List<SpeedTester> CrossProduct2Points3DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => CrossProduct2Points3D_0(0, 0, 0, 1, 1, 1),
                $"{nameof(Experiments.CrossProduct2Points3D_0)}(0, 0, 0, 1, 1, 1)"),
            };
        }

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
        public static Tuple<double, double, double> CrossProduct2Points3D_0(
            double x1, double y1, double z1,
            double x2, double y2, double z2)
            => new Tuple<double, double, double>(
                    (y1 * z2) - (z1 * y2), // X
                    (z1 * x2) - (x1 * z2), // Y
                    (x1 * y2) - (y1 * x2)  // Z
                );

        #endregion

        #region Cross Product of the Vector of Three 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the cross product of three 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CrossProductVector2DTests))]
        public static List<SpeedTester> CrossProductVector2DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => CrossProductVector2D_0(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.CrossProductVector2D_0)}(0, 0, 1, 0, 1, 1)"),
                new SpeedTester(() => CrossProductVector2D_1(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.CrossProductVector2D_1)}(0, 0, 1, 0, 1, 1)"),
                new SpeedTester(() => CrossProductVector2D_2(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.CrossProductVector2D_2)}(0, 0, 1, 0, 1, 1)"),
            };
        }

        /// <summary>
        /// The cross product is a vector perpendicular to AB
        /// and BC having length |AB| * |BC| * Sin(theta) and
        /// with direction given by the right-hand rule.
        /// For two vectors in the X-Y plane, the result is a
        /// vector with X and Y components 0 so the Z component
        /// gives the vector's length and direction.
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <returns>
        /// Return the cross product AB x BC.
        /// </returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/</remarks>
        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double CrossProductVector2D_0(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY)
            => ((aX - bX) * (cY - bY) - (aY - bY) * (cX - bX));

        /// <summary>
        /// The cross product is a vector perpendicular to AB
        /// and BC having length |AB| * |BC| * Sin(theta) and
        /// with direction given by the right-hand rule.
        /// For two vectors in the X-Y plane, the result is a
        /// vector with X and Y components 0 so the Z component
        /// gives the vector's length and direction.
        /// </summary>
        /// <param name="aX"></param>
        /// <param name="aY"></param>
        /// <param name="bX"></param>
        /// <param name="bY"></param>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        /// <returns>
        /// Return the cross product AB x BC.
        /// </returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/</remarks>
        public static double CrossProductVector2D_1(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY)
        {
            // Get the vectors' coordinates.
            double BAx = aX - bX;
            double BAy = aY - bY;
            double BCx = cX - bX;
            double BCy = cY - bY;

            // Calculate the Z coordinate of the cross product.
            return ((BAx) * (BCy) - (BAy) * (BCx));
        }

        /// <summary>
        /// Return the cross product AB x BC.
        /// The cross product is a vector perpendicular to AB
        /// and BC having length |AB| * |BC| * Sin(theta) and
        /// with direction given by the right-hand rule.
        /// For two vectors in the X-Y plane, the result is a
        /// vector with X and Y components 0 so the Z component
        /// gives the vector's length and direction.
        /// </summary>
        /// <param name="Ax"></param>
        /// <param name="Ay"></param>
        /// <param name="Bx"></param>
        /// <param name="By"></param>
        /// <param name="Cx"></param>
        /// <param name="Cy"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/perform-geometric-operations-on-polygons-in-c/</remarks>
        public static double CrossProductVector2D_2(
            double Ax, double Ay,
            double Bx, double By,
            double Cx, double Cy)
        {
            // Get the vectors' coordinates.
            double BAx = Ax - Bx;
            double BAy = Ay - By;
            double BCx = Cx - Bx;
            double BCy = Cy - By;

            // Calculate the Z coordinate of the cross product.
            return (BAx * BCy - BAy * BCx);
        }

        #endregion

        #region Cubic Interpolation of 1D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 1D cubic interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CubicInterpolate1DTests))]
        public static List<SpeedTester> CubicInterpolate1DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => CubicInterpolate1D(0, 1, 2, 3, 0.5d),
                $"{nameof(Experiments.CubicInterpolate1D)}(0, 1, 2, 3, 0.5d)"),
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
        public static double CubicInterpolate1D(double v0, double v1, double v2, double v3, double t)
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

        #region Cubic Interpolation of 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 2D cubic interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CubicInterpolate2DTests))]
        public static List<SpeedTester> CubicInterpolate2DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => CubicInterpolate2D(0, 1, 2, 3, 4, 5, 6, 7, 0.5d),
                $"{nameof(Experiments.CubicInterpolate2D)}(0, 1, 2, 3, 4, 5, 6, 7, 0.5d)"),
            };
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
        public static Tuple<double, double> CubicInterpolate2D(
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

            return new Tuple<double, double>(
                aX0 * t * mu2 + aX1 * mu2 + aX2 * t + x1,
                aY0 * t * mu2 + aY1 * mu2 + aY2 * t + y1);
        }

        #endregion

        #region Cubic Interpolation of 3D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 3D cubic interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CubicInterpolate3DTests))]
        public static List<SpeedTester> CubicInterpolate3DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => CubicInterpolate3D(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 0.5d),
                $"{nameof(Experiments.CubicInterpolate3D)}(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 0.5d)"),
            };
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
        public static Tuple<double, double, double> CubicInterpolate3D(
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

            return new Tuple<double, double, double>(
                aX0 * t * mu2 + aX1 * mu2 + aX2 * t + x1,
                aY0 * t * mu2 + aY1 * mu2 + aY2 * t + y1,
                aZ0 * t * mu2 + aZ1 * mu2 + aZ2 * t + z1);
        }

        #endregion

        #region Cubic CatmulRom Spline Interpolation of 1D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 1D Catmull Rom Spline interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CubicInterpolateCatmullRomSplines1DTests))]
        public static List<SpeedTester> CubicInterpolateCatmullRomSplines1DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => CubicInterpolateCatmullRomSplines1D(0, 1, 2, 3, 0.5d),
                $"{nameof(Experiments.CubicInterpolateCatmullRomSplines1D)}(0, 1, 2, 3, 0.5d)"),
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
        public static double CubicInterpolateCatmullRomSplines1D(double v0, double v1, double v2, double v3, double t)
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

        #region Cubic CatmulRom Spline Interpolation of 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 2D Catmull Rom Spline interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CubicInterpolateCatmullRomSplines2DTests))]
        public static List<SpeedTester> CubicInterpolateCatmullRomSplines2DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => CubicInterpolateCatmullRomSplines2D(0, 1, 2, 3, 4, 5, 6, 7, 0.5d),
                $"{nameof(Experiments.CubicInterpolateCatmullRomSplines2D)}(0, 1, 2, 3, 4, 5, 6, 7, 0.5d)"),
            };
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
        public static Tuple<double, double> CubicInterpolateCatmullRomSplines2D(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double t)
        {
            double mu2 = t * t;

            double aX0 = -0.5 * x0 + 1.5 * x1 - 1.5 * x2 + 0.5 * x3;
            double aY0 = -0.5 * y0 + 1.5 * y1 - 1.5 * y2 + 0.5 * y3;
            double aX1 = x0 - 2.5 * x1 + 2 * x2 - 0.5 * x3;
            double aY1 = y0 - 2.5 * y1 + 2 * y2 - 0.5 * y3;
            double aX2 = -0.5 * x0 + 0.5 * x2;
            double aY2 = -0.5 * y0 + 0.5 * y2;

            return new Tuple<double, double>(
                aX0 * t * mu2 + aX1 * mu2 + aX2 * t + x1,
                aY0 * t * mu2 + aY1 * mu2 + aY2 * t + y1);
        }

        #endregion

        #region Cubic CatmulRom Spline Interpolation of 3D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 3D Catmull Rom Spline interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CubicInterpolateCatmullRomSplines3DTests))]
        public static List<SpeedTester> CubicInterpolateCatmullRomSplines3DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => CubicInterpolateCatmullRomSplines3D(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 0.5d),
                $"{nameof(Experiments.CubicInterpolateCatmullRomSplines3D)}(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 0.5d)"),
            };
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
        public static Tuple<double, double, double> CubicInterpolateCatmullRomSplines3D(
            double x0, double y0, double z0,
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double x3, double y3, double z3,
            double t)
        {
            double mu2 = t * t;

            double aX0 = -0.5 * x0 + 1.5 * x1 - 1.5 * x2 + 0.5 * x3;
            double aY0 = -0.5 * y0 + 1.5 * y1 - 1.5 * y2 + 0.5 * y3;
            double aZ0 = -0.5 * z0 + 1.5 * z1 - 1.5 * z2 + 0.5 * z3;
            double aX1 = x0 - 2.5 * x1 + 2 * x2 - 0.5 * x3;
            double aY1 = y0 - 2.5 * y1 + 2 * y2 - 0.5 * y3;
            double aZ1 = z0 - 2.5 * z1 + 2 * z2 - 0.5 * z3;
            double aX2 = -0.5 * x0 + 0.5 * x2;
            double aY2 = -0.5 * y0 + 0.5 * y2;
            double aZ2 = -0.5 * z0 + 0.5 * z2;

            return new Tuple<double, double, double>(
                aX0 * t * mu2 + aX1 * mu2 + aX2 * t + x1,
                aY0 * t * mu2 + aY1 * mu2 + aY2 * t + y1,
                aZ0 * t * mu2 + aZ1 * mu2 + aZ2 * t + z1);
        }

        #endregion

        #region Cubic Bezier Interpolation of 1D Points

        #endregion

        #region Cubic Bezier Interpolation of 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 2D cubic interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(CubicBezierInterpolate2DTests))]
        public static List<SpeedTester> CubicBezierInterpolate2DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => CubicBezierInterpolate2D_0(0, 1, 2, 3, 4, 5, 6, 7, 0.5d),
                $"{nameof(Experiments.CubicBezierInterpolate2D_0)}(0, 1, 2, 3, 4, 5, 6, 7, 0.5d)"),
                new SpeedTester(() => CubicBezierInterpolate2D_1(0, 1, 2, 3, 4, 5, 6, 7, 0.5d),
                $"{nameof(Experiments.CubicBezierInterpolate2D_1)}(0, 1, 2, 3, 4, 5, 6, 7, 0.5d)"),
                new SpeedTester(() => CubicBezierInterpolate2D_3(0, 1, 2, 3, 4, 5, 6, 7, 0.5d),
                $"{nameof(Experiments.CubicBezierInterpolate2D_3)}(0, 1, 2, 3, 4, 5, 6, 7, 0.5d)"),
                new SpeedTester(() => CubicBezierInterpolate2D_4(0, 1, 2, 3, 4, 5, 6, 7, 0.5d),
                $"{nameof(Experiments.CubicBezierInterpolate2D_4)}(0, 1, 2, 3, 4, 5, 6, 7, 0.5d)"),
                new SpeedTester(() => CubicBezierInterpolate2D_5(0, 1, 2, 3, 4, 5, 6, 7, 0.5d),
                $"{nameof(Experiments.CubicBezierInterpolate2D_5)}(0, 1, 2, 3, 4, 5, 6, 7, 0.5d)"),
                new SpeedTester(() => CubicBezierInterpolate2D_6(0, 1, 2, 3, 4, 5, 6, 7, 0.5d),
                $"{nameof(Experiments.CubicBezierInterpolate2D_6)}(0, 1, 2, 3, 4, 5, 6, 7, 0.5d)"),
            };
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
        public static Tuple<double, double> CubicBezierInterpolate2D_0(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double t)
        {
            double mum1 = 1 - t;
            double mum13 = mum1 * mum1 * mum1;
            double mu3 = t * t * t;

            return new Tuple<double, double>(
                (mum13 * x0 + 3 * t * mum1 * mum1 * x1 + 3 * t * t * mum1 * x2 + mu3 * x3),
                (mum13 * y0 + 3 * t * mum1 * mum1 * y1 + 3 * t * t * mum1 * y2 + mu3 * y3)
                );
        }

        /// <summary>
        /// Calculate parametric value of x or y given t and the four point
        /// coordinates of a cubic bezier curve. This is a separate function
        /// because we need it for both x and y values.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        /// <remarks>http://www.lemoda.net/maths/bezier-length/index.html</remarks>
        public static Tuple<double, double> CubicBezierInterpolate2D_1(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double dX, double dY,
            double t)
        {
            // Formula from Wikipedia article on Bezier curves.
            return new Tuple<double, double>(
            aX * (1.0 - t) * (1.0 - t) * (1.0 - t) + 3.0 * bX * (1.0 - t) * (1.0 - t) * t + 3.0 * cX * (1.0 - t) * t * t + dX * t * t * t,
            aY * (1.0 - t) * (1.0 - t) * (1.0 - t) + 3.0 * bY * (1.0 - t) * (1.0 - t) * t + 3.0 * cY * (1.0 - t) * t * t + dY * t * t * t);
        }

        /// <summary>
        /// evaluate a point on a bezier-curve. t goes from 0 to 1.0
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
        /// <remarks>http://www.cubic.org/docs/bezier.htm</remarks>
        public static Tuple<double, double> CubicBezierInterpolate2D_2(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double t)
        {
            // point between a and b
            Tuple<double, double> ab = LinearInterpolate2D_0(x0, y0, x1, y1, t);
            // point between b and c
            Tuple<double, double> bc = LinearInterpolate2D_0(x1, y1, x2, y2, t);
            // point between c and d
            Tuple<double, double> cd = LinearInterpolate2D_0(x2, y2, x3, y3, t);
            // point between ab and bc
            Tuple<double, double> abbc = LinearInterpolate2D_0(ab.Item1, ab.Item2, bc.Item1, bc.Item2, t);
            // point between bc and cd
            Tuple<double, double> bccd = LinearInterpolate2D_0(bc.Item1, bc.Item2, cd.Item1, cd.Item2, t);
            // point on the bezier-curve
            return LinearInterpolate2D_0(abbc.Item1, abbc.Item2, bccd.Item1, bccd.Item2, t);
        }

        /// <summary>
        /// Function to Plot a Cubic Bezier
        /// </summary>
        /// <param name="a">the starting point, or A in the above diagram</param>
        /// <param name="b">the first control point, or B</param>
        /// <param name="c">the second control point, or C</param>
        /// <param name="d">the end point, or D</param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Tuple<double, double> CubicBezierInterpolate2D_3(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double dX, double dY,
            double t)
        {
            double V1 = t;
            double V2 = (1 - t);
            return new Tuple<double, double>(
                (aX * V2 * V2 * V2) + (3 * bX * V1 * V2 * V2) + (3 * cX * V1 * V1 * V2) + (dX * V2 * V2 * V2),
                ((aY * V2 * V2 * V2) + (3 * bY * V1 * V2 * V2) + (3 * cY * V1 * V1 * V2) + (dY * V2 * V2 * V2)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Tuple<double, double> CubicBezierInterpolate2D_4(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double dX, double dY,
            double t)
        {
            //Tuple<double, double> P = (v3 - v2) - (v0 - v1);
            //Tuple<double, double> Q = (v0 - v1) - P;
            //Tuple<double, double> R = v2 - v0;
            //Tuple<double, double> S = v1;
            //Tuple<double, double> P * Pow(x, 3) + Q * Pow(x, 2) + R * x + S;
            double PX = (dX - cX) - (aX - bX);
            double PY = (dY - cY) - (aY - bY);
            double QX = (aX - bX) - PX;
            double QY = (aY - bY) - PY;
            double RX = cX - aX;
            double RY = cY - aY;
            double SX = bX;
            double SY = bY;
            return new Tuple<double, double>(
                PX * (t * t * t) + QX * (t * t) + RX * t + SX,
                PY * (t * t * t) + QY * (t * t) + RY * t + SY);
        }

        /// <summary>
        ///  Code to generate a cubic Bezier curve
        /// </summary>
        /// <param name="a">the starting point, or A in the above diagram</param>
        /// <param name="b">the first control point, or B</param>
        /// <param name="c">the second control point, or C</param>
        /// <param name="d">the end point, or D</param>
        /// <param name="t">
        ///  t is the parameter value, 0 less than or equal to t less than or equal to 1
        /// </param>
        /// <returns></returns>
        /// <remarks>
        ///  Warning - untested code
        /// </remarks>
        public static Tuple<double, double> CubicBezierInterpolate2D_5(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double dX, double dY,
            double t)
        {
            // calculate the curve point at parameter value t 
            double tSquared = (t * t);
            double tCubed = (tSquared * t);

            // calculate the polynomial coefficients 
            Tuple<double, double> cC = new Tuple<double, double>((3 * (bX - aX)), (3 * (bY - aY)));
            Tuple<double, double> cB = new Tuple<double, double>(((3 * (cX - bX)) - cC.Item1), ((3 * (cY - bY)) - cC.Item2));
            Tuple<double, double> cA = new Tuple<double, double>((dX - (aX - (cC.Item1 - cB.Item1))), (dY - (aY - (cC.Item2 - cB.Item2))));
            return new Tuple<double, double>(((cA.Item1 * tCubed) + ((cB.Item1 * tSquared) + ((cC.Item1 * t) + aX))), ((cA.Item2 * tCubed) + ((cB.Item2 * tSquared) + ((cC.Item2 * t) + aY))));
        }

        /// <summary>
        /// Function to Interpolate a Cubic Bezier Spline 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Tuple<double, double> CubicBezierInterpolate2D_6(
            double aX, double aY,
            double bX, double bY,
            double cX, double cY,
            double dX, double dY,
            double t)
        {
            Tuple<double, double> c1 = new Tuple<double, double>(((dX - cX) - (aX - bX)), ((dY - cY) - (aY - bY)));
            Tuple<double, double> c2 = new Tuple<double, double>(((aX - bX) - aX), ((aY - bY) - aY));
            Tuple<double, double> c3 = new Tuple<double, double>((cX - aX), (cY - aY));
            Tuple<double, double> c4 = new Tuple<double, double>(aX, aY);
            return new Tuple<double, double>(
                (c1.Item1 * t * t * t + c2.Item1 * t * t * t + c3.Item1 * t + c4.Item1),
                (c1.Item2 * t * t * t + c2.Item2 * t * t * t + c3.Item2 * t + c4.Item2));
        }

        #endregion

        #region Cubic Bezier Interpolation of 3D Points

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
                new SpeedTester(() => Distance2D_1(new Tuple<double, double>(0, 0), new Tuple<double, double>(1, 0)),
                $"{nameof(Experiments.Distance2D_1)}((0, 0), (1, 0))"),
                new SpeedTester(() => Distance2D_2(0, 0, 1, 0),
                $"{nameof(Experiments.Distance2D_2)}(0, 0, 1, 0)"),
                new SpeedTester(() => Distance2D_3(0, 0, 1, 0),
                $"{nameof(Experiments.Distance2D_3)}(0, 0, 1, 0)"),
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
            Tuple<double, double> a,
            Tuple<double, double> b)
            => Sqrt((b.Item1 - a.Item1) * (b.Item1 - a.Item1)
                + (b.Item2 - a.Item2) * (b.Item1 - a.Item2));

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
        public static double Distance2D_2(
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
        public static double Distance2D_3(
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
        /// Set of tests to run testing methods that calculate the dot product of two 2D points.
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

        #region Dot Product of Two 3D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the dot product for two 3D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(DotProduct3D_0Tests))]
        public static List<SpeedTester> DotProduct3D_0Tests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => DotProduct(0, 0, 0, 1, 1, 1),
                $"{nameof(Experiments.DotProduct)}(0, 0, 0, 1, 1, 1)"),
                new SpeedTester(() => DotProduct(new Tuple<double, double, double>(0, 0, 0), 1, 1, 1),
                $"{nameof(Experiments.DotProduct)}((0, 0, 0), 1, 1, 1)"),
                new SpeedTester(() => DotProduct(new Tuple<double, double, double>(0, 0, 0), new Tuple<double, double, double>(1, 1, 1)),
                $"{nameof(Experiments.DotProduct)}((0, 0, 0), (1, 1, 1))"),
            };
        }

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double DotProduct(
            Tuple<double, double, double> tuple1,
            Tuple<double, double, double> tuple2)
            => DotProduct(
                tuple1.Item1, tuple1.Item2, tuple1.Item3,
                tuple2.Item1, tuple2.Item2, tuple2.Item3
                );

        #endregion

        #region Dot Product of the Vector of Three 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the dot product of three 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(DotProductVector2DTests))]
        public static List<SpeedTester> DotProductVector2DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => DotProductVector2D_0(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.DotProductVector2D_0)}(0, 0, 1, 0, 1, 1)"),
                new SpeedTester(() => DotProductVector2D_1(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.DotProductVector2D_1)}(0, 0, 1, 0, 1, 1)"),
                new SpeedTester(() => DotProductVector2D_2(0, 0, 1, 0, 1, 1),
                $"{nameof(Experiments.DotProductVector2D_2)}(0, 0, 1, 0, 1, 1)"),
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
        public static double DotProductVector2D_0(
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
        public static double DotProductVector2D_1(
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
        public static double DotProductVector2D_2(
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
        /// Set of tests to run testing methods that calculate the 1D Hermite interpolation of points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(HermiteInterpolate1DTests))]
        public static List<SpeedTester> HermiteInterpolate1DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => HermiteInterpolate1D(0, 1, 2, 3, 0.5d, 1, 0),
                $"{nameof(Experiments.HermiteInterpolate1D)}(0, 1, 2, 3, 0.5d, 1, 0)"),
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <param name="mu"></param>
        /// <param name="tension">1 is high, 0 normal, -1 is low</param>
        /// <param name="bias">0 is even,positive is towards first segment, negative towards the other</param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        static double HermiteInterpolate1D(
            double v0,
            double v1,
            double v2,
            double v3,
            double mu, double tension, double bias)
        {
            double m0, m1, mu2, mu3;
            double a0, a1, a2, a3;

            mu2 = mu * mu;
            mu3 = mu2 * mu;
            m0 = (v1 - v0) * (1 + bias) * (1 - tension) / 2;
            m0 += (v2 - v1) * (1 - bias) * (1 - tension) / 2;
            m1 = (v2 - v1) * (1 + bias) * (1 - tension) / 2;
            m1 += (v3 - v2) * (1 - bias) * (1 - tension) / 2;
            a0 = 2 * mu3 - 3 * mu2 + 1;
            a1 = mu3 - 2 * mu2 + mu;
            a2 = mu3 - mu2;
            a3 = -2 * mu3 + 3 * mu2;

            return (a0 * v1 + a1 * m0 + a2 * m1 + a3 * v2);
        }

        #endregion

        #region Hermite Interpolation of 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 2D Hermite interpolation of points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(HermiteInterpolate2DTests))]
        public static List<SpeedTester> HermiteInterpolate2DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => HermiteInterpolate2D(0, 1, 2, 3, 4, 5, 6, 7, 0.5d, 1, 0),
                $"{nameof(Experiments.HermiteInterpolate2D)}(0, 1, 2, 3, 4, 5, 6, 7, 0.5d, 1, 0)"),
            };
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
        /// <param name="mu"></param>
        /// <param name="tension">1 is high, 0 normal, -1 is low</param>
        /// <param name="bias">0 is even,positive is towards first segment, negative towards the other</param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        static Tuple<double, double> HermiteInterpolate2D(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double mu, double tension, double bias)
        {
            double mu2 = mu * mu;
            double mu3 = mu2 * mu;

            double mX0 = (x1 - x0) * (1 + bias) * (1 - tension) / 2;
            mX0 += (x2 - x1) * (1 - bias) * (1 - tension) / 2;
            double mY0 = (y1 - y0) * (1 + bias) * (1 - tension) / 2;
            mY0 += (y2 - y1) * (1 - bias) * (1 - tension) / 2;
            double mX1 = (x2 - x1) * (1 + bias) * (1 - tension) / 2;
            mX1 += (x3 - x2) * (1 - bias) * (1 - tension) / 2;
            double mY1 = (y2 - y1) * (1 + bias) * (1 - tension) / 2;
            mY1 += (y3 - y2) * (1 - bias) * (1 - tension) / 2;
            double a0 = 2 * mu3 - 3 * mu2 + 1;
            double a1 = mu3 - 2 * mu2 + mu;
            double a2 = mu3 - mu2;
            double a3 = -2 * mu3 + 3 * mu2;

            return new Tuple<double, double>(
                a0 * x1 + a1 * mX0 + a2 * mX1 + a3 * x2,
                a0 * y1 + a1 * mY0 + a2 * mY1 + a3 * y2);
        }

        #endregion

        #region Hermite Interpolation of 3D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 3D Hermite interpolation of points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(HermiteInterpolate3DTests))]
        public static List<SpeedTester> HermiteInterpolate3DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => HermiteInterpolate3D(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 0.5d, 1, 0),
                $"{nameof(Experiments.HermiteInterpolate3D)}(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 0.5d, 1, 0)"),
            };
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
        /// <param name="mu"></param>
        /// <param name="tension">1 is high, 0 normal, -1 is low</param>
        /// <param name="bias">0 is even,positive is towards first segment, negative towards the other</param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/miscellaneous/interpolation/</remarks>
        static Tuple<double, double, double> HermiteInterpolate3D(
            double x0, double y0, double z0,
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double x3, double y3, double z3,
            double mu, double tension, double bias)
        {
            double mu2 = mu * mu;
            double mu3 = mu2 * mu;

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
            double a1 = mu3 - 2 * mu2 + mu;
            double a2 = mu3 - mu2;
            double a3 = -2 * mu3 + 3 * mu2;

            return new Tuple<double, double, double>(
                a0 * x1 + a1 * mX0 + a2 * mX1 + a3 * x2,
                a0 * y1 + a1 * mY0 + a2 * mY1 + a3 * y2,
                a0 * z1 + a1 * mZ0 + a2 * mZ1 + a3 * z2);
        }

        #endregion

        #region Intersection of Circle and Circle

        /// <summary>
        /// Find the points where the two circles intersect.
        /// </summary>
        /// <param name="cx0"></param>
        /// <param name="cy0"></param>
        /// <param name="radius0"></param>
        /// <param name="cx1"></param>
        /// <param name="cy1"></param>
        /// <param name="radius1"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/09/determine-where-two-circles-intersect-in-c/</remarks>
        private static Tuple<int, Tuple<double, double>, Tuple<double, double>> FindCircleCircleIntersections(
            double cx0,
            double cy0,
            double radius0,
            double cx1,
            double cy1,
            double radius1)
        {
            // Find the distance between the centers.
            double dx = cx0 - cx1;
            double dy = cy0 - cy1;
            double dist = Sqrt(dx * dx + dy * dy);

            Tuple<double, double> intersection1;
            Tuple<double, double> intersection2;

            // See how many solutions there are.
            if (dist > radius0 + radius1)
            {
                // No solutions, the circles are too far apart.
                intersection1 = new Tuple<double, double>(double.NaN, double.NaN);
                intersection2 = new Tuple<double, double>(double.NaN, double.NaN);
                return new Tuple<int, Tuple<double, double>, Tuple<double, double>>(0, intersection1, intersection2);
            }
            else if (dist < Abs(radius0 - radius1))
            {
                // No solutions, one circle contains the other.
                intersection1 = new Tuple<double, double>(double.NaN, double.NaN);
                intersection2 = new Tuple<double, double>(double.NaN, double.NaN);
                return new Tuple<int, Tuple<double, double>, Tuple<double, double>>(0, intersection1, intersection2);
            }
            else if ((dist == 0) && (radius0 == radius1))
            {
                // No solutions, the circles coincide.
                intersection1 = new Tuple<double, double>(double.NaN, double.NaN);
                intersection2 = new Tuple<double, double>(double.NaN, double.NaN);
                return new Tuple<int, Tuple<double, double>, Tuple<double, double>>(0, intersection1, intersection2);
            }
            else
            {
                // Find a and h.
                double a = (radius0 * radius0 -
                    radius1 * radius1 + dist * dist) / (2 * dist);
                double h = Sqrt(radius0 * radius0 - a * a);

                // Find P2.
                double cx2 = cx0 + a * (cx1 - cx0) / dist;
                double cy2 = cy0 + a * (cy1 - cy0) / dist;

                // Get the points P3.
                intersection1 = new Tuple<double, double>(
                    (cx2 + h * (cy1 - cy0) / dist),
                    (cy2 - h * (cx1 - cx0) / dist));
                intersection2 = new Tuple<double, double>(
                    (cx2 - h * (cy1 - cy0) / dist),
                    (cy2 + h * (cx1 - cx0) / dist));

                // See if we have 1 or 2 solutions.
                if (dist == radius0 + radius1)
                {
                    return new Tuple<int, Tuple<double, double>, Tuple<double, double>>(1, intersection1, intersection2);
                }

                return new Tuple<int, Tuple<double, double>, Tuple<double, double>>(2, intersection1, intersection2);
            }
        }

        #endregion

        #region Intersection of Circle and line

        /// <summary>
        /// Find the points of intersection.
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        /// <remarks>http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/</remarks>
        private static Tuple<int, Tuple<double, double>, Tuple<double, double>> LineCircle(
            Tuple<double, double> center,
            double radius,
            Tuple<double, double> point1,
            Tuple<double, double> point2)
        {
            double t;

            double dx = point2.Item1 - point1.Item1;
            double dy = point2.Item2 - point1.Item2;

            double A = dx * dx + dy * dy;
            double B = 2 * (dx * (point1.Item1 - center.Item1) + dy * (point1.Item2 - center.Item2));
            double C = (point1.Item1 - center.Item1) * (point1.Item1 - center.Item1) + (point1.Item2 - center.Item2) * (point1.Item2 - center.Item2) - radius * radius;

            Tuple<double, double> intersection1;
            Tuple<double, double> intersection2;

            double det = B * B - 4 * A * C;
            if ((A <= 0.0000001) || (det < 0))
            {
                // No real solutions.
                intersection1 = new Tuple<double, double>(double.NaN, double.NaN);
                intersection2 = new Tuple<double, double>(double.NaN, double.NaN);
                return new Tuple<int, Tuple<double, double>, Tuple<double, double>>(0, intersection1, intersection2);
            }
            else if (det == 0)
            {
                // One solution.
                t = -B / (2 * A);
                intersection1 = new Tuple<double, double>(point1.Item1 + t * dx, point1.Item2 + t * dy);
                intersection2 = new Tuple<double, double>(double.NaN, double.NaN);
                return new Tuple<int, Tuple<double, double>, Tuple<double, double>>(1, intersection1, intersection2);
            }
            else
            {
                // Two solutions.
                t = ((-B + Sqrt(det)) / (2 * A));
                intersection1 = new Tuple<double, double>(point1.Item1 + t * dx, point1.Item2 + t * dy);
                t = ((-B - Sqrt(det)) / (2 * A));
                intersection2 = new Tuple<double, double>(point1.Item1 + t * dx, point1.Item2 + t * dy);
                return new Tuple<int, Tuple<double, double>, Tuple<double, double>>(2, intersection1, intersection2);
            }
        }

        #endregion

        #region Intersection of two Line Segments

        /// <summary>
        /// Set of tests to run testing methods that calculate the 3D Hermite interpolation of points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(LineIntersection2DTests))]
        public static List<SpeedTester> LineIntersection2DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => Intersection0(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.Intersection0)}(0, 0, 2, 2, 0, 2, 2, 0)"),
                new SpeedTester(() => Intersection1(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.Intersection1)}(0, 0, 2, 2, 0, 2, 2, 0)"),
                new SpeedTester(() => Intersection2(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.Intersection2)}(0, 0, 2, 2, 0, 2, 2, 0)"),
                new SpeedTester(() => Intersection3(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.Intersection3)}(0, 0, 2, 2, 0, 2, 2, 0)"),
                new SpeedTester(() => Intersection4(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.Intersection4)}(0, 0, 2, 2, 0, 2, 2, 0)"),
                new SpeedTester(() => Intersection5(0, 0, 2, 2, 0, 2, 2, 0),
                $"{nameof(Experiments.Intersection5)}(0, 0, 2, 2, 0, 2, 2, 0)"),
            };
        }

        /// <summary>
        /// Find the intersection point between two lines.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="x4"></param>
        /// <param name="y4"></param>
        /// <returns>Returns the point of intersection.</returns>
        public static Tuple<bool, Tuple<double, double>> Intersection0(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double x4, double y4)
        {
            // Calculate the delta length vectors for the line segments.
            double deltaBAI = (x2 - x1);
            double deltaBAJ = (y2 - y1);
            double deltaDCI = (x4 - x3);
            double deltaDCJ = (y4 - y3);
            double deltaCAI = (x3 - x1);
            double deltaCAJ = (y3 - y1);

            //  If the segments are parallel return false.
            if ((deltaDCI * deltaBAJ) == (deltaDCJ * deltaBAI)) return new Tuple<bool, Tuple<double, double>>(false, null);

            // Find the index where the intersection point lies on the line.
            double s = (((deltaBAI * deltaCAJ) + (deltaBAJ * -deltaCAI)) / ((deltaDCI * deltaBAJ) - (deltaDCJ * deltaBAI)));
            double t = (((deltaDCI * -deltaCAJ) + (deltaDCJ * deltaCAI)) / ((deltaDCJ * deltaBAI) - (deltaDCI * deltaBAJ)));

            return new Tuple<bool, Tuple<double, double>>(
                // Check whether the point is on the segment.
                (s >= 0d) && (s <= 1d) && (t >= 0d) && (t <= 1d),
                // If the point exists, the point of intersection is:
                new Tuple<double, double>(x1 + (t * deltaBAI), y1 + (t * deltaBAJ)));
        }

        /// <summary>
        /// Find the intersection point between two lines.
        /// </summary>
        /// <param name="x1">The x component of the first point of the first line.</param>
        /// <param name="y1">The y component of the first point of the first line.</param>
        /// <param name="x2">The x component of the second point of the first line.</param>
        /// <param name="y2">The y component of the second point of the first line.</param>
        /// <param name="x3">The x component of the first point of the second line.</param>
        /// <param name="y3">The y component of the first point of the second line.</param>
        /// <param name="x4">The x component of the second point of the second line.</param>
        /// <param name="y4">The y component of the second point of the second line.</param>
        /// <returns>Returns the point of intersection.</returns>
        /// <remarks>https://www.topcoder.com/community/data-science/data-science-tutorials/geometry-concepts-line-intersection-and-its-applications/</remarks>
        public static Tuple<bool, Tuple<double, double>> Intersection1(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double x4, double y4)
        {
            // Calculate the delta length vectors for the line segments.
            double deltaAI = (x1 - x2);
            double deltaAJ = (y2 - y1);
            double deltaBI = (y4 - y3);
            double deltaBJ = (x3 - x4);

            // Calculate the determinant of the vectors.
            double determinant = (deltaAJ * deltaBJ) - (deltaBI * deltaAI);

            // Check if the lines are parallel.
            if (determinant == 0) return new Tuple<bool, Tuple<double, double>>(false, null);

            // Find the index where the intersection point lies on the line.
            double s = (deltaAJ * x1 + deltaAI * y1) / -determinant;
            double t = (deltaBI * x3 + deltaBJ * y3) / determinant;

            // Interpolate the point of intersection.
            return new Tuple<bool, Tuple<double, double>>(
                // Check whether the point is on the segment.
                (s >= 0d) && (s <= 1d) && (t >= 0d) && (t <= 1d),
                // If the point exists, the point of intersection is:
                new Tuple<double, double>(-((deltaAI * t) + (deltaBJ * s)), (deltaAJ * t) + (deltaBI * s)));
        }

        /// <summary>
        /// Find the intersection point between two lines.
        /// </summary>
        /// <param name="x1">The x component of the first point of the first line.</param>
        /// <param name="y1">The y component of the first point of the first line.</param>
        /// <param name="x2">The x component of the second point of the first line.</param>
        /// <param name="y2">The y component of the second point of the first line.</param>
        /// <param name="x3">The x component of the first point of the second line.</param>
        /// <param name="y3">The y component of the first point of the second line.</param>
        /// <param name="x4">The x component of the second point of the second line.</param>
        /// <param name="y4">The y component of the second point of the second line.</param>
        /// <returns>Returns the point of intersection.</returns>
        /// <remarks>http://www.vb-helper.com/howto_segments_intersect.html</remarks>
        public static Tuple<bool, Tuple<double, double>> Intersection2(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double x4, double y4)
        {
            // Calculate the delta length vectors for the line segments.
            double deltaAI = (x2 - x1);
            double deltaAJ = (y2 - y1);
            double deltaBI = (x4 - x3);
            double deltaBJ = (y4 - y3);

            // Calculate the determinant of the coefficient matrix.
            double determinant = (deltaBJ * deltaAI) - (deltaBI * deltaAJ);

            // Check if the line are parallel.
            if (determinant == 0) return new Tuple<bool, Tuple<double, double>>(false, null);

            // Find the index where the intersection point lies on the line.
            double s = ((x1 - x3) * deltaAJ + (y3 - y1) * deltaAI) / -determinant;
            double t = ((x3 - x1) * deltaBJ + (y1 - y3) * deltaBI) / determinant;

            return new Tuple<bool, Tuple<double, double>>(
                 // Check whether the point is on the segment.
                 (t >= 0d) && (t <= 1d) && (s >= 0d) && (s <= 1d),
                // If it exists, the point of intersection is:
                new Tuple<double, double>(x1 + t * deltaAI, y1 + t * deltaAJ));
        }

        /// <summary>
        /// Find the intersection point between two lines.
        /// </summary>
        /// <param name="x1">The x component of the first point of the first line.</param>
        /// <param name="y1">The y component of the first point of the first line.</param>
        /// <param name="x2">The x component of the second point of the first line.</param>
        /// <param name="y2">The y component of the second point of the first line.</param>
        /// <param name="x3">The x component of the first point of the second line.</param>
        /// <param name="y3">The y component of the first point of the second line.</param>
        /// <param name="x4">The x component of the second point of the second line.</param>
        /// <param name="y4">The y component of the second point of the second line.</param>
        /// <returns>Returns the point of intersection.</returns>
        /// <remarks>http://csharphelper.com/blog/2014/08/determine-where-two-lines-intersect-in-c/</remarks>
        public static Tuple<bool, Tuple<double, double>> Intersection3(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double x4, double y4)
        {
            // Calculate the delta length vectors for the line segments.
            double deltaAI = (x2 - x1);
            double deltaAJ = (y2 - y1);
            double deltaBI = (x4 - x3);
            double deltaBJ = (y4 - y3);

            // Calculate the determinant of the coefficient matrix.
            double determinant = (deltaBI * deltaAJ) - (deltaBJ * deltaAI);

            // Check if the lines are parallel.
            if (determinant == 0) return new Tuple<bool, Tuple<double, double>>(false, null);

            // Find the index where the intersection point lies on the line.
            double s = ((x3 - x1) * deltaAJ + (y1 - y3) * deltaAI) / -determinant;
            double t = ((x1 - x3) * deltaBJ + (y3 - y1) * deltaBI) / determinant;

            // Interpolate the point of intersection.
            return new Tuple<bool, Tuple<double, double>>(
                // The segments intersect if t1 and t2 are between 0 and 1.
                (t >= 0d) && (t <= 1d) && (s >= 0d) && (s <= 1d),
                // If it exists, the point of intersection is:
                new Tuple<double, double>(x1 + t * deltaAI, y1 + t * deltaAJ));

            //// Find the closest points on the segments.
            //if (t < 0) t = 0;
            //else if (t > 1) t = 1;
            //if (s < 0) s = 0;
            //else if (s > 1) s = 1;

            //Point2D close_p1 = new Point2D(aX + deltaAI * t, aY + deltaAJ * t);
            //Point2D close_p2 = new Point2D(cX + deltaBI * s, cY + deltaBJ * s);
        }

        /// <summary>
        /// SlopeMax is a large value "close to infinity" (Close to the largest value allowed for the data 
        /// type). Used in the Slope of a LineSeg
        /// </summary>
        /// <remarks></remarks>
        public const double SlopeMax = 9223372036854775807d;

        /// <summary>
        /// Find the intersection point between two lines.
        /// </summary>
        /// <param name="x1">The x component of the first point of the first line.</param>
        /// <param name="y1">The y component of the first point of the first line.</param>
        /// <param name="x2">The x component of the second point of the first line.</param>
        /// <param name="y2">The y component of the second point of the first line.</param>
        /// <param name="x3">The x component of the first point of the second line.</param>
        /// <param name="y3">The y component of the first point of the second line.</param>
        /// <param name="x4">The x component of the second point of the second line.</param>
        /// <param name="y4">The y component of the second point of the second line.</param>
        /// <returns>Returns the point of intersection.</returns>
        /// <remarks>http://www.gamedev.net/page/resources/_/technical/math-and-physics/fast-2d-line-intersection-algorithm-r423</remarks>
        public static Tuple<bool, Tuple<double, double>> Intersection4(
            double x1, double y1,
            double x2, double y2,
            double x3, double y3,
            double x4, double y4)
        {
            // Compute the slopes of each line. Note the kludge for infinity, however, this will be close enough.
            double slope1 = (x2 == x1) ? SlopeMax : (y2 - y1) / (x2 - x1);
            double slope2 = (x4 == x3) ? SlopeMax : (y4 - y3) / (x4 - x3);

            // Check if the lines are parallel.
            if (slope1 == slope2) return new Tuple<bool, Tuple<double, double>>(false, null);

            // Compute the determinate of the coefficient matrix.
            double determinate = slope2 - slope1;

            double s = (y3 - (slope2 * x3)) / -determinate;
            double t = (y1 - (slope1 * x1)) / determinate;

            // Use Cramer's rule to compute the return values.
            return new Tuple<bool, Tuple<double, double>>(
                (s >= 0) && (s <= 1) && (t >= 0) && (t <= 1),
                new Tuple<double, double>(t + s, slope2 * t + slope1 * s));
        }

        /// <summary>
        /// Returns the intersection of the two lines (line segments are passed in, but they are treated like infinite lines)
        /// </summary>
        /// <remarks>
        /// http://rosettacode.org/wiki/Sutherland-Hodgman_polygon_clipping#C.23
        /// Got this here:
        /// http://stackoverflow.com/questions/14480124/how-do-i-detect-triangle-and-rectangle-intersection
        /// </remarks>
        public static Tuple<bool, Tuple<double, double>> Intersection5(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double x3, double y3)
        {
            double direction1I = x1 - x0;
            double direction1J = y1 - y0;
            double direction2I = x3 - x2;
            double direction2J = y3 - y2;

            double dotPerp = (direction1I * direction2J) - (direction1J * direction2I);

            // Check if the lines are parallel.
            if (dotPerp == 0) return new Tuple<bool, Tuple<double, double>>(false, null);

            // If it's 0, it means the lines are parallel so have infinite intersection points
            if (NearZero0(dotPerp)) return null;

            double cI = x2 - x0;
            double cJ = y2 - y0;
            double t = (cI * direction2J - cJ * direction2I) / dotPerp;
            //if ((t < 0) || (t > 1)) return null; // lies outside the line segment

            double u = (cI * direction1J - cJ * direction1I) / dotPerp;
            //if ((u < 0) || (u > 1)) return null; // lies outside the line segment

            //	Return the intersection point
            return new Tuple<bool, Tuple<double, double>>(
                (t > 0) && (t < 1) && (u > 0) && (u < 1),
                new Tuple<double, double>(
                x0 + (t * direction1I),
                y0 + (t * direction1J)));
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

        #region Mixed product of Three 3D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the mixed product for three 3D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(MixedProduct3D_0Tests))]
        public static List<SpeedTester> MixedProduct3D_0Tests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => MixedProduct3D_0(0, 0,0, 1, 1, 1,2,2,2),
                $"{nameof(Experiments.MixedProduct3D_0)}(0, 0, 1, 1, 0.5d)"),
            };
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
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="z3"></param>
        /// <returns></returns>
        public static double MixedProduct3D_0(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double x3, double y3, double z3)
            => DotProduct(CrossProduct2Points3D_0(x1, y1, z1, x2, y2, z2), x3, y3, z3);

        #endregion

        #region Near Zero Inquiry

        /// <summary>
        /// 
        /// </summary>
        public const double NearZeroEpsilon = 1E-20;

        /// <summary>
        /// Set of tests to run testing methods that query whether a number is near zero.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(NearZeroTests))]
        public static List<SpeedTester> NearZeroTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => NearZero0(0.000000001d),
                $"{nameof(Experiments.NearZero0)}(0.000000001d)"),
                new SpeedTester(() => NearZero1(0.000000001d),
                $"{nameof(Experiments.NearZero1)}(0.000000001d)"),
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NearZero0(double value, double epsilon = NearZeroEpsilon)
            => (value > -epsilon) && (value < -epsilon);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool NearZero1(double value, double epsilon = NearZeroEpsilon)
            => Abs(value) <= epsilon;

        #endregion

        #region Normalize a 2D Vector

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

        #endregion

        #region Normalize a 3D Vector

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

        #endregion

        #region Normalize the Vector Between Two 2D Points

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

        #endregion

        #region Normalize the Vector Between Two 3D Points

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

        #endregion

        #region Perimeter of a Polygon

        /// <summary>
        /// Set of tests to run testing methods that calculate the cross product of three 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(Perimeter2DTests))]
        public static List<SpeedTester> Perimeter2DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => Perimeter0(new List<Tuple<double, double>>() {new Tuple<double, double>(0,0), new Tuple<double, double>(1,0), new Tuple<double, double>(0,1)}),
                $"{nameof(Experiments.Perimeter0)}((x, y){{(0,0),(1,0),(0,1)}})"),
                new SpeedTester(() => Perimeter1(new List<Tuple<double, double>>() {new Tuple<double, double>(0,0), new Tuple<double, double>(1,0), new Tuple<double, double>(0,1)}),
                $"{nameof(Experiments.Perimeter1)}((x, y){{(0,0),(1,0),(0,1)}})"),
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static double Perimeter0(List<Tuple<double, double>> points)
        {
            var last = points[0];
            double dist = 0;
            foreach (var cur in points.Skip(1))
            {
                dist += Distance2D_0(last.Item1, last.Item2, cur.Item1, cur.Item2);
                last = cur;
            }
            return dist;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/2227828/find-the-distance-required-to-navigate-a-list-of-points-using-linq</remarks>
        public static double Perimeter1(List<Tuple<double, double>> points)
        {
            return points.Zip(points.Skip(1), Distance2D_1).Sum();
        }

        #endregion

        #region Perpendicular Vector in the Clockwise Direction

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

        #endregion

        #region Perpendicular Vector in the Counter Clockwise Direction

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

        #endregion

        #region Point in Circle

        /// <summary>
        /// Set of tests to run testing methods that calculate whether a point is within a circle.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(PointInCircle2DTests))]
        public static List<SpeedTester> PointInCircle2DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => PointInCircle(0, 0, 2, 1, 1),
                $"{nameof(Experiments.PointInCircle)}(0, 0, 2, 1, 1)"),
            };
        }

        /// <summary>
        /// Find out if a Point is in a Circle. 
        /// </summary>
        /// <returns></returns>
        public static bool PointInCircle(
            double centerX,
            double centerY,
            double radius,
            double X,
            double Y)
        {
            return (radius > Distance2D_0(centerX, centerY, X, Y));
        }

        #endregion

        #region Point in Polygon

        /// <summary>
        /// Set of tests to run testing methods that calculate the cross product of three 2D points.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(PointInPolygonTests))]
        public static List<SpeedTester> PointInPolygonTests()
        {
            List<PointF> polygon = new List<PointF>() {
                new PointF(0, 0),
                new PointF(2, 0),
                new PointF(0, 2) };
            Tuple<List<double>, List<double>> PatrickMullenValues = PrecalcPointInPolygonPatrickMullenValues(polygon);
            PointF point = new PointF(1, 1);
            return new List<SpeedTester>() {
                new SpeedTester(() => PointInPolygonDarelRexFinley(polygon, point),
                $"{nameof(Experiments.PointInPolygonDarelRexFinley)}(polygon, {point})"),
                new SpeedTester(() => PointInPolygonNathanMercer(polygon, point),
                $"{nameof(Experiments.PointInPolygonNathanMercer)}(polygon, {point})"),
                new SpeedTester(() => PointInPolygonLaschaLagidse(polygon, point),
                $"{nameof(Experiments.PointInPolygonLaschaLagidse)}(polygon, {point})"),
                new SpeedTester(() => PointInPolygonPatrickMullen(polygon, point, PatrickMullenValues.Item1, PatrickMullenValues.Item2),
                $"{nameof(Experiments.PointInPolygonPatrickMullen)}(polygon, {point}, constant, multiple)"),
                //new SpeedTester(() => PointInPolygonMeowNET(polygon, point),
                //$"{nameof(Experiments.PointInPolygonMeowNET)}(polygon, {point})"),
                new SpeedTester(() => PointInPolygonAlienRyderFlex(polygon, point),
                $"{nameof(Experiments.PointInPolygonAlienRyderFlex)}(polygon, {point})"),
                new SpeedTester(() => PointInPolygonLaschaLagidse2(polygon, point),
                $"{nameof(Experiments.PointInPolygonLaschaLagidse2)}(polygon, {point})"),
                new SpeedTester(() => PointInPolygonGilKr(polygon, point),
                $"{nameof(Experiments.PointInPolygonGilKr)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonMKatzWRandolphFranklin(polygon, point),
                //$"{nameof(Experiments.PointInPolygonMKatzWRandolphFranklin)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonRodStephens(polygon, point),
                //$"{nameof(Experiments.PointInPolygonRodStephens)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonSaeedAmiri(polygon, point),
                //$"{nameof(Experiments.PointInPolygonSaeedAmiri)}(polygon, {point})"),
                new SpeedTester(() => PointInPolygonKeith(polygon, point),
                $"{nameof(Experiments.PointInPolygonKeith)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonJerryKnauss(polygon, point),
                //$"{nameof(Experiments.PointInPolygonJerryKnauss)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonJerryKnauss2(polygon, point),
                //$"{nameof(Experiments.PointInPolygonJerryKnauss2)}(polygon, {point})"),
                new SpeedTester(() => PointInPolygonPaulBourke(polygon, point),
                $"{nameof(Experiments.PointInPolygonPaulBourke)}(polygon, {point})"),
                new SpeedTester(() => PointInPolygonWRandolphFranklin(polygon, point),
                $"{nameof(Experiments.PointInPolygonWRandolphFranklin)}(polygon, {point})"),
                //new SpeedTester(() => PointInPolygonPhilippeReverdy(polygon, point),
                //$"{nameof(Experiments.PointInPolygonPhilippeReverdy)}(polygon, {point})"),
                new SpeedTester(() => PointInPolygonBobStein(polygon, point),
                $"{nameof(Experiments.PointInPolygonBobStein)}(polygon, {point})"),
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="polygon"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://paulbourke.net/geometry/polygonmesh/
        /// http://paulbourke.net/geometry/polygonmesh/contains.txt
        /// </remarks>
        public static bool PointInPolygonJerryKnauss(
            List<PointF> polygon, PointF point)
        {
            bool result = false;

            for (int i = 0; i < polygon.Count - 1; i++)
            {
                if ((((polygon[i + 1].Y < point.Y) && (point.Y < polygon[i].Y))
                    || ((polygon[i].Y < point.Y) && (point.Y < polygon[i + 1].Y)))
                    && (point.X < (polygon[i].X - polygon[i + 1].X)
                    * (point.Y - polygon[i + 1].Y)
                    / (polygon[i].Y - polygon[i + 1].Y) + polygon[i + 1].X))
                {
                    result = !result;
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="polygon"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://paulbourke.net/geometry/polygonmesh/
        /// http://paulbourke.net/geometry/polygonmesh/contains.txt
        /// </remarks>
        public static bool PointInPolygonJerryKnauss2(
            List<PointF> polygon, PointF point)
        {
            int j = polygon.Count - 1;
            bool result = false;

            for (int i = 0; i < polygon.Count; i++)
            {
                if ((((polygon[j].Y < point.Y) && (point.Y < polygon[i].Y))
                    || ((polygon[i].Y < point.Y) && (point.Y < polygon[j].Y)))
                    && (point.X < (polygon[i].X - polygon[j].X)
                    * (point.Y - polygon[j].Y)
                    / (polygon[i].Y - polygon[j].Y) + polygon[j].X))
                {
                    result = !result;
                }

                j = i;
            }
            return result;
        }

        /// <summary>
        /// The function will return true if the point x,y is inside the polygon, or
        /// false if it is not.  If the point is exactly on the edge of the polygon,
        /// then the function may return true or false.
        /// </summary>
        /// <param name="point">point to be tested</param>
        /// <param name="polygon">coordinates of corners</param>
        /// <returns></returns>
        /// <remarks>http://alienryderflex.com/polygon/</remarks>
        public static bool PointInPolygonDarelRexFinley(
            List<PointF> polygon, PointF point)
        {
            int j = polygon.Count - 1;
            bool oddNodes = false;

            for (int i = 0; i < polygon.Count; i++)
            {
                if (polygon[i].Y < point.Y && polygon[j].Y >= point.Y
                || polygon[j].Y < point.Y && polygon[i].Y >= point.Y)
                {
                    if (polygon[i].X + (point.Y - polygon[i].Y)
                        / (polygon[j].Y - polygon[i].Y) * (polygon[j].X - polygon[i].X) < point.X)
                    {
                        oddNodes = !oddNodes;
                    }
                }
                j = i;
            }

            return oddNodes;
        }

        /// <summary>
        /// The function will return true if the point x,y is inside the polygon, or
        /// false if it is not.  If the point is exactly on the edge of the polygon,
        /// then the function may return true or false.
        /// </summary>
        /// <param name="point">point to be tested</param>
        /// <param name="polygon">coordinates of corners</param>
        /// <returns></returns>
        /// <remarks>http://alienryderflex.com/polygon/</remarks>
        public static bool PointInPolygonNathanMercer(
            List<PointF> polygon, PointF point)
        {
            int j = polygon.Count - 1;
            bool oddNodes = false;

            for (int i = 0; i < polygon.Count; i++)
            {
                //  Note that division by zero is avoided because the division is protected
                //  by the "if" clause which surrounds it.
                if (polygon[i].Y < point.Y && polygon[j].Y >= point.Y
                || polygon[j].Y < point.Y && polygon[i].Y >= point.Y
                && (polygon[i].X <= point.X || polygon[j].X <= point.X))
                {
                    if (polygon[i].X + (point.Y - polygon[i].Y)
                        / (polygon[j].Y - polygon[i].Y) * (polygon[j].X - polygon[i].X) < point.X)
                    {
                        oddNodes = !oddNodes;
                    }
                }

                j = i;
            }

            return oddNodes;
        }

        /// <summary>
        ///  The function will return YES if the point x,y is inside the polygon, or
        ///  NO if it is not.  If the point is exactly on the edge of the polygon,
        ///  then the function may return YES or NO.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="polygon"></param>
        /// <returns></returns>
        /// <remarks>http://alienryderflex.com/polygon/</remarks>
        public static bool PointInPolygonLaschaLagidse(
            List<PointF> polygon, PointF point)
        {
            int i;
            int j = polygon.Count - 1;
            bool oddNodes = false;

            for (i = 0; i < polygon.Count; i++)
            {
                //  Note that division by zero is avoided because the division is protected
                //  by the "if" clause which surrounds it.
                if ((polygon[i].Y < point.Y && polygon[j].Y >= point.Y
                || polygon[j].Y < point.Y && polygon[i].Y >= point.Y)
                && (polygon[i].X <= point.X || polygon[j].X <= point.X))
                {
                    oddNodes ^= (polygon[i].X + (point.Y - polygon[i].Y)
                        / (polygon[j].Y - polygon[i].Y)
                        * (polygon[j].X - polygon[i].X) < point.X);
                }

                j = i;
            }

            return oddNodes;
        }

        /// <summary>
        ///  USAGE:
        ///  Call precalc_values() to initialize the constant[] and multiple[] arrays,
        ///  then call pointInPolygon(x, y) to determine if the point is in the polygon.
        ///
        ///  The function will return YES if the point x,y is inside the polygon, or
        ///  NO if it is not.  If the point is exactly on the edge of the polygon,
        ///  then the function may return YES or NO.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="polygon">coordinates of corners</param>
        /// <param name="constant">storage for precalculated constants (same size as polyX)</param>
        /// <param name="multiple">storage for precalculated multipliers (same size as polyX)</param>
        /// <returns></returns>
        /// <remarks>http://alienryderflex.com/polygon/</remarks>
        public static bool PointInPolygonPatrickMullen(
            List<PointF> polygon, PointF point,
            List<double> constant, List<double> multiple)
        {
            int i, j = polygon.Count - 1;
            bool oddNodes = false;

            for (i = 0; i < polygon.Count; i++)
            {
                //  Note that division by zero is avoided because the division is protected
                //  by the "if" clause which surrounds it.
                if ((polygon[i].Y < point.Y && polygon[j].Y >= point.Y
                || polygon[j].Y < point.Y && polygon[i].Y >= point.Y))
                {
                    oddNodes ^= (point.Y * multiple[i] + constant[i] < point.X);
                }
                j = i;
            }

            return oddNodes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon">coordinates of corners</param>
        /// <param name="constant">storage for precalculated constants (same size as polyX)</param>
        /// <param name="multiple">storage for precalculated multipliers (same size as polyX)</param>
        /// <remarks>http://alienryderflex.com/polygon/</remarks>
        public static Tuple<List<double>, List<double>> PrecalcPointInPolygonPatrickMullenValues(
            List<PointF> polygon)
        {
            if (polygon == null) return null;

            double[] constant = new double[polygon.Count];
            double[] multiple = new double[polygon.Count];

            int i, j = polygon.Count - 1;

            for (i = 0; i < polygon.Count; i++)
            {
                if (polygon[j].Y == polygon[i].Y)
                {
                    constant[i] = polygon[i].X;
                    multiple[i] = 0;
                }
                else
                {
                    constant[i] = polygon[i].X - (polygon[i].Y * polygon[j].X)
                        / (polygon[j].Y - polygon[i].Y) + (polygon[i].Y * polygon[i].X)
                        / (polygon[j].Y - polygon[i].Y);
                    multiple[i] = (polygon[j].X - polygon[i].X) / (polygon[j].Y - polygon[i].Y);
                }
                j = i;
            }

            return new Tuple<List<double>, List<double>>(new List<double>(constant), new List<double>(multiple));
        }

        /// <summary>
        /// Determines if the given point is inside the polygon
        /// </summary>
        /// <param name="polygon">the vertices of polygon</param>
        /// <param name="point">the given point</param>
        /// <returns>true if the point is inside the polygon; otherwise, false</returns>
        /// <remarks>http://stackoverflow.com/questions/4243042/c-sharp-point-in-polygon</remarks>
        public static bool PointInPolygonMeowNET(
            List<PointF> polygon, PointF point)
        {
            bool result = false;
            int j = polygon.Count() - 1;
            for (int i = 0; i < polygon.Count(); i++)
            {
                if (polygon[i].Y < point.Y && polygon[j].Y >= point.Y || polygon[j].Y < point.Y && polygon[i].Y >= point.Y)
                {
                    if (polygon[i].X + (point.Y - polygon[i].Y)
                        / (polygon[j].Y - polygon[i].Y)
                        * (polygon[j].X - polygon[i].X) < point.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://alienryderflex.com/polygon/
        ///  Globals which should be set before calling this function:
        ///
        ///  int    polygon.Count  =  how many corners the polygon has (no repeats)
        ///  double  polyX[]      =  horizontal coordinates of corners
        ///  double  polyY[]      =  vertical coordinates of corners
        ///  double  x, y         =  point to be tested
        ///
        ///  (Globals are used in this example for purposes of speed.  Change as
        ///  desired.)
        ///
        ///  The function will return YES if the point x,y is inside the polygon, or
        ///  NO if it is not.  If the point is exactly on the edge of the polygon,
        ///  then the function may return YES or NO.
        ///
        ///  Note that division by zero is avoided because the division is protected
        ///  by the "if" clause which surrounds it.
        /// </remarks>
        public static bool PointInPolygonAlienRyderFlex(
            List<PointF> polygon, PointF point)
        {
            int i;
            int j = polygon.Count - 1;
            bool oddNodes = false;

            for (i = 0; i < polygon.Count; i++)
            {
                if (polygon[i].Y < point.Y && polygon[j].Y >= point.Y
                || polygon[j].Y < point.Y && polygon[i].Y >= point.Y)
                {
                    if (polygon[i].X + (point.Y - polygon[i].Y)
                        / (polygon[j].Y - polygon[i].Y)
                        * (polygon[j].X - polygon[i].X) < point.X)
                    {
                        oddNodes = !oddNodes;
                    }
                }
                j = i;
            }

            return oddNodes;
        }

        /// <summary>
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://alienryderflex.com/polygon/
        ///  Globals which should be set before calling this function:
        ///
        ///  int    polygon.Count  =  how many corners the polygon has (no repeats)
        ///  double  polyX[]      =  horizontal coordinates of corners
        ///  double  polyY[]      =  vertical coordinates of corners
        ///  double  x, y         =  point to be tested
        ///
        ///  (Globals are used in this example for purposes of speed.  Change as
        ///  desired.)
        ///
        ///  The function will return YES if the point x,y is inside the polygon, or
        ///  NO if it is not.  If the point is exactly on the edge of the polygon,
        ///  then the function may return YES or NO.
        ///
        ///  Note that division by zero is avoided because the division is protected
        ///  by the "if" clause which surrounds it.
        /// </remarks>
        public static bool PointInPolygonLaschaLagidse2(
            List<PointF> polygon, PointF point)
        {
            int i;
            int j = polygon.Count - 1;
            bool oddNodes = false;

            for (i = 0; i < polygon.Count; i++)
            {
                if ((polygon[i].Y < point.Y && polygon[j].Y >= point.Y
                || polygon[j].Y < point.Y && polygon[i].Y >= point.Y)
                && (polygon[i].X <= point.X || polygon[j].X <= point.X))
                {
                    oddNodes ^= (polygon[i].X + (point.Y - polygon[i].Y)
                        / (polygon[j].Y - polygon[i].Y)
                        * (polygon[j].X - polygon[i].X) < point.X);
                }
                j = i;
            }

            return oddNodes;
        }

        /// <summary>
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/4243042/c-sharp-point-in-polygon
        /// http://stackoverflow.com/questions/217578/point-in-polygon-aka-hit-test
        /// http://www.ecse.rpi.edu/Homepages/wrf/Research/Short_Notes/pnpoly.html
        /// </remarks>
        public static bool PointInPolygonGilKr(
            List<PointF> polygon, PointF point)
        {
            int nvert = polygon.Count;
            bool c = false;
            for (int i = 0, j = nvert - 1; i < nvert; j = i++)
            {
                if (((polygon[i].Y > point.Y) != (polygon[j].Y > point.Y)) &&
                 (point.X < (polygon[j].X - polygon[i].X)
                 * (point.Y - polygon[i].Y)
                 / (polygon[j].Y - polygon[i].Y) + polygon[i].X))
                    c = !c;
            }
            return c;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/217578/how-can-i-determine-whether-a-2d-point-is-within-a-polygon
        /// http://stackoverflow.com/questions/217578/point-in-polygon-aka-hit-test
        /// </remarks>
        public static bool PointInPolygonMKatzWRandolphFranklin(
            List<PointF> polygon, PointF point)
        {
            double minX = polygon[0].X;
            double maxX = polygon[0].X;
            double minY = polygon[0].Y;
            double maxY = polygon[0].Y;
            for (int i = 1; i < polygon.Count; i++)
            {
                PointF q = polygon[i];
                minX = Min(q.X, minX);
                maxX = Max(q.X, maxX);
                minY = Min(q.Y, minY);
                maxY = Max(q.Y, maxY);
            }

            if (point.X < minX || point.X > maxX || point.Y < minY || point.Y > maxY)
            {
                return false;
            }

            // http://www.ecse.rpi.edu/Homepages/wrf/Research/Short_Notes/pnpoly.html
            bool inside = false;
            for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
            {
                if ((polygon[i].Y > point.Y) != (polygon[j].Y > point.Y) &&
                     point.X < (polygon[j].X - polygon[i].X) * (point.Y - polygon[i].Y)
                     / (polygon[j].Y - polygon[i].Y) + polygon[i].X)
                {
                    inside = !inside;
                }
            }

            return inside;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://paulbourke.net/geometry/polygonmesh/
        /// http://astronomy.swin.edu.au/pbourke/geometry/
        /// http://www.eecs.umich.edu/courses/eecs380/HANDOUTS/PROJ2/InsidePoly.html
        /// </remarks>
        public static bool PointInPolygonPaulBourke(
            List<PointF> polygon, PointF point)
        {
            PointF p1, p2;
            int counter = 0;
            int i;
            int N = polygon.Count;
            double xinters;
            p1 = polygon[0];
            for (i = 1; i <= N; i++)
            {
                p2 = polygon[i % N];
                if (point.Y > Min(p1.Y, p2.Y))
                {
                    if (point.Y <= Max(p1.Y, p2.Y))
                    {
                        if (point.X <= Max(p1.X, p2.X))
                        {
                            if (p1.Y != p2.Y)
                            {
                                xinters = (point.Y - p1.Y) * (p2.X - p1.X) / (p2.Y - p1.Y) + p1.X;
                                if (p1.X == p2.X || point.X <= xinters) counter++;
                            }
                        }
                    }
                }
                p1 = p2;
            }

            return (counter % 2 != 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>https://www.ecse.rpi.edu/Homepages/wrf/Research/Short_Notes/pnpoly.html</remarks>
        public static bool PointInPolygonWRandolphFranklin(
            List<PointF> polygon, PointF point)
        {
            bool inside = false;
            int nvert = polygon.Count;
            for (int i = 0, j = nvert - 1; i < nvert; j = i++)
            {
                if (((polygon[i].Y > point.Y) != (polygon[j].Y > point.Y)) &&
                 (point.X < (polygon[j].X - polygon[i].X) * (point.Y - polygon[i].Y)
                 / (polygon[j].Y - polygon[i].Y) + polygon[i].X))
                    inside = !inside;
            }
            return inside;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>http://stackoverflow.com/questions/4243042/c-sharp-point-in-polygon</remarks>
        public static bool PointInPolygonSaeedAmiri(
            List<PointF> polygon, PointF point)
        {
            var coef = polygon.Skip(1).Select((p, i) =>
                  (p.X - polygon[i].X) * (point.Y - polygon[i].Y)
                - (p.Y - polygon[i].Y) * (point.X - polygon[i].X)
                ).ToList();

            if (coef.Any(p => p == 0)) return true;

            for (int i = 1; i < coef.Count(); i++)
            {
                if (coef[i] * coef[i - 1] < 0) return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>http://paulbourke.net/geometry/polygonmesh/</remarks>
        public static bool PointInPolygonPhilippeReverdy(
            List<PointF> polygon, PointF point)
        {
            int i;
            double angle = 0;
            PointF p1 = new PointF();
            PointF p2 = new PointF();
            int n = polygon.Count;
            for (i = 0; i < n; i++)
            {
                p1.X = polygon[i].X - point.X;
                p1.Y = polygon[i].Y - point.Y;
                p2.X = polygon[(i + 1) % n].X - point.X;
                p2.Y = polygon[(i + 1) % n].Y - point.Y;
                angle += Angle2D(p1.X, p1.Y, p2.X, p2.Y);
            }

            return !(Abs(angle) < PI);
        }
        /// <summary>
        /// Return the angle between two vectors on a plane
        /// The angle is from vector 1 to vector 2, positive anticlockwise
        /// The result is between -pi -> pi
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static double Angle2D(double x1, double y1, double x2, double y2)
        {
            double dtheta, theta1, theta2;

            theta1 = Atan2(y1, x1);
            theta2 = Atan2(y2, x2);
            dtheta = theta2 - theta1;
            while (dtheta > PI)
                dtheta -= (PI * 2);
            while (dtheta < -PI)
                dtheta += (PI * 2);

            return (dtheta);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="point"></param>
        /// <returns>Return true if the point is in the polygon.</returns>
        /// <remarks>http://csharphelper.com/blog/2014/07/determine-whether-a-point-is-inside-a-polygon-in-c/</remarks>
        public static bool PointInPolygonRodStephens(
            List<PointF> polygon, PointF point)
        {
            // Get the angle between the point and the
            // first and last vertices.
            int max_point = polygon.Count - 1;
            double total_angle = AngleVector_0(
                polygon[max_point].X, polygon[max_point].Y,
                point.X, point.Y,
                polygon[0].X, polygon[0].Y);

            // Add the angles from the point
            // to each other pair of vertices.
            for (int i = 0; i < max_point; i++)
            {
                total_angle += AngleVector_0(
                    polygon[i].X, polygon[i].Y,
                    point.X, point.Y,
                    polygon[i + 1].X, polygon[i + 1].Y);
            }

            // The total angle should be 2 * PI or -2 * PI if
            // the point is in the polygon and close to zero
            // if the point is outside the polygon.
            return (Abs(total_angle) > 0.000001);
        }

        /// <summary>
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <remarks>
        /// http://stackoverflow.com/questions/4243042/c-sharp-point-in-polygon
        /// https://social.msdn.microsoft.com/Forums/windows/en-US/95055cdc-60f8-4c22-8270-ab5f9870270a/determine-if-the-point-is-in-the-polygon-c?forum=winforms
        /// </remarks>
        public static bool PointInPolygonKeith(
            List<PointF> polygon, PointF point)
        {
            PointF p1, p2;

            bool inside = false;

            if (polygon.Count < 3) return inside;

            var oldPoint = polygon[polygon.Count - 1];

            for (int i = 0; i < polygon.Count; i++)
            {
                var newPoint = polygon[i];

                if (newPoint.X > oldPoint.X)
                {
                    p1 = oldPoint;
                    p2 = newPoint;
                }
                else
                {
                    p1 = newPoint;
                    p2 = oldPoint;
                }

                if ((newPoint.X < point.X) == (point.X <= oldPoint.X)
                    && (point.Y - (long)p1.Y) * (p2.X - p1.X)
                    < (p2.Y - (long)p1.Y) * (point.X - p1.X))
                {
                    inside = !inside;
                }

                oldPoint = newPoint;
            }

            return inside;
        }

        //        /// <summary>
        //        /// 
        //        /// </summary>
        //        /// <param name="polygon"></param>
        //        /// <param name="point"></param>
        //        /// <returns></returns>
        //        /// <remarks>
        //        /// http://stackoverflow.com/questions/11716268/point-in-polygon-algorithm
        //        /// </remarks>
        //        public static bool point_in_polygon_check_edge(List<PointF> polygon, PointF point, double edge_error = 1.192092896e-07f)
        //        {
        //            int x = 0;
        //            int y = 1;
        //            bool r = false;
        //            for (int i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
        //            {
        //                PointF pi = polygon[i];
        //                PointF pj = polygon[j];
        //            if (fabs(pi[y] - pj[y]) <= edge_error && fabs(pj[y] - v[y]) <= edge_error && (pi[x] >= v[x]) != (pj[x] >= v[x]))
        //            {
        //                return true;
        //            }

        //            if ((pi[y] > v[y]) != (pj[y] > v[y]))
        //            {
        //                double c = (pj[x] - pi[x]) * (v[y] - pi[y]) / (pj[y] - pi[y]) + pi[x];
        //                if (fabs(v[x] - c) <= edge_error)
        //                {
        //                    return true;
        //                }
        //                if (v[x] < c)
        //                {
        //                    r = !r;
        //                }
        //            }
        //        }
        //    return r;
        //}

        /// <summary>
        /// is target point inside a 2D polygon?
        /// </summary>
        /// <param name="poly">polygon points</param>
        /// <param name="xt">x (horizontal) of target point</param>
        /// <param name="yt"> y (vertical) of target point</param>
        /// <returns></returns>
        public static bool PointInPolygonBobStein(
            List<PointF> polygon, PointF point)
        {
            double xnew, ynew;
            double xold, yold;
            double x1, y1;
            double x2, y2;
            int i;
            bool inside = false;
            int npoints = polygon.Count;
            if (npoints < 3)
            {
                return (false);
            }
            xold = polygon[npoints - 1].X;
            yold = polygon[npoints - 1].Y;
            for (i = 0; i < npoints; i++)
            {
                xnew = polygon[i].X;
                ynew = polygon[i].Y;
                if (xnew > xold)
                {
                    x1 = xold;
                    x2 = xnew;
                    y1 = yold;
                    y2 = ynew;
                }
                else
                {
                    x1 = xnew;
                    x2 = xold;
                    y1 = ynew;
                    y2 = yold;
                }
                if ((xnew < point.X) == (point.X <= xold)          /* edge "open" at one end */
                 && ((long)point.Y - (long)y1) * (long)(x2 - x1)
                  < ((long)point.Y - (long)y1) * (long)(point.X - x1))
                {
                    inside = !inside;
                }
                xold = xnew;
                yold = ynew;
            }
            return (inside);
        }
        #endregion

        #region Quadratic Bezier Interpolation of 1D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 1D cubic interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(QuadraticBezierInterpolate1DTests))]
        public static List<SpeedTester> QuadraticBezierInterpolate1DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => QuadraticBezierInterpolate1D_0(0, 1, 2, 0.5d),
                $"{nameof(Experiments.QuadraticBezierInterpolate1D_0)}(0, 1, 2, 0.5d)"),
                new SpeedTester(() => QuadraticBezierInterpolate1D_1(0, 1, 2, 0.5d),
                $"{nameof(Experiments.QuadraticBezierInterpolate1D_1)}(0, 1, 2, 0.5d)"),
            };
        }

        /// <summary>
        /// Three control point Bezier interpolation mu ranges from 0 to 1, start to end of the curve.
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private static double QuadraticBezierInterpolate1D_0(
            double x0,
            double x1,
            double x2,
            double t)
        {
            double mu1 = 1 - t;
            double mu12 = mu1 * mu1;
            double mu2 = t * t;

            return x0 * mu12 + 2 * x1 * mu1 * t + x2 * mu2;
        }

        /// <summary>
        /// Evaluate a point on a bezier-curve. t goes from 0 to 1.0
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://www.cubic.org/docs/bezier.htm</remarks>
        private static double QuadraticBezierInterpolate1D_1(
            double x0,
            double x1,
            double x2,
            double t)
        {
            // point between a and b
            double ab = LinearInterpolate1D_0(x0, x1, t);
            // point between b and c
            double bc = LinearInterpolate1D_0(x1, x2, t);
            // point on the bezier-curve
            return LinearInterpolate1D_0(ab, bc, t);
        }

        #endregion

        #region Quadratic Bezier Interpolation of 2D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 2D cubic interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(QuadraticBezierInterpolate2DTests))]
        public static List<SpeedTester> QuadraticBezierInterpolate2DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => QuadraticBezierInterpolate2D_0(0, 1, 2, 3, 4, 5, 0.5d),
                $"{nameof(Experiments.QuadraticBezierInterpolate2D_0)}(0, 1, 2, 3, 4, 5, 0.5d)"),
                new SpeedTester(() => QuadraticBezierInterpolate2D_1(0, 1, 2, 3, 4, 5, 0.5d),
                $"{nameof(Experiments.QuadraticBezierInterpolate2D_1)}(0, 1, 2, 3, 4, 5, 0.5d)"),
            };
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
        private static Tuple<double, double> QuadraticBezierInterpolate2D_0(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double t)
        {
            double mu1 = 1 - t;
            double mu12 = mu1 * mu1;
            double mu2 = t * t;

            return new Tuple<double, double>(
                (x0 * mu12 + 2 * x1 * mu1 * t + x2 * mu2),
                (y0 * mu12 + 2 * y1 * mu1 * t + y2 * mu2)
                );
        }

        /// <summary>
        /// Evaluate a point on a bezier-curve. t goes from 0 to 1.0
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <remarks>http://www.cubic.org/docs/bezier.htm</remarks>
        private static Tuple<double, double> QuadraticBezierInterpolate2D_1(
            double x0, double y0,
            double x1, double y1,
            double x2, double y2,
            double t)
        {
            // point between a and b
            Tuple<double, double> ab = LinearInterpolate2D_0(x0, y0, x1, y1, t);
            // point between b and c
            Tuple<double, double> bc = LinearInterpolate2D_0(x1, y1, x2, y2, t);
            // point on the bezier-curve
            return LinearInterpolate2D_0(ab.Item1, ab.Item2, bc.Item1, bc.Item2, t);
        }

        #endregion

        #region Quadratic Bezier Interpolation of 3D Points

        /// <summary>
        /// Set of tests to run testing methods that calculate the 3D cubic interpolation of a point.
        /// </summary>
        /// <returns></returns>
        [DisplayName(nameof(QuadraticBezierInterpolate3DTests))]
        public static List<SpeedTester> QuadraticBezierInterpolate3DTests()
        {
            return new List<SpeedTester>() {
                new SpeedTester(() => QuadraticBezierInterpolate3D_0(0, 1, 2, 3, 4, 5, 6, 7, 8, 0.5d),
                $"{nameof(Experiments.QuadraticBezierInterpolate3D_0)}(0, 1, 2, 3, 4, 5, 6, 7, 8, 0.5d)"),
                new SpeedTester(() => QuadraticBezierInterpolate3D_1(0, 1, 2, 3, 4, 5, 6, 7, 8, 0.5d),
                $"{nameof(Experiments.QuadraticBezierInterpolate3D_1)}(0, 1, 2, 3, 4, 5, 6, 7, 8, 0.5d)"),
            };
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
        private static Tuple<double, double, double> QuadraticBezierInterpolate3D_0(
            double x0, double y0, double z0,
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double t)
        {
            double mu1 = 1 - t;
            double mu12 = mu1 * mu1;
            double mu2 = t * t;

            return new Tuple<double, double, double>(
                (x0 * mu12 + 2 * x1 * mu1 * t + x2 * mu2),
                (y0 * mu12 + 2 * y1 * mu1 * t + y2 * mu2),
                (z0 * mu12 + 2 * z1 * mu1 * t + z2 * mu2));
        }

        /// <summary>
        /// Evaluate a point on a bezier-curve. t goes from 0 to 1.0
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
        /// <remarks>http://www.cubic.org/docs/bezier.htm</remarks>
        private static Tuple<double, double, double> QuadraticBezierInterpolate3D_1(
            double x0, double y0, double z0,
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double t)
        {
            // point between a and b
            Tuple<double, double, double> ab = LinearInterpolate3D_0(x0, y0, z0, x1, y1, z1, t);
            // point between b and c
            Tuple<double, double, double> bc = LinearInterpolate3D_0(x1, y1, z1, x2, y2, z2, t);
            // point on the bezier-curve
            return LinearInterpolate3D_0(ab.Item1, ab.Item2, ab.Item3, bc.Item1, bc.Item2, bc.Item3, t);
        }

        #endregion

        #region Slope of a 2D Vector

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
            => i == 0 ? SlopeMax : (j / i);

        #endregion

        #region Slope of a 2D Line

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
            => (x1 == x2) ? SlopeMax : ((y2 - y1) / (x2 - x1));

        #endregion

        #region Squared Distance Between Two 2D Points

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

        #endregion

        #region Squared Distance to a Line

        /// <summary>
        /// Find the square of the distance of a point from a line.
        /// </summary>
        /// <param name="x1">The x component of the Point.</param>
        /// <param name="y1">The y component of the Point.</param>
        /// <param name="x2_">The x component of the first point on the line.</param>
        /// <param name="y2_">The y component of the first point on the line.</param>
        /// <param name="x3_">The x component of the second point on the line.</param>
        /// <param name="y3_">The y component of the second point on the line.</param>
        /// <returns></returns>
        [Pure]
        public static double SquareDistanceToLine(
            double x1, double y1,
            double x2_, double y2_,
            double x3_, double y3_)
        {
            double A = y2_ - y3_;
            double B = x3_ - x2_;
            double C = (A * x1 + B * y1) - (A * x2_ + B * y2_);
            return (C * C) / (A * A + B * B);
        }

        #endregion
    }
}
